Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BERegistrarTrazabilidadResponse
    Public BERegistrarTrazabilidadResponse() 'PROY-140126
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
    Public Property codRespuesta() As String
        Get
            Return m_codRespuesta
        End Get
        Set(ByVal Value As String)
            m_codRespuesta = Value
        End Set
    End Property
    Private m_codRespuesta As String
    Public Property msjRespuesta() As String
        Get
            Return m_msjRespuesta
        End Get
        Set(ByVal Value As String)
            m_msjRespuesta = Value
        End Set
    End Property
    Private m_msjRespuesta As String
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
