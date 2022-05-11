<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="myaccount.aspx.cs" Inherits="myaccount" %>

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

            <main class="main-section content dashboard">

                <div id="sidebar" class="sidebar info-sidebar">
                    <div id="aside">
                        
                        <div class="user">
                            <span class="icon"><img class="img-responsive" src="images/user-default.jpg" alt="user icon" /></span>
                            <span  class="name">
                                <asp:Literal ID="ltrusername" runat="server"></asp:Literal>
                            </span>
                        </div>

                        <ul class="dashboard-menu">
                            <li class="active"><a data-toggle="tab" href="#myAccount">My Account</a></li>
                            <li><a data-toggle="tab" href="#passwordChange">Change Password</a></li>
                            <li><a data-toggle="tab" href="#MyOrder">My Orders</a></li>
                           <%-- <li><a href="track-order.html">Track My Order</a></li>--%>
                            <li>
                                <asp:LinkButton ID="lnkmyaccountlogout" runat="server" Text="Logout" OnClick="lnkmyaccountlogout_Click"></asp:LinkButton>
                            </li>
                        </ul>

                    </div>
                </div>

                <div class="main-content tab-content">
                    
                    <div id="myAccount" class="about tab-pane fade in active">
                        <h2>Account Information</h2>

                        <div class="row">

                             <div class="col-md-6 form-group">
                        <label>First Name*</label>
                        <asp:TextBox ID="txtfname" runat="server" CssClass="form-control"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rf1" runat="server" ControlToValidate="txtfname" Display="Dynamic" ForeColor="Red" ValidationGroup="profile" ErrorMessage="Enter First Name"></asp:RequiredFieldValidator>
                        </div>

                            <div class="col-md-6 form-group">
                        <label>Last Name*</label>
                        <asp:TextBox ID="txtlname" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlname" Display="Dynamic" ForeColor="Red" ValidationGroup="profile" ErrorMessage="Enter Last Name"></asp:RequiredFieldValidator>
                        </div>

                             <div class="col-md-6 form-group">
                        <label>Email*</label>
                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtemail" Display="Dynamic" ForeColor="Red" ValidationGroup="profile" ErrorMessage="Enter Email"></asp:RequiredFieldValidator>
                        </div>

                             <div class="col-md-6 form-group">
                        <label>Mobile No.*</label>
                       <asp:TextBox ID="txtmobileno" runat="server" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtmobileno" Display="Dynamic" ForeColor="Red" ValidationGroup="profile" ErrorMessage="Enter Mobile No."></asp:RequiredFieldValidator>
                        </div>
                             <div class="col-md-12 form-group">
                        <label>Address</label>
                        <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                            <div class="col-md-6 form-group">
                        <label>City</label>
                        <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                            <div class="col-md-6 form-group">
                        <label>State</label>
                        <asp:TextBox ID="txtstate" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                            <div class="col-md-6 form-group">
                        <label>Country</label>
                        <asp:DropDownList ID="drpcountry" runat="server" CssClass="form-control">
                            <asp:ListItem Text="India" Value="India"></asp:ListItem>
                        </asp:DropDownList>
                        </div>

                            <div class="col-md-6 form-group">
                        <label>Pincode</label>
                        <asp:TextBox ID="txtpincode" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        </div>

                        <br /><br />

                       
                        <div class="form-group">
                            <asp:Button ID="btnupdateinfo" runat="server" CssClass="btn theme-btn" Text="Update" ValidationGroup="profile" OnClick="btnupdateinfo_Click" />
                        </div>
                    </div>

                    <div id="passwordChange" class="about tab-pane fade">
                        <h2>Password</h2>
                        <div class="form-group">
                        <label>Current Password</label>
                        <asp:TextBox ID="txtcurrentpass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcurrentpass" Display="Dynamic" ForeColor="Red" ValidationGroup="cp" ErrorMessage="Enter Current Password"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                        <label>New Password</label>
                        <asp:TextBox ID="txtnewpass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtnewpass" Display="Dynamic" ForeColor="Red" ValidationGroup="cp" ErrorMessage="Enter New Password"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                        <label>Confirm Password</label>
                        <asp:TextBox ID="txtconfirmpass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtconfirmpass" Display="Dynamic" ForeColor="Red" ValidationGroup="cp" ErrorMessage="Enter Confirm Password"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <%--<button class="btn theme-btn" type="button">Update</button>--%>
                            <asp:Button ID="btnupdatepassword" runat="server" CssClass="btn theme-btn" Text="Change Password" ValidationGroup="cp" OnClick="btnupdatepassword_Click" />
                        </div>
                    </div>


                    <div id="MyOrder" class="about tab-pane fade">
                         <h2>My Orders</h2>
                        <div class="row">
                            <div id="dvmyorder">
                            <div id="dvorderdata" runat="server">
                                <div class="table-responsive">
                                    <table class="table orderHead">
                                        <tr>
                                            <th>Order Date</th>
                                            <th>Order No</th>
                                            <th>Total</th>
                                            <th>Disc Amt</th>
                                            <th>Grand Total</th>
                                            <th></th>
                                        </tr>
                                       
                                        <asp:Repeater ID="rptorder" runat="server" OnItemDataBound="rptorder_ItemDataBound" >
                                            <ItemTemplate>
                                                   

                                                          
                                                        <tr>
                                                            <td><%# Eval("OrderDate") %></td>
                                                            <td><%# Eval("OrderNo") %></td>
                                                            <td><%# Eval("ordercurrency") + " "+Eval("OrderTotal") %></td>
                                                            <td><%#  Eval("ordercurrency") + " "+Eval("CouponAmt") %></td>
                                                            <td><%# Eval("ordercurrency") + " "+ (Convert.ToDecimal(Eval("OrderTotal")) - Convert.ToDecimal( Eval("CouponAmt"))) %></td>
                                                            <td><a data-toggle="collapse" data-parent="#hweorderlist" href="#collapse<%# Container.ItemIndex %>"><i class="fa fa-angle-down"></i></a></td>
                                                        </tr>
                                                        
                                                         
                                                   

                                                 
                                                    
                                                        
                                                    <tr id='collapse<%# Container.ItemIndex %>' class="collapse">

                                                        <td colspan="6" class="shopingCart table-responsive no-padding">
                                                            <asp:Repeater ID="rptinner" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="table cart-table">
                                                                        
                                                                        <tbody>
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="product-img colum">
                                                                            <%--<a href='<%# Page.ResolveUrl(Common.url(Eval("StyleName").ToString().Replace("-","collection"))+"/"+Common.url(Eval("ItemName").ToString())+"/"+Common.url(Eval("Remark").ToString())+"-"+Eval("Id")+".html")%>'>--%>
                                                                                <img style="max-height:68px;" src='<%#Page.ResolveUrl("upload/products/small/") + Eval("skuname")+".jpg" %>'>

                                                                            <%--</a>--%>
                                                                        </td>
                                                                        <td class="description colum">SKU: <%# Eval("SkuName") %><br />                                                                                                                                                 
                                                                            <asp:Literal ID="ltrsize" runat="server" Text='<%# "Size: "+ Eval("size") %>' Visible='<%# Eval("size").ToString() == "-" ? false : true %>'></asp:Literal>
                                                                            <%# Eval("size").ToString() == "-" ? "" : "<br />" %>
                                                                            <p>Item: <%# Eval("ItemName") %></p>
                                                                        </td>
                                                                        <td class="colum quantity" title="Quantity">
                                                                            <label>Qty:</label>
                                                                            <asp:TextBox CssClass="form-control" ID="txtqty" Enabled="false" runat="server" onkeypress="return digits(this,event,true,false);" ReadOnly="true" Width="50px" Text='<%# Eval("Qty") %>'></asp:TextBox>
                                                                        </td>
                                                                        <td class="colum price" title="Price">
                                                                            <p class="price-detail">
                                                                                <span class="off-price"><%# Math.Round(Convert.ToDecimal(Eval("discountedprice"))>0?Convert.ToDecimal(Eval("discountedprice")):Convert.ToDecimal(Eval("price")))<=0 ? "" :  Eval("ordercurrency") +" "+ Math.Round(Convert.ToDecimal(Eval("discountedprice"))>0?Convert.ToDecimal(Eval("discountedprice")):Convert.ToDecimal(Eval("price"))) %></span></p>
                                                                            <p style="margin-bottom: 0;">Total</p>
                                                                                <span class="special-price"><%# (Convert.ToDecimal(Eval("discountedprice"))>0?Convert.ToDecimal(Eval("discountedprice"))*Convert.ToDecimal(Eval("Qty")):Convert.ToDecimal(Eval("price"))*Convert.ToDecimal(Eval("Qty")))<=0 ? "" : Eval("ordercurrency") +" " +(Convert.ToDecimal(Eval("discountedprice"))>0?Convert.ToDecimal(Eval("discountedprice"))*Convert.ToDecimal(Eval("Qty")):Convert.ToDecimal(Eval("price"))*Convert.ToDecimal(Eval("Qty")))  %></span>
                                            
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                        
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                             
                                                           
                                            </ItemTemplate>
                                        </asp:Repeater>
                                                
                                    </table>
                                    
                                </div>
                            </div>
                            <div class="notfound text-center" id="spnordlist" runat="server">There is no order list</div>
                        </div>
                        </div>
                    </div>
                    
                   
                </div>

            </main>
        </div>
    </section>

</asp:Content>
