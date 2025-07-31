Public Class BEVentasContingencia

    Private _fechaPago As String
    Private _tipoVenta As String
    Private _numPedido As String
    Private _numContrato As String
    Private _numSec As String
    Private _tipoDocumento As String
    Private _descDocumento As String
    Private _numDocumento As String
    Private _tipoDocumentoBio As String
    Private _descDocumentoBio As String
    Private _numDocumentoBio As String
    Private _nombreCliente As String
    Private _codCanal As String
    Private _codOficina As String
    Private _codVendedor As String
    Private _codOperacion As String
    Private _descOperacion As String
    Private _tipoOperacion As String
    Private _tipoContingencia As String
    Private _estadoActivacion As String
    Private _codBiometria As String
    Private _descBiometria As String
    Private _codNoBiometria As String
    Private _descNoBiometria As String
    Private _linea As String
    Private _codProducto As String
    Private _estadoPago As String
    Private _cadena1 As String
    Private _cadena2 As String
    Private _cadena3 As String
    Private _cadena4 As String
    Private _cadena5 As String
    Private _cadena6 As String
    Private _reserva As String
    Private _usuario As String

    Private _tipoDoc As String
    Private _numDoc As String
    Private _tipoDocBio As String
    Private _numDocBio As String
    Private _requestOpcional As ArrayList


    Public Property fechaPago() As String
        Get
            Return _fechaPago
        End Get
        Set(ByVal Value As String)
            _fechaPago = Value
        End Set
    End Property


    Public Property tipoVenta() As String
        Get
            Return _tipoVenta
        End Get
        Set(ByVal Value As String)
            _tipoVenta = Value
        End Set
    End Property

    Public Property numPedido() As String
        Get
            Return _numPedido
        End Get
        Set(ByVal Value As String)
            _numPedido = Value
        End Set
    End Property

    Public Property numContrato() As String
        Get
            Return _numContrato
        End Get
        Set(ByVal Value As String)
            _numContrato = Value
        End Set
    End Property

    Public Property numSec() As String
        Get
            Return _numSec
        End Get
        Set(ByVal Value As String)
            _numSec = Value
        End Set
    End Property

    Public Property tipoDocumento() As String
        Get
            Return _tipoDocumento
        End Get
        Set(ByVal Value As String)
            _tipoDocumento = Value
        End Set
    End Property

    Public Property descDocumento() As String
        Get
            Return _descDocumento
        End Get
        Set(ByVal Value As String)
            _descDocumento = Value
        End Set
    End Property

    Public Property numDocumento() As String
        Get
            Return _numDocumento
        End Get
        Set(ByVal Value As String)
            _numDocumento = Value
        End Set
    End Property

    Public Property tipoDocumentoBio() As String
        Get
            Return _tipoDocumentoBio
        End Get
        Set(ByVal Value As String)
            _tipoDocumentoBio = Value
        End Set
    End Property

    Public Property descDocumentoBio() As String
        Get
            Return _descDocumentoBio
        End Get
        Set(ByVal Value As String)
            _descDocumentoBio = Value
        End Set
    End Property

    Public Property numDocumentoBio() As String
        Get
            Return _numDocumentoBio
        End Get
        Set(ByVal Value As String)
            _numDocumentoBio = Value
        End Set
    End Property

    Public Property nombreCliente() As String
        Get
            Return _nombreCliente
        End Get
        Set(ByVal Value As String)
            _nombreCliente = Value
        End Set
    End Property


    Public Property codCanal() As String
        Get
            Return _codCanal
        End Get
        Set(ByVal Value As String)
            _codCanal = Value
        End Set
    End Property

    Public Property codOficina() As String
        Get
            Return _codOficina
        End Get
        Set(ByVal Value As String)
            _codOficina = Value
        End Set
    End Property

    Public Property codVendedor() As String
        Get
            Return _codVendedor
        End Get
        Set(ByVal Value As String)
            _codVendedor = Value
        End Set
    End Property

    Public Property codOperacion() As String
        Get
            Return _codOperacion
        End Get
        Set(ByVal Value As String)
            _codOperacion = Value
        End Set
    End Property

    Public Property descOperacion() As String
        Get
            Return _descOperacion
        End Get
        Set(ByVal Value As String)
            _descOperacion = Value
        End Set
    End Property

    Public Property tipoOperacion() As String
        Get
            Return _tipoOperacion
        End Get
        Set(ByVal Value As String)
            _tipoOperacion = Value
        End Set
    End Property

    Public Property tipoContingencia() As String
        Get
            Return _tipoContingencia
        End Get
        Set(ByVal Value As String)
            _tipoContingencia = Value
        End Set
    End Property

    Public Property estadoActivacion() As String
        Get
            Return _estadoActivacion
        End Get
        Set(ByVal Value As String)
            _estadoActivacion = Value
        End Set
    End Property

    Public Property codBiometria() As String
        Get
            Return _codBiometria
        End Get
        Set(ByVal Value As String)
            _codBiometria = Value
        End Set
    End Property

    Public Property descBiometria() As String
        Get
            Return _descBiometria
        End Get
        Set(ByVal Value As String)
            _descBiometria = Value
        End Set
    End Property

    Public Property codNoBiometria() As String
        Get
            Return _codNoBiometria
        End Get
        Set(ByVal Value As String)
            _codNoBiometria = Value
        End Set
    End Property

    Public Property descNoBiometria() As String
        Get
            Return _descNoBiometria
        End Get
        Set(ByVal Value As String)
            _descNoBiometria = Value
        End Set
    End Property

    Public Property linea() As String
        Get
            Return _linea
        End Get
        Set(ByVal Value As String)
            _linea = Value
        End Set
    End Property

    Public Property codProducto() As String
        Get
            Return _codProducto
        End Get
        Set(ByVal Value As String)
            _codProducto = Value
        End Set
    End Property

    Public Property estadoPago() As String
        Get
            Return _estadoPago
        End Get
        Set(ByVal Value As String)
            _estadoPago = Value
        End Set
    End Property

    Public Property cadena1() As String
        Get
            Return _cadena1
        End Get
        Set(ByVal Value As String)
            _cadena1 = Value
        End Set
    End Property

    Public Property cadena2() As String
        Get
            Return _cadena2
        End Get
        Set(ByVal Value As String)
            _cadena2 = Value
        End Set
    End Property

    Public Property cadena3() As String
        Get
            Return _cadena3
        End Get
        Set(ByVal Value As String)
            _cadena3 = Value
        End Set
    End Property

    Public Property cadena4() As String
        Get
            Return _cadena4
        End Get
        Set(ByVal Value As String)
            _cadena4 = Value
        End Set
    End Property

    Public Property cadena5() As String
        Get
            Return _cadena5
        End Get
        Set(ByVal Value As String)
            _cadena5 = Value
        End Set
    End Property

    Public Property cadena6() As String
        Get
            Return _cadena6
        End Get
        Set(ByVal Value As String)
            _cadena6 = Value
        End Set
    End Property

    Public Property reserva() As String
        Get
            Return _reserva
        End Get
        Set(ByVal Value As String)
            _reserva = Value
        End Set
    End Property


    Public Property usuario() As String
        Get
            Return _usuario
        End Get
        Set(ByVal Value As String)
            _usuario = Value
        End Set
    End Property

    Public Property tipoDoc() As String
        Get
            Return _tipoDoc
        End Get
        Set(ByVal Value As String)
            _tipoDoc = Value
        End Set
    End Property

    Public Property numDoc() As String
        Get
            Return _numDoc
        End Get
        Set(ByVal Value As String)
            _numDoc = Value
        End Set
    End Property

    Public Property tipoDocBio() As String
        Get
            Return _tipoDocBio
        End Get
        Set(ByVal Value As String)
            _tipoDocBio = Value
        End Set
    End Property

    Public Property numDocBio() As String
        Get
            Return _numDocBio
        End Get
        Set(ByVal Value As String)
            _numDocBio = Value
        End Set
    End Property

    Public Property requestOpcional() As ArrayList
        Get
            Return _requestOpcional
        End Get
        Set(ByVal Value As ArrayList)
            _requestOpcional = Value
        End Set
    End Property

End Class
