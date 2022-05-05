<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_recepient.aspx.vb" Inherits="incident.incident_popup_recepient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Contact</title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <script type="text/javascript">
      function returnValue() {
          var str = $("#txtselect").val()
          try {
          <%if popupType = "to" then %>
        //  alert( $("#txtidselect").val());
              window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtto').value = $("#txtselect").val();
               window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtidselect').value = $("#txtidselect").val();

                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtcc').value = $("#txtccselect").val();
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtidCCselect').value = $("#txtidccselect").val();

           //    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtbc').value = $("#txtbccselect").val();
         //  alert( $("#txtidbccselect").val());
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtidBCCSelect').value = $("#txtidbccselect").val();
          <%else %>
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtcc').value = $("#txtselect").val();
             window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtidCCselect').value = $("#txtidselect").val();
          <%end if %>
          } catch (e) {
          alert(e);
          return;
         // window.opener.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_txttab_alert_txtto').value = $("#txtselect").val();
          }
         
         window.opener.disablePopup()
        window.close();
  }
  </script>
       <style type="text/css">
           .style2
           {
               height: 21px;
           }
           .style3
           {
               width: 300px;
           }
       </style>
</head>
<body >
    <form id="form1" runat="server">
    <div class="alert">
           
<div class="heading">Email Notification</div>
<div style="padding: 0px 0px;">
<fieldset>
  <legend>Select BH Addresses</legend>
  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblError" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label>
  <table width="100%" cellspacing="1" cellpadding="2">
    <tr>
       
      <td valign="top">
        <table width="100%" cellspacing="0" cellpadding="3">
              <tr>
                <td colspan="3">
                <table width="100%"><tr><td width="300"><asp:DropDownList ID="txtdeptlist" runat="server" DataTextField="dept_name_en" 
                        DataValueField="dept_id" Visible="False">
                  </asp:DropDownList>
         
                    <asp:TextBox ID="txtfind" runat="server"></asp:TextBox>       
                   <asp:Button ID="cmdSearch" runat="server" Text="Search" 
                        CausesValidation="False" /></td>
                <td>
                
                    <asp:TextBox ID="txtnewgroup" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="cmdSaveGroup" runat="server" CausesValidation="False" 
                        Text="Create New Email group" />
                
                </td>
                </tr><tr><td>&nbsp;</td>
                <td>
                <table width="100%">
                <tr>
                <td>   
                    <asp:listbox ID="txtgroup" runat="server"
                        DataTextField="contact_group_name" DataValueField="contact_id" Rows="5" 
                        Width="240" AutoPostBack="True">
                    </asp:listbox></td>
                <td style="vertical-align:top; text-align:left">&nbsp;<asp:Button ID="cmdSelectGroup" runat="server" CausesValidation="False" Text="Select" />
                        &nbsp;<asp:Button ID="cmdEditGroup" runat="server" Text="Save" 
                        CausesValidation="False"  OnClientClick="return confirm('Are you sure you want to save?')"  />
&nbsp;<asp:Button ID="cmdDeleteGroup" runat="server" Text="Delete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete?')" /></td>
                </tr>
                </table>
                    
                 

                        </td>
                </tr></table>
                
              

                  </td>
                </tr>
              <tr>
                <td width="300" class="style3">Internal Email Search results</td>
                <td style="text-align: center;">&nbsp;</td>
                <td>TO </td>
              </tr>
              <tr>
                <td width="300" class="style3" style="vertical-align:top">
                    <asp:ListBox ID="ListBox1" runat="server" Rows="10" Width="300px" 
                        DataTextField="FullName" DataValueField="Email"></asp:ListBox>
                        <br />
                        <br />
                    
                </td>
                <td width="20" style="text-align: center;">
                </td>
                <td>
                <table>
                <tr><td width="50"><asp:Button ID="cmdSelect" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemove" runat="server" Text="<" CausesValidation="False" /></td>
                    <td>
                        <asp:ListBox ID="ListBox2" runat="server" Rows="5" Width="300px">
                        </asp:ListBox>
                    </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            CC</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="cmdSelect0" runat="server" Text="&gt;" 
                                CausesValidation="False" />
                            <br />
                            <asp:Button ID="cmdRemove0" runat="server" Text="&lt;" 
                                CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ListBox ID="ListBox3" runat="server" Rows="5" Width="300px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2" colspan="2">
                            BCC</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="cmdSelect1" runat="server" Text="&gt;" 
                                CausesValidation="False" />
                            <br />
                            <asp:Button ID="cmdRemove1" runat="server" Text="&lt;" 
                                CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ListBox ID="ListBox4" runat="server" Rows="5" Width="300px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                      </td>
                        <td>&nbsp;
                      </td>
                    </tr>
                </table>
                </td>
              </tr>
              <tr>
                <td class="style3">
                   
                    <asp:HiddenField ID="txtselect" runat="server" />
                    <asp:HiddenField ID="txtidselect" runat="server" />

                      <asp:HiddenField ID="txtccselect" runat="server" />
                    <asp:HiddenField ID="txtidccselect" runat="server" />

                      <asp:HiddenField ID="txtbccselect" runat="server" />
                    <asp:HiddenField ID="txtidbccselect" runat="server" />

                        <asp:HiddenField ID="txtotherselect" runat="server" />
                    <asp:HiddenField ID="txtidotherselect" runat="server" />
                  </td>
                <td style="text-align: center;">&nbsp;</td>
                <td><span style="margin-top: 15px; margin-bottom: 10px;">
                    <asp:Button ID="cmdSave" runat="server"  Text="Select" Width="90px" 
                        OnClientClick="returnValue()" Font-Bold="True" CausesValidation="False" />
                  <input type="button" value="Cancel" style="width: 90px;"  onclick="window.opener.disablePopup();window.close();" />
                </span></td>
              </tr>
              </table>
        
     </td>
    </tr>
    </table>

</fieldset>
</div>

</div>
    </form>
</body>
</html>
