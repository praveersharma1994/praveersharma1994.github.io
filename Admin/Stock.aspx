<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="admin_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <style>
        .inline {
            display: inline-block;
        }

        .fixed-width {
            width: 10%;
        }

        .fixed-widthtxt {
            width: 65%;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnexportexcel" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <h1>Stock List</h1>
                                    </div>
                                    <div class="col-md-6 col-sm-6 text-right">
                                        Total Products :
                                        <asp:Literal runat="server" ID="ltrtproducts"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div id="userdetail" runat="server">
                                <div class="row">
                                    <div class="col-md-12" role="grid">
                                        <div style="position: relative;">
                                            <div class="row" id="filter" style="top: 80px; position: sticky; z-index: 98; background: #fff; padding: 6px 0px;">
                                                <div class="col-md-2">
                                                    <label>Category </label>
                                                    <asp:DropDownList runat="server" data-id="drpcategory" ID="drpcategoryid" class="form-control"></asp:DropDownList>
                                                </div>

                                                <div class="col-md-2">
                                                    <label>SKU : </label>
                                                    <asp:TextBox ID="txtskumatching" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="col-md-2">
                                                    <label>From Date : </label>
                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtfromdate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                </div>
                                                 <div class="col-md-2">
                                                    <label>To Date : </label>
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                                      <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txttodate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                </div>

                                                <div class="col-md-2">
                                                    <label>
                                                        Filter :
                                                    </label>
                                                    <asp:DropDownList runat="server" data-id="drpmetal" ID="DropDownList1" class="form-control">
                                                        <asp:ListItem Value="all">All</asp:ListItem>
                                                        <asp:ListItem Value="hide">Only-Hide</asp:ListItem>
                                                        <asp:ListItem Value="show">Only-Shown</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-2">
                                                   <%-- <label style="float: right;">--%>
                                                    <label>Records :</label><br />
                                                        <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                                            <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="500">500</asp:ListItem>
                                                            <asp:ListItem Value="50000">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <%--</label>--%>
                                                </div>

                                                <div class="col-md-5">
                                                    <label>&nbsp;</label>
                                                    <div style="clear: both"></div>
                                                    <asp:Button runat="server" ID="btnsearchmatchitems" ValidationGroup="cat" Text="Search" CssClass="btn btn2 btn-info" OnClick="btnsearchmatchitems_Click" />
                                                    <asp:Button runat="server" ID="btnexportexcel" ValidationGroup="cat" Text="Export Excel" CssClass="btn btn2 btn-info" OnClick="btnexportexcel_Click" />
                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn2 btn-info " Text="Update Show/Hide Status" OnClientClick='return confirm("Are you sure to update show/hide status !")' OnClick="btnsubmit_Click" />
                                                    <asp:Button runat="server" ID="btncancel" ValidationGroup="cat" Text="Cancel" CssClass="btn btn2 btn-info " OnClick="btncancel_Click" />
                                                </div>

                                                
                                            </div>
                                        </div>
                                        <div style="clear: both"></div>
                                        <div class="row">
                                            <div class="col-md-12 table-responsive" id="divExport" runat="server">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" PageSize="100" AllowPaging="true" CssClass="display table table-bordered table-striped dataTable" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowCommand="GridView2_RowCommand">
                                                    <PagerSettings Position="Bottom" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <img width="50" src='../upload/Products/small/<%#Eval("image") %>' onerror="this.src='../upload/Products/noimage.jpg'" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SKU No">
                                                            <ItemTemplate>
                                                                <%#Eval("SKUName") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Category">
                                                            <ItemTemplate>
                                                                <%#Eval("categoryname") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Title">
                                                            <ItemTemplate>
                                                                <%#Eval("title") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MRP">
                                                            <ItemTemplate>
                                                                <%#Eval("MRP") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty" ItemStyle-CssClass="fixed-width" HeaderStyle-CssClass="fixed-width">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtqty" Width="75%" CssClass="form-control inline" onkeypress="return isNumberKey(event)" runat="server" Text='<%#Eval("stockpcs") %>'></asp:TextBox>
                                                                <asp:LinkButton runat="server" ID="lnkqtychange" CssClass="inline" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm('Are you sure to change qty')" CommandName="edtqty"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SRP">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtofferprice" Width="75%" CssClass="form-control inline" onkeypress="return isNumberKey(event)" runat="server" Text='<%#Eval("SRP") %>'></asp:TextBox>
                                                                <asp:LinkButton runat="server" ID="lnkofferpricechange" CssClass="inline" CommandArgument='<%#Eval("Id") %>' CommandName="OfferPrice"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <%-- <%#Eval("SRP") %>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Offer Price" ItemStyle-CssClass="fixed-width" HeaderStyle-CssClass="fixed-width">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtofferprice" Width="75%" CssClass="form-control inline" onkeypress="return isNumberKey(event)" runat="server" Text='<%#Eval("OfferPrice") %>'></asp:TextBox>
                                                                <asp:LinkButton runat="server" ID="lnkofferpricechange" CssClass="inline" CommandArgument='<%#Eval("Id") %>' CommandName="OfferPrice"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Show/Hide">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chk2" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkshowhide" runat="server" Checked='<%# Eval("showhide").ToString()=="0" ? false : true %>' />
                                                                <asp:HiddenField ID="hddId" runat="server" Value='<% #Eval("Id")%>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Common Products" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkcommonproducts" runat="server" Checked='<%# Eval("iscommon").ToString()=="0" ? false : Eval("iscommon").ToString()=="" ? false : true %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="text-center"></HeaderStyle>
                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <a class="btn btn2 btn-info" href="stockentry.aspx?Proid=<%#Eval("Id") %>"><i class="fa fa-edit"></i></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Other Images">
                                                            <ItemTemplate>
                                                                <label><%#  Eval("otherimage1")==""?"":Eval("otherimage1").ToString().Split(',').Count().ToString() %> </label>
                                                                <div class='<%# Eval("otherimage1")==""?"hide":"" %>' onmouseout="deldiv('<%# (Container.DataItemIndex+1) %>')" onmouseover="otherimg('<%# Eval("otherimage1") %>','othr<%# (Container.DataItemIndex+1) %>')"><span><i class="fa fa-image"></i></span></div>

                                                                <div id="othr<%# (Container.DataItemIndex+1) %>" class='<%# Eval("otherimage1")==""?"hide":"" %>'>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-md-12 text-right">
                                                <asp:Button ID="btnupdateSRP" runat="server" CssClass="btn btn2 btn-info " Text="Update SRP of Selected Products" OnClientClick='return confirm("Are you sure to update SRP !")' OnClick="btnupdateSRP_Click" />
                                                <asp:Button ID="btnupdateQTY" runat="server" CssClass="btn btn2 btn-info " Text="Update Qty of Selected Products" OnClientClick='return confirm("Are you sure to update Qty !")' OnClick="btnupdateQTY_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function otherimg(imges, ids) {
            $('#' + ids).html("");
            if (imges != "") {
                var img = imges.split(',');
                var dv = "";
                dv = dv + "<div class='image-container'>";
                for (i = 0; i < img.length; i++) {
                    dv = dv + "<span><img width='50' src='../upload/Products/othersmall/" + img[i] + "' /></span>";
                }
                dv = dv + "</div>";
                $('#' + ids).append(dv);
            }
        }

        function deldiv(ids) {
            $('#othr' + ids).html("");
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>



</asp:Content>

