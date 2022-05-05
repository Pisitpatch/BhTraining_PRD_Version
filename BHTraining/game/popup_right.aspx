<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_right.aspx.vb" Inherits="game_popup_right" %>

<!DOCTYPE html> 
<html>
<head id="Head1" runat="server">
    <title></title>
     <script type="text/javascript" src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script type="text/javascript" src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
</head>
<body>
<div data-role="dialog">
<!--<div data-role="header" data-theme="d">
<h1>Dialog</h1>
</div>-->
<script type="text/javascript">
   // stimer = 10;
</script>
<div data-role="content" style="background:#006600;">
<div style="text-align: center"><img src="images/right.png" width="414" height="142"></div>
<a onclick="stimer = <%response.write(Session("time_amount").tostring) %>;" href="game_actionword1.aspx?gid=<%response.write(gid) %>&order=<%response.write(order) %>&q_order=<%response.write(q_order) %>&lang=<%response.write(lang) %>" rel="external" data-transition="pop"  data-role="button" data-theme="c">>>&nbsp;&nbsp;Next Question</a></div>
</div>

</body>
</html>
