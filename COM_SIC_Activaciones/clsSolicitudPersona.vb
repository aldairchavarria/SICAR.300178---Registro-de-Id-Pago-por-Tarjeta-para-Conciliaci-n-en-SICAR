Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsSolicitudPersona

    Public _SOLIN_CODIGO As Integer
    Public _CLIEC_NUM_DOC As String
    Public _TDOCC_CODIGO As String
    Public _TPROC_CODIGO As String
    Public _TPROV_DESCRIPCION As String
    Public _SOPLN_TOPE_CONSUMO As String
    Public _SOPLC_MONTO_TOTAL As String
    Public _SOPLN_TOPE_CF As String
    'agregado whzr 22062015
    Public _PACUV_DESCRIPCION As String
    Public _PRDC_CODIGO As String
    Public _TOPEN_CODIGO As String
    Public _MODALIDAD_VENTA As String ''pROY 25335 R2 
    Public clsSolicitudPersona() 'PROY-140126

    Public Sub New()
    End Sub


    Public Property SOLIN_CODIGO() As Integer
        Set(ByVal value As Integer)
            _SOLIN_CODIGO = value
        End Set
        Get
            Return _SOLIN_CODIGO
        End Get
    End Property

    Public Property CLIEC_NUM_DOC() As String
        Set(ByVal value As String)
            _CLIEC_NUM_DOC = value
        End Set
        Get
            Return _CLIEC_NUM_DOC
        End Get
    End Property

    Public Property TDOCC_CODIGO() As String
        Set(ByVal value As String)
            _TDOCC_CODIGO = value
        End Set
        Get
            Return _TDOCC_CODIGO
        End Get
    End Property

    Public Property TPROC_CODIGO() As String
        Set(ByVal value As String)
            _TPROC_CODIGO = value
        End Set
        Get
            Return _TPROC_CODIGO
        End Get
    End Property

    Public Property TPROV_DESCRIPCION() As String
        Set(ByVal value As String)
            _TPROV_DESCRIPCION = value
        End Set
        Get
            Return _TPROV_DESCRIPCION
        End Get
    End Property

    Public Property SOPLN_TOPE_CONSUMO() As String
        Set(ByVal value As String)
            _SOPLN_TOPE_CONSUMO = value
        End Set
        Get
            Return _SOPLN_TOPE_CONSUMO
        End Get
    End Property

    Public Property SOPLC_MONTO_TOTAL() As String
        Set(ByVal value As String)
            _SOPLC_MONTO_TOTAL = value
        End Set
        Get
            Return _SOPLC_MONTO_TOTAL
        End Get
    End Property

    Public Property SOPLN_TOPE_CF() As String
        Set(ByVal value As String)
            _SOPLN_TOPE_CF = value
        End Set
        Get
            Return _SOPLN_TOPE_CF
        End Get
    End Property

    'agregado 22062015

    Public Property PACUV_DESCRIPCION() As String
        Set(ByVal value As String)
            _PACUV_DESCRIPCION = value
        End Set
        Get
            Return _PACUV_DESCRIPCION
        End Get
    End Property

    Public Property PRDC_CODIGO() As String
        Set(ByVal value As String)
            _PRDC_CODIGO = value
        End Set
        Get
            Return _PRDC_CODIGO
        End Get
    End Property


    Public Property TOPEN_CODIGO() As String
        Set(ByVal value As String)
            _TOPEN_CODIGO = value
        End Set
        Get
            Return _TOPEN_CODIGO
        End Get
    End Property

'pROY 25335 R2 INI
    Public Property MODALIDAD_VENTA() As String
        Set(ByVal value As String)
            _MODALIDAD_VENTA = value
        End Set
        Get
            Return _MODALIDAD_VENTA
        End Get
    End Property

'pROY 25335 R2 FIN


   
End Class
