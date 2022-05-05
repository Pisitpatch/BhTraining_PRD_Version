<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_group_edit.aspx.vb" Inherits="jci_admin_group_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="admin_test.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        <tr>
          <td width="180" valign="top" class="rowname">Test Name</td>
          <td valign="top">
              <asp:Label ID="lblGroupName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname">IP Address</td>
          <td valign="top"><asp:TextBox ID="txtip" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname">Group Name (Thai) :</td>
          <td valign="top">
          <textarea name="txtgroup_th" id="txtgroup_th" cols="45" rows="5" style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">Group Name  (Eng) :</td>
          <td valign="top"><textarea name="txtgroup_en" id="txtgroup_en" cols="45" rows="5" style="width: 635px;" runat="server"></textarea></td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Detail</td>
          <td valign="top">
              <textarea name="txtgroup_en0" id="txtdetail" cols="45" rows="5" 
                  style="width: 635px;" runat="server"></textarea></td>
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

