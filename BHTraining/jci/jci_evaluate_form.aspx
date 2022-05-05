<%@ Page Title="" Language="VB" MasterPageFile="~/jci/JCI_MasterPage.master" AutoEventWireup="false" CodeFile="jci_evaluate_form.aspx.vb" Inherits="jci_jci_evaluate_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%">
<tr>
<td width="50">1.</td>
<td><asp:Label ID="lblE1" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
</tr>
   
<tr>
<td width="50">&nbsp;</td>
<td>
<table width="100%">
<tr><td>
    <asp:RadioButtonList ID="txtanswer1" runat="server" 
        RepeatDirection="Horizontal" Width="300px">
        <asp:ListItem>5</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>1</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtanswer1" Display="Dynamic" 
        ErrorMessage="Please choose your answer"></asp:RequiredFieldValidator>
</td></tr>
</table>
</td>
</tr>
   
<tr>
<td width="50">2.</td>
<td>
    <asp:Label ID="lblE2" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    </td>
</tr>
   
<tr>
<td width="50">&nbsp;</td>
<td>
    <asp:RadioButtonList ID="txtanswer2" runat="server" 
        RepeatDirection="Horizontal" Width="300px">
        <asp:ListItem>ใช่ /Yes</asp:ListItem>
        <asp:ListItem>ไม่ใช่ /No</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtanswer2" Display="Dynamic" 
        ErrorMessage="Please choose your answer"></asp:RequiredFieldValidator>
    </td>
</tr>
   
<tr>
<td width="50">&nbsp;</td>
<td>
    &nbsp;</td>
</tr>
   
<tr>
<td width="50">&nbsp;</td>
<td>
    <asp:Button ID="cmdSubmit" runat="server" Font-Bold="True" Text="Submit" />
&nbsp;
    <asp:Button ID="cmdBack" runat="server" CausesValidation="False" 
        Text="Back to Test" />
    </td>
</tr>
   
</table>

</asp:Content>

