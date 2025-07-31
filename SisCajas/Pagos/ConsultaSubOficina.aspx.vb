Public Class ConsultaSubOficina
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitulo As System.Web.UI.WebControls.Label
    Protected WithEvents lblFiltro As System.Web.UI.WebControls.Label
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFiltro As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdAceptar As System.Web.UI.HtmlControls.HtmlInputButton

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
    Dim objclsAdmSubOficinas As COM_SIC_Activaciones.clsRecaudacionDAC
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public identificadorLog As String
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
                identificadorLog = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "
                txtFiltro.Attributes.Add("onkeydown", "f_buscar();")
                Session("ConCajTot_codSubOficina") = Nothing
                Session("dgListaSubOficina") = Nothing
                Session("CHECKED_SUBOFI_2") = Nothing
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
        DGLista.DataSource = Session("dgListaSubOficina")
        DGLista.DataBind()
        RePopulateValues()
    End Sub

    Private Sub cmdAceptar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.ServerClick
        Dim i As Integer = 0
        Dim chkSelect As New CheckBox
        Dim strSubOficinas As String = String.Empty
        Dim lstSubOficinas As ArrayList
        Dim lstSubOficinasMerge As New ArrayList
        identificadorLog = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "
        Try
            If Not Session("CHECKED_SUBOFI_2") Is Nothing Then
                lstSubOficinas = DirectCast(Session("CHECKED_SUBOFI_2"), ArrayList)
                For i = 0 To lstSubOficinas.Count - 1
                    lstSubOficinasMerge.Add(lstSubOficinas(i))
                Next
            End If

            For i = 0 To DGLista.Items.Count - 1
                chkSelect = DGLista.Items(i).FindControl("chkSel")
                Dim SubOficina As String
                If chkSelect.Checked Then
                    SubOficina = CType(DGLista.Items(i).FindControl("lblCodigo"), Label).Text
                    If Not lstSubOficinasMerge.Contains(SubOficina) Then
                        lstSubOficinasMerge.Add(SubOficina)
                    End If
                End If
            Next

            If lstSubOficinasMerge.Count > 0 Then
                For Each item As String In lstSubOficinasMerge
                    strSubOficinas += item.ToString() & ","
                Next
            End If

            If Not strSubOficinas.Equals(String.Empty) Then
                Dim ilenCad As Integer = strSubOficinas.Length
                Session("ConCajTot_codSubOficina") = strSubOficinas.Substring(0, ilenCad - 1)
                objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & " Session(ConCajTot_codSubOficina) : " & Session("ConCajTot_codSubOficina"))
                Response.Write("<script>window.opener.f_CargarDatosSubOficina();</script>")
                Response.Write("<script>window.close();</script>")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & " Error Aceptar() : " & ex.Message.ToString())
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            chkSelect.Dispose()
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub Buscar()
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Cargar Sub Oficinas - Inicio" & " ---")
            Dim strFiltro As String = txtFiltro.Value
            objclsAdmSubOficinas = New COM_SIC_Activaciones.clsRecaudacionDAC
            Dim strCodigoRespuesta As String
            Dim strDescRespuesta As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strFiltro : " & strFiltro)

            Dim dtResult As DataTable = objclsAdmSubOficinas.FiltrarSubOficina(strFiltro, strCodigoRespuesta, strDescRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strCodigoRespuesta : " & strCodigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "strDescRespuesta : " & strDescRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "dtResult.Count : " & dtResult.Rows.Count)

            Session("dgListaSubOficina") = dtResult
            Me.DGLista.DataSource = Session("dgListaSubOficina")
            Me.DGLista.CurrentPageIndex = 0
            Me.DGLista.DataBind()

            If dtResult.Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Cargar Sub Oficinas - Fin" & " ---")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, identificadorLog & "Error :" & ex.Message.ToString())
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub RemenberOldValue()
        Dim codigosSubOficinas As New ArrayList
        Dim codSubOficina As String
        For Each fila As DataGridItem In DGLista.Items
            codSubOficina = DGLista.DataKeys(fila.ItemIndex).ToString()
            Dim chkSelect As CheckBox
            chkSelect = DirectCast(fila.FindControl("chkSel"), CheckBox)
            Dim result As Boolean = chkSelect.Checked
            'Se revisa en la session
            If Not Session("CHECKED_SUBOFI_2") Is Nothing Then
                codigosSubOficinas = DirectCast(Session("CHECKED_SUBOFI_2"), ArrayList)
            End If
            If result Then
                If Not codigosSubOficinas.Contains(codSubOficina) Then
                    codigosSubOficinas.Add(codSubOficina)
                End If
            Else
                codigosSubOficinas.Remove(codSubOficina)
            End If
            If (Not codigosSubOficinas Is Nothing) And (codigosSubOficinas.Count > 0) Then
                Session("CHECKED_SUBOFI_2") = codigosSubOficinas
            End If
        Next
    End Sub

    Private Sub RePopulateValues()
        Dim codigosSubOficinas As ArrayList
        Dim codSubOficina As String
        codigosSubOficinas = DirectCast(Session("CHECKED_SUBOFI_2"), ArrayList)
        If (Not codigosSubOficinas Is Nothing) Then
            If (codigosSubOficinas.Count > 0) Then
                For Each fila As DataGridItem In DGLista.Items
                    codSubOficina = DGLista.DataKeys(fila.ItemIndex).ToString()
                    If codigosSubOficinas.Contains(codSubOficina) Then
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
