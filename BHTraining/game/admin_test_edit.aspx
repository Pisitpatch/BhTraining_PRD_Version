<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_test_edit.aspx.vb" Inherits="game_admin_test_master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
<table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24" align="absmiddle" /> Add/Edit Test</td>
  </tr>
</table>
<table width="100%" class="box-content">
        <tr>
          <td width="180" valign="top" class="rowname">Game Category</td>
          <td valign="top">
              <asp:DropDownList ID="txtcategory" runat="server">
                  <asp:ListItem Value="101">Action Words</asp:ListItem>
                  <asp:ListItem Value="102">Multiple Choice</asp:ListItem>
                  <asp:ListItem Value="103">Evaluate Form</asp:ListItem>
              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname">Test Name (Thai) :</td>
          <td valign="top"><label for="textarea"></label>
          <textarea name="txttest_th" id="txttest_th" cols="45" rows="5" style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Test Name  (Eng) :</td>
          <td valign="top"><textarea name="txttest_en" id="txttest_en" cols="45" rows="5" style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Testing Peroid : </td>
          <td valign="top"><label for="textfield2"></label>
              <asp:TextBox ID="txtdate1" runat="server" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1" PopupButtonID="Image1">
                              </asp:CalendarExtender>
                              <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
          -           <asp:TextBox ID="txtdate2" runat="server" Width="100px" />
          <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2" PopupButtonID="Image2">
                              </asp:CalendarExtender>
                              <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
          </td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Organizer : </td>
          <td valign="top"><input type="text" name="txtorg" id="txtorg" style="width: 385px;" runat="server" /></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Time (Sec) / Question</td>
          <td valign="top">
              <asp:TextBox ID="txttime" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Status :</td>
          <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td width="30"><asp:RadioButton ID="txtstatus1" GroupName="status" runat="server" />
                </td>
              <td width="80">Active</td>
              <td width="30"><asp:RadioButton ID="txtstatus2" GroupName="status" runat="server" />
                </td>
              <td>Inactive</td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="green" />
          &nbsp;</td>
        </tr>
    </table>
</asp:Content>

