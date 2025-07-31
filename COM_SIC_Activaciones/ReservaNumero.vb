Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class ReservaNumero


    Private _CodigoRespuestaOrig As String
    Private _Msisdn As String
    Private _Comercio As String
    Private _Iccid As String
    Private _PuntoVenta As String
    Private _Departamento As String
    Private _TipoActivacion As String
    Private _Opcion As String
    Private _Vendedor As String
    Private _Ciudad As String
    Private _ChipPack As String
    Private _Imei As String
    Private _CodProducto As String
    Private _CodPlan As String
    Private _CodDocumento As String
    Private _CodSerie As String
    Private _CodCorrelativo As String
    Private _Promocion As String
    Private _CodTipoProducto As String
    Private _CodigoAdicional1 As String
    Private _CodigoAdicional2 As String
    Private _Car7 As String
    Private _Car8 As String
    Private _Trace As String
    Private _OperacionClaro As String
    Private _CodigoRespuesta As String
    Private _DescripcionRespuesta As String
    Private _CodigoTxn As String
    Public ReservaNumero() 'PROY-140126


    Public Property CodigoRespuestaOrig() As String
        Set(ByVal value As String)
            _CodigoRespuestaOrig = value
        End Set
        Get
            Return _CodigoRespuestaOrig
        End Get
    End Property

    Public Property Msisdn() As String
        Set(ByVal value As String)
            _Msisdn = value
        End Set
        Get
            Return _Msisdn
        End Get
    End Property

    Public Property Comercio() As String
        Set(ByVal value As String)
            _Comercio = value
        End Set
        Get
            Return _Comercio
        End Get
    End Property

    Public Property Iccid() As String
        Set(ByVal value As String)
            _Iccid = value
        End Set
        Get
            Return _Iccid
        End Get
    End Property

    Public Property PuntoVenta() As String
        Set(ByVal value As String)
            _PuntoVenta = value
        End Set
        Get
            Return _PuntoVenta
        End Get
    End Property

    Public Property Departamento() As String
        Set(ByVal value As String)
            _Departamento = value
        End Set
        Get
            Return _Departamento
        End Get
    End Property

    Public Property TipoActivacion() As String
        Set(ByVal value As String)
            _TipoActivacion = value
        End Set
        Get
            Return _TipoActivacion
        End Get
    End Property

    Public Property Opcion() As String
        Set(ByVal value As String)
            _Opcion = value
        End Set
        Get
            Return _Opcion
        End Get
    End Property

    Public Property Vendedor() As String
        Set(ByVal value As String)
            CodigoRespuestaOrig = value
        End Set
        Get
            Return _Vendedor
        End Get
    End Property

    Public Property Ciudad() As String
        Set(ByVal value As String)
            _Ciudad = value
        End Set
        Get
            Return _Ciudad
        End Get
    End Property

    Public Property ChipPack() As String
        Set(ByVal value As String)
            _ChipPack = value
        End Set
        Get
            Return _ChipPack
        End Get
    End Property

    Public Property Imei() As String
        Set(ByVal value As String)
            _Imei = value
        End Set
        Get
            Return _Imei
        End Get
    End Property

    Public Property CodProducto() As String
        Set(ByVal value As String)
            _CodProducto = value
        End Set
        Get
            Return _CodProducto
        End Get
    End Property

    Public Property CodPlan() As String
        Set(ByVal value As String)
            _CodPlan = value
        End Set
        Get
            Return _CodPlan
        End Get
    End Property

    Public Property CodDocumento() As String
        Set(ByVal value As String)
            _CodDocumento = value
        End Set
        Get
            Return _CodDocumento
        End Get
    End Property

    Public Property CodSerie() As String
        Set(ByVal value As String)
            _CodSerie = value
        End Set
        Get
            Return _CodSerie
        End Get
    End Property

    Public Property CodCorrelativo() As String
        Set(ByVal value As String)
            _CodCorrelativo = value
        End Set
        Get
            Return _CodCorrelativo
        End Get
    End Property

    Public Property Promocion() As String
        Set(ByVal value As String)
            _Promocion = value
        End Set
        Get
            Return _Promocion
        End Get
    End Property

    Public Property CodTipoProducto() As String
        Set(ByVal value As String)
            _CodTipoProducto = value
        End Set
        Get
            Return _CodTipoProducto
        End Get
    End Property

    Public Property CodigoAdicional1() As String
        Set(ByVal value As String)
            _CodigoAdicional1 = value
        End Set
        Get
            Return _CodigoAdicional1
        End Get
    End Property

    Public Property CodigoAdicional2() As String
        Set(ByVal value As String)
            _CodigoAdicional2 = value
        End Set
        Get
            Return _CodigoAdicional2
        End Get
    End Property

    Public Property Car7() As String
        Set(ByVal value As String)
            _Car7 = value
        End Set
        Get
            Return _Car7
        End Get
    End Property

    Public Property Car8() As String
        Set(ByVal value As String)
            _Car8 = value
        End Set
        Get
            Return _Car8
        End Get
    End Property

    Public Property Trace() As String
        Set(ByVal value As String)
            _Trace = value
        End Set
        Get
            Return _Trace
        End Get
    End Property

    Public Property OperacionClaro() As String
        Set(ByVal value As String)
            _OperacionClaro = value
        End Set
        Get
            Return _OperacionClaro
        End Get
    End Property

    Public Property CodigoRespuesta() As String
        Set(ByVal value As String)
            _CodigoRespuesta = value
        End Set
        Get
            Return _CodigoRespuesta
        End Get
    End Property

    Public Property DescripcionRespuesta() As String
        Set(ByVal value As String)
            _DescripcionRespuesta = value
        End Set
        Get
            Return _DescripcionRespuesta
        End Get
    End Property

    Public Property CodigoTxn() As String
        Set(ByVal value As String)
            _CodigoTxn = value
        End Set
        Get
            Return _CodigoTxn
        End Get
    End Property



End Class
