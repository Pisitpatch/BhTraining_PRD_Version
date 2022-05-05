<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_cfb.aspx.vb" Inherits="cfb_incident" %>
<page backimg="bhbg.gif" backbottom="10mm" style="font-family: cordiaupc"><page_footer><div><table style="width: 100%; border: solid 1px black;"><tr><td style="text-align: left;width: 70%">33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667 2525  www.bumrungrad.com page </td><td style="text-align: right;width: 30%"><img src="bhfooter.gif" width="120"/></td></tr></table></div></page_footer>
<form id="form1" runat="server">
  <table width="100%" border="0" cellpadding="2" cellspacing="2">
    <tr>
      <td align="center"><strong>รายงานการดำเนินการตอบสนองความคิดเห็นของลูกค้า</strong></td>
    </tr>
    <tr>
      <td align="center"><strong>Service Recovery and Customer Feedback Action Report</strong></td>
    </tr>
    <tr>
      <td bgcolor="#F7F7F7"><div align="right"><strong>Department</strong> 
          <asp:label id="lblDept" runat="server"></asp:label><strong> CFB No </strong>  <asp:label runat="server" id="lblNo" text="lblNo"></asp:label></div></td>
    </tr>
    <tr>
      <td><strong>ชื่อลูกค้า/ผู้ประสบปัญหา
        
      </strong>
        <asp:label id="lbltitle" runat="server" text=""></asp:label>
        <asp:label id="lblName" runat="server" text=""></asp:label>        <strong> HN
          </strong>
        <asp:Label ID="lblHN" runat="server"></asp:Label>
        <strong>       อายุ        </strong>
        <asp:Label runat="server" ID="lblAge"></asp:Label>
        <strong>      เพศ        </strong>
      <asp:Label runat="server" ID="lblSex"></asp:Label>       
      </td>
    </tr>
    <tr>
      <td><strong>สัญชาติ </strong>        <asp:Label runat="server" ID="lblcountry"></asp:Label> <strong>ประเภทบริการ</strong>
        <asp:label id="lblservice_type" runat="server"></asp:label> 
        <strong>&nbsp;ประเภทลูกค้า</strong>        
          <asp:label id="lblcustomertype" runat="server"></asp:label>  </td>
    </tr>
    <tr>
      <td> <strong>วันที่เกิดเหตุ </strong>
        <asp:Label ID="lbldatetime_report" runat="server"></asp:Label>&nbsp;
         <strong>Date of complaint</strong>
        <asp:Label ID="lbldatetime_complaint" runat="server"></asp:Label></td>
    </tr>
    <tr>
      <td>
      <strong>สถานภาพผู้ร้องเรียน</strong>  <asp:Label ID="txtcomplain_status" runat="server"></asp:Label>
       &nbsp;<strong>ชื่อผู้ร้องเรียน</strong>    <asp:label id="lblcomplain_status_remark" runat="server"></asp:label></td>
    </tr>
    <tr>
      <td><strong>คำร้องเรียนมาจาก</strong>        <asp:label id="lblfeedback_from_remark" runat="server"></asp:label></td>
    </tr>
    <tr>
      <td><strong>เหตุเกิด (แผนก/ห้อง)</strong>&nbsp;        <asp:label id="lbllocation" 
              runat="server"></asp:label>
              <strong>&nbsp;หมายเลขห้อง&nbsp;</strong>        <asp:label id="lblroom" 
              runat="server"></asp:label>
              </td>
    </tr>
    
    <tr>
      <td><strong>การวินิจฉัย</strong>&nbsp;        <asp:label id="lbldiagnosis" 
              runat="server"></asp:label>&nbsp;  <strong>ผ่าตัด</strong>        
          <asp:label id="lblprocedure" runat="server"></asp:label></td>
    </tr>
    
    <tr>
      <td>
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" CellPadding="3">
              <Columns>
                  <asp:TemplateField HeaderText="Type of comment">
                      <ItemTemplate>

                          <asp:Label ID="Label3" runat="server" Text='<%# Bind("comment_type_name") %>'></asp:Label>
                           <asp:Label ID="lblDeptList" runat="server" ForeColor="GrayText" Text="" 
                                          Visible="true" />
                      </ItemTemplate>
                    
                      <HeaderStyle Width="10%" />
                      <ItemStyle Width="10%" />
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Describe the Occurrence">
                        <ItemTemplate>
                          <asp:Label ID="lblPK" runat="server" Text='<%# Bind("comment_id") %>' 
                                          Visible="false"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("comment_detail").replace(vbcrlf,"<br/>") %>'></asp:Label><br />
                          <br />
                        
                      
                        </ItemTemplate>
                       
                        <HeaderStyle Width="50%" />
                        <ItemStyle Width="50%" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Describe the Solution">
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Eval("comment_solution").replace(vbcrlf,"<br/>") %>'></asp:Label>
                      </ItemTemplate>
                     
                      <HeaderStyle Width="20%" />
                      <ItemStyle Width="20%"  VerticalAlign="Top" />  
                  </asp:TemplateField>

                    <asp:TemplateField HeaderText="Unit Defendant">
                      <ItemTemplate>
                          <asp:Label ID="lblunit" runat="server" ></asp:Label>
                      </ItemTemplate>
                     
                      <HeaderStyle Width="10%" />
                      <ItemStyle Width="10%"  VerticalAlign="Top" />  
                  </asp:TemplateField>

                  <asp:TemplateField HeaderText="Physician Defendant">
                      <ItemTemplate>
                          <asp:Label ID="lbldoctor" runat="server" ></asp:Label>
                      </ItemTemplate>
                     
                      <HeaderStyle Width="10%" />
                      <ItemStyle Width="10%"  VerticalAlign="Top" />  
                  </asp:TemplateField>
              </Columns>
          </asp:GridView>
        </td>
    </tr>
 
    <tr>
      <td class="style1"></td>
    </tr>
  
 
    <tr>
      <td>
      
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
              EnableModelValidation="True" Width="100%" CellPadding="3">
              <Columns>
                  <asp:TemplateField HeaderText="Type of comment">
                      <ItemTemplate>

                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("comment_type_name") %>'></asp:Label>
                           <asp:Label ID="lblDeptList0" runat="server" ForeColor="GrayText" Text="" 
                                          Visible="true" />
                      </ItemTemplate>
                    
                      <HeaderStyle Width="10%" />
                      <ItemStyle Width="10%" VerticalAlign="Top" />
                     
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="No">
                       <HeaderStyle Width="10%" />
                      <ItemStyle Width="10%" VerticalAlign="Top" />
                        <ItemTemplate>
                            #<asp:Label ID="lblNo" runat="server" Text='<%# Bind("order_sort") %>'></asp:Label>
                        </ItemTemplate>
                      
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate>
                          <asp:Label ID="lblPKDept" runat="server" Text='<%# Bind("comment_id") %>' 
                                          Visible="false"></asp:Label>
                          <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("dept_id") %>' 
                                          Visible="false"></asp:Label>
                            <asp:Label ID="Label5" runat="server" 
                                Text='<%# Eval("dept_name").replace(vbcrlf,"<br/>") %>'></asp:Label><br />
                       
                        </ItemTemplate>
                       
                       
                       <HeaderStyle Width="30%" />
                      <ItemStyle Width="30%" VerticalAlign="Top" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Investigation detail">
                      <ItemTemplate>
                      <strong>Investigation</strong><br />
                          <asp:Label ID="lblAns1" runat="server" ></asp:Label><br />
                          <strong>Cause</strong><br />
                          <asp:Label ID="lblAns2" runat="server" ></asp:Label><br />
                            <strong>Corrective & Prevention Action</strong><br />
                          <asp:Label ID="lblAns3" runat="server" ></asp:Label><br />
                             <strong>Result from responding to client</strong><br />
                          <asp:Label ID="lblAns4" runat="server" ></asp:Label><br />
                          <br />
                          Investigate by  <asp:Label ID="lblManagerName" runat="server" ></asp:Label>
                            
                      </ItemTemplate>
                      <HeaderStyle Width="50%" />
                      <ItemStyle Width="50%" VerticalAlign="Top" />
                    
                  </asp:TemplateField>
              </Columns>
          </asp:GridView>
        </td>
    </tr>
    <tr>
      <td><strong>การจัดการข้อร้องเรียน</strong>&nbsp; <asp:label id="lblcom" 
              runat="server"></asp:label> </td>
    </tr>
    <tr>
      <td><strong>ลูกค้า</strong> <asp:label id="lblcustomer" runat="server"></asp:label> </td>
    </tr>
     <tr>
      <td><strong>ต้องการติดต่อกลับโดย</strong>  </td>
    </tr>
     <tr>
      <td><asp:label id="lblcallback" 
              runat="server"></asp:label>&nbsp;</td>
    </tr>
     <tr>
      <td>&nbsp;</td>
    </tr>
     <tr>
      <td>
          <asp:Label ID="lblDepartmentList" runat="server" Text="-" Width="100%" 
              Visible="False"></asp:Label>
         </td>
    </tr>
     <tr>
      <td>&nbsp;</td>
    </tr>
      <tr>
      <td><strong>วันที่รายงานเหตุการณ์</strong> <asp:label id="lblreport_by" 
              runat="server" Visible="False" ></asp:label> <asp:label id="lblreport_tel" 
              runat="server" Visible="False"></asp:label> <asp:label id="lblreport_date" runat="server"></asp:label></td>
    </tr>
    <tr>
      <td>ผู้สืบสวนเหตุการณ์ </td>
    </tr>
  <!--
    <tr>
      <td>ผู้สรุปเหตุการณ์ <asp:label id="lblOwner" runat="server" Visible="False"></asp:label> </td>
    </tr>
      -->
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td></td>
    </tr>
  </table>
<table  width="100%" cellpadding="3">
  <tr>
    <td></td>
    <td width="50"></td>
    <td width="130" class="udcenter"></td>
    <td width="60"></td>
    <td width="130" class="udcenter"></td>
  </tr>
</table></form></page> 