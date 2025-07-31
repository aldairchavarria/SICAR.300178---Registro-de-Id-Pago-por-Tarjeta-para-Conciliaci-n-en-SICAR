Imports System.Globalization

Public Class ExcelCuadreCaja
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents tdCodTienda As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents tdDesTienda As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents dgCuadre As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objCuadre As New SAP_SIC_Pagos.clsPagos
    Dim objOffline As New COM_SIC_OffLine.clsOffline
    Dim dsReturn As DataSet
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
            'INICIO - Modificado por TS-CCC
            Dim strFecha As String
            Dim i As Integer
            Dim dvCuadre As New DataView
            Dim strCodUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))

            strFecha = CDate(Session("Fecha")).ToString("yyyyMMdd")

            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepCuadre")
            Dim flagSinergia As String
            Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)

            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            Dim dFecha As Date = DateTime.ParseExact(Session("Fecha"), "dd/MM/yyyy", CultureInfo.InvariantCulture)

            If Session("strTipoCuadre") = "I" Then
                If dFecha < dFechaLim Then
                    dsReturn = objCuadre.Get_CuadreCajaResumDia(Session("Fecha"), Session("ALMACEN"), Session("USUARIO"))
                Else
                    dsReturn = objOffline.GetMontoCuadre(CStr(Session("ALMACEN")), strFecha, strCodUsuario)
                End If
            Else
                If dFecha < dFechaLim Then
                    dsReturn = objCuadre.Get_CuadreCajaResumDia(Session("Fecha"), Session("ALMACEN"), "")
                Else
                    dsReturn = objOffline.GetMontoCuadre(CStr(Session("ALMACEN")), strFecha, "0")
                End If
            End If

            tdCodTienda.InnerHtml = "<b>&nbsp;" & Session("ALMACEN") & "</b>"          'temporalmente
            tdDesTienda.InnerHtml = "<b>" & Session("OFICINA") & "</b>"   'temporalmente


            Dim dsSap As New DataSet
            If dFecha < dFechaLim Then
                dsSap.Tables.Add(ConstruirTableReporte(dsReturn.Tables(0)))
            Else
                dsSap.Tables.Add(New DataTable)
            End If


            If Not IsNothing(dsReturn) Then
                If Session("strTipoCuadre") = "I" Then
                    If dFecha < dFechaLim Then
                        dvCuadre.Table = dsSap.Tables(0) 'dsReturn.Tables(0)
                        'dvCuadre.RowFilter = "TRIM(DSCUA)  <> TRIM('Monto Sobrante')" '"NUORD <> '0023'"
                        dvCuadre.RowFilter = "TRIM(DESC_CONCEPTO)  <> TRIM('Monto Sobrante')" '"TRIM(DSCUA)  <> TRIM('Monto Sobrante')"
                        dgCuadre.DataSource = dvCuadre
                    Else
                        dvCuadre.Table = dsReturn.Tables(0)
                        dgCuadre.DataSource = dvCuadre
                    End If
                Else
                    'dgCuadre.DataSource = dsReturn.Tables(0)
                    dgCuadre.DataSource = IIf(dFecha < dFechaLim, dsSap.Tables(0), dsReturn.Tables(0))
                End If
                dgCuadre.DataBind()
            End If
            'FIN - Modificado por TS-CCC
        End If
    End Sub


    Private Function ConstruirTableReporte(ByVal sapTable As DataTable) As DataTable
        Dim tbReturn As New DataTable
        tbReturn.Columns.Add("CONTADOR", GetType(String))
        tbReturn.Columns.Add("DESC_CONCEPTO", GetType(String))
        tbReturn.Columns.Add("MONTO", GetType(Double))
        Dim dr As DataRow
        For Each row As DataRow In sapTable.Rows
            dr = tbReturn.NewRow()
            dr("CONTADOR") = row("NUORD")
            dr("DESC_CONCEPTO") = row("DSCUA")
            dr("MONTO") = row("MONTO")
            tbReturn.Rows.Add(dr)
        Next
        Return tbReturn
    End Function

End Class
