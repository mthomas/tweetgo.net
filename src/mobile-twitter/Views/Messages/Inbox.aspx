<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>inbox
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Inbox | <%=Html.ActionLink("Sent", "Sent", "Messages") %></h2>

    <% Html.RenderPartial("Send"); %>
    
    <% Html.RenderPartial("Messages"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("MessageTemplate"); %>
    
    <script type="text/javascript">
        $(".rply a", $("#t")[0]).die("click");
        
        $(".rply a", $("#t")[0]).live("click", function(e) {
            var params = StatusActionParams(this);
            var screenName = params.clicked.attr("screenName");

            $("#toScreenName").val(screenName);
            $("#message").focus();
        });
        
        var nextPage = 1;
        var loadMoreMessagesUrl = "/messages/getRecievedMessages/";

        loadMoreMessages();
    </script>
</asp:Content>