﻿<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="jci_select_question.aspx.vb" Inherits="jci_jci_select_question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" type="text/javascript">
<!--
    //v1.7
    // Flash Player Version Detection
    // Detect Client Browser type
    // Copyright 2005-2008 Adobe Systems Incorporated.  All rights reserved.
    var isIE = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
    var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
    var isOpera = (navigator.userAgent.indexOf("Opera") != -1) ? true : false;
    function ControlVersion() {
        var version;
        var axo;
        var e;
        // NOTE : new ActiveXObject(strFoo) throws an exception if strFoo isn't in the registry
        try {
            // version will be set for 7.X or greater players
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
            version = axo.GetVariable("$version");
        } catch (e) {
        }
        if (!version) {
            try {
                // version will be set for 6.X players only
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");

                // installed player is some revision of 6.0
                // GetVariable("$version") crashes for versions 6.0.22 through 6.0.29,
                // so we have to be careful. 

                // default to the first public version
                version = "WIN 6,0,21,0";
                // throws if AllowScripAccess does not exist (introduced in 6.0r47)		
                axo.AllowScriptAccess = "always";
                // safe to call for 6.0r47 or greater
                version = axo.GetVariable("$version");
            } catch (e) {
            }
        }
        if (!version) {
            try {
                // version will be set for 4.X or 5.X player
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
                version = axo.GetVariable("$version");
            } catch (e) {
            }
        }
        if (!version) {
            try {
                // version will be set for 3.X player
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.3");
                version = "WIN 3,0,18,0";
            } catch (e) {
            }
        }
        if (!version) {
            try {
                // version will be set for 2.X player
                axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
                version = "WIN 2,0,0,11";
            } catch (e) {
                version = -1;
            }
        }

        return version;
    }
    // JavaScript helper required to detect Flash Player PlugIn version information
    function GetSwfVer() {
        // NS/Opera version >= 3 check for Flash plugin in plugin array
        var flashVer = -1;

        if (navigator.plugins != null && navigator.plugins.length > 0) {
            if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
                var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
                var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
                var descArray = flashDescription.split(" ");
                var tempArrayMajor = descArray[2].split(".");
                var versionMajor = tempArrayMajor[0];
                var versionMinor = tempArrayMajor[1];
                var versionRevision = descArray[3];
                if (versionRevision == "") {
                    versionRevision = descArray[4];
                }
                if (versionRevision[0] == "d") {
                    versionRevision = versionRevision.substring(1);
                } else if (versionRevision[0] == "r") {
                    versionRevision = versionRevision.substring(1);
                    if (versionRevision.indexOf("d") > 0) {
                        versionRevision = versionRevision.substring(0, versionRevision.indexOf("d"));
                    }
                }
                var flashVer = versionMajor + "." + versionMinor + "." + versionRevision;
            }
        }
        // MSN/WebTV 2.6 supports Flash 4
        else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.6") != -1) flashVer = 4;
        // WebTV 2.5 supports Flash 3
        else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.5") != -1) flashVer = 3;
        // older WebTV supports Flash 2
        else if (navigator.userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 2;
        else if (isIE && isWin && !isOpera) {
            flashVer = ControlVersion();
        }
        return flashVer;
    }
    // When called with reqMajorVer, reqMinorVer, reqRevision returns true if that version or greater is available
    function DetectFlashVer(reqMajorVer, reqMinorVer, reqRevision) {
        versionStr = GetSwfVer();
        if (versionStr == -1) {
            return false;
        } else if (versionStr != 0) {
            if (isIE && isWin && !isOpera) {
                // Given "WIN 2,0,0,11"
                tempArray = versionStr.split(" "); 	// ["WIN", "2,0,0,11"]
                tempString = tempArray[1]; 		// "2,0,0,11"
                versionArray = tempString.split(","); // ['2', '0', '0', '11']
            } else {
                versionArray = versionStr.split(".");
            }
            var versionMajor = versionArray[0];
            var versionMinor = versionArray[1];
            var versionRevision = versionArray[2];
            // is the major.revision >= requested major.revision AND the minor version >= requested minor
            if (versionMajor > parseFloat(reqMajorVer)) {
                return true;
            } else if (versionMajor == parseFloat(reqMajorVer)) {
                if (versionMinor > parseFloat(reqMinorVer))
                    return true;
                else if (versionMinor == parseFloat(reqMinorVer)) {
                    if (versionRevision >= parseFloat(reqRevision))
                        return true;
                }
            }
            return false;
        }
    }
    function AC_AddExtension(src, ext) {
        if (src.indexOf('?') != -1)
            return src.replace(/\?/, ext + '?');
        else
            return src + ext;
    }
    function AC_Generateobj(objAttrs, params, embedAttrs) {
        var str = '';
        if (isIE && isWin && !isOpera) {
            str += '<object ';
            for (var i in objAttrs) {
                str += i + '="' + objAttrs[i] + '" ';
            }
            str += '>';
            for (var i in params) {
                str += '<param name="' + i + '" value="' + params[i] + '" /> ';
            }
            str += '</object>';
        }
        else {
            str += '<embed ';
            for (var i in embedAttrs) {
                str += i + '="' + embedAttrs[i] + '" ';
            }
            str += '> </embed>';
        }
        document.write(str);
    }
    function AC_FL_RunContent() {
        var ret =
    AC_GetArgs
    (arguments, ".swf", "movie", "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
     , "application/x-shockwave-flash"
    );
        AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
    }
    function AC_SW_RunContent() {
        var ret =
    AC_GetArgs
    (arguments, ".dcr", "src", "clsid:166B1BCA-3F9C-11CF-8075-444553540000"
     , null
    );
        AC_Generateobj(ret.objAttrs, ret.params, ret.embedAttrs);
    }
    function AC_GetArgs(args, ext, srcParamName, classid, mimeType) {
        var ret = new Object();
        ret.embedAttrs = new Object();
        ret.params = new Object();
        ret.objAttrs = new Object();
        for (var i = 0; i < args.length; i = i + 2) {
            var currArg = args[i].toLowerCase();
            switch (currArg) {
                case "classid":
                    break;
                case "pluginspage":
                    ret.embedAttrs[args[i]] = args[i + 1];
                    break;
                case "src":
                case "movie":
                    args[i + 1] = AC_AddExtension(args[i + 1], ext);
                    ret.embedAttrs["src"] = args[i + 1];
                    ret.params[srcParamName] = args[i + 1];
                    break;
                case "onafterupdate":
                case "onbeforeupdate":
                case "onblur":
                case "oncellchange":
                case "onclick":
                case "ondblclick":
                case "ondrag":
                case "ondragend":
                case "ondragenter":
                case "ondragleave":
                case "ondragover":
                case "ondrop":
                case "onfinish":
                case "onfocus":
                case "onhelp":
                case "onmousedown":
                case "onmouseup":
                case "onmouseover":
                case "onmousemove":
                case "onmouseout":
                case "onkeypress":
                case "onkeydown":
                case "onkeyup":
                case "onload":
                case "onlosecapture":
                case "onpropertychange":
                case "onreadystatechange":
                case "onrowsdelete":
                case "onrowenter":
                case "onrowexit":
                case "onrowsinserted":
                case "onstart":
                case "onscroll":
                case "onbeforeeditfocus":
                case "onactivate":
                case "onbeforedeactivate":
                case "ondeactivate":
                case "type":
                case "codebase":
                case "id":
                    ret.objAttrs[args[i]] = args[i + 1];
                    break;
                case "width":
                case "height":
                case "align":
                case "vspace":
                case "hspace":
                case "class":
                case "title":
                case "accesskey":
                case "name":
                case "tabindex":
                    ret.embedAttrs[args[i]] = ret.objAttrs[args[i]] = args[i + 1];
                    break;
                default:
                    ret.embedAttrs[args[i]] = ret.params[args[i]] = args[i + 1];
            }
        }
        ret.objAttrs["classid"] = classid;
        if (mimeType) ret.embedAttrs["type"] = mimeType;
        return ret;
    }
