Public Class grdDocumentosFijoPaginas
    Inherits SICAR_WebBase '''System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgrRecauda As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdAnular As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtNroTransac As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hidEstadoTransActual As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFechaTran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidTipoCambioFechaPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidTipoTrs As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public Const cteCODMONEDA_SOLES = "604"
    Public Const cteVALMONEDA_SOLES = "PEN"
    Public Const cteCODMONEDA_DOLARES = "840"
    Public Const cteVALMONEDA_DOLARES = "USD"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                If Not Request.QueryString("fecbus") Is Nothing AndAlso Request.QueryString("fecbus").Length > 0 Then
                    txtFecha.Value = Request.QueryString("fecbus")
                Else
                    If txtFecha.Value = String.Empty Then
                        txtFecha.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.ToString("d")
                    End If
                End If
                LeeParametros()
            End If
            If hidTipoTrs.Value = "B" Then
                    Buscar()
                    hidTipoTrs.Value = ""
            End If
        End If
    End Sub

    Private Sub Buscar()
        Dim sMensaje As String
        Try
            Me.hidFechaTran.Value = txtFecha.Value
            'Dim objSAPRecau As New SAP_SIC_Recaudacion.clsRecaudacion
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim intSAP = objOffline.Get_ConsultaSAP
            Dim objSAPRecau As Object
            'Response.Write("<script language=jscript> alert('intSAP:" + intSAP + "'); </script>")

            If intSAP = 1 Then
                objSAPRecau = New SAP_SIC_Recaudacion.clsRecaudacion
            Else
                objSAPRecau = New COM_SIC_OffLine.clsOffline
            End If
            '--obtiene datos
            'nhuaringa auto filtro

            Dim codigoCaj As String = "0000000000" & hdnUsuario.Value.ToString.Trim
            codigoCaj = codigoCaj.Substring(codigoCaj.Length - 10, 10)

            Dim dsResult3 As DataSet = objSAPRecau.Get_PoolRecaudacion(codigoCaj, txtFecha.Value, Me.hdnPuntoDeVenta.Value, String.Empty, String.Empty, String.Empty, ConfigurationSettings.AppSettings("constEstadoPagoFijo").ToString)
            Dim dsResult4 As DataSet = objSAPRecau.Get_PoolRecaudacion(codigoCaj, txtFecha.Value, Me.hdnPuntoDeVenta.Value, String.Empty, String.Empty, String.Empty, ConfigurationSettings.AppSettings("constEstadoPagoPaginas").ToString)
            '---
            Dim datPool As New DataTable
            Dim nrwFila As DataRow
            datPool = dsResult3.Tables(0)
            '--agrega el conjunto de estado=4 (Paginas Claro)
            Dim sEstadoTMP As String

            For Each drwFila As DataRow In dsResult4.Tables(0).Rows
                nrwFila = datPool.NewRow()
                nrwFila("Nro_Transaccion") = drwFila("Nro_Transaccion")
                nrwFila("RUC_DEUDOR") = drwFila("RUC_DEUDOR")
                nrwFila("Nom_Deudor") = drwFila("Nom_Deudor")
                nrwFila("Mon_Pago") = drwFila("Mon_Pago")
                nrwFila("Importe_Pago") = drwFila("Importe_Pago")
                nrwFila("Fecha_Transac") = drwFila("Fecha_Transac")
                nrwFila("Hora_Transac") = drwFila("Hora_Transac")
                nrwFila("Nro_Telefono") = drwFila("Nro_Telefono")
                nrwFila("Estado_Transac") = drwFila("Estado_Transac")
                nrwFila("Oficina_Venta") = drwFila("Oficina_Venta")
                nrwFila("Nom_Of_Venta") = drwFila("Nom_Of_Venta")
                datPool.Rows.Add(nrwFila)
            Next
            '--enlaza el conjunto recaudaciones Fijo y Paginas
            Me.dgrRecauda.DataSource = datPool
            Me.dgrRecauda.DataBind()
            '--actualiza datos
            Me.hidTipoCambioFechaPago.Value = ObtenerTipoCambio(hidFechaTran.Value)
            Me.txtNroTransac.Value = String.Empty
            Me.hidEstadoTransActual.Value = String.Empty

            '--
            sMensaje = "OK"
            If dsResult3.Tables(0).Rows.Count = 0 Then
                ShowMessage("", "No existen documentos para la fecha indicada")
            End If
        Catch ex As Exception
            sMensaje = "Hubo un error. " & ex.Message
            ShowMessage("Pool Recaudación Clientes Fijo y Páginas", sMensaje)
        Finally
            sMensaje = "Pool Recaudación Cliente Fijo y Páginas. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & _
                                "|Cajero=" & hdnUsuario.Value
            '---
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagPool"))
        End Try

    End Sub

    Private Sub LeeParametros()
        Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
        Me.intCanal.Value = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        Me.hdnUsuario.Value = Session("USUARIO")
        Me.hdnBinAdquiriente.Value = Session("ALMACEN") 'ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
        Me.hdnCodComercio.Value = Session("ALMACEN")
        Me.hdnRutaLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Me.hdnDetalleLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
        Me.Session("PAGINA_INICIAL") = "grdDocumentosFijoPaginas.aspx" '//E75810
        txtNroTransac.Value = String.Empty
        hidEstadoTransActual.Value = String.Empty
        hidTipoTrs.Value = "B"
    End Sub

    '//E75810
    Private Function ObtenerTipoCambio(ByVal strFecha As String) As Decimal

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP
        Dim obPagos As Object
        If intSAP = 1 Then
            obPagos = New SAP_SIC_Pagos.clsPagos
        Else
            obPagos = New COM_SIC_OffLine.clsOffline
        End If
        If intSAP = 1 Then
            Return Format(obPagos.Get_TipoCambio(strFecha), "#######0.00")
        Else
            Return 0
        End If
    End Function

    'Private Sub cmdBuscar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
     '   Buscar()
     '   '---valida
     '   If dgrRecauda.Items.Count <= 0 Then
     '       ShowMessage("Información", "No existen documentos para la fecha indicada.")
     '   End If
    'End Sub

    Private Sub cmdAnular_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.ServerClick
        '--
        Dim sMensaje As String = String.Empty
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecFijoPaginas")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = Me.txtNroTransac.Value '////strNumeroIdentificadorDeudor
        Dim sNroSAP As String = Me.txtNroTransac.Value '//Salva dato
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Anular Pago - Inicio")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        Try
            '---
            Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones
            Dim intAutoriza As Integer
            Dim objConf As New COM_SIC_Configura.clsConfigura

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio  FP_Inserta_Aut_Transac ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP CANAL:  " & Session("CANAL"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP ALMACEN:  " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP USUARIO:  " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP NOMBRE_COMPLETO:  " & Session("NOMBRE_COMPLETO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    INP txtNroTransac:  " & txtNroTransac.Value)

            intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), _
                                String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Me.txtNroTransac.Value, 0, 10, 0, 0, 0, 0, 0, 0, String.Empty)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    OUT intAutoriza:  " & intAutoriza.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin  FP_Inserta_Aut_Transac ")
            '----
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP NroTransac  " & txtNroTransac.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP RutaLog  " & hdnRutaLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP DetalleLog  " & Me.hdnDetalleLog.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP EstadoTransActual:  " & hidEstadoTransActual.Value)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP TipoCambio:  " & hidTipoCambioFechaPago.Value)

            If intAutoriza = 1 Then
                Dim dTipoCambio As Double = Funciones.CheckDbl(hidTipoCambioFechaPago.Value)
                Dim strResult As String = obAnul.AnularPago(ConfigurationSettings.AppSettings("CodAplicacion"), intCanal.Value, Me.hdnRutaLog.Value, _
                                            Me.hdnDetalleLog.Value, Me.hdnPuntoDeVenta.Value, Me.intCanal.Value, _
                                            Me.hdnBinAdquiriente.Value, Me.hdnCodComercio.Value, Me.hdnUsuario.Value, _
                                            Me.txtNroTransac.Value, String.Empty, String.Empty, String.Empty, True, _
                                            hidEstadoTransActual.Value, dTipoCambio)

                '---logs de recibos procesados
                If Not obAnul.sbLineasLog Is Nothing Then
                    'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & obAnul.sbLineasLog.ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, obAnul.sbLineasLog.ToString())
                End If
                obAnul.sbLineasLog = Nothing '//reinicializa
                '--Más logs
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_ServerClick)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT strResult:  " & strResult & MaptPath)
                'FIN PROY-140126
                Dim arrMensaje() As String = strResult.Split("@")
               '---
                If ExisteError(arrMensaje) Then
                    sMensaje = "Hubo un error. " & arrMensaje(1)
                    ShowMessage("Error", sMensaje)
                Else
                    sMensaje = "Se procesó exitósamente la anulación"
                    ShowMessage("Información", sMensaje)
                    Buscar()
                End If
            Else
                sMensaje = "Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador"
                ShowMessage("Información", sMensaje)
            End If

        Catch ex As Exception
            sMensaje = "Hubo un error. " & ex.Message
            ShowMessage("Recaudación Clientes Fijo y Páginas", sMensaje)
        Finally
            sMensaje = "Anulación Recaudaciones Procesadas Fijo y Páginas. " & sMensaje & ". Datos: Canal=" & intCanal.Value & "|PDV=" & hdnPuntoDeVenta.Value & "|Cajero=" & _
                                   hdnUsuario.Value & "|Nro.Documento SAP=" & sNroSAP & "." '//Me.txtNroTransac.Value
            '---
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Anular Pago - Fin")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog = Nothing
'--
            Me.txtNroTransac.Value = String.Empty
            Me.hidEstadoTransActual.Value = String.Empty
            '--registra auditoria
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("codTrxFijaPagAnularPago"))
        End Try

    End Sub

    Private Function ExisteError(ByVal arrMensaje() As String) As Boolean
        Dim bresult As Boolean = True

        If arrMensaje(0).Trim().Length > 0 Then
            If IsNumeric(arrMensaje(0).Trim()) Then
                If Integer.Parse(arrMensaje(0).Trim()) = 0 Then
                    bresult = False
                End If
            End If
        End If

        Return bresult
    End Function

    Public Sub ShowMessage(ByVal pTitulo As String, ByVal pMensaje As String)
        If Not Me.IsStartupScriptRegistered(pTitulo) Then
            Me.RegisterStartupScript(pTitulo, "<script> alert('" + pMensaje + "'); </script>")
        End If
    End Sub

    Public Sub ShowCommand(ByVal pTitulo As String, ByVal pCommand As String)
        If Not Me.IsStartupScriptRegistered(pTitulo) Then
            Me.RegisterStartupScript(pTitulo, "<script> " & pCommand & "</script>")
        End If
    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception
            ' Throw New Exception("Error Registrar Auditoria.")
        End Try

    End Sub

    Private Sub dgrRecauda_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrRecauda.ItemDataBound
        '--
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblControl As Label = CType(e.Item.FindControl("lblTipoMoneda"), Label)
            Dim sCodigo As String
            If Not lblControl Is Nothing Then
                sCodigo = lblControl.Text
                '--asigna descripcion correspondiente a moneda
                If sCodigo = cteCODMONEDA_SOLES Then
                    lblControl.Text = cteVALMONEDA_SOLES
                ElseIf sCodigo = cteCODMONEDA_DOLARES Then
                    lblControl.Text = cteVALMONEDA_DOLARES
                Else '//Moneda desconocida
                    lblControl.Text = "MONEDA " & sCodigo
                End If
            End If
        End If
    End Sub

End Class
