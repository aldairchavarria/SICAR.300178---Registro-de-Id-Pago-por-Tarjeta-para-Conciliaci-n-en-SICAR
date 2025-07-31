Imports SisCajas.Funciones
Imports COM_SIC_Activaciones
Imports System.Globalization

Public Class poolConsultaPagos
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDescompensar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtRbPagos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdAnularPago As System.Web.UI.WebControls.Button
    Protected WithEvents cmdAnular As System.Web.UI.WebControls.Button
    Protected WithEvents btnReimAnul As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents tdReimAnul As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdEspReAnul As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents cmdProcesarpago As System.Web.UI.WebControls.Button
    Protected WithEvents cmdProcesar As System.Web.UI.WebControls.Button
    Protected WithEvents Procesar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdDescompensar As System.Web.UI.WebControls.Button
    Protected WithEvents cmdReasignar As System.Web.UI.WebControls.Button
    Protected WithEvents btnReasignar As System.Web.UI.WebControls.Button
    Protected WithEvents hidParamImpresion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdFormaPago As System.Web.UI.WebControls.Button 'PROY-27440


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim ds As DataSet
    Dim drFila As DataRow
    Dim strOficinaVta As String
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'Dim objPedidoProcesar As New COM_SIC_Procesa_Pagos.PedidoProcesar
    Dim objclsOffline As New COM_SIC_OffLine.clsOffline
    Dim dtsap, dtsicar, dtsap2, dtncanje As DataTable
    Dim objLog As New SICAR_Log


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            '  btnReasignar.Attributes.Add("onClick", "f_Reasignacion()")

            'CAMBIADO POR FFS INICIO
            'btnDescompensar.Attributes.Add("onClick", "f_Procesar()")
            'CAMBIADO POR FFS FIN

            'Dim ds As DataSet
            Dim strFecha As String
            'Response.Write(Page.IsPostBack.ToString)                                     


            If Not Page.IsPostBack Then
                If Not Session("mensajeErrorLineasDesactivadas") Is Nothing And Session("mensajeErrorLineasDesactivadas") <> "" Then
                    Response.Write("<script>alert('" & Session("mensajeErrorLineasDesactivadas") & "')</script>")
                    Session.Remove("mensajeErrorLineasDesactivadas")
                End If
                If Not Session("mensajeErrorLineasDesactivadasDOL") Is Nothing And Session("mensajeErrorLineasDesactivadasDOL") <> "" Then
                    Response.Write("<script>alert('" & Session("mensajeErrorLineasDesactivadasDOL") & "')</script>")
                    Session.Remove("mensajeErrorLineasDesactivadasDOL")
                End If
                If Not Session("mensajeErrorBonoPrepago") Is Nothing And Session("mensajeErrorBonoPrepago") <> "" Then
                    Response.Write("<script>alert('" & Session("mensajeErrorBonoPrepago") & "')</script>")
                    Session.Remove("mensajeErrorBonoPrepago")
                End If
                If Not Session("AnulacionBSCSRenovacionCorporativa") Is Nothing And Session("AnulacionBSCSRenovacionCorporativa") <> "" Then
                    Response.Write("<script>alert('" & Session("AnulacionBSCSRenovacionCorporativa") & "')</script>")
                    Session.Remove("AnulacionBSCSRenovacionCorporativa")
                End If
                If Not Session("ErrorAnulacionRPM6") Is Nothing And Session("ErrorAnulacionRPM6") <> "" Then
                    Response.Write("<script>alert('" & Session("ErrorAnulacionRPM6") & "')</script>")
                    Session.Remove("ErrorAnulacionRPM6")
                End If
                If Not Session("msgErrorMigracionSaldoTFI") Is Nothing And Session("msgErrorMigracionSaldoTFI") <> "" Then
                    Response.Write("<script>alert('" & Session("msgErrorMigracionSaldoTFI") & "')</script>")
                    Session.Remove("msgErrorMigracionSaldoTFI")
                End If
                If Not Session("msgNotaCanje") Is Nothing And Session("msgNotaCanje") <> "" Then
                    Response.Write("<script>alert('" & Session("msgNotaCanje") & "')</script>")
                    Session.Remove("msgNotaCanje")
                End If
                If Session("FechaPago") = "" Then
                    txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
                Else
                    txtFecha.Text = Session("FechaPago")
                    Session("FechaPago") = ""
                End If

            End If

            If Trim(Session("CodImprTicket")) <> "" Then
                tdReimAnul.Visible = True
                tdEspReAnul.Visible = True
            Else
                tdReimAnul.Visible = False
                tdEspReAnul.Visible = False
            End If

            strOficinaVta = Session("ALMACEN") ' oficina de vta

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
            '*****************************************************************************************'
            '*** LLamamos al mètodo que va a consultar la oficina de venta de la Lista Precios *****'
            '******************************************************************************************'
            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio consulta cargar grilla")
                CargarGrilla(ConsultaPuntoVenta(strOficinaVta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta cargar grilla")
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al tratar de llamar al mètodo cargar grilla.")
            End Try
            '******************************************************************************************'
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'

            '  btnPagar.Attributes.Add("onclick", "f_Pagos();")     'Esto ya estaba comentado, hasta antes del 15.12.14
            'CargarGrilla(strOficinaVta)                            'Linea que venia funcionando hasta el cambio por la consulta del punto de venta del SP ListaPrecios.                         

            If Len(Trim(Session("strMensajeCaja"))) > 0 Then
                Response.Write("<script>alert('" & Session("strMensajeCaja") & "')</script>")
                Session("strMensajeCaja") = ""
            End If

            'If ds.Tables(0).Rows.Count = 0 Then
            If dtsap.Rows.Count = 0 Then
                Response.Write("<script> alert('No existen documentos para la fecha indicada')</script>")
            End If
        End If
    End Sub

    '****************************************************'
    '****************************************************'
    '++++METODO PARA HACER CONSULTA DEL PUNTO DE VENTA+++'
    '****************************************************'
    '****************************************************'
    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Dim strIdentifyLog As String = ""
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al tratar de consultar el Punto de Venta")
            Exit Function
        End Try
    End Function


    '****************************************************************************'
    '*** Everis - " " - 15.12.2014 ***'
    '***El parámetro que recibe la grilla es consultado en el SP ListaPrecios.***'
    '****************************************************************************'
    Private Sub CargarGrilla(ByVal strOficinaVta As String)
        'Dim objPool As New SAP_SIC_Pagos.clsPagos
        Dim objclsOffline As New COM_SIC_OffLine.clsOffline
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap


        Dim dtcon As New DataTable
        'Dim dtsicar As DataTable
        Dim dsConsulta As New DataSet

        Dim strIdentifyLog As String = strOficinaVta
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Get_ConsultaPagosUsuario(Zpvu_Rfc_Con_Pagos_Usuario)")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha: " & txtFecha.Text)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "USUARIO: " & Session("USUARIO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOficinaVta: " & strOficinaVta)

        dtsap = obj.ConsultaPoolPagos(ConfigurationSettings.AppSettings("ESTADO_PAG"), DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), strOficinaVta, Session("USUARIO"))
        dtncanje = obj.ConsultaPoolNotasCanje(ConfigurationSettings.AppSettings("ESTADO_PAG"), DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), strOficinaVta, Session("USUARIO"))

        dtsap.Columns.Add("Estado_SAP", GetType(String))
        dtsap.Columns.Add("ID_T_TRS_PEDIDO", GetType(String))
        dtsap.Columns.Add("PEDMC_TIPO_ENTREGA", GetType(String))'PROY-140397C-MCKINSEY -> JSQ

        dtncanje.Columns.Add("Estado_SAP", GetType(String))
        dtncanje.Columns.Add("ID_T_TRS_PEDIDO", GetType(String))
        dtncanje.Columns.Add("PEDMC_TIPO_ENTREGA", GetType(String))'PROY-140397C-MCKINSEY -> JSQ

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
                        row("PAGON_INICIAL") = CuotaInicial '666
                        row.EndEdit()
                    End If
                Next
            Next
        End If
        'PROY-30166 -IDEA-38863 - FIN
        Dim i As Integer
        Dim l As Integer
        Dim PEDIC_TIPOOFICINA As String = ""
        Dim PEDIC_ISRENTA As String = ""
        Dim DRA_ASOCIADO As String = ""
        'PROY-140397-MCKINSEY -> JSQ INICIO
        Dim TIPO_CLASE_FACTURA As String = ""

        Dim TIPO_ENTREGA As String = ""
        'PROY-140397-MCKINSEY -> JSQ FIN
        If dtsap.Rows.Count > 0 Then
            For i = 0 To dtsap.Rows.Count - 1
                dtsap.Rows.Item(i)("Estado_SAP") = "PROCESADO"

                PEDIC_ISRENTA = dtsap.Rows.Item(i)("PEDIC_ISRENTA")

                PEDIC_TIPOOFICINA = dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA")

                If PEDIC_ISRENTA = "S" Then
                    If PEDIC_TIPOOFICINA = "01" Then
                        dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA") = "CAC"
                    ElseIf PEDIC_TIPOOFICINA = "02" Then
                        dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA") = "DAC"
                    ElseIf PEDIC_TIPOOFICINA = "03" Then
                        dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA") = "CADENA"
                    End If

                    DRA_ASOCIADO = Funciones.CheckStr(dtsap.Rows.Item(i)("PEDIV_DRAASOCIADO"))
                    dtsap.Rows.Item(i)("PAGOC_CODSUNAT") = DRA_ASOCIADO
                Else
                    dtsap.Rows.Item(i)("PEDIC_TIPOOFICINA") = ""
                End If

                'PROY-140397-MCKINSEY -> JSQ INICIO

                TIPO_ENTREGA = Convert.ToString(dtsap.Rows.Item(i)("TIPO_ENTREGA"))

                If Trim(TIPO_ENTREGA) = "0" Then
                    dtsap.Rows.Item(i)("PEDMC_TIPO_ENTREGA") = "Presencial"
                ElseIf Trim(TIPO_ENTREGA) = "1" Then
                    dtsap.Rows.Item(i)("PEDMC_TIPO_ENTREGA") = "Delivery"
                Else

                    dtsap.Rows.Item(i)("PEDMC_TIPO_ENTREGA") = ""
                End If

                TIPO_CLASE_FACTURA = dtsap.Rows.Item(i)("PEDIV_DESCCLASEFACTURA")

                If (TIPO_CLASE_FACTURA = "NOTA DE CREDITO") Then
                    dtsap.Rows.Item(i)("PEDMC_TIPO_ENTREGA") = ""
                End If
                If (TIPO_CLASE_FACTURA = "NOTA DE CANJE") Then
                    dtsap.Rows.Item(i)("PEDMC_TIPO_ENTREGA") = ""
                End If

                'PROY-140397-MCKINSEY -> JSQ FIN


            Next
        End If

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
            'PROY-140397C-MCKINSEY -> JSQ INICIO
            drsap("PEDMC_TIPO_ENTREGA") = dr("PEDMC_TIPO_ENTREGA")
            ''PROY-140397C-MCKINSEY -> JSQ FIN
            dtsap.Rows.Add(drsap)
        Next

        dtsap.AcceptChanges()

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Get_ConsultaPagosUsuario(Zpvu_Rfc_Con_Pagos_Usuario)")

        dtsap.DefaultView.Sort = "PAGOD_FECHACONTA DESC" 'PROY-140397-MCKINSEY -> Jordy Sullca Q RevenixZ

        dgPool.DataSource = dtsap
        dgPool.DataBind()

    End Sub

    '----------------------------------------------------------------------------
    '--- PROY-140397 - MCKINSEY --> Jordy Sullca RevenixZ INICIO
    '----------------------------------------------------------------------------
    Protected Sub DiferenciarMultipunto(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgPool.ItemDataBound
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
    '--- PROY-140397 - MCKINSEY --> Jordy Sullca RevenixZ FIN
    '----------------------------------------------------------------------------

    'AGREGADO POR FFS INICIO
    Private Sub cmdDescompensar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDescompensar.Click


        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        'CAMBIADO POR FFS INICIO
        'Dim dvPagos As New DataView(ds.Tables(0))
        Dim dvPagos As New DataView(dtsap)
        'CAMBIADO POR FFS FIN

        Dim dsReturn As DataSet
        Dim i As Integer
        If Len(Trim(Request.Item("rbPagos"))) > 0 Then
            dvPagos.RowFilter = "VBELN='" & Request.Item("rbPagos") & "'"
            drFila = dvPagos.Item(0).Row
            dsReturn = objPagos.Get_RegistroAnulCompensacion(drFila.Item("FKDAT"), strOficinaVta, drFila.Item("XBLNR"), Session("USUARIO"))
            If Not IsNothing(dsReturn) Then
                For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    If dsReturn.Tables(0).Rows(i).Item(0) = "E" Then
                        Response.Write("<script language=javascript>alert('" & dsReturn.Tables(0).Rows(i).Item(1) & "');</script>")
                        Exit Sub
                    End If
                Next
            Else
                Response.Write("<script language = javascript>alert('Ha ocurrido un error. Por favor reintentar')</script>")
                Exit Sub
            End If
            Session("FechaPago") = drFila.Item("FKDAT")
            Response.Redirect("PoolConsultaPagos.aspx")
        End If
    End Sub
    'AGREGADO POR FFS FIN


    'Private Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    'Ejecutar Batch
    'End Sub

    Private Sub cmdProcesar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcesar.Click
        '*** temporal 11.03.2015
        Dim strOficinaVta As String = Session("ALMACEN") ' oficina de vta
        Dim strCodigo As String

        Dim params() As String = hidParamImpresion.Value.Split(CChar(";"))
        Dim strCodSAP$ = params(0)
        Dim strCodProcesar$ = params(1)
        Dim strCodSunat$ = params(2)
        Dim strTipoDoc$ = params(3)

        strCodigo = txtRbPagos.Value
        Dim strRespuesta As String
        'objPedidoProcesar.MigrarPedidoPago(strCodigo, strRespuesta)
        CargarGrilla(strOficinaVta)
        If strRespuesta = "" Then
            Me.RegisterStartupScript("imprimir", "<script language=javascript>alert('El Pago fue Procesado Correctamente, por favor Intente nuevamente');</script>")
            'Me.RegisterStartupScript("imprimirF", "<script language=javascript>var objIframe = document.getElementById('IfrmImpresion');objIframe.src = 'OperacionesImp.aspx?codRefer=" + strCodSAP + "&FactSunat=" + strCodSunat + "&Reimpresion=1&TipoDoc=" + strTipoDoc + "';</script>")
        Else
            Response.Write("<script>alert('" & strRespuesta & "');</script>")
        End If
    End Sub

    '*****Anular Pagos del Modulo
    Private Sub cmdAnularPagoMssap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnularPago.Click

        Dim sDocumentoSap As String = Request.Item("rbPagos")
        Dim obj As New COM_SIC_Cajas.clsCajas
        Dim ConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim TrsMssap As New COM_SIC_Activaciones.clsTrsMsSap
        '******WEB SERVICE DE ANULACIONES
        Dim objClsPagosWS As New COM_SIC_Activaciones.clsPagosWS

        'Variables
        Dim dsPedido As DataSet
        Dim AnuID As DataSet
        Dim CodAnulacion As String
        Dim K_ANUPN_ID As Integer
        Dim strCodOficina As String = Session("ALMACEN")
        '******VARIABLES PARA EL WEB SERVICE DE ANULACION
        Dim ClaseFactura As String
        Dim Sociedad As String
        Dim CanalDistribucion As String
        Dim Sector As String
        Dim DocuSAP As String
        Dim NroCompensacion As String
        Dim PuntoVenta As String
        Dim fechaContable As String
        Dim PAGOID As String
        Dim esNC As String
        '*Variables parde Retorno del WS
        Dim K_COD_RESPUESTA As String
        Dim K_MSJ_RESPUESTA As String
        Dim K_ID_TRANSACCION As String
        '**Para LOGS
        Dim strIdentifyLog As String

        'LMEDRANO para anular Rentas Adelantadas
        Dim ConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim nroGeneradoSap As String

        '**Variables para AUDITORIA
        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
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


        '**Vista de la Grilla
        Dim dvPagos As New DataView(dtsap)

        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim intAutoriza As Integer
        Dim strTipoTienda As String = Session("CANAL")
        Dim strNomVendedor As String = ""


        Dim strCodUsuario As String = Session("USUARIO")  ' se debe leer de una variable de sesion
        Dim i As Integer
        Dim filter As String = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
        dvPagos.RowFilter = filter
        drFila = dvPagos.Item(0).Row

        Session("drFilaDoc") = drFila

        '**Para LOGS
        strIdentifyLog = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))

        intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, ConfigurationSettings.AppSettings("codAplicacion"), strCodUsuario, Session("NOMBRE_COMPLETO"), "", "", _
                            Funciones.CheckStr(drFila("PEDIV_NOMBRECLIENTE")), "", "", drFila("PEDIN_NROPEDIDO"), 0, 3, 0, 0, 0, 0, 0, 0, "", strNomVendedor)
        Try

            If intAutoriza = 1 Then

                If CStr(drFila("PEDIC_CLASEFACTURA")) <> ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    'Dim objConsultaMsSap As New COM_SIC_Activaciones.clsConsultaMsSap
                    Dim strRespuesta As String
                    Try
                        ConsultaMssap.Valida_Renta(Funciones.CheckDbl(txtRbPagos.Value), strRespuesta)
                        If Trim(strRespuesta) = "1" Then
                            Dim dtsapAnulados As DataTable
                            Dim intRentaAsociada As Integer = 0
                            dtsapAnulados = ConsultaMssap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), ConsultaPuntoVenta(strCodOficina), "") 'txtFecha.Text, "0001")
                            If Not IsNothing(dtsapAnulados) Then
                                For i = 0 To dtsapAnulados.Rows.Count - 1
                                    If (dtsapAnulados.Rows.Item(i)("PEDIN_PEDIDOALTA") = Funciones.CheckDbl(txtRbPagos.Value)) Then
                                        'Session("MSG_RENTA") = "El documento no se puede Anular. El documento tiene asociado Rentas Adelantadas que no han sido anuladas"
                                        intRentaAsociada = intRentaAsociada + 1
                                    End If
                                Next
                            End If
                            If intRentaAsociada = 0 Then
                                Response.Write("<script>alert('El documento no se puede anular el Pago. El documento tiene asociado Rentas Adelantadas que no han sido anuladas.');</script>")
                                Exit Sub
                            End If
                        End If
                    Catch ex As Exception
                        Response.Write("<script>alert('Ocurrio un error al Anular Pedido de Renta. Volver a intentar.');</script>")
                        Exit Sub
                    End Try
                End If

                '***Consulta PEDIDO 
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Consultar Pedido - SP: SSAPSS_PEDIDO")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Nro PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Punto de Venta: " & ConsultaPuntoVenta(Session("ALMACEN")))
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Cod Interlocutor: " & "")

                dsPedido = ConsultaMssap.ConsultaPedido(drFila.Item("PEDIN_NROPEDIDO"), ConsultaPuntoVenta(Session("ALMACEN")), "")

                nroGeneradoSap = drFila.Item("PEDIN_NROPEDIDO")

                If dsPedido.Tables.Count > 0 Then
                    If dsPedido.Tables(0).Rows.Count > 0 Then

                        'PAGOID = IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO")), "", dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO"))
                        'PAGOID = dsPedido.Tables(0).Rows(0).Item("PAGON_IDPAGO")
                        esNC = IIf(IsDBNull(dsPedido.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO")), "", dsPedido.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO"))
                        'ClaseFactura = dsPedido.Tables(0).Rows(0).Item("PEDIC_CLASEFACTURA")
                        'objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ID DE PAGO: " & PAGOID)
                    End If
                Else

                End If
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN Consultar Pedido - SP: SSAPSS_PEDIDO")
                '**FIN Consulta Pedido

                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO Validacion si la NC esta como Forma de Pago")
                objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NC Usada como Medio de Pago?: " & esNC)

                If esNC = "1" Then
                    Response.Write("<script>alert('No se pueden Anular Notas de Crédito que fueron usadas como medio de pago');</script>")
                    Exit Sub
                Else
                    strOficinaVta = ConsultaPuntoVenta(strOficinaVta)
                    'PROY-27440 INI
                    Dim bolFormaPago As Boolean = False
                    Response.Redirect("detaConsultaPagos.aspx?pc_OfiVta=" + strOficinaVta + "&pc_fecha=" + Right(txtFecha.Text, 4) + txtFecha.Text.Substring(3, 2) + Left(txtFecha.Text, 2) + "&numeroTelefono=" + drFila.Item(31) & "&FormaPago=" & bolFormaPago)
                    'PROY-27440 FIN
                    ''***Inicio ANULACION DE PAGO
                    'objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO Anulacion de pago - SP: SSAPSU_ANULARPAGO")
                    'objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PAGO: " & Funciones.CheckInt(PAGOID))
                    'objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))
                    'objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "OUT ID ANULACION: " & K_ANUPN_ID)

                    'AnuID = TrsMssap.AnularPago(PAGOID, drFila.Item("PEDIN_NROPEDIDO"))

                    'Dim IDAnulacion As String
                    'IDAnulacion = Funciones.CheckStr(IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("ANUPN_ID")), "", AnuID.Tables(0).Rows(0).Item("ANUPN_ID")))

                    'If Len(Trim(IDAnulacion)) > 0 Then
                    '    'cambiar estado flujo 
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID PEDIDO: " & drFila("PEDIN_NROPEDIDO"))
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN ID ANULACION: " & K_ANUPN_ID)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN PROCESO: " & ConfigurationSettings.AppSettings("ANULA_PAGO"))

                    '    TrsMssap.ActualizaEstadoFlujo(drFila("PEDIN_NROPEDIDO"), IDAnulacion, ConfigurationSettings.AppSettings("ANULA_PAGO"))

                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN ACTUALIZAR ESTADO FLUJO SP - SSAPSU_ESTADOFLUJODEVOLUCION")
                    'End If
                    'If ClaseFactura <> ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    '    '******WEB SERVICE DE ANULACION
                    '    If ConfigurationSettings.AppSettings("LanzarServicio") = "0" Then
                    '        Sociedad = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_SOCIEDAD")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_SOCIEDAD"))
                    '        NroCompensacion = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_PAGOSAP")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_PAGOSAP"))
                    '        PuntoVenta = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOV_OFICINAVENTA")), "", AnuID.Tables(0).Rows(0).Item("PAGOV_OFICINAVENTA"))
                    '        fechaContable = IIf(IsDBNull(AnuID.Tables(0).Rows(0).Item("PAGOD_FECHACONTA")), "", AnuID.Tables(0).Rows(0).Item("PAGOD_FECHACONTA"))

                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "INICIO WEB SERVICE DE ANULACION DE PAGO")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN NRO PEDIDO: " & drFila("PEDIN_NROPEDIDO"))
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Clase Factura: " & ClaseFactura)
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sociedad: " & Sociedad)
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Canal de Distribucion: " & CanalDistribucion)
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sector: " & Sector)
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Documento SAP: " & DocuSAP)
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Fecha Contable: " & "")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Sociedad Venta Pago: " & "")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Documento Compansacion  " & "")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Punto de Venta : " & "")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Flag: " & ConfigurationSettings.AppSettings("FLAG_ANULAR_PAGO"))
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Usuario: " & "")
                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "IN Terminal: " & "")

                    '        objClsPagosWS.AnularPedidoPago(drFila("PEDIN_NROPEDIDO"), _
                    '                                            PAGOID, "", Sociedad, _
                    '                                            "", "", "", fechaContable, "", NroCompensacion, PuntoVenta, ConfigurationSettings.AppSettings("FLAG_ANULAR_PAGO"), CurrentUser, CurrentTerminal, _
                    '                                        K_COD_RESPUESTA, K_MSJ_RESPUESTA, K_ID_TRANSACCION)


                    '        objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "FIN WEB SERVICE DE ANULACION DE PEDIDO")
                    '    End If
                    'End If
                    'Else
                    'Mi código
                    'If ClaseFactura = ConfigurationSettings.AppSettings("TIPO_RENTA_ADELANTADA") Then
                    '    Dim resp As Boolean
                    '    Dim codApli, userApli, numAsociado, dravDescTrs As String

                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "OBTENIENDO DATOS PARA LA ANULACIÓN")
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroGeneradoSap)

                    '    resp = ConsultaPvu.ObtenerDatosTransaccionales(nroGeneradoSap, numAsociado, codApli, userApli, dravDescTrs)

                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "DATOS OBNTENIDOS")
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. GENRADO SAP" & nroGeneradoSap)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. ASOCIADO" & numAsociado)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "CÓDIGO APLICACIÓN" & codApli)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "USUARIO APLICACIÓN" & userApli)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "DESC. TRS." & dravDescTrs)

                    '    If resp = False Then
                    '        Throw New Exception(" Error al Anular la Renta Adelantada. Volver a Intentar")
                    '    End If

                    '    Dim numAnulado As Int64 = 0
                    '    Dim codRespuesta As Int64 = 0
                    '    Dim msgRespuesta As String = String.Empty
                    '    Dim TrsPvu As New COM_SIC_Activaciones.clsTrsPvu

                    '    TrsPvu.AnularExtornarPagoDRA(codApli, userApli, numAsociado, dravDescTrs, numAnulado, codRespuesta, msgRespuesta)

                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "RESPUESTA ANULACIÓN")
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "NRO. ANULADO" & numAnulado)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "CÓDIGO RESPUESTA" & codRespuesta)
                    '    objLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "MENSAJE RESPUESTA" & msgRespuesta)
                    'End If

                    ''******FIN DE WS DE ANULACION

                    '**AUDITORIA
                    objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                    Response.Write("<script language=jscript> alert('Proceso de Anulación de Pago realizado con éxito!!!'); </script>")
                    CargarGrilla(ConsultaPuntoVenta(strCodOficina))
                End If
            Else
                Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
            End If
        Catch ex As Exception
            wParam5 = 0
            wParam6 = "Error en Anulación de documentos. " & ex.Message
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try

    End Sub

    '**Antiguo ANULAR - Comentado 
    'Private Sub cmdAnularPago_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnularPago.Click

    '    Dim strEstadoRegistro As String = ConfigurationSettings.AppSettings("constEstadoPagadoReposicion")
    '    Dim intFlagBloqueo As Integer
    '    Dim strCodBloqueo As String
    '    Dim strTipoBloqueo As String
    '    Dim strIccidActual As String
    '    Dim strIccidNuevo As String
    '    Dim strUsuario As String
    '    Dim strMensaje As String ' Resultado general
    '    Dim strTipoVenta As String
    '    Dim sDocumentoSap As String = Request.Item("rbPagos")
    '    Dim obj As New COM_SIC_Cajas.clsCajas

    '    Dim intExistePedidoSisact As Integer = obj.Consulta_Existe_Pedido_Reposicion(sDocumentoSap, strEstadoRegistro, intFlagBloqueo, strCodBloqueo, strTipoBloqueo, strIccidActual, strIccidNuevo, strUsuario, strTipoVenta)

    '    'Si el Pedido existe, registramos el estado ANULADO-PAGADO
    '    If intExistePedidoSisact = 1 Then
    '        Dim sMensaje As String = ""
    '        sMensaje = "No se puede anular este tipo de documento por este medio."
    '        Response.Write("<script>alert('" & sMensaje & "');</script>")
    '        Exit Sub
    '    End If

    '    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    '    'CAMBIADO POR FFS INICIO
    '    'Dim dvPagos As New DataView(ds.Tables(0))
    '    Dim dvPagos As New DataView(dtsap)
    '    'CAMBIADO POR FFS FIN

    '    Dim dsReturn As DataSet
    '    Dim objConf As New COM_SIC_Configura.clsConfigura
    '    Dim intAutoriza As Integer
    '    Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
    '    Dim strCodUsuario As String = Session("USUARIO")  ' se debe leer de una variable de sesion
    '    Dim drFila2 As DataRow
    '    Dim i As Integer
    '    'Response.Write(Request.Item("rbPagos")) : Response.End()
    '    dvPagos.RowFilter = "VBELN='" & Request.Item("rbPagos") & "'"
    '    drFila = dvPagos.Item(0).Row

    '    Dim dsPedido As DataSet
    '    Dim dsVendedor As DataSet
    '    Dim strIdVendedor As String
    '    Dim strNomVendedor As String

    '    If CStr(drFila("FKART")) = cteTIPODOC_DEPOSITOGARANTIA AndAlso CStr(drFila("NRO_DEP_GARANTIA")).Trim().Length > 0 AndAlso CLng(CStr(drFila("NRO_DEP_GARANTIA"))) > 0 Then
    '        'Anulacion de pedido asociado
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            If ds.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
    '                drFila2 = ds.Tables(0).Rows(i)
    '                Exit For
    '            End If
    '        Next

    '        If IsNothing(drFila2) Then
    '            Dim objPool As New SAP_SIC_Pagos.clsPagos
    '            Dim ds2 As DataSet
    '            ds2 = objPool.Get_ConsultaPoolFactura(strOficinaVta, txtFecha.Text, "I", "", "", "", "20", "1")
    '            For i = 0 To ds2.Tables(0).Rows.Count - 1
    '                If ds2.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds2.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
    '                    drFila2 = ds2.Tables(0).Rows(i)
    '                    Exit For
    '                End If
    '            Next
    '        End If

    '        If Not IsNothing(drFila2) Then
    '            'CARIAS: Ahora se necesita el nombre del asesor. Para este fin necesitamos:
    '            '1.- Consultar el Pedido
    '            dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drFila2("VBELN"), "")

    '        End If
    '    Else
    '        'CARIAS: Ahora se necesita el nombre del asesor. Para este fin necesitamos:
    '        '1.- Consultar el Pedido
    '        dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drFila("VBELN"), "")
    '    End If

    '    '2.- Obtener el codigo de Vendedor
    '    If Not IsNothing(dsPedido) Then
    '        strIdVendedor = dsPedido.Tables(0).Rows(0).Item("VENDEDOR")
    '    Else
    '        strIdVendedor = ""
    '    End If
    '    '3.- Obtener el nombre usando el codigo de vendedor conseguido

    '    If Len(Trim(strIdVendedor)) > 0 Then
    '        dsVendedor = objPagos.Get_ConsultaVend(strIdVendedor)
    '        If Not IsNothing(dsVendedor) Then
    '            strNomVendedor = dsVendedor.Tables(0).Rows(0).Item("NOMBRE")
    '        Else
    '            strNomVendedor = ""
    '        End If
    '    Else
    '        strNomVendedor = ""
    '    End If
    '    'CARIAS: Fin de bloque


    '    intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
    '                      drFila("NAME1"), "", drFila("XBLNR"), drFila("VBELN"), 0, 3, 0, 0, 0, 0, 0, 0, "", strNomVendedor)
    '    If intAutoriza = 1 Then
    '        If Len(txtRbPagos.Value.Trim) > 0 Then
    '            dvPagos.RowFilter = "VBELN='" & txtRbPagos.Value & "'"
    '            drFila = dvPagos.Item(0).Row
    '            Session("drFilaDoc") = drFila

    '            If IsNothing(dsPedido) Then 'Renta DTH no necesariamente esta asociada con una bolteta
    '                Dim obAnular As New clsAnulaciones
    '                Session("FechaPago") = drFila.Item("FKDAT")
    '                obAnular.AnularDepGaran(CStr(drFila("NRO_DEP_GARANTIA")), CStr(drFila("NRO_REF_DEP_GAR")), strCodUsuario, "S")
    '                Response.Redirect("poolConsultaPagos.aspx")
    '            End If

    '            ' Validación Anulación Recarga Virtual DTH
    '            If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = CheckStr(ConfigurationSettings.AppSettings("strTVPrepago")) Then
    '                For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
    '                    Dim CodArticulo As String = Funciones.CheckStr(dsPedido.Tables(1).Rows(idx).Item("ARTICULO"))
    '                    If CodArticulo = ConfigurationSettings.AppSettings("strCodArticuloDTH") Then
    '                        Response.Write("<script>alert('Ud. no está autorizado a realizar esta operación. Motivo: Recarga Virtual DTH.'); </script>")
    '                        Exit Sub
    '                    ElseIf CodArticulo = ConfigurationSettings.AppSettings("strCodArticuloRV") Then
    '                        Response.Write("<script>alert('La anulación de Recargas Virtuales se encuentra desactivada.'); </script>")
    '                        Exit Sub
    '                    End If
    '                Next
    '            End If
    '            ' Validación Anulación Recarga Virtual DTH

    '            If CStr(drFila("FKART")) = cteTIPODOC_DEPOSITOGARANTIA AndAlso CStr(drFila("NRO_DEP_GARANTIA")).Trim().Length > 0 AndAlso CLng(CStr(drFila("NRO_DEP_GARANTIA"))) > 0 Then
    '                Dim obAnular As New clsAnulaciones
    '                Session("FechaPago") = drFila.Item("FKDAT")
    '                obAnular.AnularDepGaran(CStr(drFila("NRO_DEP_GARANTIA")), CStr(drFila("NRO_REF_DEP_GAR")), strCodUsuario, "S")
    '                Response.Redirect("poolConsultaPagos.aspx")
    '            Else
    '                'ALINEACION DEL PQT-169512-TSK-36121 ---INICIO
    '                'roaming
    '                Try
    '                    'Dim strDocSap As String = drFila("VBELN")
    '                    'Dim dsPedido As New DataSet
    '                    'dsPedido = (New SapGeneralNegocios).Get_ConsultaPedido("", Session("Almacen"), sDocumentoSap, "")
    '                    Dim objcajas As New COM_SIC_Cajas.clsCajas
    '                    Dim dtDet As DataTable = CType(dsPedido.Tables(1), DataTable)
    '                    Dim nroTelefono = Right(dtDet.Rows(0)("NUMERO_TELEFONO"), 9)
    '                    Dim Flag As String = ""
    '                    Dim retorno As Int64 = 0
    '                    Dim mensaje As String = ""

    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Inicio Actualizacion Servicio Roaming")
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Telefono: " & nroTelefono)
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Flag: " & Flag)
    '                    objcajas.ActualizarServRoaming(drFila("VBELN"), nroTelefono, Flag, retorno, mensaje)
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "retorno: " & retorno)
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "mensaje: " & mensaje)
    '                Catch ex As Exception
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Error Actualizacion Servicio Roaming")
    '                Finally
    '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Fin Actualizacion Servicio Roaming")
    '                End Try

    '                'ALINEACION DEL PQT-169512-TSK-36121 ---FINAL

    '                'Para los casos de pago 0
    '                If CDbl(drFila("PAGOS")) = 0 Then
    '                    Dim dsResult As DataSet
    '                    Dim obAnular As New clsAnulaciones
    '                    Dim strVias() As String = {""}
    '                    obAnular.AnulacionDePago(drFila("VBELN"), drFila("XBLNR"), txtFecha.Text, strVias, Session("CANAL"), Session("ALMACEN"), Session("USUARIO"), strVias)

    '                    ' Para Venta de Portabilidad
    '                    ActualizaPago(CStr(drFila.Item("PEDIDO")))

    '                    ' Roaming
    '                    ActualizarPlanRoaming(CStr(drFila("VBELN")), ConfigurationSettings.AppSettings("CodigoAnulacionRoaming"))

    '                    ' Anulacion Renovacion RPM6
    '                    Dim strTelefono As String
    '                    Dim strCodArticulo As String

    '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
    '                        For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
    '                            strCodArticulo = CStr(dsPedido.Tables(1).Rows(idx).Item("ARTICULO"))
    '                            strTelefono = CStr(dsPedido.Tables(1).Rows(idx).Item("NUMERO_TELEFONO"))

    '                            If strCodArticulo = ConfigurationSettings.AppSettings("strCodArticuloRenovacion") Then
    '                                AnularRenovacionRPM6(strTelefono)
    '                            End If
    '                        Next
    '                    End If
    '                    ' Anulacion Renovacion RPM6

    '                    ' Anulacion Bono Renovacion Prepago
    '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") And _
    '                        dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA") = ConfigurationSettings.AppSettings("strDTVRenovacion") Then

    '                        Dim strIMEI, strTelfRenov As String
    '                        For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
    '                            strIMEI = Right(dsPedido.Tables(1).Rows(idx).Item("SERIE"), 15)
    '                            strTelfRenov = Right(CheckStr(dsPedido.Tables(1).Rows(idx).Item("NUMERO_TELEFONO")), 9)
    '                        Next

    '                        If ConfigurationSettings.AppSettings("ConsFlagRenovPrepago") Or _
    '                            InStr(1, ConfigurationSettings.AppSettings("ConsLineasRenovPrepago"), strTelfRenov) > 0 Then
    '                            AnularBonoPrepago(strTelfRenov, strIMEI, CheckStr(drFila("PEDIDO")))
    '                        End If

    '                    End If
    '                    ' Anulacion Bono Renovacion Prepago

    '                    Response.Redirect("poolConsultaPagos.aspx")
    '                Else ' Para los casos normales
    '                    Response.Redirect("detaConsultaPagos.aspx?pc_OfiVta=" + strOficinaVta + "&pc_fecha=" + Right(txtFecha.Text, 4) + txtFecha.Text.Substring(3, 2) + Left(txtFecha.Text, 2))
    '                End If

    '                'dsReturn = objPagos.Get_RegistroAnulCompensacion(drFila.Item("FKDAT"), "0006", drFila.Item("XBLNR"), "")
    '                'If dsReturn.Tables(0).Rows(0).Item(0) = "E" Then
    '                'Response.Write("<script language=javascript>alert('" & dsReturn.Tables(0).Rows(0).Item(1) & "');</script>")
    '                'End If
    '            End If

    '        End If

    '    Else
    '        Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
    '    End If
    'End Sub

    Private Sub cmdAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.Click
        '*******************************
        'Dim strTipoTienda As String = Session("CANAL")  ' se debe leer de una variable de sesion
        'Dim strCodUsuario As String = Session("USUARIO")  ' se debe leer de una variable de sesion
        'Dim strCodOficina As String = Session("ALMACEN") ' se debe leer de una variable de sesion
        'Dim strCodImprTicket As String = Session("CodImprTicket")
        'Dim cteTIPODOC_DEPOSITOGARANTIA As String = ConfigurationSettings.AppSettings("cteTIPODOC_DEPOSITOGARANTIA")
        ''*******************************

        'Dim objPagos As New SAP_SIC_Pagos.clsPagos

        ''CAMBIADO POR FFS INICIO
        ''Dim dvPagos As New DataView(ds.Tables(0))
        'Dim dvPagos As New DataView(dtsap)
        ''CAMBIADO POR FFS FIN



        'Dim dsReturn As DataSet
        'Dim objConf As New COM_SIC_Configura.clsConfigura
        'Dim intAutoriza As Integer
        'Dim i As Integer
        'Dim drFila2 As DataRow
        'Dim dsPedido As DataSet
        'Dim dsVendedor As DataSet
        'Dim strIdVendedor As String
        'Dim strNomVendedor As String

        ''Response.Write(txtRbPagos.Value)
        'Try
        '    ' pregunta por autorizacion para anular pago
        '    dvPagos.RowFilter = "VBELN='" & Request.Item("rbPagos") & "'"
        '    drFila = dvPagos.Item(0).Row
        '    'ALINEACION DEL PQT-169512-TSK-36121 ---- INICIO
        '    Dim strNumSerieDoc As String = ""
        '    Dim CONS_VALIDA_DOC_TICKET As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_TICKET")
        '    Dim CONS_VALIDA_DOC_BOLETA As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_BOLETA")
        '    Dim CONS_VALIDA_DOC_NOTA_CREDITO As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_NOTA_CREDITO")
        '    Dim CONS_VALIDA_DOC_FACTURA As String = ConfigurationSettings.AppSettings("CONS_VALIDA_DOC_FACTURA")

        '    If InStr(CStr(drFila("Xblnr")), "-") > 0 Then
        '        strNumSerieDoc = CStr(drFila("Xblnr")).Substring(0, InStr(CStr(drFila("Xblnr")), "-") - 1)
        '    End If

        '    If strNumSerieDoc.Equals(CONS_VALIDA_DOC_TICKET) OrElse _
        '    strNumSerieDoc.Equals(CONS_VALIDA_DOC_BOLETA) OrElse _
        '    strNumSerieDoc.Equals(CONS_VALIDA_DOC_NOTA_CREDITO) OrElse _
        '    strNumSerieDoc.Equals(CONS_VALIDA_DOC_FACTURA) Then
        '        Response.Write("<script>alert('El documento no se puede Anular.'); </script>")
        '        Exit Sub
        '    End If
        '    'ALINEACION DEL PQT-169512-TSK-36121 ----FINAL	

        '    If CStr(drFila("FKART")) = cteTIPODOC_DEPOSITOGARANTIA AndAlso CStr(drFila("NRO_DEP_GARANTIA")).Trim().Length > 0 AndAlso CLng(CStr(drFila("NRO_DEP_GARANTIA"))) > 0 Then
        '        'Anulacion de pedido asociado
        '        For i = 0 To ds.Tables(0).Rows.Count - 1
        '            If ds.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
        '                drFila2 = ds.Tables(0).Rows(i)
        '                Exit For
        '            End If
        '        Next

        '        If IsNothing(drFila2) Then
        '            Dim objPool As New SAP_SIC_Pagos.clsPagos
        '            Dim ds2 As DataSet
        '            ds2 = objPool.Get_ConsultaPoolFactura(strOficinaVta, txtFecha.Text, "I", "", "", "", "20", "1")
        '            For i = 0 To ds2.Tables(0).Rows.Count - 1
        '                If ds2.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds2.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
        '                    drFila2 = ds2.Tables(0).Rows(i)
        '                    Exit For
        '                End If
        '            Next
        '        End If

        '        If Not IsNothing(drFila2) Then
        '            'CARIAS: Ahora se necesita el nombre del asesor. Para este fin necesitamos:
        '            '1.- Consultar el Pedido
        '            dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drFila2("VBELN"), "")
        '        End If
        '    Else
        '        'CARIAS: Ahora se necesita el nombre del asesor. Para este fin necesitamos:
        '        '1.- Consultar el Pedido
        '        dsPedido = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drFila("VBELN"), "")
        '    End If

        '    '2.- Obtener el codigo de Vendedor
        '    If Not IsNothing(dsPedido) Then
        '        strIdVendedor = dsPedido.Tables(0).Rows(0).Item("VENDEDOR")
        '    Else
        '        strIdVendedor = ""
        '    End If
        '    '3.- Obtener el nombre usando el codigo de vendedor conseguido

        '    If Len(Trim(strIdVendedor)) > 0 Then
        '        dsVendedor = objPagos.Get_ConsultaVend(strIdVendedor)
        '        If Not IsNothing(dsVendedor) Then
        '            strNomVendedor = dsVendedor.Tables(0).Rows(0).Item("NOMBRE")
        '        Else
        '            strNomVendedor = ""
        '        End If
        '    Else
        '        strNomVendedor = ""
        '    End If

        '    'validación anulacion de promo modem
        '    If Valida_Anula_Prom(drFila("VBELN")) > 0 Then
        '        Response.Write("<script language=jscript> alert('" & ConfigurationSettings.AppSettings("contMsgPromPostModemPreAnula").ToString() & "'); </script>")
        '        Exit Sub
        '    End If

        '    If Not IsNothing(dsPedido) Then
        '        ' Validación Anulación Recarga Virtual DTH
        '        If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
        '            For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                Dim CodArticulo As String = Funciones.CheckStr(dsPedido.Tables(1).Rows(idx).Item("ARTICULO"))
        '                If CodArticulo = ConfigurationSettings.AppSettings("strCodArticuloDTH") Then
        '                    Response.Write("<script>alert('Ud. no está autorizado a realizar esta operación. Motivo: Recarga Virtual DTH.'); </script>")
        '                    Exit Sub
        '                ElseIf CodArticulo = ConfigurationSettings.AppSettings("strCodArticuloRV") Then
        '                    Response.Write("<script>alert('La anulación de Recargas Virtuales se encuentra desactivada.'); </script>")
        '                    Exit Sub
        '                End If
        '            Next
        '        End If
        '        ' Validación Anulación Recarga Virtual DTH

        '        'Miguel - Desactivación de las lineas para TFI
        '        'If Not IsNothing(drFila) Then
        '        '    numeroFactura = CStr(drFila("XBLNR"))
        '        'ElseIf Not IsNothing(drFila2) Then
        '        '    numeroFactura = CStr(drFila2("XBLNR"))
        '        'End If
        '        'Dim dsDetalleComp As DataSet

        '        'Miguel - Desactivación de los TFI
        '        'dsDetalleComp = objPagos.Get_ConsultaPedido(numeroFactura, Session("ALMACEN"), "", "")
        '        If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVTFI") Then
        '            Dim objDesActivacion As New COM_SIC_Activaciones.clsActivacion

        '            Dim objFileLog As New COM_SIC_Cajas.clsLog
        '            Dim transaccion As String = ConfigurationSettings.AppSettings("strDesactivacionTFI")
        '            Dim codUsuario As String = Session("codUsuario")
        '            Dim strIPServidor As String = Request.ServerVariables("REMOTE_ADDR")
        '            Dim strNombreServidor As String = Request.ServerVariables("SERVER_NAME")
        '            Dim strIPCliente As String = HttpContext.Current.Request.UserHostAddress
        '            Dim strNombreCliente As String = System.Net.Dns.GetHostByAddress(strIPCliente).HostName

        '            Dim resultadoLineasDesactivadas As String = ""
        '            Dim numerosValidados As String = ""
        '            Dim j As Integer
        '            For j = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                Dim strNumero As String = dsPedido.Tables(1).Rows(j).Item("NUMERO_TELEFONO")
        '                Dim strResultado As String
        '                Dim strTransac As String
        '                Dim strParamsIn As String
        '                Dim strParamsOut As String
        '                If strNumero <> String.Empty Or strNumero.ToString <> "" Then
        '                    If numerosValidados.IndexOf(CDbl(strNumero).ToString()) = -1 Then
        '                        Try
        '                            strParamsIn = "telefono: " + CDbl(strNumero).ToString()
        '                            objDesActivacion.FK_EjecutarDesactivacion(CDbl(strNumero).ToString(), strResultado, strTransac)
        '                            strParamsOut = "resultado: " + strResultado + " transaccion: " + strTransac
        '                            objFileLog.FK_RegistrarLog(transaccion, codUsuario, strIPCliente, strNombreCliente, strIPServidor, strNombreServidor, strParamsIn, strParamsOut, "")
        '                        Catch ex As Exception
        '                            strParamsOut = "resultado: " + strResultado + " transaccion: " + strTransac
        '                            objFileLog.FK_RegistrarLog(transaccion, codUsuario, strIPCliente, strNombreCliente, strIPServidor, strNombreServidor, strParamsIn, strResultado, ex.Message)
        '                            strResultado = 0
        '                        End Try
        '                        numerosValidados += CDbl(strNumero).ToString() & "|"
        '                        If strResultado <> "1" Then
        '                            resultadoLineasDesactivadas += CDbl(strNumero).ToString() & "|"
        '                        End If
        '                    End If
        '                End If
        '            Next
        '            If resultadoLineasDesactivadas.Trim <> "" Then
        '                Dim arrLineas() As String = resultadoLineasDesactivadas.Split("|")
        '                Dim k As Integer
        '                Dim mensaje As String = ConfigurationSettings.AppSettings("msjeErrorDesactivarLinea")
        '                For k = 0 To arrLineas.Length - 1
        '                    mensaje += "\n" & arrLineas(k)
        '                Next
        '                Response.Write("<script language=jscript> alert('" & mensaje & "'); </script>")
        '            End If
        '        End If
        '        'Fin de Desactivación de los TFI

        '        'Miguel - Desactivación de ciertos equipos PREPAGO
        '        'numeroSAP = CStr(drFila("XBLNR"))
        '        'Dim dsDetalleComp As DataSet
        '        'dsDetalleComp = objPagos.Get_ConsultaPedido(numeroSAP, Session("ALMACEN"), "", "")
        '        If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
        '            'Dim strModems As String = ConfigurationSettings.AppSettings("equiposBono").ToString()
        '            Dim strDocSap As String = drFila("VBELN")
        '            Dim strListMaterialBBA As String = ListaMaterialesBBA(strDocSap)
        '            Dim objActivacion As New COM_SIC_Activaciones.clsActivacion
        '            Dim resultadoLineasDesactivadas As String = ""
        '            Dim numerosValidados As String = ""
        '            Dim j As Integer
        '            For j = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                Dim strEquipo As String = dsPedido.Tables(1).Rows(j).Item("ARTICULO")
        '                Dim strNumero As String = dsPedido.Tables(1).Rows(j).Item("NUMERO_TELEFONO")
        '                Dim strListaPrecio As String = dsPedido.Tables(1).Rows(i).Item("UTILIZACION")
        '                Dim strCampana As String = dsPedido.Tables(1).Rows(i).Item("CAMPANA")
        '                Dim strResultado As String
        '                Dim strTransac As String
        '                If strNumero <> String.Empty Or strNumero.ToString <> "" Or strEquipo <> String.Empty Then
        '                    If numerosValidados.IndexOf(CDbl(strNumero).ToString()) = -1 Then
        '                        If strListMaterialBBA.IndexOf(strEquipo) <> -1 Then
        '                            Try
        '                                Dim CodOpcion As String = Get_CodOpcionBBA(strEquipo, strListMaterialBBA, strListaPrecio, strCampana, strDocSap)
        '                                If CodOpcion <> "" Then
        '                                    objActivacion.FK_EjecutarDesactivacion(CDbl(strNumero).ToString(), strResultado, strTransac)
        '                                End If
        '                            Catch ex As Exception
        '                                strResultado = 0
        '                            End Try
        '                            numerosValidados += CDbl(strNumero).ToString() & "|"
        '                            If strResultado <> "1" Then
        '                                resultadoLineasDesactivadas += CDbl(strNumero).ToString() & "|"
        '                            End If
        '                        End If
        '                    End If
        '                End If
        '            Next
        '            If resultadoLineasDesactivadas.Trim <> "" Then
        '                Dim arrLineas() As String = resultadoLineasDesactivadas.Split("|")
        '                Dim k As Integer
        '                Dim mensaje As String = ConfigurationSettings.AppSettings("msjeErrorDesactivarLinea")
        '                For k = 0 To arrLineas.Length - 1
        '                    mensaje += "\n" & arrLineas(k)
        '                Next
        '                Response.Write("<script language=jscript> alert('" & mensaje & "'); </script>")
        '            End If
        '        End If
        '        'Fin de Desactivación de los PREPAGOS
        '        'ALINEACION DEL PQT-169512-TSK-36121 ---- INICIO 

        '        'roaming
        '        Try
        '            'Dim strDocSap As String = drFila("VBELN")
        '            'Dim dsPedido As New DataSet
        '            'dsPedido = (New SapGeneralNegocios).Get_ConsultaPedido("", Session("Almacen"), sDocumentoSap, "")
        '            Dim objcajas As New COM_SIC_Cajas.clsCajas
        '            Dim dtDet As DataTable = CType(dsPedido.Tables(1), DataTable)
        '            Dim nroTelefono = Right(dtDet.Rows(0)("NUMERO_TELEFONO"), 9)
        '            Dim Flag As String = ""
        '            Dim retorno As Int64 = 0
        '            Dim mensaje As String = ""

        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Inicio Actualizacion Servicio Roaming")
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Telefono: " & nroTelefono)
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Flag: " & Flag)
        '            objCajas.ActualizarServRoaming(drFila("VBELN"), nroTelefono, Flag, retorno, mensaje)
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "retorno: " & retorno)
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "mensaje: " & mensaje)
        '        Catch ex As Exception
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Error Actualizacion Servicio Roaming")
        '        Finally
        '            objFileLog.Log_WriteLog(pathFile, strArchivo, " -" & drFila("VBELN") & "- " & "Fin Actualizacion Servicio Roaming")
        '        End Try

        '        'ALINEACION DEL PQT-169512-TSK-36121 --- FINAL 
        '        'Desactiva Status DOL
        '        Dim obAnul As New clsAnulaciones
        '        If obAnul.ValidacionVentaDOL(drFila("VBELN"), drFila("PEDIDO"), Session("ALMACEN")) Then
        '            Dim strDocSap As String = drFila("VBELN")
        '            Dim k As Integer
        '            Dim mensaje As String = ConfigurationSettings.AppSettings("msjeErrorDesactivarLineaDOL")

        '            For k = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                Dim strEquipo As String = dsPedido.Tables(1).Rows(k).Item("ARTICULO")
        '                Dim strNumero As String = dsPedido.Tables(1).Rows(k).Item("NUMERO_TELEFONO")
        '                Dim blnOK As Boolean
        '                If strEquipo.Substring(0, 2) = "PS" Then
        '                    blnOK = obAnul.Desactiva_Contact_Status_DOL(FormatoTelefono(strNumero), strDocSap)
        '                    If Not blnOK Then
        '                        mensaje += "\n" & FormatoTelefono(strNumero)
        '                        Session.Add("mensajeErrorLineasDesactivadasDOL", mensaje)
        '                    End If
        '                End If
        '            Next
        '        End If
        '    End If


        '    'CARIAS: Fin de bloque
        '    intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, ConfigurationSettings.AppSettings("codAplicacion"), strCodUsuario, Session("NOMBRE_COMPLETO"), "", "", _
        '      drFila("NAME1"), "", drFila("XBLNR"), drFila("VBELN"), 0, 3, 0, 0, 0, 0, 0, 0, "", strNomVendedor)
        '    If intAutoriza = 1 Then
        '        ' pregunta por autorizacion para anular venta
        '        intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, ConfigurationSettings.AppSettings("codAplicacion"), strCodUsuario, Session("NOMBRE_COMPLETO"), "", "", _
        '                      drFila("NAME1"), "", drFila("XBLNR"), drFila("VBELN"), 0, 4, 0, 0, 0, 0, 0, 0, "", strNomVendedor)

        '        If intAutoriza = 1 Then
        '            If Len(txtRbPagos.Value.Trim) > 0 Then
        '                dvPagos.RowFilter = "VBELN='" & txtRbPagos.Value & "'"
        '                drFila = dvPagos.Item(0).Row
        '                Dim obAnular As New clsAnulaciones

        '                If CStr(drFila("FKART")) = cteTIPODOC_DEPOSITOGARANTIA AndAlso CStr(drFila("NRO_DEP_GARANTIA")).Trim().Length > 0 AndAlso CLng(CStr(drFila("NRO_DEP_GARANTIA"))) > 0 Then
        '                    obAnular.AnularDepGaran(CStr(drFila("NRO_DEP_GARANTIA")), CStr(drFila("NRO_REF_DEP_GAR")), strCodUsuario)

        '                    'Anular SOT DTH
        '                    Dim strDocSapDTH As String = CStr(drFila("NRO_DEP_GARANTIA"))
        '                    Dim strIdentifyLog As String = strDocSapDTH
        '                    Try

        '                        Dim objCajas As New COM_SIC_Cajas.clsCajas
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio Anular SOT DTH")
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "inp strDocSapDTH:" & strDocSapDTH)
        '                        Dim nroContratoDTH, codRespuestaDTH As Integer
        '                        Dim nroSec, nroDocSapDTH, nroDepSapDTH, mensajeDTH As String
        '                        Dim estadoAnulado As String = ConfigurationSettings.AppSettings("constEstadoContrato_Anulado")
        '                        Dim estadoContrato As String
        '                        If objCajas.validaDocSAPxDTH(strDocSapDTH, nroContratoDTH, nroSec, nroDocSapDTH, nroDepSapDTH, codRespuestaDTH, mensajeDTH) Then
        '                            If codRespuestaDTH = 0 Then
        '                                estadoContrato = objCajas.Consulta_estado_contrato(CInt(nroContratoDTH))
        '                                If estadoContrato <> estadoAnulado Then
        '                                    Dim nroSot = objCajas.ConsultaNroSot(nroSec)
        '                                    If AnularSot(nroSot, nroSec, strDocSapDTH) = "0" Then
        '                                        objCajas.Actualizar_estado_contrato(CheckInt64(nroContratoDTH), estadoAnulado, nroSot)
        '                                    End If
        '                                End If
        '                            End If
        '                        End If

        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "estadoContrato:" & estadoContrato)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroContratoDTH:" & nroContratoDTH)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroDocSapDTH:" & nroDocSapDTH)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "nroDepSapDTH:" & nroDepSapDTH)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "out codRespuestaDTH:" & codRespuestaDTH)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "out mensajeDTH:" & mensajeDTH)
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin Anular SOT DTH")

        '                    Catch ex As Exception
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR Anular SOT DTH: " & ex.Message.ToString())
        '                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR Anular SOT DTH: " & ex.StackTrace.ToString())
        '                    End Try

        '                    If Not IsNothing(drFila2) Then
        '                        obAnular.AnularViasPago(CStr(drFila2("VBELN")), CStr(drFila2("FKART")), _
        '                                                CStr(drFila2("XBLNR")), CStr(drFila2("FKDAT")), _
        '                                                CStr(drFila2("PEDIDO")), CStr(drFila2("NRO_DEP_GARANTIA")), _
        '                                                CStr(drFila2("NRO_REF_DEP_GAR")), CStr(drFila2("NRO_CONTRATO")), _
        '                                                CStr(drFila2("NRO_OPE_INFOCORP")), CStr(drFila2("CODIGO_APROBACIO")), _
        '                                                strTipoTienda, strCodOficina, strCodImprTicket, strCodUsuario, CDec(drFila2("PAGOS")), 1, "")
        '                        ImprimeAnulado(CStr(drFila2("XBLNR")))
        '                    End If

        '                    ''Anulacion de pedido asociado
        '                    'For i = 0 To ds.Tables(0).Rows.Count - 1
        '                    '    If ds.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
        '                    '        drFila2 = ds.Tables(0).Rows(i)
        '                    '        Exit For
        '                    '    End If
        '                    'Next

        '                    'If Not IsNothing(drFila2) Then
        '                    '    obAnular.AnularViasPago(CStr(drFila2("VBELN")), CStr(drFila2("FKART")), _
        '                    '                        CStr(drFila2("XBLNR")), CStr(drFila2("FKDAT")), _
        '                    '                        CStr(drFila2("PEDIDO")), CStr(drFila2("NRO_DEP_GARANTIA")), _
        '                    '                        CStr(drFila2("NRO_REF_DEP_GAR")), CStr(drFila2("NRO_CONTRATO")), _
        '                    '                        CStr(drFila2("NRO_OPE_INFOCORP")), CStr(drFila2("CODIGO_APROBACIO")), _
        '                    '                        strTipoTienda, strCodOficina, strCodImprTicket, strCodUsuario, CDec(drFila2("PAGOS")), 1)
        '                    'Else
        '                    '    'Buscar en los documentos por pagar
        '                    '    Dim objPool As New SAP_SIC_Pagos.clsPagos
        '                    '    Dim ds2 As DataSet
        '                    '    ds2 = objPool.Get_ConsultaPoolFactura(strOficinaVta, txtFecha.Text, "I", "", "", "", "20", "1")
        '                    '    For i = 0 To ds2.Tables(0).Rows.Count - 1
        '                    '        If ds2.Tables(0).Rows(i).Item("PEDIDO") = drFila("PEDIDO") And ds2.Tables(0).Rows(i).Item("FKART") <> cteTIPODOC_DEPOSITOGARANTIA Then
        '                    '            drFila2 = ds2.Tables(0).Rows(i)
        '                    '            Exit For
        '                    '        End If
        '                    '    Next

        '                    '    If Not IsNothing(drFila2) Then
        '                    '        obAnular.AnularViasPago(CStr(drFila2("VBELN")), CStr(drFila2("FKART")), _
        '                    '                                                            CStr(drFila2("XBLNR")), CStr(drFila2("FKDAT")), _
        '                    '                                                            CStr(drFila2("PEDIDO")), CStr(drFila2("NRO_DEP_GARANTIA")), _
        '                    '                                                            CStr(drFila2("NRO_REF_DEP_GAR")), CStr(drFila2("NRO_CONTRATO")), _
        '                    '                                                            CStr(drFila2("NRO_OPE_INFOCORP")), CStr(drFila2("CODIGO_APROBACIO")), _
        '                    '                                                            strTipoTienda, strCodOficina, strCodImprTicket, strCodUsuario, CDec(drFila2("PAGOS")))

        '                    '    End If
        '                    'End If

        '                Else
        '                    obAnular.AnularViasPago(CStr(drFila("VBELN")), CStr(drFila("FKART")), _
        '                                            CStr(drFila("XBLNR")), CStr(drFila("FKDAT")), _
        '                                            CStr(drFila("PEDIDO")), CStr(drFila("NRO_DEP_GARANTIA")), _
        '                                            CStr(drFila("NRO_REF_DEP_GAR")), CStr(drFila("NRO_CONTRATO")), _
        '                                            CStr(drFila("NRO_OPE_INFOCORP")), CStr(drFila("CODIGO_APROBACIO")), _
        '                                            strTipoTienda, strCodOficina, strCodImprTicket, strCodUsuario, CDec(drFila("PAGOS")), 1, "")
        '                    ImprimeAnulado(CStr(drFila("XBLNR")))
        '                End If

        '                If Not IsNothing(dsPedido) Then
        '                    'Registra Log anulación documento SAP Bono Anual TFI
        '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
        '                        Dim x As Integer
        '                        Dim blnMostrarMensaje As Boolean = False
        '                        For x = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                            Dim strCodArt As String = CStr(dsPedido.Tables(1).Rows(x).Item("ARTICULO"))
        '                            Dim strNumero As String = dsPedido.Tables(1).Rows(x).Item("NUMERO_TELEFONO")

        '                            If strCodArt = ConfigurationSettings.AppSettings("strCodArticuloBonoAnualTFI") Then
        '                                Dim objFileLog As New SICAR_Log
        '                                Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
        '                                Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        '                                Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        '                                Dim strIdentifyLog As String = CStr(drFila("VBELN"))

        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Anula Documento SAP Bono Anual TFI-----------")
        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     NroDocSAP : " & CStr(drFila("VBELN")))
        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     strNumero : " & strNumero)
        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OficinaVenta : " & Session("ALMACEN"))
        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Usuario : " & Session("strUsuario"))
        '                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Anula Documento SAP Bono Anual TFI-----------")

        '                                objFileLog = Nothing
        '                                blnMostrarMensaje = True
        '                            End If
        '                        Next
        '                        If blnMostrarMensaje Then
        '                            Response.Write("<script language=jscript> alert('Se realizó la anulación de la compra, por favor comunicar a ATU para anular las recargas programadas.'); </script>")
        '                        End If
        '                    End If

        '                    ' Anular Venta Portabilidad'
        '                    Anula_Portabilidad(CStr(drFila("PEDIDO")))

        '                    ' Roaming
        '                    ActualizarPlanRoaming(CStr(drFila("VBELN")), ConfigurationSettings.AppSettings("CodigoAnulacionRoaming"))

        '                    ' Anulacion Renovacion RPM6
        '                    Dim strTelefono As String
        '                    Dim strCodArticulo As String

        '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") Then
        '                        For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                            strCodArticulo = CStr(dsPedido.Tables(1).Rows(idx).Item("ARTICULO"))
        '                            strTelefono = CStr(dsPedido.Tables(1).Rows(idx).Item("NUMERO_TELEFONO"))

        '                            If strCodArticulo = ConfigurationSettings.AppSettings("strCodArticuloRenovacion") Then
        '                                AnularRenovacionRPM6(strTelefono)
        '                            End If
        '                        Next
        '                    End If
        '                    ' Anulacion Renovacion RPM6



        '                    ' Anulacion Bono Renovacion Prepago

        '                    Dim strEstadoRegistro As String = ConfigurationSettings.AppSettings("constEstadoPagadoReposicion")
        '                    Dim intFlagBloqueo As Integer
        '                    Dim strCodBloqueo As String
        '                    Dim strTipoBloqueo As String
        '                    Dim strIccidActual As String
        '                    Dim strIccidNuevo As String
        '                    Dim strUsuario As String
        '                    Dim strMensaje As String ' Resultado general
        '                    Dim strTipoVenta As String
        '                    Dim sDocumentoSap As String = Request.Item("rbPagos")
        '                    Dim obj As New COM_SIC_Cajas.clsCajas

        '                    Dim intExistePedidoSisact As Integer = obj.Consulta_Existe_Pedido_Reposicion(sDocumentoSap, strEstadoRegistro, intFlagBloqueo, strCodBloqueo, strTipoBloqueo, strIccidActual, strIccidNuevo, strUsuario, strTipoVenta)

        '                    If intExistePedidoSisact = 1 Then
        '                        Dim strIMEI, strTelfRenov As String
        '                        For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                            strIMEI = Right(dsPedido.Tables(1).Rows(idx).Item("SERIE"), 15)
        '                            strTelfRenov = Right(dsPedido.Tables(1).Rows(idx).Item("NUMERO_TELEFONO"), 9)
        '                        Next


        '                        AnularBonoPrepagoReposicion(strTelfRenov, strIMEI, sDocumentoSap)

        '                        'INICIO FMES
        '                        AnularReposicionPrepagoSisact(sDocumentoSap)
        '                        'FIN FMES


        '                    End If

        '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Anulacion Bono Renovacion Prepago" & dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") & " - " & dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA"))
        '                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Anulacion Bono Renovacion Prepago" & ConfigurationSettings.AppSettings("strTVPrepago") & " - " & ConfigurationSettings.AppSettings("strDTVRenovacion"))

        '                    ' Anulacion Bono Reposicion Prepago


        '                    ' Anulacion Bono Renovacion Prepago
        '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") And _
        '                        dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA") = ConfigurationSettings.AppSettings("strDTVRenovacion") Then

        '                        Dim strIMEI, strTelfRenov As String
        '                        For idx As Integer = 0 To dsPedido.Tables(1).Rows.Count - 1
        '                            strIMEI = Right(dsPedido.Tables(1).Rows(idx).Item("SERIE"), 15)
        '                            strTelfRenov = Right(dsPedido.Tables(1).Rows(idx).Item("NUMERO_TELEFONO"), 9)
        '                        Next

        '                        AnularBonoPrepago(strTelfRenov, strIMEI, CheckStr(drFila("PEDIDO")))
        '                    End If
        '                    ' Anulacion Bono Renovacion Prepago

        '                    'Renovacion Corporativa
        '                    If dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPostpago") Then

        '                        Dim p_nro_pedido As String = Funciones.CheckStr(drFila("PEDIDO"))

        '                        'Anulacion Renovacion B2E
        '                        If dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA") = ConfigurationSettings.AppSettings("constCodRenovB2E") Then
        '                            AnularVentaRenovCorp(p_nro_pedido, "B2E")
        '                            AnularRenovCorpBSCS(p_nro_pedido, "B2E")
        '                        End If

        '                        'Anulacion Renovacion Business
        '                        If dsPedido.Tables(0).Rows(0).Item("CLASE_VENTA") = ConfigurationSettings.AppSettings("constCodRenovBus") Then
        '                            If dsPedido.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE") = ConfigurationSettings.AppSettings("constDocClienteBus") Then
        '                                AnularVentaRenovCorp(p_nro_pedido, "BUS")
        '                                AnularRenovCorpBSCS(p_nro_pedido, "B2E")
        '                            End If
        '                        End If
        '                    End If
        '                    'Renovacion Corporativa
        '                End If

        '                'Anula Promo Modem
        '                Anula_Vta_Prom(drFila("VBELN"), ConfigurationSettings.AppSettings("ConstVentaEstado_Anulado"))

        '                Response.Write("<script language=jscript> alert('Proceso de anulación de pedido lanzado, Verifique posteriormente'); </script>")
        '                CargarGrilla(strCodOficina)
        '            End If
        '        Else
        '            Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")

        '        End If

        '        '--- ACTIVACION REGISTRO SISACT PREPAGO --- INICIO
        '        Dim strDocSap As String
        '        strDocSap = drFila("VBELN")
        '        Dim dtDatos As New DataTable
        '        Dim objBus As New COM_SIC_Cajas.clsCajas
        '        dtDatos = objBus.ListarDatosCabeceraVenta(strDocSap)
        '        If dtDatos.Rows.Count > 0 Then
        '            Dim strAlmacen As String = dtDatos.Rows(0).Item("VEPR_COD_PDV").ToString()
        '            Dim strCanal As String = dtDatos.Rows(0).Item("VEPR_COD_CAN").ToString()
        '            Dim constDocAnulado As String = ConfigurationSettings.AppSettings("constDocAnulado")
        '            Dim resultado As Integer = 0
        '            Dim updVentaPrepago As Boolean = objBus.ActualizarPagoCorner(strDocSap, constDocAnulado, strAlmacen, resultado)
        '        End If
        '        '--- ACTIVACION REGISTRO SISACT PREPAGO --- FIN


        '        ' Inicio E77568
        '        ' Inicio PS - Automatización de canje y nota de crédito
        '        ' Liberar los puntos Claro Club si es la anulación de una nota de crédito que tiene 
        '        ' Claro puntos reservados
        '        Dim ID_CCLUB As String ' Tipo doc. identidad en PuntosCC
        '        Dim NroDocumento As String
        '        If ExisteCanjePuntosClaroClub(strDocSap, ID_CCLUB, NroDocumento) Then
        '            LiberarPuntosCC(strDocSap, ID_CCLUB, NroDocumento)
        '        End If
        '        ' Fin PS - Automatización de canje y nota de crédito
        '        ' Fin E77568

        '    Else
        '        Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
        '    End If

        'Catch ex As Exception

        '    Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        '    Response.Write(ex.StackTrace)
        'End Try
    End Sub

    'AGREGADO POR FFS (Handles btnReasignar.Click)
    Private Sub btnReasignar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReasignar.Click
        'CAMBIADO POR FFS INICIO
        'Dim dvPagos As New DataView(ds.Tables(0))
        Dim dvPagos As New DataView(dtsap)
        'CAMBIADO POR FFS FIN

        If Len(Trim(Request.Item("rbPagos"))) > 0 Then


            Dim strEstadoRegistro As String = ConfigurationSettings.AppSettings("constEstadoPagadoReposicion")
            Dim intFlagBloqueo As Integer
            Dim strCodBloqueo As String
            Dim strTipoBloqueo As String
            Dim strIccidActual As String
            Dim strIccidNuevo As String
            Dim strUsuario As String
            Dim strMensaje As String ' Resultado general
            Dim strTipoVenta As String
            Dim sDocumentoSap As String = Request.Item("rbPagos")
            Dim obj As New COM_SIC_Cajas.clsCajas

            Dim intExistePedidoSisact As Integer = obj.Consulta_Existe_Pedido_Reposicion(sDocumentoSap, strEstadoRegistro, intFlagBloqueo, strCodBloqueo, strTipoBloqueo, strIccidActual, strIccidNuevo, strUsuario, strTipoVenta)

            'Si el Pedido existe, registramos el estado ANULADO-PAGADO
            If intExistePedidoSisact = 1 Then
                Dim sMensaje As String = ""
                sMensaje = "No se permite reasignar en Reposiciones Pack Prepago"
                Response.Write("<script>alert('" & sMensaje & "');</script>")
                Exit Sub
            End If


            dvPagos.RowFilter = "VBELN='" & Request.Item("rbPagos") & "'"
            drFila = dvPagos.Item(0).Row
            Session("DocSel") = drFila
            Session("Consulta") = "1"
            Session("Pool") = ""
            Session("ReAsigna") = "S"
            Response.Redirect("asigSunatImp.aspx")
        End If


    End Sub

    Private Sub ImprimeAnulado(ByVal strRefer As String)

        Dim strScript As String
        Dim dsResult As DataSet
        Dim objPagos As New SAP_SIC_Pagos.clsPagos

        dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
        'si la impresion no es por SAP, abrir cuadro de impresoras
        If Trim(dsResult.Tables(0).Rows(0).Item("IMPRESION_SAP")) = "" Then
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
    End Sub

    Private Sub Anula_Portabilidad(ByVal strNroPedido As String)

        Dim i, intResultado As Integer
        Dim resultado As Integer = 0
        Dim oListaTelefono As DataSet
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim strTipoVenta As Integer = 0

        Dim blnOK As Boolean
        Dim strMensaje As String
        Dim objVentas As New SAP_SIC_Ventas.clsVentas

        Try
            If Not strNroPedido = "" Then
                oListaTelefono = objCajas.FP_Get_TelefonosPorta(strNroPedido, strTipoVenta, intResultado)

                If intResultado = 0 Then
                    For i = 0 To oListaTelefono.Tables(0).Rows.Count - 1
                        Dim msisdn As String = ""
                        Dim NroSEC As String = ""
                        NroSEC = oListaTelefono.Tables(0).Rows(i).Item("SOLIN_CODIGO")
                        msisdn = oListaTelefono.Tables(0).Rows(i).Item("PORT_NUMERO")

                        blnOK = objVentas.Set_AnulaTelefonoPortable(msisdn, strMensaje)
                        resultado = objCajas.RollbackDetalleVenta(msisdn, CType(NroSEC, Int64))
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Write("<script>alert('" & "Error. Anulacion de Venta de Portabilidad. " & ex.Message & "')</script>")
        End Try
    End Sub

    Private Sub ActualizaPago(ByVal strNroPedido As String)

        Dim intResultado As Integer
        Dim resultado As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        If Not strNroPedido.Trim = "" Then
            resultado = objCajas.ActualizaPago(strNroPedido, 2, intResultado)
        End If

    End Sub

    Public Sub AnularRenovacionRPM6(ByVal nroTelefono As String)

        Dim objBSCS As New COM_SIC_Cajas.clsCajas
        Dim Accion, CodOcc, NroCuotas As Integer
        Dim Monto As Double
        Dim CodRenov, Remark, DescTickler, MensajeFinal, TipoTickler As String
        MensajeFinal = String.Empty

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionRPC6" 'ConfigurationSettings.AppSettings("constNameLogRegistroDOL")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = nroTelefono
        Dim strOficina As String = Funciones.CheckStr(Session("OFICINA"))

        Try
            Dim ArrayParametros() As String = Split(ConfigurationSettings.AppSettings("strAnular"), ";")
            CodRenov = ArrayParametros(0).ToString()
            Accion = Integer.Parse(ArrayParametros(1))
            CodOcc = Integer.Parse(ArrayParametros(2))
            NroCuotas = Integer.Parse(ArrayParametros(3))
            Monto = Funciones.CheckDbl(ArrayParametros(4))
            Remark = String.Empty
            DescTickler = ConfigurationSettings.AppSettings("strDesTicklerRPC6") 'String.Empty
            TipoTickler = ArrayParametros(7).ToString()

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio AnularRenovacionRPM6-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  nroTelefono : " & FormatoTelefono(nroTelefono))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CodRenov : " & CodRenov)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Accion : " & Accion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  DescTickler : " & DescTickler)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodUsrRenovRPC : " & ConfigurationSettings.AppSettings("strCodUsrRenovRPC"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Cod. Oficina : " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  Oficina : " & strOficina)

            Dim Respuesta As String = objBSCS.FP_RegistrarRenovacionRPM6(FormatoTelefono(nroTelefono), CodRenov, Accion, DescTickler, strOficina, ConfigurationSettings.AppSettings("strCodUsrRenovRPC"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Respuesta : " & Respuesta)

            Select Case Split(Respuesta, ";")(0).ToString()
                Case "0"
                    MensajeFinal = "La anulación se ha realizado con éxito"
                Case Else
                    MensajeFinal = "Ha ocurrido un error en la anulación. Por favor comuníquese con el Administrador."
            End Select

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  MensajeFinal : " & MensajeFinal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin AnularRenovacionRPM6--------------")

            objBSCS.FP_RegistrarLogRenovacionRPM6(FormatoTelefono(nroTelefono), CodRenov, Accion, CodOcc, NroCuotas, Monto, Remark, TipoTickler, DescTickler, ConfigurationSettings.AppSettings("strCodUsrRenovRPC"), Session("strUsuario"), Split(Respuesta, ";")(1).ToString(), Split(Respuesta, ";")(2).ToString(), strOficina)
            If Len(MensajeFinal) > 0 Then
                Response.Write("<script>alert('" & MensajeFinal & "');</script>")
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovacionRPM6)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126  
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin AnularRenovacionRPM6--------------")

            MensajeFinal = "Ha ocurrido un error en la anulación. Por favor comuníquese con el Administrador."
            Response.Write("<script>alert('" & MensajeFinal & "');</script>")
        End Try

    End Sub
    Function FormatoTelefono(ByVal telefono)
        Dim aux
        aux = telefono
        If aux <> "" Then
            Dim longitud
            Dim posicion
            longitud = Len(telefono)
            If longitud > 0 Then
                'posicion = 1
                Do While InStr(1, aux, "0") = 1
                    aux = Mid(aux, 2, Len(aux))
                Loop
            End If
            If InStr(1, aux, "1") = 1 Then    'Si es lima adicionar 0 adelante
                aux = "0" & aux
            End If
        End If
        If aux = "" Then
            FormatoTelefono = telefono
        Else
            FormatoTelefono = aux
        End If
    End Function
    Private Function ListaMaterialesBBA(ByVal strDocSap As String) As String

        Dim strListMateriales As String = ""
        Dim strGrupo As String = ConfigurationSettings.AppSettings("constGrupoPrepagoEspecial")

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogActivacionPrepago")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strDocSap & "|" & strGrupo

        Try

            Dim dsMaterial As DataSet
            Dim objSicarDB As New COM_SIC_Activaciones.clsBDSiscajas
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ListaMaterialesBBA-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strGrupo : " & strGrupo)

            dsMaterial = objSicarDB.FP_ConsultaMaterialBBA(strGrupo)

            If Not IsNothing(dsMaterial) AndAlso dsMaterial.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsMaterial.Tables(0).Rows.Count - 1
                    strListMateriales += "|" & dsMaterial.Tables(0).Rows(idx).Item("SPARV_KEY") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE2") & ";" & _
                                                dsMaterial.Tables(0).Rows(idx).Item("SPARV_VALUE3")
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strListMateriales : " & strListMateriales)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ListaMaterialesBBA-----------")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ListaMaterialesBBA)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ListaMaterialesBBA-----------")
        Finally
            objFileLog = Nothing
        End Try

        Return strListMateriales
    End Function
    Private Function Get_CodOpcionBBA(ByVal strCodMaterial As String, ByVal strListMaterial As String, ByVal strListaPrecio As String, ByVal strCampana As String, ByVal strDocSap As String) As String

        Dim arrListMateriales As String()
        Dim arrMaterial As String()
        Dim strOpcion As String = ""

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogActivacionPrepago")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strDocSap & "|" & strCodMaterial

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio Get_CodOpcionBBA-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodMaterial : " & strCodMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListMaterial : " & strListMaterial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strListaPrecio : " & strListaPrecio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCampana : " & strCampana)

            arrListMateriales = strListMaterial.Split("|")

            If arrListMateriales.Length > 0 Then
                For idx As Integer = 0 To arrListMateriales.Length - 1
                    arrMaterial = arrListMateriales(idx).Split(";")
                    If arrMaterial.Length > 3 Then
                        If strCodMaterial.ToUpper() = arrMaterial(0).ToUpper() _
                        And strListaPrecio.ToUpper() = arrMaterial(2).ToUpper() _
                        And strCampana.ToUpper() = arrMaterial(3).ToUpper() Then

                            strOpcion = arrMaterial(1).ToString()
                            Exit For
                        End If
                    End If
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strOpcion : " & strOpcion)
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Get_CodOpcionBBA)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin Get_CodOpcionBBA-----------")
            objFileLog = Nothing
        End Try

        Return strOpcion

    End Function

