Imports System.IO
Imports System.Globalization
Imports System.Configuration
Imports COM_SIC_Activaciones 'INC000001492087

'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/
Imports COM_SIC_RVirtual
'/*----------------------------------------------------------------------------------------------------------------*/
'/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
'/*----------------------------------------------------------------------------------------------------------------*/		
Public Class RecargaVirt
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
    'Inherits System.Web.UI.Page
    Inherits SICAR_WebBase
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents hldVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cboTipDocumento As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboSelectVend As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboImporte As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNTelf As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboClasePedido As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFechaPrecioVenta As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboClasePedido1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnGrabarRec As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    ' PROY-31850 - INI
    Protected WithEvents cmdConsultaI As System.Web.UI.WebControls.Button
    Protected WithEvents btnConsultaI As System.Web.UI.WebControls.Button
    Protected WithEvents hdlMsj As System.Web.UI.HtmlControls.HtmlInputHidden
    'PROY-31850 - FIN
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim objVentas As New SAP_SIC_Ventas.clsVentas
    'Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim blnError As Boolean
    Dim dblDescuento, dblPreIGV, dblPrecio, dblTotal As Double
    Dim strServicio As String
    Dim strDescServicio As String
    Private numeroOperacionSAP% = 0
    Private tbFacturacion As New DataTable
    Private numeroReferenciaSunat As String

    'inicio ts-JTN
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecargaVirtual")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    'fin ts-JTN

    Dim K_PEDIN_NROPEDIDO As Int64 = 0
    Dim K_DESLOG As String = ""
    Dim K_NROLOG As String = ""

    'INI :: PROY-31850 - MEJORA RECARGAS
    Dim blnLineaOLO As Boolean = False
    Dim strInicioNroOLO As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("constInicioNumeroOLO"))
    Dim intLongNroOLO = strInicioNroOLO.Length()
    'FIN :: PROY-31850 - MEJORA RECARGAS

    Dim strIdentifyLog As String = ""
    Dim strValorIGV As Double 'PROY-31766'

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
       
        Session("FlagPoolPagos") = Nothing
        
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Carga el Load de la RV")
            Dim dcVendedor As New DataColumn
            Dim dcVendedor2 As New DataColumn
            Dim dsVendedor As DataSet
            Dim dsTipDoc As DataSet
            Dim dvTipDoc As New DataView
            Dim dsDocVta As DataSet
            Dim dcClasePed As New DataColumn
            Dim dvClasePed As New DataView
            Dim dvClasePed1 As New DataView
            Dim dsOficina As DataSet
            Dim i As Integer


            cboTipDocumento.Attributes.Add("onChange", "f_CambiaTipoDocVenta()")
            txtNumDocumento.Attributes.Add("onKeyDown", "textCounter(this)")
            txtNumDocumento.Attributes.Add("onKeyUp", "textCounter(this)")
            'btnGrabar.Attributes.Add("onClick", "f_Valida();")

            Dim objOffline As New COM_SIC_OffLine.clsOffline

            If Not Page.IsPostBack Then
                ' PROY-31850 FASE IV - INI
                txtNTelf.Attributes.Add("onBlur", "f_ConsultaI()")
                ' PROY-31850 FASE IV - FIN
                txtFechaPrecioVenta.Text = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Date
                '''TODO: AGREGADO POR JYMMY TORRES
                'dsOficina = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                dsOficina = objOffline.ParametrosVenta(Session("ALMACEN"))

                Dim isDbnull As Boolean = Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
                Dim numDocumento$
                Dim leerClienteVarios As Boolean = (ConfigurationSettings.AppSettings("FLAG_COD_CLTE_VARIOS") = 1)

                If (leerClienteVarios) Then
                    numDocumento$ = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS")
                Else
                    numDocumento$ = IIf(isDbnull, ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"), _
                                        Trim(Convert.ToString(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))))
                End If


                txtNumDocumento.Text = numDocumento
                '''CAMBIADO HASTA AQUI

                'dsVendedor = objVentas.Get_ConsultaVendedor(Session("ALMACEN")) ''TODO: CALLBACK SAP
                ''TODO: AGREGADO POR JYMMY TORRES
                Try
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Inicia consulta los datos de los vendedores.")
                    dsVendedor = objOffline.ListarVenderoresPorTienda(Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Fin consulta los datos de los vendedores.")
                    ''AGREGADO HASTA AQUI
                    If Not dsVendedor Is Nothing Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Inicia asigna los valores consultados al vendedor.")
                        dcVendedor.ColumnName = "USUARIOFONO"
                        dcVendedor.DataType = GetType(String)
                        dcVendedor.Expression = "USUARIO+'#'+TELEFONO"
                        dsVendedor.Tables(0).Columns.Add(dcVendedor)

                        dcVendedor2.ColumnName = "USUARIONOMBRE"
                        dcVendedor2.DataType = GetType(String)
                        dcVendedor2.Expression = "USUARIO+' - '+TRIM(NOMBRE)"
                        dsVendedor.Tables(0).Columns.Add(dcVendedor2)

                        cboSelectVend.DataSource = dsVendedor.Tables(0)
                        cboSelectVend.DataValueField = "USUARIOFONO"
                        cboSelectVend.DataTextField = "USUARIONOMBRE"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Fin asigna los valores consultados al vendedor.")
                    End If
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Error al asignar los valores del vendedor.")
                End Try

                'dsTipDoc = objVentas.Get_LeeTipoDocCliente ''TODO: CALLBACK SAP
                ''TODO: AGREGADO POR JYMMY TORRES
                dsTipDoc = objOffline.ListarTipoDocumentosCliente()
                ''AGREGADO HASTA AQUI

                If Not dsTipDoc Is Nothing Then
                    dvTipDoc.Table = dsTipDoc.Tables(0)
                    dvTipDoc.RowFilter = "J_1ATODC <> '-'"

                    cboTipDocumento.DataSource = dvTipDoc
                    cboTipDocumento.DataValueField = "J_1ATODC"
                    cboTipDocumento.DataTextField = "TEXT30"
                End If


                'dsDocVta = objVentas.Get_ConsultaClasePedido(Session("ALMACEN")) '' TODO: CALLBACK SAP
                ''TODO: AGREGADO POR JYMMY TORRES   
                'dsDocVta = objOffline.ListarClasePedido(Session("ALMACEN"))
                dsDocVta = objOffline.ConsultaClasePedido(Session("ALMACEN"))
                ''AGREGADO HASTA AQUI

                If Not dsDocVta Is Nothing Then
                    dcClasePed.ColumnName = "AUART2"
                    dcClasePed.DataType = GetType(String)
                    dcClasePed.Expression = "AUART+RECIBE_PAGO"
                    dsDocVta.Tables(0).Columns.Add(dcClasePed)

                    dvClasePed.Table = dsDocVta.Tables(0)
                    'dvClasePed.RowFilter = "KSCHL='ZFAC' or KSCHL='FAC' or KSCHL='ZNCV' or KSCHL='NPED' or KSCHL='DEV' or KSCHL='N/C'"

                    dvClasePed1.Table = dsDocVta.Tables(0)
                    'dvClasePed1.RowFilter = "KSCHL<>'ZFAC' and KSCHL<>'FAC'"

                    cboClasePedido.DataSource = dvClasePed
                    cboClasePedido.DataValueField = "AUART2"
                    cboClasePedido.DataTextField = "BEZEI"

                    cboClasePedido1.DataSource = dvClasePed1
                    cboClasePedido1.DataValueField = "AUART2"
                    cboClasePedido1.DataTextField = "BEZEI"
                End If


                'carga de recargas Javier 23/02/2009
                cargaRecarga() ''TODO: CALLBACK ORACLE, Binds cboImporte
                Me.DataBind()


                If Not dsTipDoc Is Nothing Then
                    cboTipDocumento.SelectedValue = "1" 'DNI
                End If

                If Trim(Session("TipDoc")) <> "" Then
                    cboTipDocumento.SelectedValue = Session("TipDoc")
                    txtNumDocumento.Text = Session("NumDoc")
                    Session("TipDoc") = ""
                    Session("NumDoc") = ""
                End If

                If Session("CANAL") = "MT" And Convert.ToString(dsOficina.Tables(0).Rows(0).Item("VAL_VEND_POST")) <> "" Then
                    cboSelectVend.Enabled = False
                    objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV " & " Inicio de la asignacion de los valores del vendedor.")
                    For i = 0 To cboSelectVend.Items.Count - 1
                        'If Right(Left(cboSelectVend.Items(i).Value, 10), 5) = Session("USUARIO") Then
                        '    cboSelectVend.SelectedIndex = i
                        '    Exit For
                        'End If
                        If Funciones.CheckInt64(Trim(Funciones.CheckStr(Left(cboSelectVend.Items(i).Value, 10)))) = Session("USUARIO") Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Load - RV (cboSelectVend): " & Funciones.CheckStr(cboSelectVend.Items(i).Value))
                            cboSelectVend.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    cboSelectVend.Enabled = True
                End If

                cboSelectVend.Items.Insert(0, "")

                For i = 0 To cboClasePedido.Items.Count - 1
                    If Left(cboClasePedido.Items(i).Value, 4) = "ZPVR" Then
                        cboClasePedido.SelectedIndex = i
                    End If
                Next

                For i = 0 To cboClasePedido1.Items.Count - 1
                    If Left(cboClasePedido1.Items(i).Value, 4) = "ZPBR" Then
                        cboClasePedido1.SelectedIndex = i
                    End If
                Next

            End If
        End If
        'txtNTelf.Text = "997670263"
        'cboSelectVend.SelectedIndex = 1
    End Sub

    Public Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click

        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Inicia el: btnGrabar_Click ")
        Dim strClasePedido As String
        Dim strIdentifyLog As String = txtNumDocumento.Text

        ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
        If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
            Response.Write("<script>alert('Error: No puede realizar esta operacion, ya realizo cuadre de caja')</script>")
            Exit Sub
        End If
        ''' FIN VERIFICACION

        Dim dsPrecio As DataSet
        Dim i As Integer ''QUE ES I???
        Dim dsParamOf As DataSet
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim dsOficina As DataSet

        ''TODO: CAMBIADO POR JYMMY TORRES
        'dsOficina = objPagos.Get_ParamGlobal(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Consulta los datos de la oficina ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Consulta los datos de la oficina ParametrosVenta")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Param:" & Session("ALMACEN"))
        dsOficina = (New COM_SIC_OffLine.clsOffline).ParametrosVenta(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Fin consulta datos oficina. ")

        ''CAMBIADO HASTA AQUI

        If cboTipDocumento.SelectedValue = "6" Then
            strClasePedido = cboClasePedido.SelectedValue
        Else
            strClasePedido = cboClasePedido1.SelectedValue
        End If

        dsParamOf = objConf.FP_Lista_Param_Oficina(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("CodAplicacion")) '' TODO CALLBACK ORACLE

        '        strServicio = "SERECVILIM"
        '        strDescServicio = "SERVICIO RECARGA VIRTUAL LIMA"
        strServicio = dsParamOf.Tables(0).Rows(0).Item("CAJA_RECVIRTUAL")
        strDescServicio = dsParamOf.Tables(0).Rows(0).Item("CAJA_RECVIRTDES")
        ''' TODO: CONSULTA REALIZADA A LGZ
        ''' PENDIENTE DE CAMBIO


        'COMENTADO POR TS.JTN INICIO

        '                                       OF.VTA       POS       ARTICULO    LP        CANTIDAD                  FECHA                       TELEFONO        CLASEPEDIDO          DESCUENTO   PRE+IGV      PRECIO    TOTAL                        
        'dsPrecio = objVentas.Get_ConsultaPrecio(Session("almacen"), "", "000001", strServicio, "01", cboImporte.SelectedValue, txtFechaPrecioVenta.Text, "", txtNTelf.Text, strClasePedido, "", Trim(dsOficina.Tables(0).Rows(0).Item("vtweg")), Trim(dsOficina.Tables(0).Rows(0).Item("vkorg")), dblDescuento, dblPreIGV, dblPrecio, dblTotal) ''todo: callback sap

        '//dsPrecio = objVentas.Get_ConsultaPrecio("0006", "0", "000000", "SERECVILIM", "01", cboImporte.SelectedValue, txtFechaPrecioVenta.Text, "", "0", "ZPBR", "", "MT", "TC01", dblDescuento, dblPreIGV, dblPrecio, dblTotal)
        Me.calculaMontos()

        'For i = 0 To dsPrecio.Tables(0).Rows.Count - 1
        '    If dsPrecio.Tables(0).Rows(i).Item("type") = "e" Then
        '        blnError = True
        '        Response.Write("<script>alert('" & dsPrecio.Tables(0).Rows(i).Item("message") & "')</script>")
        '    End If
        'Next
        'Response.Write("<script>alert('proceso de recarga OK, antes de grabar pedido')</script>")
        'COMENTADO POR TS.JTN FIN
        If Not blnError Then

            'Session("CodImprTicket")
            'Dim codigoCaja As String = objOffline.ObtenerCaja(codUsuario, Session("ALMACEN"))
            Dim codigoCaja As String = Session("CodImprTicket")
            Dim fkart = ConfigurationSettings.AppSettings("constClaseVoleta").ToString

            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Out Session(ALMACEN): " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Out drPagos.Item(FKART): " & fkart)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Out strCorrelativo(codigoCaja): " & codigoCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO ObtenerUltimoCorrelativoSunat")
            'numeroReferenciaSunat = objPagos.ObtenerUltimoCorrelativoSunat(Session("ALMACEN"), fkart, codigoCaja)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Verifica campo Telefono")
            Dim NroTelefonoReca As String = txtNTelf.Text
            
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Inicia GrabarPedido()")
            GrabarPedido()
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Fin a GrabarPedido()")
        End If
    End Sub

    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/
    'Se creará un nuevo método RegistrarRecargaVirtual() desde este método se registrarnlos detalles de la recarga virtual en la tabla 
    'SICAT_RECARGA_VIRTUAL_TRX. Se invocara el siguiente WS: BSS_RECARGAVIRTUAL Metodo: CREARRECARGA.

    Private Function RegistrarRecargaVirtual(ByVal strIdentifyLog As String, _
                                             ByVal strCanal As String, _
                                             ByVal strUserSesion As String, _
                                             ByVal arrCabecera() As String, _
                                             ByVal arrDetalle() As String) As Boolean

        Dim objRegistrarRecVirtual As New COM_SIC_RVirtual.clsBSS_RVirtual
        Dim objResponse As New COM_SIC_RVirtual.BEResponseRecargaVirtual
        Dim strEstadoPendienteRV As String

        'Leer los estados de la recarga Virtual de la tabla de parametros
        'PENDIENTE DE PAGO, PAGADO, CONFIRMADO POR SWITCH, DEVUELTO'
        If (Session("ESTADO_RECARGA_V") Is Nothing) Then
            Dim codGrupo As Integer = Funciones.CheckDbl(ConfigurationSettings.AppSettings("constGrupoParam_EstadosRecargaVirtual"))
            Dim dsCodigos As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)
            If Not IsNothing(dsCodigos) Then
                strEstadoPendienteRV = Funciones.CheckStr(dsCodigos.Tables(0).Rows(0).Item("PARAV_VALOR"))
            End If
            Session("ESTADO_RECARGA_V") = dsCodigos
        Else
            Dim dsCodigos As DataSet = Session("ESTADO_RECARGA_V")
            strEstadoPendienteRV = Funciones.CheckStr(dsCodigos.Tables(0).Rows(0).Item("PARAV_VALOR"))
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ------------------------------------------------------- ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " INICIO RegistrarRecargaVirtual() WS:WBSS_RecargaVirtual ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " URL: " & System.Configuration.ConfigurationSettings.AppSettings("constRutaCrearRecargaVirtual"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " BD: SICAT_RECARGA_VIRTUAL_TRX ")

        Try
            Dim dsDatos As DataSet
            Dim K_COD_RESP As String = ""
            Dim K_MEN_RESP As String = ""
            Dim itemRecarga As New COM_SIC_RVirtual.BERecargaVirtual
            Dim itemHeaderDataPower As New COM_SIC_RVirtual.BEHeaderDataPower

            With itemRecarga
                .linea = Funciones.CheckStr(txtNTelf.Text) 'Funciones.CheckStr(Session("numeroTelefono"))
                .estado = ""
                .fecha = ""
                .nombreUsuario = strUserSesion
                .puntoVenta = Session("ALMACEN")
                .tipoDocumento = Funciones.CheckStr(cboTipDocumento.SelectedValue)
                .numeroDocumento = Funciones.CheckStr(txtNumDocumento.Text)
                .lineaCliente = Funciones.CheckStr(txtNTelf.Text)
                .montoRecarga = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .fechaSwTrx = "" 'Fecha  respuesta del switch transaccional"
                .valorVenta = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .valorDescuento = "0"
                .valorSubTotal = Funciones.CheckDbl(dblPrecio) 'Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .valorIGV = Funciones.CheckDbl(dblPreIGV) 'arrDetalle(16)
                .valorTotal = Funciones.CheckDbl(Funciones.CheckDbl(dblPrecio) + Funciones.CheckDbl(dblPreIGV)) 'arrDetalle(11)
                .estadoInsertar = strEstadoPendienteRV 'ConfigurationSettings.AppSettings("constEstadoInsertarRecVirtual").ToString 'Estado: PENDIENTE DE PAGO, PAGADO, CONFIRMADO POR SWITCH, DEVUELTO'
                .trace = ""
                '.flagOperacion = ""
                .listaResquestOpcional = ""
            End With

            itemHeaderDataPower.consumer = ""
            itemHeaderDataPower.country = ""
            itemHeaderDataPower.dispositivo = ""
            itemHeaderDataPower.language = ""
            itemHeaderDataPower.modulo = ""
            itemHeaderDataPower.msgType = ""
            itemHeaderDataPower.operation = ""
            itemHeaderDataPower.pid = ""
            itemHeaderDataPower.userId = ""
            itemHeaderDataPower.wsIp = ConfigurationSettings.AppSettings("consIpBSS_RecargaVirtual").ToString
            itemHeaderDataPower._system = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " <<<<<<< Parametros de Entrada al OSB >>>>>>> ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " linea :  " & itemRecarga.linea)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " estado :  " & itemRecarga.estado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " fecha :  " & itemRecarga.fecha)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " nombreUsuario :  " & itemRecarga.nombreUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " puntoVenta :  " & itemRecarga.puntoVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " tipoDocumento :  " & itemRecarga.tipoDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " numeroDocumento :  " & itemRecarga.numeroDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " lineaCliente :  " & itemRecarga.lineaCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " montoRecarga :  " & itemRecarga.montoRecarga)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " fechaSwTrx :  " & itemRecarga.fechaSwTrx)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " valorVenta :  " & itemRecarga.valorVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " valorDescuento :  " & itemRecarga.valorDescuento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " valorSubTotal :  " & itemRecarga.valorSubTotal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " valorIGV :  " & itemRecarga.valorIGV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " valorTotal :  " & itemRecarga.valorTotal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " estadoInsertar :  " & itemRecarga.estadoInsertar)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " trace :  " & itemRecarga.trace)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " flagOperacion :  " & itemRecarga.flagOperacion)

            ' Leer Usuario y Contraseña del Key Regedit
            Dim KeyUsrPasRecVirtual As String = ConfigurationSettings.AppSettings("KeyUsrPasRecVirtual")
            Dim objsegu As COM_SIC_Seguridad.Configuracion = New COM_SIC_Seguridad.Configuracion(KeyUsrPasRecVirtual)
            Dim usuario As String
            Dim contraseña As String

            'INC000001492087: INI
            Dim strUsrAplicacionEncriptado As String
            Dim strClaveUsrEncriptado As String
            strUsrAplicacionEncriptado = objsegu.LeerUsuarioEncriptado()
            strClaveUsrEncriptado = objsegu.LeerContrasenaEncriptado()

            Try
                Dim strIDTrans_ As String = String.Empty
                Dim strIpAplicacion_ As String = String.Empty
                Dim strAplicacion_ As String = String.Empty
                strIDTrans_ = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
                strAplicacion_ = ConfigurationSettings.AppSettings("constAplicacion")
                strIpAplicacion_ = Request.ServerVariables("LOCAL_ADDR")
                Dim sAuditoriastring_ As New AuditoriaEWS
                sAuditoriastring_.IDTRANSACCION = strIDTrans_
                sAuditoriastring_.IPAPLICACION = strIpAplicacion_
                sAuditoriastring_.APLICACION = strAplicacion_
                sAuditoriastring_.USRAPP = CurrentUser

                Dim objClave As ConsultaClavesNegocio = New ConsultaClavesNegocio
                Dim mensajeError As String = ""

                Dim codAplicacionClave As String = ConfigurationSettings.AppSettings("strAplicacionSISACT")
                Dim usuarioEncrypt As String = strUsrAplicacionEncriptado
                Dim claveEncrypt As String = strClaveUsrEncriptado
                Dim usuarioDesencrypt As String = String.Empty
                Dim claveDesencrypt As String = String.Empty

                objClave.ejecutarConsultaClave(sAuditoriastring_, codAplicacionClave, usuarioEncrypt, claveEncrypt, usuarioDesencrypt, claveDesencrypt, mensajeError)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ejecutarConsultaClave Mensaje ws :" & mensajeError)

                usuario = usuarioDesencrypt
                contraseña = claveDesencrypt

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "ejecutarConsultaClave Exception :" & ex.Message)
            End Try
            'INC000001492087: FIN

            Dim ipAplicacion As String = CurrentTerminal

            objResponse = objRegistrarRecVirtual.CrearRecargaVirtual(strCanal, _
                                                               strUserSesion, _
                                                               itemRecarga, _
                                                               itemHeaderDataPower, _
                                                               usuario, _
                                                               contraseña, _
                                                               ipAplicacion)

            Dim K_estado As String = objResponse.K_estado
            Dim k_codigo_respuesta As String = objResponse.k_codigo_respuesta
            Dim k_descripcion As String = objResponse.k_descripcion
            Dim k_ubicacionError As String = objResponse.k_ubicacionError
            Dim k_fecha As String = objResponse.k_fecha
            Dim k_origen As String = objResponse.k_origen
            Dim k_xmlRequest As String = objResponse.k_XML_Request
            Dim k_xmlResponse As String = objResponse.k_XML_Response

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " <<<<<<< Parametros de Salida del OSB >>>>>>> ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.K_estado = " & K_estado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_codigo_respuesta = " & k_codigo_respuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_descripcion = " & k_descripcion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_ubicacionError = " & k_ubicacionError)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_fecha = " & k_fecha)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_origen = " & k_origen)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_XML_Request = " & k_xmlRequest)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " objResponse.k_XML_Response = " & k_xmlResponse)
           
            If k_codigo_respuesta = "0" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ERROR = " & ex.Message)
        End Try

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " FIN  RegistrarRecargaVirtual() WS:WBSS_RecargaVirtual ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & " ------------------------------------------------------- ")
    End Function

    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/

    Private Sub GrabarPedido()
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Entra en el GrabarPedido()")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Inicia declaraciòn variales")


        Dim dsResultado As DataSet
        Dim i As Integer
        Dim strFecha As String
        Dim strRealizado As String
        Dim strTipDocVta As String
        Dim arrCabecera(49) As String
        Dim arrDetalle(27) As String
        Dim strCabecera As String
        Dim strDetalle As String
        Dim dblMonto As Double
        Dim dblIGV As Double
        Dim intAux As Integer

        Dim strEntrega As String
        Dim strFactura As String
        Dim strNroContrato As String
        Dim strNroDocCliente As String
        Dim strNroPedido As String
        Dim strRefHistorico As String
        Dim strTipDocCliente As String
        Dim dblValorDescuento As Decimal
        Dim dvFiltro As New DataView
        Dim drFila As DataRow
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim intOper As Double

        'Variables de Auditoria

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        Dim wParam1 As Long
        Dim wParam2 As String
        Dim wParam3 As String
        Dim wParam4 As Long
        Dim wParam5 As Integer
        Dim wParam6 As String
        Dim wParam7 As Long
        Dim wParam8 As Long
        Dim wParam9 As Long
        Dim wParam10 As String
        Dim wParam11 As Integer
        Dim Detalle(5, 3) As String
        'Fin variables de auditoria

        Dim objOffline As New COM_SIC_OffLine.clsOffline

        'Auditoria
        Dim dsOficina As DataSet

        '/*----------------------------------------------------------------------------------------------------------------*/
        '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
        '/*----------------------------------------------------------------------------------------------------------------*/
        Dim NroDocumentoVentaPVU As Int64 = 0               'numero de documento de venta en PVU tabla SISACT_AP_VENTA
        Dim objTrsPvu As New COM_SIC_Activaciones.clsTrsPvu 'objeto qpara grabar la venta en SISACT_INFO_VENTA_SAP
        '/*----------------------------------------------------------------------------------------------------------------*/
        '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
        '/*----------------------------------------------------------------------------------------------------------------*/	

        '*******************************************************'
        'Guardar Pedido/Detalle en SINERGIA_MSSAP 6.0 31-12-14
        Dim objAct As New COM_SIC_Activaciones.clsTrsMsSap
        Dim objMsSAP As New COM_SIC_Activaciones.clsConsultaMsSap
        '*******************************************************'

        'dsOficina = objPagos.Get_ParamGlobal(Session("ALMACEN")) ''TODO: CALLBACK SAP TRAE LOS PARAMETROS DE VENTA
        ''TODO: CAMBIADO POR JYMMY TORRES
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fin declaraciòn variales")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Inicia ParametrosVenta")
        dsOficina = objOffline.ParametrosVenta(Session("ALMACEN"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fin ParametrosVenta")
        ''CAMBIADO HASTA AQUI


        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Inicia asigna variables al wParam")
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcRecF")
        wParam5 = 1
        wParam6 = "Recarga Virtual Frecuente"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtRecF")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fin asigna variables al wParam")


        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fechas:")
        strFecha = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- strFecha" & strFecha)
        Dim dateTimeValue As DateTime
        Dim cultureName As String = "es-PE"
        Dim culture As CultureInfo = New CultureInfo(cultureName)
        dateTimeValue = Convert.ToDateTime(strFecha, culture)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- dateTimeValue: " & Funciones.CheckStr(dateTimeValue))



        'dsResultado = objVentas.Get_VerificaVendedor(Session("ALMACEN"), Left(cboSelectVend.SelectedValue, 10)) ''TODO: CALLBACK SAP
        ''TODO: CAMBIADO POR JYMMY TORRES
        '''dsResultado = objOffline.GetVerificaVendedor(Session("ALMACEN"), Left(cboSelectVend.SelectedValue, 10)) ''QUITADO
        '''CAMBIADO HASTA AQUI
        blnError = False

        'If BuscaCliente() Then ''TODO: CALLBACK SAP
        'For i = 0 To dsResultado.Tables(0).Rows.Count - 1
        '    If dsResultado.Tables(0).Rows(i).Item("TYPE") = "E" Then
        '        blnError = True
        '        wParam5 = 0
        '        wParam6 = "El vendedor no fue autorizado. Recarga Virtual frecuente"
        '        Response.Write("<script>alert('Mensaje Error : Vendedor no autorizado')</script>")
        '    End If
        'Next
        If Not blnError Then

            '************** VALIDACIÒN DE ASIGNACIÒN DE CAJA ******************'
            '** IDG **'
            Dim cod_usuario_formateado As String = ""
            cod_usuario_formateado = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicia consulta caja asignada: objMsSAP.ObtenerCajaAsignada ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Paràmetros: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param1:(strFecha) " & strFecha.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & cod_usuario_formateado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: " & Funciones.CheckStr(Session("ALMACEN")))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param2: SICAR.PCK_SICAR_OFF_SAP.MIG_ObtenerCajaAsignada")

            Try
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Ejecuta consulta")
                dsResultado = objMsSAP.ObtenerCajaAsignada(strFecha, cod_usuario_formateado, Funciones.CheckStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin consulta")
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-Error al consultar.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-Err." & ex.Message.ToString)
            End Try


            If dsResultado Is Nothing Then
                '**no se encontro datos en la consulta.
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- No existe ninguna caja asignada, termina el proceso.")
                blnError = True
                Response.Write("<script>alert('No exite ninguna caja asignada, favor de verificar.')</script>")
                Exit Sub
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Validamos si el campo CAJA_CERRADA tiene valores (S-N)")
                If dsResultado.Tables(0).Rows.Count <= 0 Then
                    blnError = True
                    Response.Write("<script>alert('No exite ninguna caja asignada, favor de verificar.')</script>")
                    Exit Sub
                Else
                    If IsDBNull(dsResultado.Tables(0).Rows(0).Item("CAJA_CERRADA")) Then        '** validamos que no sea NULL  **'
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- El valor de la caja no esta bien registrada, es NULL")
                        blnError = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Consultar la tabla T_TRS_CAJA_DIARIO, la columna CAJA_CERRADA")
                        Response.Write("<script>alert('El indicador de caja cerrada no es el correcto, verificar.')</script>")
                        Exit Sub
                    Else
                        '**Para verificar si la caja esta cerrada.
                        strRealizado = Funciones.CheckStr(dsResultado.Tables(0).Rows(0).Item("CAJA_CERRADA"))
                    End If
                End If
            End If

            '*****dsResultado = objVentas.Get_ConsultaCierreCaja(Session("ALMACEN"), strFecha, "", strRealizado) ''TODO: CALLBACK SAP
            ''*****TODO: CAMBIADO POR JYMMY TORRES

            '***************************************************
            'IDG
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " consulta el cierre de caja")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " consulta el cierre de caja: GetConsultaCierreCaja")
            dsResultado = objOffline.GetConsultaCierreCaja(Session("ALMACEN"), strFecha, "", strRealizado) ''v/
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Fin GetConsultaCierreCaja")
            'FDG
            ''****CAMBIADO HASTA AQUI
            '** FDG  **'
            '********************FIN VALIDACIÒN CAJA ******************'


            If Not blnError Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Validad blnError I.")


                '****//
                'INICIO JTN
                'dsResultado = objVentas.Get_ConsultaCierreCaja(Session("ALMACEN"), strFecha, "", strRealizado)
                'For i = 0 To dsResultado.Tables(0).Rows.Count - 1
                '    If dsResultado.Tables(0).Rows(i).Item("TYPE") = "E" Then
                '        blnError = True
                '        wParam5 = 0
                '        wParam6 = "Error al consultar el Cierre de caja. Recarga Virtual Frecuente"
                '        Response.Write("<script>alert('" & dsResultado.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
                '    End If
                'Next
                '***//

                If Not blnError Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Validad blnError II.")
                    If strRealizado = "S" Then
                        blnError = True
                        wParam5 = 0
                        wParam6 = "Cierre de caja ya efectuado. Recarga Virtual Frecuente"
                        Response.Write("<script>alert('No se puede realizar pedidos por que se realizo el cierre de caja')</script>")
                    Else

                        '***EJECUTA LA RECARGA VIRTUAL: ************************************************'
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia Mètodo : VerificaSaldoTienda")
                        Try
                            'PROY-31850 FASE IV - INI
                            If (txtNTelf.Text.Trim.Substring(0, intLongNroOLO) <> strInicioNroOLO) Then
                            VerificaSaldoTienda()
                            End If
                            'PROY-31850 FASE IV - FIN
                        Catch ex As Exception
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia Mètodo : VerificaSaldoTienda")
                        End Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Error: VerificaSaldoTienda()")
                        '*******************************************************************************'

                        If Not blnError Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "     Validad blnError III.")
                            If cboTipDocumento.SelectedValue = 6 Then
                                strTipDocVta = IIf(Right(Trim(cboClasePedido.SelectedValue), 1) = "X" And Len(Trim(cboClasePedido.SelectedValue)) > 4, Left(cboClasePedido.SelectedValue, 4), cboClasePedido.SelectedValue)
                            Else
                                strTipDocVta = IIf(Right(Trim(cboClasePedido1.SelectedValue), 1) = "X" And Len(Trim(cboClasePedido1.SelectedValue)) > 4, Left(cboClasePedido1.SelectedValue, 4), cboClasePedido1.SelectedValue)
                            End If
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " strTipDocVta=>" & strTipDocVta.ToString)
                            arrCabecera(1) = strTipDocVta
                            arrCabecera(2) = Session("ALMACEN")
                            arrCabecera(3) = txtFechaPrecioVenta.Text
                            arrCabecera(4) = cboTipDocumento.SelectedValue
                            arrCabecera(5) = txtNumDocumento.Text
                            arrCabecera(6) = ""
                            arrCabecera(7) = "L"  'moneda
                            arrCabecera(16) = "02" 'Se asume prepago 'Session("TipVenta")
                            arrCabecera(27) = Left(cboSelectVend.SelectedValue, 10)
                            arrCabecera(24) = ""


                            arrCabecera(25) = ""


                            arrCabecera(29) = "01"  ' se asume ALTA

                            arrDetalle(1) = "000001"  'pos

                            '******************************************************************'
                            'arrDetalle(2) = strServicio 'articulo   '**LINEA ANTERIOR 30-12-14
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Inicio RetornaCodigoServicio")
                            arrDetalle(2) = RetornaCodigoServicio(strServicio.Trim).Trim
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Fin RetornaCodigoServicio")
                            '******************************************************************'

                            arrDetalle(3) = strDescServicio 'desc Articulo
                            arrDetalle(4) = "01" 'Lista
                            arrDetalle(5) = "PVP" 'Desc. Lista
                            arrDetalle(6) = "0001" 'Campaña
                            arrDetalle(7) = "NO DEFINIDO" 'Desc. Campaña
                            arrDetalle(8) = ""  'IMEI
                            'PROYEC 31850 FASE IV INI
                            Dim strNroTlfolo As String
                            Dim codigos_Prec_CodRecarga() As String
                            Dim cantidad
                            strNroTlfolo = txtNTelf.Text.Trim()
                            codigos_Prec_CodRecarga = Split(cboImporte.SelectedValue, ";")
                            If (strNroTlfolo.ToString.StartsWith(strInicioNroOLO)) Then
                                Dim importe As String
                                importe = codigos_Prec_CodRecarga(0)
                                cantidad = importe
                            Else
                                cantidad = cboImporte.SelectedValue  'Cantidad
                            End If
                            'PROYEC 31850 FASE IV FIN
                            arrDetalle(9) = cantidad  'Cantidad 'PROYEC 31850 FASE IV 
                            arrDetalle(10) = dblPrecio 'Valor
                            arrDetalle(11) = dblPreIGV 'SubTotal
                            arrDetalle(12) = "0" 'Descuento
                            arrDetalle(13) = "0" ' Descuento Adicional
                            arrDetalle(14) = "0" ' Porcentaje Descuento Adicional
                            arrDetalle(15) = arrDetalle(10) 'Valor
                            arrDetalle(16) = dblPreIGV - dblPrecio

                            arrDetalle(18) = "000"  'Plan tarif
                            arrDetalle(19) = "NO APLICA"  ' desc Plan Tarif
                            arrDetalle(20) = "" ' Centro de costo
                            arrDetalle(22) = "A10130011"  ' Grupo de Art
                            arrDetalle(25) = txtNTelf.Text  ' Telefono

                            If Len(strDetalle) > 0 Then
                                strDetalle = strDetalle & "|" & Join(arrDetalle, ";")
                            Else
                                strDetalle = Join(arrDetalle, ";")
                            End If



                            arrCabecera(9) = dblPrecio
                            arrCabecera(10) = dblPreIGV - dblPrecio
                            arrCabecera(11) = dblPreIGV
                            arrCabecera(39) = ""  'TG para franquicias

                            arrCabecera(48) = "" 'dsOficina.Tables(0).Rows(0).Item("VKORG")
                            arrCabecera(49) = "" 'dsOficina.Tables(0).Rows(0).Item("VTWEG")

                            strCabecera = Join(arrCabecera, ";")

                            arrCabecera(9) = dblPrecio
                            arrCabecera(10) = dblPreIGV - dblPrecio
                            arrCabecera(11) = dblPreIGV
                            arrCabecera(39) = ""  'TG para franquicias

                            arrCabecera(48) = "" 'dsOficina.Tables(0).Rows(0).Item("VKORG")
                            arrCabecera(49) = "" 'dsOficina.Tables(0).Rows(0).Item("VTWEG")
                            strCabecera = Join(arrCabecera, ";")

                            'AUDITORIA
                            Detalle(1, 1) = "DocId"
                            Detalle(1, 2) = txtNumDocumento.Text
                            Detalle(1, 3) = "Doc Identidad"

                            Detalle(2, 1) = "Vendedor"
                            Detalle(2, 2) = Left(cboSelectVend.SelectedValue, 10)
                            Detalle(2, 3) = "Vendedor"

                            Detalle(3, 1) = "CodArt"
                            Detalle(3, 2) = arrDetalle(2)
                            Detalle(3, 3) = "Cod Articulo"

                            Detalle(4, 1) = "Cantidad"
                            Detalle(4, 2) = arrDetalle(9)
                            Detalle(4, 3) = "Cantidad"

                            Detalle(5, 1) = "Telefono"
                            Detalle(5, 2) = arrDetalle(25)
                            Detalle(5, 3) = "Telefono"

                            'FIN AUDITORIA

                            'dsResultado = objVentas.Set_CreaPedidoA(strCabecera, strDetalle, "", "", arrCabecera, strEntrega, strFactura, strNroContrato, strNroDocCliente, strNroPedido, strRefHistorico, strTipDocCliente, dblValorDescuento) ''TODO: CALLBACK SAP
                            ''TODO: CAMBIADO POR JYMMY TORRES
                            'dsResultado = objOffline.CreaPedidoFactura(strCabecera, strDetalle, "", "", arrCabecera, strEntrega, strFactura, strNroContrato, strNroDocCliente, strNroPedido, strRefHistorico, strTipDocCliente, dblValorDescuento)


                            'Try

                            '    'If (Not IsNothing(Session("numeroOperacionSAP"))) Then
                            '    '    Response.Write("<script>alert('No puede realizar esta operacion por recargas pendientes')</script>")
                            '    '    Return
                            '    'End If


                            '    '****linea normal
                            '    'numeroOperacionSAP = objOffline.CreaPedidoFactura("", "", crearTramaCabeceraPedido, crearTramaDetallePedido, crearTramaPago)
                            'Catch ex As Exception
                            '    blnError = True
                            '    wParam5 = 0
                            '    wParam6 = "Error al crear el pedido. Recarga Virtual Frecuente"
                            '    Response.Write("<script>alert('" & wParam6 & " ')</script>")
                            'End Try

                            ''CAMBIADO HASTA AQUI

                            'AUDITORIA
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicio Detalle AUDITORIA")
                            Detalle(1, 1) = "DocId"
                            Detalle(1, 2) = txtNumDocumento.Text
                            Detalle(1, 3) = "Doc Identidad"

                            Detalle(2, 1) = "Vendedor"
                            Detalle(2, 2) = Left(cboSelectVend.SelectedValue, 10)
                            Detalle(2, 3) = "Vendedor"

                            Detalle(3, 1) = "CodArt"
                            Detalle(3, 2) = arrDetalle(2)
                            Detalle(3, 3) = "Cod Articulo"

                            Detalle(4, 1) = "Cantidad"
                            Detalle(4, 2) = arrDetalle(9)
                            Detalle(4, 3) = "Cantidad"

                            Detalle(5, 1) = "Telefono"
                            Detalle(5, 2) = arrDetalle(25)
                            Detalle(5, 3) = "Telefono"
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Fin Detalle AUDITORIA")

                            'FIN AUDITORIA

                            'For i = 0 To dsResultado.Tables(1).Rows.Count - 1
                            '    If dsResultado.Tables(1).Rows(i).Item(0) = "E" Then
                            '        blnError = True
                            '        wParam5 = 0
                            '        wParam6 = "Error al crear el pedido. Recarga Virtual Frecuente"
                            '        Response.Write("<script>alert('" & dsResultado.Tables(1).Rows(i).Item(3) & " ')</script>")
                            '    End If
                            'Next

                            If Not blnError Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "blnError, antes de registrar el punto venta")
                                '========================================================================================='
                                '****** No va ser incluido ************'
                                'Comentado en bloque: 

                                '****REGISTRAR CABECERA DEL PEDIDO 6.0 *******'
                                'intOper = objCajas.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), _
                                'ConfigurationSettings.AppSettings("CodAplicacion"), Session("USUARIO"), _
                                'cboTipDocumento.SelectedValue, _
                                'txtNumDocumento.Text, strTipDocVta, strFactura, "", _
                                'dblMonto, dblIGV, (dblMonto + dblIGV), "V")                                              ''TODO: CALLBACK ORACLE

                                '*************************************************************** 
                                '***PARÀMETROS ESTABLECIDOS PARA LAS RECARGAS VIRTUALES:
                                '*************************************************************** 

                                'INICIO|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
                                Dim strNumeroDocumento As String

                                If (txtNTelf.Text.Trim.Substring(0, intLongNroOLO) = strInicioNroOLO) Then
                                    strNumeroDocumento = Funciones.CheckStr(Session("NumeroDocumentoOLO"))
                                Else
                                    strNumeroDocumento = Funciones.CheckStr(txtNumDocumento.Text)
                                End If
                                'FIN|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO


                                '** PASO 1: consultamos los datos de la oficina para obtener el interlocutor
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : INTEV_CODINTERLOCUTOR")
                                Dim INTEV_CODINTERLOCUTOR As String = objMsSAP.ConsultaPuntoVenta(Session("ALMACEN")).Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : " & INTEV_CODINTERLOCUTOR)


                                '** Paso 2: Consultamos el OFICC_ORGVENTA
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : OFICC_ORGVENTA")
                                Dim OFICC_ORGVENTA As String = objMsSAP.ConsultaOfina(INTEV_CODINTERLOCUTOR, "").Tables(0).Rows(0).Item("OFICC_ORGVENTA")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Consultamos el : " & OFICC_ORGVENTA)

                                '** Paso 3: Registramos el Pedido
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Inicia el Registro del Pedido - Recarga Virtual")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Inicia RegistrarPedido")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Parametros:")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param:" & Funciones.CheckStr(INTEV_CODINTERLOCUTOR))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param:" & "")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param:" & Funciones.CheckStr(OFICC_ORGVENTA))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: PEDIC_CANALVENTA " & ConfigurationSettings.AppSettings("PEDIC_CANALVENTA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: PEDIC_SECTOR " & ConfigurationSettings.AppSettings("PEDIC_SECTOR"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("TIPO_VENTA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_DESCCLASEBOLETA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_RVF"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_CODTIPOOPERACION"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIV_DESCTIPOOPERACION"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & Funciones.CheckDate(dateTimeValue).ToString)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("constAplicacion"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_ESTADO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("ISRENTA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIDO_ALTA"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIV_UBIGEO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("ESQUEMACALCULO_RV"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_TIPODOCCLIENTE"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PEDIC_TIPOCLIENTE"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("DISTRITO_CLIENTE_GENERICO"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("CODDPTO_CLIENTE"))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: " & ConfigurationSettings.AppSettings("PAIS_CLIENTE_GENERICO"))
                                'INICIO|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param: NUMERO DE DOCUMENTO: " & Funciones.CheckStr(strNumeroDocumento))
                                'FIN|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO


                                 'Funciones.CheckStr(strNumeroDocumento), _  INCIDENCIA RECARGA POR NUMERO DOCUMENTO

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/	
                                ''llamamos a la funcion que inserta el detalle de la venat en PVU
                                Dim cadenaCabecera As String = String.Join(";", arrCabecera)
                                Dim cadenaDetalle As String = String.Join(";", arrDetalle)

                                NroDocumentoVentaPVU = RegistrarHistoricoVenta(cadenaCabecera, cadenaDetalle)

                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                objAct.RegistrarPedido(INTEV_CODINTERLOCUTOR, "", _
                                                            ConfigurationSettings.AppSettings("PEDIC_TIPODOCUMENTO"), _
                                                            OFICC_ORGVENTA, _
                                                            ConfigurationSettings.AppSettings("PEDIC_CANALVENTA"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_SECTOR"), _
                                                            ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                                                            Funciones.CheckDate(dateTimeValue), _
                                                            ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO_RV"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_DESCCLASEBOLETA"), _
                                                            "", _
                                                            ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_RVF"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_CODTIPOOPERACION"), _
                                                            ConfigurationSettings.AppSettings("PEDIV_DESCTIPOOPERACION"), _
                                                            Funciones.CheckDate(dateTimeValue), _
                                                            ConfigurationSettings.AppSettings("constAplicacion"), _
                                                            Session("USUARIO"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_ESTADO"), _
                                                            ConfigurationSettings.AppSettings("ISRENTA"), _
                                                            ConfigurationSettings.AppSettings("PEDIDO_ALTA"), _
                                                            ConfigurationSettings.AppSettings("PEDIV_UBIGEO"), _
                                                            ConfigurationSettings.AppSettings("ESQUEMACALCULO_RV"), _
                                                            ConfigurationSettings.AppSettings("PEDIC_TIPODOCCLIENTE"), _
                                                            Funciones.CheckStr(strNumeroDocumento), _
                                                            ConfigurationSettings.AppSettings("PEDIC_TIPOCLIENTE"), _
                                                            "", "", "", _
                                                            dateTimeValue, "", "", _
                                                            Funciones.CheckStr(txtNTelf.Text), _
                                                            "", "-", "0", _
                                                            ConfigurationSettings.AppSettings("DISTRITO_CLIENTE_GENERICO"), _
                                                            System.Configuration.ConfigurationSettings.AppSettings("CODDPTO_CLIENTE"), _
                                                            ConfigurationSettings.AppSettings("PAIS_CLIENTE_GENERICO"), "", "", _
                                                            "", "", "", "01", "", Session("USUARIO"), dateTimeValue, _
                                                            Session("USUARIO"), dateTimeValue, "0", _
                                                            K_PEDIN_NROPEDIDO, K_NROLOG, K_DESLOG)

                                If K_PEDIN_NROPEDIDO <> 0 And K_DESLOG = "OK" Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "La cabecera del pedido de RV fue registrado correctamente  =>" & K_PEDIN_NROPEDIDO)
                                    '*** Paso 4: Registramos el detalle para el # peridod insertado.
                                    K_NROLOG = ""
                                    K_DESLOG = ""
                                    Dim strIdentificador As String = Funciones.CheckStr(K_PEDIN_NROPEDIDO)
                                    Try

                                        '**** REGISTRAMOS EL DETALLE DEL PEDIDO ***************************************************************************'
                                        '**** CK: Verificar los montos enviados en la recarga virtual: ***'
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Inicio del registro del Detalle de la RV - Recarga Virtual")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Metodo: RegistraDetallePedido")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Nro Pedido: " & K_PEDIN_NROPEDIDO)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN PDV: " & INTEV_CODINTERLOCUTOR)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cod Interlocutor: " & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cod Serie: " & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cod Material: " & Funciones.CheckStr(arrDetalle(2)))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Desc Material: " & Funciones.CheckStr(arrDetalle(3)))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cantidad: 1 ")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Precio: " & dblPrecio)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Telefono: " & txtNTelf.Text)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Clarify: " & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Nro Renta: 0")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Total Renta: 0")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cuotas: 0")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Cod LP: " & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Desc LP: " & "")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Usuario Crea: " & "00000" & Funciones.CheckStr(Session("USUARIO")))
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     IN Fecha: " & Funciones.CheckStr(dateTimeValue))
                                        'PROY-31850 FASE IV - INI
                                        Dim strIdMaterialOLO = String.Empty
                                        Dim strDescMaterialOLO = String.Empty
                                        Dim strPlanIdOLO = String.Empty
                                        Dim strCodRedknee = String.Empty
                                        If (txtNTelf.Text.Substring(0, intLongNroOLO) = strInicioNroOLO) Then
                                            strIdMaterialOLO = ConfigurationSettings.AppSettings("codMaterial_OLO")
                                            strDescMaterialOLO = ConfigurationSettings.AppSettings("descMaterial_OLO")
                                            Dim codigos_Prec_CodRecargas() As String
                                            codigos_Prec_CodRecargas = Split(cboImporte.SelectedValue, ";")
                                            strPlanIdOLO = codigos_Prec_CodRecargas(1)
                                            strCodRedknee = codigos_Prec_CodRecargas(2)
                                        Else
                                            strIdMaterialOLO = Funciones.CheckStr(arrDetalle(2))
                                            strDescMaterialOLO = Funciones.CheckStr(arrDetalle(3))
                                        End If

                                        objAct.RegistraDetallePedido(K_PEDIN_NROPEDIDO, INTEV_CODINTERLOCUTOR, "", _
                                    "", strIdMaterialOLO, strDescMaterialOLO, 1, dblPrecio, txtNTelf.Text, "", 0, 0, 0, strPlanIdOLO, strCodRedknee, "00000" & Funciones.CheckStr(Session("USUARIO")), _
                                        dateTimeValue, "00000" & Funciones.CheckStr(Session("USUARIO")), dateTimeValue, K_NROLOG, K_DESLOG)


                                        'PROY-31850 - FASE IV  FIN
                                        '********************************************'**********************************************************************'
                                    Catch ex As Exception
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Error al registrar el detalle del pedido => " & K_PEDIN_NROPEDIDO)
                                        Response.Write("<script>alert('Ocurrio un error al registrar el detalle del pedido. Comuniquelo a su centro de Soporte')</script>")
                                        Exit Sub
                                    End Try

                                    If K_DESLOG <> "OK" Then
                                        Response.Write("<script>alert('Ocurrio un error al registrar el detalle del pedido. Comuniquelo a su centro de Soporte')</script>")
                                        Exit Sub
                                    End If
                                    '************************* Inicio Actualiza el Ajuste de redondeo***********************

                                    Try
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Inicio Actualiza el Ajuste de redondeo despues de guardar el detalle de la RV - Recarga Virtual")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -     Metodo: ActualizarAjusteRedondeo - SP: SSAPSU_ACTUALIZARPEDIDO")
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " -        IN Nro Pedido : " & Funciones.CheckStr(K_PEDIN_NROPEDIDO))

                                        objAct.ActualizarAjusteRedondeo(K_PEDIN_NROPEDIDO)

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - FIN Actualiza el Ajuste de redondeo despues de guardar el detalle de la RV - Recarga Virtual")

                                    Catch ex As Exception
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & strIdentificador & " - Error al Actualiza el Ajuste del redondeo en el detalle del pedido de la RV => " & K_PEDIN_NROPEDIDO)
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "  Err." & ex.Message.ToString)
                                    End Try

                                    '************************* Fin Actualiza el Ajuste de redondeo***********************

                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/			
                                    'INICIO REGISTRO EN LA TABLA SISACT_INFO_VENTA_SAP 
                                    Try
                                        If NroDocumentoVentaPVU > 0 Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -   INICIO GRABAR VENTA EN SISACT - SISACT_INFO_VENTA_SAP")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -   IN:[ID VENTA: " & _
                                                                    Funciones.CheckInt64(NroDocumentoVentaPVU) & "][NRO DOC: " & _
                                                                    Funciones.CheckStr(K_PEDIN_NROPEDIDO) & "][TIPO VENTA: " & _
                                                                    ConfigurationSettings.AppSettings("TIPO_DOC_INFO_VENTA") & "][MONTO: " & _
                                                                    Funciones.CheckDbl(dblPrecio) & "]")

                                            objTrsPvu.GrabarInfoVentaSap("", _
                                                                         "", _
                                                                         Funciones.CheckInt64(NroDocumentoVentaPVU), _
                                                                         Funciones.CheckStr(K_PEDIN_NROPEDIDO), _
                                                                         ConfigurationSettings.AppSettings("TIPO_DOC_INFO_VENTA"), _
                                                                         Funciones.CheckDbl(dblPrecio))

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -   FIN GRABAR VENTA EN SISACT - SISACT_INFO_VENTA_SAP")
                                        Else
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "No se pudo registro la venta en la tabla SISACT_INFO_VENTA_SAP")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Razon: No se registro la venta en la tabla SISACT_AP_VENTA")
                                        End If
                                    Catch ex As Exception
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Error en el registro de venta: trabla SISACT_INFO_VENTA_SAP")
                                    End Try
                                    'FIN REGISTRO EN LA TABLA SISACT_INFO_VENTA_SAP 

                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    'REGISTRO DE RECARGA VIRTUAL en SICAT_RECARGA_VIRTUAL_TRX. mediante el WS: BSS_RECARGAVIRTUAL.CREARRECARGA.

                                    If NroDocumentoVentaPVU > 0 Then
                                        'Iniciamos lel registro de la recarga virtual en la tabla  SICAT_RECARGA_VIRTUAL_TRX. mediante el WS: BSS_RECARGAVIRTUAL.CREARRECARGA.
                                        Dim strCanal As String = Session("CANAL")
                                        Dim strUserSesion As String = Session("USUARIO")
                                        Dim estadoRegistrarRecargaVirtual As Boolean

                                        'Invocamos al WS para el registro de la Recarga Virtual
                                        estadoRegistrarRecargaVirtual = RegistrarRecargaVirtual(strIdentifyLog, strCanal, strUserSesion, arrCabecera, arrDetalle)

                                        If estadoRegistrarRecargaVirtual = True Then
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " Se registro la Recarga Virtual correctamente")
                                        Else
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " Ocurrio un ERROR en el registro de la Recarga Virtual")
                                        End If
                                    End If
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/			

                                Else
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Error al registrar la cabecera del pedido - Recarga Virtual")
                                    Response.Write("<script>alert('Ocurrio un error al registrar el pedido, favor de volver a intentar.')</script>")
                                    Exit Sub
                                End If

                                '*************************************************************************************************************************************************************************'
                                'objCajas.FP_Det_Oper(intOper, 1, strServicio, "", txtNTelf.Text, cboImporte.SelectedValue, dblPrecio, dblPreIGV - dblPrecio, dblPreIGV)          ''TODO: CALLBACK ORACLE
                                '*** se comento la linea, debido a que ahora se envia el NroPedido del Mòdulo, y las tramas las generamos individuales
                                'numeroOperacionSAP = objOffline.CreaPedidoFactura("", "", crearTramaCabeceraPedido, crearTramaDetallePedido, crearTramaPago)
                                Try
                                    crearTramaCabeceraPedido()
                                    crearTramaDetallePedido()
                                    crearTramaPago()
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "Error en la creacciòn de las Tramas: crearTramaCabeceraPedido - crearTramaDetallePedido - crearTramaPago")
                                    Exit Sub
                                End Try
                                '*************************************************************************************************************************************************************************'

                                ''dsResultado = objVentas.Set_ActualizaEstadoPedido(strFactura, Session("ALMACEN"), "", "S") ''TODO: CALLBACK SAP
                                '''TODO: CAMBIADO POR JYMMY TORRES
                                'dsResultado = objOffline.ActualizaEstadoPedido(strFactura, Session("ALMACEN"), "", "S")
                                '''CAMBIADO HASTA AQUI

                                'For i = 0 To dsResultado.Tables(0).Rows.Count - 1
                                '    If dsResultado.Tables(0).Rows(i).Item(0) = "E" Then
                                '        blnError = True
                                '        wParam5 = 0
                                '        wParam6 = "Error en actualizacion de estado pedido"
                                '        Response.Write("<script>alert('" & dsResultado.Tables(0).Rows(i).Item(3) & " ')</script>")
                                '    End If
                                'Next

                                If Not blnError Then
                                    ''dsResultado = objVentas.Set_ActualizaEstadoPedido(strFactura, Session("ALMACEN"), "S", "") ''TODO CALLBACK SAP
                                    '''TODO: CAMBIADO POR JYMMY TORRES
                                    'dsResultado = objOffline.ActualizaEstadoPedido(strFactura, Session("ALMACEN"), "", "S")
                                    '''CAMBIADO HASTA AQUI
                                    'For i = 0 To dsResultado.Tables(0).Rows.Count - 1
                                    '    If dsResultado.Tables(0).Rows(i).Item(0) = "E" Then
                                    '        blnError = True
                                    '        wParam5 = 0
                                    '        wParam6 = "Error en actualizacion de estado pedido"
                                    '        Response.Write("<script>alert('" & dsResultado.Tables(0).Rows(i).Item(3) & " ')</script>")
                                    '    End If
                                    'Next
                                    '****** No va ser incluido ************'
                                    '=====================================================================================================================
                                    'wParam???
                                    objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                                    If Not blnError Then   'El pedido se ha creado sin problemas
                                        ''TODO: I Pendientes por Pagar         / Impresion de Documentos
                                        'dsResultado = objPagos.Get_ConsultaPoolFactura(Session("ALMACEN"), txtFechaPrecioVenta.Text, "I", "", txtNumDocumento.Text, cboTipDocumento.SelectedValue, "", "1") ''TODO: CALLBACK SAP
                                        ''TODO: CAMBIADO POR JYMMY TORRES
                                        'dsResultado = objOffline.ConsultarPoolFacturas(Session("ALMACEN"), txtFechaPrecioVenta.Text, "I", "", txtNumDocumento.Text, cboTipDocumento.SelectedValue, "", "1")
                                        ''CAMBIADO HASTA AQUI
                                        'dvFiltro.Table = dsResultado.Tables(0)
                                        'dvFiltro.RowFilter = "VBELN=" & strFactura
                                        drFila = tbFacturacion.Rows(0)
                                        Session("DocSel") = drFila
                                        Session("VentaR") = "1"
                                        'Session("numeroTelefono") = txtNTelf.Text
                                        'Session("numeroOperacionSAP") = numeroOperacionSAP
                                        Session("recargaVirtual") = True
                                        'Response.Redirect("detaPago.aspx?pDocSap=" & numeroOperacionSAP & "&numeroTelefono=" & txtNTelf.Text & "&montoRecarga=" & drFila("TOTAL"))
                                        'INICIO|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
                                        Response.Redirect("detaPago.aspx?pDocSap=" & K_PEDIN_NROPEDIDO & "&numeroTelefono=" & txtNTelf.Text & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO") & "&numerodocumento=" & strNumeroDocumento)
                                        'FIN|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
                                    End If
                                End If

                            End If
                        End If
                    End If
                End If
            End If
            'Aqui llega solo si no paso por las validaciones anteriores.
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            'Else
            '    Session("RecFrec") = "1"
            '    Response.Redirect("datosCliente.aspx?strTipDoc=" & cboTipDocumento.SelectedValue & "&strNumDoc=" & txtNumDocumento.Text)
        End If
    End Sub  '*** Fin del grabar pedido recarga virtual ****'

    Private Sub VerificaSaldoTienda()
        Dim strSaldo As String

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia Mètodo : IngresaVentaRecargaRV :" & Funciones.CheckStr(txtNTelf.Text))
        IngresaVentaRecargaRV(txtNTelf.Text)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Mètodo : IngresaVentaRecargaRV ")

        'If Not blnError Then
            '    strSaldo = objVentas.Get_ConsultaSaldoRecarga(Session("ALMACEN")) ''TODO: CALLBACK SAP
            '    If CDbl(cboImporte.SelectedValue) > CDbl(strSaldo) Then
            '        Response.Write("<script>alert('Error: Se debe configurar el codigo de tienda para realizar recargas.')</script>")
            '    End If
        'End If
    End Sub

    Private Sub IngresaVentaRecargaRV(ByVal strNumTelefono As String)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Ingresa en el mètodo IngresaVentaRecargaRV")

        Dim pvTerminalID As String
        Dim pvTrace As String
        Dim pvCanal As String
        Dim pvTelefono As String
        Dim pvBinAdquiriente As String
        Dim pvCodCadena As String
        Dim pvCodComercio As String
        Dim pvMonto As String
        Dim pvMoneda As String
        Dim pvProducto As String

        Dim strVendedor As String
        Dim strTrama As String
        Dim arrTrama() As String
        Dim strMensaje As String

        Dim objRVirt As New COM_SIC_RVirtual.clsRVirtual
        'inicio jtn
        Dim vendedor As String = cboSelectVend.SelectedItem.Text
        If vendedor = "" Then
            blnError = True
            Return
        End If
        'fin jtn
        'strVendedor = objVentas.Get_ConsultaVendedorRecarga(Session("ALMACEN"), Session("CANAL")) 'TODO: INCLUIR BACHERO?
        If Len(Session("USUARIO")) = 0 Then
            Response.Write("<script>alert('Error: Se debe configurar el codigo de tienda para realizar recargas.')</script>")
            blnError = True
        Else
            If txtNTelf.Text = "" Then
                Response.Write("<script>alert('Error: Debe ingresar numero telefono para realizar recargas.')</script>")
                blnError = True
                Exit Sub
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Asignaciòn de los valores a la tram")
            'pvBinAdquiriente = ConfigurationSettings.AppSettings("cteCODIGO_BINADQUIRIENTE") 'Mid(strVendedor, 4, 6)
            pvBinAdquiriente = Session("ALMACEN")
            pvCodCadena = Mid(strVendedor, 4, 7)
            pvCodComercio = Session("ALMACEN")

            pvTerminalID = "PVU"
            'pvTrace = "000002"
            pvTrace = Right("00" & Now().Second, 2) + Right("0000" & CDbl(Session("USUARIO")) * Now().Millisecond(), 4)
            pvCanal = "91"
            pvTelefono = strNumTelefono

            pvMonto = Request.Item("txtCant")
            pvMoneda = "604"
            pvProducto = "1"

            'verificar el resultado de objRVirt.Consulta modificado JCR
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Ejecutar la consulta: objRVirt.Consulta")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parámetros: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám1 : " & Funciones.CheckStr(Session("ALMACEN")).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám2 : " & Funciones.CheckStr(pvTerminalID).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám3 : " & Funciones.CheckStr(pvTrace).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám4 : " & Funciones.CheckStr(pvCanal).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám5 : " & Funciones.CheckStr(pvTelefono).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám6 : " & Funciones.CheckStr(pvBinAdquiriente).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám7 : " & Funciones.CheckStr(pvCodCadena).ToString.Trim)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Parám8 : " & Funciones.CheckStr(pvCodComercio).ToString.Trim)
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
            strTrama = objRVirt.Consulta(Session("ALMACEN"), pvTerminalID, pvTrace, pvCanal, pvTelefono, pvBinAdquiriente, pvCodCadena, pvCodComercio)
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin objRVirt.Consulta")

            ''TODO: AQUI LA RECARGA VIRTUAL

            arrTrama = Split(strTrama, ";")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Verifica la trama arrTrama(2):" & arrTrama(2).ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "El valor 30, significa error")
            Dim codError% = Convert.ToInt32(arrTrama(2))

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Verifica la trama arrTrama(1): " & arrTrama(1).ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Verifica la trama arrTrama(1)<>3")
            If CDbl(Trim(arrTrama(1))) <> 3 Then
                If codError = 30 Then
                    strMensaje = ConfigurationSettings.AppSettings("validacionTelefono")
                Else
                    strMensaje = "Error: No se puede realizar el servicio de recarga virtual. Motivo: " & arrTrama(3)
                End If

                If Trim(arrTrama(3)) = "" Then
                    strMensaje = "NO SE PUEDEN REALIZAR OPERACIONES DE RECARGA VIRTUAL EN ESTE MOMENTO"
                End If
                Response.Write("<script>alert('" & strMensaje & " ')</script>")
                blnError = True
            End If

        End If
    End Sub

    Private Function BuscaCliente() As Boolean
        Dim dsResultado As DataSet
        Dim i As Integer

        Dim arrCliente(64) As String

        'dsResultado = objPagos.Get_ConsultaCliente(Session("ALMACEN"), cboTipDocumento.SelectedValue, txtNumDocumento.Text) ''TODO CALLBACK SAP
        ''TODO: CAMBIADO POR JYMMY TORRES
        dsResultado = (New COM_SIC_OffLine.clsOffline).GetConsultaCliente(Session("ALMACEN"), cboTipDocumento.SelectedValue, txtNumDocumento.Text)
        '' CAMBIADO HAST AQUI

        If Not IsNothing(dsResultado) Then
            If dsResultado.Tables(0).Rows.Count = 0 Then
                Session("MenCliente") = "Cliente no encontrado."
                BuscaCliente = False
                Exit Function
            End If

            For i = 0 To dsResultado.Tables(0).Columns.Count - 1
                arrCliente(i) = dsResultado.Tables(0).Rows(0).Item(i)
            Next
            arrCliente(7) = Right(arrCliente(7), 2) & "/" & Mid(arrCliente(7), 5, 2) & "/" & Left(arrCliente(7), 4)
            'dsResultado = objVentas.Set_ActualizaCreaCliente(Session("ALMACEN"), arrCliente) ''TODO: CALLBACK SAP
            ''TODO: CAMBIADO POR JYMMY TORRES
            dsResultado = (New COM_SIC_OffLine.clsOffline).SetActualizaCreaClienteSap(Session("ALMACEN"), arrCliente)
            ''CAMBIADO HASTA AQUI

            For i = 0 To dsResultado.Tables(1).Rows.Count - 1
                If dsResultado.Tables(1).Rows(i).Item("TYPE") = "E" Then
                    Session("MenCliente") = dsResultado.Tables(1).Rows(i).Item("MESSAGE")
                    BuscaCliente = False
                    Exit Function
                End If
            Next
        Else
            Session("MenCliente") = "Error de Conexion(SAP) Consultando Usuario"
            BuscaCliente = False
            Exit Function
        End If

        BuscaCliente = True
    End Function

    Private Sub cargaRecarga()
        Dim objConfig As New COM_SIC_Configura.clsConfigura
        Dim dsRecarga As New DataSet
        Try
            dsRecarga = objConfig.FP_Consulta_RecargaVirtual("", "", "0")
            cboImporte.DataSource = dsRecarga.Tables(0)
            cboImporte.DataTextField = "REVIN_DESCRIP_RECARGA"
            cboImporte.DataValueField = "REVIN_VALOR_RECARGA"
            cboImporte.DataBind()
        Catch ex As Exception
            Response.Write("<script>alert('Error cargando los montos de Recargas.')</script>")
        End Try

    End Sub

    Private Sub btnGrabarRec_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabarRec.ServerClick

    End Sub
    '' TODO: CREADO POR JYMMY TORRES
    Function crearTramaCabeceraPedido() As String
        Try
            Dim arrayCabecera(14) As String
            'Dim strServicio, strDesServicio, moneda, canal As String
            Dim moneda, canal As String
            Dim codUsuario As String = cboSelectVend.SelectedValue.Split(CChar("#"))(0)  'Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))


            'strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
            'strDescServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")

            'Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(";"))
            canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
            Dim VTWEG, VKORG As String
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            ' Obtener Parametros Generales del Punto de Venta
            '''CAMBIADO POR JYMMY TORRES
            Dim dsOficina As DataSet = objOffline.ParametrosVenta(Session("ALMACEN"))
            If dsOficina.Tables.Count > 0 AndAlso dsOficina.Tables(0).Rows.Count > 0 Then
                VKORG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VKORG"))
                VTWEG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VTWEG"))
            Else
                VKORG = ""
                VTWEG = ""
            End If

            arrayCabecera(0) = ConfigurationSettings.AppSettings("constAUART").ToString '-->CLASE DOCUMENTO DE VENTAS v ZPBR
            arrayCabecera(1) = VKORG '-->ORGANIZACION DE VENTAS v
            arrayCabecera(2) = VTWEG '-->CANAL v
            arrayCabecera(3) = "10" '-->SECTOR
            arrayCabecera(4) = ConfigurationSettings.AppSettings("TIPO_DOC_COD_CLTE_VARIOS") '-->TIPO NUMERO IDENTIFICACION FISCAL (DNI)
            arrayCabecera(5) = txtNumDocumento.Text '-->CODIGO CLIENTE SCI ?
            arrayCabecera(6) = txtFechaPrecioVenta.Text '-->FECHA DOCUMENTO
            arrayCabecera(7) = Session("ALMACEN") '-->OFICINA DE VENTA
            arrayCabecera(8) = "0000000000000000" '-->NUMERO DE REFERENCIA SUNAT
            arrayCabecera(9) = codUsuario '-->NUMERO CLIENTE
            arrayCabecera(10) = "PEN" '-->MONEDA
            arrayCabecera(11) = "02" '-->TIPO VENTA
            arrayCabecera(12) = "01" '-->CLASE VENTA
            arrayCabecera(13) = "CLIENTE GENERICO"
            arrayCabecera(14) = ""


            '+++PEDIC_TIPOVENTA+++'=>'02' PREPAGO
            '***************************************************************************************'
            '** CREAMOS LA TABLA **'
            tbFacturacion = New DataTable

            'tbFacturacion.Columns.Add("FKART", GetType(String))
            tbFacturacion.Columns.Add("PEDIC_CLASEFACTURA", GetType(String))

            tbFacturacion.Columns.Add("FKDAT", GetType(String))

            'tbFacturacion.Columns.Add("VBELN", GetType(String))
            tbFacturacion.Columns.Add("PEDIN_NROPEDIDO", GetType(String))

            'tbFacturacion.Columns.Add("NAME1", GetType(String))
            tbFacturacion.Columns.Add("PEDIV_NOMBRECLIENTE", GetType(String))

            'tbFacturacion.Columns.Add("TOTAL", GetType(Double))
            tbFacturacion.Columns.Add("INPAN_TOTALDOCUMENTO", GetType(Double))


            'tbFacturacion.Columns.Add("INICIAL", GetType(Int32))
            tbFacturacion.Columns.Add("PAGON_INICIAL", GetType(Int32))


            'tbFacturacion.Columns.Add("PAGOS", GetType(Int32))
            tbFacturacion.Columns.Add("PEDIN_PAGO", GetType(Int32))

            'tbFacturacion.Columns.Add("SALDO", GetType(Int32))
            tbFacturacion.Columns.Add("PEDIN_SALDO", GetType(Int32))

            tbFacturacion.Columns.Add("PEDIDO", GetType(String))
            tbFacturacion.Columns.Add("XBLNR", GetType(String))
            tbFacturacion.Columns.Add("RECIBE_PAGO", GetType(String))
            tbFacturacion.Columns.Add("NRO_DEP_GARANTIA", GetType(String))
            tbFacturacion.Columns.Add("ES_VTA_BUSINESS", GetType(String))
            tbFacturacion.Columns.Add("NRO_CONTRATO", GetType(String))

            tbFacturacion.Columns.Add("PEDIC_TIPOVENTA", GetType(String))

            tbFacturacion.Columns.Add("PAGOC_CODSUNAT", GetType(String)) 'DANTE REVISAR

            '***************************************************************************************'


            '***************************************************************************************'
            '**CREAMOS LA FILA PARA AGREGAR LOS DATOS : **'
            '***************************************************************************************'
            Dim newRow As DataRow = tbFacturacion.NewRow()
            'newRow("FKART") = ConfigurationSettings.AppSettings("constClaseVoleta").ToString
            newRow("PEDIC_CLASEFACTURA") = ConfigurationSettings.AppSettings("constClaseVoleta").ToString

            newRow("FKDAT") = txtFechaPrecioVenta.Text

            'newRow("VBELN") = K_PEDIN_NROPEDIDO
            newRow("PEDIN_NROPEDIDO") = K_PEDIN_NROPEDIDO

            'newRow("NAME1") = "CLIENTE GENERICO"
            newRow("PEDIV_NOMBRECLIENTE") = "CLIENTE GENERICO"

            'newRow("TOTAL") = Convert.ToDouble(cboImporte.SelectedValue)
            'PROYEC 31850 FASE IV INI
            Dim strNroTlfono = txtNTelf.Text.Trim()
            Dim codigos_Prec_CodRecarga() As String
            Dim precio As String
            codigos_Prec_CodRecarga = Split(cboImporte.SelectedValue, ";")
            If (strNroTlfono.ToString.StartsWith(strInicioNroOLO)) Then
                precio = codigos_Prec_CodRecarga(0)
            Else
                precio = cboImporte.SelectedValue
            End If
            'PROYEC 31850 FASE IV FIN
            newRow("INPAN_TOTALDOCUMENTO") = Convert.ToDouble(precio) 'PROYEC 31850 FASE IV FIN
            'newRow("INICIAL") = 0
            newRow("PAGON_INICIAL") = 0

            'newRow("PAGOS") = 0
            newRow("PEDIN_PAGO") = 0

            'newRow("SALDO") = Convert.ToDouble(cboImporte.SelectedValue)
            'PROYEC 31850 FASE IV INI
            If (strNroTlfono.ToString.StartsWith(strInicioNroOLO)) Then
                precio = codigos_Prec_CodRecarga(0)
            Else
                precio = cboImporte.SelectedValue
            End If
            'PROYEC 31850 FASE IV FIN

            newRow("PEDIN_SALDO") = Convert.ToDouble(precio) 'PROYEC 31850 FASE IV FIN
            newRow("PEDIC_TIPOVENTA") = "02" '*** para las recargas, el tipo de venta=02 consultado con ERIBERTO.

            newRow("PEDIDO") = ""
            newRow("XBLNR") = ""
            newRow("RECIBE_PAGO") = "X"
            newRow("NRO_DEP_GARANTIA") = "0000000000"
            newRow("PAGOC_CODSUNAT") = "0000000000000000" '"0000000000"
            newRow("ES_VTA_BUSINESS") = ""
            newRow("NRO_CONTRATO") = ""
            tbFacturacion.Rows.Add(newRow)
            Return Join(arrayCabecera, ";")
            '***************************************************************************************'

            '''CAMBIADO HASTA AQUI
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Function crearTramaDetallePedido() As String
        Try
            Dim arrayDetalle(14) As String

            'Dim strServicio, strDesServicio, moneda, canal As String
            Dim moneda, canal As String
            'strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
            'strDescServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")

            'Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(","))

            canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")


            'PROYEC 31850 FASE IV INI
            Dim strNroTlfolo As String
            Dim codigos_Prec_CodRecarga() As String
            Dim importeolo
            strNroTlfolo = txtNTelf.Text.Trim()
            codigos_Prec_CodRecarga = Split(cboImporte.SelectedValue, ";")
            If (strNroTlfolo.ToString.StartsWith(strInicioNroOLO)) Then
                Dim importe As String
                importeolo = codigos_Prec_CodRecarga(0)
            Else
                importeolo = cboImporte.SelectedValue
            End If
            'PROYEC 31850 FASE IV FIN

            'PROY-31766 INICIO'
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Inicio Obtener IGV - Met.crearTramaDetallePedido() - ObtenerIGVActual()")
            Dim valorIGV As Double = ObtenerIGVActual() 'PROY-31766'
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor IGV: " & valorIGV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Fin Obtener IGV - Met.crearTramaDetallePedido() - ObtenerIGVActual()")
            'PROY-31766 FIN'

            Dim recEfectiva# = CDbl(importeolo)  'PROYEC 31850 FASE IV INI
            Dim valVenta# = recEfectiva / (1 + valorIGV)'PROY-31766
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Calculando valor de la Venta: " & recEfectiva & "/ (1+" & valorIGV & ")") 'PROY-31766'
            valVenta = Math.Round(valVenta, 2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor de la Venta: " & valVenta) 'PROY-31766'

            arrayDetalle(0) = "10" '-->POSICION DOCUMENTO DE VENTAS
            arrayDetalle(1) = strServicio 'ConfigurationSettings.AppSettings("strCodArticuloRV") '-->NUMERO MATERIAL
            arrayDetalle(2) = recEfectiva '-->CANTIDAD PEDIDO ACUMULADA P_KWMENG
            arrayDetalle(3) = "1" '-->CAMPAÑA
            arrayDetalle(4) = "1" '-->PLAN TARIFARIO SCI
            arrayDetalle(5) = txtNTelf.Text '-->NUMERO TELEFONO
            arrayDetalle(6) = "01" '-->INDICADOR UTILIZACION
            arrayDetalle(7) = recEfectiva 'Session("codUsuario") '-->CODIGO VENDEDOR GENERICO RECARGAS VIRT
            arrayDetalle(8) = valVenta 'Math.Round(dblPrecio, 2) '-->VALOR DE LA VENTA
            arrayDetalle(9) = "0" '-->DESCUENTO 1
            arrayDetalle(10) = recEfectiva - valVenta 'Math.Round(dblPreIGV, 2) '-->IGV
            arrayDetalle(11) = recEfectiva 'Math.Round(dblPreIGV + dblPrecio, 2) '-->VALOR DE LA VENTA
            arrayDetalle(12) = "0" '-->NUMERO RECARGA VIRTUAL
            arrayDetalle(13) = strDescServicio 'ConfigurationSettings.AppSettings("strDesArticuloRV") '-->servicio de reca

            'tbFacturacion.Rows(0)("TOTAL") = dblPrecio

            Return Join(arrayDetalle, ";")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Function crearTramaPago() As String
        Dim arrayPago(13) As String
        Dim strServicio, strDesServicio, moneda, canal, VKORG, VTWEG As String
        Dim viasPagoSap As String() = ConfigurationSettings.AppSettings("constCodigoViasSap").Split(CChar(";"))
        'Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(","))

        'INICIO JYMMYT
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim dsOficina As DataSet = objOffline.ParametrosVenta(Session("ALMACEN"))
        If dsOficina.Tables.Count > 0 AndAlso dsOficina.Tables(0).Rows.Count > 0 Then
            VKORG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VKORG"))
            VTWEG = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("VTWEG"))
        Else
            VKORG = ""
            VTWEG = ""
        End If
        ''FIN JYMMYT

        Dim tipoCambio As Double = objOffline.Get_TipoCambio(txtFechaPrecioVenta.Text)


        canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")
        'arrayPago(0) = dsOficina.Tables(0).Rows(0).Item("VKORG") '-->ORGANIZACION DE VENTAS
        arrayPago(0) = "" '-->ORGANIZACION DE VENTAS
        arrayPago(1) = Session("ALMACEN") '-->AMBITO NO DEFINIDO
        arrayPago(2) = viasPagoSap(0) '-->ZEFE
        arrayPago(3) = "" '-->CAMPAA
        arrayPago(4) = "" '-->PLAN TARIFARIO SCI
        'PROYEC 31850 FASE IV INI
        Dim strNroTlfolo As String
        Dim codigos_Prec_CodRecarga() As String
        Dim importeolopago As String
        strNroTlfolo = txtNTelf.Text.Trim()
        codigos_Prec_CodRecarga = Split(cboImporte.SelectedValue, ";")
        If (strNroTlfolo.ToString.StartsWith(strInicioNroOLO)) Then
            importeolopago = codigos_Prec_CodRecarga(0)
        Else
            importeolopago = cboImporte.SelectedValue
        End If
        'PROYEC 31850 FASE IV FIN
        arrayPago(5) = importeolopago 'dblTotal '-->IMPORTE 'PROYEC 31850 FASE IV FIN
        arrayPago(6) = "PEN" '-->INDICADOR UTILIZACION
        arrayPago(7) = "0" '-->tipo  de cambio
        arrayPago(8) = "0000000000000000" '-->NUMERO REFERENCIA SUNAT
        arrayPago(9) = viasPagoSap(0) '-->DESCUENTO 1
        arrayPago(10) = DateTime.Today.ToString("dd/MM/yyyy")
        arrayPago(11) = "" '-->
        arrayPago(12) = "" '-->
        arrayPago(13) = "001" '-->PARAMETRO CONTADOR
        Return Join(arrayPago, ";")
    End Function
    '' CREADO HASTA AQUI


    Private Sub calculaMontos()
        ''Dim dblDescuento, dblPreIGV, dblPrecio, dblTotal As Double
        'PROY-31766 INICIO'
        objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Inicio Obtener IGV - Met.calculaMontos() - ObtenerIGVActual()")
        Dim valorIGV As Double = ObtenerIGVActual()
        objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Valor IGV:" & valorIGV)
        objFileLog.Log_WriteLog(pathFile, strArchivo, Session("USUARIO") & " - " & "Fin Obtener IGV - Met.calculaMontos() - ObtenerIGVActual()")
        'PROY-31766 FIN'
        Dim precioUnitarioRecarga As Double = Convert.ToDouble(ConfigurationSettings.AppSettings("precioUnitarioRecarga")) '0.84
        Dim subTotal#, total#, montoIgv#, x#
        'PROYEC 31850 FASE IV INICIO
        Dim strNroTlfo As String = ""
        strNroTlfo = txtNTelf.Text.Trim()
        Dim cantidadolo As String
        
        If (strNroTlfo.ToString.StartsWith(strInicioNroOLO)) Then
            Dim codigos_Prec_CodRecarga() As String
            codigos_Prec_CodRecarga = Split(cboImporte.SelectedValue, ";")
            cantidadolo = codigos_Prec_CodRecarga(0)
        Else
            cantidadolo = cboImporte.SelectedValue
        End If
        'PROYEC 31850 FASE IV FIN
        x = cantidadolo 'PROYEC 31850 FASE IV FIN
        subTotal = x * precioUnitarioRecarga
        montoIgv = subTotal * valorIGV
        total = subTotal + montoIgv

        dblDescuento = 0
        dblPreIGV = montoIgv
        dblPrecio = subTotal
        dblTotal = dblPreIGV + subTotal
    End Sub


    '******************************************************************************************'
    '***Función implementada para cambiar los còdigo de las recargas virtuales                 '
    '***EVERIS                                                                                 '
    '***" " : 30-12-14                                                                    '
    '******************************************************************************************'
    Public Function RetornaCodigoServicio(ByVal strServicio As String) As String
        Dim strRetorna As String = ""
        Dim objConf As New COM_SIC_Configura.clsConfigura
        Dim dsCodMaterialRecarga As DataSet
        If strServicio <> "" Then
            dsCodMaterialRecarga = objConf.ConsultaCodigoMaterialRecargaVirtual(strServicio)
            If dsCodMaterialRecarga.Tables(0).Rows.Count > 0 Then
                '*** ME RETORNA EL NUEVO CODIDO DEL MATERIAL CONFIGURADO EN EL TABLA: SINERGIA_MATERIALRECARGA
                strRetorna = dsCodMaterialRecarga.Tables(0).Rows(0).Item("VALORACTUAL")
                Return strRetorna
            End If
        End If
        Return ""
    End Function
    ' PROY-31850 FASE IV- INI
    Private Sub btnConsultaI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultaI.Click
        Dim Msj As String
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-----------------------------------------------------------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicia consulta de Planes para telefonos Olo y Claro. ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-----------------------------------------------------------------------------------------------------------------")
            If ConsultarImportesOLO() = False Then
                Response.Write("<script>alert('" & hdlMsj.Value & "')</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, hdlMsj.Value)
                Exit Sub
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-----------------------------------------------------------------------------------------------------------------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin consulta de Planes para telefonos Olo y Claro. ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-----------------------------------------------------------------------------------------------------------------")
        Catch ex As Exception

        End Try
    End Sub
    ' PROY-31850 FASE IV - FIN
    Private Function ConsultarImportesOLO() As Boolean
        Dim strNroTlf As String = ""
        Dim sw As Double = True
        'PROY-31850 FASE IV - INICIO

        Dim UserApli As String = Funciones.CheckStr(Session("USUARIO"))
        Dim idTrans As String = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim CodApli As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("CodAplicacion"))
        Dim lstDatosPlanes As New ArrayList
        ' PROY-31850 FASE IV - FIN

        Dim numDocCliOlo As String 'PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
        Try

            strNroTlf = txtNTelf.Text.Trim()

            If (strNroTlf.ToString.StartsWith(strInicioNroOLO)) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Consulta Planes para telefono OLO")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Consulta de numero de Telefono : " & strNroTlf)
                ' PROY-31850 FASE IV - INICIO
                Dim objPlanesOlo As New COM_SIC_Activaciones.BWRecargasOlo
                objPlanesOlo.ObtenerPlanesRecarga("51" & strNroTlf, idTrans, UserApli, CodApli, lstDatosPlanes, numDocCliOlo) 'numDocCliOlo  PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO

                'INICIO|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO
                Session("NumeroDocumentoOLO") = numDocCliOlo
                'FIN|PROY-31850 - INCIDENCIA RECARGA POR NUMERO DOCUMENTO

                'hdlMsj.Value = "Servicio de Planes de Olo en contruccion.. "
                cboImporte.Items.Clear()

                If lstDatosPlanes Is Nothing = True Then
                    hdlMsj.Value = "Error cargando planes OLO. Intente nuevamente!"
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Error cargando planes OLO. Intente nuevamente!")
                    sw = False
                End If
                If sw = True Then
                    If lstDatosPlanes.Count > 0 Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio de llenado Datos Planes OLO")
                        For indice As Integer = 0 To lstDatosPlanes.Count - 1 Step 1
                            Dim item As ListItem
                            item = New ListItem(lstDatosPlanes.Item(indice).descripcion, lstDatosPlanes.Item(indice).precio & ";" & lstDatosPlanes.Item(indice).codigoRecarga & ";" & lstDatosPlanes.Item(indice).codigoRecargaRedknee)
                            cboImporte.Items.Add(item)
                        Next
                        cboImporte.DataBind()
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin de llenado Datos Planes OLO")
                        ' PROY-31850 FASE IV - FIN
                        sw = True
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Carga de Planes OLO Exitosamente")
                    End If
                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Consulta Planes para telefono Movil Claro")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Consulta de numero de Telefono : " & strNroTlf)
                ' PROY-31850 FASE IV - INICIO
                cboImporte.Items.Clear()
                ' PROY-31850 FASE IV - FIN
                cargaRecarga()
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Consulta Planes para telefono Movil Claro")
                sw = True
            End If
            Return sw

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, ex.ToString())
        End Try

    End Function
    ' PROY: 31850 FASE IV FIN

    'PROY- 31766'
    Public Function ObtenerIGVActual() As Double
        Dim objConsultaIGV As New COM_SIC_Activaciones.clsWConsultaIGV
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        Dim strIdentifyLog As String = Funciones.CheckStr(Session("USUARIO"))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== INICIO - Proceso Met. ObtenerIGVActual() ==")
        Dim constIGV As Double = objConsultaIGV.ObtenerIGV(strCodRpta, strMsgRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   idRespuesta:" & strCodRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   mensaje:" & strMsgRpta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   IGV:" & constIGV)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== FIN - Proceso Met. ObtenerIGVActual() ==")
        Return constIGV
    End Function
    'PROY- 31766'

    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/	
    Public Function RegistrarHistoricoVenta(ByVal CadenaCabecera As String, ByVal CadenaDetalle As String) As Integer
        Dim p_documento As Int64 = 0
        Dim objTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
        Dim nroTelefonoRecargaVirtual As String
        Dim v_Topen_Codigo As String = ""
        Dim vTopenCodigoRV As String = ""
        Dim strClasePedido As String
        Dim strAlmacen As String
        Dim strPlazo As String
        Dim strOrgVenta As String

        Dim K_NRO_LOG As Int64 = 0
        Dim K_DES_LOG As String = ""
        Dim K_NRO_ERROR As String = ""
        Dim K_DES_ERROR As String = ""
        Dim p_msgerr As String = ""
        Dim dsOficina As DataSet
        Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu

        Dim strTrama1(49) As String
        Dim strTrama2(27) As String

        'Inicio registro de la venta en la tabla SISACT_AP_VENTA
        Try
            'Inicio Consultamos los parametros de configuracion correspondientes a Tarjeta Fisica y Recarga Virtual
            Dim codGrupo As Integer = Funciones.CheckDbl(ConfigurationSettings.AppSettings("constGrupoParam_ListaOperacion"))
            Dim dsCodigos As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)
            If Not IsNothing(dsCodigos) Then
                For idx As Integer = 0 To dsCodigos.Tables(0).Rows.Count - 1
                    If Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR1")) = "RECARGA VIRTUAL" Then
                        vTopenCodigoRV = Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR"))
                    End If
                Next
            End If
            'Fin Consultamos los parametros de configuracion correspondientes a Tarjeta Fisica y Recarga Virtual

            strAlmacen = Session("ALMACEN")
            v_Topen_Codigo = vTopenCodigoRV
            strPlazo = "00"

            If cboTipDocumento.SelectedValue = "06" Then
                strClasePedido = ConfigurationSettings.AppSettings("PEDIC_CLASEFACTURA")
            Else
                strClasePedido = ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA")
            End If

            dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), K_NRO_LOG, K_DES_LOG)
            strOrgVenta = Funciones.CheckStr(dsOficina.Tables(0).Rows(0).Item("PAOFV_ORGANIZACIONVENTAS"))

            Dim str_Tipo_Doc As String = ""
            Dim str_Codigo_Servicio As String = ""

            If (CadenaCabecera.Length > 0) Then
                strTrama1 = Split(CadenaCabecera, ";")
                str_Codigo_Servicio = strTrama1(16) 'codigo tipo venta : 02 pre --ConfigurationSettings.AppSettings("TVEN_CODIGO")
            Else
                str_Codigo_Servicio = ConfigurationSettings.AppSettings("TVEN_CODIGO")
            End If

            str_Tipo_Doc = ConfigurationSettings.AppSettings("PEDIC_TIPODOCCLIENTE") 'cboTipDocumento.SelectedValue

			objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Paran Inicio: objTrsPvu.RegistrarVenta")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " strClasePedido: " & strClasePedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " CANAL_PVU: " & ConfigurationSettings.AppSettings("CANAL_PVU"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " strAlmacen: " & strAlmacen)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " str_Tipo_Doc: " & str_Tipo_Doc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " MONEDA_PVU: " & ConfigurationSettings.AppSettings("MONEDA_PVU"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " v_Topen_Codigo: " & v_Topen_Codigo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " cboImporte: " & cboImporte.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " dblPreIGV: " & dblPreIGV)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " dblPrecio: " & dblPrecio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Obs: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " str_Codigo_Servicio: " & str_Codigo_Servicio)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Obs2: ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " USUARIO: 00000" & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " strPlazo: " & strPlazo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Vendedor: " & Funciones.CheckStr(Left(cboSelectVend.SelectedValue, 10)))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " strOrgVenta: " & strOrgVenta)
			
            objTrsPvu.RegistrarVenta(strClasePedido, _
                                     ConfigurationSettings.AppSettings("CANAL_PVU"), _
                                     strAlmacen, _
                                     str_Tipo_Doc, _
                                     Funciones.CheckStr(txtNumDocumento.Text), _
                                     ConfigurationSettings.AppSettings("MONEDA_PVU"), _
                                     v_Topen_Codigo, _
                                     cboImporte.SelectedValue, _
                                     dblPreIGV, _
                                     dblPrecio, _
                                     "", _
                                     str_Codigo_Servicio, _
                                     "", _
                                     "00000" & Session("USUARIO"), _
                                     strPlazo, _
                                     Funciones.CheckStr(Left(cboSelectVend.SelectedValue, 10)), _
                                     strOrgVenta, _
                                     0, _
                                     0, _
                                     p_msgerr, _
                                     p_documento)
									 
			objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " p_msgerr: " & p_msgerr)
			objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " p_documento: " & p_documento)
			objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & " Param Fin: objTrsPvu.RegistrarVenta")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Error al registrar la venta en pvu.")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Err." & ex.Message.ToString)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Err." & ex.StackTrace.ToString)
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Err. Param(p_msgerr) =>  " & IIf(p_msgerr.ToString = "", "Error, no registro", p_msgerr.ToString))
        End Try
        'Fin registro de la venta en la tabla SISACT_AP_VENTA

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Valor de retorno:")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param(p_documento) =>  " & IIf(p_documento.ToString = "", "Error, no registro", p_documento.ToString))
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param(p_msgerr) =>  " & IIf(p_msgerr.ToString = "", "Error, no registro", p_msgerr.ToString))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  FIN RegistrarVenta -  SP: sp_reg_venta")

        'Inicio registro del detalle de la venta en la tabla SISACT_AP_VENTA_DETALLE
        Try
            If p_documento > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  INICIO RegistrarVentaDetalle -  SP: sp_reg_venta_detalle")

                nroTelefonoRecargaVirtual = Funciones.CheckStr(txtNTelf.Text)
               
                If (CadenaDetalle.Length > 0) Then
                    strTrama2 = Split(CadenaDetalle, ";")
                    Dim str_Servicio As String = ""
                    Dim str_DescServicio As String = ""
                    Dim strPlan As String = ""
                    Dim strDescPlan As String = ""
                    Dim strCampania As String = ""
                    Dim strDescCampania As String = ""

                    Dim dbl_Precio As Double
                    Dim dbl_SubTotal As Double
                    Dim dbl_IGV As Double
                    Dim str_Total As Double

                    str_Servicio = strTrama2(2)
                    str_DescServicio = strTrama2(3)
                    strPlan = strTrama2(18)
                    strDescPlan = strTrama2(19)
                    strCampania = strTrama2(6)
                    strDescCampania = strTrama2(7)

                    dbl_Precio = Funciones.CheckDbl(cboImporte.SelectedValue)
                    dbl_SubTotal = Funciones.CheckDbl(dblPrecio)
                    dbl_IGV = Funciones.CheckDbl(dblPreIGV)
                    str_Total = Funciones.CheckDbl(dbl_SubTotal + dbl_IGV)

                    objTrsPvu.RegistrarVentaDetalle(Funciones.CheckInt64(1), _
                             Funciones.CheckInt64(p_documento), _
                             str_Servicio, _
                             str_DescServicio, _
                             strPlan, _
                             strDescPlan, _
                             nroTelefonoRecargaVirtual, _
                             strCampania, _
                             strDescCampania, _
                             Funciones.CheckInt("1"), _
                             dbl_Precio, _
                             0, _
                             dbl_SubTotal, _
                             dbl_IGV, _
                             str_Total, _
                             Funciones.CheckStr("01"), _
                             "PVP", _
                             "", _
                             "00", _
                             "SIN CUOTAS", _
                             "01", _
                             p_documento, _
                             p_msgerr)
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  No se registrara el detalle de a venta : No existe detalle de la venta")
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  No se logro registrar la venta : RegistrarVenta(PVU.TABLA: sisact_ap_venta)")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -   Error en el registro de venta en PVU")
            btnGrabarRec.Visible = False
            Exit Function
        End Try
        'Fin registro del detalle de la venta en la tabla SISACT_AP_VENTA_DETALLE

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -   FIN RegistrarVentaDetalle -  SP: sp_reg_venta_detalle")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fin de registro de venta en PVU")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")

        'devolvemos el valor del numero de venta generada
        Return p_documento
    End Function
    '/*----------------------------------------------------------------------------------------------------------------*/
    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
    '/*----------------------------------------------------------------------------------------------------------------*/			
End Class
