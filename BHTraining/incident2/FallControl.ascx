<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FallControl.ascx.vb" Inherits="incident.incident_FallControl" ClassName="incident_FallControl"  %>
          <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
          <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td valign="top">1. 
        <asp:Label ID="Label1" runat="server" Text="Who falled and age"></asp:Label></td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><asp:RadioButton ID="txtwho1" runat="server" 
                GroupName="whofall" />
          </td>
        <td width="115">Patient </td>
        <td width="23"><asp:RadioButton ID="txtwho2" runat="server" 
                GroupName="whofall" />
          </td>
        <td width="115">Relative </td>
        <td width="23"><asp:RadioButton ID="txtwho3" runat="server" 
                GroupName="whofall" />
          </td>
        <td>Visitor&nbsp;
         
          </td>
        <td width="30">Age</td>
        <td><input type="text" name="txtyear3" id="txtyear3" style="width: 30px;" runat="server" />
            yrs.&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">2. Nationality</td>
    <td valign="top">
    <table cellspacing="0" cellpadding="0" style="width: 98%">
      <tr>
        <td width="150">
            <asp:RadioButtonList ID="txtfall_nation" runat="server" 
            RepeatDirection="Horizontal" BorderWidth="0px" CellPadding="0" CellSpacing="0" 
                Width="90%">
            <asp:ListItem>Thai</asp:ListItem>
            <asp:ListItem>Arabic</asp:ListItem>
            <asp:ListItem>International</asp:ListItem>
        </asp:RadioButtonList>
        </td><td>
        &nbsp;<asp:TextBox ID="txtnation_remark" runat="server"></asp:TextBox>
        </td></tr>
        </table>
    </td>
  </tr>
  <tr>
    <td valign="top">3. Type of fall</td>
    <td valign="top">
        <asp:RadioButtonList ID="txtfalltype" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Anticipated physiological</asp:ListItem>
            <asp:ListItem Value="2">Unanticipated physiological</asp:ListItem>
            <asp:ListItem Value="3">Accidental</asp:ListItem>
        </asp:RadioButtonList>
    </td>
  </tr>
  <tr>
    <td valign="top">4. Time of fall</td>
    <td valign="top">
            <asp:DropDownList ID="txtfall_period" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">M shift</asp:ListItem>
        <asp:ListItem Value="2">E shift</asp:ListItem>
        <asp:ListItem Value="3">N shift</asp:ListItem>
        </asp:DropDownList>
    
       at
           <asp:DropDownList ID="txtfall_hour1" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtfall_min1" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
              </td>
   
  </tr>
  <tr>
    <td valign="top">5. Activity at Time of fall</td>
    <td valign="top">
        <asp:DropDownList ID="txtactivity_fall" runat="server" Width="285px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">Transferring into or out of bed</asp:ListItem>
        <asp:ListItem Value="2">Transferring in or out of chair</asp:ListItem>
        <asp:ListItem Value="3">Toileting</asp:ListItem>
        <asp:ListItem Value="4">Moving about in bed</asp:ListItem>
        <asp:ListItem Value="5">Walking</asp:ListItem>
        <asp:ListItem Value="6">Rehabilitation</asp:ListItem>
        <asp:ListItem Value="7">Sitting in a chair</asp:ListItem>
        <asp:ListItem Value="8">Sitting in a commode</asp:ListItem>
        <asp:ListItem Value="9">Sitting in a wheelchair</asp:ListItem>
        <asp:ListItem Value="10">Sitting in a stretcher</asp:ListItem>
        <asp:ListItem Value="11">Other (please identify)</asp:ListItem>
        </asp:DropDownList>
      
      &nbsp;
