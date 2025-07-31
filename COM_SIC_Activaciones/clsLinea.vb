Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsLinea
    Public NUM_TELEFONO As String
    Public PLAN As String
    Public FECHA_ACTIVAC As String
    Public ESTADO_LINEA As String
    Public FECHA_CAMB_ESTADO As String
    Public clsLinea() 'PROY-140126
    Public Sub New()
    End Sub
End Class
