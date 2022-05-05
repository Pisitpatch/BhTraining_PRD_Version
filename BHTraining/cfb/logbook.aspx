<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="logbook.aspx.vb" Inherits="cfb_logbook" %>

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


    function openPopup(irId, irNo) {
        window.open("popup_status.aspx?irId=" + irId + "&irNo=" + irNo, "List", "scrollbars=no,resizable=no,width=800,height=480");
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer">
</div>
<div id="header">&nbsp;&nbsp;CFB Log Book</div>
<div id="data">
<table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td height="30">From&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="60px"></asp:TextBox>
          
          &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="60px"></asp:TextBox>
            &nbsp;  
            <asp:DropDownList ID="txtselect" runat="server">
            <asp:ListItem Value="1">CFB Report Logbook</asp:ListItem>
        
                    <asp:ListItem Value="2">MCO  Logbook</asp:ListItem>
                  <asp:ListItem Value="3">Morning brief Dashboard  Logbook</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
    
      <tr>
        <td height="30"><asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="button-green2" />        
&nbsp;
<asp:Button ID="cmdReset" runat="server" Text="Reset" CssClass="button-green2" />&nbsp;

<asp:Button ID="cmdExport" runat="server" Text="Export to excel" />
         
                  </td>
      </tr>
      </table>
      <br />
      <div style="width:1000px; height:300px; overflow:auto" >
      <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="true"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              EmptyDataText="There is no data." 
        EnableModelValidation="True" DataKeyNames="ir_id">
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="#eef1f3" />
         <AlternatingRowStyle BackColor="White" />
      
</asp:GridView>
    </div>
</div>
</asp:Content>

