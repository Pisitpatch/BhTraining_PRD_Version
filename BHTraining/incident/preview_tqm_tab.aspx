<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_tqm_tab.aspx.vb" Inherits="incident_preivew_tqm_tab" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
</head>
<body>
    <form id="form1" runat="server">
    <page backimg="bhbg.gif" backbottom="10mm" style="font-family: cordiaupc"><page_footer>
    <div id="data">
   <div><table style="width: 100%; border: solid 1px black;"><tr><td style="text-align: left;width: 70%">33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667 2525  www.bumrungrad.com</td><td style="text-align: right;width: 30%"><img src="../images/bhfooter.gif" width="120"/></td></tr></table></div>
     <table width="100%" border="0" cellpadding="2" cellspacing="2">
    <tr>
      <td align="center" style="text-align:center"><strong>รายงานอุบัติการณ์</strong></td>
    </tr>
    <tr>
      <td align="center" style="text-align:center"><strong>Accident & Incident Reports : Part of TQM
          </strong></td>
    </tr>
    </table>
    
  
   
<fieldset>

          <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" >
        <tr>
          <td width="20%" ><b>
              <asp:Label ID="lblIRtqm1" runat="server" Text="Grand Topic" />
              </b></td>
          <td  >
              <asp:Label ID="txtgrandtopic" runat="server" Text=""></asp:Label>
                       </td>
        </tr>
        <tr>
          <td width="20%">
              <b>
              <asp:Label ID="lblIRtqm2" runat="server" Text="Topic" />
              </b>
            </td>
          <td>
           <asp:Label ID="txtnormaltopic" runat="server" Text=""></asp:Label>
             
            </td>
        </tr>
          <tr>
              <td class="style3" width="20%">
                  <b>
                  <asp:Label ID="lblIRtqm3" runat="server" Text="Subtopic 1" />
                  </b>
              </td>
              <td class="style3">
                  <asp:Label ID="txtsubtopic1" runat="server" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
              <td class="style3" width="20%">
                  <b>
                  <asp:Label ID="lblIRtqm4" runat="server" Text="Subtopic 2" />
                  </b>
              </td>
              <td class="style3">
                  <asp:Label ID="txtsubtopic2" runat="server" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
              <td class="style3" width="20%">
                  <strong>Subtopic 3</strong></td>
              <td class="style3">
                  <asp:Label ID="txtsubtopic3" runat="server" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
              <td style="vertical-align:top" width="20%">
                  <b>Topic Detail</b></td>
              <td>
                  <asp:Label ID="txttqm_detail" runat="server" 
                      Width="650px"></asp:Label>
              </td>
          </tr>
          <tr>
              <td style="vertical-align:top" width="20%">
                  <b>Case Owner</b></td>
              <td>
                  <asp:Label ID="txtcase_owner" runat="server"  Rows="3" 
                      ></asp:Label>
                  &nbsp;</td>
          </tr>
      </table></td>
    </tr>
              <tr>
                  <td valign="top">
                  <table width="100%" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                    <strong>Acknowledge department</strong></td>
              </tr>
              <tr>
                  <td valign="top">
                      <asp:Label ID="txtinfo_dept2" runat="server"  Width="250px"></asp:Label>
                  </td>
              </tr>
            </table>
                    <br />
                    <table width="100%" cellspacing="0" cellpadding="0">
           

                  <tr>
                  <td valign="top">
                      <strong>Defendant Unit</strong></td>
              </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="txtunit_defandent_select" runat="server" 
                                     Width="250px">
                                </asp:Label>
                            </td>
                        </tr>
            </table><br />
                     <asp:GridView ID="GridTQMCause" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="tqm_cause_id" HeaderStyle-CssClass="colname" 
                          EnableModelValidation="True">
           <Columns>
               <asp:TemplateField HeaderText="Cause of Incident">
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" Text='<%# Bind("cause_name") %>'></asp:Label>
                   </ItemTemplate>
                
                   <ItemStyle Width="35%" />
                    <HeaderStyle Width="35%" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Description">
                <ItemStyle Width="35%" />
                 <HeaderStyle Width="35%" />
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("cause_remark") %>'></asp:Label>
                   </ItemTemplate>
                 
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Unit Defendant">
                 <ItemStyle Width="30%" />
                 <HeaderStyle Width="30%" />
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("dept_unit_name") %>'></asp:Label>
                   </ItemTemplate>
                 
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
       
                    
                  </td>
              </tr>
    <tr>
      <td valign="top">&nbsp;</td>
    </tr>
    <tr>
      <td valign="top"><strong>More Information</strong></td>
    </tr>
              <tr>
                  <td valign="top">
                      <asp:Label ID="txttqm_remark" runat="server" Text=""></asp:Label>
                     </td>
              </tr>
              <tr>
                  <td valign="top">
                      <strong><asp:Label ID="lblIRtqm7" runat="server" Text="Action" 
                          /></strong>
                  </td>
              </tr>
    <tr>
      <td valign="top">
       <asp:Label ID="txtaction_tqm_detail" runat="server" Text=""></asp:Label>
     </td>
    </tr>
     <tr>
     <td>
     <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
          <td width="30%"><strong><asp:Label ID="lblIRtqm9" Text="Severity Level" runat="server" 
                   />
                   </strong></td>
          <td >
              <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
             </td>
          </tr>
        
     </table>
     </td>
     </tr>      
    <tr>
      <td valign="top">
      <table width="100%" cellspacing="0" cellpadding="0" >
        <tr>
          <td width="30%"><strong><asp:Label ID="lblIRtqm10" Text="Quality concern" runat="server" 
                   /></strong></td>
          <td ><asp:Label ID="txttqmconcern1"  runat="server" />
            </td>
          </tr>
        <tr>
          <td width="30%"><strong><asp:Label ID="lblIRtqm13" Text="Refer to IMCO Medical Support" runat="server" /></strong></td>
          <td><asp:Label ID="txttqmrefer1" runat="server" />
            </td>
          </tr>
        </table></td>
    </tr>
    </table>
