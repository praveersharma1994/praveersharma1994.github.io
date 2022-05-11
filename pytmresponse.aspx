<%@ Page Title="" Language="C#" MasterPageFile="~/Clientmaster.master" AutoEventWireup="true" CodeFile="pytmresponse.aspx.cs" Inherits="pytmresponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Transaction Response</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <style>
     
        /*th {
            font-size: 14px;
            background: #f1c677;
            color: #000000;
            font-weight: bold;
            height: 30px;
        }*/

        td {
            font-size: 13px;
        }

      .pageTitle {
            font-size: 24px;
        }
        @media screen and (min-width:991px) {
            .jovifashion-section.header + #mainWrap {
                margin-top: 165px;
                padding-bottom: 35px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cp1" Runat="Server">
    <style>
        .hwesecone h2 {
            text-align: center;
            font-size: 22px;
            margin-top: 0px;
            padding: 6px;
            /*margin-top: 30px;*/
            background-color: #da712d;
            color: white;
            margin-bottom: 0px;
        }

        .t2link {
            margin-top: 30px;
            text-align:center;
        }

        .t2link a {
                color: #fff;
                text-decoration: underline;
            }

        .contentautocenter {
            width: auto;
            margin: 0 auto;
        }

        .hwesecone {
            width: 70%;
            margin: 0 auto;
            text-align: center;
            border: 1px solid #dededc;
            font-size: 15px;
            margin: 30px auto;
            margin-bottom: 150px;
            display: table;
            max-width: 100%;
        }

        .hero-wrapper03 {
            padding-top: 96px;
        }

        .hwesecone table.total-result {
            margin: 0 auto;
            min-width: 250px;
        }

        .hwesecone table tr th {
            background-color: #40403e;
            color: #fff;
            font-weight: normal;
            text-align: center;
            border: 1px solid #40403e;
            font-size: 15px !important;
            text-transform: uppercase;
            padding: 1px 5px;
        }

        .hwesecone table tr td {
            border: 1px solid #ccc;
            font-size: 14px !important;
        }

        .boxhwebg1 {
            padding: 0 10px 10px 10px;
            font-family: Calibri;
            /*border: 1px solid #ccc;*/
            /*border-radius: 5px;*/
            font-weight: 600;
        }

        .boxhwebg1 tr {
                border-bottom: 1px solid #f9f9f9;
            }

        .boxhwebg1 tr td {
                    border: 0px !important;
                    font-size: 15px !important;
                    line-height: 28px;
                }

        .boxhwebg1 tr td.fieldName {
                        text-transform: uppercase;
                        text-align: left;
                    }

        .boxhwebg {
            padding: 10px 10px 0px 10px;
            font-family: Calibri;
            /*border: 1px solid #ccc;*/
            /*border-radius: 5px;*/
            font-weight: 600;
        }

        .boxhwebg tr {
                border-bottom: 1px solid #f9f9f9;
            }

        .boxhwebg tr td {
                    border: 0px !important;
                    font-size: 15px !important;
                    line-height: 28px;
                }
 
        .boxhwebg tr td.fieldName {
                        text-transform: uppercase;
                        text-align: left;
                    }

        .t2link a {
            /*background-color: #000;*/
            color: #000;
            padding: 5px 10px;
            text-decoration: none;
            font-size: 19px;
        }

        .width-tbl {
            width:35%;
        }

        @media (min-width: 320px) and (max-width:600px) {
           .width-tbl { width:50%;}
}

    </style>

    <section style="background: #FFF">
        <div class="container">
            <div class="col-sm-12 animated fadeInRightNow notransition fadeInRight">
                <div class="t2link"><a href="https://www.SStyleFactory.com/">&laquo; Go To Home Page</a></div>
                <div class="hwesecone">
                    <h2>
                        <asp:Label ID="ltrlResponce" runat="server" Style="text-transform: uppercase;"></asp:Label>&nbsp;!!!                                    
                        <asp:HiddenField runat="server" ID="hfcurrencyrate" />
                    </h2>
                    <div class="contentautocenter">
                        <div class="tab-content">
                            <div class="boxhwebg">
                                <table class="total-result">
                                    <tbody>
                                        <tr>
                                            <td class="fieldName width-tbl" style="text-align: left; width:200px;">Transaction Id </td>
                                            <td align="left">:
                                                <asp:Literal runat="server" ID="ltrlTransactionId"></asp:Literal>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="fieldName width-tbl" style="text-align: left; width:200px;">Order No.</td>
                                            <td align="left">:
                                                <asp:Literal runat="server" ID="ltrorderno"></asp:Literal>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="fieldName" style="text-align: left;">Payment Id </td>
                                            <td align="left">:
                                                <asp:Literal runat="server" ID="ltrlPaymentId"></asp:Literal>
                                            </td>
                                        </tr>--%>
                                        <tr>

                                            <td class="fieldName width-tbl" style="text-align: left;">Amount </td>
                                            <td align="left">:
                                                <asp:Literal runat="server" ID="ltrlAmt"></asp:Literal>
                                                <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div style="padding:10px;" class="table-resposive">
                    <asp:GridView ID="grvorddetail" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="false" GridLines="None" Width="100%">
                        <FooterStyle />
                        <RowStyle />
                        <HeaderStyle />

                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                    <%-- <asp:HiddenField ID="hfloginid" runat="server" Value='<%# Eval("UserId") %>' />--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" Height="50px"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <img id="imgitem" width="50" height="50" style="border: none;" src='<%#"https://www.SStyleFactory.com/upload/products/small/"+ Eval("Img") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Height="55px"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label ID="lblproductname" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Barcode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpid" runat="server" Text='<%# Eval("Pid") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="ItemCode">
                                <ItemTemplate>
                                    <asp:Label ID="lblproductcode" runat="server" Text='<%# Eval("ModelNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblcurr" runat="server" Text='<%# Eval("OrderCurrency") %>'></asp:Label>
                                    <asp:Label ID="lblprice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="text-center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblorderstatus" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lblcurr1" runat="server" Text='<%# Eval("OrderCurrency") %>'></asp:Label>
                                    <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"  CssClass="text-center"  />
                                <HeaderStyle HorizontalAlign="Center"  />
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    </div>
                    <div>
                        <div class=" hamararight">
                            <div class="tab-content">
                                <div class="boxhwebg1">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="fieldName width-tbl">Total
                                            </td>
                                            <td style="text-align: left;">:&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltotalamount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="fieldName width-tbl">Discount
                                            </td>
                                            <td style="text-align: left;">:&nbsp;&nbsp;&nbsp;<asp:Label ID="lbldiscountamt" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="fieldName width-tbl">Shipping
                                            </td>
                                            <td style="text-align: left;">:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblShipping" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="fieldName width-tbl">Grand
                                            </td>
                                            <td style="text-align: left; width: 100px;">:&nbsp;&nbsp;&nbsp;<asp:Label ID="lblgrand" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="clear: both"></div>
                </div>

                <div class="hwesectwo">
                    <div class="col-sm-4">&nbsp;</div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

