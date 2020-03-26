Imports System.Xml

Module Desencryptacion
    Private aes As New AES()
    Public CnString As String
    Dim dbcnString As String
    Public appPwUnique As String = "Punto_de_venta.My.MySetting.Encrytacion.microsoft"

    Public Function checkServer()
        Dim doc As XmlDocument = New XmlDocument()
        doc.Load("ConnectionString.xml")
        Dim root As XmlElement = doc.DocumentElement
        dbcnString = root.Attributes.Item(0).Value
        CnString = (aes.Decrypt(dbcnString, appPwUnique, Integer.Parse("256")))
        Return CnString

    End Function
End Module




















