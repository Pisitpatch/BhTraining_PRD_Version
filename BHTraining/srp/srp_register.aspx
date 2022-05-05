<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_register.aspx.vb" Inherits="srp_srp_register" %>
<%@ Import Namespace="ShareFunction" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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



<style type="text/css">

.style1 {color: #FF0000}
.style3 {color: #FF0000; font-weight: bold; }

    .style4
    {
        height: 31px;
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer" />
</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
<table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;<strong>On the Spot Card Register</strong></div></td>
  </tr>
</table>
<div id="data">
  <table width="100%" cellpadding="2" cellspacing="1" class="tdata3">
    <tr>
      <td colspan="4" class="theader" style="background-color:#11720c"><img src="../images/sidemenu_circle.jpg" width="10" height="10" alt="bullet" />&nbsp;Search</td>
    </tr>
    <tr>
      <td width="150" height="30"><strong>Date</strong></td>
      <td width="250"><asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
         
         &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox></td>
      <td width="120">&nbsp;</td>
      <td >&nbsp;
        </td>
    </tr>
    <tr>
      <td height="30"><strong>Cost Center</strong></td>
      <td colspan="3">
          <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
              DataValueField="dept_id">
          </asp:DropDownList>
          <strong>&nbsp;</strong></td>
    </tr>
   
    <tr>
      <td height="30"><strong>Employee</strong></td>
      <td colspan="3">       
              <asp:TextBox ID="txtfind_emp" runat="server"></asp:TextBox>
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
        </td>
    </tr>
  </table>
 
  <br />
    <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found.
  <br />
  <asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True">
                  <HeaderStyle BackColor="#11720c" ForeColor="White"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
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
                      <asp:BoundField DataField="move_name" HeaderText="Card ID." >
                      <ItemStyle Width="100px" BackColor="LightGreen" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="ผู้แจกการ์ด/Awarded by">
                          <ItemTemplate>
                               <asp:Label ID="lblAward" runat="server" Text='<%# Bind("r_award_by_emp_name") %>'></asp:Label>
                            
                             
                          </ItemTemplate>
                          
                      </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="คะแนนที่ได้รับ/ Point">
                      <ItemStyle BackColor="LightBlue" HorizontalAlign="Right" />
                          <ItemTemplate>
                             <div align="right">
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("point_trans") %>'></asp:Label>
                        </div>
                          </ItemTemplate>
                      
                      </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="ชื่อผู้ลงทะเบียน/Employee Name">
                          <ItemTemplate>
                          (<asp:Label ID="lblMethod" runat="server" Text='<%# Bind("emp_code") %>' Visible="true"></asp:Label>)
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                      <asp:BoundField DataField="dept_name" HeaderText="Cost Center" />
                      <asp:TemplateField HeaderText="Recognition">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("reward_type_name") %>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                     
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("movement_remark") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
  
   
  <div class="tabber" id="mytabber1">
    <div class="tabbertab">
      <h2> On-the-Spot reward information </h2>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
       
      <table width="100%" cellspacing="0" cellpadding="3">
        <tr>
          <td valign="top" bgcolor="#eef1f3" width="250"><span class="theader"><strong><span class="style3">*</span>หมายเลขการ์ด/On-the-Spot Card ID.</strong></span></td>
          <td bgcolor="#eef1f3"><strong><asp:TextBox ID="txtadd_cardid" runat="server" 
                  Width="100px"></asp:TextBox>
                   <asp:Button ID="cmdSearchCard" runat="server" Text="Find Card" />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtadd_cardid"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
            </strong></td>
        </tr>
           <tr>
          <td valign="top" bgcolor="#eef1f3" class="style4"><span class="style3">*</span><strong>ผู้แจกการ์ด/Awarded by</strong></td>
          <td bgcolor="#eef1f3" class="style4">
            <asp:TextBox ID="txtadd_award" runat="server" Width="200px"></asp:TextBox> 
             
           &nbsp;
              <asp:Label ID="lblAwardEmpCode" runat="server" Text="-" ForeColor="red" Font-Bold="true"></asp:Label>
            
          </td>
        </tr>
          <tr>
          <td valign="top" bgcolor="#eef1f3"><strong>รหัสพนักงาน/Employee No.</strong></td>
          <td bgcolor="#eef1f3"><strong><asp:TextBox ID="txtadd_empcode" runat="server" 
                  Width="200px"></asp:TextBox> 
                     <asp:Button ID="cmdFindEmp" runat="server" Text="Find Employee" />
        
           
          </strong></td>
        </tr>
     
        <tr>
          <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
          <td valign="top" bgcolor="#eef1f3">
              <asp:ListBox ID="txtemplist" runat="server" DataTextField="user_fullname1" 
                  DataValueField="emp_code"></asp:ListBox>
            </td>
        </tr>
        </table>
       
       </ContentTemplate>
        </asp:UpdatePanel> 
          <table width="100%" cellspacing="0" cellpadding="3">
        <tr>
          <td valign="top" width="250"><span class="style3">*</span><strong>วันที่ลงทะเบียน/Register Date</strong></td>
          <td>
          <table cellspacing="0" cellpadding="0" >
              <tbody>
                <tr>
                  <td width="180"><asp:TextBox ID="txtdate_report" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                            
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server"  
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_report" PopupButtonID="Image1" >
                              </asp:CalendarExtender>
                              <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /><br />
                   </td>
                  <td style="vertical-align: top;" width="40">เวลา</td>
                  <td>
                      <asp:DropDownList ID="txthour" runat="server">
                      </asp:DropDownList>
                      :
                      <asp:DropDownList ID="txtmin" runat="server">
                      </asp:DropDownList>
                    </td>
                </tr>
              </tbody>
            </table></td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3"><strong>ประเภทพนักงาน/Employee Type</strong></td>
          <td bgcolor="#eef1f3">
              <asp:DropDownList ID="txtpoint" runat="server">
              <asp:ListItem Value="100">Full Time</asp:ListItem>
               <asp:ListItem Value="100">Shift</asp:ListItem>
               <asp:ListItem Value="0">Part Time</asp:ListItem>
              </asp:DropDownList>
            </td>
        </tr>
     
      
     
        <tr>
          <td valign="top" bgcolor="#eef1f3"><strong><span class="style1">*</span>Complementary Notes:</strong></td>
          <td valign="top" bgcolor="#eef1f3"><asp:DropDownList ID="txtadd_detail_combo" runat="server"  
                DataTextField="note_th" DataValueField="note_id" 
               >
           
              
             </asp:DropDownList></td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
          <td valign="top" bgcolor="#eef1f3">
              <asp:TextBox ID="txtadd_note" runat="server" Rows="2" TextMode="MultiLine" 
                  Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3"><strong>Outstanding Characteristics:</strong></td>
          <td valign="top" bgcolor="#eef1f3"><table style="margin-left: -3px;" cellspacing="1" cellpadding="2" 
            width="100%">
              <tbody>
                <tr>
                  <td><asp:CheckBox ID="chk1" runat="server" />
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
            </table></td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
          <td valign="top" bgcolor="#eef1f3"><table cellspacing="0" cellpadding="3" width="90%" align="center">
              <tbody>
                <tr>
                  <td 
                  width="25%" valign="top" bgcolor="#11720c" style="color:White"><strong>Clear</strong></td>
                  <td width="25%" valign="top" bgcolor="#11720c" style="color:White"><strong>Care</strong></td>
                  <td width="25%" valign="top" bgcolor="#11720c" style="color:White"><strong>Smart</strong></td>
                  <td width="25%" valign="top" bgcolor="#11720c" style="color:White"><strong>Smart</strong></td>
                </tr>
                <tr>
                  <td valign="top"><strong>
                    <label> </label>
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
            </table></td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3"><strong>หมายเหตุ/Remark</strong></td>
          <td valign="top" bgcolor="#eef1f3"><asp:TextBox ID="txtadd_remark" runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox></td>
        </tr>
        <tr>
            
          <td colspan="2" valign="top" bgcolor="#eef1f3">&nbsp;</td>
        </tr>
      </table>
      <div align="right">
        &nbsp;<asp:Button ID="Button1" runat="server" Text="Save" Width="100px" OnClientClick="return confirm('Are you want to register card?')" />
        &nbsp;</div>
    </div>
  </div>
    
  <div align="right"></div>
</div>

</asp:Content>

