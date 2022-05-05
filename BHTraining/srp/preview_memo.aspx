<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_memo.aspx.vb" Inherits="srp_preview_memo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ใบสื่อสารภายใน / Internal Communication</title>
    <style type="text/css">
body {
	font-family: Tahoma, Arial, Microsoft Sans Serif;
	font-size: 13px;
	line-height: 1.5em;
}
.otsmemo, .otsmemo td { border-collapse: collapse; border: solid 1px #000;}
</style>
</head>
<body>
<form id="form1" runat ="server">
<div style="width: 800px;">
  <table class="otsmemo" width="100%" cellspacing="0" cellpadding="8">
    <tr>
      <td align="center" style="font-size: 18px; line-height: 1.5em;;"><strong>ใบสื่อสารภายใน <br />
 Internal  Communication</strong></td>
    </tr>
  </table>
  <br />
  <table class="otsmemo" width="100%" cellspacing="0" cellpadding="8">
    <tr>
      <td width="50%" valign="top"><strong>To</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Khun 
          <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
          <br />
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDeptName" runat="server" Text="Label"></asp:Label>
          <br /></td>
      <td colspan="2" valign="top"><strong>From</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ผู้จัดการฝ่ายบริหารพนักงานสัมพันธ์และสวัสดิการ<br />
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Department Manager, Employee Relations & Welfare Benefits
<br /></td>
    </tr>
    <tr>
      <td valign="top"><strong>Subject</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ส่งมอบบัตรรางวัลทำดี-ทันใด 
          ไตรมาสที่
          <asp:Label ID="lblQuarter_th" runat="server" Text="lblQuarter_th"></asp:Label>
          <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;On the spot Reward Cards for Q<asp:Label ID="lblQuarter_en" runat="server" Text=""></asp:Label>
          <br /></td>
      <td width="25%" valign="top"><strong>Ref.</strong><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRD/ERM/WS/<asp:Label ID="lblRef" runat="server" Text="" /></td>
      <td width="25%" valign="top"><strong>Date</strong>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
              ID="lblDateTH" runat="server" Text=""></asp:Label>
          <br />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
              ID="lblDateEN" runat="server" Text=""></asp:Label>
          </td>
    </tr>
    <tr>
      <td colspan="3" valign="top" style="padding: 15px 30px;"><br />        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;คณะกรรมการโครงการเชิดชูเกียรติพนักงาน ขอส่งมอบบัตรรางวัลทำดี-ทันใดประจำไตรมาสที่ <asp:Label ID="lblQuarter_th1" runat="server" Text="lblQuarter_th1" />  ปี <asp:Label ID="lblYearThai" runat="server" Text="lblYearThai" />  ให้แก่ท่านเพื่อส่งเสริมการ ปฏิบัติงานของพนักงานอย่างเต็มความสามารถในการให้บริการ ทั้งนี้  ท่านสามารถมอบบัตรรางวัลแก่ทั้งพนักงานในความรับผิดชอบของท่าน  และพนักงานที่อยู่ นอกเหนือความรับผิดชอบของท่านเพื่อเชิดชูเกียรติ  และให้รางวัลแก่พนักงานโดยทันที ที่พนักงานปฏิบัติตนตามหลักเกณฑ์ ของ ค่านิยมหลัก Core Values คณะกรรมการฯ  ใคร่ขอขอบพระคุณทุกท่านที่ให้การสนับสนุนโครงการด้วยดีตลอดมา<br />
      <br />
      <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td width="30%" style="border: none;">&nbsp;</td>
          <td style="border: none;" align="center">พจนีย์  บุญประสิทธิ์
            <br />ผู้จัดการฝ่ายบริหารพนักงานสัมพันธ์และสวัสดิการ</td>
        </tr>
      </table>      <br />
      ผู้ประสานงาน : โครงการเชิดชูเกียรติพนักงาน โทร.73008,74040 และ 72029 <br />
      รายละเอียดหลักเกณฑ์การคำนวณจำนวนบัตรที่ได้รับประจำไตรมาสนี้ <br /><br />
      <table class="otsmemo" width="100%" cellspacing="0" cellpadding="3">
        <tr>
          <td width="21%" align="center" valign="top">จำนวนพนักงาน<br />
            ในความดูแล</td>
          <td width="32%" align="center" valign="top">โควต้าสำหรับแจกพนักงานในแผนก<br />
จำนวน20% ของพนักงานทั้งหมด  <br />
(คูณ 3 เดือน)</td>
          <td width="27%" align="center" valign="top">โควต้าพิเศษตาม
ตำแหน่งงานสำหรับแจก พนักงานนอกแผนก</td>
          <td width="20%" align="center" valign="top">รวมจำนวนที่ได้รับใน<br />
ไตรมาสที่  <asp:Label ID="lblQuarter_th2" runat="server" Text=""></asp:Label> </td>
        </tr>
        <tr>
          <td align="center" valign="top">
              <asp:Label ID="lblNum1" runat="server" Text=""></asp:Label>
            </td>
          <td align="center" valign="top">
              <asp:Label ID="lblPercent1" runat="server" Text=""></asp:Label>
            </td>
          <td align="center" valign="top">
              <asp:Label ID="lblBonus1" runat="server" Text=""></asp:Label>
            </td>
          <td align="center" valign="top">
              <asp:Label ID="lblQuota1" runat="server" Text=""></asp:Label>
            </td>
        </tr>
      </table>
      <br />
      <p>Staff  Recognition Committee would like to send you on-the-spot reward cards for Q<asp:Label ID="lblQuarter_en1" runat="server" Text="lblQuarter_en1" />/<asp:Label ID="lblYearEN" runat="server" Text="lblYearEN" />  to encourage the BI staff to put their best effort into desired service. <br />
        However, both staff  under your responsibility and the other staff are allowed to recognized and rewarded  by you as soon as possible after the noteworthy actions or behaviors that are  aligned to Core Values. The committee would like to thank you for your kind  support always.</p>
      <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td width="30%" style="border: none;">&nbsp;</td>
          <td style="border: none;" align="center">Poatjanee  Boonprasit<br />
              Department Manager, Employee Relations &amp; Welfare Benefits</td>
        </tr>
      </table>
      <br />
       Staff Recognition Program  Ext. 73008,74040 and 72029
<br />
<br />
<table class="otsmemo" width="100%" cellspacing="0" cellpadding="3">
  <tr>
    <td width="21%" align="center" valign="top">Number of staff under supervision</td>
    <td width="32%" align="center" valign="top">20%  of staff x 3 months
(Q<asp:Label ID="lblQuarter_en2" runat="server" Text="" />) + Special Additional for Management  (by position) </td>
    <td width="27%" align="center" valign="top">Special Addition for Management (by position) for  public</td>
    <td width="20%" align="center" valign="top">Total number in Q<asp:Label ID="lblQuarter_en3" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
    <td align="center" valign="top">
        <asp:Label ID="lblNum2" runat="server" Text=""></asp:Label>
      </td>
    <td align="center" valign="top">
        <asp:Label ID="lblPercent2" runat="server" Text=""></asp:Label>
      </td>
    <td align="center" valign="top">
        <asp:Label ID="lblBonus2" runat="server" Text=""></asp:Label>
      </td>
    <td align="center" valign="top">
        <asp:Label ID="lblQuota2" runat="server" Text=""></asp:Label>
      </td>
  </tr>
</table>
<br /><br /><br /><br />
</td>
    </tr>
    <tr>
      <td colspan="3" valign="top" align="center" style="line-height: 2.35em">โรงพยาบาลบำรุงราษฎร์ มุ่งมั่นที่จะให้บริการทางการแพทย์ที่ดีที่สุด ด้วยความเอื้ออาทร และยึดถือหลักคุณธรรม แก่ผู้ป่วยของเราทุกคน<br />
          Bumrungrad aspires to provide the best care with science, compassion, and integrity for each of our patients</td>
    </tr>
  </table>
  <table width="100%" cellspacing="0" cellpadding="0" style="border-bottom: solid 5px #000;">
    <tr>
      <td valign="top" style="padding: 15px;">BI-00010-I-F-T-1007<br />
      <div style="font-size: 12px; margin-top: 10px;">33 Sukhumvit 3, Bangkok 10110  Thailand Tel: +662 667 1000 Fax: +662 667 2525  <a href="http://www.bumrungrad.com">www.bumrungrad.com</a></div></td>
      <td width="185" align="right" valign="bottom">
      <!--<img src="../images/internal.jpg" width="185" height="54" />-->
      </td>
    </tr>
  </table>
  
</div>
</form>
</body>
</html>
