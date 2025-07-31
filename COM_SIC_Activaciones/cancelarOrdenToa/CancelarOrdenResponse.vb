'INC000002161718 inicio
Imports System
Public Class CancelarOrdenResponse

    Private _MessageResponse As MessageResponseCancelarOrden

    Public Property MessageResponse() As MessageResponseCancelarOrden
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseCancelarOrden)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseCancelarOrden
    End Sub

End Class
