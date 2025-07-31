'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class bodyRequestSmartWatch

    Private _consultarProcesarIotRequest As BESmartWatchRequest

    Public Property consultarProcesarIotRequest() As BESmartWatchRequest
        Get
            Return _consultarProcesarIotRequest
        End Get
        Set(ByVal Value As BESmartWatchRequest)
            _consultarProcesarIotRequest = Value
        End Set
    End Property
    Public Sub New()
        _consultarProcesarIotRequest = New BESmartWatchRequest
    End Sub
End Class
'FIN PROY-140379