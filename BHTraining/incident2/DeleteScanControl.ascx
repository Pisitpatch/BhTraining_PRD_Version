<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DeleteScanControl.ascx.vb" Inherits="incident_DeleteScanControl" %>
<%@ Import Namespace="ShareFunction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .style1
    {
        height: 27px;
    }
</style>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/jquery.ajaxQueue.js'></script>
<script type='text/javascript' src='../js/jquery-autocomplete/lib/thickbox-compressed.js'></script>

  <link rel="stylesheet" href="../js/autocomplete/jquery.autocomplete.css" type="text/css" />
  <script type="text/javascript" src="../js/autocomplete/jquery.autocomplete.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {


          $("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doc_name").autocomplete("../getDocument.ashx", {
              matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doc_name').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

          $("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doc_name").autocomplete("../getDocument.ashx", {
              matchContains: false,
              autoFill: false,
              mustMatch: false
          });

          $('#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doc_name').result(function (event, data, formatted) {
              // $("#result").html(!data ? "No match!" : "Selected: " + formatted);
              var serial = data;
              // alert("serial ::" + serial);

          });

      });

  </script>
<script type="text/javascript">

    function checkDate(sender, args) {
        if (sender._selectedDate > new Date()) {
           // alert("You cannot select a day !");
           // sender._selectedDate = new Date();
            // set the date back to the current date
          //  sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }

    function onSubmit() {

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_hn").val() == "") {
            alert("Please enter Wrong HN");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_hn").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doc_name").val() == "") {
            alert("Please enter Wrong Document name");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doc_name").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doctor").val() == "") {
            alert("Please enter Wrong Doctor");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtwrong_doctor").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtdate1").val() == "") {
            alert("Please enter Date");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtdate1").focus();
            return false;
        }


        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_hn").val() == "") {
            alert("Please enter Correct HN");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_hn").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doc_name").val() == "") {
            alert("Please enter Correct Document name");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doc_name").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doctor").val() == "") {
            alert("Please enter Correct Doctor");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtcorrect_doctor").focus();
            return false;
        }

        if ($("#ctl00_ContentPlaceHolder1_ctl03_txtdate2").val() == "") {
            alert("Please enter Date");
            $("#ctl00_ContentPlaceHolder1_ctl03_txtdate2").focus();
            return false;
        }

        return true;
    }
