<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_incident.aspx.vb" Inherits="incident_preview_incident" %>
<form id="form1" runat="server">
<page backimg="bhbg.gif" backbottom="10mm" style="font-family: cordiaupc"><page_footer><div><table style="width: 100%; border: solid 1px black;"><tr><td style="text-align: left;width: 70%">33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667 2525  www.bumrungrad.com</td><td style="text-align: right;width: 30%"><img src="bhfooter.gif" width="120"/></td></tr></table></div></page_footer>
  <table width="100%" border="0" cellpadding="2" cellspacing="2">
    <tr>
      <td align="center"><strong>รายงานอุบัติการณ์</strong></td>
    </tr>
    <tr>
      <td align="center"><strong>Accident & Incident Reports
          </strong></td>
    </tr>
    <tr>
      <td bgcolor="#F7F7F7"><div align="right"><strong>Department</strong> 
          <asp:label id="lblDept" runat="server"></asp:label><strong> Incident No </strong>  <asp:label runat="server" id="lblNo" text="lblNo"></asp:label></div></td>
    </tr>
    <tr>
      <td><strong>ชื่อผู้ป่วย/ผู้ประสบปัญหา  </strong>
        <asp:label id="lblName" runat="server" text=""></asp:label>        <strong> HN
          </strong>
        <asp:Label ID="lblHN" runat="server"></asp:Label>
        <strong>       อายุ        </strong>
        <asp:Label runat="server" ID="lblAge"></asp:Label>
        <strong>      เพศ        </strong>
      <asp:Label runat="server" ID="lblSex"></asp:Label></td>
    </tr>
    <tr>
      <td><strong>แผนก/ห้อง</strong>&nbsp;  <asp:Label runat="server" ID="lblroom"></asp:Label>  <strong> Diagnosis</strong>  <% Response.Write(diag) %></td>
    </tr>
    <tr>
      <td><strong>ผ่าตัด</strong>
        <asp:Label ID="lblprocedure" runat="server">-</asp:Label>
        <strong>วันที่ผ่าตัด/คลอด </strong>
        <asp:Label ID="txtdate_oper" runat="server">-</asp:Label></td>
    </tr>
    <tr>
      <td><strong>แพทย์เจ้าของไข้  </strong>
      <asp:label id="lbldoctor_owner" runat="server">-</asp:label></td>
    </tr>
    <tr>
      <td><strong>สถานภาพ&nbsp;</strong>  <asp:label id="lblpttype" runat="server"></asp:label>&nbsp;
            <strong>ประเภทบริการ</strong>        <asp:label id="lblservicetype" runat="server"></asp:label>
            &nbsp; <strong>ประเภทลูกค้า </strong>  <asp:label id="lblsegment" runat="server"></asp:label>
            </td>
    </tr>
    <tr>
      <td><strong>วันที่เกิดเหตุ  </strong>
        <asp:label id="lbldatetime_report" runat="server"></asp:label>
        <strong>      สถานที่เกิดเหตุ        </strong>
      <asp:Label ID="lbllocation" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
      <td><strong>แผนกที่เกี่ยวข้อง</strong>
        <asp:label id="lblDeptRelate" runat="server"></asp:label>
         </td>
    </tr>
    <tr>
      <td><strong><u>รายงานสรุปเหตุการณ์ที่เกิดขึ้นทั้งหมด</u></strong></td>
    </tr>
    <tr>
      <td><asp:label id="lblDetail" runat="server"></asp:label></td>
    </tr>
     <tr>
      <td><strong>รายละเอียดการดำเนินงานเบื้องต้น</strong></td>
    </tr>
    <tr>
      <td><asp:Label ID="lblinitial_action" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
      <td><strong><u>การดำเนินการหลังเกิดเหตุการณ์ขึ้น</u></strong></td>
    </tr>
    <tr>
      <td><asp:Label ID="lblpost_action" runat="server" Text=""></asp:Label></td>
    </tr>
   
    <tr>
      <td><strong>จากผลการดำเนินงาน 
        
    
      </strong></td>
    </tr>
    <tr>
      <td><asp:Label ID="lblAction_Remark" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
      <td><strong><u>ผลการประเมินผู้ป่วย</u></strong>  <asp:Label runat="server" ID="lblSevere"></asp:Label></td>
    </tr>
    <tr>
      <td><strong><u>ผลการประเมินด้านอื่น</u></strong>  <asp:Label runat="server" ID="lblEffect"></asp:Label></td>
    </tr>
   
    

    <tr>
      <td>&nbsp;</td>
    </tr>
  
    

    <tr>
      <td >
         <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </td>
    </tr>
  
    

    <tr>
      <td bgcolor="#F7F7F7">&nbsp;</td>
    </tr>
  
    

    <tr>
      <td bgcolor="#F7F7F7"><strong>รายงานการติดตามการเกิดอุบัติการณ์ (Sup/Mgr/Director Investigated)</strong></td>
    </tr>
    <tr>
      <td>
          <asp:Label ID="lblDepartmentList" runat="server" Text="-" Width="100%"></asp:Label>
        </td>
    </tr>
   <!--
    <tr>
      <td><strong><u>วิธีแก้ไขปัญหาและป้องกันการเกิดเหตุซ้ำ</u></strong></td>
    </tr>
    <tr>
      <td>
          <asp:Label ID="lblPreventionList" runat="server" Text="" Width="100%"></asp:Label>
        </td>
    </tr>
    -->

    <tr>
      <td bgcolor="#F7F7F7"><strong>ส่วนของฝ่ายบริหารงานคุณภาพ (TQM)</strong></td>
    </tr>
    <tr>
      <td><strong>Grand Topic</strong> <asp:Label ID="lblTopic" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
     <tr>
      <td><strong>Topic</strong> <asp:Label ID="lblTopic1" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
     <tr>
      <td><strong>Sub Topic1</strong> <asp:Label ID="lblTopic2" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
         <tr>
      <td><strong>Sub Topic2</strong> <asp:Label ID="lblTopic3" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
         <tr>
      <td><strong>Sub Topic3</strong> <asp:Label ID="lblTopic4" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
       <tr>
      <td><strong>Topic detail</strong> <asp:Label ID="lblTopicDetail" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
  
  
    <tr>
      <td><strong>Incident Cause </strong>&nbsp;
      <asp:Label ID="lblIRCause" runat="server" Text=""  Width="100%"></asp:Label>
      </td>
    </tr>
      <tr>
      <td><strong>Action</strong>&nbsp;<br /> <asp:Label ID="lblAction" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
    <tr>
      <td><strong>Severity level </strong>&nbsp; <asp:Label ID="lblLevel" runat="server" Text=""  Width="100%"></asp:Label></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>วันรายงานเหตุการณ์ 
      <asp:label id="lblreport_by" runat="server" Visible="False"></asp:label> 
          <asp:label id="lblreport_tel" runat="server" Visible="False"></asp:label> <asp:label id="lblreport_date" runat="server"></asp:label> </td>
    </tr>
    <tr>
      <td>ผู้สืบสวนเหตุการณ์ </td>
    </tr>
    <tr>
      <td>ผู้สรุปเหตุการณ์ 
      <asp:label id="lblowner" runat="server"></asp:label> </td>
    </tr>
    <tr>
      <td>&nbsp;</td>
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
</table>
</page>
</form>
