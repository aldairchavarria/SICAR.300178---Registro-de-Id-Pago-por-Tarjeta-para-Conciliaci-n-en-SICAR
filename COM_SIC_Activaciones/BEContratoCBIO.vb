Imports System.Runtime.Serialization

'INICIATIVA-489
<Serializable()> _
Public Class BEContratoCBIO

    Public _idTransaccion As String
    Public _cs_id As String
    Public _co_id As String
    Public _msisdn As String
    Public _motivo As String
    Public _p_observaciones As String
    Public _flag_occ_apadece As String
    Public _flag_ND_PCS As String
    Public _NDArea As String
    Public _NDMotivo As String
    Public _NDSubMotivo As String
    Public _CacDac As String
    Public _cicloFact As String
    Public _idTipoCliente As String
    Public _numDoc As String
    Public _clienteCta As String
    Public _montoPCS As String
    Public _mto_fidelizacion As String
    Public _fecha_ejecucion As String
    Public _trace As String
    Public _usuario_sistema As String
    Public _usuario_aplicacion As String
    Public _password_usuario As String
    Public _fecha_actual As String
    Public _poId As String
    Public _billCycleId As String
    Public _cuentaAsesor As String
    Public _nombresCliente As String
    Public _apellidosCliente As String
    Public _emailCliente As String
    Public _coIdPub As String
    Public _csIdPub As String

    Public BEContratoCBIO()

    Public Sub New()
    End Sub

    Public Property idTransaccion() As String
        Set(ByVal value As String)
            _idTransaccion = value
        End Set
        Get
            Return _idTransaccion
        End Get
    End Property

    Public Property cs_id() As String
        Set(ByVal value As String)
            _cs_id = value
        End Set
        Get
            Return _cs_id
        End Get
    End Property

    Public Property co_id() As String
        Set(ByVal value As String)
            _co_id = value
        End Set
        Get
            Return _co_id
        End Get
    End Property

    Public Property msisdn() As String
        Set(ByVal value As String)
            _msisdn = value
        End Set
        Get
            Return _msisdn
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

    Public Property p_observaciones() As String
        Set(ByVal value As String)
            _p_observaciones = value
        End Set
        Get
            Return _p_observaciones
        End Get
    End Property

    Public Property flag_occ_apadece() As String
        Set(ByVal value As String)
            _flag_occ_apadece = value
        End Set
        Get
            Return _flag_occ_apadece
        End Get
    End Property

    Public Property flag_ND_PCS() As String
        Set(ByVal value As String)
            _flag_ND_PCS = value
        End Set
        Get
            Return _flag_ND_PCS
        End Get
    End Property

    Public Property NDArea() As String
        Set(ByVal value As String)
            _NDArea = value
        End Set
        Get
            Return _NDArea
        End Get
    End Property

    Public Property NDMotivo() As String
        Set(ByVal value As String)
            _NDMotivo = value
        End Set
        Get
            Return _NDMotivo
        End Get
    End Property

    Public Property NDSubMotivo() As String
        Set(ByVal value As String)
            _NDSubMotivo = value
        End Set
        Get
            Return _NDSubMotivo
        End Get
    End Property

    Public Property CacDac() As String
        Set(ByVal value As String)
            _CacDac = value
        End Set
        Get
            Return _CacDac
        End Get
    End Property

    Public Property cicloFact() As String
        Set(ByVal value As String)
            _cicloFact = value
        End Set
        Get
            Return _cicloFact
        End Get
    End Property

    Public Property idTipoCliente() As String
        Set(ByVal value As String)
            _idTipoCliente = value
        End Set
        Get
            Return _idTipoCliente
        End Get
    End Property

    Public Property numDoc() As String
        Set(ByVal value As String)
            _numDoc = value
        End Set
        Get
            Return _numDoc
        End Get
    End Property

    Public Property clienteCta() As String
        Set(ByVal value As String)
            _clienteCta = value
        End Set
        Get
            Return _clienteCta
        End Get
    End Property

    Public Property montoPCS() As String
        Set(ByVal value As String)
            _montoPCS = value
        End Set
        Get
            Return _montoPCS
        End Get
    End Property

    Public Property mto_fidelizacion() As String
        Set(ByVal value As String)
            _mto_fidelizacion = value
        End Set
        Get
            Return _mto_fidelizacion
        End Get
    End Property

    Public Property fecha_ejecucion() As String
        Set(ByVal value As String)
            _fecha_ejecucion = value
        End Set
        Get
            Return _fecha_ejecucion
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

    Public Property usuario_sistema() As String
        Set(ByVal value As String)
            _usuario_sistema = value
        End Set
        Get
            Return _usuario_sistema
        End Get
    End Property

    Public Property usuario_aplicacion() As String
        Set(ByVal value As String)
            _usuario_aplicacion = value
        End Set
        Get
            Return _usuario_aplicacion
        End Get
    End Property

    Public Property password_usuario() As String
        Set(ByVal value As String)
            _password_usuario = value
        End Set
        Get
            Return _password_usuario
        End Get
    End Property

    Public Property fecha_actual() As String
        Set(ByVal value As String)
            _fecha_actual = value
        End Set
        Get
            Return _fecha_actual
        End Get
    End Property

    Public Property poId() As String
        Set(ByVal value As String)
            _poId = value
        End Set
        Get
            Return _poId
        End Get
    End Property

    Public Property billCycleId() As String
        Set(ByVal value As String)
            _billCycleId = value
        End Set
        Get
            Return _billCycleId
        End Get
    End Property

    Public Property cuentaAsesor() As String
        Set(ByVal value As String)
            _cuentaAsesor = value
        End Set
        Get
            Return _cuentaAsesor
        End Get
    End Property

    Public Property nombresCliente() As String
        Set(ByVal value As String)
            _nombresCliente = value
        End Set
        Get
            Return _nombresCliente
        End Get
    End Property

    Public Property apellidosCliente() As String
        Set(ByVal value As String)
            _apellidosCliente = value
        End Set
        Get
            Return _apellidosCliente
        End Get
    End Property

    Public Property emailCliente() As String
        Set(ByVal value As String)
            _emailCliente = value
        End Set
        Get
            Return _emailCliente
        End Get
    End Property

    Public Property coIdPub() As String
        Set(ByVal value As String)
            _coIdPub = value
        End Set
        Get
            Return _coIdPub
        End Get
    End Property

    Public Property csIdPub() As String
        Set(ByVal value As String)
            _csIdPub = value
        End Set
        Get
            Return _csIdPub
        End Get
    End Property


End Class
