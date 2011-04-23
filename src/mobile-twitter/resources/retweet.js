$(".rtwt a", $("#t")[0]).live("click", function(e) {

    var params = StatusActionParams(this);

    $("<div></div>")
        .html("<p>retweet this status?</p>")
        .dialog({
            "resizable": false,
            "height": 320,
            "width": "80%",
            "modal": true,
            "buttons": {
                "retweet": function() { NewStyleRetweet(params); $(this).dialog("close"); },
                "add note": function() { OldStyleRetweet(params); $(this).dialog("close"); },
                "cancel": function() { $(this).dialog("close"); }
            }
        });

    return false;
});

function OldStyleRetweet(params) {
    if (onHomeController) {
        var clicked = params.clicked;
        
        var username = clicked.parents(".status").find("strong").text();
        var body = clicked.parents(".status").find("p").text();
        
        $("#update_form").show();
        $("#status").val("RT @" + username + ": " + body);
        window.setTimeout(function() {
            $("#status").putCursorAtEnd();
        }, 10);
    }
    else {
        location.href = "/?retweetId=" + params.statusId;
    }
}

function NewStyleRetweet(params) {

    params.clicked.addClass("progress_button");

    $.post(urls.retweet, { "statusId": params.statusId }, function() {
        params.clicked.removeClass("progress_button");
        alert("Status retweeted");
    });
}