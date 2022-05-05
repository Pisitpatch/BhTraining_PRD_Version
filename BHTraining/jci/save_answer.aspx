<%@ Import Namespace="System.data" %>
<%
    
    If IsNothing(Session("jci_emp_code")) Then
        Response.Redirect("login.aspx")
        Response.End()
    End If
    
    
    Dim conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
    Dim questionid As String = ""
    Dim sql As String = ""
    Dim errorMsg As String
    Dim ds As New DataSet
    
    Dim strClientIP As String
    strClientIP = Request.UserHostAddress()
    
    questionid = Request.QueryString("qid")
  
    
    Try
        If conn.setConnectionForTransaction Then

        Else
            Response.Write("Connection Error")
        End If
        
        If questionid = "" Then
            Throw New Exception("no question id")
        End If
        
        sql &= " SELECT question_id , trans_q_name_th , trans_q_name_en , '" & strClientIP & "' , '" & Session("jci_user_fullname").ToString & "' "
        sql &= " , '" & Session("jci_emp_code").ToString & "' , GETDATE()  ," & Date.Now.Ticks
        sql &= " FROM jci_trans_list WHERE question_id = " & questionid & " AND trans_create_by_emp_code = " & Session("jci_emp_code").ToString
        ds = conn.getDataSetForTransaction(sql, "t1")
        '   Response.Write( ds.Tables(0).Rows.Count)
        If ds.Tables(0).Rows.Count = 0 Then
            sql = "INSERT INTO jci_trans_list (question_id , trans_q_name_th , trans_q_name_en , trans_ip_address , trans_create_by_name "
            sql &= " , trans_create_by_emp_code , trans_create_date , trans_create_date_ts , trans_dept_name , trans_dept_id , trans_job_title ) "
            sql &= " SELECT question_id , question_detail_th , question_detail_en , '" & strClientIP & "' , '" & Session("jci_user_fullname").ToString & "' "
            sql &= " , '" & Session("jci_emp_code").ToString & "' , GETDATE()  ," & Date.Now.Ticks
            sql &= " , '" & Session("jci_dept_name").ToString & "' , '" & Session("jci_dept_id").ToString & "' , '" & Session("jci_job_title").ToString & "'"
            sql &= " FROM jci_master_question WHERE question_id = " & questionid
            
           ' Response.Write (sql)
           
        Else
            sql = "UPDATE jci_trans_list SET trans_create_date = GETDATE() , trans_create_date_ts = '" & Date.Now.Ticks & "' "
            sql &= " WHERE trans_id = " & Session("trans_id").ToString
        End If
       
        errorMsg = conn.executeSQLForTransaction(sql)
        If errorMsg <> "" Then
            Throw New Exception(errorMsg)
        End If
        
        conn.setDBCommit()
    Catch ex As Exception
        conn.setDBRollback()
        Response.Write(ex.Message & sql)
        Response.End()
    Finally
        Try
            ' response.write("close connnection")
            conn.closeSql()
            conn = Nothing
        Catch ex As Exception
          
        End Try
    End Try
   
   ' Response.Redirect("jci_select_question.aspx")
    
    
    
%>