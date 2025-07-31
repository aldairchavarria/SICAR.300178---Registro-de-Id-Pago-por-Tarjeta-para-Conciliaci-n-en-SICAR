'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BodyRequestObtenerDocumentos

    Private _obtenerDocumentoRequest As ObtenerDocumentos

    Public Property obtenerDocumentoRequest() As ObtenerDocumentos
        Get
            Return _obtenerDocumentoRequest
        End Get
        Set(ByVal Value As ObtenerDocumentos)
            _obtenerDocumentoRequest = Value
        End Set
    End Property

    Public Sub New()
        _obtenerDocumentoRequest = New ObtenerDocumentos
    End Sub

End Class
