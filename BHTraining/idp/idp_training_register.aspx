<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_training_register.aspx.vb" Inherits="idp_idp_training_register" %>

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
<script type="text/javascript">
    function onRegister() {
        var result = false;
        if ($("#ctl00_ContentPlaceHolder1_txtmobile").val() == "") {
            alert("Please enter Contact Mobile No.");
            $("#ctl00_ContentPlaceHolder1_txtmobile").focus();
            return false;
        }
        result = confirm('Are you sure you want to register this Training Course ?')

        return result;
    }

</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="../images/doc01.gif" width="32" height="32" align="absmiddle" />&nbsp;&nbsp;Training Register</div>
<div id="data">
        <div class="tabber" id="mytabber2">
          <div class="tabbertab">
            <h2>Staff Information</h2>
            <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                <tr>
                  <td valign="top"><span class="theader"><strong>Internal Training No.</strong></span></td>
                  <td><strong><asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label>
                    
                      </strong>
                      </td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Division</strong></td>
                        <td width="230"><asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Cost Center</strong></td>
                        <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
              </table>
          </div>
          </div>
        <fieldset>
          <legend><strong>Registration Form</strong> </legend>
          <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td width="150" valign="top"><strong>Training Course</strong></td>
    <td valign="top">
        <asp:Label ID="lblTitle" runat="server" Text="Label" Font-Bold="true"></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Location</strong></td>
    <td valign="top" >
        <asp:Label ID="lblLocation" runat="server" Text="Label" ></asp:Label></td>
  </tr>
  <tr>
    <td valign="top"><strong>Date</strong></td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="250">
        <asp:Label ID="lblDate" runat="server" Text="Label" ></asp:Label></td>
        <td width="40"><strong>Time</strong></td>
        <td>
        <asp:Label ID="lblTime" runat="server" Text="Label" ></asp:Label></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top"><strong>Working Hour</strong></td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="25">
          &nbsp;<asp:RadioButton ID="txtwork1" GroupName="work" runat="server" />
         </td>
        <td width="90">Yes</td>
        <td width="25"><asp:RadioButton ID="txtwork2" GroupName="work" runat="server" />
          </td>
        <td>No</td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top" class="auto-style1"><strong>Contact Mobile</strong></td>
    <td valign="top" class="auto-style1">
        <asp:TextBox ID="txtmobile" runat="server"></asp:TextBox> <span style="color:red"> **  Your mobile is needed. Reminder will be sent to you via SMS</span>
      </td>
  </tr>
  <tr>
    <td valign="top"><strong>Contact Email</strong></td>
    <td valign="top">
      <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>   &nbsp;</td>
  </tr>
  <tr>
    <td valign="top"><strong>Remark</strong></td>
    <td valign="top">
        <asp:TextBox ID="txtremark" runat="server" Rows="4" TextMode="MultiLine" 
            Width="80%"></asp:TextBox>
      </td>
  </tr>
  </table>
</fieldset>
        <br />
      <div align="center">&nbsp;
          <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label><br />
      <asp:Button ID="cmdRegister" runat="server" Text="Register" Font-Bold="true"  OnClientClick="return onRegister();"
              Width="150px" />
      <asp:Button ID="cmdCancel" runat="server" Text="Cancel Registration" 
              ForeColor="Red" Font-Bold="true"  OnClientClick="return confirm('Are you sure you want to cancel this Training Course ?')"
              Width="150px" Visible="False" />
&nbsp;<asp:Button ID="cmdBack" runat="server" Text="Back" Width="150px" />
      </div>
      </div>
</asp:Content>

