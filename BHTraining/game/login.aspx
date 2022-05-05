<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="game_login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panel_login" runat="server" DefaultButton="cmdLogin">
    <table id="signin" width="500" align="center" cellpadding="5" cellspacing="0" style="border: solid 1px #CCCCCC; background: #FFFFFF;">
    
    <tr>
      <td style="background: url(../images/boxheader.gif); font-size: 15px; font-weight: bold;">Welcome to Bumrungrad Way Training</td>
    </tr>
    <tr>
      <td>Please enter your Employee ID:<br />
       <asp:TextBox  ID= "txtusername" runat="server" Width="185px"></asp:TextBox>
       <br />
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
      <td style="background: #f0f0f0; text-align: left;">
          <asp:Button ID="cmdLogin" runat="server" Text="Sign In" 
              CssClass="button-green" />  
        &nbsp;</td>
    </tr>
  </table>
  </asp:Panel>
  <br />
<div style="width:100%" align="center">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
     
      <td  valign="top">      

      </td>
    </tr>
  </table>
  </div>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
</asp:Content>



