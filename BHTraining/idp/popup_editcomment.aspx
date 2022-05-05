<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_editcomment.aspx.vb" Inherits="idp_popup_editcomment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
    <style type="text/css">
        .style2
        {
            height: 93px;
        }
    </style>
      <script type="text/javascript" >
          function closePopup() {
              window.opener.disablePopup();
              window.close();
          }
  </script>
</head>
<body  onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panelAddComment" runat="server">
                    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"><strong>Review By</strong></td>
                        <td bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">


                            <tr>
                              <td width="156" valign="top"><strong><asp:Label ID="lblJobType" runat="server" Text=""></asp:Label></strong></td>
                              <td width="180" valign="top">
                                  <input name="txtname" type="text" id="txtname" style="width: 180px" value="" runat="server" disabled="disabled" /></td>
                              <td width="159" valign="top">
                                  <input name="txtjobtitle" type="text" id="txtjobtitle" style="width: 150px" value="" runat="server" disabled="disabled" /></td>
                              <td width="189" valign="top">
                                  <input name="txtdeptname" type="text" id="txtdeptname" style="width: 180px" value="" runat="server" disabled="disabled" /></td>
                              <td width="120"><input name="txtdatetime" type="text" disabled="disabled" id="txtdatetime" style="width: 120px;" value="" readonly="readonly" runat="server" /></td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Acknowledge</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td width="80">
                                  <asp:DropDownList ID="txtacknowedge_status" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                                  </td>
                              <td width="25">&nbsp;</td>
                              <td width="158"><strong>Comment Subject</strong></td>
                              <td>
                                 <asp:DropDownList ID="txtcomment" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                     <asp:ListItem Value="101">มีงบประมาณ</asp:ListItem>
                                      <asp:ListItem Value="102"> มีงบประมาณและอยู่ใน IDP</asp:ListItem>
                                      <asp:ListItem Value="103">นอกงบประมาณ</asp:ListItem>
                                      <asp:ListItem Value="104">นอกงบประมาณและอยู่ใน IDP</asp:ListItem>
                                      <asp:ListItem Value="105">หัวข้ออบรมไม่สอดคล้องกับแผนพัฒนาหรือตำแหน่งงาน</asp:ListItem>
                                      <asp:ListItem Value="106">อื่นๆ</asp:ListItem>
                                   
                                  </asp:DropDownList>
                            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3" class="style2"><strong>Comment Detail</strong></td>
                        <td bgcolor="#eef1f3" class="style2"><textarea name="txtcomment_detail" id="txtcomment_detail" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3">&nbsp;</td>
                        <td bgcolor="#eef1f3">&nbsp;<asp:Button ID="cmdSave1" runat="server" Text="Save" OnClientClick="alert('Save completed')"  Width="100px"   Font-Bold="True" />
                            <input type="button" value="Close" style="width: 100px;"  onclick="closePopup();" />
                              </td>
                      </tr>
                    </table>
                    </asp:Panel>
    </div>
    </form>
</body>
</html>
