<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="ext_register.aspx.vb" Inherits="idp_ext_register" MaintainScrollPositionOnPostback="true" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 21px;
        }
     
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_txtbarcode").focus();
        });
    </script>

       <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {

          $("#ctl00_ContentPlaceHolder1_txtname").autocomplete("../ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtname').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);

              var serial = Array();
              // alert(data[1]);
              serial = data[1].split("#");
              // alert("serial ::" + serial[1]);
              $('#ctl00_ContentPlaceHolder1_txtempcode').val(serial[0]);
              $('#ctl00_ContentPlaceHolder1_txtdept').val(serial[3]);
              $('#ctl00_ContentPlaceHolder1_txtjobtitle').val(serial[2]);

          });

          $("#ctl00_ContentPlaceHolder1_txtname").click(function () {
              $(this).select();
          });


      });

      function myFocus() {
          if ($('#ctl00_ContentPlaceHolder1_txtbarcode').val() != "") {
             // alert("xxx");
          }
      }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="../images/menu_03.png" width="32" height="32" alt="x"   /> <asp:Label
                      ID="lblHeader" runat="server" Text=""></asp:Label></div>
<div id="data">
  <table width="98%">
     <tr>
                <td valign="top" bgcolor="#eef1f3" width="180" style="font-weight:bold">Attendance type</td>
                <td valign="top" bgcolor="#eef1f3" style="color:Red">    
        <asp:DropDownList ID="txttype" runat="server">
            <asp:ListItem Value="1">ผู้เข้าอบรม / Trainee</asp:ListItem>
            <asp:ListItem Value="2">วิทยากร / Speaker</asp:ListItem>
        </asp:DropDownList>
                </td>
              </tr>
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="180"><strong>Register Participants</strong></td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:TextBox ID="txtbarcode" runat="server" BackColor="#FFFF66" 
                         ></asp:TextBox> (รหัสพนักงาน)
&nbsp; <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbarcode"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator></td>
              </tr>
     
     
      <tr>
                <td valign="top" bgcolor="#eef1f3" ><strong>Training Hour</strong></td>
                <td valign="top" bgcolor="#eef1f3" style="color:Red">    
                    <asp:TextBox ID="txttrain_hour" runat="server"></asp:TextBox>
                </td>
              </tr>
     
     
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="180" style="color:Red">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3" style="color:Red">    
                    <asp:Button ID="Button1" runat="server" Text="ลงทะเบียน" />
                    &nbsp;(สำหรับการลงทะเบียนด้วยระบบ Barcode โดยใช้รหัสพนักงาน)</td>
              </tr>
     
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="180" style="color:Red">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3" >    
                    <asp:FileUpload ID="FileUploadCSV" runat="server" Width="200px" />
                    &nbsp;<asp:Button ID="cmdUploadCSV" runat="server" Text="Upload" 
                        style="height: 26px" /> (ไฟล์นามสกุล .csv)
                    <a href="employee.csv">ตัวอย่างรูปแบบไฟล์ที่ใช้อัพโหลด (รหัสพนักงาน , ชื่อพนักงาน)</a></td>
              </tr>
     
    </table>
    <div style="width:100%">
    <table width="100%">
    <tr>
    <td width="180">
        <b>Training Course Topic</b></td>
    <td>
        <asp:Label ID="lblTopic" runat="server" Text="Label"></asp:Label>
    </td>

    </tr>
    <tr>
    <td style="vertical-align:top"><b>Course detail</b></td><td>
        <asp:Label ID="lblDetail" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td><b>Schedule</b></td><td>
        <asp:Label ID="lblSchedule" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td><b>Speaker</b></td><td>
        <asp:Label ID="lblSpeaker" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
    <td><b>Location</b></td><td>
        <asp:Label ID="lblLocation" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td><b>Traning Type</b></td><td>
        <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="style2"><b>Status</b></td><td class="style2">
        <asp:Label ID="lblStatus" runat="server" Text="Open"></asp:Label>
        </td>
    </tr>
    <tr>
    <td><b>Training Document</b></td><td>
        <asp:Label ID="lblDocument" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    </table>
    </div>
    <br />
    
   
    <div style="width:100%">
    <table width="100%">
    <tr>
    <td> <div>Found <asp:Label ID="lblNum" runat="server" Text="-"></asp:Label> records.
    <br /><asp:Label ID="lblInfo" runat="server" Text="-"></asp:Label>
    </div></td>
    <td  style="text-align:right">   <asp:Button ID="cmdRegister" runat="server" 
            Text="ลงทะเบียนโดยไม่ใช้ Barcode" Font-Bold="true" Visible="False" />
               <asp:Button ID="cmdDelete" runat="server" Text="ลบรายชื่อที่การทะเบียนไว้แล้ว" ForeColor="blue" OnClientClick="return confirm('Are you sure you want to delete ?')" />
        <asp:Button ID="cmdUnRegister" runat="server" Text="ยกเลิกการลงทะเบียน" ForeColor="Red" OnClientClick="return confirm('Are you sure you want to cancel ?')" /></td>
    </tr>
    </table>
     
    </div>
         <asp:GridView ID="GridRegister" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                   <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                  <asp:TemplateField>
                  <HeaderStyle Width="30px" />
                   <HeaderTemplate>
                   
                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" OnCheckedChanged="onCheckAll" AutoPostBack="True" />
            </HeaderTemplate>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("register_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chk" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                      <asp:BoundField HeaderText="Attendance type" DataField="attendance_type_name" />
                      <asp:BoundField HeaderText="Employee code" DataField="emp_code" />
                  <asp:TemplateField HeaderText="Employee">
                  
                    <ItemTemplate>
                     
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label>
                       </ItemTemplate>
                  </asp:TemplateField>
                      <asp:BoundField HeaderText="Department"  DataField="dept_name" />
                      <asp:BoundField HeaderText="Job title"  DataField="job_title" />
                      <asp:TemplateField HeaderText="Target Group">
                          <ItemTemplate>
                              <asp:Label ID="lblTarget" runat="server" Text='<%# Bind("target") %>'></asp:Label>
                          </ItemTemplate>
                      
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Registered">
                          <ItemTemplate>
                              <asp:Label ID="lblIsRegister" runat="server"></asp:Label>
                                <asp:Label ID="lblRegistered" runat="server" Text='<%# Eval("is_register") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                       
                      </asp:TemplateField>
                      <asp:BoundField DataField="training_hour" HeaderText="Training Hours" />
                      <asp:BoundField DataField="online" HeaderText="Online" />
                      <asp:TemplateField HeaderText="Date/time">
                          <ItemTemplate>
                           <asp:Label ID="lblDateTS" runat="server" Text='<%# Eval("register_time") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblDate" runat="server" Text=''></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                      <asp:BoundField DataField="register_by" HeaderText="Register by" />
                  </Columns>
                </asp:GridView>
</div>
</asp:Content>

