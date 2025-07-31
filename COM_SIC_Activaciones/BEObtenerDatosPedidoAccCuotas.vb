Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BEObtenerDatosPedidoAccCuotas

    Private _codPromocion As String
    Private _descPromocion As String
    Private _codAccesorio As String
    Private _grupoMaterial As String
    Private _tipoProdFacturar As String
    Private _servidorVenta As String
    Private _codTipoTecnologia As String
    Private _descTipoTecnologia As String
    Private _numeroPedido As String
    Private _lineaFacturar As String
    Private _modalidadVenta As String
    Private _flagCargoRecibo As String
    Private _cuotaInicialFinal As String
    Private _tipoDocEvalCred As String
    Private _numeroSec As String
    Private _cuocCodigo As String
    Private _cuotasBrms As String
    Private _precioLista As String
    Private _cargoFijo As String
    Private _cuotaInicial As String
    Private _customerID As String
    Private _coID As String
    Private _cuentaFacturar As String
    Private _descPlanFijo As String
    Private _idVentaCuota As String
    Private _numeroCuotas As String
    Private _numDocEvalCred As String
    Private _flagRRLLEvalCred As String
    Private _flagPagoSec As String
    Private _servidorSEC As String

    Public Property codPromocion() As String
        Get
            Return _codPromocion
        End Get
        Set(ByVal Value As String)
            _codPromocion = Value
        End Set
    End Property

    Public Property descPromocion() As String
        Get
            Return _descPromocion
        End Get
        Set(ByVal Value As String)
            _descPromocion = Value
        End Set
    End Property

    Public Property codAccesorio() As String
        Get
            Return _codAccesorio
        End Get
        Set(ByVal Value As String)
            _codAccesorio = Value
        End Set
    End Property

    Public Property grupoMaterial() As String
        Get
            Return _grupoMaterial
        End Get
        Set(ByVal Value As String)
            _grupoMaterial = Value
        End Set
    End Property

    Public Property tipoProdFacturar() As String
        Get
            Return _tipoProdFacturar
        End Get
        Set(ByVal Value As String)
            _tipoProdFacturar = Value
        End Set
    End Property

    Public Property servidorVenta() As String
        Get
            Return _servidorVenta
        End Get
        Set(ByVal Value As String)
            _servidorVenta = Value
        End Set
    End Property

    Public Property codTipoTecnologia() As String
        Get
            Return _codTipoTecnologia
        End Get
        Set(ByVal Value As String)
            _codTipoTecnologia = Value
        End Set
    End Property

    Public Property descTipoTecnologia() As String
        Get
            Return _descTipoTecnologia
        End Get
        Set(ByVal Value As String)
            _descTipoTecnologia = Value
        End Set
    End Property

    Public Property numeroPedido() As String
        Get
            Return _numeroPedido
        End Get
        Set(ByVal Value As String)
            _numeroPedido = Value
        End Set
    End Property

    Public Property lineaFacturar() As String
        Get
            Return _lineaFacturar
        End Get
        Set(ByVal Value As String)
            _lineaFacturar = Value
        End Set
    End Property

    Public Property modalidadVenta() As String
        Get
            Return _modalidadVenta
        End Get
        Set(ByVal Value As String)
            _modalidadVenta = Value
        End Set
    End Property

    Public Property flagCargoRecibo() As String
        Get
            Return _flagCargoRecibo
        End Get
        Set(ByVal Value As String)
            _flagCargoRecibo = Value
        End Set
    End Property

    Public Property cuotaInicialFinal() As String
        Get
            Return _cuotaInicialFinal
        End Get
        Set(ByVal Value As String)
            _cuotaInicialFinal = Value
        End Set
    End Property

    Public Property tipoDocEvalCred() As String
        Get
            Return _tipoDocEvalCred
        End Get
        Set(ByVal Value As String)
            _tipoDocEvalCred = Value
        End Set
    End Property

    Public Property numeroSec() As String
        Get
            Return _numeroSec
        End Get
        Set(ByVal Value As String)
            _numeroSec = Value
        End Set
    End Property

    Public Property cuocCodigo() As String
        Get
            Return _cuocCodigo
        End Get
        Set(ByVal Value As String)
            _cuocCodigo = Value
        End Set
    End Property

    Public Property cuotasBrms() As String
        Get
            Return _cuotasBrms
        End Get
        Set(ByVal Value As String)
            _cuotasBrms = Value
        End Set
    End Property

    Public Property precioLista() As String
        Get
            Return _precioLista
        End Get
        Set(ByVal Value As String)
            _precioLista = Value
        End Set
    End Property

    Public Property cargoFijo() As String
        Get
            Return _cargoFijo
        End Get
        Set(ByVal Value As String)
            _cargoFijo = Value
        End Set
    End Property

    Public Property cuotaInicial() As String
        Get
            Return _cuotaInicial
        End Get
        Set(ByVal Value As String)
            _cuotaInicial = Value
        End Set
    End Property

    Public Property customerID() As String
        Get
            Return _customerID
        End Get
        Set(ByVal Value As String)
            _customerID = Value
        End Set
    End Property

    Public Property coID() As String
        Get
            Return _coID
        End Get
        Set(ByVal Value As String)
            _coID = Value
        End Set
    End Property

    Public Property cuentaFacturar() As String
        Get
            Return _cuentaFacturar
        End Get
        Set(ByVal Value As String)
            _cuentaFacturar = Value
        End Set
    End Property

    Public Property descPlanFijo() As String
        Get
            Return _descPlanFijo
        End Get
        Set(ByVal Value As String)
            _descPlanFijo = Value
        End Set
    End Property

    Public Property idVentaCuota() As String
        Get
            Return _idVentaCuota
        End Get
        Set(ByVal Value As String)
            _idVentaCuota = Value
        End Set
    End Property

    Public Property numeroCuotas() As String
        Get
            Return _numeroCuotas
        End Get
        Set(ByVal Value As String)
            _numeroCuotas = Value
        End Set
    End Property

    Public Property numDocEvalCred() As String
        Get
            Return _numDocEvalCred
        End Get
        Set(ByVal Value As String)
            _numDocEvalCred = Value
        End Set
    End Property

    Public Property flagRRLLEvalCred() As String
        Get
            Return _flagRRLLEvalCred
        End Get
        Set(ByVal Value As String)
            _flagRRLLEvalCred = Value
        End Set
    End Property

    Public Property flagPagoSec() As String
        Get
            Return _flagPagoSec
        End Get
        Set(ByVal Value As String)
            _flagPagoSec = Value
        End Set
    End Property

    Public Property servidorSEC() As String
        Get
            Return _servidorSEC
        End Get
        Set(ByVal Value As String)
            _servidorSEC = Value
        End Set
    End Property

End Class
