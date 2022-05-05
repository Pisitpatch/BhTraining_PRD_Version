<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_commitee_score.aspx.vb" Inherits="ssip_popup_commitee_score" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inno Champian Committee Review</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
  <script type="text/javascript">
      function validateCommittee() {
          if (confirm('Are you sure you want to update review?')) {
              if ($("#txtaward_scale").val() == "") {
                  alert("กรุณาระบุ Award Scale");
                  $("#txtaward_scale").focus();
                  return false;
              }

              if ($("#txtscorename1").val() == 0) {
                  alert("กรุณาระบุ ข้อเสนอแนะและนวัตกรรมนี้");
                  $("#txtscorename1").focus();
                  return false;
              }

              if ($("#txtscorename2").val() == 0) {
                  alert("กรุณาระบุ รูปแบบข้อเสนอแนะและนวัตกรรมนี้ ");
                  $("#txtscorename2").focus();
                  return false;
              }

              if ($("#txtscorename3").val() == 0) {
                  alert("กรุณาระบุ ความสมบูรณ์ของข้อเสนอแนะ");
                  $("#txtscorename3").focus();
                  return false;
              }

              if ($("#txtscorename4").val() == 0) {
                  alert("กรุณาระบุ การศึกษาเพื่อสนับสนุนในการนำเสนอนวัตกรรมและข้อเสนอแนะ");
                  $("#txtscorename4").focus();
                  return false;
              }

              if ($("#txtscorename5").val() == 0) {
                  alert("กรุณาระบุ ค่าใช้จ่ายในการนำมาใช้");
                  $("#txtscorename5").focus();
                  return false;
              }

          } else {
              return false;
          }
      }
  </script>
</head>
<body onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div>
    <div >
    <asp:Panel ID= "panel_score" runat="server">
<table width="98%" align="center" cellpadding="2" cellspacing="1">
  <tr>
    <td valign="top" bgcolor="#eef1f3"></td>
    <td valign="top" bgcolor="#eef1f3"><div align="right"><strong>
        <asp:Button ID="cmdSave" runat="server" Text="Save Review" Font-Bold="true" OnClientClick="return validateCommittee();" />&nbsp;
        <input type="button" id="cmdClose" value ="Close Window" onclick="window.close();"    />
    </strong></div></td>
  </tr>
  <tr>
    <td colspan="2" valign="top" bgcolor="#eef1f3">
     <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                <tr>
                  <td width="156" valign="top" bgcolor="#eef1f3"><strong>Review</strong></td>
                  <td width="180" valign="top" bgcolor="#eef1f3"><strong>By</strong></td>
                  <td width="159" valign="top" bgcolor="#eef1f3"><strong>Position</strong></td>
                  <td width="189" valign="top" bgcolor="#eef1f3"><strong>Unit/Dept</strong></td>
                  <td width="120" bgcolor="#eef1f3"><strong>Date</strong></td>
                </tr>
                <tr>
                  <td valign="top">
                                  
                      <asp:Label ID="lblJobType" runat="server" 
                                      Text=""></asp:Label></td>
                  <td valign="top">
                      <asp:Label ID="txtname" runat="server" Text=""></asp:Label>
                  
                 </td>
                  <td valign="top">
                   <asp:Label ID="txtjobtitle" runat="server" Text="" />
                 </td>
                  <td valign="top">
                   <asp:Label ID="txtdeptname" runat="server" Text="" />
                  </td>
                  <td>
                  <asp:Label ID="txtdatetime" runat="server" Text="" />
                 </td>
                </tr>
              </table>
    </td>
  </tr>
