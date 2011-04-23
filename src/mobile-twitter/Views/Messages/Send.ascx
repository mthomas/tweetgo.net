<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%using(Html.BeginForm("Send", "Messages", FormMethod.Post, new{id="message_form"})){%>
    <input type="text" name="toScreenName" id="toScreenName" />
    <textarea name="message" id="message"></textarea>
    <button type="submit">send</button>
<%} %>