#Region "Anulacion Renovacion Corporativa"

    Public Sub AnularVentaRenovCorp(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String)

        Dim oDatos As New COM_SIC_Cajas.clsCajas
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionCorporativa"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "---------------Inicio Anulacion Venta Renovacion-----------")
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

    Public Sub AnularRenovCorpBSCS(ByVal p_nro_pedido As String, ByVal p_tipo_renov As String)

        Dim oConsulta As New COM_SIC_Cajas.clsCajas
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionCorporativa"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

        Dim strOut As String
        Dim MsgError As String
        Dim LineasError As String = ""
        Dim MensajeFinal As String = ""

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------INICIO ANULACION PAGO RENOVACION--------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Pedido : " & p_nro_pedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Tipo Venta : " & p_tipo_renov)

            'Consultar las líneas para la Renovación
            Dim dsLineas As DataSet = oConsulta.FP_ConsultarDatosRenov(p_nro_pedido, p_tipo_renov)

            If Not IsNothing(dsLineas) AndAlso dsLineas.Tables(0).Rows.Count > 0 Then

                For x As Integer = 0 To dsLineas.Tables(0).Rows.Count - 1

                    Dim telefono As String = dsLineas.Tables(0).Rows(x)(0)

                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "-----------------------------------------------------------")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   INP  Nro Telefono     : " & telefono)

                    'Anular datos de la Renovacion en BSCS
                    'strOut = oConsulta.FP_AnularRenovCorpBSCS(telefono, MsgError)
                    strOut = oConsulta.FP_AnularRenovCorpBSCSNuevo(telefono, MsgError)

                    If Not strOut.Equals("0") Then
                        LineasError = LineasError & ";" & telefono
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Codigo : " & strOut)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorpBSCS)"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   OUT  Mensaje : " & CheckStr(MsgError) & MaptPath)
                    'FIN PROY-140126

                Next

                If Not LineasError = "" Then
                    LineasError = Right(LineasError, Len(LineasError) - 1)
                    MensajeFinal = "Ocurrió un Error en la anulación en BSCS de la(s) siguiente(s) línea(s): " & LineasError

                    Session.Add("AnulacionBSCSRenovacionCorporativa", MensajeFinal)
                End If
            Else
                'INI PROY-140126
                Dim MaptPath As String
                MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorpBSCS)"
                objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & "No se encontró Datos de Líneas." & MaptPath)
                'FIN PROY-140126

            End If

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularRenovCorpBSCS/1881)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126


        Finally
            oConsulta = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, p_nro_pedido & "- " & "----------------FIN ANULACION PAGO RENOVACION--------------")
        End Try
    End Sub

