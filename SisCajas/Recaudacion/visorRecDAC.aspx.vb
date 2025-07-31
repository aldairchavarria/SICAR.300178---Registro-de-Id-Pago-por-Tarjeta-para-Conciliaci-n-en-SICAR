Public Class visorRecDAC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNroDoc01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDoc02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOficina As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCliente01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodCliente02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoPago01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoPago02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCodCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCntRegistros As System.Web.UI.WebControls.TextBox
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadOficinaHandler As System.Web.UI.WebControls.Button
    Protected WithEvents loadCajeroHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodCajero As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton

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
    Private Const ESTADO As String = "ESTADO_DAC"
    Private Const CAJERO As String = "C"

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
                CargarEstado()
                txtNroDoc01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtNroDoc01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtCodCliente01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtCodCliente01.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtMontoPago01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMontoPago02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                txtCodOficina.Attributes.Add("onkeydown", "f_buscarOficina();")
                txtCodCajero.Attributes.Add("onkeydown", "f_buscarCajero();")
                txtCodOficina.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCodCajero.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
            End If
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
            Dim strNroDoc1 As String = txtNroDoc01.Text
            Dim strNroDocc2 As String = txtNroDoc02.Text
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strMtoPago1 As String = txtMontoPago01.Text
            Dim strMtoPago2 As String = txtMontoPago02.Text
            Dim strEstado As String = cboEstado.SelectedValue
            Dim strCajero As String = hidCodCajero.Value
            Dim strCodCliente1 As String = txtCodCliente01.Text
            Dim strCodCliente2 As String = txtCodCliente02.Text
            Dim strCntRegistros As String = txtCntRegistros.Text
            Dim strUrl As String = "visorRecDAC_detalle.aspx"

            strUrl = strUrl & "?nd1=" & strNroDoc1 & "&nd2=" & strNroDocc2 _
                    & "&of=" & strOficinaVta & "&fi=" & strFechaIni & "&ff=" & strFechaFin _
                    & "&mp1=" & strMtoPago1 & "&mp2=" & strMtoPago2 _
                    & "&est=" & strEstado & "&cj=" & strCajero & "&cc1=" & strCodCliente1 _
                    & "&cc2=" & strCodCliente2 & "&creg=" & strCntRegistros

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

    Private Sub CargarEstado()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio  CargarEstado")
            objClsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsEstados As DataSet = objClsAdmCaja.GetCodigos(ESTADO, String.Empty)
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
