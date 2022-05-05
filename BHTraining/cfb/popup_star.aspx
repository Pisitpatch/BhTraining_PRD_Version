<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_star.aspx.vb" Inherits="cfb_popup_star" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="data">
 <fieldset>
  <legend>Nominee ผู้ถูกเสนอชื่อ</legend>
     
      <table width="100%" cellspacing="2" cellpadding="3" >
                <tr>
                  <td width="250" valign="top" bgcolor="#eef1f3"><b>Nominee&#39;s Type</b></td>
                  <td>
                      <asp:RadioButtonList ID="txtnominee_type" runat="server" 
                          RepeatDirection="Horizontal">
                          <asp:ListItem Value="1">Employee</asp:ListItem>
                          <asp:ListItem Value="2">Subcontract</asp:ListItem>
                      </asp:RadioButtonList>
                    </td>
                  </tr>
                    <tr>
                      <td valign="top" bgcolor="#eef1f3"><b>ประเภทบุคคล / Nominee&#39;s Name &nbsp;</b></td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:TextBox ID="txtfind_name" runat="server" Width="80px"></asp:TextBox>
                                      <asp:Button ID="cmdFindName" runat="server" Text="Search" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" >
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtperson_all" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" SelectionMode="Multiple" Width="200px" Rows="5">
                                      </asp:ListBox>
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &gt;&gt;" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelatePerson" runat="server" CausesValidation="False" 
                                          Text=" &lt;&lt;" />
                                  </td>
                                  <td valign="top" >
                                      <asp:ListBox ID="txtperson_select" runat="server" DataTextField="user_fullname" 
                                          DataValueField="emp_code" Width="200px" Rows="5"></asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                  <tr>
                      <td valign="top" bgcolor="#eef1f3"><b>ประเภททีม / Nominee&#39;s Department &nbsp;</b></td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtdept_all" runat="server" 
                                          DataTextField="dept_name_en" DataValueField="costcenter_id" 
                                          SelectionMode="Multiple" Width="200px" Rows="5"></asp:ListBox>
                                  </td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelateDept" runat="server" Text=" &gt;&gt;" 
                                          CausesValidation="False" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelateDept" runat="server" Text=" &lt;&lt;" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="492">
                                      <asp:ListBox ID="txtdept_select" runat="server" DataTextField="costcenter_name" 
                                          DataValueField="costcenter_id" Width="200px" Rows="5"></asp:ListBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" width="492">
                                      &nbsp;</td>
                              </tr>
                          </table>
                      </td>
                </tr>
                
                  <tr>
                      <td bgcolor="#eef1f3" valign="top">
                          ชื่อแพทย์ผู้ถูกเสนอ&nbsp;</td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:TextBox ID="txtfind_doctor" runat="server" Width="80px"></asp:TextBox>
                                      <asp:Button ID="cmdFindDoctor" runat="server" Text="Search" 
                                          CausesValidation="False" />
                                  </td>
                                  <td valign="top" width="60">
                                      &nbsp;</td>
                                  <td valign="top" width="492">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td valign="top" width="210">
                                      <asp:ListBox ID="txtdoctor_all" runat="server" DataTextField="doctor_name_en" 
                                          DataValueField="emp_no" SelectionMode="Multiple" Width="200px" Rows="5">
                                      </asp:ListBox>
                                      &nbsp;</td>
                                  <td valign="top" width="60">
                                      <asp:Button ID="cmdAddRelateDoctor" runat="server" CausesValidation="False" 
                                          Text=" &gt;&gt;" />
                                      <br />
                                      <br />
                                      <asp:Button ID="cmdRemoveRelateDoctor" runat="server" CausesValidation="False" 
                                          Text=" &lt;&lt;" />
                                  </td>
                                  <td valign="top" width="492">
                                      <asp:ListBox ID="txtdoctor_select" runat="server" 
                                          DataTextField="doctor_name_en" DataValueField="emp_no" Width="200px" 
                                          Rows="5">
                                      </asp:ListBox>
                                  </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        (กรณีค้นหาไม่เจอ) ระบุชื่อ-สกุล พนักงาน/แพทย์&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtcustom_name" runat="server" Rows="3" TextMode="MultiLine" 
                            Width="80%"></asp:TextBox>
                    </td>
                </tr>
                  </table>
                  
  </fieldset>
    <div align="center"> <asp:Button ID="cmdSave" runat="server" Text="Send to Star" Font-Bold="true" OnClientClick="return confirm('Are you sure you want to send to Star of BI Online?')" />
     <input type="button" value="Close" style="width: 100px;"  onclick=" window.close();" />
    </div>
    </div>
   
    </form>
</body>
</html>
