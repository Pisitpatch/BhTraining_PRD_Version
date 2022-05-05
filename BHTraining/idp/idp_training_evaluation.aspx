<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_training_evaluation.aspx.vb" Inherits="idp_idp_training_evaluation" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

     /* Optional: Temporarily hide the "tabber" class so it does not "flash"
     on the page as plain HTML. After tabber runs, the class is changed
     to "tabberlive" and it will appear. */

     document.write('<style type="text/css">.tabber{display:none;}<\/style>');

     /*==================================================
     Set the tabber options (must do this before including tabber.js)
     ==================================================*/
     var tabberOptions = {

         'cookie': "tabber", /* Name to use for the cookie */

         'onLoad': function (argsObj) {
             var t = argsObj.tabber;
             var i;

             /* Optional: Add the id of the tabber to the cookie name to allow
             for multiple tabber interfaces on the site.  If you have
             multiple tabber interfaces (even on different pages) I suggest
             setting a unique id on each one, to avoid having the cookie set
             the wrong tab.
             */
             if (t.id) {
                 t.cookie = t.id + t.cookie;
             }

             /* If a cookie was previously set, restore the active tab */
             i = parseInt(getCookie(t.cookie));
             if (isNaN(i)) { return; }
             t.tabShow(i);
             // alert('getCookie(' + t.cookie + ') = ' + i);
         },

         'onClick': function (argsObj) {
             var c = argsObj.tabber.cookie;
             var i = argsObj.index;
             // alert('setCookie(' + c + ',' + i + ')');
             setCookie(c, i);
         }
     };

     /*==================================================
     Cookie functions
     ==================================================*/
     function setCookie(name, value, expires, path, domain, secure) {
         document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
     }

     function getCookie(name) {
         var dc = document.cookie;
         var prefix = name + "=";
         var begin = dc.indexOf("; " + prefix);
         if (begin == -1) {
             begin = dc.indexOf(prefix);
             if (begin != 0) return null;
         } else {
             begin += 2;
         }
         var end = document.cookie.indexOf(";", begin);
         if (end == -1) {
             end = dc.length;
         }
         return unescape(dc.substring(begin + prefix.length, end));
     }
     function deleteCookie(name, path, domain) {
         if (getCookie(name)) {
             document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
         }
     }

