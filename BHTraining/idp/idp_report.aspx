<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_report.aspx.vb" Inherits="idp_idp_report" %>

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



</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer" />
</div>

 <div id="data">
  <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td class="theader" style="height: 23px"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
  </tr>
  <tr>
    <td height="30"><table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td width="100" height="30">Year</td>
        <td width="300">
            <asp:DropDownList ID="txtyear" runat="server">
            </asp:DropDownList>
           </td>
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
        <td width="100">Department</td>
        <td width="225">
            <asp:DropDownList ID="txtdept" runat="server" 
                DataTextField="dept_name_en" Width="200px"
                DataValueField="dept_id">
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
    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />&nbsp;
     </td>
  </tr>
      </table>
        <br />

<br />
   <asp:GridView ID="GridView2" runat="server"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              EmptyDataText="There is no data." 
        EnableModelValidation="True" AutoGenerateColumns="False">
          <Columns>
             
              <asp:BoundField DataField="Cost_Center" HeaderText="Cost Center" />
              <asp:BoundField DataField="Department" HeaderText="Department" />
              <asp:TemplateField HeaderText="Completed Topics">
               
                  <ItemTemplate>
                  <a href="javascript:;" onclick="window.open('popup_idp_topic_list.aspx?dept=<%# Eval("Cost_Center") %>&status=1&planyear=<% response.write(txtyear.selectedValue) %>');">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("topic_complete") %>'></asp:Label>
                      </a>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Not Completed Topics">
                
                  <ItemTemplate>
                  <a href="javascript:;" onclick="window.open('popup_idp_topic_list.aspx?dept=<%# Eval("Cost_Center") %>&planyear=<% response.write(txtyear.selectedValue) %>');">
                      <asp:Label ID="Label2" runat="server" Text='<%# Bind("topic_all") %>'></asp:Label>
                      </a>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="percent" HeaderText="% Completed" />
              <asp:TemplateField HeaderText="Submit IDP">
                 <ItemStyle BackColor="#99ff99" />
                  <ItemTemplate>
                   <a href="javascript:;" onclick="window.open('popup_idp_topic_list.aspx?dept=<%# Eval("Cost_Center") %>&planyear=<% response.write(txtyear.selectedValue) %>&viewtype=emp');">
                      <asp:Label ID="Label3" runat="server" Text='<%# Bind("emp1") %>'></asp:Label>
                      </a>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Not Submit IDP">
                 
                  <ItemTemplate>
                  <a href="javascript:;" onclick="window.open('popup_idp_topic_list.aspx?dept=<%# Eval("Cost_Center") %>&planyear=<% response.write(txtyear.selectedValue) %>&viewtype=notsubmit');">
                      <asp:Label ID="Label4" runat="server" Text='<%# Eval("emp_notsubmit") %>'></asp:Label>
                      </a>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="percentSubmit" HeaderText="% Submit"/>
              <asp:BoundField DataField="emp_fulltime" HeaderText="Total Fulltime "  ItemStyle-BackColor="#99ffcc"  Visible ="false" />
              <asp:BoundField DataField="emp_fte" HeaderText="Total Staff" />
          </Columns>
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="#eef1f3" />
         <AlternatingRowStyle BackColor="White" />
      
</asp:GridView>
 </div>
  
</asp:Content>
