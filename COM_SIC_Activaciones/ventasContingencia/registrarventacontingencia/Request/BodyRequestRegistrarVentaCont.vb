Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BodyRequestRegistrarVentaCont

    Private _registrarVentaContingenciaRequest As BEVentasContingencia

    Public Property registrarVentaContingenciaRequest() As BEVentasContingencia
        Get
            Return _registrarVentaContingenciaRequest
        End Get
        Set(ByVal Value As BEVentasContingencia)
            _registrarVentaContingenciaRequest = Value
        End Set
    End Property

End Class