<input type="text" name="txtfall_remark" id="txtfall_remark" style="width: 285px" runat="server" /></td>
  </tr>
  <tr>
    <td valign="top">6. Location</td>
    <td valign="top">
         <asp:DropDownList ID="txtlocation_fall" runat="server" Width="285px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">Patient's room</asp:ListItem>
        <asp:ListItem Value="2">Patient's toilet</asp:ListItem>
        <asp:ListItem Value="3">Public toilet</asp:ListItem>
        <asp:ListItem Value="4">Public area (please identify)</asp:ListItem>
        <asp:ListItem Value="5">Other (please identify)</asp:ListItem>
       </asp:DropDownList>
 
      &nbsp;
<input type="text" name="txtlocation_remark" id="txtlocation_remark" style="width: 285px" runat="server" /></td>
  </tr>
  <!--
  <tr>
    <td valign="top">6. Renovation area</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><asp:RadioButton ID="txtreno_yes" runat="server" GroupName="renovation" />
          </td>
        <td width="215">Yes</td>
        <td width="23"><asp:RadioButton ID="txtreno_no" runat="server" GroupName="renovation"  />
          </td>
        <td>No</td>
        </tr>
    </table></td>
  </tr>
  -->
  <tr>
    <td valign="top">7. Having assistant during fall</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><asp:RadioButton ID="txtassist_yes" runat="server" GroupName="assistant" />
          <label for="radio12"></label></td>
        <td width="215">Yes</td>
        <td width="23"><asp:RadioButton ID="txtassist_no" runat="server" GroupName="assistant" />
          <label for="radio13"></label></td>
        <td>No</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">8. Post Fall : Vital sign/Neuro sign</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><asp:RadioButton ID="txtvital_normal" runat="server" GroupName="vital" />
          <label for="radio14"></label></td>
        <td width="215">Normal</td>
        <td width="23"><asp:RadioButton ID="txtvital_abnormal" runat="server" GroupName="vital" />
         </td>
        <td>Abnormal 
&nbsp;          <input type="text" name="txtvital_remark" id="txtvital_remark" style="width: 395px" runat="server" />
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">9. Examination conducted</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23"><asp:RadioButton ID="txtexam_yes" runat="server" GroupName="examination" />&nbsp;</td>
        <td><table width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="80">Yes</td>
            <td>By Dr</td>
            <td width="250"><input type="text" name="txtexam_doctor" id="txtexam_doctor" style="width: 225px" runat="server" /></td>
            <td>Date</td>
            <td width="150"><input type="text" name="txtdate_exam" id="txtdate_exam" style="width: 100px; background: #0F0;" runat="server" /> </td>
            <td>Time</td>
            <td>
             <asp:DropDownList ID="txtfall_hour2" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtfall_min2" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList>
