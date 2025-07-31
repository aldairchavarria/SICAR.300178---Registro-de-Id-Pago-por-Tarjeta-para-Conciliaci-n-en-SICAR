Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageResponseObtenerPedido
    Private _Header As HeadersResponse
    Private _Body As BodyResponseObtenerDatosPedidoAcc

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseObtenerDatosPedidoAcc
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseObtenerDatosPedidoAcc)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseObtenerDatosPedidoAcc
    End Sub
End Class
