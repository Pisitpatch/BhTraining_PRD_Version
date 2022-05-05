<%@ Page Language="VB" AutoEventWireup="false" CodeFile="game_over_choice.aspx.vb" Inherits="game_game_over_choice" %>

<!DOCTYPE html> 
<html> 
	<head> 
	<title>My Page</title> 
	<meta name="viewport" content="width=device-width, initial-scale=1"> 
	  <script type="text/javascript" src="jquery-1.6.min.js"></script>
<script type="text/javascript" src="jquery.flip.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script type="text/javascript" src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
    <style type="text/css">
	@font-face
{
font-family: myFirstFont;
src: url('TH K2D July8.ttf')
}
div
{
font-family:myFirstFont;
font-weight: bold;
}
.answer li a:link, .answer li a:visited 
{
color: #339;
text-decoration: none;
}
.answer li a:hover
{
color: #339;
text-decoration: none;
}
    </style>
</head> 
<body> 
<!--<audio  autoplay="autoplay" loop="loop">
  <source src="game1.mp3" type="audio/mpeg" />
</audio>-->

<div data-role="page" style="width: 1280px; height: 1024px; background: url(images/GameJCI-1280x1024-05.jpg) no-repeat;">

<div>
		  <table width="100%">
		    <tr>
		      <td height="60" style="font-family: 'Comic Sans MS'; font-size: 18px; font-weight: bold; color: #FFF;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
		      <td width="285" align="right"  style="font-family: 'Comic Sans MS'; font-size: 18px; font-weight: bold; color: #FFF;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	        </tr>
  </table>
	    </div><!-- /header -->

	<div style="margin: 0px; padding: 0px; overflow: hidden;">
	  <div class="result" style="height: 415px; margin: 180px 120px" >
      <table width="100%">
  <tr>
    <td width="40%" valign="top">
    <div style="font-family: 'Comic Sans MS'; font-size: 36px; color: #FFF; text-align: center; margin: 10px;">Player</div>
    <div style="height: 300px; text-align: center; font-family: 'Comic Sans MS'; font-size: 18px; font-weight: bold; color: #FFF; line-height: 2.15em;"><br>
      <%Response.Write(Session("jci_user_fullname").ToString)%><br>
     <%Response.Write(Session("jci_dept_name").ToString)%>
      <br>
ID <%Response.Write(Session("jci_emp_code").ToString)%></div>
<a href="multiple-choice-index.aspx" rel="external"><img src="images/Logout.png"  border="0"  style=" margin-top: 75px; margin-left: 55px"></a>
    </td>
    <td>
        <div style="font-family: 'Comic Sans MS'; font-size: 36px; color: #FFF; text-align: center; margin: 10px;">Score</div>
        <div style="height: 300px;">
           <asp:Panel ID="panel1" runat="server">
          <table width="90%" cellspacing="0" cellpadding="8" 
                   style="margin: 0px 10px; font-family: 'Comic Sans MS'; font-size: 20px; font-weight: bold; color: #FFF; ">
            <tr>
              <td width="50%">Patient Safety </td>
              <td style="text-align: right; padding-right: 50px;">
                  <asp:Label ID="lblScore1" runat="server" Text="0"></asp:Label>
                  &nbsp;&nbsp;point(s)</td>
              </tr>
            <tr>
              <td>Facility Mgt. and Safety</td>
              <td style="text-align: right; padding-right: 50px;"><asp:Label ID="lblScore2" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;point(s)</td>
              </tr>
            <tr>
              <td>Patient Care</td>
              <td style="text-align: right; padding-right: 50px;"><asp:Label ID="lblScore3" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;point(s)</td>
              </tr>
            <tr>
              <td>Healthcare Org.</td>
              <td style="text-align: right; padding-right: 50px;"><asp:Label ID="lblScore4" runat="server" Text="0"></asp:Label>&nbsp;&nbsp;point(s)</td>
              </tr>
            <tr>
              <td>Result</td>
              <td style="text-align: right; padding-right: 50px;"><asp:Label ID="lblSum" 
                      runat="server" Text="-"></asp:Label>&nbsp;&nbsp;point(s)</td>
              </tr>
              <tr>
                  <td>
                      Evaluation</td>
                  <td style="text-align: right; padding-right: 50px;"><asp:Label ID="lblEvaluate" 
                      runat="server" Text="-"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      </td>
              </tr>
          </table>
          </asp:Panel>
          <br />
          <br />
        </div>
    </td>
  </tr>
</table>

	    
  </div>
    </div><!-- /content -->

</div><!-- /page -->

</body>
</html>
