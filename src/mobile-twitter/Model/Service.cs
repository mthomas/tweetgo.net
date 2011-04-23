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
using TweetSharp;
using TweetSharp.Model;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Service;

namespace mobile_twitter.Model
{
    public class TwitterServiceEx : TwitterService
    {
        public TwitterServiceEx(IClientInfo info) : base(info)
        {
        }

        public T WithTweetSharpEx<T>(Func<IFluentTwitter, ITwitterLeafNode> executor) where T : class
        {
            return WithTweetSharp<T>(executor);
        }

        public IFluentTwitter GetFluent()
        {
            return base.GetAuthenticatedQuery();
        }
    }

    public abstract class Service
    {
        protected readonly OAuthToken _currentUserToken;
        protected readonly TwitterServiceEx _service;

        protected Service(OAuthToken currentUserToken)
        {
            if (currentUserToken == null)
            {
                throw new NullReferenceException("currentUserToken");
            }

            var info = new TwitterClientInfo
                           {
                               ConsumerKey = OAuth.GetOAuthSecrets().Key,
                               ConsumerSecret = OAuth.GetOAuthSecrets().Secret,
                           };


            _service = new TwitterServiceEx(info);
            _service.AuthenticateWith(currentUserToken.Token, currentUserToken.TokenSecret);


            _service.CacheWith(null);


            _currentUserToken = currentUserToken;
        }

        public TwitterRateLimitStatus RateLimitingStatus
        {
            get { return _service.RateLimitStatus; }
        }

        protected string CurrentUserScreenName
        {
            get { return _currentUserToken.ScreenName; }
        }
    }
}