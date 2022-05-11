<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="seocategorymeta.aspx.cs" Inherits="seocategorymeta" %>

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
            <asp:PostBackTrigger ControlID="btn_excelformat" />
            <asp:PostBackTrigger ControlID="btnSeach" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-3 col-sm-3">
                                        <h1>Add Category / Web Url Meta</h1>
                                    </div>
                                    <div class="col-md-9 col-sm-9 text-right">
                                        <asp:RadioButton ID="rdo_category" runat="server" Text="Category Url"  GroupName="url" Checked="true"/> &nbsp;
                            <asp:RadioButton ID="rdo_website" runat="server" Text="Website Url"  GroupName="url" /> &nbsp;
                                        <asp:FileUpload runat="server" ID="FileUpload1"  aria-controls="dynamic-table"></asp:FileUpload>
                                        &nbsp; &nbsp;
                                      
                                          <asp:Button runat="server" ID="btnSeach" style="float:left" Text="Import Excel" CssClass="btn btn2 btn-info" OnClick="btnSeach_Click" />
                                        &nbsp; &nbsp;
                                        <asp:Button runat="server" ID="btnEdit" Text="Save List"   CssClass="btn btn2 btn-primary" OnClick="btnEdit_Click" />
                                        &nbsp; &nbsp;
                                          <asp:Button ID="btn_excelformat" runat="server" Text="Excel Format" Width="135px" 
                                CssClass="loginButton" OnClick="btn_ecelformat" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                         <asp:Label ID="lblMessage" runat="server" CssClass="redMessage" Visible="False"
                                ForeColor="#CC3300" />
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
                
               
               

              
                <asp:TemplateField HeaderText="Category/Sub-Category URL">
                    <ItemTemplate>
                   
                         <asp:Label ID="lblurl" runat="server"  Text='<%#Eval("CategoryUrl") %>'></asp:Label>
                        
                    </ItemTemplate>
                       <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:TemplateField>

                
                 <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblmetatitle" runat="server"  Text='<%# Eval("Title") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="300px" HorizontalAlign="Left"/>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                         <asp:Label ID="lblmetadescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="300px" HorizontalAlign="Left"/>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Keyword">
                    <ItemTemplate>
                        <asp:Label ID="lblmetakeyword" runat="server"  Text='<%# Eval("Keywords") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="300px" HorizontalAlign="Left"/>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Alt">
                    <ItemTemplate>
                        <asp:Label ID="lblmetakeyword" runat="server"  Text='<%# Eval("Alt") %>'></asp:Label>
                    </ItemTemplate>
                       <ItemStyle Width="300px" HorizontalAlign="Left"/>
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

