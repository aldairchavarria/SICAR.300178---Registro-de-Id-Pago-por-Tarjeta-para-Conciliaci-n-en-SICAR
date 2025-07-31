Imports System.Data.OracleClient

Public Class rep_AnulaExcel
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblfecha As System.Web.UI.WebControls.Label
    Protected WithEvents lblHora As System.Web.UI.WebControls.Label
    Protected WithEvents DgAnulacion As System.Web.UI.WebControls.DataGrid

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
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim strFecha As String = Request.QueryString("pfecha")
            Dim strCajero As String = Request.QueryString("pcajero")

            Dim dfecha As DateTime = New DateTime(CInt(Left(strFecha, 4)), CInt(Mid(strFecha, 5, 2)), CInt(Right(strFecha, 2)))
            lblfecha.Text = dfecha.ToString("d")
            lblHora.Text = DateTime.Now.ToString("t")


            If Not Page.IsPostBack Then
                llena_grid(dfecha, strCajero)
            End If
            Response.AddHeader("Content-Disposition", "attachment;filename=AnulxCajero.xls")
        Response.ContentType = "application/vnd.ms-excel"
        End If
    End Sub

    Private Sub llena_grid(ByVal fecha As DateTime, ByVal cajero As String)

        Dim objPoolPagos As New SAP_SIC_Pagos.clsPagos
        'Dim dsAnulacion As DataSet = objPoolPagos.Get_ConsultaPoolFactura(Session("ALMACEN"), fecha.ToString("d"), "R", "", "", "", "20", "1")
        'Dim dsAnulacion As DataSet = objPoolPagos.Get_ConsultaPagosUsuario(fecha.ToString("d"), fecha.ToString("d"), "X", cajero, Session("ALMACEN"))
        Dim dsAnulacion As DataSet = objPoolPagos.Get_ConsultaPagosUsuario(fecha.ToString("d"), "", "X", cajero, Session("ALMACEN"))
        'Incidencia demora en la carga de pool documentos pagados --> Solicidtud de Luis Palacios enviar solo una fecha 18/06/2012
        DgAnulacion.DataSource = dsAnulacion.Tables(0)
        DgAnulacion.DataBind()

    End Sub

End Class
