<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_group.aspx.vb" Inherits="jci_admin_group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" align="absmiddle" />&nbsp;&nbsp;
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
            <asp:TemplateField HeaderText="Group of question">
            
                <ItemTemplate>
                    <a href="admin_question.aspx?tid=<%# tid %>&gid=<%# Eval("group_id") %>">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("group_name_th") %>'></asp:Label>
                    (<asp:Label ID="Label2" runat="server" Text='<%# Bind("num") %>'></asp:Label> Questions)
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IP Address">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("group_fix_ip") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="create_by_emp_name" HeaderText="Updated by" />
            <asp:BoundField DataField="create_date" HeaderText="Last Update" />
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <a href="admin_group_edit.aspx?mode=edit&tid=<%# tid %>&gid=<%# Eval("group_id") %>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                    </a>
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete ?')"  CommandName="onDeleteGroup" CommandArgument='<%# Eval("group_id") %>' />
                   
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

