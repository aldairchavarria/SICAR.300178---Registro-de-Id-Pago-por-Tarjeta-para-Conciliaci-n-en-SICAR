Imports SisCajas.Funciones
Imports COM_SIC_Activaciones 'PROY-32089
Imports System.Globalization 'PROY-32089
Imports COM_SIC_Seguridad 'IDEA-300846

Public Class clsActivaciones
    
    Public Shared Function EjecutarActivacion(ByVal strDocSap As String, _
                                                ByVal strNumAsignaSUNAT As String, _
                                                ByVal montoPago As String, _
                                                ByVal CurrentUser As String, _
                                                ByVal CurrentTerminal As String, _
                                                ByRef nroSec As String, _
                                                ByVal sisact_Cod_Operacion As String, _
                                                ByVal strNumeroDocumento As String) As String 'INICIATIVA-219

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap
        Dim codResp, msgResp As String
        Try
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio EjecutarActivacion")
            Dim codRespOK As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codResp"))

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "sisact_Cod_Operacion: " & sisact_Cod_Operacion)

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio AsignarPagoAcuerdosXDocSap", strDocSap, strNumAsignaSUNAT, CurrentUser)
            codResp = (New COM_SIC_Activaciones.clsBDSiscajas).AsignarPagoAcuerdosXDocSap(strDocSap, strNumAsignaSUNAT, CheckDbl(montoPago), CurrentUser, msgResp)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin AsignarPagoAcuerdosXDocSap", codResp, msgResp)

            If codResp = codRespOK Then
                Dim dsDatosAcuerdos As New DataSet

                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ConsultaAcuerdosXDocSap", strDocSap)
                dsDatosAcuerdos = (New COM_SIC_Activaciones.clsBDSiscajas).ConsultaAcuerdosXDocSap(strDocSap, codResp, msgResp)
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ConsultaAcuerdosXDocSap", codResp, msgResp)

                If codResp = codRespOK Then
                    Dim blnPagado As Boolean = True
                    For Each dr As DataRow In dsDatosAcuerdos.Tables(0).Rows
                        If (Funciones.CheckStr(dr("TIPO_DOCUMENTO")) = "F" Or Funciones.CheckStr(dr("TIPO_DOCUMENTO")) = "G") And Funciones.CheckStr(dr("nro_referencia")) = "" Then
                            blnPagado = False
                            Exit For
                        End If
                    Next
                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "blnPagado: " & blnPagado)
                    If blnPagado Then
                        Dim cadenaNroAcuerdos As String = ""
                        Dim nroAcuerdo, codEstadoAcuerdo, codTipoProducto, codMotivoMig, strTelefono As String 'INICIATIVA-219
                        Dim codEstadoReservado As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codEstadoReservado"))
                        Dim codEstadoNuevo As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codEstadoNuevo"))
                        Dim oAudit As New COM_SIC_Activaciones.ItemGenerico
                        Dim oActivacionConverg As New COM_SIC_Activaciones.clsActivacion
                        oAudit.CODIGO = ""
                        oAudit.CODIGO2 = CurrentTerminal
                        oAudit.DESCRIPCION = ConfigurationSettings.AppSettings("constAplicacion")
                        oAudit.DESCRIPCION2 = CurrentUser

                        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio - dsDatosAcuerdos.Tables(1).Rows  ")

                        'INI: INICIATIVA-219
                        Dim ReadKeySettings As New COM_SIC_Seguridad.ReadKeySettings
                        Dim strFlagCBIO As String = CheckStr(ReadKeySettings.key_flagCBIO)
                        Dim objActivacionCBIO As New ClsActivacionCBIO
                        Dim objEncolarCBIO As New BWEncolarTransaccionesCBIO
                        Dim objEncolarRequest As New EncolarTransaccionRequest
                        Dim objEncolarResponse As New EncolarTransaccionResponse
                        Dim objAuditoriaWS As New AuditoriaEWS             
                        Dim objActivarCBIOWS As New BWActivarCBIO
                        'FIN: INICIATIVA-219

                        For Each dr As DataRow In dsDatosAcuerdos.Tables(1).Rows
                            nroAcuerdo = Funciones.CheckStr(dr("SUB_CONTRATO"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "nroAcuerdo: " & nroAcuerdo)
                            codEstadoAcuerdo = Funciones.CheckStr(dr("ESTADO"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "codEstadoAcuerdo: " & codEstadoAcuerdo)
                            codTipoProducto = Funciones.CheckStr(dr("PRDC_CODIGO"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "codTipoProducto: " & codTipoProducto)
                            codMotivoMig = Funciones.CheckStr(dr("CONTV_MOTIVO_MIG"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "codMotivoMig: " & codMotivoMig)
                            nroSec = Funciones.CheckStr(dr("contn_numero_sec"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "nroSec: " & nroSec)
                            'INI: INICIATIVA-219
                            strTelefono = Funciones.CheckStr(dr("telefono"))
                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Telefono: " & strTelefono)
                            'INI: INICIATIVA-219

                            'if cod 'if es repo y reno no entrar a este webservice (solo altas y migraciones)
                            Dim cod_OperaSisact() As String
                            cod_OperaSisact = ConfigurationSettings.AppSettings("codOperacionSisact").Split(",")

                            For j As Integer = 0 To UBound(cod_OperaSisact)
                                If sisact_Cod_Operacion = Funciones.CheckStr(cod_OperaSisact(j)) Or sisact_Cod_Operacion = "" Then
                                    codResp = CheckStr(ConfigurationSettings.AppSettings("codRespDocSapNoConvergente"))
                                    Return codResp
                                    Exit Function
                                End If
                            Next

                            'If sisact_Cod_Operacion = ConfigurationSettings.AppSettings("codOperacionSisact") Then
                            '    codResp = CheckStr(ConfigurationSettings.AppSettings("codRespDocSapNoConvergente"))
                            '    Exit Function
                            'End If
                            If codEstadoAcuerdo = codEstadoReservado AndAlso cadenaNroAcuerdos.IndexOf(nroAcuerdo) = -1 Then
                                If codTipoProducto <> CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codProductoDTH")) Then
                                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ActualizaEstadoContrato", nroAcuerdo, codEstadoNuevo)

                                    'INI: INICIATIVA-219
                                    Dim strCodResp = String.Empty
                                    Dim strMsgResp = String.Empty
                                    Dim strTipoOperacionCBIO = String.Empty

                                    'PBI000002155923-INICIO
                                    Dim arrListaNum As ArrayList
                                    Dim nroAcuerdo1 As String
                                    If sisact_Cod_Operacion= "01" and codTipoProducto ="01" Then 
                                     nroAcuerdo1 = Funciones.CheckStr(dr("ID_CONTRATO"))
									 objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[INICIO CONSULTATRANSACCION()]")
                                        arrListaNum = objActivacionCBIO.consultaTransaccion1(nroAcuerdo, strTelefono, Funciones.CheckStr(sisact_Cod_Operacion), strCodResp, strMsgResp)
                                        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[TPROC_CODIGO : {0}]", Funciones.CheckStr(ConfigurationSettings.AppSettings("TPROC_CODIGO")))
										 objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[nroAcuerdo1 : {0}]", Funciones.CheckStr(nroAcuerdo1) )
									     objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[strCodResp : {0}]", Funciones.CheckStr(strCodResp) )
										 objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[strMsgResp : {0}]",Funciones.CheckStr(strMsgResp) )
										 objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-CON CAMBIOS]-[FIN CONSULTATRANSACCION()]")
                                    Else
									objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-SIN CAMBIOS]-[INICIO CONSULTATRANSACCION()]")
                                        arrListaNum = objActivacionCBIO.consultaTransaccion(strTelefono, Funciones.CheckStr(sisact_Cod_Operacion), strCodResp, strMsgResp)
									     objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-SIN CAMBIOS]-[strCodResp : {0}]", Funciones.CheckStr(strCodResp) )
										 objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-SIN CAMBIOS]-[strMsgResp : {0}]",Funciones.CheckStr(strMsgResp) )
									objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[PBI000002155923-SIN CAMBIOS]-[FIN CONSULTATRANSACCION()]")
                                    End If

                                    'PBI000002155923-FIN

                                    If (strCodResp = "0" And Not arrListaNum Is Nothing And arrListaNum.Count > 0) Then
                                        codEstadoNuevo = Funciones.CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoCBIO_codEstadoNuevo"))
                                    End If
                                    'FIN: INICIATIVA-219 

                                    codResp = (New COM_SIC_Activaciones.clsBDSiscajas).ActualizaEstadoContrato(nroAcuerdo, "", "", codEstadoNuevo, CurrentUser, "", "Pago de factura " & strDocSap, "", msgResp)
                                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ActualizaEstadoContrato", codResp, msgResp)

                                    If codMotivoMig <> "104" Then '104 =portabilidad 
                                        oAudit.CODIGO = Format(Now, "yyyyMMddmmss") & nroAcuerdo
                                        Try
                                            'INICIO: INICIATIVA-219
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [strFlagCBIO : {0}]", Funciones.CheckStr(strFlagCBIO)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [Consulta origen linea] - [strTelefono : {0}]", Funciones.CheckStr(strTelefono)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [strCodResp : {0}]", Funciones.CheckStr(strCodResp)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [strMsgResp : {0}]", Funciones.CheckStr(strMsgResp)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [arrListaPrepago : {0}]", IIf(arrListaNum Is Nothing, "No Existe", arrListaNum.Count.ToString)))

                                            If (strCodResp = "0" And Not arrListaNum Is Nothing And arrListaNum.Count > 0) Then
                                                For Each item As COM_SIC_Activaciones.ItemGenerico In arrListaNum
                                                    'Enviaremos el tipo de Operacion de CBIO
                                                    If sisact_Cod_Operacion = "02" Then 'MIGRACION
                                                        strTipoOperacionCBIO = ReadKeySettings.key_TipoOperacionMigracionAltaCBIO
                                                    ElseIf sisact_Cod_Operacion = "01" Then 'ALTA
                                                        strTipoOperacionCBIO = ReadKeySettings.key_TipoOperacionAltaCBIO
                                                    End If

                                                    objEncolarRequest.encolarTransaccionRequest.idNegocio = item.CODIGO
                                                    objEncolarRequest.encolarTransaccionRequest.idNegocioAux = Nothing
                                                    objEncolarRequest.encolarTransaccionRequest.tipoOperacion = strTipoOperacionCBIO
                                                    objEncolarRequest.encolarTransaccionRequest.nombreAplicacion = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
                                                    objEncolarRequest.encolarTransaccionRequest.listaOpcional = Nothing 'No se usuara

                                                    objAuditoriaWS.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
                                                    objAuditoriaWS.msgId = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
                                                    objAuditoriaWS.userId = CurrentUser
                                                    objAuditoriaWS.timestamp = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss")

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [Inicio EncolarTransaccionCBIO()"))

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "sisact_Cod_Operacion", Funciones.CheckStr(sisact_Cod_Operacion)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "IdNegocio", Funciones.CheckStr(item.CODIGO)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "IdNegocioAux", Funciones.CheckStr(item.CODIGO2)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objEncolarRequest.encolarTransaccionRequest] - [{0} : {1}]", "nombreAplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))))

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [Parametros Header Request] [{0}] -  INI", "objAuditoriaWS"))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objAuditoriaWS] - [{0} : {1}]", "IDTRANSACCION", Funciones.CheckStr(objAuditoriaWS.IDTRANSACCION)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objAuditoriaWS] - [{0} : {1}]", "msgId", Funciones.CheckStr(objAuditoriaWS.msgId)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objAuditoriaWS] - [{0} : {1}]", "userId", Funciones.CheckStr(objAuditoriaWS.userId)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objAuditoriaWS] - [{0} : {1}]", "timestamp", Funciones.CheckStr(objAuditoriaWS.timestamp)))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [Parametros Header Request] [{0}] -  FIN", "objAuditoriaWS"))

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [URL : {0}] - [nroAcuerdo : {1}] - [idTrs : {2}]", Funciones.CheckStr(ConfigurationSettings.AppSettings("EncolarReposicionRest")), Funciones.CheckStr(nroAcuerdo), Funciones.CheckStr(oAudit.CODIGO)))

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [Ejecutando: objEncolarCBIO.EncolarTransaccionCBIO()]"))

                                                    objEncolarResponse = objEncolarCBIO.EncolarTransaccionCBIO(objEncolarRequest, objAuditoriaWS)

                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("[INICIATIVA-219] - [objEncolarResponse] - [objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta: {0}] - [nroAcuerdo: {1}] - [idTrs: {2}]", Funciones.CheckStr(objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta), Funciones.CheckStr(nroAcuerdo), Funciones.CheckStr(oAudit.CODIGO)))

                                                    If objEncolarResponse.encolarReposicionResponse.responseAudit.codigoRespuesta <> "0" Then
                                                        Throw New Exception(msgResp)
                                                    End If
                                                    Exit For
                                                Next
                                                'FIN: INICIATIVA-219
                                            Else
                                                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "[INICIATIVA-219][No realizara la activacion en CBIO]")
                                        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio EjecutarActivacion:  " & ConfigurationSettings.AppSettings("WSActivosPostpagoConvergente_url"), "nroAcuerdo: " & nroAcuerdo, "idTrs: " & oAudit.CODIGO)
                                                oActivacionConverg.EjecutarActivacion(nroAcuerdo, "", oAudit, codResp, msgResp)
                                            End If

                                            If codResp <> CheckStr(ConfigurationSettings.AppSettings("WSActivosPostpagoConvergente_codRespOK")) Then
                                                Throw New Exception(msgResp)
                                            End If
                                        Catch ex As Exception
                                            'INI PROY-140126
                                            Dim MaptPath As String
                                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                            MaptPath = "( Class : " & MaptPath & "; Function: EjecutarActivacion)"
                                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Error EjecutarActivacion: ", ex.Message.ToString() & " " & ex.StackTrace.ToString() & MaptPath)
                                            'FIN PROY-140126                                           
                                            Dim codEstadoError As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codEstadoError")) ' Se declaro al inicio

                                            'INICIO: INICIATIVA-219
                                            If (strCodResp = "0" And Not arrListaNum Is Nothing And arrListaNum.Count > 0) Then
                                                codEstadoError = Funciones.CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoCBIO_codEstadoError"))
                                            End If
                                            'FIN: INICIATIVA-219

                                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ActualizaEstadoContrato", nroAcuerdo, codEstadoError)
                                            codResp = (New COM_SIC_Activaciones.clsBDSiscajas).ActualizaEstadoContrato(nroAcuerdo, "", "", codEstadoError, CurrentUser, "", "Error Invocar WS (SICAR). DocSap: " & strDocSap, "", msgResp)
                                            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ActualizaEstadoContrato", codResp, msgResp)

                                            EnviarCorreoAlerta(nroAcuerdo, ex)
                                        End Try
                                        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin EjecutarActivacion", msgResp)
                                    End If
                                End If
                                cadenaNroAcuerdos &= "|" & nroAcuerdo
                            End If
                        Next
                    End If
                End If
            End If
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin EjecutarActivacion")
        Catch ex As Exception
            EnviarCorreoAlerta("", ex)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR:" & ex.Message)
        End Try
        Return codResp
    End Function

    Public Shared Function ValidaAnularPagoDocumento(ByVal strDocSap As String) As Boolean
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap

        Dim blnAnular As Boolean = True
        Try
            Dim codResp, msgResp As String
            Dim codRespOK As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codResp"))

            Dim dsDatosAcuerdos As New DataSet

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ConsultaAcuerdosXDocSap", strDocSap)
            dsDatosAcuerdos = (New COM_SIC_Activaciones.clsBDSiscajas).ConsultaAcuerdosXDocSap(strDocSap, codResp, msgResp)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ConsultaAcuerdosXDocSap", codResp, msgResp)

            If dsDatosAcuerdos.Tables.Count > 0 AndAlso codResp = codRespOK Then
                Dim codEstadosNoAnular As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codEstadosNoAnular"))
                For Each dr As DataRow In dsDatosAcuerdos.Tables(1).Rows
                    If codEstadosNoAnular.IndexOf(Funciones.CheckStr(dr("ESTADO"))) > -1 Then
                        blnAnular = False
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR ValidaAnularDocumento", ex.Message.ToString())
        End Try

        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "blnAnular", blnAnular)
        Return blnAnular
    End Function
    Public Shared Function ValidaAnularDocumento(ByVal strDocSap As String) As Boolean
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap

        Dim blnAnular As Boolean = True
        Try
            Dim codResp, msgResp As String
            Dim codRespOK As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codResp"))

            Dim dsDatosAcuerdos As New DataSet

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ConsultaAcuerdosXDocSap", strDocSap)
            dsDatosAcuerdos = (New COM_SIC_Activaciones.clsBDSiscajas).ConsultaAcuerdosXDocSap(strDocSap, codResp, msgResp)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ConsultaAcuerdosXDocSap", codResp, msgResp)

            If dsDatosAcuerdos.Tables.Count > 0 AndAlso codResp = codRespOK Then
                For Each dr As DataRow In dsDatosAcuerdos.Tables(0).Rows
                    If (Funciones.CheckStr(dr("TIPO_DOCUMENTO")) = "F" Or Funciones.CheckStr(dr("TIPO_DOCUMENTO")) = "G") And Funciones.CheckStr(dr("nro_referencia")) <> "" Then
                        blnAnular = False
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR ValidaAnularDocumento", ex.Message.ToString())
        End Try

        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "blnAnular", blnAnular)
        Return blnAnular
    End Function
    Public Shared Sub AnularAcuerdo(ByVal strDocSap As String, ByVal usuario As String)
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap

        Dim blnAnular As Boolean = True
        Try
            Dim codResp, msgResp As String
            Dim codRespOK As String = CheckStr(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codResp"))

            Dim dsDatosAcuerdos As New DataSet

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio ConsultaAcuerdosXDocSap", strDocSap)
            dsDatosAcuerdos = (New COM_SIC_Activaciones.clsBDSiscajas).ConsultaAcuerdosXDocSap(strDocSap, codResp, msgResp)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin ConsultaAcuerdosXDocSap", codResp, msgResp)

            If dsDatosAcuerdos.Tables.Count > 0 AndAlso codResp = codRespOK Then
                Dim nroAcuerdo As String = ""
                For Each dr As DataRow In dsDatosAcuerdos.Tables(1).Rows
                    nroAcuerdo = Funciones.CheckStr(dr("ID_CONTRATO"))
                    Exit For
                Next
                Dim codEstadoAnulado As String = ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_codEstadoAnulado")
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio CambiarEstadoAcuerdo", nroAcuerdo, codEstadoAnulado, usuario)
                codResp = (New COM_SIC_Activaciones.clsBDSiscajas).CambiarEstadoAcuerdo(nroAcuerdo, codEstadoAnulado, usuario, msgResp)
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin CambiarEstadoAcuerdo", codResp, msgResp)
            End If
        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR ValidaAnularDocumento", ex.Message.ToString())
        End Try

    End Sub

    Private Shared Sub EnviarCorreoAlerta(ByVal nroAcuerdo As String, ByVal ex As Exception)
        Try
            Dim strHTMLCuerpo As String
            strHTMLCuerpo = "<HTML>"
            strHTMLCuerpo = strHTMLCuerpo & "<HEAD></HEAD><BODY><table>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td>" & ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_correoAsunto") & "</tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td></tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td>   nroAcuerdo: " & nroAcuerdo & "</tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td>   WS: " & ConfigurationSettings.AppSettings("WSActivosPostpagoConvergente_url") & "</tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td>   Método: ejecutarActivacion</tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "<tr><td>   ERROR: " & ex.Message.ToString() & "</tr></td>"
            strHTMLCuerpo = strHTMLCuerpo & "</table></BODY></HTML>"
            EnviarEmail(ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_correoRemitente"), ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_correoDestinatario"), "", "", ConfigurationSettings.AppSettings("ActivosPostpagoConvergente_correoAsunto"), strHTMLCuerpo, "")
        Catch ex2 As Exception
        End Try
    End Sub

    Public Shared Sub AnularVentaSisact(ByVal strDocSap As String, ByVal usuario As String, ByRef p_id_venta As Int64, ByRef p_id_contrato As Int64)
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap
        Try
            Dim p_cod_resp, p_msg_resp, p_ost As String
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio AnularVentaSisact", strDocSap, usuario)
            p_cod_resp = (New COM_SIC_Activaciones.clsBDSiscajas).AnularVentaPVUDB(strDocSap, usuario, p_id_venta, p_id_contrato, p_msg_resp)
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin AnularVentaSisact", p_id_venta, p_id_contrato, p_msg_resp)
        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR AnularVentaSisact", ex.Message.ToString())
        End Try
    End Sub

    Public Shared Sub AnularCanjeEquipo(ByVal p_Id_Venta As Int64)
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogCanjeEquipo"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = p_Id_Venta.ToString()
        Try
            Dim p_cod_resp, p_msg_resp, p_ost As String
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Inicio AnularCanjeEquipo", p_Id_Venta)
            p_cod_resp = (New COM_SIC_Activaciones.clsBDSiscajas).AnularCanjeEquipo(p_Id_Venta, p_ost, p_msg_resp)

            If p_cod_resp = "0" Then
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Se realizó la Anulacion de Caje de Equipo correctamente")
            End If

        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR AnularCanjeEquipo", ex.Message.ToString())
        End Try
        objFileLog.EscribirLog(pathFile, strArchivo, idLog, "Fin AnularCanjeEquipo")
    End Sub

    Public Shared Function ObtenerDatosVenta(ByVal strDocSap As String) As DataSet
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = strDocSap
        Dim dsDatosVenta As New DataSet
        Try
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "----> ObtenerDatosVenta")
            dsDatosVenta = (New COM_SIC_Activaciones.clsConsultaPvu).ObetenerDatosVenta(strDocSap)

        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "ERROR ObtenerDatosVenta", ex.Message.ToString())
            dsDatosVenta = Nothing
        End Try

        Return dsDatosVenta
    End Function

    'PROY-32089 INI :: 1.1 ::
    Public Shared Function validarEnvioSolicitudPortabilidad(ByVal strNroSec As String, ByVal dtsPedidos As DataSet, ByVal objAudit As BEAuditoria, ByRef strMensaje As String, ByRef strCodigo As String, Optional ByVal strNroPedido As String = "") As Int64

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strNroSec
        Dim theReturn As Int64 = 0
        Try


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Verificar CP Portabilidad (Pagar - ValidarSEC)")


            Dim objConsulta As New COM_SIC_Activaciones.ClsPortabilidad
            Dim strCodRpta As String = "", strMsgRpta As String = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - strNroSec: " & strNroSec)

            Dim objDatos As DataSet = objConsulta.ValidarSEC(strNroSec, strCodRpta, strMsgRpta) 'VALIDA QUE LA SEC NO TENGA PAGO, SE ENCUENTRE CON ESTADO SPR/CPSPR Y RABDCP

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - strCodRpta : " & strCodRpta) 'IDEA-300846
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - strCodRpta : " & strMsgRpta) 'IDEA-300846

            'PROY-26963 JACOSTA INI
            Dim SecProcesoSP = getValidarSECProcesoSP(objDatos, dtsPedidos) 'PROY 32089

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - SecProcesoSP: " & SecProcesoSP)

            'INI: PROY-140262 BLACKOUT
            Dim strPedidoSunat As String = Funciones.CheckStr(dtsPedidos.Tables(0).Rows(0).Item("PEDIV_NROSUNAT"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - consFlagBlackOut  : " & clsKeyAPP.consFlagBlackOut)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - strPedidoSunat  : " & strPedidoSunat)
            If clsKeyAPP.consFlagBlackOut = "1" AndAlso strPedidoSunat.Trim.Length > 0 Then
                SecProcesoSP = 2
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - No realiza solictud de portabilidad por que viene de una anulacion")
            End If
            'FIN: PROY-140262 BLACKOUT


            If (SecProcesoSP = "1") Then
                Dim oEnvioPortal As New BWEnvioPortal
                Dim strIdTransaction As String = DateTime.Now.ToString("yyMMddhhmmss")
                Dim strNameApli As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("CONS_APLICACION"))
                Dim strTipoPorta As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsTipoPorta"))
                Dim strRptaService As String = "", strMsgEmail As String = "", strRptService As String = ""

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "usuario_id : " & objAudit.Usuario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ipcliente : " & objAudit.Terminal)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIdTransaction : " & strIdTransaction)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strNameApli : " & strNameApli)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strNameHost : " & objAudit.HostName)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strNameServer : " & objAudit.ServerName)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIpServer : " & objAudit.IPServer)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strTipoPorta : " & strTipoPorta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strNroPedido : " & strNroPedido)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio :  RealizarSolicitudPortabilidad")

                ':: 3 :: ENVIA SOLICITUD DE PORTABILIDAD
                strRptaService = oEnvioPortal.RealizarSolicitudPortabilidad(CLng(strNroSec), strIdTransaction, objAudit.Terminal, _
                                                                                strNameApli, objAudit.Usuario, objAudit.HostName, objAudit.ServerName, _
                                                                                objAudit.IPServer, strTipoPorta, strRptService, "1", strNroPedido)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin :  RealizarSolicitudPortabilidad")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strRptaService : " & strRptaService)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strRptService : " & strRptService)
				
		'IDEA-300846 INI
                strMensaje = Funciones.CheckStr(ReadKeySettings.Key_MsjSPEnviadaCAC)
                'IDEA-300846 FIN
				
                If (strRptaService = "0") Then 'Si al realizar la SP es OK
                    'NumEstadoSP = 1
                    theReturn = 1
                    'INI: PROY-14026 2BLACKOUT
                ElseIf (strRptaService = "9") And (clsKeyAPP.consFlagBlackOut = "1") Then
                    theReturn = 1
                    'FIN: PROY-14026 2BLACKOUT
                Else
                    'Enviar email a Mesa Portabilidad
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio :  Enviar_Email")
                    strMsgEmail = Enviar_Email(objAudit, strIdTransaction, strNameApli, strNroSec, strRptService)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin :  Enviar_Email")
                    Exit Function
                End If

            ElseIf (SecProcesoSP = "2") Then 'jacosta 26963 SEC es validad y tiene SP
                'NumEstadoSP = 2
                theReturn = 2
            Else
                strMensaje = clsKeyAPP.consMensPagoNoProce ':: 2 ::
                'NumEstadoSP = 3
                theReturn = 3 'SEC no es valida
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strMensaje :  strMensaje")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "theReturn :  theReturn")

            'PROY-26963 JACOSTA FIN

        Catch ex As Exception
            objFileLog.EscribirLog(pathFile, strArchivo, strIdentifyLog, "ERROR ValidarSP", ex.Message.ToString())
            strMensaje = clsKeyAPP.consMensPagoNoProce
            'Return False
        End Try

        Return theReturn
    End Function

    Public Shared Function Enviar_Email(ByVal objAudit As BEAuditoria, ByVal strIdTran As String, ByVal strNameAplica As String, ByVal NroSec As String, ByVal MsgSercice As String) As String

        Dim strRpta As String = ""
        Dim oEnvioEmail As New BWEnvioCorreo

        oEnvioEmail.EnviarCorreoWS(strIdTran, objAudit.Terminal, strNameAplica, objAudit.Usuario, NroSec, MsgSercice, strRpta)
        oEnvioEmail = Nothing

        Return strRpta
    End Function

    Public Shared Function getValidarSECProcesoSP(ByVal arraylistaSP As DataSet, ByVal dsPedidos As DataSet) As String
        'IDEA-300846 - INI
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = arraylistaSP.Tables(0).Rows(0)("SOPON_SOLIN_CODIGO")
        'IDEA-300846 - FIN

        Dim numEstado As String = ""
        Dim arrEstadosSPPermitidos() As String = clsKeyAPP.consEstadosSPPermitidos.Split("|"c)
        Dim arrEstadosSPNoPermitidos() As String = clsKeyAPP.consEstadosSPNoPermitidos.Split("|"c)
        Dim arrEstadosNoSP() As String = clsKeyAPP.consEstadosNoSP.Split("|"c)

        'IDEA-300846 - INI
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - clsKeyAPP.consEstadosSPPermitidos : " & clsKeyAPP.consEstadosSPPermitidos)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - clsKeyAPP.consEstadosSPNoPermitidos : " & clsKeyAPP.consEstadosSPNoPermitidos)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - clsKeyAPP.consEstadosNoSP : " & clsKeyAPP.consEstadosNoSP)
        'IDEA-300846 - FIN

        For Each obj_dr As DataRow In arraylistaSP.Tables(0).Rows
            For Each dt As DataRow In dsPedidos.Tables(1).Rows
                If obj_dr.Item("SOPOC_INICIO_RANGO") = dt.Item("DEPEV_NROTELEFONO") Then
                    For i As Integer = 0 To arrEstadosNoSP.Length - 1 'CPPR|RABDCP NO TIENE Solicitud de Portabilidad
                        If (arrEstadosNoSP(i) <> Nothing AndAlso arrEstadosNoSP(i) <> "" AndAlso arrEstadosNoSP(i).Split(";"c).Length = 2) Then
                            Dim arrNoSP() As String = arrEstadosNoSP(i).Split(";"c)
                            For Each item As DataRow In arraylistaSP.Tables(0).Rows
                                If dt.Item("DEPEV_NROTELEFONO") = item("SOPOC_INICIO_RANGO") Then

                                    'IDEA-300846 - INI
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " CPPR|RABDCP NO TIENE Solicitud de Portabilidad")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_INICIO_RANGO : " & item("SOPOC_INICIO_RANGO"))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_TIPO_MENSAJE : " & item("SOPOV_TIPO_MENSAJE"))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_MOTIVO : " & item("SOPOV_MOTIVO"))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_FLAG_SP : " & item("SOPOC_FLAG_SP"))
                                    'IDEA-300846 - FIN

                                    If (DBNull.Value.Equals(item("SOPOV_MOTIVO"))) Then
                                        item("SOPOV_MOTIVO") = ""
                                    End If

                                    'INI: PROY-140223 IDEA-140462
                                    If (DBNull.Value.Equals(item("SOPOV_TIPO_MENSAJE"))) Then
                                        item("SOPOV_TIPO_MENSAJE") = ""
                                    End If
                                    'FIN: PROY-140223 IDEA-140462

                                    If (item("SOPOV_TIPO_MENSAJE").Equals(arrNoSP(0)) AndAlso item("SOPOV_MOTIVO").Equals(arrNoSP(1))) Then   'FLAG_SP ><`1`'FALTA VALIDAR EL SOPOV MOTIVO CUANDO SEA NOTHING

                                        ' SOPOC_FLAG_SP EN UNO TIENE SP
                                        If Funciones.CheckStr(item("SOPOC_FLAG_SP")) <> "1" Then
                                            Return "1"
                                        Else
                                            Return "2"
                                        End If

                                    End If

                                    'If (item("SOPOV_TIPO_MENSAJE").Equals(arrNoSP(0)) AndAlso item("SOPOV_MOTIVO").Equals(arrNoSP(1)) AndAlso Funciones.CheckStr(item("SOPOC_FLAG_SP")) <> "1") Then    '(item.SOPOV_TIPO_MENSAJE.Equals(arrNoSP(0)) AndAlso item.SOPOV_MOTIVO.Equals(arrNoSP(1))) Then
                                    '    Return "1"
                                    'End If
                                End If

                            Next
                        End If
                    Next
                    'PROY-140223
                    For i As Integer = 0 To arrEstadosSPNoPermitidos.Length - 1 'PEP Solicitud de Portabilidad NO PERMITIDA
                        If (arrEstadosSPNoPermitidos(i) <> Nothing AndAlso arrEstadosSPNoPermitidos(i) <> "" AndAlso arrEstadosSPNoPermitidos(i).Split(";"c).Length = 2) Then
                            Dim arrSPNo() As String = arrEstadosSPNoPermitidos(i).Split(";"c)
                            For Each item As DataRow In arraylistaSP.Tables(0).Rows

                                'IDEA-300846 - INI
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PEP Solicitud de Portabilidad NO PERMITIDA")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_INICIO_RANGO : " & item("SOPOC_INICIO_RANGO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_TIPO_MENSAJE : " & item("SOPOV_TIPO_MENSAJE"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_MOTIVO : " & item("SOPOV_MOTIVO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_FLAG_SP : " & item("SOPOC_FLAG_SP"))
                                'IDEA-300846 - FIN

                                If dt.Item("DEPEV_NROTELEFONO") = item("SOPOC_INICIO_RANGO") Then
                                    If (DBNull.Value.Equals(item("SOPOV_MOTIVO"))) Then
                                        item("SOPOV_MOTIVO") = ""
                                    End If
                                    If (item("SOPOV_TIPO_MENSAJE").Equals(arrSPNo(0)) AndAlso item("SOPOV_MOTIVO").Equals(arrSPNo(1))) Then
                                        If (Convert.ToDateTime(dsPedidos.Tables(0).Rows(0)("PEDID_FECHADOCUMENTO")) >= DateTime.Now.AddDays(-1 * clsKeyAPP.consDiasPermitidosPagoPEP)) Then
                                        Return "2"
                                        Else
                                            Return "3"
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next

                    For i As Integer = 0 To arrEstadosSPPermitidos.Length - 1 'SPR|RABDCP|CPSPR|SP|ANS Estados de SP PERMITIDOS
                        If (arrEstadosSPPermitidos(i) <> Nothing AndAlso arrEstadosSPPermitidos(i) <> "" AndAlso arrEstadosSPPermitidos(i).Split(";"c).Length = 2) Then
                            Dim arrSP() As String = arrEstadosSPPermitidos(i).Split(";"c)
                            For Each item As DataRow In arraylistaSP.Tables(0).Rows

                                'IDEA-300846 - INI
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " SPR|RABDCP|CPSPR|SP|ANS Estados de SP PERMITIDOS")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_INICIO_RANGO : " & item("SOPOC_INICIO_RANGO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_TIPO_MENSAJE : " & item("SOPOV_TIPO_MENSAJE"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOV_MOTIVO : " & item("SOPOV_MOTIVO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " IDEA-300846 - SOPOC_FLAG_SP : " & item("SOPOC_FLAG_SP"))
                                'IDEA-300846 - FIN

                                If dt.Item("DEPEV_NROTELEFONO") = item("SOPOC_INICIO_RANGO") Then
                                    If (DBNull.Value.Equals(item("SOPOV_MOTIVO"))) Then
                                        item("SOPOV_MOTIVO") = ""
                                    End If
                                    If (item("SOPOV_TIPO_MENSAJE").Equals(arrSP(0)) AndAlso item("SOPOV_MOTIVO").Equals(arrSP(1)) Or (Funciones.CheckStr(item("SOPOC_FLAG_SP")) = "1")) Then
                                        Return "2"
                                    End If
                                End If
                            Next
                        End If
                    Next
                    'PROY-140223
                End If
            Next
        Next

        Return ""
    End Function
    'PROY-32089 F2 FIN

    'PROY-140335-INI
    Public Function ValidarSECProcesoSP(ByVal nroSEC As String, ByRef msjRpta As String, ByVal CurrentTerminal As String, ByVal CurrentUser As String, Optional ByVal nroPedido As String = "") As Integer
        Dim codrpta As Integer = 0
        Dim codRes As String = String.Empty
        Dim msjRes As String = String.Empty
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim idLog As String = nroSEC
        Try

           
            Dim oBWRegistraPorta As New BWRegistraPorta
            Dim audit As New ItemGenerico
            Dim resSalida = ""

            audit.CODIGO = ConfigurationSettings.AppSettings("CONS_APLICACION")
            audit.CODIGO2 = CurrentTerminal
            audit.CODIGO3 = CurrentUser
            audit.DESCRIPCION = Now.ToString("yyMMddhhmmssff")

            'INI: PROY-140262 BLACKOUT
            audit.CODIGO4 = clsKeyAPP.consFlagBlackOut
            audit.DESCRIPCION4 = clsKeyAPP.consMensajeProcesarSolicitudPortabilidadBlackOut
            'FIN: PROY-140262 BLACKOUT
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [NroSec] " & nroSEC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [NroPedido] " & nroPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [ItemGenerico] " & audit.DESCRIPCION)
            'INI: PROY-140262 BLACKOUT
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [ItemGenerico][CODIGO4] " & audit.CODIGO4)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [ItemGenerico][DESCRIPCION4] " & audit.DESCRIPCION4)
            'FIN: PROY-140262 BLACKOUT  
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [codRpta] ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta [msgRpta] ")

            oBWRegistraPorta.ProcesarSolicitud(audit, nroPedido, nroSEC, codRes, msjRes) ':: 5 ::
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta -  RESPUESTA: [codRpta]" & Funciones.CheckStr(codRes))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "BWRegistraPorta.ProcesarSolicitudPorta -  RESPUESTA: [msgRpta]" & Funciones.CheckStr(msjRes))

            codrpta = codRes
            msjRpta = msjRes
        Catch ex As Exception
            Console.WriteLine(ex.Message())
            Console.WriteLine(ex.Source())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PROY-26963 - IDEA-34399 |Error Método: ValidarSECProcesoSP" & ex.StackTrace.ToString())
        End Try

        Return codrpta
    End Function
 
    Public Function ValidarEstadoPendienteSP(ByVal objDatos As DataSet, ByVal dtLineas As DataSet, ByRef blnExistePEP As Boolean, ByRef strLista_SOPOC_ID_SOLICITUD As ArrayList, ByRef mensajeABDCP As String) As Boolean
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogActivacionesPostpago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Dim SOPOC_ID_SOLICITUD As String = String.Empty
        Dim MOTIVOSRECHAZO As String = String.Empty 'IDEA-300846
        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][INICIO]")
        Dim arrEstadosSPPendientes() As String = clsKeyAPP.consTipoMensajeEsperandoRespuestaSP.Split("|"c)
        Dim arrTipoMensajeFinalesSP() As String = clsKeyAPP.consTipoMensajeFinalesSP.Split("|"c)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][clsKeyAPP.consTipoMensajeEsperandoRespuestaSP]=>" & Funciones.CheckStr(clsKeyAPP.consTipoMensajeEsperandoRespuestaSP))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][clsKeyAPP.consTipoMensajeFinalesSP]=>" & Funciones.CheckStr(clsKeyAPP.consTipoMensajeFinalesSP))

        'IDEA-300846 INI
        For Each filaLineas As DataRow In objDatos.Tables(0).Rows
            Dim TIPO As String = String.Empty 
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][filaLineas.Item(SOPOV_TIPO_MENSAJE)=>" & Funciones.CheckStr(filaLineas.Item("SOPOV_TIPO_MENSAJE")))
            If (Funciones.CheckStr(filaLineas.Item("SOPOV_TIPO_MENSAJE")) = "RABDCP") Then
                mensajeABDCP = mensajeABDCP & " \n Linea " & filaLineas.Item("SOPOC_INICIO_RANGO") & " con Rechazo: " & Funciones.CheckStr(filaLineas.Item("MENSAJE_ABDCP")) ' MEJORA BLACKOUT
                If (Funciones.CheckStr(filaLineas.Item("SOPOV_MOTIVO")) = Funciones.CheckStr(ReadKeySettings.Key_MotivoErrorPin)) Then
                    TIPO = "1" ' ******************** MOTIVO ERROR DE PIN ********************
                ElseIf (Funciones.CheckStr(filaLineas.Item("SOPOV_MOTIVO")) = Funciones.CheckStr(ReadKeySettings.Key_MotivoRechazoDeuda)) Then
                    TIPO = "2" ' ******************** MOTIVO ERROR POR DEUDA ********************
                Else
                    TIPO = "3" ' ******************** OTROS MOTIVOS DE RECHAZO ********************
                End If
                    MOTIVOSRECHAZO = MOTIVOSRECHAZO + TIPO + "|"    
            End If
        Next
        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][MOTIVOSRECHAZO=>" & Funciones.CheckStr(MOTIVOSRECHAZO))

        If MOTIVOSRECHAZO <> "" Then
            Dim arrMsjRechazoLinea() As String = MOTIVOSRECHAZO.Split("|")
            If (InStr(1, MOTIVOSRECHAZO, "3") > 0) Then
                mensajeABDCP = Funciones.CheckStr(ReadKeySettings.Key_MsjSPRechazoNoPinNoDeudaCAC)
            Else
                If (InStr(1, MOTIVOSRECHAZO, "1") > 0) Then
                    mensajeABDCP = Funciones.CheckStr(ReadKeySettings.Key_MsjSPRechazoPinCAC)
                Else
                    mensajeABDCP = Funciones.CheckStr(ReadKeySettings.Key_MsjSPRechazoDeudaCAC)
                End If    
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][mensajeABDCP=>" & Funciones.CheckStr(mensajeABDCP))
        Else 'IDEA-300846 FIN
        For Each filaLineas As DataRow In objDatos.Tables(0).Rows
            For Each filaValidaSP As DataRow In dtLineas.Tables(0).Rows
                If filaLineas.Item("SOPOC_INICIO_RANGO") = filaValidaSP.Item("SOPOC_INICIO_RANGO") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][filaValidaSP.Item(SOPOC_INICIO_RANGO)]=>" & Funciones.CheckStr(filaValidaSP.Item("SOPOC_INICIO_RANGO")))
                    SOPOC_ID_SOLICITUD = String.Empty
                    SOPOC_ID_SOLICITUD = Funciones.CheckStr(filaValidaSP.Item("SOPOC_ID_SOLICITUD")) 'PROY-140223 
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][SOPOC_ID_SOLICITUD=>" & Funciones.CheckStr(SOPOC_ID_SOLICITUD))
                    If (Funciones.CheckStr(filaLineas.Item("SOPOV_TIPO_MENSAJE")) <> "PEP") Then 'PROY-140223
                        strLista_SOPOC_ID_SOLICITUD.Add(SOPOC_ID_SOLICITUD)
                        For k As Integer = 0 To arrEstadosSPPendientes.Length - 1
                            If (arrEstadosSPPendientes(k).Equals(Funciones.CheckStr(filaLineas.Item("SOPOV_TIPO_MENSAJE"))) Or ((Funciones.CheckStr(filaLineas.Item(("SOPOC_FLAG_SP"))) = "1") And (clsKeyAPP.consTipoMensajeFinalesSP.IndexOf(Funciones.CheckStr(filaLineas.Item("SOPOV_TIPO_MENSAJE")))) < 0)) Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][PENDIENTE][TRUE]")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][FIN]")
                                Return True
                                Exit Function
                            End If
                        Next
                    Else
                        blnExistePEP = True 'PROY-140223
                    End If
                End If
            Next
        Next
        End If 'IDEA-300846
        objFileLog.Log_WriteLog(pathFile, strArchivo, "[ValidarEstadoPendienteSP][FIN]")

        Return False
    End Function
