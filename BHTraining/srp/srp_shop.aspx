<%@ Page Title="" Language="VB" MasterPageFile="~/srp/SRP_MasterPage.master" AutoEventWireup="false" CodeFile="srp_shop.aspx.vb" Inherits="srp_srp_shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
/* Picture Thumbnails */
#thumbnails ul
{
    width: 800px;
    list-style: none;
}
#thumbnails li
{
    text-align: center;
    display: inline;
    width: 200px;
    height: 320px;
    float: left;
    margin-bottom: 20px;
}
        </style>
<script type="text/javascript">
    function openDetail(id) {
        loadPopup(1);
        my_window = window.open('srp_shop_reserve.aspx?id=' + id, 'popupFile', 'alwaysRaised,scrollbars =yes,width=800,height=750');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="data">
    <table width="100%" cellpadding="2" cellspacing="1">
  <tr>
    <td><div id="header"><img src="../images/srp_logo_32.png" alt="1" width="32" height="32"  />&nbsp;&nbsp;<strong>Staff Recognition Program : Online Catalog</strong></div></td>
  </tr>
</table>
  <table width="100%" cellpadding="3" cellspacing="0" class="tdata3">
    <tr>
      <td colspan="2" class="theader" style="background-color:#11720c"><img src="../images/sidemenu_circle.jpg" width="10" height="10" alt="x" />&nbsp;Search</td>
    </tr>
    <tr>
      <td height="30" width="150"><strong>ประเภทสินค้า</strong></td>
      <td>
          <asp:DropDownList ID="txttype" runat="server">
          <asp:ListItem Value = "0">- ทุกประเภท -</asp:ListItem>
          <asp:ListItem Value = "OTS">On the Spot (ใช้การ์ด OTS แลกของ)</asp:ListItem>
           <asp:ListItem Value = "SRP">Staff Recognition Point (ใช้คะแนนหรือเงินสดแลกของ)</asp:ListItem>
          </asp:DropDownList>
        </td>
    </tr>
   
    <tr>
      <td height="30" width="150"><strong>ชื่อสินค้า</strong></td>
      <td>
          <asp:TextBox ID="txtname" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
   
    <tr>
      <td height="30" colspan="2">
          <asp:Button ID="cmdSearch" runat="server" Text="Search" 
              CausesValidation="False" />
        </td>
    </tr>
  </table>
  <br />
<ul id="thumbnails">
    <asp:ListView runat="server" ID="PicturesListView" ItemPlaceholderID="PicturesListItemPlaceholder"
       >
        <LayoutTemplate>
            <li runat="server" id="PicturesListItemPlaceholder"></li>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <a href='srp_shop_picture.aspx?inv_id=<%# Eval("inv_item_id") %>' target="_blank" class="thickbox" rel="gallery-test"
                    title='<%# Eval("inv_remark") %>'>
                    <img src="srp_show_image.ashx?id=<%# Eval("inv_item_id") %>" alt='<%# Eval("inv_item_name_th") %>' width="150" height="150"  border="0" />
                </a>
                <br /> <span style="color:blue"><%# Eval("inv_item_name_th") %></span>
                <br />Point : [<%# FormatNumber(Eval("total_point"),0) %>] 
                <br />Price : [<%# FormatNumber(Eval("total_price"),0) %>Baht]
                <br />คงเหลือ : [<%# FormatNumber(Eval("on_hand"), 0)%>]
                 <br /><a href="javascript:;"  onclick="openDetail(<%# Eval("inv_item_id") %>)"><span style="font-weight:bold ; font-size:15px; text-decoration:underline"><%# Eval("is_voucher") %></span></a>
                 <br /> <%# Eval("inv_remark") %>
                </li>
        </ItemTemplate>
    </asp:ListView>
   
</ul>
   
    <br />
    <br />
    <div style="clear:both">
    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="PicturesListView" 
            PageSize="20">
        <Fields>
            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                <asp:NumericPagerField />
            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                ShowNextPageButton="False" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>
    </div>
    </div>
</asp:Content>

