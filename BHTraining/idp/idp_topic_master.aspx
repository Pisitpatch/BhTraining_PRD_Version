<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_topic_master.aspx.vb" Inherits="idp_idp_topic_master" %>

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
<link rel="stylesheet" href="../js/tab_simple/example.css" TYPE="text/css" MEDIA="screen" />
<link rel="stylesheet" href="../js/tab_simple/example-print.css" TYPE="text/css" MEDIA="print" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
      <table width="100%" ><tr>
      <td><img src="../images/doc01.gif" alt="IDP" width="32" height="32"  />&nbsp;&nbsp;<span class="style1">Manage Deveopment Topic</span> </td>
      <td>
      <div align="right" id="div_status" runat="server"> 

        </div></td>
      </tr></table>
      

      </div>
      <div id="data">
    <div class="tabber" id="mytabber1">
  <div class="tabbertab">
            <h2>
             <strong>Development topic</strong>
            </h2>
            <asp:GridView ID="GridTopic" runat="server" CssClass="tdata"  CellPadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="topic_id" 
                  EnableModelValidation="True" AllowPaging="True" PageSize="30" 
                EmptyDataText="There is no data.">
                  <HeaderStyle BackColor="#CCCCCC" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                      <HeaderStyle Width="33px" HorizontalAlign="Center" />
                     <ItemStyle Width="33px" HorizontalAlign="Center" />
                         <HeaderTemplate>
                          <asp:CheckBox ID="HeaderLevelCheckBox" runat="server" OnCheckedChanged="onSelectAll" AutoPostBack="true" />
                         </HeaderTemplate>
                          <ItemTemplate><asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" VerticalAlign="top" />
                          <ItemTemplate>
                          <asp:Label ID="lblPK" runat="server" Text='<%# Bind("topic_id") %>' Visible="false"></asp:Label>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("topic_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories">
                         <ItemStyle Width="120px" VerticalAlign="top"  />
                          <ItemTemplate>                          
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("category_name_th") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="For Department">
                           
                            <ItemTemplate>
                                <asp:Label ID="lblDeptList" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Development Topics">
                           <ItemStyle VerticalAlign="top" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("topic_name_th") %>'></asp:Label><br />
                                <asp:Label ID="lblNameEN" runat="server" Text='<%# Bind("topic_name_en") %>' ForeColor="Green"></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="By">
                      <ItemStyle Width="200px" VerticalAlign="top" />
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("create_by_name") %>'></asp:Label><br />
                               <asp:Label ID="Label4" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                    
                     
                  </Columns>
                  <PagerSettings Position="Top" />
              </asp:GridView>
            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                <%If GridTopic.Rows.Count = 0 Then%>
              <tr>
                <td width="33" class="colname">&nbsp;</td>
                <td width="33" class="colname"><strong>No</strong></td>
                <td width="192" class="colname"><strong>Development Categories </strong></td>
                <td width="369" class="colname"><strong><strong><strong>Topics</strong></strong></strong></td>
                <td width="138" class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
                </tr>
                <% End If%>
             <tr>
                <td width="33" valign="top">&nbsp;</td>
                <td width="33" valign="top">&nbsp;</td>
                <td width="192" valign="top">Category</td>
                <td valign="top"><asp:DropDownList ID="txtadd_category" runat="server" Width="130px" >
                  <asp:ListItem Value="4">Unit Mandatory</asp:ListItem>
                    </asp:DropDownList></td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">Department</td>
                <td valign="top">
                    <asp:ListBox ID="txtdept" runat="server" DataTextField="dept_name_en" 
                        DataValueField="dept_id" SelectionMode="Multiple"></asp:ListBox>
                  </td>
                <td colspan="2" valign="top">   
                    &nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">Topic (TH)</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_topic" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td colspan="2" valign="top">   
                    &nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">Topic (EN)</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_topic_en" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td colspan="2" valign="top">   
                    &nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">   
                <asp:Button ID="cmdAddTopic" runat="server" Text="Add Topic" />
                     <asp:Button ID="cmdDelTopic" runat="server" Text="Delete" />
                      <asp:Button ID="cmdSaveOrder" runat="server" Text="Save Order" /></td>
                <td colspan="2" valign="top">   
                    &nbsp;</td>
                </tr>
            </table>
            </div>
</div>
        </div>
</asp:Content>

