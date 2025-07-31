Public Class serviciosAdicionales

    Private _codigoServicio As String
    Private _estado As String
    Private _categoriaServicio As String
    Private _tipoServicio As String
    Private _valorServicio As String

    Public Property codigoServicio() As String
        Get
            Return _codigoServicio
        End Get
        Set(ByVal Value As String)
            _codigoServicio = Value
        End Set
    End Property

    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal Value As String)
            _estado = Value
        End Set
    End Property

    Public Property categoriaServicio() As String
        Get
            Return _categoriaServicio
        End Get
        Set(ByVal Value As String)
            _categoriaServicio = Value
        End Set
    End Property

    Public Property tipoServicio() As String
        Get
            Return _tipoServicio
        End Get
        Set(ByVal Value As String)
            _tipoServicio = Value
        End Set
    End Property

    Public Property valorServicio() As String
        Get
            Return _valorServicio
        End Get
        Set(ByVal Value As String)
            _valorServicio = Value
        End Set
    End Property

End Class
