Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsCliente

    Private _NROTRANSACCION As String
    Private _MSISDN As String
    Private _NOMBRES As String
    Private _APELLIDOS As String
    Private _TIPODOCUMENTO As String
    Private _NUMERODOCUMENTO As String
    Private _TELEFONOREFERENCIA As String
    Private _FECHANACIMIENTO As String
    Private _LUGARNACIMIENTO As String
    Private _MOTIVOREGISTRO As String
    Private _DIRECCIONCOMPLETA As String
    Private _CIUDAD As String
    Private _CODIGOEMPLEADO As String
    Private _CODIGOSISTEMA As String
    Private _TIPO As String
    Private _TEXTO As String
    Public clsCliente() 'PROY-140126

    Public Sub New()
    End Sub

    Public Property NROTRANSACCION() As String
        Set(ByVal value As String)
            _NROTRANSACCION = value
        End Set
        Get
            Return _NROTRANSACCION
        End Get

    End Property

    Public Property MSISDN() As String
        Set(ByVal value As String)
            _MSISDN = value
        End Set
        Get
            Return _MSISDN
        End Get

    End Property

    Public Property NOMBRES() As String
        Set(ByVal value As String)
            _NOMBRES = value
        End Set
        Get
            Return _NOMBRES
        End Get

    End Property

    Public Property APELLIDOS() As String
        Set(ByVal value As String)
            _APELLIDOS = value
        End Set
        Get
            Return _APELLIDOS
        End Get

    End Property

    Public Property TIPODOCUMENTO() As String
        Set(ByVal value As String)
            _TIPODOCUMENTO = value
        End Set
        Get
            Return _TIPODOCUMENTO
        End Get

    End Property

    Public Property NUMERODOCUMENTO() As String
        Set(ByVal value As String)
            _NUMERODOCUMENTO = value
        End Set
        Get
            Return _NUMERODOCUMENTO
        End Get

    End Property

    Public Property TELEFONOREFERENCIA() As String
        Set(ByVal value As String)
            _TELEFONOREFERENCIA = value
        End Set
        Get
            Return _TELEFONOREFERENCIA
        End Get

    End Property

    Public Property LUGARNACIMIENTO() As String
        Set(ByVal value As String)
            _LUGARNACIMIENTO = value
        End Set
        Get
            Return _LUGARNACIMIENTO
        End Get

    End Property

    Public Property FECHANACIMIENTO() As String
        Set(ByVal value As String)
            _FECHANACIMIENTO = value
        End Set
        Get
            Return _FECHANACIMIENTO
        End Get

    End Property

    Public Property MOTIVOREGISTRO() As String
        Set(ByVal value As String)
            _MOTIVOREGISTRO = value
        End Set
        Get
            Return _MOTIVOREGISTRO
        End Get

    End Property

    Public Property DIRECCIONCOMPLETA() As String
        Set(ByVal value As String)
            _DIRECCIONCOMPLETA = value
        End Set
        Get
            Return _DIRECCIONCOMPLETA
        End Get

    End Property

    Public Property CIUDAD() As String
        Set(ByVal value As String)
            _CIUDAD = value
        End Set
        Get
            Return _CIUDAD
        End Get

    End Property

    Public Property CODIGOEMPLEADO() As String
        Set(ByVal value As String)
            _CODIGOEMPLEADO = value
        End Set
        Get
            Return _CODIGOEMPLEADO
        End Get

    End Property

    Public Property CODIGOSISTEMA() As String
        Set(ByVal value As String)
            _CODIGOSISTEMA = value
        End Set
        Get
            Return _CODIGOSISTEMA
        End Get

    End Property

    Public Property TIPO() As String
        Set(ByVal value As String)
            _TIPO = value
        End Set
        Get
            Return _TIPO
        End Get

    End Property

    Public Property TEXTO() As String
        Set(ByVal value As String)
            _TEXTO = value
        End Set
        Get
            Return _TEXTO
        End Get

    End Property
End Class
