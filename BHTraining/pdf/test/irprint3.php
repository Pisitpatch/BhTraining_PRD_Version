<?php
	require_once("setPDF.php");
	$pdf->setPrintHeader(false);
	$pdf->AddPage();
	$htmlcontent= <<<EOD
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
<table width="100%" border="0" cellspacing="5">
    <tr>
      <td><div align="center"><strong>รายงานอุบัติการณ์ความคลาดเคลื่อนในระบบบริหารการใช้ยา<br />
      Medication Management Incident Report</strong></div></td>
    </tr>
    <tr>
      <td>100020002</td>
    </tr>
    <tr>
      <td bgcolor="#CCCCCC"><div align="right"><strong>Department</strong> สูตินารีเวช <strong> Incident No </strong>100020002.</div></td>
    </tr>
    <tr>
      <td><strong>ชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนก</strong></td>
    </tr>
    <tr>
      <td><strong>Diagnosis ผ่าตัด วันที่ผ่าตัด คลอด<br />
      </strong></td>
    </tr>
    <tr>
      <td><strong>แพทย์เจ้าของไข้</strong></td>
    </tr>
    <tr>
      <td><strong>สถานภาพ</strong></td>
    </tr>
    <tr>
      <td><strong>วันที่เกิดเหตุ เวลา สถานที่เกิดเหตุ ภาค ภาค ภาค ภาค ภาค ภาค</strong></td>
    </tr>
    <tr>
      <td><strong>ต้นเหตุเกิดจากแผนก</strong></td>
    </tr>
    <tr>
      <td><strong><u>รายงานสรุปเหตุการณ์ที่เกิดขึ้นทั้งหมด</u></strong></td>
    </tr>
    <tr>
      <td>แผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อ ผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อ ผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยแผนกชื่อผู้ป่วยสระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ สระ น้ำมั้ง ต้นเหตุ สิทธิ์ชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนกชื่อผู้ป่วย/ผู้ประสบปัญหา อายุ เพศ แผนก</td>
    </tr>
  </table>
<table  width="100%" cellpadding="3">
  <tr>
    <td></td>
    <td width="50"></td>
    <td width="130" class="udcenter"></td>
    <td width="60"></td>
    <td width="130" class="udcenter"></td>
  </tr>
