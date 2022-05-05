<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="false" CodeFile="star_master.aspx.vb" Inherits="star_star_master" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="data">
      <div id="header"><img src="../images/star_32.png" alt="c" width="32" height="32"  />&nbsp;&nbsp;Star Manage Master data</div>
          <div align="right"></div>
      
          <div class="tabber" id="mytabber1">
             <div class="tabbertab">
            <h2>
              Complementary Note</h2>
            <asp:GridView ID="GridRoom" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="note_id" 
                >
                 <HeaderStyle BackColor="pink" ForeColor="White" HorizontalAlign="Center"  />
                  <AlternatingRowStyle BackColor="#fed6f5" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                           <asp:Label ID="lblPK" runat="server" Text='<%# Bind("note_id") %>' 
                                  Visible="false"></asp:Label>
                             
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Complementary Note">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                           
                            
                              <asp:Textbox ID="txtnotename" runat="server" Width="250px" Text='<%# Bind("note_th") %>'></asp:Textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Endrose">
                          <ItemTemplate>
                             
                               <asp:DropDownList ID="txtendrose" runat="server" Width="185px">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Endorse</asp:ListItem>
               <asp:ListItem Value="2">Do Not Endorse</asp:ListItem>
                
             </asp:DropDownList>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Types of recognitions">
                          <ItemTemplate>
                               <asp:DropDownList ID="txtrecog" runat="server" Width="185px" DataTextField="recognition_name" DataValueField="recognition_id">
                      </asp:DropDownList>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="Recognition Award">
                          <ItemTemplate>
                            <asp:DropDownList ID="txtaward" runat="server">
           <asp:ListItem Value="0">0</asp:ListItem>
             <asp:ListItem Value="100">100</asp:ListItem>
             <asp:ListItem Value="200">200</asp:ListItem>
             <asp:ListItem Value="300">300</asp:ListItem>
             <asp:ListItem Value="1000">1000</asp:ListItem>
             <asp:ListItem Value="5000">5000</asp:ListItem>
             <asp:ListItem Value="10000">10000</asp:ListItem>
             </asp:DropDownList>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
             <table width="100%">
              <tr>
              <td width="300">Complementary Note</td>
              <td>
                  <asp:TextBox ID="txtadd_note" runat="server"></asp:TextBox>
                  <asp:Button ID="cmdAddNote" runat="server" Text="Add" />
