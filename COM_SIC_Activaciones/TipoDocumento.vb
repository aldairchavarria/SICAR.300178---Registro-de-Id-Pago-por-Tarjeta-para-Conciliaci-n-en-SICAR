Public Class TipoDocumento

    Private _ID_BSCS_IX As String
    Private _TDOCC_CODIGO As String

    Public Property TDOCC_CODIGO() As String
        Get
            Return _TDOCC_CODIGO
        End Get
        Set(ByVal Value As String)
            _TDOCC_CODIGO = Value
        End Set
    End Property

    Public Property ID_BSCS_IX() As String
        Get
            Return _ID_BSCS_IX
        End Get
        Set(ByVal Value As String)
            _ID_BSCS_IX = Value
        End Set
    End Property

End Class
