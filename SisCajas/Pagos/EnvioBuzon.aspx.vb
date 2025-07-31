Public Class EnvioBuzon
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents hldVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cboMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtBolsa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtImporte As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroBolsa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox 'INICIATIVA-565

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
    Dim nameFile As String = "Log_EnvioBuzon"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogEnvioBuzon")
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
            btnGrabar.Attributes.Add("onClick", "f_Valida()")
            'INICIATIVA-565 INI
            If Not Page.IsPostBack Then
                txtFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            End If
            'INICIATIVA-565 FIN

            If Request.Item("strCajero") <> "" Then
                txtCajero.Text = Request.Item("strCajero")
                txtImporte.Text = Request.Item("strMonto")
                txtNroBolsa.Text = Request.Item("strBolsa")
                txtFecha.Text = Request.Item("strFecha") 'INICIATIVA-565
        End If
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click


        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Get_ConsultaEstadoCaja ")
        If Not VerificarCierreCaja() Then
            Return
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "fin Get_ConsultaEstadoCaja ")


        Dim obj As New COM_SIC_Cajas.clsCajas
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim dblBuzon As Double
        Dim dblEfectivo As Double
        Dim dblTipCam As Double
        Dim strURL As String
        Dim dblMonto As Double
        Dim datFecha As Date
        Dim intAutoriza As Integer
        Dim objConf As New COM_SIC_Configura.clsConfigura

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
        Dim Detalle(7, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcEnCB")
        wParam5 = 1
        wParam6 = "Envio a Caja Buzon"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtEnCB")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        'FIN DE AUDITORIA



        'datFecha = CDate(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Now.Year)

        Try
            'dblEfectivo = obj.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), datFecha)
            dblEfectivo = obj.FP_CalculaEfectivo(Session("ALMACEN"), Session("USUARIO"), Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Now.Year)
            dblMonto = CDbl(txtMonto.Text)
            'dblTipCam = Math.Round(objPagos.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Now.Year), 2)
            'dblTipCam = objPagos.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Now.Year).ToString("N2")
            'dblTipCam = objPagos.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Now.Year).ToString("N3") 'aotane 05.08.2013 Comment to TS-CCC
            dblTipCam = CDbl(ObtenerTipoCambioSAP(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))) 'Add TS-CCC

            If cboMoneda.SelectedValue = 2 Or cboMoneda.SelectedValue = 4 Then   'efectivo dolares o cheque dolares
                dblMonto = dblMonto * dblTipCam
            End If

            'If dblMonto > dblEfectivo And (dblMonto - dblEfectivo) > 1 And (cboMoneda.SelectedValue = 1 Or cboMoneda.SelectedValue = 2) Then  'solo se consistencia si es efectivo
            '    'Response.Write("<script>alert('La cifra ingresada es mayor a su disponible de efectivo')</script>")

            '    intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
            '          "", "", "", txtBolsa.Text, 0, 9, 0, 0, 0, 0, 0, 0, "")
            '    If intAutoriza = 1 Then
            '        Try
            '            ' se usa CDbl(txtMonto.Text) y no dblMonto porque debemos ingresar el monto original y no el convertido
            '            dblBuzon = obj.FP_InsertaCajaBuzon(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto.Text), cboMoneda.SelectedValue, txtBolsa.Text, dblTipCam, Session("NOMBRE_COMPLETO"))
            '            strURL = "EnvioBuzon.aspx?strCajero=" & Session("NOMBRE_COMPLETO") & "&strMonto=" & IIf(cboMoneda.SelectedValue = 1 Or cboMoneda.SelectedValue = 3, "S/.", "US$") & " " & Format(CDbl(txtMonto.Text), "######0.00") & "&strBolsa=" & txtBolsa.Text
            '            Response.Redirect(strURL)
            '        Catch ex As Exception
            '            Response.Write("<script>alert('EL NUMERO DE BOLSA YA HABIA SIDO INGRESADO')</script>")
            '        Finally
            '            txtMonto.Text = ""
            '        End Try
            '    Else
            '        Response.Write("<script>alert('Necesita autorización para enviar una cifra mayor a su disponible de efectivo')</script>")
            '    End If

            'Else
            Detalle(1, 1) = "OfVta"
            Detalle(1, 2) = Session("ALMACEN")
            Detalle(1, 3) = "Oficina de Venta"

            Detalle(2, 1) = "Usuario"
            Detalle(2, 2) = Session("USUARIO")
            Detalle(2, 3) = "Usuario"

            Detalle(3, 1) = "Monto"
            Detalle(3, 2) = CDbl(txtMonto.Text)
            Detalle(3, 3) = "Monto"

            Detalle(4, 1) = "Via"
            Detalle(4, 2) = cboMoneda.SelectedItem.Text
            Detalle(4, 3) = "Via"

            Detalle(5, 1) = "Sobre"
            Detalle(5, 2) = txtBolsa.Text
            Detalle(5, 3) = "Sobre"

            Detalle(6, 1) = "TipCam"
            Detalle(6, 2) = dblTipCam
            Detalle(6, 3) = "Tipo de Cambio"

            Detalle(7, 1) = "Cajero"
            Detalle(7, 2) = Session("NOMBRE_COMPLETO")
            Detalle(7, 3) = "Cajero"


            Try
                ' se usa CDbl(txtMonto.Text) y no dblMonto porque debemos ingresar el monto original y no el convertido
                'INICIATIVA-565 INI
                Dim fecha As DateTime
                fecha = CDate(txtFecha.Text + " " + Date.Now.ToString("hh:mm:ss tt"))
                dblBuzon = obj.FP_InsertaCajaBuzon(Session("ALMACEN"), Session("USUARIO"), CDbl(txtMonto.Text), cboMoneda.SelectedValue, txtBolsa.Text, dblTipCam, Session("NOMBRE_COMPLETO"), fecha)
                'INICIATIVA-565 INI FIN
                RegistrarAuditoria(String.Concat("Se grabo el sobre caja buzon : ", Session("ALMACEN"), ";", Session("USUARIO"), ";", CDbl(txtMonto.Text), ";", cboMoneda.SelectedValue, ";", txtBolsa.Text, ";", dblTipCam, ";", Session("NOMBRE_COMPLETO")))

                'objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                strURL = "EnvioBuzon.aspx?strCajero=" & Session("NOMBRE_COMPLETO") & "&strMonto=" & IIf(cboMoneda.SelectedValue = 1 Or cboMoneda.SelectedValue = 3, "S/", "US$") & " " & Format(CDbl(txtMonto.Text), "######0.00") & "&strBolsa=" & txtBolsa.Text & "&strFecha=" & txtFecha.Text 'INICIATIVA-565
                objFileLog.Log_WriteLog(pathFile, strArchivo, "strURL " & strURL)
                Response.Redirect(strURL)
            Catch ex As Exception
                wParam5 = 0
                wParam6 = "Error en " & wParam6 & "." & ex.Message
                objFileLog.Log_WriteLog(pathFile, strArchivo, "wParam6 " & wParam6)
                Response.Write("<script>alert('EL NUMERO DE BOLSA YA HABIA SIDO INGRESADO')</script>")
            Finally
                txtMonto.Text = ""
            End Try
            'objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            ' End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Exepcion  " & ex.Message)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "StackTrace  " & ex.StackTrace)
            If Err.Number = 13 Then
                Response.Write("<script>alert('La cifra ingresada no tiene el formato correcto')</script>")
            Else
                Response.Write("<script>alert('No se pudo registrar la operación. Intente nuevamente')</script>")
                'Response.Write("<script>alert('" & Err.Description & "')</script>")
            End If
        End Try


    End Sub

