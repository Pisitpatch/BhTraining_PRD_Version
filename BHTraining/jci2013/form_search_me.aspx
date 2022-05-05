<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="form_search_me.aspx.vb" Inherits="jci2013_form_search_me" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table class="box-header" width="100%">
        <tbody>
          <tr>
            <td><img alt="web" src="../images/web.png"  width="24" height="24" />&nbsp;&nbsp;<a href="form_list.aspx?menu=3">Form Setting</a> &gt; <a href="form_detail.aspx?id=<% Response.Write(id)%>&menu=3&mode=<% Response.Write(mode) %>">Form Detail</a> &gt; Select ME</td>
              <td width="400">
                 Edition  <asp:DropDownList ID="txtedition" runat="server" DataTextField="edition" DataValueField="edition">
                  </asp:DropDownList>
                  <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                  <asp:Button ID="cmdSearch" runat="server" Text="Search" />
              </td>
            <td style="PADDING-RIGHT: 15px" align="right" width="100">
                <asp:Button ID="cmdSaveME" runat="server" Text="Select ME" />
              </td>
          </tr>
        </tbody>
           
      </table>
    <br />
   Found  <asp:Label ID="lblNum" runat="server" Text="-"></asp:Label> records
    <br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data.">
        <Columns>
         <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                  <asp:CheckBox ID="chk" runat="server" />
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:BoundField DataField="edition" HeaderText="Edition" />
            <asp:TemplateField>
            
                <ItemTemplate>
                    <asp:Label ID="lblPK" runat="server" Text='<%# Bind("std_id")%>' Visible="false"></asp:Label>
                    <span style="color:green">
                    [<asp:Label ID="Label1" runat="server" Text='<%# Bind("chapter")%>'></asp:Label>,<asp:Label ID="Label2" runat="server" Text='<%# Bind("std_no")%>'></asp:Label>]
                    </span>
                    <span style="color:red">ME<asp:Label ID="Label3" runat="server" Text='<%# Bind("measure_element_no")%>' ></asp:Label></span>
                    ,<asp:Label ID="Label4" runat="server" Text='<%# Bind("measure_element_detail")%>'></asp:Label>
                   
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p>&nbsp;</p>
      <p>&nbsp;</p>
</asp:Content>

