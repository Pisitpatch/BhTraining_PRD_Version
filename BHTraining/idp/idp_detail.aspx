<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_detail.aspx.vb" Inherits="idp_idp_detail" %>
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

  'cookie':"tabber", /* Name to use for the cookie */

  'onLoad': function(argsObj)
  {
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

  'onClick':function(argsObj)
  {
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
    document.cookie= name + "=" + escape(value) +
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
    function openPopup(flag, popupType) {
        if (flag.checked) {
            is_sms = 1;
        } else {
            is_sms = 0;
        }

        window.open('../incident/popup_recepient.aspx?modules=ladder&popupType=' + popupType, '', 'alwaysRaised,scrollbars =no,status=no,width=800,height=620');
    }

    function editDetail(cid) {
        loadPopup(1);
        my_window = window.open('popup_editcomment.aspx?id=<%response.write(id) %>&cid=' + cid, 'popupFile', 'alwaysRaised,scrollbars =yes,status=yes,width=850,height=600');
    }

    function openDetail(id,empcode) {
        loadPopup(1);
        my_window = window.open('popup_idp_file.aspx?id=<%response.write(id) %>&fid=' + id + "&empcode=" + empcode + "&flag=<%response.write(flag) %>" , 'popupFile', 'alwaysRaised,scrollbars =yes,width=800,height=750');
    }

    function validateIDP() {
        if ($("#ctl00_ContentPlaceHolder1_txtadd_cat1").val() == "0") {
            alert("Please choose Category !");
            $("#ctl00_ContentPlaceHolder1_txtadd_cat1").focus();
            return false;
        }

        if (($("#ctl00_ContentPlaceHolder1_txtadd_topic1").val() == "") || ($("#ctl00_ContentPlaceHolder1_txtfind_topic").val() == "0")) {
            alert("Please enter topic !");
            $("#ctl00_ContentPlaceHolder1_txtadd_topic1").focus();
            return false;
        }

        if (($("#ctl00_ContentPlaceHolder1_txtadd_expect1").val() == "") || ($("#ctl00_ContentPlaceHolder1_txtfind_expect").val() == "0")) {
            alert("Please enter expect outcome !");
            $("#ctl00_ContentPlaceHolder1_txtadd_expect1").focus();
            return false;
        }
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

        if ($("#ctl00_ContentPlaceHolder1_txtacknowedge_status").val() == "") {
            alert("Please choose Acknowledge !");
            $("#ctl00_ContentPlaceHolder1_txtacknowedge_status").focus();
            return false;
        }
    }

    function onchangeStatus(me){
        //alert(me.value);
        if (me.value == 4){
          if (confirm("Are you sure you want to change status to Cancelled ?")){
            return true;
          }else{
            me.value = 0;
            return false;
          }
        }
    }

    $(document).ready(function () { 
      checkSession(<%response.write(session("emp_code").toString) %> , '<%response.write(viewtype) %>'); // Check session every 1 sec.
    });
</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />
    <style type="text/css">
        .style2
        {
            height: 30px;
        }
        .style3
        {
            height: 29px;
        }
        .style4
        {
            height: 22px;
        }
        .style6
        {
            width: 246px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
  <table width="100%" cellpadding="0" cellspacing="0" >
    <tr>
       
      <td><div id="header"><img src="../images/menu_03.png" width="32" height="32" /> &nbsp;
       <asp:Label ID="lblHeader" runat="server" Text=" Individual Development Plan"></asp:Label>
     
          <asp:Label ID="lblStatus" runat="server" ForeColor="#333399"></asp:Label>
      <asp:HiddenField ID="txtstatusid" runat="server" /></div></td>
      <td>
      <div align="right"  style="margin-right: 10px;" id="div_status" runat="server">
          <asp:Button ID="cmdThai" runat="server" Text="Thai" />
      <asp:Button ID="cmdEng" runat="server" Text="Eng" />
       Status
          <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="status_name" DataValueField="idp_status_id" Font-Bold="True" 
                    BackColor="Aqua" ForeColor="Blue" Enabled="False" Width="285px"> </asp:DropDownList>
          <asp:Button ID="cmdUpdateStatus" runat="server" Text="Update Status" />
           <asp:Button ID="cmdPreview" runat="server" Text="Print Preview" Visible="false" />
          </div></td>
  </tr></table>
      <div id="data">
          <div align="right">
            
              <asp:Panel ID="panel_toolbar" runat="server">
              <asp:Label ID="lblFormName" runat="server" Text="Form Name : " Font-Bold="true"></asp:Label>
              <asp:DropDownList ID="txtform" runat="server" DataValueField="template_id" 
                  DataTextField="template_title" AutoPostBack="True">
              </asp:DropDownList>

              <asp:Button ID="cmdNewIDP" runat="server" Text="New" Width="150px" 
                  Visible="False" />
           
            &nbsp;
            <asp:Button ID="cmdRenewIDP" runat="server" Text="Renew" Width="150px" 
                  Visible="False" />
            
            &nbsp;
              <asp:Button ID="cmdSaveDraft1" runat="server" Text="Save as Draft" Width="150px" OnCommand="onSave" 
            CommandArgument="1" />
           
            &nbsp;
             <asp:Button ID="cmdSubmit1" runat="server" Text="Submit" Width="150px" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True" OnClientClick="return confirm('Are you sure you want to submit ?')"  />
            &nbsp;&nbsp;
          </asp:Panel>
            <asp:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server" TargetControlID="panel_toolbar"  VerticalSide="Top" VerticalOffset="10" HorizontalSide="Right">
              </asp:AlwaysVisibleControlExtender>
            &nbsp;&nbsp;
          </div>
        <div class="tabber" id="mytabber2">
            <div class="tabbertab">
              <h2>Staff Information</h2>
              <table width="100%" cellspacing="1" cellpadding="2" >
                <tr>
                  <td valign="top">
                      <asp:Label ID="lblyear" runat="server" Text="Year"></asp:Label>
                    </td>
                  <td>
                  <asp:DropDownList ID="txtmonth" runat="server" AutoPostBack="True">
                  <asp:ListItem Value="4">April</asp:ListItem>
                  <asp:ListItem Value="10">October</asp:ListItem>
                      </asp:DropDownList>
                      <asp:DropDownList ID="txtyear" runat="server" AutoPostBack="True">
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                  <td valign="top"><span class="theader"><strong>Reference No.</strong></span></td>
                  <td><strong><asp:Label ID="lblIDP_NO" runat="server" Text=""></asp:Label></strong></td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top" class="style4"><strong>Hire Date</strong></td>
                  <td class="style4">
                            <asp:Label ID="lblHireDate" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Job Type</strong></td>
                        <td width="230"><asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Cost Center</strong></td>
                        <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
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
        
            <asp:ListItem Value="4">Please review IDP/Nursing Clinical Ladder </asp:ListItem>
          

              
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
        Width="100%" DataKeyNames="log_status_id" CellPadding="3" AllowPaging="True" BorderWidth="1px" 
        BorderStyle="None" ShowFooter="True" BackColor="White" BorderColor="#999999" 
                  EnableModelValidation="True" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="Azure" />
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
                    
                      <asp:TextBox ID="txtLogDate" ReadOnly="true" runat="server" Text='<%# Bind("log_time", "{0:dd/MM/yyyy hh:mm}") %>'  ></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:BoundField HeaderText="TAT" />
        </Columns>
                    <HeaderStyle BackColor="#EEF1F3" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                   
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
          
            <br />
          </div>
        </div>
    
        <asp:Panel runat="server" ID="PlaceHolder1">
        <div class="tabber" id="mytabber1">
          <div class="tabbertab">
            <h2>
             <strong><asp:Label ID="lblTab1" runat="server" Text="Functional Development"></asp:Label></strong></h2>
              <asp:Label ID="lblLadderNotify" runat="server" Font-Bold="true" Visible="false" Text="แบบฟอร์มนี้ใช้กรอกเพื่อยื่นคำขอปรับระดับวิชาชีพเท่านั้น ไม่สามารถใช้เป็นหลักฐานอ้างอิงการอนุมัติปรับระดับวิชาชีพ"></asp:Label>
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                 
             <asp:Panel ID="panelFunction" runat="server">
             <div >
                 &nbsp;<asp:GridView ID="GridFunction" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("function_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="25px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="R/E">
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblRequire" runat="server" Text='<%# Bind("is_required") %>'></asp:Label>
                                <asp:Label ID="lblDelete" runat="server" Text='<%# Bind("is_delete") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblNewIDP" runat="server" Text='<%# Bind("is_new_idp") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories">
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("category_name_th") %>'></asp:Label><br />
                               <asp:Label ID="lblCategoryEN" runat="server" Text='<%# Bind("category_name_en") %>' ForeColor="navy"></asp:Label>
                          </ItemTemplate>
                        
                          <ItemStyle Width="120px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Topics">
                         <ItemStyle Width="250px" />
                          <ItemTemplate>
                              <asp:Label ID="txtcat_id" runat="server" Text='<%# Bind("category_id") %>' Visible="false"></asp:Label>
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("topic_name") %>'></asp:Label><br />
                                <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("template_title") %>'></asp:Label>
                                 <asp:Label ID="lblTopicNameEn" runat="server" Text='<%# Bind("topic_name_en") %>' Visible="false"></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Expected outcomes">
                          <ItemTemplate>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expect_detail") %>'></asp:Label>
                              <br />  <asp:Label ID="Label2" runat="server" Text='<%# Bind("expect_en") %>' ></asp:Label>
                          </ItemTemplate>
                         
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Methodology">
                          <ItemTemplate>
                              <asp:DropDownList ID="txtmethod_grid" runat="server" DataTextField="method_name" DataValueField="method_id" Width="80px" Visible="false">
                              </asp:DropDownList>
                              <asp:Label ID="lblMethod" runat="server" Text='<%# Bind("methodLang") %>' Visible="true"></asp:Label>
                               <asp:Label ID="lblMethodId" runat="server" Text='<%# Bind("method_id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                       
                          <ItemStyle Width="120px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Limit">
                          <ItemTemplate>
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("nursing_limit") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Time">
                          <ItemTemplate>
                              <asp:TextBox ID="txtTime" runat="server" Text='<%# Bind("nursing_time") %>' 
                                  Width="25px"></asp:TextBox>
                            
                             
                          </ItemTemplate>
                          <ItemStyle VerticalAlign="Top" Width="60px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Score">
                          <ItemTemplate>
                             
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("nursing_score") %>'></asp:Label>
                          </ItemTemplate>
                      
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Achieve">
                          <ItemTemplate>
                              <asp:Label ID="lblArchieve" runat="server" 
                                  Text='<%# Eval("nursing_achieve") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Start">
                          <ItemTemplate>
                            <asp:TextBox ID="lblDate1" runat="server"  Width="120px"
                                  Text='<%# ConvertTSToDateString(Eval("date_start_ts")) %>'>

                          </asp:TextBox>
                                 <asp:CalendarExtender ID="txtdate222" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="lblDate1" PopupButtonID="Image1">
                              </asp:CalendarExtender>
                                 <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" />
                          </ItemTemplate>
                        
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Finish">
                          <ItemTemplate>
                          <asp:TextBox ID="lblDate2" runat="server"  Width="120px" 
                                  Text='<%# ConvertTSToDateString(Eval("date_complete_ts")) %>'>

                          </asp:TextBox>
                               <asp:CalendarExtender ID="txtdate111" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="lblDate2" PopupButtonID="Image2" >
                              </asp:CalendarExtender>
                           <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.gif" />
                          </ItemTemplate>
                          
                          <ItemStyle Width="100px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Status">
                          <ItemTemplate>
                           <asp:DropDownList ID="txtStatusInGrid" runat="server" Width="80px" >
                    <asp:ListItem Value="0">-</asp:ListItem>
                    <asp:ListItem Value="1">Completed</asp:ListItem>
                    <asp:ListItem Value="2">On-going</asp:ListItem>
                    <asp:ListItem Value="3">Hold</asp:ListItem>
                      <asp:ListItem Value="4">Cancelled</asp:ListItem>
                    </asp:DropDownList>
                              <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("topic_status") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblStatusId" runat="server" Text='<%# Bind("topic_status_id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Textbox TextMode="MultiLine" Rows="2" ID="txtremark1" runat="server" Text='<%# Bind("function_remark") %>' ToolTip='<%# Bind("function_remark") %>'></asp:Textbox><br />
                                <asp:Label ID="lblComment_th" runat="server" Text='<%# Eval("comment_th") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblComment_en" runat="server" Text='<%# Bind("comment_en") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                     
                                      <asp:Label ID="lblFileList" runat="server" ></asp:Label>
                                            <asp:Button ID="cmdFile" runat="server" Text="Detail"    />
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                  </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <asp:Panel ID="panel_ladder_score" runat="server">
              <table width="100%" style="background-color:#CCCCCC">
              <tr>
              <td>
                  <strong>
                  Total score for <asp:Label ID="lblScoreName" runat="server" Text=""></asp:Label>
                  </strong>
                  </td>
              <td><strong>Employee Score</strong></td>
              </tr>
                  <tr>
                      <td>
                          Require :
                          <asp:Label ID="lblScoreRequire" runat="server" Text=""></asp:Label>
                      </td>
                      <td>
                          Require :
                          <asp:Label ID="lblEmpScoreRequire" runat="server" Text=""></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td>
                          Elective :
                          <asp:Label ID="lblScoreElective" runat="server" Text=""></asp:Label>
                      </td>
                      <td>
                          Elective :
                          <asp:Label ID="lblEmpScoreElective" runat="server" Text=""></asp:Label>
                      </td>
                  </tr>
              </table>
              </asp:Panel>
              </div>
                <span style="color:magenta"><strong>If cancel, please define reason in remark 
                 field.</strong></span><br />&nbsp;<span style="color:Red">R = Require (Cannot 
                 remove from your IDP or Ladder)</span>&nbsp; &nbsp;
            <br />   <span style="color:blue">E = Elective (Optional)</span>
                 <br />
                 <asp:Button ID="cmdSaveOrder" runat="server" Text="Update My Topic" 
                  OnClientClick="alert('Save completed')" Visible="False" /> &nbsp;
              <asp:Button ID="cmdDelete" runat="server" ForeColor="Red" Text="Delete" 
                            OnClientClick="return confirm('Are you sure you want to delete?')" 
                            Width="130px" />
            <br /><br />
            <asp:Panel ID="panel_addTopic" runat="server">
            <fieldset>
            <legend style="font-weight:bold">Add New Topic (Elective)</legend>
           
            <table width="100%"   cellpadding="3">  
           
              <% 'If mode = "add" or txtstatus.SelectedValue = "" or txtstatus.SelectedValue = "1" Then%>
          
                <tr>
                    <td valign="top" width="200" class="style3">
                        หมวดหมู่ / Category</td>
                    <td valign="top" class="style3" ><asp:DropDownList ID="txtadd_cat1" runat="server" 
                            DataValueField="category_id" Width="300px" AutoPostBack="True" >
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        หัวข้อ / Topic</td>
                    <td valign="top" >
                        <asp:DropDownList ID="txtfind_topic" runat="server" AutoPostBack="True" 
                            DataValueField="topic_id" >
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtadd_topic1" runat="server" BackColor="#FFFF99" Rows="2" 
                            TextMode="MultiLine" Visible="False" Width="300px"></asp:TextBox>

                         <asp:TextBox ID="TextBox1" runat="server" BackColor="#FFFF99"
                             Width="300px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                </table>
                <asp:Panel ID="panel_idp_expectoutcome" runat="server" Visible="false">
                 <table width="100%"   cellpadding="3">  
                <tr>
                    <td valign="top" width="200">
                        สิ่งที่จะได้รับ / Expected outcome</td>
                    <td valign="top" >
                        <asp:DropDownList ID="txtfind_expect" runat="server" AutoPostBack="True" 
                            DataValueField="expect_id">
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtadd_expect1" runat="server" BackColor="#FFFF99" Rows="2" 
                            TextMode="MultiLine" Visible="False" Width="300px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                            CompletionSetCount="5" DelimiterCharacters="" Enabled="True" 
                            EnableViewState="false" MinimumPrefixLength="1" ServiceMethod="getExpect" 
                            ServicePath="~/IDP_TopicService.asmx" TargetControlID="txtadd_expect1">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style2" >วิธีการ / Methodogy
                        </td>
                    <td valign="top" class="style2" >
                        <asp:DropDownList ID="txtadd_method1" runat="server" 
                            DataTextField="method_name" DataValueField="method_id" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        เริ่มต้น / Start</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtadd_datestart1" runat="server" BackColor="#FFFF99" 
                            Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtadd_datestart1" PopupButtonID="Image2">
                        </asp:CalendarExtender>
                        &nbsp;<asp:Image ID="Image2" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        สิ้นสุด / Finish</td>
                    <td valign="top" >
                        <asp:TextBox ID="txtadd_dateend1" runat="server" BackColor="#FFFF99" 
                            Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="txtadd_dateend1" PopupButtonID="Image3">
                        </asp:CalendarExtender>
                        &nbsp;<asp:Image ID="Image3" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" >
                        สถานะ / Status</td>
                    <td valign="top" >
                        <asp:DropDownList ID="txtadd_status1" runat="server" 
                            Width="100px">
                            <asp:ListItem Value="0">-</asp:ListItem>
                            <asp:ListItem Value="1">Completed</asp:ListItem>
                            <asp:ListItem Value="2">On-going</asp:ListItem>
                            <asp:ListItem Value="3">Hold</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>
                </asp:Panel>
           <table width="100%"   cellpadding="3">  
                <tr>
                    <td valign="top" width="200">
                        <asp:Label ID="lblRemark" runat="server" Text="หมายเหตุ / Remark"></asp:Label>
                       </td>
                    <td valign="top">
                        <asp:TextBox ID="txtadd_remark" runat="server"  Rows="2" 
                            TextMode="MultiLine"  Width="300px"></asp:TextBox>
                      
                    </td>
                </tr>
          
         
                  <tr>
                      <td valign="top" width="200">
                          &nbsp;</td>
                      <td valign="top">
                          <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                              CompletionSetCount="5" DelimiterCharacters="" Enabled="True" 
                              MinimumPrefixLength="1" ServiceMethod="getTopic" 
                              ServicePath="~/IDP_TopicService.asmx" TargetControlID="txtadd_topic1">
                          </asp:AutoCompleteExtender>
                          <asp:Button ID="cmdAddTopic" runat="server" Font-Bold="True" 
                              OnClientClick="return validateIDP()" Text="Add New Topic" Height="27px" />
                      </td>
                </tr>
          
         
                  <% 'end if %>
            </table>
             </fieldset>
            </asp:Panel>
            </asp:Panel>
         </ContentTemplate>
              </asp:UpdatePanel>
          </div>

          <div class="tabbertab" runat="server" id="tab_ladder" >
            <h2>
             <strong>Educator Review</strong>
            </h2>
                <asp:GridView ID="GridEducator" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td bgcolor="#DBE1E6">
                      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                          <tr>
                            <td width="156" valign="top" bgcolor="#DBE1E6"><strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label></strong></td>
                            <td width="180" valign="top"><strong><asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label></strong></td>
                            <td width="159" valign="top"><strong><asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label></strong></td>
                            <td width="189" valign="top"><strong><asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label></strong></td>
                            <td width="120"><strong>
                                <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label></strong></td>
                          </tr>
                      </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td bgcolor="#FFFFFF" width="250"><strong><asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label> </strong></td>
                            <td bgcolor="#FFFFFF">
                             <strong>Comment Subject :</strong>   <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                            <td align="right"><div align="right"> 
                            <!--<img src="../images/comment_add.png" width="16" height="16" /> 
                            <img src="../images/comment_edit.png" width="16" height="16" /> 
                            <img src="../images/comment_delete.png" width="16" height="16" />-->
                               <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>   
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>            
                                <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/comment_edit.png" ToolTip="Edit Comment" Visible="false" />
                                 <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/comment_delete.png" OnCommand="onDeleteEducatorComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' />
                              
                            </div></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><p><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label><br />
                              <br />
                      </p></td>
                    </tr>
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#336666"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
            <asp:Panel runat="server" ID="panel_educator" Enabled="false">
              <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td colspan="5"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="150"><strong>Acknowledge</strong></td>
                      <td><table width="100%" border="0">
                          <tr>
                            <td width="158"><span class="red">
                             <asp:DropDownList ID="txteducator_status" runat="server">
                                 
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                            </span></td>
                            <td><strong>Comment Subject&nbsp;
                            </strong>
                                <asp:DropDownList ID="txteducator_subject" runat="server" DataTextField="subject_name" DataValueField="subject_id">
                                
                                </asp:DropDownList>
                              </td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td><strong>Detail</strong></td>
                      <td><textarea name="txteducator_detail" id="txteducator_detail" cols="45" rows="5" 
                              style="width: 98%" runat="server"></textarea></td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td>
                          <asp:Button ID="cmdSaveEducatorReview" runat="server" Text="Save" />
                        </td>
                    </tr>
                  </table>
</td>
              </tr>
            </table>
            </asp:Panel>
            <br />
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
                  <td width="1022" colspan="5">
                      <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td bgcolor="#DBE1E6">
                      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                          <tr>
                            <td width="156" valign="top" bgcolor="#DBE1E6"><strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label></strong></td>
                            <td width="180" valign="top"><strong><asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label></strong></td>
                            <td width="159" valign="top"><strong><asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label></strong></td>
                            <td width="189" valign="top"><strong><asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label></strong></td>
                            <td width="120"><strong>
                                <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date", "{0:dd/MM/yyyy hh:mm}") %>'></asp:Label></strong></td>
                          </tr>
                      </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td bgcolor="#FFFFFF" width="250"><strong><asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label></strong></td>
                            <td bgcolor="#FFFFFF">
                               <strong>Comment Subject :</strong>  <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                            <td align="right"><div align="right"> 
                            <!--<img src="../images/comment_add.png" width="16" height="16" /> 
                            <img src="../images/comment_edit.png" width="16" height="16" /> 
                            <img src="../images/comment_delete.png" width="16" height="16" />-->
                               <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>   
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>            
                                <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/comment_edit.png" ToolTip="Edit Comment" />
                                 <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/comment_delete.png" OnCommand="onDeleteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' />
                              
                            </div></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><p><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label><br />
                              <br />
                      </p></td>
                    </tr>
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#336666"></td>
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
                        <td width="150" bgcolor="#eef1f3"><strong>Acknowledge</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td width="80">
                                  <asp:DropDownList ID="txtacknowedge_status" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              <td width="25">&nbsp;</td>
                              <td width="158"><strong>Comment Subject</strong></td>
                              <td>
                                 <asp:DropDownList ID="txtcomment" runat="server" DataTextField="subject_name" DataValueField="subject_id">
                               
                                   
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

                  <br /></td>
                </tr>
              </table>
            
              </asp:Panel>
            <br />
          </div>
            <div class="tabbertab" id="tab_competency" runat="server">
            <h2><strong>Competency proficiency Scale</strong></h2>
            <asp:Panel ID="panel_scale" runat="server" Enabled= "false">
            <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td colspan="5"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                  <tr>
                    <td colspan="2"><strong>Competency proficiency Scale</strong></td>
                  </tr>
                  <tr>
                    <td><strong>1</strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>Professional Competency</strong></label></td>
                          <td width="214">
                              <asp:TextBox ID="txtscale1" runat="server" Width="120px"></asp:TextBox>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>2. </strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>General Nursing Skill</strong></label></td>
                          <td width="214">
                              <asp:TextBox ID="txtscale2" runat="server" Width="120px"></asp:TextBox>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>3. </strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td width="246"><label for="radio5"><strong>General OPD Nursing Skill</strong></label></td>
                          <td width="214">
                              <asp:TextBox ID="txtscale3" runat="server" Width="120px"></asp:TextBox>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td><strong>4</strong></td>
                    <td>
                    <table width="100%" border="0">
                        <tr>
                          <td class="style6"><strong>Functional Competency</strong></td>
                          <td width="214">
                              <asp:TextBox ID="txtscale4" runat="server" Width="120px"></asp:TextBox>
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table>
                    </td>
                  </tr>
                  <tr>
                    <td><strong>Total </strong></td>
                    <td><table width="100%" border="0">
                        <tr>
                          <td class="style6"><strong>= 1+2+3+4</strong></td>
                          <td width="214">
                              <asp:TextBox ID="txttotal_scale" runat="server" Width="120px"></asp:TextBox>
                             
                            </td>
                          <td width="382">&nbsp;</td>
                        </tr>
                    </table></td>
                  </tr>
                    <tr>
                        <td>
                           <strong>Full Score</strong></td>
                        <td>
                            &nbsp;<asp:TextBox ID="txtfullscore" runat="server" 
                                Width="120px"></asp:TextBox>
                            &nbsp;=
                            <asp:TextBox ID="txtfullscore_percent" runat="server" Width="120px"></asp:TextBox>
                            &nbsp;%
                            <asp:Button ID="cmdCal" runat="server" Text="Calculate" />
                        </td>
                    </tr>
                    <tr>
                      <td width="150"><strong>Acknowledge</strong></td>
                      <td>
                          <asp:DropDownList ID="txtstatus_scale" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                     </td>
                    </tr>
                    <tr>
                      <td><strong>Detail</strong></td>
                      <td>
                          <asp:TextBox ID="txtscale_detail" runat="server" Rows="3" TextMode="MultiLine" 
                              Width="350px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="cmdUpdateScale" runat="server" Text="Update" />
                        </td>
                    </tr>

                  </table>
                  <br /></td>
              </tr>
            </table>
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <%# Container.DataItemIndex + 1 %>.
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:BoundField HeaderText="Date/Time" DataField="inform_date" DataFormatString="{0:dd/MM/yyyy hh:mm}" ItemStyle-Width="120px" />
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
          </asp:Panel>
        
        <br />
  </div>
</asp:Content>

