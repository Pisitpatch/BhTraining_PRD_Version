<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_status.aspx.vb" Inherits="incident.incident_popup_status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Bumrungrad Information System</title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body>
    <form id="form1" runat="server">
    <div>
    <div class="alert" style="overflow:auto">
<div class="heading">View Status Log 
    <asp:Label ID="lblHead" runat="server" Text="Label"></asp:Label></div>
<div style="padding: 0px 10px;">
<div style="margin-top: 20px; margin-bottom: 10px;">
   <asp:gridview ID="Gridview1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#000FFF" BorderStyle="None"    GridLines="None"
            CellPadding="3" CellSpacing="2" Width="100%" 
        EnableModelValidation="True" >
        <RowStyle ForeColor="#000066" /><AlternatingRowStyle BackColor="#F8F8F8" />
        <Columns>
            <asp:BoundField DataField="ir_status_name" HeaderText="Status" />
            <asp:TemplateField HeaderText="Date time">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("log_time") %>'></asp:Label>
                      <asp:Label ID="lblDateTS" runat="server" Visible="false" Text='<%# Bind("log_time_ts") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:BoundField HeaderText="Report by" DataField="log_create_by" />
            <asp:TemplateField HeaderText="Duration" >
                <ItemTemplate>
                    <asp:Label ID="lblDuration" runat="server"></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
        </Columns>
       
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" CssClass="gvHeaderRow" />
        </asp:gridview>
        <div style="width:100%;height:2px;  background-color:#81aeb1" ></div>
         <div style="margin-top:30px;width:100%; text-align:center">
        <input id="Button1" type="button" value="Close Window" onclick="window.close()" /></div>
</div>
</div>
</div>
 
       
    </div>
   
    </form>
</body>
</html>
