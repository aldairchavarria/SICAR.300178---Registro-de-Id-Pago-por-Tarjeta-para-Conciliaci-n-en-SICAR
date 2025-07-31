Imports System
Imports SisCajas.Funciones

Public Class ClsKeyPOS

  Private Shared _strCodOpeVE As String
  Private Shared _strCodOpeVC As String
  Private Shared _strCodOpeAN As String
  Private Shared _strDesOpeVE As String
  Private Shared _strDesOpeVC As String
  Private Shared _strDesOpeAN As String
  Private Shared _strOpeON As String
  Private Shared _strOpeOFF As String
  Private Shared _strOpeFina As String
  Private Shared _strNoFina As String
  Private Shared _strPosServInte As String
  Private Shared _strCodTarjetaVS As String
  Private Shared _strCodTarjetaMC As String

  Private Shared _strTipPagoVR As String
  Private Shared _strTipPagoRVF As String
  Private Shared _strTipPagoDxP As String
  Private Shared _strTipPagoPR As String
  Private Shared _strTipPagoRDAC As String
  Private Shared _strTipPagoRCC As String
  Private Shared _strTipPagoPAP As String
  Private Shared _strTipPagoPCC As String
  Private Shared _strTipPagoPDTH As String
  Private Shared _strTipPagoRCFP As String
  Private Shared _strTipPagoDP As String
  Private Shared _strTipPagoRP As String
    Private Shared _strTipPagoDEV As String

  Private Shared _strEstTRanPen As String
  Private Shared _strEstTRanPro As String
  Private Shared _strEstTRanAce As String
  Private Shared _strEstTRanRec As String
  Private Shared _strEstTRanInc As String


  Private Shared _strTipoPosVI As String
  Private Shared _strTipoPosMC As String
  Private Shared _strTipoPosAM As String
  Private Shared _strTipoPosDI As String


  Private Shared _strTipoTransPAG As String
  Private Shared _strTipoTransANU As String
  Private Shared _strTipoTransRIM As String
  Private Shared _strTipoTransRDO As String
  Private Shared _strTipoTransRTO As String
  Private Shared _strTipoTransAPP As String
  Private Shared _strTipoTransCIP As String

  Private Shared _strTipoMonSoles As String

  Private Shared _strTranMC_Compra As String
  Private Shared _strTranMC_Anulacion As String
  Private Shared _strTranMC_RepDetallado As String
  Private Shared _strTranMC_RepTotales As String
  Private Shared _strTranMC_ReImpresion As String
  Private Shared _strTranMC_Cierre As String

  Private Shared _strMonedaMC_Soles As String
  Private Shared _strMonedaMC_Dolares As String

  Private Shared _strConstMC_POS As String

  Private Shared _strMonedaVisa_Soles As String
  Private Shared _strMonedaVisa_Dolares As String

  Private Shared _strPwdComercio_MC As String
    Private Shared _strOpeNoFinan As String

    Private Shared _strMsjSinTransacciones As String
    Private Shared _strMsjValidacionCajero As String
    Private Shared _strCodOpeREC As String
    Private Shared _strDesOpeREC As String

    Private Shared _strAnulacionExitosa As String  '' WBC
    Private Shared _strAnulacionRechazada As String '' WBC
    Private Shared _strAnulacionIncompleta As String '' WBC
    Private Shared _strEstadAnul As String '' WBC
    Private Shared _strAnulMesjBloqueante As String '' WBC
    Private Shared _strValReaAnul As String '' WBC
    Private Shared _strsjErrorGuardarTrans As String
    Private Shared _strIP As String
    Private Shared _strIPMsjDesconfigurado As String
    Private Shared _strTipPagoRXDNI As String
    Private Shared _strCodPermisoAnuPOS As String
    'Proy-31949 Inicio
    Private Shared _strMsjErrorAdelanto As String
    Private Shared _strMsjErrorParcial As String

    Private Shared _strNumIntentosPago As String
    Private Shared _strNumIntentosAnular As String
    Private Shared _strMsjErrorTipTarjeta As String
    Private Shared _strMsjErrorNumIntentos As String
    Private Shared _strMsjErrorTimeOut As String
    Private Shared _strMsjPagoNumIntentos As String
    Private Shared _strMsjCajaCerrada As String
    Private Shared _strNumIntentosCuadre As String
    'Proy-31949 Fin

  Private Shared strParamGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("consParamGrupoPOS"))
  Private Shared bolParamPOS As Boolean

    'Proy-31949 Inicio
    Public Shared Property strMsjErrorParcial() As String
        Get
            If (bolParamPOS) Then
                Return _strMsjErrorParcial
            Else
                ObtenerParametro(strMsjErrorParcial)
                Return _strMsjErrorParcial
            End If
        End Get
        Set(ByVal Value As String)
            _strMsjErrorParcial = Value
        End Set
    End Property

    Public Shared Property strMsjErrorAdelanto() As String
        Get
            If (bolParamPOS) Then
                Return _strMsjErrorAdelanto
            Else
                ObtenerParametro(strMsjErrorAdelanto)
                Return _strMsjErrorAdelanto
            End If
        End Get
        Set(ByVal Value As String)
            _strMsjErrorAdelanto = Value
        End Set
    End Property

    Public Shared Property strMsjCajaCerrada() As String
        Get
            If (bolParamPOS) Then
                Return _strMsjCajaCerrada
            Else
                ObtenerParametro(strMsjCajaCerrada)
                Return _strMsjCajaCerrada
            End If
        End Get
        Set(ByVal Value As String)
            _strMsjCajaCerrada = Value
        End Set
    End Property
    Public Shared Property strNumIntentosPago() As String
        Set(ByVal value As String)
            _strNumIntentosPago = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strNumIntentosPago
            Else
                ObtenerParametro(strParamGrupo)
                Return _strNumIntentosPago
            End If
        End Get
    End Property

    Public Shared Property strNumIntentosAnular() As String
        Set(ByVal value As String)
            _strNumIntentosAnular = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strNumIntentosAnular
            Else
                ObtenerParametro(strParamGrupo)
                Return _strNumIntentosAnular
            End If
        End Get
    End Property


    Public Shared Property strMsjErrorTipTarjeta() As String
        Set(ByVal value As String)
            _strMsjErrorTipTarjeta = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjErrorTipTarjeta
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjErrorTipTarjeta
            End If
        End Get
    End Property

    Public Shared Property strMsjErrorNumIntentos() As String
        Set(ByVal value As String)
            _strMsjErrorNumIntentos = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjErrorNumIntentos
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjErrorNumIntentos
            End If
        End Get
    End Property

    Public Shared Property strMsjErrorTimeOut() As String
        Set(ByVal value As String)
            _strMsjErrorTimeOut = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjErrorTimeOut
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjErrorTimeOut
            End If
        End Get
    End Property

    Public Shared Property strMsjPagoNumIntentos() As String
        Set(ByVal value As String)
            _strMsjPagoNumIntentos = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjPagoNumIntentos
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjPagoNumIntentos
            End If
        End Get
    End Property

    Public Shared Property strNumIntentosCuadre() As String
        Set(ByVal value As String)
            _strNumIntentosCuadre = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strNumIntentosCuadre
            Else
                ObtenerParametro(strParamGrupo)
                Return _strNumIntentosCuadre
            End If
        End Get
    End Property
    'Proy-31949 Fin

    Public Shared Property strIPMsjDesconfigurado() As String
        Set(ByVal value As String)
            _strIPMsjDesconfigurado = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strIPMsjDesconfigurado
            Else
                ObtenerParametro(strParamGrupo)
                Return _strIPMsjDesconfigurado
            End If
        End Get
    End Property

    Public Shared Property strDesOpeREC() As String
        Set(ByVal value As String)
            _strDesOpeREC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strDesOpeREC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDesOpeREC
            End If
        End Get
    End Property

    Public Shared Property strCodOpeREC() As String
        Set(ByVal value As String)
            _strCodOpeREC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodOpeREC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodOpeREC
            End If
        End Get
    End Property

    Public Shared Property strPwdComercio_MC() As String
        Set(ByVal value As String)
            _strPwdComercio_MC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strPwdComercio_MC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strPwdComercio_MC
            End If
        End Get
    End Property

    Public Shared Property strCodOpeVE() As String
        Set(ByVal value As String)
            _strCodOpeVE = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodOpeVE
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodOpeVE
            End If
        End Get
    End Property
    Public Shared Property strCodOpeVC() As String
        Set(ByVal value As String)
            _strCodOpeVC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodOpeVC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodOpeVC
            End If
        End Get
    End Property
    Public Shared Property strCodOpeAN() As String
        Set(ByVal value As String)
            _strCodOpeAN = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodOpeAN
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodOpeAN
            End If
        End Get
    End Property
    Public Shared Property strDesOpeVE() As String
        Set(ByVal value As String)
            _strDesOpeVE = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strDesOpeVE
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDesOpeVE
            End If
        End Get
    End Property
    Public Shared Property strDesOpeVC() As String
        Set(ByVal value As String)
            _strDesOpeVC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strDesOpeVC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDesOpeVC
            End If
        End Get
    End Property
    Public Shared Property strDesOpeAN() As String
        Set(ByVal value As String)
            _strDesOpeAN = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strDesOpeAN
            Else
                ObtenerParametro(strParamGrupo)
                Return _strDesOpeAN
            End If
        End Get
    End Property
    Public Shared Property strOpeON() As String
        Set(ByVal value As String)
            _strOpeON = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strOpeON
            Else
                ObtenerParametro(strParamGrupo)
                Return _strOpeON
            End If
        End Get
    End Property
    Public Shared Property strOpeOFF() As String
        Set(ByVal value As String)
            _strOpeOFF = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strOpeOFF
            Else
                ObtenerParametro(strParamGrupo)
                Return _strOpeOFF
            End If
        End Get
    End Property
    Public Shared Property strOpeFina() As String
        Set(ByVal value As String)
            _strOpeFina = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strOpeFina
            Else
                ObtenerParametro(strParamGrupo)
                Return _strOpeFina
            End If
        End Get
    End Property
    Public Shared Property strNoFina() As String
        Set(ByVal value As String)
            _strNoFina = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strNoFina
            Else
                ObtenerParametro(strParamGrupo)
                Return _strNoFina
            End If
        End Get
    End Property
    Public Shared Property strPosServInte() As String
        Set(ByVal value As String)
            _strPosServInte = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strPosServInte
            Else
                ObtenerParametro(strParamGrupo)
                Return _strPosServInte
            End If
        End Get
    End Property
    Public Shared Property strCodTarjetaVS() As String
        Set(ByVal value As String)
            _strCodTarjetaVS = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodTarjetaVS
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodTarjetaVS
            End If
        End Get
    End Property
    Public Shared Property strCodTarjetaMC() As String
        Set(ByVal value As String)
            _strCodTarjetaMC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodTarjetaMC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodTarjetaMC
            End If
        End Get
    End Property
    Public Shared Property strTipPagoVR() As String
        Set(ByVal value As String)
            _strTipPagoVR = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoVR
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoVR
            End If
        End Get
    End Property
    Public Shared Property strTipPagoRVF() As String
        Set(ByVal value As String)
            _strTipPagoRVF = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRVF
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRVF
            End If
        End Get
    End Property
    Public Shared Property strTipPagoDxP() As String
        Set(ByVal value As String)
            _strTipPagoDxP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoDxP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoDxP
            End If
        End Get
    End Property
    Public Shared Property strTipPagoPR() As String
        Set(ByVal value As String)
            _strTipPagoPR = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoPR
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoPR
            End If
        End Get
    End Property
    Public Shared Property strTipPagoRDAC() As String
        Set(ByVal value As String)
            _strTipPagoRDAC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRDAC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRDAC
            End If
        End Get
    End Property
    Public Shared Property strTipPagoRCC() As String
        Set(ByVal value As String)
            _strTipPagoRCC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRCC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRCC
            End If
        End Get
    End Property
    Public Shared Property strTipPagoPAP() As String
        Set(ByVal value As String)
            _strTipPagoPAP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoPAP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoPAP
            End If
        End Get
    End Property
    Public Shared Property strTipPagoPCC() As String
        Set(ByVal value As String)
            _strTipPagoPCC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoPCC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoPCC
            End If
        End Get
    End Property
    Public Shared Property strTipPagoPDTH() As String
        Set(ByVal value As String)
            _strTipPagoPDTH = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoPDTH
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoPDTH
            End If
        End Get
    End Property
    Public Shared Property strTipPagoRCFP() As String
        Set(ByVal value As String)
            _strTipPagoRCFP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRCFP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRCFP
            End If
        End Get
    End Property
    Public Shared Property strTipPagoDP() As String
        Set(ByVal value As String)
            _strTipPagoDP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoDP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoDP
            End If
        End Get
    End Property
    Public Shared Property strTipPagoRP() As String
        Set(ByVal value As String)
            _strTipPagoRP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRP
            End If
        End Get
    End Property
    Public Shared Property strTipPagoDEV() As String
        Set(ByVal value As String)
            _strTipPagoDEV = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoDEV
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoDEV
            End If
        End Get
    End Property


    Public Shared Property strEstTRanPen() As String
        Set(ByVal value As String)
            _strEstTRanPen = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstTRanPen
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstTRanPen
            End If
        End Get
    End Property
    Public Shared Property strEstTRanPro() As String
        Set(ByVal value As String)
            _strEstTRanPro = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstTRanPro
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstTRanPro
            End If
        End Get
    End Property
    Public Shared Property strEstTRanAce() As String
        Set(ByVal value As String)
            _strEstTRanAce = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstTRanAce
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstTRanAce
            End If
        End Get
    End Property
    Public Shared Property strEstTRanRec() As String
        Set(ByVal value As String)
            _strEstTRanRec = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstTRanRec
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstTRanRec
            End If
        End Get
    End Property
    Public Shared Property strEstTRanInc() As String
        Set(ByVal value As String)
            _strEstTRanInc = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstTRanInc
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstTRanInc
            End If
        End Get
    End Property
    Public Shared Property strTipoPosVI() As String
        Set(ByVal value As String)
            _strTipoPosVI = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoPosVI
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoPosVI
            End If
        End Get
    End Property
    Public Shared Property strTipoPosMC() As String
        Set(ByVal value As String)
            _strTipoPosMC = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoPosMC
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoPosMC
            End If
        End Get
    End Property

    Public Shared Property strTipoPosAM() As String
        Set(ByVal value As String)
            _strTipoPosAM = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoPosAM
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoPosAM
            End If
        End Get
    End Property

    Public Shared Property strTipoPosDI() As String
        Set(ByVal value As String)
            _strTipoPosDI = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoPosDI
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoPosDI
            End If
        End Get
    End Property

    Public Shared Property strTipoTransPAG() As String
        Set(ByVal value As String)
            _strTipoTransPAG = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransPAG
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransPAG
            End If
        End Get
    End Property
    Public Shared Property strTipoTransANU() As String
        Set(ByVal value As String)
            _strTipoTransANU = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransANU
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransANU
            End If
        End Get
    End Property
    Public Shared Property strTipoTransRIM() As String
        Set(ByVal value As String)
            _strTipoTransRIM = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransRIM
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransRIM
            End If
        End Get
    End Property
    Public Shared Property strTipoTransRDO() As String
        Set(ByVal value As String)
            _strTipoTransRDO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransRDO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransRDO
            End If
        End Get
    End Property
    Public Shared Property strTipoTransRTO() As String
        Set(ByVal value As String)
            _strTipoTransRTO = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransRTO
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransRTO
            End If
        End Get
    End Property
    Public Shared Property strTipoTransAPP() As String
        Set(ByVal value As String)
            _strTipoTransAPP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransAPP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransAPP
            End If
        End Get
    End Property
    Public Shared Property strTipoTransCIP() As String
        Set(ByVal value As String)
            _strTipoTransCIP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoTransCIP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoTransCIP
            End If
        End Get
    End Property

    Public Shared Property strTipoMonSoles() As String
        Set(ByVal value As String)
            _strTipoMonSoles = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipoMonSoles
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipoMonSoles
            End If
        End Get
    End Property


    Public Shared Property strTranMC_Compra() As String
        Set(ByVal value As String)
            _strTranMC_Compra = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_Compra
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_Compra
            End If
        End Get
    End Property
    Public Shared Property strTranMC_Anulacion() As String
        Set(ByVal value As String)
            _strTranMC_Anulacion = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_Anulacion
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_Anulacion
            End If
        End Get
    End Property
    Public Shared Property strTranMC_RepDetallado() As String
        Set(ByVal value As String)
            _strTranMC_RepDetallado = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_RepDetallado
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_RepDetallado
            End If
        End Get
    End Property
    Public Shared Property strTranMC_RepTotales() As String
        Set(ByVal value As String)
            _strTranMC_RepTotales = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_RepTotales
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_RepTotales
            End If
        End Get
    End Property
    Public Shared Property strTranMC_ReImpresion() As String
        Set(ByVal value As String)
            _strTranMC_ReImpresion = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_ReImpresion
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_ReImpresion
            End If
        End Get
    End Property
    Public Shared Property strTranMC_Cierre() As String
        Set(ByVal value As String)
            _strTranMC_Cierre = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTranMC_Cierre
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTranMC_Cierre
            End If
        End Get
    End Property

    Public Shared Property strMonedaMC_Soles() As String
        Set(ByVal value As String)
            _strMonedaMC_Soles = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMonedaMC_Soles
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMonedaMC_Soles
            End If
        End Get
    End Property
    Public Shared Property strMonedaMC_Dolares() As String
        Set(ByVal value As String)
            _strMonedaMC_Dolares = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMonedaMC_Dolares
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMonedaMC_Dolares
            End If
        End Get
    End Property

    Public Shared Property strConstMC_POS() As String
        Set(ByVal value As String)
            _strConstMC_POS = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strConstMC_POS
            Else
                ObtenerParametro(strParamGrupo)
                Return _strConstMC_POS
            End If
        End Get
    End Property

    Public Shared Property strMonedaVisa_Soles() As String
        Set(ByVal value As String)
            _strMonedaVisa_Soles = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMonedaVisa_Soles
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMonedaVisa_Soles
            End If
        End Get
    End Property
    Public Shared Property strMonedaVisa_Dolares() As String
        Set(ByVal value As String)
            _strMonedaVisa_Dolares = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMonedaVisa_Dolares
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMonedaVisa_Dolares
            End If
        End Get
    End Property
    Public Shared Property strOpeNoFinan() As String
        Set(ByVal value As String)
            _strOpeNoFinan = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strOpeNoFinan
            Else
                ObtenerParametro(strParamGrupo)
                Return _strOpeNoFinan
            End If
        End Get
    End Property

    Public Shared Property strMsjSinTransacciones() As String
        Set(ByVal value As String)
            _strMsjSinTransacciones = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjSinTransacciones
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjSinTransacciones
            End If
        End Get
    End Property

    Public Shared Property strMsjValidacionCajero() As String
        Set(ByVal value As String)
            _strMsjValidacionCajero = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strMsjValidacionCajero
            Else
                ObtenerParametro(strParamGrupo)
                Return _strMsjValidacionCajero
            End If
        End Get
    End Property

    ''WBC INI
    Public Shared Property strAnulacionExitosa() As String
        Set(ByVal value As String)
            _strAnulacionExitosa = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strAnulacionExitosa
            Else
                ObtenerParametro(strParamGrupo)
                Return _strAnulacionExitosa
            End If
        End Get
    End Property
    Public Shared Property strAnulacionRechazada() As String
        Set(ByVal value As String)
            _strAnulacionRechazada = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strAnulacionRechazada
            Else
                ObtenerParametro(strParamGrupo)
                Return _strAnulacionRechazada
            End If
        End Get
    End Property
    Public Shared Property strAnulacionIncompleta() As String
        Set(ByVal value As String)
            _strAnulacionIncompleta = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strAnulacionIncompleta
            Else
                ObtenerParametro(strParamGrupo)
                Return _strAnulacionIncompleta
            End If
        End Get
    End Property
    Public Shared Property strEstadAnul() As String
        Set(ByVal value As String)
            _strEstadAnul = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strEstadAnul
            Else
                ObtenerParametro(strParamGrupo)
                Return _strEstadAnul
            End If
        End Get
    End Property
    Public Shared Property strAnulMesjBloqueante() As String
        Set(ByVal value As String)
            _strAnulMesjBloqueante = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strAnulMesjBloqueante
            Else
                ObtenerParametro(strParamGrupo)
                Return _strAnulMesjBloqueante
            End If
        End Get
    End Property
    Public Shared Property strValReaAnul() As String
        Set(ByVal value As String)
            _strValReaAnul = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strValReaAnul
            Else
                ObtenerParametro(strParamGrupo)
                Return _strValReaAnul
            End If
        End Get
    End Property
    ''WBC FIN
    Public Shared Property strsjErrorGuardarTrans() As String
        Set(ByVal value As String)
            _strsjErrorGuardarTrans = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strsjErrorGuardarTrans
            Else
                ObtenerParametro(strParamGrupo)
                Return _strsjErrorGuardarTrans
            End If
        End Get
    End Property
    Public Shared Property strIP() As String
        Set(ByVal value As String)
            _strIP = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strIP
            Else
                ObtenerParametro(strParamGrupo)
                Return _strIP
            End If
        End Get
    End Property

    Public Shared Property strTipPagoRXDNI() As String
        Set(ByVal value As String)
            _strTipPagoRXDNI = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strTipPagoRXDNI
            Else
                ObtenerParametro(strParamGrupo)
                Return _strTipPagoRXDNI
            End If
        End Get
    End Property

    Public Shared Property strCodPermisoAnuPOS() As String
        Set(ByVal value As String)
            _strTipPagoRXDNI = value
        End Set
        Get
            If (bolParamPOS) Then
                Return _strCodPermisoAnuPOS
            Else
                ObtenerParametro(strParamGrupo)
                Return _strCodPermisoAnuPOS
            End If
        End Get
    End Property



    Public Shared Function ObtenerParametro(ByVal strCodGrupo As String) As Boolean
        bolParamPOS = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).FP_ConsultaParametros(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("SPARV_KEY"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("SPARV_VALUE"))

                If (strCodigo = "3") Then
                    _strCodOpeVE = strValor
                ElseIf (strCodigo = "4") Then
                    _strCodOpeVC = strValor
                ElseIf (strCodigo = "5") Then
                    _strCodOpeAN = strValor
                ElseIf (strCodigo = "6") Then
                    _strDesOpeVE = strValor
                ElseIf (strCodigo = "7") Then
                    _strDesOpeVC = strValor
                ElseIf (strCodigo = "8") Then
                    _strDesOpeAN = strValor
                ElseIf (strCodigo = "9") Then
                    _strOpeFina = strValor
                ElseIf (strCodigo = "10") Then
                    _strNoFina = strValor
                ElseIf (strCodigo = "11") Then
                    _strPosServInte = strValor
                ElseIf (strCodigo = "12") Then
                    _strOpeOFF = strValor
                ElseIf (strCodigo = "13") Then
                    _strOpeON = strValor
                ElseIf (strCodigo = "14") Then
                    _strCodTarjetaVS = strValor
                ElseIf (strCodigo = "15") Then
                    _strCodTarjetaMC = strValor
                ElseIf (strCodigo = "16") Then
                    _strTipPagoVR = strValor 'Ventas Rápidas
                ElseIf (strCodigo = "17") Then
                    _strTipPagoRVF = strValor
                ElseIf (strCodigo = "18") Then
                    _strTipPagoDxP = strValor 'Documentos por pagar
                ElseIf (strCodigo = "19") Then
                    _strTipPagoPR = strValor
                ElseIf (strCodigo = "20") Then
                    _strTipPagoRDAC = strValor
                ElseIf (strCodigo = "21") Then
                    _strTipPagoRCC = strValor
                ElseIf (strCodigo = "22") Then
                    _strTipPagoPAP = strValor
                ElseIf (strCodigo = "23") Then
                    _strTipPagoPCC = strValor
                ElseIf (strCodigo = "24") Then
                    _strTipPagoPDTH = strValor
                ElseIf (strCodigo = "25") Then
                    _strTipPagoRCFP = strValor
                ElseIf (strCodigo = "26") Then
                    _strTipPagoDP = strValor
                ElseIf (strCodigo = "27") Then
                    _strTipPagoRP = strValor 'Recaudaciones Procesadas
                ElseIf (strCodigo = "28") Then
                    _strTipPagoDEV = strValor 'Devoluciones
                ElseIf (strCodigo = "29") Then
                    _strEstTRanPen = strValor 'PENDIENTE
                ElseIf (strCodigo = "30") Then
                    _strEstTRanPro = strValor
                ElseIf (strCodigo = "31") Then
                    _strEstTRanAce = strValor
                ElseIf (strCodigo = "32") Then
                    _strEstTRanRec = strValor
                ElseIf (strCodigo = "33") Then
                    _strEstTRanInc = strValor
                ElseIf (strCodigo = "34") Then
                    _strTipoPosVI = strValor
                ElseIf (strCodigo = "35") Then
                    _strTipoPosMC = strValor
                ElseIf (strCodigo = "36") Then
                    _strTipoPosAM = strValor
                ElseIf (strCodigo = "37") Then
                    _strTipoPosDI = strValor 'DINERS
                ElseIf (strCodigo = "38") Then
                    _strTipoTransPAG = strValor 'PAGO
                ElseIf (strCodigo = "39") Then
                    _strTipoTransANU = strValor
                ElseIf (strCodigo = "40") Then
                    _strTipoTransRIM = strValor
                ElseIf (strCodigo = "41") Then
                    _strTipoTransRDO = strValor
                ElseIf (strCodigo = "42") Then
                    _strTipoTransRTO = strValor
                ElseIf (strCodigo = "43") Then
                    _strTipoTransAPP = strValor
                ElseIf (strCodigo = "44") Then
                    _strTipoTransCIP = strValor 'CIERRE DE PUERTO
                ElseIf (strCodigo = "45") Then
                    _strTipoMonSoles = strValor 'SOLES=1
                ElseIf (strCodigo = "46") Then
                    _strTranMC_Compra = strValor 'COMPRA MC=01
                ElseIf (strCodigo = "47") Then
                    _strTranMC_Anulacion = strValor
                ElseIf (strCodigo = "48") Then
                    _strTranMC_RepDetallado = strValor
                ElseIf (strCodigo = "49") Then
                    _strTranMC_RepTotales = strValor
                ElseIf (strCodigo = "50") Then
                    _strTranMC_ReImpresion = strValor
                ElseIf (strCodigo = "51") Then
                    _strTranMC_Cierre = strValor
                ElseIf (strCodigo = "52") Then
                    _strMonedaMC_Soles = strValor 'MONEDA SOLES MC=604
                ElseIf (strCodigo = "53") Then
                    _strMonedaMC_Dolares = strValor
                ElseIf (strCodigo = "54") Then
                    _strConstMC_POS = strValor
                ElseIf (strCodigo = "55") Then
                    _strMonedaVisa_Soles = strValor
                ElseIf (strCodigo = "56") Then
                    _strMonedaVisa_Dolares = strValor
                ElseIf (strCodigo = "57") Then
                    _strEstadAnul = strValor ' ACEPTADO|RECHAZADO|INCOMPLETO
                ElseIf (strCodigo = "58") Then
                    _strAnulMesjBloqueante = strValor ' No se puede Anular Pago. Existe un pago con estado RECHAZADO.
                ElseIf (strCodigo = "59") Then
                    _strValReaAnul = strValor 'DINNERS|TARJETA VISA|TARJETA MASTERCARD|AMERICAN EXPRESS
                ElseIf (strCodigo = "60") Then
                    _strDesOpeREC = strValor
                ElseIf (strCodigo = "61") Then
                    _strCodOpeREC = strValor
                ElseIf (strCodigo = "64") Then
                    _strPwdComercio_MC = strValor
                ElseIf (strCodigo = "65") Then
                    _strOpeNoFinan = strValor
                ElseIf (strCodigo = "66") Then
                    _strAnulacionExitosa = strValor 'TRANSACCION EXITOSA
                ElseIf (strCodigo = "67") Then
                    _strAnulacionRechazada = strValor 'TRANSACCION RECHAZADA
                ElseIf (strCodigo = "68") Then
                    _strAnulacionIncompleta = strValor 'TRANSACCION INCOMPLETA
                ElseIf (strCodigo = "69") Then
                    _strMsjSinTransacciones = strValor
                ElseIf (strCodigo = "70") Then
                    _strMsjValidacionCajero = strValor
                ElseIf (strCodigo = "71") Then
                    _strsjErrorGuardarTrans = strValor
                ElseIf (strCodigo = "72") Then
                    _strIP = strValor
                ElseIf (strCodigo = "73") Then
                    _strIPMsjDesconfigurado = strValor
                ElseIf (strCodigo = "74") Then
                    _strTipPagoRXDNI = strValor
                ElseIf (strCodigo = "75") Then
                    _strCodPermisoAnuPOS = strValor
                ElseIf (strCodigo = "76") Then
                    _strNumIntentosPago = strValor
                ElseIf (strCodigo = "77") Then
                    _strNumIntentosAnular = strValor
                ElseIf (strCodigo = "78") Then
                    _strMsjErrorTipTarjeta = strValor
                ElseIf (strCodigo = "79") Then
                    _strMsjErrorNumIntentos = strValor
                ElseIf (strCodigo = "80") Then
                    _strMsjErrorTimeOut = strValor
                ElseIf (strCodigo = "81") Then
                    _strMsjPagoNumIntentos = strValor
                ElseIf (strCodigo = "82") Then
                    _strMsjCajaCerrada = strValor
                ElseIf (strCodigo = "83") Then
                    _strMsjErrorAdelanto = strValor
                ElseIf (strCodigo = "84") Then
                    _strMsjErrorParcial = strValor
                ElseIf (strCodigo = "85") Then
                    _strNumIntentosCuadre = strValor
                End If

            Next
            bolParamPOS = True
        End If
    End Function
  
End Class
