<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cfb_service_recovery.aspx.vb" Inherits="cfb.cfb_cfb_service_recovery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Recovery</title>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        window.opener.loadPopup(1);
    });
</script>
</head>
<body onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="panelDetail" runat ="server">
 <div class="alert">
<div class="heading">Customer Feedback</div>
<div style="padding: 0px 10px;">
<div style="margin-top: 15px; margin-bottom: 10px;"></div>
<fieldset>
  <legend></legend><table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td width="385" valign="top"><asp:Label ID="lblCFBcomment1" 
            runat="server" Text="Describe the occurence/ feedback"></asp:Label>
      </td>
    </tr>
  <tr>
    <td valign="top">
      <textarea name="textarea" id="txtfeedback" cols="45" rows="3" style="width: 685px;" runat="server"></textarea></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment2" 
            runat="server" Text="Describe the solution/ responding to customer"></asp:Label>
        </td>
    </tr>
  <tr>
    <td valign="top">
      <textarea name="textarea2" id="txtsolution" cols="45" rows="3" style="width: 685px;" runat="server"></textarea></td>
  </tr>
  <tr>
    <td valign="top">
    <asp:Label ID="lblCFBcomment3" 
            runat="server" Text="Relevant Unit"></asp:Label>
    </td>
  </tr>
  <tr>
    <td valign="top">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
      <asp:GridView ID="GridDept" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4"
              Width="735px" ShowHeader="False" DataKeyNames="cfb_relate_dept_id" 
                  ForeColor="#333333" GridLines="None">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit name">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("cfb_relate_dept_id") %>' Visible="false"></asp:Label>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                 
                  <asp:CommandField ShowDeleteButton="True">
                  <ItemStyle Width="80px" />
                  </asp:CommandField>
              </Columns>
               <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <EditRowStyle BackColor="#999999" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          </asp:GridView>
    <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
        <tr>
          <td>
            <asp:DropDownList ID="txtadd_dept" runat="server" Width="450px" 
                  DataTextField="dept_name_en" DataValueField="dept_id" >
              </asp:DropDownList>
              <asp:Button ID="cmdAdd" runat="server" Text="Add" width="45px" />
</td>
          </tr>

      </table>
      </ContentTemplate>
        </asp:UpdatePanel>
      </td>
  </tr>
  </table>
</fieldset><br />
<fieldset id="service" runat="server">
  <legend>
      <asp:Label ID="lblCFBcomment4" runat="server" Text="Service Recovery Management"></asp:Label></legend><table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td width="385" valign="top">
     <asp:Label ID="lblCFBcomment5" runat="server" Text=" Able to anticipate the problem before customer 
        complains"></asp:Label>
   </td>
    <td><table width="100%" cellspacing="0" cellpadding="0" style="margin-left: -3px;">
      <tr>
        <td width="23">
            <asp:RadioButton ID="txts1" GroupName="g1" runat="server" />
          </td>
        <td width="100"> <asp:Label ID="lblCFBcomment6" runat="server" Text="Yes" /></td>
        <td width="23"> <asp:RadioButton ID="txts2" GroupName="g1" runat="server" /></td>
        <td><asp:Label ID="lblCFBcomment7" runat="server" Text="No" /></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment8" runat="server" Text="Evaluate customer" /></td>
    <td><table width="100%" cellspacing="0" cellpadding="0" style="margin-left: -3px;">
      <tr>
        <td width="23">  <asp:RadioButton ID="txts11" GroupName="g2" runat="server" /></td>
        <td width="100"><asp:Label ID="lblCFBcomment9" runat="server" Text="Satisfied" /></td>
        <td width="23">  <asp:RadioButton ID="txts22" GroupName="g2" runat="server" /></td>
        <td><asp:Label ID="lblCFBcomment10" runat="server" Text="Dissatisfied and made complaint" /></td>
      </tr>
    </table></td>
  </tr>
  </table>
</fieldset><br />
<fieldset id="complain" runat="server">
  <legend></legend><table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment11" runat="server" Text="Complaint Management" /></td>
    <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td width="23">  <asp:RadioButton ID="txtcom1" GroupName="c1" runat="server" /></td>
        <td width="100"><asp:Label ID="lblCFBcomment12" runat="server" Text="Yes" /></td>
        <td width="23">  <asp:RadioButton ID="txtcom2" GroupName="c1" runat="server" /></td>
        <td><asp:Label ID="lblCFBcomment13" runat="server" Text="No" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td width="180" valign="top"><asp:Label ID="lblCFBcomment14" runat="server" Text="Customer" /></td>
    <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td width="23" valign="top">
            <asp:DropDownList ID="txtcustomer" runat="server">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">Satisfied and not requested to respond back</asp:ListItem>
            <asp:ListItem Value="2">Satisfied and requested to respond back</asp:ListItem>
            <asp:ListItem Value="3">Dissatisfied and not requested to respond back</asp:ListItem>
            <asp:ListItem Value="4">Dissatisfied and requested to respond back</asp:ListItem>
            </asp:DropDownList>
         </td>
      </tr>
      <tr>
        <td valign="top">(please identify)&nbsp;&nbsp;          <input type="text" name="txtcus_detail" id="txtcus_detail" style="width: 285px" runat="server" /></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment19" runat="server" Text="Request to respond back by" /></td>
    <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td width="23">
            <asp:CheckBox ID="chk_tel" runat="server" Text="" />
          </td>
        <td width="150"><asp:Label ID="lblCFBcomment20" runat="server" Text="Telephone" /></td>
        <td><input type="text" name="txttel" id="txttel" style="width: 335px" runat="server" /></td>
        </tr>
      <tr>
        <td>
            <asp:CheckBox ID="chk_email" runat="server" Text="" />
          </td>
        <td><asp:Label ID="lblCFBcomment21" runat="server" Text="E-mail" /></td>
        <td><input type="text" name="txtemail" id="txtemail" style="width: 335px" runat="server" /></td>
        </tr>
      <tr>
        <td>
            <asp:CheckBox ID="chk_othter" runat="server" Text="" />
          </td>
        <td><asp:Label ID="lblCFBcomment22" runat="server" Text="Other (please identify)" /></td>
        <td><input type="text" name="txtother" id="txtother" style="width: 335px" runat="server" /></td>
        </tr>
      </table></td>
  </tr>
  </table>
</fieldset><br />
<div align="center" style="margin-top: 15px; margin-bottom: 10px;">
    <asp:Button ID="cmdSave" runat="server" Text="Save"  Width="120px" 
        Font-Bold="True" />
    
  <input type="button" value="Close" style="width: 100px;"  onclick="window.opener.disablePopup(); window.close();" />
</div>
</div>
</div>
</asp:Panel>
    </form>
</body>
</html>
