<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ots_report_receipt.aspx.vb" Inherits="srp_ots_report_receipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Invoice</title>
<link href="report.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div style="width: 800px;">
  <table class="rName" width="98%" align="center" cellpadding="5" cellspacing="0">
    <tr>
      <td width="100"><img src="logoSRP.gif" width="100" height="101" /></td>
      <td><h2 align="center">On-the-Spot Reward</h2></td>
    </tr>
  </table>
  <table width="98%" align="center" cellpadding="5" cellspacing="0">
    <tr>
      <td><strong>Date :</strong>
          <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
        </td>
      <td align="right"><strong>Reference No. :
          </strong>&nbsp;<asp:Label ID="lblRef" runat="server" 
              Text="Label"></asp:Label> </td>
    </tr>
  </table>
  <table width="98%" align="center" cellpadding="5" cellspacing="0">
    <tr>
      <td><strong>Customer :</strong>
          <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
        </td>
      <td><strong>Employee ID :</strong>&nbsp;
          <asp:Label ID="lblID" runat="server" Text="Label"></asp:Label>
        </td>
      <td><strong>Cost Center :</strong>
          <asp:Label ID="lblDept" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
  </table>
<br />
  
  <table class="rData" width="98%" align="center" cellpadding="5" cellspacing="0">
    <tr>
      <th width="5%" bgcolor="#CCCCCC" style="text-align: center;">No.</th>
      <th bgcolor="#CCCCCC" style="text-align: center;">Description</th>
      <th width="20%" bgcolor="#CCCCCC" style="text-align: center;">Price/Unit</th>
      <th width="6%" bgcolor="#CCCCCC" style="text-align: center;">Qty</th>
      <th width="10%" bgcolor="#CCCCCC" style="text-align: center;">Total Amount (Baht)</th>
      <th width="10%" bgcolor="#CCCCCC" style="text-align: center;">Total Points</th>
    </tr>
    <% 
        For i As Integer = 0 To dsInvoice.Tables("t1").Rows.Count - 1%>
    <tr>
      <td align="center"><%Response.Write (i+1) %></td>
      <td><% Response.Write(dsInvoice.Tables("t1").Rows(i)("sale_item_name_th").ToString)%>&nbsp;</td>
      <td><% Response.Write(dsInvoice.Tables("t1").Rows(i)("item_baht_total").ToString)%></td>
      <td align="center"><% Response.Write(dsInvoice.Tables("t1").Rows(i)("sale_qty").ToString)%></td>
      <td align="right"><% Response.Write(dsInvoice.Tables("t1").Rows(i)("item_baht_used").ToString)%></td>
      <td align="right"><% Response.Write(dsInvoice.Tables("t1").Rows(i)("item_point_used").ToString)%></td>
    </tr>
   <% Next i %>
   <% For ii As Integer = 0 To line - dsInvoice.Tables("t1").Rows.Count - 1%>
     <tr>
      <td align="center"></td>
      <td>&nbsp;</td>
      <td></td>
      <td align="center"></td>
      <td align="right"></td>
      <td align="right"></td>
    </tr>
   <% next ii %>
  </table>
  <br />
  <table width="98%" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td valign="top"><table class="rData" width="98%" cellpadding="5" cellspacing="0">
        <tr>
          <td width="60%" align="right" bgcolor="#CCCCCC"><!--<strong>Available Points / คะแนนสะสม :</strong>-->&nbsp;</td>
          <td align="right">  <asp:Label ID="lblpoint_available" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCCCCC"><strong>Redeemed Points / คะแนนใช้ไป :</strong></td>
          <td align="right">  <asp:Label ID="lblpoint_use" runat="server" Text="0"></asp:Label></td>
        </tr>
        <tr>
          <td align="right" bgcolor="#CCCCCC"><!--<strong>Outstanding Points / คะแนนคงเหลือ :</strong>-->&nbsp;</td>
          <td align="right">  <asp:Label ID="lblpoint_remain" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
      </table></td>
      <td width="46%" valign="top"><table class="rData" width="100%" align="center" cellpadding="5" cellspacing="0">
        <tr>
          <td width="56%" align="right" bgcolor="#D6D6D6"><strong>Total Amount : </strong></td>
          <td align="right">  <asp:Label ID="lblTotalAmount" runat="server" Text="Label"></asp:Label> &nbsp;&nbsp;Baht&nbsp;&nbsp;</td>
        </tr>
        <tr>
          <td align="right" bgcolor="#D6D6D6"><strong> Total Points : </strong></td>
          <td align="right">  <asp:Label ID="lblTotalPoint" runat="server" Text="Label"></asp:Label> &nbsp;&nbsp;Points</td>
        </tr>
        <tr>
          <td align="right" bgcolor="#D6D6D6"><strong>Total Card(s) : </strong></td>
          <td align="right">  <asp:Label ID="lblCard" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;Cards&nbsp;</td>
        </tr>
      </table></td>
    </tr>
  </table>
  <br />
  <br />
  <table width="98%" align="center" cellpadding="5" cellspacing="0">
    <tr>
      <td><span style="font-style:italic">&nbsp;Date: 
          <asp:Label ID="lblPrintDate" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
      <td align="right">Page 1 of 1</td>
    </tr>
  </table>
  
  
</div>
    </form>
</body>
</html>
