Imports System.Data.SqlClient
Imports System.Data
Imports ShareFunction
Imports System.DirectoryServices

Namespace incident
    Partial Class incident_popup_recepient
        Inherits System.Web.UI.Page
        Private conn As New DBUtil(ConfigurationManager.AppSettings("database").ToString)
        Private irId As String = ""
        Protected popupType As String = ""
        Protected modules As String = "ir"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If IsNothing(Session("user_fullname")) Then
                ' Response.Redirect("../login.aspx")
                Response.End()
            End If

            irId = Request.QueryString("irId")
            modules = Request.QueryString("modules")
            If modules = "" Then
                modules = "ir"
            End If

            popupType = Request.QueryString("popupType")
            If conn.setConnectionForTransaction Then

            Else
                Response.Write("Connection Error")
            End If

            If Not Page.IsPostBack Then ' เข้ามาครั้งแรก 
                bindDept()
                bindGroup()

                If modules = "idp" Then
                    ' cmdSaveGroup.Enabled = False
                    'cmdEditGroup.Enabled = False
                    'cmdDeleteGroup.Enabled = False
                End If
              
            Else

                

            End If

            If txtgroup.SelectedIndex < 0 Then
                cmdEditGroup.Visible = False
                cmdDeleteGroup.Visible = False
                cmdSelectGroup.Visible = False
            Else
                cmdEditGroup.Visible = True
                cmdDeleteGroup.Visible = True
                cmdSelectGroup.Visible = True

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

        Sub bindDept()
            Dim sql As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM user_dept  ORDER BY dept_name_en ASC"
                ds = conn.getDataSetForTransaction(sql, "t1")
                If conn.errMessage <> "" Then
                    Throw New Exception(conn.errMessage)
                End If
                txtdeptlist.DataSource = ds
                txtdeptlist.DataBind()

                txtdeptlist.Items.Insert(0, New ListItem("-", ""))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
                ds = Nothing
            End Try
        End Sub

        Protected Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
            Dim sql As String
            'Dim ds As New DataSet

            Try
                If txtfind.Text = "" Then
                    Return
                End If
                'sql = "SELECT * FROM user_profile WHERE 1 = 1 "
                'If txtdeptlist.SelectedValue <> "" Then
                '    sql &= " AND dept_id = " & txtdeptlist.SelectedValue
                'End If
                'If txtfind.Text <> "" Then
                '    sql &= " AND user_fullname LIKE '%" & txtfind.Text & "%'"
                'End If

                'sql &= "  ORDER BY user_fullname ASC"
                'ds = conn.getDataSet(sql, "t1")
                'If conn.errMessage <> "" Then
                '    Throw New Exception(conn.errMessage)
                'End If
                '' Response.Write(sql)
                'ListBox1.DataSource = ds
                'ListBox1.DataBind()

                Dim searchRoot As New DirectoryEntry("LDAP://bumrungrad.com:389")

                ' We are responsible to dispose this!
                Using searchRoot
                    ' Build the search filter.
                    Dim searchFilter As String = "(&(objectClass=*)(objectClass=*)"
                    ' Add search string if specified.
                    If txtfind.Text <> "" Then
                        searchFilter += "(cn=*" + txtfind.Text + "*))"
                    Else
                        searchFilter += ")"
                    End If

                    ' Instantiate ds object.
                    Dim ds As New DirectorySearcher(searchRoot, searchFilter)

                    ' Enable paging for large queries
                    ds.PageSize = 500

                    Using searchResults As SearchResultCollection = ds.FindAll()

                        If searchResults.Count > 0 Then
                            ' make the People table and its columns
                            Dim tblPeople As New DataTable("People")

                            ' Holds the columns and rows as they are being
                            Dim col As DataColumn
                            Dim row As DataRow

                            ' Create LogonName column
                            col = New DataColumn("LogonName", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create FullName column
                            col = New DataColumn("FullName", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Details column
                            col = New DataColumn("Description", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Phone column
                            col = New DataColumn("Phone", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Mobile column
                            col = New DataColumn("Mobile", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Email column
                            col = New DataColumn("Email", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Office column
                            col = New DataColumn("Office", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create Department column
                            col = New DataColumn("Department", System.Type.[GetType]("System.String"))
                            col.DefaultValue = ""
                            tblPeople.Columns.Add(col)

                            ' Create More info column
                            col = New DataColumn("MoreInfo")
                            tblPeople.Columns.Add(col)

                            ' Iterate over all the results in the resultset.
                            For Each result As SearchResult In searchResults
                                row = tblPeople.NewRow()
                                ' Getting values
                                ' Display Name

                                If result.Properties.Contains("samaccountname") Then
                                    row("LogonName") = result.Properties("samaccountname")(0).ToString()
                                End If

                                ' Display Name
                                If result.Properties.Contains("displayName") Then
                                    Try
                                        If result.Properties.Contains("mail") Then
                                            If result.Properties("mail")(0).ToString() <> "" Then
                                                row("FullName") = result.Properties("displayName")(0).ToString() & " (" & result.Properties("mail")(0).ToString() & ")"
                                            End If
                                        Else
                                            row("FullName") = result.Properties("displayName")(0).ToString()
                                        End If
                                    Catch ex As Exception
                                        row("FullName") = result.Properties("displayName")(0).ToString()
                                    End Try


                                End If

                                ' Details
                                If result.Properties.Contains("Description") Then
                                    row("Description") = result.Properties("Description")(0).ToString()
                                End If

                                ' Telephone number

                                If result.Properties.Contains("telephoneNumber") Then
                                    row("Phone") = result.Properties("telephoneNumber")(0).ToString()
                                End If

                                ' Mobile phone
                                If result.Properties.Contains("mobile") Then
                                    row("Mobile") = result.Properties("mobile")(0).ToString()
                                End If

                                ' Email addresss (and format it)
                                If result.Properties.Contains("mail") Then
                                    If result.Properties("mail")(0).ToString() <> "" Then
                                        Dim email As String = result.Properties("mail")(0).ToString()
                                        row("Email") = email
                                    End If
                                End If

                                ' Office location

                                If result.Properties.Contains("physicalDeliveryOff iceName") Then
                                    row("Office") = result.Properties("physicalDeliveryOfficeName")(0).ToString()
                                End If

                                ' Department
                                If result.Properties.Contains("department") Then
                                    row("Department") = result.Properties("department")(0).ToString()
                                End If

                                ' If there is actually something to display, create a new table row.
                                If row("FullName") <> "" And row("Email") <> "" Then
                                    tblPeople.Rows.Add(row)
                                End If
                            Next


                            ' instantiate a new DataSet object that
                            Dim dataSet As New DataSet()
                            dataSet.Tables.Add(tblPeople)

                            ' set the data source for the grid to the people table
                            ListBox1.DataSource = dataSet
                            ListBox1.DataBind()

                        Else

                        End If
                    End Using
                End Using

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                'ds.Dispose()
                'ds = Nothing
            End Try
        End Sub

        Function getMobileForSMS(ByVal email As String) As String
            Dim sql As String
            Dim dataset As New DataSet
            Dim bhUsername As String = ""
            Dim returnMobile As String = ""
            Try

                Dim searchRoot As New DirectoryEntry("LDAP://bumrungrad.com:389", "bmc-users\h2000", "enter", AuthenticationTypes.Secure)

                ' We are responsible to dispose this!
                Using searchRoot
                    ' Build the search filter.
                    Dim searchFilter As String = "(&(objectClass=*)(objectClass=*)"
                    ' Add search string if specified.
                    searchFilter += "(cn=*" + email + "*))"
                    ' Instantiate ds object.
                    Dim ds As New DirectorySearcher(searchRoot, searchFilter)

                    ' Enable paging for large queries
                    ds.PageSize = 500

                    Using searchResults As SearchResultCollection = ds.FindAll()

                        If searchResults.Count > 0 Then

                            ' Iterate over all the results in the resultset.
                            For Each result As SearchResult In searchResults

                                If result.Properties.Contains("samaccountname") Then
                                    bhUsername = result.Properties("samaccountname")(0).ToString()
                                End If
                                Exit For
                            Next
                        Else

                        End If
                    End Using
                End Using

                If bhUsername <> "" Then
                    sql = "SELECT * FROM user_profile WHERE bh_username = '" & bhUsername & "'"
                    dataset = conn.getDataSetForTransaction(sql, "t1")
                    If dataset.Tables("t1").Rows.Count > 1 Then
                        returnMobile = dataset.Tables("t1").Rows(0)("custom_mobile").ToString
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                'ds.Dispose()
                'ds = Nothing
            End Try

            Return returnMobile
        End Function

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

            txtccselect.Value = ""
            txtidccselect.Value = ""

            txtbccselect.Value = ""
            txtIdBCCselect.Value = ""

            For i As Integer = 0 To ListBox2.Items.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                txtselect.Value &= limit & ListBox2.Items(i).Text
                txtidselect.Value &= limit & ListBox2.Items(i).Value
            Next i

            For i As Integer = 0 To ListBox3.Items.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                txtccselect.Value &= limit & ListBox3.Items(i).Text
                txtidccselect.Value &= limit & ListBox3.Items(i).Value
            Next i

            For i As Integer = 0 To ListBox4.Items.Count - 1
                If i = 0 Then
                    limit = ""
                Else
                    limit = ","
                End If
                txtBCCselect.Value &= limit & ListBox4.Items(i).Text
                txtIdBCCselect.Value &= limit & ListBox4.Items(i).Value
            Next i
        End Sub

      
  
       
        Protected Sub cmdSelect0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelect0.Click
            While ListBox1.Items.Count > 0 AndAlso ListBox1.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = ListBox1.SelectedItem
                selectedItem.Selected = False
                ListBox3.Items.Add(selectedItem)
                ListBox1.Items.Remove(selectedItem)
            End While

            onSelect()
        End Sub

        Protected Sub cmdRemove0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove0.Click
            While ListBox3.Items.Count > 0 AndAlso ListBox3.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = ListBox3.SelectedItem
                selectedItem.Selected = False
                ListBox1.Items.Add(selectedItem)
                ListBox3.Items.Remove(selectedItem)
            End While

            onSelect()
        End Sub

        Protected Sub cmdSelect1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelect1.Click
            While ListBox1.Items.Count > 0 AndAlso ListBox1.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = ListBox1.SelectedItem
                selectedItem.Selected = False
                ListBox4.Items.Add(selectedItem)
                ListBox1.Items.Remove(selectedItem)
            End While

            onSelect()
        End Sub

        Protected Sub cmdRemove1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove1.Click
            While ListBox4.Items.Count > 0 AndAlso ListBox4.SelectedItem IsNot Nothing
                Dim selectedItem As ListItem = ListBox4.SelectedItem
                selectedItem.Selected = False
                ListBox1.Items.Add(selectedItem)
                ListBox4.Items.Remove(selectedItem)
            End While

            onSelect()
        End Sub

       

        Sub bindGroup()
            Dim sql As String
            '  Dim errorMsg As String
            Dim ds As New DataSet

            Try
                sql = "SELECT * FROM address_master WHERE 1 =1  "
                If modules = "idp" Then
                    sql &= " AND contact_type = 'idp'"
                ElseIf modules = "ssip" Then
                    sql &= " AND contact_type = 'ssip'"
                ElseIf modules = "star" Then
                    sql &= " AND contact_type = 'star'"
                ElseIf modules = "ladder" Then
                    sql &= " AND contact_type = 'ladder'"
                Else
                    sql &= " AND contact_type = 'ir'"
                End If

                sql &= " ORDER BY contact_group_name ASC "
                ds = conn.getDataSetForTransaction(sql, "t1")

                txtgroup.DataSource = ds
                txtgroup.DataBind()

                '  txtgroup.Items.Insert(0, New ListItem("-- Please select --", ""))

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub cmdSaveGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSaveGroup.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim pk_sub As String = ""

            Try
                If ListBox2.Items.Count <= 0 Then
                    ' lblError.Text = "กรุณาระบุรายชื่อ Email ที่ต้องการ"
                    'Return
                End If

                If txtnewgroup.Text.Trim = "" Then
                    lblError.Text = "กรุณาระบุชื่อ Email group"
                    Return
                End If

                pk = getPK("contact_id", "address_master", conn)
                sql = "INSERT INTO address_master (contact_id , contact_group_name , contact_type) VALUES("
                sql &= "" & pk & " , "
                sql &= "'" & addslashes(txtnewgroup.Text) & "'  ,"
                sql &= "'" & modules & "'  "
                sql &= ")"

                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                For i As Integer = 0 To ListBox2.Items.Count - 1
                    pk_sub = getPK("subcontact_id", "address_detail", conn)
                    sql = "INSERT INTO address_detail (subcontact_id , contact_id , contact_name , contact_email , contact_category) VALUES("
                    sql &= "" & pk_sub & " , "
                    sql &= "'" & pk & "'  ,"
                    sql &= "'" & ListBox2.Items(i).Text & "'  ,"
                    sql &= "'" & ListBox2.Items(i).Value & "'  ,"
                    sql &= "'TO'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next

                For i As Integer = 0 To ListBox3.Items.Count - 1
                    pk_sub = getPK("subcontact_id", "address_detail", conn)
                    sql = "INSERT INTO address_detail (subcontact_id , contact_id , contact_name , contact_email , contact_category) VALUES("
                    sql &= "" & pk_sub & " , "
                    sql &= "'" & pk & "'  ,"
                    sql &= "'" & ListBox3.Items(i).Text & "'  ,"
                    sql &= "'" & ListBox3.Items(i).Value & "'  ,"
                    sql &= "'CC'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next

                conn.setDBCommit()

                bindGroup()
                txtnewgroup.Text = ""
                lblError.Text = ""
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub

        Protected Sub cmdSelectGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelectGroup.Click
            Dim sql As String
            Dim ds As New DataSet

            Try
                If txtgroup.SelectedValue = "" Or txtgroup.Items.Count <= 0 Then
                    Return
                End If

                sql = "SELECT * FROM address_detail WHERE contact_category = 'TO' AND contact_id = " & txtgroup.SelectedValue
                ds = conn.getDataSetForTransaction(sql, "t1")

                For i As Integer = 0 To ds.Tables("t1").Rows.Count - 1
                    ListBox2.Items.Add(New ListItem(ds.Tables("t1").Rows(i)("contact_name").ToString, ds.Tables("t1").Rows(i)("contact_email").ToString))
                Next i

                sql = "SELECT * FROM address_detail WHERE contact_category = 'CC' AND contact_id = " & txtgroup.SelectedValue
                ds = conn.getDataSetForTransaction(sql, "t2")

                For i As Integer = 0 To ds.Tables("t2").Rows.Count - 1
                    ListBox3.Items.Add(New ListItem(ds.Tables("t2").Rows(i)("contact_name").ToString, ds.Tables("t2").Rows(i)("contact_email").ToString))
                Next i


                onSelect()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                ds.Dispose()
            End Try
        End Sub

        Protected Sub cmdDeleteGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteGroup.Click
            Dim sql As String
            Dim errorMsg As String

            Try
                If txtgroup.SelectedValue = "" Or txtgroup.Items.Count <= 0 Then
                    Return
                End If

                sql = "DELETE FROM address_master WHERE contact_id = " & txtgroup.SelectedValue
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(ErrorPage)
                End If

                conn.setDBCommit()

                bindGroup()
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message)
            Finally

            End Try
        End Sub

        Protected Sub cmdEditGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEditGroup.Click
            Dim sql As String
            Dim errorMsg As String
            Dim pk As String = ""
            Dim pk_sub As String = ""

            Try
                If ListBox2.Items.Count <= 0 Then
                    Return
                End If

                If txtgroup.SelectedValue = "" Or txtgroup.Items.Count <= 0 Then
                    Return
                End If

                sql = "DELETE FROM address_detail WHERE contact_id = " & txtgroup.SelectedValue
                errorMsg = conn.executeSQLForTransaction(sql)
                If errorMsg <> "" Then
                    Throw New Exception(errorMsg)
                End If

                For i As Integer = 0 To ListBox2.Items.Count - 1
                    pk_sub = getPK("subcontact_id", "address_detail", conn)
                    sql = "INSERT INTO address_detail (subcontact_id , contact_id , contact_name , contact_email , contact_category) VALUES("
                    sql &= "" & pk_sub & " , "
                    sql &= "'" & txtgroup.SelectedValue & "'  ,"
                    sql &= "'" & ListBox2.Items(i).Text & "'  ,"
                    sql &= "'" & ListBox2.Items(i).Value & "'  ,"
                    sql &= "'TO'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next

                For i As Integer = 0 To ListBox3.Items.Count - 1
                    pk_sub = getPK("subcontact_id", "address_detail", conn)
                    sql = "INSERT INTO address_detail (subcontact_id , contact_id , contact_name , contact_email , contact_category) VALUES("
                    sql &= "" & pk_sub & " , "
                    sql &= "'" & txtgroup.SelectedValue & "'  ,"
                    sql &= "'" & ListBox3.Items(i).Text & "'  ,"
                    sql &= "'" & ListBox3.Items(i).Value & "'  ,"
                    sql &= "'CC'  "
                    sql &= ")"

                    errorMsg = conn.executeSQLForTransaction(sql)
                    If errorMsg <> "" Then
                        Throw New Exception(errorMsg)
                    End If
                Next

                conn.setDBCommit()

                bindGroup()
                txtnewgroup.Text = ""
            Catch ex As Exception
                conn.setDBRollback()
                Response.Write(ex.Message & sql)
            End Try
        End Sub
    End Class

End Namespace
