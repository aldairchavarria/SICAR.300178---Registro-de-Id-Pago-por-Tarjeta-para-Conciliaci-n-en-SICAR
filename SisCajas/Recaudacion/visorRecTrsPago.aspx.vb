Public Class visorRecTrsPago
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNroTran01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroTran02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoPagado01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoPagado02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMtoTotPagado01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMtoTotPagado02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNroTelefono01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroTelefono02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNroCheque01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroCheque02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocCont01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocCont02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadDataSubOfi As System.Web.UI.WebControls.Button
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents hidSubOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents txtRuc1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRuc2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboServicio As System.Web.UI.WebControls.DropDownList

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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String = String.Empty
    Dim objClsContab As COM_SIC_Recaudacion.clsContabilizar
    Dim objClsOffline As COM_SIC_OffLine.clsOffline
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const CAJERO As String = "C"
    Private Const ESTADOS As String = "ESTADO TRANSACCION"
    Private Const TIPO_DOCUMENTO_REC As String = "TIPO_DOC_REC"

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
                txtNroTran01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroTran02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCheque01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCheque02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroTelefono01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroTelefono02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtDocCont01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtDocCont02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtMontoPagado01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMontoPagado02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMtoTotPagado01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMtoTotPagado02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtCodOficina.Attributes.Add("onkeydown", "f_buscarOficina();")
                txtCodCajero.Attributes.Add("onkeydown", "f_buscarCajero();")
                txtCodOficina.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCodCajero.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtRuc1.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtRuc2.Attributes.Add("onkeydown", "validarNumero(this.value);")



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
                txtCodOficina.Text = strCodOficina
                txtOficina.Text = strOficina
                lbCodOficina.Items.Clear()
                lbOficina.Items.Clear()
                lbCodOficina.Items.Add(New ListItem(Trim(strCodSubOficina)))
                lbOficina.Items.Add(New ListItem(Trim(strSubOficina)))
                hidCodOficina.Value = strCodOficina
                hidSubOficina.Value = "'" & Trim(strCodSubOficina) & "',"
                chkTodosOf.Checked = False
                Me.RegisterStartupScript("Desactivar Controle", "<script language=javascript>f_DesactivarControles();</script>")
            Else
                chkTodosOf.Checked = True
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " --- Fin - Validando Sub Oficina DAC - RecTrsPago ---")
            'Proy-33111- Fin '


        End If

    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            If Not Session("dgVListaOficina") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgVListaOficina"), DataTable)
                If Not Session("visor_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("visor_codOficina"))
                    Dim dv As New DataView
                    dv.Table = dt
                    dv.RowFilter = "CODIGO = '" & codigoOfi & "'"
                    Dim drvResultado As DataRowView = dv.Item(0)
                    If Not drvResultado Is Nothing Then
                        With drvResultado
                            txtCodOficina.Text = Trim(drvResultado("CODIGO"))
                            txtOficina.Text = Trim(drvResultado("DESCRIPCION"))
                        End With
                        hidCodOficina.Value = CStr(Session("visor_codOficina"))
                    End If
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
                        hidCodCajero.Value = CStr(Session("visor_codCajero")).Substring(5, 5)
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub loadCajeroHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadCajeroHandler.Click
        CargarCajeros(hidCodOficina.Value)
    End Sub

    Private Sub loadOficinaHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadOficinaHandler.Click
        CargarOficinaVta()
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            strIdentifyLog = Session("ALMACEN") & "|" & strUsuario

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INICIO - cmdBuscar_Click")
            Dim strNroTransac1 As String = txtNroTran01.Text
            Dim strNroTransac2 As String = txtNroTran02.Text
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strSubOficinaVta As String = hidSubOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strMtoPagado1 As String = txtMontoPagado01.Text
            Dim strMtoPagado2 As String = txtMontoPagado02.Text
            Dim strMtoTotPago1 As String = txtMtoTotPagado01.Text
            Dim strMtoTotPago2 As String = txtMtoTotPagado02.Text
            Dim strEstado As String = cboEstado.SelectedValue
            Dim strNroTel1 As String = txtNroTelefono01.Text
            Dim strNroTel2 As String = txtNroTelefono02.Text
            Dim strCajero As String = hidCodCajero.Value
            Dim strViaPago As String = cboViaPago.SelectedValue
            Dim strNroCheque1 As String = txtNroCheque01.Text
            Dim strNroCheque2 As String = txtNroCheque02.Text
            Dim strDocContab1 As String = txtDocCont01.Text
            Dim strDocContab2 As String = txtDocCont02.Text
            Dim strCntRegistros As String = txtCntRegistros.Text
            Dim strRuc1 As String = txtRuc1.Text
            Dim strRuc2 As String = txtRuc2.Text
            Dim strServicio As String = cboServicio.SelectedValue
            Dim strUrl As String = "visorRecTrsPago_detalle.aspx"

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strSubOficinaVta: " & strSubOficinaVta)

            If strSubOficinaVta.Length > 0 Then
                strSubOficinaVta = strSubOficinaVta.Substring(0, strSubOficinaVta.Length - 1)
            End If

            If strOficinaVta.Length = 0 Then
                strOficinaVta = txtCodOficina.Text
            End If

            If strCajero.Length = 0 Then
                strCajero = txtCodCajero.Text
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOficinaVta: " & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strSubOficinaVta: " & strSubOficinaVta)

            strUrl = strUrl & "?nt1=" & strNroTransac1 & "&nt2=" & strNroTransac2 _
                    & "&of=" & strOficinaVta & "&subof=" & strSubOficinaVta _
                    & "&fi=" & strFechaIni & "&ff=" & strFechaFin _
                    & "&mp1=" & strMtoPagado1 & "&mp2=" & strMtoPagado2 & "&mtp1=" & strMtoTotPago1 _
                    & "&mtp2=" & strMtoTotPago2 & "&est=" & strEstado & "&tel1=" & strNroTel1 _
                    & "&tel2=" & strNroTel2 & "&cj=" & strCajero & "&vp=" & strViaPago _
                    & "&nchq1=" & strNroCheque1 & "&nchq2=" & strNroCheque2 & "&dcon1=" & strDocContab1 _
                    & "&dcon2=" & strDocContab2 & "&rc1=" & strRuc1 & "&rc2=" & strRuc2 & "&svc=" & strServicio & "&creg=" & strCntRegistros
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strUrl: " & strUrl)
            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIN - cmdBuscar_Click")
        End Try
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

                    lbOficina.Items.Clear()
                    lbCodOficina.Items.Clear()

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "SUOFV_ID = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodOficina.Items.Add(New ListItem(Trim(drvResultado("SUOFV_ID"))))
                                strCodigos = strCodigos + Trim(drvResultado("SUOFV_ID")) + ";"
                                lbOficina.Items.Add(New ListItem(Trim(drvResultado("SUOFV_SUB_OFICINA"))))
                                strSubOficinasDesc = strSubOficinasDesc + Trim(drvResultado("SUOFV_SUB_OFICINA")) + ";"
                                strSubOficinas = strSubOficinas + "'" + Trim(drvResultado("SUOFV_ID")) + "',"
                            End With
                        End If
                    Next
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strSubOficinasDesc : " & strSubOficinasDesc)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & " strSubOficinas : " & strSubOficinas)
                    hidSubOficina.Value = CStr(strSubOficinas)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "hidSubOficina : " & hidSubOficina.Value)
                    chkTodosOf.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error loadDataSubOfi_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin loadDataHandler_Click")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarParametros()
        CargarEstado()
        CargarViasPago()
        CargarServicio()
    End Sub

    Private Sub CargarEstado()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarEstado")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsEstados As DataSet = objClsAdmCaja.GetCodigos(ESTADOS, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(Int32))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            Dim dr As DataRow
            For Each item As DataRow In dsEstados.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = CInt(item("codigo"))
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            Dim dvEstados As DataView = dtResult.DefaultView
            dvEstados.Sort = "CODIGO ASC"

            cboEstado.DataSource = dvEstados
            cboEstado.DataTextField = "DESCRIPCION"
            cboEstado.DataValueField = "CODIGO"
            cboEstado.DataBind()

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

    Private Sub CargarViasPago()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  ViasPagos")
            objClsOffline = New COM_SIC_OffLine.clsOffline
            Dim dsFormaPago As DataSet = objClsOffline.Get_ViasPagos()
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With
            Dim dr As DataRow
            For Each item As DataRow In dsFormaPago.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = Trim(item("CCINS"))
                dr("DESCRIPCION") = Trim(item("CCINS")) & " - " & Trim(item("VTEXT"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  ViasPagos")
            objClsOffline = Nothing
        End Try
    End Sub

    Private Sub CargarServicio()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarServicio")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsEstados As DataSet = objClsAdmCaja.GetCodigos(TIPO_DOCUMENTO_REC, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            Dim dr As DataRow
            For Each item As DataRow In dsEstados.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = Trim(item("codigo"))
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            cboServicio.DataSource = dtResult
            cboServicio.DataTextField = "DESCRIPCION"
            cboServicio.DataValueField = "CODIGO"
            cboServicio.DataBind()

            cboServicio.Items.Insert(0, New ListItem("TODOS", "0"))
            cboServicio.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarServicio")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarCajeros(ByVal oficinaVta As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarCajeros")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strCodCajero As String = txtCodCajero.Text
            If oficinaVta.Equals(String.Empty) Then
                oficinaVta = 0
            End If

            Dim dsCajeros As DataSet = objClsAdmCaja.GetVendedores(strCodCajero, oficinaVta, CAJERO)
            If Not dsCajeros.Tables(0) Is Nothing Then
                If dsCajeros.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró cajero'); </script>")
                Else
                    Dim drCj As DataRow = dsCajeros.Tables(0).Rows(0)
                    txtCodCajero.Text = Trim(drCj("CODIGO"))
                    hidCodCajero.Value = Trim(drCj("CODIGO")).Substring(5, 5)
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

    Private Sub CargarOficinaVta()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarOficinaVta")
            Dim dsOficinaVta As DataSet
            Dim strCodOficina As String
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            strCodOficina = txtCodOficina.Text
            dsOficinaVta = objClsAdmCaja.GetOficinas(strCodOficina)

            If Not dsOficinaVta.Tables(0) Is Nothing Then
                If dsOficinaVta.Tables(0).Rows.Count = 0 Then
                    Response.Write("<script language=jscript> alert('No se encontró oficina de venta'); </script>")
                Else
                    Dim drOf As DataRow = dsOficinaVta.Tables(0).Rows(0)
                    txtCodOficina.Text = Trim(drOf("CODIGO"))
                    hidCodOficina.Value = Trim(drOf("CODIGO"))
                    txtOficina.Text = Trim(drOf("DESCRIPCION"))
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
