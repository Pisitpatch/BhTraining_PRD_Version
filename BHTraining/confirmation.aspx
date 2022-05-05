<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="confirmation.aspx.vb" Inherits="confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    <fieldset><legend>Confirm your information</legend>
<table width = "100%">
<tr><td width="150">Username</td><td>
    <asp:Label ID="lblUsername" runat="server"></asp:Label>
    </td></tr>
<tr><td width="200">Empolyee Code/MD Code</td><td>
    <asp:TextBox ID="txtcode" runat="server" BackColor="#FFFFCC"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtcode"
        ErrorMessage="Please enter your Employee Code or MD Code"></asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtcode"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
    </td></tr>
<tr><td width="150">Mobile Phone</td><td>
    <asp:TextBox ID="txtmobile" runat="server"></asp:TextBox>
    </td></tr>
<tr><td width="150">&nbsp;</td><td>
    <asp:Button ID="cmdSave" runat="server" Text="Confirm" Font-Bold="True" />
&nbsp;
    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" 
        CausesValidation="False" />
    </td></tr>
</table>
</fieldset>
</asp:Content>

