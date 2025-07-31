'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDocumentosRequest
    Private _MessageRequest As MessageRequestObtenerDocumentos

    Public Property MessageRequest() As MessageRequestObtenerDocumentos
        Get
            Return _MessageRequest
        End Get
        Set(ByVal Value As MessageRequestObtenerDocumentos)
            _MessageRequest = Value
        End Set
    End Property

    Public Sub New()
        _MessageRequest = New MessageRequestObtenerDocumentos
    End Sub

End Class
