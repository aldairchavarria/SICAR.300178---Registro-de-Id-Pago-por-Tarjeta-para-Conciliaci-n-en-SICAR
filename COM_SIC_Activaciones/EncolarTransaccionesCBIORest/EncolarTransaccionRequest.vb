Public Class EncolarTransaccionRequest

    'INICIATIVA-219
    Dim _encolarTransaccionRequest As TransaccionAuditRequest

    Public Property encolarTransaccionRequest() As TransaccionAuditRequest
        Get
            Return _encolarTransaccionRequest
        End Get
        Set(ByVal Value As TransaccionAuditRequest)
            _encolarTransaccionRequest = Value
        End Set
    End Property
    Public Sub New()
        _encolarTransaccionRequest = New TransaccionAuditRequest
    End Sub
End Class
