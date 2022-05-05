<%@ Page Language="VB" AutoEventWireup="false" CodeFile="result_template.aspx.vb" Inherits="jci2013_result_template" %>

<html>
<head>
<title>
รายงานผลการประเมินคุณภาพภายใน  : Internal Quality Assessment Report</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Content-Language" content="th">
<meta http-equiv="pragma" content="no-cache">
</head>

<style type="text/css">
body {
	margin-left: 0px;
	margin-top: 0px;
	font-family: Tahoma, "Microsoft Sans Serif";
	font-size: 12px;
}
</style>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
     <form id="form1" runat="server">
<table width="95%" align="center" cellpadding="0" cellspacing="0" style="border:2px #CCCCCC; background: #DCE6FC;margin-left: 0px;margin-top: 0px;">
  
  <tr>
    <td align="center"><table width="100%" border="0" cellspacing="0" cellpadding="5">
      <tr>
        <td colspan="2" align="center" bgcolor="#FFFFFF"><table width="100%" border="1" cellspacing="0" cellpadding="5">
          <tr>
            <td align="center"><h2>รายงานผลการประเมินคุณภาพภายใน :<br>
Internal Quality Assessment Report</h2></td>
          </tr>
        </table>          </td>
        </tr>
      <tr>
        <td width="50%" bgcolor="#EBEBEB"><strong>หน่วยงานที่ถูกประเมิน : <asp:Label ID="lblDept" runat="server" Text=""></asp:Label> </strong><strong>, Type: <asp:Label ID="lblType" runat="server" Text=""></asp:Label><br>
        Location: <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label> Building: <asp:Label ID="lblBuilding" runat="server" Text=""></asp:Label></strong></td>
        <td bgcolor="#EBEBEB"><strong>Date <asp:Label ID="lblDate" runat="server" Text=""></asp:Label> Time <asp:Label ID="lblTime" runat="server" Text=""></asp:Label></strong></td>
      </tr>
      <tr>
        <td bgcolor="#EBEBEB"><strong>ผู้ประเมิน / Assessors  :  <asp:Label ID="lblLeader" runat="server" Text=""></asp:Label> <br>
          Member </strong><asp:Label ID="lblMember" runat="server" Text=""></asp:Label><br></td>
        <td bgcolor="#EBEBEB"><strong>Duration of assessment </strong> <asp:Label ID="lblHour" runat="server" Text="-"></asp:Label> hr.</td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF"><strong>ผลการประเมินคุณภาพภายใน  (Internal Quality Assessment Report)</strong></td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF"><strong>สิ่งที่น่าชื่นชม ( Impression / Best practice/Innovation  ) <br>
         <asp:Label ID="lblImpression" runat="server" Text=""></asp:Label><br>
        </strong></td>
      </tr>
      
      <tr>
        <td colspan="2" bgcolor="#FFFFFF">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" Width="100%" EmptyDataText="��辺��¡��" Visible="False">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                       <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="top" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="มาตรฐาน">
                         <HeaderStyle Width="140px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="140px" />
                        <ItemTemplate>
                            
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("chapter")%>'></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("std_no")%>'></asp:Label>
                            /
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("measure_element_no")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เนื้อหา">
                        <ItemStyle VerticalAlign="Top" />
                        <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("assessment_me_id")%>' Visible="false"></asp:Label>
                              <asp:Label ID="lblIpadPK" runat="server" Text='<%# Bind("ipad_assessment_me_id")%>' Visible="false"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("me_comment_detail") %>'></asp:Label>
                            <br /><br /><asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
          
            

        </td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF"><strong>ข้อเสนอแนะเพื่อการพัฒนา  ( Partial Met / Recommendation for Improvement / Observation )</strong></td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="มาตรฐาน">
                         <HeaderStyle Width="140px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="140px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("chapter")%>'></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("std_no")%>'></asp:Label>
                            /
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("measure_element_no")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เนื้อหา">
                         <HeaderStyle Width="500px" />
                        <ItemStyle VerticalAlign="Top" Width="500px" />
                        <ItemTemplate>
                               <asp:Label ID="lblPK" runat="server" Text='<%# Bind("assessment_me_id")%>' Visible="false"></asp:Label>
                              <asp:Label ID="lblIpadPK" runat="server" Text='<%# Bind("ipad_assessment_me_id")%>' Visible="false"></asp:Label>
                         <asp:Label ID="Label7" runat="server" Text='<%# Bind("criteria") %>'></asp:Label><br />
                               <asp:Label ID="Label2" runat="server" Text='<%# Bind("me_comment_detail") %>'></asp:Label>
                            <br /><br /><asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="การดำเนินการแก้ไข (Corrective Action)">
                       
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ระยะเวลา (Timeframe)">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width ="200px" />
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
                    </td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF"><strong>สิ่งที่ต้องแก้ไขเพื่อให้เป็นไปตามมาตรฐาน ( Not met / NCR )</strong></td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF">
            
               <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="5" EnableModelValidation="True" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <HeaderStyle Width="30px" />
                           <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="มาตรฐาน">
                         <HeaderStyle Width="140px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  Width="140px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("chapter")%>'></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("std_no")%>'></asp:Label>
                            /
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("measure_element_no")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เนื้อหา">
                         <HeaderStyle Width="500px" />
                        <ItemStyle VerticalAlign="Top" Width="500px" />
                        <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("assessment_me_id")%>' Visible="false"></asp:Label>
                              <asp:Label ID="lblIpadPK" runat="server" Text='<%# Bind("ipad_assessment_me_id")%>' Visible="false"></asp:Label>
                             <asp:Label ID="Label7" runat="server" Text='<%# Bind("criteria") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("me_comment_detail") %>'></asp:Label>
                            <br /><br /><asp:Label ID="lblPicture" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="การดำเนินการแก้ไข (Corrective Action)">
                       
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ระยะเวลา (Timeframe)">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width ="200px" />
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
                  </td>
      </tr>
      

      <tr>
        <td colspan="2" bgcolor="#FFFFFF"><div align="right"><strong>Department  Responsibility</strong> : <strong>ลงชื่อ</strong> …………………………………………<strong>วันที่</strong>………………………………<br /></div>
        </div></td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF">TQM-01090-I-F-C-1211-Rev04</td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#FFFFFF">33 Sukhumvit 3, Bangkok 10110 Thailand Tel: +66 (0) 2667 1000 Fax: +66 (0) 2667 2525    www.bumrungrad.com</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td align="center"><table width="100%" border="0" cellspacing="0" cellpadding="5">
      <tr>
        <td width="50" align="center" valign="top" bgcolor="#FFFFFF">&nbsp;</td>
        </tr>
    </table></td>
  </tr>
</table>
         </form>
</body>
</html>

