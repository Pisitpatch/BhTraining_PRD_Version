<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="std_list.aspx.vb" Inherits="jci2013_std_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png" align="absMiddle" width="24" height="24" />&nbsp;&nbsp;Standard Setting</td>
            <td style="PADDING-RIGHT: 15px" align="right">
                 Edition 
                <asp:DropDownList ID="txtedtion" runat="server" AutoPostBack="True" DataTextField="edition" DataValueField="edition">
                </asp:DropDownList>
                Chapter 
                <asp:DropDownList ID="txtchapter" runat="server" AutoPostBack="True" DataTextField="chapter" DataValueField="chapter">
                </asp:DropDownList>
                <asp:Button ID="cmdNew" runat="server" Text="Upload XLSX" /></td>
          </tr>
        </tbody>
      </table>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data.">
        <Columns>
         <asp:TemplateField HeaderText="Type">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
              <asp:Label ID="lblType" runat="server" Text='<%# Bind("type")%>'></asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Edition">
            
                <ItemTemplate>
                    
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("edition")%>'></asp:Label>
                    
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section Name">
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblIP" runat="server" Text='<%# Bind("section_name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chapter">
              
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("chapter")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="std_no" HeaderText="Standard No." />
            <asp:BoundField DataField="measure_element_no" HeaderText="ME No." />
            <asp:TemplateField HeaderText="Measurable Elements Detail">
              
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("std_detail")%>' Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("measure_element_detail") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Options">
                <ItemStyle HorizontalAlign="Left" Width="80px" />
                <ItemTemplate>
                    <a href="std_detail.aspx?mode=edit&menu=2&id=<%# Eval("std_id")%>">
                    <asp:Image ID="ImageButton1" runat="server" ImageUrl="../images/page_edit.png" ToolTip="Edit" />
                   </a>
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete ?')"  CommandName="onDeleteGroup" CommandArgument='<%# Eval("std_id") %>' />
                   
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

