<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="internal_training_detail.aspx.vb" Inherits="idp_internal_training_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#ctl00_ContentPlaceHolder1_txtrelate_idpno").autocomplete("../getIDPNo.ashx", { matchContains: false,
            autoFill: false,
            mustMatch: false
        });

        $('#ctl00_ContentPlaceHolder1_txtrelate_idpno').result(function (event, data, formatted) {
            // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
            var serial = data;
            alert("serial ::" + serial);

        });


        $("#ctl00_ContentPlaceHolder1_txtrelate_idpno").click(function () {
            $(this).select();
        });
    });


  </script>

    <script type="text/javascript">

        /* Optional: Temporarily hide the "tabber" class so it does not "flash"
        on the page as plain HTML. After tabber runs, the class is changed
        to "tabberlive" and it will appear. */

        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        /*==================================================
        Set the tabber options (must do this before including tabber.js)
        ==================================================*/
        var tabberOptions = {

            'cookie': "tabber", /* Name to use for the cookie */

            'onLoad': function (argsObj) {
                var t = argsObj.tabber;
                var i;

                /* Optional: Add the id of the tabber to the cookie name to allow
                for multiple tabber interfaces on the site.  If you have
                multiple tabber interfaces (even on different pages) I suggest
                setting a unique id on each one, to avoid having the cookie set
                the wrong tab.
                */
                if (t.id) {
                    t.cookie = t.id + t.cookie;
                }

                /* If a cookie was previously set, restore the active tab */
                i = parseInt(getCookie(t.cookie));
                if (isNaN(i)) { return; }
                t.tabShow(i);
                // alert('getCookie(' + t.cookie + ') = ' + i);
            },

            'onClick': function (argsObj) {
                var c = argsObj.tabber.cookie;
                var i = argsObj.index;
                // alert('setCookie(' + c + ',' + i + ')');
                setCookie(c, i);
            }
        };

        /*==================================================
        Cookie functions
        ==================================================*/
        function setCookie(name, value, expires, path, domain, secure) {
            document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
        }

        function getCookie(name) {
            var dc = document.cookie;
            var prefix = name + "=";
            var begin = dc.indexOf("; " + prefix);
            if (begin == -1) {
                begin = dc.indexOf(prefix);
                if (begin != 0) return null;
            } else {
                begin += 2;
            }
            var end = document.cookie.indexOf(";", begin);
            if (end == -1) {
                end = dc.length;
            }
            return unescape(dc.substring(begin + prefix.length, end));
        }
        function deleteCookie(name, path, domain) {
            if (getCookie(name)) {
                document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
            }
        }

</script>
<script type="text/javascript">
    function editDetail(cid) {
        window.open('popup_editcomment.aspx?id=<%response.write(id) %>&cid=' + cid, '', 'alwaysRaised,scrollbars =yes,status=yes,width=800,height=600');
    }

    function addComment() {
        /*
        if ($("#ctl00_ContentPlaceHolder1_txtcomment").val() == "") {
        alert("Please choose Comment Subject !");
        $("#ctl00_ContentPlaceHolder1_txtcomment").focus();
        return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txtcomment_detail").val() == "") {
        alert("Please enter Comment Detail !");
        $("#ctl00_ContentPlaceHolder1_txtcomment_detail").focus();
        return false;
        }
        */
        if ($("#ctl00_ContentPlaceHolder1_txtacknowedge_status").val() == "") {
            alert("Please choose Acknowledge !");
            $("#ctl00_ContentPlaceHolder1_txtacknowedge_status").focus();
            return false;
        }
    }
</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />


