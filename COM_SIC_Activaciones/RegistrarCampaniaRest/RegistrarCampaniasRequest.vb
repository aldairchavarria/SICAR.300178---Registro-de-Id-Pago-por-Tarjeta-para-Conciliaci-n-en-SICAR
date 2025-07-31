'INC000002161718 inicio
Imports System

Public Class RegistrarCampaniasRequest
    Private _MessageRequest As MessageRequestRegistarCampania

    Public Property MessageRequest() As MessageRequestRegistarCampania
        Get
            Return _MessageRequest
        End Get
        Set(ByVal Value As MessageRequestRegistarCampania)
            _MessageRequest = Value
        End Set
    End Property

    Public Sub New()
        _MessageRequest = New MessageRequestRegistarCampania
    End Sub
End Class
