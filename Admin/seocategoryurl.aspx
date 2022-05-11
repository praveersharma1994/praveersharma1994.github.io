<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="seocategoryurl.aspx.cs" Inherits="seocategoryurl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <h1>Category / Website Urls</h1>
                                    </div>
                                    <div class="col-md-6 col-sm-6 text-right">
                                        <asp:RadioButton ID="rdo_category" runat="server" Text="Category Url" GroupName="url" Checked="true" OnCheckedChanged="rdo_category_CheckedChanged" AutoPostBack="true" />
                                        &nbsp; &nbsp;
                                        <asp:RadioButton ID="rdo_website" runat="server" Text="Website Url" GroupName="url" OnCheckedChanged="rdo_website_CheckedChanged" AutoPostBack="true" />
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btnsave" runat="server" Text="Export Excel" Width="135px" CssClass="loginButton" OnClick="btnsave_Click" Style="float: right;" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div id="userdetail" runat="server">
                                <div class="row">
                                    <div class="col-md-12" role="grid">

                                        <div class="row">
                                            <div class="col-md-12 table-responsive" id="divExport" runat="server">
                                                  <asp:Panel runat="server" ID="pnlGroupExport">
                                                <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" OnRowDataBound="gv1_RowDataBound" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red"  CssClass="display table table-bordered table-striped dataTable">
                                                    <PagerSettings Position="Bottom" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.NO.">
                                                            <ItemTemplate>
                                                                <%#Container.DisplayIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%#Eval("Item") %>
                    </ItemTemplate>
                    <ItemStyle Width="150px" />

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Category">
                    <ItemTemplate>
                        <%#Eval("SubCategory") %>
                    </ItemTemplate>
                    <ItemStyle Width="180px" />

                </asp:TemplateField>--%>





                                                        <asp:TemplateField HeaderText="Category/Website URL">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblurl" runat="server" Text='<%#Eval("CategoryUrl") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Meta Title" SortExpression="Title">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmetatitle" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Meta Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmetadescription" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Meta Keyword">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmetakeyword" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Alt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblalt" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                      </asp:Panel>
                                            </div>
                                            <div class="col-md-12 text-right">
                                                <%--<asp:Button ID="btnupdateSRP" runat="server" CssClass="btn btn2 btn-info " Text="Update SRP of Selected Products" OnClientClick='return confirm("Are you sure to update SRP !")'  />
                                                <asp:Button ID="btnupdateQTY" runat="server" CssClass="btn btn2 btn-info " Text="Update Qty of Selected Products" OnClientClick='return confirm("Are you sure to update Qty !")'  />--%>
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




</asp:Content>

