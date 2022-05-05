<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="role_management.aspx.vb" Inherits="role_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div id="header"><img src="images/menu_09.png" width="32" height="32"  />&nbsp;&nbsp;Role Management</div>
  <div id="data">
   <div class="small" style="margin-bottom: 3px;">You are viewing page <% Response.Write(GridView1.PageIndex + 1)%> of <% Response.Write(GridView1.PageCount)%>
   <br />
   Total <%Response.Write(GridView1.Rows.Count)%> records 
   </div>
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
                    AllowPaging="True" DataKeyNames="role_id">
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
                        <asp:TemplateField HeaderText="Role name">
                            <ItemTemplate>
                            <a href="role_detail.aspx?mode=edit&roleid=<%# eval("role_id") %>">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("role_name") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Description" />
                        <asp:BoundField HeaderText="Creator" />
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
   </div>
</asp:Content>

