<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_group_edit.aspx.vb" Inherits="Game_MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" class="box-header">
  <tr>
    <td><img src="../images/web.png" width="24" height="24"  />&nbsp;&nbsp; 
      <a href="admin_question_master.aspx?tid=<%response.write(tid) %>">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
    </td>
  </tr>
</table>
<table width="100%" class="box-content">
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">Test Name</td>
          <td valign="top">
              <asp:Label ID="lblGroupName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">Zone No.</td>
          <td valign="top">
              <asp:DropDownList ID="txtzone" runat="server">
                  <asp:ListItem>1</asp:ListItem>
                  <asp:ListItem>2</asp:ListItem>
                  <asp:ListItem>3</asp:ListItem>
                  <asp:ListItem>4</asp:ListItem>
              </asp:DropDownList>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">IP Address</td>
          <td valign="top"><asp:TextBox ID="txtip" runat="server"></asp:TextBox>
              <asp:Button ID="cmdAddIP" runat="server" Text="Add IP Address" />
            &nbsp;<asp:Button ID="cmdIPDel" runat="server" Text="Remove IP Address" />
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">&nbsp;</td>
          <td valign="top">
              <asp:ListBox ID="lblIP" runat="server" Width="150px" DataTextField="ip_address" 
                  DataValueField="ip_address"></asp:ListBox>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">Target Group</td>
          <td valign="top">
              <table cellpadding="0" cellspacing="0" style="margin-top: 5px;" width="100%">
                  <tr>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td width="255">
                                      <asp:ListBox ID="txttarget_all" runat="server" DataTextField="target_name" 
                                          DataValueField="target_id" SelectionMode="Multiple" Width="250px">
                                      </asp:ListBox>
                                  </td>
                                  <td width="60">
                                      <asp:Button ID="cmdAddTarget" runat="server" CausesValidation="False" 
                                          Text="&gt;" />
                                      <br />
                                      <asp:Button ID="cmdRemoveTarget" runat="server" CausesValidation="False" 
                                          Text="&lt;" />
                                      &nbsp;<br />
                                  </td>
                                  <td>
                                      <asp:ListBox ID="txttarget_select" runat="server" DataTextField="target_name" 
                                          DataValueField="target_id" SelectionMode="Multiple" Width="250px">
                                      </asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
              </table>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">&nbsp;</td>
          <td valign="top">
              <table cellpadding="0" cellspacing="0" style="margin-top: 5px; display:none" width="100%">
                  <tr>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td>
                                      <asp:TextBox ID="txtfind_jobtype" runat="server" Width="130px"></asp:TextBox>
                                      <asp:Button ID="cmdSearchJobType" runat="server" Text="search" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td width="255">
                                      <asp:ListBox ID="lblJobTypeAll" runat="server" DataTextField="job_title" 
                                          DataValueField="job_title" SelectionMode="Multiple" Width="250px">
                                      </asp:ListBox>
                                  </td>
                                  <td width="60">
                                      <asp:Button ID="cmdAddType" runat="server" CausesValidation="False" 
                                          Text="&gt;" />
                                      <br />
                                      <asp:Button ID="cmdRemoveType" runat="server" CausesValidation="False" 
                                          Text="&lt;" />
                                      &nbsp;<br />
                                  </td>
                                  <td >
                                      <asp:ListBox ID="lblJobTypeSelect" runat="server" DataTextField="job_title" 
                                          DataValueField="job_title" SelectionMode="Multiple" Width="250px">
                                      </asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
              </table>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">Number of Questions</td>
          <td valign="top">
              <asp:TextBox ID="txtqty" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td width="180" valign="top" class="rowname" style="font-weight: bold">Group Name (Thai) :</td>
          <td valign="top">
          <textarea name="txtgroup_th" id="txtgroup_th" cols="45" rows="2" style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname" style="font-weight: bold">Group Name  (Eng) :</td>
          <td valign="top"><textarea name="txtgroup_en" id="txtgroup_en" cols="45" rows="2" style="width: 635px;" runat="server"></textarea></td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname" style="font-weight: bold">Detail</td>
          <td valign="top">
              <textarea name="txtgroup_en0" id="txtdetail" cols="45" rows="5" 
                  style="width: 635px;" runat="server"></textarea></td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="green" />
          &nbsp;</td>
        </tr>
      
      
        <tr>
          <td valign="top" class="rowname"><strong>Employee ID</strong></td>
          <td valign="top">
              <asp:TextBox ID="txtempcode" runat="server"></asp:TextBox>
               <asp:Button ID="cmdClearScore" runat="server" Text="Clear Employee Score" CssClass="green" OnClientClick="return confirm('Are you sure you want to clear employee score?');" />
            </td>
        </tr>
    </table>
</asp:Content>