</td>
          </tr>
        </table></td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        <td>What are investigation &amp; treatment?</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><textarea name="txttreatment" id="txttreatment" cols="45" rows="3" style="width: 700px" runat="server"></textarea></td>
        </tr>
      <tr>
        <td><asp:RadioButton ID="txtexam_no" runat="server" GroupName="examination" /></td>
        <td>Refuse treatment</td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        <td><label for="textarea4"></label>
          <textarea name="txtrefuse" id="txtrefuse" cols="45" rows="3" style="width: 700px" runat="server"></textarea></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" valign="top">10. Associate Risk Factor and Cause</td>
    </tr>
  <tr>
    <td valign="top">10.1 Medication last 24 hrs</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_alzheimer" 
                id="chk_alzheimer" runat="server" value="1" />
          </td>
        <td width="135" valign="top">Alzheimer's drug</td>
        <td width="200" valign="top"><input type="text" name="txt_alzheimer" id="txt_alzheimer" style="width: 180px" runat="server" /></td>
        <td width="23" valign="top"><input type="checkbox" name="chk_sedative" id="chk_sedative" runat="server" />
        </td>
        <td width="135" valign="top">Sedative / Hypnotics </td>
        <td valign="top"><input type="text" name="txt_sedative" id="txt_sedative" style="width: 180px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_analgesic" id="chk_analgesic" runat="server" />
          </td>
        <td valign="top">Analgesic / muscle relaxant agents</td>
        <td valign="top"><input type="text" name="txt_analgesic" id="txt_analgesic" style="width: 180px" runat="server" /></td>
        <td valign="top"><input type="checkbox" name="chk_diuretic" id="chk_diuretic" runat="server" />
         </td>
        <td valign="top">Diuretics</td>
        <td valign="top"><input type="text" name="txt_diuretic" id="txt_diuretic" style="width: 180px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_beta" id="chk_beta" runat="server" />
          <label for="checkbox24"></label></td>
        <td valign="top">Anti HT / beta-blocker</td>
        <td valign="top"><input type="text" name="txt_beta" id="txt_beta" style="width: 180px" runat="server" /></td>
        <td valign="top"><input type="checkbox" name="chk_laxative" id="chk_laxative" runat="server" />
          </td>
        <td valign="top">Laxatives</td>
        <td valign="top"><input type="text" name="txt_laxative" id="txt_laxative" style="width: 180px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_antiepil" id="chk_antiepil" runat="server" />
         </td>
        <td valign="top">Antiepileptic</td>
        <td valign="top"><input type="text" name="txt_antiepil" id="txt_antiepil" style="width: 180px" runat="server" /></td>
        <td valign="top"><input type="checkbox" name="chk_narcotic" id="chk_narcotic" runat="server" />
          <label for="checkbox30"></label></td>
        <td valign="top">Narcotic</td>
        <td valign="top"><input type="text" name="txt_narcotic" id="txt_narcotic" style="width: 180px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_benzo" id="chk_benzo" runat="server" />
          </td>
        <td valign="top">Benzodiazepines</td>
        <td valign="top"><input type="text" name="txt_benzo" id="txt_benzo" style="width: 180px" runat="server" /></td>
        <td valign="top"><input type="checkbox" name="chk_other1" id="chk_other1" runat="server" />
          </td>
        <td valign="top">Other</td>
        <td valign="top"><input type="text" name="txt_other1" id="txt_other1" style="width: 180px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.2 Patient's Factor</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_pt1" id="chk_pt1" runat="server" />
         </td>
        <td width="215" valign="top">Mobility impairment</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_pt2" id="chk_pt2" runat="server" /></td>
        <td width="215" valign="top">Poor eyesight</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_pt3" id="chk_pt3" runat="server" /></td>
        <td valign="top">Altered elimination</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_pt4" id="chk_pt4" runat="server" />
         </td>
        <td valign="top">Anemia</td>
        <td valign="top"><input type="checkbox" name="chk_pt5" id="chk_pt5" runat="server" /></td>
        <td valign="top">Old age &gt; 60 yrs</td>
        <td valign="top"><input type="checkbox" name="chk_pt6" id="chk_pt6" runat="server" /></td>
        <td valign="top">Dizziness/ Vertigo</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_pt7" id="chk_pt7" runat="server" />
          <label for="checkbox36"></label></td>
        <td valign="top">Neoplasm</td>
        <td valign="top"><input type="checkbox" name="chk_pt8" id="chk_pt8" runat="server" /></td>
        <td valign="top">Younger than 12 yrs</td>
        <td valign="top"><input type="checkbox" name="chk_pt9" id="chk_pt9" runat="server" /></td>
        <td valign="top">Implusive/ Noncompliance</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_pt10" id="chk_pt10" runat="server" />
          </td>
        <td valign="top">CHF</td>
        <td valign="top"><input type="checkbox" name="chk_pt11" id="chk_pt11" runat="server" /></td>
        <td valign="top">Postural Hypotension</td>
        <td valign="top"><input type="checkbox" name="chk_pt12" id="chk_pt12" runat="server" /></td>
        <td valign="top">Not cooperation</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_pt13" id="chk_pt13" runat="server" />
          </td>
        <td valign="top">CVA / Stroke</td>
        <td valign="top"><input type="checkbox" name="chk_pt14" id="chk_pt14" runat="server" /></td>
        <td colspan="3" valign="top">Other&nbsp;&nbsp;
          <input type="text" name="txtptother" id="txtptother" style="width: 235px" runat="server" /></td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.3 Post operative / Procedure</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="350"><input type="text" name="txtprocedure" id="txtprocedure" style="width: 335px" runat="server" /></td>
        <td width="130"><label for="radio19">&nbsp;Type of Anesthesia</label></td>
        <td>
            <asp:DropDownList ID="txttype_anes" runat="server">
            <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
            <asp:ListItem Value="1">GA</asp:ListItem>
            <asp:ListItem Value="2">SB</asp:ListItem>
            <asp:ListItem Value="3">EB</asp:ListItem>
            <asp:ListItem Value="4">IV sedation</asp:ListItem>
            <asp:ListItem Value="5">LA</asp:ListItem>
            </asp:DropDownList>
        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.4 Nursing care intervention / by staff before patient fall</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_rn_care1" id="chk_rn_care1" runat="server" />
         </td>
        <td valign="top">Mobility assisting high risk patient during transferation</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care2" id="chk_rn_care2" runat="server" />
         </td>
        <td valign="top">Provide assistance for toileting every time of nursing care intervention</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care3" id="chk_rn_care3" runat="server" />
        </td>
        <td valign="top">Reviewed list of medication by pharmacist</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care4" id="chk_rn_care4" runat="server" />
         </td>
        <td valign="top">Having assistance in toilet or sitting on commode</td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care5" id="chk_rn_care5" runat="server" /></td>
        <td valign="top">Provide toileting assistance every 2 hr or depends on patient's condition</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care6" id="chk_rn_care6" runat="server" /></td>
        <td valign="top">Position patient in easily observable area</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care7" id="chk_rn_care7" runat="server" /></td>
        <td valign="top">Consider having sitter at all time</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_rn_care8" id="chk_rn_care8" runat="server" />
        </td>
        <td valign="top">Other&nbsp;
