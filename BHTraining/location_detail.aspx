<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="location_detail.aspx.vb" Inherits="location_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data"><table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="4" class="theader"><img src="images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Location 
        Detail</td>
    </tr>
  
 
  <tr>
    <td  width="120" height="30">Location Name</td>
    <td width="235">
        <asp:TextBox ID="txtnamelocal" runat="server" Width="350px" ></asp:TextBox>
        <br />
        <asp:Label ID="txtlabel" runat="server" ForeColor="Red" Visible ="false"></asp:Label>
      </td>
    <td>&nbsp;</td>
    <td >
        &nbsp;</td>
</tr>
  <tr>
    <td  height="30">&nbsp;</td>
    <td width="235">
          <asp:Button ID="cmdSaveDept" runat="server" Text="Save" 
            CssClass="button-green2" Font-Bold="True" CausesValidation="False" />&nbsp;
       <asp:Button ID="cmdCancel" runat="server" Text="Cancel" 
            CssClass="button-green2" CausesValidation="False" /></td>
    <td width="75"> &nbsp;</td>
    <td width="200">
        &nbsp;</td>
</tr>
  <tr>
    <td colspan="4" align="right">
        
     </td>
  </tr>
      </table>

      </div>
    </asp:Content>
