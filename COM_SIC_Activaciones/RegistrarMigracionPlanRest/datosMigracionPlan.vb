Public Class datosMigracionPlan

    Private _datosCliente As datosCliente
    Private _datosLinea As datosLinea
    Private _flagAplicaOCC As String
    Private _montoOCC As String
    Private _canal As String
    Private _codigoPuntoVenta As String
    Private _sec As String
    Private _tipoOperacion As String
    Private _fechaCreSolicitud As String
    Private _serviciosAdicionales As ArrayList

    Public Property datosCliente() As datosCliente
        Get
            Return _datosCliente
        End Get
        Set(ByVal Value As datosCliente)
            _datosCliente = Value
        End Set
    End Property

    Public Property datosLinea() As datosLinea
        Get
            Return _datosLinea
        End Get
        Set(ByVal Value As datosLinea)
            _datosLinea = Value
        End Set
    End Property

    Public Property flagAplicaOCC() As String
        Get
            Return _flagAplicaOCC
        End Get
        Set(ByVal Value As String)
            _flagAplicaOCC = Value
        End Set
    End Property

    Public Property montoOCC() As String
        Get
            Return _montoOCC
        End Get
        Set(ByVal Value As String)
            _montoOCC = Value
        End Set
    End Property

    Public Property canal() As String
        Get
            Return _canal
        End Get
        Set(ByVal Value As String)
            _canal = Value
        End Set
    End Property

    Public Property codigoPuntoVenta() As String
        Get
            Return _codigoPuntoVenta
        End Get
        Set(ByVal Value As String)
            _codigoPuntoVenta = Value
        End Set
    End Property

    Public Property sec() As String
        Get
            Return _sec
        End Get
        Set(ByVal Value As String)
            _sec = Value
        End Set
    End Property

    Public Property tipoOperacion() As String
        Get
            Return _tipoOperacion
        End Get
        Set(ByVal Value As String)
            _tipoOperacion = Value
        End Set
    End Property

    Public Property fechaCreSolicitud() As String
        Get
            Return _fechaCreSolicitud
        End Get
        Set(ByVal Value As String)
            _fechaCreSolicitud = Value
        End Set
    End Property

    Public Property serviciosAdicionales() As ArrayList
        Get
            Return _serviciosAdicionales
        End Get
        Set(ByVal Value As ArrayList)
            _serviciosAdicionales = Value
        End Set
    End Property

    Public Sub New()
        _datosCliente = New datosCliente
        _datosLinea = New datosLinea
    End Sub

End Class
