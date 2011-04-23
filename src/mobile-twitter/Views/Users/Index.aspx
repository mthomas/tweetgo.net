<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="TweetSharp.Fluent"%>
<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>users<%=Settings.TitleSeperator %><%=((TwitterUser) ViewData["User"]).ScreenName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% var user = (TwitterUser) ViewData["user"]; %>
    <% var friendship = (TwitterFriendship) ViewData["friendship"]; %>
    <% var isYou = (bool) ViewData["isYou"]; %>
    
    <h2>
        <%=user.ScreenName %>
            
        <%if(!String.IsNullOrEmpty(user.Name)) {%><span>(<%=user.Name %>)</span><%}%>
    </h2>
    
    <div id="user">
        <img src="<%=user.ProfileImageUrl.Replace("_normal.", "_bigger.") %>" />
        
        <%if(!String.IsNullOrEmpty(user.Name)) {%><p><a target="_blank" href="<%=user.Url %>"><%=user.Url %></a></p><%}%>
        
        <%if(!String.IsNullOrEmpty(user.Location)) {%><p><%=user.Location%></p><%}%>
        
        <%if(!String.IsNullOrEmpty(user.Description)){%>
            <p id="bio"><%=user.Description %></p>
        <%}%>    
        
        <p>
            <%=user.FollowersCount %> followers - <%=user.FriendsCount %> following
        </p>
        
        <%if (!isYou) {%>
            <p id="are_following" <%=Html.DisplayStyleTag(friendship.Relationship.Target.FollowedBy) %>>you are following <%=user.ScreenName%> <button id="unfollow" data-screenName="<%=user.ScreenName %>">unfollow</button></p>
            <p id="not_following" <%=Html.DisplayStyleTag(!friendship.Relationship.Target.FollowedBy) %>><button id="follow" data-screenName="<%=user.ScreenName %>">follow</button></p>
        <%} else {%>
            <p id="is_you">this is you</p>
        <% }%>
    </div>
    
    <h2>timeline</h2>
    
    <p id="newTweets" onclick="showNewTweets()" style="display:none">there are new tweets</p>
    
    <ol id="t">
        <li id="loading_tweets">Loading Tweets...</li>
    </ol>
    
    <p id="loading_more" style="display: none">Loading More Tweets...</p>
    
    <button class="big_link_button" onclick="loadMoreTweets()" id="more" name="more">More</button>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <% var user = (TwitterUser) ViewData["User"]; %>

    <script>
        var linkReplies = true;
    </script>

    <% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("StatusTemplate"); %>
    <% 
        Context.Items["__Flushed"] = true;
        Response.Flush(); %>
    
    <script type="text/javascript">
        var initialData = <%=((Func<string>)ViewData["loadInitialData"])()%>;
        var url = '<%=ViewData["loadMoreUrl"] %>';
        var hideScreenName = true;
        var pollForNewTweets = false;
        var params = {"screenName" : '<%=user.ScreenName %>'};
        var moreMode = '<%=ViewData["moreMode"] ?? "since" %>';
        
        handleInitialTweets(initialData);
    </script>
    
    
</asp:Content>
