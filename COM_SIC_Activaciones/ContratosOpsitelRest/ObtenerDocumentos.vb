'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel
Imports System
Imports System.Runtime.Serialization
<Serializable()> _
Public Class ObtenerDocumentos

    Private _numeroPedido As String
    Private _numeroContrato As String
    Private _numeroSot As String
    Private _numeroLinea As String
    Private _numeroDocumento As String
    Private _numeroSec As String
    Private _flagFirma As String
    Private _flagEnvioCorreo As String
    Private _requestOpcional As Object()

    Public Property numeroPedido() As String
        Get
            Return _numeroPedido
        End Get
        Set(ByVal Value As String)
            _numeroPedido = Value
        End Set
    End Property

    Public Property numeroContrato() As String
        Get
            Return _numeroContrato
        End Get
        Set(ByVal Value As String)
            _numeroContrato = Value
        End Set
    End Property

    Public Property numeroSot() As String
        Get
            Return _numeroSot
        End Get
        Set(ByVal Value As String)
            _numeroSot = Value
        End Set
    End Property

    Public Property numeroLinea() As String
        Get
            Return _numeroLinea
        End Get
        Set(ByVal Value As String)
            _numeroLinea = Value
        End Set
    End Property

    Public Property numeroDocumento() As String
        Get
            Return _numeroDocumento
        End Get
        Set(ByVal Value As String)
            _numeroDocumento = Value
        End Set
    End Property

    Public Property numeroSec() As String
        Get
            Return _numeroSec
        End Get
        Set(ByVal Value As String)
            _numeroSec = Value
        End Set
    End Property

    Public Property flagFirma() As String
        Get
            Return _flagFirma
        End Get
        Set(ByVal Value As String)
            _flagFirma = Value
        End Set
    End Property

    Public Property flagEnvioCorreo() As String
        Get
            Return _flagEnvioCorreo
        End Get
        Set(ByVal Value As String)
            _flagEnvioCorreo = Value
        End Set
    End Property

    Public Property requestOpcional() As Object()
        Get
            Return _requestOpcional
        End Get
        Set(ByVal Value As Object())
            _requestOpcional = Value
        End Set
    End Property


End Class
