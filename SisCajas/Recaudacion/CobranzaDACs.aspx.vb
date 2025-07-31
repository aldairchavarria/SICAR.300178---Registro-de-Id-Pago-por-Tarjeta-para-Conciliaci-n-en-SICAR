Public Class CobranzaDACs
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtCodCliente As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvCodCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTrama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboOpcionPago As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTipoPago As System.Web.UI.WebControls.TextBox

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            txtCodCliente.Attributes.Add("onkeypress", "javascript:ValidaNumero(this)")
            If Session("strMensajeDAC") <> "" Then
                Response.Write("<script language=javascript>alert('" & Session("strMensajeDAC") & "')</script>")
                Session("strMensajeDAC") = ""
            End If
            'If Not Session("ClienteDAC") Is Nothing Then
            'If Not CStr(Session("ClienteDAC").Equals(String.Empty)) Then
            txtTrama.Text = Request.Item("strTrama")
            txtMonto.Text = Request.Item("strMonto")
            txtDealer.Text = Request.Item("Dealer")
            'End If
            'End If

            ' GB 04/2015
            If Not (Page.IsPostBack) Then

                Dim ds As New DataSet
                Dim table As DataTable = New DataTable("Combo")
                Dim column As DataColumn
                Dim row As DataRow

                column = New DataColumn
                column.DataType = System.Type.GetType("System.Int32")
                column.ColumnName = "value"
                table.Columns.Add(column)

                column = New DataColumn
                column.DataType = System.Type.GetType("System.String")
                column.ColumnName = "texto"
                table.Columns.Add(column)

                row = table.NewRow()
                row("value") = Convert.ToInt32(ConfigurationSettings.AppSettings("PrefijoPagoDAC"))
                row("texto") = "Pago DAC"
                table.Rows.Add(row)

                row = table.NewRow()
                row("value") = Convert.ToInt32(ConfigurationSettings.AppSettings("PrefijoPagoDRA"))
                row("texto") = "Pago DRA"
                table.Rows.Add(row)

                ds.Tables.Add(table)

                Me.cboOpcionPago.DataSource = ds
                Me.cboOpcionPago.DataValueField = "value"
                Me.cboOpcionPago.DataTextField = "texto"
                Me.cboOpcionPago.DataBind()

                Me.txtTipoPago.Text = ConfigurationSettings.AppSettings("PrefijoPagoDAC")
            End If
            '--//

        End If
    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        '//-- GB 04/2015
        Session("OpcionPago") = Me.txtTipoPago.Text.Trim()
        '//--

        Session("ClienteDAC") = Me.txtCodCliente.Text.Trim()
        Response.Redirect("detCobranzaDAC.aspx")
    End Sub


End Class
