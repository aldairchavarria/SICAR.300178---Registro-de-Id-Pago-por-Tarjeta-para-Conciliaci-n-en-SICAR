'INC000002161718 inicio
Imports System

Public Class MessageResponseRegistarCampania
    Private _Header As HeadersResponse
    Private _Body As BodyResponseRegistrarCampania

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseRegistrarCampania
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseRegistrarCampania)
            _Body = Value
        End Set
    End Property


    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseRegistrarCampania
    End Sub
End Class
