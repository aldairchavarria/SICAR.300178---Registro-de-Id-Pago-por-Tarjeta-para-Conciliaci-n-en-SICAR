Public Class contabilizarRem
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents txtFecConta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdProcesar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFechaDoc As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodOficina As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents lbCodOficina As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTodosOf As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnLimpiar As System.Web.UI.HtmlControls.HtmlInputButton

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
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogContabilizarRem")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogContabilizarRem")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
    Dim strIdentifyLog As String
    Dim objClsContab As COM_SIC_Recaudacion.clsContabilizar
    Protected MensajeAudi As String
#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language='javascript'>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not IsPostBack Then
                txtFechaDoc.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                txtFecConta.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                strUsuario = Session("USUARIO")

                'Get IP
                Dim Ip As String = String.Empty
                Dim listaIp As String
                Dim hostName As String = System.Net.Dns.GetHostName
                Dim local As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(hostName)
                For Each I_ip As System.Net.IPAddress In local.AddressList
                    Ip = I_ip.ToString
                    Exit For
                Next
                ViewState("strIpAddressRem") = Ip

                Session("ContaRec_codOficina") = Nothing
                chkTodosOf.Attributes.Add("onclick", "checkEnabledOfi(this.checked);")
            End If
            cmdProcesar.Attributes.Add("onClick", "f_Procesar()")
        End If
    End Sub

    Private Sub loadDataHandler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loadDataHandler.Click
        Try
            strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio IND - loadDataHandler_Click ")
            If Not Session("dgListaOficinaRec") Is Nothing Then
                Dim dt As DataTable = DirectCast(Session("dgListaOficinaRec"), DataTable)
                If Not Session("ContaRec_codOficina") Is Nothing Then
                    Dim codigoOfi As String = CStr(Session("ContaRec_codOficina"))
                    Dim dv As New DataView
                    Dim arrCodigos() As String = Split(codigoOfi, ",")
                    dv.Table = dt

                    lbOficina.Items.Clear()
                    lbCodOficina.Items.Clear()

                    For i As Int32 = 0 To arrCodigos.Length - 1
                        dv.RowFilter = "CODIGO = '" & arrCodigos(i) & "'"
                        Dim drvResultado As DataRowView = dv.Item(0)
                        If Not drvResultado Is Nothing Then
                            With drvResultado
                                lbCodOficina.Items.Add(New ListItem(Trim(drvResultado("CODIGO"))))
                                lbOficina.Items.Add(New ListItem(Trim(drvResultado("DESCRIPCION"))))
                            End With
                        End If
                    Next

                    hidCodOficina.Value = CStr(Session("ContaRec_codOficina"))
                    chkTodosOf.Checked = False
                End If
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   error loadDataHandler_Click: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin IND - loadDataHandler_Click")
        End Try
    End Sub

    Private Sub cmdProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcesar.Click
        strUsuario = Session("USUARIO")
        strIdentifyLog = Session("ALMACEN") & "|" & strUsuario
        Dim strOficina As String
        Dim strFechaDoc As String
        Dim strFechaCont As String
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio cmdProcesar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

            strFechaDoc = txtFechaDoc.Value
            strFechaCont = txtFecConta.Value
            strOficina = hidCodOficina.Value
            Dim iCntEnvExitoso As Integer
            Dim iCntEnvFallido As Integer
            Dim iCntEnviada As Integer
            Dim strResult As String = String.Empty
            Dim blnError As Boolean = False
            Dim strMesContable As String = Date.Now.Month.ToString.PadLeft(2, "0")
            objClsContab = New COM_SIC_Recaudacion.clsContabilizar

            Dim strNombreAplicacion As String = ConfigurationSettings.AppSettings("NombreAplicacionContabilizacionRemWS")
            Dim strVersion As String = ConfigurationSettings.AppSettings("VersionContabilizacionRemWS")
            Dim strUsuarioApp As String = ConfigurationSettings.AppSettings("UsuarioAppContabilizacionRemWS")

            If strOficina.Equals(String.Empty) And Not chkTodosOf.Checked Then
                Response.Write("<script> alert('" + "Seleccione oficina" + "');</script>")
                Exit Sub
            End If

            If chkTodosOf.Checked Then
                strOficina = "0"
            End If

            If strNombreAplicacion Is Nothing Or strVersion Is Nothing Or strUsuarioApp Is Nothing Then
                Throw New ArgumentNullException
            End If

            If txtFecConta.Value.Equals(String.Empty) Then
                Response.Write("<script> alert('Seleccione fecha de contabilización');</script>")
                Exit Sub
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio objClsContab - Metodo ProcesarContabilizacionRemesa")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cod. Oficina : " & strOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Oficina de Venta : " & strOficina)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha Documento : " & strFechaDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha Contabilización  : " & strFechaCont)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Usuario : " & strUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin")

            objClsContab.ProcesarContabilizacionRem(strIdentifyLog, strOficina, strFechaDoc, strFechaCont, CStr(ViewState("strIpAddressRem")), strMesContable, strUsuario, iCntEnviada, iCntEnvExitoso, iCntEnvFallido)

            Dim mensajeAlert As String = String.Empty
            Dim strMsjEnviados As String = "Cantidad de documentos procesados : " & iCntEnviada
            Dim strMsjEnviadosOK As String = "Cantidad de documentos procesados correctamente : " & iCntEnvExitoso
            Dim strMsjEnviadosBAD As String = "Cantidad de documentos procesados fallidos : " & iCntEnvFallido
            mensajeAlert = String.Format("<script language='javascript'>alert('El proceso de contabilización ha terminado correctamente. \n{0}\n{1}\n{2}');</script>", strMsjEnviados, strMsjEnviadosOK, strMsjEnviadosBAD)
            Me.RegisterStartupScript("RegistraAlerta", mensajeAlert)
        Catch ex1 As ArgumentNullException
            Response.Write("<script> alert('" + ex1.Message.Substring(0, 20) & ". Los valores de parámetros no han sido configurados." + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex1.Message)
        Catch webEx As System.Net.WebException
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                Response.Write("<script> alert('" + "Tiempo de espera excedido." + "');</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: Tiempo de espera excedido.")
            Else
                Response.Write("<script> alert('" + webEx.Message + "');</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & webEx.Message)
            End If
        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Error Mensaje: " & ex.Message)
        Finally
            objClsContab = Nothing
            MensajeAudi = "Contabilizacion de Remesa. " & strUsuario & "|" & strOficina & "|" & strFechaDoc & "|" & strFechaCont
            RegistrarAuditoria(MensajeAudi, ConfigurationSettings.AppSettings("CodAuditContabRemesa"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin cmdProcesar_Click")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End Try
    End Sub

    Private Sub btnLimpiar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.ServerClick
        lbCodOficina.Items.Clear()
        lbOficina.Items.Clear()
    End Sub
#End Region

#Region "Metodos"

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
                Throw New Exception("Error. No se registro en Auditoria la Grabacion de contabilizacion de remesa.")
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
