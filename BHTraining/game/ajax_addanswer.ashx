<%@ WebHandler Language="VB" Class="ajax_addanswer" %>

Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient


Public Class ajax_addanswer : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim dept_id As String = context.Request.QueryString("dept_id")
        Dim qid As String = context.Request.QueryString("qid")
        Dim emp_code As String = context.Request.QueryString("emp_code")
        Dim answer_id As String = context.Request.QueryString("answer_id")
        Dim correct As String = context.Request.QueryString("correct")
        Dim gid As String = context.Request.QueryString("gid")
        Dim strClientIP As String      
        Dim sql As String = ""
        Dim ds As New DataSet
        Dim order As Integer = 1
     
        strClientIP = context.Request.UserHostAddress()
        
        Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Dim errorMsg As String = ""     
       
        Try
            If conn.setConnectionForTransaction Then

            Else
                Throw New Exception(conn.errMessage)
            End If
            
            sql = "SELECT * FROM jci_trans_list WHERE 1 = 1 "
            sql &= " AND question_id = " & qid
            sql &= " AND trans_create_by_emp_code = " & emp_code
            sql &= " ORDER BY trans_order_sort DESC "
            ds = conn.getDataSetForTransaction(sql, "tOrder")
            If ds.Tables("tOrder").Rows.Count > 0 Then
                order = CInt( ds.Tables("tOrder").Rows(0)("trans_order_sort").ToString) + 1
            End If
            
            sql = "SELECT * FROM jci_trans_list WHERE game_answer_id = " & answer_id
            sql &= " AND question_id = " & qid
            sql &= " AND trans_create_by_emp_code = " & emp_code
            ds = conn.getDataSetForTransaction(sql, "t1")
            
            If ds.Tables("t1").Rows.Count = 0 Then
                sql = "INSERT INTO jci_trans_list (question_id , trans_q_name_th , trans_q_name_en , trans_ip_address , trans_create_by_name "
                sql &= " , trans_create_by_emp_code , trans_create_date , trans_create_date_ts , trans_dept_name , trans_dept_id , game_answer_id , is_correct_answer "
                sql &= " , game_group_id , trans_order_sort "
                sql &= " )VALUES("
                sql &= "" & qid & " , null , null , '" & strClientIP & "' , null "
                sql &= " , " & emp_code & " , GETDATE() , " & Date.Now.Ticks & " , '" & "" & "' , " & dept_id & " , " & answer_id & ", " & correct
                sql &= " , " & gid
                sql &= " , " & order
                sql &= " )"
                
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg & sql)
                End If
            Else
                sql = "UPDATE jci_trans_list SET is_correct_answer = " & correct
                sql &= " , trans_create_date = GETDATE() "
                sql &= " , trans_create_date_ts = " & Date.Now.Ticks
                sql &= " WHERE game_answer_id = " & answer_id
                sql &= " AND question_id = " & qid
                sql &= " AND trans_create_by_emp_code = " & emp_code
            End If
        
         
            
      
            
            conn.setDBCommit()
        Catch ex As Exception
            conn.setDBRollback
            context.Response.Write(ex.Message)
        Finally
            ds.Dispose()
            conn.closeSql()
        End Try
      
        
      
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class