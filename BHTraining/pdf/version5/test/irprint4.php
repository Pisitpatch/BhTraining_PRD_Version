<?php
require_once("setPDF.php");
 ob_start();
// 	include('irform.html');
//	include ('http://powerpointproduct.com/bh_demo/pdf3/irform.html');
include ('http://powerpointproduct.com/bh_demo/pdf3/irform.php?foo=1&bar=2');
$content = ob_get_clean();
$pdf->setPrintHeader(false);
//	$pdf->setPrintFooter(false);
$pdf->AddPage();
//	$htmlcontent= '<<<EOD'.$content.'EOD;' ;
$htmlcontent= $content;
$htmlcontent=stripslashes($htmlcontent);
$htmlcontent=AdjustHTML($htmlcontent);
	// ���ҧ�����Ҩҡ  HTML code
$pdf->writeHTML($htmlcontent, true, 0, true, 0);
	// ����͹ pointer �˹���ش����
$pdf->lastPage();
	// �Դ������ҧ�͡��� PDF
ob_end_clean();
$pdf->Output('test.pdf', 'I');
?>