Public Class BECambioPlanCBIO

    Public _CanalVenta As String
    Public _SegmentoCliente As String
    Public _TipoDocumento As String
    Public _NumeroDocumento As String
    Public _FechaNacimiento As String
    Public _GeneroCliente As String
    Public _ComportamientoPago As String
    Public _NumeroSec As String
    Public _TipoOperacion As String
    Public _FechaSolicitudReno As String
    Public _CodigoInterlocutor As String
    Public _CodigoOficinaVenta As String
    Public _NumeroLinea As String
    Public _Iccid As String
    Public _CasoEspecial As String
    Public _Campana As String
    Public _TopeConsumo As String
    Public _MontoTopeConsumo As String
    Public _Correo As String
    Public _FlagCorreo As String
    Public _MarcaEquipo As String
    Public _ModeloEquipo As String
    Public _PrecioVenta As String
    Public _FechaInicioContratoActual As String
    Public _FechaActivacionAlta As String
    Public _TipoProducto As String 'Descripcion del Tipo de Producto
    Public _TipoTecnologia As String
    Public _PoIdNuevo As String 'Codigo del plan de cbio
    Public _PoIdNuevoDescripcion As String
    Public _ModalidadPago As String 'ContratoCode - Cuotas
    Public _CargoFijoNuevoPlan As String
    Public _PlazoContratoActual As String
    Public _PoIdActual As String 'Codigo del plan de cbio
    Public _PoIdActualDescripcion As String
    Public _CargoFijoActualPlan As String
    Public _FechaTransaccion As String
    Public _TipoSuscripcion As String 'Postpago
    Public _FlagMontoOCC As String
    Public _MontoOCC As String

    Public Property CanalVenta() As String
        Get
            Return _CanalVenta
        End Get
        Set(ByVal Value As String)
            _CanalVenta = Value
        End Set
    End Property

    Public Property SegmentoCliente() As String
        Get
            Return _SegmentoCliente
        End Get
        Set(ByVal Value As String)
            _SegmentoCliente = Value
        End Set
    End Property

    Public Property TipoDocumento() As String
        Get
            Return _TipoDocumento
        End Get
        Set(ByVal Value As String)
            _TipoDocumento = Value
        End Set
    End Property

    Public Property NumeroDocumento() As String
        Get
            Return _NumeroDocumento
        End Get
        Set(ByVal Value As String)
            _NumeroDocumento = Value
        End Set
    End Property

    Public Property FechaNacimiento() As String
        Get
            Return _FechaNacimiento
        End Get
        Set(ByVal Value As String)
            _FechaNacimiento = Value
        End Set
    End Property

    Public Property GeneroCliente() As String
        Get
            Return _GeneroCliente
        End Get
        Set(ByVal Value As String)
            _GeneroCliente = Value
        End Set
    End Property

    Public Property ComportamientoPago() As String
        Get
            Return _ComportamientoPago
        End Get
        Set(ByVal Value As String)
            _ComportamientoPago = Value
        End Set
    End Property

    Public Property NumeroSec() As String
        Get
            Return _NumeroSec
        End Get
        Set(ByVal Value As String)
            _NumeroSec = Value
        End Set
    End Property

    Public Property TipoOperacion() As String
        Get
            Return _TipoOperacion
        End Get
        Set(ByVal Value As String)
            _TipoOperacion = Value
        End Set
    End Property

    Public Property FechaSolicitudReno() As String
        Get
            Return _FechaSolicitudReno
        End Get
        Set(ByVal Value As String)
            _FechaSolicitudReno = Value
        End Set
    End Property

    Public Property CodigoInterlocutor() As String
        Get
            Return _CodigoInterlocutor
        End Get
        Set(ByVal Value As String)
            _CodigoInterlocutor = Value
        End Set
    End Property

    Public Property CodigoOficinaVenta() As String
        Get
            Return _CodigoOficinaVenta
        End Get
        Set(ByVal Value As String)
            _CodigoOficinaVenta = Value
        End Set
    End Property

    Public Property NumeroLinea() As String
        Get
            Return _NumeroLinea
        End Get
        Set(ByVal Value As String)
            _NumeroLinea = Value
        End Set
    End Property

    Public Property Iccid() As String
        Get
            Return _Iccid
        End Get
        Set(ByVal Value As String)
            _Iccid = Value
        End Set
    End Property

    Public Property CasoEspecial() As String
        Get
            Return _CasoEspecial
        End Get
        Set(ByVal Value As String)
            _CasoEspecial = Value
        End Set
    End Property

    Public Property Campana() As String
        Get
            Return _Campana
        End Get
        Set(ByVal Value As String)
            _Campana = Value
        End Set
    End Property

    Public Property TopeConsumo() As String
        Get
            Return _TopeConsumo
        End Get
        Set(ByVal Value As String)
            _TopeConsumo = Value
        End Set
    End Property

    Public Property MontoTopeConsumo() As String
        Get
            Return _MontoTopeConsumo
        End Get
        Set(ByVal Value As String)
            _MontoTopeConsumo = Value
        End Set
    End Property

    Public Property Correo() As String
        Get
            Return _Correo
        End Get
        Set(ByVal Value As String)
            _Correo = Value
        End Set
    End Property

    Public Property FlagCorreo() As String
        Get
            Return _FlagCorreo
        End Get
        Set(ByVal Value As String)
            _FlagCorreo = Value
        End Set
    End Property

    Public Property MarcaEquipo() As String
        Get
            Return _MarcaEquipo
        End Get
        Set(ByVal Value As String)
            _MarcaEquipo = Value
        End Set
    End Property

    Public Property ModeloEquipo() As String
        Get
            Return _ModeloEquipo
        End Get
        Set(ByVal Value As String)
            _ModeloEquipo = Value
        End Set
    End Property

    Public Property PrecioVenta() As String
        Get
            Return _PrecioVenta
        End Get
        Set(ByVal Value As String)
            _PrecioVenta = Value
        End Set
    End Property

    Public Property FechaInicioContratoActual() As String
        Get
            Return _FechaInicioContratoActual
        End Get
        Set(ByVal Value As String)
            _FechaInicioContratoActual = Value
        End Set
    End Property

    Public Property FechaActivacionAlta() As String
        Get
            Return _FechaActivacionAlta
        End Get
        Set(ByVal Value As String)
            _FechaActivacionAlta = Value
        End Set
    End Property

    Public Property TipoProducto() As String
        Get
            Return _TipoProducto
        End Get
        Set(ByVal Value As String)
            _TipoProducto = Value
        End Set
    End Property

    Public Property TipoTecnologia() As String
        Get
            Return _TipoTecnologia
        End Get
        Set(ByVal Value As String)
            _TipoTecnologia = Value
        End Set
    End Property

    Public Property PoIdNuevo() As String
        Get
            Return _PoIdNuevo
        End Get
        Set(ByVal Value As String)
            _PoIdNuevo = Value
        End Set
    End Property

    Public Property PoIdNuevoDescripcion() As String
        Get
            Return _PoIdNuevoDescripcion
        End Get
        Set(ByVal Value As String)
            _PoIdNuevoDescripcion = Value
        End Set
    End Property

    Public Property ModalidadPago() As String
        Get
            Return _ModalidadPago
        End Get
        Set(ByVal Value As String)
            _ModalidadPago = Value
        End Set
    End Property

    Public Property CargoFijoNuevoPlan() As String
        Get
            Return _CargoFijoNuevoPlan
        End Get
        Set(ByVal Value As String)
            _CargoFijoNuevoPlan = Value
        End Set
    End Property

    Public Property PlazoContratoActual() As String
        Get
            Return _PlazoContratoActual
        End Get
        Set(ByVal Value As String)
            _PlazoContratoActual = Value
        End Set
    End Property

    Public Property PoIdActual() As String
        Get
            Return _PoIdActual
        End Get
        Set(ByVal Value As String)
            _PoIdActual = Value
        End Set
    End Property

    Public Property PoIdActualDescripcion() As String
        Get
            Return _PoIdActualDescripcion
        End Get
        Set(ByVal Value As String)
            _PoIdActualDescripcion = Value
        End Set
    End Property

    Public Property CargoFijoActualPlan() As String
        Get
            Return _CargoFijoActualPlan
        End Get
        Set(ByVal Value As String)
            _CargoFijoActualPlan = Value
        End Set
    End Property

    Public Property FechaTransaccion() As String
        Get
            Return _FechaTransaccion
        End Get
        Set(ByVal Value As String)
            _FechaTransaccion = Value
        End Set
    End Property

    Public Property TipoSuscripcion() As String
        Get
            Return _TipoSuscripcion
        End Get
        Set(ByVal Value As String)
            _TipoSuscripcion = Value
        End Set
    End Property

    Public Property FlagMontoOCC() As String
        Get
            Return _FlagMontoOCC
        End Get
        Set(ByVal Value As String)
            _FlagMontoOCC = Value
        End Set
    End Property

    Public Property MontoOCC() As String
        Get
            Return _MontoOCC
        End Get
        Set(ByVal Value As String)
            _MontoOCC = Value
        End Set
    End Property

End Class
