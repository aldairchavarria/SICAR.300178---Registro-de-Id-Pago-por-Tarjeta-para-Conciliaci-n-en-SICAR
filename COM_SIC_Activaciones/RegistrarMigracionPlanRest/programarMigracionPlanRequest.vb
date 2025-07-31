Public Class programarMigracionPlanRequest

    Private _idNegocio As String
    Private _aplicacion As String
    Private _usuarioAplicacion As String
    Private _usuarioSistema As String
    Private _descCoSer As String
    Private _codigoInteraccion As String
    Private _nroCuenta As String
    Private _fechaProgramacion As String
    Private _datosMigracionPlan As datosMigracionPlan
    Private _listaOpcional As ArrayList

    Public Property idNegocio() As String
        Get
            Return _idNegocio
        End Get
        Set(ByVal Value As String)
            _idNegocio = Value
        End Set
    End Property

    Public Property aplicacion() As String
        Get
            Return _aplicacion
        End Get
        Set(ByVal Value As String)
            _aplicacion = Value
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

    Public Property usuarioSistema() As String
        Get
            Return _usuarioSistema
        End Get
        Set(ByVal Value As String)
            _usuarioSistema = Value
        End Set
    End Property

    Public Property descCoSer() As String
        Get
            Return _descCoSer
        End Get
        Set(ByVal Value As String)
            _descCoSer = Value
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

    Public Property nroCuenta() As String
        Get
            Return _nroCuenta
        End Get
        Set(ByVal Value As String)
            _nroCuenta = Value
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

    Public Property datosMigracionPlan() As datosMigracionPlan
        Get
            Return _datosMigracionPlan
        End Get
        Set(ByVal Value As datosMigracionPlan)
            _datosMigracionPlan = Value
        End Set
    End Property

    Public Property listaOpcional() As ArrayList
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As ArrayList)
            _listaOpcional = Value
        End Set
    End Property

    Public Sub New()
        _datosMigracionPlan = New datosMigracionPlan
    End Sub

End Class
