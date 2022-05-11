<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="aboutus.aspx.cs" Inherits="aboutus" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">

       <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>About</li>
            </ul>
        </div>
    </div>

    <section class="section section-padding">
        <div class="container">
            
            <main class="main-section content">

                <div id="sidebar" class="sidebar info-sidebar">
                    <div id="aside">
                        <div class="tab-box">
                            <h4 class="title" data-toggle="collapse" data-target="#tabList1">Get to Know Us</h4>
                            <ul id="tabList1" class="collapse in">
                                <li class="active"><a data-toggle="tab" href="#aboutUs">About</a></li>
                                <li><a data-toggle="tab" href="#termsConditions">Terms & Conditions</a></li>
                                <li><a data-toggle="tab" href="#privacyPolicy">Privacy Policy</a></li>
                                <li><a data-toggle="tab" href="#contactUs">Contact Us</a></li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="main-content tab-content">
                    
                    <div id="aboutUs" class="about tab-pane fade in active">
                        <h2>About Us</h2>
                       <p><b>SStyle Factory</b> brings fashion jewelry and accessories for the everyday needs of you and for your special occasions. Unique, high quality styles at competitive and affordable prices to keep you looking glamorous every day.</p>
                        <p>SStyle Factory is committed to bring to our customer the very best in value and satisfaction. Customers Happiness and Satisfaction is what we aim.</p>
                        <p>We launch new Products and Collection, in keeping with the fast pace of this industry and trying to eliminate the barrier of seasonal fashion. The online fashion market is growing with every passing day and what sets us apart is our ability to deliver an experience along with an impressive product. </p>
                        <p>SStyle Factory strive on establishing a relationship with each customer, giving them a freedom to express themselves thru the products available on this platform.</p>
                    </div>

                    <div id="termsConditions" class="terms-conditions tab-pane fade">
                        <h2>Terms & Conditions</h2>
                        <p>This Website is owned and operated by SStyle Factory. Throughout the site, the terms “we”, “us” and “our” refer to SStyle Factory.</p>
                        <h3>GENERAL TERMS AND CONDITIONS</h3>
                        <ul>
                            <li>By using our Website, you accept the Terms and Conditions of SStyle Factory. We (SStyle Factory) have the right to change these Terms and Conditions without any prior information to you.</li>
                            <li>By indicating your acceptance to buy any product or service offered on the site, you are obligated to complete such transactions. Users are prohibited from indicating their acceptance to buy products and services where they do not intend to complete such transactions.</li>
                            <li>We reserve the right to refuse service to anyone for any reason at any time.</li>
                            <li>SStyle Factory reserves the right not to accept orders received from users who are not "customers".</li>
                            <li>Prices for our products are subject to change without any prior notice.</li>
                            <li>We reserve the right at any time to modify or discontinue the Service (or any part or content thereof) without notice at any time.</li>
                            <li>We shall not be liable to you or to any third-party for any modification, price change, suspension or discontinuance of the service.</li>
                            <li>We have made every effort to display as accurately as possible the colours and images are subject to little differ as it looks online.</li>
                        </ul>
                    </div>

                    <div id="privacyPolicy" class="privacy-policy tab-pane fade">
                        <h2>Privacy Policy</h2>
                        <p>WeYou will find information on how we process your personal data in our Privacy Policy.</p>
                        <p>Please also read, if you haven't already done so, our General Terms and Conditions contain important information on the security we use.</p>
                        <p>For further information on our Privacy Policy, you can contact Customer Care, or send a request to the email address <strong>2015fabfashion@gmail.com</strong></p>
                        <h3>GOVERNING LAW AND DISPUTE RESOLUTION</h3>
                        <p>These General Terms and Conditions of Sale (including the Return Policy), their subject matter and formation, are governed by Indian law.</p>
                        <p>In the event of a dispute between SStyle Factory and you arising from the General Terms and Conditions of Sale (including the Return Policy)</p>
                        <h3>GOVERNING LAW</h3>
                        <p>These terms and conditions are governed by and construed in accordance with the laws of India’s Court and you irrevocably submit to the exclusive jurisdiction of the courts in that State or location. </p>
                        <h3>AMENDMENTS AND UPDATES</h3>
                        <p>The General Terms and Conditions of Sale (including the Returns Policy) may be amended from time to time. Any changes are effective as of the date of publication on https://fabfashionaccessories.com and will apply to any new orders you place following the date of publication.</p>
                        <p>Any amendments made to General Terms and Conditions or any other rules and procedures may or may not be notified to the customer. The customer is hence requested to go through terms and conditions before making any purchases.</p>
                    </div>

                    <div id="contactUs" class="contact-us tab-pane fade">
                        <h2>Contact Us</h2>
                        <p>D/206, Gokul Industrial Estate, Marol, <br />Andheri East, Mumbai-59.</p> 
                        <p><strong>Phone</strong> : 07506908279</p>
                        <p><strong>Email</strong> : 2015fabfashion@gmail.com</p>
                    </div>
                   
                </div>

            </main>
        </div>
    </section>

    </asp:Content>