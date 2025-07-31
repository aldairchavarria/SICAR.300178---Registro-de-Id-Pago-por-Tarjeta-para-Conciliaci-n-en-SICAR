Public Class bsqDocSinDeuda
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtIngreso As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemesa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMontoS As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkCierre As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificador As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoIdentificador As System.Web.UI.WebControls.DropDownList

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
        'Session("ALMACEN") = "0006"
        'Session("USUARIO") = "T11106"
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Len(Trim(Session("strMENSREC"))) > 0 Then
                Response.Write("<script language=javascript>alert('" & Session("strMENSREC") & "')</script>")
                Session("strMENSREC") = ""
            End If
            If Not Page.IsPostBack Then

                LeeParametros()

            End If
        End If
    End Sub

    Private Sub LeeParametros()
        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        Dim cteCODIGO_BINADQUIRIENTE As String = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")

        Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
        Me.intCanal.Value = cteCODIGO_CANAL
        Me.hdnUsuario.Value = Session("USUARIO")
        Me.hdnBinAdquiriente.Value = Session("ALMACEN") ' cteCODIGO_BINADQUIRIENTE
        Me.hdnCodComercio.Value = Session("ALMACEN")
        Me.hdnRutaLog.Value = cteCODIGO_RUTALOG
        Me.hdnDetalleLog.Value = cteCODIGO_DETALLELOG

    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click

        Try
            If ValidarDatosBusqueda(Me.hdnPuntoDeVenta.Value, Me.hdnUsuario.Value, Me.cboTipoIdentificador.SelectedValue, txtIdentificador.Text, Me.hdnBinAdquiriente.Value, Me.hdnCodComercio.Value) Then
                Response.Redirect("resDocSinDeuda.aspx?pTipoIdent=" & Me.cboTipoIdentificador.SelectedValue & "&pIdent=" & Me.txtIdentificador.Text, False)
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "');</script>")
        End Try

    End Sub

    Private Function ValidarDatosBusqueda( _
                                        ByVal strPuntoDeVenta As String, _
                                        ByVal strUsuario As String, _
                                        ByVal strTipoIdentificador As String, _
                                        ByVal strIdentificador As String, _
                                        ByVal strBinAdquiriente As String, _
                                        ByVal strCodComercio As String) As Boolean
        Dim strMensajeError
        strMensajeError = "no puede estar vacío o nulo."

        If Len(Trim(strPuntoDeVenta)) = 0 Then
            Throw New ApplicationException("El Punto de Venta " & strMensajeError)
        End If
        If Len(Trim(strUsuario)) = 0 Then
            Throw New ApplicationException("El Usuario " & strMensajeError)
        End If
        If Len(Trim(strTipoIdentificador)) = 0 Then
            Throw New ApplicationException("El Tipo de Identificador " & strMensajeError)
        End If
        If Len(Trim(strIdentificador)) = 0 Then
            Throw New ApplicationException("El Identificador " & strMensajeError)
        End If
        If Len(Trim(strBinAdquiriente)) = 0 Then
            Throw New ApplicationException("El Bin del Adquiriente " & strMensajeError)
        End If
        If Len(Trim(strCodComercio)) = 0 Then
            Throw New ApplicationException("El Código del Comercio " & strMensajeError)

        End If

        Return True
    End Function



End Class
