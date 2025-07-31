Public Class rep_DiarioDetIndExcel
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDiarioE As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblfecha As System.Web.UI.WebControls.Label
    Protected WithEvents lblHora As System.Web.UI.WebControls.Label
    Protected WithEvents txtUsuario As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Public strusuario As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Parametro de entrada ... Query string
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim strFecha As String = Request.QueryString("pfecha")
            Dim dfecha As DateTime = New DateTime(CInt(Left(strFecha, 4)), CInt(Mid(strFecha, 5, 2)), CInt(Right(strFecha, 2)))
            'Dim dfecha As Date = CDate(strFecha)

            lblfecha.Text = dfecha.ToString("d")
            lblHora.Text = DateTime.Now.ToString("t")

            strusuario = Session("USUARIO")

            If Not Page.IsPostBack Then
                llena_grid(dfecha)
            End If
            Response.AddHeader("Content-Disposition", "attachment;filename=DiarioElectrIndiv.xls")
            Response.ContentType = "application/vnd.ms-excel"
        End If
    End Sub

    Private Sub llena_grid(ByVal fecha As DateTime)
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        'Try
        Dim dsDiario As DataSet = objCajas.FP_ListDiarioE(Session("ALMACEN"), fecha, strusuario)
        Dim i As Integer
        Dim j As Integer
        Dim intFilas As Integer

        If Not dsDiario Is Nothing Then
            dgDiarioE.DataSource = dsDiario.Tables(0)

            'intFilas = tblResult.Rows.Count

            'For i = 0 To 2
            '    For j = 0 To 20
            '        tblResult.Rows(i).Cells(j).InnerText = IIf(dsDiario.Tables(0).Rows(i).Item(j) Is DBNull.Value, "&nbsp;", dsDiario.Tables(0).Rows(i).Item(j))
            '    Next
            'Next

            'dgDiarioE.DataBind()
            ' Else
            '    Response.Write("<Script languaje=jscript> alert('NO HAY OPERACIONES ELECTRONICAS PARA LA FECHA SEÑALADA'); </script>")
        End If
        'Catch ex As Exception
        '   Response.Write("<Script languaje=jscript> alert('" + ex.Message + "'); </script>")
        ' End Try


        'Dim obCon As New OracleConnection("user id=usrbdgcarpetas;data source=timdev;password=usrbdgcarpetas")
        'Dim obCmd As New OracleCommand("select * from conf_operacion where oper_fecha_trans= to_date('" + fecha.ToString("dd/MM/yyyy") + "','dd/mm/yyyy') and cod_cajero='" + strusuario + "'", obCon)
        'Dim dsOpera As New DataSet

        'Dim da As New OracleDataAdapter(obCmd)
        'da.Fill(dsOpera)
        'DgOpera.DataSource = dsOpera
        'DgOpera.DataSource = dsOpera.Tables(0)
        'DgOpera.DataBind()

    End Sub


End Class
