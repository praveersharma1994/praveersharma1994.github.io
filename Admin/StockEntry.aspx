<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="StockEntry.aspx.cs" Inherits="Admin_StockEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function opendiv() {
            $("#dvedit").click();
        }

        function changeurl() {
            window.history.pushState("", "", window.location.href.split("?")[0]);
        }
    </script>
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
                    <div class="col-md-12">
                        <div class="page-heading">
                            <h1>Stock Entry</h1>
                        </div>
                    </div>
                </div>
                <!--row end-->

                <ul class="nav nav-tabs">
                    <li class="active"><a href="#addstock" id="dvadd" data-toggle="tab">Add</a></li>
                    <li class=""><a href="#editstock" id="dvedit" data-toggle="tab">Edit</a></li>
                </ul>

                <div class="tab-content">
                    <div class="tab-pane fade active in" id="addstock">
                        <!--row start-->
                        <div class="row">
                            <!--box-info start-->
                            <div class="box-info">
                                <!--form-horizontal row-border start-->
                                <div class="form-horizontal row-border">
                                    <!--adv-table start-->
                                    <div class="adv-table">
                                        <div role="grid" class="dataTables_wrapper" id="dynamic-table_wrapper">
                                            <!-- col-md-6 start -->
                                            <div class="col-md-6">
                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Collection </label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpCollection" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ControlToValidate="drpCollection" Font-Size="11px" InitialValue="0" Text="Collection  is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Category </label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpCategory" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ControlToValidate="drpCategory" Font-Size="11px" InitialValue="0" Text="Category  is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Material </label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpMaterial" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ControlToValidate="drpMaterial" Font-Size="11px" InitialValue="0" Text="Material  is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Size </label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpsize" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ControlToValidate="drpsize" Font-Size="11px" InitialValue="0" Text="Size  is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                                <%-- <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Avl Sizes: </label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtavlsizes" class="form-control model" ClientIDMode="Static" placeholder="comma separated sizes ex. S,M,L,XL"></asp:TextBox>
                                                    </div>
                                                </div>--%>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Color </label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpcolor" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <%-- <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Avl Colors: </label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtavlcolors" class="form-control model" ClientIDMode="Static" placeholder="comma separated colors ex. Red,Green"></asp:TextBox>
                                                    </div>
                                                </div>--%>

                                                 <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Avl Variations: </label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtvariations" class="form-control model" ClientIDMode="Static" placeholder="comma separated size-color combinations"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Gender Type</label>
                                                    <div class="col-sm-9">
                                                        <asp:DropDownList runat="server" ID="drpgender" class="form-control" ClientIDMode="Static">
                                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                            <asp:ListItem Text="Kids" Value="K"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">SKU *</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtSKU" class="form-control model" onblur="chksku()" ClientIDMode="Static"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtSKU" ForeColor="Red" Font-Size="11px" Text="SKU No is required" SetFocusOnError="true" runat="server" ID="req" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                        <label class="" id="notexist"></label>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">MRP</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtmrp" class="form-control" onkeypress="return digits(this,event,true,false)" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">SRP</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtsrp" class="form-control" onkeypress="return digits(this,event,true,false)" ClientIDMode="Static"></asp:TextBox>
                                                    </div>
                                                </div>

                                            


                                            </div>
                                            <div class="col-md-6">

                                                    <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Qty</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtQty" class="form-control" onkeypress="return digits(this,event,false,false)" Text="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtQty" ForeColor="Red" Font-Size="11px" Text="Qty is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator13" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Title</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" ID="txtTitle" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Description </label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtDescription" ForeColor="Red" Font-Size="11px" InitialValue="0" Text="Description is required" SetFocusOnError="true" runat="server" ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="cat"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <asp:GridView runat="server" ID="grdFeatures" AutoGenerateColumns="false" ShowHeader="false" Width="100%" GridLines="None" OnRowDataBound="grdFeatures_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <div class="row form-group" style="margin-bottom: 0px;">
                                                                    <label class="col-sm-3 control-label"><%#Eval("FeatureName") %></label>
                                                                    <div class="col-sm-9">
                                                                        <asp:HiddenField runat="server" ID="hddFeatureId" Value='<%#Eval("FeatureId").ToString() %>'></asp:HiddenField>
                                                                        <asp:TextBox runat="server" ID="txtValue" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Main Image(600 X 600) </label>
                                                    <div class="col-sm-7">
                                                        <asp:FileUpload runat="server" ID="fluMain" onchange="readURL(this)" />
                                                        <asp:HiddenField runat="server" ID="hddImg" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div style="width: 73px; height: 73px; display: table-cell; vertical-align: middle; text-align: center; background: #fff; border: solid 1px #ccc; padding: 2px;">
                                                            <asp:Image runat="server" ID="imgPriview" Style="max-height: 70px; max-width: 70px;" ImageUrl="../images/noimage.jpg" onerror="this.src='../images/noimage.jpg'" ClientIDMode="Static" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <label class="col-sm-3 control-label">Other Images (600 X 600)</label>
                                                    <div class="col-sm-9">
                                                        <asp:FileUpload type="file" runat="server" ID="fluOtherImg" multiple="multiple" class="input-xlarge focused" />
                                                        <%--  <asp:HiddenField runat="server" ID="hddotherimage" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hddimgcount" runat="server" ClientIDMode="Static" />--%>
                                                    </div>
                                                </div>

                                                <div class="row form-group">
                                                    <asp:Repeater ID="repimages" runat="server">
                                                        <ItemTemplate>
                                                            <div class="col-md-3 col-sm-4 mb10">
                                                                <img class='img-responsive' src="../upload/Products/OtherSmall/<%#Container.DataItem %>" />
                                                                <span><a class="text-danger" id="btndel" onclick="del('<%# Container.DataItem  %>',this)"><i class="fa fa-trash-o"></i></a></span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <!-- col-md-6 end -->

                                            <div class="row">
                                                <div class="col-sm-9 col-sm-offset-4">
                                                    <div class="btn-toolbar">
                                                        <asp:Button runat="server" ID="btnSave" class="btn-success btn btn-fill" ValidationGroup="cat" OnClick="btnSave_Click" Text="Submit" />
                                                        <asp:Button runat="server" ID="btnCancel" class="btn-default btn btn-fill" OnClick="btnCancel_Click" Text="Cancel" />
                                                        <asp:HiddenField runat="server" ID="hddId" ClientIDMode="Static" />
                                                        <asp:HiddenField runat="server" ID="removedfile" ClientIDMode="Static" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="tab-pane" id="editstock">
                        <div>
                            <label style="float: right;">
                                Show
                                            <asp:DropDownList runat="server" ID="drpPagging" Width="60px" aria-controls="dynamic-table" AutoPostBack="true" OnSelectedIndexChanged="drpPagging_SelectedIndexChanged">
                                                <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="500">500</asp:ListItem>
                                                <asp:ListItem Value="50000">All</asp:ListItem>
                                            </asp:DropDownList>
                                entries</label>
                        </div>
                        <div class="row">
                            <div class="col-md-12 table-responsive" id="divExport" runat="server">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView2_RowCommand" EmptyDataText="No Record Found?" EmptyDataRowStyle-ForeColor="Red" PageSize="100" AllowPaging="true" CssClass="display table table-bordered table-striped dataTable" OnPageIndexChanging="GridView2_PageIndexChanging">
                                    <PagerSettings Position="Bottom" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <img width="50" src='../upload/Products/small/<%#Eval("image") %>' onerror="this.src='../upload/Products/noimage.jpg'" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SKU No">
                                            <ItemTemplate>
                                                <%#Eval("SKUName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Category">
                                            <ItemTemplate>
                                                <%#Eval("categoryname") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <%#Eval("title") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRP">
                                            <ItemTemplate>
                                                <%#Eval("MRP") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SRP">
                                            <ItemTemplate>
                                                <%#Eval("SRP") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%#Eval("id") %>' ID="btnedit" CommandName="edititem" CssClass="btn btn2 btn-info" runat="server"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <asp:LinkButton CommandArgument='<%#Eval("id") %>' ID="btnDelete" CommandName="deleteitem" OnClientClick="return confirm('Are You Sure You Want To Continue?')" CssClass="btn btn2 btn-danger" runat="server"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    function readURL(input) {
                        if (input.files && input.files[0]) {
                            var reader = new FileReader();
                            reader.onload = function (e) {
                                var image = document.getElementById("<%=imgPriview.ClientID%>");
                                var extention = input.files[0].name.split('.').pop().toLocaleLowerCase();
                                if (extention === "jpg" || extention === "png" || extention === "jpeg") {
                                    if (input.files[0].size <= 2e+6) {
                                        image.setAttribute("src", e.target.result);
                                    }
                                    else {
                                        ShowNotify("File size must be less then or equal 2MB", "error");
                                        $(input).val("");
                                        $(".imgPriview").prop("src", "../images/NoImage.jpg");
                                    }
                                }
                                else {
                                    ShowNotify("Only .jpg file type allowed!", "error");
                                    $(input).val("");
                                    $(".imgPriview").prop("src", "../images/NoImage.jpg");
                                }
                            }

                            reader.readAsDataURL(input.files[0]);
                            }
                        }

                        function chksku() {
                            if ($("#txtSKU").val() != "") {
                                var id = "0";
                                if ($("#hddId").val() != "") { id = $("#hddId").val(); }
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    url: 'StockEntry.aspx/checkExist',
                                    data: "{'sku':'" + $("#txtSKU").val() + "','pid':'" + id + "'}",
                                    dataType: "json",
                                    success: function (response) {
                                        if (response.d == "1") {
                                            $("#txtSKU").val('');
                                            $("#notexist").addClass("alert alert-danger hwecustomalert").text("SKU is not available");
                                        }
                                        else if (response.d == "2") {
                                            $("#notexist").addClass("alert alert-danger hwecustomalert").text("Error");
                                        }
                                        else {
                                            $("#notexist").removeClass("alert alert-danger hwecustomalert").text("");
                                        }
                                    },
                                    error: function (err) {
                                        alert("error");
                                    }
                                });
                            }
                        }

                        function del(imgname, ids) {
                            $(ids).parent().parent().remove();
                            $("#removedfile").val($("#removedfile").val() + imgname + ",");
                        }
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

