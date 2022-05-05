<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="game_home" %>

<!DOCTYPE HTML>
<html>
<head>
<title>Action Words </title>
<meta name="apple-mobile-web-app-capable" content="yes" />
<script src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        document.ontouchmove = function (e) {
          //  e.preventDefault();
        }
    });
</script>
    <style type="text/css">
        .ui-page {
    background: transparent;
}
.ui-content{
    background: transparent;
}
    </style>
</head>

<body >
<div data-role="page" style="background: url(images/GameJCI-1024x748.jpg); background-repeat:no-repeat"> 
	<div data-role="content">
    <div style="width:100%; height:748px; vertical-align:middle">
<form id="form1" runat="server" data-ajax="false">
	<p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
     <p>&nbsp;</p>
      <p>&nbsp;</p>
	<table width="700"  cellpadding="3" cellspacing="2" style="background: transparent; color:#FFF; vertical-align:middle">
    <tr>
      <td width="100">Employee ID:</td>
      <td width="200">
       <asp:TextBox  ID= "txtusername" runat="server"  data-theme="b"></asp:TextBox>
       
        </td>
      <td rowspan="4" style="vertical-align:middle">
   
          <asp:ImageButton ID="cmdLogin1" runat="server" 
              ImageUrl="~/game/images/Start.png" data-enhance="false" data-role="none" OnClientClick="$('#audio-player')[0].play();" />
        
        
         
        </td>
    </tr>
 
 
    <tr>
      <td>
         
          Language :</td>
      <td>
         
          <asp:DropDownList ID="txtlang" runat="server"  >
              <asp:ListItem Value="th">Thai</asp:ListItem>
              <asp:ListItem Value="en">English</asp:ListItem>
          </asp:DropDownList>
         
        
         
          <asp:DropDownList ID="txtgroup" runat="server" DataTextField="group_name_th" 
              DataValueField="group_id" Visible="False">
          </asp:DropDownList>
         
        
         
        </td>
    </tr>
 
 
    <tr>
      <td>
         
          &nbsp;</td>
      <td>
         
          <asp:Label ID="lblError" runat="server" ForeColor="White"></asp:Label>
          <asp:Label ID="lblError2" runat="server" ForeColor="White"></asp:Label>
        </td>
    </tr>
    
    
  
    <tr>
      <td>
         
          &nbsp;</td>
      <td>
         
          <a href="http://bhtraining/game/ipad_demo.html"  style="color:Yellow" rel="external">วิธีการเล่น / How to play</a></td>
    </tr>
    
    
  
  </table>
  
</form>
</div>
</div>

</div>
<audio id="audio-player" name="audio-player" src="sound/button-11.wav"  ></audio>
</body>
</html>