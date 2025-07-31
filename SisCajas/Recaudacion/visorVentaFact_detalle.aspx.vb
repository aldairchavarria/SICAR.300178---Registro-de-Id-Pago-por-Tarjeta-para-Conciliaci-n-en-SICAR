Public Class visorVentaFact_detalle
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
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
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVisorVentaDet")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogVisorVentaDet")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strUsuario As String = String.Empty
    Dim strIdentifyLog As String = String.Empty
    Dim objClsOffline As COM_SIC_OffLine.clsOffline
    Private Const SORT_ASCENDING As String = "ASC"
    Private Const SORT_DESCENDING As String = "DESC"
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            strIdentifyLog = Session("ALMACEN") & "-" & Session("USUARIO") & " --- "
            If Not IsPostBack Then

                If Not Request.QueryString("of") Is Nothing Then
                    CargarDatos()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim strURL As String
        Session("TablaVntFacOrdenado") = Nothing
        Session("TablaVntFac") = Nothing
        strURL = "visorVentaFact.aspx"
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

#Region "Metodos"

    Private Sub CargarDatos()
        Try
            Dim strOficinaVta As String, strFechaIni As String, strFechaFin As String
            Dim strMtoPagado1 As String, strMtoPagado2 As String, strTipoDoc As String
            Dim strEstado As String, strDocSunat1 As String, strDocSunat2 As String
            Dim strCajero As String, strViaPago As String, strVendedor As String
            Dim strCuotas As String, strCntRegistros, strSubOficinaVta As String

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "--- Transac Pago - INI  CargarDatos")
        
            strOficinaVta = CStr(Request.QueryString("of"))
            strSubOficinaVta = CStr(Request.QueryString("subof"))
            strFechaIni = CStr(Request.QueryString("fi"))
            strFechaFin = CStr(Request.QueryString("ff"))
            strTipoDoc = CStr(Request.QueryString("td"))
            strDocSunat1 = CStr(Request.QueryString("ds1"))
            strDocSunat2 = CStr(Request.QueryString("ds2"))
            strMtoPagado1 = CStr(Request.QueryString("mp1"))
            strMtoPagado2 = CStr(Request.QueryString("mp2"))
            strCajero = CStr(Request.QueryString("cj"))
            strVendedor = CStr(Request.QueryString("vd"))
            strViaPago = CStr(Request.QueryString("vp"))
            strCuotas = CStr(Request.QueryString("cuo"))
            strEstado = CStr(Request.QueryString("est"))
            strCntRegistros = CStr(Request.QueryString("creg"))

            Dim dsResult As New DataSet
            Dim strRespuesta As String
            objClsOffline = New COM_SIC_OffLine.clsOffline


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strOficinaVta :" & strOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strSubOficinaVta :" & strSubOficinaVta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strFechaIni :" & strFechaIni)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strFechaFin :" & strFechaFin)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strTipoDoc :" & strTipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strDocSunat1 :" & strDocSunat1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strDocSunat2 :" & strDocSunat2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strMtoPagado1 :" & strMtoPagado1)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strMtoPagado2 :" & strMtoPagado2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strCajero :" & strCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strVendedor :" & strVendedor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strViaPago :" & strViaPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strCuotas :" & strCuotas)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strEstado :" & strEstado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strCntRegistros :" & strCntRegistros)

            dsResult = objClsOffline.GetVisFacturaDet(strOficinaVta, strSubOficinaVta, strFechaIni, strFechaFin, strTipoDoc, _
                            strDocSunat1, strDocSunat2, strMtoPagado1, strMtoPagado2, strCajero, _
                            strVendedor, strViaPago, strCuotas, strEstado, strCntRegistros, strRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "strRespuesta :" & strRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "Cursor Count :" & dsResult.Tables(0).Rows.Count)

            Dim dtTblResultado As DataTable
            Dim dtTblFacDet As New DataTable
            With dtTblFacDet
                .Columns.Add("DESC_DOCUMENTO", GetType(System.String))
                .Columns.Add("ORDEN", GetType(System.String))
                .Columns.Add("FECHA", GetType(System.String))
                .Columns.Add("FACTURA_FICTICIA", GetType(System.String))
                .Columns.Add("REFERENCIA", GetType(System.String))
                .Columns.Add("COD_VENDEDOR", GetType(System.String))
                .Columns.Add("VENDEDOR", GetType(System.String))
                .Columns.Add("CLASE_FACTURA_COD", GetType(System.String))
                .Columns.Add("CUOTA", GetType(System.Decimal))
                .Columns.Add("TOTFA", GetType(System.Decimal))
                .Columns.Add("ZAEX", GetType(System.Decimal))
                .Columns.Add("ZDIN", GetType(System.Decimal))
                .Columns.Add("ZEFE", GetType(System.Decimal))
                .Columns.Add("ZCAR", GetType(System.Decimal))
                .Columns.Add("ZCHQ", GetType(System.Decimal))
                .Columns.Add("ZACE", GetType(System.Decimal))
                .Columns.Add("ZCRS", GetType(System.Decimal))
                .Columns.Add("ZSAG", GetType(System.Decimal))
                .Columns.Add("ZCZO", GetType(System.Decimal))
                .Columns.Add("ZDMT", GetType(System.Decimal))
                .Columns.Add("ZMCD", GetType(System.Decimal))
                .Columns.Add("ZRIP", GetType(System.Decimal))
                .Columns.Add("ZVIS", GetType(System.Decimal))
                .Columns.Add("TDPP", GetType(System.Decimal))
                .Columns.Add("ZDEL", GetType(System.Decimal))
                .Columns.Add("ZCIB", GetType(System.Decimal))
                .Columns.Add("ZNCR", GetType(System.Decimal))
                .Columns.Add("ZEAM", GetType(System.Decimal))
                .Columns.Add("SALDO", GetType(System.Decimal))
                .Columns.Add("MONEDA", GetType(System.String))
                .Columns.Add("ESTADO", GetType(System.String))
                .Columns.Add("CUO1", GetType(System.Decimal))
                .Columns.Add("CUO6", GetType(System.Decimal))
                .Columns.Add("CUO12", GetType(System.Decimal))
                .Columns.Add("CUO18", GetType(System.Decimal))
                .Columns.Add("CUO24", GetType(System.Decimal))
                .Columns.Add("TIPO_DOCUMENTO", GetType(System.String))
                .Columns.Add("DES_ESTADO", GetType(System.String))
                .Columns.Add("CAJERO", GetType(System.String))
                .Columns.Add("OFICINA_VENTA", GetType(System.String))
                .Columns.Add("DESC_OFICINA", GetType(System.String))
                .Columns.Add("NOM_CAJERO", GetType(System.String))
                .Columns.Add("CASOV_SUB_OFICINA", GetType(System.String))
                .Columns.Add("SUB_OFICINA_DESC", GetType(System.String))
            End With

            If Not dsResult Is Nothing Then
                If dsResult.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In dsResult.Tables(0).Rows
                        Dim newRow As DataRow
                        newRow = dtTblFacDet.NewRow
                        newRow("DESC_DOCUMENTO") = row("DESC_DOCUMENTO")
                        newRow("ORDEN") = row("ORDEN")
                        newRow("FECHA") = row("FECHA")
                        newRow("FACTURA_FICTICIA") = row("FACTURA_FICTICIA")
                        newRow("REFERENCIA") = row("REFERENCIA")
                        newRow("COD_VENDEDOR") = row("COD_VENDEDOR")
                        newRow("VENDEDOR") = row("VENDEDOR")
                        newRow("CLASE_FACTURA_COD") = row("CLASE_FACTURA_COD")
                        newRow("CUOTA") = CDec(row("CUOTA"))
                        newRow("TOTFA") = CDec(row("TOTFA"))
                        newRow("ZAEX") = CDec(row("ZAEX"))
                        newRow("ZDIN") = CDec(row("ZDIN"))
                        newRow("ZEFE") = CDec(row("ZEFE"))
                        newRow("ZCAR") = CDec(row("ZCAR"))
                        newRow("ZCHQ") = CDec(row("ZCHQ"))
                        newRow("ZACE") = CDec(row("ZACE"))
                        newRow("ZCRS") = CDec(row("ZCRS"))
                        newRow("ZSAG") = CDec(row("ZSAG"))
                        newRow("ZCZO") = CDec(row("ZCZO"))
                        newRow("ZDMT") = CDec(row("ZDMT"))
                        newRow("ZMCD") = CDec(row("ZMCD"))
                        newRow("ZRIP") = CDec(row("ZRIP"))
                        newRow("ZVIS") = CDec(row("ZVIS"))
                        newRow("TDPP") = CDec(row("TDPP"))
                        newRow("ZDEL") = CDec(row("ZDEL"))
                        newRow("ZCIB") = CDec(row("ZCIB"))
                        newRow("ZNCR") = CDec(row("ZNCR"))
                        newRow("ZEAM") = CDec(row("ZEAM"))
                        newRow("SALDO") = CDec(row("SALDO"))
                        newRow("CUO1") = CDec(row("CUO1"))
                        newRow("CUO6") = CDec(row("CUO6"))
                        newRow("CUO12") = CDec(row("CUO12"))
                        newRow("CUO18") = CDec(row("CUO18"))
                        newRow("CUO24") = CDec(row("CUO24"))
                        newRow("CAJERO") = row("CAJERO")
                        newRow("NOM_CAJERO") = row("NOM_CAJERO")
                        newRow("MONEDA") = row("MONEDA")
                        newRow("ESTADO") = row("ESTADO")
                        newRow("TIPO_DOCUMENTO") = row("TIPO_DOCUMENTO")
                        newRow("DES_ESTADO") = row("DES_ESTADO")
                        newRow("DESC_OFICINA") = row("DESC_OFICINA")
                        newRow("OFICINA_VENTA") = row("OFICINA_VENTA")
                        newRow("CASOV_SUB_OFICINA") = row("CASOV_SUB_OFICINA")
                        newRow("SUB_OFICINA_DESC") = row("SUB_OFICINA_DESC")
                        dtTblFacDet.Rows.Add(newRow)
                    Next
                End If
            End If

            Dim dtSubTotal As New DataTable("SubTotales")
            dtSubTotal = dtTblFacDet.Clone()
            dtTblResultado = dtTblFacDet.Clone()

            Dim vOficina As String, vCajero As String, vFecha As String, vClase As String
            Dim vSubOficina As String
            Dim vCantidadEncontrados As Int32, vCantidadswitch As Int32 = 0, _
            vContador As Int32 = 1, vTotal As Int32 = 0

            vTotal = dtTblFacDet.Rows.Count()
            For Each fila As DataRow In dtTblFacDet.Rows()
                'Set Valores a Agrupar
                vOficina = fila("OFICINA_VENTA")
                vSubOficina = Funciones.CheckStr(fila("CASOV_SUB_OFICINA"))
                vCajero = fila("CAJERO") 'CStr(fila("CAJERO")).PadLeft(10, CChar("0"))
                vFecha = fila("FECHA")
                vClase = fila("CLASE_FACTURA_COD")

                If vContador = (vCantidadswitch + 1) Then
                    Dim filas As DataRow() = dtTblFacDet.Select("OFICINA_VENTA='" & vOficina & "'" & _
                                            " and CAJERO='" & vCajero & "'" & _
                                            " and FECHA='" & vFecha & "'" & _
                                            " and CLASE_FACTURA_COD= '" & vClase & "'" & _
                                            " and CASOV_SUB_OFICINA= '" & vSubOficina & "'")

                    If Not filas Is Nothing Then
                        'Agregando grupo a Temporal
                        vCantidadEncontrados = filas.Length()
                        vCantidadswitch += vCantidadEncontrados
                        Dim dtTem As New DataTable
                        dtTem = dtTblFacDet.Clone()
                        For Each item As DataRow In filas
                            dtTem.ImportRow(item)
                        Next
                        dtTem.AcceptChanges()

                        'Calculando SubTotales
                        Dim newSubtotal As DataRow
                        newSubtotal = dtSubTotal.NewRow()
                        newSubtotal("DESC_DOCUMENTO") = " - "
                        newSubtotal("ORDEN") = "2"
                        newSubtotal("FECHA") = fila("FECHA")
                        newSubtotal("FACTURA_FICTICIA") = " - "
                        newSubtotal("REFERENCIA") = " - "
                        newSubtotal("COD_VENDEDOR") = " - "
                        newSubtotal("VENDEDOR") = " - "
                        newSubtotal("CLASE_FACTURA_COD") = fila("CLASE_FACTURA_COD")
                        newSubtotal("CUOTA") = 0
                        newSubtotal("TOTFA") = dtTem.Compute("sum(TOTFA)", "")
                        newSubtotal("ZAEX") = dtTem.Compute("sum(ZAEX)", "")
                        newSubtotal("ZDIN") = dtTem.Compute("sum(ZDIN)", "")
                        newSubtotal("ZEFE") = dtTem.Compute("sum(ZEFE)", "")
                        newSubtotal("ZCAR") = dtTem.Compute("sum(ZCAR)", "")
                        newSubtotal("ZCHQ") = dtTem.Compute("sum(ZCHQ)", "")
                        newSubtotal("ZACE") = dtTem.Compute("sum(ZACE)", "")
                        newSubtotal("ZCRS") = dtTem.Compute("sum(ZCRS)", "")
                        newSubtotal("ZSAG") = dtTem.Compute("sum(ZSAG)", "")
                        newSubtotal("ZCZO") = dtTem.Compute("sum(ZCZO)", "")
                        newSubtotal("ZDMT") = dtTem.Compute("sum(ZDMT)", "")
                        newSubtotal("ZMCD") = dtTem.Compute("sum(ZMCD)", "")
                        newSubtotal("ZRIP") = dtTem.Compute("sum(ZRIP)", "")
                        newSubtotal("ZVIS") = dtTem.Compute("sum(ZVIS)", "")
                        newSubtotal("TDPP") = dtTem.Compute("sum(TDPP)", "")
                        newSubtotal("ZDEL") = dtTem.Compute("sum(ZDEL)", "")
                        newSubtotal("ZCIB") = dtTem.Compute("sum(ZCIB)", "")
                        newSubtotal("ZNCR") = dtTem.Compute("sum(ZNCR)", "")
                        newSubtotal("ZEAM") = dtTem.Compute("sum(ZEAM)", "")
                        newSubtotal("SALDO") = dtTem.Compute("sum(SALDO)", "")
                        newSubtotal("CUO1") = dtTem.Compute("sum(CUO1)", "")
                        newSubtotal("CUO6") = dtTem.Compute("sum(CUO6)", "")
                        newSubtotal("CUO12") = dtTem.Compute("sum(CUO12)", "")
                        newSubtotal("CUO18") = dtTem.Compute("sum(CUO18)", "")
                        newSubtotal("CUO24") = dtTem.Compute("sum(CUO24)", "")
                        newSubtotal("CAJERO") = fila("CAJERO")
                        newSubtotal("NOM_CAJERO") = fila("NOM_CAJERO")
                        newSubtotal("MONEDA") = " - "
                        newSubtotal("ESTADO") = " - "
                        newSubtotal("TIPO_DOCUMENTO") = "TOTAL " & fila("TIPO_DOCUMENTO")
                        newSubtotal("DES_ESTADO") = " - "
                        newSubtotal("DESC_OFICINA") = fila("DESC_OFICINA")
                        newSubtotal("OFICINA_VENTA") = fila("OFICINA_VENTA")
                        newSubtotal("CASOV_SUB_OFICINA") = Funciones.CheckStr(fila("CASOV_SUB_OFICINA"))
                        newSubtotal("SUB_OFICINA_DESC") = Funciones.CheckStr(fila("SUB_OFICINA_DESC"))
                        dtSubTotal.Rows.Add(newSubtotal)
                    End If
                End If

                If vContador = vTotal Then
                    'Calculando SubTotales
                    Dim newSubtotal As DataRow
                    newSubtotal = dtSubTotal.NewRow()
                    newSubtotal("DESC_DOCUMENTO") = " -- "
                    newSubtotal("ORDEN") = "3"
                    newSubtotal("FECHA") = " -- "
                    newSubtotal("FACTURA_FICTICIA") = " -- "
                    newSubtotal("REFERENCIA") = " -- "
                    newSubtotal("COD_VENDEDOR") = " -- "
                    newSubtotal("VENDEDOR") = " -- "
                    newSubtotal("CLASE_FACTURA_COD") = " -- "
                    newSubtotal("CUOTA") = 0
                    newSubtotal("TOTFA") = dtTblFacDet.Compute("sum(TOTFA)", "")
                    newSubtotal("ZAEX") = dtTblFacDet.Compute("sum(ZAEX)", "")
                    newSubtotal("ZDIN") = dtTblFacDet.Compute("sum(ZDIN)", "")
                    newSubtotal("ZEFE") = dtTblFacDet.Compute("sum(ZEFE)", "")
                    newSubtotal("ZCAR") = dtTblFacDet.Compute("sum(ZCAR)", "")
                    newSubtotal("ZCHQ") = dtTblFacDet.Compute("sum(ZCHQ)", "")
                    newSubtotal("ZACE") = dtTblFacDet.Compute("sum(ZACE)", "")
                    newSubtotal("ZCRS") = dtTblFacDet.Compute("sum(ZCRS)", "")
                    newSubtotal("ZSAG") = dtTblFacDet.Compute("sum(ZSAG)", "")
                    newSubtotal("ZCZO") = dtTblFacDet.Compute("sum(ZCZO)", "")
                    newSubtotal("ZDMT") = dtTblFacDet.Compute("sum(ZDMT)", "")
                    newSubtotal("ZMCD") = dtTblFacDet.Compute("sum(ZMCD)", "")
                    newSubtotal("ZRIP") = dtTblFacDet.Compute("sum(ZRIP)", "")
                    newSubtotal("ZVIS") = dtTblFacDet.Compute("sum(ZVIS)", "")
                    newSubtotal("TDPP") = dtTblFacDet.Compute("sum(TDPP)", "")
                    newSubtotal("ZDEL") = dtTblFacDet.Compute("sum(ZDEL)", "")
                    newSubtotal("ZCIB") = dtTblFacDet.Compute("sum(ZCIB)", "")
                    newSubtotal("ZNCR") = dtTblFacDet.Compute("sum(ZNCR)", "")
                    newSubtotal("ZEAM") = dtTblFacDet.Compute("sum(ZEAM)", "")
                    newSubtotal("SALDO") = dtTblFacDet.Compute("sum(SALDO)", "")
                    newSubtotal("CUO1") = dtTblFacDet.Compute("sum(CUO1)", "")
                    newSubtotal("CUO6") = dtTblFacDet.Compute("sum(CUO6)", "")
                    newSubtotal("CUO12") = dtTblFacDet.Compute("sum(CUO12)", "")
                    newSubtotal("CUO18") = dtTblFacDet.Compute("sum(CUO18)", "")
                    newSubtotal("CUO24") = dtTblFacDet.Compute("sum(CUO24)", "")
                    newSubtotal("CAJERO") = " -- "
                    newSubtotal("NOM_CAJERO") = " -- "
                    newSubtotal("MONEDA") = " -- "
                    newSubtotal("ESTADO") = " -- "
                    newSubtotal("TIPO_DOCUMENTO") = "TOTAL TOTAL "
                    newSubtotal("DES_ESTADO") = " -- "
                    newSubtotal("DESC_OFICINA") = " -- "
                    newSubtotal("OFICINA_VENTA") = "ZZZ" ' Solo por orden lleva este valor, no se muestra.
                    newSubtotal("SUB_OFICINA_DESC") = "---"
                    newSubtotal("CASOV_SUB_OFICINA") = "ZZZ" ' Solo por orden lleva este valor, no se muestra.
                    dtSubTotal.Rows.Add(newSubtotal)
                End If
                vContador += 1
            Next

            'Detalle
            For Each detalle As DataRow In dtTblFacDet.Rows
                dtTblResultado.ImportRow(detalle)
            Next
            dtTblResultado.AcceptChanges()

            'Total y SubTotales
            For Each subtotal As DataRow In dtSubTotal.Rows
                dtTblResultado.ImportRow(subtotal)
            Next

            Dim dvResultado As DataView = dtTblResultado.DefaultView
            dvResultado.Sort = "OFICINA_VENTA, CASOV_SUB_OFICINA, FECHA, CAJERO, CLASE_FACTURA_COD, ORDEN"

            Session("TablaVntFac") = dvResultado
            Session("TablaVntFacOrdenado") = Nothing
            DGLista.DataSource = dvResultado
            DGLista.DataBind()

            If dtSubTotal.Rows.Count = 0 Then
                Response.Write("<script language=jscript> alert('No se encontraron registros'); </script>")
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Feact Det - Error Mensaje: " & ex.Message)
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fact Det - FIN  CargarDatos")
            objClsOffline = Nothing
        End Try
    End Sub

    Private Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)
        Dim dvConsulta As DataView
        If Not Session("TablaVntFac") Is Nothing Then
            dvConsulta = CType(Session("TablaVntFac"), DataView)
            If hdnOrdenacion.Value <> "" Then
                dvConsulta.Sort = sortExpression + " " + direction
            End If
            DGLista.DataSource = dvConsulta
            DGLista.DataBind()
        End If
        Session("TablaVntFacOrdenado") = dvConsulta
    End Sub

#End Region
End Class
