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

using System.Collections.Generic;
using System.Linq;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace mobile_twitter.Model
{
    public class ListService : Service
    {
        public ListService(OAuthToken currentUserToken) : base(currentUserToken)
        {
        }

        public IEnumerable<TwitterList> ListLists(string ownerScreenName)
        {
            var fluent = _service.GetFluent();


            var a = fluent.Lists().GetListsBy(ownerScreenName).Request().AsLists();
            var b = fluent.Lists().GetSubscriptions(ownerScreenName).Request().AsLists();

            return a.Union(b);
        }

        /// <summary>
        ///     Returns null when no list is found.
        /// </summary>
        public TwitterList GetList(string listOwnerScreenName, string listSlug)
        {
            return _service.GetList(listOwnerScreenName, listSlug);
        }

        public IEnumerable<Status> GetStatuses(string ownerScreenName, string listSlug, long? olderThan, long? newerThan)
        {
            if (newerThan != null)
            {
                return
                    _service.ListTweetsOnListTimelineSince(ownerScreenName, listSlug, newerThan.Value).Select(
                        s => new Status(s)).ToArray();
            }

            if (olderThan != null)
            {
                return
                    _service.ListTweetsOnListTimelineBefore(ownerScreenName, listSlug, olderThan.Value).Select(
                        s => new Status(s)).ToArray();
            }

            return _service.ListTweetsOnListTimeline(ownerScreenName, listSlug).Select(s => new Status(s)).ToArray();
        }
    }
}