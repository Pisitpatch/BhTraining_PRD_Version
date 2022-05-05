<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="pdf_list.aspx.vb" Inherits="jci2013_pdf_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png" align="absMiddle" width="24" height="24" />&nbsp;&nbsp;Hospital Standard</td>
            <td style="PADDING-RIGHT: 15px" align="right"><asp:Button ID="cmdNew" runat="server" Text="Add PDF File..." /></td>
          </tr>
        </tbody>
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
            <asp:TemplateField HeaderText="Description">
            
                <ItemTemplate>
                    
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pdf_name")%>'></asp:Label>
                    
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Current Status">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("status_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="File Size">
              
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("pdf_size")/1024 %>'></asp:Label> Kb
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="update_by" HeaderText="Updated by" />
            <asp:BoundField DataField="lastupdate_raw" HeaderText="Last Update" />
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <a href="pdf_detail.aspx?mode=edit&id1=<%# Eval("pdf_id")%>&menu=1">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                   </a>
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete ?')"  CommandName="onDeleteGroup" CommandArgument='<%# Eval("pdf_id") %>' />
                   
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

