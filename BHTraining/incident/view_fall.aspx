<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="view_fall.aspx.vb" Inherits="incident_view_fall" %>

<table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Fall Investigation</strong></td>
    </tr>
    <tr>
      <td><strong>Who falled and age</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblwhofall" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Age</strong>&nbsp;&nbsp;
      <asp:Label ID="lblfallage" runat="server"></asp:Label>
      &nbsp;&nbsp;yrs.&nbsp;&nbsp;&nbsp;&nbsp;<strong>Type of fall</strong>&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lbltypeoffall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<strong>Time of fall</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltimeoffall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Activity at Time of Fall</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblactoffall" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Location</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfalllocation" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Having assistant during fall</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblasstfall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Post Fall : Vital sign/Neuro sign</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpostfall" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Examination conducted</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallexam" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>By Dr</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallexamby" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Date</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallexamdate" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Time</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallexamtime" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>What are investigation &amp; treatment</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfalltx" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>Associated Risk Factor / and Cause</strong></td>
    </tr>
    <tr>
      <td>1. Medication last 24 hrs<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>2. Patient's Factors<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>3. Post operative / Procedure<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk3" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</span> Type of anesthesia<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lblfalltypeofanes" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>4. Nursing care intervention / by staff before patient fall<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lblfallrisk4" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>5. Safety of equipment<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk5" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>6. Safety of enviroment<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk6" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>7. Inform Instruction<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk7" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td>8. Assessment / Reassessment&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;When (Last time)<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallassesstime" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</span>Reason<span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallassessreason" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span><br /><span style="border-bottom: dashed 1px #000">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisk8" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
    </tr>
    <tr>
      <td class="under">Fall Risk Assessment</td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" class="irReporttbl">
        <tr>
          <td><strong>Risk Factor</strong></td>
          <td width="100" class="center"><strong>Risk points</strong></td>
          <td width="100" class="center"><strong>Score (*w)</strong></td>
          <td width="100" class="center"><strong>Score (**sp)</strong></td>
        </tr>
        <tr>
          <td>Confusion / Disorientation</td>
          <td class="center">4</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Depression (following as physician's order)</td>
          <td class="center">2</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Altered Elimination (any positive answer to urgency, frequency, or incontinence) <br />
            Charted with altered elimination needs</td>
          <td class="center">1</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Dizziness / Vertigo<br />
            Charted with dizziness or vertigo</td>
          <td class="center">1</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Implusive / Noncompliance</td>
          <td class="center">1</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Any prescribed antiepileptic medication 
            (See in medication list from   appendix 1) <br />
            (24 hrs after receiving medicine)</td>
          <td class="center">2</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Any prescribed benzodiazepine medication            (See in medication list from   appendix 1) <br />
            (24 hrs after receiving medicine)</td>
          <td class="center">1</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Able to rise in singel movement</td>
          <td class="center">0</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Pushes up, successful in one</td>
          <td class="center">1</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Multiple attempts but successful</td>
          <td class="center">3</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
        <tr>
          <td>Unable to rise without assistance</td>
          <td class="center">4</td>
          <td class="center">&nbsp;</td>
          <td class="center">&nbsp;</td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><strong>Ward assessment score</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallwardscore" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Fall prevention specialist assessment score</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallspscore" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td>* w = Assessement by ward nurse<br />
* sp = Assessment by fall prevention specialist</td>
    </tr>
    <tr>
      <td><strong>Risk Level of fall</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblfallrisklevel" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<strong>Severity outcome</strong>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblfalloutcome" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
  </table>

