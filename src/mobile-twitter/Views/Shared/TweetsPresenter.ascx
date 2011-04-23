<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<p id="newTweets" onclick="showNewTweets()" style="display:none">click to view new tweets</p>
    
<ol id="t">
    <li id="loading_tweets">loading...</li>
</ol>

<p id="loading_more" style="display: none">loading...</p>

<button class="big_link_button" onclick="loadMoreTweets()" id="more" name="more">More</button>