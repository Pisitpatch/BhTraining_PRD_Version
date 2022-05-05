<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dialogLogff.aspx.vb" Inherits="game_dialogLogff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="jquery-1.6.min.js"></script>


  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="dialog">
	
		<div data-role="header" data-theme="d">
			<h1>Dialog</h1>

		</div>

		<div data-role="content" data-theme="c">
			<h1>Delete page?</h1>
			<p>This is a regular page, styled as a dialog. To create a dialog, just link to a normal page and include a transition and <code>data-rel="dialog"</code> attribute.</p>
			<a href="docs-dialogs.html" data-role="button" data-rel="back" data-theme="b">Sounds good</a>       
			<a href="docs-dialogs.html" data-role="button" data-rel="back" data-theme="c">Cancel</a>    
		</div>
	</div>

    </form>
</body>
</html>
