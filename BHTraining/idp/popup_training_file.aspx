<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_training_file.aspx.vb" Inherits="idp_popup_training_file" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>IDP File</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
  <script type="text/javascript" >
      function closePopup() {
          window.opener.disablePopup();
          window.close();
      }
  </script>
</head>
<body onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div>
    <table>
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="120"><strong>File Attachment</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table>
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top"><table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload1" runat="server"  />
                      <asp:Button ID="cmdUpload"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdDeleteFile" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFile" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/ext_training/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView></td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
    </table>
    <input type="button" id="cmdClose" name="cmdClose" value="Close window" onclick="closePopup();" />
    </div>
    </form>
</body>
</html>