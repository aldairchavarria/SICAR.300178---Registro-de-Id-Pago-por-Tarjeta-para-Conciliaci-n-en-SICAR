Imports Thycotic.Web.RemoteScripting
Imports COM_SIC_Activaciones
Imports System.Text.RegularExpressions
Imports System.Text
Imports SisCajas.Funciones
Imports System.Collections
Imports System.Collections.Specialized.HybridDictionary
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
Imports System.Globalization

Public Class DevolSaldoFavor
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidSwOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdProcesar As System.Web.UI.WebControls.Button
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtCodigoCuenta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoSaldoFavor As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnValidar As System.Web.UI.WebControls.Button
    Protected WithEvents totalRecibos As System.Web.UI.WebControls.Label
    Protected WithEvents divDocumentos As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents pnlBotones As System.Web.UI.WebControls.Panel
    Protected WithEvents txtTipoProducto As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidCodDevolSaldo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hidCargaPool As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoProducto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodigoCuenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Botones As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents hidBiometriaHit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMontoSaldo As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Dim strIdSaldoFavor As String = String.Empty


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else

            txtCodigoCuenta.Attributes.Add("onkeydown", "validarNumero(this.value);")
            txtMontoSaldoFavor.Attributes.Add("onkeydown", "validarNumero(this.value);")

            Dim strFecha As String

            If Not Page.IsPostBack Then
                If txtFecha.Text = "" Then
                    txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)
                End If
                CargarGrilla()
            Else
                CargarGrilla()
            End If

        End If


    End Sub

    Private Sub CargarGrilla()

        strIdSaldoFavor = txtFecha.Text

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======= INICIO CargarGrilla()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))

        Dim listaDevolucionSaldo As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaDevolucionSaldoBean()

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


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))

            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest
            objGestionaRecaudacionRequest.MessageRequest.Body.fechaInicio = txtFecha.Text
            objGestionaRecaudacionRequest.MessageRequest.Body.fechaFin = txtFecha.Text

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            objGestionaRecaudacionesResponse = objGestionaRecaudacion.obtenerDevolucionSaldo(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            Dim strCodigoRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            Dim strMensajeRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdSaldoFavor, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ConsultaFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))


            If (strCodigoRpta = "0") Then
                listaDevolucionSaldo = objGestionaRecaudacionesResponse.MessageResponse.Body.listaDevolucionSaldoBean
                Dim intCantidadReg As Integer = Funciones.CheckInt(listaDevolucionSaldo.Length)

                dgPool.DataSource = listaDevolucionSaldo
                dgPool.DataBind()
                hidCargaPool.Value = "1"
            Else
                hidCargaPool.Value = "0"
                Dim srtAlerta As String = "Error al Cargar la Grilla, servicio obtenerDevolucionSaldo"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
            End If

        Catch ex As Exception
            Dim strMsjError As String = "Error en el servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "ERROR - CargarGrilla() Mensaje ", ex.Message.ToString()))
            dgPool.DataSource = listaDevolucionSaldo
            dgPool.DataBind()
            hidCargaPool.Value = "0"
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "========= FIN CargarGrilla()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))


    End Sub

    Private Sub cmdProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcesar.Click

        strIdSaldoFavor = hidCodDevolSaldo.Value

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======= INICIO ValidacionProcesarDevolucion()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))

        If (hidBiometriaHit.Value = " " Or hidBiometriaHit.Value = "" Or hidBiometriaHit.Value = "0") Then
            Dim srtAlerta As String = "La devolucion no ha pasado por el proceso de Identificación Biometrica desde el SISACT"
            Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
            Exit Sub
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - hidTipoProducto : " & hidTipoProducto.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - hidCodigoCuenta : " & hidCodigoCuenta.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - txtMontoSaldoFavor : " & txtMontoSaldoFavor.Text)


        If (hidTipoProducto.Value = "" Or hidCodigoCuenta.Value = "" Or txtMontoSaldoFavor.Text = "" Or hidTipoProducto.Value = " " Or hidCodigoCuenta.Value = " " Or txtMontoSaldoFavor.Text = " ") Then
            Dim srtAlerta As String = "Por favor de validar la Devolucion"
            Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
            Exit Sub
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - hidTipoProducto : " & hidTipoProducto.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - hidCodigoCuenta : " & hidCodigoCuenta.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - txtMontoSaldoFavor : " & txtMontoSaldoFavor.Text)

        '***** INI :: Valida la asignacion de caja *****
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim dsCajeroA As DataSet

        Try


            '****Obtiene Datos del pedido ****
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - INI Valida Asignacion de Caja ")

            Dim cultureNameX As String = "es-PE"
            Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
            Dim dateTimeValueCaja As DateTime = Convert.ToDateTime(DateTime.Now, cultureX)
            Dim dFecha2 As Date
            Dim sFecha2 As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
            Dim sCajero As String = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")
            Dim sOficina As String = Funciones.CheckStr(Session("ALMACEN"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - Fecha Pedido : " & sFecha2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - Oficina : " & sOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - Cajero : " & sCajero)
            dsCajeroA = objOfflineCaja.GetDatosAsignacionCajero(sOficina, sFecha2, sCajero)
            If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- Cantidad de Registros devueltos: " & dsCajeroA.Tables(0).Rows.Count.ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- No se pudo determinar el numero de la caja asignada.")
                Response.Write("<script>alert('No se encontro el Nùmero/Nombre de caja asignada.')</script>")
                Exit Sub
            End If

            ' Validar cierre de caja
            If Not dsCajeroA Is Nothing Then
                For cont As Int32 = 0 To dsCajeroA.Tables(0).Rows.Count - 1
                    If dsCajeroA.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - " & "MENSAJE : " & "Caja Cerrada, no es posible realizar el pago.")
                        Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", "Caja Cerrada, no es posible realizar el pago.")
                        Me.RegisterStartupScript("RegistraAlerta", script)
                        Exit Sub
                    End If
                Next
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- Error al tratar de consultar los datos de CAJA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- Error: " & ex.Message.ToString)
        Finally
            objOfflineCaja = Nothing
            dsCajeroA.Dispose()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - FIN Valida Asignacion de Caja ")
        End Try
        '***** FIN :: Valida la asignacion de caja *****

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "========= FIN ValidacionProcesarDevolucion()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))


        '''''ProcesarDevolucion''''''

        strIdSaldoFavor = hidCodDevolSaldo.Value

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======= INICIO ProcesarDevolucion()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))

        Try
            Dim srtAlerta As String = ""

            If (hidTipoProducto.Value = "" Or hidCodigoCuenta.Value = "" Or txtMontoSaldoFavor.Text = "") Then
                srtAlerta = "Por favor de validar la Devolucion"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                Exit Sub
            End If

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

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))

            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvCodigo = hidCodDevolSaldo.Value
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvEstado = "D"
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvMonto = txtMontoSaldoFavor.Text
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvUsuario = "SICAR"
            objGestionaRecaudacionRequest.MessageRequest.Body.accion = "2"

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            'objGestionaRecaudacionesResponse = objGestionaRecaudacion.RegistrarDevolucionSaldo(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            'Dim strCodigoRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            'Dim strMensajeRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            'objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdSaldoFavor, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ConsultaFormaPagos "))
            'objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            'objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))


            'If (strCodigoRpta <> "0") Then
            '    srtAlerta = "Error en el servicio RegistrarDevolucionSaldo"
            '    Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
            '     Exit Sub
            'End If

        Catch ex As Exception
            Dim strMsjError As String = "Error en el servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "ERROR - CargarGrilla() Mensaje ", ex.Message.ToString()))

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "========= FIN ProcesarDevolucion()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))

        '''''ProcesarDevolucion''''''

        '''''ActualizarDevolucion''''''


        strIdSaldoFavor = hidCodDevolSaldo.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======= INICIO ActualizarDevolucion()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))

        Try
            Dim srtAlerta As String = ""

            If (hidTipoProducto.Value = "" Or hidCodigoCuenta.Value = "" Or txtMontoSaldoFavor.Text = "") Then
                srtAlerta = "Por favor de validar la Devolucion"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                Exit Sub
            End If

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

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))

            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvCodigo = hidCodDevolSaldo.Value
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvEstado = "D"
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvMonto = txtMontoSaldoFavor.Text
            objGestionaRecaudacionRequest.MessageRequest.Body.devolvUsuario = "SICAR"
            objGestionaRecaudacionRequest.MessageRequest.Body.accion = "2"

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            objGestionaRecaudacionesResponse = objGestionaRecaudacion.RegistrarDevolucionSaldo(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            Dim strCodigoRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            Dim strMensajeRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdSaldoFavor, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ConsultaFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))


            If (strCodigoRpta <> "0") Then
                srtAlerta = "Error en el servicio RegistrarDevolucionSaldo"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                Exit Sub
            End If

        Catch ex As Exception
            Dim strMsjError As String = "Error en el servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "ERROR - CargarGrilla() Mensaje ", ex.Message.ToString()))

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "========= FIN ActualizarDevolucion()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))


        '''''ActualizarDevolucion''''''



        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim CodParamRecaudacion As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoRecaudacion"))
        Dim dsParametros As DataSet
        Dim str_key_ClaseFacturaDevSaldoFavor As String
        Dim str_key_DescDocDevSaldoFavor As String
        Dim str_key_MetodoPagoFavor As String


        objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Consulta Parametros: Tipo de Arqueo")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Método : objConsultaPvu.ObtenerSISACT_Parametros")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " CodParamRecaudacion: " & CodParamRecaudacion)
        dsParametros = objConsultaPvu.ObtenerSISACT_Parametros(CodParamRecaudacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Cantidad de registros: " & dsParametros.Tables(0).Rows.Count.ToString)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Consulta Parametros: Tipo de Arqueo")

        If dsParametros.Tables(0).Rows.Count > 0 Then

            For Each fila As DataRow In dsParametros.Tables(0).Rows
                Select Case fila(3)
                    Case "key_ClaseFacturaDevSaldoFavor"
                        str_key_ClaseFacturaDevSaldoFavor = Funciones.CheckStr(fila(2))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "str_key_ClaseFacturaDevSaldoFavor" & " : " & Funciones.CheckStr(str_key_ClaseFacturaDevSaldoFavor))
                    Case "key_DescDocDevSaldoFavor"
                        str_key_DescDocDevSaldoFavor = Funciones.CheckStr(fila(2))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "str_key_DescDocDevSaldoFavor" & " : " & Funciones.CheckStr(str_key_DescDocDevSaldoFavor))
                    Case "key_MetodoPagoFavor"
                        str_key_MetodoPagoFavor = Funciones.CheckStr(fila(2))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "str_key_MetodoPagoFavor" & " : " & Funciones.CheckStr(str_key_MetodoPagoFavor))
                End Select
            Next

        End If

        Dim objCaja As New COM_SIC_Cajas.clsCajas
        strIdSaldoFavor = hidCodDevolSaldo.Value
        Dim strUsuario As String = Session("USUARIO")
        Dim strOficina As String = Session("ALMACEN")
        Dim nImporte As Decimal = Decimal.Parse(txtMontoSaldoFavor.Text)
        'Inicio - Proceso de Cuadre de Caja Para la Devolucion
        Dim fechaCajas As String = ""

        fechaCajas = Format(Now.Year, "0000").ToString.Trim & Format(Now.Month, "00").ToString.Trim & Format(Now.Day, "00").ToString.Trim

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- fechaCajas : " & fechaCajas.ToString)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " Inicio método : FP_InsertaEfectivo")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Oficina: " & strOficina)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Usuario: " & strUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Importe: - " & txtMontoSaldoFavor.Text)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     fecha: " & fechaCajas)
        Dim dFecha As Date
        Dim sFecha As String = Format(Now.Day, "00").ToString.Trim & "/" & Format(Now.Month, "00").ToString.Trim & "/" & Format(Now.Year, "0000").ToString.Trim

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & " - Fecha Pedido : " & sFecha)
        Try
            nImporte = Convert.ToDouble(txtMontoSaldoFavor.Text)
            objCaja.FP_InsertaEfectivo(strOficina, strUsuario, nImporte * (-1), sFecha)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "Entro al Error de insertar efectivo")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "Error " & ex.Message.ToString())
        End Try





        Dim KEY_CLASEFACTURADEV As String = ""
        Dim KEY_MEDIOPAGODEV As String = ""
        Dim resCajas As Integer
        Dim P_ID_TI_VENTAS_FACT As String = ""
        Dim P_MSGERR As String = ""
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "Inicio método : Consultar Pago Caja")

            KEY_CLASEFACTURADEV = Funciones.CheckStr(str_key_ClaseFacturaDevSaldoFavor) 'PARAMETRO

            KEY_MEDIOPAGODEV = Funciones.CheckStr(str_key_ClaseFacturaDevSaldoFavor) 'PARAMETRO

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Metodo: " & "objCaja.RegistrarPago")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_OFICINA_VENTA		: " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_FECHA				: " & fechaCajas)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_CAJERO             : " & Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_DESC_DOCUMENTO     : " & str_key_DescDocDevSaldoFavor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_FACTURA_FICTICIA   : " & strIdSaldoFavor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_REFERENCIA         : " & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_VENDEDOR           : " & Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_MONEDA             : " & System.Configuration.ConfigurationSettings.AppSettings("MONEDA"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_CLASE_FACTURA_COD  : " & KEY_CLASEFACTURADEV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_NRO_CUOTAS         : " & "0")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_TOTAL_FACTURA      : " & nImporte)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_SALDO              : " & "0")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_REFERENCIA_ORIG    : " & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_ESTADO             : " & System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_NODO               : " & Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_USUARIO_CREACION   : " & System.Configuration.ConfigurationSettings.AppSettings("Usuario_Aplicacion"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_ID_TI_VENTAS_FACT  : " & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_MSGERR             : " & "")

            resCajas = objCaja.RegistrarPago(Session("ALMACEN"), _
                                            fechaCajas, _
                                            Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                            str_key_DescDocDevSaldoFavor, _
                                            strIdSaldoFavor, _
                                            "", _
                                            Microsoft.VisualBasic.Right(Funciones.CheckStr("0000000000" & Session("USUARIO")), 10), _
                                            System.Configuration.ConfigurationSettings.AppSettings("MONEDA"), _
                                            KEY_CLASEFACTURADEV, _
                                            "0", _
                                            nImporte, _
                                            "0", _
                                            "", _
                                            System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                            Funciones.CheckStr(Right(System.Net.Dns.GetHostName, 2)), _
                                            System.Configuration.ConfigurationSettings.AppSettings("Usuario_Aplicacion"), _
                                            P_ID_TI_VENTAS_FACT, _
                                            P_MSGERR)

            '                       ByVal P_OFICINA_VENTA As String, _  OK
            '                        ByVal P_FECHA As String, _  OK
            '                        ByVal P_CAJERO As String, _ OK
            '                        ByVal P_DESC_DOCUMENTO As String, _ PARAMETRO OK
            '                        ByVal P_FACTURA_FICTICIA As String, _ IDPEDIDO DE SALDO A FAVOR OK
            '                        ByVal P_REFERENCIA As String, _  ID DE COMPROBANTE DE PAGO - PENDIENTE
            '                        ByVal P_VENDEDOR As String, _   OK
            '                        ByVal P_MONEDA As String, _ OK
            '                        ByVal P_CLASE_FACTURA_COD As String, _  PARAMETRO OK
            '                        ByVal P_NRO_CUOTAS As String, _ OK
            '                        ByVal P_TOTAL_FACTURA As Double, _ OK
            '                        ByVal P_SALDO As Double, _ OK
            '                        ByVal P_REFERENCIA_ORIG As String, _ ID DE COMPROBANTE DE PAGO - PENDIENTE
            '                        ByVal P_ESTADO As String, _  OK
            '                        ByVal P_NODO As String, _ OK
            '                        ByVal P_USUARIO_CREACION As String, _ OK
            '                        ByRef P_ID_TI_VENTAS_FACT As String, _ OK
            '                        ByVal P_MSGERR As String, _ OK
            '                        Optional ByVal P_NRO_TELEFONO As String = "", _ OK
            '                        Optional ByVal P_NROOPE_TRACE As String = "") As Integer OK


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "Fin método : RegistrarPago")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "Error al tratar de ejecutar el método:  RegistrarPago")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Exception: " & ex.ToString() & MaptPath)
            'FIN PROY-140126

        End Try

        Try
            If P_ID_TI_VENTAS_FACT > 0 Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " Código generado en la tabla TI_VENTAS_FACT=> " & P_ID_TI_VENTAS_FACT.ToString)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_FECHA              : " & fechaCajas)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_FACTURA_FICTICIA   : " & strIdSaldoFavor)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_REFERENCIA         : " & str_key_DescDocDevSaldoFavor)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_CLASE_FACTURA_COD  : " & KEY_CLASEFACTURADEV)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_MEDIO_PAGO         : " & KEY_MEDIOPAGODEV)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_REF_NC             : " & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_MONTO              : " & nImporte)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_ESTADO             : " & System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_USUARIO_CREACION   : " & System.Configuration.ConfigurationSettings.AppSettings("Usuario_Aplicacion"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & " P_MSGERR             : " & "")


                resCajas = objCaja.RegistrarPagoDetalle(P_ID_TI_VENTAS_FACT, _
                                                        fechaCajas, _
                                                        strIdSaldoFavor, _
                                                        str_key_DescDocDevSaldoFavor, _
                                                        KEY_CLASEFACTURADEV, _
                                                        KEY_MEDIOPAGODEV, _
                                                        "", _
                                                        nImporte, _
                                                        System.Configuration.ConfigurationSettings.AppSettings("ESTADO_VENTA_FLUJOCAJA"), _
                                                        System.Configuration.ConfigurationSettings.AppSettings("Usuario_Aplicacion"), _
                                                        P_MSGERR)

                'ByVal P_ID_TI_VENTAS_FACT As Int64, _  OK
                'ByVal P_FECHA As String, _    OK
                'ByVal P_FACTURA_FICTICIA As String, _ OK
                'ByVal P_REFERENCIA As String, _ OK PARAMETRO
                'ByVal P_CLASE_FACTURA_COD As String, _ OK PARAMETRO
                'ByVal P_MEDIO_PAGO As String, _  OK PARAMETRO
                'ByVal P_REF_NC As String, _ ID DE COMPROBANTE DE PAGO - PENDIENTE
                'ByVal P_MONTO As Double, _  OK
                'ByVal P_ESTADO As String, _ OK
                'ByVal P_USUARIO_CREACION As String, _ OK
                'ByRef P_MSGERR As String) As Integer
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "Error al tratar de ejecutar el método:  RegistrarPagoDetalle")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: GrabarDevolucion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdSaldoFavor & "- " & "     Exception: " & ex.ToString() & MaptPath)
            'FIN PROY-140126

        End Try

        'Fin - Proceso de Cuadre de Caja Para la Devolucion
        '********************************************************************************************************************

        txtCodigoCuenta.Text = ""
        txtMontoSaldoFavor.Text = ""
        txtTipoProducto.Text = ""
        hidTipoProducto.Value = ""
        hidCodigoCuenta.Value = ""
        hidTipoProducto.Value = ""

        CargarGrilla()
    End Sub

    Private Sub btnValidar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidar.Click

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======= INICIO CargarGrilla()  ========"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))


        Try
            Dim srtAlerta As String = ""

            If (hidTipoProducto.Value = "" Or hidCodigoCuenta.Value = "") Then
                srtAlerta = "Por favor de Selecionar un Registro de devolucion"
                Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                Exit Sub
            End If

            txtTipoProducto.Text = hidTipoProducto.Value
            txtCodigoCuenta.Text = hidCodigoCuenta.Value

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


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "consumer", objHeaderRequest.consumer))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "country", objHeaderRequest.country))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "dispositivo", objHeaderRequest.dispositivo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "language", objHeaderRequest.language))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "modulo", objHeaderRequest.modulo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "msgType", objHeaderRequest.msgType))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "operation", objHeaderRequest.operation))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "pid", objHeaderRequest.pid))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "system", objHeaderRequest.system))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "timestamp", objHeaderRequest.timestamp))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objHeaderRequest.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "wsIp", objHeaderRequest.wsIp))

            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objGestionaRecaudacionRequest.MessageRequest.Body.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmss")
            objGestionaRecaudacionRequest.MessageRequest.Body.codigoAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            objGestionaRecaudacionRequest.MessageRequest.Body.tipoServicio = hidTipoProducto.Value
            objGestionaRecaudacionRequest.MessageRequest.Body.clienteNroCuenta = hidCodigoCuenta.Value
            objGestionaRecaudacionRequest.MessageRequest.Body.usuarioAplicaion = ConfigurationSettings.AppSettings("Usuario_Aplicacion")

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "idTransaccion", objGestionaRecaudacionRequest.MessageRequest.Body.idTransaccion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "codigoAplicacion", objGestionaRecaudacionRequest.MessageRequest.Body.codigoAplicacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "tipoServicio", objGestionaRecaudacionRequest.MessageRequest.Body.tipoServicio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "clienteNroCuenta", objGestionaRecaudacionRequest.MessageRequest.Body.clienteNroCuenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "usuarioAplicaion", objGestionaRecaudacionRequest.MessageRequest.Body.usuarioAplicaion))

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IDTRANSACCION", objBEAuditoriaREST.IDTRANSACCION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "IPAPLICACION", objBEAuditoriaREST.IPAPLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "idTransaccionNegocio", objBEAuditoriaREST.idTransaccionNegocio))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "USRAPP", objBEAuditoriaREST.USRAPP))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "applicationCodeWS", objBEAuditoriaREST.applicationCodeWS))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "APLICACION", objBEAuditoriaREST.APLICACION))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "userId", objBEAuditoriaREST.userId))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2} -->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "nameRegEdit", objBEAuditoriaREST.nameRegEdit))

            objGestionaRecaudacionesResponse = objGestionaRecaudacion.ValidarDevolucionSaldo(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            Dim strSaldoFavorRpta As String = ""
            Dim strCodigoRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            Dim strMensajeRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta
            strSaldoFavorRpta = objGestionaRecaudacionesResponse.MessageResponse.Body.saldoFavor
            Dim strEstadoCuentaRpta As String = objGestionaRecaudacionesResponse.MessageResponse.Body.estadoCuenta

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}:", strIdSaldoFavor, "LOG_INICIATIVA_318", " OUTPUT SERVICIO ConsultaFormaPagos "))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strCodigoRpta", strCodigoRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strMensajeRpta", strMensajeRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strSaldoFavorRpta", strSaldoFavorRpta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "strEstadoCuentaRpta", strEstadoCuentaRpta))

            If (strCodigoRpta = "0") Then
                Dim strEstadoCuentaConfiguracion As String = ConfigurationSettings.AppSettings("EstCuentaDevolSaldo")

                If (strEstadoCuentaRpta = strEstadoCuentaConfiguracion And strSaldoFavorRpta <= 50) Then
                    txtMontoSaldoFavor.Text = strSaldoFavorRpta
                Else
                    srtAlerta = "No cuenta con los requisitos previos para realizar la Devolucion"
                    Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                End If
            Else
                If (strCodigoRpta = "1") Then
                    srtAlerta = "Cuenta del cliente no cuenta con lo requerido para la devolucion de saldo a favor"
                    Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                Else
                    srtAlerta = "Error en el servicio RegistrarDevolucionSaldo"
                    Response.Write("<script language=jscript> alert('" + srtAlerta + "'); </script>")
                End If
            End If

        Catch ex As Exception
            Dim strMsjError As String = "Error en el servicio"
            ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1} --{2}-->{3}", strIdSaldoFavor, "LOG_INICIATIVA_318", "ERROR - CargarGrilla() Mensaje ", ex.Message.ToString()))

        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "========= FIN CargarGrilla()  ========="))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-- {1}{2}", strIdSaldoFavor, "LOG_INICIATIVA_318", "======================================="))


    End Sub




End Class
