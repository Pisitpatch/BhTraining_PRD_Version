<?php
 	ob_start();
 	include('../share/test.html');
	$content = ob_get_clean();
//	$content = '<page style="font-family: angsanaupc"><br />'.$content.'</page>';
//	$content = '<page backimg="bhbg.gif" backbottom="10mm" style="font-family: cordiaupc"><page_footer><div><table style="width: 100%; border: solid 1px black;"><tr><td style="text-align: left;width: 50%">ไทย ไทย 33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667 2525  www.bumrungrad.com page [[page_cu]]/{nb}</td><td style="text-align: right;width: 50%"><img src="bhfooter.gif" width="120"/></td></tr></table></div></page_footer>'.$content.'</page>';
	// conversion HTML => PDF
	require_once('html2pdf.class.php');
//	$html2pdf = new HTML2PDF('P','A4','en','UTF-8');
	$html2pdf = new HTML2PDF('P','A4','en', true, 'UTF-8');
//	$html2pdf->setModeDebug();
//	$html2pdf->AddFont('tahoma','','tahoma.php');
//	$html2pdf->AddFont('tahoma','B','tahomab.php');
	$html2pdf->setDefaultFont('cordiaupc');
 	ob_end_clean();
//	$html2pdf->writeHTML($content, isset($_GET['vuehtml']));
	$html2pdf->writeHTML($content);
	$html2pdf->Output('print.pdf','I');
?>
