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

using System.Web;
using System.Web.Mvc;
using mobile_twitter.Model;
using TweetSharp.Model;

namespace mobile_twitter.Controllers
{
    public class RequiresAccessTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = GetAccessToken(filterContext.HttpContext);

            if (token == null || token.Token == null)
            {
                if (filterContext.ActionDescriptor.ActionName == "Index" &&
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Home")
                {
                    return;
                }

                filterContext.HttpContext.Response.Redirect("/");
                filterContext.HttpContext.ApplicationInstance.CompleteRequest();
                return;
            }

            filterContext.Controller.ViewData["Username"] = token.ScreenName;

            var tweetgoController = filterContext.Controller as TweetgoController;
            if (tweetgoController != null)
            {
                tweetgoController.Token = token;
            }

            if (filterContext.ActionParameters.ContainsKey("token"))
            {
                filterContext.ActionParameters["token"] = token;
            }
        }

        public static OAuthToken GetAccessToken(HttpContextBase context)
        {
            var cookie = context.Request.Cookies[AuthenticationService.CookieName];

            return new AuthenticationService().GetTokenFromCooke(cookie);
        }
    }
}