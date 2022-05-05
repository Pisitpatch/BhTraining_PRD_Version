<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_review_score.aspx.vb" Inherits="game_admin_review_score" %>

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
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer">
</div>
<table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" alt="web" />&nbsp;&nbsp;
                <a href="admin_test_master.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="> Review"></asp:Label>
                </td>
            <td align="right" style="padding-right: 15px;">
                &nbsp;</td>
        </tr>
    </table>
  <table width="600" cellpadding="3" cellspacing="0" class="tdata3">
          <tr>
            <td colspan="4" class="theader">Search</td>
          </tr>
          <tr>
            
            <td colspan="4"><table width="100%" cellpadding="2">
              <tr>
                <td width="150">Report Type</td>
                <td width="385">
                    <asp:DropDownList ID="txtreport" runat="server">
                    <asp:ListItem Value="1">1. รายงานจำนวนครั้งที่ใช้ในการตอบคำถามจนถูก</asp:ListItem>
                     <asp:ListItem Value="2">2. รายงานสถิติของตัวเลือกคำตอบที่ถูกเลือกตอบ</asp:ListItem>
                   
                        <asp:ListItem Value="3">3. รายงานคะแนนพนักงานแยกตามวันที่ (มีรายชื่อซ้ำตามการใช้งานจริง)</asp:ListItem>
                    <asp:ListItem Value="4">4. รายงานจำนวนพนักที่เข้าสอบแยกตามแผนก</asp:ListItem>
                    <asp:ListItem Value="5">5. รายงานคะแนนเฉลี่ยแยกตามชุดข้อสอบและแผนก</asp:ListItem>
                     <asp:ListItem Value="6">6. รายงานคะแนนเฉลี่ยแยกตามแผนกและกลุ่มเป้าหมาย</asp:ListItem>
                       <asp:ListItem Value="7">7. รายงานจำนวนพนักงานที่เข้าสอบแยกตาม Job Title</asp:ListItem>
                      <asp:ListItem Value="8">8. รายงานแบบประเมินความพึงพอใจ</asp:ListItem>
                       <asp:ListItem Value="9">9. รายงานคะแนนพนักงานทั้งหมด (ไม่มีรายชื่อซ้ำ)</asp:ListItem>

                    </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                <td width="120">Group of questions</td>
                <td width="385">
                    <asp:DropDownList ID="txtgroup" runat="server" DataTextField="group_name_th" 
                        DataValueField="group_id">
                    </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                <td width="120">Department</td>
                <td width="385">
                    <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
                        DataValueField="dept_id">
                    </asp:DropDownList>
                  </td>
              </tr>
            
              <tr>
                <td width="120">From&nbsp;</td>
                <td width="385">&nbsp;<asp:TextBox ID="txtdate1" runat="server" Width="60px"></asp:TextBox>
          
          &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="60px"></asp:TextBox>
                    &nbsp;</td>
              </tr>
            
              <tr>
                <td width="120">&nbsp;</td>
                <td width="385">
                <asp:Button ID="cmdSearch" runat="server" Text="Query report" CssClass="button-green2" />
                <asp:Button 
                    ID="cmdExport" runat="server" Text="Export to Excel" />
                  </td>
              </tr>
            
              </table>
           
              </td>
          </tr>
        
        </table>
        <br />
   <asp:GridView ID="GridView2" runat="server"  Width="100%" CellPadding="4" 
               GridLines="None" CssClass="box-content"
              EmptyDataText="There is no data." 
        EnableModelValidation="True">
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
       
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="#eef1f3" />
         <AlternatingRowStyle BackColor="#99ffcc" />
      
</asp:GridView>
</asp:Content>

