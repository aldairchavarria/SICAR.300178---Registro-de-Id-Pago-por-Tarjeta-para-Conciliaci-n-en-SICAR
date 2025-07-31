Imports System.Data.OracleClient
Public Class ConfTransaccion
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGTransaccion As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdGrabar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdNuevo As System.Web.UI.HtmlControls.HtmlInputText

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Dim chkControl As New CheckBox
    Dim i As Integer
    Dim objConfig As New COM_SIC_Configura.clsConfigura
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            'Dim orConector As New OracleConnection
            'Dim orComLT As New OracleCommand
            'Dim orComTR As New OracleCommand

            'Dim orDataAdLT As New OracleDataAdapter
            'Dim orDataATR As New OracleDataAdapter

            Dim dsDataLT As New DataSet
            'Dim dsDataTR As New DataSet

            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()
                'orComLT.Connection = orConector
                'orComLT.Parameters.Add("K_CODCANAL", OracleType.VarChar, 5)
                'orComLT.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
                'orComLT.Parameters.Add("K_APLIC_COD", OracleType.Number)
                'orComLT.Parameters.Add("K_CUR_LISTTRAN", OracleType.Cursor)
                'orComLT.Parameters(0).Value = Session("CANAL")
                'orComLT.Parameters(1).Value = Session("ALMACEN")
                'orComLT.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")
                'orComLT.Parameters(0).Direction = ParameterDirection.Input
                'orComLT.Parameters(1).Direction = ParameterDirection.Input
                'orComLT.Parameters(2).Direction = ParameterDirection.Input
                'orComLT.Parameters(3).Direction = ParameterDirection.Output
                'orComLT.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_TRANSAC"
                'orComLT.CommandType = CommandType.StoredProcedure
                'orDataAdLT.SelectCommand = orComLT
                'orDataAdLT.Fill(dsDataLT)
                dsDataLT = objConfig.FP_Lista_Transaccion(Session("CANAL"), ConfigurationSettings.AppSettings("codAplicacion"), Session("ALMACEN"))

                DGTransaccion.DataSource = dsDataLT.Tables(0)
                DGTransaccion.DataBind()
        End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        'Dim orConector As New OracleConnection
        'Dim orComdElim As New OracleCommand
        'Dim orComdAct As New OracleCommand

        'Dim orDAElim As New OracleDataAdapter
        'Dim orDAAct As New OracleDataAdapter

        'Dim dsEliminar As New DataSet
        'Dim dsActualiza As New DataSet
        Dim intResultado As Integer

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
        Dim Detalle(4, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPtrx")
        wParam5 = 1
        wParam6 = "Permisos por Transaccion"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPTRX")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        'FIN DE AUDITORIA


        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()

        ''---- ELIMINANDO LOS REGISTROS ACTUALES
        'orComdElim.Connection = orConector
        'orComdElim.Parameters.Add("K_COD_CANAL", OracleType.VarChar, 5)
        'orComdElim.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
        'orComdElim.Parameters.Add("K_APLIC_COD", OracleType.Number)
        'orComdElim.Parameters.Add("K_RETVAL", OracleType.Number)
        'orComdElim.Parameters(0).Direction = ParameterDirection.Input
        'orComdElim.Parameters(1).Direction = ParameterDirection.Input
        'orComdElim.Parameters(2).Direction = ParameterDirection.Input
        'orComdElim.Parameters(3).Direction = ParameterDirection.Output
        'orComdElim.Parameters(0).Value = CStr(Session("CANAL"))
        'orComdElim.Parameters(1).Value = CStr(Session("ALMACEN"))
        'orComdElim.Parameters(2).Value = CInt(ConfigurationSettings.AppSettings("codAplicacion"))
        'orComdElim.CommandText = "CONF_PARAMETROS_CAJA.CONF_LIM_TRANSAC_RESTRINGIDA"
        'orComdElim.CommandType = CommandType.StoredProcedure
        'intResultado = orComdElim.ExecuteNonQuery
        intResultado = objConfig.FP_Elimina_Transac_Rest(CStr(Session("CANAL")), CInt(ConfigurationSettings.AppSettings("codAplicacion")), CStr(Session("ALMACEN")))
        '---- INGRESANDO LOS NUEVOS REGISTROS
        For i = 0 To DGTransaccion.Items.Count - 1
            chkControl = DGTransaccion.Items(i).FindControl("CheckBox1")
            If chkControl.Checked Then
                'orComdAct.Connection = orConector
                'orComdAct.Parameters.Add("K_COD_CANAL", OracleType.VarChar, 5)
                'orComdAct.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
                'orComdAct.Parameters.Add("K_APLIC_COD", OracleType.Number)
                'orComdAct.Parameters.Add("K_CODTRXN", OracleType.Number)
                'orComdAct.Parameters.Add("K_RETVAL", OracleType.Number)
                'orComdAct.Parameters(0).Value = Session("CANAL")
                'orComdAct.Parameters(1).Value = Session("ALMACEN")
                'orComdAct.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")
                'orComdAct.Parameters(3).Value = DGTransaccion.Items(i).Cells(0).Text
                'orComdAct.Parameters(0).Direction = ParameterDirection.Input
                'orComdAct.Parameters(1).Direction = ParameterDirection.Input
                'orComdAct.Parameters(2).Direction = ParameterDirection.Input
                'orComdAct.Parameters(3).Direction = ParameterDirection.Input
                'orComdAct.Parameters(4).Direction = ParameterDirection.Output
                'orComdAct.CommandText = "CONF_PARAMETROS_CAJA.CONF_ACT_TRANSAC_RESTRINGIDA"
                'orComdAct.CommandType = CommandType.StoredProcedure
                'intResultado = orComdAct.ExecuteNonQuery
                intResultado = objConfig.FP_Actualiza_Transac_Rest(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), DGTransaccion.Items(i).Cells(0).Text)

                'AUDITORIA
                Detalle(1, 1) = "Canal"
                Detalle(1, 2) = Session("CANAL")
                Detalle(1, 3) = "Canal"

                Detalle(2, 1) = "OfVta"
                Detalle(2, 2) = Session("ALMACEN")
                Detalle(2, 3) = "Oficina de Venta"

                Detalle(3, 1) = "CodApl"
                Detalle(3, 2) = ConfigurationSettings.AppSettings("codAplicacion")
                Detalle(3, 3) = "Codigo de Aplicacion"

                Detalle(4, 1) = "Trans"
                Detalle(4, 2) = DGTransaccion.Items(i).Cells(0).Text
                Detalle(4, 3) = "Transaccion"

                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                'FIN DE AUDITORIA
            End If
        Next
        Response.Write("<script>alert('Se realizo la actualización');</script>")

    End Sub

    Private Sub DGTransaccion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DGTransaccion.ItemDataBound
        'Dim orConector As New OracleConnection
        'Dim orComTR As New OracleCommand
        'Dim orDataATR As New OracleDataAdapter
        Dim dsDataTR As New DataSet

        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()
        'orComTR.Connection = orConector
        'orComTR.Parameters.Add("K_CODCANAL", OracleType.VarChar, 5)
        'orComTR.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
        'orComTR.Parameters.Add("K_APLIC_COD", OracleType.Number)
        'orComTR.Parameters.Add("K_CUR_LISTTRANREST", OracleType.Cursor)

        'orComTR.Parameters(0).Value = Session("CANAL")
        'orComTR.Parameters(1).Value = Session("ALMACEN")
        'orComTR.Parameters(2).Value = ConfigurationSettings.AppSettings("codAplicacion")

        'orComTR.Parameters(0).Direction = ParameterDirection.Input
        'orComTR.Parameters(1).Direction = ParameterDirection.Input
        'orComTR.Parameters(2).Direction = ParameterDirection.Input
        'orComTR.Parameters(3).Direction = ParameterDirection.Output

        'orComTR.CommandText = "CONF_PARAMETROS_CAJA.CONF_TRANSAC_RESTRINGIDAS"
        'orComTR.CommandType = CommandType.StoredProcedure

        'orDataATR.SelectCommand = orComTR
        'orDataATR.Fill(dsDataTR)

        dsDataTR = objConfig.FP_Consulta_Transac_Rest(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"))

        For i = 0 To dsDataTR.Tables(0).Rows.Count - 1
            If CType(dsDataTR.Tables(0).Rows(i).Item("ID_CONFTRAN"), String) = e.Item.Cells(0).Text Then
                chkControl = CType(e.Item.FindControl("CheckBox1"), CheckBox)
                chkControl.Checked = True
            End If
        Next

    End Sub
End Class
