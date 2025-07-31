Public Class ResponseDataTOA

    Private _listaDetalleActividadType As BElistaDetalleActividadType
    Private _listaOpcional As BElistaOpcional()

    Public Property listaDetalleActividadType() As BElistaDetalleActividadType
        Get
            Return _listaDetalleActividadType
        End Get
        Set(ByVal Value As BElistaDetalleActividadType)
            _listaDetalleActividadType = Value
        End Set
    End Property

    Public Property listaOpcional() As BElistaOpcional()
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As BElistaOpcional())
            _listaOpcional = Value
        End Set
    End Property

    Public Sub New()
        _listaDetalleActividadType = New BElistaDetalleActividadType
    End Sub

End Class
