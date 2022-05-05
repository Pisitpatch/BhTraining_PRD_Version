<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup_review_score.aspx.vb" Inherits="jci_popup_review_score" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script type="text/javascript" src="../js/jquery-1.4.2.min.js" charset="utf-8"></script>
         <script type="text/javascript" src="../js/media_play.js" charset="utf-8"></script>
 <script type="text/javascript" src="../js/media_chili.js" charset="utf-8"></script>
<script type="text/javascript">

    $(function () {

        //$.fn.media.mapFormat('avi','quicktime');

        // this one liner handles all the examples on this page

        $('a.media').media();

    });

</script>
<title>Bumrungrad Information System</title>

<link href="../css/jcistyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellspacing="0" cellpadding="0">
  <tr>
    <td><div style="width: 900px;"><table width="100%" class="box-header">
  <tr>
    <td><img src="../images/report.png" width="24" height="24" align="absmiddle" />&nbsp;&nbsp;Answer Review</td>
  </tr>
</table>
	<div class="question">
        <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="questiondesc">
      <span style="background: #a14d48; color: #FFF; padding: 3px 5px;">Submitted  by</span>
<strong><asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></strong> Employee ID <strong><asp:Label ID="lblEmpCode" runat="server" Text="Label"></asp:Label></strong> Department 
<strong><asp:Label ID="lblDeptName" runat="server" Text="Label"></asp:Label></strong> 
Job Title <strong><asp:Label ID="lblJobTitle" runat="server" Text="Label"></asp:Label></strong></div>		
      <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
          <td width="485" valign="top"><div style="padding: 30px 50px;">
          <a class="media {width:450, height:370}" href="mediaplayer.swf?file=flash/<%response.write(emp_code) %>-<%response.write(question_id) %>.flv">SWF with FLV (mediaplayer.swf?file62752-1.flv)</a> 
         </div></td>
          <td valign="top"><div style="margin: 30px 0px;">
            <fieldset style="width: 300px; border: solid 1px #CCC;">
              <legend style="background: url(images/boxheader.gif); border: solid 1px #CCC; padding: 5px 10px; margin-left: 10px; margin-bottom: 5px;">Answer Score
              </legend>
                <table width="100%" class="box-content">
                <tr>
                  <td><table width="100%" cellpadding="5">
                    <tr>
                      <td width="25"><asp:RadioButton ID="txtscore1" GroupName="score" runat="server" CssClass="score" />
                        </td>
                      <td>Not met </td>
                      <td><img src="../images/151.png" alt="" width="16" height="16"  /></td>
                      </tr>
                    <tr>
                      <td><asp:RadioButton ID="txtscore2" GroupName="score" runat="server" CssClass="score"  />
                        </td>
                      <td>Partially met </td>
                      <td><img src="../images/154.png" alt="" width="16" height="16"  /></td>
                      </tr>
                    <tr>
                      <td><asp:RadioButton ID="txtscore3" GroupName="score" runat="server" CssClass="score"  />
                        </td>
                      <td>Fully met </td>
                      <td><img src="../images/152.png" alt="" width="16" height="16"  /></td>
                      </tr>
                    </table></td>
                  </tr>
                <tr>
                  <td>
                      <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="green" />
                                   <input class="green" type="submit" name="button3" id="button3" value="Cancel" onclick="window.close();"/>
 <asp:Button ID="cmdClear" runat="server" Text="Reset" CssClass="green" /></td>
                </tr>
                </table>
            </fieldset>
          </div></td>
        </tr>
    </table></div></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