</script>
<script type="text/javascript" src="../js/tab_simple/tabber.js"></script>
<link rel="stylesheet" href="../js/tab_simple/example.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header"><img src="../images/doc01.gif" width="32" height="32" alt="icon"  />&nbsp;&nbsp;Training Evaluation Form</div>
<div id="data">
        <div class="tabber" id="mytabber2">
          <div class="tabbertab">
            <h2>Training Information</h2>
            <table width="100%" cellspacing="1" cellpadding="2" style="margin: 8px 10px;">
                <tr>
                  <td valign="top"><span class="theader"><strong>Training Course</strong></span></td>
                  <td><strong><asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                    
                      </strong>
                      </td>
                </tr>
                <tr>
                  <td width="150" valign="top"><strong>Date</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180">
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                        <td width="60"><strong>Location</strong></td>
                        <td width="230"><asp:Label ID="lblLocation" runat="server" Text=""></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td valign="top"><strong>Speaker</strong></td>
                  <td><table width="100%" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="180"><asp:Label ID="lblSpeaker" runat="server" Text=""></asp:Label></td>
                        <td width="60">&nbsp;</td>
                        <td width="230">&nbsp;</td>
                      </tr>
                  </table></td>
                </tr>
              </table>
          </div>
          </div>
       
      <div class="tabber" id="Div1">
          <div class="tabbertab">
            <h2>Training Evaluation Form</h2>
            <div style="text-align:right; width:100%"><asp:Button ID="cmdSave1" runat="server" Text="Save" Width="100px" OnClientClick="return confirm('Are you sure you want to summit? ');" /></div>
            <br />
             <asp:GridView ID="GridView1" runat="server" 
              AutoGenerateColumns="False"  Width="98%" CellPadding="1" 
               GridLines="None" CssClass="tdata3" PageSize="100" RowHeaderColumn="position" 
              AllowSorting="True" EmptyDataText="There is no data." 
        EnableModelValidation="True">
            
              <Columns>
                
                  <asp:TemplateField HeaderText="เรื่องที่ประเมิน / Topic">
                  <ItemStyle VerticalAlign="Top" />
                      <ItemTemplate>
                        <asp:Label ID="lblHeaderHTML0" runat="server" Text=""></asp:Label>
                   <asp:Label ID="lblPK" runat="server" Text='<%# Bind("pk") %>' Visible="false"></asp:Label>
                         <asp:Label ID="Label2" runat="server" Text='<%# Eval("topic_name_th") %>'></asp:Label> / 
                          <br />
                          <asp:Label ID="Label1" runat="server" Text='<%# Eval("topic_name_en") %>' ForeColor="#0033CC"></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle ForeColor="White" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="ระดับความคิดเห็น /Description">
                    
                      <ItemTemplate>
                        <ItemStyle VerticalAlign="top" />
                          <asp:Label ID="lblHeaderHTML" runat="server" Text=""></asp:Label>
                          <asp:Label ID="lblCategory" runat="server" text='<%# Eval("category_name_th") %>' Visible="false" ></asp:Label>

                           <asp:Label ID="lblCategory2" runat="server" text='<%# Eval("category_name_en")%>' Visible="false" ></asp:Label>
                          <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" >
                          <asp:ListItem Value="5">5 Excellent</asp:ListItem>
                          <asp:ListItem Value="4">4 Good&nbsp;&nbsp;</asp:ListItem>
                          <asp:ListItem Value="3">3 Average</asp:ListItem>
                          <asp:ListItem Value="2">2 Below average</asp:ListItem>
                          <asp:ListItem Value="1">1 Needs improvement</asp:ListItem>
                          </asp:RadioButtonList>
                      
                
                      </ItemTemplate>
                  </asp:TemplateField>
                
               
              </Columns>
              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#ABBBB4" ForeColor="White" HorizontalAlign="Center" 
                  BorderStyle="None" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#abbbb4" Font-Bold="True" ForeColor="White" 
                  CssClass="theader" />
              
              <EditRowStyle BackColor="#2461BF" />
              
              
          </asp:GridView>
          <br />
          <table width="100%">
          <tr>
          <td><strong>รูปแบบการถ่ายทอดความรู้แบบใด ที่ท่านคิดว่ามีประสิทธิภาพสำหรับหลักสูตรนี้ เพราะเหตุใด / Which methodology in delivering would suit best for this course ?    
              </strong>    </td>
          </tr>
          <tr>
          <td>
          <table width="100%">
          <tr>
          <td width="50%">
              <asp:CheckBox ID="txtc1" runat="server" Text=" บรรยาย / Lecture" />
              </td>
          <td>
              <asp:CheckBox ID="txtc2" runat="server" Text=" การสอนงาน / Coaching" />
              </td>
          </tr>
          <tr>
          <td width="50%">
              <asp:CheckBox ID="txtc3" runat="server" Text=" ประชุมเชิงปฎิบัติการ / Workshop" />
              </td>
          <td>
              <asp:CheckBox ID="txtc4" runat="server" Text=" นำเสนอโดยกลุ่มงาน / Group presentation" />
              </td>
          </tr>
          <tr>
          <td width="50%">
              <asp:CheckBox ID="txtc5" runat="server" Text=" บทบาทสมมุติ / Rol play" />
              </td>
          <td>
              <asp:CheckBox ID="txtc6" runat="server" Text=" อื่นๆ / Other" />
              </td>
          </tr>
          
          </table>
          </td>
          </tr>
          <tr>
          <td><strong>สิ่งที่สำคัญที่ท่านได้เรียนรู้จากการอบรมครั้งนี้ / My key learning areas where    
              </strong>    </td>
          </tr>
          <tr>
          <td>1.
              <asp:TextBox ID="txtkey1" runat="server" Width="500px"></asp:TextBox>
              </td>
          </tr>
          <tr>
          <td>2. 
              <asp:TextBox ID="txtkey2" runat="server" Width="500px"></asp:TextBox>
              </td>
          </tr>
          <tr>
          <td>3.    
              <asp:TextBox ID="txtkey3" runat="server" Width="500px"></asp:TextBox>
              </td>
          </tr>
          <tr>
          <td><strong>ข้อเสนอแนะสำหรับวิทยากร / Suggestion for speaker&nbsp; </strong> </td>
          </tr>
          <tr>
          <td>
              <asp:TextBox ID="txtdetail_speaker" runat="server" Width="90%" Rows="5" 
                  TextMode="MultiLine" ></asp:TextBox>
              </td>
          </tr>
          <tr>
          <td>
              <strong>ข้อเสนอแนะสำหรับการอบรมนี้ / Suggestion for thsi course&nbsp; </strong> 
              </td>
          </tr>
          <tr>
          <td>
              <asp:TextBox ID="txtdetail_course" runat="server" Width="90%" Rows="5" 
                  TextMode="MultiLine" ></asp:TextBox>
              </td>
          </tr>
          <tr>
          <td>
              <strong>หากมีการจัดอบรมซ้ำ ท่านจะแนะนำให้เพื่อนร่วมงานเข้าร่วมในการอบรมหรือไม่ / 
              In case this training is held again, will you recommend it to your colleagues?</strong></td>
          </tr>
          <tr>
          <td>
              <asp:CheckBox ID="txtr1" runat="server" Text=" แนะนำ / Yes" />
              &nbsp;<asp:CheckBox ID="txtr2" runat="server" Text=" ไม่แนะนำ / No" />
              &nbsp;<asp:CheckBox ID="txtr3" runat="server" Text=" ไม่แน่ใจ / Not sure" />
              </td>
          </tr>
          </table>

          <br />
          <div style="text-align:right; width:100%"><asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px" OnClientClick="return confirm('Are you sure you want to summit? ');" /></div>
          
          </div>
          </div>
      </div>
    
</asp:Content>


