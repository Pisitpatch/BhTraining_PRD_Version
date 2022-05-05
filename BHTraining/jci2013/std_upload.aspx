﻿<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="std_upload.aspx.vb" Inherits="jci2013_std_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="std_list.aspx?menu=2">Standard Setting</a> > Upload Excel
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        
        <tr>
          <td width="180" valign="top" class="rowname">Description</td>
          <td valign="top"><asp:TextBox ID="txtdesc" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname">Edition</td>
          <td valign="top">
              <asp:TextBox ID="txtedition" runat="server" MaxLength="3" Width="50px">5</asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname">File :</td>
          <td valign="top">
              <asp:FileUpload ID="FileUpload1" runat="server" Width="80%" />
            </td>
        </tr>
        <tr>
          <td valign="top" class="rowname" style="height: 34px">Status :</td>
          <td valign="top" class="auto-style1">
              <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                  <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                  <asp:ListItem Value="0">Inactive</asp:ListItem>
              </asp:RadioButtonList>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="green" />
          &nbsp;</td>
        </tr>
    </table>
</asp:Content>

