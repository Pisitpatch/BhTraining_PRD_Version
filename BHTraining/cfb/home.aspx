<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="home.aspx.vb" Inherits="cfb.cfb_home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

        .style2
        {
            height: 25px;
        }
        .style3
        {
            height: 27px;
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

    function openPopup(irId , irNo) {
        window.open("../incident/popup_status.aspx?irId=" + irId + "&irNo=" + irNo, "List", "scrollbars=no,resizable=no,width=800,height=480");
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer">
</div>
<div id="header"><img src="../images/menu_02.png" width="32" height="32" hspace="5" align="absmiddle"  />&nbsp;&nbsp;Customer Feedback Report</div>
<div id="data"><table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="8" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
  </tr>
  <tr>
    <td height="30" colspan="8"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="40" height="30">From</td>
        <td><label>&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="60px"></asp:TextBox>
          </label>
          &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="60px"></asp:TextBox>
          &nbsp; Department
          <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" DataValueField="dept_id" Width="200px">
            </asp:DropDownList>        Status
          <asp:DropDownList ID="txtstatus" runat="server" DataTextField="ir_status_name" DataValueField="ir_status_id">
          </asp:DropDownList>
          HN <input type="text" name="txtfindhn" id="txtfindhn" style="width: 95px;" runat="server" /></td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td colspan="8" align="right"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td class="style2">CFB No. <asp:TextBox ID="txtcfbno" runat="server" Width="100px"></asp:TextBox>
          &nbsp; IR&CFB Case Owner
            <asp:TextBox ID="txtowner" runat="server"></asp:TextBox>  
            <asp:CheckBox ID="chk_showall" runat="server" Text="Waiting for investigation" 
                AutoPostBack="True" /> 
           </td>
        </tr>
          <tr>
        <td height="30">
        Physician Defendant <asp:TextBox ID="txtfinddoctor" runat="server" Width="250px"></asp:TextBox>

          &nbsp;<asp:DropDownList ID="txtspecialty" runat="server" DataTextField="specialty" DataValueField="specialty">
            </asp:DropDownList>

            &nbsp;</td>
        </tr>
          <tr>
        <td height="30">

            Unit Defendant
            <asp:DropDownList ID="txtunit_defendant" runat="server" 
                DataTextField="dept_unit_name" DataValueField="dept_unit_id">
            </asp:DropDownList>
&nbsp;
            </td>
      </tr>
      <tr>
        <td class="style3"><asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />        
          &nbsp;
          <asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="button-green2" />

            <asp:CheckBox ID="chkFlag" runat="server" Text="View only Follow up report" Visible="False" />
             <asp:CheckBox ID="chkChangeToDraft" runat="server" Text="View only Waiting for submit status" Visible="False" />
            <asp:CheckBox ID="chkMoveToIR" runat="server" Text="View only Move to IR report status" Visible="False" />
        </td>
        </tr>
      </table></td>
  </tr>
  </table>
    
    <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="bottom"><span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span>

                  <div runat="server" id="div_dept_priority" visible="false">
        <img src="../images/154.png" alt="Normal" /> Normal Priority
        <img src="../images/153.png" alt="Low" /> Low Priority
        <img src="../images/151.png" alt="High" /> High Priority
                      &nbsp;
                         <asp:DropDownList ID="txtdept_priority" runat="server" AutoPostBack="True" Visible="False">
                         <asp:ListItem Value="">Show all priority</asp:ListItem>
                        <asp:ListItem Value="normal">Normal</asp:ListItem>
                        <asp:ListItem Value="low">Low</asp:ListItem>
                        
                        <asp:ListItem Value="high">High</asp:ListItem>
                    </asp:DropDownList>
    </div>
              </td>
              <td style="text-align: right;">
         
            <asp:Button ID="cmdExport" runat="server" Text="Export to excel" />
         
                </td>
            </tr>
          </table>
        </div>
        <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" DataKeyNames="ir_id" 
              AllowPaging="True" PageSize="20" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
        EnableModelValidation="True">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                <asp:TemplateField>
                      <ItemStyle Width="20px" />
                      <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../images/special-offer.png" Visible="false" ToolTip="Repeat CFB"  ImageAlign="AbsMiddle"/><br />
                           <asp:Image ID="Image2" runat="server" ImageUrl="../images/refresh.png" Visible="false" ToolTip="Repeat Incident"  ImageAlign="AbsMiddle"/>
                              <asp:Image ID="Image3" runat="server" ImageUrl="../images/attach.png" Visible="false" ToolTip="Attach File"  ImageAlign="AbsMiddle"/>
                               <asp:Image ID="imgReply" runat="server" ImageUrl="~/images/reply.png" Visible="false"  />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="CFB NO.">
                      <ItemStyle Width="140px" />
                      <ItemTemplate>
                           <asp:Image ID="imgFlag" runat="server" ImageUrl="~/images/menu_09.png" Width="16" AlternateText="Flag" Visible="false"  />
                           
                          <asp:Image ID="imgPriority" runat="server" ImageUrl="~/images/154.png" Visible="false"  />
                          <asp:HyperLink ID="LinkNo" runat="server" 
                              NavigateUrl='<%# Eval("ir_id", "form_cfb.aspx?mode=edit&cfbId={0}") %>' 
                              Text='<%# Eval("cfb_no") %>'></asp:HyperLink>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Department" >
              
                      <ItemTemplate>
                          <asp:Label ID="lblNoComment" runat="server" Text='' Visible="false"></asp:Label> 
                          <asp:Label ID="lblDept" runat="server" Text='<%# Eval("division") %>'></asp:Label> 
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="HN">
                      <ItemTemplate>
                          <asp:Label ID="linkHN" runat="server"  Text='<%# Eval("hn") %>'></asp:Label>

                         
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Type of Comment">
                      <ItemTemplate>
                          <asp:Label ID="lblComment" runat="server"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Status">
                    
                      <ItemTemplate>
                       <a href="javascript:void(null);" onclick='openPopup(<%# Eval("ir_id") %> , <%# """" & Eval("cfb_no") & """" %>)'>
                           <img src="../images/log.png" width="16" height="16" hspace="1"  border ="0"   title="View log"/>
                          </a>
                        <asp:Label ID="lblPK" runat="server" Text='<%# bind("ir_id") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblChangeDraft" runat="server" Text='<%# bind("is_change_to_draft") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblMoveIR" runat="server" Text='<%# Bind("is_move_to_ir")%>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatusID" runat="server" Text='<%# bind("status_id") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatus" runat="server" Text='<%# bind("ir_status_name") %>'></asp:Label>
                           <asp:Label ID="lblIsCancel" runat="server" Text='<%# bind("is_cancel") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatusDept" runat="server" Text=''></asp:Label>
                         <asp:Label ID="lblStatusPSM" runat="server"  Text='<%# bind("psm_status_id") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblStatusPSM_Name" runat="server"  Text='<%# bind("psm_status_name") %>' ForeColor="#0033CC"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Submit Date">
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" 
                              Text='<%# Bind("date_submit", "{0:dd MMM yyyy}") %>'></asp:Label>
                               <asp:Label ID="Label2" runat="server" 
                              Text='<%# Bind("date_submit", "{0:t}") %>'></asp:Label>
                      </ItemTemplate>
                    
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Closed Date">
                      
                      <ItemTemplate>
                       <asp:Label ID="lblCloseDateTS" runat="server" Text='<%# Bind("date_close_ts") %>' Visible="false"></asp:Label>
                          <asp:Label ID="lblCloseDate" runat="server" Text='<%# Bind("date_close") %>' Visible="false"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Case Owner">
                      <ItemTemplate>
                    
                          <asp:Label ID="lblOwner" runat="server"></asp:Label>
                      </ItemTemplate>
                    
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="TAT" Visible="False">
                      <ItemTemplate>
                      <asp:Label ID="lblDateTS" runat="server" Visible="false" Text='<%#Bind ("datetime_complaint_ts")%>'></asp:Label>
                          <asp:Label ID="lblTAT" runat="server"></asp:Label>
                      </ItemTemplate>
                    
                  </asp:TemplateField>

                  <asp:TemplateField>
                   
                  <ItemStyle ForeColor="Red" />
                  <ItemTemplate>
                                     
                      <asp:ImageButton ID="linkDelete" runat="server" CommandName="cancelCommand" 
                    CommandArgument="<%# Bind('ir_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/bin.png" ToolTip="Delete" />
                     
                      <asp:ImageButton ID="linkCancel" runat="server" CommandName="cancelCommandByTQM" 
                    CommandArgument="<%# Bind('ir_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/cancel.png" ToolTip="Cancel" />
                      
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
<div align="center"></div>
      </div>
</asp:Content>


