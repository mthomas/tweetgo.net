$.ajaxSetup({
    type: "POST",
    dataType: "json"
});

$("#errors").bind("click", function(e) {
    $("#errors").empty();
    $("#errors").hide();
});

$("#errors").bind("ajaxError", function(event, request, settings) {
    $(this).show();
    $(this).append("<p>Something went wrong. Try refreshing the page.</p>");
});


$(".FlashInformation").effect("highlight", {}, 4000, function() {
    $(this).effect("fade", {}, 2000, function() { });
});

var currentlyOpen;

function messageClicked(e) {
    var params = StatusActionParams(this);
    var actions = params.actions;

	if (currentlyOpen && currentlyOpen === this) {
	    $(currentlyOpen).removeClass("active");
	    currentlyOpen = null;
	}
	else {
	    $(this).addClass("active");

	    if (currentlyOpen) {
	        $(currentlyOpen).removeClass("active");
	        currentlyOpen = null;
	    }

	    currentlyOpen = this;
	}
}

$(".co a").live("click", function(e) {
    $("#update_form").toggle();
    $("#update_form textarea").focus();
});

$(".status").live("click", messageClicked);

$(".fave", $("#t")[0]).live("click", function(e) {
    var params = StatusActionParams(this);

    params.clicked.addClass("progress_button");

    if (params.statusMessage.hasClass("is_fave")) {
        $.post(urls.unfavorite, { "statusId": params.statusId }, function() {
            params.statusMessage.removeClass("is_fave");
            params.clicked.removeClass("progress_button");
        });

    }
    else {
        $.post(urls.favorite, { "statusId": params.statusId }, function() {
            params.statusMessage.addClass("is_fave");
            params.clicked.removeClass("progress_button");
        });
    }

    return false;
});

$("#unfollow").live("click", function(e) {
    var screenName = $(this).attr("data-screenName");

    $.post(urls.unfollow, { "screenName": screenName }, function() {
        $("#not_following").show();
        $("#are_following").hide();
    });

});


$("#follow").live("click", function(e) {
    var screenName = $(this).attr("data-screenName");

    $.post(urls.follow, { "screenName": screenName }, function() {
        $("#not_following").hide();
        $("#are_following").show();
    });

});
    

$(".rply a", $("#t")[0]).live("click", function(e) {
    var params = StatusActionParams(this);
    var screenName = params.clicked.attr("screenName");

    if (linkReplies) {
        location.href = urls.home + "?replyTo=" + params.statusId;
    }
    else {
        $("#inReplyTo").val(params.statusId);
        $("#status").val("@" + screenName + " ");

        $("#update_form").show();

        window.setTimeout(function() {
            $("#status").putCursorAtEnd();
            
        }, 10);
    }
});

$(".view_reply", $("#t")[0]).live("click", function(e) {
    var params = StatusActionParams(this);

    if (!params.clicked.attr('data-reply_loaded') === true) {
        params.clicked.attr('data-reply_loaded', true);

        $.post(urls.getStatus, { "statusId": params.clicked.attr("data-reply_id")}, function(response) {
            params.clicked.parent().parent().append("<span class='reply_text'>" + response.results.Text + "</span>");
        });
    }

    e.stopPropagation();

    return false;
});

function StatusActionParams(clickedElement){
    var clicked = $(clickedElement);
    var statusMessage  = clicked.parents(".status");
    
    var statusId = statusMessage.attr("data-status_id");
    var actions = $(".ctrls", statusMessage );
    
    return {
        "statusId":statusId,
        "clicked":clicked,
        "statusMessage":statusMessage,
        "actions":actions
    };
}



/* loading tweets via ajax */

var highestId = 0;
var oldestId = 0x7FFFFFFFFFFFFFFF; //MaxValue of a long
var loaded = {};

function showRateLimitingStatus(status) {
    if (status != null && status.RemainingHits) {
        $("#rateLimitingStatus").html(status.RemainingHits + "/" + status.HourlyLimit + " | resets in " + status.ResetsInMinutes + " minutes");
    }
}

