<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminlogin.aspx.cs" Inherits="adminlogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>S Style Factory - Admin</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <!-- Bootstrap -->
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/css/bootstrap.min.css")) %>' rel="stylesheet"/>
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/css/style-responsive.css")) %>' rel="stylesheet"/>
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/css/atom-style.css")) %>' rel="stylesheet"/>
    <link href='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/css/font-awesome.min.css")) %>' rel="stylesheet"/>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300' rel='stylesheet' type='text/css'/>
</head>
<body>
    <div class="container login-bg">
        <form id="form1" runat="server" class="login-form-signin">
            <div class="login-logo">
                <img src="admincss/images/avatar.png" />
            </div>
            <h2 class="login-form-signin-heading">Login Your Account</h2>
            <asp:Panel ID="pnl_login" runat="server" DefaultButton="btnLogin">
            <div class="login-wrap">
                <asp:TextBox runat="server" ID="txtLoginId" placeholder="Login Id" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="req" ControlToValidate="txtLoginId" ValidationGroup="login" Text="*" Display="None" SetFocusOnError="true" ErrorMessage="-Login Id is required"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPassword" Text="*" Display="None" SetFocusOnError="true" ErrorMessage="-Password is required" ValidationGroup="login"></asp:RequiredFieldValidator>
                <asp:ValidationSummary runat="server" ID="valid" ValidationGroup="login" style="font-size:11px; margin-bottom:5px;"  ShowMessageBox="false" ShowSummary="true" />
                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" ValidationGroup="login" CssClass="btn btn-lg btn-primary btn-block" Text="Sign In" />
               <div class="text-center"><asp:Label ID="lblstatus" runat="server" Text="" Font-Bold="true" Font-Size="12px"  ForeColor="Red"></asp:Label></div> 

            </div>
                </asp:Panel>
        </form>
    </div>
    <script src='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/js/jquery-1.10.2.js")) %>'></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src='<%# Common.GetLatestVersion(Page.ResolveUrl("admincss/js/bootstrap.min.js")) %>'></script>
</body>
</html>
