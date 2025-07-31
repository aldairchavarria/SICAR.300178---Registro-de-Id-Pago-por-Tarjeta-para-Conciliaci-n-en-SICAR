'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BodyResponseObtenerDocumentos

    Private _obtenerDocumentoResponse As ObtenerDocumentoResponse

    Public Property obtenerDocumentoResponse() As ObtenerDocumentoResponse
        Get
            Return _obtenerDocumentoResponse
        End Get
        Set(ByVal Value As obtenerDocumentoResponse)
            _obtenerDocumentoResponse = Value
        End Set
    End Property

    Public Sub New()
        _obtenerDocumentoResponse = New ObtenerDocumentoResponse
    End Sub

End Class
