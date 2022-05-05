<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AnesControl.ascx.vb" Inherits="incident.incident_AnesControl" %>
          <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
          <tr>
            <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
              <tr>
                <td width="200" valign="top">1. Check all occurrences</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top">
                      <input type="checkbox" name="chk_topic11" id="chk_topic11" runat="server" />
                  
                      </td>
                    <td valign="top">Mortality within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic12" id="chk_topic12" runat="server" />
                      </td>
                    <td valign="top">Cardiopulmonary resuscitation</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic13" id="chk_topic13" runat="server" />
                      </td>
                    <td valign="top">Unplanned transfer to critical care</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic14" id="chk_topic14" runat="server" />
                      </td>
                    <td valign="top">&gt; 2 unplanned hours spent in PACU</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic15" id="chk_topic15" runat="server" /></td>
                    <td valign="top">Patient dissatisfaction</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic16" id="chk_topic16" runat="server" /></td>
                    <td valign="top">Operative procedure cancelled</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">2. Problems related to patient assessment</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic21" id="chk_topic21" runat="server" />
                    </label>
                      </td>
                    <td valign="top">Failure to recognize patient disease</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic22" id="chk_topic22" runat="server" />
                      </td>
                    <td valign="top">Lack of medical optimization</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic23" id="chk_topic23" runat="server" />
                     </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic2_other" id="topic2_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">3. Problems related to anesthesia equipment</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic31" id="chk_topic31" runat="server" />
                    </label>
                    </td>
                    <td valign="top">Failure to check anesthesia equipment</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic32" id="chk_topic32" runat="server" />
                      </td>
                    <td valign="top">Failure to adhere to monitoring standards</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic33" id="chk_topic33" runat="server" />
                      </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic3_other" id="topic3_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">4. Problems related to anesthetic medication</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic41" id="chk_topic41" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Medication dosing error</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic42" id="chk_topic42" runat="server" />
                     </td>
                    <td valign="top">Inappropriate use</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic43" id="chk_topic43" runat="server" /></td>
                    <td valign="top">Adverse drug reaction (not anaphylaxis)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic44" id="chk_topic44" runat="server" /></td>
                    <td valign="top">Anaphylaxis</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic45" id="chk_topic45" runat="server" /></td>
                    <td valign="top">Wrong medication given</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic46" id="chk_topic46" runat="server" /></td>
                    <td valign="top">Incorrect controlled substance count</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic47" id="chk_topic47" runat="server" />
                      <label for="checkbox12"></label></td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic4_other" id="topic4_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">5. Problems related to airway management</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic51" id="chk_topic51" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Dental injury</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic52" id="chk_topic52" runat="server" />
                      </td>
                    <td valign="top">Undetected esophageal intubation</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic53" id="chk_topic53" runat="server" /></td>
                    <td valign="top">Failed tracheal intubation/ventilation</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic54" id="chk_topic54" runat="server" /></td>
                    <td valign="top">Severe epistaxis</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic55" id="chk_topic55" runat="server" /></td>
                    <td valign="top">Laryngospasm</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic56" id="chk_topic56" runat="server" />
                      </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic5_other" id="topic5_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">6. Problems related to the cardiovascular system</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic61" id="chk_topic61" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Cardiac arrest within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic62" id="chk_topic62" runat="server" />
                     </td>
                    <td valign="top">Myocardial ischemia</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic63" id="chk_topic63" runat="server" /></td>
                    <td valign="top">Dysrhythmias requiring treatment</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic64" id="chk_topic64" runat="server" /></td>
                    <td valign="top">Myocardial infarction within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic65" id="chk_topic65" runat="server" /></td>
                    <td valign="top">Congestive heart failure</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic66" id="chk_topic66" runat="server" /></td>
                    <td valign="top">Hypertension/Hypotension with adverse outcome</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic67" id="chk_topic67" runat="server" />
                      </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic6_other" id="topic6_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">7. Problems related to the respiratory system</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic71" id="chk_topic71" runat="server" />
                    </label>
                    </td>
                    <td valign="top">Noncardiogenic pulmonary edema</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic72" id="chk_topic72" runat="server" />
                      </td>
                    <td valign="top">Respiratory failure within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic73" id="chk_topic73" runat="server" /></td>
                    <td valign="top">Aspiration pneumonitis</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic74" id="chk_topic74" runat="server" /></td>
                    <td valign="top">Pulmonary embolism</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic75" id="chk_topic75" runat="server" /></td>
                    <td valign="top">Pneumo/hemothorax</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic76" id="chk_topic76" runat="server" /></td>
                    <td valign="top">Reintubation within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic77" id="chk_topic77" runat="server" /></td>
                    <td valign="top">Bronchospasm</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic78" id="chk_topic78" runat="server" />
                     </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic7_other" id="topic7_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">8. Problems related to the nervous system</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic81" id="chk_topic81" runat="server" />
                    </label>
                      </td>
                    <td valign="top">Delayed emergence (&gt; 60 minutes)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic82" id="chk_topic82" runat="server" />
                      </td>
                    <td valign="top">Awareness under general anesthesia</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic83" id="chk_topic83" runat="server" /></td>
                    <td valign="top">Central nervous system injury</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic84" id="chk_topic84" runat="server" /></td>
                    <td valign="top">Post-dural puncture headache</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic85" id="chk_topic85" runat="server" /></td>
                    <td valign="top">Inadvertent dural puncture</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic86" id="chk_topic86" runat="server" /></td>
                    <td valign="top">Peripheral nervous system injury</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic87" id="chk_topic87" runat="server" /></td>
                    <td valign="top">Failed regional anesthesia</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic88" id="chk_topic88" runat="server" />
                      </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic8_other" id="topic8_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">9. Problems related to the renal system</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic91" id="chk_topic91" runat="server" />
                    </label>
                      </td>
                    <td valign="top">Unexpected renal insufficiency within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic92" id="chk_topic92" runat="server" />
                      </td>
                    <td valign="top">Unexpected renal failure within 48 hours</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic93" id="chk_topic93" runat="server" />
                     </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic9_other" id="topic9_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">10. Problems related to fluid/blood products</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic101" id="chk_topic101" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Fluid management problem</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic102" id="chk_topic102" runat="server" />
                      </td>
                    <td valign="top">Transfusion reaction/error</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic103" id="chk_topic103" runat="server" />
                     </td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic10_other" id="topic10_other" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">11. Complete the following incident analysis by checking the appropriate responses</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="50">ASA </td>
                    <td width="23"><input type="checkbox" name="chk_topic111" id="chk_topic111" runat="server" /></td>
                    <td width="50">I</td>
                    <td width="23"><input type="checkbox" name="chk_topic112" id="chk_topic112" runat="server" /></td>
                    <td width="50">II</td>
                    <td width="23"><input type="checkbox" name="chk_topic113" id="chk_topic113" runat="server" /></td>
                    <td width="50">III</td>
                    <td width="23"><input type="checkbox" name="chk_topic114" id="chk_topic114" runat="server" /></td>
                    <td width="50">IV</td>
                    <td width="23"><input type="checkbox" name="chk_topic115" id="chk_topic115" runat="server" /></td>
                    <td width="50">V</td>
                    <td width="23"><input type="checkbox" name="chk_topic116" id="chk_topic116" runat="server" /></td>
                    <td width="50">VI</td>
                    <td width="23"><input type="checkbox" name="chk_topic117" id="chk_topic117" runat="server" /></td>
                    <td>E</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">12. Outcome Category</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic121" id="chk_topic121" runat="server" />
                    </label>
                     </td>
                    <td valign="top">No change in hospital course (1)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic122" id="chk_topic122" runat="server" />
                      <label for="checkbox32"></label></td>
                    <td valign="top">Increased care/risk without function deficit (2)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic123" id="chk_topic123" runat="server" /></td>
                    <td valign="top">Increased care with reversible function deficit (3)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic124" id="chk_topic124" runat="server" /></td>
                    <td valign="top">Increased care with irreversible function deficit (4)</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic125" id="chk_topic125" runat="server" />
                     </td>
                    <td valign="top">Death (5)</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">13. Analyze the events that contributed to the outcome and check appropriate categories</td>
              </tr>
              <tr>
                <td valign="top">Human error</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic131" id="chk_topic131" runat="server" />
                    </label>
                      </td>
                    <td valign="top">Operator error</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic132" id="chk_topic132" runat="server" />
                      <label for="checkbox35"></label></td>
                    <td valign="top">Improper technique</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic133" id="chk_topic133" runat="server" /></td>
                    <td valign="top">Communication error</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic134" id="chk_topic134" runat="server" /></td>
                    <td valign="top">Inadequate data source</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic135" id="chk_topic135" runat="server" />
                      <label for="checkbox36"></label></td>
                    <td valign="top">Data disregarded</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic136" id="chk_topic136" runat="server" /></td>
                    <td valign="top">Inadequate knowledge</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic137" id="chk_topic137" runat="server" /></td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic13_other1" id="topic13_other1" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">System error</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic138" id="chk_topic138" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Equipment failure</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic139" id="chk_topic139" runat="server" />
                      <label for="checkbox38"></label></td>
                    <td valign="top">Technical/Accidental</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic1310" id="chk_topic1310" runat="server" /></td>
                    <td valign="top">Communication failure</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic1311" id="chk_topic1311" runat="server" /></td>
                    <td valign="top">Limitation of therapeutic standards</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic1312" id="chk_topic1312" runat="server" />
                      <label for="checkbox39"></label></td>
                    <td valign="top">Limitation of diagnostic standards</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic1313" id="chk_topic1313" runat="server" /></td>
                    <td valign="top">Limitation of resources available</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic1314" id="chk_topic1314" runat="server" /></td>
                    <td valign="top">Other&nbsp;&nbsp;
                      <input type="text" name="topic13_other2" id="topic13_other2" style="width: 450px" runat="server" /></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">14. To what extent did the anesthesiologists / anesthetisis contribute to the occurrence?</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><label>
                      <input type="checkbox" name="chk_topic141" id="chk_topic141" runat="server" />
                    </label>
                     </td>
                    <td valign="top">Not at all</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic142" id="chk_topic142" runat="server" />
                      <label for="checkbox41"></label></td>
                    <td valign="top">Minor</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic143" id="chk_topic143" runat="server" /></td>
                    <td valign="top">Moderate</td>
                  </tr>
                  <tr>
                    <td valign="top"><input type="checkbox" name="chk_topic144" id="chk_topic144" runat="server" /></td>
                    <td valign="top">Major</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">15. Was corrective action timely and appropriate?</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr><td width="23"><input type="checkbox" name="chk_topic151" id="chk_topic151" runat="server" /></td>
                   
                    <td width="150" valign="top">Yes</td>
                    <td width="23" valign="top"><input type="checkbox" name="chk_topic152" id="chk_topic152" runat="server" /></td>
                    <td width="150" valign="top">No</td>
                    <td width="23" valign="top"><input type="checkbox" name="chk_topic153" id="chk_topic153" runat="server" /></td>
                    <td valign="top">NA</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top">16. Does documentation support the analysis?</td>
              </tr>
              <tr>
                <td valign="top"><table width="90%" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                  <tr>
                    <td width="23" valign="top"><input type="checkbox" name="chk_topic161" id="chk_topic161" runat="server" />
                      <label for="checkbox139"></label>
                      <label for="checkbox44"></label>
                      <label for="radio35"></label></td>
                    <td width="150" valign="top">Yes</td>
                    <td width="23" valign="top"><input type="checkbox" name="chk_topic162" id="chk_topic162" runat="server" /></td>
                    <td width="150" valign="top">No</td>
                    <td width="23" valign="top"><input type="checkbox" name="chk_topic163" id="chk_topic163" runat="server" /></td>
                    <td valign="top">NA</td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="2"><hr style="height: 2px; width: 98%; text-align: left;" /></td>
              </tr>
              <tr>
                <td valign="top">Clinical Summary</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtsum" id="comment1" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Was the outcome preventable?</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtprevent" id="comment2" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top">What might have been done to change the outcome?</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtoutcome1" id="comment3" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Is there clinical evidence to support individual practice change that may have altered outcome?</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtoutcome2" id="comment4" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Are there any system base changes that may prevent similar future outcomes?</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtoutcome3" id="comment5" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
              <tr>
                <td valign="top">Lesson learned from this case?</td>
              </tr>
              <tr>
                <td valign="top"><textarea name="txtlesson" id="comment6" cols="45" rows="3" style="width: 850px" runat="server"></textarea></td>
              </tr>
            </table>
       </td></tr></table>     
