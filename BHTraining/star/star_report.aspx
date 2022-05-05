<%@ Page Title="" Language="VB" MasterPageFile="~/star/Star_MasterPage.master" AutoEventWireup="false" CodeFile="star_report.aspx.vb" Inherits="star_star_report" %>

<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="display: none;">
        <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left: 5px; cursor: pointer" />
    </div>

    <div id="data">
        <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
            <tr>
                <td width="550" colspan="4" class="theader">
                    <img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
            </tr>
            <tr>

                <td colspan="4">
                    <table width="100%" cellpadding="2">
                        <tr>
                            <td width="120">Report Type</td>
                            <td width="385">
                                <asp:DropDownList ID="txtreport" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="1">1. สรุป จำนวนผู้ได้รับดาวเด่นประจำเดือนประเภทบุคคลและทีม</asp:ListItem>
                                    <asp:ListItem Value="2">2. สรุป จำนวนแพทย์ที่ได้รับดาวเด่น</asp:ListItem>
                                    <asp:ListItem Value="3">3. สรุป จำนวนพนักงานประเภทบุคคลได้รับดาวเด่น</asp:ListItem>
                                    <asp:ListItem Value="4">4. สรุป จำนวนประเภททีมได้รับดาวเด่น</asp:ListItem>
                                    <asp:ListItem Value="5">5. สรุป ระยะเวลาการดำเนินการ</asp:ListItem>
                                    <asp:ListItem Value="6">6. สรุป จำนวนคะแนนที่พนักงานได้รับ (เฉพาะที่ Awarded แล้ว)</asp:ListItem>
                                    <asp:ListItem Value="7">7. สรุป รายชื่อผู้ใด้รับดาวเด่น (BI Way)</asp:ListItem>
                                    <asp:ListItem Value="8">8. สรุป รายชื่อผู้ใด้รับดาวเด่น (CoAST)</asp:ListItem>
                                    <asp:ListItem Value="9">9. สรุปจำนวนพนักงาน แพทย์ และแผนกที่ถูกชม</asp:ListItem>
                                    <asp:ListItem Value="10">10. สรุปจำนวนดาวเด่นตามประเภทผู้เสนอชื่อ</asp:ListItem>
                                    <asp:ListItem Value="11">11. สรุปรวมจำนวน CoAST ดาวเด่นประเภททีม</asp:ListItem>
                                    <asp:ListItem Value="12">12.  สรุปรวมจำนวน CoAST ดาวเด่นประเภทบุคคล</asp:ListItem>
                                    <asp:ListItem Value="13">13. รายงานรวมคะแนนดาวเด่น 100, 200 ตาม Quater</asp:ListItem>
                                        <asp:ListItem Value="14">14.   รายงานสรุปดาวเด่นประเภททีม Top 20</asp:ListItem>
                                      <asp:ListItem Value="15">15.  รายงานสรุปดาวเด่นประเภทบุคคล Top 20</asp:ListItem>
                                   
                                </asp:DropDownList>
                            </td>
                            <td width="80">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="120">Date Type</td>
                            <td width="385">
                                <asp:DropDownList ID="txtdate_type" runat="server">
                                    <asp:ListItem Value="1">Submitted Date</asp:ListItem>
                                    <asp:ListItem Value="2">Awarded Date</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="80">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="120">Date Range</td>
                            <td width="385">
                                <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>

                                &nbsp;to&nbsp;<asp:TextBox
                                    ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;</td>
                            <td width="80">&nbsp;</td>
                            <td>&nbsp;
                                         
              
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
                            <td>&nbsp;</td>
                        </tr>

                    </table>

                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="cmdSearch" runat="server" Text="Query report" CssClass="button-green2" />
                    &nbsp;<asp:Button
                        ID="cmdExport" runat="server" Text="Export to Excel" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" Width="98%" CellPadding="4"
            GridLines="None" CssClass="tdata3"
            EmptyDataText="There is no data."
            EnableModelValidation="True" DataKeyNames="star_id">
            <Columns>
                <asp:TemplateField HeaderText="Star No.">

                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("star_no") %>'></asp:Label>
                        <asp:Label ID="lblPK" runat="server" Text='<%# Bind("star_id") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nominee_type_name" HeaderText="Nominator type" />
                <asp:TemplateField HeaderText="Nominator name">

                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="country" HeaderText="Nation" />
                <asp:TemplateField HeaderText="Nominee Emp. ID">

                    <ItemTemplate>
                        <asp:Label ID="lblNomineeEmpCode" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Norminee name (Individual)">

                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Norminee Dept. name (Team)">

                    <ItemTemplate>
                        <asp:Label ID="lblNomineeDept" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nominee Dept. Id">
                    <ItemStyle VerticalAlign="Top" />
                    <ItemTemplate>
                        <asp:Label ID="lblNomineeDeptID" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="srp_point" HeaderText="Regonition Point"
                    NullDisplayText="0" />
                <asp:BoundField DataField="status_name" HeaderText="Status" />
                <asp:BoundField DataField="submit_date" HeaderText="Submit date" />
                <asp:BoundField DataField="award_date" HeaderText="Award Date" />
                <asp:BoundField DataField="feedback" HeaderText="From" />
                <asp:BoundField DataField="service_detail" HeaderText="รายละเอียดที่ท่านประทับใจในบริการ" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White"
                CssClass="theader" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="#eef1f3" />
            <AlternatingRowStyle BackColor="White" />

        </asp:GridView>

        <br />
        <asp:GridView ID="GridView2" runat="server" Width="98%" CellPadding="4"
            GridLines="None" CssClass="tdata3"
            EmptyDataText="There is no data."
            EnableModelValidation="True" Visible="False">
            <Columns>

                <asp:BoundField />
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

