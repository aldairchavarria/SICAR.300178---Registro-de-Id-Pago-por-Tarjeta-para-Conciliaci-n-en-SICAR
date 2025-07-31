Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerOrdenToaResponse
    Private _MessageResponse As MessageResponseObtenerOrdenTOA


    Public Property MessageResponse() As MessageResponseObtenerOrdenTOA
        Get
            Return _MessageResponse
        End Get
        Set(ByVal Value As MessageResponseObtenerOrdenTOA)
            _MessageResponse = Value
        End Set
    End Property

    Public Sub New()
        _MessageResponse = New MessageResponseObtenerOrdenTOA
    End Sub

End Class
