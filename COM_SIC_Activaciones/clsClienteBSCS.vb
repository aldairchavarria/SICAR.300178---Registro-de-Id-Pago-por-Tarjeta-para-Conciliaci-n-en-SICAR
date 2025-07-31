Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsClienteBSCS

    Public _customerId As Integer
    Public _cuenta As String
    Public _apellidos As String
    Public _razonSocial As String
    Public _tip_doc As String
    Public _num_doc As String
    Public _titulo As String
    Public _telef_principal As String
    Public _estado_civil As String
    Public _fecha_nac As DateTime
    Public _lug_nac As String
    Public _ruc_dni As String
    Public _nomb_comercial As String
    Public _contacto_cliente As String
    Public _rep_legal As String
    Public _telef_contacto As String
    Public _fax As String
    Public _email As String
    Public _cargo As String
    Public _consultor As String
    Public _asesor As String
    Public _direccion_fac As String
    Public _urbanizacion_fac As String
    Public _distrito_fac As String
    Public _provincia_fac As String
    Public _cod_postal_fac As String
    Public _departamento_fac As String
    Public _pais_fac As String
    Public _direccion_leg As String
    Public _urbanizacion_leg As String
    Public _distrito_leg As String
    Public _provincia_leg As String
    Public _cod_postal_leg As String
    Public _departamento_leg As String
    Public _pais_leg As String
    Public _co_id As Integer
    Public _nicho_id As String
    Public _num_cuentas As Integer
    Public _num_lineas As Integer
    Public _ciclo_fac As String
    Public _status_cuenta As String
    Public _modalidad As String
    Public _tipo_cliente As String
    Public _fecha_act As DateTime
    Public _limite_credito As Double
    Public _segmento As String
    Public _respon_pago As String
    Public _credit_score As String
    Public _forma_pago As String
    Public _codigo_tipo_cliente As String
    Public _sexo As String
    Public _nacionalidad As Integer
    Public _estado_civil_id As Integer
    Public _Nombre As String
    Public clsClienteBSCS() 'PROY-140126

    'INI: INICIATIVA-219
    Public _co_id_pub As String
    Public _customer_id_pub As String
    Public _imsi As String
    Public _descripcion_plan As String
    Public _numero_telefono As String
    Public _iccid As String
    Public _billingAccountId As String
    Public _productOfferingIdNew As String
    Public _productOfferingIdOld As String
    'FIN: INICIATIVA-219

    Public Sub New()
    End Sub

    Public Property customerId() As Integer
        Set(ByVal value As Integer)
            _customerId = value
        End Set
        Get
            Return _customerId
        End Get
    End Property

    Public Property cuenta() As String
        Set(ByVal value As String)
            _cuenta = value
        End Set
        Get
            Return _cuenta
        End Get
    End Property

    Public Property Nombre() As String
        Set(ByVal value As String)
            _Nombre = value
        End Set
        Get
            Return _Nombre
        End Get
    End Property

    Public Property apellidos() As String
        Set(ByVal value As String)
            _apellidos = value
        End Set
        Get
            Return _apellidos
        End Get
    End Property
    Public Property razonSocial() As String
        Set(ByVal value As String)
            _razonSocial = value
        End Set
        Get
            Return _razonSocial
        End Get
    End Property

    Public Property tip_doc() As String
        Set(ByVal value As String)
            _tip_doc = value
        End Set
        Get
            Return _tip_doc
        End Get
    End Property

    Public Property num_doc() As String
        Set(ByVal value As String)
            _num_doc = value
        End Set
        Get
            Return _num_doc
        End Get
    End Property

    Public Property titulo() As String
        Set(ByVal value As String)
            _titulo = value
        End Set
        Get
            Return _titulo
        End Get
    End Property

    Public Property telef_principal() As String
        Set(ByVal value As String)
            _telef_principal = value
        End Set
        Get
            Return _telef_principal
        End Get
    End Property

    Public Property estado_civil() As String
        Set(ByVal value As String)
            _estado_civil = value
        End Set
        Get
            Return _estado_civil
        End Get
    End Property

    Public Property fecha_nac() As DateTime
        Set(ByVal value As DateTime)
            _fecha_nac = value
        End Set
        Get
            Return _fecha_nac
        End Get
    End Property

    Public Property lug_nac() As String
        Set(ByVal value As String)
            _lug_nac = value
        End Set
        Get
            Return _lug_nac
        End Get
    End Property

    Public Property ruc_dni() As String
        Set(ByVal value As String)
            _ruc_dni = value
        End Set
        Get
            Return _ruc_dni
        End Get
    End Property

    Public Property nomb_comercial() As String
        Set(ByVal value As String)
            _nomb_comercial = value
        End Set
        Get
            Return _nomb_comercial
        End Get
    End Property

    Public Property contacto_cliente() As String
        Set(ByVal value As String)
            _contacto_cliente = value
        End Set
        Get
            Return _contacto_cliente
        End Get
    End Property

    Public Property rep_legal() As String
        Set(ByVal value As String)
            _rep_legal = value
        End Set
        Get
            Return _rep_legal
        End Get
    End Property

    Public Property telef_contacto() As String
        Set(ByVal value As String)
            _telef_contacto = value
        End Set
        Get
            Return _telef_contacto
        End Get
    End Property

    Public Property fax() As String
        Set(ByVal value As String)
            _fax = value
        End Set
        Get
            Return _fax
        End Get
    End Property

    Public Property email() As String
        Set(ByVal value As String)
            _email = value
        End Set
        Get
            Return _email
        End Get
    End Property

    Public Property cargo() As String
        Set(ByVal value As String)
            _cargo = value
        End Set
        Get
            Return _cargo
        End Get
    End Property

    Public Property consultor() As String
        Set(ByVal value As String)
            _consultor = value
        End Set
        Get
            Return _consultor
        End Get
    End Property

    Public Property asesor() As String
        Set(ByVal value As String)
            _asesor = value
        End Set
        Get
            Return _asesor
        End Get
    End Property

    Public Property direccion_fac() As String
        Set(ByVal value As String)
            _direccion_fac = value
        End Set
        Get
            Return _direccion_fac
        End Get
    End Property

    Public Property urbanizacion_fac() As String
        Set(ByVal value As String)
            _urbanizacion_fac = value
        End Set
        Get
            Return _urbanizacion_fac
        End Get
    End Property

    Public Property distrito_fac() As String
        Set(ByVal value As String)
            _distrito_fac = value
        End Set
        Get
            Return _distrito_fac
        End Get
    End Property

    Public Property provincia_fac() As String
        Set(ByVal value As String)
            _provincia_fac = value
        End Set
        Get
            Return _provincia_fac
        End Get
    End Property

    Public Property cod_postal_fac() As String
        Set(ByVal value As String)
            _cod_postal_fac = value
        End Set
        Get
            Return _cod_postal_fac
        End Get
    End Property

    Public Property departamento_fac() As String
        Set(ByVal value As String)
            _departamento_fac = value
        End Set
        Get
            Return _departamento_fac
        End Get
    End Property

    Public Property pais_fac() As String
        Set(ByVal value As String)
            _pais_fac = value
        End Set
        Get
            Return _pais_fac
        End Get
    End Property

    Public Property direccion_leg() As String
        Set(ByVal value As String)
            _direccion_leg = value
        End Set
        Get
            Return _direccion_leg
        End Get
    End Property

    Public Property urbanizacion_leg() As String
        Set(ByVal value As String)
            _urbanizacion_leg = value
        End Set
        Get
            Return _urbanizacion_leg
        End Get
    End Property

    Public Property distrito_leg() As String
        Set(ByVal value As String)
            _distrito_leg = value
        End Set
        Get
            Return _distrito_leg
        End Get
    End Property

    Public Property provincia_leg() As String
        Set(ByVal value As String)
            _provincia_leg = value
        End Set
        Get
            Return _provincia_leg
        End Get
    End Property

    Public Property cod_postal_leg() As String
        Set(ByVal value As String)
            _cod_postal_leg = value
        End Set
        Get
            Return _cod_postal_leg
        End Get
    End Property

    Public Property departamento_leg() As String
        Set(ByVal value As String)
            _departamento_leg = value
        End Set
        Get
            Return _departamento_leg
        End Get
    End Property

    Public Property pais_leg() As String
        Set(ByVal value As String)
            _pais_leg = value
        End Set
        Get
            Return _pais_leg
        End Get
    End Property

    Public Property co_id() As Integer
        Set(ByVal value As Integer)
            _co_id = value
        End Set
        Get
            Return _co_id
        End Get
    End Property

    Public Property nicho_id() As String
        Set(ByVal value As String)
            _nicho_id = value
        End Set
        Get
            Return _nicho_id
        End Get
    End Property

    Public Property num_cuentas() As Integer
        Set(ByVal value As Integer)
            _num_cuentas = value
        End Set
        Get
            Return _num_cuentas
        End Get
    End Property

    Public Property num_lineas() As Integer
        Set(ByVal value As Integer)
            _num_lineas = value
        End Set
        Get
            Return _num_lineas
        End Get
    End Property

    Public Property ciclo_fac() As String
        Set(ByVal value As String)
            _ciclo_fac = value
        End Set
        Get
            Return _ciclo_fac
        End Get
    End Property

    Public Property status_cuenta() As String
        Set(ByVal value As String)
            _status_cuenta = value
        End Set
        Get
            Return _status_cuenta
        End Get
    End Property

    Public Property modalidad() As String
        Set(ByVal value As String)
            _modalidad = value
        End Set
        Get
            Return _modalidad
        End Get
    End Property

    Public Property tipo_cliente() As String
        Set(ByVal value As String)
            _tipo_cliente = value
        End Set
        Get
            Return _tipo_cliente
        End Get
    End Property

    Public Property fecha_act() As DateTime
        Set(ByVal value As DateTime)
            _fecha_act = value
        End Set
        Get
            Return _fecha_act
        End Get
    End Property

    Public Property limite_credito() As Double
        Set(ByVal value As Double)
            _limite_credito = value
        End Set
        Get
            Return _limite_credito
        End Get
    End Property

    Public Property segmento() As String
        Set(ByVal value As String)
            _segmento = value
        End Set
        Get
            Return _segmento
        End Get
    End Property

    Public Property respon_pago() As String
        Set(ByVal value As String)
            _respon_pago = value
        End Set
        Get
            Return _respon_pago
        End Get
    End Property

    Public Property credit_score() As String
        Set(ByVal value As String)
            _credit_score = value
        End Set
        Get
            Return _credit_score
        End Get
    End Property

    Public Property forma_pago() As String
        Set(ByVal value As String)
            _forma_pago = value
        End Set
        Get
            Return _forma_pago
        End Get
    End Property

    Public Property codigo_tipo_cliente() As String
        Set(ByVal value As String)
            _codigo_tipo_cliente = value
        End Set
        Get
            Return _codigo_tipo_cliente
        End Get
    End Property

    Public Property sexo() As String
        Set(ByVal value As String)
            _sexo = value
        End Set
        Get
            Return _sexo
        End Get
    End Property

    Public Property nacionalidad() As Integer
        Set(ByVal value As Integer)
            _nacionalidad = value
        End Set
        Get
            Return _nacionalidad
        End Get
    End Property

    Public Property estado_civil_id() As Integer
        Set(ByVal value As Integer)
            _estado_civil_id = value
        End Set
        Get
            Return _estado_civil_id
        End Get
    End Property
    'INI: INICIATIVA-219
    Public Property co_id_pub() As String
        Set(ByVal value As String)
            _co_id_pub = value
        End Set
        Get
            Return _co_id_pub
        End Get
    End Property

    Public Property customer_id_pub() As String
        Set(ByVal value As String)
            _customer_id_pub = value
        End Set
        Get
            Return _customer_id_pub
        End Get
    End Property

    Public Property imsi() As String
        Set(ByVal value As String)
            _imsi = value
        End Set
        Get
            Return _imsi
        End Get
    End Property

    Public Property descripcion_plan() As String
        Set(ByVal value As String)
            _descripcion_plan = value
        End Set
        Get
            Return _descripcion_plan
        End Get
    End Property

    Public Property numero_telefono() As String
        Set(ByVal value As String)
            _numero_telefono = value
        End Set
        Get
            Return _numero_telefono
        End Get
    End Property

    Public Property iccid() As String
        Set(ByVal value As String)
            _iccid = value
        End Set
        Get
            Return _iccid
        End Get
    End Property

    Public Property billingAccountId() As String
        Set(ByVal value As String)
            _billingAccountId = value
        End Set
        Get
            Return _billingAccountId
        End Get
    End Property

    Public Property productOfferingIdNew() As String
        Set(ByVal value As String)
            _productOfferingIdNew = value
        End Set
        Get
            Return _productOfferingIdNew
        End Get
    End Property

    Public Property productOfferingIdOld() As String
        Set(ByVal value As String)
            _productOfferingIdOld = value
        End Set
        Get
            Return _productOfferingIdOld
        End Get
    End Property
    'FIN: INICIATIVA-219

End Class
