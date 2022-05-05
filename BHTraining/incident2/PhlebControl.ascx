<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PhlebControl.ascx.vb" Inherits="incident.incident_PhlebControl" %>
          <style type="text/css">
              .auto-style1 {
                  width: 50px;
              }
          </style>
          <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td colspan="2" valign="top">1. Peripheral cannula related infusion complications</td>
    </tr>
  <tr>
    <td width="200" valign="top">1.1 Infiltration</td>
    <td valign="top">
    <table width="700" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top">
            <asp:RadioButton ID="txtinf_s1" GroupName="inf" runat="server" />
        </td>
        <td width="50" valign="top">Stage 1</td>
        <td valign="top"> Skin blanched, Edema less than 1 inch in any direction, Cool to touch, With or without pain</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtinf_s2" GroupName="inf" runat="server"  /></td>
        <td valign="top">Stage 2</td>
        <td valign="top"> Skin blanched, Edema 1 to 6 inches in any direction, Cool to touch, With or without pain</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtinf_s3" GroupName="inf" runat="server" /></td>
        <td valign="top">Stage 3</td>
        <td valign="top"> Skin blanched, translucent, Gross edema greater than 6 inches in any direction, Cool to touch, Mild to moderate pain, Possible numbness</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtinf_s4" GroupName="inf" runat="server" /></td>
        <td valign="top">Stage 4</td>
        <td valign="top"> Skin blanched, translucent, skin tight, leaking skin discolored, bruised, swollen, Gross edema greater than 6 inches in any direction, Deep pitting tissue edema, Circulatory impairment, Moderate to severe pain, Infiltration of any amount of blood product, irritant, or vesicant</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtinf_s5" GroupName="inf" runat="server" /></td>
        <td valign="top" colspan="2">Not Selected</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" valign="top">Clinical symptom for infiltration</td>
    </tr>
  <tr>
    <td valign="top">- Pain</td>
    <td valign="top"><asp:DropDownList ID="txtinf_pain" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">No pain (pain score 0)</asp:ListItem>
      <asp:ListItem Value="2">Mild (pain score 1-4)</asp:ListItem>
      <asp:ListItem Value="3">Moderate (pain score 5-7)</asp:ListItem>
      <asp:ListItem Value="4">Severe (pain score 7-10)</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Edema</td>
    <td valign="top"><asp:DropDownList ID="txtinf_edema" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">&lt; 2.5 cms in any direction</asp:ListItem>
      <asp:ListItem Value="2">2.5 cms - 15 cms in any direction</asp:ListItem>
      <asp:ListItem Value="3">15 cms in any direction</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">1.2 Phlebitis</td>
    <td valign="top"><table width="700" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><asp:RadioButton ID="txtph_s0" GroupName="phl" runat="server" />
          </td>
        <td valign="top" class="auto-style1">Stage 0</td>
        <td valign="top"> No symptom</td>
      </tr>
      <tr>
        <td width="23" valign="top"><asp:RadioButton ID="txtph_s1" GroupName="phl" runat="server" />
          </td>
        <td valign="top" class="auto-style1">Stage 1</td>
        <td valign="top"> Erythema at access site with or without pain</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtph_s2" GroupName="phl" runat="server" /></td>
        <td valign="top" class="auto-style1">Stage 2</td>
        <td valign="top">  Pain at access site with erythema and/or edema</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtph_s3" GroupName="phl" runat="server" /></td>
        <td valign="top" class="auto-style1">Stage 3 </td>
        <td valign="top">Pain at access site with erythema, Streak formation, Palpable venous cord</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtph_s4" GroupName="phl" runat="server" /></td>
        <td valign="top" class="auto-style1">Stage 4 </td>
        <td valign="top">Pain at access site with erythema, Streak formation, Palpable venous cord >1 inch

                    in length,  Purulent drainage</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtph_s5" GroupName="phl" runat="server" Enabled="False" Visible="false" /></td>
        <td valign="top" class="auto-style1"><asp:label runat="server" ID="lblStage5" Text="Stage 5" Visible="false" /> </td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="txtph_s6" GroupName="phl" runat="server" /></td>
        <td valign="top" colspan="2">Not Selected</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" valign="top">Clinical symptom for phlebitis</td>
    </tr>
  <tr>
    <td valign="top">- Pain</td>
    <td valign="top"><asp:DropDownList ID="txtpain" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">No pain (pain score 0)</asp:ListItem>
      <asp:ListItem Value="2">Mild (pain score 1-4)</asp:ListItem>
      <asp:ListItem Value="3">Moderate (pain score 5-7)</asp:ListItem>
      <asp:ListItem Value="4">Severe (pain score 7-10)</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Redness</td>
    <td valign="top"><asp:DropDownList ID="txtredness" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Mild</asp:ListItem>
      <asp:ListItem Value="2">Moderate</asp:ListItem>
      <asp:ListItem Value="3">Severe</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Erythema</td>
    <td valign="top"><asp:DropDownList ID="txterythema" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Yes</asp:ListItem>
      <asp:ListItem Value="2">No</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Swelling</td>
    <td valign="top"><asp:DropDownList ID="txtswelling" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Yes</asp:ListItem>
      <asp:ListItem Value="2">No</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Induration</td>
    <td valign="top"><asp:DropDownList ID="txtinduration" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Yes</asp:ListItem>
      <asp:ListItem Value="2">No</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Palpable venous cord</td>
    <td valign="top"><asp:DropDownList ID="txtpvc" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Yes</asp:ListItem>
      <asp:ListItem Value="2">No</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">- Fever</td>
    <td valign="top"><asp:DropDownList ID="txtfever" runat="server" Width="385px">
      <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
      <asp:ListItem Value="1">Yes</asp:ListItem>
      <asp:ListItem Value="2">No</asp:ListItem>
    </asp:DropDownList></td>
  </tr>
  <tr>
    <td valign="top">Type of Phlebitis</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_mechanical" id="chk_mechanical" runat="server" />
          <label for="checkbox22"></label>
