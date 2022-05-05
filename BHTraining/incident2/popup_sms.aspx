<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_sms.aspx.vb" Inherits="incident_popup_sms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <script type="text/javascript">
         $(document).ready(function () {
             window.opener.loadPopup(1);
             addEvent();
         });
     </script>

     <script type="text/javascript">
      function returnValue() {
          var str = $("#txtselect").val()
          try {
       
        //  alert( $("#txtidselect").val());
              window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtsend_sms').value = $("#txtselect").val();
               window.opener.document.getElementById('ctl00_ContentPlaceHolder1_txtsend_idsms').value = $("#txtidselect").val();

             
          } catch (e) {
          alert(e);
          return;
         // window.opener.document.getElementById('ctl00_ContentPlaceHolder1_TabContainer1_txttab_alert_txtto').value = $("#txtselect").val();
          }

        window.opener.disablePopup() 
        window.close();
    }

   
    var handler = function() {
        window.opener.disablePopup();
    };

    function addEvent() {
        $(window).unload(function () {
       //     window.opener.disablePopup();
        });
    }

    function removeEvent() {
        $(window).unload(function () {
           // alert('Handler for .unload() called.');
        });
    }

    function onSendSMS() {
        	$.ajax({
   			type: "POST",
   			url: "../ajax_update_visit2.php",
			//url: "http://top.com/website/addlife/crm/ajax_get_user_ingroup.php",
  			data: {field: this_field  },
			success: function(data){
				if (data != ""){
					alert(data);
				}else{
					//$("#" + this_alert).fadeIn();
					//alert("Status has been changed to " );	
			}
			},
  			error: function(e){alert("error :: " +e);}
	});			
    }
  </script>
</head>
<body id="iBody"  >
    <form id="form1" runat="server">
    <div>
    <div class="alert">
           
<div class="heading">SMS Notification</div>
<div style="padding: 0px 10px;">
<div style="margin-top: 15px; margin-bottom: 10px;">
   </div>
<fieldset>
  <legend>Select BH Addresses</legend>
  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblError" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label>
  <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;" >
    <tr>
       
      <td valign="top">
        <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td colspan="3">
                <table width="100%"><tr><td width="300">
         
                    <asp:TextBox ID="txtfind" runat="server"></asp:TextBox>       
                   <asp:Button ID="cmdSearch" runat="server" Text="Search" 
                        CausesValidation="False" OnClientClick="removeEvent()" /></td>
                <td>
                
&nbsp;</td>
                </tr><tr><td>&nbsp;</td>
                <td>
             
                    
                 

                        </td>
                </tr></table>
                
              

                  </td>
                </tr>
              <tr>
                <td class="style3">Mobile No. Search results</td>
                <td style="text-align: center;">&nbsp;</td>
                <td>TO </td>
              </tr>
              <tr>
                <td style="vertical-align:top" class="style3">
                    <asp:ListBox ID="ListBox1" runat="server" Rows="10" Width="300px" 
                        DataTextField="FullName" DataValueField="custom_mobile"></asp:ListBox>
                        <br /><br />
                  

                    </td>
                <td width="20" style="text-align: center;">
                  </td>
                <td style="vertical-align:top">
                <table>
                <tr><td width="50"><asp:Button ID="cmdSelect" runat="server" Text=">" 
                        CausesValidation="False" />
                  <br />
                   <asp:Button ID="cmdRemove" runat="server" Text="<" CausesValidation="False" /></td>
                    <td>
                        <asp:ListBox ID="ListBox2" runat="server" Rows="10" Width="300px">
                        </asp:ListBox>
                    </td>
                    </tr>
                
                 
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                </td>
                </tr>
              <tr>
                <td class="style3">
                   
                    <asp:HiddenField ID="txtselect" runat="server" />
                    <asp:HiddenField ID="txtidselect" runat="server" />
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
    </div>
    </form>
</body>
</html>