<input type="text" name="txtrnremark" id="txtrnremark" style="width: 235px" runat="server" /></td>
        </tr>
    </table></td>
    </tr>
  <tr>
    <td valign="top">10.5 Safety of equipment</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_equip1" id="chk_equip1" runat="server" />
          </td>
        <td valign="top">Cane</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip2" id="chk_equip2" runat="server" />
          <label for="checkbox53"></label></td>
        <td valign="top">Crutch</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip3" id="chk_equip3" runat="server" />
         </td>
        <td valign="top">Consider bed alarm (Fall level 3 only)</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip4" id="chk_equip4" runat="server" />
          <label for="checkbox56"></label></td>
        <td valign="top">Furniture</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip5" id="chk_equip5" runat="server" /></td>
        <td valign="top">Use hospital slipper</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip6" id="chk_equip6" runat="server" /></td>
        <td valign="top">Walker</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip7" id="chk_equip7" runat="server" /></td>
        <td valign="top">Wheel of bed / Wheel chair has been locked</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip8" id="chk_equip8" runat="server" /></td>
        <td valign="top">Wheel chair / stretcher</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip9" id="chk_equip9" runat="server" /></td>
        <td valign="top">Using safety strapsor seat belt</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip10" id="chk_equip10" runat="server" />
          <label for="checkbox61"></label></td>
        <td valign="top">Selected suitable device</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_equip11" id="chk_equip11" 
                runat="server" /></td>
        <td valign="top">Other
