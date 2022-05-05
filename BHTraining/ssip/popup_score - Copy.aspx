<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_score.aspx.vb" Inherits="ssip_popup_score" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
</head>
<body style="margin:2px" onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="panelManager" runat="server">
      <table width="100%" cellspacing="1" cellpadding="2">
      <tr><td  style="text-align:right" bgcolor="#eef1f3">
          <asp:Button ID="cmdSave" runat="server" Text="Save Review" OnClientClick="alert('Save completed')" />
      </td></tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3">
            
            <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px; margin-right: -3px;">
                <tr>
                  <td width="156" valign="top" bgcolor="#eef1f3"><strong>Review</strong></td>
                  <td width="180" valign="top" bgcolor="#eef1f3"><strong>By</strong></td>
                  <td width="159" valign="top" bgcolor="#eef1f3"><strong>Position</strong></td>
                  <td width="189" valign="top" bgcolor="#eef1f3"><strong>Unit/Dept</strong></td>
                  <td width="120" bgcolor="#eef1f3"><strong>Date</strong></td>
                  <td width="120" bgcolor="#eef1f3"><strong>Turn Around Time</strong></td>
                </tr>
                <tr>
                  <td valign="top" bgcolor="#F8F8F8">
                                  
                      <asp:Label ID="lblJobType" runat="server" 
                                      Text=""></asp:Label></td>
                  <td valign="top"><input name="txtname" type="text" id="txtname" style="width: 180px"  runat="server" readonly="readonly" /></td>
                  <td valign="top"><input name="txtjobtitle" type="text" id="txtjobtitle" style="width: 150px"  runat="server" readonly="readonly" /></td>
                  <td valign="top"><input name="txtdeptname" type="text" id="txtdeptname" style="width: 180px"  runat="server" readonly="readonly" /></td>
                  <td><input name="txtdatetime" type="text" disabled="disabled" id="txtdatetime" style="width: 120px;"  readonly="readonly" runat="server" /></td>
                  <td>&nbsp;</td>
                </tr>
              </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>1. ข้อเสนอแนะและนวัตกรรมนี้ เกี่ยวข้องกับหน่วยงานของท่านใช่หรือไม่ </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer1" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason1" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>2. ข้อมูลประกอบข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายให้ท่านเข้าใจได้</strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer2" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason2" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr bgcolor="#eef1f3">
            <td valign="top"><strong>3. ข้อเสนอแนะและนวัตกรรมนี้ สามารถอธิบายถึงสถานการณ์ วิธีการ ขั้นตอนการทำงานที่เป็นอยู่จริงในอนาคต  พร้อมทั้งสามารถนำมาใช้ได้จริงหรือไม่ </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer3" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason3" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>4. ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ เคยได้รับการเสนอหรือได้รับการพิจารณาจากผู้บริหารโรงพยาบาลมาก่อนหรือไม่
              <label for="select20"></label>
            </strong></td>
          </tr>
          <tr>
            <td valign="top">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer4" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason4" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>5. หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้มีประโยชน์ และสามารถนำมาปฏิบัติได้จริง หน่วยงานของท่านมีนโยบายจะทำหรือไม่</strong></td>
          </tr>
          <tr>
            <td valign="top">
           <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer5" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason5" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
            </td>
          </tr>
        
          <tr>
            <td valign="top"><table width="98%" align="center" cellpadding="2" cellspacing="1" style="margin-left: -3px; margin-top: -3px;">
                <tr bgcolor="#eef1f3">
                  <td width="30" valign="top" ><strong>5.1</strong></td>
                  <td width="865" valign="top" ><strong>กรณีสามารถนำไปปฏิบัติได้จริง จำเป็นต้องกำหนดนโยบายหรือระเบียบข้อบังคับหรือไม่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >
                 <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="23">&nbsp;</td>
                  <td width="215" style="vertical-align:top">
                      <asp:DropDownList ID="txtanswer51" runat="server">
                      <asp:ListItem Value="">-- Please Select --</asp:ListItem>
                      <asp:ListItem Value="yes">Yes</asp:ListItem>
                       <asp:ListItem Value="no">No</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                <td width="100" style="vertical-align:top">กรุณาระบุเหตุผล</td>
                  <td>
                      <asp:TextBox ID="txtmgr_reason51" runat="server" Width="90%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                   </td>
                </tr>
            </table>
                  
                  </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.2</strong></td>
                  <td valign="top" ><strong>หากท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำมาได้จริง จะต้องใช้งบประมาณเท่าไหร่</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" >จำนวนเงิน
                    <input type="text" name="txtbudget_num" id="txtbudget_num" style="width: 180px" runat="server" />
                    บาท <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        runat="server" ControlToValidate="txtbudget_num"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" Display="Dynamic" ></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.3</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมนี้ สามารถนำไปปฏิบัติและช่วยให้เกิดการเปลี่ยนแปลงที่ดีขึ้นได้ ในระดับใด</strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="200" valign="top">
                            <asp:ListBox ID="lblDeptAll" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="185px" SelectionMode="Multiple"></asp:ListBox>
