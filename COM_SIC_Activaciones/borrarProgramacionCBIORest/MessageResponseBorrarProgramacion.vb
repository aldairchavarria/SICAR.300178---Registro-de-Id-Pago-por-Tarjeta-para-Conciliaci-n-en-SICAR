Imports System.Runtime.Serialization

Public Class MessageResponseBorrarProgramacion

    'INICIATIVA-219
    Dim _borrarProgracionResponse As BorrarProgramacionResponse

    Public Property borrarProgracionResponse() As BorrarProgramacionResponse
        Get
            Return _borrarProgracionResponse
        End Get
        Set(ByVal Value As BorrarProgramacionResponse)
            _borrarProgracionResponse = Value
        End Set
    End Property

    Public Sub New()
        _borrarProgracionResponse = New BorrarProgramacionResponse
    End Sub

End Class
