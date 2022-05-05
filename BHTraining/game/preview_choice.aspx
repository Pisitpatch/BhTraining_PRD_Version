<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_choice.aspx.vb" Inherits="game_preview_choice" %>


<!DOCTYPE html> 
<html> 
	<head> 
	<title>Preview : Multiple Choice</title> 
	<meta name="viewport" content="width=device-width, initial-scale=1"> 
<script src="jquery-1.6.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
<script type="text/javascript">


    $(document).ready(function () {
       // setTime();
        $("#play-bt").click(function () {
            $("#audio-player")[0].play();
            $("#message").text("Music started");
        })

        $("#pause-bt").click(function () {
            $("#audio-player")[0].pause();
            $("#message").text("Music paused");
        })

        $("#stop-bt").click(function () {
            $("#audio-player")[0].pause();
            $("#audio-player")[0].currentTime = 0;
            $("#message").text("Music Stopped");
        })


    });



    function speakQuestion(filename) {
        $("#audio-player")[0].src = filename;
        $("#audio-player")[0].play();
    }

    var stimer = 60;

    function setTime(){
	    //var ini_timer = 10;

	    mytime.innerHTML = stimer;
	  //  form1.htime.value = ini_timer-stimer;
	    myid =window.setTimeout("setTime()",1000);


	    if (stimer <= 0) {
	        timeoutAnswer(0, 0);
		    alert("หมดเวลาสำหรับข้อนี้  ");
		    window.clearTimeout(myid);
		  //  form1.rightAnswer.value = false;
		
		    //alert("test");
		    //alert(form1.name);
		  //  form1.submit();
		    //document.forms[0].submit();
	    }
	    stimer = stimer - 1;
    }


    function clearTime(){

	    if ((!form1.choice[0].checked) &&  (!form1.choice[1].checked) && (!form1.choice[2].checked) && (!form1.choice[3].checked)) {
		    alert("กรุณาเลือกคำตอบก่อน ");
		    return false;
	    }

	    window.clearTimeout(myid);
    }

</script>
<script type="text/javascript">
    var sound_correct = new Array(); 
    var sound_wrong = new Array(); 
       sound_correct[0] = "Chailaew.mp3"
        sound_correct[1] = "Deemark.mp3"
        sound_correct[2] = "Excellent.mp3"
        sound_correct[3] = "Good.mp3"
        sound_correct[4] = "Great.mp3"
        sound_correct[5] = "Kengmak.mp3"
        sound_correct[6] = "Tooktong.mp3"
        sound_correct[7] = "Welldone.mp3"
        sound_correct[8] = "Yiam.mp3"
        sound_correct[9] = "Chailaew.mp3"

        sound_wrong[0] = "Payayam.mp3"
        sound_wrong[1] = "PayayamIkkrung.mp3"
        sound_wrong[2] = "TryAgain.mp3"
        sound_wrong[3] = "Payayam.mp3"
    $(function () {
        
        <%
        for i as integer = 0 to dsQuestion.tables(0).rows.count - 1
        %>
       
        $("#choice<% response.write(i+1) %>").bind("click", function () {
        <% if dsQuestion.Tables(0).Rows(i)("is_correct").ToString = "1" then 
      
        %>
          //  Math.floor((Math.random()*100)+1);
           // $("#audio-player")[0].src = "beep-1.mp3";
             $("#audio-player")[0].src = "sound/" + sound_correct[Math.floor((Math.random()*9)+1)];
             <% else %>
             // $("#audio-player")[0].src = "beep-5.mp3";
              $("#audio-player")[0].src = "sound/" + sound_wrong[Math.floor((Math.random()*3)+1)];
          <% end if %>
        // alert(1111);
            $("#audio-player")[0].play();
          // stimer = 10;
           <% if (dsQuestion.tables(0).rows(i)("is_correct").toString = "1" ) then %>
                 
                 //   addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 1 );
                   
           <%else %>
                 
                //    addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 0 );
                    
           <%end if %>
          //  return false;
        });
        <% next i
         %>


  
    });
			
function addAnswer(answer_id , is_correct){
		$.ajax({
   			type: "POST",
   			url: "ajax_addanswer.ashx?dept_id=<% response.write(session("jci_dept_id").tostring()) %>&emp_code=<% response.write(session("jci_emp_code").tostring()) %>&gid=<% response.write(gid) %>&qid=<%response.write(qid) %>&answer_id=" + answer_id + "&correct=" + is_correct ,
			
  			data: {   },
			success: function(data){
				if (data != ""){
					alert(data);
				
				//	alert("This item code can be used");
					//$("#txtcode").val(data);
				}else{
					//alert("This item code has been used already");
					
			}
			},
  			error: function(e){alert("error :: " +e);}
		});
}	

