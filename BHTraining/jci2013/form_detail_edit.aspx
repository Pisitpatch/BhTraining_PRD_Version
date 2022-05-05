<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="form_detail_edit.aspx.vb" Inherits="jci2013_form_detail_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 34px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="admin_test.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        
        <tr>
          <td width="180" valign="top" class="rowname">Type</td>
          <td valign="top">
              <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
       
        <tr>
          <td valign="top" class="rowname">Edition </td>
          <td valign="top" >
              <asp:Label ID="lblEdition" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Section</td>
          <td valign="top" >
              <asp:Label ID="lblSectionNo" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Section Name</td>
          <td valign="top" >
              <asp:Label ID="lblSectionName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Chapter</td>
          <td valign="top" >
              <asp:Label ID="lblChapter" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Chapter Name</td>
          <td valign="top" >
              <asp:Label ID="lblChapterName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Goal</td>
          <td valign="top" >
              <asp:Label ID="lblGoal" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Standard No.</td>
          <td valign="top" >
              <asp:Label ID="lblStdNo" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Standard Detail</td>
          <td valign="top" >
              <asp:Label ID="lblStdName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">ME No.</td>
          <td valign="top" >
              <asp:Label ID="lblMeNo" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">ME Detail</td>
          <td valign="top" >
              <asp:Label ID="lblMEDetail" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Criteria</td>
          <td valign="top" >
              <asp:TextBox ID="txtcriteria" runat="server" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Method</td>
          <td valign="top" >
              <asp:TextBox ID="txtmethod" runat="server" Width="80%"></asp:TextBox>
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

