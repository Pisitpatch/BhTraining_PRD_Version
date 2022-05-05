<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_question_master.aspx.vb" Inherits="game_admin_question_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" align="absmiddle" />&nbsp;&nbsp;
                <a href="admin_test_master.aspx">Main</a> 
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
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <a href="admin_group_edit.aspx?mode=edit&tid=<%# tid %>&gid=<%# Eval("group_id") %>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                    </a>
                    <asp:imageButton ID="ImageButton2" runat="server" Visible="false" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete ?')"  CommandName="onDeleteGroup" CommandArgument='<%# Eval("group_id") %>' />
                       
                    <a href="admin_review_score.aspx?gid=<%# Eval("group_id") %>">
                    <asp:Image ID="ImageButton4" runat="server" ImageUrl="../images/comment_edit.png" ToolTip="Review" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
         <asp:TemplateField HeaderText="Zone No.">
                                  <ItemStyle HorizontalAlign="Center" Width="60px" />
               <ItemTemplate>
                  <asp:Label ID="lblZone" runat="server" Text='<%# Bind("zone_no") %>'></asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Group Name">
            
                <ItemTemplate>
                    <a href="admin_question.aspx?tid=<%# tid %>&gid=<%# Eval("group_id") %>">
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("group_name_th") %>'></asp:Label>
                    
                    </a>
                     <asp:Label ID="lblPK" runat="server" Text='<%# Bind("group_id") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="num_question" HeaderText="Random Questions" 
                ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="#ffff99" >
<ItemStyle HorizontalAlign="Center" BackColor="#FFFF99"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="Total Questions">
            <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                   <asp:Label ID="Label2" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Target Group">
               
                <ItemTemplate>
                    <asp:Label ID="lblTarget" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IP Address">
                <ItemStyle HorizontalAlign="left"  />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text=''></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Remark" DataField="group_remark" />
           
        </Columns>
    </asp:GridView>
</asp:Content>


