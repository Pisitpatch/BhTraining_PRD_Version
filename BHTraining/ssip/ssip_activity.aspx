<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="ssip_activity.aspx.vb" Inherits="ssip_ssip_activity" %>
<%@ Import Namespace="ShareFunction" %>

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
                <td><div id="header"><img src="../images/menu_05.png" alt="SSIP" width="32" height="32"  />&nbsp;Staff Suggestion and Innovation </div></td>
                <td><div id="header2" align="right">
                  <asp:Button ID="cmdNew" runat="server"  Width="150px" Text="New Activity Record" />
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
        <td width="60">&nbsp;</td>
        <td width="238">
            &nbsp;</td>
        <td width="63">&nbsp;</td>
        <td>
            &nbsp;</td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td align="right"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="60">&nbsp;</td>
        <td>
          &nbsp;</td>
        </tr>
    </table></td>
  </tr>

  <tr>
    <td align="right">
    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />&nbsp;     </td>
  </tr>
      </table>
<br />
          <div class="small" style="margin-bottom: 3px;"> Records Found</div>
          <asp:GridView ID="GridView1" runat="server" 
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
                  <asp:TemplateField HeaderText="Employee Name">
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("rec_emp_name") %>'></asp:Label><br />
                          Dept name : <asp:Label ID="Label3" runat="server" Text='<%# Bind("rec_dept_name") %>'></asp:Label><br />
                          Cost Center : <asp:Label ID="Label4" runat="server" Text='<%# Bind("rec_dept_id") %>'></asp:Label><br />
                      </ItemTemplate>
                    
                  </asp:TemplateField>
              
                  <asp:TemplateField HeaderText="Innovation Subject">
                      <ItemTemplate>
                      <a href="ssip_activity_detail.aspx?mode=edit&id=<%# Eval("record_id") %>">
                          <asp:Label ID="Label2" runat="server" Text='<%#Bind ("ssip_subject")%>'></asp:Label>
                          </a>
                      </ItemTemplate>
                     
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                                 
                  <asp:TemplateField HeaderText="Activity Date" >
              
                      <ItemTemplate>
                     
                          <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("activity_date_start_ts"))%>'></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:BoundField DataField="room_name" HeaderText="Room" />

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
       
        </div>
      </div>
</asp:Content>

