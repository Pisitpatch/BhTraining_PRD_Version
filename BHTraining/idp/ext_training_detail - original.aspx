<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="ext_training_detail.aspx.vb" Inherits="idp_ext_training_detail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
  <div style="display: none;"> <img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer"  /> </div>
    <div id="header">
        <table width="100%" cellpadding="2" cellspacing="1">
            <tr>
              <td><img src="../images/doc01.gif" alt="" width="32" height="32"  />&nbsp;&nbsp;<span class="style1">External Training Request</span> </td>
              <td><div align="right"> Status
                  <asp:DropDownList ID="txtstatus" runat="server" 
                    DataTextField="status_name" DataValueField="idp_status_id" Font-Bold="True" 
                    BackColor="Aqua" ForeColor="Blue" Width="285px"> </asp:DropDownList>
           &nbsp;<select name="select8" id="select10">
                        <option>EN</option>
                        <option>TH</option>
                      </select>
                        <asp:Button ID="cmdUpdateStatus" runat="server" Text="Update Status" />
              </div></td>
            </tr>
          </table>
          <div align="right">
          &nbsp;&nbsp;&nbsp;
&nbsp;
   <asp:Button ID="cmdSaveDraft1" runat="server" Text="Save as Draft" Width="150px" OnCommand="onSave" 
            CommandArgument="1" />
           
            &nbsp;
             <asp:Button ID="cmdSubmit1" runat="server" Text="Submit" Width="150px" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True"  />
  &nbsp;&nbsp;
        </div>
      </div>
<div id="data">
          <div class="tabber" id="mytabber2">
            <div class="tabbertab">
              <h2>Staff Information</h2>
              <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                <tr>
                  <td valign="top"><span class="theader"><strong>External Training No.</strong></span></td>
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
              <h2>External Training Approval and Comment</h2>
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
             <strong>Training Information</strong>
            </h2>
            <asp:Panel ID="panel_detail" runat="server">
            <table width="100%" cellspacing="0" cellpadding="5">
              <tr>
                <td valign="top" bgcolor="#DBE1E6"><strong>Title/หัวข้อที่ต้องการฝึกอบรม</strong></td>
                <td valign="top" bgcolor="#DBE1E6"><input type="text" name="txttitle" id="txttitle" style="width: 735px" runat="server" /></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>การเข้าร่วม</strong></td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:DropDownList ID="txttype" runat="server">
                    <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                    <asp:ListItem Value="1">ผู้เข้าอบรม</asp:ListItem>
                    <asp:ListItem Value="2">วิทยากร</asp:ListItem>
                    </asp:DropDownList>
               </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>แรงจูงใจในการเข้าอบรมหัวข้อนี้</strong></td>
                <td valign="top" bgcolor="#eef1f3"><p>
                    <label>
                    <input type="checkbox" name="checkbox3" id="chk_attend1" runat="server" />
                    </label>
                  หัวข้อนี้เป็นหัวข้อที่ระบุอยู่ในแผนพัฒนาตนเองของฉัน <strong>IDP No. </strong>
                    <asp:TextBox ID="txtrelate_idpno" runat="server" Width="100px"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtrelate_idpno"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
                    <asp:Button ID="Button1" runat="server" Text=".." Visible="false" />
                                   <br />
                  <input type="checkbox" name="checkbox4" id="chk_attend2" runat="server" />
                  เพื่อให้ตนเองสามารถปรับระดับ Career Ladder 
                    <asp:DropDownList ID="txtladder1" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                  
เป็นระดับ
  <asp:DropDownList ID="txtladder2" runat="server">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
