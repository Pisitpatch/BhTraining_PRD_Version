<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_report_internal_summary.aspx.vb" Inherits="idp_popup_report_internal_summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="data">
     <asp:Label runat="server" ID="lblTopic"></asp:Label><br />
    <asp:Label runat="server" ID="lblNum"></asp:Label> Records found.
       <asp:GridView ID="Gridview1" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="stattable" CellPadding="3" 
            CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" PageSize="50">
           <Columns>
  
               <asp:TemplateField HeaderText="Employee Code">
                  <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("emp_code") %>'></asp:Label>
                   </ItemTemplate>
                  
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Employee">
                 <ItemStyle VerticalAlign="Top" />
                   <ItemTemplate>
                       <asp:Label ID="lblTotal" runat="server" Text='<%# bind("emp_name_en") %>'></asp:Label>
                       
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="emp_status" HeaderText="Status" />
               <asp:TemplateField HeaderText="Department">
               
                   <ItemTemplate>
                   
                      <asp:Label ID="lblIRNo" runat="server" Text='<%# Eval("dept_name") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Attended">
                  
                   <ItemTemplate>
                       <asp:Label ID="lblAttend" runat="server" Text='<%# Bind("attend") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
    </div>
    </form>
</body>
</html>
