Imports Thycotic.Web.RemoteScripting
Imports COM_SIC_Activaciones
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.IO
Imports SisCajas.Funciones
Imports System.Collections
Imports System.Collections.Specialized.HybridDictionary
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
Imports COM_SIC_Procesa_Pagos.FormasPagoPDVRest
Imports AjaxPro

'INI-1019 - YGP
Public Class SICAR_Forma_TPago_PDV
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnExportar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPDV As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFP As System.Web.UI.WebControls.DropDownList
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents gridParaExportar As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents gridExportar As System.Web.UI.WebControls.DataGrid

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
    Dim idFormaPagoPDV As String = String.Empty
    Dim mensajeAlerta As String = String.Empty
    Dim codigoTransaccion As String = String.Empty
    Dim descripcionTransaccion As String = String.Empty

    Private Shared listaFormaPagos As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType()
    Private Shared listaOficinasVenta As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaOficinasVenta()
    Private Shared listaFormaPagoPDV As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV()

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Session("USUARIO") Is Nothing) Then
            Response.Redirect(rutaSite)
            Response.End()
            Exit Sub
        Else
            idLog = Session("ALMACEN") & "-" & Session("USUARIO") & " -- " & "INICIATIVA_1019"

            If Not Page.IsPostBack Then
                btnNuevo.Attributes.Add("OnClick", "f_Nuevo()")
                Inicio()
            End If
        End If
    End Sub

    Private Sub Inicio()
        CargarComboFormasPago()
        CargarComboPDV()
        CargarGridFormasPagoPDV()
    End Sub

    Private Sub CargarComboFormasPago()
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO CargarComboFormasPago()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim codigoRespuesta As String = String.Empty
            Dim mensajeRespuesta As String = String.Empty

            ObtenerFormasPago(codigoRespuesta, mensajeRespuesta, listaFormaPagos)

            If (codigoRespuesta = "0") Then
                Session("ini1019-listaFormaPagos") = listaFormaPagos
                ddlFP.DataSource = listaFormaPagos
                ddlFP.DataValueField = "codigoTipoPago"
                ddlFP.DataTextField = "descripcionTipoPago"
                ddlFP.DataBind()

                Dim item = New System.Web.UI.WebControls.ListItem("TODOS", "0")
                ddlFP.Items.Insert(0, item)
            Else
                mensajeAlerta = "Ha ocurrido un error en el servicio."
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - CargarComboFormasPago()", mensajeRespuesta))
                Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
            End If
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - CargarComboFormasPago()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=========", "FIN CargarComboFormasPago()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub ObtenerFormasPago(ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String, ByRef listaFormasPago As COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.listaFormaPagoType())
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO ObtenerFormasPago()"))

        Try
            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objGestionaRecaudacionRequest As New GestionaRecaudacionRequest
            Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
            Dim objGestionaRecaudacion As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion
            Dim cantidadRegistros As Integer = 0

            ObtenerHeaderRequest(objHeaderRequest, "consultarFormaPago")
            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            ObtenerAuditoriaRequest(objBEAuditoriaREST)

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "REQUEST", JavaScriptSerializer.Serialize(objGestionaRecaudacionRequest).ToString()))

            objGestionaRecaudacionesResponse = objGestionaRecaudacion.ConsultaFormaPagos(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            codigoRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            mensajeRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTs SERVICIO", "cargarComboFormasPago"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codigoRespuesta", codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeRespuesta", mensajeRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "RESPONSE", JavaScriptSerializer.Serialize(objGestionaRecaudacionesResponse).ToString()))

            If (codigoRespuesta = "0") Then
                listaFormaPagos = objGestionaRecaudacionesResponse.MessageResponse.Body.listaFormaPagoType
                cantidadRegistros = Funciones.CheckInt(listaFormaPagos.Length)
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "Cantidad registros", cantidadRegistros))
            End If
        Catch ex As Exception
            codigoRespuesta = "-1"
            mensajeRespuesta = "Exception: " + ex.Message
            mensajeAlerta = "Ha ocurrido un error en el servicio."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ObtenerFormasPagoP()", mensajeAlerta, mensajeRespuesta))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=========", "FIN ObtenerFormasPagoP()"))
    End Sub

    Private Sub CargarComboPDV()
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO CargarComboPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim codigoRespuesta As String = String.Empty
            Dim mensajeRespuesta As String = String.Empty

            ObtenerOficinasVenta(codigoRespuesta, mensajeRespuesta, listaOficinasVenta)

            If (codigoRespuesta = "0") Then
                Session("ini1019-listaOficinasVenta") = listaOficinasVenta
                ddlPDV.DataSource = listaOficinasVenta
                ddlPDV.DataValueField = "codPDV"
                ddlPDV.DataTextField = "descOficinaVenta"
                ddlPDV.DataBind()

                Dim item = New System.Web.UI.WebControls.ListItem("TODOS", "0")
                ddlPDV.Items.Insert(0, item)
            Else
                mensajeAlerta = "Ha ocurrido un error en el servicio. "
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - CargarComboPDV()", mensajeRespuesta))
                Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
            End If
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - CargarComboPDV()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN CargarComboPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub ObtenerOficinasVenta(ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String, ByRef listaOficinasVenta As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaOficinasVenta())
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO ObtenerOficinasVenta()"))

        Try
            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objFormasPagoPDVRequest As New FormasPagoPDVRequest
            Dim objFormasPagosPDVResponse As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.FormasPagoPDVResponse
            Dim objBWFormasPagoPDV As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.BWFormasPagoPDV
            Dim cantidadRegistros As Integer = 0

            ObtenerHeaderRequest(objHeaderRequest, "consultarOficinasVenta")
            objFormasPagoPDVRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objFormasPagoPDVRequest.MessageRequest.Body.codPDV = ""
            objFormasPagoPDVRequest.MessageRequest.Body.codCanal = ""

            ObtenerAuditoriaRequest(objBEAuditoriaREST)

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "INPUTS SERVICIO objBWFormasPagoPDV.ConsultarOficinaVenta"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "REQUEST", JavaScriptSerializer.Serialize(objFormasPagoPDVRequest).ToString()))

            objFormasPagosPDVResponse = objBWFormasPagoPDV.ConsultarOficinaVenta(objFormasPagoPDVRequest, objBEAuditoriaREST)

            codigoRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.codigoRespuesta
            mensajeRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTs SERVICIO objBWFormasPagoPDV.ConsultarOficinaVenta"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codigoRespuesta", codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeRespuesta", mensajeRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "RESPONSE", JavaScriptSerializer.Serialize(objFormasPagosPDVResponse).ToString()))

            If (codigoRespuesta = "0") Then
                listaOficinasVenta = objFormasPagosPDVResponse.MessageResponse.Body.listaOficinasVenta
                cantidadRegistros = Funciones.CheckInt(listaOficinasVenta.Length)
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "Cantidad registros", cantidadRegistros))
            End If
        Catch ex As Exception
            codigoRespuesta = "-1"
            mensajeRespuesta = "Exception: " + ex.Message
            mensajeAlerta = "Ha ocurrido un error en el servicio."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ObtenerOficinasVenta()", mensajeAlerta, mensajeRespuesta))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN ObtenerOficinasVenta()"))
    End Sub

    Private Sub gridDetalle_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles gridDetalle.ItemDataBound
        Dim chkControl As CheckBox = CType(e.Item.FindControl("chkestado"), CheckBox)
        Dim estadoMedioPago As Label = CType(e.Item.FindControl("estadoMedioPago"), Label)
        Dim chckEnabled As Boolean = False

        If e.Item.ItemType = ListItemType.EditItem And gridDetalle.EditItemIndex = e.Item.ItemIndex Then
            chkControl.Enabled = True
        ElseIf (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) And gridDetalle.EditItemIndex <> -1 And gridDetalle.EditItemIndex <> e.Item.ItemIndex Then
            Dim imgEditar As ImageButton = CType(e.Item.FindControl("imgModificar"), ImageButton)
            imgEditar.Visible = False
        End If

        If e.Item.ItemIndex <> -1 Then
            If (estadoMedioPago.Text = "0") Then
                chkControl.Enabled = False
                chkControl.Checked = False
        End If
        End If
    End Sub

    Public Sub HabilitarControles(ByVal habilitar As Boolean, ByVal itemIndex As Integer)
        gridDetalle.EditItemIndex = itemIndex
        btnExportar.Enabled = habilitar
        btnNuevo.Enabled = habilitar
        ddlFP.Enabled = habilitar
        ddlPDV.Enabled = habilitar
        btnBuscar.Enabled = habilitar
        btnLimpiar.Enabled = habilitar
    End Sub

    Public Sub EditarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim estadoMedioPago As Label = CType(e.Item.FindControl("estadoMedioPago"), Label)
        Dim descripcionMedioPago As Label = CType(e.Item.FindControl("descMedioPago"), Label)

        If (estadoMedioPago.Text = 0) Then
            mensajeAlerta = "La forma de pago " + descripcionMedioPago.Text + " se encuentra DESHABILITADA de manera general. Primero habilitela desde la opcion Configuracion --> Forma de Pago."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - gridDetalle_ItemDataBound()", mensajeAlerta))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
        Else
            gridDetalle.EditItemIndex = -1
        HabilitarControles(False, e.Item.ItemIndex)
        gridDetalle.DataSource = listaFormaPagoPDV
        gridDetalle.DataBind()
        End If
    End Sub

    Public Sub CancelarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        HabilitarControles(True, -1)
        gridDetalle.DataSource = listaFormaPagoPDV
        gridDetalle.DataBind()
    End Sub

    Public Sub ActualizarItem(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO ActualizarItem()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Dim codigoRespuesta As String = String.Empty
        Dim mensajeRespuesta As String = String.Empty

        Try
            Dim id As Integer = Convert.ToInt32(CType(e.Item.FindControl("id"), Label).Text)
            Dim estado As CheckBox = CType(e.Item.FindControl("chkestado"), CheckBox)
            Dim _estado As String = "0"

            If (estado.Checked = True) Then
                _estado = "1"
            End If

            HabilitarControles(True, -1)
            ActualizarEstado(id, _estado, codigoRespuesta, mensajeRespuesta)

            If (codigoRespuesta = "0") Then
                mensajeAlerta = "Datos actualizados correctamente."
                CargarGridFormasPagoPDV()
            Else
                mensajeAlerta = "Ha ocurrido un error al actualizar la forma de Pago por PDV."
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - cargarComboFormasPago()", mensajeRespuesta))
                gridDetalle.DataBind()
            End If
            descripcionTransaccion = "Se actualizo la consulta de la forma de pago por PDV correctamente."
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error al actualizar la forma de pago por PDV."
            descripcionTransaccion = mensajeAlerta
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ActualizarItem()", mensajeAlerta, ex.Message.ToString()))
        Finally
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ActualizarItem() - Mensaje ", mensajeAlerta))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
            codigoTransaccion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strActualizarFormasPagoPDV"))
            RegistrarAuditoria(codigoTransaccion, descripcionTransaccion)
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN ActualizarItem()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub ActualizarEstado(ByVal id As Integer, ByVal estado As String, ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String)
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO ActualizarEstado()"))

        Try
            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objFormasPagoPDVRequest As New FormasPagoPDVRequest
            Dim objFormasPagosPDVResponse As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.FormasPagoPDVResponse
            Dim objBWFormasPagoPDV As New COM_SIC_Procesa_Pagos.FormasPagoPDVRest.BWFormasPagoPDV

            ObtenerHeaderRequest(objHeaderRequest, "actualizarFormaPagoPDV")
            ObtenerAuditoriaRequest(objBEAuditoriaREST)

            objFormasPagoPDVRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objFormasPagoPDVRequest.MessageRequest.Body.id = id
            objFormasPagoPDVRequest.MessageRequest.Body.estado = estado
            objFormasPagoPDVRequest.MessageRequest.Body.usuario = CurrentUser

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "INPUTS SERVICIO objBWFormasPagoPDV.ActualizarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "id", id))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "estado", estado))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "usuario", CurrentUser))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "REQUEST", JavaScriptSerializer.Serialize(objFormasPagoPDVRequest).ToString()))

            objFormasPagosPDVResponse = objBWFormasPagoPDV.ActualizarFormaPagoPDV(objFormasPagoPDVRequest, objBEAuditoriaREST)

            codigoRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.codigoRespuesta
            mensajeRespuesta = objFormasPagosPDVResponse.MessageResponse.Body.mensajeRespuesta
            mensajeAlerta = mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "OUTPUTs SERVICIO objBWFormasPagoPDV.ActualizarFormaPagoPDV"))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "codigoRespuesta", codigoRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "mensajeRespuesta", mensajeRespuesta))
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} ---- {1}: {2}", idLog, "RESPONSE", JavaScriptSerializer.Serialize(objFormasPagosPDVResponse).ToString()))

        Catch ex As Exception
            codigoRespuesta = "-1"
            mensajeRespuesta = "Exception: " + ex.Message
            mensajeAlerta = "Ha ocurrido un error al actualizar la forma de pago por PDV."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ActualizarFormaPagoPDV()", mensajeAlerta, ex.Message.ToString()))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ActualizarEstado() - Mensaje ", mensajeAlerta))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN ActualizarEstado()"))
    End Sub

    Private Sub ObtenerFormasPagoPDV(ByVal codigoPDV As String, ByVal codigoMedioPago As String, ByRef codigoRespuesta As String, ByRef mensajeRespuesta As String, _
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

            If (codigoPDV = "0") Then
                objFormasPagoPDVRequest.MessageRequest.Body.codPDV = ""
            Else
                objFormasPagoPDVRequest.MessageRequest.Body.codPDV = codigoPDV
            End If

            If (codigoMedioPago = "0") Then
                objFormasPagoPDVRequest.MessageRequest.Body.codMedioPago = ""
            Else
                objFormasPagoPDVRequest.MessageRequest.Body.codMedioPago = codigoMedioPago
            End If

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
            mensajeAlerta = "Ha ocurrido un error en el servicio."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - ObtenerFormasPagoPDV()", mensajeAlerta, mensajeRespuesta))
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN ObtenerFormasPagoPDV()"))
    End Sub

    Private Sub gridDetalle_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles gridDetalle.PageIndexChanged
        gridDetalle.CurrentPageIndex = e.NewPageIndex
        HabilitarControles(True, -1)
        gridDetalle.DataSource = listaFormaPagoPDV
        gridDetalle.DataBind()
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargarGridFormasPagoPDV()
    End Sub

    Private Sub CargarGridFormasPagoPDV()
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO CargarGridFormasPagoPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim codigoRespuesta As String = String.Empty
            Dim mensajeRespuesta As String = String.Empty

            Dim codigoPDV As String = ddlPDV.SelectedValue.ToString()
            Dim codigoMedioPago As String = ddlFP.SelectedValue.ToString()

            ObtenerFormasPagoPDV(codigoPDV, codigoMedioPago, codigoRespuesta, mensajeRespuesta, listaFormaPagoPDV)

            If codigoRespuesta = 0 Then
                Dim cantidadRegistros As Integer = listaFormaPagoPDV.Length

                If (cantidadRegistros = 0) Then
                    mensajeAlerta = "No se encontraron registros que coincidan con los criterios de busqueda. Intentelo nuevamente."
                    Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
                    gridDetalle.DataSource = Nothing
                    gridDetalle.DataBind()
                    btnExportar.Enabled = False
                Else
                    gridDetalle.DataSource = listaFormaPagoPDV
                    gridDetalle.DataBind()
                    gridDetalle.CurrentPageIndex = 0
                    btnExportar.Enabled = True
                End If
            Else
                Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeRespuesta + "');</script>")
            End If
            descripcionTransaccion = "Se realizo la consulta de formas de pago por PDV correcctamente."
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error al cargar las formas de pago por PDV."
            descripcionTransaccion = "Error al realizar la consulta de formas de pago por PDV."
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - CargarGridFormasPagoPDV()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
            btnExportar.Enabled = False
        Finally
            codigoTransaccion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strConsultarFormasPagoPDV"))
            RegistrarAuditoria(codigoTransaccion, descripcionTransaccion)
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN CargarGridFormasPagoPDV()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        ddlPDV.SelectedIndex = 0
        ddlFP.SelectedIndex = 0
        gridDetalle.DataSource = Nothing
        gridDetalle.DataBind()
        listaFormaPagoPDV = Nothing
        gridExportar.DataSource = Nothing
        gridExportar.DataBind()
        btnExportar.Enabled = False
    End Sub

    Private Sub btnExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "INICIO btnExportar_Click()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))

        Try
            Dim fechaArchivo As String = String.Format("{0}{1}{2}", Format(Now.Day, "00"), Format(Now.Month, "00"), Format(Now.Year, "0000"))
            Dim horaArchivo As String = String.Format("{0}{1}{2}", TimeOfDay().Hour, TimeOfDay().Minute, TimeOfDay().Second)
            Dim nombreArchivo As String = String.Format("RepFormasPagoPDV_{0}{1}", fechaArchivo, horaArchivo)

            Dim listaFormaPagoPDVTemp As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV()
            listaFormaPagoPDVTemp = listaFormaPagoPDV

            If Not (listaFormaPagoPDVTemp Is Nothing) Then
            For Each formaPagoPDV As COM_SIC_Procesa_Pagos.FormasPagoPDVRest.listaFormaPagoPDV In listaFormaPagoPDVTemp
                If formaPagoPDV.estadoMedioPago = "0" Or formaPagoPDV.estado = "0" Then
                    formaPagoPDV.estado = "Deshabilitado"
                Else
                    formaPagoPDV.estado = "Habilitado"
                End If
            Next

            gridExportar.DataSource = listaFormaPagoPDVTemp
            gridExportar.DataBind()

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombreArchivo + ".xls")
            Response.ContentType = "application/vnd.ms-excel"

            Dim writer As StringWriter = New StringWriter
            Dim htmlWriter As HtmlTextWriter = New HtmlTextWriter(writer)

            gridExportar.Visible = True
            gridExportar.RenderControl(htmlWriter)
            gridExportar.Visible = False

            Dim style As String = "<style> TD {mso-number-format:\@; } </style>"
            Response.Write(style)
            Response.Output.Write(writer.ToString())
            Response.End()
            Else
                mensajeAlerta = "Ha ocurrido un error al exportar los registros en excel. Intentelo nuevamente"
                objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2}", idLog, "ERROR - btnExportar_Click()", mensajeAlerta))
                Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
            End If
        Catch ex As Exception
            mensajeAlerta = "Ha ocurrido un error al exportar los registros en excel. Intentelo nuevamente"
            objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} --> {2} {3}", idLog, "ERROR - btnExportar_Click()", mensajeAlerta, ex.Message.ToString()))
            Me.RegisterStartupScript("MensajeAlerta", "<script>mensajeAlerta('" + mensajeAlerta + "');</script>")
        End Try

        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1} {2} {1}", idLog, "=======", "FIN btnExportar_Click()"))
        objFileLog.Log_WriteLog(pathFile, nombreArchivo, String.Format("{0} -- {1}", idLog, "========================================================"))
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
