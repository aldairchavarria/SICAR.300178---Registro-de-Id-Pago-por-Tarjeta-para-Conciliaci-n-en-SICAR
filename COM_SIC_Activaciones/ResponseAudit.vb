Public Class ResponseAudit

    Private _idTransaccion As String
    Private _codigoRespuesta As String
    Private _mensajeRespuesta As String

    Public Property idTransaccion() As String
        Get
            Return _idTransaccion
        End Get
        Set(ByVal Value As String)
            _idTransaccion = Value
        End Set
    End Property

    Public Property codigoRespuesta() As String
        Get
            Return _codigoRespuesta
        End Get
        Set(ByVal Value As String)
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

End Class
