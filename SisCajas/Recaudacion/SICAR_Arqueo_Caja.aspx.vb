Imports System.Globalization
Imports Thycotic.Web.RemoteScripting
Imports COM_SIC_Activaciones
Imports COM_SIC_Procesa_Pagos
Imports SisCajas.Funciones

Public Class SICAR_Arqueo_Caja
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnAgregar As System.Web.UI.WebControls.Button
    Protected WithEvents tdBtnGrabar As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents txtMontoSicar As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoArqueo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCantidad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaArqueo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cboValor As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtRespCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRespArqueo As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidRespArqueo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCantidad As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMonto As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtMontoFondo As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMontoSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMontoFondo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMontoArqueo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoArqueo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidRespCaja As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidValor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTotalEfectivoSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTotalTarjetaSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTotalChequeSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblInfo As System.Web.UI.WebControls.Label
    Protected WithEvents hidTotalEfectivo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTotalTarjeta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTotalCheque As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIsPostBack As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidDiv As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTotalEfectivoSicar As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalEfectivoIngresado As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalTarjetaSicar As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalChequeSicar As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDiferenciaEfectivo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalTarjetaIngresado As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDiferenciaTarjeta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalChequeIngresado As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDiferenciaCheque As System.Web.UI.WebControls.TextBox

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Dim objFileLog As New SICAR_Log
    Dim objCaja As COM_SIC_Cajas.clsCajas
    Dim strUsuario, strAlmacen, strOficina As String
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim dtDetalle As New DataTable("Detalle")
    Dim objclsOffline As COM_SIC_OffLine.clsOffline

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Cargar load Arqueo de Caja")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Declaraciòn de Variables")

            Dim dsTipoArqueo As DataSet
            Dim dsArqEfeSol As DataSet
            Dim dsParametros As DataSet
            Dim dsResultadoCC As DataSet
            Dim strCierreCajaMensaje As String = String.Empty
            Dim i As Integer
            Dim strCajaCerrada As String = "0"
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Declaraciòn de Variables")

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Asignación de Valores")
            dtDetalle.Columns.Add("Tipo", GetType(System.String))
            dtDetalle.Columns.Add("Valor", GetType(System.String))
            dtDetalle.Columns.Add("valorSicar", GetType(System.String))
            dtDetalle.Columns.Add("Cantidad", GetType(System.String))
            dtDetalle.Columns.Add("Monto", GetType(System.String))
            dtDetalle.Columns.Add("Total", GetType(System.String))
            dtDetalle.Columns.Add("idtipo", GetType(System.String))
            dtDetalle.Columns.Add("montoSicar", GetType(System.String))
            dtDetalle.Columns.Add("montoFondo", GetType(System.String))
            dtDetalle.Columns.Add("montoArqueo", GetType(System.String))
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Asignación de Valores")

            btnGrabar.Attributes.Add("onClick", "f_Grabar()")
            btnAgregar.Attributes.Add("onClick", "f_Agregar()")

            If Not Page.IsPostBack Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, " Obtenemos la fecha de arqueo")
                txtFechaArqueo.Text = Format(Now.Date.Day, "00") & "/" & Format(Now.Date.Month, "00") & "/" & Format(Now.Date.Year, "0000")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fecha => " & Funciones.CheckStr(txtFechaArqueo.Text))

                ValidacionProcesoCuadreCaja()

                dgDetalle.DataSource = dtDetalle
                dgDetalle.DataBind()

                Dim objOffline As New COM_SIC_OffLine.clsOffline

                txtRespCaja.Text = Trim(Session("NOMBRE_COMPLETO"))
                txtRespCaja.Enabled = False
                Me.hidRespCaja.Value = Session("USUARIO")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Codigo: " & Session("CODUSUARIO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cod. Resp. Caja: " & Session("USUARIO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Nom. Resp. Caja: " & Session("NOMBRE_COMPLETO"))

                'usuario del perfil del arqueo
                If (Session("PUSUARQNOM") Is Nothing) Then
                    Session("PUSUARQNOM") = Trim(Request.QueryString("pUsuArqNom"))
                    Session("PUSUARQCOD") = Trim(Request.QueryString("pUsuArqCod"))
                End If
                txtRespArqueo.Text = Session("PUSUARQNOM")
                txtRespArqueo.Enabled = False
                Me.hidRespArqueo.Value = Session("PUSUARQCOD")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cod. Resp. Arqueo: " & Request.QueryString("pUsuArqCod"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Nom. Resp. Arqueo: " & Request.QueryString("pUsuArqNom"))

                strAlmacen = Session("ALMACEN")
                strOficina = Session("OFICINA")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Punto de Venta: " & strAlmacen)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Oficina: " & strOficina)

                Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
                Dim CodParamRecaudacion As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoRecaudacion"))

                objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Consulta Parametros: Tipo de Arqueo")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Método : objConsultaPvu.ObtenerSISACT_Parametros")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " CodParamRecaudacion: " & CodParamRecaudacion)
                dsParametros = objConsultaPvu.ObtenerSISACT_Parametros(CodParamRecaudacion)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cantidad de registros: " & dsParametros.Tables(0).Rows.Count.ToString)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Consulta Parametros: Tipo de Arqueo")

                If Not dsParametros Is Nothing Then
                    Dim dvTipArq As New DataView
                    dvTipArq.Table = dsParametros.Tables(0)
                    dvTipArq.RowFilter = "PARAV_VALOR1='TIPOARQ'"
                    cboTipoArqueo.DataSource = dvTipArq
                    cboTipoArqueo.DataValueField = "PARAV_VALOR"
                    cboTipoArqueo.DataTextField = "PARAV_DESCRIPCION"
                    cboTipoArqueo.DataBind()
                    cboTipoArqueo.Items.Insert(0, "--Seleccione--")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " No se encontraron datos para el tipo de arqueo.")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Verificar: SISACT_PKG_CONSULTA_GENERAL.secss_con_parametro_gp")
                End If

                CargarMontosSicar()

                Try
                    strUsuario = Session("USUARIO")
                    strAlmacen = Session("ALMACEN")
                    strOficina = Session("OFICINA")
                    objCaja = New COM_SIC_Cajas.clsCajas
                    txtMontoSicar.Text = "0.00"
                    txtMontoFondo.Text = "0.00"
                    lblInfo.Text = ""
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Error.." & ex.Message)
                End Try
            Else
                Me.hidIsPostBack.Value = 0
            End If
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If Request.Item("hidVerif") = "1" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicia el metodo AgregarItem")
                AgregarItem()
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin del metodo AgregarItem")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin del metodo AgregarItem - Exception: " & ex.Message)
        End Try
    End Sub

    Private Sub AgregarItem()
        Dim strPos As String
        Dim drFila As DataRow
        Dim i As Integer
        Dim j As Integer

        Try
            For i = 0 To dgDetalle.Items.Count - 1
                drFila = dtDetalle.NewRow()
                For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                    drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
                Next
                dtDetalle.Rows.Add(drFila)
            Next

            drFila = dtDetalle.NewRow()
            drFila.Item("Tipo") = cboTipoArqueo.SelectedItem.Text
            drFila.Item("Valor") = cboValor.SelectedItem.Text
            drFila.Item("valorSicar") = Format(Val(Me.hidValor.Value), "#######0.00")
            drFila.Item("Cantidad") = Me.hidCantidad.Value
            drFila.Item("Monto") = Format(Val(Me.hidMonto.Value), "#######0.00")
            drFila.Item("Total") = Format(Val(Me.hidCantidad.Value) * Val(Me.hidMonto.Value), "#######0.00")
            drFila.Item("idtipo") = cboTipoArqueo.SelectedItem.Value
            drFila.Item("montoSicar") = Format(Val(Me.hidMonto.Value), "#######0.00")
            drFila.Item("montoFondo") = Format(Val(txtMontoFondo.Text), "#######0.00")
            drFila.Item("montoArqueo") = 0

            dtDetalle.Rows.Add(drFila)
            dgDetalle.DataSource = dtDetalle
            dgDetalle.DataBind()

            txtCantidad.Text = ""
            txtMonto.Text = ""
            txtMontoSicar.Text = "0.00"
            cboValor.Items.Clear()
            cboTipoArqueo.SelectedIndex = 0

            btnGrabar.Visible = True
            CalculaTotales()
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " AgregarItem - Exception: " & ex.Message)
        End Try
    End Sub

    Private Sub RegistrarArqueoCaja()
        Dim strCodRpt, strMsjRpt As String
        Dim objGestionaRecaudacionesRequest As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionRequest
        Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
        Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
        Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
        Dim objauditRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.BEAuditRequest
        Dim objGestiona As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion
        Dim descTrans As String

        objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
        objHeaderRequest.dispositivo = ""
        objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
        objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
        objHeaderRequest.operation = "registrarArqueoCaja"
        objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
        objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
        objHeaderRequest.userId = ""
        objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))


        objGestionaRecaudacionesRequest.MessageRequest.Header.headerRequest = objHeaderRequest

        objauditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        objauditRequest.ipAplicacion = CurrentTerminal
        objauditRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
        objauditRequest.usuarioAplicacion = CurrentUser

        '---------------------------------------------
        Dim i As Integer
        Dim strTramaArqueo As String = ""
        Dim strTotalSicar As String = 0

        strTramaArqueo = ""
        For i = 0 To dgDetalle.Items.Count - 1
            '1=Tipo, 2=desValor, 3=valorSicar, 4=Cantidad, 5=Monto, 6=Total, 7=idtipo, 8=montoSicar, 9=montoFondo, 10=montoArqueo

            If dgDetalle.Items(i).Cells(7).Text = 1 Then
                strTotalSicar = Me.hidTotalEfectivoSicar.Value
            ElseIf dgDetalle.Items(i).Cells(7).Text = 2 Then
                strTotalSicar = Me.hidTotalTarjetaSicar.Value
            Else
                strTotalSicar = Me.hidTotalChequeSicar.Value
            End If

            strTramaArqueo = strTramaArqueo + "" & "0" & "," & _
            dgDetalle.Items(i).Cells(7).Text & "," & _
            Me.txtFechaArqueo.Text & "," & _
            Trim(Session("ALMACEN")) & "," & _
            Trim(Session("OFICINA")) & "," & _
            Me.hidRespCaja.Value & "," & _
            Me.hidRespArqueo.Value & "," & _
            "PEN" & "," & _
            strTotalSicar & "," & _
            dgDetalle.Items(i).Cells(9).Text & "," & _
            dgDetalle.Items(i).Cells(10).Text & "," & _
            dgDetalle.Items(i).Cells(2).Text & "," & _
            dgDetalle.Items(i).Cells(3).Text & "," & _
            dgDetalle.Items(i).Cells(5).Text & "," & _
            dgDetalle.Items(i).Cells(4).Text & "," & _
            dgDetalle.Items(i).Cells(6).Text & "|"
        Next
        strTramaArqueo = Mid(strTramaArqueo, 1, Len(strTramaArqueo) - 1)
        '---------------------------------------------

        objFileLog.Log_WriteLog(pathFile, strArchivo, " --- Inicia Parametros RegistrarArqueoCaja ---")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " strTramaArqueo: " & strTramaArqueo)
        objGestionaRecaudacionesRequest.MessageRequest.body.datosClob = strTramaArqueo
        objFileLog.Log_WriteLog(pathFile, strArchivo, " --- Fin Parametros Trama RegistrarArqueoCaja ---")

        objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
        objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
        objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
        objBEAuditoriaREST.USRAPP = CurrentUser
        objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("strAplicacionSISACT")
        objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
        objBEAuditoriaREST.userId = CurrentUser
        objBEAuditoriaREST.nameRegEdit = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, " IDTRANSACCION: " & objBEAuditoriaREST.IDTRANSACCION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " IPAPLICACION: " & objBEAuditoriaREST.IPAPLICACION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " idTransaccionNegocio: " & objBEAuditoriaREST.idTransaccionNegocio)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " APLICACION: " & objBEAuditoriaREST.APLICACION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " applicationCodeWS: " & objBEAuditoriaREST.applicationCodeWS)

        objGestionaRecaudacionesResponse = objGestiona.RegistrarArqueoCaja(objGestionaRecaudacionesRequest, objBEAuditoriaREST)

        strCodRpt = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
        strMsjRpt = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

        objFileLog.Log_WriteLog(pathFile, strArchivo, " Response WS RegistrarCampana")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " COD_RPTA: " & strCodRpt)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " MSG_RPTA: " & strMsjRpt)

        If (strCodRpt.Equals("0")) Then
            Response.Write("<script>alert('Se grabo correctamente el arqueo de caja')</script>")
            btnGrabar.Enabled = False
            btnAgregar.Enabled = False
            
            descTrans = "Exito al Registrar Arqueo de Caja. " & strMsjRpt & "|Cod. Resp. Arqueo:" & Me.hidRespCaja.Value & "|Resp. Arqueo:" & txtRespArqueo.Text
            RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsRegistraArqueoCajaGR"), descTrans)
        Else
            descTrans = "Error al Registrar Arqueo de Caja. " & strMsjRpt & "|Cod. Resp. Arqueo:" & Me.hidRespCaja.Value & "|Resp. Arqueo:" & txtRespArqueo.Text
            RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsRegistraArqueoCajaGR"), descTrans)
            Response.Write("<script>alert('Se presento un error al grabar el arqueo de caja, Error WS: " & strMsjRpt & "')</script>")
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim descTrans As String

        Try
            If Request.Item("hidVerif") = "1" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicia el metodo RegistrarArqueoCaja")
                RegistrarArqueoCaja()
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin del metodo RegistrarArqueoCaja")
            End If
        Catch ex As Exception
            descTrans = "Exception al Registrar Arqueo de Caja. " & ex.Message & "|Cod. Resp. Arqueo:" & Me.hidRespCaja.Value & "|Resp. Arqueo:" & txtRespArqueo.Text
            RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsRegistraArqueoCajaGR"), descTrans)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin del metodo RegistrarArqueoCaja - Exception: " & ex.Message)
            Response.Write("<script>alert('Se presento un error al grabar el arqueo de caja, " & ex.Message & "')</script>")
        End Try
    End Sub

    Private Sub dgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.ItemCommand
        Dim drFila As DataRow
        Dim i, j As Integer

        For i = 0 To dgDetalle.Items.Count - 1
            drFila = dtDetalle.NewRow()
            For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
            Next
            dtDetalle.Rows.Add(drFila)
        Next

        dtDetalle.Rows.RemoveAt(e.Item.ItemIndex)

        dgDetalle.DataSource = dtDetalle
        dgDetalle.DataBind()

        CalculaTotales()

        If dgDetalle.Items.Count = 0 Then
            btnGrabar.Visible = False
        End If
    End Sub

    Private Sub cboTipoArqueo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoArqueo.SelectedIndexChanged
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim ds As New DataSet
        Dim CodParamRecaudacion As Int64 = Funciones.CheckInt64(ConfigurationSettings.AppSettings("key_ParanGrupoRecaudacion"))

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio Consulta Parametros: Valores")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " CodParamRecaudacion" & " : " & Funciones.CheckStr(CodParamRecaudacion))
            ds = objConsultaPvu.ObtenerSISACT_Parametros(CodParamRecaudacion)

            If Not ds Is Nothing Then
                Dim dvValor As New DataView
                dvValor.Table = ds.Tables(0)

                If cboTipoArqueo.SelectedValue = 1 Then
                    dvValor.RowFilter = "PARAV_VALOR1='ARQEFESOL'"
                ElseIf cboTipoArqueo.SelectedValue = 2 Then
                    dvValor.RowFilter = "PARAV_VALOR1='ARQTAR'"
                ElseIf cboTipoArqueo.SelectedValue = 3 Then
                    dvValor.RowFilter = "PARAV_VALOR1='ARQCHE'"
                End If

                cboValor.DataSource = dvValor
                cboValor.DataValueField = "PARAV_VALOR"
                cboValor.DataTextField = "PARAV_DESCRIPCION"
                cboValor.DataBind()
                cboValor.Items.Insert(0, "--Seleccione--")

                objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Consulta Parametros: Valores")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, " No se encontraron datos para los valores.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Verificar: SISACT_PKG_CONSULTA_GENERAL.secss_con_parametro_gp")
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin del metodo RegistrarArqueoCaja - Exception: " & ex.Message)
        End Try
    End Sub

    Private Sub cboValor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboValor.SelectedIndexChanged
        If cboTipoArqueo.SelectedValue = 1 Then
            txtMonto.Text = cboValor.SelectedValue
            txtCantidad.Text = "1"
            txtCantidad.Enabled = True
            txtMonto.Enabled = False
        ElseIf cboTipoArqueo.SelectedValue = 2 Then
            txtMonto.Text = ""
            txtCantidad.Text = "1"
            txtCantidad.Enabled = False
            txtMonto.Enabled = True
        ElseIf cboTipoArqueo.SelectedValue = 3 Then
            txtMonto.Text = ""
            txtCantidad.Text = "1"
            txtCantidad.Enabled = False
            txtMonto.Enabled = True
        End If

        '-----
        Dim strConcepto As String = ""
        Dim dblMonto As Double = 0.0
        If cboTipoArqueo.SelectedValue = 1 Then
            strConcepto = "Ingreso total en efectivo"
        ElseIf cboTipoArqueo.SelectedValue = 2 Then
            If cboValor.SelectedValue = 1 Then
                strConcepto = "-- Total por Tarjetas MasterCard"
            ElseIf cboValor.SelectedValue = 2 Then
                strConcepto = "-- Total por Tarjetas Visa"
            ElseIf cboValor.SelectedValue = 3 Then
                strConcepto = "-- Total por Tarjetas American Express"
            ElseIf cboValor.SelectedValue = 4 Then
                strConcepto = "-- Total por Tarjetas Dinners"
            End If
        ElseIf cboTipoArqueo.SelectedValue = 3 Then
            strConcepto = "Ingreso Total Cheque"
        End If
        txtMontoSicar.Text = ObtenerMontoSicar(strConcepto)
        txtMontoSicar.Text = Format(Val(txtMontoSicar.Text), "#######0.00")
        Me.hidValor.Value = Val(txtMontoSicar.Text)
        '-----
    End Sub

    Private Sub RegistrarAuditoria(ByVal strCodTrans As String, ByVal descTrans As String)
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
            Dim auditoriaGrabado As Boolean

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CargarMontosSicar()
        Dim dsReturn As DataSet
        Dim objOffLine As New COM_SIC_OffLine.clsOffline
        Dim strFecha As String
        Dim strCodUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        Dim strMsj As String = String.Empty

        strFecha = CDate(txtFechaArqueo.Text).ToString("yyyyMMdd")
        dsReturn = objOffLine.GetMontoCuadre(CStr(Session("ALMACEN")), strFecha, strCodUsuario)

        If dsReturn.Tables(0).Rows.Count <= 0 Then
            Session("MONTOSICAR") = Nothing
            Exit Sub
        Else
            Session("MONTOSICAR") = dsReturn.Tables(0)
        End If
    End Sub

    Private Function ObtenerMontoSicar(ByVal strTipoValor As String) As Double
        Dim dvMonto As New DataView
        Dim monto As Double = 0
        Dim strMsj As String = String.Empty

        If (Session("MONTOSICAR") Is Nothing) Then
            strMsj = "No se ha procesado aún el Cuadre de Caja para el " & CStr(txtFechaArqueo.Text) & " para obtener el valor de Sicar."
            'Response.Write("<script language=jscript> alert('" & strMsj & "'); </script>")
            monto = 0.0
            lblInfo.Text = strMsj
        Else
            lblInfo.Text = ""
            dvMonto.Table = Session("MONTOSICAR")
            dvMonto.RowFilter = "TRIM(DESC_CONCEPTO) = TRIM('" & strTipoValor & "')"

            Dim drvResultado As DataRowView = dvMonto.Item(0)
            If Not drvResultado Is Nothing Then
                With drvResultado
                    monto = Trim(drvResultado("MONTO"))
                End With
            End If
        End If

        Return monto
    End Function

    Private Sub txtFechaArqueo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFechaArqueo.TextChanged
        txtMontoSicar.Text = "0.00"
        ValidacionProcesoCuadreCaja()
        CargarMontosSicar()
    End Sub

    Private Sub CalculaTotales()
        Dim i As Integer

        txtTotalEfectivoSicar.Text = "0.00"
        txtTotalEfectivoIngresado.Text = "0.00"
        txtTotalTarjetaSicar.Text = "0.00"
        txtTotalTarjetaIngresado.Text = "0.00"
        txtTotalChequeSicar.Text = "0.00"
        txtTotalChequeIngresado.Text = "0.00"

        For i = 0 To dgDetalle.Items.Count - 1
            If dgDetalle.Items(i).Cells(1).Text = "ARQUEO EFECTIVO" Then
                txtTotalEfectivoSicar.Text = Val(dgDetalle.Items(i).Cells(3).Text) + Val(txtMontoFondo.Text)
                txtTotalEfectivoIngresado.Text += Val(dgDetalle.Items(i).Cells(6).Text)
            End If
            If dgDetalle.Items(i).Cells(1).Text = "ARQUEO TARJETA" Then
                txtTotalTarjetaSicar.Text += Val(dgDetalle.Items(i).Cells(3).Text)
                txtTotalTarjetaIngresado.Text += Val(dgDetalle.Items(i).Cells(6).Text)
            End If
            If dgDetalle.Items(i).Cells(1).Text = "ARQUEO CHEQUE" Then
                txtTotalChequeSicar.Text = Val(dgDetalle.Items(i).Cells(3).Text)
                txtTotalChequeIngresado.Text = Val(dgDetalle.Items(i).Cells(6).Text)
            End If
        Next

        txtTotalEfectivoSicar.Text = Format(Val(txtTotalEfectivoSicar.Text), "#######0.00")
        txtTotalEfectivoIngresado.Text = Format(Val(txtTotalEfectivoIngresado.Text), "#######0.00")
        txtDiferenciaEfectivo.Text = Format(Val(txtTotalEfectivoSicar.Text) - Val(txtTotalEfectivoIngresado.Text), "#######0.00")

        txtTotalTarjetaSicar.Text = Format(Val(txtTotalTarjetaSicar.Text), "#######0.00")
        txtTotalTarjetaIngresado.Text = Format(Val(txtTotalTarjetaIngresado.Text), "#######0.00")
        txtDiferenciaTarjeta.Text = Format(Val(txtTotalTarjetaSicar.Text) - Val(txtTotalTarjetaIngresado.Text), "#######0.00")

        txtTotalChequeSicar.Text = Format(Val(txtTotalChequeSicar.Text), "#######0.00")
        txtTotalChequeIngresado.Text = Format(Val(txtTotalChequeIngresado.Text), "#######0.00")
        txtDiferenciaCheque.Text = Format(Val(txtTotalChequeSicar.Text) - Val(txtTotalChequeIngresado.Text), "#######0.00")
    End Sub

    Private Sub ValidacionProcesoCuadreCaja()
        Dim dsResultadoCC As DataSet
        Dim i As Integer
        Dim strCajaCerrada As String = "0"
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        If Request.QueryString("estado") = "" Then
            objclsOffline = New COM_SIC_OffLine.clsOffline
            dsResultadoCC = objclsOffline.GetDatosAsignacionCajero(Session("ALMACEN"), txtFechaArqueo.Text, codUsuario)
            If Not dsResultadoCC Is Nothing Then
                For i = 0 To dsResultadoCC.Tables(0).Rows.Count - 1
                    If dsResultadoCC.Tables(0).Rows(i).Item("CAJA_CERRADA") <> "N" Then
                        Response.Write("<script>alert('La caja individual de la oficina de venta " & CStr(Session("ALMACEN")) & " - " & CType(Session("OFICINA"), String).Trim() & " ya ha sido cerrada. Aceptar para continuar.')</script>")
                        strCajaCerrada = "1"
                    End If
                Next
            End If
            If strCajaCerrada = "0" Then
                Me.hidIsPostBack.Value = 1
            End If
        ElseIf Request.QueryString("estado") = "1" Then
            If Request.QueryString("fecha") = txtFechaArqueo.Text Then
                Me.hidIsPostBack.Value = 0
                Me.hidDiv.Value = 0
            Else
                Response.Write("<script>alert('La fecha del proceso de cuadre de caja individual es diferente a la fecha de arqueo de caja.')</script>")
                Me.hidIsPostBack.Value = 0
                Me.hidDiv.Value = 1
            End If
        Else
            Me.hidIsPostBack.Value = 0
            Me.hidDiv.Value = 1
        End If
    End Sub

End Class
