<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Notification.aspx.cs" Inherits="Admin_Appusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        $(document).keydown(function (event) {
            if (event.keyCode == 116) {
                location.href = 'notification.aspx';
                return false;
            }
        });
    </script>
    <style>
        .danger {
            color: red;
        }
    </style>
    <script>
        $(document).ready(function () {
            $('#chkNewArrivalAll').click(function () {
                if ($('#chkNewArrivalAll').is(':checked')) {
                    $('input:checkbox[name$=chkNewArrival]').each(
                        function () {
                            var id = $(this).attr('id');
                            document.getElementById(id).checked = true;
                        });
                }
                else {
                    $('input:checkbox[name$=chkNewArrival]').each(
                        function () {
                            var id = $(this).attr('id');
                            document.getElementById(id).checked = false;
                        });
                }
            });
            $('input:checkbox[name$=chkNewArrival]').click(function () {
                if (!$(this).is(':checked')) {
                    var id = $('#chkNewArrivalAll').attr('id');
                    document.getElementById(id).checked = false;
                }
                else {
                    if ($('input:checkbox[name$=chkNewArrival]').length ==
                    $('input:checkbox[name$=chkNewArrival]:checked').length) {
                        var id = $('#chkNewArrivalAll').attr('id');
                        document.getElementById(id).checked = true;
                    }
                }
            });
        });
        function fun() {
            $('#chkNewArrivalAll').click(function () {
                if ($('#chkNewArrivalAll').is(':checked')) {
                    $('input:checkbox[name$=chkNewArrival]').each(
                        function () {
                            var id = $(this).attr('id');
                            document.getElementById(id).checked = true;
                        });
                }
                else {
                    $('input:checkbox[name$=chkNewArrival]').each(
                        function () {
                            var id = $(this).attr('id');
                            document.getElementById(id).checked = false;
                        });
                }
            });
            $('input:checkbox[name$=chkNewArrival]').click(function () {
                if (!$(this).is(':checked')) {
                    var id = $('#chkNewArrivalAll').attr('id');
                    document.getElementById(id).checked = false;
                }
                else {
                    if ($('input:checkbox[name$=chkNewArrival]').length ==
                    $('input:checkbox[name$=chkNewArrival]:checked').length) {
                        var id = $('#chkNewArrivalAll').attr('id');
                        document.getElementById(id).checked = true;
                    }
                }
            });
        }

        function hihepopup() {
            $(".modal-backdrop").removeClass("modal-backdrop");
            $("body").removeClass("modal-open");
        }
    </script>
    <asp:UpdatePanel runat="server" ID="upd">
        <Triggers>
            <asp:PostBackTrigger ControlID="btndownloadexcel" />
        </Triggers>
        <ContentTemplate>
            <div class="hwemain-container">
                <!--content start-->
                <div class="agile-tables">
                    <!--row start-->
                    <div class="row">
                        <!--col-md-12 start-->
                        <div class="col-md-12">
                            <div class="text-center">
                                <h3>App User</h3>
                                <hr />
                            </div>
                        </div>
                        <!--col-md-12 end-->
                    </div>
                    <!--row end-->

                    <!--row start-->
                    <div class="row">
                        <!--col-md-6 start-->
                        <div class="col-md-12">
                            <!--box-info start-->
                            <div class="box-info">
                                <div class="row form-group">
                                    <div class="col-sm-2">
                                        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                            <asp:ListItem Value="all">Filter Users</asp:ListItem>
                                            <asp:ListItem Value="isactive">Active</asp:ListItem>
                                            <asp:ListItem Value="notactive">Not Active</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtsearch" runat="server" placeholder="user name/ Name/ Email" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" CssClass="btn btn-primary" Text="Search" />
                                        <button type="button" class="btn btn-info btn-md" data-toggle="modal" data-target="#myModal">Send Notification</button>
                                        <asp:Button ID="btndeleteuser" runat="server" OnClick="btndeleteuser_Click" CssClass="btn btn-primary" OnClientClick="return confirm('Are You sure to delete selected users !')" Text="Delete Selected Users" />
                                        <asp:Button ID="btndownloadexcel" runat="server" OnClick="btndownloadexcel_Click" CssClass="btn btn-primary"  Text="Download Excel" />
                                    </div>
                                    <div id="dynamic-table_length" class="dataTables_length  col-sm-1">
                                        <asp:DropDownList runat="server" ID="drpPagging" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                            <asp:ListItem Value="250">250</asp:ListItem>
                                            <asp:ListItem Value="500">500</asp:ListItem>
                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <!--form-horizontal row-border start-->
                                <div class="form-horizontal row-border">
                                    <!--adv-table start-->
                                    <div class="adv-table">
                                        <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                            <div class="table-responsive">
                                                <asp:GridView AutoGenerateColumns="false" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" PageSize="100" AllowPaging="true" runat="server" ID="gvProduct" CssClass="display table table-bordered table-striped dataTable" aria-describedby="dynamic-table_info" OnPageIndexChanging="gvProduct_PageIndexChanging" >
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <input id="chkNewArrivalAll" type="checkbox" style="height: 15px!important; text-align: center; " title="Select All" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkNewArrival" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Literal runat="server" ID="ltrSNO" Text='<%# Container.DataItemIndex+1 %>' />
                                                                <asp:HiddenField runat="server" ID="hddId" Value='<%# Eval("id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="id" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Literal runat="server" ID="ltrid" Text='<%#Eval("id") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUser" runat="server" Text='<%#Eval("firstname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdress" runat="server" Text='<%#Eval("address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="City">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcity" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="State">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstate" runat="server" Text='<%#Eval("state") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Zip">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblzip" runat="server" Text='<%#Eval("zip") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Contact No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcontactno" runat="server" Text='<%#Eval("contactno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Email">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UserName">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Literal runat="server" ID="ltrLogin" Text='<%#Eval("username") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Password">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPwd" runat="server" Text='<%#Eval("Password") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Convert.ToDateTime(Eval("createdate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="From">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblregfrom" runat="server" Text='<%#Eval("registerfrom") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
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
                    </div>
                    <!--row end-->

                    <!-- Modal -->
                    <div class="modal fade" id="myModal" role="dialog">
                        <div class="modal-dialog modal-md">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Notification</h4>
                                </div>
                                <div class="modal-body">
                                    <label>Title</label><asp:TextBox ID="txttitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    <label>Message</label><asp:TextBox ID="txtmessage" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnsendnotification" runat="server" ClientIDMode="Static" CssClass="btn btn-info" OnClick="btnsendnotification_Click" Text="Send Notification" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--row end-->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

