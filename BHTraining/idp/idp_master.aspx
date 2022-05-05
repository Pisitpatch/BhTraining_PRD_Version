<%@ Page Title="" Language="VB" MasterPageFile="~/idp/IDP_MasterPage.master" AutoEventWireup="false" CodeFile="idp_master.aspx.vb" Inherits="idp_idp_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Import Namespace="ShareFunction" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

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
<script type="text/javascript">
    var global_time;
    // =========== Check Session ===========================
    checkSession('<%response.write(session("bh_username").toString) %>', '<%response.write(viewtype) %>', ''); // Check session every 1 sec.
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="data">
      <div id="header"><img src="../images/doc01.gif" alt="c" width="32" height="32"  />&nbsp;&nbsp;
          <asp:Label ID="lblHeader" runat="server" Text="IDP Manage Master data"></asp:Label>
      </div>
          <div align="right"></div>
       
          <div class="tabber" id="mytabber1">
             <div class="tabbertab" id="tab_category" runat="server">
            <h2>
             <asp:Label ID="lblCategory" runat="server" Text="Category Management"></asp:Label></h2>
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
                      <asp:BoundField DataField="status" HeaderText="Status" HtmlEncode="False" />
                      <asp:TemplateField HeaderText="Categories (th)">
                         <ItemStyle Width="250px" />
                          <ItemTemplate>
                           
                            
                              <asp:textbox ID="txtname_th" runat="server" Text='<%# Bind("category_name_th") %>' Width="200px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Categories (en)">
                           <ItemStyle Width="250px" />
                          <ItemTemplate>
                                <asp:textbox ID="txtname_en" runat="server" Text='<%# Bind("category_name_en") %>' Width="200px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Type">
                         
                          <ItemTemplate>
                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("category_type") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date"  />
                    
                     
                      <asp:BoundField DataField="create_by_name" HeaderText="Create by" />
                    
                     
                  </Columns>
              </asp:GridView>
              <br />
           <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
                   
              <tr>
                <td  width="100" valign="top">Category&nbsp;</td>
                <td valign="top"><span style="font-weight: bold">
                    <asp:TextBox ID="txtadd_mastercategory" runat="server" Width="350px"></asp:TextBox> 
                    
                </span></td>
                <td valign="top">
               
                </td>
              </tr>
                   
              <tr>
                <td  width="100" valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:Button ID="cmdAddCat" runat="server" Text="Add Category" />
                    <asp:Button ID="cmdDelCat" runat="server" Text="Inactive" Width="100px" />
                      <asp:Button ID="cmdActiveCat" runat="server" Text="Active" Width="100px" />
                    <asp:Button ID="cmdOrderCat" runat="server" Text="Save" Width="100px" />
                  </td>
                <td valign="top">
                    &nbsp;</td>
              </tr>
            </table>
            <br />
          </div>
          <div class="tabbertab" id="tab_topic" runat="server">
            <h2>
             <strong>  <asp:Label ID="lblTopic" runat="server" Text="Topic Management"></asp:Label></strong>
            </h2>
            
                 <asp:GridView ID="GridMasterTopic" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="master_topic_id" 
                  EnableModelValidation="True" AllowPaging="True" PageSize="30">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("master_topic_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                 
                      <asp:TemplateField HeaderText="Status">
                         <ItemStyle Width="50px" VerticalAlign="Top" />
                          <ItemTemplate>
                              <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                              <asp:DropDownList ID="txtstatus" runat="server">
                              <asp:ListItem Value="1">Active</asp:ListItem>
                              <asp:ListItem Value="0">Inactive</asp:ListItem>
                              </asp:DropDownList>
                              <asp:Label ID="lblStatusId" runat="server" Text='<%# Bind("is_active") %>' Visible="false" ></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                 
                      <asp:TemplateField HeaderText="Topic (th)">
                         <ItemStyle Width="280px" VerticalAlign="Top"/>
                          <ItemTemplate>
                           
                            
                              <asp:textbox ID="txtname_th" runat="server" Text='<%# Bind("master_topic_name_th") %>'  Width="250px" ToolTip='<%# Bind("master_topic_name_th") %>'></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Topic (en)">
                           <ItemStyle Width="280px" VerticalAlign="Top"/>
                            <ItemTemplate>
                                <asp:textbox ID="txtname_en" runat="server" Text='<%# Bind("master_topic_name_en") %>' ToolTip='<%# Bind("master_topic_name_en") %>' Width="250px"></asp:textbox>
                                
                            </ItemTemplate>
                      </asp:TemplateField>

                     
                    
                    

                     
                    
                      <asp:TemplateField HeaderText="Create by">
                          <ItemTemplate>
                              <asp:Label ID="lblBy" runat="server" Text='<%# Bind("topic_create_by") %>'></asp:Label>
                            <asp:Label ID="lblDeptId" runat="server" Text='<%# Bind("owner_dept_id") %>' Visible="false"></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                      <asp:TemplateField HeaderText="Department">
                          <ItemTemplate>
                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("owner_dept_name") %>'></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
              <Br />
              <table width="100%">
              <tr><td width="150">Topic name (th)</td>
              <td>
                  <asp:TextBox ID="txtmaster_topic_th" runat="server" Width="550px"></asp:TextBox>
                  </td>
              </tr>
              <tr><td width="150">Topic name (en)</td>
              <td>
                  <asp:TextBox ID="txtmaster_topic_en" runat="server" Width="550px"></asp:TextBox>
                  </td>
              </tr>
             
              <tr><td width="150">&nbsp;</td>
              <td>
                  <asp:Button ID="cmdAddMasterTopic" runat="server" Text="Add new topic" />
                  &nbsp;<asp:Button ID="cmdSaveTopic" runat="server" Text="Save" Width="100px"  />
                  &nbsp;<asp:Button ID="cmdDeleteTopic" runat="server" Text="Delete" 
                      ForeColor="Red" OnClientClick="return confirm('Are you sure you want to delete?')" 
                      Width="100px" style="height: 26px"  />
                  </td>
              </tr>
              </table>
            </div>
          <div class="tabbertab" id="tab_mapping" runat="server">
            <h2>
                <strong>Mapping Category and Topic</strong>
            </h2>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
            <fieldset id="searchBox" >
            <legend style="font-weight:bold">Search topic</legend>
             <table width="100%">
            <tr>
            <td width="100">Category</td>
            <td><asp:DropDownList ID="txtfind_category" runat="server"  DataTextField="category_name_en" DataValueField="category_id" Width="300px" >
              </asp:DropDownList></td></tr>
            <tr>
            <td width="100">Topic</td>
            <td>
                <asp:TextBox ID="txtfind_keyword" runat="server" Width="300px"></asp:TextBox>
                </td></tr>
                
            <tr>
            <td width="100">&nbsp;</td>
            <td>
                <asp:Button ID="cmdFindTopic" runat="server" Text="Search" />
                </td></tr>
            </table>
            </fieldset>
             <br />
         
             Found <asp:Label ID="lblNum2" runat="server" Text="Label"></asp:Label> record.
            <asp:GridView ID="GridTopic" runat="server" CssClass="tdata" cellpadding="3"
                  AutoGenerateColumns="False" Width="100%" DataKeyNames="topic_id" 
                  EnableModelValidation="True" AllowPaging="True" PageSize="30" 
                  EmptyDataText="There is no item.">
                  <HeaderStyle BackColor="#CBEDED" CssClass="colname" />
                  <AlternatingRowStyle BackColor="Azure" />
                  <Columns>
                      <asp:TemplateField>
                         <ItemStyle VerticalAlign="Top" />
                          <ItemTemplate>
                              <asp:Label ID="lblPK" runat="server" Text='<%# Bind("topic_id") %>' Visible="false"></asp:Label>
                              <asp:CheckBox ID="chk" runat="server" />
                          </ItemTemplate>
                          <ItemStyle Width="33px" />
                      </asp:TemplateField>
                    
                      <asp:TemplateField HeaderText="Categories">
                         <ItemStyle Width="240px" VerticalAlign="Top"/>
                          <ItemTemplate>
                              <asp:Label ID="lblunit" runat="server" Text='<%# Bind("is_unit_mandatory") %>' Visible="false"></asp:Label> 
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("category_id") %>' Visible="false"></asp:Label> 
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("category_name_th") %>' Font-Bold="true"></asp:Label>  <br />
                              (<asp:Label ID="Label2" runat="server" Text='<%# Bind("category_name_en") %>'></asp:Label>)
                          </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Topic">
                            <ItemStyle VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("topic_name_th") %>'></asp:Label>  <br />
                               (<asp:Label ID="Label8" runat="server" Text='<%# Bind("topic_name_en") %>'></asp:Label>)
                            </ItemTemplate>
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment (th)">
                      <ItemStyle  VerticalAlign="Top"/>
                          <ItemTemplate>
                              <asp:TextBox ID="txtcomment_th" runat="server" Text='<%# Bind("comment_th") %>' TextMode="MultiLine" Rows="3"></asp:TextBox>
                          </ItemTemplate>
                         
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Comment (en)">
                        <ItemStyle  VerticalAlign="Top"/>
                          <ItemTemplate>
                               <asp:TextBox ID="txtcomment_en" runat="server" Text='<%# Bind("comment_en") %>' TextMode="MultiLine" Rows="3"></asp:TextBox>
                          </ItemTemplate>
                       
                      </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unique Topic">
                          <ItemStyle VerticalAlign="Top" Width="80px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <div style="text-align:center"><asp:CheckBox ID="chkUnique" runat="server" Visible="true" />
                                 <asp:Label ID="lblUnique" runat="server" Text='<%# Bind("is_nursing_unique") %>' Visible="false" ></asp:Label> 
                                </div>
                                
                            </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Create by">
                       <ItemStyle VerticalAlign="Top" />
                          <ItemTemplate>
                              <asp:Label ID="lblBy" runat="server" Text='<%# Bind("create_by_name") %>'></asp:Label><br />
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("owner_dept_name") %>'></asp:Label><br />
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("create_date") %>'></asp:Label>
                          </ItemTemplate>
                        
                      </asp:TemplateField>
                    
                     
                  </Columns>
              </asp:GridView>
              <br />
            <asp:Button ID="cmdDelTopic" runat="server" Text="Delete" ForeColor="Red" Width="100px" OnClientClick="return confirm('Are you sure you want to delete ?');" />
                      
              &nbsp;<asp:Button ID="cmdSaveMapTopic" runat="server" Text="Save" Width="100px" />
                      
              <br />
            <table width="100%" cellpadding="3" cellspacing="0" >
     
             <tr>
                <td width="200" valign="top">&nbsp;</td>
                <td valign="top">
                    <asp:DropDownList ID="txtscope" runat="server" AutoPostBack="True" 
                        Visible="False">
                    <asp:ListItem Value="0">Hospital Level</asp:ListItem>
                    <asp:ListItem Value="1">Department Level</asp:ListItem>
                    </asp:DropDownList>
               </td>
                </tr>
     
             <tr>
                <td width="200" valign="top">Category</td>
                <td valign="top">
                <table width="100%" cellpadding="0" cellspacing="0">
                <tr><td><asp:DropDownList ID="txtadd_category" runat="server"  
                        DataTextField="category_name_en" DataValueField="category_id" 
                        AutoPostBack="True">
                  
                    </asp:DropDownList>
                </td></tr>
                <tr>
                <td>
                <asp:Panel ID="panel_dept" runat="server" Visible="false">
                <table width="100%" cellspacing="0" cellpadding="0" >
                      <tr>
                        <td width="180">
                            <asp:ListBox ID="lblCCAll" runat="server"  DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                        <td width="60" > <asp:Button ID="cmdCCAdd" runat="server" Text=">" 
                        CausesValidation="False" Width="30px" />
                  <br />
                   <asp:Button ID="lblCCRemove" runat="server" Text="<" CausesValidation="False"  Width="30px" /></td>
                        <td >
                            <asp:ListBox ID="lblCCSelect" runat="server" DataTextField="dept_name_en" 
                                DataValueField="dept_id" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                          </td>
                      </tr>
                  </table>
                </asp:Panel>
                </td>
                </tr>
                </table>
               </td>
                </tr>
              <tr>
                <td valign="top">Topic name</td>
                <td valign="top">
                    <asp:DropDownList ID="txtadd_topic" runat="server" 
                        DataTextField="master_topic_name_th" DataValueField="master_topic_id">
                    </asp:DropDownList>
                  </td>
                </tr>
                </table>
                <asp:Panel runat="server" ID="panelTopicLadder" Visible="false">
                  <table width="100%" cellpadding="3" cellspacing="0" >
     
           
               
                      <tr>
                          <td valign="top" width="200">
                              &nbsp;</td>
                          <td valign="top">
                              <asp:CheckBox ID="chkAddUnique" runat="server" Text="Unique Topic" />
                          </td>
                      </tr>
                </table>
                </asp:Panel>
                  <table width="100%" cellpadding="3" cellspacing="0" >
     
            
              <tr>
                <td valign="top" width="200">&nbsp;</td>
                <td valign="top">   
                <asp:Button ID="cmdAddTopic" runat="server" Text="Add" Width="100px" />
                      </td>
                </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td valign="top">&nbsp;</td>
                </tr>
            </table>
              </ContentTemplate>
              </asp:UpdatePanel>
            </div>
       
          
          <div class="tabbertab" id="tab_expect" runat="server">
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
                      <asp:TemplateField HeaderText="Expect outcomes (th)">
                         <ItemStyle  />
                          <ItemTemplate>
                                                   
                              <asp:textbox ID="txtname_th" runat="server" Text='<%# Bind("expect_detail") %>' Width="250px" TextMode="MultiLine" Rows="4"></asp:textbox><br />
                            
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Expect outcomes (en)">
                        
                          <ItemTemplate>
                                 <asp:textbox ID="txtname_en" runat="server" Text='<%# Bind("expect_detail_en") %>' Width="250px"  TextMode="MultiLine" Rows="4"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date" ItemStyle-Width="120px"   >              
<ItemStyle Width="120px"></ItemStyle>
                      </asp:BoundField>
                      <asp:BoundField DataField="create_by_name" HeaderText="Create by" 
                          ItemStyle-Width="200px" >                                       
<ItemStyle Width="200px"></ItemStyle>
                      </asp:BoundField>
                  </Columns>
              </asp:GridView>
              <br />
          <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
            
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top">
                 Expect outcomes (th)
                  <asp:TextBox
                      ID="txtadd_expect" runat="server" Width="600px"></asp:TextBox>
                 </td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"> Expect outcomes (en)
                  <asp:TextBox
                      ID="txtadd_expect_en" runat="server" Width="600px"></asp:TextBox>
                 </td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"> <asp:Button ID="cmdAddExpect" runat="server" Text="Add" />
                     <asp:Button ID="cmdDelExpect" runat="server" Text="Delete" />
                      <asp:Button ID="cmdOrderExpect" runat="server" Text="Save Order" /></td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
            </table>
            <br />
      </div>

          <div class="tabbertab" id="tab_method" runat="server">
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
                      <asp:TemplateField HeaderText="Methodogy (th)">
                         <ItemStyle Width="280px" />
                          <ItemTemplate>
                                               <asp:textbox ID="txtname_th" runat="server" Text='<%# Bind("method_name_th") %>' Width="250px" ></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Methodogy (en)">
                         
                          <ItemTemplate>
                             <asp:textbox ID="txtname_en" runat="server" Text='<%# Bind("method_name") %>' Width="250px"></asp:textbox>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField HeaderText="Date/Time" 
                          DataField="create_date"  />              
                      <asp:BoundField DataField="create_by_name" HeaderText="By" />                                       
                  </Columns>
              </asp:GridView>
              <br />
          <table width="100%" cellpadding="3" cellspacing="0" class="tdata">
            
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"><span style="font-weight: bold">
                 
                  <asp:TextBox
                      ID="txtadd_method" runat="server" Width="350px"></asp:TextBox>
                  </span></td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
              <tr>
                <td  width="30" valign="top">&nbsp;</td>
                <td  width="30" valign="top">&nbsp;</td>
                <td valign="top"> <asp:Button ID="cmdAddMethod" runat="server" 
                        Text="Add" />
                     <asp:Button ID="cmdDeleteMethod" runat="server" Text="Delete" />
                      <asp:Button ID="cmdSaveMethod" runat="server" Text="Save Order" /></td>
                <td colspan="2" valign="top"> &nbsp;</td>
                </tr>
            </table>
            <br />
      </div>
          
          
          <div class="tabbertab" id="tab_message" runat="server" visible="false">
            <h2>
                IDP Message
            </h2>
              <table width="100%">
              <tr>
              <td>
              ข้อความหน้าแรก
              </td>
              </tr>
              <tr>
              <td>
                  <cc1:Editor ID="txtmessage" runat="server" />
                  </td>
              </tr>
              <tr>
              <td>
                  <asp:Label ID="lblPicture" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
              <td>
                  <asp:FileUpload ID="FileUpload0" runat="server" />
              &nbsp;
                  <asp:Button ID="cmdUpload" runat="server" Text="Upload Picture" />
                  <asp:Button ID="cmdDelPicture" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr><td>ข้อความหน้า Development Plan</td></tr>
               <tr>
              <td>
                  <cc1:Editor ID="txtmessageIDP" runat="server" />
                  </td>
              </tr>
               <tr>
              <td>
                  <asp:CheckBox ID="chkDelay" runat="server" Text="Delay 30 Seconds" />
                  </td>
              </tr>
               <tr>
              <td>
                  <asp:Label ID="lblPictureIDP" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
              <td>
                  <asp:FileUpload ID="FileUploadIDP" runat="server" />
              &nbsp;
                  <asp:Button ID="cmdUploadIDP" runat="server" Text="Upload Picture" />
                  <asp:Button ID="cmdDelPictureIDP" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr><td>ข้อความหน้า External Training Request</td></tr>
               <tr>
              <td>
                  <cc1:Editor ID="txtmessageExternal" runat="server" />
                  </td>
              </tr>
               <tr>
              <td>
                  <asp:Label ID="lblPictureExt" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
              <td>
                  <asp:FileUpload ID="FileUploadExt" runat="server" />
              &nbsp;
                  <asp:Button ID="cmdUploadExt" runat="server" Text="Upload Picture" />
                  <asp:Button ID="cmdDelPictureExt" runat="server" Text="Delete picture" />
              </td>
              </tr>
              <tr>
              <td>
                  <asp:Button ID="cmdUpdateObject" runat="server" Text="Update" />
              </td>
              </tr>
              <tr>
              <td>&nbsp;</td>
              </tr>
              <tr>
              <td>
                   <fieldset>
        <legend>IDP Management</legend>
         
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="200">
                    Change IDP Submit status to</td>
                <td>
            <asp:DropDownList ID="txtconfig" runat="server" >
            <asp:ListItem Value="0">Pending all request</asp:ListItem>
            <asp:ListItem Value="1">Active</asp:ListItem>
            </asp:DropDownList>
          &nbsp;
                    <asp:Button ID="cmdConfig" runat="server" Text="Update Status" OnClientClick="return confirm('Are you sure you want to change status ?')" />
                &nbsp;</td>
            </tr>
        </table>
   
        </fieldset></td>
              </tr>
              </table>
            </div>
          </div>
        </div>
</asp:Content>

