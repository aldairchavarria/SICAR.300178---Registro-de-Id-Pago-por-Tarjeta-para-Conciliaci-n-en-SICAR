Public Class EncolarTransaccionResponse

    'INICIATIVA-219
    Dim _encolarReposicionResponse As TransaccionAuditResponse

    Public Property encolarReposicionResponse() As TransaccionAuditResponse
        Get
            Return _encolarReposicionResponse
        End Get
        Set(ByVal Value As TransaccionAuditResponse)
            _encolarReposicionResponse = Value
        End Set
    End Property
    Public Sub New()
        _encolarReposicionResponse = New TransaccionAuditResponse
    End Sub

End Class
