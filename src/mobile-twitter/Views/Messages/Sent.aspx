<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TwitterDirectMessage>>" %>

<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>sent messages
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.ActionLink("Inbox", "Inbox", "Messages") %> | Sent</h2>
	
    <% Html.RenderPartial("Send"); %>
    
    <% Html.RenderPartial("Messages"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
	<% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("MessageTemplate"); %>
    
    <script type="text/javascript">
        var nextPage = 1;
        var loadMoreMessagesUrl = "/messages/getSentMessages/";

        loadMoreMessages();
    </script>
</asp:Content>
