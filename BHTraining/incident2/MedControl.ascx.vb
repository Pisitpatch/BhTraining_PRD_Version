Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ShareFunction

Namespace incident

    Partial Class incident_MedControl
        Inherits System.Web.UI.UserControl
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Private mode As String = ""
        Private irId As String
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If conn.setConnectionForTransaction Then

            Else
                Response.Write("Connection Error")
            End If

            mode = Request.QueryString("mode")
            irId = Request.QueryString("irId")
        End Sub

        Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        End Sub

        Protected Sub cmdAddPeriod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddPeriod.Click
            Dim sql As String
            Dim errorMsg As String = ""
            Dim pk As String

            Try
                pk = getPK("ir_med_id", "ir_med_tab_period", conn)
                sql = "INSERT INTO ir_med_tab_period (ir_med_id , ir_id , job_type_id , job_type_name , period_id , period_name , work_name, work_id , session_id) VALUES("
                sql &= " '" & pk & "' ,"
                If mode = "add" Then
                    sql &= " null ,"
                Else
                    sql &= " '" & irId & "' ,"
                   
                End If
                sql &= " '" & txtadd_jobtype.SelectedIndex & "' ,"
                sql &= " '" & txtadd_jobtype.SelectedItem.Text & "' ,"
                sql &= " '" & txtadd_period.SelectedIndex & "' ,"
                sql &= " '" & txtadd_period.SelectedItem.Text & "' ,"
                sql &= " '" & txtworkprocess.SelectedItem.Text & "' ,"
                sql &= " '" & txtworkprocess.SelectedValue & "' ,"
                If mode = "add" Then
                    sql &= " '" & Session.SessionID & "' "
                Else
                    sql &= " null "
                End If

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                ' Response.Write(sql)
                bindMedPeriod()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Sub bindMedPeriod()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_med_tab_period WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & Session.SessionID & "'"
                Else
                    sql &= " AND ir_id = " & irId
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")

                GridMedPeriod.DataSource = ds
                GridMedPeriod.DataBind()
             

            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Sub bindDrugWrongName()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM ir_med_tab_drug WHERE 1 = 1 "
                If mode = "add" Then
                    sql &= " AND session_id = '" & Session.SessionID & "'"
                Else
                    sql &= " AND ir_id = " & irId
                End If
                ds = conn.getDataSetForTransaction(sql, "t1")

                GridDrugWrongName.DataSource = ds
                GridDrugWrongName.DataBind()


            Catch ex As Exception
                Response.Write(ex.Message & sql)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub GridMedPeriod_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridMedPeriod.RowDeleted

        End Sub

        Protected Sub GridMedPeriod_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridMedPeriod.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_med_tab_period WHERE ir_med_id = " & GridMedPeriod.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If

                conn.setDBCommit()
                bindMedPeriod()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridMedPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMedPeriod.SelectedIndexChanged
           
        End Sub

        Protected Sub cmdAddWrongName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddWrongName.Click
            Dim sql As String
            Dim errorMsg As String = ""
            Dim pk As String

            Try
                pk = getPK("wrong_drug_id", "ir_med_tab_drug", conn)
                sql = "INSERT INTO ir_med_tab_drug (wrong_drug_id , ir_id , drug_wrong_name , drug_group ,  lasa_name , lasa_id , chk_alert , chk_floor , chk_smartpump , chk_bcma , session_id ) VALUES("
                sql &= " '" & pk & "' ,"
                If mode = "add" Then
                    sql &= " null ,"
                Else
                    sql &= " '" & irId & "' ,"

                End If
                sql &= " '" & addslashes(txtdrugname.Value) & "' ,"
                sql &= " '" & addslashes(txtdruggroup.Value) & "' ,"
                sql &= " '" & txtlasa.SelectedItem.Text & "' ,"
                sql &= " '" & txtlasa.SelectedValue & "' ,"
                If chkHighAlert.Checked = True Then
                    sql &= " 'Yes' , "
                Else
                    sql &= " 'No' , "
                End If

                If chkFloorStock.Checked = True Then
                    sql &= " 'Yes' , "
                Else
                    sql &= " 'No' , "
                End If

                If chkSmartPump.Checked = True Then
                    sql &= " 'Yes' , "
                Else
                    sql &= " 'No' , "
                End If

                If chkBCMA.Checked = True Then
                    sql &= " 'Yes' , "
                Else
                    sql &= " 'No' , "
                End If


                If mode = "add" Then
                    sql &= " '" & Session.SessionID & "' "
                Else
                    sql &= " null "
                End If

                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                conn.setDBCommit()
                ' Response.Write(sql)
                txtdrugname.Value = ""
                txtdruggroup.Value = ""
                chkHighAlert.Checked = False
                txtlasa.SelectedIndex = 0
                bindDrugWrongName()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Protected Sub GridDrugWrongName_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridDrugWrongName.RowDeleting
            Dim sql As String
            Dim result As String
            Try
                sql = "DELETE FROM ir_med_tab_drug WHERE wrong_drug_id = " & GridDrugWrongName.DataKeys(e.RowIndex).Value & ""
                result = conn.executeSQLForTransaction(sql)

                If result <> "" Then
                    Throw New Exception(result)
                End If

                conn.setDBCommit()
                bindDrugWrongName()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            End Try
        End Sub

        Protected Sub GridDrugWrongName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridDrugWrongName.SelectedIndexChanged

        End Sub
    End Class
End Namespace
