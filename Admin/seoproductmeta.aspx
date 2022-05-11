<%@ Page Title="Stocks" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="seoproductmeta.aspx.cs" Inherits="seoproductmeta" %>

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
            <asp:PostBackTrigger ControlID="btn_excelformat" />
        </Triggers>
        <ContentTemplate>
            <div class="scrollable wrapper">
                <div class="box-info">
                    <div class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="page-heading">
                                <div class="row">
                                    <div class="col-md-3 col-sm-3">
                                        <h1>Add Product Meta</h1>
                                    </div>
                                    <div class="col-md-9 col-sm-9 text-right">
                                        <asp:FileUpload ID="fuExcel" runat="server" CssClass="homePass1" ></asp:FileUpload>
                                        &nbsp; &nbsp;
                                      
                                         <asp:Button ID="btnsave" runat="server" Text="Import Execl" Width="135px" ValidationGroup="insert"
                                CssClass="loginButton" OnClick="btnsave_Click" />
                                        &nbsp; &nbsp;
                                         <asp:Button ID="btnEmport" runat="server" Text="Save Record" Width="135px" ValidationGroup="insert"
                                CssClass="loginButton" OnClick="btnEmport_Click" />
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
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            

                             <asp:TemplateField HeaderText="Sku">
                                <ItemTemplate>
                                    <asp:Label ID="lblurl" runat="server" Text='<%# Eval("Sku") %>'></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle  HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <asp:Label ID="lblurl1" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </ItemTemplate>
                                 <ItemStyle  HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Keywords">
                                <ItemTemplate>
                                    <%# Eval("Keywords") %>
                                </ItemTemplate>

                                 <ItemStyle  HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <%#Eval("Description") %>
                                </ItemTemplate>
                                 <ItemStyle  HorizontalAlign="Left" />
                            </asp:TemplateField>
                           <%--  <asp:TemplateField HeaderText="Alt">
                                <ItemTemplate>
                                    <%#Eval("Alt") %>
                                </ItemTemplate>
                                  <ItemStyle  HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                            

                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDeletes" runat="server" ToolTip="Delete" ImageUrl="~/images/close.png"
                                        CommandArgument='<%#Eval("Sku") %>' CommandName="DeleteMeta" CausesValidation="false" OnClientClick="return confirm('Are you sure to Delete...')" />
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

