<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_post_training.aspx.vb" Inherits="idp_idp_post_training" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript">

     /* Optional: Temporarily hide the "tabber" class so it does not "flash"
     on the page as plain HTML. After tabber runs, the class is changed
     to "tabberlive" and it will appear. */

     document.write('<style type="text/css">.tabber{display:none;}<\/style>');

     /*==================================================
     Set the tabber options (must do this before including tabber.js)
     ==================================================*/
     var tabberOptions = {

         'cookie': "tabber", /* Name to use for the cookie */

         'onLoad': function (argsObj) {
             var t = argsObj.tabber;
             var i;

             /* Optional: Add the id of the tabber to the cookie name to allow
             for multiple tabber interfaces on the site.  If you have
             multiple tabber interfaces (even on different pages) I suggest
             setting a unique id on each one, to avoid having the cookie set
             the wrong tab.
             */
             if (t.id) {
                 t.cookie = t.id + t.cookie;
             }

             /* If a cookie was previously set, restore the active tab */
             i = parseInt(getCookie(t.cookie));
             if (isNaN(i)) { return; }
             t.tabShow(i);
             // alert('getCookie(' + t.cookie + ') = ' + i);
         },

         'onClick': function (argsObj) {
             var c = argsObj.tabber.cookie;
             var i = argsObj.index;
             // alert('setCookie(' + c + ',' + i + ')');
             setCookie(c, i);
         }
     };

     /*==================================================
     Cookie functions
     ==================================================*/
     function setCookie(name, value, expires, path, domain, secure) {
         document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
     }

     function getCookie(name) {
         var dc = document.cookie;
         var prefix = name + "=";
         var begin = dc.indexOf("; " + prefix);
         if (begin == -1) {
             begin = dc.indexOf(prefix);
             if (begin != 0) return null;
         } else {
             begin += 2;
         }
         var end = document.cookie.indexOf(";", begin);
         if (end == -1) {
             end = dc.length;
         }
         return unescape(dc.substring(begin + prefix.length, end));
     }
     function deleteCookie(name, path, domain) {
         if (getCookie(name)) {
             document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
         }
     }

</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="header"><img src="../images/doc01.gif" width="32" height="32" align="absmiddle" />&nbsp;&nbsp;My External Training Action and Goal</div>
<div id="data">
 <div class="tabber" id="mytabber2">
          <div class="tabbertab">
            <h2>Staff Information</h2>
            <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                <tr>
                  <td valign="top"><span class="theader"><strong>Internal Training No.</strong></span></td>
                  <td><strong><asp:Label ID="lblrequest_NO" runat="server" Text=""></asp:Label>
                    
                      </strong>
                      </td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Employee No.</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblempcode" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Name</strong></td>
                        <td width="230"><asp:Label ID="lblname" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Job Title</strong></td>
                        <td><asp:Label ID="lbljobtitle" runat="server" Text=""></asp:Label> </td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Department</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblDept" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Division</strong></td>
                        <td width="230"><asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                        <td width="80"><strong>Cost Center</strong></td>
                        <td><asp:Label ID="lblCostcenter" runat="server" Text=""></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
              </table>
          </div>
          </div>

