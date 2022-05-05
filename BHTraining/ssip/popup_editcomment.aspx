<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_editcomment.aspx.vb" Inherits="ssip_popup_editcomment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
    <script type="text/javascript">
        function closePopup() {
            window.opener.disablePopup();
            window.close();
        }
      </script>
</head>
<body onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div>
       <asp:Panel ID="panelAddComment" runat="server">
                <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"><strong>Review By</strong></td>
                        <td bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">


                            <tr>
                              <td width="156" valign="top"><strong><asp:Label ID="lblJobType" runat="server" Text=""></asp:Label></strong></td>
                              <td width="180" valign="top"><input name="txtname" type="text" id="txtname" style="width: 180px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="159" valign="top"><input name="txtjobtitle" type="text" id="txtjobtitle" style="width: 150px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="189" valign="top"><input name="txtdeptname" type="text" id="txtdeptname" style="width: 180px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="120"><input name="txtdatetime" type="text" disabled="disabled" id="txtdatetime" style="width: 120px;" value="" readonly="readonly" runat="server" /></td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Comment Subject</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td>
                                  <asp:TextBox ID="txtadd_subject" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3"><strong>Comment Detail</strong></td>
                        <td bgcolor="#eef1f3"><textarea name="txtcomment_detail" id="txtcomment_detail" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3">&nbsp;</td>
                        <td bgcolor="#eef1f3">
                            <asp:Button ID="cmdSave1" runat="server" 
                                OnClientClick="alert('Save completed')" Text="Save" Width="100px" />
                            <input type="button" value="Close" style="width: 100px;"  onclick="closePopup();" />
                        </td>
                      </tr>
                    </table>
                    </asp:Panel>
    </div>
    </form>
</body>
</html>
