<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_message.aspx.vb" Inherits="idp_idp_message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../js/jquery.countdown.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#counter').countdown({
                image: '../images/digits.png',
               // startTime: '01:12:12:00'
                startTime: '<% Response.Write(counter_date)%>'
            });
            /*
            $('#counter_2').countdown({
                image: '../images/digits.png',
                startTime: '00:10',
                timerEnd: function () { alert('end!'); },
                format: 'mm:ss'
            });
            */
        });
    </script>

        <style type="text/css">
      br { clear: both; }
      .cntSeparator {
        font-size: 54px;
        margin: 10px 7px;
        color: #000;
      }
      .desc { margin: 7px 3px; }
      .desc div {
        float: left;
        font-family: Arial;
        width: 70px;
        margin-right: 65px;
        font-size: 13px;
        font-weight: bold;
        color: #000;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
  

<div id="header"><img src="../images/menu_05.png" alt="SSIP" width="32" height="32"  />&nbsp;News and Event</div>
<div id="data">
    <!--
    <table width="600" align="center"><tr><td align="center">
        <div id="counter"></div>
  <div class="desc">
    <div>Day</div>
    <div>Hour</div>
    <div>Minute</div>
    <div>Second</div>
  </div>

                            </td></tr></table>
       
       
 <br />
    -->
<asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>

</div>
</asp:Content>


