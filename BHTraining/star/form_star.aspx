<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="true" CodeFile="form_star.aspx.vb" Inherits="star_form_star" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

  <script type="text/javascript">
      $(document).ready(function () {



          $("#ctl00_ContentPlaceHolder1_txtcomplain_remark").autocomplete("../ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtcomplain_remark').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_txtcomplain_remark").click(function () {
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

<script type="text/javascript">
    var global_time;
    $(document).ready(function () {
        checkSession('<%response.write(session("bh_username").toString) %>', '<%response.write(viewtype) %>'); // Check session every 1 sec.
    });

    function openPopup(flag, popupType) {
        if (flag.checked) {
            is_sms = 1;
        } else {
            is_sms = 0;
        }

        window.open('../incident/popup_recepient.aspx?modules=star&popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=620');
    }

    function editDetail(cid) {
        loadPopup(1);
        my_window = window.open('popup_editcomment.aspx?id=<%response.write(id) %>&cid=' + cid, 'popupFile', 'alwaysRaised,scrollbars =yes,status=no,width=850,height=600');
    }

    function openReview(cid) {
        loadPopup(1);
        my_window = window.open('popup_score.aspx?id=<%response.write(id) %>&cid=' + cid, 'popupFile', 'alwaysRaised,scrollbars =yes,status=no,width=850,height=800');
    }

    function openCommiteeReview(cid) {
        loadPopup(1);
        my_window = window.open('popup_commitee_score.aspx?id=<%response.write(id) %>&cid=' + cid, 'popupFile', 'alwaysRaised,scrollbars =yes,status=no,width=800,height=750');
    }

    function checkDate(sender, args) {
        if (sender._selectedDate > new Date()) {
            alert("You cannot select a day !");
            sender._selectedDate = new Date();
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }

    function validateCommittee() {
        if (confirm('Are you sure you want to add new review?')) {
            if ($("#ctl00_ContentPlaceHolder1_txtadd_award_scale").val() == "") {
                alert("กรุณาระบุ Award Scale");
                $("#ctl00_ContentPlaceHolder1_txtadd_award_scale").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtscorename1").val() == 0) {
                alert("กรุณาระบุ ข้อเสนอแนะและนวัตกรรมนี้");
                $("#ctl00_ContentPlaceHolder1_txtscorename1").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtscorename2").val() == 0) {
                alert("กรุณาระบุ รูปแบบข้อเสนอแนะและนวัตกรรมนี้ ");
                $("#ctl00_ContentPlaceHolder1_txtscorename2").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtscorename3").val() == 0) {
                alert("กรุณาระบุ ความสมบูรณ์ของข้อเสนอแนะ");
                $("#ctl00_ContentPlaceHolder1_txtscorename3").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtscorename4").val() == 0) {
                alert("กรุณาระบุ การศึกษาเพื่อสนับสนุนในการนำเสนอนวัตกรรมและข้อเสนอแนะ");
                $("#ctl00_ContentPlaceHolder1_txtscorename4").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtscorename5").val() == 0) {
                alert("กรุณาระบุ ค่าใช้จ่ายในการนำมาใช้");
                $("#ctl00_ContentPlaceHolder1_txtscorename5").focus();
                return false;
            }

        } else {
            return false;
        }
    }

    function checkName(me) {
        
        if (me.checked == true) {
            $("#ctl00_ContentPlaceHolder1_txtptname").val($("#ctl00_ContentPlaceHolder1_txtcomplain_remark").val());
        } else {

        }
    }
</script>
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
        .style2
        {
            height: 22px;
        }
        .style3
        {
            height: 23px;
        }
        .style4
        {
            width: 23px;
        }
        .style5
        {
            width: 80px;
        }
        .auto-style1 {
            height: 23px;
            width: 25%;
        }
        .auto-style2 {
            width: 25%;
        }
        .auto-style3 {
            height: 41px;
        }
        .auto-style4 {
            height: 25px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <table width="100%" cellpadding="1" cellspacing="1">
          <tr>
            <td><div id="header"><img src="../images/star_32.png" alt="Star" width="32" height="32"   />&nbsp;Star of 
                Bumrungrad <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></div></td>
            <td><div style="margin-right: 10px;" align="right">
              <strong><asp:Label ID="Label4" runat="server" Text='Status: '></asp:Label></strong>
              <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="status_name" DataValueField="status_id" Font-Bold="True" 
                    BackColor="Aqua" ForeColor="Blue" Enabled="False">            </asp:DropDownList>
                <asp:Button ID="cmdDraft1" runat="server" Text="Save as Draft" 
                    OnCommand="onSave" CommandArgument="1" CausesValidation="False" />
                      <asp:Button ID="cmdCopy1" runat="server" Text="Copy as Draft" 
                    OnCommand="onSave" CommandArgument="11" CausesValidation="False" 
                    Visible="False"  OnClientClick="return confirm('Are you sure you want to copy as draft?');"  />
                 <asp:Button ID="cmdSubmit" runat="server" Text="Submit" 
                    OnCommand="onSave" CommandArgument="2" 
                    OnClientClick="return confirm('Are you sure you want to submit Star of bumrunrad?');" 
                    Font-Bold="True" />
                 <asp:Button ID="cmdRecv1" runat="server" Text="รับเรื่อง (Receive Suggestion)" 
                    OnCommand="onSave" CommandArgument="4" Visible="False" />
                 <asp:Button ID="cmdHRReview1" runat="server" Text="Save Review" 
                    oncommand="onSave" CommandArgument="99" Visible="False" 
                    CausesValidation="False" />
                <asp:CheckBox ID="chkSendMailAward" runat="server" Text="Send Email" 
                    Visible="False" />
                <asp:Button ID="cmdPreview" runat="server" Text="Print Preview" Visible="false" />
</div></td>
          </tr>
        </table>
  <div id="data">

  <div class="tabber" id="mytabber2" runat="server">
      <div class="tabbertab" id="tabSendMail" runat="server" visible="false">
        <h2>Message Alert</h2>
       <table width="100%" cellspacing="1" cellpadding="2">
  <tr>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="150" valign="top"><input type="button" name="button3" id="button1" value="Address" style="width: 85px;" onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') ,  'to')" /></td>
        <td valign="top"><input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" readonly="readonly" />
            <asp:Button ID="cmdSendMail" runat="server" Text=" Send " 
                CausesValidation="False" /></td>
      </tr>
       <tr>
                    <td valign="top"> 
                        <strong>
                        <input id="btnCC" name="button9" 
               onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') , 'cc')" 
                style="width: 85px;display:none" type="button" value="Cc..."  />
                        CC :</strong></td>
                <td valign="top"><input type="text" name="txtto0" id="txtcc" style="width: 635px;" 
                            runat="server" readonly="readonly" /></td></tr>
      <tr>
        <td valign="top"><strong>Subject :</strong></td>
        <td valign="top">
            <asp:DropDownList ID="txtsubject" runat="server" Width="641px" 
                AutoPostBack="True">
                   <asp:ListItem Value="4">Review and proposal to Star of the month</asp:ListItem>
                   <asp:ListItem Value="7">Review of Recognition Award</asp:ListItem>
                    <asp:ListItem Value="5">Approval for Star of the month</asp:ListItem>
          <asp:ListItem Value="6">Acknowledgement Star of BI</asp:ListItem>
         
           
            
            </asp:DropDownList>
          
            <asp:CheckBox ID="chk_sms" runat="server"  />
          
          Send SMS</td>
      </tr>
      <tr>
        <td valign="top"><strong>Message :</strong></td>
        <td valign="top"><textarea name="txtmessage" id="txtmessage" cols="45" rows="5" style="width: 635px;" runat="server"></textarea>
       <asp:HiddenField ID="txtidselect"
            runat="server" /> <asp:HiddenField ID="txtidCCselect"   runat="server" />
             <asp:HiddenField ID="txtidBCCSelect"   runat="server" />  <asp:HiddenField ID="txtidOther"   runat="server" />          </td>
      </tr>
      </table></td>
  </tr>
</table>

</div>
<div class="tabbertab" id="tabMailLog" runat="server" visible="false">
        <h2>Alert log</h2>
        <asp:GridView ID="GridAlertLog" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" Visible="true" 
            Width="780px" DataKeyNames="log_alert_id" CellSpacing="1">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="alert_date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="alert_method" HeaderText="Method" />
            <asp:BoundField DataField="subject" HeaderText="Subject" />
            <asp:BoundField DataField="send_to" HeaderText="Recepient" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#E098AC" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
        <br /></div>
   <div class="tabbertab" id="tabAttachFile" runat="server" >
  <h2>Attachment</h2>
  <table>
    <tr>
      <td colspan="2" valign="top">
      <table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td colspan="2" valign="top">
            <asp:FileUpload ID="FileUpload0" runat="server" Width="535" />
         <asp:Button ID="cmdUpload"
              runat="server" Text="Upload File" CausesValidation="False" />         
         <asp:Button ID="cmdDeleteFile" runat="server" 
              Text="Delete selected attachments" CausesValidation="False" />         
       </td>
        </tr>
        </table>
          <asp:GridView ID="GridFileAttach" runat="server" AutoGenerateColumns="False" 
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
                      <a href="../share/star/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
          </asp:GridView></td>
    </tr>
  </table>
  <br />
</div>     
 <div class="tabbertab" id="idp_log" runat="server">
              <h2>History log  
                  <asp:Label ID="lblLogNum" runat="server" Text=""></asp:Label></h2>
   <asp:GridView ID="GridviewIDPLog" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="log_status_id" AllowPaging="True" ShowFooter="True" 
                  EnableModelValidation="True">
               <Columns>
                   <asp:BoundField DataField="star_status_name" HeaderText="Action" >
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
                    
                      <asp:TextBox ID="txtLogDate" ReadOnly="true" runat="server" Text='<%# Bind("log_time") %>'  DataFormatString="{0:dd MMM yyyy}"></asp:TextBox>
                        <asp:Label ID="lblDateTS" runat="server" Visible="false" Text='<%# Bind("log_time_ts") %>'></asp:Label>
                </ItemTemplate>
               
            </asp:TemplateField>
                   <asp:TemplateField HeaderText="TAT">
                       <ItemTemplate>
                           <asp:Label ID="lblDuration" runat="server"></asp:Label>
                            <asp:Label ID="lblTS" runat="server" Visible="false" Text='<%# Bind("log_time_ts") %>'></asp:Label>
                       </ItemTemplate>
                      
                   </asp:TemplateField>
        </Columns>
    </asp:GridView>
          
            <br />
            </div>
          </div><br />
<div class="tabber" id="mytabber1">
  <div class="tabbertab" id="tabDetail" runat="server">
        <h2><strong>Star of Bumrungrad information </strong></h2>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:Panel ID="panelDetail" runat="server">
        <div style="font-weight:bold">
        Star No. 
            <asp:Label ID="lblStarNo" runat="server" Text="" ForeColor="#ff3399"></asp:Label>
        </div>
        <fieldset >
            <legend>Nominator ผู้เสนอชื่อ</legend>
             <table width="100%" cellspacing="1" cellpadding="2"  bgcolor="#eef1f3">
                 <tr>
                      <td valign="top" width="250">
                          <asp:Label ID="lblCFBdetail10" runat="server" Text="ประเภทผู้เสนอ"></asp:Label>
                      </td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td width="250">
                                      <asp:DropDownList ID="txtcomplain_status" runat="server" Width="235px" 
                                          AutoPostBack="True">
                                          <asp:ListItem Value="">------ กรุณาเลือก ------</asp:ListItem>
                                          <asp:ListItem Value="1">ผู้ป่วย</asp:ListItem>
                                          <asp:ListItem Value="2">ญาติผู้ป่วยหรือเพื่อน</asp:ListItem>
                                          <asp:ListItem Value="3">ผู้มาเยี่ยมไข้</asp:ListItem>
                                          <asp:ListItem Value="4">พนักงาน</asp:ListItem>
                                          <asp:ListItem Value="5">แพทย์</asp:ListItem>
                                       
                                          <asp:ListItem Value="7">อื่นๆ</asp:ListItem>
                                      </asp:DropDownList>
                                      <br />
                                  </td>
                                  <td>
                                   <div id="div_hn_remark" runat="server" visible="true">
                                      <asp:Label ID="lblCFBdetail11" runat="server" Text="* ผู้เสนอชื่อ โปรดระบุชื่อ-สกุล"></asp:Label>
                                      &nbsp;&nbsp;
                                      <input type="text" id="txtcomplain_remark" style="width: 250px" runat="server" 
                                maxlength="200" />
                                </div>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>

            </table>
            <div id="div_profile" runat="server" visible="false" >
               <table width="100%" cellspacing="1" cellpadding="2" bgcolor="#eef1f3">
               <tr>
               <td width="250"></td>
               <td>
                   <table cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                          <td width="60">
                           <asp:DropDownList ID="txttitle_new" runat="server" Width="50px">
         
                <asp:ListItem Value="นาง">นาง</asp:ListItem>
            <asp:ListItem Value="น.ส.">น.ส.</asp:ListItem>
            <asp:ListItem Value="นาย">นาย</asp:ListItem>
             <asp:ListItem Value="ด.ช.">ด.ช.</asp:ListItem>
             <asp:ListItem Value="ด.ญ.">ด.ญ.</asp:ListItem>                     
             <asp:ListItem Value="">ไม่ระบุ</asp:ListItem>
            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
             <asp:ListItem Value="Master">Master</asp:ListItem>
             <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
           
      
            </asp:DropDownList>
                          </td>
                           <td class="style5">
                               <asp:Label ID="lblCFBdetail4" runat="server" Text="HN"></asp:Label>
                           </td>
                           <td width="120">
                               <asp:TextBox ID="txthn" runat="server" Width="75px"></asp:TextBox>
                               <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" 
                                   AcceptNegative="Left" ErrorTooltipEnabled="True" Mask="999999999" 
                                   MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                   OnInvalidCssClass="MaskedEditError" PromptCharacter="*" 
                                   TargetControlID="txthn" />
                           </td>
                           <td width="70">
                               <asp:Label ID="lblCFBdetail5" runat="server" Text="สัญชาติ"></asp:Label>
                           </td>
                           <td>
                               <asp:DropDownList ID="txtcountry" runat="server" DataTextField="national_name" 
                                   DataValueField="national_name">
                               </asp:DropDownList>
                           </td>
                       </tr>
                       <tr>
                           <td width="60">
                               <asp:Label ID="lblIRdetail23" runat="server" Text="อายุ" />
                           </td>
                           <td class="style5">
                                 <input type="text" name="txtptage" id="txtptage" 
                style="width: 30px" runat="server" />
                                      &nbsp;
                                      <asp:Label ID="lblIRdetail5" runat="server" Text="ปี" />&nbsp;</td>
                           <td width="120">
                               &nbsp;</td>
                           <td width="70">
                               <asp:Label ID="lblIRdetail24" runat="server" Text="เพศ" />
                           </td>
                           <td>
                               <asp:DropDownList ID="txtptsex" runat="server" Width="100px">
                                   <asp:ListItem Value="">ไม่ระบุ</asp:ListItem>
                                   <asp:ListItem Value="Male">ชาย</asp:ListItem>
                                   <asp:ListItem Value="Female">หญิง</asp:ListItem>
                               </asp:DropDownList>
                           </td>
                       </tr>
                   </table>
                   </td>
               </tr>
               </table>
            </div>
               <table width="100%" cellspacing="1" cellpadding="2"  bgcolor="#eef1f3">
                <tr>
                  <td valign="top" width="250"><asp:Label ID="lblCFBdetail12" runat="server" Text="เสนอชื่อมาจาก"></asp:Label></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="250" style="vertical-align:top">
                                    <asp:DropDownList ID="txtfeedback_from" runat="server" Width="235px" 
                                        AutoPostBack="True">
                                        <asp:ListItem Value="">------ กรุณาเลือก ------</asp:ListItem>
                                   
                                        <asp:ListItem Value="1">แบบฟอร์มดาวเด่น</asp:ListItem>
                                        <asp:ListItem Value="2">แบบฟอร์ม CFB</asp:ListItem>
                                        <asp:ListItem Value="3">Email / Web site ของ ร.พ.</asp:ListItem>
                                        <asp:ListItem Value="4">อีเมล์ / แฟกซ์</asp:ListItem>
                                        <asp:ListItem Value="5">โทรศัพท์</asp:ListItem>
                                        <asp:ListItem Value="6">อื่นๆ ระบุ</asp:ListItem>
                                           
                                    </asp:DropDownList>
                                      <br />
                                </td>
                                <td>
                                <div id="div_source_other" runat="server" visible="false">
                                    <asp:Label ID="lblCFBdetail13" runat="server" Text="โปรดระบุ"></asp:Label>&nbsp;&nbsp;
                                    <input ID="txtfeedback_remark" runat="server" style="width: 350px" type="text" 
                                        maxlength="200" />
                                        </div>
                                <div id="div_cfb" runat="server" visible="false">
                               หมายเลข CFB  <asp:textbox ID="txtCFBNo" runat="server" Text="" BackColor="#FFFF99" 
                             ></asp:textbox>
                                </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
             
  <tr>
    <td width="180" valign="top"><asp:Label ID="lblCFBcomment14" runat="server" Text="การติดต่อกลับลูกค้า" /></td>
    <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td  colspan="2" valign="top">
            <asp:DropDownList ID="txtcustomer" runat="server" AutoPostBack="True">
            <asp:ListItem Value="">------ กรุณาเลือก ------</asp:ListItem>
            <asp:ListItem Value="1">พึงพอใจ และไม่ต้องการให้ตอบกลับ</asp:ListItem>
            <asp:ListItem Value="2">พึงพอใจ และต้องการให้ตอบกลับ</asp:ListItem>
            <asp:ListItem Value="3">ไม่พึงพอใจ และไม่ต้องการให้ตอบกลับ (โปรดระบุ รายละเอียด)</asp:ListItem>
            <asp:ListItem Value="4">ไม่พึงพอใจ และต้องการให้ตอบกลับ (โปรดระบุ รายละเอียด)</asp:ListItem>
            </asp:DropDownList>
            <br />
         </td>
      </tr>
      <tr>
        <td colspan="2" valign="top">
        <div id="div_response_remark" runat="server" visible="false">
        (โปรดระบุ)&nbsp;&nbsp;  
                <input type="text" name="txtcus_detail" id="txtcus_detail" style="width: 285px" runat="server" />
            </div>    
                </td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment19" runat="server" Text="กรณีต้องการติดต่อกลับโดย" Visible="false" /></td>
    <td>
    <div id="div_response_method" runat="server" visible="false">
    <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td class="style4">
            <asp:CheckBox ID="chk_tel" runat="server" Text="" />
          </td>
        <td width="150"><asp:Label ID="lblCFBcomment20" runat="server" Text="โทรศัพท์" /></td>
        <td><input type="text" name="txttel" id="txttel" style="width: 335px" runat="server"  /></td>
        </tr>
      <tr>
        <td class="style4">
            <asp:CheckBox ID="chk_email" runat="server" Text="" />
          </td>
        <td><asp:Label ID="lblCFBcomment21" runat="server" Text="อีเมล์" /></td>
        <td><input type="text" name="txtemail" id="txtemail" style="width: 335px" runat="server" /></td>
        </tr>
      <tr>
        <td class="style4">
            <asp:CheckBox ID="chk_othter" runat="server" Text="" />
          </td>
        <td><asp:Label ID="lblCFBcomment22" runat="server" Text="ที่อยู่ (อื่นๆ ระบุ)" /></td>
        <td><input type="text" name="txtother" id="txtother" style="width: 335px" runat="server" /></td>
        </tr>
      </table>
      </div>
      </td>
  </tr>
                  <tr>
                      <td valign="top">
                          <strong><span class="style1">*</span>รายละเอียดที่ท่านประทับใจในบริการ <br />
          (Describe the Occurrence)<br />
          How did the nominee delight you?<br />
          Text Area Autocomplete <br />
          for star co-ordinator</strong></td>
                      <td>
                          <asp:TextBox ID="txtstar_detail" runat="server" Rows="5" TextMode="MultiLine" 
                              Width="80%"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td>
                          &nbsp;</td>
                  </tr>
  </table>
            <div id="section1" runat="server" visible="false">
              <table width="100%" cellspacing="2" cellpadding="3" style="margin: 8px 10px;">
                <tr>
                  <td width="170" valign="top"><asp:Label ID="lblCFBdetail3" runat="server" Text="ชื่อผู้ป่วย"></asp:Label>
                  <input id="Checkbox1" type="checkbox" onclick="checkName(this)"/> <span style="color:#3333CC">ใช้เหมือนผู้เสนอชื่อ</span>
                  </td>
                  <td width="783">
                  <table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="250">
                        <asp:DropDownList ID="txttitle" runat="server" Width="50px">
         
                <asp:ListItem Value="นาง">นาง</asp:ListItem>
            <asp:ListItem Value="น.ส.">น.ส.</asp:ListItem>
            <asp:ListItem Value="นาย">นาย</asp:ListItem>
             <asp:ListItem Value="ด.ช.">ด.ช.</asp:ListItem>
             <asp:ListItem Value="ด.ญ.">ด.ญ.</asp:ListItem>                     
             <asp:ListItem Value="">ไม่ระบุ</asp:ListItem>
            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
             <asp:ListItem Value="Master">Master</asp:ListItem>
             <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
           
      
            </asp:DropDownList>
                        <input type="text" id="txtptname" style="width: 165px" 
                runat="server" /> </td>
                        <td width="80">&nbsp;</td>
                        <td width="120">
                            &nbsp;</td>
                        <td width="70">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                      </tr>
                  </table></td>
                </tr>
                 <tr>
                      <td valign="top">
                          
                           <asp:CheckBox ID="txtsamename" runat="server" Text="ใช้ชื่อเดียวกับผู้เสนอชื่อ" 
                               ForeColor="#3333CC" Visible="false" />
                      </td>
                      <td> &nbsp;</td>
                </tr>
                  <tr>
                      <td valign="top">
                          <asp:Label ID="lblIRdetail4" runat="server" Text="อายุ" />
                      </td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td width="75">
                                    
                                  </td>
                                  <td width="55">
                                      &nbsp;</td>
                                  <td width="80">
                                      &nbsp;</td>
                                  <td width="30">
                                      &nbsp;</td>
                                  <td width="100">
                                      &nbsp;</td>
                                  <td width="75">
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
                  </table>
                  </div>
                    <table width="100%" cellspacing="2" cellpadding="3" style="margin: 8px 10px;">
                 <tr>
    <td valign="top" width="170"><asp:Label ID="lblIRdetail20" Text="สถานที่เกิดเหตุการณ์ประทับใจ" runat="server" /> 
        </td>
    <td>
        <asp:TextBox ID="txtlocation" runat="server" Width="735px" 
            ToolTip="Auto complete"></asp:TextBox> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" />
         <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/LocationService.asmx" ServiceMethod="getLocation" CompletionSetCount="5" 
            TargetControlID="txtlocation"  EnableViewState="false">
        </asp:AutoCompleteExtender><br />
       
        </td>
  </tr>
   <tr>
    <td valign="top">
        <asp:Label ID="lblIRdetail21" runat="server" Text="หมายเลขห้อง" />
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
                      <td valign="top">
                          <asp:Label ID="lblIRdetail14" runat="server" Text="การมารับบริการของผู้ป่วย (ประเภทบริการ)" />
                      </td>
                      <td>
                          <table width="100%">
                              <tr>
                                  <td width="200">
                                      <asp:DropDownList ID="txtservicetype" runat="server">
                                          <asp:ListItem>-- กรุณาเลือก --</asp:ListItem>
                                          <asp:ListItem Value="OPD">ผู้ป่วยนอก/OPD</asp:ListItem>
                                          <asp:ListItem Value="IPD">ผู้ป่วยใน/IPD</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                                  <td width="130">
                                      <asp:Label ID="lblIRdetail9" runat="server" Text="ประเภทลูกค้า" />
                                  </td>
                                  <td>
                                      <asp:DropDownList ID="txtsegment" runat="server">
                                          <asp:ListItem Value="">-- กรุณาเลือก --</asp:ListItem>
                                          <asp:ListItem Value="1">Thai</asp:ListItem>
                                          <asp:ListItem Value="2">Expatriate</asp:ListItem>
                                          <asp:ListItem Value="3">International</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
            
                <tr>
                  <td valign="top">
                      <asp:Label ID="lblCFBdetail8" runat="server" Text="วันที่เกิดเหตุการณ์ (วันที่เกิดเหตุ)"></asp:Label>
                  </td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                          <td width="150">
                              <asp:TextBox ID="txtdate_report" runat="server" BackColor="Lime" Width="100px" ></asp:TextBox>
                             
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server"  
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_report" PopupButtonID="Image9" >
                              </asp:CalendarExtender>
                              <asp:Image ID="Image9" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
                              <br />
                        </td>
                          <td width="40" style="vertical-align:top">
                              <asp:Label ID="lblCFBdetail9" runat="server" Text="เวลา"></asp:Label></td>
                          <td>
                              <asp:DropDownList ID="txthour" runat="server">
                              </asp:DropDownList>
                              :
                              <asp:DropDownList ID="txtmin" runat="server">
                              </asp:DropDownList>
                          </td>
                      </tr>
                    </table></td>
                </tr>
                <tr>
                  <td valign="top"><asp:Label ID="lblIRdetail22" runat="server" 
                          Text="วันที่เขียนเสนอชื่อ"></asp:Label></td>
                  <td>
                      <asp:TextBox ID="txtdate_complaint" runat="server" BackColor="Lime" 
                          Width="100px" ></asp:TextBox>
                   
                      <asp:CalendarExtender ID="txtdate_complaint_CalendarExtender" runat="server" 
                          Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Image4" 
                          TargetControlID="txtdate_complaint">
                      </asp:CalendarExtender>
                      <asp:Image ID="Image4" runat="server" CssClass="mycursor" 
                          ImageUrl="~/images/calendar.gif" />
                    </td>
                </tr>
              
             
              </table>
           
 </fieldset>
  <fieldset>
  <legend>Nominee ผู้ถูกเสนอชื่อ</legend>
     
      <table width="100%" cellspacing="2" cellpadding="3" >
                <tr>
                  <td width="250" valign="top" bgcolor="#eef1f3"><b>Nominee&#39;s Type</b></td>
                  <td>
                      <asp:RadioButtonList ID="txtnominee_type" runat="server" 
                          RepeatDirection="Horizontal">
                          <asp:ListItem Value="1">Employee</asp:ListItem>
                          <asp:ListItem Value="2">Subcontract</asp:ListItem>
                      </asp:RadioButtonList>
                    </td>
                  </tr>
                    <tr>
                      <td valign="top" bgcolor="#eef1f3"><b>ประเภทบุคคล / Nominee&#39;s Name &nbsp;</b>
                        <br />
                                      (กรุณาเลือกแค่ 1 รายชื่อ)
                      </td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:TextBox ID="txtfind_name" runat="server" Width="80px"></asp:TextBox>
                                      <asp:Button ID="cmdFindName" runat="server" Text="Search" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" >
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtperson_all" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" Width="200px" Rows="10">
                                      </asp:ListBox>
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &gt;&gt;" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &lt;&lt;" />
                                  </td>
                                  <td valign="top" >
                                      <asp:ListBox ID="txtperson_select" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" Width="200px" Rows="5"></asp:ListBox>
                                    </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                  <tr>
                      <td valign="top" bgcolor="#eef1f3"><b>ประเภททีม / Nominee&#39;s Department &nbsp;</b>
                        <br />
                                      (กรุณาเลือกแค่ 1 รายชื่อ)</td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtdept_all" runat="server" 
                                          DataTextField="dept_name_en" DataValueField="costcenter_id" Width="200px" 
                                          Rows="10"></asp:ListBox>
                                  </td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelateDept" runat="server" Text=" &gt;&gt;" 
                                          CausesValidation="False" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelateDept" runat="server" Text=" &lt;&lt;" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="492">
                                      <asp:ListBox ID="txtdept_select" runat="server" DataTextField="costcenter_name" 
                                          DataValueField="costcenter_id" Width="200px" Rows="5"></asp:ListBox>
                                            <br />
                                  </td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" width="492">
                                      &nbsp;</td>
                              </tr>
                          </table>
                      </td>
                </tr>
                
                  <tr>
                      <td bgcolor="#eef1f3" valign="top">
                          ชื่อแพทย์ผู้ถูกเสนอ&nbsp;</td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:TextBox ID="txtfind_doctor" runat="server" Width="80px"></asp:TextBox>
                                      <asp:Button ID="cmdFindDoctor" runat="server" Text="Search" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" width="492">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtdoctor_all" runat="server" DataTextField="doctor_name_en" 
                                          DataValueField="emp_no" SelectionMode="Multiple" Width="200px" Rows="10">
                                      </asp:ListBox>
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelateDoctor" runat="server" CausesValidation="False" 
                                          Text=" &gt;&gt;" style="height: 26px" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelateDoctor" runat="server" CausesValidation="False" 
                                          Text=" &lt;&lt;" />
                                  </td>
                                  <td valign="top" width="492">
                                      <asp:ListBox ID="txtdoctor_select" runat="server" 
                                          DataTextField="doctor_name_en" DataValueField="emp_no" Width="200px" 
                                          Rows="10">
                                      </asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        (กรณีค้นหาไม่เจอ) ระบุชื่อ-สกุล พนักงาน/แพทย์&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtcustom_name" runat="server" Rows="3" TextMode="MultiLine" 
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                  </table>
                  
  </fieldset>

             
              <br />
              </asp:Panel>
              </ContentTemplate>
    </asp:UpdatePanel>
  </div>
    <div class="tabbertab" id="tab_manager" runat="server" visible="false">
              <h2>Chair of Star Committee  <asp:Label ID="lblManagerNum" runat="server" Text=""></asp:Label></h2>
              <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td width="1022" colspan="5">
                  <asp:GridView ID="GridManagerComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px"
 BorderStyle="None" 
                          EmptyDataText="There is no Sup/Mgr review">
                     
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                              <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td width="60" rowspan="3" valign="top" bgcolor="#FFFFFF"><img src="../images/thumb_user.jpg" width="50" height="50"></td>
                            <td bgcolor="#EDF4F9"><table width="100%" cellspacing="1" cellpadding="2">
                              <tr>
                                <td valign="top"><strong>
                                  <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                                </strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label>
                                   ,
                                  <asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label>
                                   ,
                                <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                                      <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
 </td>
                              <td><div align="right">
                                  <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                                
                                  <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/bt_delete.gif" OnCommand="onDeleteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' OnClientClick="return confirm('Are you you want to delete ?');" />                                
                                                                 
</div></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label>  
                                  &nbsp; </td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                          
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label>
                           
                            </td>
                          </tr>
                      </table>
                                        
                                      
                      </td>
                    </tr>
                    
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#D4D4D4"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                 <asp:Panel ID="panelAddComment" runat="server" Visible="false">
                    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"><strong>Review By</strong></td>
                        <td bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">


                            <tr>
                              <td width="156" valign="top"><strong><asp:Label ID="lblJobType" runat="server" Text=""></asp:Label></strong></td>
                              <td width="180" valign="top"><input name="txtname" type="text" id="txtname" style="width: 180px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="159" valign="top"><input name="txtjobtitle" type="text" id="txtjobtitle" style="width: 150px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="189" valign="top"><input name="txtdeptname" type="text" id="txtdeptname" style="width: 180px" value="" runat="server"  disabled="disabled" /></td>
                              <td width="120"><input name="txtdatetime" type="text" disabled="disabled" id="txtdatetime" style="width: 120px;" value="" readonly="readonly" runat="server" /></td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Approval</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td width="80">
                                  <asp:DropDownList ID="txtacknowedge_status" runat="server">
                                  
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              <td width="25">&nbsp;</td>
                              <td width="158"><strong>Effective recognition</strong></td>
                              <td>
                                 <asp:DropDownList ID="txtcomment" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Star of the month</asp:ListItem>
                                    <asp:ListItem Value="2">Star of the year</asp:ListItem>
                                    <asp:ListItem Value="3">Gold star</asp:ListItem>
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
                            <asp:Button ID="cmdAddComment" runat="server" Text="Save"  OnClientClick="return addComment()" />
                        </td>
                      </tr>
                    </table>
                    </asp:Panel>
                    
                    </td>
                </tr>
              </table>
              <br />
            </div>
  <div class="tabbertab" id="tab_commitee" runat="server" visible="false">
  <h2>Star Committee  <asp:Label ID="lblNumCommittee" runat="server" Text=""></asp:Label></h2>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
      <ContentTemplate>
        
    <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px"
 BorderStyle="None" 
                          EmptyDataText="There is no Committee review">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                              <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td width="60" rowspan="3" valign="top" bgcolor="#FFFFFF"><img src="../images/thumb_user.jpg" width="50" height="50"></td>
                            <td bgcolor="#EDF4F9"><table width="100%" cellspacing="1" cellpadding="2">
                              <tr>
                                <td valign="top"><strong>
                                  <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                                </strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label>
                                   ,
                                  <asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label>
                                   ,
                                <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                                      <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
 </td>
                              <td><div align="right">
                                  <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                                  <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Edit Comment" Visible="false" />                                
                                  <asp:Button ID="cmdDelComment" runat="server" Text="Delete" OnCommand="onDeleteCommitteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' OnClientClick="return confirm('Are you you want to delete ?');" />                                
                                  <asp:ImageButton ID="cmdMgrReview" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Review Score" Visible="false"  />                                
</div></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                                  Types of recognition :  <asp:Label ID="Label55" runat="server" Text='<%# Bind("recognition_type_name") %>'></asp:Label>
                                  &nbsp;
                                  <br />
                                  <asp:Label runat="server" ID="Label5" />
                                  <asp:LinkButton runat="server" ID="myExpand1">more detail.. </asp:LinkButton>                              </td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                          
                           
                           
                            </td>
                          </tr>
                      </table>
                                        <asp:CollapsiblePanelExtender ID="cpe11" runat="Server"
    TargetControlID="Panel1"
    CollapsedSize="0"
   
    Collapsed="True"
  TextLabelID="Label5"
    AutoExpand="False"
    ScrollContents="True"

    CollapsedText=""
    ExpandedText="" 
  
    ExpandDirection="Vertical" ExpandControlID="myExpand1" CollapseControlID="myExpand1" 
                          SuppressPostBack="True" />
                       <asp:Panel ID="Panel1" runat="server">
                       <table width="100%">
                       <tr>
                       <td width="250" style="font-weight:bold">เหตุการณ์/คำชม :</td>
                       <td><asp:Label ID="Label6" runat="server" Text='<%# bind("event_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">รายละเอียดเหตุการณ์ :</td>
                       <td><asp:Label ID="Label7" runat="server" Text='<%# bind("detail_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold ; vertical-align:top">สิ่งที่ประทับใจเกี่ยวข้องกับ :</td>
                       <td><asp:Label ID="lblCare" runat="server" Text=''></asp:Label>
                       <asp:Label ID="lblchk1" runat="server" Text='<%# bind("chk_clear") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblchk2" runat="server" Text='<%# bind("chk_care") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblchk3" runat="server" Text='<%# bind("chk_smart") %>' Visible="false"></asp:Label>
                             <asp:Label ID="lblchk4" runat="server" Text='<%# bind("chk_quality") %>' Visible="false"></asp:Label>

                      <asp:Label ID="lblbi1" runat="server" Text='<%# Bind("chk_newbi1")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi2" runat="server" Text='<%# Bind("chk_newbi2")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi3" runat="server" Text='<%# Bind("chk_newbi3")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi4" runat="server" Text='<%# Bind("chk_newbi4")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi5" runat="server" Text='<%# Bind("chk_newbi5")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi6" runat="server" Text='<%# Bind("chk_newbi6")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi7" runat="server" Text='<%# Bind("chk_newbi7")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi8" runat="server" Text='<%# Bind("chk_newbi8")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi9" runat="server" Text='<%# Bind("chk_newbi9")%>' Visible="false"></asp:Label>
                             <asp:Label ID="lblbi10" runat="server" Text='<%# bind("chk_newbi10") %>' Visible="false"></asp:Label>
                       
                           <asp:Label ID="lbl2015_1" runat="server" Text='<%# Bind("chk_2015_1")%>' Visible="false"></asp:Label>
                           <asp:Label ID="lbl2015_2" runat="server" Text='<%# Bind("chk_2015_2")%>' Visible="false"></asp:Label>
                           <asp:Label ID="lbl2015_3" runat="server" Text='<%# Bind("chk_2015_3")%>' Visible="false"></asp:Label>
                           <asp:Label ID="lbl2015_4" runat="server" Text='<%# Bind("chk_2015_4")%>' Visible="false"></asp:Label>
                       </td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Types of recognition</td>
                       <td><asp:Label ID="Label9" runat="server" Text='<%# bind("recognition_type_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Endorse :</td>
                       <td><asp:Label ID="Label2" runat="server" Text='<%# bind("recognition_name") %>'></asp:Label>
                       <asp:Label ID="lblEndorseID" runat="server" Text='<%# bind("recognition_id") %>' Visible="false"></asp:Label>
                       </td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Recognition Award</td>
                       <td><asp:Label ID="lblAward" runat="server" Text='<%# bind("recognition_award") %>'></asp:Label></td>
                       </tr>
                         <tr>
                       <td style="font-weight:bold">ข้อพิจารณาของคณะกรรมการ</td>
                       <td><asp:Label ID="lblCommitteeConsider" runat="server" Text='<%# bind("committee_name") %>'></asp:Label></td>
                       </tr>
                       <tr >
                       <td style="font-weight:bold">Comment</td>
                       <td><asp:Label ID="lblCommitteeComment1" runat="server" Text='<%# bind("committee_comment") %>'></asp:Label></td>
                       </tr>
                       <tr>
                       <td></td>
                       <td></td>
                       </tr>
                       <tr>
                       <td></td>
                       <td></td>
                       </tr>
                       </table>
                                   
                          
  
   
                            </asp:Panel>

                      
                      </td>
                    </tr>
                    
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#D4D4D4"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                      <asp:panel runat="server" ID="panel_add_committee" Visible="false">
  <table width="100%" cellpadding="2" cellspacing="1">
      <tr>
        <td width="1037" valign="top" bgcolor="#eef1f3"><table cellspacing="0" cellpadding="3" width="80%" align="center">
            <tbody>
              <tr>
                <td bgcolor="#6699cc" valign="top" 
                  width="305"><strong>Conclusion</strong></td>
                <td bgcolor="#6699cc" valign="top" width="723"><strong><u>Recognition Award</u></strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Endorse (<asp:Label ID="lblSumEndorse1" runat="server" Text=""></asp:Label>)
                    <br />
                </strong></td>
                <td valign="top"><asp:Label ID="lblEndorseDetail" runat="server" Text=""></asp:Label></td>
              </tr>
           
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top"><strong>Do Not Endorse (<asp:Label ID="lblSumNotEndorse1" runat="server" Text=""></asp:Label>)</strong></td>
                <td valign="top">&nbsp;</td>
              </tr>
                <tr>
                    <td valign="top"><strong>Comment</strong></td>
                    <td valign="top">
                        <asp:Label ID="lblComitteeComment" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table></td>
      </tr>
    </table>
    <br />
    <fieldset>
    <legend>รายละเอียดที่ท่านประทับใจในบริการ (Describe the Occurrence)</legend>
      <asp:Label ID="lblCommitteeDetail" runat="server" Text="" BackColor="#ebebeb" ForeColor="#0033cc" Font-Size="18px"></asp:Label>                    
      </fieldset>
     <fieldset>
    <legend>ผู้ถูกเสนอชื่อ (Nominee)</legend>
      <asp:Label ID="lblNominee" runat="server" Text=""  ></asp:Label>                    
      </fieldset>
    <br />
    <table width="100%" cellpadding="2" cellspacing="1">
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>เหตุการณ์/คำชม</strong></td>
        <td valign="top"><asp:DropDownList ID="txtadd_event_admire" runat="server" 
                DataTextField="admire_topic" DataValueField="admire_id" 
                AutoPostBack="True">
            </asp:DropDownList>
          </td>
      </tr>
      <!--
      <tr id="test" runat="server" visible="false">
        <td valign="top" bgcolor="#eef1f3"><strong>ข้อความที่ระบุในเหตุการณ์ที่ท่านประทับใจ</strong></td>
        <td valign="top" ><strong>
             <asp:DropDownList ID="txtadd_sentence_admire" runat="server" 
                 DataTextField="admire_topic" DataValueField="admire_id">
             </asp:DropDownList>
         </strong></td>
      </tr>
      -->
      <tr >
        <td valign="top" bgcolor="#eef1f3"><strong>การระบุรายละเอียดเหตุการณ์</strong></td>
        <td valign="top">  <asp:DropDownList ID="txtadd_detail_combo" runat="server"  DataTextField="note_th" DataValueField="note_id" 
                AutoPostBack="True">
           
              
             </asp:DropDownList></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>สิ่งที่ประทับใจเกี่ยวข้องกับ<br />
          Appreciation Category</strong></td>
        <td>
            <div id="div_commitee" runat="server" visible="false">
            <table cellspacing="0" cellpadding="3" width="90%" align="center">
              <tbody>
                <tr>
                  <td 
                  width="25%" valign="top" bgcolor="#6699cc"><strong>Clear</strong></td>
                  <td width="25%" valign="top" bgcolor="#6699cc"><strong>Care</strong></td>
                  <td width="25%" valign="top" bgcolor="#6699cc"><strong>Smart</strong></td>
                  <td width="25%" valign="top" bgcolor="#6699cc"><strong>Smart</strong></td>
                </tr>
                <tr>
                  <td valign="top">
                      <asp:CheckBox ID="chk_add_clear" runat="server" Text="ความสามารถในการสื่อสาร" 
                          Visible="true" />
                    </td>
                  <td valign="top">
                    <asp:CheckBox ID="chk_add_care" runat="server" Text="สัมพันธไมตรีแบบไทย" 
                          Visible="true" />
                   
                  </td>
                  <td valign="top">
                    <asp:CheckBox ID="chk_add_smart" runat="server" Text="ความเป็นเลิศทางวิชาการ" 
                          Visible="true" />
                 </td>
                  <td valign="top">
                   <asp:CheckBox ID="chk_add_quality" runat="server" Text="คุณภาพงานบริการ" 
                          Visible="true" />
                   
                 </td>
                </tr>
              </tbody>
          </table>
            </div>

               <table style="margin-left: -3px;" cellspacing="1" cellpadding="2" 
            width="100%">
              <tbody>
                <tr>
                  <td><asp:CheckBox ID="chk2015_com1" runat="server" Text="Compassionate Caring บริการด้วยความเอื้ออาทร" Font-Bold="True" />&nbsp;</td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com1" runat="server" />
                          Exceed our customer&#39;s expectations / มุ่งมั่นที่จะให้บริการที่เกินความคาดหวังของลูกค้า </td>
                  </tr>
                <tr>
                  <td class="auto-style3"> 
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com5" runat="server" />
                          Embrace cultural diversity with Thai hospitality / ให้การบริการแบบไทย แก่ผู้ป่วยทุกชาติ ภาษา และวัฒนธรรมอย่างเท่าเทียมกัน
                    </td>
                </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com9" runat="server" /> Value corporate social responsibilities / เป็นองค์กรที่ยึดมั่นในความรับผิดชอบต่อสังคม 
                    </td>
                </tr>
                <tr>
                  <td class="auto-style4">
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com10" runat="server" /> Operate in an environmentally responsible manner / ให้ความสำคัญต่อการอนุรักษ์สิ่งแวดล้อมและทรัพยากรธรรมชาติ 
                    </td>
                </tr>
                  -->
                <tr>
                  <td> <asp:CheckBox ID="chk2015_com2" runat="server" Text="Adaptability, Learning, and Innovation มีความพร้อมในการปรับเปลี่ยน เรียนรู้ สร้างสรรค์นวัตกรรมใหม่ๆ" Font-Bold="True" />&nbsp;</td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com4" runat="server" /> Professional excellence and innovation / มุ่งมั่นที่ก้าวสู่ความเป็นเลิศทางวิชาชีพและนวัตกรรมทางการแพทย์อย่างต่อเนื่อง
                          </td>
                  </tr>
                  -->
                <tr>
                  <td>   <asp:CheckBox ID="chk2015_com3" runat="server" Text="Safety, Quality with Measurable Results ยึดมั่นเรื่องความปลอดภัย คุณภาพมีผลลัพธ์ที่วัดผลได้" Font-Bold="True" />&nbsp;</td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com3" runat="server" /> Continually improve the quality and safety / ปรับปรุงคุณภาพและความปลอดภัยของทุกสิ่งที่เราปฏิบัติอย่างต่อเนื่อง 
                          </td>
                  </tr>
                <tr>
                  <td> 
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com6" runat="server" />
                          Make everything World Class / มุ่งมั่นที่จะพัฒนาการบริการทุกขั้นตอนให้ได้มาตรฐานระดับโลก
                    </td>
                </tr>
                  -->
                <tr>
                  <td>  <asp:CheckBox ID="chk2015_com4" runat="server" Text="Teamwork and Integrity ทำงานเป็นทีมและยึดมั่นหลักคุณธรรม" Font-Bold="True" />&nbsp;</td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com2" runat="server" /> Committed to our staff's welfare and development / มุ่งมั่นในการพัฒนาด้านการศึกษา การเรียนการสอน การเพิ่มศักยภาพและระบบสวัสดิการที่ดีต่อบุคลากรโรงพยาบาล
                         </td>
                  </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com7" runat="server" /> Be trusted, honest, and ethical in all our dealing / ปฏิบัติต่อผู้ป่วยด้วยความซื่อสัตย์ มีความไว้วางใจซึ่งกันและกัน และคงไว้ซึ่งจริยธรรม 
                    </td>
                </tr>
                <tr>
                  <td> 
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_com8" runat="server" />
                          Work as a team / ทำงานร่วมกันเป็นทีมและแลกเปลี่ยนสิ่งที่เรียนรู้แก่กันและกัน 
                    </td>
                </tr>
                  -->
                  </tbody>
                  </table>

        </td>
      </tr>
      <tr>
        <td width="199" valign="top" bgcolor="#eef1f3"><strong>Recognition</strong></td>
        <td width="838" valign="top"><strong>
             <asp:DropDownList ID="txtstar_conclusion" runat="server" Width="185px">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Endorse</asp:ListItem>
               <asp:ListItem Value="2">Do Not Endorse</asp:ListItem>
                
             </asp:DropDownList>
       
        </strong></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3" class="style1"><strong>Types of recognition</strong></td>
        <td valign="top" class="style1">
        <asp:DropDownList ID="txtstar_recog" runat="server" Width="185px" DataTextField="recognition_name" DataValueField="recognition_id">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
          
               <asp:ListItem Value="1">Half Star Record</asp:ListItem>
                <asp:ListItem Value="2">Star Record</asp:ListItem>
               <asp:ListItem Value="3">Star Pin</asp:ListItem>
            <asp:ListItem Value="4">Gold Pin</asp:ListItem>
             </asp:DropDownList>
        
        </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>Recognition Award</strong></td>
        <td valign="top"> <asp:DropDownList ID="txtadd_award" runat="server">
           <asp:ListItem Value="0">0</asp:ListItem>
             <asp:ListItem Value="100">100</asp:ListItem>
             <asp:ListItem Value="200">200</asp:ListItem>
             <asp:ListItem Value="300">300</asp:ListItem>
             <asp:ListItem Value="1000">1000</asp:ListItem>
             <asp:ListItem Value="5000">5000</asp:ListItem>
             <asp:ListItem Value="10000">10000</asp:ListItem>
             </asp:DropDownList></td>
      </tr>
         <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>ข้อพิจารณาของคณะกรรมการ</strong></td>
         <td valign="top">
          <asp:DropDownList ID="txtconsider" runat="server" Width="185px">
             <asp:ListItem Value="">-</asp:ListItem>
              <asp:ListItem Value="1">Suggestion / CFB</asp:ListItem>
               <asp:ListItem Value="2">Could not find Nominee name</asp:ListItem>
               <asp:ListItem Value="3">No submitted person</asp:ListItem>
               <asp:ListItem Value="4">Thank you card</asp:ListItem> 
                <asp:ListItem Value="4">Duplicate</asp:ListItem> 
                  <asp:ListItem Value="5">Other</asp:ListItem> 
             </asp:DropDownList>
      </td>
       </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>Comment</strong></td>
        <td bgcolor="#eef1f3"><textarea name="txtadd_comment" id="txtadd_comment" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
      </tr>
      <tr>
        <td colspan="2" valign="top"><div >
            <asp:Button ID="cmdSaveCommittee" runat="server" Text="Add Review" 
                CausesValidation="False" />
            </div></td>
        </tr>
    </table>
    </asp:panel>
      </ContentTemplate>
      </asp:UpdatePanel>
  &nbsp;</div>

   <div class="tabbertab" id="tab_coordinator" runat="server" visible="false">
  <h2>Star Coordinator</h2>
  
   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
   <ContentTemplate>
   <asp:Panel ID="panel_hr" runat="server">
  <fieldset>
     <legend> <strong>Conclusion Summary</strong> </legend>
     <table width="100%" cellpadding="2" cellspacing="1">
       <tr>
         <td bgcolor="#eef1f3" valign="top" colspan="2">
         <table cellspacing="0" cellpadding="3" width="80%" align="center">
           <tbody>
             <tr>
               <td bgcolor="#6699cc" valign="top" 
                  width="305"><strong>Conclusion</strong></td>
               <td bgcolor="#6699cc" valign="top" width="723"><strong>Types of recognition</strong></td>
             </tr>
             <tr>
               <td valign="top"><strong>Endorse (<asp:Label ID="lblSumEndorse2" runat="server" Text="Label"></asp:Label>)<br />
               </strong></td>
               <td valign="top"><asp:Label ID="lblEndorseDetail2" runat="server" Text=""></asp:Label></td>
             </tr>
            
             <tr>
               <td valign="top">&nbsp;</td>
               <td valign="top">&nbsp;</td>
             </tr>
             <tr>
               <td valign="top"><strong>Do Not Endorse (<asp:Label ID="lblSumNotEndorse2" runat="server" Text="Label"></asp:Label>)</strong></td>
               <td valign="top">&nbsp;</td>
             </tr>
               <tr>
                   <td valign="top"><strong>เหตุการณ์/คำชม</strong></td>
                   <td valign="top">
                       <asp:GridView ID="gridAdmireHR" runat="server" CellPadding="4" CssClass="tdata3" EmptyDataText="There is no data." EnableModelValidation="True" GridLines="None" Width="98%">
                           <Columns>
                               <asp:BoundField />
                           </Columns>
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                           <HeaderStyle BackColor="#abbbb4" CssClass="theader" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                           <RowStyle BackColor="#eef1f3" />
                           <AlternatingRowStyle BackColor="White" />
                       </asp:GridView>
                   </td>
               </tr>
               <tr>
                   <td valign="top"><strong>รายละเอียดเหตุการณ์</strong></td>
                   <td valign="top">
                       <asp:GridView ID="gridDetailHR" runat="server" CellPadding="4" CssClass="tdata3" EmptyDataText="There is no data." EnableModelValidation="True" GridLines="None" Width="98%">
                           <Columns>
                               <asp:BoundField />
                           </Columns>
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                           <HeaderStyle BackColor="#abbbb4" CssClass="theader" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                           <RowStyle BackColor="#eef1f3" />
                           <AlternatingRowStyle BackColor="White" />
                       </asp:GridView>
                   </td>
               </tr>
               <tr>
                   <td valign="top"><strong>สิ่งที่ประทับใจเกี่ยวข้องกับ<br /> Appreciation Category</strong></td>
                   <td valign="top">
                       <asp:GridView ID="gridCoastHR" runat="server" CellPadding="4" CssClass="tdata3" EmptyDataText="There is no data." EnableModelValidation="True" GridLines="None" Width="98%">
                           <Columns>
                               <asp:BoundField />
                           </Columns>
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                           <HeaderStyle BackColor="#abbbb4" CssClass="theader" Font-Bold="True" ForeColor="White" />
                           <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                           <RowStyle BackColor="#eef1f3" />
                           <AlternatingRowStyle BackColor="White" />
                       </asp:GridView>
                   </td>
               </tr>
               <tr>
                   <td valign="top">&nbsp;</td>
                   <td valign="top">&nbsp;</td>
               </tr>
           </tbody>
         </table>
      
         
         </td>
       </tr>
         <tr>
             <td bgcolor="#eef1f3" colspan="2" valign="top">
                    <br />
      <asp:Label ID="lblHRDetail" runat="server" Text="Label" BackColor="#ebebeb" ForeColor="#0033cc" Font-Size="18px"></asp:Label>                    

    <br /></td>
         </tr>
       <tr>
         <td width="199" valign="top" bgcolor="#eef1f3"><strong>Types of Recognition Teams</strong></td>
         <td width="838">      <asp:DropDownList ID="txthr_recog_type" runat="server" Width="185px">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Individual</asp:ListItem>
               <asp:ListItem Value="2">Team</asp:ListItem>
                <asp:ListItem Value="3">Organization</asp:ListItem>
             </asp:DropDownList><br /></td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>เหตุการณ์/คำชม</strong></td>
         <td valign="top"><asp:DropDownList ID="txtevent_admire" 
                 runat="server" DataTextField="admire_topic" DataValueField="admire_id" 
                 AutoPostBack="True">
             </asp:DropDownList>
         </td>
       </tr>
       <!--
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>ข้อความที่ระบุในเหตุการณ์ที่ท่านประทับใจ</strong></td>
         <td valign="top"><strong>
             <asp:DropDownList ID="txtsentence_admire" runat="server" 
                 DataTextField="admire_topic" DataValueField="admire_id">
             </asp:DropDownList>
         &nbsp;</strong></td>
       </tr>
       -->
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>การระบุรายละเอียดเหตุการณ์</strong></td>
         <td valign="top">
         <asp:DropDownList ID="txthr_detail_combo" runat="server"  DataTextField="note_th" DataValueField="note_id" 
                AutoPostBack="True">
           
              
             </asp:DropDownList>
            
         
         </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>สิ่งที่ประทับใจเกี่ยวข้องกับ<br />
           Appreciation Category</strong></td>
         <td>
             <div id="div_oldbi_hr" runat="server" visible="false">
             <table cellspacing="0" cellpadding="3" width="90%" align="center">
               <tbody>
                 <tr>
                   <td valign="top" bgcolor="#6699cc" class="auto-style1"><strong>Clear</strong></td>
                   <td width="25%" valign="top" bgcolor="#6699cc" class="style3"><strong>Care</strong></td>
                   <td width="25%" valign="top" bgcolor="#6699cc" class="style3"><strong>Smart</strong></td>
                   <td width="25%" valign="top" bgcolor="#6699cc" class="style3"><strong>Smart</strong></td>
                 </tr>
                 <tr>
                   <td valign="top" class="auto-style2"><strong>
                     <label> </label>
                     </strong>
                       <label>
                       <input type="checkbox" name="chk_communicate" id="chk_communicate" runat="server" />
                       </label>
                     ความสามารถในการสื่อสาร<strong><br />
                     </strong></td>
                   <td valign="top">
                     <input type="checkbox" name="chk_relative" id="chk_relative" runat="server" />
                  สัมพันธไมตรีแบบไทย</td>
                   <td valign="top">
                     <input type="checkbox" name="chk_talent" id="chk_talent" runat="server" />
                   ความเป็นเลิศทางวิชาการ</td>
                   <td valign="top">
                     <input type="checkbox" name="chk_quality" id="chk_quality" runat="server" />
                   คุณภาพงานบริการ</td>
                 </tr>
               </tbody>
           </table>
             </div>
             <table style="margin-left: -3px;" cellspacing="1" cellpadding="2" 
            width="100%">
              <tbody>
                <tr>
                  <td><asp:CheckBox ID="chk2015_1" runat="server" Text="Compassionate Caring บริการด้วยความเอื้ออาทร" Font-Bold="True" />&nbsp;</td>
                </tr>
         <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi1" runat="server" />
                          Exceed our customer&#39;s expectations / มุ่งมั่นที่จะให้บริการที่เกินความคาดหวังของลูกค้า </td>
                  </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi5" runat="server" />
                          Embrace cultural diversity with Thai hospitality / ให้การบริการแบบไทย แก่ผู้ป่วยทุกชาติ ภาษา และวัฒนธรรมอย่างเท่าเทียมกัน
                    </td>
                </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi9" runat="server" /> Value corporate social responsibilities / เป็นองค์กรที่ยึดมั่นในความรับผิดชอบต่อสังคม 
                    </td>
                </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi10" runat="server" /> Operate in an environmentally responsible manner / ให้ความสำคัญต่อการอนุรักษ์สิ่งแวดล้อมและทรัพยากรธรรมชาติ
                    </td>
                </tr>
                  -->
                <tr>
                  <td>
                      <asp:CheckBox ID="chk2015_2" runat="server" Font-Bold="True" Text="Adaptability, Learning, and Innovation มีความพร้อมในการปรับเปลี่ยน เรียนรู้ สร้างสรรค์นวัตกรรมใหม่ๆ" />
                    </td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi4" runat="server" /> Professional excellence and innovation / มุ่งมั่นที่ก้าวสู่ความเป็นเลิศทางวิชาชีพและนวัตกรรมทางการแพทย์อย่างต่อเนื่อง
                           </td>
                  </tr>
                  -->
                <tr>
                  <td>
                      <asp:CheckBox ID="chk2015_3" runat="server" Font-Bold="True" Text="Safety, Quality with Measurable Results ยึดมั่นเรื่องความปลอดภัย คุณภาพมีผลลัพธ์ที่วัดผลได้" />
                    </td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi3" runat="server" /> Continually improve the quality and safety / ปรับปรุงคุณภาพและความปลอดภัยของทุกสิ่งที่เราปฏิบัติอย่างต่อเนื่อง 
                          </td>
                  </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi6" runat="server" />
                          Make everything World Class / มุ่งมั่นที่จะพัฒนาการบริการทุกขั้นตอนให้ได้มาตรฐานระดับโลก
                    </td>
                </tr>
                  -->
                <tr>
                  <td>
                      <asp:CheckBox ID="chk2015_4" runat="server" Font-Bold="True" Text="Teamwork and Integrity ทำงานเป็นทีมและยึดมั่นหลักคุณธรรม" />
                    </td>
                </tr>
                  <!--
                  <tr>
                      <td>
                          &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi2" runat="server" /> Committed to our staff's welfare and development / มุ่งมั่นในการพัฒนาด้านการศึกษา การเรียนการสอน การเพิ่มศักยภาพและระบบสวัสดิการที่ดีต่อบุคลากรโรงพยาบาล
                          </td>
                  </tr>
                <tr>
                  <td>
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi7" runat="server" /> Be trusted, honest, and ethical in all our dealing / ปฏิบัติต่อผู้ป่วยด้วยความซื่อสัตย์ มีความไว้วางใจซึ่งกันและกัน และคงไว้ซึ่งจริยธรรม 
                    </td>
                </tr>
                <tr>
                  <td> 
                      &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkbi8" runat="server" />
                          Work as a team / ทำงานร่วมกันเป็นทีมและแลกเปลี่ยนสิ่งที่เรียนรู้แก่กันและกัน 
                    </td>
                </tr>
                  -->
                  </tbody>
                  </table>

         </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Effective Recognition</strong></td>
         <td valign="top">
         <asp:DropDownList ID="txthr_recog" runat="server" Width="185px">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Endorse</asp:ListItem>
               <asp:ListItem Value="2">Do Not Endorse</asp:ListItem>
                
             </asp:DropDownList>
      
        </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Effective Types of recognition</strong></td>
         <td valign="top"> <asp:DropDownList ID="txthr_type" runat="server" Width="185px" DataTextField="recognition_name" DataValueField="recognition_id">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Half Star Record</asp:ListItem>
               <asp:ListItem Value="2">Star Record</asp:ListItem>
               <asp:ListItem Value="3">Star Pin</asp:ListItem>
                 <asp:ListItem Value="4">Gold Pin</asp:ListItem>
                 
             </asp:DropDownList></td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>ข้อพิจารณาของคณะกรรมการ</strong></td>
         <td valign="top">
          <asp:DropDownList ID="txthr_commit" runat="server" Width="185px">
             <asp:ListItem Value="">-- Please Select --</asp:ListItem>
              <asp:ListItem Value="1">Suggestion / CFB</asp:ListItem>
               <asp:ListItem Value="2">Could not find Nominee name</asp:ListItem>
               <asp:ListItem Value="3">No submitted person</asp:ListItem>
               <asp:ListItem Value="4">Thank you card</asp:ListItem> 
                <asp:ListItem Value="4">Duplicate</asp:ListItem> 
                  <asp:ListItem Value="5">Other</asp:ListItem> 
             </asp:DropDownList>
      </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Comment</strong></td>
         <td bgcolor="#eef1f3"><textarea name="txtstar_comment" id="txtstar_comment" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top" 
            colspan="2"><strong>Reward</strong></td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Reward 
           Summary</strong></td>
         <td valign="top">&nbsp;</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"> - Staff Recognition Point</td>
         <td valign="top">
             <asp:DropDownList ID="txtsrp" runat="server">
             <asp:ListItem Value="0">0</asp:ListItem>
             <asp:ListItem Value="100">100</asp:ListItem>
             <asp:ListItem Value="200">200</asp:ListItem>
             <asp:ListItem Value="300">300</asp:ListItem>
             <asp:ListItem Value="1000">1000</asp:ListItem>
             <asp:ListItem Value="5000">5000</asp:ListItem>
             <asp:ListItem Value="10000">10000</asp:ListItem>
             </asp:DropDownList>
          
         </td>
       </tr>
       
       <tr>
         <td bgcolor="#eef1f3" valign="top">- Cash </td>
         <td valign="top">
           <asp:TextBox ID="txtcash" runat="server" Width="100px"></asp:TextBox>
        </td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Awarding Date</strong></td>
         <td valign="top"><asp:TextBox ID="txtdate_award" runat="server" Width="80px"></asp:TextBox>
          
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_award" PopupButtonID="Image2">

                              </asp:CalendarExtender>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
             &nbsp;วันที่สรุปผลรางวัล</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Reward Recieve 
           Date</strong></td>
         <td valign="top">
             <asp:TextBox ID="txtdate_receive" runat="server" Width="80px"></asp:TextBox>
               
                              <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_receive" PopupButtonID="Image3">

                              </asp:CalendarExtender>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
             &nbsp; วันที่พนักงานรับรางวัล</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Remark</strong></td>
         <td valign="top">
             <asp:TextBox ID="txtstar_remark" runat="server" Rows="3" TextMode="MultiLine" 
                 Width="80%"></asp:TextBox>
           </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
         <td valign="top">&nbsp;</td>
       </tr>
     </table>
     </fieldset>
    
     <fieldset>
                <legend> <strong>Statistical Analysis</strong> </legend>
                <table width="98%" align="center" cellpadding="2" cellspacing="1">
                  <tr>
                    <td valign="top" bgcolor="#eef1f3"><table cellspacing="1" cellpadding="2" width="100%">
                        <tbody>
                          <tr>
                            <td width="80"><strong>Main Topic</strong></td>
                            <td>
                                <asp:DropDownList ID="txttopic_main" runat="server" 
                                    DataTextField="main_topic_name_th" DataValueField="main_topic_id" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                              </td>
                            <td width="80">&nbsp;</td>
                            <td>&nbsp;</td>
                          </tr>
                          <tr>
                            <td class="style2"><strong>Sub Topic</strong></td>
                            <td class="style2">
                                <asp:DropDownList ID="txttopic_sub" runat="server" DataTextField="subtopic_name_th" DataValueField="subtopic_id">
                                </asp:DropDownList>
                              </td>
                            <td class="style2"></td>
                            <td class="style2"></td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                          </tr>
                          <tr>
                            <td style="vertical-align: top;">&nbsp;</td>
                            <td colspan="3">&nbsp;</td>
                          </tr>
                        </tbody>
                      </table></td>
                  </tr>
                </table>
              </fieldset>
               </asp:Panel>
               </ContentTemplate>
      </asp:UpdatePanel>
             
  </div>
    
 <div class="tabbertab" id="tab_update" runat="server" visible="false">
  <h2>Additional Comment <asp:Label ID="lblNumComment" runat="server" /></h2>
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
                              <asp:BoundField DataField="inform_detail" HeaderText="Message" ItemStyle-Width="400px" />
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
        <asp:Button ID="cmdAddUpdate" runat="server" Text="Save review" 
            CausesValidation="False" />
        </td>
    </tr>
    </table>
  
      <br />
</div>
  <br />
  
</div><br />
      <div align="right">
        <div align="right"> 
            <asp:Button ID="cmdDraft2" runat="server" Text="Save as Draft" 
                OnCommand="onSave" CommandArgument="1" CausesValidation="False" />
                  <asp:Button ID="cmdCopy2" runat="server" Text="Copy as Draft" 
                    OnCommand="onSave" CommandArgument="11" CausesValidation="False" 
                    Visible="False"  OnClientClick="return confirm('Are you sure you want to copy as draft?');"  />
                 <asp:Button ID="cmdSubmit2" runat="server" Text="Submit" 
                OnCommand="onSave" CommandArgument="2" 
                OnClientClick="return confirm('Are you sure you want to submit your Star of bumrunrad?');" 
                Font-Bold="True"  />
  &nbsp;</div>
      </div>
      </div>
      
</asp:Content>


