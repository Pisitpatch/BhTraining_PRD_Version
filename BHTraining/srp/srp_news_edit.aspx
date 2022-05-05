<%@ Page Language="VB" AutoEventWireup="false" CodeFile="srp_news_edit.aspx.vb" Inherits="srp_srp_news_edit" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link href="../css/main.css" rel="stylesheet" type="text/css" />
 <link href="../css/popup.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.6.min.js" charset="utf-8"></script>

<!-- Load TinyMCE -->
<script type="text/javascript" src="../js/tiny_mce/jquery.tinymce.js"></script>
<!-- <script language="javascript" type="text/javascript" src="../js/jscripts/tiny_mce/tiny_mce.js"></script> -->
<script type="text/javascript">

    function fileBrowserCallBack(field_name, url, type, win) {
        // This is where you insert your custom filebrowser logic
        // alert("Example of filebrowser callback: field_name: " + field_name + ", url: " + url + ", type: " + type);
        // Insert new URL, this would normaly be done in a popup
        // win.document.forms[0].elements[field_name].value = "someurl.htm";

        tinyMCE.activeEditor.windowManager.open({
            url: "save.php?page=customfilebrowser.php",
            width: 782,
            height: 440,
            inline: "yes",
            close_previous: "no"
        }, {
            window: win,
            input: field_name
        });
    }

    $().ready(function () {
        $('textarea.tinymce').tinymce({
            // Location of TinyMCE script
            script_url: '../js/tiny_mce/tiny_mce.js',

            // General options
            language: "th",
            theme: "advanced",

            file_browser_callback: "fileBrowserCallBack",
            plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "pastetext,pasteword,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media",
            //		theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",

            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            // content_css: "../js/tiny_mce327/jscripts/tiny_mce/themes/mycontent.css",
            //content_css : "js/jscripts/tiny_mce/themes/mycontent.css",
            // Drop lists for link/image/media/template dialogs

            template_external_list_url: "lists/template_list.js",
            external_link_list_url: "lists/link_list.js",
            /*		
            external_image_list_url : tinyMCEImageList = new Array(
            ["Logo  qweeewe1", "media/logo.jpg"],
            ["Logo 2 Over", "media/logo_over.jpg"]
            ),
            */

            //external_image_list_url : "../share/image_list.js?time_stamp=<?php echo time();?>",
            media_external_list_url: "lists/media_list.js?time_stamp=123",


            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }


        });


    });
</script>
  

</head>
<body>

    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <div id="data">
    <table class="tdata3" width="100%">
    <tr>
    <td width="120">หัวข้อข่าว</td>
    <td >
        <asp:TextBox ID="txttopic" runat="server" Width="90%"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td >วันที่ข่าว</td>
    <td >
        <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
          <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server"  
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate" PopupButtonID="Image1" >
                              </asp:CalendarExtender>
                              <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
        </td>
    </tr>
    <tr>
    <td  >รายละเอียด</td>
    <td  >
        <asp:TextBox ID="txtdetail" runat="server" Rows="5" Width="90%" TextMode="MultiLine" ></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td  >&nbsp;</td>
    <td  >
        <asp:Label ID="lblFileName" runat="server"></asp:Label> <asp:Label ID="lblFilePath" runat="server" Visible="false"></asp:Label>
&nbsp;<asp:LinkButton ID="linkDeleteFile" runat="server" OnClientClick="return confirm('Are you sure you want to delete file?')" ForeColor="red">[Delete File]</asp:LinkButton>
        </td>
    </tr>
    <tr>
    <td  >ไฟล์แนบประกอบ</td>
    <td  >
        <asp:FileUpload ID="FileUpload0" runat="server" />
        </td>
    </tr>
    <tr>
    <td  >สถานะข่าว</td>
    <td  >
        <asp:DropDownList ID="txtstatus" runat="server">
        <asp:ListItem Value="1">Active</asp:ListItem>
        <asp:ListItem Value="0">Inactive</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td >&nbsp;</td>
    <td >
        <asp:CheckBox ID="chkNew" runat="server" Text="แสดงไอคอนข่าวใหม่" /> <img src="../images/newlogo.gif" />
&nbsp;</td>
    </tr>
    <tr>
    <td >&nbsp;</td>
    <td >
        <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" Font-Bold="True" />
&nbsp;<input type="button" name="cmdClose" id="cmdClose" value="Close Window" onclick="window.close();" />
        </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>

