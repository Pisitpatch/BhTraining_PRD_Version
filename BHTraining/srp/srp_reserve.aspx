<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_reserve.aspx.vb" Inherits="srp_srp_reserve" %>
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
            height: 30px;
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

   
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer" />
</div>
 <table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;
    <strong>SRP Item Reservation - รายการจองสินค้า</strong></div></td>
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
      <td class="style2"><strong>Employee</strong></td>
      <td colspan="3" class="style2">       
              <asp:TextBox ID="txtfind_emp" runat="server"></asp:TextBox>
       </td>
    </tr>
   
    <tr>
      <td height="30" colspan="4">
          <asp:Button ID="cmdSearch" runat="server" Text="Search" 
              CausesValidation="False" />
        &nbsp;<asp:Button 
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
                      <asp:BoundField DataField="reserve_date_raw" HeaderText="Reservation Date" />
                      <asp:TemplateField HeaderText="Item Name">
                          <ItemTemplate>
                              <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("inv_item_name_en") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="reserve_qty" HeaderText="Qty" />
                    
                      <asp:TemplateField HeaderText="Employee Name">
                          <ItemTemplate>
                         (<asp:Label ID="lblMethod" runat="server" Text='<%# Bind("emp_code") %>' Visible="true"></asp:Label>)
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                          </ItemTemplate>
                         
                      
                         
                      </asp:TemplateField>
                      <asp:BoundField DataField="contact_tel" HeaderText="Contact Tel." >
                      <ItemStyle Width="150px" />
                      </asp:BoundField>
                    
                      <asp:TemplateField HeaderText="Department">
                      <ItemStyle    />
                          <ItemTemplate >
                             <div >
                        <asp:Label ID="lblScore" runat="server" Text='<%# Eval("dept_name") %>'></asp:Label>
                        </div>
                          </ItemTemplate>
                      <FooterTemplate>
                       <div style="text-align:right">
                       <asp:Label ID="lblTotalScore" runat="server" Font-Bold="true" ></asp:Label>
                       </div>
                      </FooterTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Remark" DataField="reserve_remark" 
                          ItemStyle-Width="100px"  >
                   
<ItemStyle Width="100px"></ItemStyle>
                   
                      </asp:BoundField>
                     <asp:TemplateField>
                   
                  <ItemStyle ForeColor="Red" />
                  <ItemTemplate>
                                     
                      <asp:ImageButton ID="linkDelete" runat="server" CommandName="cancelCommand" 
                    CommandArgument="<%# Bind('reserve_id') %>" OnClientClick="return confirm('Are you sure you want to delete this item?')" ImageUrl="~/images/bin.png" ToolTip="Delete" />
                     
                        
                  </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <br />
                 <div>
                
              </div>
              </div>
</asp:Content>

