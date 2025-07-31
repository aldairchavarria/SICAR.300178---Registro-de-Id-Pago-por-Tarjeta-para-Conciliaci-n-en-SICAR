Imports System.Runtime.Serialization
'PROY-26963 INI

'PROY-140126
<Serializable()> _
Public Class BEAuditoria
    Private _strUsuario As String
    Private _strTerminal As String
    Private _strHostName As String 'PROY-26963 F1 
    Private _strServername As String 'PROY-26963 F1 
    Private _strIPServer As String 'PROY-26963 F1 
    Public BEAuditoria() 'PROY-140126

    Public Property Usuario() As String
        Set(ByVal value As String)
            _strUsuario = value
        End Set
        Get
            Return _strUsuario
        End Get
    End Property
    Public Property Terminal() As String
        Set(ByVal value As String)
            _strTerminal = value
        End Set
        Get
            Return _strTerminal
        End Get
    End Property
    Public Property HostName() As String
        Set(ByVal value As String)
            _strHostName = value
        End Set
        Get
            Return _strHostName
        End Get
    End Property
    Public Property ServerName() As String
        Set(ByVal value As String)
            _strServername = value
        End Set
        Get
            Return _strServername
        End Get
    End Property
    Public Property IPServer() As String
        Set(ByVal value As String)
            _strIPServer = value
        End Set
        Get
            Return _strIPServer
        End Get
    End Property
End Class
'PROY-26963 FIN