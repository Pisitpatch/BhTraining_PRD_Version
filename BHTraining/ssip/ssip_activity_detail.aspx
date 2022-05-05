<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="ssip_activity_detail.aspx.vb" Inherits="ssip_ssip_activity_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />

 <style type="text/css">
@import "../js/datepicker2/redmond.calendars.picker.css";
 /*Or use these for a ThemeRoller theme
 
@import "themes16/southstreet/ui.all.css";
@import "js/datepicker/ui-southstreet.datepick.css";
*/

     </style>

<script type="text/javascript" src="../js/datepicker2/jquery.calendars.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.plus.js"></script>


<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.picker.ext.js"></script>
<script type="text/javascript" src="../js/datepicker2/jquery.calendars.thai.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        var calendar = $.calendars.instance();

        $('#ctl00_ContentPlaceHolder1_txtaction_date').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			}

			);
        $('#ctl00_ContentPlaceHolder1_txtfind_date').calendarsPicker(
			{

			    showTrigger: '#calImg',
			    dateFormat: 'dd/mm/yyyy'
			});
    });
    

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="display: none;">
	<img id="calImg" src="../images/calendar.gif" alt="Popup" class="trigger" style="margin-left:5px; cursor:pointer"  />
</div>
 <div id="header">
        <div id="header2"><img src="../images/doc01.gif" alt="1" width="32" height="32" />&nbsp;Staff Suggestion and Innovation Activity Record
          <div align="right">&nbsp;&nbsp;&nbsp;
  &nbsp;&nbsp;
          </div>
        </div>
        </div>
      <div id="data">
      <div class="tabber" id="mytabber2">
  <div class="tabbertab">
    <h2>Staff Information</h2>
      <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
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
                        <td width="60"><strong>Job Type</strong></td>
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
    <h2>Innovation Activity</h2>
   <table width="100%" cellspacing="0" cellpadding="3">
 

       
      <tr>
        <td valign="top" bgcolor="#eef1f3" width="182"><strong>รายละเอียดกิจกรรม</strong></td>
        <td>
            &nbsp;</td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>วันเวลาที่เข้าทำกิจกรรม</strong></td>
        <td>
        <asp:TextBox  ID="txtaction_date" runat="server" Width="135px" ></asp:TextBox>&nbsp; <strong>Start</strong> 
           <asp:DropDownList ID="txthour1" runat="server">
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
              <asp:DropDownList ID="txtmin1" runat="server">
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
<strong>End</strong>
  <asp:DropDownList ID="txthour2" runat="server">
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
              <asp:DropDownList ID="txtmin2" runat="server">
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
        <td valign="top" bgcolor="#eef1f3"><strong>ห้องประชุม</strong></td>
        <td>
            <strong>
         <asp:DropDownList ID="txtroom" runat="server" Width="185px" 
                DataTextField="room_name" DataValueField="room_id">
            </asp:DropDownList>
         
        </strong>
          </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><br />
          <strong>เลขที่ข้อเสนอแนะ SSIP No.</strong></td>
        <td>
         
            <asp:Label ID="txtssip_no" runat="server" Text=""></asp:Label>
        </td>
      </tr>
    
      <tr>
        <td height="31" valign="top" bgcolor="#eef1f3"><strong>ชื่อเรื่อง (Innovation Subject)</strong></td>
        <td>
          <asp:Label ID="txtsubject" runat="server" Text="-"></asp:Label>
        </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>รายละเอียด</strong></td>
        <td><textarea name="txtdetail" id="txtdetail" cols="45" rows="2" style="width: 600px" runat="server"></textarea></td>
      </tr>
        <tr>
        <td height="31" valign="top" bgcolor="#eef1f3">
            <strong style="color: rgb(51, 51, 51); font-family: 'Segoe UI', Arial, Tahoma; font-size: 13px; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; background-color: rgb(238, 241, 243); ">
            รายชื่อผู้ร่วม Activity</strong></td>
        <td>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td valign="top" width="210">
                        <asp:ListBox ID="txtperson_all" runat="server" DataTextField="user_fullname" 
                            DataValueField="emp_code" SelectionMode="Multiple" Width="200px">
                        </asp:ListBox>
                        &nbsp;</td>
                    <td valign="top" width="60">
                        <asp:Button ID="cmdAddRelatePerson" runat="server" Text=" &gt;&gt;" />
                        <br />
                        <br />
                        <asp:Button ID="cmdRemoveRelatePerson" runat="server" Text=" &lt;&lt;" />
                    </td>
                    <td valign="top" width="492">
                        <asp:ListBox ID="txtperson_select" runat="server" DataTextField="user_fullname" 
                            DataValueField="emp_code" Width="200px"></asp:ListBox>
                    </td>
                </tr>
            </table>
            </td>
      </tr>
        <tr>
        <td height="31" valign="top" bgcolor="#eef1f3"><strong>ประเภทกิจกรรม</strong></td>
        <td><strong>
            <asp:DropDownList ID="txtadd_activity" runat="server" Width="185px">
            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
            <asp:ListItem Value="1">Presentation</asp:ListItem>
            <asp:ListItem Value="2">Meeting</asp:ListItem>
            </asp:DropDownList>
         
        </strong></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>อุปกรณ์ที่ใช้</strong></td>
        <td><strong>
            <asp:DropDownList ID="txttool" runat="server" Width="185px">
            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
             <asp:ListItem Value="1">Projector</asp:ListItem>
              <asp:ListItem Value="2">White Board</asp:ListItem>
            </asp:DropDownList>
         
          &nbsp;</strong></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong> หนังสือที่ใช้</strong></td>
        <td><strong>
           <asp:DropDownList ID="txtreference" runat="server" Width="185px">
            <asp:ListItem Value="">-- Please Select --</asp:ListItem>
             <asp:ListItem Value="1">Book1</asp:ListItem>
              <asp:ListItem Value="2">Book2</asp:ListItem>
            </asp:DropDownList>&nbsp;
        </strong></td>
      </tr>
      <tr>
        <td colspan="2" valign="top">
    <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" Font-Bold="true" />
          &nbsp;<asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="100px" />
          </td>
      </tr>
    </table>
  </div>
  </div>
    </div>
</asp:Content>