<br />
                  <input type="checkbox" name="checkbox5" id="chk_attend3" runat="server" />
                  การพัฒนาในหัวข้อนี้ ฉันได้รับคำแนะนำจาก หัวหน้างานของฉัน<br />
                  <input type="checkbox" name="checkbox6" id="chk_attend4" runat="server" />
                  การพัฒนาในหัวข้อนี้ ฉันได้จาการประเมินความสามารถของตัวฉันเอง<br />
                  <input type="checkbox" name="checkbox7" id="chk_attend5" runat="server" />
                  ฉันต้องการเตรียมพร้อมตัวเองเพื่ออนาคตในหน้าที่การงานของตัวเองด้วยหัวข้อนี้<br />
                  <input type="checkbox" name="checkbox8" id="chk_attend6" runat="server" />
                  องค์กรของฉันเล็งเห็นว่าหัวข้อนี้เป็นเรื่องที่สำคัญ<br />
                  <input type="checkbox" name="checkbox9" id="chk_attend7" runat="server" />
                  อื่นๆ <span style="font-weight: bold">
                    <input type="text" name="textfield" id="txtattend_remark" style="width: 350px" runat="server" />
                  </span></p></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Course Outline</strong></td>
                <td valign="top" bgcolor="#eef1f3"><strong>
                  <textarea name="textarea3" id="txtcourse_detail" cols="45" rows="3" style="width: 735px" runat="server"></textarea>
                  <br />
                </strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>File Attachment</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table>
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top">
              <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload1" runat="server"  />
                      <asp:Button ID="cmdUpload"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdDeleteFile" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFile" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/attach_file/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView></td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Expected outcomes from this training</strong></td>
                <td valign="top" bgcolor="#eef1f3"><strong>
                  <textarea name="textarea4" id="txtexect_detail" cols="45" rows="3" style="width: 735px" runat="server"></textarea>
                </strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Training type</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335">
                          <asp:DropDownList ID="txttraintype" runat="server">
                          <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
                          <asp:ListItem Value="1">Meeting</asp:ListItem>
                          <asp:ListItem Value="2">Training</asp:ListItem>
                          <asp:ListItem Value="3">Seminar</asp:ListItem>
                          <asp:ListItem Value="4">Workshop</asp:ListItem>
                        
                          </asp:DropDownList>
                      </td>
                      <td width="80">&nbsp;</td>
                      <td>&nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td width="150" valign="top" bgcolor="#eef1f3"><strong>Facility<br />
                </strong></td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield18" id="txtfacility" style="width: 735px" runat="server" />
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>institution</strong></td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield3" id="txtinstitution" style="width: 735px" runat="server" /></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Place</strong></td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:TextBox ID="txtplace" runat="server" Width="100px"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Date</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                    <tr>
                      <td width="300"> <asp:TextBox ID="txtdate1" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                              <asp:MaskedEditExtender
                                TargetControlID="txtdate1" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate1" PopupButtonID="Image1">
                              </asp:CalendarExtender>  <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /> to
                       
                       <asp:TextBox ID="txtdate2" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                              <asp:MaskedEditExtender ID="MaskedEditExtender1"
                                TargetControlID="txtdate2" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate2" PopupButtonID="Image2">
                              </asp:CalendarExtender>  <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  />
                       </td>
                      <td width="40">Time</td>
                      <td>
                       <asp:DropDownList ID="txthour1" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin1" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
                        to
                        <asp:DropDownList ID="txthour2" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtmin2" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Training Hour</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="150">
                          <input type="text" name="txthour" id="txthour" style="width: 100px; text-align:right" runat="server" maxlength="5" />
                        &nbsp;&nbsp;hrs.</td>
                    </tr>
                </table></td>
              </tr>
              

              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                  <tr>
                    <td width="25"><asp:RadioButton ID="txtr1" runat="server" 
                            GroupName="register"  />
                      </td>
                    <td>Reservation made, Employee Training and Development Unit will provide cash for registration</td>
                  </tr>
                  <tr>
                    <td height="26">
                        <asp:RadioButton ID="txtr2" runat="server" GroupName="register" />
                      </td>
                    <td>Employee Training and Developement Department Unit will make reservation and provide cash for registration</td>
                  </tr>
                  <tr>
                    <td>
                        <asp:RadioButton ID="txtr3" runat="server" GroupName="register" />
                      </td>
                    <td>Reservation made and prepaid by Employee Training and Development Department Unit on&nbsp;&nbsp;
                        <asp:TextBox ID="txtdate_register" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                              <asp:MaskedEditExtender ID="MaskedEditExtender2"
                                TargetControlID="txtdate1" 
                                Mask="99/99/9999"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date" 
                                InputDirection="RightToLeft" 
                                AcceptNegative="Left" 
                               
                                ErrorTooltipEnabled="True" runat="server"/>
                              <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtdate_register" PopupButtonID="Image3">
                              </asp:CalendarExtender>  <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.gif" CssClass="mycursor"  /> </td>
                  </tr>
                </table></td>
              </tr>
            </table>
            </asp:Panel>
          </div>
          <div class="tabbertab">
            <h2>
            <strong>ค่าใช้จ่ายประมาณการ (Estimate budget)</strong>
            </h2>
            <asp:Panel ID="panel_expense" runat= "server">
            <asp:Panel ID="div_budget" runat="server" Enabled="false">
            <strong>For Accounting Manager</strong>
            <table width="90%">
            <tr><td width="150">Approve status</td>
            <td>
                <asp:DropDownList ID="txtbudget_status" runat="server">
                <asp:ListItem Value="">-- N/A --</asp:ListItem>
                <asp:ListItem Value="0">Not Approve</asp:ListItem>
                <asp:ListItem Value="1">Approve</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdBudgetSave" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to update status ?')" />
            </td></tr>
                <tr>
                    <td width="150">
                        Remark</td>
                    <td>
                        <asp:TextBox ID="txtbudget_remark" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        By</td>
                    <td>
                        <asp:Label ID="lblUpdateBy" runat="server" ForeColor="#000099" Text="-"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
           </asp:Panel>
               <asp:GridView ID="GridExpense" runat="server" CssClass="tdata" CellPadding="3" 
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                    DataKeyNames="expense_id" 
                    EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)">
                  <HeaderStyle BackColor="#CBEDED"  />
                  <Columns>
                      <asp:TemplateField>
                            <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll" AutoPostBack="True" />
            </HeaderTemplate>
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="30px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ค่าใช้จ่าย">
                          <ItemTemplate>
                              <asp:Label ID="lblExpensetypeID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblReqBudget" runat="server" Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label><br />
                              -  <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_by") %>' ForeColor="BlueViolet"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server" 
                                  Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงิน">
                         <ItemStyle HorizontalAlign="Right" />
                          <ItemTemplate>
                              &nbsp;                     
                              <asp:Label ID="lblValue" runat="server" Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                               &nbsp;<asp:Label ID="lblcurtype" runat="server" Text='<%# Eval("currency_type_name") %>'></asp:Label>
                               <asp:Label ID="lblcurtypeid" runat="server" Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" ForeColor="BlueViolet" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="อัตราแลกเปลี่ยน">
                        <ItemStyle HorizontalAlign="Right" />
                          <ItemTemplate>
                              <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="expense_request_type_name" HeaderText="ประเภท" />
                      <asp:BoundField DataField="expense_payment_name" HeaderText="วิธีชำระเงิน" />
                      <asp:BoundField HeaderText="หมายเหตุ" DataField="expense_remark" />
                       <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <input class="detailButton" name="btnDetail" 
                                          onclick='openDetail(<%# Eval("expense_id") %>)' 
                                          type="button" value="File"  />
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                              </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <asp:Panel ID="panel_add_expense" runat="server">
            <table width="100%" class="tdata">
                <%If GridExpense.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname" width="180"><strong>คำใช้จ่าย</strong></td>
                <td class="colname"><strong>จำนวนเงิน <span style="font-weight: bold">(Baht)</span></strong></td>
              </tr>
                <%end if %>
             <tr>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="180">
                    &nbsp;</td>
                <td valign="top">
                <table width="100%">
                <tr>
                <td width="150">ประเภทค่าใช้จ่าย</td>
                <td> 
                    <asp:DropDownList ID="txtadd_expense_type" runat="server" BackColor="#FFFF99">
                        <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                        <asp:ListItem Value="1">Registration Fees</asp:ListItem>
                        <asp:ListItem Value="2">Travel Expense</asp:ListItem>
                        <asp:ListItem Value="3">Hotel Expense</asp:ListItem>
                        <asp:ListItem Value="4">Meal Expense</asp:ListItem>
                        <asp:ListItem Value="5">Other</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                    <tr>
                        <td width="150">
                            จำนวน</td>
                        <td>
                            <asp:TextBox ID="txtadd_value" runat="server" BackColor="#FFFF99" 
                                Font-Bold="True" ForeColor="#000099" MaxLength="7" Width="250px">0</asp:TextBox>
                            &nbsp;<asp:DropDownList ID="txtcurrency" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">BAHT</asp:ListItem>
                                <asp:ListItem Value="2">USD</asp:ListItem>
                                <asp:ListItem Value="3">EUR</asp:ListItem>
                                <asp:ListItem Value="4">JPY</asp:ListItem>
                                <asp:ListItem Value="5">CNY</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtadd_value" Display="Dynamic" 
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ประเภทการเบิก</td>
                        <td><table width="100%" cellspacing="0" cellpadding="0">
                        <tr><td width="150">
                            <asp:DropDownList ID="txtreq_type" runat="server" DataTextField="request_name_th" DataValueField="request_id">
                          
                            </asp:DropDownList>
                        </td>
                        <td width="100">วิธีชำระเงิน</td>
                          <td> <asp:DropDownList ID="txtpayment_type" runat="server">
                          <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                         
                            </asp:DropDownList></td>
                        </tr>
                        </table></td>
                    </tr>
                    <tr>
                        <td>
                            อัตราแลกเปลี่ยน</td>
                        <td>
                            1   <asp:Label ID="lblcurrency0" runat="server" Text=""></asp:Label> ต่อ
                            <asp:TextBox ID="txtadd_exchange_rate" runat="server" Width="80px"></asp:TextBox>
                          บาท
                            <br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                        runat="server" ControlToValidate="txtadd_exchange_rate" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:CheckBox ID="txtadd_sponsor" runat="server" 
                                Text="อบรมโดยมีสปอนเซอร์สนับสนุน" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">
                            หมายเหตุ</td>
                        <td>
                            <asp:TextBox ID="txtadd_expense_remark" runat="server" Rows="3" TextMode="MultiLine" 
                                Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                   
                        </td>
              </tr>
              <tr>
                  
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddExpense" runat="server" Text="เพิ่มรายการ (Add new)" OnClientClick="return validateExpense();"  />
                     <asp:Button ID="cmdDelExpense" runat="server" Text="ลบรายการ (Delete)" 
                        CausesValidation="False" />
                      <asp:Button ID="cmdSaveOrder" runat="server" Text="Update Order" 
                        CausesValidation="False" Visible="False" />
               </td>
              </tr>
            
            </table>
            </asp:Panel>
            <div><strong>Total Budget</strong> :  <asp:Label ID="txttotal" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Baht <strong>Request 
                Budget</strong>  <asp:Label ID="txtrequest_budget" runat="server" Text="-" Font-Bold="True" 
                        Font-Size="18px" ForeColor="Red"></asp:Label> Bath
                        </div>
            </asp:Panel>
            <br />
          </div>
            <div class="tabbertab" id="tab_account_expense" runat="server">
            <h2>
           <strong>ค่าใช้จ่ายที่เบิกจริง (Actual Expense)nse)</strong>
            </h2>
                 <asp:Panel ID="panel_update_expense" runat="server" >
          
            <table width="90%">
            <tr><td width="150">Approve status</td>
            <td>
                <asp:DropDownList ID="txtexpense_status" runat="server">
                <asp:ListItem Value="">-- N/A --</asp:ListItem>
                <asp:ListItem Value="0">On process</asp:ListItem>
                <asp:ListItem Value="1">Completed</asp:ListItem>
                </asp:DropDownList>
                
            </td></tr>
                <tr>
                    <td width="150">
                        Remark</td>
                    <td>
                        <asp:TextBox ID="txtexpense_remark" runat="server" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                        By</td>
                    <td>
                        <asp:Label ID="lblUpdateby2" runat="server" ForeColor="#000099" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="150">
                      
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

           
             <asp:Button ID="cmdUpdateExpense" runat="server" Text="Update" />
             </asp:Panel>
             <br /><br />
                <asp:GridView ID="GridExpense2" runat="server" CssClass="tdata" CellPadding="3" 
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                    DataKeyNames="expense_id" 
                    EmptyDataText="ยังไม่มีการทำรายการ (There is no transaction)">
                  <HeaderStyle BackColor="#CBEDED"  />
                  <Columns>
                      <asp:TemplateField>
                             <HeaderTemplate>
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll2" AutoPostBack="True" />
            </HeaderTemplate>
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("expense_id") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblTopicID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="30px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ค่าใช้จ่าย">
                          <ItemTemplate>
                             <asp:Label ID="lblExpensetypeID" runat="server" Text='<%# Bind("expense_topic_id") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblReqBudget" runat="server" Text='<%# Bind("is_request_budget") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("expense_topic_name") %>'></asp:Label><br />
                              -  <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_by") %>' ForeColor="BlueViolet"></asp:Label>
                          </ItemTemplate>
                          <EditItemTemplate>
                              <asp:TextBox ID="TextBox1" runat="server" 
                                  Text='<%# Bind("expense_topic_name") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemStyle Width="180px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงิน">
                         <ItemStyle />
                          <ItemTemplate>
                              &nbsp;                     
                              <asp:Label ID="lblValue" runat="server" Text='<%# FormatNumber(Eval("expense_value"),2) %>'></asp:Label>
                               &nbsp;<asp:Label ID="lblcurtype" runat="server" Text='<%# Eval("currency_type_name") %>'></asp:Label>
                               <asp:Label ID="lblcurtypeid" runat="server" Text='<%# Eval("currency_type_id") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblConvertToBaht" runat="server" Font-Underline="true" ForeColor="BlueViolet" ></asp:Label>
                                
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="จำนวนเงินอนุมัติ">
                         
                          <ItemTemplate>
                              <asp:Label ID="lblApprove" runat="server"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="อัตราแลกเปลี่ยน">
                        
                          <ItemTemplate>
                              <asp:Label ID="lblExchange" runat="server" Text='<%# Eval("exchange_rate") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="วันที่ทำรายการ" DataField="create_date" />
                      <asp:TemplateField HeaderText="ผู้มาติดต่อ">
                      
                          <ItemTemplate>
                              <asp:TextBox ID="textPersonContact" runat="server" Text='<%# Bind("acc_receive_by") %>' Width="80px"></asp:TextBox>
                            
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="บันทึกโดย" DataField="acc_expense_by" />
                      <asp:TemplateField HeaderText="ประเภท">
                        
                          <ItemTemplate>
                              <asp:Label ID="lbltype_id" runat="server" Text='<%# Bind("expense_request_type_id") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblpayment_id" runat="server" Text='<%# Bind("expense_payment_id") %>' Visible="false"></asp:Label>
                              <asp:DropDownList ID="txttype" runat="server" Width="80px">
                              
                              <asp:ListItem Value="5">ชำระเงิน / Payment</asp:ListItem>
                              <asp:ListItem Value="4">รับคืน / Clear advance</asp:ListItem>
                              </asp:DropDownList>
                           
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="วิธีชำระเงิน">
                         
                          <ItemTemplate>
                                 <asp:DropDownList ID="txtpayment" runat="server" Width="80px">
                                  <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                           <asp:ListItem Value="4">โอนเงิน / Transfer</asp:ListItem>
                              </asp:DropDownList>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="expense_remark" HeaderText="หมายเหตุ" />
                      <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <input class="detailButton" name="btnDetail" 
                                          onclick='openDetail(<%# Eval("expense_id") %>)' 
                                          type="button" value="File"  />
                                  </ItemTemplate>
                                  <ItemStyle VerticalAlign="Top" />
                              </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <asp:Panel ID="panel_actual_expense" runat="server">
            <table width="100%" class="tdata">
                <%If GridExpense.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname" width="180"><strong>คำใช้จ่าย</strong></td>
                <td class="colname"><strong>จำนวนเงิน <span style="font-weight: bold">(Baht)</span></strong></td>
              </tr>
                <%end if %>
             <tr>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="30">&nbsp;</td>
                <td valign="top" width="180">
                    &nbsp;</td>
                <td valign="top">
                <table width="100%">
                <tr>
                <td width="150">ประเภทค่าใช้จ่าย</td>
                <td> <asp:DropDownList ID="txtadd_expense_type2" runat="server" BackColor="#FFFF99">
                    <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                    <asp:ListItem Value="1">Registration Fees</asp:ListItem>
                     <asp:ListItem Value="2">Travel Expense</asp:ListItem>
                    <asp:ListItem Value="3">Hotel Expense</asp:ListItem>
                    <asp:ListItem Value="4">Meal Expense</asp:ListItem>
                    <asp:ListItem Value="5">Other</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                    <tr>
                        <td width="150">
                            จำนวน</td>
                        <td>
                            <asp:TextBox ID="txtadd_value2" runat="server" BackColor="#FFFF99" 
                                Font-Bold="True" ForeColor="#000099" MaxLength="7" Width="250px">0</asp:TextBox>
                            &nbsp;<asp:DropDownList ID="txtcurrency2" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="1">BAHT</asp:ListItem>
                                <asp:ListItem Value="2">USD</asp:ListItem>
                                <asp:ListItem Value="3">EUR</asp:ListItem>
                                <asp:ListItem Value="4">JPY</asp:ListItem>
                                <asp:ListItem Value="5">CNY</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="txtadd_value2" Display="Dynamic" 
                                ErrorMessage="Please Enter Only Numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ประเภทการจ่าย</td>
                        <td><table width="100%" cellspacing="0" cellpadding="0">
                        <tr><td width="150">
                            <asp:DropDownList ID="txtreq_type2" runat="server" DataTextField="request_name_th" DataValueField="request_id">
                          
                            </asp:DropDownList>
                        </td>
                        <td width="100">วิธีชำระเงิน</td>
                          <td> <asp:DropDownList ID="txtpayment_type2" runat="server">
                          <asp:ListItem Value="1">เงินสด / Cash</asp:ListItem>
                          <asp:ListItem Value="2">บัตรเครดิต / Credit card</asp:ListItem>
                          <asp:ListItem Value="3">เช็ค / Check</asp:ListItem>
                           <asp:ListItem Value="4">โอนเงิน / Transfer</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        </table></td>
                    </tr>
                    <tr>
                        <td>
                            อัตราแลกเปลี่ยน</td>
                        <td>
                            1  <asp:Label ID="lblcurrency" runat="server" Text=""></asp:Label>  ต่อ
                            <asp:TextBox ID="txtadd_exchange_rate2" runat="server" Width="80px"></asp:TextBox>
                            บาท
                         <br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                        runat="server" ControlToValidate="txtadd_exchange_rate2" 
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^[0-9]*[.]?[0-9]+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ชื่อผู้รับเงิน</td>
                        <td>
                            <asp:TextBox ID="txtadd_receive" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top">
                            หมายเหตุ</td>
                        <td>
                            <asp:TextBox ID="txtadd_expense_remark2" runat="server" Rows="3" TextMode="MultiLine" 
                                Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                   
                        </td>
              </tr>
              <tr>
                  
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddExpense2" runat="server" Text="เพิ่มรายการ (Add new)"   />
                     <asp:Button ID="Button3" runat="server" Text="ลบรายการ (Delete)" 
                        CausesValidation="False" />
               </td>
              </tr>
            
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>จำนวนเงินที่อนุมัติ</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblApproveBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Red" Text="-"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>จำนวนเงินที่รับคืน</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="lblReturnBudget" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Red" Text="-"></asp:Label>
                        Baht</td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" valign="top">
                        &nbsp;</td>
                    <td bgcolor="#eef1f3" style="height:30px" valign="top">
                        <strong>จำนวนทั้งหมดที่จ่าย</strong></td>
                    <td bgcolor="#eef1f3" valign="top">
                        <asp:Label ID="txtactual_expense" runat="server" Font-Bold="True" 
                            Font-Size="18px" ForeColor="Red" Text="-"></asp:Label>
                        Baht</td>
                </tr>
            </table>
            </asp:Panel>
            </div>
          <div class="tabbertab" id="tab_trd" runat="server">
            <h2>
             Part of TRD        </h2>
            <table width="100%">
              <tr>
                <td colspan="2" valign="top"><table width="100%" cellspacing="0" cellpadding="4">
                    <tr>
                <td valign="top" bgcolor="#eef1f3"><table>
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top">
              <table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                      <asp:FileUpload ID="FileUpload2" runat="server"  />
                      <asp:Button ID="cmdTRDUpload"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdTRDDelete" runat="server" Text="Delete selected attachments" 
                      CausesValidation="False" />                      
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridFileTRD" runat="server" AutoGenerateColumns="False" 
              CellSpacing="1" CellPadding="2" BorderWidth="0px" DataKeyNames="trainging_file_id"
              Width="100%" ShowHeader="False">
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK1" runat="server" Text='<%# Bind("trainging_file_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chkSelect1" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField>
                    <EditItemTemplate>
                      <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("file_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Bind("file_path") %>' Visible="false"></asp:Label>
                      <a href="../share/idp/hr/<%# Eval("file_path") %>" target="_blank">
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("file_name") %>'></asp:Label>
                      </a> </ItemTemplate>
                  </asp:TemplateField>
                  </Columns>
                </asp:GridView></td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
                    <tr>
                      <td height="24" valign="top" bgcolor="#DBE1E6"><table width="100%" cellspacing="1" cellpadding="2">
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6"><strong>Private Note</strong></td>
                          <td valign="top" bgcolor="#DBE1E6"><strong>
                            <textarea name="txttrd_note" id="txttrd_note" cols="45" rows="3" style="width: 450px" runat="server"></textarea>
                          </strong></td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6"><strong>Reply Message</strong></td>
                          <td valign="top" bgcolor="#DBE1E6">
                <asp:TextBox ID="txtreply" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="450px" BackColor="#CCFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6">Last Update</td>
                          <td valign="top" bgcolor="#DBE1E6">
                        <asp:Label ID="lblLastUpdate" runat="server" Text="lblLastUpdate"></asp:Label> &nbsp;
                        <asp:Label ID="lblreply_by" runat="server" Text="lblreply_by"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                          <td width="150" valign="top" bgcolor="#DBE1E6">&nbsp;</td>
                          <td valign="top" bgcolor="#DBE1E6">
                        <asp:Button ID="cmdReply" runat="server" Text="Update Reply" />
                            </td>
                        </tr>
                      </table></td>
                      </tr>
                    
                </table></td>
              </tr>
            </table>

             <asp:Panel ID="panel_reply" runat="server">
  
            
            </asp:Panel>
            </div>
         
  <div class="tabbertab" id="tab_update" runat="server">
  <h2>ความเห็นเพิ่มเติม (Additional Comment)</h2>
     <asp:GridView ID="GridInformation" runat="server" AutoGenerateColumns="False" 
                          CellPadding="4" Width="95%" ForeColor="#333333" 
        GridLines="None" EnableModelValidation="True" DataKeyNames="inform_id">
                          <RowStyle BackColor="#E3EAEB" />
                          <Columns>
                              <asp:TemplateField HeaderText="No" SortExpression="date">
                                 <ItemStyle Width="40px" />
                                  <ItemTemplate>
                                     <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:BoundField HeaderText="Date/Time" DataField="inform_date" ItemStyle-Width="120px" />
                              <asp:BoundField DataField="inform_detail" HeaderText="Information update" ItemStyle-Width="400px" />
                              <asp:BoundField DataField="inform_by" HeaderText="By" />
                              <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:Label ID="lblEmpNo" runat="server" Text='<%# Bind("inform_emp_code") %>' Visible="false"></asp:Label>
                                      <asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" 
                                          CommandName="Delete" Text="Delete"></asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="80px" />
                              </asp:TemplateField>
                          </Columns>
                          <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                          <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                          <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                          <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                          <EditRowStyle BackColor="#7C6F57" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                      <br />
    <table width="100%">
    <tr>
    <td width="150" style="vertical-align:top">Additional information</td>
    <td style="vertical-align:top">
        <asp:TextBox ID="txtadd_update" runat="server" Rows="4" TextMode="MultiLine" 
            Width="90%"></asp:TextBox></td>
    </tr>
    <tr>
    <td width="150">&nbsp;</td>
    <td>
        <asp:Button ID="cmdAddUpdate" runat="server" Text="Add more information" 
            CausesValidation="False" />
        </td>
    </tr>
    </table>
  
      <br />
</div>
        </div>
        <br />
        <br />
        <div align="right">
          &nbsp;&nbsp;&nbsp;
&nbsp;
 <asp:Button ID="cmdSaveDraft2" runat="server" Text="Save as Draft" Width="150px" OnCommand="onSave" 
            CommandArgument="1" />
           
            &nbsp;
             <asp:Button ID="cmdSubmit2" runat="server" Text="Submit" Width="150px" OnCommand="onSave" 
            CommandArgument="2" Font-Bold="True"  />
&nbsp;&nbsp;
</div>
      </div>
</asp:Content>

