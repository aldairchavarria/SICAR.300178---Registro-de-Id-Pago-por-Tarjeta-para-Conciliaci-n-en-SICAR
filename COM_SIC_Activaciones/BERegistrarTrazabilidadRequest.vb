Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BERegistrarTrazabilidadRequest
    Public BERegistrarTrazabilidadRequest() 'PROY-140126
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Public Property auditRequest() As BERegistrarTrazabilidadAuditoria
        Get
            Return m_auditRequest
        End Get
        Set(ByVal Value As BERegistrarTrazabilidadAuditoria)
            m_auditRequest = Value
        End Set
    End Property
    Private m_auditRequest As BERegistrarTrazabilidadAuditoria
    Public Property idPadre() As String
        Get
            Return m_idPadre
        End Get
        Set(ByVal Value As String)
            m_idPadre = Value
        End Set
    End Property
    Private m_idPadre As String
    Public Property codOperacion() As String
        Get
            Return m_codOperacion
        End Get
        Set(ByVal Value As String)
            m_codOperacion = Value
        End Set
    End Property
    Private m_codOperacion As String
    Public Property sistema() As String
        Get
            Return m_sistema
        End Get
        Set(ByVal Value As String)
            m_sistema = Value
        End Set
    End Property
    Private m_sistema As String
    Public Property codCanal() As String
        Get
            Return m_codCanal
        End Get
        Set(ByVal Value As String)
            m_codCanal = Value
        End Set
    End Property
    Private m_codCanal As String
    Public Property codPdv() As String
        Get
            Return m_codPdv
        End Get
        Set(ByVal Value As String)
            m_codPdv = Value
        End Set
    End Property
    Private m_codPdv As String
    Public Property codModalVenta() As String
        Get
            Return m_codModalVenta
        End Get
        Set(ByVal Value As String)
            m_codModalVenta = Value
        End Set
    End Property
    Private m_codModalVenta As String
    Public Property tipoDocumento() As String
        Get
            Return m_tipoDocumento
        End Get
        Set(ByVal Value As String)
            m_tipoDocumento = Value
        End Set
    End Property
    Private m_tipoDocumento As String
    Public Property numeroDocumento() As String
        Get
            Return m_numeroDocumento
        End Get
        Set(ByVal Value As String)
            m_numeroDocumento = Value
        End Set
    End Property
    Private m_numeroDocumento As String
    Public Property lineas() As String
        Get
            Return m_lineas
        End Get
        Set(ByVal Value As String)
            m_lineas = Value
        End Set
    End Property
    Private m_lineas As String
    Public Property veprnId() As String
        Get
            Return m_veprnId
        End Get
        Set(ByVal Value As String)
            m_veprnId = Value
        End Set
    End Property
    Private m_veprnId As String
    Public Property dniAutorizado() As String
        Get
            Return m_dniAutorizado
        End Get
        Set(ByVal Value As String)
            m_dniAutorizado = Value
        End Set
    End Property
    Private m_dniAutorizado As String
    Public Property usuarioCtaRed() As String
        Get
            Return m_usuarioCtaRed
        End Get
        Set(ByVal Value As String)
            m_usuarioCtaRed = Value
        End Set
    End Property
    Private m_usuarioCtaRed As String
    Public Property idHijo() As String
        Get
            Return m_idHijo
        End Get
        Set(ByVal Value As String)
            m_idHijo = Value
        End Set
    End Property
    Private m_idHijo As String
    Public Property padreAnt() As String
        Get
            Return m_padreAnt
        End Get
        Set(ByVal Value As String)
            m_padreAnt = Value
        End Set
    End Property
    Private m_padreAnt As String
    Public Property dniConsultado() As String
        Get
            Return m_dniConsultado
        End Get
        Set(ByVal Value As String)
            m_dniConsultado = Value
        End Set
    End Property
    Private m_dniConsultado As String
    Public Property wsOrigen() As String
        Get
            Return m_wsOrigen
        End Get
        Set(ByVal Value As String)
            m_wsOrigen = Value
        End Set
    End Property
    Private m_wsOrigen As String
    Public Property tipoValidacion() As String
        Get
            Return m_tipoValidacion
        End Get
        Set(ByVal Value As String)
            m_tipoValidacion = Value
        End Set
    End Property
    Private m_tipoValidacion As String
    Public Property origenTipo() As String
        Get
            Return m_origenTipo
        End Get
        Set(ByVal Value As String)
            m_origenTipo = Value
        End Set
    End Property
    Private m_origenTipo As String
    Public Property codigoError() As String
        Get
            Return m_codigoError
        End Get
        Set(ByVal Value As String)
            m_codigoError = Value
        End Set
    End Property
    Private m_codigoError As String
    Public Property mensajeProceso() As String
        Get
            Return m_mensajeProceso
        End Get
        Set(ByVal Value As String)
            m_mensajeProceso = Value
        End Set
    End Property
    Private m_mensajeProceso As String
    Public Property estado() As String
        Get
            Return m_estado
        End Get
        Set(ByVal Value As String)
            m_estado = Value
        End Set
    End Property
    Private m_estado As String
    Public Property flag() As String
        Get
            Return m_flag
        End Get
        Set(ByVal Value As String)
            m_flag = Value
        End Set
    End Property
    Private m_flag As String
    Public Property listaOpcional() As ArrayList
        Get
            Return m_listaOpcional
        End Get
        Set(ByVal Value As ArrayList)
            m_listaOpcional = Value
        End Set
    End Property
    Private m_listaOpcional As ArrayList
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
