Imports System

Public Class CancelarOrdenRequest

    Private _MessageRequest As MessageRequestCancelarOrden

    Public Property MessageRequest() As MessageRequestCancelarOrden
        Get
            Return _MessageRequest
        End Get
        Set(ByVal Value As MessageRequestCancelarOrden)
            _MessageRequest = Value
        End Set
    End Property

    Public Sub New()
        _MessageRequest = New MessageRequestCancelarOrden
    End Sub

End Class
