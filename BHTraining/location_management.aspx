<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="location_management.aspx.vb" Inherits="location_management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div id="header"><img src="images/menu_09.png" width="32" height="32"  />&nbsp;&nbsp;Location/Response Unit Management</div>
  <div id="data">
   <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="6" class="theader"><img src="images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
    </tr>

  <tr>
    <td width="150" height="30">Location Name</td>
    <td width="235">
        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
    <td width="75">&nbsp;</td>
    <td>
        &nbsp;</td>
</tr>
  <tr>
    <td colspan="6" align="right">
        
        <asp:Button ID="cmdFind" runat="server" Text="Search" 
            CssClass="button-green2" />&nbsp;
      <asp:Button ID="cmdReset" runat="server" Text="Clear" 
            CssClass="button-green2" />
        &nbsp;&nbsp;
           <asp:Button ID="cmdAdd" runat="server" Text="Add New Location" 
            CssClass="button-green2" />

    </td>
  </tr>
      </table>
        <br />
   <div class="small" style="margin-bottom: 3px;">You are viewing page <% Response.Write(GridView1.PageIndex + 1)%> of <% Response.Write(GridView1.PageCount)%>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
                    AllowPaging="True" DataKeyNames="location_id" PageSize="200" EnableModelValidation="True">
                   <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                       <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Location / Response Unit ">
                            <ItemTemplate>
                            <a href="location_detail.aspx?mode=edit&id=<%#Eval("location_id") %>">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("location_name") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
   <br />
   Total <%Response.Write(GridView1.Rows.Count)%> records 
   </div>
   </div>
    </asp:Content>
