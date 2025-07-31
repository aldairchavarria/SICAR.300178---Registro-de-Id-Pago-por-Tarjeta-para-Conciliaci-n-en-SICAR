'PROY-140715  - IDEA 140805 | No biometría en SISACT en caída RENIEC | INICIO
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ConsultarVentaContResponse
    Private _MessageResponse As MessageResponseConsultarVentCont

    Public Property MessageResponse() As MessageResponseConsultarVentCont
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseConsultarVentCont)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseConsultarVentCont
    End Sub

End Class
