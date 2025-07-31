'INICIO PROY-140379
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class SmartWatchRequest

    Private _MessageRequest As MessageRequest

    Public Property MessageRequest() As MessageRequest
        Get
            Return _MessageRequest
        End Get
        Set(ByVal Value As MessageRequest)
            _MessageRequest = Value
        End Set
    End Property

    Public Sub New()
        _MessageRequest = New MessageRequest

    End Sub

End Class
'FIN PROY-140379