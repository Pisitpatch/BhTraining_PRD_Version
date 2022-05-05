<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_quota.aspx.vb" Inherits="srp_sqp_quota" %>
<%@ Import Namespace="ShareFunction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />


    <style type="text/css">
        .style2
        {
            height: 30px;
        }
    </style>

<script type="text/javascript">
    function selectAll(me) {
       // alert(me.checked);
        if (me.checked == true) {

            $(".chkSelect1 input:checkbox").attr("checked", "true");
        } else {
            $(".chkSelect1 input:checkbox").removeAttr("checked");
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;<strong>On the Spot Card Quota Setting</strong></div></td>
  </tr>
</table>
<div id="data">
  <div class="tabber" id="mytabber2">
    <div class="tabbertab">
      <h2>
        <strong>1. Quota Template</strong>
      </h2>
      <asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
            ShowFooter="True" PageSize="30">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField>
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:CheckBox ID="chkSelect" runat="server" />
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("quota_id") %>' 
                                  Visible="false"></asp:Label>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Grade">
                       
                          <ItemTemplate>
                            
                              <asp:Label ID="lblJobTitle" runat="server" Text='<%# Bind("job_title_name") %>'></asp:Label>
                          </ItemTemplate>
                        
                          <FooterTemplate>
                            <asp:DropDownList ID="txtadd_jobtitle1" runat="server" DataTextField="job_type" DataValueField="job_type">
                              </asp:DropDownList>
                           
                          </FooterTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="% Card of staff">
                        
                          <ItemTemplate>
                              <asp:Label ID="lblCal" runat="server" Text='<%# Bind("calculate_percent") %>' Visible="false"></asp:Label>
                              <asp:DropDownList ID="txtcal" runat="server">
                              <asp:ListItem Value="20">20 %</asp:ListItem>
                               <asp:ListItem Value="0">not calculate</asp:ListItem>
                              </asp:DropDownList>
                           </ItemTemplate>

                              <FooterTemplate>
                           <asp:DropDownList ID="txtadd_cal" runat="server">
                              <asp:ListItem Value="20">20 %</asp:ListItem>
                               <asp:ListItem Value="0">not calculate</asp:ListItem>
                              </asp:DropDownList>
                         
                          </FooterTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="x Month">
                         
                          <ItemTemplate>
                              <asp:Textbox ID="txtx_month" runat="server" Text='<%# Bind("x_month") %>' Width="50px" Visible="true"></asp:Textbox>
                          </ItemTemplate>
                           <FooterTemplate>
                           <asp:Textbox ID="txtadd_month" runat="server" Text='' Width="50px" Visible="true"></asp:Textbox>
                         
                          </FooterTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="+ Jobtype">
                          <ItemTemplate>
                          
                              <asp:Textbox ID="txtx_jobtype" runat="server" Text='<%# Bind("x_job_type") %>' Width="50px" Visible="true"></asp:Textbox>
                              
                          </ItemTemplate>
                         <FooterTemplate>
                           <asp:Textbox ID="txtadd_jobtype" runat="server" Text='' Width="50px" Visible="true"></asp:Textbox>
                         
                          </FooterTemplate>
                          
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Last Updated">
                          <ItemTemplate>
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("last_update_raw") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="By">
                          <ItemTemplate>
                               <asp:Label ID="lblAward" runat="server" Text='<%# Bind("last_update_by") %>'></asp:Label>
                            
                             
                          </ItemTemplate>
                          
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
    <Br />
      <div align="right">
          <asp:Button ID="cmdAdd" runat="server" Text="Add Rule" Width="180px" />
        &nbsp;<asp:Button ID="cmdSave" runat="server" Text="Save Rule" Width="180px" />
           &nbsp;<asp:Button ID="cmdDelete" runat="server" Text="Delete Rule" Width="180px" />
&nbsp;
      </div>
    </div>
    <div class="tabbertab">
      <h2>
        <strong>2. Chain of Command</strong>
      </h2>
        <table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
        <tr>
          <td colspan="2" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Cost Center</strong></td>
          <td>   <asp:DropDownList ID="txtcommand_dept" runat="server" DataTextField="dept_name_en" 
              DataValueField="dept_id">
          </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Job Type</strong></td>
          <td>   <asp:DropDownList ID="txtcommand_jobtype" runat="server" DataTextField="job_type" DataValueField="job_type">
                              </asp:DropDownList>
                              &nbsp;</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Employee</strong></td>
          <td>   
              <asp:TextBox ID="txtcommand_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Manager</strong></td>
          <td>   <asp:DropDownList ID="txtcommand_manager" runat="server" 
                  DataTextField="user_fullname" DataValueField="mgr_emp_code">
                              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td width="86" height="30">&nbsp;</td>
          <td>   <asp:Button ID="cmdCommandSearch" runat="server" Text="Search" />
            </td>
        </tr>
      </table>
       
      <br />
        <asp:Label ID="lblNumCommand" runat="server" Text=""></asp:Label> Records found.
      <br />
      <asp:GridView ID="gridview2" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" PageSize="30">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="Cost Center">
                        
                          <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("costcentre_name_e") %>'></asp:Label>
                              (<asp:Label ID="lblDate" runat="server" Text='<%# Eval("dept_id") %>'></asp:Label>)
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("emp_code") %>' 
                                  Visible="false"></asp:Label>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Employee">
                        
                          <ItemTemplate>
                          
                           <asp:Label ID="Label2" runat="server" Text='<%# Bind("user_fullname") %>'></asp:Label><br />
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("user_fullname_local") %>'></asp:Label>
                              (<asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("emp_code") %>'></asp:Label>)
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Type">
                          <ItemTemplate>
                          
                              <asp:Label ID="lblMethod" runat="server" Text='<%# Bind("emp_type") %>' Visible="true"></asp:Label>
                             
                          </ItemTemplate>
                       
                          <ItemStyle Width="120px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Job Type">
                          <ItemTemplate>
                             <asp:Label ID="lblLimit" runat="server" Text='<%# Bind("job_type") %>'></asp:Label>
                          </ItemTemplate>
                         
                       
                         
                      </asp:TemplateField>
                    
                        <asp:TemplateField HeaderText="Report to Mgr. Name">
                           <ItemStyle BackColor="#66ccff" />
                        
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("mgr_name1") %>'></asp:Label>
                                (<asp:Label ID="lblScore" runat="server" Text='<%# Bind("mgr_emp_code") %>'></asp:Label>)
                            </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <div>
              <asp:Label ID="lblMsg" runat="server" ForeColor="#FF3300"></asp:Label>
              <asp:FileUpload ID="myFile1" runat="server" />
    &nbsp;<asp:Button ID="cmdUpload" runat="server" Text="Import" />
              </div>
     
    </div>
    <div class="tabbertab">
      <h2><strong>3. Calculate Quota</strong></h2>
      <table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
        <tr>
          <td colspan="2" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Cost Center</strong></td>
          <td>   <asp:DropDownList ID="txtfind_dept" runat="server" DataTextField="dept_name_en" 
              DataValueField="dept_id">
          </asp:DropDownList>
            <strong>Quarter
                        </strong>
                         <asp:DropDownList ID="txtfind_quarter" runat="server" 
                  DataTextField="name" DataValueField="id">
        </asp:DropDownList>
            &nbsp;</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Job Type</strong></td>
          <td>   <asp:DropDownList ID="txtfind_jobtype" runat="server" DataTextField="job_type" DataValueField="job_type">
                              </asp:DropDownList>
                              &nbsp;</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Name</strong></td>
          <td>   
              <asp:TextBox ID="txtfind_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="86" height="30">&nbsp;</td>
          <td>   <asp:Button ID="cmdQuota" runat="server" Text="Search" />
            </td>
        </tr>
      </table>
       
      <br />
       <asp:Label ID="lblQuotaNum" runat="server" Text=""></asp:Label> Records found.
      <br />
       <asp:GridView ID="gridview3" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
            ShowFooter="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="Quarter">
                          <ItemStyle BackColor="#ccffff" />
                          <ItemTemplate>
                                 <asp:Label ID="lblQuarter" runat="server" Text='<%# Bind("quater_no") %>' 
                                  Visible="true" ></asp:Label>/<asp:Label ID="lblYear" runat="server" Text='<%# Bind("year_no") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Cost Center Name">
                        
                          <ItemTemplate>
                               <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("mgr_dept_name") %>' 
                                  Visible="true"></asp:Label>
                                     <asp:Label ID="lblPK" runat="server" Text='<%# Bind("cal_id") %>' 
                                  Visible="false"></asp:Label>
                              (<asp:Label ID="lblDeptID" runat="server" Text='<%# Bind("mgr_dept_id") %>' 
                                  Visible="true" ></asp:Label>)
                           </ItemTemplate>

                             
                      </asp:TemplateField>
                     
                    <asp:TemplateField HeaderText="Mgr. Name">
                        <ItemTemplate>
                               <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("mgr_name") %>' 
                                  Visible="true" ForeColor="Blue" ></asp:Label>
                                  (<asp:Label ID="lblEmpCode" runat="server" Text='<%# Bind("mgr_emp_code") %>' 
                                  Visible="true" ></asp:Label>)
                                  <br />
                                   <asp:Label ID="lblJobTitle1" runat="server" Text='<%# Bind("mgr_jobtitle") %>' 
                                  Visible="true" ></asp:Label>
                          
                    </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="No.Staff">
                          <ItemTemplate>
                                <asp:TextBox ID="txtamount" runat="server" Text='<%# Bind("staff_amount") %>' 
                                  Visible="true" Width="50px"></asp:TextBox>
                          </ItemTemplate>
    
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="% Card">
                            <ItemTemplate>
                              <asp:Label ID="lblPercent" runat="server" Text='<%# Bind("calculate_percent") %>' Visible="false"></asp:Label>
                           <asp:DropDownList ID="txtpercent" runat="server">
                                  <asp:ListItem Value="20">20 %</asp:ListItem>
                                  <asp:ListItem Value="0">--</asp:ListItem>
                              </asp:DropDownList>
                              </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="xMonth">
                         
                          <ItemTemplate>
                             <asp:TextBox ID="txtx_month" runat="server" Text='<%# Bind("x_month") %>' 
                                  Visible="true" Width="50px"></asp:TextBox>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="+Jobtype">
                          <ItemTemplate>
                          
                             <asp:TextBox ID="txtx_jobtype" runat="server" Text='<%# Bind("x_jobtype") %>' 
                                  Visible="true" Width="50px"></asp:TextBox>
                              
                          </ItemTemplate>
                       
                                                
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="=Total">
                         
                          <ItemTemplate>
                              <asp:TextBox ID="txttotal" runat="server" Text='<%# Bind("quota_total") %>' 
                                  Visible="true" Width="50px"></asp:TextBox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Issued">
                        <ItemStyle BackColor="Lime" />
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("issue_num") %>' ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Updated">
                         
                          <ItemTemplate>
                                <asp:Label ID="lblUpdateDateRaw" runat="server" Text='<%# Bind("last_update_raw") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="by">
                        
                          <ItemTemplate>
                             <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Bind("update_by") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
   <br />
   <table width="100%">
   <tr>
   <td width="300"></td>
   <td align="right" style="text-align:right">
   
      <div >
      <fieldset style="width:500px;">
      <legend>Automatic Calculate Quota</legend>
     
         Quater No.  
          <asp:DropDownList ID="txtcal_q" runat="server">
          <asp:ListItem Value="1">1</asp:ListItem>
           <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
             <asp:ListItem Value="4">4</asp:ListItem>
          </asp:DropDownList>
         
       Year  <asp:TextBox ID="txtcal_y" runat="server" Width="80px" MaxLength="4"></asp:TextBox>
        &nbsp;<asp:Button ID="cmdCal" runat="server" Text="Calculate Quota"  OnClientClick="return confirm('Are you sure you want to Calculate Quota?')"
              Width="150px" style="height: 26px" />
&nbsp;
<br /><br />
 </fieldset>
 </div>
     
   </td>
   </tr>
   </table>
     
    </div>
    <div class="tabbertab">
      <h2>
        <strong>4. Create Quarter Issue</strong>
      </h2>
      <table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
        <tr>
          <td colspan="2" class="theader"><img src="../images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Search</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Cost Center</strong></td>
          <td><asp:DropDownList ID="txtfind_dept2" runat="server" DataTextField="dept_name_en" 
              DataValueField="dept_id">
          </asp:DropDownList>
            <strong>Quarter
                        </strong>
                         <asp:DropDownList ID="txtfind_quarter2" runat="server" 
                  DataTextField="name" DataValueField="id" AutoPostBack="True">
        </asp:DropDownList>
            &nbsp;</td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Card ID</strong></td>
          <td>
              <asp:TextBox ID="txtfind_cardid" runat="server"></asp:TextBox>
              </td>
        </tr>
        <tr>
          <td width="86" class="style2"><strong>Job Type</strong></td>
          <td class="style2">
              <asp:DropDownList ID="txtfind_jobtype2" runat="server" DataTextField="job_type" 
                  DataValueField="job_type">
                              </asp:DropDownList>
                              </td>
        </tr>
        <tr>
          <td width="86" height="30"><strong>Name</strong></td>
          <td>
              <asp:TextBox ID="txtmgrname" runat="server"></asp:TextBox>
              <asp:Button ID="cmdSearchQuota" runat="server" Text="Search" /></td>
        </tr>
      </table>
      <br />
      <fieldset style="width:700px">
      <legend>Create Issue </legend>
           <asp:Button ID="cmdCreateIssueQuota0" runat="server" Text="Create Issue from quota" OnClientClick="return confirm('Are you sure you want to Create Issue from quota?');" />
          <asp:Button ID="cmdExcel1" runat="server" Text="Export to Excel" />
      <br /><br />
      </fieldset>
      <br />
      <div align="right">
           <asp:Button ID="cmdValidate1" runat="server" Text="Validate Card" Width="150px" Visible="False" />
       <asp:Button ID="cmdGenerate0" runat="server" Text="Generate Card ID." Width="150px" OnClientClick="return confirm('Are you sure you want to Generate Card ID.?');" />
          <asp:Button ID="cmdSaveIssueQuota0" runat="server" Text="Save" Width="150px" />
      
      </div><br />
       <asp:Label ID="lblIssueNum" runat="server" Text=""></asp:Label> Records found.
      <br />
        <asp:GridView ID="gridview4" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." 
            ShowFooter="True" AllowPaging="True" PageSize="100">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                    <asp:TemplateField>
                         <ItemStyle Width="30px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:CheckBox ID="chkSelect" runat="server" CssClass="chkSelect1" />
                            
                             
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <HeaderTemplate>
                           <asp:CheckBox ID="chkSelectAll" runat="server"   />
                          </HeaderTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Quarter">
                        <ItemStyle BackColor="#ccffff" />
                          <ItemTemplate>
                               <asp:Label ID="lblQuater" runat="server" Text='<%# Bind("quater") %>' 
                                  Visible="true"></asp:Label>
                           </ItemTemplate>

                             
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Created Date">
                       
                          <ItemTemplate>
                            
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("quater_issue_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblDate" runat="server" Text='<%# Bind("date_issue_raw") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                
                     
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Project">
                         
                          <ItemTemplate>
                               <asp:Label ID="lblProject" runat="server" Text='<%# Bind("project_name") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Employee Code">
                         
                          <ItemTemplate>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("mgr_emp_code") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                       
                               <asp:Label ID="lblEmpName" runat="server" Text='<%# Bind("user_fullname") %>' 
                                  Visible="true" ForeColor="Blue" ></asp:Label><br />
                                 <asp:Label ID="Label6" runat="server" Text='<%# Bind("user_fullname_local") %>' 
                                  Visible="true" ForeColor="Blue" ></asp:Label>
                          
                                 
                    </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Job Type">
                        
                          <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("mgr_job_type") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Center">
                        
                          <ItemTemplate>
                               <asp:Label ID="lblEmpDeptID" runat="server" Text='<%# Bind("mgr_dept_id") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                        
                          <ItemTemplate>
                               <asp:Label ID="lblEmpDept" runat="server" Text='<%# Bind("mgr_dept_name") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Qty">
                          <ItemTemplate>
                                <asp:TextBox ID="txtamount" runat="server" Text='<%# Bind("issue_qty") %>' 
                                  Visible="true" Width="50px"></asp:TextBox>
                          </ItemTemplate>
    
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Quota">
                         
                          <ItemTemplate>
                             <asp:Label ID="lblQuota" runat="server" Text='<%# Bind("quota_qty") %>' 
                                  Visible="true" ></asp:Label>
                             
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Card ID.">
                      <ItemStyle BackColor="LightGreen" />
                            <ItemTemplate>
                              <asp:TextBox ID="txtstart" runat="server" Text='<%# Bind("card_id_start") %>' 
                                  Visible="true" Width="70px"></asp:TextBox>
                                  -
                             <asp:Label ID="txtend" runat="server" Text='<%# Bind("card_id_end") %>' 
                                  Visible="true" Width="50px"></asp:Label>
                                <br /> <asp:Button ID="cmdValidate" runat="server"
                                  Text="Validate" OnCommand="onValidate" CommandName="validate" CommandArgument='<%# Eval("card_id_start") & "#" & Eval("card_id_end") & "#" & Eval("quater_issue_id")%>'  Visible="false" />
                                <br /><asp:Label ID="lblValidate" runat="server" ForeColor="red" Text="-" />
                              </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Registered">
                          <ItemTemplate>
                          
                           <asp:Label ID="lblRegister" runat="server" 
                                  Visible="true" ></asp:Label>
                              
                          </ItemTemplate>
                       
                                                
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Issued by">
                         
                          <ItemTemplate>
                          <asp:Label ID="txtIssueBy" runat="server" Text='<%# Bind("quota_issue_by") %>' 
                                  Visible="true" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Print">
                        
                          <ItemTemplate>
                              
                              <asp:Button ID="cmdPrint" runat="server"
                                  Text="Print" />
                          </ItemTemplate>
                      </asp:TemplateField>
                    
                  </Columns>
              </asp:GridView>
     <br />
      <div align="right">
       <asp:Button ID="cmdGenerate1" runat="server" Text="Generate Card ID." 
              Width="150px" 
              OnClientClick="return confirm('Are you sure you want to Generate Card ID.?');" />
          <asp:Button ID="cmdSaveIssueQuota" runat="server" Text="Save" Width="150px" />
           <asp:Button ID="cmdCreateIssueQuota" runat="server" 
              Text="Create Issue from quota" Width="160px" 
              OnClientClick="return confirm('Are you sure you want to Create Issue from quota?');" 
              Visible="False" />
      
      </div>
    </div>
  </div>
  <div align="right"></div>
</div>

</asp:Content>

