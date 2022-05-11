<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="mycart.aspx.cs" Inherits="mycart" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">


    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>Shopping Cart</li>
            </ul>
        </div>
    </div>



    <section class="section section-padding">
        <div class="container">

            <div class="shopingCart" id="divCart" runat="server">


                <h2>Shopping Cart</h2>


                <div class="row" >
                    <div class="col-md-8 col-sm-12">
                        <table class="cart-table" width="100%" style="width: 100%;">

                            <asp:Repeater ID="repCart" runat="server" OnItemCommand="repCart_ItemCommand" OnItemDataBound="repCart_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="cart-item">images
                                        <td class="colum remove-item"><%--<a href="#" class="remove" aria-label="Remove this item"><i class="fa fa-times-circle"></i></a>--%>
                                            <asp:LinkButton runat="server" ToolTip="Remove this item" ID="ltrlDelete" CssClass="remove" CommandArgument='<%#Eval("ProductId") %>' CommandName="del" OnClientClick="return confirm('Are you sure to remove this item !')"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </td>
                                        <td class="product-img colum" title="Product">
                                            <%-- <img src="images/products/img4.jpeg" />--%>
                                            <img src="<%#"upload/products/small/"+Eval("img") %>" onerror="this.src='<%=Page.ResolveUrl("/noimage.jpg")%>'" />
                                        </td>
                                        <td class="description colum">
                                            <h4><%# Eval("shortdesc") %></h4>
                                            <asp:HiddenField ID="hfvariation" runat="server" Value='<%# Eval("variation") %>' />
                                            <asp:HiddenField ID="hfsize" runat="server" Value='<%# Eval("size") %>' />
                                            <asp:HiddenField ID="hfcolor" runat="server" Value='<%# Eval("color") %>' />
                                            <asp:HiddenField ID="hfremarks" runat="server" Value='<%# Eval("remarks") %>' />

                                           

                                            <div class="cVari-area" id="dvvariation" runat="server">
                                                <label>Variations</label>
                                                 <asp:DropDownList ID="drpvariatioin" runat="server" CssClass="cVariations" ToolTip="Size - Color Combination" AutoPostBack="true" OnSelectedIndexChanged="drpvariatioin_SelectedIndexChanged">

                                            </asp:DropDownList>
                                           <%-- <select class="cVariations">
                                                <option>E-GREEN</option>
                                                <option>M-BLUE</option>
                                                <option>E-YELLOW</option>
                                            </select>--%>
                                            </div>
                                            

                                            <%--<asp:TextBox ID="txtremarks" runat="server" Text='<%# Eval("remarks") %>' CssClass="form-control" placeholder="Specify product requirements like size, colors etc." OnTextChanged="txtremarks_TextChanged" AutoPostBack="true" ></asp:TextBox>--%>
                                            <p><strong><%# Eval("ext1") +" | "+ Eval("ext2") %></strong></p>
                                            <%--<p style='<%# Eval("size").ToString()=="" ? "display:none;" : "" %>'><span>Size: </span><strong><%# Eval("Size") %></strong></p>
                                            <p style='<%# Eval("color").ToString()=="" ? "display:none;" : "" %>'><span>Color: </span><strong><%# Eval("color") %></strong></p>--%>
                                            <p><span>Item Code: </span><strong><%# Eval("modelno") %></strong></p>
                                        </td>
                                        <td class="colum quantity" title="Quantity">
                                            <label>Qty:</label>

                                            <asp:TextBox runat="server" ID="txtQty" onkeypress="return digits(this,event,false,false)" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" Style="text-align: center;" Text='<%#Eval("qty") %>' CssClass="form-control" Width="50" MaxLength="4" size="1"></asp:TextBox>
                                            <%--<select class="form-control quantity">
                                                <option>1</option>
                                                <option>2</option>
                                                <option>3</option>
                                                <option>4</option>
                                                <option>5</option>
                                            </select>--%>
                                            <asp:HiddenField runat="server" ID="hddId" Value='<%#Eval("ProductId") %>' />

                                        </td>
                                        <td class="colum price" title="Price">
                                            <p class="price-detail">
                                                <span class="off-price">₹<%# Eval("discountprice") %></span>
                                             <%--   <span class="old-price">₹<%# Eval("unitPrice") %></span>--%>
                                                <p style="margin-bottom: 0;">Total</p>
                                                <span class="special-price">₹<%# Convert.ToDecimal(Eval("discountprice"))* Convert.ToDecimal(Eval("qty")) %></span>
                                            </p>
                                            <%--<div class="moveWishlist"><a href="#"><i class="fa fa-heart-o"></i>Move to WishList</a></div>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                         <%--   <tr class="cart-item">
                                <td class="colum remove-item"><a href="#" class="remove" aria-label="Remove this item"><i class="fa fa-times-circle"></i></a></td>
                                <td class="product-img colum" title="Product">
                                    <img src="images/products/img4.jpeg" /></td>
                                <td class="description colum">
                                    <h4>OUTLOOK QUEENS Shoulder Bag</h4>
                                    <p><strong>Women's Bags</strong></p>
                                    <p><span>Size: </span><strong>XL</strong></p>
                                    <p><span>Item Code: </span><strong>FBR001</strong></p>
                                </td>
                                <td class="colum quantity" title="Quantity">
                                    <label>Qty:</label>
                                    <select class="form-control quantity">
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                    </select></td>
                                <td class="colum price" title="Price">
                                    <p class="price-detail">
                                        <span class="special-price">₹281</span>
                                        <span class="old-price">₹1299</span>
                                        <span class="off-price">off 75%</span>
                                    </p>
                                    <div class="moveWishlist"><a href="#"><i class="fa fa-heart-o"></i>Move to WishList</a></div>
                                </td>
                            </tr>
                            <tr class="cart-item">
                                <td class="colum remove-item"><a href="#" class="remove" aria-label="Remove this item"><i class="fa fa-times-circle"></i></a></td>
                                <td class="product-img colum" title="Product">
                                    <img src="images/products/img5.jpeg" /></td>
                                <td class="description colum">
                                    <h4>CRYSTLE Shoulder Bag</h4>
                                    <p><strong>Women's Bags</strong></p>
                                    <p><span>Size: </span><strong>XL</strong></p>
                                    <p><span>Item Code: </span><strong>FBR001</strong></p>
                                </td>
                                <td class="colum quantity" title="Quantity">
                                    <label>Qty:</label>
                                    <select class="form-control quantity">
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                    </select></td>
                                <td class="colum price" title="Price">
                                    <p class="price-detail">
                                        <span class="special-price">₹319</span>
                                        <span class="old-price">₹999</span>
                                        <span class="off-price">off 85%</span>
                                    </p>
                                    <div class="moveWishlist"><a href="#"><i class="fa fa-heart-o"></i>Move to WishList</a></div>
                                </td>
                            </tr>--%>

                        </table>
                    </div>

                    <div class="col-md-offset-0 col-md-4 col-sm-offset-2 col-sm-8">
                        <div class="cartPrice-detail">
                            <%--<h4>Option</h4>
                            <div class="cartCoupon">
                                <input type="text" name="coupon_code" class="form-control" id="coupon_code" placeholder="Apply Coupon code">
                                <button type="submit" class="btn applyBtn">Apply</button>
                            </div>--%>

                            <div class="cartTotal">
                                <h4>Payment Summary</h4>
                                <div class="subTotal">
                                    <span class="title">Bag Total  : </span>
                                    <span class="price"><i class="fa fa-inr"></i><asp:Literal ID="ltrlSubTotal" runat="server"></asp:Literal></span>
                                </div>
                                <div class="deliveryAmount">
                                    <span class="title">Delivery Charges: </span>
                                    <span class="price">FREE</span>
                                </div>
                             <%--   <div class="couponDiscount">
                                    <span class="title">Coupon Discount: </span>
                                    <span class="price"><i class="fa fa-inr"></i>0.00</span>
                                </div>--%>
                                <div class="grandTotal">
                                    <span class="title"><strong>Grand Total  : </strong></span>
                                    <span class="price"><strong><i class="fa fa-inr"></i><asp:Literal ID="ltrlNetTotal" runat="server"></asp:Literal> </strong></span>
                                </div>
                            </div>
                            <div class="checkout-btn">
                                <%--<button class="btn theme-btn">Proceed to Checkout</button>--%>
                                <a href="checkout.html" class="btn theme-btn">Proceed to Checkout</a>
                               
                            </div>
                        </div>

                    </div>
                </div>



                <div class="back-toShopping"><a class="btn" href="#">Continue Shopping</a></div>


            </div>

            <div style="display: none; text-align: center;" runat="server" id="divEmptyCart" class="divEmptyCart">
                                Your Shopping Cart is Empty !!<br />
                                <br />
                               
                                <a class="btn" href="index.html">Continue Shopping</a>
                            </div>


        </div>
    </section>



</asp:Content>
