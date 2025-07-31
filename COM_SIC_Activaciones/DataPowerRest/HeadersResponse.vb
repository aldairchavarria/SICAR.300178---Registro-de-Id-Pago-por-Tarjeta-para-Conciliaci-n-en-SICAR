'INC000002161718 inicio
Imports System

Public Class HeadersResponse

    Private _HeaderResponse As HeaderResponse

    Public Property HeaderResponse() As HeaderResponse
        Get
            Return _HeaderResponse
        End Get
        Set(ByVal Value As HeaderResponse)
            _HeaderResponse = Value
        End Set
    End Property

    Public Sub New()
        _HeaderResponse = New HeaderResponse
    End Sub
End Class
