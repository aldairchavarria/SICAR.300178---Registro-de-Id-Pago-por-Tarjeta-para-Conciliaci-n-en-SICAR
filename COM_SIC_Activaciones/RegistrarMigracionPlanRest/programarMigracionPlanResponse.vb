Public Class programarMigracionPlanResponse

    Private _responseAudit As ResponseAudit
    Private _listaOpcional As ArrayList

    Public Property responseAudit() As ResponseAudit
        Get
            Return _responseAudit
        End Get
        Set(ByVal Value As ResponseAudit)
            _responseAudit = Value
        End Set
    End Property

    Public Property listaOpcional() As ArrayList
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As ArrayList)
            _listaOpcional = Value
        End Set
    End Property

    Public Sub New()
        _responseAudit = New ResponseAudit
        _listaOpcional = New ArrayList
    End Sub

End Class
