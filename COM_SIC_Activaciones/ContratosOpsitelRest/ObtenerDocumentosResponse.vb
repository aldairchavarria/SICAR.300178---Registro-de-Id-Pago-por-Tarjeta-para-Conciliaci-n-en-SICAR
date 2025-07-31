'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDocumentosResponse
    Private _MessageResponse As MessageResponseObtenerDocumentos

    Public Property MessageResponse() As MessageResponseObtenerDocumentos
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseObtenerDocumentos)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseObtenerDocumentos
    End Sub

End Class
