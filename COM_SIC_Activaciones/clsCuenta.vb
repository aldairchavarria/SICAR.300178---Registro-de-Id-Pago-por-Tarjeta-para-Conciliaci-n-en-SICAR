Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsCuenta

    Public CS_ID As String
    Public CODIGO_BSCS As String
    Public CS_LEVEL_CODE As String
    Public CS_ID_HIGH As String
    Public CODIGO_BSCS_HIGH As String
    Public NOMBRE_TITULAR As String
    Public DIRECCION_DESCRIPCION As String
    Public REFERENCIA As String
    Public DESC_DPTO As String
    Public DESC_PROV As String
    Public DESC_DIST As String
    Public COD_UBIGEO As String
    Public LIM_CREDITO As String
    Public ESTADO_CUENTA As String
    Public RESPONS_PAGO As String
    Public linea() As clsLinea
    Public clsCuenta() 'PROY-140126

    Public Sub New()
    End Sub
End Class