</fieldset><br />
   
   
   <strong>Involving Physician</strong>
<br />
<table width="100%">
   <tr>
                  <td valign="top">
                      <asp:GridView ID="GridViewTQMDoctor" runat="server" AutoGenerateColumns="False" 
                          CellPadding="4" GridLines="Horizontal" Width="100%" BackColor="White" 
                         
                          DataKeyNames="defendant_id" EnableModelValidation="True"  CssClass="tdata" >
                          <RowStyle BackColor="White" ForeColor="#333333" />
                          <Columns>
                              <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="10%" />
               <HeaderStyle Width="10%" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                              <asp:TemplateField HeaderText="Involving Physician">
                               <ItemStyle  Width="40%" />
               <HeaderStyle Width="40%" />
                                  <ItemTemplate>
                                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("doctor_name") %>'></asp:Label>
                                  </ItemTemplate>
                               
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Specialty">
                                 <ItemStyle  Width="50%" />
               <HeaderStyle Width="50%" />
                                  <ItemTemplate>
                                      <asp:Label ID="Label2" runat="server" Text='<%# Bind("specialty") %>'></asp:Label>
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                          </Columns>
                          <FooterStyle BackColor="White" ForeColor="#333333" />
                          <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                  <HeaderStyle CssClass="colname" />
                      </asp:GridView>
                  </td>
              </tr>
    <tr>
      <td valign="top"></td>
    </tr>
</table>

<br />
<fieldset>
<legend>Log Book</legend>
<br />
<table width="100%" >
<tr><td width="150" style="vertical-align:top"><strong>Patient Safety Goals</strong></td><td>
    <asp:Label ID="txtlog_safety" runat="server" Text=""></asp:Label>
  
    </td></tr>
    <tr>
        <td style="vertical-align:top" width="150">
            P<strong>atient Safety Goals 2</strong></td>
        <td>
         <asp:Label ID="txtlog_safety2" runat="server" Text=""></asp:Label>
            
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            <strong>Lab category</strong></td>
        <td>
          <asp:Label ID="txtlog_lab" runat="server" Text=""></asp:Label>
          
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            <strong>ASA classification</strong></td>
        <td>
          <asp:Label ID="txtlog_asa" runat="server" Text=""></asp:Label>
          
        </td>
    </tr>
   
</table>
</fieldset>
<br />
<fieldset>
  <legend><asp:Label ID="lblIRtqm16" Text="Related Customer Feedback" runat="server" /></legend>
  <br />
  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top">
        <table width="100%" cellspacing="1" cellpadding="2">
            <tr>
                <td>
                    <asp:GridView ID="GridRelateCFB" runat="server"  AutoGenerateColumns="False"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="tdata" DataKeyNames="ir_relate_document_id" 
                        EnableModelValidation="True" >
                         
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="Labels0" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CFB No.">
                               <ItemStyle  Width="90%" />
                                <HeaderStyle Width="90%" />
                                <ItemTemplate>
                                    CFB<asp:Label ID="Label1" runat="server" Text='<%# Bind("reference_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <HeaderStyle CssClass="colname" />
               <PagerStyle ForeColor="Black" HorizontalAlign="Center" 
                  BorderStyle="None" Font-Bold="False" />
              
              <AlternatingRowStyle BackColor="White" />

                    </asp:GridView>
                </td>
            </tr>
          </table>
       </td>
    </tr>
    </table>
</fieldset>
<br />
<fieldset>
  <legend><asp:Label ID="Label4" Text="Related Accident & Incident Reports" runat="server" /></legend>
  <br />
  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top">
        <table width="100%" cellspacing="1" cellpadding="2">
            <tr>
                <td>
                    <asp:GridView ID="GridRelateIR" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" CssClass="tdata" DataKeyNames="ir_relate_document_id" 
                        EnableModelValidation="True" GridLines="None" Width="100%">
                      
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="Labels1" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IR No.">
                             <ItemStyle  Width="90%" />
                                <HeaderStyle Width="90%" />
                                <ItemTemplate>
                                    IR<asp:Label ID="Label6" runat="server" Text='<%# Bind("reference_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                         <HeaderStyle CssClass="colname" />
                        <PagerStyle BorderStyle="None" Font-Bold="False" ForeColor="Black" 
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
          
          </table>
       </td>
    </tr>
    </table>
</fieldset>
<br />
<fieldset>
  <legend><asp:Label ID="Label7" Text="Repeat Accident & Incident Reports" runat="server" /></legend>
  <br />
  <table width="100%">
    <tr>
                <td width="20%" class="style3">
                    <strong>Report Type</strong></td>
                <td class="style3" width="80%">
                 <asp:Label ID="txtreporttype" runat="server"></asp:Label>
                   
                </td>
            </tr>
      <tr>
          <td width="20%" >
              <strong>Repeat IR No.</strong></td>
          <td width="80%">
              <asp:Label ID="txtrepeatIR" runat="server"></asp:Label>
          </td>
      </tr>
   
  </table>
  </fieldset>


    </div>
    </form>
</body>
</html>