<label for="checkbox2"></label>
          <label for="radio8"></label></td>
        <td width="150" valign="top">Mechanical</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_chemical" id="chk_chemical" runat="server" /></td>
        <td width="150" valign="top">Chemical</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_bacterial" id="chk_bacterial" runat="server" /></td>
        <td valign="top">Bacterial</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">2. Gauge and Number of Gauge</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_jelco" id="chk_jelco" runat="server" />
          </td>
        <td width="150" valign="top">Jelco No 
          <input type="text" name="txtjelco" id="txtjelco" style="width: 40px" runat="server" /></td>
        <td width="23" valign="top"><input type="checkbox" name="chk_venflon" id="chk_venflon" runat="server" /></td>
        <td width="150" valign="top">Venflon No
          <input type="text" name="txtvenflon" id="txtvenflon" style="width: 40px" runat="server" /></td>
        <td width="23" valign="top"><input type="checkbox" name="chk_other_gauge" id="chk_other_gauge" runat="server" /></td>
        <td valign="top">Other
          <input type="text" name="txtothergauge" id="txtothergauge" style="width: 40px"  runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">3. IV site</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_iv_dorsal" id="chk_iv_dorsal" runat="server" />
          </td>
        <td width="150" valign="top">Dorsal metacarpal vein</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_iv_cep" id="chk_iv_cep" runat="server" /></td>
        <td width="150" valign="top">Cephalic vein</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_iv_basi" id="chk_iv_basi" runat="server" /></td>
        <td valign="top" width="150">Basilic vein</td>
          <td width="23" valign="top"><input type="checkbox" name="chk_iv_other" id="chk_iv_other" runat="server" /></td>
        <td valign="top">Other <asp:TextBox ID="txtivother" runat="server" Width="60px"></asp:TextBox>
          </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">4. IV Fluid</td>
    <td valign="top"><input type="text" name="txtfluid" id="txtfluid" style="width: 700px" runat="server" /> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" />
         </td>
  </tr>
  <tr>
    <td valign="top">5. IV medication</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23"><input type="checkbox" name="chk_conc" id="chk_conc" runat="server" /></td>
        <td width="150">High Concentration</td>
        <td width="23"><input type="checkbox" name="chk_chemo" id="chk_chemo" runat="server" /></td>
        <td width="150">On Chemotherapy</td>
        <td width="23"><input type="checkbox" name="chk_med" id="chk_med" runat="server" /></td>
        <td width="150">Circulatory Medicine</td>
        <td width="23"><input type="checkbox" name="chk_anti" id="chk_anti" runat="server" /></td>
        <td>Antibiotic</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_ivmed_other" id="chk_ivmed_other" runat="server" /></td>
        <td colspan="7">Other&nbsp;(Please fill the name of medication/ solution)&nbsp;
          <input type="text" name="txt_ivmed_other" id="txt_ivmed_other" style="width: 285px" runat="server" /> 
        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/spellcheck.png" />
          </td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td height="23" valign="top">6. Duration of catherter in place</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23">
            <asp:RadioButton ID="txtduration1" GroupName="duration" runat="server" />
          </td>
        <td width="150">&lt; 24 hrs</td>
        <td width="23">
            <asp:RadioButton ID="txtduration2" GroupName="duration" runat="server" />
          </td>
        <td width="150">24-48 hrs</td>
        <td width="23">
            <asp:RadioButton ID="txtduration3" GroupName="duration" runat="server" />
          </td>
        <td width="150">48-72 hrs</td>
        <td width="23">
            <asp:RadioButton ID="txtduration4" GroupName="duration" runat="server" />
          </td>
        <td>72-96 hrs</td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">7. Medical History</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td colspan="8"><asp:DropDownList ID="txtmed_history" runat="server" Width="385px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">Yes</asp:ListItem>
        <asp:ListItem Value="2">No</asp:ListItem>
         </asp:DropDownList></td>
        </tr>
      <tr>
        <td width="23"><input type="checkbox" name="chk_immune" id="chk_immune" runat="server" /></td>
        <td width="150">Immune suppression</td>
        <td width="23"><input type="checkbox" name="chk_dm" id="chk_dm" runat="server" /></td>
        <td width="150">DM</td>
        <td width="23"><input type="checkbox" name="chk_obesity" id="chk_obesity" runat="server" /></td>
        <td width="150">Obesity</td>
        <td width="23"><input type="checkbox" name="chk_coagulo" id="chk_coagulo" runat="server" /></td>
        <td>Coagulopathy</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_cva" id="chk_cva" runat="server" /></td>
        <td>CVA</td>
        <td><input type="checkbox" name="chk_cancer" id="chk_cancer" runat="server" /></td>
        <td>Cancer</td>
        <td><input type="checkbox" name="chk_mal" id="chk_mal" runat="server" /></td>
        <td>Malnourished</td>
        <td><input type="checkbox" name="chk_bony" id="chk_bony" runat="server" /></td>
        <td>Bony Trauma</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_history_other" id="chk_history_other" runat="server" /></td>
        <td colspan="5">Other&nbsp;&nbsp;          <input type="text" name="txthistoryother" id="txthistoryother" style="width: 450px" runat="server" /></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
  </table></td>
  </tr>
  </table>

