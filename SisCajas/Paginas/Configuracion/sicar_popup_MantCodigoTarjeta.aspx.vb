Public Class sicar_popup_MantCodigoTarjeta
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboMantTarjeta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCodigo As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboEstado As System.Web.UI.HtmlControls.HtmlSelect
    Protected WithEvents cmdAceptar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtUsuarioCreacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaCreacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUsuarioModificacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFechaModificacion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComentario As System.Web.UI.WebControls.TextBox

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
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        If Not Page.IsPostBack Then

            cargarInformacionTarjeta()

        End If

    End Sub
    Private Sub cargarInformacionTarjeta()

        Try
            Dim objTransac As New COM_SIC_Activaciones.clsTarjetasPOS
            Dim dtTarjetas As New DataTable
            Dim dtViasPago As New DataTable
            Dim strRespuesta As String
            Dim strCodRespuesta As String
            Dim strEstado As String
            Dim strCCINS As String
            Dim strTarjeta As String
            Dim strCodigo As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "flagOperacion : " & Request.QueryString("flagOperacion"))

            If Request.QueryString("flagOperacion") = 2 Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "codTarjeta : " & Request.QueryString("codTarjeta"))

                dtTarjetas = objTransac.CodigosTarjetaPos(Request.QueryString("codTarjeta"), "", "", strCodRespuesta, strRespuesta)
                strCodigo = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAN_ID")), "", dtTarjetas.Rows(0)("COTAN_ID")))
                txtCodigo.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAV_ARD_ID")), "", dtTarjetas.Rows(0)("COTAV_ARD_ID")))
                txtComentario.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAV_COMENTARIO")), "", dtTarjetas.Rows(0)("COTAV_COMENTARIO")))
                txtUsuarioCreacion.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAV_USER_CREA")), "", dtTarjetas.Rows(0)("COTAV_USER_CREA")))
                txtFechaCreacion.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAD_FECHA_CREA")), "", dtTarjetas.Rows(0)("COTAD_FECHA_CREA")))
                txtUsuarioModificacion.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAV_USER_MODI")), "", dtTarjetas.Rows(0)("COTAV_USER_MODI")))
                txtFechaModificacion.Text = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAD_FECHA_MODI")), "", dtTarjetas.Rows(0)("COTAD_FECHA_MODI")))
                strEstado = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAN_ESTADO")), "", dtTarjetas.Rows(0)("COTAN_ESTADO")))
                strCCINS = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("CCINS")), "", dtTarjetas.Rows(0)("CCINS")))
                strTarjeta = Funciones.CheckStr(IIf(IsDBNull(dtTarjetas.Rows(0)("COTAV_COMENTARIO")), "", dtTarjetas.Rows(0)("COTAV_COMENTARIO")))

                objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "dtTarjetas.count : " & dtTarjetas.Rows.Count)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "strCodRespuesta : " & strCodRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "strRespuesta : " & strRespuesta)

                If strEstado.ToString() = 1 Then
                    cboEstado.SelectedIndex = 1
                ElseIf strEstado.ToString() = 0 Or strEstado.ToString() = "" Then
                    cboEstado.SelectedIndex = 0
                End If


            End If

            dtViasPago = objTransac.ConsultarViasPagoPos("", strCodRespuesta, strRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "strCodRespuesta : " & strCodRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "strRespuesta : " & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "dtViasPago count : " & dtViasPago.Rows.Count.ToString())

            Dim i As Integer
            Dim j As Integer = -1
            For i = 0 To dtViasPago.Rows.Count - 1


                'cboMantTarjeta.Items.Insert(, )
                cboMantTarjeta.Items.Insert(i, New ListItem(Funciones.CheckStr(dtViasPago.Rows(i).Item("VTEXT")), Funciones.CheckInt(dtViasPago.Rows(i).Item("ID_T_VIAS_PAGO"))))
                If (dtViasPago.Rows(i).Item("ID_T_VIAS_PAGO") = strCCINS) Then
                    j = i
                End If
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "strCCINS : " & strCCINS)
            cboMantTarjeta.SelectedIndex = IIf(j < 0, 0, j)

        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, "cargarInformacionTarjeta : " & "ERROR - cargarInformacionTarjeta : " & ex.Message.ToString())

        End Try


    End Sub

    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        Dim sMensaje As String = String.Empty
        Dim strCodErr As String = String.Empty
        Dim strMsjErr As String = String.Empty
        Try

            If txtCodigo.Text.Trim = "" Or txtComentario.Text.Trim = "" Then
                Response.Write("<script>alert('Verifique los campos ingresados');</script>")
                Return
            End If

            Dim objTransac As New COM_SIC_Activaciones.clsTarjetasPOS
            Session("COTAN_ID") = Request.QueryString("codTarjeta")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click")

            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_COTAN_ID : " & Session("COTAN_ID"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_CCINS : " & cboMantTarjeta.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_COTAV_ARD_ID : " & txtCodigo.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_COTAV_COMENTARIO : " & txtComentario.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_COTAN_ESTADO : " & cboEstado.SelectedIndex.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "P_COTAV_USER_MOD: " & Session("USUARIO"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "flagOperacion: " & Request.QueryString("flagOperacion"))

            If Request.QueryString("flagOperacion") = 2 Then

                objTransac.ActualizarViaPagoPos(Session("COTAN_ID"), _
                                                cboMantTarjeta.SelectedValue, _
                                                txtCodigo.Text, _
                                                txtComentario.Text, _
                                                cboEstado.SelectedIndex, _
                                                Session("USUARIO"), _
                                                strCodErr, _
                                                strMsjErr)
            Else
                objTransac.RegistrarViaPagoPos(cboMantTarjeta.SelectedValue, _
                                                txtCodigo.Text, _
                                                txtComentario.Text, _
                                                cboEstado.SelectedIndex, _
                                                Session("USUARIO"), _
                                                strCodErr, _
                                                strMsjErr)

            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "strCodErr: " & strCodErr)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "btnGrabar_Click : " & "strMsjErr: " & strMsjErr)

            If strCodErr <> "0" Then
                Response.Write("<script>alert('Error al grabar');</script>")
                Exit Sub
            Else
                Response.Write("<script>window.opener.f_respuesta('Registro grabado correctamente');</script>")
                Response.Write("<script>window.close();</script>")
            End If


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "error btnGuardar_Click: " & ex.Message.ToString())
            Exit Sub
        Finally

            sMensaje = "RegistrarOficinasxCajasPOS - Registro"

            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsMantCodTar_codTrsAuditoria"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin btnGrabar_Click")
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
