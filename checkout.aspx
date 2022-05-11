<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="checkout.aspx.cs" Inherits="checkout" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">


    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>Checkout</li>
            </ul>
        </div>
    </div>



    <section class="section section-padding">
        <div class="container">


            <div class="checkoutBox">

                <h2>Checkout</h2>

                <div class="row">
                    <div class="col-md-8 col-sm-12">

                        <div class="billingBox">
                            <h3>Shipping Information</h3>
                            <div class="registerform row">
                                <div class="form-group col-md-6">
                                    <%-- <input class="form-control" type="text" placeholder="First Name">--%>
                                    <asp:TextBox ID="txtsfname" runat="server" CssClass="form-control" placeholder="First Name" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf1" runat="server" ControlToValidate="txtsfname" ForeColor="Red" ErrorMessage="Enter First Name" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <%--<input class="form-control" type="text" placeholder="Last Name">--%>
                                    <asp:TextBox ID="txtslname" runat="server" CssClass="form-control" placeholder="Last Name" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf2" runat="server" ControlToValidate="txtslname" ForeColor="Red" ErrorMessage="Enter Last Name" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <%--<input class="form-control" type="tel" placeholder="Phone">--%>
                                    <asp:TextBox ID="txtsmobile" runat="server" autocomplete="off" CssClass="form-control" onblur="checkdigit();" placeholder="*Mobile No. (10 digits only)" MaxLength="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="ftb1" runat="server" TargetControlID="txtsmobile" FilterMode="ValidChars" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="rf3" runat="server" ControlToValidate="txtsmobile" ForeColor="Red" ErrorMessage="Enter Mobile" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <%-- <input class="form-control" type="email" placeholder="Email">--%>
                                    <asp:TextBox ID="txtsemail" runat="server" autocomplete="off" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf4" runat="server" ControlToValidate="txtsemail" ForeColor="Red" ErrorMessage="Enter Email" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="re1" runat="server" ControlToValidate="txtsemail" ForeColor="Red" ErrorMessage="Enter Valid Email" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-md-12">
                                    <%--<input class="form-control" type="text" placeholder="Address">--%>
                                    <asp:TextBox ID="txtsaddress1" runat="server" autocomplete="off" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf5" runat="server" ControlToValidate="txtsaddress1" ForeColor="Red" ErrorMessage="Enter Email" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <asp:DropDownList ID="drpscountry" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="India" Value="India"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                   <%-- <asp:TextBox ID="txtsstate" runat="server" autocomplete="off" CssClass="form-control" placeholder="State"></asp:TextBox>--%>
                                    <asp:DropDownList ID="drpsstate" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="-- State --" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Andhra Pradesh" Value="Andhra Pradesh"></asp:ListItem>
                                        <asp:ListItem Text="Arunachal Pradesh" Value="Arunachal Pradesh"></asp:ListItem>
                                        <asp:ListItem Text="Assam" Value="Assam"></asp:ListItem>
                                        <asp:ListItem Text="Andaman and Nicobar Islands" Value="Andaman and Nicobar Islands"></asp:ListItem>
                                        <asp:ListItem Text="Bihar" Value="Bihar"></asp:ListItem>
                                        <asp:ListItem Text="Chandigarh" Value="Chandigarh"></asp:ListItem>
                                        <asp:ListItem Text="Chhattisgarh" Value="Chhattisgarh"></asp:ListItem>
                                        <asp:ListItem Text="Dadar and Nagar Haveli" Value="Dadar and Nagar Haveli"></asp:ListItem>
                                        <asp:ListItem Text="Daman and Diu" Value="Daman and Diu"></asp:ListItem>
                                        <asp:ListItem Text="Delhi" Value="Delhi"></asp:ListItem>
                                        <asp:ListItem Text="Goa" Value="Goa"></asp:ListItem>
                                        <asp:ListItem Text="Gujarat" Value="Gujarat"></asp:ListItem>
                                        <asp:ListItem Text="Haryana" Value="Haryana"></asp:ListItem>
                                        <asp:ListItem Text="Himachal Pradesh" Value="Himachal Pradesh"></asp:ListItem>
                                        <asp:ListItem Text="Jammu and Kashmir" Value="Jammu and Kashmir"></asp:ListItem>
                                        <asp:ListItem Text="Jharkhand" Value="Jharkhand"></asp:ListItem>
                                        <asp:ListItem Text="Karnataka" Value="Karnataka"></asp:ListItem>
                                        <asp:ListItem Text="Kerala" Value="Kerala"></asp:ListItem>
                                        <asp:ListItem Text="Lakshadweep" Value="Lakshadweep"></asp:ListItem>
                                        <asp:ListItem Text="Madhya Pradesh" Value="Madhya Pradesh"></asp:ListItem>
                                        <asp:ListItem Text="Maharashtra" Value="Maharashtra"></asp:ListItem>
                                        <asp:ListItem Text="Manipur" Value="Manipur"></asp:ListItem>
                                        <asp:ListItem Text="Meghalaya" Value="Meghalaya"></asp:ListItem>
                                        <asp:ListItem Text="Mizoram" Value="Mizoram"></asp:ListItem>
                                        <asp:ListItem Text="Nagaland" Value="Nagaland"></asp:ListItem>
                                        <asp:ListItem Text="Odisha" Value="Odisha"></asp:ListItem>
                                        <asp:ListItem Text="Puducherry" Value="Puducherry"></asp:ListItem>
                                        <asp:ListItem Text="Punjab" Value="Punjab"></asp:ListItem>
                                        <asp:ListItem Text="Rajasthan" Value="Rajasthan"></asp:ListItem>
                                        <asp:ListItem Text="Sikkim" Value="Sikkim"></asp:ListItem>
                                        <asp:ListItem Text="Tamil Nadu" Value="Tamil Nadu"></asp:ListItem>
                                        <asp:ListItem Text="Telangana" Value="Telangana"></asp:ListItem>
                                        <asp:ListItem Text="Tripura" Value="Tripura"></asp:ListItem>
                                        <asp:ListItem Text="Uttar Pradesh" Value="Uttar Pradesh"></asp:ListItem>
                                        <asp:ListItem Text="Uttarakhand" Value="Uttarakhand"></asp:ListItem>
                                        <asp:ListItem Text="West Bengal" Value="West Bengal"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rf6" runat="server" ControlToValidate="drpsstate" ForeColor="Red" InitialValue="0" ErrorMessage="Select State" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <asp:TextBox ID="txtscity" runat="server" autocomplete="off" CssClass="form-control" placeholder="City"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf7" runat="server" ControlToValidate="txtscity" ForeColor="Red" ErrorMessage="Enter City" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-6">
                                    <asp:TextBox ID="txtszip" runat="server" autocomplete="off" CssClass="form-control" placeholder="Pincode"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rf8" runat="server" ControlToValidate="txtszip" ForeColor="Red" ErrorMessage="Enter Pincode" SetFocusOnError="true" Display="Dynamic" ValidationGroup="order"></asp:RequiredFieldValidator>
                                </div>


                            </div>

                        </div>

                    </div>



                    <div class="col-md-offset-0 col-md-4 col-sm-offset-2 col-sm-8">
                        <div class="cartPrice-detail">

                            <div class="cartCoupon">
                                <%--<input type="text" name="coupon_code" class="form-control" id="coupon_code" placeholder="Apply Coupon code">--%>
                                <asp:TextBox ID="txtcouponcode" runat="server" CssClass="form-control" placeholder="Apply Coupon code" autocomplete="off"></asp:TextBox>
                                <%--<button type="submit" class="btn applyBtn">Apply</button>--%>
                                <asp:Button ID="btncoupon" runat="server" Text="Apply" CssClass="btn applyBtn" OnClick="btncoupon_Click" />
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                            </div>

                            <div class="cartTotal">
                                <h4>Payment Summary</h4>
                                <div class="subTotal">
                                    <span class="title">Bag Total  : </span>
                                    <span class="price"><i class="fa fa-inr"></i>
                                        <asp:Literal ID="ltrlSubTotal" runat="server"></asp:Literal></span>
                                </div>
                                <div class="deliveryAmount">
                                    <span class="title">Delivery Charges: </span>
                                    <span class="price">FREE</span>
                                </div>
                                <div class="couponDiscount" id="dvcoupondisc" runat="server">
                                    <span class="title">Coupon Discount: </span>
                                    <span class="price"><i class="fa fa-inr"></i>
                                        <asp:Literal ID="ltrdiscount" runat="server" Text="0"></asp:Literal>
                                    </span>
                                </div>
                                <div class="grandTotal">
                                    <span class="title"><strong>Grand Total  : </strong></span>
                                    <span class="price"><strong><i class="fa fa-inr"></i>
                                        <asp:Literal ID="ltrlNetTotal" runat="server"></asp:Literal>
                                    </strong></span>
                                </div>
                            </div>
                            <div class="order-btn">

                                <asp:Label ID="lblcodmsg" runat="server" style="color:#f00;" Visible="false" ># COD is not available on order below <i class="fa fa-inr"></i> 500</asp:Label>
                               

                                <asp:Button ID="btncheckout" runat="server" CssClass="btn theme-btn" Visible="false" Text="Place Order (COD)" ValidationGroup="order" OnClick="btncheckout_Click" />

                                <asp:Button ID="btnpaytm" runat="server" CssClass="btn theme-btn" Text="Pay Online" ValidationGroup="order" OnClick="btnpaytm_Click" />
                                <%-- * We will send an OTP on your given mobile no.--%>
                            </div>

                            <%--<div>
                                 <asp:TextBox ID="txtotp" runat="server" autocomplete="off" CssClass="form-control" placeholder="OTP"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" CssClass="btn theme-btn" Text="Confirm COD Order" ValidationGroup="order"  />
                            </div>--%>
                        </div>
                    </div>
                </div>


            </div>


        </div>
    </section>

    <script>
        function checkdigit() {

            var mb = $("#cp1_txtsmobile").val();

            if (length(mb) == 10) {

            }
            else {
                alert("Enter 10 digit mobile no.");
            }
        }
    </script>


</asp:Content>
