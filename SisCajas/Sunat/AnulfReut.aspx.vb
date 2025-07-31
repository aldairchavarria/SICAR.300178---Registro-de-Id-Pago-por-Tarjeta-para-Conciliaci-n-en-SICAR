Public Class AnulfReut
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtClaseDoc As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtSerie As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtCorrelativo As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtMotivo As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button

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
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If txtFecha.Value.Trim() = "" Then
                txtFecha.Value = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Now.Date.ToString("d")
        End If
        End If
    End Sub

    Private Sub BtnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        'variables de sesion     
        'Dim strTipoOficina As String = Session("VarVal2")
        Dim strTipoOficina As String = Session("CANAL")
        'Dim oficinaVenta As String = "0006"
        Dim oficinaVenta As String = Session("ALMACEN")

        'variables del WEB CONFIG
        ' Objectos de conexion
        Dim strClaseDoc, strSerie, strCorrelativo, strFecha, strMotivo
        Dim sValorA, sValorB, msgErr
        'Variables globales
        strClaseDoc = txtClaseDoc.Value
        strSerie = txtSerie.Value
        strCorrelativo = txtCorrelativo.Value
        strFecha = txtFecha.Value
        strMotivo = txtMotivo.Value

        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim dsResult As DataSet
        Try
            dsResult = obSAP.Set_SUNATAnulR(oficinaVenta, strClaseDoc, strSerie, strCorrelativo, strFecha, strMotivo)
            Dim drFila As DataRow
            msgErr = ""
            For Each drFila In dsResult.Tables(0).Rows
                If CStr(drFila(0)) = "E" Then
                    msgErr = msgErr + CStr(drFila(3))
                End If
            Next
            If (msgErr <> "") Then
                Response.Write("<script language=jscript> alert('" + msgErr + "'); </script>")
            End If
            'Response.Write("<script language=jscript> alert('Anulación Efectuada.'); </script>")
            txtClaseDoc.Value = ""
            txtSerie.Value = ""
            txtCorrelativo.Value = ""
            txtMotivo.Value = ""

        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try

    End Sub
End Class
