'INICIO PROY-140379
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class MessageRequestCancelarOrden

    Private _Header As HeadersRequest
    Private _Body As bodyRequestCancelarOrden

    Public Property Header() As HeadersRequest
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersRequest)
            _Header = Value
        End Set
    End Property

    Public Property Body() As bodyRequestCancelarOrden
        Get
            Return _Body
        End Get
        Set(ByVal Value As bodyRequestCancelarOrden)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersRequest
        _Body = New bodyRequestCancelarOrden


    End Sub

End Class
'FIN PROY-140379