<%@ Page Language="VB" AutoEventWireup="false" CodeFile="srp_shop_reserve.aspx.vb" Inherits="srp_srp_shop_reserve" %>
<%@ Import Namespace="ShareFunction" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>จองสินค้า</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            height: 30px;
        }
        .auto-style1 {
            height: 25px;
        }
    </style>
</head>
<body onunload="window.opener.disablePopup()">
    <form id="form1" runat="server">
    <div id="data">
    <asp:GridView ID="gridview1" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" EnableModelValidation="True" 
                     EmptyDataText="There is no data." AllowPaging="True" 
        ShowFooter="True" DataKeyNames="reserve_id" Visible="False">
                  <HeaderStyle BackColor="#11720c" ForeColor="White" />
                  <FooterStyle  BackColor="#EEEEEE"  />
                  <RowStyle VerticalAlign="Top" />
                  <AlternatingRowStyle BackColor="#eef1f3" />
                  <Columns>
                      <asp:TemplateField HeaderText="วันที่จอง">
                         <ItemStyle Width="150px" HorizontalAlign="Center" />
                          <ItemTemplate>
                              <asp:Label ID="lblDate" runat="server" Text='<%# ConvertTSToDateString(Eval("reserve_date_ts")) %>'></asp:Label>
                               <asp:Label ID="Label1" runat="server" Text='<%# ConvertTSTo(Eval("reserve_date_ts"),"hour") %>'></asp:Label>:
                                <asp:Label ID="Label2" runat="server" Text='<%# ConvertTSTo(Eval("reserve_date_ts"),"min") %>'></asp:Label>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("reserve_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:Label ID="lblEmpNo" runat="server" Text='<%# Bind("emp_code") %>' 
                                  Visible="false"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="emp_name" HeaderText="ชื่อผู้จอง" >
                     
                      </asp:BoundField>
                      <asp:TemplateField HeaderText="จำนวน">
                        
                          <ItemTemplate>
                          
                           
                              <asp:Label ID="lblTopicName" runat="server" Text='<%# Bind("reserve_qty") %>'></asp:Label>
                             
                       
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="หมายเหตุ" Visible="False">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("reserve_remark") %>' ></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>

                       <asp:TemplateField ShowHeader="False">
                                  <ItemTemplate>
                                      <asp:Label ID="lblPK1" runat="server" Text='<%# Bind("reserve_id") %>' Visible="false"></asp:Label>
                                      <asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" ForeColor="Red"
                                          CommandName="Delete" Text="ลบรายการจอง"></asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="80px" />
                              </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              <br />
    <fieldset>
  <legend></legend><table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
  <tr>
    <td width="385" valign="top">
        <asp:Label ID="lblNameTH" 
            runat="server" Text="" Font-Bold="true"></asp:Label>
      </td>
    </tr>
  <tr>
    <td valign="top" class="style2">&nbsp;
        &nbsp;</td>
    </tr>
  <tr>
    <td valign="top">
        <fieldset>
            <legend>คำนวนยอดเงินที่ต้องชำระ</legend>
            <table width="100%">
                <tr><td width="120" class="style2">ชื่อสินค้า</td>
                    <td class="style2" colspan="6">
                        <asp:Label ID="lblName" runat="server" ForeColor="#000099" Text="Label"></asp:Label>
                    </td>
                </tr>

                <tr><td  class="style2"><asp:Label ID="lblCFBcomment1" 
            runat="server" Text="จำนวนที่ต้องการ"></asp:Label></td>
                    <td width="60" class="style2">
        <asp:DropDownList ID="txtqty" runat="server" AutoPostBack="True">
        <asp:ListItem Value="1">1</asp:ListItem>
        <asp:ListItem Value="2">2</asp:ListItem>
        <asp:ListItem Value="3">3</asp:ListItem>
        <asp:ListItem Value="4">4</asp:ListItem>
      <asp:ListItem Value="5">5</asp:ListItem>
        </asp:DropDownList>
                    </td>
                    <td width="50" class="style2">&nbsp;</td>
                     <td class="style2">
                         &nbsp;</td>
                      <td width="100" class="style2">&nbsp;</td>
                     <td width="50" class="style2">
                         &nbsp;</td>
                     <td class="style2">&nbsp;</td>
                </tr>

                <tr><td width="80" class="style2">ใช้คะแนน</td>
                    <td width="60" class="style2">
                        <asp:TextBox ID="txtpoint" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td width="50" class="style2">Point(s)</td>
                     <td class="style2">
                         <asp:Button ID="cmdCalPoint" runat="server" Text="คำนวนคะแนน" Width="130px" />
                    </td>
                      <td width="100" class="style2">ต้องการคะแนน</td>
                     <td width="50" class="style2">
                         <asp:Label ID="lblTotalScore" runat="server" Text="0" Font-Bold="True" ForeColor="#000099"></asp:Label>
                    </td>
                     <td class="style2">คะแนน / 1 รายการ</td>
                </tr>

                <tr><td width="80" class="auto-style1">ใช้เงินสด</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtbaht" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td class="auto-style1">Baht(s)</td>
                     <td class="auto-style1">
                         <asp:Button ID="cmdCalBaht" runat="server" Text="จ่ายด้วยเงินอย่างเดียว" Width="130px" />
                    </td>
                      <td class="auto-style1">หรือเงินสด</td>
                     <td class="auto-style1">
                         <asp:Label ID="lblTotalCash" runat="server" Text="0" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                    </td>
                     <td class="auto-style1">บาท / 1 รายการ</td>
                </tr>

            </table>
        </fieldset></td>
  </tr>
      <!-- 
  <tr>
    <td valign="top">
        เบอร์โทรศัพท์ติดต่อกลับ 
        <asp:TextBox ID="txttel" runat="server" MaxLength="30" Visible="False"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td valign="top"><asp:Label ID="lblCFBcomment2" 
            runat="server" Text="หมายเหตุ"></asp:Label>
        </td>
    </tr>
  <tr>
    <td valign="top">
      <textarea name="textarea2" id="txtsolution" cols="45" rows="3" style="width: 685px;" runat="server" visible="False"></textarea></td>
  </tr>
          -->
  <tr>
    <td valign="top">
        <asp:Button ID="cmdSave" runat="server" Text="Submit" Visible="False" />
    &nbsp;<input id="cmdClose" type="button" value="Close" onclick="window.close();" /></td>
  </tr>
  <tr>
    <td valign="top">
       
      </td>
  </tr>
  </table>
</fieldset>
    </div>
        <br />
       <table width="800"><tr><td align="center" style="text-align:center ; color:green ; font-size:16px">
             <strong>หากท่านสนใจกรุณาติดต่อแลกของรางวัลที่เคาน์เตอร์โครงการเชิดชูเกียรติอีกครั้ง<br />
             ในวันจันทร์ พุธ ศุกร์ เวลา 08.30-15.00
            <br />
            ไม่มีพักเบรคเที่ยง

             <br />

             <br />
                 Please submit card(s) at the Staff Recognition Program counter to get reward on Monday, Wednesday, Friday from 08.30-15.00, no lunch break.  
            </strong>
             <br />

            <br />
            <img src="../images/noo_Bumrung2.gif" width="150" /></td></tr></table>
       
           
    </form>
</body>
</html>
