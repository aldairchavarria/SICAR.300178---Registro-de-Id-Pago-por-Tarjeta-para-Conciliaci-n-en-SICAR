Imports System.Runtime.Serialization
'PROY-33313 RP INICIO

'PROY-140126
<Serializable()> _
Public Class BETipiChipSuelto

    Private _VENTV_NROPEDIDO As String
    Private _VENTV_NROSEC As String
    Private _VENTC_TIPODOCUMENTO As String
    Private _VENTV_NRODOCUMENTO As String
    Private _VENTC_LINEA As String
    Private _VENTV_PRODUCTO As String
    Private _VENTD_FEC_OPERACION As Date
    Private _VENTV_APLICACION As String
    Private _VENTV_TIP_OPERACION As String
    Private _VENTV_PDV As String
    Private _VENTV_ESTADO As String
    Private _VENTV_MODALIDAD As String
    Private _VENTC_TIPDOCVEND As String
    Private _VENTV_NRODOCVEND As String
    Private _VENTV_CODVENDEDOR As String
    Private _VENTV_ICCID As String
    Private _VENTV_PLAN As String
    Private _VENTV_CANAL As String
    Private _VENTV_USUARIO_CREA As String
    Private _VENTV_USUARIO_MODIF As String
    Private _CODIGO_RESPUESTA As String
    Private _MENSAJE_RESPUESTA As String
    Private _VENTN_MONTO As Decimal
    Public BETipiChipSuelto() 'PROY-140126

    'PROY-140360-IDEA-46301
    Private _VENTV_IMEI As String
    Private _VENTV_TIPO As String
    Private _VENTV_ID_ONBASE As String ' Proy-140360 Onbase
    'Proy-140360 Fin
    Public Property VENTV_NROPEDIDO() As String
        Set(ByVal value As String)
            _VENTV_NROPEDIDO = value
        End Set
        Get
            Return _VENTV_NROPEDIDO
        End Get
    End Property

    Public Property VENTV_NROSEC() As String
        Set(ByVal value As String)
            _VENTV_NROSEC = value
        End Set
        Get
            Return _VENTV_NROSEC
        End Get
    End Property

    Public Property VENTC_TIPODOCUMENTO() As String
        Set(ByVal value As String)
            _VENTC_TIPODOCUMENTO = value
        End Set
        Get
            Return _VENTC_TIPODOCUMENTO
        End Get
    End Property

    Public Property VENTV_NRODOCUMENTO() As String
        Set(ByVal value As String)
            _VENTV_NRODOCUMENTO = value
        End Set
        Get
            Return _VENTV_NRODOCUMENTO
        End Get
    End Property

    Public Property VENTC_LINEA() As String
        Set(ByVal value As String)
            _VENTC_LINEA = value
        End Set
        Get
            Return _VENTC_LINEA
        End Get
    End Property

    Public Property VENTV_PRODUCTO() As String
        Set(ByVal value As String)
            _VENTV_PRODUCTO = value
        End Set
        Get
            Return _VENTV_PRODUCTO
        End Get
    End Property

    Public Property VENTD_FEC_OPERACION() As Date
        Set(ByVal value As Date)
            _VENTD_FEC_OPERACION = value
        End Set
        Get
            Return _VENTD_FEC_OPERACION
        End Get
    End Property

    Public Property VENTV_APLICACION() As String
        Set(ByVal value As String)
            _VENTV_APLICACION = value
        End Set
        Get
            Return _VENTV_APLICACION
        End Get
    End Property

    Public Property VENTV_TIP_OPERACION() As String
        Set(ByVal value As String)
            _VENTV_TIP_OPERACION = value
        End Set
        Get
            Return _VENTV_TIP_OPERACION
        End Get
    End Property

    Public Property VENTV_PDV() As String
        Set(ByVal value As String)
            _VENTV_PDV = value
        End Set
        Get
            Return _VENTV_PDV
        End Get
    End Property

    Public Property VENTV_ESTADO() As String
        Set(ByVal value As String)
            _VENTV_ESTADO = value
        End Set
        Get
            Return _VENTV_ESTADO
        End Get
    End Property

    Public Property VENTV_MODALIDAD() As String
        Set(ByVal value As String)
            _VENTV_MODALIDAD = value
        End Set
        Get
            Return _VENTV_MODALIDAD
        End Get
    End Property

    Public Property VENTC_TIPDOCVEND() As String
        Set(ByVal value As String)
            _VENTC_TIPDOCVEND = value
        End Set
        Get
            Return _VENTC_TIPDOCVEND
        End Get
    End Property

    Public Property VENTV_NRODOCVEND() As String
        Set(ByVal value As String)
            _VENTV_NRODOCVEND = value
        End Set
        Get
            Return _VENTV_NRODOCVEND
        End Get
    End Property

    Public Property VENTV_CODVENDEDOR() As String
        Set(ByVal value As String)
            _VENTV_CODVENDEDOR = value
        End Set
        Get
            Return _VENTV_CODVENDEDOR
        End Get
    End Property

    Public Property VENTV_ICCID() As String
        Set(ByVal value As String)
            _VENTV_ICCID = value
        End Set
        Get
            Return _VENTV_ICCID
        End Get
    End Property

    Public Property VENTV_PLAN() As String
        Set(ByVal value As String)
            _VENTV_PLAN = value
        End Set
        Get
            Return _VENTV_PLAN
        End Get
    End Property

    Public Property VENTV_CANAL() As String
        Set(ByVal value As String)
            _VENTV_CANAL = value
        End Set
        Get
            Return _VENTV_CANAL
        End Get
    End Property

    Public Property VENTV_USUARIO_CREA() As String
        Set(ByVal value As String)
            _VENTV_USUARIO_CREA = value
        End Set
        Get
            Return _VENTV_USUARIO_CREA
        End Get
    End Property

    Public Property VENTV_USUARIO_MODIF() As String
        Set(ByVal value As String)
            _VENTV_USUARIO_MODIF = value
        End Set
        Get
            Return _VENTV_USUARIO_MODIF
        End Get
    End Property

    Public Property CODIGO_RESPUESTA() As String
        Set(ByVal value As String)
            _CODIGO_RESPUESTA = value
        End Set
        Get
            Return _CODIGO_RESPUESTA
        End Get
    End Property

    Public Property MENSAJE_RESPUESTA() As String
        Set(ByVal value As String)
            _MENSAJE_RESPUESTA = value
        End Set
        Get
            Return _MENSAJE_RESPUESTA
        End Get
    End Property

    Public Property VENTN_MONTO() As Decimal
        Set(ByVal value As Decimal)
            _VENTN_MONTO = value
        End Set
        Get
            Return _VENTN_MONTO
        End Get
    End Property
'PROY-140360-IDEA-46301 INICIO
    Public Property VENTV_IMEI() As String
        Set(ByVal value As String)
            _VENTV_IMEI = value
        End Set
        Get
            Return _VENTV_IMEI
        End Get
    End Property

    Public Property VENTV_TIPO() As String
        Set(ByVal value As String)
            _VENTV_TIPO = value
        End Set
        Get
            Return _VENTV_TIPO
        End Get
    End Property
   'Onbase
    Public Property VENTV_ID_ONBASE() As String
        Set(ByVal value As String)
            _VENTV_ID_ONBASE = value
        End Set
        Get
            Return _VENTV_ID_ONBASE
        End Get
    End Property

    'FIN PROY-140360-IDEA-46301
End Class
'PROY-33313 RP FIN