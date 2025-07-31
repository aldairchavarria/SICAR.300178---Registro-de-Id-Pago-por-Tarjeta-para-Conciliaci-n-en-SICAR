Imports System.Xml
Imports SisCajas.Funciones
Imports COM_SIC_Activaciones
Imports SisCajas.clsActivaciones
Imports System.Globalization
Imports COM_SIC_Seguridad 'INICIATIVA 712 Cobro Anticipado
Imports COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Request ''IDEA300216
Imports COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Response ''IDEA300216
Imports COM_SIC_Procesa_Pagos.NotificacionTransaccion ''IDEA300216

Public Class PoolPagos
    Inherits SICAR_WebBase
    Dim ds As DataSet
    Protected WithEvents cmdAnular As System.Web.UI.WebControls.Button
    Protected WithEvents txtRbPagos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtDocSap As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDG As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTipDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtpImp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEfectivo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtRecibido As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtEntregar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents flagVentaEquipoPrepago As System.Web.UI.HtmlControls.HtmlInputHidden
    Dim drFila As DataRow
    '140245 Inicio
    Dim strNro_Sec As Integer
    Dim strTipo_opera_Des As String
    Dim strNro_Ped_Det As Integer
    Dim strTipo_Opera_Cod As String 'NUEVO CAMBIO
    '140245 Fin

    Dim dtsap, dtsicar, dtncanje As DataTable
    Dim objFileLog As New SICAR_Log
    Protected WithEvents cmdProcesar As System.Web.UI.WebControls.Button
    'PROY-140335 INI
    Protected WithEvents cmdProcesarSP As System.Web.UI.WebControls.Button
    Protected WithEvents cmdProcesarPago As System.Web.UI.WebControls.Button
    'PROY-140335 FIN
    Protected WithEvents txtsession As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtOffline As System.Web.UI.WebControls.TextBox

    'INCIDENCIA MEJORA SICAR - INI
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocumento As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnBuscarPool As System.Web.UI.HtmlControls.HtmlInputHidden
    'INCIDENCIA MEJORA SICAR - FIN
    Dim objclsOffline As New COM_SIC_OffLine.clsOffline
    Public dtIGV As DataTable

#Region "PROY-140715 - IDEA 140805 || No Biometria en SISACT x caida RENIEC |Declaracion de Variables |INI"
    Dim strFlagGeneralCaidaNoBio As String = String.Empty
    Dim strTipoContingenciaCaidaNoBio As String = String.Empty
