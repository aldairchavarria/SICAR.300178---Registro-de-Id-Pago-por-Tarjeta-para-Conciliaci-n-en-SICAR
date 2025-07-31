Imports COM_SIC_Configura

Public Class sicar_MantRecargaVirtual
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDescripcion As System.Web.UI.WebControls.TextBox
    Protected WithEvents gridDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                Inicio()
            Else
                BuscarRecargas("", "", "")
            End If
        End If
    End Sub

    Private Sub Inicio()
        LlenaComboEstado()
        Me.txtMonto.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
        BuscarRecargas("", "", "")
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

        BuscarRecargas(txtMonto.Text.Trim, txtDescripcion.Text.Trim, cboEstado.SelectedValue)

    End Sub

    Private Sub BuscarRecargas(ByVal strValorRecarga As String, ByVal strDescripcion As String, ByVal strEstado As String)

        Dim objConfig As New clsConfigura
        Dim dsRecargaVirtual As New System.Data.DataSet
        Dim dtRecargaVirtual As New System.Data.DataTable

        dsRecargaVirtual = objConfig.FP_Consulta_RecargaVirtual(strValorRecarga, strDescripcion, strEstado)
        If dsRecargaVirtual.Tables.Count > 0 Then
            dtRecargaVirtual = dsRecargaVirtual.Tables(0)

            For i As Integer = 0 To dtRecargaVirtual.Rows.Count - 1

                If dtRecargaVirtual.Rows(i)("REVIC_ESTADO_RECARGA").ToString = "0" Then
                    dtRecargaVirtual.Rows(i)("REVIC_ESTADO_RECARGA") = "Activo"
                Else
                    dtRecargaVirtual.Rows(i)("REVIC_ESTADO_RECARGA") = "Inactivo"
                End If
            Next


            gridDetalle.DataSource = dtRecargaVirtual
            gridDetalle.DataBind()
        Else
            gridDetalle.DataSource = New ArrayList
            gridDetalle.DataBind()

            Response.Write("<script>alert('No existen registros para la consulta.')</script>")
        End If
        RegistrarAuditoria()
        objConfig = Nothing

    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        txtMonto.Text = ""
        txtDescripcion.Text = ""

        gridDetalle.DataSource = New ArrayList
        gridDetalle.DataBind()

    End Sub

    Private Sub LlenaComboEstado()
        Dim dtEstado As New System.Data.DataTable
        Dim drFila As DataRow

        dtEstado.Columns.Add("CODIGO", GetType(System.String))
        dtEstado.Columns.Add("DESCRIPCION", GetType(System.String))


        drFila = dtEstado.NewRow
        drFila.Item("CODIGO") = ""
        drFila.Item("DESCRIPCION") = "-TODOS-"
        dtEstado.Rows.Add(drFila)

        drFila = dtEstado.NewRow
        drFila.Item("CODIGO") = "0"
        drFila.Item("DESCRIPCION") = "Activo"
        dtEstado.Rows.Add(drFila)

        drFila = dtEstado.NewRow
        drFila.Item("CODIGO") = "1"
        drFila.Item("DESCRIPCION") = "Inactivo"
        dtEstado.Rows.Add(drFila)

        cboEstado.DataSource = dtEstado
        cboEstado.DataValueField = "CODIGO"
        cboEstado.DataTextField = "DESCRIPCION"
        cboEstado.DataBind()

        cboEstado.SelectedValue = "0"
    End Sub
    Private Sub RegistrarAuditoria()
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim auditoriaGrabado As Boolean

            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsManRecVirtConsu")
            Dim descTrans As String = "Consulta Recarga Virtual"

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub
End Class
