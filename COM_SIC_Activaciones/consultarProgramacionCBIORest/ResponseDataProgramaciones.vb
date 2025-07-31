Imports System.Runtime.Serialization

Public Class ResponseDataProgramaciones

    'INICIATIVA-219
    Dim _result As Int64
    Dim _listaProgramaciones As listaProgramaciones()

    Public Property listaProgramaciones() As listaProgramaciones()
        Get
            Return _listaProgramaciones
        End Get
        Set(ByVal Value As listaProgramaciones())
            _listaProgramaciones = Value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Property result() As Int64
        Get
            Return _result
        End Get
        Set(ByVal Value As Int64)
            _result = Value
        End Set
    End Property

End Class
