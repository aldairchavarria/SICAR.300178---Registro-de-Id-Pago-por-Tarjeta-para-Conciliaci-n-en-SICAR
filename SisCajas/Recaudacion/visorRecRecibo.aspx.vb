Public Class visorRecRecibo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNroTran01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroTran02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipDocRecaudacion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNroDocRec1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDocRec2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtImporteRec1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtImporteRec2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMtoTotPagado01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMtoTotPagado02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroCobranza01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroCobranza02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroAcreedor1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroAcreedor2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDoc01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDoc02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTracePago1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTracePago2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
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
    Dim objClsOffline As COM_SIC_OffLine.clsOffline
    Dim objClsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const MONEDAS As String = "TIPO MONEDA"
    Private Const TIPO_DOC_REC As String = "TIPO_DOC_REC"
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
                CargarTipoDocRecauda()
                CargarMoneda()
                txtNroTran01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroTran02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroDocRec1.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroDocRec2.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCobranza01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroCobranza02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroAcreedor1.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroAcreedor1.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroDoc01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroDoc02.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtTracePago1.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtTracePago2.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtImporteRec1.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtImporteRec2.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMtoTotPagado01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMtoTotPagado02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
            End If
        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI - Recibo - cmdBuscar_Click")

            Dim strNroTransac1 As String = txtNroTran01.Text
            Dim strNroTransac2 As String = txtNroTran02.Text
            Dim strTipoDocRec As String = cboTipDocRecaudacion.SelectedValue
            Dim strNroDocRec1 As String = txtNroDocRec1.Text
            Dim strNroDocRec2 As String = txtNroDocRec2.Text
            Dim strMoneda As String = cboMoneda.SelectedValue
            Dim strImporteRecibo1 As String = txtImporteRec1.Text
            Dim strImporteRecibo2 As String = txtImporteRec2.Text
            Dim strMtoTotPago1 As String = txtMtoTotPagado01.Text
            Dim strMtoTotPago2 As String = txtMtoTotPagado02.Text
            Dim strNroCob1 As String = txtNroCobranza01.Text
            Dim strNroCob2 As String = txtNroCobranza02.Text
            Dim strNroAcre1 As String = txtNroAcreedor1.Text
            Dim strNroAcre2 As String = txtNroAcreedor2.Text
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strNroDoc1 As String = txtNroDoc01.Text
            Dim strNroDoc2 As String = txtNroDoc02.Text
            Dim strTrcPago1 As String = txtTracePago1.Text
            Dim strTrcPago2 As String = txtTracePago2.Text
            Dim strCntRegistros As String = txtCntRegistros.Text

            Dim strUrl As String = "visorRecRecibo_detalle.aspx"

            strUrl = strUrl & "?nt1=" & strNroTransac1 & "&nt2=" & strNroTransac2 _
                    & "&tdr=" & strTipoDocRec & "&ndr1=" & strNroDocRec1 & "&ndr2=" & strNroDocRec2 _
                    & "&mn=" & strMoneda & "&ir1=" & strImporteRecibo1 & "&ir2=" & strImporteRecibo2 _
                    & "&mtp1=" & strMtoTotPago1 & "&mtp2=" & strMtoTotPago2 _
                    & "&nc1=" & strNroCob1 & "&nc2=" & strNroCob2 _
                    & "&na1=" & strNroAcre1 & "&na2=" & strNroAcre2 _
                    & "&fi=" & strFechaIni & "&ff=" & strFechaFin _
                    & "&ndoc1=" & strNroDoc1 & "&ndoc2=" & strNroDoc2 _
                    & "&tp1=" & strTrcPago1 & "&tp2=" & strTrcPago2 & "&creg=" & strCntRegistros

            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Recibo - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN - Recibo - cmdBuscar_Click")
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub CargarTipoDocRecauda()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarTipoDocRecauda")
            Dim dsTipoDocRec As DataSet
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            dsTipoDocRec = objClsAdmCaja.GetCodigos(TIPO_DOC_REC, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With
            Dim dr As DataRow
            For Each item As DataRow In dsTipoDocRec.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = Trim(item("codigo"))
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            cboTipDocRecaudacion.DataSource = dtResult
            cboTipDocRecaudacion.DataTextField = "DESCRIPCION"
            cboTipDocRecaudacion.DataValueField = "CODIGO"
            cboTipDocRecaudacion.DataBind()

            cboTipDocRecaudacion.Items.Insert(0, New ListItem("TODOS", "0"))
            cboTipDocRecaudacion.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN  CargarTipoDocRecauda")
            objClsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub CargarMoneda()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI - Recibos - CargarMoneda")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsMonedas As DataSet = objClsAdmCaja.GetCodigos(MONEDAS, String.Empty)
            Dim dtResult As New DataTable
            With dtResult
                .Columns.Add("CODIGO", GetType(String))
                .Columns.Add("DESCRIPCION", GetType(String))
            End With
            Dim dr As DataRow
            For Each item As DataRow In dsMonedas.Tables(0).Rows
                dr = dtResult.NewRow
                dr("CODIGO") = Trim(item("codigo"))
                dr("DESCRIPCION") = Trim(item("codigo")) & " - " & Trim(item("descripcion"))
                dtResult.Rows.Add(dr)
            Next
            dtResult.AcceptChanges()

            cboMoneda.DataSource = dtResult
            cboMoneda.DataTextField = "DESCRIPCION"
            cboMoneda.DataValueField = "CODIGO"
            cboMoneda.DataBind()

            cboMoneda.Items.Insert(0, New ListItem("TODOS", "0"))
            cboMoneda.SelectedIndex = 0
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Recibos - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN - Recibos - CargarMoneda")
            objClsAdmCaja = Nothing
        End Try
    End Sub

#End Region

End Class
