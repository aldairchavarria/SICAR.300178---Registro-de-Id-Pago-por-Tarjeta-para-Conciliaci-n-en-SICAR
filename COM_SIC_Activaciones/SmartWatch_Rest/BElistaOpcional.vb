
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BElistaOpcional

    Private _campo As String
    Private _valor As String

    Public Property campo() As String
        Get
            Return _campo
        End Get
        Set(ByVal Value As String)
            _campo = Value
        End Set
    End Property


    Public Property valor() As String
        Get
            Return _valor
        End Get
        Set(ByVal Value As String)
            _valor = Value
        End Set
    End Property
End Class
