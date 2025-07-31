Public Class FormaPagoExcel
    Inherits System.Web.UI.Page
    '' PROY-27440 INI  Pagina Nueva
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub
    Protected WithEvents dgFormasPago As System.Web.UI.WebControls.DataGrid
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub

#End Region
    Dim objLog As New SICAR_Log
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strNroPedido As String = ""
    Dim strIdentifyLog As String = ""
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            strNroPedido = Request.QueryString("NroPedido")
            strIdentifyLog = "FormaPagoExcel: [" + CStr(strNroPedido) + "] "
            llena_grid()

            Dim nombFile As String = "FormaPagos" + strNroPedido + ".xls"
            Response.Write("<script>alert('" & nombFile & " ');</script>")
            Response.AddHeader("Content-Disposition", "attachment;filename=" + nombFile)
            Response.ContentType = "application/vnd.ms-excel"


        End If
    End Sub

    Private Sub llena_grid()
        Dim strMetodo As String = String.Empty
        Try
            Dim obPagos As New COM_SIC_Activaciones.clsConsultaMsSap

            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ***********************************************************")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ******************* LLENA_GRID() - INICIO *******************")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "  ** INICIO ConsultarFormasPago == ")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "     PKG_SISCAJ_POS.SICASS_DETAPAGO ")
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "     IN Nro PEDIDO: " & strNroPedido)
            Dim dsDetalle As DataTable = obPagos.ConsultarFormasPago(strNroPedido)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "     OUT Total Pagos: " & dsDetalle.Rows.Count)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "  ** FIN ConsultarFormasPago == ")

            Dim dtFormasPagoTrans As New DataTable
            Dim dtFormasPagoTransVIS As New DataTable
            Dim dtFormasPagoTransMCD As New DataTable
            Dim dtFormasPagoTransAMX As New DataTable
            Dim dtFormasPagoTransDIN As New DataTable
            Dim i As Integer
            '' creacion de datatable Global fin
            Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
            Dim strCodRptTipPos As String = ""
            Dim strMsgRptTipPos As String = ""
            Dim strCodRptForm As String = ""
            Dim strMsgRptForm As String = ""
            Dim strTipoPago As String = ClsKeyPOS.strTipPagoDP '' 11 Documentos Pagados
            Dim strPDV As String = Session("ALMACEN")
            strMetodo = "ObtenerFormasDePagoTrans()"
            dtFormasPagoTransVIS = objSicarDB.ObtenerFormasDePagoTrans(strNroPedido, "VIS", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    IN: " & strNroPedido & "     IN: VIS     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)
            dtFormasPagoTransMCD = objSicarDB.ObtenerFormasDePagoTrans(strNroPedido, "MCD", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    IN: " & strNroPedido & "     IN: VIS     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)
            dtFormasPagoTransAMX = objSicarDB.ObtenerFormasDePagoTrans(strNroPedido, "AMX", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    IN: " & strNroPedido & "     IN: VIS     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)
            dtFormasPagoTransDIN = objSicarDB.ObtenerFormasDePagoTrans(strNroPedido, "DIN", "", strTipoPago, strCodRptForm, strMsgRptForm).Tables(0)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    IN: " & strNroPedido & "     IN: VIS     Tipo Pago: " & strTipoPago & "     OUT Cod.Rpta: " & strCodRptForm & "     OUT Msj. Rpta: " & strMsgRptForm)


            dsDetalle.Columns.Add("INDICE", GetType(String))
            dsDetalle.Columns.Add("EQUIVALENCIA", GetType(String))
            dsDetalle.Columns.Add("TRANSACCION_ID", GetType(String))
            dsDetalle.Columns.Add("FORMA_PAGO", GetType(String))
            dsDetalle.Columns.Add("NRO_REFERENCIAPOS", GetType(String))
            dsDetalle.Columns.Add("TIPO_TARJETA", GetType(String))
            dsDetalle.Columns.Add("NRO_TARJETA", GetType(String))
            dsDetalle.Columns.Add("TIPO_TARJETAPOS", GetType(String))
            dsDetalle.Columns.Add("ESTADO_ANULACION", GetType(String))
            dsDetalle.Columns.Add("RESULTADO_PROCESO", GetType(String))


            If Not dtFormasPagoTransVIS Is Nothing AndAlso dtFormasPagoTransVIS.Rows.Count > 0 Or _
               Not dtFormasPagoTransMCD Is Nothing AndAlso dtFormasPagoTransMCD.Rows.Count > 0 Or _
               Not dtFormasPagoTransAMX Is Nothing AndAlso dtFormasPagoTransAMX.Rows.Count > 0 Or _
               Not dtFormasPagoTransDIN Is Nothing AndAlso dtFormasPagoTransDIN.Rows.Count > 0 Then

                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    Total Pagos VISA: " & dtFormasPagoTransVIS.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    Total Pagos MCD: " & dtFormasPagoTransMCD.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    Total Pagos AMX: " & dtFormasPagoTransAMX.Rows.Count)
                objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "    Total Pagos DIN: " & dtFormasPagoTransDIN.Rows.Count)

                For i = 0 To dsDetalle.Rows.Count - 1

                    Dim strCodTarjSAP As String = dsDetalle.Rows(i).Item("DEPAV_FORMAPAGO")
                    strMetodo = "Obtener_TipoPOS()"
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " == " & " INICIO Obtener_TipoPOS == ")
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " PKG_SISCAJ_POS.SICASS_VIAS_PAGO == ")
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " IN Cod SAP: " & strCodTarjSAP)
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " IN Cod Oficina: " & strPDV)
                    Dim strResult As String = objSicarDB.Obtener_TipoPOS(strCodTarjSAP, strPDV, strCodRptTipPos, strMsgRptTipPos) ''WND - Cod PDV Hard ''SICASS_VIAS_PAGO
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " OUT Cod Rpta: " & strCodRptTipPos)
                    'INI PROY-140126
                    Dim MaptPath As String
                    MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
                    MaptPath = "( Class : " & MaptPath & "; Function: llena_grid)"
                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " OUT Msj Rpta: " & strMsgRptTipPos & MaptPath)
                    'FIN PROY-140126

                    objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " == " & " FIN Obtener_TipoPOS == ")


                    If strCodRptTipPos = "0" And (strResult.Split("#")(0)).Length > 0 Then
                        Dim strCodTarjEquival As String
                        Dim ArrFormasPago As ArrayList
                        strCodTarjEquival = strResult.Split("#")(0) '' MCD --> MCD#TARJETA CMR SAGA

                        dsDetalle.Rows.Item(i)("EQUIVALENCIA") = strCodTarjEquival '' MCD / VIS

                        If strCodTarjEquival = "VIS" Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                            dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransVIS.Rows(0).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                            dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransVIS.Rows(0).Item("TRNSV_ID_REF")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransVIS.Rows(0).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                            dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransVIS.Rows(0).Item("NRO_TARJETA")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransVIS.Rows(0).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                            dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransVIS.Rows(0).Item("ESTADO_ANULACION")
                            dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransVIS.Rows(0).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### VISA - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       Indice: " & i)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       FORMA_PAGO: " & dtFormasPagoTransVIS.Rows(0).Item("FORMA_PAGO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_REFERENCIAPOS: " & dtFormasPagoTransVIS.Rows(0).Item("TRNSV_ID_REF"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETA: " & dtFormasPagoTransVIS.Rows(0).Item("TIPO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_TARJETA: " & dtFormasPagoTransVIS.Rows(0).Item("NRO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETAPOS: " & dtFormasPagoTransVIS.Rows(0).Item("TIPO_TARJETA_POS"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       ESTADO_ANULACION: " & dtFormasPagoTransVIS.Rows(0).Item("ESTADO_ANULACION"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       RESULTADO_PROCESO: " & dtFormasPagoTransVIS.Rows(0).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### VISA - FIN ###")

                            dtFormasPagoTransVIS.Rows.RemoveAt(0)

                        ElseIf strCodTarjEquival = "MCD" Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                            dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransMCD.Rows(0).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                            dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransMCD.Rows(0).Item("TRNSV_ID_REF")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransMCD.Rows(0).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                            dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransMCD.Rows(0).Item("NRO_TARJETA")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransMCD.Rows(0).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                            dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransMCD.Rows(0).Item("ESTADO_ANULACION")
                            dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransMCD.Rows(0).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### MCD - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       Indice: " & i)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       FORMA_PAGO: " & dtFormasPagoTransMCD.Rows(0).Item("FORMA_PAGO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_REFERENCIAPOS: " & dtFormasPagoTransMCD.Rows(0).Item("TRNSV_ID_REF"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETA: " & dtFormasPagoTransMCD.Rows(0).Item("TIPO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_TARJETA: " & dtFormasPagoTransMCD.Rows(0).Item("NRO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETAPOS: " & dtFormasPagoTransMCD.Rows(0).Item("TIPO_TARJETA_POS"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       ESTADO_ANULACION: " & dtFormasPagoTransMCD.Rows(0).Item("ESTADO_ANULACION"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       RESULTADO_PROCESO: " & dtFormasPagoTransMCD.Rows(0).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### MCD - FIN ###")
                            dtFormasPagoTransMCD.Rows.RemoveAt(0)

                        ElseIf strCodTarjEquival = "AMX" Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                            dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransAMX.Rows(0).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                            dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransAMX.Rows(0).Item("TRNSV_ID_REF")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransAMX.Rows(0).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                            dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransAMX.Rows(0).Item("NRO_TARJETA")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransAMX.Rows(0).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                            dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransAMX.Rows(0).Item("ESTADO_ANULACION")
                            dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransAMX.Rows(0).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### AMX - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       Indice: " & i)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       FORMA_PAGO: " & dtFormasPagoTransAMX.Rows(0).Item("FORMA_PAGO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_REFERENCIAPOS: " & dtFormasPagoTransAMX.Rows(0).Item("TRNSV_ID_REF"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETA: " & dtFormasPagoTransAMX.Rows(0).Item("TIPO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_TARJETA: " & dtFormasPagoTransAMX.Rows(0).Item("NRO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETAPOS: " & dtFormasPagoTransAMX.Rows(0).Item("TIPO_TARJETA_POS"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       ESTADO_ANULACION: " & dtFormasPagoTransAMX.Rows(0).Item("ESTADO_ANULACION"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       RESULTADO_PROCESO: " & dtFormasPagoTransAMX.Rows(0).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### AMX - FIN ###")
                            dtFormasPagoTransAMX.Rows.RemoveAt(0)

                        ElseIf strCodTarjEquival = "DIN" Then
                            dsDetalle.Rows.Item(i)("INDICE") = i
                            dsDetalle.Rows.Item(i)("FORMA_PAGO") = dtFormasPagoTransDIN.Rows(0).Item("FORMA_PAGO") 'TARJETA VISA/TARJETA MASTERCARD /AMERICAN EXPRESS
                            dsDetalle.Rows.Item(i)("NRO_REFERENCIAPOS") = dtFormasPagoTransDIN.Rows(0).Item("TRNSV_ID_REF")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETA") = dtFormasPagoTransDIN.Rows(0).Item("TIPO_TARJETA") ' VISA/MASTERCARD
                            dsDetalle.Rows.Item(i)("NRO_TARJETA") = dtFormasPagoTransDIN.Rows(0).Item("NRO_TARJETA")
                            dsDetalle.Rows.Item(i)("TIPO_TARJETAPOS") = dtFormasPagoTransDIN.Rows(0).Item("TIPO_TARJETA_POS") 'VISA AMEX MASTERCARD
                            dsDetalle.Rows.Item(i)("ESTADO_ANULACION") = dtFormasPagoTransDIN.Rows(0).Item("ESTADO_ANULACION")
                            dsDetalle.Rows.Item(i)("RESULTADO_PROCESO") = dtFormasPagoTransDIN.Rows(0).Item("RESULTADO_PROCESO")

                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### DIN - INICIO ###")
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       Indice: " & i)
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       FORMA_PAGO: " & dtFormasPagoTransDIN.Rows(0).Item("FORMA_PAGO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_REFERENCIAPOS: " & dtFormasPagoTransDIN.Rows(0).Item("TRNSV_ID_REF"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETA: " & dtFormasPagoTransDIN.Rows(0).Item("TIPO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       NRO_TARJETA: " & dtFormasPagoTransDIN.Rows(0).Item("NRO_TARJETA"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       TIPO_TARJETAPOS: " & dtFormasPagoTransDIN.Rows(0).Item("TIPO_TARJETA_POS"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       ESTADO_ANULACION: " & dtFormasPagoTransDIN.Rows(0).Item("ESTADO_ANULACION"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & "       RESULTADO_PROCESO: " & dtFormasPagoTransDIN.Rows(0).Item("RESULTADO_PROCESO"))
                            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ### DIN - FIN ###")
                            dtFormasPagoTransDIN.Rows.RemoveAt(0)
                        End If
                    End If

                Next
            End If
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ******************* LLENA_GRID() - FIN *******************")
            dgFormasPago.DataSource = dsDetalle
            dgFormasPago.DataBind()
        Catch ex As Exception
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & "  = " & " Error en el método : " & strMetodo & " Mensaje Error: " & ex.Message)
            objLog.Log_WriteLog(pathFilePos, strArchivoPos, strIdentifyLog & " - " & " ******************* LLENA_GRID() - FIN *******************")
            Me.RegisterStartupScript("Catch", "<script language=javascript>alert('" + ex.Message.ToString() + "');</script>")
        End Try
    End Sub

End Class
