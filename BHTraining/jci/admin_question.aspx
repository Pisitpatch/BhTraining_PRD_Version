<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_question.aspx.vb" Inherits="jci_admin_question" MaintainScrollPositionOnPostback="true" %>

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
                <asp:Button ID="cmdNew" runat="server" Text="Create new question" />
               </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" BorderColor="#CCC"
        EmptyDataText="No question found">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Question">
                <ItemTemplate>
                   
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("question_detail_th") %>'></asp:Label>
                    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
             <ItemStyle Width="50px" HorizontalAlign="Center" />
             <HeaderTemplate>
             Display
             </HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblPK" runat="server" Text='<%# Bind("question_id") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lblActive" runat="server" Text='<%# Bind("is_active") %>' Visible="false"></asp:Label>
                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="onChangeActive"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="create_by_emp_name" HeaderText="Updated by" 
                 >
            </asp:BoundField>
            <asp:BoundField DataField="create_date" HeaderText="Last Update" 
                 >
            </asp:BoundField>
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <a href="admin_question_edit.aspx?mode=edit&tid=<%# tid %>&gid=<%# gid %>&qid=<%# Eval("question_id") %>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                    </a>
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandName="onDeleteTest" CommandArgument='<%# Eval("question_id") %>'  />
                  
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

