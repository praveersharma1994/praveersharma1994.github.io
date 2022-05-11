<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DiscountCoupon.aspx.cs" Inherits="admin_DiscountCoupon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script type="text/javascript">
       function digits(obj, e, allowDecimal, allowNegative) {

           var key; var isCtrl = false; var keychar; var reg; if (window.event) { key = e.keyCode; isCtrl = window.event.ctrlKey } else if (e.which) { key = e.which; isCtrl = e.ctrlKey; }
           if (isNaN(key)) return true; keychar = String.fromCharCode(key); if (key == 8 || isCtrl) { return true; }
           reg = /\d/; var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false; var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false; return isFirstN || isFirstD || reg.test(keychar);
       }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="update1">
        <ProgressTemplate>
            <div style="position: fixed; top: 0; left: 0; background: url(images/tab_bg.png) 0 0 repeat; width: 100%; height: 100%; opacity: 1; z-index: 9999;">
                <div style="text-align: center; margin: 20% 0 0 0; opacity: 1;">
                    <img src="images/loading.gif" alt="Loading"  title="Loading" /><br />
                    Please wait while Processing....
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="update1" UpdateMode="Always">
        <ContentTemplate>
         <div class="scrollable wrapper">
                <!--row start-->
                <div class="row">
                    <!--col-md-12 start-->
                    <div class="col-md-12">
                        <div class="page-heading">
                            <h1>Discount Coupon</h1>
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

                            <!--form-horizontal row-border start-->
                            <div class="form-horizontal row-border" action="">


                                <!--adv-table start-->
                                <div class="adv-table">

                            <fieldset>
                                <legend>
                                    <asp:Literal runat="server" ID="ltrlHeading1"></asp:Literal></legend>
                                <div style="width: 80%; margin: auto;">

                               <div class="form-group">
                                    <label class="col-sm-3 control-label">Discount</label>
                                    <div class="col-sm-9">
                                       <asp:TextBox runat="server" CssClass="input-xlarge focused" ID="txtName" onkeypress="return digits(this,event,true,false)" MaxLength="6"></asp:TextBox> &nbsp;<a href="#">%</a>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtName" ErrorMessage="Discount is required !" Display="Dynamic" ValidationGroup="item" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>                                 
                                                                     
                                    <div class="form-group">
                                    <label class="col-sm-3 control-label">Discount Code</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" CssClass="input-xlarge focused" ID="txtCode" MaxLength="10" ReadOnly="false"></asp:TextBox> &nbsp; <asp:LinkButton ID="lnk_getguid" runat="server" OnClick="lnk_newguidclick">Get New Code</asp:LinkButton>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtCode" ErrorMessage="Code is required !" Display="Dynamic" ValidationGroup="item" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>





                                   <div class="form-group">
                                    <label class="col-sm-3 control-label">Status</label>
                                    <div class="col-sm-9">
                                    <asp:CheckBox ID="chk_active" runat="server"  /> &nbsp;<a href="#">(Check for Active)</a> 
                                           
                                        </div>

                                    </div>

                                     <div class="form-group">
                                    <label class="col-sm-3 control-label">Expiry Date (dd/MM/yyyy)</label>
                                    <div class="col-sm-9">
                                       <asp:TextBox runat="server" CssClass="input-xlarge focused" ID="txtexpirydate" MaxLength="10"></asp:TextBox> 
                                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtexpirydate" Format="dd/MM/yyyy"  ></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtexpirydate" ErrorMessage="Expiry Date is required !" Display="Dynamic" ValidationGroup="item" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>   

                                 <%--    <div class="form-group">
                                    <label class="col-sm-3 control-label">Expiry Date</label>
                                    <div class="col-sm-9">
                                       <asp:TextBox runat="server" CssClass="input-xlarge focused" ID="TextBox1" MaxLength="10"></asp:TextBox> 
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtexpirydate" Format="dd/MM/yyyy"  ></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtexpirydate" ErrorMessage="Expiry Date is required !" Display="Dynamic" ValidationGroup="item" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>  --%>

                                </div>
                                <div class="form-actions" style="padding: 15px;">
                                    <div style="width: 40.5%; margin: auto;">
                                        <asp:Button runat="server" ID="btnSave" Text="Submit" BorderWidth="0" ValidationGroup="item" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                        <asp:Button runat="server" ID="btnClear" Style="margin-left: 10px;" Text="Clear" CssClass="btn  btn-primary" OnClick="btnClear_Click" />
                                        <asp:ValidationSummary runat="server" ID="validationSummery" ShowMessageBox="true" ShowSummary="false" ValidationGroup="chng" />
                                        <asp:HiddenField runat="server" ID="hddType" />
                                        <asp:HiddenField runat="server" ID="hddId" />
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset>
                                <legend style="margin-top: 20px; margin-bottom: 50px;">
                                    <asp:Literal runat="server" ID="ltrlListHeading"></asp:Literal>
                                    <div class="span10" style="text-align: right; float: right">
                                        <asp:TextBox runat="server" placeholder="Discount Code" CssClass="input-xlarge focused" ID="txtSearchName"></asp:TextBox>
                                        &nbsp;
                                 

                                        <asp:Button runat="server" ID="btnSearch" Text="Search" Style="height: 25px!important;" CssClass=" btn btn-info" OnClick="btnSearch_Click" />
                                        &nbsp;
                                    <asp:DropDownList runat="server" Width="70px" ID="drpPageSize" AutoPostBack="True" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                        <asp:ListItem Value="10">10</asp:ListItem>

                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                    </asp:DropDownList>
                                        <span style="font-size: 12px;">Records per page</span>
                                    </div>
                                </legend>
                                <div style="width: 100%; margin: auto;">

                                    <div class="span12" style="margin: 0px;">
                                        <asp:GridView runat="server" EmptyDataText="No Record found" PagerStyle-CssClass="pages" AllowPaging="true" Width="100%" ID="grdList" GridLines="None" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#f1f1f1" CssClass="table table-bordered" OnSelectedIndexChanging="grdList_SelectedIndexChanging" OnRowCommand="grdList_RowCommand" OnPageIndexChanging="grdList_PageIndexChanging" OnRowDataBound="grdList_RowDataBound">
                                            <Columns>

                                                <asp:TemplateField HeaderText="#S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount (%)">
                                                    <ItemTemplate>
                                                        <%#Eval("DiscountPerc")%>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount Code">
                                                    <ItemTemplate>
                                                        <%#Eval("DiscountCode") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Expiry Date">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime(Eval("editedon")).ToString("dd/MM/yyyy") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                      <%--  <%#Eval("displayOrder") %>--%>
                                                        <asp:CheckBox ID="chk_status" runat="server"  />
                                                        <asp:HiddenField ID="hd_status" runat="server" Value='<%#Eval("status") %>' />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnEdit" CommandArgument='<%#Eval("Id") %>' CommandName="edt" CssClass="btn btn-primary"><i class="icon-pencil icon-white"></i> Edit</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="72px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure ?')" CommandArgument='<%#Eval("Id") %>' CommandName="del" CssClass="btn btn-danger"><i class="icon-remove icon-white"></i> Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="85px" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </fieldset>

  </div>

                            </div>

                        </div>

                    </div>




                </div>
                <!--row end-->

                <!--row end-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

