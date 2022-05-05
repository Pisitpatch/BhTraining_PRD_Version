<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP2_MasterPage.master" AutoEventWireup="false" CodeFile="report_ladder.aspx.vb" Inherits="idp_report_ladder" %>
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
<table width="100%" cellpadding="0" cellspacing="0" >
    <tr>
       
      <td><div id="header"><img src="../images/menu_03.png" width="32" height="32" /> &nbsp;
       <asp:Label ID="lblHeader" runat="server" Text="Report"></asp:Label>
     
       
          </div></td>
      <td>
          &nbsp;</td>
  </tr></table>
 <div id="data">
  <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
          <tr>
            <td width="550" colspan="4" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
          </tr>
          <tr>
            
            <td colspan="4"><table width="100%" cellpadding="2">
              <tr>
                <td width="120">&nbsp;</td>
                <td width="385">
                    &nbsp;</td>
                <td width="80">&nbsp;</td>
                <td>
                    &nbsp;</td>
              </tr>
              <tr>
                <td width="120">Date Range</td>
                <td width="385">
                    <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
          
          &nbsp;to&nbsp;<asp:TextBox 
                ID="txtdate2" runat="server" Width="80px"></asp:TextBox> &nbsp;</td>
                <td width="80">&nbsp;</td>
                <td>
                   
             
                &nbsp;
                                         
              
                 </td>
              </tr>
              <tr>
                <td width="120">&nbsp;</td>
                <td width="385">
                    <asp:DropDownList ID="txtdept" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Visible="false">
                    </asp:DropDownList>
             </td>
                <td width="80">&nbsp;</td>
                <td>
                    &nbsp;</td>
              </tr>
            
              </table>
           
              </td>
          </tr>
          <tr>
            <td colspan="4" align="right">
                <asp:Button ID="cmdSearch" runat="server" Text="Query report"  />
                &nbsp;<asp:Button 
                    ID="cmdExport" runat="server" Text="Export to Excel" />
              </td>
          </tr>
        </table>
        <br />
         <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="4" 
               GridLines="None" CssClass="tdata3" 
              EmptyDataText="There is no data." 
        EnableModelValidation="True" DataKeyNames="idp_id">
             <Columns>
                 <asp:TemplateField HeaderText="Ladder No.">
                   
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("idp_no") %>'></asp:Label>
                          <asp:Label ID="lblPK" runat="server" Text='<%# Bind("idp_id") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblStatusId" runat="server" Text='<%#Bind ("status_id")%>' Visible="false"></asp:Label> 
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="report_emp_code" HeaderText="Emp. Code" />
                 <asp:TemplateField HeaderText="Emp. Name">
                    
                     <ItemTemplate>
                         <asp:Label ID="lblName" runat="server" Text='<%# Bind("report_by") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Job Title">
                    
                     <ItemTemplate>
                         <asp:Label ID="lblJobTitle" runat="server"  Text='<%# Bind("report_jobtitle") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cost Center ID">
                     
                     <ItemTemplate>
                         <asp:Label ID="lblDeptID" runat="server"  Text='<%# Bind("report_dept_id") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cost Center Name">
                    
                     <ItemTemplate>
                         <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("report_dept_name") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="plan_month" HeaderText="Month" 
                     NullDisplayText="0" />
                 <asp:BoundField DataField="plan_year" HeaderText="Year" />
                 <asp:BoundField DataField="status_name" HeaderText="Status" />
                 <asp:BoundField DataField="date_submit" HeaderText="Submit date" />
                 <asp:BoundField HeaderText="Ladder Form Name" 
                     DataField="ladder_template_name" />
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
                     <asp:TemplateField HeaderText="Educator 1">
                      <ItemTemplate>
                          <asp:Label ID="lblImage6" runat="server" Text="-"></asp:Label>
                         
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Educator 2">
                      <ItemTemplate>
                          <asp:Label ID="lblImage7" runat="server" Text="-"></asp:Label>
                      
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Educator 3">
                      <ItemTemplate>
                          <asp:Label ID="lblImage8" runat="server" Text="-"></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                      <asp:TemplateField HeaderText="Educator review">
                      <ItemTemplate>
                          <asp:Label ID="lblReview" runat="server" Text="-"></asp:Label>
                        
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
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

