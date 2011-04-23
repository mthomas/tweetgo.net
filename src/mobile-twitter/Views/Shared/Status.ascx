<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TwitterStatus>" %>

<%@ Import Namespace="mobile_twitter"%>
<%@ Import Namespace="Dimebrain.TweetSharp.Model"%>

<% var status = Model; %>
<% var hideScreenName = (bool) (ViewData["HideScreenName"] ?? false); %>

<p class="status_message <%=status.IsFavorited  ? "favorite" : ""%> <%=hideScreenName ? "no_screenName" : "" %>" status_id="<%=status.Id %>" id="status_<%=status.Id %>">
    <%if(!hideScreenName){%>
        <a target="_blank" href="http://www.twitter.com/<%=status.User.ScreenName %>"><img src="<%=status.User.ProfileImageUrl %>" class="status_user_image" /></a>
        
        <%=Html.ActionLink(status.User.ScreenName, "User", "Home", new { screenName = status.User.ScreenName, page = 1 }, new { @class = "status_user_name"  })%>
    <%} %>
    
    <%=StatusFormattingHelper.FormatLinksAndMentions(status.Text) %>
    
    <span class="status_info">
        <%=status.CreatedDate.ToPrettyDate() %> ago from <%=status.Source %>
        <% if(status.InReplyToStatusId != 0){%>
            <a class="view_reply" status_id="<%=status.InReplyToStatusId %>">in reply to <%=status.InReplyToScreenName %></a>
        <%} %>    
    </span>           
</p>

<p style="display: none" id="actions_<%=status.Id %>">
    <a id="favorite_button_<%=status.Id %>" class="big_link_button favorite_button" status_id="<%=status.Id %>" style="display: <%=status.IsFavorited ? "none" : "block" %>">Favorite</a>
    <a id="unfavorite_button_<%=status.Id %>" class="big_link_button unfavorite_button" status_id="<%=status.Id %>" style="display: <%=!status.IsFavorited ? "none" : "block" %>" >Un-Favorite</a>
    <a class="big_link_button retweet_button" status_id="<%=status.Id %>">Retweet</a>
    <a class="big_link_button reply_button"  status_id="<%=status.Id %>" screenName="<%=status.User.ScreenName %>">Reply</a>
</p>