Public Class docRecaudacion
    Inherits System.Web.UI.Page

    Const cteRECIBOS = 0
    Const ctePAGOS = 1

    Public TramaDeudaSAP, _
            FormasPago, _
            Recibos, _
            NombreCliente, _
            CodPuntoDeVenta, _
            PuntoDeVenta, _
            SubOficina, _
            NumeroDeudaSAP, _
            FechaDeudaSAP, _
            HoraDeudaSAP, _
            CodCajero, _
            NombreCajero, _
            NumeroDocumentoDeudor, _
            TipoDocumentoDeudor, _
            strDescRedondeoSolicitado, _
            EstadoTransaccion As String
    Public MontoTotalPagado As Decimal
    Public MontoTotalRecibo As Decimal
    Public ValorRedondeoSolicitado As Decimal
    Public NomEmpresa As String
    Public dsDeuda As DataSet
    Protected WithEvents hidFlagConRedondeo As System.Web.UI.HtmlControls.HtmlInputHidden
    Public dblTipCam As Double
    Public TipoMoneda As String
    Public Importe As Decimal
    Public decTipoCambio, decImporte As Decimal
    Protected WithEvents hidTipoDocumento As System.Web.UI.HtmlControls.HtmlInputHidden


#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

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
            If Not Page.IsPostBack Then
                Dim strNroDoc As String = Request.QueryString("p_nrosap")
                hidTipoDocumento.Value = Request.QueryString("p_tipodoc")

                LeeDatos(strNroDoc)
            End If
        End If
    End Sub


    Public Sub LeeDatos(ByVal NumeroDeudaSAP As String)

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_ObtenerDocumentoDeudaSAP"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogActivacionPrepago")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = NumeroDeudaSAP
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDocumentoDeuda BD SICAR.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP NumeroDeudaSAP  " & NumeroDeudaSAP)

        dsDeuda = ObtenerDocumentoDeudaSAP(NumeroDeudaSAP)

        Dim drFila As DataRow
        For Each drFila In dsDeuda.Tables(3).Rows
            If drFila("TYPE") = "E" Then
                Throw New ApplicationException(drFila("MESSAGE"))
            End If
        Next

        NomEmpresa = ConfigurationSettings.AppSettings("gStrRazonSocial")
        ''CAMBIADO CStr y CDec por Convert.ToString inicio
        ''
        Me.NombreCliente = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NOM_DEUDOR")) 'ok
        Me.NumeroDeudaSAP = NumeroDeudaSAP 'Convert.ToString(dsDeuda.Tables(0).Rows(0)("NRO_TRANSACCION")) 'ok
        Me.MontoTotalPagado = Convert.ToDecimal(dsDeuda.Tables(0).Rows(0)("IMPORTE_PAGO")) 'ok
        Me.EstadoTransaccion = Convert.ToString(dsDeuda.Tables(0).Rows(0)("ESTADO_TRANSAC")) 'gcastillo

        Dim drFilaRecibos As DataRow
        If ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString.Equals(hidTipoDocumento.Value) Then
            Me.MontoTotalPagado = 0
        End If
        For Each drFilaRecibos In dsDeuda.Tables(1).Rows
            Me.MontoTotalRecibo += Funciones.CheckDbl(drFilaRecibos("IMPORTE_RECIBO"))
            If ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString.Equals(hidTipoDocumento.Value) Then
                Me.MontoTotalPagado += Funciones.CheckDbl(drFilaRecibos("IMPORTE_PAGADO"))
            End If
        Next

        Me.FechaDeudaSAP = Convert.ToString(dsDeuda.Tables(0).Rows(0)("FECHA_TRANSAC")) 'ok
        Me.HoraDeudaSAP = Convert.ToString(dsDeuda.Tables(0).Rows(0)("HORA_TRANSAC"))


        Me.NombreCajero = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NOM_CAJERO"))
        Me.PuntoDeVenta = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NOM_OF_VENTA"))
        Me.CodPuntoDeVenta = Convert.ToString(dsDeuda.Tables(0).Rows(0)("OFICINA_VENTA")) 'ok

        If (Convert.ToString(dsDeuda.Tables(0).Rows(0)("CASOV_SUB_OFICINA")) <> "0") Then
            Me.SubOficina = Convert.ToString(dsDeuda.Tables(0).Rows(0)("CASOV_SUB_OFICINA")) & " - " & _
            Convert.ToString(dsDeuda.Tables(0).Rows(0)("SUB_OFICINA_DESC"))    '33111
        Else
            Me.SubOficina = ""
        End If

        Me.CodCajero = Convert.ToString(dsDeuda.Tables(0).Rows(0)("COD_CAJERO")) 'ok
        Me.TipoDocumentoDeudor = Convert.ToString(dsDeuda.Tables(0).Rows(0)("TIPO_DOC_DEUDOR")) 'ok
        Me.NumeroDocumentoDeudor = Convert.ToString(dsDeuda.Tables(0).Rows(0)("NRO_DOC_DEUDOR")) 'ok
        ''
        ''cambiado fin


        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT NombreCliente  " & Me.NombreCliente)

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT NumeroDeudaSAP  " & Me.NumeroDeudaSAP)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT MontoTotalPagado  " & Me.MontoTotalPagado)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT MontoTotalRecibo  " & Me.MontoTotalRecibo)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT FechaDeudaSAP  " & Me.FechaDeudaSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT HoraDeudaSAP  " & Me.HoraDeudaSAP)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT NombreCajero  " & Me.NombreCajero)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT PuntoDeVenta  " & Me.PuntoDeVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT CodPuntoDeVenta  " & Me.CodPuntoDeVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT NombreCajero  " & Me.NombreCajero)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT TipoDocumentoDeudor  " & Me.TipoDocumentoDeudor)

        '''objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT NumeroDocumentoDeudor  " & Me.NumeroDocumentoDeudor)

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ObtenerDocumentoDeuda BD SICAR.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")


        'Me.dblTipCam = ObtenerTipoCambioSAP(Now.Date.ToString("d"))
        Me.dblTipCam = ObtenerTipoCambioSAP(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"))

        If ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString.Equals(hidTipoDocumento.Value) Then
            Me.TipoMoneda = "$."
            Me.Importe = Me.MontoTotalPagado / CDec(Me.dblTipCam)
            Me.Importe = Math.Round(Me.Importe, 2)
        Else
            Me.TipoMoneda = "S/"
            Me.Importe = Me.MontoTotalPagado
        End If

        If Me.MontoTotalPagado <> Me.MontoTotalRecibo Then
            Me.hidFlagConRedondeo.Value = "1"
            Me.ValorRedondeoSolicitado = Math.Abs(Me.MontoTotalPagado - Me.MontoTotalRecibo)
            If Me.MontoTotalPagado < Me.MontoTotalRecibo Then
                Me.strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamImportePendiente"))
                If ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString.Equals(hidTipoDocumento.Value) Then
                    Me.ValorRedondeoSolicitado = Me.ValorRedondeoSolicitado / CDec(Me.dblTipCam)
                    Me.ValorRedondeoSolicitado = Math.Round(Me.ValorRedondeoSolicitado, 2)
                    Me.strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamImportePendienteDolares"))
                End If
            Else
                Me.strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamPagoACuenta"))
                If ConfigurationSettings.AppSettings("constCodigoServFactDolares").ToString.Equals(hidTipoDocumento.Value) Then
                    Me.ValorRedondeoSolicitado = Me.ValorRedondeoSolicitado / CDec(Me.dblTipCam)
                    Me.ValorRedondeoSolicitado = Math.Round(Me.ValorRedondeoSolicitado, 2)
                    Me.strDescRedondeoSolicitado = ObtenerDescParametro(System.Configuration.ConfigurationSettings.AppSettings("constGrupoMsgTicket"), System.Configuration.ConfigurationSettings.AppSettings("constCodParamPagoACuentaDolares"))
                End If
            End If
        End If

    End Sub

    Public Function ObtenerDocumentoDeudaSAP(ByVal strNumSAP As String) As DataSet

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ''TODO CAMBIADO POR TS.JTN
        'Dim intSAP = objOffline.Get_ConsultaSAP
        ''Dim obSap As New SAP_SIC_Recaudacion.clsRecaudacion
        'Dim obSap As Object

        'Dim objPagos As Object
        'If intSAP = 1 Then
        '    obSap = New SAP_SIC_Recaudacion.clsRecaudacion
        'Else
        '    obSap = New COM_SIC_OffLine.clsOffline
        'End If

        'Return obSap.Get_RegistroDeuda(strNumSAP)
        Return objOffline.GetRegistroDeuda(strNumSAP)
        ''CAMIADO HASTA AQUI.

    End Function

    Public Function ObtenerTipoCambioSAP(ByVal strFecha As String) As String

        Dim objOffline As New COM_SIC_OffLine.clsOffline
        ''CAMBIADO POR TS.JTN INICIO
        'Dim intSAP = objOffline.Get_ConsultaSAP

        'Dim obSap As New SAP_SIC_Pagos.clsPagos
        'If intSAP = 1 Then
        '    'Dim obSap As New SAP_SIC_Pagos.clsPagos
        '    Return obSap.Get_TipoCambio(strFecha).ToString("N2")
        'Else
        '    Return 0
        'End If
        Return Format(objOffline.Obtener_TipoCambio(strFecha), "#######0.000")
        ''CAMBIADO POR TS.JTN FIN
        'Return obSap.Get_TipoCambio(strFecha).ToString("N2")
    End Function

    Public Function GetHour(ByVal strHoraDeuda As String)
        'Dim intBlank
        'Dim strHora As String
        'If strHoraDeuda = "" Then Return ""
        'intBlank = InStr(1, strHoraDeuda, " ")
        'If intBlank > 0 Then
        '    strHora = Mid(strHoraDeuda, intBlank)
        'Else
        '    strHora = strHoraDeuda
        'End If
        'strHora = strHora.Substring(0, 2) + ":" + strHora.Substring(2, 2) + ":" + strHora.Substring(4, 2)
        'GetHour = strHora
        Return strHoraDeuda
    End Function

    Public Function ObtenerDescParametro(ByVal strGrupo As String, ByVal strCodigo As String) As String

        Dim strDescParametro As String
        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_Parametros"
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecFijoPaginas")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = strGrupo & "|" & strCodigo
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDescParametro.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strGrupo  " & strGrupo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     INP strCodigo:  " & strCodigo)


        Try
            Dim dsParam As DataSet

            Dim objSicarDB As New COM_SIC_Cajas.clsCajas
            dsParam = objSicarDB.FP_ConsultaParametros(strGrupo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT dsParam.Tables(0).Rows.Count:  " & dsParam.Tables(0).Rows.Count)
            If Not IsNothing(dsParam) AndAlso dsParam.Tables(0).Rows.Count > 0 Then
                For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT SPARN_CODIGO:  " & Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO")))
                    If Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO")).Equals(strCodigo) Then
                        strDescParametro = Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_DESCRIPCION"))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     OUT SPARV_DESCRIPCION:  " & Funciones.CheckStr(dsParam.Tables(0).Rows(idx).Item("SPARV_DESCRIPCION")))
                        Exit For
                    End If
                Next
            End If
            objSicarDB = Nothing
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     ERROR:" & ex.Message.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ObtenerDescParametro.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")

        End Try
        Return strDescParametro
    End Function

End Class
