Imports System.Runtime.Serialization

'PROY-140126 
<Serializable()> _
Public Class AuditoriaEWS
    Private _USRAPP As String
    Private _IPAPLICACION As String
    Private _APLICACION As String
    Private _IDTRANSACCION As String
    Public AuditoriaEWS()'PROY-140126
    'INC000002161718 inicio
    Private _idTransaccionNegocio As String
    Private _userId As String
    Private _applicationCodeWS As String
    Private _nameRegEdit As String
    'INC000002161718 fin

    'INI: INICIATIVA-219
    Private _msgId As String
    Private _timestamp As String

    Public Property msgId() As String
        Get
            Return _msgId
        End Get
        Set(ByVal Value As String)
            _msgId = Value
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
    'FIN: INICIATIVA-219

    Public Property USRAPP() As String
        Get
            Return _USRAPP
        End Get
        Set(ByVal Value As String)
            _USRAPP = Value
        End Set
    End Property
    Public Property IPAPLICACION() As String
        Get
            Return _IPAPLICACION
        End Get
        Set(ByVal Value As String)
            _IPAPLICACION = Value
        End Set
    End Property
    Public Property APLICACION() As String
        Get
            Return _APLICACION
        End Get
        Set(ByVal Value As String)
            _APLICACION = Value
        End Set
    End Property
    Public Property IDTRANSACCION() As String
        Get
            Return _IDTRANSACCION
        End Get
        Set(ByVal Value As String)
            _IDTRANSACCION = Value
        End Set
    End Property

    'INC000002161718 inicio
    Public Property idTransaccionNegocio() As String
        Get
            Return _idTransaccionNegocio
        End Get
        Set(ByVal Value As String)
            _idTransaccionNegocio = Value
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

    Public Property applicationCodeWS() As String
        Get
            Return _applicationCodeWS
        End Get
        Set(ByVal Value As String)
            _applicationCodeWS = Value
        End Set
    End Property

    Public Property nameRegEdit() As String
        Get
            Return _nameRegEdit
        End Get
        Set(ByVal Value As String)
            _nameRegEdit = Value
        End Set
    End Property
    'INC000002161718 fin
End Class
