<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_idp_topic_list.aspx.vb" Inherits="idp_popup_idp_topic_list" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="data">
     <asp:Label runat="server" ID="lblTopic"></asp:Label><br />
    <asp:Label runat="server" ID="lblNum"></asp:Label> Records found.
       <asp:GridView ID="Gridview1" runat="server" Width="100%" CssClass="stattable" CellPadding="3" 
            CellSpacing="2" GridLines="None"
               
                          EnableModelValidation="True" PageSize="50">
           <Columns>
  
           </Columns>
           <HeaderStyle CssClass="dataHeader center" />
           <AlternatingRowStyle CssClass="row2" />
          </asp:GridView>
    </div>
    </form>
</body>
</html>