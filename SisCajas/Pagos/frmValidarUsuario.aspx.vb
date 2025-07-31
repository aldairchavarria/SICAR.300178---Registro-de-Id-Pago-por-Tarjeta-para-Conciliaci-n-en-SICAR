Imports ADODB
Imports System
Imports System.Data.OleDb
Imports System.DirectoryServices
'Import 09-oct-2015 LMR
Imports COM_SIC_Activaciones

Public Class frmValidarUsuario
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtValUsuario As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents lblID As System.Web.UI.WebControls.Label
    Protected WithEvents btnValidar As System.Web.UI.WebControls.Button
    Protected WithEvents hddAction As System.Web.UI.HtmlControls.HtmlInputHidden
    'INICIATIVA-318 INI
    Protected WithEvents hddActionAutorizador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents hddCombo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hddActionFpago As System.Web.UI.HtmlControls.HtmlInputHidden
    'INICIATIVA-318 FIN

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

#Region " ----- Declaraciones ----- "
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogEliminaBuzon")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogEliminaBuzon")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'INICIATIVA-318 INI
    Dim VFlujo As String
    Dim VFormaPAgo As String
    Dim VCombo As String
    Dim arrParametrosFormaPagoPerfil As ArrayList
    'INICIATIVA-318 FIN

#End Region

