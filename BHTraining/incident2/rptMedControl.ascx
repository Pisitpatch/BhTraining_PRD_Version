<%@ Control Language="VB" AutoEventWireup="false" CodeFile="rptMedControl.ascx.vb" Inherits="incident_rptMedControl" %>
<table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Medication Management Incident Investigation</strong></td>
    </tr>
    <tr>
      <td><strong>1. Serious type of medication error</strong></td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type I</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblmedserioustype1" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type II</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblmedserioustype2" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type III</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblmedserioustype3" runat="server"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td class="style1"><strong>2. Level of severity outcome</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmedlevelofseverity" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>3. Drug category</strong></td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Drug wrong name</strong></td>
    </tr>
    <tr>
      <td><table width="95%" align="center" cellpadding="3" cellspacing="0" class="irReporttbl">
        <tr>
          <td style="text-align:center"><strong>Drug name</strong></td>
          <td style="text-align:center"><strong>Drug group</strong></td>
          <td style="text-align:center"  width="100"><strong>LASA</strong></td>
          <td style="text-align:center"  width="180"><strong>High alert drug</strong></td>
        </tr>
        <tr>
          <td valign="top"><asp:Label ID="lbldrugwrongname" runat="server"></asp:Label></td>
          <td valign="top"><asp:Label ID="lbldrugwronggroup" runat="server"></asp:Label></td>
          <td valign="top"><asp:Label ID="lbldrugwronglasa" runat="server"></asp:Label></td>
          <td valign="top"><asp:Label ID="lbldrugwronghighalert" runat="server"></asp:Label></td>
        </tr>
       
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Drug right name</strong></td>
    </tr>
    <tr>
      <td><table width="95%" align="center" cellpadding="3" cellspacing="0" class="irReporttbl">
        <tr>
          <td ><strong>Drug name</strong></td>
        </tr>
        <tr>
          <td valign="top"><asp:Label ID="lbldrugrightname" runat="server"></asp:Label></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><strong>4. Categorization event of medication error</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblcategorizeofmederr" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>5. Type of Medication order</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbltypeofmedorder" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>6. Robot product</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmedrobot" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>7. CPOE</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmedcpoe" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>8. MAR error</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmedmarerr" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>9. Time of incident</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrtime1" runat="server"></asp:Label>&nbsp;&nbsp;at&nbsp;&nbsp;<asp:Label ID="lblmederrtime2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>10. Period of employment</strong></td>
    </tr>
    <tr>
      <td><table width="95%" align="center" cellpadding="3" cellspacing="0" class="irReporttbl">
        <tr>
          <td style="text-align:center"><strong>Job type</strong></td>
          <td style="text-align:center"><strong>Period of employment</strong></td>
          <td style="text-align:center"  width="180"><strong>Work Process</strong></td>
        </tr>
        <tr>
          <td valign="top"><asp:Label ID="lbljobtype" runat="server"></asp:Label></td>
          <td valign="top"><asp:Label ID="lblperiodofemploy" runat="server"></asp:Label>;</td>
          <td valign="top"><asp:Label ID="lblworkprocess" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
        </tr>
        <tr>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
          <td valign="top">&nbsp;</td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><strong>11. Cause</strong></td>
    </tr>
    <tr>
      <td><strong>11.1 Personal/ Human factor</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>11.2 Communication breakdown</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>11.3 Poor practice habit</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause3" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>11.4 System</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause4" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>11.5 Product/ Package &amp; Design</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause5" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td><strong>11.6 Miscellaneous</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblmederrcause6" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
  </table>