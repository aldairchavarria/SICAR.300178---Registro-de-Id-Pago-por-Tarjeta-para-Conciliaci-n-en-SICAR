'INICIO PROY-140379
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageResponse

    Private _Header As HeadersResponse
    Private _Body As bodyResponseSmartWatch

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As bodyResponseSmartWatch
        Get
            Return _Body
        End Get
        Set(ByVal Value As bodyResponseSmartWatch)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New bodyResponseSmartWatch

    End Sub

End Class
'FIN PROY-140379