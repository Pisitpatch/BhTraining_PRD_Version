<%@ Page Language="VB" AutoEventWireup="false" CodeFile="game_actionword.aspx.vb" Inherits="game_game_actionword" %>
<html>
<head>
<meta name="apple-mobile-web-app-capable" content="yes">
<link rel="apple-touch-icon" href="images/3plussoft.png"/>

<style type="text/css">



html {
	margin: 0px;
	padding: 0px
}
body {
	margin: 0px;
	padding: 0px
}
.container img.wide {
	max-width: 100%;
	max-height: 100%;
	height: auto;
}
.container img.tall {
	max-height: 100%;
	max-width: 100%;
	width: auto;
}
​
</style>
<script type="text/javascript" src="jquery-1.6.min.js"></script>
<script type="text/javascript" src="jquery.flip.min.js"></script>
  <link rel="stylesheet" href="../js/jquery.mobile/jquery.mobile-1.1.0.min.css" />
<script src="../js/jquery.mobile/jquery.mobile-1.1.0.min.js"></script>

<script type="text/javascript">
    $(window).load(function () {
        $('.container').find('img').each(function () {
            var imgClass = (this.width / this.height > 1) ? 'wide' : 'tall';
            $(this).addClass(imgClass);
        })
    });

    $(document).ready(function () {
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
            $("#audio-player")[0].play();
            var $this = $(this);
            $("#flipbox<% response.write(i+1) %>").flip({
                direction: $this.attr("rel"),
                speed: 600,
                /*	color: $this.attr("rev"),*/
                //content: $this.attr("title"),//(new Date()).getTime(),
                <% if dsQuestion.Tables(0).Rows(i)("is_correct").ToString = "1" then %>
                content: "<img src='images/ok-icon1.png' alt='OK' />", 
                <% else %>
                 content: "<img src='images/no-icon1.png' alt='OK' />", 
                <% end if %>
                onBefore: function () { $(".revert").show() },
                onEnd: function () {

                // $("#audio-player")[0].play();
                 <% if (dsQuestion.tables(0).rows(i)("is_correct").toString = "1" ) then %>
                 
                    addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 1 );
                    setTimeout(function () {
                  
                                $.mobile.changePage("game_actionword.aspx?order=<%response.write(order) %>&q_order=<%response.write(q_order) %>&gid=<%response.write(gid) %>", "slide", false, false);
                      //  document.frm.action = "game_actionword.aspx?order=<%response.write(order) %>&q_order=<%response.write(q_order) %>&gid=<%response.write(gid) %>";
                       // document.frm.submit();
                    }, 2000); // in milliseconds
                <%else %>
                 
                    addAnswer(<% response.write(dsQuestion.Tables(0).Rows(i)("answer_id").ToString) %> , 0 );
                 <%end if %>

                    //console.log('when the animation has already ended');
                }
            })
            return false;
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
		
</script>
</head>
<body style="margin:0px 0px 0px 0px"><div data-role="page" style="margin:0px 0px 0px 0px"> 
	<div data-role="content" style="margin:0px 0px 0px 0px">
<form action="" method="post" id="frm" name="frm" >
  <div style="width:100%; height:6%; vertical-align:middle; background-color:#000">
    <table width="100%">
      <tr>
        <td width="50%"><a href="javascript:;" onclick="window.location.reload();"><img src="images/home1.png" alt="home" title="home" border="0" /></a> <a href="dialogLogff.aspx" data-rel="dialog" data-transition="pop" ><img src="images/logoff.png" alt="logoff" title="logoff" border="0" /></a></td>
        <td align="right">
       
        <a href="javascript:;" onclick="window.location.reload();"><img src="images/refresh.png" alt="home" title="home" border="0" /></a></td>
      </tr>
    </table>
  </div>
  <div id="main" style="width:100%;">
  <div id="content" >
   <%
        for i as integer = 0 to dsQuestion.tables(0).rows.count - 1
        %>
    <div  class="container" id="flipbox<% response.write(i+1) %>" style="background-color: rgb(200, 217, 126); visibility: visible; width:50%; height:47% ; float:left ; text-align:center ">
      <a href="javacript:;" id="choice<% response.write(i+1) %>" class="top" rel="bt" rev="#B0EB17" title="Ohhh yeah!">
      <img src="../share/game/answer/<%response.write(dsQuestion.Tables(0).Rows(i)("file_path").ToString) %>" alt="choice 1" border="0" /></a> 
   
    </div>
    <%Next i %>
  
  </div>
  </div>
</form>
<audio id="audio-player" name="audio-player" src="beep-1.mp3"  ></audio>
</div>
</div>
</body>
</html>
