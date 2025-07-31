Imports SisCajas.clsContantes_site
Imports SisCajas.clsLib_Session
Imports SisCajas.clsAudi
Imports SisCajas.GenFunctions
Imports System.Data.OracleClient
Public Class rep_Diario
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDiarioE As System.Web.UI.WebControls.DataGrid

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Public objComponente, objRecordSet
    Public iAux, sFechaActual, sHoraActual, StrXml
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB
    Public strcadenaprint As String = ""
    Public strRuta As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim ObjAux As New GenFunctions(Server, Request, Response)
            Dim ObjAudi As New clsAudi
            Dim ObjCtes As New clsContantes_site
            strRuta = ObjCtes.strRutaSite

            'Parametro de entrada ... Query string
            strFecha = Request.QueryString("pfecha")
            Dim dfecha As DateTime = New DateTime(CInt(Left(strFecha, 4)), CInt(Mid(strFecha, 5, 2)), CInt(Right(strFecha, 2)))
            sFechaActual = dfecha.ToString("d")
            sHoraActual = DateTime.Now.ToString("t")

            If Not Page.IsPostBack Then
                llena_grid(dfecha)
            End If

            If (Len(Session("STRMessage")) > 0) Then
                Response.Write("<script language=JavaScript type='text/javascript'>")
                Response.Write("alert('" & Session("STRMessage") & "');")
                Response.Write("</script>")
            End If
            Session("STRMessage") = ""
        End If
    End Sub

    Private Sub llena_grid(ByVal fecha As DateTime)
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Try
            Dim dsDiarioE As DataSet = objCajas.FP_ListDiarioE(Session("ALMACEN"), fecha)
            If Not dsDiarioE Is Nothing Then
                dgDiarioE.DataSource = dsDiarioE.Tables(0)
                dgDiarioE.DataBind()
            Else
                Response.Write("<Script languaje=jscript> alert('NO HAY OPERACIONES ELECTRONICAS PARA LA FECHA SEÑALADA'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<Script languaje=jscript> alert('" + ex.Message + "'); </script>")
        End Try

    End Sub
End Class
