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
using System.IO;
using System.Web;

namespace mobile_twitter.Infrastucture
{
    public class YSlowHttpModule : IHttpModule
    {
        private static readonly List<string> _headersToRemove = new List<string>
                                                                    {
                                                                        "X-AspNet-Version",
                                                                        "X-AspNetMvc-Version",
                                                                        "Etag",
                                                                        "Server",
                                                                    };

        private static readonly HashSet<string> _longCacheExtensions = new HashSet<string>
                                                                           {
                                                                               ".js",
                                                                               ".css",
                                                                               ".png",
                                                                               ".jpg",
                                                                               ".gif",
                                                                               ".ico",
                                                                           };

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextEndRequest;
        }

        public void Dispose()
        {
        }

        #endregion

        private static void ContextEndRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;

            _headersToRemove.ForEach(h => context.Response.Headers.Remove(h));
            var extension = Path.GetExtension(context.Request.Url.AbsolutePath);

            if (_longCacheExtensions.Contains(extension))
            {
                context.Response.CacheControl = "Public";
                context.Response.Expires = 44000*12; //about a year
            }
        }
    }
}