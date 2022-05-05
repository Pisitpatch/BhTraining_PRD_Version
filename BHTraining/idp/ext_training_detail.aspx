<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="ext_training_detail.aspx.vb" Inherits="idp_ext_training_detail" %>
<%@ Import Namespace="ShareFunction" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>
<link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
<script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

<script type="text/javascript">
    var global_time;

    $(document).ready(function () {
        $("#ctl00_ContentPlaceHolder1_txtrelate_idpno").autocomplete("../getIDPNo.ashx", { matchContains: false,
            autoFill: false,
            mustMatch: false
        });

        $('#ctl00_ContentPlaceHolder1_txtrelate_idpno').result(function (event, data, formatted) {
            // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
            var serial = data;
            alert("serial ::" + serial);

        });


        $("#ctl00_ContentPlaceHolder1_txtrelate_idpno").click(function () {
            $(this).select();
        });

        $("#ctl00_ContentPlaceHolder1_txtplace").autocomplete("../ajax_country.ashx", { matchContains: false,
            autoFill: false,
            mustMatch: false
        });

        $('#ctl00_ContentPlaceHolder1_txtplace').result(function (event, data, formatted) {
            // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
            var serial = data;
           // alert("serial ::" + serial);

        });


        $("#ctl00_ContentPlaceHolder1_txtplace").click(function () {
            $(this).select();
        });

        $("#ctl00_ContentPlaceHolder1_txtinstitution").autocomplete("../ajax_institution.ashx", { matchContains: false,
            autoFill: false,
            mustMatch: false
        });

        $('#ctl00_ContentPlaceHolder1_txtinstitution').result(function (event, data, formatted) {
            // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
            var serial = data;
            // alert("serial ::" + serial);

        });


        $("#ctl00_ContentPlaceHolder1_txtinstitution").click(function () {
            $(this).select();
        });


        $("#ctl00_ContentPlaceHolder1_txtsh_location").autocomplete("../ajax_sh_location.ashx", { matchContains: false,
            autoFill: false,
            mustMatch: false
        });

        $('#ctl00_ContentPlaceHolder1_txtsh_location').result(function (event, data, formatted) {
            // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
            var serial = data;
            // alert("serial ::" + serial);

        });


        $("#ctl00_ContentPlaceHolder1_txtsh_location").click(function () {
            $(this).select();
        });

        // =========== Check Session ===========================
        checkSession('<%response.write(session("bh_username").toString) %>', '<%response.write(viewtype) %>', '<%response.write(session("req").toString) %>'); // Check session every 1 sec.
    });

    function openPopupRegister(id) {
        loadPopup(1);
        my_window = window.open('popup_int_register.aspx?sh_id=' + id + "&id=<%response.write(id) %>", 'popupFile', 'alwaysRaised,scrollbars =yes,width=800,height=750');
    }
	
	
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
<script type="text/javascript">
    function editDetail(cid) {
        loadPopup(1);
        my_window = window.open('popup_editcomment.aspx?id=<%response.write(id) %>&cid=' + cid, 'popupFile', 'alwaysRaised,scrollbars =yes,status=yes,width=800,height=600');
    }

    function addComment() {
        /*
        if ($("#ctl00_ContentPlaceHolder1_txtcomment").val() == "") {
        alert("Please choose Comment Subject !");
        $("#ctl00_ContentPlaceHolder1_txtcomment").focus();
        return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txtcomment_detail").val() == "") {
        alert("Please enter Comment Detail !");
        $("#ctl00_ContentPlaceHolder1_txtcomment_detail").focus();
        return false;
        }
        */
        if (confirm("Are you sure you want to confirm?")) {
            if ($("#ctl00_ContentPlaceHolder1_txtacknowedge_status").val() == "") {
                alert("Please choose Acknowledge !");
                $("#ctl00_ContentPlaceHolder1_txtacknowedge_status").focus();
                return false;
            }
        } else {
            return false;
        }
    }
</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />

<script type="text/javascript">
    function validateExpense() {
        if ($("#ctl00_ContentPlaceHolder1_txtadd_expense_type").val() == "") {
            alert("กรุณาระบุประเภทค่าใช้จ่าย / Please choose expense type");
            $("#ctl00_ContentPlaceHolder1_txtadd_expense_type").focus();
            return false;
        }
    }

    function openDetail(id) {
        loadPopup(1);
        my_window = window.open('popup_training_file.aspx?id=<%response.write(id) %>&fid=' + id, 'popupFile', 'alwaysRaised,scrollbars =yes,width=800,height=750');
    }

    function checkDate(sender,args)
    {
        if ($("#ctl00_ContentPlaceHolder1_txtdate1").val() == ""){
            return false;
        }else{
           
        }

         var d = $("#ctl00_ContentPlaceHolder1_txtdate1").val().split("/");
         var date1 = new Date(d[2] + "/" + d[1] + "/" + d[0]);
       // alert(date1);
         if (sender._selectedDate < date1)
           {
                        alert("You cannot select a day !");
                        sender._selectedDate = date1; 
                        // set the date back to the current date
                        sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
    }

    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    function validateExternalForm() {
        if ($("#ctl00_ContentPlaceHolder1_txttitle").val() == "") {
            alert("กรุณาระบุหัวการฝึกอบรม / Please enter training title");
            $("#ctl00_ContentPlaceHolder1_txttitle").focus();
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_txttitle").val() == "") {
            alert("กรุณาระบุประเภทการเข้าร่วม / Please enter attendance type");
            $("#ctl00_ContentPlaceHolder1_txttype").focus();
            return false;
        }
		/*
        if ($("#ctl00_ContentPlaceHolder1_txtexect_detail").val() == "") {
            alert("กรุณาระบุประโยชน์ที่ได้รับ / Please enter Expected outcomes");
            $("#ctl00_ContentPlaceHolder1_txtexect_detail").focus();
            return false;
        }
		*/
        if ($("#ctl00_ContentPlaceHolder1_txtfacility").val() == "") {
            alert("กรุณาระบุสถานที่อบรม / Please enter Meeting venue");
            $("#ctl00_ContentPlaceHolder1_txtfacility").focus();
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_txtinstitution").val() == "") {
            alert("กรุณาระบุสถาบันที่จัดอบรม / Please enter Institute");
            $("#ctl00_ContentPlaceHolder1_txtinstitution").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txtplace").val() == "") {
            alert("กรุณาระบุจังหวัด/ประเทศ / Please enter Province/Country");
            $("#ctl00_ContentPlaceHolder1_txtplace").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txtdate1").val() == "") {
            alert("กรุณาระบุวันที่ฝีกอบรม /Please enter Training date");
            $("#ctl00_ContentPlaceHolder1_txtdate1").focus();
            return false;
        }
        if ($("#ctl00_ContentPlaceHolder1_txtdate2").val() == "") {
            alert("กรุณาระบุวันที่ฝึกอบรม / Please enter Training date");
            $("#ctl00_ContentPlaceHolder1_txtdate2").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txthour").val() == "") {
            alert("กรุณาระบุชั่วโมงทำงาน / Please enter Training hour");
            $("#ctl00_ContentPlaceHolder1_txthour").focus();

            return false;

         
        }
        <%if req = "ext" then %>
           try{
                hour1 = parseFloat($("#ctl00_ContentPlaceHolder1_txthour").val() );
                if (!isNumber(hour1)){
                 alert("กรุณาระบุชั่วโมงทำงานเป็นตัวเลขเท่านั้น");
                $("#ctl00_ContentPlaceHolder1_txthour").focus();
                return false;
                }
               //  return false;
             
            }catch(e){
               alert("กรุณาระบุชั่วโมงทำงานเป็นตัวเลขเท่านั้น");
                $("#ctl00_ContentPlaceHolder1_txthour").focus();
                return false;
            }
        <%end if %>

        if ($("#ctl00_ContentPlaceHolder1_txttraintype").val() == "") {
            alert("กรุณาระบุประเภทการอบรม / Please enter Training type");
            $("#ctl00_ContentPlaceHolder1_txttraintype").focus();
            return false;
        }
       // alert($("#ctl00_ContentPlaceHolder1_txtbudgetRequest").val());
         <%If GridExpense.Rows.Count = 0 Then%>
            if ( $("#ctl00_ContentPlaceHolder1_txtbudgetRequest_0").is(':checked') ){
              alert("กรุณาระบุค่าใช้จ่ายหน้าส่วนของ Estimate budget / Please fill your Estimate budget ");
              return false;
            }else if( $("#ctl00_ContentPlaceHolder1_txtbudgetRequest_1").is(':checked') ){

            }
         <%end if %>

        if (confirm('Are you sure you want to submit ?')) {

        } else {
            return false;
        }
    } // end function

    function openPopup(flag, popupType) {
        if (flag.checked) {
            is_sms = 1;
        } else {
            is_sms = 0;
        }
        loadPopup(1);
        my_window = window.open('../incident/popup_recepient.aspx?modules=idp&popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=yes,width=800,height=600');
    }

    function openPopupSMS() {
        loadPopup(1);
        my_window = window.open('../incident/popup_sms.aspx', '', 'alwaysRaised,scrollbars =no,status=yes,width=800,height=600');
    }
</script>

    <style type="text/css">
        .style2
        {
            height: 29px;
        }
        </style>

    <style type="text/css">
    @import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

     </style>

<script type="text/javascript" src="../js/datepicker2/jquery.calendars.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.plus.js"></script>


<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.ext.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.thai.js"></script>

<script type="text/javascript">
    var global_time;
    var c = new Date();
    var date_str = c.getDate() + "/" + (c.getMonth() + 1) + "/" + c.getFullYear();
    // alert(date_str);
    $(function() {
        //	$.datepick.setDefaults({useThemeRoller: true,autoSize:true});
        var calendar = $.calendars.instance();

        $('#ctl00_ContentPlaceHolder1_txtdate11').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_txtdate2').calendarsPicker(
			{
			   // maxDate: date_str,
			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_txtdate_assessment').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_ctl03_txtdate_exam').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
		);

       

        $('#ctl00_ContentPlaceHolder1_txtpsm_date_expect').calendarsPicker(
		{
		    showTrigger: '#calImg',
		    dateFormat: 'dd/mm/yyyy'
		}
	    );
        //	$('#inlineDatepicker').datepick({onSelect: showDate});
    });


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
  <div style="display: none;"> <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer"  /> </div>
 <asp:Panel ID="PanelMain" runat="server" DefaultButton="cmdSaveDraft1">
    <div id="header">
        <table width="100%" cellpadding="0" cellspacing="0">
    <tr>
              <td><div id="headerIDP"><img src="../images/menu_03.png" width="32" height="32" align="absmiddle"  /><asp:Label
                      ID="lblHeader" runat="server" Text="Label"></asp:Label></div></td>
              <td><div align="right">
               <asp:Button ID="cmdThai" runat="server" Text="Thai" CausesValidation="False" />
      <asp:Button ID="cmdEng" runat="server" Text="Eng" CausesValidation="False" />

               Status
                  <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="status_name" DataValueField="idp_status_id" Font-Bold="True" 
                    BackColor="Aqua" ForeColor="Blue" Width="285px"> </asp:DropDownList>
           &nbsp;
                        <asp:Button ID="cmdUpdateStatus" runat="server" Text="Update Status" />
              </div></td>
            </tr>
          </table>
          <br />
<div align="right">
          &nbsp;&nbsp;&nbsp;
&nbsp;
    <asp:Button ID="cmdPreview" runat="server" Text="Print Preview" Width="150px" />&nbsp;
   <asp:Button ID="cmdSaveDraft1" runat="server" Text="Save as Draft" Width="150px" OnCommand="onSave" 
            CommandArgument="1" />
           
            &nbsp;
             <asp:Button ID="cmdSubmit1" runat="server" Text="Submit" Width="150px" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True" OnClientClick="return  validateExternalForm(); "  />

              <asp:Button ID="cmdSaveGoal" runat="server" Text="Save Action after training"   Font-Bold="true"  />
  &nbsp;&nbsp;
        </div>
      </div>
