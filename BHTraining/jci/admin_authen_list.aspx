<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_authen_list.aspx.vb" Inherits="jci_admin_authen_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" />&nbsp;&nbsp;
                <a href="admin_test.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
                </td>
            <td align="right" style="padding-right: 15px;">
                <asp:Button ID="cmdNew" runat="server" Text="Create new group" />
               </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data.">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <ItemTemplate>
                    <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                 
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("user_fullname") %>'></asp:Label>
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Job Title" />
            <asp:BoundField HeaderText="Editor" />
            <asp:BoundField HeaderText="Reviewer" />
            <asp:BoundField  HeaderText="Created by" 
                ItemStyle-Width="120px" >
<ItemStyle Width="120px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField  HeaderText="Create Date" 
                ItemStyle-Width="120px" >
<ItemStyle Width="120px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <a href="admin_authen_detail.aspx?mode=edit&tid=<%# tid %>&empcode=<%# Eval("emp_code") %>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                    </a>
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete ?')"  CommandName="onDeleteGroup" CommandArgument='<%# Eval("group_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

