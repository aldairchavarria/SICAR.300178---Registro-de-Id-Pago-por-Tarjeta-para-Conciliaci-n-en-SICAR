Public Class grdDocumentos
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgrRecauda As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdAnular As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtNroTransac As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFechaTran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnBinAdquiriente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnCodComercio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents intCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnRutaLog As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnDetalleLog As System.Web.UI.HtmlControls.HtmlInputHidden
	Protected WithEvents hidEstadoTransActual As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents dwTipoPaginas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hidFecTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidNombreCliente As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMonPago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdFormaPago As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hidEstadoSicar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdEditar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

	Public objFileLog As New SICAR_Log
	Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
	Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
	Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdentifyLog As String 'PROY-27440 


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                If Not Request.QueryString("fecbus") Is Nothing AndAlso Request.QueryString("fecbus").Length > 0 Then
                    txtFecha.Value = Request.QueryString("fecbus")
                Else
					If txtFecha.Value = String.Empty Then
						txtFecha.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")				   'Now.ToString("d")
					End If
                End If
                LeeParametros()
                Buscar()
        End If
        End If
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick

        Buscar()

    End Sub

    Private Sub Buscar()

        Dim strOficina As String = Session("ALMACEN")

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
        Dim Detalle(2, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPRP")
        wParam5 = 1
        wParam6 = "Pool de Recaudaciones Procesadas"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPPrc")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "Fecha"
        Detalle(1, 2) = txtFecha.Value
        Detalle(1, 3) = "Fecha"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = strOficina
        Detalle(2, 3) = "Oficina venta"

        
        'FIN AUDITORIA

        Try
            Me.txtFechaTran.Value = txtFecha.Value
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim intSAP = objOffline.Get_ConsultaSAP
            Dim objSAPRecau As Object

            'CAMBIADO POR FFS INICIO
            'If intSAP = 1 Then
            'objSAPRecau = New SAP_SIC_Recaudacion.clsRecaudacion
            'Else
            'objSAPRecau = New COM_SIC_OffLine.clsOffline
            'End If

            '--trae todos, incluidos los anulados
            'Dim dsResult As DataSet = objSAPRecau.Get_PoolRecaudacion(txtFecha.Value, strOficina, String.Empty, String.Empty, String.Empty, "1")    '//Estado =

            'AGREGADO POR FFS INICIO
            Dim StrTipo As String = "00"

            If dwTipoPaginas.SelectedValue = "00" Then
                StrTipo = "T" 'Todos
            ElseIf dwTipoPaginas.SelectedValue = "01" Then
                StrTipo = "M" 'Movil
            ElseIf dwTipoPaginas.SelectedValue = "02" Then
                StrTipo = "F" ' Fijo y Paginas
            End If
            'AGREGADO POR FFS FIN
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

            Dim dsResult As DataSet = objOffline.GetPoolRecaudacion(txtFecha.Value, strOficina, StrTipo, Session("USUARIO"))  '//Estado =

            'COMENTADO POR FFS INICIO(YA NO APLICARIA PORQ ESTA ESTA MODO DESCONECTADO)
            '--trae todos, incluidos los anulados
            'nhuaringa auto hdnUsuario

            'Dim codigoCaj As String = "0000000000" & hdnUsuario.Value.ToString.Trim
            'codigoCaj = codigoCaj.Substring(codigoCaj.Length - 10, 10)

            'Dim dsResult As DataSet = objSAPRecau.Get_PoolRecaudacion(codigoCaj, txtFecha.Value, strOficina, String.Empty, String.Empty, String.Empty, "1")  '//Estado =
            'Me.dgrRecauda.DataSource = dsResult.Tables(0)
            'Me.dgrRecauda.DataBind()
            'COMENTADO POR FFS FIN
			
			 'CAMBIADO POR FFS FIN

            Me.dgrRecauda.DataSource = dsResult.Tables(0)
            Me.dgrRecauda.DataBind()

			

            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No existen documentos para la fecha indicada'); </script>")
            End If
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            wParam5 = 0
            wParam6 = "Error en Pool de Recaudaciones Procesadas." & ex.Message
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        End Try

    End Sub

    Private Sub LeeParametros()
        Dim cteCODIGO_CANAL As String = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        Dim cteCODIGO_BINADQUIRIENTE As String = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE")
        Dim cteCODIGO_RUTALOG As String = ConfigurationSettings.AppSettings("cteCODIGO_RUTALOG")
        Dim cteCODIGO_DETALLELOG As String = ConfigurationSettings.AppSettings("cteCODIGO_DETALLELOG")

        Me.hdnPuntoDeVenta.Value = Session("ALMACEN")
        Me.intCanal.Value = cteCODIGO_CANAL
        Me.hdnUsuario.Value = Session("USUARIO")
        Me.hdnBinAdquiriente.Value = Session("ALMACEN") 
        Me.hdnCodComercio.Value = Session("ALMACEN")
        Me.hdnRutaLog.Value = cteCODIGO_RUTALOG
        Me.hdnDetalleLog.Value = cteCODIGO_DETALLELOG
				hidEstadoTransActual.Value = String.Empty '//E75810
				Me.Session("PAGINA_INICIAL") = "grdDocumentos.aspx" '//E75810
        hidEstadoSicar.Value = String.Empty 'INICIATIVA-529
        'PROY-27440 INI
objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FORMA_PAGO[VALIDACION]")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        '------
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "HidPtoVenta : " & Me.hdnPuntoDeVenta.Value)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.hdnPuntoDeVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: LeeParametros)"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126


        'FIN CONSULTA INTEGRACION AUTOMATICO POS
     
        Dim strIntegracionPOS As String = Funciones.CheckStr(strFlagIntAut)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FORMA_PAGO[strIntegracionPOS]" & strIntegracionPOS)
        If Not strIntegracionPOS.Equals("1") Then
            cmdFormaPago.Visible = False
            cmdFormaPago.Disabled = True
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FORMA_PAGO[FIN]")
        'PROY-27440 FIN
    End Sub

	Private Sub cmdAnular_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.ServerClick

        ' INICIATIVA-529
        If hidEstadoTransActual.Value = "ERROR DE PROCESO" Or hidEstadoSicar.Value = "ANULADA" Then
            Response.Write("<SCRIPT> alert('" + "El Documento no se puede anular" + "');</SCRIPT>")
            Exit Sub
        End If
        '***** INI :: Valida la asignacion de caja *****
        'Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim strPedido As String
        Dim strFecha As String
        Dim strNombre As String
        Dim strMontoPago As String
        Dim strFechaTrans As Date
        'CNH 2017-10-09 INI
        Dim strIntegracionPOS As String = ""
        ' INI 27440
