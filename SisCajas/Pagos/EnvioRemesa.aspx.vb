Imports SisCajas.Funciones
Public Class EnvioRemesa
    Inherits SICAR_WebBase

    Dim objFileLog As New SICAR_Log 'PBI000002148450
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtBolsa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtMonEfSol As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonChSol As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonEfDol As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonChDol As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgSobres As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hidFlagPerfil As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNroSobre As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtCompServ As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCompServ As System.Web.UI.WebControls.Label 'INI-936 - CNSO



    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objLog As New SICAR_Log 'PBI000002148450
    Dim nameFile As String = "EnvioRemesa" 'PBI000002148450
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga") 'PBI000002148450
    Dim strArchivo As String = objLog.Log_CrearNombreArchivo(nameFile) 'PBI000002148450

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450" & " - " & "Se ingreso a EnvioRemesa") 'PBI000002148450
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim alertaCompServ As New clsKeyAPP 'INI-936 - CNSO
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450  - alertaCompServ.bolParamEnvioRemesa: " & Funciones.CheckStr(alertaCompServ.bolParamEnvioRemesa)) 'PBI000002148450
            Dim dsSobres As DataSet
            If Request("MonefSol") <> "" Then
                txtMonEfSol.Text = Request("MonEfSol")
                txtMonChSol.Text = Request("MonChSol")
                txtMonEfDol.Text = Request("MonEfDol")
                txtMonChDol.Text = Request("MonChDol")
                txtBolsa.Text = Request("Bolsa")
                txtCompServ.Text = Request("CompServ") 'INICIATIVA - 529
            End If

            If Not Page.IsPostBack Then
                txtFecha.Text = String.Format("{0:dd/MM/yyyy}", Now)
                dsSobres = objCajas.FP_BolsasLibres(Session("ALMACEN"), "")
                If Not IsNothing(dsSobres) Then
                    Session("dsSobres") = dsSobres
                    dgSobres.DataSource = dsSobres.Tables(0)
                End If

                Me.DataBind()

                'Validacion Perfil Usuario "Supervisor Recaudaciones" 
                If CType(Session("codPerfil"), String).IndexOf(ConfigurationSettings.AppSettings("cod_perfil_supervisor")) <> -1 Then
                    dgSobres.Columns(6).Visible = True
                    hidFlagPerfil.Value = "S"
                End If

                btnGrabar.Attributes.Add("onClick", "f_Valida()")

            End If

            lblCompServ.Text = alertaCompServ.strAlertaCompServ 'INI-936 - CNSO
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450  - alertaCompServ.strAlertaCompServ: " & Funciones.CheckStr(alertaCompServ.strAlertaCompServ)) 'PBI000002148450
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PBI000002148450  - alertaCompServ.bolParamEnvioRemesa: " & Funciones.CheckStr(alertaCompServ.bolParamEnvioRemesa)) 'PBI000002148450
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'INICIATIVA - 529 INI
        If cboMoneda.SelectedValue = "0" Then
            Response.Write("<script>alert('Tiene que seleccionar un tipo de vía.')</script>")
            Return
        End If
        'INICIATIVA - 529 FIN

        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dblMontEfSol As Double
        Dim dblMontChSol As Double
        Dim dblMontEfDol As Double
        Dim dblMontChDol As Double
        Dim strURL As String
        Dim dblTipCam As Double
        Dim chkSelect As New CheckBox

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
        Dim Detalle(7, 3) As String 'INICIATIVA - 529

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcEnRm")
        wParam5 = 1
        wParam6 = "Envio de Remesa"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtEnRm")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        'FIN DE AUDITORIA

        dblTipCam = ObtenerTipoCambioSAP(txtFecha.Text) 'TS-JTN
        Dim i As Integer

        'AUDITORIA
        Detalle(1, 1) = "OfVta"
        Detalle(1, 2) = Session("ALMACEN")
        Detalle(1, 3) = "Oficina de Venta"

        Detalle(2, 1) = "Usuario"
        Detalle(2, 2) = Session("USUARIO")
        Detalle(2, 3) = "Usuario"

        Detalle(3, 1) = "Bolsa"
        Detalle(3, 2) = txtBolsa.Text
        Detalle(3, 3) = "Bolsa"

        Detalle(4, 1) = "TipCam"
        Detalle(4, 2) = dblTipCam
        Detalle(4, 3) = "Tipo de Cambio"

        Detalle(5, 1) = "Fecha"
        Detalle(5, 2) = txtFecha.Text & " " & CStr(Now.Hour & ":" & Now.Minute & ":" & Now.Second)
        Detalle(5, 3) = "Fecha"

        Detalle(6, 1) = "NomUs"
        Detalle(6, 2) = Session("NOMBRE_COMPLETO")
        Detalle(6, 3) = "Nombre Usuario"
        'INICIATIVA - 529 INI
        Detalle(7, 1) = "CompServ"
        Detalle(7, 2) = txtCompServ.Text
        Detalle(7, 3) = "CompServ"
        'INICIATIVA - 529 FIN
        'FIN DE AUDITORIA

        Try
            Dim strDetalleRemesa As String
            Dim fechaSobre As Date
            Dim strFechaSobre As String
            Dim strMsgError As String
            Dim formatES As New System.Globalization.CultureInfo("es-ES", True)

            For i = 0 To dgSobres.Items.Count - 1
                chkSelect = dgSobres.Items(i).FindControl("chkSobre")
                If chkSelect.Checked Then
                    objCajas.FP_SobreRemesa(Session("ALMACEN"), _
                                            CType(dgSobres.Items(i).FindControl("lblItemNroSobre"), Label).Text, _
                                            txtBolsa.Text + "|" + txtCompServ.Text) 'INICIATIVA - 529
                    fechaSobre = CType(dgSobres.Items(i).FindControl("lblFecha"), Label).Text
                    strFechaSobre = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(fechaSobre.ToShortDateString, formatES))

                    strDetalleRemesa += ConfigurationSettings.AppSettings("cod_Mandt_Sap") & ";" & txtBolsa.Text & ";" & _
                                        txtFecha.Text & ";" & _
                                        CType(dgSobres.Items(i).FindControl("lblItemNroSobre"), Label).Text & ";" & _
                                        strFechaSobre & ";" & _
                                        getTipoViaByDescripcion(CType(dgSobres.Items(i).FindControl("lblTipoVia"), Label).Text) & ";" & _
                                        CType(dgSobres.Items(i).FindControl("lblMonto"), Label).Text & ";" & _
                                        Session("ALMACEN") & ";" & _
                                        CType(dgSobres.Items(i).FindControl("lblUsuario"), Label).Text & ";" & _
                                        txtCompServ.Text & "|" 'INICIATIVA - 529
                End If
            Next

            objCajas.FP_InsertaRemesa2(Session("ALMACEN"), _
                                        Session("USUARIO"), _
                                        txtBolsa.Text, _
                                        dblTipCam, _
                                        txtFecha.Text & " " & Now.Hour & ":" & Now.Minute & ":" & Now.Second, _
                                        dblMontEfSol, _
                                        dblMontChSol, _
                                        dblMontEfDol, _
                                        dblMontChDol, _
                                        Session("NOMBRE_COMPLETO"))

            If dblMontEfSol > 0 Or dblMontChSol > 0 Or dblMontEfDol > 0 Or dblMontChDol > 0 Then

                strURL = "EnvioRemesa.aspx?MonefSol=" & Format(dblMontEfSol, "#####0.00")
                strURL = strURL & "&MonChSol=" & Format(dblMontChSol, "#####0.00")
                strURL = strURL & "&MonEfDol=" & Format(dblMontEfDol, "#####0.00")
                strURL = strURL & "&MonChDol=" & Format(dblMontChDol, "#####0.00")
                strURL = strURL & "&Bolsa=" & txtBolsa.Text
                strURL = strURL & "&CompServ=" & txtCompServ.Text 'INICIATIVA - 529
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Redirect(strURL)
            End If
            Response.Write("<script>alert('No existen envios a caja buzon pendientes de remesar')</script>")

        Catch Ex As Exception
            wParam5 = 0
            wParam6 = "Error en " & wParam6 & "." & Ex.Message
            If Ex.Message.IndexOf("ORA-") <> -1 Then
                Response.Write("<script>alert('EL NUMERO DE BOLSA YA HABIA SIDO INGRESADO')</script>")
            ElseIf Ex.Message.IndexOf("SAP") <> -1 Then
                Response.Write("<script>alert('" & Ex.Message & "')</script>")
            End If
        End Try
        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
    End Sub

    Private Sub dgSobres_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSobres.ItemDataBound
        Dim chkBox As CheckBox
        Dim chkBoxAll As CheckBox
        Dim imgModificar As ImageButton
        Dim imgGrabar As ImageButton
        Dim imgCancelar As ImageButton

        chkBox = e.Item.FindControl("chkSobre")
        If Not IsNothing(chkBox) Then
            chkBox.Attributes.Add("onClick", "f_Suma();")
        End If
        chkBoxAll = e.Item.FindControl("chkAll")
        If Not IsNothing(chkBoxAll) Then
            chkBoxAll.Attributes.Add("onClick", "f_CheckAll();")
        End If
        imgModificar = e.Item.FindControl("imgModificar")
        If Not IsNothing(imgModificar) Then
            imgModificar.Attributes.Add("onClick", "SetDivPosition();")
        End If
        imgGrabar = e.Item.FindControl("imgGrabar")
        If Not IsNothing(imgGrabar) Then
            imgGrabar.Attributes.Add("onClick", "SetDivPosition();")
        End If
        imgCancelar = e.Item.FindControl("imgCancelar")
        If Not IsNothing(imgCancelar) Then
            imgCancelar.Attributes.Add("onClick", "SetDivPosition();")
        End If
    End Sub

    Private Sub dgSobres_ItemDataBound(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSobres.ItemCommand
        Try
            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dsSobres As New DataSet

            If ((e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem)) Then
                If e.CommandName.ToUpper = "EDIT" Then
                    dgSobres.EditItemIndex = e.Item.ItemIndex
                End If
            ElseIf (e.Item.ItemType = ListItemType.EditItem) Then
                If e.CommandName.ToUpper = "CANCEL" Then
                    dgSobres.EditItemIndex = -1

                ElseIf e.CommandName.ToUpper = "UPDATE" Then
                    Dim nroSobre As String
                    Dim fechaSobre As String
                    Dim intResult As Integer
                    Dim formatES As New System.Globalization.CultureInfo("es-ES", True)
                    Dim fechaSob As Date

                    nroSobre = CType(dgSobres.Items(e.Item.ItemIndex).FindControl("lblEditNroSobre"), Label).Text
                    fechaSobre = CType(dgSobres.Items(e.Item.ItemIndex).FindControl("txtFechaSobre"), TextBox).Text
                    fechaSob = DateTime.Parse(fechaSobre, formatES)

                    intResult = objCajas.FP_Actualiza_Bolsa_Fecha(Session("ALMACEN"), nroSobre, fechaSob)
                    RegAuditoria_ActualizaBolsa(intResult)
                    dgSobres.EditItemIndex = -1
                End If
            End If

            dsSobres = objCajas.FP_BolsasLibres(Session("ALMACEN"), "")
            dgSobres.DataSource = dsSobres.Tables(0)
            dgSobres.DataBind()

        Catch ex As Exception
            Response.Write("<script>alert('Error Editando la fecha. ')</script>")
        End Try
    End Sub
    Private Sub RegAuditoria_ActualizaBolsa(ByVal intResult As Integer)
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String
            Dim descTrans As String = "Envio Remesas: Editar Fecha de sobre"

            Select Case intResult
                Case 0
                    descTrans = "Envio de Remesa: Edita fecha"
                Case 1
                    descTrans = "Envio de Remesa: Error actualizando fecha"
            End Select

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsEnvRemEditFech")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS

            Dim auditoriaGrabado As Boolean
            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try

    End Sub

    Private Function getTipoViaByDescripcion(ByVal strDesTipoVia As String) As String
        Dim strReturn As String
        Select Case strDesTipoVia
            Case "EFECTIVO SOLES"
                strReturn = "01"
            Case "EFECTIVO DOLARES"
                strReturn = "02"
            Case "CHEQUES SOLES"
                strReturn = "03"
            Case "CHEQUES DOLARES"
                strReturn = "04"
            Case Else
                strReturn = ""
        End Select
        Return strReturn
    End Function

    'TS-JTN
    Public Function ObtenerTipoCambioSAP(ByVal strFecha As String) As String
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Return Format(objOffline.Obtener_TipoCambio(strFecha), "#######0.000")
    End Function
    'TS-JTN
    'INICIATIVA - 529 INI
    Private Sub cboMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMoneda.SelectedIndexChanged
        Dim dsSobres As DataSet
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        txtFecha.Text = String.Format("{0:dd/MM/yyyy}", Now)
        dsSobres = objCajas.FP_BolsasLibres(Session("ALMACEN"), "")
        If Not IsNothing(dsSobres) Then
            Session("dsSobres") = dsSobres
            If cboMoneda.SelectedValue = "0" Then
                dgSobres.DataSource = dsSobres.Tables(0)
            Else
                dgSobres.DataSource = dsSobres.Tables(0).Select("BUZON_TIPOVIA = '" + cboMoneda.SelectedItem.Text.ToUpper + "'")
            End If
            dgSobres.DataBind()
        End If
    End Sub
    'INICIATIVA - 529 FIN
End Class
