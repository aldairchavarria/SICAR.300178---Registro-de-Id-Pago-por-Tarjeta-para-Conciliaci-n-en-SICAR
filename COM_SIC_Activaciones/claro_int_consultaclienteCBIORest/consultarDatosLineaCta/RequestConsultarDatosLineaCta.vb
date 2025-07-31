Namespace claro_int_consultaclienteCBIORest.consultarDatosLineaCta

    'INICIATIVA-219
    Public Class RequestConsultarDatosLineaCta

        Private _consultarDatosLineaCtaRequest As consultarDatosLineaCtaRequest

        Public Property consultarDatosLineaCtaRequest() As consultarDatosLineaCtaRequest
            Get
                Return _consultarDatosLineaCtaRequest
            End Get
            Set(ByVal Value As consultarDatosLineaCtaRequest)
                _consultarDatosLineaCtaRequest = Value
            End Set
        End Property

        Public Sub New()
            _consultarDatosLineaCtaRequest = New consultarDatosLineaCtaRequest
        End Sub
    End Class

End Namespace