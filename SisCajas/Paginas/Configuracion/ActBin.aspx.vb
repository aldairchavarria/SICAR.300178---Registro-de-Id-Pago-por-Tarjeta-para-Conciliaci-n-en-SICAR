Imports System.Data.OracleClient
Public Class ActBin
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitActualizaBIN As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodigoBin As System.Web.UI.WebControls.Label
    Protected WithEvents lblDesBIN As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNumero As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtDescripcion As System.Web.UI.HtmlControls.HtmlInputText
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
    Dim IdCodBin As Integer
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
            IdCodBin = Request("CodBin")

            'Dim orConector As New OracleConnection
            'Dim orComando As New OracleCommand
            'Dim orDataAd As New OracleDataAdapter
            Dim dsData As New DataSet

            If Not Page.IsPostBack Then
                'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
                'orConector.Open()

                'orComando.Connection = orConector
                'orComando.Parameters.Add("K_COD_BIN", OracleType.Number)
                'orComando.Parameters.Add("K_CUR_LISTBIN", OracleType.Cursor)
                'orComando.Parameters(0).Value = IdCodBin
                'orComando.Parameters(0).Direction = ParameterDirection.Input
                'orComando.Parameters(1).Direction = ParameterDirection.Output
                'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_VISUALIZA_BIN"
                'orComando.CommandType = CommandType.StoredProcedure

                'orDataAd.SelectCommand = orComando
                'orDataAd.Fill(dsData)

                dsData = objConfig.FP_Consulta_BIN(IdCodBin)
                txtNumero.Value = dsData.Tables(0).Rows(0).Item("BIN_COD")
                txtDescripcion.Value = dsData.Tables(0).Rows(0).Item("BIN_DESC")
                selEstado.Value = dsData.Tables(0).Rows(0).Item("BIN_ESTADO")
            End If
        End If
    End Sub

    Private Sub cmdGrabar_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGrabar.ServerClick
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
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcMBIN")
        wParam5 = 1
        wParam6 = "Mantenimiento de BINs. Modificacion"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtMnBn")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "IdBin"
        Detalle(1, 2) = IdCodBin
        Detalle(1, 3) = "Id de BIN"

        Detalle(2, 1) = "Numero"
        Detalle(2, 2) = CStr(txtNumero.Value)
        Detalle(2, 3) = "Numero"

        Detalle(3, 1) = "Descripcion"
        Detalle(3, 2) = CStr(txtDescripcion.Value)
        Detalle(3, 3) = "Descripcion"

        Detalle(4, 1) = "Estado"
        Detalle(4, 2) = CStr(selEstado.Value)
        Detalle(4, 3) = "Estado"

        'FIN DE AUDITORIA


        'Dim orConector As New OracleConnection
        'Dim orComando As New OracleCommand
        'Dim orDataAd As New OracleDataAdapter
        'Dim dsData As New DataSet

        'orConector.ConnectionString = "user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas"
        'orConector.Open()

        'orComando.Connection = orConector
        'orComando.Parameters.Add("K_ID_CODBIN", OracleType.Number)
        'orComando.Parameters.Add("K_COD_BIN", OracleType.Number)
        'orComando.Parameters.Add("K_DESCRIPCION", OracleType.VarChar, 100)
        'orComando.Parameters.Add("K_ESTADO", OracleType.Number)
        'orComando.Parameters.Add("K_RETVAL", OracleType.Number)

        'Dim i As Integer
        'For i = 0 To 3
        '    orComando.Parameters(i).Direction = ParameterDirection.Input
        'Next
        'orComando.Parameters(4).Direction = ParameterDirection.Output

        'orComando.Parameters(0).Value = IdCodBin
        'orComando.Parameters(1).Value = Trim(CStr(txtNumero.Value))
        'orComando.Parameters(2).Value = Trim(CStr(txtDescripcion.Value))
        'orComando.Parameters(3).Value = CInt(selEstado.Value)

        'orComando.CommandText = "CONF_PARAMETROS_CAJA.CONF_ACTUALIZA_BIN"
        'orComando.CommandType = CommandType.StoredProcedure

        'intResultado = orComando.ExecuteNonQuery

        intResultado = objConfig.FP_Actualiza_BIN(IdCodBin, Trim(CStr(txtNumero.Value)), Trim(CStr(txtDescripcion.Value)), CInt(selEstado.Value))
        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        Response.Write("<script>window.opener.f_Refrescar();</script>")
        Response.Write("<script>window.close();</script>")

    End Sub
End Class
