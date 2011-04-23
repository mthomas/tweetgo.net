<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>privacy policy
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Privacy Policy</h2> 
    
    <p class="lead-in">We care about your privacy.  As a result we follow these principles, first articulated by <a href="http://automattic.com/privacy">Automattic</a>: </p> 
    
    <ul> 
        <li>We don&#8217;t ask you for personal information unless we truly need it. (We can&#8217;t stand services that ask you for things like your gender or income level for no apparent reason.)</li> 
        <li>We don&#8217;t share your personal information with anyone except to comply with the law, develop our products, or protect our rights.</li> 
        <li>We don&#8217;t store personal information on our servers unless required for the on-going operation of one of our services.</li> 
    </ul> 
    
    <p>	Below is our privacy policy which incorporates these goals:</p> 

    <p>(Note, this privacy policy based on Automattic's privacy policy that they released under <strong>Creative Commons Sharealike</strong> license.)</p>

    <p>	
        tweetgo.net (“<strong>tweetgo</strong>”) operates <a href="http://www.tweetgo.net/">tweetgo.net</a>. 
        It is tweetgo.net’s policy to respect your privacy regarding any information we may collect while operating our websites.
    </p> 
    
    <h3>Website Visitors</h3> 
    
    <p>	Like most website operators, tweetgo.net collects non-personally-identifying information of the sort that web browsers and servers typically make available, such as the browser type, language preference, referring site, and the date and time of each visitor request. tweetgo.net&#8217;s purpose in collecting non-personally identifying information is to better understand how tweetgo.net&#8217;s visitors use its website. From time to time, tweetgo.net may release non-personally-identifying information in the aggregate, e.g., by publishing a report on trends in the usage of its website.</p> 
    
    <p>	tweetgo.net also collects potentially personally-identifying information like Internet Protocol (IP) addresses. tweetgo.net does not use such information to identify its visitors, however, and does not disclose such information, other than under the same circumstances that it uses and discloses personally-identifying information, as described below.</p> 
    
    <h3>Gathering of Personally-Identifying Information</h3> 
    
    <p>	Certain visitors to tweetgo.net’s websites choose to interact with tweetgo.net in ways that require tweetgo.net to gather personally-identifying information. The amount and type of information that tweetgo.net gathers depends on the nature of the interaction. For example, we ask visitors who use <a href="http://www.tweetgo.net/">tweetgo.net</a> to authorize us to access your twitter.com account which gives us access to personal information you have provided to twitter.com. tweetgo.net collects such information only insofar as is necessary or appropriate to fulfill the purpose of the visitor’s interaction with tweetgo.net. tweetgo.net does not disclose personally-identifying information other than as described below. And visitors can always refuse to supply personally-identifying information, with the caveat that it may prevent them from engaging in certain website-related activities.</p> 
    
    <h3>Aggregated Statistics</h3> 
    
    <p>tweetgo.net may collect statistics about the behavior of visitors to its websites. For instance, tweetgo.net may monitor the most popular blogs on the WordPress.com site or use spam screened by the Akismet service to help identify spam. tweetgo.net may display this information publicly or provide it to others. However, tweetgo.net does not disclose personally-identifying information other than as described below.</p> 
    
    <h3>Protection of Certain Personally-Identifying Information</h3> 
    
    <p>tweetgo.net discloses potentially personally-identifying and personally-identifying information only to those of its employees, contractors and affiliated organizations that (i) need to know that information in order to process it on tweetgo.net’s behalf or to provide services available at tweetgo.net’s websites, and (ii) that have agreed not to disclose it to others. Some of those employees, contractors and affiliated organizations may be located outside of your home country; by using tweetgo.net’s websites, you consent to the transfer of such information to them. tweetgo.net will not rent or sell potentially personally-identifying and personally-identifying information to anyone. Other than to its employees, contractors and affiliated organizations, as described above, tweetgo.net discloses potentially personally-identifying and personally-identifying information only when required to do so by law, or when tweetgo.net believes in good faith that disclosure is reasonably necessary to protect the property or rights of tweetgo.net, third parties or the public at large. If you are a registered user of an tweetgo.net website and have supplied your email address, tweetgo.net may occasionally send you an email to tell you about new features, solicit your feedback, or just keep you up to date with what’s going on with tweetgo.net and our products. We primarily use our various product blogs to communicate this type of information, so we expect to keep this type of email to a minimum. If you send us a request (for example via a support email or via one of our feedback mechanisms), we reserve the right to publish it in order to help us clarify or respond to your request or to help us support other users. tweetgo.net takes all measures reasonably necessary to protect against the unauthorized access, use, alteration or destruction of potentially personally-identifying and personally-identifying information.</p> 
    
    <h3>Cookies</h3> 
    
    <p>A cookie is a string of information that a website stores on a visitor’s computer, and that the visitor’s browser provides to the website each time the visitor returns. tweetgo.net uses cookies to help tweetgo.net identify and track visitors, their usage of tweetgo.net website, and their website access preferences. tweetgo.net visitors who do not wish to have cookies placed on their computers should set their browsers to refuse cookies before using tweetgo.net’s websites, with the drawback that certain features of tweetgo.net’s websites may not function properly without the aid of cookies.</p> 
    
    <h3>Ads</h3> 
    
    <p>Ads appearing on any of our websites may be delivered to users by advertising partners, who may set cookies. These cookies allow the ad server to recognize your computer each time they send you an online advertisement to compile information about you or others who use your computer. This information allows ad networks to, among other things, deliver targeted advertisements that they believe will be of most interest to you. This Privacy Policy covers the use of cookies by tweetgo.net and does not cover the use of cookies by any advertisers.</p> 
    
    <h3>Privacy Policy Changes</h3> 
    
    <p>	Although most changes are likely to be minor, tweetgo.net may change its Privacy Policy from time to time, and in tweetgo.net’s sole discretion. tweetgo.net encourages visitors to frequently check this page for any changes to its Privacy Policy. If you have a wordpress.com account, you should also check your blog’s dashboard for alerts to these changes. Your continued use of this site after any change in this Privacy Policy will constitute your acceptance of such change.

    </p>

    <% Html.RenderPartial("LicenseAndThanks"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
