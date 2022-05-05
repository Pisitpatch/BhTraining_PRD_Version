<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="form_authen.aspx.vb" Inherits="jci2013_form_authen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="form_list.aspx?menu=3">Form Setting</a> 
        > Form Authenticate
    </td>
  </tr>
</table>
     <fieldset>
  <legend>Edit Authenticate</legend>
     
      <table width="100%"  class="box-content" >
                <tr>
                  <td width="100" valign="top" class="rowname">&nbsp;</td>
                  <td>
                      &nbsp;</td>
                  </tr>
                    <tr>
                      <td valign="top" class="rowname"><strong>รายชื่อพนักงาน</strong></td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:TextBox ID="txtfind_name" runat="server" Width="80px"></asp:TextBox>
                                      <asp:Button ID="cmdFindName" runat="server" Text="Search" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" >
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtperson_all" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" Width="200px" Rows="10">
                                      </asp:ListBox>
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &gt;&gt;" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &lt;&lt;" />
                                  </td>
                                  <td valign="top" >
                                      <asp:ListBox ID="txtperson_select" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" Width="200px" Rows="5"></asp:ListBox>
                                    </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                  <tr>
                      <td valign="top" class="rowname">&nbsp;</td>
                      <td>
                          <asp:Button ID="cmdSave" runat="server" Text="Save" />
                          <asp:Button ID="cmdCancel" runat="server" Text="Cancel" />
                      </td>
                </tr>
                
                 
                  </table>
                  
  </fieldset>
</asp:Content>