function loadTweets(newerThan, olderThan, handleData) {
    if (moreMode === "page") {
        params["page"] = currentPage + 1;
        currentPage = currentPage + 1;
    }
    else {
        params["newerThan"] = newerThan;
        params["olderThan"] = olderThan;
    }
    
    $.post(url, params, function(data) {
        showRateLimitingStatus(data.rateLimitingStatus);
        handleData(data.results);
    });
}

var hasLoadedOnce = false;
function renderTweets(data, prepend) {

    var length = data.length;

    if (length > 0 && hasLoadedOnce) {
        if (prepend === true) {
            $("#t").prepend("<li class='divider'></li>");
        }
        else {
            $("#t").append("<li class='divider'></li>");
        }
    }

    for (var i = 0; i < length; i++) {

        var tweet = data[i];
        if (loaded[tweet.Id] !== true) {
            var html = tmpl("statusTemplate", { "hideScreenName": hideScreenName, "status": tweet });

            if (prepend === true) {
                $("#t").prepend(html);
            }
            else {
                $("#t").append(html);
            }

            oldestId = Math.min(oldestId, tweet.Id);
            highestId = Math.max(highestId, tweet.Id);

            loaded[tweet.Id] = true;
        }
    }

    hasLoadedOnce = true;
}

function handleInitialTweets(data) {
    $("#loading_tweets").hide();

    renderTweets(data);

    pollForTweets();
}

var bufferOfNewTweets = [];
function handleNewTweets(data) {
    data = data.reverse();
    var length = data.length;
    for (var i = 0; i < length; i++) {
        highestId = Math.max(highestId, data[i].Id);
        bufferOfNewTweets.push(data[i]);
        $("#newTweets").show();
    }

    pollForTweets();
}

function showNewTweets() {

    renderTweets(bufferOfNewTweets, true);

    $("#newTweets").hide();
}

function pollForTweets() {
    if (pollForNewTweets) {
        setTimeout(function() { loadTweets(highestId, null, handleNewTweets) }, 1000 * 60);
    }
}

function loadMoreTweets() {
    $("#loading_more").show();
    loadTweets(null, oldestId, function(data) {
        $("#loading_more").hide();
        renderTweets(data);
    });
    return false;
}



/* MESSAAGES */
function renderMessages(data) {
	var length = data.length;

	if (length > 0 && hasLoadedOnce) {
	    $("#t").append("<li class='divider'></li>");
	}

	for (var i = 0; i < length; i++) {

		var tweet = data[i];
		if (loaded[tweet.Id] !== true) {
			var html = tmpl("messageTemplate", { "status": tweet });

			$("#t").append(html);

			loaded[tweet.Id] = true;
		}
    }

    hasLoadedOnce = true;
}

function loadMoreMessages() {
	$("#loading").show();

	$.post(loadMoreMessagesUrl, { "page": nextPage }, function(data) {
		showRateLimitingStatus(data.rateLimitingStatus);
		if (data.results.length == 0) {
			$("#no_more").show();
			$("#load_more").hide();
		}
		else {
			$("#load_more").show();
		}
		renderMessages(data.results);
		$("#loading").hide();
	});

	nextPage++;
}

$(".message_reply_button").live("click", function(e) {
    var params = StatusActionParams(this);
    var screenName = params.clicked.attr("screenName");

    $("#toScreenName").val(screenName);

    $("#message").focus();
});

// jQuery plugin: PutCursorAtEnd 1.0
// http://plugins.jquery.com/project/PutCursorAtEnd
// by teedyay
//
// Puts the cursor at the end of a textbox/ textarea

// codesnippet: 691e18b1-f4f9-41b4-8fe8-bc8ee51b48d4
(function($) {
    jQuery.fn.putCursorAtEnd = function() {
        return this.each(function() {
            $(this).focus();

            // If this function exists...
            if (this.setSelectionRange) {
                // ... then use it
                // (Doesn't work in IE)

                // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
                var len = $(this).val().length * 2;
                this.setSelectionRange(len, len);
            }
            else {
                // ... otherwise replace the contents with itself
                // (Doesn't work in Google Chrome)
                $(this).val($(this).val());
            }

            // Scroll to the bottom, in case we're in a tall textarea
            // (Necessary for Firefox and Google Chrome)
            this.scrollTop = 999999;
        });
    };
})(jQuery);