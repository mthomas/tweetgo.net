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

using System.Web.Mvc;
using mobile_twitter.Model;

namespace mobile_twitter.Controllers
{
    [RequiresAccessToken]
    public class StatusesController : TweetgoController
    {
        public JsonResult OnHometimeline(long? newerThan, long? olderThan)
        {
            var service = new StatusesService(Token);

            return StandardJsonResult(service, s => s.OnHomeTimeline(newerThan, olderThan));
        }

        public JsonResult Mentions(long? newerThan, long? olderThan)
        {
            var service = new StatusesService(Token);

            return StandardJsonResult(service, s => s.Mentions(newerThan, olderThan));
        }

        public JsonResult Favorites(int? page)
        {
            var service = new StatusesService(Token);

            return StandardJsonResult(service, s => s.Favorites(page));
        }

        public JsonResult ForUser(string screenName, long? newerThan, long? olderThan)
        {
            var service = new UserService(Token);

            return StandardJsonResult(service, s => s.Statuses(screenName, newerThan, olderThan));
        }
    }
}