<div id="data">
          <div class="tabber" id="mytabber2">
            <div class="tabbertab">
              <h2>Staff Information</h2>
              <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td valign="top"><span class="theader"><strong>Training No.</strong></span></td>
                  <td><strong><asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label>
                    
                      </strong>
                        <asp:Label ID="lblViewtype" runat="server" Text="Label" Visible="False"></asp:Label>
                      </td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="100"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="100"><strong>Cost Center</strong></td>
                        <td width="230"><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                        <td width="80">&nbsp;</td>
                        <td></td>
                      </tr>
                  </table></td>
                    <% If req = "ext" Then%>
                </tr>
                    <tr>
                  <td valign="top" colspan="2" style="background-color:#EEEEEE;color:#0000ff"><strong>Year <asp:Label ID="lblBudgetYear" runat="server" Font-Bold="True" ></asp:Label> Department External Training Budget</strong>&nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top"><strong>Budget Allocated</strong></td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td width="180">
                                      <asp:Label ID="lblBudgetApprove" runat="server" Text=""></asp:Label>
                                      THB</td>
                                  <td width="100"><strong>Requested</strong></td>
                                  <td width="230">
                                      <asp:Label ID="lblBudgetRequest" runat="server" Text=""></asp:Label>
                                      THB</td>
                                  <td width="80"><strong>Balance</strong></td>
                                  <td>
                                      <asp:Label ID="lblBudgetBalance" runat="server" Font-Bold="True"></asp:Label>
                                      THB </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
                  <% end If %>
              </table>
            </div>
      <div class="tabbertab" id="email_alert" runat="server" visible="false">
        <h2>Email Alert</h2>
        <table width="100%" cellspacing="1" cellpadding="2">
  <tr>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="100" valign="top">
          <input type="button" name="button3" id="button2" value="Address" style="width: 85px;" onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') ,  'to')" />
        </td>
        <td valign="top"><input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" readonly="readonly" />
            <asp:Button ID="cmdSendMail" runat="server" Text=" Send " />&nbsp;<asp:CheckBox 
                ID="chkHigh" runat="server" ForeColor="#FF3300" Text="High Priority" />
          </td>
      </tr>
       <tr>
                    <td valign="top"> 
                        <input id="btnCC" name="button9" 
               onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') , 'cc')" 
                style="width: 85px;display:none" type="button" value="Cc..."  />
                        CC :</td>
                    <td valign="top"><input type="text" name="txtto0" id="txtcc" style="width: 635px;" 
                            runat="server" readonly="readonly" /></td></tr>
      <tr>
        <td valign="top">
          <input type="button" name="button10" id="button4" value="SMS" 
                style="width: 85px;" 
                onclick="openPopupSMS()" /></td>
        <td valign="top">
            <asp:TextBox ID="txtsend_sms" runat="server" Width="635px" BackColor="#FFFF99"></asp:TextBox>
            <asp:CheckBox ID="chk_sms" runat="server" />
            Send SMS</td>
      </tr>
      <tr>
        <td valign="top">Subject</td>
        <td valign="top">
            <asp:DropDownList ID="txtsubject" runat="server" Width="641px" 
                AutoPostBack="True">
        
            <asp:ListItem Value="4">Review external training request</asp:ListItem>
             <asp:ListItem Value="5">Review internal training request</asp:ListItem>
           
            <asp:ListItem Value="102">Need Accounting review budget</asp:ListItem>
             <asp:ListItem Value="103">Review external training expense</asp:ListItem>
              
            </asp:DropDownList>
          
           </td>
      </tr>
      <tr>
        <td valign="top">Message</td>
        <td valign="top"><textarea name="txtmessage" id="txtmessage" cols="45" rows="5" style="width: 635px;" runat="server"></textarea>
      <asp:HiddenField ID="txtsend_idsms"
            runat="server" />
       <asp:HiddenField ID="txtidselect"
            runat="server" /> <asp:HiddenField ID="txtidCCselect"   runat="server" />
             <asp:HiddenField ID="txtidBCCSelect"   runat="server" />  <asp:HiddenField ID="txtidOther"   runat="server" />
          </td>
      </tr>
      </table></td>
  </tr>
</table></div>
       <div class="tabbertab" id="tabMailLog" runat="server" visible="false">
        <h2>IDP Alert log</h2>
        <asp:GridView ID="GridAlertLog" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" Visible="true" 
            Width="780px" DataKeyNames="log_alert_id" CellSpacing="1">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="alert_date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="alert_method" HeaderText="Method" />
            <asp:BoundField DataField="subject" HeaderText="Subject" />
            <asp:BoundField DataField="send_to" HeaderText="Recepient" />
             <asp:BoundField DataField="cc_to" HeaderText="CC" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
        <br /></div>
          <div class="tabbertab" id="idp_log" runat="server">
              <h2>Review log</h2>
                <asp:GridView ID="GridviewIDPLog" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="log_status_id" 
        CellSpacing="1" CellPadding="2" AllowPaging="True" BorderWidth="0px" 
        BorderStyle="None" ShowFooter="True">
               <Columns>
                   <asp:BoundField DataField="idp_status_name" HeaderText="Action" >
                   <ItemStyle BackColor="#F8F8F8" />
                   </asp:BoundField>
            <asp:TemplateField HeaderText="By">
                <ItemTemplate>
                    
                     <asp:TextBox ID="txtLogReportby" ReadOnly="true" runat="server" Text='<%# Bind("log_create_by") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position">
                <ItemTemplate>
                    
                      <asp:TextBox ID="txtLogPosition" ReadOnly="true" runat="server" Text='<%# Bind("position") %>'></asp:TextBox>
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit/Dept">
                <ItemTemplate>
                   
                      <asp:TextBox ID="txtLogDept" ReadOnly="true" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    
                      <asp:TextBox ID="txtLogDate" ReadOnly="true" runat="server" Text='<%# Bind("log_time","{0:dd/MM/yyyy hh:mm}") %>'  ></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:BoundField HeaderText="TAT" />
        </Columns>
                    <HeaderStyle BackColor="#EEF1F3" />
    </asp:GridView>
          
            <br />
            </div>
        </div>
        <br />
        <div class="tabber" id="tab_ext_training11">
          <div class="tabbertab" id="tab_ext_training"  runat="server">
            <h2>
            External Training 
            </h2>
            <asp:Panel ID="panel_detail" runat="server">
            <table width="100%" cellspacing="0" cellpadding="5">
              <tr>
                <td valign="top" bgcolor="#DBE1E6">
                <asp:Label ID="lblIDPTitle" runat="server" Text="หัวข้อที่ต้องการฝึกอบรม " Font-Bold="true" />
                </td>
                <td valign="top" bgcolor="#DBE1E6"><input type="text" name="txttitle" id="txttitle" style="width: 735px" runat="server" /></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                <asp:Label ID="lblIDPJoin" runat="server" Text="การเข้าร่วม " Font-Bold="true" />
               </td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:DropDownList ID="txttype" runat="server">
                    <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                    <asp:ListItem Value="1">ผู้เข้าอบรม / Trainee</asp:ListItem>
                    <asp:ListItem Value="2">วิทยากร / Speaker</asp:ListItem>
                    </asp:DropDownList>
               </td>
              </tr>
              <!--
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                  <asp:Label ID="lblIDPMotivation" runat="server" Text="แรงจูงใจในการเข้าอบรมหัวข้อนี้ " Font-Bold="true" />
               </td>
                <td valign="top" bgcolor="#eef1f3"><p>
                   
                    <input type="checkbox" name="checkbox3" id="chk_attend1" runat="server" />
                    <asp:Label ID="lblIDPMotivate1" runat="server" Text=" หัวข้อนี้เป็นหัวข้อที่ระบุอยู่ในแผนพัฒนาตนเองของฉัน"  />
                  <strong>IDP No. </strong>
                    <asp:DropDownList ID="txtrelate_idpno" runat="server" DataTextField="idp_no" 
                        DataValueField="idp_no">
                    </asp:DropDownList>
                  
                    <asp:Button ID="Button1" runat="server" Text=".." Visible="false" />
                                   <br />
                  <input type="checkbox" name="checkbox4" id="chk_attend2" runat="server" />
                   <asp:Label ID="lblIDPMotivate2" runat="server" Text="เพื่อให้ตนเองสามารถปรับระดับ Career Ladder "  />
                 
                    <asp:DropDownList ID="txtladder1" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
           <asp:Label ID="Label8" runat="server" Text="เป็นระดับ "  />        

  <asp:DropDownList ID="txtladder2" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
<br />
                  <input type="checkbox" name="checkbox5" id="chk_attend3" runat="server" />
                  <asp:Label ID="lblIDPMotivate3" runat="server" Text="การพัฒนาในหัวข้อนี้ ฉันได้รับคำแนะนำจาก หัวหน้างานของฉัน"  />
                  <br />
                  <input type="checkbox" name="checkbox6" id="chk_attend4" runat="server" />
                   <asp:Label ID="lblIDPMotivate4" runat="server" Text="การพัฒนาในหัวข้อนี้ ฉันได้จาการประเมินความสามารถของตัวฉันเอง"  />
                 <br />
                  <input type="checkbox" name="checkbox7" id="chk_attend5" runat="server" />
                  <asp:Label ID="lblIDPMotivate5" runat="server" Text="ฉันต้องการเตรียมพร้อมตัวเองเพื่ออนาคตในหน้าที่การงานของตัวเองด้วยหัวข้อนี้"  />
                  <br />
                  <input type="checkbox" name="checkbox8" id="chk_attend6" runat="server" />
                   <asp:Label ID="lblIDPMotivate6" runat="server" Text=" องค์กรของฉันเล็งเห็นว่าหัวข้อนี้เป็นเรื่องที่สำคัญ"  />
                 <br />
                  <input type="checkbox" name="checkbox9" id="chk_attend7" runat="server" />
                   <asp:Label ID="lblIDPMotivate7" runat="server" Text="อื่นๆ"  />
                   <span style="font-weight: bold">
                    <input type="text" name="textfield" id="txtattend_remark" style="width: 350px" runat="server" />
                  </span></p></td>
              </tr>
              -->
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:Label ID="lblCategory" runat="server" Font-Bold="True" 
                        Text="หมวดหมู่การอบรม" />
               </td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:DropDownList ID="txtadd_extcat" runat="server" 
                        DataValueField="category_id" Width="300px">
                    </asp:DropDownList>
                  </td>
              </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblIDPCourse" runat="server" Font-Bold="true" Text="หลักสูตร " />
                    </td>
                    <td bgcolor="#eef1f3" valign="top">
                        <strong>
                        <textarea ID="txtcourse_detail" runat="server" cols="45" name="textarea3" 
                            rows="3" style="width: 735px"></textarea>
                        <br />
                        </strong>
                    </td>
                </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                 <asp:Label ID="lblIDPFile" runat="server" Text="ไฟล์แนบ" Font-Bold="true" />
                </td>
                <td valign="top" bgcolor="#eef1f3"><table>
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top">
              <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload1" runat="server"  />
                      <asp:Button ID="cmdUpload"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdDeleteFile" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFile" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView>
                <br />
                
                <asp:Label ID="lblAttach1" runat="server" Text="** ในกรณีที่แนบไฟล์ไม่ได้กรุณาส่งเอกสารให้ฝ่ายเรียนรู้และพัฒนา" ForeColor="Red" />
                </td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                <asp:Label ID="lblIDPExpect" runat="server" Text="การพัฒนานี้มีประโยชน์ต่อเป้าหมายส่วนบุคคลหรือของแผนกด้านใด (ตอบได้ > 1 ข้อ)
