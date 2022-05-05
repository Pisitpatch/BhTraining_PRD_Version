<%

    Try
        If IsNothing(Session("session_myid")) Then ' ถ้าไม่มี Session
            '  Response.Redirect("../login.aspx")
            Response.Write("99")
            'Response.End()
        Else
              Response.Write("0")
        End If
    Catch ex As Exception
        Response.Write(ex.Message)
    End Try
   
%>