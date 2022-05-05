<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="idp_home" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

        .style2
        {
            height: 28px;
        }

    </style>

<script type="text/javascript" src="../js/datepicker2/jquery.calendars.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.plus.js"></script>


<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.ext.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.thai.js"></script>

<script type="text/javascript">
    $(function() {
        //	$.datepick.setDefaults({useThemeRoller: true,autoSize:true});
        var calendar = $.calendars.instance();

        $('#ctl00_ContentPlaceHolder1_txtdate1').calendarsPicker(
			{
			    
			showTrigger: '#calImg' ,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer">
</div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><div id="header"><img src="../images/menu_03.png" width="32" height="32" align="absmiddle"  />&nbsp;&nbsp;
            <asp:label runat="server" id="lblHeader" text="Individual Development Plan"></asp:label>
        </div></td>
        <td><div align="right">
            <asp:Button ID="cmdNew" runat="server" Text="New" 
          Width="150px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
      </tr>
    </table>
    
<div id="data">
 
<table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
  </tr>
  <tr>
    <td height="30"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="40" height="30">&nbsp;From</td>
        <td width="300">&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
         
         &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
&nbsp; </td>
        <td width="60">Department</td>
        <td width="238">
            <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" Width="200px"
                DataValueField="dept_id">
            </asp:DropDownList>
           </td>
        <td width="63">Status</td>
        <td>
            <asp:DropDownList ID="txtstatus" runat="server" DataTextField="status_name" DataValueField="idp_status_id">
            </asp:DropDownList>
          </td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td align="right"><table width="100%" cellpadding="3" cellspacing="0" >
      <tr>
        <td width="120">Employee code</td>
        <td width="150">
          <input type="text" name="txtemp_code" id="txtemp_code" style="width: 95px;" runat="server" /> </td>
        <td width="100">Employee Name</td>
        <td>
          <input type="text" name="txtname" id="txtname" style="width: 155px;" runat="server" /> </td>
        </tr>
      <tr>
        <td width="120">IDP Year</td>
        <td width="150">
            <asp:DropDownList ID="txtidp_year" runat="server">
              </asp:DropDownList>
          </td>
        <td width="100">&nbsp;</td>
        <td>
            &nbsp;</td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td align="right">
  <asp:Panel ID="panel_ladder" runat="server" Visible="false">
  <table width="100%" cellpadding="3" cellspacing="0">
  <tr>
  <td width="120" class="style2">Form name</td>
  <td class="style2">
      <asp:DropDownList ID="txtform" runat="server" DataValueField="template_id" 
                  DataTextField="template_title">
      </asp:DropDownList>
      </td>
  </tr>
      <tr>
          <td >
              Form type</td>
          <td>
              <asp:DropDownList ID="txtformtype" runat="server">
                  <asp:ListItem Value="">-</asp:ListItem>
                  <asp:ListItem Value="1">Adjust Level</asp:ListItem>
                  <asp:ListItem Value="2">Maintain Level</asp:ListItem>
              </asp:DropDownList>
          </td>
      </tr>
      <tr>
          <td>
              Job Title</td>
          <td>
              <asp:DropDownList ID="txtjobtitle" runat="server" DataTextField="job_title" 
                  DataValueField="job_title">
              </asp:DropDownList>
          </td>
      </tr>
      <tr>
          <td>
              Year</td>
          <td>
              <asp:DropDownList ID="txtmonth" runat="server">
              <asp:ListItem Value="">-- All Month --</asp:ListItem>
          
              <asp:ListItem Value="4">April</asp:ListItem>
           
              <asp:ListItem Value="10">October</asp:ListItem>
           
              </asp:DropDownList>
              &nbsp;<asp:DropDownList ID="txtyear" runat="server">
              </asp:DropDownList>
          </td>
      </tr>
      <tr>
          <td>
              Order by</td>
          <td>
              <asp:DropDownList ID="txtorder" runat="server">
                  <asp:ListItem Value="date_submit_ts">Submit date</asp:ListItem>
                  <asp:ListItem Value="hire_date">Hire date</asp:ListItem>
              </asp:DropDownList>
              &nbsp;<asp:DropDownList ID="txtsort" runat="server">
                  <asp:ListItem Value="DESC">Descending</asp:ListItem>
                  <asp:ListItem Value="ASC">Ascending</asp:ListItem>
              </asp:DropDownList>
          </td>
      </tr>
  </table>
  </asp:Panel>
    
     </td>
  </tr>
  <tr>
    <td align="right">
    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />&nbsp;
        <input id="customsearch" type="hidden" runat="server" />
     </td>
  </tr>
      </table>
      <br />
        <div id="div_dept" runat="server">
         <fieldset>
     <legend><strong>For Manager Process</strong></legend>
     
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140">
                    Approval Status</td>
                <td>
            <asp:DropDownList ID="txtdeptstatus" runat="server">
                        <asp:ListItem Value="1">Approve</asp:ListItem>
                         <asp:ListItem Value="2">Not Approve</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdDeptStatus" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                </td>
            </tr>
        </table>
   
     </fieldset>
      </div>
      <div id="div_hr" runat="server">
       <fieldset>
     <legend><strong>For Coordinator Process</strong></legend>
        
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
        </table>
  
    </fieldset>
      </div>

        <div id="div_educator" runat="server" visible="false">
       <fieldset>
     <legend><strong>For Educator Process</strong></legend>
        
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="140">
                    Change status to</td>
                <td>
            <asp:DropDownList ID="txteducator_status" runat="server" >
            <asp:ListItem Value="1">Approve</asp:ListItem>
                         <asp:ListItem Value="2">Not Approve</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdUpdateStatus_Educator" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                </td>
            </tr>
        </table>
  
    </fieldset>
      </div>
<br />
        <div class="small" style="margin-bottom: 3px; width: 100%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="bottom"><span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span></td>
              <td style="text-align: right;">&nbsp;</td>
            </tr>
          </table>
        </div>
        <div align="right"> <asp:Button ID="cmdExport" runat="server" 
                Text="Export to excel" Visible="False" /></div>
       
        <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="idp_id" 
              AllowPaging="True" PageSize="100" RowHeaderColumn="position" 
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
                  <asp:TemplateField HeaderText="NO.">
                      <ItemTemplate>
                     <asp:Image ID="imgReply" runat="server" ImageUrl="~/images/reply.png" Visible="false"  />
                          <asp:HyperLink ID="HyperLink1" runat="server" 
                              NavigateUrl='<%# Eval("idp_id", "idp_detail.aspx?mode=edit&id={0}&flag=" & flag) %>' 
                              Text='<%# Eval("idp_no") %>'></asp:HyperLink>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:BoundField DataField="plan_year" HeaderText="Year" />
                  <asp:TemplateField HeaderText="Staff Information">
                    
                      <ItemTemplate>
                       Employee ID : <asp:Label ID="Label6" runat="server" Text='<%# Bind("report_emp_code") %>'></asp:Label>
                          <br />
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("report_by") %>'></asp:Label>
                          <br />Hire date : <asp:Label ID="Label5" runat="server" Text='<%# Bind("hire_date", "{0:dd/MM/yyyy}") %>'></asp:Label>
                          <asp:Label ID="lblFormName" runat="server" Text='<%# Bind("ladder_template_name") %>' ForeColor="#003399" Font-Bold="true" Visible="false"></asp:Label>
                          <br /><asp:Label runat="server" Text="Year : " Font-Bold="true" ForeColor="#003399" /> 
                          <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("plan_month") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblYear" runat="server" Text='<%# Bind("plan_year") %>' Font-Bold="true" ForeColor="#003399"></asp:Label>
                          <br />Dept name : <asp:Label ID="Label3" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                          <br />Cost Center : <asp:Label ID="Label4" runat="server" Text='<%# Bind("report_dept_id") %>'></asp:Label>
                           <br />Authorize : <asp:Label ID="lblAuthen" runat="server" ></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Status" >
              
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%#Bind ("status_name")%>'></asp:Label> 
                         <asp:Label ID="lblStatusId" runat="server" Text='<%#Bind ("status_id")%>' Visible="false"></asp:Label> 
                           <asp:Label ID="lblPk" runat="server" Text='<%#Bind ("idp_id")%>' Visible="false"></asp:Label> 
                              <asp:Label ID="lblEmpCode" runat="server" Text='<%#Bind ("report_emp_code")%>' Visible="false"></asp:Label> 
                    
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:BoundField DataField="ladder_full_score_percent" HeaderText="% Scale" />
                  <asp:TemplateField HeaderText="1st Level " >
                 
                      <ItemTemplate>
                      <asp:Label ID="lblImage1" runat="server" Text=""></asp:Label>
                         <asp:Image ID="imgWait1" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel1" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove1" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="2nd ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage2" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait2" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel2" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove2" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                      <asp:TemplateField HeaderText="3rd ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage3" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait3" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel3" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove3" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                      <asp:TemplateField HeaderText="4th ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage4" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait4" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel4" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove4" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>

                       <asp:TemplateField HeaderText="5th ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage5" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait5" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel5" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove5" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                     <asp:TemplateField HeaderText="Educator ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage6" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait6" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel6" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove6" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Submit Date">
                      
                      <ItemTemplate>
                          <asp:Label ID="lblDateSubmit" runat="server" 
                              Text='<%#  Bind("date_submit", "{0:dd/MM/yyyy hh:mm}") %>'></asp:Label>
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
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
        <div><br />
          <table width="100%">
          <tr><td width="200"><img src="../images/sign_explain.png" alt="tip" /> Tip :</td>
          <td>
          <img src="../images/button_ok.png" alt="ok" /> = Approve&nbsp;
          <img src="../images/button_cancel.png" alt="no" /> = Not Approve&nbsp;
          <img src="../images/history.png" alt="n/a" /> = Wait for approval&nbsp;
          <img src="../images/information.gif" alt="n/a" /> = Comment  
          <img src="../images/reply.png" alt="reply" /> Reply message</td>
          </tr>
          <tr><td width="200">&nbsp;<strong>Approval Level</strong></td>
            <td>1st approval = Supervisor</td>
          </tr>
          <tr><td width="200">&nbsp;</td>
            <td>2nd approval = Manager</td>
          </tr>
          <tr><td width="200" >&nbsp;</td>
            <td >3rd approval = Department Manager</td>
          </tr>
          <tr><td width="200" >&nbsp;</td>
            <td >4th approval = Director</td>
          </tr>
          <tr><td width="200">&nbsp;</td>
            <td>5th approval = C-Suite</td>
          </tr>
          <tr><td width="200" >&nbsp;</td>
            <td >Acc approval = Accounting</td>
          </tr>
          </table>
        
    </div>
<br />

      </div>
</asp:Content>