#End Region

#Region " ----- Funciones ----- "

    Private Function VerificarCierreCaja() As Boolean

        VerificarCierreCaja = True
        'INICIO - MODIFICADO POR TS_CCC

        'Dim objPagos As SAP_SIC_Pagos.clsValidar
        'Dim cajaBuzon, saldoInicial As Decimal
        'Dim cierreRealizado, Resultado, msgError As String

        'objPagos = New SAP_SIC_Pagos.clsValidar
        'objPagos.Get_ConsultaEstadoCaja(Session("ALMACEN"), Date.Today.ToString("dd/MM/yyyy"), Session("USUARIO"), Resultado, msgError, cajaBuzon, cierreRealizado, saldoInicial)
        'objPagos = Nothing

        'If Resultado <> "0" Then
        '    Response.Write(String.Concat("<script language=javascript>alert('Error: ", msgError, "');</script>"))
        'Else
        '    If cierreRealizado <> "N" Then
        '        Response.Write("<script language=javascript>alert('Caja Cerrada, no es posible crear un sobre');</script>")
        '    Else
        '        VerificarCierreCaja = True
        '    End If
        'End If

        Dim dsResultado As DataSet
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim strCierreCajaMensaje As String = String.Empty
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

        Dim asignacionCajeroMensaje = objOffline.VerificarAsignacionCajero(Session("ALMACEN"), codUsuario, Date.Today.ToString("dd/MM/yyyy"))
        If asignacionCajeroMensaje <> String.Empty Then
            Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", asignacionCajeroMensaje)
            Me.RegisterStartupScript("RegistraAlerta3", script)
            VerificarCierreCaja = False
            Exit Function
        End If

        dsResultado = objOffline.GetDatosAsignacionCajero(Session("ALMACEN"), Date.Today.ToString("dd/MM/yyyy"), codUsuario)
        If Not dsResultado Is Nothing Then
            For i As Int32 = 0 To dsResultado.Tables(0).Rows.Count - 1
                If dsResultado.Tables(0).Rows(i).Item("CAJA_CERRADA") = "S" Then
                    strCierreCajaMensaje = "Caja Cerrada, no es posible crear un sobre"
                    Dim script$ = String.Format("<script language=javascript>alert('{0}')</script>", strCierreCajaMensaje)
                    Me.RegisterStartupScript("RegistraAlerta", script)
                    VerificarCierreCaja = False
                    Exit For
                End If
            Next
        End If

        'FIN - MODIFICADO POR TS_CCC
        Return VerificarCierreCaja
    End Function

    'INICIO - MODIFICADO POR TS_CCC
    Public Function ObtenerTipoCambioSAP(ByVal strFecha As String) As String
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Return Format(objOffline.Obtener_TipoCambio(strFecha), "#######0.000")
    End Function
    'FIN - MODIFICADO POR TS_CCC

#End Region

#Region " ----- Metodos ----- "

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

            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsNuevoSobreCajaBuzon")
            Dim descTrans As String = StrLogAuditoria

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub

#End Region

End Class
