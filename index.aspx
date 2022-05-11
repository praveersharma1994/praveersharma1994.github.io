<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="index.aspx.cs" Inherits="index" %>


<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">


    <div class="mainSlider">

        <asp:Repeater ID="rptoffer" runat="server">
        <ItemTemplate>
            <div class="item">
            <a href='<%# Eval("bannerurl").ToString()=="#" ? "javascript:void(0);" : Eval("bannerurl").ToString()  %>'>
                <img class="img1 img-responsive" src='<%# "upload/stripbanner/"+ Eval("BannerImg")+"?"+System.DateTime.Now.Ticks %>' />
                <img class="img2 img-responsive" src='<%# "upload/stripbanner/"+ Eval("BannerImgmobile")+"?"+System.DateTime.Now.Ticks %>' />
            </a>
        </div>
        </ItemTemplate>
    </asp:Repeater>

      <%--  <div class="item">
            <img class="img-responsive" src="images/imgpsh_fullsize_slide.jpg" />
        </div>
        <div class="item">
            <img class="img-responsive" src="images/imgpsh_fullsize_slide2.jpg" />
        </div>--%>
    </div>

    


    <div class="home-banner animate fadeIn">
        <div class="bannerSlider row">

            <asp:Repeater ID="rptusp" runat="server">
                <ItemTemplate>
                    <div class="item col-md-3">
                        <div class="column">
                            <a href='<%# Eval("url").ToString()=="#" ? "javascript:void(0)" : Eval("Url") %>'>
                                <div class="banner-img">
                                    <img src='<%# "upload/usp/" + Eval("iconimagename") %>' />
                                </div>
                                <h4><%# Eval("USPTitle") %></h4>
                                <p><%# Eval("USPSubTitle") %></p>
                            </a>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <%--<div class="item col-md-3">
                <div class="column">
                    <div class="banner-img">
                        <img src="images/5.png" /></div>
                    <h4>On 1st Order</h4>
                    <p>Get A Free Gift on First Order.</p>
                </div>
            </div>
            <div class="item col-md-3">
                <div class="column">
                    <div class="banner-img">
                        <img src="images/1.png" /></div>
                    <h4>Free Shipping</h4>
                    <p>For all FabFashion Products.</p>
                </div>
            </div>
            <div class="item col-md-3">
                <div class="column">
                    <div class="banner-img">
                        <img src="images/3.png" /></div>
                    <h4>Cash on delivery</h4>
                    <p>On all FabFashion products.</p>
                </div>
            </div>
            <div class="item col-md-3">
                <div class="column">
                    <div class="banner-img">
                        <img src="images/2.png" /></div>
                    <h4>Money back guarantee</h4>
                    <p>100% money back guarante</p>
                </div>
            </div>--%>
        </div>
    </div>

    <%--<div class="home-banner hide-md-m animate fadeIn">
        <div class="row">
            <div class="item col-md-3">
                <div class="column">
                <div class="banner-img"><img src="images/5.png" /></div>
                <h4>On 1st Order</h4>
                <p>Get A Free Gift on First Order.</p>
                </div>
            </div>      
            <div class="item col-md-3">
                <div class="column">
                <div class="banner-img"><img src="images/1.png" /></div>
                <h4>Free Shipping</h4>
                <p>For all FabFashion Products.</p>
                </div>
            </div>
            <div class="item col-md-3">
                <div class="column">
                <div class="banner-img"><img src="images/3.png" /></div>
                <h4>Cash on delivery</h4>
                <p>On all FabFashion products.</p>
                </div>
            </div>
            <div class="item col-md-3">
                <div class="column">
                <div class="banner-img"><img src="images/2.png" /></div>
                <h4>Money back guarantee</h4>
                <p>100% money back guarante</p>
                </div>
            </div>
            
        </div>
    </div>--%>
    <!--
    ==================== Home Products  -->

    <section class="section section-padding">

        <div class="products home-products">

            <asp:Literal ID="ltrprogallery" runat="server"></asp:Literal>


            <%-- <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img1.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹210</span>
                            <span class="old-price">₹2199</span>
                            <span class="off-price">off 90%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img2.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Setr</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹559</span>
                            <span class="old-price">₹3499</span>
                            <span class="off-price">off 84%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img3.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Zaveri Pearls Zinc Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹261</span>
                            <span class="old-price">₹1290</span>
                            <span class="off-price">off 79%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img4.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">OUTLOOK QUEENS Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹224</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 82%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img5.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">CRYSTLE Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹319</span>
                            <span class="old-price">₹999</span>
                            <span class="off-price">off 85%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img6.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Geeta Collection Hand-held Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹281</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 75%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                
                <div class="item product-item banner-img">
                    <div class="img-box"><img class="img-responsive" src="images/fabsitebanner1.jpg" /></div>
                </div>

                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img7.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Daylfonos Antique Shaped Cigarette Lighter Pocket Light...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹239</span>
                            <span class="old-price">₹799</span>
                            <span class="off-price">off 70%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img8.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Captiver Batten set of 3 wenge wooden Décor shelves mou...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹889</span>
                            <span class="old-price">₹1799</span>
                            <span class="off-price">off 50%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img9.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Good Knight Activ+ Mosquito Vaporiser Refill</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹71</span>
                            <span class="old-price">₹72</span>
                            <span class="off-price">off ₹1</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img10.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Philips QT3310/15 Cordless Trimmer for Men</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹1099</span>
                            <span class="old-price">₹1360</span>
                            <span class="off-price">off 13%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img12.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Bajaj Pluto 500 W Mixer Grinder</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹1200</span>
                            <span class="old-price">₹1600</span>
                            <span class="off-price">off 25%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img1.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹210</span>
                            <span class="old-price">₹2199</span>
                            <span class="off-price">off 90%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
              
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img2.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Setr</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹559</span>
                            <span class="old-price">₹3499</span>
                            <span class="off-price">off 84%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img3.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Zaveri Pearls Zinc Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹261</span>
                            <span class="old-price">₹1290</span>
                            <span class="off-price">off 79%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img4.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">OUTLOOK QUEENS Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹224</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 82%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img5.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">CRYSTLE Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹319</span>
                            <span class="old-price">₹999</span>
                            <span class="off-price">off 85%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img6.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Geeta Collection Hand-held Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹281</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 75%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img7.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Daylfonos Antique Shaped Cigarette Lighter Pocket Light...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹239</span>
                            <span class="old-price">₹799</span>
                            <span class="off-price">off 70%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img8.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Captiver Batten set of 3 wenge wooden Décor shelves mou...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹889</span>
                            <span class="old-price">₹1799</span>
                            <span class="off-price">off 50%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img9.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Good Knight Activ+ Mosquito Vaporiser Refill</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹71</span>
                            <span class="old-price">₹72</span>
                            <span class="off-price">off ₹1</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img10.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Philips QT3310/15 Cordless Trimmer for Men</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹1099</span>
                            <span class="old-price">₹1360</span>
                            <span class="off-price">off 13%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img1.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹210</span>
                            <span class="old-price">₹2199</span>
                            <span class="off-price">off 90%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
              
                <div class="item product-item banner-img">
                    <div class="img-box"><img class="img-responsive" src="images/fabsitebanner4.jpg" /></div>
                </div>

                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img2.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Setr</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹559</span>
                            <span class="old-price">₹3499</span>
                            <span class="off-price">off 84%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img3.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Zaveri Pearls Zinc Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹261</span>
                            <span class="old-price">₹1290</span>
                            <span class="off-price">off 79%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img4.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">OUTLOOK QUEENS Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹224</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 82%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img5.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">CRYSTLE Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹319</span>
                            <span class="old-price">₹999</span>
                            <span class="off-price">off 85%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img6.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Geeta Collection Hand-held Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹281</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 75%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img7.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Daylfonos Antique Shaped Cigarette Lighter Pocket Light...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹239</span>
                            <span class="old-price">₹799</span>
                            <span class="off-price">off 70%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
               

                <div class="item product-item banner-img">
                    <div class="img-box"><img class="img-responsive" src="images/fabsitebanner5.jpg" /></div>
                </div>

                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img8.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Captiver Batten set of 3 wenge wooden Décor shelves mou...</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹889</span>
                            <span class="old-price">₹1799</span>
                            <span class="off-price">off 50%</span>
                            </p>
                        </div>
                        <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                    </div>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img9.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Good Knight Activ+ Mosquito Vaporiser Refill</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹71</span>
                            <span class="old-price">₹72</span>
                            <span class="off-price">off ₹1</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img10.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Philips QT3310/15 Cordless Trimmer for Men</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹1099</span>
                            <span class="old-price">₹1360</span>
                            <span class="off-price">off 13%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img1.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹210</span>
                            <span class="old-price">₹2199</span>
                            <span class="off-price">off 90%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img2.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Atasi International Alloy Jewel Setr</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹559</span>
                            <span class="old-price">₹3499</span>
                            <span class="off-price">off 84%</span>
                            </p>
                        </div>
                        <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                    </div>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img3.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">Zaveri Pearls Zinc Jewel Set</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹261</span>
                            <span class="old-price">₹1290</span>
                            <span class="off-price">off 79%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>
                <div class="item product-item">
                    <div class="add-to-wishlist"><i class="fa fa-heart-o"></i></div>
                    <div class="item-img"><a href="#"><img class="img-responsive" src="images/products/img4.jpeg" /></a></div>
                    <div class="text">
                        <span class="title"><a href="#">OUTLOOK QUEENS Shoulder Bag</a></span>
                        <div class="price-box">
                            <p class="price-detail">
                            <span class="special-price">₹224</span>
                            <span class="old-price">₹1299</span>
                            <span class="off-price">off 82%</span>
                            </p>
                        </div>
                    </div>
                    <button class="add-to-cart"><i class="fa fa-shopping-bag"></i> Add to Cart</button>
                </div>--%>
        </div>

    </section>







    <script>
        $('.mainSlider').slick({
            dots: true,
            infinite: true,
            slidesToShow: 1,
            arrows: false,
            fade: true,
            autoplay: true,
            
        });
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




</asp:Content>
