Imports System.Globalization

Public Class rep_VentaDocExcel
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgReporte As System.Web.UI.WebControls.DataGrid

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
    Public bMostrar As Boolean = False

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
#End Region

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
            Dim strFecha As String
            Dim strUsuario As String
            Dim strIndividual As String
            Dim dsDatos As DataSet
            Dim objPagos As New SAP_SIC_Pagos.clsPagos

            strFecha = Request.Item("strFecha")
            strIndividual = Request.Item("Individual")

            If Len(Trim(strIndividual)) > 0 Then
                strUsuario = Session("USUARIO")
            Else
                strUsuario = ""
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicio CARGA EXCEL")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")

            'Fecha limite
            Dim flagSinergia As String
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
            'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepFactDet")
            Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
            Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
            Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
            Dim dFechaLim As New Date(anio, mes, dia)
            Dim dFecha As Date = DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "strUsuario -> " & strUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "OFICINA -> " & CStr(Session("ALMACEN")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FECHA LIM ->" & Funciones.CheckStr(dFechaLim))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FECHA ->" & Funciones.CheckStr(dFecha))


            If dFecha < dFechaLim Then
                bMostrar = True
                dsDatos = objPagos.Get_CuadreCajaVtasFact(strFecha, Session("ALMACEN"), strUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicio CARGA EXCEL IF 1")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO : Creando columnas del EXCEL ")
                Dim dtTblRpt As New DataTable
                With dtTblRpt
                    .Columns.Add("DSCOM", GetType(System.String))
                    .Columns.Add("VBELN", GetType(System.String))
                    .Columns.Add("XBLNR", GetType(System.String))
                    .Columns.Add("KUNNR", GetType(System.String))
                    .Columns.Add("WAERK", GetType(System.String))
                    .Columns.Add("FKART", GetType(System.String))
                    .Columns.Add("CUOTA", GetType(System.String))
                    .Columns.Add("TOTFA", GetType(System.Decimal))
                    .Columns.Add("ZEFE", GetType(System.Decimal))
                    .Columns.Add("ZAEX", GetType(System.Decimal))
                    .Columns.Add("ZCAR", GetType(System.Decimal))
                    .Columns.Add("ZCHQ", GetType(System.Decimal))
                    .Columns.Add("ZCIB", GetType(System.Decimal))
                    .Columns.Add("ZDEL", GetType(System.Decimal))
                    .Columns.Add("ZDIN", GetType(System.Decimal))
                    .Columns.Add("ZDMT", GetType(System.Decimal))
                    .Columns.Add("ZMCD", GetType(System.Decimal))
                    .Columns.Add("ZRIP", GetType(System.Decimal))
                    .Columns.Add("ZSAG", GetType(System.Decimal))
                    .Columns.Add("ZVIS", GetType(System.Decimal))
                    .Columns.Add("VIIN", GetType(System.Decimal))
                    .Columns.Add("ZCRS", GetType(System.Decimal))
                    .Columns.Add("ZCZO", GetType(System.Decimal))
                    .Columns.Add("ZVW1", GetType(System.Decimal))
                    .Columns.Add("ZVW2", GetType(System.Decimal))
                    .Columns.Add("ZACE", GetType(System.Decimal))
                    .Columns.Add("CUO1", GetType(System.Decimal))
                    .Columns.Add("CUO3", GetType(System.Decimal))
                    .Columns.Add("CUO6", GetType(System.Decimal))
                    .Columns.Add("CUO12", GetType(System.Decimal))
                    .Columns.Add("CUO18", GetType(System.Decimal))
                    .Columns.Add("CUO24", GetType(System.Decimal))
                    .Columns.Add("NODEF", GetType(System.Decimal))
                    .Columns.Add("ZEAM", GetType(System.Decimal))
                    .Columns.Add("ZEOV", GetType(System.Decimal))
                    .Columns.Add("SALDO", GetType(System.Decimal))
                End With
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN : Creando columnas del EXCEL ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO : CARGA DATOS")
                If dsDatos.Tables(0).Rows.Count > 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " dsDatos.Tables(0).Rows.Count -> " & Funciones.CheckStr(dsDatos.Tables(0).Rows.Count))
                    For Each row As DataRow In dsDatos.Tables(0).Rows
                        Dim newRow As DataRow
                        newRow = dtTblRpt.NewRow
                        newRow("DSCOM") = row("DSCOM")
                        newRow("VBELN") = row("VBELN")
                        newRow("XBLNR") = Mid(Trim(row("REFERENCIA")), 2)
                        newRow("KUNNR") = row("KUNNR")
                        newRow("WAERK") = row("WAERK")
                        newRow("FKART") = row("FKART")
                        newRow("CUOTA") = row("CUOTA")
                        newRow("TOTFA") = CDec(row("TOTFA"))
                        newRow("ZEFE") = CDec(row("ZEFE"))
                        newRow("ZAEX") = CDec(row("ZAEX"))
                        newRow("ZCAR") = CDec(row("ZCAR"))
                        newRow("ZCHQ") = CDec(row("ZCHQ"))
                        newRow("ZCIB") = CDec(row("ZCIB"))
                        newRow("ZDEL") = CDec(row("ZDEL"))
                        newRow("ZDIN") = CDec(row("ZDIN"))
                        newRow("ZDMT") = CDec(row("ZDMT"))
                        newRow("ZMCD") = CDec(row("ZMCD"))
                        newRow("ZRIP") = CDec(row("ZRIP"))
                        newRow("ZSAG") = CDec(row("ZSAG"))
                        newRow("ZVIS") = CDec(row("ZVIS"))
                        newRow("VIIN") = CDec(row("VIIN"))
                        newRow("ZCRS") = CDec(row("ZCRS"))
                        newRow("ZCZO") = CDec(row("ZCZO"))
                        newRow("ZVW1") = CDec(row("ZVW1"))
                        newRow("ZVW2") = CDec(row("ZVW2"))
                        newRow("ZACE") = CDec(row("ZACE"))
                        newRow("CUO1") = CDec(0)
                        newRow("CUO3") = CDec(row("CUO3"))
                        newRow("CUO6") = CDec(row("CUO6"))
                        newRow("CUO12") = CDec(row("CUO12"))
                        newRow("CUO18") = CDec(row("CUO18"))
                        newRow("CUO24") = CDec(0)
                        newRow("NODEF") = CDec(row("NODEF"))
                        newRow("ZEAM") = CDec(row("ZEAM"))
                        newRow("ZEOV") = CDec(row("ZEOV"))
                        newRow("SALDO") = CDec(row("SALDO"))
                        dtTblRpt.Rows.Add(newRow)
                    Next
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN : CARGA DATOS")
                dgReporte.DataSource = dtTblRpt
                dgReporte.DataBind()
            Else
                bMostrar = False
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicio CARGA EXCEL -> ELSE 1")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "strIndividual ->" & strIndividual)
                If Len(Trim(strIndividual)) > 0 Then
                    dsDatos = Session("rptFacturacionXCajero")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Session(rptFacturacionXCajero) -> " & Funciones.CheckStr(Session("rptFacturacionXCajero")))
                Else
                    dsDatos = Session("rptFacturacionXPdv")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Session(rptFacturacionXPdv) -> " & Funciones.CheckStr(Session("rptFacturacionXPdv")))
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO : Creando columnas del EXCEL ")

                Dim dtTblRpt As New DataTable
                With dtTblRpt
                    .Columns.Add("DSCOM", GetType(System.String))
                    .Columns.Add("VBELN", GetType(System.String))
                    .Columns.Add("XBLNR", GetType(System.String))
                    .Columns.Add("KUNNR", GetType(System.String))
                    .Columns.Add("WAERK", GetType(System.String))
                    .Columns.Add("FKART", GetType(System.String))
                    .Columns.Add("CUOTA", GetType(System.String))
                    .Columns.Add("TOTFA", GetType(System.Decimal))
                    .Columns.Add("ZEFE", GetType(System.Decimal))
                    .Columns.Add("ZAEX", GetType(System.Decimal))
                    .Columns.Add("ZCAR", GetType(System.Decimal))
                    .Columns.Add("ZCHQ", GetType(System.Decimal))
                    .Columns.Add("ZCIB", GetType(System.Decimal))
                    .Columns.Add("ZDEL", GetType(System.Decimal))
                    .Columns.Add("ZDIN", GetType(System.Decimal))
                    .Columns.Add("ZDMT", GetType(System.Decimal))
                    .Columns.Add("ZMCD", GetType(System.Decimal))
                    .Columns.Add("ZRIP", GetType(System.Decimal))
                    .Columns.Add("ZSAG", GetType(System.Decimal))
                    .Columns.Add("ZVIS", GetType(System.Decimal))
                    .Columns.Add("VIIN", GetType(System.Decimal))
                    .Columns.Add("ZCRS", GetType(System.Decimal))
                    .Columns.Add("ZCZO", GetType(System.Decimal))
                    .Columns.Add("ZVW1", GetType(System.Decimal))
                    .Columns.Add("ZVW2", GetType(System.Decimal))
                    .Columns.Add("ZACE", GetType(System.Decimal))
                    .Columns.Add("CUO1", GetType(System.Decimal))
                    .Columns.Add("CUO3", GetType(System.Decimal))
                    .Columns.Add("CUO6", GetType(System.Decimal))
                    .Columns.Add("CUO12", GetType(System.Decimal))
                    .Columns.Add("CUO18", GetType(System.Decimal))
                    .Columns.Add("CUO24", GetType(System.Decimal))
                    .Columns.Add("NODEF", GetType(System.Decimal))
                    .Columns.Add("ZEAM", GetType(System.Decimal))
                    .Columns.Add("ZEOV", GetType(System.Decimal))
                    .Columns.Add("SALDO", GetType(System.Decimal))
                End With
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN : Creando columnas del EXCEL ")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO : CARGA DATOS")
                If dsDatos.Tables(0).Rows.Count > 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " dsDatos.Tables(0).Rows.Count -> " & Funciones.CheckStr(dsDatos.Tables(0).Rows.Count))
                    For Each row As DataRow In dsDatos.Tables(0).Rows
                        Dim newRow As DataRow
                        newRow = dtTblRpt.NewRow
                        newRow("DSCOM") = row("DSCOM")
                        newRow("VBELN") = row("VBELN")
                        newRow("XBLNR") = row("REFERENCIA")
                        newRow("KUNNR") = row("KUNNR")
                        newRow("WAERK") = row("WAERK")
                        newRow("FKART") = row("FKART")
                        newRow("CUOTA") = row("CUOTA")
                        newRow("TOTFA") = CDec(row("TOTFA"))
                        newRow("ZEFE") = CDec(row("ZEFE"))
                        newRow("ZAEX") = CDec(row("ZAEX"))
                        newRow("ZCAR") = CDec(row("ZCAR"))
                        newRow("ZCHQ") = CDec(row("ZCHQ"))
                        newRow("ZCIB") = CDec(row("ZCIB"))
                        newRow("ZDEL") = CDec(row("ZDEL"))
                        newRow("ZDIN") = CDec(row("ZDIN"))
                        newRow("ZDMT") = CDec(row("ZDMT"))
                        newRow("ZMCD") = CDec(row("ZMCD"))
                        newRow("ZRIP") = CDec(row("ZRIP"))
                        newRow("ZSAG") = CDec(row("ZSAG"))
                        newRow("ZVIS") = CDec(row("ZVIS"))
                        newRow("VIIN") = CDec(0)
                        newRow("ZCRS") = CDec(row("ZCRS"))
                        newRow("ZCZO") = CDec(row("ZCZO"))
                        newRow("ZVW1") = CDec(0)
                        newRow("ZVW2") = CDec(0)
                        newRow("ZACE") = CDec(row("ZACE"))
                        newRow("CUO1") = CDec(row("CUO1"))
                        newRow("CUO3") = CDec(0)
                        newRow("CUO6") = CDec(row("CUO6"))
                        newRow("CUO12") = CDec(row("CUO12"))
                        newRow("CUO18") = CDec(row("CUO18"))
                        newRow("CUO24") = CDec(row("CUO24"))
                        newRow("NODEF") = CDec(0)
                        newRow("ZEAM") = CDec(row("ZEAM"))
                        newRow("ZEOV") = CDec(0)
                        newRow("SALDO") = CDec(row("SALDO"))
                        dtTblRpt.Rows.Add(newRow)
                    Next
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN : CARGA DATOS")
                dgReporte.DataSource = dtTblRpt
                dgReporte.DataBind()

            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN CARGA EXCEL")
    End Sub

    Private Sub dgReporte_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReporte.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(5).Style("mso-number-format") = "\@"
            e.Item.Cells(7).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(8).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(9).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(10).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(11).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(12).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(13).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(14).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(15).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(16).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(17).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(18).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(19).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(20).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(21).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(22).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(23).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(24).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(25).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(26).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(27).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(28).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(29).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(30).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(31).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(32).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(33).Style("mso-number-format") = "\#\,\#\#0\.00"
            e.Item.Cells(34).Style("mso-number-format") = "\#\,\#\#0\.00"
        End If
    End Sub
End Class
