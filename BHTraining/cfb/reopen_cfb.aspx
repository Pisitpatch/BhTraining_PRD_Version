<%@ Page Title="" Language="VB" MasterPageFile="~/cfb/CFB_MasterPage.master" AutoEventWireup="false" CodeFile="reopen_cfb.aspx.vb" Inherits="cfb_reopen_cfb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%">
<tr><td width="120" style="vertical-align:top">
    CFB No.</td>
    <td width="80%">
        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
    </td>
    </tr>
<tr><td width="120" style="vertical-align:top">
    Date Report</td>
    <td width="80%">
        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
    </td>
    </tr>
<tr><td width="120" style="vertical-align:top">
    <asp:Label ID="lblIRReopen1" runat="server" Text="Reason to re-open"></asp:Label></td>
    <td width="80%">
        <asp:TextBox ID="txtadd_reason" runat="server" TextMode="MultiLine" Rows="15" Width="80%"></asp:TextBox></td>
    </tr>
<tr><td width="120" style="vertical-align:top">
    &nbsp;</td>
    <td width="80%">
        <asp:Button ID="cmdSave" runat="server" Text="Confirm to reopen" Font-Bold="true" />
&nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel" />
    </td>
    </tr>
</table>
</asp:Content>

