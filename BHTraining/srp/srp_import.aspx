<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_import.aspx.vb" Inherits="srp_srp_import" %>
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
 <table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;
    <strong>SRP Point Adjustment</strong></div></td>
  </tr>
</table>

<div id="data">
<table width="100%" cellpadding="2" cellspacing="1" class="tdata3">
    <tr>
      <td colspan="4" class="theader" style="background-color:#11720c"><img src="../images/sidemenu_circle.jpg" width="10" height="10" alt="bullet" />&nbsp;Search</td>
    </tr>
    <tr>
      <td width="150" height="30"><strong>Date</strong></td>
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
          <strong>&nbsp;</strong></td>
    </tr>
   
    <tr>
      <td height="30"><strong>Employee</strong></td>
      <td colspan="3">       
              <asp:TextBox ID="txtfind_emp" runat="server"></asp:TextBox>
       </td>
    </tr>
   
    <tr>
      <td height="30"><strong>Card ID.</strong></td>
      <td colspan="3">
          <strong> 
              <asp:TextBox ID="txtfind_card" runat="server"></asp:TextBox>
          </strong>
        </td>
    </tr>
   
    <tr>
      <td height="30"><strong>Award by</strong></td>
      <td colspan="3">
          <strong> 
              <asp:TextBox ID="txtfind_mgr" runat="server"></asp:TextBox>
          </strong>
        </td>
    </tr>
   
    <tr>
      <td height="30" colspan="4">
          <asp:Button ID="cmdSearch" runat="server" Text="Search" 
              CausesValidation="False" />
          <asp:Button 
                    ID="cmdExport" runat="server" Text="Export to Excel" />
        </td>
    </tr>
  </table>
 
  <br />
<asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
        ShowFooter="True" PageSize="50">
                  <HeaderStyle BackColor="#11720c" ForeColor="White" />
                  <FooterStyle  BackColor="#EEEEEE"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="Date">
                         <ItemStyle Width="100px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("movement_create_date_ts")) %>'></asp:Label>
                               <asp:Label ID="Label1" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"hour") %>'></asp:Label>:
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertTSTo(Eval("movement_create_date_ts"),"min") %>'></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("point_movement_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblMovementID" runat="server" Text='<%# Bind("movememt_type_id") %>' 
                                  Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="move_name" HeaderText="Card ID." >
                      <ItemStyle Width="150px" />
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="Recognition">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("reward_type_name") %>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Transaction" DataField="movememt_type_name" 
                          ItemStyle-Width="100px"  >
                   
<ItemStyle Width="100px"></ItemStyle>
                   
                      </asp:BoundField>
                    
                      <asp:TemplateField HeaderText="Employee Name">
                          <ItemTemplate>
                         (<asp:Label ID="lblMethod" runat="server" Text='<%# Bind("emp_code") %>' Visible="true"></asp:Label>)
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("user_fullname") %>'></asp:Label>
                          </ItemTemplate>
                         
                      
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="คะแนนที่ได้รับ/ Point">
                      <ItemStyle HorizontalAlign="Right"  BackColor="LightBlue" />
                          <ItemTemplate >
                             <div style="text-align:right">
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("point_trans") %>'></asp:Label>
                        </div>
                          </ItemTemplate>
                      <FooterTemplate>
                       <div style="text-align:right">
                       <asp:Label ID="lblTotalScore" runat="server" Font-Bold="true" ></asp:Label>
                       </div>
                      </FooterTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("movement_remark") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="movement_create_by" HeaderText="Create by" />
                  </Columns>
              </asp:GridView>
              <br />
                 <div>
                 <fieldset>
                 <legend>Import Point from external source</legend>
              <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
              <asp:FileUpload ID="myFile1" runat="server" />
              &nbsp;
                     <asp:DropDownList ID="txtreward" runat="server">
                     <asp:ListItem Value="3">CQI</asp:ListItem>
                     <asp:ListItem Value="4">Other</asp:ListItem>
                     </asp:DropDownList>
    &nbsp;<asp:TextBox ID="txtremark1" runat="server">Add Point from Excel</asp:TextBox>
                     <asp:Button ID="cmdUpload" runat="server" Text="Import CSV" />
    &nbsp;(Emp code , Point)</fieldset>
    <br />
      <fieldset>
                 <legend>Adjust Point</legend>
                 <table width="100%">
                 <tr>
                 <td width="120">Employee Code</td>
                 <td>
                     <asp:TextBox ID="txtemp_code" runat="server"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                 <td width="120">Point</td>
                 <td>
                     <asp:TextBox ID="txtpoint" runat="server">0</asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                 <td width="120">Remark</td>
                 <td>
                     <asp:TextBox ID="txtadjust_remark" runat="server"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                 <td width="120">&nbsp;</td>
                 <td>
                     <asp:Button ID="cmdAdjust" runat="server" Text="Adjust Point" />
                     </td>
                 </tr>
                 </table>
                 </fieldset>
              </div>
              </div>
</asp:Content>

