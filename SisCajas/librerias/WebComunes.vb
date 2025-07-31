Imports COM_SIC_Activaciones
Imports COM_SIC_Seguridad
Imports System.Configuration
Public Class WebComunes

#Region " PROY-31836 | Mejora de Procesos Postventa del servicio Proteccion Movil | Bryan Chumbes Lizarraga "
    Public Shared Sub CargarAppSettings()

        Dim objFileLog As New SICAR_Log
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

            'INI: INICIATIVA-219 | CBIO
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
                        ReadKeySettings.key_flagCBIO = valor
                    Case "key_DescricpionCBIO"
                        ReadKeySettings.key_DescricpionCBIO = valor
                    Case "key_TipoOperacionAltaCBIO"
                        ReadKeySettings.key_TipoOperacionAltaCBIO = valor
                    Case "key_TipoOperacionMigracionAltaCBIO"
                        ReadKeySettings.key_TipoOperacionMigracionAltaCBIO = valor
                    Case "key_TipoOperacionReposicionCBIO"
                        ReadKeySettings.key_TipoOperacionReposicionCBIO = valor
                    Case "key_TipoOperacionRenovacionMasReposicionCBIO"
                        ReadKeySettings.key_TipoOperacionRenovacionMasReposicionCBIO = valor
                    Case "key_PoIDProteccionMovil"
                        ReadKeySettings.key_PoIDProteccionMovil = valor
					Case "key_FlagMejoraRenovacion"
                        ReadKeySettings.key_FlagMejoraRenovacion = valor
                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_flagCBIO] [{1}]", strIdentifyLog, ReadKeySettings.key_flagCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_DescricpionCBIO] [{1}]", strIdentifyLog, ReadKeySettings.key_DescricpionCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionAltaCBIO] [{1}]", strIdentifyLog, ReadKeySettings.key_TipoOperacionAltaCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionMigracionAltaCBIO] [{1}]", strIdentifyLog, ReadKeySettings.key_TipoOperacionMigracionAltaCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_TipoOperacionReposicionCBIO] [{1}]", strIdentifyLog, ReadKeySettings.key_TipoOperacionReposicionCBIO))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_PoIDProteccionMovil] [{1}]", strIdentifyLog, ReadKeySettings.key_PoIDProteccionMovil))
			objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [key_FlagMejoraRenovacion] [{1}]", strIdentifyLog, ReadKeySettings.key_FlagMejoraRenovacion))
            'FIN: INICIATIVA-219 | CBIO

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


        'JLOPETAS - PROY 140589 - INI
        Try

            Dim key_ParanGrupoCobroDelivery As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Costo Delivery]", strIdentifyLog))
            key_ParanGrupoCobroDelivery = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoCobroDelivery").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoCobroDelivery))
            Dim listParametrosCobroDelivery As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoCobroDelivery)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosCobroDelivery.Count()))


            Dim sbMat As New System.Text.StringBuilder
            For Each oItem As BEParametros In listParametrosCobroDelivery
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_Msj_Costo_DLV"
                        ReadKeySettings.Key_Msj_Costo_DLV = valor
                    Case "Key_Msj_AnuCostdlv"
                        ReadKeySettings.Key_Msj_AnuCostdlv = valor
                    Case "Key_Msj_Generico_Anula_DLV"
                        ReadKeySettings.Key_Msj_Generico_Anula_DLV = valor
                    Case "Key_MensajePorta_envio"
                        ReadKeySettings.Key_MensajePorta_envio = valor
                    Case "Key_MensajePorta_aprobacion"
                        ReadKeySettings.Key_MensajePorta_aprobacion = valor
                End Select

            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_Msj_Costo_DLV] [{1}]", strIdentifyLog, ReadKeySettings.Key_Msj_Costo_DLV))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_Msj_AnuCostdlv] [{1}]", strIdentifyLog, ReadKeySettings.Key_Msj_AnuCostdlv))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_Msj_Generico_Anula_DLV] [{1}]", strIdentifyLog, ReadKeySettings.Key_Msj_Generico_Anula_DLV))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MensajePorta_envio] [{1}]", strIdentifyLog, ReadKeySettings.Key_MensajePorta_envio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MensajePorta_aprobacion] [{1}]", strIdentifyLog, ReadKeySettings.Key_MensajePorta_aprobacion))
        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales Costo Delivery]", strIdentifyLog))
        End Try
        'JLOPETAS - PROY 140589 - FIN

        'PROY-140662 - DLV F4 - INI
        Try

            Dim key_ParanGrupoDLVF4 As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales DLV - F4 ]", strIdentifyLog))
            key_ParanGrupoDLVF4 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_DeliveryFase4").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoDLVF4))
            Dim listParametrosDLVF4 As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoDLVF4)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosDLVF4.Count()))


            For Each oItem As BEParametros In listParametrosDLVF4
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_ModoCargaPropiedadesUpdate"
                        ReadKeySettings.Key_ModoCargaPropiedadesUpdate = valor
                    Case "Key_TipoCarga"
                        ReadKeySettings.Key_TipoCarga = valor
                    Case "Key_ConfiguracionSOT"
                        ReadKeySettings.Key_ConfiguracionSOT = valor
                    Case "Key_ConfiguracionInventario"
                        ReadKeySettings.Key_ConfiguracionInventario = valor
                    Case "Key_TipoComando"
                        ReadKeySettings.Key_TipoComando = valor
                    Case "Key_EncriptBase64DP"
                        ReadKeySettings.Key_EncriptBase64DP = valor
                    Case "Key_MsjOrdenTOA"
                        ReadKeySettings.Key_MsjOrdenTOA = valor
                    Case "Key_MsjOrdenTOACaido"
                        ReadKeySettings.Key_MsjOrdenTOACaido = valor
                    Case "Key_EstadoOrdenTOA"
                        ReadKeySettings.Key_EstadoOrdenTOA = valor
                    Case "Key_EstadoAuditTOA"
                        ReadKeySettings.Key_EstadoAuditTOA = valor
                    Case "Key_flagTOA"
                        ReadKeySettings.Key_flagTOA = valor
                End Select
            Next

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, ex.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ModoCargaPropiedadesUpdate] [{1}]", strIdentifyLog, ReadKeySettings.Key_ModoCargaPropiedadesUpdate))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoCarga] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoCarga))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ConfiguracionSOT] [{1}]", strIdentifyLog, ReadKeySettings.Key_ConfiguracionSOT))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ConfiguracionInventario] [{1}]", strIdentifyLog, ReadKeySettings.Key_ConfiguracionInventario))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoComando] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoComando))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_EncriptBase64DP] [{1}]", strIdentifyLog, ReadKeySettings.Key_EncriptBase64DP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjOrdenTOA] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjOrdenTOA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjOrdenTOACaido] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjOrdenTOACaido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_EstadoOrdenTOA] [{1}]", strIdentifyLog, ReadKeySettings.Key_EstadoOrdenTOA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_EstadoAuditTOA] [{1}]", strIdentifyLog, ReadKeySettings.Key_EstadoAuditTOA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_flagTOA] [{1}]", strIdentifyLog, ReadKeySettings.Key_flagTOA))

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales DLV - F4 ]", strIdentifyLog))
        End Try



        Try
            Dim key_ParanGrupoDLVF2 As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales DLV - F2 ]", strIdentifyLog))
            key_ParanGrupoDLVF2 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("ParanGrupoDelivery").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoDLVF2))
            Dim listParametrosDLVF2 As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoDLVF2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosDLVF2.Count()))


            For Each oItem As BEParametros In listParametrosDLVF2
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_PDVVentaExpress"
                        ReadKeySettings.Key_PDVVentaExpress = valor
                    Case "Key_TipoOrdenRegular"
                        ReadKeySettings.Key_TipoOrdenRegular = valor
                    Case "Key_TipoOrdenExpress"
                        ReadKeySettings.Key_TipoOrdenExpress = valor
                End Select
            Next

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, ex.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_PDVVentaExpress] [{1}]", strIdentifyLog, ReadKeySettings.Key_PDVVentaExpress))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOrdenRegular] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOrdenRegular))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOrdenExpress] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOrdenExpress))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales DLV - F2 ]", strIdentifyLog))
        End Try

        'PROY-140662 - DLV F4 - FIN