</script>
<table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td width="185" valign="top" bgcolor="#eaeaea">Document Name</td>
    <td valign="top">
        <asp:GridView ID="gridDeleteScan" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" EnableModelValidation="True" GridLines="Horizontal"  DataKeyNames="delete_scan_id" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="ใบที่ผิด">
                   
                    <ItemTemplate>
                       
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tr>
              <td bgcolor="#FFD7D7" width="150">HN<span class="style2">*</span></td>
              <td bgcolor="#FFD7D7"> <asp:Label ID="Label1" runat="server" Text='<%# Eval("wrong_hn") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"> ชื่อเอกสาร<span class="style2">*</span></td>
              <td bgcolor="#FFD7D7"><asp:Label ID="LabelWrongDocName" runat="server" Text='<%# Eval("wrong_doc_name") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7">วันที่ปรากฏในระบบ<span class="style2">*</span></td>
              <td bgcolor="#FFD7D7"><asp:Label ID="Label4" runat="server" Text='<%#  Bind("wrong_date_raw", "{0:dd/MM/yyyy hh:mm}") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7">ชื่อแพทย์<span class="style2">*</span></td>
              <td bgcolor="#FFD7D7"><asp:Label ID="Label5" runat="server" Text='<%# Eval("wrong_doctor") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7">หมายเลขเอกสาร(ถ้ามี)</td>
              <td bgcolor="#FFD7D7"><asp:Label ID="Label6" runat="server" Text='<%# Eval("wrong_doc_no") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7">ข้อมูลเพิ่มเติม (ถ้ามี)</td>
              <td bgcolor="#FFD7D7"><asp:Label ID="Label7" runat="server" Text='<%# Eval("wrong_other")%>'></asp:Label>&nbsp;</td>
            </tr>
          
          </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ใบที่ถูก">
                  
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                          <table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tr>
              <td bgcolor="#E1E1FF" width="150">HN<span class="style2">*</span></td>
              <td bgcolor="#E1E1FF"> <asp:Label ID="Label3" runat="server" Text='<%# Eval("correct_hn") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#E1E1FF"> ชื่อเอกสาร<span class="style2">*</span></td>
              <td bgcolor="#E1E1FF"><asp:Label ID="Label8" runat="server" Text='<%# Eval("correct_doc_name")%>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#E1E1FF">วันที่ปรากฏในระบบ<span class="style2">*</span></td>
              <td bgcolor="#E1E1FF"><asp:Label ID="Label9" runat="server" Text='<%#  Bind("correct_date_raw", "{0:dd/MM/yyyy hh:mm}") %>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#E1E1FF">ชื่อแพทย์<span class="style2">*</span></td>
              <td bgcolor="#E1E1FF"><asp:Label ID="Label10" runat="server" Text='<%# Eval("correct_doctor")%>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#E1E1FF">หมายเลขเอกสาร(ถ้ามี)</td>
              <td bgcolor="#E1E1FF"><asp:Label ID="Label11" runat="server" Text='<%# Eval("correct_doc_no")%>'></asp:Label>&nbsp;</td>
            </tr>
            <tr>
              <td bgcolor="#E1E1FF">ข้อมูลเพิ่มเติม (ถ้ามี)</td>
              <td bgcolor="#E1E1FF"><asp:Label ID="Label12" runat="server" Text='<%# Eval("correct_other")%>'></asp:Label>&nbsp;</td>
            </tr>
          
          </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
      </td>
    
  </tr>
  <tr>
    <td width="185" valign="top" bgcolor="#eaeaea">&nbsp;</td>
    <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tr>
              <td colspan="2" bgcolor="#FFD7D7"><p align="center" id="yui_3_16_0_1_1408785434585_8315"><strong> <span id="yui_3_16_0_1_1408785434585_8344" lang="TH">ใบที่ผิด </span><span id="yui_3_16_0_1_1408785434585_8314">(<span id="yui_3_16_0_1_1408785434585_8313" lang="TH">ต้องการลบออกจากระบบ</span>)</span></strong></p></td>
              <td colspan="2" bgcolor="#E1E1FF"><p align="center" id="yui_3_16_0_1_1408785434585_7948"><strong> <span id="yui_3_16_0_1_1408785434585_8227" lang="TH">ใบที่ถูก </span><span id="yui_3_16_0_1_1408785434585_7947">(<span id="yui_3_16_0_1_1408785434585_7946" lang="TH">ใช้ตรวจเทียบความถูกต้องก่อนลบ</span>)</span></strong></p></td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7">HN</td>
              <td bgcolor="#FFD7D7">
                  <asp:TextBox ID="txtwrong_hn" runat="server" Width="90%"></asp:TextBox>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span1">HN</span></td>
              <td bgcolor="#E1E1FF">
                  <asp:TextBox ID="txtcorrect_hn" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"> <span id="yui_3_16_0_1_1408785434585_8298" lang="TH">ชื่อเอกสาร</span></td>
              <td bgcolor="#FFD7D7">
                  <asp:TextBox ID="txtwrong_doc_name" runat="server" Width="90%"></asp:TextBox>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span2" lang="TH">ชื่อเอกสาร</span></td>
              <td bgcolor="#E1E1FF">
                  <asp:TextBox ID="txtcorrect_doc_name" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"><span id="yui_3_16_0_1_1408785434585_8295" lang="TH">วันที่ปรากฏในระบบ</span></td>
              <td bgcolor="#FFD7D7">
                  <table cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                          <td width="150">
                              <asp:TextBox ID="txtdate1" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                              <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtdate1" />
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" PopupButtonID="Image1" TargetControlID="txtdate1"></asp:CalendarExtender>
                              <asp:Image ID="Image1" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                              <br />
                          </td>
                          <td style="vertical-align: top" width="40">
                              <asp:Label ID="lblCFBdetail9" runat="server" Text="Time"></asp:Label>
                          </td>
                          <td>
                              <asp:DropDownList ID="txthour" runat="server">
                              </asp:DropDownList>
                              :
                              <asp:DropDownList ID="txtmin" runat="server">
                              </asp:DropDownList>
                          </td>
                      </tr>
                  </table>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span3" lang="TH">วันที่ปรากฏในระบบ</span></td>
              <td bgcolor="#E1E1FF">
                  <table cellpadding="0" cellspacing="0" width="100%">
                      <tr>
                          <td width="150">
                              <asp:TextBox ID="txtdate2" runat="server" BackColor="Lime" Width="100px"></asp:TextBox>
                              <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtdate2" />
                              <asp:CalendarExtender ID="txtdate_report_CalendarExtender0" runat="server" Enabled="True" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" PopupButtonID="Image2" TargetControlID="txtdate2"></asp:CalendarExtender>
                              <asp:Image ID="Image2" runat="server" CssClass="mycursor" ImageUrl="~/images/calendar.gif" />
                              <br />
                          </td>
                          <td style="vertical-align: top" width="40">
                              <asp:Label ID="lblCFBdetail10" runat="server" Text="Time"></asp:Label>
                          </td>
                          <td>
                              <asp:DropDownList ID="txthour2" runat="server">
                              </asp:DropDownList>
                              :
                              <asp:DropDownList ID="txtmin2" runat="server">
                              </asp:DropDownList>
                          </td>
                      </tr>
                  </table>
                </td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"><span id="yui_3_16_0_1_1408785434585_8301" lang="TH">ชื่อแพทย์</span></td>
              <td bgcolor="#FFD7D7">
                  <asp:TextBox ID="txtwrong_doctor" runat="server" Width="90%"></asp:TextBox>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span4" lang="TH">ชื่อแพทย์</span></td>
              <td bgcolor="#E1E1FF">
                  <asp:TextBox ID="txtcorrect_doctor" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"><span id="yui_3_16_0_1_1408785434585_8302" lang="TH">หมายเลขเอกสาร </span><span id="yui_3_16_0_1_1408785434585_8293">(ถ้ามี)</span></td>
              <td bgcolor="#FFD7D7">
                  <asp:TextBox ID="txtwrong_doc_no" runat="server" Width="90%"></asp:TextBox>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span5" lang="TH">หมายเลขเอกสาร </span><span id="Span6">(ถ้ามี)</span></td>
              <td bgcolor="#E1E1FF">
                  <asp:TextBox ID="txtcorrect_doc_no" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td bgcolor="#FFD7D7"><span id="yui_3_16_0_1_1408785434585_8303" lang="TH">ข้อมูลเพิ่มเติม </span><span id="yui_3_16_0_1_1408785434585_8288">(ถ้ามี)</span></td>
              <td bgcolor="#FFD7D7">
                  <asp:TextBox ID="txtwrong_other" runat="server" Width="90%"></asp:TextBox>
                </td>
              <td bgcolor="#E1E1FF"><span id="Span7" lang="TH">ข้อมูลเพิ่มเติม </span><span id="Span8">(ถ้ามี)</span></td>
              <td bgcolor="#E1E1FF">
                  <asp:TextBox ID="txtcorrect_other" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td colspan="4"><div align="right">
                  <asp:Button ID="cmdAdd" runat="server" Text="Add" CausesValidation="False" Width="100px" OnClientClick="return onSubmit();" />
                  <span style="PADDING-BOTTOM: 10px; PADDING-TOP: 10px; PADDING-LEFT: 10px; PADDING-RIGHT: 10px">
                &nbsp;</span></div></td>
              </tr>
          </table></td>
    
  </tr>
  <tr>
    <td width="185" valign="top" bgcolor="#eaeaea">Delete</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
          <asp:RadioButton ID="txtscandel1"  groupname="scandel" runat="server" />
     
          </td>
        <td width="100">Yes</td>
        <td width="23">  <asp:RadioButton ID="txtscandel2"  groupname="scandel" runat="server" />
         </td>
        <td>No</td>
        </tr>
    </table></td>
    
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">1. Unit of Error Document</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><asp:RadioButton ID="txtscanunit1" groupname="scanunit" runat="server" />
          </td>
        <td width="100">NRS OPD</td>
        <td width="23">&nbsp;<asp:RadioButton ID="txtscanunit2" groupname="scanunit" 
                runat="server" />
         </td>
        <td width="100">NRS IPD</td>
        <td width="23">&nbsp;<asp:RadioButton ID="txtscanunit3" groupname="scanunit" 
                runat="server" />
          </td>
        <td width="100">Check-up</td>
        <td width="23"><asp:RadioButton ID="txtscanunit4" groupname="scanunit" 
                runat="server" />
          </td>
        <td width="100">MDCL</td>
        <td width="23"><asp:RadioButton ID="txtscanunit5" groupname="scanunit" 
                runat="server" />
          </td>
        <td>Others 
         
          <input type="text" name="txtscanunit_other" id="txtscanunit_other" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">2. Type of Document</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofdoc1" id="chk_typeofdoc1" runat="server" />
