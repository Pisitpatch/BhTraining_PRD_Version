<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="dept_detail.aspx.vb" Inherits="dept_detail" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--   <script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.bgiframe.min.js'></script> -->
<script type='text/javascript' src='js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='js/jquery-autocomplete/lib/thickbox-compressed.js'></script>
 <link rel="stylesheet" href="js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          $("#ctl00_ContentPlaceHolder1_txtemp_name").autocomplete("ajax_employee.ashx", { matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_txtemp_name').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data[1];
            //  alert("serial ::" + serial);
              var value = Array();
              value = data[1].split("#");
              $('#ctl00_ContentPlaceHolder1_txtemp_code').val(value[0]);
              $('#ctl00_ContentPlaceHolder1_txtjob_title').val(value[1]);
              $('#ctl00_ContentPlaceHolder1_txtjob_type').val(value[2]);

          });

          $("#ctl00_ContentPlaceHolder1_txtemp_name").click(function () {
              $(this).select();
          });




      });


  </script>
   <script type="text/javascript">
      function IAmSelected(source, eventArgs) {
          // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
          $("#ctl00_ContentPlaceHolder1_txtempcode").attr("disabled",false);
          $("#ctl00_ContentPlaceHolder1_txtempcode").val(eventArgs.get_value());
      }
      function onCheckBlank(me) {
          if (me.value == "") {
              $("#ctl00_ContentPlaceHolder1_txtempcode").val("");
          }
      }
      function onSelectCC(cc) {
          $('#ctl00_ContentPlaceHolder1_txtcostcenter').val(cc);
      }
  </script>
    <style type="text/css">
        .style2
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="header"><img src="images/menu_12.png" width="32" height="32"  />&nbsp;&nbsp;Costcenter Detail</div>
    
      <div id="data"><table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="4" class="theader"><img src="images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Costcenter 
        Detail</td>
    </tr>
  <tr>
    <td width="150" height="30">Division</td>
    <td width="235">
        <asp:DropDownList ID="txtdivision" runat="server" 
            DataTextField="division_name_en" DataValueField="division_id" 
            BackColor="#99CCFF">
        </asp:DropDownList>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
    </tr>
  <tr>
    <td width="150" height="30">Department code</td>
    <td width="235">
        <asp:TextBox ID="txtid" runat="server" Width="350px" ></asp:TextBox>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
    </tr>
  <tr>
    <td  height="30">Department Name</td>
    <td width="235">
        <asp:TextBox ID="txtname" runat="server" Width="350px" ></asp:TextBox>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
</tr>
  <tr>
    <td  height="30">Department Name (Local)</td>
    <td width="235">
        <asp:TextBox ID="txtnamelocal" runat="server" Width="350px" ></asp:TextBox>
      </td>
    <td width="75">&nbsp;</td>
    <td width="200">
        &nbsp;</td>
</tr>
  <tr>
    <td  height="30">&nbsp;</td>
    <td width="235">
          <asp:Button ID="cmdSaveDept" runat="server" Text="Save" 
            CssClass="button-green2" Font-Bold="True" CausesValidation="False" />&nbsp;
       <asp:Button ID="cmdCancel" runat="server" Text="Cancel" 
            CssClass="button-green2" CausesValidation="False" /></td>
    <td width="75"> &nbsp;</td>
    <td width="200">
        &nbsp;</td>
</tr>
  <tr>
    <td colspan="4" align="right">
        
     </td>
  </tr>
      </table>

      </div>
      
      <div id="data2">
      <div><strong>Email group</strong></div>
      <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  
  <tr><td>
      
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4"  CssClass="tdata3"   Width="100%" 
                    AllowPaging="True" DataKeyNames="mail_id" 
          EmptyDataText="No data" EnableModelValidation="True">
                   <RowStyle BackColor="#EEEEEE" />
                    <Columns>
                       <asp:TemplateField HeaderText="No.">
               <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
           </asp:TemplateField>
                        <asp:BoundField DataField="full_name" HeaderText="Employee name" />
                        <asp:BoundField DataField="email" HeaderText="Email" />
                        <asp:BoundField DataField="job_title" HeaderText="Job Title" />
                         <asp:BoundField DataField="job_type" HeaderText="Job Type" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                    CommandName="Delete" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                    </Columns>
                   <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#80adb0" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              <FooterStyle BackColor="#80adb0" />
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <div>
                <table width="100%">
                <tr>
                <td width="130">Employee Name</td>
                <td>
                    <asp:TextBox ID="txtemp_name" runat="server" Width="400px"></asp:TextBox>
                    &nbsp;<asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/images/spellcheck.png" ToolTip="Autocomplete" />
                    </td>
                </tr>
                <tr>
                <td width="130" class="style2">Employee Code</td>
                <td class="style2">
                    <asp:TextBox ID="txtemp_code" runat="server"  Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td width="130">Job Title</td>
                <td>
                    <asp:TextBox ID="txtjob_title" runat="server"  Width="400px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td width="130">Job Type</td>
                <td>
                    <asp:TextBox ID="txtjob_type" runat="server"  Width="400px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td width="130">Email</td>
                <td>
                    <asp:TextBox ID="txtemail" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  ControlToValidate="txtemail" runat="server" 
        ErrorMessage="You must enter an email address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtemail" Display="Dynamic" 
                        ErrorMessage="Email is required field"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                </table>

                    <asp:Button ID="cmdAdd" runat="server" Text="Add" />
                    
                </div>
     
     
  </td></tr></table>
      </div>
      <div align="center">   
        </div>
            
           
</asp:Content>

