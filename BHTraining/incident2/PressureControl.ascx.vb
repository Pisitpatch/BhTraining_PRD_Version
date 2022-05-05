
Partial Class incident_PressureControl
    Inherits System.Web.UI.UserControl

    Protected Sub cmdCalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Dim total As Integer = 0

        Try
            If txtsensory.SelectedValue <> "" Then
                total += CInt(txtsensory.SelectedValue)
            End If

            If txtmoisture.SelectedValue <> "" Then
                total += CInt(txtmoisture.SelectedValue)
            End If

            If txtactivity.SelectedValue <> "" Then
                total += CInt(txtactivity.SelectedValue)
            End If

            If txtmobility.SelectedValue <> "" Then
                total += CInt(txtmobility.SelectedValue)
            End If

            If txtnutrition.SelectedValue <> "" Then
                total += CInt(txtnutrition.SelectedValue)
            End If

            If txtfriction.SelectedValue <> "" Then
                total += CInt(txtfriction.SelectedValue)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        txtscore.Value = total
    End Sub
End Class
