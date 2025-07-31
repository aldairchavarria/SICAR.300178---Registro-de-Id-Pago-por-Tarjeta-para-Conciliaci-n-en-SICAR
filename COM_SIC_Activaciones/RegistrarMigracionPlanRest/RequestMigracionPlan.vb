Public Class RequestMigracionPlan

    Private _programarMigracionPlanRequest As programarMigracionPlanRequest

    Public Property programarMigracionPlanRequest() As programarMigracionPlanRequest
        Get
            Return _programarMigracionPlanRequest
        End Get
        Set(ByVal Value As programarMigracionPlanRequest)
            _programarMigracionPlanRequest = Value
        End Set
    End Property

    Public Sub New()
        _programarMigracionPlanRequest = New programarMigracionPlanRequest
    End Sub

End Class
