'INC000002161718 inicio
Imports System

Public Class BEAuditRequest

    Private _idTransaccion As String
    Private _ipAplicacion As String
    Private _nombreAplicacion As String
    Private _usuarioAplicacion As String

    Public Property idTransaccion() As String
        Get
            Return _idTransaccion
        End Get
        Set(ByVal Value As String)
            _idTransaccion = Value
        End Set
    End Property

    Public Property ipAplicacion() As String
        Get
            Return _ipAplicacion
        End Get
        Set(ByVal Value As String)
            _ipAplicacion = Value
        End Set
    End Property
    Public Property nombreAplicacion() As String
        Get
            Return _nombreAplicacion
        End Get
        Set(ByVal Value As String)
            _nombreAplicacion = Value
        End Set
    End Property
    Public Property usuarioAplicacion() As String
        Get
            Return _usuarioAplicacion
        End Get
        Set(ByVal Value As String)
            _usuarioAplicacion = Value
        End Set
    End Property

End Class
