<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="dept_management.aspx.vb" Inherits="dept_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id="header"><img src="images/menu_09.png" width="32" height="32"  />&nbsp;&nbsp;Department Management</div>
  <div id="data">
  <fieldset>
  <legend>Search</legend>
  <table width="100%">
  <tr><td width="120">Department </td><td>
      <asp:TextBox ID="txtdept" runat="server"></asp:TextBox></td>
  </tr>
  <tr><td width="120">Cost Center</td><td>
      <asp:TextBox ID="txtdept0" runat="server"></asp:TextBox></td>
  </tr>
  <tr><td width="120">Divsion</td><td>
      <asp:DropDownList ID="txtdivision" runat="server" 
          DataTextField="division_name_en" DataValueField="division_id">
      </asp:DropDownList>
      &nbsp;<asp:Button ID="cmdEditDivision" runat="server" Text="Edit Division" />
      </td>
  </tr>
  <tr><td width="120">Person name</td><td>
      <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:DropDownList ID="txtposition" runat="server" 
          DataTextField="position_name_en" DataValueField="position_id">
      </asp:DropDownList>
      </td>
  </tr>
  <tr><td width="120">&nbsp;</td><td>
      <asp:Button ID="Button1" runat="server" Text="Search" />
      </td>
  </tr>
  </table>
  </fieldset>
  <br />
   <div class="small" style="margin-bottom: 3px;">You are viewing page <% Response.Write(GridView1.PageIndex + 1)%> of <% Response.Write(GridView1.PageCount)%>
   <br />
   Total <%Response.Write(GridView1.Rows.Count)%> records 
   </div>
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="98%" 
                    AllowPaging="True" DataKeyNames="dept_id" PageSize="50" 
          EnableModelValidation="True">
                   <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                       <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                        <asp:BoundField DataField="dept_id" HeaderText="Costcenter Code" />
                        <asp:TemplateField HeaderText="Costcenter name">
                            <ItemTemplate>
                             <asp:Label ID="lblPk" runat="server" Text='<%# Bind("dept_id") %>' Visible="false"></asp:Label>
                            <a href="dept_detail.aspx?mode=edit&id=<%# eval("dept_id") %>">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_name_en") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Group">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text=''></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Division">
                          
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("division_name_en") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
   </div>
</asp:Content>

