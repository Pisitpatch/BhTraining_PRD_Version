<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="doctor_detail.aspx.vb" Inherits="doctor_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
 <table>
 <tr>
 <td width="80%" style="vertical-align:top"><table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr>
    <td colspan="4" class="theader"><img src="images/sidemenu_circle.jpg" width="10" height="10" />&nbsp;Personal 
        Information</td>
    </tr>
  <tr>
    <td width="150" >MD Code</td>
    <td width="250">
        <asp:Label ID="txtid" runat="server"></asp:Label>
      </td>
    <td width="150">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td class="style2"  >Specialty</td>
    <td class="style2" >
        <asp:Label ID="txthn" runat="server"></asp:Label>
      </td>
    <td class="style2"></td>
    <td class="style2">
        </td>
  </tr>
  
  <tr>
    <td>Status</td>
    <td >
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Enable</asp:ListItem>
            <asp:ListItem Value="0">Disable</asp:ListItem>
        </asp:RadioButtonList>
      </td>
    <td >&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>Module Access</td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%" >
                    <b>Available Module</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    &nbsp;</td>
                <td width="40%">
                    <b>Granted Module(s)</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtmodule1" runat="server" Width="90%" 
                        DataTextField="module_name" DataValueField="module_id" BackColor="#FFFFCC"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center">
                    <asp:Button ID="cmdAddModule" runat="server" Text=">" />
                    <br />
                    <asp:Button ID="cmdRemoveModule" runat="server" Text="<" />
                   </td>
                <td width="40%">
                    <asp:ListBox ID="txtmodule2" runat="server" Width="90%" 
                        DataTextField="module_name" DataValueField="module_id"></asp:ListBox>
                </td>
            </tr>
          
        </table>
      </td>
  </tr>
  <tr>
    <td>Roles</td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%">
                    <b>Available Roles</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    &nbsp;</td>
                <td width="40%">
                    <b>Granted Role(s)</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtrole1" runat="server" Width="90%" DataTextField="role_name" 
                        DataValueField="role_id" BackColor="#FFFFCC"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center">
                    <asp:Button ID="cmdAddRole" runat="server" Text=">" />
                    <br />
                    <asp:Button ID="cmdRemoveRole" runat="server" Text="<" />
                   </td>
                <td width="40%">
                    <asp:ListBox ID="txtrole2" runat="server" Width="90%" DataTextField="role_name" 
                        DataValueField="role_id"></asp:ListBox>
                </td>
            </tr>
          
        </table></td>
  </tr>
   <tr>
    <td>Costcenter Access</td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%">
                    <b>Available Costcenter</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    &nbsp;</td>
                <td width="40%">
                    <b>Granted Costcenter(s)</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtcc" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id" BackColor="#FFFFCC"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center">
                    <asp:Button ID="cmdAddCC" runat="server" Text=">" />
                    <br />
                    <asp:Button ID="cmdRemoveCC" runat="server" Text="<" />
                   </td>
                <td width="40%">
                    <asp:ListBox ID="txtcc2" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id"></asp:ListBox>
                </td>
            </tr>
          
        </table></td>
  </tr>
  <tr>
    <td>Create date</td>
    <td >
        &nbsp;</td>
    <td >Last Login</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td >
        &nbsp;</td>
    <td >&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="4" align="center" style="text-align:center">
        <asp:Button ID="cmdSave" Text="Save" runat="server" 
            Font-Bold="True" ></asp:Button>
       &nbsp;&nbsp;
        <asp:Button ID="cmdBack" Text="<< Back" runat="server" 
            ></asp:Button>
       &nbsp;</td>
  </tr>
      </table></td>
  
 
 </table>

      </div>
        <br />
</asp:Content>



