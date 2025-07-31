
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BElistaDetalleActividadType

    Private _idActividad As String
    Private _tipoActividad As String
    Private _timeSlot As String
    Private _cierreServicio As String
    Private _duracion As String
    Private _numerodePedido As String
    Private _startTime As String
    Private _estado As String
    Private _zonaTrabajo As String
    Private _fechaCierre As String
    Private _fechaCreacionTOA As String
    Private _fechaAsignacion As String
    Private _recursoAsignacionAutomat As String
    Private _fechaAsignacionAutomat As String
    Private _idRecurso As String
    Private _fecha As String
    Private _longitud As String
    Private _latitud As String
    Private _subtipoOrden As String
    Private _zona As String
    Private _mapa As String
    Private _numeroPedido As String
    Private _cdeli_sec As String
    Private _tipoVenta As String
    Private _tipoProducto As String
    Private _tipoOperacion As String
    Private _modalidadVenta As String
    Private _flagPortabilidad As String
    Private _requiereBiometria As String
    Private _tipoDocumento As String
    Private _numeroDocumento As String
    Private _nombre As String
    Private _correo As String
    Private _telefonoContacto As String
    Private _telefonoContactoSecundario As String
    Private _departamento As String
    Private _provincia As String
    Private _distrito As String
    Private _direccion As String
    Private _referencia As String
    Private _fechaEntrega As String
    Private _comentarios As String
    Private _totalCobrar As String
    Private _modalidadPago As String
    Private _razonNoCompletado As String
    Private _inicioServicio As String


    Public Property idActividad() As String
        Get
            Return _idActividad
        End Get
        Set(ByVal Value As String)
            _idActividad = Value
        End Set
    End Property

    Public Property tipoActividad() As String
        Get
            Return _tipoActividad
        End Get
        Set(ByVal Value As String)
            _tipoActividad = Value
        End Set
    End Property

    Public Property timeSlot() As String
        Get
            Return _timeSlot
        End Get
        Set(ByVal Value As String)
            _timeSlot = Value
        End Set
    End Property

    Public Property cierreServicio() As String
        Get
            Return _cierreServicio
        End Get
        Set(ByVal Value As String)
            _cierreServicio = Value
        End Set
    End Property

    Public Property duracion() As String
        Get
            Return _duracion
        End Get
        Set(ByVal Value As String)
            _duracion = Value
        End Set
    End Property

    Public Property numerodePedido() As String
        Get
            Return _numerodePedido
        End Get
        Set(ByVal Value As String)
            _numerodePedido = Value
        End Set
    End Property

    Public Property startTime() As String
        Get
            Return _startTime
        End Get
        Set(ByVal Value As String)
            _startTime = Value
        End Set
    End Property

    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal Value As String)
            _estado = Value
        End Set
    End Property

    Public Property zonaTrabajo() As String
        Get
            Return _zonaTrabajo
        End Get
        Set(ByVal Value As String)
            _zonaTrabajo = Value
        End Set
    End Property

    Public Property fechaCierre() As String
        Get
            Return _fechaCierre
        End Get
        Set(ByVal Value As String)
            _fechaCierre = Value
        End Set
    End Property

    Public Property fechaCreacionTOA() As String
        Get
            Return _fechaCreacionTOA
        End Get
        Set(ByVal Value As String)
            _fechaCreacionTOA = Value
        End Set
    End Property

    Public Property fechaAsignacion() As String
        Get
            Return _fechaAsignacion
        End Get
        Set(ByVal Value As String)
            _fechaAsignacion = Value
        End Set
    End Property

    Public Property recursoAsignacionAutomat() As String
        Get
            Return _recursoAsignacionAutomat
        End Get
        Set(ByVal Value As String)
            _recursoAsignacionAutomat = Value
        End Set
    End Property

    Public Property fechaAsignacionAutomat() As String
        Get
            Return _fechaAsignacionAutomat
        End Get
        Set(ByVal Value As String)
            _fechaAsignacionAutomat = Value
        End Set
    End Property

    Public Property idRecurso() As String
        Get
            Return _idRecurso
        End Get
        Set(ByVal Value As String)
            _idRecurso = Value
        End Set
    End Property

    Public Property fecha() As String
        Get
            Return _fecha
        End Get
        Set(ByVal Value As String)
            _fecha = Value
        End Set
    End Property

    Public Property longitud() As String
        Get
            Return _longitud
        End Get
        Set(ByVal Value As String)
            _longitud = Value
        End Set
    End Property

    Public Property latitud() As String
        Get
            Return _latitud
        End Get
        Set(ByVal Value As String)
            _latitud = Value
        End Set
    End Property

    Public Property subtipoOrden() As String
        Get
            Return _subtipoOrden
        End Get
        Set(ByVal Value As String)
            _subtipoOrden = Value
        End Set
    End Property

    Public Property zona() As String
        Get
            Return _zona
        End Get
        Set(ByVal Value As String)
            _zona = Value
        End Set
    End Property

    Public Property mapa() As String
        Get
            Return _mapa
        End Get
        Set(ByVal Value As String)
            _mapa = Value
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

    Public Property cdeli_sec() As String
        Get
            Return _cdeli_sec
        End Get
        Set(ByVal Value As String)
            _cdeli_sec = Value
        End Set
    End Property

    Public Property tipoVenta() As String
        Get
            Return _tipoVenta
        End Get
        Set(ByVal Value As String)
            _tipoVenta = Value
        End Set
    End Property

    Public Property tipoProducto() As String
        Get
            Return _tipoProducto
        End Get
        Set(ByVal Value As String)
            _tipoProducto = Value
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

    Public Property modalidadVenta() As String
        Get
            Return _modalidadVenta
        End Get
        Set(ByVal Value As String)
            _modalidadVenta = Value
        End Set
    End Property

    Public Property flagPortabilidad() As String
        Get
            Return _flagPortabilidad
        End Get
        Set(ByVal Value As String)
            _flagPortabilidad = Value
        End Set
    End Property

    Public Property requiereBiometria() As String
        Get
            Return _requiereBiometria
        End Get
        Set(ByVal Value As String)
            _requiereBiometria = Value
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

    Public Property nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal Value As String)
            _nombre = Value
        End Set
    End Property

    Public Property correo() As String
        Get
            Return _correo
        End Get
        Set(ByVal Value As String)
            _correo = Value
        End Set
    End Property

    Public Property telefonoContacto() As String
        Get
            Return _telefonoContacto
        End Get
        Set(ByVal Value As String)
            _telefonoContacto = Value
        End Set
    End Property

    Public Property telefonoContactoSecundario() As String
        Get
            Return _telefonoContactoSecundario
        End Get
        Set(ByVal Value As String)
            _telefonoContactoSecundario = Value
        End Set
    End Property

    Public Property departamento() As String
        Get
            Return _departamento
        End Get
        Set(ByVal Value As String)
            _departamento = Value
        End Set
    End Property

    Public Property provincia() As String
        Get
            Return _provincia
        End Get
        Set(ByVal Value As String)
            _provincia = Value
        End Set
    End Property

    Public Property distrito() As String
        Get
            Return _distrito
        End Get
        Set(ByVal Value As String)
            _distrito = Value
        End Set
    End Property

    Public Property direccion() As String
        Get
            Return _direccion
        End Get
        Set(ByVal Value As String)
            _direccion = Value
        End Set
    End Property

    Public Property referencia() As String
        Get
            Return _referencia
        End Get
        Set(ByVal Value As String)
            _referencia = Value
        End Set
    End Property

    Public Property fechaEntrega() As String
        Get
            Return _fechaEntrega
        End Get
        Set(ByVal Value As String)
            _fechaEntrega = Value
        End Set
    End Property

    Public Property comentarios() As String
        Get
            Return _comentarios
        End Get
        Set(ByVal Value As String)
            _comentarios = Value
        End Set
    End Property

    Public Property totalCobrar() As String
        Get
            Return _totalCobrar
        End Get
        Set(ByVal Value As String)
            _totalCobrar = Value
        End Set
    End Property

    Public Property modalidadPago() As String
        Get
            Return _modalidadPago
        End Get
        Set(ByVal Value As String)
            _modalidadPago = Value
        End Set
    End Property

    Public Property razonNoCompletado() As String
        Get
            Return _razonNoCompletado
        End Get
        Set(ByVal Value As String)
            _razonNoCompletado = Value
        End Set
    End Property

    Public Property inicioServicio() As String
        Get
            Return _inicioServicio
        End Get
        Set(ByVal Value As String)
            _inicioServicio = Value
        End Set
    End Property
End Class
