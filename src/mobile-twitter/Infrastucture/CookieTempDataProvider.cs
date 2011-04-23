#region license

// Copyright (c) 2011 Michael Thomas
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace mobile_twitter.Infrastucture
{
    //TODO - further audit this.  Needs to be simplified too.
    public class CookieTempDataProvider : ITempDataProvider
    {
        private const string TempDataCookieKey = "__ControllerTempData";
        private const string SigningKey = "AAAAAAAAAAAAAAAAAAAAAAAAAA";

        #region ITempDataProvider Members

        IDictionary<string, object> ITempDataProvider.LoadTempData(ControllerContext controllerContext)
        {
            return LoadTempData(controllerContext);
        }

        void ITempDataProvider.SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            SaveTempData(controllerContext, values);
        }

        #endregion

        protected virtual IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            var httpContext = controllerContext.HttpContext;

            var cookie = httpContext.Request.Cookies[TempDataCookieKey];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                try
                {
                    var deserializedTempData = DeserializeTempData(cookie.Value);
                    ClearCookies(cookie, httpContext);
                    return deserializedTempData;
                }
                catch
                {
                    ClearCookies(cookie, httpContext);
                    httpContext.Response.Redirect("/");
                    httpContext.ApplicationInstance.CompleteRequest();
                }
            }

            return new Dictionary<string, object>();
        }

        private void ClearCookies(HttpCookie cookie, HttpContextBase httpContext)
        {
            cookie.Expires = DateTime.MinValue;
            cookie.Value = string.Empty;

            if (httpContext.Response != null && httpContext.Response.Cookies != null)
            {
                var responseCookie = httpContext.Response.Cookies[TempDataCookieKey];
                if (responseCookie != null)
                {
                    responseCookie.Expires = new DateTime(1980, 1, 1);
                    responseCookie.Value = string.Empty;
                    responseCookie.HttpOnly = true;
                }
                else
                {
                    httpContext.Response.Cookies.Add(new HttpCookie(TempDataCookieKey)
                                                         {
                                                             Value = "",
                                                             HttpOnly = true,
                                                             Expires = new DateTime(1980, 1, 1),
                                                         });
                }
            }
        }


        protected virtual void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            var httpContext = controllerContext.HttpContext;


            if (httpContext.Items["__Flushed"] != null && (bool) httpContext.Items["__Flushed"])
            {
                return;
            }

            var cookieValue = SerializeTempData(values);

            if (httpContext.Response.Cookies[TempDataCookieKey] != null)
            {
                httpContext.Response.Cookies.Remove(TempDataCookieKey);
            }

            var cookie = new HttpCookie(TempDataCookieKey)
                             {
                                 HttpOnly = true,
                                 Value = cookieValue
                             };

            httpContext.Response.Cookies.Add(cookie);
        }

        private static IDictionary<string, object> DeserializeTempData(string signedData)
        {
            var message = SignedString.ExtractAndVerifyMessage(signedData, SigningKey);

            return new JavaScriptSerializer().Deserialize<IDictionary<string, object>>(message);
        }

        private static string SerializeTempData(IDictionary<string, object> values)
        {
            return SignedString.CreateSignedString(new JavaScriptSerializer().Serialize(values), SigningKey);
        }
    }
}