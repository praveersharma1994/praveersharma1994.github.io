<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="seoproducturl.aspx.cs" Inherits="seoproducturl" %>

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
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-6 col-sm-6">
                                        <h1>Product Url</h1>
                                    </div>
                                    <div class="col-md-6 col-sm-6 text-right">
                                        Item :
                                        <asp:DropDownList runat="server" ID="drpDisplay">
                                        </asp:DropDownList>
                                        &nbsp; &nbsp;
                                       Category:
                                        <asp:DropDownList runat="server" ID="drpMeta">
                                        </asp:DropDownList>
                                        &nbsp; &nbsp;
                                        <asp:Button runat="server" ID="btnFilter" Text="Filter Product" OnClick="btnFilter_Click" />
                                        &nbsp; &nbsp;
                                        <asp:Button runat="server" ID="btnExport" Text="Export Excel" OnClick="bntExport_Click" />
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
                                                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red"  CssClass="display table table-bordered table-striped dataTable">
                                                        <PagerSettings Position="Bottom" />
                                                        
                                                        <Columns>
                    <asp:TemplateField HeaderText="S.NO.">
                        <ItemTemplate>
                            <%#Container.DisplayIndex+1 %>
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sku">
                        <ItemTemplate>
                            <%#Eval("ItemCode") %>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left"/>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <%#Eval("Item") %>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Left"/>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Collection">
                        <ItemTemplate>
                            <%#Eval("colname") %>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left"/>

                    </asp:TemplateField>
                   
                    

                   
                    <asp:TemplateField HeaderText="Product URL">
                        <ItemTemplate>
                          <%--<%# Eval("seobarcode") == null ? "" + url + "jewellery" + "/" + CommanClass.url(Convert.ToString(Eval("Item"))) + "/" + CommanClass.url(Convert.ToString(Eval("ProductDesc"))) + "-" + Eval("barcode") + ".html" : "" + url + "jewellery" + "/" + CommanClass.url(Convert.ToString(Eval("Item"))) + "/" + CommanClass.url(Convert.ToString(Eval("MetaUrl"))) + "-" +  Eval("barcode") + ".html" %>--%>
                            <%# url + Common.url(Convert.ToString(Eval("colname"))) + "/" + Common.url(Convert.ToString(Eval("Item"))) + "/" + Common.url(Convert.ToString(Eval("title"))) + "-" + Eval("productid") + ".html" %>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <%# Eval("Title") %>
                        </ItemTemplate>                   
                         <ItemStyle Width="120px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Meta Title">
                        <ItemTemplate>
                           
                              <%#Eval("seobarcode") == null ? Eval("Title") : Eval("seotitle") %>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Meta Keyword">
                        <ItemTemplate>
                            <%#Eval("seobarcode") == null ? "-" : Eval("keyword") %>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Meta Description">
                        <ItemTemplate>
                              <%#Eval("seobarcode") == null ? "-" : Eval("Description") %>
                            
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left"/>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Alt">
                        <ItemTemplate>
                          <%#Eval("seobarcode") == null ? "-" : Eval("MetaUrl") %>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
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

