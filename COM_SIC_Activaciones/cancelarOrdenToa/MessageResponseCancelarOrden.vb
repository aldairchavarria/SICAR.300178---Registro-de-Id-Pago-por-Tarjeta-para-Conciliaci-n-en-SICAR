'INC000002161718 inicio
Imports System

Public Class MessageResponseCancelarOrden
    Private _Header As HeadersResponse
    Private _Body As BodyResponseCancelarOrden

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseCancelarOrden
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseCancelarOrden)
            _Body = Value
        End Set
    End Property


    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseCancelarOrden
    End Sub
End Class