&nbsp;<asp:Button ID="cmdSaveNote" runat="server" Text="Save" />
                  &nbsp;<asp:Button ID="cmdDeleteNote" runat="server" Text="Delete" />
                  </td>
              </tr>
              </table>
            <br />
          </div>
               <div class="tabbertab">
            <h2>
              Outstanding Charecteristic Topic </h2>

                 <asp:GridView ID="GridAdmire" runat="server"  cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="admire_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="pink" ForeColor="White" HorizontalAlign="Center"  />
                  <AlternatingRowStyle BackColor="#fed6f5" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("admire_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="เหตุการณ์/คำชม">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                           
                            
                              <asp:Textbox ID="txttopicname" Width="250px" runat="server" Text='<%# Bind("admire_topic") %>' ToolTip='<%# Bind("admire_topic") %>'></asp:Textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Co">
                      <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                          <ItemTemplate>
                              <div style="text-align:center"><asp:CheckBox ID="txtclear" runat="server" Text="" /></div>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="A">
                         <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                          <ItemTemplate>
                               <div style="text-align:center"><asp:CheckBox ID="txtcare" runat="server" Text="" /></div>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="S">
                         <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                          <ItemTemplate>
                            <div style="text-align:center"><asp:CheckBox ID="txtsmart" runat="server" Text="" /></div>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="T">
                         <HeaderStyle HorizontalAlign="Center" />
                      <ItemStyle HorizontalAlign="Center" />
                          <ItemTemplate>
                              <div style="text-align:center"><asp:CheckBox ID="txtquality" runat="server" Text="" /></div>
                          </ItemTemplate>
                       
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
              <br />
              <table width="100%">
              <tr>
              <td width="300">เหตุการณ์/คำชม</td>
              <td>
                  <asp:TextBox ID="txtadd_admire" runat="server"></asp:TextBox>
                  <asp:Button ID="cmdAddAdmire" runat="server" Text="Add" />
&nbsp;<asp:Button ID="cmdSaveAdmire" runat="server" Text="Save" />
                  &nbsp;<asp:Button ID="cmdDeleteAdmire" runat="server" Text="Delete" />
                  </td>
              </tr>
              </table>
              <table width="100%">
              <tr>
              <td>
                  &nbsp;</td>
              </tr>
               <tr>
              <td>
                  &nbsp;</td>
              </tr>
                 </table>

              </div>
                 <div class="tabbertab">
            <h2>Recognition Type</h2>
               <asp:GridView ID="gridRecognition" runat="server"  cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="recognition_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="pink" ForeColor="White" HorizontalAlign="Center"  />
                  <AlternatingRowStyle BackColor="#fed6f5" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("recognition_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Recognition Type">
                         <ItemStyle />
                          <ItemTemplate>
                           
                            
                              <asp:Textbox ID="txttopicname" Width="250px" runat="server" Text='<%# Bind("recognition_name") %>' ToolTip='<%# Bind("recognition_name") %>'></asp:Textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
                  <br />
              <table width="100%">
              <tr>
              <td width="300">Recognition</td>
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
             Statistic Analysis 
               
             </h2>
             <fieldset style="width:600px">
             <legend>Add Grand Topic</legend>
             <table>
             <tr>
             <td width="120">Grand Topic</td>
             <td> <asp:TextBox ID="txtadd_grandtopic" runat="server" Width="350px"></asp:TextBox> 
              <asp:Button ID="cmdAddGrandTopic" runat="server" Text="Add" />
             </td>
                
             </tr>
             </table>
                
             </fieldset>
             <br />
              <fieldset style="width:600px">
             <legend>Add Sub Topic</legend>
             <table>
             <tr>
             <td width="120">Grand Topic</td>
             <td> 
                 <asp:DropDownList ID="txtadd_subgrandtopic" runat="server" 
                     DataTextField="main_topic_name_th" DataValueField="main_topic_id">
                 </asp:DropDownList>
             </td>
                
             </tr>
             <tr>
             <td width="120">Sub Topic</td>
             <td> <asp:TextBox ID="txtadd_subtopic" runat="server" Width="350px"></asp:TextBox> 
              <asp:Button ID="cmdAddSubTopic" runat="server" Text="Add" />
             </td>
                
             </tr>
             </table>
                
             </fieldset>
             <br />
              <asp:GridView ID="gridGrandTopic" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="main_topic_id" EnableModelValidation="True" 
                >
                 <HeaderStyle BackColor="pink" ForeColor="White" HorizontalAlign="Center"  />
                  <AlternatingRowStyle BackColor="#fed6f5" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                           <asp:Label ID="lblPK" runat="server" Text='<%# Bind("main_topic_id") %>' 
                                  Visible="false"></asp:Label>
                             <asp:Label ID="Labels" runat="server" > <%# Container.DataItemIndex + 1 %>. </asp:Label>
                             
                          </ItemTemplate>
                          <ItemStyle Width="33px" VerticalAlign="Top" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Grand Topic">
                         <ItemStyle Width="280px" VerticalAlign="Top" />
                       
                          <ItemTemplate>
                           
                             
                              <asp:label ID="txttopic" runat="server" Text='<%# Bind("main_topic_name_th") %>'></asp:label>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sub Topic">
                      <ItemStyle VerticalAlign="Top" />
                          <ItemTemplate>
                             
                               <asp:Label ID="lblSubTopic" runat="server"></asp:Label>
                              
                          </ItemTemplate>
                        
                         
                        
                      </asp:TemplateField>
                      <asp:TemplateField>
                          <ItemTemplate>
                               <asp:Label ID="Label2" runat="server"></asp:Label>
                          </ItemTemplate>
                        
                         
                        
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
              </div>
             
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
               <a class="media {width:450, height:370}" href="mediaplayer.swf?file=flash/<% response.write(clip_name) %>"></a>
              <!--  <a class="media {width:450, height:370}" href="mediaplayer.swf?file=flash/test.flv">SWF with FLV (mediaplayer.swf?file62752-1.flv)</a>   -->
              &nbsp;</td>
              </tr>
                   <tr>
              <td>
                  <asp:Label ID="Label1" runat="server"></asp:Label>
                  </td>
              </tr>
                 <tr>
              <td>
                  <asp:FileUpload ID="FileUploadClip" runat="server" />
              &nbsp;
                  <asp:Button ID="cmdAddClip" runat="server" Text="Upload Clip" Width="100px" />
                  <asp:Button ID="cmdDeleteClip" runat="server" Text="Delete Clip" 
                      Width="100px" />
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
   
              
          </div>
       
        </div>
</asp:Content>

