<%="<script type='text/html' id='messageTemplate'>"%>
    <li class="status" data-status_id="<#=status.Id #>">
        <span class="img">
            <a href="/user/<#=status.ScreenName #>"><img src="<#=status.ProfileImageUrlNormal #>" class="status_user_image" /></a>
        </span>
        
        <span class="twt">
            <strong><a href="/user/<#=status.ScreenName#>"><#=status.ScreenName#></a></strong> <p><#=status.Text #></p>
            
            <span class="meta">
                <em><#=status.CreatedDate #></em>
            </span>
            
            <span class="ctrls">
                <ul>
                    <li class="rply ctrl"><a screenName="<#=status.ScreenName #>" class="ctrl">Reply</a></li>
                </ul>
            </span>
        </span>
    </li>
<%="</script>"%>