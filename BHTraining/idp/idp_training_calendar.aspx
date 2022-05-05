<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_training_calendar.aspx.vb" Inherits="idp_idp_training_calendar" %>

<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
    <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
    <script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

    <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
    <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_txttopic").autocomplete("../ajax_idp_course_list.ashx", {
                matchContains: false,
                autoFill: false,
                mustMatch: false
            });

            $('#ctl00_ContentPlaceHolder1_txttopic').result(function (event, data, formatted) {
                // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
                var serial = data;
                // alert("serial ::" + serial);

            });
        });
    </script>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="display: none;">
        <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left: 5px; cursor: pointer">
    </div>
    <div id="header">
        <img src="../images/doc01.gif" width="32" height="32" />&nbsp;&nbsp;<asp:Label ID="lblHeader"
            runat="server" Text="Training Calendar"></asp:Label>
    </div>
    <div align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
    <div id="data">

        <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
            <tr>
                <td class="theader">
                    <img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
            </tr>

            <tr>
                <td align="right">
                    <table width="100%" cellspacing="2" cellpadding="2">
                        <tr>
                            <td width="150">Date</td>
                            <td>
                                <asp:TextBox ID="txtdate1" runat="server" Width="80px"></asp:TextBox>
                                &nbsp; to
            <asp:TextBox ID="txtdate2" runat="server" Width="80px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td >Training Course</td>
                            <td>
                                <asp:TextBox ID="txttopic" runat="server" Width="700px"></asp:TextBox>
                            </td>

                        </tr>
                     
                        <tr>
                            <td >Training Course</td>
                            <td>
                                <asp:DropDownList ID="txtcourse_name" runat="server"
                                    DataTextField="internal_title" DataValueField="internal_title">
                                </asp:DropDownList>
                            </td>

                        </tr>
                     
                        <tr>
                            <td >Class Status&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="txtclass_status" runat="server" Visible="True">
                                    <asp:ListItem Value="">-- All Status --</asp:ListItem>
                                    <asp:ListItem Value="1">Open</asp:ListItem>
                                    <asp:ListItem Value="0">Close</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                           <tr>
                            <td >&nbsp;</td>
                            <td>
                                &nbsp;</td>

                        </tr>
                    </table>
                </td>
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
                    <asp:Button ID="cmdClear" runat="server" Text="Clear" CssClass="button-green2" />
                </td>
            </tr>
        </table>
        <br />
        <div class="small" style="margin-bottom: 3px; width: 98%">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="bottom"><span class="small">
                        <asp:Label ID="lblNum" runat="server" Text=""></asp:Label>
                        Records Found</span></td>
                    <td style="text-align: right;">&nbsp;</td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False" Width="98%" CellPadding="4"
            GridLines="None" CssClass="tdata3"
            AllowPaging="True" PageSize="100" RowHeaderColumn="position"
            AllowSorting="True" EmptyDataText="There is no data."
            EnableModelValidation="True">
            <RowStyle BackColor="#eef1f3" />
            <Columns>
                <asp:TemplateField>

                    <ItemTemplate>

                        <asp:Label ID="Labels" runat="server">
                 <%# Container.DataItemIndex + 1 %>.
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblPK" runat="server" Text='<%# Bind("schedule_id") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("idp_id") %>' Visible="false"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertTSToDateString(Eval("schedule_start_ts")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle ForeColor="White" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time">

                    <ItemTemplate>
                        <asp:Label ID="lblDateTS" runat="server" Text='<%# Eval("schedule_start_ts") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblStart1" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts"),"hour").PadLeft(2,"0") %>' />:
                     <asp:Label ID="lblStart11" runat="server" Text='<%# ConvertTSTo(Eval("schedule_start_ts"),"min").PadLeft(2,"0") %>' />
                        -
                        <asp:Label ID="lblEnd1" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts"),"hour").PadLeft(2,"0") %>' />:
                     <asp:Label ID="lblEnd11" runat="server" Text='<%# ConvertTSTo(Eval("schedule_end_ts"),"min").PadLeft(2,"0") %>' />
                    </ItemTemplate>
                    <HeaderStyle ForeColor="White" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Training Course">

                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("internal_title") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblDocument" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location">

                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("location") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle ForeColor="White" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bookings">
                    <ItemTemplate>
                        <asp:Label ID="lblBook" runat="server" Text='-'></asp:Label>
                    </ItemTemplate>

                    <ItemStyle Width="80px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Attendant">

                    <ItemTemplate>
                        <asp:Label ID="lblRegis" runat="server"></asp:Label>/<asp:Label ID="lblMax" runat="server" Text='<%# Eval("max_attendee") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblRegisText" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class Status">

                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("is_open") %>'></asp:Label>
                        <asp:Label ID="lblEmpRegister" runat="server" Text='<%# Bind("is_booking") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="140px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="">

                    <ItemTemplate>
                        <a href="ext_register.aspx?id=<%# Eval("idp_id") %>&sh_id=<%# Eval("schedule_id") %>&flag=<%Response.Write(flag)%>">
                            <asp:Label ID="lblRegisterForm" Text="Attendance Record" runat="server"></asp:Label></a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Course Evaluation">
                    <ItemTemplate>
                        <a href="idp_training_evaluation.aspx?id=<%# Eval("idp_id") %>&sh_id=<%# Eval("schedule_id") %>&flag=<%Response.Write(flag)%>">
                            <asp:Label ID="lblEvaluation" Text="Training Class Evaluation" runat="server"></asp:Label></a>
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
</asp:Content>
