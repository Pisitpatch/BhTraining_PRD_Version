<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_star.aspx.vb" Inherits="star_preview_star" %>
<%@ Import Namespace="ShareFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title> Preview </title>
<link href="../css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="data">
     <h3 style="text-decoration:underline">Star of Bumrungrad information </h3>
     <asp:Panel ID="panelDetail" runat="server">
        <div style="font-weight:bold">
        Star No. 
            <asp:Label ID="lblStarNo" runat="server" Text="" ForeColor="#ff3399"></asp:Label>
        </div>
        <fieldset >
            <legend>Nominator ผู้เสนอชื่อ</legend>
             <table width="100%" cellspacing="1" cellpadding="2"  bgcolor="#eef1f3">
                 <tr>
                      <td valign="top" width="250">
                          <asp:Label ID="lblCFBdetail10" runat="server" Text="ผู้เสนอชื่อ"></asp:Label>
                      </td>
                      <td>
                          <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                                  <td width="250">
                                      <asp:Label ID="txtname_type" runat="server" Text=""></asp:Label>
                                      <br />
                                  </td>
                                  <td>
                                   <div id="div_hn_remark" runat="server" visible="true">
                                      <asp:Label ID="lblCFBdetail11" runat="server" Text="*โปรดระบุชื่อ-สกุล"></asp:Label>
                                      &nbsp;&nbsp;
                                   
                                       <asp:Label ID="txtcomplain_remark" runat="server" Text=""></asp:Label>

                                </div>
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>

            </table>
            <div id="div_profile" runat="server" visible="false" >
               <table width="100%" cellspacing="1" cellpadding="2" bgcolor="#eef1f3">
               <tr>
               <td width="250"></td>
               <td>
                   <table cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                          <td width="60">
               <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                          </td>
                           <td class="style5">
                               <asp:Label ID="lblCFBdetail4" runat="server" Text="HN"></asp:Label>
                           </td>
                           <td width="120">
                               <asp:Label ID="txthn" runat="server" Width="75px"></asp:Label>
                           </td>
                           <td width="70">
                               <asp:Label ID="lblCFBdetail5" runat="server" Text="สัญชาติ"></asp:Label>
                           </td>
                           <td>
                            <asp:Label ID="txtcountry" runat="server" ></asp:Label>
                              
                           </td>
                       </tr>
                       <tr>
                           <td width="60">
                               <asp:Label ID="lblIRdetail23" runat="server" Text="อายุ" />
                           </td>
                           <td class="style5">
                               
                   <asp:Label ID="txtptage" runat="server" Text=""></asp:Label>
                                      &nbsp;
                                      <asp:Label ID="lblIRdetail5" runat="server" Text="ปี" />&nbsp;</td>
                           <td width="120">
                               &nbsp;</td>
                           <td width="70">
                               <asp:Label ID="lblIRdetail24" runat="server" Text="เพศ" />
                           </td>
                           <td>
                             <asp:Label ID="txtptsex" runat="server" />
                              
                           </td>
                       </tr>
                   </table>
                   </td>
               </tr>
               </table>
            </div>
               <table width="100%" cellspacing="1" cellpadding="2"  bgcolor="#eef1f3">
                <tr>
                  <td valign="top" width="250"><asp:Label ID="lblCFBdetail12" runat="server" Text="เสนอชื่อมาจาก"></asp:Label></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="250" style="vertical-align:top">
                                <asp:Label ID="txtfeedback_from" runat="server" ></asp:Label>
                                 
                                      <br />
                                </td>
                                <td>
                                <div id="div_source_other" runat="server" visible="false">
                                    <asp:Label ID="lblCFBdetail13" runat="server" Text="โปรดระบุ"></asp:Label>&nbsp;&nbsp;
                                    <asp:Label ID="txtfeedback_remark11" runat="server" />
                                   
                                        </div>
                                <div id="div_cfb" runat="server" visible="false">
                               หมายเลข CFB  <asp:Label ID="txtCFBNo" runat="server" Text="" 
                             ></asp:Label>
                                </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
             
  <tr>
    <td width="180" valign="top"><asp:Label ID="lblCFBcomment14" runat="server" Text="การติดต่อกลับลูกค้า" /></td>
    <td><table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td valign="top">
        <asp:Label ID="txtcustomer" runat="server" Text=""></asp:Label>
           
            <br />
         </td>
      </tr>
      <tr>
        <td valign="top">
        <div id="div_response_remark" runat="server" visible="false">
        (โปรดระบุ)&nbsp;&nbsp;  
         <asp:Label ID="txtcus_detail" runat="server" Text=""></asp:Label>
              
            </div>    
                </td>
      </tr>
      </table></td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment19" runat="server" Text="กรณีต้องการติดต่อกลับโดย" Visible="false" /></td>
    <td>
    <div id="div_response_method" runat="server" visible="false">
    <table width="100%" cellspacing="1" cellpadding="2" style="margin-left: -3px;">
      <tr>
        <td width="150"><asp:Label ID="lblCFBcomment20" runat="server" Text="โทรศัพท์" /></td>
        <td>
         <asp:Label ID="txttel" runat="server" Text=""></asp:Label>
       </td>
        </tr>
      <tr>
        <td><asp:Label ID="lblCFBcomment21" runat="server" Text="อีเมล์" /></td>
        <td>
         <asp:Label ID="txtemail" runat="server" Text=""></asp:Label>
       </td>
        </tr>
      <tr>
        <td><asp:Label ID="lblCFBcomment22" runat="server" Text="ที่อยู่ (อื่นๆ ระบุ)" /></td>
        <td>
         <asp:Label ID="txtother" runat="server" Text=""></asp:Label>
       </td>
        </tr>
      </table>
      </div>
      </td>
  </tr>
                  <tr>
                      <td valign="top">
                          <strong><span class="style1">*</span>รายละเอียดที่ท่านประทับใจในบริการ <br />
          (Describe the Occurrence)<br />
          How did the nominee delight you?<br />
          Text Area Autocomplete <br />
          for star co-ordinator</strong></td>
                      <td>
                          <asp:Label ID="txtstar_detail" runat="server" ></asp:Label>
                      </td>
                  </tr>
                  <tr>
                      <td valign="top">
                          &nbsp;</td>
                      <td>
                          &nbsp;</td>
                  </tr>
  </table>
            <div id="section1" runat="server" visible="false">
              <table width="100%" cellspacing="2" cellpadding="3" style="margin: 8px 10px;">
                <tr>
                  <td width="170" valign="top"><asp:Label ID="lblCFBdetail3" runat="server" Text="ชื่อผู้ป่วย"></asp:Label>
                  &nbsp;</td>
                  <td width="783">
                  <table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="250">
                        <asp:DropDownList ID="txttitle" runat="server" Width="50px">
         
                <asp:ListItem Value="นาง">นาง</asp:ListItem>
            <asp:ListItem Value="น.ส.">น.ส.</asp:ListItem>
            <asp:ListItem Value="นาย">นาย</asp:ListItem>
             <asp:ListItem Value="ด.ช.">ด.ช.</asp:ListItem>
             <asp:ListItem Value="ด.ญ.">ด.ญ.</asp:ListItem>                     
             <asp:ListItem Value="">ไม่ระบุ</asp:ListItem>
            <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
            <asp:ListItem Value="Ms.">Ms.</asp:ListItem>
            <asp:ListItem Value="Miss.">Miss.</asp:ListItem>
             <asp:ListItem Value="Master">Master</asp:ListItem>
             <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
           
      
            </asp:DropDownList>
                        <input type="text" id="txtptname" style="width: 165px" 
                runat="server" /> </td>
                        <td width="80">&nbsp;</td>
                        <td width="120">
                            &nbsp;</td>
                        <td width="70">&nbsp;</td>
                        <td>
                            &nbsp;</td>
                      </tr>
                  </table></td>
                </tr>
                  </table>
                  </div>
                    <table width="100%" cellspacing="2" cellpadding="3" style="margin: 8px 10px;">
                 <tr>
    <td valign="top" width="170"><asp:Label ID="lblIRdetail20" Text="สถานที่เกิดเหตุการณ์ประทับใจ" runat="server" /> 
        </td>
    <td>
        <asp:Label ID="txtlocation" runat="server" 
            ></asp:Label> 
     
       
        </td>
  </tr>
   <tr>
    <td valign="top">
        <asp:Label ID="lblIRdetail21" runat="server" Text="หมายเลขห้อง" />
       </td>
    <td>
        <asp:Label ID="txtroom" runat="server"></asp:Label>
      
      </td>
  </tr>
                  <tr>
                      <td valign="top">
                          <asp:Label ID="lblIRdetail14" runat="server" Text="การมารับบริการของผู้ป่วย (ประเภทบริการ)" />
                      </td>
                      <td>
                          <table width="100%">
                              <tr>
                                  <td width="200">
                                   <asp:Label ID="txtservicetype" runat="server"  />
                                      
                                  </td>
                                  <td width="130">
                                      <asp:Label ID="lblIRdetail9" runat="server" Text="ประเภทลูกค้า" />
                                  </td>
                                  <td>
                                   <asp:Label ID="txtsegment" runat="server"  />
                                      
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
            
                <tr>
                  <td valign="top">
                      <asp:Label ID="lblCFBdetail8" runat="server" Text="วันที่เกิดเหตุการณ์ (วันที่เกิดเหตุ)"></asp:Label>
                  </td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                          <td width="150">
                              <asp:Label ID="txtdate_report" runat="server" ></asp:Label>
                        </td>
                      </tr>
                    </table></td>
                </tr>
                <tr>
                  <td valign="top"><asp:Label ID="lblIRdetail22" runat="server" 
                          Text="วันที่เขียนเสนอชื่อ"></asp:Label></td>
                  <td>
                      <asp:Label ID="txtdate_complaint" runat="server" ></asp:Label>
                   
                     
                    </td>
                </tr>
              
             
              </table>
           
 </fieldset>
  <fieldset>
  <legend>Nominee ผู้ถูกเสนอชื่อ</legend>
     
      <table width="100%" cellspacing="2" cellpadding="3" >
                <tr>
                  <td width="350" valign="top" bgcolor="#eef1f3"><b>Nominee&#39;s Type</b></td>
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
                                      <asp:Label ID="lblNominee" runat="server" Text="Label"></asp:Label>
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
                                      <asp:Label ID="lblDeptSelect" runat="server" Text="Label"></asp:Label>
                                  </td>
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
                                      <asp:Label ID="lblDocSelect" runat="server" Text="Label"></asp:Label>
                                  </td>
                              </tr>
                          </table>
                      </td>
                </tr>
                <tr>
                    <td bgcolor="#eef1f3" valign="top">
                        (กรณีค้นหาไม่เจอ) ระบุชื่อ-สกุล พนักงาน/แพทย์&nbsp;</td>
                    <td>
                        <asp:Label ID="txtcustom_name" runat="server" ></asp:Label>
                    </td>
                </tr>
                  </table>
                  
  </fieldset>

             
              <br />
              </asp:Panel>
  <h3 style="text-decoration:underline">Chair of Star Committee </h3>
    <asp:GridView ID="GridManagerComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px"
 BorderStyle="None" 
                          EmptyDataText="There is no Sup/Mgr review">
                     
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                              <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td width="60" rowspan="3" valign="top" bgcolor="#FFFFFF"><img src="../images/thumb_user.jpg" width="50" height="50"></td>
                            <td bgcolor="#EDF4F9"><table width="100%" cellspacing="1" cellpadding="2">
                              <tr>
                                <td valign="top"><strong>
                                  <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                                </strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label>
                                   ,
                                  <asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label>
                                   ,
                                <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                                      <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
 </td>
                              <td><div align="right">
                                  <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                                
                               
                                                                 
