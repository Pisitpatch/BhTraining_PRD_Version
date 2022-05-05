<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="ext_training_list.aspx.vb" Inherits="idp_ext_training_list" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

    .style2
    {
        height: 17px;
    }

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

    function openPopup(irId) {
        window.open("../incident/popup_status.aspx?irId=" + irId, "List", "scrollbars=no,resizable=no,width=800,height=480");
        return false;
    }
</script>
<script type="text/javascript">
    var global_time;
    // =========== Check Session ===========================
    checkSession('<%response.write(session("bh_username").toString) %>', '<%response.write(viewtype) %>', '<%response.write(session("req").toString) %>'); // Check session every 1 sec.
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><div id="header"><img src="../images/menu_03.png" width="32" height="32"   />&nbsp;&nbsp;<asp:Label ID="lblHeader"
        runat="server" Text="-"></asp:Label> Training Request  
            <asp:Label ID="lblAction" runat="server" ForeColor="#0033CC"></asp:Label></div></td>
        <td><div align="right" style="margin-right: 10px;">
          <asp:Button ID="cmdNew" runat="server" Text="New Request" 
          Width="150px" /></div></td>
      </tr>
    </table>
<div id="data">
 
<table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" alt="x" />&nbsp;Search</td>
  </tr>
  <tr>
    <td height="30"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="40" height="30">Date</td>
        <td colspan="5">
            <asp:RadioButtonList ID="txtdatetype" runat="server" 
                RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Submit date</asp:ListItem>
                <asp:ListItem Value="2">Training date</asp:ListItem>
            </asp:RadioButtonList>
          </td>
        </tr>
      <tr>
        <td width="40" height="30">From</td>
        <td width="300"><label>&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
          </label>
         &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
