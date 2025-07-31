'INICIO PROY-140379
Imports System.Runtime.Serialization
<Serializable()> _
Public Class BESmartWatchRequest

    Private _appIOT As String
    Private _proceso As String
    Private _motivo As String
    Private _msisdnpAct As String
    Private _imsiP As String
    Private _imeiP As String
    Private _msisdnD1 As String
    Private _msisdnD2 As String
    Private _IccidP As String
    Private _listaOpcional As ArrayList


    Public Property appIOT() As String
        Set(ByVal value As String)
            _appIOT = value
        End Set
        Get
            Return _appIOT
        End Get
    End Property
    Public Property proceso() As String
        Set(ByVal value As String)
            _proceso = value
        End Set
        Get
            Return _proceso
        End Get
    End Property
    Public Property motivo() As String
        Set(ByVal value As String)
            _motivo = value
        End Set
        Get
            Return _motivo
        End Get
    End Property
    Public Property msisdnpAct() As String
        Set(ByVal value As String)
            _msisdnpAct = value
        End Set
        Get
            Return _msisdnpAct
        End Get
    End Property
    Public Property imsiP() As String
        Set(ByVal value As String)
            _imsiP = value
        End Set
        Get
            Return _imsiP
        End Get
    End Property
    Public Property imeiP() As String
        Set(ByVal value As String)
            _imeiP = value
        End Set
        Get
            Return _imeiP
        End Get
    End Property
    Public Property msisdnD1() As String
        Set(ByVal value As String)
            _msisdnD1 = value
        End Set
        Get
            Return _msisdnD1
        End Get
    End Property
    Public Property msisdnD2() As String
        Set(ByVal value As String)
            _msisdnD2 = value
        End Set
        Get
            Return _msisdnD2
        End Get
    End Property
    Public Property IccidP() As String
        Set(ByVal value As String)
            _IccidP = value
        End Set
        Get
            Return _IccidP
        End Get
    End Property
    Public Property listaOpcional() As ArrayList
        Set(ByVal value As ArrayList)
            _listaOpcional = value
        End Set
        Get
            Return _listaOpcional
        End Get
    End Property

End Class
'FIN PROY-140379

