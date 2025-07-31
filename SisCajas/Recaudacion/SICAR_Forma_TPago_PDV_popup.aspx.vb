Imports Thycotic.Web.RemoteScripting
Imports COM_SIC_Activaciones
Imports System.Text.RegularExpressions
Imports System.Text
Imports SisCajas.Funciones
Imports System.Collections
Imports System.Collections.Specialized.HybridDictionary
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
Imports COM_SIC_Procesa_Pagos.FormasPagoPDVRest
Imports AjaxPro
'INI-1019 - YGP
Public Class SICAR_Forma_TPago_PDV_popup
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hidRpta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlPDV As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFP As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnRegistrar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button

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
    Dim nombreArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim rutaSite As String = ConfigurationSettings.AppSettings("RutaSite")

    Dim idLog As String = String.Empty
    Dim mensajeAlerta As String = String.Empty
    Dim codigoTransaccion As String = String.Empty
    Dim descripcionTransaccion As String = String.Empty

    Private Shared listaFormaPagos As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType()
    Private Shared listaOficinasVenta As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaOficinasVenta()
    Private Shared listaFormaPagoPDV As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV()
    Private Shared listaFpPDV() As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV
    Private Shared listaFP() As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & rutaSite & "');</script>")
        If (Session("USUARIO") Is Nothing) Then

            Response.Redirect(rutaSite)
            Response.End()
            Exit Sub
        Else
            idLog = Session("ALMACEN") & "-" & Session("USUARIO") & " -- " & "INICIATIVA_1019"
            If Not Page.IsPostBack Then
                Inicio()
            End If
        End If
    End Sub

    Private Sub Inicio()
        btnRegistrar.Enabled = False
        ddlFP.Enabled = False
        CargarComboPDV()
    End Sub

    Private Sub CargarComboPDV()
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO CargarComboPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim mensajeRespuesta As String = String.Empty
            listaOficinasVenta = Session("ini1019-listaOficinasVenta")

            If (listaOficinasVenta Is Nothing) Then
                mensajeRespuesta = "Ha ocurrido un error al cargar las formas de pago. Intente nuevamente ingresando nuevamente a la opcion Configuracion --> Formas de Pago PDV"
                Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeRespuesta + "');</script>")
            Else
                ddlPDV.DataSource = listaOficinasVenta
                ddlPDV.DataValueField = "codPDV"
                ddlPDV.DataTextField = "descOficinaVenta"
                ddlPDV.DataBind()

                Dim item = New System.Web.UI.WebControls.ListItem("Seleccione...", "0")
                ddlPDV.Items.Insert(0, item)
            End If
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error al cargar las formas de pago."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - CargarComboPDV()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeAlerta + "');</script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN CargarComboPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub CargarComboFormasPago()
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO CargarComboFormasPago()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim listaFormaPagosTemp() As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType
            Dim contador As Integer = 0
            Dim codigoRespuesta As String = String.Empty
            Dim mensajeRespuesta As String = String.Empty
            Dim codigoPDV As String = ddlPDV.SelectedValue

            listaFormaPagos = Session("ini1019-listaFormaPagos")

            If (listaFormaPagos Is Nothing) Then
                mensajeRespuesta = "Ha ocurrido un error al cargar las formas de pago. Intente nuevamente ingresando nuevamente a la opcion Configuracion --> Formas de Pago PDV"
                Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeRespuesta + "');</script>")
            Else
                ObtenerFormasPagoPDV(codigoPDV, codigoRespuesta, mensajeRespuesta, listaFormaPagoPDV)

                If codigoRespuesta = 0 Then

                    If (listaFormaPagoPDV.Length > 0) Then

                        Dim coincidencia As Integer = 0
                        For Each formaPago As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType In listaFormaPagos

                            For Each formaPagoPDV As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV In listaFormaPagoPDV
                                If formaPago.codigoTipoPago = formaPagoPDV.ccins Then
                                    coincidencia = 1
                                    Exit For
                                End If
                            Next

                            If coincidencia = 0 Then
                                ReDim Preserve listaFormaPagosTemp(contador)
                                listaFormaPagosTemp(contador) = formaPago
                                contador = contador + 1
                            Else
                                coincidencia = 0
                            End If
                        Next
                    Else
                        listaFormaPagosTemp = listaFormaPagos
                    End If

                        If Not (listaFormaPagosTemp Is Nothing) Then
                            ddlFP.DataSource = listaFormaPagosTemp
                            ddlFP.DataValueField = "codigoTipoPago"
                            ddlFP.DataTextField = "descripcionTipoPago"
                            ddlFP.DataBind()
                            ddlFP.Enabled = True
                            btnRegistrar.Enabled = True
                        Else
                            ddlPDV.SelectedIndex = 0
                            ddlFP.Enabled = False
                            btnRegistrar.Enabled = False
                            mensajeRespuesta = "El punto de venta seleccionado ya tiene agregados todos los medios de pago posibles. Intente con otro punto de venta."
                            Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeRespuesta + "');</script>")
                        End If
                    Else
                    Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeRespuesta + "');</script>")
                End If
            End If
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error al cargar las formas de pago."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - CargarComboFormasPago()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeAlerta + "');</script>")
        Finally

        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN CargarComboFormasPago()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub ddlPDV_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPDV.SelectedIndexChanged
        If (ddlPDV.SelectedValue <> "0") Then
            btnRegistrar.Enabled = False
            ddlFP.Enabled = False
            ddlFP.Items.Clear()
            ddlFP.DataBind()
            CargarComboFormasPago()
        Else
            btnRegistrar.Enabled = False
            ddlFP.Enabled = False
            ddlFP.Items.Clear()
            ddlFP.DataBind()
        End If
    End Sub

    Private Sub btnRegistrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO btnRegistrar_Click()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Dim codigoRespuesta As String = String.Empty
        Dim mensajeRespuesta As String = String.Empty

        Try
            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objFormasPagoPDVRequest As New FormasPagoPDVRequest
            Dim objFormasPagosPDVResponse As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.FormasPagoPDVResponse
            Dim objBWFormasPagoPDV As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.BWFormasPagoPDV
            Dim codigoPDV As String = ddlPDV.SelectedValue
            Dim codigoMedioPago As String = ddlFP.SelectedValue

            listaFormaPagos = Session("ini1019-listaFormaPagos")

            For Each formaPago As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType In listaFormaPagos
                If formaPago.codigoTipoPago = codigoMedioPago And formaPago.estadoTipoPago = 0 Then
                    mensajeAlerta = "La forma de pago " + formaPago.descripcionTipoPago.Trim + " se encuentra DESHABILITADA de manera general. Primero habilitela desde la opcion Configuracion --> Forma de Pago o intente con otro medio de pago."
                    descripcionTransaccion = mensajeAlerta
                    objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - gridDetalle_ItemDataBound()", mensajeAlerta))
                    Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeAlerta + "');</script>")
                    Exit Sub
                End If
            Next

            ObtenerHeaderRequest(objHeaderRequest, "registrarFormaPagoPDV")
            ObtenerAuditoriaRequest(objBEAuditoriaREST)

            objFormasPagoPDVRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objFormasPagoPDVRequest.MessageRequest.Body.codPDV = codigoPDV
            objFormasPagoPDVRequest.MessageRequest.Body.ccins = codigoMedioPago
            objFormasPagoPDVRequest.MessageRequest.Body.usuario = CurrentUser

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "INPUTS SERVICIO objBWFormasPagoPDV.RegistrarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codPDV", codigoPDV))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codMedioPago", codigoMedioPago))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "usuario", CurrentUser))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "REQUEST", JavaScriptSerializer.Serialize(objFormasPagoPDVRequest).ToString()))

            objFormasPagosPDVResponse = objBWFormasPagoPDV.RegistrarFormaPagoPDV(objFormasPagoPDVRequest, objBEAuditoriaREST)

            codigoRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.codigoRespuesta
            mensajeRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTs SERVICIO objBWFormasPagoPDV.RegistrarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codigoRespuesta", codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeRespuesta", mensajeRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "RESPONSE", JavaScriptSerializer.Serialize(objFormasPagosPDVResponse).ToString()))

            If (codigoRespuesta = "0") Then
                mensajeAlerta = "Datos registrados correctamente."
                descripcionTransaccion = "Se registro correctamente la forma de pago en el PDV seleccionado."
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeAlerta", mensajeAlerta))
                Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeAlerta + "'); cerrarPopup('" + codigoPDV + "','" + codigoMedioPago + "');</script>")
            Else
                descripcionTransaccion = "No se registro correctamente la forma de pago en el PDV seleccionado."
            End If
        Catch ex As Exception
            mensajeRespuesta = "Exception: " + ex.Message
            mensajeAlerta = "Ha ocurrido un error al registrar la forma de pago por PDV."
            descripcionTransaccion = mensajeAlerta
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ObtenerFormasPagoPDV()", mensajeAlerta, mensajeRespuesta))
            Me.RegisterStartupScript("Mensaje", "<script>mensaje('" + mensajeAlerta + "');</script>")
        Finally
            codigoTransaccion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strRegistrarFormasPagoPDV"))
            RegistrarAuditoria(codigoTransaccion, descripcionTransaccion)
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN btnRegistrar_Click()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub ObtenerFormasPagoPDV(ByVal codigoPDV As String, ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String, _
        ByRef listaFormaPagoPDV As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV())

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO ObtenerFormasPagoPDV()"))

        Try
            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objFormasPagoPDVRequest As New FormasPagoPDVRequest
            Dim objFormasPagosPDVResponse As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.FormasPagoPDVResponse
            Dim objBWFormasPagoPDV As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.BWFormasPagoPDV
            Dim cantidadRegistros As Integer

            ObtenerHeaderRequest(objHeaderRequest, "consultarFormaPagoPDV")
            ObtenerAuditoriaRequest(objBEAuditoriaREST)

            objFormasPagoPDVRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objFormasPagoPDVRequest.MessageRequest.Body.codPDV = codigoPDV
            objFormasPagoPDVRequest.MessageRequest.Body.codMedioPago = ""

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "INPUTS SERVICIO objBWFormasPagoPDV.ConsultarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codPDV", ""))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codMedioPago", ""))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "REQUEST", JavaScriptSerializer.Serialize(objFormasPagoPDVRequest).ToString()))

            objFormasPagosPDVResponse = objBWFormasPagoPDV.ConsultarFormaPagoPDV(objFormasPagoPDVRequest, objBEAuditoriaREST)

            codigoRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.codigoRespuesta
            mensajeRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTs SERVICIO objBWFormasPagoPDV.ConsultarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codigoRespuesta", codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeRespuesta", mensajeRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "RESPONSE", JavaScriptSerializer.Serialize(objFormasPagosPDVResponse).ToString()))

            If (codigoRespuesta = "0") Then
                listaFormaPagoPDV = objFormasPagosPDVResponse.MessageResponse.Body.listaFormaPagoPDV
                cantidadRegistros = Funciones.CheckInt(listaOficinasVenta.Length)
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "Cantidad registros", cantidadRegistros))
            End If
        Catch ex As Exception
            codigoRespuesta = "-1"
            mensajeRespuesta = "Exception: " + ex.Message
            mensajeAlerta = "Ha ocurrido un error en el servicio al consultar las formas de pago del punto de venta registradas."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ObtenerFormasPagoPDV()", mensajeAlerta, mensajeRespuesta))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN ObtenerFormasPagoPDV()"))
    End Sub

    Private Sub ObtenerHeaderRequest(ByRef objHeaderRequest As COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest, ByVal operation As String)
        objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
        objHeaderRequest.dispositivo = ""
        objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
        objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
        objHeaderRequest.operation = operation
        objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
        objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
        objHeaderRequest.userId = ""
        objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))
    End Sub

    Private Sub ObtenerAuditoriaRequest(ByRef objBEAuditoriaREST As COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS)
        objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
        objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
        objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
        objBEAuditoriaREST.USRAPP = CurrentUser
        objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
        objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
        objBEAuditoriaREST.userId = CurrentUser
        objBEAuditoriaREST.nameRegEdit = ""
    End Sub

    Private Sub RegistrarAuditoria(ByVal codigoTransaccion As String, ByVal descripcionTransaccion As String)
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=========", "INICIO RegistrarAuditoria()"))

        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuarioId As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal

            Dim codigoServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim blnAuditoriaGrabado As Boolean

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "INPUTS objAuditoriaWS.RegistrarAuditoria"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "strCodTrans", codigoTransaccion))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "strCodServ", codigoServicio))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "ipcliente", ipcliente))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "nombreHost", nombreHost))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "ipServer", ipServer))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "nombreServer", nombreServer))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "usuario_id", usuarioId))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "descTrans", descripcionTransaccion))

            blnAuditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(codigoTransaccion, codigoServicio, ipcliente, nombreHost, ipServer, nombreServer, usuarioId, "", "0", descripcionTransaccion)

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTS objAuditoriaWS.RegistrarAuditoria"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "blnAuditoriaGrabado", blnAuditoriaGrabado))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - RegistrarAuditoria()", ex.Message.ToString()))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=========", "FIN RegistrarAuditoria()"))
    End Sub
End Class
