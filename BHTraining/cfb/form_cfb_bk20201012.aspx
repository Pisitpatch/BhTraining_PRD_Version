<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="form_cfb.aspx.vb" Inherits="cfb.form_cfb" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script>
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
          //  alert( $("#ctl00_ContentPlaceHolder1_txtprocedure").val() );

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

          $("#ctl00_ContentPlaceHolder1_txtpsm_add_doctor").autocomplete("../getDoctor.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtpsm_add_doctor').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              // var serial = array();
              //  var serial = data.split(",");
              // alert("serial ::" + data[1]);
              $("#ctl00_ContentPlaceHolder1_txtpsm_add_special").val(data[1]);

          });

          $("#ctl00_ContentPlaceHolder1_txtpsm_add_doctor").click(function () {
              $(this).select();
          });

          $("#ctl00_ContentPlaceHolder1_txtdiagnosis").autocomplete("../getDiagnosis.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtdiagnosis').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

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

    <script type="text/javascript">
        function openDetail(id , type_id) {
            loadPopup(1);
            my_window =  window.open('cfb_service_recovery.aspx?status=<%Response.Write(txtstatus.SelectedValue)%>&mode=<%Response.Write(mode)%>&id=' + id + '&typeid=' + type_id , 'popupFile', 'alwaysRaised,scrollbars =yes,width=800,height=750');
   }
    <% If mode = "add" Then%>
        $(document).ready(function() {
            $(".detailButton").css("display","none");
        });
    <% End If%>
    
    
        function openPopup(flag , popupType) {
            if (flag.checked) {
                is_sms = 1;
            } else {
                is_sms = 0;
            }
        
            window.open('../incident/popup_recepient.aspx?popupType=' + popupType, '', 'alwaysRaised,scrollbars =yes,status=yes,width=800,height=600');
        }

        function openConcern(concern_id){
            window.open('mco_popup.aspx?irId=<%Response.Write(cfbId)%>&concern_id=' + concern_id, '', 'alwaysRaised,scrollbars =yes,status=yes,width=800,height=600');
    }

    function validateComment(){
        if ($("#ctl00_ContentPlaceHolder1_txtcomment_type").val() == ""){
            alert("กรุณาระบุ Type of comment");
            $("#ctl00_ContentPlaceHolder1_txtcomment_type").focus();
            return false
        }

        if ($("#ctl00_ContentPlaceHolder1_txtadd_detail").val() == ""){
            alert("กรุณาระบุ Describe the Occurrence");
            $("#ctl00_ContentPlaceHolder1_txtadd_detail").focus();
            return false
        }else{
            return true;
        }
    }

    function IAmSelected(source, eventArgs) {
        // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
        // $("#ctl00_ContentPlaceHolder1_txtmdcode").attr("disabled", false);
        $("#ctl00_ContentPlaceHolder1_txtpsm_add_special").val(eventArgs.get_value());
    }

    function IAmSelected2(source, eventArgs) {
        // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
        $("#ctl00_ContentPlaceHolder1_txtmdcode").attr("disabled", false);
        $("#ctl00_ContentPlaceHolder1_txtmdcode").val(eventArgs.get_value());
    }
    function txttel_onclick() {

    }

    function checkDate(sender,args)
    {
        if (sender._selectedDate > new Date())
        {
            alert("You cannot select a day !");
            sender._selectedDate = new Date(); 
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }

    $(document).ready(function () { 
        checkSession("<%Response.Write(Session("bh_username").ToString)%>" , "<%Response.Write(viewtype)%>"); // Check session every 1 sec.
     });
    </script>
    <!--
<script type="text/javascript">
    document.onkeydown = document.onkeypress = function (evt) {
        if (typeof evt == 'undefined') {
            evt = window.event;
        }
        if (evt) {
            var keyCode = evt.keyCode ? evt.keyCode : evt.charCode;
            if (keyCode == 8) {
                if (evt.preventDefault) {
                    evt.preventDefault();
                }
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    }
</script>
-->

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

        $('#ctl00_ContentPlaceHolder1_txtdate_report').calendarsPicker(
			{
			    maxDate: date_str,
			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);

        $('#ctl00_ContentPlaceHolder1_txtdate_complaint').calendarsPicker(
			{
			   
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="display: none;">
        <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left: 5px; cursor: pointer" />
    </div>

    <div align="right">
        <asp:Panel ID="Panel1" runat="server" Width="400px" HorizontalAlign="Center">
            <asp:Button ID="cmdRestore" runat="server" Text="Resotre from cancelled status"
                OnClientClick="return confirm('Are you sure you want to restore this case?')"
                Visible="false" CausesValidation="False" />

            <asp:Button ID="cmdDraft2" runat="server" Text="Save draft" OnCommand="onSave"
                CommandArgument="1" ForeColor="#FF3300" CausesValidation="False" />
            <asp:Button ID="cmdSubmit2" runat="server" Text="Submit" OnCommand="onSave" OnClientClick="this.disabled=true;"
                CommandArgument="2" Font-Bold="True" UseSubmitBehavior="False" />
            <asp:Button ID="cmdTQMDraft2" runat="server" Text="Save draft" OnCommand="onSave"
                CommandArgument="" ForeColor="#FF3300" CausesValidation="False" />
            <asp:Button ID="cmdTQMView2" runat="server" Text="Save revision"  ForeColor="#000099" Font-Bold="True" CausesValidation="False" />
            <asp:Button ID="cmdTQMClose2" runat="server" Text="Close CFB" OnCommand="onSave"
                CommandArgument="9" Font-Bold="True" OnClientClick="return confirm('Are you sure you want to close this CFB case ?')" CausesValidation="False" />
            <asp:Button ID="cmdDeptReturn2" runat="server" Text="Save and return case to TQM" OnCommand="onSave"
                CommandArgument="7" Font-Bold="True" OnClientClick="return confirm('Are you sure you want to return case to IR&CFB ? \n\nOnce the case is return to IR&CFB you can not be edited this information anymore. ')" CausesValidation="False" />
            <asp:Button ID="cmdPSMReturn2" runat="server" Text="Save and return case to TQM" OnCommand="onSave"
                CommandArgument="8" Font-Bold="True" CausesValidation="False" />

        </asp:Panel>
        <asp:Button ID="cmdReopen2" runat="server" Text="Re-Open" Font-Bold="True" Visible="false"  CausesValidation="False" />

        <asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="Panel1" VerticalSide="Top" VerticalOffset="10" HorizontalSide="Right"></asp:AlwaysVisibleControlExtender>
    </div>
    <div id="data">
        <table width="100%" cellpadding="2" cellspacing="1">
            <tr>
                <td>&nbsp;&nbsp;<img src="../images/menu_02.png" width="32" height="32" hspace="5" alt="icon" />
                    <span class="style1">CFB Report :
        <asp:Label ID="lblHeader" runat="server" Text="" ForeColor="Red"></asp:Label>
                        &nbsp;<asp:Button ID="cmdPrintForm" runat="server" Text="Print Form"
                            CausesValidation="False" />

                    </span></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><strong>
                    <asp:Label ID="lblCFBdetail1" runat="server" Text="Customer Feedback No."></asp:Label>
                </strong>&nbsp;<asp:Label ID="txtcfbid" runat="server" Width="150px"
                    Font-Bold="True" ForeColor="Blue"></asp:Label>
                    &nbsp;<strong>
                        <asp:Label ID="lblCFBdetail2" runat="server" Text="Department"></asp:Label>
                    </strong>&nbsp;
                    <asp:Label ID="txtdivision" runat="server" Width="235px"
                        Font-Bold="True" ForeColor="Blue" ReadOnly="true"></asp:Label>

                    <asp:Label ID="lblDeptID" runat="server"></asp:Label>

                </td>
                <td>
                    <div align="right">
                        Status
          <asp:DropDownList ID="txtstatus" runat="server"
              DataTextField="ir_status_name" DataValueField="ir_status_id" Font-Bold="True"
              BackColor="Aqua" ForeColor="Blue" Enabled="False" Width="285px">
          </asp:DropDownList>
                        <asp:DropDownList ID="txtlang" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="en">EN</asp:ListItem>
                            <asp:ListItem Value="th" Selected="True">TH</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
        <div class="remark">
            <img src="../images/sign_explain.png" width="16" height="16" />
            Send report to IR&CFB within 
        24 hours after occurrence (Sent to supervisor after office hour and immediately report if serious clinical occurence)
        </div>

        <div class="tabber" id="mytabber2">
            <div class="tabbertab" id="txttab_alert" runat="server">
                <h2>CFB Mail Alert</h2>
                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                    <tr>
                        <td valign="top">
                            <table width="100%" cellspacing="1" cellpadding="2">
                                <tr>
                                    <td valign="top" width="100">
                                        <input id="button1" name="button3"
                                            onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') , 'to')"
                                            style="width: 85px;" type="button" value="Address" />
                                    </td>
                                    <td valign="top">
                                        <input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" />
                                        <asp:Button ID="cmdSendMail" runat="server" Text=" Send " CausesValidation="False" /></td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <input id="btnCC" name="button9"
                                            onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') , 'cc')"
                                            style="width: 85px; display: none;" type="button" value="Cc..." /></td>
                                    <td valign="top">
                                        <input type="text" name="txtto0" id="txtcc" style="width: 635px;"
                                            runat="server" /></td>
                                </tr>
                                <tr>
                                    <td valign="top">Subject</td>
                                    <td valign="top">
                                        <asp:DropDownList ID="txtsubject" runat="server" Width="641px"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                            <asp:ListItem Value="4">Need Dept Mgr/ Director Investigation</asp:ListItem>
                                            <asp:ListItem Value="5">Need Risk Management Investigation</asp:ListItem>
                                            <asp:ListItem Value="100">Sentinel event, Serious outcome</asp:ListItem>
                                            <asp:ListItem Value="101">Information Updated</asp:ListItem>
                                            <asp:ListItem Value="102">Case Follow-up</asp:ListItem>
                                        </asp:DropDownList>

                                        <input id="chk_sms" runat="server" type="checkbox" visible="false" />
                                       <!-- Send SMS-->

                                        <asp:CheckBox ID="chk_priority" runat="server" Text="High Priority" Font-Bold="True" ForeColor="Red" />

                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Message</td>
                                    <td valign="top">
                                        <textarea id="txtmessage" runat="server" cols="45" rows="5"
                                            style="width: 635px;"></textarea>

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="tabbertab" id="txttab_alert_log" runat="server">
                <h2>CFB Alert Log
                    <asp:Label ID="lblAlertLogNum" runat="server" Text="(0)" /></h2>
                <asp:GridView ID="GridAlertLog" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="90%" DataKeyNames="log_alert_id" CellSpacing="1"
                    EnableModelValidation="True">
                    <RowStyle BackColor="#E3EAEB" />
                    <Columns>
                        <asp:BoundField DataField="alert_date" HeaderText="Date" />
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
            </div>

            <div class="tabbertab" id="txttab_attachment" runat="server">
                <h2>CFB Attachment
                    <asp:Label ID="lblAttachNum" runat="server" Text="(0)" /></h2>
                <table>
                    <tr>
                        <td valign="top">
                            <table width="100%" cellspacing="1" cellpadding="2">
                                <tr>
                                    <td valign="top">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="535px" />
                                        <asp:Button ID="cmdUpload"
                                            runat="server" Text="Upload" CausesValidation="False" />
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
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                                            <a href="../share/cfb/attach_file/<%# Eval("file_path") %>" target="_blank">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="tabbertab" id="txttab_review_log" runat="server">
                <h2>CFB Report Review Log
                    <asp:Label ID="lblReviewLogNum" runat="server" Text="(0)" /></h2>
                <asp:GridView ID="GridviewIncidentLog" runat="server" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="log_status_id" CellPadding="4" AllowPaging="True" ShowFooter="True" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("status_id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblReportby" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblReportLogRemark" runat="server" Text='<%# Bind("log_remark") %>' Width="100px"></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>

                                <asp:Label ID="txtLogReportby" runat="server" Text='<%# Bind("log_create_by") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Position">
                            <ItemTemplate>
                                &nbsp;&nbsp;
                <asp:Label ID="txtLogPosition" runat="server" Text='<%# Bind("position") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                &nbsp;&nbsp;
                <asp:Label ID="txtLogDept" runat="server" Text='<%# Bind("dept_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                &nbsp;&nbsp;
                <asp:Label ID="txtLogDate" runat="server" Text='<%# Bind("log_time") %>' DataFormatString="{0:dd MMM yyyy}"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>

            <div class="tabbertab">
                <h2>Revision
                    <asp:Label ID="lblRevisionNum" runat="server" Text="(0)" /></h2>
                <asp:GridView ID="GridRevision" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Width="100%" EnableModelValidation="True" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="revision_date" HeaderText="Revision Date">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="revision_by_name" HeaderText="Updated by">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="PDF Revision">

                            <ItemTemplate>

                                <a href='../share/cfb/revision/<%# Eval("file_name") %>.pdf'
                                    target="_blank">
                                    <asp:Label ID="Label1" runat="server"> <%# Container.DataItemIndex + 1 %>.</asp:Label>
                                </a>
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>

            <div class="tabbertab" runat="server" id="div_convert" visible="false">
                <h2><strong><span style="color: blue">CFB Form Management</span></strong></h2>
                <fieldset style="width: 70%">
                    <legend><strong>Convert to Incident</strong></legend>
                    <table cellspacing="1" cellpadding="3">
                        <tr>
                            <td>
                                <asp:Button ID="cmdConvert" runat="server" Text="Move Data to Incident Report" CausesValidation="False" Width="210px" OnClientClick="return confirm('Are you sure you want to move data to Incident Form ?');" />
                                &nbsp;ย้าย CFB Report นี้ ไปเป็น New Incident Report</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmdConvertAsCopy" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to copy data to Incident Form ?');" Text="Copy Data to Incident Report" Width="210px" /> &nbsp;Copy CFB Report ไปเป็น New Incident Report 
                            </td>
                        </tr>
                        <tr>
                            <td>New Incident Form Status   
                                <asp:RadioButtonList ID="txtconvertstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="1" Selected="True">Current CFB Status</asp:ListItem>
                                    <asp:ListItem Value="2">Draft Status</asp:ListItem>
                                </asp:RadioButtonList>
                                &nbsp;</td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <fieldset style="width: 70%">
                    <legend><strong>CFB Form Management</strong></legend>
                    <table cellspacing="1" cellpadding="3">
                        <tr>
                            <td>
                                <asp:Button ID="cmdChangeToDraft" runat="server" Text="Change this CFB Report to Draft" CausesValidation="False" Width="210px" ForeColor="Red" OnClientClick="return confirm('Are you sure you want to change status to Draft ?');" />
                            </td>

                            <td>เปลี่ยนสถานะเป็น Draft เพื่อให้ผู้รายงาน Submit เข้ามาใหม่ได้อีกครั้ง (ใช้ CFB No. เดิม)
                        &nbsp;</td>


                           
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmdCopy" runat="server" Text="Copy Data to new CFB Report" CausesValidation="False" Width="210px" OnClientClick="return confirm('Are you sure you want to copy to newly form ?');" />
                            </td>

                            <td>สร้างรายงาน CFB หมายเลขใหม่จากข้อมูลเดิม
                        &nbsp;</td>



                        </tr>

                    </table>
                </fieldset>
                <br />
                <fieldset style="width: 70%">
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

        <asp:HiddenField ID="txtidselect"
            runat="server" />
        <asp:HiddenField ID="txtidCCselect" runat="server" />
        <asp:HiddenField ID="txtidBCCSelect" runat="server" />
        <asp:HiddenField ID="txtidOther" runat="server" />
        <br />
        <div class="tabber" id="mytabber1">
            <div class="tabbertab">
                <h2>Customer Feedback </h2>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                <fieldset <%If viewtype = "dept" Then
                        Response.Write("class='readonly'")
                    End If%>>
                    <legend></legend>
                    <asp:Panel ID="panel_cfb_detail" runat="server">
                        <table width="100%" cellspacing="2" cellpadding="3" style="margin: 8px 10px;">
                            <tr>
                                <td width="170" valign="top">
                                    <asp:Label ID="lblCFBdetail3" runat="server" Text="Patient name"></asp:Label></td>
                                <td >
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
                                                <input type="text" id="txtcomplain" style="width: 165px"
                                                    runat="server" />
                                            </td>
                                            <td width="80">
                                                <asp:Label ID="lblCFBdetail4" runat="server" Text="HN"></asp:Label></td>
                                            <td width="120">
                                                <asp:TextBox ID="txthn" runat="server" Width="75px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1"
                                                    TargetControlID="txthn"
                                                    Mask="999999999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Number"
                                                    AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" runat="server" PromptCharacter="*" />
                                            </td>
                                            <td width="70">
                                                <asp:Label ID="lblCFBdetail5" runat="server" Text="Nationality"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtcountry" runat="server"></asp:TextBox>


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail4" Text="Age" runat="server" /></td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="75">
                                                <input type="text" name="txtptage" id="txtptage"
                                                    style="width: 30px" runat="server" />
                                                &nbsp;
            <asp:Label ID="lblIRdetail5" Text="yrs" runat="server" />
                                            </td>
                                            <td width="55">
                                                <asp:Label ID="lblIRdetail6" Text="Gender" runat="server" />
                                            </td>
                                            <td width="80">
                                                <asp:DropDownList ID="txtptsex" runat="server" Width="100px">
                                                    <asp:ListItem Value="">Not Specified</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td width="30">&nbsp;</td>
                                            <td width="100">&nbsp;</td>
                                            <td width="75">&nbsp;</td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail20" Text="Location (unit/room)" runat="server" />
                                </td>
                                <td>
                                      <asp:DropDownList ID="txtlocation_combo" runat="server" DataTextField="location" DataValueField="location" onchange="$('#ctl00_ContentPlaceHolder1_txtlocation').val(this.value);" ></asp:DropDownList>
        <asp:TextBox ID="txtlocation" runat="server" Width="135px" 
            ToolTip="Auto complete"  BackColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" Visible="False" />
        

                                   
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/LocationService.asmx" ServiceMethod="getLocation" CompletionSetCount="5"
                                        TargetControlID="txtlocation" EnableViewState="false">
                                    </asp:AutoCompleteExtender>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
            ControlToValidate="txtlocation" Display="Dynamic" 
            ErrorMessage="กรุณาระบุสถานที่เกิดเหตุ" ></asp:RequiredFieldValidator>
                                    <br />

                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail21" runat="server" Text="Room No." />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtroom" runat="server" ToolTip="Auto complete" Width="235px"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtroom_AutoCompleteExtender" runat="server"
                                        CompletionSetCount="5" DelimiterCharacters="" Enabled="True"
                                        EnableViewState="false" MinimumPrefixLength="1" ServiceMethod="getLocation"
                                        ServicePath="~/LocationService.asmx" TargetControlID="txtroom">
                                    </asp:AutoCompleteExtender>
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
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail12" runat="server" Text="Diagnosis" />
                                </td>
                                <td>
                                    <input type="text" name="txtdiagnosis" id="txtdiagnosis" style="width: 735px" runat="server" onfocus="this.select()" title="Auto complete" />
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/spellcheck.png" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail13" Text="Operation/Procedure" runat="server" />
                                </td>
                                <td>
                                    <input type="text" name="txtoperation" id="txtoperation" style="width: 735px" runat="server" /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail14" runat="server" Text="Type of service" />
                                </td>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td width="200">
                                                <asp:DropDownList ID="txtservicetype" runat="server">
                                                    <asp:ListItem>-- Please Select --</asp:ListItem>
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
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
            ControlToValidate="txtsegment" Display="Dynamic" 
            ErrorMessage="กรุณาระบุประเภทลูกค้า (Customer Segment)" ></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <% If mode = "edit" Then%>

                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBdetail6" runat="server" Text="Repeated customer feedback history"></asp:Label>
                                </td>
                                <td>
                                    <div class="warning" id="incidentWarn" runat="server"></div>
                                    <asp:GridView ID="GridHistory" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" Width="100%" ForeColor="#333333" GridLines="None"
                                        EnableModelValidation="True">
                                        <RowStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundField DataField="datetime_report" HeaderText="Date of occurrence"
                                                SortExpression="date" DataFormatString="{0:d MMMM yyyy}" />
                                            <asp:TemplateField HeaderText="CFB  No.">

                                                <ItemTemplate>
                                                    <%If (authorized_access_linkclick) Then %>
                                                    <a href="form_cfb.aspx?mode=edit&cfbId=<%# Eval("ir_id") %>">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("cfb_no") %>'></asp:Label></a>
                                                    <%Else%>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("cfb_no") %>'></asp:Label>
                                                    <%End If%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="comment_type_name" HeaderText="Type of Comment" />
                                            <asp:BoundField DataField="dept_name" HeaderText="Unit of occurence" />
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
                            <% End If%>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBdetail8" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="150">
                                                <asp:TextBox ID="txtdate_report" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                                            
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="กรุณาระบุวันที่เกิดเหตุ" ControlToValidate="txtdate_report"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="40" style="vertical-align: top">
                                                <asp:Label ID="lblCFBdetail9" runat="server" Text="Time"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="txthour" runat="server">
                                                </asp:DropDownList>
                                                :
                              <asp:DropDownList ID="txtmin" runat="server">
                              </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblIRdetail22" runat="server"
                                        Text="Date of complaint"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtdate_complaint" runat="server" BackColor="Lime"
                                        Width="100px"></asp:TextBox>
                                
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBdetail10" runat="server" Text="Status of complainant"></asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="250">
                                                <asp:DropDownList ID="txtcomplain_status" runat="server" Width="235px">
                                                    <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                                                    <asp:ListItem Value="1">Patient</asp:ListItem>
                                                    <asp:ListItem Value="2">Relatives</asp:ListItem>
                                                    <asp:ListItem Value="3">Visitors</asp:ListItem>
                                                    <asp:ListItem Value="4">Employee</asp:ListItem>
                                                    <asp:ListItem Value="5">Physician</asp:ListItem>
                                                    <asp:ListItem Value="6">Web Site</asp:ListItem>
                                                    <asp:ListItem Value="7">Other</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtcomplain_status" Display="Dynamic"
                                                    ErrorMessage="กรุณาระบุสถานภาพผู้ร้องเรียน"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCFBdetail11" runat="server" Text="Complainant name"></asp:Label>
                                                &nbsp;&nbsp;
                                      <input type="text" id="txtcomplain_remark" style="width: 350px" runat="server"
                                          maxlength="200" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBdetail12" runat="server" Text="Feedback from"></asp:Label></td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="250">
                                                <asp:DropDownList ID="txtfeedback_from" runat="server" Width="235px">
                                                    <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                                                    <asp:ListItem Value="1">Face to Face</asp:ListItem>
                                                    <asp:ListItem Value="2">Suggestion box</asp:ListItem>
                                                    <asp:ListItem Value="3">Customer Comment Form</asp:ListItem>
                                                    <asp:ListItem Value="4">Telephone</asp:ListItem>
                                                    <asp:ListItem Value="5">E-mail</asp:ListItem>
                                                    <asp:ListItem Value="6">Letter</asp:ListItem>
                                                    <asp:ListItem Value="7">Fax</asp:ListItem>
                                                    <asp:ListItem Value="8">Web site</asp:ListItem>
                                                    <asp:ListItem Value="9">Other</asp:ListItem>
                                                    <asp:ListItem Value="10">Survey</asp:ListItem>
                                                    <asp:ListItem Value="11">Facebook</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ErrorMessage="กรุณาระบุคำร้องเรียนมาจาก" ControlToValidate="txtfeedback_from"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>หัวข้อ/Title</td>
                                            <td></td>
                                            <td>
                                                <asp:DropDownList ID="txtfeedback_othertopic" runat="server">
                                                    
                                                    <asp:ListItem Value="0">-- กรุณาเลือก / Please Select --</asp:ListItem>
                                                    <asp:ListItem Value="1">ด้านการบริการ /Our Service</asp:ListItem>
                                                    <asp:ListItem Value="2">ด้านความปลอดภัย / Safety Concerns</asp:ListItem>
                                                    <asp:ListItem Value="3">ด้านจริยธรรม /Ethical Concerns</asp:ListItem>
                                                    <asp:ListItem Value="4">อื่น ๆ /Others</asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCFBdetail13" runat="server" Text="(please identify)"></asp:Label>&nbsp;&nbsp;
                                    <input id="txtfeedback_remark" runat="server" style="width: 150px" type="text"
                                        maxlength="200" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="top">

                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                        CssClass="tdata" DataKeyNames="comment_id" Width="98%"
                                        EnableModelValidation="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>'
                                                        Width="30px"></asp:TextBox>
                                                </ItemTemplate>

                                                <ItemStyle Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type of Comment">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("comment_type_name") %>'></asp:Label>
                                                    <asp:Label ID="lblCommentTypeId" runat="server" Text='<%# Bind("comment_type_id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDeptList" runat="server" ForeColor="GrayText" Text=""
                                                        Visible="true"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle Width="180px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Describe the Occurrence">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("comment_detail").replace(vbcrlf,"<br/>") %>'></asp:Label><br />
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("lastupdate_by") %>' ForeColor="BlueViolet"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("lastupdate_time") %>' ForeColor="BlueViolet"></asp:Label>
                                                    <br />
                                                    <asp:Button ID="cmdSend" runat="server" Text="Send to Star of BH" Visible="true" OnCommand="onGotoStar" CommandArgument='<%# Bind("comment_id") %>' />
                                                </ItemTemplate>

                                                <ItemStyle VerticalAlign="Top" Width="520px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Describe the Solution">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("comment_solution") %>'></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle VerticalAlign="Top" Width="250px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <input class="detailButton" name="btnDetail"
                                                        onclick='openDetail(<%# Eval("comment_id") %> , <%# Eval("comment_type_id") %>)'
                                                        type="button" value="Detail.." />
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False"
                                                        CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle ForeColor="#FF3300" Width="40px" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#DADADA" />
                                    </asp:GridView>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="top">

                                    <table width="98%" class="tdata">
                                        <%If GridView1.Rows.Count <= 0 Then%>
                                        <tr>
                                            <td valign="top" width="40" style="background-color: #DADADA; font-weight: bold">No.</td>
                                            <td valign="top" width="180" style="background-color: #DADADA; font-weight: bold">Type of Comment</td>
                                            <td valign="top" width="520" style="background-color: #DADADA; font-weight: bold">Describe the Occurrence</td>
                                            <td valign="top" width="250" style="background-color: #DADADA; font-weight: bold">Describe the Solution</td>
                                            <td valign="top" style="background-color: #DADADA; font-weight: bold">&nbsp;</td>
                                        </tr>
                                        <% End If%>
                                        <%If txtstatus.SelectedValue = "" Or txtstatus.SelectedValue = "1" Or viewtype = "tqm" Then%>
                                        <tr>
                                            <td valign="top" width="40"></td>
                                            <td valign="top" width="180">
                                                <asp:DropDownList ID="txtcomment_type" runat="server">
                                                    <asp:ListItem Value="">--- Please Select ---</asp:ListItem>
                                                    <asp:ListItem Value="1">Praise</asp:ListItem>
                                                    <asp:ListItem Value="2">Suggestion</asp:ListItem>
                                                    <asp:ListItem Value="3">Complaint</asp:ListItem>
                                                    <asp:ListItem Value="4">Service Recovery</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td valign="top" width="520">
                                                <textarea id="txtadd_detail" runat="server" cols="35" rows="6"
                                                    style="width: 480px"></textarea>
                                            </td>
                                            <td valign="top" width="250">
                                                <textarea id="txtadd_solution" runat="server" cols="35" rows="5"
                                                    style="width: 200px"></textarea>
                                            </td>
                                            <td valign="top">&nbsp;
                                      
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">&nbsp;
                                            </td>
                                            <td valign="top">
                                                <span style="visibility: hidden">Found
                                                    <asp:TextBox ID="lblCommentCount" runat="server" Visible="true"
                                                        Width="30px" Enabled="False"></asp:TextBox>
                                                    &nbsp;Comment
                                                </span>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                    ErrorMessage="กรณาระบุ Describe the Occurence" ControlToValidate="lblCommentCount"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="top">
                                                <asp:Button ID="cmdAddTopic" runat="server" Font-Bold="True"
                                                    ForeColor="#0033CC" Text="Add Topic" CausesValidation="False" OnClientClick="return validateComment()" />
                                                <asp:Button ID="cmdSaveOrder" runat="server" ForeColor="#990000"
                                                    Text="Save Order" CausesValidation="False" />
                                                <br />
                                                <asp:Label ID="Label5" runat="server"
                                                    Text="Please press 'Add Topic' button before save data." ForeColor="#FF3300"></asp:Label>
                                            </td>
                                            <td valign="top">&nbsp;
                                            </td>
                                        </tr>
                                        <%End If%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <fieldset id="complain" runat="server">
                            <legend></legend>
                            <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblCFBcomment11" runat="server" Text="Complaint Management" /></td>
                                    <td>
                                        <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                                            <tr>
                                                <td width="23">
                                                    <asp:RadioButton ID="txtcom1" GroupName="c1" runat="server" /></td>
                                                <td width="100">
                                                    <asp:Label ID="lblCFBcomment12" runat="server" Text="Yes" /></td>
                                                <td width="23">
                                                    <asp:RadioButton ID="txtcom2" GroupName="c1" runat="server" /></td>
                                                <td>
                                                    <asp:Label ID="lblCFBcomment13" runat="server" Text="No" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr style="display:none">
                                    <td width="180" valign="top">
                                        <asp:Label ID="lblCFBcomment14" runat="server" Text="Customer" /></td>
                                    <td>
                                        <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <asp:DropDownList ID="txtcustomer" runat="server">
                                                        <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                                                        <asp:ListItem Value="1">Satisfied and not requested to respond back</asp:ListItem>
                                                        <asp:ListItem Value="2">Satisfied and requested to respond back</asp:ListItem>
                                                        <asp:ListItem Value="3">Dissatisfied and not requested to respond back</asp:ListItem>
                                                        <asp:ListItem Value="4">Dissatisfied and requested to respond back</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
<%--                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                        ErrorMessage="กรุณาระบุความพึงพอใจของลูกค้า    " ControlToValidate="txtcustomer"
                                                        Display="Dynamic" Width="300px"></asp:RequiredFieldValidator>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" valign="top">(please identify)&nbsp;&nbsp;         
                                                    <input type="text" name="txtcus_detail" id="txtcus_detail" style="width: 285px" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180" valign="top">
                                        <asp:Label ID="lblCFBcomment23" runat="server" Text="Case Manager for Handle Case" /></td>
                                    <td colspan="2" valign="top">(Please identify name)&nbsp;&nbsp;         
                                                    <input type="text" name="txtcase_manager" id="txtcase_manager" style="width: 285px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblCFBcomment19" runat="server" Text="Contact Information" /></td>
                                    <td>
                                        <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                                            <tr>
                                                <td width="23">
                                                    <asp:CheckBox ID="chk_tel" runat="server" Text="" />
                                                </td>
                                                <td width="150">
                                                    <asp:Label ID="lblCFBcomment20" runat="server" Text="Telephone" /></td>
                                                <td>
                                                    <input type="text" name="txttel" id="txttel" style="width: 335px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chk_email" runat="server" Text="" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCFBcomment21" runat="server" Text="E-mail" /></td>
                                                <td>
                                                    <input type="text" name="txtemail" id="txtemail" style="width: 335px" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chk_othter" runat="server" Text="" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCFBcomment22" runat="server" Text="Other (please identify)" /></td>
                                                <td>
                                                    <input type="text" name="txtother" id="txtother" style="width: 335px" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table width="100%">
                            <tr>
                                <td valign="top" width="150">
                                    <asp:Label ID="lblIRReportBy1" runat="server" Text="Report by"></asp:Label>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblReport" runat="server" ForeColor="#0033CC"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="IRlblCallback1" runat="server" Text="Telephone No."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtcontact" runat="server" Width="285px"></asp:TextBox>
                                    &nbsp;(กรุณาระบุเบอร์โทรติดต่อกลับ / Please enter your contact no.)</td>
                            </tr>
                            <tr>
                                <td valign="top">&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtcfb_tel" runat="server" Width="285px" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </asp:Panel>

                     <asp:Panel ID="panel_dashborad" runat="server" >
       <table width="100%" cellspacing="1" cellpadding="2">
           
      <tr>
    <td colspan="2"><hr style="height: 2px; width: 98%; text-align: left;" /></td>
  </tr>
             <tr>
        <td valign="top" width="200">
              <asp:Label ID="Label12" runat="server" Text="Clinical Type"></asp:Label> &nbsp;</td>
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
      <tr>
          <td valign="top">Risk Level</td>
          <td>
              <asp:RadioButtonList ID="txtsevere" runat="server" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                  <asp:ListItem Value="10">Near Miss</asp:ListItem>
                  <asp:ListItem Value="1">No apparent injury</asp:ListItem>
                  <asp:ListItem Value="2">Minor (needs observation)</asp:ListItem>
                  <asp:ListItem Value="3">Moderate (needs further treatment)</asp:ListItem>
                  <asp:ListItem Value="4">Severe (result in extended hospital stay or transfer to critical care)</asp:ListItem>
                  <asp:ListItem Value="5">Death</asp:ListItem>
              </asp:RadioButtonList>
              <br />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtsevere" Display="Dynamic" ErrorMessage="กรุณาระบุ Risk Level (ผลการประเมิน)"></asp:RequiredFieldValidator>
          </td>
    </tr>
             <tr>
                 <td valign="top">Patient Safety Goal</td>
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
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
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
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ControlToValidate="txtpayor" Display="Dynamic" 
            ErrorMessage="กรุณาระบุ Issue" ></asp:RequiredFieldValidator>
                 </td>
             </tr>
           
       </table>
        
        </asp:Panel>
                </fieldset>

            </div>

            <div class="tabbertab" id="tabInvestigate" runat="server">
                <h2>Additional Investigation and Action Taken    <legend>
                    <asp:Label ID="lblCFBaction1" runat="server" Text="Describe the investigation and action"></asp:Label></legend>
                    <asp:Panel ID="panel_cfb_investigate" runat="server">
                        <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBaction2" runat="server" Text="Part of customer"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <textarea name="txtpart_customer" id="txtpart_customer" cols="45" rows="5" style="width: 850px" runat="server"></textarea></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBaction3" runat="server" Text="Part of hospital / Top management"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <textarea name="txtpart_hospital" id="txtpart_hospital" cols="45" rows="5" style="width: 850px" runat="server"></textarea></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCFBaction4" runat="server" Text="Part of employee"></asp:Label></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <textarea name="txtpart_employee" id="txtpart_employee" cols="45" rows="5" style="width: 850px" runat="server"></textarea></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>

            <div class="tabbertab" id="tabManager" runat="server">
                <h2>Sup/Mgr/Dept Mgr/Director</h2>

                <fieldset>
                    <legend></legend>
                    <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                        <tr>
                            <td>
                                <strong>Department</strong>
                                <asp:DropDownList ID="txtdepttab_combo" runat="server"
                                    DataTextField="dept_name" DataValueField="dept_id"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;<strong>Related Comment</strong> &nbsp;
                  <asp:DropDownList ID="txtdeptcase" runat="server"
                      DataTextField="order_sort" DataValueField="cfb_relate_dept_id"
                      AutoPostBack="True" Width="150px">
                  </asp:DropDownList>
                                <asp:TextBox ID="txtdepttab_comment_type" runat="server" ReadOnly="True"
                                    Visible="False"></asp:TextBox>
                                <asp:Button ID="cmdPrintInvest" runat="server" Text="Print investigation"
                                    CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Status : </strong>
                                <asp:Label ID="lblDeptStatus" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="lblDeptStatusID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <strong>Priority :</strong>
                                <asp:DropDownList ID="txtdept_priority" runat="server">
                                    <asp:ListItem Value="normal">Normal</asp:ListItem>
                                    <asp:ListItem Value="low">Low</asp:ListItem>

                                    <asp:ListItem Value="high">High</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDeptCommentDetail" Width="90%" Height="80px"
                                    Style="overflow: scroll" runat="Server" Text=" "
                                    ForeColor="#000099" Font-Bold="True" /></td>
                        </tr>

                        <tr>
                            <td>
                                <table cellpadding="2" cellspacing="1" width="100%">
                                    <tr>
                                        <td width="80">
                                            <strong>Grand Topic</strong></td>
                                        <td width="300">

                                            <asp:Label ID="lblGrandTopic" runat="server" Text="-"></asp:Label>

                                        </td>
                                        <td width="80">
                                            <strong>Subtopic 1</strong></td>
                                        <td>

                                            <asp:Label ID="lblSubtopic1" runat="server" Text="-"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Topic</strong></td>
                                        <td>
                                            <asp:Label ID="lblTopic" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td>
                                            <strong>Subtopic 2</strong></td>
                                        <td>
                                            <asp:Label ID="lblSubtopic2" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCFBtqm11" runat="server" Text="Subtopic 3" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSubtopic3" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="panel_department" runat="server">
                        <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                            <tr>
                                <td valign="top">
                                    <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                                        <tr>
                                            <td width="200">
                                                <asp:Label ID="Label2" runat="server" Text="Action for Customer Feedback"></asp:Label></td>
                                            <td width="23">
                                                <input type="checkbox" id="dept_chk_feedback1" runat="server" />
                                            </td>
                                            <td width="180">
                                                <asp:Label ID="Label3" runat="server" Text="Response back to client"></asp:Label>
                                            </td>
                                            <td width="23">
                                                <input type="checkbox" id="dept_chk_feedback2" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Investigate and sending back to TQM"></asp:Label>&nbsp;
                          <asp:DropDownList ID="dept_feedback_within" runat="server" Width="150px">
                              <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
                              <asp:ListItem Value="1">Within 24 hours</asp:ListItem>
                              <asp:ListItem Value="2">Within 72 hours</asp:ListItem>
                          </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCFBdept4" runat="server" Text="Investigation"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                    <textarea id="txtinvestigation" runat="server" cols="45" rows="5" style="width: 735px"></textarea>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>ระบุสาเหตุ / Cause of CFB</td>
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
          </asp:GridView><br />
 <asp:Button ID="cmdDeptAddCause" runat="server" Text="Add Cause" Width="110px" CausesValidation="False" />

                                </td>
                            </tr>
                           
                            <tr>
                                <td>
                                    <asp:Label ID="lblCFBdept5" runat="server" Text="Cause" Visible="false"></asp:Label>
                                    หมายเหตุ / Remark
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <textarea id="txtcause" runat="server" cols="45" rows="5" style="width: 735px"></textarea> </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCFBdept6" runat="server" Text="Corrective &amp; Prevention Action"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <textarea id="txtcorrective" runat="server" cols="45" rows="5" style="width: 735px"></textarea> </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCFBdept7" runat="server" Text="Result from responding to client"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="2" cellspacing="1" style="margin-left: -3px;" width="100%">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="txtresult" runat="server">
                                                    <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                    <asp:ListItem Value="1">Satisfied</asp:ListItem>
                                                    <asp:ListItem Value="2">Unsatisfied and requested to further action (please identify)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="23">
                                                <textarea id="txtresult_detail" runat="server" cols="45" rows="5" style="width: 735px"></textarea> </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>


                                            <asp:GridView ID="GridViewPrevent" runat="server" Width="100%"
                                                AutoGenerateColumns="False" CssClass="tdata" CellPadding="3"
                                                DataKeyNames="prevent_dept_id" HeaderStyle-CssClass="colname"
                                                EmptyDataText="">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">

                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPk" runat="server" Text='<%# bind("prevent_dept_id") %>' Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtorder" runat="server" Width="50px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="action_detail"
                                                        HeaderText="Action Plans ">
                                                        <ItemStyle Width="450px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="date_start" HeaderText="Start"
                                                        DataFormatString="{0:dd MMM yyyy}">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="date_end" HeaderText="Completed" DataFormatString="{0:dd MMM yyyy}" />
                                                    <asp:BoundField DataField="resp_person" HeaderText="Responsible Person" />
                                                    <asp:CommandField ShowDeleteButton="True">
                                                        <ItemStyle Width="80px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <HeaderStyle CssClass="colname" />
                                            </asp:GridView>
                                            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                                                <%If GridViewPrevent.Rows.Count = 0 Then%>
                                                <tr>
                                                    <td valign="top" width="60" class="colname"
                                                        style="font-weight: bold; height: 24px;">No.</td>
                                                    <td valign="top" width="450" class="colname"
                                                        style="font-weight: bold; height: 24px;">Action Plans</td>
                                                    <td valign="top" width="80" class="colname"
                                                        style="font-weight: bold; height: 24px;">Start</td>
                                                    <td valign="top" width="80" class="colname"
                                                        style="font-weight: bold; height: 24px;">Completed</td>
                                                    <td valign="top" class="colname" style="font-weight: bold; height: 24px;">Responsible Person</td>
                                                </tr>
                                                <%End If%>
                                                <tr>
                                                    <td valign="top" width="60">&nbsp;
                                                    </td>
                                                    <td valign="top" width="450">
                                                        <textarea id="txt_addprevent" runat="server" cols="45" name="txt_addprevent"
                                                            rows="2" style="width: 350px"></textarea></td>
                                                    <td valign="top" width="100">
                                                        <asp:TextBox ID="txtdate_prevent1" runat="server" Width="80px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_prevent1">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="top" width="100">
                                                        <asp:TextBox ID="txtdate_prevent2" runat="server" Width="80px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_prevent2">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td valign="top">

                                                        <input id="txt_addperson" runat="server" name="txt_addperson" type="text" /></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Button ID="cmdAddPrevent" runat="server" Text="Add Topic" CausesValidation="False" />
                                            <asp:Button ID="cmdPreventOrder" runat="server" Text="Save Order" CausesValidation="False" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>

            </div>


            <div class="tabbertab" id="tabTQM" runat="server">
                <h2>Part of IR&CFB </h2>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <a href="logbook.aspx" alt="Log book">View IR&CFB log book</a>
                        <fieldset>
                            <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                <tr>
                                    <td width="500"><strong>Comment No.</strong>
                                        <asp:DropDownList
                                            ID="txttqmcase" runat="server" DataValueField="comment_id"
                                            DataTextField="order_sort" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txttqm_complaintype" runat="server" Width="150px" ReadOnly="True"></asp:TextBox></td>
                                    <td style="text-align: right">
                                        <asp:DropDownList ID="txtcomboowner" runat="server" Width="235px" AutoPostBack="True">
                                            <asp:ListItem Value="">-</asp:ListItem>
                                                                
														<asp:ListItem Value="Burassakorn Tajama">Burassakorn Tajama</asp:ListItem>
                                            <asp:ListItem Value="Chonpitcha Barnyen">Chonpitcha Barnyen</asp:ListItem>
                                            <asp:ListItem Value="Sasithorn Sutinperk">Sasithorn Sutinperk</asp:ListItem>
                                          
                         <asp:ListItem Value="Wipamol Ruangsri">Wipamol Ruangsri</asp:ListItem> 
                        <asp:ListItem Value="Pattama Nitithanasombat">Pattama Nitithanasombat</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="500">&nbsp;</td>
                                    <td style="text-align: right">Owner&nbsp;
                         <asp:TextBox ID="txttqm_owner" runat="server" Width="230px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="panelTQM" runat="server">
                                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                    <tr>
                                        <td valign="top">
                                            <table>
                                                <tr>
                                                    <td width="150">
                                                        <asp:CheckBox ID="txttqm_followup" runat="server" Text=" Case Follow-up" /></td>
                                                    <td width="20">Remark</td>
                                                    <td width="150">
                                                        <asp:TextBox ID="txttqm_follow_remark" runat="server"></asp:TextBox></td>
                                                    <td width="20">Date</td>
                                                    <td>
                                                        <asp:TextBox ID="txttqm_follow_date" runat="server"></asp:TextBox>
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender2"
                                                            TargetControlID="txttqm_follow_date"
                                                            Mask="99/99/9999"
                                                            MessageValidatorTip="true"
                                                            OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError"
                                                            MaskType="Date"
                                                            InputDirection="RightToLeft"
                                                            AcceptNegative="Left"
                                                            ErrorTooltipEnabled="True" runat="server" />
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txttqm_follow_date" PopupButtonID="Image5">
                                                        </asp:CalendarExtender>
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor" />
                                                    </td>
                                                </tr>
                                            </table>


                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblTQMCommentDetail" Width="90%" Height="80px"
                                                Style="overflow: scroll" runat="Server" Text=" "
                                                ForeColor="#000099" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">&nbsp;<table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
                                            <tr>
                                                <td width="200">
                                                    <asp:Label ID="lblCFBdept1" runat="server" Text="Action for Customer Feedback"></asp:Label></td>
                                                <td width="23">
                                                    <input type="checkbox" id="chk_feedback1" runat="server" />
                                                </td>
                                                <td width="180">
                                                    <asp:Label ID="lblCFBdept2" runat="server" Text="Response back to client"></asp:Label>
                                                </td>
                                                <td width="23">
                                                    <input type="checkbox" id="chk_feedback2" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCFBdept3" runat="server" Text="Investigate and sending back to TQM"></asp:Label>&nbsp;
                          <asp:DropDownList ID="txtfeedback_within" runat="server" Width="150px">
                              <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
                              <asp:ListItem Value="1">Within 24 hours</asp:ListItem>
                              <asp:ListItem Value="2">Within 72 hours</asp:ListItem>
                          </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td valign="top">
                                            <table width="100%" cellspacing="1" cellpadding="2">
                                                <tr>
                                                    <td width="80">Grand Topic</td>
                                                    <td>
                                                        <asp:DropDownList ID="txtgrandtopic" runat="server" AutoPostBack="True"
                                                            DataTextField="grand_topic_name" DataValueField="grand_topic_id" Width="350px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="80">Subtopic 1</td>
                                                    <td>
                                                        <asp:DropDownList ID="txtsubtopic1" runat="server"
                                                            DataTextField="subtopic_name" DataValueField="ir_subtopic_id"
                                                            Width="350px" AutoPostBack="True" Style="margin-bottom: 0px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Topic</td>
                                                    <td>
                                                        <asp:DropDownList ID="txtnormaltopic" runat="server" AutoPostBack="True"
                                                            DataTextField="topic_name" DataValueField="ir_topic_id" Width="350px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>Subtopic 2</td>
                                                    <td>
                                                        <asp:DropDownList ID="txtsubtopic2" runat="server"
                                                            DataTextField="subtopic2_name_en" DataValueField="ir_subtopic2_id" AutoPostBack="true" Width="350px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCFBtqm1" runat="server" Text="Subtopic 3"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="txtsubtopic3" runat="server" Width="350px" DataTextField="subtopic3_name_en" DataValueField="ir_subtopic3_id">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top">Topic Detail</td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txttqmcfb_detail" runat="server" TextMode="MultiLine"
                                                            Width="650px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />

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
                                                        <br />
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
                                                    <td valign="top" width="80">&nbsp;</td>
                                                    <td valign="top">
                                                        <strong>Defendant Unit</strong></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" width="352">
                                                        <asp:ListBox ID="txtunit_defandent_all" runat="server"
                                                            DataTextField="dept_unit_name" DataValueField="dept_unit_id" Width="250px"></asp:ListBox>
                                                    </td>
                                                    <td valign="top" width="80">
                                                        <asp:Button ID="cmdUnitAdd" runat="server" Text="&gt;&gt;"
                                                            Style="height: 26px" />
                                                        <br />
                                                        <asp:Button ID="cmdUnitRemove" runat="server" Text="&lt;&lt;" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:ListBox ID="txtunit_defandent_select" runat="server"
                                                            DataTextField="dept_unit_name" DataValueField="dept_unit_id" Width="250px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>

                                            <br />
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView ID="GridViewTQMDoctor" runat="server" AutoGenerateColumns="False"
                                                            CellPadding="4" GridLines="Horizontal" Width="850px" BackColor="White"
                                                            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                                                            DataKeyNames="defendant_id" EnableModelValidation="True"
                                                            EmptyDataText="There is no Involving Physician">
                                                            <RowStyle BackColor="White" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No.">
                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Labels" runat="server"> <%# Container.DataItemIndex + 1 %>. </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="doctor_name" HeaderText="Involving Physician" />
                                                                <asp:BoundField DataField="dept_unit_name" HeaderText="Unit" />
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                                                            CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete? ')"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="White" ForeColor="#333333" />
                                                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>

                                            <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblIRtqm8" Text="Involving Physician" runat="server" /></td>
                                                    <td>
                                                        <asp:TextBox ID="txttqm_finddoctor" runat="server" Width="300px"
                                                            BackColor="#66FF99"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="2" ServicePath="~/DoctorService.asmx" ServiceMethod="getDoctor" CompletionSetCount="5"
                                                            TargetControlID="txttqm_finddoctor" OnClientItemSelected="IAmSelected2" EnableViewState="false">
                                                        </asp:AutoCompleteExtender>
                                                        MD Code
                                                        <asp:TextBox ID="txtmdcode" runat="server" BackColor="#66FF99"></asp:TextBox>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Defendant Unit</td>
                                                    <td>
                                                        <asp:DropDownList ID="txtadd_doctor_defentdant_unit" runat="server"
                                                            DataTextField="dept_unit_name" DataValueField="dept_unit_id"
                                                            BackColor="#66FF99">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="cmdTQMAddDoctor" runat="server" Text="Add Involving Physician"  CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:GridView ID="GridTQMCause" runat="server" Width="100%"
                                                AutoGenerateColumns="False" CssClass="tdata" CellPadding="3"
                                                DataKeyNames="tqm_cause_id" HeaderStyle-CssClass="colname"
                                                EnableModelValidation="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">

                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPk" runat="server" Text='<%# bind("tqm_cause_id") %>' Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtorder" runat="server" Width="50px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cause_name"
                                                        HeaderText="Cause of Incident">
                                                        <ItemStyle Width="350px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemStyle Width="200px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("cause_remark") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="dept_unit_name" HeaderText="Unit Defendant"></asp:BoundField>
                                                    <asp:CommandField ShowDeleteButton="True">
                                                        <ItemStyle Width="80px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <HeaderStyle CssClass="colname" />
                                            </asp:GridView>
                                            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                                                <%If GridTQMCause.Rows.Count = 0 Then%>
                                                <tr>
                                                    <td valign="top" width="60" class="colname" style="font-weight: bold">No.</td>
                                                    <td valign="top" width="350" class="colname" style="font-weight: bold">Cause of Incident</td>
                                                    <td valign="top" width="200" class="colname" style="font-weight: bold">Description</td>
                                                    <td valign="top" class="colname" style="font-weight: bold">Unit Defendant</td>
                                                    <td valign="top" class="colname" style="font-weight: bold" width="80"></td>
                                                </tr>
                                                <%End If%>
                                                <tr style="vertical-align: top">
                                                    <td width="60"></td>
                                                    <td valign="top" width="350">
                                                        <asp:DropDownList ID="txtcfb_cause" runat="server">
                                                            <asp:ListItem Value="0">--- Please Select ---</asp:ListItem>
                                                            <asp:ListItem Value="1">Personal / Human Error</asp:ListItem>
                                                            <asp:ListItem Value="2">Communication</asp:ListItem>
                                                            <asp:ListItem Value="3">System Error</asp:ListItem>
                                                            <asp:ListItem Value="4">Equipment Error</asp:ListItem>
                                                            <asp:ListItem Value="5">Enviroment</asp:ListItem>
                                                            <asp:ListItem Value="6">Poor Practice Habit</asp:ListItem>
                                                            <asp:ListItem Value="7">Others</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                    <td width="200">
                                                        <asp:TextBox ID="txttqm_addremark" runat="server" Rows="3" TextMode="MultiLine"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="txtadd_cause_defendant" runat="server"
                                                            DataTextField="dept_unit_name"
                                                            DataValueField="dept_unit_id">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="80"></td>
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
                            <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtadd_datetqm1" />
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image7" TargetControlID="txtadd_datetqm1">
                            </asp:CalendarExtender>
                            <asp:Image ID="Image7" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                        </td>
                        <td valign="top" width="100">
                            <asp:TextBox ID="txtadd_datetqm2" runat="server" Width="60px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtadd_datetqm2" />
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image8" TargetControlID="txtadd_datetqm2">
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
          <asp:Button ID="cmdAddPreventFromDept" runat="server" Text="Copy Action Plan from Mgr." Width="180px" Visible="true"  CausesValidation="False" />
        </td>
    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <input type="checkbox" id="chk_tqm_contact" runat="server" />
                                                    </td>
                                                    <td width="265">
                                                        <asp:Label ID="lblCFBtqm2" runat="server"
                                                            Text="Contacted customer for initial responding"></asp:Label></td>
                                                    <td width="60">
                                                        <asp:Label ID="lblCFBtqm3" runat="server" Text="Method"></asp:Label>
                                                    </td>
                                                    <td width="100">
                                                        <asp:DropDownList ID="txtmedthod" runat="server">
                                                            <asp:ListItem Value="">- Please select -</asp:ListItem>
                                                            <asp:ListItem Value="Email">Email</asp:ListItem>
                                                            <asp:ListItem Value="Telephone">Telephone</asp:ListItem>
                                                            <asp:ListItem Value="Letter">Letter</asp:ListItem>
                                                            <asp:ListItem Value="Face to face">Face to face</asp:ListItem>
                                                            <asp:ListItem Value="Other">Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="40">
                                                        <asp:Label ID="lblCFBtqm4" runat="server" Text="Date"></asp:Label>
                                                    </td>
                                                    <td width="130">
                                                        <asp:TextBox ID="txttqm_date" runat="server" Width="80px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txttqm_date_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txttqm_date">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td width="40">
                                                        <asp:Label ID="lblCFBtqm5" runat="server" Text="Time"></asp:Label>
                                                    </td>
                                                    <td width="110">
                                                        <asp:DropDownList ID="txthour2" runat="server">
                                                        </asp:DropDownList>
                                                        :
                              <asp:DropDownList ID="txtmin2" runat="server">
                              </asp:DropDownList>
                                                    </td>
                                                    <td width="60">
                                                        <asp:Label ID="lblCFBtqm6" runat="server" Text="Duration"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <input id="txtduration" runat="server" style="width: 30px;" type="text" />
                                                        <asp:Label ID="lblCFBtqm7" runat="server" Text="mins"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Response by
                          <asp:TextBox ID="txt_response" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="23">
                                                        <input id="chk_tqmother" runat="server" type="checkbox" visible="False" />
                                                    </td>
                                                    <td width="150">
                                                        <asp:Label ID="lblCFBtqm8" runat="server" Text="Other (please identify)" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <input id="txttqm_other" runat="server" style="width: 678px" type="text" visible="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblCFBtqm9" runat="server" Text="Details"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <textarea id="txttqm_detail" runat="server" cols="45" rows="5"
                                                style="width: 850px"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="173">
                                                        <asp:Label ID="lblCFBtqm10" runat="server" Text="Customer"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="txttqm_customer" runat="server" Width="335px">
                                                            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                            <asp:ListItem Value="1">Satisfied and not request to response back</asp:ListItem>
                                                            <asp:ListItem Value="2">Satisfied and requested to respond back</asp:ListItem>
                                                            <asp:ListItem Value="3">Dissatisfied and not request to response back</asp:ListItem>
                                                            <asp:ListItem Value="4">Dissatisfied and requested to respond back</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <textarea id="txttqm_customer_detail" runat="server" cols="45" rows="5"
                                                style="width: 850px"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Contribute factor</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <textarea id="txttqm_factor" runat="server" cols="45" name="S1" rows="5"
                                                style="width: 850px"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Corrective action</td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <textarea id="txttqm_corrective" runat="server" cols="45" name="S2" rows="5"
                                                style="width: 850px"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="150">
                                                        <asp:DropDownList ID="txtreporttype" runat="server">
                                                            <asp:ListItem Value="0">New Report</asp:ListItem>
                                                            <asp:ListItem Value="1">Additional Report</asp:ListItem>
                                                            <asp:ListItem Value="2">Repeated Report</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="60">CFB No.</td>
                                                    <td>
                                                        <asp:TextBox ID="txttqm_cfbno" runat="server"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttqm_cfbno"
                                                            ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>

                        <br />
                        <asp:Panel ID="panelTQM2" runat="server">
                            <fieldset>
                                <legend>CFB evaluation</legend>
                                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                    <tr>
                                        <td width="170" valign="top">Group of report</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="Group_of_report" ID="txtrepgr1" />
                                                    </td>
                                                    <td width="100">Direct</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="Group_of_report" ID="txtrepgr2" />
                                                    </td>
                                                    <td>Indirect</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Service Recovery Criteria</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="ServiceRcoveryCriteria" ID="txtsrcritia1" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="ServiceRcoveryCriteria" ID="txtsrcritia2" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Service Recovery Success</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="ServiceRcoverySuccess" ID="txtsrsuccess1" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="ServiceRcoverySuccess" ID="txtsrsuccess2" />
                                                    </td>
                                                    <td width="100">No</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="ServiceRcoverySuccess" ID="txtsrsuccess3" /></td>
                                                    <td>N/A</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Success by</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtlog_successby" runat="server" Width="335px">
                                                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                                                <asp:ListItem Value="MGR">MGR</asp:ListItem>
                                                <asp:ListItem Value="ADD/DD/CD/DMG">ADD/DD/CD/DMG</asp:ListItem>
                                                <asp:ListItem Value="TQM staff">TQM staff</asp:ListItem>
                                                <asp:ListItem Value="MCO">MCO</asp:ListItem>
                                                <asp:ListItem Value="Executive">Executive</asp:ListItem>
                                                <asp:ListItem Value="MDCL">MDCL</asp:ListItem>
                                                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                                <asp:ListItem Value="Sup (Admin)">Sup (Admin)</asp:ListItem>
                                                <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Owner unit</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="OwnerUnit" ID="txtowner1" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="OwnerUnit" ID="txtowner2" />
                                                    </td>
                                                    <td width="100">No</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="OwnerUnit" ID="txtowner3" /></td>
                                                    <td>N/A</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Success within</td>
                                        <td valign="top">
                                            <input type="text" name="successwithin" id="successwithin" style="width: 335px;" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">New pt</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="Newpt" ID="txtnewpt1" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="Newpt" ID="txtnewpt2" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Service place</td>
                                        <td valign="top">
                                            <input type="text" name="serviceplace" id="serviceplace" style="width: 335px;" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Change to other hospital</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="NewHospital" ID="txtnewhosp1" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton runat="server" GroupName="NewHospital" ID="txtnewhosp2" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Refer hospital name</td>
                                        <td valign="top">
                                            <input type="text" name="newhopsname" id="newhospname" style="width: 335px;" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Customer objective</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtlog_customer_object" runat="server" Width="335px">
                                                <asp:ListItem Value="Not specifed">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="Obtain a refund/ compensation">Obtain a refund/ compensation</asp:ListItem>
                                                <asp:ListItem Value="Receive an explaination">Receive an explaination</asp:ListItem>
                                                <asp:ListItem Value="Register their concern/ doesn't want reply but still want action">Register their concern/ doesn't want reply but still want action</asp:ListItem>
                                                <asp:ListItem Value="Obtain an apology">Obtain an apology</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Resolution</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtlog_resolution" runat="server" Width="335px">
                                                <asp:ListItem Value="Not specified">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="Cost refunded">Cost refunded</asp:ListItem>
                                                <asp:ListItem Value="Explaination provided">Explaination provided</asp:ListItem>
                                                <asp:ListItem Value="Concerned registered">Concerned registered</asp:ListItem>
                                                <asp:ListItem Value="Apology provided from manager involved">Apology provided from manager involved</asp:ListItem>
                                                <asp:ListItem Value="Staff counselled/ offered performance support">Staff counselled/ offered performance support</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Action Taken</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtlog_action" runat="server" Width="335px">
                                                <asp:ListItem Value="Not specified">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="Recommendation are made">Recommendation are made</asp:ListItem>
                                                <asp:ListItem Value="QI activity, including risk management">QI activity, including risk management</asp:ListItem>
                                                <asp:ListItem Value="QI activity, including MAFF">QI activity, including MAFF</asp:ListItem>
                                                <asp:ListItem Value="Policy written or modified">Policy written or modified</asp:ListItem>
                                                <asp:ListItem Value="Procedure/ practice modified">Procedure/ practice modified</asp:ListItem>
                                                <asp:ListItem Value="Training/ education of staff provided">Training/ education of staff provided</asp:ListItem>
                                                <asp:ListItem Value="Staff counselled or offered performance support">Staff counselled or offered performance support</asp:ListItem>
                                                <asp:ListItem Value="Dutied changed">Dutied changed</asp:ListItem>
                                                <asp:ListItem Value="No further action required">No further action required</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                    <tr>
                                        <td width="170" valign="top">Involve</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txttqm_concern" runat="server">
                                                <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="1">Clinic</asp:ListItem>
                                                <asp:ListItem Value="2">Service</asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Clinical Risk Level</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txttqm_clinic" runat="server">
                                                <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="1">Near miss (0)</asp:ListItem>
                                                <asp:ListItem Value="2">No harm (1)</asp:ListItem>
                                                <asp:ListItem Value="3">Mild AE (2)</asp:ListItem>
                                                <asp:ListItem Value="4">Moderate AE (3)</asp:ListItem>
                                                <asp:ListItem Value="5">Serious AE (4)</asp:ListItem>
                                                <asp:ListItem Value="6">Sentinel Event (5)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Service Risk Level</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txttqm_service" runat="server">
                                                <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="1">Register (1)</asp:ListItem>
                                                <asp:ListItem Value="2">Explain (2)</asp:ListItem>
                                                <asp:ListItem Value="3">Apology (3)</asp:ListItem>
                                                <asp:ListItem Value="4">Apology (3)</asp:ListItem>
                                                <asp:ListItem Value="5">Refund (4)</asp:ListItem>
                                                <asp:ListItem Value="6">Sentinel Event (5)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Refer to Risk Management</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtrisk1" GroupName="risk" runat="server" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtrisk2" GroupName="risk" runat="server" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Refer to IMCO</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtimco1" GroupName="imco" runat="server" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtimco2" GroupName="imco" runat="server" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Quality Concern</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtqc1" GroupName="concern" runat="server" />
                                                    </td>
                                                    <td width="100">Yes</td>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtqc2" GroupName="concern" runat="server" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Final Outcome</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtoutcome" runat="server">
                                                <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                <asp:ListItem Value="1">Satisfied</asp:ListItem>
                                                <asp:ListItem Value="2">Dissatisfied</asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Person</td>
                                        <td valign="top">
                                            <asp:RadioButtonList ID="txtlog_person" runat="server"
                                                RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Area Code</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="txtlog_areacode" runat="server">
                                                <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Remark</td>
                                        <td valign="top">
                                            <textarea id="txttqm_remark" runat="server" cols="45" rows="5"
                                                style="width: 735px;"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" width="150">&nbsp;</td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chk_write" runat="server" Text="Write Off" Width="100px" /></td>
                                                    <td width="100" style="text-align: right">
                                                        <asp:TextBox ID="txtwrite_bath" runat="server" Width="75px"></asp:TextBox>
                                                    </td>
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
                                        <td style="vertical-align: top" width="150">&nbsp;</td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chk_refund" runat="server" Text="Refund" Width="100px" />
                                                    </td>
                                                    <td width="100" style="text-align: right">
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
                                        <td style="vertical-align: top" width="150">&nbsp;</td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chk_remove" runat="server" Text="Remove" Width="100px" />
                                                    </td>
                                                    <td width="100" style="text-align: right">
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
                                        <td style="vertical-align: top" width="150">Related BI Way</td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtbiway1" runat="server" GroupName="biway" />
                                                    </td>
                                                    <td width="50">Yes</td>
                                                    <td width="100">
                                                        <asp:DropDownList ID="txtbiway" runat="server">
                                                            <asp:ListItem Value="">-</asp:ListItem>
                                                            <asp:ListItem Value="1">Care</asp:ListItem>
                                                            <asp:ListItem Value="2">Clear</asp:ListItem>
                                                            <asp:ListItem Value="3">Smart</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="23">
                                                        <asp:RadioButton ID="txtbiway2" runat="server" GroupName="biway" />
                                                    </td>
                                                    <td>No</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" width="150">Related standard</td>
                                        <td valign="top">
                                          <asp:TextBox type="text" name="txtrelated_standard" id="txtrelated_standard" style="width: 285px" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top" width="150">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <fieldset>
                                <legend>Related Incident Report</legend>
                                <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                                    <tr>
                                        <td valign="top">
                                            <table width="100%" cellspacing="1" cellpadding="2">
                                                <tr>
                                                    <td width="100"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtrefno" runat="server" Width="235px"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtrefno"
                                                            ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <br />

            </div>

            <div class="tabbertab" id="tabPSM" runat="server">
                <h2>Risk Management</h2>
                <fieldset>
                    <asp:Panel ID="panel_psm" runat="server">
                        <table width="100%" cellpadding="2" cellspacing="1" style="margin: 8px 10px;">
                            <tr>
                                <td width="160" valign="top">Attach files</td>
                                <td valign="top">

                                    <table>
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                            <asp:FileUpload ID="FileUpload3" runat="server" Width="535" />
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
                                                                <a href='../share/mco/cfb/<%# Eval("file_path") %>'
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
                                <fieldset>
                                    <legend><strong>Risk Management Concern list</strong> </legend>
                                    <table width="100%" cellpadding="2" cellspacing="1" style="margin: 8px 10px;">
                                        <tr>
                                            <td width="160" valign="top">Concern</td>
                                            <td valign="top">
                                                <asp:Button ID="cmdPSMAddConcern" runat="server" Text="Add concern" CausesValidation="False"  />
                                                &nbsp;</td>
                                        </tr>

                                    </table>
                                    <asp:Panel ID="panel_psm_concern" runat="server" Visible="false"
                                        BackColor="#CCCCFF">
                                        <table width="100%" cellpadding="2" cellspacing="1" style="margin: 8px 10px;">
                                            <tr>
                                                <td valign="top">Concern detail</td>
                                                <td valign="top">
                                                    <textarea id="txtpsm_concern" cols="45" name="txtpsm_concern" rows="5" runat="server"
                                                        style="width: 735px"></textarea></td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Final comment category</td>
                                                <td valign="top">
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="300">
                                                                <asp:DropDownList ID="txtpsm_category" runat="server" Width="285px"
                                                                    AutoPostBack="True" DataTextField="psm_category_name"
                                                                    DataValueField="psm_category_id">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="90">Subcategory</td>
                                                            <td>
                                                                <asp:DropDownList ID="txtpsm_subcategory" runat="server" Width="285px"
                                                                    DataTextField="subcategory_name" DataValueField="psm_sub_category_id">
                                                                </asp:DropDownList>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Standard of care</td>
                                                <td valign="top">
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="20">
                                                                <asp:RadioButton ID="txtpsm_std1" runat="server" GroupName="psm_std" /></td>

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
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">Defendant&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Physician</span></td>
                                                <td valign="top">
                                                    <label></label>
                                                    <label></label>
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="370">
                                                                <asp:TextBox ID="txtpsm_add_doctor" runat="server" Width="335px"></asp:TextBox>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                                    DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/DoctorService.asmx" ServiceMethod="getDoctor2" CompletionSetCount="5"
                                                                    TargetControlID="txtpsm_add_doctor" EnableViewState="false" OnClientItemSelected="IAmSelected">
                                                                </asp:AutoCompleteExtender>
                                                                &nbsp;<img src="../images/spellcheck.png" width="16" height="16" /></td>
                                                            <td width="60">Specialty</td>
                                                            <td>
                                                                <input name="txtpsm_add_special" type="text" id="txtpsm_add_special" style="width: 230px;" runat="server" />
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
                    <asp:Button ID="cmdPSMAddDept" runat="server" Text="Add" CausesValidation="False" />
                                                    <asp:Button ID="cmdPSMRemoveDept" runat="server" Text="Delete" CausesValidation="False" />

                                                    <asp:HiddenField ID="txtpsm_add_deptid" runat="server" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">&nbsp;</td>
                                                <td valign="top">

                                                    <asp:ListBox ID="txtpsm_list_dept" runat="server"
                                                        DataTextField="doctor_name" DataValueField="doctor_name" Width="735px"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">&nbsp;</td>
                                                <td valign="top">&nbsp;</td>
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
                                    <asp:GridView ID="GridConcern" runat="server" Width="90%" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" CssClass="tdata" CellPadding="3"
                                        DataKeyNames="concern_id" HeaderStyle-CssClass="colname"
                                        EnableModelValidation="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">

                                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPk" runat="server" Text='<%# bind("concern_id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="Labels" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="concern_detail"
                                                HeaderText="Concern">
                                                <ItemStyle Width="350px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="topic_name" HeaderText="Category"></asp:BoundField>
                                            <asp:BoundField HeaderText="Subcategory" DataField="subtopic_name" />
                                            <asp:BoundField HeaderText="Standard of care" DataField="std_care_type_name" />
                                            <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <input class="detailButton" name="btnDetail"
                                                        onclick='openConcern(<%# Eval("concern_id") %>)'
                                                        type="button" value="Detail.." />
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
                                <table width="100%" cellpadding="2" cellspacing="1" style="margin: 8px 10px;">
                                    <tr>
                                        <td width="160" valign="top">Resolution</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="1" cellpadding="2">
                                                <tr>
                                                    <td width="23">
                                                        <input type="checkbox" name="checkbox15" id="chk_reso1" runat="server" />

                                                    </td>
                                                    <td width="200">Verbal explanation</td>
                                                    <td width="20">
                                                        <input type="checkbox" name="checkbox20" id="chk_reso2" runat="server" /></td>
                                                    <td>Written explantion</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="checkbox" name="checkbox16" id="chk_reso3" runat="server" /></td>
                                                    <td>Verbal apology </td>
                                                    <td>
                                                        <input type="checkbox" name="checkbox17" id="chk_reso4" runat="server" /></td>
                                                    <td>Written apology</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="checkbox" name="checkbox18" id="chk_reso5" runat="server" /></td>
                                                    <td>Goodwill Gesture</td>
                                                    <td>
                                                        <input type="checkbox" name="checkbox19" id="chk_reso6" runat="server" /></td>
                                                    <td>Compenstion</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Compensation Detail
                                            <br />
                                            (if applicable)</td>
                                        <td valign="top">
                                            <textarea name="txtpsm_compensation" id="txtpsm_compensation" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Patient Satisfaction</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="1" cellpadding="2">
                                                <tr>
                                                    <td width="20">
                                                        <asp:RadioButton ID="txtpsm_pt1" runat="server" GroupName="psm_satis" />
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
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Legal Implication</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="1" cellpadding="2">
                                                <tr>
                                                    <td width="23">
                                                        <input type="checkbox" name="chk_psm_refer" id="chk_psm_refer" runat="server" />
                                                    </td>
                                                    <td>refer to legal team</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Recommendation</td>
                                        <td valign="top">
                                            <textarea name="txtpsm_recommend" id="txtpsm_recommend" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Remark</td>
                                        <td valign="top">
                                            <textarea name="txtpsm_remark" id="txtpsm_remark" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td valign="top">MCO-MS Work Status</td>
                                        <td valign="top">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="235">
                                                        <asp:DropDownList ID="txtpsm_status" runat="server">
                                                            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
                                                            <asp:ListItem Value="1">Handled by MCO-MS</asp:ListItem>
                                                            <asp:ListItem Value="2">Assisted by MCO-MS</asp:ListItem>
                                                            <asp:ListItem Value="3">Screening and return to TQM</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td width="80">Closed Date </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpsm_date_expect" runat="server" Width="170px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtdate_assess_CalendarExtender" runat="server"
                                                            ClearTime="True" Enabled="True" TargetControlID="txtpsm_date_expect"
                                                            Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Responsible Person</td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtpsm_add_person" runat="server" Width="385px"></asp:TextBox>

                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/EmployeeService.asmx" ServiceMethod="getEmployee" CompletionSetCount="5"
                                                TargetControlID="txtpsm_add_person" EnableViewState="false">
                                            </asp:AutoCompleteExtender>
                                            &nbsp;<asp:Button ID="cmdAddPSMPerson" runat="server" Text="Add" CausesValidation="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">&nbsp;</td>
                                        <td valign="top">
                                            <textarea id="txtpsm_response" runat="server" cols="45" name="txtpsm_remark0"
                                                rows="5" style="width: 435px"></textarea></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </fieldset>
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
                                <asp:Label ID="Labels" runat="server">
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
                        <td width="150" style="vertical-align: top">Additional information</td>
                        <td style="vertical-align: top">
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
        <div align="right">
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                <asp:Button ID="cmdDraft" runat="server" Text="Save draft" OnCommand="onSave"
                    CommandArgument="1" ForeColor="#FF3300" CausesValidation="False" />
                <asp:Button ID="cmdSubmit" runat="server" Text="Submit" OnCommand="onSave"
                    CommandArgument="2" Font-Bold="True" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                <asp:Button ID="cmdTQMDraft1" runat="server" Text="Save draft" OnCommand="onSave"
                    CommandArgument="" ForeColor="#FF3300" CausesValidation="False" />
                <asp:Button ID="cmdTQMView1" runat="server" Text="Save revision" ForeColor="#000099" Font-Bold="True" CausesValidation="False" />
                <asp:Button ID="cmdTQMClose1" runat="server" Text="Close CFB" OnCommand="onSave"
                    CommandArgument="9" Font-Bold="True" CausesValidation="False" />
                <asp:Button ID="cmdDeptReturn1" runat="server" Text="Save and return case to TQM" OnCommand="onSave"
                    CommandArgument="7" Font-Bold="True" CausesValidation="False" />
                <asp:Button ID="cmdPSMReturn1" runat="server" Text="Save and return case to TQM" OnCommand="onSave"
                    CommandArgument="8" Font-Bold="True" CausesValidation="False" />
            </asp:Panel>
        </div>
    </div>

</asp:Content>
