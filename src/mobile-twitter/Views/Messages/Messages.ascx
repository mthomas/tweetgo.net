<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<ol id="t"></ol>

<p id="loading">Loading Messages</p>

<p id="no_more" style="display: none">No More Messages</p>

<button type="button" onclick="loadMoreMessages()" id="load_more" style="display:none">More</button>