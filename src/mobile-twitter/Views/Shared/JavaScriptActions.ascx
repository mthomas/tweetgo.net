<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Bundler.Framework"%>

<script type="text/javascript">
var urls = {
    favorite : "<%=Url.Action("Favorite", "Home") %>",
    unfavorite : "<%=Url.Action("Unfavorite", "Home") %>",
    retweet :  "<%=Url.Action("Retweet", "Home") %>",
    home : "<%=Url.Action("Index", "Home") %>",
    getStatus : "<%=Url.Action("GetStatus", "Home") %>",
    follow : "/users/follow/",
    unfollow : "/users/unfollow/"
};

var onHomeController = <%=ViewContext.RouteData.GetRequiredString("Controller").Equals("Home", StringComparison.OrdinalIgnoreCase) ? "true" : "false" %>;

</script>

<%= Bundle.JavaScript()
    .Add("~/resources/actions.js")
    .Add("~/resources/retweet.js")
    .Add("~/resources/tmpl.js")
    .Render("~/resources/site.js") %>