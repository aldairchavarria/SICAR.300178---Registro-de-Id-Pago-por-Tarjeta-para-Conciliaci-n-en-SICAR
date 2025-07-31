Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEDocumentosGenerados

    Public _nroPedido As String
    Public _idVenta As String
    Public _nroContrato As String
    Public _nroDocumento As String
    Public _tipoDocumento As String
    Public _correo As String
    Public _tipoOperacion As String


    Public _auditRequest As String
    Public _idTransaccion As String
    Public _ipAplicacion As String
    Public _nombreAplicacion As String
    Public _usuarioAplicacion As String

    'PROY-140126
    Public BEDocumentosGenerados()

    Public Sub New()
    End Sub

    Public Property nroPedido() As String
        Set(ByVal value As String)
            _nroPedido = value
        End Set
        Get
            Return _nroPedido
        End Get
    End Property


    Public Property idVenta() As String
        Set(ByVal value As String)
            _idVenta = value
        End Set
        Get
            Return _idVenta
        End Get
    End Property



    Public Property nroContrato() As String
        Set(ByVal value As String)
            _nroContrato = value
        End Set
        Get
            Return _nroContrato
        End Get
    End Property


    Public Property nroDocumento() As String
        Set(ByVal value As String)
            _nroDocumento = value
        End Set
        Get
            Return _nroDocumento
        End Get
    End Property

    Public Property tipoDocumento() As String
        Set(ByVal value As String)
            _tipoDocumento = value

        End Set
        Get
            Return _tipoDocumento
        End Get
    End Property

    Public Property correo() As String
        Set(ByVal value As String)
            _correo = value
        End Set
        Get
            Return _correo
        End Get
    End Property

    Public Property tipoOperacion() As String
        Set(ByVal value As String)
            _tipoOperacion = value
        End Set
        Get
            Return _tipoOperacion
        End Get
    End Property

    Public Property auditRequest() As String
        Set(ByVal value As String)
            _auditRequest = value
        End Set
        Get
            Return _auditRequest
        End Get
    End Property

    Public Property idTransaccion() As String
        Set(ByVal value As String)
            _idTransaccion = value
        End Set
        Get
            Return _idTransaccion
        End Get
    End Property

    Public Property ipAplicacion() As String
        Set(ByVal value As String)
            _ipAplicacion = value
        End Set
        Get
            Return _ipAplicacion
        End Get
    End Property
    Public Property nombreAplicacion() As String
        Set(ByVal value As String)
            _nombreAplicacion = value
        End Set
        Get
            Return _nombreAplicacion
        End Get
    End Property
    Public Property usuarioAplicacion() As String
        Set(ByVal value As String)
            _usuarioAplicacion = value
        End Set
        Get
            Return _usuarioAplicacion
        End Get
    End Property
End Class
