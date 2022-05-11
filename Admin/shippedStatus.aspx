<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shippedStatus.aspx.cs" Inherits="admin_shippedStatus" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .rating_heading {
            width: 25%;
        }

        .ajax__calendar_today {
            padding-top: 0px!important;
            height: 20px!important;
        }

        .reviewbox {
            width: 100%;
            float: left;
            font-size: 13px;
            border-top: dashed 1px #ccc;
            padding-top: 5px;
            line-height: 20px;
        }

        .reviewRow {
            width: 100%;
            float: left;
            padding: 3px 0px;
        }

        .reviewTitle {
            width: 25%;
            float: left;
        }

        .reviewValue {
            width: 75%;
            float: left;
        }



        .reviewRow textarea {
            padding: 6px 5px;
            border: 1px solid #ccc;
            font-size: 13px;
            float: left;
            width: 100%;
            resize: none;
        }

        .reviewField {
            width: 48%;
            float: left;
        }

        .reviewRow span {
            float: left;
        }

        .reviewRow input[type="text"] {
            padding: 6px 5px;
            border: 1px solid #ccc;
            font-size: 13px;
            float: left;
            width: 100%;
        }

        .reviewRow select {
            padding: 4px 5px;
            border: 1px solid #ccc;
            font-size: 13px;
            float: left;
            width: 100%;
        }

        .reviewRow label {
            padding: 6px 5px;
            float: left;
            width: 14%;
        }

        .btn {
            background: #ff2a40;
            padding: 6px 20px;
            color: #fff;
            border: none;
            font-size: 14px;
        }
    </style>
</head>
<body style="height: 450px;">
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager runat="server" ID="script"></asp:ScriptManager>
            <div id="clientReview" style="width: 700px; z-index: 999991; float: left; height: 450px;">

                <h3 style="margin: 2px 0px; font-weight: normal;">Add Shipping Detail of
                 <span style="color: red">
                     <asp:Literal runat="server" ID="ltrlOrdero"></asp:Literal></span>
                </h3>
                <div class="reviewbox">
                    <div class="reviewRow">
                        <span class="rating_heading">Client</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:Literal runat="server" ID="ltrlClient"></asp:Literal>
                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">Order Date</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:Literal runat="server" ID="ltrlOrdDate"></asp:Literal>
                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">Shipped By</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:DropDownList runat="server" ID="drpShippedBy"></asp:DropDownList>
                        </div>
                    </div>
                 <%--   <div class="reviewRow">
                        <span class="rating_heading">Order Status</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:DropDownList runat="server" ID="drpOrderStatus"></asp:DropDownList>
                        </div>
                    </div>--%>
                    <div class="reviewRow">
                        <span class="rating_heading">Track No</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:TextBox runat="server" ID="txtTrackNo"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="req" ControlToValidate="txtTrackNo" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" Font-Size="11px" ErrorMessage="is required" ValidationGroup="Ratting"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">Shipped Date</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:TextBox runat="server" ID="txtShippedDate"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtShippedDate" ID="cal1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtShippedDate" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" Font-Size="11px" ErrorMessage="is required" ValidationGroup="Ratting"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">Expected Deliver Date</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:TextBox runat="server" ID="txtExpDeliverDate"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtExpDeliverDate" ID="CalendarExtender1" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtExpDeliverDate" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" Font-Size="11px" ErrorMessage="is required" ValidationGroup="Ratting"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">Remark</span> <span class="rading_box"></span>
                        <div class="reviewField">
                            <asp:TextBox runat="server" ID="txtRemark" Height="150px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="reviewRow">
                        <span class="rating_heading">&nbsp;</span>
                        <div class="reviewField">
                            <asp:Button runat="server" ID="btnReview" Text="Submit" CssClass="button btn" ValidationGroup="Ratting" OnClick="btnReview_Click" />
                        </div>
                    </div>
                </div>



            </div>

        </div>
    </form>
</body>
</html>
