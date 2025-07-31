Imports System.Data.OracleClient

Public Class ListadoClienteB
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGReporte As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblRUC As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label

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
        'Dim orComando As New OracleCommand
        'Dim orDataAd As New OracleDataAdapter
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim dsData As New DataSet
            Dim cteContador As Double
            Dim i As Integer
            Dim objRecbusiness As New COM_SIC_RecBusiness.clsRecBusiness

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
            Dim Detalle(1, 3) As String

            Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
            ' fin de variables de auditoria

            'AUDITORIA
            wParam1 = Session("codUsuario")
            wParam2 = Request.ServerVariables("REMOTE_ADDR")
            wParam3 = Request.ServerVariables("SERVER_NAME")
            wParam4 = ConfigurationSettings.AppSettings("gConstOpcRBus")
            wParam5 = 1
            wParam6 = "Reporte de Clientes Business"
            wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
            wParam8 = ConfigurationSettings.AppSettings("gConstEvtRCbs")
            wParam9 = Session("codPerfil")
            wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
            wParam11 = 1

            Detalle(1, 1) = "RUC"
            Detalle(1, 2) = CStr(Request("strRUC"))
            Detalle(1, 3) = "RUC"
            'FIN DE AUDITORIA

            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=tim;data source=BSCS708;password=tim"
                'orConector.Open()
                'orComando.Connection = orConector
                'orComando.Parameters.Add("P_RUC_DNI", OracleType.VarChar, 12)
                'orComando.Parameters.Add("P_CURSOR", OracleType.Cursor)

                'orComando.Parameters(0).Value = CStr(Request("strRUC"))

                'orComando.Parameters(0).Direction = ParameterDirection.Input
                'orComando.Parameters(1).Direction = ParameterDirection.Output

                'orComando.CommandText = "TIM100_PKG_CONSULTAS_BSCS.SP_DEUDA_PENDIENTE_X_RUCNID"
                'orComando.CommandType = CommandType.StoredProcedure

                'orDataAd.SelectCommand = orComando
                'orDataAd.Fill(dsData)
                dsData = objRecbusiness.FP_DeudaXRucDNI(CStr(Request("strRUC")))
                DGReporte.DataSource = dsData.Tables(0)
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            End If

            DGReporte.DataBind()

            For i = 0 To DGReporte.Items.Count - 1
                cteContador = cteContador + DGReporte.Items(i).Cells(6).Text
            Next
            lblRUC.Text = Request("strRUC")
        lblTotal.Text = Format(cteContador, "#######0.00")
        End If
    End Sub

End Class
