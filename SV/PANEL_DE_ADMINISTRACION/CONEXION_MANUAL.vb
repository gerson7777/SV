Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Xml

Public Class CONEXION_MANUAL
    Private aes As New AES()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtCnStirng.Text = "" Then
            MessageBox.Show("Por favor ingrese una cadena", "Campos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            SavetoXML(aes.Encrypt(txtCnStirng.Text, appPwUnique, Integer.Parse("256")))
            mostrar_usuarios()
        End If
    End Sub
    Sub mostrar_usuarios()
        Dim conectionTest As New SqlConnection(txtCnStirng.Text)
        Dim idusuario As String
        Dim com As New SqlCommand("select idUsuario from usuario", conectionTest)
        Try
            conectionTest.Open()
            idusuario = (com.ExecuteScalar())
            conectionTest.Close()
            MessageBox.Show("Conexion Creada Correctamente", "Conexion con Exito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error de Conexion", "Fallo la conexion", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


    End Sub
    Public Sub SavetoXML(ByVal dbcnString)
        Dim doc As XmlDocument = New XmlDocument()
        doc.Load("ConnectionString.xml")
        Dim root As XmlElement = doc.DocumentElement
        root.Attributes.Item(0).Value = dbcnString
        Dim writer As XmlTextWriter = New XmlTextWriter("ConnectionString.xml", Nothing)
        writer.Formatting = Formatting.Indented
        doc.Save(writer)
        writer.Close()
    End Sub

    Dim dbcnString As String
    Public Sub ReadfromXML()
        Try
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load("ConnectionString.xml")
            Dim root As XmlElement = doc.DocumentElement
            dbcnString = root.Attributes.Item(0).Value
            txtCnStirng.Text = (aes.Decrypt(dbcnString, appPwUnique, Integer.Parse("256")))
        Catch ex As System.Security.Cryptography.CryptographicException

        End Try
    End Sub

    Private Sub CONEXION_MANUAL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReadfromXML()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()

    End Sub
End Class