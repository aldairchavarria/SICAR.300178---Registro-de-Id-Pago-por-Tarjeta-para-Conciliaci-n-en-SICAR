Imports System.IO
Imports SisCajas.Funciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Activaciones
Imports COM_SIC_Adm_Cajas
Public Class SICAR_POSnoFinancieras
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents cboTipPost As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCodComercio As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodComercio As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblCodTerminal As System.Web.UI.WebControls.Label
    Protected WithEvents txtCodTerminal As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblRefVoucher As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefVoucher As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents lblTipoTransaccion As System.Web.UI.WebControls.Label
    Protected WithEvents cboTipoTransaccion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents loadDataHandler As System.Web.UI.WebControls.Button
    Protected WithEvents cmdEnviar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblTitOperacionesNoFinan As System.Web.UI.WebControls.Label
    Protected WithEvents HidEstTrans As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidOperacionVoucher As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTransAnu As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTransRIM As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cboTipPOS As System.Web.UI.WebControls.DropDownList
    Protected WithEvents HidTransMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidMonedaMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidUsuCodCaja As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidCodOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDesOpera As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblEnvioPos As System.Web.UI.WebControls.Label
    Protected WithEvents HidPtoVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosVisa As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoPosMC As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoMoneda As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIdCabez As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidDatoAuditPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidApliPOS As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidIntAutPos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipoTransAP As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents HidTipPagONF As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents PnPosNoFinanciera As System.Web.UI.WebControls.Panel
    Protected WithEvents HidTipoTran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidMsjCajero As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Dim objTransPos As New COM_SIC_Activaciones.clsTransaccionPOS
    Dim dsTiposTransaccion As DataSet
    'PROY-27440 INI
    Dim objFileLog As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    Dim strIdentifyLog As String
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        'Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
        If (Session("USUARIO") Is Nothing) Then

            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(nameFilePos, strArchivoPos, "Inicio de la pagina Operaciones No Financieras.")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Inicio Page_Load")

            If Not IsPostBack Then

                LlenarDatosCombos()
                load_data_param_pos()
                If Not validar_IntegracionPOS() Then
                    RegisterClientScriptBlock("f_ValidaSwitch", "<script language=jscript> f_ValidaSwitch(); </script>")
                    PnPosNoFinanciera.Visible = False

                End If
            End If
        End If

    End Sub

    Private Function validar_IntegracionPOS() As Boolean
        Dim bolResp As Boolean = True
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "validar_IntegracionPOS.")

        Dim integracionPOS As String
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "integracionPOS: " & integracionPOS)
        integracionPOS = HidIntAutPos.Value
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "integracionPOS:" & integracionPOS)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Inicio Validacion:")
        If integracionPOS <> "1" Then
            bolResp = False
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Inicio Validacion[Respuesta]:" & bolResp)
        End If
        Return bolResp
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Fin Validacion")
    End Function
    Private Sub LlenarDatosCombos()
        'Tipo de Transaccion 
        Dim row As DataRow
        Dim strCodRpta As String = ""
        Dim strMsgRpta As String = ""
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Inicio Llenar Datos Combos.")
        dsTiposTransaccion = objTransPos.ObtenerTiposTransaccion(strCodRpta, strMsgRpta)
        For Each oRecord As Object In dsTiposTransaccion.Tables(0).Rows
            If oRecord("SPARV_VALUE").ToString() <> ClsKeyPOS.strTipoTransPAG.ToString Then
                cboTipoTransaccion.Items.Add(New ListItem(oRecord("SPARV_VALUE2").ToString(), oRecord("SPARV_VALUE").ToString()))
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "cboTipoTransaccion[" & oRecord("SPARV_VALUE2").ToString() & "][" & oRecord("SPARV_VALUE").ToString() & "]")
            End If
        Next
        cboTipoTransaccion.Items.Insert(0, "--Seleccione--")

        'Tipo POS
        cboTipPOS.Items.Add(New ListItem(ClsKeyPOS.strTipoPosVI, ClsKeyPOS.strCodTarjetaVS))
        cboTipPOS.Items.Add(New ListItem(ClsKeyPOS.strTipoPosMC, ClsKeyPOS.strCodTarjetaMC))
        cboTipPOS.Items.Insert(0, "--Seleccione--")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "cboTipoTransaccion[" & ClsKeyPOS.strTipoPosVI & "][" & ClsKeyPOS.strCodTarjetaVS & "]")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "cboTipoTransaccion[" & ClsKeyPOS.strTipoPosMC & "][" & ClsKeyPOS.strCodTarjetaMC & "]")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "Fin Llenar Datos Combos.")
    End Sub

    Private Sub load_data_param_pos()

        Dim strMensaje As String = ClsKeyPOS.strIPMsjDesconfigurado
        Me.HidPtoVenta.Value = Funciones.CheckStr(Session("ALMACEN"))
        Dim strIpClient As String = Funciones.CheckStr(Session("IpLocal"))
        Me.HidOperacionVoucher.Value = String.Format("{0}{1}{2}", ClsKeyPOS.strTipoTransANU, "|", ClsKeyPOS.strTipoTransRIM)
        'CNH 2017-10-09 INI


        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : Validacion Integracion INI")
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : " & "HidPtoVenta : " & HidPtoVenta.Value)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : " & "strIpClient : " & strIpClient)


        Dim strCodRptaFlag As String = ""
        Dim strMsgRptaFlag As String = ""

        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS

        'INI CONSULTA INTEGRACION AUTOMATICO POS

        Dim strFlagIntAut As String = ""

        strCodRptaFlag = "" : strMsgRptaFlag = ""
        objConsultaPos.Obtener_Integracion_Auto(Funciones.CheckStr(Me.HidPtoVenta.Value), strIpClient, String.Empty, strFlagIntAut, strCodRptaFlag, strMsgRptaFlag)

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : " & "strFlagIntAut : " & Funciones.CheckStr(strFlagIntAut))
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : " & "strCodRptaFlag : " & Funciones.CheckStr(strCodRptaFlag))
        'INI PROY-140126
        Dim MaptPath As String
        MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
        MaptPath = "( Class : " & MaptPath & "; Function: load_data_param_pos)"
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : " & "strMsgRptaFlag : " & Funciones.CheckStr(strMsgRptaFlag) & MaptPath)
        'FIN PROY-140126

        Me.HidIntAutPos.Value = Funciones.CheckStr(strFlagIntAut)

        'FIN CONSULTA INTEGRACION AUTOMATICO POS
        'CNH 2017-10-09 FIN
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "SICAR POS NOFINANCIERAS - " & "load_data_param_pos : Validacion Integracion FIN")

        Me.HidTipPagONF.Value = ClsKeyPOS.strOpeNoFinan
        Me.HidEstTrans.Value = ClsKeyPOS.strEstTRanPen & "|" & ClsKeyPOS.strEstTRanPro _
            & "|" & ClsKeyPOS.strEstTRanAce & "|" & ClsKeyPOS.strEstTRanRec & "|" & ClsKeyPOS.strEstTRanInc
        Me.HidTipoOpera.Value = ClsKeyPOS.strOpeFina & "|" & ClsKeyPOS.strNoFina
        Me.HidTipoTransAnu.Value = ClsKeyPOS.strTipoTransANU
        Me.HidTipoTransRIM.Value = ClsKeyPOS.strTipoTransRIM
        Me.HidTipoTransAP.Value = ClsKeyPOS.strTipoTransAPP
        Me.HidTipoPOS.Value = ClsKeyPOS.strCodTarjetaVS & "|" & ClsKeyPOS.strCodTarjetaMC
        Me.HidTipoTran.Value = ClsKeyPOS.strTipoTransPAG & "|" & ClsKeyPOS.strTipoTransANU & "|" _
    & ClsKeyPOS.strTipoTransRIM & "|" & ClsKeyPOS.strTipoTransRDO & "|" & _
    ClsKeyPOS.strTipoTransRTO & "|" & ClsKeyPOS.strTipoTransAPP & "|" & ClsKeyPOS.strTipoTransCIP

        Me.HidTransMC.Value = ClsKeyPOS.strTranMC_Compra & "|" & ClsKeyPOS.strTranMC_Anulacion & "|" _
        & ClsKeyPOS.strTranMC_RepDetallado & _
        "|" & ClsKeyPOS.strTranMC_RepTotales & "|" & ClsKeyPOS.strTranMC_ReImpresion & _
        "|" & ClsKeyPOS.strTranMC_Cierre & "|" & ClsKeyPOS.strPwdComercio_MC

        Me.HidMonedaMC.Value = ClsKeyPOS.strMonedaMC_Soles & "|" & ClsKeyPOS.strMonedaMC_Dolares

        Me.HidMonedaVisa.Value = ClsKeyPOS.strMonedaVisa_Soles & "|" & ClsKeyPOS.strMonedaVisa_Dolares
        Me.hidUsuCodCaja.Value = Funciones.CheckStr(Session("USUARIO"))
        Me.HidTipoMoneda.Value = ClsKeyPOS.strTipoMonSoles
        Me.HidCodOpera.Value = ClsKeyPOS.strCodOpeVE & "|" & _
   ClsKeyPOS.strCodOpeVC & "|" & ClsKeyPOS.strCodOpeAN

        Me.HidDesOpera.Value = ClsKeyPOS.strDesOpeVE & "|" & _
        ClsKeyPOS.strDesOpeVC & "|" & ClsKeyPOS.strDesOpeAN
        Me.hidMsjCajero.Value = ClsKeyPOS.strMsjValidacionCajero
        'DATOS DEL POS
        Me.HidDatoPosVisa.Value = ""
        Me.HidDatoPosMC.Value = ""
        Me.HidDatoAuditPos.Value = Funciones.CheckStr(strIpClient) & "|" & _
  Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")) & "|" & _
  Funciones.CheckStr(Session("USUARIO"))

        Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
        'Dim strIp As String = CurrentTerminal()
        Dim strEstadoPos As String = "1"
        Dim strTipoVisa As String = "V"

        Dim ds As DataSet

        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, strArchivoPos & "load_data_param_pos : " & "strIpClient : " & strIpClient)
        'VISA INICIO
        strTipoVisa = "V"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "load_data_param_pos VS : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosVisa.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
        Else
            'strMensaje = "Ocurrió un error al tratar de recuperar los datos del POS. Comuníquese con soporte."
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            Response.End()
        End If

        'VISA FIN

        'MC INICIO
        strTipoVisa = "M"
        ds = objSicarDB.ConsultarDatosPOS(strIpClient, Me.HidPtoVenta.Value, strEstadoPos, strTipoVisa)
        objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "load_data_param_pos MC : " & "Count : " & Funciones.CheckStr(ds.Tables(0).Rows.Count))

        If ds.Tables(0).Rows.Count > 0 Then
            Me.HidDatoPosMC.Value = "POSN_ID=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSN_ID")) _
            & "|POSV_NROTIENDA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROTIENDA")) _
            & "|POSV_NROCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_NROCAJA")) _
            & "|POSV_IDESTABLEC=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IDESTABLEC")) _
            & "|POSV_EQUIPO=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_EQUIPO")) _
            & "|POSV_TIPO_POS=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_TIPO_POS")) _
            & "|POSV_COD_TERMINAL=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_COD_TERMINAL")) _
            & "|POSV_IPCAJA=" & Funciones.CheckStr(ds.Tables(0).Rows(0)("POSV_IPCAJA"))
        Else
            'strMensaje = "Ocurrió un error al tratar de recuperar los datos del POS. Comuníquese con soporte."
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            Response.End()
        End If
        Me.HidApliPOS.Value = ClsKeyPOS.strConstMC_POS
        'MC FIN
        If ds.Tables(0).Rows.Count > 0 Then
            Dim strTramaArrys As String() = HidDatoPosMC.Value.Split("|")
            Me.txtCodComercio.Value = strTramaArrys(1).Substring(strTramaArrys(1).IndexOf("=") + 1)
            Me.txtCodTerminal.Value = strTramaArrys(6).Substring(strTramaArrys(6).IndexOf("=") + 1)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "load_data_param_pos : " & "Fin : ")
        Else
            'strMensaje = "Ocurrió un error al tratar de recuperar los datos del POS. Comuníquese con soporte."
            Response.Write("<script>alert('" & strMensaje & "');</script>")
            Response.End()
        End If
    End Sub


End Class
