Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class PlantillaInteraccion

    Private _NOMBRE_TRANSACCION As String
    Private _X_NRO_INTERACCION As String
    Private _X_INTER_1 As String
    Private _X_INTER_2 As String
    Private _X_INTER_3 As String
    Private _X_INTER_4 As String
    Private _X_INTER_5 As String
    Private _X_INTER_6 As String
    Private _X_INTER_7 As String
    Private _X_INTER_8 As Double
    Private _X_INTER_9 As Double
    Private _X_INTER_10 As Double
    Private _X_INTER_11 As Double
    Private _X_INTER_12 As Double
    Private _X_INTER_13 As Double
    Private _X_INTER_14 As Double
    Private _X_INTER_15 As String
    Private _X_INTER_16 As String
    Private _X_INTER_17 As String
    Private _X_INTER_18 As String
    Private _X_INTER_19 As String
    Private _X_INTER_20 As String
    Private _X_INTER_21 As String
    Private _X_INTER_22 As Double
    Private _X_INTER_23 As Double
    Private _X_INTER_24 As Double
    Private _X_INTER_25 As Double
    Private _X_INTER_26 As Double
    Private _X_INTER_27 As Double
    Private _X_INTER_28 As Double
    Private _X_INTER_29 As String
    Private _X_INTER_30 As String
    Private _X_PLUS_INTER2INTERACT As Double
    Private _X_ADJUSTMENT_AMOUNT As Double
    Private _X_ADJUSTMENT_REASON As String
    Private _X_ADDRESS As String
    Private _X_AMOUNT_UNIT As String
    Private _X_BIRTHDAY As DateTime
    Private _X_CLARIFY_INTERACTION As String
    Private _X_CLARO_LDN1 As String
    Private _X_CLARO_LDN2 As String
    Private _X_CLARO_LDN3 As String
    Private _X_CLARO_LDN4 As String
    Private _X_CLAROLOCAL1 As String
    Private _X_CLAROLOCAL2 As String
    Private _X_CLAROLOCAL3 As String
    Private _X_CLAROLOCAL4 As String
    Private _X_CLAROLOCAL5 As String
    Private _X_CLAROLOCAL6 As String
    Private _X_CONTACT_PHONE As String
    Private _X_DNI_LEGAL_REP As String
    Private _X_DOCUMENT_NUMBER As String
    Private _X_EMAIL As String
    Private _X_FIRST_NAME As String
    Private _X_FIXED_NUMBER As String
    Private _X_FLAG_CHANGE_USER As String
    Private _X_FLAG_LEGAL_REP As String
    Private _X_FLAG_OTHER As String
    Private _X_FLAG_TITULAR As String
    Private _X_IMEI As String
    Private _X_LAST_NAME As String
    Private _X_LASTNAME_REP As String
    Private _X_LDI_NUMBER As String
    Private _X_NAME_LEGAL_REP As String
    Private _X_OLD_CLARO_LDN1 As String
    Private _X_OLD_CLARO_LDN2 As String
    Private _X_OLD_CLARO_LDN3 As String
    Private _X_OLD_CLARO_LDN4 As String
    Private _X_OLD_CLAROLOCAL1 As String
    Private _X_OLD_CLAROLOCAL2 As String
    Private _X_OLD_CLAROLOCAL3 As String
    Private _X_OLD_CLAROLOCAL4 As String
    Private _X_OLD_CLAROLOCAL5 As String
    Private _X_OLD_CLAROLOCAL6 As String
    Private _X_OLD_DOC_NUMBER As String
    Private _X_OLD_FIRST_NAME As String
    Private _X_OLD_FIXED_PHONE As String
    Private _X_OLD_LAST_NAME As String
    Private _X_OLD_LDI_NUMBER As String
    Private _X_OLD_FIXED_NUMBER As String
    Private _X_OPERATION_TYPE As String
    Private _X_OTHER_DOC_NUMBER As String
    Private _X_OTHER_FIRST_NAME As String
    Private _X_OTHER_LAST_NAME As String
    Private _X_OTHER_PHONE As String
    Private _X_PHONE_LEGAL_REP As String
    Private _X_REFERENCE_PHONE As String
    Private _X_REASON As String
    Private _X_MODEL As String
    Private _X_LOT_CODE As String
    Private _X_FLAG_REGISTERED As String
    Private _X_REGISTRATION_REASON As String
    Private _X_CLARO_NUMBER As String
    Private _X_MONTH As String
    Private _X_OST_NUMBER As String
    Private _X_BASKET As String
    Private _X_RECHARGE_DATE As DateTime
    Private _X_EXPIRE_DATE As DateTime
    Private _ID_INTERACCION As String
    Public _Script_IMEI As String
    Public _SESSION_ID As String
    Private _MSISDN As String
    Private _P_OLD_ID_CONTACTO As String
    Private _P_USUARIO_ID As String
    Private _TIENE_DATOS As String
    Private _TICKET As String
    Private _ACCION As String
    Private _NRO_DIAS As Integer
    Private _NRO_TELEFONO As String
    Private _NRO_TARJETA As String
    Public _X_ADDRESS5 As String
    Public _X_CHARGE_AMOUNT As Double
    Public _X_CITY As String
    Public _X_CONTACT_SEX As String
    Public _X_DEPARTMENT As String
    Public _X_DISTRICT As String
    Public _X_EMAIL_CONFIRMATION As String
    Public _X_FAX As String
    Public _X_FLAG_CHARGE As String
    Public _X_MARITAL_STATUS As String
    Public _X_OCCUPATION As String
    Public _X_POSITION As String
    Public _X_REFERENCE_ADDRESS As String
    Public _X_TYPE_DOCUMENT As String
    Public _X_ZIPCODE As String
    Public _X_ICCID As String
    Public _NRO_INTENTOS As Integer
    Public _ES_TFI As String
    Public _SUBSCRIBER_STATUS As String
    Public _PROMOBB As String
    Public PlantillaInteraccion() 'PROY-140126

    Public Sub New()
    End Sub

    Public Property X_NRO_INTERACCION() As String
        Get
            Return _X_NRO_INTERACCION
        End Get
        Set(ByVal value As String)
            _X_NRO_INTERACCION = value
        End Set

    End Property
    Public Property X_INTER_1() As String
        Get
            Return _X_INTER_1
        End Get
        Set(ByVal value As String)
            _X_INTER_1 = value
        End Set

    End Property
    Public Property X_INTER_2() As String
        Get
            Return _X_INTER_2
        End Get
        Set(ByVal value As String)
            _X_INTER_2 = value
        End Set

    End Property
    Public Property X_INTER_3() As String
        Get
            Return _X_INTER_3
        End Get
        Set(ByVal value As String)
            _X_INTER_3 = value
        End Set

    End Property
    Public Property X_INTER_4() As String
        Get
            Return _X_INTER_4
        End Get
        Set(ByVal value As String)
            _X_INTER_4 = value
        End Set

    End Property
    Public Property X_INTER_5() As String
        Get
            Return _X_INTER_5
        End Get
        Set(ByVal value As String)
            _X_INTER_5 = value
        End Set

    End Property
    Public Property X_INTER_6() As String
        Get
            Return _X_INTER_6
        End Get
        Set(ByVal value As String)
            _X_INTER_6 = value
        End Set

    End Property
    Public Property X_INTER_7() As String
        Get
            Return _X_INTER_7
        End Get
        Set(ByVal value As String)
            _X_INTER_7 = value
        End Set

    End Property
    Public Property X_INTER_8() As Double
        Get
            Return _X_INTER_8
        End Get
        Set(ByVal value As Double)
            _X_INTER_8 = value
        End Set

    End Property
    Public Property X_INTER_9() As Double
        Get
            Return _X_INTER_9
        End Get
        Set(ByVal value As Double)
            _X_INTER_9 = value
        End Set

    End Property
    Public Property X_INTER_10() As Double
        Get
            Return _X_INTER_10
        End Get
        Set(ByVal value As Double)
            _X_INTER_10 = value
        End Set

    End Property
    Public Property X_INTER_11() As Double
        Get
            Return _X_INTER_11
        End Get
        Set(ByVal value As Double)
            _X_INTER_11 = value
        End Set

    End Property
    Public Property X_INTER_12() As Double
        Get
            Return _X_INTER_12
        End Get
        Set(ByVal value As Double)
            _X_INTER_12 = value
        End Set

    End Property
    Public Property X_INTER_13() As Double
        Get
            Return _X_INTER_13
        End Get
        Set(ByVal value As Double)
            _X_INTER_13 = value
        End Set

    End Property
    Public Property X_INTER_14() As Double
        Get
            Return _X_INTER_14
        End Get
        Set(ByVal value As Double)
            _X_INTER_14 = value
        End Set

    End Property
    Public Property X_INTER_15() As String
        Get
            Return _X_INTER_15
        End Get
        Set(ByVal value As String)
            _X_INTER_15 = value
        End Set

    End Property
    Public Property X_INTER_16() As String
        Get
            Return _X_INTER_16
        End Get
        Set(ByVal value As String)
            _X_INTER_16 = value
        End Set

    End Property
    Public Property X_INTER_17() As String
        Get
            Return _X_INTER_17
        End Get
        Set(ByVal value As String)
            _X_INTER_17 = value
        End Set

    End Property
    Public Property X_INTER_18() As String
        Get
            Return _X_INTER_18
        End Get
        Set(ByVal value As String)
            _X_INTER_18 = value
        End Set

    End Property
    Public Property X_INTER_19() As String
        Get
            Return _X_INTER_19
        End Get
        Set(ByVal value As String)
            _X_INTER_19 = value
        End Set

    End Property
    Public Property X_INTER_20() As String
        Get
            Return _X_INTER_20
        End Get
        Set(ByVal value As String)
            _X_INTER_20 = value
        End Set

    End Property
    Public Property X_INTER_21() As String
        Get
            Return _X_INTER_21
        End Get
        Set(ByVal value As String)
            _X_INTER_21 = value
        End Set

    End Property
    Public Property X_INTER_22() As Double
        Get
            Return _X_INTER_22
        End Get
        Set(ByVal value As Double)
            _X_INTER_22 = value
        End Set

    End Property
    Public Property X_INTER_23() As Double
        Get
            Return _X_INTER_23
        End Get
        Set(ByVal value As Double)
            _X_INTER_23 = value
        End Set

    End Property
    Public Property X_INTER_24() As Double
        Get
            Return _X_INTER_24
        End Get
        Set(ByVal value As Double)
            _X_INTER_24 = value
        End Set

    End Property
    Public Property X_INTER_25() As Double
        Get
            Return _X_INTER_25
        End Get
        Set(ByVal value As Double)
            _X_INTER_25 = value
        End Set

    End Property
    Public Property X_INTER_26() As Double
        Get
            Return _X_INTER_26
        End Get
        Set(ByVal value As Double)
            _X_INTER_26 = value
        End Set

    End Property
    Public Property X_INTER_27() As Double
        Get
            Return _X_INTER_27
        End Get
        Set(ByVal value As Double)
            _X_INTER_27 = value
        End Set

    End Property
    Public Property X_INTER_28() As Double
        Get
            Return _X_INTER_28
        End Get
        Set(ByVal value As Double)
            _X_INTER_28 = value
        End Set

    End Property
    Public Property X_INTER_29() As String
        Get
            Return _X_INTER_29
        End Get
        Set(ByVal value As String)
            _X_INTER_29 = value
        End Set

    End Property
    Public Property X_INTER_30() As String
        Get
            Return _X_INTER_30
        End Get
        Set(ByVal value As String)
            _X_INTER_30 = value
        End Set

    End Property
    Public Property X_PLUS_INTER2INTERACT() As Double
        Get
            Return _X_PLUS_INTER2INTERACT
        End Get
        Set(ByVal value As Double)
            _X_PLUS_INTER2INTERACT = value
        End Set

    End Property
    Public Property X_ADJUSTMENT_AMOUNT() As Double
        Get
            Return _X_ADJUSTMENT_AMOUNT
        End Get
        Set(ByVal value As Double)
            _X_ADJUSTMENT_AMOUNT = value
        End Set

    End Property
    Public Property X_ADJUSTMENT_REASON() As String
        Get
            Return _X_ADJUSTMENT_REASON
        End Get
        Set(ByVal value As String)
            _X_ADJUSTMENT_REASON = value
        End Set

    End Property
    Public Property X_ADDRESS() As String
        Get
            Return _X_ADDRESS
        End Get
        Set(ByVal value As String)
            _X_ADDRESS = value
        End Set

    End Property
    Public Property X_AMOUNT_UNIT() As String
        Get
            Return _X_AMOUNT_UNIT
        End Get
        Set(ByVal value As String)
            _X_AMOUNT_UNIT = value
        End Set

    End Property
    Public Property X_BIRTHDAY() As DateTime
        Get
            Return _X_BIRTHDAY
        End Get
        Set(ByVal value As DateTime)
            _X_BIRTHDAY = value
        End Set

    End Property
    Public Property X_CLARIFY_INTERACTION() As String
        Get
            Return _X_CLARIFY_INTERACTION
        End Get
        Set(ByVal value As String)
            _X_CLARIFY_INTERACTION = value
        End Set

    End Property
    Public Property X_CLARO_LDN1() As String
        Get
            Return _X_CLARO_LDN1
        End Get
        Set(ByVal value As String)
            _X_CLARO_LDN1 = value
        End Set

    End Property
    Public Property X_CLARO_LDN2() As String
        Get
            Return _X_CLARO_LDN2
        End Get
        Set(ByVal value As String)
            _X_CLARO_LDN2 = value
        End Set

    End Property
    Public Property X_CLARO_LDN3() As String
        Get
            Return _X_CLARO_LDN3
        End Get
        Set(ByVal value As String)
            _X_CLARO_LDN3 = value
        End Set

    End Property
    Public Property X_CLARO_LDN4() As String
        Get
            Return _X_CLARO_LDN4
        End Get
        Set(ByVal value As String)
            _X_CLARO_LDN4 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL1() As String
        Get
            Return _X_CLAROLOCAL1
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL1 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL2() As String
        Get
            Return _X_CLAROLOCAL2
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL2 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL3() As String
        Get
            Return _X_CLAROLOCAL3
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL3 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL4() As String
        Get
            Return _X_CLAROLOCAL4
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL4 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL5() As String
        Get
            Return _X_CLAROLOCAL5
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL5 = value
        End Set

    End Property
    Public Property X_CLAROLOCAL6() As String
        Get
            Return _X_CLAROLOCAL6
        End Get
        Set(ByVal value As String)
            _X_CLAROLOCAL6 = value
        End Set

    End Property
    Public Property X_CONTACT_PHONE() As String
        Get
            Return _X_CONTACT_PHONE
        End Get
        Set(ByVal value As String)
            _X_CONTACT_PHONE = value
        End Set

    End Property
    Public Property X_DNI_LEGAL_REP() As String
        Get
            Return _X_DNI_LEGAL_REP
        End Get
        Set(ByVal value As String)
            _X_DNI_LEGAL_REP = value
        End Set

    End Property
    Public Property X_DOCUMENT_NUMBER() As String
        Get
            Return _X_DOCUMENT_NUMBER
        End Get
        Set(ByVal value As String)
            _X_DOCUMENT_NUMBER = value
        End Set

    End Property
    Public Property X_EMAIL() As String
        Get
            Return _X_EMAIL
        End Get
        Set(ByVal value As String)
            _X_EMAIL = value
        End Set

    End Property
    Public Property X_FIRST_NAME() As String
        Get
            Return _X_FIRST_NAME
        End Get
        Set(ByVal value As String)
            _X_FIRST_NAME = value
        End Set

    End Property
    Public Property X_FIXED_NUMBER() As String
        Get
            Return _X_FIXED_NUMBER
        End Get
        Set(ByVal value As String)
            _X_FIXED_NUMBER = value
        End Set

    End Property
    Public Property X_FLAG_CHANGE_USER() As String
        Get
            Return _X_FLAG_CHANGE_USER
        End Get
        Set(ByVal value As String)
            _X_FLAG_CHANGE_USER = value
        End Set

    End Property
    Public Property X_FLAG_LEGAL_REP() As String
        Get
            Return _X_FLAG_LEGAL_REP
        End Get
        Set(ByVal value As String)
            _X_FLAG_LEGAL_REP = value
        End Set

    End Property
    Public Property X_FLAG_OTHER() As String
        Get
            Return _X_FLAG_OTHER
        End Get
        Set(ByVal value As String)
            _X_FLAG_OTHER = value
        End Set

    End Property
    Public Property X_FLAG_TITULAR() As String
        Get
            Return _X_FLAG_TITULAR
        End Get
        Set(ByVal value As String)
            _X_FLAG_TITULAR = value
        End Set

    End Property
    Public Property X_IMEI() As String
        Get
            Return _X_IMEI
        End Get
        Set(ByVal value As String)
            _X_IMEI = value
        End Set

    End Property
    Public Property Script_IMEI() As String
        Get
            Return _Script_IMEI
        End Get
        Set(ByVal value As String)
            _Script_IMEI = value
        End Set

    End Property
    Public Property SESSION_ID() As String
        Get
            Return _SESSION_ID
        End Get
        Set(ByVal value As String)
            _SESSION_ID = value
        End Set

    End Property

    Public Property X_LAST_NAME() As String
        Get
            Return _X_LAST_NAME
        End Get
        Set(ByVal value As String)
            _X_LAST_NAME = value
        End Set

    End Property
    Public Property X_LASTNAME_REP() As String
        Get
            Return _X_LASTNAME_REP
        End Get
        Set(ByVal value As String)
            _X_LASTNAME_REP = value
        End Set

    End Property
    Public Property X_LDI_NUMBER() As String
        Get
            Return _X_LDI_NUMBER
        End Get
        Set(ByVal value As String)
            _X_LDI_NUMBER = value
        End Set

    End Property
    Public Property X_NAME_LEGAL_REP() As String
        Get
            Return _X_NAME_LEGAL_REP
        End Get
        Set(ByVal value As String)
            _X_NAME_LEGAL_REP = value
        End Set

    End Property
    Public Property X_OLD_CLARO_LDN1() As String
        Get
            Return _X_OLD_CLARO_LDN1
        End Get
        Set(ByVal value As String)
            _X_OLD_CLARO_LDN1 = value
        End Set

    End Property
    Public Property X_OLD_CLARO_LDN2() As String
        Get
            Return _X_OLD_CLARO_LDN2
        End Get
        Set(ByVal value As String)
            _X_OLD_CLARO_LDN2 = value
        End Set

    End Property
    Public Property X_OLD_CLARO_LDN3() As String
        Get
            Return _X_OLD_CLARO_LDN3
        End Get
        Set(ByVal value As String)
            _X_OLD_CLARO_LDN3 = value
        End Set

    End Property
    Public Property X_OLD_CLARO_LDN4() As String
        Get
            Return _X_OLD_CLARO_LDN4
        End Get
        Set(ByVal value As String)
            _X_OLD_CLARO_LDN4 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL1() As String
        Get
            Return _X_OLD_CLAROLOCAL1
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL1 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL2() As String
        Get
            Return _X_OLD_CLAROLOCAL2
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL2 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL3() As String
        Get
            Return _X_OLD_CLAROLOCAL3
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL3 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL4() As String
        Get
            Return _X_OLD_CLAROLOCAL4
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL4 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL5() As String
        Get
            Return _X_OLD_CLAROLOCAL5
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL5 = value
        End Set

    End Property
    Public Property X_OLD_CLAROLOCAL6() As String
        Get
            Return _X_OLD_CLAROLOCAL6
        End Get
        Set(ByVal value As String)
            _X_OLD_CLAROLOCAL6 = value
        End Set

    End Property
    Public Property X_OLD_DOC_NUMBER() As String
        Get
            Return _X_OLD_DOC_NUMBER
        End Get
        Set(ByVal value As String)
            _X_OLD_DOC_NUMBER = value
        End Set

    End Property
    Public Property X_OLD_FIRST_NAME() As String
        Get
            Return _X_OLD_FIRST_NAME
        End Get
        Set(ByVal value As String)
            _X_OLD_FIRST_NAME = value
        End Set

    End Property
    Public Property X_OLD_FIXED_PHONE() As String
        Get
            Return _X_OLD_FIXED_PHONE
        End Get
        Set(ByVal value As String)
            _X_OLD_FIXED_PHONE = value
        End Set

    End Property
    Public Property X_OLD_LAST_NAME() As String
        Get
            Return _X_OLD_LAST_NAME
        End Get
        Set(ByVal value As String)
            _X_OLD_LAST_NAME = value
        End Set

    End Property
    Public Property X_OLD_LDI_NUMBER() As String
        Get
            Return _X_OLD_LDI_NUMBER
        End Get
        Set(ByVal value As String)
            _X_OLD_LDI_NUMBER = value
        End Set

    End Property
    Public Property X_OLD_FIXED_NUMBER() As String
        Get
            Return _X_OLD_FIXED_NUMBER
        End Get
        Set(ByVal value As String)
            _X_OLD_FIXED_NUMBER = value
        End Set

    End Property
    Public Property X_OPERATION_TYPE() As String
        Get
            Return _X_OPERATION_TYPE
        End Get
        Set(ByVal value As String)
            _X_OPERATION_TYPE = value
        End Set

    End Property
    Public Property X_OTHER_DOC_NUMBER() As String
        Get
            Return _X_OTHER_DOC_NUMBER
        End Get
        Set(ByVal value As String)
            _X_OTHER_DOC_NUMBER = value
        End Set

    End Property
    Public Property X_OTHER_FIRST_NAME() As String
        Get
            Return _X_OTHER_FIRST_NAME
        End Get
        Set(ByVal value As String)
            _X_OTHER_FIRST_NAME = value
        End Set

    End Property
    Public Property X_OTHER_LAST_NAME() As String
        Get
            Return _X_OTHER_LAST_NAME
        End Get
        Set(ByVal value As String)
            _X_OTHER_LAST_NAME = value
        End Set

    End Property
    Public Property X_OTHER_PHONE() As String
        Get
            Return _X_OTHER_PHONE
        End Get
        Set(ByVal value As String)
            _X_OTHER_PHONE = value
        End Set

    End Property
    Public Property X_PHONE_LEGAL_REP() As String
        Get
            Return _X_PHONE_LEGAL_REP
        End Get
        Set(ByVal value As String)
            _X_PHONE_LEGAL_REP = value
        End Set

    End Property
    Public Property X_REFERENCE_PHONE() As String
        Get
            Return _X_REFERENCE_PHONE
        End Get
        Set(ByVal value As String)
            _X_REFERENCE_PHONE = value
        End Set

    End Property
    Public Property X_REASON() As String
        Get
            Return _X_REASON
        End Get
        Set(ByVal value As String)
            _X_REASON = value
        End Set

    End Property
    Public Property X_MODEL() As String
        Get
            Return _X_MODEL
        End Get
        Set(ByVal value As String)
            _X_MODEL = value
        End Set

    End Property
    Public Property X_LOT_CODE() As String
        Get
            Return _X_LOT_CODE
        End Get
        Set(ByVal value As String)
            _X_LOT_CODE = value
        End Set

    End Property
    Public Property X_FLAG_REGISTERED() As String
        Get
            Return _X_FLAG_REGISTERED
        End Get
        Set(ByVal value As String)
            _X_FLAG_REGISTERED = value
        End Set

    End Property
    Public Property X_REGISTRATION_REASON() As String
        Get
            Return _X_REGISTRATION_REASON
        End Get
        Set(ByVal value As String)
            _X_REGISTRATION_REASON = value
        End Set

    End Property
    Public Property X_CLARO_NUMBER() As String
        Get
            Return _X_CLARO_NUMBER
        End Get
        Set(ByVal value As String)
            _X_CLARO_NUMBER = value
        End Set

    End Property
    Public Property X_MONTH() As String
        Get
            Return _X_MONTH
        End Get
        Set(ByVal value As String)
            _X_MONTH = value
        End Set

    End Property
    Public Property X_OST_NUMBER() As String
        Get
            Return _X_OST_NUMBER
        End Get
        Set(ByVal value As String)
            _X_OST_NUMBER = value
        End Set

    End Property
    Public Property X_BASKET() As String
        Get
            Return _X_BASKET
        End Get
        Set(ByVal value As String)
            _X_BASKET = value
        End Set

    End Property

    Public Property X_EXPIRE_DATE() As DateTime
        Get
            Return _X_EXPIRE_DATE
        End Get
        Set(ByVal value As DateTime)
            _X_EXPIRE_DATE = value
        End Set

    End Property
    Public Property ID_INTERACCION() As String
        Get
            Return _ID_INTERACCION
        End Get
        Set(ByVal value As String)
            _ID_INTERACCION = value
        End Set

    End Property

    Public Property NOMBRE_TRANSACCION() As String
        Set(ByVal value As String)
            _NOMBRE_TRANSACCION = value
        End Set
        Get
            Return _NOMBRE_TRANSACCION
        End Get

    End Property

    Public Property MSISDN() As String
        Get
            Return _MSISDN
        End Get
        Set(ByVal value As String)
            _MSISDN = value
        End Set

    End Property
    Public Property X_RECHARGE_DATE() As DateTime
        Get
            Return _X_RECHARGE_DATE
        End Get
        Set(ByVal value As DateTime)
            _X_RECHARGE_DATE = value
        End Set

    End Property

    Public Property TICKET() As String
        Get
            Return _TICKET
        End Get
        Set(ByVal value As String)
            _TICKET = value
        End Set

    End Property
    Public Property ACCION() As String
        Get
            Return _ACCION
        End Get
        Set(ByVal value As String)
            _ACCION = value
        End Set

    End Property

    Public Property NRO_DIAS() As Integer
        Get
            Return _NRO_DIAS
        End Get
        Set(ByVal value As Integer)
            _NRO_DIAS = value
        End Set

    End Property
    Public Property NRO_TELEFONO() As String
        Get
            Return _NRO_TELEFONO
        End Get
        Set(ByVal value As String)
            _NRO_TELEFONO = value
        End Set

    End Property
    Public Property NRO_TARJETA() As String
        Get
            Return _NRO_TARJETA
        End Get
        Set(ByVal value As String)
            _NRO_TARJETA = value
        End Set

    End Property
    Public Property X_ADDRESS5() As String
        Get
            Return _X_ADDRESS5
        End Get
        Set(ByVal value As String)
            _X_ADDRESS5 = value
        End Set

    End Property
    Public Property X_CHARGE_AMOUNT() As Double
        Get
            Return _X_CHARGE_AMOUNT
        End Get
        Set(ByVal value As Double)
            _X_CHARGE_AMOUNT = value
        End Set

    End Property
    Public Property X_CITY() As String
        Get
            Return _X_CITY
        End Get
        Set(ByVal value As String)
            _X_CITY = value
        End Set

    End Property
    Public Property X_CONTACT_SEX() As String
        Get
            Return _X_CONTACT_SEX
        End Get
        Set(ByVal value As String)
            _X_CONTACT_SEX = value
        End Set

    End Property
    Public Property X_DEPARTMENT() As String
        Get
            Return _X_DEPARTMENT
        End Get
        Set(ByVal value As String)
            _X_DEPARTMENT = value
        End Set

    End Property
    Public Property X_DISTRICT() As String
        Get
            Return _X_DISTRICT
        End Get
        Set(ByVal value As String)
            _X_DISTRICT = value
        End Set

    End Property
    Public Property X_EMAIL_CONFIRMATION() As String
        Get
            Return _X_EMAIL_CONFIRMATION
        End Get
        Set(ByVal value As String)
            _X_EMAIL_CONFIRMATION = value
        End Set

    End Property
    Public Property X_FAX() As String
        Get
            Return _X_FAX
        End Get
        Set(ByVal value As String)
            _X_FAX = value
        End Set

    End Property
    Public Property X_FLAG_CHARGE() As String
        Get
            Return _X_FLAG_CHARGE
        End Get
        Set(ByVal value As String)
            _X_FLAG_CHARGE = value
        End Set

    End Property
    Public Property X_MARITAL_STATUS() As String
        Get
            Return _X_MARITAL_STATUS
        End Get
        Set(ByVal value As String)
            _X_MARITAL_STATUS = value
        End Set

    End Property
    Public Property X_OCCUPATION() As String
        Get
            Return _X_OCCUPATION
        End Get
        Set(ByVal value As String)
            _X_OCCUPATION = value
        End Set

    End Property
    Public Property X_POSITION() As String
        Get
            Return _X_POSITION
        End Get
        Set(ByVal value As String)
            _X_POSITION = value
        End Set

    End Property
    Public Property X_REFERENCE_ADDRESS() As String
        Get
            Return _X_REFERENCE_ADDRESS
        End Get
        Set(ByVal value As String)
            _X_REFERENCE_ADDRESS = value
        End Set

    End Property
    Public Property X_TYPE_DOCUMENT() As String
        Get
            Return _X_TYPE_DOCUMENT
        End Get
        Set(ByVal value As String)
            _X_TYPE_DOCUMENT = value
        End Set

    End Property
    Public Property X_ZIPCODE() As String
        Get
            Return _X_ZIPCODE
        End Get
        Set(ByVal value As String)
            _X_ZIPCODE = value
        End Set

    End Property
    Public Property P_OLD_ID_CONTACTO() As String
        Get
            Return _P_OLD_ID_CONTACTO
        End Get
        Set(ByVal value As String)
            _P_OLD_ID_CONTACTO = value
        End Set

    End Property
    Public Property P_USUARIO_ID() As String
        Get
            Return _P_USUARIO_ID
        End Get
        Set(ByVal value As String)
            _P_USUARIO_ID = value
        End Set

    End Property
    Public Property X_ICCID() As String
        Get
            Return _X_ICCID
        End Get
        Set(ByVal value As String)
            _X_ICCID = value
        End Set

    End Property
    Public Property TIENE_DATOS() As String
        Get
            Return _TIENE_DATOS
        End Get
        Set(ByVal value As String)
            _TIENE_DATOS = value
        End Set

    End Property
    Public Property NRO_INTENTOS() As Integer
        Get
            Return _NRO_INTENTOS
        End Get
        Set(ByVal value As Integer)
            _NRO_INTENTOS = value
        End Set

    End Property

    Public Property ES_TFI() As String
        Get
            Return _ES_TFI
        End Get
        Set(ByVal value As String)
            _ES_TFI = value
        End Set

    End Property
    Public Property SUBSCRIBER_STATUS() As String
        Get
            Return _SUBSCRIBER_STATUS
        End Get
        Set(ByVal value As String)
            _SUBSCRIBER_STATUS = value
        End Set

    End Property
    Public Property PROMOBB() As String
        Get
            Return _PROMOBB
        End Get
        Set(ByVal value As String)
            _PROMOBB = value
        End Set

    End Property

End Class