</table>
<table width="98%" align="center" cellpadding="2" cellspacing="1">
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>A. การพิจารณาเรื่องประโยชน์ที่จับต้องได้ (Tangible)</strong></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>1. ข้าพเจ้าเห็นด้วยว่าการนำข้อเสนอแนะ / นวัตกรรมนี้ไปใช้จะช่วยให้ประหยัดค่าใช้จ่ายหรือเพิ่มรายได้
      
    </strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><label for="radio20"></label></td>
        <td width="215" valign="top"><strong>
                      <asp:DropDownList ID="txtanswer1" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    &nbsp;</strong></td>
        <td width="23"><label for="label5"></label></td>
        <td valign="top" width="100">กรุณาระบุเหตุผล          </td>
        <td valign="top">                      <asp:TextBox ID="txtmgr_reason1" runat="server" Width="90%" 
                          TextMode="MultiLine" Rows="3"></asp:TextBox>
                   &nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr bgcolor="#eef1f3">
    <td valign="top"><strong>2. ข้าพเจ้าเห็นด้วยกับมูลค่าโดยประมาณที่จะเพิ่มขึ้นหรือที่จะประหยัดได้</strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="100%" cellspacing="0" cellpadding="0">
      <tr>
        <td width="23"><label for="radio20"></label></td>
        <td width="215" valign="top"><strong>
                      <asp:DropDownList ID="txtanswer2" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    &nbsp;</strong></td>
        <td width="23"><label for="label5"></label></td>
        <td valign="top" width="100">กรุณาระบุเหตุผล </td>
        <td valign="top">
                      <asp:TextBox ID="txtmgr_reason2" runat="server" Width="90%" 
                          TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>การคำนวณการประหยัดค่าใช้จ่ายในปีแรก:</strong></td>
  </tr>
  <tr><td>
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="25%"><span style="font-weight: bold">
           <asp:TextBox ID="txtamount_old" runat="server" Width="150px"></asp:TextBox>
          -</span></td>
        <td width="25%"><span style="font-weight: bold">
          <asp:TextBox ID="txtamount_new" runat="server" Width="150px"></asp:TextBox>
            </span></td>
       
        <td width="25%"> <asp:TextBox ID="txtamount_save" runat="server" Width="150px" 
                BackColor="#FFFF99"></asp:TextBox>
            <strong>บาท</strong></td>
             <td width="25%"><span style="font-weight: bold"> <asp:Button ID="cmdCal11" runat="server" Text="Calculate" /> 
           <asp:TextBox ID="txtamount_cal" runat="server" Width="80px" Visible="false"></asp:TextBox>
                 </span></td>
      </tr>
      <tr>
        <td><strong>ค่าใช้จ่ายต่อปี (วิธีเก่า)</strong></td>
        <td><strong>ค่าใช้จ่ายต่อปี  (วิธีที่นำเสนอใหม่)</strong></td>
        <td><strong>จำนวนที่คาดว่าจะประหยัด<!--ค่าใช้จ่ายในการนำไปใช้ - รายได้--> </strong></td>
        <td><strong></strong></td>
      </tr>
    </table>
    </td></tr>
  <tr>
    <td valign="top" bgcolor="#eef1f3"><strong>B. การพิจารณาประโยชน์ที่จับต้องไม่ได้ (Intangible)</strong></td>
  </tr>
  <tr>
    <td valign="top"><table width="98%" align="center" cellpadding="2" cellspacing="1">
      <tr bgcolor="#eef1f3">
        <td valign="top" bgcolor="#eef1f3" ><strong>1. ข้อเสนอแนะและนวัตกรรมนี้ สามารถนำไปใช้ประโยชน์ในด้านใด</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList ID="txtscorename1" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" AutoPostBack="True">
            </asp:DropDownList>
        
        </strong></td>
        <td valign="top" ><input type="text" name="txtscore1" id="txtscore1" 
                style="width: 180px" runat="server" value="0" /></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>2. รูปแบบข้อเสนอแนะและนวัตกรรมนี้ </strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList ID="txtscorename2" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" AutoPostBack="True">
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" ><input type="text" name="txtscore2" id="txtscore2" 
                style="width: 180px" runat="server" value="0" /></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>3.ความสมบูรณ์ของข้อเสนอแนะ</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" ><strong>
            <asp:DropDownList ID="txtscorename3" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" AutoPostBack="True" 
                style="height: 22px">
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" ><input type="text" name="txtscore3" id="txtscore3" 
                style="width: 180px" runat="server" value="0" /></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" ><strong>4. การศึกษาเพื่อสนับสนุนในการนำเสนอนวัตกรรมและข้อเสนอแนะ</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr >
        <td valign="top"><strong>
            <asp:DropDownList ID="txtscorename4" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" AutoPostBack="True">
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" ><input type="text" name="txtscore4" id="txtscore4" 
                style="width: 180px" runat="server" value="0" /></td>
      </tr>
      <tr bgcolor="#eef1f3">
        <td valign="top" bgcolor="#eef1f3" ><strong>5. ค่าใช้จ่ายในการนำมาใช้</strong></td>
        <td valign="top" bgcolor="#eef1f3" ><strong>คะแนน </strong></td>
      </tr>
      <tr>
        <td valign="top" class="style1" ><strong>
            <asp:DropDownList ID="txtscorename5" runat="server" DataTextField="score_name" 
                DataValueField="score_value" Width="450px" AutoPostBack="True">
            </asp:DropDownList>
        
        &nbsp;</strong></td>
        <td valign="top" class="style1" ><input type="text" name="txtscore5" id="txtscore5" 
                style="width: 180px" runat="server" value="0" /></td>
      </tr>
        <tr>
            <td valign="top">
                <strong>Intangible Benefits Award Scale</strong>&nbsp;</td>
            <td  valign="top">
                <asp:DropDownList ID="txtaward_scale" runat="server" AutoPostBack="True" 
                    Width="150px">
                    <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                    <asp:ListItem Value="1">Improve Environment and Safety</asp:ListItem>
                    <asp:ListItem Value="2">Increase Efficiency</asp:ListItem>
                    <asp:ListItem Value="3">Customer	Service</asp:ListItem>
                    <asp:ListItem Value="4">Healthcare</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
      <tr>
        <td valign="top" bgcolor="#B9C6CE" ><strong>Total</strong></td>
        <td valign="top" bgcolor="#B9C6CE" >
           
            <asp:Label
                ID="txtsum" runat="server" Text="0" Font-Bold="True" Font-Size="16px"></asp:Label> </td>
      </tr>
        <tr>
            <td bgcolor="#B9C6CE" valign="top">
                <strong>Result</strong></td>
            <td bgcolor="#B9C6CE" valign="top">
                <asp:Label ID="lblResult" runat="server" Text="-"></asp:Label>
            </td>
        </tr>
        <tr>
            <td bgcolor="#B9C6CE" valign="top">
                <strong>Reference :</strong>&nbsp;Moderate 34 - 44, Substantial 45 - 64, High 65 -84, 
                Exceptional 85 - 100</td>
            <td bgcolor="#B9C6CE" valign="top">
                &nbsp;</td>
        </tr>
    </table></td>
  </tr>
</table>
</asp:Panel>
</div>
    </div>
    </form>
</body>
</html>