<input type="text" name="txtrnremark0" id="txtequip_remark" style="width: 235px" 
                runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.6 Safety of enviroment</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_safe1" id="chk_safe1" runat="server" />
         </td>
        <td valign="top">2 upper side rails up</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe2" id="chk_safe2" runat="server" />
          <label for="checkbox65"></label></td>
        <td valign="top">Can reach to nurse call / Food tray</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe3" id="chk_safe3" runat="server" />
          <label for="checkbox66"></label></td>
        <td valign="top">Decreasing risks : obstacles and clutter on bedside / floor</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe4" id="chk_safe4" runat="server" />
          <label for="checkbox67"></label></td>
        <td valign="top">Suitable surface : dry floor</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe5" id="chk_safe5" runat="server" /></td>
        <td valign="top">Alert Signage</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe6" id="chk_safe6" runat="server" /></td>
        <td valign="top">Safety in kid zone</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe7" id="chk_safe7" runat="server" /></td>
        <td valign="top">Light</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe8" id="chk_safe8" runat="server" /></td>
        <td valign="top">Low level of bed</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_safe9" id="chk_safe9" runat="server" /></td>
        <td valign="top">Other&nbsp;
          <input type="text" name="txtsafe_remark" id="txtsafe_remark" style="width: 235px" runat="server" /></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">10.7 Inform Instruction</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_inform1" id="chk_inform1" runat="server" />
          </td>
        <td valign="top">Re-oriented confused patients</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_inform2" id="chk_inform2" runat="server" />
         </td>
        <td valign="top">Educate / Instruct patient and relative about fall prevention</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_inform3" id="chk_inform3" runat="server" />
          </td>
        <td valign="top">Orientating patient to bed area / ward facilities and not to get assistance</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.8 Assessment / Reassessment</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="115">When (Last time)</td>
        <td width="150">
            <asp:TextBox ID="txtdate_assess" runat="server" Width="100px" BackColor="GreenYellow"></asp:TextBox>
            <asp:CalendarExtender ID="txtdate_assess_CalendarExtender" runat="server" 
                ClearTime="True" Enabled="True" TargetControlID="txtdate_assess" 
                Format="dd/MM/yyyy">
            </asp:CalendarExtender>
        &nbsp;</td>
        <td width="40">Time</td>
        <td width="115"> <asp:DropDownList ID="txtfall_hour3" runat="server">
           <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
              </asp:DropDownList>
            :
              <asp:DropDownList ID="txtfall_min3" runat="server">
               <asp:ListItem Value="0">00</asp:ListItem>
               <asp:ListItem Value="1">01</asp:ListItem>
               <asp:ListItem Value="2">02</asp:ListItem>
               <asp:ListItem Value="3">03</asp:ListItem>
               <asp:ListItem Value="4">04</asp:ListItem>
               <asp:ListItem Value="5">05</asp:ListItem>
               <asp:ListItem Value="6">06</asp:ListItem>
               <asp:ListItem Value="7">07</asp:ListItem>
               <asp:ListItem Value="8">08</asp:ListItem>
               <asp:ListItem Value="9">09</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
               <asp:ListItem Value="11">11</asp:ListItem>
               <asp:ListItem Value="12">12</asp:ListItem>
               <asp:ListItem Value="13">13</asp:ListItem>
               <asp:ListItem Value="14">14</asp:ListItem>
               <asp:ListItem Value="15">15</asp:ListItem>
               <asp:ListItem Value="16">16</asp:ListItem>
               <asp:ListItem Value="17">17</asp:ListItem>
               <asp:ListItem Value="18">18</asp:ListItem>
               <asp:ListItem Value="19">19</asp:ListItem>
               <asp:ListItem Value="20">20</asp:ListItem>
               <asp:ListItem Value="21">21</asp:ListItem>
               <asp:ListItem Value="22">22</asp:ListItem>
               <asp:ListItem Value="23">23</asp:ListItem>
               <asp:ListItem Value="24">24</asp:ListItem>
               <asp:ListItem Value="25">25</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="27">27</asp:ListItem>
               <asp:ListItem Value="28">28</asp:ListItem>
               <asp:ListItem Value="29">29</asp:ListItem>
               <asp:ListItem Value="30">30</asp:ListItem>
               <asp:ListItem Value="31">31</asp:ListItem>
               <asp:ListItem Value="32">32</asp:ListItem>
               <asp:ListItem Value="33">33</asp:ListItem>
               <asp:ListItem Value="34">34</asp:ListItem>
               <asp:ListItem Value="35">35</asp:ListItem>
               <asp:ListItem Value="36">36</asp:ListItem>
               <asp:ListItem Value="37">37</asp:ListItem>
               <asp:ListItem Value="38">38</asp:ListItem>
               <asp:ListItem Value="39">39</asp:ListItem>
               <asp:ListItem Value="40">40</asp:ListItem>
               <asp:ListItem Value="41">41</asp:ListItem>
               <asp:ListItem Value="42">42</asp:ListItem>
               <asp:ListItem Value="43">43</asp:ListItem>
               <asp:ListItem Value="44">44</asp:ListItem>
               <asp:ListItem Value="45">45</asp:ListItem>
               <asp:ListItem Value="46">46</asp:ListItem>
               <asp:ListItem Value="47">47</asp:ListItem>
               <asp:ListItem Value="48">48</asp:ListItem>
               <asp:ListItem Value="49">49</asp:ListItem>
               <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="51">51</asp:ListItem>
               <asp:ListItem Value="52">52</asp:ListItem>
               <asp:ListItem Value="53">53</asp:ListItem>
               <asp:ListItem Value="54">54</asp:ListItem>
               <asp:ListItem Value="55">55</asp:ListItem>
               <asp:ListItem Value="56">56</asp:ListItem>
               <asp:ListItem Value="57">57</asp:ListItem>
               <asp:ListItem Value="58">58</asp:ListItem>
               <asp:ListItem Value="59">59</asp:ListItem>
                
              </asp:DropDownList></td>
        <td width="60">Reason</td>
        <td><input type="text" name="txtreason" id="txtreason" style="width: 235px" runat="server" /></td>
      </tr>
    </table>
      <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_assess1" id="chk_assess1" runat="server" />
          </td>
        <td valign="top">On admission to hospital</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_assess2" id="chk_assess2" runat="server" />
          <label for="checkbox77"></label></td>
        <td valign="top">Post operative patient</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_assess3" id="chk_assess3" runat="server" /></td>
        <td valign="top">On medicine effect / changed medicine order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_assess4" id="chk_assess4" runat="server" /></td>
        <td valign="top">On OPD Nursing intervention</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_assess5" id="chk_assess5" runat="server" />
          <label for="checkbox78"></label></td>
        <td valign="top">Other&nbsp;
          <input type="text" name="txtassess_other" id="txtassess_other" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">10.9 Other</td>
    <td valign="top">  <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_factor_other" id="chk_factor_other" runat="server" />
          </td>
          <td>Other 
          <input type="text" name="txtfactor_other" id="txtfactor_other" 
                  style="width: 235px" runat="server" /></td>
          </tr>
          </table>

  </tr>
  <tr>
    <td valign="top">11. Fall Risk Assessment</td>
    <td valign="top">
    <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td valign="top" >
         <strong>Identify reason for reassessment:</strong>
         
         </td>
        <td width="100" valign="top" style="text-align: center"><b>Risk points</b></td>
        <td width="100" valign="top" style="text-align: center"><b>Score (*w)</b></td>
        <td width="100" valign="top" style="text-align: center"><b>Score (**sp)</b></td>
      </tr>
         <tr>
        <td valign="top" style="color:red">Patient is immobile/comatose, no fall risk</td>
        <td valign="top" style="text-align: center">0</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw13" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
          
             
            </asp:DropDownList>
      
             </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts13" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
       
              
            </asp:DropDownList>
      
             </td>
      </tr>
         <tr>
        <td valign="top" style="color:red">History of fall within 1 yr or &gt;= 65 yrs is considered high risk</td>
        <td valign="top" style="text-align: center">5</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw14" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
           <asp:ListItem Value="5">5</asp:ListItem>
             
            </asp:DropDownList>
      
             </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts14" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="5">5</asp:ListItem>
              
            </asp:DropDownList>
      
             </td>
      </tr>
      </table>
    <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td valign="top" >
          <strong>Fall Risk Factor</strong></td>
        <td width="100" valign="top" style="text-align: center"><b>Risk points</b></td>
        <td width="100" valign="top" style="text-align: center"><b>Score (*w)</b></td>
        <td width="100" valign="top" style="text-align: center"><b>Score (**sp)</b></td>
      </tr>
      <tr>
        <td valign="top">Confusion/Disorientation</td>
        <td valign="top" style="text-align: center">4</td>
        <td valign="top" style="text-align: center">
            <asp:DropDownList ID="txtw1" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            </asp:DropDownList>
      
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts1" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            </asp:DropDownList>
       
        </td>
      </tr>
      <tr>
        <td valign="top">Any history of depression/feeling down/sad now</td>
        <td valign="top" style="text-align: center">2</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw2" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts2" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            </asp:DropDownList>
        
        </td>
      </tr>
      <tr>
        <td valign="top">Altered elimination</td>
        <td valign="top" style="text-align: center">1</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw3" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
        
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts3" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
      </tr>
      <tr>
        <td valign="top">Dizziness/Vertigo</td>
        <td valign="top" style="text-align: center">1</td>
        <td valign="top" style="text-align: center">
           <asp:DropDownList ID="txtw4" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
           <asp:DropDownList ID="txts4" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
      </td>
      </tr>
      <tr>
        <td valign="top">Implusive/Non compliance</td>
        <td valign="top" style="text-align: center">1</td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txtw5" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts5" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
      
        </td>
      </tr>
      <tr>
        <td valign="top">Any prescribed antiepileptic within 24 hrs</td>
        <td valign="top" style="text-align: center">2</td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txtw6" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts6" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            </asp:DropDownList>
       
        </td>
      </tr>
      <tr>
        <td valign="top">Any benzodiazepine/Sedation within 24 hrs</td>
        <td valign="top" style="text-align: center">1</td>
        <td valign="top" style="text-align: center">
           <asp:DropDownList ID="txtw7" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts7" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
      </tr>
      <tr>
        <td valign="top" style="color:red">Any new/increase dose for diuretic, antipsychotic,<br />
            hypnotics &amp; sedatives, antihypertensive, narcotic</td>
        <td valign="top" style="text-align: center">2</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw12" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
          
              <asp:ListItem Value="4">2</asp:ListItem>
            </asp:DropDownList>
      
          </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts12" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
       
              <asp:ListItem Value="4">2</asp:ListItem>
            </asp:DropDownList>
      
          </td>
      </tr>
      <tr>
        <td valign="top">
        Get-up-and-go Test (Choose one)<br />
        Able to rise in single movement</td>
        <td valign="top" style="text-align: center">0</td>
        <td valign="top" style="text-align: center">
           <asp:DropDownList ID="txtw8" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
           
            </asp:DropDownList>
        
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts8" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
           
            </asp:DropDownList>
      
        </td>
      </tr>
      <tr>
        <td valign="top">Pushes up successful in one attempt</td>
        <td valign="top" style="text-align: center">1</td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txtw9" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts9" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            </asp:DropDownList>
       
        </td>
      </tr>
      <tr>
        <td valign="top">Multiple attempts but successful</td>
        <td valign="top" style="text-align: center">3</td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txtw10" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
          
              <asp:ListItem Value="3">3</asp:ListItem>
            </asp:DropDownList>
       
        </td>
        <td valign="top" style="text-align: center">
          <asp:DropDownList ID="txts10" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
           
              <asp:ListItem Value="3">3</asp:ListItem>
            </asp:DropDownList>
      
        </td>
      </tr>
      <tr>
        <td valign="top">Unable to rise without assistance</td>
        <td valign="top" style="text-align: center">4</td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txtw11" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
          
              <asp:ListItem Value="4">4</asp:ListItem>
            </asp:DropDownList>
      
        </td>
        <td valign="top" style="text-align: center">
         <asp:DropDownList ID="txts11" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
       
              <asp:ListItem Value="4">4</asp:ListItem>
            </asp:DropDownList>
      
        </td>
      </tr>
    </table>
      <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
      <tr>
        <td width="300"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
          <tr>
            <td colspan="2" class="red"><table width="100%" cellspacing="0" cellpadding="0">
              <tr>
                <td width="40%" valign="top"><table width="95%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td colspan="2">Fall Score</td>
                    </tr>
                  <tr>
                    <td>High Risk</td>
                    <td>5 and higher</td>
                  </tr>
                  <tr>
                    <td>Moderate Risk</td>
                    <td>3-4</td>
                  </tr>
                  <tr>
                    <td>Low Risk</td>
                    <td>0-2</td>
                  </tr>
                </table></td>
                <td valign="top" class="red">* w = Assessment by ward nurse<br />
