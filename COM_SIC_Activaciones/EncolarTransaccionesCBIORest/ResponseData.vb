Public Class ResponseData

    'INICIATIVA-219
    Private _listaOpcional As ArrayList

    Public Property listaOpcional() As ArrayList
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As ArrayList)
            _listaOpcional = Value
        End Set
    End Property


    Public Sub New()
        _listaOpcional = New ArrayList
    End Sub

End Class
