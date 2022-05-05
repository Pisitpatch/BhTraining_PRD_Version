<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_master.aspx.vb" Inherits="srp_srp_master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
<script type="text/javascript">

    $(function () {

        //$.fn.media.mapFormat('avi','quicktime');

        // this one liner handles all the examples on this page

        $('a.media').media();

    });

</script>
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
        checkSession('<%response.write(session("bh_username").toString) %>', ''); // Check session every 1 sec.
    });
</script>
    <style type="text/css">
* {
  margin: 0;
  padding: 0;
  }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="data">
      <div id="header"><img src="../images/star_32.png" alt="c" width="32" height="32"  />&nbsp;&nbsp;SRP Manage Master data</div>
          <div align="right"></div>
      
          <div class="tabber" id="mytabber1">
           
              <div class="tabbertab">
            <h2>
             News and Events </h2>
                <table width="100%">
              <tr>
              <td><cc1:Editor ID="txtnews" runat="server" />
                  
              </td>
              </tr>
                      <tr>
              <td>
              
              &nbsp;</td>
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
                  <asp:Button ID="Button3" runat="server" Text="Upload Picture" Width="100px" />
                  <asp:Button ID="Button4" runat="server" Text="Delete picture" Width="100px" />
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
             SRP News</h2>
                      <asp:GridView ID="GridNews" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="new_id" EnableModelValidation="True" ForeColor="#333333" GridLines="None" Width="100%">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField>
                                   <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk1" runat="server" />
                                         <asp:Label ID="lblPK1" runat="server" Text='<%# bind("new_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="News Title">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red">[Edit]</asp:LinkButton>
                                        &nbsp;   <a href="#" onclick=" window.open('srp_news_detail.aspx?id=<%# Eval("new_id") %>', '<%# Eval("title_th") %>', 'x', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600');">
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("title_th") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="new_date" HeaderText="News date" />
                                <asp:BoundField DataField="status_name" HeaderText="Status" />
                                <asp:TemplateField HeaderText="By">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("create_by") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                  <br />
                  <br />
                   <a id="addNew" ;="" href="#" onclick="window.open('srp_news_edit.aspx?mode=add', '', 'alwaysRaised,scrollbars =no,status=yes,width=850,height=600')" >[Add new]</a> <asp:Button ID="cmdDelNews" runat="server" Text="Delete News" />
                           &nbsp;<asp:Button ID="cmdNewsRefresh" runat="server" Text="Refresh" />
                  </div>
       <div class="tabbertab">
            <h2>
             On the spot card </h2>
           <table width="100%">
               <tr>
                   <td>
                        <asp:GridView ID="gridBanner" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="banner_id" EnableModelValidation="True" ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="0">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField>
                                   <ItemStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Picture">
                                    <ItemStyle Width="200px" />
                                    <ItemTemplate>
                                        <img src="../share/ots_card/<%# Eval("banner_path") %>"  height="100"  />
                                       
                                        <asp:Label ID="lblPK1" runat="server" Text='<%# bind("banner_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Picture name">
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="lblBannerPath" runat="server" Text='<%# Bind("banner_path") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Detail">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBannerDetail" runat="server" Text='<%# Bind("banner_detail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Create By">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("create_by") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </td>
               </tr>

               <tr>
                   <td>
                       <br />
                       <fieldset>
                           <legend>Add new OTS Picture</legend>
                            <table >
                       <tr>
                           <td width="150" height="30">OTS Image</td>
                           <td>
                               <asp:FileUpload ID="FileUploadOTS" runat="server" />
                           </td>
                       </tr>
                       <tr>
                           <td width="150" height="30">Detail</td>
                           <td>
                               <asp:TextBox ID="txtots_detail" runat="server" Width="300px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td width="150" height="30">&nbsp;</td>
                           <td>
                               <asp:Button ID="cmdAddOTS" runat="server" Text="Add new image" />
                           &nbsp;<asp:Button ID="cmdDelBanner" runat="server" Text="Delete Picture" />
                           </td>
                       </tr>
                       </table>
                       </fieldset>
                      &nbsp;</td>
               </tr>

           </table>
              </div>
          </div>
       
        </div>
</asp:Content>



