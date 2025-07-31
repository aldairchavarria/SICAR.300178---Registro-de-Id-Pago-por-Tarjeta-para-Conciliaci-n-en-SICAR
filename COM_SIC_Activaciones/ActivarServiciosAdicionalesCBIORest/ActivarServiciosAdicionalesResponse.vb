Namespace ActivarServiciosAdicionalesCBIORest

    'INICIATIVA-219
    Public Class ActivarServiciosAdicionalesResponse
        Private _orderID As String
        Private _productInstanceID As String
        Private _codigoRespuesta As String
        Private _mensajeRespuesta As String

        Public Property orderID() As String
            Get
                Return _orderID
            End Get
            Set(ByVal Value As String)
                _orderID = Value
            End Set
        End Property

        Public Property productInstanceID() As String
            Get
                Return _productInstanceID
            End Get
            Set(ByVal Value As String)
                _productInstanceID = Value
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

End Namespace