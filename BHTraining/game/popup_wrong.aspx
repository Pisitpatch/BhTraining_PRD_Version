<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_wrong.aspx.vb" Inherits="game_popup_wrong" %>

<!DOCTYPE html> 
<html>
<head runat="server">
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
    window.clearTimeout(myid);
</script>
<div data-role="content" style="background: #900;">
<div style="text-align: center"><img src="images/wrong.png" width="414" height="142"></div>
<a href="#" onclick="myid =window.setTimeout('setTime()',1000);stimer = 15;"  data-role="button" data-rel="back" data-theme="c">>>&nbsp;&nbsp;Try again...</a></div>
</div>
   
</body>
</html>
