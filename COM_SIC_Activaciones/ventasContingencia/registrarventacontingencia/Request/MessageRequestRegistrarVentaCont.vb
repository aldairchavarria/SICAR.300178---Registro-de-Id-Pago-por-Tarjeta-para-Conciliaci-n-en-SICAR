'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageRequestRegistrarVentaCont
    Private _Header As HeadersRequest
    Private _Body As BodyRequestRegistrarVentaCont

    Public Property Header() As HeadersRequest
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersRequest)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyRequestRegistrarVentaCont
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyRequestRegistrarVentaCont)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersRequest
        _Body = New BodyRequestRegistrarVentaCont
    End Sub

End Class
