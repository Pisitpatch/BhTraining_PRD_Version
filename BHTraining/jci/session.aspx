<%
   
    If IsNothing(Session("jci_emp_code")) = True Then
        Response.Write("employeeid=error&questionid=error")
    Else
        
        If IsNothing(Session("question_id")) = True Then
            Response.Write("employeeid=error&questionid=error")
        Else
            Response.Write("employeeid=" & Session("jci_emp_code").ToString & "&questionid=" & Session("question_id"))
        End If
        
        
    End If
   
%>
