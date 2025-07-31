'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class RegistrarVentaContRequest
    Private _MessageRequest As MessageRequestRegistrarVentaCont

    Public Property MessageRequest() As MessageRequestRegistrarVentaCont
        Get
            Return _MessageRequest
        End Get
        Set(ByVal Value As MessageRequestRegistrarVentaCont)
            _MessageRequest = Value
        End Set
    End Property

    Public Sub New()
        _MessageRequest = New MessageRequestRegistrarVentaCont
    End Sub

End Class
