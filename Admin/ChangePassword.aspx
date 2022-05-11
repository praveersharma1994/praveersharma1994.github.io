<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="admin_ChangePassword" %>

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
                            <h1>Account Setting</h1>
                        </div>
                    </div>
                    <!--col-md-12 end-->
                </div>
                <!--row end-->

                <!--row start-->
                <div class="row">
                    <!--box-info start-->
                    <div class="col-md-12">
                        <div class="box-info">
                            <h3>Change Password</h3>
                            <hr>
                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border" >

                                <!--form-group start-->

                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Enter Old Password</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txt_old" class="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txt_old" Font-Size="11px" Text="Old Password is required" SetFocusOnError="true" runat="server" ID="req" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Enter New Password</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" ID="txt_new" class="form-txt_new" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txt_new" Font-Size="11px" Text="New Password is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                         

                                    </div>
                                </div>
                                <!--form-group end-->

                                <!--form-group start-->
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">Confirm Password</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" Text="" ID="txtUrl" class="form-control" TextMode="Password"></asp:TextBox>
                                          <asp:RequiredFieldValidator ControlToValidate="txtUrl" Font-Size="11px" Text="Confirm Password is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" Text="Cofirm Password Not Match" Font-Size="12px" Display="Dynamic" ControlToValidate="txtUrl" ControlToCompare="txt_new" ValidationGroup="cat"></asp:CompareValidator>
                                    </div>
                                </div>
                                <!--form-group end-->

                                  <!--row start-->
                            <div class="row">
                                <div class="col-sm-9 col-sm-offset-3">
                                    <div class="btn-toolbar">
                                        <asp:Button runat="server" ID="btnSave" class="btn-primary btn btn2" ValidationGroup="cat" OnClick="btnSave_Click" Text="Update" />
                                        <asp:Button runat="server" ID="btnCancel" class="btn-default btn btn2" OnClick="btnCancel_Click" Text="Cancel" />
                                       
                                    </div>
                                      <div class="col-sm-4 text-center"><asp:Label ID="lblstatus" runat="server" Text="" Font-Bold="true" Font-Size="12px"  ForeColor="Red"></asp:Label></div> 
                                </div>
                            </div>
                            <!--row end-->

                            </div>
                            <!--form-horizontal row-border end-->

                        </div>


                        
                    </div>
                    <!--box-info end-->
                    <!--col-md-6 start-->



                    <!--col-md-6 end-->

                </div>
                <!--row end-->


              
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

