<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="admin_review_question.aspx.vb" Inherits="jci_admin_review_question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="box-header">
  <tr>
    <td><img src="../images/report.png" width="24" height="24" />&nbsp;&nbsp;
     <a href="admin_test.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
    </td>
    <td align="right" style="padding-right: 15px;" ></td>
  </tr>
</table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CssClass="box-content"  Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <ItemTemplate>
                    <asp:Label ID="Labels" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Selected Question">
                <ItemTemplate>
                <span style="cursor:pointer; color: #006c69;" onclick="window.open('popup_review_score.aspx?id=<%# Eval("trans_id") %>' , '', 'alwaysRaised,scrollbars =yes,width=900,height=600') ;" >
                   <img src="../images/film.png" alt="Video" />
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("trans_q_name_th") %>'></asp:Label>
                   </span>
                    <asp:Label ID="lblScore" runat="server"  Text='<%# Bind("review_score") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="trans_create_date" HeaderText="Test Date" />
            <asp:BoundField  HeaderText="Duration" />
            <asp:TemplateField HeaderImageUrl="~/images/151.png">
               <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="imgSelect1" runat="server" ImageUrl="../images/action_check.png" />   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderImageUrl="~/images/154.png">
                 <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="imgSelect2" runat="server" ImageUrl="../images/action_check.png" />   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderImageUrl="~/images/152.png">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="imgSelect3" runat="server" ImageUrl="../images/action_check.png" />   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="review_date" HeaderText="Review date" />
            <asp:BoundField HeaderText="Review by" DataField="review_by_name" />
        </Columns>
    </asp:GridView>
</asp:Content>

