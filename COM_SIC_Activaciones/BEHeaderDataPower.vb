Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEHeaderDataPower
    Private _country As String
    Private _language As String
    Private _consumer As String
    Private _system As String
    Private _modulo As String
    Private _pid As String
    Private _userId As String
    Private _dispositivo As String
    Private _wsIp As String
    Private _operation As String
    Private _timestamp As DateTime
    Private _msgType As String
    Public BEHeaderDataPower() 'PROY-140126

    Public Property country() As String
        Get
            Return _country
        End Get
        Set(ByVal Value As String)
            _country = Value
        End Set
    End Property
    Public Property language() As String
        Get
            Return _language
        End Get
        Set(ByVal Value As String)
            _language = Value
        End Set
    End Property
    Public Property consumer() As String
        Get
            Return _consumer
        End Get
        Set(ByVal Value As String)
            _consumer = Value
        End Set
    End Property
    Public Property system() As String
        Get
            Return _system
        End Get
        Set(ByVal Value As String)
            _system = Value
        End Set
    End Property
    Public Property modulo() As String
        Get
            Return _modulo
        End Get
        Set(ByVal Value As String)
            _modulo = Value
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
    Public Property userId() As String
        Get
            Return _userId
        End Get
        Set(ByVal Value As String)
            _userId = Value
        End Set
    End Property
    Public Property dispositivo() As String
        Get
            Return _dispositivo
        End Get
        Set(ByVal Value As String)
            _dispositivo = Value
        End Set
    End Property
    Public Property wsIp() As String
        Get
            Return _wsIp
        End Get
        Set(ByVal Value As String)
            _wsIp = Value
        End Set
    End Property
    Public Property operation() As String
        Get
            Return _operation
        End Get
        Set(ByVal Value As String)
            _operation = Value
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
    Public Property msgType() As String
        Get
            Return _msgType
        End Get
        Set(ByVal Value As String)
            _msgType = Value
        End Set
    End Property
End Class
