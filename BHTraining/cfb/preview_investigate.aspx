<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preview_investigate.aspx.vb" Inherits="cfb_preview_investigate" %>
<page backimg="bhbg.gif" backbottom="10mm" style="font-family: cordiaupc"><page_footer><div><table style="width: 100%; border: solid 1px black;"><tr><td style="text-align: left;width: 70%">33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 2 667 1000 Fax: +662 667 2525  www.bumrungrad.com page </td><td style="text-align: right;width: 30%"><img src="bhfooter.gif" width="120"/></td></tr></table></div></page_footer>
<form id="form1" runat="server" enableviewstate="False" method="post">
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
        <asp:label id="lblservice_type" runat="server"></asp:label>   </td>
    </tr>
    <tr>
      <td> <strong>วันที่เกิดเหตุ </strong>
        <asp:Label ID="lbldatetime_report" runat="server"></asp:Label></td>
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
      <td>
          <strong>ลำดับ #<asp:label id="lblOrder" runat="server" width="25px"></asp:label>  </strong></td>
    </tr>
 
 
    <tr>
      <td>
                      <asp:Label ID="lblDeptCommentDetail"
                        Runat="Server" EnableViewState="False" Font-Bold="False" 
                           />
                          <br /><br />
                          </td>
    </tr>
 
 
    <tr>
      <td>
          <strong>แผนก</strong> <asp:label id="lblDeptName" runat="server" ></asp:label> </td>
    </tr>
 
 
    <tr>
      <td>
          <strong>ประเภท 
          </strong> <asp:label id="lbltypename" runat="server" Width="90%"></asp:label> </td>
    </tr>
 
 
    <tr>
      <td>
          <strong>Grand Topic</strong> <asp:label id="lblGrand" runat="server" 
              Width="90%"></asp:label> </td>
    </tr>
 
 
    <tr>
      <td>
          <strong>Topic</strong> <asp:label id="lblTopic" runat="server" ></asp:label> &nbsp;<asp:label 
              id="lblTopic0" runat="server" ></asp:label>  &nbsp;<asp:label 
              id="lblTopic1" runat="server" ></asp:label>  </td>
    </tr>
 
 
    <tr>
      <td>
          &nbsp;</td>
    </tr>
 
 
    <tr>
      <td>
      <strong>Sup/Mgr/Director Investigation</strong><br />
        </td>
    </tr>
    <tr>
      <td><strong>Investigation</strong></td>
    </tr>
    <tr>
      <td> <asp:label id="lblans1" runat="server" Width="90%"></asp:label> </td>
    </tr>
      <tr>
      <td class="style1"><strong>Cause&nbsp;</strong></td>
    </tr>
      <tr>
      <td> <asp:label id="lblans2" runat="server" Width="90%"></asp:label> </td>
    </tr>
      <tr>
      <td><strong>Corrective & Prevention Action</strong>&nbsp;</td>
    </tr>
      <tr>
      <td> <asp:label id="lblans3" runat="server" Width="90%"></asp:label> </td>
    </tr>
    <tr>
      <td><strong>Result from responding to client</strong>&nbsp;</td>
    </tr>
    <tr>
      <td> <asp:label id="lblans4" runat="server" Width="90%"></asp:label> </td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>


                           <asp:GridView ID="GridViewPrevent" runat="server" Width="100%" 
              AutoGenerateColumns="False" CssClass="tdata" CellPadding="3" 
              DataKeyNames="prevent_dept_id" HeaderStyle-CssClass="colname" 
              EnableModelValidation="True">
           <Columns>
               <asp:TemplateField HeaderText="Action Plans ">
                   <ItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("action_detail") %>'></asp:Label>
                   </ItemTemplate>
                  
                   <ItemStyle Width="40%" />
                   <headerStyle width="40%" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Start">
                   <ItemTemplate>
                       <asp:Label ID="Label2" runat="server" 
                           Text='<%# Bind("date_start", "{0:dd MMM yyyy}") %>'></asp:Label>
                   </ItemTemplate>
                  
                  <ItemStyle Width="10%" />
                   <headerStyle width="10%" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Completed">
                   <ItemTemplate>
                       <asp:Label ID="Label3" runat="server" 
                           Text='<%# Bind("date_end", "{0:dd MMM yyyy}") %>'></asp:Label>
                   </ItemTemplate>
                  <ItemStyle Width="10%" />
                   <headerStyle width="10%" />
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Responsible Person">
                   <ItemTemplate>
                       <asp:Label ID="Label4" runat="server" Text='<%# Bind("resp_person") %>'></asp:Label>
                   </ItemTemplate>
                 <ItemStyle Width="40%" />
                   <headerStyle width="40%" />
               </asp:TemplateField>
           </Columns>
           <HeaderStyle CssClass="colname" />
          </asp:GridView>
        </td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>วันรายงานเหตุการณ์ <asp:label id="lblreport_by" runat="server" Visible="False"></asp:label> 
          <asp:label id="lblreport_tel" runat="server" Visible="False"></asp:label> <asp:label id="lblreport_date" runat="server"></asp:label></td>
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