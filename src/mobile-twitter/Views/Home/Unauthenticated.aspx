<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Simple.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %> a mobile friendly Twitter client
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="public_home">
        <h1>welcome to <%=Settings.LogoMarkup %></h1>
        
        <p><%=Settings.LogoMarkup %> is an online, mobile interface to twitter.  <strong><a href="<%=ViewData["Url"] %>">Sign in with twitter</a></strong> and give it a try!</p>
        
        <p>
            <a href="<%=ViewData["Url"] %>">
                <img src="<%=Url.Content("~/resources/Sign-in-with-Twitter-lighter.png") %>" alt="sign in with twitter" style="margin: 0 auto; display: block;" />
            </a>
        </p>
        
        <p><strong>tweetgo.net</strong> features a simple, attractive, and easy to use design while supporting a wide variety of features. </p>

        <p>I designed tweetgo.net specifically for the Nokia N900 (Maemo 5), and think that it is one of the best Twitter clients for the N900.</p>
        
        <p><strong>tweetgo.net</strong> also works well on most other modern smartphones and mobile devices with a high resolution screen.</p>

        <p>read and agree to our <%=Html.ActionLink("Terms of Service", "tos", "help") %> before using tweetgo.net</p>
    </div>
</asp:Content>
