Imports System.Runtime.Serialization

Public Class ResponseDataBorrarProgramacion

    'INICIATIVA-219
    Private _listaOpcional As ListaOpcional

    Public Property listaOpcional() As listaOpcional
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As listaOpcional)
            _listaOpcional = Value
        End Set
    End Property


    Public Sub New()
        _listaOpcional = New ListaOpcional
    End Sub

End Class
