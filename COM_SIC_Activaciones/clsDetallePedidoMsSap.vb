Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsDetallePedidoMsSap

    Private _VEPR_PEDIN_NROPEDIDO As String
    Private _VEPR_INTEV_CODINTERLOCUTOR As String
    Private _VEPR_SERIC_CODSERIE As String
    Private _VEPR_DEPEV_NROTELEFONO As String
    Public clsDetallePedidoMsSap() 'PROY-140126

    Public Property K_PEDIN_NROPEDIDO() As String
        Set(ByVal value As String)
            _VEPR_PEDIN_NROPEDIDO = value
        End Set
        Get
            Return _VEPR_PEDIN_NROPEDIDO
        End Get
    End Property
    Public Property K_INTEV_CODINTERLOCUTOR() As String
        Set(ByVal value As String)
            _VEPR_INTEV_CODINTERLOCUTOR = value
        End Set
        Get
            Return _VEPR_INTEV_CODINTERLOCUTOR
        End Get
    End Property
    Public Property K_SERIC_CODSERIE() As String
        Set(ByVal value As String)
            _VEPR_SERIC_CODSERIE = value
        End Set
        Get
            Return _VEPR_SERIC_CODSERIE
        End Get
    End Property
    Public Property K_DEPEV_NROTELEFONO() As String
        Set(ByVal value As String)
            _VEPR_DEPEV_NROTELEFONO = value
        End Set
        Get
            Return _VEPR_DEPEV_NROTELEFONO
        End Get
    End Property
End Class