Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Partial Class idp_popup_evaluate
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected id As String = ""
    Protected name As String = ""
    Protected method As String
    Protected category As String
    Protected jobtype As String
    Protected planyear As String
    Protected sh_id As String
    Protected dept As String
    Protected viewtype As String = ""
    Protected status As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsNothing(Session("user_fullname")) Then
            Response.Redirect("../login.aspx")
            Response.End()
        End If

        viewtype = Request.QueryString("viewtype")

        id = Request.QueryString("id")
        name = Request.QueryString("name")
        method = Request.QueryString("method")
        category = Request.QueryString("category")
        jobtype = Request.QueryString("jobtype")
        planyear = Request.QueryString("planyear")
        sh_id = Request.QueryString("sh_id")
        dept = Request.QueryString("dept")
        status = Request.QueryString("status")

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        If Not Page.IsPostBack Then ' First time load
            ' lblTopic.Text &= "<strong>Cost Center :</strong> " & dept & "<br/>"
            'lblTopic.Text &= "<strong>Year :</strong> " & planyear & "<br/>"
            ' lblTopic.Text &= "<strong>Job Type :</strong> " & jobtype & "<br/>"
          
            bindGrid()
            bindGrid2()
            bindGridMethod()
            bindGridRecommend()
        End If


    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindGrid()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select d.topic_name_th AS 'Evaluation Topic'"
            sql &= " , ISNULL(MAX(num1),0) AS '1 Needs improvement' , ISNULL(MAX(num2),0) AS '2 Below average' , ISNULL(MAX(num3),0) AS '3 Average' "
            sql &= " , ISNULL(MAX(num4),0) AS '4 Good  ' , ISNULL(MAX(num5),0) AS '5 Excellent' ,  avg(cast(a.evaluation_score as dec(3,1))) AS 'Average Score' "
            sql &= " from idp_evaluation_list a "
            sql &= " inner join idp_trans_list b on a.idp_id = b.idp_id "
            sql &= " inner join idp_external_req c on b.idp_id = c.idp_id and c.request_type = 'int'"
            sql &= " inner join idp_m_evaluation_topic d on a.evaluation_form_id  = d.evaluation_form_id"
            sql &= " inner join idp_training_schedule e on a.schedule_id = e.schedule_id"
            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS num1 , schedule_id , evaluation_form_id FROM idp_evaluation_list WHERE evaluation_score = 1  "
            sql &= " GROUP BY schedule_id , evaluation_form_id) n1"
            sql &= " ON a.schedule_id = n1.schedule_id and a.evaluation_form_id = n1.evaluation_form_id"
            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS num2 , schedule_id, evaluation_form_id FROM idp_evaluation_list WHERE evaluation_score = 2 "
            sql &= " GROUP BY schedule_id , evaluation_form_id) n2"
            sql &= " ON a.schedule_id = n2.schedule_id and a.evaluation_form_id = n2.evaluation_form_id"
            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS num3 , schedule_id, evaluation_form_id FROM idp_evaluation_list WHERE evaluation_score = 3  "
            sql &= " GROUP BY schedule_id, evaluation_form_id) n3"
            sql &= " ON a.schedule_id = n3.schedule_id and a.evaluation_form_id = n3.evaluation_form_id"
            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS num4 , schedule_id, evaluation_form_id FROM idp_evaluation_list WHERE evaluation_score = 4  "
            sql &= " GROUP BY schedule_id, evaluation_form_id) n4"
            sql &= " ON a.schedule_id = n4.schedule_id and a.evaluation_form_id = n4.evaluation_form_id"
            sql &= " LEFT OUTER JOIN (SELECT COUNT(*) AS num5 , schedule_id , evaluation_form_id FROM idp_evaluation_list WHERE evaluation_score = 5  "
            sql &= " GROUP BY schedule_id , evaluation_form_id) n5"
            sql &= " ON a.schedule_id = n5.schedule_id and a.evaluation_form_id = n5.evaluation_form_id"

            sql &= " WHERE a.schedule_id = " & sh_id
            sql &= " group by d.topic_name_th , c.internal_title , a.idp_id , e.schedule_id"
            sql &= " order by e.schedule_id"

            ds = conn.getDataSet(sql, "t1")
            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage & sql)
            End If
            Gridview1.DataSource = ds
            Gridview1.DataBind()

            ' lblNum.Text = Gridview1.Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGrid2() ' comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select suggest_speaker AS 'Suggestion for speaker' , suggest_course AS 'Suggestion for this course' "

            sql &= " from idp_evaluation_head a "

            sql &= " WHERE (ISNULL(cast(suggest_speaker as varchar),'') <> '' OR ISNULL(cast(suggest_course as varchar),'') <> '') AND a.schedule_id = " & sh_id
      

            ds = conn.getDataSet(sql, "t1")

            Gridview2.DataSource = ds
            Gridview2.DataBind()

            ' lblNum.Text = Gridview1.Rows.Count
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridRecommend() ' comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select ISNULL(MAX(n1.num1),0) AS 'แนะนำ' , ISNULL(MAX(n2.num2),0) AS 'ไม่แนะนำ'  , ISNULL(MAX(n3.num3),0) AS 'ไม่แน่ใจ' "

            sql &= " from idp_evaluation_head a "
            sql &= " LEFT OUTER JOIN (select count(*) as num1 , schedule_id  from idp_evaluation_head where recommend1 = 1 group by schedule_id ) n1"
            sql &= " ON a.schedule_id = n1.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num2 , schedule_id  from idp_evaluation_head where recommend2 = 1 group by schedule_id ) n2"
            sql &= " ON a.schedule_id = n2.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num3 , schedule_id  from idp_evaluation_head where recommend3 = 1 group by schedule_id ) n3"
            sql &= " ON a.schedule_id = n3.schedule_id"
       
            sql &= " where a.schedule_id = " & sh_id
            sql &= " group by  a.schedule_id "



            ds = conn.getDataSet(sql, "t1")

            GridviewRecommend.DataSource = ds
            GridviewRecommend.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Sub bindGridMethod() ' comment
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "select ISNULL(MAX(n1.num1),0) AS 'บรรยาย' , ISNULL(MAX(n2.num2),0) AS 'การสอนงาน'  , ISNULL(MAX(n3.num3),0) AS 'ประชุมเชิงปฎิบัติการ' "
            sql &= " , ISNULL(MAX(n4.num4),0) AS 'นำเสนอโดยกลุ่มงาน' , ISNULL(MAX(n5.num5),0) AS 'บทบาทสมมุติ' , ISNULL(MAX(n6.num6),0) AS 'อื่นๆ'"
            sql &= " from idp_evaluation_head a "
            sql &= " LEFT OUTER JOIN (select count(*) as num1 , schedule_id  from idp_evaluation_head where method1 = 1 group by schedule_id ) n1"
            sql &= " ON a.schedule_id = n1.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num2 , schedule_id  from idp_evaluation_head where method2 = 1 group by schedule_id ) n2"
            sql &= " ON a.schedule_id = n2.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num3 , schedule_id  from idp_evaluation_head where method3 = 1 group by schedule_id ) n3"
            sql &= " ON a.schedule_id = n3.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num4 , schedule_id  from idp_evaluation_head where method4 = 1 group by schedule_id ) n4"
            sql &= " ON a.schedule_id = n4.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num5 , schedule_id  from idp_evaluation_head where method5 = 1 group by schedule_id ) n5"
            sql &= " ON a.schedule_id = n5.schedule_id"
            sql &= " LEFT OUTER JOIN (select count(*) as num6 , schedule_id  from idp_evaluation_head where method6 = 1 group by schedule_id ) n6"
            sql &= " ON a.schedule_id = n6.schedule_id"
            sql &= " where a.schedule_id = " & sh_id
            sql &= " group by  a.schedule_id "



            ds = conn.getDataSet(sql, "t1")

            GridviewMethod.DataSource = ds
            GridviewMethod.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
