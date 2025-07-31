Imports SisCajas.GenFunctions
Imports ADODB
Imports System.Data.OleDb
Imports COM_SIC_Activaciones
Imports COM_SIC_Cajas

Public Class home
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents Ingreso As System.Web.UI.WebControls.ImageButton
    Protected WithEvents IngRegularizador As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtDesOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cboOficina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTipoUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtImpresora As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtMensajeImp As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFlag As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents txtCodOficina2 As System.Web.UI.HtmlControls.HtmlInputHidden

  'PROY-27440 INI
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HidDatoIP As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HidTipOpera As System.Web.UI.HtmlControls.HtmlInputHidden
  Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
  'PROY-27440 FIN

    'Protected WithEvents cboPrinterA As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents lblImpresora As System.Web.UI.WebControls.Label
    'Protected WithEvents txtTipoUsuario As System.Web.UI.HtmlControls.HtmlInputHidden


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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLoginLog")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim IdentificadorLog As String 'PROY-33111-Inicio
    Dim objIGV As New clsConsultaIgv
    Dim dtIGV As DataTable
    Dim dtIGVEx As DataTable
    'PROY-27440 INI
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    Dim strIdentifyLogPOS As String
    Dim objLog As New SICAR_Log
    'PROY-27440 FIN

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strDomainUser, strUsuario As String
        Dim objEmpleado As New clsUsuario
        Dim objEmpleadoWS As New clsEmpleadoWS
        Dim strTipoUsuario
        Dim rsVerificaUsuario As Recordset
        Dim strNUsuario
        Dim daRecord As New OleDbDataAdapter
        Dim dsRecord As New DataSet
        Dim intSAP As Integer
        Dim objMsSAP As New COM_SIC_Activaciones.clsConsultaMsSap

        Session.Timeout = 90

        Me.cboOficina.Attributes.Add("onchange", "CambiaImprOficina(this.value);")
        Me.Ingreso.Attributes.Add("onClick", "Botones();")
        Me.IngRegularizador.Attributes.Add("onClick", "Botones();")

        If Not Page.IsPostBack Then

            Dim strRutaActual As String = Request.ServerVariables.Item("HTTP_HOST") & Request.ServerVariables.Item("PATH_INFO")
            Dim strRutaAplicacion As String = ConfigurationSettings.AppSettings.Item("ConstPathAplicacion") & ConfigurationSettings.AppSettings.Item("ConstPageHome")
            strRutaAplicacion = strRutaAplicacion.Substring(7)

            If ConfigurationSettings.AppSettings.Item("ConstFlagIngreso") = "1" Then
                If Not strRutaActual.ToUpper.Equals(strRutaAplicacion.ToUpper) Then
                    Response.Redirect("ErrorIngresoUrl.aspx")
                    Response.End()
                End If
            End If

            strDomainUser = Request.ServerVariables("LOGON_USER")  'Request.ServerVariables("LOGON_USER")   '"E77281" 
            strUsuario = Trim(Mid(strDomainUser, InStr(1, strDomainUser, "\", vbTextCompare) + 1, 20))

            Session("strUsuario") = strUsuario
            txtFlag.Value = "NV"
            IdentificadorLog = Session("strUsuario") 'PROY-33111-Inicio
            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strUsuario : " & Session("strUsuario"))
            Dim objAccesoAplicativo As Object
            Dim codAplicacion As String = ConfigurationSettings.AppSettings("codAplicacion")

            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim oAccesoRequest As New AuditoriaWS.AccesoRequest
            Dim oAccesoResponse As New AuditoriaWS.AccesoResponse

            objAuditoriaWS.Url = ConfigurationSettings.AppSettings("consRutaWSSeguridad").ToString()
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials
            objAuditoriaWS.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("ConstTimeOutEmpleado").ToString())

            oAccesoRequest.usuario = strUsuario
            oAccesoRequest.aplicacion = codAplicacion
            'FFS
            oAccesoResponse = objAuditoriaWS.leerDatosUsuario(oAccesoRequest)

            If oAccesoResponse.resultado.estado = "1" Then
                Session("CadenaAleatoria") = Funciones.CadenaAleatoria()
                Session("codUsuario") = oAccesoResponse.auditoria.AuditoriaItem.item(0).codigo
                Session("codPerfil") = oAccesoResponse.auditoria.AuditoriaItem.item(0).perfil
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Cod Perfil : " & Session("codPerfil"))

                'Consulta WS Datos Empleado
                objEmpleado = GetDatosEmpleado(strUsuario)

                If objEmpleado Is Nothing OrElse objEmpleado.CodigoVendedor = "" Then
                    rsVerificaUsuario = Nothing
                    objEmpleado = Nothing
                    Response.Redirect("Erroringreso.asp")
                End If
                '*************************************************************************'

                '*****************************************************************'
                ''** BLOQUE TEMPORAL, COMENTADO:
                'Session("USUARIO") = "23175"
                Session("USUARIO") = objEmpleado.CodigoVendedor
                Session("CODIGOAREA") = objEmpleado.AreaId
                Session("NOMBREAREA") = objEmpleado.AreaDescripcion
                strNUsuario = Session("USUARIO")
                '******************************************************************'

                'Session("USUARIO") = objEmpleado.CodigoVendedor
                'Session("CODIGOAREA") = objEmpleado.AreaId
                'Session("NOMBREAREA") = objEmpleado.AreaDescripcion
                'strNUsuario = Session("USUARIO")


                IdentificadorLog = Session("USUARIO") 'PROY-33111-Inicio

                Dim objOffline As New COM_SIC_OffLine.clsOffline
                intSAP = objOffline.Get_ConsultaSAP

                'FFS
                'Trabajar en Modo desconectado.

                'CAMBIADO POR FFS MODO DESCONECTADO DE ZAP
                Dim dsVendedor As DataSet
                'If intSAP = 1 Then
                '    Dim objPagos As New SAP_SIC_Pagos.clsPagos
                '    dsVendedor = objPagos.Get_ConsultaVend(strNUsuario)
                '    objPagos = Nothing
                'Else
                Dim objPagos As New COM_SIC_OffLine.clsOffline
                dsVendedor = objPagos.Get_ConsultaVend(strNUsuario)
                objPagos = Nothing
                'End If

                If Not dsVendedor Is Nothing Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Nro Registro Vendedor : " & dsVendedor.Tables(0).Rows.Count)
                    If dsVendedor.Tables(0).Rows.Count > 0 Then
                        Session("NOMBRE_COMPLETO") = dsVendedor.Tables(0).Rows(0).Item(1)
                        Session("ALMACEN") = dsVendedor.Tables(0).Rows(0).Item(2)
                        Session("OFICINA") = dsVendedor.Tables(0).Rows(0).Item(3)
                        txtCodOficina2.Value = Convert.ToString(dsVendedor.Tables(0).Rows(0).Item(4)) '---> CAMBIADO POR JTN
                        'FFS
                        'strTipoUsuario = dsVendedor.Tables(0).Rows(0).Item(6)
                        strTipoUsuario = dsVendedor.Tables(0).Rows(0).Item(6)
                        Session("PERFIL_SAP") = strTipoUsuario
                        txtTipoUsuario.Value = strTipoUsuario
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strTipoUsuario:" & strTipoUsuario)
                        'PROY-33111-Inicio
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Inicio Consulta Cajero - DAC")
                        Dim objRecaudacionDAc As New clsRecaudacionDAC
                        Dim dtCajeroDac As New DataTable
                        Dim strCodRespuesta, strRespuesta As String

                        Dim strEstadoCajDac As String
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Cajero Sicar :" & Session("USUARIO"))
                        dtCajeroDac = objRecaudacionDAc.ConsultarCajeroDAC("", "", Session("USUARIO"), strCodRespuesta, strRespuesta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strCodRespuesta :" & strCodRespuesta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strRespuesta:" & strRespuesta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Nro Registros:" & dtCajeroDac.Rows.Count)

                        Session("COD_SUBOFICINA") = ""
                        Session("SUBOFICINA") = ""
                        Session("SUBOFICINADAC") = ""

                        If (dtCajeroDac.Rows.Count > 0) Then

                            strEstadoCajDac = Funciones.CheckStr(dtCajeroDac.Rows(0)("CASOV_ESTADO"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- strEstadoCajDac: " & strEstadoCajDac)
                            If (strEstadoCajDac = 1) Then
                                
                                Session("COD_SUBOFICINA") = Funciones.CheckStr(dtCajeroDac.Rows(0)("CASOV_SUB_OFICINA"))
                                Session("SUBOFICINA") = Funciones.CheckStr(dtCajeroDac.Rows(0)("SUB_OFICINA_DESC"))
                                Session("SUBOFICINADAC") = "-  " & Session("COD_SUBOFICINA") & " - " & Session("SUBOFICINA")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- COD SUB_OFICINA : " & Session("COD_SUBOFICINA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- SUB OFICINA : " & Session("SUBOFICINA"))
                            End If

                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Fin Consulta Cajero - DAC")
                        'PROY-33111-Fin

                    Else
                        rsVerificaUsuario = Nothing
                        objEmpleado = Nothing
                        Response.Redirect("Erroringreso.asp")
                    End If
                Else
                    objEmpleado = Nothing
                    Response.Redirect("Erroringreso.asp")
                End If
            Else
                Response.Redirect("Erroringreso.asp")
            End If

            irSitePiloto()

            'PROY-27440 INICIO

            Me.HidTipOpera.Value = ClsKeyPOS.strIP
            Me.HidDatoAuditPos.Value = "" & "|" & _
          Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
          Funciones.CheckStr(Session("USUARIO"))

            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME LOAD - HidTipOpera: " & HidTipOpera.Value)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME LOAD - HidDatoAuditPos: " & HidDatoAuditPos.Value)
            'PROY-27440 FIN

            If strTipoUsuario = "R" Or txtCodOficina2.Value <> "" Then
                cboOficina.Visible = True
                Ingreso.Visible = False
                IngRegularizador.Visible = True
                lblOficina.Visible = True

                Dim dsOficinas As DataSet

                If strTipoUsuario = "R" Then
                    'CAMBIADO POR FFS (MODO DESCONECTADO DE ZAP)
                    'If intSAP = 1 Then
                    '    Dim objOficinas As New SAP_SIC_Pagos.clsPagos
                    '    dsOficinas = objOficinas.Get_ConsultaOficinaVenta("", "")
                    'Else
                    Dim objOficinas As New COM_SIC_OffLine.clsOffline
                    dsOficinas = objOficinas.Get_ConsultaOficinaVenta("", "")
                    'End If
                Else
                    'CAMBIADO POR FFS (MODO DESCONECTADO DE ZAP)
                    'If intSAP = 1 Then
                    '    Dim objOficinas As New SAP_SIC_Pagos.clsPagos
                    '    dsOficinas = objOficinas.Get_ConsultaOficinaVenta(Session("ALMACEN"), "")
                    '    dsOficinas.Merge(objOficinas.Get_ConsultaOficinaVenta(txtCodOficina2.Value, ""), True)
                    'Else
                    Dim objOficinas As New COM_SIC_OffLine.clsOffline
                    dsOficinas = objOficinas.Get_ConsultaOficinaVenta(Session("ALMACEN"), "")
                    dsOficinas.Merge(objOficinas.Get_ConsultaOficinaVenta(txtCodOficina2.Value, ""), True)
                    'End If
                    txtCodOficina.Value = Session("ALMACEN")
                End If

                cboOficina.DataSource = dsOficinas.Tables(0)
                cboOficina.DataValueField = "VKBUR"
                cboOficina.DataTextField = "BEZEI"
                cboOficina.DataBind()
            Else
                cboOficina.Visible = False
                Ingreso.Visible = True
                IngRegularizador.Visible = False
                lblOficina.Visible = False
                txtCodOficina.Value = Session("ALMACEN")
            End If

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Inicio de Consulta IGV objIGV.Obtener_Lista()")

                dtIGV = objIGV.Obtener_Lista("", CStr(Session("CadenaAleatoria")), "0")
                dtIGVEx = objIGV.Obtener_Lista("", CStr(Session("CadenaAleatoria")), "1")

                dtIGV.AcceptChanges()
                dtIGVEx.AcceptChanges()
                Session("Lista_Impuesto") = dtIGV
                Session("Lista_ImpuestoEx") = dtIGVEx
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & " - Registros IGV : " & dtIGV.Rows.Count)
                If dtIGV.Rows.Count = 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- No existen registros de IGV en la consulta realizada")
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- Inicio de Consulta IGV objIGV.Obtener_Lista()")
            Catch exx As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, IdentificadorLog & "- EXCEPCION CONSULTA IGV: " & exx.Message & "-" & exx.StackTrace)
                Session("Lista_Impuesto") = New DataTable
                Session("Lista_ImpuestoEx") = New DataTable

            End Try
        End If
        Call Me.CargarOpcionesPagina()
    End Sub

    Private Sub CargarOpcionesPagina()

        Dim conPag As New COM_SIC_Activaciones.clsAuditoriaWS
        Dim OpcionesPag As New ArrayList
        Dim vResultado As Boolean = False
        Dim CodAplicacion As String = ConfigurationSettings.AppSettings("CodAplicacion")

        OpcionesPag = conPag.ConsultarOpcionesPagina(Session("codUsuario"), CodAplicacion, vResultado)

        If vResultado Then
            Session("WS_OpcionesPagina") = OpcionesPag
        End If

    End Sub

    Private Sub IngRegularizador_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles IngRegularizador.Click

    'PROY-27440 INI
    Session("IpLocal") = "" : Session("IpLocal") = Me.HidDatoIP.Value
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME IngRegularizador_Click - Session(IpLocal): " & Session("IpLocal"))
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME IngRegularizador_Click - HidDatoIP: " & HidDatoIP.Value)
    'PROY-27440 FIN

        Dim CanalVenta
        Session("CANAL") = ""
        Session("ALMACEN") = cboOficina.SelectedValue
        Session("OFICINA") = cboOficina.SelectedItem.Text

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'CAMBIADO POR FFS (MODO DESCONECTADO DE ZAP)

        'Dim intSAP = objOffline.Get_ConsultaSAP
        'Dim objPagos As New SAP_SIC_Pagos.clsPagos

        Dim objPagos As Object
        'If intSAP = 1 Then
        '    objPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        objPagos = New COM_SIC_OffLine.clsOffline
        'End If
        Dim dsCanal As DataSet
        dsCanal = objPagos.Get_ConsultaOficinaVenta(Session("ALMACEN"), Session("CANAL"))
        objPagos = Nothing

        If dsCanal.Tables(0).Rows.Count > 0 Then
            CanalVenta = dsCanal.Tables(0).Rows(0).Item("VTWEG")
        End If
        If CanalVenta <> "" Then
            Session("CANAL") = Trim(CanalVenta)
        End If
        Session("CodImprTicket") = ""
        If txtImpresora.Value.Trim.Length > 0 Then
            Dim arrImp() As String = txtImpresora.Value.Split(";")
            If arrImp.Length = 4 Then
                Session("CodImprTicket") = arrImp(0)
                Session("SerieImprTicket") = arrImp(1)
                Session("UsuarioImpr") = arrImp(2)
                Session("FecLoginUsuImpr") = arrImp(3)

                'CAMBIADO POR FFS (MODO DESCONECTADO DE ZAP)
                'If intSAP = 1 Then
                '    objPagos = New SAP_SIC_Pagos.clsPagos
                '    objPagos.Set_ActUsoImpresora(Session("USUARIO"), Session("CodImprTicket"), Me.txtMensajeImp.Value)
                'Else
                'Dim dsImpresoras As DataSet
                'dsImpresoras = objOffline.Get_ListaImpresoras(Session("CANAL"))
                objOffline.Set_ActUsoImpresora(Session("ALMACEN"), Session("USUARIO"), Session("CodImprTicket"), Me.txtMensajeImp.Value)
                objOffline = Nothing
                'End If

            End If
        End If
        Session("CodImprTicket") = "1"
        Response.Redirect("index.aspx")
        'Response.Write("<script language=javascript>window.open('index.aspx','Chuls','menubar=false);</script>")
    End Sub

    Private Sub Ingreso_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Ingreso.Click

        'PROY-27440 INI
        Session("IpLocal") = "" : Session("IpLocal") = Me.HidDatoIP.Value
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME IngRegularizador_Click - Session(IpLocal): " & Session("IpLocal"))
        objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLogPOS & " === " & "HOME IngRegularizador_Click - HidDatoIP: " & HidDatoIP.Value)
        'PROY-27440 FIN

        Dim CanalVenta
        Session("CANAL") = ""

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim intSAP = objOffline.Get_ConsultaSAP

        'CAMBIADO POR FFS (INICIO)
        'Dim objPagos As New SAP_SIC_Pagos.clsPagos

        Dim objPagos As Object
        'If intSAP = 1 Then
        '    objPagos = New SAP_SIC_Pagos.clsPagos
        'Else
        objPagos = New COM_SIC_OffLine.clsOffline
        'End If
        'CAMBIADO POR FFS (FIN)

        Dim dsCanal As DataSet
        dsCanal = objPagos.Get_ConsultaOficinaVenta(Session("ALMACEN"), Session("CANAL"))
        objPagos = Nothing

        If dsCanal.Tables(0).Rows.Count > 0 Then
            CanalVenta = dsCanal.Tables(0).Rows(0).Item("VTWEG")
        End If
        If CanalVenta <> "" Then
            Session("CANAL") = Trim(CanalVenta)
        End If

        Session("CodImprTicket") = ""
        If txtImpresora.Value.Trim.Length > 0 Then
            Dim arrImp() As String = txtImpresora.Value.Split(";")
            If arrImp.Length = 4 Then
                Session("CodImprTicket") = arrImp(0)
                Session("SerieImprTicket") = arrImp(1)
                Session("UsuarioImpr") = arrImp(2)
                Session("FecLoginUsuImpr") = arrImp(3)

                'CAMBIADO POR FFS (INICIO)
                'If intSAP = 1 Then
                '    objPagos = New SAP_SIC_Pagos.clsPagos
                '    objPagos.Set_ActUsoImpresora(Session("USUARIO"), Session("CodImprTicket"), Me.txtMensajeImp.Value)
                objOffline.Set_ActUsoImpresora(Session("ALMACEN"), Session("USUARIO"), Session("CodImprTicket"), Me.txtMensajeImp.Value)
                objOffline = Nothing
                'Else
                'Dim dsImpresoras As DataSet
                'dsImpresoras = objOffline.Get_ListaImpresoras(Session("CANAL"))
                'objOffline = Nothing
                'objOffline = Nothing
                'End If
                'CAMBIADO POR FFS (FIN)

            End If
        End If

        Response.Redirect("index.aspx")
    End Sub
    Private Sub irSitePiloto()
        Dim strIdentifyLog As String = Session("strUsuario")
        Try
            Dim strGrupo As String = ConfigurationSettings.AppSettings("constParamPilotoGrupo")
            Dim constPilotoFlag As String = ConfigurationSettings.AppSettings("constParamPilotoFlag")
            Dim constPilotoPdv As String = ConfigurationSettings.AppSettings("constParamPilotoPdv")
            Dim constPilotoSite As String = ConfigurationSettings.AppSettings("constParamPilotoSite")

            Dim strPilotoPdv As String = String.Empty
            Dim strPilotoSite As String = String.Empty

            Dim dsParam As DataSet = (New COM_SIC_Cajas.clsCajas).FP_ConsultaParametros(strGrupo)

            If Not IsNothing(dsParam) AndAlso dsParam.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1

                    Dim strParamKey As String = Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_KEY"))
                    Dim strParamValue As String = Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE"))

                    If strParamKey = constPilotoFlag Then
                        If strParamValue = "N" Then
                            Exit Sub
                        End If
                    End If

                    If strParamKey = constPilotoPdv Then
                        strPilotoPdv = strParamValue
                    End If

                    If strParamKey = constPilotoSite Then
                        strPilotoSite = strParamValue
                    End If
                Next
            End If

            Dim pdvUsuario As String = Session("ALMACEN")
            Dim nodoActual As String = Funciones.CheckStr(Request.ServerVariables("HTTP_HOST"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[nodoActual={0}]", nodoActual))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[pdvUsuario={0}]", pdvUsuario))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[pilotoPdv={0}]", strPilotoPdv))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[pilotoSite={0}]", strPilotoSite))

            If strPilotoPdv.IndexOf(pdvUsuario) > 0 Then
                If Not strPilotoSite.IndexOf(nodoActual) >= 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[REDIRECCIONA PILOTO => {0}]", strPilotoSite))
                    Response.Redirect(strPilotoSite)
                    Exit Sub
                End If
            Else
                If strPilotoSite.IndexOf(nodoActual) >= 0 Then
                    Response.Redirect("ErrorIngresoUrl.aspx")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: irSitePiloto)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-" & String.Format("[ERROR={0}]", ex.Message) & MaptPath)
            'FIN PROY-140126

            Exit Sub
        End Try
    End Sub
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

End Class