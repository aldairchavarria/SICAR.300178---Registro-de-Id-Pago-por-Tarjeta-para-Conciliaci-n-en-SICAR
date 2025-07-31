Imports System.Runtime.Serialization

Public Class consultarProgramacionesResponse

    'INICIATIVA-219
    Dim _responseAudit As responseAudit
    Dim _responseData As ResponseDataProgramaciones

    Public Property responseAudit() As responseAudit
        Get
            Return _responseAudit
        End Get
        Set(ByVal Value As responseAudit)
            _responseAudit = Value
        End Set
    End Property

    Public Property responseData() As ResponseDataProgramaciones
        Get
            Return _responseData
        End Get
        Set(ByVal Value As ResponseDataProgramaciones)
            _responseData = Value
        End Set
    End Property

    Public Sub New()
        _responseAudit = New ResponseAudit
        _responseData = New ResponseDataProgramaciones
    End Sub


End Class
