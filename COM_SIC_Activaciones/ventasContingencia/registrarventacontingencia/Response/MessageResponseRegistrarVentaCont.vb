'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageResponseRegistrarVentaCont
    Private _Header As HeadersResponse
    Private _Body As BodyVentasContingenciaResponse

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyVentasContingenciaResponse
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyVentasContingenciaResponse)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyVentasContingenciaResponse
    End Sub


End Class

Public Class BodyVentasContingenciaResponse
    Private _registrarVentaContingenciaResponse As RegistrarVentaContingenciaResponse

    Public Property registrarVentaContingenciaResponse() As RegistrarVentaContingenciaResponse
        Get
            Return _registrarVentaContingenciaResponse
        End Get

        Set(ByVal Value As registrarVentaContingenciaResponse)
            _registrarVentaContingenciaResponse = Value
        End Set
    End Property

End Class


<Serializable()> _
Public Class RegistrarVentaContingenciaResponse
    Private _auditResponse As BEAuditResponse
    Private _responseOpcional As String

    Public Property auditResponse() As BEAuditResponse
        Get
            Return _auditResponse
        End Get
        Set(ByVal Value As BEAuditResponse)
            _auditResponse = Value
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

End Class
