<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="division_detail.aspx.vb" Inherits="division_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
   <div id="header"><img src="images/menu_12.png" width="32" height="32"  />&nbsp;&nbsp;Division Detail</div>
    
      <div id="data">
      <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
      <tr><td width="250">Division name (TH)</td><td>
          <asp:TextBox ID="txtdivision_th" runat="server"></asp:TextBox>
          </td>
      </tr>
      <tr><td >Division name (EN)</td><td>
          <asp:TextBox ID="txtdivision_en" runat="server"></asp:TextBox>
          </td>
      </tr>
      <tr><td >Associate Division Director</td><td>
          <asp:TextBox ID="txtass_director" runat="server" Width="300px" BackColor="#FFFF99"></asp:TextBox>
          <asp:AutoCompleteExtender ID="txtass_director_AutoCompleteExtender" 
              runat="server" DelimiterCharacters="" Enabled="True" 
              ServiceMethod="getEmployee" ServicePath="EmployeeService.asmx" CompletionSetCount="5" 
              TargetControlID="txtass_director"  MinimumPrefixLength="1">
          </asp:AutoCompleteExtender>
          </td>
      </tr>
      <tr><td >Director / Associate Division Director</td><td>
          <asp:TextBox ID="txtdirector" runat="server" Width="300px" BackColor="#FFFF99"></asp:TextBox>
           <asp:AutoCompleteExtender ID="AutoCompleteExtender1" 
              runat="server" DelimiterCharacters="" Enabled="True" 
              ServiceMethod="getEmployee" ServicePath="EmployeeService.asmx" CompletionSetCount="5" 
              TargetControlID="txtdirector"  MinimumPrefixLength="1">
          </asp:AutoCompleteExtender>
          </td>
      </tr>
      <tr><td >Above Associate Division Director&nbsp;</td><td>
          <asp:TextBox ID="txtass_chief" runat="server" Width="300px" BackColor="#FFFF99"></asp:TextBox>
           <asp:AutoCompleteExtender ID="AutoCompleteExtender2" 
              runat="server" DelimiterCharacters="" Enabled="True" 
              ServiceMethod="getEmployee" ServicePath="EmployeeService.asmx" CompletionSetCount="5" 
              TargetControlID="txtass_chief"  MinimumPrefixLength="1">
          </asp:AutoCompleteExtender>
          </td>
      </tr>
      <tr><td >Chief&nbsp;</td><td>
          <asp:TextBox ID="txtchief" runat="server" Width="300px" BackColor="#FFFF99"></asp:TextBox>
           <asp:AutoCompleteExtender ID="AutoCompleteExtender3" 
              runat="server" DelimiterCharacters="" Enabled="True" 
              ServiceMethod="getEmployee" ServicePath="EmployeeService.asmx" CompletionSetCount="5" 
              TargetControlID="txtchief"  MinimumPrefixLength="1">
          </asp:AutoCompleteExtender>
          </td>
      </tr>
      <tr><td >&nbsp;</td><td>
          <asp:Button ID="cmdSave" runat="server" Text="Save" />
          &nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel" />
          </td>
      </tr>
      </table>
      </div>
</asp:Content>