// -->
</script>
<script language="JavaScript" type="text/javascript">
    AC_FL_RunContent(
		'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0',
		'width', '800',
		'height', '400',
		'src', 'webrecord_bh',
		'quality', 'high',
		'pluginspage', 'http://www.adobe.com/go/getflashplayer',
		'align', 'middle',
		'play', 'true',
		'loop', 'true',
		'scale', 'showall',
		'wmode', 'window',
		'devicefont', 'false',
		'id', 'webrecord_bh',
		'bgcolor', '#333333',
		'name', 'webrecord_bh',
		'menu', 'true',
		'allowFullScreen', 'false',
		'allowScriptAccess', 'sameDomain',
		'movie', 'webrecord_bh',
		'salign', ''
		); //end AC code
</script>

    <script type="text/javascript">
    function submitAnswer(qid) {
        // var doc = document.forms[0];
        if (confirm("Are you sure you want to submit your answer ?")) {
            location.href = "save_answer.aspx?qid=" + qid;
        } else {
            return false;
        }
       
       // doc.submit();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" cellspacing="0" cellpadding="0" style="background: #E3E3E3; min-height: 900px;">
  
  <tr>
    <td valign="top">
   
    <p>&nbsp;</p><table width="100%">
  <tr>
     
    <td width="33%" valign="top" style="padding: 0px 20px;">
    <asp:Panel ID="panel_eval" runat="server" BorderStyle="Solid" BorderWidth="0px" >
    <div id="Div1" runat="server" visible ="false">
      <table width="100%" align="center" cellpadding="6" cellspacing="0">
        <tr>
          <th width="100%" align="left" style="background: #409997; color: #FFF; font-weight: normal; font-size: 16px; text-align: left; padding: 6px 10px;"><strong>แบบประเมินกิจกรรม</strong></th>
        </tr>
        <tr>
          <td></td>
        </tr>
      </table>
      
    <table width="100%" cellpadding="6" cellspacing="0">
<tr>
<td width="15" style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #333333; vertical-align:top">1.</td>
<td style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #333333; vertical-align:top">
  <asp:Label ID="lblE1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
</tr>
   
<tr>
<td width="15" bgcolor="#FFFFFF">&nbsp;</td>
<td bgcolor="#FFFFFF">
<table width="100%">
<tr><td>
    <asp:RadioButtonList ID="txtanswer1" runat="server" 
        RepeatDirection="Horizontal" Width="300px">
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtanswer1" Display="Dynamic" 
        ErrorMessage="Please choose your answer"></asp:RequiredFieldValidator>
</td></tr>
</table></td>
</tr>
   
<tr>
<td width="15" style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #333333; vertical-align:top">2.</td>
<td style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #333333; vertical-align:top">
    <asp:Label ID="lblE2" runat="server" Text="Label" Font-Bold="True"></asp:Label>    </td>
</tr>
   
<tr>
<td width="15" bgcolor="#FFFFFF">&nbsp;</td>
<td bgcolor="#FFFFFF">
    <asp:RadioButtonList ID="txtanswer2" runat="server" 
        RepeatDirection="Horizontal" Width="300px">
        <asp:ListItem>ใช่ /Yes</asp:ListItem>
        <asp:ListItem>ไม่ใช่ /No</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtanswer2" Display="Dynamic" 
        ErrorMessage="Please choose your answer"></asp:RequiredFieldValidator>    </td>
</tr>
<tr>
<td colspan="2" align="center" bgcolor="#FFFFFF">
    <asp:Button ID="cmdSubmit" runat="server" Font-Bold="True" Text="Submit Review" />    
</td>
</tr>
</table>
</div>
    </asp:Panel>
<asp:Label ID="lblGroupList" runat="server" Text="Group List" Visible="False"></asp:Label>
    
  
    
    </td>
    <td valign="top" style="padding-right: 50px;">     
      <asp:Label ID="lblQuestionList" runat="server" Text="Question List"></asp:Label> 
     
     </td>
  </tr>
</table>


    <p>&nbsp;</p>
    <p>&nbsp;</p></td>
  </tr>
  </table>
</asp:Content>

