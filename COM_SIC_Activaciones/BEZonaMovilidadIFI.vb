Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class BEZonaMovilidadIFI

    Public _tipocliente As String
    Public _linea As String
    Public _tipoDireccion As String
    Public _nombreDireccion As String
    Public _numeroDireccion As String
    Public _subDireccion As String
    Public _numeroSubDireccion As String
    Public _lote As String
    Public _tipoSubDireccion As String
    Public _tipoUrbanizacion As String
    Public _nombreUrbanizacion As String
    Public _tipoDomicilio As String
    Public _zonaEtapa As String
    Public _nombreZonaEtapa As String
    Public _referencia As String
    Public _departamento As String
    Public _provincia As String
    Public _distrito As String
    Public _codigoPostal As String
    Public _ubigeo As String
    Public _telefonoReferencia As String
    Public _flagCobro As String
    Public _accion As String
    Public _estado As String

    Public BEZonaMovilidadIFI()

    Public Sub New()
    End Sub

    Public Property tipocliente() As String
        Set(ByVal value As String)
            _tipocliente = value
        End Set
        Get
            Return _tipocliente
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

    Public Property tipoDireccion() As String
        Set(ByVal value As String)
            _tipoDireccion = value
        End Set
        Get
            Return _tipoDireccion
        End Get
    End Property

    Public Property nombreDireccion() As String
        Set(ByVal value As String)
            _nombreDireccion = value
        End Set
        Get
            Return _nombreDireccion
        End Get
    End Property
    Public Property numeroDireccion() As String
        Set(ByVal value As String)
            _numeroDireccion = value
        End Set
        Get
            Return _numeroDireccion
        End Get
    End Property
    Public Property subDireccion() As String
        Set(ByVal value As String)
            _subDireccion = value
        End Set
        Get
            Return _subDireccion
        End Get
    End Property
    Public Property numeroSubDireccion() As String
        Set(ByVal value As String)
            _numeroSubDireccion = value
        End Set
        Get
            Return _numeroSubDireccion
        End Get
    End Property
    Public Property lote() As String
        Set(ByVal value As String)
            _lote = value
        End Set
        Get
            Return _lote
        End Get
    End Property
    Public Property tipoSubDireccion() As String
        Set(ByVal value As String)
            _tipoSubDireccion = value
        End Set
        Get
            Return _tipoSubDireccion
        End Get
    End Property
    Public Property tipoUrbanizacion() As String
        Set(ByVal value As String)
            _tipoUrbanizacion = value
        End Set
        Get
            Return _tipoUrbanizacion
        End Get
    End Property
    Public Property nombreUrbanizacion() As String
        Set(ByVal value As String)
            _nombreUrbanizacion = value
        End Set
        Get
            Return _nombreUrbanizacion
        End Get
    End Property
    Public Property tipoDomicilio() As String
        Set(ByVal value As String)
            _tipoDomicilio = value
        End Set
        Get
            Return _tipoDomicilio
        End Get
    End Property
    Public Property zonaEtapa() As String
        Set(ByVal value As String)
            _zonaEtapa = value
        End Set
        Get
            Return _zonaEtapa
        End Get
    End Property
    Public Property nombreZonaEtapa() As String
        Set(ByVal value As String)
            _nombreZonaEtapa = value
        End Set
        Get
            Return _nombreZonaEtapa
        End Get
    End Property
    Public Property referencia() As String
        Set(ByVal value As String)
            _referencia = value
        End Set
        Get
            Return _referencia
        End Get
    End Property
    Public Property departamento() As String
        Set(ByVal value As String)
            _departamento = value
        End Set
        Get
            Return _departamento
        End Get
    End Property
    Public Property provincia() As String
        Set(ByVal value As String)
            _provincia = value
        End Set
        Get
            Return _provincia
        End Get
    End Property
    Public Property distrito() As String
        Set(ByVal value As String)
            _distrito = value
        End Set
        Get
            Return _distrito
        End Get
    End Property
    Public Property codigoPostal() As String
        Set(ByVal value As String)
            _codigoPostal = value
        End Set
        Get
            Return _codigoPostal
        End Get
    End Property
    Public Property ubigeo() As String
        Set(ByVal value As String)
            _ubigeo = value
        End Set
        Get
            Return _ubigeo
        End Get
    End Property
    Public Property telefonoReferencia() As String
        Set(ByVal value As String)
            _telefonoReferencia = value
        End Set
        Get
            Return _telefonoReferencia
        End Get
    End Property
    Public Property flagCobro() As String
        Set(ByVal value As String)
            _flagCobro = value
        End Set
        Get
            Return _flagCobro
        End Get
    End Property
    Public Property accion() As String
        Set(ByVal value As String)
            _accion = value
        End Set
        Get
            Return _accion
        End Get
    End Property
    Public Property estado() As String
        Set(ByVal value As String)
            _estado = value
        End Set
        Get
            Return _estado
        End Get
    End Property

End Class