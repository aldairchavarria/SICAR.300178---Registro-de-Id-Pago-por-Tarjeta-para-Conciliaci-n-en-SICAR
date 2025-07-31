Imports System.Data.OracleClient

Public Class ParamOficina
    Inherits System.Web.UI.Page
    Dim i, intResultado As Integer
    Dim objConfig As New COM_SIC_Configura.clsConfigura
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents SelNomBanco As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SelTipMoneda1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SelTipMoneda2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CtaBanco1 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents CtaBanco2 As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCajaSol As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTolSol As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCajaDolar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTolDolar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents chkFecha As System.Web.UI.WebControls.CheckBox
    Protected WithEvents SelNomBanco1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SelNomBanco2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtOrgVenta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCanal As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCanalDes As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtSector As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtOfic As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtOficDes As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents chkImpSAP As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtCtaSol As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCtaDolar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim orConector As New OracleConnection
        'Dim orCmdBancos As New OracleCommand
        'Dim orCmdMoneda As New OracleCommand
        'Dim orCmdDatGeneral As New OracleCommand

        'Dim orDABanco As New OracleDataAdapter
        'Dim orDAMoneda As New OracleDataAdapter
        'Dim orDADGeneral As New OracleDataAdapter
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim dsBancos As New DataSet
            Dim dsTipMoneda As New DataSet
            Dim dsDatGenerales As New DataSet

            If Not Page.IsPostBack Then

                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()

                'orCmdBancos.Connection = orConector
                'orCmdBancos.Parameters.Add("K_CUR_LISTBANC", OracleType.Cursor)
                'orCmdBancos.Parameters(0).Direction = ParameterDirection.Output

                'orCmdBancos.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_BANC"
                'orCmdBancos.CommandType = CommandType.StoredProcedure

                'orDABanco.SelectCommand = orCmdBancos
                'orDABanco.Fill(dsBancos)
                dsBancos = objConfig.FP_Lista_Bancos()

                SelNomBanco1.DataSource = dsBancos.Tables(0)
                SelNomBanco1.DataValueField = "ID_CONFBANC"
                SelNomBanco1.DataTextField = "BANC_DESC"
                SelNomBanco1.DataBind()
                SelNomBanco1.Items.Insert(0, New ListItem(" -- Seleccione -- ", ""))

                SelNomBanco2.DataSource = dsBancos.Tables(0)
                SelNomBanco2.DataValueField = "ID_CONFBANC"
                SelNomBanco2.DataTextField = "BANC_DESC"
                SelNomBanco2.DataBind()
                SelNomBanco2.Items.Insert(0, New ListItem(" -- Seleccione -- ", ""))
                ''------------
                'orCmdMoneda.Connection = orConector
                'orCmdMoneda.Parameters.Add("K_CUR_LISTMONEDA", OracleType.Cursor)
                'orCmdMoneda.Parameters(0).Direction = ParameterDirection.Output

                'orCmdMoneda.CommandText = "CONF_PARAMETROS_CAJA.CONF_TIPO_MONEDA"
                'orCmdMoneda.CommandType = CommandType.StoredProcedure

                'orDAMoneda.SelectCommand = orCmdMoneda
                'orDAMoneda.Fill(dsTipMoneda)
                dsTipMoneda = objConfig.FP_Lista_Moneda()

                SelTipMoneda1.DataSource = dsTipMoneda.Tables(0)
                SelTipMoneda1.DataValueField = "ID_CONFTIP_MONEDA"
                SelTipMoneda1.DataTextField = "MOND_DESC"
                SelTipMoneda1.DataBind()
                SelTipMoneda1.Items.Insert(0, New ListItem(" -- Seleccione -- ", ""))

                SelTipMoneda2.DataSource = dsTipMoneda.Tables(0)
                SelTipMoneda2.DataValueField = "ID_CONFTIP_MONEDA"
                SelTipMoneda2.DataTextField = "MOND_DESC"
                SelTipMoneda2.DataBind()
                SelTipMoneda2.Items.Insert(0, New ListItem(" -- Seleccione -- ", ""))
                ''------------
                'orCmdDatGeneral.Connection = orConector
                'orCmdDatGeneral.Parameters.Add("K_CODCANAL", OracleType.VarChar, 5)
                'orCmdDatGeneral.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
                'orCmdDatGeneral.Parameters.Add("K_APLIC_COD", OracleType.Number)
                'orCmdDatGeneral.Parameters.Add("K_CUR_LISTPARAMOFIC", OracleType.Cursor)
                'orCmdDatGeneral.Parameters(0).Value = Session("CANAL")
                'orCmdDatGeneral.Parameters(1).Value = Session("ALMACEN")
                'orCmdDatGeneral.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")
                'orCmdDatGeneral.Parameters(0).Direction = ParameterDirection.Input
                'orCmdDatGeneral.Parameters(1).Direction = ParameterDirection.Input
                'orCmdDatGeneral.Parameters(2).Direction = ParameterDirection.Input
                'orCmdDatGeneral.Parameters(3).Direction = ParameterDirection.Output

                'orCmdDatGeneral.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_PARAM_OFICINA"
                'orCmdDatGeneral.CommandType = CommandType.StoredProcedure

                'orDADGeneral.SelectCommand = orCmdDatGeneral
                'orDADGeneral.Fill(dsDatGenerales)
                dsDatGenerales = objConfig.FP_Lista_Param_Oficina(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"))

                txtOrgVenta.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("COD_ORG_VENTA") Is Nothing, "", dsDatGenerales.Tables(0).Rows(0).Item("COD_ORG_VENTA"))
                txtCanal.Value = dsDatGenerales.Tables(0).Rows(0).Item("COD_CANAL")
                txtCanalDes.Value = dsDatGenerales.Tables(0).Rows(0).Item("CANAL_DESC")
                txtSector.Value = dsDatGenerales.Tables(0).Rows(0).Item("SECTOR_DESC")
                txtOfic.Value = dsDatGenerales.Tables(0).Rows(0).Item("COD_PDV")
                txtOficDes.Value = dsDatGenerales.Tables(0).Rows(0).Item("PDV_DESC")

                txtCajaSol.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_MAX_DISP_SOL"))
                txtCajaDolar.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_MAX_DISP_DOL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_MAX_DISP_DOL"))
                txtTolSol.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_SOL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_SOL"))
                txtTolDolar.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_DOL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TOLERANCIA_DOL"))
                txtCtaSol.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_CUENTA_MON_NACIONAL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_CUENTA_MON_NACIONAL"))
                txtCtaDolar.Value = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_CUENTA_MON_EXTRAN") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_CUENTA_MON_EXTRAN"))

                If CInt(Trim(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_VALIDA_CAMBIO_FECH"))) = 1 Then
                    chkFecha.Checked = True
                End If
                If CInt(Trim(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_VALIDA_IMP_SAP"))) = 1 Then
                    chkImpSAP.Checked = True
                End If
                SelNomBanco1.SelectedValue = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_BANC_MON_NACIONAL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_BANC_MON_NACIONAL"))
                SelTipMoneda1.SelectedValue = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TIPO_MON_NACIONAL") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TIPO_MON_NACIONAL"))
                SelNomBanco2.SelectedValue = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_BANCO_MON_EXTRAN") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_BANCO_MON_EXTRAN"))
                SelTipMoneda2.SelectedValue = IIf(dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TIPO_MON_EXTRAN") Is DBNull.Value, "", dsDatGenerales.Tables(0).Rows(0).Item("CAJA_TIPO_MON_EXTRAN"))

        End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick

        Dim intValCamFec As Integer
        Dim intValImpSap As Integer

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
        Dim Detalle(22, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria


        If SelNomBanco1.SelectedValue = "" Then
            Response.Write("<script>alert('Seleccione el banco asociado')</script>")
            Exit Sub
        ElseIf SelTipMoneda1.SelectedValue = "" Then
            Response.Write("<script>alert('Ingrese el tipo de moneda')</script>")
            Exit Sub
        ElseIf Trim(txtCtaSol.Value) = "" Then
            Response.Write("<script>alert('Ingrese el número de cuenta asociada')</script>")
            Exit Sub
        ElseIf SelNomBanco2.SelectedValue = "" Then
            Response.Write("<script>alert('Seleccione el banco asociado')</script>")
            Exit Sub
        ElseIf Trim(SelTipMoneda2.SelectedValue) = "" Then
            Response.Write("<script>alert('Ingrese el tipo de moneda asociada')</script>")
            Exit Sub
        ElseIf Trim(txtCtaDolar.Value) = "" Then
            Response.Write("<script>alert('Ingrese el número de cuenta asociada')</script>")
            Exit Sub
        ElseIf (SelTipMoneda1.SelectedValue = SelTipMoneda2.SelectedValue) Then
            Response.Write("<script>alert('Verifique las monedas configuradas')</script>")
            Exit Sub
        ElseIf (txtCtaSol.Value = txtCtaDolar.Value) Then
            Response.Write("<script>alert('Verifique las cuentas ingresadas')</script>")
            Exit Sub
        ElseIf Trim(txtCajaSol.Value) = "" Then
            Response.Write("<script>alert('Ingrese el limite máximo en soles')</script>")
            Exit Sub
        ElseIf Trim(txtTolSol.Value) = "" Then
            Response.Write("<script>alert('Ingrese la tolerancia en soles')</script>")
            Exit Sub
        ElseIf Trim(txtCajaDolar.Value) = "" Then
            Response.Write("<script>alert('Ingrese el limite máximo en dolares')</script>")
            Exit Sub
        ElseIf Trim(txtTolDolar.Value) = "" Then
            Response.Write("<script>alert('Ingrese la tolerancia en dolares')</script>")
            Exit Sub
        End If

        'Dim orConector As New OracleConnection
        'Dim orCmdActCaja As New OracleCommand
        'Dim orDAActCaja As New OracleDataAdapter
        'Dim dsActCaja As New DataSet

        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()

        'orCmdActCaja.Connection = orConector
        'orCmdActCaja.Parameters.Add("K_COD_CANAL", OracleType.VarChar, 5)
        'orCmdActCaja.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
        'orCmdActCaja.Parameters.Add("K_APLIC_COD", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_COD_ORGVTA", OracleType.VarChar, 5)
        'orCmdActCaja.Parameters.Add("K_CANAL_DESC", OracleType.VarChar, 100)
        'orCmdActCaja.Parameters.Add("K_PDV_DESC", OracleType.VarChar, 100)
        'orCmdActCaja.Parameters.Add("K_SECTOR_DESC", OracleType.VarChar, 5)
        'orCmdActCaja.Parameters.Add("K_MON_NACIONAL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_BANC_MON_NAC", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_CTA_MON_NAC", OracleType.VarChar, 20)
        'orCmdActCaja.Parameters.Add("K_MON_EXTRAN", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_BANC_MON_EXTRAN", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_CTA_MON_EXTRAN", OracleType.VarChar, 20)
        'orCmdActCaja.Parameters.Add("K_EFEC_SOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_MAX_SOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_TOL_SOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_EFE_DOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_MAX_DOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_TOL_DOL", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_VALIDA_CAMBIO_FECHA", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_VALIDA_IMP_SAP", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_ESTADO", OracleType.Number)
        'orCmdActCaja.Parameters.Add("K_RETVAL", OracleType.Number)

        'orCmdActCaja.Parameters(0).Value = Session("CANAL")
        'orCmdActCaja.Parameters(1).Value = Session("ALMACEN")
        'orCmdActCaja.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")

        'orCmdActCaja.Parameters(3).Value = txtOrgVenta.Value
        'orCmdActCaja.Parameters(4).Value = txtCanalDes.Value
        'orCmdActCaja.Parameters(5).Value = txtOficDes.Value
        'orCmdActCaja.Parameters(6).Value = txtSector.Value
        'orCmdActCaja.Parameters(7).Value = SelTipMoneda1.SelectedValue
        'orCmdActCaja.Parameters(8).Value = SelNomBanco1.SelectedValue
        'orCmdActCaja.Parameters(9).Value = txtCtaSol.Value
        'orCmdActCaja.Parameters(10).Value = SelTipMoneda2.SelectedValue
        'orCmdActCaja.Parameters(11).Value = SelNomBanco2.SelectedValue
        'orCmdActCaja.Parameters(12).Value = txtCtaDolar.Value
        'orCmdActCaja.Parameters(13).Value = txtCajaSol.Value
        'orCmdActCaja.Parameters(14).Value = txtCajaSol.Value
        'orCmdActCaja.Parameters(15).Value = txtTolSol.Value
        'orCmdActCaja.Parameters(16).Value = txtCajaDolar.Value
        'orCmdActCaja.Parameters(17).Value = txtCajaDolar.Value
        'orCmdActCaja.Parameters(18).Value = txtTolDolar.Value
        'If chkFecha.Checked = True Then
        '    orCmdActCaja.Parameters(19).Value = 1
        'Else
        '    orCmdActCaja.Parameters(19).Value = 2
        'End If
        'If chkImpSAP.Checked = True Then
        '    orCmdActCaja.Parameters(20).Value = 1
        'Else
        '    orCmdActCaja.Parameters(20).Value = 2
        'End If
        'orCmdActCaja.Parameters(21).Value = 1
        'For i = 0 To 21
        '    orCmdActCaja.Parameters(i).Direction = ParameterDirection.Input
        'Next
        'orCmdActCaja.Parameters(22).Direction = ParameterDirection.Output

        'orCmdActCaja.CommandText = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_PARAM_OFICINA"
        'orCmdActCaja.CommandType = CommandType.StoredProcedure

        'intResultado = orCmdActCaja.ExecuteNonQuery

        If chkFecha.Checked = True Then
            intValCamFec = 1
        Else
            intValCamFec = 2
        End If
        If chkImpSAP.Checked = True Then
            intValImpSap = 1
        Else
            intValImpSap = 2
        End If

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcMPDV")
        wParam5 = 1
        wParam6 = "Parámetros de Oficina"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtMPDV")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "Canal"
        Detalle(1, 2) = Session("CANAL")
        Detalle(1, 3) = "Canal"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = Session("ALMACEN")
        Detalle(2, 3) = "Oficina de Venta"

        Detalle(3, 1) = "CodApl"
        Detalle(3, 2) = ConfigurationSettings.AppSettings("codAplicacion")
        Detalle(3, 3) = "Codigo de Aplicacion"

        Detalle(4, 1) = "OrgVta"
        Detalle(4, 2) = txtOrgVenta.Value
        Detalle(4, 3) = "Organizacion de Venta"

        Detalle(5, 1) = "CanalDes"
        Detalle(5, 2) = txtCanalDes.Value
        Detalle(5, 3) = "Descripcion de Canal"

        Detalle(6, 1) = "OfDes"
        Detalle(6, 2) = txtOficDes.Value
        Detalle(6, 3) = "Descripcion de PDV"

        Detalle(7, 1) = "Sector"
        Detalle(7, 2) = txtSector.Value
        Detalle(7, 3) = "Sector"

        Detalle(8, 1) = "TipMon1"
        Detalle(8, 2) = SelTipMoneda1.SelectedValue
        Detalle(8, 3) = "Tipo de Moneda 1"

        Detalle(9, 1) = "Banco 1"
        Detalle(9, 2) = SelNomBanco1.SelectedValue
        Detalle(9, 3) = "Banco 1"

        Detalle(10, 1) = "CuentaSol"
        Detalle(10, 2) = txtCtaSol.Value
        Detalle(10, 3) = "Cuenta de Soles"

        Detalle(11, 1) = "TipMon2"
        Detalle(11, 2) = SelTipMoneda2.SelectedValue
        Detalle(11, 3) = "Tipo de Moneda 2"

        Detalle(12, 1) = "Banco 2"
        Detalle(12, 2) = SelNomBanco2.SelectedValue
        Detalle(12, 3) = "Banco 2"

        Detalle(13, 1) = "IdBin"
        Detalle(13, 2) = txtCtaDolar.Value
        Detalle(13, 3) = "Id de BIN"

        Detalle(14, 1) = "EfecSol"
        Detalle(14, 2) = txtCajaSol.Value
        Detalle(14, 3) = "Efectivo en Soles"

        Detalle(15, 1) = "MaxSol"
        Detalle(15, 2) = txtCajaSol.Value
        Detalle(15, 3) = "Maximo disponible en soles"

        Detalle(16, 1) = "TolSol"
        Detalle(16, 2) = txtTolSol.Value
        Detalle(16, 3) = "Tolerancia en Soles"

        Detalle(17, 1) = "EfecDol"
        Detalle(17, 2) = txtCajaDolar.Value
        Detalle(17, 3) = "Efectivo en Soles"

        Detalle(18, 1) = "MaxDol"
        Detalle(18, 2) = txtCajaDolar.Value
        Detalle(18, 3) = "Maximo disponible en dolares"

        Detalle(19, 1) = "TolDol"
        Detalle(19, 2) = txtTolDolar.Value
        Detalle(19, 3) = "Tolerancia en Dolares"

        Detalle(20, 1) = "ValCamFec"
        Detalle(20, 2) = intValCamFec
        Detalle(20, 3) = "Cambio de Fecha"

        Detalle(21, 1) = "ValImpSap"
        Detalle(21, 2) = intValImpSap
        Detalle(21, 3) = "Impresion por SAP"

        Detalle(22, 1) = "Estado"
        Detalle(22, 2) = 1
        Detalle(22, 3) = "Estado"


        'FIN DE AUDITORIA


        intResultado = objConfig.FP_Actualiza_Param_Oficina(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), txtOrgVenta.Value, _
                       txtCanalDes.Value, txtOficDes.Value, txtSector.Value, SelTipMoneda1.SelectedValue, SelNomBanco1.SelectedValue, txtCtaSol.Value, SelTipMoneda2.SelectedValue, SelNomBanco2.SelectedValue, _
                       txtCtaDolar.Value, txtCajaSol.Value, txtCajaSol.Value, txtTolSol.Value, txtCajaDolar.Value, txtCajaDolar.Value, txtTolDolar.Value, intValCamFec, intValImpSap, 1)

        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)


    End Sub
End Class
