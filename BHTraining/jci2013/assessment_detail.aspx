<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="assessment_detail.aspx.vb" Inherits="jci2013_assessment_detail" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <!-- Add mousewheel plugin (this is optional) -->
	<script type="text/javascript" src="fancyapp/lib/jquery.mousewheel-3.0.6.pack.js"></script>

	<!-- Add fancyBox main JS and CSS files -->
	<script type="text/javascript" src="fancyapp/source/jquery.fancybox.js?v=2.1.5"></script>
	<link rel="stylesheet" type="text/css" href="fancyapp/source/jquery.fancybox.css?v=2.1.5" media="screen" />

    	<!-- Add Button helper (this is optional) -->
	<link rel="stylesheet" type="text/css" href="fancyapp/source/helpers/jquery.fancybox-buttons.css?v=1.0.5" />
	<script type="text/javascript" src="fancyapp/source/helpers/jquery.fancybox-buttons.js?v=1.0.5"></script>

	<!-- Add Thumbnail helper (this is optional) -->
	<link rel="stylesheet" type="text/css" href="fancyapp/source/helpers/jquery.fancybox-thumbs.css?v=1.0.7" />
	<script type="text/javascript" src="fancyapp/source/helpers/jquery.fancybox-thumbs.js?v=1.0.7"></script>

	<!-- Add Media helper (this is optional) -->
	<script type="text/javascript" src="fancyapp/source/helpers/jquery.fancybox-media.js?v=1.0.6"></script>

	<script type="text/javascript">
	    $(document).ready(function () {
	        /*
			 *  Simple image gallery. Uses default settings
			 */

	        $('.fancybox').fancybox({
	            'type': 'image'
	        });

	        /*
			 *  Different effects
			 */

	        // Change title type, overlay closing speed
	        $(".fancybox-effects-a").fancybox({
	            helpers: {
	                title: {
	                    type: 'outside'
	                },
	                overlay: {
	                    speedOut: 0
	                }
	            }
	        });

	        // Disable opening and closing animations, change title type
	        $(".fancybox-effects-b").fancybox({
	            openEffect: 'none',
	            closeEffect: 'none',

	            helpers: {
	                title: {
	                    type: 'over'
	                }
	            }
	        });

	        // Set custom style, close if clicked, change title type and overlay color
	        $(".fancybox-effects-c").fancybox({
	            wrapCSS: 'fancybox-custom',
	            closeClick: true,

	            openEffect: 'none',

	            helpers: {
	                title: {
	                    type: 'inside'
	                },
	                overlay: {
	                    css: {
	                        'background': 'rgba(238,238,238,0.85)'
	                    }
	                }
	            }
	        });

	        // Remove padding, set opening and closing animations, close if clicked and disable overlay
	        $(".fancybox-effects-d").fancybox({
	            padding: 0,

	            openEffect: 'elastic',
	            openSpeed: 150,

	            closeEffect: 'elastic',
	            closeSpeed: 150,

	            closeClick: true,

	            helpers: {
	                overlay: null
	            }
	        });

	        /*
			 *  Button helper. Disable animations, hide close button, change title type and content
			 */

	        $('.fancybox-buttons').fancybox({
	            openEffect: 'none',
	            closeEffect: 'none',

	            prevEffect: 'none',
	            nextEffect: 'none',

	            closeBtn: false,

	            helpers: {
	                title: {
	                    type: 'inside'
	                },
	                buttons: {}
	            },

	            afterLoad: function () {
	                this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
	            }
	        });


	        /*
			 *  Thumbnail helper. Disable animations, hide close button, arrows and slide to next gallery item if clicked
			 */

	        $('.fancybox-thumbs').fancybox({
	            prevEffect: 'none',
	            nextEffect: 'none',

	            closeBtn: false,
	            arrows: false,
	            nextClick: true,

	            helpers: {
	                thumbs: {
	                    width: 50,
	                    height: 50
	                }
	            }
	        });

	        /*
			 *  Media helper. Group items, disable animations, hide arrows, enable media and button helpers.
			*/
	        $('.fancybox-media')
				.attr('rel', 'media-gallery')
				.fancybox({
				    openEffect: 'none',
				    closeEffect: 'none',
				    prevEffect: 'none',
				    nextEffect: 'none',

				    arrows: false,
				    helpers: {
				        media: {},
				        buttons: {}
				    }
				});

	        /*
			 *  Open manually
			 */

	        $("#fancybox-manual-a").click(function () {
	            $.fancybox.open('1_b.jpg');
	        });

	        $("#fancybox-manual-b").click(function () {
	            $.fancybox.open({
	                href: 'iframe.html',
	                type: 'iframe',
	                padding: 5
	            });
	        });

	        $("#fancybox-manual-c").click(function () {
	            $.fancybox.open([
					{
					    href: '1_b.jpg',
					    title: 'My title'
					}, {
					    href: '2_b.jpg',
					    title: '2nd title'
					}, {
					    href: '3_b.jpg'
					}
	            ], {
	                helpers: {
	                    thumbs: {
	                        width: 75,
	                        height: 50
	                    }
	                }
	            });
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

    <script type="text/javascript">

        function openPopup(flag, popupType) {
            if (flag.checked) {
                is_sms = 1;
            } else {
                is_sms = 0;
            }
          //  loadPopup(1);
            my_window = window.open('../incident/popup_recepient.aspx?popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=600');
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="form_list.aspx?menu=3">Form Setting</a> 
        > <a href="assessment_list.aspx?form_id=<% Response.Write(form_id) %>&menu=3">Assessment List</a> >Assessment Detail
    </td>
  </tr>
</table>
    <div class="tabber" id="mytabber2" style="width:100%">
         <div class="tabbertab">
    <h2>Assessement Detail</h2>

<table width="100%" class="box-content">
        
        <tr>
          <td width="150" valign="top" class="rowname" >Emplyee Code</td>
          <td valign="top" >
              <asp:TextBox ID="txtcode" runat="server"></asp:TextBox>
            &nbsp;(Assessors Leader)</td>
        </tr>
       
        <tr>
          <td width="150" valign="top" class="rowname" >Employee Name</td>
          <td valign="top" >
              <asp:TextBox ID="txtleader" runat="server"></asp:TextBox>
            &nbsp;(Assessors Leader)</td>
        </tr>
       
        <tr>
          <td width="150" valign="top" class="rowname" >Date</td>
          <td valign="top" >
              <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
          <td valign="top" class="rowname" >Time</td>
          <td valign="top" >
              <asp:TextBox ID="txttime" runat="server"></asp:TextBox>
            </td>
        </tr>
    
      
        <tr>
          <td valign="top" class="rowname" >Member</td>
          <td valign="top" >
              <asp:TextBox ID="txtmember" runat="server" Rows="3" TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" >Department</td>
          <td valign="top" >
              <asp:TextBox ID="txtcustom_dept" runat="server"></asp:TextBox>
              <asp:DropDownList ID="txtdept" runat="server" DataValueField="dept_id" DataTextField="dept_name_en" Width="400px" AutoPostBack="True">
              </asp:DropDownList>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" >Type</td>
          <td valign="top" >
              <asp:TextBox ID="txtcustom_type" runat="server"></asp:TextBox>
              <asp:DropDownList ID="txttype" runat="server" DataTextField="type_name" DataValueField="type_id" Width="400px" AutoPostBack="True">
              </asp:DropDownList>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" >Location</td>
          <td valign="top" >
              <asp:TextBox ID="txtcustom_location" runat="server"></asp:TextBox>
              <asp:DropDownList ID="txtlocation" runat="server" DataTextField="location_name" DataValueField="location_id" Width="400px" AutoPostBack="True">
              </asp:DropDownList>
            </td>
        </tr>
    
      
        <tr>
          <td valign="top" class="rowname">Building</td>
          <td valign="top">
              <asp:TextBox ID="txtcustom_building" runat="server"></asp:TextBox>
            </td>
        </tr>
    
      
        <tr>
          <td valign="top" class="rowname">Impression</td>
          <td valign="top">
              <asp:TextBox ID="txtimpression" runat="server" Rows="3" TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
    
      
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data." CellPadding="4" AllowPaging="True" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" PageSize="100" ShowFooter="True"  >
        <Columns>
         <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="50px" />
               <ItemTemplate>
                  <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="ME ">
            
                <ItemTemplate>
                    <asp:Label ID="lblPK" runat="server" Text='<%# Bind("assessment_me_id")%>' Visible="true"></asp:Label> : 
                      <asp:Label ID="lblIpadPK" runat="server" Text='<%# Bind("ipad_assessment_me_id")%>' Visible="true"></asp:Label>
                     <asp:Label ID="lblScore" runat="server" Text='<%# Bind("me_score_level")%>' Visible="false"></asp:Label>
                   
                    <span style="color:green">
                    [<asp:Label ID="Label1" runat="server" Text='<%# Bind("chapter")%>'></asp:Label>,<asp:Label ID="Label2" runat="server" Text='<%# Bind("std_no")%>'></asp:Label>]
                    </span>
                    <span style="color:red">ME<asp:Label ID="Label3" runat="server" Text='<%# Bind("measure_element_no")%>' ></asp:Label></span>
                    ,<asp:Label ID="Label4" runat="server" Text='<%# Bind("measure_element_detail")%>'></asp:Label>
                   
                   <br />
                   <asp:Label ID="Label5" runat="server" ForeColor="DarkGray" Text='<%# Bind("criteria")%>'></asp:Label>
                    <br />
                    <strong>Comment : </strong>  
                    <br />
                    <asp:TextBox ID="txtcomment" runat="server" Width="90%" TextMode="MultiLine" Text='<%# Bind("me_comment_detail")%>' Rows="2"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Score">
              <ItemStyle Width="120px" />
                <ItemTemplate>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Value="10">Fully met</asp:ListItem>
                        <asp:ListItem Value="5">Partial met</asp:ListItem>
                        <asp:ListItem Value="0">Not met</asp:ListItem>
                        <asp:ListItem Value="-1">N/A</asp:ListItem>
                         <asp:ListItem Value="-2">None</asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Picture">
              
                <ItemTemplate>
                    <asp:Label ID="lblPicture" runat="server">-</asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
         <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
         <PagerSettings Position="TopAndBottom" />
         <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
         <RowStyle Height="50px" BackColor="White" ForeColor="#003399" />
         <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
    </asp:GridView>
            </td>
        </tr>
    
      
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="green" />
          &nbsp;</td>
        </tr>
    </table>
        </div>
            <div class="tabbertab">
    <h2>Email Result</h2>
<table width="100%" cellspacing="1" cellpadding="2" class="box-content">
  <tr>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="100" valign="top">
          <input type="button" name="button3" id="button1" value="Address" style="width: 85px;" onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms'), 'to')" />
        </td>
        <td valign="top"><input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" />
            <asp:Button ID="cmdSendMail" runat="server" Text=" Send " />&nbsp;<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
          </td>
      </tr>
       <tr>
                    <td valign="top" style="text-align:right" > 
                        <input id="btnCC" name="button9" 
               onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms'), 'cc')" 
                style="width: 85px;display:none" type="button" value="Cc..."  /></td>
                    <td valign="top"><input type="text" name="txtto0" id="txtcc" style="width: 635px;" 
                            runat="server" /></td></tr>
      <tr>
        <td valign="top">
          <input type="button" name="button10" id="button2" value="SMS" 
                style="width: 85px;display:none" 
                onclick="openPopupSMS()" /></td>
        <td valign="top">
            <asp:TextBox ID="txtsend_sms" runat="server" Width="635px" BackColor="#FFFF99" Visible="False"></asp:TextBox>
            <asp:CheckBox ID="chk_sms" runat="server" Text ="Send SMS" Visible="false" />
            </td>
      </tr>
      <tr>
        <td valign="top">Subject</td>
        <td valign="top">
            <asp:TextBox ID="txtsubject" runat="server" Width="635px">Internal Quality Assessment Report</asp:TextBox>
          
                                        <asp:CheckBox ID="chk_priority" runat="server" Text="High Priority" Font-Bold="True" ForeColor="Red" />

           </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;<asp:HiddenField ID="txtsend_idsms"
            runat="server" />
       <asp:HiddenField ID="txtidselect"
            runat="server" /> <asp:HiddenField ID="txtidCCselect"   runat="server" />
             <asp:HiddenField ID="txtidBCCSelect"   runat="server" />  <asp:HiddenField ID="txtidOther"   runat="server" />
          </td>
      </tr>
      </table></td>
  </tr>
</table>
                </div>
        </div>
</asp:Content>

