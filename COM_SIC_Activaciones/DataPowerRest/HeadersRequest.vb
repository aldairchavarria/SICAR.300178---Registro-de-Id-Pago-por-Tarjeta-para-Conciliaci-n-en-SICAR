'INC000002161718 inicio
Imports System

Public Class HeadersRequest

    Private _HeaderRequest As HeaderRequest

    Public Property HeaderRequest() As HeaderRequest
        Get
            Return _HeaderRequest
        End Get
        Set(ByVal Value As HeaderRequest)
            _HeaderRequest = Value
        End Set
    End Property

    Public Sub New()
        _HeaderRequest = New HeaderRequest
    End Sub

End Class