#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnReasignar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCompensar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnPagar As System.Web.UI.WebControls.Button
    Protected WithEvents hidVerif As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPoolPagos")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objLog.Log_CrearNombreArchivo(nameFile)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450" & " - " & "Se ingreso a PoolPagos") 'PBI000002148450
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450 - clsKeyAPP.bolParamProteccionMovil: " & Funciones.CheckStr(clsKeyAPP.bolParamProteccionMovil)) 'PBI000002148450
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SESSION USUARIO" & Funciones.CheckStr(Session("USUARIO")))
            Dim strMensaje As String = Funciones.CheckStr(Session("SP_strMensajeAlert"))
            strFlagGeneralCaidaNoBio = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_FlagGeneral) '//'PROY-140715 - IDEA 140805

            If (strMensaje.Length > 0) Then
                Response.Write("<script>alert('" & Session("SP_strMensajeAlert") & "');</script>")
                Session.Remove("SP_strMensajeAlert")
            End If

            'Dim objPool As New SAP_SIC_Pagos.clsPagos
            Dim strFecha As String

            btnPagar.Attributes.Add("onClick", "f_Pagos()")
            ' btnReasignar.Attributes.Add("onClick", "f_Reasignacion()") TODOEB
            ' btnCompensar.Attributes.Add("onClick", "f_Compensar();")

            'INCIDENCIA MEJORA SICAR - INI
            Dim dsTipo As DataSet
            Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
            objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR" & "- " & " Consulta tipo documento cliente.")
            dsTipo = objConsultaPvu.ConsultaTipoDocumento("")

            If Not dsTipo Is Nothing Then
                cboTipDocumento.DataSource = dsTipo.Tables(0)
                cboTipDocumento.DataValueField = "TDOCC_CODIGO"
                cboTipDocumento.DataTextField = "TDOCV_DESCRIPCION"
                cboTipDocumento.DataBind()

                objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR" & "- " & " Asigna el tipo de documento por defecto DNI.")
                If cboTipDocumento.SelectedIndex = -1 Then
                    cboTipDocumento.SelectedValue = "01"
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR" & "- " & " No se encontraron registros para el tipo de documento cliente.")
            End If

            txtNumDocumento.Attributes.Add("onKeyDown", "textCounter(this)")
            txtNumDocumento.Attributes.Add("onKeyUp", "textCounter(this)")
            cboTipDocumento.Attributes.Add("onChange", "f_CambiaTipo()")
            'INCIDENCIA MEJORA SICAR - FIN

            If Not Page.IsPostBack Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450 - clsKeyAPP.bolParamProteccionMovil: " & Funciones.CheckStr(clsKeyAPP.bolParamProteccionMovil)) 'PBI000002148450
                If Not Session("ErrorRenovacionRPM6") Is Nothing And Session("ErrorRenovacionRPM6") <> "" Then
                    Response.Write("<script>alert('" & Session("ErrorRenovacionRPM6") & "');</script>")
                    Session.Remove("ErrorRenovacionRPM6")
                End If
                If Not Session("AnulacionRenovacionCorporativa") Is Nothing And Session("AnulacionRenovacionCorporativa") <> "" Then
                    Response.Write("<script>alert('" & Session("AnulacionRenovacionCorporativa") & "');</script>")
                    Session.Remove("AnulacionRenovacionCorporativa")
                End If
                If Not Session("msgActivacionCAC") Is Nothing And Session("msgActivacionCAC") <> "" Then
                    Response.Write("<script>alert('" & Session("msgActivacionCAC") & "');</script>")
                    Session.Remove("msgActivacionCAC")
                End If
                If Not Session("mensajeErrorBonoPrepago") Is Nothing And Session("mensajeErrorBonoPrepago") <> "" Then
                    Response.Write("<script>    ('" & Session("mensajeErrorBonoPrepago") & "');</script>")
                    Session.Remove("mensajeErrorBonoPrepago")
                End If
                If Not Session("PagoBSCSRenovacionCorporativa") Is Nothing And Session("PagoBSCSRenovacionCorporativa") <> "" Then
                    Response.Write("<script>alert('" & Session("PagoBSCSRenovacionCorporativa") & "');</script>")
                    Session.Remove("PagoBSCSRenovacionCorporativa")
                End If
                If Not Session("mensajeErrorLineas") Is Nothing And Session("mensajeErrorLineas") <> "" Then
                    Response.Write("<script>alert('" & Session("mensajeErrorLineas") & "');</script>")
                    Session.Remove("mensajeErrorLineas")
                End If
                If Not Session("strMensajeWSCABB") Is Nothing And Session("strMensajeWSCABB") <> "" Then
                    Response.Write("<script>alert('" & Session("strMensajeWSCABB") & "');</script>")
                    Session.Remove("strMensajeWSCABB")
                End If
               

                '/*----------------------------------------------------------------------------------------------------------------*/
                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                '/*----------------------------------------------------------------------------------------------------------------*/               
                If Not Session("msgErrorGenerarSot") Is Nothing And Session("msgErrorGenerarSot") <> "" And (Session("msgErrorDevolverRV") Is Nothing Or Session("msgErrorDevolverRV") = "") Then
                    Response.Write("<script>alert('" & Session("msgErrorGenerarSot") & "');</script>")
                    Session.Remove("msgErrorGenerarSot")
                ElseIf Not Session("msgErrorDevolverRV") Is Nothing And Session("msgErrorDevolverRV") <> "" Then
                    Response.Write("<script>alert('" & Session("msgErrorDevolverRV") & "');</script>")
                    Session.Remove("msgErrorDevolverRV")
                End If
                '/*----------------------------------------------------------------------------------------------------------------*/
                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                '/*----------------------------------------------------------------------------------------------------------------*/

                ' INICIO : BONO REPOSICION EN CAC 
                If Not Session("mensajeBonoReposisicon") Is Nothing And Session("mensajeBonoReposisicon") <> "" Then
                    Response.Write("<script>alert('" & Session("mensajeBonoReposisicon") & "');</script>")
                    Session.Remove("mensajeBonoReposisicon")
                End If
                ' FIN : BONO REPOSICION EN CAC 
                If Not Session("msgDevolucion") Is Nothing And Session("msgDevolucion") <> "" Then
                    Response.Write("<script>alert('" & Session("msgDevolucion") & "');</script>")
                    Session.Remove("msgDevolucion")
                End If

                'PROY -MCKINSEY -> JSQ INICIO
                '/*-------------------------------------------*/
                '/*--JSQ  INICIO  -  PROY-140379 - MCKINSEY --*/
                '/*-------------------------------------------*/ 
                Dim CodGrupoParamMckinsey As String = ConfigurationSettings.AppSettings("CodParamGrupoMcKinsey")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "MCKINSEY paramGrupo -> " + Funciones.CheckStr(ConfigurationSettings.AppSettings("CodParamGrupoMcKinsey").ToString()))

                Dim objObtenerParamGrupo As New COM_SIC_Cajas.clsCajas
                Dim dsParamGrupomckinsey As DataSet

                Dim strCantDiasFiltroFecha As Integer = 0

                Dim FechaFiltro As Date = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)

                objFileLog.Log_WriteLog(pathFile, strArchivo, "MCKINSEY FechaFiltro -> " + FechaFiltro)

                dsParamGrupomckinsey = objObtenerParamGrupo.ObtenerParamByGrupo(CodGrupoParamMckinsey)

                Dim i As Integer
                For i = 0 To dsParamGrupomckinsey.Tables(0).Rows.Count - 1
                    If dsParamGrupomckinsey.Tables(0).Rows(i).Item("PARAV_VALOR1") = "key_CantDiasFiltro" Then
                        strCantDiasFiltroFecha = dsParamGrupomckinsey.Tables(0).Rows(i).Item("PARAV_VALOR")
                    End If
                Next
                'Resta Cantidad de dias Configurable al filtro Fecha
                objFileLog.Log_WriteLog(pathFile, strArchivo, "MCKINSEY strCantDiasFiltroFecha -> " + strCantDiasFiltroFecha.ToString())

                Dim FechaFiltro1 As String = String.Format("{0:dd/MM/yyyy}", FechaFiltro, CultureInfo.InvariantCulture)
                FechaFiltro = FechaFiltro1

                FechaFiltro = FechaFiltro.AddDays(-strCantDiasFiltroFecha)

                objFileLog.Log_WriteLog(pathFile, strArchivo, "MCKINSEY FechaFiltro RESTADA -> " + FechaFiltro)

                If FechaFiltro.ToString() Is Nothing OrElse FechaFiltro.ToString() = "" Then
                    txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)
                Else
                    txtFecha.Text = String.Format("{0:dd/MM/yyyy}", FechaFiltro, CultureInfo.InvariantCulture)
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "MCKINSEY txtFecha.Text -> " + String.Format("{0:dd/MM/yyyy}", FechaFiltro, CultureInfo.InvariantCulture))
                
                '/*-------------------------------------------*/
                '/*--JSQ  FIN  -  PROY- 140379 - MCKINSEY --*/
                '/*-------------------------------------------*/

                If Not Session("Valida_Pago_MSG") Is Nothing And Session("Valida_Pago_MSG") <> "" Then
                    Response.Write("<script>alert('" & Session("Valida_Pago_MSG") & "');</script>")
                    Session.Remove("Valida_Pago_MSG")
                End If

                '=============================================================================================
                '***Bloque mal agregado, por eso no esta entrando para hacer la impresiòn
                '*******agregar por impresion
                If Not Request.QueryString("pImp") Is Nothing Then     'AndAlso Request.QueryString("pImp") = "S"
                    txtpImp.Text = Request.QueryString("pImp")
                    If Not Request.QueryString("pDocSap") Is Nothing Then
                        txtDocSap.Text = Request.QueryString("pDocSap")
                    Else
                        txtDocSap.Text = ""
                    End If
                    If Not Request.QueryString("pDocSunat") Is Nothing Then
                        txtDocSunat.Text = Request.QueryString("pDocSunat")
                    End If
                    If Not Request.QueryString("pNroDG") Is Nothing Then
                        txtNroDG.Text = Request.QueryString("pNroDG")
                    End If
                    If Not Request.QueryString("pTipDoc") Is Nothing Then
                        txtTipDoc.Text = Request.QueryString("pTipDoc")
                    End If

                    ''INICIO JTN
                    If Not Request.QueryString("isOffline") Is Nothing Then
                        txtOffline.Text = Request.QueryString("isOffline")
                    End If
                    ''FIN JTN

                End If
                '*******fin agregar por impresion
                '===========================================================================================

                If Not Request.QueryString("strEfectivo") Is Nothing Then
                    txtEfectivo.Value = Request.QueryString("strEfectivo")
                End If
                If Not Request.QueryString("strRecibido") Is Nothing Then
                    txtRecibido.Value = Request.QueryString("strRecibido")
                End If
                If Not Request.QueryString("strEntregar") Is Nothing Then
                    txtEntregar.Value = Request.QueryString("strEntregar")
                End If
                If Not Request.QueryString("flagVentaEquipoPrepago") Is Nothing Then
                    flagVentaEquipoPrepago.Value = Request.QueryString("flagVentaEquipoPrepago")
                End If
            Else

                cboTipDocumento.SelectedValue = Request.Item("cbotipdocumento") 'INCIDENCIA MEJORA SICAR

                Dim strAccion As String = hidAccion.Value
                hidAccion.Value = ""
                If strAccion = "COM" Then
                    Compensar()
                End If

            End If

            ' E75893 - Mensaje de Error Registro DOL
            If Not Session("mensajeErrorDOL") Is Nothing And Session("mensajeErrorDOL") <> "" Then
                Response.Write("<script>alert('" & Session("mensajeErrorDOL") & "')</script>")
                Session.Remove("mensajeErrorDOL")
            End If

            'INCIDENCIA MEJORA SICAR - INI
            Dim CodMejoraSICAR As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodMejoraSICAR"))
	    Dim CodParamMckinsey As String = ConfigurationSettings.AppSettings("CodParamGrupoMcKinsey")
            Dim objParametros As New COM_SIC_Cajas.clsCajas
            Dim dsMejoraSICAR As DataSet
	    Dim dsGrupomckinsey As DataSet
            Dim FlagListado As String = String.Empty
            Dim DiasFiltro As Integer = 0
            Dim tipo_doc As String = String.Empty
            tipo_doc = cboTipDocumento.SelectedValue

            dsMejoraSICAR = objParametros.ObtenerParamByGrupo(CodMejoraSICAR)

            Dim x As Integer
            For x = 0 To dsMejoraSICAR.Tables(0).Rows.Count - 1
                If dsMejoraSICAR.Tables(0).Rows(x).Item("PARAV_VALOR1") = "Key_FlagListadoSICAR" Then
                    FlagListado = dsMejoraSICAR.Tables(0).Rows(x).Item("PARAV_VALOR")
                End If
            Next
			
	    dsGrupomckinsey = objParametros.ObtenerParamByGrupo(CodParamMckinsey)

            Dim y As Integer
            For y = 0 To dsGrupomckinsey.Tables(0).Rows.Count - 1
                If dsGrupomckinsey.Tables(0).Rows(y).Item("PARAV_VALOR1") = "key_CantDiasFiltro" Then
                    DiasFiltro = dsGrupomckinsey.Tables(0).Rows(y).Item("PARAV_VALOR")
                End If
            Next

            Dim strBuscaPool As String = hdnBuscarPool.Value
            hdnBuscarPool.Value = "0"

            If Not Page.IsPostBack Then
                If FlagListado = "1" Then
                    CargarGrilla(ConsultaPuntoVenta(Session("ALMACEN")))
                End If
                txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)
            Else
                If cboTipDocumento.SelectedValue <> "" And Funciones.CheckStr(txtNumDocumento.Text.ToString.Trim) <> "" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR Dias Filtro -> " + DiasFiltro.ToString())
                    Dim FechaFiltroA As Date = String.Format("{0:dd/MM/yyyy}", txtFecha.Text, CultureInfo.InvariantCulture)
                    Dim FechaFiltroB As Date = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)
                    Dim FechaFiltroB1 As String = String.Format("{0:dd/MM/yyyy}", FechaFiltroB, CultureInfo.InvariantCulture)
                    FechaFiltroB = FechaFiltroB1
                    FechaFiltroB = FechaFiltroB.AddDays(-DiasFiltro)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR FechaFiltro RESTADA -> " + FechaFiltroB)

                    If FechaFiltroB.ToString() Is Nothing OrElse FechaFiltroB.ToString() = "" Then
                        txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now, CultureInfo.InvariantCulture)
                    Else
                        txtFecha.Text = String.Format("{0:dd/MM/yyyy}", FechaFiltroB, CultureInfo.InvariantCulture)
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "INCIDENCIA MEJORA SICAR txtFecha.Text -> " + String.Format("{0:dd/MM/yyyy}", FechaFiltroB, CultureInfo.InvariantCulture))

                    CargarGrilla(ConsultaPuntoVenta(Session("ALMACEN")), cboTipDocumento.SelectedValue, IIf(txtNumDocumento.Text.ToString.Trim.Length = 0, "00000000", Funciones.CheckStr(txtNumDocumento.Text.ToString.Trim)))
                    txtFecha.Text = String.Format("{0:dd/MM/yyyy}", FechaFiltroA, CultureInfo.InvariantCulture)
                Else
                    CargarGrilla(ConsultaPuntoVenta(Session("ALMACEN")))
                End If
            End If
            'INCIDENCIA MEJORA SICAR - FIN

            ' Mensaje de Error CHIP Repuesto
            If Len(Trim(Session("mensajeCHIPRepuesto"))) > 0 Then
                Response.Write("<script>alert('" & Session("mensajeCHIPRepuesto") & "')</script>")
                Session("mensajeCHIPRepuesto") = ""
            End If

            If Len(Trim(Session("strMensajeCaja"))) > 0 Then
                Response.Write("<script>alert('" & Session("strMensajeCaja") & "')</script>")
                Session("strMensajeCaja") = ""
            End If

            If Len(Trim(Session("strMensajeCajaRec"))) > 0 Then
                Response.Write("<script>alert('" & Session("strMensajeCajaRec") & "')</script>")
                Session("strMensajeCajaRec") = ""
            End If

            ' Inicio IDEA-11056 Renovación TFI para CAC's
            If Len(Trim(Session("P_MSJ_RESULTADO"))) > 0 Then
                Response.Write("<script>alert('" & Session("P_MSJ_RESULTADO") & "')</script>")
                Session("P_MSJ_RESULTADO") = ""
            End If
            ' Fin IDEA-11056 Renovación TFI para CAC's 
            'INICIO - PROY-140846  IDEA 143176 - REGULATORIO - GPRD
            If Not Session("ShowMsgRepoProgramada") Is Nothing Then
                Dim sValSessionRepoProgramada As String = String.Empty
                sValSessionRepoProgramada = Funciones.CheckStr(Session("ShowMsgRepoProgramada"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "sValSessionRepoProgramada: " & "- " & Funciones.CheckStr(sValSessionRepoProgramada))
                If sValSessionRepoProgramada = "0" Then
                    Dim sMensaje As String = String.Empty
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ReadKeySettings.Key_MensajeReposicionProgramada: " & "- " & Funciones.CheckStr(ReadKeySettings.Key_MensajeReposicionProgramada))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ReadKeySettings.Key_TiempoProgramacionReposicion: " & "- " & Funciones.CheckStr(ReadKeySettings.Key_TiempoProgramacionReposicion))
                    sMensaje = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeReposicionProgramada), Funciones.CheckStr(ReadKeySettings.Key_TiempoProgramacionReposicion))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "sMensaje: " & "- " & Funciones.CheckStr(sMensaje))
                    Response.Write("<script>alert('" & sMensaje & "');</script>")
                End If
            End If
            'FIN PROY-140846 IDEA 143176 - REGULATORIO - GPRD
            'INCIDENCIA MEJORA SICAR - INI
            If Not dsTipo Is Nothing Then
                If tipo_doc <> "" Then
                    cboTipDocumento.SelectedValue = tipo_doc
                End If
            End If
            'INCIDENCIA MEJORA SICAR - FIN

            'dgPool.Columns(0).ItemStyle.Width = dgPool.Columns(0).ItemStyle.Width.Percentage(5)
            'dgPool.Columns(1).ItemStyle.Width = dgPool.Columns(1).ItemStyle.Width.Percentage(15)
            'dgPool.Columns(2).ItemStyle.Width = dgPool.Columns(2).ItemStyle.Width.Percentage(5)
            'dgPool.Columns(3).ItemStyle.Width = dgPool.Columns(3).ItemStyle.Width.Percentage(5)
        End If

        'JLOPETAS - PROY 140589 - INI
        Try
            Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu 'JLOPETAS - PROY 140589
            Dim COD_RPTA As String = String.Empty 'JLOPETAS - PROY 140589
            Dim MSJ_RPTA As String = String.Empty 'JLOPETAS - PROY 140589
            Dim oItemGenerico As ItemGenerico = clsConsultaPvu.ConsultarFlagsPicking(Funciones.CheckStr(Session("ALMACEN")), COD_RPTA, MSJ_RPTA)

            If COD_RPTA = "1" Then

                Session("FLAG_PICKING") = Funciones.CheckStr(oItemGenerico.FLAG_PICKING)
                Session("FLAG_DLV") = Funciones.CheckStr(oItemGenerico.FLAG_DLV)

            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "" & "- " & "Error al consultar los flags de picking")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "" & "- " & "Exception" & "- " & ex.ToString)
        End Try
        'JLOPETAS - PROY 140589 - FIN

    End Sub

    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
			Dim PtodeVenta As String
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
				'PtodeVenta = dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub CargarGrilla(ByVal strCodOficina As String, Optional ByVal TipoDocumento As String = "", Optional ByVal NumeroDocumento As String = "")

        If (strCodOficina = "" Or strCodOficina Is Nothing Or Session("Lista_Impuesto") Is Nothing) Then
            Dim strMsjErr As String = ConfigurationSettings.AppSettings("conMsjErrSesion")
            Response.Write("<script language='javascript'>alert('" & strMsjErr & "');parent.parent.document.location.href='../home.aspx';</script>")
            Exit Sub
        End If

        Dim P_CODIGO_RESPUESTA As String
        Dim P_MENSAJE_RESPUESTA As String
        Dim C_VENTA As DataTable
        Dim C_VENTA_DET As DataTable


        'Dim objPool As New SAP_SIC_Pagos.clsPagos
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline

        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objclsTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
        Dim strErrorMsg As String

        strErrorMsg = ""

        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        Dim strIdentifyLog As String = strCodOficina
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Consulta Pool de Documentos por Pagar")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Estado: " & ConfigurationSettings.AppSettings("PEDIC_ESTADO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha: " & DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strCodOficina: " & strCodOficina)

        dtsap = objclsConsultaMsSap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), strCodOficina, "", TipoDocumento, NumeroDocumento)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Consulta Documentos de Nota de Canje")

        dtncanje = objclsConsultaMsSap.ConsultaPoolNotasCanje(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), strCodOficina, "")

        'PROY-30166 -IDEA-38863 - INICIO
        Dim objClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
        Dim TamCadPedido As Integer
        Dim dsCuotasxPedido As DataSet
        Dim strCodRpta As String
        Dim strMsjRpta As String
        Dim strCad As String
        Dim CadenaPedidos As String
        Dim t As Integer
        dsCuotasxPedido = Nothing
        CadenaPedidos = ""
        Dim strContrato As String = String.Empty
        Dim strTelef As String = String.Empty
        '' VALIDAR QUE DTSAP TENGA DATOS
        If Not dtsap Is Nothing AndAlso dtsap.Rows.Count > 0 Then
            For t = 0 To dtsap.Rows.Count - 1
                CadenaPedidos += "'" + Funciones.CheckStr(dtsap.Rows(t).Item("PEDIN_NROPEDIDO")) + "'" + ","
            Next
            TamCadPedido = CadenaPedidos.Length
            CadenaPedidos = CadenaPedidos.Substring(0, TamCadPedido - 1)
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INI ObtenerMontoInicialxPedidos")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " * IN Pedidos: " & CadenaPedidos)
        dsCuotasxPedido = objClsTrsPvu.ObtenerMontoInicialxPedidos(CadenaPedidos, strContrato, strTelef, strCodRpta, strMsjRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " * OUT Cod Rpta: " & strCodRpta)
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: CargarGrilla)"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " * OUT Msj Rpta: " & strMsjRpta & MaptPath)
        'FIN PROY-140126
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN ObtenerMontoInicialxPedidos")

        If Not dsCuotasxPedido Is Nothing AndAlso dsCuotasxPedido.Tables(0).Rows.Count > 0 AndAlso Not dtsap Is Nothing AndAlso dtsap.Rows.Count > 0 Then
            For Each row As DataRow In dtsap.Rows
                Dim PedidoPool As Integer = row("PEDIN_NROPEDIDO")
                For Each row2 As DataRow In dsCuotasxPedido.Tables(0).Rows

                    Dim PedidoCuota As Integer = row2("PEDIN_NROPEDIDO")
                    Dim CuotaInicial As Double = row2("MONTO_CUOTA_INICIAL")
                    If PedidoPool = PedidoCuota Then
                        row.BeginEdit()
                        row("PAGON_INICIAL") = CuotaInicial
                        row.EndEdit()
                    End If
                Next
            Next
        End If
        'PROY-30166 -IDEA-38863 - FIN
        Dim ResultActualizarPago As Int32

        '******************************UPDATE AL ESTADO DE LOS PEDIDOS QUE NO SON DE CACPS*******************************************************
        Dim i As Integer
        'Try
        '    For i = 0 To dtsap.Rows.Count - 1
        '        If (dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA") <> "01") Then
        '            ResultActualizarPago = objclsTrsMsSap.ActualizarPago(dtsap.Rows.Item(i)("PEDIN_NROPEDIDO"), "PAG")
        '        End If
        '    Next
        'Catch ex As Exception
        '    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error en el método => objclsTrsMsSap.ActualizarPago ")
        'End Try
        '***************************************************************************************************************************************


        'TRAER EL NRO DE DOCUMENTO SUANT MODULO MSSAP
        dtIGV = Session("Lista_Impuesto")
        Dim IGVactual As Decimal = 0.0
        For Each row As DataRow In dtIGV.Rows
            If (Date.Now() >= CDate(row("impudFecIniVigencia").ToString.Trim) And Date.Now() <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
                IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2)
                Exit For
            End If
        Next
        Dim documentosPagadosTable As DataTable = objclsOffline.GetConsultaPagosUsuario(txtFecha.Text, "", "", codUsuario, strCodOficina, "1", IGVactual).Tables(0)

        'FIN DE MODIFICACION
        'implementar filas con error
        Dim filasConError() As DataRow = documentosPagadosTable.Select("Estado_SAP = 'ERROR DE PROCESO'")

        For Each filaConError As DataRow In filasConError
            Dim numeroSunat$ = filaConError("XBLNR")
            Dim existeEnPool As Boolean = dtsap.Select("XBLNR = '" & numeroSunat & "'").Length > 0
            If existeEnPool Then
                Dim filaEliminar As DataRow = dtsap.Select("XBLNR = '" & numeroSunat & "'")(0)
                dtsap.Rows.Remove(filaEliminar)
                dtsap.AcceptChanges()
            End If
        Next
        'PROY-140335 -INI
        Dim colPorta As New Data.DataColumn("PEDIC_PORTA", GetType(System.String))
        colPorta.DefaultValue = "N"
        dtsap.Columns.Add(colPorta)  

        Dim strTVPrepago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPrepago"))
        Dim strDescriOpePrePago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))

        Dim strTVPostpago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))
        Dim strDescriOpePostPago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
        'PROY-140335 -FIN
        dtsap.Columns.Add("Estado_SAP", GetType(String))
        dtsap.Columns.Add("ID_T_TRS_PEDIDO", GetType(String))
        dtsap.Columns.Add("PEDMC_TIPO_ENTREGA", GetType(String))'PROY-140397-MCKINSEY -> JSQ

        dtncanje.Columns.Add("Estado_SAP", GetType(String))
        dtncanje.Columns.Add("ID_T_TRS_PEDIDO", GetType(String))
        dtncanje.Columns.Add("PEDMC_TIPO_ENTREGA", GetType(String))'PROY-140397-MCKINSEY -> JSQ

        Dim e As Integer
        Dim PEDIC_TIPOOFICINA As String = ""
        Dim PEDIC_ISRENTA As String = ""
        Dim DRA_ASOCIADO As String = ""
        'PROY-140397-MCKINSEY -> JSQ INICIO
        Dim TIPO_ENTREGA As String = ""
        Dim DESCCLASEFACTURA As String = ""
        'Dim PEDIN_NROPEDIDO As String = ""

        Dim flag_multi As String = ""
        'PROY-140397-MCKINSEY -> JSQ FIN

        'PROY-140335 INI
        Dim tipoVenta As String = ""
        Dim desTipoOpe As String = ""
        'PROY-140335 FIN
        If dtsap.Rows.Count > 0 Then
            For e = 0 To dtsap.Rows.Count - 1
                dtsap.Rows.Item(e)("Estado_SAP") = "PROCESADO"

                PEDIC_ISRENTA = dtsap.Rows.Item(e)("PEDIC_ISRENTA")

                If PEDIC_ISRENTA = "S" Then
                    dtsap.Rows.Item(e)("PAGOC_CODSUNAT") = ""
                End If
                'PROY-140335-INI
                tipoVenta = Funciones.CheckStr(dtsap.Rows.Item(e)("PEDIC_TIPOVENTA"))
                desTipoOpe = Funciones.CheckStr(dtsap.Rows.Item(e)("PEDIV_DESCTIPOOPERACION"))
                If (desTipoOpe = strDescriOpePrePago AndAlso tipoVenta = strTVPrepago) OrElse (desTipoOpe = strDescriOpePostPago AndAlso tipoVenta = strTVPostpago) Then
                    dtsap.Rows.Item(e)("PEDIC_PORTA") = "P"
                End If
               'PROY-140335-FIN

                'PROY-140397-MCKINSEY -> JSQ INICIO

                'PEDIN_NROPEDIDO = dtsap.Rows.Item(e)("PEDIN_NROPEDIDO")


                TIPO_ENTREGA = Convert.ToString(dtsap.Rows.Item(e)("TIPO_ENTREGA"))

                If TIPO_ENTREGA = "0" Then
                    dtsap.Rows.Item(e)("TIPO_ENTREGA") = "Presencial"
                ElseIf TIPO_ENTREGA = "1" Then
                    dtsap.Rows.Item(e)("TIPO_ENTREGA") = "Delivery"
                Else
                    dtsap.Rows.Item(e)("TIPO_ENTREGA") = ""
                End If



                DESCCLASEFACTURA = dtsap.Rows.Item(e)("PEDIV_DESCCLASEFACTURA")

                If (DESCCLASEFACTURA = "NOTA DE CREDITO") Then
                    dtsap.Rows.Item(e)("TIPO_ENTREGA") = ""
                End If
                If (DESCCLASEFACTURA = "NOTA DE CANJE") Then
                    dtsap.Rows.Item(e)("TIPO_ENTREGA") = ""
                End If

               'PROY-140397-MCKINSEY -> JSQ FIN

            Next
        End If

        '*** Inicio Cargar Notas de Canje en el Pool ****
        For Each dr As DataRow In dtncanje.Rows
            Dim drsap As DataRow = dtsap.NewRow

            drsap("PAGOC_STATUS") = dr("PAGOC_STATUS")
            drsap("PAGOC_SOCIEDAD") = dr("PAGOC_SOCIEDAD")
            drsap("PEDIN_PEDIDOSAP") = dr("PEDIN_PEDIDOSAP")
            drsap("PAGOD_FECHACONTA") = dr("PAGOD_FECHACONTA")
            drsap("PEDIV_NOMBRECLIENTE") = dr("PEDIV_NOMBRECLIENTE")
            drsap("PEDIV_NRODOCCLIENTE") = dr("PEDIV_NRODOCCLIENTE")
            drsap("PEDIC_CLASEFACTURA") = dr("PEDIC_CLASEFACTURA")
            drsap("PEDIV_DESCCLASEFACTURA") = dr("PEDIV_DESCCLASEFACTURA")
            drsap("PAGOC_INDICADORSUNAT") = dr("PAGOC_INDICADORSUNAT")
            drsap("FACTURA_ANULADA") = dr("FACTURA_ANULADA")
            drsap("PAGOC_CODSUNAT") = dr("PAGOC_CODSUNAT")
            drsap("PEDIC_MONEDA") = dr("PEDIC_MONEDA")
            drsap("INPAN_TOTALMERCADERIA") = dr("INPAN_TOTALMERCADERIA")
            drsap("INPAN_TOTALIMPUESTO") = dr("INPAN_TOTALIMPUESTO")
            drsap("INPAN_TOTALDOCUMENTO") = dr("INPAN_TOTALDOCUMENTO")
            drsap("PAGON_INICIAL") = dr("PAGON_INICIAL")
            drsap("PEDIN_PAGO") = dr("PEDIN_PAGO")
            drsap("PEDIN_SALDO") = dr("PEDIN_SALDO")
            drsap("PEDIC_ESQUEMACALCULO") = dr("PEDIC_ESQUEMACALCULO")
            drsap("PEDIV_TIPOCOMERCIAL") = dr("PEDIV_TIPOCOMERCIAL")
            drsap("PEDIV_HORA") = dr("PEDIV_HORA")
            drsap("PEDIN_NROPEDIDO") = dr("PEDIN_NROPEDIDO")
            drsap("PEDIN_PEDIDOALTA") = dr("PEDIN_PEDIDOALTA")
            drsap("CLIEV_TELEFONOCLIENTE") = dr("CLIEV_TELEFONOCLIENTE")
            drsap("PEDIC_TIPOVENTA") = dr("PEDIC_TIPOVENTA")
            drsap("PEDIC_TIPOOFICINA") = dr("PEDIC_TIPOOFICINA")
            drsap("PEDIC_ISRENTA") = dr("PEDIC_ISRENTA")
            drsap("VENDEDOR") = dr("VENDEDOR")
            drsap("PEDID_FECHADOCUMENTO") = dr("PEDID_FECHADOCUMENTO")
            drsap("PEDIC_CODTIPOOPERACION") = dr("PEDIC_CODTIPOOPERACION")
            drsap("DEPEN_NROCUOTA") = dr("DEPEN_NROCUOTA")
            drsap("DEPEV_NROTELEFONO") = dr("DEPEV_NROTELEFONO")
            drsap("DEPEV_CODIGOLP") = dr("DEPEV_CODIGOLP")
            drsap("DEPEV_DESCRIPCIONLP") = dr("DEPEV_DESCRIPCIONLP")
            drsap("PAGON_IDPAGO") = dr("PAGON_IDPAGO")
            drsap("PEDIV_DRAASOCIADO") = dr("PEDIV_DRAASOCIADO")
            drsap("PEDIV_SISTEMAVENTA") = dr("PEDIV_SISTEMAVENTA")
            drsap("PEDIV_DESCTIPOOPERACION") = dr("PEDIV_DESCTIPOOPERACION")
            drsap("PEDIC_ESTADO") = dr("PEDIC_ESTADO")
            drsap("Estado_SAP") = "PROCESADO"
            drsap("ID_T_TRS_PEDIDO") = dr("ID_T_TRS_PEDIDO")
           'PROY-140397-MCKINSEY -> JSQ INICIO
            drsap("PEDMC_TIPO_ENTREGA") = dr("PEDMC_TIPO_ENTREGA")
            'PROY-140397-MCKINSEY -> JSQ FIN

            dtsap.Rows.Add(drsap)
        Next
        '**** fin Notas de Canje en el Pool ****'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ConsultaPoolPagos")

        'dtsap.DefaultView.Sort = "PEDID_FECHADOCUMENTO DESC" 'PROY-140397-MCKINSEY -> Jordy Sullca Q RevenixZ 'INCIDENCIA MEJORA SICAR
        dtsap.DefaultView.Sort = "PEDIN_NROPEDIDO DESC" 'INCIDENCIA MEJORA SICAR

        dgPool.DataSource = dtsap
        dgPool.DataBind()

        Session("dtsap") = dtsap

        If dtsap.Rows.Count = 0 Then
            Response.Write("<script>alert('No existen documentos para la fecha indicada')</script>")
        End If
    End Sub
    '----------------------------------------------------------------------------
    '--RMZ INICIO - PROY-32280 - PICKING -- 
    '----------------------------------------------------------------------------
    Protected Sub ColorearMultipunto(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgPool.ItemDataBound
        Dim hidFlagMP As System.Web.UI.HtmlControls.HtmlInputHidden
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            
            hidFlagMP = e.Item.Cells(18).FindControl("FLAG_MULTIPUNTO")
            Dim valorMultiPunto As String = hidFlagMP.Value

            If valorMultiPunto = "1" Then
                e.Item.BackColor = e.Item.BackColor.Yellow
                e.Item.Font.Bold = True
            End If
        End If
    End Sub
    '----------------------------------------------------------------------------
    '--RMZ FIN    - PROY-32280 - PICKING -- 
    '----------------------------------------------------------------------------

    'Private Sub btnCompensar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompensar.ServerClick
    '    'MODIFCADO POR FFS INICIO
    '    'Dim dvPagos As New DataView(ds.Tables(0))
    '    Dim dvPagos As New DataView(dtsap)
    '    'MODIFICADO POR FFS FIN
    '    If Len(Trim(Request.Item("rbPagos"))) > 0 Then
    '        dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
    '        drFila = dvPagos.Item(0).Row
    '        Session("DocSel") = drFila
    '        Response.Redirect("compensacion.aspx")
    '    End If
    'End Sub

    'Private Sub btnCompensar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompensar.Click
    '    'MODIFCADO POR FFS INICIO
    '    'Dim dvPagos As New DataView(ds.Tables(0))
    '    Dim dvPagos As New DataView(dtsap)
    '    'MODIFICADO POR FFS FIN
    '    If Len(Trim(Request.Item("rbPagos"))) > 0 Then


    '        Dim strEstadoRegistro As String = ConfigurationSettings.AppSettings("constEstadoPendienteReposicion")
    '        Dim intFlagBloqueo As Integer
    '        Dim strCodBloqueo As String
    '        Dim strTipoBloqueo As String
    '        Dim strIccidActual As String
    '        Dim strIccidNuevo As String
    '        Dim strUsuario As String
    '        Dim strMensaje As String ' Resultado general
    '        Dim strTipoVenta As String
    '        Dim sDocumentoSap As String = Request.Item("rbPagos")
    '        Dim obj As New COM_SIC_Cajas.clsCajas

    '        Dim intExistePedidoSisact As Integer = obj.Consulta_Existe_Pedido_Reposicion(sDocumentoSap, strEstadoRegistro, intFlagBloqueo, strCodBloqueo, strTipoBloqueo, strIccidActual, strIccidNuevo, strUsuario, strTipoVenta)


    '        If intExistePedidoSisact = 1 Then
    '            Dim sMensaje As String = ""
    '            sMensaje = "No se permite compensar en Reposiciones Pack Prepago"
    '            Response.Write("<script>alert('" & sMensaje & "');</script>")
    '            Exit Sub
    '        End If

    '        dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
    '        drFila = dvPagos.Item(0).Row
    '        Session("DocSel") = drFila
    '        Response.Redirect("compensacion.aspx")
    '    End If
    'End Sub


    Private Sub btnPagar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagar.Click
        'Dim dvPagos As New DataView(ds.Tables(0))}
        Dim objAct As New COM_SIC_Activaciones.clsTrsMsSap 'SD973999
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim dvPagos As New DataView(dtsap)
        Dim dsPedido As DataSet
        Dim correlativoSunat As String = ""

        Me.txtDocSap.Text = txtRbPagos.Value
        '***validaciòn del estado del pedido a pagar ****'

        Dim COD_RESPUESTA As String
        Dim MSJ_RESPUESTA As String
        Dim COD_MSG_RENTA As String
        'INICIO - IDEA-141711 - Pack Internet Móvil Prepago
        Dim COD_MSG_RECARGA As String
        'FIN - IDEA-141711 - Pack Internet Móvil Prepago
        Dim strIdentifyLog As String = Funciones.CheckStr(txtRbPagos.Value)

        Dim msg_VentaAlta As String = ""
        Dim cod_msg_ventaAlta As String = ""
        Dim rpt_consulta As String = ""

        'PROY-24724-IDEA-28174 - INI
        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
        Dim strNroPedido As String = Funciones.CheckStr(txtRbPagos.Value())
        Dim strNroPedidoEquipo As String = String.Empty
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        'PROY-24724-IDEA-28174 - FIN
        Session("FlagPoolPagos") = 1 'PROY 30166'
        Session("isVentaCtg") = False 'PROY-140715 
        Session("isVentaCtgUno") = False 'PROY-140715 

        'CAMPAÑA COMBO PREPAGO - INICIO
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO")

        Dim codRespuesta As String = String.Empty
        Dim msjRespuesta As String = String.Empty


        'PROY-140715 - IDEA 140805 | No Biometria en SISACT x caida RENIEC | Consultar Pedido contingencia |INI
        If strFlagGeneralCaidaNoBio.Equals("1") Then
            consultarPedidoVentaContingencia(strNroPedido)
        End If
        'PROY-140715 - IDEA 140805| FIN

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 1 " & strNroPedido)
            dsPedido = objConsultaMsSap.ConsultaPedido(strNroPedido, "", "")
            Dim valTipo_Venta As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 2 " & strNroPedido)
            If valTipo_Venta = ConfigurationSettings.AppSettings("strTVPrepago").ToString() Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 3 " & strNroPedido)
                ValidaPagoCombosPrepago(strNroPedido, codRespuesta, msjRespuesta)
                If codRespuesta = "1" Then
                    Response.Write("<script>alert('" & Funciones.CheckStr(msjRespuesta) & "');</script>")
                    Exit Sub
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 4 " & strNroPedido)
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR:" & "- " & msjRespuesta.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        End Try
        'CAMPAÑA COMBO PREPAGO - FIN

        '****** CONSULTA SI TIENE VENTA ALTA SIN PAGAR PARA SEGUIR EL PREOCESO *****'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta si ha Pagado la venta Alta o no exista antes de Pagar")

        Try
            rpt_consulta = objConsultaPvu.ValidacionPagodeVentaAltaconReno(Funciones.CheckDbl(txtRbPagos.Value), cod_msg_ventaAlta, msg_VentaAlta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "cod_msg_ventaAlta" & "- " & cod_msg_ventaAlta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "msg_VentaAlta" & "- " & msg_VentaAlta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "rpt_consulta" & "- " & rpt_consulta)

            If cod_msg_ventaAlta <> ConfigurationSettings.AppSettings("cod_msg_ventaAlta_valido").ToString Then
                If cod_msg_ventaAlta = ConfigurationSettings.AppSettings("cod_msg_ventaAlta_casiValido").ToString Then
                    Session("Mensaje_VentaAlta") = Funciones.CheckStr(msg_VentaAlta)
                Else
                    Response.Write("<script>alert('" & Funciones.CheckStr(msg_VentaAlta) & "');</script>")
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al consultar si ha Pagado la venta Alta o no exista antes de Pagar.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "cod_msg_ventaAlta" & "- " & cod_msg_ventaAlta)
            Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("Error_Consultar_VentaAlta_Reno").ToString & "');</script>")
            Exit Sub
        End Try


        '***************************************************************************************************'
        'JLOPETAS - PROY 140589 - INI
        Try

            If Funciones.CheckStr(Session("FLAG_PICKING")) = "1" AndAlso Funciones.CheckStr(Session("FLAG_DLV")) = "1" Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "********** Inicio Valida Pago DLV **********")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")
                Dim ISCOSTODLV As String
                Dim ISPEDCOSDLV As String
                Dim NROPEDCOSDLV As String
                Dim COD_RPTA As String
                Dim MSJ_RPTA As String
                Dim STR_MENSAJE As String
                Dim strIsPorta As String
                Dim strFlagPorta As String
                Dim arrLista As ArrayList
                Dim numSEC As String
                Dim strCdRespPorta As String
                Dim strMsjPorta As String
                Dim oPrePagoPedido As DataTable
                Dim oPrePagoDetPedido As DataTable
                Dim intPendienteSPdlv As Integer
                Dim strCodRptaDLV As String
                Dim strMsgRptaDLV As String
                Dim pstrCodRpta As String
                Dim pstrMsgRpta As String
                Dim blnExistePEP As Boolean
                Dim strLstIDSolicitud As New ArrayList
                Dim mensajeABDCP As String
                Dim intPendienteSP As Boolean = False
                Dim intProcesoSP As Integer
                Dim strMensajeDLV As String
                Dim ds_contenedor As DataSet
                Dim objConsulta As New COM_SIC_Activaciones.ClsPortabilidad
                Dim obj As New ClsPortabilidad

                arrLista = objConsultaMsSap.validaPagoDLV(Funciones.CheckDbl(txtRbPagos.Value), ISCOSTODLV, ISPEDCOSDLV, NROPEDCOSDLV, strIsPorta, strFlagPorta, COD_RPTA, MSJ_RPTA)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "NroPedido => " & Funciones.CheckStr(txtRbPagos.Value))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "ISCOSTODLV => " & Funciones.CheckStr(ISCOSTODLV))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "ISPEDCOSDLV => " & Funciones.CheckStr(ISPEDCOSDLV))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "NROPEDCOSDLV => " & Funciones.CheckStr(NROPEDCOSDLV))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "COD_RPTA => " & Funciones.CheckStr(COD_RPTA))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "MSJ_RPTA => " & Funciones.CheckStr(MSJ_RPTA))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "strIsPorta => " & Funciones.CheckStr(strIsPorta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "strFlagPorta => " & Funciones.CheckStr(strFlagPorta))

                Session("Flag_PagoDLV") = "0"

                If strFlagPorta = "1" AndAlso ISCOSTODLV = "1" Then

                    For Each item As ItemGenerico In arrLista

                        If Funciones.CheckStr(item.CODIGO2) = "01" Then

                            numSEC = Me.Obtener_NroSec_PostPago(Funciones.CheckStr(item.CODIGO)) 'NroSEC - POSTPAGO

                        ElseIf Funciones.CheckStr(item.CODIGO2) = "02 then" Then

                            numSEC = Me.Obtener_NroSec_PrePago(Funciones.CheckStr(item.CODIGO), strCdRespPorta, strMsjPorta, oPrePagoPedido, oPrePagoDetPedido) 'NroSEC - PREPAGO

                        End If

                        ds_contenedor = obj.Obtener_tipo_producto(numSEC, item.CODIGO, pstrCodRpta, pstrMsgRpta)



                        'If clsKeyAPP.consTipoProductoPermitidosSP.IndexOf(ds_contenedor.Tables(0).Rows(0).Item("PRDC_CODIGO")) > -1 Then
                        '    blValidaOperacion = True
                        'End If

                        ' ******************** VALIDA ENVIO SOLICITUD PORTABILIDAD - INICIO ********************

                        dsPedido = objConsultaMsSap.ConsultaPedido(Funciones.CheckInt64(item.CODIGO), "", "")

                        Dim theReturn As Int32 = validaEnvioSolPortaDLV(numSEC, dsPedido)

                        If theReturn = 1 Then

                            STR_MENSAJE = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MensajePorta_envio).Replace("{1}", Funciones.CheckStr(item.CODIGO))
                            Response.Write("<script>alert('" & STR_MENSAJE & "');</script>")
                            btnPagar.Attributes.Remove("disabled")
                            'Exit For
                            Exit Sub

                        ElseIf theReturn = 3 Then

                            STR_MENSAJE = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MensajePorta_aprobacion).Replace("{1}", Funciones.CheckStr(item.CODIGO))
                            Response.Write("<script>alert('" & STR_MENSAJE & "');</script>")
                            btnPagar.Attributes.Remove("disabled")

                        End If
                        ' ******************** VALIDA ENVIO SOLICITUD PORTABILIDAD - FIN ********************

                        ' ******************** VALIDA RESPUESTA DE SOLICITUD PORTABILIDAD - INICIO ********************

                        Dim objDatos As DataSet = objConsulta.ValidarSEC(numSEC, strCodRptaDLV, strMsgRptaDLV)

                        intPendienteSP = New clsActivaciones().ValidarEstadoPendienteSP(objDatos, ds_contenedor, blnExistePEP, strLstIDSolicitud, mensajeABDCP)

                        If intPendienteSP Then 'Pendiente de Respuesta de la Solicitud de Portabilidad
                            STR_MENSAJE = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MensajePorta_aprobacion).Replace("{1}", Funciones.CheckStr(item.CODIGO))
                            Response.Write("<script>alert('" & STR_MENSAJE & "');</script>")
                            btnPagar.Attributes.Remove("disabled")
                            'Exit For
                            Exit Sub
                        End If

                        intProcesoSP = Convert.ToInt64(New clsActivaciones().ValidarSECProcesoSP(numSEC, strMensajeDLV, CurrentTerminal, CurrentUser, Funciones.CheckInt64(item.CODIGO)))

                        If blnExistePEP Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Procede con exito ")
                            intProcesoSP = 1
                        End If


                        If intProcesoSP <> 1 AndAlso intProcesoSP <> 3 Then
                            STR_MENSAJE = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MensajePorta_aprobacion).Replace("{1}", Funciones.CheckStr(item.CODIGO))
                            Response.Write("<script>alert('" & STR_MENSAJE & "');</script>")
                            btnPagar.Attributes.Remove("disabled")
                            'Exit For
                            Exit Sub
                        End If

                        ' ******************** VALIDA RESPUESTA DE SOLICITUD PORTABILIDAD - FIN ********************
                    Next

                ElseIf ISCOSTODLV = "1" AndAlso strIsPorta <> "1" Then


                    STR_MENSAJE = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_Msj_Costo_DLV).Replace("{1}", Funciones.CheckStr(txtRbPagos.Value)).Replace("{2}", Funciones.CheckStr(NROPEDCOSDLV))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "STR_MENSAJE => " & Funciones.CheckStr(STR_MENSAJE))

                    If ISPEDCOSDLV = "0" Then
                        If COD_RPTA = "1" Then 'el pedido no se encuentra pagado
                            Session("Flag_PagoDLV") = "0"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "Flag_PagoDLV => " & Funciones.CheckStr(Session("Flag_PagoDLV")))
                            Response.Write("<script>alert('" & STR_MENSAJE & "');</script>")
                            btnPagar.Attributes.Remove("disabled")
                            Exit Sub
                        End If

                    Else
                        If COD_RPTA = "1" Then 'el pedido no se encuentra pagado
                            Session("Flag_PagoDLV") = "1"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cobro Delivery " & "- " & "Flag_PagoDLV => " & Funciones.CheckStr(Session("Flag_PagoDLV")))
                        End If
                    End If

                End If

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "************ FIN Valida Pago DLV ************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "************ ERROR Valida Pago DLV ************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************")
        End Try
        'JLOPETAS - PROY 140589 - FIN

        'PROY-140662 - DLV-F4 - INI
        Try
            Dim blValidacion As Boolean
            Dim intNroPedido As Int64
            Dim strFlagDLV As String
            Dim strIdOrdenTOA As String
            Dim strCodRPT As String
            Dim strMsjRPTA As String
            Dim strCodRespuesta As String
            Dim strEstado As String
            Dim ReadKeySettings As New COM_SIC_Seguridad.ReadKeySettings
            Dim paramEstadoAudit As String
            Dim paramEstadoTOA As String
            Dim Key_flagTOA As String

            Key_flagTOA = Funciones.CheckStr(ReadKeySettings.Key_EstadoOrdenTOA)

            intNroPedido = Funciones.CheckInt64(strNroPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  intNroPedido  =>" & strNroPedido)

            blValidacion = objConsultaPvu.GetOrdenTOA(intNroPedido, strFlagDLV, strIdOrdenTOA, strCodRPT, strMsjRPTA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  strFlagDLV =>" & strFlagDLV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  strIdOrdenTOA =>" & strIdOrdenTOA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  strCodRPT =>" & strCodRPT)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  strMsjRPTA =>" & strMsjRPTA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-  DELIVERY_FASE4 =>" & strMsjRPTA)

            If Key_flagTOA = "1" Then
            If strFlagDLV = "1" AndAlso strIdOrdenTOA <> "" Then

                Dim objWS As COM_SIC_Activaciones.BWObtenerOrdenToa
                Dim objMensajeResponse As COM_SIC_Activaciones.ObtenerOrdenToaResponse
                paramEstadoAudit = Funciones.CheckStr(ReadKeySettings.Key_EstadoAuditTOA)
                paramEstadoTOA = Funciones.CheckStr(ReadKeySettings.Key_EstadoOrdenTOA)


                objMensajeResponse = objWS.ObtenerOrdenToaWS(Funciones.CheckStr(strIdOrdenTOA))
                strCodRespuesta = objMensajeResponse.MessageResponse.Body.auditResponse.codigoRespuesta


                If paramEstadoAudit.IndexOf(strCodRespuesta) > -1 AndAlso Not objMensajeResponse.MessageResponse.Body.responseData Is Nothing Then

                    strEstado = objMensajeResponse.MessageResponse.Body.responseData.listaDetalleActividadType.estado

                    If paramEstadoTOA.IndexOf(strEstado) > -1 Then
                        Session("strFlagDLV") = strFlagDLV
                        Session("strEstadoTOA") = "1"
                        'Session("strIdOrdenTOA") = strIdOrdenTOA
                        'Session("ALMACEN")
                    Else
                        Response.Write("<script>alert('" & ReadKeySettings.Key_MsjOrdenTOA & "');</script>")
                        Exit Sub
                    End If


                End If

            Else
                Session("strEstadoTOA") = "0"

            End If
            End If

            'PROY-140662 - DLV-F4 - FIN


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Trace Error => " & ex.StackTrace)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Mensaje Error => " & ex.Message)

        End Try


        '****** CONSULTA SI TIENE RENTA SIN PAGAR PARA SEGUIR EL PREOCESO *****'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta si ha Pagado Renta antes de Pagar")

        Try
            objConsultaMsSap.Validar_PagoRenta(Funciones.CheckDbl(txtRbPagos.Value), COD_MSG_RENTA, COD_RESPUESTA, MSJ_RESPUESTA)


            If COD_MSG_RENTA = "0" Then
                'Session("MSG_RENTA") = "El documento tiene asociado Rentas Adelantadas sin Pago"
                Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_RENTA_ADELANTADA").ToString & "');</script>")
                'Response.Redirect("PoolPagos.aspx")
                Exit Sub
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error consulta si ha Pagado Renta antes de Pagar")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        End Try

        '****** CONSULTA SI TIENE ACCESORIO CON COSTO*****'----INICIATIVA-1006-TIENDA VIRTUAL-INICIO
        Dim intNroPedACC_cos As Int64
        Dim strFlagACC_cos As String
        Dim strEstadoPed_ACC_Cos As String
        Dim mensaje As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Inicia consulta si el pedido tiene accesorio con costo]")
        Try
            objConsultaMsSap.validaPedidoAccCosto(Funciones.CheckInt64(strNroPedido), intNroPedACC_cos, strFlagACC_cos, strEstadoPed_ACC_Cos, COD_RESPUESTA, MSJ_RESPUESTA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "====OUTPUT INI====")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(intNroPedACC_cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strFlagACC_cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strEstadoPed_ACC_Cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(COD_RESPUESTA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(MSJ_RESPUESTA))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "====OUTPUT FIN====")

            If strEstadoPed_ACC_Cos = "ACT" Then
                mensaje = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MsjAccPendientePago).Replace("{0}", Funciones.CheckStr(strNroPedido)).Replace("{1}", Funciones.CheckStr(intNroPedACC_cos))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo] " & "- " & "STR_MENSAJE => " & Funciones.CheckStr(mensaje))
                Response.Write("<script>alert('" & mensaje & "');</script>")
                Exit Sub
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al consultar si el pedido tiene accesorio con costo")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "[INICIATIVA-1006 | Tienda Virtual - Fin de consulta si el pedido tiene accesorio con costo]")
        End Try
        '****** CONSULTA SI TIENE ACCESORIO CON COSTO*****'----INICIATIVA-1006-TIENDA VIRTUAL-FIN

        'INICIO - IDEA-141711 - Pack Internet Móvil Prepago
        '****** CONSULTA SI TIENE RECARGAS SIN PAGAR PARA SEGUIR EL PROCESO *****'
        Try

            Dim strMsgPedido As String = ""
            Dim CodGrupoParamPackInternet As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodGrupoParamPackPrepago"))

            Dim objObtenerParamGrupo As New COM_SIC_Cajas.clsCajas
            Dim dsParamGrupoParamPackInternet As DataSet

            dsParamGrupoParamPackInternet = objObtenerParamGrupo.ObtenerParamByGrupo(CodGrupoParamPackInternet)

            Dim i As Integer
            For i = 0 To dsParamGrupoParamPackInternet.Tables(0).Rows.Count - 1
                If dsParamGrupoParamPackInternet.Tables(0).Rows(i).Item("PARAV_VALOR1") = "Key_MsgPedido" Then
                    strMsgPedido = dsParamGrupoParamPackInternet.Tables(0).Rows(i).Item("PARAV_VALOR")
                End If
            Next


            objConsultaMsSap.Validar_PagoRecarga(Funciones.CheckDbl(txtRbPagos.Value), COD_MSG_RECARGA, COD_RESPUESTA, MSJ_RESPUESTA)

            If COD_MSG_RECARGA = "0" Then
                'Session("MSJ_RESPUESTA") = "El documento tiene asociado Recargas sin Pago"
                Response.Write("<script>alert('" & strMsgPedido.ToString & "');</script>")
                'Response.Redirect("PoolPagos.aspx")
                Exit Sub
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error consulta si ha Pagado Recarga antes de Pagar")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        End Try

	'***************************************************************************************************'
        'FIN - IDEA-141711 - Pack Internet Móvil Prepago

        'PROY-23111-IDEA-29841 - INICIO
        '****** CONSULTA SI EL ACCESORIO TIENE PACK RELACIONADO PENDIENTE DE PAGO *****'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta si es accesorio y si tiene pack relacionado pendiente de pago")

        Dim strFiltroPedidoAcc As String
        Dim strPedidoAcc As String
        Dim strCodTipoOperacion As String        

        If (txtsession.Value = 1) Then
            strFiltroPedidoAcc = "ID_T_TRS_PEDIDO=" & txtRbPagos.Value
            strPedidoAcc = txtRbPagos.Value
        Else
            strFiltroPedidoAcc = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
            strPedidoAcc = Request.Item("rbPagos")
        End If
        'SD973999 - SICAR INICIO
        Dim strServicio As String
        Dim dsParamOf As DataSet
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim CanalRecarga As String
        'Dim VendedorRecarga As String
        'Dim AlmacenRecarga As String
        Dim objPagos As New COM_SIC_OffLine.clsOffline
        Dim dsVendedor As DataSet
        Dim dsCodMaterialRecarga As DataSet
        Dim strRetorna As String
        Dim materialRecarga As String
        Dim PedidoRecarga As Long
        Dim dsCanal As DataSet
        Dim AlmRecarga As String = Session("ALMACEN")

        PedidoRecarga = Funciones.CheckInt64(strPedidoAcc)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Consulta Pedido")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP NROPEDIDO " & PedidoRecarga.ToString())
        dsPedido = objConsultaMsSap.ConsultaPedido(PedidoRecarga, "", "")

        '140245 Inicio
        If Not dsPedido Is Nothing Then
            If dsPedido.Tables(1).Rows.Count > 0 Then
                strNro_Ped_Det = dsPedido.Tables(1).Rows(0).Item("DEPEN_CONSECUTIVO")
                Session("strNro_Ped_Det") = strNro_Ped_Det
                'tipo operacion 
                strTipo_opera_Des = dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION")
                Session("strTipo_opera_Des") = strTipo_opera_Des
                strTipo_Opera_Cod = dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION")
                Session("strTipo_Opera_Cod") = strTipo_Opera_Cod
                'log
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llenar Numero Pedido Det" & strNro_Ped_Det)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llenar Tipo Operacion Des" & strTipo_opera_Des)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llenar Tipo Operacion Des" & strTipo_opera_Des)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llenar Tipo Operacion Cod" & strTipo_Opera_Cod)

            End If
        End If

        '140245 Fin

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Consulta Pedido")
        If Not dsPedido Is Nothing Then
            materialRecarga = dsPedido.Tables(1).Rows(0).Item("DEPEC_CODMATERIAL")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO Get_ConsultaOficinaVenta")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP ALMACEN " & AlmRecarga)
            dsCanal = objPagos.Get_ConsultaOficinaVenta(AlmRecarga, "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN Get_ConsultaOficinaVenta")

            If dsCanal.Tables(0).Rows.Count > 0 Then
                CanalRecarga = dsCanal.Tables(0).Rows(0).Item("VTWEG")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO FP_Lista_Param_Oficina")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP CANAL " & CanalRecarga)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP OFICINA " & AlmRecarga)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP APLICACION " & ConfigurationSettings.AppSettings("CodAplicacion").ToString())
                dsParamOf = objConf.FP_Lista_Param_Oficina(CanalRecarga, AlmRecarga, ConfigurationSettings.AppSettings("CodAplicacion"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN FP_Lista_Param_Oficina")

                strServicio = dsParamOf.Tables(0).Rows(0).Item("CAJA_RECVIRTUAL")
                If strServicio <> "" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO ConsultaCodigoMaterialRecargaVirtual")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "INP VALOR HISTORICO " & strServicio)
                    dsCodMaterialRecarga = objConf.ConsultaCodigoMaterialRecargaVirtual(strServicio)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN ConsultaCodigoMaterialRecargaVirtual")

                    If dsCodMaterialRecarga.Tables(0).Rows.Count > 0 Then
                        strRetorna = dsCodMaterialRecarga.Tables(0).Rows(0).Item("VALORACTUAL")
                    End If
                End If
                If materialRecarga = strRetorna Then
                    Session("recargaVirtual") = True
                Else
                    Session("recargaVirtual") = False
                End If
            End If
        End If

        'INICIO IDEA300216

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "******** Inicio Validacion CLAVE UNICA ********")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")
            dvPagos.RowFilter = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
            drFila = dvPagos.Item(0).Row
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "TIPO_ENTREGA: " & Funciones.CheckStr(drFila("TIPO_ENTREGA").ToString()))
            Dim nroPedido60 = Funciones.CheckStr(Request.Item("rbPagos"))
            Dim sRptaValidaCU As String = String.Empty
            Dim sNroSEC As String = String.Empty
            Dim sTipoVenta As String = String.Empty
            Dim sTransaccion As String = String.Empty
            Dim sTipoDoc As String = String.Empty
            Dim sNumDoc As String = String.Empty
            Dim sRptaValNotificacion As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "nroPedido60: " & Funciones.CheckStr(nroPedido60))

            sRptaValidaCU = GetDataValidarClaveUnica(nroPedido60, sTipoDoc, sNumDoc, sNroSEC, sTipoVenta, sTransaccion)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sRptaValidaCU: " & Funciones.CheckStr(sRptaValidaCU))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTipoDoc: " & Funciones.CheckStr(sTipoDoc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNumDoc: " & Funciones.CheckStr(sNumDoc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNroSEC: " & Funciones.CheckStr(sNroSEC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTipoVenta: " & Funciones.CheckStr(sTipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTransaccion: " & Funciones.CheckStr(sTransaccion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Key_DescripcionOperacionPortabilidad: " & Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionPortabilidad))

            If sRptaValidaCU = "0" And Not Funciones.CheckStr(sTransaccion).Equals(Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionPortabilidad)) Then
                Dim sMsjeFinal As String = String.Empty
                Dim sCodFinal As String = String.Empty

                sRptaValNotificacion = GetAprobClaveUnica(sTipoDoc, sNumDoc, sNroSEC, nroPedido60, sTipoVenta, sTransaccion)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sRptaValNotificacion: " & Funciones.CheckStr(sRptaValNotificacion))

                If sRptaValNotificacion.IndexOf(";") > -1 Then
                    sCodFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(0))
                    If sCodFinal = "0" Or sCodFinal = "1" Then 'CLAVE UNICA PENDIENTE
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    ElseIf sCodFinal = "2" Then 'CLAVE UNICA APROBADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                    ElseIf sCodFinal = "3" Then 'CLAVE UNICA EXPIRADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    ElseIf sCodFinal = "5" Then 'SE OMITIO X REGLA CLAVE UNICA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                    ElseIf sCodFinal = "4" Or sCodFinal = "6" Then 'ANULADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    End If
                Else
                    Response.Write("<script>alert('" & Funciones.CheckStr(ReadKeySettings.Key_MensajeErrorConsultaClaveUnica) & "');</script>")
                    Exit Sub
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NO PASARA CLAVE UNICA TRANSACCION O PRODUCTO NO REGULADO")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Excepcion: " & Funciones.CheckStr(ex.Message))
            Response.Write("<script>alert('" & ex.Message & "');</script>")
            Exit Sub
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "********** Fin Validacion CLAVE UNICA *********")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")

        End Try
       
        'FIN IDEA300216
        '***************************************************************************************************'
        '***********Verificar CP Portabilidad PROY-26963 fase 2 INI***************** :: 1 ::
        '***************************************************************************************************'

        'Inicio PROY 32089
        Dim strMensaje As String = "", strCodigo As String = ""
        Dim EnvioSolicitudPortabilidad As String = "N"
        Dim objAudit As New BEAuditoria
        objAudit.Terminal = CurrentTerminal
        objAudit.Usuario = CurrentUser
        objAudit.HostName = Funciones.CheckStr(Request.UserHostName)
        objAudit.ServerName = Funciones.CheckStr(Request.ServerVariables("REMOTE_ADDR"))
        objAudit.IPServer = Funciones.CheckStr(Request.ServerVariables("HTTP_X_FORWARDED_FOR"))


        Dim strDescriOpePostPago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
        Dim strDescriOpePrePago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
        Dim strTipoOperacionPago As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION"))
        Dim strNroSec As String
        Dim NroPed64Porta As Integer = Convert.ToInt64(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))

        Dim strCodRespPorta As String
        Dim strMensajPorta As String
        Dim odtPrePagoPedido As DataTable
        Dim odtPrePagoDetPedido As DataTable

        Dim nroPedidoPin As String = String.Empty ''JMGF

        If (strDescriOpePostPago = strTipoOperacionPago) Then
            strNroSec = Me.Obtener_NroSec_PostPago(Funciones.CheckStr(PedidoRecarga))
            nroPedidoPin = Funciones.CheckStr(PedidoRecarga) ''JMGF
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][PedidoRecarga][nroPedidoPin] = " & nroPedidoPin) ''JMGF
        ElseIf strDescriOpePrePago = strTipoOperacionPago Then
            strNroSec = Me.Obtener_NroSec_PrePago(NroPed64Porta, strCodRespPorta, strMensajPorta, odtPrePagoPedido, odtPrePagoDetPedido)
            nroPedidoPin = Funciones.CheckStr(NroPed64Porta) ''JMGF
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][NroPed64Porta][nroPedidoPin] = " & nroPedidoPin) ''JMGF
        End If
        'Fin PROY 32089

        'INI PROY 140585 F2
        Dim strOpePortaPost = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
        Dim strOpePortaPre = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO ConsultaPinPortabilidad")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strTipo_opera_Des] => " & strTipo_opera_Des)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strNroSec] => " & strNroSec)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strOpePortaPost] => " & strOpePortaPost)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strOpePortaPre] => " & strOpePortaPre)

        If strTipo_opera_Des = strOpePortaPost Or strTipo_opera_Des = strOpePortaPre Then
            'PROY-140740 INI
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-INICIO Consultar Oferta")
            Dim strOferta As String
            Dim strOfertaBusiness = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsOfertaBusiness"))
            strOferta = ConsultaOferta(strNroSec)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-Oferta-->" + strOferta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-FIN Consultar Oferta")
            If strOferta <> strOfertaBusiness Then
            Dim sResultado As String
            sResultado = ConsultaPinPortabilidad(strNroSec, NroPed64Porta)
            Dim ArrResult() = sResultado.Split("|"c)
            If ArrResult.Length > 0 Then
                If ArrResult(0) <> "true" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][DEBE PASAR PIN PORTABILIDAD POR APLICATIVO MOVIL]")
                    Response.Write("<script>alert('" & ArrResult(1) & "');</script>")
                    Exit Sub
                End If
            End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-ES BUSINESS NO DEBE PASAR PIN PORTABILIDAD")
                'PROY-140740 FIN
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN ConsultaPinPortabilidad")
        ''PROY-140585 F2
        End If


        'SD973999 - SICAR FIN
        dvPagos.RowFilter = strFiltroPedidoAcc
        drFila = dvPagos.Item(0).Row
        strCodTipoOperacion = drFila("PEDIC_CODTIPOOPERACION").ToString()

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo de operación: " & strCodTipoOperacion)

        If strCodTipoOperacion = ConfigurationSettings.AppSettings("COD_TIPO_OPERACION_VENTA_VARIOS").ToString() Then
            Dim dsPedidoAcc As New DataSet
            Dim dsPedidoPack As New DataSet
            Dim strResultado As String
            Dim strPedidoPack As String
            Dim strEstadoPedidoPack As String

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Entrada SP_OBTENER_PACK_ACC: P_PEDIDO_ACC = " & strPedidoAcc)
                dsPedidoAcc = objConsultaPvu.ObtenerPedidoPackAccesorio(strPedidoAcc, strResultado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Salida SP_OBTENER_PACK_ACC: K_RESULTADO = " & strResultado)

                If strResultado = "OK" Then
                    If Not dsPedidoAcc Is Nothing Then
                        If dsPedidoAcc.Tables(0).Rows.Count > 0 Then
                            strResultado = String.Empty
                            strPedidoPack = dsPedidoAcc.Tables(0).Rows(0)(1)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Entrada SSAPSS_PEDIDO_PACK_ACC: P_PEDIDO_PACK = " & strPedidoPack)
                            dsPedidoPack = objConsultaMsSap.ValidarPagoPackAccesorio(strPedidoPack, strResultado)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Salida SSAPSS_PEDIDO_PACK_ACC: K_RESULTADO = " & strResultado)

                            If strResultado = "OK" Then
                                If Not dsPedidoPack Is Nothing Then
                                    If dsPedidoPack.Tables(0).Rows.Count > 0 Then
                                        strEstadoPedidoPack = dsPedidoPack.Tables(0).Rows(0)(1)

                                        If strEstadoPedidoPack <> ConfigurationSettings.AppSettings("ESTADO_PAG").ToString() Then
                                            Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_PAGO_PACK").ToString & "');</script>")
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Else
                                Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC").ToString & "');</script>")
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_ERROR_VALIDAR_ACC").ToString & "');</script>")
                    Exit Sub
                End If
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error consulta si es accesorio y si tiene pack relacionado pendiente de pago")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
                Exit Sub
            End Try
        End If

        '***************************************************************************************************'
        'PROY-23111-IDEA-29841 - FIN

        'PROY-24724-IDEA-28174 - INI
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "== Consulta Proteccion Movil - INICIO ==")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "== PKG_CONSULTA.SSAPSS_VALIDA_ASURION ==")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "==     IN NRO PEDIDO PM: " & strNroPedido)
        objProteccionMovil.ValidaPagoEquipoProteccionMovil(strNroPedido, strNroPedidoEquipo, strCodRpta, strMsgRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "==     OUT NRO PEDIDO Equipo: " & Funciones.CheckStr(strNroPedidoEquipo))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "==     OUT Cod Rpta " & strCodRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "==     OUT Msg Rpta " & strMsgRpta)

        If Not strCodRpta.Equals("0") And Not strCodRpta.Equals("-2") Then
            If strCodRpta.Equals("-1") Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "==     EQUIPO DE PROTECCIÓN MÓVIL PENDIENTE PAGO    ==")
                Dim strMensajePendientePago As String = Replace(clsKeyAPP.strProteccionMovilNoPagada, "*", Funciones.CheckStr(strNroPedidoEquipo)) 'PROY-24724-IDEA-28174 -  PARAMETROS
                Response.Write("<script>alert('" & strMensajePendientePago & "');</script>")
                Exit Sub
            Else
                Response.Write("<script>alert('" & clsKeyAPP.strProteccionMovilError & " ');</script>") 
                Exit Sub
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & "- " & "== Consulta Proteccion Movil - INICIO ==")
        'PROY-24724-IDEA-28174 - FIN

        Try
            If Len(Trim(Request.Item("rbPagos"))) > 0 And hidVerif.Value = "1" Then
                Dim filter As String = String.Empty
                If (txtsession.Value = 1) Then
                    filter = "ID_T_TRS_PEDIDO=" & txtRbPagos.Value
                Else
                    filter = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
                End If

                dvPagos.RowFilter = filter

                drFila = dvPagos.Item(0).Row

                If (txtsession.Value = 1) Then
                    Dim TipDocumento$ = drFila.Item("PEDIC_CLASEFACTURA")
                    If TipDocumento = "ZPVR" Then '
                        drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("constClaseFactura").ToString
                    ElseIf (TipDocumento = "ZPBR") Then
                        drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("constClaseVoleta").ToString
                    End If
                    drFila.AcceptChanges()
                End If
                Session("DocSel") = drFila  'ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA")

                'If drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then 'ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA") Then
                '    Response.Redirect("depGaran.aspx")
                'End If
                'TODO RECIBE_PAGO y NUMBR

                'Session("recargaVirtual") = (txtsession.Value = 1) 'SD973999 - Paquetizado SICAR
                Session("numeroTelefono") = drFila.Item("CLIEV_TELEFONOCLIENTE")
                'Response.Redirect("Detapago.aspx?pDocSap=" & txtRbPagos.Value & "&numeroTelefono=" & drFila("CLIEV_TELEFONOCLIENTE") & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO"))


                'VALIDACION BIOMETRICA PROY-25335-0 - Oscar Atencio Timana

                'PROY-25335 - 0

                'PROY-140715 - IDEA 140805 | No Biometria en SISACT x caida RENIEC | Omitir Validaciones BIOMETRIA |INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - strTipoContingenciaCaidaNoBio : " & strTipoContingenciaCaidaNoBio) 'INC000004636534

                If Not strTipoContingenciaCaidaNoBio.Equals("2") Then     'INC000004636534 CAMBIAR POR LA RESPUES DEL SERVICIO SI ES QUE NO DEBE PASAR POR ESTE FLUJO
                'PROY-25335 - 0
                Try

                Dim objVentaRenovacionPack As COM_SIC_Cajas.VentaRenovaPost
                Dim strTipDocCliente As String = dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE")
                Dim strNroDocCliente As String = dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE")
                Dim strTipoVenta As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente" & " : " & Funciones.CheckStr(strTipDocCliente))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "strNroDocCliente" & " : " & Funciones.CheckStr(strNroDocCliente))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipoVenta" & " : " & Funciones.CheckStr(strTipoVenta))
                Dim NroPed64 As New Int64

                If drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    NroPed64 = Convert.ToInt64(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOALTA")))
                Else
                    NroPed64 = Convert.ToInt64(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))
                End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "NroPed64" & " : " & Funciones.CheckStr(NroPed64))

                    Dim strBioTipoValidacion As String = ""
                    Dim strRespuestaBioHit As String = ""
                    Dim strCodRptaBio As String = ""
                    Dim strMgsRptaBio As String = ""
                    Dim StrBioExito As String = ""
                    Dim StrValidacionInicialExito As String = ""
                    Dim StrTipoProdSec As String = ""
                Dim strMensNoHit As String = ""
                Dim strMensFecha As String = ""
                Dim strIncapacidad As String = ""
                Dim strNumHoras As Integer = 0

                Dim strOffBio As String = ConsultaPuntoVentaBio(Session("ALMACEN"))
                Dim sNroSec As String = Me.Obtener_NroSec_PostPago(Funciones.CheckStr(NroPed64))
                    '140245 Inicio 
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Proy-140245")
                    strNro_Sec = sNroSec
                    Session("strNro_Sec") = strNro_Sec
                    'log 
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Llenar Numero Sec" & strNro_Sec)
                    '140245 Fin

                Dim dsRRLL As DataSet
                Dim dsBio As DataSet
                Dim dsParametros As DataSet
                Dim dtFechaBio As DateTime
                    Dim Flag_pack_reno As String = ""
                    Dim str_KeyRenovacionPermitida As String = ""
                    'pROY 25335 R2 INI
                    Dim str_Key_ModalidadMigracion As String = ""
                    Dim str_Key_ProductoPermitido As String = ""
                    Dim str_Key_TipoOperacionPermitida As String = ""
                    'pROY 25335 R2 FIN
                Dim strResultadoCartaPoder As String = ""

                If sNroSec.Equals("1") Or sNroSec.Equals("") Then
                    sNroSec = 0
                End If

                Dim NroSec64 As New Int64
                If sNroSec.Equals("") Or sNroSec.Equals("1") Or sNroSec.Equals("0") Then
                    NroSec64 = 0
                Else
                    NroSec64 = Convert.ToInt64(sNroSec)
                End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "NroSec64" & " : " & Funciones.CheckStr(NroSec64))


                Dim strCodigoTipoOperacionBio = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strCodigoTipoOperacionBio" & " : " & Funciones.CheckStr(strCodigoTipoOperacionBio))

                    'PROY-30182-IDEA-40535-INICIO
                    Dim outoffbio = String.Empty
                    Dim strTipo_Venta = drFila.Item("PEDIC_TIPOVENTA")
                    Dim objGrupoParam As New COM_SIC_Cajas.clsCajas
                    Dim CodGrupoParamPortaPrepagoTV = Funciones.CheckStr(ConfigurationSettings.AppSettings("constCodGrupoParam_PortaPrepagoTV").ToString())
                    Dim strDescTipoOperacion As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_DESCTIPOOPERACION"))

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO MÉTODO : objConsultaPvu.ValidarIdentidad: " & strDescTipoOperacion)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO MÉTODO : objConsultaPvu.ValidarIdentidad: " & Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_ALTA_PRE").ToString()))

                    'INC000002904777 - G.G.A  - SOLUCION - INICIO
                    Dim strCodRespuestaPrepago As String = ""
                    Dim strCodProductoPrepago As String = ""
                    Dim blnIsOLO = False
                    Dim objClsActivacionPEL As New ClsActivacionPel
                    objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- strTipo_Venta : " & Funciones.CheckStr(strTipo_Venta))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- NroPed64 : " & Funciones.CheckStr(NroPed64))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- strConstCodProdOLO : " & Funciones.CheckStr(clsKeyOLO.strConstCodProdOLO))
                    'consKeyflagIsOLO =>  1 : "activo" ; 0 : "inactivo" 
                    If strTipo_Venta = ConfigurationSettings.AppSettings("strTVPrepago").ToString() _
                      And ConfigurationSettings.AppSettings("consKeyflagIsOLO").ToString() = "1" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- Llamando a  objClsActivacionPEL.Lis_Lista_Detalle_Venta_Prepago()")
                        Dim arrListaPrepago As ArrayList = objClsActivacionPEL.Lis_Lista_Detalle_Venta_Prepago(Funciones.CheckStr(NroPed64), strCodRespuestaPrepago)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- Fin de llamando a  objClsActivacionPEL.Lis_Lista_Detalle_Venta_Prepago()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- strCodRespuestaPrepago : " & Funciones.CheckStr(strCodRespuestaPrepago))
                        If arrListaPrepago.Count > 0 Then
                            For Each item As COM_SIC_Activaciones.DetalleVentaPrepago In arrListaPrepago
                                strCodProductoPrepago = Funciones.CheckStr(item.COD_PROD_PREP)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- strCodProductoPrepago : " & Funciones.CheckStr(strCodProductoPrepago))
                                If strCodProductoPrepago = Funciones.CheckStr(clsKeyOLO.strConstCodProdOLO) Then
                                    blnIsOLO = True
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, NroPed64 & "- blnIsOLO : " & Funciones.CheckStr(blnIsOLO))

                    'INC000002904777 - G.G.A  - SOLUCION - FIN
                        '' IDEA-142730 - INI fsd
                        Dim desTipoOpAlPre = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_ALTA_PRE")
                        Dim codOpRen = ConfigurationSettings.AppSettings("COD_OPERACION_RENOVACION")
                        Dim codOpPorRe = ConfigurationSettings.AppSettings("COD_OPERACION_REPOSICION")
                        Dim desTipOpPorPre = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE")
                        Dim tvrPre = ConfigurationSettings.AppSettings("strTVPrepago")

                        If strTipo_Venta = tvrPre.ToString() _
                        And (strDescTipoOperacion = desTipOpPorPre.ToString() OrElse _
                        strDescTipoOperacion = desTipoOpAlPre.ToString() OrElse _
                        strCodTipoOperacion = codOpRen.ToString() OrElse _
                        strCodTipoOperacion = codOpPorRe.ToString()) Then

                            '' IDEA-142730 - fin fsd

                        If blnIsOLO = False Then  'INC000002904777 - G.G.A  - SOLUCION - INICIO
                        Dim dsReturn As DataSet
                        dsReturn = objConsultaMsSap.ConsultaOutoffbioTiendaVirtual(Session("ALMACEN"))
                        If dsReturn.Tables(0).Rows.Count > 0 Then
                            outoffbio = dsReturn.Tables(0).Rows(0).Item("OVENV_OUTOFFBIO")
                        End If

                        If outoffbio = "1" Then
                            Dim dsPdV_Sinergia As DataSet = objConsultaMsSap.ConsultaPuntoVenta(Session("ALMACEN"))
                            Dim PdV_Sinergia = CStr(dsPdV_Sinergia.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))
                            Dim i As Integer
							Dim strMsgErrorValidarIdentidadPortaPrepago As String = String.Empty
                            Dim dsParamGrupo As DataSet
                            dsParamGrupo = objGrupoParam.ObtenerParamByGrupo(CodGrupoParamPortaPrepagoTV)
                            Dim strPdvTiendaVirtual As String = String.Empty
                            Dim strDocuParam As String = String.Empty
                            For i = 0 To dsParamGrupo.Tables(0).Rows.Count - 1
                                If dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "PDV_TV" Then
                                    strPdvTiendaVirtual = strPdvTiendaVirtual + dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR") + "|"
                                ElseIf dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "2" Then
                                    strMsgErrorValidarIdentidadPortaPrepago = dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR")
                                ElseIf dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "4" Then 'INC000002444187                                                                 
                                    strDocuParam = dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR") 'INC000002444187                                                                 
                                End If
                            Next
                            'PROY-32851 :: INI
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strPdvTiendaVirtual" & Funciones.CheckStr2(strPdvTiendaVirtual))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "PdV_Sinergia" & Funciones.CheckStr2(PdV_Sinergia))
                            Dim bln_PuntosVentaAPK As Boolean = False
                            Dim CodParamPuntosVentaAPK  As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ContratacionElectronica_apk"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "CodParamPuntosVentaAPK" & " : " & Funciones.CheckStr(CodParamPuntosVentaAPK))
                            dsParametros = objConsultaPvu.ListaParametrosGrupo(CodParamPuntosVentaAPK)
                            
                            If dsParametros.Tables(0).Rows.Count > 0 Then
                                For Each fila As DataRow In dsParametros.Tables(0).Rows
                                    if Funciones.CheckStr(fila(2)).Equals(PdV_Sinergia) Then
                                        bln_PuntosVentaAPK=True
                                        Exit For
                                    End If 
                                Next
                            End If
                                    Dim tipDocCliente As String
                                    tipDocCliente = ConfigurationSettings.AppSettings("tipo_Docu_RUC")

                             
                            If (strPdvTiendaVirtual.IndexOf(PdV_Sinergia) > -1 Or bln_PuntosVentaAPK) And strDocuParam.IndexOf(strTipDocCliente) > -1 Then  'INC000002444187                                                                 
                            'PROY-32851 :: FIN
									objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO MÉTODO : objConsultaPvu.ValidarIdentidad")
									objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente : " & strTipDocCliente)
									objFileLog.Log_WriteLog(pathFile, strArchivo, "strNroDocCliente : " & strNroDocCliente)
									objFileLog.Log_WriteLog(pathFile, strArchivo, "NroSec64 : " & NroSec64)
									objFileLog.Log_WriteLog(pathFile, strArchivo, "NroPed64 : " & NroPed64)
                                    dsBio = objConsultaPvu.ValidarIdentidad(strTipDocCliente, strNroDocCliente, NroSec64, NroPed64, strCodRptaBio, strMgsRptaBio)
                            'PROY-32851 :: INI
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strNroDocCliente : " & strNroDocCliente)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroPed64 : " & NroPed64)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroSec64 : " & NroSec64)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strCodRptaBio : " & strCodRptaBio)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strMgsRptaBio : " & strMgsRptaBio)
                            'PROY-32851 :: FIN
                                    If dsBio Is Nothing Then	
										objFileLog.Log_WriteLog(pathFile, strArchivo, " - Codigo respuesta - " & strCodRptaBio)
										objFileLog.Log_WriteLog(pathFile, strArchivo, " - Mensaje validación : " & strMsgErrorValidarIdentidadPortaPrepago)
										Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                        Return
                                    Else
                                        If strCodRptaBio.Equals("1") Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Mensaje validación : " & strMsgErrorValidarIdentidadPortaPrepago)
											Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                            Return
                                        End If
                                    End If
                                        '' IDEA-142730 - INI fsd

                                    ElseIf (strTipDocCliente.Equals(tipDocCliente)) Then
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente" & " : " & Funciones.CheckStr(strTipDocCliente))

                                        strResultadoCartaPoder = objConsultaPvu.ConsultarSecConCartaPoder(Funciones.CheckStr(sNroSec), Funciones.CheckStr(NroPed64))
                                        If (strResultadoCartaPoder = 1) Then 'HC  =1 lo correcto
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))

                                            ''LISTAR REPRESENTANTE LEGAL - ini
                                            Dim intRrll As New Int32
                                            intRRLL = 0
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & "Consulta")
                                            Dim dsPdvSinergiaRuc As DataSet = objConsultaMsSap.ConsultaPuntoVenta(Session("ALMACEN"))
                                            Dim pdvSinergiaRuc = CStr(dsPdvSinergiaRuc.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))
                                            Dim dtRepChip = ConfigurationSettings.AppSettings("strDTVReposicionChip")
                                            If Not (strCodigoTipoOperacionBio.Equals(dtRepChip.ToString()) AndAlso bln_PuntosVentaAPK) Then
                                                dsRRLL = objConsultaPvu.ListaRepresentanteLegal(NroPed64)
                                            End If
                                            If Not dsRRLL Is Nothing Then
                                                intRRLL = dsRRLL.Tables.Count()
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & Convert.ToString(IntRrll))
                                            Else
                                                intRRLL = 0
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & " 0")
                                            End If

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRrll" & " : " & Funciones.CheckStr(IntRrll))

                                            ''LISTAR REPRESENTANTE LEGAL - fin


                                            '' validar si es DNI RRLL
                                            Dim strTipDocRrll As String = dsRRLL.Tables(0).Rows(0).Item("SRLV_TIPO_DOCUMENTO_RL")
                                            Dim strNroDocRrll As String = dsRRLL.Tables(0).Rows(0).Item("SRLV_NRO_DOCUMENTO_RL")

                                            If strTipDocRrll.Equals("01") Then  ' HC 01 lo correcto
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & "Consulta")
                                                dsBio = objConsultaPvu.ValidarIdentidad(strTipDocRrll, strNroDocRrll, NroSec64, NroPed64, strCodRptaBio, strMgsRptaBio)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strNroDocRrll : " & strNroDocRrll)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroPed64 : " & NroPed64)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroSec64 : " & NroSec64)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strCodRptaBio : " & strCodRptaBio)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strMgsRptaBio : " & strMgsRptaBio)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocRrll" & " : " & Funciones.CheckStr(strTipDocRrll))
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRrll" & " : " & Funciones.CheckStr(IntRrll))
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                                If ((Not dsBio Is Nothing) AndAlso (dsBio.Tables(0).Rows.Count > 0)) Then
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, " dsBio" & "- " & "  Not dsBio Is Nothing")
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & " 0")
                                                    strIncapacidad = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_FLA_INCAPACIDAD"))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                                    strRespuestaBioHit = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_RPTAVAL"))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))
                                                    dtFechaBio = Funciones.CheckDate(dsBio.Tables(0).Rows(0).Item("BIOM_FECHA_CREA"))
                                                Else
                                                    If (strCodRptaBio = "0") Then
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " dsBio" & "- " & "  Not dsBio Is Nothing")
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & " 0")
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))

                                                    Else
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                                        Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                                        Return
                                                    End If
                            End If
                                            Else
                                                Dim codTipoOperacionBio = ConfigurationSettings.AppSettings("strDTVReposicionChip")
                                                Dim dtvRenovacion = ConfigurationSettings.AppSettings("strDTVRenovacion")
                                                If (strCodigoTipoOperacionBio.Equals(codTipoOperacionBio.ToString()) OrElse strCodigoTipoOperacionBio.Equals(dtvRenovacion.ToString())) Then
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strCodigoTipoOperacionBio" & " : " & Funciones.CheckStr(strCodigoTipoOperacionBio))
                                                    If strResultadoCartaPoder.Equals("1") AndAlso strIncapacidad.Equals("1") Then
                                                            StrValidacionInicialExito = "True"
                                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                                        Else
                                                            StrValidacionInicialExito = "False"
                                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                            End If
                                                    Else
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRRLL" & " : " & Funciones.CheckStr(IntRRLL))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                                    If IntRRLL = 1 AndAlso strResultadoCartaPoder.Equals("1") AndAlso strIncapacidad.Equals("1") Then
                                                            StrValidacionInicialExito = "True"
                                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                                        Else
                                                            StrValidacionInicialExito = "False"
                                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                                        End If
                                                    End If
                                                End If




                                            End If

                            End If
                                    ''IDEA-142730 fin FSD
                        End If                    
                        'PROY-32851 :: INI
                        End If   'INC000002904777 - G.G.A  - SOLUCION - FIN
                    ElseIf strTipo_Venta = ConfigurationSettings.AppSettings("strTVPrepago").ToString() _
                    And strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenoRepoPre")) Then

                        If blnIsOLO = False Then  'INC000002904777 - G.G.A  - SOLUCION - INICIO

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---TIPO VENTA: ---" & strTipo_Venta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---TIPO OPERACION: ---" & strCodigoTipoOperacionBio)

                        Dim dsPdV_Sinergia As DataSet = objConsultaMsSap.ConsultaPuntoVenta(Session("ALMACEN"))
                        Dim PdV_Sinergia = CStr(dsPdV_Sinergia.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))
                        Dim i As Integer
                        Dim strMsgErrorValidarIdentidadPortaPrepago As String = String.Empty
                        Dim dsParamGrupo As DataSet
                        dsParamGrupo = objGrupoParam.ObtenerParamByGrupo(CodGrupoParamPortaPrepagoTV)

                        For i = 0 To dsParamGrupo.Tables(0).Rows.Count - 1
                            If dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "2" Then
                                strMsgErrorValidarIdentidadPortaPrepago = dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR")
                            End If
                        Next

                        Dim bln_PuntosVentaAPK As Boolean = False
                        Dim CodParamPuntosVentaAPK As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ContratacionElectronica_apk"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "CodParamPuntosVentaAPK" & " : " & Funciones.CheckStr(CodParamPuntosVentaAPK))
                        dsParametros = objConsultaPvu.ListaParametrosGrupo(CodParamPuntosVentaAPK)

                        If dsParametros.Tables(0).Rows.Count > 0 Then
                            For Each fila As DataRow In dsParametros.Tables(0).Rows
                                If Funciones.CheckStr(fila(2)).Equals(PdV_Sinergia) Then
                                    bln_PuntosVentaAPK = True
                                    Exit For
                                End If
                            Next
                        End If

                        If bln_PuntosVentaAPK Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO MÈTODO : objConsultaPvu.ValidarIdentidad")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente : " & strTipDocCliente)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strNroDocCliente : " & strNroDocCliente)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "NroSec64 : " & NroSec64)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "NroPed64 : " & NroPed64)
                            dsBio = objConsultaPvu.ValidarIdentidad(strTipDocCliente, strNroDocCliente, NroSec64, NroPed64, strCodRptaBio, strMgsRptaBio)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strNroDocCliente : " & strNroDocCliente)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroPed64 : " & NroPed64)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroSec64 : " & NroSec64)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strCodRptaBio : " & strCodRptaBio)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strMgsRptaBio : " & strMgsRptaBio)
                            If dsBio Is Nothing Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, " - Codigo respuesta - " & strCodRptaBio)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, " - Mensaje validaciòn : " & strMsgErrorValidarIdentidadPortaPrepago)
                                Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                Return
                            Else
                                If strCodRptaBio.Equals("1") Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, " - Mensaje validaciòn : " & strMsgErrorValidarIdentidadPortaPrepago)
                                    Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                    Return
                                End If
                            End If
                        End If
                        'PROY-32851 :: FIN
                        End If   'INC000002904777 - G.G.A  - SOLUCION - FIN
                    ElseIf strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenovacion")) Then
					'PROY-30182-IDEA-40535-FIN
                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Ini ConsultarVentaRenovPostCAC()---")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FlagRenoPack")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "NroPed" & " : " & Funciones.CheckStr(NroPed64))
                        objVentaRenovacionPack = (New COM_SIC_Cajas.clsCajas).ConsultarVentaRenovPostCAC(Funciones.CheckStr(NroPed64))
                        If Not objVentaRenovacionPack Is Nothing Then
                            Flag_pack_reno = objVentaRenovacionPack.FLAG_CHIP
                        Else
                            Flag_pack_reno = "0"
                        End If
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Exception ConsultarVentaRenovPostCAC()---" & ex.Message)
                    Finally
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin ConsultarVentaRenovPostCAC()---")
                    End Try
                End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Flag_pack_reno" & " : " & Funciones.CheckStr(Flag_pack_reno))
                StrBioExito = "True"


                Dim strTipoOperacionSEC As String = ""

                If sNroSec.Equals("") Or sNroSec.Equals("0") Or sNroSec.Equals("1") Then
                    strTipoOperacionSEC = ""
                Else
                    strTipoOperacionSEC = ConsultaSolicitudTipoOperacion(sNroSec.ToString().Trim())
                End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipoOperacionSEC" & " : " & Funciones.CheckStr(strTipoOperacionSEC))
                If strOffBio = "1" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strOffBio" & " : " & Funciones.CheckStr(strOffBio))
                    If strTipoVenta.Equals(ConfigurationSettings.AppSettings("strTVPostpago")) Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipoVenta" & " : " & Funciones.CheckStr(strTipoVenta))
                            'PROY-25335-Contratacion Electronica R2
                            ' If strTipoOperacionSEC.Equals("2") Then

                            'StrBioExito = "True"

                            ' Else

                        If strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVAlta")) Or strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVReposicionChip")) Or strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenovacion")) Then

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strCodigoTipoOperacionBio" & " : " & Funciones.CheckStr(strCodigoTipoOperacionBio))
                            If sNroSec.Equals("") Or sNroSec.Equals("0") Or sNroSec.Equals("1") Then
                                StrTipoProdSec = "01"
                            Else
                                        StrTipoProdSec = ConsultaSolicitudPospago(Funciones.CheckStr(sNroSec))
                            End If
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "StrTipoProdSec" & " : " & Funciones.CheckStr(StrTipoProdSec))
                               
                                Dim strModalidadVenta As String = "" 'PROY-25335-Contratacion Electronica R2

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente" & " : " & Funciones.CheckStr(strTipDocCliente))
                            If strTipDocCliente.Equals("01") Or strTipDocCliente.Equals("06") Then
                                'VALIDACION INICIAL
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente" & " : " & Funciones.CheckStr(strTipDocCliente))
                                Dim listaRLegal As New ArrayList
                                Dim IntRRLL As New Int32
                                        IntRRLL = 0
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & "Consulta")
                                    'PROY-32851 :: INICIO
                                    Dim dsPdV_Sinergia As DataSet = objConsultaMsSap.ConsultaPuntoVenta(Session("ALMACEN"))
                                    Dim PdV_Sinergia = CStr(dsPdV_Sinergia.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA"))

                                    Dim bln_PuntosVentaAPK As Boolean = False
                                    Dim CodParamPuntosVentaAPK As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ContratacionElectronica_apk"))
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "CodParamPuntosVentaAPK" & " : " & Funciones.CheckStr(CodParamPuntosVentaAPK))
                                    dsParametros = objConsultaPvu.ListaParametrosGrupo(CodParamPuntosVentaAPK)

                                    If dsParametros.Tables(0).Rows.Count > 0 Then
                                        For Each fila As DataRow In dsParametros.Tables(0).Rows
                                            If Funciones.CheckStr(fila(2)).Equals(PdV_Sinergia) Then
                                                bln_PuntosVentaAPK = True
                                                Exit For
                                            End If
                                        Next
                                    End If

                                    If Not (strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVReposicionChip")) And bln_PuntosVentaAPK) Then
                                        dsRRLL = objConsultaPvu.ListaRepresentanteLegal(NroSec64)
                                    End If
                                    'PROY-32851 :: FIN 

                                If Not dsRRLL Is Nothing Then
                                    IntRRLL = dsRRLL.Tables.Count()
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & Convert.ToString(IntRRLL))
                                Else
                                    IntRRLL = 0
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ListaRepresentanteLegal" & "- " & " 0")
                                End If

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRRLL" & " : " & Funciones.CheckStr(IntRRLL))

                                '0 : OK strResultadoCartaPoder
                                        strResultadoCartaPoder = objConsultaPvu.ConsultarSecConCartaPoder(Funciones.CheckStr(sNroSec), Funciones.CheckStr(NroPed64))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))
                                '0 : OK strIncapacidad
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & "Consulta")
                                dsBio = objConsultaPvu.ValidarIdentidad(strTipDocCliente, strNroDocCliente, NroSec64, NroPed64, strCodRptaBio, strMgsRptaBio)
                            'PROY-32851 :: INI
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strNroDocCliente : " & strNroDocCliente)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroPed64 : " & NroPed64)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:NroSec64 : " & NroSec64)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strCodRptaBio : " & strCodRptaBio)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad:strMgsRptaBio : " & strMgsRptaBio)
                                    If ((Not dsBio Is Nothing) AndAlso (dsBio.Tables(0).Rows.Count > 0)) Then

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " dsBio" & "- " & "  Not dsBio Is Nothing")
                            'PROY-32851 :: FIN
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & " 0")
                                            strIncapacidad = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_FLA_INCAPACIDAD"))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                            strRespuestaBioHit = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_RPTAVAL"))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))
                                    dtFechaBio = Funciones.CheckDate(dsBio.Tables(0).Rows(0).Item("BIOM_FECHA_CREA"))

                                    Else
                                        

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " dsBio" & "- " & " dsBio Is Nothing")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad - NroPed64 : " & NroPed64)
                                        dsBio = objConsultaPvu.ValidarIdentidadXSec(NroSec64, strCodRptaBio, strMgsRptaBio)                                      
                                        strRespuestaBioHit = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_RPTAVAL"))
                                        dtFechaBio = Funciones.CheckDate(dsBio.Tables(0).Rows(0).Item("BIOM_FECHA_CREA"))
                                        strBioTipoValidacion = Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_TIPOVALIDACION"))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " strRespuestaBioHit" & "- " & strRespuestaBioHit)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " dtFechaBio" & "- " & Funciones.CheckStr(dtFechaBio))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, " strBioTipoValidacion" & "- " & strBioTipoValidacion)


                                          If strRespuestaBioHit = "0" And strBioTipoValidacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("BioTipoValidacionNoReniec")) Then
                                            strIncapacidad = "0" '  SI strIncapacidad = "0" ENTONCES TIENE DISCAPACIDAD
                                Else
                                    strIncapacidad = "1"
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                    strRespuestaBioHit = "1"
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))
                                    dtFechaBio = DateTime.Now
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidarIdentidad" & "- " & "  Vacio")
                                End If

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " - " & Funciones.CheckStr(strIncapacidad))
                                

                                    End If

                                        If Funciones.CheckStr(strIncapacidad) = "" Then
                                            strIncapacidad = "1"
                                        End If
                                        If Funciones.CheckStr(strRespuestaBioHit) = "" Then
                                            strRespuestaBioHit = "1"
                                        End If
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))

                                StrValidacionInicialExito = "False"

                                If strTipDocCliente.Equals("01") Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipDocCliente" & " : " & Funciones.CheckStr(strTipDocCliente))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRRLL" & " : " & Funciones.CheckStr(IntRRLL))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                    If IntRRLL = 0 And strResultadoCartaPoder.Equals("1") And strIncapacidad.Equals("1") Then
                                        StrValidacionInicialExito = "True"
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                    Else
                                        StrValidacionInicialExito = "False"
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                    End If
                                Else
                                    If strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVReposicionChip")) Or strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenovacion")) Then
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strCodigoTipoOperacionBio" & " : " & Funciones.CheckStr(strCodigoTipoOperacionBio))
                                        If strResultadoCartaPoder.Equals("1") And strIncapacidad.Equals("1") Then
                                            StrValidacionInicialExito = "True"
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                        Else
                                            StrValidacionInicialExito = "False"
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                        End If
                                    Else
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "IntRRLL" & " : " & Funciones.CheckStr(IntRRLL))
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strResultadoCartaPoder" & " : " & Funciones.CheckStr(strResultadoCartaPoder))
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "strIncapacidad" & " : " & Funciones.CheckStr(strIncapacidad))
                                        If IntRRLL = 1 And strResultadoCartaPoder.Equals("1") And strIncapacidad.Equals("1") Then
                                            StrValidacionInicialExito = "True"
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                        Else
                                            StrValidacionInicialExito = "False"
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                                        End If

                                    End If


                                End If

                                'VALIDACION INICIAL
                            Else
                                StrValidacionInicialExito = "False"
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                            End If

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrValidacionInicialExito" & " : " & Funciones.CheckStr(StrValidacionInicialExito))
                            If StrValidacionInicialExito.Equals("True") Then


                                'Buscar Codigo Configuracion.
                                        Dim CodParam As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("ConstKeyBiometria"))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "CodParam" & " : " & Funciones.CheckStr(CodParam))
                                dsParametros = objConsultaPvu.ListaParametrosGrupo(CodParam)
                                If dsParametros.Tables(0).Rows.Count > 0 Then

                                    For Each fila As DataRow In dsParametros.Tables(0).Rows
                                        Select Case fila(3)
                                            Case "2"
                                                        strMensNoHit = Funciones.CheckStr(fila(2))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strMensNoHit" & " : " & Funciones.CheckStr("OK"))
                                            Case "3"
                                                        strMensFecha = Funciones.CheckStr(fila(2))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strMensFecha" & " : " & Funciones.CheckStr("OK"))
                                            Case "4"
                                                        strNumHoras = Funciones.CheckInt(fila(2))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strNumHoras" & " : " & Funciones.CheckStr(strNumHoras))
                                                Exit For
                                        End Select
                                    Next
                                End If

                                'Buscar Codigo Configuracion Reno Pack
                                        Dim CodParamRenoPack As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("Key_ContratacionElectronica"))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "CodParamRenoPack" & " : " & Funciones.CheckStr(CodParamRenoPack))
                                dsParametros = objConsultaPvu.ListaParametrosGrupo(CodParamRenoPack)
                                If dsParametros.Tables(0).Rows.Count > 0 Then

                                    For Each fila As DataRow In dsParametros.Tables(0).Rows
                                        Select Case fila(3)
                                            Case "Key_RenovacionPermitida"
                                                        str_KeyRenovacionPermitida = Funciones.CheckStr(fila(2))
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "str_KeyRenovacionPermitida" & " : " & Funciones.CheckStr(str_KeyRenovacionPermitida))
                                                'PROY-25335-Contratacion Electronica R2 INICIO
                                                Case "Key_ModalidadMigracion"
                                                    str_Key_ModalidadMigracion = Funciones.CheckStr(fila(2))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "str_Key_ModalidadMigracion" & " : " & Funciones.CheckStr(str_Key_ModalidadMigracion))
                                                Case "Key_ProductoPermitido"
                                                    str_Key_ProductoPermitido = Funciones.CheckStr(fila(2))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "str_Key_ProductoPermitido" & " : " & Funciones.CheckStr(str_Key_ProductoPermitido))
                                                Case "Key_TipoOperacionPermitida"
                                                    str_Key_ProductoPermitido = Funciones.CheckStr(fila(2))
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "str_Key_TipoOperacionPermitida" & " : " & Funciones.CheckStr(str_Key_TipoOperacionPermitida))
                                                'PROY-25335-Contratacion Electronica R2 FIN
                                                Exit For
                                        End Select
                                    Next
                                End If


                                Dim dtFechaDocumento As DateTime = dtFechaBio ' dsPedido.Tables(0).Rows(0).Item("BIOM_FECHA_CREA")
                                Dim dFechaActual As DateTime = DateTime.Now
                                dFechaActual = DateTime.Now.AddHours(-strNumHoras)


                                    'PROY-25335-Contratacion Electronica R2 INICIO
                                    Dim ValidaTipoProductoOK As Boolean = True

                                    If strTipoOperacionSEC.Equals("2") Then

                                        If sNroSec.Equals("") Or sNroSec.Equals("0") Or sNroSec.Equals("1") Then
                                            strModalidadVenta = "-1"
                                        Else
                                            strModalidadVenta = ConsultaSolicitudModalidadVenta(sNroSec.ToString().Trim())
                                        End If
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "strModalidadVenta" & " : " & Funciones.CheckStr(strModalidadVenta))

                                        If (str_Key_ModalidadMigracion.IndexOf(Funciones.CheckStr(strModalidadVenta)) > -1) Then
                                            If (str_Key_ProductoPermitido.IndexOf(Funciones.CheckStr(StrTipoProdSec)) > -1) Then
                                                ValidaTipoProductoOK = True
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("True"))
                                            Else
                                                ValidaTipoProductoOK = False
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("False"))
                                            End If
                                        Else
                                            ValidaTipoProductoOK = False
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("False"))
                                        End If

                                    Else
                                        If (str_Key_TipoOperacionPermitida.IndexOf(Funciones.CheckStr(strCodigoTipoOperacionBio)) > -1) Then
                                            If (str_Key_ProductoPermitido.IndexOf(Funciones.CheckStr(StrTipoProdSec)) > -1) Then
                                                ValidaTipoProductoOK = True
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("True"))
                                            Else
                                                ValidaTipoProductoOK = False
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("False"))
                                            End If
                                        Else
                                       'PROY-25335-Contratacion Electronica R2 FIN
                                If StrTipoProdSec.Equals("01") Or StrTipoProdSec.Equals("04") Then
                                                'PROY-25335-Contratacion Electronica R2 INICIO
                                                ValidaTipoProductoOK = True
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("True"))
                                            Else
                                                ValidaTipoProductoOK = False
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, "ValidaTipoProductoOK" & " : " & Funciones.CheckStr("False"))
                                            End If
                                        End If
                                    End If


                                    If ValidaTipoProductoOK Then 'PROY-25335-Contratacion Electronica R2 FIN
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "StrTipoProdSec" & " : " & Funciones.CheckStr(StrTipoProdSec))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciando Validacion de Tipo Operacion y Tipo Documento...") 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor strTipDocCliente:" & strTipDocCliente) 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor strCodigoTipoOperacionBio:" & strCodigoTipoOperacionBio) 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor strNroDocCliente:" & strNroDocCliente) 'FALLA.INC000001412166.SICAR JVERASTEGUI

                                        If Not (strTipDocCliente.Equals("06") AndAlso strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenovacion"))) Then 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "No cumple la validacion strTipDocCliente:" & strTipDocCliente & ", strNroDocCliente:" & strNroDocCliente & ", strCodigoTipoOperacionBio:" & strCodigoTipoOperacionBio) 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                        '<33062>
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuestaBioHit" & " : " & Funciones.CheckStr(strRespuestaBioHit))
                                            If strRespuestaBioHit.Equals("0") Then
                                                If dtFechaDocumento <= dFechaActual Then
                                                    Response.Write("<script>alert('" & strMensFecha & "');</script>")
                                                    Exit Sub
                                                Else
                                                    StrBioExito = "True"
                                                End If
                                            Else
                                                StrBioExito = "False"
                                            End If
                                        '</33062>                                    
                                        Else 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Si cumple la validacion strTipDocCliente:" & strTipDocCliente & ", strNroDocCliente:" & strNroDocCliente & ", strCodigoTipoOperacionBio:" & strCodigoTipoOperacionBio) 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                            StrBioExito = "True" 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                        End If 'FALLA.INC000001412166.SICAR JVERASTEGUI
                                            'FSD
                                            If (strTipDocCliente.Equals("06") AndAlso strCodigoTipoOperacionBio.Equals(ConfigurationSettings.AppSettings("strDTVRenovacion"))) Then
                                                dsRRLL = objConsultaPvu.ListaRepresentanteLegal(NroSec64)
                                                Dim i As Integer
                                                Dim strMsgErrorValidarIdentidadPortaPrepago As String = String.Empty
                                                Dim IntRRLL = dsRRLL.Tables.Count()
                                                Dim dsParamGrupo As DataSet
                                                dsParamGrupo = objGrupoParam.ObtenerParamByGrupo(CodGrupoParamPortaPrepagoTV)
                                                For i = 0 To dsParamGrupo.Tables(0).Rows.Count - 1
                                                    If dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "PDV_TV" Then

                                                    ElseIf dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR1") = "2" Then
                                                        strMsgErrorValidarIdentidadPortaPrepago = dsParamGrupo.Tables(0).Rows(i).Item("PARAV_VALOR")
                                                    End If
                                                Next

                                                If (IntRRLL > 0) Then
                                                    Dim strTipDocRrll As String = dsRRLL.Tables(0).Rows(0).Item("SRLV_TIPO_DOCUMENTO_RL")
                                                    Dim strNroDocRrll As String = dsRRLL.Tables(0).Rows(0).Item("SRLV_NRO_DOCUMENTO_RL")
                                                    If Not (strRespuestaBioHit.Equals("0")) AndAlso strTipDocRrll.Equals("01") Then
                                                        Response.Write("<script>alert('" & strMsgErrorValidarIdentidadPortaPrepago & "');</script>")
                                                        Exit Sub
                                                    End If
                                                End If
                                            End If
                                        Else
                                            StrBioExito = "True"
                                        End If

                                Else
                                    StrBioExito = "True"
                                End If
                            End If
                        End If
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "StrBioExito" & " : " & Funciones.CheckStr(StrBioExito))

                        'PROY-140715 - INICIO
                        Dim isVentaCtg As Boolean = Session("isVentaCtg")
                        Dim isVentaCtgUno As Boolean = Session("isVentaCtgUno")
                        If (isVentaCtg = True Or isVentaCtgUno = True) Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "La venta se realizo por contingencia")
                        Else
                If StrBioExito.Equals("False") Then
                    Response.Write("<script>alert('" & strMensNoHit & "');</script>")
                    Exit Sub
                End If
                        End If
                        'PROY-140715 - FIN


                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error validacion Biometria PostPago ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
                End Try
                'VALIDACION BIOMETRICA PROY-25335-0 - Oscar Atencio Timana

                End If 'PROY-140715 - IDEA 140805 | FIN



                'PROY-140590 IDEA142068 - INICIO
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "INICIO PROY-140590 VALIDACION POOL PAGOS"))

                Dim Key_Flag As String = Funciones.CheckStr(clsKeyAPP.Key_FlagValidacCampana())
                Dim MsgPagoCampania As String = Funciones.CheckStr(clsKeyAPP.Key_MsgPagoCampania())
                Session("descBanco") = Nothing
                Session("descCampana") = Nothing
                Session("strmensajeCampana") = Nothing

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_FlagValidacCampana", Key_Flag))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_MsgPagoCampania", MsgPagoCampania))

                If Key_Flag = "1" Then
                    Dim strPedido As String = Funciones.CheckStr(txtRbPagos.Value)
                    Dim descBanco As String = String.Empty
                    Dim descCampana As String = String.Empty
                    Dim rpta As Boolean = False

                    rpta = ValidarCampaniaSTBK(strPedido, descBanco, descCampana)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "rpta", rpta))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "descBanco", descBanco))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "descCampana", descCampana))

                    If (rpta) Then
                        Dim strmensajeCampana As String = String.Format(MsgPagoCampania, descBanco, descBanco)
                        Session("descBanco") = Funciones.CheckStr(descBanco)
                        Session("descCampana") = Funciones.CheckStr(descCampana)
                        Session("strmensajeCampana") = Funciones.CheckStr(strmensajeCampana)
                    End If
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "FIN PROY-140590 VALIDACION POOL PAGOS"))
                'PROY-140590 IDEA142068 - FIN


                'INICIATIVA 712 Cobro Anticipado INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - INICIO")
                Try
                    Dim MensajePA As String
                    If ConsultaPA(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"), dsPedido.Tables(1).Rows(0).Item("DEPEC_CODMATERIAL"), dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE"), MensajePA) Then
                        Response.Write("<script>alert('" & MensajePA & "');</script>")
                        Exit Sub
                    End If
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - Error btnPagar: " & ex.Message)
                End Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - FIN")
                'INICIATIVA 712 Cobro Anticipado FIN

                Dim strRedirect As String = "Detapago.aspx?pDocSap=" & txtRbPagos.Value & "&numeroTelefono=" & drFila.Item(31) & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO") & "&docSunat=" & drFila.Item(10) & "&montoCuota=" & drFila.Item("PAGON_INICIAL") 'PROY-32089

                If drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    Response.Redirect("Detapago_R.aspx?pDocSap=" & txtRbPagos.Value & "&numeroTelefono=" & drFila("CLIEV_TELEFONOCLIENTE") & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO"), False)
                Else

                        Response.Redirect("Detapago.aspx?pDocSap=" & txtRbPagos.Value & "&numeroTelefono=" & drFila.Item(31) & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO") & "&docSunat=" & drFila.Item(10) & "&montoCuota=" & drFila.Item("PAGON_INICIAL"), False) 'PROY-32089
                    End If
                End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error en el Filtro del documento a Pagar")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        End Try
    End Sub

    Private Sub btnAnular_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'MODIFICADO POR FFS INICIO
        'Dim dvPagos As New DataView(ds.Tables(0))
        Dim dvPagos As New DataView(dtsap)
        'MODIFICADO POR FFS FIN

        If Len(Trim(Request.Item("rbPagos"))) > 0 And hidVerif.Value = "1" Then
            dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
            drFila = dvPagos.Item(0).Row
            Session("DocSel") = drFila
            ' If drFila.Item("FKART") = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA") Then
            If drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA") Then
                'Response.Redirect("depGaran.aspx")
            End If
        End If
    End Sub

    Private Sub cmdAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.Click
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - CMDANULAR INICIO - SESSION USUARIO" & Funciones.CheckStr(Session("USUARIO")))
        '****** PROY 26210 ADD EGSC
        Dim drReport As DataRow 'Clona la fila del drFila para usar en la tipificacion
        '***************


        '******VARIABLES
        Dim usuario_id As String = CurrentUser
        Dim objClsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objClsTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
        '******WEB SERVICE DE ANULACIONES
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS

        Dim strTipoTienda As String = Session("CANAL")
        Dim strCodUsuario As String = Session("USUARIO")
        Dim strCodOficina As String = Session("ALMACEN")
        Dim strCodImprTicket As String = Session("CodImprTicket")
        Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
        '*******************************
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        'Dim dvPagos As New DataView(ds.Tables(0))
        Dim dvPagos As New DataView(dtsap)
        Dim dsReturn As DataSet
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim intAutoriza As Integer
        Dim drFila2 As DataRow
        Dim i As Integer
        Dim dsPedido As DataSet
        Dim dsVendedor As DataSet
        Dim strIdVendedor As String
        Dim strNomVendedor As String

        'PROY-24724-IDEA-28174 - INI
        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
        Dim strNroPedidoEquipo As String = String.Empty
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        Dim blnProteccionMovilPendiente As Boolean = False
        'PROY-24724-IDEA-28174 - FIN

        'Variables de Auditoria
        Dim wParam1 As Long
        Dim wParam2 As String
        Dim wParam3 As String
        Dim wParam4 As Long
        Dim wParam5 As Integer
        Dim wParam6 As String
        Dim wParam7 As Long
        Dim wParam8 As Long
        Dim wParam9 As Long
        Dim wParam10 As String
        Dim wParam11 As Integer
        Dim Detalle(5, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        'Inicio variables de auditoria
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPag")
        wParam5 = 1
        wParam6 = "Anulación de documentos"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPAnu")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(4, 1) = "Oficina"
        Detalle(4, 2) = Session("ALMACEN")
        Detalle(4, 3) = "oficina"

        Detalle(5, 1) = "Usuario"
        Detalle(5, 2) = Session("USUARIO")
        Detalle(5, 3) = "Usuario"
        'Fin variables de auditoria

        'If dtsap.Rows.Count > 0 Then
        'Else
        '    Response.Write("<script language=jscript> alert('Para poder Anular un Pago se requiere que este procesado'); </script>")
        'End If

        'JLOPETAS - PROY 140589 - INI
        Dim strIdentifyLogDLV As String = Funciones.CheckStr(Funciones.CheckDbl(txtRbPagos.Value)) 'JLOPETAS - PROY 140589 

        Try
            If Funciones.CheckStr(Session("FLAG_PICKING")) = "1" AndAlso Funciones.CheckStr(Session("FLAG_DLV")) = "1" Then

                Dim isFlagAnulacion As String
                Dim isFlagCostoDLV As String
                Dim strCodRespuesta As String
                Dim strMSJRespuesta As String
                Dim strMensajeRetorno As String
                Dim NroPedCostoDlv As String

                objClsTrsMsSap.ConsultaAnulaPedidoDLV(Funciones.CheckDbl(txtRbPagos.Value), NroPedCostoDlv, isFlagAnulacion, isFlagCostoDLV, strCodRespuesta, strMSJRespuesta)

                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - NroPedido =>" & Funciones.CheckStr(txtRbPagos.Value))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - NroPedCostoDlv =>" & Funciones.CheckStr(NroPedCostoDlv))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - isFlagAnulacion =>" & Funciones.CheckStr(isFlagAnulacion))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - isFlagCostoDLV =>" & Funciones.CheckStr(isFlagCostoDLV))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - strCodRespuesta =>" & Funciones.CheckStr(strCodRespuesta))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - strMSJRespuesta =>" & Funciones.CheckStr(strMSJRespuesta))

                If isFlagAnulacion = "1" Then
                    If isFlagCostoDLV = "2" Then
                        strMensajeRetorno = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_Msj_AnuCostdlv).Replace("{1}", Funciones.CheckStr(txtRbPagos.Value))
                        Response.Write("<script>alert('" & strMensajeRetorno & "');</script>")
                    End If

                    If isFlagCostoDLV = "" AndAlso strCodRespuesta = "1" Then
                        strMensajeRetorno = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_Msj_Generico_Anula_DLV).Replace("{1}", Funciones.CheckStr(txtRbPagos.Value)).Replace("{2}", Funciones.CheckStr(NroPedCostoDlv))
                        Response.Write("<script>alert('" & strMensajeRetorno & "');</script>")
                    End If
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "Cobro Delivery - strMensajeRetorno =>" & Funciones.CheckStr(strMensajeRetorno))
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "ERROR - ex.Message" & ex.Message)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogDLV & " - " & "ERROR - ex.StackTrace" & ex.StackTrace)
            Exit Sub
        End Try
        'JLOPETAS - PROY 140589 - FIN

        '****** CONSULTA SI TIENE ACCESORIO CON COSTO*****'----INICIATIVA-1006-TIENDA VIRTUAL-INICIO
        Dim strIdentifyLogACC As String = Funciones.CheckStr(Funciones.CheckDbl(txtRbPagos.Value))
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim intNroPedACC_cos As Int64
        Dim strFlagACC_cos As String
        Dim strEstadoPed_ACC_Cos As String
        Dim mensajeACC As String
        Dim COD_RespuestaACC As String
        Dim MSJ_RespuestaACC As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Inicia consulta si el pedido tiene accesorio con costo]")
        Try
            objConsultaMsSap.validaPedidoAccCosto(Funciones.CheckInt64(txtRbPagos.Value), intNroPedACC_cos, strFlagACC_cos, strEstadoPed_ACC_Cos, COD_RespuestaACC, MSJ_RespuestaACC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "====OUTPUT INI====")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(intNroPedACC_cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strFlagACC_cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(strEstadoPed_ACC_Cos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(COD_RespuestaACC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo]-[OUTPUT]" & Funciones.CheckStr(MSJ_RespuestaACC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "====OUTPUT FIN====")

            If strEstadoPed_ACC_Cos = "ACT" Then
                mensajeACC = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MsjAccAnulacion).Replace("{0}", Funciones.CheckStr(txtRbPagos.Value)).Replace("{1}", Funciones.CheckStr(intNroPedACC_cos))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "- " & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo] " & "- " & "STR_MENSAJE => " & Funciones.CheckStr(mensajeACC))
                Response.Write("<script>alert('" & mensajeACC & "');</script>")
                Exit Sub
            End If
            If strEstadoPed_ACC_Cos = "PAG" Then
                mensajeACC = Funciones.CheckStr(COM_SIC_Seguridad.ReadKeySettings.Key_MsjAccPagado).Replace("{0}", Funciones.CheckStr(txtRbPagos.Value)).Replace("{1}", Funciones.CheckStr(intNroPedACC_cos))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "- " & "[INICIATIVA-1006 | Tienda Virtual - Accesorio con Costo] " & "- " & "STR_MENSAJE => " & Funciones.CheckStr(mensajeACC))
                Response.Write("<script>alert('" & mensajeACC & "');</script>")
                Exit Sub
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "- " & "Error al consultar si el pedido tiene accesorio con costo")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLogACC & "[INICIATIVA-1006 | Tienda Virtual - Fin de consulta si el pedido tiene accesorio con costo]")
        End Try
        '****** CONSULTA SI TIENE ACCESORIO CON COSTO*****'----INICIATIVA-1006-TIENDA VIRTUAL-FIN

        Try
            Dim filter As String = String.Empty
            If (txtsession.Value = 1) Then
                filter = "ID_T_TRS_PEDIDO=" & txtRbPagos.Value
            Else
                'filter = "VBELN=" & Request.Item("rbPagos")
                filter = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
            End If

            dvPagos.RowFilter = filter
            drFila = dvPagos.Item(0).Row
            drReport = dvPagos.Item(0).Row 'PROY 26210 EGSC CUSTOM ADD

            '****SOLUCION SALTO CORRELATIVO
            Dim dsNumCorr As DataSet
            Dim CorrelativoSunat As String
            Dim objCajass As New COM_SIC_Cajas.clsCajas
            Dim SerieSunat As String
            Dim strNumSunat As String
            Dim tipo As String
            Dim corrModulo As String
            Dim referencia_asociada As String = ""
            Dim arrayBusqueda As String()
            '**PARA LOGS
            Dim strIdentifyLog As String = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "----------------------------------------------------")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "            INICIO ANULACION DE PEDIDO              ")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "----------------------------------------------------")

            'INICIATIVA-710 - INICIO
            Try
                Dim strPedido As String = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
                Dim strCodMsg As String = ""
                Dim strDesMsg As String = ""

                ValidaAnulacionCombo(strPedido, strCodMsg, strDesMsg)
                If strCodMsg = "1" Then
                    objLog.Log_WriteLog(pathFile, strArchivo, strDesMsg)
                    Response.Write("<script>alert('" & Funciones.CheckStr(strDesMsg) & "');</script>")
                    Exit Sub
                End If
            Catch ex As Exception
                objLog.Log_WriteLog(pathFile, strArchivo, " - Exception - " & "Error Message. " & ex.StackTrace)
            End Try
            'INICIATIVA-710 - FIN

            '***** INI: Valida Asignacion y Cierre de caja - TS-CCC *****
            Dim dsResultado As DataSet
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim strCierreCajaMensaje As String = String.Empty
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            'PROY-140397-MCKINSEY -> JSQ
            Dim sFechaCaja As String = DateTime.Now().ToString("dd/MM/yyyy")
            Try
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Validar Asignacion y caja cerrada")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Oficina : " & Session("ALMACEN"))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Fecha : " & sFechaCaja)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Cajero : " & codUsuario)
                Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, sFechaCaja)
                If asignacionCajeroMensaje <> String.Empty Then
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & asignacionCajeroMensaje)
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
                    Me.RegisterStartupScript("RegistraAlerta", script)
                    Exit Sub
                End If

                'dsResultado = objOffline.GetDatosAsignacionCajero(Session("ALMACEN"), txtFecha.Text, codUsuario)
                'If Not dsResultado Is Nothing Then
                '    For cont As Int32 = 0 To dsResultado.Tables(0).Rows.Count - 1
                '        If dsResultado.Tables(0).Rows(cont).Item("CAJA_CERRADA") = "S" Then
                '            strCierreCajaMensaje = "Caja Cerrada, no es posible anular."
                '            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE : " & strCierreCajaMensaje)
                '            Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                '            Me.RegisterStartupScript("RegistraAlerta", script)
                '            Exit Sub
                '        End If
                '    Next
                'End If
            Catch ex As Exception
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
            Finally
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Validar Asignacion y caja cerrada")
                'dsResultado.Dispose()
                objOffline = Nothing
            End Try
            '***** FIN: Valida Asignacion y Cierre de caja - TS-CCC *****

            Dim PdV_Sinergia As String = Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN")))

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Consultar Pedido - SP: SSAPSS_PEDIDO")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Nro PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Punto de Venta: " & PdV_Sinergia)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Cod Interlocutor: " & "")

            dsPedido = objClsConsultaMsSap.ConsultaPedido(drFila.Item("PEDIN_NROPEDIDO"), PdV_Sinergia, "")

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Consultar Pedido - SP: SSAPSS_PEDIDO")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791")

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|1-" + dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|2-" + ConfigurationSettings.AppSettings("strTVPostpago"))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|3-" + dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|4-" + ConfigurationSettings.AppSettings("consCodTipoOperVentaAlta"))

            'ADD_INI_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO

            Dim flagActivaSimcard As String = ConfigurationSettings.AppSettings("constActivaSimcardsWS")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " flagActivaSimcard: " & flagActivaSimcard)
         
            Dim descOpePortaPost As String = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST")
            Dim bolDesaLineaYCambiarEstSans As Boolean = False


            'G.G.A - INICIO - 11/10/2019
            If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") And _
            dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("consCodTipoOperVentaAlta")Then

                bolDesaLineaYCambiarEstSans = True

                If dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION") = descOpePortaPost Then
                    bolDesaLineaYCambiarEstSans = False
                End If
            End If
            'G.G.A - FIN - 11/10/2019

            If bolDesaLineaYCambiarEstSans Then

                'G.G.A - FIN - 11/10/2019
                Dim strMaterial As String = String.Empty
                Dim strSerie As String = String.Empty
                Dim strLinea As String = String.Empty
                Dim strCodRpt_Des As String = String.Empty
                Dim strCodRpt_Cam As String = String.Empty
                Dim strTipoMaterial As String = String.Empty
                Dim v_TipoOficina As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA")
                Dim StrCodEstado As String = ConfigurationSettings.AppSettings("WSSans_RollbackCambiarStatus")

                'INI: IDEA-150017 IFI VOLTE
                Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu

                Dim strCampanaCod As String
                Dim strTipo_Prod_Cod As String
                Dim bolDesaLineaYCambiarEstSansIFI As Boolean = False
                Dim strCadenaCampanas As String = ConfigurationSettings.AppSettings("constCampanasIfiVolte")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IDEA-150017|[strCadenaCampanas]: " + strCadenaCampanas)

                Dim arrCadenaCampanas As String()
                arrCadenaCampanas = strCadenaCampanas.Split("|"c)

                Dim P_CODIGO_RESPUESTA As String = ""
                Dim P_MENSAJE_RESPUESTA As String = ""
                Dim C_VENTA As DataTable
                Dim C_VENTA_DET As DataTable
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|IDEA-150017|Metodo:ConsultarPedidosPVU")

                objClsConsultaPvu.ConsultarPedidosPVU(drFila.Item("PEDIN_NROPEDIDO"), _
                                                                  P_CODIGO_RESPUESTA, _
                                                                  P_MENSAJE_RESPUESTA, _
                                                                  C_VENTA, _
                                                                  C_VENTA_DET)

                strTipo_Prod_Cod = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("PRDC_CODIGO"))
                strCampanaCod = Funciones.CheckStr(C_VENTA_DET.Rows(0).Item("CAMPANA"))

                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IDEA-150017|[strTipo_Prod_Cod]: " + strTipo_Prod_Cod)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IDEA-150017|[strCampanaCod]: " + strCampanaCod)

                For y As Integer = 0 To arrCadenaCampanas.Length - 1
                    If (strCampanaCod = arrCadenaCampanas(y)) Then
                        bolDesaLineaYCambiarEstSansIFI = True
                        Exit For
                    End If
                Next
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IDEA-150017|[bolDesaLineaYCambiarEstSansIFI]: " + Funciones.CheckStr(bolDesaLineaYCambiarEstSansIFI))
                If (strTipo_Prod_Cod.Equals("06")) Then
                    If bolDesaLineaYCambiarEstSansIFI Then
                        StrCodEstado = ConfigurationSettings.AppSettings("constEstDispSansIfiVolte")
                    End If
                End If
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IDEA-150017|[StrCodEstado]: " + StrCodEstado)
                'FIN: IDEA-150017

                If (v_TipoOficina = "01" Or v_TipoOficina = "02" Or v_TipoOficina = "03") Then
                    Dim j As Integer = 0
                    For j = 0 To dsPedido.Tables(1).Rows.Count - 1

                        strMaterial = Funciones.CheckStr(dsPedido.Tables(1).Rows(j).Item("DEPEC_CODMATERIAL"))
                        strSerie = Funciones.CheckStr(dsPedido.Tables(1).Rows(j).Item("SERIC_CODSERIE"))
                        strLinea = Funciones.CheckStr(dsPedido.Tables(1).Rows(j).Item("DEPEV_NROTELEFONO"))
                        strTipoMaterial = Funciones.CheckStr(dsPedido.Tables(1).Rows(j).Item("MATEC_TIPOMATERIAL"))

                        If strSerie <> String.Empty Then
                            If strTipoMaterial = ConfigurationSettings.AppSettings("ConsTipoMaterialesCHIP") Then
                            ' Invocamos el método DesasociacionLinea
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|Método:DesasociacionLinea")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|[INPUT].[Línea]: " + strLinea)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|[INPUT].[Serie]: " + strSerie)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|[INPUT].[Material]: " + strMaterial)
                            strCodRpt_Des = DesasociacionLinea("-", strLinea, strSerie)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN|PROY-24388 - IDEA-31791|Método:DesasociacionLinea")

                            'invocamos el método cambiarStatus
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|Método:cambiarStatus")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|[INPUT].[Línea]: " + strLinea)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO|PROY-24388 - IDEA-31791|[INPUT].[CodEstado]: " + StrCodEstado)
                            strCodRpt_Cam = CambiarEstadoSans("-", strLinea, StrCodEstado)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN|PROY-24388 - IDEA-31791|Método:cambiarStatus")
                        End If
                        End If
                    Next

                End If

            End If
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN|PROY-24388 - IDEA-31791")
            'FIN_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO

            Dim strNumSerieDoc As String = ""
            Dim CONS_VALIDA_DOC_TICKET As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_TICKET")
            Dim CONS_VALIDA_DOC_BOLETA As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_BOLETA")
            Dim CONS_VALIDA_DOC_NOTA_CREDITO As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_NOTA_CREDITO")
            Dim CONS_VALIDA_DOC_FACTURA As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_FACTURA")


            If InStr(CStr(drFila("PAGOC_CODSUNAT")), "-") > 0 Then
                strNumSerieDoc = CStr(drFila("PAGOC_CODSUNAT")).Substring(0, InStr(CStr(drFila("PAGOC_CODSUNAT")), "-") - 1)
            End If 'TODOEB SE COMENTO DEVIDO A QUE EL CAMPO DE SUNAT AUN SE ENCUENTRA BACIO

            If strNumSerieDoc <> "" AndAlso (strNumSerieDoc.Equals(CONS_VALIDA_DOC_TICKET) OrElse _
            strNumSerieDoc.Equals(CONS_VALIDA_DOC_BOLETA) OrElse _
            strNumSerieDoc.Equals(CONS_VALIDA_DOC_NOTA_CREDITO) OrElse _
            strNumSerieDoc.Equals(CONS_VALIDA_DOC_FACTURA)) Then
                Response.Write("<script>alert('El documento no se puede Anular.'); </script>")
                Exit Sub
            End If

            ''' FIN ALINEAMIENTO
            Dim isRecargaVirtual As Boolean = (txtsession.Value = 1)

            If Len(Trim(strIdVendedor)) > 0 Then
                dsVendedor = objPagos.Get_ConsultaVend(strIdVendedor)
                If Not IsNothing(dsVendedor) Then
                    strNomVendedor = dsVendedor.Tables(0).Rows(0).Item("NOMBRE")
                Else
                    strNomVendedor = ""
                End If
            Else
                strNomVendedor = ""
            End If
            'CARIAS: Fin de bloque
            'End If

            '********************************************desde aqui

            If Valida_Anula_Prom(drFila("PEDIN_NROPEDIDO")) > 0 Then         '**Linea que puede ser la correcta ***'
                Response.Write("<script language=jscript> alert('" & ConfigurationSettings.AppSettings("contMsgPromPostModemPreAnula").ToString() & "'); </script>")
                Exit Sub
            End If  'TODOEB COMENTO SEGUN ESPECIFICACION DE YA NO USO.

            intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, ConfigurationSettings.AppSettings("codAplicacion"), strCodUsuario, Session("NOMBRE_COMPLETO"), "", "", _
                         Funciones.CheckStr(drFila("PEDIV_NOMBRECLIENTE")), "", "", drFila("PEDIN_NROPEDIDO"), 0, 4, 0, 0, 0, 0, 0, 0, "", strNomVendedor)

            If Funciones.CheckStr(drFila("PEDIC_CLASEFACTURA")) = "0000" And Funciones.CheckStr(drFila("PAGOC_CODSUNAT")) = "" Then
                intAutoriza = 1
            End If


            If intAutoriza = 1 Then
                If Len(txtRbPagos.Value.Trim) > 0 Then
                    'dvPagos.RowFilter = "VBELN=" & txtRbPagos.Value TODORB
                    dvPagos.RowFilter = "PEDIN_NROPEDIDO=" & txtRbPagos.Value
                    'dvPagos.RowFilter = filter
                    drFila = dvPagos.Item(0).Row
                    Dim obAnular As New clsAnulaciones

                    If CStr(drFila("PEDIC_CLASEFACTURA")) = cteTIPODOC_DEPOSITOGARANTIA AndAlso CStr(drFila("NRO_DEP_GARANTIA")).Trim().Length > 0 AndAlso CLng(CStr(drFila("NRO_DEP_GARANTIA"))) > 0 Then
                        obAnular.AnularDepGaran(CStr(drFila("NRO_DEP_GARANTIA")), CStr(drFila("NRO_REF_DEP_GAR")), strCodUsuario)


                        'Anular SOT DTH
                        Dim strDocSapDTH As String = CStr(drFila("PEDIN_NROPEDIDO"))
                        'Dim strIdentifyLog As String = strDocSapDTH
                        strIdentifyLog = strDocSapDTH

                        Dim objCajas As New COM_SIC_Cajas.clsCajas
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Anular SOT DTH")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "inp strDocSapDTH:" & strDocSapDTH)
                        Dim nroContratoDTH, codRespuestaDTH As Integer
                        Dim nroSec, nroDocSapDTH, nroDepSapDTH, mensajeDTH As String
                        Dim estadoAnulado As String = ConfigurationSettings.AppSettings("constEstadoContrato_Anulado")
                        Dim estadoContrato As String
                        If objCajas.validaDocSAPxDTH(strDocSapDTH, nroContratoDTH, nroSec, nroDocSapDTH, nroDepSapDTH, codRespuestaDTH, mensajeDTH) Then
                            If codRespuestaDTH = 0 Then
                                estadoContrato = objCajas.Consulta_estado_contrato(CInt(nroContratoDTH))
                                If estadoContrato <> estadoAnulado Then
                                    Dim nroSot = objCajas.ConsultaNroSot(nroSec)
                                    If AnularSot(nroSot, nroSec, strDocSapDTH) = "0" Then
                                        objCajas.Actualizar_estado_contrato(CheckInt64(nroContratoDTH), estadoAnulado, nroSot)
                                    End If
                                End If
                            End If
                        End If

                        Try

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "estadoContrato:" & estadoContrato)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroContratoDTH:" & nroContratoDTH)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroDocSapDTH:" & nroDocSapDTH)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroDepSapDTH:" & nroDepSapDTH)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "out codRespuestaDTH:" & codRespuestaDTH)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "out mensajeDTH:" & mensajeDTH)
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Anular SOT DTH")

                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR Anular SOT DTH: " & ex.Message.ToString())
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR Anular SOT DTH: " & ex.StackTrace.ToString())
                        End Try


                        'Anulacion de pedido asociado
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            If ds.Tables(0).Rows(i).Item("PEDIN_NROPEDIDO") = drFila("PEDIN_NROPEDIDO") And ds.Tables(0).Rows(i).Item("PEDIC_CLASEFACTURA") <> cteTIPODOC_DEPOSITOGARANTIA Then
                                drFila2 = ds.Tables(0).Rows(i)
                                Exit For
                            End If
                        Next

                        'AUDITORIA
                        Detalle(1, 1) = "Fecha"
                        Detalle(1, 2) = DateTime.Now
                        Detalle(1, 3) = "Fecha"

                        Detalle(2, 1) = "Factura"
                        Detalle(2, 2) = IIf(Len(Trim(CStr(drFila("PEDIN_NROPEDIDO")))) > 0, CStr(drFila("PEDIN_NROPEDIDO")), "(vacio)")
                        Detalle(2, 3) = "Factura"

                        Detalle(3, 1) = "DocSap"
                        Detalle(3, 2) = CStr(drFila("PEDIN_NROPEDIDO"))
                        Detalle(3, 3) = "Documento SAP"

                        'FIN AUDITORIA

                        If Not IsNothing(drFila2) Then

                            obAnular.AnularViasPago(CStr(drFila2("PEDIN_NROPEDIDO")), CStr(drFila2("PEDIC_CLASEFACTURA")), _
                                              "", DateTime.Now, _
                                              CStr(drFila2("PEDIN_NROPEDIDO")), CStr(drFila2("NRO_DEP_GARANTIA")), _
                                              CStr(drFila2("NRO_REF_DEP_GAR")), CStr(drFila2("NRO_CONTRATO")), _
                                              CStr(drFila2("NRO_OPE_INFOCORP")), CStr(drFila2("CODIGO_APROBACIO")), _
                                              strTipoTienda, strCodOficina, strCodImprTicket, strCodUsuario, CDec(drFila2("PAGOS")), , "")

                            'ImprimeAnulado(CStr(drFila2("XBLNR")))
                        End If
                    Else

                        'AUDITORIA
                        Detalle(1, 1) = "Fecha"
                        Detalle(1, 2) = DateTime.Now
                        Detalle(1, 3) = "Fecha"

                        Detalle(2, 1) = "Factura"
                        Detalle(2, 2) = "" 'CStr(drFila("XBLNR"))
                        Detalle(2, 3) = "Factura"

                        Detalle(3, 1) = "DocSap"
                        'Detalle(3, 2) = CStr(drFila("VBELN"))
                        Detalle(3, 2) = CStr(drFila("PEDIN_NROPEDIDO"))
                        Detalle(3, 3) = "Documento SAP"
                        'FIN AUDITORIA

                        '**PARA LOGS
                        strIdentifyLog = Funciones.CheckStr(drFila("PEDIN_NROPEDIDO"))
                        Dim ClaseFactura As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA"))
                        Dim CodTipoOperacion As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
                        Dim PAGOID As String
                        Dim AnuID As DataSet

                        Try
                            PAGOID = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PAGOID: " & Funciones.CheckInt(PAGOID))
                        Catch ex As Exception
                            PAGOID = ""
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "PAGOID Catch: " & Funciones.CheckInt(PAGOID))
                        End Try

                        If (ClaseFactura = ConfigurationSettings.AppSettings("strTipDoc") Or _
                            ClaseFactura = ConfigurationSettings.AppSettings("ClaseNotaCanje")) And PAGOID <> "" Then
                            '***Inicio ANULACION DE PAGO
                            PAGOID = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO"))

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ID DE PAGO: " & PAGOID)

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO Anulacion de pago - SP: SSAPSU_ANULARPAGO")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PAGO: " & Funciones.CheckInt(PAGOID))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))


                            AnuID = objClsTrsMsSap.AnularPago(PAGOID, drFila.Item("PEDIN_NROPEDIDO"))

                            Dim IDAnulacion As String
                            IDAnulacion = Funciones.CheckStr(IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("ANUPN_ID")), "", AnuID.Tables(0).Rows(0).Item("ANUPN_ID")))

                            If ClaseFactura <> ConfigurationSettings.AppSettings("ClaseNotaCanje") Then

                                If Len(Trim(IDAnulacion)) > 0 Then
                                    'cambiar estado flujo 
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID ANULACION: " & IDAnulacion)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN PROCESO: " & ConfigurationSettings.AppSettings("ANULA_PAGO"))
                                    objClsTrsMsSap.ActualizaEstadoFlujo(drFila.Item("PEDIN_NROPEDIDO"), IDAnulacion, ConfigurationSettings.AppSettings("ANULA_PAGO"))
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                                End If
                            End If
                        End If

                        If (ClaseFactura = ConfigurationSettings.AppSettings("strTipDoc")) Or (ClaseFactura = ConfigurationSettings.AppSettings("ClaseNotaCanje")) Then
                            Try
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [INICIO LiberarDevolucion]")

                                Dim K_SERIE As String = Funciones.CheckStr(dsPedido.Tables(1).Rows(0).Item("SERIC_CODSERIE"))
                                Dim codlog, deslog As String
                                Dim objClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu

                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        [IN SERIE: ]" & K_SERIE & "]")
                                objClsTrsPvu.LiberarDevolucion(K_SERIE, codlog, deslog)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        [OUT CODLOG: ]" & codlog & "][OUT DESLOG: " & deslog & "]")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [FIN LiberarDevolucion]")
                            Catch ex As Exception
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [ERROR LiberarDevolucion]")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        [Message de Exception ]" & ex.Message.ToString)
                            End Try
                        End If

                        '***Anula Pedido  

                        'gdelasca Inicio Proy-9067
                        Try

                            Dim vCodMaterialSaliente As String = ""
                            Dim vCodSerieSaliente As String = ""
                            Dim Obs_AnulaPedido As String
                            Obs_AnulaPedido = ConfigurationSettings.AppSettings("ObsAnulaPediRenoAnti").ToString()
                            Dim objPvu As New COM_SIC_Activaciones.clsTrsPvu
                            Dim vCodRsptActualiza As Integer
                            Dim vMsjRsptaActualiza As String = ""
                            Dim v_Obtenido As DataSet

                            Dim vCursorxIdPedidoActual As Object
                            Dim vCodRpta As String
                            Dim vMsjRpta As String
                            Dim DsCanjeEquipo As DataSet

                            DsCanjeEquipo = objPvu.ConsultaEstRenoAnticipada(strIdentifyLog, vCursorxIdPedidoActual, vCodRpta, vMsjRpta)

                            If Not IsNothing(DsCanjeEquipo) AndAlso DsCanjeEquipo.Tables(0).Rows.Count > 0 Then

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--INICIO MÉTODO ActualizarRenoAnticipada()--")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp Nº PEDIDO:" & strIdentifyLog)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ESTADO :" & ConfigurationSettings.AppSettings("EstRenoAntiAnulPedido").ToString())
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp COD MATERIAL:" & vCodMaterialSaliente)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp COD SERIE:" & vCodSerieSaliente)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp USU MODIFI:" & CurrentUser)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp OBSERVACION:" & Obs_AnulaPedido)

                                v_Obtenido = objPvu.ActualizarRenoAnticipada(strIdentifyLog, ConfigurationSettings.AppSettings("EstRenoAntiAnulPedido").ToString(), vCodSerieSaliente, vCodMaterialSaliente, CurrentUser, Obs_AnulaPedido, vCodRsptActualiza, vMsjRsptaActualiza)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--FIN Metodo ActualizarRenoAnticipada()--")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out COD RESPUESTA:" & vCodRsptActualiza)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out MSJ RESPUESTA:" & vMsjRsptaActualiza)

                            End If

                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR : " & ex.Message.ToString())
                        End Try

                        'gdelasca FIN Proy-9067

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Nro Pedido: " & drFila("PEDIN_NROPEDIDO"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Motivo: " & ConfigurationSettings.AppSettings("Motivo_Anulacion"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Estado: " & ConfigurationSettings.AppSettings("Estado_Anulacion"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Tipo de Oficina: " & dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA"))

                        'INI FALLA INC000002644707
                        Try

                            Dim contador As Integer = 0
                            Dim PedidoVenta As Integer
                            Dim dsPedidoVenta As DataSet
                            Dim estadoPedidoVenta As String
                            Dim arrCadena As String()
                            Dim objMssap As New COM_SIC_Activaciones.clsConsultaMsSap

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    drFila.Item(PEDIC_CLASEFACTURA): " & Funciones.CheckStr(drFila.Item("PEDIC_CLASEFACTURA")))

                            If drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then                                
                                PedidoVenta = dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOALTA")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    PedidoVenta: " & Funciones.CheckStr(PedidoVenta))

                                dsPedidoVenta = objMssap.ConsultaPedido(PedidoVenta, "", "")
                                estadoPedidoVenta = dsPedidoVenta.Tables(0).Rows(0).Item("PEDIC_ESTADO")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    estadoPedidoVenta: " & Funciones.CheckStr(estadoPedidoVenta))

                                Dim Key_EstadosNoPermitidosAnularRenta As String = Funciones.CheckStr(clsKeyAPP.Key_EstadosNoPermitidosAnular())
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    Key_EstadosNoPermitidosAnular: " & Key_EstadosNoPermitidosAnularRenta)

                                arrCadena = Key_EstadosNoPermitidosAnularRenta.Split("|"c)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    arrCadena: " & Funciones.CheckStr(arrCadena))
                                For y As Integer = 0 To arrCadena.Length - 1
                                    If (estadoPedidoVenta = arrCadena(y)) Then
                                        contador = +1
                                    End If
                                Next
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    contador: " & Funciones.CheckStr(contador))

                                Dim Key_MensajeNoPermitidosAnularRenta As String = Funciones.CheckStr(clsKeyAPP.Key_MensajeNoPermitidosAnular())
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    Key_MensajeNoPermitidosAnular: " & Key_MensajeNoPermitidosAnularRenta)

                                If contador <> 0 Then
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " El pedido de la venta se encuentra pagado o pendiente de pago: " & PedidoVenta)
                                    Response.Write("<script>alert('" & Key_MensajeNoPermitidosAnularRenta.ToString() & "');</script>")
                                    Exit Sub
                                End If

                            End If
                            'End If

                        objClsTrsMsSap.AnularPedido(drFila("PEDIN_NROPEDIDO"), ConfigurationSettings.AppSettings("Motivo_Anulacion"), ConfigurationSettings.AppSettings("Estado_Anulacion"), dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA"))

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Anular Pedido - SP: SSAPSU_ANULARPEDIDO")
                        '**Fin Anulacion Pedido

                        Catch ex As Exception 'INC000002644707
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE ERROR ANULAR PEDIDO : " & ex.Message.ToString()) 'INC000002644707
                        End Try 'INC000002644707
                        'FIN FALLA INC000002644707

                        '**Inicio Anula Canje
                        If ClaseFactura.Equals(ConfigurationSettings.AppSettings("ClaseNotaCanje")) OrElse _
                            ClaseFactura.Equals(ConfigurationSettings.AppSettings("strTipDoc")) OrElse _
                            CodTipoOperacion.Equals(ConfigurationSettings.AppSettings("ClaseBolFactCanje")) Then

                            Dim blnAnularCanje As Boolean = AnulaCanjeMSSAP(drFila("PEDIN_NROPEDIDO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Anulacion Registro de Canje: " & blnAnularCanje)

                        End If
                        '**Fin Anula Canje

                        'PROY-23111-IDEA-29841 - INICIO
                        Dim strCodOperacion As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Codigo de Operacion: " & strCodOperacion)
                        If strCodOperacion.Equals(ConfigurationSettings.AppSettings("COD_TIPO_OPERACION_VENTA_VARIOS")) Then
                            AnularVentaAccesorios(drFila("PEDIN_NROPEDIDO"), CurrentUser)
                        End If
                        'PROY-23111-IDEA-29841 - FIN


                        'PROY-24724-IDEA-28174 - INI
                        Dim strNroPedidoPM As String = Funciones.CheckStr(drFila("PEDIN_NROPEDIDO"))
                        If strCodOperacion = clsKeyAPP.strClaseBolFactCanje Then 
                            AnularPedidoCanjeConProteccionMovil(strNroPedidoPM)
                        End If

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ConsultarDatosPM - INICIO ==")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         SP: PKG_TRANS_ASURION.SISACTSS_CONSULTAR_SEGURO")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN NRO PEDIDO EQUIPO: " & strNroPedidoEquipo)

                        Dim dsProteccionMovil As DataSet = objProteccionMovil.ConsultarProteccionMovil(strNroPedidoPM, strCodRpta, strMsgRpta)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT COD RPTA: " & strCodRpta)
                        'INI PROY-140126
                        Dim MaptPath As String
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_Click)"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT MSG RPTA: " & strMsgRpta & MaptPath)
                        'FIN PROY-140126
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ConsultarDatosPM - FIN    ==")

                        If strCodRpta = "0" AndAlso Not dsProteccionMovil Is Nothing AndAlso dsProteccionMovil.Tables.Count > 0 AndAlso dsProteccionMovil.Tables(0).Rows.Count > 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   B/F con Proteccion Movil")
                            blnProteccionMovilPendiente = True
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Consulta Proteccion Movil - INICIO ==")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     SP: PKG_CONSULTA.SSAPSS_VALIDA_ASURION") 'PROY-24724 - Iteracion 2 Siniestros
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     IN NRO PEDIDO PM: " & strNroPedidoPM)
                            objProteccionMovil.ValidaPagoEquipoProteccionMovil(strNroPedidoPM, strNroPedidoEquipo, strCodRpta, strMsgRpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT Cod Rpta " & strCodRpta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT Msg Rpta " & strMsgRpta)

                            If (strCodRpta.Equals("0") Or strCodRpta.Equals("-1")) AndAlso strNroPedidoEquipo <> "" Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularProteccionMovil :: INI  ==")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")

                                objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strNroPedidoEquipo:" & strNroPedidoEquipo)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SESSION USUARIO:" & Funciones.CheckStr(Session("USUARIO")))
                                AnularProteccionMovil(strNroPedidoPM, strNroPedidoEquipo)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularProteccionMovil :: FIN  ==")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
                            End If
                        End If
                        'PROY-24724-IDEA-28174 - FIN
                        'PROY-24724 - Iteracion 2 Siniestros -INI
                        Dim strCodMaterial As String = Funciones.CheckStr(dsPedido.Tables(1).Rows(0).Item("DEPEC_CODMATERIAL"))
                        Dim strTelefono As String = Right(Funciones.CheckStr(dsPedido.Tables(1).Rows(0).Item("DEPEV_NROTELEFONO")), 9)

                        If (strCodMaterial.Equals(clsKeyAPP.strCodMaterialSiniestro)) Then
                            AnularPedidoSiniestro(strTelefono)
                        End If
                        'PROY-24724 - Iteracion 2 Siniestros -FIN

                        '**Inicio Cambio de Estado Flujo
                        If ClaseFactura <> ConfigurationSettings.AppSettings("ClaseNotaCanje") Then
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Cambiar Estado Flujo - SP: SSAPSU_ESTADOFLUJODEVOLUCION")
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Nro Pedido: " & drFila("PEDIN_NROPEDIDO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN ID Anulacion: " & drFila("PEDIN_NROPEDIDO"))
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Proceso(tipo de Anulacion): " & ConfigurationSettings.AppSettings("ANULA_PEDIDO"))

                            objClsTrsMsSap.ActualizaEstadoFlujo(drFila("PEDIN_NROPEDIDO"), drFila("PEDIN_NROPEDIDO"), ConfigurationSettings.AppSettings("ANULA_PEDIDO"))

                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Cambiar Estado Flujo - SP: SSAPSU_ESTADOFLUJODEVOLUCION")
                        End If
                        '******INICO WEB SERVICE DE ANULACION


                        'Dim ClaseFactura As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")
                        Dim Sociedad As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_ORGVENTA")
                        Dim CanalDistribucion As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_CANALVENTA")
                        Dim Sector As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_SECTOR")
                        Dim DocuSAP As String

                        DocuSAP = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROFACTURA")) 'Funciones.CheckStr(IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOSAP")), "", dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOSAP")))

                        '*Variables parde Retorno del WS
                        Dim K_COD_RESPUESTA As String
                        Dim K_MSJ_RESPUESTA As String
                        Dim K_ID_TRANSACCION As String

                        Dim origen As String = "3"
                        Dim Estado_Anulado As String = ConfigurationSettings.AppSettings("FE_Estado_Anulado_Sicar")
                        '************** Inicio de Flujo para Guardar el correlativo como reciclaje *****************
                        dsNumCorr = objCajass.ConsultaDocPagos(drFila("PEDIN_NROPEDIDO"), origen)
                        If dsNumCorr.Tables.Count > 0 Then
                            If dsNumCorr.Tables(0).Rows.Count > 0 AndAlso IsDBNull(dsNumCorr.Tables(0).Rows(0)("flag_paperless")) Then
                                'SerieSunat = dsNumCorr.Tables(0).Rows(0)("SERIE").ToString() 'serie
                                'strNumSunat = dsNumCorr.Tables(0).Rows(0)("CORRELATIVO").ToString() 'CORRELATIVO
                                tipo = dsNumCorr.Tables(0).Rows(0)("TIPO_DOCUMENTO").ToString() 'tipo
                                corrModulo = dsNumCorr.Tables(0).Rows(0)("REFERENCIA").ToString() 'cORRELATIVO DEL MODULO

                                If corrModulo <> "" Then
                                    arrayBusqueda = corrModulo.Split("|")
                                    referencia_asociada = arrayBusqueda(0).ToString()
                                    corrModulo = arrayBusqueda(1).ToString()
                                End If

                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio FP_Grabar_Reciclaje")
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "cod_pdv:" & PdV_Sinergia)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "TipoDoc:" & tipo)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "TipoDocRef:" & referencia_asociada)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "CorrSun:" & corrModulo)

                                Dim resultados As String = objClsTrsMsSap.FP_Grabar_Reciclaje(PdV_Sinergia, tipo, referencia_asociada, corrModulo)
                                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin FP_Grabar_Reciclaje")
                            End If
                        End If
                        '*********************** FIN de Flujo para Guardar el correlativo como reciclaje ****************************
                        'Inicio - Anulacion de Nota de Crédito de Flujo de CAja

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Actualizar_Estado_Pedido_Sicar_trs_pagos")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "NumFactSap:" & drFila.Item("PEDIN_NROPEDIDO"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "Origen:" & "1")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN " & "Estado:" & Estado_Anulado)

                        Dim updateEstado As String = objCajass.Actualizar_Estado_Pago(drFila("PEDIN_NROPEDIDO"), "1", Estado_Anulado)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Actualizar_Estado_Pedido_Sicar_trs_pagos")

                        Dim p_codRpta As String = ""
                        Dim p_MsgRpta As String = ""

                        'Dim strUsuario As String = Session("USUARIO")
                        'Dim strOficina As String = Session("ALMACEN")

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INGRESO DE PARAMETROS PARA LA ANULACIÓN en el Flujo de Caja (SP:MIG_ActEstadoVentasFact)")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. PEDIDO:  " & drFila("PEDIN_NROPEDIDO"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ESTADO ANULADO: " & ConfigurationSettings.AppSettings("AnulacionPago_FlujoCaja"))
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "USUARIO APLICACION: " & ConfigurationSettings.AppSettings("Usuario_Aplicacion_CuadreCaja"))

                        Try
                            objCajass.AnularPedido_CuadreCaja(drFila("PEDIN_NROPEDIDO"), ConfigurationSettings.AppSettings("AnulacionPago_FlujoCaja"), ConfigurationSettings.AppSettings("Usuario_Aplicacion_CuadreCaja"), p_codRpta, p_MsgRpta)
                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR EN EL SP:MIG_ActEstadoVentasFact")
                            'INI PROY-140126
                            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                            MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_Click)"
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
                            'FIN PROY-140126

                        End Try

                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "SALIDA DE PARAMETROS PARA LA ANULACIÓN en el Flujo de Caja (SP:MIG_ActEstadoVentasFact)")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_codRpta:  " & p_codRpta)
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "p_MsgRpta:  " & p_MsgRpta)
                        'Fin - Anulacion de Nota de Crédito de Flujo de CAja

                        '*Logica de Anulaciòn CONTRA EL WS y el MODULO
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO - Valida si es Nota de Crédito")
                        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Tipo de Documento: " & ClaseFactura)


                        If ClaseFactura = ConfigurationSettings.AppSettings("strTipDoc") Then
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Validamos si tiene Documento SAP : " & DocuSAP)
                            If Trim(DocuSAP).Length > 0 Then
                                'INVOCAMOS AL WS  
                                If ConfigurationSettings.AppSettings("LanzarServicio") = "0" Then
                                    Dim ClaseFactura_SAP As String = ConfigurationSettings.AppSettings("valorAUART")

                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO WEB SERVICE DE ANULACION DE PEDIDO")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Nro de Pedido: " & drFila("PEDIN_NROPEDIDO"))
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Nro Pago Interno: " & "")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Clase Factura_SAP: " & ClaseFactura_SAP)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Sociedad: " & Sociedad)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Canal de Distribucion: " & CanalDistribucion)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Sector: " & Sector)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Documento SAP: " & DocuSAP)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Fecha Contable: " & "")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Sociedad Venta Pago: " & Sociedad)
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Documento Compansacion:  " & "")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Punto de Venta : " & "")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Flag: " & ConfigurationSettings.AppSettings("FLAG_ANULAR_PEDIDO"))
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Usuario: " & "")
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    IN Terminal: " & "")
                                    Try
                                        objClsPagosWS.AnularPedidoPago(drFila("PEDIN_NROPEDIDO"), "", ClaseFactura_SAP, Sociedad, CanalDistribucion, Sector, DocuSAP, "", Sociedad, "", "", ConfigurationSettings.AppSettings("FLAG_ANULAR_PEDIDO"), CurrentUser, CurrentTerminal, K_COD_RESPUESTA, K_MSJ_RESPUESTA, K_ID_TRANSACCION)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Exito inicio invocacion el Webservice Anulacion de Pedido WS")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID K_COD_RESPUESTA: " & K_COD_RESPUESTA)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & K_MSJ_RESPUESTA)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    K_ID_TRANSACCION: " & K_ID_TRANSACCION)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Exito FIN invocacion el Webservice Anulacion de Pedido WS")

                                    Catch ex As Exception

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Fallo el Webservice Anulacion de Pedido WS")
                                        'INI PROY-140126
                                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                                        MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_Click)"
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Exception: " & ex.ToString() & MaptPath)
                                        'FIN PROY-140126

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID K_COD_RESPUESTA: " & K_COD_RESPUESTA)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ID MSJ DE RESPUESTA: " & K_MSJ_RESPUESTA)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    K_ID_TRANSACCION: " & K_ID_TRANSACCION)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anulacion de Pedido WS")

                                    End Try
                                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN WEB SERVICE DE ANULACION DE PEDIDO")
                                End If
                            End If
                        Else
                            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "No es NOTA DE CRÉDITO.")
                        End If


                        '******FIN DE WS DE ANULACION

                        'Renovacion Corporativa|
                        '  If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") Then
                        If drFila.Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") Then

                            'CONFIRMAR
                            Dim p_nro_pedido As String = Funciones.CheckStr(drFila("PEDIN_NROPEDIDO"))

                            'Anulacion Renovacion B2E
                            If dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("constCodRenovB2E") Then
                                AnularVentaRenovCorp(p_nro_pedido, "B2E")
                            End If

                            'Anulacion Renovacion Business
                            If dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("constCodRenovBus") Then
                                If dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE") = ConfigurationSettings.AppSettings("constDocClienteBus") Then
                                    AnularVentaRenovCorp(p_nro_pedido, "BUS")
                                End If
                            End If
                        End If
                        'Renovacion Corporativa

                        ' Anular Venta Portabilidad'
                        Anula_Portabilidad(CStr(drFila("PEDIN_NROPEDIDO")))

                        ' Anular Venta Renovacion Prepago
                        If Not IsNothing(dsPedido.Tables(0)) Then
                            'If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
                            If drFila.Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
                                Anula_RenovacionPrepago(CStr(drFila("PEDIN_NROPEDIDO")))
                            End If
                        End If

                        'Anula Promo Modem
                        Anula_Vta_Prom(drFila("PEDIN_NROPEDIDO"), ConfigurationSettings.AppSettings("ConstVentaEstado_Anulado"))

                        'VALIDAR EVERIS
                        Dim strDocSap As String
                        strDocSap = drFila("PEDIN_NROPEDIDO")
                        '--- ANULACION SISACT PREPAGO --- INICIO+
                        'CONFIRMAR
                        Dim dtDatos As New DataTable
                        Dim objBus As New COM_SIC_Cajas.clsCajas
                        dtDatos = objBus.ListarDatosCabeceraVenta(strDocSap)
                        If dtDatos.Rows.Count > 0 Then
                            Dim strAlmacen As String = dtDatos.Rows(0).Item("VEPR_COD_PDV").ToString()
                            Dim strCanal As String = dtDatos.Rows(0).Item("VEPR_COD_CAN").ToString()
                            Dim constDocAnulado As String = ConfigurationSettings.AppSettings("constDocAnulado")
                            Dim resultado As Integer = 0
                            Dim p_tipoDoc As String = drFila("PEDIV_DESCCLASEFACTURA")
                            Dim updVentaPrepago As Boolean = objBus.ActualizarPagoCorner(strDocSap, constDocAnulado, strAlmacen, p_tipoDoc, "", "", resultado)
                            'If Funciones.CheckStr(dtDatos.Rows(0).Item("VEPR_FLAG_VENTA_CAC").ToString()) = "1" Then
                                AnularReservaBAM(strDocSap)
                            'End If

                        End If
                        'CONFIRMAR
                        '--- ANULACION SISACT PREPAGO --- FIN
                        'ALINEACION PQT-169512-TSK-36121 ----INICIO
                        'roaming
                        Try

                            'Dim dsPedido As New DataSet
                            'dsPedido = (New SapGeneralNegocios).Get_ConsultaPedido("", Session("Almacen"), sDocumentoSap, "")
                            Dim objcajas As New COM_SIC_Cajas.clsCajas
                            Dim dtDet As DataTable = CType(dsPedido.Tables(1), DataTable)
                            Dim nroTelefono = Right(Funciones.CheckStr(dtDet.Rows(0)("DEPEV_NROTELEFONO")), 9)
                            Dim Flag As String = ""
                            Dim retorno As Int64 = 0
                            Dim mensaje As String = ""

                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Inicio Actualizacion Servicio Roaming")
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Telefono: " & nroTelefono)
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Flag: " & Flag)
                            objCajas.ActualizarServRoaming(strDocSap, nroTelefono, Flag, retorno, mensaje)
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "retorno: " & retorno)
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "mensaje: " & mensaje)
                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Error Actualizacion Servicio Roaming")
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & " Exception - " & "Error Message - MBC. " & ex.StackTrace)
                        Finally
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Fin Actualizacion Servicio Roaming")
                        End Try
                        'ALINEACION PQT-169512-TSK-36121 ----FIN
                        ' Inicio PS - Automatización de canje y nota de crédito
                        ' Liberar los puntos Claro Club si es la anulación de una nota de crédito que tiene 
                        ' Claro puntos reservados
                        Dim ID_CCLUB As String ' Tipo doc. identidad en PuntosCC
                        Dim NroDocumento As String
                        If ExisteCanjePuntosClaroClub(strDocSap, ID_CCLUB, NroDocumento) Then
                            LiberarPuntosCC(strDocSap, ID_CCLUB, NroDocumento)
                        End If
                        ' Fin PS - Automatización de canje y nota de crédito
                        Try
                            Dim strEstadoRegistro As String = ConfigurationSettings.AppSettings("constEstadoPendienteReposicion")
                            Dim intFlagBloqueo As Integer
                            Dim strCodBloqueo As String
                            Dim strTipoBloqueo As String
                            Dim strIccidActual As String
                            Dim strIccidNuevo As String
                            Dim strUsuario As String
                            Dim strMensaje As String
                            Dim strTipoVenta As String

                            Dim obj As New COM_SIC_Cajas.clsCajas
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Inicio Consulta_Existe_Pedido_Reposicion")
                            Dim intExistePedidoSisact As Integer = obj.Consulta_Existe_Pedido_Reposicion(strDocSap, strEstadoRegistro, intFlagBloqueo, strCodBloqueo, strTipoBloqueo, strIccidActual, strIccidNuevo, strUsuario, strTipoVenta)
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "intExistePedidoSisact: " & intExistePedidoSisact.ToString)

                            If intExistePedidoSisact = 1 Then
                                Dim estadoAnulado As String = ConfigurationSettings.AppSettings("constEstadoAnuladoReposicion")
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Inicio AnularReposicionPrepagoSisact")
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "strDocSap: " & strDocSap)
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "estadoAnulado: " & estadoAnulado)
                                Dim respuestaAnulacion As Boolean = obj.AnularReposicionPrepagoSisact(strDocSap, estadoAnulado)
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "respuestaAnulacion: " & respuestaAnulacion.ToString)
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Fin AnularReposicionPrepagoSisact")
                            End If
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Fin Consulta_Existe_Pedido_Reposicion")
                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "Error Actualizacion de estado de Reposicion. " & ex.Message)
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & " Exception - " & "Error Message - MBC. " & ex.StackTrace)
                        End Try

                        '***INICIO BLOQUE QUE LIBERA LAS RENOVACIONES Y SEC *****************
                        Try
                            If dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("strDTVRenovacion") Then
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & " ----- Inicio Anulacion de Renovacion -----")

                                Dim oVentaRenovacion As COM_SIC_Cajas.VentaRenovaPost
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "   Inicio Consulta Datos de Renovacion - Metodo: ConsultarVentaRenovPostCAC - SP: SISACT_CONS_VENTA_RENOV_CAC")
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN Doc SAP   : " & strDocSap)
                                oVentaRenovacion = (New COM_SIC_Cajas.clsCajas).ConsultarVentaRenovPostCAC(strDocSap)

                                'AndAlso oVentaRenovacion.SOLIN_CODIGO > 0

                                If Not oVentaRenovacion Is Nothing Then
                                    Dim SOLINCODIGO As String = Funciones.CheckStr(oVentaRenovacion.SOLIN_CODIGO)
                                    objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       ANTES DE LA SEC   : " & SOLINCODIGO)
                                    If oVentaRenovacion.SOLIN_CODIGO > 0 Then
                                        Dim blnOk As Boolean
                                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "   Inicio Libera Renovacion - Metodo: Actualiza_Del_CC - SP: SP_UPDATE_DEL_CC")
                                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN SEC    : " & oVentaRenovacion.SOLIN_CODIGO)
                                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN Doc SAP: " & strDocSap)

                                        blnOk = (New COM_SIC_Cajas.clsCajas).Actualiza_Del_CC(oVentaRenovacion.SOLIN_CODIGO, strDocSap)

                                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       OUT blnOk : " & blnOk)
                                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "   Fin Libera Renovacion")
                                    End If
                                End If
                                objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & " ----- Fin Anulacion de Renovacion -----")
                            End If
                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "ERROR AnularRenovacion: " & ex.Message.ToString())
                        End Try

                        Dim IdVenta, IdContrato As Int64
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "----- Inicio Anular Venta SISACT -----")
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "   Metodo: AnularVentaSisact SP: sp_anular_venta_x_docsap")
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN Doc SAP     : " & strDocSap)
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN Usuario     : " & CurrentUser)

                        AnularVentaSisact(strDocSap, CurrentUser, IdVenta, IdContrato)

                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       OUT ID Venta    : " & IdVenta)
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       OUT ID Contrato : " & IdContrato)
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "----- Fin Anular Venta SISACT ------")

                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "----- Inicio Anular Canje Equipo -----")
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "   Metodo: AnularCanjeEquipo SP: sp_anular_canje_equipo")
                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "       IN ID Venta    : " & IdVenta)

                        AnularCanjeEquipo(IdVenta)

                        objLog.Log_WriteLog(pathFile, strArchivo, " -" & strDocSap & "- " & "----- Fin Anular Canje Equipo ------")
                        '***FIN BLOQUE QUE LIBERA LAS RENOVACIONES Y SEC *****************

                        '----------------------------------------------------
                        'INICIATIVA-219 INI - Rollback Transaccion CBIO
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-INICIO-Rollback Transaccion CBIO")
                        Dim RollBackTransaccionCBIO_codRespuesta As String = ""
                        Dim RollBackTransaccionCBIO_msjRespuesta As String = ""
                        Dim objActivacionCBIO As New COM_SIC_Activaciones.ClsActivacionCBIO
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-Rollback Transaccion Input-IdContrato: " & CheckStr(IdContrato))
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-Rollback Transaccion Input-strCodOperacion: " & CheckStr(strCodOperacion))
                        objActivacionCBIO.RollBackTransaccionCBIO(IdContrato, strCodOperacion, RollBackTransaccionCBIO_codRespuesta, RollBackTransaccionCBIO_msjRespuesta)
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-FIN-Rollback Transaccion Output-CBIO-codRespuesta: " & CheckStr(RollBackTransaccionCBIO_codRespuesta))
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-FIN-Rollback Transaccion Output-CBIO-msjRespuesta: " & CheckStr(RollBackTransaccionCBIO_msjRespuesta))
                        objLog.Log_WriteLog(pathFile, strArchivo, "INICIATIVA-219-FIN-Rollback Transaccion CBIO")
                        '----------------------------------------------------

                        'INICIATIVA-710 - INICIO
                        Try
                            Dim strPedido As String = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
                            Dim strCodMsg As String = ""
                            Dim strDesMsg As String = ""

                            AnulaComboClaro(strPedido, strCodMsg, strDesMsg)
                            If strCodMsg = "1" Then
                                objLog.Log_WriteLog(pathFile, strArchivo, strDesMsg)
                                Response.Write("<script>alert('" & Funciones.CheckStr(strDesMsg) & "');</script>")
                                Exit Sub
                            End If
                        Catch ex As Exception
                            objLog.Log_WriteLog(pathFile, strArchivo, " - Exception - " & "Error Message. " & ex.StackTrace)
                        End Try
                        'INICIATIVA-710 - FIN

                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        Response.Write("<script language=jscript> alert('Proceso de anulación de pedido lanzado, Verifique posteriormente'); </script>")

                        'PROY-24724-IDEA-28174 - INI
                        If blnProteccionMovilPendiente Then
                            Dim strProteccionMovilPendiente = clsKeyAPP.strProteccionMovilPendiente 
                            Response.Write("<script language=jscript> alert('" & strProteccionMovilPendiente & "'); </script>")
                        End If
                        'PROY-24724-IDEA-28174 - FIN
                        CargarGrilla(ConsultaPuntoVenta(strCodOficina))
                End If
                Else
                    Response.Write("<script language=jscript> alert('Ocurrio un error al Anular el Pago. Intentar de Nuevo'); </script>")
                End If
            Else
                Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
            End If

            '----------------------------------------------------
            'PROY 26210 RMZ BEGIN - ESCRIBIR TIPI RENOVACION NO CONCRETADA. (Solo BOLETA - RENOVACION)
            objLog.Log_WriteLog(pathFile, strArchivo, "PREPARANDO PARA TIPIFICACION - RENOVACION NO CONCRETADA")
            objLog.Log_WriteLog(pathFile, strArchivo, "Tipo Venta: " & dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
            objLog.Log_WriteLog(pathFile, strArchivo, "Tipo Operacion: " & dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
            objLog.Log_WriteLog(pathFile, strArchivo, "Tipo Documento: " & drFila.Item("PEDIC_CLASEFACTURA"))
            If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") And _
                dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION") = ConfigurationSettings.AppSettings("strDTVRenovacion") And _
                drFila.Item("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA") Then
                Me.GenerarTipificacionRenovacionNoConcretada(drReport)
            End If
            '---------------------------------------------------- 

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "----------------------------------------------------")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "            FIN ANULACION DE PEDIDO                 ")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "----------------------------------------------------")

            ActualizarWL(drFila.ItemArray(31)) 
			objLog.Log_WriteLog(pathFile, strArchivo, "Fin Actualizar WhiteList")
                  
            'PROY-140662-BLOQUE-FINAL-INI
            Dim strNroPedido As String = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
            Dim strFlagTOA As String = COM_SIC_Seguridad.ReadKeySettings.Key_flagTOA
            If (strFlagTOA = "1") Then
                CancelarOrdenToa(strNroPedido, strIdentifyLog)
            End If
            'PROY-140662-BLOQUE-FINAL-FIN

        Catch ex As Exception
            wParam5 = 0
            wParam6 = "Error en Anulación de documentos. " & ex.Message
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            objLog.Log_WriteLog(pathFile, strArchivo, " - Exception - " & "Error Message - MBC. " & ex.StackTrace)
            objLog.Log_WriteLog(pathFile, strArchivo, " - PBI000002133882 Exception - " & "Error Message: " & ex.Message)
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub CancelarOrdenToa(ByVal strNroPedido As String, ByVal strIdentifyLog As String)
        Dim strCodRpt, strMsjRpt As String
        Dim objReqCancelarToa As New COM_SIC_Activaciones.CancelarOrdenRequest
        Dim objRespCancelarToa As New COM_SIC_Activaciones.CancelarOrdenResponse
        Dim objBECanOrdenReq As New COM_SIC_Activaciones.BECancelarOrdenRequest
        Dim objHeaderRequest As New COM_SIC_Activaciones.HeaderRequest
        Dim objBEAuditoriaREST As New COM_SIC_Activaciones.AuditoriaEWS
        Dim objCancelarTOA As New COM_SIC_Activaciones.BWCancelarOrdenTOA

        Try
            objHeaderRequest.consumer = ""
            objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
            objHeaderRequest.dispositivo = ""
            objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
            objHeaderRequest.modulo = ""
            objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
            objHeaderRequest.operation = ""
            objHeaderRequest.pid = ""
            objHeaderRequest.system = ""
            objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
            objHeaderRequest.userId = ""
            objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_RegistrarCampana_wsIp"))

            objReqCancelarToa.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objBECanOrdenReq.nroPedido = strNroPedido
            objBECanOrdenReq.aplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("CONS_APLICACION"))

            objReqCancelarToa.MessageRequest.Body.cancelarOrdenToaRequest = objBECanOrdenReq

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = CurrentTerminal
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 IDTRANSACCION:" & objBEAuditoriaREST.IDTRANSACCION)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 IPAPLICACION:" & objBEAuditoriaREST.IPAPLICACION)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 idTransaccionNegocio:" & objBEAuditoriaREST.idTransaccionNegocio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 idTransaccionNegocio:" & objBEAuditoriaREST.APLICACION)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 idTransaccionNegocio:" & objBEAuditoriaREST.applicationCodeWS)

            objRespCancelarToa = objCancelarTOA.CancelarOrdenTOA(objReqCancelarToa, objBEAuditoriaREST)
            strCodRpt = objRespCancelarToa.MessageResponse.Body.responseAudit.codigoRespuesta
            strMsjRpt = objRespCancelarToa.MessageResponse.Body.responseAudit.mensajeRespuesta
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 strCodRpt:" & strCodRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Proy-140662 strMsjRpt:" & strMsjRpt)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140662-Bloque-Final Error al Cancelar TOA : " & ex.Message.ToString)
        End Try
    End Sub
    'INICIO PROY-140360 - IDEA-46301
    Private Function ValidarModalidadVentaContratoCode(ByVal strNumSEC As String) As Boolean

        Dim strIdentifyLog As String = CStr(Session("USUARIO")) & " - " & strNumSEC
        Dim objActivaciones As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Validar Modalidad Venta ContratoCode - INICIO")
            ValidarModalidadVentaContratoCode = False

            lista = objActivaciones.ConsultaSolicitud_NROSEC(Funciones.CheckStr(strNumSEC))

            If Not lista Is Nothing And lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    If Funciones.CheckStr(item.MODALIDAD_VENTA).Equals("2") Then
                        ValidarModalidadVentaContratoCode = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 Modalidad de Venta : " & Funciones.CheckStr(item.MODALIDAD_VENTA))
                        Exit For
                    End If
                Next
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301  No hay datos de la sec")
            End If
        Catch ex As Exception
            ValidarModalidadVentaContratoCode = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Error al validar Modalidad ContratoCode : " & ex.Message.ToString)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-140360-IDEA-46301 Validar Modalidad Venta ContratoCode - FIN")
        End Try

        Return ValidarModalidadVentaContratoCode
    End Function
    
    ' FIN PROY-140360 - IDEA-46301
    Private Sub ActualizarWL(ByVal NRO_Telefono As String)

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim Rpta As New Integer
        Dim Estado As String
        Dim IMEI As String
        Dim dtResultado As DataTable
        Dim drResultado As DataRow
        Dim DocSunat As String

        DocSunat = Request.QueryString("docSunat")
        'NRO_Telefono = Request.QueryString("numeroTelefono")

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio del mètodo ActualizarWL")
        dtResultado = objClsConsultaPvu.ConsultaVentaRenoWL(NRO_Telefono, "1")
        If dtResultado.Rows.Count > 0 Then
            drResultado = dtResultado.Rows(0)
            Estado = drResultado.Item("WLRPV_ESTADO")
            IMEI = drResultado.Item("WLRPV_IMEI_EQUIPO")
        End If


        If Estado = "1" And IMEI <> "" Then
            Rpta = objClsConsultaPvu.ActualizaVentaRenoWL(NRO_Telefono, "0")
            If Rpta = "0" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - Actualizado OK")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - Error al Actualizar")
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin del mètodo ActualizarWL - No Actualizado")
        End If

    End Sub


    'Private Sub btnReasignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReasignar.Click
    '    'Dim dvPagos As New DataView(ds.Tables(0))
    '    'MODIFICADO POR FFS INICIO
    '    'Dim dvPagos As New DataView(ds.Tables(0))
    '    Dim dvPagos As New DataView(dtsap)
    '    'MODIFICADO POR FFS FIN


    '    If Len(Trim(Request.Item("rbPagos"))) > 0 Then
    '        dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
    '        drFila = dvPagos.Item(0).Row
    '        Session("DocSel") = drFila
    '        Session("Pool") = ""
    '        Session("ReAsigna") = "S"
    '        Response.Redirect("asigSunatImp.aspx")
    '    End If
    'End Sub

'PROY-140335 -INI
    Private Sub cmdProcesarSP_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcesarSP.Click

        Dim strMensaje As String = "", strCodigo As String = ""
        Dim objAudit As New BEAuditoria
        Dim dsPedido As DataSet
        Dim dsPedido_Alta As DataSet
        Dim strIdentifyLog As String = Funciones.CheckStr(txtRbPagos.Value)
        Dim strFiltroPedidoAcc As String
        Dim strPedidoAcc As String
        Dim strNroSec As String
        Dim strCodRespPorta As String
        Dim strMensajPorta As String
        Dim odtPrePagoPedido As DataTable
        Dim odtPrePagoDetPedido As DataTable
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strCodTipoOperacion, PedidoRecarga As String
        Dim pMsjRpta As String
        Dim obj As New ClsPortabilidad
        Dim pstrCodRpta, pstrMsgRpta As String
        Dim ds_contenedor As DataSet
        Dim objConsulta As New COM_SIC_Activaciones.ClsPortabilidad
        Dim NroPedidoAlta As String
        Dim pResultado_validacion As Boolean
        Dim blValidaOperacion As Boolean = False
        Dim strDescriOpePostPago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
        Dim strTVPostpago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))
        Dim strDescriOpePrePago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
        Dim strTVPrepago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPrepago"))
        Dim dt As DataGrid = dgPool
        Dim COD_MSG_RENTA As String = ""
        Dim MSJ_RESPUESTA As String = ""
        Dim COD_RESPUESTA As String = ""

        'CAMPAÑA COMBO PREPAGO - INICIO
        Dim strNroPedido As String = Funciones.CheckStr(txtRbPagos.Value())

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO")
        Dim codRespuesta As String = String.Empty
        Dim msjRespuesta As String = String.Empty

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 1 " & strNroPedido)
            dsPedido = objConsultaMsSap.ConsultaPedido(strNroPedido, "", "")
            Dim valTipo_Venta As String = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 2 " & strNroPedido)
            If valTipo_Venta = ConfigurationSettings.AppSettings("strTVPrepago").ToString() Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 3 " & strNroPedido)
                ValidaPagoCombosPrepago(strNroPedido, codRespuesta, msjRespuesta)
                If codRespuesta = "1" Then
                    Response.Write("<script>alert('" & Funciones.CheckStr(msjRespuesta) & "');</script>")
                    Exit Sub
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIA VALIDACION COMBO CLARO PREPAGO 4 " & strNroPedido)
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR:" & "- " & msjRespuesta.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
            Exit Sub
        End Try
        'CAMPAÑA COMBO PREPAGO - FIN

        'INICIO IDEA300216
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "******** Inicio Validacion CLAVE UNICA ProcesarSP********")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*********************************************************")


            Dim nroPedido60 = Funciones.CheckStr(Request.Item("rbPagos"))
            Dim sRptaValidaCU As String = String.Empty
            Dim sNroSEC As String = String.Empty
            Dim sTipoVenta As String = String.Empty
            Dim sTransaccion As String = String.Empty
            Dim sTipoDoc As String = String.Empty
            Dim sNumDoc As String = String.Empty
            Dim sRptaValNotificacion As String = String.Empty

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "nroPedido60: " & Funciones.CheckStr(nroPedido60))

            sRptaValidaCU = GetDataValidarClaveUnica(nroPedido60, sTipoDoc, sNumDoc, sNroSEC, sTipoVenta, sTransaccion)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sRptaValidaCU: " & Funciones.CheckStr(sRptaValidaCU))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTipoDoc: " & Funciones.CheckStr(sTipoDoc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNumDoc: " & Funciones.CheckStr(sNumDoc))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sNroSEC: " & Funciones.CheckStr(sNroSEC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTipoVenta: " & Funciones.CheckStr(sTipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sTransaccion: " & Funciones.CheckStr(sTransaccion))

            If sRptaValidaCU = "0" Then
                Dim sMsjeFinal As String = String.Empty
                Dim sCodFinal As String = String.Empty

                sRptaValNotificacion = GetAprobClaveUnica(sTipoDoc, sNumDoc, sNroSEC, nroPedido60, sTipoVenta, sTransaccion)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "sRptaValNotificacion: " & Funciones.CheckStr(sRptaValNotificacion))

                If sRptaValNotificacion.IndexOf(";") > -1 Then
                    sCodFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(0))
                    If sCodFinal = "0" Or sCodFinal = "1" Then 'CLAVE UNICA PENDIENTE
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    ElseIf sCodFinal = "2" Then 'CLAVE UNICA APROBADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                    ElseIf sCodFinal = "3" Then 'CLAVE UNICA EXPIRADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    ElseIf sCodFinal = "5" Then 'SE OMITIO X REGLA CLAVE UNICA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                    ElseIf sCodFinal = "4" Or sCodFinal = "6" Then 'ANULADA
                        sMsjeFinal = Funciones.CheckStr(sRptaValNotificacion.Split(";")(1))
                        Response.Write("<script>alert('" & Funciones.CheckStr(sMsjeFinal) & "');</script>")
                        Exit Sub
                    End If
                Else
                    Response.Write("<script>alert('" & Funciones.CheckStr(ReadKeySettings.Key_MensajeErrorConsultaClaveUnica) & "');</script>")
                    Exit Sub
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NO PASARA CLAVE UNICA TRANSACCION O PRODUCTO NO REGULADO")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Excepcion: " & Funciones.CheckStr(ex.Message))
            Response.Write("<script>alert('" & ex.Message & "');</script>")
            Exit Sub
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "********** Fin Validacion CLAVE UNICA *********")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "***********************************************")

        End Try
        'INICIO IDEA300216
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][INICIO][cmdProcesarSP]")
            objAudit.Terminal = CurrentTerminal
            objAudit.Usuario = CurrentUser
            objAudit.HostName = Funciones.CheckStr(Request.UserHostName)
            objAudit.ServerName = Funciones.CheckStr(Request.ServerVariables("REMOTE_ADDR"))
            objAudit.IPServer = Funciones.CheckStr(Request.ServerVariables("HTTP_X_FORWARDED_FOR"))
            btnPagar.Attributes.Add("disabled", "true")
            If (txtsession.Value = 1) Then
                strFiltroPedidoAcc = "ID_T_TRS_PEDIDO=" & txtRbPagos.Value
                strPedidoAcc = txtRbPagos.Value
            Else
                strFiltroPedidoAcc = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
                strPedidoAcc = Request.Item("rbPagos")
            End If

            '****** CONSULTA SI TIENE RENTA SIN PAGAR PARA SEGUIR EL PREOCESO *****'
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia consulta si ha Pagado Renta antes de Pagar")

            Try
                objConsultaMsSap.Validar_PagoRenta(Funciones.CheckDbl(txtRbPagos.Value), COD_MSG_RENTA, COD_RESPUESTA, MSJ_RESPUESTA)


                If COD_MSG_RENTA = "0" Then
                    'Session("MSG_RENTA") = "El documento tiene asociado Rentas Adelantadas sin Pago"
                    Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("MSG_RENTA_ADELANTADA").ToString & "');</script>")
                    btnPagar.Attributes.Remove("disabled")
                    Exit Sub
                End If

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error consulta si ha Pagado Renta antes de Pagar")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Exception" & "- " & ex.ToString)
                Exit Sub
            End Try

            '***************************************************************************************************'
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][cmdProcesarSP]=>" & Funciones.CheckStr(strPedidoAcc))

            dsPedido = objConsultaMsSap.ConsultaPedido(strPedidoAcc, "", "")

            Dim strCodTipVenta As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIC_TIPOVENTA"))
            Dim strDesTipOpe As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_DESCTIPOOPERACION"))
            Dim NroPed64Porta As Integer = Convert.ToInt64(Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO")))
            If dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") = "0000" Then
                NroPedidoAlta = dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOALTA")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDO-RENTA][NroPedidoAlta]=>" & NroPedidoAlta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDO-RENTA][ConsultaPedidoAlta]=>" & NroPedidoAlta)

                dsPedido_Alta = objConsultaMsSap.ConsultaPedido(NroPedidoAlta, "", "")
            Else
                NroPedidoAlta = strPedidoAcc
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDO-BOLETA][NroPedidoAlta]=>" & NroPedidoAlta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDO-RENTA][ConsultaPedidoAlta]=>" & NroPedidoAlta)

                dsPedido_Alta = dsPedido
            End If

            Dim nroPedidoPin As String = String.Empty ''JMGF


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][strCodTipOpe]=>" & Funciones.CheckStr(strCodTipVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][strDesTipOpe]=>" & Funciones.CheckStr(strDesTipOpe))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][NroRentaPorta]=>" & Funciones.CheckStr(NroPed64Porta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][NroPedidoAlta]=>" & Funciones.CheckStr(NroPedidoAlta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][cmdProcesarSP][INI][Validacion Operacion Permitida]")
            If (strDescriOpePostPago = strDesTipOpe) AndAlso (strCodTipVenta = strTVPostpago) Then
                strNroSec = Me.Obtener_NroSec_PostPago(Funciones.CheckStr(NroPedidoAlta))

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][POSTPAGO-SEC][strNroSec]=>" & strNroSec)
                nroPedidoPin = Funciones.CheckStr(NroPedidoAlta) ''JMGF
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [PROY-140585 F2][PROCESARSP][NroPedidoAlta][nroPedidoPin]=>" & nroPedidoPin) ''JMGF

            ElseIf (strDescriOpePrePago = strDesTipOpe) AndAlso (strCodTipVenta = strTVPrepago) Then
                strNroSec = Me.Obtener_NroSec_PrePago(NroPed64Porta, strCodRespPorta, strMensajPorta, odtPrePagoPedido, odtPrePagoDetPedido)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PREPAGO-SEC][strNroSec]=>" & strNroSec)

                nroPedidoPin = Funciones.CheckStr(NroPed64Porta) ''JMGF
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [PROY-140585 F2][PROCESARSP][NroPed64Porta][nroPedidoPin]= " & nroPedidoPin) ''JMGF

            End If

            'INI PROY 140585
            Dim strOpePortaPost = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
            Dim strOpePortaPre = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
            Dim strTipo_opera_Des As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_DESCTIPOOPERACION"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO ConsultaPinPortabilidad")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strTipo_opera_Des] => " & strTipo_opera_Des)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strNroSec] => " & strNroSec)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strOpePortaPost] => " & strOpePortaPost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][strOpePortaPre] => " & strOpePortaPre)

            If strTipo_opera_Des = strOpePortaPost Or strTipo_opera_Des = strOpePortaPre Then
                'PROY-140740 INI
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-INICIO Consultar Oferta")
                Dim strOferta As String
                Dim strOfertaBusiness = Funciones.CheckStr(ConfigurationSettings.AppSettings("ConsOfertaBusiness"))
                strOferta = ConsultaOferta(strNroSec)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-Oferta-->" + strOferta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-FIN Consultar Oferta")
                If strOferta <> strOfertaBusiness Then

                Dim sResultado As String
                sResultado = ConsultaPinPortabilidad(strNroSec, nroPedidoPin)
                Dim ArrResult() = sResultado.Split("|"c)
                If ArrResult.Length > 0 Then
                    If ArrResult(0) <> "true" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ConsultaPinPortabilidad][DEBE PASAR PIN PORTABILIDAD POR APLICATIVO MOVIL]")
                        Response.Write("<script>alert('" & ArrResult(1) & "');</script>")
                        Exit Sub
            End If
                End If
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROY-140740-ES BUSINESS NO DEBE PASAR PIN PORTABILIDAD")
                'PROY-140740 FIN
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN ConsultaPinPortabilidad")
            'FIN PROY 140585

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][strNroSec]=>" & Funciones.CheckStr(strNroSec))
            ds_contenedor = obj.Obtener_tipo_producto(strNroSec, NroPedidoAlta, pstrCodRpta, pstrMsgRpta)

            If clsKeyAPP.consTipoProductoPermitidosSP.IndexOf(ds_contenedor.Tables(0).Rows(0).Item("PRDC_CODIGO")) > -1 Then
                blValidaOperacion = True
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][cmdProcesarSP][Validacion Operacion Permitida][blValidaOperacion]" & Funciones.CheckStr(blValidaOperacion.ToString()))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][cmdProcesarSP][FIN][Validacion Operacion Permitida]")
            If Not blValidaOperacion Then
                btnPagar_Click(sender, e)
                btnPagar.Attributes.Remove("disabled")
                Exit Sub
            End If

            If blValidaOperacion Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][INI][Envio de SP]")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][strNroSec]" & strNroSec)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][NroPedidoAlta]" & NroPedidoAlta)

                Dim theReturn As Int32 = validarEnvioSolicitudPortabilidad(strNroSec, dsPedido_Alta, objAudit, strMensaje, strCodigo, NroPedidoAlta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][strCodigo]" & strCodigo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][strMensaje]" & strMensaje)
                If (theReturn = 1) Then
                    Response.Write("<script>alert('" & strMensaje & "');</script>")
                    Me.RegisterStartupScript("f_ProcesarPago", "<script language=javascript>f_ProcesarPago();</script>")
                ElseIf (theReturn = 2) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][Se Procesa la SP]")
                    Me.RegisterStartupScript("f_ProcesarPago", "<script language=javascript>f_ProcesarPago();</script>")
                    Exit Sub
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INC-INC000001606873][VALIDARENVIOSP][Error al enviar SP]")
                    btnPagar.Attributes.Remove("disabled")
                    Response.Write("<script>alert('" & strMensaje.Replace(Chr(10), " ") & "');</script>")
                End If
            Else
                btnPagar_Click(sender, e)
                btnPagar.Attributes.Remove("disabled")
                Exit Sub
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][FIN][cmdProcesarSP]")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--[INC-INC000001606873]- Exception Obtener_tipo_producto : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][VALIDARENVIOSP][FIN][cmdProcesarSP]")
            btnPagar.Attributes.Remove("disabled")
            Response.Write("<script>alert('" & ex.Message & "');</script>")
        End Try
    End Sub

    Private Sub cmdProcesarPago_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcesarPago.Click

        Dim SOPOC_ID_SOLICITUD As String   'PROY 32089
        Dim strLstIDSolicitud As New ArrayList 'PROY 32089
        Dim strCodRpta As String = String.Empty, strMsgRpta As String = String.Empty
        Dim blnExistePEP As Boolean
        Dim intPendienteSP As Boolean = False
        Dim pResultado_validacion As Boolean = False
        Dim intProcesoSP As Integer
        Dim strIdentifyLog As String = Funciones.CheckStr(txtRbPagos.Value)
        Dim strMensaje As String = "", strCodigo As String = ""
        Dim dsPedido As DataSet
        Dim strFiltroPedidoAcc As String
        Dim strPedidoAcc As String
        Dim strNroSec As String
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strCodTipoOperacion As String
        Dim pMsjRpta As String
        Dim obj As New ClsPortabilidad
        Dim pstrCodRpta, pstrMsgRpta As String
        Dim ds_contenedor As DataSet
        Dim objConsulta As New COM_SIC_Activaciones.ClsPortabilidad
        Dim NroPedidoAlta As String
        Dim mensajeABDCP As String
        Dim strDescriOpePostPago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
        Dim strTVPostpago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPostpago"))
        Dim strDescriOpePrePago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
        Dim strTVPrepago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("strTVPrepago"))
        Dim strCodRespPorta As String
        Dim strMensajPorta As String
        Dim odtPrePagoPedido As DataTable
        Dim odtPrePagoDetPedido As DataTable
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][INICIO][cmdProcesarPago]")
        strPedidoAcc = Funciones.CheckStr(txtRbPagos.Value)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][strPedidoAcc][]=>" & strPedidoAcc)

        dsPedido = objConsultaMsSap.ConsultaPedido(strPedidoAcc, "", "")
        If dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA") = "0000" Then
            NroPedidoAlta = dsPedido.Tables(0).Rows(0).Item("PEDIN_PEDIDOALTA")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDORENTA][NroPedidoAlta]=>" & NroPedidoAlta)
        Else
            NroPedidoAlta = strPedidoAcc
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PEDIDOBOLETA][NroPedidoAlta]=>" & NroPedidoAlta)

        End If
        Dim strDesTipOpe As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIV_DESCTIPOOPERACION"))
        Dim strCodTipVenta As String = Funciones.CheckStr(dsPedido.Tables(0).Rows(0)("PEDIC_TIPOVENTA"))
        If (strDescriOpePostPago = strDesTipOpe) AndAlso (strCodTipVenta = strTVPostpago) Then
            strNroSec = Me.Obtener_NroSec_PostPago(Funciones.CheckStr(NroPedidoAlta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][POSTPAGO-SEC][strNroSec]=>" & strNroSec)
        ElseIf (strDescriOpePrePago = strDesTipOpe) AndAlso (strCodTipVenta = strTVPrepago) Then
            strNroSec = Me.Obtener_NroSec_PrePago(NroPedidoAlta, strCodRespPorta, strMensajPorta, odtPrePagoPedido, odtPrePagoDetPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][PREPAGO-SEC][strNroSec]=>" & strNroSec)
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][NroPedidoAlta]=>" & NroPedidoAlta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][strNroSec]=>" & strNroSec)

        Dim objDatos As DataSet = objConsulta.ValidarSEC(strNroSec, strCodRpta, strMsgRpta)
        ds_contenedor = obj.Obtener_tipo_producto(strNroSec, NroPedidoAlta, pstrCodRpta, pstrMsgRpta)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][ValidarEstadoPendienteSP]")
        intPendienteSP = New clsActivaciones().ValidarEstadoPendienteSP(objDatos, ds_contenedor, blnExistePEP, strLstIDSolicitud, mensajeABDCP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][ValidarEstadoPendienteSP][blnExistePEP]=>" & blnExistePEP.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][ValidarEstadoPendienteSP][mensajeABDCP]=>" & Funciones.CheckStr(mensajeABDCP))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][ValidarEstadoPendienteSP][intPendienteSP]=>" & Funciones.CheckStr(intPendienteSP.ToString()))

        If intPendienteSP Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][ValidarEstadoPendienteSP][SOLICITUD DE PORTABIIDAD PENDIENTE]")
            btnPagar.Attributes.Remove("disabled")
            Response.Write("<script>alert('Pendiente de Respuesta de la Solicitud de Portabilidad...')</script>")
            Exit Sub
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][INICIO][Session(IdSolicitudPorta)")
        Session("IdSolicitudPorta") = strLstIDSolicitud
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][Session(IdSolicitudPorta)=> " & strLstIDSolicitud.Count)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][PROCESARSP][cmdProcesarPago][FIN][Session(IdSolicitudPorta)")
        intProcesoSP = Convert.ToInt64(New clsActivaciones().ValidarSECProcesoSP(strNroSec, strMensaje, CurrentTerminal, CurrentUser, NroPedidoAlta))

        strMensaje = strMensaje.Replace("..", ".")
        btnPagar.Attributes.Remove("disabled")
        If (blnExistePEP) Then
            intProcesoSP = 1
        End If

        Select Case intProcesoSP
            Case 1
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][ValidarSECProcesoSP]" & Funciones.CheckStr(strMensaje) & ".")

                btnPagar_Click(sender, e)
                Exit Select
            Case 2
                '[INC-INC000001606873]mensaje.error.solicitud.portabilidad.procedente.idf2=Error: Solicitud portabilidad No es Procedente.
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][ValidarSECProcesoSP]" & Funciones.CheckStr(strMensaje) & ".")
                Response.Write("<script>alert('" & strMensaje.Replace(Chr(10), " ") + " " + mensajeABDCP.Replace(Chr(10), " ") & "');</script>")
                Exit Select
            Case 3
                '[INC-INC000001606873]mensaje.exito.solicitud.portabilidad.procedente.idf3=Acreditación Ok.
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][ValidarSECProcesoSP]" & Funciones.CheckStr(strMensaje) & ".")

                btnPagar_Click(sender, e)
                Exit Select
            Case 4
                '[INC-INC000001606873]mensaje.error.solicitud.portabilidad.procedente.idf4=Error: Sin Acreditación.
                Response.Write("<script>alert('" & strMensaje.Replace(Chr(10), " ") + " " + mensajeABDCP.Replace(Chr(10), " ") & "');</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][ValidarSECProcesoSP]" & Funciones.CheckStr(strMensaje) & ".")

                Exit Select
            Case Else
                '[INC-INC000001606873]Otros Errores 
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- [INC-INC000001606873][ValidarSECProcesoSP]" & Funciones.CheckStr(strMensaje) & ".")
                Response.Write("<script>alert('" & strMensaje.Replace(Chr(10), " ") + " " + mensajeABDCP.Replace(Chr(10), " ") & "');</script>")
                Exit Select
        End Select
    End Sub
'PROY-140335 -FIN


    Private Sub cmdProcesar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcesar.Click
        Dim strOficinaVta As String = Session("ALMACEN") ' oficina de vta
        Dim strCodigo As String
        strCodigo = txtRbPagos.Value
        Dim strRespuesta As String

        Dim objPedidoProcesar As New COM_SIC_Procesa_Pagos.PedidoProcesar
        objPedidoProcesar.MigrarPedidoPago(strCodigo, strRespuesta)
        If strRespuesta = "" Then
            Response.Write("<script>alert('El Pago fue Procesado Correctamente');</script>")
            CargarGrilla(ConsultaPuntoVenta(strOficinaVta))
        Else
            Response.Write("<script>alert('" & strRespuesta & "');</script>")
        End If
    End Sub


    Private Sub ImprimeAnulado(ByVal strRefer As String)

        Dim strScript As String
        Dim dsResult As DataSet
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu           '**** SINEGIA60 ****'

        Dim k_nrolog As Int64 = 0
        Dim k_deslog As String = ""

        'dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))     '** 
        dsResult = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)
        'si la impresion no es por SAP, abrir cuadro de impresoras
        'If Trim(dsResult.Tables(0).Rows(0).Item("IMPRESION_SAP")) = "" Then
        If Not dsResult Is Nothing Then
            If Trim(IIf(IsDBNull(dsResult.Tables(0).Rows(0).Item("PAOFC_IMPRESION_VIA_IMPRESORA")), "", dsResult.Tables(0).Rows(0).Item("PAOFC_IMPRESION_VIA_IMPRESORA"))) = "" Then
                If Left(strRefer, 2) = ConfigurationSettings.AppSettings("k_Prefijo_Ticket") Then

                    strScript = "<script language=javascript>" & Chr(13)
                    strScript = strScript & "function f_ImprimirAnul(){" & Chr(13)
                    strScript = strScript & "var objIframe = document.getElementById('IfrmImpresion');" & Chr(13)
                    strScript = strScript & "objIframe.style.visibility = 'visible';" & Chr(13)
                    strScript = strScript & "objIframe.style.width = 0;" & Chr(13)
                    strScript = strScript & "objIframe.style.height = 0;" & Chr(13)
                    strScript = strScript & "objIframe.contentWindow.location.replace('ImpresionAnularTicket.aspx?NroSunat=" & strRefer & "');" & Chr(13)
                    strScript = strScript & "}" & Chr(13)
                    strScript = strScript & "f_ImprimirAnul();" & Chr(13)
                    strScript &= "</script>" & Chr(13)

                    RegisterClientScriptBlock("Imprime", strScript)
                End If
            End If
        End If
    End Sub
    Private Sub Anula_Portabilidad(ByVal strNroPedido As String)

        Dim i, intResultado As Integer
        Dim resultado As Integer = 0
        Dim oListaTelefono As DataSet
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim strTipoVenta As Integer = 0

        Dim blnOK As Boolean
        Dim strMensaje As String
        'Dim objVentas As New SAP_SIC_Ventas.clsVentas

        Dim objSans As New NEGOCIO_SIC_SANS.SansNegocio
        Dim strCodUsuario As String = IIf(IsNothing(CurrentUser), Session("codUsuario"), CurrentUser)
        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - INICIO Anula_Portabilidad")
        Try
            If Not strNroPedido = "" Then
                oListaTelefono = objCajas.FP_Get_TelefonosPorta(strNroPedido, strTipoVenta, intResultado)

                If intResultado = 0 Then
                    For i = 0 To oListaTelefono.Tables(0).Rows.Count - 1
                        Dim msisdn As String = ""
                        Dim NroSEC As String = ""
                        NroSEC = oListaTelefono.Tables(0).Rows(i).Item("SOLIN_CODIGO")
                        msisdn = oListaTelefono.Tables(0).Rows(i).Item("PORT_NUMERO")

                        'blnOK = objVentas.Set_AnulaTelefonoPortable(msisdn, strMensaje)

                        'INC000000961273
                        Dim flagActivaSimcard As String = ConfigurationSettings.AppSettings("constActivaSimcardsWS")
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - flagActivaSimcard: " & flagActivaSimcard)
                        If flagActivaSimcard.Equals("1") Then
                            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - INICIO AnulaTelefonoPortable: " & i)
                            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - IN msisdn: " & msisdn)
                            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - IN strCodUsuario: " & strCodUsuario)
                        blnOK = objSans.Set_AnulaTelefonoPortable(msisdn, strCodUsuario)
                            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - OUT blnOK: " & blnOK)
                            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - FIN AnulaTelefonoPortable: " & i)
                        End If

                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - INI RollbackDetalleVenta: " & i)
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - NroSEC: " & NroSEC)
                        resultado = objCajas.RollbackDetalleVenta(msisdn, CType(NroSEC, Int64))
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - resultado: " & resultado)
                        objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - FIN RollbackDetalleVenta: " & i)
                    Next
                End If
            End If
            objLog.Log_WriteLog(pathFile, strArchivo, strNroPedido & " - FIN Anula_Portabilidad")
        Catch ex As Exception
            Response.Write("<script>alert('" & "Error. Anulacion de Venta de Portabilidad. " & ex.Message & "')</script>")
        End Try
    End Sub

    Private Sub Anula_RenovacionPrepago(ByVal strNroPedido As String)

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer
        Try
            intResult = objCajas.FP_Anula_RenovacionPrepago(strNroPedido)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
    End Sub

    'INICIATIVA-710 - INICIO
    Private Sub ValidaPagoCombosPrepago(ByVal strNroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String)

        Dim objActi As New COM_SIC_Activaciones.ClsActivacionCBIO
        Dim strIdentifyLog As String = strNroPedido
        Try

            objActi.ValidaPagoComboPrepago(strNroPedido, codRespuesta, msjRespuesta)

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[VALIDACION COMBO PREPAGO] Codigo Respuesta:" & codRespuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[VALIDACION COMBO PREPAGO] Descripción Respuesta: " & msjRespuesta)

        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN - [VALIDACION COMBO PREPAGO]")
    End Sub

    Private Sub AnulaComboClaro(ByVal strNroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String)

        Dim strIdentifyLog As String = strNroPedido
        Dim dsPedido As DataSet
        Dim objActi As New COM_SIC_Activaciones.ClsActivacionCBIO
        Dim objClsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim PdV_Sinergia As String = Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN")))

        Try

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO - CONSULTA MSSAP => NROPEDIDO: ] " & strNroPedido)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO - CONSULTA MSSAP => PDV_SINERGIA: ] " & PdV_Sinergia)

            dsPedido = objClsConsultaMsSap.ConsultaPedido(strNroPedido, PdV_Sinergia, "")

            If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") Then
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INICIO ANULA COMBO - POSTPAGO]")
                objActi.AnulaCombosPostpago(strNroPedido, codRespuesta, msjRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO POSTPAGO] Codigo Respuesta:" & codRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO POSTPAGO] Descripción Respuesta: " & msjRespuesta)
            ElseIf dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INICIO ANULA COMBO - PREPAGO]")
                objActi.AnulaCombosPrepago(strNroPedido, codRespuesta, msjRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO PREPAGO] Codigo Respuesta:" & codRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULA COMBO PREPAGO] Descripción Respuesta: " & msjRespuesta)
            End If

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ERROR ANULA COMBO]")
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try

    End Sub

    Private Sub ValidaAnulacionCombo(ByVal strNroPedido As String, ByRef codRespuesta As String, ByRef msjRespuesta As String)

        Dim dsPedido As DataSet
        Dim objClsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strIdentifyLog As String = strNroPedido
        Dim PdV_Sinergia As String = Funciones.CheckStr(ConsultaPuntoVenta(Session("ALMACEN")))
        Dim objActi As New COM_SIC_Activaciones.ClsActivacionCBIO

        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[VALIDA ANULACION COMBO - CONSULTA MSSAP => NROPEDIDO: ] " & strNroPedido)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[VALIDA ANULACION COMBO - CONSULTA MSSAP => PDV_SINERGIA: ] " & PdV_Sinergia)

            dsPedido = objClsConsultaMsSap.ConsultaPedido(strNroPedido, PdV_Sinergia, "")

            If dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPostpago") Then
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INICIA VALIDACION ANULACION PAGO COMBO - POSTPAGO]")
                objActi.ValidaAnulacionComboPostpago(strNroPedido, codRespuesta, msjRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULACION PAGO COMBO POSTPAGO] Codigo Respuesta:" & codRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULACION PAGO COMBO POSTPAGO] Descripción Respuesta: " & msjRespuesta)
            ElseIf dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[INICIA VALIDACION ANULACION PAGO COMBO - PREPAGO]")
                objActi.ValidaAnulacionComboPrepago(strNroPedido, codRespuesta, msjRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULACION PAGO COMBO PREPAGO] Codigo Respuesta:" & codRespuesta)
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ANULACION PAGO COMBO PREPAGO] Descripción Respuesta: " & msjRespuesta)
            End If

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "[ERROR VALIDACION ANULACION PAGO COMBO]")
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
    End Sub
    'INICIATIVA-710 - FIN

#Region "Anulacion Renovacion Corporativa"

    Public Sub AnularVentaRenovCorp(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String)

        Dim oDatos As New COM_SIC_Cajas.clsCajas
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionCorporativa"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "---------------Inicio Anulacion Venta Renovacion Pool Pagos-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Pedido : " & p_nro_pedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Tipo Venta : " & p_tipo_renov)

            Dim blnExito As Boolean = oDatos.FP_AnularVentaRenovCorp(p_nro_pedido, p_tipo_renov)

            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Respuesta : " & blnExito)

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularVentaRenovCorp)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126           
        Finally
            oDatos = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------Fin Anulacion Venta Renovacion--------------")
        End Try
    End Sub

#End Region

    Private Function Valida_Anula_Prom(ByVal strNroSap As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.validaAnulaVtaProm(strNroSap)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "');</script>")
        End Try
        Return intResult
    End Function

    Private Function Anula_Vta_Prom(ByVal strNroSap As String, ByVal strEstado As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.AnulaVtaProm(strNroSap, strEstado)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "');</script>")
        End Try
        Return intResult
    End Function

    Private Function AnularSot(ByVal nroSot As String, ByVal nroSec As String, ByVal nroDocSap As String) As String

        Dim oTransaccion As New SGATransaction
        Dim idLog As String = nroDocSap & " - " & nroSot
        Dim oSGAResponseTrs As New SGAResponseVenta
        oSGAResponseTrs.codRepuesta = -1
        Dim observacion As String = "Anulación Automática SICAR."
        Try

            Dim oAudit As New ItemGenerico
            oAudit.CODIGO = nroSot
            oAudit.DESCRIPCION = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            oAudit.DESCRIPCION2 = CurrentTerminal

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio AnularSot")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "WS    : " & ConfigurationSettings.AppSettings("constSGATransaccion_Url"))
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Method: " & "anularSot")
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nroSot: " & nroSot)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nroSec: " & nroSec)

            If CheckStr(nroSot) = "" Or nroSot = "0" Then
                nroSot = "0"
                observacion = nroSec
                oSGAResponseTrs = oTransaccion.AnularSot(nroSot, observacion, oAudit)
            Else
                oSGAResponseTrs = oTransaccion.AnularSot(nroSot, observacion, oAudit)
            End If

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codResp: " & oSGAResponseTrs.codRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codMsg: " & oSGAResponseTrs.msgRepuesta)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin AnularSot")

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR AnularSot: " & ex.Message.ToString())
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR AnularSot: " & ex.StackTrace.ToString())
            Throw New Exception("No se pudo generar la SOT.")
        End Try
        Return oSGAResponseTrs.codRepuesta
    End Function

#Region "Puntos Claro Club"
    Public Sub LiberarPuntosCC(ByVal strDocSap As String, _
                               ByVal ID_CCLUB As String, _
                               ByVal NroDocumento As String)
        Dim idLog As String = strDocSap

        Try
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio LiberarPuntosCC()")
            Dim tipoClie As String
            Dim objPuntosClaroClubNegocio As New clsPuntosClaroClub
            Dim txId As String
            Dim errorCode As String
            Dim errorMsg As String
            tipoClie = ConfigurationSettings.AppSettings("consTipoclie")

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio liberarPuntos(): " & ConfigurationSettings.AppSettings("WSGestionarPuntosCC_url"))
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp ID_CCLUB: " & ID_CCLUB)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp NroDocumento: " & NroDocumento)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp tipoClie: " & tipoClie)

            objPuntosClaroClubNegocio.liberarPuntos(ID_CCLUB, _
                                                    NroDocumento, _
                                                    tipoClie, _
                                                    txId, _
                                                    errorCode, _
                                                    errorMsg)

            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out txId:" & txId)

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LiberarPuntosCC)"
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out errorCode:" & errorCode & MaptPath)
            'FIN PROY-140126
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out errorMsg:" & errorMsg)
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin liberarPuntos()")
            If errorCode <> "0" Then
                Throw New Exception(errorMsg)
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LiberarPuntosCC)"
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR LiberarPuntosCC(): " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126           
        Finally
            objLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin LiberarPuntosCC(): ")
        End Try
    End Sub

    Public Function TieneReserva(ByVal k_tipo_doc As String, _
                                 ByVal k_num_doc As String, _
                                 ByVal strIdentifyLog As String) As Boolean
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim k_tipo_clie As String = ConfigurationSettings.AppSettings("consClienteWS")
        Dim k_tipo_doc2 As String
        Dim k_estado As String
        Dim k_coderror As Double
        Dim k_descerror As String
        Dim bTieneReserva As Boolean = False
        Dim constVALBLOQUEOBOLSA As String = ConfigurationSettings.AppSettings("constVALBLOQUEOBOLSA")
        Dim idLog As String

        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio TieneReserva()")

            objCajas.ValidaBloqueoBolsa(k_tipo_doc, _
                                        k_num_doc, _
                                        k_tipo_clie, _
                                        k_tipo_doc2, _
                                        k_estado, _
                                        k_coderror, _
                                        k_descerror)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_tipo_doc2:" & k_tipo_doc2)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_estado:" & k_estado)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_coderror:" & k_coderror)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_descerror:" & k_descerror)

            If k_coderror = 0.0 Then
                If constVALBLOQUEOBOLSA = k_estado Then
                    bTieneReserva = True
                End If
            End If
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "bTieneReserva:" & bTieneReserva)

            Return bTieneReserva
        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR TieneReserva(): " & ex.Message.ToString())
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR TieneReserva(): " & ex.StackTrace.ToString())

            Return False
        Finally
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin TieneReserva()")
        End Try
    End Function

    Public Function ExisteCanjePuntosClaroClub(ByVal NroDocSap As String, _
                                               ByRef ID_CCLUB As String, _
                                               ByRef NroDocumento As String) As Boolean
        Dim bExiste As Boolean = False
        Dim strIdentifyLog As String = NroDocSap
        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ExisteCanjePuntosClaroClub()-----------")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dt As DataTable
            Dim CONS_FLAG_CANJE As String = ConfigurationSettings.AppSettings("CONS_FLAG_CANJE")
            dt = objCajas.ListarCanjePuntos(NroDocSap)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If dr.Item("FLAG_CANJE").ToString <> CONS_FLAG_CANJE Then
                    bExiste = True
                End If
                ID_CCLUB = Funciones.CheckStr(dr.Item("ID_CCLUB"))
                NroDocumento = Funciones.CheckStr(dr.Item("NUM_DOC"))
            End If

            Return bExiste
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ExisteCanjePuntosClaroClub)"
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT ERROR : " & ex.StackTrace.ToString())

            Return bExiste
        Finally
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ExisteCanjePuntosClaroClub()-----------")
        End Try
    End Function
#End Region

    ' Fin PS - Automatización de canje y nota de crédito
    ' Fin E77568
    Private Sub Compensar()
        Dim dtSap2 As New DataTable
        dtSap2 = CType(Session("dtsap"), DataTable)
        'Dim dvPagos As New DataView(dtsap)
        Dim dvPagos As New DataView(dtSap2)
        'MODIFICADO POR FFS FIN
        If Len(Trim(Request.Item("rbPagos"))) > 0 Then
            dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
            drFila = dvPagos.Item(0).Row
            Session("DocSel") = drFila
            Response.Redirect("compensacion.aspx")
        End If
    End Sub

    Private Function AnularReservaBAM(ByVal NroDocSap As String) As Boolean
        Dim proceso As Boolean = False
        Dim oBusExpress As New ClsActivacionPel
        Dim oBusExpressPel As New COM_SIC_Cajas.clsCajas

        Dim sDocumentoSap As String = ""
        Dim strOficina As String = ""
        Dim strCodigoResp As String = ""
        Dim strDescripcionResp As String = ""
        Dim strOpcion As String = ""
        Dim strComercioBAM As String = ""
        Dim bolAnulacion As Boolean = True
        Dim strIdentifyLog As String = NroDocSap

        Dim i As Int16 = 1


        strOficina = CStr(Session("Almacen"))
        sDocumentoSap = NroDocSap
        Dim numerosTelefono As String = ""
        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROCESO ANULACION RESERVA BAM")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")

            strComercioBAM = ConfigurationSettings.AppSettings("constComercioBAM")


            Dim lista As ArrayList = oBusExpress.Lis_Lista_Detalle_Venta_Prepago(sDocumentoSap, strCodigoResp)

            For Each item As DetalleVentaPrepago In lista
                i = i + 1
                If item.COD_PROD_PREP <> ConfigurationSettings.AppSettings("constCodProdPrepagoJuerga") Then
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "******************************************")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PROCESO ANULACION RESERVA " & item.DESCRIPCION_PRODUCTO & " >> ITEM - i :" + i.ToString())
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "******************************************")
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Documento: " & NroDocSap)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Almacen: " & strOficina)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Comercio: " & strComercioBAM)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo TXN: " & item.COD_TXN)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ICCID: " & item.SERIE_CHIP)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IMEI: " & item.SERIE_EQUI)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "MSISDN: " & item.LINEA)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "PDV: " & item.COD_PDV_PEL)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Vendedor: " & item.COD_VEN_PEL)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Número Operación: " & item.NUM_OPER)

                    bolAnulacion = oBusExpressPel.AnulacionReserva(strComercioBAM, item.COD_TXN, item.SERIE_CHIP, item.SERIE_EQUI, item.LINEA, item.COD_PDV_PEL, item.COD_VEN_PEL, item.NUM_OPER, strCodigoResp, strDescripcionResp)

                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Respuesta:" & strCodigoResp)
                    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Descripción Respuesta: " & strDescripcionResp)


                End If

            Next


            'numerosTelefono = numerosTelefono
            ' RegisterStartupScript("script", "<script>" & numerosTelefono & "</script>")

        Catch ex As Exception
            bolAnulacion = False
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error oBusExpress.AnulacionReserva() :" + ex.Message)
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")
        Finally
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin oBusExpress.AnulacionReserva() :")
            objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************")
        End Try

        Return bolAnulacion
    End Function

    Private Function AnulaCanjeMSSAP(ByVal nroPedido As String) As Boolean

        Dim objclsConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim objTrsConsultaMsSap As New COM_SIC_Activaciones.clsTrsMsSap

        Dim flagAnularPedido As String = ConfigurationSettings.AppSettings("strFlagAnularPedido")
        Dim estadoPendienteCanje As String = ConfigurationSettings.AppSettings("idPendienteCanje")
        Dim estadoExitoCanje As String = ConfigurationSettings.AppSettings("idExitoCanje")
        Dim escenarioAnuladoCanje As String = ConfigurationSettings.AppSettings("idEscenarioAnu")
        Dim estadoNotaCredito As String = ConfigurationSettings.AppSettings("strEstadoNotaCredito")
        Dim strFlagAccion As String = ConfigurationSettings.AppSettings("strAccionAnularSerieCanje")

        Dim blnDocCanje As Boolean
        Dim dtConsultaCanje As DataTable
        Dim K_CODLOG, K_DESLOG As String
        Dim strIdentifyLog As String = nroPedido

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     =========================================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     == INICIO CAMBIO DE ESTADO DE NOTA DE CREDITO / CANJE ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     =========================================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [INICIO][ConsultaCanje][IN NRO PEDIDO: " & nroPedido & "]")

            dtConsultaCanje = objclsConsultaMsSap.ConsultaCanje(nroPedido, K_CODLOG, K_DESLOG)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [FIN][ConsultaCanje][OUT CODLOG: " & K_CODLOG & "][OUT DESLOG: " & K_DESLOG & "]")

            If Not IsNothing(dtConsultaCanje) AndAlso dtConsultaCanje.Rows.Count > 0 Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [ESTADO CANJE EQUIPO: " & Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) & "]")

                If (Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoPendienteCanje) Or _
                   (Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoNotaCredito) Or _
                    (Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESTADO")) = estadoExitoCanje And _
                    Funciones.CheckStr(dtConsultaCanje.Rows(0).Item("CANJV_ESCENARIO")) = escenarioAnuladoCanje) Then

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [INICIO][ActualizarEstadoNotaCanje][IN NRO PEDIDO: " & nroPedido & "][IN FLAG: " & flagAnularPedido & "]")
                    Dim actualizaEstado As String = objTrsConsultaMsSap.ActualizarEstadoNotaCanje(nroPedido, flagAnularPedido, K_CODLOG, K_DESLOG)
                    If K_CODLOG = "0" And K_DESLOG = "OK" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         Se Realizó Correctamente el cambio de estado del Canje")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "             [INICIO ActualizaEstadoSerieCanje][IN NRO PEDIDO: " & nroPedido & "][IN FLAG: " & strFlagAccion & "]")
                        Dim strActualizaSerieCanje = objTrsConsultaMsSap.ActualizaEstadoSerieCanje(Funciones.CheckInt64(nroPedido), strFlagAccion, K_CODLOG, K_DESLOG)
                        If K_CODLOG = "0" And K_DESLOG = "OK" Then
                        blnDocCanje = True
                    End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "             [FIN][ActualizaEstadoSerieCanje][OUT CODLOG: " & K_CODLOG & "][OUT DESLOG: " & K_DESLOG & "][OUT strActualizaSerieCanje: " & strActualizaSerieCanje & "]")
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         [FIN][ActualizarEstadoNotaCanje][OUT CODLOG: " & K_CODLOG & "][OUT DESLOG: " & K_DESLOG & "][OUT actualizaEstado: " & actualizaEstado & "]")

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje en SAP]")
                    'Session("msgDevolucion") = ConfigurationSettings.AppSettings("msgNoExisteCanjeSAP")
                    blnDocCanje = False
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [No se encontró registro del Canje]")
                'Session("msgDevolucion") = ConfigurationSettings.AppSettings("msgNoExisteCanjeBD")
                blnDocCanje = False
            End If

        Catch ex As Exception
            blnDocCanje = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     [Error - PagarRegistroCanje]" & ex.Message.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     =========================================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     === FIN CAMBIO DE ESTADO DE NOTA DE CREDITO / CANJE ===")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     =========================================================")
        End Try

        Return blnDocCanje
    End Function

    'PROY-23111-IDEA-29841 - INICIO
    Private Function AnularVentaAccesorios(ByVal nroPedido As String, ByVal strUsuario As String) As Boolean

        Dim objClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
        Dim strIdentifyLog As String = nroPedido
        Dim strEstadoVentaAccesorio As String = ConfigurationSettings.AppSettings("strEstadoVentaAcceAnulada")
        Dim strCodRpta As String = ""
        Dim strMsgRtpa As String = ""

        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    ===== INICIO ANULACION VENTAS VARIAS =====")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [AnularVentaAccesorios][SP: SISACT_PKG_MANT_PROMO_ACC.SISACTSU_RELACION_DETA_VTA_ACC]")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [IN NRO PEDIDO : " & nroPedido & "]")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [IN ESTADO : " & strEstadoVentaAccesorio & "]")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [IN USUARIO : " & strUsuario & "]")
        objClsTrsPvu.AnularVentaAccesorios(nroPedido, strEstadoVentaAccesorio, strUsuario, strCodRpta, strMsgRtpa)
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [OUT COD RPTA : " & strCodRpta & "]")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    [OUT MSG RPTA : " & strMsgRtpa & "]")
        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    ===== FIN ANULACION VENTAS VARIAS =====")

    End Function
    'PROY-23111-IDEA-29841 - FIN

  'ADD_INI_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO
    Private Function DesasociacionLinea(ByVal sDocSAP As String, ByVal strLinea As String, ByVal strSerie As String) As String
        Dim strItReturnAsociacion As String = String.Empty
        Dim strCodRpt As String = String.Empty
        Dim strMsgResp As String = String.Empty
        Dim strItReturn As String = String.Empty
        Dim strUsuarioSesion As String = CheckStr(CurrentUser)
        Dim BLSans As New NEGOCIO_SIC_SANS.SansNegocio
        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:DesasociacionLinea|[INPUT].[Usuario Sesión]: " + strUsuarioSesion)
            strCodRpt = BLSans.desasociarNroTelefSerie(strLinea, strSerie, strUsuarioSesion, strMsgResp, strItReturn)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:DesasociacionLinea|[---------- Respuesta del Servicio ----------]")
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:DesasociacionLinea|[OUTPUT].[Código Respuesta]: " + strCodRpt)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:DesasociacionLinea|[OUTPUT].[Mensaje Respuesta]: " + strMsgResp)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:DesasociacionLinea|[OUTPUT].[Mensaje It Return]: " + strItReturn)

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & "ERROR|PROY-24388 - IDEA-31791|Método:DesasociacionLinea|Mensaje: " + ex.Message)
     End Try
    End Function
    'FIN_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO
    'ADD_INI_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO
    Private Function CambiarEstadoSans(ByVal sDocSAP As String, ByVal strLinea As String, ByVal StrCodEstado As String) As String
        Dim strCodRpt As String = String.Empty
        Dim strMsgResp As String = String.Empty
        Dim strItReturn As String = String.Empty
        Dim strUsuarioSesion As String = CheckStr(CurrentUser)
        Dim BLSans As New NEGOCIO_SIC_SANS.SansNegocio
        Try
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:cambiarStatus|[INPUT].[Usuario Sesión]: " + strUsuarioSesion)
            strCodRpt = BLSans.Cambiar_Status(strLinea, StrCodEstado, strUsuarioSesion, strMsgResp, strItReturn)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:cambiarStatus|[---------- Respuesta del Servicio ----------]")
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:cambiarStatus|[OUTPUT].[Código Respuesta]: " + strCodRpt)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:cambiarStatus|[OUTPUT].[Mensaje Respuesta]: " + strMsgResp)
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & ": PROY-24388 - IDEA-31791|Método:cambiarStatus|[OUTPUT].[Mensaje It Return]: " + strItReturn)

        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, strLinea & "- " & sDocSAP & "ERROR|PROY-24388 - IDEA-31791|Método:cambiarStatus|Mensaje: " + ex.Message)
        End Try

    End Function
    'FIN_PROY_24388_RESERVA_Y_ACTIVACIÓN_EN_LÍNEA_PREPAGO_CAC_Y_POSTPAGO

  'PROY-24724-IDEA-28174 - INI
    Private Function AnularProteccionMovil(ByVal strNroPedido As String, ByVal strNroPedidoEquipo As String) As Boolean
        'Objetos
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - NumeroPedido:" & Funciones.CheckStr(strNroPedido))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - NumeroPedidoEquipo:" & Funciones.CheckStr(strNroPedidoEquipo))

        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA objProteccionMovil")
        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA objWSProteccionMovil")
        Dim objWSProteccionMovil As New COM_SIC_Activaciones.BWGestionaProteccionMovil
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA objEnvioCorreoWS")
        Dim objEnvioCorreoWS As New COM_SIC_Activaciones.BWEnvioCorreo
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strIdentifyLog")
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("USUARIO")) & " - " & strNroPedido
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strIdentifyLog:" & strIdentifyLog)
        'Variables
        Dim strNroSEC As String
        'Variables para Eliminar 
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strCodServicioPM")
        Dim strCodServicio As String = clsKeyAPP.strCodServicioPM 'PROY-24724-IDEA-28174 -  PARAMETROS  //3354
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strCodServicio:" & strCodServicio)
        'Variables de control
        Dim strCodRpta As String
        Dim strMsgRpta As String
        Dim blnErrorEliminarProteccionMovil As Boolean = False
        Dim blnErrorEliminarProteccionMovilWS As Boolean = False
        'Variables de Envio de Correo
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strRemitentePM")
        Dim strRemitente As String = clsKeyAPP.strRemitentePM
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strRemitente:" & strRemitente)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strDestinatarioPM")
        Dim strDestinatario As String = clsKeyAPP.strDestinatarioPM
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strDestinatario:" & strDestinatario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strMensajeAnulacionPM")
        Dim strMensaje As String = clsKeyAPP.strMensajeAnulacionPM
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strMensaje:" & strMensaje)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strAsuntoPM")
        Dim strAsunto As String = clsKeyAPP.strAsuntoPM
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strAsunto:" & strAsunto)
        Dim arrAnulacion(3) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA arrAsunto")
        Dim arrAsunto(0) As String
        arrAsunto(0) = "Anulación"
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE HACE STRING FORMAT A strAsunto")
        strAsunto = String.Format(strAsunto, arrAsunto)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strAsunto:" & strAsunto)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strFlagCorreoPM")
        Dim strFlag As String = clsKeyAPP.strFlagCorreoPM
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strFlag:" & strFlag)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA strUsuarioAplicacion")
        Dim strUsuarioAplicacion = ConfigurationSettings.AppSettings("Usuario_Aplicacion")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - strUsuarioAplicacion:" & Funciones.CheckStr(strUsuarioAplicacion))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002133882 - SE INICIALIZA dsProteccionMovil")
		Dim dsProteccionMovil As DataSet
        Dim strCodRptaPM As String
        Dim strMsgRptaPM As String
        Dim strNroCertificado As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Obtener SEC EQUIPO - INICIO ==")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN NRO PEDIDO EQUIPO: " & strNroPedidoEquipo)
        strNroSEC = ConsultarSECEquipo(strNroPedidoEquipo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Obtener SEC EQUIPO - FIN ==")

        If Not strNroSEC.Equals("") Then
            
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== ConsultarDatosPM - INICIO ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== SP: PKG_TRANS_ASURION.SISACTSS_CONSULTAR_SEGURO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== IN NRO PEDIDO EQUIPO: " & strNroPedidoEquipo)

            dsProteccionMovil = objProteccionMovil.ConsultarProteccionMovil(strNroPedidoEquipo, strCodRptaPM, strMsgRptaPM)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== OUT COD RPTA: " & strCodRptaPM)
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularProteccionMovil)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== OUT MSG RPTA: " & strMsgRptaPM & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== ConsultarDatosPM - FIN    ==")

            If strCodRptaPM = "0" AndAlso Not dsProteccionMovil Is Nothing AndAlso dsProteccionMovil.Tables.Count > 0 AndAlso dsProteccionMovil.Tables(0).Rows.Count > 0 Then
                strNroCertificado = Funciones.CheckStr(dsProteccionMovil.Tables(0).Rows(0).Item("EVALV_NRO_CERTF_RPTA"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== strNroCertificado: " & strNroCertificado)
            Else
                strNroCertificado = ""
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== No hay datos de PM para el pedido: " & strNroPedidoEquipo)
            End If
			
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     EliminarServicioPM - INICIO ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     PKG_TRANS_ASURION.SISACTSD_BORRAR_SERV_VENTA ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN NRO SEC: " & strNroSEC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN COD SERVICIO: " & strCodServicio)
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Cantidad de Proteccion Movil: " & dsProteccionMovil.Tables(0).Rows.Count)
			
            'CAMBIAR
            If (dsProteccionMovil.Tables(0).Rows.Count = 1) Then
            Dim intNroSEC As Integer = Funciones.CheckInt64(strNroSEC)
            objProteccionMovil.EliminarServicioProteccionMovil(intNroSEC, strCodServicio, strCodRpta, strMsgRpta)
			End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT COD RPTA: " & strCodRpta)
            'INI PROY-140126
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularProteccionMovil/)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT MSG RPTA: " & strMsgRpta & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     EliminarServicioPM - FIN ==")

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     EliminarProteccionMovil - INICIO ==")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     WS: " & ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_URL"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Método: eliminarPrima ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN NRO SEC: " & strNroSEC)

                objWSProteccionMovil.EliminarProteccionMovil(strNroSEC, strNroCertificado, CurrentUser, CurrentTerminal, strCodRpta, strMsgRpta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT COD RPTA: " & strCodRpta)
                'INI PROY-140126               
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularProteccionMovil/)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT MSG RPTA: " & strMsgRpta & MaptPath)
                'FIN PROY-140126

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ElimiarProteccionMovil - FIN ==")

                If strCodRpta <> "0" Then
                    blnErrorEliminarProteccionMovil = True
                End If

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== ERROR - Eliminar Protección Móvil:")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & ConfigurationSettings.AppSettings("consGestionaProteccionMovilWS_Error"))
                'INI PROY-140126
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularProteccionMovil)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Mensaje Exception: " & ex.Message.ToString() & MaptPath)
                'FIN PROY-140126 
                blnErrorEliminarProteccionMovil = True
            Finally
                If blnErrorEliminarProteccionMovil Then
                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     EliminarEvaluacionPM - INICIO ==")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     SPKG_TRANS_ASURION.SISACTSU_ELIMINA_EVAL_SEGURO") 'PROY-24724 - Iteracion 2 Siniestros
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         IN NRO SEC: " & strNroSEC)

                        objProteccionMovil.EliminarEvaluacionProteccionMovil(strNroSEC, strCodRpta, strMsgRpta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT COD RPTA: " & strCodRpta)
                        'INI PROY-140126                       
                        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                        MaptPath = "( Class : " & MaptPath & "; Function: AnularProteccionMovil)"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         OUT MSG RPTA: " & strMsgRpta & MaptPath)
                        'FIN PROY-140126


                        If strCodRpta.Equals("0") Then
                            blnErrorEliminarProteccionMovilWS = False
                        Else
                            blnErrorEliminarProteccionMovilWS = True
                        End If
                    Catch e As Exception
                        blnErrorEliminarProteccionMovilWS = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ERROR - EliminarEvaluacionPM" & e.Message.ToString())
                    Finally
                        If blnErrorEliminarProteccionMovilWS Then
                            Try
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - INICIO ==")
                                arrAnulacion(0) = " - Paquete: PKG_TRANS_ASURION "
                                arrAnulacion(1) = " - SP: SISACTSU_ELIMINA_EVAL_SEGURO"
                                arrAnulacion(2) = "   1. SEC: " & strNroSEC
                                strMensaje = strMensaje.Replace("*", "<br>")
                                strMensaje = String.Format(strMensaje, arrAnulacion)
                                objEnvioCorreoWS.EnviarCorreoPM(strRemitente, strDestinatario, strAsunto, strMensaje, _
                                                                strFlag, CurrentTerminal, CurrentUser, "", "")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - FIN ==")
                            Catch x As Exception
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      ERROR - EnviarCorreoPM: " & x.Message.ToString())
                            End Try
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     EliminarEvaluacionPM - FIN ==")
                    End Try

                End If
            End Try
        End If

        Return True
    End Function

    Public Function ConsultarSECEquipo(ByVal strNroPedido As String) As String

        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("USUARIO")) & " - " & strNroPedido
        Dim dsContrato As DataSet
        Dim dsAcuerdo As DataSet
        Dim strNroContrato As String
        Dim strNroSEC As String = ""
        Dim strCodRptaSEC, strMsgRptaSEC As String

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         [INICIO][Consulta Contraro][IN Pedido: " & strNroPedido & "]")
        dsContrato = objClsConsultaPvu.ObtenerDrsap(strNroPedido, strCodRptaSEC, strMsgRptaSEC)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         [FIN][Consulta Contraro][OUT COD: " & strCodRptaSEC & "][OUT MSG: " & strMsgRptaSEC & "]")

        If Not dsContrato Is Nothing AndAlso dsContrato.Tables.Count > 0 AndAlso dsContrato.Tables(0).Rows.Count > 0 Then

            strNroContrato = Funciones.CheckStr2(dsContrato.Tables(0).Rows(0).Item("ID_CONTRATO"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         [INICIO][Consulta Acuerdo][IN Contrato: " & strNroContrato & "]")
            dsAcuerdo = objClsConsultaPvu.ConsultaAcuerdoPCS(Funciones.CheckInt64(strNroContrato), DBNull.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         [FIN][Consulta Acuerdo]")

            If Not dsAcuerdo Is Nothing AndAlso dsAcuerdo.Tables.Count > 0 AndAlso dsAcuerdo.Tables(0).Rows.Count > 0 Then
                strNroSEC = Funciones.CheckStr(dsAcuerdo.Tables(0).Rows(0).Item("contn_numero_sec"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==         [SEC: " & strNroSEC & "]")
            End If
        End If

        Return strNroSEC
    End Function
      
    Public Function AnularPedidoCanjeConProteccionMovil(ByVal strNroPedidoPM As Int64)
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("USUARIO")) & " - " & strNroPedidoPM
        Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
        Dim objEnvioCorreoWS As New COM_SIC_Activaciones.BWEnvioCorreo

        ''ConsultaPedidoOrigen
        Dim strNroPedidoEquipo As Int64
        Dim strSerieEquipoOrigen As String = String.Empty
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        ''ConsultarSEC
        Dim strNroSEC As String = String.Empty
        ''Mostrar
        Dim dsMostrarPM As DataSet
        'AnulaPedidoCanje
        Dim strCodCanje As String = "C"
        Dim strSoplnCodigo As String = String.Empty
        Dim dtCanjeEquipo As DataTable
        Dim blExitoAnula As Boolean = False
        'Variables de Envio de Correo
        Dim strRemitente As String = clsKeyAPP.strRemitentePM
        Dim strDestinatario As String = clsKeyAPP.strDestinatarioPM
        Dim strMensaje As String = clsKeyAPP.strMensajeAnulacionPM
        Dim strAsunto As String = clsKeyAPP.strAsuntoPM
        Dim arrAnulacion(3) As String
        Dim arrAsunto(0) As String
        arrAsunto(0) = "Anulación"
        strAsunto = String.Format(strAsunto, arrAsunto)
        Dim strFlag As String = clsKeyAPP.strFlagCorreoPM
        Dim strUsuarioAplicacion = ConfigurationSettings.AppSettings("Usuario_Aplicacion")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   CONSULTARPEDIDOORIGEN - INICIO ==")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   PKG_CONSULTA.SSAPSS_CONSULTAPEDIDOORIGEN")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     IN  NRO DOC: " & strNroPedidoPM)

        dtCanjeEquipo = objProteccionMovil.ConsultarPedidoOrigen(strNroPedidoPM, strNroPedidoEquipo, strCodRpta, strMsgRpta)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT NRO. PED. EQ. ORIGEN:" & strNroPedidoEquipo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT  COD RPTA: " & strCodRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT  MSJ RPTA: " & strMsgRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   CONSULTARPEDIDOORIGEN - F I N  ==")

        If Not dtCanjeEquipo Is Nothing AndAlso strCodRpta = "0" AndAlso dtCanjeEquipo.Rows.Count > 0 Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     OBTENER SEC EQUIPO - INICIO ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==         IN NRO PEDIDO EQUIPO: " & strNroPedidoEquipo)

            strNroSEC = ConsultarSECEquipo(strNroPedidoEquipo)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==         OUT: " & strNroSEC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     OBTENER SEC EQUIPO - FIN ==")

            If Not strNroSEC.Equals("") Then
                strSerieEquipoOrigen = Funciones.CheckStr(dtCanjeEquipo.Rows(0).Item("CANJV_ENT_SERIE_MAT"))

                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==   MOSTRARDATOSPROTECCIONMOVIL - INICIO ==")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==   PKG_TRANS_ASURION.SISASS_CONSU_EVAL_SEGU")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     IN  NRO SEC: " & strNroSEC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     IN  NRO Serie Equi. Origen: " & strSerieEquipoOrigen)

                dsMostrarPM = objProteccionMovil.MostrarDatosProteccionMovil(strNroSEC, strSerieEquipoOrigen, strCodRpta, strMsgRpta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     OUT COD RPTA: " & strCodRpta)
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularPedidoCanjeConProtecionMovil)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==     OUT MSJ RPTA: " & strMsgRpta & MaptPath)
                'FIN PROY-140126 

                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==   MOSTRARDATOSPROTECCIONMOVIL - F I N ==")
                If Not dsMostrarPM Is Nothing AndAlso strCodRpta = "0" AndAlso dsMostrarPM.Tables(0).Rows.Count > 0 Then  ''SI obtiene datos sí se trata de un canje con pm

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==============================================")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularCanjeProteccionMovil :: INI  ==")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==============================================")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   PKG_TRANS_ASURION.SISASU_ANULA_CANJE ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     IN SEC: " & strNroSEC)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     IN SERIE EQUI. ORIGEN: " & strSerieEquipoOrigen)

                    objProteccionMovil.AnularCanjeProteccionMovil(strNroSEC, strSerieEquipoOrigen, strCodRpta, strMsgRpta)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT CODRPTA: " & strCodRpta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     OUT MSJRPTA: " & strMsgRpta)

                    If strCodRpta = 0 Then
                        blExitoAnula = True
                    End If

                    If blExitoAnula Then
                        Try
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - INICIO ==")
                            'PROY-24724-IDEA-28174 - INI PARAMETROS
                            arrAnulacion(0) = " - Paquete: PKG_TRANS_ASURION "
                            arrAnulacion(1) = " - SP: SISACTSU_ELIMINA_EVAL_SEGURO"
                            arrAnulacion(2) = "   1. SEC: " & strNroSEC
                            strMensaje = strMensaje.Replace("*", "<br>")
                            strMensaje = String.Format(strMensaje, arrAnulacion)
                            ''PROY-24724-IDEA-28174 - FIN PARAMETROS

                            objEnvioCorreoWS.EnviarCorreoPM(strRemitente, strDestinatario, strAsunto, strMensaje, _
                                                            strFlag, CurrentTerminal, CurrentUser, "", "")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - FIN ==")
                        Catch x As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      ERROR - EnviarCorreoPM: " & x.Message.ToString())
                        End Try
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==============================================")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularCanjeProteccionMovil :: INI  ==")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==============================================")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==   No se obtuvo datos MostrarDatosProteccionMovil ==")
                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "== No se encontro el numero de SEC ==")
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroPedidoPM & "- " & "==   No se obtuvo Datos del Pedido Origen ==")
        End If
    End Function
    'PROY-24724-IDEA-28174 - FIN
    'PROY-24724 - Iteracion 2 Siniestros -INI
    Public Function AnularPedidoSiniestro(ByVal strTelefono As String)

        Dim strIdentifyLog As String = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))
        Dim strCodRspta As String = String.Empty
        Dim strMsgRspta As String = String.Empty
        Dim listaResponse As New ArrayList
        Dim objGestionaPostventa As New COM_SIC_Activaciones.BWGestionaPostventaProteccionMovil
        Dim strMetodo As String = String.Empty
        Dim strEstadPendienteSini As String = Funciones.CheckStr(clsKeyAPP.strEstdPendienteSiniestro).Split(";")(0)
        Dim strEstadAprobadoSini As String = Funciones.CheckStr(clsKeyAPP.strEstdAprobadoSiniestro).Split(";")(0)
        Dim bolErrorAnulacionPedidoSini As Boolean = False
        Dim strIdTransaccion As String = strTelefono & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        'Correo 
        Dim objEnvioCorreoWS As New COM_SIC_Activaciones.BWEnvioCorreo
        Dim strMensaje As String = clsKeyAPP.strMensajeSiniestro
        Dim arrAnulacion(18) As String
        Dim strFlag As String = clsKeyAPP.strFlagCorreoPM
        Dim strRemitente As String = clsKeyAPP.strRemitentePM
        Dim strDestinatario As String = clsKeyAPP.strDestinatarioPM
        Dim strAsunto As String = clsKeyAPP.strAsuntoSiniestro
        Dim arrAsunto(0) As String
        arrAsunto(0) = "ANULACION"
        strAsunto = String.Format(strAsunto, arrAsunto)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularPedidoSiniestro :: INI  ==")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")

        Try
            strMetodo = "ObtenerDetalleSiniestro"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     MostrarDetalleSiniestro -  INICIO ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     WS: " & ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Método: ObtenerDetalleSiniestro")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  IdTransaccion: " & strIdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Teléfono: " & strTelefono)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Estado: " & strEstadPendienteSini)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Usuario: " & CurrentUser)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Terminal: " & CurrentTerminal)

            objGestionaPostventa.MostrarDetalleSiniestro(strIdTransaccion, strTelefono, strEstadPendienteSini, CurrentUser, CurrentTerminal, strCodRspta, strMsgRspta, listaResponse)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       OUT Cod. Rpta : " & strCodRspta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       OUT Msj. Rpta : " & strMsgRspta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       OUT ListaResponse.Count: : " & listaResponse.Count)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     MostrarDetalleSiniestro -  FIN ==")

            If strCodRspta.Equals("0") Then
                strMetodo = "ActualizarEstadoPagoSiniestro"
                strIdTransaccion = strTelefono & DateTime.Now.ToString("yyyyMMddHHmmssfff")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ActualizarEstadoPagoSiniestro -  INICIO ==")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     WS: " & ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Método: actualizarEstadoPagoSiniestro ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  IdTransaccion: " & strIdTransaccion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  NroCertificado: " & Funciones.CheckStr(listaResponse(0).nroCertif))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  CustomerId: " & Funciones.CheckStr(listaResponse(0).customerId))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  NroSiniestro: " & Funciones.CheckStr(listaResponse(0).nroSiniestro))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Estado Siniestro: " & strEstadAprobadoSini)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       IN  Teléfono: " & Funciones.CheckStr(listaResponse(0).nroTelefono))

                objGestionaPostventa.ActualizarEstadoPagoSiniestro(listaResponse, strIdTransaccion, strEstadAprobadoSini, CurrentUser, CurrentTerminal, strCodRspta, strMsgRspta)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       OUT Cod. Rpta : " & strCodRspta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==       OUT Msj. Rpta : " & strMsgRspta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     ActualizarEstadoPagoSiniestro -   FIN    ==")

                If strCodRspta.Equals("0") Then
                    bolErrorAnulacionPedidoSini = True
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==  Ocurrió un error al ActualizarEstadoPagoSiniestro==")
                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==  No se encontraron datos de Siniestro ==")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==     Mensaje  : " & "strMsgRspta")
            End If

        Catch ex As Exception
            bolErrorAnulacionPedidoSini = False
            strMsgRspta = ex.Message.ToString()
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== ERROR " & strMetodo)
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularPedidoSiniestro)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== Mensaje Exception: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126


        Finally
            If bolErrorAnulacionPedidoSini = False Then
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - INICIO ==")
                    arrAnulacion(0) = "la anulación"
                    arrAnulacion(1) = "WS GestionaPostventaProteccionMovil: " & ConfigurationSettings.AppSettings("consGestionaPostventaProteccionMovilWS_URL")
                    arrAnulacion(2) = "Error Reportado en el Método: " & strMetodo & " Nº Pedido: " & strIdentifyLog & " Descripción de Error: " & strMsgRspta
                    arrAnulacion(3) = "Regularizar Ejecutando Primer Método: obtenerDetalleSiniestro "
                    arrAnulacion(4) = " 1. nroTelefono: " & strTelefono
                    arrAnulacion(5) = " 2. estado: " & strEstadPendienteSini
                    arrAnulacion(6) = "Segundo Método: actualizarEstadoPagoSiniestro "
                    arrAnulacion(7) = "1. nroCertif = nroCertif (Obtenido del Primer Método)"
                    arrAnulacion(8) = "2. customerId = customerId (Obtenido del Primer Método)"
                    arrAnulacion(9) = "3. nroSiniestro = nroSiniestro (Obtenido del Primer Método)"
                    arrAnulacion(10) = "4. estado " & strEstadAprobadoSini
                    arrAnulacion(11) = "5. usuarioModif = " & CurrentUser
                    arrAnulacion(12) = ""
                    arrAnulacion(13) = ""
                    arrAnulacion(14) = ""
                    arrAnulacion(15) = ""
                    arrAnulacion(16) = ""
                    arrAnulacion(17) = ""
                    strMensaje = strMensaje.Replace("*", "<br>")
                    strMensaje = String.Format(strMensaje, arrAnulacion)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Remitente: " & strRemitente)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Destinatario: " & strDestinatario)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Asunto: " & strAsunto)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Mensaje: " & strMensaje)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Flag: " & strFlag)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN IP: " & CurrentTerminal)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             IN Usuario: " & CurrentUser)

                    objEnvioCorreoWS.EnviarCorreoPM(strRemitente, strDestinatario, strAsunto, strMensaje, _
                                                    strFlag, CurrentTerminal, CurrentUser, "", "")

                    'Ocurrió un error tratando de realizar {0} del Siniestro  Datos: * {1} * {2} * {3} * {4} * {5} * {6} * {7} * {8} * {9}
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             OUT Cod Rpta: " & strCodRspta)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: AnularPedidoSiniestro)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==             OUT Msg Rpta: " & strMsgRspta & MaptPath)
                    'FIN PROY-140126

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      EnviarCorreoPM - FIN ==")
                Catch x As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==      ERROR - EnviarCorreoPM: " & x.Message.ToString())
                End Try
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== AnularPedidoSiniestro :: FIN  ==")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "===================================")
        End Try
    End Function
    'PROY-24724 - Iteracion 2 Siniestros -FIN



 
    ' INICIO 'VALIDACION BIOMETRICA PROY-25335-0 - Oscar Atencio Timana
  'PROY-25335 - 0
    Private Function Obtener_NroSec_PostPago(ByVal strNroPedido As String) As String
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim strNroSec As String = ""

        strNroSec = objConsultaPvu.ObtenerSec(strNroPedido)


        objConsultaPvu = Nothing
        Return strNroSec

    End Function

    'Inicio PROY 32089
    Private Function Obtener_NroSec_PrePago(ByVal strNroPedido As String, _
    ByRef CodResp As String, ByRef Mensaje As String, ByRef odtVentaRef As DataTable, ByRef odtVentaDetRef As DataTable) As String

        Dim strNroSec As String = ""

        Dim odtVenta As DataTable
        Dim odtVentaDet As DataTable



        Dim objClsConsultaPvu As New clsConsultaPvu
        objClsConsultaPvu.ConsultarPedidosPrepago(strNroPedido, CodResp, Mensaje, odtVenta, odtVentaDet)

        If (odtVenta.Rows.Count > 0 And odtVentaDet.Rows.Count > 0) Then
            strNroSec = Funciones.CheckStr(odtVenta.Rows(0)("VEPR_SEC_PORT"))
        End If

        objClsConsultaPvu = Nothing

        odtVentaDetRef = odtVenta
        odtVentaDetRef = odtVentaDet



        Return strNroSec

    End Function
    'Fin PROY 32089

    Private Function ConsultaSolicitudPospago(ByVal nroSEC As String) As String 'Boolean
        Dim oSolicitudNegocios As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList
        Dim Respuesta As String = ""

        lista = oSolicitudNegocios.ConsultaSolicitudNroSEC(nroSEC)

        If Not lista Is Nothing Then
            If lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    Respuesta = item.PRDC_CODIGO
                Next
            End If
        End If

        Return Respuesta
    End Function

    Private Function ConsultaPuntoVentaBio(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaPvu
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta_Codigo(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_OUTOFFBIO") ''
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function ConsultaSolicitudTipoOperacion(ByVal nroSEC As String) As String 'Boolean
        Dim oSolicitudNegocios As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList
        Dim Respuesta As String = ""

        lista = oSolicitudNegocios.ConsultaSolicitudNroSEC(nroSEC)

        If Not lista Is Nothing Then
            If lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    Respuesta = item.TOPEN_CODIGO
                Next
            End If
        End If

        Return Respuesta
    End Function

    Private Function ConsultaSolicitudModalidadVenta(ByVal nroSEC As String) As String
        Dim oSolicitudNegocios As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList
        Dim Respuesta As String = ""

        lista = oSolicitudNegocios.ConsultaSolicitud_NROSEC(nroSEC)

        If Not lista Is Nothing Then
            If lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    Respuesta = item._MODALIDAD_VENTA
                Next
            End If
        End If

        Return Respuesta
    End Function


  'PROY-25335 - 0
    ' FIN 'VALIDACION BIOMETRICA PROY-25335-0 - Oscar Atencio Timana

    'Inicio PROY 32089
    Private Function ValidarProcesoSolicitudPortabilidad(ByVal Cabecera_Detalle As DataSet, ByVal ds_tipo_producto As DataSet) As Boolean

        Dim pResultado As Boolean = False
        Dim pTipo_venta, pCodigo_operacion, pDescripcion_operacion As String
        Dim pTipoProducto As String = ds_tipo_producto.Tables(0).Rows(0).Item("PRDC_CODIGO")

        For Each dr As DataRow In Cabecera_Detalle.Tables(0).Rows
            pTipo_venta = dr.Item("PEDIC_TIPOVENTA")
            pCodigo_operacion = dr.Item("PEDIC_CODTIPOOPERACION")
            pDescripcion_operacion = dr.Item("PEDIV_DESCTIPOOPERACION")

            If pTipo_venta = ConfigurationSettings.AppSettings("strTVPostpago") AndAlso pCodigo_operacion = "01" AndAlso _
               pDescripcion_operacion = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST").ToString() AndAlso _
               (pTipoProducto = "01" Or pTipoProducto = "04") _
                OrElse _
               pTipo_venta = ConfigurationSettings.AppSettings("strTVPrepago") AndAlso pCodigo_operacion = "01" AndAlso _
               pDescripcion_operacion = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE").ToString() AndAlso _
               (pTipoProducto = "01" Or pTipoProducto = "04") Then
                pResultado = True  'Acceso a nueva lògica
            Else
                pResultado = False   'Acceso a flujo actual
            End If

        Next

        Return pResultado
    End Function
    'Fin PROY 32089

    '------------------------------------------
    ' PROY 26210 EGSC BEGIN - RENOVACION NO CONCRETADA
    '------------------------------------------
#Region "RENOVACION NO CONCRETADA - TIPIFICACION"

    'Registra la tipificacion y escribe un javascript para mostrar datos de tipificacion.
    Private Sub GenerarTipificacionRenovacionNoConcretada(ByVal drTipi As DataRow)

        '***********

        If (Not drTipi Is Nothing And drTipi("DEPEV_NROTELEFONO").ToString() <> "") Then
            Dim strIdIte As String = Me.RegistrarTipificacionRNC(drTipi("DEPEV_NROTELEFONO"), CurrentUser, drTipi)

        End If
    End Sub


    'Registra la tipificacion: RENOVACIÓN NO CONCRETADA
    'Cabecera y detalle.
    Private Function RegistrarTipificacionRNC(ByVal numeroTelefono As String, ByVal usuario As String, ByVal drTipi As DataRow) As String

        Dim oInteraccion = New COM_SIC_INActChip.Interaccion
        Dim strIdentifyLog As String = ""
        Dim strNroSec As String = "" 'ADD: PROY 26210 - RMZ
        strNroSec = Me.Obtener_NroSecPostPago(drTipi("PEDIN_NROPEDIDO")) 'ADD: PROY 26210 - RMZ
        Dim strCadenaNotas As String = "" 'ADD: PROY 26210 - RMZ
        strCadenaNotas = "Nombre Cliente : " & drTipi("PEDIV_NOMBRECLIENTE") & vbCrLf & "Importe : " & drTipi("INPAN_TOTALDOCUMENTO") & vbCrLf _
                         & "Saldo : " & drTipi("PEDIN_SALDO") & vbCrLf & "Tipo : " & drTipi("PEDIV_DESCCLASEFACTURA") & vbCrLf _
                         & "Fact.SUNAT : " & drTipi("PAGOC_CODSUNAT") & vbCrLf & "Doc. SAP Ref : " & IIf(drTipi.IsNull("PEDIN_PEDIDOSAP"), "", drTipi("PEDIN_PEDIDOSAP")) & vbCrLf _
                         & "Utilización : " & drTipi("DEPEV_DESCRIPCIONLP") & vbCrLf & "Cuota : " & drTipi("DEPEN_NROCUOTA") & vbCrLf _
                         & "Moneda : " & drTipi("PEDIC_MONEDA") & vbCrLf & "Neto : " & drTipi("INPAN_TOTALMERCADERIA") & vbCrLf _
                         & "Impuesto : " & drTipi("INPAN_TOTALIMPUESTO") & vbCrLf & "Pago : " & drTipi("INPAN_TOTALDOCUMENTO") & vbCrLf _
                         & "Estado SAP : " & drTipi("ESTADO_SAP") & vbCrLf & "Núm Telefónico : " & drTipi("DEPEV_NROTELEFONO") & vbCrLf _
                         & "NROPEDIDO : " & drTipi("PEDIN_NROPEDIDO") & vbCrLf & "Nro SEC : " & strNroSec 'ADD: PROY 26210 - RMZ

        With oInteraccion
            '.OBJID_CONTACTO = objId
            .TELEFONO = numeroTelefono
            .TIPO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_NC")
            .CLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_CLASE_NC")
            .SUBCLASE = ConfigurationSettings.AppSettings("CONS_RENOVACION_SUBCLASE_NC")
            .USUARIO_PROCESO = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO_NC")
            .AGENTE = usuario
            .TIPO_CODIGO = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_COD")
            .CLASE_CODIGO = ConfigurationSettings.AppSettings("CONS_RENOVACION_COD_CL_NC")
            .SUBCLASE_CODIGO = ConfigurationSettings.AppSettings("CONS_RENOVACION_COD_SCL_NC")

            .METODO = ConfigurationSettings.AppSettings("CONS_RENOVACION_METODO_NC")
            '.TIPO_INTER = ConfigurationSettings.AppSettings("CONS_RENOVACION_TIPO_INTER_NC")
            .FLAG_CASO = ConfigurationSettings.AppSettings("CONS_RENOVACION_FLAG_NC")
            .RESULTADO = ConfigurationSettings.AppSettings("CONS_RENOVACION_RESULTADO_NC")
            '.HECHO_EN_UNO = ConfigurationSettings.AppSettings("CONS_RENOVACION_HECHO_NC")
            .NOTAS = strCadenaNotas 'MOD: PROY 26210 - RMZ
        End With

        Dim flagInter As String
        Dim mensajeInter As String
        Dim idInteraccion As String

        Try
            strIdentifyLog = drTipi("PEDIN_NROPEDIDO")

            Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- RENOVACIÓN NO CONCRETADA ---")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Tipificacion  ---")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Notas : " & strCadenaNotas) 'ADD: PROY 26210 - RMZ
            Dim oIntReturn As New ItemGenerico
            'Dim oInteraccionNegocios As New InteraccionNegocios
            'oIntReturn = oInteraccionNegocios.InsertarInteraccion(oInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicia Metodo Tipificacion")
            oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo Tipificacion")

            'Registro del detalle Tipificacion : 

            'EGSC COMENTADO TIPI - PROCESO RENOVACION CAC:  Dim strInteraccion As String = TipificacionCambioPlanRenov(numeroTelefono, documentoSAP, vendedor, notasCambioPlan)
            If (flagInter.ToUpper().Equals("OK")) Then
                Dim oPlantilla1 As New COM_SIC_INActChip.PlantillaInteraccion
                oPlantilla1.X_ADDRESS = drTipi("PEDIV_NOMBRECLIENTE") 'Nombre del Cliente
                oPlantilla1.X_AMOUNT_UNIT = drTipi("INPAN_TOTALDOCUMENTO") 'Importe
                oPlantilla1.X_CHARGE_AMOUNT = drTipi("PEDIN_SALDO") 'Saldo
                oPlantilla1.X_TYPE_DOCUMENT = drTipi("PEDIV_DESCCLASEFACTURA") 'Tipo
                oPlantilla1.X_INTER_1 = drTipi("PAGOC_CODSUNAT") 'Fact.SUNAT
                oPlantilla1.X_BIRTHDAY = drTipi("PEDID_FECHADOCUMENTO") 'Fecha

                oPlantilla1.X_OST_NUMBER = IIf(drTipi.IsNull("PEDIN_PEDIDOSAP"), "", drTipi("PEDIN_PEDIDOSAP")) 'Doc. SAP Ref
                oPlantilla1.X_REASON = drTipi("DEPEV_DESCRIPCIONLP") 'Utilización
                oPlantilla1.X_INTER_10 = drTipi("DEPEN_NROCUOTA") 'Cuota
                oPlantilla1.X_MODEL = drTipi("PEDIC_MONEDA") 'Moneda
                oPlantilla1.X_ZIPCODE = drTipi("INPAN_TOTALMERCADERIA") 'Neto
                oPlantilla1.X_INTER_11 = drTipi("INPAN_TOTALIMPUESTO") 'Impuesto
                oPlantilla1.X_INTER_12 = drTipi("INPAN_TOTALDOCUMENTO") 'Pago
                oPlantilla1.X_LDI_NUMBER = drTipi("ESTADO_SAP") 'Estado SAP
                oPlantilla1.X_OTHER_PHONE = drTipi("DEPEV_NROTELEFONO") 'Núm Telefónico
                oPlantilla1.X_LOT_CODE = drTipi("PEDIN_NROPEDIDO") 'NROPEDIDO

                '--------------------------------------------------------------
                'AQUI SE AGREGA EL TIPO DE CLIENTE 
                '--------------------------------------------------------------
                Try
                    Dim objServicio As New clsClienteBSCS
                    Dim strempleado As String = CheckStr(CurrentUser)
                    Dim oSIACPostpagoConsultas As New clsDatosPostpagoNegocios
                    Dim objBLCbio As New BLDatosCBIO 'INICIATIVA-219

                    objServicio = objBLCbio.LeerDatosCliente(numeroTelefono, "", strempleado) 'INICIATIVA-219
                    'oSIACPostpagoConsultas.LeerDatosCliente(numeroTelefono, "", strempleado, "") 'MOD: PROY 26210 - RMZ

                    oPlantilla1.X_INTER_2 = objServicio.tipo_cliente 'Tipo Cliente
                Catch

                End Try


                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo InsertarPlantilla()")

                oTipificacion.InsertarPlantillaInteraccion(oPlantilla1, idInteraccion, flagInter, mensajeInter)

                If (flagInter.ToUpper().Equals("OK")) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nombre del Cliente : " & oPlantilla1.X_ADDRESS)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Importe : " & oPlantilla1.X_AMOUNT_UNIT)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Saldo : " & oPlantilla1.X_CHARGE_AMOUNT)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo : " & oPlantilla1.X_TYPE_DOCUMENT)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fact.SUNAT : " & oPlantilla1.X_INTER_1)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha : " & oPlantilla1.X_BIRTHDAY)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Doc. SAP Ref : " & oPlantilla1.X_OST_NUMBER)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Utilización : " & oPlantilla1.X_REASON)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cuota : " & oPlantilla1.X_INTER_10)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Moneda : " & oPlantilla1.X_MODEL)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Neto : " & oPlantilla1.X_ZIPCODE)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Impuesto : " & oPlantilla1.X_INTER_11)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Pago : " & oPlantilla1.X_INTER_12)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Estado SAP : " & oPlantilla1.X_LDI_NUMBER)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Núm Telefónico : " & oPlantilla1.X_OTHER_PHONE)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NROPEDIDO : " & oPlantilla1.X_LOT_CODE)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo Cliente : " & oPlantilla1.X_INTER_2)
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo InsertarPlantilla()")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Fin Tipificacion ---")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error : " & ex.Message.ToString())

        End Try

        Return idInteraccion

    End Function
    '*****************************************************************
    'ADD: PROY 26210 - RMZ
    '*****************************************************************
    Private Function Obtener_NroSecPostPago(ByVal strNroPedido As String) As String
        Dim strIdentifyLog As String = ""
        strIdentifyLog = strNroPedido
        Dim objContrato As DataSet          '*** guardar la informaciòn recuperada del contrato - RMZ ***'
        Dim dsAcuerdo As DataSet
        Dim strNroSec As String = ""
        Dim P_COD_RESP As String = ""
        Dim P_MSG_RESP As String = ""
        Dim strContrato As String = ""
        Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Obtener SEC - Renovacion No Concretada")
        objContrato = objClsConsultaPvu.ObtenerDrsap(strNroPedido, P_COD_RESP, P_MSG_RESP)
        If Not (objContrato Is Nothing) Then
            If objContrato.Tables(0).Rows(0).Item("ID_CONTRATO") <> Nothing Then
                strContrato = Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: " & Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO")))
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: ")
            End If
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Contrato Obtenido: ")
        End If

        dsAcuerdo = objClsConsultaPvu.ConsultaAcuerdoPCS(Funciones.CheckInt64(strContrato), DBNull.Value)

        If Not dsAcuerdo Is Nothing Then
            strNroSec = Funciones.CheckStr(dsAcuerdo.Tables(0).Rows(0).Item("contn_numero_sec"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         OUT Sec: " & strNroSec)
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "         OUT Sec: " & strNroSec)
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Obtener SEC - Renovacion No Concretada")
        Return strNroSec

    End Function
    '*****************************************************************
    'ADD: PROY 26210 - RMZ
    '*****************************************************************

#End Region
    '------------------------------------------
    ' PROY 26210 EGSC END - RENOVACION NO CONCRETADA
    '------------------------------------------

    'INI: PROY-140262 BLACKOUT
    Private Function ProcesarValidacionBlackOUT(ByVal dsPedido As DataSet, ByVal dsSolicitud As DataSet, ByVal strNroSec As String) As Boolean

        Dim strTipoProducto As String = String.Empty
        Dim strTipoVenta As String = String.Empty
        Dim strTipoDocumento As String = String.Empty
        Dim strTipoOperacion As String = String.Empty
        Dim strDescripcionOperacion As String = String.Empty
        Dim blnTieneFirmaDigital As Boolean = False
        Dim strModalidadVenta As String = String.Empty

        Dim strTipoFirmaDocumentosHP As Long = 0
        Dim strCodigoRespuesta As String = String.Empty
        Dim strMensajeRespuesta As String = String.Empty
        Dim nroContrato As String = "0"
        Dim strNroDocCliente As String = String.Empty
        Dim strNroPedido As String = String.Empty
        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|INICIO")
        If (Not dsSolicitud Is Nothing AndAlso dsSolicitud.Tables.Count > 0) Then
            strTipoProducto = dsSolicitud.Tables(0).Rows(0).Item("PRDC_CODIGO")            
            nroContrato = IIf(Funciones.CheckStr(dsSolicitud.Tables(0).Rows(0).Item("SOLIV_NUM_CON")) = String.Empty, "0", Funciones.CheckStr(dsSolicitud.Tables(0).Rows(0).Item("SOLIV_NUM_CON")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|strTipoProducto" & Funciones.CheckStr2(strTipoProducto))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|nroContrato" & Funciones.CheckStr2(nroContrato))

        End If

        If (Not dsPedido Is Nothing AndAlso dsPedido.Tables.Count > 1) Then
            strTipoDocumento = dsPedido.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE")
            strTipoVenta = dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA")
            strTipoOperacion = dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION")
            strDescripcionOperacion = dsPedido.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|strTipoDocumento" & Funciones.CheckStr2(strTipoDocumento))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|strTipoVenta" & Funciones.CheckStr2(strTipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|strTipoOperacion" & Funciones.CheckStr2(strTipoOperacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ProcesarValidacionBlackOUT|strDescripcionOperacion" & Funciones.CheckStr2(strDescripcionOperacion))

            If (strDescripcionOperacion = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST").ToString()) Then
                'PORTABILIDAD POSTPAGO
                'Consulta Expediente 
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- CONSULTAEXPEDIENTEVENTA|INICIO")

                Dim oConsultarExpedienteVenta As BEExpedienteVenta = New COM_SIC_Activaciones.clsConsultaPvu().ConsultarExpedienteVenta(Funciones.CheckStr(nroContrato), strCodigoRespuesta, strMensajeRespuesta)
                If (Not oConsultarExpedienteVenta Is Nothing) Then
                    strTipoFirmaDocumentosHP = oConsultarExpedienteVenta.SEVN_TIPO_EXPEDIENTE
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- ConsultarExpedienteVenta|strTipoFirmaDocumentosHP" & Funciones.CheckStr2(strTipoFirmaDocumentosHP))

                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- CONSULTABIOMETRIAOPCIONAISLADA|INICIO")

                Dim stCodRptaBio As String, stMsjRptaBio As String
                If (strTipoFirmaDocumentosHP = 0) Then
                    strNroDocCliente = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE"))
                    strNroPedido = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIN_NROPEDIDO"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- CONSULTABIOMETRIAOPCIONAISLADA|strNroDocCliente" & Funciones.CheckStr2(strNroDocCliente))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- CONSULTABIOMETRIAOPCIONAISLADA|strNroPedido" & Funciones.CheckStr2(strNroPedido))

                    Dim dsBio As DataSet = New clsConsultaPvu().ValidarIdentidad(strTipoDocumento, strNroDocCliente, strNroSec, strNroPedido, stCodRptaBio, stMsjRptaBio)
                    If (Not dsBio Is Nothing AndAlso dsBio.Tables.Count > 0) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strNroSec & "- " & " PROY-140262 BLACKOUT- CONSULTABIOMETRIAOPCIONAISLADA|BIOM_TIP_OPERACION" & Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_TIP_OPERACION")))
                        If (Funciones.CheckStr(dsBio.Tables(0).Rows(0).Item("BIOM_TIP_OPERACION")).ToUpper() = ConfigurationSettings.AppSettings("ConstTipOpeBioVA")) Then
                            strTipoFirmaDocumentosHP = 2
                        End If
                    End If
                End If

                'Consulta Modalida Venta
                Dim arrSolicitud As ArrayList = New COM_SIC_Activaciones.ClsCambioPlanPostpago().ConsultaSolicitud_NROSEC(Funciones.CheckStr(strNroSec))
                If Not arrSolicitud Is Nothing And arrSolicitud.Count > 0 Then
                    For Each item As clsSolicitudPersona In arrSolicitud
                        strModalidadVenta = Funciones.CheckStr(item.MODALIDAD_VENTA)
                        Exit For
                    Next
                End If
                'Valida Si aplica Pago BlackOUT
                If (Not (strTipoProducto = "01" AndAlso strTipoFirmaDocumentosHP = "2" AndAlso strTipoDocumento = "01" AndAlso strModalidadVenta = "1")) Then
                    Return False
                End If
            ElseIf (strDescripcionOperacion = ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE").ToString()) Then
                'PORTABILIDAD PREPAGO
                If (Not (strTipoProducto = "01" AndAlso strTipoDocumento = "01")) Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function


    Private Function ValidarModalidadVentaChipSuelto(ByVal sNroSec As String) As Boolean
        Dim strIdentifyLog As String = CStr(Session("USUARIO")) & " - " & sNroSec
        Dim objActivaciones As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 Validar Modalidad Venta Chip Suelto - INICIO")
            ValidarModalidadVentaChipSuelto = False

            lista = objActivaciones.ConsultaSolicitud_NROSEC(Funciones.CheckStr(sNroSec))

            If Not lista Is Nothing And lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    If Funciones.CheckStr(item.MODALIDAD_VENTA).Equals("1") Then
                        ValidarModalidadVentaChipSuelto = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 Modalidad de Venta : " & Funciones.CheckStr(item.MODALIDAD_VENTA))
                        Exit For
                    End If
                Next
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 No hay datos de la sec")
            End If

        Catch ex As Exception
            ValidarModalidadVentaChipSuelto = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 Error al validar Modalidad Venta Chip Suelto : " & ex.Message.ToString)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " PROY-33313 Validar Modalidad Venta Chip Suelto - FIN")
        End Try

        Return ValidarModalidadVentaChipSuelto
    End Function

    'FIN: PROY-140262 BLACKOUT

    ''PROY-140585 F2
    Private Function ConsultaPinPortabilidad(ByVal sNroSec As String, ByVal sNroPedido As String) As String
        Try
            Dim oObjValidarPin As String = "false"
            Dim mensaje As String = String.Empty
            Dim sFLagActPin As String = String.Empty
            Dim objConsultaSecPvu As New COM_SIC_Activaciones.clsConsultaPvu
            Dim sProdPermitido As String = String.Empty
            Dim ValRpta = String.Empty
            Dim RptaFuncion = String.Empty
            Dim sNroContrato As String = String.Empty
            Dim CodProducto As String = String.Empty
            Dim dsParamPinPorta As DataSet
            Dim ParamGrupoPinPorta = Funciones.CheckStr(ConfigurationSettings.AppSettings("ParamGrupoPinPorta"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "ParamGrupoPinPorta = " & ParamGrupoPinPorta)
            Dim objObtenerParamGrupo As New COM_SIC_Cajas.clsCajas

            dsParamPinPorta = objObtenerParamGrupo.ObtenerParamByGrupo(ParamGrupoPinPorta)

            Dim i As Integer
            For i = 0 To dsParamPinPorta.Tables(0).Rows.Count - 1
                If dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR1") = "key_MsjePagoSinPinPorta" Then
                    mensaje = Funciones.CheckStr(dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR"))
                End If
                If dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR1") = "key_FlagActValPinPago" Then
                    sFLagActPin = Funciones.CheckStr(dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR"))
                End If
                If dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR1") = "key_tipo_productoPDV" Then
                    sProdPermitido = Funciones.CheckStr(dsParamPinPorta.Tables(0).Rows(i).Item("PARAV_VALOR"))
                End If
            Next

            objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "PARAM mensaje = " & mensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "PARAM sFLagActPin = " & sFLagActPin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "PARAM sProdPermitido = " & sProdPermitido)

            Dim objDatosTipoProducto As New ClsPortabilidad
            Dim pstrCodRpta, pstrMsgRpta As String

            Dim dtLineas As DataSet = objDatosTipoProducto.Obtener_tipo_producto(sNroSec, sNroPedido.ToString, pstrCodRpta, pstrMsgRpta)
            If Not dtLineas Is Nothing AndAlso dtLineas.Tables.Count > 0 AndAlso dtLineas.Tables(0).Rows.Count > 0 Then
                CodProducto = Funciones.CheckStr(dtLineas.Tables(0).Rows(0).Item("PRDC_CODIGO"))
            End If

            Dim strCodRptaSEC, strMsgRptaSEC As String
            Dim dsContrato As DataSet

            dsContrato = objConsultaSecPvu.ObtenerDrsap(sNroPedido, strCodRptaSEC, strMsgRptaSEC)
            If Not dsContrato Is Nothing AndAlso dsContrato.Tables.Count > 0 AndAlso dsContrato.Tables(0).Rows.Count > 0 Then
                sNroContrato = Funciones.CheckStr2(dsContrato.Tables(0).Rows(0).Item("ID_CONTRATO"))
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}:{1}", "[PROY-140585 F2] ValidarPinPorta | CodProducto", Funciones.CheckStr(CodProducto)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}:{1}", "[PROY-140585 F2] ValidarPinPorta | sNroContrato", Funciones.CheckStr(sNroContrato)))
            ''JMGF INI

            Dim objCajas As COM_SIC_Cajas.clsCajas
            If sNroSec = "" AndAlso sNroContrato <> "" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}:{1}", "[PROY-140585 F2] ValidarPinPorta | sNroSec", Funciones.CheckStr(sNroContrato)))
                sNroSec = objConsultaSecPvu.ObtenerSec(sNroPedido)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140585 F2][ValidarPinPorta][ObtenerSec][sNroSec]" & Funciones.CheckStr(sNroSec))
                If sNroSec = "" Then
                    sNroSec = objCajas.ObtenerSecByContrato(sNroContrato)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140585 F2][ValidarPinPorta][ObtenerSecByContrato][sNroSec]" & Funciones.CheckStr(sNroSec))
                End If
            End If
            ''JMGF FIN

            If sFLagActPin = "1" And sProdPermitido.IndexOf(CodProducto) > -1 Then
                If sNroSec.Length > 0 Then

                    Dim cResultado As String
                    Dim validarPin As Boolean


                    objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "Entrada validarPinPortabilidad: NROSEC = " & sNroSec)
                    validarPin = objConsultaSecPvu.validarPinPortabilidad(sNroSec, cResultado)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "Salida validarPinPortabilidad: K_RESULTADO = " & cResultado)

                    'IDEA-300846 INI
                    Select Case cResultado
                        Case "0"
                        oObjValidarPin = "true"
                        Case "1"
                            ValRpta = Funciones.CheckStr(ReadKeySettings.Key_MsjValidaPinNoVigenteCAC)
                        Case "2"
                            ValRpta = Funciones.CheckStr(ReadKeySettings.Key_MsjValidaPinNoPinCAC)
                        Case "3"
                            ValRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MsjErrorGenerarPin), sNroSec)
                        Case "4"
                            ValRpta = Funciones.CheckStr(ReadKeySettings.Key_MsjSPRechazoPinCAC)
                        Case Else
                        ValRpta = mensaje
                    End Select
                    'IDEA-300846 FIN
                Else
                    oObjValidarPin = "true"
                End If
            Else
                oObjValidarPin = "true"
                ValRpta = "OK"
            End If
            RptaFuncion = Funciones.CheckStr(oObjValidarPin) & "|" & Funciones.CheckStr(ValRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, sNroSec & "- " & "Salida ConsultaPinPortabilidad = " & RptaFuncion)
            Return RptaFuncion
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Exception Error: " & ex.Message.ToString())
            Return "0" & "|" & ex.Message.ToString()
        End Try
        ''PROY-140585 F2

    End Function

    'PROY-140590 IDEA142068 - INICIO
    Private Function ValidarCampaniaSTBK(ByVal strNroPedido As String, ByRef descBanco As String, ByRef descCampana As String) As Boolean
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("USUARIO")) & " - " & strNroPedido
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim respuesta As Boolean = False

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "INICIO ValidarCampaniaSTBK"))

        Try
            Dim key_tipoVenta As String = Funciones.CheckStr(clsKeyAPP.Key_TipoVentaSTBK())
            Dim key_canal As String = Funciones.CheckStr(clsKeyAPP.Key_canalPermitidoSTBK())
            Dim key_operacionPre As String = Funciones.CheckStr(clsKeyAPP.Key_OperacionPreSTBK())
            Dim key_operacionPos As String = Funciones.CheckStr(clsKeyAPP.Key_OperacionPosSTBK())

            Dim key_campaniasSTBK As String = Funciones.CheckStr(clsKeyAPP.Key_campaniaActivasSTBK())
            Dim key_modalidad As String = Funciones.CheckStr(clsKeyAPP.Key_modalidadVentaSTBK())
            Dim key_producto As String = Funciones.CheckStr(clsKeyAPP.Key_TipoProdPermitidoSTBK())
            Dim key_operacionPrePvu As String = Funciones.CheckStr(clsKeyAPP.Key_OperacionPrePVU())
            Dim key_operacionPosPvu As String = Funciones.CheckStr(clsKeyAPP.Key_OperacionPosPVU())

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_TipoVentaSTBK", key_tipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_canalPermitidoSTBK", key_canal))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_OperacionPreSTBK", key_operacionPre))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_OperacionPosSTBK", key_operacionPos))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_campaniaActivasSTBK", key_campaniasSTBK))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_modalidadVentaSTBK", key_modalidad))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_TipoProdPermitidoSTBK", key_producto))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_OperacionPrePVU", key_operacionPrePvu))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Key_OperacionPosPVU", key_operacionPosPvu))

            Dim bolPedido As Boolean = False
            Dim tipoVenta As String = String.Empty
            Dim canal As String = String.Empty
            Dim operacion As String = String.Empty

            Dim strCodRpta As String = String.Empty
            Dim strMsjRpta As String = String.Empty

            Dim bolDatos As Boolean = False
            Dim modalidad As String = String.Empty
            Dim producto As String = String.Empty
            Dim operacionPvu As String = String.Empty

            Dim dsPedido As DataSet
            Dim dsDatos As DataSet
            Dim dsCampana As DataSet

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Inicio Consulta Datos Pedido", strNroPedido))
            dsPedido = objConsultaMsSap.ConsultaPedido(strNroPedido, "", "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "Fin Consulta Datos Pedido"))

            If Not dsPedido Is Nothing AndAlso dsPedido.Tables.Count > 0 AndAlso dsPedido.Tables(0).Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "Consulta los datos del pedido => true"))

                tipoVenta = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
                canal = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_TIPOOFICINA"))
                operacion = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))

                If key_tipoVenta.IndexOf(tipoVenta) > -1 AndAlso key_canal.IndexOf(canal) > -1 Then
                    If tipoVenta = "01" AndAlso key_operacionPos.IndexOf(operacion) > -1 Then
                        bolPedido = True
                    ElseIf tipoVenta = "02" AndAlso key_operacionPre.IndexOf(operacion) > -1 Then
                        bolPedido = True
                    End If
                End If
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Tipo Venta", tipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Canal", canal))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Operacion MSSAP", operacion))

            If (bolPedido) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "INICIO Consulta en PVUDB"))
                dsDatos = objConsultaPvu.ConsultaDatosCampania(strNroPedido, tipoVenta, operacion, strCodRpta, strMsjRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Codigo Respuesta PVUDB", Funciones.CheckStr(strCodRpta)))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Mensaje Respuesta PVUDB", Funciones.CheckStr(strMsjRpta)))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "FIN Consulta en PVUDB"))

                If strCodRpta = "0" AndAlso Not dsDatos Is Nothing AndAlso dsDatos.Tables.Count > 0 AndAlso dsDatos.Tables(0).Rows.Count > 0 Then
                    producto = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item("COD_PRODUCTO"))
                    operacionPvu = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item("COD_OPERACION"))
                    modalidad = Funciones.CheckStr(dsDatos.Tables(0).Rows(0).Item("MODALIDAD_VENTA"))

                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Tipo Producto", producto))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Operacion PVU", operacionPvu))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "Tipo de Modalidad", modalidad))

                    'Realizamos las validacion en PVUDB
                    If key_producto.IndexOf(producto) > -1 Then
                        If tipoVenta = "01" Then
                            If key_modalidad.IndexOf(modalidad) > -1 AndAlso key_operacionPosPvu.IndexOf(operacionPvu) > -1 Then
                                bolDatos = True
                            End If
                        ElseIf tipoVenta = "02" Then
                            If key_operacionPrePvu.IndexOf(operacionPvu) > -1 Then
                                bolDatos = True
                            End If
                        End If
                    End If

                    'Recorremos el dataset si cumplio validacion en PVU
                    Dim validacion As Boolean = False
                    Dim codCampana As String = String.Empty

                    If (bolDatos) Then

                        For i As Int32 = 0 To dsDatos.Tables(0).Rows.Count - 1
                            codCampana = Funciones.CheckStr(dsDatos.Tables(0).Rows(i).Item("CAMPANA"))

                            If Not key_campaniasSTBK = String.Empty Then
                                Dim arrayCampana As Array = key_campaniasSTBK.Split("|")
                                For index As Int32 = 0 To arrayCampana.Length - 1
                                    Dim campana As String = Funciones.CheckStr(arrayCampana(index))
                                    If codCampana = campana Then
                                        validacion = True
                                        Exit For
                                    End If
                                Next
                            End If

                            If (validacion) Then
                                'Invocamos al SP para obtener el Banco de la campana
                                dsCampana = objConsultaPvu.ConsultaCampaniaBin(codCampana, "", strCodRpta, strMsjRpta)
                                If strCodRpta = "0" AndAlso Not dsCampana Is Nothing AndAlso dsCampana.Tables.Count > 0 AndAlso dsCampana.Tables(0).Rows.Count > 0 Then
                                    descBanco = Funciones.CheckStr(dsCampana.Tables(0).Rows(0).Item("BANCV_DESCRIPCION"))
                                    descCampana = Funciones.CheckStr(codCampana)
                                    respuesta = True
                                    Exit For
                                End If
                            End If
                        Next
                    End If

                End If
            End If

        Catch ex As Exception
            respuesta = False
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2} {3}", strIdentifyLog, "ERROR", ex.Message, ex.StackTrace))
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "respuesta", respuesta))
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", strIdentifyLog, "FIN ValidarCampaniaSTBK"))

        Return respuesta

    End Function
    'PROY-140590 IDEA142068 - FIN

    'PROY-140740 INI
    Private Function ConsultaOferta(ByVal sNroSec As String) As String

        Dim oSolicitudNegocios As New COM_SIC_Activaciones.ClsCambioPlanPostpago
        Dim lista As New ArrayList
        Dim strOferta = String.Empty

        lista = oSolicitudNegocios.ConsultaSolicitudNroSEC(sNroSec)
        If Not lista Is Nothing Then
            If lista.Count > 0 Then
                For Each item As clsSolicitudPersona In lista
                    strOferta = item.TPROC_CODIGO
                    Exit For
                Next
            End If
        End If
        Return strOferta

    End Function
    'PROY-140740 INI

'INICIATIVA 712 Cobro Anticipado INI
    Private Function ConsultaPA(ByVal pTipoOperacion As String, ByVal pCodMaterial As String, ByVal pNumeDoc As String, ByRef Mensaje As String) As Boolean
        Dim strIdentifyLog As String = CStr(Session("USUARIO")) & " - " & pNumeDoc
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - INICIO")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado ConsultaPA() Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - pTipoOperacion: " & pTipoOperacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - pCodMaterial: " & pCodMaterial)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - pNumeDoc: " & pNumeDoc)
        If (pTipoOperacion = ReadKeySettings.Key_TipoOperacionSICAR) Then
            If Not ((ReadKeySettings.Key_CodMaterialPermitidosSICAR).IndexOf(pCodMaterial) = -1) Then
                Return ConsultaPagoAnticipado(pNumeDoc, Mensaje)
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado ConsultaPA() Fin")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - FIN")
        Return False
    End Function

    Private Function ConsultaPagoAnticipado(ByVal pNumeDoc As String, ByRef Mensaje As String) As Boolean
        Dim strIdentifyLog As String = CStr(Session("USUARIO")) & " - " & pNumeDoc
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - INICIO")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado ConsultaPagoAnticipado() Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - INICIO")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - pNumeDoc: " & pNumeDoc)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - Mensaje: " & Mensaje)

        Dim bRespuesta As New Boolean
        bRespuesta = False
        Dim objRequest As New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPAGenericRequest
        Dim oListaPagos() As COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.PagoAnticipado

        objRequest.MessageRequest = New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPAMessageRequest
        objRequest.MessageRequest.Header = New COM_SIC_Procesa_Pagos.DataPowerRest.HeadersRequest
        objRequest.MessageRequest.Header.HeaderRequest = New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - objRequest.MessageRequest.Header.HeaderRequest")

        objRequest.MessageRequest.Header.HeaderRequest.consumer = CheckStr(ReadKeySettings.ConsConsumerConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - consumer: " & objRequest.MessageRequest.Header.HeaderRequest.consumer)

        objRequest.MessageRequest.Header.HeaderRequest.country = CheckStr(ReadKeySettings.ConsCountryConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - country: " & objRequest.MessageRequest.Header.HeaderRequest.country)

        objRequest.MessageRequest.Header.HeaderRequest.dispositivo = CheckStr(ReadKeySettings.ConsDispositivoConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - dispositivo: " & objRequest.MessageRequest.Header.HeaderRequest.dispositivo)

        objRequest.MessageRequest.Header.HeaderRequest.language = CheckStr(ReadKeySettings.ConsLanguageConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - language: " & objRequest.MessageRequest.Header.HeaderRequest.language)

        objRequest.MessageRequest.Header.HeaderRequest.modulo = CheckStr(ReadKeySettings.ConsModuloConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - modulo: " & objRequest.MessageRequest.Header.HeaderRequest.modulo)

        objRequest.MessageRequest.Header.HeaderRequest.msgType = CheckStr(ReadKeySettings.ConsMsgTypeConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - msgType: " & objRequest.MessageRequest.Header.HeaderRequest.msgType)

        objRequest.MessageRequest.Header.HeaderRequest.operation = CheckStr(ReadKeySettings.ConsOperationConsultaPA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - operation: " & objRequest.MessageRequest.Header.HeaderRequest.operation)

        objRequest.MessageRequest.Header.HeaderRequest.pid = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - pid: " & objRequest.MessageRequest.Header.HeaderRequest.pid)

        objRequest.MessageRequest.Header.HeaderRequest.system = ConfigurationSettings.AppSettings("constAplicacion")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - system: " & objRequest.MessageRequest.Header.HeaderRequest.system)

        objRequest.MessageRequest.Header.HeaderRequest.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - timestamp: " & objRequest.MessageRequest.Header.HeaderRequest.timestamp)

        objRequest.MessageRequest.Header.HeaderRequest.userId = CurrentUser
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - userId: " & objRequest.MessageRequest.Header.HeaderRequest.userId)

        objRequest.MessageRequest.Header.HeaderRequest.wsIp = ConfigurationSettings.AppSettings("cons_IPClientepagoanticipadofija") 
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - wsIp: " & objRequest.MessageRequest.Header.HeaderRequest.wsIp)


        objRequest.MessageRequest.Body = New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPARequestBody
        objRequest.MessageRequest.Body.consultaPARequest = New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPARequest
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - objRequest.MessageRequest.Body.consultaPARequest")

        objRequest.MessageRequest.Body.consultaPARequest.numeroDocumento = pNumeDoc
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - numeroDocumento: " & objRequest.MessageRequest.Body.consultaPARequest.numeroDocumento)

        objRequest.MessageRequest.Body.consultaPARequest.tipoConsulta = "5"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - tipoConsulta: " & objRequest.MessageRequest.Body.consultaPARequest.tipoConsulta)

        objRequest.MessageRequest.Body.consultaPARequest.estado = ""

        Dim objServicio As New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.BWCobroAnticipadoInstalacion
        Dim oResponse As New Object
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - ConsultarPagosAnticipados() Inicio Servicio")
            oResponse = objServicio.ConsultarPagosAnticipados(objRequest)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - mensajeRespuesta: " & oResponse.MessageResponse.Body.consultaPAResponseType.responseStatus.codigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - mensajeRespuesta: " & oResponse.MessageResponse.Body.consultaPAResponseType.responseStatus.mensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - ConsultarPagosAnticipados() Fin Servicio")
            Dim oRespuesta As New COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPAGenericResponse
            oRespuesta = CType(oResponse, COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion.ConsultaPAGenericResponse)
            If IsNothing(oRespuesta._MessageResponse.Body.consultaPAResponseType.responseData) Then
                bRespuesta = True
                Mensaje = ReadKeySettings.Key_MsjSecPendienteEvaluacionSICAR
            Else
                oListaPagos = oRespuesta._MessageResponse.Body.consultaPAResponseType.responseData.pagoAnticipado
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - oListaPagos: " & oListaPagos.Length)
                If (oListaPagos.Length > "0") Then
                    bRespuesta = False
                    If Not oListaPagos(0).estado = "03" Then
                        bRespuesta = True
                        Mensaje = ReadKeySettings.Key_MsjSecPendienteAprobacionSICAR
                    End If
                End If
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - Error ConsultaPagoAnticipado(): " & ex.Message)
            bRespuesta = True
            Return bRespuesta
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - bRespuesta: " & bRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - ConsultaPagoAnticipado() Fin")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIATIVA 712 Cobro Anticipado - FIN")
        End Try
        Return bRespuesta
    End Function
    'INICIATIVA 712 Cobro Anticipado FIN

    'PROY-140715 - IDEA 140805 | No biometría en SISACT en caída RENIEC| METODO QUE INVOCA AL SERVICIO | INICIO
    Private Function consultarPedidoVentaContingencia(ByVal strNroPedido As String)

        Dim strIdentifyLog As String = "consultarPedidoVentaContingencia"
        Dim objConsultar As New COM_SIC_Activaciones.BWVentaContingencia
        Dim objResponseMessage As New ConsultarVentaContResponse
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][INICIO CONSULTAR PEDIDO VENTAS CONTINGENCIA]", String.Empty))
        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][consultarPedidoVentaContingencia][INPUT][Nro_Pedido]", Funciones.CheckStr(strNroPedido)))
            objResponseMessage = objConsultar.ConsultarVentaContingencia(strNroPedido)
            strTipoContingenciaCaidaNoBio = Funciones.CheckStr(objResponseMessage.MessageResponse.Body.consultarVentasContingenciaResponse.ventasContingencia(0).tipoContingencia())

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][consultarPedidoVentaContingencia][OUTPUT][Nro_Pedido]", Funciones.CheckStr(strTipoContingenciaCaidaNoBio)))

            If strTipoContingenciaCaidaNoBio.Equals("2") Then
                Session("isVentaCtg") = True
            End If

            If (strTipoContingenciaCaidaNoBio.Equals("1") Or strTipoContingenciaCaidaNoBio.Equals("3")) Then
                Session("isVentaCtgUno") = True
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][CATCH]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][CATCH][Message]", ex.Message))
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][CATCH][StackTrace]", ex.StackTrace))

        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1} => {2}", strIdentifyLog, "[PROY-140715 - IDEA 140805 - No biometría en SISACT en caída RENIEC_4 ][FIN CONSULTAR PEDIDO VENTAS CONTINGENCIA]", String.Empty))
    End Function
    'PROY-140715 - IDEA 140805 | FIN
''IDEA300216 INICIO
    Private Function GetDataValidarClaveUnica(ByVal nroPedido As String, ByRef tipodoc As String, ByRef numdoc As String, ByRef numsec As String, ByRef tipoventa As String, ByRef transaccion As String) As String


        Dim objContrato As DataSet
        Dim P_COD_RESP As String
        Dim P_MSG_RESP As String
        Dim sTipoVenta As String
        Dim stipoProd As String
        Dim sRespuesta As String = "-1"
        Dim bFlagTrxRegulada As Boolean = False
        Dim bRenovYChip As Boolean = False
        Dim sTipoOpeCod As String
        Dim sTipoOpeDesc As String
        Dim objClsActivacionPEL As New ClsActivacionPel
        Dim sTrxValidaNotificacion As String = String.Empty
        Dim sNumeroSEC As String = String.Empty
        Dim obConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strIdenLog As String = "GetDataValidarClaveUnica"
        Dim sTipoDocCli As String = String.Empty
        Dim sNumDocCli As String = String.Empty

        Dim dsPed As DataSet
        Dim dtCabPedidoPre As New DataTable
        Dim dtDetPedidoPre As New DataTable

        Try


            dsPed = obConsultaMsSap.ConsultaPedido(nroPedido, "", "")

            Dim strDescriOpePostPago1 As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_POST"))
            Dim strDescriOpePrePago1 As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("DESCTIPOOPERACION_PORTA_PRE"))
            Dim strTipoOperacionPago1 As String = Funciones.CheckStr(dsPed.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION"))
            Dim objClsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu

            sTipoDocCli = Funciones.CheckStr(dsPed.Tables(0).Rows(0).Item("CLIEC_TIPODOCCLIENTE"))
            sNumDocCli = Funciones.CheckStr(dsPed.Tables(0).Rows(0).Item("CLIEV_NRODOCCLIENTE"))

            If Not dsPed Is Nothing Then
                If dsPed.Tables(1).Rows.Count > 0 Then
                    sTipoOpeDesc = dsPed.Tables(0).Rows(0).Item("PEDIV_DESCTIPOOPERACION")
                    sTipoOpeCod = dsPed.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION")
                End If
            End If

            sTipoVenta = Funciones.CheckStr(dsPed.Tables(0).Rows(0).Item("PEDIC_TIPOVENTA"))
            Dim tipoOperacion_PackPrepago As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("COD_OPERACION_RENOVACION"))
            Dim sTipoOperacion As String
            sTipoOperacion = Funciones.CheckStr(dsPed.Tables(0).Rows(0).Item("PEDIC_CODTIPOOPERACION"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTipoOperacion Pedido Actual: " & Funciones.CheckStr(sTipoOperacion))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] tipoOperacion_PackPrepago : " & tipoOperacion_PackPrepago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] tipoOperacion RENOVACION: " & Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVRenovacion")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] Tipo Operacion RepoChip: " & Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVReposicionChip")))
            If (sTipoOperacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVReposicionChip")) Or sTipoOperacion = tipoOperacion_PackPrepago _
                Or sTipoOperacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVRenovacion"))) Then
                For j As Integer = 0 To dsPed.Tables(1).Rows.Count - 1
                    If Funciones.CheckStr(dsPed.Tables(1).Rows(j).Item("MATEC_TIPOMATERIAL")) = ConfigurationSettings.AppSettings("constChips") Then
                        bRenovYChip = True 'ES REPO O RENO POST O PRE CON CHIP APLICA CLAVE UNICA
                        bFlagTrxRegulada = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & " REPO/RENO CON CHIP")
                    End If
                Next
            Else ' VALIDAMOS DEMAS TRANSACCIONES
                If sTipoVenta = ConfigurationSettings.AppSettings("strTVPrepago") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "PEDIDO PREPAGO")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTipoOpeCod : " & Funciones.CheckStr(sTipoOpeCod))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTipoOpeDesc : " & Funciones.CheckStr(sTipoOpeDesc))
                    If sTipoOpeCod = "01" Then
                        If sTipoOpeDesc = "VENTA NORMAL /ALTA" Or sTipoOpeDesc = "PORTABILIDAD PREPAGO" Then
                            Dim arrListaPrepago As ArrayList = objClsActivacionPEL.Lis_Lista_Detalle_Venta_Prepago(nroPedido, P_COD_RESP)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & " OUT arrListaPrepago :" & arrListaPrepago.Count)

                            If arrListaPrepago.Count > 0 Then

                                For Each item As COM_SIC_Activaciones.DetalleVentaPrepago In arrListaPrepago
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "     INICIO LEER DATOS PREPAGO")
                                    stipoProd = Funciones.CheckStr(item.COD_PROD_PREP)
                                    sNumeroSEC = Funciones.CheckStr(Me.Obtener_NroSec_PrePago(nroPedido, P_COD_RESP, P_MSG_RESP, dtCabPedidoPre, dtDetPedidoPre))
                                Next
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] stipoProd : " & Funciones.CheckStr(stipoProd))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sNumeroSEC : " & Funciones.CheckStr(sNumeroSEC))
                            End If

                            If Not stipoProd = "01" Then
                                bFlagTrxRegulada = False ' clave unica solo productos moviles
                            Else
                                bFlagTrxRegulada = True
                            End If
                        End If

                    End If

                ElseIf sTipoVenta = ConfigurationSettings.AppSettings("strTVPostpago") Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "PEDIDO POSTPAGO")
                    Dim dsAcuerdoMigra As DataSet

                    Dim strContratoMigra As String
                    Dim sisact_Cod_OperacionMigra As String


                    Try
                        objContrato = objClsConsultaPvu.ObtenerDrsap(Funciones.CheckStr(nroPedido), P_COD_RESP, P_MSG_RESP)
                        If Not objContrato Is Nothing Then
                            If objContrato.Tables(0).Rows.Count > 0 Then
                                strContratoMigra = Funciones.CheckStr(objContrato.Tables(0).Rows(0).Item("ID_CONTRATO"))
                                stipoProd = Funciones.CheckStr(objContrato.Tables(1).Rows(0).Item("PRDC_CODIGO"))
                            End If
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] strContratoMigra : " & Funciones.CheckStr(strContratoMigra))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] stipoProd : " & Funciones.CheckStr(stipoProd))

                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "objContrato.Tables(0).Rows(0).Item(ID_CONTRATO)" & ex.Message)
                    End Try

                    If stipoProd = "01" Then

                        Try

                            dsAcuerdoMigra = objClsConsultaPvu.ConsultaAcuerdoPCS(Funciones.CheckInt64(strContratoMigra), DBNull.Value)    'Consulta PVU
                            If Not dsAcuerdoMigra Is Nothing Then

                                sisact_Cod_OperacionMigra = Funciones.CheckStr(dsAcuerdoMigra.Tables(0).Rows(0).Item("contc_tipo_operacion"))
                                sNumeroSEC = Funciones.CheckStr(dsAcuerdoMigra.Tables(0).Rows(0).Item("contn_numero_sec"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sisact_Cod_OperacionMigra : " & Funciones.CheckStr(sisact_Cod_OperacionMigra))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sNumeroSEC : " & Funciones.CheckStr(sNumeroSEC))

                                Select Case sisact_Cod_OperacionMigra
                                    Case ConfigurationSettings.AppSettings("strDTVAlta")
                                        bFlagTrxRegulada = True
                                    Case ConfigurationSettings.AppSettings("strDTVMigracion")
                                        bFlagTrxRegulada = False
                                    Case ConfigurationSettings.AppSettings("strDTVRenovacion")
                                        bFlagTrxRegulada = False
                                    Case ConfigurationSettings.AppSettings("strDTVReposicionChip")
                                        bFlagTrxRegulada = True
                                    Case Else
                                        bFlagTrxRegulada = False
                                End Select

                            End If
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "Error al tratar de recuperar el contrato MIGRACION" & ex.Message)
                        End Try


                    Else
                        bFlagTrxRegulada = False ' clave unica solo productos moviles
                    End If


                End If

            End If
            If (sTipoOperacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVReposicionChip")) Or sTipoOperacion = tipoOperacion_PackPrepago _
                Or sTipoOperacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("strDTVRenovacion"))) Then
                If Not bRenovYChip Then ' RENO SIN CHIP
                    bFlagTrxRegulada = False 'SOLO REPO O RENO CON CHIP PRE O POST
                End If
            End If
           

            If bFlagTrxRegulada Then
                If sTipoOpeDesc = "VENTA NORMAL /ALTA" Or sTipoOpeDesc = "VENTA ALTA" Then
                    sTrxValidaNotificacion = Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionAlta)
                ElseIf sTipoOpeDesc = "PORTABILIDAD PREPAGO" Or sTipoOpeDesc = "NUEVAS ALTAS PORTABILIDAD" Then
                    sTrxValidaNotificacion = Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionPortabilidad)
                ElseIf sTipoOpeDesc = "RENOVACION PACK PREPAGO" Or sTipoOpeDesc = "REPOSICION CHIP REPUESTO" Or sTipoOpeDesc = "CHIP REPUESTO" Then
                    sTrxValidaNotificacion = Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionReposicion)
                ElseIf sTipoOpeDesc = "RENOVACION + CHIP REPUESTO" Then
                    sTrxValidaNotificacion = Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionRenovacionPack)
                End If
                sRespuesta = "0"
            Else
                sRespuesta = "1"
            End If

            If sTrxValidaNotificacion.Equals(String.Empty) Then
                sTrxValidaNotificacion = sTipoOpeDesc
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTipoDocCli : " & Funciones.CheckStr(sTipoDocCli))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sNumDocCli : " & Funciones.CheckStr(sNumDocCli))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sNumeroSEC : " & Funciones.CheckStr(sNumeroSEC))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] nroPedido : " & Funciones.CheckStr(nroPedido))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTipoVenta : " & Funciones.CheckStr(sTipoVenta))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "[PROY-140846] sTrxValidaNotificacion : " & Funciones.CheckStr(sTrxValidaNotificacion))

            tipodoc = Funciones.CheckStr(sTipoDocCli)
            numdoc = Funciones.CheckStr(sNumDocCli)
            numsec = Funciones.CheckStr(sNumeroSEC)
            tipoventa = Funciones.CheckStr(sTipoVenta)
            transaccion = Funciones.CheckStr(sTrxValidaNotificacion)

        Catch exce As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "Excepcion: " & Funciones.CheckStr(exce.Message))
            sRespuesta = "-1"
            Response.Write("<script>alert('" & exce.Message & "');</script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "***********************************************")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "********** Fin Validacion CLAVE UNICA *********")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "***********************************************")
        End Try
        Return sRespuesta
        'FIN PROY-140846 - GPRD
    End Function
    Private Function GetAprobClaveUnica(ByVal tipodoc As String, ByVal numdoc As String, ByVal NroSec As String, ByVal NroPedido As String, ByVal TipoServicio As String, ByVal sTransaccion As String) As String

        Dim bRpta As Boolean = False
        Dim idLog As String = CurrentUser
        Dim oRequest As New ConsultarNotificaRequest
        Dim oResponse As New ConsultarNotificaResponse
        Dim oWSService As New RestNotificacionTransaccion
        Dim sMsjeRpta As String = "5" & ";" '& Funciones.CheckStr(AppSettings.Key_MensajeListaVaciaClaveUnica)
        Dim bExc As Boolean = False

        Try


            objFileLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- INICIO CONSULTAR CLAVE UNICA ")
            oRequest.MessageRequest.Header.HeaderRequest.consumer = Funciones.CheckStr(ConfigurationSettings.AppSettings("consumer_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.country = Funciones.CheckStr(ConfigurationSettings.AppSettings("country_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.dispositivo = Funciones.CheckStr(ConfigurationSettings.AppSettings("dispositivo_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.language = Funciones.CheckStr(ConfigurationSettings.AppSettings("language_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.modulo = Funciones.CheckStr(ConfigurationSettings.AppSettings("modulo_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.msgType = Funciones.CheckStr(ConfigurationSettings.AppSettings("msgType_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.operation = Funciones.CheckStr(ConfigurationSettings.AppSettings("operation_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.pid = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")
            oRequest.MessageRequest.Header.HeaderRequest.system = Funciones.CheckStr(ConfigurationSettings.AppSettings("system_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.timestamp = Funciones.CheckStr(ConfigurationSettings.AppSettings("timestamp_NotifTrx"))
            oRequest.MessageRequest.Header.HeaderRequest.userId = Funciones.CheckStr(ConfigurationSettings.AppSettings("userId_ConsultaNotif"))
            oRequest.MessageRequest.Header.HeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("consIpBSS_RecargaVirtual"))

            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  FIN HEADER ")
            Dim listaopc As New COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Request.ListaOpcional
            Dim listaopcional(0) As COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Request.ListaOpcional
            listaopc.campo = String.Empty
            listaopc.valor = String.Empty

            listaopcional(0) = listaopc

            Dim sModalidad As String = String.Empty
            Dim sTipoLinea As String = String.Empty
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  TipoServicio : " & Funciones.CheckStr(TipoServicio))
            If Funciones.CheckStr(TipoServicio) = "01" Then
                sModalidad = Funciones.CheckStr(ReadKeySettings.Key_TipoServicioPostpago)
                sTipoLinea = Funciones.CheckStr(ReadKeySettings.Key_TipoLineaPostpago)
            ElseIf TipoServicio = "02" Then
                sModalidad = Funciones.CheckStr(ReadKeySettings.Key_TipoServicioPrepago)
                sTipoLinea = Funciones.CheckStr(ReadKeySettings.Key_TipoLineaPrepago)
            Else
                sModalidad = Funciones.CheckStr(ReadKeySettings.Key_TipoServicioPostpago)
                sTipoLinea = Funciones.CheckStr(ReadKeySettings.Key_TipoLineaPostpago)
            End If

            oRequest.MessageRequest.Body.consultarNotificacionesRequest.tipoDoc = Funciones.CheckStr(tipodoc)
            oRequest.MessageRequest.Body.consultarNotificacionesRequest.numDoc = Funciones.CheckStr(numdoc)
            oRequest.MessageRequest.Body.consultarNotificacionesRequest.modalidad = Funciones.CheckStr(sModalidad)
            oRequest.MessageRequest.Body.consultarNotificacionesRequest.tipoLinea = Funciones.CheckStr(sTipoLinea)
            oRequest.MessageRequest.Body.consultarNotificacionesRequest.estadoNotif = Funciones.CheckStr(ReadKeySettings.Key_ConsultaEstadoClaveUnica)
            oRequest.MessageRequest.Body.consultarNotificacionesRequest.listaOpcional = listaopcional


            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  consultarNotificacionesRequest.tipoDoc : " & oRequest.MessageRequest.Body.consultarNotificacionesRequest.tipoDoc)
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  consultarNotificacionesRequest.numDoc : " & oRequest.MessageRequest.Body.consultarNotificacionesRequest.numDoc)
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  consultarNotificacionesRequest.modalidad : " & oRequest.MessageRequest.Body.consultarNotificacionesRequest.modalidad)
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  consultarNotificacionesRequest.tipoLinea : " & oRequest.MessageRequest.Body.consultarNotificacionesRequest.tipoLinea)
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  consultarNotificacionesRequest.estadoNotif : " & oRequest.MessageRequest.Body.consultarNotificacionesRequest.estadoNotif)

            oResponse = oWSService.ConsultarNotificacion(oRequest)

            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  FIN HEADER ")

            If Not oResponse Is Nothing Then
                If Not oResponse.MessageResponse.Body.consultarNotificacionesResponse Is Nothing Then
                    If Not oResponse.MessageResponse.Body.consultarNotificacionesResponse.responseStatus.codigoRespuesta Is Nothing Then
                        If oResponse.MessageResponse.Body.consultarNotificacionesResponse.responseStatus.codigoRespuesta.Equals("0") Then
                            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- ConsultarNotificacionTrx Consulta DP : OK")
                            If Not oResponse.MessageResponse.Body.consultarNotificacionesResponse.listaNotificaciones Is Nothing Then
                                objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- ConsultarNotificacionTrx listaNotificaciones.Length  : " & Funciones.CheckStr(oResponse.MessageResponse.Body.consultarNotificacionesResponse.listaNotificaciones.Length))
                                If oResponse.MessageResponse.Body.consultarNotificacionesResponse.listaNotificaciones.Length > 0 Then
                                    Dim oListaNotificacion As New ListaNotificaciones
                                    objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion : Transaccion a Validar: " & Funciones.CheckStr(sTransaccion))
                                    For i As Integer = 0 To oResponse.MessageResponse.Body.consultarNotificacionesResponse.listaNotificaciones.Length - 1
                                        oListaNotificacion = oResponse.MessageResponse.Body.consultarNotificacionesResponse.listaNotificaciones(i)
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  INICIO oListaNotificacion ")
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.estadoNotif : " & Funciones.CheckStr(oListaNotificacion.estadoNotif))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.tipoDoc : " & Funciones.CheckStr(oListaNotificacion.tipoDoc))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.numDoc : " & Funciones.CheckStr(oListaNotificacion.numDoc))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.tipoOperacion : " & Funciones.CheckStr(oListaNotificacion.tipoOperacion))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.vigencia : " & Funciones.CheckStr(oListaNotificacion.vigencia))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.sec : " & Funciones.CheckStr(oListaNotificacion.sec))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.nroPedido : " & Funciones.CheckStr(oListaNotificacion.nroPedido))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  SEC seleccionada : " & Funciones.CheckStr(NroSec))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.centrodeAtencion : " & Funciones.CheckStr(oListaNotificacion.centrodeAtencion))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion.modalidad : " & Funciones.CheckStr(oListaNotificacion.modalidad))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  sModalidad a Validar: " & Funciones.CheckStr(sModalidad))
                                        objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  FIN oListaNotificacion ")

                                        If (((sModalidad).Equals(Funciones.CheckStr(ConfigurationSettings.AppSettings("TIPO_DEFAULT")))) Or ((sModalidad.Equals(Funciones.CheckStr(ConfigurationSettings.AppSettings("consTipoServicio")))) And ((Funciones.CheckStr(sTransaccion)).Equals(Funciones.CheckStr(ReadKeySettings.Key_DescripcionOperacionReposicion))))) Then
                                                If Funciones.CheckStr(oListaNotificacion.nroPedido).Equals(Funciones.CheckStr(NroPedido)) Then
                                                    Dim strIdTrxNotificacion As String = String.Empty
                                                    strIdTrxNotificacion = Funciones.CheckStr(oListaNotificacion.notificacionTransaccion)
                                                    Select Case Funciones.CheckStr(oListaNotificacion.estadoNotif)
                                                        Case "0"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajePendienteClaveUnica), strIdTrxNotificacion)
                                                            Exit Select
                                                        Case "1"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajePendienteClaveUnica), strIdTrxNotificacion)
                                                            Exit Select
                                                        Case "2"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAprobadaClaveUnica), strIdTrxNotificacion)
                                                            bExc = True
                                                            Exit Select
                                                        Case "3"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeRechazadaClaveUnica), strIdTrxNotificacion)
                                                            Exit Select
                                                        Case "4"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAnuladaClaveUnica), strIdTrxNotificacion)
                                                            Exit Select
                                                        Case "6"
                                                            sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAnuladaClaveUnica), strIdTrxNotificacion)
                                                            Exit Select
                                                        Case Else
                                                            sMsjeRpta = Funciones.CheckStr(ReadKeySettings.Key_MensajeErrorConsultaClaveUnica)
                                                            Exit Select
                                                    End Select
                                                    sMsjeRpta = Funciones.CheckStr(oListaNotificacion.estadoNotif) & ";" & sMsjeRpta
                                                    objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion : sRpta: " & Funciones.CheckStr(sMsjeRpta)).ToString()
                                                    Exit For
                                                End If
                                        Else
                                            If Funciones.CheckStr(oListaNotificacion.sec).Equals(Funciones.CheckStr(NroSec)) Then
                                                Dim strIdTrxNotificacion As String = String.Empty
                                                strIdTrxNotificacion = Funciones.CheckStr(oListaNotificacion.notificacionTransaccion)
                                                Select Case Funciones.CheckStr(oListaNotificacion.estadoNotif)
                                                    Case "0"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajePendienteClaveUnica), strIdTrxNotificacion)
                                                        Exit Select
                                                    Case "1"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajePendienteClaveUnica), strIdTrxNotificacion)
                                                        Exit Select
                                                    Case "2"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAprobadaClaveUnica), strIdTrxNotificacion)
                                                        bExc = True
                                                        Exit Select
                                                    Case "3"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeRechazadaClaveUnica), strIdTrxNotificacion)
                                                        Exit Select
                                                    Case "4"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAnuladaClaveUnica), strIdTrxNotificacion)
                                                        Exit Select
                                                    Case "6"
                                                        sMsjeRpta = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeAnuladaClaveUnica), strIdTrxNotificacion)
                                                        Exit Select
                                                    Case Else
                                                        sMsjeRpta = Funciones.CheckStr(ReadKeySettings.Key_MensajeErrorConsultaClaveUnica)
                                                        Exit Select
                                                End Select
                                                sMsjeRpta = Funciones.CheckStr(oListaNotificacion.estadoNotif) & ";" & sMsjeRpta
                                                objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  oListaNotificacion : sRpta: " & Funciones.CheckStr(sMsjeRpta)).ToString()
                                                Exit For
                                            End If
                                        End If
                                    Next
                                Else
                                    sMsjeRpta = "5" & ";"
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "-  sMsjeRpta : " & Funciones.CheckStr(sMsjeRpta))
        Catch ex As Exception
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- ConsultarNotificacionTrx ERRORR: METODO CONSULTAR NOTIFICACION")
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- ConsultarNotificacionTrx ex.Message: " & ex.Message)
            objLog.Log_WriteLog(pathFile, strArchivo, numdoc & "- ConsultarNotificacionTrx ex.StackTrace: " & ex.StackTrace)
            sMsjeRpta = "-1;" & Funciones.CheckStr(ReadKeySettings.Key_MensajeErrorConsultaClaveUnica)
            bRpta = False
        End Try
        Return sMsjeRpta
    End Function
''IDEA300216 FIN
End Class