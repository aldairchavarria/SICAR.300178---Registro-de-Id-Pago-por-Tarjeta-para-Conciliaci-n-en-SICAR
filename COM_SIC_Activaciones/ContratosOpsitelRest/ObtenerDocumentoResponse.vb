'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDocumentoResponse

    Private _auditResponse As BEAuditResponse
    Private _responseRepositorio As RepositorioDocumentos()
    Private _responseOpcional As BElistaOpcional()

    Public Property auditResponse() As BEAuditResponse
        Get
            Return _auditResponse
        End Get
        Set(ByVal Value As BEAuditResponse)
            _auditResponse = Value
        End Set
    End Property

    Public Property responseRepositorio() As RepositorioDocumentos()
        Get
            Return _responseRepositorio
        End Get
        Set(ByVal Value As RepositorioDocumentos())
            _responseRepositorio = Value
        End Set
    End Property

    Public Property responseOpcional() As BElistaOpcional()
        Get
            Return _responseOpcional
        End Get
        Set(ByVal Value As BElistaOpcional())
            _responseOpcional = Value
        End Set
    End Property

    Public Sub New()
        _auditResponse = New BEAuditResponse
    End Sub

End Class
