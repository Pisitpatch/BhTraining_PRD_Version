<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_contact.aspx.vb" Inherits="srp_srp_contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
  <h3><strong>Contact us</strong></h3>

  <table width="100%">
<tr>
<td width="250">หัวข้อ / Subject</td>
<td>
    <asp:TextBox ID="txtsubject" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtsubject" Display="Dynamic" 
        ErrorMessage="กรุณาระบุหัวข้อ"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="250">ข้อความ / Message</td>
<td>
    <asp:TextBox ID="txtdetail" runat="server" Rows="5" TextMode="MultiLine" 
        Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtdetail" Display="Dynamic" 
        ErrorMessage="กรุณาระบุข้อความที่ต้องการ"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="250" class="style2">ชื่อและนามสกุลผู้ส่ง / Sender name</td>
<td class="style2">
    <asp:TextBox ID="txtfrom" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtfrom" Display="Dynamic" 
        ErrorMessage="กรุณาระบุชื่อของท่าน"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="250">อีเมล์ / Email</td>
<td>
    <asp:TextBox ID="txtemail" runat="server" Width="500px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
    </td>
</tr>
<tr>
<td width="250">แผนก / Department Name</td>
<td>
    <asp:TextBox ID="txtdeptname" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="txtdeptname" Display="Dynamic" 
        ErrorMessage="กรุณาระบุชื่อแผนกของท่าน"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="250">เบอร์ติดต่อกลับ / Contact Phone number</td>
<td>
    <asp:TextBox ID="txttel" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="txttel" Display="Dynamic" 
        ErrorMessage="กรุณาระบุเบอร์ติดต่อกลับ"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="250">&nbsp;</td>
<td>
    <asp:Button ID="cmdSendMail" runat="server" Text="Send Message" 
        Font-Bold="True"  />
    &nbsp;<asp:Button ID="cmdCancel" runat="server" CausesValidation="False" 
        Text="Cancel" />
    </td>
</tr>
</table>
</div>
</asp:Content>

