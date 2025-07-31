Public Class ResponseMigracionPlan

    Private _programarMigracionPlanResponse As programarMigracionPlanResponse

    Public Property programarMigracionPlanResponse() As programarMigracionPlanResponse
        Get
            Return _programarMigracionPlanResponse
        End Get
        Set(ByVal Value As programarMigracionPlanResponse)
            _programarMigracionPlanResponse = Value
        End Set
    End Property

    Public Sub New()
        _programarMigracionPlanResponse = New programarMigracionPlanResponse
    End Sub

End Class
