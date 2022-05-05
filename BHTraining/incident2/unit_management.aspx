<%@ Page Title="" Language="VB" MasterPageFile="~/incident/Incident_MasterPage.master" AutoEventWireup="false" CodeFile="unit_management.aspx.vb" Inherits="incident_unit_management" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div id="header"><img src="../images/menu_12.png" width="32" height="32"  />&nbsp;&nbsp;Unit Management</div>
   
<div id="data">
    <asp:Label ID="lblError" runat="server"></asp:Label>
<div>
<br /><br />
    <asp:Panel ID="Panel1" runat="server" Visible="true" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100">Unit Name</td><td>
        <asp:TextBox ID="txtadd_name" Width="450px" runat="server"></asp:TextBox>
        
        
        </td></tr>
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>                <asp:Button ID="cmdSaveGrandTopic" runat="server" Text="Add new" 
                    Font-Bold="True" />
                
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
 
</div>
<br />
 <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="dept_unit_id" 
           RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
        EnableModelValidation="True" OnRowEditing="GridView1_RowEditing" 
        >
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
                 <HeaderStyle ForeColor="White" />
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" > <%# Container.DataItemIndex + 1 %>. </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                  <asp:TemplateField HeaderText="Unit Name">
                  <ItemStyle VerticalAlign="Top" />
                    <EditItemTemplate>
                   <asp:TextBox ID="txtdept_name" runat="server" Text='<%# Bind("dept_unit_name") %>'></asp:TextBox>
               </EditItemTemplate>
                      <ItemTemplate>
                          
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_unit_name") %>'></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                     <asp:CommandField CancelText="Cancel" EditText="Edit" ShowEditButton="True" 
                UpdateText="Save">
            <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:CommandField>
                 
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
  </div>
 
</asp:Content>

