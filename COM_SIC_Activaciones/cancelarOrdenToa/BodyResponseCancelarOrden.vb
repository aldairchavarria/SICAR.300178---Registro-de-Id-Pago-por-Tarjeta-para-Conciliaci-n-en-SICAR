'INC000002161718 inicio
Imports System

Public Class BodyResponseCancelarOrden

    Private _responseAudit As BECancelarOrdenResponse

    Public Property responseAudit() As BECancelarOrdenResponse
        Get
            Return _responseAudit
        End Get
        Set(ByVal Value As BECancelarOrdenResponse)
            _responseAudit = Value
        End Set
    End Property


    Public Sub New()
        _responseAudit = New BECancelarOrdenResponse
    End Sub
End Class
