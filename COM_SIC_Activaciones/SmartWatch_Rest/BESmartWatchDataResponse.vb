'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BESmartWatchDataResponse
    Private _listaDetalle As ArrayList
    Public Property listaDetalle() As ArrayList
        Set(ByVal value As ArrayList)
            _listaDetalle = value
        End Set
        Get
            Return _listaDetalle
        End Get
    End Property
End Class
'FIN PROY-140379