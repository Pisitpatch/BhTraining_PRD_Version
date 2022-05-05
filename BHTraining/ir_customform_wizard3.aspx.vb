Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction

Partial Class ir_customform_wizard3
    Inherits System.Web.UI.Page

    Protected conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    'Private conn1 As DBUtil = CType(Session("myConn"), DBUtil)
    Protected id As String
    Protected dsForm As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If conn.setConnection Then

        Else
            Response.Write("Connection Error")
        End If

        id = Request.QueryString("id")


        If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
            bindInformation()
            bindForm()
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            ' response.write("close connnection")
            ' conn.closeSql()
            ' conn = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Sub bindInformation()
        Dim sql As String
        Dim ds As New DataSet
        Try
            sql = "SELECT * FROM ir_form_master WHERE form_id = " & id
          
            ds = conn.getDataSet(sql, "t1")
            txtheader1.Text = ds.Tables(0).Rows(0)("form_name_th").ToString
            txtheader2.Text = ds.Tables(0).Rows(0)("form_name_en").ToString
        Catch ex As Exception
            Response.Write("bindInformation :: " & ex.Message)
        Finally
            ds.Dispose()
            ds = Nothing
        End Try
    End Sub

    Sub bindForm()
        Dim sql As String
        Try
            sql = "SELECT element_order_sort FROM ir_formcustom_element_list WHERE form_id = " & id
            sql &= " GROUP BY element_order_sort"
            sql &= " ORDER BY element_order_sort ASC"
            dsForm = conn.getDataSet(sql, "t1")
        Catch ex As Exception
            Response.Write("bindForm :: " & ex.Message)
        End Try
    End Sub

    Protected Function getSubRow(ByVal element_id As String) As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = ""

        Try
            sql = "SELECT * FROM ir_formcustom_element_item WHERE element_id = " & element_id

            sql &= " ORDER BY item_order_sort ASC"
            ds = conn.getDataSet(sql, "t1")

            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= "<input type = 'checkbox' /> " & ds.Tables(0).Rows(i)("item_label_en").ToString
            Next i



            Return result
        Catch ex As Exception
            ' Response.Write(sql)
            'Response.Write(ex.Message)
            Return ex.Message
        Finally
            ds.Dispose()
        End Try


    End Function

    Protected Function getRadioRow(ByVal element_id As String) As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = ""

        Try
            sql = "SELECT * FROM ir_formcustom_element_item WHERE element_id = " & element_id

            sql &= " ORDER BY item_order_sort ASC"
            ds = conn.getDataSet(sql, "t1")

            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If


            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= "<input type = 'radio' /> " & ds.Tables(0).Rows(i)("item_label_en").ToString
            Next i



            Return result
        Catch ex As Exception
            ' Response.Write(sql)
            'Response.Write(ex.Message)
            Return ex.Message
        Finally
            ds.Dispose()
        End Try


    End Function

    Protected Function getDropdownRow(ByVal element_id As String) As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = ""

        Try
            sql = "SELECT * FROM ir_formcustom_element_item WHERE element_id = " & element_id

            sql &= " ORDER BY item_order_sort ASC"
            ds = conn.getDataSet(sql, "t1")

            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            result = "<select>"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= "<option> " & ds.Tables(0).Rows(i)("item_label_en").ToString & "</option>"
            Next i
            result &= "</select>"


            Return result
        Catch ex As Exception
            ' Response.Write(sql)
            'Response.Write(ex.Message)
            Return ex.Message
        Finally
            ds.Dispose()
        End Try


    End Function

    Protected Function getRow(ByVal order_sort As String) As String
        Dim sql As String
        Dim ds As New DataSet
        Dim result As String = ""

        Try
            sql = "SELECT * FROM ir_formcustom_element_list WHERE form_id = " & id
            sql &= " AND element_order_sort = " & order_sort
            sql &= " ORDER BY element_order_sort ASC"
            ds = conn.getDataSet(sql, "t1")

            If conn.errMessage <> "" Then
                Throw New Exception(conn.errMessage)
            End If

            result &= "<table width='100%'><tr>"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                result &= "<td width='100' style='vertical-align:middle'>"
                If i > 0 Then
                    result &= "&nbsp;"
                End If
                result &= ds.Tables(0).Rows(i)("element_name_en").ToString & "&nbsp;"
                result &= "</td><td>"
                If Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 1 Then
                    result &= "<input type='text' />"
                ElseIf Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 2 Then
                    result &= "<textarea rows='5'></textarea>"
                ElseIf Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 3 Then
                    result &= getRadioRow(ds.Tables(0).Rows(i)("element_id").ToString)
                ElseIf Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 4 Then
                    result &= getDropdownRow(ds.Tables(0).Rows(i)("element_id").ToString)
                ElseIf Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 5 Then
                    result &= getSubRow(ds.Tables(0).Rows(i)("element_id").ToString)
                ElseIf Convert.ToInt32(ds.Tables(0).Rows(i)("element_type_id").ToString) = 6 Then
                End If

                result &= "</td>"
            Next i

            result &= "</tr></table>"

            Return result
        Catch ex As Exception
            ' Response.Write(sql)
            'Response.Write(ex.Message)
            Return ex.Message
        Finally
            ds.Dispose()
        End Try


    End Function

    Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Response.Redirect("ir_customform_wizard2.aspx?id=" & id)
    End Sub
End Class
