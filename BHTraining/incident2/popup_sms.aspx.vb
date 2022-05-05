Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class incident_popup_sms
    Inherits System.Web.UI.Page
    Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")

        End If

        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 

        Else

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

    Sub bindList1()
        Dim sql As String = ""
        Dim ds As New DataSet

        Try
            sql = "SELECT user_fullname + ' <' + ISNULL(cast(custom_mobile as varchar),'') + '>' AS fullname , custom_mobile   FROM user_profile WHERE 1 = 1"
            If txtfind.Text <> "" Then
                sql &= " AND (user_fullname LIKE '%" & txtfind.Text & "%' "
                sql &= " OR emp_code LIKE '%" & txtfind.Text & "%' "
                'sql &= " OR emp_code LIKE '%" & txtfind.Text & "%' "
                'sql &= " OR emp_code LIKE '%" & txtfind.Text & "%' "
                sql &= ")"
                ds = conn.getDataSetForTransaction(sql, "t1")
                '  Response.Write(sql)
                ListBox1.DataSource = ds
                ListBox1.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ds.Dispose()
        End Try
    End Sub

    Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        bindList1()
    End Sub

    Protected Sub cmdSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        While ListBox1.Items.Count > 0 AndAlso ListBox1.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = ListBox1.SelectedItem
            selectedItem.Selected = False
            ListBox2.Items.Add(selectedItem)
            ListBox1.Items.Remove(selectedItem)
        End While

        onSelect()
    End Sub

    Protected Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        While ListBox2.Items.Count > 0 AndAlso ListBox2.SelectedItem IsNot Nothing
            Dim selectedItem As ListItem = ListBox2.SelectedItem
            selectedItem.Selected = False
            ListBox1.Items.Add(selectedItem)
            ListBox2.Items.Remove(selectedItem)
        End While

        onSelect()
    End Sub

    Sub onSelect()
        Dim limit As String = ""
        txtselect.Value = ""
        txtidselect.Value = ""

        

        For i As Integer = 0 To ListBox2.Items.Count - 1
            If i = 0 Then
                limit = ""
            Else
                limit = ","
            End If
            txtselect.Value &= limit & ListBox2.Items(i).Text
            txtidselect.Value &= limit & ListBox2.Items(i).Value
        Next i

      
    End Sub
End Class
