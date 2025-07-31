'PROY-31850 - INICIO
Imports System
Imports System.Configuration
Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class clsKeyOLO

    Private Shared _strConstCodProdOLO As String
    Private Shared _strConstCodActOLO As String

    Private Shared _strMobileNumberWS_Channel As String
    Private Shared _strMobileNumberWS_AdditionalNode As String
    Private Shared _strMobileNumberWS_UserName As String
    Private Shared _strMobileNumberWS_Password As String
    Private Shared _strMobileNumberWS_AgentID As String
    Private Shared _strMobileNumberWS_GroupID As String
    Private Shared _strMobileNumberWS_State As String
    Private Shared _strMobileNumberWS_PageKey As String
    Private Shared _strMobileNumberWS_Limit As String
    Private Shared _strMobileNumberWS_IsAscending As String
    Private Shared _strMobileNumberWS_PaIDType As String
    Private Shared _strMobileNumberWS_NumIntentos As String
    Private Shared _strMobileNumberWS_attributeName As String
    Private Shared _strMobileNumberWS_attributeValue As String

    Private Shared _strGenericEntityWS_NumIntentos As String
    Private Shared _strGenericEntityWS_Channel As String
    Private Shared _strGenericEntityWS_AdditionalNode As String
    Private Shared _strGenericEntityWS_UserName As String
    Private Shared _strGenericEntityWS_Password As String
    Private Shared _strGenericEntityWS_TransactionID As String
    Private Shared _strGenericEntityWS_AgentID As String
    Private Shared _strGenericEntityWS_Type As String
    Private Shared _strGenericEntityWS_attributeName As String
    Private Shared _strGenericEntityWS_attributeValue As String
    Private Shared _strGenericEntityWS_ReserveDays As String
    Private Shared _strGenericEntityWS_SpID As String
    Private Shared _strGenericEntityWS_Reserve_SicarOLO As String
    Private Shared _strGenericEntityWS_statusCode_OLO As String
    Private Shared _strGenericEntityWS_statusMessage_OLO As String
    Private Shared _strVentaErrorObtenerLineas As String
    Private Shared _strVentaErrorReservarLinea As String
    Private Shared _strVentaCorreoSicar_Remitente As String
    Private Shared _strVentaCorreoSicar_CC As String
    Private Shared _strVentaCorreoSicar_ParaDefault As String
    Private Shared _strVentaCorreoSicar_Asunto As String
    Private Shared _strVentaCorreoSicar_Cuerpo As String
    Private Shared _strNroDigitosLinea As String
    Private Shared _strFlagCorreo As String

    Private Shared _strVentaActivacionWS_NumIntentos As String
    Private Shared _strVentaActivacionWS_MsgID As String

    Private Shared strParamGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodigoGrupoVentaOLO"))
    Private Shared bolParamPOS As Boolean

    'PROY-31850 FASE IV - INICIO
    Private Shared _strRecargaOloWSCanal As String
    Private Shared _strRecargaOloWS_AdditionalNode As String
    Private Shared _strRecargaOloWS_UserName As String
    Private Shared _strRecargaOloWS_Password As String
    Private Shared _strRecargaOloWS_Campo As String
    Private Shared _strRecargaOloWS_Valor As String
    Private Shared _strRecupera_Valor_CodMaterial As String
    Private Shared _strRecargaOloWS_gpcanal As String
    Private Shared _strRecargaOloWS_version As String
    Private Shared _strRecargaOloWS_compania As String
    Public clsKeyOLO() 'PROY-140126

    'PROY-31850 FASE IV - FIN

   
    Public Shared Property strConstCodProdOLO() As String
        Set(ByVal value As String)
            _strConstCodProdOLO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strConstCodProdOLO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strConstCodProdOLO
            End If
        End Get
    End Property

    Public Shared Property strConstCodActOLO() As String
        Set(ByVal value As String)
            _strConstCodActOLO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strConstCodActOLO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strConstCodActOLO
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_Channel() As String
        Set(ByVal value As String)
            _strMobileNumberWS_Channel = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_Channel
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_Channel
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_AdditionalNode() As String
        Set(ByVal value As String)
            _strMobileNumberWS_AdditionalNode = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_AdditionalNode
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_AdditionalNode
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_UserName() As String
        Set(ByVal value As String)
            _strMobileNumberWS_UserName = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_UserName
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_UserName
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_Password() As String
        Set(ByVal value As String)
            _strMobileNumberWS_Password = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_Password
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_Password
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_AgentID() As String
        Set(ByVal value As String)
            _strMobileNumberWS_AgentID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_AgentID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_AgentID
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_GroupID() As String
        Set(ByVal value As String)
            _strMobileNumberWS_GroupID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_GroupID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_GroupID
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_State() As String
        Set(ByVal value As String)
            _strMobileNumberWS_State = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_State
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_State
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_PageKey() As String
        Set(ByVal value As String)
            _strMobileNumberWS_PageKey = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_PageKey
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_PageKey
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_Limit() As String
        Set(ByVal value As String)
            _strMobileNumberWS_Limit = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_Limit
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_Limit
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_IsAscending() As String
        Set(ByVal value As String)
            _strMobileNumberWS_IsAscending = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_IsAscending
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_IsAscending
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_PaIDType() As String
        Set(ByVal value As String)
            _strMobileNumberWS_PaIDType = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_PaIDType
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_PaIDType
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_NumIntentos() As String
        Set(ByVal value As String)
            _strGenericEntityWS_NumIntentos = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_NumIntentos
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_NumIntentos
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_Channel() As String
        Set(ByVal value As String)
            _strGenericEntityWS_Channel = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_Channel
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_Channel
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_AdditionalNode() As String
        Set(ByVal value As String)
            _strGenericEntityWS_AdditionalNode = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_AdditionalNode
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_AdditionalNode
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_UserName() As String
        Set(ByVal value As String)
            _strGenericEntityWS_UserName = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_UserName
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_UserName
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_Password() As String
        Set(ByVal value As String)
            _strGenericEntityWS_Password = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_Password
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_Password
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_TransactionID() As String
        Set(ByVal value As String)
            _strGenericEntityWS_TransactionID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_TransactionID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_TransactionID
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_AgentID() As String
        Set(ByVal value As String)
            _strGenericEntityWS_AgentID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_AgentID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_AgentID
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_Type() As String
        Set(ByVal value As String)
            _strGenericEntityWS_Type = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_Type
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_Type
            End If
        End Get
    End Property

    Public Shared Property strVentaActivacionWS_NumIntentos() As String
        Set(ByVal value As String)
            _strVentaActivacionWS_NumIntentos = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaActivacionWS_NumIntentos
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaActivacionWS_NumIntentos
            End If
        End Get
    End Property

    Public Shared Property strVentaActivacionWS_MsgID() As String
        Set(ByVal value As String)
            _strVentaActivacionWS_MsgID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaActivacionWS_MsgID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaActivacionWS_MsgID
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_NumIntentos() As String
        Set(ByVal value As String)
            _strMobileNumberWS_NumIntentos = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_NumIntentos
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_NumIntentos
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_attributeName() As String
        Set(ByVal value As String)
            _strMobileNumberWS_attributeName = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_attributeName
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_attributeName
            End If
        End Get
    End Property

    Public Shared Property strMobileNumberWS_attributeValue() As String
        Set(ByVal value As String)
            _strMobileNumberWS_attributeValue = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMobileNumberWS_attributeValue
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMobileNumberWS_attributeValue
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_attributeName() As String
        Set(ByVal value As String)
            _strGenericEntityWS_attributeName = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_attributeName
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_attributeName
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_attributeValue() As String
        Set(ByVal value As String)
            _strGenericEntityWS_attributeValue = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_attributeValue
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_attributeValue
            End If
        End Get
    End Property

    Public Shared Property strGenericEntityWS_ReserveDays() As String
        Set(ByVal value As String)
            _strGenericEntityWS_ReserveDays = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_ReserveDays
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_ReserveDays
            End If
        End Get
    End Property
    Public Shared Property strVentaErrorObtenerLineas() As String
        Set(ByVal value As String)
            _strVentaErrorObtenerLineas = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaErrorObtenerLineas
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaErrorObtenerLineas
            End If
        End Get
    End Property
    Public Shared Property strVentaErrorReservarLinea() As String
        Set(ByVal value As String)
            _strVentaErrorReservarLinea = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaErrorReservarLinea
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaErrorReservarLinea
            End If
        End Get
    End Property
    Public Shared Property strVentaCorreoSicar_Remitente() As String
        Set(ByVal value As String)
            _strVentaCorreoSicar_Remitente = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaCorreoSicar_Remitente
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaCorreoSicar_Remitente
            End If
        End Get
    End Property
    Public Shared Property strVentaCorreoSicar_CC() As String
        Set(ByVal value As String)
            _strVentaCorreoSicar_CC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaCorreoSicar_CC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaCorreoSicar_CC
            End If
        End Get
    End Property
    Public Shared Property strVentaCorreoSicar_ParaDefault() As String
        Set(ByVal value As String)
            _strVentaCorreoSicar_ParaDefault = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaCorreoSicar_ParaDefault
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaCorreoSicar_ParaDefault
            End If
        End Get
    End Property
    Public Shared Property strVentaCorreoSicar_Asunto() As String
        Set(ByVal value As String)
            _strVentaCorreoSicar_Asunto = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaCorreoSicar_Asunto
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaCorreoSicar_Asunto
            End If
        End Get
    End Property
    Public Shared Property strVentaCorreoSicar_Cuerpo() As String
        Set(ByVal value As String)
            _strVentaCorreoSicar_Cuerpo = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strVentaCorreoSicar_Cuerpo
            Else
                ObtenerParametro(strParamGrupo)
                Return _strVentaCorreoSicar_Cuerpo
            End If
        End Get
    End Property
    Public Shared Property strGenericEntityWS_SpID() As String
        Set(ByVal value As String)
            _strGenericEntityWS_SpID = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_SpID
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_SpID
            End If
        End Get
    End Property
    Public Shared Property strNroDigitosLinea() As String
        Set(ByVal value As String)
            _strNroDigitosLinea = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strNroDigitosLinea
            Else
                ObtenerParametro(strParamGrupo)
                Return _strNroDigitosLinea
            End If
        End Get
    End Property
    Public Shared Property strFlagCorreo() As String
        Set(ByVal value As String)
            _strFlagCorreo = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strFlagCorreo
            Else
                ObtenerParametro(strParamGrupo)
                Return _strFlagCorreo
            End If
        End Get
    End Property
    Public Shared Property strGenericEntityWS_Reserve_SicarOLO() As String
        Set(ByVal value As String)
            _strGenericEntityWS_Reserve_SicarOLO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_Reserve_SicarOLO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_Reserve_SicarOLO
            End If
        End Get
    End Property
    Public Shared Property strGenericEntityWS_statusMessage_OLO() As String
        Set(ByVal value As String)
            _strGenericEntityWS_statusMessage_OLO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_statusMessage_OLO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_statusMessage_OLO
            End If
        End Get
    End Property
    Public Shared Property strGenericEntityWS_statusCode_OLO() As String
        Set(ByVal value As String)
            _strGenericEntityWS_statusCode_OLO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strGenericEntityWS_statusCode_OLO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strGenericEntityWS_statusCode_OLO
            End If
        End Get
    End Property
    'PROY-31850 FASE IV - INICIO
    Public Shared Property strRecargaOloWSCanal() As String
        Set(ByVal value As String)
            _strRecargaOloWSCanal = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWSCanal
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWSCanal
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_AdditionalNode() As String
        Set(ByVal value As String)
            _strRecargaOloWS_AdditionalNode = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_AdditionalNode
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_AdditionalNode
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_Password() As String
        Set(ByVal value As String)
            _strRecargaOloWS_Password = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_Password
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_Password
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_UserName() As String
        Set(ByVal value As String)
            _strRecargaOloWS_UserName = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_UserName
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_UserName
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_Campo() As String
        Set(ByVal value As String)
            _strRecargaOloWS_Campo = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_Campo
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_Campo
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_Valor() As String
        Set(ByVal value As String)
            _strRecargaOloWS_Valor = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_Valor
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_Valor
            End If
        End Get
    End Property

    Public Shared Property strRecupera_Valor_CodMaterial() As String
        Set(ByVal value As String)
            _strRecupera_Valor_CodMaterial = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecupera_Valor_CodMaterial
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecupera_Valor_CodMaterial
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_gpcanal() As String
        Set(ByVal value As String)
            _strRecargaOloWS_gpcanal = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_gpcanal
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_gpcanal
            End If
        End Get
    End Property
    Public Shared Property strRecargaOloWS_version() As String
        Set(ByVal value As String)
            _strRecargaOloWS_version = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_version
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_version
            End If
        End Get
    End Property

    Public Shared Property strRecargaOloWS_compania() As String
        Set(ByVal value As String)
            _strRecargaOloWS_compania = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strRecargaOloWS_compania
            Else
                ObtenerParametro(strParamGrupo)
                Return _strRecargaOloWS_compania
            End If
        End Get
    End Property
    'PROY-31850 FASE IV - FIN
    Public Shared Function ObtenerParametro(ByVal strCodGrupo As String) As Boolean
        bolParamPOS = False
        Dim dsParametros As DataSet = (New COM_SIC_Activaciones.clsConsultaPvu).ListaParametrosGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = Funciones.CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))
                strValor = Funciones.CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))

                If (strCodigo = "4") Then
                    _strConstCodProdOLO = strValor
                ElseIf (strCodigo = "11") Then
                    _strConstCodActOLO = strValor
                ElseIf (strCodigo = "40") Then
                    _strMobileNumberWS_Channel = strValor
                ElseIf (strCodigo = "41") Then
                    _strMobileNumberWS_AdditionalNode = strValor
                ElseIf (strCodigo = "42") Then
                    _strMobileNumberWS_UserName = strValor
                ElseIf (strCodigo = "43") Then
                    _strMobileNumberWS_Password = strValor
                ElseIf (strCodigo = "44") Then
                    _strMobileNumberWS_AgentID = strValor
                ElseIf (strCodigo = "45") Then
                    _strMobileNumberWS_GroupID = strValor
                ElseIf (strCodigo = "46") Then
                    _strMobileNumberWS_State = strValor
                ElseIf (strCodigo = "47") Then
                    _strMobileNumberWS_PageKey = strValor
                ElseIf (strCodigo = "48") Then
                    _strMobileNumberWS_Limit = strValor
                ElseIf (strCodigo = "49") Then
                    _strMobileNumberWS_IsAscending = strValor
                ElseIf (strCodigo = "50") Then
                    _strMobileNumberWS_PaIDType = strValor
                ElseIf (strCodigo = "51") Then
                    _strGenericEntityWS_NumIntentos = strValor
                ElseIf (strCodigo = "52") Then
                    _strGenericEntityWS_Channel = strValor
                ElseIf (strCodigo = "53") Then
                    _strGenericEntityWS_AdditionalNode = strValor
                ElseIf (strCodigo = "54") Then
                    _strGenericEntityWS_UserName = strValor
                ElseIf (strCodigo = "55") Then
                    _strGenericEntityWS_Password = strValor
                ElseIf (strCodigo = "56") Then
                    _strGenericEntityWS_TransactionID = strValor
                ElseIf (strCodigo = "57") Then
                    _strGenericEntityWS_AgentID = strValor
                ElseIf (strCodigo = "58") Then
                    _strGenericEntityWS_Type = strValor
                ElseIf (strCodigo = "59") Then
                    _strVentaActivacionWS_NumIntentos = strValor
                ElseIf (strCodigo = "60") Then
                    _strVentaActivacionWS_MsgID = strValor
                ElseIf (strCodigo = "61") Then
                    _strMobileNumberWS_NumIntentos = strValor
                ElseIf (strCodigo = "66") Then
                    _strMobileNumberWS_attributeName = strValor
                ElseIf (strCodigo = "67") Then
                    _strMobileNumberWS_attributeValue = strValor
                ElseIf (strCodigo = "68") Then
                    _strGenericEntityWS_attributeName = strValor
                ElseIf (strCodigo = "69") Then
                    _strGenericEntityWS_attributeValue = strValor
                ElseIf (strCodigo = "72") Then
                    _strGenericEntityWS_ReserveDays = strValor
                ElseIf (strCodigo = "73") Then
                    _strGenericEntityWS_SpID = strValor
                ElseIf (strCodigo = "74") Then
                    _strVentaErrorObtenerLineas = strValor
                ElseIf (strCodigo = "75") Then
                    _strVentaErrorReservarLinea = strValor
                ElseIf (strCodigo = "81") Then
                    _strVentaCorreoSicar_Remitente = strValor
                ElseIf (strCodigo = "82") Then
                    _strVentaCorreoSicar_CC = strValor
                ElseIf (strCodigo = "83") Then
                    _strVentaCorreoSicar_ParaDefault = strValor
                ElseIf (strCodigo = "84") Then
                    _strVentaCorreoSicar_Asunto = strValor
                ElseIf (strCodigo = "85") Then
                    _strVentaCorreoSicar_Cuerpo = strValor
                ElseIf (strCodigo = "86") Then
                    _strFlagCorreo = strValor
                ElseIf (strCodigo = "88") Then
                    _strNroDigitosLinea = strValor
                ElseIf (strCodigo = "89") Then
                    _strGenericEntityWS_Reserve_SicarOLO = strValor
                ElseIf (strCodigo = "91") Then
                    _strGenericEntityWS_statusMessage_OLO = strValor
                ElseIf (strCodigo = "92") Then
                    _strGenericEntityWS_statusCode_OLO = strValor

                ElseIf (strCodigo = "105") Then
                    _strRecargaOloWS_Campo = strValor
                ElseIf (strCodigo = "106") Then
                    _strRecargaOloWS_Valor = strValor
                ElseIf (strCodigo = "107") Then
                    _strRecupera_Valor_CodMaterial = strValor
                ElseIf (strCodigo = "108") Then
                    _strRecargaOloWSCanal = strValor
                ElseIf (strCodigo = "109") Then
                    _strRecargaOloWS_AdditionalNode = strValor
                ElseIf (strCodigo = "110") Then
                    _strRecargaOloWS_UserName = strValor
                ElseIf (strCodigo = "111") Then
                    _strRecargaOloWS_Password = strValor
                ElseIf (strCodigo = "112") Then
                    _strRecargaOloWS_gpcanal = strValor
                ElseIf (strCodigo = "113") Then
                    _strRecargaOloWS_version = strValor
                ElseIf (strCodigo = "114") Then
                    _strRecargaOloWS_compania = strValor
                End If

            Next
            bolParamPOS = True
        End If
    End Function
End Class
'PROY-31850 - FIN