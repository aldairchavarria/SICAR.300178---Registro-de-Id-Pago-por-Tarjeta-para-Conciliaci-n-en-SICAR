Public Class ConsultaCajaInd_Cajero
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFiltro As System.Web.UI.WebControls.Label
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFiltro As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdAceptar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdCancelar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Dim objclsAdmCaja As COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const CAJERO As String = "C"
#End Region

#Region "Eventos"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            If Not IsPostBack Then
                txtFiltro.Attributes.Add("onkeydown", "f_buscar();")
                Session("ConCajTot_codCajero") = Nothing
                Session("dgListaCajero") = Nothing
                Session("dgListaOficina") = Nothing
                Session("Asignar_dgListaOficina") = Nothing
                Session("dgListaCaja") = Nothing
                Session("CHECKED_CAJ_1") = Nothing
                Buscar()
            End If
        End If
    End Sub

    Private Sub DGLista_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGLista.PageIndexChanged
        RemenberOldValue()
        DGLista.CurrentPageIndex = e.NewPageIndex
        DGLista.DataSource = Session("dgListaCajero")
        DGLista.DataBind()
        RePopulateValues()
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Buscar()
    End Sub

    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        Dim i As Integer = 0
        Dim chkSelect As New CheckBox
        Dim strCajeros As String = String.Empty
        Dim lstCajeros As ArrayList
        Dim lstCajerosMerge As New ArrayList

        Try
            If Not Session("CHECKED_CAJ_1") Is Nothing Then
                lstCajeros = DirectCast(Session("CHECKED_CAJ_1"), ArrayList)
                For i = 0 To lstCajeros.Count - 1
                    lstCajerosMerge.Add(lstCajeros(i))
                Next
            End If

            For i = 0 To DGLista.Items.Count - 1
                chkSelect = DGLista.Items(i).FindControl("chkSel")
                Dim oficina As String
                If chkSelect.Checked Then
                    oficina = CType(DGLista.Items(i).FindControl("lblCodigo"), Label).Text
                    If Not lstCajerosMerge.Contains(oficina) Then
                        lstCajerosMerge.Add(oficina)
                    End If
                End If
            Next

            If lstCajerosMerge.Count > 0 Then
                For Each item As String In lstCajerosMerge
                    strCajeros += item.ToString() & ","
                Next
            End If

            If Not strCajeros.Equals(String.Empty) Then
                Dim ilenCad As Integer = strCajeros.Length
                Session("ConCajTot_codCajero") = strCajeros.Substring(0, ilenCad - 1)
                Response.Write("<script>window.opener.f_CargarDatosCajero();</script>")
                Response.Write("<script>window.close();</script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            chkSelect.Dispose()
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub Buscar()
        Try
            Dim strCajero As String = txtFiltro.Value
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strOficina As String = "0"

            If Not Request.QueryString("of") Is Nothing Then
                If CStr(Request.QueryString("of")).Length = 0 Then
                    strOficina = "0"
                Else
                    strOficina = CStr(Request.QueryString("of"))
                End If
            End If

            Dim dsResult As DataSet = objclsAdmCaja.GetVendedores(strCajero.ToUpper(), strOficina, CAJERO)

            Session("dgListaCajero") = dsResult.Tables(0)
            Me.DGLista.DataSource = Session("dgListaCajero")
            Me.DGLista.CurrentPageIndex = 0
            Me.DGLista.DataBind()

            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub RemenberOldValue()
        Dim codigosCajeros As New ArrayList
        Dim codCajero As String
        For Each fila As DataGridItem In DGLista.Items
            codCajero = DGLista.DataKeys(fila.ItemIndex).ToString()
            Dim chkSelect As CheckBox
            chkSelect = DirectCast(fila.FindControl("chkSel"), CheckBox)
            Dim result As Boolean = chkSelect.Checked
            'Se revisa en la session
            If Not Session("CHECKED_CAJ_1") Is Nothing Then
                codigosCajeros = DirectCast(Session("CHECKED_CAJ_1"), ArrayList)
            End If
            If result Then
                If Not codigosCajeros.Contains(codCajero) Then
                    codigosCajeros.Add(codCajero)
                End If
            Else
                codigosCajeros.Remove(codCajero)
            End If
            If (Not codigosCajeros Is Nothing) And (codigosCajeros.Count > 0) Then
                Session("CHECKED_CAJ_1") = codigosCajeros
            End If
        Next
    End Sub

    Private Sub RePopulateValues()
        Dim codigosCajeros As ArrayList
        Dim codCajero As String
        codigosCajeros = DirectCast(Session("CHECKED_CAJ_1"), ArrayList)
        If (Not codigosCajeros Is Nothing) Then
            If (codigosCajeros.Count > 0) Then
                For Each fila As DataGridItem In DGLista.Items
                    codCajero = DGLista.DataKeys(fila.ItemIndex).ToString()
                    If codigosCajeros.Contains(codCajero) Then
                        Dim chkSelect As CheckBox
                        chkSelect = DirectCast(fila.FindControl("chkSel"), CheckBox)
                        chkSelect.Checked = True
                    End If
                Next
            End If
        End If
    End Sub

#End Region

End Class
