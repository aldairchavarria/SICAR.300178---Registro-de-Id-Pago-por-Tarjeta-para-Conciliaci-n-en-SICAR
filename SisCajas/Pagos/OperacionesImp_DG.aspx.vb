Public Class OperacionesImp_DG
    Inherits System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnImprimir As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents btnCancelar As System.Web.UI.HtmlControls.HtmlInputText

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Public strDocAutorizado As String = ""
    Public strCliente As String = ""
    Public strContacto As String = ""
    Public strDireccion As String = ""
    Public strDistrito As String = ""
    Public strProvincia As String = ""
    Public strDepartamento As String = ""
    Public strDocumento As String = ""
    Public strFechaEmision As String = ""
    Public strFechaVencimiento As String = ""
    Public strCodigoPago As String = ""
    Public strPuntoventa As String = ""
    Public strTelefono As String = ""
    Public strImporte As String = ""
    Public strSubTotal As String = ""
    Public strIgv As String = ""
    Public strTotalPago As String = ""
    Public strEtiquetaIGV As String = ""


    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public dtIGV As DataTable
    Dim IGVactual As String = "0"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncGenerales.js'></script>") 'PROY-140126
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Try
                Dim numDepGar As String = Request.QueryString("numDepGar")
                objFileLog.Log_WriteLog(pathFile, strArchivo, numDepGar & "- " & "Ingreso al Metodo Impresion, numDepGar: " & numDepGar)
                Impresion(numDepGar)
                'If numDepGar <> "" Then
                'Response.Write(" onload='window.parent.Imprimir();' ")
                'End If
            Catch ex As Exception
                Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
            End Try
        End If
    End Sub


    Private Sub Impresion(ByVal numDepGar As String)
        '**********************VARIABLES DE SESION
        Dim Impresion_SAP As String = Session("VarVal121")
        Dim CodImprTicket As String = Session("CodImprTicket")
        '**********
        'Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim obPVU As New COM_SIC_Activaciones.clsConsultaPvu
        Dim strIdentifyLog As String = numDepGar


        'Dim dsDG As DataSet = obPVU.ObtenerRentaAdelantada(numDepGar)
        Dim dsDG As DataSet = obPVU.ImpresionRentaAdelantada(numDepGar)
		dtIGV = Session("Lista_Impuesto")

        If dtIGV.Rows.Count = 0 Then
            Dim strScriptMensaje As String = "Error de IGV"
            strScriptMensaje = String.Format("<script language='javascript' type='text/javascript'>alert('No existe IGV configurado');</script>")
            If (Not Page.IsStartupScriptRegistered("jsValidacion")) Then Page.RegisterStartupScript("jsValidacion", strScriptMensaje)
            Exit Sub
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Impresion de Rentas Adelantadas CAC")
        Try
            If dsDG.Tables(0).Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Por lo Menos Mas de 1 Registro")

                strDocAutorizado = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DRAV_NRO_DRA"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strDocAutorizado: " & strDocAutorizado)

                strCliente = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("CLIENTE"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strCliente: " & strCliente)

                strContacto = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("CONTACTO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strContacto: " & strContacto)

                strDireccion = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DIR_COMPLETA"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strDireccion: " & strDireccion)

                strDistrito = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DISTV_DESCRIPCION"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strDistrito: " & strDistrito)

                strProvincia = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("PROVV_DESCRIPCION"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strProvincia: " & strProvincia)

                strDepartamento = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DEPAV_DESCRIPCION"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strDepartamento: " & strDepartamento)

                strDocumento = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DRAV_DOCID_CLIENTE"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strDocumento: " & strDocumento)

                'PROY-140397-MCKINSEY-> JSQ- INICIO
                strFechaEmision = String.Format("{0:dd/MM/yyyy}", dsDG.Tables(0).Rows(0)("DRAD_FECHA_CREA_MK"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strFechaEmision: " & strFechaEmision)
                strFechaVencimiento = String.Format("{0:dd/MM/yyyy}", dsDG.Tables(0).Rows(0)("DRAD_FECHA_CREA_MK"))
                'PROY-140397-MCKINSEY-> JSQ -FIN
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strFechaVencimiento: " & strFechaVencimiento)

                strCodigoPago = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("CODIGO_PAGO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strCodigoPago: " & strCodigoPago)

                strPuntoventa = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("OVENV_DESCRIPCION"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strPuntoventa: " & strPuntoventa)

                strTelefono = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("CLIEV_TEL_LEG"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strTelefono: " & strTelefono)

				
                Dim dtFecha As DateTime = Funciones.CheckDate(strFechaEmision)
                Dim igvDecimal As Decimal = 0.0
                For Each row As DataRow In dtIGV.Rows
                    If (dtFecha >= CDate(row("impudFecIniVigencia").ToString.Trim) And dtFecha <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
                        igvDecimal = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2)
                        IGVactual = CInt(igvDecimal * 100).ToString
                        Exit For
                    End If
                Next

                strEtiquetaIGV = "IGV " & IGVactual & " %"
                strImporte = (Double.Parse(dsDG.Tables(0).Rows(0)("DRAN_IMPORTE_DRA").ToString()) / (1 + igvDecimal)).ToString("0.00")
                strSubTotal = strImporte
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strSubTotal: " & strSubTotal)

                'strIgv = (Double.Parse(strSubTotal) * 18 / 100).ToString("0.00")
                strIgv = (Double.Parse(dsDG.Tables(0).Rows(0)("DRAN_IMPORTE_DRA").ToString()) - Double.Parse(strSubTotal)).ToString("0.00")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strIgv: " & strIgv)

                'strTotalPago = (Double.Parse(strSubTotal) + Double.Parse(strIgv)).ToString("0.00")
                strTotalPago = Funciones.CheckStr(dsDG.Tables(0).Rows(0)("DRAN_IMPORTE_DRA"))
                strTotalPago = Double.Parse(strTotalPago.ToString()).ToString("0.00")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "strTotalPago: " & strTotalPago)

            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error al consultar par imprimir rentas")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Impresion)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

        End Try
    End Sub

End Class
