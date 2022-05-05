<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="form_list.aspx.vb" Inherits="jci2013_form_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png"  width="24" height="24" />&nbsp;&nbsp;Form Setting</td>
            <td style="PADDING-RIGHT: 15px" align="right"><asp:Button ID="cmdNew" runat="server" Text="Create Form" /></td>
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
            <asp:TemplateField HeaderText="Form Name">
            
                <ItemTemplate>
                   
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("form_name")%>'></asp:Label>
                    
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
               
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("status_name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Create by">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("create_by")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Update">
              
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("lastupdate_raw")%>'></asp:Label> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="num" HeaderText="Assessment Amount" />
            <asp:TemplateField>
              <ItemStyle Width="90px" HorizontalAlign="Center" BackColor="LightSkyBlue" />
                <ItemTemplate>
                     [<a href="form_detail.aspx?mode=edit&id=<%# Eval("form_id")%>&menu=3"><asp:Label ID="Label2" runat="server" Text="Edit Form"></asp:Label>]</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Authenticate">
                <ItemStyle HorizontalAlign="Center" BackColor="LightSkyBlue" />
                <ItemTemplate>
                    <a href="form_authen.aspx?form_id=<%# Eval("form_id") %>&menu=3">
                        [Edit Authenticate]
                        </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Daily Checklist">
             <ItemStyle HorizontalAlign="Center" Width="120px" BackColor="LightSkyBlue" />
                <ItemTemplate>
                   
                    <a href="assessment_list.aspx?form_id=<%# Eval("form_id") %>&menu=3">[<asp:Label ID="LabelView" runat="server" Text="View Assessment"></asp:Label>]</a>
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                  <ItemStyle HorizontalAlign="Center" BackColor="LightSkyBlue" />
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("form_id") %>' CommandName="onDeleteGroup" ImageUrl="../images/page_delete.png" OnClientClick="return confirm('Are you sure you want to delete ?')" ToolTip="Delete" />
                    <asp:LinkButton ID="LinkButton1" runat="server"  CommandArgument='<%# Eval("form_id") %>' CommandName="onDeleteGroup" ImageUrl="../images/page_delete.png" OnClientClick="return confirm('Are you sure you want to delete ?')" ToolTip="Delete" >Delete</asp:LinkButton>
                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Width="80px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

