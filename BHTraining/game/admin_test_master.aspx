<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_test_master.aspx.vb" Inherits="game_admin_test_master" %>

<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="box-header" width="100%">
        <tr>
            <td>
                <img  height="24" src="../images/web.png" width="24" alt="web" />&nbsp;&nbsp;</td>
            <td align="right" style="padding-right: 15px;">
                <asp:Button ID="cmdNew" runat="server" Text="Create new test" />
               </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True">
        <Columns>
            <asp:TemplateField HeaderText="Options">
               <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                   <a href="admin_test_edit.aspx?mode=edit&tid=<%# Eval("test_id") %>"><asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" /></a>
                    
                    <asp:imageButton ID="ImageButton2" runat="server" Visible="false" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandName="onDeleteTest" CommandArgument='<%# Eval("test_id") %>'  />   
                
                </ItemTemplate>
            </asp:TemplateField>
         <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Test Name">
              
                <ItemTemplate>
                    <a href="admin_question_master.aspx?tid=<%# Eval("test_id") %>"><asp:Label ID="Label1" runat="server" Text='<%# Bind("test_name_th") %>'></asp:Label></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Category" DataField="category_name" />
            <asp:TemplateField HeaderText="Current Status">
               <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="imgStatusActive" runat="server" ImageUrl="../images/action_check.png" /> 
                    <asp:Image ID="imgStatusInActive" runat="server" ImageUrl="../images/action_delete.png" />   
                    <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("is_active") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time (Sec) / Question">
               
                <ItemTemplate>
                    <asp:Label ID="txttime" runat="server" Text='<%#  (Eval("time_amount_sec")) %>'></asp:Label> Seconds
                  
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="organizer" HeaderText="Organizer" />
            <asp:BoundField DataField="create_by_emp_name" HeaderText="Updated by" />
            <asp:BoundField DataField="create_date" HeaderText="Last Update" />
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

