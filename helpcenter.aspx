<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="helpcenter.aspx.cs" Inherits="helpcenter" %>


<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">
    
    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>Help</li>
            </ul>
        </div>
    </div>

   

    <section class="section section-padding">
        <div class="container">
            
            <main class="main-section content">

                <div id="sidebar" class="sidebar info-sidebar">
                    <div id="aside">
                        <div class="tab-box">
                            <h4 class="title" data-toggle="collapse" data-target="#tabList2">Let Us Help You</h4>
                            <ul id="tabList2" class="collapse in">
                                <li class="active"><a data-toggle="tab" href="#shipping">Shipping</a></li>
                                <li><a data-toggle="tab" href="#paymentProtection">Payment Protection</a></li>
                                <li><a data-toggle="tab" href="#returnsRefunds">Returns & Refunds</a></li>
                                <li><a data-toggle="tab" href="#faqs">Feedback</a></li>
                            </ul>
                        </div>
                        
                    </div>
                </div>

                <div class="main-content tab-content">

                    <div id="shipping" class="shipping tab-pane fade in active">
                        <h2>Shipping</h2>
                        <p>Free Shipping in all over India</p>
                    </div>

                    <div id="paymentProtection" class="paymentProtection tab-pane fade">
                        <h2>Payment Protection</h2>
                        <p>Go through a simple and secure payments process. In order to make payment, users can prefer an easy option from Net banking, Credit/Debit Card, UPI and other payment gateways. Cash on Delivery option is Available!</p>
                        <p>You may meet all requirements for Purchase Protection, and we'll repay you at the full buy cost in addition to any unique transportation costs, subject to terms and confinements. If you are charged for a transaction that you didn't make, let us know ASAP, and we've got you covered.</p>
                    </div>

                    <div id="returnsRefunds" class="returnsRefunds tab-pane fade">
                        <h2>Returns & Refunds</h2>
                        <p>If your purchase is not quite what you're looking for, you've got 07 days from when you received your order to have your items sent back. </p>
                        <p>As soon as we've received your return and issued your refund, we'll notify you by email. Make sure that all of the items you wish to return from your order arrive back with us together in one shipment, so that we can process your refund as quickly as possible. </p>
                        <p>Your return should be sent back to us within 07 days. Returns received outside of this time frame or not in a saleable condition will not be accepted for a refund at our discretion.</p>
                        <h3>REFUND</h3>
                        <p>We’ll process your refund within 5 working days of receiving your returned package post verification. However, during festival seasons it may take up to 7 working days. </p>
                        <p>As soon as we've received and taken care of your return, we'll email you to confirm that your refund has been issued.  </p>
                        <p>Please note that the refund timeframe may vary depending on the payment method and processing times between payment providers: </p>
                        <p>Our bank will credit your account within 10 working days of receipt of your items.</p>
                        <h3>OUR RETURNS POLICY</h3>
                        <p>Items should be returned new, unused and with the tags of Flaunt Basket still attached. Returns that are damaged, marked or altered will not be accepted for refund. </p>
                        <p>Please notify Customer Care if any of your purchases have been delivered without tags. You will need to request a return before sending your items back to us in original condition as sent to you.</p>
                        <h3>FAULTY GOODS</h3>
                        <p>Goods are classified as faulty if they are not of satisfactory quality, fit for purpose or as described. Please note that items which are damaged or as a result of normal wear and tear; by accident; or through misuse will not be considered faulty.  </p>
                        <p>If your item is faulty when you receive it, we request you to bring it to our notice within 48 hours of delivery through customer care email by sending a pic for further action. If you have owned your item for longer than this, and certainly over 1 month, then please contact Customer Care. </p>
                        <h3>COLOURS </h3>
                        <p>We have made every effort to display the colours of our products that appear on the website as accurately as possible. However, as computer monitors and devices may vary, we cannot guarantee that your screen's display of any colour will be completely accurate. </p>
                        <h3>REPEAT RETURNS </h3>
                        <p>We offer a flexible returns policy to make your online shopping experience even easier. We do monitor the number of returns made by customers, and continued returns will be flagged and may, at our discretion, lead to the closure of your account or future orders being refused. </p>
                    </div>

                    <div id="faqs" class="faqs tab-pane fade">
                        <h2>Feedback</h2>
                        <p>Feel free to contact S Style Factory by leaving a feedback in our app or sending an e-mail to our service center on <strong>contact@sstylefactory.com</strong></p>
                        <p>Contact No: <strong>+917506908279</strong></p>
                        
                    </div>
                   
                </div>

            </main>
        </div>
    </section>


</asp:Content>
