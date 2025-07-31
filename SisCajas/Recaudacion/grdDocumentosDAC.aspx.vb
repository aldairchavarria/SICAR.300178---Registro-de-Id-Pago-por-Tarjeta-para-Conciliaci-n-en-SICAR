Public Class grdDocumentosDAC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgrRecauda As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecha As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents cmdBuscar As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents cmdImprimir As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtTrama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMonto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPDV As System.Web.UI.WebControls.TextBox 'PROY-26366
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox 'PROY-26366
    Protected WithEvents txtFechaRegistro As System.Web.UI.WebControls.TextBox 'PROY-26366

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
    Public objFileLog As New SICAR_Log
    Public nameFileDac As String = ConfigurationSettings.AppSettings("constNameLogRecaudacionDAC")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivoDac As String = objFileLog.Log_CrearNombreArchivo(nameFileDac)
#End Region

    Dim objRecaud As New SAP_SIC_Recaudacion.clsRecaudacion
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
            ''MODIFICADO POR CCC-TS
            If Not Page.IsPostBack Then
                If txtFecha.Value = String.Empty Then
                    txtFecha.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Today.ToString("d")
                End If
                Buscar()
            End If
            txtTrama.Text = String.Empty
        End If
    End Sub

    Private Sub cmdImprimir_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimir.ServerClick
        Dim dsConsulta As DataSet
        Dim strDealer As String
        Dim dblMonto As Double
        Dim strTrama As String
        Dim i As Integer

        Try
            '' Modificado por CCC-TS
            Dim objOffLine As New COM_SIC_OffLine.clsOffline
            Dim flagSinergia As String
            Dim strFechaLim As String = objOffLine.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFCDAC")
            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            'PROY-26366 - FASE I - INICIO
            Dim PDV As String
            Dim Cajero As String
            Dim FechaRegistro As String
            'PROY-26366 - FASE I - FIN
            If CDate(txtFecha.Value) < dFechaLim Then
                dsConsulta = objRecaud.Get_ConsultaPagoDAC(Request.Item("rdoDocumentoSAP"))

                strDealer = dsConsulta.Tables(0).Rows(0).Item("Kunnr") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("Name1"), "&", " ")

                strTrama = ""

                For i = 0 To dsConsulta.Tables(0).Rows.Count - 1
                    strTrama &= dsConsulta.Tables(0).Rows(i).Item("VTEXT") & ";|"
                    dblMonto += dsConsulta.Tables(0).Rows(i).Item("MONTO")
                Next

            Else
                dsConsulta = objOffLine.GetRecaudacionDACDetalle(Request.Item("rdoDocumentoSAP"), Request.Item("hddID"))
                strDealer = dsConsulta.Tables(0).Rows(0).Item("COD_CLIENTE") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("CLIENTE"), "&", " ")

                strTrama = ""
                'PROY-26366 - FASE I - INICIO
                PDV = dsConsulta.Tables(0).Rows(0).Item("VKBUR") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("BEZEI"), "&", " ")
                Cajero = dsConsulta.Tables(0).Rows(0).Item("USUARIO") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("NOMBRE"), "&", " ")
                FechaRegistro = dsConsulta.Tables(0).Rows(0).Item("FECHA") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("HORA"), "&", " ")
                'PROY-26366 - FASE I - FIN
                For i = 0 To dsConsulta.Tables(0).Rows.Count - 1
                    strTrama &= dsConsulta.Tables(0).Rows(i).Item("VTEXT") & ";|"
                    dblMonto += dsConsulta.Tables(0).Rows(i).Item("MONTO")
                Next

            End If

            If Len(strTrama) > 0 Then
                strTrama = Left(strTrama, Len(strTrama) - 1)
            End If

            txtTrama.Text = strTrama
            txtMonto.Text = dblMonto
            txtDealer.Text = strDealer
            'PROY-26366 - FASE I - INICIO
            txtPDV.Text = PDV
            txtCajero.Text = Cajero
            txtFechaRegistro.Text = FechaRegistro
            'PROY-26366 - FASE I - FIN
            Buscar()
            'Response.Write("<script language=javascript>window.open('docRecaudacionDAC.aspx?Dealer=" & strDealer & "&MontoTotalPagado=" & dblMonto & "&strTrama=" & strTrama & "','docRecaudacion','menubar=false,width=325,height=420')</script>")
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub

    ''AGREGADO POR CCC-TS

    Private Sub Buscar()

        Dim strOficina As String = Session("ALMACEN")
        Dim strUsuario As String = Session("USUARIO")
        'Dim dsPool As DataSet
        Dim dvFiltro As New DataView
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Try
            Dim flagSinergia As String
            Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFCDAC")
            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))

            Dim dFechaLim As New Date(anio, mes, dia)

            objFileLog.Log_WriteLog(pathFile, strArchivoDac, "METODO DE CONSULTA " & "- " & "Buscar - IN Usuario: " & strUsuario & " Fecha: " & txtFecha.Value & " Oficina: " & strOficina)
            Dim dsPool As DataSet

            If CDate(txtFecha.Value) < dFechaLim Then
                dsPool = objRecaud.Get_PoolDAC(strOficina, Session("USUARIO"), txtFecha.Value)

                Dim dtTblDac As New DataTable("Datos")
                With dtTblDac
                    .Columns.Add("id_t_nro_recaudacion_dac", GetType(System.String))
                    .Columns.Add("NROAT", GetType(System.String))
                    .Columns.Add("COD_CLIENTE", GetType(System.String))
                    .Columns.Add("CLIENTE", GetType(System.String))
                    .Columns.Add("monto", GetType(System.String))
                    .Columns.Add("Oficina", GetType(System.String))
                    .Columns.Add("rectp", GetType(System.String))
                    .Columns.Add("estado", GetType(System.String))
                    .Columns.Add("usuario_registro", GetType(System.String))
                    .Columns.Add("fecha_registro", GetType(System.String))
                End With

                If dsPool.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In dsPool.Tables(0).Rows
                        Dim newRow As DataRow
                        newRow = dtTblDac.NewRow
                        newRow("id_t_nro_recaudacion_dac") = 0
                        newRow("NROAT") = row("Nroat")
                        newRow("COD_CLIENTE") = row("Kunnr")
                        newRow("CLIENTE") = row("Name1")
                        newRow("monto") = row("Monto")
                        newRow("Oficina") = row("Bktxt")
                        newRow("rectp") = row("Rectp")
                        newRow("estado") = ""
                        newRow("usuario_registro") = row("Uname")
                        newRow("fecha_registro") = ""

                        dtTblDac.Rows.Add(newRow)
                    Next
                End If

                dvFiltro.Table = dtTblDac
                dvFiltro.RowFilter = "rectp<>'2'"

            Else
                dsPool = objOffline.GetPoolRecaudacionDAC(strOficina, strUsuario, txtFecha.Value)
                dvFiltro.Table = dsPool.Tables(0)
                dvFiltro.RowFilter = "rectp<>'2'"
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivoDac, "METODO DE CONSULTA " & "- " & "Buscar - Cantidad de Registros: " & dsPool.Tables(0).Rows.Count.ToString())

            dgrRecauda.DataSource = dvFiltro
            dgrRecauda.DataBind()
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objOffline = Nothing
        End Try
    End Sub

    Private Sub cmdBuscar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.ServerClick
        Try
            Buscar()
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        End Try
    End Sub
End Class
