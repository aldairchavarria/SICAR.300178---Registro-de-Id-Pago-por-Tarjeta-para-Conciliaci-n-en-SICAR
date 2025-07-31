Imports COM_SIC_Activaciones
Imports COM_SIC_Seguridad
Imports System.Configuration
Imports COM_SIC_Entidades
Public Class WebComunes

#Region " PROY-31836 | Mejora de Procesos Postventa del servicio Proteccion Movil | Bryan Chumbes Lizarraga "
    Public Shared Sub CargarAppSettings()

        Dim objFileLog As New SrvPago_Log
        Dim nameFile As String = "CargarVariablesGlobales"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = "ParametrosGlobales"

        Try

            Dim key_ParanGrupoProteccionMovil As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales]", strIdentifyLog))

            key_ParanGrupoProteccionMovil = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoProteccionMovil").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoProteccionMovil))

            Dim listParametrosProteccionMovil As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoProteccionMovil)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosProteccionMovil.Count()))

            For Each oItem As BEParametros In listParametrosProteccionMovil
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_CantIntentosPagoRenovacion"
                        ReadKeySettings.Key_CantIntentosPagoRenovacion = valor
                    Case "Key_SubClaseRenovacionTipificaWS"
                        ReadKeySettings.Key_SubClaseRenovacionTipificaWS = valor
                    Case "Key_FlagPlantillaRenoTipificaWS"
                        ReadKeySettings.Key_FlagPlantillaRenoTipificaWS = valor
                    Case "Key_UsuarioTipificacionWS"
                        ReadKeySettings.Key_UsuarioTipificacionWS = valor
                    Case "Key_MotivoCancelTipificacionWS"
                        ReadKeySettings.Key_MotivoCancelTipificacionWS = valor
                    Case "Key_NotaExitoTipificacionWS"
                        ReadKeySettings.Key_NotaExitoTipificacionWS = valor
                    Case "Key_NotaErrorTipificacionWS"
                        ReadKeySettings.Key_NotaErrorTipificacionWS = valor
                    Case "Key_TipoOperRenoTipificacion"
                        ReadKeySettings.Key_TipoOperRenoTipificacion = valor
                    Case "Key_TipoOperRenovacionPostago"
                        ReadKeySettings.Key_TipoOperRenovacionPostago = valor
                End Select
            Next


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CantIntentosPagoRenovacion] [{1}]", strIdentifyLog, ReadKeySettings.Key_CantIntentosPagoRenovacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_SubClaseRenovacionTipificaWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_SubClaseRenovacionTipificaWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagPlantillaRenoTipificaWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_FlagPlantillaRenoTipificaWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_UsuarioTipificacionWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_UsuarioTipificacionWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MotivoCancelTipificacionWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_MotivoCancelTipificacionWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_NotaExitoTipificacionWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_NotaExitoTipificacionWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_NotaErrorTipificacionWS] [{1}]", strIdentifyLog, ReadKeySettings.Key_NotaErrorTipificacionWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOperRenoTipificacion] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOperRenoTipificacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOperRenovacionPostago] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOperRenovacionPostago))

        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurrió un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales]", strIdentifyLog))
        End Try


        '//PROY-140379 INI
        Try

            Dim key_ParanGrupoAppleWatch As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales AppleWatch ]", strIdentifyLog))
            key_ParanGrupoAppleWatch = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoAppleWatch").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoAppleWatch))
            Dim listParametrosAppleWatch As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoAppleWatch)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosAppleWatch.Count()))


            Dim sbMat As New System.Text.StringBuilder
            For Each oItem As BEParametros In listParametrosAppleWatch
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_requestBodyIOT"
                        ReadKeySettings.Key_requestBodyIOT = valor
                    Case "Key_serviceNameSW"
                        ReadKeySettings.Key_serviceNameSW = valor
                    Case "Key_requestConsultaIot_ListaOpc"
                        ReadKeySettings.Key_requestConsultaIot_ListaOpc = valor
                    Case "Key_consultarProcesarIotActualizar"
                        ReadKeySettings.Key_consultarProcesarIotActualizar = valor
                    Case "Key_consultarProcesarIotDarBaja"
                        ReadKeySettings.Key_consultarProcesarIotDarBaja = valor
                    Case "Key_TipoOper_Devol"
                        ReadKeySettings.Key_TipoOper_Devol = valor
                    Case "Key_HeaderResquest"
                        ReadKeySettings.Key_HeaderResquest = valor
                End Select
                If Funciones.CheckStr(valor1).IndexOf("Key_MaterialPermitidoSW") > -1 Then
                    sbMat.Append(valor)
                End If
            Next
            ReadKeySettings.Key_MaterialPermitidoSW = sbMat.ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_requestBodyIOT] [{1}]", strIdentifyLog, ReadKeySettings.Key_requestBodyIOT))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MaterialPermitidoSW] [{1}]", strIdentifyLog, ReadKeySettings.Key_MaterialPermitidoSW))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_serviceNameSW] [{1}]", strIdentifyLog, ReadKeySettings.Key_serviceNameSW))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_requestConsultaIot_ListaOpc] [{1}]", strIdentifyLog, ReadKeySettings.Key_requestConsultaIot_ListaOpc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_consultarProcesarIotActualizar] [{1}]", strIdentifyLog, ReadKeySettings.Key_consultarProcesarIotActualizar))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_consultarProcesarIotDarBaja] [{1}]", strIdentifyLog, ReadKeySettings.Key_consultarProcesarIotDarBaja))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOper_Devol] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOper_Devol))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_HeaderResquest] [{1}]", strIdentifyLog, ReadKeySettings.Key_HeaderResquest))

        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales Apple Watch]", strIdentifyLog))
        End Try
        '//PROY-140379 FIN

        Try

            Dim key_AutomatizacionDelivery As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales]", strIdentifyLog))

            key_AutomatizacionDelivery = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_AutomatizacionDelivery").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_AutomatizacionDelivery))

            Dim listParametrosAutomatizacionDelivery As ArrayList = New clsConsultaPvu().ConsultaParametros(key_AutomatizacionDelivery)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosAutomatizacionDelivery.Count()))

            For Each oItem As BEParametros In listParametrosAutomatizacionDelivery
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_FlagCajeroVirtualAutomatico"
                        KeySettings.Key_FlagCajeroVirtualAutomatico = valor
                    Case "Key_TipoVentaPermitido"
                        KeySettings.Key_TipoVentaPermitido = valor
                    Case "Key_TipoOperacionPostPermitido"
                        KeySettings.Key_TipoOperacionPostPermitido = valor
                    Case "Key_TipoOperacionPrePermitido"
                        KeySettings.Key_TipoOperacionPrePermitido = valor
                    Case "Key_TipoProductoPostPermitido"
                        KeySettings.Key_TipoProductoPostPermitido = valor
                    Case "Key_TipoProductoPrePermitido"
                        KeySettings.Key_TipoProductoPrePermitido = valor
                    Case "Key_FlagPermitirProteccionMovil"
                        KeySettings.Key_FlagPermitirProteccionMovil = valor
                    Case "Key_FlagPermitirBuyBack"
                        KeySettings.Key_FlagPermitirBuyBack = valor
                    Case "Key_CodigoEstadoRecibidoSICAR"
                        KeySettings.Key_CodigoEstadoRecibidoSICAR = valor
                    Case "Key_CodigoRespuestaExito"
                        KeySettings.Key_CodigoRespuestaExito = valor
                    Case "Key_MsjRptaExitoRecibidoSICAR"
                        KeySettings.Key_MsjRptaExitoRecibidoSICAR = valor
                    Case "Key_MsjRptaErrorRecibidoSICAR"
                        KeySettings.Key_MsjRptaErrorRecibidoSICAR = valor
                    Case "Key_IdentificarNoCoincide"
                        KeySettings.Key_IdentificarNoCoincide = valor
                    Case "Key_PedidoNoRetornaDatos"
                        KeySettings.Key_PedidoNoRetornaDatos = valor
                    Case "Key_ErrorCajeroVirtual"
                        KeySettings.Key_ErrorCajeroVirtual = valor
                    Case "Key_CodigoEstadoError"
                        KeySettings.Key_CodigoEstadoError = valor
                    Case "Key_CodigoRespuestaError"
                        KeySettings.Key_CodigoRespuestaError = valor
                    Case "Key_MsjRptaErrorTipoVenta"
                        KeySettings.Key_MsjRptaErrorTipoVenta = valor
                    Case "Key_MsjRptaErrorTipoOperacion"
                        KeySettings.Key_MsjRptaErrorTipoOperacion = valor
                    Case "Key_MsjRptaErrorTipoProducto"
                        KeySettings.Key_MsjRptaErrorTipoProducto = valor
                    Case "Key_MsjRptaErrorPedidoConPM"
                        KeySettings.Key_MsjRptaErrorPedidoConPM = valor
                    Case "Key_MsjRptaErrorPedidoEsPM"
                        KeySettings.Key_MsjRptaErrorPedidoEsPM = valor
                    Case "Key_MsjRptaPedidoBuyBack"
                        KeySettings.Key_MsjRptaPedidoBuyBack = valor
                    Case "Key_MsjRptaPedidoSinDatos"
                        KeySettings.Key_MsjRptaPedidoSinDatos = valor
                    Case "Key_UsuarioNoCuentaConAcceso"
                        KeySettings.Key_UsuarioNoCuentaConAcceso = valor
                    Case "Key_CuentaENoEnviada"
                        KeySettings.Key_CuentaENoEnviada = valor
                    Case "Key_ReposicionPostpago"
                        KeySettings.Key_ReposicionPostpago = valor
                    Case "Key_Repo_Reno_RenoPackPrepago"
                        KeySettings.Key_Repo_Reno_RenoPackPrepago = valor
                    Case "Key_CodigoEstadoPagoExitoSICAR"
                        KeySettings.Key_CodigoEstadoPagoExitoSICAR = valor
                    Case "Key_MsjRptaExitoPagoSICAR"
                        KeySettings.Key_MsjRptaExitoPagoSICAR = valor
                    Case "Key_FlagNoReintentarPago"
                        KeySettings.Key_FlagNoReintentarPago = valor
                    Case "Key_FlagReintentarPago"
                        KeySettings.Key_FlagReintentarPago = valor
                    Case "Key_MsjErrorValidacionDeliverySICAR"
                        KeySettings.Key_MsjErrorValidacionDeliverySICAR = valor
                    Case "Key_MsjErrorGeneralSICAR"
                        KeySettings.Key_MsjErrorGeneralSICAR = valor
                    Case "Key_VentasVariasPostpago"
                        KeySettings.Key_VentasVariasPostpago = valor
                    Case "Key_ClaseFacturaPermitido"
                        KeySettings.Key_ClaseFacturaPermitido = valor
                    Case "Key_MsjRptaErrorClaseFactura"
                        KeySettings.Key_MsjRptaErrorClaseFactura = valor
                    Case "Key_MsjRptaErrorEstadoPortabilidad"
                        KeySettings.Key_MsjRptaErrorEstadoPortabilidad = valor
                    Case "Key_MsjRptaExitoPagoRASICAR"
                        KeySettings.Key_MsjRptaExitoPagoRASICAR = valor
                    Case "Key_CodigoUsuarioGenericoPago"
                        KeySettings.Key_CodigoUsuarioGenericoPago = valor

                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagCajeroVirtualAutomatico] [{1}]", strIdentifyLog, KeySettings.Key_FlagCajeroVirtualAutomatico))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoVentaPermitido] [{1}]", strIdentifyLog, KeySettings.Key_TipoVentaPermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOperacionPostPermitido] [{1}]", strIdentifyLog, KeySettings.Key_TipoOperacionPostPermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOperacionPrePermitido] [{1}]", strIdentifyLog, KeySettings.Key_TipoOperacionPrePermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoProductoPostPermitido] [{1}]", strIdentifyLog, KeySettings.Key_TipoProductoPostPermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoProductoPrePermitido] [{1}]", strIdentifyLog, KeySettings.Key_TipoProductoPrePermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagPermitirProteccionMovil] [{1}]", strIdentifyLog, KeySettings.Key_FlagPermitirProteccionMovil))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagPermitirBuyBack] [{1}]", strIdentifyLog, KeySettings.Key_FlagPermitirBuyBack))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoEstadoRecibidoSICAR] [{1}]", strIdentifyLog, KeySettings.Key_CodigoEstadoRecibidoSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoRespuestaExito] [{1}]", strIdentifyLog, KeySettings.Key_CodigoRespuestaExito))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaExitoRecibidoSICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaExitoRecibidoSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorRecibidoSICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorRecibidoSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_IdentificarNoCoincide] [{1}]", strIdentifyLog, KeySettings.Key_IdentificarNoCoincide))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_PedidoNoRetornaDatos] [{1}]", strIdentifyLog, KeySettings.Key_PedidoNoRetornaDatos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ErrorCajeroVirtual] [{1}]", strIdentifyLog, KeySettings.Key_ErrorCajeroVirtual))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoEstadoError] [{1}]", strIdentifyLog, KeySettings.Key_CodigoEstadoError))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoRespuestaError] [{1}]", strIdentifyLog, KeySettings.Key_CodigoRespuestaError))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorTipoVenta] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorTipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorTipoOperacion] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorTipoOperacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorTipoProducto] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorTipoProducto))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorPedidoConPM] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorPedidoConPM))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorPedidoEsPM] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorPedidoEsPM))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaPedidoBuyBack] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaPedidoBuyBack))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaPedidoSinDatos] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaPedidoSinDatos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_UsuarioNoCuentaConAcceso] [{1}]", strIdentifyLog, KeySettings.Key_UsuarioNoCuentaConAcceso))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CuentaENoEnviada] [{1}]", strIdentifyLog, KeySettings.Key_CuentaENoEnviada))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ReposicionPostpago] [{1}]", strIdentifyLog, KeySettings.Key_ReposicionPostpago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_Repo_Reno_RenoPackPrepago] [{1}]", strIdentifyLog, KeySettings.Key_Repo_Reno_RenoPackPrepago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoEstadoPagoExitoSICAR] [{1}]", strIdentifyLog, KeySettings.Key_CodigoEstadoPagoExitoSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaExitoPagoSICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaExitoPagoSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagNoReintentarPago] [{1}]", strIdentifyLog, KeySettings.Key_FlagNoReintentarPago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagReintentarPago] [{1}]", strIdentifyLog, KeySettings.Key_FlagReintentarPago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjErrorValidacionDeliverySICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjErrorValidacionDeliverySICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjErrorGeneralSICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjErrorGeneralSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_VentasVariasPostpago] [{1}]", strIdentifyLog, KeySettings.Key_VentasVariasPostpago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ClaseFacturaPermitido] [{1}]", strIdentifyLog, KeySettings.Key_ClaseFacturaPermitido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorClaseFactura] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorClaseFactura))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaErrorEstadoPortabilidad] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaErrorEstadoPortabilidad))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjRptaExitoPagoRASICAR] [{1}]", strIdentifyLog, KeySettings.Key_MsjRptaExitoPagoRASICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodigoUsuarioGenericoPago] [{1}]", strIdentifyLog, KeySettings.Key_CodigoUsuarioGenericoPago))


        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurrió un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales]", strIdentifyLog))
        End Try

        'INI: INICIATIVA-219 | CBIO
        Try
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            Dim key_ParamGrupoCBIO As Int64 = 0
            key_ParamGrupoCBIO = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParamGrupoCBIO").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParamGrupoCBIO))

            Dim listParametrosCBIO As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParamGrupoCBIO)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosCBIO.Count()))

            For Each oItem As BEParametros In listParametrosCBIO
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "key_flagCBIO"
                        KeySettings.key_flagCBIO = valor
                    Case "key_DescricpionCBIO"
                        KeySettings.key_DescricpionCBIO = valor
                    Case "key_TipoOperacionAltaCBIO"
                        KeySettings.key_TipoOperacionAltaCBIO = valor
                    Case "key_TipoOperacionMigracionAltaCBIO"
                        KeySettings.key_TipoOperacionMigracionAltaCBIO = valor
                    Case "key_TipoOperacionReposicionCBIO"
                        KeySettings.key_TipoOperacionReposicionCBIO = valor
                    Case "key_TipoOperacionRenovacionMasReposicionCBIO"
                        KeySettings.key_TipoOperacionRenovacionMasReposicionCBIO = valor
                    Case "key_PoIDProteccionMovil"
                        KeySettings.key_PoIDProteccionMovil = valor
                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_flagCBIO] [{1}]", strIdentifyLog, KeySettings.key_flagCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_DescricpionCBIO] [{1}]", strIdentifyLog, KeySettings.key_DescricpionCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionAltaCBIO] [{1}]", strIdentifyLog, KeySettings.key_TipoOperacionAltaCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionMigracionAltaCBIO] [{1}]", strIdentifyLog, KeySettings.key_TipoOperacionMigracionAltaCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionReposicionCBIO] [{1}]", strIdentifyLog, KeySettings.key_TipoOperacionReposicionCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_PoIDProteccionMovil] [{1}]", strIdentifyLog, KeySettings.key_PoIDProteccionMovil))
        Catch ex As Exception

        End Try
        'FIN: INICIATIVA-219 | CBIO

        'JRM PROY 140589
        Try
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            Dim key_ParamGrupoCobroDelivery As Int64 = 0
            key_ParamGrupoCobroDelivery = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoCobroDelivery").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParamGrupoCobroDelivery))

            Dim listParametrosCobroDelivery As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParamGrupoCobroDelivery)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosCobroDelivery.Count()))

            For Each oItem As BEParametros In listParametrosCobroDelivery
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_K_PEDIC_CODTIPOOPERACION"
                        KeySettings.Key_K_PEDIC_CODTIPOOPERACION = valor
                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_K_PEDIC_CODTIPOOPERACION] [{1}]", strIdentifyLog, KeySettings.Key_K_PEDIC_CODTIPOOPERACION))
        Catch ex As Exception

        End Try
        'JRM PROY 140589

    End Sub
