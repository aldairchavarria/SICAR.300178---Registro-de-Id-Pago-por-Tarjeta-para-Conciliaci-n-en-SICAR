Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BodyResponseObtenerDatosPedidoAcc
    Private _datosPedidoResponse As ObtenerDatosPedidoAccCuotasTypeResponse

    Public Property datosPedidoResponse() As ObtenerDatosPedidoAccCuotasTypeResponse
        Get
            Return _datosPedidoResponse
        End Get
        Set(ByVal Value As ObtenerDatosPedidoAccCuotasTypeResponse)
            _datosPedidoResponse = Value
        End Set
    End Property

    Public Sub New()
        _datosPedidoResponse = New ObtenerDatosPedidoAccCuotasTypeResponse
    End Sub

End Class
