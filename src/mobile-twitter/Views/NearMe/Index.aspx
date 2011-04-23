<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>near me
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>near me</h2>
    
    <div id="waiting_for_location">
        <p>You need to grant tweetgo permission to see where you are.</p>
        <p>You may need to click a button that says something like "share your location with www.tweetgo.net".</p>
    </div>
    
    <div id="location_unsupported" style="display: none">
        <p>It looks like your browser doesn't support geolocation.  Without this support, we can't find tweets near you.</p>
    </div>
        
    
    <div id="location_denied" style="display: none">
        <p>You denied tweetgo access to your location.  In order to see tweets near you, we need to know were you are!</p>
        <p>Refresh this page to try again.</p>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
<script type="text/javascript">
    function handleLocation(lat, lng){
        location.href = "/search/?q=near:" + lat + "," + lng + " within:1mi";           
    }

    function getLocation() {
        if (!navigator.geolocation) {
            $("#location_unsupported").show();
            return;
        }

        navigator.geolocation.getCurrentPosition(function(pos) {

            myLat = pos.coords.latitude;
            myLng = pos.coords.longitude;

            handleLocation(myLat, myLng);
        }, function() {
            $("#waiting_for_location").hide();
            $("#location_denied").show();
        });
    };

    getLocation();
    
</script>
</asp:Content>