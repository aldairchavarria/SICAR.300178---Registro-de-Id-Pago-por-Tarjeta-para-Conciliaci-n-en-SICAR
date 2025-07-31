'INC000002161718 inicio
Imports System

Public Class MessageRequestRegistarCampania

    Private _Header As HeadersRequest
    Private _Body As BodyRequestRegistrarCampania

    Public Property Header() As HeadersRequest
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersRequest)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyRequestRegistrarCampania
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyRequestRegistrarCampania)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersRequest
        _Body = New BodyRequestRegistrarCampania
    End Sub

End Class
