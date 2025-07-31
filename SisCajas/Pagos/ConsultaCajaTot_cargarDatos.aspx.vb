Public Class ConsultaCajaTot_CargarDatos
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblFiltro As System.Web.UI.WebControls.Label
    Protected WithEvents txtFiltro As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents DGLista As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents hdnOrdenacion As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Private Const optTODOS As String = "0"
    Private Const SORT_ASCENDING As String = "ASC"
    Private Const SORT_DESCENDING As String = "DESC"
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
                If Not Request.QueryString("ov") Is Nothing Then
                    Session("ConsultDatosCuadCajTot") = Nothing
                    txtFiltro.Attributes.Add("onkeydown", "f_buscar();")
                    CargarDatos()
                End If
            End If
        End If
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Buscar()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        strURL = "ConsultaCajaTot.aspx"
        Session("ConsultDatosCuadCajTotOrdenado") = Nothing
        Session("ConsultDatosCuadCajTot") = Nothing
        Session("ConsultDatosCuadCajTotFill") = Nothing
        Response.Redirect(strURL)
    End Sub

    Private Sub DGLista_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DGLista.SortCommand
        Dim sortExpression As String = e.SortExpression
        Dim CurrentSortDirection As String
        CurrentSortDirection = CType(ViewState("SortDirection"), String)

        If CurrentSortDirection = SORT_ASCENDING Then
            ViewState("SortDirection") = SORT_DESCENDING
            hdnOrdenacion.Value = SORT_DESCENDING
            SortGridView(sortExpression, hdnOrdenacion.Value)
        Else
            ViewState("SortDirection") = SORT_ASCENDING
            hdnOrdenacion.Value = SORT_ASCENDING
            SortGridView(sortExpression, hdnOrdenacion.Value)
        End If
    End Sub

#End Region

#Region "Metodos"
    Private Sub CargarDatos()
        Try
            objclsAdmCaja = New COM_SIC_Adm_Cajas.clsAdmCajas
            Dim strOficinaVenta As String = Request.QueryString("ov")
            Dim strFechaInicio As String = Request.QueryString("fi")
            Dim strFechaFin As String = Request.QueryString("ff")
            Dim strCodCajero As String = Request.QueryString("cc")
            Dim strCaja As String = Request.QueryString("cj")
            Dim strEstado As String = Request.QueryString("st")
            Dim strDescripcion As String = Request.QueryString("ds")
            Dim strCntReg As String = Request.QueryString("cntReg")

            Dim dsDatos As DataSet = objclsAdmCaja.GetMontoCuadreConsultaTotal(strOficinaVenta, strFechaInicio, _
                                                    strFechaFin, strCodCajero, strCaja, strEstado, strDescripcion, strCntReg)
            Dim dsCajero As DataSet = objclsAdmCaja.GetVendedores(String.Empty, strOficinaVenta, optTODOS)

            Dim dtResult As New DataTable
            Dim drFila As DataRow

            dtResult.Columns.Add("OFICINA", GetType(System.String))
            dtResult.Columns.Add("FECHA", GetType(System.DateTime))
            dtResult.Columns.Add("CAJERO", GetType(System.String))
            dtResult.Columns.Add("DESCRIPCION", GetType(System.String))
            dtResult.Columns.Add("MONTO", GetType(System.Decimal))


            For i As Int32 = 0 To dsDatos.Tables(0).Rows.Count - 1
                drFila = dtResult.NewRow

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("OFICINA")) Then
                    drFila("OFICINA") = dsDatos.Tables(0).Rows(i).Item("OFICINA")
                Else
                    drFila("OFICINA") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("FECHA")) Then
                    drFila("FECHA") = CDate(dsDatos.Tables(0).Rows(i).Item("FECHA"))
                Else
                    drFila("FECHA") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("CAJERO")) Then
                    Dim cajero As String = CStr(dsDatos.Tables(0).Rows(i).Item("CAJERO"))
                    Dim dvCaj As New DataView
                    dvCaj.Table = dsCajero.Tables(0)
                    dvCaj.RowFilter = "CODIGO = '" & cajero & "'"
                    With dvCaj
                        drFila("CAJERO") = Trim(.Item(0).Item("DESCRIPCION"))
                    End With
                Else
                    drFila("CAJERO") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("DESC_CONCEPTO")) Then
                    drFila("DESCRIPCION") = dsDatos.Tables(0).Rows(i).Item("DESC_CONCEPTO")
                Else
                    drFila("DESCRIPCION") = String.Empty
                End If

                If Not IsDBNull(dsDatos.Tables(0).Rows(i).Item("MONTO")) Then
                    drFila("MONTO") = CDec(dsDatos.Tables(0).Rows(i).Item("MONTO"))
                Else
                    drFila("MONTO") = 0
                End If

                dtResult.Rows.Add(drFila)
            Next

            Session("ConsultDatosCuadCajTot") = dtResult
            Session("ConsultDatosCuadCajTotOrdenado") = Nothing
            Me.DGLista.DataSource = dtResult
            Me.DGLista.DataBind()

            If dtResult.Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No se encontró datos'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objclsAdmCaja = Nothing
        End Try
    End Sub

    Private Sub Buscar()
        Try
            Dim dt As DataTable = DirectCast(Session("ConsultDatosCuadCajTot"), DataTable)
            dt.TableName = "Datos"
            Dim dvFiltro As New DataView
            Dim strFiltro As String = Trim(txtFiltro.Value)
            dvFiltro.Table = dt

            dvFiltro.RowFilter = "CAJERO like " & "'%" & strFiltro & "%' OR " & " DESCRIPCION like " & "'%" & strFiltro & "%'"

            Session("ConsultDatosCuadCajTotFill") = dvFiltro
            Session("ConsultDatosCuadCajTotOrdenado") = Nothing
            Me.DGLista.DataSource = dvFiltro
            Me.DGLista.DataBind()
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As New DataView
        If Not Session("ConsultDatosCuadCajTot") Is Nothing Then
            dvConsulta = New DataView(CType(Session("ConsultDatosCuadCajTot"), DataTable))
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If

        Session("ConsultDatosCuadCajTotOrdenado") = dvConsulta
        Session("ConsultDatosCuadCajTotFill") = Nothing
    End Sub
#End Region

End Class
