Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class VentaRenovaPost

    'RENOVACION POSTPAGO CAC
    Private _VENDEDOR As String
    Private _TIPO_RENOVACION As String
    Private _FLAG_EXONERACION As Integer
    Private _FLAG_DESCUENTO As Integer
    Private _TITULAR As String
    Private _REPRESENTANTE As String
    Private _TELEFONO As String
    Private _INTERACCION As String
    Private _TIPO_DOCUMENTO As String
    Private _DOC_CLIEN_NUMERO As String
    ' Inicio E77568
    Private _FLAG_FIDELIZADO_RETENIDO As String
    ' Fin E77568
    Private _SOLIN_CODIGO As Int64

    Private _PLAN_NUEVO As String
    Private _TOPE_CONSUMO As String
    Private _LIMITE_CREDITO As Double
    Private _CICLO_FACT As String

    Private _VIGENCIA_PLAN As String
    Private _FLAG_CHIP As String
    'Inicio SD854808 Relanzamiento Cambio de Plan JAZ
    Private _NUMERO_CONTRATO As String
    Private _CANAL As String
    'Fin SD854808 Relanzamiento Cambio de Plan JAZ
    '<33062>
    Private _CORREO As String
    Private _FLAG_AFILIA_CORREO As String
    Private _FECHA_REGISTRO As String 'INICIATIVA 315 - JFG
    Private _MONTO_APADECE As String 'INICIATIVA 315 - JFG

    Public VentaRenovaPost() 'PROY-140126
    '</33062>

    Public Sub New()
    End Sub

    'Inicio SD854808 Relanzamiento Cambio de Plan JAZ
    Public Property NUMERO_CONTRATO() As String
        Set(ByVal value As String)
            _NUMERO_CONTRATO = value
        End Set
        Get
            Return _NUMERO_CONTRATO
        End Get
    End Property

    Public Property CANAL() As String
        Set(ByVal value As String)
            _CANAL = value
        End Set
        Get
            Return _CANAL
        End Get
    End Property
    'Fin SD854808 Relanzamiento Cambio de Plan JAZ

    Public Property VIGENCIA_PLAN() As String
        Set(ByVal value As String)
            _VIGENCIA_PLAN = value
        End Set
        Get
            Return _VIGENCIA_PLAN
        End Get
    End Property

    Public Property PLAN_NUEVO() As String
        Set(ByVal value As String)
            _PLAN_NUEVO = value
        End Set
        Get
            Return _PLAN_NUEVO
        End Get
    End Property
    Public Property TOPE_CONSUMO() As String
        Set(ByVal value As String)
            _TOPE_CONSUMO = value
        End Set
        Get
            Return _TOPE_CONSUMO
        End Get
    End Property
    Public Property LIMITE_CREDITO() As Double
        Set(ByVal value As Double)
            _LIMITE_CREDITO = value
        End Set
        Get
            Return _LIMITE_CREDITO
        End Get
    End Property
    Public Property CICLO_FACT() As String
        Set(ByVal value As String)
            _CICLO_FACT = value
        End Set
        Get
            Return _CICLO_FACT
        End Get
    End Property


    ' Inicio E77568
    Public Property FLAG_FIDELIZADO_RETENIDO() As String
        Set(ByVal value As String)
            _FLAG_FIDELIZADO_RETENIDO = value
        End Set
        Get
            Return _FLAG_FIDELIZADO_RETENIDO
        End Get

    End Property
    ' Fin E77568

    Public Property VENDEDOR() As String
        Set(ByVal value As String)
            _VENDEDOR = value
        End Set
        Get
            Return _VENDEDOR
        End Get

    End Property

    Public Property TIPO_RENOVACION() As String
        Set(ByVal value As String)
            _TIPO_RENOVACION = value
        End Set
        Get
            Return _TIPO_RENOVACION
        End Get

    End Property

    Public Property FLAG_EXONERACION() As Integer
        Set(ByVal value As Integer)
            _FLAG_EXONERACION = value
        End Set
        Get
            Return _FLAG_EXONERACION
        End Get

    End Property

    Public Property FLAG_DESCUENTO() As Integer
        Set(ByVal value As Integer)
            _FLAG_DESCUENTO = value
        End Set
        Get
            Return _FLAG_DESCUENTO
        End Get

    End Property

    Public Property TITULAR() As String
        Set(ByVal value As String)
            _TITULAR = value
        End Set
        Get
            Return _TITULAR
        End Get

    End Property

    Public Property REPRESENTANTE() As String
        Set(ByVal value As String)
            _REPRESENTANTE = value
        End Set
        Get
            Return _REPRESENTANTE
        End Get

    End Property

    Public Property TELEFONO() As String
        Set(ByVal value As String)
            _TELEFONO = value
        End Set
        Get
            Return _TELEFONO
        End Get

    End Property

    Public Property INTERACCION() As String
        Set(ByVal value As String)
            _INTERACCION = value
        End Set
        Get
            Return _INTERACCION
        End Get

    End Property

    Public Property TIPO_DOCUMENTO() As String
        Set(ByVal value As String)
            _TIPO_DOCUMENTO = value
        End Set
        Get
            Return _TIPO_DOCUMENTO
        End Get

    End Property

    Public Property DOC_CLIEN_NUMERO() As String
        Set(ByVal value As String)
            _DOC_CLIEN_NUMERO = value
        End Set
        Get
            Return _DOC_CLIEN_NUMERO
        End Get
    End Property
    Public Property SOLIN_CODIGO() As Int64
        Set(ByVal value As Int64)
            _SOLIN_CODIGO = value
        End Set
        Get
            Return _SOLIN_CODIGO
        End Get
    End Property


    Public Property FLAG_CHIP() As String
        Set(ByVal value As String)
            _FLAG_CHIP = value
        End Set
        Get
            Return _FLAG_CHIP
        End Get
    End Property

    '<33062>
    Public Property CORREO() As String
        Set(ByVal value As String)
            _CORREO = value
        End Set
        Get
            Return _CORREO
        End Get
    End Property

    Public Property FLAG_AFILIA_CORREO() As String
        Set(ByVal value As String)
            _FLAG_AFILIA_CORREO = value
        End Set
        Get
            Return _FLAG_AFILIA_CORREO
        End Get
    End Property

    '</33062>
  
    'Inicio INICIATIVA 315 - JFG
    Public Property FECHA_REGISTRO() As String
        Set(ByVal value As String)
            _FECHA_REGISTRO = value
        End Set
        Get
            Return _FECHA_REGISTRO
        End Get
    End Property

    Public Property MONTO_APADECE() As String
        Set(ByVal value As String)
            _MONTO_APADECE = value
        End Set
        Get
            Return _MONTO_APADECE
        End Get
    End Property
    'Fin INICIATIVA 315 - JFG

End Class
