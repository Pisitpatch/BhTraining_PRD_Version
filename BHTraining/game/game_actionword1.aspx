<%@ Page Language="VB" AutoEventWireup="false" CodeFile="game_actionword1.aspx.vb" Inherits="game_game_actionword1" %>
<!DOCTYPE html> 
<head runat="server">
<meta name="apple-mobile-web-app-capable" content="yes" />
    <title></title>
    <style type="text/css">
    @font-face
{
font-family: myFirstFont;
src: url('TH K2D July8 Bold.ttf')
    ,url('TH K2D July8 Bold.otf'); /* IE9 */
}
div
{
font-family:myFirstFont;
font-weight: bold;
}
    </style>
    <script type="text/javascript" src="jquery-1.6.min.js"></script>
<script type="text/javascript" src="jquery.flip.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script type="text/javascript" src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>
<script type="text/javascript">


    $(document).ready(function () {
        setTime();
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

    var stimer = <%response.write(Session("time_amount").tostring) %>;

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
       sound_correct[0] = "K.MackChailaew.mp3"
        sound_correct[1] = "K.MackDeemark.mp3"
        sound_correct[2] = "K.MackKengmak.mp3"
        sound_correct[3] = "K.MackTooktong.mp3"
        sound_correct[4] = "K.MackYiam.mp3"
        sound_correct[5] = "K.MackChailaew.mp3"
       // sound_correct[6] = "Tooktong.mp3"
       // sound_correct[7] = "Welldone.mp3"
       // sound_correct[8] = "Yiam.mp3"
       // sound_correct[9] = "Chailaew.mp3"

        sound_wrong[0] = "K.MackPayayam.mp3"
        sound_wrong[1] = "K.MackPayayamIkkrung.mp3"
        sound_wrong[2] = "K.MackPayayam.mp3"
       // sound_wrong[3] = "Payayam.mp3"
    $(function () {
        
        <%
        for i as integer = 0 to dsQuestion.tables(0).rows.count - 1
        %>
       
        $("#choice<% response.write(i+1) %>").bind("click", function () {
        <% if dsQuestion.Tables(0).Rows(i)("is_correct").ToString = "1" then 
      
        %>
          //  Math.floor((Math.random()*100)+1);
           // $("#audio-player")[0].src = "beep-1.mp3";
             $("#audio-player")[0].src = "sound/" + sound_correct[Math.floor((Math.random()*4)+1)];
             <% else %>
             // $("#audio-player")[0].src = "beep-5.mp3";
              $("#audio-player")[0].src = "sound/" + sound_wrong[Math.floor((Math.random()*1)+1)];
          <% end if %>
        // alert(1111);
            $("#audio-player")[0].play();
          // stimer = 10;
           <% if (dsQuestion.tables(0).rows(i)("is_correct").toString = "1" ) then %>
                 
                    addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 1 );
                   
           <%else %>
                 
                    addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 0 );
                    
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
<script type="text/javascript">
    $(document).ready(function () {
        document.ontouchmove = function (e) {
            e.preventDefault();
        }
    });
</script>
</head>
<body style="background: url(images/GameJCI-1024x748.jpg); background-repeat:no-repeat">
    
   <div data-role="page" style="width: 1024px; height: 748px;">

	<div data-role="content" data-theme="a" style="margin: 0px; padding: 0px; overflow: hidden; padding-bottom: 20px; height:748px; background: url(images/GameJCI-1024x748-06.jpg); background-repeat:no-repeat">
     <table width="100%">
		    <tr>
		      <td>
              <div style="height: 30px; margin: 0px 40px; font-family: 'Comic Sans MS'; font-size: 16px; font-weight: bold;">
              Employee Name: <%Response.Write(Session("jci_user_fullname").ToString)%> Employee ID: <%Response.Write(Session("jci_emp_code").ToString)%>
             <div style="font-size:60px; font-weight:bold;width:100px;margin-left:870px; margin-top:670px"> <strong><span id = "mytime"></span>&nbsp; </strong></div>
              </div>
              </td>
		      <td align="right">&nbsp;</td>
	        </tr>
	      </table>
    <div id="question_list" style="font-size: 32px; font-weight: bold; margin: 0px 80px; height: 88px; overflow: hidden; padding: 10px 0px;"><%Response.Write(question_text) %> </div>
     <div id="answer_list" style="margin: 8px 50px;">
      <%
        for i as integer = 0 to dsQuestion.tables(0).rows.count - 1
        %>
        <% if dsQuestion.Tables(0).Rows(i)("is_correct").ToString = "1" then 
      
        %>
    <div style="width: 345px; height: 250px; float: left; margin: 0px 0px 30px 80px;"><a href="popup_right.aspx?order=<%response.write(order) %>&q_order=<%response.write(q_order) %>&gid=<%response.write(gid) %>&lang=<%response.write(lang) %>" data-rel="dialog" data-transition="flow" id="choice<% response.write(i+1) %>">
    <%else %>
     <div style="width: 345px; height: 250px; float: left; margin: 0px 0px 30px 80px;"><a href="popup_wrong.aspx?order=<%response.write(order) %>&q_order=<%response.write(q_order) %>&gid=<%response.write(gid) %>&lang=<%response.write(lang) %>" data-rel="dialog" data-transition="flip" id="choice<% response.write(i+1) %>">
    <%end if %>
    <img src="../share/game/answer/<%response.write(dsQuestion.Tables(0).Rows(i)("file_path").ToString) %>" style="border: solid 3px #fff" alt="choice 1" border="0" width="345" height="250" />
   </a></div>		
    <% Next i%>

    <!--
        <div style="width: 345px; height: 250px; float: left; margin: 0px 0px 20px 40px;"><a href="popup_wrong.aspx" data-rel="dialog" data-transition="pop"><img src="images/Herbal-Medicine-Capsules-or-Tablets-.jpg" width="345" height="250"></a></div>
        <div style="width: 345px; height: 250px; float: left; margin: 0px 0px 20px 40px;"><a href="popup_wrong.aspx" data-rel="dialog" data-transition="slidedown"><img src="images/medicine-on-time.jpg" width="345" height="250"></a></div>
        <div style="width: 345px; height: 250px; float: left; margin: 0px 0px 20px 40px;"><a href="popup_wrong.aspx" data-rel="dialog" data-transition="flip"><img src="images/pharmaceuticals_pictures1.jpg" width="345" height="250"></a></div>
        -->
        </div>
  </div><!-- /content -->

</div>
    <audio id="audio-player" name="audio-player" src="sound/button-11.wav"  ></audio>
</body>
</html>
