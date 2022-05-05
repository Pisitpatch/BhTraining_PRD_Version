<%@ Page Language="VB" AutoEventWireup="false" CodeFile="star_news_detail.aspx.vb" Inherits="star_star_news_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>News Detail</title>
            <link href="../css/main.css" rel="stylesheet" type="text/css" />
 <link href="../css/popup.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.6.min.js" charset="utf-8"></script>

</head>
<body>
   
    <form id="form1" runat="server">
    <div id="data">
     <asp:Label ID="lblDetail" runat="server" Text="Label"></asp:Label>
     <br />
       <asp:Label ID="lblFileName" runat="server" Text="Label"></asp:Label>
     <br />
     <br />
        <div align="right"><input type="button" name="cmdClose" id="cmdClose" value="Close Window" onclick="parent.$.akModalRemove();" /></div>
    </div>
    </form>
</body>
