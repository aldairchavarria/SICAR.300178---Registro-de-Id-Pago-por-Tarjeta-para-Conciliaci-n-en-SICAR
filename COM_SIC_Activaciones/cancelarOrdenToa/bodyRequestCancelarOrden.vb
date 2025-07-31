'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class bodyRequestCancelarOrden

    Private _cancelarOrdenToaRequest As BECancelarOrdenRequest

    Public Property cancelarOrdenToaRequest() As BECancelarOrdenRequest
        Get
            Return _cancelarOrdenToaRequest
        End Get
        Set(ByVal Value As BECancelarOrdenRequest)
            _cancelarOrdenToaRequest = Value
        End Set
    End Property
    Public Sub New()
        _cancelarOrdenToaRequest = New BECancelarOrdenRequest
    End Sub
End Class
'FIN PROY-140379