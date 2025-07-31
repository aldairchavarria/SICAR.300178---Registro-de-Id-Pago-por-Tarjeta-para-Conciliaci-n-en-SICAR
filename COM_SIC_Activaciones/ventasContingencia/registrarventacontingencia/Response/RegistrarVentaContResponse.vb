'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class RegistrarVentaContResponse
    Private _MessageResponse As MessageResponseRegistrarVentaCont

    Public Property MessageResponse() As MessageResponseRegistrarVentaCont
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseRegistrarVentaCont)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseRegistrarVentaCont
    End Sub

End Class
