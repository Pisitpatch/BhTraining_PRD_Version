<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MedControl.ascx.vb" Inherits="incident.incident_MedControl" %>
 <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td valign="top">1. <asp:Label ID="lblIRMed1" runat="server" Text="Serious type"></asp:Label></td>
    <td valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td valign="top">1.1 <asp:Label ID="lblIRMed2" runat="server" Text="Type I"></asp:Label></td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23"><input type="checkbox" name="chk_wrongtime" id="chk_wrongtime" runat="server" /></td>
        <td width="150">wrong time</td>
        <td width="23"><input type="checkbox" name="chk_wrongroute" id="chk_wrongroute" runat="server" /></td>
        <td width="150">wrong route</td>
        <td width="23"><input type="checkbox" name="chk_wrongdate" id="chk_wrongdate" runat="server" /></td>
        <td width="150">wrong date</td>
        <td width="23"><input type="checkbox" name="chk_wrongrate" id="chk_wrongrate" runat="server" /></td>
        <td>wrong rate</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_omission" id="chk_omission" runat="server" /></td>
        <td>omission</td>
        <td><input type="checkbox" name="chk_wronglabel" id="chk_wronglabel" runat="server" /></td>
        <td>wrong label</td>
        <td><input type="checkbox" name="chk_wrongform" id="chk_wrongform" runat="server" /></td>
        <td>wrong form</td>
        <td><input type="checkbox" name="chk_wrongbrand" id="chk_wrongbrand" 
                runat="server" /></td>
        <td>wrong brand</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">1.2 Type II</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23"><input type="checkbox" name="chk_wrongdose" id="chk_wrongdose" runat="server" /></td>
        <td width="150">wrong dose</td>
        <td width="23"><input type="checkbox" name="chk_extradose" id="chk_extradose" runat="server" /></td>
        <td width="150">extra dose</td>
        <td width="23"><input type="checkbox" name="chk_wrongiv" id="chk_wrongiv" runat="server" /></td>
        <td width="150">wrong IV solution</td>
        <td width="23"><input type="checkbox" name="chk_wrong_deteriorate" id="chk_wrong_deteriorate" runat="server" /></td>
        <td>deteriorated drug</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_wrong_prep" id="chk_wrong_prep" runat="server" /></td>
        <td>wrong drug preparation</td>
        <td><input type="checkbox" name="chk_wrong_drugerror" id="chk_wrong_drugerror" runat="server" /></td>
        <td>unauthorized drug error</td>
        <td><input type="checkbox" name="chk_wrong_duplicate" id="chk_wrong_duplicate" runat="server" /></td>
        <td>duplicate drug</td>
        <td><input type="checkbox" name="chk_wrong_formular" id="chk_wrong_formular" runat="server" /></td>
        <td>wrong formula</td>
      </tr>
      <tr>
        <td><input type="checkbox" name="chk_wrong_qty" id="chk_wrong_qty" runat="server" /></td>
        <td>wrong quantity</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">1.3 Type III</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23"><input type="checkbox" name="chk_wrong_drug" id="chk_wrong_drug" runat="server" /></td>
        <td width="150">wrong drug</td>
        <td width="23"><input type="checkbox" name="chk_wrong_pt" id="chk_wrong_pt" runat="server" /></td>
        <td width="150">wrong patient</td>
        <td width="23"><input type="checkbox" name="chk_allergy" id="chk_allergy" runat="server" /></td>
        <td>known allergy information</td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">2. Level of serverity outcome</td>
    <td valign="top">
        <asp:DropDownList ID="txtmed_level" runat="server" Width="285px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1" >Level 0  An error occurred but the error did not reach the patient</asp:ListItem>
        <asp:ListItem Value="2">Level 1 An error occurred and reached the patient but did not cause harm</asp:ListItem>
        <asp:ListItem Value="3">Level 2  An error occurred , reached the patient , and required intervention to preclude harm</asp:ListItem>
        <asp:ListItem Value="4">Level 3 An error occurred that may have contributed to or resulted in temporary harm to the patient and require intervention</asp:ListItem>
        <asp:ListItem Value="5">Level 4  An error occurred that may have contributed to or resulted in temporary harm to the patient and required initial or prolonged hospitalization</asp:ListItem>
        <asp:ListItem Value="6">Level 5 An error occurred that may have contributed to or resulted in permanent harm to patient</asp:ListItem>
        <asp:ListItem Value="7">Level 6  An error occurred that may have contributed to or resulted in a near-death event</asp:ListItem>
        <asp:ListItem Value="8">Level 7 An error occurred that may have contributed to or resulted in the patient’s death</asp:ListItem>
        </asp:DropDownList>
    </td>
  </tr>
  <tr>
    <td valign="top">3. Drug category</td>
    <td valign="top">
    <asp:DropDownList ID="txtmed_category" runat="server" Width="285px" Visible="False">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">A</asp:ListItem>
        <asp:ListItem Value="2">B</asp:ListItem>
        <asp:ListItem Value="3">C</asp:ListItem>
        <asp:ListItem Value="4">D</asp:ListItem>
        <asp:ListItem Value="5">E</asp:ListItem>
        
        </asp:DropDownList>
   </td>
  </tr>
  <tr>
    <td valign="top">3.1 Drug wrong name  </td>
    <td valign="top">
  
                     <asp:GridView ID="GridDrugWrongName" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="wrong_drug_id" HeaderStyle-CssClass="colname" 
                          EnableModelValidation="True" 
            EmptyDataText="There is no data.">
           <Columns>
               <asp:BoundField />
               <asp:BoundField DataField="drug_wrong_name" 
                   HeaderText="Drug wrong name" >
                   <ItemStyle/>
               </asp:BoundField>
            
               <asp:BoundField DataField="drug_group" HeaderText="Drug wrong group" />
                  <asp:BoundField DataField="chk_alert" HeaderText="High alert drug" />
                    <asp:BoundField DataField="chk_floor" HeaderText="Floor Stock" />
                  <asp:BoundField DataField="lasa_name" HeaderText="Type" />
            
               <asp:BoundField DataField="chk_smartpump" HeaderText="Smart/Infusion Pump" />
               <asp:BoundField DataField="chk_bcma" HeaderText="BCMA" />
            
               <asp:CommandField ShowDeleteButton="True">
                   <ItemStyle Width="80px" />
               </asp:CommandField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
         </td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">
      </td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">Drug group <input type="text" name="txtdruggroup" id="txtdruggroup" style="width: 335px" runat="server" /></td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">Drug name <input type="text" name="txtdrugname" id="txtdrugname" 
            style="width: 335px" runat="server" title="Auto complete" /> 
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/spellcheck.png" />
         &nbsp;
         &nbsp;
         <asp:DropDownList ID="txtlasa" runat="server">
         <asp:ListItem Value="">-</asp:ListItem>
          <asp:ListItem Value="1">Look Alike</asp:ListItem>
           <asp:ListItem Value="2">Sound Alike</asp:ListItem>
        </asp:DropDownList><br /> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
         <asp:CheckBox ID="chkHighAlert" runat="server" Text="High alert drug" />
         <asp:CheckBox ID="chkFloorStock" runat="server" Text="Floor Stock" />
          <asp:CheckBox ID="chkSmartPump" runat="server" Text="Smart/Infusion pump" />
