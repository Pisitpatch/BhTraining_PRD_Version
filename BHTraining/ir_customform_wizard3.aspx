<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="ir_customform_wizard3.aspx.vb" Inherits="ir_customform_wizard3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id = "data">
<div align="center" style="border:solid 1px #000000;margin-bottom:3px">
<asp:Label ID="txtheader1" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label><br />
<asp:Label ID="txtheader2" runat="server" Font-Bold="True" Font-Size="14pt"></asp:Label>
</div>
<table width="100%" style="border:solid 1px #000000">
<%  
    For i As Integer = 0 To dsForm.Tables(0).Rows.Count - 1
        
 %>
<tr>
<td><% Response.Write(getRow(dsForm.Tables(0).Rows(i)("element_order_sort").ToString))%></td>
</tr>
<% Next i%>
</table>
<%
    Try
        ' response.write("close connnection")
        conn.closeSql()
        conn = Nothing
    Catch ex As Exception
        Response.Write(ex.Message)
        'Response.Write(ex.Message)
    End Try
%>
</div>
<div align="right">
<asp:Button ID="cmdBack" runat="server" Text="<< Back" /> 
<asp:Button ID="cmdNext" runat="server" Text="Finish" /></div>
</asp:Content>

