﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home_ceo_en.aspx.vb" Inherits="game_home_ceo_en" %>

<!DOCTYPE html> 
<html> 
	<head> 
	<title>My Page</title> 
	<meta name="viewport" content="width=device-width, initial-scale=1"> 
<script src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        document.ontouchmove = function (e) {
            e.preventDefault();
        }
    });
</script>
    <style type="text/css">
	
    </style>
</head> 
<body> 
<div data-role="page" style="width: 1024px; height: 748px; background: url(images/MessageFromCEO-Ipad-ENG.jpg) no-repeat;">

<div></div><!-- /header -->

	<div class="content">
	  <div align="right" style="padding: 40px 100px;"><a href="home_ceo.aspx" data-transition="slide" onclick="$('#audio-player')[0].play();"><img src="images/flag_thailand.png" width="32" height="32" border="0"></a>&nbsp;&nbsp;<a href="home_ceo_en.aspx" data-transition="slide"  onclick="$('#audio-player')[0].play();"><img src="images/flag_usa.png" width="32" height="32" border="0"></a></div>
      <table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td ><a href="home.aspx"  data-transition="slide"  onclick="$('#audio-player')[0].play();"><img src="images/Next.png"  border="0" style=" margin-top: 435px; margin-left: 255px"></a></td>
  </tr>
  </table>
  <br /><br /><br />
	</div><!-- /content -->

</div><!-- /page -->


</body>
</html>