Imports COM_SIC_Configura
Public Class sicar_nuevaRecargaVirtual
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescripcion As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hidAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents hidMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidFlagCambioForm As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidValores As System.Web.UI.HtmlControls.HtmlInputHidden

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

        If Not IsPostBack Then
            Inicio()
        Else
            Dim accion As String = hidAccion.Value
            hidAccion.Value = ""
            hidMensaje.Value = ""
            Select Case accion
                Case "G"
                    Grabar()
            End Select
        End If
    End Sub

    Private Sub Inicio()

        Me.btnGrabar.Attributes.Add("onclick", "f_Grabar()")
        Me.btnCancelar.Attributes.Add("onclick", "f_Cancelar()")
        Me.txtMonto.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")

        Me.LlenaComboEstado()

        Dim strAccion As String
        strAccion = Request.QueryString("accion")

        Select Case strAccion
            Case "Editar"
                InicioEditar()
            Case "Nuevo"
                InicioNuevo()
            Case Else
                Response.Redirect(ConfigurationSettings.AppSettings("RutaSite"))
                Response.End()
                Exit Sub
        End Select
        hidValores.Value = txtMonto.Text & txtDescripcion.Text & cboEstado.SelectedValue
    End Sub
    Private Sub InicioEditar()

        lblTitulo.Text = "Actualización de Recarga Virtual"
        txtMonto.Text = Request.QueryString("valor")
        txtDescripcion.Text = Request.QueryString("descripcion")
        cboEstado.SelectedValue = Request.QueryString("estado")

    End Sub

    Private Sub InicioNuevo()
        lblTitulo.Text = "Nueva Recarga Virtual"
        cboEstado.Enabled = False
    End Sub

    Private Sub Grabar()
        Dim strAccion As String
        strAccion = Request.QueryString("accion")

        Try
            Select Case strAccion
                Case "Editar"
                    EditarRecarga()
                Case "Nuevo"
                    GrabarRecarga()
            End Select
            hidAccion.Value = "OK"
        Catch ex As Exception
            hidMensaje.Value = ex.Message
            Exit Sub
        End Try
    End Sub

    Private Sub EditarRecarga()

        Dim objConfig As New clsConfigura
        Dim intResultado As Integer
        Dim id As Integer
        Dim strMonto As String
        Dim strDescripcion As String
        Dim strEstado As String

        id = Request.QueryString("id")
        strMonto = txtMonto.Text.Trim
        strDescripcion = txtDescripcion.Text
        strEstado = cboEstado.SelectedValue

        intResultado = objConfig.FP_Editar_RecargaVirtual(id, strMonto, strDescripcion, strEstado, Session("USUARIO"))
        Select Case intResultado
            Case 1
                Throw New Exception("Error. En Editar Nueva Regarga Virtual.")
            Case 2
                Throw New Exception("El monto y/o descripción ya se encuentra registrado.")
        End Select

        RegistroAuditoriaEditarRecarga(intResultado)

        objConfig = Nothing
    End Sub

    Private Sub GrabarRecarga()

        Dim objConfig As New clsConfigura
        Dim intResultado As Integer

        intResultado = objConfig.FP_Grabar_RecargaVirtual(txtMonto.Text.Trim, txtDescripcion.Text.Trim, cboEstado.SelectedValue, Session("USUARIO"))

        Select Case intResultado
            Case 1
                Throw New Exception("Error. En agregar Nueva Regarga Virtual.")
            Case 2
                Throw New Exception("El monto y/o descripción ya se encuentra registrado.")
        End Select

        RegistroAuditoriaNuevaRecarga(intResultado)

        objConfig = Nothing

    End Sub

    Private Sub LlenaComboEstado()
        Dim dtEstado As New System.Data.DataTable
        Dim drFila As DataRow

        dtEstado.Columns.Add("CODIGO", GetType(System.String))
        dtEstado.Columns.Add("DESCRIPCION", GetType(System.String))

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
    End Sub

    Private Sub RegistroAuditoriaNuevaRecarga(ByVal intResultado As Integer)
        Dim descTrans As String
        Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsManRecVirtNuevo")

        Select Case intResultado
            Case 0
                descTrans = "Nuevo Registro de Recarga Virtual"
            Case 1
                descTrans = "Error. En agregar Nueva Regarga Virtual."
            Case 2
                descTrans = "Error. El monto y/o descripción ya se encuentra registrado."
        End Select
        RegistrarAuditoria(strCodTrans, descTrans)
            
    End Sub

    Private Sub RegistroAuditoriaEditarRecarga(ByVal intResultado As Integer)
        
        Dim descTrans As String
        Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsManRecVirtEditar")

        Select Case intResultado
            Case 0
                descTrans = "Mantenimiento de Registro Recarga Virtual"
            Case 1
                descTrans = "Error. En Editar Regarga Virtual."
            Case 2
                descTrans = "Error. El monto y/o descripción ya se encuentra registrado."
        End Select
        RegistrarAuditoria(strCodTrans, descTrans)
          
    End Sub
    Private Sub RegistrarAuditoria(ByVal strCodTrans As String, ByVal descTrans As String)
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

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception

        End Try
    End Sub
End Class