'INICIATIVA 712 Cobro Anticipado INI
        Try

            Dim Key_ParanGrupoCAN As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Globales Cobro Anticipado ]", strIdentifyLog))
            Key_ParanGrupoCAN = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ParanCobroAnticipadoIsntalacion").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, Key_ParanGrupoCAN))
            Dim listParametrosCAN As ArrayList = New clsConsultaPvu().ConsultaParametros(Key_ParanGrupoCAN)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosCAN.Count()))


            Dim sbMat As New System.Text.StringBuilder
            For Each oItem As BEParametros In listParametrosCAN
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_FlagAplicaCAI"
                        ReadKeySettings.ConsFlagAplicaCAI = valor
                    Case "Key_TiempoSecPendientePagoLink"
                        ReadKeySettings.ConsTiempoSecPendientePagoLink = valor
                    Case "Key_Consumer"
                        ReadKeySettings.ConsConsumerConsultaPA = valor
                    Case "Key_Country"
                        ReadKeySettings.ConsCountryConsultaPA = valor
                    Case "Key_Dispositivo"
                        ReadKeySettings.ConsDispositivoConsultaPA = valor
                    Case "Key_Language"
                        ReadKeySettings.ConsLanguageConsultaPA = valor
                    Case "Key_Modulo"
                        ReadKeySettings.ConsModuloConsultaPA = valor
                    Case "Key_MsgType"
                        ReadKeySettings.ConsMsgTypeConsultaPA = valor
                    Case "Key_Operation"
                        ReadKeySettings.ConsOperationConsultaPA = valor
                    Case "Key_System"
                        ReadKeySettings.ConsSystemConsultaPA = valor
                    Case "Key_wsIp"
                        ReadKeySettings.ConsWsIpConsultaPA = valor
                    Case "Key_PDV_TELEVENTAS"
                        ReadKeySettings.ConsCodigoPDVTeleventas = valor
                    Case "Key_MsjValidacionExisteSecPendientePago"
                        ReadKeySettings.ConsMsjValidacionSecPendPagoLink = valor
                    Case "Key_CodPdvPermitidosCAI"
                        ReadKeySettings.ConsPDVPermitidosCAI = valor
                    Case "Key_MsjValidacionCamposObligatoriosCAI"
                        ReadKeySettings.ConsMsjValidacionSubFormularioCAI = valor
                    Case "Key_MsjValidacionExisteSecPendientePagoEnVentaExpress"
                        ReadKeySettings.ConsMsjValidacionSecPendPagoLinkEnVentaExp = valor
                    Case "Key_MsjSMS"
                        ReadKeySettings.ConsMsjSMS = valor
                    Case "Key_MsjValidacionSinNumeroTelefono"
                        ReadKeySettings.Key_MsjValidacionSinNumeroTelefono = valor
                    Case "Key_TipoOperacionSICAR"
                        ReadKeySettings.Key_TipoOperacionSICAR = valor
                    Case "Key_CodMaterialPermitidosSICAR"
                        ReadKeySettings.Key_CodMaterialPermitidosSICAR = valor
                    Case "Key_MsjSecPendienteEvaluacionSICAR"
                        ReadKeySettings.Key_MsjSecPendienteEvaluacionSICAR = valor
                    Case "Key_MsjSecPendienteAprobacionSICAR"
                        ReadKeySettings.Key_MsjSecPendienteAprobacionSICAR = valor
                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsFlagAplicaCAI] [{1}]", strIdentifyLog, ReadKeySettings.ConsFlagAplicaCAI))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsTiempoSecPendientePagoLink] [{1}]", strIdentifyLog, ReadKeySettings.ConsTiempoSecPendientePagoLink))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsConsumerConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsConsumerConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsCountryConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsCountryConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsDispositivoConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsDispositivoConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsLanguageConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsLanguageConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsModuloConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsModuloConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsMsgTypeConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsMsgTypeConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsOperationConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsOperationConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsSystemConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsSystemConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsPidConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsPidConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsWsIpConsultaPA] [{1}]", strIdentifyLog, ReadKeySettings.ConsWsIpConsultaPA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsCodigoPDVTeleventas] [{1}]", strIdentifyLog, ReadKeySettings.ConsCodigoPDVTeleventas))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsMsjValidacionSecPendPagoLink] [{1}]", strIdentifyLog, ReadKeySettings.ConsMsjValidacionSecPendPagoLink))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsPDVPermitidosCAI] [{1}]", strIdentifyLog, ReadKeySettings.ConsPDVPermitidosCAI))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsMsjValidacionSubFormularioCAI] [{1}]", strIdentifyLog, ReadKeySettings.ConsMsjValidacionSubFormularioCAI))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsMsjValidacionSecPendPagoLinkEnVentaExp] [{1}]", strIdentifyLog, ReadKeySettings.ConsMsjValidacionSecPendPagoLinkEnVentaExp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [ConsMsjSMS] [{1}]", strIdentifyLog, ReadKeySettings.ConsMsjSMS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjValidacionSinNumeroTelefono] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjValidacionSinNumeroTelefono))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoOperacionSICAR] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoOperacionSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_CodMaterialPermitidosSICAR] [{1}]", strIdentifyLog, ReadKeySettings.Key_CodMaterialPermitidosSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjSecPendienteEvaluacionSICAR] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSecPendienteEvaluacionSICAR))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjSecPendienteAprobacionSICAR] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSecPendienteAprobacionSICAR))
        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales Cobro Anticipado]", strIdentifyLog))
        End Try
        'INICIATIVA 712 Cobro Anticipado FIN

