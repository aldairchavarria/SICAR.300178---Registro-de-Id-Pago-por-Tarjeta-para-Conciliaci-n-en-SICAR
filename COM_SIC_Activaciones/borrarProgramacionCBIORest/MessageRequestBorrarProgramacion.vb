Imports System.Runtime.Serialization

Public Class MessageRequestBorrarProgramacion

    'INICIATIVA-219
    Dim _borrarProgracionRequest As BorrarProgramacionRequest

    Public Property borrarProgracionRequest() As BorrarProgramacionRequest
        Get
            Return _borrarProgracionRequest
        End Get
        Set(ByVal Value As BorrarProgramacionRequest)
            _borrarProgracionRequest = Value
        End Set
    End Property

    Public Sub New()
        _borrarProgracionRequest = New BorrarProgramacionRequest
    End Sub

End Class
