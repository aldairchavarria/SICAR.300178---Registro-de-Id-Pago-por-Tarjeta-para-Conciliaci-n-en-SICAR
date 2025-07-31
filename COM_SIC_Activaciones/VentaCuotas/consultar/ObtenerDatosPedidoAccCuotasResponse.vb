Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDatosPedidoAccCuotasResponse
    Private _MessageResponse As MessageResponseObtenerPedido

    Public Property MessageResponse() As MessageResponseObtenerPedido
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseObtenerPedido)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseObtenerPedido
    End Sub

End Class
