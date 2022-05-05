<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="cfb_contactus.aspx.vb" Inherits="cfb_cfb_contactus" MaintainScrollPositionOnPostback="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
  <h3><strong>Contact us</strong></h3>

  <table width="100%">
<tr>
<td width="150">Subject</td>
<td>
    <asp:TextBox ID="txtsubject" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtsubject" Display="Dynamic" 
        ErrorMessage="Please enter Subject"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="150">Message</td>
<td>
    <asp:TextBox ID="txtdetail" runat="server" Rows="5" TextMode="MultiLine" 
        Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtdetail" Display="Dynamic" 
        ErrorMessage="Please enter Message"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="150" class="style2">From</td>
<td class="style2">
    <asp:TextBox ID="txtfrom" runat="server" Width="500px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtfrom" Display="Dynamic" 
        ErrorMessage="Please enter your name"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td width="150">&nbsp;</td>
<td>
    <asp:Button ID="cmdSendMail" runat="server" Text="Send Message"  />
    &nbsp;<asp:Button ID="cmdCancel" runat="server" CausesValidation="False" 
        Text="Cancel" />
    </td>
</tr>
</table>
</div>
</asp:Content>

