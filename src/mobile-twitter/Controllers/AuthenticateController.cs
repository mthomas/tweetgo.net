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
using System.Web;
using System.Web.Mvc;
using mobile_twitter.Model;

namespace mobile_twitter.Controllers
{
    public class AuthenticateController : TweetgoController
    {
        private readonly AuthenticationService _service = new AuthenticationService();

        public ActionResult Index()
        {
            ClearAuthCookie();
            return RedirectHome();
        }

        public ActionResult Complete(string oauth_token, string denied)
        {
            if (denied != null)
            {
                return View("Denied");
            }

            if (String.IsNullOrEmpty(oauth_token))
            {
                FlashError("No token was provided.");
                return RedirectToAction("Index");
            }

            var accessToken = _service.CreateAndSignAccessToken(oauth_token);

            if (accessToken == null)
            {
                FlashError("Unable to validate the provided token.");
                return RedirectToAction("Index");
            }
            else
            {
                Response.Cookies.Add(CreateCookie(accessToken));

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOut()
        {
            ClearAuthCookie();

            return View();
        }

        private void ClearAuthCookie()
        {
            var cookie = Request.Cookies[AuthenticationService.CookieName];

            if (cookie != null)
            {
                cookie.Value = null;
                cookie.Expires = DateTime.Now.AddDays(-100);
                Response.Cookies.Set(cookie);
            }
        }

        private HttpCookie CreateCookie(string oauthToken)
        {
            var cookie = new HttpCookie(AuthenticationService.CookieName, oauthToken);
            cookie.Expires = DateTime.Now.AddDays(30);
            cookie.HttpOnly = true;
            return cookie;
        }
    }
}