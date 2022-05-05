<%@ Page Language="VB" AutoEventWireup="false" CodeFile="multiple-choice-ceo_en.aspx.vb" Inherits="game_multiple_choice_ceo_en" %>

<!DOCTYPE html> 
<html> 
	<head> 
	<title>My Page</title> 
	<meta name="viewport" content="width=device-width, initial-scale=1"> 
<script src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
<script language="javascript" type="text/javascript">
    function playSound(soundfile) {
        document.getElementById("dummy").innerHTML =
 "<embed src=\"" + soundfile + "\" hidden=\"true\" autostart=\"true\" loop=\"false\" />";
    }
 </script>
    <style type="text/css">
	
    </style>
</head> 
<body> 
<div data-role="page" style="width: 1280px; height: 1024px; background: url(images/MessageFromCEO-ENG.jpg) no-repeat;">

<div></div><!-- /header -->

	<div class="content">
	  <div align="right" style="padding: 20px 60px;"><a href="multiple-choice-ceo.aspx" data-transition="slide" onclick="playSound('sound/button-17.wav');"><img src="images/flag_thailand.png" width="32" height="32" border="0"></a>&nbsp;&nbsp;<a href="multiple-choice-ceo_en.aspx" data-transition="slide" onclick="playSound('sound/button-17.wav');"><img src="images/flag_usa.png" width="32" height="32" border="0"></a></div>
      <table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td ><a href="multiple-choice-login.aspx" onclick="playSound('sound/button-9.wav');"   ><img src="images/Next.png"  border="0" style=" margin-top: 605px; margin-left: 435px"></a></td>
  </tr>
  </table>
  <br /><br /><br />
	</div><!-- /content -->

</div><!-- /page -->
<span id="dummy"></span>
<audio id="audio-player" name="audio-player" src="sound/button-9.wav"  ></audio>
</body>
</html>