#End Region

    Private Function Valida_Anula_Prom(ByVal strNroSap As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.validaAnulaVtaProm(strNroSap)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
        Return intResult
    End Function

    Private Function Anula_Vta_Prom(ByVal strNroSap As String, ByVal strEstado As String) As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intResult As Integer

        Try
            intResult = objCajas.AnulaVtaProm(strNroSap, strEstado)
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try
        Return intResult
    End Function

    Public Sub AnularBonoPrepago(ByVal nroTelefono As String, ByVal nroIMEI As String, ByVal nroSap As String)

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionPrepago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim codSalida, msgSalida, strMensaje As String
        Dim strMensajeError As String

        Try
            nroTelefono = "51" & nroTelefono
            Dim input As String = String.Format("{0}|{1}", nroTelefono, nroIMEI)

            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Inicio Anulación Renovación Prepago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      nroLinea|imei: " & input)

            'Anulación Bono Prepago
            Dim oRenovaPrepago As New COM_SIC_Activaciones.clsBonoRenovaPrepago
            codSalida = oRenovaPrepago.AnularBonoPrepago(nroSap, nroTelefono, nroIMEI, msgSalida)

            'Consulta de Mensaje Renovación Prepago
            Dim codGrupo As Integer = CInt(ConfigurationSettings.AppSettings("ConstGrupoMensajeAnulacion"))
            Dim dsMensajes As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)
            If Not IsNothing(dsMensajes) Then
                For i As Integer = 0 To dsMensajes.Tables(0).Rows.Count - 1
                    Dim codigo As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(0)
                    Dim mensaje As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(1)

                    If codigo = codSalida Then
                        strMensaje = String.Format(mensaje, nroTelefono)
                        Exit For
                    End If

                    If codigo = "9" Then
                        strMensajeError = String.Format(mensaje, nroTelefono)
                    End If
                Next
            End If

        Catch ex As Exception
            strMensaje = strMensajeError
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularBonoPrepago)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      Error: " & ex.Message & MaptPath)
            'FIN PROY-140126           
        Finally
            Dim out As String = String.Format("{0}|{1}|{2}", codSalida, msgSalida, strMensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      codSalida|msgSalida|Mensaje: " & out)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Fin Anulación Renovación Prepago")

            'Session.Add("mensajeErrorBonoPrepago", strMensaje)
            Response.Write("<script>alert('" & strMensaje & "');</script>")
        End Try

    End Sub

    Public Sub AnularBonoPrepagoReposicion(ByVal nroTelefono As String, ByVal nroIMEI As String, ByVal nroSap As String)

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionPrepago"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRenovacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim codSalida, msgSalida, strMensaje As String
        Dim strMensajeError As String

        Try
            nroTelefono = "51" & nroTelefono
            Dim input As String = String.Format("{0}|{1}", nroTelefono, nroIMEI)

            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Inicio Anulación Bono Reposicion Prepago")
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "nroSap: " & nroSap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "nroTelefono: " & nroTelefono)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "nroIMEI: " & nroIMEI)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      nroLinea|imei: " & input)

            'Anulación Bono Prepago
            Dim oRenovaPrepago As New COM_SIC_Cajas.clsCajas
            codSalida = oRenovaPrepago.AnularBonoPrepago(nroSap, nroTelefono, nroIMEI, msgSalida)

            'Consulta de Mensaje Renovación Prepago
            Dim codGrupo As Integer = CInt(ConfigurationSettings.AppSettings("ConstGrupoMensajeAnulacion"))
            Dim dsMensajes As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)
            If Not IsNothing(dsMensajes) Then
                For i As Integer = 0 To dsMensajes.Tables(0).Rows.Count - 1
                    Dim codigo As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(0)
                    Dim mensaje As String = CheckStr(dsMensajes.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(1)

                    If codigo = codSalida Then
                        strMensaje = String.Format(mensaje, nroTelefono)
                        Exit For
                    End If

                    If codigo = "9" Then
                        strMensajeError = String.Format(mensaje, nroTelefono)
                    End If
                Next
            End If

        Catch ex As Exception
            strMensaje = strMensajeError
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularBonoPrepagoReposicion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      Error: " & ex.Message & MaptPath)
            'FIN PROY-140126

        Finally
            Dim out As String = String.Format("{0}|{1}|{2}", codSalida, msgSalida, strMensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "      codSalida|msgSalida|Mensaje: " & out)
            objFileLog.Log_WriteLog(pathFile, strArchivo, nroTelefono & "- " & "Fin Anulación Renovación Prepago")

            'Session.Add("mensajeErrorBonoPrepago", strMensaje)
            Response.Write("<script>alert('" & strMensaje & "');</script>")
        End Try

    End Sub

    Private Sub ActualizarPlanRoaming(ByVal NroPedido As String, ByVal CodEstado As String)
        Dim dsLineas As New DataSet
        Dim oConsulta As New COM_SIC_Cajas.clsCajas

        Dim oLog As New SICAR_Log
        Dim nameFile As String = "LogProcesoRoaming"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
        Dim strArchivo As String = oLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = NroPedido

        Try

            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Anulacion Roaming")
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------------------------")
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "NroPedido: " & NroPedido)

            'Consulta de Nro de Linea
            dsLineas = oConsulta.ObtenerLineasRoaming(NroPedido)

            If Not IsNothing(dsLineas) Then

                For i As Integer = 0 To dsLineas.Tables(0).Rows.Count - 1

                    Dim linea As String = dsLineas.Tables(0).Rows(i).Item("ROAMV_NRO_LINEA")
                    Dim plan As String = dsLineas.Tables(0).Rows(i).Item("ROAMV_COD_PLAN")

                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Linea: " & linea)
                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Plan: " & plan)
                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Input  --> " & "Estado: " & CodEstado)

                    Dim mensaje As String = oConsulta.ActualizarLineasRoaming(linea, plan, CodEstado)

                    oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output --> " & "Mensaje: " & mensaje)
                Next
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ActualizarPlanRoaming)"
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Output --> " & "Error: " & ex.Message & MaptPath)
            'FIN PROY-140126
        Finally
            oConsulta = Nothing
            dsLineas = Nothing
            oLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Anulacion Roaming")
        End Try

    End Sub
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

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio AnularSot")
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "WS    : " & ConfigurationSettings.AppSettings("constSGATransaccion_Url"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Method: " & "anularSot")
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nroSot: " & nroSot)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp nroSec: " & nroSec)

            If CheckStr(nroSot) = "" Or nroSot = "0" Then
                nroSot = "0"
                observacion = nroSec
                oSGAResponseTrs = oTransaccion.AnularSot(nroSot, observacion, oAudit)
            Else
                oSGAResponseTrs = oTransaccion.AnularSot(nroSot, observacion, oAudit)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codResp: " & oSGAResponseTrs.codRepuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out codMsg: " & oSGAResponseTrs.msgRepuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin AnularSot")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR AnularSot: " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR AnularSot: " & ex.StackTrace.ToString())
            Throw New Exception("No se pudo generar la SOT.")
        End Try
        Return oSGAResponseTrs.codRepuesta
    End Function

    ' Inicio E77568
    ' Inicio PS - Automatización de canje y nota de crédito

    ' Efectua el desbloqueo de puntos Claro Club con un llamado a webservices.
    Public Sub LiberarPuntosCC(ByVal strDocSap As String, _
                               ByVal ID_CCLUB As String, _
                               ByVal NroDocumento As String)
        Dim idLog As String = strDocSap

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio LiberarPuntosCC()")
            Dim tipoClie As String
            Dim objPuntosClaroClubNegocio As New clsPuntosClaroClub
            Dim txId As String
            Dim errorCode As String
            Dim errorMsg As String
            tipoClie = ConfigurationSettings.AppSettings("consTipoclie")

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio liberarPuntos()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp ID_CCLUB: " & ID_CCLUB)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp NroDocumento: " & NroDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "inp tipoClie: " & tipoClie)

            objPuntosClaroClubNegocio.liberarPuntos(ID_CCLUB, _
                                                    NroDocumento, _
                                                    tipoClie, _
                                                    txId, _
                                                    errorCode, _
                                                    errorMsg)

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out txId:" & txId)

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LiberarPuntosCC)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out errorCode:" & errorCode & MaptPath)
            'FIN PROY-140126
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "out errorMsg:" & errorMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin liberarPuntos()")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LiberarPuntosCC)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR LiberarPuntosCC(): " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin LiberarPuntosCC(): ")
        End Try
    End Sub

    ' Verificar si tiene ya los puntos reservados
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Inicio TieneReserva()")

            objCajas.ValidaBloqueoBolsa(k_tipo_doc, _
                                        k_num_doc, _
                                        k_tipo_clie, _
                                        k_tipo_doc2, _
                                        k_estado, _
                                        k_coderror, _
                                        k_descerror)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_tipo_doc2:" & k_tipo_doc2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_estado:" & k_estado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_coderror:" & k_coderror)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "k_descerror:" & k_descerror)

            If k_coderror = 0.0 Then
                If constVALBLOQUEOBOLSA = k_estado Then
                    bTieneReserva = True
                End If
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "bTieneReserva:" & bTieneReserva)

            Return bTieneReserva
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR TieneReserva(): " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR TieneReserva(): " & ex.StackTrace.ToString())

            Return False
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "Fin TieneReserva()")
            objCajas = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Verifica si existe pendiente (diferente de '1') puntos Claro Club por canjear.
    ''' Se hace la consulta por nro. doc. SAP de la boleta.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568.
    ''' PS - Automatización de canje y nota de crédito.
    ''' </remarks>
    Public Function ExisteCanjePuntosClaroClub(ByVal NroDocSap As String, _
                                               ByRef ID_CCLUB As String, _
                                               ByRef NroDocumento As String) As Boolean
        Dim bExiste As Boolean = False
        Dim strIdentifyLog As String = NroDocSap
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ExisteCanjePuntosClaroClub()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dt As DataTable
            Dim CONS_FLAG_CANJE As String = ConfigurationSettings.AppSettings("CONS_FLAG_CANJE")
            dt = objCajas.ListarXDocSAP(NroDocSap)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If dr.Item("FLAG_CANJE").ToString <> CONS_FLAG_CANJE Then
                    bExiste = True
                End If
                ID_CCLUB = Funciones.CheckStr(dr.Item("ID_CCLUB"))
                NroDocumento = Funciones.CheckStr(dr.Item("NUM_DOC"))
            End If

            objCajas = Nothing
            Return bExiste
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ExisteCanjePuntosClaroClub)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT ERROR : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            Return bExiste
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ExisteCanjePuntosClaroClub()")
        End Try
    End Function


    Private Function AnularReposicionPrepagoSisact(ByVal strDocumentoSap As String) As Boolean

        Dim obj As New COM_SIC_Cajas.clsCajas
        Dim intResult As Boolean
        intResult = False
        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, strDocumentoSap & " - INICIO AnularReposicionPrepagoSisact")

            Dim estadoAnulado As String = ConfigurationSettings.AppSettings("constEstadoAnuladoPagadoReposicion")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strDocumentoSap & " - PARAMETROS AnularReposicionPrepagoSisact : " & strDocumentoSap & "," & estadoAnulado)

            intResult = obj.AnularReposicionPrepagoSisact(strDocumentoSap, estadoAnulado)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strDocumentoSap & " - RESULTADO AnularReposicionPrepagoSisact : " & intResult)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR AnularReposicionPrepagoSisact: " & ex.Message.ToString())
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try

        Return intResult

    End Function

    ' Fin PS - Automatización de canje y nota de crédito
    ' Fin E77568
    'PROY-27440 INI
    Private Sub cmdFormaPago_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormaPago.Click
        Dim bol As Boolean = True
        Dim strCodOficina As String = Session("ALMACEN")
        strOficinaVta = ConsultaPuntoVenta(strOficinaVta)

        Dim dvPagos As New DataView(dtsap)
        Dim filter As String = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
        dvPagos.RowFilter = filter
        drFila = dvPagos.Item(0).Row
        Session("drFilaDoc") = drFila
        Dim strNroTelf As String = ""

        Dim isDbnull As Boolean = Convert.IsDBNull(drFila.Item(31))
        If isDbnull = False Then
            strNroTelf = drFila.Item(31)
        End If

        Dim strfecha As String = Right(txtFecha.Text, 4) + txtFecha.Text.Substring(3, 2) + Left(txtFecha.Text, 2)
        Dim bolFormaPago As Boolean = True

        Try
            Response.Redirect("detaConsultaPagos.aspx?pc_OfiVta=" & strOficinaVta & "&pc_fecha=" & strfecha & "&numeroTelefono=" & strNroTelf & "&FormaPago=" & bolFormaPago)
            CargarGrilla(ConsultaPuntoVenta(strCodOficina))
        Catch ex As Exception
            Dim rpta As String = "Error en Formas Pago" & ex.Message
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub
    'PROY-27440 FIN
End Class
