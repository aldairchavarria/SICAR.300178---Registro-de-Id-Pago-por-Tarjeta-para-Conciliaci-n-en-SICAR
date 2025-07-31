Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class Interaccion

    Private _OBJID_CONTACTO As String
    Private _OBJID_SITE As String
    Private _CUENTA As String
    Private _ID_INTERACCION As String
    Private _FECHA_CREACION As String
    Private _START_DATE As String
    Private _TELEFONO As String
    Private _TIPO As String
    Private _CLASE As String
    Private _SUBCLASE As String
    Private _TIPIFICACION As String
    Private _TIPO_CODIGO As String
    Private _CLASE_CODIGO As String
    Private _SUBCLASE_CODIGO As String
    Private _INSERTADO_POR As String
    Private _TIPO_INTER As String
    Private _METODO As String
    Private _RESULTADO As String
    Private _HECHO_EN_UNO As String
    Private _AGENTE As String
    Private _NOMBRE_AGENTE As String
    Private _APELLIDO_AGENTE As String
    Private _ID_CASO As String
    Private _NOTAS As String
    Private _FLAG_CASO As String
    Private _USUARIO_PROCESO As String
    Private _ES_TFI As String
    Private _ESLINEA_INACTIVA As String
    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Private _Caso As String
    Private _SERVAFECT_CODE As String
    Private _INCONVEN_CODE As String
    Private _SERVAFECT As String
    Private _INCONVEN As String
    Private _COD_ERR As String
    Private _MSG_ERR As String

    Private _CO_ID As String
    Private _COD_PLANO As String
    Private _VALOR1 As String
    Private _VALOR2 As String

    Public Interaccion() 'PROY-140126
    'PROY-26366-IDEA-34247 FASE 1 - FIN

    Public Sub New()
    End Sub

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
    Public Property START_DATE() As String
        Set(ByVal value As String)
            _START_DATE = value
        End Set
        Get
            Return _START_DATE
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
    Public Property TIPIFICACION() As String
        Set(ByVal value As String)
            _TIPIFICACION = value
        End Set
        Get
            Return _TIPIFICACION
        End Get

    End Property
    Public Property TIPO_CODIGO() As String
        Set(ByVal value As String)
            _TIPO_CODIGO = value
        End Set
        Get
            Return _TIPO_CODIGO
        End Get

    End Property
    Public Property CLASE_CODIGO() As String
        Set(ByVal value As String)
            _CLASE_CODIGO = value
        End Set
        Get
            Return _CLASE_CODIGO
        End Get

    End Property
    Public Property SUBCLASE_CODIGO() As String
        Set(ByVal value As String)
            _SUBCLASE_CODIGO = value
        End Set
        Get
            Return _SUBCLASE_CODIGO
        End Get

    End Property
    Public Property INSERTADO_POR() As String
        Set(ByVal value As String)
            _INSERTADO_POR = value
        End Set
        Get
            Return _INSERTADO_POR
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
    Public Property METODO() As String
        Set(ByVal value As String)
            _METODO = value
        End Set
        Get
            Return _METODO
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
    Public Property NOMBRE_AGENTE() As String
        Set(ByVal value As String)
            _NOMBRE_AGENTE = value
        End Set
        Get
            Return _NOMBRE_AGENTE
        End Get

    End Property
    Public Property APELLIDO_AGENTE() As String
        Set(ByVal value As String)
            _APELLIDO_AGENTE = value
        End Set
        Get
            Return _APELLIDO_AGENTE
        End Get

    End Property
    Public Property ID_CASO() As String
        Set(ByVal value As String)
            _ID_CASO = value
        End Set
        Get
            Return _ID_CASO
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
    Public Property ES_TFI() As String
        Get
            Return _ES_TFI
        End Get
        Set(ByVal value As String)
            _ES_TFI = value
        End Set

    End Property
    Public Property ESLINEA_INACTIVA() As String
        Get
            Return _ESLINEA_INACTIVA
        End Get
        Set(ByVal value As String)
            _ESLINEA_INACTIVA = value
        End Set

    End Property

    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Public Property Caso() As String
        Get
            Return _Caso
        End Get
        Set(ByVal value As String)
            _Caso = value
        End Set

    End Property
    Public Property SERVAFECT_CODE() As String
        Get
            Return _SERVAFECT_CODE
        End Get
        Set(ByVal value As String)
            _SERVAFECT_CODE = value
        End Set

    End Property
    Public Property INCONVEN_CODE() As String
        Get
            Return _INCONVEN_CODE
        End Get
        Set(ByVal value As String)
            _INCONVEN_CODE = value
        End Set

    End Property
    Public Property SERVAFECT() As String
        Get
            Return _SERVAFECT
        End Get
        Set(ByVal value As String)
            _SERVAFECT = value
        End Set

    End Property
    Public Property INCONVEN() As String
        Get
            Return _INCONVEN
        End Get
        Set(ByVal value As String)
            _INCONVEN = value
        End Set

    End Property
    Public Property COD_ERR() As String
        Get
            Return _COD_ERR
        End Get
        Set(ByVal value As String)
            _COD_ERR = value
        End Set

    End Property
    Public Property MSG_ERR() As String
        Get
            Return _MSG_ERR
        End Get
        Set(ByVal value As String)
            _MSG_ERR = value
        End Set

    End Property
    'PROY-26366-IDEA-34247 FASE 1 - FIN

    'PROY-140743 INI
    Public Property CO_ID() As String
        Set(ByVal value As String)
            _CO_ID = value
        End Set
        Get
            Return _CO_ID
        End Get
    End Property

    Public Property COD_PLANO() As String
        Set(ByVal value As String)
            _COD_PLANO = value
        End Set
        Get
            Return _COD_PLANO
        End Get
    End Property

    Public Property VALOR1() As String
        Set(ByVal value As String)
            _VALOR1 = value
        End Set
        Get
            Return _VALOR1
        End Get
    End Property

    Public Property VALOR2() As String
        Set(ByVal value As String)
            _VALOR2 = value
        End Set
        Get
            Return _VALOR2
        End Get
    End Property
    'PROY-140743 FIN

End Class
