Public Class visorRemesa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtCodOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaBuzIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaBuzFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtMonto01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoRemesa As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtBolsa01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBolsa02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSobre01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSobre02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodUsuarioFI As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUsuarioFI As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaFI1 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFI2 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents hidCodUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFlag As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents loadUsuarioHandler As System.Web.UI.WebControls.Button
    Protected WithEvents txtDocContableIni As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocContableFin As System.Web.UI.WebControls.TextBox

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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorRemesa")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorRemesa")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Dim objClsContab As COM_SIC_Recaudacion.clsContabilizar
    Dim objClsOffline As COM_SIC_OffLine.clsOffline
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const CAJERO As String = "C"
    Private Const TIPO_REMESA As String = "TIPO_REMESA"
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
            If Not IsPostBack Then
                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                CargarParametros()
                txtMonto01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMonto02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtCodOficina.Attributes.Add("onkeydown", "f_buscarOficina();")
                txtCodCajero.Attributes.Add("onkeydown", "f_buscarCajero();")
                txtCodUsuarioFI.Attributes.Add("onkeydown", "f_buscarUsuario();")
                txtCodOficina.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCodCajero.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")

                Session("ReporteRemesas") = Nothing 'INI-936 - CNSO
            End If
        End If
    End Sub

    'INI-936 - CNSO - Modificado para guardar dataset en sesion y redireccionar al visorRemesa_detalle.aspx
    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI  cmdBuscar_ServerClick")

            Dim strBolsaIni As String = txtBolsa01.Text
            Dim strBolsaFin As String = txtBolsa02.Text
            Dim strSobreIni As String = txtSobre01.Text
            Dim strSobreFin As String = txtSobre02.Text
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strFechaBuzIni As String = txtFechaBuzIni.Value
            Dim strFechaBuzFin As String = txtFechaBuzFin.Value
            Dim strCajero As String = hidCodCajero.Value
            Dim strMto1 As String = txtMonto01.Text
            Dim strMto2 As String = txtMonto02.Text
            Dim strTipoRemesa As String = cboTipoRemesa.SelectedValue
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strDocContableIni As String = txtDocContableIni.Text
            Dim strDocContableFin As String = txtDocContableFin.Text
            Dim strCodUsuario As String = hidCodUsuario.Value
            Dim strFechaFIIni As String = txtFechaFI1.Value
            Dim strFechaFIFin As String = txtFechaFI2.Value
            Dim strCntRegistros As String = txtCntRegistros.Text
            Dim strUrl As String = "visorRemesa_detalle.aspx"

            Dim dsResult As New DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas

            dsResult = objClsAdmCaja.GetRemesa(strBolsaIni, strBolsaFin, strSobreIni, strSobreFin, _
                            strFechaIni, strFechaFin, strFechaBuzIni, strFechaBuzFin, strCajero, strMto1, strMto2, _
                            strTipoRemesa, strOficinaVta, strDocContableIni, strDocContableFin, _
                            strCodUsuario, strFechaFIIni, strFechaFIFin, strCntRegistros)

            If dsResult.Tables(0).Rows.Count > 0 Then
                Dim dtTblRemesa As New DataTable
                Session("ReporteRemesas") = dsResult.Tables(0)
                Response.Redirect(strUrl)
            Else
                txtBolsa01.Text = ""
                txtBolsa02.Text = ""
                txtSobre01.Text = ""
                txtSobre02.Text = ""
                txtFechaBuzIni.Value = ""
                txtFechaBuzFin.Value = ""
                txtFechaIni.Value = ""
                txtFechaFin.Value = ""
                txtCodCajero.Text = ""
                txtMonto01.Text = ""
                txtMonto02.Text = ""
                cboTipoRemesa.SelectedValue = "0"
                txtCodOficina.Text = ""
                txtDocContableIni.Text = ""
                txtDocContableFin.Text = ""
                txtCodUsuarioFI.Text = ""
                txtFechaFI1.Value = ""
                txtFechaFI2.Value = ""

                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  cmdBuscar_ServerClick")
        End Try
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
                            If hidFlag.Value = "0" Then
                                txtCodCajero.Text = Trim(drvResultado("CODIGO"))
                                txtCajero.Text = Trim(drvResultado("DESCRIPCION"))
                            Else
                                txtCodUsuarioFI.Text = Trim(drvResultado("CODIGO"))
                                txtUsuarioFI.Text = Trim(drvResultado("DESCRIPCION"))
                            End If
                        End With
                        If hidFlag.Value = "0" Then
                            hidCodCajero.Value = CStr(Session("visor_codCajero")).Substring(5, 5)
                        Else
                            hidCodUsuario.Value = CStr(Session("visor_codCajero")).Substring(5, 5)
                        End If
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

    Private Sub loadUsuarioHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadUsuarioHandler.Click
        CargarUsuarios(hidCodOficina.Value)
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarParametros()
        CargarTipoRemesa()
    End Sub

    Private Sub CargarTipoRemesa()

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarTipoRemesa")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsTipoRemesa As DataSet = objClsAdmCaja.GetCodigos(TIPO_REMESA, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With

            Dim dr As DataRow
            For Each item As DataRow In dsTipoRemesa.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = item("codigo")
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            Dim dvTipoRemesa As DataView = dtResult.DefaultView
            dvTipoRemesa.Sort = "CODIGO ASC"

            cboTipoRemesa.DataSource = dvTipoRemesa
            cboTipoRemesa.DataTextField = "DESCRIPCION"
            cboTipoRemesa.DataValueField = "CODIGO"
            cboTipoRemesa.DataBind()

            cboTipoRemesa.Items.Insert(0, New ListItem("TODOS", "0"))
            cboTipoRemesa.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarTipoRemesa")
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
                    txtCodOficina.Text = String.Empty
                    hidCodOficina.Value = String.Empty
                    txtOficina.Text = String.Empty
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
                    txtCodCajero.Text = String.Empty
                    hidCodCajero.Value = String.Empty
                    txtCajero.Text = String.Empty
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

    Private Sub CargarUsuarios(ByVal oficinaVta As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarUsuarios")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strCodUsuarioFI As String = txtCodUsuarioFI.Text
            If oficinaVta.Equals(String.Empty) Then
                oficinaVta = 0
            End If

            Dim dsUsuario As DataSet = objClsAdmCaja.GetVendedores(strCodUsuarioFI, oficinaVta, CAJERO)
            If Not dsUsuario.Tables(0) Is Nothing Then
                If dsUsuario.Tables(0).Rows.Count = 0 Then
                    txtCodUsuarioFI.Text = String.Empty
                    hidCodUsuario.Value = String.Empty
                    txtUsuarioFI.Text = String.Empty
                    Response.Write("<script language=jscript> alert('No se encontró usuario'); </script>")
                Else
                    Dim drCj As DataRow = dsUsuario.Tables(0).Rows(0)
                    txtCodUsuarioFI.Text = Trim(drCj("CODIGO"))
                    hidCodUsuario.Value = Trim(drCj("CODIGO")).Substring(5, 5)
                    txtUsuarioFI.Text = Trim(drCj("DESCRIPCION"))
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarUsuarios")
            objClsAdmCaja = Nothing
        End Try
    End Sub

#End Region

End Class