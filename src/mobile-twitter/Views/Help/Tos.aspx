<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Simple.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Settings.TitleAndSeperator %>terms of service
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>tweetgo.net</h1>
    
    <h2>Terms of Service</h2>

    <div class="tos">
        
    <h3>The gist:</h3>
    
    <p>We (the <a href="http://www.tweetgo/help/about/">folks at
    tweetgo.net</a>) run a mobile twitter interface called 
    <a href="http://www.tweetgo.net">tweetgo.net</a> and would
    <strong>love</strong> for you to use it. Our basic service is free,
    and we may offer paid upgrades for advanced features. Our service is 
    designed to give you as much access to twitter functionality as
    possible. However, be responsible in how you use this service. 
    In particular, make sure that do nto use this serice to distribute any
    of the items listed below.</p>
    
    <p>
        (Note, this privacy policy based on Automattic's privacy policy 
        that they released under <strong>Creative Commons Sharealike</strong> license.)
    </p>
    
    <h3>Terms of Service:</h3>
    
    <p>The following terms and conditions govern all use of the
    tweetgo.net website (the Website). The Website is owned and operated by
    tweetgo.net (&ldquo;tweetgo.net&rdquo;). The Website is offered
    subject to your acceptance without modification of all of the terms
    and conditions contained herein and all other operating rules,
    policies (including, without limitation, <a href=
    "http://tweetgo.net.com/help/privacy/">tweetgo.net&rsquo;s Privacy
    Policy</a>) and procedures that may be published from time to time
    on this Site by tweetgo.net (collectively, the
    &ldquo;Agreement&rdquo;).</p>
    
    <p>Please read this Agreement carefully before accessing or using
    the Website. By accessing or using any part of the web site, you
    agree to become bound by the terms and conditions of this
    agreement. If you do not agree to all the terms and conditions of
    this agreement, then you may not access the Website or use any
    services. If these terms and conditions are considered an offer by
    tweetgo.net, acceptance is expressly limited to these terms. The
    Website is available only to individuals who are at least 13 years
    old.</p>
    
    <ol>
        <li><strong>Your tweetgo.net Account.</strong> If you
        create an account on the Website, you are responsible for maintaining
        the security of your account, and you are fully
        responsible for all activities that occur under the account and any
        other actions taken in connection with the account. You
        must immediately notify tweetgo.net of any unauthorized uses of your
        account or any other breaches of security. tweetgo.net
        will not be liable for any acts or omissions by You, including any
        damages of any kind incurred as a result of such acts or
        omissions.</li>
        
        <li><strong>Responsibility of Contributors.</strong> If you post 
        material to the Website, post links
        on the Website, or otherwise make (or allow any third party to
        make) material available by means of the Website (any such
        material, &ldquo;Content&rdquo;), You are entirely responsible for
        the content of, and any harm resulting from, that Content. That is
        the case regardless of whether the Content in question constitutes
        text, graphics, an audio file, or computer software. By making
        Content available, you represent and warrant that:
        
        <ul>
            <li>the downloading, copying and use of the Content will not
            infringe the proprietary rights, including but not limited to the
            copyright, patent, trademark or trade secret rights, of any third
            party;</li>
            <li>if your employer has rights to intellectual property you
            create, you have either (i) received permission from your employer
            to post or make available the Content, including but not limited to
            any software, or (ii) secured from your employer a waiver as to all
            rights in or to the Content;</li>
            <li>you have fully complied with any third-party licenses relating
            to the Content, and have done all things necessary to successfully
            pass through to end users any required terms;</li>
            <li>the Content does not contain or install any viruses, worms,
            malware, Trojan horses or other harmful or destructive
            content;</li>
            <li class="important">the Content is not spam, is not machine- or
            randomly-generated, and does not contain unethical or unwanted
            commercial content designed to drive traffic to third party sites
            or boost the search engine rankings of third party sites, or to
            further unlawful acts (such as phishing) or mislead recipients as
            to the source of the material (such as spoofing); and</li>
            <li>the Content is not pornographic, libelous or defamatory
            (<a href="http://www.eff.org/bloggers/lg/faq-defamation.php">more
            info on what that means</a>), does not contain threats or incite
            violence towards individuals or entities, and does not violate the
            privacy or publicity rights of any third party.</li>
        </ul>
    </li>
    
    <%// note - think about this if we start offering a pro version %>
    <% if(false){%>
    <li><strong>Fees and Payment.</strong> Optional premium paid
    services such as extra storage, domain purchases or VIP hosting are
    available on the Website. By selecting a premium service you agree
    to pay tweetgo.net the monthly or annual subscription fees indicated
    for that service (the payment terms for VIP hosting are described
    below). Payments will be charged on the day you sign up for a
    premium service and will cover the use of that service for a
    monthly or annual period as indicated. Premium service fees are not
    refundable.</li>
    
    
    <li>
        <strong>VIP Services.</strong>
        <ul>
            <li><strong>Fees; Payment.</strong> By signing up for a VIP
            Services account you agree to pay tweetgo.net the setup fees and
            monthly hosting fees indicated at <a href=
            "http://tweetgo.net/vip-hosting/">http://tweetgo.net/vip-hosting/</a>
            in exchange for the services listed at <a href=
            "http://tweetgo.net/vip-hosting/">http://tweetgo.net/vip-hosting/</a>.
            Applicable fees will be invoiced starting from the day your VIP
            Services are established and in advance of using such services.
            tweetgo.net reserves the right to change the payment terms and fees
            upon thirty (30) days prior written notice to you. VIP Services can
            be canceled by you at anytime on 30 days written notice to
            tweetgo.net.</li>
            
            <li><strong>Support.</strong> VIP Services include access to
            priority email support. &ldquo;Email support&rdquo; means the
            ability to make requests for technical support assistance by email
            at any time (with reasonable efforts by tweetgo.net to respond
            within one business day) concerning the use of the VIP Services.
            &ldquo;Priority&rdquo; means that support for VIP Services
            customers takes priority over support for users of the standard,
            free tweetgo.net blogging services. All VIP Services support will
            be provided in accordance with tweetgo.net standard VIP Services
            practices, procedures and policies.</li>
        </ul>
    </li>
    <%} %>
    
    <li><strong>Responsibility of Website Visitors.</strong> tweetgo.net
    has not reviewed, and cannot review, all of the material, including
    computer software, posted to the Website, and cannot therefore be
    responsible for that material&rsquo;s content, use or effects. By
    operating the Website, tweetgo.net does not represent or imply that
    it endorses the material there posted, or that it believes such
    material to be accurate, useful or non-harmful. You are responsible
    for taking precautions as necessary to protect yourself and your
    computer systems from viruses, worms, Trojan horses, and other
    harmful or destructive content. The Website may contain content
    that is offensive, indecent, or otherwise objectionable, as well as
    content containing technical inaccuracies, typographical mistakes,
    and other errors. The Website may also contain material that
    violates the privacy or publicity rights, or infringes the
    intellectual property and other proprietary rights, of third
    parties, or the downloading, copying or use of which is subject to
    additional terms and conditions, stated or unstated. tweetgo.net
    disclaims any responsibility for any harm resulting from the use by
    visitors of the Website, or from any downloading by those visitors
    of content there posted.</li>
    
    <li><strong>Content Posted on Other Websites.</strong> We have not
    reviewed, and cannot review, all of the material, including
    computer software, made available through the websites and webpages
    to which tweetgo.net links, and that link to tweetgo.net.
    tweetgo.net does not have any control over those non-WordPress
    websites and webpages, and is not responsible for their contents or
    their use. By linking to a non-WordPress website or webpage,
    tweetgo.net does not represent or imply that it endorses such
    website or webpage. You are responsible for taking precautions as
    necessary to protect yourself and your computer systems from
    viruses, worms, Trojan horses, and other harmful or destructive
    content. tweetgo.net disclaims any responsibility for any harm
    resulting from your use of non-tweetgo.net websites and
    webpages.</li>
    
    <li><strong>Copyright Infringement and DMCA Policy.</strong> As
    tweetgo.net asks others to respect its intellectual property rights,
    it respects the intellectual property rights of others. If you
    believe that material located on or linked to by tweetgo.net
    violates your copyright, you are encouraged to notify tweetgo.net in
    accordance with <a href=
    "http://tweetgo.net.com/dmca/">tweetgo.net&rsquo;s Digital Millennium
    Copyright Act (&ldquo;DMCA&rdquo;) Policy</a>. tweetgo.net will
    respond to all such notices, including as required or appropriate
    by removing the infringing material or disabling all links to the
    infringing material. In the case of a visitor who may infringe or
    repeatedly infringes the copyrights or other intellectual property
    rights of tweetgo.net or others, tweetgo.net may, in its discretion,
    terminate or deny access to and use of the Website. In the case of
    such termination, tweetgo.net will have no obligation to provide a
    refund of any amounts previously paid to tweetgo.net.</li>
    
    <li><strong>Intellectual Property.</strong> This Agreement does not
    transfer from tweetgo.net to you any tweetgo.net or third party
    intellectual property, and all right, title and interest in and to
    such property will remain (as between the parties) solely with
    tweetgo.net. tweetgo.net, WordPress, tweetgo.net, the tweetgo.net
    logo, and all other trademarks, service marks, graphics and logos
    used in connection with tweetgo.net, or the Website are
    trademarks or registered trademarks of tweetgo.net or
    tweetgo.net&rsquo;s licensors. Other trademarks, service marks,
    graphics and logos used in connection with the Website may be the
    trademarks of other third parties. Your use of the Website grants
    you no right or license to reproduce or otherwise use any
    tweetgo.net or third-party trademarks.</li>
    
    <li><strong>Changes.</strong> tweetgo.net reserves the right, at its
    sole discretion, to modify or replace any part of this Agreement.
    It is your responsibility to check this Agreement periodically for
    changes. Your continued use of or access to the Website following
    the posting of any changes to this Agreement constitutes acceptance
    of those changes. tweetgo.net may also, in the future, offer new
    services and/or features through the Website (including, the
    release of new tools and resources). Such new features and/or
    services shall be subject to the terms and conditions of this
    Agreement. <strong><br /></strong></li>
    
    <li><strong>Termination.</strong> tweetgo.net may terminate your
    access to all or any part of the Website at any time, with or
    without cause, with or without notice, effective immediately. If
    you wish to terminate this Agreement or your tweetgo.net account
    (if you have one), you may simply discontinue using the Website.
    Notwithstanding the foregoing, if you have a VIP Services account,
    such account can only be terminated by tweetgo.net if you materially
    breach this Agreement and fail to cure such breach within thirty
    (30) days from tweetgo.net&rsquo;s notice to you thereof; provided
    that, tweetgo.net can terminate the Website immediately as part of a
    general shut down of our service. All provisions of this Agreement
    which by their nature should survive termination shall survive
    termination, including, without limitation, ownership provisions,
    warranty disclaimers, indemnity and limitations of liability.
    <strong><br /></strong></li>
    
    <li class="important"><strong>Disclaimer of Warranties.</strong>
    The Website is provided &ldquo;as is&rdquo;. tweetgo.net and its
    suppliers and licensors hereby disclaim all warranties of any kind,
    express or implied, including, without limitation, the warranties
    of merchantability, fitness for a particular purpose and
    non-infringement. Neither tweetgo.net nor its suppliers and
    licensors, makes any warranty that the Website will be error free
    or that access thereto will be continuous or uninterrupted. If
    you&rsquo;re actually reading this, <a href=
    "http://tweetgo.net/tos/treat/">here&rsquo;s a treat</a>. You
    understand that you download from, or otherwise obtain content or
    services through, the Website at your own discretion and risk.</li>
    
    <li class="important"><strong>Limitation of Liability.</strong> In
    no event will tweetgo.net, or its suppliers or licensors, be liable
    with respect to any subject matter of this agreement under any
    contract, negligence, strict liability or other legal or equitable
    theory for: (i) any special, incidental or consequential damages;
    (ii) the cost of procurement or substitute products or services;
    (iii) for interruption of use or loss or corruption of data; or
    (iv) for any amounts that exceed the fees paid by you to tweetgo.net
    under this agreement during the twelve (12) month period prior to
    the cause of action. tweetgo.net shall have no liability for any
    failure or delay due to matters beyond their reasonable control.
    The foregoing shall not apply to the extent prohibited by
    applicable law.</li>
    
    <li><strong>General Representation and Warranty.</strong> You
    represent and warrant that (i) your use of the Website will be in
    strict accordance with the tweetgo.net Privacy Policy, with this
    Agreement and with all applicable laws and regulations (including
    without limitation any local laws or regulations in your country,
    state, city, or other governmental area, regarding online conduct
    and acceptable content, and including all applicable laws regarding
    the transmission of technical data exported from the United States
    or the country in which you reside) and (ii) your use of the
    Website will not infringe or misappropriate the intellectual
    property rights of any third party.</li>
    
    <li><strong>Indemnification.</strong> You agree to indemnify and
    hold harmless tweetgo.net, its contractors, and its licensors, and
    their respective directors, officers, employees and agents from and
    against any and all claims and expenses, including attorneys&rsquo;
    fees, arising out of your use of the Website, including but not
    limited to your violation of this Agreement.</li>
    
    <li><strong>Miscellaneous.</strong> This Agreement constitutes the
    entire agreement between tweetgo.net and you concerning the subject
    matter hereof, and they may only be modified by a written amendment
    signed by an authorized executive of tweetgo.net, or by the posting
    by tweetgo.net of a revised version. Except to the extent applicable
    law, if any, provides otherwise, this Agreement, any access to or
    use of the Website will be governed by the laws of the state of
    California, U.S.A., excluding its conflict of law provisions, and
    the proper venue for any disputes arising out of or relating to any
    of the same will be the state and federal courts located in Los Angeles
     County, California. The prevailing party in any action or
    proceeding to enforce this Agreement shall be entitled to costs and
    attorneys&rsquo; fees. If any part of this Agreement is held
    invalid or unenforceable, that part will be construed to reflect
    the parties&rsquo; original intent, and the remaining portions will
    remain in full force and effect. A waiver by either party of any
    term or condition of this Agreement or any breach thereof, in any
    one instance, will not waive such term or condition or any
    subsequent breach thereof. You may assign your rights under this
    Agreement to any party that consents to, and agrees to be bound by,
    its terms and conditions; tweetgo.net may assign its rights under
    this Agreement without condition. This Agreement will be binding
    upon and will inure to the benefit of the parties, their successors
    and permitted assigns.</li>
    </ol>

    <% Html.RenderPartial("LicenseAndThanks"); %>
    </div>
</asp:Content>