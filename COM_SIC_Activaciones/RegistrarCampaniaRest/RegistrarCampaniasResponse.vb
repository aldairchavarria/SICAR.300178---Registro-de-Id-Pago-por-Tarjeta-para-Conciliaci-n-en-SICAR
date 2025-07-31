'INC000002161718 inicio
Imports System
Public Class RegistrarCampaniasResponse

    Private _MessageResponse As MessageResponseRegistarCampania

    Public Property MessageResponse() As MessageResponseRegistarCampania
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseRegistarCampania)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseRegistarCampania
    End Sub

End Class
