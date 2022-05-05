<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PressureControl.ascx.vb" Inherits="incident_PressureControl" %>
<style type="text/css">
    .style1
    {
        width: 22px;
    }
    .style2
    {
        height: 25px;
    }
</style>
<table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td valign="top"><strong>1. What type of Pressure ulcer presented?</strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23"><asp:RadioButton ID="is_acquire1" GroupName="acquire" runat="server" />
          </td>
        <td width="215">Hospital acquired</td>
        <td width="23"><asp:RadioButton ID="is_acquire2" GroupName="acquire" 
                runat="server" />
          </td>
        <td width="215">Home acquired</td>
        <td width="23"><asp:RadioButton ID="is_acquire3" GroupName="acquire" 
                runat="server" />
          </td>
        <td>Other hospital acquired</td>
      </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>2. What was the stage of Pressure ulcer presented</strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="95%">
      <tr>
        <td width="23" valign="top"><asp:RadioButton ID="is_stage1" GroupName="stage" 
                runat="server" />
          &nbsp;</td>
        <td width="150" valign="top" bgcolor="#EAEAEA">Stage I :</td>
        <td valign="top">Intact skin with non-blanchable redness of a localized area usually over a bony prominence darkly pigmented skin may not have visible blanching; its color may differ from the surrounding area.</td>
      </tr>
      <tr>
        <td valign="top"><asp:RadioButton ID="is_stage2" GroupName="stage" 
                runat="server" />
          </td>
        <td valign="top" bgcolor="#EAEAEA">Stage II :</td>
        <td valign="top">Partial-thickness loss dermis presenting as a shallow open ulcer with a red pink wound bed, without slough. May also present as an intact or open/ruptured serum-filled blister.</td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_stage3" GroupName="stage" 
                runat="server" />
          </td>
        <td valign="top" bgcolor="#EAEAEA">Stage III :</td>
        <td valign="top">Full thickness loss. Subcutaneous fat may be visible but bone, tendon or muscle is not exposed. Slough may be present but does not obscure the depth of tissue loss. May include undermining and tunneling.</td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_stage4" GroupName="stage" 
                runat="server" />
          </td>
        <td valign="top" bgcolor="#EAEAEA">Stage IV :</td>
        <td valign="top">Full-thickness skin loss with exposed bone, tendon or muscle. Slough or eschar may be present on some parts of the wound bed. Often includes undermining and tunneling.</td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_stage5" GroupName="stage" 
                runat="server" />
          </td>
        <td valign="top" bgcolor="#EAEAEA">Unstageable :</td>
        <td valign="top">Full-thickness tissue loss in which the bed of the ulcer is covered by slough (yellow, tan, gray, brown or black) in the wound bed.</td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_stage6" GroupName="stage" 
                runat="server" />
          </td>
        <td valign="top" bgcolor="#EAEAEA">Suspected Deep Tissue Injury :</td>
        <td valign="top">Purple or maroon localized area of discolored intact skin or blood-filled blister due to damage of underlying soft tissue from pressure and/or shear. The area may be preceded by tissue that is painful, firm, mushy, boggy, warmer or cooler as compared to adjacent tissue.</td>
      </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>3. Location </strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_location1" GroupName="location" 
                runat="server" />
          &nbsp;</td>
        <td width="130" valign="top">Coccyx</td>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_location2" GroupName="location" 
                runat="server" />
          &nbsp;</td>
        <td width="130" valign="top">Buttock
            <asp:DropDownList ID="txtbuttock_type" runat="server">
            <asp:ListItem Value="">--</asp:ListItem>
            <asp:ListItem Value="Left">Left</asp:ListItem>
            <asp:ListItem Value="Right">Right</asp:ListItem>
            </asp:DropDownList>
          
          </td>
        <td valign="top" class="style1">
            <asp:RadioButton ID="is_location3" GroupName="location" 
                runat="server" />
          </td>
        <td width="130" valign="top">Hip
           <asp:DropDownList ID="txthip_type" runat="server">
            <asp:ListItem Value="">--</asp:ListItem>
            <asp:ListItem Value="Left">Left</asp:ListItem>
            <asp:ListItem Value="Right">Right</asp:ListItem>
            </asp:DropDownList></td>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_location4" GroupName="location" 
                runat="server" />
          </td>
        <td valign="top">Other: Please specify
          
            <input type="text" name="txtother_remark" id="txtother_remark" runat ="server" />
         </td>
      </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>4. On admission, was a skin inspection documented?
        <label for="select20"></label>
    </strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23" valign="top">&nbsp;<asp:RadioButton ID="is_admission1" GroupName="admission" 
                runat="server" />
          </td>
        <td width="130" valign="top">Yes</td>
        <td valign="top">Braden scale was&nbsp;
            <asp:TextBox ID="txtadmission" runat="server"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td valign="top">&nbsp;<asp:RadioButton ID="is_admission2" runat="server" GroupName="admission" />
          </td>
        <td valign="top">No</td>
        <td valign="top">&nbsp;</td>
      </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>5. When was the first Braden scale lower or equal than 19, were there any preventive intervention implemented?</strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_scale1" GroupName="scale" 
                runat="server" />
          &nbsp;</td>
        <td valign="top">Yes</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;&nbsp;</td>
        <td valign="top">
        <table width="100%">
          <tr>
            <td width="23">
                <asp:CheckBox ID="chk_scale1" runat="server" />
              </td>
            <td>Nursing Notes documented</td>    </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale2" runat="server" />
              </td>
            <td>Education to Patient and relative</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale3" runat="server" />
              </td>
            <td>Provided toilet worksheet</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale4" runat="server" />
              </td>
            <td>Provided airmattress</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale5" runat="server" />
              </td>
            <td>Skin barrier</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale6" runat="server" />
              </td>
            <td>Skin care (Keep clean and dry)</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale7" runat="server" />
              </td>
            <td>Pressure redistribution device</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale8" runat="server" />
              </td>
            <td>Repositioning</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale9" runat="server" />
              </td>
            <td>Nutrition support</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale10" runat="server" />
              </td>
            <td>Notify Wound care coordinator</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_scale11" runat="server" />
              </td>
            <td>Other: Please specify
              <input type="text" name="txtscale_other" id="txtscale_other" runat="server" /></td>
          </tr>
        </table>
        </td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_scale2" runat="server" GroupName="scale" />
          </td>
        <td valign="top">No: Please describe <br />
          <label>
            <textarea name="txtscale_detail" id="txtscale_detail" cols="45" rows="5" runat="server"></textarea>
          </label></td>
      </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>6. Risk factors</strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_risk1" runat="server" GroupName="risk" />
          &nbsp;</td>
        <td width="300" valign="top">Intrinsic factors</td>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_risk2" runat="server" GroupName="risk" />
          </td>
        <td valign="top">Extrinsic factors</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><table width="100%">
          <tr>
            <td width="23">
                <asp:CheckBox ID="chk_risk1" runat="server" />
              </td>
            <td>Age</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk2" runat="server" />
              </td>
            <td>Imobility</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk3" runat="server" />
              </td>
            <td>Incontinence</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk4" runat="server" />
              </td>
            <td>Poor nutrition status</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk5" runat="server" />
              </td>
            <td>Poor sensory perception</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk6" runat="server" />
              </td>
            <td>Being assisted to change position</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk7" runat="server" />
              </td>
            <td>Increase Temperature</td>
            </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk8" runat="server" />
              </td>
            <td>Limited activity</td>
          </tr>
          </table></td>
        <td valign="top">&nbsp;</td>
        <td valign="top"><table width="100%">
          <tr>
            <td width="23">
                <asp:CheckBox ID="chk_risk9" runat="server" />
              </td>
            <td>Shear</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk10" runat="server" />
              </td>
            <td>Friction</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk11" runat="server" />
              </td>
            <td>Transferring</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_risk12" runat="server" />
              </td>
            <td>Tissue load from positioning</td>
          </tr>
        </table></td>
      </tr>
      </table></td>
    </tr>
  <tr>
    <td valign="top"><strong>7. Was the use of device or appliance involved in the development or asvancement of the pressure ulcer?</strong></td>
    </tr>
  <tr>
    <td valign="top"><table width="100%">
      <tr>
        <td width="23" valign="top">
            <asp:RadioButton ID="is_device1" runat="server" GroupName="device" />
          &nbsp;</td>
        <td valign="top">Yes</td>
      </tr>
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top"><table width="100%">
          <tr>
            <td width="23">
                <asp:CheckBox ID="chk_device1" runat="server" />
              </td>
            <td>Anti-embolic device</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_device2" runat="server" />
              </td>
            <td>Intraoperative positioning device</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_device3" runat="server" />
              </td>
            <td>Orthopedic appliance</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_device4" runat="server" />
              </td>
            <td>Oxygen delivery device</td>
          </tr>
          <tr>
            <td>
                <asp:CheckBox ID="chk_device5" runat="server" />
              </td>
            <td>Tube</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td><table width="100%">
              <tr>
                <td width="23">
                <asp:CheckBox ID="chk_device51" runat="server" />
                  </td>
                <td>Endotracheal</td>
              </tr>
              <tr>
                <td class="style2">
                <asp:CheckBox ID="chk_device52" runat="server" />
                  </td>
                <td class="style2">Gastrostomy</td>
              </tr>
              <tr>
                <td>
                <asp:CheckBox ID="chk_device53" runat="server" />
                  </td>
                <td>Nasogastric</td>
              </tr>
              <tr>
                <td>
                <asp:CheckBox ID="chk_device54" runat="server" />
                  </td>
                <td>Tracheostomy</td>
              </tr>
              <tr>
                <td>
                <asp:CheckBox ID="chk_device55" runat="server" />
                  </td>
                <td>Indwelling urinary catheter</td>
              </tr>
              <tr>
                <td>
                <asp:CheckBox ID="chk_device56" runat="server" />
                  </td>
                <td>Other: Please specify 
                  <input type="text" name="txtdevice_other_remark" id="txtdevice_other_remark" runat="server" /></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td valign="top">
            <asp:RadioButton ID="is_device2" runat="server" GroupName="device" />
          </td>
        <td valign="top">No<br /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top"><strong>8. Braden scale assessment on the day the pressure ulcer acquired.</strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="95%">
      <tr>
        <td width="300" valign="top" bgcolor="#EAEAEA"><strong>Sensory Perception</strong><br />
          ability to response meaningfully to pressure- related discomfort
        </td>
        <td width="250" valign="top" bgcolor="#EAEAEA">
         <asp:DropDownList ID="txtsensory" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Completely Limited</asp:ListItem>
            <asp:ListItem Value="2">2. Very Limited</asp:ListItem>
            <asp:ListItem Value="3">3. Slightly Limited</asp:ListItem>
             <asp:ListItem Value="4">4. No Impairment</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtsensory" ErrorMessage="กรุณาระบุ Sensory Perception" Display="Dynamic"></asp:RequiredFieldValidator>
       </td>
        <td valign="top" bgcolor="#EAEAEA">Unresponsive (does not moan, finch, or grasp) to painful stimuli, due to diminished level of consciousness or sedation 
          <div align="center">OR</div>
