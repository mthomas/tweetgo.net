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
using System.Linq;
using TweetSharp.Model;

namespace mobile_twitter.Model
{
    public class MessagingService : Service
    {
        public MessagingService(OAuthToken currentUserToken) : base(currentUserToken)
        {
        }

        public IEnumerable<DirectMessage> GetInbox(int? page)
        {
            return
                _service.ListDirectMessagesReceived(page ?? 1, 20).Select(
                    m => new DirectMessage(m, DirectMessageRenderingStyle.Recieved)).ToList();
            ;
        }

        public IEnumerable<DirectMessage> GetSent(int? page)
        {
            return
                _service.ListDirectMessagesSent(page.AsPage(), 20).Select(
                    m => new DirectMessage(m, DirectMessageRenderingStyle.Sent)).ToList();
        }

        public bool SendDirectMessage(string toScreenName, string message)
        {
            if (!IsUserFollowedBy(_currentUserToken.ScreenName, toScreenName))
            {
                return false;
            }

            if (!IsValidMessage(message))
            {
                return false;
            }

            var sentMessage = _service.SendDirectMessage(toScreenName, message);

            return sentMessage != null;
        }

        private bool IsValidMessage(string message)
        {
            return !String.IsNullOrEmpty(message) && message.Length <= 140;
        }

        private bool IsUserFollowedBy(string screenName, string isFollowedByScreenName)
        {
            var frieldship = _service.GetFriendshipInfo(screenName, isFollowedByScreenName);
            return frieldship != null;
        }
    }
}