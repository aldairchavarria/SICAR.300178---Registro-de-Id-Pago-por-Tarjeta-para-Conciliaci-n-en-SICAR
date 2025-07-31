Imports System.Data

Public Class bsqDocumentosFijoPaginas
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIdentificador As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlServicios As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hidCodFactSolesTelefono As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Len(Trim(Session("strMENSREC"))) > 0 Then
                MensajeError("Mensaje", Session("strMENSREC").ToString)
                Session("strMENSREC") = String.Empty
            End If

            If Not Page.IsPostBack Then
                InicializaControles()
            End If
        End If
    End Sub

    Private Sub InicializaControles()
        '---
        Try
            Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
            Me.intCanal.Value = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
            Me.hdnBinAdquiriente.Value = Me.hdnPuntoDeVenta.Value '///ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
            Me.hdnCodComercio.Value = Me.hdnPuntoDeVenta.Value
            Me.hdnRutaLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
            Me.hdnDetalleLog.Value = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")
            Me.hidCodFactSolesTelefono.Value = ConfigurationSettings.AppSettings("constCodigoServFactSolesNumero")
            Me.hdnUsuario.Value = Session("USUARIO")

            '---carga combo
            Dim dtaServicios As DataTable
            Dim objServicios As New COM_SIC_Cajas.clsConsultaGeneral
            dtaServicios = objServicios.FP_Get_ListaServicios()
            ddlServicios.DataSource = dtaServicios
            ddlServicios.DataTextField = "DESCRIPCION"
            ddlServicios.DataValueField = "CODIGO_SERVICIO"
            ddlServicios.DataBind()
            dtaServicios = Nothing
        Catch ex As Exception
            'MensajeError("Mensaje", ex.Message)
            Response.Write(ex.Message)
            Response.End()
        End Try
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim sIdentificador As String = ConfigurationSettings.AppSettings("TipoIdentificadorFijoPaginas")
        If (sIdentificador = String.Empty) Then
            MensajeError("Mensaje", "No se ha encontrado el código de identificador predefinido para los Productos de Clientes Fijo y Páginas.")
        Else
            Dim sTipoIdentificador = "01"
            '// INICIO JYMMY
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            objOffline.EliminaPaginacion(Session.SessionID)
            '// FIN JYMMY
            Response.Redirect("resDocumentosFijoPaginas.aspx?TI=" & sTipoIdentificador & "&CS=" & Me.ddlServicios.SelectedValue & "&ID=" & sIdentificador & Me.txtIdentificador.Text)
        End If
    End Sub

    Public Sub MensajeError(ByVal pTitulo As String, ByVal pMensaje As String)
        If Not Me.IsStartupScriptRegistered(pTitulo) Then
            Me.RegisterStartupScript(pTitulo, "<script> alert('" + pMensaje + "'); </script>")
        End If
    End Sub


End Class