function timeoutAnswer(answer_id , is_correct){
		$.ajax({
   			type: "POST",
   			url: "ajax_timeoutanswer.ashx?dept_id=<% response.write(session("jci_dept_id").tostring()) %>&emp_code=<% response.write(session("jci_emp_code").tostring()) %>&gid=<% response.write(gid) %>&qid=<%response.write(qid) %>&answer_id=" + answer_id + "&correct=" + is_correct ,
			
  			data: {   },
			success: function(data){
				if (data != ""){
					alert(data);
				
				//	alert("This item code can be used");
					//$("#txtcode").val(data);
				}else{
					//alert("This item code has been used already");
					
			}
			},
  			error: function(e){alert("error :: " +e);}
		});
}	
		
</script>
    <style type="text/css">

div
{
font-family:myFirstFont;
font-weight: bold;
}
.topbar
{font-family: 'Comic Sans MS'; font-size: 18px; font-weight: bold; color: #FFF; padding-top: 15px;}
.content 
{margin: 0px; padding: 0px; overflow: hidden;}
.question
{height: 75px; padding: 10px 80px; font-size: 28px; color: #FFF; font-family: Tahoma; font-weight: normal; line-height: 1.5em;}
.question span 
{color: #FF3}
.answer 
{height: 400px; padding: 13px 250px 13px 80px;}
.answer ol
{margin: 0px;  color: #339; font-size: 28px; font-family: Tahoma; font-weight: normal; line-height: 1.85em; padding-left: 30px;}
.answer li a:link, .answer li a:visited 
{
color: #339;
text-decoration: none;
font-weight: normal;
}
.answer li a:hover
{
color: #339;
text-decoration: none;
}

    </style>
</head> 
<body> 
<!--<audio  autoplay="autoplay" loop="loop">
  <source src="POL-tekno-labs-short.mp3" type="audio/mpeg" />
</audio>-->

<div data-role="page" style="width: 1280px; height: 1024px; background: url(images/GameJCI-1280x1024-06.jpg) no-repeat;">

<div>
		  <table width="100%">
		    <tr>
		      <td height="60" class="topbar">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Player <%Response.Write(Session("jci_user_fullname").ToString)%>  ID <%Response.Write(Session("jci_emp_code").ToString)%>&nbsp;</td>
		      <td width="285" align="right" class="topbar">Time left <span id = "mytime"></span> sec&nbsp;&nbsp;<a href="multiple-choice-index.aspx" data-enhance="false" data-role="none"  rel="external" ><img src="images/on-off.png" width="24" height="24" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	        </tr>
  </table>
	    </div><!-- /header -->

	<div class="content">
    <div class="question" style="margin-top:10px; height: 105px;"><span></span> <%Response.Write(question_text) %> </div>
    <div class="answer" style="font-size:16px; height: 600px;">
  	<ol>
     <%
        for i as integer = 0 to dsQuestion.tables(0).rows.count - 1
        %>
         <% If dsQuestion.Tables(0).Rows(i)("is_correct").ToString = "1" Then%>
    <li> <a href="#"><%Response.Write(dsQuestion.Tables(0).Rows(i)("answer_detail_" & lang).ToString)%></a></li>
    <%Else%>
    <li> <a href="#"><%Response.Write(dsQuestion.Tables(0).Rows(i)("answer_detail_" & lang).ToString)%></a></li>
    <%End If %>
    <% Next i%>
    <!--
    <li> <a href="wrong.html" data-rel="dialog" data-transition="pop">ชื่อ นามสกุล และ Hospital number</a></li>
    <li> <a href="wrong.html" data-rel="dialog" data-transition="pop">ชื่อ นามสกุล และ รูปถ่าย</a></li>
    <li> <a href="correct.html" data-rel="dialog" data-transition="pop">ชื่อ นามสกุล และ วัน เดือน ปีเกิด</a></li>
    -->
  	</ol>
    </div>
    </div><!-- /content -->

</div><!-- /page -->
 <audio id="audio-player" name="audio-player" src="beep-1.mp3"  ></audio>
</body>
</html>
