'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageResponseConsultarVentCont
    Private _Header As HeadersResponse
    Private _Body As BodyResponseConsultarVentCont

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseConsultarVentCont
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseConsultarVentCont)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseConsultarVentCont
    End Sub

End Class
