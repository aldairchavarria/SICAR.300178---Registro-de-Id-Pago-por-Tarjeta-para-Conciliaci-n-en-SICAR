Imports System
Imports SisCajas.Funciones

'PROY-24724-IDEA-28174 - INI CLASE
Public Class clsKeyAPP
    Public Shared _strCodServicioPM As String
    Public Shared _strClaseBolFactCanje As String
    Public Shared _strProteccionMovilError As String
    Public Shared _strProteccionMovilPendiente As String
    Public Shared _strUserProceso As String
    Public Shared _strNotasCanje As String
    Public Shared _strSubClaseCanje As String
    Public Shared _strEstadoVentaIndividual As String
    Public Shared _strNotasVI As String
    Public Shared _strSubClaseVI As String
    Public Shared _strEstadoCanje As String
    Public Shared _strFlagdePlantillaVIndividual As String
    Public Shared _strOperacionVenta As String
    Public Shared _strEstadoErrorCanVIn As String
    Public Shared _strEstadoError As String
    Public Shared _strEstadoRespuesta As String
    Public Shared _strEstadoRespuestaCanje As String
    Public Shared _strRemitentePM As String
    Public Shared _strDestinatarioPM As String
    Public Shared _strFlagCorreoPM As String
    Public Shared _strFlagdePlantillaCanje As String
    Public Shared _strProteccionMovilNoPagada As String
    Public Shared _strAsuntoPM As String
    Public Shared _strMensajePagoPM As String
    Public Shared _strMensajeAnulacionPM As String
    'PROY-24724 - Iteracion 2 Siniestros -INI
    Public Shared _strFlagSiniestro As String
    Public Shared _strEstdPendienteSiniestro As String
    Public Shared _strEstdAprobadoSiniestro As String
    Public Shared _strEstdFinalizadoPagoSiniestro As String
    Public Shared _strCodMaterialSiniestro As String
    Public Shared _strFlagPlantillaSiniestro As String
    Public Shared _strTipiEstadoSiniestro As String
    Public Shared _strTipiSubClaseSiniestro As String
    Public Shared _strAsuntoSiniestro As String
    Public Shared _strMensajeSiniestro As String
    Public Shared _strNotasSiniestro As String
    'PROY-24724 - Iteracion 2 Siniestros -FIN
    Public Shared bolParamProteccionMovil As Boolean
    Public Shared bolParamEnvioRemesa As Boolean 'PBI000002148450
    Public Shared intParamGrupo As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("consParametroGrupo")) '44444508"
    'Inicio - INI-936 - CNSO
    Public Shared _strAlertaCompServ As String
    Public Shared Key_ParamGrupoINI936 As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("Key_ParamGrupoINI936"))
    'Fin - INI-936 - CNSO
    
    Public Shared Property strCodServicioPM() As String
        Set(ByVal value As String)
            _strCodServicioPM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strCodServicioPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strCodServicioPM
            End If
        End Get
    End Property
    Public Shared Property strMensajePagoPM() As String 'PROY-24724 - Iteracion 2  
        Set(ByVal value As String)
            _strMensajePagoPM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strMensajePagoPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strMensajePagoPM
            End If
        End Get
    End Property
    Public Shared Property strFlagdePlantillaCanje() As String
        Set(ByVal value As String)
            _strFlagdePlantillaCanje = value 'PROY-24724 - Iteracion 2  
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strFlagdePlantillaCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strFlagdePlantillaCanje
            End If
        End Get
    End Property
    Public Shared Property strAsuntoPM() As String
        Set(ByVal value As String)
            _strAsuntoPM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strAsuntoPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strAsuntoPM
            End If
        End Get
    End Property
    Public Shared Property strClaseBolFactCanje() As String
        Set(ByVal value As String)
            strClaseBolFactCanje = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strClaseBolFactCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strClaseBolFactCanje
            End If
        End Get
    End Property
    Public Shared Property strProteccionMovilError() As String
        Set(ByVal value As String)
            _strProteccionMovilError = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strProteccionMovilError
            Else
                ObtenerParametro(intParamGrupo)
                Return _strProteccionMovilError
            End If
        End Get
    End Property
    Public Shared Property strProteccionMovilPendiente() As String
        Set(ByVal value As String)
            _strProteccionMovilPendiente = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strProteccionMovilPendiente 'PROY-24724 - Iteracion 2  
            Else
                ObtenerParametro(intParamGrupo)
                Return -_strProteccionMovilPendiente 'PROY-24724 - Iteracion 2  
            End If
        End Get
    End Property
    Public Shared Property strUserProceso() As String
        Set(ByVal value As String)
            _strUserProceso = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strUserProceso
            Else
                ObtenerParametro(intParamGrupo)
                Return _strUserProceso
            End If
        End Get
    End Property
    Public Shared Property strNotasCanje() As String
        Set(ByVal value As String)
            _strNotasCanje = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strNotasCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strNotasCanje
            End If
        End Get
    End Property
    Public Shared Property strSubClaseCanje() As String
        Set(ByVal value As String)
            _strSubClaseCanje = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strSubClaseCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strSubClaseCanje
            End If
        End Get
    End Property
    Public Shared Property strEstadoVentaIndividual() As String
        Set(ByVal value As String)
            _strEstadoVentaIndividual = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoVentaIndividual
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoVentaIndividual
            End If
        End Get
    End Property
    Public Shared Property strNotasVI() As String
        Set(ByVal value As String)
            _strNotasVI = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strNotasVI
            Else
                ObtenerParametro(intParamGrupo)
                Return _strNotasVI
            End If
        End Get
    End Property
    Public Shared Property strSubClaseVI() As String
        Set(ByVal value As String)
            _strSubClaseVI = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strSubClaseVI
            Else
                ObtenerParametro(intParamGrupo)
                Return _strSubClaseVI
            End If
        End Get
    End Property
    Public Shared Property strEstadoCanje() As String
        Set(ByVal value As String)
            _strEstadoCanje = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoCanje
            End If
        End Get
    End Property
    Public Shared Property strFlagdePlantillaVIndividual() As String
        Set(ByVal value As String)
            _strFlagdePlantillaVIndividual = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strFlagdePlantillaVIndividual
            Else
                ObtenerParametro(intParamGrupo)
                Return _strFlagdePlantillaVIndividual
            End If
        End Get
    End Property
    Public Shared Property strOperacionVenta() As String
        Set(ByVal value As String)
            _strOperacionVenta = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strOperacionVenta
            Else
                ObtenerParametro(intParamGrupo)
                Return _strOperacionVenta
            End If
        End Get
    End Property
    Public Shared Property strEstadoErrorCanVIn() As String
        Set(ByVal value As String)
            _strEstadoErrorCanVIn = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoErrorCanVIn
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoErrorCanVIn
            End If
        End Get
    End Property

    Public Shared Property strEstadoError() As String
        Set(ByVal value As String)
            _strEstadoError = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoError
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoError
            End If
        End Get
    End Property
    Public Shared Property strEstadoRespuesta() As String
        Set(ByVal value As String)
            _strEstadoRespuesta = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoRespuesta
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoRespuesta
            End If
        End Get
    End Property
    Public Shared Property strEstadoRespuestaCanje() As String
        Set(ByVal value As String)
            _strEstadoRespuestaCanje = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstadoRespuestaCanje
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstadoRespuestaCanje
            End If
        End Get
    End Property
    Public Shared Property strRemitentePM() As String
        Set(ByVal value As String)
            _strRemitentePM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strRemitentePM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strRemitentePM
            End If
        End Get
    End Property
    Public Shared Property strDestinatarioPM() As String
        Set(ByVal value As String)
            _strDestinatarioPM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strDestinatarioPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strDestinatarioPM
            End If
        End Get
    End Property

    Public Shared Property strFlagCorreoPM() As String
        Set(ByVal value As String)
            _strFlagCorreoPM = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strFlagCorreoPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strFlagCorreoPM
            End If
        End Get
    End Property

    Public Shared Property strProteccionMovilNoPagada() As String
        Set(ByVal value As String)
            _strProteccionMovilNoPagada = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strProteccionMovilNoPagada
            Else
                ObtenerParametro(intParamGrupo)
                Return _strProteccionMovilNoPagada
            End If
        End Get
    End Property


    Public Shared Property strMensajeAnulacionPM() As String
        Set(ByVal value As String)
            _strMensajeAnulacionPM = value 'PROY-24724 - Iteracion 2  
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strMensajeAnulacionPM
            Else
                ObtenerParametro(intParamGrupo)
                Return _strMensajeAnulacionPM
            End If
        End Get
    End Property
    'PROY-24724 - Iteracion 2 Siniestros -INI
    Public Shared Property strFlagSiniestro() As String
        Set(ByVal value As String)
            _strFlagSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strFlagSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strFlagSiniestro
            End If
        End Get
    End Property

    Public Shared Property strEstdPendienteSiniestro() As String
        Set(ByVal value As String)
            _strEstdPendienteSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstdPendienteSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstdPendienteSiniestro
            End If
        End Get
    End Property
    Public Shared Property strEstdFinalizadoPagoSiniestro() As String
        Set(ByVal value As String)
            _strEstdFinalizadoPagoSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstdFinalizadoPagoSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstdFinalizadoPagoSiniestro
            End If
        End Get
    End Property

    Public Shared Property strEstdAprobadoSiniestro() As String
        Set(ByVal value As String)
            _strEstdAprobadoSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strEstdAprobadoSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strEstdAprobadoSiniestro
            End If
        End Get
    End Property

    Public Shared Property strCodMaterialSiniestro() As String
        Set(ByVal value As String)
            _strCodMaterialSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strCodMaterialSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strCodMaterialSiniestro
            End If
        End Get
    End Property

    Public Shared Property strFlagPlantillaSiniestro() As String
        Set(ByVal value As String)
            _strFlagPlantillaSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strFlagPlantillaSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strFlagPlantillaSiniestro
            End If
        End Get
    End Property


    Public Shared Property strTipiEstadoSiniestro() As String
        Set(ByVal value As String)
            _strTipiEstadoSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strTipiEstadoSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strTipiEstadoSiniestro
            End If
        End Get
    End Property

    Public Shared Property strTipiSubClaseSiniestro() As String
        Set(ByVal value As String)
            _strTipiSubClaseSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strTipiSubClaseSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strTipiSubClaseSiniestro
            End If
        End Get
    End Property

    Public Shared Property strAsuntoSiniestro() As String
        Set(ByVal value As String)
            _strAsuntoSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strAsuntoSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strAsuntoSiniestro
            End If
        End Get
    End Property
    Public Shared Property strMensajeSiniestro() As String
        Set(ByVal value As String)
            _strMensajeSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strMensajeSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strMensajeSiniestro
            End If
        End Get
    End Property

    Public Shared Property strNotasSiniestro() As String
        Set(ByVal value As String)
            _strNotasSiniestro = value
        End Set
        Get
            If (bolParamProteccionMovil) Then
                Return _strNotasSiniestro
            Else
                ObtenerParametro(intParamGrupo)
                Return _strNotasSiniestro
            End If
        End Get
    End Property
    'PROY-24724 - Iteracion 2 Siniestros -FIN

