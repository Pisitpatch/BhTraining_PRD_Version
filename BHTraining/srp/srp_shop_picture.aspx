<%@ Page Language="VB" AutoEventWireup="false" CodeFile="srp_shop_picture.aspx.vb" Inherits="srp_srp_shop_picture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <img src="srp_show_image.ashx?id=<% response.write(inv_id) %>" alt=''  border="0" />
    </div>
    </form>
</body>
</html>
