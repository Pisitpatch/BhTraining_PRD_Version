<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_issue.aspx.vb" Inherits="srp_srp_issue" %>
<%@ Import Namespace="ShareFunction" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

        .style2
        {
            height: 35px;
        }

    </style>

<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> 
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

 <script type="text/javascript">
     $(document).ready(function () {

         $("#ctl00_ContentPlaceHolder1_txtadd_empcode").autocomplete("../ajax_employee.ashx", { matchContains: false,
             autoFill: false,
             mustMatch: false
         });

         $('#ctl00_ContentPlaceHolder1_txtadd_empcode').result(function (event, data, formatted) {
             // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
             var serial = data[1];

             //alert("serial ::" + serial);
             var t = new Array();
             t = serial.split("#");
             $("#ctl00_ContentPlaceHolder1_txtadd_empcode").val(t[0]);
         });

         $("#ctl00_ContentPlaceHolder1_txtadd_empcode").click(function () {
             $(this).select();
         });

     });


  </script>

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

			$('#ctl00_ContentPlaceHolder1_txtissue_date').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}
			);
        //	$('#inlineDatepicker').datepick({onSelect: showDate});
    });

   
</script>

 <!--<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script>-->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {


          $("#ctl00_ContentPlaceHolder1_txtadd_empcode1").autocomplete("../ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtadd_empcode1').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              var a = Array();
              a = data[1].split("#");
              // alert(data[1]);
              $('#ctl00_ContentPlaceHolder1_txtadd_empcode').val(a[0]);
              $("#emp_name").html(data[0] + ", " + a[1] + ", " + a[3]);
              $("#ctl00_ContentPlaceHolder1_txth_empname").val(data[0]);
          });

          $("#ctl00_ContentPlaceHolder1_txtadd_empcode1").click(function () {
              $(this).select();
          });
          //  alert( $("#ctl00_ContentPlaceHolder1_txtprocedure").val() );

          $("#ctl00_ContentPlaceHolder1_txtadd_award").autocomplete("../ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtadd_award').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              var a = Array();
              a = data[1].split("#");
              // alert(data[1]);
              $('#ctl00_ContentPlaceHolder1_txtadd_award').val(a[0]);
              $("#award_name").html(data[0] + ", " + a[1] + ", " + a[3]);
              $("#ctl00_ContentPlaceHolder1_txth_awardname").val(data[0]);

          });

          $("#ctl00_ContentPlaceHolder1_txtadd_award").click(function () {
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer" />
</div>
<table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32" align="absmiddle" />&nbsp;&nbsp;<strong>On the Spot Card Issue</strong></div></td>
  </tr>
</table>
<div id="data">
  <table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
    <tr>
      <td colspan="4" class="theader" style="background-color:#11720c"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
    </tr>
    <tr>
      <td width="80" height="30"><strong>Date</strong></td>
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
          <strong>&nbsp;Card ID
          <asp:TextBox ID="txtfind_id" runat="server"></asp:TextBox>
          </strong></td>
    </tr>
   
    <tr>
      <td height="30"><strong>Project</strong></td>
      <td colspan="3">
          <asp:TextBox ID="txtfind_project" runat="server"></asp:TextBox>
          <strong> 
        &nbsp;Em&nbsp;Employee Code
              <asp:TextBox ID="txtfind_empcode" runat="server"></asp:TextBox>
        Employee Name
              <asp:TextBox ID="txtfind_empname" runat="server"></asp:TextBox> </strong>
        </td>
    </tr>
   
    <tr>
      <td height="30" colspan="4">
          <asp:Button ID="cmdSearch" runat="server" Text="Search" 
              CausesValidation="False" />
          <asp:Button ID="cmdDelete" runat="server" Text="Delete" />
          <asp:Button ID="cmdUpdate" runat="server" Text="Update" />
        </td>
    </tr>
  </table>

   <div id="div_dept" runat="server" visible="false">
     <fieldset>
     <legend><strong>For Manager Approval></legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140" class="style2">
                    Approval name</td>
                <td class="style2">
                    <asp:Label ID="lblApproveName" runat="server" Text="-" ForeColor="#3333CC"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="140">
                    Job Title</td>
                <td>
                    <asp:Label ID="lblYourLevel" runat="server" Text="Label" ForeColor="#3333CC"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="140">
                    Remark</td>
                <td>
                    <asp:TextBox ID="txtremark" runat="server" Rows="3" TextMode="MultiLine" Width="350px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="140">
                    Approval Status</td>
                <td>
            <asp:DropDownList ID="txtdeptstatus" runat="server">
                        <asp:ListItem Value="1">อนุมัติ / Approve</asp:ListItem>
                         <asp:ListItem Value="2">ไม่อนุมัติ / Not Approve</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdDeptStatus" runat="server" Text="Confirm" 
                        OnClientClick="return confirm('Are you sure you want to change status ?')" 
                        CausesValidation="False" />
                </td>
            </tr>
        </table>
        <div><img src="../images/sign_explain.png" alt="tip" /> Tip : 1. 
            เลือกสถานะอนุมัติ/ไม่อนุมัติ&nbsp; 2.เลือกรายการ โดยคลิกที่ Checkbox (<input type="checkbox" disabled />)&nbsp;&nbsp; 
            3.คลิกที่ปุ่ม Confirm </div>
         </strong>
        </fieldset>
    </div>
  <br />

    
  <span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span>
  
  
  
  <asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
        ShowFooter="True">
                   <HeaderStyle BackColor="#11720c" ForeColor="White"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField>
                         <ItemStyle Width="25px" />
                          <ItemTemplate>
                              <asp:CheckBox ID="chkSelect" runat="server" />
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Issued Date">
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("issue_date_ts")) %>'></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("issue_id") %>' 
                                  Visible="false"></asp:Label>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Quarter">
                         
                          <ItemTemplate>
                              
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("quater_no") %>'></asp:Label>/<asp:Label ID="Label3" runat="server" Text='<%# Bind("year_no") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle Width="80px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Project">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("project_name") %>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                          <asp:TemplateField HeaderText="Issued to ">
                          <ItemTemplate>
                             <asp:Label ID="Label5" runat="server" Text='<%# Bind("mgr_emp_no") %>'></asp:Label>
                          </ItemTemplate>
                         
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Issued to ">
                          <ItemTemplate>
                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("mgr_name") %>'></asp:Label>
                         
                          </ItemTemplate>
                         
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Card ID">
                          <ItemTemplate>
                          
                              <asp:Label ID="lblMethod" runat="server" Text='<%# Bind("card_id_start") %>' Visible="true"></asp:Label>
                              - <asp:Label ID="Label2" runat="server" Text='<%# Bind("card_id_end") %>' Visible="true"></asp:Label>
                          </ItemTemplate>
                       
                          <ItemStyle Width="120px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Qty Issued">
                          <ItemTemplate>
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("issue_qty") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Registered">
                      <ItemStyle BackColor="Lime" />
                          <ItemTemplate>
                               <asp:Label ID="lblAward" runat="server" Text='<%# Bind("register_num") %>'></asp:Label>
                            
                             
                          </ItemTemplate>
                          
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Quota">
                          <ItemTemplate>
                             
                        <asp:Label ID="lblScore" runat="server" Text='<%# Bind("quota_qty1") %>'></asp:Label>
                          </ItemTemplate>
                      
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Approval">
                        
                          <ItemTemplate>
                             <div align="center"><asp:Label ID="lblImage1" runat="server" Text=""></asp:Label></div>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                           
                        
                           
                            <ItemTemplate>
                                <asp:Textbox ID="lblRemark" runat="server" Text='<%# Bind("issue_remark") %>' Rows="3" TextMode="MultiLine"></asp:Textbox>
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Issued by">
                          <ItemTemplate>
                              <asp:Label ID="lblIssueBy" runat="server" Text='<%# Bind("issue_by") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
                <div><img src="../images/sign_explain.png" alt="tip" /> Tip :<br />
          <table width="100%">
          <tr><td width="250">&nbsp;<strong>Approval Status</strong></td><td>
          <img src="../images/button_ok.png" alt="ok" /> = Approve&nbsp;
          <img src="../images/button_cancel.png" alt="no" /> = Not Approve&nbsp;
          <img src="../images/history.png" alt="n/a" /> = Wait for approval&nbsp;
          
          </td></tr>
          <tr><td width="250">&nbsp;</td><td>
              &nbsp;</td></tr>
        
          </table>
        
          </div>
  <div class="tabber" id="mytabber1" runat="server">
    <div class="tabbertab">
      <h2> On-the-Spot Card  Issue</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
      <table width="100%" cellspacing="0" cellpadding="3">
        <tr>
          <td valign="top" bgcolor="#eef1f3" width="150" class="style2"><strong>Issue to Employee No.</strong></td>
          <td bgcolor="#eef1f3" class="style2">
              <asp:TextBox ID="txtadd_empcode" runat="server"></asp:TextBox><strong>
           
              <asp:Button ID="cmdFind" runat="server" Text="Find Data" 
                  CausesValidation="False" />
&nbsp;<asp:Label ID="lblname" runat="server"></asp:Label>
              </strong></td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
          <td bgcolor="#eef1f3">
          <div id="history" runat="server" visible="false">
          <table width="100%">
          <tr>
          <td width="120">Quarter Qty Issued</td>
          <td>
            <input style="width: 100px; background-color: lime;" 
                  id="txtqty" name="txtqty" 
                  value="" type="text" runat="server" readonly="readonly" />
            Registered
            <input style="width: 100px; background-color: lime;" 
                  id="txtregister" name="txtregister" 
                  value="" type="text" runat="server" readonly="readonly" />
            Quota
            <input style="width: 100px; background-color:yellow;" 
                  id="txtquota" name="txtquota" 
                  value="" type="text" runat="server" readonly="readonly" /></td>
          </tr>
          <tr>
          <td>Special Qty Issued</td>
          <td><input style="width: 100px; background-color:lime;" 
                  id="txtspecial" name="txtspecial" 
                  value="" type="text" runat="server" readonly="readonly" /></td>
          </tr>
          </table>
          <strong>
          
          
                  
                      
           </strong>
         
            </div>
            </td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3"><span class="theader"><strong><span class="style3">*</span>On-the-Spot Card Qty.</strong></span></td>
          <td bgcolor="#eef1f3">
              <asp:TextBox ID="txtadd_qty" runat="server" AutoPostBack="True"></asp:TextBox>
              <strong>&nbsp;cards from ID
              <asp:TextBox ID="txtadd_start" runat="server" AutoPostBack="True"></asp:TextBox>
&nbsp;to
              <asp:TextBox ID="txtadd_end" runat="server"></asp:TextBox>
&nbsp;</strong></td>
        </tr>
        </table>
        </ContentTemplate>
       </asp:UpdatePanel>
          <table width="100%" cellspacing="0" cellpadding="3">
        <tr>
          <td valign="top" width="150"><strong>Issued Date</strong></td>
          <td>
           <asp:TextBox ID="txtissue_date" runat="server"></asp:TextBox>
          </td>
        </tr>
        <tr>
           
          <td valign="top"><strong>Issue for project</strong></td>
          <td>
              <asp:TextBox ID="txtadd_project" runat="server" Width="300px"></asp:TextBox>
              <strong>
            &nbsp;</strong></td>
        </tr>
        <tr>
          <td valign="top"><strong>Remark</strong></td>
          <td>
              <asp:TextBox ID="txtadd_note" runat="server" Rows="2" TextMode="MultiLine" 
                  Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td valign="top" bgcolor="#eef1f3"><strong> Issued by</strong></td>
          <td bgcolor="#eef1f3"><strong>
              <asp:TextBox ID="txtadd_issueby" runat="server"></asp:TextBox>&nbsp;</strong></td>
        </tr>
        <tr>
          <td colspan="2" valign="top" bgcolor="#eef1f3">&nbsp;</td>
        </tr>
      </table>
      <div align="right">
        &nbsp;<asp:Button ID="cmdSubmit" runat="server" Text="Submit" Width="100px" OnClientClick="return confirm('Are you sure you want to submit?');" />
&nbsp;&nbsp;</div>
    </div>
        
  </div>
  <div align="right"></div>
 
</div>

</asp:Content>

