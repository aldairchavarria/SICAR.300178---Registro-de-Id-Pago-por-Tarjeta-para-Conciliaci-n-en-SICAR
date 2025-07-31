Imports System.Runtime.Serialization

Public Class consultarProgramacionesRequest

    'INICIATIVA-219
    Dim _msisdn As String
    Dim _fechaDesde As String
    Dim _fechaHasta As String
    Dim _cuenta As String
    Dim _estado As String
    Dim _contrato As String
    Dim _tipoTransaccion As String
    Dim _codigoInteraccion As String
    Dim _asesor As String
    Dim _cadDac As String
    Dim _listaOpcional As ArrayList

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

    Public Property msisdn() As String
        Get
            Return _msisdn
        End Get
        Set(ByVal Value As String)
            _msisdn = Value
        End Set
    End Property

    Public Property fechaDesde() As String
        Get
            Return _fechaDesde
        End Get
        Set(ByVal Value As String)
            _fechaDesde = Value
        End Set
    End Property

    Public Property fechaHasta() As String
        Get
            Return _fechaHasta
        End Get
        Set(ByVal Value As String)
            _fechaHasta = Value
        End Set
    End Property

    Public Property cuenta() As String
        Get
            Return _cuenta
        End Get
        Set(ByVal Value As String)
            _cuenta = Value
        End Set
    End Property

    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal Value As String)
            _estado = Value
        End Set
    End Property

    Public Property contrato() As String
        Get
            Return _contrato
        End Get
        Set(ByVal Value As String)
            _contrato = Value
        End Set
    End Property

    Public Property tipoTransaccion() As String
        Get
            Return _tipoTransaccion
        End Get
        Set(ByVal Value As String)
            _tipoTransaccion = Value
        End Set
    End Property

    Public Property codigoInteraccion() As String
        Get
            Return _codigoInteraccion
        End Get
        Set(ByVal Value As String)
            _codigoInteraccion = Value
        End Set
    End Property

    Public Property asesor() As String
        Get
            Return _asesor
        End Get
        Set(ByVal Value As String)
            _asesor = Value
        End Set
    End Property

    Public Property cadDac() As String
        Get
            Return _cadDac
        End Get
        Set(ByVal Value As String)
            _cadDac = Value
        End Set
    End Property

End Class