</table><div style="page-break-before: always;">
  <table width="100%" cellpadding="3">
    <tr>
      <td>In case of needed assessment by physician (please identify)</td>
    </tr>

    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="90">Physician's name</td>
          <td class="udcenter"></td>
          <td width="120">(On call physician)</td>
          <td width="100">Date of assessment</td>

          <td width="100" class="udcenter"></td>
          <td width="30">Time</td>
          <td width="60" class="udcenter"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">

        <tr>
          <td width="80" valign="top">Describe the assessment<br /></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>

          <td width="80">Treatment</td>
          <td width="50">X-Ray</td>
          <td width="260" class="udleft"></td>
          <td width="50">Result</td>
          <td class="udleft"></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="80" height="21"></td>
          <td width="50">Lab</td>
          <td width="260" class="udleft"></td>
          <td width="50">Result</td>

          <td class="udleft"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="80"></td>
          <td width="50">Other</td>

          <td width="260" class="udleft"></td>
          <td width="50">Result</td>
          <td class="udleft"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">

        <tr>
          <td width="80" valign="top">Describe action taken<br /></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>

          <td width="150">Severity injury affect to </td>
          <td width="50">Client</td>
          <td width="260" class="udleft"></td>
          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>

      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="150" height="21"></td>
          <td width="50">Other</td>
          <td width="260" class="udleft"></td>
          <td></td>
        </tr>
      </table></td>

    </tr>
  </table></div>
  <table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Medication Management Incident Investigation</strong></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">

        <tr>
          <td width="180">1. Level of severity outcome</td>
          <td class="udleft"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">

        <tr>
          <td width="180">2. Type of Medication error index</td>
          <td class="udleft"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td>3. Serious type of medication error</td>

    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="130">Type I</td>
          <td class="udleft"></td>
          <td width="60"></td>
        </tr>

        <tr>
          <td>Type II</td>
          <td class="udleft"></td>
          <td></td>
        </tr>
        <tr>
          <td>Type IIII</td>
          <td class="udleft"></td>

          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">4. Categorization event of medication error</td>

          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">5. Type of Medication order</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>

        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">

        <tr>
          <td width="250">6. Peroid of employment</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>
          <td class="udleft"></td>

          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td>7. Cause of medication error</td>
    </tr>
    <tr>

      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">7.1 Personal/Human factor</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>

          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">7.2 Communication breakdown</td>

          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">7.3 Practice habit</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>

        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">

        <tr>
          <td width="250">7.4 System</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>
          <td class="udleft"></td>

          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">7.5 Product/ Package &amp; Design</td>

          <td class="udleft"></td>
          <td width="150"></td>
        </tr>
        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="250">7.6 Miscellaneous</td>
          <td class="udleft"></td>
          <td width="150"></td>
        </tr>

        <tr>
          <td></td>
          <td class="udleft"></td>
          <td></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td style="padding: 0px;"></td>

    </tr>
    <tr>
      <td>13. Analyze the events that contributed to the outcome and check appropriate categories</td>
    </tr>
    <tr>
      <td style="padding: 0px;"><table width="100%" cellpadding="3" cellspacing="0">
        <tr>
          <td width="150">Human Error</td>

          <td class="udleft"></td>
        </tr>
        <tr>
          <td>System Error</td>
          <td class="udleft"></td>
        </tr>
      </table></td>
    </tr>

    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="485">14. To what extent did the anesthesiologists / anesthetisis contribute to the occurrence?</td>
          <td width="100" class="udleft"></td>
          <td></td>
        </tr>
        </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="485">15. Was corrective action timely and appropriate?</td>
          <td width="100" class="udleft"></td>
          <td></td>
        </tr>

      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="485">16. Does documentation support the analysis?</td>
          <td width="100" class="udleft"></td>
          <td></td>

        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td valign="top">Clinical Summary :<br />
            
            <div class="indent"><br />

            <br /></div>
            Was the outcome preventable?<div class="indent">
            <br /></div>
            What might have been done to change the outcome?<div class="indent">
            <br /></div>           
            Is there clinical evidence to support individual practice change that may have altered outcome?<div class="indent">
            <br /></div>
            Are there any system base changes that may prevent similar future outcomes?<div class="indent">

            <br /></div>
            Lesson learned from this case<div class="indent">
            <br /></div><br /></td>
        </tr>
      </table></td>
    </tr>
  </table>
  <table width="100%" cellpadding="3">

    <tr>
      <td><strong class="under">Additional Investigation and Action Taken</strong></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td width="80" valign="top">Describe the initial action<br /></td>
        </tr>

      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="213">Result of action</td>
          <td width="101">Patient </td>
          <td width="887" class="udcenter"></td>

        </tr>
      </table></td>
    </tr>
  </table>
  <table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Incident Report Investigation</strong></td>
    </tr>

    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="88">Unit/Department</td>
          <td width="268" class="udcenter"></td>
          <td width="465">Sending date</td>
          <td width="26" class="udcenter"></td>
          <td width="204">Receiving date</td>

          <td width="150" class="udcenter"></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td width="80" valign="top">Investigation<br /></td>

        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td width="80" valign="top">Cause<br /></td>
        </tr>

      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="3" cellspacing="0" class="irReporttbl">
        <tr>
          <td class="tblHeader" width="30"><strong>No.</strong></td>
          <td class="tblHeader" ><strong>Corrective &amp; Preventive Actions</strong></td>

          <td class="tblHeader"  width="100"><strong>Start</strong></td>
          <td class="tblHeader"  width="100"><strong>Completed</strong></td>
          <td class="tblHeader"  width="180"><strong>Responsible Person</strong></td>
        </tr>
        <tr>
          <td valign="top" class="center"></td>
          <td valign="top"></td>

          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>
        </tr>
        <tr>
          <td valign="top" class="center"></td>
          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>

          <td valign="top"></td>
        </tr>
        <tr>
          <td valign="top" class="center"></td>
          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>
        </tr>

        <tr>
          <td valign="top" class="center"></td>
          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>
          <td valign="top"></td>
        </tr>
      </table></td>
    </tr>

  </table>
  <table width="100%" cellpadding="3">
    <tr>
      <td><strong class="under">Part of TQM Department</strong></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>

          <td width="70">Grand Topic</td>
          <td width="300" class="udcenter"></td>
          <td width="70">Topic</td>
          <td class="udcenter"></td>
        </tr>
      </table></td>
    </tr>
    <tr>

      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="70">Subtopic 1</td>
          <td width="300" class="udcenter"></td>
          <td width="70">Subtopic 2</td>
          <td class="udcenter"></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="70">Subtopic 3</td>
          <td class="udcenter"></td>
          <td width="70"></td>
          <td></td>

        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td width="80" valign="top">Cause on Incident<br /></td>
        </tr>

      </table></td>
    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0" class="txtborder">
        <tr>
          <td width="80" valign="top">Action<br /></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="110">Physician Defendant</td>
          <td class="udleft"></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="110">Severity Level</td>
          <td class="udleft"></td>
        </tr>
      </table></td>

    </tr>
    <tr>
      <td><table width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td width="110">Quality Concern</td>
          <td width="130" class="udcenter"></td>
          <td width="341">Refer to IMCO Medical Support</td>
          <td width="620" class="udcenter"></td>
        </tr>
      </table></td>
    </tr>
  </table>
</body>
EOD;
	$htmlcontent=stripslashes($htmlcontent);
	$htmlcontent=AdjustHTML($htmlcontent);
	// สร้างเนื้อหาจาก  HTML code
	$pdf->writeHTML($htmlcontent, true, 0, true, 0);
	// เลื่อน pointer ไปหน้าสุดท้าย
	$pdf->lastPage();
	// ปิดและสร้างเอกสาร PDF
	ob_end_clean();
	$pdf->Output('IncidentNo1234.pdf', 'I');
//	$pdf->Output( 'tmp/report.pdf' , 'F' ); หรือใช้อันนี้ copy เป็นไฟล์เอาไว้บน server

?>