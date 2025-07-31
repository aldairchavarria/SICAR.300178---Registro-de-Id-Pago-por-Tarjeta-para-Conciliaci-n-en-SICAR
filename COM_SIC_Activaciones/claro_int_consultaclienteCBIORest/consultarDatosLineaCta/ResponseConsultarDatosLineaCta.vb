Namespace claro_int_consultaclienteCBIORest.consultarDatosLineaCta

    'INICIATIVA-219
    Public Class ResponseConsultarDatosLineaCta

        Private _consultarDatosLineaCtaResponse As consultarDatosLineaCtaResponse

        Public Property consultarDatosLineaCtaResponse() As consultarDatosLineaCtaResponse
            Get
                Return _consultarDatosLineaCtaResponse
            End Get
            Set(ByVal Value As consultarDatosLineaCtaResponse)
                _consultarDatosLineaCtaResponse = Value
            End Set
        End Property

        Public Sub New()
            _consultarDatosLineaCtaResponse = New consultarDatosLineaCtaResponse
        End Sub
    End Class

End Namespace