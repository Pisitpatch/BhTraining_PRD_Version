<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">


a:link    { color: #0B55C4; text-decoration: none; }
a:visited { color: #0B55C4; text-decoration: none; }
a:hover   { text-decoration: underline; }

fieldset {
	margin-bottom: 10px;
	border: 1px #ccc solid;
	padding: 5px;
	text-align: left;
}

fieldset p {  margin: 10px 0px;  }

legend    {
	color: #0B55C4;
	font-size: 12px;
	font-weight: bold;
}

/*input, select { font-size: 10px;  border: 1px solid silver; }
textarea      { font-size: 11px;  border: 1px solid silver; }
button        { font-size: 10px;  }*/

input.disabled { background-color: #F0F0F0; }

input.button  { cursor: pointer;   }

input:focus,
select:focus,
textarea:focus { background-color: #ffd }

/* -- overall styles ------------------------------ */

#border-top.h_green          { background: url(j_header.png) repeat-x; }
#border-top.h_green div      { background: url(j_heades.png) 100% 0 no-repeat; }
#border-top.h_green div div  { background: url(j_headet.png) no-repeat; height: 54px; }

#border-top.h_teal          { background: url(j_headeu.png) repeat-x; }
#border-top.h_teal div      { background: url(j_headeu.png) 100% 0 no-repeat; }
#border-top.h_teal div div  { background: url(j_headeu.png) no-repeat; height: 54px; }

#border-top.h_cherry          { background: url(j_headeu.png) repeat-x; }
#border-top.h_cherry div      { background: url(j_headeu.png) 100% 0 no-repeat; }
#border-top.h_cherry div div  { background: url(j_headeu.png) no-repeat; height: 54px; }

#border-top .title {
	font-size: 22px; font-weight: bold; color: #fff; line-height: 44px;
	padding-left: 180px;
}

#border-top .version {
	display: block; float: right;
	color: #fff;
	padding: 25px 5px 0 0;
}

#border-bottom 			{ background: url(j_bottom.png) repeat-x; }
#border-bottom div  		{ background: url(j_corner.png) 100% 0 no-repeat; }
#border-bottom div div 	{ background: url(j_cornes.png) no-repeat; height: 11px; }

#footer .copyright { margin: 10px; text-align: center; }

#header-box  { border: 1px solid #ccc; background: #f0f0f0; }

#content-box {
	border-left: 1px solid #ccc;
	border-right: 1px solid #ccc;
}

#content-box .padding  { padding: 10px 10px 0 10px; }

#toolbar-box 			{ background: #fbfbfb; margin-bottom: 10px; }

#submenu-box { background: #f6f6f6; margin-bottom: 10px; }
#submenu-box .padding { padding: 0px;}


/* -- status layout */
#module-status      { float: right; }
#module-status span { display: block; float: left; line-height: 16px; padding: 4px 10px 0 22px; margin-bottom: 5px; }

#module-status { background: url(mini_ico.png) 3px 5px no-repeat; }
.legacy-mode{ color: #c00;}
#module-status .preview 			  { background: url(icon-16-.png) 3px 3px no-repeat; }
#module-status .unread-messages,
#module-status .no-unread-messages { background: url(icon-160.png) 3px 3px no-repeat; }
#module-status .unread-messages a  { font-weight: bold; }
#module-status .loggedin-users     { background: url(icon-161.png) 3px 3px no-repeat; }
#module-status .logout             { background: url(icon-162.png) 3px 3px no-repeat; }

/* -- various styles -- */
span.note {
	display: block;
	background: #ffd;
	padding: 5px;
	color: #666;
}

/** overlib **/

.ol-foreground {
	background-color: #ffe;
}

.ol-background {
	background-color: #6db03c;
}

.ol-textfont {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10px;
	color: #666;
}

.ol-captionfont {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 11px;
	color: #fff;
	font-weight: bold;
}
.ol-captionfont a {
	color: #0b5fc6;
	text-decoration: none;
}

.ol-closefont {}

/** toolbar **/

div.header {
	font-size: 22px; font-weight: bold; color: #0B55C4; line-height: 48px;
	padding-left: 55px;
	background-repeat: no-repeat;
	margin-left: 10px;
}

div.header span { color: #666; }

div.configuration {
	font-size: 14px; font-weight: bold; color: #0B55C4; line-height: 16px;
	padding-left: 30px;
	margin-left: 10px;
	background-image: url(icon-163.png);
	background-repeat: no-repeat;
}

div.toolbar { float: right; text-align: right; padding: 0; }

table.toolbar    			 { border-collapse: collapse; padding: 0; margin: 0;	 }
table.toolbar td 			 { padding: 1px 1px 1px 4px; text-align: center; color: #666; height: 48px; }
table.toolbar td.spacer  { width: 10px; }
table.toolbar td.divider { border-right: 1px solid #eee; width: 5px; }

table.toolbar span { float: none; width: 32px; height: 32px; margin: 0 auto; display: block; }

table.toolbar a {
   display: block; float: left;
	white-space: nowrap;
	border: 1px solid #fbfbfb;
	padding: 1px 5px;
	cursor: pointer;
}

table.toolbar a:hover {
	border-left: 1px solid #eee;
	border-top: 1px solid #eee;
	border-right: 1px solid #ccc;
	border-bottom: 1px solid #ccc;
	text-decoration: none;
	color: #0B55C4;
}

/** for massmail component **/
td#mm_pane			{ width: 90%; }
input#mm_subject    { width: 200px; }
textarea#mm_message { width: 100%; }

/* pane-sliders  */
.pane-sliders .title {
	margin: 0;
	padding: 2px;
	color: #666;
	cursor: pointer;
}

.pane-sliders .panel   { border: 1px solid #ccc; margin-bottom: 3px;}

.pane-sliders .panel h3 { background: #f6f6f6; color: #666}

.pane-sliders .content { background: #f6f6f6; }

.pane-sliders .adminlist     { border: 0 none; }
.pane-sliders .adminlist td  { border: 0 none; padding: 3px; }

.jpane-toggler  span     { background: transparent url(j_arrow0.png) 5px 50% no-repeat; padding-left: 20px;}
.jpane-toggler-down span { background: transparent url(j_arrow_.png) 5px 50% no-repeat; padding-left: 20px;}

.jpane-toggler-down {  border-bottom: 1px solid #ccc; }

/* tabs */

dl.tabs {
	float: left;
	margin: 10px 0 -1px 0;
	z-index: 50;
}

dl.tabs dt {
	float: left;
	padding: 4px 10px;
	border-left: 1px solid #ccc;
	border-right: 1px solid #ccc;
	border-top: 1px solid #ccc;
	margin-left: 3px;
	background: #f0f0f0;
	color: #666;
}

dl.tabs dt.open {
	background: #F9F9F9;
	border-bottom: 1px solid #F9F9F9;
	z-index: 100;
	color: #000;
}

div.current {
	clear: both;
	border: 1px solid #ccc;
	padding: 10px 10px;
}

div.current dd {
	padding: 0;
	margin: 0;
}
/** cpanel settings **/

#cpanel div.icon {
	text-align: center;
/*	margin-right: 5px;*/
	float: left;
/*	margin-bottom: 5px;*/
}

#cpanel div.icon a {
	display: block;
	float: left;
	border: 1px solid #f0f0f0;
	height: 87px;
	width: 103px;
	color: #666;
	vertical-align: middle;
	text-decoration: none;
	padding-top: 8px;
}

#cpanel div.icon a:hover {
	border-left: 1px solid #eee;
	border-top: 1px solid #eee;
	border-right: 1px solid #ccc;
	border-bottom: 1px solid #ccc;
	background: #f9f9f9;
	color: #0B55C4;
}

#cpanel img  { padding: 10px 0; margin: 0 auto; }
#cpanel span { display: block; text-align: center; font-size: 9pt; }

/* standard form style table */
div.col { float: left; }

div.width-45 { width: 45%; }
div.width-55 { width: 55%; }
div.width-50 { width: 50%; }
div.width-70 { width: 70%; }
div.width-30 { width: 30%; }
div.width-60 { width: 60%; }
div.width-40 { width: 40%; }

table.admintable td 					 { padding: 3px; }
table.admintable td.key,
table.admintable td.paramlist_key {
	background-color: #f6f6f6;
	text-align: right;
	width: 140px;
	color: #666;
	font-weight: bold;
	border-bottom: 1px solid #e9e9e9;
	border-right: 1px solid #e9e9e9;
}

table.paramlist td.paramlist_description {
	background-color: #f6f6f6;
	text-align: left;
	width: 170px;
	color: #333;
	font-weight: normal;
	border-bottom: 1px solid #e9e9e9;
	border-right: 1px solid #e9e9e9;
}

table.admintable td.key.vtop { vertical-align: top; }

table.adminform {
	/*background-color: #f9f9f9;
	border: solid 1px #d5d5d5;*/
	width: 100%;
	border-collapse: collapse;
	margin: 8px 0 10px 0;
	margin-bottom: 15px;
	width: 100%;
}
table.adminform.nospace { margin-bottom: 0; }
table.adminform tr.row0 { background-color: #f9f9f9; }
table.adminform tr.row1 { background-color: #eeeeee; }

table.adminform th {
	font-size: 11px;
	padding: 6px 2px 4px 4px;
	text-align: left;
	height: 25px;
	color: #000;
	background-repeat: repeat;
}
table.adminform td { padding: 3px; text-align: left; }

table.adminform td.filter{
	text-align: left;
}

table.adminform td.helpMenu{
	text-align: right;
}


fieldset.adminform { border: 1px solid #ccc; margin: 0 10px 10px 10px; }

/** Table styles **/

table.adminlist {
	width: 100%;
	border-spacing: 1px;
	background-color: #e7e7e7;
	color: #666;
}

table.adminlist td,
table.adminlist th { padding: 4px; }

table.adminlist thead th {
	text-align: center;
	background: #f0f0f0;
	color: #666;
	border-bottom: 1px solid #999;
	border-left: 1px solid #fff;
}

table.adminlist thead a:hover { text-decoration: none; }

table.adminlist thead th img { vertical-align: middle; }

table.adminlist tbody th { font-weight: bold; }

table.adminlist tbody tr			{ background-color: #fff;  text-align: left; }
table.adminlist tbody tr.row1 	{ background: #f9f9f9; border-top: 1px solid #fff; }

table.adminlist tbody tr.row0:hover td,
table.adminlist tbody tr.row1:hover td  { background-color: #ffd ; }

table.adminlist tbody tr td 	   { height: 25px; background: #fff; border: 1px solid #fff; }
table.adminlist tbody tr.row1 td { background: #f9f9f9; border-top: 1px solid #FFF; }

table.adminlist tfoot tr { text-align: center;  color: #333; }
table.adminlist tfoot td,
table.adminlist tfoot th { background-color: #f3f3f3; border-top: 1px solid #999; text-align: center; }

table.adminlist td.order 		{ text-align: center; white-space: nowrap; }
table.adminlist td.order span { float: left; display: block; width: 20px; text-align: center; }

table.adminlist .pagination { display:table; padding:0;  margin:0 auto;	 }


    .style2
    {
        height: 34px;
    }

body, td,th,div,li,ul,input,select,textarea{font-family:"Century Gothic", Courier, monospace ; font-size:14px}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
  <br />
 <table width ="100%">
 <tr><td style="vertical-align:top"> 
 <table id="signin" width="500" align="center" cellpadding="6" cellspacing="1">
    <tr>
      <th>Sign In</th>
    </tr>
    <tr>
      <td style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #909e91;">Enter your sign in name and password</td>
    </tr>
    <tr>
      <td>Username:<br />
       <asp:TextBox  ID= "txtusername" runat="server" Width="185px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtusername" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
      <td>Password:<br />
     <asp:TextBox  ID="txtpassword" Width="185"  runat="server" TextMode="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
              ControlToValidate="txtpassword" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
      <td>
      Authentication Type:
          <br />
          <asp:DropDownList ID="txtlopgintype" runat="server">
              <asp:ListItem Value="1">Database</asp:ListItem>
              <asp:ListItem Value="2" Selected="True">Active Directory</asp:ListItem>
          </asp:DropDownList>
        &nbsp;<asp:Label ID="lblError" runat="server" Text="" ForeColor="#FF3300"></asp:Label> &nbsp;</td>
    </tr>
    <tr>
      <td>
         
        </td>
    </tr>
    <tr>
      <td style="background: #f0f0f0; text-align: center;">
          <asp:Button ID="cmdLogin" runat="server" Text="Sign In" 
              CssClass="button-green" />  
          &nbsp;</td>
    </tr>
  </table>
  </td>
 <td>
   <table class="adminform">
            <tr>
              <td width="55%" valign="top" style="padding-left: 10px;">
                 <div id="cpanel">
            <table width="550" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="top"><div class="icon"> <a href="help/irindex.html" runat="server" id="menuIR"><img src="images/menu_01.png" alt="Online Incident" border="0"  />	 
    <span>Help<br />
    Online Incident</span></a> </div></td>
    <td valign="top"><div class="icon"><a href="help/cfbindex.html" runat="server" id="menuCFB"><img src="images/menu_02.png" alt="Customer Feedback" border="0" /><span>Help<br />
      CustomerFeedback</span></a></div></td>
    <td valign="top"><div class="icon"> <a href="help/idpindex.html" runat="server" id="menuIDP"><img src="images/menu_03.png" alt="Individual Development Plan" border="0" /><span>Help<br />
    IDP</span></a></div></td>
    <td valign="top"><div class="icon">  <a href="help/ssipindex.html" runat="server" id="menuSSIP"><img src="images/menu_05.png" alt="Staff Sugestion &amp; Innovation Program" border="0" /><span>Help <br />
      Staff Suggestion</span></a></div></td>
    <td valign="top"><div class="icon"> <a href="help/ladderindex.html" runat="server" id="menuLadder"><img src="images/menu_03.png" alt="Star" border="0" />
    <br />  Nursing Ladder
    </a></div></td>
  </tr>
  <!--
    <tr>
    <td><div class="icon">  <a href="#" ><img src="images/menu_06.png" alt="Accounting" border="0" /><span>Accounting</span></a> </div></td>
    <td><div class="icon"> <a href="#"><img src="images/menu_07.png" alt="Cashier"  border="0"/><span>Cashier</span></a> </div></td>
    <td><div class="icon"> <a href="crm/index.php"><img src="images/menu_08.png" alt="CRM" border="0" /><span>CRM</span></a> </div></td>
    <td><div class="icon"> <a href="#"><img src="images/menu_09.png" alt="Queue" border="0" /><span>Queue</span></a> </div></td>
    <td><div class="icon"> <a href="#"><img src="images/menu10.png" alt="Inventory" border="0" /><span>Inventory</span></a> </div></td>
  </tr>
  -->
  <tr>
    <td valign="top"><div class="icon"> <a href="#"  runat="server" id="menuFTE"></a> </div></td>
    <td valign="top"><div class="icon"> <a href="#"  runat="server" id="menuUser"></a> </div></td>
    <td valign="top"><div class="icon"> <a href="#"  runat="server" id="menuDoctor"></a> </div></td>
    <td valign="top"><div class="icon"> <a href="#"  runat="server" id="menuDept"></a></div></td>
    <td valign="top"><div class="icon"> <a href="#"  runat="server" id="menuDivision"></a></div></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
            </table>
</div></td>
              <td width="45%" valign="top" style="padding: 8px 15px 0px 0px;">&nbsp;
             </td>
            </tr>
          </table>
 </td>
 </tr>
 </table>
  

 
  <br />
  <br />

</asp:Content>

