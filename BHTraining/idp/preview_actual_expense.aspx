<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_actual_expense.aspx.vb" Inherits="idp_preview_actual_expense" %>

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
     <h3 style="text-decoration:underline">Actual Expense</h3><br />
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
                <asp:GridView ID="GridExpense2" runat="server" CssClass="tdata" CellPadding="3" 
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                    DataKeyNames="expense_id" 
                    EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)">
                  <HeaderStyle   />
                  <Columns>
                                     
                      <asp:TemplateField HeaderText="ประเภทค่าใช้จ่าย/Expense">
                          <ItemTemplate>
                            <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("is_delete") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblTopicID" runat="server" 
                                  Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                             <asp:Label ID="lblExpensetypeID" runat="server" 
                                  Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblReqBudget" runat="server" 
                                  Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label>
                              <br />
                              -  
                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_by") %>' 
                                  ForeColor="BlueViolet"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server" 
                                  Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงิน/Amount">
                         <ItemStyle />
                          <ItemTemplate>
                              &nbsp;                     
                              <asp:Label ID="lblValue" runat="server" 
                                  Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                               &nbsp;<asp:Label ID="lblcurtype" runat="server" 
                                  Text='<%# Eval("currency_type_name") %>'></asp:Label>
                               <asp:Label ID="lblcurtypeid" runat="server" 
                                  Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" 
                                  ForeColor="BlueViolet" ></asp:Label>
                                
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงินอนุมัติ/Approve Budget">
                         
                          <ItemTemplate>
                              <asp:Label ID="lblApprove" runat="server"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="อัตราแลกเปลี่ยน/Exchange rate">
                        
                          <ItemTemplate>
                              <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="วันที่ทำรายการ/Date" DataField="create_date" />
                      <asp:TemplateField HeaderText="ผู้มาติดต่อ/Contact Person">
                      
                          <ItemTemplate>
                              <asp:Label ID="textPersonContact" runat="server" 
                                  Text='<%# Bind("acc_receive_by") %>' Width="80px"></asp:Label>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="บันทึกโดย/Record by" DataField="acc_expense_by" />
                      <asp:TemplateField HeaderText="ประเภท/Method">
                        
                          <ItemTemplate>
                              <asp:Label ID="lbltype_id" runat="server" 
                                  Text='<%# Bind("expense_request_type_id") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblpayment_id" runat="server" 
                                  Text='<%# Bind("expense_payment_id") %>' Visible="false"></asp:Label>
                              <asp:label ID="txttype" runat="server" Text='<%# Bind("expense_request_type_name") %>' >
                              
                            
                              </asp:label>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="วิธีชำระเงิน/Payment">
                         
                          <ItemTemplate>
                                 <asp:label ID="txtpayment" runat="server" Text='<%# Bind("expense_payment_name") %>' >
                                
                              </asp:label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="หมายเหตุ/Remark">
                          <ItemTemplate>
                              <asp:Label ID="txtremark" runat="server" Text='<%# Bind("expense_remark") %>' 
                                  Width="100"></asp:Label>
                            
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                     
                  </Columns>
              </asp:GridView>
              <table width="100%">
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Approved amount</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblApproveBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Approved by</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblApproveName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Received amount</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblReturnBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Total expense</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="txtactual_expense" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="#006600" Text="0"></asp:Label>
                        Baht</td>
                </tr>
              </table>
    
    </div>
    </form>
</body>
</html>
