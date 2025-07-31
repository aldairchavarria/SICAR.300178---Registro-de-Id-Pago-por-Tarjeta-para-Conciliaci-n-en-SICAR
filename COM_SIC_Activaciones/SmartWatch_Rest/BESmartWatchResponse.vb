'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BESmartWatchResponse

    Private _responseStatus As BESmartWatchStatusResponse
    Private _responseData As BESmartWatchDataResponse
    Public Property responseStatus() As BESmartWatchStatusResponse
        Set(ByVal value As BESmartWatchStatusResponse)
            _responseStatus = value
        End Set
        Get
            Return _responseStatus
        End Get
    End Property
    Public Property responseData() As BESmartWatchDataResponse
        Set(ByVal value As BESmartWatchDataResponse)
            _responseData = value
        End Set
        Get
            Return _responseData
        End Get
    End Property
    


End Class
'FIN PROY-140379

