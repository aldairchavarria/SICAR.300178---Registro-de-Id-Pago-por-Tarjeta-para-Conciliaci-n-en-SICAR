Public Class MessageResponseObtenerOrdenTOA
    Private _Header As HeadersResponse
    Private _Body As BodyResponseObtenerOrdenTOA

    Public Property Header() As HeadersResponse
        Get
            Return _Header
        End Get
        Set(ByVal Value As HeadersResponse)
            _Header = Value
        End Set
    End Property

    Public Property Body() As BodyResponseObtenerOrdenTOA
        Get
            Return _Body
        End Get
        Set(ByVal Value As BodyResponseObtenerOrdenTOA)
            _Body = Value
        End Set
    End Property

    Public Sub New()
        _Header = New HeadersResponse
        _Body = New BodyResponseObtenerOrdenTOA
    End Sub
End Class
