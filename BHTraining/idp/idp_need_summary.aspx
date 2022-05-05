<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_need_summary.aspx.vb" Inherits="idp_idp_need_summary" %>
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
<div id="header"><img src="../images/doc01.gif" width="32" height="32"  />&nbsp;&nbsp;Training Need Summary</div>
 <div align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div id="data">
 
<table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
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
      <tr>
        <td width="100" height="30">Topic name</td>
        <td width="300">
            <asp:TextBox ID="txttopic" runat="server"></asp:TextBox>
           </td>
        <td width="60">&nbsp;</td>
        <td width="238">
            &nbsp;</td>
        <td width="63">&nbsp;</td>
        <td>
            &nbsp;</td>
        </tr>
      <tr>
        <td width="100" height="30">Category</td>
        <td width="300"><asp:DropDownList ID="txtcategory" runat="server" 
                DataTextField="category_name_en" Width="200px"
                DataValueField="category_id">
            </asp:DropDownList>
           </td>
        <td width="60">Methodology</td>
        <td width="238">
            <asp:DropDownList ID="txtmethod" runat="server" DataTextField="method_name" Width="200px"
                DataValueField="method_id">
            </asp:DropDownList>
           </td>
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
    <td align="right"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="100">Employee</td>
        <td width="225">
            <asp:TextBox ID="txtemployee" runat="server"></asp:TextBox>
           </td>
        <td width="100">&nbsp;</td>
        <td>
            &nbsp;</td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td align="right">
    <div id="div_hr" runat="server">
    </div>
     </td>
  </tr>
  <tr>
    <td align="right">
    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />&nbsp;
     </td>
  </tr>
      </table>
        <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td valign="bottom"><span class="small">
                  <asp:Label ID="lblNum" runat="server" Text=""></asp:Label> Records Found</span></td>
              <td style="text-align: right;"> <asp:Button ID="cmdExport" runat="server" Text="Export by Topic to excel" /> <asp:Button ID="cmdExport2" runat="server" Text="Export by Employee to excel" />&nbsp;</td>
            </tr>
          </table>
        </div>
        <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              AllowPaging="True" PageSize="100" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
        EnableModelValidation="True">
              <RowStyle BackColor="#eef1f3" />
              <Columns>
                 <asp:TemplateField>
                  
                    <ItemTemplate>
                     
                   <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Category">
                      <ItemTemplate>
                  
                         <asp:Label ID="Label2" runat="server" Text='<%# Bind("category_name") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Topic">
                    
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("topic_name") %>'></asp:Label>
                      
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Methodology" >
              
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("methodology") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:BoundField DataField="report_jobtype" HeaderText="Job Type" />
                 
                  <asp:TemplateField HeaderText="No. Of IDP">
                      <ItemTemplate>
                          <a href='popup_training_sum.aspx?id=<%# Eval("topic_id") %>&name=<%#  HttpContext.Current.Server.UrlEncode(Eval("topic_name")) %>&method=<%# Eval("method_id") %>&category=<%# Eval("category_id") %>&jobtype=<%# Eval("report_jobtype") %>&planyear=<%# Eval("plan_year") %>&employee=<%# txtemployee.text %>&dept=<%# txtdept.selectedValue %>'  target="_blank"><asp:Label ID="Label1" runat="server" Text='<%# Bind("num") %>'></asp:Label></a>
                      </ItemTemplate>
                      
                      <ItemStyle Width="80px" />
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
</asp:Content>

