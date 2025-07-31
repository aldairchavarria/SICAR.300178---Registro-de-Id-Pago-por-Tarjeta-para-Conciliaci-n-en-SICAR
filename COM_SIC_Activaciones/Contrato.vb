Imports System.Runtime.Serialization
'PROY 26210 EGSC BEGIN COPIA

''' EGSC - Se copio la clase usada en WEB_SISACT_EXPRESS

''' <summary>
''' Descripción breve de Contrato.
''' </summary>

'PROY-140126
<Serializable()> _
Public Class Contrato
    Public Contrato() 'PROY-140126

    Public Sub New()
    End Sub
    'bym
    Private _telefono As String

    Public Property telefono() As String
        Get
            Return _telefono
        End Get
        Set(ByVal Value As String)
            _telefono = Value
        End Set
    End Property
    Private _estado As String
    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(ByVal Value As String)
            _estado = Value
        End Set
    End Property
    Private _motivo As String
    Public Property motivo() As String
        Get
            Return _motivo
        End Get
        Set(ByVal Value As String)
            _motivo = Value
        End Set
    End Property
    Private _fec_estado As DateTime
    Public Property fec_estado() As DateTime
        Get
            Return _fec_estado
        End Get
        Set(ByVal Value As DateTime)
            _fec_estado = Value
        End Set
    End Property

    Private _plan As String
    Public Property plan() As String
        Get
            Return _plan
        End Get
        Set(ByVal Value As String)
            _plan = Value
        End Set
    End Property

    Private _plazo_contrato As String
    Public Property plazo_contrato() As String
        Get
            Return _plazo_contrato
        End Get
        Set(ByVal Value As String)
            _plazo_contrato = Value
        End Set
    End Property

    Private _iccid As String
    Public Property iccid() As String
        Get
            Return _iccid
        End Get
        Set(ByVal Value As String)
            _iccid = Value
        End Set
    End Property
    Private _imsi As String
    Public Property imsi() As String
        Get
            Return _imsi
        End Get
        Set(ByVal Value As String)
            _imsi = Value
        End Set
    End Property

    Private _campania As String
    Public Property campania() As String
        Get
            Return _campania
        End Get
        Set(ByVal Value As String)
            _campania = Value
        End Set
    End Property

    Private _p_venta As String
    Public Property p_venta() As String
        Get
            Return _p_venta
        End Get
        Set(ByVal Value As String)
            _p_venta = Value
        End Set
    End Property



    Private _vendedor As String
    Public Property vendedor() As String
        Get
            Return _vendedor
        End Get
        Set(ByVal Value As String)
            _vendedor = Value
        End Set
    End Property



    Private _co_id As Integer
    Public Property co_id() As Integer
        Get
            Return _co_id
        End Get
        Set(ByVal Value As Integer)
            _co_id = Value
        End Set
    End Property


    Private _fecha_act As DateTime
    Public Property fecha_act() As DateTime
        Get
            Return _fecha_act
        End Get
        Set(ByVal Value As DateTime)
            _fecha_act = Value
        End Set
    End Property



    Private _flag_plataforma As String
    Public Property flag_plataforma() As String
        Get
            Return _flag_plataforma
        End Get
        Set(ByVal Value As String)
            _flag_plataforma = Value
        End Set
    End Property

    Private _pin1 As String
    Public Property pin1() As String
        Get
            Return _pin1
        End Get
        Set(ByVal Value As String)
            _pin1 = Value
        End Set
    End Property

    Private _puk1 As String
    Public Property puk1() As String
        Get
            Return _puk1
        End Get
        Set(ByVal Value As String)
            _puk1 = Value
        End Set
    End Property

    Private _pin2 As String
    Public Property pin2() As String
        Get
            Return _pin2
        End Get
        Set(ByVal Value As String)
            _pin2 = Value
        End Set
    End Property

    Private _puk2 As String
    Public Property puk2() As String
        Get
            Return _puk2
        End Get
        Set(ByVal Value As String)
            _puk2 = Value
        End Set
    End Property

    Private _codigo_plan_tarifario As Integer
    Public Property codigo_plan_tarifario() As Integer
        Get
            Return _codigo_plan_tarifario
        End Get
        Set(ByVal Value As Integer)
            _codigo_plan_tarifario = Value
        End Set
    End Property


End Class
'PROY 26210 