<script type="text/javascript">
    function validateExpense() {
        if ($("#ctl00_ContentPlaceHolder1_txtadd_expense_type").val() == "") {
            alert("กรุณาระบุประเภทค่าใช้จ่าย / Please choose expense type");
            $("#ctl00_ContentPlaceHolder1_txtadd_expense_type").focus();
            return false;
        }
    }

    function openDetail(id) {
        window.open('popup_training_file.aspx?id=<%response.write(id) %>&fid=' + id, '', 'alwaysRaised,scrollbars =yes,width=800,height=750');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="header"><img src="../images/doc01.gif" width="32" height="32" align="absmiddle" />&nbsp;&nbsp;Internal Training Requisition
          <div align="right">
          <input name="Button2" type="button" style="width: 150px;"    value="New" />
          <input name="input4" type="button" value="Save as Draft" style="width: 150px;"  />
 <input name="Button2" type="button" style="width: 150px;"  value="Submit Request" />
&nbsp;&nbsp;</div>
      </div>
    
      <div id="data">
    <div class="tabber" id="mytabber2">
            <div class="tabbertab">
              <h2>Staff Information</h2>
              <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                <tr>
                  <td valign="top"><span class="theader"><strong>Internal Training No.</strong></span></td>
                  <td><strong><asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label></strong></td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Division</strong></td>
                        <td width="230"><asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Cost Center</strong></td>
                        <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
              </table>
            </div>
        <div class="tabbertab" id="tab_approve" runat="server">
              <h2>Internal Training Approval and Comment</h2>
               <asp:Panel ID="panelManager1" runat="server" BorderStyle="None">
                  <div>
        <strong>Requester Level :</strong> <asp:Label ID="lblEmpLevel" runat="server"  /><br />
        <strong>Your Level :</strong> <asp:Label ID="lblYourLevel" runat="server"  /><br />
        <strong>Your Authorize :</strong> <asp:Label ID="lblMsg" runat="server" Font-Bold="true"  />
        </div>
            <table width="100%" cellspacing="1" cellpadding="2">
                <tr>
                  <td width="1022" colspan="5">
                      <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderStyle="None">
                          <Columns>
                              <asp:TemplateField>
                              <EditItemTemplate>
                              test
                              </EditItemTemplate>
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                                 
                                     <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td bgcolor="#DBE1E6">
                      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                          <tr>
                            <td width="156" valign="top" bgcolor="#DBE1E6"><strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label></strong></td>
                            <td width="180" valign="top"><strong><asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label></strong></td>
                            <td width="159" valign="top"><strong><asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label></strong></td>
                            <td width="189" valign="top"><strong><asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label></strong></td>
                            <td width="120"><strong>
                                <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label></strong></td>
                          </tr>
                      </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td bgcolor="#FFFFFF" width="250"><strong><asp:Label ID="lblCommentStatusId" runat="server" Text='<%# Bind("comment_status_id") %>' Visible="false" />
                            <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("comment_status_name") %>'></asp:Label> : Comment Subject</strong></td>
                            <td bgcolor="#FFFFFF">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label></td>
                            <td align="right"><div align="right"> 
                          
                               <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>   
                                  <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/comment_edit.png" ToolTip="Edit Comment" />
                                 <asp:ImageButton ID="cmdDelComment" runat="server" ImageUrl="~/images/comment_delete.png" OnCommand="onDeleteComment" CommandName="Delete" CommandArgument='<%# Bind("comment_id") %>' />
                            </div></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><p><asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label><br />
                              <br />
                      </p></td>
                    </tr>
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#336666"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                
                    <asp:Panel ID="panelAddComment" runat="server" Visible="false">
                    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top: 5px;">
                      <tr>
                        <td bgcolor="#DBE1E6"></td>
                        <td bgcolor="#DBE1E6">
                        
                        </td>
                      </tr>
                      <tr>
                        <td width="150" bgcolor="#eef1f3"><strong>Acknowledge</strong></td>
                        <td bgcolor="#eef1f3"><table width="100%" border="0">
                            <tr>
                              <td width="80">
                                  <asp:DropDownList ID="txtacknowedge_status" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">Approve</asp:ListItem>
                                    <asp:ListItem Value="2">Not Approve</asp:ListItem>
                                     <asp:ListItem Value="3">N/A</asp:ListItem>
                                  </asp:DropDownList>
                                  </td>
                              <td width="25">&nbsp;</td>
                              <td width="158"><strong>Comment Subject</strong></td>
                              <td>
                                 <asp:DropDownList ID="txtcomment" runat="server">
                                  <asp:ListItem Value="">- Please Select -</asp:ListItem>
                                   <asp:ListItem Value="1">เพื่อเพิ่มความรู้ในการปฏิบัติงาน</asp:ListItem>
                                    <asp:ListItem Value="2">แผนการพัฒนาไม่ตรงกับตำแหน่งงาน</asp:ListItem>
                                   
                                  </asp:DropDownList>
                            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3"><strong>Comment Detail</strong></td>
                        <td bgcolor="#eef1f3"><textarea name="txtcomment_detail" id="txtcomment_detail" cols="45" rows="5" style="width: 98%" runat="server"></textarea></td>
                      </tr>
                      <tr>
                        <td bgcolor="#eef1f3">&nbsp;</td>
                        <td bgcolor="#eef1f3"> 
                            
                            <asp:Button ID="cmdAddComment" runat="server" Text="Add Comment" OnClientClick="return addComment()"  />
                          </td>
                      </tr>
                    </table>
                    </asp:Panel>

                  <br /></td>
                </tr>
              </table>
            
              </asp:Panel>
            <br />
            </div>
       
          <div class="tabbertab" id="idp_log" runat="server">
              <h2>Review log</h2>
                <asp:GridView ID="GridviewIDPLog" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="log_status_id" 
        CellSpacing="1" CellPadding="2" AllowPaging="True" BorderWidth="0px" 
        BorderStyle="None" ShowFooter="True">
               <Columns>
                   <asp:BoundField DataField="idp_status_name" HeaderText="Action" >
                   <ItemStyle BackColor="#F8F8F8" />
                   </asp:BoundField>
            <asp:TemplateField HeaderText="By">
                <ItemTemplate>
                    
                     <asp:TextBox ID="txtLogReportby" ReadOnly="true" runat="server" Text='<%# Bind("log_create_by") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Position">
                <ItemTemplate>
                    
                      <asp:TextBox ID="txtLogPosition" ReadOnly="true" runat="server" Text='<%# Bind("position") %>'></asp:TextBox>
                </ItemTemplate>
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit/Dept">
                <ItemTemplate>
                   
                      <asp:TextBox ID="txtLogDept" ReadOnly="true" runat="server" Text='<%# Bind("dept_name") %>'></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    
                      <asp:TextBox ID="txtLogDate" ReadOnly="true" runat="server" Text='<%# Bind("log_time") %>'  DataFormatString="{0:dd MMM yyyy}"></asp:TextBox>
                </ItemTemplate>
               
            </asp:TemplateField>
            <asp:BoundField HeaderText="TAT" />
        </Columns>
                    <HeaderStyle BackColor="#EEF1F3" />
    </asp:GridView>
          
            <br />
            </div>
        </div>
        <br />
<div class="tabber" id="mytabber1">
          <div class="tabbertab">
            <h2>
              <legend><strong>Training Information</strong></legend>
            </h2>
            <table width="100%" cellspacing="1" cellpadding="2" >
              <tr>
                <td width="200" valign="top" bgcolor="#eef1f3"><strong>Training Course Topic<br />
                </strong></td>
                <td valign="top"><input type="text" name="textfield17" id="textfield3" style="width: 735px" />
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>IDP Program Reference No.</strong></td>
                <td valign="top"><input type="text" name="textfield17" id="textfield5" style="width: 275px" /></td>
              </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><strong>หลักการและเหตุผลในการจัดการฝึกอบรมสอดคล้องกับเป้าหมายของแผนกในข้อ
                  <input type="text" name="textfield7" id="textfield9" style="width: 130px" />
                </strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><label></label>
                  <label>
                  <strong>การฝึกอบรมจัดขึ้น เพื่อรองรับแผนพัฒนาบุคคล IDP ในด้าน</strong></label></td>
                </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                  <tr>
                    <td width="30" class="colname">&nbsp;</td>
                    <td width="30" class="colname"><strong>No</strong></td>
                    <td width="30" class="colname"><strong>R/E</strong></td>
                    <td class="colname"><strong>Categories</strong></td>
                    <td class="colname"><strong>Development   Topics</strong></td>
                    <td class="colname"><strong>Expected outcomes (วัตถุประสงค์ของการฝึกอบรมและพัฒนา)</strong></td>
                    <td width="100" class="colname"><strong>Methodology</strong></td>
                    <td width="80" class="colname"><strong>Measure<br />
                    วิธีการวัดผล</strong></td>
                    <td width="80" class="colname"><strong>Outcome</strong></td>
                    <td width="80" class="colname"><strong>Remark</strong></td>
                    </tr>
                  <tr>
                    <td valign="top" bgcolor="#CBEDED"><input type="checkbox" name="checkbox2" id="checkbox5" /></td>
                    <td valign="top" bgcolor="#CBEDED"><input name="textfield" type="text" id="textfield" style="width: 23px;" value="1" /></td>
                    <td valign="top" bgcolor="#CBEDED">R</td>
                    <td valign="top" bgcolor="#CBEDED">Functional</td>
                    <td valign="top" bgcolor="#CBEDED"> EKG for PT<br /></td>
                    <td valign="top" bgcolor="#CBEDED"> No incident report </td>
                    <td valign="top" bgcolor="#CBEDED">On-the-job Training</td>
                    <td valign="top" bgcolor="#CBEDED">สอบ</td>
                    <td valign="top" bgcolor="#CBEDED">ยกระดับ Career Ladder</td>
                    <td valign="top" bgcolor="#CBEDED">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top" bgcolor="#CBEDED"><label>
                      <input type="checkbox" name="checkbox3" id="checkbox6" />
                    </label></td>
                    <td valign="top" bgcolor="#CBEDED"><input name="textfield" type="text" id="textfield" style="width: 23px;" value="2" /></td>
                    <td valign="top" bgcolor="#CBEDED">R</td>
                    <td valign="top" bgcolor="#CBEDED">Functional</td>
                    <td valign="top" bgcolor="#CBEDED"> Evaluation and Exercise by Computerized Technique <br /></td>
                    <td valign="top" bgcolor="#CBEDED"> No incident report </td>
                    <td valign="top" bgcolor="#CBEDED">Training/Workshop</td>
                    <td valign="top" bgcolor="#CBEDED">สอบ</td>
                    <td valign="top" bgcolor="#CBEDED">ยกระดับ Career Ladder</td>
                    <td valign="top" bgcolor="#CBEDED">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="checkbox5" id="checkbox2" /></td>
                    <td valign="top"><input name="textfield" type="text" id="textfield11" style="width: 23px;" value="3" /></td>
                    <td valign="top">E</td>
                    <td valign="top">Personal</td>
                    <td valign="top">New Direction in Health Management<br /></td>
                    <td valign="top"> No incident report </td>
                    <td valign="top">Training/Workshop</td>
                    <td valign="top">สอบ</td>
                    <td valign="top">ยกระดับ Career Ladder</td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="checkbox5" id="checkbox3" /></td>
                    <td valign="top"><input name="textfield" type="text" id="textfield2" style="width: 23px;" value="4" /></td>
                    <td valign="top">E</td>
                    <td valign="top">Personal</td>
                    <td valign="top"> สมดุลการทรงตัว:Finding the Right Balance <br /></td>
                    <td valign="top"> No incident report </td>
                    <td valign="top">Self Study</td>
                    <td valign="top">สอบ</td>
                    <td valign="top">ยกระดับ Career Ladder</td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="checkbox5" id="checkbox4" /></td>
                    <td valign="top"><input name="textfield" type="text" id="textfield33" style="width: 23px;" value="1" /></td>
                    <td valign="top">E</td>
                    <td valign="top">Personal</td>
                    <td valign="top"> Working as a Team</td>
                    <td valign="top">ผู้ร่วมงานพึงพอใจ</td>
                    <td valign="top">On-the-job Training</td>
                    <td valign="top">สอบ</td>
                    <td valign="top">ยกระดับ Career Ladder</td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="checkbox5" id="checkbox16" /></td>
                    <td valign="top"><input name="textfield" type="text" id="textfield32" style="width: 23px;" value="2" /></td>
                    <td valign="top">E</td>
                    <td valign="top">Personal</td>
                    <td valign="top"> วิธีการจัดการกับคำตำหนิของลูกค้า<br /></td>
                    <td valign="top">จัดการปัญหาได้</td>
                    <td valign="top">Training/Workshop</td>
                    <td valign="top">สอบ</td>
                    <td valign="top">ยกระดับ Career Ladder</td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="checkbox5" id="checkbox15" /></td>
                    <td valign="top"><input name="textfield" type="text" id="textfield4" style="width: 23px;" value="3" /></td>
                    <td valign="top">E</td>
                    <td valign="top">Personal</td>
                    <td valign="top"> เทคนิคการเผชิญกับพฤติกรรมที่มี ปัญหาของคน<br /></td>
                    <td valign="top">จัดการปัญหาได้</td>
                    <td valign="top">Training/Workshop</td>
                    <td valign="top">สอบ</td>
                    <td valign="top">ยกระดับ Career Ladder</td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                  <tr>
                    <td valign="top">&nbsp;</td>
                    <td valign="top">&nbsp;</td>
                    <td colspan="3" valign="top"><select name="select" id="select" style="width: 100px">
                        <option value="Functional">Functional</option>
                        <option value="Leadership">Leadership</option>
                        <option value="Personal Excellence">Personal Excellence</option>
                        <option value="Unit Mandatory">Unit Mandatory</option>
                        <option value="Hospital Mandatory">Hospital Mandatory</option>
                      </select>
                        <select name="select15" id="select" style="width: 250px">
                          <option>Categories | Topic </option>
                          <option>-- Select --</option>
                          <option>EKG for PT</option>
                          <option>Evaluation and Exercise by Computerized Technique </option>
                          <option>New Direction in Health Management</option>
                          <option>สมดุลการทรงตัว:Finding the Right Balance </option>
                          <option>Cardiac Rehabilitation Trainer</option>
                          <option>HA National Forum </option>
                        </select>
                        <input type="submit" name="button11" id="button18" value=".." onclick="window.open('idp.html' , '', 'alwaysRaised,scrollbars =yes,width=1200,height=600') ;" />
                        <br />
                      เหมือน IDP</td>
                    <td valign="top"><select name="select" id="select22" style="width: 150px">
                        <option>Improve/maintain performance </option>
                        <option>Prepare for promotion</option>
                        <option>Support new services /quality standards</option>
                        <option>Other</option>
                      </select>
                        <br />
                      เหมือน IDP</td>
                    <td valign="top"><select name="select4" id="select8" style="width: 100px">
                        <option>- Please Select -</option>
                        <option>Coaching</option>
                        <option>Expert Briefing</option>
                        <option>Job Rotation</option>
                        <option>Mentoring</option>
                        <option>On-the-job Training</option>
                        <option>Project Assignment</option>
                        <option>Self Study</option>
                        <option>Workshop</option>
                        <option>Lecture</option>
                        <option>Work Shadowing</option>
                        <option>Other</option>
                                          </select>
                      เหมือน IDP</td>
                    <td valign="top"><input type="text" name="textfield5" id="textfield6" style="width: 100px" /></td>
                    <td valign="top"><select name="select14" id="select12" style="width: 100px">
                        <option>- Please Select -</option>
                        <option>ยกระดับ Career Ladder</option>
                        <option>คะแนน Test เพิ่มขึ้น 10%</option>
                        <option>Department KPT up 10%</option>
                        <option>Fallen rate Decrease</option>
                        <option>Medication Error Decrease</option>
                        <option>Customer Satisfaction Increase</option>
                    </select></td>
                    <td valign="top"><input type="text" name="textfield3" id="textfield8" style="width: 100px" /></td>
                    </tr>
                  <tr>
                    <td valign="top">&nbsp;</td>
                    <td valign="top">&nbsp;</td>
                    <td colspan="3" valign="top">&nbsp;</td>
                    <td valign="top">&nbsp;</td>
                    <td valign="top">&nbsp;</td>
                    <td valign="top"><input type="submit" name="button" id="button19" value="Add Row" /></td>
                    <td valign="top"><input type="submit" name="button13" id="button2" value="Delete" /></td>
                    <td valign="top">&nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Participants กลุ่มเป้าหมายผู้เข้าอบรม</strong></td>
                <td><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top"><strong>Employee List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td><input name="templatetitle2" type="text" id="templatetitle2" style="width: 200px" value="62752 : นที สรพิพัฒน์"/>
                                    <input type="submit" name="button3" id="button16" value="search" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="180"><select name="division" size="5" id="division" style="width: 300px">
                                    <option>62752 : นที สรพิพัฒน์</option>
                                </select></td>
                                <td width="60"><input type="submit" name="button3" id="button17" value="All  &gt;&gt;" style="padding: 1px 5px;width: 55px;" />
                                    <br />
                                    <input type="submit" name="button3" id="button30" value="Add  &gt;" style="padding: 1px 5px;width: 55px;" /></td>
                                <td width="230"><select name="division2" size="5" id="division2" style="width: 300px">
                                    <option>62752 : นที สรพิพัฒน์</option>
                                    <option>62753 : xxx xxxxx</option>
                                    <option>62754 : xxx xxxxx</option>
                                    <option>62755 : xxx xxxxx</option>
                                </select></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top"><strong>Cost Center List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td width="180"><select name="select9" size="5" id="select10" style="width: 300px">
                                    <option>Pharmacy - Ipd</option>
                                    <option>Pharmacy - Opd</option>
                                    <option>Physical Therapy</option>
                                    <option>Nutrition Support Department </option>
                                    <option>Emergency Room</option>
                                    <option>Ambulance Unit</option>
                                    <option>Heart Center</option>
                                    <option>Clinical Coordination Unit</option>
                                    <option>Horizon - Radio Oncology</option>
                                    <option>Horizon - Chemotherapy</option>
                                    <option>Health Screening Center</option>
                                    <option>Skin Center</option>
                                    <option>Fertility Center / IVF-Clinic</option>
                                    <option>Plastic Surgery Center</option>
                                    <option>Behavioral Health Center</option>
                                    <option>Medical Clinic 1</option>
                                </select></td>
                                <td width="60"><input type="submit" name="button14" id="button10" value="All  &gt;&gt;" style="padding: 1px 5px;width: 55px;" />
                                    <br />
                                    <input type="submit" name="button14" id="button24" value="Add  &gt;" style="padding: 1px 5px;width: 55px;" /></td>
                                <td width="230"><select name="select9" size="5" id="select11" style="width: 300px">
                                    <option>Pharmacy - Ipd</option>
                                    <option>Pharmacy - Opd</option>
                                    <option>Physical Therapy</option>
                                    <option>Nutrition Support Department </option>
                                    <option>Emergency Room</option>
                                    <option>Ambulance Unit</option>
                                    <option>Heart Center</option>
                                    <option>Clinical Coordination Unit</option>
                                    <option>Horizon - Radio Oncology</option>
                                    <option>Horizon - Chemotherapy</option>
                                    <option>Health Screening Center</option>
                                    <option>Skin Center</option>
                                    <option>Fertility Center / IVF-Clinic</option>
                                    <option>Plastic Surgery Center</option>
                                    <option>Behavioral Health Center</option>
                                    <option>Medical Clinic 1</option>
                                </select></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                      </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top"><strong>Job Type List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td><input name="templatetitle4" type="text" id="templatetitle4" style="width: 200px"/>
                                    <input type="submit" name="button9" id="button11" value="search" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="180"><select name="select6" size="5" id="select5" style="width: 300px">
                                    <option>Nursing Senior Assistant</option>
                                    <option>RN</option>
                                    <option>Patient Services</option>
                                    <option>Clinical Nurse Coordinator</option>
                                    <option>Pharmacist</option>
                                </select></td>
                                <td width="60"><input type="submit" name="button9" id="button15" value="All  &gt;&gt;" style="padding: 1px 5px;width: 55px;" />
                                    <br />
                                    <input type="submit" name="button9" id="button20" value="Add  &gt;" style="padding: 1px 5px;width: 55px;" /></td>
                                <td width="230"><select name="select5" size="5" id="select18" style="width: 300px">
                                    <option>Nursing Senior Assistant</option>
                                    <option>RN</option>
                                    <option>Patient Services</option>
                                    <option>Clinical Nurse Coordinator</option>
                                    <option>Pharmacist</option>
                                </select></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                      </tr>
                    <tr>
                      <td valign="top"><table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                        <tr>
                          <td width="150" valign="top"><strong>Job Title List</strong></td>
                          <td><table width="100%" cellspacing="0" cellpadding="0">
                              <tr>
                                <td><input name="templatetitle3" type="text" id="templatetitle3" style="width: 200px"/>
                                    <input type="submit" name="button15" id="button21" value="search" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="180"><select name="select7" size="5" id="select30" style="width: 300px">
                                    <option>Registered Nurse level 1</option>
                                    <option>Registered Nurse level 2</option>
                                    <option>Registered Nurse level 3</option>
                                    <option>Registered Nurse level 4</option>
                                    <option>Pharmacist</option>
                                    <option>Pharmacy Practice Specialist</option>
                                    <option>Pharmacy Store Officer</option>
                                    <option>Pharmacy Technician</option>
                                    <option>Supervisor, Pharmacy</option>
                                    <option>Assistant Pharmacist</option>
                                </select></td>
                                <td width="60"><input type="submit" name="button15" id="button22" value="All  &gt;&gt;" style="padding: 1px 5px;width: 55px;" />
                                    <br />
                                    <input type="submit" name="button15" id="button23" value="Add  &gt;" style="padding: 1px 5px;width: 55px;" /></td>
                                <td width="230"><select name="select17" size="5" id="select28" style="width: 300px">
                                    <option>Registered Nurse level 1</option>
                                    <option>Registered Nurse level 2</option>
                                    <option>Registered Nurse level 3</option>
                                    <option>Registered Nurse level 4</option>
                                    <option>Pharmacist</option>
                                    <option>Pharmacy Practice Specialist</option>
                                    <option>Pharmacy Store Officer</option>
                                    <option>Pharmacy Technician</option>
                                    <option>Supervisor, Pharmacy</option>
                                    <option>Assistant Pharmacist</option>
                                </select></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Course Outline</strong></td>
                <td valign="top"><textarea name="textarea7" id="textarea8" cols="45" rows="5" style="width: 735px"></textarea></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Course Outline File Attachment</strong></td>
                <td valign="top"><input type="file" name="fileField2" id="fileField2" />
                    <input type="submit" name="button8" id="button8" value="Add" /></td>
              </tr>
            </table>
          </div>
          <div class="tabbertab">
            <h2>
              <legend>Training Document</legend>
            </h2>
            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>Filename</strong></td>
                <td class="colname"><strong>Description</strong></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox4" id="checkbox7" /></td>
                <td valign="top"><input name="textfield14" type="text" id="textfield23" style="width: 23px;" value="1" /></td>
                <td valign="top"><a href="#">File.jpg</a></td>
                <td valign="top"><a href="#">Slide หัวข้อ วิธีการจัดการกับคำตำหนิของลูกค้า</a></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox4" id="checkbox8" /></td>
                <td valign="top"><input name="textfield14" type="text" id="textfield24" style="width: 23px;" value="2" /></td>
                <td valign="top"><a href="#">File.jpg</a></td>
                <td valign="top"><a href="#">Slide หัวข้อ วิธีการจัดการกับคำตำหนิของลูกค้า</a></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="file" name="fileField3" id="fileField3" /></td>
                <td valign="top"><label>
                <input name="textarea10" type="text" id="textarea10" style="width: 150px" value="" size="45" />
                </label>
                    <input type="submit" name="button16" id="button25" value="Save" />
                    <input type="submit" name="button16" id="button27" value="Delete" /></td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Speaker</strong></legend>
            </h2>
            <table width="100%" align="center" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="32" class="colname"><strong>No</strong></td>
                <td width="352" class="colname"><strong>Title</strong></td>
                <td width="352" class="colname"><strong>Name<br />
                </strong></td>
                <td width="352" class="colname"><strong>Surname</strong></td>
                <td width="352" class="colname"><strong>Company/ Institute</strong></td>
                <td width="150" class="colname"><strong>Tax ID</strong></td>
                <td width="107" class="colname"><strong>Remark</strong></td>
                <td width="107" class="colname"><strong>File</strong></td>
              </tr>
              <tr>
                <td valign="top"><input name="textfield2" type="text" id="textfield64" style="width: 23px;" value="1" /></td>
                <td valign="top">นาย</td>
                <td valign="top">Natee</td>
                <td valign="top">Sorapipat</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button4" id="button31" value=".." /></td>
              </tr>
              <tr>
                <td valign="top"><input name="textfield2" type="text" id="textfield65" style="width: 23px;" value="2" /></td>
                <td valign="top">นาย</td>
                <td valign="top">Natee</td>
                <td valign="top">Sorapipat</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button17" id="button32" value=".." /></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input name="textfield11" type="text" id="textfield20" style="width: 23px;" /></td>
                <td valign="top"><input name="textarea3" type="text" id="textarea9" style="width: 150px" value="" size="45" /></td>
                <td valign="top"><input name="textarea" type="text" id="textarea3" style="width: 150px" value="" size="45" /></td>
                <td valign="top"><input name="textarea5" type="text" id="textarea4" style="width: 150px" value="" size="45" /></td>
                <td valign="top"><input name="textarea8" type="text" id="textarea5" style="width: 150px" value="" size="45" /></td>
                <td valign="top"><input name="textarea9" type="text" id="textarea6" style="width: 150px" value="" size="45" /></td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button2" id="button14" value="Add" />
                  <input type="submit" name="button12" id="button" value="Delete" /></td>
                <td valign="top">&nbsp;</td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Training Schedule</strong></legend>
              </h2>
            <table width="100%" align="center" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="27" class="colname"><strong>รอบ</strong></td>
                <td width="158" class="colname"><strong>Training Date Start<br />
                </strong></td>
                <td width="135" class="colname"><strong>End</strong></td>
                <td width="151" class="colname"><strong>Time Start</strong></td>
                <td width="153" class="colname"><strong>End</strong></td>
                <td width="116" class="colname"><strong>Type</strong></td>
                <td width="133" class="colname"><strong>Location</strong></td>
                <td width="125" class="colname"><strong>Registrant</strong></td>
              </tr>
              <tr>
                <td valign="top"><input name="textfield12" type="text" id="textfield21" style="width: 23px;" value="1" /></td>
                <td valign="top">1 May 2011</td>
                <td valign="top">1 May 2011</td>
                <td valign="top">8.00</td>
                <td valign="top">16.00</td>
                <td valign="top">In House</td>
                <td valign="top">ห้องประชุมชั้น 11 ห้อง 1<a href="#"></a></td>
                <td valign="top"><a href="#"><span style="background: #e2e2e2;">
                  <input name="button20" type="submit" id="button33" value="รายชื่อผู้ลงทะเบียน" />
                </span></a></td>
              </tr>
              <tr>
                <td valign="top"><input name="textfield12" type="text" id="textfield22" style="width: 23px;" value="2" /></td>
                <td valign="top">2 May 2011</td>
                <td valign="top">2 May 2011</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">In House</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><a href="#"><span style="background: #e2e2e2;">
                  <input name="button19" type="submit" id="button26" value="รายชื่อผู้ลงทะเบียน" />
                </span></a></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="text" name="textfield13" id="textfield7" style="width: 100px; background: #0F0;"/>
                  <img src="../images/calendar.gif" alt="calendar" width="22" height="17" align="absmiddle" /></td>
                <td valign="top"><input type="text" name="textfield4" id="textfield12" style="width: 100px; background: #0F0;"/>
                  <img src="../images/calendar.gif" alt="calendar" width="22" height="17" align="absmiddle" /></td>
                <td valign="top"><select name="select8" id="select13">
                  <option>hh</option>
                  <option>00</option>
                  <option>01</option>
                  <option>02</option>
                  <option>03</option>
                  <option>04</option>
                  <option>05</option>
                  <option>06</option>
                  <option>07</option>
                  <option>08</option>
                  <option>09</option>
                  <option>10</option>
                  <option>11</option>
                  <option>12</option>
                  <option>13</option>
                  <option>14</option>
                  <option>15</option>
                  <option>16</option>
                  <option>17</option>
                  <option>18</option>
                  <option>19</option>
                  <option>20</option>
                  <option>21</option>
                  <option>22</option>
                  <option>23</option>
                </select>
:
<select name="select8" id="select14">
  <option>mm</option>
  <option>00</option>
  <option>01</option>
  <option>02</option>
  <option>03</option>
  <option>04</option>
  <option>05</option>
  <option>06</option>
  <option>07</option>
  <option>08</option>
  <option>09</option>
  <option>10</option>
  <option>11</option>
  <option>12</option>
  <option>13</option>
  <option>14</option>
  <option>15</option>
  <option>16</option>
  <option>17</option>
  <option>18</option>
  <option>19</option>
  <option>20</option>
  <option>21</option>
  <option>22</option>
  <option>23</option>
  <option>24</option>
  <option>25</option>
  <option>26</option>
  <option>27</option>
  <option>28</option>
  <option>29</option>
  <option>30</option>
  <option>31</option>
  <option>32</option>
  <option>33</option>
  <option>34</option>
  <option>35</option>
  <option>36</option>
  <option>37</option>
  <option>38</option>
  <option>39</option>
  <option>40</option>
  <option>41</option>
  <option>42</option>
  <option>43</option>
  <option>44</option>
  <option>45</option>
  <option>46</option>
  <option>47</option>
  <option>48</option>
  <option>49</option>
  <option>50</option>
  <option>51</option>
  <option>52</option>
  <option>53</option>
  <option>54</option>
  <option>55</option>
  <option>56</option>
  <option>57</option>
  <option>58</option>
  <option>59</option>
</select></td>
                <td valign="top"><select name="select16" id="select20">
                  <option>hh</option>
                  <option>00</option>
                  <option>01</option>
                  <option>02</option>
                  <option>03</option>
                  <option>04</option>
                  <option>05</option>
                  <option>06</option>
                  <option>07</option>
                  <option>08</option>
                  <option>09</option>
                  <option>10</option>
                  <option>11</option>
                  <option>12</option>
                  <option>13</option>
                  <option>14</option>
                  <option>15</option>
                  <option>16</option>
                  <option>17</option>
                  <option>18</option>
                  <option>19</option>
                  <option>20</option>
                  <option>21</option>
                  <option>22</option>
                  <option>23</option>
                </select>
:
<select name="select16" id="select21">
  <option>mm</option>
  <option>00</option>
  <option>01</option>
  <option>02</option>
  <option>03</option>
  <option>04</option>
  <option>05</option>
  <option>06</option>
  <option>07</option>
  <option>08</option>
  <option>09</option>
  <option>10</option>
  <option>11</option>
  <option>12</option>
  <option>13</option>
  <option>14</option>
  <option>15</option>
  <option>16</option>
  <option>17</option>
  <option>18</option>
  <option>19</option>
  <option>20</option>
  <option>21</option>
  <option>22</option>
  <option>23</option>
  <option>24</option>
  <option>25</option>
  <option>26</option>
  <option>27</option>
  <option>28</option>
  <option>29</option>
  <option>30</option>
  <option>31</option>
  <option>32</option>
  <option>33</option>
  <option>34</option>
  <option>35</option>
  <option>36</option>
  <option>37</option>
  <option>38</option>
  <option>39</option>
  <option>40</option>
  <option>41</option>
  <option>42</option>
  <option>43</option>
  <option>44</option>
  <option>45</option>
  <option>46</option>
  <option>47</option>
  <option>48</option>
  <option>49</option>
  <option>50</option>
  <option>51</option>
  <option>52</option>
  <option>53</option>
  <option>54</option>
  <option>55</option>
  <option>56</option>
  <option>57</option>
  <option>58</option>
  <option>59</option>
</select></td>
                <td valign="top"><strong>
                  <select name="select11" id="select3" style="width: 100px">
                    <option>In House</option>
                    <option>Outside</option>
                  </select>
                  </strong></td>
                <td valign="top"><strong>
                  <input type="text" name="textfield6" id="textfield16" style="width: 130px" />
                  </strong></td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button18" id="button35" value="Add" />
                    <input type="submit" name="button18" id="button36" value="Delete" /></td>
                <td valign="top">&nbsp;</td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Budget</strong></legend>
             เหมือน IDP</h2>
            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td width="150" class="colname"><strong>คำใช้จ่าย</strong></td>
                <td width="150" class="colname"><strong>จำนวนเงิน <span style="font-weight: bold">(Baht)</span></strong></td>
                <td class="colname"><strong>Type</strong></td>
                <td class="colname"><strong>Payment</strong></td>
                <td class="colname"><strong>Remark</strong></td>
                <td class="colname"><strong>File</strong></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox10" id="checkbox10" /></td>
                <td valign="top"><input name="textfield9" type="text" id="textfield13" style="width: 23px;" value="1" /></td>
                <td valign="top">อาหาร</td>
                <td valign="top">2000
                  <input type="submit" name="button7" id="button4" value=".." /></td>
                <td valign="top">เบิกล่วงหน้า<br /></td>
                <td valign="top">Check</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button7" id="button6" value=".." /></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox10" id="checkbox11" /></td>
                <td valign="top"><input name="textfield9" type="text" id="textfield14" style="width: 23px;" value="2" /></td>
                <td valign="top">วิทยากร</td>
                <td valign="top">2000
                  <input type="submit" name="button7" id="button7" value=".." /></td>
                <td valign="top">sponsor</td>
                <td valign="top">Cash</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button7" id="button9" value=".." /></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><select name="select3" id="select4" style="width: 150px">
                  <option>Speaker Fee</option>
                  <option>Equipment Expense</option>
                  <option>Food &amp; Beverage </option>
                  <option>Location Expense</option>
                  <option>Other</option>
                </select></td>
                <td valign="top"><textarea name="textarea4" id="textarea2" cols="45" rows="1" style="width: 150px"></textarea></td>
                <td valign="top"><select name="select2" id="select7" style="width: 60px">
                    <option>เบิกล่วงหน้า</option>
                    <option>ออกเอง</option>
                    <option>sponsor</option>
                </select></td>
                <td valign="top"><select name="select2" id="select6" style="width: 100px">
                    <option>Cash</option>
                    <option>Check</option>
                    <option>Credit</option>
                    <option>Draft</option>
                </select></td>
                <td valign="top"><textarea name="textarea4" id="textarea2" cols="45" rows="1" style="width: 150px"></textarea></td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3"><strong>Total Expense</strong></td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield9" id="textfield15" style="width: 150px" />
                  &nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td colspan="3" valign="top" bgcolor="#eef1f3"><input type="submit" name="button10" id="button12" value="Add Topic" />
                <input type="submit" name="button7" id="button13" value="Delete" /></td>
              </tr>
            </table>
            <br />
          </div><div class="tabbertab">
            <h2>
              <legend><strong>Expense</strong></legend>
             เหมือน IDP</h2>
            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td width="150" class="colname"><strong>คำใช้จ่าย</strong></td>
                <td width="150" class="colname"><strong>จำนวนเงิน <span style="font-weight: bold">(Baht)</span></strong></td>
                <td class="colname"><strong>Type</strong></td>
                <td class="colname"><strong>Payment</strong></td>
                <td class="colname"><strong>Remark</strong></td>
                <td class="colname"><strong>File</strong></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox10" id="checkbox10" /></td>
                <td valign="top"><input name="textfield9" type="text" id="textfield13" style="width: 23px;" value="1" /></td>
                <td valign="top">อาหาร</td>
                <td valign="top">2000
                  <input type="submit" name="button7" id="button4" value=".." /></td>
                <td valign="top">เบิกล่วงหน้า<br /></td>
                <td valign="top">Check</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button7" id="button6" value=".." /></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox10" id="checkbox11" /></td>
                <td valign="top"><input name="textfield9" type="text" id="textfield14" style="width: 23px;" value="2" /></td>
                <td valign="top">วิทยากร</td>
                <td valign="top">2000
                  <input type="submit" name="button7" id="button7" value=".." /></td>
                <td valign="top">sponsor</td>
                <td valign="top">Cash</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button7" id="button9" value=".." /></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><select name="select3" id="select4" style="width: 150px">
                  <option>Speaker Fee</option>
                  <option>Equipment Expense</option>
                  <option>Food &amp; Beverage </option>
                  <option>Location Expense</option>
                  <option>Other</option>
                </select></td>
                <td valign="top"><textarea name="textarea4" id="textarea2" cols="45" rows="1" style="width: 150px"></textarea></td>
                <td valign="top"><select name="select2" id="select7" style="width: 60px">
                    <option>เบิกล่วงหน้า</option>
                    <option>ออกเอง</option>
                    <option>sponsor</option>
                </select></td>
                <td valign="top"><select name="select2" id="select6" style="width: 100px">
                    <option>Cash</option>
                    <option>Check</option>
                    <option>Credit</option>
                    <option>Draft</option>
                </select></td>
                <td valign="top"><textarea name="textarea4" id="textarea2" cols="45" rows="1" style="width: 150px"></textarea></td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3"><strong>Total Expense</strong></td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield9" id="textfield15" style="width: 150px" />
                  &nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td colspan="3" valign="top" bgcolor="#eef1f3"><input type="submit" name="button10" id="button12" value="Add Topic" />
                <input type="submit" name="button7" id="button13" value="Delete" /></td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Part of TRD </strong></legend>
             เหมือน IDP</h2>
            <table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
              <tr>
                <td width="25">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td height="26">&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
            </table>
          </div>
          <div class="tabbertab">
            <h2>Additional Information</h2>
            <table width="100%" cellspacing="0" cellpadding="2" >
              <tr>
                <td width="150" bgcolor="#DBE1E6"><strong>By</strong></td>
                <td bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2" >
                    <tr>
                      <td width="156" valign="top"><strong> Division Manager </strong></td>
                      <td width="180" valign="top"><input name="textfield10" type="text" id="textfield10" style="width: 180px" value="Miss Usanee Haemwean " /></td>
                      <td width="159" valign="top"><input name="textfield10" type="text" id="textfield17" style="width: 150px" value="Nurse Manager" /></td>
                      <td width="189" valign="top"><input name="textfield10" type="text" id="textfield18" style="width: 180px" value="Ward 8 A" /></td>
                      <td width="120"><input name="textfield10" type="text" disabled="disabled" id="textfield19" style="width: 120px;" value="20 Oct 2010 13:40" readonly="readonly" /></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Information</strong></td>
                <td bgcolor="#eef1f3"><textarea name="textarea2" id="textarea" cols="45" rows="5" style="width: 98%"></textarea></td>
              </tr>
              <tr>
                <td bgcolor="#eef1f3">&nbsp;</td>
                <td bgcolor="#eef1f3"><img src="../images/comment_add.png" width="16" height="16" />
                    <input name="input12" type="reset" value="save" /></td>
              </tr>
            </table>
            <br />
          </div>
</div>
        <br />
        <br />
        <div align="right">
          <input name="Button" type="button" style="width: 150px;"    value="New" />
          <input name="input" type="button" value="Save as Draft" style="width: 150px;"  />
          <input name="Button" type="button" style="width: 150px;"  value="Submit Request" />
        </div>
      </div>      
</asp:Content>

