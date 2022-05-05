<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_test.aspx.vb" Inherits="jci_admin_test" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="box-header" width="100%">
        <tr>
            <td>
                <img  height="24" src="../images/web.png" width="24" align="absmiddle" />&nbsp;&nbsp;<a href = "jci_select_question.aspx">Front page</a> &gt;</td>
            <td align="right" style="padding-right: 15px;">
                <asp:Button ID="cmdNew" runat="server" Text="Create new test" />
               </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True">
        <Columns>
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
                    <a href="admin_group.aspx?tid=<%# Eval("test_id") %>"><asp:Label ID="Label1" runat="server" Text='<%# Bind("test_name_th") %>'></asp:Label></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Current Status">
               <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="imgStatusActive" runat="server" ImageUrl="../images/action_delete.png" /> 
                    <asp:Image ID="imgStatusInActive" runat="server" ImageUrl="../images/action_check.png" />   
                    <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("is_active") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Testing Period">
               
                <ItemTemplate>
                    <asp:Label ID="txtdate1" runat="server" Text='<%#  ConvertTSToDateString(Eval("test_start_date_ts")) %>'></asp:Label> - 
                     <asp:Label ID="txtdate2" runat="server" Text='<%#  ConvertTSToDateString(Eval("test_end_date_ts")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="organizer" HeaderText="Organizer" />
            <asp:BoundField DataField="create_by_emp_name" HeaderText="Updated by" />
            <asp:BoundField DataField="create_date" HeaderText="Last Update" />
            <asp:TemplateField HeaderText="Options">
               <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                   <a href="admin_test_edit.aspx?mode=edit&tid=<%# Eval("test_id") %>"><asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" /></a>
                    
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandName="onDeleteTest" CommandArgument='<%# Eval("test_id") %>'  />   
                     <a href="admin_authen_list.aspx?tid=<%# Eval("test_id") %>">
                    <asp:Image ID="ImageButton3" runat="server" ImageUrl="../images/user_view.png" ToolTip="User" />   
                    </a>
                    <a href="admin_review_user_list.aspx?tid=<%# Eval("test_id") %>">
                    <asp:Image ID="ImageButton4" runat="server" ImageUrl="../images/comment_edit.png" ToolTip="Review" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

