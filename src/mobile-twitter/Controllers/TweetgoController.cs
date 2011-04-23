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
using System.Web.Routing;
using mobile_twitter.Infrastucture;
using mobile_twitter.Model;
using TweetSharp.Model;

namespace mobile_twitter.Controllers
{
    [ValidateInput(false)]
    [PersistTempDataBeforeView]
    public class TweetgoController : Controller
    {
        public OAuthToken Token { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            TempDataProvider = new CookieTempDataProvider();
            base.Initialize(requestContext);
        }

        protected ActionResult NotFound()
        {
            throw new HttpException(404, "Not Found");
        }

        protected ActionResult Success()
        {
            return Json(new {success = true});
        }

        protected ActionResult RedirectHome()
        {
            return RedirectToAction("Index", "Home");
        }

        protected void FlashError(string message)
        {
            TempData["FlashError"] = ViewData["FlashError"] = message;
        }

        protected void Flash(string message)
        {
            TempData["FlashInformation"] = ViewData["FlashInformation"] = message;
        }

        protected JsonResult StandardJsonResult<T>(T service, Func<T, object> executeResponse) where T : Service
        {
            var results = executeResponse(service);
            object rateLimitingStatus = null;

            if (service.RateLimitingStatus != null)
            {
                rateLimitingStatus = new RateLimitingStatusDto(service.RateLimitingStatus);
            }

            return Json(new {results, rateLimitingStatus});
        }
    }
}