'PROY-140715 | No Biometria en SISACT x caida RENIEC| Metodo para obtener los datos de la tabla de parametros | INI
        Try

            Dim Key_ParanGrupoContingencia As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables No Biometria en SISACT x caida RENIEC]", strIdentifyLog))
            Key_ParanGrupoContingencia = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ParanGrupoContingencia").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, Key_ParanGrupoContingencia))
            Dim listParametrosNoBioxReniec As ArrayList = New clsConsultaPvu().ConsultaParametros(Key_ParanGrupoContingencia)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosNoBioxReniec.Count()))

            For Each oItem As BEParametros In listParametrosNoBioxReniec
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_FlagGeneral"
                        ReadKeySettings.Key_FlagGeneral = valor
                End Select

            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagGeneral] [{1}]", strIdentifyLog, ReadKeySettings.Key_FlagGeneral))

        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales No Biometria en SISACT x caida RENIEC]", strIdentifyLog))
        End Try
        'PROY-140715 | FIN

 'INICIATIVA-1006- TIENDA VIRTUAL - INICIO
        Try

            Dim key_ParanGrupoTiendaVirtual As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables INICIATIVA-1006-Tienda Virtual]", strIdentifyLog))
            key_ParanGrupoTiendaVirtual = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoTiendaVirtual").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, key_ParanGrupoTiendaVirtual))
            Dim listParametrosTiendaVirtual As ArrayList = New clsConsultaPvu().ConsultaParametros(key_ParanGrupoTiendaVirtual)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosTiendaVirtual.Count()))


            Dim sbMat As New System.Text.StringBuilder
            For Each oItem As BEParametros In listParametrosTiendaVirtual
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_MsjAccPendientePago"
                        ReadKeySettings.Key_MsjAccPendientePago = valor
                    Case "Key_MsjAccAnulacion"
                        ReadKeySettings.Key_MsjAccAnulacion = valor
                    Case "Key_MsjAccPagado"
                        ReadKeySettings.Key_MsjAccPagado = valor
                End Select

            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjAccPendientePago] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjAccPendientePago))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjAccAnulacion] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjAccAnulacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_MsjAccPagado] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjAccPagado))
        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales INICIATIVA-1006-Tienda Virtual]", strIdentifyLog))
        End Try
        'INICIATIVA-1006- TIENDA VIRTUAL - FIN

