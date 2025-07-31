Public Class BEDatosAuditoria
    Private _codUsuario As String

    Public Property codUsuario() As String
        Get
            Return _codUsuario
        End Get
        Set(ByVal value As String)
            _codUsuario = value
        End Set
    End Property

    Private _codPerfil As String

    Public Property codPerfil() As String
        Get
            Return _codPerfil
        End Get
        Set(ByVal value As String)
            _codPerfil = value
        End Set
    End Property
End Class