" Font-Bold="true" />
               </td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:CheckBox runat="server" Text="อบรมตามแผนการปรับระดับความก้าวหน้าจากระดับ " 
                        ID="chk_expect1" />
                    <asp:DropDownList ID="txtexpect_level1" runat="server">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblexpect_level1_1" runat="server" Text="เป็นระดับ"></asp:Label>
                    <asp:DropDownList ID="txtexpect_level2" runat="server">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                    <br/>
                    <asp:CheckBox runat="server" Text="เพื่อให้ผลประเมินการทำงานส่วนบุคคลดีขึ้น" ID="chk_expect2" /><br/>
                    <asp:CheckBox runat="server" Text="เกิดความรู้และทักษะใหม่มาใช้ปฏิบัติงานเพื่อสนับสนุนเป้าหมายของแผนกในด้าน " ID="chk_expect3" />
                    <asp:DropDownList runat="server" id="txtnewgoal" DataTextField="expect_detail" DataValueField="expect_id">
                    </asp:DropDownList>
                    <br/>
                    <asp:CheckBox runat="server" Text="สร้างชื่อเสียงให้โรงพยาบาลโดยการเผยแพร่ผลงาน / งานวิจัย  " ID="chk_expect4" /><br/>
                    <asp:CheckBox runat="server" Text="ทำให้เกิดบริการหรืองานใหม่ตามเป้าหมายของหน่วยงานหรือองค์กร  " ID="chk_expect5" /><br/>
                    <asp:CheckBox runat="server" Text="อื่นๆ " ID="chk_expect6" />
                    <asp:TextBox ID="txtexpect_goal_other" runat="server" Width="350px"></asp:TextBox>
                    <br/>
                    <asp:DropDownList ID="txtexpect_detail" runat="server" Visible="false">
                    </asp:DropDownList>
                    <br /> <asp:Label ID="lblExpectLabel" runat="server" Text="" Visible="false"  />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                 <asp:Label ID="lblIDPTrainType" runat="server" Text="ประเภทการอบรม " Font-Bold="true" />
               </td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335">
                          <asp:DropDownList ID="txttraintype" runat="server">
                          <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                          <asp:ListItem Value="1">การประชุม / Meeting</asp:ListItem>
                          <asp:ListItem Value="2">ฝีกอบรม / Training</asp:ListItem>
                          <asp:ListItem Value="3">การสัมมนา / Seminar</asp:ListItem>
                          <asp:ListItem Value="4">การประชุมเชิงปฎิบัติการ / Workshop</asp:ListItem>
                        
                          </asp:DropDownList>
                      </td>
                      <td width="80">&nbsp;</td>
                      <td>&nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td width="150" valign="top" bgcolor="#eef1f3">
                  <asp:Label ID="lblIDPFacility" runat="server" Text="สถานที่อบรม" Font-Bold="true" />
                </td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield18" id="txtfacility" style="width: 535px" runat="server" />
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                <asp:Label ID="lblIDPInstitute" runat="server" Text="สถาบันที่จัดอบรม " Font-Bold="true" />
                </td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="txtinstitution" id="txtinstitution" style="width: 535px" runat="server" />
                    <asp:Image ID="Image8" runat="server" ImageUrl="~/images/spellcheck.png" />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                <asp:Label ID="lblIDPPlace" runat="server" Text="จังหวัด/ประเทศ " Font-Bold="true" />
                </td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:TextBox ID="txtplace" runat="server" Width="535px"></asp:TextBox>
                    <asp:Image ID="Image7" runat="server" ImageUrl="~/images/spellcheck.png" />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><asp:Label ID="lblIDPDate" runat="server" Text="วันที่อบรม " Font-Bold="true" /></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                    <tr>
                      <td width="300"> <asp:TextBox ID="txtdate11" runat="server" BackColor="Lime" Width="100px" ></asp:TextBox>
                              
                           to
                       
                       <asp:TextBox ID="txtdate2" runat="server" BackColor="Lime" Width="100px"  ></asp:TextBox>
                           
                             
                       </td>
                      <td width="40">Time</td>
                      <td>
                       <asp:DropDownList ID="txthour1" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin1" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
                        to
                        <asp:DropDownList ID="txthour2" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin2" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><asp:Label ID="lblIDPTrainHour" runat="server" 
                        Text="ชั่วโมงการอบรม" Font-Bold="True" /></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td >
                          <input type="text" name="txthour" id="txthour" style="width: 100px; text-align:right" runat="server" maxlength="5" />
                          &nbsp;&nbsp;hrs. (ใส่จำนวนชั่วโมงเป็นตัวเลขเท่านั้น / Number Only)</td>
                    </tr>
                </table></td>
              </tr>
              

                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblIDPBudget" runat="server" Font-Bold="true" 
                            Text="การขอเบิกค่าใช้จ่าย " />
                    </td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:RadioButtonList ID="txtbudgetRequest" runat="server">
                            <asp:ListItem Value="1">ต้องการเบิกค่าใช้จ่ายจากโรงพยาบาล</asp:ListItem>
                            <asp:ListItem Value="2">ไม่ต้องการเบิกค่าใช้จ่ายจากโรงพยาบาล</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
              

              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3">
                <strong><asp:Label ID="lblIDPResvTopic" runat="server" Text="การสมัครอบรม" /></strong>
                <table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                  <tr>
                    <td width="25"><asp:RadioButton ID="txtr1" runat="server" 
                            GroupName="register"  />
                      </td>
                    <td><asp:Label ID="lblIDPResv1" runat="server" Text="ดำเนินการสมัครด้วยตนเองแล้ว" /></td>
                  </tr>
                  <tr>
                    <td height="26">
                        <asp:RadioButton ID="txtr2" runat="server" GroupName="register" />
                      </td>
                    <td> <asp:Label ID="lblIDPResv2" runat="server" Text="ดำเนินการสมัครด้วยตนเองแล้ว ต้องการเบิกเงินเพื่อนำไปชำระ ณ วันลงทะเบียน"></asp:Label>
                 
                        &nbsp;       <asp:TextBox ID="txtdate_register2" runat="server" BackColor="Lime" Width="100px" Visible="false"></asp:TextBox>
                            
                          </td>
                  </tr>
                   
                  <tr style="display:none">
                    <td>
                        <asp:RadioButton ID="txtr3" runat="server" GroupName="register" Visible="False" />
                      </td>
                    <td><asp:Label ID="lblIDPResv3" runat="server" Text="ต้องการให้ฝ่ายการเรียนรู้และพัฒนาสำรองที่นั่งให้ภายในวันที่" />   &nbsp;&nbsp;
                        <asp:TextBox ID="txtdate_register" runat="server" BackColor="Lime" Width="100px" Visible="False"></asp:TextBox>
                            
                        <br /><span style="color:red;display:none">หมายเหตุ:ทางฝ่ายการเรียนรู้และพัฒนาจะสามารถดำเนินการตามที่ขอได้เมื่อการอนุมัติครบทุกระดับ</span>

                    </td>
                  </tr>
                </table></td>
              </tr>
                 <tr>
                <td valign="top" bgcolor="#eef1f3"><asp:Label ID="lblcontact" runat="server" Text="เบอร์โทรศัพท์ของผู้ไปอบรม" Font-Bold="true" /></td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:TextBox ID="txtcontact" runat="server" Width="300px"></asp:TextBox></td>
              </tr>
            </table>
            </asp:Panel>
          </div>
           
           <div class="tabbertab" id="tab_int_training" runat="server">
            <h2>
           Internal Training 
            </h2>
            <asp:Panel ID="panel_internal_detail" runat = "server">
            <table width="100%" cellspacing="1" cellpadding="2" >
              <tr>
                <td width="200" valign="top" bgcolor="#eef1f3"><strong>Training Course Topic<br />
                </strong></td>
                <td valign="top"><input type="text" name="txtint_topic" id="txtint_topic" style="width: 735px" runat="server" />
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Development Program Ref. No.</strong></td>
                <td valign="top">
                    <asp:DropDownList ID="txtidp_relate" runat="server" 
                        DataTextField="template_title" DataValueField="template_id">
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="cmdUpdateIDPRef" runat="server" Text="Apply this IDP Program" />
                  </td>
              </tr>
             
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><label></label>
                  <label>
                  <strong>การฝึกอบรมจัดขึ้น เพื่อรองรับแผนพัฒนาบุคคล IDP ในด้าน</strong></label></td>
                </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <asp:GridView ID="GridFunction" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                            EmptyDataText="There is no item.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("relate_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="25px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                               <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="R/E">
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblRequire" runat="server" Text='<%# Bind("is_required") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="category_name" HeaderText="Categories" >
                      <ItemStyle Width="120px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="Development Topics">
                         <ItemStyle Width="250px" />
                          <ItemTemplate>
                              <asp:Label ID="txtcat_id" runat="server" Text='<%# Bind("category_id") %>' Visible="false"></asp:Label>
                           
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("topic_name") %>'></asp:Label><br />
                                <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("template_title") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Expected outcomes" DataField="expect_detail" 
                          ItemStyle-Width="180px"  >
                      <ItemStyle Width="180px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="Methodology">
                          <ItemTemplate>
                            
                              <asp:Label ID="lblMethod" runat="server" Text='<%# Bind("methodology") %>' Visible="true"></asp:Label>
                             
                          </ItemTemplate>
                       
                          <ItemStyle Width="120px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Measure">
                          <ItemTemplate>
                              <asp:Textbox ID="lblMeasure" runat="server" Text='<%# Bind("measure") %>' Visible="true" Rows="3" TextMode="MultiLine"></asp:Textbox>
                          </ItemTemplate>
                         <ItemStyle Width="180px" />
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Outcome">
                          <ItemTemplate>
                          <asp:Textbox ID="lblOutcome" runat="server" Text='<%# Bind("outcome") %>' Visible="true" Rows="3" TextMode="MultiLine"></asp:Textbox>
                          </ItemTemplate>
                           <ItemStyle Width="180px" />
                        
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Remark">
                         
                          <ItemTemplate>
                             <asp:Label ID="lblremark" runat="server" Text='<%# Bind("remark") %>' Visible="true"></asp:Label>
                              <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("topic_status") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblStatusId" runat="server" Text='<%# Bind("topic_status_id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                     
                  </Columns>
              </asp:GridView>
                   <table width="100%"  class="tdata" cellpadding="3">  
                    
              <tr>
                <td valign="top" width="25" >&nbsp;</td>
                <td valign="top" width="180" >Categtory</td>
                <td valign="top" width="250" >
                    <asp:DropDownList ID="txtadd_cat1" runat="server" AutoPostBack="True" 
                        BackColor="#FFFF99" DataValueField="category_id"  Width="235px">
                    </asp:DropDownList>
                      
                   </td>
                <td valign="top" >&nbsp;
                     </td>
              </tr>
          
                       <tr>
                           <td  valign="top" width="25">
                           </td>
                           <td  valign="top" >
                               Development Topic</td>
                           <td  valign="top" width="250">
                               <asp:DropDownList ID="txtfind_topic" runat="server" AutoPostBack="True" 
                                   DataValueField="topic_id" Width="235px">
                               </asp:DropDownList>
                               <br />
                               <asp:TextBox ID="txtadd_topic1" runat="server" BackColor="#FFFF99" Rows="2" 
                                   TextMode="MultiLine" Visible="False" Width="235px"></asp:TextBox>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
                       <tr>
                           <td  valign="top" width="25">&nbsp;
                               </td>
                           <td  valign="top" >
                               Expect outcome</td>
                           <td  valign="top" width="250">
                               <asp:DropDownList ID="txtfind_expect" runat="server" AutoPostBack="True" 
                                   DataValueField="expect_id">
                               </asp:DropDownList>
                               <br />
                               <asp:TextBox ID="txtadd_expect1" runat="server" BackColor="#FFFF99" Rows="2" 
                                   TextMode="MultiLine" Visible="False" Width="235px"></asp:TextBox>
                               <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                                   CompletionSetCount="5" DelimiterCharacters="" Enabled="True" 
                                   EnableViewState="false" MinimumPrefixLength="1" ServiceMethod="getExpect" 
                                   ServicePath="~/IDP_TopicService.asmx" TargetControlID="txtadd_expect1">
                               </asp:AutoCompleteExtender>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
                       <tr>
                           <td  valign="top" width="25">&nbsp;
                               </td>
                           <td  valign="top" >Methodology
                               &nbsp;</td>
                           <td  valign="top" width="250">
                               <asp:DropDownList ID="txtadd_method1" runat="server" BackColor="#FFFF99" 
                                   DataTextField="method_name" DataValueField="method_id" Width="235px">
                               </asp:DropDownList>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
          
                       <tr>
                           <td  valign="top" width="25">&nbsp;
                               </td>
                           <td  valign="top" >
                               Measure</td>
                           <td  valign="top" width="250">
                               <asp:TextBox ID="txtadd_measure" runat="server" Width="235px" Rows="3" 
                                   TextMode="MultiLine"></asp:TextBox>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
                       <tr>
                           <td  valign="top" width="25">&nbsp;
                               </td>
                           <td  valign="top" >
                               Outcome</td>
                           <td  valign="top" width="250">
                               <asp:TextBox ID="txtadd_outcome" runat="server" Width="235px" Rows="3" 
                                   TextMode="MultiLine"></asp:TextBox>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
                       <tr>
                           <td  valign="top" width="25">&nbsp;
                               </td>
                           <td  valign="top" >
                               Remark</td>
                           <td  valign="top" width="250">
                               <asp:TextBox ID="txtadd_remark" runat="server" Width="235px"></asp:TextBox>
                           </td>
                           <td  valign="top">&nbsp;
                               </td>
                       </tr>
          
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"> <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/IDP_TopicService.asmx" ServiceMethod="getTopic" CompletionSetCount="5" 
            TargetControlID="txtadd_topic1"   >
        </asp:AutoCompleteExtender>&nbsp;<asp:Button ID="cmdAddTopic" runat="server" 
                        Font-Bold="True" Text=" Add " Width="100px" />
                    <asp:Button ID="cmdDelete" runat="server" ForeColor="Red" Text="Delete" Width="100px" OnClientClick="return confirm('Are you sure you want to delete?');" />
                  </td>
                <td valign="top" style="text-align:center">
                
                 &nbsp;&nbsp;
                                       
                   
                  </td>
              </tr>
                       <% 'end if %>
            </table>
            </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                  <asp:Label ID="lblIDPTopic2" runat="server" Text="กลุ่มเป้าหมายผู้เข้าอบรม" Font-Bold="true"></asp:Label>
                </td>
                <td >
                    &nbsp;<asp:FileUpload ID="FileUploadCSV" runat="server" Width="200px" />
                                    &nbsp;<asp:Button ID="cmdUploadCSV" runat="server" Text="Upload Employee List" /><br />
                                    <a href="employee.csv" target="_blank">ไฟล์ตัวอย่าง</a>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td valign="top">
                   
                      <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top" style="background-color:lightblue">
                              <asp:RadioButton ID="txttarget1" runat="server" GroupName="target" />
                              <strong>Employee List</strong></td>
                          <td style="background-color:lightblue"><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td>
                                   
                                  </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                  <td colspan="3">
                                      <table width="100%">
                                          <tr>
                                              <td width="100">
                                                  Department </td>
                                              <td>
                                                  <asp:DropDownList ID="txtint_department" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td width="120">
                                                  Job Type</td>
                                              <td>
                                                  <asp:DropDownList ID="txtint_jobtype" runat="server"  DataTextField="job_type" 
                                DataValueField="job_type">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td width="120">
                                                  Job Title</td>
                                              <td>
                                                  <asp:DropDownList ID="txtint_jobtitle" runat="server"  DataTextField="job_title" 
                                DataValueField="job_title">
                                                  </asp:DropDownList>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td>
                                                  Employe name</td>
                                              <td>
                                                  <asp:TextBox ID="txtfindemployee" runat="server" Width="100px"></asp:TextBox>
                                                  <asp:Button ID="cmdSearch" runat="server" Text="search" />
                                              </td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                              <tr>
                                <td width="180">
                            <asp:ListBox ID="lblDivisionAll" runat="server" Width="250px" 
                                DataTextField="user_fullname" DataValueField="emp_code" 
                                SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                                <td width="60"><asp:Button ID="cmdSelect" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemove" runat="server" Text="<" CausesValidation="False" />
                                    <br />
                                    </td>
                                <td width="230">
                            <asp:ListBox ID="lblDivisionSelect" runat="server" Width="250px"   
                                DataTextField="user_fullname" DataValueField="emp_code" 
                                SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                              </tr>
                          </table></td>
                        </tr>
                          <tr>
                              <td style="background-color:lightblue" valign="top" width="150">&nbsp;
                                  </td>
                              <td style="background-color:lightblue">&nbsp;
                                  </td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top">
                          <asp:RadioButton ID="txttarget2" runat="server" GroupName="target" />
                          <strong>Cost Center List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td width="180">
                            <asp:ListBox ID="lblCCAll" runat="server"  DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                                <td width="60"> 
                            <asp:Button ID="cmdCCAdd" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="lblCCRemove" runat="server" Text="<" CausesValidation="False" />&nbsp;<br />
                                    </td>
                                <td width="230">
                            <asp:ListBox ID="lblCCSelect" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                      </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top">
                          <asp:RadioButton ID="txttarget3" runat="server" GroupName="target" />
                          <strong>Job Type List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td>&nbsp;<asp:TextBox ID="txtfind_jobtype" runat="server" Width="200px"></asp:TextBox>
                        
                            <asp:Button ID="cmdSearchJobType" runat="server" Text="search" />
                                  </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="180">
                            <asp:ListBox ID="lblJobTypeAll" runat="server" DataTextField="job_type" 
                                DataValueField="job_type" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                                <td width="60"> 
                            <asp:Button ID="cmdAddType" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemoveType" runat="server" Text="<" CausesValidation="False" />&nbsp;<br />
                                    </td>
                                <td width="230">
                            <asp:ListBox ID="lblJobTypeSelect" runat="server" DataTextField="job_type" 
                                DataValueField="job_type" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                      </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top">
                          <asp:RadioButton ID="txttarget4" runat="server" GroupName="target" />
                          <strong>Job Title List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td>
                            <asp:TextBox ID="txtfind_jobtitle" runat="server" Width="200px"></asp:TextBox>
                            <asp:Button ID="cmdSearchJobTitle" runat="server" Text="search" />
                                  </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="180">
                            <asp:ListBox ID="lblJobTitleAll" runat="server" DataTextField="job_title" 
                                DataValueField="job_title" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                                <td width="60"> 
                            <asp:Button ID="cmdAddTitle" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemoveTitle" runat="server" Text="<" CausesValidation="False" />&nbsp;<br />
                                    </td>
                                <td width="230">
                            <asp:ListBox ID="lblJobTitleSelect" runat="server" DataTextField="job_title" 
                                DataValueField="job_title" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                                  </td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                    </tr>
                </table>
                </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:Label ID="lblIDPCourse2" runat="server" Text="หลักสูตร" Font-Bold="true"></asp:Label></td>
                <td valign="top"><textarea name="txtint_outline" id="txtint_outline" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">
                 <asp:Label ID="lblIDPFile2" runat="server" Text="ไฟล์แนบ" Font-Bold="true"></asp:Label>
              </td>
                <td valign="top">
                  <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload4" runat="server"  />
                      <asp:Button ID="cmdFileInternal"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="Button5" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFileInternal" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView>
                </td>
              </tr>
            </table>
            </asp:Panel>
            </div>
             <div class="tabbertab" id="tab_int_document" runat="server">
            <h2>
                Training Materials
            </h2>
             <asp:GridView ID="GridTrainingDoc" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="trainging_file_id" HeaderStyle-CssClass="colname" 
                EnableModelValidation="True">
           <Columns>
               <asp:TemplateField>
               <ItemStyle Width="30px" />
                   <ItemTemplate>
                       <asp:CheckBox ID="chkSelect" runat="server" />
                   </ItemTemplate>
               </asp:TemplateField>
                           <asp:TemplateField HeaderText="File name">
                   <ItemTemplate>
                    <asp:Label ID="lblPk" runat="server" Text='<%# bind("trainging_file_id") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                        <a href="../share/idp/hr/<%# Eval("file_path") %>">
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                       </a>
                   </ItemTemplate>
                 
                   <ItemStyle Width="450px" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Description">
                   <ItemTemplate>
                  
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("file_remark") %>'></asp:Label>
                      
                   </ItemTemplate>
                  
                   <ItemStyle Width="450px" />
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
        <table width="98%" cellpadding="3" cellspacing="0" class="tdata">
            <%If GridTrainingDoc.Rows.Count = 0 Then%>
            <tr>
              <td width="30" class="colname">&nbsp;</td>
            
              <td class="colname"><strong>File name</strong></td>
              <td class="colname"><strong>Description</strong></td>
            </tr>
            <%end if %>
            <tr>
              <td valign="top">&nbsp;</td>
            
              <td valign="top">
                  <asp:FileUpload ID="FileUpload5" runat="server" />
                </td>
              <td valign="top"><label>
                  &nbsp;<asp:TextBox ID="txtfile_add_remark" 
                      runat="server"></asp:TextBox>
                  </label>&nbsp;<asp:Button ID="cmdUploadInternalFile" runat="server" 
                      Text="Save" />
                  &nbsp;<asp:Button ID="cmdDelFile" runat="server" Text="Delete" />
                </td>
            </tr>
        </table>
            </div>
          <div class="tabbertab" id="tab_int_speaker" runat="server">
            <h2>
           Speaker
            </h2>
                 <asp:GridView ID="GridSpeaker" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="speaker_id" 
                  EnableModelValidation="True" AllowPaging="True" 
                  EmptyDataText="There is no data.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("speaker_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" VerticalAlign="top" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Title">
                         <ItemStyle Width="80px" VerticalAlign="Top"/>
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name">
                           
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("fname") %>'></asp:Label>
                                
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Last Name">
                      <ItemStyle VerticalAlign="Top" />
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("lname") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:BoundField DataField="speaker_hour" HeaderText="Duration" />
                      <asp:TemplateField HeaderText="Company/ Institute">
                          <ItemTemplate>
                              <asp:Label ID="lblBy" runat="server" Text='<%# Bind("company") %>'></asp:Label><br />
                             
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                      <asp:BoundField HeaderText="Tax ID" DataField="tax_id" />
                      <asp:BoundField HeaderText="Remark" DataField="remark" />
                    
                    
                     
                  </Columns>
              </asp:GridView>
              <br />
              <asp:Panel ID="panel_speaker_add" runat="server">
              <fieldset>
              <legend><Strong>Add new speaker</Strong></legend>
            <table width="100%" cellpadding="3" cellspacing="0" >
             
                <tr>
             
                <td width="33" valign="top">&nbsp;</td>
                <td width="100" valign="top">Title</td>
                <td valign="top">  <asp:TextBox ID="txtspk_title" runat="server" Width="350px"></asp:TextBox> </td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">First name</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtspk_fname" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Last name </td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtspk_lname" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Company</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtspk_company" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Tax ID</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtspk_tax" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Duration</td>
                <td valign="top">
                    <asp:TextBox ID="txtadd_hour" runat="server"></asp:TextBox>
                    &nbsp;Hours</td>
                </tr>
                <tr>
                    <td valign="top">&nbsp;
                        </td>
                    <td valign="top">
                        Remark</td>
                    <td valign="top">
                        <span style="font-weight: bold">
                        <asp:TextBox ID="txtspk_remark" runat="server" Width="350px"></asp:TextBox>
                        </span>
                    </td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddSpeaker" runat="server" Text="Add" Width="100px" />
                    <asp:Button ID="cmdDelSpeaker" runat="server" Text="Delete" 
                        ForeColor="#FF3300" Width="100px" />
                
                  </td>
                </tr>
            </table>
            </fieldset>
            </asp:Panel>
            </div>
          <div class="tabbertab" id="tab_int_schedule" runat="server">
            <h2>
            Training Schedule
            </h2>
             <asp:GridView ID="GridSchedule" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="schedule_id" 
                  EnableModelValidation="True" AllowPaging="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("schedule_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" VerticalAlign="top" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Training Date">
                         <ItemStyle Width="150px" VerticalAlign="Top"/>

                          <ItemTemplate>
                           
                            
                              <asp:Label ID="lblDate1" runat="server" Text='<%# ConvertTSToDateString( Eval("schedule_start_ts")) %>'></asp:Label>
                               <asp:Label ID="lblDate2" runat="server" Text='<%# ConvertTSToDateString(Eval("schedule_end_ts")) %>' Visible="false"></asp:Label>
                               <br />
                               <a href="javascript:;"  onclick="window.open('popup_evaluate.aspx?sh_id=<%# Eval("schedule_id") %>');"><asp:Label runat="server" ID="lblEvaluate" Text="Evaluation Result" /></a>
                          </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="Time Start">
                      <ItemStyle VerticalAlign="Top" Width="100px" />
                          <ItemTemplate>
                                <asp:DropDownList ID="txthour_ingrid_sh1" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>

               <asp:DropDownList ID="txtmin_ingrid_sh1" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
                              <asp:Label ID="lblTimeStart" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts"),"hour")  %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblMinStart" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts"),"min")  %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="End">
                      <ItemStyle Width="100px" />
                          <ItemTemplate>

                             <asp:DropDownList ID="txthour_ingrid_sh2" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>

               <asp:DropDownList ID="txtmin_ingrid_sh2" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
                              <asp:Label ID="lblTimeStart2" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts"),"hour")  %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblMinStart2" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts"),"min")  %>' Visible="false"></asp:Label>
                      
                          </ItemTemplate>                  
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Training Type">
                         
                          <ItemTemplate>
                           <asp:DropDownList ID="txttype" runat="server">
                    <asp:ListItem Value="General Mandatory">General Mandatory</asp:ListItem>
                    <asp:ListItem Value=" Unit  Mandatory"> Unit  Mandatory</asp:ListItem>
                    <asp:ListItem Value="Training">Training</asp:ListItem>
                      <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                              <asp:Label ID="lbltype" runat="server" Text='<%# Bind("schedule_type") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Online">
                          <ItemTemplate>
                              <asp:CheckBox ID="chkOnline" runat="server" />
                              <asp:label ID="lblOnline" runat="server" Text='<%# Bind("is_online") %>' Visible="false" />
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Location">
                          <ItemTemplate>
                             
                              <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("location") %>' Visible="true"></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Max Attendee">      
                          <ItemTemplate>
                              <asp:TextBox ID="lblAttendee" runat="server" Text='<%# Bind("max_attendee") %>' Width="60px"></asp:TextBox>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Status">
                       
                          <ItemTemplate>
                             <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("is_open") %>' Visible="false"></asp:Label>
                              <asp:DropDownList ID="txtstatus" runat="server" Width="80px">
                            
                              <asp:ListItem Value="0">Closed</asp:ListItem>
                              <asp:ListItem Value="1">Open</asp:ListItem>
                              </asp:DropDownList>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                    
                     
                      <asp:TemplateField >
                         
                          <ItemTemplate>
                              
                              <asp:Button ID="cmdRegister" runat="server" Text="รายชื่อผู้ลงทะเบียน" Width="160px"   />
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                    
                     
                  </Columns>
              </asp:GridView>
              <br />
               <asp:Button ID="cmdSaveSchedule" runat="server" 
                            Text="Update Training Schedule" />  <asp:Button ID="cmdDelSchedule" runat="server" Text="Delete" 
                            ForeColor="#FF3300" OnClientClick="return confirm('Are you sure you want to delete this schedule ?')" />
              <br /><br />
              <asp:Panel ID="panel_schedule_add" runat="server">
              <fieldset>
              <legend><strong>Add new schedule</strong></legend>
            <table width="100%" cellpadding="3" cellspacing="0" >
               
             <tr>
                <td width="33" valign="top">&nbsp;</td>
                <td width="150" valign="top">Training Date Start</td>
                <td valign="top">  <asp:TextBox ID="txtsh_date1" runat="server" Width="80px" AutoPostBack="true"></asp:TextBox> 
                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtsh_date1" PopupButtonID="Image4">
                              </asp:CalendarExtender>  <asp:Image ID="Image4" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /> 
                    <asp:DropDownList ID="txthour_sh1" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin_sh1" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList></td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Training Date End</td>
                <td valign="top">
                    <asp:TextBox ID="txtsh_date2" runat="server" Width="80px"></asp:TextBox> 
                     <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtsh_date2" PopupButtonID="Image5">
                              </asp:CalendarExtender>  <asp:Image ID="Image5" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /> 
               <asp:DropDownList ID="txthour_sh2" runat="server">
                        <asp:ListItem Value="0">00</asp:ListItem>
                        <asp:ListItem Value="1">01</asp:ListItem>
                        <asp:ListItem Value="2">02</asp:ListItem>
                        <asp:ListItem Value="3">03</asp:ListItem>
                        <asp:ListItem Value="4">04</asp:ListItem>
                        <asp:ListItem Value="5">05</asp:ListItem>
                        <asp:ListItem Value="6">06</asp:ListItem>
                        <asp:ListItem Value="7">07</asp:ListItem>
                        <asp:ListItem Value="8">08</asp:ListItem>
                        <asp:ListItem Value="9">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                    </asp:DropDownList>
                    :
                    <asp:DropDownList ID="txtmin_sh2" runat="server">
                        <asp:ListItem Value="0">00</asp:ListItem>
                        <asp:ListItem Value="1">01</asp:ListItem>
                        <asp:ListItem Value="2">02</asp:ListItem>
                        <asp:ListItem Value="3">03</asp:ListItem>
                        <asp:ListItem Value="4">04</asp:ListItem>
                        <asp:ListItem Value="5">05</asp:ListItem>
                        <asp:ListItem Value="6">06</asp:ListItem>
                        <asp:ListItem Value="7">07</asp:ListItem>
                        <asp:ListItem Value="8">08</asp:ListItem>
                        <asp:ListItem Value="9">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                        <asp:ListItem Value="32">32</asp:ListItem>
                        <asp:ListItem Value="33">33</asp:ListItem>
                        <asp:ListItem Value="34">34</asp:ListItem>
                        <asp:ListItem Value="35">35</asp:ListItem>
                        <asp:ListItem Value="36">36</asp:ListItem>
                        <asp:ListItem Value="37">37</asp:ListItem>
                        <asp:ListItem Value="38">38</asp:ListItem>
                        <asp:ListItem Value="39">39</asp:ListItem>
                        <asp:ListItem Value="40">40</asp:ListItem>
                        <asp:ListItem Value="41">41</asp:ListItem>
                        <asp:ListItem Value="42">42</asp:ListItem>
                        <asp:ListItem Value="43">43</asp:ListItem>
                        <asp:ListItem Value="44">44</asp:ListItem>
                        <asp:ListItem Value="45">45</asp:ListItem>
                        <asp:ListItem Value="46">46</asp:ListItem>
                        <asp:ListItem Value="47">47</asp:ListItem>
                        <asp:ListItem Value="48">48</asp:ListItem>
                        <asp:ListItem Value="49">49</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="51">51</asp:ListItem>
                        <asp:ListItem Value="52">52</asp:ListItem>
                        <asp:ListItem Value="53">53</asp:ListItem>
                        <asp:ListItem Value="54">54</asp:ListItem>
                        <asp:ListItem Value="55">55</asp:ListItem>
                        <asp:ListItem Value="56">56</asp:ListItem>
                        <asp:ListItem Value="57">57</asp:ListItem>
                        <asp:ListItem Value="58">58</asp:ListItem>
                        <asp:ListItem Value="59">59</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Training Type</td>
                <td valign="top"><span style="font-weight: bold">
                  
                    
                </span>
                    <asp:DropDownList ID="txtsh_type" runat="server">
                    <asp:ListItem Value="General Mandatory">General Mandatory</asp:ListItem>
                    <asp:ListItem Value=" Unit  Mandatory"> Unit  Mandatory</asp:ListItem>
                    <asp:ListItem Value="Training">Training</asp:ListItem>
                      <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Location</td>
                <td valign="top">
                    <asp:TextBox ID="txtsh_location" runat="server" Width="300px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image1000" runat="server" ImageUrl="~/images/spellcheck.png" />
                  </td>
                </tr>
            
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">Max Attendee</td>
                <td valign="top">
                    <asp:TextBox ID="txtadd_attendee" runat="server"></asp:TextBox>
                    &nbsp;persons
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                ControlToValidate="txtadd_attendee" Display="Dynamic" 
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="top">&nbsp;
                        </td>
                    <td valign="top">&nbsp;
                        </td>
                    <td valign="top">
                        <asp:Button ID="cmdAddSchedule" runat="server" Text="Add New" />
                       
                       
                    </td>
                </tr>
            </table>
            </fieldset>
            </asp:Panel>
            </div>
          <div class="tabbertab">
            <h2>
               Estimate budget
            </h2>
            <asp:Panel ID="panel_expense" runat= "server">
            <asp:Panel ID="div_budget" runat="server" Enabled="false">
            <strong>For Accounting Manager</strong>
            <table width="90%">
            <tr><td width="150">Approve status</td>
            <td>
                <asp:DropDownList ID="txtbudget_status" runat="server">
                <asp:ListItem Value="">-- N/A --</asp:ListItem>
                <asp:ListItem Value="0">Not Approve</asp:ListItem>
                <asp:ListItem Value="1">Approve</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdBudgetSave" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to update status ?')" />
             
            </td></tr>
                <tr>
                    <td width="150">
                        Remark</td>
                    <td>
                        <asp:TextBox ID="txtbudget_remark" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        By</td>
                    <td>
                        <asp:Label ID="lblUpdateBy" runat="server" ForeColor="#000099" Text="-"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
           </asp:Panel>
           <div> &nbsp;&nbsp;<a href="preview_budget.aspx?id=<% response.write(id) %>" target="_blank">Print Friendly</a></div>
               <asp:GridView ID="GridExpense" runat="server" CssClass="tdata" CellPadding="3" 
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                    DataKeyNames="expense_id" 
                    EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)">
                  <HeaderStyle BackColor="#CBEDED"  />
                  <Columns>
                      <asp:TemplateField>
                            <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll" AutoPostBack="True" />
            </HeaderTemplate>
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="30px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ประเภทค่าใช้จ่าย/Expense">
                          <ItemTemplate>
                              <asp:Label ID="lblExpensetypeID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblReqBudget" runat="server" Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label><br />
                              -  <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_by") %>' ForeColor="BlueViolet"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server" 
                                  Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงิน/Amount">
                         <ItemStyle HorizontalAlign="Right" />
                          <ItemTemplate>
                              &nbsp;                     
                              <asp:Label ID="lblValue" runat="server" Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                               &nbsp;<asp:Label ID="lblcurtype" runat="server" Text='<%# Eval("currency_type_name") %>'></asp:Label>
                               <asp:Label ID="lblcurtypeid" runat="server" Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" ForeColor="BlueViolet" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="อัตราแลกเปลี่ยน/Exchange rate">
                        <ItemStyle HorizontalAlign="Right" />
                          <ItemTemplate>
                              <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="expense_request_type_name" HeaderText="ประเภทการเบิก/Payment" />
                      <asp:BoundField DataField="expense_payment_name" HeaderText="วิธีชำระเงิน/Payment method" />
                      <asp:BoundField HeaderText="หมายเหตุ/Remark" DataField="expense_remark" />
                       <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <input class="detailButton" name="btnDetail" 
                                          onclick='openDetail(<%# Eval("expense_id") %>)' 
                                          type="button" value="File"  />
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                              </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <asp:Panel ID="panel_add_expense" runat="server">
            <table width="100%" class="tdata">
                <%If GridExpense.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname" width="180"><strong>ประเภทคำใช้จ่าย/Expense</strong></td>
                <td class="colname"><strong>จำนวนเงิน/Amount <span style="font-weight: bold">(Baht)</span></strong></td>
              </tr>
                <%end if %>
             <tr>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="180">&nbsp;
                    </td>
                <td valign="top">
                <table width="100%">
                <tr>
                <td width="180">ประเภทค่าใช้จ่าย/Expense</td>
                <td> 
                    <asp:DropDownList ID="txtadd_expense_type" runat="server" BackColor="#FFFF99" DataValueField="expense_type_id">
                        <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                        <asp:ListItem Value="1">ค่าลงทะเบียน/Registration Fees</asp:ListItem>
                        <asp:ListItem Value="2">ค่าเดินทาง/Travel Expense</asp:ListItem>
                        <asp:ListItem Value="3">ค่าที่พัก/Hotel Expense</asp:ListItem>
                        <asp:ListItem Value="4">ค่าอาหาร/Meal Expense</asp:ListItem>
                        <asp:ListItem Value="5">อื่นๆ/Other</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                    <tr>
                        <td width="150">
                            จำนวนเงิน/Amount</td>
                        <td>
                            <asp:TextBox ID="txtadd_value" runat="server" BackColor="#FFFF99" 
                                Font-Bold="True" ForeColor="#000099" MaxLength="7" Width="180px">0</asp:TextBox>
                            &nbsp;<asp:DropDownList ID="txtcurrency" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">BAHT</asp:ListItem>
                                <asp:ListItem Value="2">USD</asp:ListItem>
                                <asp:ListItem Value="3">EUR</asp:ListItem>
                                <asp:ListItem Value="4">JPY</asp:ListItem>
                                <asp:ListItem Value="5">CNY</asp:ListItem>
                                <asp:ListItem Value="6">HKD</asp:ListItem>
                                <asp:ListItem Value="7">SGD</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ประเภทการเบิก/Method</td>
                        <td><table width="100%" cellspacing="0" cellpadding="0">
                        <tr><td width="150">
                            <asp:DropDownList ID="txtreq_type" runat="server" DataTextField="request_name" DataValueField="request_id">
                          
                            </asp:DropDownList>
                        </td>
                        <td width="150">วิธีชำระเงิน/Payment</td>
                          <td> <asp:DropDownList ID="txtpayment_type" runat="server">
                          <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                           <asp:ListItem Value="4">โอนเงิน / Transfer</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        </table></td>
                    </tr>
                    <tr>
                        <td>
                            อัตราแลกเปลี่ยน/Exchange rate</td>
                        <td>
                            1   <asp:Label ID="lblcurrency0" runat="server" Text=""></asp:Label> ต่อ/per
                            <asp:TextBox ID="txtadd_exchange_rate" runat="server" Width="80px"></asp:TextBox>
                            บาท/baht
                            <br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                        runat="server" ControlToValidate="txtadd_exchange_rate" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!--
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <strong style="color:Red">กรณีขอเบิกล่วงหน้า กรุณาเขียนใบเบิกเงินทดลองจ่ายส่งให้ฝ่ายการเรียนรู้และพัฒนา</strong> <a href="http://bhmossapp1:2009/webonline/PDF/form/thai/ACC/acc_00680_i_f_t_1212_rev02.pdf" target="_blank">แบบฟอร์มคลิกที่นี่</a></td>
                    </tr>
                    -->
                    <tr>
                        <td>&nbsp;
                            
                        </td>
                        <td>
                            <asp:CheckBox ID="txtadd_sponsor" runat="server" 
                                Text="อบรมโดยมีสปอนเซอร์สนับสนุน" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">
                            หมายเหตุ/Remark</td>
                        <td>
                            <asp:TextBox ID="txtadd_expense_remark" runat="server" Rows="3" TextMode="MultiLine" 
                                Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                   
                        </td>
              </tr>
              <tr>
                  
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddExpense" runat="server" Text="เพิ่มรายการ (Add new)" OnClientClick="return validateExpense();"  />
                     <asp:Button ID="cmdDelExpense" runat="server" Text="ลบรายการ (Delete)" 
                        CausesValidation="False" />
                      <asp:Button ID="cmdSaveOrder" runat="server" Text="Update Order" 
                        CausesValidation="False" Visible="False" />
               </td>
              </tr>
            
            </table>
            </asp:Panel>
            <div><strong>Total Budget</strong> :  <asp:Label ID="txttotal" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Baht <strong>Request 
                Budget</strong>  <asp:Label ID="txtrequest_budget" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Baht
                        </div>
            </asp:Panel>
            <br />
          </div>
            <div class="tabbertab" id="tab_account_expense" runat="server">
            <h2>
               Actual Expense
            </h2>
                 <asp:Panel ID="panel_update_expense" runat="server" >
          
            <table width="90%">
            <tr><td width="150">Accounting status</td>
            <td>
                <asp:DropDownList ID="txtexpense_status" runat="server">
                <asp:ListItem Value="">-- N/A --</asp:ListItem>
                <asp:ListItem Value="0">On process</asp:ListItem>
                <asp:ListItem Value="1">Completed</asp:ListItem>
                </asp:DropDownList>
                
                &nbsp;
                
            </td></tr>
                <tr>
                    <td width="150">
                        Remark</td>
                    <td>
                        <asp:TextBox ID="txtexpense_remark" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        By</td>
                    <td>
                        <asp:Label ID="lblUpdateby2" runat="server" ForeColor="#000099" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="150" style="vertical-align:top">
                      
                        Attach file</td>
                    <td>   <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload3" runat="server"  />
                      <asp:Button ID="cmdUploadFileExpense"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdDeleteFileExpense" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
              
                <asp:GridView ID="GridFileExpense" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("trainging_file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/hr/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView>
                        &nbsp;</td>
                </tr>
            </table>

           
             </asp:Panel>
            
               <div>&nbsp;&nbsp; <asp:Button ID="cmdUpdateExpense" runat="server" Text="Update" Width="100px" /> </div>
                <br />
                <asp:GridView ID="GridExpense2" runat="server" CssClass="tdata" CellPadding="3" 
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                    DataKeyNames="expense_id" 
                    EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)">
                  <HeaderStyle BackColor="#CBEDED"  />
                  <Columns>
                      <asp:TemplateField>
                             <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll2" AutoPostBack="True" />
            </HeaderTemplate>
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("is_delete") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblTopicID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="30px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ประเภทค่าใช้จ่าย/Expense">
                          <ItemTemplate>
                             <asp:Label ID="lblExpensetypeID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblReqBudget" runat="server" Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label><br />
                              -  <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_by") %>' ForeColor="BlueViolet"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server" 
                                  Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงิน/Amount">
                         <ItemStyle />
                          <ItemTemplate>
                              &nbsp;                     
                              <asp:Label ID="lblValue" runat="server" Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                               &nbsp;<asp:Label ID="lblcurtype" runat="server" Text='<%# Eval("currency_type_name") %>'></asp:Label>
                               <asp:Label ID="lblcurtypeid" runat="server" Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" ForeColor="BlueViolet" ></asp:Label>
                               <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>' Visible="false"></asp:Label>  
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงินอนุมัติ/Approve Budget">
                         
                          <ItemTemplate>
                              <asp:Label ID="lblApprove" runat="server"></asp:Label><Br />
                                <asp:Label ID="lblConvertToBaht2" runat="server" Font-Underline="true" ForeColor="BlueViolet" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="วันที่ทำรายการ/Date">
                          <ItemTemplate>
                              <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("create_date", "{0:dd/MM/yyyy hh:mm}") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ผู้มาติดต่อ/Contact Person">
                          <ItemTemplate>
                              <asp:TextBox ID="textPersonContact" runat="server" 
                                  Text='<%# Bind("acc_receive_by") %>' Width="80px"></asp:TextBox>
                              <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" 
                                  CompletionSetCount="5" DelimiterCharacters="" Enabled="True" 
                                  EnableViewState="false" MinimumPrefixLength="2" ServiceMethod="getEmployee" 
                                  ServicePath="~/EmployeeService.asmx" TargetControlID="textPersonContact">
                              </asp:AutoCompleteExtender>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="บันทึกโดย/Record by" DataField="acc_expense_by" />
                      <asp:TemplateField HeaderText="ประเภท/Method">
                        
                          <ItemTemplate>
                              <asp:Label ID="lbltype_id" runat="server" Text='<%# Bind("expense_request_type_id") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblpayment_id" runat="server" Text='<%# Bind("expense_payment_id") %>' Visible="false"></asp:Label>
                              <asp:DropDownList ID="txttype" runat="server" Width="160px">
                              
                              <asp:ListItem Value="5">ชำระเงิน / Payment</asp:ListItem>
                              <asp:ListItem Value="4">รับคืน / Clear advance</asp:ListItem>
                              <asp:ListItem Value="7">เบิกคืน / Reimbursement</asp:ListItem>
                              </asp:DropDownList>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="วิธีชำระเงิน/Payment">
                         
                          <ItemTemplate>
                                 <asp:DropDownList ID="txtpayment" runat="server" Width="140px">
                                  <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                           <asp:ListItem Value="4">โอนเงิน / Transfer</asp:ListItem>
                              </asp:DropDownList>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Advance">
                         
                          <ItemTemplate>
                          <asp:Label id="lblMoney" runat="server" Text='<%# Bind("is_receive_money") %>' Visible="false" />
                              <asp:CheckBox ID="chkReceive" runat="server" />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="หมายเหตุ/Remark">
                          <ItemTemplate>
                              <asp:TextBox ID="txtremark" runat="server" Text='<%# Bind("expense_remark") %>' Width="100" ToolTip='<%# Bind("expense_remark") %>'></asp:TextBox>
                            
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <input class="detailButton" name="btnDetail" 
                                          onclick='openDetail(<%# Eval("expense_id") %>)' 
                                          type="button" value="File"  />
                                      <asp:Label ID="lblFileNum" runat="server" Text=""></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                              </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <asp:Panel ID="panel_actual_expense" runat="server">
            <table width="100%" class="tdata">
                <%If GridExpense.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname" width="180"><strong>ประเภทค่าใช้จ่าย/Expense</strong></td>
                <td class="colname"><strong>จำนวนเงิน/Amount <span style="font-weight: bold">(Baht)</span></strong></td>
              </tr>
                <%end if %>
             <tr>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="180">&nbsp;
                    </td>
                <td valign="top">
                <table width="100%">
                <tr>
                <td width="180">ประเภทค่าใช้จ่าย/Expense</td>
                <td> <asp:DropDownList ID="txtadd_expense_type2" runat="server" BackColor="#FFFF99" DataValueField="expense_type_id">
                    <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                    <asp:ListItem Value="1">Registration Fees</asp:ListItem>
                     <asp:ListItem Value="2">Travel Expense</asp:ListItem>
                    <asp:ListItem Value="3">Hotel Expense</asp:ListItem>
                    <asp:ListItem Value="4">Meal Expense</asp:ListItem>
                    <asp:ListItem Value="5">Other</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                    <tr>
                        <td width="150">
                            จำนวน/Amount</td>
                        <td>
                            <asp:TextBox ID="txtadd_value2" runat="server" BackColor="#FFFF99" 
                                Font-Bold="True" ForeColor="#000099" MaxLength="7" Width="250px">0</asp:TextBox>
                            &nbsp;<asp:DropDownList ID="txtcurrency2" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">BAHT</asp:ListItem>
                                <asp:ListItem Value="2">USD</asp:ListItem>
                                <asp:ListItem Value="3">EUR</asp:ListItem>
                                <asp:ListItem Value="4">JPY</asp:ListItem>
                                <asp:ListItem Value="5">CNY</asp:ListItem>
                                <asp:ListItem Value="6">HKD</asp:ListItem>
                                <asp:ListItem Value="7">SGD</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtadd_value2" Display="Dynamic" 
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ประเภทการจ่าย/Method</td>
                        <td><table width="100%" cellspacing="0" cellpadding="0">
                        <tr><td width="150">
                            <asp:DropDownList ID="txtreq_type2" runat="server" DataTextField="request_name_th" DataValueField="request_id">
                          
                            </asp:DropDownList>
                        </td>
                        <td width="150">วิธีชำระเงิน/Payment</td>
                          <td> <asp:DropDownList ID="txtpayment_type2" runat="server">
                          <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                           <asp:ListItem Value="4">โอนเงิน / Transfer</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        </table></td>
                    </tr>
                    <tr>
                        <td>
                            อัตราแลกเปลี่ยน/Exchange rate</td>
                        <td>
                            1  <asp:Label ID="lblcurrency" runat="server" Text=""></asp:Label>  ต่อ/per
                            <asp:TextBox ID="txtadd_exchange_rate2" runat="server" Width="80px"></asp:TextBox>
                            บาท/baht<br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                        runat="server" ControlToValidate="txtadd_exchange_rate2" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            ชื่อผู้รับเงิน/Contact</td>
                        <td class="style2">
                            <asp:TextBox ID="txtadd_receive" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">
                            หมายเหตุ/Remark</td>
                        <td>
                            <asp:TextBox ID="txtadd_expense_remark2" runat="server" Rows="3" TextMode="MultiLine" 
                                Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                   
                        </td>
              </tr>
              <tr>
                  
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddExpense2" runat="server" Text="เพิ่มรายการ (Add new)"   />
                     <asp:Button ID="Button3" runat="server" Text="ลบรายการ (Delete)" 
                        CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete?')" />
               </td>
              </tr>
            
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Approved amount</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblApproveBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Approved by</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblApproveName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Received amount</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblReturnBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Black" Text="0"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>Total expense</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="txtactual_expense" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="#006600" Text="0"></asp:Label>
                        Baht</td>
                </tr>
            </table>
            </asp:Panel>
            </div>
          <div class="tabbertab" id="tab_trd" runat="server">
            <h2>
             Part of TRD 
            </h2>
              <asp:Panel ID="panel_reply" runat="server">
            <table width="100%">
              <tr>
                <td colspan="2" valign="top"><table width="100%" cellspacing="0" cellpadding="4">
                    <tr>
                <td valign="top" bgcolor="#eef1f3"><table>
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top">
              <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload2" runat="server"  />
                      <asp:Button ID="cmdTRDUpload"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdTRDDelete" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFileTRD" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px" DataKeyNames="trainging_file_id"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK1" runat="server" Text='<%# Bind("trainging_file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect1" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/hr/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView></td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
                    <tr>
                      <td height="24" valign="top" bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2">
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6"><strong>Private Note</strong></td>
                          <td valign="top" bgcolor="#DBE1E6"><strong>
                            <textarea name="txttrd_note" id="txttrd_note" cols="45" rows="3" style="width: 450px" runat="server"></textarea>
                          </strong></td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6"><strong>Reply Message</strong></td>
                          <td valign="top" bgcolor="#DBE1E6">
                <asp:TextBox ID="txtreply" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="450px" BackColor="#CCFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6">Last Update</td>
                          <td valign="top" bgcolor="#DBE1E6">
                        <asp:Label ID="lblLastUpdate" runat="server" Text="lblLastUpdate"></asp:Label> &nbsp;
                        <asp:Label ID="lblreply_by" runat="server" Text="lblreply_by"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6">&nbsp;</td>
                          <td valign="top" bgcolor="#DBE1E6">
                        <asp:Button ID="cmdReply" runat="server" Text="Update Reply" />
                            </td>
                        </tr>
                      </table></td>
                      </tr>
                    
                </table></td>
              </tr>
            </table>

           
  
            
            </asp:Panel>
            </div>
          <div class="tabbertab" id="tab_approve" runat="server">
              <h2>Approval and Comment</h2>
               <asp:Panel ID="panelManager1" runat="server" BorderStyle="None">
                  <div>
        <strong>Requester Level :</strong> <asp:Label ID="lblEmpLevel" runat="server"  /><br />
        <strong>Your Level :</strong> <asp:Label ID="lblYourLevel" runat="server"  /><br />
        <strong>Your Authorize :</strong> <asp:Label ID="lblMsg" runat="server" Font-Bold="true"  />
        </div>
            <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td width="100%" colspan="5">
                      <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px" BorderStyle="None">
                          <Columns>
                              <asp:TemplateField>
                              <EditItemTemplate>
                              test
                              </EditItemTemplate>
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td colspan="2" bgcolor="#DBE1E6">
                                            </td>
                    </tr>
                    <tr>
                      <td width="60" rowspan="3" valign="top"><p><img src="../images/thumb_user.jpg" width="50" height="50" /><br />
                              <br />
                      </p></td>
                      <td bgcolor="#EDF4F9"><table width="100%" border="0">
                        <tr>
                          <td valign="top"><strong>
                          <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                          </strong>
                          <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>' Visible="false"></asp:Label>
