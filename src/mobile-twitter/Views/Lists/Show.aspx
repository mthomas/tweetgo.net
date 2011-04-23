<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TweetSharp.Twitter.Model.TwitterList>" %>
<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>lists<%=Settings.TitleSeperator %><%=Html.Encode(Model.Name) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>@<%=Html.Encode(Model.User.ScreenName) %>/<%=Html.Encode(Model.Slug) %></h2>

   <% Html.RenderPartial("TweetsPresenter"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("StatusTemplate"); %>
    
    <% Context.Items["__Flushed"] = true;
        Response.Flush(); %>
    
    <script type="text/javascript">
        var linkReplies = true;
        var url = "/lists/getStatuses/";
        var hideScreenName = false;
        var pollForNewTweets = true;
        var params = {"listOwnerScreenName":"<%=Model.User.ScreenName %>", "listSlug":"<%=Model.Slug %>"};
        var initialData = <%=((Func<string>)ViewData["loadInitialData"])()%>;
        var moreMode = "since";
        
        handleInitialTweets(initialData);
    </script>
</asp:Content>