'PROY-140743 - INI
Try

            Dim ParanGrupo_VtaAccCuo As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Inicio Cargar Variables Vta Acc Cuotas]", strIdentifyLog))
            ParanGrupo_VtaAccCuo = Funciones.CheckInt64(ConfigurationSettings.AppSettings("ParanGrupo_VtaAccCuo").ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Codigo de Paran_Grupo] [{1}]", strIdentifyLog, ParanGrupo_VtaAccCuo))
            Dim listParametrosVtaCuoAcc As ArrayList = New clsConsultaPvu().ConsultaParametros(ParanGrupo_VtaAccCuo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Se obtuvieron {1} llaves de configuracion", strIdentifyLog, listParametrosVtaCuoAcc.Count()))


            Dim sbMat As New System.Text.StringBuilder
            For Each oItem As BEParametros In listParametrosVtaCuoAcc
                valor1 = oItem.strValor1
                valor = oItem.strValor
                Select Case valor1
                    Case "Key_TipoPagoAccCuo"
                        ReadKeySettings.Key_TipoPagoAccCuo = valor
                    Case "Key_TipoMovPos"
                        ReadKeySettings.Key_TipoMovPos = valor
                    Case "Key_SubClasMovPos"
                        ReadKeySettings.Key_SubClasMovPos = valor
                    Case "Key_ClasMovPos"
                        ReadKeySettings.Key_ClasMovPos = valor
                    Case "CONS_ACCCUO_METODO"
                        ReadKeySettings.CONS_ACCCUO_METODO = valor
                    Case "CONS_ACCCUO_TIPO_INTER"
                        ReadKeySettings.CONS_ACCCUO_TIPO_INTER = valor
                    Case "CONS_ACCCUO_AGENTE"
                        ReadKeySettings.CONS_ACCCUO_AGENTE = valor
                    Case "CONS_ACCUO_USUARIO"
                        ReadKeySettings.CONS_ACCUO_USUARIO = valor
                    Case "CONS_ACCCUO_FLAG"
                        ReadKeySettings.CONS_ACCCUO_FLAG = valor
                    Case "CONS_ACCCUO_RESULTADO"
                        ReadKeySettings.CONS_ACCCUO_RESULTADO = valor
                    Case "CONS_ACCCUO_HECHO"
                        ReadKeySettings.CONS_ACCCUO_HECHO = valor

                End Select

            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_TipoPagoAccCuo] [{1}]", strIdentifyLog, ReadKeySettings.Key_TipoPagoAccCuo))
             Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurri? un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Variables Vta Acc Cuotas]", strIdentifyLog))
        End Try
