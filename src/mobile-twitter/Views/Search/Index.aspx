<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TwitterSearchResult>" %>

<%@ Import Namespace="TweetSharp.Twitter.Model"%>
<%@ Import Namespace="mobile_twitter"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Search</h2>    
    
    <p>
        <input type="text" name="q" id="q" value="<%=Html.Encode((string)ViewData["q"])%>" autofocus="true" />
        <button type="button" id="search">Search</button>
    </p>
    
    <h2 id="saved_searches_header">Saved Searches</h2>
    <ul id="saved_searches">
        <%foreach (var search in (IEnumerable<TwitterSavedSearch>)ViewData["SavedSearches"]) {%>
            <li><a class="saved_search_link list_link" data-tweetgo-q="<%=search.Query %>" href="#"><%=search.Query %></a></li>
        <%} %>
    </ul>
    
    <p id="searching" style="display: none">Searching...</p>

    <ol id="t"></ol>
    
    <p id="no_results" style="display: none">Sorry, no results for your search. </p>
    
    <p id="loading_more"  style="display: none">Loading More Results</p>
    
    <button class="big_link_button" id="more" style="display: none">More</button>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script>
        var linkReplies = true;
        var hideScreenName = false;
    </script>

    <% Html.RenderPartial("JavaScriptActions"); %>
    <% Html.RenderPartial("StatusTemplate"); %>
    
    <script type="text/javascript">
        $("#q").focus();

        var lastResults;

        function search(q, paging) {
            $("#saved_searches").hide();
            $("#saved_searches_header").hide();
            $("#no_results").hide();
            $("#searching").show();

            var params = {};
            if (q !== null) {
                params["q"] = q;
                $("#t").empty();
                loaded = {};
                $("#q").val(q);
            }

            if (paging == null) {
                $("#more").hide();
            }

            $.post("/search/results/" + (paging || ""), params, function(data) {
                $("#searching").hide();
                if (data.results.Statuses.length === 0) {
                    $("#no_resuts").hide();
                    lastResults = null;
                }

                $("#more").removeClass("progress_button");
                $("#loading_more").hide();
                
                if (data.results.NextPage == null || data.results.NextPage == "") {
                    $("#more").hide();
                }
                else {
                    $("#more").show();
                }

                lastResults = data.results;
                renderTweets(data.results.Statuses);
            });
        }

        $("#q").bind("keypress", function(e) {
            if (e.keyCode == 13 || e.keyCode == 10) {
                search($("#q").val(), null);
                return false;
            }
        });
    
        $("#search").bind("click", function() {
            search($("#q").val(), null);
        });

        $("#more").bind("click", function() {
            $("#more").addClass("progress_button");
            $("#loading_more").show();
            search(null, lastResults.NextPage);
        });

        $(".saved_search_link").bind("click", function() {
            var q = $(this).attr("data-tweetgo-q");
            search(q, null);
            return false;
        });

        if ($("#q").val() != "") {
            search($("#q").val(), null);
        }
    </script>
</asp:Content>