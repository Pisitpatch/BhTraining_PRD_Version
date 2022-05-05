<? 

Function sendRequest($host,$method,$path,$data)
{
    
	//$method = strtoupper($method);
	$fp = fsockopen($host, 80);

	fputs($fp, "$method $path HTTP/1.1\r\n");
	fputs($fp, "Host: $host\r\n");
	fputs($fp,"Content-type: application/x-www-form-urlencoded\r\n");
	fputs($fp, "Content-length: " . strlen($data) . "\r\n");

	fputs($fp, "Connection: close\r\n\r\n");
	if ($method == 'POST') 
		{
			fputs($fp, $data);
		}

	while (!feof($fp)) 
		{
			$result .= fgets($fp,128);
		}
	fclose($fp);
	return $result;
}

function gwStatus($raw_socket_return) {
	$raw_socket_return = trim($raw_socket_return);
                $socket_status = "";
                $socket_return = explode("\n", $raw_socket_return);
				$count = count($socket_return);
				$iresult = $count-2;
				$socket_status = $socket_return[$iresult];
		return $socket_status;
	}
?>

<? 
//$host="www.sms.in.th";
$host="sms.powerpointproduct.com";
$method="POST";
$path="/tunnel/sendsms.php";

$RefNo="1001";//1001-9999
$Sender="0818054570";

$Msn="0818054570";
echo $Msn;

$Msg="Test test Thaihp.org";
$MsgType="T";
$User="bh@natnat";
$Password="bhsms";


$result=sendRequest($host,$method,$path,'RefNo='.$RefNo.'&Sender='.$Sender.'&Msn='.$Msn.'&Msg='.$Msg.'&MsgType='.$MsgType.'&User='.$User.'&Password='.$Password);

echo gwStatus($result);
 ?>