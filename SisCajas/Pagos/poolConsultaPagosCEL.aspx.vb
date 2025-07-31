Imports COM_SIC_INActChip
Public Class poolConsultaPagosCEL
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cmdAnularPago As System.Web.UI.WebControls.Button
    Protected WithEvents cmdAnular As System.Web.UI.WebControls.Button
    Protected WithEvents txtRbPagos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnImprimirBackGround As System.Web.UI.WebControls.Button
    Protected WithEvents IfrmImpresion As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnImprimir2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button
    Protected WithEvents CheckTodos As System.Web.UI.WebControls.CheckBox

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Dim ds As DataSet
    Dim dsVentasCEL As DataSet
    Dim drFila As DataRow
    Dim strOficinaVta As String
    Dim strFechaVenta As String
    Dim strTpoPool As String
    Dim strFechaHasta As String
    Dim strNroDocCliente As String = ""
    Dim strTipoDocCliente As String = ""
    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim datPedidosCEL As New DataTable '//tabla de pedidos de CEL para cargar en grilla

    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPasarelaDePago")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPasarelaDePago")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else


            Dim strFecha As String
            If Not Page.IsPostBack Then
                If Session("FechaPago") = "" Then
                    txtFecha.Text = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000")
                Else
                    txtFecha.Text = Session("FechaPago")
                    Session("FechaPago") = ""
                End If
            End If

            strOficinaVta = Session("ALMACEN")
            strFechaVenta = txtFecha.Text
            strTpoPool = ConfigurationSettings.AppSettings("strTipoPoolPagado")
            strFechaHasta = txtFecha.Text
            CargarGrilla(strOficinaVta, strFechaVenta, strTpoPool, strFechaHasta, strNroDocCliente, strTipoDocCliente)

            btnImprimir.Attributes.Add("onClick", "f_imprimirTodo()")

        End If

    End Sub

    Private Sub CargarGrilla(ByVal strOficinaVta As String, _
                             ByVal strFechaVenta As String, _
                             ByVal strTpoPool As String, _
                             ByVal strFechaHasta As String, _
                             ByVal strNroDocCliente As String, _
                             ByVal strTipoDocCliente As String)

        Dim objVentasCEL As New COM_SIC_Cajas.clsConsultaGeneral

        Dim strIdentifyLog As String = strOficinaVta
        Dim strDocNoImpreso As String = ConfigurationSettings.AppSettings("strDocNoImpreso")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de ConsultaMiClaro.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Oficina Venta : " & strOficinaVta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strFechaVenta : " & strFechaVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strTpoPool : " & strTpoPool)

        Try
            dsVentasCEL = objVentasCEL.FP_Get_ListaVentasCEL(strOficinaVta, strFechaVenta, strTpoPool, "", "", "") 'Ademas que esten pagados

            If datPedidosCEL.Columns.Count = 0 Then
                datPedidosCEL.Columns.Add(New DataColumn("NAME1", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("TOTAL", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("DCORTA", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("XBLNR", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("FKDAT", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("VBELN", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("WAERK", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("NETWR", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("MWSBK", GetType(String)))
                datPedidosCEL.Columns.Add(New DataColumn("FKART", GetType(String)))
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Filas Devueltas MiClaro: " & dsVentasCEL.Tables(0).Rows.Count.ToString())

            Dim drwFila As DataRow
            datPedidosCEL.Rows.Clear()

            If dsVentasCEL.Tables(0).Rows.Count > 0 Then
                'Dim drwFila As New DataRow
                Dim strNroDocSap As String
                Dim dsResult As DataSet
                Dim strFecha As String
                Dim cont As Integer = 0
                For iContVentas As Integer = 0 To dsVentasCEL.Tables(0).Rows.Count - 1
                    If dsVentasCEL.Tables(0).Rows(iContVentas).Item("CONSV_EST_EMISION") = ConfigurationSettings.AppSettings("strDocNoImpreso") Then
                        strNroDocSap = Funciones.CheckStr(dsVentasCEL.Tables(0).Rows(iContVentas).Item("OPERV_NRO_FACTURA"))
                        dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), strNroDocSap, "")

                        If Not IsNothing(dsResult) Then
                            If dsResult.Tables(0).Rows.Count > 0 Then

                                drwFila = datPedidosCEL.NewRow
                                strFecha = dsResult.Tables(0).Rows(0).Item("FECHA_DOCUMENTO")

                                drwFila("NAME1") = Funciones.CheckStr(ConfigurationSettings.AppSettings("consNombreClienteMiClaro"))
                                drwFila("TOTAL") = dsResult.Tables(0).Rows(0).Item("TOTAL_DOCUMENTO")
                                drwFila("DCORTA") = Funciones.CheckStr(ConfigurationSettings.AppSettings("consDesTipoDocSAP_MiClaro"))
                                drwFila("XBLNR") = dsResult.Tables(0).Rows(0).Item("NRO_REFERENCIA")
                                drwFila("FKDAT") = strFecha.Substring(6, 2) & "/" & strFecha.Substring(4, 2) & "/" & strFecha.Substring(0, 4)
                                drwFila("VBELN") = dsResult.Tables(0).Rows(0).Item("NRO_FACTURA")
                                drwFila("WAERK") = dsResult.Tables(0).Rows(0).Item("MONEDA")
                                drwFila("NETWR") = dsResult.Tables(0).Rows(0).Item("TOTAL_MERCADERIA")
                                drwFila("MWSBK") = dsResult.Tables(0).Rows(0).Item("TOTAL_IMPUESTO")
                                drwFila("FKART") = dsResult.Tables(0).Rows(0).Item("TIPO_DOCUMENTO")

                                datPedidosCEL.Rows.Add(drwFila)
                                cont = cont + 1
                            End If
                        End If
                    End If
                Next
                If cont = 0 Then
                    Response.Write("<script> alert('No existen documentos para la fecha indicada.'); </script>")
                End If
            Else
                Response.Write("<script> alert('No existen documentos para la fecha indicada.'); </script>")
                'hidMsg.Value = "No existen documentos para la fecha indicada."
            End If

            dgPool.DataSource = datPedidosCEL
            dgPool.DataBind()

            RegistrarAuditoria("", "consulta")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ERROR : " & ex.Message.ToString())
            Response.Write("<script> alert('Ocurrió un error al consultar a MiClaro.'); </script>")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de ConsultaMiClaro.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        End Try

    End Sub

    Private Function ActivaChipRepuesto(ByVal strNroPedidoSAP As String, ByRef strMsgResultado As String) As Integer

        Dim intCodResultado As Integer

        Dim dsResult As DataSet

        Dim i, j As Integer
        Dim strSolicitud As String
        Dim arrSolicitud() As String

        Dim strResultActChip As String

        Dim strIMEI_SANS As String
        Dim strTelefono As String
        Dim strOficVenta As String

        Dim strConsulta As String

        Dim strMSISDN As String
        Dim strCODCAUSA As String
        Dim strICCIDNEW As String
        Dim strICCIDOLD As String
        Dim strNROTXSW As String
        Dim strNROPEDIDO As String
        Dim strNROOFIVENTA As String
        Dim strESTADOINI As String

        Dim strIMSI_NUEVO As String

        Dim arrActualizaCHIP() As String
        Dim strActualizaCHIP As String


        Dim arrFilCHIP
        Dim arrResulFilCHIPError
        Dim arrResulFilCHIPValor
        Dim arrResulFilCHIPNomb
        Dim strConsultaCHIP

        Dim strTipoVenta As String
        Dim strOperacion As String
        Dim strMotivo As String
        Dim strCaso As String
        Dim vendedor As String

        Dim strResLog As String
        Dim strCodError As String
        Dim strMenError As String

        Dim objINActCHIP As New COM_SIC_INActChip.clsActivacion
        Dim objComponente As New COM_SIC_ActCHIP.clsActivacion
        Dim strIdentifyLog As String = strNroPedidoSAP
        Dim strParametros As String = ""

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de Transaccion.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Oficina Venta : " & Session("ALMACEN") & " - " & Session("OFICINA"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Usuario : " & Session("USUARIO"))

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo SetGet_LogActivacionCHIP()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strNroPedidoSAP & "|" & Session("ALMACEN") & "|" & "" & "|" & "")

        Session("FLAGCHIPREP") = ""
        dsResult = objPagos.SetGet_LogActivacionCHIP(strNroPedidoSAP, Session("ALMACEN"), "", "")

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo SetGet_LogActivacionCHIP()")

        strSolicitud = ""

        If Not IsNothing(dsResult) Then
            For i = 0 To dsResult.Tables(1).Rows.Count - 1
                If dsResult.Tables(1).Rows(i).Item("TYPE") = "E" Then
                    Session("FLAGCHIPREP") = "N"
                End If
            Next

            For i = 0 To dsResult.Tables(0).Rows.Count - 1
                For j = 0 To dsResult.Tables(0).Columns.Count - 1
                    strSolicitud = strSolicitud & dsResult.Tables(0).Rows(i).Item(j)
                    If j <> dsResult.Tables(0).Columns.Count - 1 Then
                        strSolicitud = strSolicitud & ";"
                    End If
                Next
                If i <> dsResult.Tables(0).Rows.Count - 1 Then
                    strSolicitud = strSolicitud & "¿"
                End If
            Next
        Else
            Session("FLAGCHIPREP") = "N"
        End If


        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Cadena Solicitud : " & strSolicitud)

        If Trim(strSolicitud) <> "" Then

            arrSolicitud = Split(strSolicitud, ";")

            strMSISDN = arrSolicitud(1)
            strNROPEDIDO = arrSolicitud(5)
            strCODCAUSA = arrSolicitud(6)
            strNROOFIVENTA = arrSolicitud(4)
            strNROTXSW = arrSolicitud(0)
            strICCIDOLD = arrSolicitud(2)
            strICCIDNEW = arrSolicitud(7)
            strESTADOINI = arrSolicitud(10)

            arrSolicitud(16) = Session("FechaAct")
            arrSolicitud(17) = Format(Now, "H:mm:ss")
            arrSolicitud(14) = "X"
            'Inicio Amejia
            strMSISDN = FormatoTelefono(strMSISDN)
            'strMSISDN = Right("0000000000" & strMSISDN, 10)
            'fin Amejia

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo SetGet_LogActivacionCHIP()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & "" & "|" & "" & "|" & Join(arrSolicitud, ";") & "|" & "")

            dsResult = objPagos.SetGet_LogActivacionCHIP("", "", Join(arrSolicitud, ";"), "")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo SetGet_LogActivacionCHIP()")

            'Consultar tipo de venta
            dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), strNROPEDIDO, "")

            If Not IsNothing(dsResult) Then
                If dsResult.Tables(0).Rows.Count > 0 Then
                    strTipoVenta = dsResult.Tables(0).Rows(0).Item("TIPO_VENTA")
                    strOperacion = dsResult.Tables(0).Rows(0).Item("CLASE_VENTA")
                    strMotivo = dsResult.Tables(0).Rows(0).Item("AUGRU")
                    strCaso = dsResult.Tables(0).Rows(0).Item("NRO_CLARIFY")
                    vendedor = dsResult.Tables(0).Rows(0).Item("VENDEDOR")
                End If
            End If
            ' Fin Consultar tipo de venta

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo Venta : " & strTipoVenta)

            If strTipoVenta = ConfigurationSettings.AppSettings("strTVPrepago") Then    'or strTipoVenta = strTVControl then  'CARIAS

                'Activacion de CHIP Repuesto utilizando componente PVUSIX
                strConsulta = strMSISDN & ";" & strNROPEDIDO & ";" & strCODCAUSA & ";" & strNROOFIVENTA & ";" & strNROTXSW & ";" & strICCIDOLD & ";" & Left(strICCIDNEW, 18) & ";" & strCaso & ";" & ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE") & ";" & ConfigurationSettings.AppSettings("cteCODIGO_CANAL") & ";PVU"

                ' E75893 - INVOCACION WS CAMBIO DE SIM
                If ConfigurationSettings.AppSettings("constFlagWSCambioSIM") = "S" Then

                    Session("FLAGCHIPREP") = ""

                    Dim strIP As String
                    Dim strID As String
                    Dim ICCID As String
                    Dim OLD_ICCID As String
                    Dim MSISDN As String
                    Dim strAccion As String
                    Dim strBloqueo As String
                    Dim strEmpleado As String

                    Dim strTipoPlan As String
                    Dim flagBloqueo As String

                    Dim mensajePrepago As String
                    Dim retornoCambioSIM As String
                    Dim mensajeCambioSIM As String
                    Dim retornoDesbloqueo As String
                    Dim mensajeDesbloqueo As String
                    Dim errorCambioSIM As String

                    Dim flagTFI As String

                    Dim oCambioSIM As New COM_SIC_INActChip.clsCambioSIM
                    Dim oBloqueoDesbloqueo As New COM_SIC_INActChip.clsBloqueoDesbloqueo

                    Dim idTransaccion As String     ' Formato PVU: "003xxxxxxx" - SICAR: "004xxxxxxx"
                    idTransaccion = Int((9999999 - 1000000 + 1) * Rnd() + 1000000)
                    idTransaccion = "004" & Right("0000000" & idTransaccion, 7)

                    strIP = ConfigurationSettings.AppSettings("constIPServidorSICAR")
                    strAccion = ConfigurationSettings.AppSettings("constDesbloqueoLinea")
                    strBloqueo = ConfigurationSettings.AppSettings("constBloqueoLinea")
                    strID = idTransaccion
                    ICCID = Left(strICCIDNEW, 18)
                    OLD_ICCID = Left(strICCIDOLD, 18)
                    MSISDN = FormatoTelefonoPrepago(strMSISDN)

                    ' Consultar el Usuario del Vendedor
                    Dim oConsulta As New COM_SIC_Activaciones.clsBDSiscajas
                    Dim dsUsuario As DataSet
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio FP_ConsultaUsuarioRed")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Cod Vendedor:" + vendedor)
                    Dim strRespuesta As String
                    Do While InStr(1, vendedor, "0") = 1
                        vendedor = Mid(vendedor, 2, Len(vendedor))
                    Loop
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Cod Vendedor:" + vendedor)
                    dsUsuario = oConsulta.FP_ConsultaUsuarioRed(vendedor, strRespuesta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "strRespuesta:" + strRespuesta)

                    If Not IsNothing(dsUsuario) Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "nro de registros :" + dsUsuario.Tables(0).Rows.Count.ToString())
                        If dsUsuario.Tables(0).Rows.Count > 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Codigo de red:" + dsUsuario.Tables(0).Rows(0).Item(0))
                            strEmpleado = dsUsuario.Tables(0).Rows(0).Item(0)
                        Else
                            strEmpleado = Session("strUsuario")
                        End If
                    Else
                        strEmpleado = Session("strUsuario")
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo SAP: " & vendedor)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Codigo Red : " & strEmpleado)

                    dsUsuario = Nothing
                    oConsulta = Nothing
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin FP_ConsultaUsuarioRed")
                    ' Cadena de Parametros
                    strParametros = ""
                    strParametros = strParametros & MSISDN & "|"
                    strParametros = strParametros & "1" & "|"
                    strParametros = strParametros & ConfigurationSettings.AppSettings("constServicioSMS") & "|"
                    strParametros = strParametros & ConfigurationSettings.AppSettings("constServicioSMS") & "|"
                    strParametros = strParametros & ConfigurationSettings.AppSettings("constServicioSMS") & "|"
                    strParametros = strParametros & Now() & "|"
                    strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSDatosLinea") & "|"
                    strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSDatosLinea")

                    ' Verificar si la Linea se encuentra Bloqueada
                    Dim oDatosLineaPrepago As New COM_SIC_INActChip.clsDatosLinea

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo DatosLineaPrepago()")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                    strTipoPlan = oDatosLineaPrepago.DatosLineaPrepago(MSISDN, _
                                                                        ConfigurationSettings.AppSettings("providerIdPrepago"), _
                                                                        ConfigurationSettings.AppSettings("providerIdControl"), _
                                                                        ConfigurationSettings.AppSettings("constTimeOutWSDatosLinea"), _
                                                                        ConfigurationSettings.AppSettings("constRutaWSDatosLinea"), _
                                                                        flagBloqueo, _
                                                                        mensajePrepago)

                    oDatosLineaPrepago = Nothing

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo de Plan : " & strTipoPlan)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Bloqueo : " & flagBloqueo)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajePrepago)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo DatosLineaPrepago()")

                    ' Verificar si la Linea se encuentra Bloqueada
                    If UCase(Trim(strTipoPlan)) = "P" Then
                        If UCase(Trim(flagBloqueo)) = "TRUE" Then

                            ' Cadena de Parametros
                            strParametros = ""
                            strParametros = strParametros & MSISDN & "|"
                            strParametros = strParametros & strAccion & "|"
                            strParametros = strParametros & strID & "|"
                            strParametros = strParametros & strIP & "|"
                            strParametros = strParametros & strEmpleado & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                            ' Invocar WS para Desbloquear Linea
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo BloqueoDesbloqueo()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                            retornoDesbloqueo = oBloqueoDesbloqueo.BloqueoDesbloqueo(MSISDN, _
                                                                                        strAccion, _
                                                                                        strID, _
                                                                                        strIP, _
                                                                                        strEmpleado, _
                                                                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                                        ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                                        ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                                        mensajeDesbloqueo)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoDesbloqueo)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeDesbloqueo)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo BloqueoDesbloqueo()")

                            ' Si el DesBloqueo de Linea fue exitoso realizar el Cambio de SIM
                            If Trim(retornoDesbloqueo) = "0" Then

                                ' Cadena de Parametros
                                strParametros = ""
                                strParametros = strParametros & MSISDN & "|"
                                strParametros = strParametros & ICCID & "|"
                                strParametros = strParametros & strID & "|"
                                strParametros = strParametros & strIP & "|"
                                strParametros = strParametros & strEmpleado & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                                ' Invocar WS para Cambio de SIM
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo CambioSIM()")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                                retornoCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                        ICCID, _
                                                                        strID, _
                                                                        strIP, _
                                                                        strEmpleado, _
                                                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                        ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                        ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                        mensajeCambioSIM)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoCambioSIM)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeCambioSIM)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CambioSIM()")

                                If Trim(retornoCambioSIM) = "0" Then
                                    Session("mensajeCHIPRepuesto") = ""
                                Else
                                    Session("mensajeCHIPRepuesto") = " No se pudo realizar el Cambio de SIM en Linea. Error Metodo Cambio de SIM."

                                    ' Cadena de Parametros
                                    strParametros = ""
                                    strParametros = strParametros & MSISDN & "|"
                                    strParametros = strParametros & strBloqueo & "|"
                                    strParametros = strParametros & strID & "|"
                                    strParametros = strParametros & strIP & "|"
                                    strParametros = strParametros & strEmpleado & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                                    ' Invocar WS para Bloquear Linea
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Rollback BloqueoDesbloqueo()")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                                    retornoDesbloqueo = oBloqueoDesbloqueo.BloqueoDesbloqueo(MSISDN, _
                                                                                                strBloqueo, _
                                                                                                strID, _
                                                                                                strIP, _
                                                                                                strEmpleado, _
                                                                                                ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                                                ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                                                ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                                                mensajeDesbloqueo)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoDesbloqueo)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeDesbloqueo)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Rollback BloqueoDesbloqueo()")

                                    ' Cadena de Parametros
                                    strParametros = ""
                                    strParametros = strParametros & MSISDN & "|"
                                    strParametros = strParametros & OLD_ICCID & "|"
                                    strParametros = strParametros & strID & "|"
                                    strParametros = strParametros & strIP & "|"
                                    strParametros = strParametros & strEmpleado & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                                    strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                                    ' Invocar WS para Cambio de SIM
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Rollback CambioSIM()")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                                    errorCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                            OLD_ICCID, _
                                                                            strID, _
                                                                            strIP, _
                                                                            strEmpleado, _
                                                                            ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                            ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                            ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                            mensajeCambioSIM)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & errorCambioSIM)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeCambioSIM)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Rollback CambioSIM()")

                                End If
                            Else
                                Session("mensajeCHIPRepuesto") = " No se pudo realizar el Cambio de SIM en Linea. Error Metodo Desbloqueo de Linea."

                                ' Cadena de Parametros
                                strParametros = ""
                                strParametros = strParametros & MSISDN & "|"
                                strParametros = strParametros & strBloqueo & "|"
                                strParametros = strParametros & strID & "|"
                                strParametros = strParametros & strIP & "|"
                                strParametros = strParametros & strEmpleado & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                                ' Invocar WS para Bloquear Linea
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Rollback BloqueoDesbloqueo()")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                                retornoDesbloqueo = oBloqueoDesbloqueo.BloqueoDesbloqueo(MSISDN, _
                                        strBloqueo, _
                                        strID, _
                                        strIP, _
                                        strEmpleado, _
                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                        ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                        ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                        mensajeDesbloqueo)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoDesbloqueo)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeDesbloqueo)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Rollback BloqueoDesbloqueo()")

                            End If
                        Else
                            ' Cadena de Parametros
                            strParametros = ""
                            strParametros = strParametros & MSISDN & "|"
                            strParametros = strParametros & ICCID & "|"
                            strParametros = strParametros & strID & "|"
                            strParametros = strParametros & strIP & "|"
                            strParametros = strParametros & strEmpleado & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                            strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                            ' Invocar WS para Cambio de SIM
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo CambioSIM()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                            retornoCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                    ICCID, _
                                                                    strID, _
                                                                    strIP, _
                                                                    strEmpleado, _
                                                                    ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                    ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                    ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                    mensajeCambioSIM)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoCambioSIM)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeCambioSIM)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CambioSIM()")

                            If Trim(retornoCambioSIM) = "0" Then
                                Session("mensajeCHIPRepuesto") = ""
                            Else
                                Session("mensajeCHIPRepuesto") = " No se pudo realizar el Cambio de SIM en Linea. Error Metodo Cambio de SIM."

                                ' Cadena de Parametros
                                strParametros = ""
                                strParametros = strParametros & MSISDN & "|"
                                strParametros = strParametros & OLD_ICCID & "|"
                                strParametros = strParametros & strID & "|"
                                strParametros = strParametros & strIP & "|"
                                strParametros = strParametros & strEmpleado & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constAplicacion") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM") & "|"
                                strParametros = strParametros & ConfigurationSettings.AppSettings("constRutaWSCambioSIM")

                                ' Invocar WS para Cambio de SIM
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Rollback CambioSIM()")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                                errorCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                        OLD_ICCID, _
                                                                        strID, _
                                                                        strIP, _
                                                                        strEmpleado, _
                                                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                        ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                        ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                        mensajeCambioSIM)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & errorCambioSIM)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeCambioSIM)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Rollback CambioSIM()")

                            End If
                        End If
                    Else
                        Session("mensajeCHIPRepuesto") = " No se pudo realizar el Cambio de SIM en Linea. Error Metodo Consulta Datos Prepago."
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje Error : " & Session("mensajeCHIPRepuesto"))
                    End If

                    dsResult = objPagos.Set_LogVariacion(strConsulta, "RETCODE;ERRORMSG", retornoCambioSIM & ";" & mensajeCambioSIM)

                    If Trim(retornoCambioSIM) = "0" Then

                        Session("FLAGCHIPREP") = "S"

                        arrSolicitud(8) = ""
                        arrSolicitud(13) = ConfigurationSettings.AppSettings("gstrEstSolicVariado")
                        arrSolicitud(14) = ""

                        ' Cadena de Parametros
                        strParametros = ""
                        strParametros = strParametros & "" & "|"
                        strParametros = strParametros & "" & "|"
                        strParametros = strParametros & Join(arrSolicitud, ";") & "|"
                        strParametros = strParametros & ""

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo SetGet_LogActivacionCHIP()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                        dsResult = objPagos.SetGet_LogActivacionCHIP("", "", Join(arrSolicitud, ";"), "")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo SetGet_LogActivacionCHIP()")

                        ' Creacion TIPIFICACION - INTERACCION
                        Dim dsCliente As DataSet

                        Dim objIdContacto As String
                        Dim contactoId As Int64
                        Dim nroDocCliente As String
                        Dim nombreCliente As String
                        Dim apellidoCliente As String
                        Dim clarifyTelef As String

                        Dim oInteraccion As New COM_SIC_INActChip.Interaccion
                        Dim oPlantilla As New COM_SIC_INActChip.PlantillaInteraccion

                        Dim flagInter As String
                        Dim mensajeInter As String
                        Dim idInteraccion As String

                        ' Validacion Telefono TFI
                        If Len(Trim(MSISDN)) = 8 Then
                            flagTFI = "X"
                            clarifyTelef = "000" & Trim(MSISDN)
                        Else
                            clarifyTelef = MSISDN
                        End If

                        ' Cadena de Parametros
                        strParametros = ""
                        strParametros = strParametros & clarifyTelef & "|"
                        strParametros = strParametros & "" & "|"
                        strParametros = strParametros & "" & "|"
                        strParametros = strParametros & 1

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaCliente()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Parametros : " & strParametros)

                        ' Consulta de Id Cliente Clarify
                        Dim oTipificacion As New COM_SIC_INActChip.clsTipificacion
                        dsCliente = oTipificacion.ConsultaCliente(clarifyTelef, "", contactoId, CInt("1"), "", "")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaCliente()")

                        If Not IsNothing(dsCliente) Then
                            If dsCliente.Tables(0).Rows.Count > 0 Then
                                objIdContacto = dsCliente.Tables(0).Rows(0).Item("OBJID_CONTACTO")
                                nroDocCliente = dsCliente.Tables(0).Rows(0).Item("NRO_DOC")
                                nombreCliente = dsCliente.Tables(0).Rows(0).Item("NOMBRES")
                                apellidoCliente = dsCliente.Tables(0).Rows(0).Item("APELLIDOS")

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Contacto: " & objIdContacto)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Doc. Cliente: " & nroDocCliente)

                                ' Verificar si se Realizo Desbloqueo de Linea
                                If Trim(retornoDesbloqueo) = "0" Then

                                    ' Creacion Interaccion de Desbloqueo de Linea
                                    oInteraccion = DatosInteraccion()
                                    oInteraccion.AGENTE = strEmpleado
                                    oInteraccion.OBJID_CONTACTO = objIdContacto
                                    oInteraccion.TELEFONO = MSISDN

                                    ' Validacion Parametros Tipificacion
                                    If flagTFI = "X" Then
                                        oInteraccion.TIPO = ConfigurationSettings.AppSettings("TIPO_DEFAULT_TFI")
                                        oInteraccion.CLASE = ConfigurationSettings.AppSettings("CLASE_DESBLOQUEO_CHIP_TFI")
                                        oInteraccion.SUBCLASE = ConfigurationSettings.AppSettings("SUBCLASE_DESBLOQUEO_TFI")
                                    Else
                                        oInteraccion.TIPO = ConfigurationSettings.AppSettings("TIPO_DEFAULT")
                                        oInteraccion.CLASE = ConfigurationSettings.AppSettings("CLASE_DESBLOQUEO_CHIP")
                                        oInteraccion.SUBCLASE = ConfigurationSettings.AppSettings("SUBCLASE_CHIP")
                                    End If

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Desbloqueo Linea ---")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo CrearInteraccion()")

                                    oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CrearInteraccion()")

                                    If Trim(flagInter) = "OK" Then

                                        ' Creacion Plantilla Tipificacion
                                        oPlantilla.X_FIRST_NAME = nombreCliente
                                        oPlantilla.X_LAST_NAME = apellidoCliente
                                        oPlantilla.X_DOCUMENT_NUMBER = nroDocCliente

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo InsertarPlantilla()")

                                        oTipificacion.InsertarPlantillaInteraccion(oPlantilla, idInteraccion, flagInter, mensajeInter)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo InsertarPlantilla()")
                                    End If
                                End If

                                ' Creacion Interaccion de Cambio de SIM
                                oInteraccion = DatosInteraccion()
                                oInteraccion.AGENTE = strEmpleado
                                oInteraccion.OBJID_CONTACTO = objIdContacto
                                oInteraccion.TELEFONO = MSISDN

                                ' Validacion Parametros Tipificacion
                                If flagTFI = "X" Then
                                    oInteraccion.TIPO = ConfigurationSettings.AppSettings("TIPO_DEFAULT_TFI")
                                    oInteraccion.CLASE = ConfigurationSettings.AppSettings("CLASE_CAMBIO_CHIP_TFI")
                                    oInteraccion.SUBCLASE = ConfigurationSettings.AppSettings("SUBCLASE_CAMBIO_TFI")
                                Else
                                    oInteraccion.TIPO = ConfigurationSettings.AppSettings("TIPO_DEFAULT")
                                    oInteraccion.CLASE = ConfigurationSettings.AppSettings("CLASE_CAMBIO_CHIP")
                                    oInteraccion.SUBCLASE = ConfigurationSettings.AppSettings("SUBCLASE_CHIP")
                                End If

                                ' Creacion Interaccion
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio Region Cambio SIM ---")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CrearInteraccion()")

                                oTipificacion.CrearInteraccion(oInteraccion, idInteraccion, flagInter, mensajeInter)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo CrearInteraccion()")

                                If Trim(flagInter) = "OK" Then

                                    ' Creacion Plantilla Tipificacion
                                    Dim oPlantilla1 As New COM_SIC_INActChip.PlantillaInteraccion
                                    oPlantilla1.X_FIRST_NAME = nombreCliente
                                    oPlantilla1.X_LAST_NAME = apellidoCliente
                                    oPlantilla1.X_DOCUMENT_NUMBER = nroDocCliente
                                    oPlantilla1.X_REASON = strCODCAUSA
                                    oPlantilla1.X_ICCID = ICCID

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo InsertarPlantilla()")

                                    oTipificacion.InsertarPlantillaInteraccion(oPlantilla1, idInteraccion, flagInter, mensajeInter)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Id Interaccion : " & idInteraccion)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Flag Metodo : " & flagInter)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Mensaje : " & mensajeInter)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo InsertarPlantilla()")
                                End If

                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error. Cliente no existe en Clarify. No pudo realizar la creacion de la Interaccion.")
                            End If
                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Error. Cliente no existe en Clarify. No pudo realizar la creacion de la Interaccion.")
                        End If

                    Else
                        Session("FLAGCHIPREP") = "N"
                    End If

                    ' E75893 - INVOCACION WS CAMBIO DE SIM

                Else

                    If (strTipoVenta = ConfigurationSettings.AppSettings("strTVPrepago")) And (strESTADOINI = "3") Then       ' (strMotivo = "R01" or strMotivo = "R02") then
                        strConsultaCHIP = objINActCHIP.Set_CHIPPrepagoPorRobo(strConsulta)
                    Else
                        'if strTipoVenta = strTVPrepago then
                        strConsultaCHIP = objINActCHIP.Set_CHIPPrepago(strConsulta)
                        'else
                        '  strConsultaCHIP = objComponente.Set_CHIPControl(strConsulta & ";F")
                        'end if  
                    End If

                    arrFilCHIP = Split(strConsultaCHIP, "|")
                    arrResulFilCHIPError = Split(arrFilCHIP(0), ";")
                    Session("FLAGCHIPREP") = ""

                    strCodError = Split(arrFilCHIP(2), ";")(1)       'Codigo de mensaje
                    strMenError = Split(arrFilCHIP(2), ";")(2)       'Descripcion de mensaje
                    'Response.Write(strCodError):Response.End 

                    dsResult = objPagos.Set_LogVariacion(strConsulta, "RETCODE;ERRORMSG", strCodError & ";" & strMenError)

                    'Response.Write(strResLog):Response.end

                    'if arrResulFilCHIPError(0)<>"XX00XX" then
                    If IsNumeric(strCodError) Then
                        If CInt(strCodError) = 0 Then
                            Session("FLAGCHIPREP") = "S"

                            arrSolicitud(13) = ConfigurationSettings.AppSettings("gstrEstSolicVariado")
                            arrSolicitud(8) = ""       'strIMSI_NUEVO
                            arrSolicitud(14) = ""

                            'Response.Write join(arrSolicitud,";")
                            'Response.End 

                            dsResult = objPagos.SetGet_LogActivacionCHIP("", "", Join(arrSolicitud, ";"), "")

                        Else
                            Session("FLAGCHIPREP") = "N"
                        End If
                    Else
                        Session("FLAGCHIPREP") = "N"
                    End If
                    'end if    

                    If IsNumeric(strCodError) Then
                        If CInt(strCodError) <> 0 Then       'error en el cambio de chip 
                            Session("FLAGCHIPREP") = "N"
                        End If
                    Else
                        Session("FLAGCHIPREP") = "N"
                    End If

                End If

            Else

                'Activacion de CHIP Repuesto utilizando Web Service
                strConsulta = strMSISDN & ";" & strNROPEDIDO & ";" & strCODCAUSA & ";" & strNROOFIVENTA & ";" & strNROTXSW & ";" & strICCIDOLD & ";" & strICCIDNEW

                'Response.Write strConsulta & "<br><br>"
                'Response.End 

                strConsultaCHIP = objComponente.FK_ActualizaEstadoChip(strConsulta, ConfigurationSettings.AppSettings("gStrUrlSvrPCR"), ConfigurationSettings.AppSettings("gStrServicioPCR"), ConfigurationSettings.AppSettings("gStrUsuarioPCR"))

                'Response.Write "Resul Int." & strConsultaCHIP
                'Response.End 

                arrFilCHIP = Split(strConsultaCHIP, "|")
                arrResulFilCHIPError = Split(arrFilCHIP(0), ";")
                Session("FLAGCHIPREP") = ""

                If arrResulFilCHIPError(0) <> "XX00XX" Then
                    If arrResulFilCHIPError(0) <> "0" Then
                        Session("FLAGCHIPREP") = "N"
                    Else
                        arrResulFilCHIPNomb = Split(arrFilCHIP(1), ";")
                        arrResulFilCHIPValor = Split(arrFilCHIP(2), ";")
                        strIMSI_NUEVO = ValorResultCHIP("IMSI", arrResulFilCHIPNomb, arrResulFilCHIPValor)
                        Session("FLAGCHIPREP") = "S"

                        arrSolicitud(13) = ConfigurationSettings.AppSettings("gstrEstSolicVariado")
                        arrSolicitud(8) = strIMSI_NUEVO
                        arrSolicitud(14) = ""

                        'Response.Write join(arrSolicitud,";")
                        'Response.End 

                        dsResult = objPagos.SetGet_LogActivacionCHIP("", "", (Join(arrSolicitud, ";")), "")

                    End If
                Else
                    Session("FLAGCHIPREP") = "N"
                End If
            End If     'CARIAS 	
        Else
            Session("FLAGCHIPREP") = ""
        End If
        '******************************Fin Consulta**************************************


        If Len(Funciones.CheckStr(Session("mensajeCHIPRepuesto"))) > 0 Then
            intCodResultado = 1
            strMsgResultado = Funciones.CheckStr(Session("mensajeCHIPRepuesto"))
            Session("mensajeCHIPRepuesto") = ""
        Else
            intCodResultado = 0
        End If

        Return intCodResultado

    End Function

    Function FormatoTelefono(ByVal telefono)

        Try
            Dim aux
            aux = telefono
            If aux <> "" Then
                Dim longitud
                Dim posicion
                longitud = Len(telefono)
                If longitud > 0 Then
                    'posicion = 1
                    Do While InStr(1, aux, "0") = 1
                        aux = Mid(aux, 2, Len(aux))
                    Loop
                End If
                If InStr(1, aux, "1") = 1 Then    'Si es lima adicionar 0 adelante
                    aux = "0" & aux
                End If
            End If
            If aux = "" Then
                FormatoTelefono = telefono
            Else
                FormatoTelefono = aux
            End If

        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "'); </script>")
        End Try

    End Function

    Function FormatoTelefonoPrepago(ByVal telefono)

        Try
            Dim aux
            aux = telefono
            If aux <> "" Then
                Dim longitud
                Dim posicion
                longitud = Len(telefono)
                If longitud > 0 Then
                    'posicion = 1
                    Do While InStr(1, aux, "0") = 1
                        aux = Mid(aux, 2, Len(aux))
                    Loop
                End If
            End If
            If aux = "" Then
                FormatoTelefonoPrepago = telefono
            Else
                FormatoTelefonoPrepago = aux
            End If

        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "'); </script>")
        End Try

    End Function

    Private Function DatosInteraccion() As Interaccion

        Try

            Dim oInteraccion As New Interaccion

            oInteraccion.TIPO_INTER = ConfigurationSettings.AppSettings("TIPO_INTER_DEFAULT")
            oInteraccion.METODO = ConfigurationSettings.AppSettings("METODO_DEFAULT")
            oInteraccion.USUARIO_PROCESO = ConfigurationSettings.AppSettings("USR_PROCESO")
            oInteraccion.HECHO_EN_UNO = ConfigurationSettings.AppSettings("ECHO_UNO_DEFAULT")
            oInteraccion.FLAG_CASO = ConfigurationSettings.AppSettings("FLAG_CASO_DEFAULT")
            oInteraccion.RESULTADO = ConfigurationSettings.AppSettings("RESULTADO_DEFAULT")

            Return oInteraccion

        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "'); </script>")
        End Try

    End Function

    Function ValorResultCHIP(ByVal strNombreCampo As String, ByVal arrResulFilCHIPNomb() As String, ByVal arrResulFilCHIPValor() As String)
        Try
            Dim i
            For i = 0 To UBound(arrResulFilCHIPNomb)
                If arrResulFilCHIPNomb(i) = strNombreCampo Then
                    ValorResultCHIP = arrResulFilCHIPValor(i)
                    Exit For
                End If
            Next
            If Trim(ValorResultCHIP) = "" Then
                ValorResultCHIP = "X000X"
            End If

        Catch ex As Exception
            Response.Write("<script> alert('" + ex.Message + "'); </script>")
        End Try
    End Function

    Public Function ActualizaEstadoEmision(ByVal strOficinaVenta As String, ByVal strNroPedidoSAP As String) As Integer

        Dim MensajeFinal As String
        MensajeFinal = String.Empty

        Dim strIdentifyLog As String = strNroPedidoSAP
        Dim strEstadoEmisionEmitidoCEL As String = ConfigurationSettings.AppSettings("constEstadoEmisionEmitidoCEL")
        Dim intResultado As Integer = 0

        Try

            Dim objGeneral As New COM_SIC_Cajas.clsConsultaGeneral

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ActualizaEstadoEmisionCEL-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroPedidoSAP : " & strNroPedidoSAP)

            intResultado = objGeneral.FP_Actualiza_Estado_Emision_CEL(strOficinaVenta, strNroPedidoSAP)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Resultado : " & intResultado.ToString)

            If intResultado = 0 Then
                MensajeFinal = "La actualización se realizo satisfactoriamente."
            Else
                MensajeFinal = "Ha ocurrido un error al actualizar el estado de emision CEL"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  MensajeFinal : " & MensajeFinal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin ActualizaEstadoEmisionCEL--------------")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ActualizaEstadoEmision)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin ActualizaEstadoEmisionCEL--------------")
            intResultado = 0

        End Try

        Return intResultado

    End Function

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

        Try
            Dim dvPagos As New DataView(datPedidosCEL)
            Dim strCodSAP As String
            Dim strDepGar As String
            Dim strTipoDoc As String
            Dim strCodSunat As String
            Dim strFKDAT As String
            Dim strTotal As String
            Dim intResultado As Integer = 0
            Dim i As Integer = 0

            Dim intCodResultCambioSim As Integer
            Dim strMsgResultCambioSim As String

            If CheckTodos.Checked Then

                Dim strMensajeError As String = ""
                Dim strScriptImpresion As String = ""

                If dvPagos.Table.Rows.Count > 0 Then
                    For i = 0 To dvPagos.Table.Rows.Count - 1
                        drFila = dvPagos.Item(i).Row

                        strCodSAP = drFila.Item("VBELN")
                        strDepGar = ""
                        strCodSunat = drFila.Item("XBLNR")
                        strFKDAT = drFila.Item("FKDAT")
                        strTotal = drFila.Item("TOTAL")

                        GenerarDetalleChipSap(strCodSAP)

                        intResultado = RegistrarPagoSAP(strCodSAP, strCodSunat, strFKDAT, strTotal)

                        If intResultado = 1 Then
                            Response.Write("<script> alert('No se registro de manera correcta el pago en SAP.'); </script>")
                            Exit Sub
                        End If

                        intCodResultCambioSim = ActivaChipRepuesto(strCodSAP, strMsgResultCambioSim)

                        If intCodResultCambioSim = 0 Then
                            If ActualizaEstadoEmision(strOficinaVta, strCodSAP) = 0 Then
                                strScriptImpresion += "<script>f_Imprimir('" & strCodSAP & "','" & strDepGar & "','" & strTipoDoc & "','" & strCodSunat & "');</script>"
                                'RegisterStartupScript("script", "<script>f_Imprimir('" & strCodSAP & "','" & strDepGar & "','" & strTipoDoc & "','" & strCodSunat & "');</script>")
                            Else
                                strMensajeError += "Doc. SAP:" & strCodSAP & ": No se actualizó de manera correcta el estado de emisión\n"
                            End If
                        Else
                            strMensajeError += "Doc. SAP:" & strCodSAP & ": " & strMsgResultCambioSim & "\n"
                        End If
                    Next
                End If

                If strScriptImpresion <> "" Then
                    RegisterStartupScript("script", strScriptImpresion)
                End If

                If strMensajeError <> "" Then
                    Response.Write("<script> alert('" & strMensajeError & "'); </script>")
                End If
            Else
                If Len(Trim(Request.Item("rbPagos"))) > 0 Then
                    dvPagos.RowFilter = "VBELN=" & Request.Item("rbPagos")
                    drFila = dvPagos.Item(0).Row

                    strCodSAP = drFila.Item("VBELN")
                    strDepGar = "" 'drFila.Item("NRO_DEP_GARANTIA")
                    strCodSunat = drFila.Item("XBLNR")
                    strFKDAT = drFila.Item("FKDAT")
                    strTotal = drFila.Item("TOTAL")

                    GenerarDetalleChipSap(strCodSAP)

                    intResultado = RegistrarPagoSAP(strCodSAP, strCodSunat, strFKDAT, strTotal)

                    If intResultado = 1 Then
                        Response.Write("<script> alert('No se registro de manera correcta el pago en SAP'); </script>")
                        Exit Sub
                    End If

                    intCodResultCambioSim = ActivaChipRepuesto(strCodSAP, strMsgResultCambioSim)   ' 0:OK

                    If intCodResultCambioSim = 0 Then
                        If ActualizaEstadoEmision(strOficinaVta, strCodSAP) = 0 Then
                            RegisterStartupScript("script", "<script>f_Imprimir('" & strCodSAP & "','" & strDepGar & "','" & strTipoDoc & "','" & strCodSunat & "');</script>")
                        Else
                            Response.Write("<script> alert('No se actualizó de manera correcta el estado de emisión en MiClaro'); </script>")
                        End If
                    Else
                        Response.Write("<script> alert('" & strMsgResultCambioSim & "'); </script>")
                    End If
                    RegistrarAuditoria(strCodSAP, "imprimir")
                Else
                    Response.Write("<script>alert('Seleccione algún documento.');</script>")
                End If
            End If

            CargarGrilla(strOficinaVta, strFechaVenta, strTpoPool, strFechaHasta, strNroDocCliente, strTipoDocCliente)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "ERROR : " & ex.Message.ToString())
        End Try

    End Sub


    Public Function RegistrarPagoSAP(ByVal strCodSAP As String, _
                                   ByVal strCodSunat As String, _
                                   ByVal strFKDAT As String, _
                                   ByVal strTotal As String) As Integer

        Dim MensajeFinal As String

        Dim intResultado As Integer = 0
        Dim objPagos As New SAP_SIC_Pagos.clsPagos
        Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
        Dim dsReturn As DataSet
        Dim dsResult As DataSet
        Dim strCompleto As String
        Dim strCorrelativo As String
        Dim i As Integer = 0
        Dim strNumSunat As String
        Dim strDetallePago As String
        Dim blnSunat As Boolean
        Dim strNumAsignaSUNAT As String
        Dim strNumAsignado As String
        Dim strClaseFacturaMiClaro As String = ConfigurationSettings.AppSettings("constClaseFacturaMiClaro")
        Dim strViaPago As String = ConfigurationSettings.AppSettings("constViaPagoMiClaro")

        Dim strIdentifyLog As String = strCodSAP

        Try

            blnSunat = (strCodSunat = "" Or strCodSunat = "0000000000000000")

            If blnSunat Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio - Registrar Pago SAP CEL---------------")

                '-----------------------Validando el Numero Sunat-----------------------
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Get_NumeroSUNAT (Zpvu_Rfc_Trs_Get_Nro_Sunat)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ClaseFactura:" & strClaseFacturaMiClaro)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp ALMACEN:" & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp Caja:" & Session("CodImprTicket"))

                dsReturn = objPagos.Get_NumeroSUNAT(Session("ALMACEN"), strClaseFacturaMiClaro, Trim(Session("CodImprTicket")), "", strCompleto, strCorrelativo)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCompleto:" & strCompleto)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strCorrelativo:" & strCorrelativo)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Get_NumeroSUNAT (Zpvu_Rfc_Trs_Get_Nro_Sunat)")

                For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    If dsReturn.Tables(0).Rows(i).Item(0) = "E" Then
                        intResultado = 1
                        MensajeFinal = dsReturn.Tables(0).Rows(i).Item(3)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " ERROR Get_NumeroSUNAT(): " & dsReturn.Tables(0).Rows(i).Item(3))
                        Return intResultado
                        Exit Function
                    End If
                Next

                '-----------------------Registrando el pago en SAP-----------------------
                If strCodSunat = "" Or strCodSunat = "0000000000000000" Then
                    strNumSunat = strCorrelativo
                Else
                    strNumSunat = strCodSunat
                End If

                If blnSunat Then
                    strNumAsignaSUNAT = strCompleto
                Else
                    strNumAsignaSUNAT = strNumSunat
                End If

                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & Session("ALMACEN") & ";"    ' Concatenar detalles de pago
                strDetallePago = strDetallePago & strViaPago & ";"    ' Codigo del Tipo de documento (Via de Pago)
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & strTotal & ";"    ' Monto de pago
                strDetallePago = strDetallePago & "PEN" & ";"
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & strNumAsignaSUNAT & ";"
                strDetallePago = strDetallePago & strViaPago & ";"    ' GLOSA QUE ES LO MISMO DE VIA_PAGO
                strDetallePago = strDetallePago & strFKDAT & ";"    ' Fecha de documento que esta en el detalle - F_PEDIDO
                strDetallePago = strDetallePago & ";"
                strDetallePago = strDetallePago & ";"


                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Set_NroSunatCajero (Zpvu_Rfc_Trs_Caj_Set_Nro_Sunat)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Documento	  :" & strCodSAP)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Asignar        :" & strNumSunat)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Ref_Franquicia :" & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Oficina_Venta      :" & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Reasignar          :" & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Fecha              :" & strFKDAT)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp strPagos           :" & strDetallePago)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Ref_Corner     :" & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo_Doc_Corner    :" & "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Usuario            :" & Session("USUARIO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Caja               :" & Session("CodImprTicket"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nota_Credito       :" & "")


                dsReturn = objSapCajas.Set_NroSunatCajero(strCodSAP, strNumSunat, "", Session("ALMACEN"), "", strFKDAT, strDetallePago, "", "", Session("USUARIO"), Trim(Session("CodImprTicket")), "", strNumAsignado)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strNumAsignado:" & strNumAsignado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Set_NroSunatCajero (Zpvu_Rfc_Trs_Caj_Set_Nro_Sunat)")

                For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    If dsReturn.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        intResultado = 1
                        MensajeFinal = dsReturn.Tables(0).Rows(i).Item("MESSAGE")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " ERROR Set_NroSunatCajero(): " & dsReturn.Tables(0).Rows(i).Item("MESSAGE"))
                        Return intResultado
                        Exit Function
                    End If
                Next
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin - Registrar Pago SAP CEL---------------")

        Catch ex As Exception

            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: RegistrarPagoSAP)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin - Registrar Pago SAP CEL--------------")
            intResultado = 1
        Finally
            objPagos = Nothing
            objSapCajas = Nothing
        End Try

        Return intResultado

    End Function

    Private Sub GenerarDetalleChipSap(ByVal nroPedido As String)

        Dim arrayDetalle(18) As String
        Dim oConsultaSap As New SAP_SIC_Pagos.clsPagos
        Dim nroLinea As String
        Dim Iccid As String
        Dim motivo As String = ConfigurationSettings.AppSettings("constCodMotivoReposicion")
        Dim puntoVenta As String = Session("ALMACEN")
        Dim codVendedor As String = Session("CodVendedorSAP")
        Dim dsResult As New DataSet
        Dim strIdentifyLog As String = nroPedido
        Dim codMaterial As String


        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio SetGet_LogActivacionCHIP (Zpvu_Rfc_Trs_Activacion_Chip)")

        Try
            dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), nroPedido, "")

            If Not IsNothing(dsResult) Then
                If dsResult.Tables(1).Rows.Count > 0 Then
                    nroLinea = dsResult.Tables(1).Rows(0).Item("NUMERO_TELEFONO")
                    Iccid = dsResult.Tables(1).Rows(0).Item("SERIE")
                    codMaterial = dsResult.Tables(1).Rows(0).Item("ARTICULO")
                End If
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp nroLinea : " & nroLinea)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Iccid Nuevo: " & Iccid)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp cod Material: " & codMaterial)


            Dim oConsultaSans As New NEGOCIO_SIC_SANS.SansNegocio
            Dim usuario_id As String = IIf(IsNothing(CurrentUser), Session("codUsuario"), CurrentUser)

            arrayDetalle(0) = ""
            arrayDetalle(1) = nroLinea
            'arrayDetalle(2) = oConsultaSap.ConsultarIccid("", nroLinea, "")
            arrayDetalle(2) = oConsultaSans.ConsultarIccid("", nroLinea, "", codMaterial, Iccid, usuario_id, "")
            arrayDetalle(3) = ""
            arrayDetalle(4) = puntoVenta
            arrayDetalle(5) = nroPedido
            arrayDetalle(6) = motivo
            arrayDetalle(7) = Iccid
            arrayDetalle(9) = codVendedor
            arrayDetalle(10) = ""
            arrayDetalle(11) = String.Format("{0:dd/MM/yyyy}", Now)
            arrayDetalle(12) = Format(Now, "H:mm:ss")
            arrayDetalle(13) = ConfigurationSettings.AppSettings("strEstadoSolNuevo")
            arrayDetalle(14) = "X"
            arrayDetalle(15) = ""
            arrayDetalle(16) = String.Format("{0:dd/MM/yyyy}", Now)
            arrayDetalle(17) = Format(Now, "H:mm:ss")
            ' Grabar Datos Pedido

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Iccid Anterior: " & arrayDetalle(2).ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp arrayDetalle : " & Join(arrayDetalle, ";"))
            oConsultaSap.SetGet_LogActivacionCHIP("", "", Join(arrayDetalle, ";"), "")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Error: " & ex.Message.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin SetGet_LogActivacionCHIP (Zpvu_Rfc_Trs_Activacion_Chip)")
            oConsultaSap = Nothing
        End Try
    End Sub

    Private Sub RegistrarAuditoria(ByVal strNroDocSap As String, ByVal accion As String)
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim auditoriaGrabado As Boolean

            Dim strCodTrans As String = ConfigurationSettings.AppSettings("codTrsPoolMiClaro")
            Dim descTrans As String

            If accion = "consulta" Then
                descTrans = "Consulta Pool Comprobates MiClaro Fecha: " & txtFecha.Text
            ElseIf accion = "imprimir" Then
                descTrans = "Impresión comprobante Chip Repuesto generado por MiClaro NroDocSAP: " & strNroDocSap
            End If
            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception
            ' Throw New Exception("Error. Registrar Auditoria.")
        End Try
    End Sub
End Class
