'INC000002161718 inicio
Imports System
Public Class Status
    Private _type As String
    Private _code As String
    Private _message As String
    Private _msgid As String

    Public Property type() As String
        Get
            Return _type
        End Get
        Set(ByVal Value As String)
            _type = Value
        End Set
    End Property
    Public Property code() As String
        Get
            Return _code
        End Get
        Set(ByVal Value As String)
            _code = Value
        End Set
    End Property
    Public Property message() As String
        Get
            Return _message
        End Get
        Set(ByVal Value As String)
            _message = Value
        End Set
    End Property
    Public Property msgid() As String
        Get
            Return _msgid
        End Get
        Set(ByVal Value As String)
            _msgid = Value
        End Set
    End Property


End Class
