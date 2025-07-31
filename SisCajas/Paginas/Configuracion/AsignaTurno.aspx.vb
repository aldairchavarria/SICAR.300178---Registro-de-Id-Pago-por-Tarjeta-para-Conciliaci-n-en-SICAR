Imports System.Data.OracleClient
Public Class AsignaTurno
    Inherits System.Web.UI.Page
    Dim dsData As New DataSet
    Dim objConfig As New COM_SIC_Configura.clsConfigura
    Dim i As Integer

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitActualizaBIN As System.Web.UI.WebControls.Label
    Protected WithEvents SelTipMoneda2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SelNomBanco2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SelNomBanco1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCtaSol As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCtaDolar As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents SelTurnos As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtHoraFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtHoraIni As System.Web.UI.HtmlControls.HtmlInputText


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
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()

                'orComando.Connection = orConector
                'orComando.Parameters.Add("K_COD_CANAL", OracleType.VarChar, 5)
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

                'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_LISTA_TURNO_ACTIVOS"
                'orComando.CommandType = CommandType.StoredProcedure

                'orDataAd.SelectCommand = orComando
                'orDataAd.Fill(dsData)

                dsData = objConfig.FP_Lista_Turno_Activos(Session("CANAL"), ConfigurationSettings.AppSettings("codAplicacion"), Session("ALMACEN"))

                Session("Data") = dsData
                SelTurnos.DataSource = dsData.Tables(0)
                SelTurnos.DataValueField = "ID_CONFTURNO"
                SelTurnos.DataTextField = "TURNO_DESCRIP"
                SelTurnos.DataBind()
                SelTurnos.Items.Insert(0, New ListItem(" -- Seleccione -- ", ""))
        End If
        End If
    End Sub

    Private Sub SelTurnos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelTurnos.SelectedIndexChanged
        dsData = Session("Data")
        Dim a As String
        For i = 0 To dsData.Tables(0).Rows.Count - 1
            If dsData.Tables(0).Rows(i).Item("ID_CONFTURNO") = SelTurnos.SelectedValue Then
                txtHoraIni.Value = dsData.Tables(0).Rows(i).Item("TURNO_INICIO")
                txtHoraFin.Value = dsData.Tables(0).Rows(i).Item("TURNO_FIN")

            End If
        Next

    End Sub
End Class
