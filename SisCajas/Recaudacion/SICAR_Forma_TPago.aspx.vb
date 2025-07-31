Imports Thycotic.Web.RemoteScripting
Imports COM_SIC_Activaciones
Imports System.Text.RegularExpressions
Imports System.Text
Imports SisCajas.Funciones
Imports System.Collections
Imports System.Collections.Specialized.HybridDictionary
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest


Public Class SICAR_Forma_TPago
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgFormaTPago As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnExportar As System.Web.UI.WebControls.Button 'INI-936 - YGP

    

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdFormaPago As String = String.Empty
    Public Shared listaFormaPagos As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType() 'INI-936 - YGP

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            strIdFormaPago = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "
            If Not Page.IsPostBack Then
                CargarGrilla()
            End If

        End If

    End Sub

    Private Sub ActualizarEstados(ByVal strVariableClob As String, ByRef strCodigoRpta As String, ByRef strMensajeRpta As String)

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======= INICIO ActualizarEstados()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
        Try

            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objGestionaRecaudacionRequest As New GestionaRecaudacionRequest
            Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
            Dim objGestionaRecaudacion As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion

            objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
            objHeaderRequest.dispositivo = ""
            objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
            objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
            objHeaderRequest.operation = "actualizarFormaPago"
            objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
            objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
            objHeaderRequest.userId = ""
            objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))




            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest




            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            objGestionaRecaudacionRequest.MessageRequest.Body.variableClob = strVariableClob

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} ", strIdFormaPago, "-----------------------------------------------------"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdFormaPago, "LOG_INICIATIVA_318", " INPUT SERVICIO ActualizarFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strVariableClob", strVariableClob))


            objGestionaRecaudacionesResponse = objGestionaRecaudacion.ActualizarFormaPagos(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            strCodigoRpta = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            strMensajeRpta = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdFormaPago, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ActualizarFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} ", strIdFormaPago, "-----------------------------------------------------"))


        Catch ex As Exception
            strCodigoRpta = "-1"
            strMensajeRpta = "Error en el Servicio ActualizarFormaPagos"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ERROR - ActualizarEstados() Mensaje ", ex.Message.ToString()))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ERROR - Servicio ", "objGestionaRecaudacion.ActualizarFormaPagos"))
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======= FIN ActualizarEstados()  ============"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))

    End Sub

    Private Sub CargarGrilla()


        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======= INICIO CargarGrilla()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======================================="))


        Try

            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objGestionaRecaudacionRequest As New GestionaRecaudacionRequest
            Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
            Dim objGestionaRecaudacion As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion

            objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
            objHeaderRequest.dispositivo = ""
            objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
            objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
            objHeaderRequest.operation = "consultarFormaPago"
            objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
            objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
            objHeaderRequest.userId = ""
            objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))



            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest



            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            objGestionaRecaudacionesResponse = objGestionaRecaudacion.ConsultaFormaPagos(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            Dim strCodigoRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            Dim strMensajeRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdFormaPago, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ConsultaFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))


            If (strCodigoRpta = "0") Then
                listaFormaPagos = objGestionaRecaudacionesResponse.MessageResponse.Body.listaFormaPagoType
                Dim intCantidadReg As Integer = Funciones.CheckInt(listaFormaPagos.Length)

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "Cantidad Reg", intCantidadReg))

                Session("listaFormaPagos") = listaFormaPagos 'INI-936 - YGP

                dgFormaTPago.DataSource = listaFormaPagos
                dgFormaTPago.DataBind()
            Else
                Dim srtAlerta As String = "Error en el servicio ConsultaFormaPagos"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
            End If

        Catch ex As Exception
            Dim strMsjError As String = "Error en el servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ERROR - CargarGrilla() Mensaje ", ex.Message.ToString()))
            Response.Write("<script language=jscript> alert('" + strMsjError + "'); </script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "========= FIN CargarGrilla()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "======================================="))

    End Sub

    'INI-936 - YGP - Evento ItemDataBound modificado para cargar el nombre del medio de pago
    Private Sub dgFormaTPago_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgFormaTPago.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem Then
            If dgFormaTPago.EditItemIndex <> -1 Then
                Dim chkControl As CheckBox = CType(e.Item.FindControl("chkEstadoFPago"), CheckBox)
                chkControl.Enabled = True
                Dim txtDescripcionFP As TextBox = CType(e.Item.FindControl("txtDescMedioPago"), TextBox)
                txtDescripcionFP.Text = Trim(txtDescripcionFP.Text)
            End If
        End If
    End Sub

    'INI-936 - YGP - Metodo nuevo para resfrescar la informaciòn de la grilla
    Private Sub RefrescarGrilla()
        dgFormaTPago.DataSource = listaFormaPagos
        dgFormaTPago.DataBind()
    End Sub

    'INI-936 - YGP - Metodo nuevo para editar la informaciòn del item
    Public Sub EditarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        dgFormaTPago.EditItemIndex = e.Item.ItemIndex
        btnExportar.Enabled = False
        RefrescarGrilla()
    End Sub

    'INI-936 - YGP - Metodo nuevo que reemplaza al evento del botón Grabar
    Public Sub ActualizarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========== INI ActualizarItem()========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========================================="))
                Dim strCodigoRpta As String = String.Empty
                Dim strMensajeRpt As String = String.Empty
        Dim strCodTrans As String = String.Empty
        Dim strDescTrans As String = String.Empty
                
        Try
            Dim idTipoPago As Label = CType(e.Item.FindControl("idTipoPago"), Label)
            Dim descMedioPago As TextBox = CType(e.Item.FindControl("txtDescMedioPago"), TextBox)
            Dim chkEstadoPago As CheckBox = CType(e.Item.FindControl("chkEstadoFPago"), CheckBox)

            If Trim(descMedioPago.Text).Length > 0 Then
                dgFormaTPago.EditItemIndex = -1
                btnExportar.Enabled = True

                Dim strVariableClob As String = String.Format("{0},{1},{2}", idTipoPago.Text, Trim(descMedioPago.Text), IIf(chkEstadoPago.Checked = True, "1", "0"))
                ActualizarEstados(strVariableClob, strCodigoRpta, strMensajeRpt)

                If (strCodigoRpta = "0") Then
                    strMensajeRpt = "Proceso exitoso"
                    strDescTrans = "Se Actualizó correctamente las formas de pago"
                    CargarGrilla()
                Else
                    strMensajeRpt = "Ocurrio un error en el servicio"
                    strDescTrans = "Error al actualizar las formas de pago"
                    RefrescarGrilla()
                End If
            Else
                strMensajeRpt = "El campo Descripcion no puede estar vacio."
            End If
        Catch ex As Exception
            strDescTrans = "Error al actualizar las formas de pago"
            strMensajeRpt = "Error al grabar"
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_936", "ERROR - ActualizarItem() - Mensaje ", ex.Message.ToString()))
        Finally
            strCodTrans = Funciones.CheckStr(ConfigurationSettings.AppSettings("codTransActualizarFormaPago"))
            RegistrarAuditoria(strCodTrans, strDescTrans)
            Response.Write("<script language=jscript>alert('" + strMensajeRpt + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_936", "ActualizarItem() - Mensaje ", strMensajeRpt))
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========== FIN ActualizarItem()========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_936", "=========================================="))
    End Sub

    'INI-936 - YGP - Metodo nuevo para implementar el evento OnCancel del datagridview
    Public Sub CancelarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        dgFormaTPago.EditItemIndex = -1
        RefrescarGrilla()
        btnExportar.Enabled = True
    End Sub

    'INI-936 - YGP - Metodo nuevo para implementar el evento PageIndexChanged del datagridview
    Private Sub dgFormaTPago_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFormaTPago.PageIndexChanged
        dgFormaTPago.DataSource = listaFormaPagos
        dgFormaTPago.CurrentPageIndex = e.NewPageIndex
        dgFormaTPago.DataBind()
    End Sub

    'INI-936 - YGP - Metodo nuevo para boton Exportar que redirige a la pagina de Excel para exportar
    Private Sub btnExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Session("listaFormaPagos") = listaFormaPagos
        Response.Redirect("SICAR_Forma_TPago_Excel.aspx")
    End Sub

    Private Sub RegistrarAuditoria(ByVal strCodTrans As String, ByVal descTrans As String)

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "========= INICIO RegistrarAuditoria()========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))

        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim blnAuditoriaGrabado As Boolean


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}", strIdFormaPago, "LOG_INICIATIVA_318", "INPUT objAuditoriaWS.RegistrarAuditoria :"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strCodTrans", strCodTrans))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "strCodServ", strCodServ))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ipcliente", ipcliente))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "nombreHost", nombreHost))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ipServer", ipServer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "nombreServer", nombreServer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "usuario_id", usuario_id))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "descTrans", descTrans))


            blnAuditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}", strIdFormaPago, "LOG_INICIATIVA_318", "OUTPUT objAuditoriaWS.RegistrarAuditoria :"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "blnAuditoriaGrabado", blnAuditoriaGrabado))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdFormaPago, "LOG_INICIATIVA_318", "ERROR - RegistrarAuditoria() Mensaje ", ex.Message.ToString()))
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "========== FIN RegistrarAuditoria()=========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdFormaPago, "LOG_INICIATIVA_318", "============================================="))
    End Sub

End Class
