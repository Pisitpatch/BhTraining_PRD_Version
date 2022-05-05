<%@ Page Title="" Language="VB" MasterPageFile="~/incident/Incident_MasterPage.master" AutoEventWireup="false" CodeFile="volumn_management.aspx.vb" Inherits="incident_volumn_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="data">
 <asp:Panel ID="Panel1" runat="server" Visible="true" BorderStyle="Solid" BorderWidth="1px">
    <br />
    <table width="100%">
    <tr><td width="100" class="style2">Month/Year</td><td class="style2">
        <asp:DropDownList ID="txtmonth" runat="server">
        <asp:ListItem Value="1">January</asp:ListItem>
        <asp:ListItem Value="2">Feb</asp:ListItem>
        <asp:ListItem Value="3">Mar</asp:ListItem>
        <asp:ListItem Value="4">April</asp:ListItem>
        <asp:ListItem Value="5">May</asp:ListItem>
        <asp:ListItem Value="6">June</asp:ListItem>
        <asp:ListItem Value="7">July</asp:ListItem>
        <asp:ListItem Value="8">Auguest</asp:ListItem>
        <asp:ListItem Value="9">September</asp:ListItem>
        <asp:ListItem Value="10">October</asp:ListItem>
        <asp:ListItem Value="11">November</asp:ListItem>
        <asp:ListItem Value="12">December</asp:ListItem>
        </asp:DropDownList>
         
          <asp:DropDownList ID="txtyear" runat="server">
          <asp:ListItem Value="2011">2011</asp:ListItem>
          <asp:ListItem Value="2012">2012</asp:ListItem>
          <asp:ListItem Value="2013">2013</asp:ListItem>
          </asp:DropDownList>
        </td></tr>
        <tr>
            <td width="100">
                Patient Volume</td>
            <td>
                <asp:TextBox ID="txtvolumn" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100">
                &nbsp;</td>
            <td>
                <asp:Button ID="cmdSave" runat="server" Font-Bold="True" Text="Save" />
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    <br />
 <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="500px" CellPadding="4"  DataKeyNames="volumn_month,volumn_year"
               GridLines="None" CssClass="tdata3" 
              AllowPaging="True" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
        EnableModelValidation="True">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
                 <HeaderStyle ForeColor="White" />
               <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" > <%# Container.DataItemIndex + 1 %>. </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                  <asp:TemplateField HeaderText="Month">
                  <ItemStyle VerticalAlign="Top" Width="100px" />
                   
                      <ItemTemplate>
                          <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("month_name") %>' ></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Year">
                      <ItemStyle VerticalAlign="Top" Width="100px" />
                      <ItemTemplate>
                           <asp:Label ID="lblYear" runat="server" Text='<%# Bind("volumn_year") %>' ></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataField="volumn_patient" HeaderText="Patient Volume" />
                  <asp:TemplateField ShowHeader="False">
                      <ItemStyle ForeColor="Red" />
                      <ItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                              CommandName="Delete" Text="Delete" ForeColor="Red" OnClientClick="return confirm('Are you sure you want to delete this grand topic?')"></asp:LinkButton>
                      </ItemTemplate>
                      <ItemStyle ForeColor="Red" Width="80px" />
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

</div>
</asp:Content>

