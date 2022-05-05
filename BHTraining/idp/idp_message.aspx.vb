Imports ShareFunction
Imports System.Data

Partial Class idp_idp_message
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Protected counter_date As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
            Return
        End If

        If Not Page.IsPostBack Then ' first time
            bindForm()
            makeCounter()
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

    Sub makeCounter()
        Dim newdate As String = ""
        Dim hour1 As String = ""
        Dim min1 As String = ""
        ' Dim sec1 As String = ""
        Dim day1 As String = ""

        Dim left_day1 As Double = 0
        Dim date_start As New Date(2013, 12, 31)

        Dim diff_min As Long = DateDiff(DateInterval.Minute, Date.Now, date_start)
        ' tatNum = diff_min
        '  day1 = CInt((diff_min / 1440))
        day1 = Math.Floor(diff_min / 1440)
        left_day1 = diff_min Mod 1440
        hour1 = CInt(left_day1 / 60)
        min1 = left_day1 Mod 60
        ' sec1 = day1 Mod 3600
        'hour1 = CInt(diff_min / 60)
        'min1 = diff_min Mod 60
        counter_date = day1.PadLeft(2, "0") & ":" & hour1.PadLeft(2, "0") & ":" & min1.PadLeft(2, "0") & ":00"
    End Sub

    Sub bindForm()
        Dim sql As String
        Dim ds As New DataSet

        Try
            sql = "SELECT * FROM idp_message WHERE idp_message_id = 1"
            ds = conn.getDataSetForTransaction(sql, "t1")
            If ds.Tables("t1").Rows(0)("picture_path").ToString <> "" Then
                lblPicture.text = "<img src='../share/idp/" & ds.Tables("t1").Rows(0)("picture_path").ToString & "' /><br/><br/>"
            End If

            lblpicture.text &= "" & ds.Tables("t1").Rows(0)("message_detail").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub
End Class
