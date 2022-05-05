<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="std_detail.aspx.vb" Inherits="jci2013_std_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="admin_test.aspx">Main</a>
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        
        <tr>
          <td width="180" valign="top" class="rowname">Type</td>
          <td valign="top">
              <asp:TextBox ID="lblType" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
          <td valign="top" class="rowname">Edition </td>
          <td valign="top" >
              <asp:TextBox ID="lblEdition" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Section</td>
          <td valign="top" >
              <asp:TextBox ID="lblSectionNo" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Section Name</td>
          <td valign="top" >
              <asp:TextBox ID="lblSectionName" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Chapter</td>
          <td valign="top" >
              <asp:TextBox ID="lblChapter" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Chapter Name</td>
          <td valign="top" >
              <asp:TextBox ID="lblChapterName" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Goal</td>
          <td valign="top" >
              <asp:TextBox ID="lblGoal" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Standard No.</td>
          <td valign="top" >
              <asp:TextBox ID="lblStdNo" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">Standard Detail</td>
          <td valign="top" >
              <asp:TextBox ID="lblStdName" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">ME No.</td>
          <td valign="top" >
              <asp:TextBox ID="lblMeNo" runat="server" Text="" Width="80%"></asp:TextBox>
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">ME Detail</td>
          <td valign="top" >
              <asp:TextBox ID="lblMEDetail" runat="server" Text="" Width="80%"></asp:TextBox>
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