'PROY-27440 - INI
        Dim strPtoVenta As String = Funciones.CheckStr(Session("ALMACEN"))
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strPtoVenta : " & strPtoVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(strPtoVenta), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_ServerClick)"
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126


        strIntegracionPOS = Funciones.CheckStr(strFlagIntAut)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "load_data_param_pos : Validacion Integracion FIN")
        'FIN CONSULTA INTEGRACION AUTOMATICO POS

        'CNH 2017-10-09 FIN
        'Dim dsCajeroA As DataSet
        strFechaTrans = Convert.ToDateTime(hidFecTrans.Value).ToShortDateString
        strIdentifyLog = txtNroTransac.Value
        strPedido = txtNroTransac.Value
        strNombre = hidNombreCliente.Value
        strMontoPago = hidMonPago.Value
        strFecha = txtFecha.Value

        Dim strEstadoTrans = hidEstadoTransActual.Value
     
    If strIntegracionPOS = "1" And strFechaTrans = DateTime.Today.ToShortDateString() Then

      'CNH-INI
      '************************************************************************************
      Dim intAutoriza As Integer
      Dim objConf As New COM_SIC_Configura.clsConfigura

      intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), _
      Session("ALMACEN"), _
      ConfigurationSettings.AppSettings("codAplicacion"), _
      Session("USUARIO"), _
      Session("NOMBRE_COMPLETO"), "", "", _
            "", "", "", Me.txtNroTransac.Value, 0, 10, 0, 0, 0, 0, 0, 0, "")
      objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "intAutoriza: " & intAutoriza.ToString())
      If intAutoriza = 1 Then
        Response.Redirect("SICAR_FormaPagos.aspx?pacc=" & "1" & "&pid=" & strPedido & "&pfecha=" & strFecha & "&pnombre=" & strNombre & "&pmonto=" & strMontoPago & "&es=" & strEstadoTrans)
      Else
        Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
      End If
      '************************************************************************************

    Else
            strIdentifyLog = Me.hdnPuntoDeVenta.Value & "|" & Me.txtNroTransac.Value
