<%@ Page Title="Add Banner" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="StripBanner.aspx.cs" Inherits="admin_StripBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table tr td, .table tr th {
            word-break: break-all;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upd">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <!--row start-->
                <div class="row">
                    <!--col-md-12 start-->
                    <div class="col-md-12">
                        <div class="page-heading">
                            <h1>Strip Banner (Website/Mobile)</h1>
                        </div>
                    </div>
                    <!--col-md-12 end-->
                </div>
                <!--row end-->

                <!--row start-->
                <div class="row">

                    <!--col-md-6 start-->
                    <div class="col-md-7">
                        <!--box-info start-->
                        <div class="box-info">
                            <h3>Strip Banner List</h3>
                            <hr>
                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border">


                                <!--adv-table start-->
                                <div class="adv-table">
                                    <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                        <div class="dataTables_length  col-md-8 hide" id="dynamic-table_filter">
                                            <label style="margin-top: 6px;">
                                                Search:
                                            </label>
                                            <asp:TextBox runat="server" ID="txtSearch" aria-controls="dynamic-table"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnSeach" Text="Search" CssClass="btn btn2 btn-info" OnClick="btnSeach_Click" />
                                        </div>

                                        <div class="col-md-12 table-responsive">
                                            <asp:GridView AutoGenerateColumns="false" OnRowCommand="grdList_RowCommand" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="grdList_PageIndexChanging" PageSize="10" AllowPaging="true" runat="server" ID="grdList" CssClass="display table table-bordered table-striped dataTable" aria-describedby="dynamic-table_info">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Title">
                                                        <ItemTemplate>
                                                            <%#Eval("BannerTitle") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Img (Website)">
                                                        <ItemTemplate>
                                                            <img src="<%#"../upload/stripbanner/"+Eval("BannerImg")+"?"+System.DateTime.Now.Ticks %>" onerror="this.src='../images/noimage.JPG'" style="max-height: 60px; max-width: 160px;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Img (Mobile)">
                                                        <ItemTemplate>
                                                            <img src="<%#"../upload/stripbanner/"+Eval("BannerImgmobile")+"?"+System.DateTime.Now.Ticks %>" onerror="this.src='../images/noimage.JPG'" style="max-height: 50px; max-width: 120px;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dis. Order">
                                                        <ItemTemplate>
                                                            <%#Eval("DisplayOrder") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="edt" CommandArgument='<%#Eval("BannerId") %>' CssClass="btn btn2 btn-primary" />
                                                            <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure to delete?')" Text="Delete" CommandName="del" CommandArgument='<%#Eval("BannerId") %>' CssClass="btn btn2 btn-danger" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                            <h3>Add / Edit Strip Banner</h3>
                            <hr>
                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border">
                                <!--form-group start-->
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Banner Title</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtCategory" class="form-control"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                 <div class="form-group hide">
                                    <label class="col-sm-3 control-label">URL</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txturl" class="form-control" Text="#"></asp:TextBox>
                                    </div>
                                </div>
                                <!--form-group end-->
                                <!--form-group start-->
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Image(1920 X 180) Website</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" ID="fluUpload"></asp:FileUpload>
                                        <asp:HiddenField runat="server" ID="hddImg" />
                                    </div>
                                </div>

                                 <div class="form-group">
                                    <label class="col-sm-3 control-label">Image(640 X 185) Mobile</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" ID="fluUploadmobile"></asp:FileUpload>
                                        <asp:HiddenField runat="server" ID="hddImgMobile" />
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-sm-3 control-label">Display Order</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtdisplayorder" class="form-control" Text="1"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftb1" runat="server" TargetControlID="txtdisplayorder" ValidChars="0123456789" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <!--form-group end-->

                            </div>
                            <!--form-horizontal row-border end-->
                            <!--row start-->
                            <div class="row">
                                <div class="col-sm-9 col-sm-offset-3">
                                    <div class="btn-toolbar" id="divnotadd" runat="server">
                                        <asp:Button runat="server" ID="btnSave" class="btn-primary btn btn2" ValidationGroup="cat" OnClick="btnSave_Click" Text="Submit" />
                                        <asp:Button runat="server" ID="btnCancel" class="btn-default btn btn2" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:HiddenField runat="server" ID="hddId" />
                                    </div>
                                </div>
                            </div>
                            <!--row end-->
                        </div>
                        <!--box-info end-->
                    </div>
                    <!--col-md-6 end-->

                </div>
                <!--row end-->

                <!--row end-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

