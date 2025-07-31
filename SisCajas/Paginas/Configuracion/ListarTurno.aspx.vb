Imports System.Data.OracleClient
Public Class ListarTurno
    Inherits System.Web.UI.Page

    Dim objConfig As New COM_SIC_Configura.clsConfigura
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DGListaTurnos As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTitActualizaBIN As System.Web.UI.WebControls.Label

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
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            'Dim orConector As New OracleConnection
            'Dim orComando As New OracleCommand
            'Dim orDataAd As New OracleDataAdapter
            Dim dsData As New DataSet

            'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
            'orConector.Open()

            'orComando.Connection = orConector
            'orComando.Parameters.Add("K_CODCANAL", OracleType.VarChar, 5)
            'orComando.Parameters.Add("K_APLIC_COD", OracleType.Number)
            'orComando.Parameters.Add("K_COD_PDV", OracleType.VarChar, 5)
            'orComando.Parameters.Add("K_CUR_LISTTURNO", OracleType.Cursor)

            'orComando.Parameters(0).Value = Session("CANAL")
            'orComando.Parameters(1).Value = ConfigurationSettings.AppSettings("codAplicacion")
            'orComando.Parameters(2).Value = Session("ALMACEN")

            'orComando.Parameters(0).Direction = ParameterDirection.Input
            'orComando.Parameters(1).Direction = ParameterDirection.Input
            'orComando.Parameters(2).Direction = ParameterDirection.Input
            'orComando.Parameters(3).Direction = ParameterDirection.Output

            'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_TURNO"
            'orComando.CommandType = CommandType.StoredProcedure

            'orDataAd.SelectCommand = orComando
            'orDataAd.Fill(dsData)
            dsData = objConfig.FP_Lista_Turno(Session("CANAL"), ConfigurationSettings.AppSettings("codAplicacion"), Session("ALMACEN"))
            DGListaTurnos.DataSource = dsData.Tables(0)

        DGListaTurnos.DataBind()
        End If
    End Sub

End Class
