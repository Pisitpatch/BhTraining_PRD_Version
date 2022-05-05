<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="ssip_master.aspx.vb" Inherits="ssip_ssip_master" ValidateRequest="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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

    <script type="text/javascript">

        /* Optional: Temporarily hide the "tabber" class so it does not "flash"
        on the page as plain HTML. After tabber runs, the class is changed
        to "tabberlive" and it will appear. */

        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        /*==================================================
        Set the tabber options (must do this before including tabber.js)
        ==================================================*/
        var tabberOptions = {

            'cookie': "tabber", /* Name to use for the cookie */

            'onLoad': function (argsObj) {
                var t = argsObj.tabber;
                var i;

                /* Optional: Add the id of the tabber to the cookie name to allow
                for multiple tabber interfaces on the site.  If you have
                multiple tabber interfaces (even on different pages) I suggest
                setting a unique id on each one, to avoid having the cookie set
                the wrong tab.
                */
                if (t.id) {
                    t.cookie = t.id + t.cookie;
                }

                /* If a cookie was previously set, restore the active tab */
                i = parseInt(getCookie(t.cookie));
                if (isNaN(i)) { return; }
                t.tabShow(i);
                // alert('getCookie(' + t.cookie + ') = ' + i);
            },

            'onClick': function (argsObj) {
                var c = argsObj.tabber.cookie;
                var i = argsObj.index;
                // alert('setCookie(' + c + ',' + i + ')');
                setCookie(c, i);
            }
        };

        /*==================================================
        Cookie functions
        ==================================================*/
        function setCookie(name, value, expires, path, domain, secure) {
            document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
        }

        function getCookie(name) {
            var dc = document.cookie;
            var prefix = name + "=";
            var begin = dc.indexOf("; " + prefix);
            if (begin == -1) {
                begin = dc.indexOf(prefix);
                if (begin != 0) return null;
            } else {
                begin += 2;
            }
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
            return unescape(dc.substring(begin + prefix.length, end));
        }
        function deleteCookie(name, path, domain) {
            if (getCookie(name)) {
                document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
            }
        }

</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />
<link rel="stylesheet" href="../js/tab_simple/example-print.css" type="text/css" media="print" />
    
