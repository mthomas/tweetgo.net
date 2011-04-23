<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TweetSharp.Twitter.Model.TwitterList>>" %>

<%@ Import Namespace="TweetSharp.Twitter.Model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>lists
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lists</h2>
    
    <ol id="lists">
    <% foreach(var list in Model){%>
        <li>   
            <% var url = Url.Action("Show", "Lists", new {listSlug = list.Slug, ownerScreenName = list.User.ScreenName}); %>
            <a href="<%=url %>" class="list_link">
                @<%=list.User.ScreenName%>/<%=list.Slug %>
            </a>
        </li>
    <%} %>
    </ol>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
