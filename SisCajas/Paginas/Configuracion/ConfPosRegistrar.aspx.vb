Public Class ConfPosRegistrar
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents frmPrincipal As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents chkVisa As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkMCProcesos As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblOficinaVenta As System.Web.UI.WebControls.Label
    Protected WithEvents lblNumeroCaja As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodigoEsta As System.Web.UI.WebControls.Label
    Protected WithEvents txtIpCaja As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNombreEquipo As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidTramaPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cboEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboIntegra As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cboPagoAuto As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents btnGuardar As System.Web.UI.WebControls.Button
    Protected WithEvents hidIPAntigua As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIpOculto As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnOculto As System.Web.UI.WebControls.Button
    Protected WithEvents hidCodigosOficinas As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidDesOficinas As System.Web.UI.HtmlControls.HtmlInputHidden


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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String
#End Region

    Dim COD_PDV As String = String.Empty
    Dim BEZEI As String = String.Empty
    Dim POSV_NROTIENDA As String = String.Empty
    Dim POSV_NROCAJA As String = String.Empty
    Dim POSV_IDESTABLEC As String = String.Empty
    Dim POSV_IPCAJA As String = String.Empty
    Dim POSV_EQUIPO As String = String.Empty
    Dim POSC_ESTADO As String = String.Empty
    Dim POSC_FLG_SICAR_V As String = String.Empty
    Dim POSC_FLG_SICAR_M As String = String.Empty
    Dim POSC_FLG_AUTOM As String = String.Empty
    Dim FLAG_TIPO_TARJ As String = String.Empty
    Dim strExito As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else

            If Not Page.IsPostBack Then

                Dim dsDatos As New DataSet
                Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS
                Dim strCodErr As String = String.Empty
                Dim strMsjErr As String = String.Empty

                COD_PDV = Request.QueryString("COD_PDV").ToString()
                BEZEI = Request.QueryString("BEZEI").ToString()
                POSV_NROTIENDA = Request.QueryString("POSV_NROTIENDA").ToString()
                POSV_NROCAJA = Request.QueryString("POSV_NROCAJA").ToString()
                POSV_IDESTABLEC = Request.QueryString("POSV_IDESTABLEC").ToString()
                POSV_IPCAJA = Request.QueryString("POSV_IPCAJA").ToString()
                POSV_EQUIPO = Request.QueryString("POSV_EQUIPO").ToString()
                POSC_ESTADO = Request.QueryString("POSC_ESTADO").ToString()
                'POSC_FLG_SICAR = Request.QueryString("POSC_FLG_SICAR").ToString()
                'POSC_FLG_AUTOM = Request.QueryString("POSC_FLG_AUTOM").ToString()
                FLAG_TIPO_TARJ = Request.QueryString("FLAG_TIPO_TARJ").ToString()

         


                dsDatos = objTransac.ObtenerOficinasxCajasPOS("", COD_PDV, POSV_IPCAJA, "", "", strCodErr, strMsjErr)


                If dsDatos.Tables(0).Rows.Count > 0 Then
                    POSC_FLG_SICAR_V = Convert.ToString(dsDatos.Tables(0).Rows(0).Item("POSC_FLG_SICAR_V"))
                    POSC_FLG_SICAR_M = Convert.ToString(dsDatos.Tables(0).Rows(0).Item("POSC_FLG_SICAR_M"))
                    POSC_FLG_AUTOM = Convert.ToString(dsDatos.Tables(0).Rows(0).Item("POSC_FLG_AUTOM"))
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "COD_PDV : " & COD_PDV)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "BEZEI : " & BEZEI)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_NROTIENDA : " & POSV_NROTIENDA)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_NROCAJA : " & POSV_NROCAJA)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_IDESTABLEC : " & POSV_IDESTABLEC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_IPCAJA : " & POSV_IPCAJA)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_EQUIPO : " & POSV_EQUIPO)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSC_ESTADO : " & POSC_ESTADO)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSC_FLG_SICAR_V : " & POSC_FLG_SICAR_V)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSC_FLG_SICAR_M : " & POSC_FLG_SICAR_M)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSC_FLG_AUTOM : " & POSC_FLG_AUTOM)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "FLAG_TIPO_TARJ : " & FLAG_TIPO_TARJ)

                CargaDatos()

                txtIpOculto.Text = POSV_IPCAJA.ToString

            Else

                COD_PDV = Request.QueryString("COD_PDV").ToString()
                POSV_NROTIENDA = Request.QueryString("POSV_NROTIENDA").ToString()

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "COD_PDV : " & COD_PDV)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Page_Load Registrar : " & "POSV_NROTIENDA : " & POSV_NROTIENDA)
            End If
        End If

    End Sub

    Private Sub CargaDatos()
        Try
            lblOficinaVenta.Text = BEZEI.ToString()
            lblNumeroCaja.Text = POSV_NROCAJA.ToString()
            lblCodigoEsta.Text = POSV_IDESTABLEC.ToString()
            txtIpCaja.Text = POSV_IPCAJA.ToString()
            txtNombreEquipo.Text = POSV_EQUIPO.ToString()

            If POSC_ESTADO.ToString() = 1 Then
                cboEstado.SelectedIndex = 1
            ElseIf POSC_ESTADO.ToString() = 0 Or POSC_ESTADO.ToString() = "" Then
                cboEstado.SelectedIndex = 0
            End If

            If POSC_FLG_SICAR_V.ToString() = 1 Then
                    chkVisa.Checked = True
            ElseIf POSC_FLG_SICAR_V.ToString() = 0 Or POSC_FLG_SICAR_V.ToString() = "" Then
                    chkVisa.Checked = False
                End If
            If POSC_FLG_SICAR_M.ToString() = 1 Then
                    chkMCProcesos.Checked = True
            ElseIf POSC_FLG_SICAR_M.ToString() = 0 Or POSC_FLG_SICAR_M.ToString() = "" Then
                    chkMCProcesos.Checked = False
                End If

            If POSC_FLG_AUTOM.ToString() = 1 Then
                cboPagoAuto.SelectedIndex = 1
            ElseIf POSC_FLG_AUTOM.ToString() = 0 Or POSC_FLG_AUTOM.ToString() = "" Then
                cboPagoAuto.SelectedIndex = 0
                End If



        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error CargaDatos: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin CargaDatos")
        End Try
        
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("ConfPosOficina.aspx?exito=1", False)
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim sMensaje As String = String.Empty
        Dim strCodErr As String = String.Empty
        Dim strMsjErr As String = String.Empty
        Try

            Dim objTransac As New COM_SIC_Activaciones.clsTransaccionPOS


            objTransac.RegistrarOficinasxCajasPOS(txtIpOculto.Text, _
                                                  "V", _
                                                  Session("USUARIO"), _
                                                  POSV_NROTIENDA, _
                                                  lblNumeroCaja.Text, _
                                                  lblCodigoEsta.Text, _
                                                  COD_PDV, _
                                                  txtIpCaja.Text, _
                                                  cboEstado.SelectedIndex.ToString, _
                                                  txtNombreEquipo.Text, _
                                                  "", _
                                                  IIf(chkVisa.Checked = True, "1", "0"), _
                                                  cboPagoAuto.SelectedIndex.ToString, _
                                                  "0", _
                                                  "", _
                                                  strCodErr, _
                                                  strMsjErr)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "   btnGuardar_Click - Incio Carga VISA/MC ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "IP antigua : " & txtIpOculto.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Usuario : " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Numero tienda : " & POSV_NROTIENDA)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Numero Caja : " & lblNumeroCaja.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Codigo Establecimiento : " & lblCodigoEsta.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Codigo de oficina : " & COD_PDV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Ip Nueva : " & txtIpCaja.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Estado : " & cboEstado.SelectedIndex.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Nombre Equipo : " & txtNombreEquipo.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Codigo de terminar : " & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Integracion ON : " & IIf(chkVisa.Checked = True, "1", "0"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cmdBuscar_Click : " & "Pago automatico : " & cboPagoAuto.SelectedIndex.ToString)

            If strCodErr <> "0" Then
                Response.Write("<script>alert('Ocurrio un error al guardar');</script>")
                Return
            End If

            objTransac.RegistrarOficinasxCajasPOS(txtIpOculto.Text, _
                                                  "M", _
                                                   Session("USUARIO"), _
                                                   POSV_NROTIENDA, _
                                                   lblNumeroCaja.Text, _
                                                   lblCodigoEsta.Text, _
                                                   COD_PDV, _
                                                   txtIpCaja.Text, _
                                                   cboEstado.SelectedIndex.ToString, _
                                                   txtNombreEquipo.Text, _
                                                   "", _
                                                   IIf(chkMCProcesos.Checked = True, "1", "0"), _
                                                   cboPagoAuto.SelectedIndex.ToString, _
                                                   "0", _
                                                   "", _
                                                   strCodErr, _
                                                   strMsjErr)

            If strCodErr = "0" Then
                Response.Redirect("ConfPosOficina.aspx?exito=" & strCodErr, False)
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGuardar_Click: " & ex.Message)
        Finally

            sMensaje = "RegistrarOficinasxCajasPOS - Registro"

            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsMantCajasPOS_codTrsAuditoria"))


            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGuardar_Click")
        End Try



    End Sub


    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception
            ' Throw New Exception("Error Registrar Auditoria.")
        End Try
    End Sub

End Class
