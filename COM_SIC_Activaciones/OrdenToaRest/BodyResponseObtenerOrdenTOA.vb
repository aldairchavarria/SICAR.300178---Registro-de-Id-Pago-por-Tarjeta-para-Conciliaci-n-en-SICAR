Public Class BodyResponseObtenerOrdenTOA

    Private _auditResponse As BEAuditResponse
    Private _responseData As ResponseDataTOA

    Public Property auditResponse() As BEAuditResponse
        Get
            Return _auditResponse
        End Get
        Set(ByVal Value As BEAuditResponse)
            _auditResponse = Value
        End Set
    End Property

    Public Property responseData() As ResponseDataTOA
        Get
            Return _responseData
        End Get
        Set(ByVal Value As ResponseDataTOA)
            _responseData = Value
        End Set
    End Property

    Public Sub New()
        _auditResponse = New BEAuditResponse
        _responseData = New ResponseDataTOA
    End Sub

End Class
