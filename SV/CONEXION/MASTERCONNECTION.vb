Imports System.Data.SqlClient

Module MASTERCONNECTION
    Public conection As New SqlConnection(checkServer)
    Sub open()
        If conection.State = 0 Then
            conection.Open()
        End If
    End Sub
    Sub closeCnt()
        If conection.State = 1 Then
            conection.Close()
        End If
    End Sub

End Module