limited ability to feel pain over most of body.</td>
      </tr>
      <tr>
        <td valign="top"><strong>Moisture</strong><br />
          degree to which skin is exposed to moisture</td>
        <td valign="top">
         <asp:DropDownList ID="txtmoisture" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Constantly Moist</asp:ListItem>
            <asp:ListItem Value="2">2. Very Moist</asp:ListItem>
            <asp:ListItem Value="3">3. Occasionally Moist</asp:ListItem>
             <asp:ListItem Value="4">4. Rarely Moist</asp:ListItem>
            </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtmoisture" ErrorMessage="กรุณาระบุ Moisture" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td valign="top">Skin is kept moist almost constantly by perspiration, urine, etc. Dampness is detected every time patient is moved or turned.</td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#EAEAEA"><strong>Activity</strong><br />
          degree of physical activity</td>
        <td valign="top" bgcolor="#EAEAEA">
         <asp:DropDownList ID="txtactivity" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Bedfast</asp:ListItem>
            <asp:ListItem Value="2">2. Chairfast</asp:ListItem>
            <asp:ListItem Value="3">3. Walks Occasionally</asp:ListItem>
             <asp:ListItem Value="4">4. Walks Frequently</asp:ListItem>
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtactivity" ErrorMessage="กรุณาระบุ Activity" Display="Dynamic"></asp:RequiredFieldValidator>
       </td>
        <td valign="top" bgcolor="#EAEAEA">Confined to bed.</td>
      </tr>
      <tr>
        <td valign="top"><strong>Mobility</strong><br />
          ability to change and control body position</td>
        <td valign="top">
           <asp:DropDownList ID="txtmobility" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Completely Immobile</asp:ListItem>
            <asp:ListItem Value="2">2. Very Limited</asp:ListItem>
            <asp:ListItem Value="3">3. Sligtly Limited</asp:ListItem>
             <asp:ListItem Value="4">4. No Limitation</asp:ListItem>
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtmobility" ErrorMessage="กรุณาระบุ Mobility" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td valign="top">Does not make even slight changes in body or extremity position without assistance.</td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#EAEAEA"><p><strong>Nutrition</strong><br />
          usual food intake pattern
        </p></td>
        <td valign="top" bgcolor="#EAEAEA">
         <asp:DropDownList ID="txtnutrition" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Very Poor</asp:ListItem>
            <asp:ListItem Value="2">2. Probably Inadequate</asp:ListItem>
            <asp:ListItem Value="3">3. Adequate</asp:ListItem>
             <asp:ListItem Value="4">4. Excellent</asp:ListItem>
            </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtnutrition" ErrorMessage="กรุณาระบุ Nutrition" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
        <td valign="top" bgcolor="#EAEAEA">Never eats a complete meal. Rarely eats more than 1/2 of any food offered. Eats 2 servings or less of protein (meat or dairy products) per day. Takes fluids poorly. Does not take a liquid dietary supplement 
          <div align="center">OR</div>
          is NPO and/or maintained on clear liquids or IVs for more than 5 days</td>
      </tr>
      <tr>
        <td valign="top"><strong>Friction &amp; Shear</strong><br /> </td>
        <td valign="top">
            <asp:DropDownList ID="txtfriction" runat="server" Width="250px">
            <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">1. Problem</asp:ListItem>
            <asp:ListItem Value="2">2. Potential Problem</asp:ListItem>
            <asp:ListItem Value="3">3. No Apparent Proble</asp:ListItem>
            </asp:DropDownList>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ControlToValidate="txtfriction" ErrorMessage="กรุณาระบุ Friction &amp; Shear" Display="Dynamic"></asp:RequiredFieldValidator>
       </td>
        <td valign="top">Requires moderate to maximum assistance in moving. Complete lifting without sliding against sheets is impossible. Frequently slides down in bed or chair, requiring frequent repositioning with maximum assistance. Spasticity, Contractures or agitation leads to almost constant friction.</td>
      </tr>
      <tr>
        <td colspan="3" valign="top" bgcolor="#EAEAEA"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Total score</strong><label>&nbsp;&nbsp;&nbsp;
          <input type="text" name="txtscore" id="txtscore"  style="width: 50px;"  runat="server"/>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ControlToValidate="txtscore" ErrorMessage="กรุณาระบุ Score" Display="Dynamic"></asp:RequiredFieldValidator>
          &nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdCalculate" runat="server" Text="Calculate" 
                CausesValidation="False" />
         
       </label>
        </td>
        </tr>
    </table></td>
  </tr>
          </table>