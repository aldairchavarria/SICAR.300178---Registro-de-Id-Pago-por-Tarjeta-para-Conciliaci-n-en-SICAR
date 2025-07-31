Imports System.Runtime.Serialization
Public Class MessageResponseConsultarProgramaciones

    'INICIATIVA-219
    Dim _consultarProgramacionesResponse As ConsultarProgramacionesResponse

    Public Property consultarProgramacionesResponse() As ConsultarProgramacionesResponse
        Get
            Return _consultarProgramacionesResponse
        End Get
        Set(ByVal Value As ConsultarProgramacionesResponse)
            _consultarProgramacionesResponse = Value
        End Set
    End Property

    Public Sub New()
        _consultarProgramacionesResponse = New ConsultarProgramacionesResponse
    End Sub

End Class
