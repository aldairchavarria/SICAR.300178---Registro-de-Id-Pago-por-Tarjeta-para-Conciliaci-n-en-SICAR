Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class CambioPlanPostpago
    Public CambioPlanPostpago()'PROY-140126
    Private _idTransaccion As String
    Public Property idTransaccion() As String
        Set(ByVal value As String)
            _idTransaccion = value
        End Set
        Get
            Return _idTransaccion
        End Get
    End Property
    Private _ipAplicacion As String
    Public Property ipAplicacion() As String
        Set(ByVal value As String)
            _ipAplicacion = value
        End Set
        Get
            Return _ipAplicacion
        End Get
    End Property
    Private _aplicacion As String
    Public Property aplicacion() As String
        Set(ByVal value As String)
            _aplicacion = value
        End Set
        Get
            Return _aplicacion
        End Get
    End Property
    Private _msisdn As String
    Public Property msisdn() As String
        Set(ByVal value As String)
            _msisdn = value
        End Set
        Get
            Return _msisdn
        End Get
    End Property
    Private _coId As String
    Public Property coId() As String
        Set(ByVal value As String)
            _coId = value
        End Set
        Get
            Return _coId
        End Get
    End Property
    Private _customerId As String
    Public Property customerId() As String
        Set(ByVal value As String)
            _customerId = value
        End Set
        Get
            Return _customerId
        End Get
    End Property
    Private _Cuenta As String
    Public Property Cuenta() As String
        Set(ByVal value As String)
            _Cuenta = value
        End Set
        Get
            Return _Cuenta
        End Get
    End Property
    Private _escenario As String
    Public Property escenario() As String
        Set(ByVal value As String)
            _escenario = value
        End Set
        Get
            Return _escenario
        End Get
    End Property
    Private _tipoProducto As String
    Public Property tipoProducto() As String
        Set(ByVal value As String)
            _tipoProducto = value
        End Set
        Get
            Return _tipoProducto
        End Get
    End Property
    Private _serviciosAdicionales As String
    Public Property serviciosAdicionales() As String
        Set(ByVal value As String)
            _serviciosAdicionales = value
        End Set
        Get
            Return _serviciosAdicionales
        End Get
    End Property
    Private _codigoProducto As String
    Public Property codigoProducto() As String
        Set(ByVal value As String)
            _codigoProducto = value
        End Set
        Get
            Return _codigoProducto
        End Get
    End Property
    Private _codigoPlanBase As String
    Public Property codigoPlanBase() As String
        Set(ByVal value As String)
            _codigoPlanBase = value
        End Set
        Get
            Return _codigoPlanBase
        End Get
    End Property
    Private _montoApadece As String
    Public Property montoApadece() As String
        Set(ByVal value As String)
            _montoApadece = value
        End Set
        Get
            Return _montoApadece
        End Get
    End Property
    Private _montoFidelizar As String
    Public Property montoFidelizar() As String
        Set(ByVal value As String)
            _montoFidelizar = value
        End Set
        Get
            Return _montoFidelizar
        End Get
    End Property
    Private _flagValidaApadece As String
    Public Property flagValidaApadece() As String
        Set(ByVal value As String)
            _flagValidaApadece = value
        End Set
        Get
            Return _flagValidaApadece
        End Get
    End Property
    Private _flagAplicaApadece As String
    Public Property flagAplicaApadece() As String
        Set(ByVal value As String)
            _flagAplicaApadece = value
        End Set
        Get
            Return _flagAplicaApadece
        End Get
    End Property
    Private _TopeConsumo As String
    Public Property TopeConsumo() As String
        Set(ByVal value As String)
            _TopeConsumo = value
        End Set
        Get
            Return _TopeConsumo
        End Get
    End Property
    Private _tipoTope As String
    Public Property tipoTope() As String
        Set(ByVal value As String)
            _tipoTope = value
        End Set
        Get
            Return _tipoTope
        End Get
    End Property
    Private _descripcionTipoTpe As String
    Public Property descripcionTipoTpe() As String
        Set(ByVal value As String)
            _descripcionTipoTpe = value
        End Set
        Get
            Return _descripcionTipoTpe
        End Get
    End Property
    Private _tipoRegistroTope As String
    Public Property tipoRegistroTope() As String
        Set(ByVal value As String)
            _tipoRegistroTope = value
        End Set
        Get
            Return _tipoRegistroTope
        End Get
    End Property
    Private _topeControlConsumo As String
    Public Property topeControlConsumo() As String
        Set(ByVal value As String)
            _topeControlConsumo = value
        End Set
        Get
            Return _topeControlConsumo
        End Get
    End Property
    Private _fechaProgramacionTope As String
    Public Property fechaProgramacionTope() As String
        Set(ByVal value As String)
            _fechaProgramacionTope = value
        End Set
        Get
            Return _fechaProgramacionTope
        End Get
    End Property
    Private _CAC As String
    Public Property CAC() As String
        Set(ByVal value As String)
            _CAC = value
        End Set
        Get
            Return _CAC
        End Get
    End Property
    Private _asesor As String
    Public Property asesor() As String
        Set(ByVal value As String)
            _asesor = value
        End Set
        Get
            Return _asesor
        End Get
    End Property
    Private _codigoInteraccion As String
    Public Property codigoInteraccion() As String
        Set(ByVal value As String)
            _codigoInteraccion = value
        End Set
        Get
            Return _codigoInteraccion
        End Get
    End Property
    Private _montoPCS As String
    Public Property montoPCS() As String
        Set(ByVal value As String)
            _montoPCS = value
        End Set
        Get
            Return _montoPCS
        End Get
    End Property
    Private _areaPCS As String
    Public Property areaPCS() As String
        Set(ByVal value As String)
            _areaPCS = value
        End Set
        Get
            Return _areaPCS
        End Get
    End Property
    Private _motivoPCS As String
    Public Property motivoPCS() As String
        Set(ByVal value As String)
            _motivoPCS = value
        End Set
        Get
            Return _motivoPCS
        End Get
    End Property
    Private _subMotivoPCS As String
    Public Property subMotivoPCS() As String
        Set(ByVal value As String)
            _subMotivoPCS = value
        End Set
        Get
            Return _subMotivoPCS
        End Get
    End Property
    Private _cicloFacturacion As String
    Public Property cicloFacturacion() As String
        Set(ByVal value As String)
            _cicloFacturacion = value
        End Set
        Get
            Return _cicloFacturacion
        End Get
    End Property
    Private _idTipoCliente As String
    Public Property idTipoCliente() As String
        Set(ByVal value As String)
            _idTipoCliente = value
        End Set
        Get
            Return _idTipoCliente
        End Get
    End Property
    Private _numeroDocumento As String
    Public Property numeroDocumento() As String
        Set(ByVal value As String)
            _numeroDocumento = value
        End Set
        Get
            Return _numeroDocumento
        End Get
    End Property
    Private _flagServicioOnTop As String
    Public Property flagServicioOnTop() As String
        Set(ByVal value As String)
            _flagServicioOnTop = value
        End Set
        Get
            Return _flagServicioOnTop
        End Get
    End Property
    Private _fechaProgramacion As String
    Public Property fechaProgramacion() As String
        Set(ByVal value As String)
            _fechaProgramacion = value
        End Set
        Get
            Return _fechaProgramacion
        End Get
    End Property
    Private _flagLimiteCredito As String
    Public Property flagLimiteCredito() As String
        Set(ByVal value As String)
            _flagLimiteCredito = value
        End Set
        Get
            Return _flagLimiteCredito
        End Get
    End Property
    Private _tipoClarify As String
    Public Property tipoClarify() As String
        Set(ByVal value As String)
            _tipoClarify = value
        End Set
        Get
            Return _tipoClarify
        End Get
    End Property
    Private _numeroCuentaPadre As String
    Public Property numeroCuentaPadre() As String
        Set(ByVal value As String)
            _numeroCuentaPadre = value
        End Set
        Get
            Return _numeroCuentaPadre
        End Get
    End Property
    Private _usuarioAplicacion As String
    Public Property usuarioAplicacion() As String
        Set(ByVal value As String)
            _usuarioAplicacion = value
        End Set
        Get
            Return _usuarioAplicacion
        End Get
    End Property
    Private _usuarioSistema As String
    Public Property usuarioSistema() As String
        Set(ByVal value As String)
            _usuarioSistema = value
        End Set
        Get
            Return _usuarioSistema
        End Get
    End Property
    Private _strServiCod As String
    'INI: ADD PROY 26210 - RMZ
    Public Property strServiCod() As String
        Set(ByVal value As String)
            _strServiCod = value
        End Set
        Get
            Return _strServiCod
        End Get
    End Property
    Private _strServiEst As String
    Public Property strServiEst() As String
        Set(ByVal value As String)
            _strServiEst = value
        End Set
        Get
            Return _strServiEst
        End Get
    End Property
    'FIN: ADD PROY 26210 - RMZ
    Public Sub New()
    End Sub

End Class
