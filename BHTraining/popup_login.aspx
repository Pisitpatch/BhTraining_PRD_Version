<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_login.aspx.vb" Inherits="popup_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Continue session, please login again</title>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../js/jquery-1.6.min.js" charset="utf-8"></script>
     <script type="text/javascript">
         function closeWindow() {
             parent.continueSession("<%response.write(empCode) %>", "<%response.write(viewtype) %>");
             parent.$.akModalRemove();
             return false;
         }
        
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="panel_login" runat="server">
    <table id="signin" width="100%" align="center" cellpadding="6" cellspacing="1">
  
    <tr>
      <td style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #909e91;">
          To continue please enter your username and password again</td>
    </tr>
    <tr>
      <td>Username:<br />
       <asp:TextBox  ID= "txtusername" runat="server" Width="185px" ReadOnly="true" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtusername" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
      <td>Password:<br />
     <asp:TextBox  ID="txtpassword" Width="185"  runat="server" TextMode="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
              ControlToValidate="txtpassword" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
      <td>
      Authentication Type:
          <br />
          <asp:DropDownList ID="txtlopgintype" runat="server">
              <asp:ListItem Value="1">Database</asp:ListItem>
              <asp:ListItem Value="2" Selected="True">Active Directory</asp:ListItem>
          </asp:DropDownList>
        &nbsp;<asp:Label ID="lblError" runat="server" Text="" ForeColor="#FF3300"></asp:Label> &nbsp;</td>
    </tr>
    <tr>
      <td>
         
        </td>
    </tr>
    </table>
    </asp:Panel>
    <table id="Table1" width="100%" align="center" cellpadding="6" cellspacing="1">
    <tr>
      <td style="background: #f0f0f0; text-align: center;">
          <asp:Button ID="cmdLogin" runat="server" Text="Sign In" 
              CssClass="button-green" />  

               <asp:Button ID="cmdContinue" runat="server" Text="Click to continue"  Visible= "false"
              CssClass="button-green" OnClientClick="return closeWindow();" />  
          &nbsp;</td>
    </tr>
  </table>
    </div>
    </form>
</body>
</html>
