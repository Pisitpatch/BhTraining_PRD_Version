<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_master.aspx.vb" Inherits="idp_idp_master" %>

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
<link rel="stylesheet" href="../js/tab_simple/example-print.css" type="text/css" media="print" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="data">
      <div id="header"><img src="../images/doc01.gif" alt="c" width="32" height="32"  />&nbsp;&nbsp;IDP Manage Master data</div>
          <div align="right"></div>
       
          <div class="tabber" id="mytabber1">
             <div class="tabbertab">
            <h2>
              <strong>Development</strong>
             Categories</h2>
            <asp:GridView ID="GridCategory" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="category_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK0" runat="server" Text='<%# Bind("category_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk0" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder0" runat="server" 
                                  Text='<%# Bind("category_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("category_name_th") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date"  />
                    
                     
                      <asp:BoundField DataField="create_by_name" HeaderText="By" />
                    
                     
                  </Columns>
              </asp:GridView>
           <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
               <% If GridCategory.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>Development Categories</strong></td>
                <td width="138" class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
              </tr>
               <% End If%>           
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_mastercategory" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td colspan="2" valign="top">
                 <asp:Button ID="cmdAddCat" runat="server" Text="Add Categories" />
                     <asp:Button ID="cmdDelCat" runat="server" Text="Delete" />
                      <asp:Button ID="cmdOrderCat" runat="server" Text="Save Order" />
                </td>
              </tr>
            </table>
            <br />
          </div>

          <div class="tabbertab">
            <h2>
             <strong>Development topic</strong>
            </h2>
            <fieldset>
            <legend>Filter</legend>
             <table width="100%">
            <tr>
            <td width="100">Category</td>
            <td><asp:DropDownList ID="txtfind_category" runat="server"  DataTextField="category_name_th" DataValueField="category_id" >
              </asp:DropDownList></td></tr>
            <tr>
            <td width="100">Department</td>
            <td><asp:DropDownList ID="txtfind_owner_dept" runat="server"  
                    DataTextField="owner_dept_name" DataValueField="owner_dept_id" >
              </asp:DropDownList></td></tr>
            <tr>
            <td width="100">Topic</td>
            <td>
                <asp:TextBox ID="txtfind_keyword" runat="server"></asp:TextBox>
                </td></tr>
            <tr>
            <td width="100">&nbsp;</td>
            <td>
                <asp:Button ID="cmdFindTopic" runat="server" Text="Search" />
                </td></tr>
            </table>
            </fieldset>
             <br />
              
              <br />
            <asp:GridView ID="GridTopic" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="topic_id" 
                  EnableModelValidation="True" AllowPaging="True" PageSize="30">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("topic_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" VerticalAlign="top" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder" runat="server" Text='<%# Bind("topic_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories">
                         <ItemStyle Width="240px" VerticalAlign="Top"/>
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("category_name_th") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Development Topics">
                           
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("topic_name_th") %>'></asp:Label>
                                <br /> <asp:Label ID="Label5" runat="server" Text='<%# Bind("topic_name_en") %>' ForeColor="green"></asp:Label>
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Department">
                      <ItemStyle VerticalAlign="Top" />
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("owner_dept_name") %>'></asp:Label>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="By">
                          <ItemTemplate>
                              <asp:Label ID="lblBy" runat="server" Text='<%# Bind("create_by_name") %>'></asp:Label><br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
            <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                <%If GridTopic.Rows.Count = 0 Then%>
              <tr>
                <td width="33" class="colname">&nbsp;</td>
                <td width="33" class="colname"><strong>No</strong></td>
                <td width="240" class="colname"><strong>Development Categories </strong></td>
                <td  class="colname"><strong><strong><strong>Topics</strong></strong></strong></td>
                <td  class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
                </tr>
                <% End If%>
             <tr>
                <td width="33" valign="top">&nbsp;</td>
                <td width="33" valign="top">&nbsp;</td>
                <td width="240" valign="top">Choose Category</td>
                <td valign="top"><asp:DropDownList ID="txtadd_category" runat="server"  
                        DataTextField="category_name_th" DataValueField="category_id" 
                        AutoPostBack="false">
                  
                    </asp:DropDownList></td>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">Topic name (TH)</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_topic" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td colspan="2" valign="top">   
                    &nbsp;</td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                <td valign="top">Topic name (EN)</td>
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
                <td valign="top">&nbsp;</td>
                <td colspan="2" valign="top">   
                <asp:Button ID="cmdAddTopic" runat="server" Text="Add Topic" />
                     <asp:Button ID="cmdDelTopic" runat="server" Text="Delete" />
                      <asp:Button ID="cmdSaveOrder" runat="server" Text="Save Order" /></td>
                </tr>
            </table>
            </div>
       
          
          <div class="tabbertab">
            <h2>
              <strong>Expected outcomes</strong>
            </h2><asp:GridView ID="GridExpect" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="expect_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                   <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK1" runat="server" Text='<%# Bind("expect_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk1" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder1" runat="server" 
                                  Text='<%# Bind("expect_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Expect outcomes">
                         <ItemStyle  />
                          <ItemTemplate>
                                                   
                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("expect_detail") %>'></asp:Label><br />
                               <asp:Label ID="Label6" runat="server" Text='<%# Bind("expect_detail_en") %>' ForeColor="Green"></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date" ItemStyle-Width="120px"   />              
                      <asp:BoundField DataField="create_by_name" HeaderText="By" ItemStyle-Width="200px" />                                       
                  </Columns>
              </asp:GridView>
          <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
              <%If GridExpect.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>Expected outcomes</strong></td>
                <td width="138" class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
              </tr>
              <%End If%>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top">
                 Expect outcomes (th)
                  <asp:TextBox
                      ID="txtadd_expect" runat="server" Width="300px"></asp:TextBox>
                 </td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"> Expect outcomes (en)
                  <asp:TextBox
                      ID="txtadd_expect_en" runat="server" Width="300px"></asp:TextBox>
                 </td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"> &nbsp;</td>
                <td colspan="2" valign="top"> <asp:Button ID="cmdAddExpect" runat="server" Text="Add" />
                     <asp:Button ID="cmdDelExpect" runat="server" Text="Delete" />
                      <asp:Button ID="cmdOrderExpect" runat="server" Text="Save Order" /></td>
                </tr>
            </table>
            <br />
      </div>

          <div class="tabbertab">
            <h2>
              <strong>Methodogy</strong>
            </h2><asp:GridView ID="GridMethod" runat="server" CssClass="tdata" 
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="method_id" 
                  EnableModelValidation="True">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                   <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK1" runat="server" Text='<%# Bind("method_id") %>' 
                                  Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk1" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="No">
                         <ItemStyle Width="33px" />
                          <ItemTemplate>
                              <asp:textbox ID="txtorder1" runat="server" 
                                  Text='<%# Bind("method_order_sort") %>' Width="25px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Methodogy">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                           
                            
                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("method_name") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date"  />              
                      <asp:BoundField DataField="create_by_name" HeaderText="By" />                                       
                  </Columns>
              </asp:GridView>
          <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
              <%If GridExpect.Rows.Count = 0 Then%>
              <tr>
                <td width="30" class="colname">&nbsp;</td>
                <td width="30" class="colname"><strong>No</strong></td>
                <td class="colname"><strong>Expected outcomes</strong></td>
                <td width="138" class="colname"><strong>Date / Time</strong></td>
                <td width="216" class="colname"><strong>By</strong></td>
              </tr>
              <%End If%>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"><span style="font-weight: bold">
                 
                  <asp:TextBox
                      ID="txtadd_method" runat="server" Width="350px"></asp:TextBox>
                  </span></td>
                <td colspan="2" valign="top"> <asp:Button ID="cmdAddMethod" runat="server" 
                        Text="Add" />
                     <asp:Button ID="cmdDeleteMethod" runat="server" Text="Delete" />
                      <asp:Button ID="cmdSaveMethod" runat="server" Text="Save Order" /></td>
                </tr>
            </table>
            <br />
      </div>
          </div>
        </div>
</asp:Content>

