Namespace ActualizarParticipante

    'INICIATIVA-219
    Public Class ActualizarParticipanteResponse

        Private _codigoRespuesta As Integer
        Private _mensajeRespuesta As String
        Private _mensajeError As String
        Private _participante As ActualizarParticipante.ParticipanteResponse
        Private _claroFault As ActualizarParticipante.ClaroFault

        Public Property codigoRespuesta() As Integer
            Get
                Return _codigoRespuesta
            End Get
            Set(ByVal Value As Integer)
                _codigoRespuesta = Value
            End Set
        End Property

        Public Property mensajeRespuesta() As String
            Get
                Return _mensajeRespuesta
            End Get
            Set(ByVal Value As String)
                _mensajeRespuesta = Value
            End Set
        End Property

        Public Property mensajeError() As String
            Get
                Return _mensajeError
            End Get
            Set(ByVal Value As String)
                _mensajeError = Value
            End Set
        End Property

        Public Property participante() As ActualizarParticipante.ParticipanteResponse
            Get
                Return _participante
            End Get
            Set(ByVal Value As ActualizarParticipante.ParticipanteResponse)
                _participante = Value
            End Set
        End Property

        Public Property claroFault() As ActualizarParticipante.ClaroFault
            Get
                Return _claroFault
            End Get
            Set(ByVal Value As ActualizarParticipante.ClaroFault)
                _claroFault = Value
            End Set
        End Property

        Public Sub New()
            _participante = New ActualizarParticipante.ParticipanteResponse
            _claroFault = New ActualizarParticipante.ClaroFault
        End Sub




    End Class

End Namespace