'PROY-27440 -FIN        
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio cmdAnular_ServerClick")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "USUARIO: " & Session("USUARIO"))
        'Try
        Dim strResult As String
        Dim obAnul As New COM_SIC_Recaudacion.clsAnulaciones
        Dim intAutoriza As Integer
        Dim objConf As New COM_SIC_Configura.clsConfigura

        intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
              "", "", "", Me.txtNroTransac.Value, 0, 10, 0, 0, 0, 0, 0, 0, "")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "intAutoriza: " & intAutoriza.ToString())

        If intAutoriza = 1 Then
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "INP txtNroTransac: " & Me.txtNroTransac.Value)

            strResult = obAnul.AnularPago(ConfigurationSettings.AppSettings("CodAplicacion"), Session("CANAL"), Me.hdnRutaLog.Value, _
                Me.hdnDetalleLog.Value, _
                Me.hdnPuntoDeVenta.Value, _
                Me.intCanal.Value, _
                Me.hdnBinAdquiriente.Value, _
                Me.hdnCodComercio.Value, _
                Me.hdnUsuario.Value, _
                Me.txtNroTransac.Value)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT strResult: " & strResult)

            Dim arrMensaje() As String = strResult.Split("@")
            If ExisteError(arrMensaje) Then
                Response.Write("<SCRIPT> alert('" + arrMensaje(1) + "');</SCRIPT>")
            Else
                Response.Write("<SCRIPT> alert('Se procesó exitosamente la anulación.'); document.location='grdDocumentos.aspx';</SCRIPT>")
            End If
        Else
            Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
        End If
        'Catch ex As Exception
        '    Response.Write("<SCRIPT> alert('" + ex.Message + "');</SCRIPT>")
        'End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin cmdAnular_ServerClick")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        End If 'PROY-27440 
	End Sub

	Private Function ExisteError(ByVal arrMensaje() As String) As Boolean
		Dim bresult As Boolean = True

		If arrMensaje(0).Trim().Length > 0 Then
			If IsNumeric(arrMensaje(0).Trim()) Then
				If Integer.Parse(arrMensaje(0).Trim()) = 0 Then
					bresult = False
				End If
			End If
		End If

		Return bresult
	End Function

'proy - 27440
    Private Sub cmdFormaPago_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormaPago.ServerClick
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio cmdFormaPago_ServerClick")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        '***** INI :: Valida la asignacion de caja *****
        Dim objOfflineCaja As New COM_SIC_OffLine.clsOffline
        Dim strPedido As String
        Dim strFecha As String
        Dim strNombre As String
        Dim strMontoPago As String
        Dim strFechaTrans As Date
        Dim dsCajeroA As DataSet
        strFechaTrans = hidFecTrans.Value
        strIdentifyLog = txtNroTransac.Value
        strPedido = txtNroTransac.Value
        strNombre = hidNombreCliente.Value
        strMontoPago = hidMonPago.Value
        strFecha = txtFecha.Value
        Dim sFecha As String = Convert.ToDateTime(txtFecha.Value).ToLocalTime.ToShortDateString
        Dim sCajero As String = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")
        Dim sOficina As String = Funciones.CheckStr(Session("ALMACEN"))
        Dim strEstadoTrans = hidEstadoTransActual.Value
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin cmdFormaPago_ServerClick[Redirect]")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Redirect(" & "SICAR_FormaPagos.aspx?pacc=" & "2" & "&pid=" & strPedido & "&pfecha=" & strFecha & "&pnombre=" & strNombre & "&pmonto=" & strMontoPago & "&es=" & strEstadoTrans & ")")
        Response.Redirect("SICAR_FormaPagos.aspx?pacc=" & "2" & "&pid=" & strPedido & "&pfecha=" & strFecha & "&pnombre=" & strNombre & "&pmonto=" & strMontoPago & "&es=" & strEstadoTrans)

    End Sub

    'INICIATIVA - 529 INI
    Private Sub cmdEditar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditar.ServerClick
        Response.Redirect("conDocumentos.aspx?act=2&num=" + txtNroTransac.Value + "&fec=" + hidFecTrans.Value + "&est=" + hidEstadoTransActual.Value + "&accion=E")
    End Sub
    'INICIATIVA - 529 FIN
End Class
