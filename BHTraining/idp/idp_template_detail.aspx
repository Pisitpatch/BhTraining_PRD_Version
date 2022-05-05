<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_template_detail.aspx.vb" Inherits="idp_idp_template_detail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
<link rel="stylesheet" href="../js/tab_simple/example.css" TYPE="text/css" MEDIA="screen" />
<link rel="stylesheet" href="../js/tab_simple/example-print.css" TYPE="text/css" MEDIA="print">
<script type="text/javascript">
    var global_time;
    // =========== Check Session ===========================
    checkSession('<%response.write(session("bh_username").toString) %>', '<%response.write(viewtype) %>', ''); // Check session every 1 sec.
</script>
<script type="text/javascript">
    function validateTemplate() {
       // alert($("#ctl00_ContentPlaceHolder1_txtadd_category").val());
        //return false;
        if ($("#ctl00_ContentPlaceHolder1_txtadd_category").val() == "") {
            alert("Please choose IDP Category !");
            $("#ctl00_ContentPlaceHolder1_txtadd_category").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_txtadd_topic").val() == "") {
            alert("Please choose IDP Topic !");
            $("#ctl00_ContentPlaceHolder1_txtadd_topic").focus();
            return false;
        }
    }
</script>
    <style type="text/css">
        .style2
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div id="data">
      <div id="header"><img src="../images/doc01.gif" alt="c" width="32" height="32"  />&nbsp;&nbsp;
          <asp:Label ID="lblHeader" runat="server" Text="Individual Development Plan Program"></asp:Label>
      </div>
          <div align="right">
              <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" />
           
                       &nbsp;
            <div align="left" class="colname">

            <fieldset style="width:80%">
            <legend style="font-weight:bold">ชื่อหลักสูตร (Program Name)</legend>
             <table width="100%">
            <tr><td width="150">Program Name</td>
            <td><asp:TextBox ID="txttitle" runat="server" Width="400px"></asp:TextBox></td>
            </tr>
            <tr><td class="style2" >Status</td>
            <td class="style2">
                <asp:RadioButtonList ID="txtstatus" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Draft</asp:ListItem>
                </asp:RadioButtonList>
                </td>
            </tr>
            <tr><td >&nbsp;</td>
            <td>
                &nbsp;</td>
            </tr>
            </table>
            </fieldset>
           
                
                
             
            
            </div>
          </div>
          <asp:Panel ID="panelDetail" runat="server">
          <br />
            <fieldset style="width:80%" runat="server" id="fieldset_tagetgroup">
            <legend style="font-weight:bold">กลุ่มเป้าหมาย (Target Group)</legend>
              
        <div class="tabber" id="mytabber2">
            <div class="tabbertab">
              <h2>IDP for Employee ID</h2>
              <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                <tr>
                  <td width="150" valign="top"><strong>Employee List</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td>
                            <asp:FileUpload ID="FileUploadCSV" runat="server" Width="200px" />
                            &nbsp;<asp:Button ID="cmdUploadCSV" runat="server" Text="Upload" />
                            <br />
                            <a href="employee.csv" target="_blank">ไฟล์ตัวอย่าง</a></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                          <td>
                              <asp:TextBox ID="txtfindemployee" runat="server" Width="200px"></asp:TextBox>
                              <asp:Button ID="cmdSearch" runat="server" Text="search" />
                          </td>
                          <td>
                              &nbsp;</td>
                          <td>
                              &nbsp;</td>
                      </tr>
                      <tr>
                        <td width="180">
                            <asp:ListBox ID="lblDivisionAll" runat="server" Width="300px" 
                                DataTextField="user_fullname" DataValueField="emp_code" 
                                SelectionMode="Multiple"></asp:ListBox>
                       </td>
                        <td width="60">
                        <asp:Button ID="cmdSelect" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemove" runat="server" Text="<" CausesValidation="False" />
                        </td>
                        <td width="230">
                            <asp:ListBox ID="lblDivisionSelect" runat="server" Width="300px"   
                                DataTextField="user_fullname" DataValueField="emp_code" 
                                SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
              </table>
              <br />
            </div>
            <div class="tabbertab">
              <h2>IDP for Cost Center</h2>
              <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                <tr>
                  <td width="150" valign="top"><strong>Cost Center List</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:ListBox ID="lblCCAll" runat="server"  DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                        <td width="60"> <asp:Button ID="cmdCCAdd" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="lblCCRemove" runat="server" Text="<" CausesValidation="False" /></td>
                        <td width="230">
                            <asp:ListBox ID="lblCCSelect" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
              </table>
              <br />
            </div>
            <div class="tabbertab">
              <h2>IDP for Job Type</h2>
              <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                <tr>
                  <td width="150" valign="top"><strong>Job Type List</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td>
                            <asp:TextBox ID="txtfind_jobtype" runat="server" Width="200px"></asp:TextBox>
                        
                            <asp:Button ID="cmdSearchJobType" runat="server" Text="search" />
                          </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td width="180">
                            <asp:ListBox ID="lblJobTypeAll" runat="server" DataTextField="job_type" 
                                DataValueField="job_type" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                        <td width="60"> <asp:Button ID="cmdAddType" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemoveType" runat="server" Text="<" CausesValidation="False" /></td>
                        <td width="230">
                            <asp:ListBox ID="lblJobTypeSelect" runat="server" DataTextField="job_type" 
                                DataValueField="job_type" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
              </table>
              <br />
            </div>
            <div class="tabbertab">
              <h2>IDP for Job title</h2>
              <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                <tr>
                  <td width="150" valign="top"><strong>Job Title List</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td>
                            <asp:TextBox ID="txtfind_jobtitle" runat="server" Width="200px"></asp:TextBox>
                            <asp:Button ID="cmdSearchJobTitle" runat="server" Text="search" />
                          </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td width="180">
                            <asp:ListBox ID="lblJobTitleAll" runat="server" DataTextField="job_title" 
                                DataValueField="job_title" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                        <td width="60"> <asp:Button ID="cmdAddTitle" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemoveTitle" runat="server" Text="<" CausesValidation="False" /></td>
                        <td width="230">
                            <asp:ListBox ID="lblJobTitleSelect" runat="server" DataTextField="job_title" 
                                DataValueField="job_title" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
              </table>
              <br />
            </div>
        </div>
       
        <br />
        </fieldset>
       
       <div class="tabber" id="tab_ladder_score" runat="server" visible="false">
          <div class="tabbertab">
            <h2>
              <strong>Score Setting</strong>
            </h2>
            <table width="100%" cellspacing="0" cellpadding="0" style="margin-top: 5px;">
                <tr>
                  <td width="150" valign="top"><strong>Apply until (date/time)</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="243"><asp:TextBox ID="txtapply_date" runat="server"></asp:TextBox>
                        <asp:Image ID="Image3" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    
                           <asp:CalendarExtender ID="txtdate111" runat="server" 
                                  Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtapply_date" PopupButtonID="Image3">
                              </asp:CalendarExtender>
                          </td>
                        <td width="201"><strong>Require Minimum Score </strong></td>
                        <td width="421">
                            <asp:TextBox ID="txtrequire_score" runat="server"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                        <td>   <asp:DropDownList ID="txthour1" runat="server">
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
                
              </asp:DropDownList>&nbsp;</td>
                        <td><strong>Elective Minimum Score</strong></td>
                        <td>
                            <asp:TextBox ID="txtelective_score" runat="server"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                          <td>
                              &nbsp;</td>
                          <td>
                              <strong>Form Type</strong></td>
                          <td>
                              <asp:DropDownList ID="txtformtype" runat="server">
                                  <asp:ListItem Value="0">-</asp:ListItem>
                                  <asp:ListItem Value="1">Adjust Level</asp:ListItem>
                                  <asp:ListItem Value="2">Maintain Level</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                  </table></td>
                </tr>
              </table>
            </div>
        </div>

        <div class="tabber" id="mytabber1">
          <div class="tabbertab">
            <h2>
              <strong>Topic Setting</strong>
            </h2>
                     
              <br />
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
               <asp:GridView ID="GridTemplate" runat="server" CssClass="tdata" 
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="template_detail_id" 
                  EnableModelValidation="True" AllowPaging="True" PageSize="30" 
                  EmptyDataText="There is no item.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("template_detail_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="25px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("template_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="R/E">
                         <ItemStyle Width="30px" />
                          <ItemTemplate>
                              <asp:Label ID="lblRequire" runat="server" Text='<%# Bind("template_is_require") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="txtedit_require" runat="server" 
                        Width="80px">
                        <asp:ListItem Value="1">Require</asp:ListItem>
                        <asp:ListItem Value="0">Elective</asp:ListItem>
                    </asp:DropDownList>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories">
                         <ItemStyle Width="120px" />
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("template_category_name") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Development Topics">
                           
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("topic_name_th") %>'></asp:Label><br />
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("topic_name_en") %>'></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Expected outcomes" DataField="template_expect_detail"  />
                      <asp:BoundField HeaderText="Methodogy" DataField="template_methodogy_name"  />
                    
                     
                      <asp:TemplateField HeaderText="Limit">
                        
                          <ItemTemplate>
                                <asp:textbox ID="txtLimit" runat="server" Text='<%# Bind("template_limit") %>' Width="45px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Score">
                        
                          <ItemTemplate>
                              <asp:textbox ID="txtedit_score" runat="server" Text='<%# Bind("template_score") %>' Width="50px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="template_start_date" 
                          DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start" ItemStyle-Width="80px">
                      <ItemStyle Width="80px" />
                      </asp:BoundField>
                      <asp:BoundField DataField="template_complete_date" 
                          DataFormatString="{0:dd/MM/yyyy}" HeaderText="Finish" ItemStyle-Width="80px">
                      <ItemStyle Width="80px" />
                      </asp:BoundField>
                    
                     
                  </Columns>
              </asp:GridView>
              <br />  <asp:Button ID="cmdDelTopic" runat="server" Text="Delete Topic" Width="100px" /><br />
              <br />
              <fieldset style="width:80%">
              <legend style="font-weight:bold">Add new topic</legend>
               <table width="100%" cellpadding="2" cellspacing="1" >
         
              <tr>
                <td valign="top" width="150">
                    Type</td>
                <td valign="top">
                    <asp:DropDownList ID="txtadd_require" runat="server" BackColor="#FFFF99" 
                        Width="100px">
                        <asp:ListItem Value="1">Require</asp:ListItem>
                        <asp:ListItem Value="0">Elective</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
          
              <tr>
                <td valign="top">Category</td>
                <td valign="top">
                    <asp:DropDownList ID="txtadd_category" runat="server" AutoPostBack="True" 
                        BackColor="#FFFF99" DataTextField="category_name_en" 
                        DataValueField="category_id" Width="250px">
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                    <td valign="top">
                        Topic</td>
                    <td valign="top">
                        <asp:DropDownList ID="txtadd_topic" runat="server" BackColor="#FFFF99" 
                            DataTextField="master_topic_name_th" DataValueField="topic_id">
                        </asp:DropDownList>
                    </td>
                </tr>
              </table>
               <asp:Panel ID="panel_add_idp" runat="server" Visible="false">
               <table width="100%" cellpadding="2" cellspacing="1" >
                <tr>
                    <td valign="top" width="150">
                        Expect Outcome</td>
                    <td valign="top">
                        <asp:DropDownList ID="txtadd_expect" runat="server" BackColor="#FFFF99" 
                            DataTextField="expect_detail" DataValueField="expect_id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Methodody</td>
                    <td valign="top">
                        <asp:DropDownList ID="txtadd_method1" runat="server" BackColor="#FFFF99" 
                            DataTextField="method_name" DataValueField="method_id">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Start date</td>
                    <td valign="top">
                        <asp:TextBox ID="txtadd_start" runat="server" BackColor="#FFFF99" Width="75px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtadd_start" PopupButtonID="Image1">
                        </asp:CalendarExtender>
                        &nbsp;<asp:Image ID="Image1" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Completed date</td>
                    <td valign="top">
                        <asp:TextBox ID="txtadd_complete" runat="server" BackColor="#FFFF99" 
                            Width="75px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="txtadd_complete" PopupButtonID="image2">
                        </asp:CalendarExtender>
                        &nbsp;<asp:Image ID="Image2" runat="server" CssClass="mycursor" 
                            ImageUrl="~/images/calendar.gif" />
                    </td>
                </tr>
                </table>
                </asp:Panel>
                <asp:Panel ID="panel_add_ladder" runat="server" Visible="false">
                <table width="100%" cellpadding="2" cellspacing="1" >
                  <tr>
                   
                    <td valign="top" width="150">
                        Limit</td>
                    <td valign="top">
                        <asp:TextBox ID="txtadd_limit" runat="server" BackColor="#99CCFF" Width="75px"></asp:TextBox>
                       
                        &nbsp;time&nbsp; (For Nursing Ladder)</td>
                </tr>
                    <tr>
                        <td valign="top" width="150">
                            Score</td>
                        <td valign="top">
                            <asp:TextBox ID="txtadd_score" runat="server" BackColor="#99CCFF" Width="75px"></asp:TextBox>
                            &nbsp;(For Nursing Ladder)</td>
                    </tr>
                </table>
                </asp:Panel>
                <table width="100%" cellpadding="2" cellspacing="1" >
                <tr >
                    <td valign="top" width="150">
                        &nbsp;</td>
                   
                    <td valign="top">
                        <asp:Button ID="cmdAddTopic" runat="server" Text="Add Topic" Width="100px" />
                        &nbsp;
                      
                    </td>
                </tr>
            </table>
              </fieldset>
           </ContentTemplate>
              </asp:UpdatePanel>
            </div>
            <br />
             <div align="right">
              <asp:Button ID="cmdSave1" runat="server" Text="Save" Width="100px" />
              </div>
              <br />
          </div>
        </asp:Panel>        
        <br />
        <div align="right">
            &nbsp;&nbsp;
        </div></div>
</asp:Content>

