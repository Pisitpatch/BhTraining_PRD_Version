<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="user_management.aspx.vb" Inherits="user_management"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="images/menu_12.png" width="32" height="32"  />&nbsp;&nbsp;User Management</div>
    
      <div id="data"><table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="6" class="theader"><img src="images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
    </tr>
  <tr>
    <td width="100" height="30">Employee ID</td>
    <td width="235">
        <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
      </td>
    <td width="75">Deparment</td>
    <td width="200">
        <asp:DropDownList ID="txtdept" runat="server" 
            DataTextField="dept_name_en" DataValueField="dept_id">
        </asp:DropDownList>
      </td>
   
    <td width="75">Cost Center</td>
    <td>
        <asp:DropDownList ID="txtcostcenter" runat="server" DataTextField="costcenter_id" DataValueField="costcenter_id">
        </asp:DropDownList>
</td>

  <tr>
    <td width="100" height="30">Employee Name</td>
    <td width="235">
        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
    <td width="75">&nbsp;</td>
    <td>
        &nbsp;</td>
</tr>
  <tr>
    <td colspan="6" align="right">
        
        <asp:Button ID="cmdFind" runat="server" Text="Search" 
            CssClass="button-green2" />&nbsp;
      <input name="input2" type="button" value="Clear" class="button-green2" />
        <asp:Button ID="cmdUpdateFTE" runat="server" Text="Synchronize User Database" 
            Visible="False" />
      &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="temp2.aspx">Update FTE</asp:HyperLink>
      </td>
  </tr>
      </table>
        <br />
        <div class="small" style="margin-bottom: 3px;">You are viewing page <% Response.Write(GridView1.PageIndex + 1)%>of <% Response.Write(GridView1.PageCount)%></div>
          <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="emp_code" 
              AllowPaging="True" PageSize="100" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data.">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
                 <HeaderStyle ForeColor="White" />
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                  <asp:TemplateField HeaderText="Employee Name" SortExpression="user_fullname">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("user_fullname") %>'></asp:Label> 
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:HyperLinkField DataNavigateUrlFields="emp_code" 
                      DataNavigateUrlFormatString="user_detail.aspx?mode=edit&amp;id={0}" 
                      DataTextField="emp_code" HeaderText="Employee ID">
                  <HeaderStyle ForeColor="White" />
                  </asp:HyperLinkField>
                  <asp:BoundField HeaderText="Department" DataField="dept_name" >
                  <HeaderStyle ForeColor="White" />
                  </asp:BoundField>
                  <asp:TemplateField HeaderText="Job Title">
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("job_title") %>'></asp:Label>
                      </ItemTemplate>
                     
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Status">
                    
                      <ItemTemplate>
                       <asp:Label ID="txtstatusid" runat="server" Text='<%# Bind("is_active") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatus" runat="server"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
       
<br />
<div align="center"><a href="#">- Top -</a></div>
      </div>
</asp:Content>

