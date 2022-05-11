﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ShippingMaster.aspx.cs" Inherits="ShippingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <h1>Ship Master</h1>
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
                            <h3>Ship Companies List</h3>
                            <hr>
                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border" action="">


                                <!--adv-table start-->
                                <div class="adv-table">
                                    <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                        <div class="dataTables_length  col-md-8" id="dynamic-table_filter">
                                            <label style="margin-top: 6px;">
                                                Search:
                                            </label>
                                            <asp:TextBox runat="server" ID="txtSearch" aria-controls="dynamic-table"></asp:TextBox>
                                            <asp:Button runat="server" ID="btnSeach" Text="Search" CssClass="btn btn2 btn-info" OnClick="btnSeach_Click" />
                                        </div>
                                        <div id="dynamic-table_length" class="dataTables_length  col-md-4">
                                            <label style="float: right;">
                                                Show
                                            <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                          
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="500">500</asp:ListItem>
                                            </asp:DropDownList>
                                                entries</label>
                                        </div>

                                        <div class="col-md-12">
                                            <asp:GridView AutoGenerateColumns="false" OnRowCommand="grdList_RowCommand" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" OnPageIndexChanging="grdList_PageIndexChanging" PageSize="10" AllowPaging="true" runat="server" ID="grdList" CssClass="display table table-bordered table-striped dataTable" aria-describedby="dynamic-table_info">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%#Eval("ShipName") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Website">
                                                        <ItemTemplate>
                                                            <%#Eval("ShipWebsite") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Track Url">
                                                        <ItemTemplate>
                                                           <%#Eval("TrackUrl")%> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact No">
                                                        <ItemTemplate>
                                                            <%#Eval("ContactNo")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="edt" CommandArgument='<%#Eval("id") %>' CssClass="btn btn2 btn-primary" />
                                                            <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure to delete?')" Text="Delete" CommandName="del" CommandArgument='<%#Eval("id") %>' CssClass="btn btn2 btn-danger" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="122px" />
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
                            <h3>Add / Edit Ship Company</h3>
                            <hr>
                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border" action="">

                                <!--form-group start-->
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Company Name</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtCategory" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtCategory" Font-Size="11px" Text="Company Name is required" SetFocusOnError="true" runat="server" ID="req" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-sm-3 control-label">Contact No</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtContactNo" class="form-control"></asp:TextBox>
                                          </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-sm-3 control-label">Website Url</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtWebsiteUrl" Text="#" class="form-control"></asp:TextBox>
                                          </div>
                                </div>
                                <!--form-group end-->
                                <!--form-group start-->
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Track Url</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" Text="#" ID="txtUrl" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtUrl" Font-Size="11px" Text="Company Url is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <!--form-group end-->
                                <!--form-group start-->
                               


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
