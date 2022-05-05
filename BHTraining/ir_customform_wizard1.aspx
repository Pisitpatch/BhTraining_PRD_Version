<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="ir_customform_wizard1.aspx.vb" Inherits="ir_customform_wizard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="10">&nbsp;</td>
      <td valign="top"><br />
        <table width="100%"  border="0" cellspacing="0" cellpadding="3">
          <tr>
            <td height="25" background="images/header_bkgd.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td class="green"><img src="images/head_lefto.gif" width="5" height="24" align="absmiddle" /> mm</td>
              </tr>
              <tr>
                <td height="6" background="images/shadowbg.gif"><img src="images/shadow.gif" width="150" height="6" /></td>
              </tr>
            </table></td>
          </tr>
        </table>
<table width="100%"  border="0" cellspacing="0" cellpadding="3"  >
          <tr>
            <td height="5" bgcolor="#F7941D"></td>
            <td ></td>
            <td ></td>
            <td ></td>
          </tr>
          <tr>
            <td width="25%" height="25" bgcolor='#FFFFFF' style='font-weight:bold'>1.Add/Edit form</td>
            <td width="25%" bgcolor='#EFEFEF'>2. Add/Edit element</td>
            <td width="25%" bgcolor='#EFEFEF'>3. Preview your form</td>
            <td width="25%" bgcolor='#EFEFEF'>4. Finish</td>
          </tr>
        </table>
        </td>
      <td width="10">&nbsp;</td>
    </tr>
  </table>
  <br/>
  <div id="data">
  
  <table width="100%" bgcolor="#EEEEEE">
  <tr>
  <td width="150">&nbsp;</td>
  <td>
      &nbsp;</td>
  </tr>
  <tr>
  <td width="150">Form name (th)</td>
  <td>
      <asp:TextBox ID="txtnameth" runat="server"></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td width="150">Form name (en)</td>
  <td>
      <asp:TextBox ID="txtnameen" runat="server"></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td width="150">Category</td>
  <td>
      <asp:DropDownList ID="txtcategory" runat="server">
      </asp:DropDownList>
      </td>
  </tr>
  <tr>
  <td width="150">Description</td>
  <td>
      <asp:TextBox ID="txtdesc" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td width="150">&nbsp;</td>
  <td>
      &nbsp;</td>
  </tr>
  </table>
      <asp:Panel ID="PanelStep1" runat="server">
     
  <br />
  <div align="right" style="width:100%">
      <asp:Button ID="cmdBack" runat="server" 
          Text="Back"  /> 
      <asp:Button ID="cmdCreateForm" runat="server" Text="Next &gt;&gt;" />
          </div>
      </asp:Panel>
       <asp:Panel ID="PanelStep2" runat="server">
       </asp:Panel>
  </div>
</asp:Content>

