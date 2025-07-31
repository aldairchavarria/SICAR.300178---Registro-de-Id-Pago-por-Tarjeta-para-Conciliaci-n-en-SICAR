'INICIO PROY-140379
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class SmartWatchResponse
    Private _MessageResponse As MessageResponse

    Public Property MessageResponse() As MessageResponse
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponse)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponse

    End Sub
End Class
'FIN PROY-140379