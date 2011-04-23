<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Settings.TitleAndSeperator %>not logged in</title>
</head>
<body>
    <div style="width: 80%; margin: -100px 0 0 -40% ; left: 50%; top: 50%; position: absolute; font-family: Helvetica, Arial, Droid Sans, Sans-Serif; font-size: 18pt;">
        You did not grant tweetgo.net access to your Twitter account.  It's ok if you don't want to use tweetgo.net, but if you <b>do</b> want to use tweetgo.net <%=Html.ActionLink("click here and try again", "Index", "Home") %>.
    </div>
</body>
</html>