#Region "Portabilidad"
    'PROY-32089  F2 INI
    Public Shared bolParamPorta As Boolean
    Private Shared ConsParametrosPortabildiadCP As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("ConsParametrosPortabildiadCP")) '44444510
    Public Shared _consEstadosCPPermitidos As String
    Public Shared _consEstadoDeudaCPPermitidos As String
    Public Shared _consMensPagoNoProce As String
    Public Shared _consTipoMensajeEsperandoRespuestaSP As String
    Public Shared _consTipoMensajeFinalesSP As String
    Public Shared _consEstadosSPPermitidos As String
    Public Shared _consEstadosSPNoPermitidos As String
    Public Shared _consTipoProductoPermitidosSP As String
    Public Shared _consEstadosNoSP As String
    'PROY-140223
    Public Shared _consDiasPermitidosPagoPEP As Integer

    Public Shared Property consDiasPermitidosPagoPEP() As Integer
        Set(ByVal value As Integer)
            _consDiasPermitidosPagoPEP = value
        End Set
        Get
            If (_consDiasPermitidosPagoPEP = 0) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consDiasPermitidosPagoPEP
            Else
                Return _consDiasPermitidosPagoPEP
            End If
        End Get
    End Property
    'PROY-140223

    Public Shared Property consEstadosSPPermitidos() As String
        Set(ByVal value As String)
            _consEstadosSPPermitidos = value
        End Set
        Get
            If (_consEstadosSPPermitidos Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consEstadosSPPermitidos
            Else
                Return _consEstadosSPPermitidos
            End If
        End Get
    End Property

    Public Shared Property consEstadosSPNoPermitidos() As String
        Set(ByVal value As String)
            _consEstadosSPNoPermitidos = value
        End Set
        Get
            If (_consEstadosSPNoPermitidos Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consEstadosSPNoPermitidos
            Else
                Return _consEstadosSPNoPermitidos
            End If
        End Get
    End Property

    Public Shared Property consEstadosNoSP() As String
        Set(ByVal value As String)
            _consEstadosNoSP = value
        End Set
        Get
            If (_consEstadosNoSP Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consEstadosNoSP
            Else
                Return _consEstadosNoSP
            End If
        End Get
    End Property

    'PROY-26963 F2 JACOSTA FIN

    Public Shared Property consEstadosCPPermitidos() As String
        Set(ByVal value As String)
            _consEstadosCPPermitidos = value
        End Set
        Get
            If (_consEstadosCPPermitidos Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consEstadosCPPermitidos
            Else
                Return _consEstadosCPPermitidos
            End If
        End Get
    End Property

    Public Shared Property consEstadoDeudaCPPermitidos() As String
        Set(ByVal value As String)
            _consEstadoDeudaCPPermitidos = value
        End Set
        Get
            If (_consEstadoDeudaCPPermitidos Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consEstadoDeudaCPPermitidos
            Else
                Return _consEstadoDeudaCPPermitidos
            End If
        End Get
    End Property

    Public Shared Property consMensPagoNoProce() As String
        Set(ByVal value As String)
            _consMensPagoNoProce = value
        End Set
        Get
            If (_consMensPagoNoProce Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consMensPagoNoProce
            Else
                Return _consMensPagoNoProce
            End If
        End Get
    End Property

    Public Shared Property consTipoMensajeFinalesSP() As String
        Set(ByVal value As String)
            _consTipoMensajeFinalesSP = value
        End Set
        Get
            If (_consTipoMensajeFinalesSP Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consTipoMensajeFinalesSP
            Else
                Return _consTipoMensajeFinalesSP
            End If
        End Get
    End Property
    Public Shared Property consTipoMensajeEsperandoRespuestaSP() As String
        Set(ByVal value As String)
            _consTipoMensajeEsperandoRespuestaSP = value
        End Set
        Get
            If (_consTipoMensajeEsperandoRespuestaSP Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consTipoMensajeEsperandoRespuestaSP
            Else
                Return _consTipoMensajeEsperandoRespuestaSP
            End If
        End Get
    End Property
    Public Shared Property consTipoProductoPermitidosSP() As String
        Set(ByVal value As String)
            _consTipoProductoPermitidosSP = value
        End Set
        Get
            If (_consTipoProductoPermitidosSP Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consTipoProductoPermitidosSP
            Else
                Return _consTipoProductoPermitidosSP
            End If
        End Get

    End Property
    #End Region

    'INI: PROY-140262 BLACKOUT
#Region "PROY-BLACKOUT"
    Public Shared _consFlagBlackOut As Integer = -1
    Public Shared _consDocPermitidosBlackOut As String
    Public Shared _consMensajePagoCACBlackOut As String
    Public Shared _consMensajeProcesarSolicitudPortabilidadBlackOut As String
    Public Shared _consMensajeRealizarProgramacionBlackOut As String

    Public Shared Property consFlagBlackOut() As Integer
        Set(ByVal value As Integer)
            _consFlagBlackOut = value
        End Set
        Get
            If (_consFlagBlackOut = -1) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consFlagBlackOut
            Else
                Return _consFlagBlackOut
            End If
        End Get
    End Property

    Public Shared Property consMensajePagoCACBlackOut() As String
        Set(ByVal value As String)
            _consMensajePagoCACBlackOut = value
        End Set
        Get
            If (_consMensajePagoCACBlackOut Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consMensajePagoCACBlackOut
            Else
                Return _consMensajePagoCACBlackOut
            End If
        End Get
    End Property

    Public Shared Property consDocPermitidosBlackOut() As String
        Set(ByVal value As String)
            _consDocPermitidosBlackOut = value
        End Set
        Get
            If (_consDocPermitidosBlackOut Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consDocPermitidosBlackOut
            Else
                Return _consDocPermitidosBlackOut
            End If
        End Get
    End Property

    Public Shared Property consMensajeProcesarSolicitudPortabilidadBlackOut() As String
        Set(ByVal value As String)
            _consMensajeProcesarSolicitudPortabilidadBlackOut = value
        End Set
        Get
            If (_consMensajeProcesarSolicitudPortabilidadBlackOut Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consMensajeProcesarSolicitudPortabilidadBlackOut
            Else
                Return _consMensajeProcesarSolicitudPortabilidadBlackOut
            End If
        End Get
    End Property

    Public Shared Property consMensajeRealizarProgramacionBlackOut() As String
        Set(ByVal value As String)
            _consMensajeRealizarProgramacionBlackOut = value
        End Set
        Get
            If (_consMensajeRealizarProgramacionBlackOut Is Nothing) Then
                ObtenerParametroPorta(ConsParametrosPortabildiadCP)
                Return _consMensajeRealizarProgramacionBlackOut
            Else
                Return _consMensajeRealizarProgramacionBlackOut
            End If
        End Get
    End Property
#End Region
    'INI: PROY-140262 BLACKOUT

    Public Shared Function ObtenerParametro(ByVal strCodGrupo As Int64) As Boolean
        bolParamProteccionMovil = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))

                If strCodigo = "1" Then
                    _strCodServicioPM = strValor
                ElseIf (strCodigo = "26") Then
                    _strClaseBolFactCanje = strValor
                ElseIf (strCodigo = "27") Then
                    _strEstadoErrorCanVIn = strValor
                ElseIf (strCodigo = "31") Then
                    _strEstadoError = strValor
                ElseIf (strCodigo = "32") Then
                    _strEstadoRespuesta = strValor
                ElseIf (strCodigo = "33") Then
                    _strEstadoRespuestaCanje = strValor
                ElseIf (strCodigo = "48") Then
                    _strProteccionMovilNoPagada = strValor
                ElseIf (strCodigo = "49") Then
                    _strProteccionMovilError = strValor
                ElseIf (strCodigo = "50") Then
                    _strProteccionMovilPendiente = strValor
                ElseIf (strCodigo = "52") Then
                    _strRemitentePM = strValor
                ElseIf (strCodigo = "53") Then
                    _strDestinatarioPM = strValor
                ElseIf (strCodigo = "54") Then
                    _strAsuntoPM = strValor
                ElseIf (strCodigo = "56") Then
                    _strFlagCorreoPM = strValor
                ElseIf (strCodigo = "69") Then
                    _strFlagdePlantillaCanje = strValor
                ElseIf (strCodigo = "70") Then
                    _strUserProceso = strValor
                ElseIf (strCodigo = "71") Then
                    _strNotasCanje = strValor
                ElseIf (strCodigo = "72") Then
                    _strSubClaseCanje = strValor
                ElseIf (strCodigo = "73") Then
                    _strEstadoVentaIndividual = strValor
                ElseIf (strCodigo = "74") Then
                    _strNotasVI = strValor
                ElseIf (strCodigo = "75") Then
                    _strSubClaseVI = strValor
                ElseIf (strCodigo = "76") Then
                    _strEstadoCanje = strValor
                ElseIf (strCodigo = "77") Then
                    _strFlagdePlantillaVIndividual = strValor
                ElseIf (strCodigo = "78") Then
                    _strOperacionVenta = strValor
                ElseIf (strCodigo = "81") Then ''Mensaje Error Pago
                    _strMensajePagoPM = strValor
                ElseIf (strCodigo = "82") Then
                    _strMensajeAnulacionPM = strValor ' Mensaje Error Anulacion 
                    'PROY-24724 - Iteracion 2 Siniestros -INI 
                ElseIf (strCodigo = "87") Then
                    _strFlagSiniestro = strValor
                ElseIf (strCodigo = "88") Then
                    _strEstdPendienteSiniestro = strValor 'PROY-24724 - Iteracion 2  
                ElseIf (strCodigo = "89") Then
                    _strEstdAprobadoSiniestro = strValor
                ElseIf (strCodigo = "90") Then
                    _strEstdFinalizadoPagoSiniestro = strValor
                ElseIf (strCodigo = "92") Then
                    _strCodMaterialSiniestro = strValor
                ElseIf (strCodigo = "94") Then
                    _strFlagPlantillaSiniestro = strValor
                ElseIf (strCodigo = "95") Then
                    _strTipiEstadoSiniestro = strValor
                ElseIf (strCodigo = "96") Then
                    _strTipiSubClaseSiniestro = strValor
                ElseIf (strCodigo = "97") Then
                    _strAsuntoSiniestro = strValor
                ElseIf (strCodigo = "98") Then
                    _strMensajeSiniestro = strValor
                ElseIf (strCodigo = "115") Then
                    strNotasSiniestro = strValor
                    'PROY-24724 - Iteracion 2 Siniestros -FIN 
                End If
            Next
            bolParamProteccionMovil = True
        End If
    End Function

    'INI-936 - CNSO - Metodo para obtener los parametros de la INICIATIVA-936
    Public Shared Function ObtenerParametroMR(ByVal strCodGrupo As Int64) As Boolean
        bolParamEnvioRemesa = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))

                If strCodigo = "Key_AlertaCSEnvioRemesa" Then
                    _strAlertaCompServ = strValor
                End If
            Next
            bolParamEnvioRemesa = True
        End If
    End Function

    Public Shared Function ObtenerParametroPorta(ByVal strCodGrupo As Int64) As Boolean
        bolParamPorta = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty
        Dim strDescripcion As String = String.Empty
        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))
                strDescripcion = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_DESCRIPCION"))
                If (strDescripcion.IndexOf("[consEstadosCPPermitidos]") > -1) Then
                    _consEstadosCPPermitidos = strValor
                ElseIf (strDescripcion.IndexOf("[consEstadoDeudaCPPermitidos]") > -1) Then
                    _consEstadoDeudaCPPermitidos = strValor
                ElseIf (strDescripcion.IndexOf("[consEstadosSPPermitidos]") > -1) Then
                    _consEstadosSPPermitidos = strValor
                ElseIf (strDescripcion.IndexOf("[consMensajeNoProcedePago]") > -1) Then
                    _consMensPagoNoProce = strValor
                ElseIf (strDescripcion.IndexOf("[consEstadosSPNoPermitidos]") > -1) Then
                    _consEstadosSPNoPermitidos = strValor
                ElseIf (strDescripcion.IndexOf("[consEstadosNoSP]") > -1) Then
                    _consEstadosNoSP = strValor
                ElseIf (strDescripcion.IndexOf("[consTipoMensajeFinalesSP]") > -1) Then
                    _consTipoMensajeFinalesSP = strValor
                ElseIf (strDescripcion.IndexOf("[consTipoMensajeEsperandoRespuestaSP]") > -1) Then
                    _consTipoMensajeEsperandoRespuestaSP = strValor
                ElseIf (strDescripcion.IndexOf("[consTipoProductoPermitidos]") > -1) Then
                    _consTipoProductoPermitidosSP = strValor
                'PROY-140223
                ElseIf strCodigo.Equals("consDiasPermitidosPagoPEP") Then
                    _consDiasPermitidosPagoPEP = Funciones.CheckInt(strValor)
                    'INI: PROY-140262 BLACKOUT
                ElseIf strCodigo.Equals("consFlagBlackOut") Then
                    _consFlagBlackOut = Funciones.CheckInt(strValor)
                ElseIf strCodigo.Equals("consDocPermitidosBlackOut") Then
                    _consDocPermitidosBlackOut = Funciones.CheckStr(strValor)                    
                ElseIf strCodigo.Equals("consMensajePagoCACBlackOut") Then
                    _consMensajePagoCACBlackOut = Funciones.CheckStr(strValor)
                ElseIf strCodigo.Equals("consMensajeProcesarSolicitudPortabilidadBlackOut") Then
                    _consMensajeProcesarSolicitudPortabilidadBlackOut = Funciones.CheckStr(strValor)
                ElseIf strCodigo.Equals("consMensajeRealizarProgramacionBlackOut") Then
                    _consMensajeRealizarProgramacionBlackOut = Funciones.CheckStr(strValor)
                    'FIN: PROY-140262 BLACKOUT
                End If
            Next
            bolParamPorta = True
        End If
    End Function

    'PROY-26963 F2 FIN

 '//PROY-33188 IDEA-45658 INI
    Private Shared bolParamBuyBack As Boolean
    Public Shared _Key_Estado_Activo As String
    Public Shared _Key_Estado_Pagado As String
    Public Shared intParamGrupoBuyBack As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("CodigoGrupoBuyback")) '"33188"
    Public Shared Property Key_Estado_Activo() As String
        Set(ByVal value As String)
            _Key_Estado_Activo = value
        End Set
        Get
            If (bolParamBuyBack) Then
                Return _Key_Estado_Activo
            Else
                ObtenerParametroBuyBack(intParamGrupoBuyBack)
                Return _Key_Estado_Activo
            End If
        End Get
    End Property

    Public Shared Property Key_Estado_Pagado() As String
        Set(ByVal value As String)
            _Key_Estado_Pagado = value
        End Set
        Get
            If (bolParamBuyBack) Then
                Return _Key_Estado_Pagado
            Else
                ObtenerParametroBuyBack(intParamGrupoBuyBack)
                Return _Key_Estado_Pagado
            End If
        End Get
    End Property
    Public Shared Function ObtenerParametroBuyBack(ByVal intCodGrupo As Int64) As Boolean
        bolParamBuyBack = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(intCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty
        Dim strDescripcion As String = String.Empty
        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))
                strDescripcion = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_DESCRIPCION"))
                Select Case strCodigo
                    Case "Key_EstadoActivoBuyBack"
                        _Key_Estado_Activo = strValor
                        Exit Select
                    Case "Key_EstadoPagadoBuyBack"
                        Key_Estado_Pagado = strValor
                        Exit Select
                End Select
            Next
            bolParamBuyBack = True
        End If
    End Function
    '//PROY-33188 IDEA-45658 FIN

    'PROY-140590 IDEA142068 - INICIO
    Public Shared bolCampaniasSTBK As Boolean
    Public Shared ParamGrupoSTBK As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("ParamGrupoSTBK"))

    Public Shared _Key_campaniaActivasSTBK As String
    Public Shared _Key_modalidadVentaSTBK As String
    Public Shared _Key_TipoVentaSTBK As String
    Public Shared _Key_canalPermitidoSTBK As String
    Public Shared _Key_TipoProdPermitidoSTBK As String
    Public Shared _Key_OperacionPreSTBK As String
    Public Shared _Key_OperacionPosSTBK As String
    Public Shared _Key_FlagValidacCampana As String
    Public Shared _Key_MsgPagoCampania As String
    Public Shared _Key_MsgGrabarPago As String
    Public Shared _Key_PagoAppVentas As String
    Public Shared _Key_PagoSicar As String
    Public Shared _Key_OperacionPosPVU As String
    Public Shared _Key_OperacionPrePVU As String

    Public Shared Property Key_campaniaActivasSTBK() As String
        Set(ByVal Value As String)
            _Key_campaniaActivasSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_campaniaActivasSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_campaniaActivasSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_modalidadVentaSTBK() As String
        Set(ByVal Value As String)
            _Key_modalidadVentaSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_modalidadVentaSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_modalidadVentaSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_TipoVentaSTBK() As String
        Set(ByVal Value As String)
            _Key_TipoVentaSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_TipoVentaSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_TipoVentaSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_canalPermitidoSTBK() As String
        Set(ByVal Value As String)
            _Key_canalPermitidoSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_canalPermitidoSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_canalPermitidoSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_TipoProdPermitidoSTBK() As String
        Set(ByVal Value As String)
            _Key_TipoProdPermitidoSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_TipoProdPermitidoSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_TipoProdPermitidoSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_OperacionPreSTBK() As String
        Set(ByVal Value As String)
            _Key_OperacionPreSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_OperacionPreSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_OperacionPreSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_OperacionPosSTBK() As String
        Set(ByVal Value As String)
            _Key_OperacionPosSTBK = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_OperacionPosSTBK
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_OperacionPosSTBK
            End If
        End Get
    End Property

    Public Shared Property Key_FlagValidacCampana() As String
        Set(ByVal Value As String)
            _Key_FlagValidacCampana = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_FlagValidacCampana
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_FlagValidacCampana
            End If
        End Get
    End Property

    Public Shared Property Key_MsgPagoCampania() As String
        Set(ByVal Value As String)
            _Key_MsgPagoCampania = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_MsgPagoCampania
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_MsgPagoCampania
            End If
        End Get
    End Property

    Public Shared Property Key_MsgGrabarPago() As String
        Set(ByVal Value As String)
            _Key_MsgGrabarPago = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_MsgGrabarPago
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_MsgGrabarPago
            End If
        End Get
    End Property

    Public Shared Property Key_PagoAppVentas() As String
        Set(ByVal Value As String)
            _Key_PagoAppVentas = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_PagoAppVentas
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_PagoAppVentas
            End If
        End Get
    End Property

    Public Shared Property Key_PagoSicar() As String
        Set(ByVal Value As String)
            _Key_PagoSicar = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_PagoSicar
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_PagoSicar
            End If
        End Get
    End Property

    Public Shared Property Key_OperacionPosPVU() As String
        Set(ByVal Value As String)
            _Key_OperacionPosPVU = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_OperacionPosPVU
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_OperacionPosPVU
            End If
        End Get
    End Property

    Public Shared Property Key_OperacionPrePVU() As String
        Set(ByVal Value As String)
            _Key_OperacionPrePVU = Value
        End Set
        Get
            If (bolCampaniasSTBK) Then
                Return _Key_OperacionPrePVU
            Else
                ObtenerParametrosSTBK(ParamGrupoSTBK)
                Return _Key_OperacionPrePVU
            End If
        End Get
    End Property

    Public Shared Function ObtenerParametrosSTBK(ByVal strCodGrupo As Int64) As Boolean
        bolCampaniasSTBK = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))

                If (strCodigo = "Key_campaniaActivasSTBK") Then
                    _Key_campaniaActivasSTBK = strValor
                ElseIf (strCodigo = "Key_modalidadVentaSTBK") Then
                    _Key_modalidadVentaSTBK = strValor
                ElseIf (strCodigo = "Key_TipoVentaSTBK") Then
                    _Key_TipoVentaSTBK = strValor
                ElseIf (strCodigo = "Key_canalPermitidoSTBK") Then
                    _Key_canalPermitidoSTBK = strValor
                ElseIf (strCodigo = "Key_TipoProdPermitidoSTBK") Then
                    _Key_TipoProdPermitidoSTBK = strValor
                ElseIf (strCodigo = "Key_OperacionPreSTBK") Then
                    _Key_OperacionPreSTBK = strValor
                ElseIf (strCodigo = "Key_OperacionPosSTBK") Then
                    _Key_OperacionPosSTBK = strValor
                ElseIf (strCodigo = "Key_FlagValidacCampana") Then
                    _Key_FlagValidacCampana = strValor
                ElseIf (strCodigo = "Key_MsgPagoCampania") Then
                    _Key_MsgPagoCampania = strValor
                ElseIf (strCodigo = "Key_MsgGrabarPago") Then
                    _Key_MsgGrabarPago = strValor
                ElseIf (strCodigo = "Key_PagoAppVentas") Then
                    _Key_PagoAppVentas = strValor
                ElseIf (strCodigo = "Key_PagoSicar") Then
                    _Key_PagoSicar = strValor
                ElseIf (strCodigo = "Key_OperacionPosPVU") Then
                    _Key_OperacionPosPVU = strValor
                ElseIf (strCodigo = "Key_OperacionPrePVU") Then
                    _Key_OperacionPrePVU = strValor
                End If
            Next
            bolCampaniasSTBK = True
        End If
    End Function

    'PROY-140590 IDEA142068 - FIN

    'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel - INICIO
    Public Shared bolOpsitel As Boolean
    Public Shared ParamDocOsiptel As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("key_DocOsiptel"))

    Public Shared _Key_FlagFDActual As String
    Public Shared _Key_Tipo_Venta_Op As String
    Public Shared _Key_Operacion_Post_Op As String
    Public Shared _Key_Operacion_Pre_Op As String

    Public Shared Property Key_FlagFDActual() As String
        Set(ByVal Value As String)
            _Key_FlagFDActual = Value
        End Set
        Get
            If (bolOpsitel) Then
                Return _Key_FlagFDActual
            Else
                ObtenerParametrosOpsitel(ParamDocOsiptel)
                Return _Key_FlagFDActual
            End If
        End Get
    End Property

    Public Shared Property Key_Tipo_Venta_Op() As String
        Set(ByVal Value As String)
            _Key_Tipo_Venta_Op = Value
        End Set
        Get
            If (bolOpsitel) Then
                Return _Key_Tipo_Venta_Op
            Else
                ObtenerParametrosOpsitel(ParamDocOsiptel)
                Return _Key_Tipo_Venta_Op
            End If
        End Get
    End Property

    Public Shared Property Key_Operacion_Post_Op() As String
        Set(ByVal Value As String)
            _Key_Operacion_Post_Op = Value
        End Set
        Get
            If (bolOpsitel) Then
                Return _Key_Operacion_Post_Op
            Else
                ObtenerParametrosOpsitel(ParamDocOsiptel)
                Return _Key_Operacion_Post_Op
            End If
        End Get
    End Property

    Public Shared Property Key_Operacion_Pre_Op() As String
        Set(ByVal Value As String)
            _Key_Operacion_Pre_Op = Value
        End Set
        Get
            If (bolOpsitel) Then
                Return _Key_Operacion_Pre_Op
            Else
                ObtenerParametrosOpsitel(ParamDocOsiptel)
                Return _Key_Operacion_Pre_Op
            End If
        End Get
    End Property

    Public Shared Function ObtenerParametrosOpsitel(ByVal strCodGrupo As Int64) As Boolean
        bolOpsitel = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))

                If (strCodigo = "Key_FlagFDActual") Then
                    _Key_FlagFDActual = strValor
                ElseIf (strCodigo = "Key_Tipo_Venta_Op") Then
                    _Key_Tipo_Venta_Op = strValor
                ElseIf (strCodigo = "Key_Operacion_Post_Op") Then
                    _Key_Operacion_Post_Op = strValor
                ElseIf (strCodigo = "Key_Operacion_Pre_Op") Then
                    _Key_Operacion_Pre_Op = strValor
                End If
            Next
            bolOpsitel = True
        End If
    End Function
    'PROY-140623 - IDEA 142200 - Nuevo formato contratos Osiptel - FIN

    'INC000002644707 - INICIO
    Public Shared bolCE As Boolean
    Public Shared Key_ContratacionElectronica As Int64 = Convert.ToInt64(ConfigurationSettings.AppSettings("Key_ContratacionElectronica"))

    Public Shared _Key_EstadosNoPermitidosAnular As String
    Public Shared _Key_MensajeNoPermitidosAnular As String

    'Inicio - INI-936 - CNSO
    Public Shared Property strAlertaCompServ() As String
        Set(ByVal value As String)
            _strAlertaCompServ = value
        End Set
        Get
            If (bolParamEnvioRemesa) Then
                Return _strAlertaCompServ
            Else
            ObtenerParametroMR(Key_ParamGrupoINI936)
            Return _strAlertaCompServ
            End If
        End Get
    End Property
    'Fin - INI-936 - CNSO

    Public Shared Property Key_EstadosNoPermitidosAnular() As String
        Set(ByVal Value As String)
            _Key_EstadosNoPermitidosAnular = Value
        End Set
        Get
            If (bolCE) Then
                Return _Key_EstadosNoPermitidosAnular
            Else
                ObtenerParametrosCE(Key_ContratacionElectronica)
                Return _Key_EstadosNoPermitidosAnular
            End If
        End Get
    End Property

    Public Shared Property Key_MensajeNoPermitidosAnular() As String
        Set(ByVal Value As String)
            _Key_MensajeNoPermitidosAnular = Value
        End Set
        Get
            If (bolCE) Then
                Return _Key_MensajeNoPermitidosAnular
            Else
                ObtenerParametrosCE(Key_ContratacionElectronica)
                Return _Key_MensajeNoPermitidosAnular
            End If
        End Get
    End Property


    Public Shared Function ObtenerParametrosCE(ByVal strCodGrupo As Int64) As Boolean
        bolCE = False
        Dim dsParametros As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(strCodGrupo)
        Dim i As Integer
        Dim strCodigo As String = String.Empty
        Dim strValor As String = String.Empty

        If Not IsNothing(dsParametros) Then
            For i = 0 To dsParametros.Tables(0).Rows.Count - 1
                strCodigo = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR1"))
                strValor = CheckStr(dsParametros.Tables(0).Rows(i).Item("PARAV_VALOR"))

                If (strCodigo = "Key_EstadosNoPermitidosAnular") Then
                    _Key_EstadosNoPermitidosAnular = strValor
                ElseIf (strCodigo = "Key_MensajeNoPermitidosAnular") Then
                    _Key_MensajeNoPermitidosAnular = strValor
                End If
            Next
            bolCE = True
        End If
    End Function
    'INC000002644707 - FIN
End Class
'PROY-24724-IDEA-28174 - FIN CLASE