Public Class sicar_MantCodTarjeta
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboTarjeta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents chkTodos As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnNuevo As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtOculto As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdNuevo As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        chkTodos.Attributes.Add("onChange", "f_chkCambio)")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then

                CargarCombo()
                gridDetalle.DataSource = Nothing
                gridDetalle.DataBind()

            End If
        End If



    End Sub


    Private Sub CargarGrilla()
        Try
            Dim objTransac As New COM_SIC_Activaciones.clsTarjetasPOS
            Dim dtTarjetas As New DataTable
            Dim strRespuesta As String
            Dim strCodRespuesta As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "Codigo Tarjeta : " & cboTarjeta.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "Flag Todos : " & chkTodos.Checked)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "ConsultarFormaPagoPos : " & "SISCAJC_CON_COD_TARJETAS")

            If (chkTodos.Checked) Then
                dtTarjetas = objTransac.CodigosTarjetaPos("", "", "", strCodRespuesta, strRespuesta)
                cboTarjeta.Enabled = False

            Else
                dtTarjetas = objTransac.CodigosTarjetaPos("", cboTarjeta.SelectedValue.ToString, "", strCodRespuesta, strRespuesta)
                cboTarjeta.Enabled = True
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "strRespuesta : " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "strCodRespuesta : " & strCodRespuesta)


            If (strRespuesta = "OK") Then

                If dtTarjetas.Rows.Count > 0 And Not dtTarjetas Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "count dtTarjetas : " & dtTarjetas.Rows.Count)
                    gridDetalle.DataSource = dtTarjetas
                    gridDetalle.DataBind()
                Else
                    gridDetalle.DataSource = Nothing
                    Response.Write("<script>alert('No se encontraron datos, revise los parametros ingresados')</script>")
                    gridDetalle.DataBind()
                End If

            Else

                    gridDetalle.DataSource = Nothing
                    gridDetalle.DataBind()
                    Response.Write("<script>alert('Error al cargar los datos')</script>")

                End If





        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarGrilla() : " & "Error CargarGrilla : " & ex.Message.ToString())
            Response.Write("<script>alert('Error al cargar los datos')</script>")
        End Try

    End Sub
    Private Sub CargarCombo()
        Dim objTransac As New COM_SIC_Activaciones.clsTarjetasPOS
        Dim dtTarjetas As New DataTable
        Dim dtViasPago As New DataTable
        Dim strRespuesta As String
        Dim strCodRpta As String

        Try
            dtViasPago = objTransac.ConsultarViasPagoPos("", strCodRpta, strRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarCombo() : " & "Count ViasPago : " & dtViasPago.Rows.Count.ToString())

            cboTarjeta.DataSource = dtViasPago '''BANCO GIRADOR
            cboTarjeta.DataValueField = "ID_T_VIAS_PAGO"
            cboTarjeta.DataTextField = "VTEXT"
            cboTarjeta.DataBind()

            cboTarjeta.Items.Insert(0, New ListItem("Seleccionar", "0"))
            cboTarjeta.SelectedIndex = 0

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarCombo() : " & "Error CargarCombo : " & ex.Message.ToString())
        End Try

    End Sub
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargarGrilla()
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try
            gridDetalle.DataSource = Nothing
            gridDetalle.DataBind()
            cboTarjeta.SelectedIndex = 0
            chkTodos.Checked = False
            cboTarjeta.Enabled = True

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnLimpiar_Click() : " & "Error btnLimpiar_Click : " & ex.Message.ToString())
        End Try
    End Sub
End Class
