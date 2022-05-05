<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_budget.aspx.vb" Inherits="idp_preview_budget" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
          <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title> Preview </title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="data">
     <h3 style="text-decoration:underline">Estimate Budget</h3><br />
      <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td valign="top"><span class="theader"><strong>External Training No.</strong></span></td>
                  <td><strong><asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label>
                    
                      </strong>
                        <asp:Label ID="lblViewtype" runat="server" Text="Label" Visible="False"></asp:Label>
                      </td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="100"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="100"><strong>Cost Center</strong></td>
                        <td width="230"><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                        <td width="80">&nbsp;</td>
                        <td></td>
                      </tr>
                  </table></td>
                </tr>
              </table>
              <br />
                <asp:GridView ID="GridExpense" runat="server" 
             AutoGenerateColumns="False" CellPadding="3" CssClass="tdata" 
             DataKeyNames="expense_id" 
             EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)" 
             EnableModelValidation="True" Width="100%">
                    <HeaderStyle  />
                    <Columns>
                       
                     
                        <asp:TemplateField HeaderText="ประเภทค่าใช้จ่าย/Expense">
                            <ItemTemplate>
                               <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' 
                                    Visible="false"></asp:Label>
                                <asp:Label ID="lblExpensetypeID" runat="server" 
                                    Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblReqBudget" runat="server" 
                                    Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label>
                                <br />
                                -
                                <asp:Label ID="Label4" runat="server" ForeColor="BlueViolet" 
                                    Text='<%# Bind("create_by") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="จำนวนเงิน/Amount">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                &nbsp;
                                <asp:Label ID="lblValue" runat="server" 
                                    Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                                &nbsp;<asp:Label ID="lblcurtype" runat="server" 
                                    Text='<%# Eval("currency_type_name") %>'></asp:Label>
                                <asp:Label ID="lblcurtypeid" runat="server" 
                                    Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" 
                                    ForeColor="BlueViolet"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="อัตราแลกเปลี่ยน/Exchange rate">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="expense_request_type_name" 
                            HeaderText="ประเภทการเบิก/Payment" />
                        <asp:BoundField DataField="expense_payment_name" 
                            HeaderText="วิธีชำระเงิน/Payment method" />
                        <asp:BoundField DataField="expense_remark" HeaderText="หมายเหตุ/Remark" />
                       
                    </Columns>
         </asp:GridView>
           <div><strong>Total Budget</strong> :  <asp:Label ID="txttotal" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Baht <strong>Request 
                Budget</strong>  <asp:Label ID="txtrequest_budget" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Baht
                        </div>
    
    </div>
    </form>
</body>
</html>
