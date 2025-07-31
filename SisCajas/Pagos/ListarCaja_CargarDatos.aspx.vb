Public Class ListarCaja_CargarDatos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents DGListaOfi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents eliminarHandler As System.Web.UI.WebControls.Button
    Protected WithEvents refreshHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidIDCajaOfi As System.Web.UI.HtmlControls.HtmlInputHidden
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

#Region "Variables"
    Dim objclsAdmCaja As New COM_SIC_Adm_Cajas.clsAdmCajas
    Dim strPaginaRetorno As String
    Private Const optTODOS As String = "T"
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_MantenimientoCajaOficina"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConsultaCuadre")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
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
                Buscar()
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("strOficinaDesc") = Nothing
        Session("listaCajasOfi") = Nothing
        Response.Redirect("ListarCaja.aspx")
    End Sub

    Private Sub eliminarHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles eliminarHandler.Click
        Try
            Dim strMsjErr As String = String.Empty
            Dim iIdCajaOficina As Int32 = CInt(Request.Item("hidIDCajaOfi"))
            strMsjErr = objclsAdmCaja.EliminarCajaOficina(iIdCajaOficina)

            If Not strMsjErr.Equals(String.Empty) Then
                Response.Write("<script>alert('" & strMsjErr & "');</script>")
                Exit Sub
            Else
                Response.Write("<script language=jscript> alert('Eliminado correctamente.'); </script>")
                Response.Write("<script language=jscript>f_Refrescar();</script>")
            End If
            Buscar()
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub refreshHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles refreshHandler.Click
        Buscar()
    End Sub
#End Region

#Region "Metodos"

    Private Sub Buscar()
        Try
            Dim strOficina As String = Session("strOficinaDesc")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Buscar")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI GetCajaOficina")
            Dim dsResult As DataSet = objclsAdmCaja.GetCajaOficina(strOficina, optTODOS, 0)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN GetCajaOficina")

            Session("listaCajasOfi") = dsResult.Tables(0)
            Me.DGListaOfi.DataSource = dsResult.Tables(0)
            Me.DGListaOfi.DataBind()
            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR: Mensaje - " & ex.Message)
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Buscar")
        End Try
    End Sub
#End Region

End Class