</td>
        <td width="180" valign="top">Document Error</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofdoc2" id="chk_typeofdoc2" runat="server" /></td>
        <td width="180" valign="top">Scan Error</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofdoc3" id="chk_typeofdoc3" runat="server" />
          <label for="checkbox82"></label>
          <label for="radio18"></label></td>
        <td valign="top">Registration Error</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_typeofdoc4" id="chk_typeofdoc4" runat="server" />
          <label for="checkbox83"></label></td>
        <td valign="top">Correct Document</td>
        <td valign="top"><input type="checkbox" name="chk_typeofdoc5" id="chk_typeofdoc5" runat="server" /></td>
        <td valign="top">Print Error</td>
        <td valign="top"><input type="checkbox" name="chk_typeofdoc6" id="chk_typeofdoc6" runat="server" />
          <label for="checkbox84"></label></td>
        <td valign="top">Incomplete Document</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_typeofdoc7" id="chk_typeofdoc7" runat="server" />
          <label for="checkbox85"></label></td>
        <td valign="top">Improper Document</td>
        <td valign="top"><input type="checkbox" name="chk_typeofdoc8" id="chk_typeofdoc8" runat="server" /></td>
        <td valign="top">Unnecessary Document</td>
        <td valign="top"><label for="checkbox86">
          <input type="checkbox" name="chk_typeofdoc9" id="chk_typeofdoc9" runat="server" />
        </label></td>
        <td valign="top">Other</td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Patient Record</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofptrec1" id="chk_typeofptrec1" runat="server" />
          <label for="checkbox90"></label>
          <label for="radio20"></label></td>
        <td width="180" valign="top">Patient Record Lost</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofptrec2" id="chk_typeofptrec2" runat="server" /></td>
        <td width="180" valign="top">Release Patient Record</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_typeofptrec3" id="chk_typeofptrec3" runat="server" />
          </td>
        <td valign="top">Record Error</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_typeofptrec4" id="chk_typeofptrec4" runat="server" /></td>
        <td valign="top">Other</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        </tr>
      </table>      <label for="select20"></label></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">3. Electronic Documentation</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
          <asp:RadioButton ID="txtscanedoc1"  groupname="scanedoc" runat="server" />
      
          </td>
        <td width="180">Yes</td>
        <td width="23">  <asp:RadioButton ID="txtscanedoc2"  groupname="scanedoc" runat="server" /></td>
        <td>No</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">4. Type of Document</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
        <asp:RadioButton ID="txtbarcode1"  groupname="scanbarcode" runat="server" />
        
         </td>
        <td width="180">Bar code document</td>
        <td width="23">
         <asp:RadioButton ID="txtbarcode2"  groupname="scanbarcode" runat="server" />
       
          </td>
        <td>No bar code document</td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">5. Kind of Documentation</td>
    <td valign="top">
     <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_opddoc" id="chk_opddoc" runat="server" />
          
          </td>
        <td width="315" valign="top">OPD Document</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_ipddoc" id="chk_ipddoc" runat="server" /></td>
        <td valign="top">IPD Document</td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">1 
        
          <input type="text" name="chk_opddoc_n1" id="chk_opddoc_n1" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">1 
          <input type="text" name="chk_ipddoc_n1" id="chk_ipddoc_n1" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">2 
          <input type="text" name="chk_opddoc_n2" id="chk_opddoc_n2" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">2 
          <input type="text" name="chk_ipddoc_n2" id="chk_ipddoc_n2" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">3 
          <input type="text" name="chk_opddoc_n3" id="chk_opddoc_n3" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">3 
          <input type="text" name="chk_ipddoc_n3" id="chk_ipddoc_n3" style="width: 235px" runat="server" /></td>
      </tr>
      </table> <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_cardiacdoc" id="chk_cardiacdoc" runat="server" />
          </td>
        <td width="315" valign="top">Cardiac Investigation Form</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_filmdoc" id="chk_filmdoc" runat="server" /></td>
        <td valign="top">Film Document</td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">1 
          
          <input type="text" name="chk_cardiacdoc_n1" id="chk_cardiacdoc_n1" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">1 
          <input type="text" name="chk_filmdoc_n1" id="chk_filmdoc_n1" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top" ></td>
        <td valign="top" >2 
          <input type="text" name="chk_cardiacdoc_n1" id="chk_cardiacdoc_n2" style="width: 235px" runat="server" /></td>
        <td valign="top" ></td>
        <td valign="top" >2 
          <input type="text" name="chk_filmdoc_n2" id="chk_filmdoc_n2" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">3 
          <input type="text" name="chk_cardiacdoc_n3" id="chk_cardiacdoc_n3" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">3 
          <input type="text" name="chk_filmdoc_n3" id="chk_filmdoc_n3" style="width: 235px" runat="server" /></td>
      </tr>
      </table><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_labdoc" id="chk_labdoc" runat="server" />
          <label for="checkbox27"></label>
          <label for="radio15"></label></td>
        <td width="315" valign="top">Lab Document</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_otherdoc" id="chk_otherdoc" runat="server" /></td>
        <td valign="top">Other</td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">1 
          
          <input type="text" name="chk_labdoc_n1" id="chk_labdoc_n1" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">1 
          <input type="text" name="chk_otherdoc_n1" id="chk_otherdoc_n1" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">2 
          <input type="text" name="chk_labdoc_n2" id="chk_labdoc_n2" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">2 
          <input type="text" name="chk_otherdoc_n2" id="chk_otherdoc_n2" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"></td>
        <td valign="top">3 
          <input type="text" name="chk_labdoc_n3" id="chk_labdoc_n3" style="width: 235px" runat="server" /></td>
        <td valign="top">&nbsp;</td>
        <td valign="top">3 
          <input type="text" name="chk_otherdoc_n3" id="chk_otherdoc_n3" style="width: 235px" runat="server" /></td>
      </tr>
      </table>    
     
     
     </td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">6. Found the error by</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
          
        <td width="23"><asp:RadioButton ID="txtscanerrorby1" groupname="scanerrorby" runat="server" />
       
         </td>
        <td width="100">Patient</td>
        <td width="23">&nbsp;<label for="radio12"><asp:RadioButton ID="txtscanerrorby2" 
                groupname="scanerrorby" runat="server" />
       
            </label></td>
        <td width="100">Staff</td>
        <td width="23">&nbsp;<asp:RadioButton ID="txtscanerrorby3" 
                groupname="scanerrorby" runat="server" />
       
           </td>
        <td width="100">Physician</td>
        <td width="23"><asp:RadioButton ID="txtscanerrorby4" groupname="scanerrorby" 
                runat="server" />
       
          </td>
        <td>Others
         
          <input type="text" name="txtscanerrorby_other" id="txtscanerrorby_other" runat="server" /></td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7. The cause of error</td>
    <td valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7.1 Personal / Human Factor</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_h1" id="chk_scancause_h1" runat="server" />
      </td>
        <td width="315" valign="top">H1 Human error / Careless</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_h7" id="chk_scancause_h7" runat="server" /></td>
        <td valign="top">H7 Not refreshed scan</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_h2" id="chk_scancause_h2" runat="server" />
          <label for="checkbox49"></label></td>
        <td valign="top">H2 Wrong technique / Scan color</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_h8" id="chk_scancause_h8" runat="server" /></td>
        <td valign="top">H8 Mental lapse</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_h3" id="chk_scancause_h3" runat="server" />
          <label for="checkbox52"></label></td>
        <td valign="top">H3 Multi scan</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_h9" id="chk_scancause_h9" runat="server" /></td>
        <td valign="top">H9 Misread or didn't read</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_h4" id="chk_scancause_h4" runat="server" />
          <label for="checkbox55"></label></td>
        <td valign="top">H4 Lack of knowledge inexperience / New staffs</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_h10" id="chk_scancause_h10" runat="server" /></td>
        <td valign="top">H10 Key in wrong word</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_h5" id="chk_scancause_h5" runat="server" /></td>
        <td valign="top">H5 Use memory</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_h11" id="chk_scancause_h11" runat="server" /></td>
        <td valign="top">H11 Key in wrong numeral</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_h6" id="chk_scancause_h6" runat="server" /></td>
        <td valign="top">H6 Search by using too short alphabet</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_h12" id="chk_scancause_h12" runat="server" /></td>
        <td valign="top">H12 Others&nbsp;
          <input type="text" name="txtscancause_hother" id="txtscancause_hother" style="width: 180px" runat="server" /></td>
      </tr>
      </table></td>
    </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7.2 Poor Practice Habit</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_p1" id="chk_scancause_p1" runat="server" />
          <label for="checkbox51"></label>
          <label for="radio17"></label></td>
        <td width="315" valign="top">P1 Be in rushed / hurried</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_p6" id="chk_scancause_p6" runat="server" /></td>
        <td valign="top">P6 Incomplete patient identification</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_p2" id="chk_scancause_p2" runat="server" />
          <label for="checkbox53"></label></td>
        <td valign="top">P2 Assume (physician code)</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_p7" id="chk_scancause_p7" runat="server" /></td>
        <td valign="top">P7 Not verity document</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_p3" id="chk_scancause_p3" runat="server" />
          <label for="checkbox54"></label></td>
        <td valign="top">P3 Incomplete to check document</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_p8" id="chk_scancause_p8" runat="server" /></td>
        <td valign="top">P8 Kept document in wrong chart</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_p4" id="chk_scancause_p4" runat="server" />
         </td>
        <td valign="top">P4 Lack of correct data in computer</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_p9" id="chk_scancause_p9" runat="server" /></td>
        <td valign="top">P9 Verity physician name</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_p5" id="chk_scancause_p5" runat="server" /></td>
        <td valign="top">P5 1st default of physician's name and not changed name</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_p10" id="chk_scancause_p10" runat="server" /></td>
        <td valign="top">P10 Others&nbsp;
          <input type="text" name="txtscancause_pother" id="txtscancause_pother" style="width: 180px" runat="server" /></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7.3 Communication Breakdowns</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_c1" id="chk_scancause_c1" runat="server" />
          <label for="checkbox64"></label>
          <label for="radio17"></label></td>
        <td width="315" valign="top">C1 Language barrier</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_c4" id="chk_scancause_c4" runat="server" /></td>
        <td valign="top">C4 Lack of communication</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_c2" id="chk_scancause_c2" runat="server" />
          <label for="checkbox65"></label></td>
        <td valign="top">C2 Verbal miscommunication</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_c5" id="chk_scancause_c5" runat="server" /></td>
        <td valign="top">C5 Others&nbsp;
          <input type="text" name="txtscancause_cother" id="txtscancause_cother" style="width: 180px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_c3" id="chk_scancause_c3" runat="server" />
          <label for="checkbox66"></label></td>
        <td valign="top">C3 Misunderstanding</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7.4 System</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_s1" id="chk_scancause_s1" runat="server" />
          <label for="checkbox73"></label>
          <label for="radio17"></label></td>
        <td width="315" valign="top">S1 Frequent interruption and distractions</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_s4" id="chk_scancause_s4" runat="server" /></td>
        <td valign="top">S4 Manual process from code 6</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_s2" id="chk_scancause_s2" runat="server" />
          <label for="checkbox74"></label></td>
        <td valign="top">S2 Computer program (bug/error)</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_s5" id="chk_scancause_s5" runat="server" /></td>
        <td valign="top">S5 Intended to correct document by physician / patient</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_s3" id="chk_scancause_s3" runat="server" /></td>
        <td valign="top">S3 Doing more than one care in the same period</td>
        <td valign="top"><input type="checkbox" name="chk_scancause_s6" id="chk_scancause_s6" runat="server" /></td>
        <td valign="top">S6 Others&nbsp;
          <input type="text" name="txtscancause_sother" id="txtscancause_sother" style="width: 180px" runat="server" /></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">7.5 Miscellaneous / Others</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_scancause_m1" id="chk_scancause_m1" runat="server" />
      </td>
        <td valign="top">M1 
          <input type="text" name="txtscancausem1" id="txtscancausem1" style="width: 235px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_scancause_m2" id="chk_scancause_m2" runat="server" />
          <label for="checkbox34"></label></td>
        <td valign="top">M2
          <input type="text" name="txtscancausem2" id="txtscancausem2" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eaeaea">8. Correct document in computer system</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
         <asp:RadioButton ID="txtscancorrect1"  groupname="scancorrect" runat="server" />
        
          </td>
        <td width="180">Complete</td>
        <td width="23">  <asp:RadioButton ID="txtscancorrect2"  groupname="scancorrect" runat="server" />
          </td>
        <td>On process</td>
      </tr>
    </table></td>
  </tr>
          </table>