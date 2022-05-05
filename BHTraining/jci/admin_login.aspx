<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_login.aspx.vb" Inherits="jci_admin_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table id="signin" width="500" align="center" cellpadding="5" cellspacing="0" style="border: solid 1px #CCCCCC; background: #FFFFFF;">
    
    <tr>
      <td style="background: url(../images/boxheader.gif); font-size: 15px; font-weight: bold;">Administrator Control Panel</td>
    </tr>
    <tr>
      <td>Enter your Employee ID:<br />
       <asp:TextBox  ID= "txtusername" runat="server" Width="185px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtusername" ErrorMessage="Please enter your Employee ID"></asp:RequiredFieldValidator>
        </td>
    </tr>
 
 
    <tr>
      <td>
         
          <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" Text=""></asp:Label>
         
        </td>
    </tr>
    <tr>
      <td style="background: #f0f0f0; text-align: center;">
          <asp:Button ID="cmdLogin" runat="server" Text="Sign In" 
              CssClass="button-green" />  
        &nbsp;or&nbsp;<a href="#" style="color: #C00; text-decoration: underline;">Cancel</a></td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
</asp:Content>

