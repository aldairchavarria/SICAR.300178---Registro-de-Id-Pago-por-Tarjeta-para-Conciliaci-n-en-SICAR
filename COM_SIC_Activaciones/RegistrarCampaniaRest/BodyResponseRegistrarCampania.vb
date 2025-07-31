'INC000002161718 inicio
Imports System

Public Class BodyResponseRegistrarCampania

    Private _registrarCampaniaResponse As registrarCampaniaResponse

    Public Property registrarCampaniaResponse() As registrarCampaniaResponse
        Get
            Return _registrarCampaniaResponse
        End Get
        Set(ByVal Value As registrarCampaniaResponse)
            _registrarCampaniaResponse = Value
        End Set
    End Property


    Public Sub New()
        _registrarCampaniaResponse = New RegistrarCampaniaResponse
    End Sub
End Class
