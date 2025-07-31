'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BodyResponseConsultarVentCont
    Private _consultarVentasContingenciaResponse As ConsultarVentasContingenciaResponse

    Public Property consultarVentasContingenciaResponse() As ConsultarVentasContingenciaResponse
        Get
            Return _consultarVentasContingenciaResponse
        End Get
        Set(ByVal Value As ConsultarVentasContingenciaResponse)
            _consultarVentasContingenciaResponse = Value
        End Set
    End Property

    Public Sub New()
        _consultarVentasContingenciaResponse = New ConsultarVentasContingenciaResponse
    End Sub

End Class

<Serializable()> _
Public Class ConsultarVentasContingenciaResponse

    Private _auditResponse As BEAuditResponse
    Private _ventasContingencia As BEVentasContingencia()
    Private _responseOpcional As String

    Public Property auditResponse() As BEAuditResponse
        Get
            Return _auditResponse
        End Get
        Set(ByVal Value As BEAuditResponse)
            _auditResponse = Value
        End Set
    End Property

    Public Property ventasContingencia() As BEVentasContingencia()
        Get
            Return _ventasContingencia
        End Get
        Set(ByVal Value As BEVentasContingencia())
            _ventasContingencia = Value
        End Set
    End Property


    Public Property responseOpcional() As String
        Get
            Return _responseOpcional
        End Get
        Set(ByVal Value As String)
            _responseOpcional = Value
        End Set
    End Property


    Public Sub New()
        _auditResponse = New BEAuditResponse
        '_ventasContingencia = New BEVentasContingencia
    End Sub
End Class