Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class DetalleVentaPrepago


    Private _VEPR_ID As String
    Private _VEPR_TIPO_DOC As String
    Private _VEPR_NUM_DOC As String
    Private _VEPR_COD_CAN As String
    Private _VEPR_COD_PDV As String
    Private _VEPR_TIPO_VENT As String
    Private _VEPR_TIPO_OPERACION As String
    Private _VEPR_TIPO_PROD As String
    Private _VEPR_COD_DEPA As String
    Private _VEPR_COD_VEN_PEL As String
    Private _VEPR_COD_PDV_PEL As String
    Private _VEPR_COD_PROD As String
    Private _DVPR_NU_SECU As String
    Private _DVPT_FLAG_PACK As String
    Private _DVPR_SERIE_CHIP As String
    Private _DVPR_COD_MATERIAL_CHIP As String
    Private _DVPR_SERIE_EQUI As String
    Private _DVPR_COD_MATERIAL_EQUI As String
    Private _DVPR_COD_CAMPANA As String  'FKART
    Private _DVPR_COD_LISTA_PRE As String
    Private _DVPR_COD_PROMOCION As String
    Private _DVPR_COD_PLAN As String
    Private _DVPR_COD_PROD_PREP As String
    Private _DVPR_LINEA As String
    Private _COD_ACTIVACION As String
    Private _DVPR_COD_TXN As String
    Private _DVPR_NUM_OPER As String
    Private _DVPR_IMSI As String
    Private _DVPR_TIPO_ACTI As String
    Private _DVPR_TIPO_ACTI_V2 As String
    Private _CAR1 As String
    Private _DESCRIPCION_PRODUCTO As String
    Private _DVPR_DEPAC_DESC As String 'ADD JDP 13-01-2015
    Private _DVPR_PROVV_DESC As String 'ADD JDP 13-01-2015
    Private _DVPR_DISTV_DESC As String 'ADD JDP 13-01-2015
    Private _DVPR_CPF_DESC As String   'ADD JDP 13-01-2015
    Private _VEPR_USUARIO As String    'ADD JDP 13-01-2015
    Public DetalleVentaPrepago() 'PROY-140126


    Public Property ID() As String
        Set(ByVal value As String)
            _VEPR_ID = value
        End Set
        Get
            Return _VEPR_ID
        End Get
    End Property

    Public Property TIPO_DOC() As String
        Set(ByVal value As String)
            _VEPR_TIPO_DOC = value
        End Set
        Get
            Return _VEPR_TIPO_DOC
        End Get
    End Property

    Public Property NUM_DOC() As String
        Set(ByVal value As String)
            _VEPR_NUM_DOC = value
        End Set
        Get
            Return _VEPR_NUM_DOC
        End Get
    End Property

    Public Property COD_CAN() As String
        Set(ByVal value As String)
            _VEPR_COD_CAN = value
        End Set
        Get
            Return _VEPR_COD_CAN
        End Get
    End Property

    Public Property COD_PDV() As String
        Set(ByVal value As String)
            _VEPR_COD_PDV = value
        End Set
        Get
            Return _VEPR_COD_PDV
        End Get
    End Property

    Public Property TIPO_VENT() As String
        Set(ByVal value As String)
            _VEPR_TIPO_VENT = value
        End Set
        Get
            Return _VEPR_TIPO_VENT
        End Get
    End Property

    Public Property TIPO_OPERACION() As String
        Set(ByVal value As String)
            _VEPR_TIPO_OPERACION = value
        End Set
        Get
            Return _VEPR_TIPO_OPERACION
        End Get
    End Property

    Public Property TIPO_PROD() As String
        Set(ByVal value As String)
            _VEPR_TIPO_PROD = value
        End Set
        Get
            Return _VEPR_TIPO_PROD
        End Get
    End Property

    Public Property COD_DEPA() As String
        Set(ByVal value As String)
            _VEPR_COD_DEPA = value
        End Set
        Get
            Return _VEPR_COD_DEPA
        End Get
    End Property

    Public Property COD_VEN_PEL() As String
        Set(ByVal value As String)
            _VEPR_COD_VEN_PEL = value
        End Set
        Get
            Return _VEPR_COD_VEN_PEL
        End Get
    End Property

    Public Property COD_PDV_PEL() As String
        Set(ByVal value As String)
            _VEPR_COD_PDV_PEL = value
        End Set
        Get
            Return _VEPR_COD_PDV_PEL
        End Get
    End Property

    Public Property COD_PROD() As String
        Set(ByVal value As String)
            _VEPR_COD_PROD = value
        End Set
        Get
            Return _VEPR_COD_PROD
        End Get
    End Property

    Public Property NU_SECU() As String
        Set(ByVal value As String)
            _DVPR_NU_SECU = value
        End Set
        Get
            Return _DVPR_NU_SECU
        End Get
    End Property

    Public Property FLAG_PACK() As String
        Set(ByVal value As String)
            _DVPT_FLAG_PACK = value
        End Set
        Get
            Return _DVPT_FLAG_PACK
        End Get
    End Property

    Public Property SERIE_CHIP() As String
        Set(ByVal value As String)
            _DVPR_SERIE_CHIP = value
        End Set
        Get
            Return _DVPR_SERIE_CHIP
        End Get
    End Property

    Public Property COD_MATERIAL_CHIP() As String
        Set(ByVal value As String)
            _DVPR_COD_MATERIAL_CHIP = value
        End Set
        Get
            Return _DVPR_COD_MATERIAL_CHIP
        End Get
    End Property

    Public Property SERIE_EQUI() As String
        Set(ByVal value As String)
            _DVPR_SERIE_EQUI = value
        End Set
        Get
            Return _DVPR_SERIE_EQUI
        End Get
    End Property

    Public Property COD_MATERIAL_EQUI() As String
        Set(ByVal value As String)
            _DVPR_COD_MATERIAL_EQUI = value
        End Set
        Get
            Return _DVPR_COD_MATERIAL_EQUI
        End Get
    End Property

    Public Property COD_CAMPANA() As String
        Set(ByVal value As String)
            _DVPR_COD_CAMPANA = value
        End Set
        Get
            Return _DVPR_COD_CAMPANA
        End Get
    End Property

    Public Property COD_LISTA_PRE() As String
        Set(ByVal value As String)
            _DVPR_COD_LISTA_PRE = value
        End Set
        Get
            Return _DVPR_COD_LISTA_PRE
        End Get
    End Property

    Public Property COD_PROMOCION() As String
        Set(ByVal value As String)
            _DVPR_COD_PROMOCION = value
        End Set
        Get
            Return _DVPR_COD_PROMOCION
        End Get
    End Property

    Public Property COD_PLAN() As String
        Set(ByVal value As String)
            _DVPR_COD_PLAN = value
        End Set
        Get
            Return _DVPR_COD_PLAN
        End Get
    End Property

    Public Property COD_PROD_PREP() As String
        Set(ByVal value As String)
            _DVPR_COD_PROD_PREP = value
        End Set
        Get
            Return _DVPR_COD_PROD_PREP
        End Get
    End Property

    Public Property LINEA() As String
        Set(ByVal value As String)
            _DVPR_LINEA = value
        End Set
        Get
            Return _DVPR_LINEA
        End Get

    End Property


    Public Property COD_ACTIVACION() As String
        Set(ByVal value As String)
            _COD_ACTIVACION = value
        End Set
        Get
            Return _COD_ACTIVACION
        End Get
    End Property

    Public Property COD_TXN() As String
        Set(ByVal value As String)
            _DVPR_COD_TXN = value
        End Set
        Get
            Return _DVPR_COD_TXN
        End Get
    End Property

    Public Property NUM_OPER() As String
        Set(ByVal value As String)
            _DVPR_NUM_OPER = value
        End Set
        Get
            Return _DVPR_NUM_OPER
        End Get
    End Property

    Public Property IMSI() As String
        Set(ByVal value As String)
            _DVPR_IMSI = value
        End Set
        Get
            Return _DVPR_IMSI
        End Get
    End Property


    Public Property TIPO_ACTI() As String
        Set(ByVal value As String)
            _DVPR_TIPO_ACTI = value
        End Set
        Get
            Return _DVPR_TIPO_ACTI
        End Get
    End Property

    Public Property TIPO_ACTI_V2() As String
        Set(ByVal value As String)
            _DVPR_TIPO_ACTI_V2 = value
        End Set
        Get
            Return _DVPR_TIPO_ACTI_V2
        End Get
    End Property

    Public Property DESCRIPCION_PRODUCTO() As String
        Set(ByVal value As String)
            _DESCRIPCION_PRODUCTO = value
        End Set
        Get
            Return _DESCRIPCION_PRODUCTO
        End Get
    End Property

    Public Property CAR1() As String
        Set(ByVal value As String)
            _CAR1 = value
        End Set
        Get
            Return _CAR1
        End Get
    End Property

    'INI ADD JDP 13-01-2015
    Public Property DEPAC_DESC() As String

        Set(ByVal value As String)
            _DVPR_DEPAC_DESC = value
        End Set
        Get
            Return _DVPR_DEPAC_DESC
        End Get
    End Property
    Public Property PROVV_DESC() As String

        Set(ByVal value As String)
            _DVPR_PROVV_DESC = value
        End Set
        Get
            Return _DVPR_PROVV_DESC
        End Get
    End Property

    Public Property DISTV_DESC() As String

        Set(ByVal value As String)
            _DVPR_DISTV_DESC = value
        End Set
        Get
            Return _DVPR_DISTV_DESC
        End Get
    End Property

    Public Property CPF_DESC() As String

        Set(ByVal value As String)
            _DVPR_CPF_DESC = value
        End Set
        Get
            Return _DVPR_CPF_DESC
        End Get
    End Property
    Public Property USUARIO() As String

        Set(ByVal value As String)
            _VEPR_USUARIO = value
        End Set
        Get
            Return _VEPR_USUARIO
        End Get
    End Property

    'FIN ADD JDP 13-01-2015
End Class
