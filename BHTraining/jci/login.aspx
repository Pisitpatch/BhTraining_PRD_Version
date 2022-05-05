<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="jci_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="panel_login" runat="server" DefaultButton="cmdLogin">
    <table id="signin" width="500" align="center" cellpadding="5" cellspacing="0" style="border: solid 1px #CCCCCC; background: #FFFFFF;">
    
    <tr>
      <td style="background: url(../images/boxheader.gif); font-size: 15px; font-weight: bold;">Welcome to Bumrungrad Way Training</td>
    </tr>
    <tr>
      <td>Please enter your Employee ID:<br />
       <asp:TextBox  ID= "txtusername" runat="server" Width="185px"></asp:TextBox>
       <br />
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtusername" ErrorMessage="Please enter your Employee ID"></asp:RequiredFieldValidator>
        </td>
    </tr>
 
 
    <tr>
      <td>
         
          <asp:Label ID="lblError" runat="server" ForeColor="#FF3300" Text=""></asp:Label>
         
        </td>
    </tr>
    <tr>
      <td style="background: #f0f0f0; text-align: left;">
          <asp:Button ID="cmdLogin" runat="server" Text="Sign In" 
              CssClass="button-green" />  
        &nbsp;</td>
    </tr>
  </table>
  </asp:Panel>
  <br />
<div style="width:100%" align="center">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
     
      <td  valign="top">        <script type="text/javascript" src="../js/media_play.js" charset="utf-8"></script>
 <script type="text/javascript" src="../js/media_chili.js" charset="utf-8"></script>
<script type="text/javascript">

    $(function () {

        //$.fn.media.mapFormat('avi','quicktime');

        // this one liner handles all the examples on this page

        $('a.media').media();

    });

</script>
<table width="500" align="center" cellpadding="6" cellspacing="1">
        <tr>
          <th colspan="2" align="left" style="background: #409997; color: #FFF; font-weight: normal; font-size: 16px; text-align: left; padding: 6px 10px;"><strong>Help  : วิธีการใช้งาน</strong></th>
        </tr>
        <tr>
          <td colspan="2" align="left" style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #909e91;">วิธีการตอบคำถาม</td>
        </tr>
        <tr>
          <td colspan="2" align="left" bgcolor="#FFFFFF">1. <a href="login.htm">การเข้าใช้งานระบบ</a></td>
        </tr>
        <tr>
          <td colspan="2" align="left" bgcolor="#FFFFFF">2. <a href="irreport.htm">การตอบคำถาม</a></td>
        </tr>
        <tr>
          <td colspan="2" align="left" style="background: #f0f0f0; font-weight: bold; font-size: 15px; color: #909e91;">ตัวอย่างการตอบคำถาม</td>
        </tr>
        <tr>
          <td width="50%" align="center" bgcolor="#FFFFFF"><a class="media {width:200, height:150}" href="mediaplayer.swf?file=flash/demo2.flv"></td>
          <td width="50%" align="center" bgcolor="#FFFFFF"><a class="media {width:200, height:150}" href="mediaplayer.swf?file=flash/demo1.flv">  </td>                
        </tr>
        
        <tr>
          <td colspan="2"></td>
        </tr>
      </table></td>
    </tr>
  </table>
  </div>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
</asp:Content>

