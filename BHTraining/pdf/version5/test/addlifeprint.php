<?php
	require_once("setPDFaddlife.php");
// 	ob_start();
// 	include('irform.html');
//	include ('http://powerpointproduct.com/bh_demo/pdf3/irform.html');
	include ('http://powerpointproduct.com/bh_demo/pdf3/addlifeform.html');
	$content = <<<EOF
<table width="100%" border="0" cellspacing="2">
    <tr>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
    </tr>
    <tr>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
    </tr>
    <tr>
      <td align="center"><h2>นายธิติ จิตเสรี ภาค ภาคน้ำผู้สิทธิ์</h2></td>
      <td align="center"><h2>นายธิติ จิตเสรี</h2></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
    </tr>
  </table> 
<table width="100%" border="0" cellspacing="2">
    <tr>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
    </tr>
    <tr>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
    </tr>
    <tr>
      <td align="center"><h2>นายธิติ จิตเสรี</h2></td>
      <td align="center"><h2>นายธิติ จิตเสรี</h2></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
    </tr>
  </table>
<table width="100%" border="0" cellspacing="2">
    <tr>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
      <td align="center"><img src="images/logo.gif" width="120"/></td>
    </tr>
    <tr>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
      <td align="center"><h2>Mr.Thiti Jitseri</h2></td>
    </tr>
    <tr>
      <td align="center"><h2>นายธิติ จิตเสรี</h2></td>
      <td align="center"><h2>นายธิติ จิตเสรี</h2></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
      <td align="center" bgcolor="#CCCCCC"><strong>DoB 28-Feb-11 Date 28-Feb-11</strong></td>
    </tr>
    <tr>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
      <td align="center" bgcolor="#CCCCCC">VN: 666666666 CN:23849400</td>
    </tr>
  </table>
EOF;
	$pdf->setPrintHeader(false);
	$pdf->setPrintFooter(false);
	$resolution= array(100, 40);
	$pdf->AddPage('P', $resolution);
//	$pdf->AddPage();
	$htmlcontent= $content ;
//	$htmlcontent=stripslashes($htmlcontent);
	$htmlcontent=AdjustHTML($htmlcontent);
	// สร้างเนื้อหาจาก  HTML code
	$pdf->SetDisplayMode('fullwidth','TwoColumnRight');
	$pdf->writeHTML($htmlcontent, true, 0, true, 0);
	// เลื่อน pointer ไปหน้าสุดท้าย
	$pdf->lastPage();
	// ปิดและสร้างเอกสาร PDF
	ob_end_clean();
	$pdf->Output('test.pdf', 'I');
?>