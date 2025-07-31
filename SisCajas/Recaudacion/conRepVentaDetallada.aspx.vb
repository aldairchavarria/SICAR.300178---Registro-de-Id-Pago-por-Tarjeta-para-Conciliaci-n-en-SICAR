#Region "Imports"
Imports COM_SIC_Adm_Cajas
Imports COM_SIC_OffLine
#End Region

Public Class conRepVentaDetallada
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboTipDoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDocSunat01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocSunat02 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoFact01 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoFact02 As System.Web.UI.WebControls.TextBox
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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorVentaDet")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorVentaDet")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Dim objClsAdmCaja As clsAdmCajas
    Dim objClsOffline As clsOffline
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
        Else
            If Not IsPostBack Then
                strUsuario = Session("USUARIO")
                strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
                CargarParametros()
                txtMontoFact01.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtMontoFact02.Attributes.Add("onkeypress", "javascript:ValidaNumero(this);")
                txtCntRegistros.Attributes.Add("onkeydown", "validarNumero(this.value);")
                hidCodCajero.Value = strUsuario
                hidCodOficina.Value = Session("ALMACEN")
            End If
        End If
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI  Reporte Venta Detallada cmdBuscar_Click")
            Dim strOficinaVta As String = hidCodOficina.Value
            Dim strFechaIni As String = txtFechaIni.Value
            Dim strFechaFin As String = txtFechaFin.Value
            Dim strTipoDoc As String = cboTipDoc.SelectedValue
            Dim strDocSunat1 As String = txtDocSunat01.Text
            Dim strDocSunat2 As String = txtDocSunat02.Text
            Dim strMtoPagado1 As String = txtMontoFact01.Text
            Dim strMtoPagado2 As String = txtMontoFact02.Text
            Dim strCajero As String = CStr(hidCodCajero.Value).PadLeft(10, CChar("0"))

            Dim strViaPago As String = cboViaPago.SelectedValue
            Dim strCuotas As String = cboCuotas.SelectedValue
            Dim strEstado As String = cboEstado.SelectedValue
            Dim strCntRegistros As String = txtCntRegistros.Text

            Dim strUrl As String = String.Empty
            Dim sbUrl As New System.Text.StringBuilder
            sbUrl.Append("visRepVentaDetallada.aspx")
            sbUrl.AppendFormat("?of={0}", strOficinaVta)
            sbUrl.AppendFormat("&fi={0}", strFechaIni)
            sbUrl.AppendFormat("&ff={0}", strFechaFin)
            sbUrl.AppendFormat("&td={0}", strTipoDoc)
            sbUrl.AppendFormat("&mp1={0}", strMtoPagado1)
            sbUrl.AppendFormat("&mp2={0}", strMtoPagado2)
            sbUrl.AppendFormat("&ds1={0}", strDocSunat1)
            sbUrl.AppendFormat("&ds2={0}", strDocSunat2)
            sbUrl.AppendFormat("&cj={0}", strCajero)
            sbUrl.AppendFormat("&vp={0}", strViaPago)
            sbUrl.AppendFormat("&cuo={0}", strCuotas)
            sbUrl.AppendFormat("&est={0}", strEstado)
            sbUrl.AppendFormat("&creg={0}", strCntRegistros)
            strUrl = sbUrl.ToString()
            Response.Redirect(strUrl)
        Catch ex1 As Threading.ThreadAbortException
            'Do Nothing
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN Reporte Venta Detallada  cmdBuscar_Click")
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
            Dim dsFormaPago As DataSet = objClsOffline.Obtener_ConsultaViasPago(Session("ALMACEN"))
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

#End Region

End Class
