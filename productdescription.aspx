<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="productdescription.aspx.cs" Inherits="productdescription" %>


<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">

    <script>

       


        function checkvariation() {

            debugger;
            var chk = 0;

            if ($(".chkvar").val() == undefined || $(".chkvar").val() == "undefined") {

                return true;
            }

            $('.chkvar tbody tr td input:radio:checked').each(function () {
                //alert(this.value);
                chk = 1;
            });


            //$(".chkvar").find('tbody tr td input:radio').prop('checked', true);

            if (chk == 1) {

                return true;
            }
            else {
                //$(".error").removeAttr("style");
                //$(".error").html("");
                //$(".error").html("<i class='fa fa-exclamation-circle'></i> Please choose one variation");
                $(".error").fadeIn(300);
                return false;
            }
        }



    </script>

    <style type="text/css">
        .Star {
            background-image: url(https://www.SStyleFactory.com/images/Star.png);
            height: 30px;
            width: 30px;
        }

        .WaitingStar {
            background-image: url(https://www.SStyleFactory.com/images/FilledStar.png);
            height: 30px;
            width: 30px;
        }

        .FilledStar {
            background-image: url(https://www.SStyleFactory.com/images/FilledStar.png);
            height: 30px;
            width: 30px;
        }

        .manualdatacss {
            font-size: 12px;
            font-weight: normal;
            color: #222;
        }

        .rating {
            margin: 10px 0;
            display: table;
        }

        .rpara {
            margin-bottom: 0px;
            line-height: 22px;
        }

        .review-box {
            height: 300px;
            overflow: auto;
        }

        h2.heading {
            font-family: 'lucida_brightregular';
        }
    </style>

    <link href='<%=Page.ResolveUrl("magiczoomplus/magiczoomplus.css")%>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("magiczoomplus/magiczoomplus.js")%>'></script>



    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href='<%= Page.ResolveUrl("index.html") %>'>Home</a></li>
                <li><a id="aheadcollection" runat="server"></a></li>
                <li>
                    <asp:Literal ID="ltrheadcategory" runat="server"></asp:Literal>
                </li>
            </ul>
        </div>
    </div>

<%--    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>--%>

       

    <section class="section section-padding">
        <div class="container">


            <div class="single-product">
                <div class="row">
                    <div class="col-md-6 left-sticky">
                        <div class="single-product-img">
                            <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="add-to-cart proaddtocart" OnClientClick="return checkvariation();" OnClick="lnkaddtocart_Click">
                                    <i class="fa fa-shopping-bag"></i>Add to Cart
                                </asp:LinkButton>--%>

                            <div class="add-to-wishlist">
                                <asp:LinkButton ID="lnkaddtowish" runat="server" Text="Add to Wishlist" OnClick="lnkaddtowish_Click" >
                                    <i class="fa fa-heart-o"></i>
                                </asp:LinkButton>
                                

                            </div>

                            <div class="products-item-img">
                                <%-- <img id="currentImg" runat="server" />--%>

                                <a class="MagicZoomPlus" id="mainhref" href="<%=Page.ResolveUrl("upload/products/large/"+img1) %>" rel="show-title: bottom; selectors-class: Active; zoom-position: inner;  ">
                                    <%--<img runat="server" id="img" name="image" itemprop="image" src="#" />--%>
                                    <asp:Literal ID="lt_img" runat="server"></asp:Literal>
                                </a>

                            </div>

                            <div class="slider-nav products-thumb-img">
                                <%-- <div class="item">
                                    <img id="currentImg1" runat="server" onclick="showImage1(this.src)" data-pin-nopin="true">
                                </div>--%>
                                <div class="item">
                                   <%-- <a href=' <%=Page.ResolveUrl("upload/products/large/"+img1) %>' rev='<%=Page.ResolveUrl("upload/products/large/"+img1) %>' rel="zoom-id:mainhref" class="Selector">
                                        <img src='<%=(Page.ResolveUrl("upload/products/small/"+img1))%>' class="img-responsive smallimg" onerror="this.src='<%=Page.ResolveUrl("images/noimage.jpg")%>'" />
                                    </a>--%> 
                                    
                                    <asp:Literal ID="ltrimg" runat="server"></asp:Literal>

                                    
                                </div>
                                <asp:Repeater ID="rptthumbimg" runat="server">
                                    <ItemTemplate>
                                        <div class="item">
                                            <%--<img src='<%# Page.ResolveUrl("upload/products/othersmall/") + Eval("imagename") %>' onclick="showImage('<%# Page.ResolveUrl("upload/products/otherlarge/") + Eval("imagename") %>')" data-pin-nopin="true">--%>
                                            <a href='<%# Page.ResolveUrl("upload/products/otherlarge/" +Eval("imagename")) %>' rev='<%# Page.ResolveUrl("upload/products/otherlarge/" +Eval("imagename")) %>' rel="zoom-id:mainhref" class="Selector">
                                                <img src='<%# (Page.ResolveUrl("upload/products/othersmall/" +Eval("imagename"))) %>' onerror="this.src='<%=Page.ResolveUrl("upload/products/thumb-noimage.jpg")%>'" class="img-responsive smallimg "></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>


                                <%-- <asp:Repeater ID="rptSmallImg" runat="server">
                                    <ItemTemplate>
                                        <a href='<%#Page.ResolveUrl("https://www.jovifashion.com/upload/product/" +Container.DataItem.ToString()) %>' rev='<%# Page.ResolveUrl("https://www.jovifashion.com/upload/product/" +Container.DataItem.ToString()) %>' rel="zoom-id:mainhref" class="Selector">
                                            <img src='<%# (Page.ResolveUrl( "https://www.jovifashion.com/upload/product/" +Container.DataItem.ToString())) %>' alt='<%= ltrlTitle.Text+" By Jovi Fashion" %>' title='<%= ltrlTitle.Text %>' onerror="this.src='<%=Page.ResolveUrl("upload/product/thumb-noimage.jpg")%>'" class="img-responsive smallimg "></a>
                                    </ItemTemplate>
                                </asp:Repeater>--%>



                                <%--<div class="item"><img src="images/products/img4.jpeg" onclick="showImage('images/products/img4.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img2.jpeg" onclick="showImage('images/products/product-img2.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img3.jpeg" onclick="showImage('images/products/product-img3.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img4.jpeg" onclick="showImage('images/products/product-img4.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/img4.jpeg" onclick="showImage('images/products/img4.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img2.jpeg" onclick="showImage('images/products/product-img2.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img3.jpeg" onclick="showImage('images/products/product-img3.jpeg');" data-pin-nopin="true"></div>
                                <div class="item"><img src="images/products/product-img4.jpeg" onclick="showImage('images/products/product-img4.jpeg');" data-pin-nopin="true"></div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="text">
                            <h2 class="title">
                                <asp:Literal ID="ltrtitle" runat="server"></asp:Literal>
                                <span class="item-no">SKU: #<asp:Literal ID="ltrsku" runat="server"></asp:Literal></span>
                            </h2>


                            <asp:HiddenField ID="hfproid" runat="server" />

                            <%--    <div class="product-reviews-summary">
                                <div class="rating-summary">
                                    <i class="fa fa-star active"></i>
                                    <i class="fa fa-star active"></i>
                                    <i class="fa fa-star active"></i>
                                    <i class="fa fa-star active"></i>
                                    <i class="fa fa-star"></i>
                                </div>
                                <div class="reviews-actions">
                                    <a href="#">6 Ratings and 3 Reviews</a>
                                    <a href="#" class="view">Add Your Review</a>
                                </div>
                            </div>--%>

                            <div class="price-box">
                                <p class="price-detail">
                                    <span class="special-price"><i class="fa fa-inr"></i>
                                        <asp:Literal ID="ltrofferprice" runat="server"></asp:Literal></span>
                                    <span class="old-price"><i class="fa fa-inr"></i>
                                        <asp:Literal ID="ltrmrp" runat="server"></asp:Literal></span>
                                    <span class="off-price">off
                                        <asp:Literal ID="ltroffamt" runat="server"></asp:Literal></span>
                                </p>
                            </div>
                            <%--<div class="availablety">
                                <ul id="travlsize" runat="server">
                                    <li>Available Size</li>
                                    <li><asp:Literal ID="ltravlsize" runat="server"></asp:Literal></li>
                                </ul>
                            
                                <ul id="travlcolor" runat="server">
                                    <li>Available colour</li>
                                    <li><asp:Literal ID="ltravlcolors" runat="server"></asp:Literal></li>
                                </ul>
                            </div>--%>

                            <div class="select-color" id="dvvariation" runat="server">
                                <h4>Available Variations <span class="info" style="color: green;"><i class="fa fa-info-circle"></i> Please choose one variation</span></h4>
                                <label class="error alert-danger" style="display: none; width: 100%;"><i class='fa fa-exclamation-circle'></i> Please choose one variation</label>
                                <div class="colors">


                                    <asp:RadioButtonList ID="rptvariation" CssClass="chkvar" ToolTip="Size - Color Combination" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                                    </asp:RadioButtonList>




                                </div>
                            </div>


                            <%--  <div class="fillDetail">
                            <asp:TextBox ID="txtuserdescription" runat="server" CssClass="form-control" placeholder="Please specify your product requirements like size, color etc."></asp:TextBox>
                            <br />
                            </div>--%>

                            <div class="button-box">
                                <%--<a type="button" title="Add to Cart" onclick="addToCart()" class="btn btn-lg btn-buy" id="button-cart"><span><i class="fa fa-shopping-cart leftside"></i>ADD TO CART</span></a>--%>

                                <asp:LinkButton ID="lnkaddtocart" runat="server" CssClass="add-to-cart proaddtocart" OnClientClick="return checkvariation();" OnClick="lnkaddtocart_Click">
                                    <i class="fa fa-shopping-bag"></i>Add to Cart
                                </asp:LinkButton>

                                <%-- <a href="javascript:void(0);" id="btncart" runat="server" class="add-to-cart proaddtocart" onclick="addToCart()"><i class="fa fa-shopping-bag"></i>Add to Cart</a>--%>
                                <%-- <a href="javascript:void(0);" class="buy-now"><i class="fa fa-shopping-basket"></i>Buy Now</a>--%>


                                <asp:LinkButton ID="lnkbuynow" runat="server" CssClass="buy-now" OnClick="lnkbuynow_Click">
                                    <i class="fa fa-shopping-basket"></i>Buy Now
                                </asp:LinkButton>

                            </div>
                        </div>















                        <div class="description">



                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab_a" data-toggle="tab">Description</a></li>
                                <li><a href="#tab_b" data-toggle="tab">Specification</a></li>
                                 <li><a href="#tab_c" data-toggle="tab">Product Reviews</a></li>
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_a">
                                    <div class="text">
                                        <asp:Literal ID="ltrdescription" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab_b">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>Collection</td>
                                                <td>
                                                    <asp:Literal ID="ltrcollectionname" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Category</td>
                                                <td>
                                                    <asp:Literal ID="ltrcategory" runat="server"></asp:Literal></td>
                                            </tr>
                                            <tr>
                                                <td>Material</td>
                                                <td>
                                                    <asp:Literal ID="ltrmaterial" runat="server"></asp:Literal></td>
                                            </tr>
                                            <tr id="trsize" runat="server">
                                                <td>Size</td>
                                                <td>
                                                    <asp:Literal ID="ltrsize" runat="server"></asp:Literal></td>
                                            </tr>
                                            <%--<tr >
                                            <td>Available Sizes</td>
                                            <td>
                                                </td>
                                        </tr>--%>
                                            <tr id="trcolor" runat="server">
                                                <td>Color</td>
                                                <td>
                                                    <asp:Literal ID="ltrcolor" runat="server"></asp:Literal></td>
                                            </tr>
                                            <%--<tr>
                                            <td>Available Colors</td>
                                            <td>
                                                </td>
                                        </tr>--%>

                                            <asp:Repeater runat="server" ID="rep">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("FeatureName") %></td>
                                                        <td><%#Eval("FeatureValue") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>

                                <div class="tab-pane" id="tab_c">
                                <div class="customerRating">


                                    <asp:Repeater ID="rptreviews" runat="server">
                                        <ItemTemplate>
                                            <div class="review-item">
                                        <p><%# Eval("Review") %></p>
                                        <div class="review-info">
                                           <%-- <span class="rating-summary">
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star"></i>
                                            </span>--%>
                                             <cc1:Rating ID="ratingget" runat="server"
                                                         StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                         FilledStarCssClass="FilledStar" CssClass="rating" ReadOnly="true" Enabled="false" CurrentRating='<%# Eval("Rating") %>'>
                                                      </cc1:Rating>
                                            <span class="reviewby"><%# Eval("Name") %> </span>
                                            <span class="time"><%# Convert.ToDateTime(Eval("addate")).ToString("dd MMM, yyyy") %></span>
                                        </div>
                                    </div>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                  <%--  <div class="review-item">
                                        <p>Good one from FabFashion...!!</p>
                                        <div class="review-info">
                                            <span class="rating-summary">
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star"></i>
                                            </span>
                                            <span class="reviewby">Sumit </span>
                                            <span class="time">30 Aug, 2016</span>
                                        </div>
                                    </div>



                                    <div class="review-item">
                                        <p>Good one from FabFashion...!!</p>
                                        <div class="review-info">
                                            <span class="rating-summary">
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star"></i>
                                            </span>
                                            <span class="reviewby">Sumit </span>
                                            <span class="time">30 Aug, 2016</span>
                                        </div>
                                    </div>
                                    <div class="review-item">
                                        <p>Good one from FabFashion...!!</p>
                                        <div class="review-info">
                                            <span class="rating-summary">
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star"></i>
                                            </span>
                                            <span class="reviewby">Sumit </span>
                                            <span class="time">30 Aug, 2016</span>
                                        </div>
                                    </div>
                                    <div class="review-item">
                                        <p>they are simply supop cool and as there is orange in them they will be my favourite obviously.</p>
                                        <div class="review-info">
                                            <span class="rating-summary">
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star active"></i>
                                                <i class="fa fa-star"></i>
                                                <i class="fa fa-star"></i>
                                            </span>
                                            <span class="reviewby">Karan </span>
                                            <span class="time">6 Jul, 2016</span>
                                        </div>
                                    </div>--%>


                                    

                                </div>
                                <a href="JavaScript:Void(0);" class="add-review btn theme-btn" data-toggle="modal" data-target="#reviewModal">Add Your Review</a>
                            </div>
                            </div>


                        </div>

                        <div class="social-sharing mt-4">   
                        <h4 class="social-sharing-icon">Share this product with :</h4>                    
                        <ul>
                           <%-- <li class="social-sharing-icon">
                                <a href="#" class="social-icon facebook" target="_blank" style="background: #3b5998;"><i class="fa fa-facebook"></i></a>
                            </li>
                            <li class="social-sharing-icon">
                                <a href="#" class="social-icon twitter" target="_blank" style="background: #00acee;"><i class="fa fa-twitter"></i></a>
                            </li>--%>

                            <li class="social-sharing-icon"><a class="social-icon facebook" target="_blank" runat="server" id="fbsharelink" style="background: #3b5998;"><i class="fa fa-facebook"></i></a></li>
                            <li class="social-sharing-icon"><a class="social-icon twitter" target="_blank" runat="server" id="twitersharelink" style="background: #00acee;"><i class="fa fa-twitter"></i></a></li>
                            <li class="social-sharing-icon"><a class="social-icon pinterest" target="_blank" id="pinterestsharelink" runat="server" style="background: #c8232c;"><i class="fa fa-pinterest"></i></a></li>

                        </ul>
                    </div>

                    </div>
                </div>


                <div id="reviewModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">

                        <div class="modal-content">

                            <div class="modal-body">
                                <button type="button" class="close" data-dismiss="modal">
                                    <img src="<%=(Common.GetLatestVersion(Page.ResolveUrl("images/close.png"))) %>" width="12" /></button>

                                <div class="reviewForm">
                                    <h3>Leave Your Review</h3>

                                   <p class="rating-summary">
                                     <cc1:Rating ID="Rating1" runat="server"
                                                         StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                                         FilledStarCssClass="FilledStar" CssClass="rating" CurrentRating="1">
                                                      </cc1:Rating>

                                       </p>

                                   <%-- <p class="rating-summary">


                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                        <i class="fa fa-star"></i>
                                    </p>--%>



                                    <div class="form-group">
                                        <%--<input class="form-control" type="text" placeholder="Your Name" />--%>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="* Your Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf1" runat="server" ControlToValidate="txtname" ForeColor="Red" Display="Dynamic" ValidationGroup="review" SetFocusOnError="true" ErrorMessage="Please fill your name" ></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <%--<textarea class="form-control" type="text" rows="5" placeholder="Tell us what you think..."></textarea>--%>
                                        <asp:TextBox ID="txtreview" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="* Tell us what you think..."></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf2" runat="server" ControlToValidate="txtreview" ForeColor="Red" Display="Dynamic" ValidationGroup="review" SetFocusOnError="true" ErrorMessage="Please fill review" ></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group text-center">
                                        <%--<button class="btn theme-btn" type="button">Submit</button>--%>
                                        <asp:Button ID="btnreviewsubmit" runat="server" CssClass="btn theme-btn" Text="Submit" ValidationGroup="review" OnClick="btnreviewsubmit_Click" />
                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>
                </div>



                <div class="home-banner">
                    <div class="bannerSlider row">
                        <asp:Repeater ID="rptusp" runat="server">
                <ItemTemplate>
                    <div class="item col-md-3">
                        <div class="column">
                            <a href='<%# Eval("url").ToString()=="#" ? "javascript:void(0)" : Eval("Url") %>'>
                                <div class="banner-img">
                                    <img src='<%# Page.ResolveUrl("upload/usp/") + Eval("iconimagename") %>' />
                                </div>
                                <h4><%# Eval("USPTitle") %></h4>
                                <p><%# Eval("USPSubTitle") %></p>
                            </a>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
                     <%--   <div class="item col-md-3">
                            <div class="column">
                                <div class="banner-img">
                                    <img src="<%=(Common.GetLatestVersion(Page.ResolveUrl("images/5.png"))) %>" />
                                </div>
                                <h4>On 1st Order</h4>
                                <p>Get A Free Gift on First Order.</p>
                            </div>
                        </div>
                        <div class="item col-md-3">
                            <div class="column">
                                <div class="banner-img">
                                    <img src="<%=(Common.GetLatestVersion(Page.ResolveUrl("images/1.png"))) %>" />
                                </div>
                                <h4>Free Shipping</h4>
                                <p>For all FabFashion Products.</p>
                            </div>
                        </div>
                        <div class="item col-md-3">
                            <div class="column">
                                <div class="banner-img">
                                    <img src="<%=(Common.GetLatestVersion(Page.ResolveUrl("images/3.png"))) %>" />
                                </div>
                                <h4>Cash on delivery</h4>
                                <p>On all FabFashion products.</p>
                            </div>
                        </div>
                        <div class="item col-md-3">
                            <div class="column">
                                <div class="banner-img">
                                    <img src="<%=(Common.GetLatestVersion(Page.ResolveUrl("images/2.png"))) %>" />
                                </div>
                                <h4>Money back guarantee</h4>
                                <p>100% money back guarante</p>
                            </div>
                        </div>--%>

                    </div>
                </div>


            </div>


            <div class="featured-products" id="relproduct" runat="server">
                <h2>Related Products</h2>
                <div class="products">

                    <asp:Repeater ID="rptrelatedproducts" runat="server" OnItemDataBound="rptrelatedproducts_ItemDataBound">

                        <ItemTemplate>
                            <div class="item product-item">

                                <asp:HiddenField ID="hflatestproid" runat="server" Value='<%# Eval("Id") %>' />

                                <%--<div class="add-to-wishlist" onclick="addtowish('<%# Eval("Id") %>')"><i class="fa fa-heart-o"></i></div>--%>
                                <div class="add-to-wishlist" id="btnaddtowish" runat="server" onclick='<%# "addtowish("+ Eval("id") + ",1,0,0" + ")" %>' ><i id='<%# "iwish" + Eval("Id").ToString() %>' title="Add to Wishlist" class="fa fa-heart-o"></i></div>


                                <div class="item-img">
                                    <a href='<%#Page.ResolveClientUrl(Common.url(Eval("collectionname").ToString())+"/"+Common.url(Eval("categoryname").ToString())+"/"+Common.url( Eval("title").ToString())+"-"+Eval("id") +".html")%>'>
                                        <img class="img-responsive" src='<%# Page.ResolveUrl( "upload/products/small/" + Eval("image").ToString()) %>' /></a>
                                </div>



                                <div class="text">
                                    <span class="title"><a href="product.html"><%# Eval("Title") %></a></span>
                                    <div class="price-box">
                                        <p class="price-detail">
                                            <span class="special-price"><i class="fa fa-inr"></i><%# Convert.ToDecimal(Eval("offerprice")) %></span>
                                            <span class="old-price"><i class="fa fa-inr"></i><%# Convert.ToDecimal(Eval("mrp")) %></span>
                                            <span class="off-price">off <i class="fa fa-inr"></i><%# (Convert.ToDecimal(Eval("mrp")) - Convert.ToDecimal(Eval("offerprice"))).ToString("0") %></span>
                                        </p>
                                    </div>
                                </div>
                                <%--<button class="add-to-cart" onclick="AddToCart('<%# Eval("Id") %>','1','0')"><i class="fa fa-shopping-bag"></i>Add to Cart</button>--%>
                                 <div class="buttonBox">
                                <a href="javascript:void(0);" id="btnaddtocart" runat="server" class='<%# "add-to-cart pro" + Eval("Id").ToString() %>' onclick='<%# "AddToCart("+ Eval("id") + ",1,0,0" + ")" %>'>Add to Cart</a>
                                <a href="javascript:void(0);" class="add-to-cart buy-now" onclick='<%# "BuyNow("+ Eval("id") + ",1,0,0" + ")" %>'><%--<i class="fa fa-shopping-basket"></i> --%>Buy Now</a>
                                     </div>
                                <%--<a class="bottom-line-a"  onclick="AddToCart('<%# Eval("Id") %>','1','0')"><i class="fa fa-shopping-cart"></i> Add to cart</a>--%>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>



                </div>
            </div>


        </div>
    </section>

    <script>
        function addToCart() {

            debugger;
            var Qty = "1";
            var pid = $("#<%=hfproid.ClientID %>").val();
            var rmrks = $("#cp1_txtuserdescription").val();
            AddToCartByDetail(pid, Qty, "0", rmrks);
        }
    </script>

    <script>
        $('.products-thumb-img').slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            vertical: true,
            dots: false,
            verticalSwiping: true,
            prevArrow: "<button type='button' class='slick-prev pull-left'><i class='fa fa-angle-up'></i></button>",
            nextArrow: "<button type='button' class='slick-next pull-right'><i class='fa fa-angle-down'></i></button>",
            responsive: [
            {
                breakpoint: 640,
                settings: {
                    vertical: false,
                    verticalSwiping: false,
                    arrows: false,
                }
            }
            ]
        });
    </script>
    <script>
        (function () {
            var div = document.getElementById("toggle-more");
            var curText = div.innerText;
            div.addEventListener("click", function () {
                if (this.innerText == curText) {
                    div.innerText = "See Less";
                    $(".desc-more").show(300);
                }
                else {
                    div.innerText = curText;
                    $(".desc-more").hide(300);
                }
            }, false);

        })();
    </script>



    <script>
        $('.bannerSlider').slick({
            dots: false,
            infinite: true,
            slidesToShow: 4,
            arrows: false,
            autoplay: false,
            responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 3,
                }
            },
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                }
            },
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1,
                }
            }
            ]
        });
    </script>

            <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>


