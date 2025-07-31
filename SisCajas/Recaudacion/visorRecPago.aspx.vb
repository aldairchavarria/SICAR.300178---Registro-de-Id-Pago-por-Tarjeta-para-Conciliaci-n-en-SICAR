Public Class visorRecPago
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNroTran01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroTran02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboViaPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtImportePagado01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtImportePagado02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroCheque01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroCheque02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocContable01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocContable02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox

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
    Dim strIdentifyLog As String
    Dim objOffline As COM_SIC_OffLine.clsOffline
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
                CargarViasPago()
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                txtNroTran01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroTran02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtDocContable01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtDocContable02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCheque01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCheque02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtImportePagado01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtImportePagado01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
            End If
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI - Visor Pagos - cmdBuscar_Click")

            Dim strNroTransac1 As String = txtNroTran01.Text
            Dim strNroTransac2 As String = txtNroTran02.Text
            Dim strViaPago As String = cboViaPago.SelectedValue
            Dim strImportePagado1 As String = txtImportePagado01.Text
            Dim strImportePagado2 As String = txtImportePagado02.Text
            Dim strNroChq1 As String = txtNroCheque01.Text
            Dim strNroChq2 As String = txtNroCheque02.Text
            Dim strNroContab1 As String = txtDocContable01.Text
            Dim strNroContab2 As String = txtDocContable02.Text
            Dim strUrl As String = "visorRecPago_detalle.aspx"
            Dim strCntRegistros As String = txtCntRegistros.Text

            strUrl = strUrl & "?nt1=" & strNroTransac1 & "&nt2=" & strNroTransac2 _
                    & "&vp=" & strViaPago & "&ip1=" & strImportePagado1 _
                    & "&ip2=" & strImportePagado2 & "&nchq1=" & strNroChq1 & "&nchq2=" & strNroChq2 _
                    & "&nc1=" & strNroContab1 & "&nc2=" & strNroContab2 & "&creg=" & strCntRegistros

            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Visor Pagos - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN - Visor Pagos - cmdBuscar_Click")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarViasPago()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI CargarOficinaVta")
            Dim dsViasPago As DataSet
            objOffline = New COM_SIC_OffLine.clsOffline
            dsViasPago = objOffline.Get_ViasPagos()

            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With
            Dim dr As DataRow
            For Each item As DataRow In dsViasPago.Tables(0).Rows
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
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarOficinaVta")
            objOffline = Nothing
        End Try
    End Sub

#End Region

End Class