&nbsp; </td>
        <td width="60">Department</td>
        <td width="208">
            <asp:DropDownList ID="txtdept" runat="server"  DataTextField="dept_name_en" DataValueField="dept_id" Width="180px">
            </asp:DropDownList>
           </td>
        <td width="53">Status</td>
        <td>
            <asp:DropDownList ID="txtstatus" runat="server" DataTextField="status_name" DataValueField="idp_status_id">
            </asp:DropDownList>
          </td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td align="right"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="100">Employee code</td>
        <td width="150">
          <input type="text" name="txtemp_code" id="txtemp_code" style="width: 95px;" runat="server" /> </td>
          <td width="100">Employee name</td>
          <td ><input type="text" name="txtempname" id="txtempname" style="width: 150px;" runat="server" /></td>
        </tr>
    
    </table></td>
  </tr>
   <tr>
    <td height="30">
    <table width="100%">
      <tr>
        <td width="100">Title</td>
        <td width="250">
            <asp:TextBox ID="txttitle" runat="server" Width="200px"></asp:TextBox>
          </td>
          <td width="100">Request No.</td>
          <td >
            <asp:TextBox ID="txtrequestno" runat="server" Width="200px"></asp:TextBox>
          </td>
        </tr>
    </table>
    </td>
    </tr>
  <tr>
    <td align="right">
        <asp:Button ID="cmdSearch" runat="server" CssClass="button-green2" 
            Font-Bold="True" Text="Search" />
        &nbsp;
        <asp:Button ID="cmdReset" runat="server" CssClass="button-green2" 
            Text="Reset" />
      </td>
  </tr>
  <tr>
    <td align="right">
    <div id="div_hr" runat="server">     <fieldset>
     <legend><strong>For TRD process</strong></legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140">
                    Change status to</td>
                <td>
            <asp:DropDownList ID="txthrstatus" runat="server" DataTextField="status_name" 
                        DataValueField="idp_status_id">
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdUpdateStatus" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                </td>
            </tr>
        </table></fieldset>
    </div>

     <div id="div_dept" runat="server">
     <fieldset>
     <legend><strong>For Manager Approval</strong></legend>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140" class="style2">
                    Approval name</td>
                <td class="style2">
                    <asp:Label ID="lblApproveName" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="140">
                    Approval level</td>
                <td>
                    <asp:Label ID="lblYourLevel" runat="server" Text="Label"></asp:Label>
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
                    <asp:Button ID="cmdDeptStatus" runat="server" Text="Confirm" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                </td>
            </tr>
        </table>
        <div><img src="../images/sign_explain.png" alt="tip" /> Tip : 1. 
            เลือกสถานะอนุมัติ/ไม่อนุมัติ&nbsp; 2.เลือกพนักงาน โดยคลิกที่ Checkbox (<input type="checkbox" disabled />)&nbsp;&nbsp; 
            3.คลิกที่ปุ่ม Confirm </div>
        </fieldset>
    </div>
     </td>
  </tr>
  <tr>
    <td align="right">
   </td>
  </tr>
    </table>
        <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="bottom"><span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span></td>
              <td style="text-align: right;">&nbsp;</td>
            </tr>
          </table>
        </div>
        <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="idp_id" 
              AllowPaging="True" PageSize="30" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data."  
        EnableModelValidation="True">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
               <asp:TemplateField>
               <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll" AutoPostBack="True" />
            </HeaderTemplate>
                    <ItemTemplate>
                     
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
              </asp:TemplateField>
                  <asp:TemplateField>
                       <ItemStyle Width="30px" />
                      <ItemTemplate>
                          <asp:Image ID="imgReply" runat="server" ImageUrl="~/images/reply.png" Visible="false" ToolTip='<%# Eval("reply_note") %>' />
                            <asp:Label ID="lblReplyNote" runat="server" Text='<%# Bind("reply_note") %>' Visible="false"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Request No.">
                      <ItemTemplate>
                         
                      <a href="ext_training_detail.aspx?mode=edit&id=<%# Eval("idp_id") %>&req=<%# Eval("request_type") %>">
                      <asp:Label runat="server" Text='<%# Eval("idp_no") %>' ID="lblLink"></asp:Label></a>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Staff Information">
                    
                      <ItemTemplate>
                       <asp:Label ID="lblTitleExt" runat="server" Text='<%# Bind("title") %>' Font-Bold="True" ForeColor="#0d36be"></asp:Label>
                       <asp:Label ID="lblTitleInt" runat="server" Text='<%# Bind("title") %>' Font-Bold="True" Visible="false" ForeColor="#006666"></asp:Label>
                       <br />
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("report_by") %>'></asp:Label>
                          <br />Dept name : <asp:Label ID="Label3" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                          <br />Cost Center : <asp:Label ID="Label4" runat="server" Text='<%# Bind("report_dept_id") %>'></asp:Label>
                          <asp:Label ID="lblAuthen" runat="server" Visible="false" ></asp:Label>
                        <br /> Submit date : <asp:Label ID="lblSubmitDate" runat="server" Text='<%#  Bind("date_submit", "{0:dd/MM/yyyy hh:mm}") %>' ></asp:Label>
                        <br />Action After Training : <asp:Label ID="lblAction" runat="server" Text='<%# Bind("action_complete") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Status" >
              
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%#Bind ("status_name")%>'></asp:Label> 
                         <asp:Label ID="lblStatusId" runat="server" Text='<%#Bind ("status_id")%>' Visible="false"></asp:Label> 
                           <asp:Label ID="lblPk" runat="server" Text='<%#Bind ("idp_id")%>' Visible="false"></asp:Label> 
                             <asp:Label ID="lblEmpCode" runat="server" Text='<%#Bind ("report_emp_code")%>' Visible="false"></asp:Label> 
                          <asp:Label ID="lblBudgetStatus" runat="server" Text='<%#Bind ("is_budget_approve")%>' Visible="false"></asp:Label> 
                            <asp:Label ID="lblBudgetUpdateBy" runat="server" Text='<%#Bind ("budget_update_by")%>' Visible="false"></asp:Label> 
                             <asp:Label ID="lblBudgetRequest" runat="server" Text='<%#Bind ("is_budget_request")%>' Visible="false"></asp:Label> 
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="1st Approval" Visible="false" >
                 
                      <ItemTemplate>
                      <asp:Label ID="lblImage1" runat="server" Text=""></asp:Label>
                         <asp:Image ID="imgWait1" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel1" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove1" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="2nd Approval">
                      <ItemTemplate>
                          <asp:Label ID="lblImage2" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait2" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel2" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove2" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="3rd ">
                    <ItemStyle Width="60px" />
                      <ItemTemplate>
                          <asp:Label ID="lblImage3" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait3" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel3" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove3" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                      <asp:TemplateField HeaderText="4th ">
                       <ItemStyle Width="60px" />
                      <ItemTemplate>
                          <asp:Label ID="lblImage4" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait4" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel4" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove4" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                    <asp:TemplateField HeaderText="5th ">
                     <ItemStyle Width="60px" />
                      <ItemTemplate>
                          <asp:Label ID="lblImage5" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait5" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel5" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove5" ImageUrl="../images/button_ok.png" runat="server" Visible="false" /> 
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acc ">
                     <ItemStyle Width="60px" />
                      <ItemTemplate>
                          <asp:Label ID="lblImage6" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait6" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel6" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove6" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Training Date">
                    
                      <ItemTemplate>
                          <asp:Label ID="lblTrainDate" runat="server" Text='<%# ConvertTSToDateString(Eval("date_start_ts")) %>'></asp:Label>
                            <asp:Label ID="lblTrainDateTS" runat="server" Text='<%# (Eval("date_start_ts")) %>' Visible="false"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Apply Date">
                    
                      <ItemTemplate>
                          <asp:Label ID="lblApplyDate" runat="server" Text='<%# ConvertTSToDateString(Eval("register_type_reserve_date_ts")) %>'></asp:Label>
                          <asp:Label ID="lblApplyDateTS" runat="server" Text='<%# (Eval("register_type_reserve_date_ts")) %>' Visible="false"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                     <asp:TemplateField>
                   
                  <ItemStyle ForeColor="Red" />
                  <ItemTemplate>
                                     
                      <asp:ImageButton ID="linkDelete" runat="server" CommandName="cancelCommand" 
                    CommandArgument="<%# Bind('idp_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/bin.png" ToolTip="Delete" />
                     
                      <asp:ImageButton ID="linkCancel" runat="server" CommandName="cancelCommandByTQM" 
                    CommandArgument="<%# Bind('idp_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/cancel.png" ToolTip="Cancel" />
                      
                  </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#afbac5" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
    </asp:GridView>
          <div><img src="../images/sign_explain.png" alt="tip" /> Tip :<br />
          <table width="100%">
          <tr><td width="250">&nbsp;<strong>Approval Level</strong></td><td>
          <img src="../images/button_ok.png" alt="ok" /> = Approve&nbsp;
          <img src="../images/button_cancel.png" alt="no" /> = Not Approve&nbsp;
          <img src="../images/history.png" alt="n/a" /> = Wait for approval&nbsp;
          <img src="../images/information.gif" alt="n/a" /> = Comment
          </td></tr>
          <tr><td width="250">1st approval = Supervisor</td><td>
          <img src="../images/reply.png" alt="reply" /> Reply message</td></tr>
          <tr><td width="250">2nd approval = Manager</td><td>&nbsp;
              </td></tr>
          <tr><td >3rd approval = Department Manager</td><td>&nbsp;</td></tr>
          <tr><td >4th approval = Director</td><td>&nbsp;</td></tr>
          <tr><td>5th approval = C-Suite</td><td>&nbsp;</td></tr>
          <tr><td >Acc approval = Accounting</td><td>&nbsp;</td></tr>
          </table>
        
          </div>
       
<br />

      </div>
</asp:Content>

