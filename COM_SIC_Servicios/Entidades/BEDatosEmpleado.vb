Public Class BEDatosEmpleado

    Private _NOMBRE_COMPLETO As String

    Public Property NOMBRE_COMPLETO() As String
        Get
            Return _NOMBRE_COMPLETO
        End Get
        Set(ByVal value As String)
            _NOMBRE_COMPLETO = value
        End Set
    End Property

    Private _OFICINA As String

    Public Property OFICINA() As String
        Get
            Return _OFICINA
        End Get
        Set(ByVal value As String)
            _OFICINA = value
        End Set
    End Property

    Private _ALMACEN As String

    Public Property ALMACEN() As String
        Get
            Return _ALMACEN
        End Get
        Set(ByVal value As String)
            _ALMACEN = value
        End Set
    End Property

    Private _COD_VENDEDOR As String

    Public Property COD_VENDEDOR() As String
        Get
            Return _COD_VENDEDOR
        End Get
        Set(ByVal value As String)
            _COD_VENDEDOR = value
        End Set
    End Property

    Private _CANAL As String

    Public Property CANAL() As String
        Get
            Return _CANAL
        End Get
        Set(ByVal value As String)
            _CANAL = value
        End Set
    End Property

    Private _PERFIL As String

    Public Property PERFIL() As String
        Get
            Return _PERFIL
        End Get
        Set(ByVal value As String)
            _PERFIL = value
        End Set
    End Property

    Private _CODUSUARIO As String

    Public Property CODUSUARIO() As String
        Get
            Return _CODUSUARIO
        End Get
        Set(ByVal value As String)
            _CODUSUARIO = value
        End Set
    End Property

    Private _CURRENT_USER As String

    Public Property CURRENT_USER() As String
        Get
            Return _CURRENT_USER
        End Get
        Set(ByVal value As String)
            _CURRENT_USER = value
        End Set
    End Property

    Private _NRO_CAJA As String

    Public Property NRO_CAJA() As String
        Get
            Return _NRO_CAJA
        End Get
        Set(ByVal value As String)
            _NRO_CAJA = value
        End Set
    End Property

    Private _NOMBRE_CAJA As String

    Public Property NOMBRE_CAJA() As String
        Get
            Return _NOMBRE_CAJA
        End Get
        Set(ByVal value As String)
            _NOMBRE_CAJA = value
        End Set
    End Property
End Class
