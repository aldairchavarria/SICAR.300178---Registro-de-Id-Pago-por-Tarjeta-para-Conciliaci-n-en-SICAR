Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEExpedienteVenta
    Public BEExpedienteVenta() 'PROY-140126
    'CABECERA
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Public Property SEVN_ID() As Int64
        Get
            Return m_SEVN_ID
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_ID = Value
        End Set
    End Property
    Private m_SEVN_ID As Int64
    Public Property SEVN_TIPO_EXPEDIENTE() As Int64
        Get
            Return m_SEVN_TIPO_EXPEDIENTE
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_TIPO_EXPEDIENTE = Value
        End Set
    End Property
    Private m_SEVN_TIPO_EXPEDIENTE As Int64
    Public Property SEVV_ID_CONTRATO() As Int64
        Get
            Return m_SEVV_ID_CONTRATO
        End Get
        Set(ByVal Value As Int64)
            m_SEVV_ID_CONTRATO = Value
        End Set
    End Property
    Private m_SEVV_ID_CONTRATO As Int64
    Public Property SEVN_SOLIN_CODIGO() As Int64
        Get
            Return m_SEVN_SOLIN_CODIGO
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_SOLIN_CODIGO = Value
        End Set
    End Property
    Private m_SEVN_SOLIN_CODIGO As Int64
    Public Property SEVN_SOLIN_CODIGO_DET() As Int64
        Get
            Return m_SEVN_SOLIN_CODIGO_DET
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_SOLIN_CODIGO_DET = Value
        End Set
    End Property
    Private m_SEVN_SOLIN_CODIGO_DET As Int64
    Public Property SEVN_NUMSCL() As String
        Get
            Return m_SEVN_NUMSCL
        End Get
        Set(ByVal Value As String)
            m_SEVN_NUMSCL = Value
        End Set
    End Property
    Private m_SEVN_NUMSCL As String
    Public Property SEVN_PEDIN_SINERGIA() As Int64
        Get
            Return m_SEVN_PEDIN_SINERGIA
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_PEDIN_SINERGIA = Value
        End Set
    End Property
    Private m_SEVN_PEDIN_SINERGIA As Int64
    Public Property SEVN_TIPO_PRODUCTO() As String
        Get
            Return m_SEVN_TIPO_PRODUCTO
        End Get
        Set(ByVal Value As String)
            m_SEVN_TIPO_PRODUCTO = Value
        End Set
    End Property
    Private m_SEVN_TIPO_PRODUCTO As String
    Public Property SEVV_PRODUCTO() As String
        Get
            Return m_SEVV_PRODUCTO
        End Get
        Set(ByVal Value As String)
            m_SEVV_PRODUCTO = Value
        End Set
    End Property
    Private m_SEVV_PRODUCTO As String
    Public Property SEVN_MODALIDAD() As String
        Get
            Return m_SEVN_MODALIDAD
        End Get
        Set(ByVal Value As String)
            m_SEVN_MODALIDAD = Value
        End Set
    End Property
    Private m_SEVN_MODALIDAD As String
    Public Property SEVN_TIPO_OPERACION() As String
        Get
            Return m_SEVN_TIPO_OPERACION
        End Get
        Set(ByVal Value As String)
            m_SEVN_TIPO_OPERACION = Value
        End Set
    End Property
    Private m_SEVN_TIPO_OPERACION As String
    Public Property SEVV_ESTADO() As String
        Get
            Return m_SEVV_ESTADO
        End Get
        Set(ByVal Value As String)
            m_SEVV_ESTADO = Value
        End Set
    End Property
    Private m_SEVV_ESTADO As String
    Public Property SEVV_COMPLETO() As String
        Get
            Return m_SEVV_COMPLETO
        End Get
        Set(ByVal Value As String)
            m_SEVV_COMPLETO = Value
        End Set
    End Property
    Private m_SEVV_COMPLETO As String
    Public Property SEVV_FINALIZA() As String
        Get
            Return m_SEVV_FINALIZA
        End Get
        Set(ByVal Value As String)
            m_SEVV_FINALIZA = Value
        End Set
    End Property
    Private m_SEVV_FINALIZA As String
    Public Property SEVN_COD_PDV() As String
        Get
            Return m_SEVN_COD_PDV
        End Get
        Set(ByVal Value As String)
            m_SEVN_COD_PDV = Value
        End Set
    End Property
    Private m_SEVN_COD_PDV As String
    Public Property SEVN_FLAG_SIGEXP() As Int64
        Get
            Return m_SEVN_FLAG_SIGEXP
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_FLAG_SIGEXP = Value
        End Set
    End Property
    Private m_SEVN_FLAG_SIGEXP As Int64

    'REUTILIZADOS


    Public Property USUARIO_MODIFICACION() As String
        Get
            Return m_USUARIO_MODIFICACION
        End Get
        Set(ByVal Value As String)
            m_USUARIO_MODIFICACION = Value
        End Set
    End Property
    Private m_USUARIO_MODIFICACION As String
    Public Property USUARIO_CREACION() As String
        Get
            Return m_USUARIO_CREACION
        End Get
        Set(ByVal Value As String)
            m_USUARIO_CREACION = Value
        End Set
    End Property
    Private m_USUARIO_CREACION As String
    Public Property FECHA_CREACION() As DateTime
        Get
            Return m_FECHA_CREACION
        End Get
        Set(ByVal Value As DateTime)
            m_FECHA_CREACION = Value
        End Set
    End Property
    Private m_FECHA_CREACION As DateTime
    Public Property FECHA_MODIFICACION() As DateTime
        Get
            Return m_FECHA_MODIFICACION
        End Get
        Set(ByVal Value As DateTime)
            m_FECHA_MODIFICACION = Value
        End Set
    End Property
    Private m_FECHA_MODIFICACION As DateTime

    Public Property SEVN_FLAG_CORREO() As Int64
        Get
            Return m_SEVN_FLAG_CORREO
        End Get
        Set(ByVal Value As Int64)
            m_SEVN_FLAG_CORREO = Value
        End Set
    End Property
    Private m_SEVN_FLAG_CORREO As Int64
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
