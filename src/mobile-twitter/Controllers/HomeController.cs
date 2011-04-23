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
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Elmah;
using mobile_twitter.Model;
using TweetSharp;

namespace mobile_twitter.Controllers
{
    [RequiresAccessToken]
    public class HomeController : TweetgoController
    {
        public ActionResult Index(long? replyTo)
        {
            if (Token == null)
            {
                ViewData["Url"] = new AuthenticationService().GetAuthenticationUrl();
                return View("Unauthenticated");
            }

            var service = new StatusesService(Token);

            ViewData["Action"] = "Index";
            ViewData["PageTitle"] = "home";

            if (replyTo != null)
            {
                try
                {
                    ViewData["InReplyTo"] = service.GetSingleStatus(replyTo.Value);
                }
                catch (TweetSharpException ex)
                {
                    //todo - I can't find a good way to detect invalid tweet IDs other than catching the error when thrown.  
                    //So, we try/catch, signal the error and redirect home without the replyTo.
                    ErrorSignal.FromCurrentContext().Raise(ex, System.Web.HttpContext.Current);
                    return RedirectToAction("Index");
                }
            }

            //if this call fails it will fail in the view and be handled by our standard error handing mechanism
            ViewData["loadInitialData"] =
                ExecuteOrRedirect(() => new JavaScriptSerializer().Serialize(service.OnHomeTimeline(null, null)));

            ViewData["loadMoreUrl"] = "/statuses/onHomeTimeline/";

            return View();
        }

        private Func<string> ExecuteOrRedirect(Func<string> func)
        {
            return () =>
                       {
                           //try {
                           return func();
                           //}
                           //catch {
                           //   return "''; location.href='/resources/errors/default.html';";
                           // }
                       };
        }

        public ActionResult Mentions()
        {
            ViewData["Action"] = "Mentions";
            ViewData["PageTitle"] = "mentions";

            //if this call fails it will fail in the view and be handled by our standard error handing mechanism
            ViewData["loadInitialData"] =
                ExecuteOrRedirect(
                    () => new JavaScriptSerializer().Serialize(new StatusesService(Token).Mentions(null, null)));

            ViewData["loadMoreUrl"] = "/statuses/mentions/";

            return View("Index");
        }

        public ActionResult Favorites()
        {
            ViewData["Action"] = "Favorites";
            ViewData["PageTitle"] = "favorites";

            //if this call fails it will fail in the view and be handled by our standard error handing mechanism
            ViewData["loadInitialData"] =
                ExecuteOrRedirect(() => new JavaScriptSerializer().Serialize(new StatusesService(Token).Favorites(1)));

            ViewData["loadMoreUrl"] = "/statuses/favorites/";
            ViewData["moreMode"] = "page";

            return View("Index");
        }

        public ActionResult Conversation(long id)
        {
            ViewData["Action"] = "Conversation";
            ViewData["PageTitle"] = "conversation";

            //if this call fails it will fail in the view and be handled by our standard error handing mechanism
            ViewData["loadInitialData"] =
                ExecuteOrRedirect(
                    () => new JavaScriptSerializer().Serialize(new StatusesService(Token).FollowConversation(id)));

            //ViewData["loadMoreUrl"] = "/statuses/favorites/";
            //ViewData["moreMode"] = "page";

            return View("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tweet(string status, double? lat, double? lon, long? inReplyTo)
        {
            try
            {
                new StatusesService(Token).Update(status, lat, lon, inReplyTo);
                Flash("Status Updated");
            }
            catch (StatusUpdateException ex)
            {
                FlashError(ex.FriendlyErrorMessage);
            }

            return RedirectHome();
        }

        /// <summary>
        ///     Returns JSON. Throws exceptions on error.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Favorite(long statusId)
        {
            new StatusesService(Token).Favorite(statusId);

            return Success();
        }

        /// <summary>
        ///     Returns JSON. Throws exceptions on error.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnFavorite(long statusId)
        {
            new StatusesService(Token).Unfavorite(statusId);

            return Success();
        }

        /// <summary>
        ///     Returns JSON. Throws exceptions on error.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Retweet(long statusId)
        {
            new StatusesService(Token).ReTweet(statusId);

            return Success();
        }

        /// <summary>
        ///     Returns JSON. Throws exceptions on error.
        /// </summary>
        public ActionResult GetStatus(long statusId)
        {
            return StandardJsonResult(new StatusesService(Token), s => s.GetSingleStatus(statusId));
        }
    }
}