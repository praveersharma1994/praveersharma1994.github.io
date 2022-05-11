<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="homecollectionbanner.aspx.cs" Inherits="Admin_homecollectionbanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table tr td, .table tr th {
            word-break: break-all;
        }
    </style>

    <div class="scrollable wrapper">
        <!--row start-->
        <div class="row">
            <!--col-md-12 start-->
            <div class="col-md-12">
                <div class="page-heading">
                    <h1>Home Collection Banner (App)</h1>
                </div>
            </div>
            <!--col-md-12 end-->
        </div>
        <!--row end-->

        <!--row start-->
        <%--   <asp:UpdatePanel runat="server" ID="upd">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
            <ContentTemplate>--%>
        <div class="row">
            <!--col-md-6 start-->
            <div class="col-md-7">
                <!--box-info start-->
                <div class="box-info">
                    <h3>Collection Banner List</h3>
                    <hr>
                    <!--form-horizontal row-border start-->
                    <div class="form-horizontal row-border">
                        <!--adv-table start-->
                        <div class="adv-table">
                            <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                <div class="dataTables_length  col-md-8" id="dynamic-table_filter">&nbsp; </div>
                                <div id="dynamic-table_length" class="dataTables_length  col-md-4">
                                    <label style="float: right;">
                                        Show
                                            <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                                <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                            </asp:DropDownList>
                                        entries</label>
                                </div>

                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView AutoGenerateColumns="false" OnRowCommand="grdList_RowCommand" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="grdList_PageIndexChanging" PageSize="10" AllowPaging="true" runat="server" ID="grdList" CssClass="display table table-bordered table-striped dataTable" aria-describedby="dynamic-table_info">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Img">
                                                    <ItemTemplate>
                                                        <img src='<%# "../upload/mobile/SpecialBanner/"+ Eval("ImgName")+"?"+System.DateTime.Now.Ticks %>' class='img-responsive' style='max-height: 90px; max-width: 200px;' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <%#Eval("categoryname") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="edt" CommandArgument='<%#Eval("id") %>' CssClass="btn btn2 btn-primary" />
                                                        <asp:Button runat="server" ID="btndelete" OnClientClick="return confirm('Are you sure to delete?')" Text="Delete" CommandName="del" CommandArgument='<%#Eval("id") %>' CssClass="btn btn2 btn-danger" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--adv-table end-->
                    </div>
                </div>
                <!--box-info end-->
            </div>
            <!--col-md-6 end-->

            <!--col-md-6 start-->
            <div class="col-md-5">
                <!--box-info start-->
                <div class="box-info">
                    <h3>Add / Edit Home Collection Banner</h3>
                    <hr>
                    <!--form-horizontal row-border start-->
                    <div class="form-horizontal row-border">
                        <!--form-group start-->
                        <%-- <div class="form-group">
                                    <label class="col-sm-3 control-label">Position</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList CssClass="form-control ddl-position" runat="server" ID="ddl_position">
                                            <asp:ListItem Value="1" Selected="True">Position 1 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="2">Position 2 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="3">Position 3 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="4">Position 4 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="5">Position 5 - Banner Size (704 x 300)</asp:ListItem>
                                            <asp:ListItem Value="6">Position 6 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="7">Position 7 - Banner Size (700 x 300)</asp:ListItem>
                                            <asp:ListItem Value="8">Position 8 - Banner Size (700 x 300)</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>

                        <!--form-group end-->
                        <!--form-group start-->
                        <div class="form-group" style="display: none;">
                            <label class="col-sm-3 control-label">Banner Title</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtCategory" class="form-control" Text="#"></asp:TextBox>

                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Category</label>
                            <div class="col-sm-9">

                                <asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="drpcategory">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!--form-group end-->
                        <!--form-group start-->
                        <div class="form-group" id="div-upload">
                            <label class="col-sm-3 control-label">Image</label>
                            <div class="col-sm-9">
                                <asp:FileUpload accept="image/*" CssClass="form-control" runat="server" ID="fluUpload"></asp:FileUpload>
                                <asp:HiddenField runat="server" ID="hddImg" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" Display="Dynamic" ErrorMessage="* Add Image" ControlToValidate="fluUpload" ValidationGroup="cat"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <!--form-group end-->

                        <!--form-group start-->
                        <%--<div class="form-group" id="div-bannerurl">
                                    <label class="col-sm-3 control-label">Banner Family</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" CssClass="autosuggest" Text="#" ID="txtUrl" onkeypress="getatocomplete()" class="form-control" ClientIDMode="Static"></asp:TextBox>                                       
                                        <asp:RequiredFieldValidator ControlToValidate="txtUrl" Font-Size="11px" Text="Banner Family is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                        <!--form-group end-->
                    </div>
                    <!--form-horizontal row-border end-->
                    <!--row start-->
                    <div class="row">
                        <div class="col-sm-9 col-sm-offset-3">
                            <div class="btn-toolbar">
                                <asp:Button runat="server" ID="btnSave" class="btn-primary btn btn2" ValidationGroup="cat" OnClick="btnSave_Click" Text="Submit" />
                                <asp:Button runat="server" ID="btnCancel" class="btn-default btn btn2" OnClick="btnCancel_Click" Text="Cancel" />
                                <asp:HiddenField runat="server" ID="hddId" />
                            </div>
                        </div>
                    </div>


                    <%--<div id="div-caption" class="alert alert-info small " style="padding: 5px 10px; margin-top: 10px;">
                                * All Banner Size Should be in This Ratio
                                <label id="label-caption"></label>

                            </div>--%>
                    <!--row end-->
                </div>
                <!--box-info end-->
            </div>
            <!--col-md-6 end-->

        </div>
        <!--row end-->
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
        <!--row end-->
    </div>


    <script>
        function pageLoad() {
            ChangeResolution();
            $(".cbUploadType input[type='radio']").change(function () {
                showInstruction();
            });
            showInstruction();
        }

        function showInstruction() {
            var obj = $(".cbUploadType input[type='radio']:checked");
            if (obj.length > 0) {
                var value = obj.val().toLocaleLowerCase();
                if (value === "rdovideo") {
                    $("#divinstruction").hide();
                    $("#div-caption,#div-bannerurl").hide();
                }
                else {
                    $("#divinstruction").hide();
                    $("#div-caption,#div-bannerurl").show();
                }
            }
        }

        function ChangeResolution() {
            var obj = $(".ddl-position");
            var label = $("#label-caption");
            if (obj.val() === "1") {
                label.text("760x320");
            }
            else if (obj.val() === "2") {
                label.text("500x427");
            }
        }
    </script>
</asp:Content>

