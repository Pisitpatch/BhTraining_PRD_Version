<%@ Control Language="VB" AutoEventWireup="false" CodeFile="rptPhebControl.ascx.vb" Inherits="incident_rtpPhebControl" %>
<table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Phlebitis/Infiltration Occurrence Investigation</strong></td>
    </tr>
    <tr>
      <td><strong>1. Peripheral cannula related infusion complications</strong></td>
    </tr>
    <tr>
      <td><strong>1.1 Infiltration</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblinfiltration" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Clinical symptom for infiltration</strong></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1) &nbsp;Pain&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Label ID="lblinfiltrationsymptom1" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</span> </td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2)&nbsp; Edema&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblinfiltrationsymptom2" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td><strong>1.2 Phlebitis</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
        <asp:Label ID="lblphlebitis" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Clinical symptom for phlebitis</strong></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(1)&nbsp;&nbsp;Pain&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(2)&nbsp;&nbsp;Redness&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(3)&nbsp;&nbsp;Erythema&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom3" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(4)&nbsp;&nbsp;Swelling&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom4" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(5)&nbsp;&nbsp;Induration&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom5" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(6)&nbsp;&nbsp;Palpable venous cord<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
        <asp:Label ID="lblphlebitissymptom6" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(7)&nbsp;&nbsp;Fever&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblphlebitissymptom7" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td><strong>Type of Phlebitis</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lbltypeofphlebitis" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>2. Gauge and Number of Gauge</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblguageno" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>3. IV Site</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
        <asp:Label ID="lblivsite" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>4. IV Fluid</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblivfluid" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>5. IV Medication</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
        <asp:Label ID="lblivmed" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>6. Duration of catherter in place</strong>&nbsp;&nbsp;&nbsp;&nbsp;        
      <asp:Label ID="lblcatherter" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>7. Medical History</strong>&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Label ID="lblmedhx" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
  </table>