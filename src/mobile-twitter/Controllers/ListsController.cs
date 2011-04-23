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
using mobile_twitter.Model;

namespace mobile_twitter.Controllers
{
    [RequiresAccessToken]
    public class ListsController : TweetgoController
    {
        public ActionResult Index()
        {
            return View(new ListService(Token).ListLists(Token.ScreenName));
        }

        public ActionResult Show(string ownerScreenName, string listSlug)
        {
            var service = new ListService(Token);
            var list = service.GetList(ownerScreenName, listSlug);

            if (list == null)
            {
                return NotFound();
            }

            //will throw in view exception handled normally
            ViewData["loadInitialData"] =
                new Func<string>(
                    () =>
                    new JavaScriptSerializer().Serialize(service.GetStatuses(ownerScreenName, listSlug, null, null)));

            return View(list);
        }

        /// <summary>
        ///     returns JSON - throws exception on invalid parameters
        /// </summary>
        public ActionResult GetStatuses(string listOwnerScreenName, string listSlug, long? olderThan, long? newerThan)
        {
            var service = new ListService(Token);

            return StandardJsonResult(service, s => s.GetStatuses(listOwnerScreenName, listSlug, olderThan, newerThan));
        }
    }
}