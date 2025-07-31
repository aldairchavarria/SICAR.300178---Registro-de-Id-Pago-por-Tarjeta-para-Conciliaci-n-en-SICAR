Public Class contabilizarRec_oficina
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
                Session("ContaRec_codOficina") = Nothing
                Session("dgListaOficinaRec") = Nothing
                Session("CHECKED_OFI_1") = Nothing
                Buscar()
            End If
        End If
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Buscar()
    End Sub

    Private Sub DGLista_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGLista.PageIndexChanged
        RemenberOldValue()
        DGLista.CurrentPageIndex = e.NewPageIndex
        DGLista.DataSource = Session("dgListaOficinaRec")
        DGLista.DataBind()
        RePopulateValues()
    End Sub

    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        Dim i As Integer = 0
        Dim chkSelect As New CheckBox
        Dim strOficinas As String = String.Empty
        Dim lstOficinas As ArrayList
        Dim lstOficinasMerge As New ArrayList
        Try
            If Not Session("CHECKED_OFI_1") Is Nothing Then
                lstOficinas = DirectCast(Session("CHECKED_OFI_1"), ArrayList)
                For i = 0 To lstOficinas.Count - 1
                    'strOficinas += lstOficinas(i) & ","
                    lstOficinasMerge.Add(lstOficinas(i))
                Next
            End If

            For i = 0 To DGLista.Items.Count - 1
                chkSelect = DGLista.Items(i).FindControl("chkSel")
                Dim oficina As String
                If chkSelect.Checked Then
                    oficina = CType(DGLista.Items(i).FindControl("lblCodigo"), Label).Text
                    If Not lstOficinasMerge.Contains(oficina) Then
                        lstOficinasMerge.Add(oficina)
                    End If
                End If
            Next

            If lstOficinasMerge.Count > 0 Then
                For Each item As String In lstOficinasMerge
                    strOficinas += item.ToString() & ","
                Next
            End If

            If Not strOficinas.Equals(String.Empty) Then
                Dim ilenCad As Integer = strOficinas.Length
                Session("ContaRec_codOficina") = strOficinas.Substring(0, ilenCad - 1)
                Response.Write("<script>window.opener.f_CargarDatosOficina();</script>")
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
            Dim strOficina As String = txtFiltro.Value
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim dsResult As DataSet = objclsAdmCaja.GetOficinas(strOficina.ToUpper())

            Session("dgListaOficinaRec") = dsResult.Tables(0)
            Me.DGLista.DataSource = Session("dgListaOficinaRec")
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
        Dim codigosOficinas As New ArrayList
        Dim codOficina As String
        For Each fila As DataGridItem In DGLista.Items
            codOficina = DGLista.DataKeys(fila.ItemIndex).ToString()
            Dim chkSelect As CheckBox
            chkSelect = DirectCast(fila.FindControl("chkSel"), CheckBox)
            Dim result As Boolean = chkSelect.Checked
            'Se revisa en la session
            If Not Session("CHECKED_OFI_1") Is Nothing Then
                codigosOficinas = DirectCast(Session("CHECKED_OFI_1"), ArrayList)
            End If
            If result Then
                If Not codigosOficinas.Contains(codOficina) Then
                    codigosOficinas.Add(codOficina)
                End If
            Else
                codigosOficinas.Remove(codOficina)
            End If
            If (Not codigosOficinas Is Nothing) And (codigosOficinas.Count > 0) Then
                Session("CHECKED_OFI_1") = codigosOficinas
            End If
        Next
    End Sub

    Private Sub RePopulateValues()
        Dim codigosOficinas As ArrayList
        Dim codOficina As String
        codigosOficinas = DirectCast(Session("CHECKED_OFI_1"), ArrayList)
        If (Not codigosOficinas Is Nothing) Then
            If (codigosOficinas.Count > 0) Then
                For Each fila As DataGridItem In DGLista.Items
                    codOficina = DGLista.DataKeys(fila.ItemIndex).ToString()
                    If codigosOficinas.Contains(codOficina) Then
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
