Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class InteraccionTipificacion

    Private _CODIGO_EMPLEADO As String
    Private _CODIGO_SISTEMA As String
    Private _METODO_CONTACTO As String
    Private _OBJID_CONTACTO As String
    Private _OBJID_SITE As String
    Private _CUENTA As String
    Private _ID_INTERACCION As String
    Private _FECHA_CREACION As String
    Private _TELEFONO As String
    Private _TIPO As String
    Private _CLASE As String
    Private _SUBCLASE As String
    Private _RESULTADO As String
    Private _HECHO_EN_UNO As String
    Private _AGENTE As String
    Private _NOTAS As String
    Private _FLAG_CASO As String
    Private _USUARIO_PROCESO As String
    Private _P_SERVAFECT As String
    Private _P_INCONVEN As String
    Private _P_SERVAFECT_CODE As String
    Private _P_INCONVEN_CODE As String
    Private _P_CO_ID As String
    Private _P_COD_PLANO As String
    Private _P_VALOR1 As String
    Private _P_VALOR2 As String
    Private _TIPO_INTER As String
    Public InteraccionTipificacion() 'Fin PROY-140126

    Public Property METODO_CONTACTO() As String
        Set(ByVal value As String)
            _METODO_CONTACTO = value
        End Set
        Get
            Return _METODO_CONTACTO
        End Get

    End Property
    Public Property TIPO_INTER() As String
        Set(ByVal value As String)
            _TIPO_INTER = value
        End Set
        Get
            Return _TIPO_INTER
        End Get

    End Property
    Public Property CODIGO_SISTEMA() As String
        Set(ByVal value As String)
            _CODIGO_SISTEMA = value
        End Set
        Get
            Return _CODIGO_SISTEMA
        End Get

    End Property
    Public Property CODIGO_EMPLEADO() As String
        Set(ByVal value As String)
            _CODIGO_EMPLEADO = value
        End Set
        Get
            Return _CODIGO_EMPLEADO
        End Get

    End Property
    Public Property OBJID_CONTACTO() As String
        Set(ByVal value As String)
            _OBJID_CONTACTO = value
        End Set
        Get
            Return _OBJID_CONTACTO
        End Get

    End Property
    Public Property OBJID_SITE() As String
        Set(ByVal value As String)
            _OBJID_SITE = value
        End Set
        Get
            Return _OBJID_SITE
        End Get

    End Property
    Public Property CUENTA() As String
        Set(ByVal value As String)
            _CUENTA = value
        End Set
        Get
            Return _CUENTA
        End Get

    End Property
    Public Property ID_INTERACCION() As String
        Set(ByVal value As String)
            _ID_INTERACCION = value
        End Set
        Get
            Return _ID_INTERACCION
        End Get

    End Property
    Public Property FECHA_CREACION() As String
        Set(ByVal value As String)
            _FECHA_CREACION = value
        End Set
        Get
            Return _FECHA_CREACION
        End Get

    End Property
    Public Property NOTAS() As String
        Set(ByVal value As String)
            _NOTAS = value
        End Set
        Get
            Return _NOTAS
        End Get

    End Property
    Public Property TELEFONO() As String
        Set(ByVal value As String)
            _TELEFONO = value
        End Set
        Get
            Return _TELEFONO
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
    Public Property CLASE() As String
        Set(ByVal value As String)
            _CLASE = value
        End Set
        Get
            Return _CLASE
        End Get

    End Property
    Public Property SUBCLASE() As String
        Set(ByVal value As String)
            _SUBCLASE = value
        End Set
        Get
            Return _SUBCLASE
        End Get

    End Property
    Public Property RESULTADO() As String
        Set(ByVal value As String)
            _RESULTADO = value
        End Set
        Get
            Return _RESULTADO
        End Get

    End Property
    Public Property HECHO_EN_UNO() As String
        Set(ByVal value As String)
            _HECHO_EN_UNO = value
        End Set
        Get
            Return _HECHO_EN_UNO
        End Get

    End Property
    Public Property AGENTE() As String
        Set(ByVal value As String)
            _AGENTE = value
        End Set
        Get
            Return _AGENTE
        End Get

    End Property
    Public Property FLAG_CASO() As String
        Set(ByVal value As String)
            _FLAG_CASO = value
        End Set
        Get
            Return _FLAG_CASO
        End Get

    End Property
    Public Property USUARIO_PROCESO() As String

        Set(ByVal value As String)
            _USUARIO_PROCESO = value
        End Set
        Get
            Return _USUARIO_PROCESO
        End Get

    End Property
    Public Property P_SERVAFECT() As String
        Set(ByVal value As String)
            _P_SERVAFECT = value
        End Set
        Get
            Return _P_SERVAFECT
        End Get

    End Property
    Public Property P_INCONVEN() As String
        Set(ByVal value As String)
            _P_INCONVEN = value
        End Set
        Get
            Return _P_INCONVEN
        End Get

    End Property
    Public Property P_SERVAFECT_CODE() As String
        Set(ByVal value As String)
            _P_SERVAFECT_CODE = value
        End Set
        Get
            Return _P_SERVAFECT_CODE
        End Get

    End Property
    Public Property P_INCONVEN_CODE() As String
        Set(ByVal value As String)
            _P_INCONVEN_CODE = value
        End Set
        Get
            Return _P_INCONVEN_CODE
        End Get

    End Property
    Public Property P_CO_ID() As String
        Set(ByVal value As String)
            _P_CO_ID = value
        End Set
        Get
            Return _P_CO_ID
        End Get

    End Property
    Public Property P_COD_PLANO() As String
        Set(ByVal value As String)
            _P_COD_PLANO = value
        End Set
        Get
            Return _P_COD_PLANO
        End Get

    End Property
    Public Property P_VALOR1() As String
        Set(ByVal value As String)
            _P_VALOR1 = value
        End Set
        Get
            Return _P_VALOR1
        End Get

    End Property
    Public Property P_VALOR2() As String
        Set(ByVal value As String)
            _P_VALOR2 = value
        End Set
        Get
            Return _P_VALOR2
        End Get

    End Property
End Class
