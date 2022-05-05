<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="form_ssip.aspx.vb" Inherits="ssip_form_ssip" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    var global_time ;
    $(document).ready(function () {
     checkSession('<%response.write(session("bh_username").toString) %>' , '<%response.write(viewtype) %>'); // Check session every 1 sec.
    });

    function openPopup(flag, popupType) {
        if (flag.checked) {
            is_sms = 1;
        } else {
            is_sms = 0;
        }

        window.open('../incident/popup_recepient.aspx?modules=ssip&popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=620');
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
</script>
    <style type="text/css">
        .style2
        {
            height: 30px;
        }
        .style3
        {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <table width="100%" cellpadding="1" cellspacing="1">
          <tr>
            <td><div id="header"><img src="../images/menu_05.png" alt="SSIP" width="32" height="32" align="absmiddle"  />&nbsp;Staff Suggestion and Innovation Program (SSIP) </div></td>
            <td><div style="margin-right: 10px;" align="right">
              <strong><asp:Label ID="Label4" runat="server" Text='Status: '></asp:Label></strong>
              <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="status_name" DataValueField="status_id" Font-Bold="True" 
                    BackColor="Aqua" ForeColor="Blue" Enabled="False">            </asp:DropDownList>
                <asp:Button ID="cmdDraft1" runat="server" Text="Save as Draft" OnCommand="onSave" CommandArgument="1" />
                 <asp:Button ID="cmdSubmit" runat="server" Text="Submit Suggestion" 
                    OnCommand="onSave" CommandArgument="2" 
                    OnClientClick="return confirm('Are you sure you want to submit your SSIP?');" 
                    Font-Bold="True" />
                 <asp:Button ID="cmdRecv1" runat="server" Text="รับเรื่อง (Receive Suggestion)" OnCommand="onSave" CommandArgument="4" />
                 <asp:Button ID="cmdHRReview1" runat="server" Text="Save Review" oncommand="onSave" CommandArgument="99" />
</div></td>
          </tr>
        </table>
  <div id="data">

  <div class="tabber" id="mytabber2" runat="server">
      <div class="tabbertab" id="tabSendMail" runat="server">
        <h2>SSIP Alert</h2>
       <table width="100%" cellspacing="1" cellpadding="2">
  <tr>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="150" valign="top"><input type="button" name="button3" id="button1" value="Address" style="width: 85px;" onclick="openPopup($('#ctl00_ContentPlaceHolder1_chk_sms') ,  'to')" /></td>
        <td valign="top"><input type="text" name="txtto" id="txtto" style="width: 635px;" runat="server" readonly="readonly" />
            <asp:Button ID="cmdSendMail" runat="server" Text=" Send " /></td>
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
            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="4">Need Department Review</asp:ListItem>
            <asp:ListItem Value="5">Need Committee Team Consideration</asp:ListItem>
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
<div class="tabbertab" id="tabMailLog" runat="server">
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
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
        <br /></div>
   <div class="tabbertab" id="tabAttachFile" runat="server">
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
                      <a href="../share/ssip/attach_file/<%# Eval("file_path") %>" target="_blank">
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
              <h2>Review log</h2>
   <asp:GridView ID="GridviewIDPLog" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="log_status_id" AllowPaging="True" ShowFooter="True" 
                  EnableModelValidation="True">
               <Columns>
                   <asp:BoundField DataField="ssip_status_name" HeaderText="Action" >
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
        <h2><strong>SSIP </strong>Details</h2>
     
        <asp:Panel ID="panelDetail" runat="server">
        <table width="100%" cellspacing="0" cellpadding="3">
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong><span class="red">*</span>ชื่อเรื่อง (Innovation Subject)</strong></td>
        <td><asp:TextBox ID="txttopic" runat="server" Width="735px"></asp:TextBox></td>
      </tr>
      <tr>
        <td width="182" valign="top" bgcolor="#eef1f3"><strong><span class="red">*</span>เกี่ยวข้องกับแผนก <br />
          (Relevant Department)</strong></td>
        <td>
         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
      <ContentTemplate>
      
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td width="210" valign="top">
              
              <asp:ListBox ID="txtdept_all" runat="server" 
                      DataValueField="costcenter_id" DataTextField="dept_name_en" Width="200px" 
                      AutoPostBack="True" SelectionMode="Multiple">                  </asp:ListBox>                  </td>
                <td valign="top" width="60">
                    <asp:Button ID="cmdAddRelateDept" runat="server" Text=" &gt;&gt;" /><br /><br />
                     <asp:Button ID="cmdRemoveRelateDept" runat="server" Text=" <<" />                </td>
              <td width="492" valign="top">
                  <asp:ListBox ID="txtdept_select" runat="server" DataTextField="costcenter_name" 
                      DataValueField="costcenter_id" Width="200px"></asp:ListBox>                </td>
            </tr>
        </table>
         </ContentTemplate>
      </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>รายชื่อผู้ร่วมเสนอนวัตกรรม<br />
        (Suggestion Team)</strong></td>
        <td>
         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
      <ContentTemplate>
        <table width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="210" valign="top">
                <asp:ListBox ID="txtperson_all" runat="server" 
                    Width="200px" DataTextField="user_fullname" DataValueField="emp_code" 
                    SelectionMode="Multiple">                  </asp:ListBox>
                  &nbsp;</td>
              <td valign="top" width="60">
                  <asp:Button ID="cmdAddRelatePerson" runat="server" Text=" &gt;&gt;" /><br /><br />
                  <asp:Button ID="cmdRemoveRelatePerson" runat="server" Text=" &lt;&lt;" />              </td>
            <td width="492" valign="top"><asp:ListBox ID="txtperson_select" runat="server" 
                    Width="200px"  DataTextField="user_fullname" DataValueField="emp_code">
                  </asp:ListBox>                  </td>
          </tr>
        </table>
           </ContentTemplate>
      </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>สถานะของข้อเสนอแนะ<br />
        (Suggestion status)</strong></td>
        <td>
                        <label><asp:RadioButton ID="txts1" runat="server" GroupName="status" />
          &nbsp;</label>ยังไม่ปฏิบัติ (No Experimental)<br />
         <asp:RadioButton ID="txts2" runat="server" GroupName="status" />
          อยู่ระหว่างปฏิบัติการ (During experimental)<br />
        <asp:RadioButton ID="txts3" runat="server" GroupName="status" />
          ปฏิบัติแล้ว (Already Operation) เมื่อ (Date)
            <asp:TextBox ID="txtdate_status" runat="server" Width="135" BackColor="#EEEEEE"></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender1"
                                TargetControlID="txtdate_status" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_status" PopupButtonID="Image1">                              </asp:CalendarExtender>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
         <br />
         <asp:RadioButton ID="txts4" runat="server" GroupName="status" />
          ไม่ทราบ (Unknown)</td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>ที่มาของข้อเสนอแนะ (Source of Ideas)</strong></td>
        <td>
        <asp:DropDownList ID="txtsourcetype" runat="server">
        <asp:ListItem Value="1">Customer</asp:ListItem>
        <asp:ListItem Value="2">Others company</asp:ListItem>
        <asp:ListItem Value="3">Research</asp:ListItem>
        <asp:ListItem Value="4">Employee</asp:ListItem>
        <asp:ListItem Value="5">Others</asp:ListItem>
        </asp:DropDownList>
       
          ระบุ
          <input type="text" name="txtsource" id="txtsource" style="width: 450px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong><span class="red">*</span>ข้อเสนอแนะและนวัตกรรมของข้าพเจ้าสามารถ<br /> 
          (I believe my suggest will)</strong></td>
        <td><label>
          <input type="checkbox" name="chk_suggest1" id="chk_suggest1" runat="server" />
          </label>
          เพิ่มความพึงพอใจให้แก่ลูกค้า (Improve customer satisfaction)<br />
          <input type="checkbox" name="chk_suggest2" id="chk_suggest2" runat="server" />
          ประหยัดค่าใช้จ่ายได้เป็นอย่างมาก (Reduce costs)<br />
          <input type="checkbox" name="chk_suggest3" id="chk_suggest3" runat="server" />
          เพิ่มรายได้หรือกำไรให้แก่องค์กร (Increase revenuse / Benefits)<br />
          <input type="checkbox" name="chk_suggest4" id="chk_suggest4" runat="server" />
          เพิ่มประสิทธิภาพในการทำงาน (Increase productivity)<br />
          <input type="checkbox" name="chk_suggest5" id="chk_suggest5" runat="server" />
          ปรับปรุงคุณภาพของสถานที่ทำงาน (Improve quality of workplace)</td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3">
            <strong>ประโยชน์อื่นๆ</strong></td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                  <ContentTemplate>
                      <table cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                              <td valign="top" width="210">
                                  <asp:ListBox ID="txtBenefit_all" runat="server" DataTextField="master_benefit_name" 
                                      DataValueField="master_benefit_id" SelectionMode="Multiple" Width="200px">
                                  </asp:ListBox>
                                  &nbsp;</td>
                              <td valign="top" width="60">
                                  <asp:Button ID="cmdAddRelateBenefit" runat="server" Text=" &gt;&gt;" />
                                  <br />
                                  <br />
                                  <asp:Button ID="cmdRemoveRelateBenefit" runat="server" Text=" &lt;&lt;" />
                              </td>
                              <td valign="top" width="492">
                                  <asp:ListBox ID="txtBenefit_select" runat="server" 
                                      DataTextField="master_benefit_name" DataValueField="master_benefit_id" Width="200px">
                                  </asp:ListBox>
                              </td>
                          </tr>
                      </table>
                  </ContentTemplate>
              </asp:UpdatePanel>
          </td>
      </tr>
            <tr>
                <td colspan="2" valign="top">
                    <asp:GridView ID="GridSuggest" runat="server" AutoGenerateColumns="False" 
                        CellPadding="3" CssClass="tdata" DataKeyNames="solution_id" 
                        EnableModelValidation="True" HeaderStyle-CssClass="colname" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPk" runat="server" Text='<%# bind("solution_id") %>' 
                                        Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtorder" runat="server" Text='<%# bind("order_sort") %>' 
                                        Width="30px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สิ่งที่ดำเนินการอยู่ในปัจจุบัน และข้อปัญหา (The way it is now and Problem)">
                                <ItemTemplate>
                                    <asp:Label ID="lblProblem" runat="server" Text='<%# Bind("problem_detail") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="450px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ข้อแสนอแนะและนวัตกรรมของข้าพเจ้า (My suggestion)">
                                <ItemTemplate>
                                    <asp:Label ID="lblSuggest" runat="server" 
                                        Text='<%# Bind("suggestion_detail") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="colname" />
                    </asp:GridView>
                    <table cellpadding="3" cellspacing="0" class="tdata" width="100%">
                        <%If GridSuggest.Rows.Count = 0 Then%>
                        <tr>
                            <td class="colname" width="30">
                                &nbsp;</td>
                            <td class="colname" width="30">
                                &nbsp;</td>
                            <td class="colname">
                                <strong>สิ่งที่ดำเนินการอยู่ในปัจจุบัน และข้อปัญหา (The way it is now and 
                                Problem)&nbsp;</strong></td>
                            <td class="colname">
                                <strong>ข้อแสนอแนะและนวัตกรรมของข้าพเจ้า (My suggestion)&nbsp;</strong></td>
                        </tr>
                        <%End If %>
                        <tr>
                            <td valign="top" width="30">
                                &nbsp;</td>
                            <td valign="top" width="30">
                                &nbsp;</td>
                            <td valign="top" width="450">
                                <asp:TextBox ID="txtadd_problem" runat="server" Rows="3" TextMode="MultiLine" 
                                    Width="350px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtadd_suggest" runat="server" Rows="3" TextMode="MultiLine" 
                                    Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                &nbsp;</td>
                            <td valign="top">
                                <asp:Button ID="cmdAddTopic" runat="server" Text="Add Topic" />
                                &nbsp;
                                <asp:Button ID="cmdDelTopic" runat="server" Text="Delete" />
                                &nbsp;
                                <asp:Button ID="cmdOrderTopic" runat="server" Text="Save Order" 
                                    Visible="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
      <tr>
        <td colspan="2" valign="top"><strong>ตัวอย่างเอกสาร หรือภาพก่อนและหลังปรับปรุง (Document or Picture)</strong></td>
      </tr>
      <tr>
        <td colspan="2" valign="top">
        <asp:GridView ID="GridFile" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="file_id" HeaderStyle-CssClass="colname" 
                EnableModelValidation="True">
           <Columns>
               <asp:TemplateField>
               <ItemStyle Width="30px" />
                   <ItemTemplate>
                       <asp:CheckBox ID="chkSelect" runat="server" />                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="No.">
               
               <ItemStyle Width="30px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblPk" runat="server" Text='<%# bind("file_id") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblFilePath1" runat="server" Text='<%# Bind("file_path_before") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblFilePath2" runat="server" Text='<%# Bind("file_path_after") %>' Visible="false"></asp:Label>
                       <asp:TextBox ID="txtorder" runat="server"  Width="30px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Before">
                   <ItemTemplate>
                        <a href="../share/ssip/attach_file/<%# Eval("file_path_before") %>" target="_blank">
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name_before") %>'></asp:Label>
                       </a>                   </ItemTemplate>
                 
                   <ItemStyle Width="450px" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="After">
                   <ItemTemplate>
                    <a href="../share/ssip/attach_file/<%# Eval("file_path_after") %>" target="_blank">
                       <asp:Label ID="Label2" runat="server" Text='<%# Bind("file_name_after") %>'></asp:Label>
                      </a>                   </ItemTemplate>
                  
                   <ItemStyle Width="450px" />
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
        <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
        <%if GridFile.rows.count = 0 then %>
            <tr>
              <td width="30" class="colname">&nbsp;</td>
              <td width="30" class="colname">&nbsp;</td>
              <td class="colname"><strong>ภาพประกอบก่อนปรับปรุง (Before improve picture) </strong></td>
              <td class="colname"><strong>ภาพประกอบหลังปรับปรุง (After imporve picture)</strong></td>
            </tr>
   <%end if %>
            <tr>
              <td valign="top">&nbsp;</td>
              <td valign="top">&nbsp;</td>
              <td valign="top">
                  <asp:FileUpload ID="FileUpload1" runat="server" />                </td>
              <td valign="top"><label>
                  <asp:FileUpload ID="FileUpload2" runat="server" />
                  &nbsp;</label>&nbsp;<asp:Button ID="Button11" runat="server" 
                      Text="Upload file" />
                  &nbsp;<asp:Button ID="cmdDelFile" runat="server" Text="Delete file" />                </td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="2" valign="top"><strong>ประโยชน์ที่จะได้จากข้อเสนอแนะและนวัตกรรมของข้าพเจ้า (Advantages/Benefits from my suggestion)</strong></td>
      </tr>
      <tr>
        <td colspan="2" valign="top">
        <asp:GridView ID="GridBenefit" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="benefit_id" HeaderStyle-CssClass="colname" 
                EnableModelValidation="True">
           <Columns>
               <asp:TemplateField>
               <ItemStyle Width="30px" />
                   <ItemTemplate>
                       <asp:CheckBox ID="chkSelect" runat="server" />                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="No.">
               
               <ItemStyle Width="30px" HorizontalAlign="Center" />
                   <ItemTemplate>
                       <asp:Label ID="lblPk" runat="server" Text='<%# bind("benefit_id") %>' Visible="false"></asp:Label>
                       <asp:TextBox ID="txtorder" runat="server"  Width="30px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="benefit_detail" 
                   HeaderText="Advantages/Benefits" >
                   <ItemStyle Width="450px" />               </asp:BoundField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
        <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
        <%If GridBenefit.Rows.Count = 0 Then%>
            <tr>
              <td width="30" class="colname">&nbsp;</td>
              <td width="30" class="colname"><strong>No</strong></td>
              <td class="colname"><strong>Advantages/Benefits</strong></td>
            </tr>
          <%End If%>
            <tr>
              <td valign="top" width="30">&nbsp;</td>
              <td valign="top" width="30">&nbsp;</td>
              <td valign="top"><asp:TextBox ID="txtadd_benefit" runat="server"  Width="350px" 
                      Rows="3" TextMode="MultiLine"></asp:TextBox>
&nbsp;   <asp:Button ID="cmdAddBenefit" runat="server" Text="Add Topic" 
                       />
                  &nbsp;
                   <asp:Button ID="cmdDelBenefit" runat="server" Text="Delete" />&nbsp;
                  <asp:Button ID="cmdOrderBenefit" runat="server" Text="Save Order" 
                      Visible="False" /></td>
            </tr>
            <tr>
              <td valign="top">&nbsp;</td>
              <td valign="top">&nbsp;</td>
              <td valign="top">&nbsp;</td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="2" valign="top"><strong>
          <input type="checkbox" name="chk_confirm" id="chk_confirm" runat="server" />
          <span class="red">ข้าพเจ้า ขอรับรองว่า ข้าพเจ้าเข้าใจหลักเกณฑ์ของโครงการฯ รวมทั้งการพิจารณารางวัลเรียบร้อยแล้ว <a href="http://bhtraining/ssip/ssip_objective.aspx" target="blank">[คลิกเพื่ออ่านรายละเอียดของโครงการ]</a></span></strong></td>
      </tr>
      <tr>
        <td colspan="2" valign="top"><a href="#"><strong>สอบถามข้อมูลเพิ่มเติมได้ที่ : คุณจิตติมา สุนทรกลัมพ์ ผู้ประสานงานโครงการข้อเสนอแนะและนวัตกรรม โทร. 72494<br />
  For more information, please contact Khun Chittima Suntornklam ,<br /> 
  The coordinator of the Staff Suggestion &amp; Innovation Committee on the extension number 72494</strong></a></td>
      </tr>
    </table>
          
    </asp:Panel>
   
     <asp:Panel ID="panelDetail2" runat="server">
     <table width="100%" cellspacing="0" cellpadding="3">
          <tr>
            <td valign="top" bgcolor="#eef1f3" class="style3"><span class="theader"><strong>Staff Suggestion and Innovation No. </strong></span>
                </td>
            <td valign="top" bgcolor="#eef1f3" class="style3"><asp:Label ID="lblSSIPNo" runat="server" Text="" Font-Bold="true"></asp:Label></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Employee Name</strong></td>
            <td width="806"> 
                <asp:Label ID="lblSSIPEmpName" runat="server" Font-Bold="true" Text=""></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
              </td>
          </tr>
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>Employee No.</strong> </td>
              <td width="806">
                  <asp:Label ID="lblSSIPempCode" runat="server" Font-Bold="true" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>Department</strong></td>
              <td width="806">
                  <asp:Label ID="lblSSIPDeptName" runat="server" Font-Bold="true" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>ชื่อเรื่อง (Innovation Subject)</strong></td>
              <td width="806">
                  <asp:Label ID="lblSSIPTopic" runat="server" Font-Bold="true" Text=""></asp:Label>
              </td>
          </tr>
          <tr>
            <td width="182" valign="top" bgcolor="#eef1f3"><strong>เกี่ยวข้องกับแผนก<br />
            (Relevant department)</strong></td>
            <td><table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="492" valign="top">
                      <asp:ListBox ID="txtSSIPRelateDept" runat="server" DataTextField="costcenter_name" 
                      DataValueField="costcenter_id" Width="300px"></asp:ListBox>                    </td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>รายชื่อผู้ร่วมเสนอนวัตกรรม<br />
            (Suggestion Team)</strong></td>
            <td> <asp:ListBox ID="txtSSIPRelatePerson" runat="server"  
                    DataTextField="user_fullname" DataValueField="emp_code" Width="300px"></asp:ListBox></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>สถานะของข้อเสนอแนะ<br />
            (Suggestion status)</strong></td>
            <td><asp:Label ID="lblSSIPStatusName" runat="server" Text="Label" Font-Bold="true"></asp:Label> </td>
          </tr>
         
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>ที่มาของข้อเสนอแนะ<br />
            (Source of Ideas)</strong></td>
            <td><asp:Label ID="lblSSIPFrom" runat="server" Text="Label" Font-Bold="true" ></asp:Label></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>ข้อเสนอแนะและนวัตกรรมของข้าพเจ้าสามารถ<br /> 
              (I believe my suggest will)</strong></td>
            <td style="vertical-align:top">
                <asp:Label ID="lblSSIPSuggest" runat="server" Text="Label" Font-Bold="true" ></asp:Label></td>
          </tr>
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>ประโยชน์อื่นๆ</strong></td>
              <td style="vertical-align:top">
                  <asp:ListBox ID="txtSSIPRelateBenefit" runat="server" 
                      DataTextField="master_benefit_name" DataValueField="master_benefit_id" Width="300px">
                  </asp:ListBox>
              </td>
          </tr>
          <tr>
            <td colspan="2" valign="top"> 
                <asp:GridView ID="GridSuggest2" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="solution_id" HeaderStyle-CssClass="colname" 
                EnableModelValidation="True">
           <Columns>
               <asp:TemplateField HeaderText="No.">
               
               <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Top"/>
                   <ItemTemplate>
                       <asp:Label ID="lblPk" runat="server" Text='<%# bind("solution_id") %>' Visible="false"></asp:Label>
                      <asp:TextBox ID="txtorder" runat="server"  Width="30px" Text='<%# bind("order_sort") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="สิ่งที่ดำเนินการอยู่ในปัจจุบัน และข้อปัญหา (The way it is now and Problem)">
                   <ItemTemplate>
                       <asp:Label ID="lblProblem" runat="server" Text='<%# Bind("problem_detail") %>'></asp:Label>
                   </ItemTemplate>
                 
                   <ItemStyle VerticalAlign="Top" Width="450px" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="ข้อแสนอแนะและนวัตกรรมของข้าพเจ้า (My suggestion)">
               <ItemStyle VerticalAlign="Top" Width="450px" />
                   <ItemTemplate>
                       <asp:Label ID="lblSuggest" runat="server" Text='<%# Bind("suggestion_detail") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView></td>
          </tr>
          <tr>
            <td colspan="2" valign="top"><strong>ตัวอย่างเอกสาร หรือภาพก่อนและหลังปรับปรุง</strong></td>
          </tr>
          <tr>
            <td colspan="2" valign="top">
                <asp:GridView ID="GridFile2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="3" CssClass="tdata" DataKeyNames="file_id" 
                    EnableModelValidation="True" HeaderStyle-CssClass="colname" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                          <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPk0" runat="server" Text='<%# bind("file_id") %>' 
                                    Visible="false"></asp:Label>
                              <asp:TextBox ID="txtorder0" runat="server" Text='<%# bind("order_sort") %>' 
                                    Width="30px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ภาพประกอบก่อนปรับปรุง (Before improve picture)">
                        
                            <ItemTemplate> <a href="../share/ssip/attach_file/<%# Eval("file_path_before") %>">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name_before") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                          
                            <ItemStyle VerticalAlign="Top" Width="450px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ภาพประกอบหลังปรับปรุง (After imporve picture)">
                            <ItemTemplate>
                             <a href="../share/ssip/attach_file/<%# Eval("file_path_after") %>">
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("file_name_after") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                          
                            <ItemStyle VerticalAlign="Top" Width="450px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="colname" />
                </asp:GridView>              </td>
          </tr>
          <tr>
            <td colspan="2" valign="top"><strong>ประโยชน์ที่จะได้จากข้อเสนอแนะและนวัตกรรมของข้าพเจ้า (Advantages/Benefits from my suggestion)</strong></td>
          </tr>
          <tr>
            <td colspan="2" valign="top">
                <asp:GridView ID="GridBenefit2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="3" CssClass="tdata" DataKeyNames="benefit_id" 
                    EnableModelValidation="True" HeaderStyle-CssClass="colname" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPk1" runat="server" Text='<%# bind("benefit_id") %>' 
                                    Visible="false"></asp:Label>
                                <asp:TextBox ID="txtorder1" runat="server" Text='<%# bind("order_sort") %>' 
                                    Width="30px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="benefit_detail" HeaderText="Advantages/Benefits">
                        <ItemStyle />                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="colname" />
                </asp:GridView>              </td>
          </tr>
        </table>
     </asp:Panel>
  </div>
      <div class="tabbertab" id="tabHR" runat="server">
      
        <h2>HRD Reply</h2>  
        <asp:Panel ID="panelHR1" runat="server">
          <table width="100%" cellspacing="1" cellpadding="2">
      <tr>
        <td width="350" valign="top" bgcolor="#eef1f3"><strong>ผลการพิจารณา</strong></td>
        <td valign="top"><asp:Label ID="lblResult" runat="server"  Text="-"></asp:Label>  </td>
      </tr>
     
           
              <tr>
                  <td bgcolor="#eef1f3" valign="top">
                      <strong>รายละเอียด</strong></td>
                  <td valign="top">
                      <asp:Label ID="lblDetail" runat="server" Text="-"></asp:Label>
                  </td>
              </tr>
      <tr>
        
        <td valign="top" bgcolor="#eef1f3" style="vertical-align:top"><p><strong><u>ข้อพิจารณาของคณะกรรมการ</u></strong></p></td>
        <td valign="top"><asp:Label ID="lblCommittee" runat="server" Text="-"></asp:Label></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>Implemented Department</strong></td>
        <td valign="top" bgcolor="#eef1f3">
            <asp:ListBox ID="txtdept_impl" runat="server" Width="185px" DataTextField="dept_name_en" 
                        DataValueField="costcenter_id" >
            
            </asp:ListBox>
      </td>
      </tr>
      <tr>
        <td colspan="2" valign="top" bgcolor="#eef1f3"><strong>Reward</strong></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>Awarding Date</strong></td>
        <td valign="top"><input name="txtawarddate" type="text" disabled="disabled" id="txtawarddate" style="width: 120px;" value="" readonly="readonly" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>Individual Rewarding Summary</strong></td>
        <td valign="top"><table width="100%" cellspacing="0" cellpadding="3">
            <tr>
              <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Points</strong></td>
            </tr>

            <tr>
              <td valign="top"><asp:Label ID="lblSumReward" runat="server" Text=""></asp:Label></td>
            </tr>

        </table></td>
          
      </tr>
    </table>
    </asp:Panel>
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
         <ContentTemplate>
      <asp:Panel ID="panelHR2" runat="server">
        <table width="100%" cellpadding="2" cellspacing="1">
          <tr>
             
            <td width="350" valign="top" bgcolor="#eef1f3"><strong>ผลการพิจารณา</strong></td>
            <td valign="top">
             <asp:DropDownList ID="txtSSIPResult" runat="server" Width="735px">
              <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
             <asp:ListItem Value="1">Pass consideration</asp:ListItem>
              <asp:ListItem Value="2">Not pass</asp:ListItem>
               <asp:ListItem Value="3">Duplicate subject</asp:ListItem>
                <asp:ListItem Value="4">Good ideas, but need more detail</asp:ListItem>
                  <asp:ListItem Value="5">Good ideas, can be implement</asp:ListItem>
              </asp:DropDownList>
           </td>
          </tr>
          
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>รายละเอียด</strong></td>
            <td valign="top"><textarea name="txtdetail" id="txtdetail" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
          </tr>
         
        <tr><td valign="top" bgcolor="#eef1f3"><strong></strong></td>
        <td valign="top">
            <asp:CheckBox ID="txtpublic" runat="server" Text="Show this idea to everyone" />
          </td>
      </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><p><strong><u>ข้อพิจารณาของคณะกรรมการ</u></strong></p></td>
            <td valign="top"><textarea name="txtcommittee" id="txtcommittee" cols="45" rows="5" style="width: 735px" runat="server"></textarea></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>แก้ไขชื่อเรื่อง <br />
              (Innovation Subject)</strong></td>
            <td valign="top" bgcolor="#eef1f3"><input type="text" name="txtedittopic" id="txtedittopic" style="width: 635px;" runat="server" /></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>คำค้นหา<br />(Keyword)</strong></td>
            <td valign="top" bgcolor="#eef1f3">
                <input type="text" name="txtkeyword" id="txtkeyword" 
                    style="width: 635px;" runat="server" />
              </td>
          </tr>
            <tr>
                <td bgcolor="#eef1f3" valign="top">
                    <strong>รายละเอียด<br />(Description)</strong></td>
                <td bgcolor="#eef1f3" valign="top">
                    <input type="text" name="txtdescription" id="txtdescription" 
                    style="width: 635px;" runat="server" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#eef1f3" valign="top">
                    <strong>แก้ไขเกี่ยวข้องกับแผนก<br /> (Relevant Department)</strong></td>
                <td bgcolor="#eef1f3" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td valign="top" width="312">
                                <asp:ListBox ID="txthr_alldept" runat="server" DataTextField="dept_name_en" 
                                    DataValueField="dept_id" SelectionMode="Multiple" Width="250px">
                                </asp:ListBox>
                            </td>
                            <td valign="top" width="80">
                                <asp:Button ID="cmdHRAdd" runat="server" CausesValidation="False" 
                                    Text="&gt;&gt;" />
                                <Br />
                                <asp:Button ID="cmdHrRemove" runat="server" CausesValidation="False" 
                                    Text="&lt;&lt;" />
                            </td>
                            <td valign="top">
                                <asp:ListBox ID="txthr_selectdept" runat="server" DataTextField="dept_name_en" 
                                    DataValueField="costcenter_id" SelectionMode="Multiple" Width="250px">
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>แผนกที่นำไปใช้<br />
              (Implemented Department)</strong></td>
            <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="0" cellpadding="0">
              <tr>
                <td width="312" valign="top">
                    <asp:ListBox ID="txthr_alldept2" runat="server" Width="250px"  DataTextField="dept_name_en" 
                      DataValueField="costcenter_id" SelectionMode="Multiple"></asp:ListBox>
&nbsp;</td>
                  <td valign="top" width="80">
                      <asp:Button ID="cmdImplAdd" runat="server" Text="&gt;&gt;" 
                          CausesValidation="False" />
                      <br />
                      <asp:Button ID="cmdImplRemove" runat="server" Text="&lt;&lt;" 
                          CausesValidation="False" />
                  </td>
                <td  valign="top">
                    <asp:ListBox ID="txthr_impldept" runat="server" DataTextField="dept_name_en" 
                        DataValueField="costcenter_id" Width="250px" SelectionMode="Multiple"></asp:ListBox>
                  </td>
              </tr>
            </table></td>
          </tr>
           <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Effective </strong></td>
            <td valign="top"><strong>Individual Rewarding Summary </strong>
                <asp:DropDownList ID="txtreward_sum" runat="server" Width="150px" 
                    AutoPostBack="True">
                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                 <asp:ListItem Value="1">Moderate</asp:ListItem>
                  <asp:ListItem Value="2">Substantial</asp:ListItem>
                   <asp:ListItem Value="3">High</asp:ListItem>
                    <asp:ListItem Value="4">Exceptional</asp:ListItem>
                </asp:DropDownList>
             
           </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
            <td valign="top"><strong>Intangible Benefits Award Scale</strong>
             <asp:DropDownList ID="txtaward_scale" runat="server" Width="150px" 
                    AutoPostBack="True">
                <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                 <asp:ListItem Value="1">Improve Environment and Safety</asp:ListItem>
                  <asp:ListItem Value="2">Increase Efficiency</asp:ListItem>
                   <asp:ListItem Value="3">Customer	Service</asp:ListItem>
                    <asp:ListItem Value="4">Healthcare</asp:ListItem>
                </asp:DropDownList>
            
            </td>
          </tr>
          <tr>
            <td colspan="2" valign="top" bgcolor="#eef1f3">
            <asp:Panel ID="panel_reward" runat="server">
            <table width="80%" align="center" cellpadding="3" cellspacing="0">
              <tr>
                <td width="312" valign="top" bgcolor="#3399CC"><strong>Value of Benefit</strong></td>
                <td width="492" valign="top" bgcolor="#3399CC"><strong>Points</strong></td>
                <td width="492" valign="top" bgcolor="#3399CC"><strong>
                    <asp:Label ID="lblscale1" runat="server" Text="Improve Environment and Safety"></asp:Label></strong></td>
                <td width="492" valign="top" bgcolor="#3399CC"><strong> <asp:Label ID="lblscale2" runat="server" Text="Increase Efficiency"></asp:Label></strong></td>
                <td width="492" valign="top" bgcolor="#3399CC"><strong><asp:Label ID="lblscale3" runat="server" Text="Customer	Service"></asp:Label></strong></td>
                <td width="492" valign="top" bgcolor="#3399CC"><strong><asp:Label ID="lblscale4" runat="server" Text="Healthcare"></asp:Label></strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#6699CC"><strong> 
                    <asp:Label ID="lblbenefit1" runat="server" Text="Moderate"></asp:Label><br />
                </strong></td>
                <td valign="top">
                         30 - 44</td>
                <td valign="top"> <asp:Label ID="lblreward1" runat="server" Text="1,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward5" runat="server" Text="2,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward9" runat="server" Text="3,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward13" runat="server" Text="4,000"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#6699CC"><strong><asp:Label ID="lblbenefit2" runat="server" Text="Substantial"></asp:Label>  </strong></td>
                <td valign="top">45 - 64</td>
                <td valign="top"> <asp:Label ID="lblreward2" runat="server" Text="2,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward6" runat="server" Text="3,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward10" runat="server" Text="4,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward14" runat="server" Text="5,000"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#6699CC"><strong> <asp:Label ID="lblbenefit3" runat="server" Text="High"></asp:Label> </strong></td>
                <td valign="top">65 - 84</td>
                <td valign="top"> <asp:Label ID="lblreward3" runat="server" Text="3,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward7" runat="server" Text="4,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward11" runat="server" Text="5,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward15" runat="server" Text="6,000"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#6699CC"><strong><asp:Label ID="lblbenefit4" runat="server" Text="Exceptional"></asp:Label>  </strong></td>
                <td valign="top">85 - 100</td>
                <td valign="top"> <asp:Label ID="lblreward4" runat="server" Text="4,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward8" runat="server" Text="5,000"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward12" runat="server" Text="6,000+"></asp:Label></td>
                <td valign="top"> <asp:Label ID="lblreward16" runat="server" Text="10,000+"></asp:Label></td>
              </tr>
            </table>
            </asp:Panel>
            </td>
            </tr>
               <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Tangible Benefits Award </strong></td>
            <td valign="top" bgcolor="#EEF1F3">&nbsp;</td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Estimated 1st Year Benefits</strong></td>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="25%"><span style="font-weight: bold">
                    <asp:TextBox ID="txtbenefit_old" runat="server" Width="150px" 
                        AutoPostBack="True" MaxLength="9"></asp:TextBox>
                 
                  -</span></td>
                <td width="25%"><span style="font-weight: bold">
                <asp:TextBox ID="txtbenefit_new" runat="server" Width="150px" AutoPostBack="True" 
                        MaxLength="9"></asp:TextBox>
             
                  x</span></td>
                <td width="25%"><span style="font-weight: bold">
                  <asp:TextBox ID="txtbenefit_factor" runat="server" Width="150px" 
                        AutoPostBack="True" MaxLength="9"></asp:TextBox>
               
                  =</span></td>
                <td width="25%">
                 <asp:TextBox ID="txtbenefit_final" runat="server" Width="150px" MaxLength="9" 
                        BackColor="#FFFF99" AutoPostBack="True"></asp:TextBox>
                
                  <strong>บาท</strong></td>
              </tr>
              <tr>
                <td><strong>ค่าใช้จ่ายต่อปี (วิธีเก่า)</strong></td>
                <td><strong>ค่าใช้จ่ายต่อปี  (วิธีที่นำเสนอใหม่)</strong></td>
                <td><strong>ค่าใช้จ่ายในการนำไปใช้ - รายได้ </strong></td>
                <td><strong>จำนวนที่คาดว่าจะประหยัด</strong></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Amount of Award</strong></td>
            <td valign="top">
            <asp:TextBox ID="txtamount_reward" runat="server" Width="150px" AutoPostBack="True" 
                   ></asp:TextBox> 
               <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                        runat="server" ControlToValidate="txtamount_reward" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                &nbsp;<asp:Button ID="cmdCalulate" runat="server" 
                    Text="Calculate amount of award " CausesValidation="False" />
            </td>
          </tr>
          <tr>
            <td colspan="2" valign="top" bgcolor="#eef1f3"><table width="80%" align="center" cellpadding="3" cellspacing="0">
              <tr>
                <td width="305" valign="top" bgcolor="#6699CC"><strong>Estimated First Year Benefits</strong></td>
                <td width="723" valign="top" bgcolor="#6699CC"><strong>Amount of Award</strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Up to 200,000 Baht <br />
                </strong></td>
                <td valign="top"><asp:Label ID="lblamount1" runat="server" Text="10% of estimated benefits"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top"><strong>&gt; 200,000 - 1,000,000 Baht</strong></td>
                <td valign="top"><asp:Label ID="lblamount2" runat="server" Text="Baht 20,000 for the first Baht 200,000 +3% of estimated benefits over Baht 200,000"></asp:Label></td>
              </tr>
              <tr>
                <td valign="top"><strong>&gt; 1,000,000 Baht </strong></td>
                <td valign="top"><asp:Label ID="lblamount3" runat="server" Text="Baht 44,000 for the first Baht 1,000,000 +1% of estimated benefits more Baht 1,000,000"></asp:Label></td>
              </tr>
            </table></td>
            </tr>
          <tr>
            <td colspan="2" valign="top" bgcolor="#eef1f3"><strong>Reward</strong></td>
            </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Reward Summary</strong></td>
            <td valign="top">&nbsp;</td>
          </tr>
             <tr>
                  <td bgcolor="#eef1f3" valign="top">
                      Reward Promotion Category</td>
                  <td valign="top">
                      <asp:DropDownList ID="txtssip_category" runat="server" DataValueField="ssip_cat_id" DataTextField="cat_name" >
                      </asp:DropDownList>
                  </td>
              </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3">- On-The-Spot</td>
            <td valign="top">
                <input type="text" name="txtonthespot" id="txtonthespot" style="width:150px" runat="server" /> 
                Coupon&nbsp;
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                    ControlToValidate="txtonthespot" Display="Dynamic" 
                    ErrorMessage="Please Enter Only Numbers" 
                    ValidationExpression="^[0-9]*[.]?[0-9]+$"></asp:RegularExpressionValidator>
              </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"> - Staff Recognition Point</td>
            <td valign="top">
             <asp:TextBox ID="txtsrppoint" runat="server" Width="150px" AutoPostBack="True"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        runat="server" ControlToValidate="txtsrppoint" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
               X Bonus
               
                <asp:TextBox ID="txtsrp_bonus" runat="server" Width="150px" AutoPostBack="True"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        runat="server" ControlToValidate="txtsrp_bonus" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
              =
              <asp:TextBox ID="txtsrp_total_point" runat="server" Width="150px" 
                    BackColor="#FFFF99"></asp:TextBox></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3" class="style2">- Cash</td>
            <td valign="top" class="style2"><input type="text" name="txtcash" id="txtcash" style="width: 150px" runat="server" /> 
              Baht
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                    ControlToValidate="txtcash" Display="Dynamic" 
                    ErrorMessage="Please Enter Only Numbers" 
                    ValidationExpression="^[0-9]*[.]?[0-9]+$"></asp:RegularExpressionValidator>
              </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Awarding Date</strong></td>
            <td valign="top">  <asp:TextBox ID="txtaward_date" runat="server" Width="80px" BackColor="#EEEEEE"></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender2"
                                TargetControlID="txtaward_date" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtaward_date" PopupButtonID="Image2">

                              </asp:CalendarExtender>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
                &nbsp;วันที่สรุปผลรางวัล</td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Reward Recieve Date</strong></td>
            <td valign="top"> <asp:TextBox ID="txtreceive_date" runat="server" Width="80px" BackColor="#EEEEEE"></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender3"
                                TargetControlID="txtreceive_date" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtreceive_date" PopupButtonID="Image3">

                              </asp:CalendarExtender>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /> วันที่พนักงานรับรางวัล</td>
          </tr>
          <!--
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Individual Rewarding Summary</strong></td>
            <td valign="top"><table width="100%" cellspacing="0" cellpadding="3">
              <tr>
                <td width="312" valign="top" bgcolor="#FFFFCC"><strong>Value of Benefit</strong></td>
                <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Points</strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>
                 <asp:RadioButton ID="txtb1" runat="server" Text=" Trophy" GroupName="benefit" />
                  </strong></td>
                <td valign="top">&lt; 30           
                           </td>
              </tr>
              <tr>
                <td valign="top"><strong>
                 <asp:RadioButton ID="txtb2" runat="server" Text=" Moderate" GroupName="benefit" />
                  <br />
                </strong></td>
                <td valign="top">30 - 40</td>
              </tr>
              <tr>
                <td valign="top"><strong>
                  <asp:RadioButton ID="txtb3" runat="server" Text=" Substantial" GroupName="benefit" />
                   </strong></td>
                <td valign="top">45 - 60</td>
              </tr>
              <tr>
                <td valign="top"><strong>
                  <asp:RadioButton ID="txtb4" runat="server" Text=" High" GroupName="benefit" />
                   </strong></td>
                <td valign="top">65 - 80</td>
              </tr>
              <tr>
                <td valign="top"><strong>
                 <asp:RadioButton ID="txtb5" runat="server" Text=" Exceptional" GroupName="benefit" />
                   </strong></td>
                <td valign="top">85 - 100</td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Intangible Benefits Award Scale</strong></td>
            <td valign="top"><table width="100%" cellspacing="0" cellpadding="3">
              <tr>
                <td width="312" valign="top" bgcolor="#FFFFCC"><strong>Value of Benefit</strong></td>
                <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Department</strong></td>
                <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Division</strong></td>
                <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Indivision</strong></td>
                <td width="492" valign="top" bgcolor="#FFFFCC"><strong>Hospital Wide</strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#FFFFCC"><strong>                  Moderate<br />
                </strong></td>
                <td valign="top"><asp:RadioButton ID="txtsc1" runat="server" Text=" 1,000" GroupName="scale" /></td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc5" runat="server" GroupName="scale" Text=" 2,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc9" runat="server" GroupName="scale" Text=" 3,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc13" runat="server" GroupName="scale" Text=" 4,000" />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#FFFFCC"><strong>                  Substantial </strong></td>
                <td valign="top"><asp:RadioButton ID="txtsc2" runat="server" Text=" 2,000" GroupName="scale" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc6" runat="server" GroupName="scale" Text=" 3,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc10" runat="server" GroupName="scale" Text=" 4,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc14" runat="server" GroupName="scale" Text=" 5,000" />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#FFFFCC"><strong>                  High </strong></td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc3" runat="server" GroupName="scale" Text=" 3,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc7" runat="server" GroupName="scale" Text=" 4,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc11" runat="server" GroupName="scale" Text=" 5,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc15" runat="server" GroupName="scale" Text=" 6,000" />
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#FFFFCC"><strong>                  Exceptional </strong></td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc4" runat="server" GroupName="scale" Text=" 4,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc8" runat="server" GroupName="scale" Text=" 5,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc12" runat="server" GroupName="scale" Text=" 6,000" />
                  </td>
                <td valign="top">
                    <asp:RadioButton ID="txtsc16" runat="server" GroupName="scale" Text=" 10,000" />
                  </td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>Tangible Benefits Award Scale</strong></td>
            <td valign="top"><table width="100%" cellspacing="0" cellpadding="3">
              <tr>
                <td width="117" valign="top" bgcolor="#FFFFCC"><strong>Estimated First Year Benefits</strong></td>
                <td width="706" valign="top" bgcolor="#FFFFCC"><strong>Amount of Award</strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Up to 200,000 Baht <br />
                </strong></td>
                <td valign="top"><asp:TextBox ID="txtscale1" runat="server" Width="180px"></asp:TextBox>
                  10% of estimated benefits</td>
              </tr>
              <tr>
                <td valign="top"><strong>&gt; 200,000 - <br />
                  1,000,000
                  Baht </strong></td>
                <td valign="top"><asp:TextBox ID="txtscale2" runat="server" Width="180px"></asp:TextBox>
                  Baht 20,000 for the first Baht 200,000 +3% of estimated benefits over Baht 200,000</td>
              </tr>
              <tr>
                <td valign="top"><strong>&gt; 1,000,000 Baht </strong></td>
                <td valign="top"><asp:TextBox ID="txtscale3" runat="server" Width="180px"></asp:TextBox>
                  Baht 44,000 for the first Baht 1,000,000 +1% of estimated benefits more Baht 1,000,000</td>
              </tr>

            </table></td>
          </tr>
          -->
            <tr>
                <td bgcolor="#eef1f3" valign="top">
                    <strong>Remark</strong></td>
                <td valign="top">
                    <asp:TextBox ID="txtreward_remark" runat="server" Rows="3" TextMode="MultiLine" 
                        Width="80%"></asp:TextBox>
                </td>
            </tr>
        </table>
        </asp:Panel>
        </ContentTemplate>
         </asp:UpdatePanel>
        <br />
      </div>
  
      
<div class="tabbertab" id="tabDeptManager" runat="server">
        <h2>Sup/Mgr/Dept Mgr/DD Review 
            <asp:Label ID="lblNumSup" runat="server" Text="Label"></asp:Label></h2>
           <asp:Panel ID="panelManager1" runat="server" BorderStyle="None">
      
            <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td width="1022" colspan="5">
                      <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
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
                                  <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Edit Comment" Visible="false" />                                
                                  <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/bt_delete.gif" OnCommand="onDeleteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' OnClientClick="return confirm('Are you you want to delete ?');" />                                
                                  <asp:ImageButton ID="cmdMgrReview" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Review Score"  />                                
</div></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label>  
                                  &nbsp;<asp:LinkButton runat="server" ID="myExpand1">more detail.. </asp:LinkButton>                              </td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                          
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label>
                           
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

    CollapsedText="Add new review"
    ExpandedText="Hide Details" 
  
    ExpandDirection="Vertical" ExpandControlID="myExpand1" CollapseControlID="myExpand1" 
                          SuppressPostBack="True" />
                       <asp:Panel ID="Panel1" runat="server">
                                  
  
  <table width="98%" cellspacing="1" cellpadding="2">
          <tr>
            <td valign="top" bgcolor="#eef1f3">
            
         </td>
          </tr>
       
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>1. ข้อเสนอแนะและนวัตกรรมนี้ เกี่ยวข้องกับหน่วยงานของท่านใช่หรือไม่
                  </strong>
              </td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                     <asp:Label ID="lblAns1" runat="server" Text='<%# Eval("q1_type")%>'></asp:Label>
                    </td>
                <td>
                    <asp:Label ID="lblDetail1" runat="server" Text='<%# Eval("q1_reason")%>'></asp:Label>
                  </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>2. ข้อมูลประกอบข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายให้ท่านเข้าใจได้</strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top"><asp:Label ID="lblAns2" runat="server" Text='<%# Eval("q2_type")%>'></asp:Label></td>
                <td><asp:Label ID="lblDetail2" runat="server" Text='<%# Eval("q2_reason")%>'></asp:Label></td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>3. ข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายถึงสถานการณ์ วิธีการ ขั้นตอนการทำงานที่เป็นอยู่จริงในอนาคต  พร้อมทั้งสามารถนำมาใช้ได้จริงหรือไม่ </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top"><asp:Label ID="lblAns3" runat="server" Text='<%# Eval("q3_type")%>'></asp:Label></td>
                <td><asp:Label ID="lblDetail3" runat="server" Text='<%# Eval("q3_reason")%>'></asp:Label></td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>4. ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ เคยได้รับการเสนอหรือได้รับการพิจารณาจากผู้บริหารโรงพยาบาลมาก่อนหรือไม่
              
            </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top"><asp:Label ID="lblAns4" runat="server" Text='<%# Eval("q4_type")%>'></asp:Label></td>
                <td><asp:Label ID="lblDetail4" runat="server" Text='<%# Eval("q4_reason")%>'></asp:Label></td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>5. หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้มีประโยชน์ และสามารถนำมาปฏิบัติได้จริง หน่วยงานของท่านมีนโยบายจะทำหรือไม่</strong></td>
          </tr>
          <tr>
            <td valign="top">
           <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top"><asp:Label ID="lblAns5" runat="server" Text='<%# Eval("q5_type")%>'></asp:Label></td>
                <td><asp:Label ID="lblDetail5" runat="server" Text='<%# Eval("q5_reason")%>'></asp:Label></td>
                </tr>
            </table>
            </td>
          </tr>
        
          <tr>
            <td valign="top"><table width="98%" align="center" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                <tr bgcolor="#eef1f3">
                  <td width="30" valign="top" ><strong>5.1</strong></td>
                  <td width="865" valign="top" ><strong>กรณีสามารถนำไปปฏิบัติได้จริง จำเป็นต้องกำหนดนโยบายหรือระเบียบข้อบังคับหรือไม่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >
                 <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top"><asp:Label ID="lblAns6" runat="server" Text='<%# Eval("q6_type")%>'></asp:Label></td>
                <td><asp:Label ID="lblDetail6" runat="server" Text='<%# Eval("q6_reason")%>'></asp:Label></td>
                </tr>
            </table>
                  
                  </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.2</strong></td>
                  <td valign="top" ><strong>หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำมาได้จริง จะต้องใช้งบประมาณเท่าไหร่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >จำนวนเงิน
                    <asp:Label ID="lblBudgetNum" runat="server" Text='<%# Eval("q5_budget")%>'></asp:Label> 
บาท <br />
                  
                    </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.3</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำไปปฏิบัติและช่วยให้เกิดการเปลี่ยนแปลงที่ดีขึ้นได้ ในระดับใด</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><asp:Label ID="lblDept" runat="server" Text=''></asp:Label></td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.4</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมที่พนักงานเสนอมานี้ หากนำมาปฏิบัติจะเป็นประโยชน์กับโรงพยาบาลในด้านใดบ้าง </strong></td>
                </tr>
                <tr >
                  <td valign="top">&nbsp;</td>
                  <td valign="top" >
                <asp:Label ID="lblBenefit" runat="server" Text='' ></asp:Label>
                  </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.5</strong></td>
                  <td valign="top" ><strong>กรณีที่ท่านประเมินว่า ข้อเสนอแนะและนวัตกรรมนี้สามารถนำไปใช้ได้จริง ท่านมีแผนการเพื่อดำเนินการนำไปใช้อย่างไร </strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><asp:Label ID="lblDetail8" runat="server" Text='<%# Eval("plan_detail")%>'></asp:Label></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>6. ข้อแนะนำอื่น ๆ</strong></td>
          </tr>
          <tr>
            <td valign="top"><asp:Label ID="lblDetail9" runat="server" Text='<%# Eval("other_detail")%>'></asp:Label></td>
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
                      </asp:GridView>   <div align="right" id="linkDept" runat="server">
               <a id="showDept" href="javascript:;" onclick="$('#showDept').hide();$('#hideDept').show();$('#deptContent').show();"><asp:Image ID="Image6" runat="server" ImageUrl="~/images/plus.gif" /> Add new review</a>
                <a id="hideDept" href="javascript:;" style="display:none"  onclick="$('#showDept').show();$('#hideDept').hide();$('#deptContent').hide();"><asp:Image ID="Image7" runat="server" ImageUrl="~/images/minus.gif" /> Hide</a>      
      </div>
                  <br />
                   
                    <asp:Panel ID="panelAddComment" runat="server" Visible="true">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <ContentTemplate>
                     <span id="deptContent"  >
                     <div align="right">
                                <asp:Button ID="cmdSaveDeptReview2" runat="server" Width="200px" Text="Save Review" OnClientClick="return confirm('Are you sure you want add new review?');" />
                                &nbsp;</div>
                    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"><strong>Review By</strong></td>
                  <td bgcolor="#DBE1E6"></td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Subject</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td><asp:TextBox ID="txtadd_subject" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3"><strong>Detail</strong></td>
                        <td bgcolor="#eef1f3"><textarea name="txtcomment_detail" id="txtcomment_detail" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3" colspan="2">
                        <asp:Panel ID="panelManagerReview" runat="server">
                           <table width="98%" cellspacing="1" cellpadding="2">
          <tr>
            <td valign="top" bgcolor="#eef1f3">
            
         </td>
          </tr>
       
          <tr>
              <td bgcolor="#eef1f3" valign="top">
                  <strong>1. ข้อเสนอแนะและนวัตกรรมนี้ เกี่ยวข้องกับหน่วยงานของท่านใช่หรือไม่
                  </strong>
              </td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer1" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason1" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>2. ข้อมูลประกอบข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายให้ท่านเข้าใจได้</strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer2" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason2" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>3. ข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายถึงสถานการณ์ วิธีการ ขั้นตอนการทำงานที่เป็นอยู่จริงในอนาคต  พร้อมทั้งสามารถนำมาใช้ได้จริงหรือไม่ </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer3" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason3" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>4. ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ เคยได้รับการเสนอหรือได้รับการพิจารณาจากผู้บริหารโรงพยาบาลมาก่อนหรือไม่
              <label for="select20"></label>
            </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer4" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason4" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>5. หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้มีประโยชน์ และสามารถนำมาปฏิบัติได้จริง หน่วยงานของท่านมีนโยบายจะทำหรือไม่</strong></td>
          </tr>
          <tr>
            <td valign="top">
           <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer5" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason5" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
        
          <tr>
            <td valign="top"><table width="98%" align="center" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                <tr bgcolor="#eef1f3">
                  <td width="30" valign="top" ><strong>5.1</strong></td>
                  <td width="865" valign="top" ><strong>กรณีสามารถนำไปปฏิบัติได้จริง จำเป็นต้องกำหนดนโยบายหรือระเบียบข้อบังคับหรือไม่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >
                 <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:ListBox ID="txtanswer51" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason51" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
                  
                  </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.2</strong></td>
                  <td valign="top" ><strong>หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำมาได้จริง จะต้องใช้งบประมาณเท่าไหร่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >จำนวนเงิน
                    <input type="text" name="txtbudget_num" id="txtbudget_num" style="width: 180px" runat="server" />
                    บาท <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                        runat="server" ControlToValidate="txtbudget_num"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.3</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำไปปฏิบัติและช่วยให้เกิดการเปลี่ยนแปลงที่ดีขึ้นได้ ในแผนกใด (เลือกได้มากกว่า 1 แผนกโดยกดปุ่ม Shift หรือ Ctrl )</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="200" valign="top">
                            <asp:ListBox ID="lblDeptAll" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="250px" SelectionMode="Multiple" Rows="10"></asp:ListBox>
&nbsp;</td>
                        <td width="60" valign="top" style="text-align:center">
                            <asp:Button ID="cmdAddDept" runat="server" Text=" >> " /><br />
                            <asp:Button ID="cmdRemoveDept" runat="server" Text=" << " />
                          </td>
                          <td valign="top" >
                              <asp:ListBox ID="lblDeptSelect" runat="server" DataTextField="dept_name_en" 
                                  DataValueField="dept_id" Width="250px" SelectionMode="Multiple" Rows="10"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.4</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมที่พนักงานเสนอมานี้ หากนำมาปฏิบัติจะเป็นประโยชน์กับโรงพยาบาลในด้านใดบ้าง </strong></td>
                </tr>
                <tr >
                  <td valign="top">&nbsp;</td>
                  <td valign="top" ><table width="100%" cellspacing="3" cellpadding="2">
                      <tr>
                        <td width="312" valign="top"><input type="checkbox" name="chk_benefit1" id="chk_benefit1" runat="server" />
                          เพิ่มความพึงพอใจให้แก่ลูกค้า<br /></td>
                        <td width="492" valign="top"><input type="checkbox" name="chk_benefit2" id="chk_benefit2" runat="server" />
                          ประหยัดค่าใช้จ่ายเป็นเงิน
                          <input type="text" name="txtbenefit_num" id="txtbenefit_num" style="width: 180px" runat="server" />
                          บาท <br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
                        runat="server" ControlToValidate="txtbenefit_num"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" Display="Dynamic" ></asp:RegularExpressionValidator> 
                           </td>
                      </tr>
                      <tr>
                        <td valign="top"><input type="checkbox" name="chk_benefit3" id="chk_benefit3" runat="server" />
                          เพิ่มรายได้หรือกำไรให้แก่องค์กร </td>
                        <td valign="top"><input type="checkbox" name="chk_benefit4" id="chk_benefit4" runat="server" />
                          เพิ่มประสิทธิภาพในการทำงาน </td>
                      </tr>
                      <tr>
                        <td valign="top"><input type="checkbox" name="chk_benefit5" id="chk_benefit5" runat="server" />
                          ปรับปรุงคุณภาพของสถานที่ทำงาน</td>
                        <td valign="top"><input type="checkbox" name="chk_benefit6" id="chk_benefit6" runat="server" />
                          อื่นๆ (โปรดระบุ)</td>
                      </tr>
                  </table></td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.5</strong></td>
                  <td valign="top" ><strong>กรณีที่ท่านประเมินว่า ข้อเสนอแนะและนวัตกรรมนี้สามารถนำไปใช้ได้จริง ท่านมีแผนการเพื่อดำเนินการนำไปใช้อย่างไร </strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><textarea name="txtplan" id="txtplan" cols="45" rows="5" 
                          style="width: 850px" runat="server"></textarea></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>6. ข้อแนะนำอื่น ๆ</strong></td>
          </tr>
          <tr>
            <td valign="top"><textarea name="txtother" id="txtother" cols="45" rows="5" 
                    style="width: 850px" runat="server"></textarea></td>
          </tr>

        </table>
        </asp:Panel>
                        </td>
                      </tr>
                        <tr>
                            <td bgcolor="#eef1f3">&nbsp;
                                </td>
                            <td bgcolor="#eef1f3"><div align="right">
                                <asp:Button ID="cmdAddComment" runat="server" Width="200px" Text="Save Review" OnClientClick="return confirm('Are you sure you want add new review?');" />
                                &nbsp;</div></td>
                        </tr>
                    </table>
                    </span>
                      </ContentTemplate>
                     </asp:UpdatePanel>
                    </asp:Panel>
                  
                    
                  </td>
                </tr>
             </table>
            
              </asp:Panel>
        <br />
</div>

<div class="tabbertab" id="tabCommittee" runat="server">
        <h2>Committee Evaluation  <asp:Label ID="lblNumCom" runat="server" Text="Label"></asp:Label></h2>

          <asp:Panel ID="panelCommittee" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <br />
            <asp:Panel runat="server" ID="panel_intangible" >
     <asp:GridView ID="GridCommittee" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None"  GridLines="None" 
                  ShowFooter="True" EmptyDataText="There is no commitee review" CssClass="tdata" CellPadding="2" CellSpacing="1">
                          <Columns>
                            
                                 <asp:TemplateField HeaderText="คณะกรรมการ">
                             
                              <ItemStyle Width="200px"  />
                                  <ItemTemplate>
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>  
                                  <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>  
                                      <asp:Label ID="lblName1" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                                      
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                           
                            
                              <asp:TemplateField HeaderText="1. ประโยชน์">
                              <HeaderStyle ForeColor="#cc3300" />
                              <ItemStyle ForeColor="#cc3300" />
                                  <ItemTemplate>
                                      <asp:Label ID="lbls1" runat="server" Text='<%# Bind("score1") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="2. รูปแบบ">
                                 <HeaderStyle ForeColor="#cc3300" />
                              <ItemStyle ForeColor="#cc3300" />
                                  <ItemTemplate>
                                      <asp:Label ID="lbls2" runat="server" Text='<%# Bind("score2") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="3. ความ<br/>สมบูรณ์">
                                 <HeaderStyle ForeColor="#cc3300" />
                              <ItemStyle ForeColor="#cc3300" />
                                  <ItemTemplate>
                                      <asp:Label ID="lbls3" runat="server" Text='<%# Bind("score3") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="4. การศึกษา">
                                 <HeaderStyle ForeColor="#cc3300" />
                              <ItemStyle ForeColor="#cc3300" />
                                  <ItemTemplate>
                                      <asp:Label ID="lbls4" runat="server" Text='<%# Bind("score4") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="5. ค่าใช้จ่าย">
                                 <HeaderStyle ForeColor="#cc3300" />
                              <ItemStyle ForeColor="#cc3300" />
                                  <ItemTemplate>
                                      <asp:Label ID="lbls5" runat="server" Text='<%# Bind("score5") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                                 <asp:TemplateField HeaderText="1. การเพิ่ม<br/>รายได้">
                                  <HeaderStyle ForeColor="#3333cc"/>
                              <ItemStyle ForeColor="#3333cc" />
                                     <ItemTemplate>
                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("q1_type") %>'></asp:Label>
                                     </ItemTemplate>
                                     
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="2. มูลค่า<br/>เหมาะสม">
                                <HeaderStyle ForeColor="#3333cc"/>
                              <ItemStyle ForeColor="#3333cc" />
                                  <ItemTemplate>
                                      <asp:Label ID="Label2" runat="server" Text='<%# Bind("q2_type") %>'></asp:Label>
                                  </ItemTemplate>
                               
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="3. จำนวน<br/>เงินที่<br/>ประหยัด">
                                <HeaderStyle ForeColor="#3333cc"/>
                              <ItemStyle ForeColor="#3333cc" />
                                  <ItemTemplate>
                                      <asp:Label ID="Label3" runat="server" Text='<%# Bind("amount_save") %>'></asp:Label>
                                  </ItemTemplate>
                                 
                              </asp:TemplateField>
                                 <asp:BoundField HeaderText="Award Scale" 
                                  DataField="scale_name" />
                              <asp:BoundField DataField="result_text" HeaderText="Result" />
                           <asp:TemplateField>
                             <ItemStyle Width="140px" />
                                  <ItemTemplate>
                                        <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/bt_delete.gif" OnCommand="onDeleteCommitteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' OnClientClick="return confirm('Are you sure you want to delete?')" />
                                 <asp:ImageButton ID="cmdMgrReview" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Edit Review Score"  />
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/bt_review.gif" ToolTip="Edit Review Score" Visible="false"  />
                                  </ItemTemplate>
                              
                              </asp:TemplateField>
                            
                          </Columns>
                          <FooterStyle BackColor="#EEF1F3" VerticalAlign="Top" />
                          <HeaderStyle BackColor="#EEF1F3" Font-Bold="True"  />
                          <RowStyle BorderStyle="None" Height="20px" /> <AlternatingRowStyle BackColor="Azure" />
            </asp:GridView>
            </asp:Panel>
  <table width="100%">
          <tr><td width="250"><strong>HRD Effective Award: </strong> </td><td></td></tr>
          <tr>
              <td >
                  - Individual Rewarding Summary              </td>
              <td>
                  <asp:Label ID="lblPersonSum" runat="server" ForeColor="#0033CC" Text="-"></asp:Label>              </td>
          </tr>
          <tr>
              <td >
                  - Intangible Benefits Award</td>
              <td>
                  <asp:Label ID="lblIntangAward" runat="server" ForeColor="#0033CC" Text="-"></asp:Label>              </td>
          </tr>
          <tr>
              <td >
                  - Tangible Benefits Award              </td>
              <td>
                  <asp:Label ID="lblTangAward" runat="server" ForeColor="#0033CC" Text="-"></asp:Label>              </td>
          </tr>
          <tr>
              <td >
                  - Amount of award</td>
              <td>
                  <asp:Label ID="lblAwardAmount" runat="server" ForeColor="#0033CC" Text="-"></asp:Label>
                  &nbsp;Baht</td>
          </tr>
     </table>
   <div align="right" id="linkCommittee" runat="server">
               <a id="showReview" href="javascript:;" onclick="$('#showReview').hide();$('#hideReview').show();$('#reviewContent').show();"><asp:Image ID="Image4" runat="server" ImageUrl="~/images/plus.gif" /> Add new review</a>
                <a id="hideReview" href="javascript:;" style="display:none"  onclick="$('#showReview').show();$('#hideReview').hide();$('#reviewContent').hide();"><asp:Image ID="Image5" runat="server" ImageUrl="~/images/minus.gif" /> Hide</a>      
      </div>
                          <br />
            
              
        <span id="reviewContent"  >
        <asp:Panel ID="panelAddCommiteeComment" runat="server" Visible = "false">
        <div align="right">
      <asp:Button ID="cmdSaveReview2" runat="server" Text="Save Review"  Width="200px"
                   Font-Bold="True"  OnClientClick="return validateCommittee()"  />
                   </div>
        <table width="98%" align="center" cellpadding="2" cellspacing="1">
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>A. การพิจารณาเรื่องประโยชน์ที่จับต้องได้ (Tangible)</strong></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>1. ข้าพเจ้าเห็นด้วยว่าการนำข้อเสนอแนะ / นวัตกรรมนี้ไปใช้จะช่วยให้ประหยัดค่าใช้จ่ายหรือเพิ่มรายได้
      
    </strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><label for="radio20"></label></td>
        <td width="215" valign="top"><strong>
                      <asp:ListBox ID="txtcom_ans1" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    &nbsp;</strong></td>
        <td width="23"><label for="label5"></label></td>
        <td valign="top" width="100">กรุณาระบุเหตุผล          </td>
        <td valign="top">                      <asp:TextBox ID="txtcom_reason1" runat="server" Width="90%" 
                          TextMode="MultiLine" Rows="3"></asp:TextBox>
                   &nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr bgcolor="#eef1f3">
    <td valign="top"><strong>2. ข้าพเจ้าเห็นด้วยกับมูลค่าโดยประมาณที่จะเพิ่มขึ้นหรือที่จะประหยัดได้</strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><label for="radio20"></label></td>
        <td width="215" valign="top"><strong>
                      <asp:ListBox ID="txtcom_ans2" Rows="3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:ListBox>
                    &nbsp;</strong></td>
        <td width="23"></td>
        <td valign="top" width="100">กรุณาระบุเหตุผล </td>
        <td valign="top">
                      <asp:TextBox ID="txtcom_reason2" runat="server" Width="90%" 
                          TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
      </tr>
    </table></td>
  </tr>
           
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>การคำนวณการประหยัดค่าใช้จ่ายในปีแรก:</strong></td>
  </tr>
  <tr><td>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="25%"><span style="font-weight: bold">
           <asp:TextBox ID="txtamount_old" runat="server" Width="180px" ></asp:TextBox>
          -</span></td>
        <td width="25%"><span style="font-weight: bold">
          <asp:TextBox ID="txtamount_new" runat="server" Width="180px" ></asp:TextBox>
            </span></td>
             <td width="25%">= <asp:TextBox ID="txtamount_save" runat="server" Width="180px" 
                     BackColor="#FFFF99"></asp:TextBox>
            <strong>บาท</strong></td>
        <td width="25%">
            <asp:Button ID="cmdCal11" runat="server" Text="Calculate" /> <asp:TextBox ID="txtamount_cal" runat="server" Width="80px" Visible="False"></asp:TextBox>
          </td>
       
      </tr>
      <tr>
        <td><strong>ค่าใช้จ่ายต่อปี (วิธีเก่า)</strong></td>
        <td><strong>ค่าใช้จ่ายต่อปี  (วิธีที่นำเสนอใหม่)</strong></td>
        <td><strong>จำนวนที่คาดว่าจะประหยัด<!--ค่าใช้จ่ายในการนำไปใช้ - รายได้ --></strong></td>
        <td><strong></strong></td>
      </tr>
    </table></td></tr>
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>B. การพิจารณาประโยชน์ที่จับต้องไม่ได้ (Intangible)</strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="98%" align="center" cellpadding="2" cellspacing="1">
      <tr bgcolor="#eef1f3">
        <td valign="top" bgcolor="#eef1f3" ><strong>1. ข้อเสนอแนะและนวัตกรรมนี้ สามารถนำไปใช้ประโยชน์ในด้านใด</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList  ID="txtscorename1" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" >
            </asp:DropDownList  >
        
        </strong></td>
        <td valign="top" >  <asp:textbox ID="txtscore1" runat="server" text="0" Width="180px">
          </asp:textbox>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>2. รูปแบบข้อเสนอแนะและนวัตกรรมนี้ </strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList ID="txtscorename2" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" >
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" >  <asp:textbox ID="txtscore2" runat="server" text="0" Width="180px">
          </asp:textbox></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>3.ความสมบูรณ์ของข้อเสนอแนะ</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList ID="txtscorename3" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" 
                style="height: 22px">
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" >
            <asp:textbox ID="txtscore3" runat="server" text="0" Width="180px">
          </asp:textbox>
        </td>
         
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>4. การศึกษาเพื่อสนับสนุนในการนำเสนอนวัตกรรมและข้อเสนอแนะ</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr >
        <td valign="top"><strong>
            <asp:DropDownList ID="txtscorename4" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" >
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" >  <asp:textbox ID="txtscore4" runat="server" text="0" Width="180px">
          </asp:textbox></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" bgcolor="#eef1f3" ><strong>5. ค่าใช้จ่ายในการนำมาใช้</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" class="style1" ><strong>
            <asp:DropDownList ID="txtscorename5" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px"  >
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" class="style1" >  <asp:textbox ID="txtscore5" runat="server" text="0" Width="180px">
          </asp:textbox></td>
      </tr>
        <tr>
            <td  valign="top">
                <strong>Intangible Benefits Award Scale</strong>&nbsp;&nbsp;</td>
            <td  valign="top">
                <asp:DropDownList ID="txtadd_award_scale" runat="server"
                    Width="150px">
                    <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                    <asp:ListItem Value="1">Improve Environment and Safety</asp:ListItem>
                    <asp:ListItem Value="2">Increase Efficiency</asp:ListItem>
                    <asp:ListItem Value="3">Customer Service</asp:ListItem>
                    <asp:ListItem Value="4">Healthcare</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
      <tr>
        <td valign="top" bgcolor="#B9C6CE" ><strong>Total</strong></td>
        <td valign="top" bgcolor="#B9C6CE" >
           
            <asp:Label
                ID="txtsum" runat="server" Text="0" Font-Bold="True" Font-Size="16px"></asp:Label> </td>
      </tr>
        <tr>
            <td bgcolor="#B9C6CE" valign="top">
                <strong>Result</strong></td>
            <td bgcolor="#B9C6CE" valign="top">
                <asp:Label ID="Label6" runat="server" Text="-"></asp:Label>
            </td>
        </tr>
        <tr>
            <td bgcolor="#B9C6CE" valign="top">
                <strong>Reference :</strong>&nbsp;Moderate 34 - 44, Substantial 45 - 64, High 65 -84, 
                Exceptional 85 - 100</td>
            <td bgcolor="#B9C6CE" valign="top">&nbsp;
                </td>
        </tr>
    </table><div align="right"><asp:Button ID="cmdCommitteComment" runat="server" Text="Save Review"  Width="200px"
                   Font-Bold="True" OnClientClick="return validateCommittee();" />
                <!--
                 <asp:ImageButton ID="cmdCommitteComment1" ImageUrl="../images/comment_add.png" 
                                ToolTip="Add Comment" runat="server" OnClientClick="return addComment()" 
                                style="width: 16px; height: 16px;" />
                                --></div>
</td>
  </tr>
</table>
        </asp:Panel>
        </span>
        </ContentTemplate>
              </asp:UpdatePanel>
              &nbsp;</asp:Panel>
             
        <br />
</div>
 <div class="tabbertab" id="tab_update" runat="server">
  <h2>Additional Comment <asp:Label ID="lblNumComment" runat="server" Text="Label"></asp:Label></h2>
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
   <div class="tabbertab" id="div_activity" runat="server">
  <h2>Activities Record<asp:Label ID="Label5" runat="server" Text=""></asp:Label></h2>
  <div style="width:100%">
  <asp:Button ID="cmdNew" runat="server"  Width="150px" Text="New Activity Record" />
  </div>
   <asp:GridView ID="GridActivity" runat="server" 
              AutoGenerateColumns="False"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="record_id" 
              AllowPaging="True" PageSize="20" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
                EnableModelValidation="True">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                  <asp:TemplateField HeaderText="No.">
                      <ItemTemplate>
                           <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                                 
                  <asp:TemplateField HeaderText="Activity Date" >
              
                      <ItemTemplate>
                       <a href="ssip_activity_detail.aspx?mode=edit&id=<%# Eval("record_id") %>&ssip_id=<% response.write(id) %>">
                          <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("activity_date_start_ts"))%>'></asp:Label>
                             <asp:Label ID="lblStart1" runat="server" Text='<%# ConvertTSTo(Eval("activity_date_start_ts"),"hour").PadLeft(2,"0") %>' />:
                     <asp:Label ID="lblStart11" runat="server" Text='<%# ConvertTSTo(Eval("activity_date_start_ts"),"min").PadLeft(2,"0") %>' />

                      -
                        <asp:Label ID="lblEnd1" runat="server" Text='<%# ConvertTSTo(Eval("activity_date_end_ts"),"hour").PadLeft(2,"0") %>' />:
                     <asp:Label ID="lblEnd11" runat="server" Text='<%# ConvertTSTo(Eval("activity_date_end_ts"),"min").PadLeft(2,"0") %>' />
                     </a>
                          <asp:Label ID="lblDateStart" runat="server" Text='<%# Eval("activity_date_start_ts")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lblDateEnd" runat="server" Text='<%# Eval("activity_date_end_ts")%>' Visible="false"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Staff Name">
                      <ItemTemplate>
                        <a href="ssip_activity_detail.aspx?mode=edit&id=<%# Eval("record_id") %>&ssip_id=<% response.write(id) %>">
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("user_fullname") %>'></asp:Label><br />
                        </a>
                      </ItemTemplate>
                    
                  </asp:TemplateField>
              
                  <asp:TemplateField HeaderText="Hour">
                      <ItemTemplate>
                          <asp:Label ID="lblHour" runat="server"></asp:Label>
                      </ItemTemplate>
                     
                  </asp:TemplateField>
              
                  <asp:BoundField DataField="activity_name" HeaderText="Event" />
              
                  <asp:BoundField DataField="room_name" HeaderText="Room" />

                  <asp:TemplateField HeaderText="Activity Detail">
                      <ItemTemplate>
                    
                          <asp:Label ID="Label2" runat="server" Text='<%#Bind ("activity_detail")%>'></asp:Label>
                         
                      </ItemTemplate>
                     
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                                 
                  <asp:BoundField DataField="rec_emp_name" HeaderText="Record by" />
              
                  <asp:TemplateField HeaderText="">
                    
                      <ItemTemplate>
                        <asp:Label ID="lblPK" runat="server" Text='<%# bind("record_id") %>' Visible="false"></asp:Label>

                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                   
                  <ItemStyle ForeColor="Red" />
                  <ItemTemplate>
                                     
                      <asp:ImageButton ID="linkDelete" runat="server" CommandName="cancelCommand" 
                    CommandArgument="<%# Bind('record_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/bin.png" ToolTip="Delete" />
                     
                 
                  </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
          <br />
          <table width="100%">
          <tr>
          <td width="100">Total</td>
          <td>
              <asp:Label ID="lblPersonNum" runat="server" Text=""></asp:Label> &nbsp;Persons
              </td>
          </tr>
          <tr>
          <td width="100">&nbsp;</td>
          <td>
               <asp:Label ID="lblHourNum" runat="server" Text=""></asp:Label> &nbsp;Hour&nbsp;</td>
          </tr>
          <tr>
          <td width="100">Average</td>
          <td>
               <asp:Label ID="lblAverage" runat="server" Text=""></asp:Label> &nbsp;Hour/Person</td>
          </tr>
          </table>
          <br />
  </div>
</div><br />
      <div align="right">
        <div align="right"> 
            <asp:Button ID="cmdDraft2" runat="server" Text="Save as Draft" OnCommand="onSave" CommandArgument="1" />
                 <asp:Button ID="cmdSubmit2" runat="server" Text="Submit Suggestion" 
                OnCommand="onSave" CommandArgument="2" 
                OnClientClick="return confirm('Are you sure you want to submit your SSIP?');" 
                Font-Bold="True"  />
  &nbsp;</div>
      </div>
      </div>
</asp:Content>

