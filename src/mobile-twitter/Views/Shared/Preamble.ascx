<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    function setOrientation() {
        var idName;
        
        if (window.innerHeight > window.innerWidth) {
            idName = "p";
        }
        else {
            idName = "l";
        }

        document.getElementsByTagName("body")[0].id = idName;
    }

    setOrientation();
    
    window.onresize = setOrientation;
</script>

<div id="errors" style="display: none"></div>

<%=Html.RenderFlash("FlashError") %>
<%=Html.RenderFlash("FlashInformation") %>