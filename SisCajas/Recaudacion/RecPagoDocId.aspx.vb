Public Class RecPagoDocId
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmdBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents hdnMensaje As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtDNI As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoServicio As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not Page.IsPostBack Then

            objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio RecPagoDocId.aspx Page_Load")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "--------------------------------------------------")

        End If

    End Sub

    Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim redirect$, dni$, servicio$
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio cmdBuscar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            dni = txtDNI.Text
            'servicio = cboTipoServicio.SelectedItem.Text
            servicio = cboTipoServicio.SelectedValue
            redirect = "RecPagoDocId_Det.aspx?pdni=" + dni + "&pservicio=" + servicio
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Redirect : " & " - " & redirect)
            If (ValidarDatosBusqueda(cboTipoServicio.SelectedValue, txtDNI.Text)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin cmdBuscar_Click")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                Response.Redirect(redirect, False)
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje de Advertencia : " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin cmdBuscar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
        End Try
    End Sub

    Private Function ValidarDatosBusqueda(ByVal strTipoServicio As String, ByVal strDni As String) As Boolean
        Dim strMensajeError
        strMensajeError = "no puede estar vacío o nulo."
        If Len(Trim(strTipoServicio)) = 0 Then
            Throw New ApplicationException("El Tipo de Servicio " & strMensajeError)
        End If
        If Len(Trim(strDni)) = 0 Then
            Throw New ApplicationException("El Nro de DNI " & strMensajeError)
        End If
        If Len(Trim(strDni)) <> 8 Then
            Throw New ApplicationException("El Nro de DNI debe tener 8 digitos")
        End If
        Return True
    End Function
End Class
