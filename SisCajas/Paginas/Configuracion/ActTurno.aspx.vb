Imports System.Data.OracleClient

Public Class ActTurno
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitActualizaBIN As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodigoBin As System.Web.UI.WebControls.Label
    Protected WithEvents lblDesBIN As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescripcion As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtHoraIni As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtHoraFin As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTolerancia As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents selEstado As System.Web.UI.HtmlControls.HtmlSelect
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
    Dim IdCodTurno As Integer
    Dim objConfig As New COM_SIC_Configura.clsConfigura

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            IdCodTurno = Request("CodTurno")

            'Dim orConector As New OracleConnection
            'Dim orComando As New OracleCommand
            'Dim orDataAd As New OracleDataAdapter
            Dim dsData As New DataSet

            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()

                'orComando.Connection = orConector
                'orComando.Parameters.Add("K_CODTURNO", OracleType.Number)
                'orComando.Parameters.Add("K_CUR_DATOTURNO", OracleType.Cursor)
                'orComando.Parameters(0).Value = IdCodTurno
                'orComando.Parameters(0).Direction = ParameterDirection.Input
                'orComando.Parameters(1).Direction = ParameterDirection.Output
                'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_VISUALIZA_TURNO"
                'orComando.CommandType = CommandType.StoredProcedure

                'orDataAd.SelectCommand = orComando
                'orDataAd.Fill(dsData)

                dsData = objConfig.FP_Visualiza_Turno(IdCodTurno)

                txtDescripcion.Value = dsData.Tables(0).Rows(0).Item("TURNO_DESCRIP")
                txtHoraIni.Value = dsData.Tables(0).Rows(0).Item("TURNO_INICIO")
                txtHoraFin.Value = dsData.Tables(0).Rows(0).Item("TURNO_FIN")
                txtTolerancia.Value = dsData.Tables(0).Rows(0).Item("TURNO_TOLERANCIA")
                selEstado.Value = dsData.Tables(0).Rows(0).Item("TURNO_ESTADO")
            End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
        Dim intResultado As Integer
        'Dim orConector As New OracleConnection
        'Dim orComando As New OracleCommand
        'Dim orDataAd As New OracleDataAdapter
        'Dim dsData As New DataSet

        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()

        'orComando.Connection = orConector
        'orComando.Parameters.Add("K_COD_TURNO", OracleType.Number)
        'orComando.Parameters.Add("K_DESCRIPCION", OracleType.VarChar, 100)
        'orComando.Parameters.Add("K_HORAINI", OracleType.VarChar, 5)
        'orComando.Parameters.Add("K_HORAFIN", OracleType.VarChar, 5)
        'orComando.Parameters.Add("K_TOLERANCIA", OracleType.Number)
        'orComando.Parameters.Add("K_ESTADO", OracleType.Number)
        'orComando.Parameters.Add("K_RETVAL", OracleType.Number)

        'Dim i As Integer
        'For i = 0 To 5
        '    orComando.Parameters(i).Direction = ParameterDirection.Input
        'Next
        'orComando.Parameters(6).Direction = ParameterDirection.Output

        'orComando.Parameters(0).Value = IdCodTurno
        'orComando.Parameters(1).Value = CStr(txtDescripcion.Value)
        'orComando.Parameters(2).Value = CStr(txtHoraIni.Value)
        'orComando.Parameters(3).Value = CStr(txtHoraFin.Value)
        'orComando.Parameters(4).Value = CInt(txtTolerancia.Value)
        'orComando.Parameters(5).Value = CInt(selEstado.Value)

        'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_TURNO"
        'orComando.CommandType = CommandType.StoredProcedure

        'intResultado = orComando.ExecuteNonQuery

        intResultado = objConfig.FP_Actualiza_Turno(IdCodTurno, CStr(txtDescripcion.Value), CStr(txtHoraIni.Value), CStr(txtHoraFin.Value), CInt(txtTolerancia.Value), CInt(selEstado.Value))
        Response.Write("<script>window.opener.f_Refrescar();</script>")
        Response.Write("<script>window.close();</script>")

    End Sub
End Class
