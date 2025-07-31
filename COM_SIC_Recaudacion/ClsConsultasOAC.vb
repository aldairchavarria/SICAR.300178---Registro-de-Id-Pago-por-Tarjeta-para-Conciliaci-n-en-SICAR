Imports System.Text
Imports System.Configuration
Imports SwichTransaccional.ConsultaPagoOAC

Public Class ClsConsultasOAC
    Public sbLineasLog As New StringBuilder
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim oWsOAC As New ConsultaPagos

    Public Sub New()
        oWsOAC.Url = CStr(ConfigurationSettings.AppSettings("ServiciosOACURL")) 'ServiciosOACURL
        oWsOAC.Credentials = System.Net.CredentialCache.DefaultCredentials
        Dim iTimeOut As Int32 = 0
        Dim strTimeOut As String = ConfigurationSettings.AppSettings("constSICARConsultasST_Timeout")
        If Not strTimeOut Is Nothing Then
            If strTimeOut <> "" Then
                iTimeOut = Convert.ToInt32(strTimeOut)
            End If
        End If
        oWsOAC.Timeout = iTimeOut
    End Sub
    Private Sub generateColumns(ByRef oTable As DataTable)
        oTable.Columns.Add(New DataColumn("DOCV_COD_CUENTA", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCV_RAZONSOCIAL", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCV_TELFREF", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCV_RECIBO", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCD_FECHA_EMISION", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCD_FECHA_VENCIMIENTO", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCN_COD_MONEDA", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCV_TIPO_SERVICIO", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCN_IMPORTE", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCN_TOTAL", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCN_FLAGGRUPO", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCN_FLAGDETALLE", GetType(String)))
        oTable.Columns.Add(New DataColumn("DOCV_TIPO_SERVICIO_DOC", GetType(String)))
    End Sub
    Private Function getCuentaExiste(ByVal oTable As DataTable, _
                                     ByVal oCuenta As String) As Boolean
        Dim retorno As Boolean = False
        Dim oFila As DataRow
        For Each oFila In oTable.Rows
            If (oFila("DOCV_COD_CUENTA") = oCuenta) Then
                retorno = True
                Exit For
            End If
        Next
        Return retorno
    End Function
    Private Function getCuentaTotal(ByVal xDeudaDocs As Object, _
                                    ByVal oCuenta As String) As String
        Dim total As Double = 0
        Dim i As Integer
        For i = 0 To xDeudaDocs.Length - 1
            If (xDeudaDocs(i).xDatoDocumento.Trim() = oCuenta) Then
                total = total + xDeudaDocs(i).xMontoDebe
            End If
        Next
        Return total.ToString("0.00")
    End Function
    Public Function Get_Deuda_DNI(ByVal P_ID_TRANSACCION$, _
                                    ByVal P_COD_APLICACION$, _
                                    ByVal P_COD_BANCO$, _
                                    ByVal P_COD_REENVIA$, _
                                    ByVal P_COD_MONEDA$, _
                                    ByVal P_COD_TIPO_SERV$, _
                                    ByVal P_POS_ULT_DOCUM$, _
                                    ByVal P_COD_TIPO_IDENT$, _
                                    ByVal P_NUM_DOCUMENTO_ID$, _
                                    ByVal P_NOMBRE_COMERCIO$, _
                                    ByVal P_NUMERO_COMERCIO$, _
                                    ByVal P_COD_AGENCIA$, _
                                    ByVal P_CANAL$, _
                                    ByVal P_COD_CIUDAD$, _
                                    ByVal P_COD_TERMINAL$, _
                                    ByVal P_COD_PLAZA$, _
                                    ByVal P_NRO_REFERENCIA$, _
                                    ByRef status$, _
                                    ByRef message$, _
                                    ByRef strDescripcionRptaAux$) As DataTable
        Dim auditType As New SwichTransaccional.ConsultaPagoOAC.AuditType
        Dim deudaClienteArray() As SwichTransaccional.ConsultaPagoOAC.DeudaDocIdServicioType
        Dim productoIdentificacion As String
        Dim nombreCliente As String
        Dim flagMasDocumentos As String
        Dim sPosUltimoDocumento As String
        Dim IdentificacionCliente As String
        Dim cantidadServicios As Decimal
        Dim cantidadDocumentosDeuda As Decimal
        Dim datoTransaccion As String

        objFileLog.Log_WriteLog(pathFile, strArchivo, "---------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "URL WS : " & oWsOAC.Url)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Metodo : " & "consultaDeudaDocId")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Iniciando Método GET_DEUDA_DNI - Clase ClsConsultasOAC")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "---------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "------------------   PARAMETROS DE ENTRADA    -------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_IDTRANSACCION    : " & P_ID_TRANSACCION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_APLICACION   : " & P_COD_APLICACION)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_BANCO        : " & P_COD_BANCO)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_REENVIA      : " & P_COD_REENVIA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_MONEDA       : " & P_COD_MONEDA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_TIPO_SERV    : " & P_COD_TIPO_SERV)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_POS_ULT_DOCUM    : " & P_POS_ULT_DOCUM)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_TIPO_IDENT   : " & P_COD_TIPO_IDENT)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_NUM_DOCUMENTO_ID : " & P_NUM_DOCUMENTO_ID)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_NOMBRE_COMERCIO  : " & P_NOMBRE_COMERCIO)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_NUMERO_COMERCIO  : " & P_NUMERO_COMERCIO)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_AGENCIA      : " & P_COD_AGENCIA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_CANAL            : " & P_CANAL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_CIUDAD       : " & P_COD_CIUDAD)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_TERMINAL     : " & P_COD_TERMINAL)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_COD_PLAZA        : " & P_COD_PLAZA)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "INPUT - P_NRO_REFERENCIA   : " & P_NRO_REFERENCIA)

        Dim oTableTmp As New DataTable

        Try

            auditType = oWsOAC.consultaDeudaDocId(P_ID_TRANSACCION, _
                                                         P_COD_APLICACION, _
                                                         P_COD_BANCO, _
                                                         P_COD_REENVIA, _
                                                         P_COD_MONEDA, _
                                                         P_COD_TIPO_SERV, _
                                                         P_POS_ULT_DOCUM, _
                                                         P_COD_TIPO_IDENT, _
                                                         P_NUM_DOCUMENTO_ID, _
                                                         P_NOMBRE_COMERCIO, _
                                                         P_NUMERO_COMERCIO, _
                                                         P_COD_AGENCIA, _
                                                         P_CANAL, _
                                                         P_COD_CIUDAD, _
                                                         P_COD_TERMINAL, _
                                                         P_COD_PLAZA, _
                                                         P_NRO_REFERENCIA, _
                                                         productoIdentificacion, _
                                                         nombreCliente, _
                                                         flagMasDocumentos, _
                                                         sPosUltimoDocumento, _
                                                         P_NRO_REFERENCIA, _
                                                         IdentificacionCliente, _
                                                         cantidadServicios, _
                                                         cantidadDocumentosDeuda, _
                                                         datoTransaccion, _
                                                         deudaClienteArray, _
                                                         status, _
                                                         message)
            If Not status Is Nothing And Convert.ToInt32(status) = 0 Then
                generateColumns(oTableTmp)
                Dim oDeuda As SwichTransaccional.ConsultaPagoOAC.DeudaDocIdServicioType
                Dim oIndex As Integer = 1
                Dim oFlagGrupo As Integer = 0
                For Each oDeuda In deudaClienteArray
                    Dim oDocumento As SwichTransaccional.ConsultaPagoOAC.DeudaDocIdDocumentoType
                    For Each oDocumento In oDeuda.xDeudaDocs
                        Dim oCuentaPadre As String = ""
                        If (getCuentaExiste(oTableTmp, oDocumento.xDatoDocumento.Trim()) = False) Then
                            Dim oFilaPadre As DataRow = oTableTmp.NewRow
                            oCuentaPadre = oDocumento.xDatoDocumento.Trim()
                            oFlagGrupo = oFlagGrupo + 1
                            oFilaPadre("DOCV_COD_CUENTA") = oDocumento.xDatoDocumento.Trim()
                            oFilaPadre("DOCV_RAZONSOCIAL") = nombreCliente.Trim()
                            oFilaPadre("DOCV_TELFREF") = oDocumento.xReferenciaDeuda.Trim()
                            oFilaPadre("DOCV_RECIBO") = ""
                            oFilaPadre("DOCD_FECHA_EMISION") = ""
                            oFilaPadre("DOCD_FECHA_VENCIMIENTO") = ""
                            oFilaPadre("DOCN_COD_MONEDA") = oDocumento.xCodConcepto1.Trim()
                            oFilaPadre("DOCV_TIPO_SERVICIO") = ""
                            oFilaPadre("DOCN_IMPORTE") = 0
                            oFilaPadre("DOCN_TOTAL") = getCuentaTotal(oDeuda.xDeudaDocs, oDocumento.xDatoDocumento.Trim())
                            oFilaPadre("DOCN_FLAGDETALLE") = "0"
                            oFilaPadre("DOCV_TIPO_SERVICIO_DOC") = ""
                            oFilaPadre("DOCN_FLAGGRUPO") = oFlagGrupo

                            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------PADRE-------------------------------------")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "CUENTA PADRE      : " & oDocumento.xDatoDocumento.Trim())
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "RAZON SOCIAL      : " & nombreCliente.ToString())
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "FECHA_EMISION     : " & "")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "FECHA_VENCIMIENTO : " & "")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "COD_MONEDA        : " & oDocumento.xCodConcepto1.ToString())
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "TOTAL             : " & oFilaPadre("DOCN_TOTAL"))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "GRUPO             : " & oFlagGrupo.ToString())
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------------------")

                            oTableTmp.Rows.Add(oFilaPadre)
                        End If

                        Dim oDocumentoHijo As SwichTransaccional.ConsultaPagoOAC.DeudaDocIdDocumentoType
                        For Each oDocumentoHijo In oDeuda.xDeudaDocs
                            Dim oCuentaHijo As String = oDocumentoHijo.xDatoDocumento.Trim()
                            If (oCuentaPadre <> "") Then
                                If (oCuentaPadre = oCuentaHijo) Then
                                    Dim oFilaHijo As DataRow = oTableTmp.NewRow
                                    oFilaHijo("DOCV_COD_CUENTA") = oDocumentoHijo.xDatoDocumento.Trim()
                                    oFilaHijo("DOCV_RAZONSOCIAL") = nombreCliente.Trim()
                                    oFilaHijo("DOCV_TELFREF") = oDocumentoHijo.xReferenciaDeuda.Trim()
                                    oFilaHijo("DOCV_RECIBO") = oDocumentoHijo.xNumeroDoc.Trim()
                                    oFilaHijo("DOCD_FECHA_EMISION") = oDocumentoHijo.xFechaEmision.ToShortDateString()
                                    oFilaHijo("DOCD_FECHA_VENCIMIENTO") = oDocumentoHijo.xFechaVenc.ToShortDateString()
                                    oFilaHijo("DOCN_COD_MONEDA") = oDocumentoHijo.xCodConcepto1.Trim()
                                    oFilaHijo("DOCV_TIPO_SERVICIO") = oDocumentoHijo.xCodConcepto2.Trim()
                                    oFilaHijo("DOCN_IMPORTE") = oDocumentoHijo.xMontoDebe.ToString("0.00")
                                    oFilaHijo("DOCN_TOTAL") = 0
                                    oFilaHijo("DOCN_FLAGDETALLE") = "1"
                                    oFilaHijo("DOCV_TIPO_SERVICIO_DOC") = oDocumentoHijo.xTipoServicio.Trim()
                                    oFilaHijo("DOCN_FLAGGRUPO") = oFlagGrupo

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------HIJO--------------------------------------")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "RAZON SOCIAL      : " & nombreCliente.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TELF REFERENCIA   : " & oDocumentoHijo.xReferenciaDeuda.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "RECIBO            : " & oDocumentoHijo.xNumeroDoc.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "FECHA EMISION     : " & oDocumentoHijo.xFechaEmision.ToShortDateString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "FECHA VENCIMIENTO : " & oDocumentoHijo.xFechaVenc.ToShortDateString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "MONEDA            : " & oDocumentoHijo.xCodConcepto1.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TIPO SERVICIO     : " & oDocumentoHijo.xCodConcepto2.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "IMPORTE           : " & oDocumentoHijo.xMontoDebe.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "FLAG DETALLE      : 1")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TIPO SERVICIO DOC : " & oDocumentoHijo.xTipoServicio)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "GRUPO         : " & oFlagGrupo.ToString())
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------------------")

                                    oTableTmp.Rows.Add(oFilaHijo)
                                End If
                            End If
                        Next

                    Next
                Next

            End If

            Select Case Trim(status)
                Case "0"
                    strDescripcionRptaAux = message
                Case Else
                    If strDescripcionRptaAux Is Nothing OrElse strDescripcionRptaAux = "" Then
                        strDescripcionRptaAux = BuscarMensaje(status)
                    End If
                    If strDescripcionRptaAux Is Nothing OrElse strDescripcionRptaAux = "" Then
                        strDescripcionRptaAux = BuscarMensaje(8)
                    End If
            End Select
         
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   OUT  CodigoRpta : " & status)

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: Get_Deuda_DNI)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   OUT  DescripcionRpta : " & message & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, "   OUT  Consulta : " & strDescripcionRptaAux)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "----------------------------------------------------------------------------")

        Catch webEx As System.Net.WebException
            If CType(webEx, System.Net.WebException).Status = Net.WebExceptionStatus.Timeout Then
                message = "Tiempo de espera excedido."
            Else
                message = webEx.Message
            End If
            strDescripcionRptaAux = BuscarMensaje(5)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   ERROR : " & webEx.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   OUT  DescripcionRpta : " & strDescripcionRptaAux)
        Catch ex As Exception
            message = ex.Message.ToString()
            strDescripcionRptaAux = BuscarMensaje(5)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "   ERROR : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, "   OUT  DescripcionRpta : " & strDescripcionRptaAux)
        Finally
            oWsOAC = Nothing
        End Try

        Return oTableTmp
    End Function


    Private Function BuscarMensaje(ByVal codErr As Integer) As String
        Dim strMensaje As String = ""
        Select Case codErr
            Case 2
                strMensaje = "No se encontraron documentos pendientes de pago"
            Case 3
                strMensaje = "Documento / Recibo a pagar no válido"
            Case 5
                strMensaje = "En este momento no podemos procesar su operación. Por favor comuníquese con Service Desk"
            Case 6
                strMensaje = "La anulación no se pudo realizar. Por favor comuníquese con Service Desk"
            Case 7
                strMensaje = "Número telefónico o factura no existe."
            Case 8
                strMensaje = "Verifique los datos ingresados. Por favor vuelva a intentar."
        End Select
        Return strMensaje
    End Function

End Class
