<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_message2.aspx.vb" Inherits="idp_idp_message2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <% If (id = 2) Then%>
    <script type="text/javascript">
        <% If is_delay = "1" Then%>
        var i = 1;
        var global_counter;
        var counter_sec = 30;

        $(document).ready(function(){
   
            $("#ctl00_ContentPlaceHolder1_cmdNext").hide();
            $("#ctl00_ContentPlaceHolder1_cmdNext2").hide();
            
            counter();
        });

        function counter() {
            global_counter = window.setTimeout("counter()", 1000);
            //alert(1);
           
            $("#interval").html("Please wait ... (" + (counter_sec - i) + ")");


            i++;
            if (i > counter_sec) {
                $("#interval").html("");
                $("#ctl00_ContentPlaceHolder1_cmdNext").show();
                $("#ctl00_ContentPlaceHolder1_cmdNext2").show();
                window.clearTimeout(global_counter);
            }
        }
  <% end if %>
    </script>
    <% end if %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="../images/menu_05.png" alt="SSIP" width="32" height="32"  />&nbsp;Information</div>
<div id="data">
       <span id="interval" style="color:red"></span>
    <div align="center">
<asp:Button ID="cmdNext" runat="server" Text="Next >>" />
</div>
    <br />
<asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>
     <div align="center">
    <asp:Button ID="cmdNext2" runat="server" Text="Next >>" />
         </div>
</div>
</asp:Content>

