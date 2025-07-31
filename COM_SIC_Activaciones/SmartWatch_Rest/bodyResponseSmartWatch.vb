'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class bodyResponseSmartWatch

    Private _consultarProcesarIotResponse As BESmartWatchResponse

    Public Property consultarProcesarIotResponse() As BESmartWatchResponse
        Get
            Return _consultarProcesarIotResponse
        End Get
        Set(ByVal Value As BESmartWatchResponse)
            _consultarProcesarIotResponse = Value
        End Set
    End Property
    Public Sub New()
        _consultarProcesarIotResponse = New BESmartWatchResponse
    End Sub
End Class
'FIN PROY-140379