Namespace ActualizarDatosFacturacion

    'INICIATIVA-219
    Public Class ActualizarDatosFacturacionResponse

        Private _adrSeq As String
        Private _responseAudit As ActualizarDatosFacturacion.ResponseAudit
        Private _listaOpcional As ActualizarDatosFacturacion.ListaOpcional

        Public Property adrSeq() As String
            Get
                Return _adrSeq
            End Get
            Set(ByVal Value As String)
                _adrSeq = Value
            End Set
        End Property

        Public Property responseAudit() As ActualizarDatosFacturacion.ResponseAudit
            Get
                Return _responseAudit
            End Get
            Set(ByVal Value As ActualizarDatosFacturacion.ResponseAudit)
                _responseAudit = Value
            End Set
        End Property


        Public Sub New()
            _responseAudit = New ActualizarDatosFacturacion.ResponseAudit
            _listaOpcional = New ActualizarDatosFacturacion.ListaOpcional
        End Sub


    End Class

End Namespace
