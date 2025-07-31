Public Class asignarCajas
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCajas As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cboCajeros As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
    'FFS (INSTANCIA OBJETO PARA TRABAJAR EN MODO DESCONECTADO DE SAP)
    Dim objOffline As New COM_SIC_OffLine.clsOffline

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        'Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        'Session("ALMACEN") = "0006"
        'Session("USUARIO") = "23175"
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim objPagos As New SAP_SIC_Pagos.clsPagos
            Dim dsCajeros As DataSet
            'FFS INICIO
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            'FFS FIN

            Dim dsCajas As DataSet

            If Not Page.IsPostBack Then
                'dsCajas = objSapCajas.Get_CajaOficinas(Session("ALMACEN"))
                dsCajas = objOffline.Get_CajaOficinas(Session("ALMACEN"))

                dgCajas.DataSource = dsCajas.Tables(0)
                dgCajas.DataBind()

                'dsCajeros = objPagos.Get_ConsultaCajeros(Session("ALMACEN"), "C")

                dsCajeros = objOffline.Get_ConsultaCajeros(Session("ALMACEN"), "C")
                If Not dsCajeros Is Nothing Then
                    cboCajeros.DataTextField = "NOMBRE"
                    cboCajeros.DataValueField = "USUARIO"
                    cboCajeros.DataSource = dsCajeros.Tables(0)
                    cboCajeros.DataBind()
                    cboCajeros.Items.Insert(0, "")
                End If
            End If
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim dsResult As DataSet
        Dim i As Integer
        Dim blnError As Boolean = False
        Dim cajero As String = cboCajeros.SelectedValue

        'dsResult = objSapCajas.Set_CajeroDiario(Session("ALMACEN"), cboCajeros.SelectedValue, Now.ToString("d"), Request.Item("rbCaja"))
        'FFS INICIO (MODO DESCONECTADO DE SAP)

        If cajero = "" Then
            Response.Write("<script>alert('Para Realizar la Asignación Seleccionar un Usuario')</script>")
            Exit Sub
        End If

        If Request.Item("rbCaja") = "" Then
            Response.Write("<script>alert('Para Realizar la Asignación Seleccionar una Caja')</script>")
            Exit Sub
        End If

        '**Retorna el valor para la Caja, al final del mètodo **'
        dsResult = objOffline.Set_CajeroDiario(Session("ALMACEN"), cboCajeros.SelectedValue, Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), Request.Item("rbCaja"))
        'FFS FIN

        Session("CAJA") = Request.Item("rbCaja")


        If Not IsNothing(dsResult) Then
            For i = 0 To dsResult.Tables(0).Rows.Count - 1
                If dsResult.Tables(0).Rows(i).Item("TYPE") = "E" Then
                    Response.Write("<script>alert('" & dsResult.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                    blnError = True
                End If
            Next
        End If
        If Not blnError Then
            Response.Write("<script>alert('Asignación realizada con éxito')</script>")
        End If

        cboCajeros.SelectedIndex = 0
    End Sub

End Class
