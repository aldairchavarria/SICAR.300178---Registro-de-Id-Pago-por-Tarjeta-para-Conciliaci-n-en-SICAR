Imports COM_SIC_Seguridad 'PROY-140846

Public Class Terminar
    Inherits SICAR_WebBase 'System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()


    End Sub
    Protected WithEvents cmdAnular As System.Web.UI.WebControls.Button
    Protected WithEvents txtDocSap As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDG As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTipDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRbPagos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtEfectivo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRecibido As System.Web.UI.WebControls.TextBox
    'Dolares
    Protected WithEvents txtRecibidoUS As System.Web.UI.WebControls.TextBox
    'Origen
    Protected WithEvents txtOrigen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEntregar As System.Web.UI.WebControls.TextBox
    Protected WithEvents strCodPago As System.Web.UI.WebControls.TextBox
    Protected WithEvents strFechaExpira As System.Web.UI.WebControls.TextBox
    Protected WithEvents strNroRecarga As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOffline As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtpImp As System.Web.UI.WebControls.TextBox 'PROY-23700-IDEA-29415

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
    Dim objLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPoolPagos")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objLog.Log_CrearNombreArchivo(nameFile)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else

            '<33062>
            If Not Session("msgErrorInsertaMail") Is Nothing And Session("msgErrorInsertaMail") <> "" Then
                Response.Write("<script>alert('" & Session("msgErrorInsertaMail") & "')</script>")
                Session.Remove("msgErrorInsertaMail")
            End If
            '</33062>
            ' E75893 - Mensaje de Error Registro DOL
            If Not Session("mensajeErrorDOL") Is Nothing And Session("mensajeErrorDOL") <> "" Then
                Response.Write("<script>alert('" & Session("mensajeErrorDOL") & "')</script>")
                Session.Remove("mensajeErrorDOL")
            End If
            If Not Session("ErrorRenovacionRPM6") Is Nothing And Session("ErrorRenovacionRPM6") <> "" Then
                Response.Write("<script>alert('" & Session("ErrorRenovacionRPM6") & "')</script>")
                Session.Remove("ErrorRenovacionRPM6")
            End If
            If Not Session("mensajeErrorLineas") Is Nothing And Session("mensajeErrorLineas") <> "" Then
                Response.Write("<script>alert('" & Session("mensajeErrorLineas") & "')</script>")
                Session.Remove("mensajeErrorLineas")
            End If

            If Not Session("strMensajeWSCABB") Is Nothing And Session("strMensajeWSCABB") <> "" Then
                Response.Write("<script>alert('" & Session("strMensajeWSCABB") & "')</script>")
                Session.Remove("strMensajeWSCABB")
            End If

            If Len(Trim(Session("strMensajeCaja"))) > 0 Then
                Response.Write("<script>alert('" & Session("strMensajeCaja") & "')</script>")
                Session("strMensajeCaja") = ""
            End If

            If Len(Trim(Session("strMensajeCajaRec"))) > 0 Then
                Response.Write("<script>alert('" & Session("strMensajeCajaRec") & "')</script>")
                Session("strMensajeCajaRec") = ""
            End If
            'PROY-23700-IDEA-29415 - INI
            If Not Request.QueryString("pImp") Is Nothing Then
                txtpImp.Text = Request.QueryString("pImp")
            End If
            'PROY-23700-IDEA-29415 - FIN

            'PROY-24388 INICIO
            If Not Session("msgActivacionCAC") Is Nothing And Session("msgActivacionCAC") <> "" Then
                Response.Write("<script>alert('" & Session("msgActivacionCAC") & "');</script>")
                Session.Remove("msgActivacionCAC")
            End If
            'PROY-24388 FIN

            '//PROY-140379 INI AW
            If Not Session("MensajeErrorPagoCanjeAW") Is Nothing And Session("MensajeErrorPagoCanjeAW") <> "" Then
                Response.Write("<script>alert('" & Session("MensajeErrorPagoCanjeAW") & "');</script>")
                Session.Remove("MensajeErrorPagoCanjeAW")
            End If
            '//PROY-140379 FIN AW
            '*******agregar por impresion
            If Not Request.QueryString("pImp") Is Nothing AndAlso Request.QueryString("pImp") = "S" Then
                If Not Request.QueryString("pDocSap") Is Nothing Then
                    txtDocSap.Text = Request.QueryString("pDocSap")
                Else
                    txtDocSap.Text = ""
                End If
                If Not Request.QueryString("pDocSunat") Is Nothing Then
                    txtDocSunat.Text = Request.QueryString("pDocSunat")
                End If
                If Not Request.QueryString("pNroDG") Is Nothing Then
                    txtNroDG.Text = Request.QueryString("pNroDG")
                End If
                If Not Request.QueryString("pTipDoc") Is Nothing Then
                    txtTipDoc.Text = Request.QueryString("pTipDoc")
                End If

                If Not Request.QueryString("strEfectivo") Is Nothing Then
                    txtEfectivo.Text = Request.QueryString("strEfectivo")
                End If
                If Not Request.QueryString("strRecibido") Is Nothing Then
                    txtRecibido.Text = Request.QueryString("strRecibido")
                End If
                'Dolares
                If Not Request.QueryString("strRecibidoUS") Is Nothing Then
                    txtRecibidoUS.Text = Request.QueryString("strRecibidoUS")
                End If
                If Not Request.QueryString("strEntregar") Is Nothing Then
                    txtEntregar.Text = Request.QueryString("strEntregar")
                End If

                ' Parametros Recarga Virtual DTH
                If Not Request.QueryString("strCodPago") Is Nothing Then
                    strCodPago.Text = Request.QueryString("strCodPago")
                End If
                If Not Request.QueryString("strFechaExpira") Is Nothing Then
                    strFechaExpira.Text = Request.QueryString("strFechaExpira")
                End If
                If Not Request.QueryString("strNroRecarga") Is Nothing Then
                    strNroRecarga.Text = Request.QueryString("strNroRecarga")
                End If
                'Parametro para ORIGEN
                If Not Request.QueryString("strOrigen") Is Nothing Then
                    txtOrigen.Text = Request.QueryString("strOrigen")
                End If


                ''INICIO JTN
                If Not Request.QueryString("isOffline") Is Nothing Then
                    txtOffline.Text = Request.QueryString("isOffline")
                End If
                ''FIN JTN
                'PROY-23700-IDEA-29415 - INI
                'INICIO - PROY-140846  IDEA 143176 - REGULATORIO - GPRD
                objFileLog.Log_WriteLog(pathFile, strArchivo, "LEN Session(mensajeCHIPRepuesto): " & "- " & Funciones.CheckStr(Len(Trim(Session("mensajeCHIPRepuesto")))))
                If Len(Trim(Session("mensajeCHIPRepuesto"))) > 0 Then
                    Response.Write("<script>alert('" & Session("mensajeCHIPRepuesto") & "')</script>")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Session(mensajeCHIPRepuesto): " & "- " & Funciones.CheckStr(Session("mensajeCHIPRepuesto")))
                    Session("mensajeCHIPRepuesto") = ""
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "sValSessionRepoProgramada: " & "- " & Funciones.CheckStr(Len(Trim(Session("ShowMsgRepoProgramada")))))
                If Not Session("ShowMsgRepoProgramada") Is Nothing Then
                    Dim sValSessionRepoProgramada As String = String.Empty
                    sValSessionRepoProgramada = Funciones.CheckStr(Session("ShowMsgRepoProgramada"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "sValSessionRepoProgramada: " & "- " & Funciones.CheckStr(sValSessionRepoProgramada))
                    If sValSessionRepoProgramada = "0" Then
                        Dim sMensaje As String = String.Empty
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "ReadKeySettings.Key_MensajeReposicionProgramada: " & "- " & Funciones.CheckStr(ReadKeySettings.Key_MensajeReposicionProgramada))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "ReadKeySettings.Key_TiempoProgramacionReposicion: " & "- " & Funciones.CheckStr(ReadKeySettings.Key_TiempoProgramacionReposicion))
                        sMensaje = String.Format(Funciones.CheckStr(ReadKeySettings.Key_MensajeReposicionProgramada), Funciones.CheckStr(ReadKeySettings.Key_TiempoProgramacionReposicion))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "sMensaje: " & "- " & Funciones.CheckStr(sMensaje))
                        Response.Write("<script>alert('" & sMensaje & "');</script>")
                    End If
                End If
                'FIN PROY-140846 IDEA 143176 - REGULATORIO - GPRD
            ElseIf Request.QueryString("pImp") = "NCJ" Then
                If Not Request.QueryString("pDocSap") Is Nothing Then
                    txtDocSap.Text = Request.QueryString("pDocSap")
                Else
                    txtDocSap.Text = ""
                End If
                If Not Request.QueryString("pDocSunat") Is Nothing Then
                    txtDocSunat.Text = Request.QueryString("pDocSunat")
                End If
                If Not Request.QueryString("pNroDG") Is Nothing Then
                    txtNroDG.Text = Request.QueryString("pNroDG")
                End If
                If Not Request.QueryString("pTipDoc") Is Nothing Then
                    txtTipDoc.Text = Request.QueryString("pTipDoc")
                End If

                If Not Request.QueryString("strEfectivo") Is Nothing Then
                    txtEfectivo.Text = Request.QueryString("strEfectivo")
                End If
                If Not Request.QueryString("strRecibido") Is Nothing Then
                    txtRecibido.Text = Request.QueryString("strRecibido")
                End If
                'Dolares
                If Not Request.QueryString("strRecibidoUS") Is Nothing Then
                    txtRecibidoUS.Text = Request.QueryString("strRecibidoUS")
                End If
                If Not Request.QueryString("strEntregar") Is Nothing Then
                    txtEntregar.Text = Request.QueryString("strEntregar")
                End If

                ' Parametros Recarga Virtual DTH
                If Not Request.QueryString("strCodPago") Is Nothing Then
                    strCodPago.Text = Request.QueryString("strCodPago")
                End If
                If Not Request.QueryString("strFechaExpira") Is Nothing Then
                    strFechaExpira.Text = Request.QueryString("strFechaExpira")
                End If
                If Not Request.QueryString("strNroRecarga") Is Nothing Then
                    strNroRecarga.Text = Request.QueryString("strNroRecarga")
                End If
                'Parametro para ORIGEN
                If Not Request.QueryString("strOrigen") Is Nothing Then
                    txtOrigen.Text = Request.QueryString("strOrigen")
                End If
                
                If Not Request.QueryString("isOffline") Is Nothing Then
                    txtOffline.Text = Request.QueryString("isOffline")
                End If

                'PROY-23700-IDEA-29415 - FIN
            End If
            '*******fin agregar por impresion
        End If
    End Sub

End Class
