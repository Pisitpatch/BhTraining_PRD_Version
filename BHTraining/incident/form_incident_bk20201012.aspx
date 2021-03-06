<%@ Page Title="" Language="VB" MasterPageFile="~/incident/Incident_MasterPage.master" AutoEventWireup="false" CodeFile="form_incident.aspx.vb" Inherits="incident.incident_form_incident" MaintainScrollPositionOnPostback="true" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc" TagName="Fall"   Src="~/incident/FallControl.ascx"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          var lock = <% Response.Write(is_lock)%>;
          if (parseInt(lock) == 1){
              $(":input").attr("disabled"	,"disabled");
          }

          $("#ctl00_ContentPlaceHolder1_txtdiagnosis").autocomplete("../getDiagnosis.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtdiagnosis').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });



          $("#ctl00_ContentPlaceHolder1_txtdoctor").autocomplete("../getDoctor.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtdoctor').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_txtdoctor").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtexam_doctor").autocomplete("../getDoctor.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtexam_doctor').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtexam_doctor").click(function () {
              $(this).select();
          });

          //  alert( $("#ctl00_ContentPlaceHolder1_txtprocedure").val() );

          $("#ctl00_ContentPlaceHolder1_txtatt_doctor").autocomplete("../getDoctor.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtatt_doctor').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;

              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_txtatt_doctor").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_txttqm_finddoctor").autocomplete("../getDoctor.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txttqm_finddoctor').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data[1];

              //alert("serial ::" + serial);
              $("#ctl00_ContentPlaceHolder1_txtmdcode").val(serial);
          });

          $("#ctl00_ContentPlaceHolder1_txttqm_finddoctor").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtdrugname").autocomplete("../getDrugLIstHandler.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtdrugname').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert(data[1]);
              $('#ctl00_ContentPlaceHolder1_ctl03_txtdruggroup').val(data[1]);
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtdrugname").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtdrugname_right").autocomplete("../getDrugLIstHandler.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtdrugname_right').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtdrugname_right").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtfluid").autocomplete("../getDrugLIstHandler.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtfluid').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtfluid").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txt_ivmed_other").autocomplete("../getDrugLIstHandler.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txt_ivmed_other').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txt_ivmed_other").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_txtpsm_diagnonsis").autocomplete("../getDiagnosis.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtpsm_diagnonsis').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_txtpsm_diagnonsis").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_txtcountry").autocomplete("../getNational.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtcountry').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_txtcountry").click(function () {
              $(this).select();
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

            'onLoad': function(argsObj) {
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

            'onClick': function(argsObj) {
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

        $('#ctl00_ContentPlaceHolder1_txtdate_op').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_txtdate_report').calendarsPicker(
			{
			    maxDate: date_str,
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

    function openPopup(flag, popupType) {
        if (flag.checked) {
            is_sms = 1;
        } else {
            is_sms = 0;
        }
        loadPopup(1);
        my_window = window.open('popup_recepient.aspx?popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=600');
    }

    function openPopupSMS() {
        loadPopup(1);
        my_window = window.open('popup_sms.aspx', '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=600');
    }

    function IAmSelected(source, eventArgs) {
        // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
        $("#ctl00_ContentPlaceHolder1_txtmdcode").attr("disabled", false);
        $("#ctl00_ContentPlaceHolder1_txtmdcode").val(eventArgs.get_value());
    }

    function IAmSelected2(source, eventArgs) {
        // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
        // $("#ctl00_ContentPlaceHolder1_txtmdcode").attr("disabled", false);
        $("#ctl00_ContentPlaceHolder1_txtpsm_add_special").val(eventArgs.get_value());
    }

    function openConcern(concern_id) {
        window.open('../cfb/mco_popup.aspx?irId=<%response.write(irId) %>&concern_id=' + concern_id, '', 'alwaysRaised,scrollbars =yes,status=yes,width=800,height=600');
    }

    function data_change(field) {
        var check = true;
        var value = field.value; //get characters
        //check that all characters are digits, ., -, or ""
        for (var i = 0; i < field.value.length; ++i) {
            var new_key = value.charAt(i); //cycle through characters
            if (((new_key < "0") || (new_key > "9")) &&
                    !(new_key == "")) {
                check = false;
                break;
            }
        }
        //apply appropriate colour based on value
        if (!check) {
            field.style.backgroundColor = "red";
        }
        else {
            field.style.backgroundColor = "white";
        }
    }

     $(document).ready(function () { 
      checkSession("<%response.write(session("bh_username").toString) %>" , "<%response.write(viewtype) %>"); // Check session every 1 sec.
    });
</script>
<style type="text/css" media="screen">
/*
input[disabled="disabled"], input.disabled
 { background-color:Yellow; font-weight:bold ; color:Red }
 */
    .style2
    {
        height: 34px;
    }
    .style3
    {
        height: 22px;
    }
    .auto-style1 {
        height: 21px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer"  />
</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   
    <table width="100%" cellpadding="2" cellspacing="1">
          <tr>
            <td><img src="../images/menu_01.png" width="32" height="32" hspace="5"   />&nbsp;&nbsp;<span class="style1">Accident & Incident Reports : 
            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></span>
            &nbsp;  
            
            <asp:Label ID="lblForm" runat="server" Text="Change form type" Visible="false"></asp:Label>    &nbsp; 
            <asp:DropDownList ID="txtchangeForm" runat="server" Visible="false">
               <asp:ListItem  Value="">-- Please select--</asp:ListItem>
               <asp:ListItem  Value="5">General Incident</asp:ListItem>
               <asp:ListItem  Value="1">Fall Occurrence</asp:ListItem>
               <asp:ListItem  Value="2">Medication Error</asp:ListItem>
               <asp:ListItem  Value="3">Phlebitis/ Infiltration</asp:ListItem>
               <asp:ListItem  Value="4">Anesthesia Event</asp:ListItem>
                <asp:ListItem  Value="6">Pressure Ulcer</asp:ListItem>
               <asp:ListItem  Value="7">Delete Patient Record</asp:ListItem>
              </asp:DropDownList>
                <asp:Button ID="cmdChangeForm" runat="server" Text="Change Form" OnClientClick="return confirm('Are you sure you want to change form?')" Visible ="false" />
              
                <asp:Button ID="cmdPrint" runat="server" Text="Print Form" 
                    CausesValidation="False" />
               
            </td>
             
          </tr>
        </table>
    
    <div align="right">
    <asp:Panel ID="Panel1" runat="server" Width="400px" HorizontalAlign="Center">
        <asp:Button ID="cmdRestore" runat="server" Text="Resotre from cancelled status" OnClientClick="return confirm('Are you sure you want to restore this case?')" Visible="false" />
     <asp:Button ID="cmdDraft2" runat="server" Text="Save draft" OnCommand="onSave" 
            CommandArgument="1" ForeColor="#FF3300" CausesValidation="False" />
    <asp:Button ID="cmdSubmit2" runat="server"  Text="Submit" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True"  />
    <asp:Button ID="cmdTQMDraft2" runat="server" Text="Save draft" OnCommand="onSave" 
            CommandArgument="" ForeColor="#FF3300"  CausesValidation="False" /> 
    <asp:Button ID="cmdTQMView2" runat="server" Text="Save revision"  ForeColor="#000099" Font-Bold="True" CausesValidation="False"  /> 
    <asp:Button ID="cmdTQMClose2" runat="server"  Text="Close incident" OnCommand="onSave" 
            CommandArgument="9" Font-Bold="True" CausesValidation="False"  />
    <asp:Button ID="cmdDeptReturn2" runat="server"  Text="Save and return case to TQM" OnCommand="onSave" 
            CommandArgument="7" Font-Bold="True" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to return case to TQM?');"  />
     <asp:Button ID="cmdPSMReturn2" runat="server"  Text="Save and return case to TQM" OnCommand="onSave" 
            CommandArgument="8" Font-Bold="True" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to return case to TQM?');"  />         
</asp:Panel> <asp:Button ID="cmdReopen2" runat="server"  Text="Re-Open"  
             Font-Bold="True" Visible="false" CausesValidation="False"  />
              <asp:Button ID="cmdMCO" runat="server"  Text="Receive Case"  
             Font-Bold="True" Visible="false" CausesValidation="False" />
            
        <asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="Panel1" VerticalSide="Top" VerticalOffset="10" HorizontalSide="Right">
        </asp:AlwaysVisibleControlExtender>
       </div>
<div id="data">
<table width="100%" cellspacing="3" cellpadding="3">
  <tr>
    <td><strong>
      <asp:Label ID="lblIRdetail1" Text="Incident No." runat="server" />      
      :
    </strong> 
    <asp:Label ID="txtirno" runat="server" Width="150px" 
            Font-Bold="True" BorderStyle="Inset" BackColor="#EECB85" BorderWidth="2px" BorderColor="#E3E3E3"> </asp:Label>
  &nbsp;
  <strong>
  <asp:Label ID="lblIRdetail2" Text="Department" runat="server" />   
  :</strong>
  <asp:TextBox  ID="txtdivision" runat="server" Width="235px" 
            ReadOnly="True" BackColor="#EECB85" Font-Bold="True" ForeColor="Blue"></asp:TextBox>
  <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/DivisionService.asmx" ServiceMethod="getDivision" CompletionSetCount="5" 
            TargetControlID="txtdivision"  EnableViewState="false"> </asp:AutoCompleteExtender></td>
    <td align="right"> <strong>Status :</strong>
      <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="ir_status_name" DataValueField="ir_status_id" Font-Bold="True" 
                    BackColor="#C87F7B" ForeColor="Blue" Enabled="False">
      </asp:DropDownList>
                <asp:DropDownList ID="txtlang" runat="server" AutoPostBack="True">
                <asp:ListItem Value="th" >TH</asp:ListItem>
                <asp:ListItem Value="en">EN</asp:ListItem>
                
                </asp:DropDownList></td>
    </tr>
</table>
<div class="remark" id="lblInform" runat="server">
  <img src="../images/sign_explain.png" alt="ir" width="16" height="16"  /> Send 
    report to IR&CFB within 24 hours after occurrence (Sent to supervisor after office hour and immediately report if serious clinical occurence)
</div>
<div class="tabber" id="mytabber2">
<div class="tabbertab" id="incident_alert" runat="server">
        <h2>Incident Alert</h2>
        <table width="100%" cellspacing="1" cellpadding="2">
  <tr>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="100" valign="top">
          <input type="button" name="button3" id="button1" value="Address" style="width: 85px;" onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') ,  'to')" />
        </td>
        <td valign="top"><input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" />
            <asp:Button ID="cmdSendMail" runat="server" Text=" Send " CausesValidation="False" /></td>
      </tr>
       <tr>
                    <td valign="top" style="text-align:right" > 
                        <input id="btnCC" name="button9" 
               onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') , 'cc')" 
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
            <asp:DropDownList ID="txtsubject" runat="server" Width="641px" 
                AutoPostBack="True">
            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="4">Need Dept Mgr/ Director Investigation</asp:ListItem>
            <asp:ListItem Value="5">Need Medical Support Investigation</asp:ListItem>
            <asp:ListItem Value="100">Sentinel event, Serious outcome</asp:ListItem>
            <asp:ListItem Value="101">Information Updated</asp:ListItem>
            <asp:ListItem Value="102">Case Follow-up</asp:ListItem>
            </asp:DropDownList>
          
                                        <asp:CheckBox ID="chk_priority" runat="server" Text="High Priority" Font-Bold="True" ForeColor="Red" />

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
<div class="tabbertab" id="incident_log" runat="server">
        <h2>Incident Alert log <asp:Label ID="lblAlertLogNum" runat="server" Text="(0)" /></h2>
    <asp:GridView ID="GridAlertLog" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            Width="90%" DataKeyNames="log_alert_id" CellSpacing="1" 
            EnableModelValidation="True">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="alert_date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="alert_method" HeaderText="Method" />
            <asp:BoundField DataField="subject" HeaderText="Subject" />
            <asp:BoundField DataField="send_to" HeaderText="Recepient" />
            <asp:BoundField DataField="cc_to" HeaderText="CC" />
            <asp:BoundField DataField="bcc_to" HeaderText="BCC" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
       
        <br /></div>
  <div class="tabbertab">
    <h2>Incident Attachment <asp:Label ID="lblAttachNum" runat="server" Text="(0)" /></h2>
  <table>
    <tr>
      <td colspan="2" valign="top">
      <table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td colspan="2" valign="top">
            <asp:FileUpload ID="FileUpload1" runat="server" Width="535" />
         <asp:Button ID="cmdUpload"
              runat="server" Text="Upload" CausesValidation="False" />         
         <asp:Button ID="cmdDeleteFile" runat="server" 
              Text="Delete selected attachments" CausesValidation="False" />         
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
                      <a href="../share/incident/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
          </asp:GridView></td>
    </tr>
  </table>
  <br />
</div>     
<div class="tabbertab">
  <h2>Accident & Incident Reports Review log <asp:Label ID="lblReviewLogNum" runat="server" Text="(0)" /></h2>
    <asp:GridView ID="GridviewIncidentLog" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="log_status_id" ShowHeader="False" 
        CellSpacing="1" CellPadding="2" AllowPaging="True" BorderWidth="0" 
        BorderStyle="None" ShowFooter="True">
               <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("status_id") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblLog" runat="server" Text='<%# Bind("log_remark") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblReportby" runat="server" Text="Reported by" Width="100px"></asp:Label>&nbsp;&nbsp;
                     <asp:TextBox ID="txtLogReportby" ReadOnly="true" runat="server" Text='<%# Bind("log_create_by") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblPosition" runat="server" Text="Position"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="txtLogPosition" ReadOnly="true" runat="server" Text='<%# Bind("position") %>'></asp:TextBox>
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblDept" runat="server" Text="Unit/Dept"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="txtLogDept" ReadOnly="true" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            
              <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblDateLog" runat="server" Text="Date"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="txtLogDate" ReadOnly="true" runat="server" Text='<%# Bind("log_time") %>'  DataFormatString="{0:dd MMM yyyy}"></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:BoundField />
        </Columns>
    </asp:GridView>
  
  <br />
</div>

<div class="tabbertab">
  <h2>Revision <asp:Label ID="lblRevisionNum" runat="server" Text="(0)" /></h2>
     <asp:GridView ID="GridRevision" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" EnableModelValidation="True">
            <Columns>
                  <asp:BoundField DataField="revision_date" HeaderText="Revision Date">
                  <ItemStyle Width="150px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="revision_by_name" HeaderText="Updated by">
                  <ItemStyle Width="200px" />
                  </asp:BoundField>
                  <asp:TemplateField HeaderText="PDF Revision">
                    
                      <ItemTemplate>
                        
                        <a href='../share/incident/revision/<%# Eval("file_name") %>.pdf' 
                              target="_blank">
                          <asp:Label ID="Label1" runat="server" > <%# Container.DataItemIndex + 1 %>.</asp:Label>
                          </a>
                      </ItemTemplate>
                    
                  </asp:TemplateField>
              </Columns>
          </asp:GridView>
  </div>

      <div class="tabbertab" runat="server" id="div_convert" visible="false" >
  <h2><strong><span style="color:blue">Incident Form Management</span></strong></h2>
           <fieldset style="width:70%">
               <legend><strong>Convert to Incident</strong></legend>
                  <table  cellspacing="1" cellpadding="3" width="100%">
                <tr>
                    <td >
                         <asp:Button ID="cmdConvert" runat="server" Text="Move Incident to CFB Report" CausesValidation="False" Width="210px" OnClientClick="return confirm('Are you sure you want to convert to CFB Report ?');" />
                       
                        &nbsp;ย้ายจาก Accident & Incident Reports เป็น CFB Report
                        
                    &nbsp;</td>

                </tr>              
                <tr>
                    <td > <asp:Button ID="cmdCopyToCFB" runat="server" Text="Copy Incident to CFB Report" CausesValidation="False" Width="210px" OnClientClick="return confirm('Are you sure you want to copy to Incident Form ?');" />     Copy Accident & Incident Reports เป็น CFB Report
                         &nbsp;</td>
                </tr>        
                <tr>
                    <td >  New Incident Form Status    <asp:RadioButtonList ID="txtconvertstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">Current Incident Status</asp:ListItem>
                            <asp:ListItem Value="2">Draft Status</asp:ListItem>
                        </asp:RadioButtonList>
                         &nbsp;</td>
       
                </tr>
           
               
                </table>
           </fieldset>
         <br />
            <fieldset style="width:70%">
                <legend><strong>incident Form Management</strong></legend>
                   <table  cellspacing="1" cellpadding="3">
                     <tr>
                    <td >
                         New Form Type
            <asp:DropDownList ID="txtchangeFormCopy" runat="server" >
              
               <asp:ListItem  Value="5">General Incident</asp:ListItem>
               <asp:ListItem  Value="1">Fall Occurrence</asp:ListItem>
               <asp:ListItem  Value="2">Medication Error</asp:ListItem>
               <asp:ListItem  Value="3">Phlebitis/ Infiltration</asp:ListItem>
               <asp:ListItem  Value="4">Anesthesia Event</asp:ListItem>
                <asp:ListItem  Value="6">Pressure Ulcer</asp:ListItem>
               <asp:ListItem  Value="7">Delete Scan</asp:ListItem>
              </asp:DropDownList></td>

                    <td >&nbsp;</td>

                  

                </tr>
                     <tr>
                    <td >
                        <asp:Button ID="cmdChangeToDraft" runat="server" Text="Change this IR to Draft" CausesValidation="False" Width="210px" ForeColor="Red" OnClientClick="return confirm('Are you sure you want to change status to Draft ?');" />
                    </td>

                    <td >เปลี่ยนสถานะเป็น Draft เพื่อให้ผู้รายงาน Submit เข้ามาใหม่ได้อีกครั้ง (ใช้ IR No. เดิม)
                        &nbsp;</td>

                  

                </tr>
                <tr>
                    <td >
                        <asp:Button ID="cmdCopy" runat="server" Text="Copy data to new IR" CausesValidation="False" Width="210px"  OnClientClick="return confirm('Are you sure you want to copy to newly form ?');" />
                    </td>

                    <td > 
                      
                       ได้ IR No. ใหม่</td>

                   

                </tr>

            </table>
                </fieldset>
         <br />
            <fieldset style="width:70%">
                <legend><strong>Lock/Unlock Form</strong></legend>
                  <asp:RadioButtonList ID="txtlock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True">
                             <asp:ListItem Value="1">Lock this form</asp:ListItem>
                             <asp:ListItem Value="0" Selected="True">Unlock this form</asp:ListItem>
                         </asp:RadioButtonList><br />
                  <asp:Label ID="lblLockMsg" runat="server" ForeColor="Red"></asp:Label>
            </fieldset>
            <br />
            <br />
            </div>
          </div>
<div class="tabber" id="mytabber1">
  <div class="tabbertab">
        <h2>Incident Details</h2>

<fieldset id="fs_detail" runat="server" class="disabled">
<asp:Panel ID="panel_ir_detail" runat="server" >
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Visible="True" ShowMessageBox="True" ShowSummary="False" />
<table width="100%" cellspacing="1" cellpadding="2" >
   <tr>
     <td width="200" valign="top"><asp:Label ID="lblIRdetail22" Text="Describe the occurrence" runat="server" /> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtoccurrence" Display="Dynamic" 
            ErrorMessage="กรุณาระบุข้อมูลสรุปเหตุการณ์" ></asp:RequiredFieldValidator>
       </td>
     <td><label>
       <textarea name="txtoccurrence" id="txtoccurrence" cols="45" rows="10" 
             style="width: 735px" runat="server"></textarea>
       </label></td>
   </tr>
  <tr>
    <td valign="top" > 
        </td>
    <td ><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
        <tr>
          <td>
              <fieldset style="padding:10px"><legend><asp:Label ID="lblIRdetail21" Text="Unit defendant" runat="server" Font-Bold="true" /></legend>
                  <br />
           <asp:GridView ID="GridDept" runat="server" AutoGenerateColumns="False" 
                  CellPadding="4"
              Width="735px" ShowHeader="False" DataKeyNames="relate_dept_id" 
                  ForeColor="#333333" GridLines="None" 
                  EmptyDataText="กรุณาระบุแผนกที่เกี่ยวข้อง / Please enter relevant unit" 
                  EnableModelValidation="True">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
              <Columns>
                 <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit name">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("relate_dept_id") %>' Visible="false"></asp:Label>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
               
                  <asp:TemplateField>
                   
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
               
                  <asp:TemplateField ShowHeader="False">
                      <ItemStyle Width="80px" />
                      <ItemTemplate>
                          <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" ForeColor="red"
                              CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>
                      </ItemTemplate>
                      <ItemStyle Width="80px" />
                  </asp:TemplateField>
              </Columns>
               <EmptyDataRowStyle BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                   ForeColor="#FF3300" />
               <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <EditRowStyle BackColor="#999999" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          </asp:GridView>
           <br />
        <table width="700">
            <tr><td class="auto-style1">เลือกแผนกที่ต้องการ / Select Department</td></tr>

            

        </table>
              <asp:DropDownList ID="txtadd_dept" runat="server" Width="693px" 
                  DataTextField="dept_name_en" DataValueField="dept_id">
              </asp:DropDownList>
              <asp:Button ID="cmdAddDept" runat="server" Text="Add" Width="45px" 
                  CausesValidation="False" />
                  </fieldset>
           </td>
          </tr>
        
      </table></td>
  </tr>
 <tr>
    <td valign="top"><asp:Label ID="lblIRdetail20" Text="Location (unit/room)" runat="server" /> 
        </td>
    <td>
        
         <asp:DropDownList ID="txtlocation_combo" runat="server" DataTextField="location" DataValueField="location" onchange="$('#ctl00_ContentPlaceHolder1_txtlocation').val(this.value);" ></asp:DropDownList>
        <asp:TextBox ID="txtlocation" runat="server" Width="135px" 
            ToolTip="Auto complete"  BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" Visible="False" />
        
         <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/LocationService.asmx" ServiceMethod="getLocation" CompletionSetCount="5" 
            TargetControlID="txtlocation"  EnableViewState="false">
        </asp:AutoCompleteExtender><br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtlocation" Display="Dynamic" 
            ErrorMessage="กรุณาระบุสถานที่เกิดเหตุ" ></asp:RequiredFieldValidator>
       
        </td>
  </tr>
      <tr>
                 <td valign="top">Response Unit</td>
                 <td>
                     <asp:DropDownList ID="txtresponse_unit" runat="server" DataTextField="location" DataValueField="location" >
                     </asp:DropDownList>
                     <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ControlToValidate="txtresponse_unit" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ Response Unit" ></asp:RequiredFieldValidator>
                 </td>
             </tr>
    <tr>
      <td valign="top"><asp:Label ID="lblIRdetail16" Text="Date of occurence/Report" runat="server" /> </td>
      <td><table width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td width="235"><input type="text" name="txtdate_report" id="txtdate_report" style="width: 170px; background: #0F0;" runat="server" />
          <br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtdate_report" Display="Dynamic" 
            ErrorMessage="กรุณาระบุวันที่เกิดเหตุ" ></asp:RequiredFieldValidator> </td>
          <td width="50"><asp:Label ID="lblIRdetail17" Text="Time" runat="server" /> </td>
          <td width="130">
              <asp:DropDownList ID="txthour" runat="server">
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin" runat="server">
              </asp:DropDownList>
          </td>
          <td width="250" style="font-weight: bold; color: red;"><asp:Label ID="lblIRdetail18" Text="Sentinel event, Serious event outcome" runat="server" /> </td>
          <td width="23"><input type="checkbox" name="txtserious" id="txtserious" runat="server" onclick="return confirm('Are you sure this incicent is Sentinel event?')" /></td>
          <td><asp:Label ID="lblIRdetail19" Text="Yes" runat="server" />  &nbsp;<a href="#"><strong>[ ? ]</strong></a></td>
        </tr>
      </table></td>
    </tr>
      <tr>
    <td colspan="2"><hr style="height: 2px; width: 98%; text-align: left;" /></td>
  </tr>
  <tr>
    <td width="200" valign="top">
          <asp:Label ID="lblIRdetail3" Text="Patient/Person affected" runat="server" />        
</td>
    <td>
    <table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="250">
            <asp:DropDownList ID="txttitle" runat="server" Width="50px">
                      <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
             <asp:ListItem Value="Master">Master</asp:ListItem>
             <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
           
             <asp:ListItem Value="นาง">นาง</asp:ListItem>
            <asp:ListItem Value="น.ส.">น.ส.</asp:ListItem>
            <asp:ListItem Value="นาย">นาย</asp:ListItem>
             <asp:ListItem Value="ด.ช.">ด.ช.</asp:ListItem>
             <asp:ListItem Value="ด.ญ.">ด.ญ.</asp:ListItem>                     
             <asp:ListItem Value="">No title</asp:ListItem>
            </asp:DropDownList>
        <input type="text" name="txtptname" id="txtptname" 
                style="width: 155px" runat="server" />
            </td>
        <td width="30">
            <asp:Label ID="lblIRdetail4" Text="Age" runat="server" />          
</td>
        <td width="75"><input type="text" name="txtptage" id="txtptage" 
                style="width: 30px" runat="server" />
                        
            <asp:Label ID="lblIRdetail5" Text="yrs" runat="server" /> 
              <br /><asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                runat="server" ControlToValidate="txtptage"
ErrorMessage="กรุณาระบุอายุเป็นตัวเลขเท่านั้น"  ValidationExpression="^\d+$" Display="Dynamic" ></asp:RegularExpressionValidator> 
</td>
        <td width="40">
            <input type="text" name="txtptxx" id="txtpt_monthday" 
                style="width: 30px" runat="server" />
          </td>
        <td width="80">
            &nbsp;<asp:DropDownList ID="txtpt_selectmonthday" runat="server" Width="65px">
                <asp:ListItem>เดือน</asp:ListItem>
                <asp:ListItem>วัน</asp:ListItem>
            </asp:DropDownList>
          </td>
        <td width="30">
            <asp:Label ID="lblIRdetail6" runat="server" Text="Gender" />
          </td>
        <td width="100">
            <asp:DropDownList ID="txtptsex" runat="server" Width="60px">
                <asp:ListItem Value="">Not Specific</asp:ListItem>
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
            </asp:DropDownList>
                </td>
        <td width="75">
            <asp:Label ID="lblIRdetail7" runat="server" Text="HN" />
          </td>
        <td>
            <asp:TextBox ID="txtpthn" runat="server" Width="80px" MaxLength="9"></asp:TextBox>
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" 
                Mask="999999999" MaskType="Number" MessageValidatorTip="true" 
                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                PromptCharacter="*" TargetControlID="txtpthn" />
       
       
        </td>
        </tr>
      </table></td>
  </tr>
      <tr>
        <td valign="top" width="200">
            <asp:Label ID="lblIRNation" runat="server" Text="Nationality1"></asp:Label>
            </td>
        <td>
            
            <asp:TextBox ID="txtcountry" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top" width="200">
            <asp:Label ID="lblIRdetail8" runat="server" Text="Unit/Room" />
        </td>
        <td>
            <asp:TextBox ID="txtroom" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td valign="top" width="200">
            <asp:Label ID="lblIRdetail44" runat="server" Text="Type of service" />
        </td>
        <td>
            <table width="100%">
                <tr>
                    <td width="200">
                        <asp:DropDownList ID="txtservicetype" runat="server">
                            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                            <asp:ListItem Value="OPD">OPD</asp:ListItem>
                            <asp:ListItem Value="IPD">IPD</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="130">
                        <asp:Label ID="lblIRdetail9" runat="server" Text="Customer Segment" />
                    </td>
                    <td>
                        <asp:DropDownList ID="txtsegment" runat="server">
                            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                            <asp:ListItem Value="1">Thai</asp:ListItem>
                            <asp:ListItem Value="2">Expatriate</asp:ListItem>
                            <asp:ListItem Value="3">International</asp:ListItem>
                              <asp:ListItem Value="4">Unknown</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ControlToValidate="txtsegment" Display="Dynamic" 
            ErrorMessage="กรุณาระบุประเภทลูกค้า (Customer Segment)" ></asp:RequiredFieldValidator>
                    </td>
                    <td width="100">
                        Clinical type</td>
                    <td>
                        <asp:DropDownList ID="txtclinical" runat="server">
                            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                            <asp:ListItem Value="1">Clinical</asp:ListItem>
                            <asp:ListItem Value="2">Non-Clinical</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
            ControlToValidate="txtclinical" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ Clinical type" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  <%If (mode = "edit") Then%>

  <tr>
                  <td valign="top"><asp:Label ID="lblIRdetail10" Text="Repeated incident history" runat="server" /> </td>
                  <td>
                  <div class="warning" id="incidentWarn" runat="server"></div>
                      <asp:GridView ID="GridRepeatIncident" runat="server" AutoGenerateColumns="False" 
                          CellPadding="4" Width="100%" ForeColor="#333333" GridLines="None" 
                          EnableModelValidation="True">
                          <RowStyle BackColor="#E3EAEB" />
                          <Columns>
                              <asp:BoundField DataField="datetime_report" HeaderText="Date of occurrence " 
                                  SortExpression="date" DataFormatString="{0:d MMMM yyyy}" />
                              <asp:TemplateField HeaderText="IR No.">
                                 
                                  <ItemTemplate>
									   <%If (authorized_access_linkclick) Then %>
										<a href="form_incident.aspx?mode=edit&irId=<%# Eval("ir_id") %>&formId=<%# Eval("form_id") %>"><asp:Label ID="Label1" runat="server" Text='<%# Bind("ir_no") %>'></asp:Label></a>
									  <%Else%>
										<asp:Label ID="Label9" runat="server" Text='<%# Bind("ir_no") %>'></asp:Label>
									  <% End If %>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="form_name_en" HeaderText="Incident Topic" />
                              <asp:BoundField DataField="severe_name" HeaderText="Severity level" />
                          </Columns>
                          <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                          <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                          <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                          <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                          <EditRowStyle BackColor="#7C6F57" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                </td>
                    
              </tr>
  <%End If%>
  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail11" Text="Status of affecfed person" runat="server" /> </td>
    <td>
        <asp:RadioButtonList ID="txtpttype" runat="server" CellSpacing="2" 
            RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="2">
            <asp:ListItem Value="1">Outpatient</asp:ListItem>
            <asp:ListItem Value="2">Inpatient</asp:ListItem>
            <asp:ListItem Value="3">Employee</asp:ListItem>
            <asp:ListItem Value="4">Visitor</asp:ListItem>
            <asp:ListItem Value="5">Other (please identify)</asp:ListItem>
        </asp:RadioButtonList>&nbsp;<input type="text" name="txtstatusother" id="txtstatusother" style="width: 150px" runat="server" />
        </td>
  </tr>

  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail12" Text="Diagnosis" runat="server" /> </td>
    <td><input type="text" name="txtdiagnosis" id="txtdiagnosis" style="width: 735px" runat="server" onfocus="this.select()" title="Auto complete" />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/spellcheck.png" />
      </td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail13" Text="Operation/Procedure" runat="server" /> </td>
    <td><input type="text" name="txtoperation" id="txtoperation" style="width: 735px" runat="server"  /></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail14" Text="Date of Operation/Procedure" runat="server" /> </td>
    <td><input type="text" name="txtdate_op" id="txtdate_op" style="width: 170px; background: #0F0;" runat="server"/>
      </td>
  </tr>
    <tr>
      <td valign="top"><asp:Label ID="lblIRdetail15" Text="Attending Physician" runat="server" /> 
          </td>
      <td>
          <asp:TextBox ID="txtatt_doctor" runat="server" Width="735px" ToolTip="Auto complete"></asp:TextBox>
               <asp:Image ID="Image3" runat="server" 
    ImageUrl="~/images/spellcheck.png" />
         
      </td>
    </tr>
  
  
  
  
  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail23" Text="Notified after incidence" runat="server" /> </td>
    <td><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><input type="checkbox" name="txtnoti1" id="txtnoti1" runat="server" /></td>
        <td width="150"><asp:Label ID="lblIRdetail24" Text="Attending physician" runat="server" /> </td>
        <td width="23"><input type="checkbox" name="txtnoti2" id="txtnoti2" runat="server" /></td>
        <td width="130"><asp:Label ID="lblIRdetail25" Text="Family member" runat="server" /> </td>
        <td width="23"><input type="checkbox" name="txtnoti3" id="txtnoti3" runat="server" /></td>
        <td><asp:Label ID="lblIRdetail26" Text="Documentation on the file" runat="server" /> </td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td colspan="2"><hr style="height: 2px; width: 98%; text-align: left;" /></td>
  </tr>
  <tr>
    <td colspan="2" valign="top"></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblIRaction1" Text="Describe the initial action" runat="server" /></td>
    <td><textarea name="txtinitial" id="txtinitial" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
  </tr>
  <tr>
    <td valign="top"><span style="text-decoration: underline">
      <asp:Label ID="lblIRaction2" Text="Result of action" runat="server" />      
</span></td>
    <td><asp:Label ID="lblIRaction3" Text="Patient" runat="server" /><label for="select2"></label>
       <asp:DropDownList ID="txtresult_action" runat="server">
                <asp:ListItem Value="1">Satisfied and not requested to respond back</asp:ListItem>
                <asp:ListItem Value="2">Satisfied and requested to respond back</asp:ListItem>
                <asp:ListItem Value="3">Not applicable</asp:ListItem>
                <asp:ListItem Value="4">Unsatisfied (please identify)</asp:ListItem>
                </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td><textarea name="txtactionremark" id="txtactionremark" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblIRdetail41" Text="Severity injury affect to " runat="server" />
      <br />
      <span style="text-decoration: underline"><asp:Label ID="lblIRdetail42" Text="Client" runat="server" /></span></td>
    <td>
        <asp:RadioButtonList ID="txtsevere" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow" CellPadding="5" 
            CellSpacing="5">
             <asp:ListItem Value="10" >Near Miss</asp:ListItem>
            <asp:ListItem Value="1" >No apparent injury</asp:ListItem>
            <asp:ListItem Value="2">Minor (needs observation)</asp:ListItem>
            <asp:ListItem Value="3">Moderate (needs further treatment)</asp:ListItem>
            <asp:ListItem Value="4">Severe (result in extended hospital stay or transfer to critical care)</asp:ListItem>
            <asp:ListItem Value="5">Death</asp:ListItem>
        </asp:RadioButtonList><br />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
            ControlToValidate="txtsevere" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ Risk Level (ผลการประเมิน)" ></asp:RequiredFieldValidator>
    
      </td>
  </tr>
  <tr>
    <td valign="top"> <asp:Label ID="lblIRdetail43" Text="Other" runat="server" /></td>
    <td>
    <asp:RadioButtonList ID="txteffect" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" >No Effect</asp:ListItem>
        <asp:ListItem Value="2">Effect to  </asp:ListItem>
          </asp:RadioButtonList>&nbsp;<input type="text" name="txteffect_detail" id="txteffect_detail" style="width: 285px" runat="server" />
    </td>
  </tr>
     <tr>
                 <td  valign="top">Patient Safety Goal</td>
                 <td>
                     <asp:TextBox ID="txtsafegoal" runat="server" Width="285px"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td valign="top">By Payor</td>
                 <td>  
                      <asp:DropDownList ID="txtpayor" runat="server">
                         <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                         <asp:ListItem Value="Self pay">Self pay</asp:ListItem>
                         <asp:ListItem Value="Insurance">Insurance</asp:ListItem>
                         <asp:ListItem Value="Embassy">Embassy</asp:ListItem>
                         <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                     
                     </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="txtpayor" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ By Payor" ></asp:RequiredFieldValidator>
                 </td>
             </tr>
         <tr>
                 <td valign="top">Issue</td>
                 <td>
                     <asp:DropDownList ID="txtissue" runat="server">
                         <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                         <asp:ListItem Value="AMA">AMA</asp:ListItem>
                         <asp:ListItem Value="Cancel / Postpone procedure">Cancel / Postpone procedure</asp:ListItem>
                         <asp:ListItem Value="Facility & Environment">Facility & Environment</asp:ListItem>
                         <asp:ListItem Value="Fall">Fall</asp:ListItem>
                         <asp:ListItem Value="Finance">Finance</asp:ListItem>
                         <asp:ListItem Value="Hand off communication">Hand off communication</asp:ListItem>
                         <asp:ListItem Value="Imaging">Imaging</asp:ListItem>
                         <asp:ListItem Value="Infection Control">Infection Control</asp:ListItem>
                         <asp:ListItem Value="Laboratory">Laboratory</asp:ListItem>
                         <asp:ListItem Value="Medication Error">Medication Error</asp:ListItem>
                         <asp:ListItem Value="Medical Record">Medical Record</asp:ListItem>
                         <asp:ListItem Value="Medication Management">Medication Management</asp:ListItem>
                         <asp:ListItem Value="Patients Identification">Patient's Identification</asp:ListItem>
                         <asp:ListItem Value="Patient records">Patient records </asp:ListItem>
                         <asp:ListItem Value="Pre-op Process">Pre-op Process</asp:ListItem>
                         <asp:ListItem Value=">Refer out">Refer out</asp:ListItem>
                         <asp:ListItem Value=">Safety / Security">Safety / Security</asp:ListItem>
                         <asp:ListItem Value="Service Manner">Service Manner</asp:ListItem>
                         <asp:ListItem Value="Treatment / Outcome">Treatment / Outcome</asp:ListItem>
                         <asp:ListItem Value="Waiting time">Waiting time</asp:ListItem>
                         <asp:ListItem Value="Other">Other</asp:ListItem>
                     </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="txtpayor" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ Issue" ></asp:RequiredFieldValidator>
                 </td>
             </tr>
           
     <tr>
    <td colspan="2"><hr style="height: 2px; width: 98%; text-align: left;" /></td>
  </tr>
  
   
    <tr>
        <td valign="top">
              <asp:Label ID="lblIRReportBy" runat="server" Text="Report by"></asp:Label> &nbsp;</td>
        <td>
            <asp:Label ID="lblReport" runat="server" ForeColor="#0033CC"></asp:Label>  &nbsp;</td>
    </tr>
    
    <tr>
        <td valign="top">
            <asp:Label ID="IRlblCallback" runat="server" Text="Contact Number"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtcontact" runat="server" Width="285px"></asp:TextBox>
        </td>
    </tr>

  </table>
  </asp:Panel>
  
      </fieldset>
 
 <br /></div>
     
        <div class="tabbertab">
        <h2>Physician Assessment</h2>
        
        <fieldset>
         <asp:Panel ID="panel_additional" runat="server" >
           <table width="100%" cellpadding="2" cellspacing="1" >
             <%If (mode = "edit") Then%>
             <%End If%>
             <tr>
               <td colspan="2" valign="top"><span class="red">
                 <asp:Label ID="lblIRdetail27" Text="In case of needed assessment by physician (please identify)" runat="server" />           
               </span></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail28" Text="Physician's name" runat="server" /></td>
               <td><table width="100%" cellspacing="0" cellpadding="0">
                 <tr>
                   <td width="400"><asp:TextBox ID="txtdoctor" runat="server" Width="360px" ToolTip="Auto complete" ></asp:TextBox>
                     <asp:Image ID="Image4" runat="server" 
    ImageUrl="~/images/spellcheck.png" /></td>
                   <td><asp:RadioButtonList ID="txtdoctype" runat="server" 
                RepeatDirection="Horizontal">
                     <asp:ListItem Value="1">On call physician</asp:ListItem>
                     <asp:ListItem Value="2" >Attending physician</asp:ListItem>
                     <asp:ListItem Value="3">Consultant</asp:ListItem>
                   </asp:RadioButtonList></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail29" Text="Date of assessment" runat="server" /></td>
               <td><table width="100%" cellspacing="0" cellpadding="0">
                 <tr>
                   <td width="235"><input type="text" name="txtdate_assessment" id="txtdate_assessment" style="width: 170px; background: #0F0;" runat="server"/></td>
                   <td width="50"><asp:Label ID="lblIRdetail30" Text="Time" runat="server" /></td>
                   <td><asp:DropDownList ID="txthour2" runat="server"> </asp:DropDownList>
                     :
                     <asp:DropDownList ID="txtmin2" runat="server"> </asp:DropDownList></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail31" Text="Describe the assessment" runat="server" /></td>
               <td><input type="text" name="txtassessment" id="txtassessment" style="width: 735px" runat="server" /></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail32" Text="Treatment" runat="server" /></td>
               <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                 <tr>
                   <td width="23"><input type="checkbox" name="txtxray" id="txtxray" runat="server" value="1" /></td>
                   <td width="100"><asp:Label ID="lblIRdetail33" Text="X-Ray" runat="server" /></td>
                   <td width="280"><input type="text" name="txtxray_detail" id="txtxray_detail" style="width: 265px" runat="server" /></td>
                   <td width="47"><asp:Label ID="lblIRdetail34" Text="Result" runat="server" /></td>
                   <td><input type="text" name="txtxray_result" id="txtxray_result" style="width: 265px" runat="server" /></td>
                 </tr>
                 <tr>
                   <td><input type="checkbox" name="txtlab" id="txtlab" runat="server" /></td>
                   <td><asp:Label ID="lblIRdetail35" Text="Lab" runat="server" /></td>
                   <td><input type="text" name="txtlab_detail" id="txtlab_detail" style="width: 265px" runat="server" /></td>
                   <td><asp:Label ID="lblIRdetail36" Text="Result" runat="server" /></td>
                   <td><input type="text" name="txtlab_result" id="txtlab_result" style="width: 265px" runat="server" /></td>
                 </tr>
                 <tr>
                   <td><input type="checkbox" name="txtother" id="txtother" runat="server" /></td>
                   <td><asp:Label ID="lblIRdetail37" Text="Other (identify)" runat="server" /></td>
                   <td><input type="text" name="txtother_detail" id="txtother_detail" style="width: 265px" runat="server" /></td>
                   <td><asp:Label ID="lblIRdetail38" Text="Result" runat="server" /></td>
                   <td><input type="text" name="txtother_result" id="txtother_result" style="width: 265px" runat="server" /></td>
                 </tr>
               </table></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail39" Text="Recommendation from administrator (if applicable)" runat="server" /></td>
               <td valign="top"><textarea name="txtrecommend" id="txtrecommend" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
             </tr>
             <tr>
               <td valign="top"><asp:Label ID="lblIRdetail40" Text="Describe the action taken" runat="server" /></td>
               <td><textarea name="txtaction" id="txtaction" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
             </tr>
           </table>
           </asp:Panel>
           
</fieldset>

<br /></div>
<div class="tabbertab" id="tab_tqm" runat="server">
<h2>Part of IR&CFB</h2>
    <asp:Panel ID="panelTQM" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
   
    <asp:Panel ID="panelTQMSub1" runat="server">
<fieldset>
<a href="logbook.aspx" title="Log Book" >View IR&CFB log book</a>
   <asp:Button ID="cmdPrintTQM" runat="server" Text="Print Part of IR&CFB" 
                    CausesValidation="False" />
<br />
          <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" >
        <tr>
          <td width="80"><asp:Label ID="lblIRtqm1" Text="Grand Topic" runat="server" /></td>
          <td>
              <asp:DropDownList ID="txtgrandtopic" runat="server" 
                  DataTextField="grand_topic_name" DataValueField="grand_topic_id" 
                  Width="350px" AutoPostBack="True">              </asp:DropDownList>             </td>
          <td width="80">
              <asp:Label ID="lblIRtqm3" runat="server" Text="Subtopic 1" />
            </td>
          <td>  
              <asp:DropDownList ID="txtsubtopic1" runat="server" AutoPostBack="True" 
                  DataTextField="subtopic_name" DataValueField="ir_subtopic_id" Width="350px">
              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td>
              <asp:Label ID="lblIRtqm2" runat="server" Text="Topic" />
            </td>
          <td>
              <asp:DropDownList ID="txtnormaltopic" runat="server" AutoPostBack="True" 
                  DataTextField="topic_name" DataValueField="ir_topic_id" Width="350px">
              </asp:DropDownList>
            </td>
          <td><asp:Label ID="lblIRtqm4" Text="Subtopic 2" runat="server" /></td>
          <td><asp:DropDownList ID="txtsubtopic2" runat="server" 
                  DataTextField="subtopic2_name_en" DataValueField="ir_subtopic2_id" 
                  Width="350px" AutoPostBack="True">
              </asp:DropDownList></td>
        </tr>
          <tr>
              <td class="style3">
              </td>
              <td class="style3">
              </td>
              <td class="style3">
                  Subtopic 3</td>
              <td class="style3">
                  <asp:DropDownList ID="txtsubtopic3" runat="server" 
                      DataTextField="subtopic3_name_en" DataValueField="ir_subtopic3_id" Width="350">
                  </asp:DropDownList>
              </td>
          </tr>
          <tr>
              <td style="vertical-align:top">
                  Topic Detail</td>
              <td colspan="3">
                  <asp:TextBox ID="txttqm_detail" runat="server" TextMode="MultiLine" 
                      Width="650px"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td style="vertical-align:top">
                  Case Owner</td>
              <td colspan="3">
                   <asp:DropDownList ID="txtcomboowner" runat="server" Width="305px" AutoPostBack="True">
                                            <asp:ListItem Value="">-</asp:ListItem>
                                            <asp:ListItem Value="Burassakorn Tajama">Burassakorn Tajama</asp:ListItem>
                                            <asp:ListItem Value="Chonpitcha Barnyen">Chonpitcha Barnyen</asp:ListItem>
                                            <asp:ListItem Value="Sasithorn Sutinperk">Sasithorn Sutinperk</asp:ListItem>
                                          
                        <asp:ListItem Value="Wipamol Ruangsri">Wipamol Ruangsri</asp:ListItem> 
                        <asp:ListItem Value="Pattama Nitithanasombat">Pattama Nitithanasombat</asp:ListItem>
                                        </asp:DropDownList><br />
                  <asp:TextBox ID="txtcase_owner" runat="server" Width="300px" Rows="3" 
                      TextMode="MultiLine"></asp:TextBox>
                  &nbsp;Use comma (,) for multiple case owner</td>
          </tr>
      </table></td>
    </tr>
              <tr>
                  <td valign="top">
                  <table width="100%" cellspacing="0" cellpadding="0">
              <tr>
                <td width="352" valign="top">
                    <strong>Select department requires information update</strong></td>
                  <td valign="top" width="80">&nbsp;
                      </td>
                <td valign="top">
                    <strong>Acknowledge department</strong></td>
              </tr>
              <tr>
                  <td valign="top" width="352">
                      <asp:ListBox ID="txtinfo_dept1" runat="server" DataTextField="dept_name_en" 
                          DataValueField="dept_id" Width="250px"></asp:ListBox>
                  </td>
                  <td valign="top" width="80">
                      <asp:Button ID="cmdHRAdd" runat="server" Text="&gt;&gt;" />
                      <Br />
                      <asp:Button ID="cmdHrRemove" runat="server" Text="&lt;&lt;" />
                  </td>
                  <td valign="top">
                      <asp:ListBox ID="txtinfo_dept2" runat="server" DataTextField="dept_name_en" 
                          DataValueField="dept_id" Width="250px"></asp:ListBox>
                  </td>
              </tr>
            </table>
                    <br />
                    <table width="100%" cellspacing="0" cellpadding="0">
           

                  <tr>
                  <td valign="top" width="352">
                      <strong>Unit</strong></td>
                  <td valign="top" width="80">&nbsp;
                      </td>
                  <td valign="top">
                      <strong>Defendant Unit</strong></td>
              </tr>
                        <tr>
                            <td valign="top" width="352">
                                <asp:ListBox ID="txtunit_defandent_all" runat="server" 
                                    DataTextField="dept_unit_name" DataValueField="dept_unit_id" Width="250px">
                                </asp:ListBox>
                            </td>
                            <td valign="top" width="80">
                                <asp:Button ID="cmdUnitAdd" runat="server" Text="&gt;&gt;" />
                                <Br />
                                <asp:Button ID="cmdUnitRemove" runat="server" Text="&lt;&lt;" />
                            </td>
                            <td valign="top">
                                <asp:ListBox ID="txtunit_defandent_select" runat="server" 
                                    DataTextField="dept_unit_name" DataValueField="dept_unit_id" Width="250px">
                                </asp:ListBox>
                            </td>
                        </tr>
            </table><br />
                     <asp:GridView ID="GridTQMCause" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="tqm_cause_id" HeaderStyle-CssClass="colname" 
                          EnableModelValidation="True">
           <Columns>
               <asp:TemplateField HeaderText="No.">
               
               <ItemStyle Width="60px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblPk" runat="server" Text='<%# bind("tqm_cause_id") %>' Visible="false"></asp:Label>
                       <asp:TextBox ID="txtorder" runat="server"  Width="50px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="cause_name" 
                   HeaderText="Cause of Incident" >
                   <ItemStyle Width="350px" />
               </asp:BoundField>
               <asp:TemplateField HeaderText="Description">
                <ItemStyle Width="200px" />
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("cause_remark") %>'></asp:Label>
                   </ItemTemplate>
                 
               </asp:TemplateField>
               <asp:BoundField DataField="dept_unit_name" HeaderText="Unit Defendant" 
                    >
                 
               </asp:BoundField>
               <asp:TemplateField ShowHeader="False">
                   <ItemTemplate>
                       <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                           CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>
                   </ItemTemplate>
                   <ItemStyle Width="80px" />
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
          <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
  <%If GridTQMCause.Rows.Count = 0 Then%>
      <tr>
        <td valign="top" width="60" class="colname" style="font-weight:bold">No.</td>
        <td valign="top" width="350" class="colname" style="font-weight:bold">Cause of Incident</td>
        <td valign="top" width="200" class="colname" style="font-weight:bold">Description</td>
       <td valign="top" class="colname" style="font-weight:bold">Unit Dedendant</td>
        <td valign="top" class="colname" style="font-weight:bold" width="80"></td>
        </tr>
  <%end if %>
  <tr style="vertical-align:top"><td width="60"></td>
  <td  valign="top" width="350"> 
   <asp:DropDownList ID="txtcause" runat="server">
                                       <asp:ListItem Value="0">---- Please Select ----</asp:ListItem>
                                      <asp:ListItem Value="1">Personal / Human Error</asp:ListItem>
                                      <asp:ListItem Value="2">Communication</asp:ListItem>
                                      <asp:ListItem Value="3">System Error</asp:ListItem>
                                      <asp:ListItem Value="4">Equipment Error</asp:ListItem>
                                      <asp:ListItem Value="5">Enviroment</asp:ListItem>
                                      <asp:ListItem Value="6">Poor Practice Habit</asp:ListItem>
                                      <asp:ListItem Value="8">Patient’s factor</asp:ListItem>
                                      <asp:ListItem Value="7">Others</asp:ListItem>
                                  </asp:DropDownList></td>
  <td width="200">
      <asp:TextBox ID="txttqm_addremark" runat="server" Rows="3" TextMode="MultiLine" 
          Width="200px"></asp:TextBox>
      </td>
       <td  >           <asp:DropDownList ID="txtadd_cause_defendant" runat="server" 
                DataTextField="dept_unit_name" 
               DataValueField="dept_unit_id">
           </asp:DropDownList>
      </td>
  <td  width="80"></td>
  </tr>
  </table>
                    
                      <asp:Button ID="cmdTQMAddCause" runat="server" Text="Add Cause"  CausesValidation="False" />
                      <asp:Button ID="cmdTQMOrderCause" runat="server" Text="Save Order"  CausesValidation="False" />
                  </td>
              </tr>
    <tr>
      <td valign="top">&nbsp;</td>
    </tr>
              <tr>
                  <td valign="top"> <asp:GridView ID="gridTQMActionPlan" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="tdata" DataKeyNames="prevent_tqm_id" EnableModelValidation="True" HeaderStyle-CssClass="colname" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPk" runat="server" Text='<%# Bind("prevent_tqm_id")%>' Visible="false"></asp:Label>
                               
                                  <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action plans ">
                            
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("action_detail") %>'></asp:Label>
                                <br />- <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("dept_name_en") %>' ForeColor="blue"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="450px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="date_start" DataFormatString="{0:dd MMM yyyy}" HeaderText="Start" NullDisplayText="-">
                        <ItemStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date_end" DataFormatString="{0:dd MMM yyyy}" HeaderText="Completed" NullDisplayText="-" />
                        <asp:BoundField DataField="resp_person" HeaderText="Responsible Person" />
                        <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle Width="80px" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle CssClass="colname" />
                </asp:GridView>
           
     <table cellpadding="3" cellspacing="0" class="tdata" width="100%">
                    <%If gridTQMActionPlan.Rows.Count = 0 Then%>
                    <tr>
                        <td class="colname" style="font-weight:bold" valign="top" width="60">No.</td>
                        <td class="colname" style="font-weight:bold" valign="top" width="450">Action Plans</td>
                        <td class="colname" style="font-weight:bold" valign="top" width="80">Start</td>
                        <td class="colname" style="font-weight:bold" valign="top" width="80">Completed</td>
                        <td class="colname" style="font-weight:bold" valign="top">Responsible Person</td>
                    </tr>
                    <%end if %>
                    <tr>
                        <td valign="top" width="60">&nbsp; </td>
                        <td valign="top" width="450">
                            <textarea id="txt_addprevent_tqm" runat="server" cols="45" name="txt_addprevent" rows="2" style="width: 350px"></textarea></td>
                        <td valign="top" width="100">
                            <asp:TextBox ID="txtadd_datetqm1" runat="server" Width="60px"></asp:TextBox>
                         
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image7" TargetControlID="txtadd_datetqm1">
                            </asp:CalendarExtender>
                            <asp:Image ID="Image7" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                        </td>
                        <td valign="top" width="100">
                            <asp:TextBox ID="txtadd_datetqm2" runat="server" Width="60px"></asp:TextBox>
                            
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image8" TargetControlID="txtadd_datetqm2">
                            </asp:CalendarExtender>
                            <asp:Image ID="Image8" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                        </td>
                        <td valign="top">
                            <input ID="txt_addperson_tqm" runat="server" name="txt_addperson_tqm" type="text" />
                            <br />
                        </td>
                    </tr>
                </table>

                  </td>
              </tr>
    <tr>
      <td valign="top">
          <asp:Button ID="cmdAddPreventTQM" runat="server" Text="Add Action Plan" Width="110px"  CausesValidation="False" />
          <asp:Button ID="cmdAddPreventFromDept" runat="server" Text="Copy Action Plan from Mgr." Width="180px"   CausesValidation="False" />
        </td>
    </tr>
              <tr>
                  <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                  <td valign="top">More Information</td>
              </tr>
              <tr>
                  <td valign="top">
                      <textarea ID="txttqm_remark" runat="server" cols="45" 
                          name="txttqm_remark" rows="5" style="width: 850px"></textarea></td>
              </tr>
              <tr>
                  <td valign="top">
                      <asp:Label ID="lblIRtqm7" runat="server" Text="Action" />
                  </td>
              </tr>
    <tr>
      <td valign="top"><textarea name="txtaction_tqm_detail" id="txtaction_tqm_detail" cols="45" rows="5" style="width: 850px" runat="server"></textarea></td>
    </tr>
     <tr>
     <td>
     <table>
         <tr>
          <td width="180"><asp:Label ID="lblIRtqm9" Text="Severity Level" runat="server" /></td>
          <td width="23">
            <asp:RadioButton ID="txtsevere_level1" GroupName="severe_level" runat="server" />
          </td>
          <td width="130">Insignificant</td>
          <td width="23">
              <asp:RadioButton ID="txtsevere_level2" GroupName="severe_level" 
                  runat="server" />
            </td>
          <td width="130">Near miss</td>
          <td width="23"><label for="select3">
           <asp:RadioButton ID="txtsevere_level3" GroupName="severe_level" 
                  runat="server" />
              </label></td>
          <td width="130">No harm</td>
          <td width="23"><label for="select3">
           <asp:RadioButton ID="txtsevere_level4" GroupName="severe_level" 
                  runat="server" />
              </label></td>
          <td>Mild Adverse Event</td>
          </tr>
        <tr>
          <td>&nbsp;</td>
          <td><label for="select3">
            <asp:RadioButton ID="txtsevere_level5" GroupName="severe_level" 
                  runat="server" />
              </label>
            </td>
          <td>Moderate Adverse Event</td>
          <td><asp:RadioButton ID="txtsevere_level6" GroupName="severe_level" 
                  runat="server" />
             
            </td>
          <td>Serious Adverse Event</td>
          <td><asp:RadioButton ID="txtsevere_level7" GroupName="severe_level" 
                  runat="server" />
            </td>
          <td>Sentinel event</td>
          <td>
              <asp:RadioButton ID="txtsevere_level8" runat="server" 
                  GroupName="severe_level" />
            </td>
          <td>None</td>
        </tr>
     </table>
     </td>
     </tr>      
    <tr>
      <td valign="top">
      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
        <tr>
          <td width="343"><asp:Label ID="lblIRtqm10" Text="Quality concern" runat="server" /></td>
          <td width="23"><asp:RadioButton ID="txttqmconcern1" GroupName="concern" runat="server" />
            </td>
          <td width="130"><asp:Label ID="lblIRtqm11" Text="Yes" runat="server" /></td>
          <td width="23"><asp:RadioButton ID="txttqmconcern2" GroupName="concern" 
                  runat="server" />
            </td>
          <td><asp:Label ID="lblIRtqm12" Text="No" runat="server" /></td>
          </tr>
        <tr>
          <td><asp:Label ID="lblIRtqm13" Text="Refer to Risk Management" runat="server" /></td>
          <td><asp:RadioButton ID="txttqmrefer1" GroupName="refer" runat="server" />
            </td>
          <td><asp:Label ID="lblIRtqm14" Text="Yes" runat="server" /></td>
          <td><asp:RadioButton ID="txttqmrefer2" GroupName="refer" runat="server" /></td>
          <td><asp:Label ID="lblIRtqm15" Text="No" runat="server" /></td>
          </tr>
        </table></td>
    </tr>
    </table>
</fieldset><br />
    </asp:Panel>
     </ContentTemplate>
 </asp:UpdatePanel>
<fieldset>
<table>
   <tr>
                  <td valign="top">
                      <asp:GridView ID="GridViewTQMDoctor" runat="server" AutoGenerateColumns="False" 
                          CellPadding="4" GridLines="Horizontal" Width="850px" BackColor="White" 
                          BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                          DataKeyNames="defendant_id" EnableModelValidation="True">
                          <RowStyle BackColor="White" ForeColor="#333333" />
                          <Columns>
                              <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                              <asp:BoundField DataField="doctor_name" HeaderText="Involving Physician" />
                              <asp:BoundField DataField="specialty" HeaderText="Specialty" />
                              <asp:BoundField DataField="monitor_flag" HeaderText="" />
                              <asp:CommandField ShowDeleteButton="True">
                                  <ItemStyle Width="50px" />
                              </asp:CommandField>
                          </Columns>
                          <FooterStyle BackColor="White" ForeColor="#333333" />
                          <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                      </asp:GridView>
                  </td>
              </tr>
    <tr>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" >
        <tr>
          <td><asp:Label ID="lblIRtqm8" Text="Involving Physician" runat="server" /></td>
          <td>
              <asp:TextBox ID="txttqm_finddoctor" runat="server" Width="200px"></asp:TextBox>
           
              MD Code <asp:TextBox ID="txtmdcode" runat="server"></asp:TextBox>
              &nbsp;
              </td>
          </tr>
    
          <tr>
              <td>
                  </td>
              <td>
                  <asp:DropDownList ID="txtmonitor" runat="server">
                  <asp:ListItem Value="Monitor">Monitor</asp:ListItem>
                    <asp:ListItem Value="Not Monitor">Not Monitor</asp:ListItem>
                  </asp:DropDownList>
                  <asp:Button ID="cmdTQMAddDoctor" runat="server" Text="Add Involving Physician" CausesValidation="False" />
              </td>
          </tr>
    
        </table></td>
    </tr>
</table>
</fieldset>
<br />
<fieldset>
<legend>Log Book</legend>
<table width="100%" >
<tr><td width="150" style="vertical-align:top">Patient Safety Goals</td><td>
 
    <asp:DropDownList ID="txtlog_safety" runat="server">
    <asp:ListItem Value="0">0</asp:ListItem>
    <asp:ListItem Value="1">1</asp:ListItem>
    <asp:ListItem Value="2">2</asp:ListItem>
    <asp:ListItem Value="3">3</asp:ListItem>
    <asp:ListItem Value="4">4</asp:ListItem>
    <asp:ListItem Value="5">5</asp:ListItem>
    <asp:ListItem Value="6">6</asp:ListItem>
    <asp:ListItem Value="7">7</asp:ListItem>
    <asp:ListItem Value="8">8</asp:ListItem>
     <asp:ListItem Value="9">9</asp:ListItem>
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
    </asp:DropDownList>
    </td></tr>
    <tr>
        <td style="vertical-align:top" width="150">
            Patient Safety Goals 2</td>
        <td>
            <asp:DropDownList ID="txtlog_safety2" runat="server">
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
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
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            Lab category</td>
        <td>
            <asp:DropDownList ID="txtlog_lab" runat="server">
                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                <asp:ListItem Value="1">Microbiology</asp:ListItem>
                <asp:ListItem Value="2">Chemistry</asp:ListItem>
                <asp:ListItem Value="3">Molecular Biology</asp:ListItem>
                <asp:ListItem Value="4">Immunology</asp:ListItem>
                <asp:ListItem Value="5">Hematology &amp; Coagulation</asp:ListItem>
                <asp:ListItem Value="6">Blood Bank</asp:ListItem>
                <asp:ListItem Value="7">Surgical Pathology</asp:ListItem>
                <asp:ListItem Value="8">Urine/Stool/Fluids</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            ASA classification</td>
        <td>
            <asp:DropDownList ID="txtlog_asa" runat="server">
                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                <asp:ListItem Value="1">Level 1</asp:ListItem>
                <asp:ListItem Value="2">Level 2</asp:ListItem>
                <asp:ListItem Value="3">Level 3</asp:ListItem>
                <asp:ListItem Value="4">Level 4</asp:ListItem>
                
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            &nbsp;</td>
        <td>
        <table>
        <tr>
        <td><asp:CheckBox ID="chk_write" runat="server" Text="Write Off" Width="100px" /></td>
        <td width="100" style="text-align:right"><asp:TextBox ID="txtwrite_bath" runat="server" Width="75px"></asp:TextBox> </td>
        <td width="120">THB to Unit</td>
        <td width="300">
            <asp:DropDownList ID="txtwrite_dept" runat="server" 
                DataTextField="dept_unit_name" DataValueField="dept_unit_id">
            </asp:DropDownList>
        </td>
        </tr>
        </table>
           
           </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            &nbsp;</td>
        <td>
         <table>
        <tr>
        <td>
            <asp:CheckBox ID="chk_refund" runat="server" Text="Refund" Width="100px" />
        </td>
         <td width="100" style="text-align:right">
            <asp:TextBox ID="txtrefund_bath" runat="server" Width="75px"></asp:TextBox>
            </td>
    <td width="120">THB by Unit</td>
      <td width="300">
            <asp:DropDownList ID="txtrefund_dept" runat="server" 
                DataTextField="dept_unit_name" DataValueField="dept_unit_id">
            </asp:DropDownList>
        </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            &nbsp;</td>
        <td>
         <table>
        <tr>
        <td>
            <asp:CheckBox ID="chk_remove" runat="server" Text="Remove" Width="100px" />
            </td>
             <td width="100" style="text-align:right">
            <asp:TextBox ID="txtremove_bath" runat="server" Width="75px"></asp:TextBox>
            </td>
            <td width="120">THB by Unit</td>
             <td width="300">
            <asp:DropDownList ID="txtremove_dept" runat="server" 
                DataTextField="dept_unit_name" DataValueField="dept_unit_id">
            </asp:DropDownList>
        </td>
            </tr>
            </table>
            </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">
            Follow up</td>
        <td>
            <asp:DropDownList ID="txtfollow" runat="server">
            <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
                <asp:ListItem Value="1">Flag</asp:ListItem>
                <asp:ListItem Value="2">Not Flag</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" width="150">Relate Standard</td>
        <td valign="top">
          <asp:TextBox type="text" name="txtrelated_standard" id="txtrelated_standard" style="width: 285px" runat="server"></asp:TextBox></td>
    </tr>
</table>
</fieldset>
<br />
<fieldset>
  <legend><asp:Label ID="lblIRtqm16" Text="Related Customer Feedback" runat="server" /></legend>
  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top">
        <table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td width="100"><asp:Label ID="lblIRtqm17" Text="Reference No." runat="server" 
                    Visible="False" /></td>
            <td>
                <asp:TextBox ID="txtir_cfb" runat="server" Width="235px" Visible="False"></asp:TextBox> 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtir_cfb"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
          </td>
            </tr>
            <tr>
                <td width="100">&nbsp;
                    </td>
                <td>
                    <asp:GridView ID="GridRelateCFB" runat="server"  AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="ir_relate_document_id" 
                        EnableModelValidation="True" >
                          <RowStyle BackColor="#eef1f3" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <asp:Label ID="Labels0" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CFB No.">
                              
                                <ItemTemplate>
                                    CFB<asp:Label ID="Label1" runat="server" Text='<%# Bind("reference_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle Width="50px" />
                            </asp:CommandField>
                        </Columns>
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
               <PagerStyle ForeColor="Black" HorizontalAlign="Center" 
                  BorderStyle="None" Font-Bold="False" />
              
              <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td width="100">&nbsp;
                    </td>
                <td>
                    <asp:TextBox ID="txtadd_cfbno" runat="server" Width="235px"></asp:TextBox>
                    &nbsp;<asp:Button ID="cmdAddCFBNo" runat="server" Text="Add CFB No."  CausesValidation="False" />
                </td>
            </tr>
          </table>
       </td>
    </tr>
    </table>
</fieldset>
<br />
<fieldset>
  <legend><asp:Label ID="Label4" Text="Related Accident & Incident Reports" runat="server" /></legend>
  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top">
        <table width="100%" cellspacing="1" cellpadding="2">
          <tr>
            <td width="100"><asp:Label ID="Label5" Text="Reference No." runat="server" 
                    Visible="False" /></td>
            <td>
                <asp:TextBox ID="txtrelate_ir" runat="server" Width="235px" Visible="False"></asp:TextBox> 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtrelate_ir"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
          </td>
            </tr>
            <tr>
                <td width="100">&nbsp;
                    </td>
                <td>
                    <asp:GridView ID="GridRelateIR" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" CssClass="tdata3" DataKeyNames="ir_relate_document_id" 
                        EnableModelValidation="True" GridLines="None" Width="98%">
                        <RowStyle BackColor="#eef1f3" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <asp:Label ID="Labels1" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IR No.">
                                <ItemTemplate>
                                    IR<asp:Label ID="Label6" runat="server" Text='<%# Bind("reference_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle Width="50px" />
                            </asp:CommandField>
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#abbbb4" CssClass="theader" Font-Bold="True" 
                            ForeColor="White" />
                        <PagerStyle BorderStyle="None" Font-Bold="False" ForeColor="Black" 
                            HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td width="100">&nbsp;
                    </td>
                <td>
                    <asp:TextBox ID="txtadd_irno" runat="server" Width="235px"></asp:TextBox>
                    &nbsp;<asp:Button ID="cmdAddIRNO" runat="server" Text="Add IR No."  CausesValidation="False" />
                </td>
            </tr>
          
          </table>
       </td>
    </tr>
    </table>
</fieldset>

<fieldset>
  <legend><asp:Label ID="Label7" Text="Repeat Accident & Incident Reports" runat="server" /></legend>
  <table>
    <tr>
                <td width="100">
                    Report Type</td>
                <td>
                    <asp:DropDownList ID="txtreporttype" runat="server">
                        <asp:ListItem Value="0">New Report</asp:ListItem>
                        <asp:ListItem Value="1">Additional Report</asp:ListItem>
                        <asp:ListItem Value="2">Repeated Report</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
      <tr>
          <td width="100">
              Repeat IR No.</td>
          <td>
              <asp:TextBox ID="txtrepeatIR" runat="server"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                  ControlToValidate="txtrepeatIR" ErrorMessage="Please Enter Only Numbers" 
                  ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
          </td>
      </tr> 
      <tr>
          <td width="100">
              &nbsp;</td>
          <td>
              &nbsp;</td>
      </tr>
  </table>
  </fieldset>
 </asp:Panel>
 <br />
 <table>
  <tr>
        <td valign="top" width="100">
              <asp:Label ID="Label8" runat="server" Text="Report by"></asp:Label> &nbsp;</td>
        <td>
            <asp:Label ID="lblTQMReportby" runat="server" ForeColor="#0033CC"></asp:Label>  &nbsp;</td>
    </tr>

    <tr>
        <td valign="top">
            <asp:Label ID="Label10" runat="server" Text="Contact Number"></asp:Label>
        </td>
        <td>
           <asp:Label ID="lblTQMContact" runat="server" ForeColor="#0033CC"></asp:Label>
        </td>
    </tr>
 </table>
</div>
<div class="tabbertab" id="tab_dept" runat="server">
        <h2>Sup/Mgr/Dept Mgr/Director</h2>
        
        <fieldset>

  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top">
          <asp:DropDownList ID="txtdept_tab" runat="server" DataValueField="dept_id" 
              DataTextField="dept_name" AutoPostBack="True">
          </asp:DropDownList>
          <asp:TextBox ID="txtrelateid" runat="server" Visible="false"></asp:TextBox>
       </td>
    </tr>
    </table>
    
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
      </ContentTemplate>
         
            </asp:UpdatePanel>
      <asp:Panel ID="panel_department" runat="server" >
    <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
      <td valign="top"><asp:Label ID="lblIRdept1" Text="Investigation" runat="server" /></td>
      </tr>
    <tr>
      <td valign="top"><textarea name="txtdept_invest" id="txtdept_invest" cols="45" rows="5" style="width: 850px" runat="server"></textarea></td>
      </tr>
   
    <tr>
      <td valign="top">ระบุสาเหตุ / Cause of Incident</td>
    </tr>
    <tr>
      <td valign="top">
         
          <asp:GridView ID="GridDeptCause" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="tdata" DataKeyNames="dept_cause_id" EnableModelValidation="True" ShowFooter="True" HeaderStyle-CssClass="colname" Width="100%" EmptyDataText="กรุณาระบุสาเหตุ / Please enter Cause of Incident" >
              <Columns>
                  
                  <asp:TemplateField HeaderText="No.">
                      <ItemStyle HorizontalAlign="Center" Width="30px" />
                       <FooterStyle VerticalAlign="Top" Width="30px" />
                      <ItemTemplate>
                          <asp:Label ID="lblPk0" runat="server" Text='<%# bind("dept_cause_id") %>' Visible="false"></asp:Label>
                         
                               <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="สาเหตุ / Cause of Incident">
                     
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("cause_name") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle Width="200px" />

                      <FooterStyle VerticalAlign="Top" Width="200px" />
                      <FooterTemplate>
                           <asp:DropDownList ID="txtcause_dept" runat="server">
                          <asp:ListItem Value="0">---- Please Select ----</asp:ListItem>
                          <asp:ListItem Value="1">Personal / Human Error</asp:ListItem>
                          <asp:ListItem Value="2">Communication</asp:ListItem>
                          <asp:ListItem Value="3">System Error</asp:ListItem>
                          <asp:ListItem Value="4">Equipment Error</asp:ListItem>
                          <asp:ListItem Value="5">Enviroment</asp:ListItem>
                          <asp:ListItem Value="6">Poor Practice Habit</asp:ListItem>
                          <asp:ListItem Value="8">Patient’s factor</asp:ListItem>
                          <asp:ListItem Value="7">Others</asp:ListItem>
                      </asp:DropDownList>

                      </FooterTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="รายละเอียด / Description">
                      
                      <ItemTemplate>
                          <asp:Label ID="Label11" runat="server" Text='<%# Bind("cause_remark") %>'></asp:Label>
                      </ItemTemplate>
                      <FooterTemplate>
                           <asp:TextBox ID="txttqm_addremark_dept" runat="server" Rows="3" TextMode="MultiLine" Width="95%"></asp:TextBox>
                      </FooterTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField ShowHeader="False">
                      <ItemTemplate>
                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" Text="Delete"></asp:LinkButton>
                      </ItemTemplate>
                      <ItemStyle Width="80px" />
                       <FooterStyle VerticalAlign="Top" Width="80px" />
                  </asp:TemplateField>
                  
              </Columns>
              <HeaderStyle CssClass="colname" />
              <FooterStyle BackColor="#EEEEEE" />
          </asp:GridView>
        </td>
    </tr>
        <tr>
            <td valign="top">
                <asp:Button ID="cmdDeptAddCause" runat="server" Text="Add Cause" Width="110px"  CausesValidation="False" />
            </td>
        </tr>
         <tr>
      <td valign="top" ><asp:Label ID="lblIRdept2" Text="Cause" runat="server" />&nbsp;&nbsp;&nbsp;
          <asp:DropDownList ID="txtadd_cause" runat="server" 
              AutoPostBack="True" Visible="False">
          <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
          <asp:ListItem Value="Personal / Human Error">Personal / Human Error</asp:ListItem>
          <asp:ListItem >Communication</asp:ListItem>
          <asp:ListItem>System Error</asp:ListItem>
          <asp:ListItem>Equipment Error</asp:ListItem>
          <asp:ListItem>Enviroment</asp:ListItem>
          <asp:ListItem>Poor Practice Habit</asp:ListItem>
          <asp:ListItem>Others</asp:ListItem>
         
          </asp:DropDownList>

          <asp:Button ID="cmdAddCause" runat="server" Text="Add" Visible="False" />
          <br />
          <textarea name="txtdept_cause" id="txtdept_cause" cols="45" rows="5" style="width: 850px" runat="server"></textarea>
      </td>
      </tr>
   
        <tr>
            <td valign="top">&nbsp;</td>
        </tr>

        <tr>
            <td valign="top">
                <asp:GridView ID="GridViewPrevent" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="tdata" DataKeyNames="prevent_dept_id" EnableModelValidation="True" ShowFooter="True" HeaderStyle-CssClass="colname" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPk" runat="server" Text='<%# bind("prevent_dept_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action plans ">
                           
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("action_detail") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="450px" />

                            <FooterTemplate>
                                 <textarea id="txt_addprevent" runat="server" cols="45" name="txt_addprevent" rows="2" style="width: 350px"></textarea>

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start">
                         
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("date_start", "{0:dd MMM yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                            <FooterTemplate>
                                 <asp:TextBox ID="txtdate_prevent1" runat="server" Width="60px"></asp:TextBox>
                           
                            <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image5" TargetControlID="txtdate_prevent1">
                            </asp:CalendarExtender>
                            <asp:Image ID="Image5" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed">
                          
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("date_end", "{0:dd MMM yyyy}") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                  <asp:TextBox ID="txtdate_prevent2" runat="server" Width="60px"></asp:TextBox>
                      
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image6" TargetControlID="txtdate_prevent2">
                            </asp:CalendarExtender>
                            <asp:Image ID="Image6" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Responsible Person">
                            
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("resp_person") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>  <input ID="txt_addperson" runat="server" name="txt_addperson" type="text" />
                            <br />
                            

                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle Width="80px" />
                        </asp:CommandField>
                    </Columns>
                    <HeaderStyle CssClass="colname" />
                    <FooterStyle BackColor="#EEEEEE" />
                </asp:GridView>
              
            </td>
        </tr>
    <tr>
      <td valign="top">
          <asp:Button ID="cmdAddPrevent" runat="server" Text="Add Action Plan" Width="110px"  CausesValidation="False" />
        <asp:Button ID="cmdSaveOrder" runat="server" Text="Save Order" Visible="False" />
        <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="Please press 'Add Action Plan' button before save data."></asp:Label>
         
        </td>
    </tr>
    </table>
    <br />
    <fieldset>
    <legend>FMS Part</legend>
    <table width="100%">
    <tr><td><strong>มูลค่าความเสียหายของทรัพย์สิน (Damage asset cost)</strong> </td></tr>
     <tr><td>
         <asp:RadioButtonList ID="txtfms_damage" runat="server" 
             RepeatDirection="Horizontal">
             <asp:ListItem>ไม่มีความเสียหาย</asp:ListItem>
             <asp:ListItem>&lt;1,000 บาท</asp:ListItem>
             <asp:ListItem>1,000 -10,000 บาท</asp:ListItem>
             <asp:ListItem>10,001 -100,000 บาท</asp:ListItem>
             <asp:ListItem>100,001 -1,000,000 บาท</asp:ListItem>
             <asp:ListItem>&gt;1,000,000</asp:ListItem>
         </asp:RadioButtonList>
         </td></tr>
      <tr><td><strong>ผลกระทบต่อการทำงาน/ระบบ (Work/System disruption)</strong></td></tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="txtfms_work" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem>0 hour</asp:ListItem>
                    <asp:ListItem>1-2 hours</asp:ListItem>
                    <asp:ListItem>3-5 hours</asp:ListItem>
                    <asp:ListItem>6-8 hours</asp:ListItem>
                    <asp:ListItem>>8 hours</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    </fieldset>
     </asp:Panel>
        
   
</fieldset>
        
        <br /></div>
<div class="tabbertab" id="tab_psm" runat="server">
  <h2>Risk Management</h2>
   <asp:Panel ID="panel_psm" runat="server" >
        <fieldset>
          <table width="100%" cellpadding="2" cellspacing="1">
    <tr>
      <td width="180" valign="top">Attach files</td>
      <td valign="top">
         
          <table>
              <tr>
                  <td colspan="2" valign="top">
                     <table cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                              <td colspan="2" valign="top">
                                  <asp:FileUpload ID="FileUpload2" runat="server" Width="535" />
                                  <asp:Button ID="cmdUploadFileMCO" runat="server" CausesValidation="False" 
                                      Text="Add File" />
                                  <asp:Button ID="cmdDeleteFileMCO" runat="server" CausesValidation="False" 
                                      Text="Delete selected attachments" />
                              </td>
                          </tr>
                      </table>
                      <asp:GridView ID="GridFileMCO" runat="server" AutoGenerateColumns="False" 
                          BorderWidth="0px" CellPadding="2" CellSpacing="1" ShowHeader="False" 
                          Width="100%">
                          <Columns>
                              <asp:TemplateField>
                                  <ItemTemplate>
                                      <asp:Label ID="lblPKMCO" runat="server" Text='<%# Bind("file_id") %>' 
                                          Visible="false"></asp:Label>
                                      <asp:CheckBox ID="chkSelectMCO" runat="server" />
                                  </ItemTemplate>
                                  <ItemStyle Width="30px" />
                              </asp:TemplateField>
                              <asp:TemplateField>
                              
                                  <ItemTemplate>
                                      <asp:Label ID="lblFilePathMCO" runat="server" Text='<%# Bind("file_path") %>' 
                                          Visible="false"></asp:Label>
                                      <a href='../share/mco/ir/<%# Eval("file_path") %>' 
                                          target="_blank">
                                      <asp:Label ID="Label3" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                                      </a>
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                      </asp:GridView>
                  </td>
              </tr>
          </table>
        </td>
    </tr>
      <tr>
                  <td valign="top">Diagnosis</td>
                  <td valign="top">
                      <asp:TextBox ID="txtpsm_diagnonsis" runat="server" Width="80%"></asp:TextBox>
                    
                      &nbsp;
                      <img src="../images/spellcheck.png" width="16" height="16" />
                    </td>
                </tr>
    </table>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <fieldset><legend><strong>Medical Support Concern list</strong> </legend>
              <table  width="100%" cellpadding="2" cellspacing="1">
                 <tr>
                      <td  width="160" valign="top">
                          Concern</td>
                      <td valign="top">
                          <asp:Button ID="cmdPSMAddConcern" runat="server" Text="Add concern" CausesValidation="False" />
                          &nbsp;</td>
                  </tr>
 
              </table>
              <asp:Panel ID="panel_psm_concern" runat="server" Visible="false" 
                    BackColor="#CCCCFF">
               <table width="100%" cellpadding="2" cellspacing="1">
            <tr>
                <td valign="top">Concern detail</td>
                <td valign="top">
                    <textarea ID="txtpsm_concern" cols="45" name="txtpsm_concern" rows="5" runat="server"
                        style="width: 735px"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Final comment category</td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="300">
                        <asp:DropDownList ID="txtpsm_category" runat="server" Width="285px" 
                            AutoPostBack="True" DataTextField="psm_category_name" 
                            DataValueField="psm_category_id">
                        </asp:DropDownList>
                      </td>
                    <td width="90">Subcategory</td>
                    <td> <asp:DropDownList ID="txtpsm_subcategory" runat="server" Width="285px" 
                            DataTextField="subcategory_name" DataValueField="psm_sub_category_id">
                        </asp:DropDownList>&nbsp;</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">Standard of care</td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="20"><asp:RadioButton ID="txtpsm_std1" runat="server" GroupName="psm_std" /></td>
                     
                    <td width="50">Yes</td>
                    <td width="20">
                        <asp:RadioButton ID="txtpsm_std2" runat="server" GroupName="psm_std" />
                      </td>
                    <td width="50">No</td>
                    <td width="20">
                        <asp:RadioButton ID="txtpsm_std3" runat="server" GroupName="psm_std" />
                      </td>
                    <td>Borderline</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">Defendant&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Physician</span></td>
                <td valign="top"><label> </label>
                  <label>                    </label>
                  <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="370">
                          <asp:TextBox ID="txtpsm_add_doctor" runat="server" Width="335px" ></asp:TextBox>
                       <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/DoctorService.asmx" ServiceMethod="getDoctor2" CompletionSetCount="5" 
            TargetControlID="txtpsm_add_doctor"  EnableViewState="false" OnClientItemSelected="IAmSelected2" >
        </asp:AutoCompleteExtender>
&nbsp;<img src="../images/spellcheck.png" width="16" height="16" /></td>
                      <td width="60">Specialty</td>
                      <td><input name="txtpsm_add_special" type="text" id="txtpsm_add_special" style="width: 230px;" runat="server" />
                          <asp:Button ID="cmdPSMAddDoc" runat="server" Text="Add" CausesValidation="False" />
                          &nbsp;<asp:Button ID="cmdPSMDelDoc" runat="server" Text="Delete" CausesValidation="False" />
                        </td>
                    </tr>
                  </table>

                    <asp:ListBox ID="txtpsm_list_doctor" runat="server" Width="735px" 
                        DataTextField="doctor_name" DataValueField="doctor_name"></asp:ListBox>
                 </td>
              </tr>
              <tr>
                <td valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Department</span></td>
                <td valign="top">
                    <asp:DropDownList ID="txtpsm_add_dept" runat="server" DataTextField="dept_name_en" DataValueField="dept_id" Width="630px">
                    </asp:DropDownList>
                                   &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="cmdPSMAddDept" runat="server" Text="Add" CausesValidation="False" /> <asp:Button ID="cmdPSMRemoveDept" runat="server" Text="Delete" CausesValidation="False"  />
                
                    <asp:HiddenField ID="txtpsm_add_deptid" runat="server"  />
                
                  </td>
              </tr>
                   <tr>
                       <td valign="top">&nbsp;
                           </td>
                       <td valign="top">
                          
                           <asp:ListBox ID="txtpsm_list_dept" runat="server" 
                               DataTextField="doctor_name" DataValueField="doctor_name" Width="735px">
                           </asp:ListBox>
                       </td>
                   </tr>
                   <tr>
                       <td valign="top">&nbsp;
                           </td>
                       <td valign="top">&nbsp;
                           </td>
                   </tr>
              <tr>
                <td colspan="2" valign="top">
                 <asp:Button ID="cmdSaveConcenrn" runat="server" Text="Save" Font-Bold="True" CausesValidation="False" />
                    &nbsp;<asp:Button ID="cmdCancelConcern" runat="server" Text="Cancel" CausesValidation="False" />
               </td>
              </tr>
              <tr>
                <td colspan="2" valign="top"></td>
              </tr>
            </table>
              </asp:Panel>
                  <asp:GridView ID="GridConcern" runat="server" Width="90%"  HorizontalAlign="Center"
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="concern_id" HeaderStyle-CssClass="colname" 
                          EnableModelValidation="True">
           <Columns>
               <asp:TemplateField HeaderText="No.">
               
               <ItemStyle Width="60px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblPk" runat="server" Text='<%# bind("concern_id") %>' Visible="false"></asp:Label>
                      <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="concern_detail" 
                   HeaderText="Concern" >
                   <ItemStyle Width="350px" />
               </asp:BoundField>
               <asp:BoundField DataField="topic_name" HeaderText="Category" >
                 
               </asp:BoundField>
               <asp:BoundField HeaderText="Subcategory" DataField="subtopic_name" />
               <asp:BoundField HeaderText="Standard of care" DataField="std_care_type_name" />
               <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <input class="detailButton" name="btnDetail" 
                                          onclick='openConcern(<%# Eval("concern_id") %>)' 
                                          type="button" value="Detail.."  />
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                              </asp:TemplateField>
               <asp:CommandField ShowDeleteButton="True">
                   <ItemStyle Width="80px" />
               </asp:CommandField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
          <br />
         </fieldset>
                <table width="100%" cellpadding="2" cellspacing="1">
    <tr>
      <td width="160" valign="top">Resolution</td>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
        <tr>
          <td width="23">
            <input type="checkbox" name="checkbox15" id="chk_reso1" runat="server" />
           
          </td>
          <td width="200">Verbal explanation</td>
          <td width="20"><input type="checkbox" name="checkbox20" id="chk_reso2" runat="server" /></td>
          <td>Written explantion</td>
          </tr>
        <tr>
          <td><input type="checkbox" name="checkbox16" id="chk_reso3" runat="server" /></td>
          <td>Verbal apology </td>
          <td><input type="checkbox" name="checkbox17" id="chk_reso4" runat="server" /></td>
          <td>Written apology</td>
          </tr>
        <tr>
          <td><input type="checkbox" name="checkbox18" id="chk_reso5" runat="server" /></td>
          <td>Goodwill Gesture</td>
          <td><input type="checkbox" name="checkbox19" id="chk_reso6" runat="server" /></td>
          <td>Compenstion</td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td valign="top">Compensation Detail <br />
        (if applicable)</td>
      <td valign="top"><textarea name="txtpsm_compensation" id="txtpsm_compensation" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
    </tr>
    <tr>
      <td valign="top">Patient Satisfaction</td>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
        <tr>
          <td width="20"><asp:RadioButton ID="txtpsm_pt1" runat="server" GroupName="psm_satis" />
            </td>
          <td width="50">Yes</td>
          <td width="20">
              <asp:RadioButton ID="txtpsm_pt2" runat="server" GroupName="psm_satis" />
            </td>
          <td width="50">No</td>
          <td width="20">
              <asp:RadioButton ID="txtpsm_pt3" runat="server" GroupName="psm_satis" />
            </td>
          <td width="125">Cannot contact</td>
          <td width="20">
              <asp:RadioButton ID="txtpsm_pt4" runat="server" GroupName="psm_satis" />
            </td>
          <td>Patient did not response</td>
          </tr>
        </table></td>
    </tr>
    <tr>
      <td valign="top">Legal Implication</td>
      <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
        <tr>
          <td width="23">
            <input type="checkbox" name="chk_psm_refer" id="chk_psm_refer" runat="server" />
          </td>
          <td>refer to legal team</td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td valign="top">Recommendation</td>
      <td valign="top"><textarea name="txtpsm_recommend" id="txtpsm_recommend" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
    </tr>
    <tr>
      <td valign="top">Remark</td>
      <td valign="top"><textarea name="txtpsm_remark" id="txtpsm_remark" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
    </tr>
    <tr>
      <td valign="top">MCO-MS Work Status</td>
      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td width="235">
              <asp:DropDownList ID="txtpsm_status" runat="server">
              <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
              <asp:ListItem Value="1">Handled by MCO-MS</asp:ListItem>
              <asp:ListItem Value="2">Assisted by MCO-MS</asp:ListItem>
              <asp:ListItem Value="3">Screening and return to TQM</asp:ListItem>
               <asp:ListItem Value="4">Turn to Legal</asp:ListItem>
              </asp:DropDownList>
          
         </td>
          <td width="80">Closed Date </td>
          <td><asp:TextBox ID="txtpsm_date_expect" runat="server" Width="170px"></asp:TextBox>
                   <asp:CalendarExtender ID="txtdate_assess_CalendarExtender" runat="server" 
                ClearTime="True" Enabled="True" TargetControlID="txtpsm_date_expect" 
                Format="dd/MM/yyyy">
            </asp:CalendarExtender></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td valign="top">Responsible Person</td>
      <td valign="top">
          <asp:TextBox ID="txtpsm_add_person" runat="server" Width="385px"></asp:TextBox>
         
                       <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/EmployeeService.asmx" ServiceMethod="getEmployee" CompletionSetCount="5" 
            TargetControlID="txtpsm_add_person"  EnableViewState="false" >
        </asp:AutoCompleteExtender>
          &nbsp;<asp:Button ID="cmdAddPSMPerson" runat="server" Text="Add" />
        </td>
    </tr>
                    <tr>
                        <td valign="top">&nbsp;
                            </td>
                        <td valign="top">
                            <textarea ID="txtpsm_response" runat="server" cols="45" name="txtpsm_remark0" 
                                rows="5" style="width: 435px"></textarea></td>
                    </tr>
    </table>
    </ContentTemplate>
                </asp:UpdatePanel>
</fieldset>
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
                               <asp:BoundField HeaderText="Date/Time" DataField="inform_date" 
                                  ItemStyle-Width="120px" >
<ItemStyle Width="120px"></ItemStyle>
                              </asp:BoundField>
                              <asp:BoundField DataField="inform_detail" HeaderText="Information update" 
                                  ItemStyle-Width="400px" >
<ItemStyle Width="400px"></ItemStyle>
                              </asp:BoundField>
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
        <asp:Button ID="cmdAddUpdate" runat="server" Text="Add more information" CausesValidation="False" />
        </td>
    </tr>
    </table>
  
      <br />
</div>

   
      </div>
      
  <div class="tabber" id="mytabber11">    
   <div class="tabbertab" id="tabOccurrence" runat="server">
        <h2>Describe the occurrence</h2>
        <fieldset>
        <asp:Panel ID="panel_form" runat="server" >
          <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
      </asp:Panel>
      </fieldset>
        <br /></div>
  </div>
  <br />
     <div align="right">
      <asp:Panel ID="Panel2" runat="server"  HorizontalAlign="Center">
          <asp:Button ID="cmdDraft" runat="server" Text="Save draft" OnCommand="onSave" 
              CommandArgument="1" ForeColor="#FF3300" CausesValidation="False" />
    <asp:Button ID="cmdSubmit" runat="server"  Text="Submit" OnCommand="onSave" 
              CommandArgument="2" Font-Bold="True" />
        <asp:Button ID="cmdTQMDraft1" runat="server" Text="Save draft" OnCommand="onSave" 
            CommandArgument="" ForeColor="#FF3300" CausesValidation="False" /> 
       <asp:Button ID="cmdTQMView1" runat="server" Text="Save revision"  ForeColor="#000099" Font-Bold="True" CausesValidation="False" /> 
    <asp:Button ID="cmdTQMClose1" runat="server"  Text="Close incident" OnCommand="onSave" 
            CommandArgument="9" Font-Bold="True" CausesValidation="False"  />
                <asp:Button ID="cmdDeptReturn1" runat="server"  Text="Save and return case to TQM" OnCommand="onSave"  
            CommandArgument="7" Font-Bold="True" OnClientClick="return confirm('Are you sure you want to return case to TQM?');"  CausesValidation="False" />  
               <asp:Button ID="cmdPSMReturn1" runat="server"  Text="Save and return case to TQM" OnCommand="onSave"  CausesValidation="False"
            CommandArgument="8" Font-Bold="True" OnClientClick="return confirm('Are you sure you want to return case to TQM?');"   />     
            </asp:Panel>
        </div>
</asp:Content>

