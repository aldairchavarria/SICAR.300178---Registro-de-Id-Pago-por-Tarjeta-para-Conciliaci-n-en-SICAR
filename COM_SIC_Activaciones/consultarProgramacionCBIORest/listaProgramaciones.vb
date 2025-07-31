Imports System.Runtime.Serialization

Public Class listaProgramaciones

    'INICIATIVA-219
    Dim _coIdPub As String
    Dim _fechaProgramacion As String
    Dim _fechaRegistro As String
    Dim _fechaEjecucion As String
    Dim _estado As String
    Dim _desEstado As String
    Dim _esBatch As String
    Dim _codigoError As String
    Dim _mensajeError As String
    Dim _usuarioSistema As String
    Dim _idTransaccion As String
    Dim _serviCod As String
    Dim _descripcionServicio As String
    Dim _msisdn As String
    Dim _idBatch As String
    Dim _usuarioAplicacion As String
    Dim _emailUsuarioAplicacion As String
    Dim _xmlEntrada As String
    Dim _numeroCuenta As String
    Dim _codigoInteraccion As String
    Dim _puntoVenta As String
    Dim _tipoServ As String
    Dim _coSer As String
    Dim _desCoSer As String
    Dim _tipoRegistro As String

    Public Property coIdPub() As String
        Get
            Return _coIdPub
        End Get
        Set(ByVal Value As String)
            _coIdPub = Value
        End Set
    End Property

    Public Property fechaProgramacion() As String
        Get
            Return _fechaProgramacion
        End Get
        Set(ByVal Value As String)
            _fechaProgramacion = Value
        End Set
    End Property

    Public Property fechaRegistro() As String
        Get
            Return _fechaRegistro
        End Get
        Set(ByVal Value As String)
            _fechaRegistro = Value
        End Set
    End Property

    Public Property fechaEjecucion() As String
        Get
            Return _fechaEjecucion
        End Get
        Set(ByVal Value As String)
            _fechaEjecucion = Value
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

    Public Property desEstado() As String
        Get
            Return _desEstado
        End Get
        Set(ByVal Value As String)
            _desEstado = Value
        End Set
    End Property

    Public Property esBatch() As String
        Get
            Return _esBatch
        End Get
        Set(ByVal Value As String)
            _esBatch = Value
        End Set
    End Property

    Public Property codigoError() As String
        Get
            Return _codigoError
        End Get
        Set(ByVal Value As String)
            _codigoError = Value
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

    Public Property usuarioSistema() As String
        Get
            Return _usuarioSistema
        End Get
        Set(ByVal Value As String)
            _usuarioSistema = Value
        End Set
    End Property

    Public Property idTransaccion() As String
        Get
            Return _idTransaccion
        End Get
        Set(ByVal Value As String)
            _idTransaccion = Value
        End Set
    End Property

    Public Property serviCod() As String
        Get
            Return _serviCod
        End Get
        Set(ByVal Value As String)
            _serviCod = Value
        End Set
    End Property

    Public Property descripcionServicio() As String
        Get
            Return _descripcionServicio
        End Get
        Set(ByVal Value As String)
            _descripcionServicio = Value
        End Set
    End Property

    Public Property msisdn() As String
        Get
            Return _msisdn
        End Get
        Set(ByVal Value As String)
            _msisdn = Value
        End Set
    End Property

    Public Property idBatch() As String
        Get
            Return _idBatch
        End Get
        Set(ByVal Value As String)
            _idBatch = Value
        End Set
    End Property

    Public Property usuarioAplicacion() As String
        Get
            Return _usuarioAplicacion
        End Get
        Set(ByVal Value As String)
            _usuarioAplicacion = Value
        End Set
    End Property

    Public Property emailUsuarioAplicacion() As String
        Get
            Return _emailUsuarioAplicacion
        End Get
        Set(ByVal Value As String)
            _emailUsuarioAplicacion = Value
        End Set
    End Property

    Public Property xmlEntrada() As String
        Get
            Return _xmlEntrada
        End Get
        Set(ByVal Value As String)
            _xmlEntrada = Value
        End Set
    End Property

    Public Property numeroCuenta() As String
        Get
            Return _numeroCuenta
        End Get
        Set(ByVal Value As String)
            _numeroCuenta = Value
        End Set
    End Property

    Public Property codigoInteraccion() As String
        Get
            Return _codigoInteraccion
        End Get
        Set(ByVal Value As String)
            _codigoInteraccion = Value
        End Set
    End Property

    Public Property puntoVenta() As String
        Get
            Return _puntoVenta
        End Get
        Set(ByVal Value As String)
            _puntoVenta = Value
        End Set
    End Property

    Public Property tipoServ() As String
        Get
            Return _tipoServ
        End Get
        Set(ByVal Value As String)
            _tipoServ = Value
        End Set
    End Property

    Public Property coSer() As String
        Get
            Return _coSer
        End Get
        Set(ByVal Value As String)
            _coSer = Value
        End Set
    End Property

    Public Property desCoSer() As String
        Get
            Return _desCoSer
        End Get
        Set(ByVal Value As String)
            _desCoSer = Value
        End Set
    End Property

    Public Property tipoRegistro() As String
        Get
            Return _tipoRegistro
        End Get
        Set(ByVal Value As String)
            _tipoRegistro = Value
        End Set
    End Property

End Class
