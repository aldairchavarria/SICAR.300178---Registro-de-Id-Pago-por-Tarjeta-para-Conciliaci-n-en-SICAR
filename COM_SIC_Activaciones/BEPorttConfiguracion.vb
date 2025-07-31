Imports System.Runtime.Serialization
'PROY 31393 (Omision) - Inicio

'PROY-140126
<Serializable()> _
Public Class BEPorttConfiguracion
    Private _PORTN_CODIGO As Double
    Private _PORTV_EST_PROCESO As String
    Private _PORTV_MOTIVO As String
    Private _PORTV_FLAG_ACREDITA As Integer
    Private _PORTV_OPERADOR As String
    Private _PORTC_TIPO_PRODUCTO As String
    Private _PORTV_TIPO_VENTA As String
    Private _PORTV_APLICACION As String
    Private _PORTC_ESTADO As String
    Private _PORTV_MOD_VENTA As String
    Public BEPorttConfiguracion() 'PROY-140126

    Public Property PORTN_CODIGO() As String
        Get
            Return _PORTN_CODIGO
        End Get
        Set(ByVal Value As String)
            _PORTN_CODIGO = Value
        End Set
    End Property

    Public Property PORTV_EST_PROCESO() As String
        Get
            Return _PORTV_EST_PROCESO
        End Get
        Set(ByVal Value As String)
            _PORTV_EST_PROCESO = Value
        End Set
    End Property

    Public Property PORTV_MOTIVO() As String
        Get
            Return _PORTV_MOTIVO
        End Get
        Set(ByVal Value As String)
            _PORTV_MOTIVO = Value
        End Set
    End Property

    Public Property PORTV_FLAG_ACREDITA() As String
        Get
            Return _PORTV_FLAG_ACREDITA
        End Get
        Set(ByVal Value As String)
            _PORTV_FLAG_ACREDITA = Value
        End Set
    End Property

    Public Property PORTV_OPERADOR() As String
        Get
            Return _PORTV_OPERADOR
        End Get
        Set(ByVal Value As String)
            _PORTV_OPERADOR = Value
        End Set
    End Property

    Public Property PORTC_TIPO_PRODUCTO() As String
        Get
            Return _PORTC_TIPO_PRODUCTO
        End Get
        Set(ByVal Value As String)
            _PORTC_TIPO_PRODUCTO = Value
        End Set
    End Property

    Public Property PORTV_TIPO_VENTA() As String
        Get
            Return _PORTV_TIPO_VENTA
        End Get
        Set(ByVal Value As String)
            _PORTV_TIPO_VENTA = Value
        End Set
    End Property

    Public Property PORTV_APLICACION() As String
        Get
            Return _PORTV_APLICACION
        End Get
        Set(ByVal Value As String)
            _PORTV_APLICACION = Value
        End Set
    End Property

    Public Property PORTC_ESTADO() As String
        Get
            Return _PORTC_ESTADO
        End Get
        Set(ByVal Value As String)
            _PORTC_ESTADO = Value
        End Set
    End Property

    Public Property PORTV_MOD_VENTA() As String
        Get
            Return _PORTV_MOD_VENTA
        End Get
        Set(ByVal Value As String)
            _PORTV_MOD_VENTA = Value
        End Set
    End Property


End Class
'PROY 31393 (Omision) - Fin