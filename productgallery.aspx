<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Clientmaster.master" CodeFile="productgallery.aspx.cs" Inherits="productgallery" %>


<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cp1">


    <script>


        function digits(obj, e, allowDecimal, allowNegative) {


            var key; var isCtrl = false; var keychar; var reg; if (window.event) { key = e.keyCode; isCtrl = window.event.ctrlKey } else if (e.which) { key = e.which; isCtrl = e.ctrlKey; }
            if (isNaN(key)) return true; keychar = String.fromCharCode(key); if (key == 8 || isCtrl) { return true; }
            reg = /\d/; var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false; var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false; return isFirstN || isFirstD || reg.test(keychar);
        }

        function showgobutton() {

            // document.getElementById("cp1_lnknext").innerText = "GO";
            // document.getElementById("cp1_lnknext").style.fontSize = "12px";
            // document.getElementById("cp1_lnknext").style.backgroundColor = "#383838";



            document.getElementById("hf1").value = "GO";
            document.getElementById("cp1_lnkgo").style.display = "inline-block";

        }

        //function hidego() {
        //    //document.getElementById("cp1_lnknext").innerHTML = "&raquo;";
        //    //document.getElementById("cp1_lnknext").style.fontSize = "20px";

        //    document.getElementById("cp1_lnknext").style.display = "block";
        //    document.getElementById("cp1_lnkgo").style.display = "none";


        //}
    </script>


    <asp:HiddenField ID="hddminvalue" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hddmaxvalue" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hduserid" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hddnoproducts" ClientIDMode="Static" runat="server" Value="20" />

    <asp:HiddenField ID="hfcurrency" ClientIDMode="Static" runat="server" Value="INR" />

    <script src='<%=(Common.GetLatestVersion(Page.ResolveUrl("js/gallery.js"))) %>'></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js"></script>



    <div class="breadcrumb-bar">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href='<%= Page.ResolveUrl("index.html") %>'>Home</a></li>
                <li>
                    <asp:Literal ID="asubcategory" runat="server"></asp:Literal>
                </li>
            </ul>
        </div>
    </div>




    <section class="section section-padding">
        <div class="container">

            <main class="main-section content">
                

                <div id="sidebar" class="sidebar category-sidebar">
                    <div id="aside">

                        <div class="topbuttonbox">
                            <asp:LinkButton ID="lnkapplyfilter" runat="server" CssClass="btn theme-btn" Text="Apply Filter" OnClick="lnkapplyfilter_Click"></asp:LinkButton>
                            <a class="clearfilter" href='<%= Page.Request.Url.ToString().ToLower().Replace(".aspx",".html") %>' ><i class="fa fa-refresh"></i> Clear Filter</a>
                        </div>

                        <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#moreList">Category</h4>
                            <ul id="moreList" class="family collapse in">

                                 <asp:CheckBoxList ID="repfamily"  runat="server" CssClass="categoryfilter">
                                        
                                     </asp:CheckBoxList>


                                <%--<asp:Repeater ID="repfamily" runat="server">
                                            <ItemTemplate>
                                                <li>

                                                     <label for='chkfamily<%# Container.ItemIndex+1 %>'>
                                                    

                                                        <input class="checkbox" id='chkfamily<%# Container.ItemIndex+1 %>' type="checkbox" onchange="filter('category','<%# Eval("categoryname") %>',this)" value='<%# Eval("categoryname") %>' />
                                                    
                                                          

                                                   
                                                      <span><%# Eval("categoryname").ToString() %></span>
                                                    </label>
                                                </li>
                                            </ItemTemplate>

                                        </asp:Repeater>--%>

                                <%--<li class="active"><a href="#">All</a></li>
                                <li><a href="#">gents wallet</a></li>
                                <li><a href="#">ladies wallet</a></li>
                                <li><a href="#">slings</a></li>
                                <li><a href="#">shoulder bag</a></li>--%>
                            </ul>
                        </div>

                      <%--  <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#cp1_chkgender">Gender</h4>

                            <asp:CheckBoxList ID="chkgender" OnSelectedIndexChanged="chkgender_SelectedIndexChanged"  AutoPostBack="true" runat="server" CssClass="categoryfilter">
                                        
                                     </asp:CheckBoxList>

                         
                        </div>--%>

                        <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#cp1_chkPrice">Price</h4>

                            <asp:CheckBoxList ID="chkPrice"  runat="server" CssClass="categoryfilter"></asp:CheckBoxList>
                           <%-- <label for="amount">Range:</label>


                            <p>
                                               
                                                <input type="text" id="amount" readonly="true" />
                                            </p>
                                            <div id="slider-range"></div>--%>


                           
                        </div>

                        <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#cp1_rptmaterial">Material</h4>

                            <asp:CheckBoxList ID="rptmaterial"  runat="server" CssClass="categoryfilter">
                                        
                                     </asp:CheckBoxList>

                            <%--<ul id="byMaterial" class="material collapse in">
                                   <asp:Repeater ID="rptmaterial" runat="server">
                                            <ItemTemplate>
                                                <li>

                                                     <label for='chkfamily<%# Container.ItemIndex+1 %>'>
                                                    

                                                        <input class="checkbox" id='chkmaterial<%# Container.ItemIndex+1 %>' type="checkbox" onchange="filter('material','<%# Eval("Material") %>',this)" value='<%# Eval("Material") %>' />
                                                    
                                                   
                                                      <span><%# Eval("Material").ToString() %></span>
                                                    </label>
                                                </li>
                                            </ItemTemplate>
                                       </asp:Repeater>
                                      </ul>--%>

                              
                            
                        </div>

                        <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#cp1_rptsize">Size</h4>

                            <asp:CheckBoxList ID="rptsize"  runat="server" CssClass="categoryfilter">
                                        
                                     </asp:CheckBoxList>

                            <%--<ul id="bysize" class="collapse in">
                                <asp:Repeater ID="rptsize" runat="server">
                                            <ItemTemplate>
                                                <li>

                                                     <label for='chkfamily<%# Container.ItemIndex+1 %>'>
                                                    

                                                        <input class="checkbox" id='chksize<%# Container.ItemIndex+1 %>' type="checkbox" onchange="filter('size','<%# Eval("sizename") %>',this)" value='<%# Eval("sizename") %>' />
                                                    
                                                   
                                                      <span><%# Eval("sizename").ToString() %></span>
                                                    </label>
                                                </li>
                                            </ItemTemplate>
                                       </asp:Repeater>
                            </ul>--%>
                        </div>

                        <div class="filter-section">
                            <h4 class="title" data-toggle="collapse" data-target="#cp1_rptcolor">Color</h4>

                            <asp:CheckBoxList ID="rptcolor"  runat="server" CssClass="categoryfilter">
                                        
                                     </asp:CheckBoxList>

                            <%--<ul id="bycolor" class="collapse in">
                                 <asp:Repeater ID="rptcolor" runat="server">
                                            <ItemTemplate>
                                                <li>

                                                     <label for='chkfamily<%# Container.ItemIndex+1 %>'>
                                                    

                                                        <input class="checkbox" id='chkcolor<%# Container.ItemIndex+1 %>' type="checkbox" onchange="filter('color','<%# Eval("colorname") %>',this)" value='<%# Eval("colorname") %>' />
                                                    
                                                   
                                                      <span><%# Eval("colorname").ToString() %></span>
                                                    </label>
                                                </li>
                                            </ItemTemplate>
                                       </asp:Repeater>
                            </ul>--%>
                        </div>
                    </div>
                </div>

                <div class="category-products main-content">

                    <div class="shortBy-area">
                        <ul class="shortby">
                            <li>Sort By: </li>
                            <li>
                                <asp:DropDownList ID="drpsorting" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpsorting_SelectedIndexChanged">
                                    <asp:ListItem Text="New Arrivals" Value="newarrivals" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Price: Low to High" Value="lth"></asp:ListItem>
                                    <asp:ListItem Text="Price: High to Low" Value="htl"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<select class="form-control">
                                <option>New Arrivals</option>
                                <option>Price: Low to High</option>
                                <option>Price: High to Low</option>
                                </select>--%>

                            </li>
                            
                        </ul>

                         <ul class="shortby">
                            <li>View Products on Page: </li>
                            <li>
                                <asp:DropDownList ID="drppage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppage_SelectedIndexChanged" >
                                    <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<select class="form-control">
                                <option>New Arrivals</option>
                                <option>Price: Low to High</option>
                                <option>Price: High to Low</option>
                                </select>--%>

                            </li>
                            
                        </ul>

                         <%--<asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="1">
                                            <Fields>
                                                <asp:NumericPagerField NextPageText="&gt;&gt;" NumericButtonCssClass="color_text"
                                                    NextPreviousButtonCssClass="next" ButtonCount="4" PreviousPageText="&lt;&lt;" />
                                            </Fields>
                                        </asp:DataPager>--%>

                    </div>


                     <asp:UpdatePanel ID="up1" runat="server" RenderMode="Inline" >
                                 <ContentTemplate>

                    <div class="products">

                         

                        <asp:Repeater ID="rptgallery" runat="server" OnItemDataBound="rptgallery_ItemDataBound">
                                        <ItemTemplate>
                                            
                                            <div class="item product-item">

                             <asp:HiddenField ID="hflatestproid" runat="server" Value='<%# Eval("Id") %>' />

                            <div class="add-to-wishlist" id="btnaddtowish" runat="server" onclick='<%# "addtowish("+ Eval("id") + ",1,0,0" + ")" %>' ><i id='<%# "iwish" + Eval("Id").ToString() %>' title="Add to Wishlist" class="fa fa-heart-o"></i></div>


                            <div class="item-img"><a href='<%# Page.ResolveClientUrl(Common.url(Eval("collectionname").ToString())+"/"+Common.url(Eval("categoryname").ToString())+"/"+Common.url( Eval("title").ToString())+"-"+Eval("id") +".html")%>' ><img class="img-responsive" src='<%# Page.ResolveUrl( "upload/products/small/" + Eval("image").ToString()) %>'  /></a></div>



                            <div class="text">
                                <span class="title"><a href='<%#Page.ResolveClientUrl(Common.url(Eval("collectionname").ToString())+"/"+Common.url(Eval("categoryname").ToString())+"/"+Common.url( Eval("title").ToString())+"-"+Eval("id") +".html")%>'><%# Eval("Title") %></a></span>
                                <div class="price-box">
                                    <p class="price-detail">
                                    <span class="special-price"><i class="fa fa-inr"></i><%# Convert.ToDecimal(Eval("offerprice")) %></span>
                                    <span class="old-price"><i class="fa fa-inr"></i><%# Convert.ToDecimal(Eval("mrp")) %></span>
                                    <span class="off-price">off <%# Math.Round(((Convert.ToDecimal(Eval("mrp")) - Convert.ToDecimal(Eval("offerprice")))/Convert.ToDecimal(Eval("mrp")))*100) %>%</span>
                                    </p>
                                </div>
                            </div>
                            <div class="buttonBox">
                            <a href="javascript:void(0);" id="btnaddtocart" runat="server" class='<%# "add-to-cart pro" + Eval("Id").ToString() %>'  onclick='<%# "AddToCart("+ Eval("id") + ",1,0,0" + ")" %>' ><%--<i class="fa fa-shopping-bag"></i> --%>Add to Cart</a>
                            <a href="javascript:void(0);" class="add-to-cart buy-now" onclick='<%# "BuyNow("+ Eval("id") + ",1,0,0" + ")" %>'><%--<i class="fa fa-shopping-basket"></i> --%>Buy Now</a>
                            </div>
                            <%--<a class="bottom-line-a"  onclick="AddToCart('<%# Eval("Id") %>','1','0')"><i class="fa fa-shopping-cart"></i> Add to cart</a>--%>
                        </div>

                                        </ItemTemplate>
                                    </asp:Repeater>
                     
                        
               
                    </div>
                   <div class="text-center" id="dvnorecord" runat="server">No Record(s) found !!</div>

                                     <div class="drPagination text-center" id="dvpaging" runat="server">
                                         <div class="hwepagination">
                                        <%--<a class="pagebutton" href="#">&laquo;</a>--%>
                                        <asp:LinkButton ID="lnkpre" runat="server" CssClass="prv pagebutton" OnClick="lnkpre_Click" ><i class="fa fa-angle-left"></i></asp:LinkButton>
                                             <div class="innerarea">
                                        <span>1</span>
                                         <span><i class="fa fa-ellipsis-h"></i>&nbsp;</span>
                
                                            <asp:TextBox ID="ltrcurpage" runat="server" CssClass="currentpage" autocomplete="off" onclick="showgobutton();" onkeypress="return digits(this,event,false,false);" MaxLength="3" ></asp:TextBox>
               
                                        <span>&nbsp;<i class="fa fa-ellipsis-h"></i></span>
                                        <a href="javascript:void(0)">
                                            <asp:Literal ID="ltrtotpage" runat="server" ></asp:Literal>
                                        </a>
                                       <%-- <a class="pagebutton" href="#">&raquo;</a>--%>
                                                    <asp:HiddenField ID="hf1" runat="server" ClientIDMode="Static" />
                                                  </div>
                                        <asp:LinkButton ID="lnknext" runat="server" CssClass="nxt pagebutton" OnClick="lnknext_Click"  ><i class="fa fa-angle-right"></i></asp:LinkButton>
                                                                 <asp:LinkButton ID="lnkgo" runat="server" CssClass="go pagebutton" OnClick="lnkgo_Click" >Go</asp:LinkButton>
                                               
                                    </div>
                                   </div>

                                     </ContentTemplate>
                         </asp:UpdatePanel>
                </div>

            </main>
        </div>
    </section>



    <a href="javascript:void(0)" class="filterToggler hide-sm-d"><i class="fa fa-sliders"></i></a>





    <script type="text/javascript" src="<%=(Common.GetLatestVersion(Page.ResolveUrl("js/sticky-sidebar.min.js"))) %>"></script>

    <script type="text/javascript">
        var sidebar = new StickySidebar('#sidebar', {
            containerSelector: '.main-section',
            innerWrapperSelector: '#aside',
            topSpacing: 60,
            bottomSpacing: 0,
        });
    </script>







</asp:Content>
