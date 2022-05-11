<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="proreviews.aspx.cs" Inherits="proreviews" %>

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
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnexportexcel" />
        </Triggers>--%>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <h1>Product Review List</h1>
                                    </div>
                                   <%-- <div class="col-md-6 col-sm-6 text-right">
                                        Total Products :
                                        <asp:Literal runat="server" ID="ltrtproducts"></asp:Literal>
                                    </div>--%>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div id="userdetail" runat="server">
                                <div class="row">
                                    <div class="col-md-12" role="grid">
                                        <%--<div style="position: relative;">
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
                                                    <label>
                                                        Filter :
                                                    </label>
                                                    <asp:DropDownList runat="server" data-id="drpmetal" ID="DropDownList1" class="form-control">
                                                        <asp:ListItem Value="all">All</asp:ListItem>
                                                        <asp:ListItem Value="hide">Only-Hide</asp:ListItem>
                                                        <asp:ListItem Value="show">Only-Shown</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-5">
                                                    <label>&nbsp;</label>
                                                    <div style="clear: both"></div>
                                                    <asp:Button runat="server" ID="btnsearchmatchitems" ValidationGroup="cat" Text="Search" CssClass="btn btn2 btn-info" OnClick="btnsearchmatchitems_Click" />
                                                    <asp:Button runat="server" ID="btnexportexcel" ValidationGroup="cat" Text="Export Excel" CssClass="btn btn2 btn-info" OnClick="btnexportexcel_Click" />
                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn2 btn-info " Text="Update Show/Hide Status" OnClientClick='return confirm("Are you sure to update show/hide status !")' OnClick="btnsubmit_Click" />
                                                    <asp:Button runat="server" ID="btncancel" ValidationGroup="cat" Text="Cancel" CssClass="btn btn2 btn-info " OnClick="btncancel_Click" />
                                                </div>

                                                <div class="col-md-1">
                                                    <label style="float: right;">

                                                        <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                                            <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="500">500</asp:ListItem>
                                                            <asp:ListItem Value="50000">All</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div style="clear: both"></div>
                                        <div class="row">
                                            <div class="col-md-12 table-responsive" id="divExport" runat="server">
                                                <asp:GridView runat="server" ID="grdList" DataKeyNames="ID" AutoGenerateColumns="false"
                                                AllowPaging="False" ShowFooter="true" PageSize="10" OnPageIndexChanging="grdList_PageIndexChanging" OnRowCommand="grdList_RowCommand" CssClass="display table table-bordered table-striped dataTable" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Literal runat="server" ID="ltrSNO" Text='<%# Container.DataItemIndex+1 %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img1" runat="server" Height="80" Width="80" ImageUrl='<%#"../upload/Products/Small/"+ Eval("Image") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("ItemCode") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rating">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("Rating") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("Name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                  

                                                    <asp:TemplateField HeaderText="Review">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("review") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%#Eval("Addate","{0:D}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action / Status">
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure to delete?')" Text="Delete" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' CssClass="btn btn2 btn-danger" Visible="false" />
                                                            <asp:Button runat="server" ID="btnactinact" OnClientClick="return confirm('Are you sure to change status ?')" Text='<%# Eval("status").ToString()=="0"?"Inactive":"Active" %>' CommandName="actinact" CommandArgument='<%#Eval("id") %>' CssClass='<%# Eval("status").ToString()=="0"?"btn btn2 btn-danger":"btn btn2 btn-primary" %>' ToolTip='<%# Eval("status").ToString()=="0"?"Click to Active":"Click to Inactive" %>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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

