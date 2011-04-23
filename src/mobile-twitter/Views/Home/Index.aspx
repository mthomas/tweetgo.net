<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TwitterStatus>>" %>
<%@ Import Namespace="mobile_twitter.Model"%>
<%@ Import Namespace="System.Web.Script.Serialization"%>

<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Settings.TitleAndSeperator %><%=ViewData["PageTitle"] %>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%=ViewData["PageTitle"] %></h2>
    
    <% var inReplyTo = ViewData["InReplyTo"] as Status; %>

    <%using(Html.BeginForm("Tweet", "Home", FormMethod.Post, new{id="update_form"})){%>
        
        <input type="hidden" name="lat" id="lat" />
        <input type="hidden" name="lon" id="lon" />
        <input type="hidden" name="inReplyTo" id="inReplyTo" <%=inReplyTo != null ? "value=\"" + inReplyTo.Id + "\"" : "" %> />
        
        <textarea onkeyup="UpdateCharCount()" onkeydown="UpdateCharCount()" onkeypress="UpdateCharCount()" id="status" name="status" autocomplete="off"><%=inReplyTo != null ? "@" + inReplyTo.UserScreenName + " " : "" %></textarea>
        
        <button style="" id="update_button">update</button>
        <span id="char_count">140</span>
    
    <%} %>
    
    <% Html.RenderPartial("TweetsPresenter"); %>     
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Scripts">
    <script>
        var status = document.getElementById("status");
        var counter = document.getElementById("char_count");

        function UpdateCharCount() {
            counter.innerHTML = 140 - document.getElementById("status").value.length;
        }

        UpdateCharCount();
    </script>
    
    <% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("StatusTemplate"); %>
    
    <% var inReplyTo = ViewData["InReplyTo"] as Status; %>
    <% if(Request.QueryString["compose"] == "true" || inReplyTo != null){%>
        <script>$("#update_form").show(); $("#status").putCursorAtEnd();</script>
    <%} %>
    
    <%
       Context.Items["__Flushed"] = true;
        Response.Flush(); 
    %>
    
    <script type="text/javascript">
        var linkReplies = false;
        var initialData = <%=((Func<string>)ViewData["loadInitialData"])()%>;
        var url = '<%=ViewData["loadMoreUrl"] %>';
        var hideScreenName = false;
        var params = {};
        var moreMode = '<%=ViewData["moreMode"] ?? "since" %>';
        var pollForNewTweets = moreMode !== "page";
        var currentPage = 1;
        
        handleInitialTweets(initialData);
    </script>
</asp:Content>