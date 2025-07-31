Public Class configPaycall_Lista
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents eliminarHandler As System.Web.UI.WebControls.Button
    Protected WithEvents refreshHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidIDCtaRemesa As System.Web.UI.HtmlControls.HtmlInputHidden
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
    Dim objclsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Dim strPaginaRetorno As String
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogConfigPaycall")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogConfigPaycall")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Protected MensajeAudi As String
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
                strUsuario = Session("USUARIO")
                Buscar()
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("strCPOficinaID") = Nothing
        Response.Redirect("configPaycall.aspx")
    End Sub

    Private Sub refreshHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles refreshHandler.Click
        Buscar()
    End Sub

    Private Sub eliminarHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles eliminarHandler.Click
        Dim strMsjErr As String = String.Empty
        Dim iIdCtaRemesa As Int32 = CInt(Request.Item("hidIDCtaRemesa"))
        Try
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            strMsjErr = objclsAdmCaja.EliminarCtaRemesa(iIdCtaRemesa)

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
        Finally
            objclsAdmCaja = Nothing
            MensajeAudi = "Configuracion de Paycall. Eliminar: " & strUsuario & "|" & iIdCtaRemesa
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("CodAuditEliConfigPaycall"))
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub Buscar()
        Try
            strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
            Dim strCodOficina As String = Session("strCPOficinaID")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI Buscar")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INI GetCtaRemesa")
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsResult As DataSet = objclsAdmCaja.GetCtaRemesa(strCodOficina, 0)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   FIN GetCtaRemesa")

            Me.DGLista.DataSource = dsResult.Tables(0)
            Me.DGLista.DataBind()
            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR: Mensaje - " & ex.Message)
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objclsAdmCaja = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Buscar")
        End Try
    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Dim oAuditoria As COM_SIC_Activaciones.clsAuditoriaWS
        Try
            Dim user As String = CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            oAuditoria = New COM_SIC_Activaciones.clsAuditoriaWS

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio RegistrarAuditoria")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Nombre Host     : " & nameHost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Nombre Server   : " & nameServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ipServer        : " & ipServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " HostInfo        : " & hostInfo.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " Usuario_id      : " & user)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ipCliente       : " & ipHost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " codServicio     : " & CodServicio)

            Dim auditoriaGrabado As Boolean = oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

            If Not auditoriaGrabado Then
                Throw New Exception("Error. No se registro en Auditoria la configuracion de Paycall.")
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            oAuditoria = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin RegistrarAuditoria")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub

#End Region

End Class
