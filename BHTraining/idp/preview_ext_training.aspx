<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_ext_training.aspx.vb" Inherits="idp_preview_ext_training" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
           <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title> Preview </title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div id="data">
         <h3 style="text-decoration:underline">External Training Information</h3>
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
                  <td><asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                      </td>
                </tr>
                <tr>
                  <td valign="top"><strong>Name</strong></td>
                  <td><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                       </td>
                </tr>
                <tr>
                  <td valign="top"><strong>Cost Center</strong></td>
                  <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Job Title</strong></td>
                  <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                </tr>
              </table>
              <br />
               <asp:Panel ID="panel_detail" runat="server">
       <table width="100%" cellspacing="0" cellpadding="5">
              <tr>
                <td valign="top" >
                <asp:Label ID="lblIDPTitle" runat="server" Text="หัวข้อที่ต้องการฝึกอบรม " Font-Bold="true" />
                </td>
                <td valign="top" >
                    <asp:Label ID="txttitle" runat="server" Text="Label"></asp:Label>
                </td>
              </tr>
              <tr>
                <td valign="top" >
                <asp:Label ID="lblIDPJoin" runat="server" Text="การเข้าร่วม " Font-Bold="true" />
               </td>
                <td valign="top" >
                 <asp:Label ID="txttype" runat="server" Text="Label"></asp:Label>
                   
               </td>
              </tr>
              <tr>
                <td valign="top" >
                    <strong>ประเภทการอบรม</strong></td>
                <td valign="top" >
                  
                    <asp:Label ID="txtcategory" runat="server" Text="Label"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td valign="top">
                      <asp:Label ID="lblIDPMotivation" runat="server" Font-Bold="true" Text="แรงจูงใจในการเข้าอบรมหัวข้อนี้ " />
                  </td>
                  <td valign="top">
                      <asp:Label ID="txtmotivation" runat="server" Text="" />
                  </td>
              </tr>
              <tr>
                <td valign="top" >
                 <asp:Label ID="lblIDPCourse" runat="server" Text="หลักสูตร " Font-Bold="true" />
               </td>
                <td valign="top" >
                <asp:Label ID="txtcourse_detail" runat="server" Text=""  />
                
                 </td>
              </tr>
              <tr>
                <td valign="top" >
                <asp:Label ID="lblIDPExpect" runat="server" Text="ประโยชน์ที่ท่านคาดว่าจะได้รับจาการอบรมครั้งนี้ " Font-Bold="true" />
               </td>
                <td valign="top" >
                    <asp:Label ID="txtexect_detail" runat="server" Text="Label"></asp:Label>
                
               </td>
              </tr>
              <tr>
                <td valign="top" >
                 <asp:Label ID="lblIDPTrainType" runat="server" Text="ประเภทการอบรม " Font-Bold="true" />
               </td>
                <td valign="top" ><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335">
                      <asp:Label ID="txttraintype" runat="server" Text="Label"></asp:Label>
                         
                      </td>
                      <td width="80">&nbsp;</td>
                      <td>&nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td width="150" valign="top" >
                  <asp:Label ID="lblIDPFacility" runat="server" Text="สถานที่อบรม" Font-Bold="true" />
                </td>
                <td valign="top" >
                <asp:Label ID="txtfacility" runat="server" Text="Label"></asp:Label>
               
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top" >
                <asp:Label ID="lblIDPInstitute" runat="server" Text="สถาบันที่จัดอบรม " Font-Bold="true" />
                </td>
                <td valign="top" >
                 <asp:Label ID="txtinstitution" runat="server" Text="Label"></asp:Label>
              
                  </td>
              </tr>
              <tr>
                <td valign="top" >
                <asp:Label ID="lblIDPPlace" runat="server" Text="จังหวัด/ประเทศ " Font-Bold="true" />
                </td>
                <td valign="top" >
                  <asp:Label ID="txtplace" runat="server" Text="Label"></asp:Label>
                  
                  </td>
              </tr>
              <tr>
                <td valign="top" ><asp:Label ID="lblIDPDate" runat="server" Text="วันที่อบรม " Font-Bold="true" /></td>
                <td valign="top" ><table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                    <tr>
                      <td width="300">
                          <asp:Label ID="txtdate1" runat="server" Text="Label"></asp:Label>
                      &nbsp;
                          -
                       &nbsp;
                      <asp:Label ID="txtdate2" runat="server" Text="Label"></asp:Label>
                       </td>
                      <td width="40">&nbsp;</td>
                      <td>
                       &nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" ><asp:Label ID="lblIDPTrainHour" runat="server" Text="ชั่วโมงการทำงาน " Font-Bold="true" /></td>
                <td valign="top" ><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="150">
                        <asp:Label ID="txthour" runat="server" Text="Label"></asp:Label>
                        
                        &nbsp;&nbsp;hrs.</td>
                    </tr>
                </table></td>
              </tr>
              

                <tr>
                    <td  valign="top">
                        <asp:Label ID="lblIDPBudget" runat="server" Font-Bold="true" 
                            Text="การขอเบิกค่าใช้จ่าย " />
                    </td>
                    <td  valign="top">
                        <asp:Label ID="txtrequestBudget" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
              

              <tr>
                <td colspan="2" valign="top" >
                <strong><asp:Label ID="lblIDPResvTopic" runat="server" Text="การสมัครอบรม" /></strong>
                <br />
                <asp:Label ID="txtregisterType" runat="server" Text="" /> 
                </td>
              </tr>
            </table>
            </asp:Panel>
            <br />
            <h3>Budget Request</h3>
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
        <br />
        <h3>Actual Expense</h3>
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
                    <td  style="height:30px; width:250px" valign="top">
                        <strong>Budget approved amount</strong></td>
                    <td  valign="top">
                        <asp:Label ID="lblApproveBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td  style="height:30px" valign="top">
                        <strong>Budget approved by</strong></td>
                    <td  valign="top">
                        <asp:Label ID="lblApproveName" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  style="height:30px" valign="top">
                        <strong>Received amount</strong></td>
                    <td  valign="top">
                        <asp:Label ID="lblReturnBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td  style="height:30px" valign="top">
                        <strong>Total expense</strong></td>
                    <td  valign="top">
                        <asp:Label ID="txtactual_expense" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="#006600" Text="0"></asp:Label>
                        Baht</td>
                </tr>
              </table>
 <br />
        <h3>Approve and comment</h3>
         <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px" BorderStyle="None">
                          <Columns>
                              <asp:TemplateField>
                              <EditItemTemplate>
                              test
                              </EditItemTemplate>
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td colspan="2" bgcolor="#DBE1E6">
                                            </td>
                    </tr>
                    <tr>
                    
                      <td bgcolor="#EDF4F9"><table width="100%" border="0">
                        <tr>
                          <td valign="top"><strong>
                          <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                          </strong>
                          <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label>
,                          
<asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label> 
,
                          <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                          <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
                         </td>
                          <td align="right"><div align="right">
                              <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                            
                          </div></td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><strong>
                        <asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />                        
                        <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label>
: </strong>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label></td>
                    </tr>
                  </table>

                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>

    </div>
    </form>
</body>
</html>
