<%@ Control Language="VB" AutoEventWireup="false" CodeFile="rptPressureControl.ascx.vb" Inherits="incident_rptPressureControl" %>
<table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Pressure Ulcer  Incident Investigation</strong></td>
    </tr>
    <tr>
      <td><strong>1. What type of Pressure ulcer presented?</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltypeofpressure" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>2. What was the stage of Pressure ulcer presented</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblstageofpressure" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>3. Location</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpressurelocation" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>4. On admission, was a skin inspection documented?</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpressuredocument" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>5. When was the first Braden scale lower or equal than 19, were there any preventive intervention implemented?</strong>&nbsp;&nbsp;&nbsp;&nbsp;
      <br />
      <asp:Label ID="lblpressureprevention1" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><asp:Label ID="lblpressureprevention2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
      <td><strong>6. Risk factors</strong></td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.1 Intrinsic factors</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpressureintrinsic" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.2 Extrinsic factors</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpressureextrinsic" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td> <strong>7. Was the use of device or appliance involved in the development or asvancement of the pressure ulcer?</strong>&nbsp;&nbsp;&nbsp;&nbsp;
      <br />
      <asp:Label ID="lblpressuredevice1" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="lblpressuredevice2" runat="server"></asp:Label>
     </td>
    </tr>
    <tr>
      <td><strong>8. Braden scale assessment on the day the pressure ulcer acquired</strong></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" class="irReporttbl">
        <tr>
          <td width="200" valign="top"><strong>Sensory Perception</strong><br />
            ability to response meaningfully to   pressure- related discomfort </td>
          <td valign="top"><asp:Label ID="lblbradenscale1" runat="server"></asp:Label>
        </td>
        </tr>
        <tr>
          <td valign="top"><strong>Moisture</strong><br />
            degree to which skin is exposed to moisture</td>
          <td valign="top"><asp:Label ID="lblbradenscale2" runat="server"></asp:Label></td>
          </tr>
        <tr>
          <td valign="top"><strong>Activity</strong><br />
            degree of physical activity</td>
          <td valign="top"><asp:Label ID="lblbradenscale3" runat="server"></asp:Label></td>
          </tr>
        <tr>
          <td valign="top"><strong>Mobility</strong><br />
            ability to change and control body position</td>
          <td valign="top"><asp:Label ID="lblbradenscale4" runat="server"></asp:Label></td>
          </tr>
        <tr>
          <td valign="top"><p><strong>Nutrition</strong><br />
            usual food intake pattern </p></td>
          <td valign="top"><asp:Label ID="lblbradenscale5" runat="server"></asp:Label></td>
          </tr>
        <tr>
          <td valign="top"><strong>Friction &amp; Shear</strong><br /></td>
          <td valign="top"><asp:Label ID="lblbradenscale6" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top"><strong>Total Score</strong></td>
          <td valign="top"><asp:Label ID="lblScore" runat="server"></asp:Label></td>
        </tr>
      </table></td>
    </tr>
  </table>