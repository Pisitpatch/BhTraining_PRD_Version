Imports System.Data
Imports System.DirectoryServices

Partial Class ad
    Inherits System.Web.UI.Page
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim searchRoot As New DirectoryEntry("LDAP://bumrungrad.com:389", "bmc-users\h2000", "enter", AuthenticationTypes.Secure)

        ' We are responsible to dispose this!
        Using searchRoot
            ' Build the search filter.
            Dim searchFilter As String = "(&(objectClass=person)(objectClass=user)"
            ' Add search string if specified.
            If txtSearch.Text <> "" Then
                searchFilter += "(cn=*" + txtSearch.Text + "*))"
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
                            row("FullName") = result.Properties("displayName")(0).ToString()
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
                                row("Email") = "<a href=""mailto:" + email + """ title=""Send mail to " + email + """>" + email + "</a>"
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
                        If row("Phone") <> "" OrElse row("Mobile") <> "" OrElse row("Email") <> "" OrElse row("Office") <> "" OrElse row("Department") <> "" Then
                            tblPeople.Rows.Add(row)
                        End If
                    Next

                    ' instantiate a new DataSet object that
                    'Dim dataSet As New DataSet()
                    'dataSet.Tables.Add(tblPeople)

                    ' set the data source for the grid to the people table
                    'gvSearchResult.DataSource = dataSet.Tables("People")
                    'gvSearchResult.DataBind()



                    ' instantiate a new DataSet object that
                    Dim dataSet As New DataSet()
                    dataSet.Tables.Add(tblPeople)

                    ' set the data source for the grid to the people table
                    gvSearchResult.DataSource = dataSet.Tables("People")
                    gvSearchResult.DataBind()





                    lblSearchStatus.Visible = True
                    lblSearchStatus.Text = tblPeople.Rows.Count.ToString() + " matches found."
                Else
                    lblSearchStatus.Visible = True
                    lblSearchStatus.Text = "No matches found..."
                End If
            End Using
        End Using
    End Sub
End Class
