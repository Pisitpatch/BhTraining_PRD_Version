<%@ Page Title="" Language="VB" MasterPageFile="~/jci2013/JCIMasterPage.master" AutoEventWireup="false" CodeFile="form_detail.aspx.vb" Inherits="jci2013_form_detail" %>

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
      <a href="form_list.aspx?menu=3">Form Setting</a> 
        > Form Detail
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        
        <tr>
          <td width="180" valign="top" class="rowname">Form Name</td>
          <td valign="top"><asp:TextBox ID="txtname" runat="server" Width="80%"></asp:TextBox>
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
        <% If mode = "edit" Then%>
      
        <tr>
          <td valign="top" class="rowname" >&nbsp;</td>
          <td valign="top" >
              <asp:Button ID="cmdAddME" runat="server" Text="Custom Add ME" Visible="False" />
            &nbsp;<asp:Button ID="cmdDelME" runat="server" Text="Delete ME" Visible="False" />
              <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
              <asp:Button ID="cmdUpload" runat="server" Text="Upload XLSX" Visible="false" />
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" >&nbsp;</td>
          <td valign="top" >
              <asp:Label ID="lblNum" runat="server" Text="-"></asp:Label> Records
            </td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" >&nbsp;</td>
          <td valign="top" >
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" 
        EmptyDataText="There is no data." Visible="False">
        <Columns>
         <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:CheckBox ID="chk" runat="server" />
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="ME ">
            
                <ItemTemplate>
                    <asp:Label ID="lblPK" runat="server" Text='<%# Bind("me_id")%>' Visible="false"></asp:Label>
                    <a href="form_detail_edit.aspx?id=<% Response.Write(id)%>&menu=3&mode=<% Response.Write(mode)%>&subid=<%# Eval("me_id")%>">
                    <span style="color:green">
                    [<asp:Label ID="Label1" runat="server" Text='<%# Bind("chapter")%>'></asp:Label>,<asp:Label ID="Label2" runat="server" Text='<%# Bind("std_no")%>'></asp:Label>]
                    </span>
                    <span style="color:red">ME<asp:Label ID="Label3" runat="server" Text='<%# Bind("measure_element_no")%>' ></asp:Label></span>
                    ,<asp:Label ID="Label4" runat="server" Text='<%# Bind("measure_element_detail")%>'></asp:Label>
                    </a>
                   <br />
                    &nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Bind("criteria")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <RowStyle Height="40px" />
    </asp:GridView>
            </td>
        </tr>
        <%End If %>
      
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="green" />
          &nbsp;</td>
        </tr>
    </table>
</asp:Content>

