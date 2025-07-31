'INICIO PROY-140379
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageRequest

    Private _Header As HeadersRequest
    Private _Body As bodyRequestSmartWatch

    Public Property Header() As HeadersRequest
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersRequest)
            _Header = Value
        End Set
    End Property

    Public Property Body() As bodyRequestSmartWatch
        Get
            Return _Body
        End Get
        Set(ByVal Value As bodyRequestSmartWatch)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersRequest
        _Body = New bodyRequestSmartWatch

    End Sub

End Class
'FIN PROY-140379