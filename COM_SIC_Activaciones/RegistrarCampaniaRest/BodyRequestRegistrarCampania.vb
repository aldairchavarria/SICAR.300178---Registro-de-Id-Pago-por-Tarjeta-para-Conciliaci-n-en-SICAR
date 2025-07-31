'INC000002161718 inicio
Imports System

Public Class BodyRequestRegistrarCampania

    Private _registrarCampaniaRequest As RegistrarCampaniaRequest

    Public Property registrarCampaniaRequest() As registrarCampaniaRequest
        Get
            Return _registrarCampaniaRequest
        End Get
        Set(ByVal Value As registrarCampaniaRequest)
            _registrarCampaniaRequest = Value
        End Set
    End Property
    Public Sub New()
        _registrarCampaniaRequest = New RegistrarCampaniaRequest
    End Sub
End Class