</div></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("subject") %>'></asp:Label>  
                                  &nbsp; </td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                          
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("detail") %>'></asp:Label>
                           
                            </td>
                          </tr>
                      </table>
                                        
                                      
                      </td>
                    </tr>
                    
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#D4D4D4"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                <br />
      <h3 style="text-decoration:underline">Star Committee </h3>
      <asp:GridView ID="GridComment" runat="server" AutoGenerateColumns="False" 
                          Width="100%" EnableModelValidation="True" BorderWidth="0px"
 BorderStyle="None" 
                          EmptyDataText="There is no Committee review">
                          <Columns>
                              <asp:TemplateField>
                             
                              <ItemStyle BorderStyle="None" />
                                  <ItemTemplate>
                              <table width="100%" cellspacing="0" cellpadding="0" >
                    <tr>
                      <td>
                      <table width="100%" border="0">
                          <tr>
                            <td width="60" rowspan="3" valign="top" bgcolor="#FFFFFF"><img src="../images/thumb_user.jpg" width="50" height="50"></td>
                            <td bgcolor="#EDF4F9"><table width="100%" cellspacing="1" cellpadding="2">
                              <tr>
                                <td valign="top"><strong>
                                  <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("review_by_name") %>'></asp:Label>
                                </strong>
                                <asp:Label ID="lblPostJobType" runat="server" Text='<%# Bind("review_by_jobtype") %>'></asp:Label>
                                   ,
                                  <asp:Label ID="lblPostJobTitle" runat="server" Text='<%# Bind("review_by_jobtitle") %>'></asp:Label>
                                   ,
                                <asp:Label ID="lblPostDept" runat="server" Text='<%# Bind("review_by_dept_name") %>'></asp:Label>
                                      <asp:Label ID="lblPosttime" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
 </td>
                              <td><div align="right">
                                  <asp:Label ID="lblEmpcode" runat="server" Text='<%# Bind("review_by_empcode") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' Visible="false"></asp:Label>
                                  <asp:ImageButton ID="cmdEditComment" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Edit Comment" Visible="false" />                                
                                
                                  <asp:ImageButton ID="cmdMgrReview" runat="server" ImageUrl="~/images/bt_edit.gif" ToolTip="Review Score" Visible="false"  />                                
