Public Class TransaccionAuditRequest

    'INICIATIVA-219
	Private _nombreAplicacion As String
    Private _idNegocio As String
    Private _idNegocioAux As String
    Private _tipoOperacion As String
    Private _xmlDatos As String
    Private _listaOpcional As listaOpcional

    Public Sub New()
        _listaOpcional = New ListaOpcional
    End Sub

    Public Property nombreAplicacion() As String
        Get
            Return _nombreAplicacion
        End Get
        Set(ByVal Value As String)
            _nombreAplicacion = Value
        End Set
    End Property
	
    Public Property idNegocio() As String
        Get
            Return _idNegocio
        End Get
        Set(ByVal Value As String)
            _idNegocio = Value
        End Set
    End Property

    Public Property idNegocioAux() As String
        Get
            Return _idNegocioAux
        End Get
        Set(ByVal Value As String)
            _idNegocioAux = Value
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

    Public Property xmlDatos() As String
        Get
            Return _xmlDatos
        End Get
        Set(ByVal Value As String)
            _xmlDatos = Value
        End Set
    End Property
	
	Public Property listaOpcional() As listaOpcional
        Get
            Return _listaOpcional
        End Get
        Set(ByVal Value As listaOpcional)
            _listaOpcional = Value
        End Set
    End Property
	
End Class
