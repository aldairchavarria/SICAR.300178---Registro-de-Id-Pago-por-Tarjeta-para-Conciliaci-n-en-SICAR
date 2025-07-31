'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/
Imports System
Imports System.Collections
Imports System.Text
Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BERecargaVirtual
    Public _estado As String
    Public _estadoInsertar As String
    Public _estadoActualizar As String
    Public _fecha As String
    Public _fechaSwTrx As String
    'Public flagOperacion As String
    Public _linea As String
    Public _lineaCliente As String
    Public _listaResquestOpcional As String
    Public _montoRecarga As String
    Public _nombreUsuario As String
    Public _numeroDocumento As String
    Public _puntoVenta As String
    Public _tipoDocumento As String
    Public _trace As String
    Public _valorDescuento As String
    Public _valorIGV As String
    Public _valorSubTotal As String
    Public _valorTotal As String
    Public _valorVenta As String
    Public BERecargaVirtual() 'PROY-140126

    Public Sub New()
    End Sub

    Public Property estado() As String
        Set(ByVal value As String)
            _estado = value
        End Set
        Get
            Return _estado
        End Get
    End Property
    Public Property estadoActualizar() As String
        Set(ByVal value As String)
            _estadoActualizar = value
        End Set
        Get
            Return _estadoActualizar
        End Get
    End Property
    Public Property estadoInsertar() As String
        Set(ByVal value As String)
            _estadoInsertar = value
        End Set
        Get
            Return _estadoInsertar
        End Get
    End Property
    Public Property fecha() As String
        Set(ByVal value As String)
            _fecha = value
        End Set
        Get
            Return _fecha
        End Get
    End Property
    Public Property fechaSwTrx() As String
        Set(ByVal value As String)
            _fechaSwTrx = value
        End Set
        Get
            Return _fechaSwTrx
        End Get
    End Property

    Public Property linea() As String
        Set(ByVal value As String)
            _linea = value
        End Set
        Get
            Return _linea
        End Get
    End Property
    Public Property lineaCliente() As String
        Set(ByVal value As String)
            _lineaCliente = value
        End Set
        Get
            Return _lineaCliente
        End Get
    End Property

    Public Property listaResquestOpcional() As String
        Set(ByVal value As String)
            _listaResquestOpcional = value
        End Set
        Get
            Return _listaResquestOpcional
        End Get
    End Property
    Public Property montoRecarga() As String
        Set(ByVal value As String)
            _montoRecarga = value
        End Set
        Get
            Return _montoRecarga
        End Get
    End Property
    Public Property nombreUsuario() As String
        Set(ByVal value As String)
            _nombreUsuario = value
        End Set
        Get
            Return _nombreUsuario
        End Get
    End Property
    Public Property numeroDocumento() As String
        Set(ByVal value As String)
            _numeroDocumento = value
        End Set
        Get
            Return _numeroDocumento
        End Get
    End Property
    Public Property puntoVenta() As String
        Set(ByVal value As String)
            _puntoVenta = value
        End Set
        Get
            Return _puntoVenta
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
    Public Property trace() As String
        Set(ByVal value As String)
            _trace = value
        End Set
        Get
            Return _trace
        End Get
    End Property
    Public Property valorDescuento() As String
        Set(ByVal value As String)
            _valorDescuento = value
        End Set
        Get
            Return _valorDescuento
        End Get
    End Property
    Public Property valorIGV() As String
        Set(ByVal value As String)
            _valorIGV = value
        End Set
        Get
            Return _valorIGV
        End Get
    End Property
    Public Property valorSubTotal() As String
        Set(ByVal value As String)
            _valorSubTotal = value
        End Set
        Get
            Return _valorSubTotal
        End Get
    End Property
    Public Property valorTotal() As String
        Set(ByVal value As String)
            _valorTotal = value
        End Set
        Get
            Return _valorTotal
        End Get
    End Property
    Public Property valorVenta() As String
        Set(ByVal value As String)
            _valorVenta = value
        End Set
        Get
            Return _valorVenta
        End Get
    End Property
End Class
'/*----------------------------------------------------------------------------------------------------------------*/
Public Class BEResponseRecargaVirtual
    Public K_estado As String
    Public k_codigo_respuesta As String
    Public k_descripcion As String
    Public k_ubicacionError As String
    Public k_fecha As String
    Public k_origen As String
    Public k_XML_Request As String
    Public k_XML_Response As String
End Class

Public Class BEHeaderDataPower
    Public country As String
    Public language As String
    Public consumer As String
    Public _system As String
    Public modulo As String
    Public pid As String
    Public userId As String
    Public dispositivo As String
    Public wsIp As String
    Public operation As String
    Public msgType As String
End Class
'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/


