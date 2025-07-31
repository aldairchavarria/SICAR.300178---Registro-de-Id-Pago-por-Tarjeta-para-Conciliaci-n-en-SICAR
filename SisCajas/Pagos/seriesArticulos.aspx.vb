Public Class seriesArticulos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnAgregar As System.Web.UI.WebControls.Button
    Protected WithEvents btnContinuar As System.Web.UI.WebControls.Button
    Protected WithEvents dgSeries As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCDesde As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCHasta As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim dtSeries As New DataTable("Series")
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            dtSeries.Columns.Add("Pos", GetType(System.String))
            dtSeries.Columns.Add("Desde", GetType(System.String))
            dtSeries.Columns.Add("Hasta", GetType(System.String))
            dtSeries.Columns.Add("Cantidad", GetType(System.String))

            btnAgregar.Attributes.Add("onClick", "f_Agregar()")

            If Not Page.IsPostBack Then
                dgSeries.DataSource = dtSeries
                dgSeries.DataBind()
        End If
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        AgregarLinea()
    End Sub

    Private Sub AgregarLinea()
        Dim strInicioSerie As String
        Dim strFinSerie As String
        Dim drFila As DataRow
        Dim i As Integer
        Dim j As Integer
        Dim strTrama As String

        strInicioSerie = Right("000000000000000000" + Trim(txtCDesde.Text), 18)  ' Captura el inicio de serie
        strFinSerie = Right("000000000000000000" + Trim(txtCHasta.Text), 18)  ' Captura el fin de serie

        For i = 0 To dgSeries.Items.Count - 1
            drFila = dtSeries.NewRow()
            For j = 1 To dgSeries.Items(i).Cells.Count - 1
                drFila.Item(j - 1) = dgSeries.Items(i).Cells(j).Text
            Next
            dtSeries.Rows.Add(drFila)
            If Len(Trim(strTrama)) > 0 Then
                strTrama = strTrama & "|"
            End If
            strTrama = strTrama & ";" & dgSeries.Items(i).Cells(1).Text & ";" & dgSeries.Items(i).Cells(2).Text & ";" & dgSeries.Items(i).Cells(3).Text
        Next

        If (strFinSerie <> "000000000000000000") Then
            If SeriesCheckBorder(strTrama, strInicioSerie, strFinSerie) Then
                drFila = dtSeries.NewRow()
                drFila("Pos") = Format(CDbl(Request.Item("strItem")), "000000")
                drFila("Desde") = strInicioSerie
                drFila("Hasta") = strFinSerie
                drFila("Cantidad") = (CDbl(strFinSerie) - CDbl(strInicioSerie)) + 1

                dtSeries.Rows.Add(drFila)
            Else
                Response.Write("<script language=javascript>alert('Existe un cruce de series con los campos Desde y Hasta')</script>")
            End If

        Else
            strFinSerie = ""
            If SeriesCheckBorder(strTrama, strInicioSerie, strInicioSerie) Then
                drFila = dtSeries.NewRow()
                drFila("Pos") = Format(CDbl(Request.Item("strItem")), "000000")
                drFila("Desde") = strInicioSerie
                drFila("Hasta") = strFinSerie
                drFila("Cantidad") = 1

                dtSeries.Rows.Add(drFila)
            Else
                Response.Write("<script language=javascript>alert('Existe un cruce de series con el Valor del campo Desde')</script>")
            End If
        End If
        dgSeries.DataSource = dtSeries
        dgSeries.DataBind()


    End Sub

    Private Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Dim drFila As DataRow
        Dim i As Integer
        Dim j As Integer
        Dim strTrama As String

        For i = 0 To dgSeries.Items.Count - 1
            drFila = dtSeries.NewRow()
            For j = 1 To dgSeries.Items(i).Cells.Count - 1
                drFila.Item(j - 1) = dgSeries.Items(i).Cells(j).Text
            Next
            dtSeries.Rows.Add(drFila)
            If Len(Trim(strTrama)) > 0 Then
                strTrama = strTrama & "|"
            End If
            strTrama = strTrama & ";" & dgSeries.Items(i).Cells(1).Text & ";" & dgSeries.Items(i).Cells(2).Text & ";" & dgSeries.Items(i).Cells(3).Text
        Next
        Session("strArti") = Request.Item("strArti")
        Session("strTrama") = strTrama
        Response.Redirect("ventaArticulos.aspx")
    End Sub


    Public Function SeriesCheckBorder(ByVal CSA As String, ByVal Dsd As String, ByVal Hst As String) As Boolean
        Dim i As Long
        Dim DLin As Object
        Dim DCol As Object

        SeriesCheckBorder = True
        If Len(Trim(CSA)) > 0 Then

            DLin = Split(CSA, "|")

            For i = 0 To UBound(DLin)
                DCol = Split(DLin(i), ";")
                If (DCol(3) <> "") Then
                    If (Dsd <= DCol(3)) And (Dsd >= DCol(2)) Then
                        SeriesCheckBorder = False
                    End If
                    If (Hst <= DCol(3)) And (Hst >= DCol(2)) Then
                        SeriesCheckBorder = False
                    End If
                    If (DCol(3) <= Hst) And (DCol(3) >= Dsd) Then
                        SeriesCheckBorder = False
                    End If
                End If
                If (DCol(2) <= Hst) And (DCol(2) >= Dsd) Then
                    SeriesCheckBorder = False
                End If
            Next
        End If
    End Function

    Private Sub dgSeries_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSeries.ItemCommand
        Dim drFila As DataRow
        Dim i, j As Integer
        Dim strPos As String

        For i = 0 To dgSeries.Items.Count - 1
            drFila = dtSeries.NewRow()
            For j = 1 To dgSeries.Items(i).Cells.Count - 1
                drFila(j - 1) = dgSeries.Items(i).Cells(j).Text
            Next
            dtSeries.Rows.Add(drFila)
        Next
        dtSeries.Rows.RemoveAt(e.Item.ItemIndex)

        For i = 0 To dtSeries.Rows.Count - 1
            dtSeries.Rows(i).Item(0) = Format(CDbl(Request.Item("strItem")), "000000")
        Next

        dgSeries.DataSource = dtSeries
        dgSeries.DataBind()
    End Sub
End Class
