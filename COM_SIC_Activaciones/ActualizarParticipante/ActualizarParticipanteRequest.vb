Namespace ActualizarParticipante

    'INICIATIVA-219
    Public Class ActualizarParticipanteRequest

        Private _codigoClienteUnico As String
        Private _participanteId As String
        Private _tipoDocumento As String
        Private _numeroDocumento As String
        Private _nombres As String
        Private _apellidoMaterno As String
        Private _apellidoPaterno As String
        Private _razonSocial As String
        Private _participante As ActualizarParticipante.ParticipanteRequest

        Public Property codigoClienteUnico() As String
            Get
                Return _codigoClienteUnico
            End Get
            Set(ByVal Value As String)
                _codigoClienteUnico = Value
            End Set
        End Property

        Public Property participanteId() As String
            Get
                Return _participanteId
            End Get
            Set(ByVal Value As String)
                _participanteId = Value
            End Set
        End Property

        Public Property tipoDocumento() As String
            Get
                Return _tipoDocumento
            End Get
            Set(ByVal Value As String)
                _tipoDocumento = Value
            End Set
        End Property

        Public Property numeroDocumento() As String
            Get
                Return _numeroDocumento
            End Get
            Set(ByVal Value As String)
                _numeroDocumento = Value
            End Set
        End Property

        Public Property nombres() As String
            Get
                Return _nombres
            End Get
            Set(ByVal Value As String)
                _nombres = Value
            End Set
        End Property

        Public Property apellidoMaterno() As String
            Get
                Return _apellidoMaterno
            End Get
            Set(ByVal Value As String)
                _apellidoMaterno = Value
            End Set
        End Property

        Public Property apellidoPaterno() As String
            Get
                Return _apellidoPaterno
            End Get
            Set(ByVal Value As String)
                _apellidoPaterno = Value
            End Set
        End Property

        Public Property razonSocial() As String
            Get
                Return _razonSocial
            End Get
            Set(ByVal Value As String)
                _razonSocial = Value
            End Set
        End Property

        Public Property participante() As ActualizarParticipante.ParticipanteRequest
            Get
                Return _participante
            End Get
            Set(ByVal Value As ActualizarParticipante.ParticipanteRequest)
                _participante = Value
            End Set
        End Property

        Public Sub New()
            _participante = New ActualizarParticipante.ParticipanteRequest
        End Sub

    End Class

End Namespace

