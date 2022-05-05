<%@ Page Language="VB" AutoEventWireup="false" CodeFile="mco_popup.aspx.vb" Inherits="cfb_mco_popup" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
    <script type="text/javascript">
        function IAmSelected(source, eventArgs) {
            // alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
            // $("#ctl00_ContentPlaceHolder1_txtmdcode").attr("disabled", false);
            $("#txtpsm_add_special").val(eventArgs.get_value());
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <div>
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
   <asp:Panel ID="panel_psm_concern" runat="server" 
                    BackColor="#CCCCFF">
               <table width="100%" cellpadding="2" cellspacing="1" style="margin: 8px 10px;">
            <tr>
                <td valign="top">Concern detail</td>
                <td valign="top">
                    <textarea ID="txtpsm_concern" cols="45" name="txtpsm_concern" rows="5" runat="server"
                        style="width: 535px"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Final comment category</td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="300">
                        <asp:DropDownList ID="txtpsm_category" runat="server" Width="285px" 
                            AutoPostBack="True" DataTextField="psm_category_name" 
                            DataValueField="psm_category_id">
                        </asp:DropDownList>
                      </td>
                   
                  </tr>
                </table></td>
              </tr>
                   <tr>
                       <td valign="top">
                           Subcategory</td>
                       <td valign="top">
                           <asp:DropDownList ID="txtpsm_subcategory" runat="server" 
                               DataTextField="subcategory_name" DataValueField="psm_sub_category_id" 
                               Width="285px">
                           </asp:DropDownList>
                       </td>
                   </tr>
              <tr>
                <td valign="top">Standard of care</td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="20"><asp:RadioButton ID="txtpsm_std1" runat="server" GroupName="psm_std" /></td>
                     
                    <td width="50">Yes</td>
                    <td width="20">
                        <asp:RadioButton ID="txtpsm_std2" runat="server" GroupName="psm_std" />
                      </td>
                    <td width="50">No</td>
                    <td width="20">
                        <asp:RadioButton ID="txtpsm_std3" runat="server" GroupName="psm_std" />
                      </td>
                    <td>Borderline</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">Defendant&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Physician</span></td>
                <td valign="top">
                  <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="370">
                          <asp:TextBox ID="txtpsm_add_doctor" runat="server" Width="335px" ></asp:TextBox>
                       <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
            DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServicePath="~/DoctorService.asmx" ServiceMethod="getDoctor2" CompletionSetCount="5" 
            TargetControlID="txtpsm_add_doctor"  EnableViewState="false" OnClientItemSelected="IAmSelected" >
        </asp:AutoCompleteExtender>
&nbsp;<img src="../images/spellcheck.png" width="16" height="16" /></td>
                      <td>
                          <asp:Button ID="cmdPSMAddDoc" runat="server" Text="Add" />
                          &nbsp;<asp:Button ID="cmdPSMDelDoc" runat="server" Text="Delete" />
                        </td>
                     
                    </tr>
                     
                  </table>

                 </td>
              </tr>
                   <tr>
                       <td valign="top">
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Specialty</span></td>
                       <td valign="top">
                           <input name="txtpsm_add_special" type="text" id="txtpsm_add_special" style="width: 335px;" runat="server" />
                       </td>
                   </tr>
                   <tr>
                       <td valign="top">
                           &nbsp;</td>
                       <td valign="top">
                           <asp:ListBox ID="txtpsm_list_doctor" runat="server" DataTextField="concern_doctor" 
                               DataValueField="concern_doctor" Width="535px"></asp:ListBox>
                       </td>
                   </tr>
              <tr>
                <td valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-decoration: underline">Department</span></td>
                <td valign="top">
                    <asp:DropDownList ID="txtpsm_add_dept" runat="server" DataTextField="dept_name_en" DataValueField="dept_id" Width="400px">
                    </asp:DropDownList>
                                   &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="cmdPSMAddDept" runat="server" Text="Add"  />
                
                    <asp:HiddenField ID="txtpsm_add_deptid" runat="server" /> <asp:Button ID="cmdPSMRemoveDept" runat="server" 
                        Text="Delete"  />
                
                    
                
                  </td>
              </tr>
                   <tr>
                       <td valign="top">
                           &nbsp;</td>
                       <td valign="top">
                           <asp:ListBox ID="txtpsm_list_dept" runat="server" Width="535px"  DataTextField="concern_dept_name" DataValueField="costcenter_id"></asp:ListBox>
                       </td>
                   </tr>
              <tr>
                <td colspan="2" valign="top">
                 <asp:Button ID="cmdSaveConcenrn" runat="server" Text="Save" Font-Bold="True" OnClientClick="alert('Save completed');" />
                    &nbsp;<input type="button" value="Close" style="width: 100px;"  onclick="window.close();" />
               </td>
              </tr>
              <tr>
                <td colspan="2" valign="top"></td>
              </tr>
            </table>
              </asp:Panel>
    </ContentTemplate>
                </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
