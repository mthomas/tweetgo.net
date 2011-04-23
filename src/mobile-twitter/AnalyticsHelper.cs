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
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace mobile_twitter
{
    public static class AnalyticsHelper
    {
        // Copyright 2009 Google Inc. All Rights Reserved.
        private const string GaAccount = "XXXXXXX";
        private const string GaPixel = "~/ga.aspx";

        public static string GoogleAnalyticsImage(this HtmlHelper helper)
        {
            return "<img src=\"" + GoogleAnalyticsGetImageUrl(helper) + "\" />";
        }

        private static string GoogleAnalyticsGetImageUrl(this HtmlHelper helper)
        {
            var url = new StringBuilder();
            url.Append(VirtualPathUtility.ToAbsolute(GaPixel) + "?");
            url.Append("utmac=").Append(GaAccount);
            var RandomClass = new Random();
            url.Append("&utmn=").Append(RandomClass.Next(0x7fffffff));
            var referer = "-";
            if (helper.ViewContext.HttpContext.Request.UrlReferrer != null
                && "" != helper.ViewContext.HttpContext.Request.UrlReferrer.ToString())
            {
                referer = helper.ViewContext.HttpContext.Request.UrlReferrer.ToString();
            }
            url.Append("&utmr=").Append(HttpUtility.UrlEncode(referer));
            if (HttpContext.Current.Request.Url != null)
            {
                url.Append("&utmp=").Append(
                    HttpUtility.UrlEncode(helper.ViewContext.HttpContext.Request.Url.PathAndQuery));
            }
            url.Append("&guid=ON");
            return url.ToString().Replace("&", "&amp;");
        }
    }
}