'PROY-140335-FIN

    'JLOPETAS - PROY 140589 - INI
    Public Shared Function validaEnvioSolPortaDLV(ByVal strNroSec As String, ByVal dtsPedidos As DataSet) As Int64

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "LogValidaEnvioEstadoPortabilidad"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strNroSec
        Dim theReturn As Int64 = 0


        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Verificar CP Portabilidad (Pagar - ValidarSEC)")

            Dim objConsulta As New COM_SIC_Activaciones.ClsPortabilidad
            Dim strCodRpta As String = "", strMsgRpta As String = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - strNroSec: " & strNroSec)

            Dim objDatos As DataSet = objConsulta.ValidarSEC(strNroSec, strCodRpta, strMsgRpta) 'VALIDA QUE LA SEC NO TENGA PAGO, SE ENCUENTRE CON ESTADO SPR/CPSPR Y RABDCP
            Dim SecProcesoSP = getValidarSECProcesoSP(objDatos, dtsPedidos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - SecProcesoSP: " & SecProcesoSP)

            ''INI: PROY-140262 BLACKOUT
            'Dim strPedidoSunat As String = Funciones.CheckStr(dtsPedidos.Tables(0).Rows(0).Item("PEDIV_NROSUNAT"))
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - consFlagBlackOut  : " & clsKeyAPP.consFlagBlackOut)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - strPedidoSunat  : " & strPedidoSunat)
            'If clsKeyAPP.consFlagBlackOut = "1" AndAlso strPedidoSunat.Trim.Length > 0 Then
            '    SecProcesoSP = 2
            '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " PROY-140262 BLACKOUT - No realiza solictud de portabilidad por que viene de una anulacion")
            'End If
            ''FIN: PROY-140262 BLACKOUT


            If (SecProcesoSP = "1") Then 'SEC ES VALIDA, PERO NO TIENE SOLICITUD DE PORTABILIDAD
                theReturn = 1
            ElseIf (SecProcesoSP = "2") Then 'SEC ES VALIDA, Y TIENE SOLICITUD DE PORTABILIDAD
                theReturn = 2
            Else 'SEC NO ES VALIDA, NO PRECEDE 
                theReturn = 3
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "theReturn : " & Funciones.CheckStr(theReturn))

        Catch ex As Exception
            theReturn = -99
            objFileLog.EscribirLog(pathFile, strArchivo, strIdentifyLog, "ERROR ValidarSP - ex.Message", ex.Message.ToString())
            objFileLog.EscribirLog(pathFile, strArchivo, strIdentifyLog, "ERROR ValidarSP - ex.StackTrace", ex.StackTrace.ToString())
        End Try

        Return theReturn

    End Function
    'JLOPETAS - PROY 140589 - FIN

End Class
