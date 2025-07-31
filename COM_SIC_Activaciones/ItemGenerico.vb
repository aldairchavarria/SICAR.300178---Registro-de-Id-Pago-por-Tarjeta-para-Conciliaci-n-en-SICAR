Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class ItemGenerico
    Private _CODIGO As String
    Private _CODIGO2 As String
    'PROY-32089 INI
    Private _CODIGO3 As String
    'PROY-32089 FIN
    Private _DESCRIPCION As String
    Private _DESCRIPCION2 As String
    Public ItemGenerico() 'PROY-140126

    Public Property CODIGO() As String
        Set(ByVal value As String)
            _CODIGO = value
        End Set
        Get
            Return _CODIGO
        End Get
    End Property
    Public Property CODIGO2() As String
        Set(ByVal value As String)
            _CODIGO2 = value
        End Set
        Get
            Return _CODIGO2
        End Get
    End Property
    Public Property DESCRIPCION() As String
        Set(ByVal value As String)
            _DESCRIPCION = value
        End Set
        Get
            Return _DESCRIPCION
        End Get
    End Property
    Public Property DESCRIPCION2() As String
        Set(ByVal value As String)
            _DESCRIPCION2 = value
        End Set
        Get
            Return _DESCRIPCION2
        End Get
    End Property
    'PROY-32089 INI
    Public Property CODIGO3() As String
        Set(ByVal value As String)
            _CODIGO3 = value
        End Set
        Get
            Return _CODIGO3
        End Get
    End Property
    'PROY-32089 FIN

    'INI: PROY-140262 BLACKOUT
    Sub New()
        _CODIGO4 = String.Empty
        _DESCRIPCION4 = String.Empty
    End Sub

    Private _CODIGO4 As String
    Public Property CODIGO4() As String
        Set(ByVal value As String)
            _CODIGO4 = value
        End Set
        Get
            Return _CODIGO4
        End Get
    End Property

    Private _DESCRIPCION4 As String
    Public Property DESCRIPCION4() As String
        Set(ByVal value As String)
            _DESCRIPCION4 = value
        End Set
        Get
            Return _DESCRIPCION4
        End Get
    End Property
    'FIN: PROY-140262 BLACKOUT

    'JLOPETAS - PROY 140589 - INI
    Private _FLAG_PICKING As String
    Public Property FLAG_PICKING() As String
        Set(ByVal value As String)
            _FLAG_PICKING = value
        End Set
        Get
            Return _FLAG_PICKING
        End Get
    End Property

    Private _FLAG_DLV As String
    Public Property FLAG_DLV() As String
        Set(ByVal value As String)
            _FLAG_DLV = value
        End Set
        Get
            Return _FLAG_DLV
        End Get
    End Property

    Private _FLAG_PAGOENLINEA As String
    Public Property FLAG_PAGOENLINEA() As String
        Set(ByVal value As String)
            _FLAG_PAGOENLINEA = value
        End Set
        Get
            Return _FLAG_PAGOENLINEA
        End Get
    End Property
    'JLOPETAS - PROY 140589 - FIN

    'INICIATIVA-1006 | Tienda Virtual  - ACC con Costo | INI
    Private _MONTO_PAGAR As String
    Public Property MONTO_PAGAR() As String
        Set(ByVal value As String)
            _MONTO_PAGAR = value
        End Set
        Get
            Return _MONTO_PAGAR
        End Get
    End Property

    Private _MONTO_PAGAR_RA As String
    Public Property MONTO_PAGAR_RA() As String
        Set(ByVal value As String)
            _MONTO_PAGAR_RA = value
        End Set
        Get
            Return _MONTO_PAGAR_RA
        End Get
    End Property

    Private _ESTADO_PEDIDO As String
    Public Property ESTADO_PEDIDO() As String
        Set(ByVal value As String)
            _ESTADO_PEDIDO = value
        End Set
        Get
            Return _ESTADO_PEDIDO
        End Get
    End Property

    Private _MONTO_PM As String
    Public Property MONTO_PM() As String
        Set(ByVal value As String)
            _MONTO_PM = value
        End Set
        Get
            Return _MONTO_PM
        End Get
    End Property

    Private _FLAG_BIOMETRIA As String
    Public Property FLAG_BIOMETRIA() As String
        Set(ByVal value As String)
            _FLAG_BIOMETRIA = value
        End Set
        Get
            Return _FLAG_BIOMETRIA
        End Get
    End Property

    Private _MONTO_ACC_COSTO As String
    Public Property MONTO_ACC_COSTO() As String
        Set(ByVal value As String)
            _MONTO_ACC_COSTO = value
        End Set
        Get
            Return _MONTO_ACC_COSTO
        End Get
    End Property

    'INICIATIVA-1006 | Tienda Virtual  - ACC con Costo | FIN

End Class