<script type="text/javascript">
  $(document).ready(function () {
     checkSession('<%response.write(session("bh_username").toString) %>' , ''); // Check session every 1 sec.
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="data">
      <div id="header"><img src="../images/doc01.gif" alt="c" width="32" height="32"  />&nbsp;&nbsp;SSIP Manage Master data</div>
          <div align="right"></div>
      
          <div class="tabber" id="mytabber1">
             <div class="tabbertab">
            <h2>
              Room</h2>
            <asp:GridView ID="GridRoom" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="room_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK0" runat="server" Text='<%# Bind("room_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk0" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Room name">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("room_name") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date"  />
                    
                     
                      <asp:BoundField DataField="create_by_name" HeaderText="By" />
                    
                     
                  </Columns>
              </asp:GridView>
           <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
               <% If GridRoom.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>Development Categories</strong></td>
                <td width="138" class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
              </tr>
               <% End If%>           
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_room" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td colspan="2" valign="top">
                 <asp:Button ID="cmdAddRoom" runat="server" Text="Add Room" />
                     <asp:Button ID="cmdDelCat" runat="server" Text="Delete" />
                </td>
              </tr>
            </table>
            <br />
          </div>
               <div class="tabbertab">
            <h2>
              Objective and rule </h2>

              <table width="100%">
              <tr>
              <td>
                  <cc1:Editor ID="txtobject" runat="server" />
              </td>
              </tr>
               <tr>
              <td>
                  <asp:Label ID="lblPicture" runat="server"></asp:Label>
                  </td>
              </tr>
                 <tr>
              <td>
                  <asp:FileUpload ID="FileUpload0" runat="server" />
              &nbsp;
                  <asp:Button ID="cmdUpload" runat="server" Text="Upload Picture" />
                  <asp:Button ID="cmdDelPicture" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr>
              <td>
                  <asp:Button ID="cmdUpdateObject" runat="server" Text="Update" />
              </td>
              </tr>
              </table>

              </div>
         <div class="tabbertab">
            <h2>
              Definition of Innovation  </h2>
                <table width="100%">
              <tr>
              <td>
                <cc1:Editor ID="txtinnovation" runat="server" />
                
              </td>
              </tr>
                   <tr>
              <td>
                  <asp:Label ID="lblPicture1" runat="server"></asp:Label>
                  </td>
              </tr>
                 <tr>
              <td>
                  <asp:FileUpload ID="FileUpload1" runat="server" />
              &nbsp;
                  <asp:Button ID="Button1" runat="server" Text="Upload Picture" />
                  <asp:Button ID="Button2" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr>
              <td>
                  <asp:Button ID="cmdUpdateInno" runat="server" Text="Update" />
              </td>
              </tr>
              </table>
              </div>
             
              <div class="tabbertab">
            <h2>
              Innovation Committee  </h2>
                <table width="100%">
              <tr>
              <td><cc1:Editor ID="txtcommittee" runat="server" />
                  
              </td>
              </tr>
                   <tr>
              <td>
                  <asp:Label ID="lblPicture2" runat="server"></asp:Label>
                  </td>
              </tr>
                 <tr>
              <td>
                  <asp:FileUpload ID="FileUpload2" runat="server" />
              &nbsp;
                  <asp:Button ID="Button3" runat="server" Text="Upload Picture" />
                  <asp:Button ID="Button4" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr>
              <td>
                  <asp:Button ID="cmdCommittee" runat="server" Text="Update" />
              </td>
              </tr>
              </table>
              </div>

               <div class="tabbertab">
            <h2>
              Innovation Champion  </h2>
                <table width="100%">
              <tr>
              <td>
              <cc1:Editor ID="txtchampion" runat="server" />
                 
              </td>
              </tr>
                   <tr>
              <td>
                  <asp:Label ID="lblPicture3" runat="server"></asp:Label>
                  </td>
              </tr>
                 <tr>
              <td>
                  <asp:FileUpload ID="FileUpload3" runat="server" />
              &nbsp;
                  <asp:Button ID="Button5" runat="server" Text="Upload Picture" />
                  <asp:Button ID="Button6" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr>
              <td>
                  <asp:Button ID="cmdChampion" runat="server" Text="Update" />
              </td>
              </tr>
              </table>
              </div>

                 <div class="tabbertab">
            <h2>
             ประโยชน์อื่นๆ/Benefit  </h2>
                <asp:GridView ID="gridBenefit" runat="server"   CellPadding="3" CssClass="tdata"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="master_benefit_id" 
                  EnableModelValidation="True">
               <HeaderStyle CssClass="colname" />
                  <AlternatingRowStyle BackColor="#eeeeee" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("master_benefit_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Benefit Type">
                         <ItemStyle />
                          <ItemTemplate>
                           
                            
                              <asp:Textbox ID="txttopicname" Width="250px" runat="server" Text='<%# Bind("master_benefit_name") %>' ToolTip='<%# Bind("master_benefit_name") %>'></asp:Textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
                  <br />
              <table width="100%">
              <tr>
              <td width="300">ประโยชน์อื่นๆ/Benefit</td>
              <td>
                  <asp:TextBox ID="txtadd_recog" runat="server"></asp:TextBox>
                  <asp:Button ID="cmdAddRecog" runat="server" Text="Add" />
&nbsp;<asp:Button ID="cmdSaveRecog" runat="server" Text="Save" />
                  &nbsp;<asp:Button ID="cmdDelRecog" runat="server" Text="Delete" />
                  </td>
              </tr>
              </table>
              </div>

                  <div class="tabbertab">
            <h2>
             Reward Promotion Category  </h2>
                <asp:GridView ID="gridCategory" runat="server"   CellPadding="3" CssClass="tdata"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="ssip_cat_id" 
                  EnableModelValidation="True">
               <HeaderStyle CssClass="colname" />
                  <AlternatingRowStyle BackColor="#eeeeee" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("ssip_cat_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Promotion Category Name">
                         <ItemStyle />
                          <ItemTemplate>
                           
                            
                              <asp:Textbox ID="txttopicname" Width="250px" runat="server" Text='<%# Bind("cat_name") %>' ToolTip='<%# Bind("cat_name") %>'></asp:Textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
                  <br />
              <table width="100%">
              <tr>
              <td width="300">Promotion Category</td>
              <td>
                  <asp:TextBox ID="txtadd_category" runat="server"></asp:TextBox>
                  <asp:Button ID="cmdAddCategory" runat="server" Text="Add" />
&nbsp;<asp:Button ID="cmdSaveCategory" runat="server" Text="Save" />
                  &nbsp;<asp:Button ID="cmdDelCategory" runat="server" Text="Delete" />
                  </td>
              </tr>
              </table>
              </div>
          </div>
       
        </div>
</asp:Content>
