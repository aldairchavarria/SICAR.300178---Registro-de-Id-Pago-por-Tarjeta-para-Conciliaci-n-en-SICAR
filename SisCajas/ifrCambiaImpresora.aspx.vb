Public Class ifrCambiaImpresora
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cbImpresora As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtCanal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtRows As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Dim CodOficina As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.cbImpresora.Attributes.Add("onchange", "ValidaUsuarioImp();")

        If Not Page.IsPostBack Then

            CodOficina = Request.QueryString("codOficina")
            Me.txtUsuario.Value = CStr(Session("USUARIO")).PadLeft(10, "0")
            txtFecha.Value = Now.ToString("yyyyMMdd")
            Try
                LlenaCombo(CodOficina)
            Catch ex As Exception
                Response.Write("<script> alert('" + ex.Message + "'); </script>")
            End Try
        End If
    End Sub

    'Private Sub LlenaCombo(ByVal strOficina As String)


    '    Dim objOffline As New COM_SIC_OffLine.clsOffline
    '    Dim intSAP = objOffline.Get_ConsultaSAP

    '    'Dim obSap As New SAP_SIC_Pagos.clsPagos
    '    'Dim dsImp As DataSet = obSap.Get_ListaImpresoras(strOficina)
    '    Dim dsImp As DataSet
    '    Dim obSap As Object
    '    If intSAP = 1 Then
    '        obSap = New SAP_SIC_Pagos.clsPagos
    '    Else
    '        obSap = New COM_SIC_OffLine.clsOffline
    '    End If
    '    dsImp = obSap.Get_ListaImpresoras(strOficina)

    '    Dim drFila As DataRow
    '    'For Each drFila In dsImp.Tables(1).Rows
    '    '    If drFila("TYPE") = "E" Then
    '    '        Throw New ApplicationException(drFila("MESSAGE"))
    '    '    End If
    '    'Next

    '    cbImpresora.Items.Clear()
    '    cbImpresora.Items.Add("")
    '    For Each drFila In dsImp.Tables(0).Rows
    '        cbImpresora.Items.Add(New ListItem(drFila("DENOM_CAJA"), drFila("CAJA") + ";" + drFila("SERIE_CAJA") + ";" + drFila("USADO_POR") + ";" + drFila("FECHA_USO")))
    '    Next
    '    cbImpresora.SelectedValue = ""
    '    If dsImp.Tables(0).Rows.Count > 0 Then
    '        txtRows.Value = dsImp.Tables(0).Rows.Count.ToString()
    '    Else
    '        txtRows.Value = ""
    '    End If

    '    Dim dsOfi As DataSet = obSap.Get_ConsultaOficinaVenta(strOficina, "")


    '    If dsOfi.Tables(0).Rows.Count > 0 Then
    '        txtCanal.Value = dsOfi.Tables(0).Rows(0)("VTWEG")
    '    End If

    'End Sub


    Private Sub LlenaCombo(ByVal strOficina As String)

        'CAMBIADO POR FFS INICIO
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        'Dim intSAP = objOffline.Get_ConsultaSAP

        'Dim obSap As New SAP_SIC_Pagos.clsPagos
        'Dim dsImp As DataSet = obSap.Get_ListaImpresoras(strOficina)
        Dim dsImp As DataSet
        Dim obSap As Object

        'If intSAP = 1 Then
        '    obSap = New SAP_SIC_Pagos.clsPagos
        'Else
        'obSap = New COM_SIC_OffLine.clsOffline
        'End If
        dsImp = objOffline.Get_ListaImpresoras(strOficina)

        Dim drFila As DataRow
        'For Each drFila In dsImp.Tables(1).Rows
        '    If drFila("TYPE") = "E" Then
        '        Throw New ApplicationException(drFila("MESSAGE"))
        '    End If
        'Next

        cbImpresora.Items.Clear()
        cbImpresora.Items.Add("")
        For Each drFila In dsImp.Tables(0).Rows
            cbImpresora.Items.Add(New ListItem(drFila("DENOM_CAJA"), drFila("CAJA") + ";" + drFila("SERIE_CAJA") + ";" + drFila("USADO_POR") + ";" + formatofecha(drFila("FECHA_USO"))))
        Next
        cbImpresora.SelectedValue = ""
        If dsImp.Tables(0).Rows.Count > 0 Then
            txtRows.Value = dsImp.Tables(0).Rows.Count.ToString()
        Else
            txtRows.Value = ""
        End If

        Dim dsOfi As DataSet = objOffline.Get_ConsultaOficinaVenta(strOficina, "")


        If dsOfi.Tables(0).Rows.Count > 0 Then
            txtCanal.Value = dsOfi.Tables(0).Rows(0)("VTWEG")
        End If

    End Sub


    Private Function formatofecha(ByVal fecha As String) As String
        Dim fechaformat As String
        fechaformat = fecha.Substring(6, 4) & fecha.Substring(3, 2) & fecha.Substring(0, 2)
        Return fechaformat
    End Function





End Class
