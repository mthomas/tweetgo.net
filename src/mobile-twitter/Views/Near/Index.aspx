<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GeoLocationViewData>" %>
<%@ Import Namespace="mobile_twitter.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>near
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>tweets near</h2>
    
    <% Html.RenderPartial("TweetsPresenter"); %>

</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>