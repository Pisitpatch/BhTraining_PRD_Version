<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ad.aspx.vb" Inherits="ad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
<div id="divContent">
<h1>Search for people</h1>
<asp:TextBox ID="txtSearch" runat="server" ToolTip="Enter
search criteria here" Width="250px" Wrap="False"
TabIndex="1"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server"
OnClick="btnSearch_Click" Text="Search" />
<br />
<br />
<asp:Label ID="lblSearchStatus" runat="server" Font-Bold="True"
Visible="False"></asp:Label>
<br />
<asp:GridView ID="gvSearchResult" runat="server"
AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
HorizontalAlign="Center">
<Columns>
<asp:BoundField DataField="LogonName" HeaderText="Short
name:" />
<asp:BoundField DataField="FullName" HeaderText="Name:"
NullDisplayText="n/a" />
<asp:BoundField DataField="Description" HeaderText="Description:"
NullDisplayText="n/a" />
<asp:BoundField DataField="Phone" HeaderText="Phone:"
/>
<asp:BoundField DataField="Mobile" HeaderText="Mobile:"
NullDisplayText="n/a" />
<asp:BoundField DataField="Email" HeaderText="Email:"
HtmlEncode="False" />
<asp:BoundField DataField="Office" HeaderText="Office:"
/>
<asp:BoundField DataField="Department"
HeaderText="Department:" />
</Columns>
</asp:GridView>
<br />
</div>
</form>
</body>

</html>
