Imports System.Runtime.Serialization

Public Class BorrarProgramacionRequest

    'INICIATIVA-219
    Dim _msisdn As String
    Dim _serviCod As String
    Dim _servcEstado As String
    Dim _listaOpcional As ListaOpcional

    Public Property listaOpcional() As ListaOpcional
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As ListaOpcional)
            _listaOpcional = Value
        End Set
    End Property


    Public Sub New()
        _listaOpcional = New ListaOpcional
    End Sub

    Public Property msisdn() As String
        Get
            Return _msisdn
        End Get
        Set(ByVal Value As String)
            _msisdn = Value
        End Set
    End Property

    Public Property serviCod() As String
        Get
            Return _serviCod
        End Get
        Set(ByVal Value As String)
            _serviCod = Value
        End Set
    End Property

    Public Property servcEstado() As String
        Get
            Return _servcEstado
        End Get
        Set(ByVal Value As String)
            _servcEstado = Value
        End Set
    End Property

End Class
