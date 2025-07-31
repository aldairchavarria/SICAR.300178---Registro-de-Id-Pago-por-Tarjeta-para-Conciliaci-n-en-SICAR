Public Class TransaccionAuditResponse

    'INICIATIVA-219
    Private _responseAudit As responseAudit
    Private _responseData As responseData

    Public Property responseAudit() As responseAudit
        Get
            Return _responseAudit
        End Get
        Set(ByVal Value As responseAudit)
            _responseAudit = Value
        End Set
    End Property

    Public Property responseData() As responseData
        Get
            Return _responseData
        End Get
        Set(ByVal Value As responseData)
            _responseData = Value
        End Set
    End Property

    Public Sub New()
        _responseAudit = New ResponseAudit
        _responseData = New ResponseData
    End Sub
End Class
