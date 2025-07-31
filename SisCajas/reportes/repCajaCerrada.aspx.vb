Imports SisCajas.clsLib_Session
Imports SisCajas.clsContantes_site

Public Class repCajaCerrada
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
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFecInicio As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFecFinal As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblAlerta As System.Web.UI.WebControls.Label

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Public strFecInicio As String
    Public strFecFinal As String
    Public strSiteOcultarCapas As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        'Session("USUARIO") = "1000054"
        'Session("ALMACEN") = "0006"
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not IsPostBack Then
                Session("FechaAct") = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Format(Now, "d")
                strFecInicio = Session("FechaAct")
                strFecFinal = Session("FechaAct")
                txtFecInicio.Value = strFecInicio
                txtFecFinal.Value = strFecFinal
            End If


            'Introducir aquí el código de usuario para inicializar la página

            If (Len(Session("STRMessage")) > 0) Then
                Response.Write("<script language=JavaScript type='text/javascript'>")
                Response.Write("alert('" & Session("STRMessage") & "');")
                Response.Write("</script>")
            End If
            Session("STRMessage") = ""
        End If

    End Sub


    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If ValidarFechas(txtFecInicio.Value, txtFecFinal.Value) = True Then
            'Session("fecini") = txtFecInicio.Value
            'Session("fecfin") = txtFecFinal.Value

            Response.Redirect("repCajaCerradaDetalle.aspx?ini=" + txtFecInicio.Value + ",fin=" + txtFecFinal.Value)
        End If
        
    End Sub

    Function ValidarFechas(ByVal fecini As String, ByVal fecfin As String) As Boolean
        ValidarFechas = True

        If fecini = "" Or fecfin = "" Then
            ValidarFechas = False
            MensajeError("Sicar", "Debe ingresar las fechas de inicio y fin.")
            Exit Function
        End If

        If CDate(fecfin) > Now.Date Then
            ValidarFechas = False
            MensajeError("Sicar", "La fechas no deben ser mayores a la fecha actual.")
            Exit Function
        End If

        If CDate(fecini) > CDate(fecfin) Then
            ValidarFechas = False
            MensajeError("Sicar", "La fecha de inicio debe ser menor o iguala la fecha final.")
            Exit Function
        End If


    End Function

    Public Sub MensajeError(ByVal pTitulo As String, ByVal pMensaje As String)
        If Not Me.IsStartupScriptRegistered(pTitulo) Then
            Me.RegisterStartupScript(pTitulo, "<script> alert('" + pMensaje + "'); </script>")
        End If
    End Sub

End Class
