<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_event.aspx.vb" Inherits="ssip_popup_event" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div
    <asp:panel ID="panel_event" runat="server" Enabled="false">
    <table>
     <tr>
        <td valign="top" bgcolor="#eef1f3"><strong>รายละเอียดการจองห้องประชุม</strong></td>
        <td>
            <asp:Label ID="lblRoom" runat="server" Text="-" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
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
        <td valign="top" bgcolor="#eef1f3"><br />
          <strong>เลขที่ข้อเสนอแนะ SSIP No.</strong></td>
        <td><label>
          <input type="text" name="txtssip_no" id="txtssip_no" style="width: 135px; background: #EEEEEE;" runat="server" />
          <input type="submit" name="button10" id="button10" value="..." />
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
        <td colspan="2" valign="top">
   </td>
      </tr>
    </table>
    </asp:panel>
    &nbsp;<div align="right">
     <input id="cmdDelete" type="button" value="Delete" onclick="parent.$.akModalRemove();" />
          &nbsp;<input id="cmdClose" type="button" value="Close" onclick="parent.$.akModalRemove();" /></div>
    </div></br>
    </form>
</body>
</html>
