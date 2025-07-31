Imports System.Runtime.Serialization

Public Class MessageRequestConsultarProgramaciones

    'INICIATIVA-219
    Dim _consultarProgramacionesRequest As ConsultarProgramacionesRequest

    Public Property consultarProgramacionesRequest() As consultarProgramacionesRequest
        Get
            Return _consultarProgramacionesRequest
        End Get
        Set(ByVal Value As consultarProgramacionesRequest)
            _consultarProgramacionesRequest = Value
        End Set
    End Property

    Public Sub New()
        _consultarProgramacionesRequest = New ConsultarProgramacionesRequest
    End Sub

End Class
