<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="star_home" %>

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
    <div >
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td><div id="header"><img src="../images/star_32.png" alt="SSIP" width="32" height="32" a  />&nbsp;Star of bumrungrad 
                <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                </div></td>
                  
                <td><div id="header2" align="right">
                  <asp:Button ID="cmdNew" runat="server"  Width="150px" Text="New" />
                </div>                </td>
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
        <td width="40" height="30">From</td>
        <td width="300">&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
         
         &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
&nbsp; </td>
        <td width="60">Department</td>
        <td width="238">
            <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" Width="200px"
                DataValueField="dept_id">            </asp:DropDownList>           </td>
        <td width="63">Status</td>
        <td>
            <asp:DropDownList ID="txtstatus" runat="server" DataTextField="status_name" DataValueField="status_id">            </asp:DropDownList>          </td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td align="right"><table width="100%" cellspacing="2" cellpadding="1">
      <tr>
        <td width="120">Star No.</td>
        <td>
            <asp:TextBox ID="txtstarno" runat="server"></asp:TextBox>
          &nbsp;<asp:CheckBox ID="txtpin" runat="server" 
                Text="แสดงรายการที่พนักงานได้รับ Star Pin มากกว่าหรือเท่ากับ 5 ครั้ง" />
          </td>
        </tr>
    
      <tr>
        <td >Nominee</td>
        <td>
            <asp:TextBox ID="txtnominee" runat="server"></asp:TextBox>
          </td>
        </tr>
    
      <tr>
        <td >Job Type</td>
        <td>
                <asp:DropDownList ID="txtjobtype" runat="server" DataTextField="job_type" DataValueField="job_type">
                              </asp:DropDownList>&nbsp;</td>
        </tr>
    
    </table></td>
  </tr>

  <tr>
    <td align="right">
    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />&nbsp;     </td>
  </tr>
      </table>
       <br />
        <div id="div_hr" runat="server" visible="false">
         <fieldset>
     <legend><strong>For Coordinator Process</strong></legend>
     
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
               
                <td>
          &nbsp;
                    <asp:Button ID="cmdAwardStatus" runat="server" Text="Send Star Point to SRP" OnClientClick="return confirm('Are you sure you want to send point to SRP ?')" />
                </td>
            </tr>
        </table>
   
     </fieldset>
      </div>
<br />
          <div class="small" style="margin-bottom: 3px;">
              <asp:Label ID="lblNum" runat="server" Text="Label"></asp:Label> Records Found</div>
          <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="star_id" 
              AllowPaging="True" PageSize="20" RowHeaderColumn="position" 
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
                  <asp:TemplateField HeaderText="Star No.">
                      <ItemTemplate>
                          <asp:HyperLink ID="HyperLink1" runat="server"  
                              NavigateUrl='<%# "form_star.aspx?mode=edit&id=" & Eval("star_id")%>' 
                              Text='<%# Eval("ssip_no1") %>'></asp:HyperLink>
                              <br />
                               <asp:Label ID="lblMsg" runat="server" Text='' ForeColor="Blue"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="ผู้ทำรายการ">
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("report_by") %>'></asp:Label><br />
                          Dept name : <asp:Label ID="Label3" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label><br />
                          Cost Center : <asp:Label ID="Label4" runat="server" Text='<%# Bind("report_dept_id") %>'></asp:Label><br />
                      </ItemTemplate>
                    <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
              
                  <asp:TemplateField HeaderText="Nominator ผู้เสนอชื่อ">
                      <HeaderStyle ForeColor="White" />
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" 
                              Text='<%# Bind("complain_status_remark") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
              
                  <asp:TemplateField HeaderText="Nominee ผู้ถูกเสนอชื่อ">
                      <ItemTemplate>
                          <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                       
                      </ItemTemplate>
                   <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
  
                  <asp:TemplateField HeaderText="Star Point" >
                <HeaderStyle ForeColor="White" />
                      <ItemTemplate>
                          <asp:Label ID="Label5" runat="server" Text='<%# Bind("srp_point") %>'></asp:Label>
                      </ItemTemplate>
                    
                  </asp:TemplateField>
  
                  <asp:TemplateField HeaderText="Doc. Status" >
              
                      <ItemTemplate>
                     <asp:Label ID="lblsend" runat="server" Text='<%#Bind ("is_send_score")%>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatusName" runat="server" Text='<%#Bind ("status_name")%>'></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Submit Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("submit_date") %>'></asp:Label>
                                              <asp:Label ID="lblSubmitDateTS" runat="server" Text='<%# Bind("submit_date_ts") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                      
                                        <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                     <asp:TemplateField HeaderText="Approval ">
                      <ItemTemplate>
                          <asp:Label ID="lblImage1" runat="server" Text=""></asp:Label>
                          <asp:Image ID="imgWait6" ImageUrl="../images/history.png" runat="server" Visible="false" />
                          <asp:Image ID="imgCancel6" ImageUrl="../images/button_cancel.png" runat="server" Visible="false" />
                          <asp:Image ID="imgApprove6" ImageUrl="../images/button_ok.png" runat="server" Visible="false" />
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Committee">
                      <ItemTemplate>
                          <asp:Label ID="lblCommittee" runat="server" Text=''></asp:Label>
                      </ItemTemplate>
                     <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="TAT">
                      <ItemTemplate>
                          <asp:Label ID="lblTAT" runat="server"></asp:Label>
                      </ItemTemplate>
                    <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="">
                    
                      <ItemTemplate>
                        <asp:Label ID="lblPK" runat="server" Text='<%# bind("star_id") %>' Visible="true"></asp:Label>
                          <asp:Label ID="lblStatusID" runat="server" Text='<%# bind("status_id") %>' Visible="false"></asp:Label>
                          <a href="javascript:void(null);" onclick='openPopup(<%# Eval("star_id") %>)'>
                          <asp:Label ID="lblStatus" runat="server" Text='<%# bind("status_id") %>' Visible="false"></asp:Label>
                          </a>
                         
                         
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                   
                  <ItemStyle ForeColor="Red" />
                  <ItemTemplate>
                                     
                      <asp:ImageButton ID="linkDelete" runat="server" CommandName="cancelCommand" 
                    CommandArgument="<%# Bind('star_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/bin.png" ToolTip="Delete" />
                     
                      <asp:ImageButton ID="linkCancel" runat="server" CommandName="cancelCommandByTQM" 
                    CommandArgument="<%# Bind('star_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/cancel.png" ToolTip="Cancel" />
                      
                  </ItemTemplate>
                  </asp:TemplateField>
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#E098AC" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              
          </asp:GridView>
          <br />
       
        </div>
      </div>
</asp:Content>

