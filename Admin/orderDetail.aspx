<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderDetail.aspx.cs" Inherits="admin_orderdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/animate.css" rel="stylesheet" />
    <link rel="stylesheet" href="../admincss/css/bootstrap.min.css" type="text/css" />
    <link href="../admincss/css/artstyle.css" rel="stylesheet" />

    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("../admincss/css/style-responsive.css")) %>' rel="stylesheet" />
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("../admincss/css/atom-style.css")) %>' rel="stylesheet" />
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("../admincss/css/font-awesome.min.css")) %>' rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300' rel='stylesheet' type='text/css' />
    <!-- Google Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Lato:400,300,700,900' rel='stylesheet' type='text/css' />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script type="text/javascript" src='<%= Page.ResolveUrl("../js/bootstrap.min.js")%>'></script>
    <style>
        body {
            padding: 50px 0;
        }
    </style>
</head>



<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 id="enquiryno" runat="server"></h2>
            <asp:Repeater ID="repCart" runat="server">
                <HeaderTemplate>
                    <table id="wishlist-table" class="clean-table linearize-table data-table">
                        <thead>
                            <tr class="first last">
                                <th class="customer-wishlist-item-image">Image</th>
                                <th class="customer-wishlist-item-info">Description</th>
                                <th class="customer-wishlist-item-quantity">Quantity</th>
                                <th class="customer-wishlist-item-price">Price</th>
                                <th class="customer-wishlist-item-cart">Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hddId" Value='<%#Eval("Productid") %>'></asp:HiddenField>
                    <tr id="item_31" class="first odd ">
                        <td class="wishlist-cell0 customer-wishlist-item-image text-center">
                            <img class="fixtoheight" alt='<%# Eval("shortdesc") %>' src='<%# Page.ResolveUrl("https://www.sstylefactory.com/upload/products/small/" + Eval("img")) %>'>
                        </td>
                        <td class="wishlist-cell1 customer-wishlist-item-info">
                             <h3 class="product-name">SKU: <%# Eval("modelno") %></h3><br />
                            <h3 class="product-name"><a title='<%# Eval("shortdesc") %>' href="#"><%# Eval("shortdesc") %></a></h3>
                            <div class="description std">
                                <div class="inner">Size : <%# Eval("Size") %></div>
                            </div>
                            <div><%# Eval("Comment").ToString()==""?"":"Comment:"+Eval("Comment")+"" %> </div>
                           </td>
                        <td data-rwd-label="Quantity" class="wishlist-cell2 customer-wishlist-item-quantity">
                            <div class="cart-cell">
                                <div class="add-to-cart-alt">
                                    <asp:TextBox ID="txtQty" runat="server" CssClass="input-text qty validate-not-negative-number" onkeypress="return digits(this,event,false,false)" Text='<%# Eval("qty") %>' ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </td>
                        <td data-rwd-label="Price" class="wishlist-cell3 customer-wishlist-item-price">
                            <div class="cart-cell">
                                <div class="price-box">
                                    <span class="regular-price"><span class="price">Rs. <asp:Literal ID="ltrprice" runat="server" Text='<%# Math.Round(Convert.ToDecimal(Eval("Price")))%>'></asp:Literal></span> </span>
                                </div>
                            </div>
                        </td>
                        <td class="wishlist-cell4 customer-wishlist-item-cart">
                            <div class="cart-cell">
                                <div class="cart-cell">
                                    <div class="price-box">
                                        <span class="regular-price"><span class="price" style="color: #444;">Rs. <asp:Literal ID="ltramt" runat="server" Text='<%# Math.Round(Convert.ToDecimal( Eval("total").ToString())) %>'></asp:Literal></span> </span>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                    </tbody>
</table>
                </FooterTemplate>

            </asp:Repeater>

            <div class="row">
                <div class="col-md-8">
                 <span style="font-size:16px; margin-top:10px; font-weight:bold;">Payment Status:</span> <asp:Label ID="lblpaymentstatus" runat="server"></asp:Label>  <asp:Label ID="lblpaymentid" runat="server"></asp:Label>
                </div>

                <div class="col-md-4">
                     <div runat="server" id="discount" visible="false">
            <h3 class="col-md-12 text-right">Sub Total : Rs. <asp:Literal runat="server" ID="ltrsubtot"></asp:Literal></h3>
            <h3 class="col-md-12 text-right">Discount Amt : Rs. <asp:Literal runat="server" ID="ltrdiscount"></asp:Literal></h3>
                </div>
            <h3 class="col-md-12 text-right">Total Amt : Rs. <asp:Literal runat="server" ID="ltrlTotalAmt"></asp:Literal></h3>
                </div>

            </div>

           


            <div class="row">

                <div class="col-md-6">
                    <h3>Billing Address</h3>
                    <asp:Label ID="lblbillname" runat="server" Text="Gaurav" ></asp:Label><br />
                    <asp:Label ID="lblbilladdress1" runat="server" Text="Jaipur" ></asp:Label><br />
                    <asp:Label ID="lblbilladdress2" runat="server" Text="Jaipur" ></asp:Label>
                    <asp:Label ID="lblbillcity" runat="server" Text="Jaipur" ></asp:Label>, 
                    <asp:Label ID="lblbillstate" runat="server" Text="Jaipur" ></asp:Label><br />
                    <asp:Label ID="lblbillcountry" runat="server" Text="India" ></asp:Label> - <asp:Label ID="lblbillzip" runat="server" Text="India" ></asp:Label><br />
                    Contact No.: <asp:Label ID="lblbillcontactno" runat="server" Text="India" ></asp:Label><br />
                    <asp:Label ID="lblbillemail" runat="server" Text="India" ></asp:Label><br />
                </div>
                <div class="col-md-6">
                     <h3>Shipping Address</h3>
                    <asp:Label ID="lblshipname" runat="server" Text="Gaurav" ></asp:Label><br />
                    <asp:Label ID="lblshipaddress1" runat="server" Text="Jaipur" ></asp:Label><br />
                    <asp:Label ID="lblshipaddress2" runat="server" Text="Jaipur" ></asp:Label>
                    <asp:Label ID="lblshipcity" runat="server" Text="Jaipur" ></asp:Label>, 
                    <asp:Label ID="lblshipstate" runat="server" Text="Jaipur" ></asp:Label><br />
                    <asp:Label ID="lblshipcountry" runat="server" Text="India" ></asp:Label> - <asp:Label ID="lblshipzip" runat="server" Text="India" ></asp:Label><br />
                    Contact No.: <asp:Label ID="lblshipcontactno" runat="server" Text="India" ></asp:Label><br />
                    <asp:Label ID="lblshipemail" runat="server" Text="India" ></asp:Label><br />
                </div>
            </div>

        </div>
    </form>
</body>
</html>
<script>
    function sadsa() {

    }
</script>
