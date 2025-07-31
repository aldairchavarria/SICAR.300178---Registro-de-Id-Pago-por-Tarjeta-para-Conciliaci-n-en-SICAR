Imports System.Runtime.Serialization

Public Class BorrarProgramacionResponse

    'INICIATIVA-219
    Dim _responseAudit As ResponseAudit
    Dim _responseData As ResponseDataBorrarProgramacion

    Public Property responseAudit() As ResponseAudit
        Get
            Return _responseAudit
        End Get
        Set(ByVal Value As ResponseAudit)
            _responseAudit = Value
        End Set
    End Property

    Public Property responseData() As ResponseDataBorrarProgramacion
        Get
            Return _responseData
        End Get
        Set(ByVal Value As ResponseDataBorrarProgramacion)
            _responseData = Value
        End Set
    End Property

    Public Sub New()
        _responseAudit = New ResponseAudit
        _responseData = New ResponseDataBorrarProgramacion
    End Sub
End Class
