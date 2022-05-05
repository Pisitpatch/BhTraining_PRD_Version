<%@ Page Title="" Language="VB" MasterPageFile="~/ssip/SSIP_MasterPage.master" AutoEventWireup="false" CodeFile="ssip_reserve_room.aspx.vb" Inherits="ssip_ssip_reserve_room" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel='stylesheet' type='text/css' href='../js/fullcalendar/fullcalendar/fullcalendar.css' />
<link rel='stylesheet' type='text/css' href='../js/fullcalendar/fullcalendar/fullcalendar.print.css' media='print' />

<script type='text/javascript' src='../js/fullcalendar/jquery/jquery-ui-1.8.11.custom.min.js'></script>
<script type='text/javascript' src='../js/fullcalendar/fullcalendar/fullcalendar.min.js'></script>
<script type='text/javascript'>
  $(document).ready(function () {
     checkSession('<%response.write(session("bh_username").toString) %>' , ''); // Check session every 1 sec.
    });

    $(document).ready(function () {

        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();

        $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            eventClick: function(calEvent, jsEvent, view) {

             var id = calEvent.id ;
             $.showAkModal('popup_event.aspx?id=' + id , 'Reservation Room Information', 800, 300);
            /*
            var result = confirm("Are you sure you want to remove " +  calEvent.title  + " event ?");
            if (!result){
                return false;
            }

            var id = calEvent.id ;
                $.ajax({
   			        type: "POST",
   			        url: "../ajax_delete_event.ashx?id=" + id,
			        
			        success: function(data){
				        if (data != ""){
					        alert(data);
				        }else{
                        //alert(11)
					      var url = window.location.href;
                          window.location.href = url;
			            }
			        },
  			        error: function(e){alert("error :: " +e);}
		        });		
                */
            },
            editable: false,
         /*   defaultView: 'agendaDay',*/
            events: [
				/*
				{
				    title: 'Long Event',
				    start: new Date(y, m, d - 5),
				    end: new Date(y, m, d - 2)
				},
			
				{
				    title: 'Meeting',
				    start: new Date(y, m, d, 10, 30),
				    allDay: false
				},
				{
				    title: 'Lunch',
				    start: new Date(y, m, d, 12, 0),
				    end: new Date(y, m, d, 14, 0),
				    allDay: false
				},
				{
				    title: 'Birthday Party',
				    start: new Date(y, m, d + 1, 19, 0),
				    end: new Date(y, m, d + 1, 22, 30),
				    allDay: false
				}
                */
                <%response.write(room_event) %>
			]
        });

    });

</script>

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

        $('#ctl00_ContentPlaceHolder1_txtreserve_date').calendarsPicker(
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
      <div id="header"><img src="../images/menu_05.png" alt="SSIP" width="32" height="32" align="absmiddle"  />&nbsp;Staff Suggestion and Innovation : Reservation Innovation Lab</div>
  <div id="data">
<div class="tabber" id="mytabber2">
  <div class="tabbertab">
    <h2>Staff Information</h2>
      <table width="100%" cellspacing="1" cellpadding="2">
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
    <table width="100%" cellspacing="0" cellpadding="4">
      <tr>
        <td width="182" valign="top" bgcolor="#eef1f3" ><strong>Room</strong></td>
        <td width="806" bgcolor="#eef1f3"><strong>
          <asp:DropDownList ID="txtroom" runat="server" Width="185px" 
                DataTextField="room_name" DataValueField="room_id" AutoPostBack="True">
            </asp:DropDownList>
         
          &nbsp;</strong></td>
      </tr>
      <tr>
        <td colspan="2" valign="top">
       <div id='calendar'></div>
       </td>
        </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>รายละเอียดการจองห้องประชุม</strong></td>
        <td>
            <strong>
         <asp:DropDownList ID="lblroom" runat="server" Width="185px" 
                DataTextField="room_name" DataValueField="room_id">
            </asp:DropDownList>
         
        </strong>
          </td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>วันที่จอง</strong></td>
        <td>
        <asp:TextBox  ID="txtreserve_date" runat="server" Width="135px" ></asp:TextBox>&nbsp; <strong>Start</strong> 
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
        <td valign="top" bgcolor="#eef1f3"><strong>เลขที่ข้อเสนอแนะ SSIP No.</strong></td>
        <td><label>

            <asp:DropDownList ID="txtssip_no" runat="server" DataTextField="ssip_no" 
                DataValueField="ssip_no">
            </asp:DropDownList>
        </label></td>
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
        <td height="31" valign="top" bgcolor="#eef1f3"><strong>ชื่อเรื่อง (Innovation Subject)</strong></td>
        <td><input type="text" name="txtsubject" id="txtsubject" style="width: 600px" runat="server" /></td>
      </tr>
      <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>รายละเอียด</strong></td>
        <td><textarea name="txtdetail" id="txtdetail" cols="45" rows="2" style="width: 600px" runat="server"></textarea></td>
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
        <td colspan="2" align="right" valign="top">
    <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" />          </td>
      </tr>
    </table>

<div align="right">
    &nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;
</div>
      </div>      
</asp:Content>