#End Region


    Public Function ValidarModalidadVentaContratoCode(ByVal strNumSEC As String) As Boolean

        Dim objFileLog As New SrvPago_Log
        Dim nameFile As String = "CargarVariablesGlobales"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Dim strIdentifyLog As String = strNumSEC
        Dim objActivaciones As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Validar Modalidad Venta Contrato Code- INICIO")
            ValidarModalidadVentaContratoCode = False

            lista = objActivaciones.ConsultaSolicitud_NROSEC(Funciones.CheckStr(strNumSEC))

            If Not lista Is Nothing And lista.Count > 0 Then
                For Each item As COM_SIC_Activaciones.clsSolicitudPersona In lista
                    If Funciones.CheckStr(item.MODALIDAD_VENTA).Equals("2") Then
                        ValidarModalidadVentaContratoCode = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  PROY-140360-IDEA-46301 Modalidad de Venta : " & Funciones.CheckStr(item.MODALIDAD_VENTA))
                        Exit For
                    End If
                Next
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 No hay datos de la sec")
            End If
        Catch ex As Exception
            ValidarModalidadVentaContratoCode = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Error al validar Modalidad ContratoCode : " & ex.Message.ToString)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " IPROY-140360-IDEA-46301 Validar Modalidad Venta ContratoCode - FIN")
        End Try

        Return ValidarModalidadVentaContratoCode
    End Function

End Class
