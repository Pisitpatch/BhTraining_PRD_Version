<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="user_list.aspx.vb" Inherits="jci2013_user_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png" align="absMiddle" width="24" height="24" />&nbsp;&nbsp;Users</td>
            <td style="PADDING-RIGHT: 15px" align="right">
                 <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                <asp:Button ID="cmdSearch" runat="server" Text="Search" />
                 <asp:Button ID="CmdSave" runat="server" Text="Update" />
              </td>
          </tr>
        </tbody>
      </table>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data." AllowPaging="True" CellPadding="4" ForeColor="#333333" >
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
         <asp:TemplateField HeaderText="Employee code">
                                  <ItemStyle HorizontalAlign="Center" Width="100px" />
               <ItemTemplate>
              <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("emp_code")%>'></asp:Label>
                    <asp:Label ID="lblAdmin" runat="server" Text='<%# Bind("jci_admin")%>' Visible="false"></asp:Label>
                    <asp:Label ID="lblUser" runat="server" Text='<%# Bind("jci_user")%>' Visible="false"></asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee Name">
            
                <ItemTemplate>
                    
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("user_fullname")%>'></asp:Label>
                    
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("dept_name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Costcenter">
              
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("dept_id")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
              
                <ItemTemplate>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Value="1">Administor</asp:ListItem>
                        <asp:ListItem Value="2">Surveyor</asp:ListItem>
                    <asp:ListItem Value="0">None</asp:ListItem>
                       
                    </asp:RadioButtonList>
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
         <EditRowStyle BackColor="#999999" />
         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

