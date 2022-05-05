<%@ Page Language="VB" AutoEventWireup="false" CodeFile="multiple-choice-login.aspx.vb" Inherits="game_multiple_choice_login" %>

<!DOCTYPE HTML>
<html>
<head>
<title>Multiple Choice </title>

<script src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
    <style type="text/css">
        .ui-page {
    background: transparent;
}
.ui-content{
    background: transparent;
}
    </style>
</head>

<body> 
<!--<audio  autoplay="autoplay" loop="loop">
  <source src="game1.mp3" type="audio/mpeg" />
</audio>-->
<div data-role="page" style="width: 1280px; height: 1024px; background:url(images/GameJCI-1280x1024-01.jpg) no-repeat;">
<form id="form1" runat="server" data-ajax="false">
<div>

		  <table width="100%">
		    <tr>
		      <td>&nbsp;&nbsp;</td>
		      <td align="right"><a href="multiple-choice-index.aspx" rel="external"  data-enhance="false" data-role="none"><img src="images/home.png" width="32" height="32" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;</td>
	        </tr>
	      </table>
	    </div><!-- /header -->

	<div style="margin: 0px; padding: 0px; overflow: hidden;">
  <asp:Panel ID="panel1" runat="server" DefaultButton="cmdLogin1">
     <table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td height="865"><table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td width="250" style="padding-left: 50px; color: #FFF;"><table width="100%">
      <tr>
        <td><label for="basic" style="font-size: 20px;">Employee ID:</label></td>
      </tr>
      <tr>
        <td><asp:TextBox  ID= "txtusername" runat="server"  data-theme="b"></asp:TextBox>
         
          <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" Text=""></asp:Label>
         
          </td>
      </tr>
      <tr>
        <td><label for="select-choice-0" class="select" style="font-size: 20px;">Language:</label></td>
      </tr>
      <tr>
        <td>   <asp:DropDownList ID="txtlang" runat="server"  >
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
                <a href="JCI-Muliple-Choice-Demo 1024x748.htm" target="_blank" style="color:White">How to play</a></td>
        </tr>
    </table>
        </td>
    <td style="padding-left: 50px;"><br>
      <br>
       <asp:ImageButton ID="cmdLogin1" runat="server" 
              ImageUrl="~/game/images/Start.png" data-enhance="false" data-role="none" rel="external"  /></td>
  </tr>
</table>
</td>
  </tr>
</table>
</asp:Panel>
  </div><!-- /content -->
 </form>
</div><!-- /page -->

</body>
</html>
