<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="OrderList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../FancyBox/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="../FancyBox/jquery.fancybox1.js?v=2.1.5"></script>
    <link href="../FancyBox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
                         elm[i].id != theBox.id) {
                    //elm[i].click();

                    if (elm[i].checked != xState)
                        elm[i].click();
                    elm[i].checked = xState;
                }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.fancyBox123').fancybox({
                autoDimensions: true,
                height: 800,
                width: 700,
                afterClose: function () {
                    window.location.reload();
                }
            });
            $('.fancyBoxDetail').fancybox({
                autoDimensions: true,
                height: 800,
                width: 1000,
            });
        });
	</script>

    <script type="text/javascript" src="../FancyBox/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="../FancyBox/jquery.fancybox1.js?v=2.1.5"></script>
    <link href="../FancyBox/jquery.fancybox.css" rel="stylesheet" type="text/css" media="screen" />


    <script type="text/javascript">
        $(document).ready(function () {
            $('.fancyBox123').fancybox({
                autoDimensions: true,
                height: 800,
                width: 700,
                afterClose: function () {
                    // window.location.reload();
                }
            });

            $('.fancyBox123').fancybox({
                autoDimensions: true,
                height: 500,
                width: 700

            });

            $('.fancyBoxDetail').fancybox({
                autoDimensions: true,
                height: 800,
                width: 1000,
            });
        });

    </script>

    <style>
        .hweuserdetail hr {
            margin-top: 5px;
            margin-bottom: 5px;
            border: 0;
            border-top: 1px solid #eee;
        }
    </style>

    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <style>
            td a, th a {
                color: #2a6496 !important;
            }
        </style>
    <%--  <asp:UpdatePanel runat="server" ID="upd">
        <ContentTemplate>--%>
    <div class="scrollable wrapper">
        <!--row start-->
        <div class="row">
            <!--col-md-12 start-->
            <div class="col-md-12">
                <div class="page-heading">
                    <h1>Order List</h1>
                </div>
            </div>
            <!--col-md-12 end-->
        </div>
        <!--row end-->

        <!--row start-->
        <div class="row">
            <!--col-md-6 start-->
            <div class="col-md-12">
                <!--box-info start-->
                <div class="box-info">
                    <!--form-horizontal row-border start-->
                    <div class="form-horizontal row-border" action="">
                        <!--adv-table start-->
                        <div class="adv-table">
                            <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                <div class="dataTables_length  col-md-10" id="dynamic-table_filter">
                                    <label style="margin-top: 6px;">
                                        Search:
                                    </label>

                                    <asp:TextBox runat="server" ID="txtSearch" aria-controls="dynamic-table" placeholder="Order No"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtEmail" aria-controls="dynamic-table" placeholder="Email"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtUserName" aria-controls="dynamic-table" placeholder="User Name"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="txtOrderDate" aria-controls="dynamic-table" placeholder="Order Date"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtOrderDate" ID="cal1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                                    <asp:DropDownList runat="server" Width="110px" ID="drpshoworder" Visible="false">
                                        <asp:ListItem Value="0">Unhide Order</asp:ListItem>
                                        <asp:ListItem Value="1">Hide Order</asp:ListItem>
                                        <asp:ListItem Value="2">All Order</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button runat="server" ID="btnSeach" Text="Search" CssClass="btn btn2 btn-info" OnClick="btnSeach_Click" />
                                </div>
                                <div id="dynamic-table_length" class="dataTables_length  col-md-2">
                                    <label style="float: right;">
                                        Show
                                            <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                                <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="500">500</asp:ListItem>
                                                <asp:ListItem Value="1000">1000</asp:ListItem>
                                            </asp:DropDownList>
                                        entries</label>
                                </div>

                                <div class="col-md-12 row">

                                    <div class="table-responsive">
                                        <asp:GridView AutoGenerateColumns="false" OnRowDataBound="grdList_RowDataBound" OnRowCommand="grdList_RowCommand" OnSorting="grdList_Sorting" AllowSorting="true" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="grdList_PageIndexChanging" PageSize="20" AllowPaging="true" runat="server" ID="grdList" CssClass="display table table-bordered table-striped dataTable" aria-describedby="dynamic-table_info">
                                            <columns>
                                            <asp:TemplateField HeaderText="S No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order No" SortExpression="OrderNo">
                                                <ItemTemplate>
                                                    <a href="#" style="text-transform: uppercase"><span class="orderlistlink" style='<%# Eval("Status").ToString()=="1" ? "color:#016b01;" : "color:#f54e4e;"  %>' onclick="javascript:window.open('orderDetail.aspx?order=<%#Eval("OrderNo") %>','mywindowtitle','width=1200,height=768')"><%#Eval("OrderNo") %></span></a>
                                                    <asp:HiddenField runat="server" ID="hddOrderNo" Value='<%#Eval("OrderNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" SortExpression="OrderTotal">
                                                <ItemTemplate>
                                                    <%#Eval("OrderTotal") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount Amt">
                                                <ItemTemplate>
                                                    <%#Eval("CouponAmt") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Net Amt">
                                                <ItemTemplate>
                                                    <%# Convert.ToDecimal( Eval("ShippingAmt"))+Convert.ToDecimal( Eval("OrderTotal")) - Convert.ToDecimal(Eval("CouponAmt")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User" SortExpression="Name">
                                                <ItemTemplate>
                                                    <%#Eval("UserName") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ContactNo">
                                                <ItemTemplate>
                                                    <%#Eval("ContactNo") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <%#Eval("Email") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Comment">
                                                <ItemTemplate>
                                                    <%#Eval("Comment") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status" SortExpression="OrderStatus">
                                                    <ItemTemplate>
                                                        <div class="hweuserdetail text-center" style="min-height:65px;">
                                                            <div style="<%#Eval("OrderStatus").ToString()=="Processed"?"display:block;": "display:none;" %>" id="divprocess">
                                                                <%#Eval("OrderStatus") %><hr />
                                                            </div>

                                                            <div style="<%#Eval("OrderStatus").ToString()=="Shipped"?"display:block;": "display:none;" %>">
                                                                <%--<a href="shippedStatus.aspx?order=<%#Eval("OrderNo") %>&d=d" class="fancybox alert alert-success fancybox.iframe fancyBox123 link012" style="font-weight: 500">Shipped</a>--%>
                                                                <a href="shippedStatus.aspx?order=<%#Eval("OrderNo") %>&d=d" target="_blank" class="link012" style="font-weight: 500">Shipped</a>
                                                                <hr />
                                                            </div>

                                                            <div style="<%#Eval("OrderStatus").ToString()=="Processed"?"display:block;": "display:none;" %>" id="divship">
                                                               <%-- <a href="shippedStatus.aspx?order=<%#Eval("OrderNo") %>" class="fancybox  fancybox.iframe fancyBox123 link012">Shipped</a>--%>
                                                                 <a href="shippedStatus.aspx?order=<%#Eval("OrderNo") %>" target="_blank" class="link012">Shipped</a>
                                                                <hr />
                                                            </div>

                                                            <div style="<%#Eval("OrderStatus").ToString()=="cancelled"?"display:block;": "display:none;" %>" id="divcancel">
                                                                <span class="alert alert-danger">Cancelled</span>
                                                                <hr />
                                                            </div>

                                                            <div style="<%#Eval("OrderStatus").ToString()=="Processed"?"display:block;": "display:none;" %>" id="divcanlink">
                                                                <asp:LinkButton ID="lnkcancelorder" runat="server" Text="Cancel" Style="color: red !important;" OnClientClick="return confirm('Are you sure to Cancel this order')" OnClick="lnkcancelorder_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Order By">
                                                <ItemTemplate>
                                                    <%#Eval("orderby") %> <br /><br />
                                                    <%# Eval("PaymentMode") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    
                                            <asp:TemplateField HeaderText="Created On" SortExpression="OrderDate">
                                                <ItemTemplate>
                                                    <%# Convert.ToDateTime( Eval("OrderDate")).ToString("dd/MM/yyyy hh:mm tt")%>
                                                    <br /><br />
                                                    <asp:LinkButton ID="lnkdeleteorder" runat="server" Text="Delete Order" OnClientClick="return confirm('Are you sure to delete this order !')" OnClick="lnkdeleteorder_Click" ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </columns>
                                        </asp:GridView>
                                    </div>
                            </div>
                            <div class="col-md-12">&nbsp;</div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--row end-->
    </div>

    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script>

        $(function () {
            pageLoad();
        })

        function pageLoad() {
            $('[data-toggle="popover"]').popover({
                html: true,
                content: function () {
                    var html = "";
                    var userid = $(this).attr("data-userid");
                    var filter = $(this).attr("data-filter");
                    if (filter === "1") {
                        html = html + "<div class='table-responsive'>";
                        html = html + "<table  class='table table-bordered'>";
                        html = html + "<tbody>";
                        html = html + "<tr><th>Status</th><th>Action</th></tr>";
                        html = html + "<tr>";
                        html = html + "<td><select id='statusselection' style='width: 100%;'><option value='close'>Close</option><option value='paid'>Paid</option><option value='unpaid'>Unpaid</option></select></td>";
                        html = html + "<td><input type='button' value='Save' onclick=updateorderstatus('" + userid + "')></td>";
                        html = html + "</tr>";
                        html = html + "</tbody>";
                        html = html + "</table>";
                        html = html + "</div>";
                    }
                    return html;
                },
                width: "300px"
            });

            $('body').on('click', function (e) {
                $('[data-toggle="popover"]').each(function () {
                    //the 'is' for buttons that trigger popups
                    //the 'has' for icons within a button that triggers a popup
                    if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                        $(this).popover('hide');
                    }
                });
                var tooltips = $('.popover.fade.bottom').not('.in');
                if (tooltips) {
                    tooltips.remove();
                }
            });
        }

        function updateorderstatus(orderid) {
            var status = $("#statusselection option:selected").val();
            $.ajax({
                type: "POST",
                url: "OrderList.aspx/Updatestatus",
                data: '{Orderid:"' + orderid + '",Status:"' + status + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == "Sucess") {
                        window.location.reload();
                        alert(response.d);
                    }
                    else {
                        alert(response.d);
                    }
                },
                error: function (respose) { }
            });
        }
    </script>
</asp:Content>

