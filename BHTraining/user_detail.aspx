<%@ Page Title="" Language="VB" MasterPageFile="~/bh1.master" AutoEventWireup="false" CodeFile="user_detail.aspx.vb" Inherits="user_detail" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <td width="150" >Employee ID</td>
    <td width="250">
        <asp:Label ID="txtid" runat="server"></asp:Label>
      </td>
    <td width="150">&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td  >HN</td>
    <td >
        <asp:Label ID="txthn" runat="server"></asp:Label>
      </td>
    <td>Sex</td>
    <td>
        <asp:Label ID="txtsex" runat="server"></asp:Label>
      </td>
  </tr>
  <tr>
    <td  >Name (EN)</td>
    <td >
        <asp:Label ID="txtnameen" runat="server"></asp:Label>
      </td>
    <td>Name (TH)</td>
    <td>
        <asp:Label ID="txtnameth" runat="server"></asp:Label>
      </td>
  </tr>
  <tr>
    <td  >Employee type</td>
    <td >
        <asp:Label ID="txtemptype" runat="server"></asp:Label>
      </td>
    <td>Hire date</td>
    <td>
        <asp:Label ID="txthiredate" runat="server"></asp:Label>
      </td>
  </tr>
  <tr>
    <td  >Job Title</td>
    <td >
        <asp:Label ID="txtjobtitle" runat="server"></asp:Label>
      </td>
    <td>Job Type</td>
    <td>
        <asp:Label ID="txtjobtype" runat="server"></asp:Label>
      </td>
  </tr>
  <tr>
    <td  >Date of Birth</td>
    <td >
        <asp:Label ID="txtdob" runat="server"></asp:Label>
      </td>
    <td>Age</td>
    <td>
        <asp:Label ID="txtage" runat="server"></asp:Label>
      &nbsp;years</td>
  </tr>
  <tr>
    <td>Cost Center name</td>
    <td >
        <asp:Label ID="txtdeptname" runat="server"></asp:Label>
      </td>
    <td >Cost Center</td>
    <td>
        <asp:Label ID="txtcostcenter" runat="server"></asp:Label>
      </td>
  </tr>
  <tr>
    <td>Department</td>
    <td >
        <asp:DropDownList ID="txtdept" runat="server"   DataTextField="dept_name_en" DataValueField="dept_id">
        </asp:DropDownList>
      </td>
    <td >Division</td>
    <td>&nbsp;</td>
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
                        DataTextField="module_name" DataValueField="module_id" BackColor="#FFFFCC" 
                        SelectionMode="Multiple"></asp:ListBox>
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
                        DataValueField="role_id" BackColor="#FFFFCC" SelectionMode="Multiple"></asp:ListBox>
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
    <td style="background-color:lightyellow"><b>Costcenter Access - IR&amp;CFB</b></td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%" >
                    <b>Available Costcenter - IR&amp;CFB</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    <b></b></td>
                <td width="40%">
                    <b>Granted Costcenter(s) - IR&amp;CFB</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtcc" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id" BackColor="#FFFFCC" 
                        SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center;">
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
    <td style="background-color:lightgreen"><strong>Costcenter Access - IDP</strong></td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%">
                    <b>Available Costcenter - IDP</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    &nbsp;</td>
                <td width="40%">
                    <b>Granted Costcenter(s) - IDP</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtidp_cc1" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id" BackColor="#FFFFCC" 
                        SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center">
                    <asp:Button ID="cmdAddIDP" runat="server" Text=">" style="height: 26px" />
                    <br />
                    <asp:Button ID="cmdRemoveIDP" runat="server" Text="<" />
                   </td>
                <td width="40%">
                    <asp:ListBox ID="txtidp_cc2" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id"></asp:ListBox>
                </td>
            </tr>
          
        </table></td>
  </tr>
   <tr>
    <td style="background-color:lightblue"><strong>Costcenter Access - SSIP</strong></td>
    <td colspan="3" >
        <table cellpadding="0" cellspacing="0" style="width:100%;">
            <tr>
                <td width="40%">
                    <b>Available Costcenter - SSIP</b></td>
                <td width="20%" align="center" style="vertical-align:middle">
                    &nbsp;</td>
                <td width="40%">
                    <b>Granted Costcenter(s) - SSIP</b></td>
            </tr>
          
            <tr>
                <td width="40%">
                    <asp:ListBox ID="txtssip_cc1" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id" BackColor="#FFFFCC" 
                        SelectionMode="Multiple"></asp:ListBox>
                </td>
                <td width="20%"  style="vertical-align:middle; text-align:center">
                    <asp:Button ID="cmdAddSSIP" runat="server" Text=">" style="height: 26px" />
                    <br />
                    <asp:Button ID="cmdRemoveSSIP" runat="server" Text="<" />
                   </td>
                <td width="40%">
                    <asp:ListBox ID="txtssip_cc2" runat="server" Width="90%" DataTextField="costcenter_name" 
                        DataValueField="costcenter_id"></asp:ListBox>
                </td>
            </tr>
          
        </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td >
        &nbsp;</td>
    <td >&nbsp;</td>
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
 <td style="vertical-align:top">
     <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr><td class="theader">
  <img src="images/sidemenu_circle.jpg" width="10" height="10" alt="personal photo" />&nbsp;Photo
      
      </td>
      </tr>
  <tr><td>&nbsp;</td></tr>
  <tr><td>
      <asp:FileUpload ID="FileUpload1" runat="server" />
      </td></tr>
  </table>
     <br />
       <table width="98%" cellpadding="3" cellspacing="0" class="tdata3">
  <tr><td class="theader">
  <img src="images/sidemenu_circle.jpg" width="10" height="10" alt="personal photo" />&nbsp;Account 
      Information
      </td>
      </tr>
  <tr><td>
  <table width="100%"><tr>
  <td width="80">User name</td>
  <td>
      <asp:TextBox ID="txtusername" runat="server" Enabled="False"></asp:TextBox>
      </td>
  </tr><tr>
  <td width="80">Password</td>
  <td>
      <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
          </td>
  </tr><tr>
  <td width="80">Email</td>
  <td><asp:TextBox ID="txtemail" runat="server"></asp:TextBox></td>
  </tr><tr>
  <td width="80">SMS</td>
  <td><asp:TextBox ID="txtsms" runat="server"></asp:TextBox></td>
  </tr></table>
  
  </td></tr>
  
  </table>
  <br />
     </td>
 
 
 </table>

      </div>
        <br />
</asp:Content>

