<%@ Page Language="VB" AutoEventWireup="false" CodeFile="multiple-choice-index.aspx.vb" Inherits="game_multiple_choice_index" %>

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

<div data-role="page" style="width: 1280px; height: 1024px; background:url(images/CoverGameJCI-1280x1024.jpg) no-repeat;">

<div>
		  <table width="100%">
		    <tr>
		      <td>&nbsp;</td>
		      <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;</td>
	        </tr>
	      </table>
	    </div><!-- /header -->

	<div style="margin: 0px; padding: 0px; overflow: hidden;">
     <table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td ><a href="multiple-choice-ceo.aspx" onclick="playSound('sound/button-9.wav');" data-transition="pop" ><img src="images/Go.png"  border="0" style=" margin-top: 805px; margin-left: 935px"></a></td>
  </tr>
</table>
  </div><!-- /content -->

</div><!-- /page -->
<span id="dummy"></span>
</body>
</html>
