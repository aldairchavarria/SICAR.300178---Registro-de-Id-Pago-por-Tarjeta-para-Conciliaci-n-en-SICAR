Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BERegistrarTrazabilidadAuditoria
    Public BERegistrarTrazabilidadAuditoria() 'PROY-140126
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Public Property idTransaccion() As String
        Get
            Return m_idTransaccion
        End Get
        Set(ByVal Value As String)
            m_idTransaccion = Value
        End Set
    End Property
    Private m_idTransaccion As String
    Public Property ipAplicacion() As String
        Get
            Return m_ipAplicacion
        End Get
        Set(ByVal Value As String)
            m_ipAplicacion = Value
        End Set
    End Property
    Private m_ipAplicacion As String
    Public Property nombreAplicacion() As String
        Get
            Return m_nombreAplicacion
        End Get
        Set(ByVal Value As String)
            m_nombreAplicacion = Value
        End Set
    End Property
    Private m_nombreAplicacion As String
    Public Property usuarioAplicacion() As String
        Get
            Return m_usuarioAplicacion
        End Get
        Set(ByVal Value As String)
            m_usuarioAplicacion = Value
        End Set
    End Property
    Private m_usuarioAplicacion As String
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