** sp = Assessment by fall prevention specialist</td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>Ward assessment score</td>
            <td width="300">
               
                <asp:DropDownList ID="txtward_sc" runat="server">
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
            <asp:ListItem Value="17">17</asp:ListItem>
            <asp:ListItem Value="18">18</asp:ListItem>
            <asp:ListItem Value="19">19</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
             <asp:ListItem Value="21">21</asp:ListItem>
              <asp:ListItem Value="22">22</asp:ListItem>
              <asp:ListItem Value="23">23</asp:ListItem>
              <asp:ListItem Value="24">24</asp:ListItem>
              <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="26">26</asp:ListItem>
              <asp:ListItem Value="27">27</asp:ListItem>
                </asp:DropDownList>
            </td>
          </tr>
          <tr>
            <td>Fall prevention specialist assessment/ Manager/ Supervisor/ Incharge score</td>
            <td>
            
              <asp:DropDownList ID="txtmanager_sc" runat="server">
                <asp:ListItem Value="0">0</asp:ListItem>
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
            <asp:ListItem Value="18">18</asp:ListItem>
            <asp:ListItem Value="19">19</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
             <asp:ListItem Value="21">21</asp:ListItem>
              <asp:ListItem Value="22">22</asp:ListItem>
              <asp:ListItem Value="23">23</asp:ListItem>
              <asp:ListItem Value="24">24</asp:ListItem>
              <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="26">26</asp:ListItem>
              <asp:ListItem Value="27">27</asp:ListItem>
                </asp:DropDownList>
            </td>
          </tr>
          </table></td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">
        &nbsp;</td>
  </tr>
    <tr>
    <td valign="top">12. Risk level of fall</td>
    <td valign="top">
        <asp:DropDownList ID="txtlevelfall" runat="server" Width="635px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">Level 1</asp:ListItem>
        <asp:ListItem Value="2">Level 2</asp:ListItem>
        <asp:ListItem Value="3">Level 3</asp:ListItem>
         <asp:ListItem Value="4">Unknown</asp:ListItem>
        </asp:DropDownList>
    </td>
  </tr>
  <tr>
    <td valign="top">13. Severity outcome</td>
    <td valign="top">
     <asp:DropDownList ID="txtsecure" runat="server" Width="635px">
        <asp:ListItem Value="">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">Level 0 (No any injuries)</asp:ListItem>
        <asp:ListItem Value="2">Level 1 (application if a dressing, ice, cleaning of a wound, limb elevation, or tipical medication)</asp:ListItem>
        <asp:ListItem Value="3">Level 2 (Resulted in suturing, application of sterile-strips/skin glue, or splinting)</asp:ListItem>
        <asp:ListItem Value="4">Level 3 (result in surgery, casting, traction, or required consultation for neurological or internal injury)</asp:ListItem>
        <asp:ListItem Value="5">Level 4 (the patient died as a result of injuries sustained from the fall)</asp:ListItem>
        </asp:DropDownList>
</td>
  </tr>
  </table>
