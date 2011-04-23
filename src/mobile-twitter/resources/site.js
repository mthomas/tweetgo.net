
$.ajaxSetup({type:"POST",dataType:"json"});$("#errors").bind("click",function(e){$("#errors").empty();$("#errors").hide();});$("#errors").bind("ajaxError",function(event,request,settings){$(this).show();$(this).append("<p>Something went wrong. Try refreshing the page.</p>");});$(".FlashInformation").effect("highlight",{},4000,function(){$(this).effect("fade",{},2000,function(){});});var currentlyOpen;function messageClicked(e){var params=StatusActionParams(this);var actions=params.actions;if(currentlyOpen&&currentlyOpen===this){$(currentlyOpen).removeClass("active");currentlyOpen=null;}
else{$(this).addClass("active");if(currentlyOpen){$(currentlyOpen).removeClass("active");currentlyOpen=null;}
currentlyOpen=this;}}
$(".co a").live("click",function(e){$("#update_form").toggle();$("#update_form textarea").focus();});$(".status").live("click",messageClicked);$(".fave",$("#t")[0]).live("click",function(e){var params=StatusActionParams(this);params.clicked.addClass("progress_button");if(params.statusMessage.hasClass("is_fave")){$.post(urls.unfavorite,{"statusId":params.statusId},function(){params.statusMessage.removeClass("is_fave");params.clicked.removeClass("progress_button");});}
else{$.post(urls.favorite,{"statusId":params.statusId},function(){params.statusMessage.addClass("is_fave");params.clicked.removeClass("progress_button");});}
return false;});$("#unfollow").live("click",function(e){var screenName=$(this).attr("data-screenName");$.post(urls.unfollow,{"screenName":screenName},function(){$("#not_following").show();$("#are_following").hide();});});$("#follow").live("click",function(e){var screenName=$(this).attr("data-screenName");$.post(urls.follow,{"screenName":screenName},function(){$("#not_following").hide();$("#are_following").show();});});$(".rply a",$("#t")[0]).live("click",function(e){var params=StatusActionParams(this);var screenName=params.clicked.attr("screenName");if(linkReplies){location.href=urls.home+"?replyTo="+params.statusId;}
else{$("#inReplyTo").val(params.statusId);$("#status").val("@"+screenName+" ");$("#update_form").show();window.setTimeout(function(){$("#status").putCursorAtEnd();},10);}});$(".view_reply",$("#t")[0]).live("click",function(e){var params=StatusActionParams(this);if(!params.clicked.attr('data-reply_loaded')===true){params.clicked.attr('data-reply_loaded',true);$.post(urls.getStatus,{"statusId":params.clicked.attr("data-reply_id")},function(response){params.clicked.parent().parent().append("<span class='reply_text'>"+response.results.Text+"</span>");});}
e.stopPropagation();return false;});function StatusActionParams(clickedElement){var clicked=$(clickedElement);var statusMessage=clicked.parents(".status");var statusId=statusMessage.attr("data-status_id");var actions=$(".ctrls",statusMessage);return{"statusId":statusId,"clicked":clicked,"statusMessage":statusMessage,"actions":actions};}
var highestId=0;var oldestId=0x7FFFFFFFFFFFFFFF;var loaded={};function showRateLimitingStatus(status){if(status!=null&&status.RemainingHits){$("#rateLimitingStatus").html(status.RemainingHits+"/"+status.HourlyLimit+" | resets in "+status.ResetsInMinutes+" minutes");}}
function loadTweets(newerThan,olderThan,handleData){if(moreMode==="page"){params["page"]=currentPage+1;currentPage=currentPage+1;}
else{params["newerThan"]=newerThan;params["olderThan"]=olderThan;}
$.post(url,params,function(data){showRateLimitingStatus(data.rateLimitingStatus);handleData(data.results);});}
var hasLoadedOnce=false;function renderTweets(data,prepend){var length=data.length;if(length>0&&hasLoadedOnce){if(prepend===true){$("#t").prepend("<li class='divider'></li>");}
else{$("#t").append("<li class='divider'></li>");}}
for(var i=0;i<length;i++){var tweet=data[i];if(loaded[tweet.Id]!==true){var html=tmpl("statusTemplate",{"hideScreenName":hideScreenName,"status":tweet});if(prepend===true){$("#t").prepend(html);}
else{$("#t").append(html);}
oldestId=Math.min(oldestId,tweet.Id);highestId=Math.max(highestId,tweet.Id);loaded[tweet.Id]=true;}}
hasLoadedOnce=true;}
function handleInitialTweets(data){$("#loading_tweets").hide();renderTweets(data);pollForTweets();}
var bufferOfNewTweets=[];function handleNewTweets(data){data=data.reverse();var length=data.length;for(var i=0;i<length;i++){highestId=Math.max(highestId,data[i].Id);bufferOfNewTweets.push(data[i]);$("#newTweets").show();}
pollForTweets();}
function showNewTweets(){renderTweets(bufferOfNewTweets,true);$("#newTweets").hide();}
function pollForTweets(){if(pollForNewTweets){setTimeout(function(){loadTweets(highestId,null,handleNewTweets)},1000*60);}}
function loadMoreTweets(){$("#loading_more").show();loadTweets(null,oldestId,function(data){$("#loading_more").hide();renderTweets(data);});return false;}
function renderMessages(data){var length=data.length;if(length>0&&hasLoadedOnce){$("#t").append("<li class='divider'></li>");}
for(var i=0;i<length;i++){var tweet=data[i];if(loaded[tweet.Id]!==true){var html=tmpl("messageTemplate",{"status":tweet});$("#t").append(html);loaded[tweet.Id]=true;}}
hasLoadedOnce=true;}
function loadMoreMessages(){$("#loading").show();$.post(loadMoreMessagesUrl,{"page":nextPage},function(data){showRateLimitingStatus(data.rateLimitingStatus);if(data.results.length==0){$("#no_more").show();$("#load_more").hide();}
else{$("#load_more").show();}
renderMessages(data.results);$("#loading").hide();});nextPage++;}
$(".message_reply_button").live("click",function(e){var params=StatusActionParams(this);var screenName=params.clicked.attr("screenName");$("#toScreenName").val(screenName);$("#message").focus();});(function($){jQuery.fn.putCursorAtEnd=function(){return this.each(function(){$(this).focus();if(this.setSelectionRange){var len=$(this).val().length*2;this.setSelectionRange(len,len);}
else{$(this).val($(this).val());}
this.scrollTop=999999;});};})(jQuery);
$(".rtwt a",$("#t")[0]).live("click",function(e){var params=StatusActionParams(this);$("<div></div>").html("<p>retweet this status?</p>").dialog({"resizable":false,"height":320,"width":"80%","modal":true,"buttons":{"retweet":function(){NewStyleRetweet(params);$(this).dialog("close");},"add note":function(){OldStyleRetweet(params);$(this).dialog("close");},"cancel":function(){$(this).dialog("close");}}});return false;});function OldStyleRetweet(params){if(onHomeController){var clicked=params.clicked;var username=clicked.parents(".status").find("strong").text();var body=clicked.parents(".status").find("p").text();$("#update_form").show();$("#status").val("RT @"+username+": "+body);window.setTimeout(function(){$("#status").putCursorAtEnd();},10);}
else{location.href="/?retweetId="+params.statusId;}}
function NewStyleRetweet(params){params.clicked.addClass("progress_button");$.post(urls.retweet,{"statusId":params.statusId},function(){params.clicked.removeClass("progress_button");alert("Status retweeted");});}
(function(){var cache={};this.tmpl=function tmpl(str,data){var fn=!/\W/.test(str)?cache[str]=cache[str]||tmpl(document.getElementById(str).innerHTML):new Function("obj","var p=[],print=function(){p.push.apply(p,arguments);};"+"with(obj){p.push('"+
str.replace(/[\r\t\n]/g," ").replace(/'(?=[^#]*#>)/g,"\t").split("'").join("\\'").split("\t").join("'").replace(/<#=(.+?)#>/g,"',$1,'").split("<#").join("');").split("#>").join("p.push('")
+"');}return p.join('');");return data?fn(data):fn;};})();