#Region " ----- Funciones ----- "

    Private Function IsAuthenticated(ByVal vUsuario As String, ByVal vClave As String) As Boolean
        Dim strDominio As String = ConfigurationSettings.AppSettings("DominioLDAP")
        Dim entry As New DirectoryEntry(strDominio, vUsuario, vClave)
        Try
            Dim obj As Object = entry.NativeObject()
            Dim search As New DirectorySearcher(entry)
            search.Filter = "(SAMAccountName=" + vUsuario + ")"
            search.PropertiesToLoad.Add("cn")
            Dim resul As SearchResult
            resul = search.FindOne()
            If (resul Is Nothing) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region " ----- Eventos ----- "

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Try
            'INICIATIVA-318 INI
            VFormaPAgo = Trim(Request.QueryString("v_formapago"))
            VFlujo = Trim(Request.QueryString("v_flujo"))
            VCombo = Trim(Request.QueryString("v_combo"))
            hddActionAutorizador.Value = VFlujo
            hddCombo.Value = VCombo
            'INICIATIVA-318 FIN

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Page_Load()")
            If Not IsPostBack Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Not IsPostBack")
                Call Me.Limpiar()
                Call Me.Inicializar()
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Not IsPostBack")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini IsPostBack")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Action : " & hddAction.Value)
                If hddAction.Value = "OK" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Call Me.Aceptar")
                    Call Me.Aceptar()
                    Me.hddAction.Value = String.Empty
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin IsPostBack")
            End If
        Catch ex As Exception
            Response.Write(String.Concat("<script language=javascript>alert('" + ex.Message.ToString + "');</script>"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : " & ex.Message.ToString)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Page_Load()")
        End Try
    End Sub

#End Region

#Region " ----- Metodos ----- "

    Private Sub Limpiar()

        Me.txtValUsuario.Text = String.Empty
        Me.txtValPassword.Text = String.Empty
        Me.lblID.Text = String.Empty
        Me.hddAction.Value = String.Empty

    End Sub

    Private Sub Inicializar()
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Inicializar ")
        Me.lblID.Text = Session("AutValidacion")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " ID : " & Me.lblID.Text)
        Session("AutValidacion") = Nothing
        If Me.lblID.Text = "C01" Then
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Inicializar)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Mensaje : Autorizar Transacción - Eliminación de Caja Buzón" & MaptPath)
            'FIN PROY-140126

            Me.lblTitulo.Text = "Autorizar Transacción - Eliminación de Caja Buzón"
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Inicializar ")
    End Sub

    Private Sub Aceptar()
        Dim pUsuario As String = Trim(Me.txtValUsuario.Text)
        Dim pPassword As String = Trim(Me.txtValPassword.Text)
        Dim rsVerificaUsuario As ADODB.Recordset
        Dim objAccesoAplicativo As Object
        Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")
        Dim codOpcionPagina As String = ConfigurationSettings.AppSettings("constOpcPagValAnulacionCajaBuzon")
        Dim daRecord As New OleDbDataAdapter
        Dim dsRecord As New DataSet
        Dim pPerfil As String
        Dim rBol As Boolean = False
        Dim LOpciones As New ArrayList

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Aceptar() ")
            If Me.lblID.Text = "C01" Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini condiciones para supervisor y/o jefe ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ID : " & Me.lblID.Text)
                REM ---- condiciones para supervisor y/o jefe ----
                If Not IsAuthenticated(pUsuario, pPassword) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario y/o contraseña incorrecto")
                    Response.Write("<script language='javascript'>alert('Usuario y/o contraseña incorrecto');</script>")
                    Return
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin condiciones para supervisor y/o jefe ")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Obtener perfil del Usuario ")

                'LMR 09-oct-2015 Obtener perfil del Usuario 
                Dim RptUser As Boolean
                Dim CodUser As String
                LeerUser(pUsuario, RptUser, CodUser)
                If (RptUser = True) Then
                    pPerfil = CodUser
                Else
                    pPerfil = ""
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, " OUT Perfil : " & pPerfil)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Obtener perfil del Usuario ")

                Dim conPag As New COM_SIC_Activaciones.clsAuditoriaWS
                Dim OpcionesPag As New ArrayList
                Dim vResultado As Boolean = False

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini ConsultarOpcionesPagina ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "In perfil : " & pPerfil)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "In codAplicacion : " & codAplicacion)

                OpcionesPag = conPag.ConsultarOpcionesPagina(pPerfil, codAplicacion, vResultado)

                objFileLog.Log_WriteLog(pathFile, strArchivo, "OUT vResultado : " & vResultado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin ConsultarOpcionesPagina ")

                If vResultado Then
                    Session("WS_OpcionesPagina_Aut") = OpcionesPag
                Else
                    Session("WS_OpcionesPagina_Aut") = Nothing
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Obtener perfil del Usuario ")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Validar por opciones de pagina ")
                REM ---- Validar por opciones de pagina
                If Session("WS_OpcionesPagina_Aut") Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : No se cargaron las opciones de pagina")
                    Response.Write("<script language='javascript'>alert('No se cargaron las opciones de pagina');</script>")
                    Return
                Else
                    LOpciones = Session("WS_OpcionesPagina_Aut")
                End If

                For Each Rpt As String In LOpciones
                    If String.Compare(Rpt, codOpcionPagina) = 0 Then
                        Session("UsuAutorizador") = pUsuario
                        rBol = True
                        Exit For
                    End If
                Next

                ''pPerfil = "0003"

                'For Each uRow As DataRow In dsRecord.Tables(0).Rows
                '    If Not uRow("PERFC_COD") Is Nothing Then
                '        pPerfil = Trim(uRow("PERFC_COD"))
                '        While pPerfil.Length < 4
                '            pPerfil = "0" + pPerfil
                '        End While
                '        If CType(pPerfil, String).IndexOf(ConfigurationSettings.AppSettings("cod_perfil_supervisor")) <> -1 Then
                '            Session("UsuAutorizador") = pUsuario
                '            rBol = True
                '        End If
                '    End If
                'Next

                If Not rBol Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario ingresado no existe o no tiene perfil autorizador")
                    Response.Write("<script language='javascript'>alert('Usuario ingresado no existe o no tiene perfil autorizador');</script>")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario ingresado autorizado")
                    Response.Write("<script language='javascript'>window.opener.frmPrincipal.hddEnvio.value = 'OK';</script>")
                    Response.Write("<script language='javascript'>window.opener.frmPrincipal.submit();</script>")
                    Response.Write("<script language='javascript'>window.close();</script>")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Validar por opciones de pagina ")

                'If Not CType(pPerfil, String).IndexOf(ConfigurationSettings.AppSettings("cod_perfil_supervisor")) <> -1 Then
                '    Response.Write("<script language='javascript'>alert('Usuario ingresado no existe o no tiene perfil autorizador');</script>")
                'Else
                '    Session("UsuAutorizador") = pUsuario
                '    Response.Write("<script language='javascript'>window.opener.frmPrincipal.hddEnvio.value = 'OK';</script>")
                '    Response.Write("<script language='javascript'>window.opener.frmPrincipal.submit();</script>")
                '    Response.Write("<script language='javascript'>window.close();</script>")
                'End If

            End If

            'INICIATIVA-318 INI
            If VFlujo = "FormaPago" Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini condiciones para supervisor y/o jefe ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ID : " & Me.lblID.Text)
                REM ---- condiciones para supervisor y/o jefe ----
                If Not IsAuthenticated(pUsuario, pPassword) Then
                    hddActionFpago.Value = "FormaPago"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario y/o contraseña incorrecto")
                    Response.Write("<script language='javascript'>alert('Usuario y/o contraseña incorrecto');</script>")
                    Return
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin condiciones para supervisor y/o jefe ")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Ini Obtener perfil del Usuario ")

                Dim bolAutorizado As Boolean
                Dim strFormaPago As String
                Dim strCombo As String

                strCombo = VCombo
                strFormaPago = VFormaPAgo
                bolAutorizado = ObtenerAprobacionFormaPago(strFormaPago)

                If (bolAutorizado = False) Then
                    hddActionFpago.Value = "FormaPago"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario ingresado no existe o no tiene perfil autorizador")
                    Response.Write("<script language='javascript'>alert('Usuario ingresado no existe o no tiene perfil autorizador');</script>")
                Else
                    hddActionFpago.Value = "FormaPago"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : Usuario ingresado autorizado")
                    Response.Write("<script language='javascript'>window.opener.frmPrincipal.hddEnvioAutorizador.value = 'OK';</script>")
                    Dim strQry = "<script language='javascript'>window.opener.frmPrincipal.hddComboAutorizador.value = " & Chr(39) & strCombo & Chr(39) & ";</script>"
                    Response.Write(strQry)
                    Response.Write("<script language='javascript'>alert('Usuario ingresado autorizado');</script>")
                    Response.Write("<script language='javascript'>window.opener.frmPrincipal.submit();</script>")
                    Response.Write("<script language='javascript'>window.close();</script>")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Validar por opciones de pagina ")



            End If
            'INICIATIVA-318 INI
        Catch ex As Exception
            Response.Write(String.Concat("<script language=javascript>alert('" + ex.Message.ToString + "');</script>"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Mensaje : " & ex.Message.ToString)
        Finally
            objAccesoAplicativo = Nothing
            daRecord.Dispose()
            dsRecord.Dispose()
            LOpciones = Nothing
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Aceptar() ")
        End Try
    End Sub

    'Funcion Lee usuario 09-oct-2015
    Private Sub LeerUser(ByVal user As String, ByRef rpt As Boolean, ByRef CodUser As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " Ini  - LeerUser() ")
        Try
            rpt = True
            Dim objAccesoAplicativo As Object
            Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")

            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
            Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

            objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Ini  - ST leerDatosUsuario() ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " IN usuario : " & user)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " IN aplicacion : " & codAplicacion)
            oAccesoRequest.usuario = user
            oAccesoRequest.aplicacion = codAplicacion
            oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " OUT estado : " & oAccesoResponse.resultado.estado.ToString())
            If oAccesoResponse.resultado.estado = "1" Then
                CodUser = oAccesoResponse.auditoria.AuditoriaItem.item(0).codigo
                objFileLog.Log_WriteLog(pathFile, strArchivo, " OUT CodUser : " & oAccesoResponse.auditoria.AuditoriaItem.item(0).codigo.ToString())
            Else
                rpt = False
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LeerUser)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin  - LeerUser - Mensaje : " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126           
            rpt = False
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin  - LeerUser() ")
        End Try
    End Sub
    'INICIATIVA-318 INI
    Private Function ConsultaParametrosFormaPagoPerfil(ByVal strIdenLog As String) As String
        Dim strCodPerfilFormaPago As String = ""
        Dim pUsuario As String = Trim(Me.txtValUsuario.Text)
        Try

            Dim objpvuDB As New COM_SIC_Activaciones.clsConsultaPvu
            Dim oParamteros As New COM_SIC_Activaciones.BEParametros
            Dim strCodGrupo As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("key_ParanGrupoFormaPagoPerfil"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdenLog & "- " & "--Inicio Proceso Consulta Parametros--")
            If strCodGrupo <> "" Then
                arrParametrosFormaPagoPerfil = objpvuDB.ConsultaParametros(strCodGrupo)
            End If

            Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")
            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
            Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

            objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            oAccesoRequest.usuario = pUsuario
            oAccesoRequest.aplicacion = codAplicacion

            oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

            If oAccesoResponse.resultado.estado = "1" Then

                If oAccesoResponse.auditoria.AuditoriaItem.item.Length > 0 Then
                    Dim item As New COM_SIC_Seguridad.EntidadConsulSeguridad
                    For i As Integer = 0 To oAccesoResponse.auditoria.AuditoriaItem.item.Length - 1
                        strCodPerfilFormaPago = strCodPerfilFormaPago & oAccesoResponse.auditoria.AuditoriaItem.item(i).perfil & ","
                    Next
                    strCodPerfilFormaPago = strCodPerfilFormaPago.Substring(0, strCodPerfilFormaPago.Length - 1)
                End If

            End If

        Catch ex As Exception

        End Try

        Return strCodPerfilFormaPago

    End Function

    Public Function ObtenerAprobacionFormaPago(ByVal strFormaPago As String) As Boolean

        Dim strValor As String
        Dim strValor1 As String
        Dim strPerfilusuario As String = ""
        Dim strConfirma As String
        Dim sArrayPerfiles As String()
        Dim bolAutorizado As Boolean
        Dim strCodPerfilFormaPago As String

        Try
            strCodPerfilFormaPago = ConsultaParametrosFormaPagoPerfil("")

            If strCodPerfilFormaPago.Length > 0 Then
                sArrayPerfiles = strCodPerfilFormaPago.Split(","c)
            End If

            For Each item As BEParametros In arrParametrosFormaPagoPerfil
                strValor = item.strValor.Split("|")(0)
                strValor1 = item.strValor1.Split("|")(0)
                Select Case strValor
                    Case strFormaPago
                        For Each sPerfil As String In sArrayPerfiles
                            If strValor1 = sPerfil Then
                                bolAutorizado = True
                                Exit For
                            Else
                                bolAutorizado = False
                            End If
                        Next

                        If (bolAutorizado) Then
                            Exit For
                        End If
                End Select
            Next
        Catch ex As Exception
        End Try
        Return bolAutorizado
    End Function
    'INICIATIVA-318 FIN
#End Region

End Class
