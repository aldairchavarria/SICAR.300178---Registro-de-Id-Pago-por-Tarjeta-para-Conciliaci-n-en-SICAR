#Region "Imports"
Imports COM_SIC_Adm_Cajas
Imports COM_SIC_OffLine
#End Region

Public Class visorVentaFact
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cboTipDoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDocSunat01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocSunat02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoFact01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoFact02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodVendedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVendedor As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboCuotas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadVendedorHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodVendedor As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lbCodSubOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbSubOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTodosSubOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents loadDataSubOfi As System.Web.UI.WebControls.Button
    Protected WithEvents hidSubOficina As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorVentaDet")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorVentaDet")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Dim objClsAdmCaja As clsAdmCajas
    Dim objClsOffline As clsOffline
    Private cteCajero As String = ConfigurationSettings.AppSettings("constVisVntFac_Cajero")
    Private cteVendedor As String = ConfigurationSettings.AppSettings("constVisVntFac_Vendedor")
    Private Const cteEstadoVentaCuadre As String = "ESTADO_VENTA_CUADRE"
    Private Const cteTipoDocumentos As String = "TIPO_DOC_VENTA"
#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
            If Not IsPostBack Then
                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                CargarParametros()
                txtMontoFact01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMontoFact02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                'txtCodOficina.Attributes.Add("onkeydown", "f_buscarOficina();")
                'txtCodOficina.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCodCajero.Attributes.Add("onkeydown", "f_buscarCajero();")
                txtCodCajero.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCodVendedor.Attributes.Add("onkeydown", "f_buscarVendedor();")
                txtCodVendedor.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                cmdBuscar.Attributes.Add("onClick", "f_OcultarBotonera();")
            End If

            'Proy-33111- Inicio '
            Dim strCodSubOficina As String = Session("COD_SUBOFICINA")
            Dim strSubOficina As String = Session("SUBOFICINA")
            Dim strCodOficina As String = Session("ALMACEN")
            Dim strOficina As String = Session("OFICINA")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- Inicio - Validando Sub Oficina DAC - RecTrsPago ---")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- strCodSubOficina :" & strCodSubOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- strSubOficina :" & strSubOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- strCodOficina :" & strCodOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- strOficina :" & strOficina)
            If strCodSubOficina.Length > 0 Then
                lbSubOficina.Items.Clear()
                lbCodSubOficina.Items.Clear()
                lbOficina.Items.Clear()
                lbOficina.Items.Add(New ListItem(Trim(strCodOficina) & " - " & Trim(strOficina)))
                lbCodSubOficina.Items.Add(New ListItem(Trim(strCodSubOficina)))
                lbSubOficina.Items.Add(New ListItem(Trim(strSubOficina)))
                hidCodOficina.Value = strCodOficina
                hidSubOficina.Value = "'" & Trim(strCodSubOficina) & "',"
                chkTodosSubOf.Checked = False
                Me.RegisterStartupScript("Desactivar Controle", "<script language=javascript>f_DesactivarControles();</script>")
            Else
                chkTodosSubOf.Checked = True
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- Fin - Validando Sub Oficina DAC - RecTrsPago ---")
            'Proy-33111- Fin '
        End If
    End Sub

    Private Sub loadCajeroHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadCajeroHandler.Click
        CargarCajeros(hidCodOficina.Value)
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            If Not Session("dgListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficina"), DataTable)
                If Not Session("ConCajTot_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ConCajTot_codOficina"))
                    Dim dv As New DataView
                    Dim arrCodigos() As String = Split(codigoOfi, ",")
                    dv.Table = dt
                    lbOficina.Items.Clear()
                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "CODIGO = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                Dim oficina As String = String.Format("{0} - {1}", Trim(drvResultado("CODIGO")), Trim(drvResultado("DESCRIPCION")))
                                lbOficina.Items.Add(New ListItem(oficina))
                            End With
                        End If
                    Next
                    hidCodOficina.Value = CStr(Session("ConCajTot_codOficina"))
                    chkTodosOf.Checked = False
                End If
            End If

            If Not Session("dgVListaCajero") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgVListaCajero"), DataTable)
                If Not Session("visor_codCajero") Is Nothing Then
                    Dim codigoCaj As String = CStr(Session("visor_codCajero"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoCaj & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCodCajero.Text = Trim(drvResultado("CODIGO"))
                            txtCajero.Text = Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodCajero.Value = Session("visor_codCajero")
                    End If
                End If
            End If

            If Not Session("dgVListaVendedor") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgVListaVendedor"), DataTable)
                If Not Session("visor_codVendedor") Is Nothing Then
                    Dim codigoVen As String = CStr(Session("visor_codVendedor"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoVen & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCodVendedor.Text = Trim(drvResultado("CODIGO"))
                            txtVendedor.Text = Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodVendedor.Value = Session("visor_codVendedor")
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub loadOficinaHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadOficinaHandler.Click
        CargarOficinaVta()
    End Sub

    Private Sub loadVendedorHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadVendedorHandler.Click
        CargarVendedores(hidCodOficina.Value)
    End Sub

    Private Sub loadDataSubOfi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles loadDataSubOfi.Click
        Try
            Dim identificadorLog = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "

            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Inicio IND - loadDataSubOfi_Click ---")

            If Not Session("dgListaSubOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaSubOficina"), DataTable)
                If Not Session("ConCajTot_codSubOficina") Is Nothing Then
                    Dim codigoSubOfi As String = CStr(Session("ConCajTot_codSubOficina"))
                    Dim dv As New DataView
                    Dim strCodigos As String = ""
                    Dim strSubOficinasDesc As String = ""
                    Dim strSubOficinas As String = ""
                    Dim arrCodigos() As String = Split(codigoSubOfi, ",")
                    dv.Table = dt

                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "dt Count : " & dt.Rows.Count)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "codigoSubOfi : " & codigoSubOfi)

                    lbSubOficina.Items.Clear()
                    lbCodSubOficina.Items.Clear()

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "SUOFV_ID = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodSubOficina.Items.Add(New ListItem(Trim(drvResultado("SUOFV_ID"))))
                                strCodigos = strCodigos + Trim(drvResultado("SUOFV_ID")) + ";"
                                lbSubOficina.Items.Add(New ListItem(Trim(drvResultado("SUOFV_SUB_OFICINA"))))
                                strSubOficinasDesc = strSubOficinasDesc + Trim(drvResultado("SUOFV_SUB_OFICINA")) + ";"
                                strSubOficinas = strSubOficinas + "'" + Trim(drvResultado("SUOFV_ID")) + "',"
                            End With
                        End If
                    Next
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strSubOficinasDesc : " & strSubOficinasDesc)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & " strSubOficinas : " & strSubOficinas)
                    hidSubOficina.Value = CStr(strSubOficinas)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "hidSubOficina : " & hidSubOficina.Value)
                    chkTodosSubOf.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error loadDataSubOfi_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin loadDataHandler_Click")
        End Try
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            strIdentifyLog = Session("ALMACEN") & "|" & strUsuario

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- INI  cmdBuscar_Click")
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strSubOficinaVta As String = hidSubOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strTipoDoc As String = cboTipDoc.SelectedValue
            Dim strDocSunat1 As String = txtDocSunat01.Text
            Dim strDocSunat2 As String = txtDocSunat02.Text
            Dim strMtoPagado1 As String = txtMontoFact01.Text
            Dim strMtoPagado2 As String = txtMontoFact02.Text
            Dim strCajero As String = hidCodCajero.Value
            Dim strVendedor As String = hidCodVendedor.Value
            Dim strViaPago As String = cboViaPago.SelectedValue
            Dim strCuotas As String = cboCuotas.SelectedValue
            Dim strEstado As String = cboEstado.SelectedValue
            Dim strCntRegistros As String = txtCntRegistros.Text
            Dim strMsjErr As String = String.Empty
            Dim strUrl As String = "visorVentaFact_detalle.aspx"

            If strOficinaVta.Equals(String.Empty) And Not chkTodosOf.Checked Then
                strMsjErr = "Seleccione oficina"
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strSubOficinaVta: " & strSubOficinaVta)

            If strSubOficinaVta.Length > 0 Then
                strSubOficinaVta = strSubOficinaVta.Substring(0, strSubOficinaVta.Length - 1)
            End If

            If strCajero.Length = 0 Then
                strCajero = txtCodCajero.Text
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOficinaVta: " & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strSubOficinaVta: " & strSubOficinaVta)

            If chkTodosOf.Checked Then
                strOficinaVta = "0"
            End If

            strUrl = strUrl & "?of=" & strOficinaVta & "&subof=" & strSubOficinaVta _
                    & "&fi=" & strFechaIni & "&ff=" & strFechaFin _
                    & "&td=" & strTipoDoc & "&mp1=" & strMtoPagado1 & "&mp2=" & strMtoPagado2 _
                    & "&ds1=" & strDocSunat1 & "&ds2=" & strDocSunat2 _
                    & "&cj=" & strCajero & "&vd=" & strVendedor & "&vp=" & strViaPago _
                    & "&cuo=" & strCuotas & "&est=" & strEstado & "&creg=" & strCntRegistros
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strUrl: " & strUrl)
            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  cmdBuscar_Click")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarParametros()
        CargarEstado()
        CargarFormasPago()
        CargarTipoDocumento()
        CargarCuotas()
    End Sub

    Private Sub CargarEstado()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarEstado")
            objClsAdmCaja = New clsAdmCajas
            Dim dsEstados As DataSet = objClsAdmCaja.GetCodigos(cteEstadoVentaCuadre, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With
            If Not dsEstados Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsEstados.Tables(0).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("codigo"))
                    dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()

                Dim dvEstados As DataView = dtResult.DefaultView
                dvEstados.Sort = "CODIGO DESC"

                cboEstado.DataSource = dvEstados
                cboEstado.DataTextField = "DESCRIPCION"
                cboEstado.DataValueField = "CODIGO"
                cboEstado.DataBind()

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INFO CargarEstado: No se encontraron registros de estado.")
            End If

            cboEstado.Items.Insert(0, New ListItem("TODOS", "0"))
            cboEstado.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarEstado")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarFormasPago()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarFormasPago")
            objClsOffline = New clsOffline
            Dim dsFormaPago As DataSet = objClsOffline.Get_ViasPagos()
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            If Not dsFormaPago Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsFormaPago.Tables(0).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("CCINS"))
                    dr("DESCRIPCION") = Trim(item("CCINS")) & " - " & Trim(item("VTEXT"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INFO CargarFormasPago: No se encontraron registros de forma de pago.")
            End If

            cboViaPago.DataSource = dtResult
            cboViaPago.DataTextField = "DESCRIPCION"
            cboViaPago.DataValueField = "CODIGO"
            cboViaPago.DataBind()

            cboViaPago.Items.Insert(0, New ListItem("TODOS", "0"))
            cboViaPago.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarFormasPago")
            objClsOffline = Nothing
        End Try
    End Sub

    Private Sub CargarTipoDocumento()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarTipoDocumento")
            objClsAdmCaja = New clsAdmCajas
            Dim dsTipoDoc As DataSet = objClsAdmCaja.GetCodigos(cteTipoDocumentos, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
                .Columns.Add("ORDEN", GetType(Int32))
            End With

            If Not dsTipoDoc Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsTipoDoc.Tables(0).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("codigo"))
                    dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                    dr("ORDEN") = CInt(item("DES_FILTRO1"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()

                Dim dvTipoDoc As DataView = dtResult.DefaultView
                dvTipoDoc.Sort = "ORDEN ASC"

                cboTipDoc.DataSource = dvTipoDoc
                cboTipDoc.DataTextField = "DESCRIPCION"
                cboTipDoc.DataValueField = "CODIGO"
                cboTipDoc.DataBind()
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INFO CargarTipoDocumento: No se encontraron registros de Tipo de DOcumentos.")
            End If

            cboTipDoc.Items.Insert(0, New ListItem("TODOS", "0"))
            cboTipDoc.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarTipoDocumento")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarCuotas()
        Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarCuotas")
            Dim dsCuotas As DataSet = clsConsultaPvu.ConsultaCuotas()
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            If Not dsCuotas Is Nothing Then
                Dim dr As DataRow
                For Each item As DataRow In dsCuotas.Tables(0).Rows
                    dr = dtResult.NewRow
                    dr("CODIGO") = Trim(item("CUOC_CODIGO"))
                    dr("DESCRIPCION") = Trim(item("CUOC_CODIGO")) & " - " & Trim(item("CUOV_DESCRIPCION"))
                    dtResult.Rows.Add(dr)
                Next
                dtResult.AcceptChanges()
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " No se encontraron registros del nùmero de cuotas.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Verificar: PVU.SISACT_PKG_GENERAL.SP_CON_TIPO_CUOTA")
            End If

            cboCuotas.DataSource = dtResult
            cboCuotas.DataTextField = "DESCRIPCION"
            cboCuotas.DataValueField = "CODIGO"
            cboCuotas.DataBind()

            cboCuotas.Items.Insert(0, New ListItem("TODOS", "0"))
            cboCuotas.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarCuotas")
            clsConsultaPvu = Nothing
        End Try
    End Sub

    Private Sub CargarCajeros(ByVal oficinaVta As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarCajeros")
            objClsAdmCaja = New clsAdmCajas
            Dim strCodCajero As String = txtCodCajero.Text
            If oficinaVta.Equals(String.Empty) Then
                oficinaVta = 0
            End If

            Dim dsCajeros As DataSet = objClsAdmCaja.GetVendedores(strCodCajero, oficinaVta, cteCajero)
            If Not dsCajeros.Tables(0) Is Nothing Then
                If dsCajeros.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró cajero'); </script>")
                Else
                    Dim drCj As DataRow = dsCajeros.Tables(0).Rows(0)
                    txtCodCajero.Text = Trim(drCj("CODIGO"))
                    hidCodCajero.Value = Trim(drCj("CODIGO"))
                    txtCajero.Text = Trim(drCj("DESCRIPCION"))
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarCajeros")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarVendedores(ByVal oficinaVta As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarVendedores")
            objClsAdmCaja = New clsAdmCajas
            Dim strCodVendedor As String = txtCodVendedor.Text
            If oficinaVta.Equals(String.Empty) Then
                oficinaVta = 0
            End If

            Dim dsVendedores As DataSet = objClsAdmCaja.GetVendedores(strCodVendedor, oficinaVta, cteVendedor)
            If Not dsVendedores.Tables(0) Is Nothing Then
                If dsVendedores.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró vendedores'); </script>")
                Else
                    Dim drCj As DataRow = dsVendedores.Tables(0).Rows(0)
                    txtCodVendedor.Text = Trim(drCj("CODIGO"))
                    hidCodVendedor.Value = Trim(drCj("CODIGO"))
                    txtVendedor.Text = Trim(drCj("DESCRIPCION"))
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarVendedores")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarOficinaVta()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarOficinaVta")
            Dim dsOficinaVta As DataSet
            Dim strCodOficina As String
            objClsAdmCaja = New clsAdmCajas
            'strCodOficina = txtCodOficina.Text
            dsOficinaVta = objClsAdmCaja.GetOficinas(strCodOficina)

            If Not dsOficinaVta.Tables(0) Is Nothing Then
                If dsOficinaVta.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró oficina de venta'); </script>")
                Else
                    Dim drOf As DataRow = dsOficinaVta.Tables(0).Rows(0)
                    'txtCodOficina.Text = Trim(drOf("CODIGO"))
                    hidCodOficina.Value = Trim(drOf("CODIGO"))
                    'txtOficina.Text = Trim(drOf("DESCRIPCION"))
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarOficinaVta")
            objClsAdmCaja = Nothing
        End Try
    End Sub

#End Region

End Class