'PROY-140743 - FIN

        'IDEA-143176 | Regulatorio OSIPTEL REPO | INI
        Try
            Dim Key_ParamGrupoProgramacionRepo As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty

            Key_ParamGrupoProgramacionRepo = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParamGrupoProgramacionRepo"))
            Dim listParametros As ArrayList = New clsConsultaPvu().ConsultaParametros(Key_ParamGrupoProgramacionRepo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ParamGrupoProgramacionRepo] [{1}]", strIdentifyLog, Funciones.CheckStr(Key_ParamGrupoProgramacionRepo)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [listParametros.Count] [{1}]", strIdentifyLog, Funciones.CheckStr(listParametros.Count)))

            For Each oItem As BEParametros In listParametros
                valor1 = Funciones.CheckStr(oItem.strValor1)
                valor = Funciones.CheckStr(oItem.strValor)
                Select Case valor1
                    Case "Key_CodigoProgramacionReposicion"
                        ReadKeySettings.Key_CodigoProgramacionReposicion = valor
                    Case "Key_MensajeReposicionProgramada"
                        ReadKeySettings.Key_MensajeReposicionProgramada = valor
                    Case "Key_FlagReposicionProgramada"
                        ReadKeySettings.Key_FlagReposicionProgramada = valor
                    Case "Key_TiempoProgramacionReposicion"
                        ReadKeySettings.Key_TiempoProgramacionReposicion = valor
                    Case "Key_TipoServicioPostpago"
                        ReadKeySettings.Key_TipoServicioPostpago = valor
                    Case "Key_TipoServicioPrepago"
                        ReadKeySettings.Key_TipoServicioPrepago = valor
                    Case "Key_TipoServicioPostpago"
                        ReadKeySettings.Key_TipoServicioPostpago = valor
                    Case "Key_CodigoServicioProgRepo"
                        ReadKeySettings.Key_CodigoServicioProgRepo = valor
                    Case "Key_TipoRegistroProgRepo"
                        ReadKeySettings.Key_TipoRegistroProgRepo = valor
                    Case "Key_EsBatchProgRepo"
                        ReadKeySettings.Key_EsBatchProgRepo = valor
                    Case "Key_descCodServicioProgRepo"
                        ReadKeySettings.Key_descCodServicioProgRepo = valor
                    Case "Key_EstadoProgramacionReposicion"
                        ReadKeySettings.Key_EstadoProgramacionReposicion = valor
                    Case "Key_ClienteSapPrepago"
                        ReadKeySettings.Key_ClienteSapPrepago = valor
                    Case "Key_ClienteSapPostpago"
                        ReadKeySettings.Key_ClienteSapPostpago = valor
                    ''IDEA300216 INI
                    Case "Key_DescripcionOperacionAlta"
                        ReadKeySettings.Key_DescripcionOperacionAlta = valor
                    Case "Key_DescripcionOperacionPortabilidad"
                        ReadKeySettings.Key_DescripcionOperacionPortabilidad = valor
                    Case "Key_DescripcionOperacionRenovacion"
                        ReadKeySettings.Key_DescripcionOperacionRenovacion = valor
                    Case "Key_DescripcionOperacionRenovacionPack"
                        ReadKeySettings.Key_DescripcionOperacionRenovacionPack = valor
                    Case "Key_DescripcionOperacionReposicion"
                        ReadKeySettings.Key_DescripcionOperacionReposicion = valor
                    Case "Key_TipoLineaPostpago"
                        ReadKeySettings.Key_TipoLineaPostpago = valor
                    Case "Key_TipoLineaPrepago"
                        ReadKeySettings.Key_TipoLineaPrepago = valor
                    Case "Key_ConsultaEstadoClaveUnica"
                        ReadKeySettings.Key_ConsultaEstadoClaveUnica = valor
                    Case "Key_MensajeAnuladaClaveUnica"
                        ReadKeySettings.Key_MensajeAnuladaClaveUnica = valor
                    Case "Key_MensajeAprobadaClaveUnica"
                        ReadKeySettings.Key_MensajeAprobadaClaveUnica = valor
                    Case "Key_MensajeErrorConsultaClaveUnica"
                        ReadKeySettings.Key_MensajeErrorConsultaClaveUnica = valor
                    Case "Key_MensajePendienteClaveUnica"
                        ReadKeySettings.Key_MensajePendienteClaveUnica = valor
                    Case "Key_MensajeRechazadaClaveUnica"
                        ReadKeySettings.Key_MensajeRechazadaClaveUnica = valor
                    ''IDEA300216 FIN
                End Select
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_FlagReposicionProgramada] [{1}]", strIdentifyLog, Funciones.CheckStr(ReadKeySettings.Key_FlagReposicionProgramada)))

        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurrio un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales Regulatorio Osiptel]", strIdentifyLog))
        End Try
        'IDEA-143176 | FIN

	'IDEA-300846 INI
        Try
            Dim Key_ParamValidarPortabilidad As Int64 = 0
            Dim valor1 As String = String.Empty
            Dim valor As String = String.Empty

            Key_ParamValidarPortabilidad = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ParamValidarPortabilidad"))
            Dim listParametrosPorta As ArrayList = New clsConsultaPvu().ConsultaParametros(Key_ParamValidarPortabilidad)
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Key_ParamValidarPortabilidad] [{1}]", strIdentifyLog, Funciones.CheckStr(Key_ParamValidarPortabilidad)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [listParametros.Count] [{1}]", strIdentifyLog, Funciones.CheckStr(listParametrosPorta.Count)))

            For Each oItem As BEParametros In listParametrosPorta
                valor1 = Funciones.CheckStr(oItem.strValor1)
                valor = Funciones.CheckStr(oItem.strValor)
                Select Case valor1
                    Case "Key_MsjValidaPinNoPinCAC"
                        ReadKeySettings.Key_MsjValidaPinNoPinCAC = valor
                    Case "Key_MsjValidaPinNoVigenteCAC"
                        ReadKeySettings.Key_MsjValidaPinNoVigenteCAC = valor
                    Case "Key_MsjSPEnviadaCAC"
                        ReadKeySettings.Key_MsjSPEnviadaCAC = valor
                    Case "Key_MsjSPRechazoNoPinNoDeudaCAC"
                        ReadKeySettings.Key_MsjSPRechazoNoPinNoDeudaCAC = valor
                    Case "Key_MsjSPRechazoPinCAC"
                        ReadKeySettings.Key_MsjSPRechazoPinCAC = valor
                    Case "Key_MsjSPRechazoDeudaCAC"
                        ReadKeySettings.Key_MsjSPRechazoDeudaCAC = valor
                    Case "Key_MotivoErrorPin"
                        ReadKeySettings.Key_MotivoErrorPin = valor
                    Case "Key_MotivoRechazoDeuda"
                        ReadKeySettings.Key_MotivoRechazoDeuda = valor
                    Case "Key_MsjErrorGenerarPin"
                        ReadKeySettings.Key_MsjErrorGenerarPin = valor
                End Select
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjValidaPinNoPinCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjValidaPinNoPinCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjValidaPinNoVigenteCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjValidaPinNoVigenteCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjSPEnviadaCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSPEnviadaCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjSPRechazoNoPinNoDeudaCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSPRechazoNoPinNoDeudaCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjSPRechazoPinCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSPRechazoPinCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjSPRechazoDeudaCAC] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjSPRechazoDeudaCAC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MotivoErrorPin] [{1}]", strIdentifyLog, ReadKeySettings.Key_MotivoErrorPin))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MotivoRechazoDeuda] [{1}]", strIdentifyLog, ReadKeySettings.Key_MotivoRechazoDeuda))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - IDEA-300846 - [Key_MsjErrorGenerarPin] [{1}]", strIdentifyLog, ReadKeySettings.Key_MsjErrorGenerarPin))

        Catch e As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Ocurrio un error al cargar las variables globales] [{1}]", strIdentifyLog, e.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - [Fin Cargar Variables Globales Validar Portabilidad]", strIdentifyLog))
        End Try
        'IDEA-300846 | FIN
    End Sub
#End Region

End Class
