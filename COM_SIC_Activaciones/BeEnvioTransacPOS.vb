Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BeEnvioTransacPOS
  Private _idCabecera As String
  Private _idTransaccionPos As String
  Private _codVenta As String
  Private _nroTienda As String
  Private _nroCaja As String
  Private _nroReferencia As String
  Private _nroAprobacion As String
  Private _codOperacion As String
  Private _desOperacion As String
  Private _tipoOperacion As String
  Private _montoOperacion As String
  Private _monedaOperacion As String
  Private _fechaTransaccion As String
  Private _nroTarjeta As String
  Private _fecExpiracion As String
  Private _codCajero As String
  Private _codAnulador As String
  Private _flagAnulacion As String
  Private _nombreCliente As String
  Private _ipCaja As String
  Private _idAnulacion As String
  Private _obsAnulacion As String
  Private _codEstablecimiento As String
  Private _tipoTarjeta As String
  Private _ipCliente As String
  Private _ipServidor As String
  Private _nombrePcCliente As String
  Private _nombrePcServidor As String
  Private _impresionVoucher As String
  Private _usuarioRed As String
  Private _tipoPago As String
  Private _numPedido As String
  Private _estadoTransaccion As String
  Private _codRespTransaccion As String
  Private _codAprobTransaccion As String
  Private _descTransaccion As String
  Private _numVoucher As String
  Private _numSeriePos As String
  Private _nombreEquipoPos As String
  Private _numTransaccion As String
  Private _tipoPos As String
  Private _tipoTransaccion As String
  Private _fechaTransaccionPos As String
  Private _horaTransaccionPos As String
  Private _nroRegistro As String
  Private _TransId As String
  Private _nroTelefono As String
  Private _UserAplicacion As String
  Private _FlagPago As String
  Private _IdTransPos As String
  Private _fechaMovimiento As String
  Private _tipoMovimiento As String
  Private _IdRefAnu As String
  Public BeEnvioTransacPOS() 'PROY-140126

  Public Sub New()
  End Sub
  Public Property IdRefAnu() As String
    Set(ByVal value As String)
      _IdRefAnu = value
    End Set
    Get
      Return _IdRefAnu
    End Get
  End Property
  Public Property IdTransPos() As String
    Set(ByVal value As String)
      _IdTransPos = value
    End Set
    Get
      Return _IdTransPos
    End Get
  End Property
  Public Property FlagPago() As String
    Set(ByVal value As String)
      _FlagPago = value
    End Set
    Get
      Return _FlagPago
    End Get
  End Property
  Public Property idTransaccionPos() As String
    Set(ByVal value As String)
      _idTransaccionPos = value
    End Set
    Get
      Return _idTransaccionPos
    End Get

  End Property
  Public Property idCabecera() As String
    Set(ByVal value As String)
      _idCabecera = value
    End Set
    Get
      Return _idCabecera
    End Get

  End Property
  Public Property codVenta() As String
    Set(ByVal value As String)
      _codVenta = value
    End Set
    Get
      Return _codVenta
    End Get

  End Property
  Public Property nroTienda() As String
    Set(ByVal value As String)
      _nroTienda = value
    End Set
    Get
      Return _nroTienda
    End Get

  End Property
  Public Property nroCaja() As String
    Set(ByVal value As String)
      _nroCaja = value
    End Set
    Get
      Return _nroCaja
    End Get
  End Property

  Public Property nroReferencia() As String
    Set(ByVal value As String)
      _nroReferencia = value
    End Set
    Get
      Return _nroReferencia
    End Get
  End Property
  Public Property nroAprobacion() As String
    Set(ByVal value As String)
      _nroAprobacion = value
    End Set
    Get
      Return _nroAprobacion
    End Get
  End Property
  Public Property codOperacion() As String
    Set(ByVal value As String)
      _codOperacion = value
    End Set
    Get
      Return _codOperacion
    End Get
  End Property
  Public Property desOperacion() As String
    Set(ByVal value As String)
      _desOperacion = value
    End Set
    Get
      Return _desOperacion
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
  Public Property montoOperacion() As String
    Set(ByVal value As String)
      _montoOperacion = value
    End Set
    Get
      Return _montoOperacion
    End Get
  End Property
  Public Property monedaOperacion() As String
    Set(ByVal value As String)
      _monedaOperacion = value
    End Set
    Get
      Return _monedaOperacion
    End Get
  End Property
  Public Property fechaTransaccion() As String
    Set(ByVal value As String)
      _fechaTransaccion = value
    End Set
    Get
      Return _fechaTransaccion
    End Get
  End Property
  Public Property nroTarjeta() As String
    Set(ByVal value As String)
      _nroTarjeta = value
    End Set
    Get
      Return _nroTarjeta
    End Get
  End Property
  Public Property fecExpiracion() As String
    Set(ByVal value As String)
      _fecExpiracion = value
    End Set
    Get
      Return _fecExpiracion
    End Get
  End Property
  Public Property codCajero() As String
    Set(ByVal value As String)
      _codCajero = value
    End Set
    Get
      Return _codCajero
    End Get
  End Property
  Public Property codAnulador() As String
    Set(ByVal value As String)
      _codAnulador = value
    End Set
    Get
      Return _codAnulador
    End Get
  End Property
  Public Property nombreCliente() As String
    Set(ByVal value As String)
      _flagAnulacion = value
    End Set
    Get
      Return _flagAnulacion
    End Get
  End Property

  Public Property flagAnulacion() As String
    Set(ByVal value As String)
      _nombreCliente = value
    End Set
    Get
      Return _nombreCliente
    End Get
  End Property
  Public Property ipCaja() As String
    Set(ByVal value As String)
      _ipCaja = value
    End Set
    Get
      Return _ipCaja
    End Get
  End Property

  Public Property idAnulacion() As String
    Set(ByVal value As String)
      _idAnulacion = value
    End Set
    Get
      Return _idAnulacion
    End Get
  End Property
  Public Property obsAnulacion() As String
    Set(ByVal value As String)
      _obsAnulacion = value
    End Set
    Get
      Return _obsAnulacion
    End Get
  End Property



  Public Property codEstablecimiento() As String
    Set(ByVal value As String)
      _codEstablecimiento = value
    End Set
    Get
      Return _codEstablecimiento
    End Get
  End Property

  Public Property tipoTarjeta() As String
    Set(ByVal value As String)
      _tipoTarjeta = value
    End Set
    Get
      Return _tipoTarjeta
    End Get
  End Property

  Public Property ipCliente() As String
    Set(ByVal value As String)
      _ipCliente = value
    End Set
    Get
      Return _ipCliente
    End Get
  End Property
  Public Property ipServidor() As String
    Set(ByVal value As String)
      _ipServidor = value
    End Set
    Get
      Return _ipServidor
    End Get
  End Property
  Public Property nombrePcCliente() As String
    Set(ByVal value As String)
      _nombrePcCliente = value
    End Set
    Get
      Return _nombrePcCliente
    End Get
  End Property
  Public Property nombrePcServidor() As String
    Set(ByVal value As String)
      _nombrePcServidor = value
    End Set
    Get
      Return _nombrePcServidor
    End Get
  End Property

  Public Property impresionVoucher() As String
    Set(ByVal value As String)
      _impresionVoucher = value
    End Set
    Get
      Return _impresionVoucher
    End Get
  End Property

  Public Property usuarioRed() As String
    Set(ByVal value As String)
      _usuarioRed = value
    End Set
    Get
      Return _usuarioRed
    End Get
  End Property

  Public Property tipoPago() As String
    Set(ByVal value As String)
      _tipoPago = value
    End Set
    Get
      Return _tipoPago
    End Get
  End Property

  Public Property numPedido() As String
    Set(ByVal value As String)
      _numPedido = value
    End Set
    Get
      Return _numPedido
    End Get
  End Property

  Public Property estadoTransaccion() As String
    Set(ByVal value As String)
      _estadoTransaccion = value
    End Set
    Get
      Return _estadoTransaccion
    End Get
  End Property

  Public Property codRespTransaccion() As String
    Set(ByVal value As String)
      _codRespTransaccion = value
    End Set
    Get
      Return _codRespTransaccion
    End Get
  End Property

  Public Property codAprobTransaccion() As String
    Set(ByVal value As String)
      _codAprobTransaccion = value
    End Set
    Get
      Return _codAprobTransaccion
    End Get
  End Property

  Public Property descTransaccion() As String
    Set(ByVal value As String)
      _descTransaccion = value
    End Set
    Get
      Return _descTransaccion
    End Get
  End Property

  Public Property numVoucher() As String
    Set(ByVal value As String)
      _numVoucher = value
    End Set
    Get
      Return _numVoucher
    End Get
  End Property

  Public Property numSeriePos() As String
    Set(ByVal value As String)
      _numSeriePos = value
    End Set
    Get
      Return _numSeriePos
    End Get
  End Property

  Public Property nombreEquipoPos() As String
    Set(ByVal value As String)
      _nombreEquipoPos = value
    End Set
    Get
      Return _nombreEquipoPos
    End Get
  End Property

  Public Property numTransaccion() As String
    Set(ByVal value As String)
      _numTransaccion = value
    End Set
    Get
      Return _numTransaccion
    End Get
  End Property

  Public Property tipoPos() As String
    Set(ByVal value As String)
      _tipoPos = value
    End Set
    Get
      Return _tipoPos
    End Get
  End Property

  Public Property tipoTransaccion() As String
    Set(ByVal value As String)
      _tipoTransaccion = value
    End Set
    Get
      Return _tipoTransaccion
    End Get
  End Property

  Public Property fechaTransaccionPos() As String
    Set(ByVal value As String)
      _fechaTransaccionPos = value
    End Set
    Get
      Return _fechaTransaccionPos
    End Get
  End Property

  Public Property horaTransaccionPos() As String
    Set(ByVal value As String)
      _horaTransaccionPos = value
    End Set
    Get
      Return _horaTransaccionPos
    End Get
  End Property

  Public Property nroRegistro() As String
    Set(ByVal value As String)
      _nroRegistro = value
    End Set
    Get
      Return _nroRegistro
    End Get
  End Property

  Public Property nroTelefono() As String
    Set(ByVal value As String)
      _nroTelefono = value
    End Set
    Get
      Return _nroTelefono
    End Get
  End Property

  Public Property TransId() As String
    Set(ByVal value As String)
      _TransId = value
    End Set
    Get
      Return _TransId
    End Get
  End Property

    Public Property fechaMovimiento() As String
        Set(ByVal value As String)
            _fechaMovimiento = value
        End Set
        Get
            Return _fechaMovimiento
        End Get
    End Property

    Public Property tipoMovimiento() As String
        Set(ByVal value As String)
            _tipoMovimiento = value
        End Set
        Get
            Return _tipoMovimiento
        End Get
    End Property

  Public Property UserAplicacion() As String
    Set(ByVal value As String)
      _UserAplicacion = value
    End Set
    Get
      Return _UserAplicacion
    End Get
  End Property

End Class
