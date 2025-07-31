Imports COM_SIC_Seguridad
Imports COM_SIC_Activaciones
Imports System.DirectoryServices

Public Class SICAR_validar
    Inherits System.Web.UI.Page

    Public K_ACEPTAR = 1
    Public K_CANCELAR = 2

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents lblMensaje As System.Web.UI.WebControls.Label
    Protected WithEvents txtUsuario As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents btnValidar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnCancelar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hidPerfilesAValidar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidOpcion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidnValueAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidVeces As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidVerificar As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Dim strCodigoUsuArq As String = "0"
    Dim strNombreUsuArq As String = "N"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim intAccion

            If Not Page.IsPostBack Then
                hidOpcion.Value = Trim(Request.QueryString("v_opcion"))
                hidPerfilesAValidar.Value = ConfigurationSettings.AppSettings("constPerfil_AutorizacionAdmSup")
                txtPass.Attributes.Add("onkeypress", "return FC_Enter(event)")

                Dim strPerfil = ""
                Dim sArrayPerfiles As String()
                strPerfil = ConsultaPerfil(Session("strUsuario"))

                If strPerfil.Length > 0 Then
                    sArrayPerfiles = strPerfil.Split(","c)
                End If

                'validar si el perfil es valido para direccionar directamente
                If hidOpcion.Value = 1 Then
                    Dim strPerfArray As String() = Trim(hidPerfilesAValidar.Value).Split(","c)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio - Validacion Perfil Sesion")
                    For Each sPerOri As String In strPerfArray
                        For Each sPerfil As String In sArrayPerfiles
                            If sPerOri = sPerfil Then
                            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                            Response.Redirect(strRutaSite & "/Recaudacion/SICAR_Arqueo_Caja.aspx?pUsuArqCod=" + Session("USUARIO") + "&pUsuArqNom=" + Trim(Session("NOMBRE_COMPLETO")), False)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Response.Redirect: " & "SICAR_Arqueo_Caja.aspx")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Cod. Perfil Autorizado: " & sPerOri)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Cod. Perfil Sesion: " & Session("codPerfil"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin Exit For - Validacion Perfil Sesion")
                            Exit For
                        End If
                    Next

                    Next
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin - Validacion Perfil Sesion")
                End If

                If Trim(Request.QueryString("v_opcion")) = "1" Then
                    lblTitulo.Text = "AUTORIZACIÓN ARQUEO"
                    btnCancelar.Disabled = True
                Else
                    lblTitulo.Text = "AUTORIZACIÓN"
                End If
            Else
                intAccion = CInt(Request("hidAccion"))
                If intAccion = K_ACEPTAR Then
                    Validar()
                End If
            End If
        End If
    End Sub

        Public Function Validar() As String
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Inicio Validar() ***")
            Dim blnCorrecto As Boolean = False
            Dim sUsuario As String = Trim(txtUsuario.Text)
            Dim sContrasena As String = Trim(txtPass.Text)
            Dim sOpcion As String = Trim(hidOpcion.Value)
            Dim resultado As Boolean = False
            Dim sCodPerfil As String = ""
            Dim objEmpleado As New clsUsuario

            objFileLog.Log_WriteLog(pathFile, strArchivo, " sOpcion: " & sOpcion)

            If sOpcion = "1" Then
                blnCorrecto = False

                objFileLog.Log_WriteLog(pathFile, strArchivo, " sOpcion: " & sOpcion & " ==> Autorizacion Arqueo de Caja")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Inicio ConsultarPerfiles() ***")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " sUsuario: " & sUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " sContrasena: " & sContrasena)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " hidPerfilesAValidar: " & hidPerfilesAValidar.Value)
                sCodPerfil = ConsultarPerfiles(sUsuario, sContrasena, hidPerfilesAValidar.Value)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " sCodPerfil: " & sCodPerfil)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin ConsultarPerfiles() ***")

                If sCodPerfil <> "-1" Then
                    blnCorrecto = True
                End If

                If blnCorrecto Then
                    Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                    hidnValueAccion.Value = "M"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " hidnValueAccion: " & hidnValueAccion.Value)

                    objEmpleado = GetDatosEmpleado(Trim(txtUsuario.Text))
                    If objEmpleado Is Nothing OrElse objEmpleado.CodigoVendedor = "" Then
                        objEmpleado = Nothing
                        objFileLog.Log_WriteLog(pathFile, strArchivo, " GetDatosEmpleado: " & "No se encontraron datos")
                    Else
                        strCodigoUsuArq = objEmpleado.CodigoVendedor
                        strNombreUsuArq = objEmpleado.NombreCompleto

                        objFileLog.Log_WriteLog(pathFile, strArchivo, " strCodigoUsuArq: " & strCodigoUsuArq)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, " strNombreUsuArq: " & strNombreUsuArq)
                    End If

                    Response.Redirect(strRutaSite & "/Recaudacion/SICAR_Arqueo_Caja.aspx?pUsuArqCod=" + strCodigoUsuArq + "&pUsuArqNom=" + strNombreUsuArq, False)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Response.Redirect: " & "SICAR_Arqueo_Caja.aspx")
                Else
                    Dim sMensaje As String = "La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo."
                    hidnValueAccion.Value = "E"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " hidnValueAccion: " & hidnValueAccion.Value)
                    Response.Write("<script> alert('" & sMensaje & "');</script>")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " alert: " & sMensaje)
                End If

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin Validar() ***")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin Validar() *** Exception: " & ex.Message & " ")
        End Try
        Return hidnValueAccion.Value
    End Function

    Private Function ConsultarPerfiles(ByVal pUsuario As String, ByVal pClave As String, ByVal pCadenaPerfiles As String) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, " pUsuario: " & pUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " pClave: " & pClave)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " pCadenaPerfiles: " & pCadenaPerfiles)

        Dim resultado As Boolean = IsAuthenticated(pUsuario, pClave)
        Dim strCodPerfil As String = ""
        Dim sArrayPerfiles As String()
        Dim strcodAplicacion As String = ConfigurationSettings.AppSettings("CodigoAplicacion")
        Dim sPerfilAutorizado As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, " IsAuthenticated - resultado: " & resultado.ToString)

        If resultado = True Then
            resultado = False

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio - ConsulSeguridad ")
            Dim cs As New COM_SIC_Seguridad.ConsulSeguridad
            Dim idTrans As String
            Dim errorMsg As String
            Dim CodError As String
            Dim codApp As Long = Integer.Parse(ConfigurationSettings.AppSettings("CodAplicacion"))
            Dim ipApp As String = ConfigurationSettings.AppSettings("strWebIpCod")
            Dim nomApp As String = ConfigurationSettings.AppSettings("ConsNombreAplicacion")
            Dim lista As ArrayList

            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio - verificaUsuario ")

            objFileLog.Log_WriteLog(pathFile, strArchivo, " idTrans: " & idTrans)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " ipApp: " & ipApp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " nomApp: " & nomApp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " pUsuario: " & pUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " codApp: " & codApp)

            lista = cs.verificaUsuario(idTrans, ipApp, nomApp, Trim(pUsuario), codApp, errorMsg, CodError)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " errorMsg => " & errorMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " CodError => " & CodError)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin - verificaUsuario ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " lista cantidad: " & lista.Count().ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin - ConsulSeguridad ")

            If lista.Count() > 0 Then
                Dim item As New COM_SIC_Seguridad.EntidadConsulSeguridad
                For i As Integer = 0 To lista.Count - 1
                    item = CType(lista.Item(i), COM_SIC_Seguridad.EntidadConsulSeguridad)
                    strCodPerfil = strCodPerfil & item.PERFCCOD & ","
                Next
            Else
                strCodPerfil = ""
            End If

            If strCodPerfil = "" Then
                sPerfilAutorizado = "-1"
                Return sPerfilAutorizado
            End If

            strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " strCodPerfil: " & strCodPerfil)

            If strCodPerfil.Length > 0 Then
                sArrayPerfiles = strCodPerfil.Split(","c)
                Dim strPerfArray As String() = Trim(pCadenaPerfiles).Split(","c)
                For Each sPerOri As String In strPerfArray
                    For Each sPerfil As String In sArrayPerfiles
                        If sPerOri = sPerfil Then
                            sPerfilAutorizado = sPerOri
                            Exit For
                        End If
                    Next
                    If sPerfilAutorizado <> "" Then
                        Exit For
                    End If
                Next

                objFileLog.Log_WriteLog(pathFile, strArchivo, " sPerfilAutorizado: " & sPerfilAutorizado)

                If sPerfilAutorizado = "" Then
                    sPerfilAutorizado = "-1"
                End If
            Else
                sPerfilAutorizado = "-1"
            End If

            Return sPerfilAutorizado
        Else
            Return "-1"
        End If
    End Function

    Private Function IsAuthenticated(ByVal vUsuario As String, ByVal vClave As String) As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Inicio a IsAuthenticated() ***")
        Dim strDominio As String = ConfigurationSettings.AppSettings("DominioLDAP")
        objFileLog.Log_WriteLog(pathFile, strArchivo, " strDominio: " & strDominio)
        Dim entry As New DirectoryEntry(strDominio, vUsuario, vClave)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " vUsuario: " & vUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, " vClave: " & vClave)

        Try
            Dim obj As Object = entry.NativeObject()
            Dim search As New DirectorySearcher(entry)
            search.Filter = "(SAMAccountName=" + vUsuario + ")"
            search.PropertiesToLoad.Add("cn")
            Dim resul As SearchResult
            resul = search.FindOne()
            If (resul Is Nothing) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, " *** resul Is Nothing ***")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin a IsAuthenticated() ***")
                Return False
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** resul Not Is Nothing ***")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin a IsAuthenticated() ***")
            Return True
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Exception: " & ex.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, " *** Fin a IsAuthenticated() ***")
            Return False
        End Try
    End Function

    Private Function GetDatosEmpleado(ByVal strUsuario As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "INICIO WS EMPLEADO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Dim objEmpleado As New clsUsuario
            Dim oTransaccion As New WSEmpleado.ConsultaOpcionesAuditoriaService

            oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlEmpleado").ToString()
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            oTransaccion.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio WS EMPLEADO --> URL: " & oTransaccion.Url & ", TimeOut: " & oTransaccion.Timeout.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Dim objReqPadre As New WSEmpleado.leerDatosEmpleado
            Dim objEmpleadoRequest As New WSEmpleado.DatosEmpleadoRequest
            Dim objAuditRequest As New WSEmpleado.AuditRequest
            'Response
            Dim objRespPadre As New WSEmpleado.leerDatosEmpleadoResponse
            'Set oRequest
            objAuditRequest.aplicacion = ConfigurationSettings.AppSettings("ConsNombreAplicacion")
            objAuditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            objAuditRequest.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            objAuditRequest.usrAplicacion = strUsuario
            'Invocar Método
            objEmpleadoRequest.login = strUsuario
            objReqPadre.audit = objAuditRequest
            objReqPadre.DatosEmpleadoRequest = objEmpleadoRequest

            objRespPadre = oTransaccion.leerDatosEmpleado(objReqPadre)

            'Auditoria
            Dim vResultado As String = objRespPadre.audit.codigoRespuesta
            Dim msgSalida As String = objRespPadre.audit.mensajeRespuesta

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO Input: --> strLogin :" + strUsuario + " aplicacion: " + objAuditRequest.aplicacion + ", idTransaccion: " + objAuditRequest.idTransaccion + ", ipAplicacion: " + objAuditRequest.ipAplicacion + " OUTPUT: vResultado: " + vResultado + ", msgSalida : " + msgSalida)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            'Exito de la consulta
            If vResultado = "1" Then
                objEmpleado.UsuarioId = objRespPadre.EmpleadoResponse.empleados(0).codigo
                objEmpleado.Login = objRespPadre.EmpleadoResponse.empleados(0).login
                objEmpleado.Nombre = objRespPadre.EmpleadoResponse.empleados(0).nombres
                objEmpleado.Apellido = objRespPadre.EmpleadoResponse.empleados(0).paterno
                objEmpleado.ApellidoMaterno = objRespPadre.EmpleadoResponse.empleados(0).materno
                objEmpleado.NombreCompleto = objRespPadre.EmpleadoResponse.empleados(0).nombres & " " & objRespPadre.EmpleadoResponse.empleados(0).paterno & " " & objRespPadre.EmpleadoResponse.empleados(0).materno
                objEmpleado.CodigoVendedor = objRespPadre.EmpleadoResponse.codigoVendedor
                objEmpleado.AreaId = objRespPadre.EmpleadoResponse.empleados(0).codigoArea
                objEmpleado.AreaDescripcion = objRespPadre.EmpleadoResponse.empleados(0).descripcionArea

                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, " WS EMPLEADO CorrectoWS OutPut: --> vResultado : " + vResultado + ", msgSalida : " + msgSalida)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO SIN DATOS Output: --> vResultado : " + vResultado + ", msgSalida : " + msgSalida)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
                objEmpleado = Nothing
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "FIN WS EMPLEADO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")

            Return objEmpleado

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "WS EMPLEADO ERROR : --> Mensaje WS : " + ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-------------------------------------------------")
        End Try
    End Function

    Private Function ConsultaPerfil(ByVal pUsuarioIn As String) As String
        Dim strCodPerfil As String = ""
        Dim pUsuario As String = Trim(pUsuarioIn)
        Try
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
                        strCodPerfil = strCodPerfil & oAccesoResponse.auditoria.AuditoriaItem.item(i).perfil & ","
                    Next
                    strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1)
                End If

            End If

        Catch ex As Exception

        End Try

        Return strCodPerfil

    End Function


End Class
