'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageRequestObtenerDocumentos

    Private _Header As HeadersRequest
    Private _Body As BodyRequestObtenerDocumentos

    Public Property Header() As HeadersRequest
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersRequest)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyRequestObtenerDocumentos
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyRequestObtenerDocumentos)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersRequest
        _Body = New BodyRequestObtenerDocumentos
    End Sub

End Class
