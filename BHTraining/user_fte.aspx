<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="user_fte.aspx.vb" Inherits="user_fte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="header"><img src="images/menu_upload.gif" width="32" height="32"  />&nbsp;&nbsp;Upload FTE File</div>
  <table>
  <tr><td width="100">&nbsp;</td>
  <td >
      <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
      </td>
  </tr>
  <tr><td width="150">Choose your file (xls)</td>
  <td ><asp:FileUpload ID="myFile1" runat="server" />
    &nbsp;<asp:Button ID="cmdUpload" runat="server" Text="1. Upload to server" />
    <asp:Button ID="cmdUpdate" runat="server" Text="2. Synchonize to BH Database" 
          ForeColor="#009900" /></td>
  </tr>
  </table>
    
<hr />
     <asp:GridView ID="GridView1" runat="server"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              AllowPaging="True" PageSize="200" 
              AllowSorting="True">
              <PagerSettings Position="TopAndBottom" />
              <RowStyle BackColor="#EFF3FB" />
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
<asp:Label id="lblText" runat="server"></asp:Label>
</asp:Content>