,                          
<asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label> 
,
                          <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                          <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
                         </td>
                          <td align="right"><div align="right">
                              <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                              <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Edit Comment" />                      
                              <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/bt_delete.gif" OnCommand="onDeleteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' OnClientClick="return confirm('Are you sure you want to delete?');" />                      
                          </div></td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><strong>
                        <asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />                        
                        <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label>
: </strong>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label></td>
                    </tr>
                  </table>

                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                
                    <asp:Panel ID="panelAddComment" runat="server" Visible="false">
                    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"></td>
                        <td bgcolor="#DBE1E6">
                        
                        </td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Acknowledge</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td width="80">
                                  <asp:DropDownList ID="txtacknowedge_status" runat="server">
                                  <asp:ListItem Value="3">N/A</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                   
                                  </asp:DropDownList>
                                  </td>
                              <td width="25">&nbsp;</td>
                              <td width="158"><strong>Comment Subject</strong></td>
                              <td>
                                    <!--
                                   <asp:ListItem Value="1">เพื่อเพิ่มความรู้ในการปฏิบัติงาน</asp:ListItem>
                                    <asp:ListItem Value="2">แผนการพัฒนาไม่ตรงกับตำแหน่งงาน</asp:ListItem>
                                    <asp:ListItem Value="3">อื่นๆ</asp:ListItem>
                                     -->
                                 <asp:DropDownList ID="txtcomment" runat="server">
                                  <asp:ListItem Value="">-</asp:ListItem>
                                   
                                      <asp:ListItem Value="101">มีงบประมาณ</asp:ListItem>
                                      <asp:ListItem Value="102"> มีงบประมาณและอยู่ใน IDP</asp:ListItem>
                                      <asp:ListItem Value="103">นอกงบประมาณ</asp:ListItem>
                                      <asp:ListItem Value="104">นอกงบประมาณและอยู่ใน IDP</asp:ListItem>
                                      <asp:ListItem Value="105">หัวข้ออบรมไม่สอดคล้องกับแผนพัฒนาหรือตำแหน่งงาน</asp:ListItem>
                                      <asp:ListItem Value="106">อื่นๆ</asp:ListItem>
                                      
                                  </asp:DropDownList>
                            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3"><strong>Comment Detail</strong></td>
                        <td bgcolor="#eef1f3"><textarea name="txtcomment_detail" id="txtcomment_detail" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3">&nbsp;</td>
                        <td bgcolor="#eef1f3"> 
                            
                            &nbsp;<asp:ImageButton ID="cmdAddComment" ImageUrl="~/images/bt_save.gif" runat="server" />
                          </td>
                      </tr>
                    </table>
                    </asp:Panel>

                  <br /></td>
                </tr>
              </table>
            
              </asp:Panel>
            <br />
            </div> 
              <div class="tabbertab" id="tab_goal" runat="server">
            <h2>
            Actions after training
            </h2>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                      </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel ID="panel_goal" runat="server">
            <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td valign="top" bgcolor="#DBE1E6"><strong>หัวข้อที่ฝึกอบรม/Title</strong></td>
                <td valign="top" bgcolor="#DBE1E6">
                    <asp:Label ID="lblGoalTitle" runat="server" Text="Label" Font-Bold="true"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top"><strong>ประเภทการอบรม/Training type</strong></td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335"> <asp:Label ID="lblGoalType" runat="server" Text="Label" Font-Bold="false"></asp:Label></td>
                      <td><strong>วันที่อบรม/Date</strong>  <asp:Label ID="lblGoalTrainDate" runat="server" Text="Label" Font-Bold="false"></asp:Label></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top"> 
                    <asp:CheckBox ID="chk_certificate" runat="server" Text="มีใบประกาศนียบัตร / อื่นๆ โปรดระบุ / Certificate or Other (Please identify)" />
                    &nbsp;<asp:TextBox ID="txtcertificate_remark" runat="server"></asp:TextBox>
                  </td>
              </tr>
                 <tr>
                    <td valign="top" >
                        &nbsp;</td>
                    <td valign="top">
                     <asp:Label ID="lblActionAttach1" runat ="server" Text = "แนบประกาศนียบัตร หรือ เอกสารรับรองอื่นๆ ถ้ามี" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        &nbsp;</td>
                    <td valign="top">
                        <asp:Label ID="lblCerFile" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        </td>
                    <td valign="top">
                        <asp:FileUpload ID="FileUpload6" runat="server" />
                        <asp:Button ID="cmdUploadCertifica" runat="server" Text="Attached file" />
                        <asp:Button ID="cmdDelCer" runat="server" Text="Delete File" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <strong>สถานที่อบรม/Facility </strong>
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblGoalFacility" runat="server" Font-Bold="true" Text="Label"></asp:Label>
                    </td>
                </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>ระดับความรู้ความสามารถ / My knowledge and skill level</strong></td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;
                    </td>
              </tr>
                <tr  >
                    <td bgcolor="#eef1f3" style="color:Red" valign="top">
                        * ก่อนการอบรมนี้ / before this training </td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:RadioButtonList ID="txtgoal_level" runat="server" 
                            RepeatDirection="Horizontal" ForeColor="Red">
                            <asp:ListItem>ยังไม่มีทักษะ/No skill</asp:ListItem>
                            <asp:ListItem>น้อย/Low</asp:ListItem>
                            <asp:ListItem>ปานกลาง/Medium</asp:ListItem>
                            <asp:ListItem>สูง/High</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top" style="color:Red">
                        * หลังการอบรมนี้ / after this training</td>
                    <td bgcolor="#eef1f3" valign="top" style="color:Red">
                        <asp:RadioButtonList ID="txtgoal_level_after" runat="server"  ForeColor="Red"
                            RepeatDirection="Horizontal">
                            <asp:ListItem >ยังไม่มีทักษะ/No skill</asp:ListItem>
                            <asp:ListItem>น้อย/Low</asp:ListItem>
                            <asp:ListItem>ปานกลาง/Medium</asp:ListItem>
                            <asp:ListItem>สูง/High</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">&nbsp;
                        </td>
                    <td bgcolor="#eef1f3" valign="top">
                        ยังไม่มีทักษะ / No skill = มีความรู้แต่ยังต้องฝึกฝนให้มีทักษะ<br /> น้อย / low = 
                        นำความรู้และทักษะมาใช้ในงานได้ แต่ยังต้องมีผู้แนะนำ<br /> ปานกลาง / Medium = 
                        นำความรู้และทักษะมาใช้ในงานได้ด้วยตนเอง ต้องการคำแนะนำน้อยมาก<br /> สูง / High = 
                        นำความรู้และทักษะมาใช้ในงานได้ด้วยตนเอง และสามารถสอนผู้อื่นให้เข้าใจได้</td>
                </tr>
              <tr>
                <td colspan="2" valign="top"><strong>หัวข้อที่ได้เรียนรู้และมีประโยชน์ / What did I 
                    learn from this course that will be particularly helpful?<br />
                </strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><strong>
                  <textarea name="txtgoal_benefit" id="txtgoal_benefit" cols="45" rows="3" style="width: 735px" runat="server"></textarea>
                </strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>ฉันจะเอาความรู้ที่ได้มาปรับใช้อย่างไร / How 
                    will I apply what I have learned?</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top">
                   
                       <asp:GridView ID="GridGoal" runat="server" 
                        CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("goal_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="30px" />
                      </asp:TemplateField>
                   
                      <asp:BoundField DataField="action_detail" HeaderText="สิ่งที่จะดำเนินการ/Action" >
                      <ItemStyle Width="250px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="รายละเอียด/Detail">
                         <ItemStyle Width="250px" />
                          <ItemTemplate>
                              <asp:Label ID="lblPkAction" runat="server" Text='<%# Bind("goal_id") %>' 
                                  Visible="false"></asp:Label>
                           
                              <asp:Label ID="Label9" runat="server" Text='<%# Bind("person_detail") %>'></asp:Label>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="เป้าหมายของการดำเนินการ / Expected goal of the action (s)”">
                         
                          <ItemTemplate>
                                <asp:Label ID="lblExpectGoal" runat="server" Text='<%# Bind("expect_goal_text") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="เรี่มต้น /Start">
                          <ItemTemplate>
                              <asp:Textbox ID="txtdate1" runat="server" Text='<%# ConvertTSToDateString(Eval("start_date_ts")) %>' 
                                  Visible="true" Width="100px"></asp:Textbox>
                                   <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" 
                                   Format="dd/MM/yyyy" PopupButtonID="Image99" TargetControlID="txtdate1">
                               </asp:CalendarExtender>
                               &nbsp;<asp:Image ID="Image99" runat="server" CssClass="mycursor" 
                                   ImageUrl="~/images/calendar.gif" />
                          </ItemTemplate>
                        
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="สิ้นสุด /Completed">
                          <ItemTemplate>
                            
                              <asp:Textbox ID="txtdate2" runat="server" Text='<%# ConvertTSToDateString(Eval("end_date_ts")) %>' 
                                  Visible="true" Width="100px"></asp:Textbox>
                                  <asp:CalendarExtender ID="CalendarExtender66" runat="server" Enabled="True" 
                                   Format="dd/MM/yyyy" PopupButtonID="Image999" TargetControlID="txtdate2">
                               </asp:CalendarExtender>
                               &nbsp;<asp:Image ID="Image999" runat="server" CssClass="mycursor" 
                                   ImageUrl="~/images/calendar.gif" />
                          </ItemTemplate>
                       
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="สถานะ/Status">
                     
                          <ItemTemplate>
                              <asp:DropDownList ID="txtaction_status" runat="server">
                               <asp:ListItem Value="1">กำลังดำเนินการ / Inprogress</asp:ListItem>
                                   <asp:ListItem Value="2">ดำเนินการต่อเนื่อง / Continue</asp:ListItem>
                                   <asp:ListItem Value="3">เสร็จสมบรูณ์ / Complete</asp:ListItem>
                                   <asp:ListItem Value="4">ระงับไว้ก่อน / Pending</asp:ListItem>
                                   <asp:ListItem Value="5">ยกเลิก / Cancel</asp:ListItem>
                              </asp:DropDownList>
                          <asp:Label ID="lblActionId" runat="server" Text='<%# Bind("action_status") %>' 
                                  Visible="false"></asp:Label>
                           
                          </ItemTemplate>
                          
                      </asp:TemplateField>
                     
                  
                     
                  </Columns>
              </asp:GridView>
              <!--
                      <a id="showDept" href="javascript:;" onclick="$('#showDept').hide();$('#hideDept').show();$('#deptContent').show();"><asp:Image ID="Image61" runat="server" ImageUrl="~/images/plus.gif" /> Add new goal</a>
                <a id="hideDept" href="javascript:;" style="display:none"  onclick="$('#showDept').show();$('#hideDept').hide();$('#deptContent').hide();"><asp:Image ID="Image62" runat="server" ImageUrl="~/images/minus.gif" /> Hide</a>      
         -->
                    &nbsp;<asp:Button ID="cmdDeleteGoal" runat="server" ForeColor="Red" 
                        Text=" Delete " CausesValidation="False" />
                          <span id="deptContent" >
               <asp:Panel ID="panel_addgoal" runat="server">
                  <table width="100%"  class="tdata" cellpadding="3">  
                     
              <tr>
                <td valign="top" width="200" >
                    <strong>สิ่งที่จะดำเนินการ/Action</strong></td>
                <td valign="top" >
                    <asp:DropDownList ID="txtadd_action" runat="server">
                        <asp:ListItem>ใช้ในการดูแลผู้ป่วยให้ดีขึ้น</asp:ListItem>
                        <asp:ListItem>ใช้ความรู้และทักษะในงานประจำ</asp:ListItem>
                        <asp:ListItem>แบ่งปันความรู้ให้เพื่อนร่วมงาน</asp:ListItem>
                        <asp:ListItem>เริ่มโครงการหรือบริการใหม่</asp:ListItem>
                        <asp:ListItem>ปรับปรุงระบบงานเดิมให้ดีขึ้น</asp:ListItem>
                        <asp:ListItem>อื่นๆ</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                      
                   </td>
              </tr>
          
          
                     
                       <tr>
                           <td valign="top" class="style2" >
                               <strong>รายละเอียด/Detail</strong></td>
                           <td valign="top" class="style2">
                               <asp:TextBox ID="txtadd_whom" runat="server" Rows="3" TextMode="MultiLine" 
                                   Width="300px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td valign="top" ><strong>เป้าหมายของการดำเนินการ</strong> &nbsp;
                               </td>
                           <td valign="top">
                               <asp:DropDownList ID="txtgoal_action" runat="server" DataValueField="expect_id" DataTextField="expect_detail">
                              
                               </asp:DropDownList>
                           </td>
                       </tr>
                       <tr>
                           <td valign="top">
                               <strong>กำหนดเริ่ม/Start</strong></td>
                           <td valign="top">
                               <asp:TextBox ID="txtadd_date1" runat="server" Width="58px"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" 
                                   Format="dd/MM/yyyy" PopupButtonID="Image9" TargetControlID="txtadd_date1">
                               </asp:CalendarExtender>
                               &nbsp;<asp:Image ID="Image9" runat="server" CssClass="mycursor" 
                                   ImageUrl="~/images/calendar.gif" />
                           </td>
                      </tr>
                       <tr>
                           <td valign="top" >
                               <strong>กำหนดเสร็จ/Complete</strong></td>
                           <td valign="top" >
                               <asp:TextBox ID="txtadd_date2" runat="server" Width="58px"></asp:TextBox>
                               <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" 
                                   Format="dd/MM/yyyy" PopupButtonID="Image10" TargetControlID="txtadd_date2">
                               </asp:CalendarExtender>
                               &nbsp;<asp:Image ID="Image10" runat="server" CssClass="mycursor" 
                                   ImageUrl="~/images/calendar.gif" />
                           </td>
                       </tr>
                       <tr>
                           <td valign="top">
                               <strong>สถานะ/Status</strong></td>
                           <td valign="top" >
                               <asp:DropDownList ID="txtadd_status" runat="server">
                                   <asp:ListItem Value="1">กำลังดำเนินการ / Inprogress</asp:ListItem>
                                   <asp:ListItem Value="2">ดำเนินการต่อเนื่อง / Continue</asp:ListItem>
                                   <asp:ListItem Value="3">เสร็จสมบรูณ์ / Complete</asp:ListItem>
                                   <asp:ListItem Value="4">ระงับไว้ก่อน / Pending</asp:ListItem>
                                   <asp:ListItem Value="5">ยกเลิก / Cancel</asp:ListItem>
                               </asp:DropDownList>
                           </td>
                       </tr>
                      <tr>
                          <td valign="top" >&nbsp;
                              </td>
                          <td valign="top">
                              <asp:Button ID="cmdAddGoal" runat="server" Font-Bold="True" Text="Save new Action" />
                              &nbsp;&nbsp;&nbsp;</td>
                      </tr>
            </table>
              </asp:Panel>
              </span>
                    
                 


                </td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>อุปสรรค</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top">
                    <asp:TextBox ID="txtgoal_problem" runat="server" Rows="2" TextMode="MultiLine" 
                        Width="90%"></asp:TextBox>
                  </td>
              </tr>
              </table>
              <div id="action_goal" runat="server" visible="false">
               <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td colspan="2" valign="top"><strong>เป้าหมายดำเนินการ / Expected outcome of the actions</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top">
                    <asp:CheckBox ID="ch_kpi1" runat="server" />
&nbsp;ปรับความก้าวหน้าจากระดับ/adjust career ladder from level
                   <asp:DropDownList ID="txtgoal_ladder1" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                  เป็นระดับ/to level
                   <asp:DropDownList ID="txtgoal_ladder2" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                  <br />
                    <asp:CheckBox ID="ch_kpi2" runat="server" />&nbsp; 
                    ผลประเมินการทำงานดีขึ้นจาก/Performance appraisal score increase from
                 <asp:TextBox ID="txtgoal_kpi2" runat="server" Width="100px" Visible="False"></asp:TextBox>
                    <asp:DropDownList ID="txtgoal_kpi2_scope1" runat="server">
                        <asp:ListItem Value="UN">UN</asp:ListItem>
                        <asp:ListItem Value="IN">IN</asp:ListItem>
                        <asp:ListItem Value="ME">ME</asp:ListItem>
                        <asp:ListItem Value="EE">EE</asp:ListItem>
                        <asp:ListItem Value="FE">FE</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;เป็น/to
                    <asp:DropDownList ID="txtgoal_kpi2_scope2" runat="server">
                     <asp:ListItem Value="UN">UN</asp:ListItem>
                        <asp:ListItem Value="IN">IN</asp:ListItem>
                        <asp:ListItem Value="ME">ME</asp:ListItem>
                        <asp:ListItem Value="EE">EE</asp:ListItem>
                        <asp:ListItem Value="FE">FE</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CheckBox ID="ch_kpi3" runat="server" />
                    &nbsp; ตัวชี้วัดหลักตามเป้าหมายแผนกดีขึ้น / Department KPI increase
             
                  <asp:TextBox ID="txtgoal_kpi3" runat="server" Width="100px"></asp:TextBox>
                  %
                  <br />
                  <asp:CheckBox ID="ch_kpi4" runat="server" />&nbsp;
                  อัตราการลื่นตกหกล้มลดลง / Fall rate decrease
             
                  <asp:TextBox ID="txtgoal_kpi4" runat="server" Width="100px"></asp:TextBox>
                  %
                  <br />
                  <asp:CheckBox ID="ch_kpi5" runat="server" />&nbsp;
                  ความคลาดเคลื่อนทางยาลดลง / Medication error rate decrease
             
                  <asp:TextBox ID="txtgoal_kpi5" runat="server" Width="100px"></asp:TextBox>
                  %
                  <br />
                  <asp:CheckBox ID="ch_kpi6" runat="server" />&nbsp;
                  เพิ่มความพึงพอใจลูกค้า / Customer engagement score increase
             
                  <asp:TextBox ID="txtgoal_kpi6" runat="server" Width="100px"></asp:TextBox>
                  %
                  <br />
                  <asp:CheckBox ID="ch_kpi7" runat="server" />&nbsp;
                  เพิ่มความผูกพันของพนักงาน / Employee engagement score increase
             
                  <asp:TextBox ID="txtgoal_kpi7" runat="server" Width="100px"></asp:TextBox>
                  %
                <br />
                  <asp:CheckBox ID="ch_kpi8" runat="server" />&nbsp;
                  อื่นๆ / Others
                  <asp:TextBox ID="txtgoal_kpi8" runat="server" Width="500px" MaxLength="200"></asp:TextBox></td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="top"><div align="right"></div></td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="top">&nbsp;</td>
              </tr>
            </table>
            </div>
            </asp:Panel>
          
            <br />
          </div>
  <div class="tabbertab" id="tab_update" runat="server">
  <h2>Additional Comment</h2>
     <asp:GridView ID="GridInformation" runat="server" AutoGenerateColumns="False" 
                          CellPadding="4" Width="95%" ForeColor="#333333" 
        GridLines="None" EnableModelValidation="True" DataKeyNames="inform_id">
                          <RowStyle BackColor="#E3EAEB" />
                          <Columns>
                              <asp:TemplateField HeaderText="No" SortExpression="date">
                                 <ItemStyle Width="40px" />
                                  <ItemTemplate>
                                     <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:BoundField HeaderText="Date/Time" DataField="inform_date" ItemStyle-Width="120px" />
                              <asp:BoundField DataField="inform_detail" HeaderText="Information update" ItemStyle-Width="400px" />
                              <asp:BoundField DataField="inform_by" HeaderText="By" />
                              <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:Label ID="lblEmpNo" runat="server" Text='<%# Bind("inform_emp_code") %>' Visible="false"></asp:Label>
                                      <asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" 
                                          CommandName="Delete" Text="Delete"></asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="80px" />
                              </asp:TemplateField>
                          </Columns>
                          <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                          <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                          <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                          <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                          <EditRowStyle BackColor="#7C6F57" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                      <br />
    <table width="100%">
    <tr>
    <td width="150" style="vertical-align:top">Additional information</td>
    <td style="vertical-align:top">
        <asp:TextBox ID="txtadd_update" runat="server" Rows="4" TextMode="MultiLine" 
            Width="90%"></asp:TextBox></td>
    </tr>
    <tr>
    <td width="150">&nbsp;</td>
    <td>
        <asp:Button ID="cmdAddUpdate" runat="server" Text="Add more information" 
            CausesValidation="False" />
        </td>
    </tr>
    </table>
  
      <br />
</div>
        </div>
        <br />
        <br />
        <div align="right">
          &nbsp;&nbsp;&nbsp;
&nbsp;
 <asp:Button ID="cmdSaveDraft2" runat="server" Text="Save as Draft" Width="150px" OnCommand="onSave" 
            CommandArgument="1" />
           
            &nbsp;
             <asp:Button ID="cmdSubmit2" runat="server" Text="Submit" Width="150px" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True" OnClientClick="return  validateExternalForm(); "  />
&nbsp;&nbsp;
</div>
      </div>
      </asp:Panel>
</asp:Content>