&nbsp;</td>
                        <td width="60" valign="top" style="text-align:center">
                            <asp:Button ID="cmdAddDept" runat="server" Text=" >> " /><br />
                            <asp:Button ID="cmdRemoveDept" runat="server" Text=" << " />
                          </td>
                          <td valign="top" >
                              <asp:ListBox ID="lblDeptSelect" runat="server" DataTextField="dept_name_en" 
                                  DataValueField="dept_id" Width="185px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table></td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.4</strong></td>
                  <td valign="top" ><strong>ท่านคิดว่าข้อเสนอแนะและนวัตกรรมที่พนักงานเสนอมานี้ หากนำมาปฏิบัติจะเป็นประโยชน์กับโรงพยาบาลในด้านใดบ้าง </strong></td>
                </tr>
                <tr >
                  <td valign="top">&nbsp;</td>
                  <td valign="top" ><table width="100%" cellspacing="3" cellpadding="2">
                      <tr>
                        <td width="312" valign="top"><input type="checkbox" name="chk_benefit1" id="chk_benefit1" runat="server" />
                          เพิ่มความพึงพอใจให้แก่ลูกค้า<br /></td>
                        <td width="492" valign="top"><input type="checkbox" name="chk_benefit2" id="chk_benefit2" runat="server" />
                          ประหยัดค่าใช้จ่ายเป็นเงิน
                          <input type="text" name="txtbenefit_num" id="txtbenefit_num" style="width: 180px" runat="server" />
                          บาท <br />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        runat="server" ControlToValidate="txtbenefit_num"
ErrorMessage="Please Enter Only Numbers"  ValidationExpression="^\d+$" Display="Dynamic" ></asp:RegularExpressionValidator> 
                           </td>
                      </tr>
                      <tr>
                        <td valign="top"><input type="checkbox" name="chk_benefit3" id="chk_benefit3" runat="server" />
                          เพิ่มรายได้หรือกำไรให้แก่องค์กร </td>
                        <td valign="top"><input type="checkbox" name="chk_benefit4" id="chk_benefit4" runat="server" />
                          เพิ่มประสิทธิภาพในการทำงาน </td>
                      </tr>
                      <tr>
                        <td valign="top"><input type="checkbox" name="chk_benefit5" id="chk_benefit5" runat="server" />
                          ปรับปรุงคุณภาพของสถานที่ทำงาน</td>
                        <td valign="top"><input type="checkbox" name="chk_benefit6" id="chk_benefit6" runat="server" />
                          อื่นๆ (โปรดระบุ)</td>
                      </tr>
                  </table></td>
                </tr>
                <tr bgcolor="#eef1f3">
                  <td valign="top" ><strong>5.5</strong></td>
                  <td valign="top" ><strong>กรณีที่ท่านประเมินว่า ข้อเสนอแนะและนวัตกรรมนี้สามารถนำไปใช้ได้จริง ท่านมีแผนการเพื่อดำเนินการนำไปใช้อย่างไร </strong></td>
                </tr>
                <tr>
                  <td valign="top" >&nbsp;</td>
                  <td valign="top" ><textarea name="txtplan" id="txtplan" cols="45" rows="5" 
                          style="width: 850px" runat="server"></textarea></td>
                </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top" bgcolor="#eef1f3"><strong>6. ข้อแนะนำอื่น ๆ</strong></td>
          </tr>
          <tr>
            <td valign="top"><textarea name="txtother" id="txtother" cols="45" rows="5" 
                    style="width: 850px" runat="server"></textarea></td>
          </tr>

        </table>
        </asp:Panel>
    </div>
    <br />
    </form>
</body>
</html>
