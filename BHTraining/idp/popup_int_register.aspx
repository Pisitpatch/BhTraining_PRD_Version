<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_int_register.aspx.vb" Inherits="idp_popup_int_register" %>
<%@ Import Namespace="ShareFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>IDP File</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
      <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {

          $("#txtname").autocomplete("../ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#txtname').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);

              var serial = Array();
             // alert(data[1]);
              serial = data[1].split("#");
              // alert("serial ::" + serial[1]);
              $('#txtempcode').val(serial[0]);
              $('#txtdept').val(serial[3]);
              $('#txtjobtitle').val(serial[2]);

          });

          $("#txtname").click(function () {
              $(this).select();
          });


      });


  </script>
  <script type="text/javascript">
      function closePopup() {
          window.opener.disablePopup();
          window.close();
      }
      </script>
</head>
<body onunload="closePopup();">
    <form id="form1" runat="server">
    <div>
     <table width="98%">
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="120">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">
                    <asp:FileUpload ID="FileUploadCSV" runat="server" Width="200px" />
                    &nbsp;<asp:Button ID="cmdUploadCSV" runat="server" Text="Upload" />
                </td>
              </tr>
      <tr>
                <td valign="top" bgcolor="#eef1f3" width="120"><strong>ผู้ลงทะเบียน</strong></td>
                <td valign="top" bgcolor="#eef1f3"><table width="100%">
                  <tr>
                    <td colspan="2" valign="top">
                    
                    <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top"><table width="100%" cellspacing="0" cellpadding="2">
                  <tr>
                    <td valign="top">
                       <asp:TextBox ID="txtname" runat="server" Width="200px"></asp:TextBox>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" />
                      <asp:Button ID="cmdAdd"
              runat="server" Text="Add" CausesValidation="False" />                      
                      <asp:Button ID="cmdDeleteFile" runat="server" Text="Delete" 
                      CausesValidation="False" />                      
                     
                    &nbsp;<asp:HiddenField ID="txtempcode" runat="server" />
                    <asp:HiddenField ID="txtdept" runat="server" />
                     <asp:HiddenField ID="txtjobtitle" runat="server" />
                    </td>
                  </tr>
                </table>
                <asp:GridView ID="GridRegister" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                   <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                  <asp:TemplateField>
                    <ItemTemplate>
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("register_id") %>' Visible="false"></asp:Label>
                      <asp:CheckBox ID="chk" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Registerd employee">
                  
                    <ItemTemplate>
                     
                      <asp:Label ID="Label1" runat="server" Text='<%# Bind("emp_name") %>'></asp:Label><br />
                      Tel:  <asp:Label ID="Label2" runat="server" Text='<%# Bind("contact_tel") %>'></asp:Label><br />
                        Email: <asp:Label ID="Label3" runat="server" Text='<%# Bind("contact_email") %>'></asp:Label>
                       </ItemTemplate>
                  </asp:TemplateField>
                      <asp:BoundField HeaderText="Employee code" DataField="emp_code" />
                      <asp:BoundField HeaderText="Department"  DataField="dept_name" />
                      <asp:BoundField HeaderText="Job title"  DataField="job_title" />
                      <asp:BoundField DataField="create_date" HeaderText="Register Date" />
                      <asp:TemplateField HeaderText="Attendance Date">
                         
                          <ItemTemplate>
                              <asp:Label ID="lblDateTS" runat="server" Text='<%# Eval("register_time") %>' Visible="false"></asp:Label>
                              <asp:Label ID="lblDate" runat="server" Text=''></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                </asp:GridView></td>
            </tr>
          </table>
                    
                    </td>
                  </tr>
                </table></td>
              </tr>
    </table>
      <input type="button" id="cmdClose" name="cmdClose" value="Close window" onclick="closePopup();" />
    </div>
    </form>
</body>
</html>
