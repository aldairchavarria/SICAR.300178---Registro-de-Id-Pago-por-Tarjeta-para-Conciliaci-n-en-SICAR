'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageResponseObtenerDocumentos
    Private _Header As HeadersResponse
    Private _Body As BodyResponseObtenerDocumentos

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseObtenerDocumentos
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseObtenerDocumentos)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseObtenerDocumentos
    End Sub
End Class