&nbsp;<asp:CheckBox ID="chkBCMA" runat="server" Text="BCMA " />
        <br />
       

        <asp:Button ID="cmdAddWrongName" runat="server" 
            Text="Add drug wrong name" CausesValidation="False" />
         </td>
  </tr>
  <tr>
    <td valign="top">3.2 Drug right name</td>
    <td valign="top">
        <input type="text" name="txtdrugname_right" id="txtdrugname_right" 
            style="width: 535px" runat="server" title="Auto complete" /> 
        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/spellcheck.png" />
         </td>
  </tr>
  <tr>
    <td valign="top">4. Categorization event of medication error</td>
    <td valign="top">
    <table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_err_order" id="chk_err_order" runat="server" />
          </td>
        <td width="215" valign="top">Prescribing/ Ordering error</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_err_transcription" id="chk_err_transcription" runat="server" /></td>
        <td width="215" valign="top">Transcription error</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_err_key" id="chk_err_key" runat="server" /></td>
        <td valign="top">Key order error (order entry)</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_err_verify" id="chk_err_verify" runat="server" />
          </td>
        <td valign="top">Verification error</td>
        <td valign="top"><input type="checkbox" name="chk_err_predis" id="chk_err_predis" runat="server" /></td>
        <td valign="top">Pre-dispensing error</td>
        <td valign="top"><input type="checkbox" name="chk_err_dispensing" id="chk_err_dispensing" runat="server" /></td>
        <td valign="top">Dispensing error</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_err_preadmin" id="chk_err_preadmin" runat="server" />
          </td>
        <td valign="top">Pre-administration error</td>
        <td valign="top"><input type="checkbox" name="chk_err_admin" id="chk_err_admin" runat="server" /></td>
        <td valign="top">Administration error</td>
        <td valign="top"><input type="checkbox" name="chk_err_monitor" id="chk_err_monitor" runat="server" /></td>
        <td valign="top">Monitoring error</td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">5. Type of Medication order</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_order_type1" id="chk_order_type1" runat="server" />
          </td>
        <td width="215" valign="top">Standard orders</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_order_type2" id="chk_order_type2" runat="server" /></td>
        <td width="215" valign="top">Single orders</td>
        <td width="23" valign="top"><input type="checkbox" name="chk_order_type3" id="chk_order_type3" runat="server" /></td>
        <td valign="top">verbal or Telelphone orders</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_order_type4" id="chk_order_type4" runat="server" />
          </td>
        <td valign="top">stat orders</td>
        <td valign="top"><input type="checkbox" name="chk_order_type5" id="chk_order_type5" runat="server" /></td>
        <td valign="top">P.R.N orders</td>
        <td valign="top"><input type="checkbox" name="chk_order_type6" id="chk_order_type6" runat="server" /></td>
        <td valign="top">Order for continues</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_order_type7" id="chk_order_type7" runat="server" />
          </td>
        <td valign="top">Order for&nbsp;<asp:DropDownList ID="txtorder_type" runat="server">
        <asp:ListItem Value="">-- Please Select  --</asp:ListItem>
        <asp:ListItem Value="Dose">Dose</asp:ListItem>
        <asp:ListItem Value="Day">Day</asp:ListItem>
        <asp:ListItem Value="Week">Week</asp:ListItem>
            </asp:DropDownList>
          </td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top">6. Robot product</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23" ><asp:RadioButton ID="txtrobot1" GroupName="robot" runat="server" />
          </td>
        <td width="215" >Yes</td>
        <td width="23" >
            <asp:RadioButton ID="txtrobot2" runat="server" GroupName="robot" />
          </td>
        <td >No</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">7. CPOE</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
            <asp:RadioButton ID="txtcpoe1" runat="server" GroupName="cpoe" />
          </td>
        <td width="215">Yes</td>
        <td width="23">
            <asp:RadioButton ID="txtcpoe2" runat="server" GroupName="cpoe" />
          </td>
        <td>No</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">8. MAR error</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23">
            <asp:RadioButton ID="txtmar1" runat="server" GroupName="mar" />
          </td>
        <td width="215">Yes</td>
        <td width="23">
            <asp:RadioButton ID="txtmar2" runat="server" GroupName="mar" />
          </td>
        <td>No</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">9. Time of incident</td>
    <td valign="top">
        <asp:DropDownList ID="txtmed_period" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">M shift</asp:ListItem>
        <asp:ListItem Value="2">E shift</asp:ListItem>
        <asp:ListItem Value="3">N shift</asp:ListItem>
        </asp:DropDownList>
    
       at
              
            <asp:DropDownList ID="txtmed_hour1" runat="server">
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
              <asp:DropDownList ID="txtmed_min1" runat="server">
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
  <!--
  <tr>
    <td valign="top">10. Peroid of employment</td>
    <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_rn" id="chk_rn" runat="server" />
         </td>
        <td width="135" valign="top">Nursing</td>
        <td valign="top">
         <asp:DropDownList ID="txtrn_exp" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">on probation</asp:ListItem>
        <asp:ListItem Value="2">&lt; 1 yr</asp:ListItem>
        <asp:ListItem Value="3">1-2 yr</asp:ListItem>
        <asp:ListItem Value="4">2-5 yrs</asp:ListItem>
         <asp:ListItem Value="5">&gt; 5 yrs</asp:ListItem>
        </asp:DropDownList>
       </td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_ph" id="chk_ph" runat="server" />
          </td>
        <td valign="top">Pharmacist</td>
        <td valign="top"><asp:DropDownList ID="txtph_exp" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">on probation</asp:ListItem>
        <asp:ListItem Value="2">&lt; 1 yr</asp:ListItem>
        <asp:ListItem Value="3">1-2 yr</asp:ListItem>
        <asp:ListItem Value="4">2-5 yrs</asp:ListItem>
         <asp:ListItem Value="5">&gt; 5 yrs</asp:ListItem>
        </asp:DropDownList></td>
        </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_aph" id="chk_aph" runat="server" />
          </td>
        <td valign="top">Assist-Pharmacist</td>
        <td valign="top"><asp:DropDownList ID="txtaph_exp" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">on probation</asp:ListItem>
        <asp:ListItem Value="2">&lt; 1 yr</asp:ListItem>
        <asp:ListItem Value="3">1-2 yr</asp:ListItem>
        <asp:ListItem Value="4">2-5 yrs</asp:ListItem>
         <asp:ListItem Value="5">&gt; 5 yrs</asp:ListItem>
        </asp:DropDownList></td>
        </tr>
      </table></td>
  </tr>
  -->
  <tr>
    <td valign="top">10. Peroid of employment</td>
    <td valign="top">
                     <asp:GridView ID="GridMedPeriod" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="ir_med_id" HeaderStyle-CssClass="colname" 
                          EnableModelValidation="True" EmptyDataText="There is no data.">
           <Columns>
               <asp:BoundField />
               <asp:BoundField DataField="job_type_name" 
                   HeaderText="Job type" >
                   <ItemStyle Width="350px" />
               </asp:BoundField>
               <asp:BoundField DataField="work_name" HeaderText="Work process" />
               <asp:BoundField DataField="period_name" HeaderText="Peroid of employment" 
                    >
                 
               </asp:BoundField>
               <asp:CommandField ShowDeleteButton="True">
                   <ItemStyle Width="80px" />
               </asp:CommandField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
          </td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">
      Job type&nbsp;  <asp:DropDownList ID="txtadd_jobtype" runat="server">
        <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
        <asp:ListItem Value="1">Nursing</asp:ListItem>
          <asp:ListItem Value="2">Pharmacist</asp:ListItem>
         <asp:ListItem Value="3">Assist-Pharmacist</asp:ListItem>
          <asp:ListItem Value="4">Physician</asp:ListItem>
          <asp:ListItem Value="5">Secretary</asp:ListItem>
          <asp:ListItem Value="6">Practical Nurse</asp:ListItem>
            <asp:ListItem Value="99">Other</asp:ListItem>
        </asp:DropDownList>
        &nbsp;Period of employment <asp:DropDownList ID="txtadd_period" runat="server" Width="160px">
        <asp:ListItem Value="0">------ Please Select ------</asp:ListItem>
        <asp:ListItem Value="1">on probation</asp:ListItem>
        <asp:ListItem Value="2">&lt; 1 yr</asp:ListItem>
        <asp:ListItem Value="3">1-2 yr</asp:ListItem>
        <asp:ListItem Value="4">2-5 yrs</asp:ListItem>
         <asp:ListItem Value="5">&gt; 5 yrs</asp:ListItem>
        </asp:DropDownList>
      &nbsp;Work Process
        <asp:DropDownList ID="txtworkprocess" runat="server">
            <asp:ListItem Value="">-</asp:ListItem>
            <asp:ListItem Value="1">Create</asp:ListItem>
            <asp:ListItem Value="2">Review</asp:ListItem>
            <asp:ListItem Value="3">Fill</asp:ListItem>
            <asp:ListItem Value="4">Check</asp:ListItem>
            <asp:ListItem Value="5">Dispense</asp:ListItem>
            <asp:ListItem Value="6">Admin</asp:ListItem>
            <asp:ListItem Value="7">Prescribe </asp:ListItem>
            <asp:ListItem Value="8">Scan </asp:ListItem>
            <asp:ListItem Value="9">T.O. /  V.O. /  W.O. </asp:ListItem>
            <asp:ListItem Value="10">Prepare</asp:ListItem>
            <asp:ListItem Value="11">Co-Sign</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="cmdAddPeriod" runat="server" Text="Add" 
            CausesValidation="False"  />
      </td>
  </tr>
  <tr>
    <td valign="top">11. Cause</td>
    <td valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td valign="top">11.1 Personal/ Human factor</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_h1" id="chk_h1" runat="server" />
         </td>
        <td valign="top">H1 Lack of knowledge</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h2" id="chk_h2" runat="server" />
          </td>
        <td valign="top">H2 Performance deficit</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h3" id="chk_h3" runat="server" />
          </td>
        <td valign="top">H3 Miscalculation of dosage or infusion rate</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h4" id="chk_h4" runat="server" />
         </td>
        <td valign="top">H4 Error in stocking/ restocking/ cart filling</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h5" id="chk_h5" runat="server" /></td>
        <td valign="top">H5 Key in computer error</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h6" id="chk_h6" runat="server" /></td>
        <td valign="top">H6 Transcription error</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h7" id="chk_h7" runat="server" /></td>
        <td valign="top">H7 Stress (high volume work load, etc.)</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h8" id="chk_h8" runat="server" /></td>
        <td valign="top">H8 Fatigue/ Lack of sleep</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h9" id="chk_h9" runat="server" /></td>
        <td valign="top">H9 Human error/ Careless</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h10" id="chk_h10" runat="server" /></td>
        <td valign="top">H10 Misread or didn't read</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_h11" id="chk_h11" runat="server" />
          </td>
        <td valign="top">H11 Other&nbsp;
          <input type="text" name="txth11_remark" id="txth11_remark" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.2 Communication breakdown</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_c1" id="chk_c1" runat="server" />
         </td>
        <td valign="top">C1 Verbal miscommunication/ Order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c2" id="chk_c2" runat="server" />
          </td>
        <td valign="top">C2 Ilegible handwriting</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c3" id="chk_c3" runat="server" />
          </td>
        <td valign="top">C3 Abbreviations</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c4" id="chk_c4" runat="server" />
          </td>
        <td valign="top">C4 Non-metric units of measurement</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c5" id="chk_c5" runat="server" /></td>
        <td valign="top">C5 Decimal point</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c6" id="chk_c6" runat="server" /></td>
        <td valign="top">C6 Misinterpretation of the order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c7" id="chk_c7" runat="server" /></td>
        <td valign="top">C7 Lack of communication among care team</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_c8" id="chk_c8" runat="server" />
          </td>
        <td valign="top">C8 Other&nbsp;
          <input type="text" name="txtc8_remark" id="txtc8_remark" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.3 Poor practice habit</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_p1" id="chk_p1" runat="server" />
         </td>
        <td valign="top">P1 Not follow drug administrative policy</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p2" id="chk_p2" runat="server" />
         </td>
        <td valign="top">P2 Be in rushed/ hurried</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p3" id="chk_p3" runat="server" />
         </td>
        <td valign="top">P3 No signature on MAR</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p4" id="chk_p4" runat="server" />
          </td>
        <td valign="top">P4 Unused MAR</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p5" id="chk_p5" runat="server" /></td>
        <td valign="top">P5 No co-signature</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p6" id="chk_p6" runat="server" /></td>
        <td valign="top">P6 Do short cut/ not followed process of receiving order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p7" id="chk_p7" runat="server" /></td>
        <td valign="top">P7 Stockpiling unused medication</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p8" id="chk_p8" runat="server" /></td>
        <td valign="top">P8 Loss/ Incompleted assessment</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p9" id="chk_p9" runat="server" /></td>
        <td valign="top">P9 Incompleted/ Failure to check order, MAR</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p10" id="chk_p10" runat="server" /></td>
        <td valign="top">P10 Lack of verifying process</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p11" id="chk_p11" runat="server" /></td>
        <td valign="top">P11 No verify patient's own drug</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p12" id="chk_p12" runat="server" /></td>
        <td valign="top">P12 No giving information</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p13" id="chk_p13" runat="server" /></td>
        <td valign="top">P13 Assume/ Guess intent order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p14" id="chk_p14" runat="server" /></td>
        <td valign="top">P14 Patient identification</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p15" id="chk_p15" runat="server" /></td>
        <td valign="top">P15 Used another patient medicine</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p16" id="chk_p16" runat="server" /></td>
        <td valign="top">P16 Not corrected form 1st default or reordering</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p17" id="chk_p17" runat="server" /></td>
        <td valign="top">P17 Kept document in wrong chart</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p18" id="chk_p18" runat="server" /></td>
        <td valign="top">P18 Not verify document</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p19" id="chk_p19" runat="server" /></td>
        <td valign="top">P19 Incompleted/ Lack of checking process</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p20" id="chk_p20" runat="server" /></td>
        <td valign="top">P20 Lack of follow up</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_p21" id="chk_p21" runat="server" />
          <label for="checkbox40"></label></td>
        <td valign="top">P21 Other&nbsp;
          <input type="text" name="txtp21_remark" id="txtp21_remark" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.4 System</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_s1" id="chk_s1" runat="server" />
          </td>
        <td valign="top">S1 Staffing</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s2" id="chk_s2" runat="server" />
         </td>
        <td valign="top">S2 Lighting</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s3" id="chk_s3" runat="server" />
          </td>
        <td valign="top">S3 Noise level</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s4" id="chk_s4" runat="server" />
          </td>
        <td valign="top">S4 Frequent interruptions and distractions </td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s5" id="chk_s5" runat="server" /></td>
        <td valign="top">S5 Assignment or placement of healthcare/ inexperienced personal</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s6" id="chk_s6" runat="server" /></td>
        <td valign="top">S6 Floor stock</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s7" id="chk_s7" runat="server" /></td>
        <td valign="top">S7 Pre-printed medication order</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s8" id="chk_s8" runat="server" /></td>
        <td valign="top">S8 Computer program (bug/error)</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_s9" id="chk_s9" runat="server" />
          </td>
        <td valign="top">S9 Other&nbsp;
          <input type="text" name="txts9_remark" id="txts9_remark" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.5 Product/ Package &amp; Design</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_d1" id="chk_d1" runat="server" />
          </td>
        <td valign="top">D1 Look alike product</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_d2" id="chk_d2" runat="server" />
         </td>
        <td valign="top">D2 Sound alike product</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_d3" id="chk_d3" runat="server" />
          </td>
        <td valign="top">D3 Similar color/ shape/ size</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_d4" id="chk_d4" runat="server" />
          </td>
        <td valign="top">D4 Multiple dosage form</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_d5" id="chk_d5" runat="server" /></td>
        <td valign="top">D5 Similar drug name/ spelling</td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_d6" id="chk_d6" runat="server" />
         </td>
        <td valign="top">D6 Other&nbsp;
          <input type="text" name="txtd6_remark" id="txtd6_remark" style="width: 235px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.6 Miscellaneous</td>
    <td valign="top"><table width="100%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
      <tr>
        <td width="23" valign="top"><input type="checkbox" name="chk_m1" id="chk_m1" runat="server" /></td>
        <td valign="top">M1&nbsp;
          <input type="text" name="txtm1_remark" id="txtm1_remark" style="width: 535px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top"><input type="checkbox" name="chk_m2" id="chk_m2" runat="server" />
          <label for="checkbox74"></label></td>
        <td valign="top">M2&nbsp;
          <input type="text" name="txtm2_remark" id="txtm2_remark" style="width: 535px" runat="server" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top">11.7 Floor stock</td>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23" ><asp:RadioButton ID="txtfloor1" GroupName="floor" runat="server" />
          </td>
        <td width="215" >Yes</td>
        <td width="23" >
            <asp:RadioButton ID="txtfloor2" runat="server" GroupName="floor" />
          </td>
        <td >No</td>
      </tr>
    </table>&nbsp;</td>
  </tr>
  <tr>
    <td valign="top">&nbsp;</td>
    <td valign="top">
       
      </td>
  </tr>
  </table>

