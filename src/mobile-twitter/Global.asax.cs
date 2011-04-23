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
using System.Web.UI;

namespace mobile_twitter
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute("inbox",
                            "inbox",
                            new {controller = "Messages", action = "inbox", page = 1});

            routes.MapRoute("sent",
                            "sent",
                            new {controller = "Messages", action = "sent", page = 1});

            routes.MapRoute("send",
                            "send",
                            new {Controller = "messages", action = "send"});


            routes.MapRoute("list",
                            "lists/{ownerScreenName}/{listSlug}",
                            new {Controller = "lists", action = "show"});

            routes.MapRoute("user",
                            "user/{screenName}",
                            new {controller = "Users", action = "Index"});


            routes.MapRoute("Favorites",
                            "favorites",
                            new {controller = "Home", action = "Favorites"});

            routes.MapRoute("Mentions",
                            "mentions",
                            new {controller = "Home", action = "Mentions"});

            routes.MapRoute("Conversation",
                            "conversation/{id}",
                            new {controller = "Home", action = "Conversation"});

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = ""} // Parameter defaults
                );
        }


        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);


            Error += MvcApplication_Error;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleError(e.ExceptionObject as Exception);
        }

        private void HandleError(Exception e)
        {
            var lastError = (e as ViewStateException);
            if (lastError == null && e != null)
            {
                lastError = HttpContext.Current.Error.GetBaseException() as ViewStateException;
            }

            if (lastError != null)
            {
                var cookies = Request.Cookies.AllKeys;
                foreach (var cookie in cookies)
                {
                    HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }

                HttpContext.Current.ClearError();
                Response.Redirect("~/");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private void MvcApplication_Error(object sender, EventArgs e)
        {
            HandleError(HttpContext.Current.Error);
        }
    }
}