<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="wishlist.aspx.cs" Inherits="wishlist" %>

<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">


    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.html">Home</a></li>
                <li>Wishlist</li>
            </ul>
        </div>
    </div>



    <section class="section section-padding">
        <div class="container">

            <div class="shopingCart wishlistItems" id="divCart" runat="server">
                <h2>My Wishlist</h2>
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <table class="cart-table" width="100%" style="width: 100%;">



                            <asp:Repeater ID="repCart" runat="server" OnItemCommand="repCart_ItemCommand" OnItemDataBound="repCart_ItemDataBound">
                                <ItemTemplate>

                                    <tr class="cart-item in-stock">

                                        <td class="colum remove-item"><%--<a href="#" class="remove" aria-label="Remove this item"><i class="fa fa-times-circle"></i></a>--%>
                                            <asp:LinkButton runat="server" ToolTip="Remove this item" ID="ltrlDelete" CssClass="remove" CommandArgument='<%#Eval("ProductId") %>' CommandName="del" OnClientClick="return confirm('Are you sure to remove this item from wishlist !')"><i class="fa fa-times-circle"></i></asp:LinkButton>
                                        </td>

                                        <td class="product-img colum" title='<%# Eval("shortdesc") %>'>
                                            <%-- <img src="images/products/img4.jpeg" />--%>
                                            <a href='<%#Page.ResolveClientUrl(Common.url(Eval("ext1").ToString())+"/"+Common.url(Eval("ext2").ToString())+"/"+Common.url( Eval("shortdesc").ToString())+"-"+Eval("ProductId") +".html")%>'>
                                                <img src="<%#"upload/products/small/"+Eval("img") %>" onerror="this.src='<%=Page.ResolveUrl("/noimage.jpg")%>'" />
                                            </a>
                                        </td>

                                        <td class="description colum">
                                            <%-- <h4>OUTLOOK QUEENS Shoulder Bag</h4>--%>
                                            <%--<p><strong>Women's Bags</strong></p>--%>


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

                                            <asp:Literal ID="ltrqty" runat="server" Text="1"></asp:Literal>



                                            <asp:HiddenField runat="server" ID="hddId" Value='<%#Eval("ProductId") %>' />

                                        </td>

                                        <%-- <td class="colum price" title="Price">
                                            <p class="price-detail">
                                                <span class="special-price">₹281</span>
                                                <span class="old-price">₹1299</span>
                                                <span class="off-price">off 75%</span>
                                            </p>
                                            <div class="moveWishlist"><a class="btn theme-btn" href="#"><i class="fa fa-shopping-bag"></i>Add to Cart</a></div>
                                        </td>--%>

                                        <td class="colum price" title="Price">
                                            <p class="price-detail">
                                                <span class="off-price">₹<%# Eval("discountprice") %></span>
                                                <%--   <span class="old-price">₹<%# Eval("unitPrice") %></span>--%>
                                                <%--<p style="margin-bottom: 0;">Total</p>
                                                <span class="special-price">₹<%# Convert.ToDecimal(Eval("discountprice"))* Convert.ToDecimal(Eval("qty")) %></span>--%>
                                            </p>
                                            <div class="moveWishlist">

                                                 <asp:LinkButton ID="lnkaddtocart" runat="server" CssClass="btn theme-btn" OnClick="lnkaddtocart_Click">
                                    <i class="fa fa-shopping-bag"></i> Add to Cart
                                </asp:LinkButton>

                                               

                                            </div>
                                            <%--<div class="moveWishlist"><a href="#"><i class="fa fa-heart-o"></i>Move to WishList</a></div>--%>
                                        </td>

                                    </tr>

                                </ItemTemplate>
                            </asp:Repeater>



                        </table>
                    </div>

                    <div class="col-md-offset-0 col-md-4 col-sm-offset-2 col-sm-8">
                    </div>
                </div>




            </div>

            <div style="display: none; text-align: center;" runat="server" id="divEmptyCart" class="divEmptyCart">
                Your Wishlist is Empty !!<br />
                <br />

                <a class="btn" href="index.html">Continue Shopping</a>
            </div>
        </div>
    </section>



</asp:Content>
