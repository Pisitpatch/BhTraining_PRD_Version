<%@ Page Title="" Language="VB" MasterPageFile="~/game/Game_MasterPage.master" AutoEventWireup="false" CodeFile="admin_answer_edit.aspx.vb" Inherits="game_admin_answer_edit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
   <table class="box-header" width="100%">
        <tr>
            
            <td>
                <img  height="24" src="../images/web.png" width="24" alt="web" />&nbsp;&nbsp;
                <a href="admin_test_master.aspx">Main</a> 
                <asp:Label ID="lblPathWay" runat="server" Text="Label"></asp:Label>
                </td>
            <td align="right" style="padding-right: 15px;">
                &nbsp;</td>
        </tr>
    </table>
 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="box-content"
        Width="100%" EnableModelValidation="True" BorderColor="#000CCC">
        <Columns>
            <asp:TemplateField HeaderText="">
                <ItemStyle HorizontalAlign="Center" Width="30px" />
                <ItemTemplate>
                   
                    <asp:imageButton ID="ImageButton2" runat="server" ImageUrl="../images/page_delete.png" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandName="onDeleteTest" CommandArgument='<%# Eval("answer_id") %>'  />
                  
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField HeaderText="No.">
                                  <ItemStyle HorizontalAlign="Center" Width="30px" />
               <ItemTemplate>
                <asp:Label ID="Labels" runat="server" >
                 <%# Container.DataItemIndex + 1 %>.
                </asp:Label>
            </ItemTemplate>
                              </asp:TemplateField>
            <asp:TemplateField HeaderText="Correct Answer">
              <ItemStyle Width="50px" />
                <ItemTemplate>
                    <asp:Button ID="cmdCorrect" runat="server" Text="Correct Answer" CommandArgument='<%# Eval("answer_id") %>' OnCommand="onChangeCorrect" />
                   
                      <asp:Label ID="lblPK" runat="server" Text='<%# Bind("answer_id") %>' Visible="false"></asp:Label>
                 <asp:Label ID="lblCorrect" runat="server" Text='<%# Bind("is_correct") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Answer Detail (TH)">
                <ItemTemplate>
                   
                    <asp:Textbox ID="txtanswer_th" runat="server" Text='<%# Bind("answer_detail_th") %>' Width="90%" Rows="5" TextMode="MultiLine"></asp:Textbox>
                   
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Answer Detail (EN)" >
                <ItemTemplate>
                    <asp:Textbox ID="txtanswer_en" runat="server" text='<%# Bind("answer_detail_en") %>' Width="90%" Rows="5" TextMode="MultiLine"></asp:Textbox>
                </ItemTemplate>
             
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Answer Picture">
                
                <ItemTemplate>
                
                  <img src='../share/game/answer/<%# Eval("file_path") %>' alt="Action Words" width="150" />
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
    </asp:GridView>
<table width="100%" class="box-content">
        <tr>
          <td width="180" valign="top" class="rowname">รายละเอียดคำตอบ (TH) :</td>
          <td valign="top"><label for="textarea"></label>
          <textarea name="txtanswer_th" id="txtanswer_th" cols="45" rows="3" style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">รายละเอียดคำตอบ (EN) :</td>
          <td valign="top">
          <textarea name="txtanswer_en" id="txtanswer_en" cols="45" rows="3" 
                  style="width: 635px;" runat="server"></textarea></td>
        </tr>
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
          <td valign="top" class="rowname">&nbsp;</td>
          <td valign="top">
              <asp:Button ID="cmdSave" runat="server" Text="Add" CssClass="green" />
              <asp:Button ID="cmdCancel" runat="server" Text="Back" CssClass="green" />
          &nbsp;<asp:Button ID="cmdEdit" runat="server" Text="Update Answer" CssClass="green" />
            </td>
        </tr>
    </table>
</asp:Content>
