<%="<script type='text/html' id='statusTemplate'>"%>
<li class="status <#=status.IsFavorited  ? "is_fave" : ""#> <#=hideScreenName ? "no_profileImage" : "" #>" data-status_id="<#=status.Id #>">    
    <#if(!hideScreenName) {#>
        <span class="img">
            <a href="/user/<#=status.UserScreenName #>"><img src="<#=status.UserProfileImageUrlBigger #>" /></a>
        </span>
    <#} #>
    
    <span class="twt">
        <#if(!hideScreenName) {#>
            <strong><a href="/user/<#=status.UserScreenName#>"><#=status.UserScreenName#></a></strong>
        <#} #> <p><#=status.Text #></p>
        
        <span class="meta">
            <span class="fave_star">&#x2605;</span>
	        <em><#=status.CreatedDate #></em> ago
            <# if(status.InReplyToStatusId !== null) { #>
                <span class="view_reply" data-reply_id="<#=status.InReplyToStatusId#>">in reply to <#=status.InReplyToScreenName #>
                    
                </span><!--<a href="/conversation/<#=status.Id#>">(follow)</a>-->
            <#} #>    
	    </span>
    	
	    <span class="ctrls">
		    <ul>
			    <li class="rply ctrl"><a href="#" class="ctrl" screenName="<#=status.UserScreenName #>">reply</a></li>
			    <li class="fave ctrl"><a href="#" class="ctrl">favorite</a></li>
			    <li class="rtwt ctrl"><a href="#" class="ctrl">retweet</a></li>
		    </ul>
	    </span>
    </span>
</li>
<%="</script>"%>