<div class="tabber" id="mytabber1">
          <div class="tabbertab">
            <h2>
              <legend>My Action and Goal</legend>
            </h2>
            <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td valign="top" bgcolor="#DBE1E6"><strong>Title/หัวข้อที่ฝึกอบรม</strong></td>
                <td valign="top" bgcolor="#DBE1E6"><strong>สมดุลการทรงตัว:Finding the Right Balance </strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Training type</strong></td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335">Training</td>
                      <td><strong>Date<img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /> 11-Jan-10 13-Jan-10</strong></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td width="150" valign="top"><strong>Facility<br />
                </strong></td>
                <td valign="top"><strong>คณะพยาบาลศาสตร์ มหาวิทยาลัยมหิดล โรงแรมปริ้นซ์พาเลซ</strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>ฉันมีความรู้ความสามารถขั้นไหน</strong></td>
                <td valign="top" bgcolor="#eef1f3"><label>
                  <input type="radio" name="radio" id="radio10" value="radio" />
                  </label>
                  ไม่มี
                  <label>
                    <input type="radio" name="radio" id="radio11" value="radio" />
                    </label>
                  น้อย
                  <label>
                    <input type="radio" name="radio" id="radio12" value="radio" />
                    </label>
                  ปานกลาง
                  <label>
                    <input type="radio" name="radio" id="radio13" value="radio" />
                    </label>
                  สูง</td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>หัวข้อที่ได้เรียนรู้และมีประโยชน์<br />
                </strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top" bgcolor="#eef1f3"><strong>
                  <textarea name="textarea6" id="textarea7" cols="45" rows="3" style="width: 735px"></textarea>
                </strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>ฉันจะเอาความรู้ที่ได้มาปรับใช้อย่างไร</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><table width="98%" cellpadding="3" cellspacing="0" class="tdata">
                    <tr>
                      <td width="30" class="colname">&nbsp;</td>
                      <td width="30" class="colname"><strong>No</strong></td>
                      <td class="colname"><strong>Action</strong></td>
                      <td class="colname"><strong>ดำเนินการร่วมกับ</strong></td>
                      <td width="80" class="colname"><strong>กำหนดเริ่ม</strong></td>
                      <td width="80" class="colname"><strong>กำหนดเสร็จ</strong></td>
                      <td width="80" class="colname"><strong>Status</strong></td>
                    </tr>
                    <tr>
                      <td valign="top"><input type="checkbox" name="checkbox12" id="checkbox12" /></td>
                      <td valign="top"><input name="textfield20" type="text" id="textfield20" style="width: 23px;" value="1" /></td>
                      <td valign="top">ถ่ายทอดความรู้จากการอบรมให้เพื่อนร่วมงาน</td>
                      <td valign="top">(Miss) Jariya    Sunthan</td>
                      <td valign="top">1 Mar 2010</td>
                      <td valign="top">30 Jun 2010</td>
                      <td valign="top">On-going</td>
                    </tr>
                    <tr>
                      <td valign="top"><input type="checkbox" name="checkbox13" id="checkbox13" /></td>
                      <td valign="top"><input name="textfield19" type="text" id="textfield21" style="width: 23px;" value="2" /></td>
                      <td valign="top">ปรับปรุงงานในแผนก</td>
                      <td valign="top">(Miss) Jariya    Sunthan</td>
                      <td valign="top">1 Mar 2010</td>
                      <td valign="top">30 Jun 2010</td>
                      <td valign="top">On-going</td>
                    </tr>
                    <tr>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top"><textarea name="textarea6" id="textarea8" cols="45" rows="1" style="width: 350px"></textarea></td>
                      <td valign="top"><textarea name="textarea6" id="textarea9" cols="45" rows="2" style="width: 350px"></textarea></td>
                      <td valign="top"><input type="text" name="textfield9" id="textfield10" style="width: 30px; background: #0F0;"/>
                          <img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /></td>
                      <td valign="top"><input type="text" name="textfield10" id="textfield7" style="width: 30px; background: #0F0;"/>
                          <img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /></td>
                      <td valign="top"><select name="select6" id="select6" style="width: 80px">
                          <option>- Please Select -</option>
                          <option>Completed</option>
                          <option>On-going</option>
                          <option>Hold</option>
                      </select></td>
                    </tr>
                    <tr>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top"><input type="submit" name="button5" id="button5" value="Add Topic" /></td>
                      <td valign="top"><input type="submit" name="button2" id="button" value="Save Order" /></td>
                      <td valign="top"><input type="submit" name="button6" id="button8" value="Delete" /></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>อุปสรรค</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><table width="98%" cellpadding="3" cellspacing="0" class="tdata">
                    <tr>
                      <td width="30" class="colname">&nbsp;</td>
                      <td width="30" class="colname"><strong>No</strong></td>
                      <td class="colname"><strong>ฉันคิดว่าจะเจออุปสรรคอะไร</strong></td>
                      <td class="colname"><strong>จะทำอย่างไรให้ผ่านอุปสรรคไปได้</strong></td>
                    </tr>
                    <tr>
                      <td valign="top"><input type="checkbox" name="checkbox14" id="checkbox14" /></td>
                      <td valign="top"><input name="textfield3" type="text" id="textfield8" style="width: 23px;" value="1" /></td>
                      <td valign="top">ปรับปรุงงานในแผนก</td>
                      <td valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                      <td valign="top"><input type="checkbox" name="checkbox15" id="checkbox15" /></td>
                      <td valign="top"><input name="textfield7" type="text" id="textfield9" style="width: 23px;" value="2" /></td>
                      <td valign="top">ปรับปรุงงานในแผนก</td>
                      <td valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top"><textarea name="textarea6" id="textarea10" cols="45" rows="2" style="width: 350px"></textarea></td>
                      <td valign="top"><textarea name="textarea6" id="textarea11" cols="45" rows="2" style="width: 350px"></textarea></td>
                    </tr>
                    <tr>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top">&nbsp;</td>
                      <td valign="top"><input type="submit" name="button4" id="button3" value="Add Topic" />
                          <input type="submit" name="button" id="button4" value="Save Order" />
                          <input type="submit" name="button9" id="button10" value="Delete" /></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><strong>ผลลัพท์ที่จะได้รับ/สอดคล้องกับ KPI</strong></td>
              </tr>
              <tr>
                <td colspan="2" valign="top"><label>
                  <input type="checkbox" name="checkbox" id="checkbox" />
                  </label>
                  ผู้เข้ารับการอบรมสามารถยกระดับ Career Ladder จากระดับ
                  <select name="select10" id="select15">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                                    </select>
                  เป็นระดับ
                  <select name="select11" id="select16">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                  </select>
                  <br />
                  <input type="checkbox" name="checkbox2" id="checkbox2" />
                  คะแนน post test เพิ่มขึ้น
                  <input type="text" name="textfield4" id="textfield13" style="width: 100px" />
                  %<br />
                  <input type="checkbox" name="checkbox" id="checkbox" />
                  เพื่อเพิ่มผลลัพท์ของแผนกเป็น KPI หัวข้อ
                  <input type="text" name="textfield5" id="textfield14" style="width: 100px" />
                  <input type="text" name="textfield6" id="textfield16" style="width: 100px" />
                  %<br />
                  <input type="checkbox" name="checkbox" id="checkbox2" />
                  อื่นๆ
                  <input type="text" name="textfield3" id="textfield20" style="width: 100px" /></td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="top"><div align="right"></div></td>
              </tr>
              <tr>
                <td colspan="2" align="center" valign="top">&nbsp;</td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Training Information</strong></legend>
             (view only)</h2>
            <table width="100%" cellspacing="1" cellpadding="2">
              <tr>
                <td valign="top" bgcolor="#DBE1E6"><strong>Title/หัวข้อที่ต้องการฝึกอบรม</strong></td>
                <td valign="top" bgcolor="#DBE1E6"><input type="text" name="textfield26" id="textfield31" style="width: 735px" /></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>การเข้าร่วม</strong></td>
                <td valign="top" bgcolor="#eef1f3"><select name="select5" id="select5" style="width: 320px">
                    <option>------ Please Select ------</option>
                    <option>ผู้เข้าอบรม</option>
                    <option>วิทยากร</option>
                </select></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>แรงจูงใจในการเข้าอบรมหัวข้อน</strong>ี้</td>
                <td valign="top" bgcolor="#eef1f3"><p>
                    <label>
                    <input type="checkbox" name="checkbox3" id="checkbox3" />
                    </label>
                  หัวข้อนี้เป็นหัวข้อที่ระบุอยู่ในแผนพัฒนาตนเองของฉัน <strong>IDP No. </strong>
                  <input type="text" name="textfield25" id="textfield30" style="width: 100px" />
                  <input type="submit" name="button8" id="button7" value=".." />
                  <br />
                  <input type="checkbox" name="checkbox4" id="checkbox4" />
                  เพื่อให้ตนเองสามารถปรับระดับ Career Ladder
                  <select name="select5" id="select11">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                  </select>
                  เป็นระดับ
                  <select name="select5" id="select12">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                  </select>
                  <br />
                  <input type="checkbox" name="checkbox5" id="checkbox5" />
                  การพัฒนาในหัวข้อนี้ ฉันได้รับคำแนะนำจาก หัวหน้างานของฉัน<br />
                  <input type="checkbox" name="checkbox6" id="checkbox6" />
                  การพัฒนาในหัวข้อนี้ ฉันได้จาการประเมินความสามารถของตัวฉันเอง<br />
                  <input type="checkbox" name="checkbox7" id="checkbox7" />
                  ฉันต้องการเตรียมพร้อมตัวเองเพื่ออนาคตในหน้าที่การงานของตัวเองด้วยหัวข้อนี้<br />
                  <input type="checkbox" name="checkbox8" id="checkbox8" />
                  องค์กรของฉันเล็งเห็นว่าหัวข้อนี้เป็นเรื่องที่สำคัญ<br />
                  <input type="checkbox" name="checkbox9" id="checkbox9" />
                  อื่นๆ <span style="font-weight: bold">
                    <input type="text" name="textfield" id="textfield" style="width: 350px" />
                  </span></p></td>
              </tr>
              <tr>
                <td valign="top"><strong>Course Outline</strong></td>
                <td valign="top"><strong>
                  <textarea name="textarea3" id="textarea3" cols="45" rows="3" style="width: 735px"></textarea>
                  <br />
                </strong></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3"><strong>Course Outline File Attachment</strong></td>
                <td valign="top"><input type="file" name="fileField" id="fileField" />
                    <input type="submit" name="button8" id="button7" value="Add" /></td>
              </tr>
              <tr>
                <td valign="top"><strong>Expected outcomes from this training</strong></td>
                <td valign="top"><strong>
                  <textarea name="textarea4" id="textarea4" cols="45" rows="3" style="width: 735px"></textarea>
                </strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Training type</strong></td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="335"><select name="select5" id="select13" style="width: 320px">
                          <option>------ Please Select ------</option>
                          <option>Meeting</option>
                          <option>Training</option>
                          <option>Seminar</option>
                          <option>Workshop</option>
                      </select></td>
                      <td width="80">&nbsp;</td>
                      <td>&nbsp;</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td width="150" valign="top"><strong>Facility<br />
                </strong></td>
                <td valign="top"><input type="text" name="textfield18" id="textfield19" style="width: 735px" />
                  &nbsp;</td>
              </tr>
              <tr>
                <td valign="top"><strong>institution</strong></td>
                <td valign="top"><input type="text" name="textfield" id="textfield" style="width: 735px" /></td>
              </tr>
              <tr>
                <td valign="top"><strong>Place</strong></td>
                <td valign="top"><select name="select5" id="select14" style="width: 320px">
                    <option>------ Please Select ------</option>
                    <option>Over Sea</option>
                    <option>Outside</option>
                    <option>Inhouse</option>
                </select></td>
              </tr>
              <tr>
                <td valign="top"><strong>Date</strong></td>
                <td valign="top"><table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
                    <tr>
                      <td width="300"><input type="text" name="textfield23" id="textfield26" style="width: 100px; background: #0F0;"/>
                          <img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /> to
                        <input type="text" name="textfield23" id="textfield28" style="width: 100px; background: #0F0;"/>
                          <img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /></td>
                      <td width="40">Time</td>
                      <td><select name="select9" id="select17">
                          <option>hh</option>
                          <option>00</option>
                          <option>01</option>
                          <option>02</option>
                          <option>03</option>
                          <option>04</option>
                          <option>05</option>
                          <option>06</option>
                          <option>07</option>
                          <option>08</option>
                          <option>09</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                        </select>
                        :
                        <select name="select9" id="select18">
                          <option>mm</option>
                          <option>00</option>
                          <option>01</option>
                          <option>02</option>
                          <option>03</option>
                          <option>04</option>
                          <option>05</option>
                          <option>06</option>
                          <option>07</option>
                          <option>08</option>
                          <option>09</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                          <option>24</option>
                          <option>25</option>
                          <option>26</option>
                          <option>27</option>
                          <option>28</option>
                          <option>29</option>
                          <option>30</option>
                          <option>31</option>
                          <option>32</option>
                          <option>33</option>
                          <option>34</option>
                          <option>35</option>
                          <option>36</option>
                          <option>37</option>
                          <option>38</option>
                          <option>39</option>
                          <option>40</option>
                          <option>41</option>
                          <option>42</option>
                          <option>43</option>
                          <option>44</option>
                          <option>45</option>
                          <option>46</option>
                          <option>47</option>
                          <option>48</option>
                          <option>49</option>
                          <option>50</option>
                          <option>51</option>
                          <option>52</option>
                          <option>53</option>
                          <option>54</option>
                          <option>55</option>
                          <option>56</option>
                          <option>57</option>
                          <option>58</option>
                          <option>59</option>
                        </select>
                        to
                        <select name="select9" id="select19">
                          <option>hh</option>
                          <option>00</option>
                          <option>01</option>
                          <option>02</option>
                          <option>03</option>
                          <option>04</option>
                          <option>05</option>
                          <option>06</option>
                          <option>07</option>
                          <option>08</option>
                          <option>09</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                        </select>
                        :
                        <select name="select9" id="select20">
                          <option>mm</option>
                          <option>00</option>
                          <option>01</option>
                          <option>02</option>
                          <option>03</option>
                          <option>04</option>
                          <option>05</option>
                          <option>06</option>
                          <option>07</option>
                          <option>08</option>
                          <option>09</option>
                          <option>10</option>
                          <option>11</option>
                          <option>12</option>
                          <option>13</option>
                          <option>14</option>
                          <option>15</option>
                          <option>16</option>
                          <option>17</option>
                          <option>18</option>
                          <option>19</option>
                          <option>20</option>
                          <option>21</option>
                          <option>22</option>
                          <option>23</option>
                          <option>24</option>
                          <option>25</option>
                          <option>26</option>
                          <option>27</option>
                          <option>28</option>
                          <option>29</option>
                          <option>30</option>
                          <option>31</option>
                          <option>32</option>
                          <option>33</option>
                          <option>34</option>
                          <option>35</option>
                          <option>36</option>
                          <option>37</option>
                          <option>38</option>
                          <option>39</option>
                          <option>40</option>
                          <option>41</option>
                          <option>42</option>
                          <option>43</option>
                          <option>44</option>
                          <option>45</option>
                          <option>46</option>
                          <option>47</option>
                          <option>48</option>
                          <option>49</option>
                          <option>50</option>
                          <option>51</option>
                          <option>52</option>
                          <option>53</option>
                          <option>54</option>
                          <option>55</option>
                          <option>56</option>
                          <option>57</option>
                          <option>58</option>
                          <option>59</option>
                        </select></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top"><strong>Training Hour</strong></td>
                <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="150"><input type="text" name="textfield24" id="textfield29" style="width: 100px" />
                        &nbsp;&nbsp;hrs.</td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="2" valign="top">&nbsp;</td>
              </tr>
            </table>
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Expense</strong></legend>
             (view only)</h2>
            <table width="98%" cellpadding="3" cellspacing="0" class="tdata">
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>คำใช้จ่าย</strong></td>
                <td class="colname"><strong>จำนวนเงิน <span style="font-weight: bold">(Baht)</span></strong></td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox10" id="checkbox10" /></td>
                <td valign="top"><input name="textfield14" type="text" id="textfield15" style="width: 23px;" value="1" /></td>
                <td valign="top">Registration Fees</td>
                <td valign="top">2000</td>
              </tr>
              <tr>
                <td valign="top"><input type="checkbox" name="checkbox11" id="checkbox11" /></td>
                <td valign="top"><input name="textfield14" type="text" id="textfield17" style="width: 23px;" value="2" /></td>
                <td valign="top">Travel Expense</td>
                <td valign="top">2000</td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><select name="select" id="select" style="width: 320px">
                  <option>Registration Fees</option>
                  <option>Travel Expense</option>
                  <option>Hotel Expense</option>
                  <option>Meal Expense</option>
                  <option>Other</option>
                </select></td>
                <td valign="top"><textarea name="textarea" id="textarea2" cols="45" rows="1" style="width: 350px"></textarea></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top"><input type="submit" name="button3" id="button2" value="Add Topic" />
                    <input type="submit" name="button3" id="button6" value="Save Order" />
                    <input type="submit" name="button7" id="button9" value="Delete" /></td>
              </tr>
              <tr>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
                <td valign="top" bgcolor="#eef1f3"><strong>Total Expense</strong></td>
                <td valign="top" bgcolor="#eef1f3"><input type="text" name="textfield16" id="textfield24" style="width: 235px" />
                  &nbsp;&nbsp;<strong>Baht</strong></td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab">
            <h2>
              <legend><strong>Part of Training Department</strong></legend>
             (view only)</h2>
            <table width="100%" cellspacing="1" cellpadding="2" style="margin-top: -3px; margin-left: -3px;">
              <tr>
                <td width="25"><input type="radio" name="radio7" id="radio7" value="radio7" />
                    <label for="radio7"></label></td>
                <td>Reservation made, Employee Training and Development Unit will provide cash for registration</td>
              </tr>
              <tr>
                <td height="26"><input type="radio" name="radio7" id="radio8" value="radio7" /></td>
                <td>Employee Training and Developement Department Unit will make reservation and provide cash for registration</td>
              </tr>
              <tr>
                <td><input type="radio" name="radio7" id="radio9" value="radio7" /></td>
                <td>Reservation made and prepaid by Employee Training and Development Department Unit on&nbsp;&nbsp;
                    <input type="text" name="textfield8" id="textfield11" style="width: 100px; background: #0F0;"/>
                    <img src="../images/calendar.gif" alt="1" width="22" height="17" align="absmiddle" /></td>
              </tr>
            </table>
          </div>
        </div>
          </div>
</asp:Content>

