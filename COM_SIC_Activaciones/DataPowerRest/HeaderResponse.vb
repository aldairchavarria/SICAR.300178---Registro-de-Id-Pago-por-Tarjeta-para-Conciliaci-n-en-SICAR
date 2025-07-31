'INC000002161718 inicio
Imports System

Public Class HeaderResponse

    Private _consumer As String
    Private _pid As String
    Private _status As New status
    Private _timestamp As String
    Private _varArg As String

    Public Property consumer() As String
        Get
            Return _consumer
        End Get
        Set(ByVal Value As String)
            _consumer = Value
        End Set
    End Property

    Public Property pid() As String
        Get
            Return _pid
        End Get
        Set(ByVal Value As String)
            _pid = Value
        End Set
    End Property

    Public Property timestamp() As String
        Get
            Return _timestamp
        End Get
        Set(ByVal Value As String)
            _timestamp = Value
        End Set
    End Property
    Public Property varArg() As String
        Get
            Return _varArg
        End Get
        Set(ByVal Value As String)
            _varArg = Value
        End Set
    End Property

    Public Property status() As status
        Get
            Return _status
        End Get
        Set(ByVal Value As status)
            _status = Value
        End Set
    End Property



    Public Sub New()
        _status = New Status
    End Sub
End Class
