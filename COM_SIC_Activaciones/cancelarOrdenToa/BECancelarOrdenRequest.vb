'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BECancelarOrdenRequest

    Private _nroPedido As String
    Private _aplicacion As String



    Public Property nroPedido() As String
        Set(ByVal value As String)
            _nroPedido = value
        End Set
        Get
            Return _nroPedido
        End Get
    End Property
    Public Property aplicacion() As String
        Set(ByVal value As String)
            _aplicacion = value
        End Set
        Get
            Return _aplicacion
        End Get
    End Property
End Class
'FIN PROY-140379

