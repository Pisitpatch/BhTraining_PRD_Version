<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_question.aspx.vb" Inherits="game_admin_answer_edit" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" alt="web" />&nbsp;&nbsp;
                <a href="admin_test_master.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
                </td>
            <td align="right" style="padding-right: 15px;">
              <asp:Button ID="cmdAdd" runat="server" Text="Display all" />
                 <asp:Button ID="cmdNew" runat="server" Text="Create new question" />
               </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" BorderColor="#000CCC"
        EmptyDataText="No question found">
        <Columns>
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="70px" />
                <ItemTemplate>
                    <a href="admin_question_edit.aspx?mode=edit&tid=<%# tid %>&gid=<%# gid %>&qid=<%# Eval("question_id") %>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                    </a>
                    
                    <asp:imageButton ID="ImageButton2" runat="server" Visible="false" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandName="onDeleteTest" CommandArgument='<%# Eval("question_id") %>'  />
                  
                   <a href="preview_choice.aspx?q_order=1&tid=<%# tid %>&gid=<%# gid %>&qid=<%# Eval("question_id") %>" target="_blank">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../images/zoom.png" ToolTip="Preview" />
                    </a>
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
            <asp:TemplateField HeaderText="Question">
                <ItemTemplate>
                <asp:Label ID="lblCorrect" runat="server" Text='<%# Bind("correct") %>' Visible="false"></asp:Label> 
                     <a href="admin_answer_edit.aspx?mode=edit&tid=<%# tid %>&gid=<%# gid %>&qid=<%# Eval("question_id") %>">
                    <asp:Label ID="lblQ1" runat="server" Text='<%# Bind("question_detail_th") %>'></asp:Label> ( <asp:Label ID="Label4" runat="server" Text='<%# Bind("num") %>'></asp:Label>)

                    
                    <br />
                    <asp:Label ID="lblQ2" runat="server" Text='<%# Bind("question_detail_en") %>'></asp:Label> ( <asp:Label ID="Label2" runat="server" Text='<%# Bind("num") %>'></asp:Label>)
                    </a>
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
            <asp:BoundField DataField="jci_code" HeaderText="JCI Std." />
            <asp:BoundField DataField="answer4" HeaderText="4th Answer" 
                 >
            </asp:BoundField>
            <asp:BoundField DataField="answer3" HeaderText="3rd Answer"></asp:BoundField>
            <asp:BoundField DataField="answer2" HeaderText="2nd Answer"></asp:BoundField>
            <asp:BoundField DataField="answer1" HeaderText="1st Answer"></asp:BoundField>
        </Columns>
    </asp:GridView>
</asp:Content>

