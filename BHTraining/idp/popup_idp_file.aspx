<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_idp_file.aspx.vb" Inherits="idp_popup_idp_file" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Import Namespace="ShareFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>IDP File</title>
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
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="data">
    <table width="98%">
      <tr>
                <td valign="top"  width="120"><strong>File Attachment</strong></td>
                <td valign="top" ><table>
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
                      <asp:Button ID="cmdDeleteFile" runat="server" Text="Delete" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                </td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
    </table>
    <br />
       <asp:GridView ID="GridFile" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px" GridLines="None" CssClass="tdata3"
              Width="100%" EnableModelValidation="True" 
            EmptyDataText="There is no data.">
               <RowStyle BackColor="#eef1f3" />
                  <Columns>
                  <asp:TemplateField>
                   <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                      <asp:TemplateField HeaderText="Action Date">
                          <ItemStyle VerticalAlign="Top" Width="120px" />
                          <ItemTemplate>
                                 <asp:TextBox ID="txtdate1" runat="server" BackColor="#FFFF99" 
                        Width="80px" Text='<%# ConvertTSToDateString(Eval("date_action_ts")) %>'></asp:TextBox>
                          &nbsp;<asp:Image ID="Image2" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image2" 
                        TargetControlID="txtdate1">
                        </asp:CalendarExtender>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Remark">
                        <ItemStyle VerticalAlign="Top" Width="200px" />
                          <ItemTemplate>
                              <asp:TextBox ID="txtremark1" runat="server" TextMode="MultiLine" Rows="3" Text='<%# Bind("action_remark") %>'></asp:TextBox>
                          </ItemTemplate>
                      </asp:TemplateField>
                  <asp:TemplateField HeaderText="File Attach">
                   <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
                </asp:GridView>
                <br />
                   <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" Font-Bold="true" />&nbsp;
      <input type="button" id="cmdClose" name="cmdClose" value="Close window" onclick="closePopup();" />
      &nbsp;</div>
    </form>
</body>
</html>
