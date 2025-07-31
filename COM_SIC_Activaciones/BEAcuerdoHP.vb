Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEAcuerdoHP
    Public BEAcuerdoHP()'PROY-140126
    'INICIO-PROY-25335-Contratacion Electronica R2 - GAPS
    Public Property CodigoAcuerdo() As String
        Get
            Return m_CodigoAcuerdo
        End Get
        Set(ByVal Value As String)
            m_CodigoAcuerdo = Value
        End Set
    End Property
    Private m_CodigoAcuerdo As String

    Public Property idDetalleHP() As String
        Get
            Return m_idDetalleHP
        End Get
        Set(ByVal Value As String)
            m_idDetalleHP = Value
        End Set
    End Property
    Private m_idDetalleHP As String
    Public Property Estado() As String
        Get
            Return m_Estado
        End Get
        Set(ByVal Value As String)
            m_Estado = Value
        End Set
    End Property
    Private m_Estado As String
    Public Property CodigoContrato() As String
        Get
            Return m_CodigoContrato
        End Get
        Set(ByVal Value As String)
            m_CodigoContrato = Value
        End Set
    End Property
    Private m_CodigoContrato As String
    Public Property CodigoSec() As String
        Get
            Return m_CodigoSec
        End Get
        Set(ByVal Value As String)
            m_CodigoSec = Value
        End Set
    End Property
    Private m_CodigoSec As String
    Public Property Nro_sot() As String
        Get
            Return m_Nro_sot
        End Get
        Set(ByVal Value As String)
            m_Nro_sot = Value
        End Set
    End Property
    Private m_Nro_sot As String

    Public Property CodigoHP() As String
        Get
            Return m_CodigoHP
        End Get
        Set(ByVal Value As String)
            m_CodigoHP = Value
        End Set
    End Property
    Private m_CodigoHP As String
    Public Property FechaCreacion() As String
        Get
            Return m_FechaCreacion
        End Get
        Set(ByVal Value As String)
            m_FechaCreacion = Value
        End Set
    End Property
    Private m_FechaCreacion As String
    Public Property RutaHP() As String
        Get
            Return m_RutaHP
        End Get
        Set(ByVal Value As String)
            m_RutaHP = Value
        End Set
    End Property
    Private m_RutaHP As String

    Public Property NombrePDF() As String
        Get
            Return m_NombrePDF
        End Get
        Set(ByVal Value As String)
            m_NombrePDF = Value
        End Set
    End Property
    Private m_NombrePDF As String
    'FIN-PROY-25335-Contratacion Electronica R2 - GAPS
End Class
