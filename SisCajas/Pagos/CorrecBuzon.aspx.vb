Public Class CorrecBuzon
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgSobres As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnEliminar As System.Web.UI.WebControls.Button
    Protected WithEvents hddEnvio As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " ----- Declaraciones ----- "

    Private ReadOnly Property CurrentUser() As String
        Get
            Dim domainUser As String = Request.ServerVariables("LOGON_USER")
            Dim usuarioLogin As String = domainUser.Substring(domainUser.IndexOf("\") + 1)
            If usuarioLogin Is Nothing Then usuarioLogin = ""
            Return usuarioLogin.Trim().ToUpper()
        End Get
    End Property

    Private ReadOnly Property CurrentTerminal() As String
        Get
            Return Request.ServerVariables("REMOTE_HOST")
        End Get
    End Property

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogEliminaBuzon")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogEliminaBuzon")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

#End Region

#Region " ----- Eventos ----- "

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
            Dim dsSobres As DataSet
            Dim objCajas As New COM_SIC_Cajas.clsCajas

            If Not Page.IsPostBack Then
                dsSobres = objCajas.FP_BolsasLibres(Session("ALMACEN"), "")
                If Not IsNothing(dsSobres) Then
                    dgSobres.DataSource = dsSobres.Tables(0)
                End If

                Me.DataBind()
            Else
                If Me.hddEnvio.Value = "OK" Then
                    Call Me.Eliminar()
                End If
        End If
        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio btnEliminar_Click ")
        If Not IniciarProceso() Then
            Return
        End If

        If Not VerificarCierreCaja() Then
            Return
        End If

        Session("AutValidacion") = "C01"
        Response.Write("<script language=javascript>window.open('frmValidarUsuario.aspx','SICAR','width=400px,height=150px,resizable=no,directories=no,menubar=no,status=no,toolbar=no,left=300,top=300');</script>")

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio btnEliminar_Click ")
    End Sub

    Private Sub dgSobres_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSobres.ItemDataBound
        Dim chkBox As CheckBox
        chkBox = e.Item.FindControl("chkSobre")
        If Not IsNothing(chkBox) Then
            chkBox.Attributes.Add("onClick", "f_Suma();")
        End If
    End Sub

#End Region

#Region " ----- Metodos ----- "

    Private Sub Eliminar()
   
        Dim i As Integer
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim chkSelect As CheckBox

        'Variables de Auditoria
        Dim wParam1 As Long
        Dim wParam2 As String
        Dim wParam3 As String
        Dim wParam4 As Long
        Dim wParam5 As Integer
        Dim wParam6 As String
        Dim wParam7 As Long
        Dim wParam8 As Long
        Dim wParam9 As Long
        Dim wParam10 As String
        Dim wParam11 As Integer
        Dim Detalle(6, 3) As String
        Dim retorno As Boolean = False
        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA

        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcAcCB")
        wParam5 = 1
        wParam6 = "Eliminacion de Cajas Buzon"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtAcCB")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        'FIN DE AUDITORIA

        For i = 0 To dgSobres.Items.Count - 1
            chkSelect = dgSobres.Items(i).FindControl("chkSobre")
            If chkSelect.Checked Then

                'AUDITORIA
                Detalle(1, 1) = "OfVta"
                Detalle(1, 2) = Session("ALMACEN")
                Detalle(1, 3) = "Oficina de Venta"

                Detalle(2, 1) = "Sobre"
                Detalle(2, 2) = dgSobres.Items(i).Cells(1).Text
                Detalle(2, 3) = "Sobre"

                Detalle(3, 1) = "Usuario"
                Detalle(3, 2) = Session("USUARIO")
                Detalle(3, 3) = "Usuario"

                Detalle(4, 1) = "NomUs"
                Detalle(4, 2) = Session("NOMBRE_COMPLETO")
                Detalle(4, 3) = "Nombre Usuario"

                Detalle(5, 1) = "UsuAutor"
                Detalle(5, 2) = Session("UsuAutorizador")
                Detalle(5, 3) = "Usuario Autorizador"

                Detalle(6, 1) = "TipoOpe"
                Detalle(6, 2) = "Eliminación de sobre"
                Detalle(6, 3) = "Tipo de Operación"

                'FIN DE AUDITORIA

                objCajas.FP_EliminaBuzon(Session("ALMACEN"), dgSobres.Items(i).Cells(1).Text, Session("USUARIO"), Session("NOMBRE_COMPLETO"))
                RegistrarAuditoria(String.Concat("Se elimino el sobre de caja buzon : ", Session("ALMACEN"), ";", dgSobres.Items(i).Cells(1).Text, ";", Session("USUARIO"), ";", Session("NOMBRE_COMPLETO"), ";", Session("UsuAutorizador"), ";", "Eliminación de sobre"))
                'objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            End If
        Next

        Response.Write("<script language=javascript>alert('Se elimino correctamente');</script>")
        Response.Redirect("CorrecBuzon.aspx")

    End Sub

    Private Sub RegistrarAuditoria(ByVal StrLogAuditoria As String)
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

            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsEliminarSobreCajaBuzon")
            Dim descTrans As String = StrLogAuditoria

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub

#End Region

#Region " ----- Funciones ----- "

    Private Function IniciarProceso() As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio IniciarProceso() ")
        IniciarProceso = False
        Dim chkSelect As CheckBox
        Dim UsuarioSobre As String
        Dim UsuarioSistema As String
        UsuarioSistema = Trim(Session("USUARIO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "UsuarioSistema : " & UsuarioSistema)
        Try
            If dgSobres.Items.Count > 0 Then
                For i As Int32 = 0 To dgSobres.Items.Count - 1
                    chkSelect = dgSobres.Items(i).FindControl("chkSobre")
                    If chkSelect.Checked Then
                        UsuarioSobre = Trim(dgSobres.Items(i).Cells(5).Text())
                        If UsuarioSistema = UsuarioSobre Then
                            IniciarProceso = True
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : No es posible eliminar sobre(s) de otros usuarios")
                            Response.Write(String.Concat("<script language=javascript>alert('No es posible eliminar sobre(s) de otros usuarios');</script>"))
                            IniciarProceso = False
                            GoTo Salir
                        End If
                    End If
                Next
            End If
            If Not IniciarProceso Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Debe tener seleccionado al menos un registro")
                Response.Write("<script language=javascript>alert('Debe tener seleccionado al menos un registro');</script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : " & ex.Message.ToString)
            Response.Write(String.Concat("<script language=javascript>alert('" + ex.Message.ToString + "');</script>"))
            IniciarProceso = False
        End Try
Salir:
        Return IniciarProceso
    End Function

    Private Function VerificarCierreCaja() As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio VerificarCierreCaja()")
        VerificarCierreCaja = True
        'INICIO - MODIFICADO POR TS_CCC

        'Dim objPagos As SAP_SIC_Pagos.clsValidar
        'Dim cajaBuzon, saldoInicial As Decimal
        'Dim cierreRealizado, Resultado, msgError As String

        'objPagos = New SAP_SIC_Pagos.clsValidar
        'objPagos.Get_ConsultaEstadoCaja(Session("ALMACEN"), Now.ToShortDateString, Session("USUARIO"), Resultado, msgError, cajaBuzon, cierreRealizado, saldoInicial)
        'objPagos = Nothing

        'If Resultado <> "0" Then
        '    Response.Write(String.Concat("<script language=javascript>alert('Error: ", msgError, "');</script>"))
        'Else
        '    If cierreRealizado <> "N" Then
        '        Response.Write("<script language=javascript>alert('No es posible anular un sobre de una caja ya cerrada');</script>")
        '    Else
        '        VerificarCierreCaja = True
        '    End If
        'End If

        Dim dsResultado As DataSet
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim strCierreCajaMensaje As String = String.Empty
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, " Ini VerificarAsignacionCajero()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " codUsuario:" & codUsuario.ToString())
        objFileLog.Log_WriteLog(pathFile, strArchivo, " PDV :" & Session("ALMACEN"))
        Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, Date.Today.ToString("dd/MM/yyyy"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, " OUT asignacionCajeroMensaje :" & asignacionCajeroMensaje)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin VerificarAsignacionCajero()")
        If asignacionCajeroMensaje <> String.Empty Then
            Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
            Me.RegisterStartupScript("RegistraAlerta3", script)
            VerificarCierreCaja = False
            Exit Function
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, " Ini GetDatosAsignacionCajero()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " IN Pdv :" & Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, " IN Fecha :" & Date.Today.ToString("dd/MM/yyyy"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, " IN usuario :" & codUsuario)
        dsResultado = objOffline.GetDatosAsignacionCajero(Session("ALMACEN"), Date.Today.ToString("dd/MM/yyyy"), codUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin GetDatosAsignacionCajero()")
        If Not dsResultado Is Nothing Then
            For i As Int32 = 0 To dsResultado.Tables(0).Rows.Count - 1
                If dsResultado.Tables(0).Rows(i).Item("CAJA_CERRADA") = "S" Then
                    strCierreCajaMensaje = "No es posible anular un sobre de una caja ya cerrada"
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Mensaje: " & strCierreCajaMensaje)
                    Me.RegisterStartupScript("RegistraAlertaCC", script)
                    VerificarCierreCaja = False
                    Exit For
                End If
            Next
        End If

        'FIN - MODIFICADO POR TS_CCC
        Return VerificarCierreCaja

    End Function

#End Region




End Class