</div></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                                  Types of recognition :  <asp:Label ID="Label55" runat="server" Text='<%# Bind("recognition_type_name") %>'></asp:Label>
                                  &nbsp;
                                  <br />
                                                             </td>
                          </tr>
                          <tr>
                            <td bgcolor="#FFFFFF">
                          
                           
                           
                            </td>
                          </tr>
                      </table>
                                       
                       <asp:Panel ID="Panel1" runat="server">
                       <table width="100%">
                       <tr>
                       <td width="250" style="font-weight:bold">เหตุการณ์/คำชม :</td>
                       <td><asp:Label ID="Label6" runat="server" Text='<%# bind("event_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">รายละเอียดเหตุการณ์ :</td>
                       <td><asp:Label ID="Label7" runat="server" Text='<%# bind("detail_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">สิ่งที่ประทับใจเกี่ยวข้องกับ :</td>
                       <td><asp:Label ID="lblCare" runat="server" Text=''></asp:Label>
                       <asp:Label ID="lblchk1" runat="server" Text='<%# bind("chk_clear") %>' Visible="false"></asp:Label>
                         <asp:Label ID="lblchk2" runat="server" Text='<%# bind("chk_care") %>' Visible="false"></asp:Label>
                           <asp:Label ID="lblchk3" runat="server" Text='<%# bind("chk_smart") %>' Visible="false"></asp:Label>
                             <asp:Label ID="lblchk4" runat="server" Text='<%# bind("chk_quality") %>' Visible="false"></asp:Label>
                       
                       </td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Types of recognition</td>
                       <td><asp:Label ID="Label9" runat="server" Text='<%# bind("recognition_type_name") %>'></asp:Label></td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Endorse :</td>
                       <td><asp:Label ID="Label2" runat="server" Text='<%# bind("recognition_name") %>'></asp:Label>
                       <asp:Label ID="lblEndorseID" runat="server" Text='<%# bind("recognition_id") %>' Visible="false"></asp:Label>
                       </td>
                       </tr>
                        <tr>
                       <td style="font-weight:bold">Recognition Award</td>
                       <td><asp:Label ID="lblAward" runat="server" Text='<%# bind("recognition_award") %>'></asp:Label></td>
                       </tr>
                         <tr>
                       <td style="font-weight:bold">ข้อพิจารณาของคณะกรรมการ</td>
                       <td><asp:Label ID="lblCommitteeConsider" runat="server" Text='<%# bind("committee_name") %>'></asp:Label></td>
                       </tr>
                       <tr >
                       <td style="font-weight:bold">Comment</td>
                       <td><asp:Label ID="Label11" runat="server" Text='<%# bind("committee_comment") %>'></asp:Label></td>
                       </tr>
                       <tr>
                       <td></td>
                       <td></td>
                       </tr>
                       <tr>
                       <td></td>
                       <td></td>
                       </tr>
                       </table>
                                   
                          
  
   
                            </asp:Panel>

                      
                      </td>
                    </tr>
                    
                  </table>
                    <table width="100%">
                      <tr>
                        <td height="1" valign="top" bgcolor="#D4D4D4"></td>
                      </tr>
                    </table>
                     <br />
                                  </ItemTemplate>
                                
                              </asp:TemplateField>
                            
                          </Columns>
                          <RowStyle BorderStyle="None" />
                      </asp:GridView>
                      <table cellspacing="0" cellpadding="3" width="80%" align="center">
            <tbody>
              <tr>
                <td bgcolor="#6699cc" valign="top" 
                  width="305"><strong>Conclusion</strong></td>
                <td bgcolor="#6699cc" valign="top" width="723"><strong><u>Recognition Award</u></strong></td>
              </tr>
              <tr>
                <td valign="top"><strong>Endorse (<asp:Label ID="lblSumEndorse1" runat="server" Text=""></asp:Label>)
                    <br />
                </strong></td>
                <td valign="top"><asp:Label ID="lblEndorseDetail" runat="server" Text=""></asp:Label></td>
              </tr>
           
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
              </tr>
              <tr>
                <td valign="top"><strong>Do Not Endorse (<asp:Label ID="lblSumNotEndorse1" runat="server" Text=""></asp:Label>)</strong></td>
                <td valign="top">&nbsp;</td>
              </tr>
            </tbody>
        </table>

        <h3 style="text-decoration:underline">Star Coordinator </h3>
          <table width="100%" cellpadding="2" cellspacing="1">
     
         <tr>
         <td width="199" valign="top" bgcolor="#eef1f3"><strong>Types of Recognition Teams</strong></td>
         <td width="838">    
             <asp:Label ID="txthr_recog_type" runat="server" ></asp:Label>
         <br /></td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>เหตุการณ์/คำชม</strong></td>
         <td valign="top">
          <asp:Label ID="txtevent_admire" runat="server" ></asp:Label>
       
         </td>
       </tr>
       <!--
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>ข้อความที่ระบุในเหตุการณ์ที่ท่านประทับใจ</strong></td>
         <td valign="top"><strong>
             <asp:DropDownList ID="txtsentence_admire" runat="server" 
                 DataTextField="admire_topic" DataValueField="admire_id">
             </asp:DropDownList>
         &nbsp;</strong></td>
       </tr>
       -->
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>การระบุรายละเอียดเหตุการณ์</strong></td>
         <td valign="top">
          <asp:Label ID="txthr_detail_combo" runat="server" ></asp:Label>
        
            
         
         </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>สิ่งที่ประทับใจเกี่ยวข้องกับ<br />
           Appreciation Category</strong></td>
         <td><label></label>
             <table cellspacing="0" cellpadding="3" width="90%" align="center">
               <tbody>
                 <tr>
                   <td 
                  width="25%" valign="top"  class="style3"><strong>Clear</strong></td>
                   <td width="25%" valign="top"  class="style3"><strong>Care</strong></td>
                   <td width="25%" valign="top" class="style3"><strong>Smart</strong></td>
                   <td width="25%" valign="top" class="style3"><strong>Smart</strong></td>
                 </tr>
                 <tr>
                   <td valign="top"><strong>
                     <label> </label>
                     </strong>
                       <label>
                       <input type="checkbox" name="chk_communicate" id="chk_communicate" runat="server" />
                       </label>
                     ความสามารถในการสื่อสาร<strong><br />
                     </strong></td>
                   <td valign="top">
                     <input type="checkbox" name="chk_relative" id="chk_relative" runat="server" />
                  สัมพันธไมตรีแบบไทย</td>
                   <td valign="top">
                     <input type="checkbox" name="chk_talent" id="chk_talent" runat="server" />
                   ความเป็นเลิศทางวิชาการ</td>
                   <td valign="top">
                     <input type="checkbox" name="chk_quality" id="chk_quality" runat="server" />
                   คุณภาพงานบริการ</td>
                 </tr>
               </tbody>
           </table></td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Effective Recognition</strong></td>
         <td valign="top">
             <asp:Label ID="txthr_recog" runat="server" ></asp:Label>
      
      
        </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Effective Types of recognition</strong></td>
         <td valign="top">
         <asp:Label ID="txthr_type" runat="server" ></asp:Label>
        </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>ข้อพิจารณาของคณะกรรมการ</strong></td>
         <td valign="top">
         <asp:Label ID="txthr_commit" runat="server" ></asp:Label>
         
      </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Comment</strong></td>
         <td >
             <asp:Label ID="txtstar_comment" runat="server" ></asp:Label>
         </td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Reward 
           Summary</strong></td>
         <td valign="top" bgcolor="#eef1f3">&nbsp;</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"> - Staff Recognition Point</td>
         <td valign="top">
         <asp:Label ID="txtsrp" runat="server" ></asp:Label>
            
          
         </td>
       </tr>
       
       <tr>
         <td bgcolor="#eef1f3" valign="top">- Cash </td>
         <td valign="top">
           <asp:Label ID="txtcash" runat="server" ></asp:Label>
        </td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Awarding Date</strong></td>
         <td valign="top"><asp:Label ID="txtdate_award" runat="server" ></asp:Label>
           
             &nbsp;วันที่สรุปผลรางวัล</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Reward Recieve 
           Date</strong></td>
         <td valign="top">
             <asp:Label ID="txtdate_receive" runat="server" ></asp:Label>
             
                             
             &nbsp; วันที่พนักงานรับรางวัล</td>
       </tr>
       <tr>
         <td bgcolor="#eef1f3" valign="top"><strong>Remark</strong></td>
         <td valign="top">
         <asp:Label ID="txtstar_remark" runat="server" ></asp:Label>
            
           </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Main Topic</strong></td>
         <td valign="top">
             <asp:Label ID="lblMainTopic" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
       <tr>
         <td valign="top" bgcolor="#eef1f3"><strong>Sub Topic</strong></td>
         <td valign="top">
             <asp:Label ID="lblSubTopic" runat="server" Text="Label"></asp:Label>
           </td>
       </tr>
     </table>
    </div>
    </form>
</body>
</html>
