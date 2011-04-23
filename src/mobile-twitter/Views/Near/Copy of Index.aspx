<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeoLocationViewData>" %>
<%@ Import Namespace="mobile_twitter.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>near
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>tweets near</h2>
    
    <div id="map_canvas" style="width: 800px; height: 600px"></div>
    <div id="tweets"></div>

</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>

<script type="text/javascript">

    var myLat;
    var myLng;

    var content = "";
    
    var loadFromTwitter = function(lat, lng, r) {
        var url = 'http://search.twitter.com/search.json?geocode='
			+ lat + '%2C'
			+ lng + ' %2C' + r
			+ 'km&callback=manage_response';

        var script = document.createElement('script');
        script.setAttribute('src', url);
        document.getElementsByTagName('head')[0].appendChild(script);
    };

    var getLocation = function() {
        navigator.geolocation.getCurrentPosition(function(pos) {

            myLat = pos.coords.latitude;
            myLng = pos.coords.longitude;

            loadFromTwitter(pos.coords.latitude, pos.coords.longitude, 3);
        }, function() {
            $("#denied").show();
        });
    };

    var map;
    var manage_response = function(tl) {
        var latlng = new google.maps.LatLng(myLat, myLng);

        var myOptions = {
            zoom: 14,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        var meInfo = new google.maps.InfoWindow({
            content: "Me!"
        });

        var me = new google.maps.Marker({
            position: latlng,
            map: map,
            title: "You!"
        });

        google.maps.event.addListener(me, 'click', function() { meInfo.open(map, me); });
        var results = tl.results;
        for (var i = 0; i < results.length; i++) {
            displayTweet(results[i]);
        }

        document.getElementById("tweets").innerHTML = content;
    };

    var formatMs = function(ms) {
        var s = ms / 1000;

        var m = s / 60;
        var h = m / 60;
        var d = h / 24;

        if (d > 1) return Math.round(d) + " days";
        if (h > 1) return Math.round(h) + " hours";
        if (m > 1) return Math.round(m) + " minutes";
        if (s > 1) return Math.round(s) + " seconds";

        return ms + "ms";
    };

    var formatDistance = function(location) {
        if (location.indexOf(",") === -1) return "unknown";

        if (location.indexOf(":") > 0) {
            location = location.split(":")[1].trim();
        }

        var lat = location.split(",")[0];
        var lng = location.split(",")[1];

        var miles = distance(lat, lng, myLat, myLng, 'M');

        if (miles < 1) return (Math.round(miles * 5280)) + " feet";

        return Math.round(miles * 10) / 10 + " miles";
    };

    var getLatLng = function(location) {
        if (location.indexOf(",") === -1) return null;

        if (location.indexOf(":") > 0) {
            location = location.split(":")[1].trim();
        }

        var lat = location.split(",")[0];
        var lng = location.split(",")[1];

        return new google.maps.LatLng(lat, lng);
    }

    var displayTweet = function(tweet) {
        var ago = formatMs(new Date() - new Date(tweet.created_at));
        var distance = formatDistance(tweet.location);
        var latLng = getLatLng(tweet.location);

        var text = '<p class="status_message">'
            + '<a target="_blank" href="http://www.twitter.com/' + tweet.from_user + '"><img src="' + tweet.profile_image_url + '" class="status_user_image" /></a>'
            + '<a class="status_user_name" target="_blank"  href="http://www.twitter.com/' + tweet.from_user + '">' + tweet.from_user + '</a> '
			+ formatText(tweet.text)
			+ '<span class="status_info">' + ago + ' ago | ' + distance + ' away</span>';

        var marker = new google.maps.Marker({
            position: latLng,
            map: map,
            title: tweet.from_user
        });

        var info = new google.maps.InfoWindow({
            content: text
        });
        
        google.maps.event.addListener(marker, 'click', function() {
          info.open(map,marker);
        });
        
        content += text;
    };

    function formatText(text) {
        return text.replace(/(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/gim, "<a target=\"_blank\" href=\"$1\">$1</a>");
    }

    function distance(lat1, lon1, lat2, lon2, unit) {
        var radlat1 = Math.PI * lat1 / 180;
        var radlat2 = Math.PI * lat2 / 180;
        var radlon1 = Math.PI * lon1 / 180;
        var radlon2 = Math.PI * lon2 / 180;
        var theta = lon1 - lon2;
        var radtheta = Math.PI * theta / 180;
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta); ;
        dist = Math.acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        if (unit == "K") { dist = dist * 1.609344 };
        if (unit == "N") { dist = dist * 0.8684 };
        return dist;
    }

    if (!navigator.geolocation) {
        $("#sad_message").show();
    }
    else {

        getLocation();
    }
    
</script>






</asp:Content>