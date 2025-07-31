Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDatosPedidoAccCuotasTypeResponse
    Private _responseStatus As BEAuditResponse
    Private _responseData As BEObtenerDatosPedidoAccCuotas()

    Public Property responseStatus() As BEAuditResponse
        Get
            Return _responseStatus
        End Get
        Set(ByVal Value As BEAuditResponse)
            _responseStatus = Value
        End Set
    End Property

    Public Property responseData() As BEObtenerDatosPedidoAccCuotas()
        Get
            Return _responseData
        End Get
        Set(ByVal Value As BEObtenerDatosPedidoAccCuotas())
            _responseData = Value
        End Set
    End Property

    Public Sub New()
        _responseStatus = New BEAuditResponse
    End Sub
End Class
