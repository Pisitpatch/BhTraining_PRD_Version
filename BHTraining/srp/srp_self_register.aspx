<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_self_register.aspx.vb" Inherits="srp_srp_self_register" %>

<%@ Import Namespace="ShareFunction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style type="text/css">
/* Picture Thumbnails */
#thumbnails ul
{
    width: 800px;
    list-style: none;
}
#thumbnails li
{
    text-align: center;
    display: inline;
    width: 200px;
    height: 160px;
    float: left;
    margin-bottom: 20px;
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
        $(function () {
            //	$.datepick.setDefaults({useThemeRoller: true,autoSize:true});
            var calendar = $.calendars.instance();

            $('#ctl00_ContentPlaceHolder1_txtdate1').calendarsPicker(
                {

                    showTrigger: '#calImg',
                    dateFormat: 'dd/mm/yyyy'
                }
                );

            $('#ctl00_ContentPlaceHolder1_txtdate2').calendarsPicker(
                {

                    showTrigger: '#calImg',
                    dateFormat: 'dd/mm/yyyy'
                }
                );

            $('#ctl00_ContentPlaceHolder1_txtdate').calendarsPicker(
           {

               showTrigger: '#calImg',
               dateFormat: 'dd/mm/yyyy'
           }
           );

            //	$('#inlineDatepicker').datepick({onSelect: showDate});
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
        function onSubmit() {
            if ($("#ctl00_ContentPlaceHolder1_txtadd_cardid").val() == "") {
                alert("กรุณาระบุหมายเลขการ์ด");
                $("#ctl00_ContentPlaceHolder1_txtadd_cardid").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtadd_award").val() == "") {
                alert("กรุณาระบุชื่อผู้แจกการ์ด");
                $("#ctl00_ContentPlaceHolder1_txtadd_award").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtadd_award_empcode").val() == "") {
                alert("กรุณาระบุรหัสพนักงานผู้แจกการ์ด");
                $("#ctl00_ContentPlaceHolder1_txtadd_award_empcode").focus();
                return false;
            }
            if ($("#ctl00_ContentPlaceHolder1_txtadd_detail_combo").val() == "") {
             //   alert("กรุณาระบุ Complementary Notes");
              //  $("#ctl00_ContentPlaceHolder1_txtadd_detail_combo").focus();
               // return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtadd_note").val() == "") {
                alert("กรุณาระบุรายละเอียด");
                $("#ctl00_ContentPlaceHolder1_txtadd_note").focus();
                return false;
            }
            if (confirm("Are you sure you want to register your card ?")) {
                return true;
            } else {
                return false;
            }

            return true;
        }
    </script>
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
    <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
    <script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

    <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
    <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {



            $("#ctl00_ContentPlaceHolder1_txtadd_award").autocomplete("../getUser.ashx", {
                matchContains: false,
                autoFill: false,
                mustMatch: false
            });

            $('#ctl00_ContentPlaceHolder1_txtadd_award').result(function (event, data, formatted) {
                // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
                var serial = data;
                // alert("serial ::" + serial[1]);

                $('#ctl00_ContentPlaceHolder1_txtadd_award_empcode').val(serial[1]);

            });
            $("#ctl00_ContentPlaceHolder1_txtadd_award").click(function () {
                $(this).select();
            });

            $("#ctl00_ContentPlaceHolder1_txtadd_award_empcode").autocomplete("../getUser.ashx", {
                matchContains: false,
                autoFill: false,
                mustMatch: false
            });

            $('#ctl00_ContentPlaceHolder1_txtadd_award_empcode').result(function (event, data, formatted) {
                // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
                var serial = data;
                // alert("serial ::" + serial[1]);
                $('#ctl00_ContentPlaceHolder1_txtadd_award_empcode').val(serial[1]);
                $('#ctl00_ContentPlaceHolder1_txtadd_award').val(serial[0]);

            });
            $("#ctl00_ContentPlaceHolder1_txtadd_award_empcode").click(function () {
                $(this).select();
            });

        });

    </script>
    <style type="text/css">
        .style1 {
            color: #FF0000;
        }

        .style3 {
            color: #FF0000;
            font-weight: bold;
        }

        .style4 {
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="display: none;">
        <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left: 5px; cursor: pointer" />
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table width="100%" cellpadding="2" cellspacing="1">
        <tr>
            <td>
                <div id="header">
                    <img src="../images/srp_logo_32.png" alt="1" width="32" height="32" />&nbsp;&nbsp;<strong>On the Spot Card Register</strong></div>
            </td>
        </tr>
    </table>
    <div id="data">


        <div id="div_search" runat="server">
            <table width="100%" cellpadding="2" cellspacing="1" class="tdata3">
                <tr>
                    <td colspan="4" class="theader" style="background-color: #11720c">
                        <img src="../images/sidemenu_circle.jpg" width="10" height="10" alt="bullet" />&nbsp;Search</td>
                </tr>
                <tr>
                    <td width="150" height="30"><strong>Date</strong></td>
                    <td width="250">
                        <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>

                        &nbsp;to&nbsp;<asp:TextBox
                            ID="txtdate2" runat="server" Width="80px"></asp:TextBox></td>
                    <td width="120">&nbsp;</td>
                    <td>&nbsp;
                    </td>
                </tr>

                <tr>
                    <td height="30"><strong>Card ID.</strong></td>
                    <td colspan="3">
                        <strong>
                            <asp:TextBox ID="txtfind_card" runat="server"></asp:TextBox>
                        </strong>
                    </td>
                </tr>

                <tr>
                    <td height="30"><strong>Award by</strong></td>
                    <td colspan="3">
                        <strong>
                            <asp:TextBox ID="txtfind_mgr" runat="server"></asp:TextBox>
                        </strong>
                    </td>
                </tr>

                <tr>
                    <td height="30" colspan="4">
                        <asp:Button ID="cmdSearch" runat="server" Text="Search"
                            CausesValidation="False" />
                        <asp:Button ID="cmdNew" runat="server" Text="Register On-The-Spot Card"
                            CausesValidation="False" Font-Bold="true" />
                    </td>
                </tr>
            </table>
                <div id="div_complete_register" runat="server" style="text-align: center; width: 100%; " align="center">
                    <br />
            <table width="500px" align="center">
                <tr>
                    <td align="center" style="text-align:center; font-size:16px; border: solid 2px green">

                     
                        <strong>ลงทะเบียนสำเร็จ<br />
                        </strong>
                        <br />
                        กรุณานำบัตรไปยื่นที่<br />
                        เคาน์เตอร์โครงการเชิดชู<br />
                        เกียรติอีกครั้งเพื่อรับรางวัล<br />
                        ในวันจันทร์ พุธ ศุกร์ เวลา<br />
                        08:30-15:00 ไม่มีพักเบรคเที่ยง<br />
                        <br />
                      Please submit card(s) at 
                        <br />
                        the Staff Recognition Program counter
                        <br />
                        to get reward on Monday, Wednesday, Friday from <br />
                        08.30-15.00, no lunch break. <br />
                        <br />
                    </td>
                </tr>

            </table>


        </div>
            <br />
            <asp:Label ID="lblNum" runat="server" Text=""></asp:Label>
            Records Found.
  <br />
            <asp:GridView ID="gridview1" runat="server" CssClass="tdata" CellPadding="3"
                AutoGenerateColumns="False" Width="100%" EnableModelValidation="True"
                EmptyDataText="There is no data." AllowPaging="True">
                <HeaderStyle BackColor="#11720c" ForeColor="White" />
                <RowStyle VerticalAlign="Top" />
                <AlternatingRowStyle BackColor="#e0f3f1" />
                <Columns>
                    <asp:BoundField DataField="status" HeaderText="Status" HtmlEncode="False" />
                    <asp:TemplateField HeaderText="Register Date">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("movement_create_date_ts")) %>'></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"hour") %>'></asp:Label>:
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"min") %>'></asp:Label>
                            <asp:Label ID="lblPK" runat="server" Text='<%# Bind("point_movement_id") %>'
                                Visible="false"></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="move_name" HeaderText="Card ID.">
                        <ItemStyle Width="100px" BackColor="LightGreen" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ผู้แจกการ์ด/Awarded by">
                        <ItemTemplate>
                            <asp:Label ID="lblAward" runat="server" Text='<%# Bind("r_award_by_emp_name") %>'></asp:Label>


                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ชื่อผู้ลงทะเบียน/Employee Name">
                        <ItemTemplate>
                            (<asp:Label ID="lblMethod" runat="server" Text='<%# Bind("emp_code") %>' Visible="true"></asp:Label>)
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                        </ItemTemplate>



                    </asp:TemplateField>
                    <asp:BoundField DataField="dept_name" HeaderText="Cost Center" />
                      <asp:BoundField DataField="inv_item_name_en" HeaderText="บัตรกำนัลที่ต้องการ" />
                    <asp:TemplateField HeaderText="Remark">

                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("movement_remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
            </asp:GridView>
        </div>


    

        <div class="tabber" id="mytabber1" runat="server" visible="false">
            <div class="tabbertab">
                <h2>ลงทะเบียน On-the-Spot Card ด้วยตนเอง </h2>


                <table width="100%" cellspacing="0" cellpadding="3">
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" width="250"><span class="theader"><strong><span class="style3">*</span>หมายเลขการ์ด/On-the-Spot Card ID.</strong></span></td>
                        <td bgcolor="#e0f3f1"><strong>
                            <asp:TextBox ID="txtadd_cardid" runat="server"
                                Width="100px"></asp:TextBox>
                            <asp:Button ID="cmdSearchCard" runat="server" Text="Validate Card" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtadd_cardid"
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </strong></td>
                    </tr>
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" class="style4">&nbsp;</td>
                        <td bgcolor="#e0f3f1" class="style4">
                            <asp:Label ID="lblAwardEmpCode" runat="server" Font-Bold="true" ForeColor="red"
                                Text="-"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" class="style4"><span class="style3">*</span> <strong>วันที่ผู้บริหารแจกการ์ด / Awarded date</strong></td>
                        <td bgcolor="#e0f3f1" class="style4">
                            <asp:TextBox ID="txtdate" runat="server" Width="80px"></asp:TextBox>
         
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" class="style4"><span class="style3">*</span><strong>ชื่อผู้แจกการ์ด/Awarded by</strong></td>
                        <td bgcolor="#e0f3f1" class="style4">
                            <asp:TextBox ID="txtadd_award" runat="server" Width="200px"></asp:TextBox>

                            &nbsp;
             
            
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" class="style4"><span class="style3">*</span><strong>รหัสผู้แจกการ์ด/Mgr. Employee 
              code</strong></td>
                        <td bgcolor="#e0f3f1" class="style4">
                            <asp:TextBox ID="txtadd_award_empcode" runat="server" Width="200px" MaxLength="7"></asp:TextBox>

                            <strong>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    runat="server" ControlToValidate="txtadd_award_empcode"
                                    ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            </strong>

                        </td>
                    </tr>

                </table>


                <table width="100%" cellspacing="0" cellpadding="3">


                    <!--
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" width="250"><strong><span class="style1">*</span>Complementary Notes:</strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:DropDownList ID="txtadd_detail_combo" runat="server"
                                DataTextField="note_th" DataValueField="note_id" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    -->
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"  width="250"><strong><span class="style1">*</span>รายละเอียด</strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:TextBox ID="txtadd_note" runat="server" Rows="2" TextMode="MultiLine"
                                Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><strong><span class="style1">*</span>Outstanding Characteristics:</strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <table style="margin-left: -3px;" cellspacing="1" cellpadding="2"
                                width="100%">
                                <tbody>
                                    <tr>
                                        <td> <asp:CheckBox ID="chk2015_1" runat="server" Text="Compassionate Caring บริการด้วยความเอื้ออาทร" Font-Bold="True" />
                                            &nbsp;</td>
                                    </tr>
                                    <!--
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi1" runat="server" Visible="False" />
                                            Exceed our customer's expectations</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi5" runat="server" Visible="False" />
                                            Embrace cultural diversity with Thai hospitality
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi9" runat="server" Visible="False" />
                                            Value corporate social responsibilities
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi10" runat="server" Visible="False" />
                                            Operate in an environmentally responsible manner
                                        </td>
                                    </tr>
                                    -->
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chk2015_2" runat="server" Text="Adaptability, Learning, and Innovation มีความพร้อมในการปรับเปลี่ยน เรียนรู้ สร้างสรรค์นวัตกรรมใหม่ๆ" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <!--
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi4" runat="server" Visible="False" />
                                            Professional excellence and innovation
                                        </td>
                                    </tr>
                                    -->
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chk2015_3" runat="server" Text="Safety, Quality with Measurable Results ยึดมั่นเรื่องความปลอดภัย คุณภาพมีผลลัพธ์ที่วัดผลได้" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <!--
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi3" runat="server" />
                                            Continually improve the quality and safety
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi6" runat="server" Visible="False" />
                                            Make everything World Class
                                        </td>
                                    </tr>
                                    -->
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chk2015_4" runat="server" Text="Teamwork and Integrity ทำงานเป็นทีมและยึดมั่นหลักคุณธรรม" Font-Bold="True" />
                                        </td>
                                    </tr>
                                    <!--
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi2" runat="server" Visible="False" />
                                            Committed to our staff's welfare and development
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi7" runat="server" />
                                            Be trusted, honest, and ethical in all our dealing
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi8" runat="server" Visible="False" />
                                            Work as a team
                                        </td>
                                    </tr>
                                    -->
                                    <tr>
                                        <td>  <asp:CheckBox ID="chkSpecial" runat="server" Text="Special Project" Font-Bold="True" />
                                            &nbsp;<asp:TextBox ID="txtspecial_remark" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div id="old_biway1" runat="server" visible="false">
                                <table style="margin-left: -3px;" cellspacing="1" cellpadding="2"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk1" runat="server" />
                                                Customer Satisfaction Driven</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk2" runat="server" />
                                                &nbsp;World Class</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk3" runat="server" />
                                                &nbsp;Professional Excellence</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk4" runat="server" />
                                                &nbsp;Thai Hospitality</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk5" runat="server" />
                                                &nbsp;Constant Improvement</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk6" runat="server" />
                                                &nbsp;Commitment to Staff Welfare &amp; Development</td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table cellspacing="0" cellpadding="3" width="90%" align="center">
                                    <tbody>
                                        <tr>
                                            <td
                                                width="25%" valign="top" bgcolor="#11720c" style="color: White"><strong>Clear</strong></td>
                                            <td width="25%" valign="top" bgcolor="#11720c" style="color: White"><strong>Care</strong></td>
                                            <td width="25%" valign="top" bgcolor="#11720c" style="color: White"><strong>Smart</strong></td>
                                            <td width="25%" valign="top" bgcolor="#11720c" style="color: White"><strong>Smart</strong></td>
                                        </tr>
                                        <tr>
                                            <td valign="top"><strong>
                                                <label></label>
                                            </strong>
                                                <label>
                                                    <asp:CheckBox ID="chk_clear" runat="server" />
                                                    &nbsp;</label>ความสามารถในการสื่อสาร<strong><br />
                                                    </strong></td>
                                            <td valign="top">
                                                <asp:CheckBox ID="chk_care" runat="server" />
                                                &nbsp;สัมพันธไมตรีแบบไทย</td>
                                            <td valign="top">
                                                <asp:CheckBox ID="chk_smart1" runat="server" />
                                                &nbsp;ความเป็นเลิศทางวิชาการ</td>
                                            <td valign="top">
                                                <asp:CheckBox ID="chk_smart2" runat="server" />
                                                &nbsp;คุณภาพงานบริการ</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><strong>หมายเหตุ/Remark</strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:TextBox ID="txtadd_remark" runat="server" TextMode="MultiLine" Rows="3" Width="95%"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><strong>รายการสินค้า / Voucher list</strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" colspan="2">
                            <ul id="thumbnails">
                            <asp:ListView ID="PicturesListView" runat="server" ItemPlaceholderID="PicturesListItemPlaceholder">
                                <LayoutTemplate>
                                    <li id="PicturesListItemPlaceholder" runat="server"></li>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li> <img src="srp_show_image.ashx?id=<%# Eval("inv_item_id") %>" alt='<%# Eval("inv_item_name_th") %>' width="100" height="100"  border="0" />
                <br /> <span style="color:blue"><%# Eval("inv_item_name_en") %></span>
              <br /><%# Eval("inv_remark") %>
                 <br />คงเหลือ : [<%# FormatNumber(Eval("on_hand"), 0)%>]
               
             
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                                </ul>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1" colspan="2">
                            <strong>**หมายเหตุ :</strong>
                            <br />
                            - Gift Voucher Big C ใช้บัตร On the spot 2 ใบ<br />
                            - สำหรับพนักงานที่เลือกการโอนเงินเข้าบัญชีเงินเดือน จะตัดรอบการส่งทุกวันที่ 20 ของทุกเดือน</td>
                    </tr>
                    
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><strong></strong></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:DropDownList ID="txtscore" runat="server" Visible="false">
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><b>บัตรกำนัลที่ต้องการอันดับ 1</b></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:DropDownList ID="txtvoucher1" runat="server" DataTextField="inv_item_name_en" DataValueField="inv_item_id">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top" bgcolor="#e0f3f1"><b>บัตรกำนัลที่ต้องการอันดับ 2</b></td>
                        <td valign="top" bgcolor="#e0f3f1">
                            <asp:DropDownList ID="txtvoucher2" runat="server" DataTextField="inv_item_name_en" DataValueField="inv_item_id">
                            </asp:DropDownList>
                        </td>
                    </tr>
                  
                    <tr>
                        <td valign="top" bgcolor="#e0f3f1">&nbsp;</td>
                        <td valign="top" bgcolor="#e0f3f1">
                            &nbsp;</td>
                    </tr>
                  
                </table>
                <br />
                <div align="center">
                    &nbsp;<asp:Button ID="cmdSave" runat="server" Text="Register Card" Width="200px" Font-Bold="true"
                        OnClientClick="return onSubmit()"
                        Enabled="False" />
                    &nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel"
                        CausesValidation="False" />
                </div>
            </div>
        </div>

        <br />

        <div align="right"></div>
    </div>

</asp:Content>


