Imports Thycotic.Web.RemoteScripting
Imports System.Globalization
Imports System.IO
Imports System.Configuration
Imports COM_SIC_Activaciones


Public Class ventaArticulos
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cboIMEIArt As System.Web.UI.WebControls.DropDownList
    Protected WithEvents CAP As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents cboTipDocumento As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNumDocumento As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboClasePedido As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFechaPrecioVenta As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboMotivoPedido As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtcodcomer As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboSelectVend As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtTelfV As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboVPlazo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCodArti As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboArti As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNTelf As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboCamp As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboLPre As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboPlanT As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboCentroCosto As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tdBtnGrabar As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents btnAgregar As System.Web.UI.WebControls.Button
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnSeries As System.Web.UI.WebControls.Button
    Protected WithEvents btnAgregarGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents dgDetalle As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cboClasePedido1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCant As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtIMEIArt As System.Web.UI.HtmlControls.HtmlInputText

    Protected WithEvents hidCampana As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidListaPrecio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidPlanTarifario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIDCampana As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIDListaPrecio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIDPlanTarifario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtDescuentoCC As System.Web.UI.HtmlControls.HtmlInputText

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    '***********************************************************************************************************************'
    Dim clsConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu           '**** SINEGIA60 ****'
    Dim clsConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap       '**** SINEGIA60 ****'
    Dim objTrsPvu As New COM_SIC_Activaciones.clsTrsPvu                     '**** SINEGIA60 ****'
    Dim objTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap                 '**** SINEGIA60 ****'

    Dim k_nrolog As Integer = 0                                             '**** SINEGIA60 ****'
    Dim k_deslog As String = ""                                             '**** SINEGIA60 ****'
    Dim K_NROINTERNO_PEDIDO As Int64 = 0                                    '**** SINEGIA60 ****'
    Dim dsCliente As DataSet                                                '** Para almacenar los datos del cliente **'
    Dim arrCliente(66) As String 'SE ADICIONA 2 POSICIONES 31636                                           '** Guarda la data del cliente al momento de consultar su existencia.
    Dim str_pedic_tipo_documento As String
    Dim str_pedic_clasepedido As String = ""
    Dim strTipoMaterialVR As String
    Dim preciorecarga As Double = 0
    '***********************************************************************************************************************'

    Dim objVentas As New SAP_SIC_Ventas.clsVentas
    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim objCajas As New COM_SIC_Cajas.clsCajas
    Dim dtDetalle As New DataTable("Detalle")
    Dim blnError As Boolean
    Dim intAutoriza As Integer
    Dim objConf As New COM_SIC_Configura.clsConfigura
    Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
    Dim tbFacturacion As New DataTable
    Dim numeroOperacionSAP% = 0


    'inicio ts-JTN
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogVentaRapida")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    'fin ts-JTN

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
            Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
            Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
            If (Session("USUARIO") Is Nothing) Then
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Cargar load Venta Ràpida")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Declaraciòn de Variables")
                Dim dsTipDoc As DataSet
                Dim dsDocVta As DataSet
                Dim dsMotPed As DataSet
                Dim dsVendedor As DataSet
                Dim dsPlazos As DataSet
                Dim dsArticulos As DataSet
                Dim dsCampana As DataSet
                Dim dsCentro As DataSet
                Dim dsOficina As DataSet
                Dim dsLista As DataSet
                Dim dsPlanT As DataSet

                Dim dcClasePed As New DataColumn
                Dim dcVendedor As New DataColumn
                Dim dcVendedor2 As New DataColumn
                Dim dcArticulo As New DataColumn

                Dim dvCuotas As New DataView
                Dim dvTipDoc As New DataView
                Dim dvArti As New DataView
                Dim dvClasePed As New DataView
                Dim dvClasePed1 As New DataView
                Dim objOffline As New COM_SIC_OffLine.clsOffline
                Dim i As Integer
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Declaraciòn de Variables")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Asignaciòn de Valores")
                dtDetalle.Columns.Add("Pos", GetType(System.String))
                dtDetalle.Columns.Add("Articulo", GetType(System.String))
                dtDetalle.Columns.Add("Cantidad", GetType(System.String))
                dtDetalle.Columns.Add("Util", GetType(System.String))
                dtDetalle.Columns.Add("Serie", GetType(System.String))
                dtDetalle.Columns.Add("Denominacion", GetType(System.String))
                dtDetalle.Columns.Add("Valor", GetType(System.String))
                dtDetalle.Columns.Add("PorDes", GetType(System.String))
                dtDetalle.Columns.Add("DesAdic", GetType(System.String))
                dtDetalle.Columns.Add("Campana", GetType(System.String))
                dtDetalle.Columns.Add("PT", GetType(System.String))
                dtDetalle.Columns.Add("NroTelef", GetType(System.String))
                dtDetalle.Columns.Add("IGV", GetType(System.String))
                dtDetalle.Columns.Add("Lista", GetType(System.String))
                dtDetalle.Columns.Add("Camp", GetType(System.String))
                dtDetalle.Columns.Add("PlanT", GetType(System.String))
                dtDetalle.Columns.Add("CCosto", GetType(System.String))
                dtDetalle.Columns.Add("GRUPO", GetType(System.String))
                dtDetalle.Columns.Add("StrSeries", GetType(System.String))

                cboCamp.Attributes.Add("onChange", "f_CambiaCamp()")
                cboLPre.Attributes.Add("onChange", "f_CambiaLista()")
                cboArti.Attributes.Add("onChange", "f_CambiaArti()")
                cboPlanT.Attributes.Add("onChange", "f_CambiaPlan()")
                cboClasePedido.Attributes.Add("onChange", "f_CambiaClasePed()")
                cboClasePedido1.Attributes.Add("onChange", "f_CambiaClasePed()")
                cboMotivoPedido.Attributes.Add("onChange", "f_CambiaMotivo()")
                cboSelectVend.Attributes.Add("onChange", "f_CambiaVend()")
                txtCodArti.Attributes.Add("onBlur", "f_CambiaTxtArti()")
                btnAgregar.Attributes.Add("onClick", "f_Agrega()")
                btnAgregarGrabar.Attributes.Add("onClick", "f_AgregaGraba()")
                btnGrabar.Attributes.Add("onClick", "f_Valida()")
                btnSeries.Attributes.Add("onClick", "f_Series()")

                cboTipDocumento.Attributes.Add("onChange", "f_CambiaTipDoc()")
                txtNumDocumento.Attributes.Add("onKeyDown", "textCounter(this)")
                txtNumDocumento.Attributes.Add("onKeyUp", "textCounter(this)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Asignaciòn de Valores")

                If Not Page.IsPostBack Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Entra en el Not Postback ")
                    Me.hidCampana.Value = String.Empty
                    Me.hidListaPrecio.Value = String.Empty
                    Me.hidPlanTarifario.Value = String.Empty
                    Me.hidIDCampana.Value = "0"
                    Me.hidIDListaPrecio.Value = "0"
                    Me.hidIDPlanTarifario.Value = "0"

                    ' intAutoriza = objConf.FP_Inserta_Aut_Transac(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("USUARIO"), Session("NOMBRE_COMPLETO"), "", "", _
                    '                         "", "", "", "", 0, 1, 0, 0, 0, 0, 0, 0, "")
                    'If intAutoriza = 1 Then

                    tdBtnGrabar.Visible = False

                    dgDetalle.DataSource = dtDetalle
                    '****** CAMBIO EN LOS PARAMETROS DE LA OFICINA *****'
                    'dsOficina = objPagos.Get_ParamGlobal(Session("ALMACEN"))               '** change **'
                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia Consulta la parametros de Oficina")
                        dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Consulta loa paràmetros de Oficina")

                        'Dim isDbnull As Boolean = Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
                        Dim isDbnull As Boolean = Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("PAOFV_COD_CLTE_VARIOS"))
                        Dim numDocumento$
                        Dim leerClienteVarios As Boolean = (ConfigurationSettings.AppSettings("FLAG_COD_CLTE_VARIOS") = 1)

                        If (leerClienteVarios) Then
                            numDocumento$ = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS")
                        Else
                            'numDocumento$ = IIf(isDbnull, ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"), _
                            '                    Trim(Convert.ToString(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Key : COD_CLTE_VARIOS")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Key : COD_CLTE_VARIOS =>" & ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"))
                            numDocumento$ = IIf(isDbnull, ConfigurationSettings.AppSettings("COD_CLTE_VARIOS"), _
                                                Trim(Convert.ToString(dsOficina.Tables(0).Rows(0).Item("PAOFV_COD_CLTE_VARIOS"))))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Key COD_CLTE_VARIOS")

                        End If
                        txtNumDocumento.Text = numDocumento
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al consultar: ConsultaParametrosOficina")
                    End Try

                    ''Trim(dsOficina.Tables(0).Rows(0).Item("COD_CLTE_VARIOS"))
                    '*****************************FIN CAMBIO PARÀMETROS *******************************'
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Obtenemos la fecha de precio venta")
                    txtFechaPrecioVenta.Text = Format(Day(Now.Date), "00") & "/" & Format(Month(Now.Date), "00") & "/" & Format(Year(Now.Date), "0000")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fecha => " & Funciones.CheckStr(txtFechaPrecioVenta.Text))
                    '******* SINERGIA60 09-02-15***************************************************
                    'dsTipDoc = objVentas.Get_LeeTipoDocCliente

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio consulta tipo documento")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Mètodo : clsConsultaPvu.ConsultaTipoDocumento")
                    dsTipDoc = clsConsultaPvu.ConsultaTipoDocumento("")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Cantidad de registros: " & dsTipDoc.Tables(0).Rows.Count.ToString)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta tipo documento")
					
		    dsTipDoc.Tables(0).Rows(2).Delete()
					
                    ''TODO CAMBIADO POR JYMMY TORRES
                    'dsTipDoc = objOffline.ListarTipoDocumentosCliente
                    ''CAMBIADO HASTA AQUI

                    If Not dsTipDoc Is Nothing Then
                        dvTipDoc.Table = dsTipDoc.Tables(0)
                        'dvTipDoc.RowFilter = "J_1ATODC <> '-'"

                        cboTipDocumento.DataSource = dvTipDoc
                        'cboTipDocumento.DataValueField = "J_1ATODC"
                        'cboTipDocumento.DataTextField = "TEXT30"
                        cboTipDocumento.DataValueField = "TDOCC_CODIGO"
                        cboTipDocumento.DataTextField = "TDOCV_DESCRIPCION"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Setea para que el tipo de documento sea DNI")
                        If dsTipDoc.Tables(0).Rows.Count > 0 Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, "Entra Setea para que el tipo de documento sea DNI")
                            'cboTipDocumento.SelectedValue = "01"   '**DNI** 31636
                            cboTipDocumento.SelectedValue = "01" 
                        End If
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Setea para que el tipo de documento sea DNI")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron datos para el tipo documento.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_CONSULTA_GENERAL.SISACT_CON_TIPO_DOC")
                    End If
                    '*fin cambio tipo documento **********************************************************


                    '***SINERGIA60: **********************************************************************************'
                    'dsDocVta = objVentas.Get_ConsultaClasePedido(Session("ALMACEN")) ''TODO: RFC SAP
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Consulta la clase pedido: ConsultaClasePedido")
                    dsDocVta = clsConsultaPvu.ConsultaClasePedido(System.Configuration.ConfigurationSettings.AppSettings("tipo_canal"), System.Configuration.ConfigurationSettings.AppSettings("docu_estado"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Consulta la clase pedido: ConsultaClasePedido")

                    ''TODO: CAMBIADO POR JYMMY TORRES
                    'dsDocVta = objOffline.ConsultaClasePedido(Session("ALMACEN"))
                    ''CAMBIADO HASTA AQUI


                    'dcClasePed.ColumnName = "AUART2"
                    'dcClasePed.DataType = GetType(String)
                    'dcClasePed.Expression = "AUART+RECIBE_PAGO"
                    'dsDocVta.Tables(0).Columns.Add(dcClasePed)

                    If Not dsDocVta Is Nothing Then
                        '**Natural:
                        dvClasePed.Table = dsDocVta.Tables(0)
                        'dvClasePed.RowFilter = "KSCHL='ZFAC' or KSCHL='FAC' or KSCHL='ZNCV' or KSCHL='NPED' or KSCHL='DEV' or KSCHL='N/C'"
                        dvClasePed.RowFilter = "docu_codigo='ZPVR'"

                        '**Juridica:
                        dvClasePed1.Table = dsDocVta.Tables(0)
                        'dvClasePed1.RowFilter = "KSCHL<>'ZFAC' and KSCHL<>'FAC'"
                        dvClasePed1.RowFilter = "docu_codigo='ZPBR'"

                        '****divNat
                        cboClasePedido.DataSource = dvClasePed
                        'cboClasePedido.DataValueField = "AUART2"
                        'cboClasePedido.DataTextField = "BEZEI"
                        cboClasePedido.DataValueField = "docu_codigo"
                        cboClasePedido.DataTextField = "docu_descrip"


                        '****divJur:
                        cboClasePedido1.DataSource = dvClasePed1
                        'cboClasePedido1.DataValueField = "AUART2"
                        'cboClasePedido1.DataTextField = "BEZEI"
                        cboClasePedido1.DataValueField = "docu_codigo"
                        cboClasePedido1.DataTextField = "docu_descrip"


                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Termina la asignaciòn de laConsulta la clase pedido: ConsultaClasePedido")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros de tipo documento venta.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar : pvu.sisact_pkg_cons_maestra_sap_6.sisact_oficina_docu_cons")
                    End If
                    '**FIN DOCUMENTOS DE VENTA********************************************************************************'



                    '*INICIO CAMBIOS MOTIVO PEDIDO***********************************************************************************
                    'dsMotPed = objVentas.Get_ConsultaMotivoPedido(Session("ALMACEN"), "") ' TODO: CALLBACK SAP
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "inicio consulta : ConsultaMotivoPedido")
                    dsMotPed = Me.clsConsultaPvu.ConsultaMotivoPedido(ConfigurationSettings.AppSettings("moti_opera"), ConfigurationSettings.AppSettings("moti_estado"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta : ConsultaMotivoPedido")

                    '''TODO:CAMBIADO POR JYMMY TORRES
                    'dsMotPed = objOffline.ConsultaMotivoPedido("", Session("ALMACEN"))
                    '''CAMBIADO HASTA AQUI
                    If Not dsMotPed Is Nothing Then
                        cboMotivoPedido.DataSource = dsMotPed.Tables(0)
                        'cboMotivoPedido.DataValueField = "AUGRU"
                        'cboMotivoPedido.DataTextField = "BEZEI"
                        cboMotivoPedido.DataValueField = "moti_codigo"
                        cboMotivoPedido.DataTextField = "moti_descrip"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignaciòn de motivo pedido")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros para el motivo de pedido.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar : pvu.sisact_pkg_cons_maestra_sap_6.sisact_motivo_venta_cons")
                    End If
                    '**FIN CAMBIOS MOTIVO PEDIDO******************



                    '*******************************************************************************************'
                    '***DATOS VENDEDOR SINERGIA60 ***'
                    '*******************************************************************************************'
                    'dsVendedor = objVentas.Get_ConsultaVendedor(Session("ALMACEN")) ''TODO: CALLBACK SAP

                    '''TODO: CAMBIADO POR JYMMY TORRES
                    'dsVendedor = objOffline.Get_ConsultaVend(Session("ALMACEN"))
                    '''CAMBIADO HASTA AQUI

                    'dcVendedor.ColumnName = "USUARIOFONO"
                    'dcVendedor.DataType = GetType(String)
                    'dcVendedor.Expression = "USUARIO+'#'+TELEFONO"
                    'dsVendedor.Tables(0).Columns.Add(dcVendedor)

                    'dcVendedor2.ColumnName = "USUARIONOMBRE"
                    'dcVendedor2.DataType = GetType(String)
                    'dcVendedor2.Expression = "USUARIO+' - '+TRIM(NOMBRE)"
                    'dsVendedor.Tables(0).Columns.Add(dcVendedor2)

                    'cboSelectVend.DataSource = dsVendedor.Tables(0)
                    'cboSelectVend.DataValueField = "USUARIOFONO"
                    'cboSelectVend.DataTextField = "USUARIONOMBRE"

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio de la consulta del Vendedor: ConsultaVendedor")
                    dsVendedor = objOffline.ListarVenderoresPorTienda(Session("ALMACEN")) 'SD1056123
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin de la consulta del Vendedor: ConsultaVendedor")

                    If Not dsVendedor Is Nothing Then
                        dcVendedor.ColumnName = "CODIGOUSUARIO"
                        dcVendedor.DataType = GetType(String)
                        dcVendedor.Expression = "USUARIO+' - '+NOMBRE" 'SD1056123
                        dsVendedor.Tables(0).Columns.Add(dcVendedor)
                        cboSelectVend.DataSource = dsVendedor.Tables(0)
                        cboSelectVend.DataValueField = "USUARIO" 'SD1056123
                        cboSelectVend.DataTextField = "CODIGOUSUARIO"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta vendedor.")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros de vendedores.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_CONS_MAESTRA_SAP_6.SISACT_VENDEDORES_SAP_CONS")
                    End If

                    '*******************************************************************************************'


                    '***** CAMBIOS CUOTAS : *********************************************************************'
                    'dsPlazos = objVentas.Get_LeeCuotas()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Consulta las cuotas: ConsultaCuotas(PVU)")
                    dsPlazos = Me.clsConsultaPvu.ConsultaCuotas()
                    If Not dsPlazos Is Nothing Then
                        dvCuotas.Table = dsPlazos.Tables(0)
                        'dvCuotas.RowFilter = "CUOTA = '01' or CUOTA = '02' or CUOTA = '03' or CUOTA = '06' or CUOTA = '12' or CUOTA = '18'"

                        cboVPlazo.DataSource = dvCuotas
                        'cboVPlazo.DataValueField = "CUOTA"
                        'cboVPlazo.DataTextField = "DESCRIPCION"
                        cboVPlazo.DataValueField = "CUOC_CODIGO"
                        cboVPlazo.DataTextField = "CUOV_DESCRIPCION"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignaciòn de las cuotas")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros del nùmero de cuotas.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_GENERAL.SP_CON_TIPO_CUOTA")
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin las cuotas: ConsultaCuotas(PVU)")
                    '********************************************************************************************'

                    '***** CONSULTA DE ARTICULOS **************************************************************
                    ' Por defecto, el tipo de venta sera PREPAGO = 02
                    'dsArticulos = objVentas.Get_ConsultaArticulo(Now.Date, "", "02", Session("ALMACEN"), "")
                    'dsArticulos = objVentas.Get_ConsultaArticulo(Format(Day(Now.Date), "00") & "/" & Format(Month(Now.Date), "00") & "/" & Format(Year(Now.Date), "0000"), "", "02", Session("ALMACEN"), "") ''TODO:CALLBACK SAP
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio de la consulta de materiasles : ConsultaStock")
                    dsArticulos = clsConsultaMssap.ConsultaStock(ConsultaPuntoVenta(Session("ALMACEN")), "", ConfigurationSettings.AppSettings("tipo_oficina"), "1")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin de la consulta de materiasles : ConsultaStock")

                    If Not dsArticulos Is Nothing Then
                        'MATKL
                        dcArticulo.ColumnName = "MATNRMATKL"
                        dcArticulo.DataType = GetType(String)
                        'dcArticulo.Expression = "MATNR+'#'+MATKL+'#'+SERNP"
                        '*** SE INDICO QUE EL GRUPO MATERIAL ES EL TIPO
                        'dcArticulo.Expression = "MATEC_CODMATERIAL+'#'+MATEC_TIPOMATERIAL+'#'+MATEC_TIPOMATERIAL"
                        dcArticulo.Expression = "MATEC_CODMATERIAL+'#'+MATEC_TIPOMATERIAL"
                        dsArticulos.Tables(0).Columns.Add(dcArticulo)

                        '************************************************************************** 
                        '** bloque comentado segùn especificaciones de EDD - EVERIS 16.02.2015 **'
                        '************************************************************************** 

                        'Filtrar materiales MiClaro
                        'Dim strFiltroMaterial As String
                        'Dim arrMaterialesNoPermitidos() As String
                        'arrMaterialesNoPermitidos = Funciones.CheckStr(ConfigurationSettings.AppSettings("constCodMatChipRepMiClaro")).Split(";"c)

                        'For i = 0 To UBound(arrMaterialesNoPermitidos)
                        '    If strFiltroMaterial = "" Then
                        '        'strFiltroMaterial = "MATNR <> '" & arrMaterialesNoPermitidos(i) & "'"
                        '        strFiltroMaterial = "MATEC_CODMATERIAL <> '" & arrMaterialesNoPermitidos(i) & "'"
                        '    Else
                        '        'strFiltroMaterial = strFiltroMaterial & " and MATNR <> '" & arrMaterialesNoPermitidos(i) & "'"
                        '        strFiltroMaterial = strFiltroMaterial & " and MATEC_CODMATERIAL <> '" & arrMaterialesNoPermitidos(i) & "'"
                        '    End If
                        'Next

                        ''PAOFC_SERVICIO_TECNICO
                        ''If Trim(dsOficina.Tables(0).Rows(0).Item("SERV_TECNICO")) = "" Then
                        'If Convert.IsDBNull(dsOficina.Tables(0).Rows(0).Item("PAOFC_SERVICIO_TECNICO")) OrElse Trim(dsOficina.Tables(0).Rows(0).Item("PAOFC_SERVICIO_TECNICO")) = "" Then
                        '    If strFiltroMaterial = "" Then
                        '        'strFiltroMaterial = strFiltroMaterial & "SUBSTRING(MATNR,1,6) <> 'SERTEC'"
                        '        strFiltroMaterial = strFiltroMaterial & "SUBSTRING(MATEC_CODMATERIAL,1,6) <> 'MATEC_TIPOMATERIAL'"
                        '    Else
                        '        'strFiltroMaterial = strFiltroMaterial & " and " & "SUBSTRING(MATNR,1,6) <> 'SERTEC'"
                        '        strFiltroMaterial = strFiltroMaterial & " and " & "SUBSTRING(MATEC_CODMATERIAL,1,6) <> 'MATEC_TIPOMATERIAL'"
                        '    End If
                        'End If

                        'dvArti.Table = dsArticulos.Tables(0)
                        'If strFiltroMaterial <> "" Then
                        '    dvArti.RowFilter = strFiltroMaterial
                        'End If
                        '*****************************************************************************************************

                        'cboArti.DataSource = dvArti
                        'Fin Filtrar materiales MiClaro

                        cboArti.DataSource = dsArticulos.Tables(0)
                        cboArti.DataValueField = "MATNRMATKL" ' "MATEC_CODMATERIAL" '"MATNRMATKL"
                        'cboArti.DataTextField = "MAKTX"
                        cboArti.DataTextField = "MATEV_DESCMATERIAL"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignaciòn de datos al cbo artículo.")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros de materiales.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: SINERGIA_MMSAP.PKG_MSSAP.SSAPSS_STOCK")
                    End If
                    '***** FIN LISTA DE MATERIALES *************************************************************************


                    '**CAMPAÑA:**********************************************************************
                    'dsCampana = objVentas.Get_ConsultaCampana(Now.Date, "02")
                    'dsCampana = objVentas.Get_ConsultaCampana(Format(Day(Now.Date), "00") & "/" & Format(Month(Now.Date), "00") & "/" & Format(Year(Now.Date), "0000"), "02")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Proceso ConsultaCampanaXTipoVenta")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Parametros: ")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Param1: NULL")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Param2: NULL")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Param3: " & System.Configuration.ConfigurationSettings.AppSettings("tipo_venta"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Param4: " & System.Configuration.ConfigurationSettings.AppSettings("P_CAMPC_ESTADO"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia consulta: ConsultaCampanaXTipoVenta")

                    dsCampana = clsConsultaPvu.ConsultaCampanaXTipoVenta(DBNull.Value, DBNull.Value, _
                                                                         System.Configuration.ConfigurationSettings.AppSettings("tipo_venta"), _
                                                                         System.Configuration.ConfigurationSettings.AppSettings("P_CAMPC_ESTADO"))

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin  consulta: ConsultaCampanaXTipoVenta")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia la asignaciòn a la lista desplegable")

                    If Not dsCampana Is Nothing Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Entra a asignar datos de campaña por tipo venta")
                        cboCamp.DataSource = dsCampana.Tables(0)
                        cboCamp.DataBind()

                        cboCamp.DataValueField = "CAMPV_CODIGO"
                        cboCamp.DataTextField = "CAMPV_DESCRIPCION"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin la asignaciòn a la lista desplegable")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros de campaña")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: pvu.SISACT_PKG_MANT_CONVERGENTE_6.SP_CON_CAMPANHAS_TIPO_VENTA")
                    End If

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio Asignaciòn de la campaña")
                    If Not dsCampana Is Nothing Then
                        cboCamp.SelectedIndex = 0
                    End If
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin Asignaciòn de la campaña")
                    '***FIN CAMPAÑA************************************************



                    '************************************************************************************************'
                    '**CENTRO DE COSTOS:***'
                    '************************************************************************************************'
                    'dsCentro = objVentas.Get_ConsultaCentroCostos("", Session("ALMACEN"), "")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia consulta : ConsultaCentroCostos")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Paràmetros:")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Paràm1:" & Session("ALMACEN"))
                    dsCentro = clsConsultaPvu.ConsultaCentroCostos(Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta : ConsultaCentroCostos")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia asignaciòn: ConsultaCentroCostos")
                    If Not dsCentro Is Nothing Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Entra asignar los datos")
                        cboCentroCosto.DataSource = dsCentro.Tables(0)
                        'cboCentroCosto.DataValueField = "MVGR4"
                        'cboCentroCosto.DataTextField = "KTEXT"
                        cboCentroCosto.DataValueField = "codigo"
                        cboCentroCosto.DataTextField = "descripcion"
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignar los datos")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin asignaciòn: ConsultaCentroCostos")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraton registros de centros de costo.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_CONS_MAESTRA_SAP_6.sp_con_centro_costo")
                    End If
                    '***FIN CONSULTA CENTRO COSTO ********************************************************************'


                    '+++++++++++++++++++++++++++++++++++++++++++++++++++
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Hace el DataBing para todas las listas")
                    Me.DataBind()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin DataBing para todas las listas")
                    '+++++++++++++++++++++++++++++++++++++++++++++++++++

                    If Trim(Session("TipDoc")) <> "" Then
                        cboTipDocumento.SelectedValue = Session("TipDoc")
                        txtNumDocumento.Text = Session("NumDoc")
                        Session("TipDoc") = ""
                        Session("NumDoc") = ""
                    End If


                    Try
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia selecciòn del Vendedor:")
                        'If Session("CANAL") = "MT" And Trim(dsOficina.Tables(0).Rows(0).Item("VAL_VEND_POST")) <> "" Then
                        If Session("CANAL") = "MT" And Trim(IIf(isDbnull(dsOficina.Tables(0).Rows(0).Item("PAOFC_VENTAPOSPAGO")), "", dsOficina.Tables(0).Rows(0).Item("PAOFC_VENTAPOSPAGO"))) <> "" Then
                            cboSelectVend.Enabled = False
                            For i = 0 To cboSelectVend.Items.Count - 1
                                'If cboSelectVend.Items(i).Value = Session("USUARIO") Then
                                If Trim(Funciones.CheckInt64(cboSelectVend.Items(i).Value)) = Trim(Funciones.CheckInt64(CStr(Session("USUARIO")))) Then
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Encontro el vendedor en la Lista")
                                    cboSelectVend.SelectedIndex = i
                                    'txtTelfV.Text = Right(cboSelectVend.Items(i).Value, 10)
                                    Exit For
                                End If
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "No encontro el vendedor de la sessiòn en la lista de vendedores consultados.")
                            Next
                        Else
                            cboSelectVend.Enabled = True
                        End If
                    Catch ex As Exception
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Error al tratar de asignar el vendedor del inicio de session.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Err." & ex.Message.ToString)
                        Response.Write("<script>alert('Error al tratar de asignar el inicio de sesiòn al vendedor.')</script>")
                        Exit Sub
                    End Try


                    '******************************************************************************************************************'
                    '** LISTA DE PRECIOS :
                    'dsLista = objVentas.Get_ConsultaUtilizacion(txtFechaPrecioVenta.Text, cboCamp.SelectedValue, "02", "", "")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Consulta la lista de precios")
                    If Not dsCampana Is Nothing Then
                        'dsLista = clsConsultaPvu.ConsultaListaPrecios(System.Configuration.ConfigurationSettings.AppSettings("TPROC_CODIGO"), _
                        '                                System.Configuration.ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                        '                                System.Configuration.ConfigurationSettings.AppSettings("CANAC_CODIGO"), _
                        '                                System.Configuration.ConfigurationSettings.AppSettings("DEPAC_CODIGO"), _
                        '                                txtCodArti.Text, _
                        '                                cboCamp.SelectedValue, _
                        '                                System.Configuration.ConfigurationSettings.AppSettings("TOPEN_CODIGO"), _
                        '                                System.Configuration.ConfigurationSettings.AppSettings("PLZAC_CODIGO"))
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin consulta de la lista de precios.")

                        'objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia la asignaciòn de datos de la lista de precios.")
                        'cboLPre.DataSource = dsLista.Tables(0)
                        ''cboLPre.DataValueField = "ABRVW"
                        '' cboLPre.DataTextField = "BEZEI"
                        'cboLPre.DataValueField = "LIPRN_CODIGOLISTAPRECIO"
                        'cboLPre.DataTextField = "LIPRV_DESCRIPCION"
                        'cboLPre.DataBind()
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin la asignaciòn de datos de la lista de precios.")
                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron listas de precios configuradas para la campaña")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_NUEVA_LISTAPRECIOS.SISACSS_CONSULTAR_LISTAPRECIOS")
                    End If

                    'cboLPre.SelectedValue = "01"
                    'cboLPre.SelectedIndex = 1
                    '******************************************************************************************************************'


                    '******************************************************************************
                    '*** Plan tarifario:
                    'dsPlanT = objVentas.Get_ConsultaPlanTarifario(cboLPre.SelectedValue, "02")
                    '
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia consulta : ConsultaPlanXTipoVenta")
                    dsPlanT = clsConsultaPvu.ConsultaPlanXTipoVenta("", "", _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("TIPO_VENTA"), _
                                                                        System.Configuration.ConfigurationSettings.AppSettings("PLANC_ESTADO"))

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin  consulta : ConsultaPlanXTipoVenta")

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia asignaciòn ConsultaPlanXTipoVenta")
                    If Not dsPlanT Is Nothing Then
                        cboPlanT.DataSource = dsPlanT.Tables(0)
                        'cboPlanT.DataValueField = "PLAN_TARIFARIO"
                        'cboPlanT.DataTextField = "DESCRIPCION"
                        cboPlanT.DataValueField = "PLNV_CODIGO"
                        cboPlanT.DataTextField = "PLNV_DESCRIPCION"
                        cboPlanT.DataBind()
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin  asignaciòn ConsultaPlanXTipoVenta")

                    Else
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "No se encontraron registros para el plan tarifario.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, "Verificar: PVU.SISACT_PKG_MANT_CONVERGENTE_6.SP_CON_PLANES_TIPO_VENTA")
                    End If
                    '********************************************************************************************************************************************


                    cboCamp.Items.Insert(0, "")
                    cboPlanT.Items.Insert(0, "")
                    cboLPre.Items.Insert(0, "")
                    cboMotivoPedido.Items.Insert(0, "")
                    cboSelectVend.Items.Insert(0, "")
                    cboVPlazo.Items.Insert(0, "")
                    cboArti.Items.Insert(0, "")
                    cboCentroCosto.Items.Insert(0, "")

                    Dim valor_plan As String = ""

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "For del Plan Tarifario")
                    For i = 0 To cboPlanT.Items.Count - 1
                        If Trim(cboPlanT.Items(i).Value) = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica") Then
                            cboPlanT.SelectedIndex = i
                            valor_plan = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica")
                        End If
                    Next

                    If valor_plan = "" Then
                        Dim oListItem As New ListItem
                        oListItem.Text = ConfigurationSettings.AppSettings("Descr_PlanTarifario_NoAplica")
                        oListItem.Value = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica")
                        cboPlanT.Items.Add(oListItem)
                    End If

                    cboPlanT.SelectedValue = ConfigurationSettings.AppSettings("Codigo_PlanTarifario_NoAplica") '"000"

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "For del ZPBR")
                    For i = 0 To cboClasePedido1.Items.Count - 1
                        If Left(cboClasePedido1.Items(i).Value, 4) = "ZPBR" Then
                            cboClasePedido1.SelectedIndex = i
                        End If
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "For del ZPVR")
                    For i = 0 To cboClasePedido.Items.Count - 1
                        If Left(cboClasePedido.Items(i).Value, 4) = "ZPVR" Then
                            cboClasePedido.SelectedIndex = i
                        End If
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "IsNothing(Session(detVenta))")
                    If Not IsNothing(Session("detVenta")) Then
                        dgDetalle.DataSource = Session("detVenta")
                        dgDetalle.DataBind()
                        Session("detVenta") = Nothing
                        If dgDetalle.Items.Count > 0 Then
                            tdBtnGrabar.Visible = True
                        End If
                    End If

                    If Len(Trim(Session("strArti"))) > 0 Then
                        'Response.Write(Request.Item("strArti")) : Response.End()
                        txtCodArti.Text = Session("strArti")
                        txtCant.Value = SeriesCountCant(Session("strTrama"), Format(dgDetalle.Items.Count + 1, "000000"))
                        If txtCant.Value = 0 Then
                            txtCant.Value = ""
                        End If
                        If Not Session("serie_defecto") Is Nothing Then
                            txtIMEIArt.Value = Session("serie_defecto")
                            If txtIMEIArt.Value <> "" Then
                                Session("serie_defecto") = Nothing
                            End If
                        End If
                    End If
                End If
            End If
        End If
        objFileLog.Log_WriteLog(pathFile, strArchivo, "Fin la ejecuciòn del Load -  Venta artículos.")
    End Sub

    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Dim strIdLog As String = Session("USUARIO")
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdLog & " - " & "  INICIO Consulta punto de venta")
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdLog & " - " & "      Inicia el proceso: ConsultaPuntoVenta")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdLog & " - " & "          IN PDV: " & P_OVENC_CODIGO)
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdLog & " - " & "      OUT Valor encontrado: " & Funciones.CheckStr(dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")))
            If dsReturn.Tables(0).Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdLog & " - " & "  FIN Consulta punto de venta")
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If
            Return Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Function FormatoTelefono(ByVal telefono)
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
    End Function

    Private Sub ConsultaFecha()
        Dim strMensaje As String = String.Empty
        Dim objBSCS As New COM_SIC_Cajas.clsCajas
        Dim strCodRenovacion As String
        Dim resp As String

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = "Log_RenovacionRPC6" 'ConfigurationSettings.AppSettings("constNameLogRegistroDOL")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRegistroDOL")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = txtNumDocumento.Text

        Try
            strCodRenovacion = ConfigurationSettings.AppSettings("strCodRenovacion")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio FP_ConsultaFCaducidaRPM6-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  txtNTelf : " & txtNTelf.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  strCodRenovacion : " & strCodRenovacion)

            resp = objBSCS.FP_ConsultaFCaducidaRPM6(FormatoTelefono(txtNTelf.Text.Trim), strCodRenovacion)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  resp : " & resp)

            Dim ArrayResp = Split(resp, ";")

            If ArrayResp(0) = "0" Then
                strMensaje = "El cliente cuenta con el plan " & cboPlanT.SelectedItem.Text & ". La fecha de caducidad de su plan es: " & ArrayResp(1)
            Else
                strMensaje = "Error al verificar Renovación en BSCS, consulte con el administrador"
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  strMensaje : " & strMensaje)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin FP_ConsultaFCaducidaRPM6--------------")

            Session.Add("msgCaducidadRMP6", strMensaje)
            Response.Write("<script language=javascript>alert('" & strMensaje & "');</script>")
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: ConsultaFecha)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   ERROR : " & ex.Message & MaptPath)
            'FIN PROY-140126
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------Fin FP_ConsultaFCaducidaRPM6--------------")

            strMensaje = "Error al verificar Renovación en BSCS. "
            Response.Write("<script language=javascript>alert('" & strMensaje & "');</script>")
        End Try
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        'Response.Write("<script>alert('boton agregar CodeBehind.')</script>")
        'Response.Write("boton agregar CodeBehind")
        'Response.End()

        If Request.Item("hidVerif") = "1" Then
            If txtCodArti.Text.Trim = ConfigurationSettings.AppSettings("strCodArticuloRenovacion") Then
                ConsultaFecha()
            End If
            Try
                AgregaLinea()
                cboCamp.SelectedIndex = 0
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Error.." & ex.Message)
                cboCamp.SelectedIndex = 0
            End Try
        End If
    End Sub

    Private Sub AgregaLinea()
        Dim dsPrecio As DataSet
        Dim rpt As Integer = 0
        Dim dsPrecioMaterial As DataSet
        Dim dsPrecioVenta As DataSet
        Dim dsArticulo As DataSet
        Dim dblPrecio As Double
        Dim dblDescuento As Double
        Dim dblPreIGV As Double
        Dim dblTotal As Double
        Dim strPos As String
        Dim strSeries As String
        Dim i As Integer
        Dim j As Integer
        Dim dsOficina As DataSet
        Dim drFila As DataRow
        Dim strIMEIArt As String
        Dim strClasePedido As String

        '*** Nuevas variales ******************************'
        Dim K_PREVC_CODMATERIAL_OUT As String = ""
        Dim K_PREVV_DESCRIPCION_OUT As String = ""
        Dim K_PREVC_CODSERIE_OUT As String = ""
        Dim K_PREVN_PRECIOBASE_OUT As Double
        Dim K_PREVN_PRECIOVENTA_OUT As Double
        Dim K_PREVN_DESCUENTO_OUT As Double
        Dim K_PREVN_IGV_OUT As Double
        Dim K_PREVN_TOTAL_OUT As Double
        '***************************************************



        '*************************************************************************************************'
        'dsOficina = objPagos.Get_ParamGlobal(Session("ALMACEN"))
        dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)
        '*************************************************************************************************'

        If cboTipDocumento.SelectedValue = "06" Then
            strClasePedido = cboClasePedido.SelectedValue
        Else
            strClasePedido = cboClasePedido1.SelectedValue
        End If


        If Request.Item("txtIMEIArt") = "" Then
            strIMEIArt = Request.Item("hidIMEI")
        Else
            strIMEIArt = Repetir("0", 18 - Len(Trim(Request.Item("txtIMEIArt")))) & Request.Item("txtIMEIArt")
        End If

        'If Left(txtCodArti.Text, 2) = "RC" Then
        If Left(txtCodArti.Text, 2) = "TV0005" Then
            If Session("strTrama") <> "" Then
                strSeries = Session("strTrama")
            Else
                strSeries = ""
            End If
        Else
            strSeries = ""
        End If

        Dim objVentasSans As New NEGOCIO_SIC_SANS.SansNegocio
        Dim usuario_id As String = Session("codUsuario")


        strPos = Format(dgDetalle.Items.Count + 1, "000000")
        'Response.Write("inicio AgregaLinea")
        'Response.End()

        '*****************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************
        Dim listaMatCambioSIM As String = ConfigurationSettings.AppSettings("codMatChipRepuesto")
        'If listaMatCambioSIM.IndexOf(txtCodArti.Text) > -1 Then
        '    'dsPrecio = objVentas.Get_ConsultaPrecio(Session("ALMACEN"), "", strPos, txtCodArti.Text, cboLPre.SelectedValue, CDbl(Request.Item("txtCant")), txtFechaPrecioVenta.Text, "", txtNTelf.Text, strClasePedido, strSeries, Trim(dsOficina.Tables(0).Rows(0).Item("VTWEG")), Trim(dsOficina.Tables(0).Rows(0).Item("VKORG")), dblDescuento, dblPreIGV, dblPrecio, dblTotal)
        '    dsPrecio = objVentasSans.Get_ConsultaPrecio(Session("ALMACEN"), "", strPos, txtCodArti.Text, cboLPre.SelectedValue, CDbl(Request.Item("txtCant")), txtFechaPrecioVenta.Text, "", txtNTelf.Text, strClasePedido, strSeries, Trim(dsOficina.Tables(0).Rows(0).Item("VTWEG")), Trim(dsOficina.Tables(0).Rows(0).Item("VKORG")), dblDescuento, dblPreIGV, dblPrecio, dblTotal, usuario_id)
        'Else
        'dsPrecio = objVentas.Get_ConsultaPrecio(Session("ALMACEN"), "", strPos, txtCodArti.Text, cboLPre.SelectedValue, CDbl(Request.Item("txtCant")), txtFechaPrecioVenta.Text, strIMEIArt, txtNTelf.Text, strClasePedido, strSeries, Trim(dsOficina.Tables(0).Rows(0).Item("VTWEG")), Trim(dsOficina.Tables(0).Rows(0).Item("VKORG")), dblDescuento, dblPreIGV, dblPrecio, dblTotal)
        'dsPrecio = objVentasSans.Get_ConsultaPrecio(Session("ALMACEN"), "", strPos, txtCodArti.Text, cboLPre.SelectedValue, CDbl(Request.Item("txtCant")), txtFechaPrecioVenta.Text, strIMEIArt, txtNTelf.Text, strClasePedido, strSeries, Trim(dsOficina.Tables(0).Rows(0).Item("VTWEG")), Trim(dsOficina.Tables(0).Rows(0).Item("VKORG")), dblDescuento, dblPreIGV, dblPrecio, dblTotal, usuario_id)


        'harcode cboCamp.SelectedValue =0178
        'dsPrecio = clsConsultaPvu.ConsultaListaPrecios(System.Configuration.ConfigurationSettings.AppSettings("TPROC_CODIGO"), _
        '                        System.Configuration.ConfigurationSettings.AppSettings("TIPO_VENTA"), _
        '                        System.Configuration.ConfigurationSettings.AppSettings("CANAC_CODIGO"), _
        '                        System.Configuration.ConfigurationSettings.AppSettings("DEPAC_CODIGO"), _
        '                        txtCodArti.Text, _
        '                         "0178", _
        '                        System.Configuration.ConfigurationSettings.AppSettings("TOPEN_CODIGO"), _
        '                        System.Configuration.ConfigurationSettings.AppSettings("PLZAC_CODIGO"))
        'End If
        '*****************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************

        'Response.Write("total Get_ConsultaPrecio: " & dsPrecio.Tables(0).Rows.Count)
        'Response.End()
        blnError = False

        'For i = 0 To dsPrecio.Tables(0).Rows.Count - 1
        '    If dsPrecio.Tables(0).Rows(i).Item("TYPE") = "E" Then
        '        blnError = True
        '        Session("strTrama") = ""
        '        Response.Write("<script>alert('" & dsPrecio.Tables(0).Rows(i).Item("MESSAGE") & "')</script>")
        '        Exit For
        '    End If
        'Next


        If Not blnError Then

            Dim strServicio As String = ConfigurationSettings.AppSettings("MATERIAL_RV")
            'dsArticulo = objVentas.Get_ConsultaArticulo(txtFechaPrecioVenta.Text, txtCodArti.Text, "02", Session("ALMACEN"), "")
            'dsArticulo = clsConsultaMssap.ConsultaStock(ConsultaPuntoVenta(Session("ALMACEN")), "", ConfigurationSettings.AppSettings("tipo_oficina"), "1")

            'If CDbl(dsOficina.Tables(0).Rows(0).Item("MONTO_MIN_RECAR")) > CDbl(txtCant.Value) Or CDbl(dsOficina.Tables(0).Rows(0).Item("MONTO_MAX_RECAR")) < CDbl(txtCant.Value) Then
            'If CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA")) > CDbl(txtCant.Value) Or CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA")) < CDbl(txtCant.Value) Then
            '    'Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("MONTO_MIN_RECAR") & " y " & dsOficina.Tables(0).Rows(0).Item("MONTO_MAX_RECAR") & "')</script>")
            '    Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA") & " y " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA") & "')</script>")
            '    blnError = True
            '    Exit Sub
            'End If

            'INI-936 - SMB - Se agrego el IndexOf para recorrer la variable strServicio
            If (Funciones.CheckStr(strServicio).IndexOf(Funciones.CheckStr(Mid(cboArti.SelectedValue, 1, 18)) > -1)) Then
                If CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA")) > CDbl(txtCant.Value) Or CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA")) < CDbl(txtCant.Value) Then
                    'Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("MONTO_MIN_RECAR") & " y " & dsOficina.Tables(0).Rows(0).Item("MONTO_MAX_RECAR") & "')</script>")
                    Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA") & " y " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA") & "')</script>")
                    blnError = True
                    Exit Sub
                End If
                IngresaVentaRecargaRV(txtNTelf.Text)
            End If

            'For i = 0 To dsArticulo.Tables(0).Rows.Count - 1
            '    Dim strArrTiposServicio() As String = CStr(ConfigurationSettings.AppSettings("constServicios")).Split("|")
            '    Dim isservicios As Boolean = False
            '    For Each itemTipoServ As String In strArrTiposServicio
            '        isservicios = Funciones.CheckStr(dsArticulo.Tables(0).Rows(i).Item("MATEC_TIPOMATERIAL")).IndexOf(itemTipoServ) >= 0
            '        If isservicios Then
            '            Exit For
            '        End If
            '    Next

            '    'MATEC_TIPOMATERIAL
            '    'If dsArticulo.Tables(0).Rows(i).Item("MATKL") = "A10130011" Then
            '    If isservicios Then
            '        Dim objConf As New COM_SIC_Configura.clsConfigura
            '        Dim strServicio As String = ConfigurationSettings.AppSettings("MATERIAL_RV") 'dsParamOf.Tables(0).Rows(0).Item("CAJA_RECVIRTUAL")
            '        Dim strDescServicio As String = dsParamOf.Tables(0).Rows(0).Item("CAJA_RECVIRTDES")

            '        'If CDbl(dsOficina.Tables(0).Rows(0).Item("MONTO_MIN_RECAR")) > CDbl(txtCant.Value) Or CDbl(dsOficina.Tables(0).Rows(0).Item("MONTO_MAX_RECAR")) < CDbl(txtCant.Value) Then
            '        If CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA")) > CDbl(txtCant.Value) Or CDbl(dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA")) < CDbl(txtCant.Value) Then
            '            'Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("MONTO_MIN_RECAR") & " y " & dsOficina.Tables(0).Rows(0).Item("MONTO_MAX_RECAR") & "')</script>")
            '            Response.Write("<script language=javascript>alert('La recarga debe ser entre " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMINIMORECARGA") & " y " & dsOficina.Tables(0).Rows(0).Item("PAOFC_MONTOMAXIMORECARGA") & "')</script>")
            '            Exit Sub
            '        End If

            '        'If (strServicio <> dsArticulo.Tables(0).Rows(i)(0)) Then
            '        '    Response.Write(String.Format("<script language=javascript>alert('{0}');</script>", "El material de recarga no pertenece a la oficina de venta."))
            '        '    blnError = True
            '        '    Exit Sub
            '        'End If
            '        If (strServicio = dsArticulo.Tables(0).Rows(i).Item("MATEC_CODMATERIAL")) Then
            '            IngresaVentaRecargaRV(txtNTelf.Text)
            '        End If

            '    End If
            'Next


            If Not blnError Then
                For i = 0 To dgDetalle.Items.Count - 1
                    drFila = dtDetalle.NewRow()
                    For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                        drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
                    Next
                    dtDetalle.Rows.Add(drFila)
                Next

                drFila = dtDetalle.NewRow()

                '*******************************************************************************************'
                '**CONSULTAMOS LOS VALORES PARA LA LISTA DE PRECIOS ***'
                Try
                    'I.  Consulta a precio Base : 000000000070000917
                    dsPrecio = Me.clsConsultaMssap.ConsultarPrecioBase(Funciones.CheckStr(txtCodArti.Text), "")
                    'dsPrecio = Me.clsConsultaMssap.ConsultarPrecioBase("000000000070000917", "")

                    'RETORNA: SELECT MATEC_CODMATERIAL, MATEV_DESCMATERIAL, MATEN_PRECIOBASE, MATEN_PRECIOCOMPRA
                    'CANAC_CODIGO
                    If Not dsPrecio Is Nothing Then
                        'II. 
                        'SELECT DISTINCT L.LIPRN_CODIGOLISTAPRECIO,L.LIPRV_DESCRIPCION, M.MATED_PRECIOVENTA

                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - Llamando - clsConsultaPvu.ConsultaListaPrecios()")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT TPROC_CODIGO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("TPROC_CODIGO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT TVENC_CODIGO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("TVENC_CODIGO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT CANAC_CODIGO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("CANAC_CODIGO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT DEPARTAMENTO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("DEPARTAMENTO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT TOPEN_CODIGO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("TOPEN_CODIGO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT PLZAC_CODIGO - " & Funciones.CheckStr(ConfigurationSettings.AppSettings("PLZAC_CODIGO")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - MATEC_CODMATERIAL : " & Funciones.CheckStr(dsPrecio.Tables(0).Rows(0).Item("MATEC_CODMATERIAL")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - MATEN_PRECIOBASE : " & Funciones.CheckStr(dsPrecio.Tables(0).Rows(0).Item("MATEN_PRECIOBASE")))
                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - cboCamp.SelectedValue : " & Funciones.CheckStr(cboCamp.SelectedValue))

                        dsPrecioMaterial = clsConsultaPvu.ConsultaListaPrecios(ConfigurationSettings.AppSettings("TPROC_CODIGO"), _
                        ConfigurationSettings.AppSettings("TVENC_CODIGO"), _
                        ConfigurationSettings.AppSettings("CANAC_CODIGO"), _
                        ConfigurationSettings.AppSettings("DEPARTAMENTO"), _
                        dsPrecio.Tables(0).Rows(0).Item("MATEC_CODMATERIAL"), _
                        cboCamp.SelectedValue, _
                        System.Configuration.ConfigurationSettings.AppSettings("TOPEN_CODIGO"), _
                        System.Configuration.ConfigurationSettings.AppSettings("PLZAC_CODIGO"))

                        objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - Fin de llamando - clsConsultaPvu.ConsultaListaPrecios()")
                        If Not dsPrecioMaterial Is Nothing Then
                            'III. PRECIO BASE Y PRECIO VENTA

                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - Llamando - clsConsultaMssap.ConsultarPrecioVenta()")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT MATEC_CODMATERIAL : " & Funciones.CheckStr(dsPrecio.Tables(0).Rows(0).Item("MATEC_CODMATERIAL")))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT strIMEIArt : " & Funciones.CheckStr(strIMEIArt))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT MATEN_PRECIOBASE : " & Funciones.CheckStr(dsPrecio.Tables(0).Rows(0).Item("MATEN_PRECIOBASE")))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INPUT MATED_PRECIOVENTA : " & Funciones.CheckStr(dsPrecioMaterial.Tables(0).Rows(0).Item("MATED_PRECIOVENTA")))

                            dsPrecioVenta = clsConsultaMssap.ConsultarPrecioVenta(dsPrecio.Tables(0).Rows(0).Item("MATEC_CODMATERIAL"), _
                                                        strIMEIArt, _
                                                        dsPrecio.Tables(0).Rows(0).Item("MATEN_PRECIOBASE"), _
                                                        dsPrecioMaterial.Tables(0).Rows(0).Item("MATED_PRECIOVENTA"), _
                                                         K_PREVC_CODMATERIAL_OUT, _
                                                         K_PREVV_DESCRIPCION_OUT, _
                                                         K_PREVC_CODSERIE_OUT, _
                                                         K_PREVN_PRECIOBASE_OUT, _
                                                         K_PREVN_PRECIOVENTA_OUT, _
                                                         K_PREVN_DESCUENTO_OUT, _
                                                         K_PREVN_IGV_OUT, _
                                                         K_PREVN_TOTAL_OUT)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - Fin de Llamando - clsConsultaMssap.ConsultarPrecioVenta()")

                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVC_CODMATERIAL_OUT : " & Funciones.CheckStr(K_PREVC_CODMATERIAL_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVV_DESCRIPCION_OUT : " & Funciones.CheckStr(K_PREVV_DESCRIPCION_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVC_CODSERIE_OUT : " & Funciones.CheckStr(K_PREVC_CODSERIE_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVN_PRECIOBASE_OUT : " & Funciones.CheckStr(K_PREVN_PRECIOBASE_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVN_PRECIOVENTA_OUT : " & Funciones.CheckStr(K_PREVN_PRECIOVENTA_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVN_DESCUENTO_OUT : " & Funciones.CheckStr(K_PREVN_DESCUENTO_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVN_IGV_OUT : " & Funciones.CheckStr(K_PREVN_IGV_OUT))
                            objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - OUTPUT K_PREVN_TOTAL_OUT : " & Funciones.CheckStr(K_PREVN_TOTAL_OUT))
                             


                        End If
                    End If
                Catch ex As Exception
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Agregar Linea ERROR : " & ex.Message)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Agregar Linea ERROR : Consultar la Lista de Precios.")
                End Try
                '*******************************************************************************************'
                '**RESERVAR SERIE:
                rpt = clsConsultaMssap.ReservarSerie(strIMEIArt)
                If rpt = 0 Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " Ocurrio un error al reservar la serie.")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " La serie " & strIMEIArt & " fue reservada correctamente.")
                End If
                '*******************************************************************************************'
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - INICIO - Valores para DRFILA para el guardado de Pedido-")
                drFila("Pos") = strPos
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Pos) : " & Funciones.CheckStr(drFila("Pos")))
                drFila.Item("Articulo") = txtCodArti.Text

                Session("CodArtiRV") = txtCodArti.Text.ToString.Trim
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Articulo) : " & Funciones.CheckStr(drFila("Articulo")))
                '************************************************************************
                If ConfigurationSettings.AppSettings("MATERIAL_RV").IndexOf(Session("CodArtiRV") > -1) Then   'INI-936 - SMB - Key Recargas Virtual
                    drFila.Item("Cantidad") = "1"
                Else
                    drFila.Item("Cantidad") = Request.Item("txtCant")
                End If
                '************************************************************************

                drFila.Item("Util") = Server.HtmlDecode(Me.hidListaPrecio.Value) ' cboLPre.SelectedItem.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Util) : " & Funciones.CheckStr(drFila("Util")))
                drFila.Item("Serie") = strIMEIArt
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Serie) : " & Funciones.CheckStr(drFila("Serie")))
                drFila.Item("Denominacion") = cboArti.SelectedItem.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Denominacion) : " & Funciones.CheckStr(drFila("Denominacion")))

                '***EL PRECIO DEBE SER SIN IGV:*******************************************************************
                If ConfigurationSettings.AppSettings("MATERIAL_RV").IndexOf(Session("CodArtiRV") > 1) Then   'INI-936 - SMB - Key
                    drFila.Item("Valor") = Format(Funciones.CheckDbl(Request.Item("txtCant")), "####0.00")   'Format(dblPrecio, "####0.00")
                Else
                    drFila.Item("Valor") = Format(K_PREVN_PRECIOVENTA_OUT * Funciones.CheckDbl(Request.Item("txtCant")), "####0.00")  'Format(dblPrecio, "####0.00")
                End If
                '*************************************************************************************************
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Valor) : " & Funciones.CheckStr(drFila("Valor")))

                drFila.Item("PorDes") = ""
                'drFila.Item("DesAdic") = Format(K_PREVN_DESCUENTO_OUT, "####0.00")
                drFila.Item("DesAdic") = Format(K_PREVN_DESCUENTO_OUT * Funciones.CheckDbl(Request.Item("txtCant")), "####0.00")
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(DesAdic) : " & Funciones.CheckStr(drFila("DesAdic")))

                drFila.Item("Campana") = Server.HtmlDecode(Me.hidCampana.Value)   'cboCamp.SelectedItem.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Campana) : " & Funciones.CheckStr(drFila("Campana")))

                drFila.Item("PT") = Server.HtmlDecode(Me.hidPlanTarifario.Value) 'cboPlanT.SelectedItem.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(PT) : " & Funciones.CheckStr(drFila("PT")))

                drFila.Item("NroTelef") = txtNTelf.Text
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(NroTelef) : " & Funciones.CheckStr(drFila("NroTelef")))
                If Session("CodArtiRV") = ConfigurationSettings.AppSettings("MATERIAL_RV") Then  '**KEY 
                    drFila.Item("IGV") = Format(Funciones.CheckDbl(Request.Item("txtCant")), "####0.00")
                Else
                    'drFila.Item("IGV") = Format(K_PREVN_IGV_OUT, "####0.00") 'dblPreIGV - dblPrecio
                    Dim dValorVentaD3 As Double
                    Dim dValorVenta As Double
                    dValorVentaD3 = Math.Round(K_PREVN_IGV_OUT, 3)
                    dValorVentaD3 = dValorVentaD3 + 0.001
                    dValorVenta = Math.Round(dValorVentaD3, 2)
                    drFila.Item("IGV") = Format(dValorVenta * Funciones.CheckDbl(Request.Item("txtCant")), "####0.00") 'dblPreIGV - dblPrecio
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(IGV) : " & Funciones.CheckStr(drFila("IGV")))

                drFila.Item("Lista") = Me.hidIDListaPrecio.Value 'cboLPre.SelectedValue
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Lista) : " & Funciones.CheckStr(drFila("Lista")))

                drFila.Item("Camp") = Me.hidIDCampana.Value 'cboCamp.SelectedValue
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(Camp) : " & Funciones.CheckStr(drFila("Camp")))

                drFila.Item("PlanT") = Me.hidIDPlanTarifario.Value 'cboPlanT.SelectedValue
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(PlanT) : " & Funciones.CheckStr(drFila("PlanT")))

                drFila.Item("CCosto") = cboCentroCosto.SelectedValue
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(CCosto) : " & Funciones.CheckStr(drFila("CCosto")))

                drFila.Item("GRUPO") = Mid(cboArti.SelectedValue, 20, 6)
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(GRUPO) : " & Funciones.CheckStr(drFila("GRUPO")))

                drFila.Item("strSeries") = strSeries
                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(txtCodArti.Text) & " - drFila(strSeries) : " & Funciones.CheckStr(drFila("strSeries")))

                dtDetalle.Rows.Add(drFila)
                dgDetalle.DataSource = dtDetalle
                dgDetalle.DataBind()

                'resetear el estado serie
                Session("strArti") = ""
                Session("strTrama") = ""
                txtCant.Value = ""

                txtCodArti.Text = ""

                tdBtnGrabar.Visible = True

                Session("strTipoMaterialVR") = Mid(cboArti.SelectedValue, 20, 6)
            End If
        End If

    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        If Request.Item("hidVerif") = "1" Then
            Session.Remove("msgCaducidadRMP6")
            GrabarPedido()
        End If
    End Sub

    Private Sub VerificaSaldoTienda()
        Dim strSaldo As String
        Dim dsOficina As DataSet    '***
        If DetalleRecarga() Then
            IngresaVentaRecargaRV(dgDetalle.Items(0).Cells(12).Text)
            If Not blnError Then
                dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)
                'strSaldo = objVentas.Get_ConsultaSaldoRecarga(Session("ALMACEN"))
                '**PAOFC_MONTOMAXIMORECARGA**'
                strSaldo = IIf(IsDBNull(dsOficina.Tables(0).Rows(0).Item("PAOFC_SALDO_RECARGA_VIRTUAL")), 0, dsOficina.Tables(0).Rows(0).Item("PAOFC_SALDO_RECARGA_VIRTUAL"))
                If CDbl(dgDetalle.Items(0).Cells(3).Text) > CDbl(strSaldo) Then
                    Response.Write("<script>alert('Error: Se debe configurar el codigo de tienda para realizar recargas.')</script>")
                End If
            End If
        End If
    End Sub

    'Private Sub IngresaVentaRecargaRV(ByVal strNumTelefono As String)
    '    Dim pvTerminalID As String
    '    Dim pvTrace As String
    '    Dim pvCanal As String
    '    Dim pvTelefono As String
    '    Dim pvBinAdquiriente As String
    '    Dim pvCodCadena As String
    '    Dim pvCodComercio As String
    '    Dim pvMonto As String
    '    Dim pvMoneda As String
    '    Dim pvProducto As String

    '    Dim strVendedor As String
    '    Dim strTrama As String
    '    Dim arrTrama() As String
    '    Dim strMensaje As String

    '    Dim objRVirt As New COM_SIC_RVirtual.clsRVirtual

    '    'strVendedor = objVentas.Get_ConsultaVendedorRecarga(Session("ALMACEN"), "MT")
    '    strVendedor = System.Configuration.ConfigurationSettings.AppSettings("CODIGO_CADENA")
    '    If Len(strVendedor) = 0 Then
    '        Response.Write("<script>alert('Error: Se debe configurar el codigo de tienda para realizar recargas.')</script>")
    '        blnError = True
    '    Else
    '        pvBinAdquiriente = Mid(strVendedor, 4, 6)
    '        pvCodCadena = Mid(strVendedor, 4, 7)
    '        pvCodComercio = Session("ALMACEN")

    '        pvTerminalID = "PVU"
    '        'pvTrace = "000002"
    '        pvTrace = Convert.ToString(Right("00" & Now().Second, 2) + Right("0000" & CDbl(Session("USUARIO")) * Now().Millisecond(), 4))

    '        pvCanal = "91"
    '        pvTelefono = strNumTelefono

    '        pvMonto = Request.Item("txtCant")
    '        pvMoneda = "604"
    '        pvProducto = "1"

    '        strTrama = objRVirt.Consulta(Session("ALMACEN"), pvTerminalID, pvTrace, pvCanal, pvTelefono, pvBinAdquiriente, pvCodCadena, pvCodComercio)

    '        arrTrama = Split(strTrama, ";")

    '        If CDbl(Trim(arrTrama(0))) < 0 Then
    '            strMensaje = "Error: No se puede realizar el servicio de recarga virtual. Motivo: " & arrTrama(3)
    '            If Trim(arrTrama(3)) = "" Then
    '                strMensaje = "NO SE PUEDEN REALIZAR OPERACIONES DE RECARGA VIRTUAL EN ESTE MOMENTO" & pvTrace
    '            End If
    '            Response.Write("<script>alert('" & strMensaje & "  ')</script>")
    '            blnError = True
    '        End If

    '    End If

    'End Sub
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

    Private Function DetalleRecarga() As Boolean
        Dim i As Integer
        Dim blnResult As Boolean
        blnResult = False
        For i = 0 To dgDetalle.Items.Count - 1
            If dgDetalle.Items(i).Cells(18).Text = "A10130011" Then 'A10130011--> recarga virtual
                blnResult = True
            End If
        Next
        DetalleRecarga = blnResult
    End Function

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
                .linea = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(25))) 'Funciones.CheckStr(Session("numeroTelefono"))
                .estado = ""
                .fecha = ""
                .nombreUsuario = strUserSesion
                .puntoVenta = Session("ALMACEN")
                .tipoDocumento = Funciones.CheckStr(cboTipDocumento.SelectedValue).ToString.Trim
                .numeroDocumento = Funciones.CheckStr(txtNumDocumento.Text)
                .lineaCliente = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(25)))
                .montoRecarga = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .fechaSwTrx = "" 'Fecha  respuesta del switch transaccional"
                .valorVenta = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .valorDescuento = "0"
                .valorSubTotal = Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)))
                .valorIGV = "0" 'arrDetalle(16)
                .valorTotal = Funciones.CheckDbl(Funciones.CheckDbl(arrDetalle(10))) '+ Funciones.CheckDbl(arrDetalle(16))) 'arrDetalle(11)
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
        Dim K_NRO_ERROR As String = ""
        Dim K_DES_ERROR As String = ""
        Dim p_msgerr As String = ""
        Dim p_documento As Int64 = 0
        Dim strTipoMaterial As String = ""
        Dim strEsquemaCalculo As String = ""

        Dim dsResultado As DataSet
        Dim i As Integer
        Dim j As Integer
        Dim t As Integer
        Dim intOper As Integer
        Dim strFecha As String = ""
        Dim strRealizado As String = ""
        Dim strTipDocVta As String = ""
        Dim arrCabecera(49) As String  'CARIAS (40)
        Dim arrDetalle(27) As String
        Dim strCabecera As String = ""
        Dim strDetalle As String = ""
        Dim dblMonto As Double
        Dim dblIGV As Double
        Dim intAux As Integer

        Dim strEntrega As String = ""
        Dim strFactura As String = ""
        Dim strNroContrato As String = ""
        Dim strNroDocCliente As String = ""
        Dim strNroPedido As String = ""
        Dim strRefHistorico As String = ""
        Dim strTipDocCliente As String = ""
        Dim dblValorDescuento As Decimal
        Dim dvFiltro As New DataView
        Dim drFila As DataRow

        Dim strCadenaSeries As String = ""
        Dim strClasePedido As String = ""
        Dim strDescrClasePedido As String = ""

        'Variables de Auditoria
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
        'CC -Lisetti Macedo
        Dim GeneroDsct As Integer = 0
        Dim valEquipo As Integer = 0
        'CC -Lisetti Macedo

        Dim sector_Configurable As String = ""

        Dim dsOficina As DataSet

        Dim isRecargaVirtua2 As Boolean

        'For t = 0 To dgDetalle.Items.Count - 1
        '    ' isRecargaVirtual = Funciones.CheckStr(dgDetalle.Items(t).Cells(18).Text).IndexOf(Funciones.CheckStr(ConfigurationSettings.AppSettings("constServicios"))) >= 0 'cboArti.SelectedValue.IndexOf(ConfigurationSettings.AppSettings("constServicios")) > 0
        'Next
        'isRecargaVirtual = Funciones.CheckStr(dgDetalle.Items(i).Cells(18).Text).IndexOf(Funciones.CheckStr(ConfigurationSettings.AppSettings("constServicios"))) >= 0 'cboArti.SelectedValue.IndexOf(ConfigurationSettings.AppSettings("constServicios")) > 0
        Dim isRecargaVirtual As Boolean = cboArti.SelectedValue.IndexOf(Funciones.CheckStr(ConfigurationSettings.AppSettings("constServicios"))) >= 0

        '/*----------------------------------------------------------------------------------------------------------------*/
        '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
        '/*----------------------------------------------------------------------------------------------------------------*/									                                    
        Dim nroTelefonoVentaRapida As String
        Dim isVentaTarjetaFisica As Boolean
        Dim isVentaRecargaVirtual As Boolean
        Dim v_Topen_Codigo As String = ""
        '/*----------------------------------------------------------------------------------------------------------------*/
        '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
        '/*----------------------------------------------------------------------------------------------------------------*/									                                    

        Dim idPedido As Integer
        Session("recargaVirtual") = isRecargaVirtual
        Dim strIdentifyLog As String = "GrabarPedido - " & Funciones.CheckStr(Session("USUARIO"))

        '*******************************************************************************************************'
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "=============================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "             INICIO GRABAR PEDIDO            ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "=============================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Inicio Consulta los datos de la oficina")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN PDV: " & Session("ALMACEN"))
        dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fin Consulta datos de la oficina")
        '*******************************************************************************************************'

        'PROY- 31766
        Dim objConsultaIGV As New COM_SIC_Activaciones.clsWConsultaIGV
        Dim strCodRpta As String = String.Empty
        Dim strMsgRpta As String = String.Empty
        Dim constIGV As Double
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== INICIO - Proceso Met. ObtenerIGVActual() ==")
            constIGV = objConsultaIGV.ObtenerIGV(strCodRpta, strMsgRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   idRespuesta:" & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   mensaje:" & strMsgRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   IGV:" & constIGV)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   ERROR:" & ex.Message.ToString())
            constIGV = Convert.ToDouble(ConfigurationSettings.AppSettings("valorIGV"))
        Finally
            constIGV = Math.Round(constIGV, 2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== FIN - Proceso Met. ObtenerIGVActual() ==")
        End Try
        'PROY- 31766'

        If cboTipDocumento.SelectedValue = "06" Then
            strClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEFACTURA")               'cboClasePedido.SelectedValue
            strDescrClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_DESCCLASEFACTURA")
        Else
            strClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEBOLETA")                'cboClasePedido1.SelectedValue
            strDescrClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_DESCCLASEBOLETA")
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicio Tipo y Clase de Pedido")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   cboTipDocumento.SelectedValue : " & Funciones.CheckStr(cboTipDocumento.SelectedValue))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   strClasePedido : " & Funciones.CheckStr(strClasePedido))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   strDescrClasePedido : " & Funciones.CheckStr(strDescrClasePedido))
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Fin Tipo y Clase de Pedido")

        strFecha = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")

        If BuscaCliente() Then
            'dsResultado = objVentas.Get_VerificaVendedor(Session("ALMACEN"), Left(cboSelectVend.SelectedValue, 10))
            blnError = False

            If Not blnError Then
                '*********INICIO DE LA VALIDACIÒN DE CAJA ******************************************************************************************************************************************************************************************

                Dim cod_usuario_formateado As String = ""
                cod_usuario_formateado = Funciones.CheckStr(Session("USUARIO")).PadLeft(10, "0")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicia consulta de la caja asignada.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   Paràmetros:")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   Param1: " & DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToShortDateString)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   (strFecha) " & strFecha.ToString)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   Param2: " & cod_usuario_formateado)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   Param3: " & Funciones.CheckStr(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Ejecuta consulta.")

                '********* dsResultado = clsConsultaMssap.ObtenerCajaAsignada(DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), "00000" & Funciones.CheckStr(Session("USUARIO")), Funciones.CheckStr(Session("ALMACEN")))
                dsResultado = clsConsultaMssap.ObtenerCajaAsignada(strFecha, cod_usuario_formateado, Funciones.CheckStr(Session("ALMACEN")))

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fin consulta de la caja asignada.")

                '** I-DG
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Inicia validaciòn datos de caja asignada.")
                If dsResultado Is Nothing Then
                    '**no se encontro datos en la consulta.
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "-   No existe ninguna caja asignada, termina el proceso.")
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
                            strRealizado = Funciones.CheckStr(dsResultado.Tables(0).Rows(0).Item("CAJA_CERRADA"))
                        End If
                    End If
                End If
                '** F-DG

                If Not blnError Then
                    If strRealizado = "S" Then
                        blnError = True
                        Response.Write("<script>alert('No se puede realizar pedidos por que se realizo el cierre de caja')</script>")
                    Else
                        'VerificaSaldoTienda()
                        If Not blnError Then
                            strTipDocVta = IIf(Right(Trim(strClasePedido), 1) = "X" And Len(Trim(strClasePedido)) > 4, Left(strClasePedido, 4), strClasePedido)
                            arrCabecera(1) = strTipDocVta
                            arrCabecera(2) = Session("ALMACEN")
                            arrCabecera(3) = txtFechaPrecioVenta.Text
                            arrCabecera(4) = cboTipDocumento.SelectedValue
                            arrCabecera(5) = txtNumDocumento.Text
                            arrCabecera(6) = cboMotivoPedido.SelectedValue
                            arrCabecera(7) = Left(Request.Item("cboMoneda"), 1)
                            arrCabecera(16) = "02" 'Se asume prepago 'Session("TipVenta")
                            arrCabecera(27) = Left(cboSelectVend.SelectedValue, 10)
                            arrCabecera(24) = cboVPlazo.SelectedValue

                            If Trim(arrCabecera(6)) = "T23" Then
                                arrCabecera(25) = txtcodcomer.Text
                            End If
                            arrCabecera(29) = "01"  ' se asume ALTA

                            For i = 0 To dgDetalle.Items.Count - 1
                                dblMonto += CDbl(dgDetalle.Items(i).Cells(7).Text)
                                dblIGV += CDbl(dgDetalle.Items(i).Cells(13).Text)

                                arrDetalle(1) = dgDetalle.Items(i).Cells(1).Text  'pos
                                arrDetalle(2) = dgDetalle.Items(i).Cells(2).Text 'articulo

                                intAux = InStr(1, dgDetalle.Items(i).Cells(6).Text, "-")
                                arrDetalle(3) = Server.HtmlDecode(Trim(Right(dgDetalle.Items(i).Cells(6).Text, Len(dgDetalle.Items(i).Cells(6).Text) - intAux)))  'desc Articulo
                                arrDetalle(4) = dgDetalle.Items(i).Cells(14).Text 'Lista
                                arrDetalle(5) = Server.HtmlDecode(dgDetalle.Items(i).Cells(4).Text) 'Desc. Lista
                                arrDetalle(6) = dgDetalle.Items(i).Cells(15).Text 'Campaña
                                arrDetalle(7) = Server.HtmlDecode(dgDetalle.Items(i).Cells(10).Text)   'Desc. Campaña
                                arrDetalle(8) = IIf(dgDetalle.Items(i).Cells(5).Text = "&nbsp;", "", dgDetalle.Items(i).Cells(5).Text)  'IMEI
                                arrDetalle(9) = dgDetalle.Items(i).Cells(3).Text 'Cantidad
                                arrDetalle(10) = dgDetalle.Items(i).Cells(7).Text 'Valor
                                arrDetalle(11) = CDbl(arrDetalle(10)) + CDbl(dgDetalle.Items(i).Cells(13).Text) 'SubTotal
                                arrDetalle(12) = IIf(dgDetalle.Items(i).Cells(8).Text = "&nbsp;", "0", dgDetalle.Items(i).Cells(8).Text) 'Descuento
                                arrDetalle(13) = "0" ' Descuento Adicional
                                arrDetalle(14) = "0" ' Porcentaje Descuento Adicional
                                arrDetalle(15) = arrDetalle(10) 'Valor
                                arrDetalle(16) = dgDetalle.Items(i).Cells(13).Text

                                arrDetalle(18) = dgDetalle.Items(i).Cells(16).Text  'Plan tarif
                                arrDetalle(19) = Server.HtmlDecode(dgDetalle.Items(i).Cells(11).Text)  ' desc Plan Tarif
                                arrDetalle(20) = IIf(dgDetalle.Items(i).Cells(17).Text = "&nbsp;", "", dgDetalle.Items(i).Cells(17).Text) ' Centro de costo
                                arrDetalle(22) = dgDetalle.Items(i).Cells(18).Text  ' Grupo de Art

                                If Funciones.CheckStr(ConfigurationSettings.AppSettings("constServicios")).IndexOf(Funciones.CheckStr(arrDetalle(22))) > -1 Then
                                    isRecargaVirtua2 = True
                                End If
                                If Funciones.CheckStr(ConfigurationSettings.AppSettings("constServicios")).IndexOf(Funciones.CheckStr(dgDetalle.Items(i).Cells(18).Text)) > -1 Then
                                    isRecargaVirtua2 = True
                                End If
                                arrDetalle(25) = IIf(dgDetalle.Items(i).Cells(12).Text = "&nbsp;", "", dgDetalle.Items(i).Cells(12).Text) ' Telefono
                                'CC-Lisetti - Validacion de equipos para descuentos CC
                                If dgDetalle.Items(i).Cells(18).Text = ConfigurationSettings.AppSettings("constPacks") Then
                                    valEquipo = valEquipo + 1
                                End If
                                'CC-Lisetti -Fin
                                If Len(strDetalle) > 0 Then
                                    strDetalle = strDetalle & "|" & Join(arrDetalle, ";")
                                Else
                                    strDetalle = Join(arrDetalle, ";")
                                End If

                                If Len(Trim(dgDetalle.Items(i).Cells(19).Text)) > 0 And dgDetalle.Items(i).Cells(19).Text <> "&nbsp;" Then
                                    If Len(strCadenaSeries) > 0 Then
                                        strCadenaSeries = strCadenaSeries & "|"
                                    End If
                                    strCadenaSeries = strCadenaSeries & dgDetalle.Items(i).Cells(19).Text
                                End If

                                'Auditoria
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


                                wParam1 = Session("codUsuario")
                                wParam2 = Request.ServerVariables("REMOTE_ADDR")
                                wParam3 = Request.ServerVariables("SERVER_NAME")
                                wParam4 = ConfigurationSettings.AppSettings("gConstOpcVtaR")
                                wParam5 = 1
                                wParam6 = "Venta Rapida"
                                wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
                                wParam8 = ConfigurationSettings.AppSettings("gConstEvtVtaR")
                                wParam9 = Session("codPerfil")
                                wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
                                wParam11 = 1

                                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                                'Fin de auditoria

                            Next

                            'CC- Lisetti - Inicio Validacion de Claro Club Puntos
                            If Funciones.CheckDbl(Me.txtDescuentoCC.Value) > dblMonto Then
                                '**el descuento tiene que ser menor que el monto del pedido
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- El monto del descuento tiene que ser menor que el monto del pedido.")
                                blnError = True
                                Response.Write("<script>alert('El monto del descuento de Claro Club Puntos tiene que ser menor que el monto del pedido')</script>")
                                Exit Sub
                            End If
                            If Funciones.CheckDbl(Me.txtDescuentoCC.Value) > 0.0 And valEquipo = 0 Then
                                '**el descuento de Claro Club solo debe aplicar a equipos. 
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- El descuento de Claro Club solo debe aplicar a equipos.")
                                blnError = True
                                Response.Write("<script>alert('El descuento de Claro Club Puntos debe aplicarse a Equipos')</script>")
                                Exit Sub
                            End If
                            'CC- Lisetti - Fin Validacion de Claro Club Puntos

                            arrCabecera(9) = dblMonto
                            arrCabecera(10) = dblIGV
                            arrCabecera(11) = dblMonto + dblIGV
                           
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  dblMonto - " + Funciones.CheckStr(arrCabecera(9))) 'INC000001150298
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  dblIGV - " + Funciones.CheckStr(arrCabecera(10))) 'INC000001150298
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  dblMonto + dblIGV - " + Funciones.CheckStr(arrCabecera(11))) 'INC000001150298

                            arrCabecera(39) = ""  'TG para franquicias

                            'arrCabecera(48) = dsOficina.Tables(0).Rows(0).Item("VKORG")
                            'arrCabecera(49) = dsOficina.Tables(0).Rows(0).Item("VTWEG")
                            arrCabecera(48) = dsOficina.Tables(0).Rows(0).Item("PAOFV_ORGANIZACIONVENTAS")
                            arrCabecera(49) = dsOficina.Tables(0).Rows(0).Item("PAOFC_CANALDISTRIBUCION")
                            strCabecera = Join(arrCabecera, ";")


                            Dim listaNegraNC$ = ConfigurationSettings.AppSettings("ListaNegraNC").ToString
                            Dim index% = listaNegraNC.IndexOf(strTipDocVta)


                            If isRecargaVirtual And index = -1 Then
                                Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                                Dim objOffline As New COM_SIC_OffLine.clsOffline
                                Dim codigoCaja As String = Session("CodImprTicket")

                                Dim fkart = ConfigurationSettings.AppSettings("constClaseVoleta").ToString

                                ''Dim numeroReferenciaSunat$ = objPagos.ObtenerUltimoCorrelativoSunat(Session("ALMACEN"), fkart, codigoCaja)


                                'AGREGADO POR FFS INICIO
                                Session("numeroTelefono") = dgDetalle.Items(0).Cells(12).Text 'txtNTelf.Text
                                'AGREGADO POR FFS FIN

                                ''' VERIFICACION DE CUADRE DE CAJA 05.02.2014 TS-JTN

                                '''Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
                                If (objOffline.CuadreCajeroRealizado(Session("ALMACEN"), codUsuario)) Then
                                    Response.Write("<script>alert('Error: No puede realizar esta operacion, ya realizo cuadre de caja')</script>")
                                    Return
                                End If
                                ''' FIN VERIFICACION

                                Dim arrayCabecera(14) As String
                                Dim arrayDetalle(14) As String
                                Dim strServicio, strDesServicio, moneda, canal As String
                                Dim VTWEG, VKORG As String

                                strServicio = ConfigurationSettings.AppSettings("strCodArticuloDTH")
                                strDesServicio = ConfigurationSettings.AppSettings("strDesArticuloDTH")
                                'Dim tramaRecibos() As String = strRecibos.Value.Split(CChar(";"))

                                canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")

                                Dim codClienteVarios As String = ConfigurationSettings.AppSettings("COD_CLTE_VARIOS")
                                Dim defaulTipoDocCliente = ConfigurationSettings.AppSettings("TIPO_DOC_COD_CLTE_VARIOS")

                                arrayCabecera(0) = strTipDocVta '-->CLASE DOCUMENTO DE VENTAS v
                                arrayCabecera(1) = dsOficina.Tables(0).Rows(0).Item("PAOFV_ORGANIZACIONVENTAS") 'dsOficina.Tables(0).Rows(0).Item("VKORG") '-->ORGANIZACION DE VENTAS v
                                arrayCabecera(2) = dsOficina.Tables(0).Rows(0).Item("PAOFC_CANALDISTRIBUCION")  'dsOficina.Tables(0).Rows(0).Item("VTWEG") '-->CANAL MT
                                arrayCabecera(3) = "10" '-->SECTOR
                                arrayCabecera(4) = IIf(codClienteVarios = txtNumDocumento.Text, defaulTipoDocCliente, cboTipDocumento.SelectedValue) '"1" '-->TIPO NUMERO IDENTIFICACION FISCAL
                                arrayCabecera(5) = txtNumDocumento.Text '-->CODIGO CLIENTE SCI ?
                                arrayCabecera(6) = txtFechaPrecioVenta.Text '-->FECHA DOCUMENTO
                                arrayCabecera(7) = Session("ALMACEN") '-->OFICINA DE VENTA
                                arrayCabecera(8) = "0000000000000000" ''hidTextIdentificador.Value '-->NUMERO DE REFERENCIA
                                arrayCabecera(9) = cboSelectVend.SelectedValue().Substring(0, 10).ToString() '  Usuario    '-->NUMERO CLIENTE
                                arrayCabecera(10) = "PEN" '-->MONEDA
                                arrayCabecera(11) = "02" '-->TIPO VENTA
                                arrayCabecera(12) = "01" '-->CLASE VENTA
                                arrayCabecera(13) = "CLIENTE GENERICO" ' txtNombreCliente.Text
                                arrayCabecera(14) = Session("DireccionCliente")
                                ''Session("DireccionCliente")
                                Session("pagoConRuc") = (cboTipDocumento.SelectedValue = "6")

                                '****Valor de IGV ************************************************************'
                                Dim valorIGV As Double = constIGV  'PROY-31766'
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor IGV: " & valorIGV)
                                Dim precioUnitarioRecarga As Double = Convert.ToDouble(ConfigurationSettings.AppSettings("precioUnitarioRecarga")) '0.84
                                Dim subTotal#, total#, montoIgv#, x#

                                If Session("CodArtiRV") = ConfigurationSettings.AppSettings("MATERIAL_RV") Then  '**KEY  Recargas Virtual
                                    x = CDbl(dgDetalle.Items(0).Cells(3).Text)
                                    subTotal = x * precioUnitarioRecarga
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor del subTotal: " & subTotal) 'PROY-31766'
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Calculando monto con IGV: " & subTotal & "*" & valorIGV) 'PROY-31766'
                                    montoIgv = subTotal * valorIGV
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Monto con IGV: " & montoIgv) 'PROY-31766'
                                    total = subTotal + montoIgv
                                    preciorecarga = subTotal
                                Else
                                    Dim recEfectiva# = CDbl(dgDetalle.Items(0).Cells(3).Text)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor de la Recarga: " & recEfectiva) 'PROY-31766'
                                    Dim valVenta# = recEfectiva / (1 + Funciones.CheckDbl(constIGV)) 'PROY-28846'
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Calculando valor de la Venta: " & recEfectiva & "/ (1+" & valorIGV & ")") 'PROY-31766'
                                    valVenta = Math.Round(valVenta, 2)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor de la Venta: " & valVenta) 'PROY-31766'
                                End If
                                '*****************************************************************************'

                                Dim arrayPagos(13) As String
                                Dim viasPagoSap As String() = ConfigurationSettings.AppSettings("constCodigoViasSap").Split(CChar(";"))

                                canal = ConfigurationSettings.AppSettings("cteCODIGO_CANAL")

                                Dim newRow As DataRow = tbFacturacion.NewRow()

                                tbFacturacion.Columns.Add("FKART", GetType(String))
                                tbFacturacion.Columns.Add("FKDAT", GetType(String))
                                tbFacturacion.Columns.Add("VBELN", GetType(String))
                                tbFacturacion.Columns.Add("NAME1", GetType(String))
                                tbFacturacion.Columns.Add("TOTAL", GetType(Double))
                                tbFacturacion.Columns.Add("INICIAL", GetType(Int32))
                                tbFacturacion.Columns.Add("PAGOS", GetType(Int32))
                                tbFacturacion.Columns.Add("SALDO", GetType(Int32))
                                tbFacturacion.Columns.Add("PEDIDO", GetType(String))
                                tbFacturacion.Columns.Add("XBLNR", GetType(String))
                                tbFacturacion.Columns.Add("RECIBE_PAGO", GetType(String))
                                tbFacturacion.Columns.Add("NRO_DEP_GARANTIA", GetType(String))
                                tbFacturacion.Columns.Add("ES_VTA_BUSINESS", GetType(String))
                                tbFacturacion.Columns.Add("NRO_CONTRATO", GetType(String))

                                If cboTipDocumento.SelectedValue = "6" Then
                                    newRow("FKART") = ConfigurationSettings.AppSettings("constClaseFactura").ToString
                                Else
                                    newRow("FKART") = ConfigurationSettings.AppSettings("constClaseVoleta").ToString
                                End If

                                'newRow("FKART") = ConfigurationSettings.AppSettings("constClaseVoleta").ToString
                                newRow("FKDAT") = txtFechaPrecioVenta.Text
                                newRow("VBELN") = ""
                                newRow("NAME1") = "CLIENTE GENERICO"
                                newRow("TOTAL") = arrayDetalle(2) 'Convert.ToDouble(Request.Item("txtCant"))
                                newRow("INICIAL") = 0
                                newRow("PAGOS") = 0
                                newRow("SALDO") = 0
                                newRow("PEDIDO") = ""
                                newRow("XBLNR") = "0000000000000000"
                                newRow("RECIBE_PAGO") = "X"
                                newRow("NRO_DEP_GARANTIA") = "0000000000"
                                newRow("ES_VTA_BUSINESS") = ""
                                newRow("NRO_CONTRATO") = "0000000000"

                                tbFacturacion.Rows.Add(newRow)

                                idPedido = objOffline.CreaPedidoFactura("", "", Join(arrayCabecera, ";"), Join(arrayDetalle, ";"), Join(arrayPagos, ";"))
                            Else

                                Dim valorIGV As Double = constIGV 'PROY-31766'
                                objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor IGV: " & valorIGV)
                                Dim precioUnitarioRecarga As Double = Convert.ToDouble(ConfigurationSettings.AppSettings("precioUnitarioRecarga")) '0.84
                                Dim subTotal#, total#, montoIgv#, x#

                                '++x
                                If Session("CodArtiRV") = ConfigurationSettings.AppSettings("MATERIAL_RV") Then   '**KEY  Recargas Virtual
                                    subTotal = dblMonto * precioUnitarioRecarga
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Valor del subTotal: " & subTotal) 'PROY-31766'
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Calculando monto con IGV: " & subTotal & "*" & valorIGV) 'PROY-31766'
                                    montoIgv = subTotal * valorIGV
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, "Monto con IGV: " & montoIgv) 'PROY-31766'
                                    total = subTotal + montoIgv
                                    preciorecarga = subTotal
                                    Session("recargaVirtual") = True
                                End If

                                '*API
                                strTipoMaterial = Session("strTipoMaterialVR")
                                str_pedic_clasepedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_RV")
                                If strTipoMaterial.Trim.ToString.Length >= 0 Then
                                    If (strTipoMaterial.Trim.ToString = System.Configuration.ConfigurationSettings.AppSettings("constS1") Or _
                                    strTipoMaterial.Trim.ToString = System.Configuration.ConfigurationSettings.AppSettings("constS2") Or strTipoMaterial.ToString.Trim.Substring(0, 2) = "TS") Then
                                        str_pedic_tipo_documento = ConfigurationSettings.AppSettings("TIPO_DOC_SERVI")
                                        str_pedic_clasepedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEPEDIDO_TS")
                                    Else
                                        str_pedic_tipo_documento = ConfigurationSettings.AppSettings("TIPO_DOC_PVU_VR")
                                    End If
                                Else
                                    str_pedic_tipo_documento = ConfigurationSettings.AppSettings("TIPO_DOC_PVU_VR")
                                End If

                                'sector_Configurable

                                If Funciones.CheckStr(strTipoMaterial.Trim) = System.Configuration.ConfigurationSettings.AppSettings("constPacks") Then
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("Pedic_Sector_Equipos"))
                                ElseIf Funciones.CheckStr(strTipoMaterial.Trim) = System.Configuration.ConfigurationSettings.AppSettings("constChips") Then
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("Pedic_Sector_Chips"))
                                ElseIf Funciones.CheckStr(strTipoMaterial.Trim) = System.Configuration.ConfigurationSettings.AppSettings("const_Merchandising") And _
                                  strClasePedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CLASEFACTURA") Then
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("Pedic_Sector_Merchandising"))
                                ElseIf Funciones.CheckStr(strTipoMaterial.Trim) = System.Configuration.ConfigurationSettings.AppSettings("constS1") Then
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("PEDIC_SECTOR"))
                                ElseIf Funciones.CheckStr(strTipoMaterial.Trim) = System.Configuration.ConfigurationSettings.AppSettings("constTarjetas") Then
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("Pedic_Sector_Tarjetas"))
                                Else
                                    sector_Configurable = Funciones.CheckStr(System.Configuration.ConfigurationSettings.AppSettings("Pedic_Sector_Default"))
                                End If

                                '***** GUARDAR VENTA EN PVU *********************
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Inicio de registro de venta en PVU")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -       INICIO RegistrarVenta -  SP: sisact_pkg_acuerdo_6.sp_reg_venta") 'INC000001150298
                           
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     strClasePedido - " + Funciones.CheckStr(strClasePedido)) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     CANAL_PVU - " + Funciones.CheckStr(ConfigurationSettings.AppSettings("CANAL_PVU"))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     ALMACEN - " + Funciones.CheckStr(Session("ALMACEN"))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     cboTipDocumento.SelectedValue -" + Funciones.CheckStr(cboTipDocumento.SelectedValue)) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     txtNumDocumento.Text - " + Funciones.CheckStr(txtNumDocumento.Text)) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     MONEDA_PVU - " + Funciones.CheckStr(ConfigurationSettings.AppSettings("MONEDA_PVU"))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     TOPEN_CODIGO - " + Funciones.CheckStr(ConfigurationSettings.AppSettings("TOPEN_CODIGO"))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     dblMonto + dblIGV - " + Funciones.CheckStr(arrCabecera(11))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     dblIGV - " + Funciones.CheckStr(arrCabecera(10))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     dblMonto - " + Funciones.CheckStr(arrCabecera(9))) 'INC000001150298
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     cboVPlazo.SelectedValue - " + Funciones.CheckStr(cboVPlazo.SelectedValue)) 'INC000001150298

                                '**aix
                                Try
                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/									                                    
                                    'Consultamos los parametros de configuracion correspondientes a Tarjeta Fisica y Recarga Virtual
                                    Dim vTopenCodigoTF As String
                                    Dim vTopenCodigoRV As String  ' = Funciones.CheckStr(dsCodigos.Tables(0).Rows(i).Item("PARAV_VALOR")).Split("|")(1)
                                    Dim codGrupo As Integer = Funciones.CheckDbl(ConfigurationSettings.AppSettings("constGrupoParam_ListaOperacion"))
                                    Dim dsCodigos As DataSet = (New COM_SIC_Cajas.clsCajas).ObtenerParamByGrupo(codGrupo)

                                    'Leemos los parametros de configuracion correspondientes a los tipos de ventas de TF y RV
                                    If Not IsNothing(dsCodigos) Then
                                        For idx As Integer = 0 To dsCodigos.Tables(0).Rows.Count - 1
                                            If Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR1")) = "RECARGA VIRTUAL" Then
                                                vTopenCodigoRV = Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR"))
                                            ElseIf Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR1")) = "TARJETA FISICA" Then
                                                vTopenCodigoTF = Funciones.CheckStr(dsCodigos.Tables(0).Rows(idx).Item("PARAV_VALOR"))
                                            End If
                                        Next
                                    End If

                                    isVentaTarjetaFisica = cboArti.SelectedValue.IndexOf(Funciones.CheckStr(ConfigurationSettings.AppSettings("constTarjetas"))) >= 0
                                    isVentaRecargaVirtual = Session("CodArtiRV").IndexOf(Funciones.CheckStr(ConfigurationSettings.AppSettings("MATERIAL_RV"))) >= 0

                                    If isVentaTarjetaFisica Then
                                        v_Topen_Codigo = vTopenCodigoTF 'ConfigurationSettings.AppSettings("TOPEN_CODIGO_TF")
                                        'v_Tipo_Producto = ConfigurationSettings.AppSettings("constTipoProductoVentasVarias")
                                    ElseIf isVentaRecargaVirtual Then
                                        v_Topen_Codigo = vTopenCodigoRV 'ConfigurationSettings.AppSettings("TOPEN_CODIGO_RV")
                                        'v_Tipo_Producto = ConfigurationSettings.AppSettings("constTipoProductoVentasVarias")
                                    Else
                                        v_Topen_Codigo = ConfigurationSettings.AppSettings("TOPEN_CODIGO")
                                        'v_Tipo_Producto = "01"
                                    End If

                                    '/*----------------------------------------------------------------------------------------------------------------*/
                                    '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                    '/*----------------------------------------------------------------------------------------------------------------*/


                                    objTrsPvu.RegistrarVenta(strClasePedido, _
                                                             Funciones.CheckStr(ConfigurationSettings.AppSettings("CANAL_PVU")), _
                                                             Funciones.CheckStr(Session("ALMACEN")), _
                                                             Funciones.CheckStr(cboTipDocumento.SelectedValue).ToString.Trim, _
                                                             Funciones.CheckStr(txtNumDocumento.Text), _
                                                             Funciones.CheckStr(ConfigurationSettings.AppSettings("MONEDA_PVU")), _
                                                             v_Topen_Codigo, _
                                                             Funciones.CheckDbl(arrCabecera(11)), _
                                                             Funciones.CheckDbl(arrCabecera(10)), _
                                                             Funciones.CheckDbl(arrCabecera(9)), _
                                                             "", _
                                                             Funciones.CheckStr(ConfigurationSettings.AppSettings("TVEN_CODIGO")), _
                                                             "", _
                                                             "00000" & Funciones.CheckStr(Session("USUARIO")), _
                                                             IIf(cboVPlazo.SelectedValue = "", "00", Funciones.CheckStr(cboVPlazo.SelectedValue)), _
                                                             Funciones.CheckStr(cboSelectVend.SelectedValue), _
                                                             Funciones.CheckStr(arrCabecera(48)), 0, 0, _
                                                             p_msgerr, p_documento)
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Error al registrar la venta en pvu.")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Err." & ex.Message.ToString)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Err." & ex.StackTrace.ToString)
                                End Try


                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Valor de retorno:")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- Param(p_documento) =>  " & IIf(p_documento.ToString = "", "Error, no registro", p_documento.ToString))
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -       FIN RegistrarVenta -  SP: sp_reg_venta")

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     strClasePedido - " + Funciones.CheckStr(strClasePedido))      'INC000001150298


                                Try
                                    If p_documento > 0 Then

                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -       INICIO RegistrarVentaDetalle -  SP: sp_reg_venta_detalle")

                                        For i = 0 To dgDetalle.Items.Count - 1
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_correlativo - " + Funciones.CheckStr(Funciones.CheckInt64(i + 1)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_documento - " + Funciones.CheckStr(p_documento))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_material - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(2).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_material_desc - " + Funciones.CheckStr(Trim(Right(dgDetalle.Items(i).Cells(6).Text, Len(dgDetalle.Items(i).Cells(6).Text)))))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_plan - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(16).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_plan_desc - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(11).Text()))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_telefono - " + Funciones.CheckStr(txtNTelf.Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_campana - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(15).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_campana_desc - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(10).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_cantidad - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(3).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_precio - " + Funciones.CheckStr(arrDetalle(10)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_subtotal - " + Funciones.CheckStr(arrDetalle(10)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_igv - " + Funciones.CheckStr(arrDetalle(16)))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_total - " + Funciones.CheckStr(Funciones.CheckDbl(arrDetalle(10)) + Funciones.CheckDbl(arrDetalle(16))))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_lista_precio - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(14).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_lista_precio_desc - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(4).Text))
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  IN     p_imei19 - " + Funciones.CheckStr(dgDetalle.Items(i).Cells(5).Text))
                                         
                                            '/*----------------------------------------------------------------------------------------------------------------*/
                                            '/*--JR  INICIO -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                            '/*----------------------------------------------------------------------------------------------------------------*/
                                            If isVentaRecargaVirtual Then
                                                nroTelefonoVentaRapida = arrDetalle(25)
                                            Else
                                                nroTelefonoVentaRapida = Funciones.CheckStr(txtNTelf.Text)
                                            End If

                                            'verificamos que No sea una Venta por Lotes para registrar la venta en PVU
                                            Dim flagVentaLote As Int32 = 0
                                            If Funciones.CheckInt(dgDetalle.Items(i).Cells(3).Text) > 1 Then
                                                flagVentaLote = 1
                                            End If

                                            If (isVentaTarjetaFisica And flagVentaLote = 1) Or (isVentaRecargaVirtual And flagVentaLote = 1) Then
                                                'No se grabara el detalle de esta venta en sisact_ap_venta_detalle
                                            Else
                                            objTrsPvu.RegistrarVentaDetalle(Funciones.CheckInt64(i + 1), _
                                                     Funciones.CheckInt64(p_documento), _
                                                     Funciones.CheckStr(dgDetalle.Items(i).Cells(2).Text), _
                                                     Server.HtmlDecode(Trim(Right(dgDetalle.Items(i).Cells(6).Text, Len(dgDetalle.Items(i).Cells(6).Text)))), _
                                                     Funciones.CheckStr(dgDetalle.Items(i).Cells(16).Text), _
                                                     Server.HtmlDecode(dgDetalle.Items(i).Cells(11).Text), _
                                                     nroTelefonoVentaRapida, _
                                                     Funciones.CheckStr(dgDetalle.Items(i).Cells(15).Text), _
                                                     Server.HtmlDecode(dgDetalle.Items(i).Cells(10).Text), _
                                                     Funciones.CheckStr(dgDetalle.Items(i).Cells(3).Text), _
                                                     Funciones.CheckDbl(arrDetalle(10)), _
                                                     Funciones.CheckDbl("0"), _
                                                     Funciones.CheckDbl(arrDetalle(10)), _
                                                     Funciones.CheckDbl(arrDetalle(16)), _
                                                     Funciones.CheckDbl(arrDetalle(11)), _
                                                     Funciones.CheckStr(dgDetalle.Items(i).Cells(14).Text), _
                                                     Server.HtmlDecode(dgDetalle.Items(i).Cells(4).Text), _
                                                     IIf(dgDetalle.Items(i).Cells(5).Text = "&nbsp;", "", Funciones.CheckStr(dgDetalle.Items(i).Cells(5).Text)), _
                                                      IIf(cboVPlazo.SelectedValue = "", "00", Funciones.CheckStr(cboVPlazo.SelectedValue)), "", _
                                                     "01", _
                                                     p_documento, p_msgerr)
                                              objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ------------------------------------------------------------------------------------------")
                                            End If
                                            '/*----------------------------------------------------------------------------------------------------------------*/
                                            '/*--JR  FIN     -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                            '/*----------------------------------------------------------------------------------------------------------------*/
                                        Next
                                    Else
                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      No se logro registrar la venta : RegistrarVenta(PVU.TABLA: sisact_ap_venta)")
                                    End If
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -       Error en el registro de venta en PVU")
                                    tdBtnGrabar.Visible = False
                                    dtDetalle.Clear()
                                    dgDetalle.DataSource = dtDetalle
                                    dgDetalle.DataBind()
                                    Exit Sub
                                End Try

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -       FIN RegistrarVentaDetalle -  SP: sp_reg_venta_detalle")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - Fin de registro de venta en PVU")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ---------------------------------------")

                                '** DETERMINAMOS LOS ESQUEMAS DE CÀLCULO **'
                                '** Verificar si existe un procedure que consulte si el material tiene series, para identificar el tipo de esquema
                                Dim Motivo_Pedido As String = ""
                                Try
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      INICIO ESQUEMA DE CALCULO.")
                                    'strTipoMaterial = cboArti.SelectedValue
                                    If strTipoMaterial.Trim.ToString.Length >= 0 Then
                                        If (strTipoMaterial.Trim.ToString = System.Configuration.ConfigurationSettings.AppSettings("constTarjetas") Or _
                                           strTipoMaterial.Trim.ToString = System.Configuration.ConfigurationSettings.AppSettings("constS1") Or _
                                           strTipoMaterial.Trim.ToString = System.Configuration.ConfigurationSettings.AppSettings("constS2") Or _
                                           strTipoMaterial.Trim.ToString.Substring(0, 2) = "TS") Then
                                            '** ESQUEMA DE CALCULO NO SERIADO **'
                                            strEsquemaCalculo = System.Configuration.ConfigurationSettings.AppSettings("ESQUEMACALCULO_RV")     'Para materiales que no tienen serie.
                                            Motivo_Pedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO_RV")
                                        Else
                                            strEsquemaCalculo = System.Configuration.ConfigurationSettings.AppSettings("ESQUEMACALCULO_VR")
                                            Motivo_Pedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO")
                                        End If
                                    Else
                                        strEsquemaCalculo = System.Configuration.ConfigurationSettings.AppSettings("ESQUEMACALCULO_VR")
                                        Motivo_Pedido = System.Configuration.ConfigurationSettings.AppSettings("PEDIV_MOTIVOPEDIDO")
                                    End If
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Esquema seleccionado: " & strEsquemaCalculo.ToString)
                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Error al determinar el tipo de esquema de càlculo.")
                                End Try
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      FIN ESQUEMA DE CALCULO.")
                                '*****************************************************************************************************************************************************
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  Verificar tipo y nombre de documento que se guarda")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  strClasePedido: " & strClasePedido)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  strDescrClasePedido: " & strDescrClasePedido)
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -  Motivo_Pedido: " & Motivo_Pedido)

                                '******GUARDAR PEDIDO : MSSAP6 *******************
                                'REG PED:
                                Try
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ----------------------------------------")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - INICIO de registro de venta en MSINCDB")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ----------------------------------------")

                                    Dim tipoCliec As String = Funciones.CheckStr(arrCliente(61))
                                    If tipoCliec = "" Then
                                        tipoCliec = "01"
                                        'IIf(IsDBNull(arrCliente(61)), "01", Funciones.CheckStr(arrCliente(61)))
                                    End If

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      INICIO RegistrarPedido SP: SSAPSI_PEDIDO")

                                    objTrsMsSap.RegistrarPedido(ConsultaPuntoVenta(Session("ALMACEN")), _
                                                                 DBNull.Value, _
                                                                 str_pedic_tipo_documento, _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PEDIC_ORGVENTA"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PEDIC_CANALVENTA"), _
                                                                 sector_Configurable, _
                                                                 Funciones.CheckStr(arrCabecera(4)), _
                                                                 DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                 Motivo_Pedido, _
                                                                 strClasePedido, _
                                                                 strDescrClasePedido, _
                                                                 "", _
                                                                 str_pedic_clasepedido, _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("COD_TIPO_OPERACION"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PEDIV_DESCTIPOOPERACION_VR"), _
                                                                 DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                 "00000" & Funciones.CheckStr(Session("USUARIO")), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PEDIC_ESTADO"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("ISRENTA"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PEDIDO_ALTA"), _
                                                                 Funciones.CheckStr(Session("Ubigeo_legal")), _
                                                                 Funciones.CheckStr(strEsquemaCalculo.ToString), _
                                                                 Funciones.CheckStr(arrCliente(1)), _
                                                                 Funciones.CheckStr(arrCliente(0)), _
                                                                 tipoCliec, _
                                                                 Funciones.CheckStr(arrCliente(2)), _
                                                                 Funciones.CheckStr(arrCliente(3)), _
                                                                 Funciones.CheckStr(arrCliente(4)), _
                                                                 IIf(arrCliente(6) = "", DBNull.Value, arrCliente(6)), _
                                                                 Funciones.CheckStr(arrCliente(5)), _
                                                                 Funciones.CheckStr(arrCliente(8)), "", "", _
                                                                 Funciones.CheckStr(arrCliente(17)), _
                                                                 0, _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("DISTRITO_CLIENTE"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("CODDPTO_CLIENTE"), _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("PAIS_CLIENTE_GENERICO"), _
                                                                 "", "", "", "", "", _
                                                                 System.Configuration.ConfigurationSettings.AppSettings("tipo_oficina"), _
                                                                 DBNull.Value, Session("USUARIO"), _
                                                                 DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                 "00000" & Funciones.CheckStr(Session("USUARIO")), _
                                                                 DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                 "0", K_NROINTERNO_PEDIDO, K_NRO_ERROR, K_DES_ERROR)

                                Catch ex As Exception
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Error en el registro de venta en MSSAP")
                                    tdBtnGrabar.Visible = False
                                    dtDetalle.Clear()
                                    dgDetalle.DataSource = dtDetalle
                                    dgDetalle.DataBind()
                                    Response.Write("<script>alert('Ocurrio un error al registrar la Cabecera del del pedido ')</script>")
                                    Exit Sub
                                End Try

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      FIN RegistrarPedido SP: SSAPSI_PEDIDO")

                                If K_NROINTERNO_PEDIDO > 0 Then

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "        OUT NRO PEDIDO: " & K_NROINTERNO_PEDIDO)

                                    strFactura = Funciones.CheckStr(K_NROINTERNO_PEDIDO)
                                    If (strDetalle.Length > 0) Then

                                        K_NRO_ERROR = 0
                                        K_DES_ERROR = ""

                                        '**RECORRER LA GRILLA:
                                        'strFactura = K_NROINTERNO_PEDIDO        '** Para no modificar esta variable:
                                        For i = 0 To dgDetalle.Items.Count - 1
                                            Try
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      INICIO RegistraDetallePedido SP: SSAPSI_DETALLEPEDIDO")

                                                objTrsMsSap.RegistraDetallePedido(Funciones.CheckInt64(K_NROINTERNO_PEDIDO), _
                                                                ConsultaPuntoVenta(Session("ALMACEN")), _
                                                                "", _
                                                                IIf(dgDetalle.Items(i).Cells(18).Text = "TV0005", "", IIf(dgDetalle.Items(i).Cells(5).Text = "&nbsp;", "", dgDetalle.Items(i).Cells(5).Text)), _
                                                                dgDetalle.Items(i).Cells(2).Text, _
                                                                Server.HtmlDecode(Trim(Right(dgDetalle.Items(i).Cells(6).Text, Len(dgDetalle.Items(i).Cells(6).Text)))), _
                                                                Funciones.CheckInt(dgDetalle.Items(i).Cells(3).Text), _
                                                                IIf(preciorecarga > 0, preciorecarga, Funciones.CheckDbl(dgDetalle.Items(i).Cells(7).Text)), _
                                                                Funciones.CheckStr(IIf(dgDetalle.Items(i).Cells(12).Text = "&nbsp;", "", dgDetalle.Items(i).Cells(12).Text)), _
                                                                "", 0, 0, _
                                                                IIf(cboVPlazo.SelectedValue = "", 0, Funciones.CheckInt(cboVPlazo.SelectedValue)), _
                                                                dgDetalle.Items(i).Cells(14).Text, _
                                                                dgDetalle.Items(i).Cells(4).Text, _
                                                                Funciones.CheckStr(Session("USUARIO")), _
                                                                DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                Funciones.CheckStr(Session("USUARIO")), _
                                                                DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), _
                                                                K_NRO_ERROR, K_DES_ERROR)
                                            Catch ex As Exception
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_NROINTERNO_PEDIDO) & "- Error en el registro de venta en MSSAP")
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, Funciones.CheckStr(K_NROINTERNO_PEDIDO) & "- Mensaje: " & ex.Message.ToString())
                                                tdBtnGrabar.Visible = False
                                                dtDetalle.Clear()
                                                dgDetalle.DataSource = dtDetalle
                                                dgDetalle.DataBind()
                                                Response.Write("<script>alert('Ocurrio un error al registrar el detalle del del pedido ')</script>")
                                                Exit Sub
                                            End Try

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      FIN RegistraDetallePedido SP: SSAPSI_DETALLEPEDIDO")

                                            '**Actualizamos la serie: 
                                            If K_DES_ERROR <> "OK" Then
                                                '**LOG DEL REGISTRO ***'
                                                Response.Write("<script>alert('Ocurrio un error al registrar el detalle del del pedido ')</script>")
                                                tdBtnGrabar.Visible = False
                                                dtDetalle.Clear()
                                                dgDetalle.DataSource = dtDetalle
                                                dgDetalle.DataBind()
                                                Exit Sub
                                            End If
                                        Next

                                        '************************* Inicio Actualiza el Ajuste de redondeo***********************
                                        Try
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      INICIO AJUSTE DE REDONDEO")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -          IN NRO PEDIDO: " & K_NROINTERNO_PEDIDO)
                                            objTrsMsSap.ActualizarAjusteRedondeo(K_NROINTERNO_PEDIDO)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      FIN AJUSTE DE REDONDEO")
                                        Catch ex As Exception
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Error al Actualiza el Ajuste del redondeo en el detalle del pedido de la VR - Venta Rapida => " & K_NROINTERNO_PEDIDO)
                                        End Try

                                        '************************* Fin Actualiza el Ajuste de redondeo***********************

                                    End If  '** FIN TAMAÑO DEL GRID **'
                                    '
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ----------------------------------------")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - FIN de registro de venta en MSINCDB")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " ----------------------------------------")

                                    '***** REGISTRO EN LA SISACT_INFO_VENTA_SAP **************************************************************************************************************************************************************'   
                                    '** TIPO_DOC_INFO_VENTA=F **
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      INICIO GRABAR VENTA EN SISACT")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -          IN:[ID VENTA: " & Funciones.CheckInt64(p_documento) & "][NRO DOC: " & Funciones.CheckStr(K_NROINTERNO_PEDIDO) & "][TIPO VENTA: " & ConfigurationSettings.AppSettings("TIPO_DOC_INFO_VENTA") & "][MONTO: " & Funciones.CheckDbl(dblMonto) & "]")
                                    objTrsPvu.GrabarInfoVentaSap("", "", _
                                                                    Funciones.CheckInt64(p_documento), _
                                                                    Funciones.CheckStr(K_NROINTERNO_PEDIDO), _
                                                                    ConfigurationSettings.AppSettings("TIPO_DOC_INFO_VENTA"), _
                                                                    Funciones.CheckDbl(dblMonto))
                                    '*********************************************************************************************************************************************************************************************************'
                                Else
                                    '** Error al registrar el pedido ***'
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      Ocurrio un error al registrar el pedido.")
                                    blnError = True
                                End If
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " -      FIN GRABAR VENTA EN SISACT")
                                '********'CC- Inicio Lisetti Macedo
                                If Not blnError Then
                                    Dim nroPedido As String = Funciones.CheckStr(K_NROINTERNO_PEDIDO)
                                    If Funciones.CheckDbl(txtDescuentoCC.Value) > 0 Then
                                        Try

                                            ' Hacer Reserva Puntos CC
                                            bloquearPuntos()
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Inicio GenerarNotaCreditoCC()")


                                            ' factura = nro doc SAP, de oConsultaSap.RealizarPedido().
                                            Dim montoDescuento As Decimal = Decimal.Parse(Me.txtDescuentoCC.Value)
                                            Dim nroLog, desLog As String
                                            objTrsMsSap.ActualizarDescuentoPedido(nroPedido, ConfigurationSettings.AppSettings("constSINERGIAEsquemaRenovacion"), _
                                            ConfigurationSettings.AppSettings("constSINERGIAClaseCondicion53"), montoDescuento, nroLog, desLog)

                                            'GenerarNotaCreditoCC(factura, _
                                            '                     documentoSAPNotaCredito, _
                                            '                     documentoSAPNotaCreditoCC)
                                            GeneroDsct += 1
                                        Catch ex As Exception
                                            'EliminarPedidoSISACT(factura)
                                            'getCCLUB(cboTipDocumento.SelectedValue)
                                            LiberarPuntosCC(nroPedido, cboTipDocumento.SelectedValue, Me.txtNumDocumento.Text)
                                            EliminarCanjePuntos(nroPedido)
                                            'Throw New Exception(ex.Message)
                                            'Throw New Exception("Cuenta(s) del Cliente Bloqueada, No Podrá realizar Canjes hasta que sea Liberada.Existe un canje en proceso.")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Exception GenerarNotaCreditoCC() - Mensaje: " & ex.Message.ToString())
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " Exception GenerarNotaCreditoCC() - Mensaje: " & ex.StackTrace.ToString())
                                            Response.Write("<script>alert('Cuenta(s) del Cliente Bloqueada, No Podrá realizar Canjes hasta que sea Liberada.Existe un canje en proceso.')</script>")
                                            Exit Sub
                                        Finally
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Fin GenerarNotaCreditoCC()")
                                        End Try
                                    End If
                                    'Recalculo de los descuent
                                    If (GeneroDsct > 0) Then
                                        Try
                                            Dim pnrolog, pdeslog As String
                                            For i = 0 To dgDetalle.Items.Count - 1
                                                If dgDetalle.Items(i).Cells(18).Text = ConfigurationSettings.AppSettings("constPacks") Then
                                                    objTrsMsSap.RecalculaEsquema(nroPedido, i + 1, ConfigurationSettings.AppSettings("constSINERGIAEsquemaRenovacion"), pnrolog, pdeslog)
                                                End If
                                            Next
                                            If (pnrolog <> "0") Then
                                                Throw New Exception("No se pudo generar el recalculo esquema en MsSap.")
                                            End If
                                            objTrsMsSap.RecalculaDescuento(nroPedido, ConfigurationSettings.AppSettings("constSINERGIAEsquemaRenovacion"), pnrolog, pdeslog)
                                            If (pnrolog <> "0") Then
                                                Throw New Exception("No se pudo generar el recalculo descuento en MsSap.")
                                            End If
                                        Catch ex As Exception
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Exception RecalculaEsquema() and RecalculaDescuento() - Mensaje: " & ex.Message)
                                            Throw ex
                                        Finally
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Fin RecalculaEsquema() and RecalculaDescuento()")
                                        End Try
                                    End If
                                End If
                                'CC-  Fin Lisetti Macedo***************************************************************************************************************************************************

                                If Not blnError Then
                                    intOper = objCajas.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("CodAplicacion"), Session("USUARIO"), _
                                    cboTipDocumento.SelectedValue, txtNumDocumento.Text, strTipDocVta, strFactura, "", dblMonto, dblIGV, (dblMonto + dblIGV), "V")

                                    For i = 1 To dgDetalle.Items.Count
                                        objCajas.FP_Det_Oper(intOper, i, dgDetalle.Items(i - 1).Cells(2).Text, IIf(dgDetalle.Items(i - 1).Cells(5).Text = "&nbsp;", "", dgDetalle.Items(i - 1).Cells(5).Text), dgDetalle.Items(i - 1).Cells(12).Text, CInt(dgDetalle.Items(i - 1).Cells(3).Text), CDbl(dgDetalle.Items(i - 1).Cells(7).Text), CDbl(dgDetalle.Items(i - 1).Cells(13).Text), CDbl(dgDetalle.Items(i - 1).Cells(7).Text) + CDbl(dgDetalle.Items(i - 1).Cells(13).Text))
                                    Next

                                End If
                            End If

                            If Not blnError Then   'El pedido se ha creado sin problemas
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                '/*--JR  INICIO  -  PROY-29380 IDEA-38934 - Iteración 2 y 3 2da Fase Sinergia Adec en Sist de Venta y Post-Venta --*/
                                '/*----------------------------------------------------------------------------------------------------------------*/
                                If isVentaRecargaVirtual Then
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

                                If isRecargaVirtual And index = -1 Then
                                    drFila = tbFacturacion.Rows(0)
                                Else
                                    '**consulta todos los documentos:
                                    'dsResultado = objPagos.Get_ConsultaPoolFactura(Session("ALMACEN"), txtFechaPrecioVenta.Text, "I", "", txtNumDocumento.Text, cboTipDocumento.SelectedValue, "", "1")
                                    dvFiltro.Table = clsConsultaMssap.ConsultaPoolPagos(ConfigurationSettings.AppSettings("PEDIC_ESTADO"), DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture), ConsultaPuntoVenta(Session("ALMACEN")), "")     'txtFecha.Text, "0001")

                                    'dvFiltro.Table = dsResultado.Tables(0)
                                    'dvFiltro.RowFilter = "VBELN=" & strFactura
                                    dvFiltro.RowFilter = "PEDIN_NROPEDIDO=" & strFactura
                                    drFila = dvFiltro.Item(0).Row

                                    If isRecargaVirtua2 Then
                                        Session("recargaVirtual") = True
                                    Else
                                        Session("recargaVirtual") = False
                                    End If
                                End If
                                Session("DocSel") = drFila
                                'Session("numeroTelefono") = txtNTelf.Text
                                Session("VentaR") = "1"
                                Session("detVenta") = Nothing
                                Session("NC") = (index > 0)
                                'TS-LGZ *** INICIO

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "=============================================")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "             FIN GRABAR PEDIDO            ")
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "=============================================")

                                'Response.Redirect("detaPago.aspx?pDocSap=" & idPedido & "&numeroTelefono=" & dgDetalle.Items(0).Cells(12).Text & "&montoRecarga=" & drFila("TOTAL"))
                                Response.Redirect("detaPago.aspx?pDocSap=" & Funciones.CheckInt64(K_NROINTERNO_PEDIDO) & "&numeroTelefono=" & dgDetalle.Items(0).Cells(12).Text & "&montoRecarga=" & drFila("INPAN_TOTALDOCUMENTO"))

                                'TS-LGZ *** FINAL
                            Else
                                Response.Write("<script>alert('Ocurrio un error al registrar la Cabecera del del pedido ')</script>")
                                tdBtnGrabar.Visible = False
                                dtDetalle.Clear()
                                dgDetalle.DataSource = dtDetalle
                                dgDetalle.DataBind()
                            End If
                        End If
                    End If
                End If
            End If
        Else
            '    '   Session("detVenta") = dgDetalle.DataSource  '.Items
            dtDetalle.Rows.Clear()
            For i = 0 To dgDetalle.Items.Count - 1
                drFila = dtDetalle.NewRow()
                For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                    drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
                Next
                dtDetalle.Rows.Add(drFila)
            Next
            Session("detVenta") = dtDetalle
            Response.Redirect("datosCliente.aspx?strTipDoc=" & cboTipDocumento.SelectedValue & "&strNumDoc=" & txtNumDocumento.Text)
        End If  'buscacliente
    End Sub

    '** Consulta los datos del Cliente -  Fuentes PVU : MSSAP **'
    Private Sub ConsultarDatosCliente()
        Try
            Dim CLIEV_NRO_DOCUMENTO As String = ""
            Dim CLIEC_TIPO_DOCUMENTO As String = ""
            Dim CLIEV_NOMBRE As String = ""
            Dim CLIEV_APELLIDO_PATERNO As String = ""
            Dim CLIEV_APELLIDO_MATERNO As String = ""
            Dim CLIEV_RAZON_SOCIAL As String = ""
            Dim CLIED_FECHA_NACIMIENTO As String
            Dim CLIEV_E_MAIL As String

            dsCliente = clsConsultaPvu.consultaDatosCliente(cboTipDocumento.SelectedValue, txtNumDocumento.Text, k_nrolog, k_deslog)
            If dsCliente.Tables(0).Rows.Count > 0 Then
                CLIEV_NRO_DOCUMENTO = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_NRO_DOCUMENTO")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_NRO_DOCUMENTO"))
                CLIEC_TIPO_DOCUMENTO = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEC_TIPO_DOCUMENTO")), "", dsCliente.Tables(0).Rows(0).Item("CLIEC_TIPO_DOCUMENTO"))
                CLIEV_NOMBRE = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_NOMBRE")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_NOMBRE"))
                CLIEV_APELLIDO_PATERNO = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_APELLIDO_PATERNO")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_APELLIDO_PATERNO"))
                CLIEV_APELLIDO_MATERNO = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_APELLIDO_MATERNO")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_APELLIDO_MATERNO"))
                CLIEV_RAZON_SOCIAL = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_RAZON_SOCIAL")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_RAZON_SOCIAL"))
                CLIED_FECHA_NACIMIENTO = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIED_FECHA_NACIMIENTO")), "", dsCliente.Tables(0).Rows(0).Item("CLIED_FECHA_NACIMIENTO"))
                CLIEV_E_MAIL = IIf(IsDBNull(dsCliente.Tables(0).Rows(0).Item("CLIEV_E_MAIL")), "", dsCliente.Tables(0).Rows(0).Item("CLIEV_E_MAIL"))
            Else
                '**Buscamos en MSSAP **'

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnAgregarGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarGrabar.Click
        If Request.Item("hidVerif") = "1" Then
            If txtCodArti.Text.Trim = ConfigurationSettings.AppSettings("strCodArticuloRenovacion") Then
                ConsultaFecha()
            End If
            AgregaLinea()
            If Not blnError Then
                GrabarPedido()
            End If
            cboCamp.SelectedIndex = 0
        End If
    End Sub

    Private Sub dgDetalle_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDetalle.ItemCommand
        Dim drFila As DataRow
        Dim i, j As Integer
        Dim strPos As String
        Dim strSerie As String
        Dim arrSerie() As String
        Dim arrLinSerie() As String
        Dim strLinSerie As String

        For i = 0 To dgDetalle.Items.Count - 1
            drFila = dtDetalle.NewRow()
            For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
            Next
            dtDetalle.Rows.Add(drFila)
        Next

        '*** ACTUALIZAR ESTADO SERIE **********************************************************************************'
        Try
            If dtDetalle.Rows.Count > 0 Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicia liberaciòn de Serie")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "La serie a liberar es: " & Funciones.CheckStr(dtDetalle.Rows(e.Item.ItemIndex).Item(4)))
                clsConsultaMssap.CambiarEstadoSerie(Funciones.CheckStr(dtDetalle.Rows(e.Item.ItemIndex).Item(4)), ConfigurationSettings.AppSettings("ESTADO_SERIE_LIBERA"), "")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "La serie fue liberada correctamente: " & Funciones.CheckStr(dtDetalle.Rows(e.Item.ItemIndex).Item(4)))
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Ocurrio un error al tratar de liberar la serie: " & Funciones.CheckStr(dtDetalle.Rows(e.Item.ItemIndex).Item(4)))
        End Try
        '**************************************************************************************************************'
        dtDetalle.Rows.RemoveAt(e.Item.ItemIndex)

        For i = 0 To dtDetalle.Rows.Count - 1
            dtDetalle.Rows(i).Item(0) = Format(i + 1, "000000")
            strSerie = dtDetalle.Rows(i).Item(18)
            If Len(Trim(strSerie)) > 0 And Trim(strSerie) <> "&nbsp;" Then
                arrSerie = Split(strSerie, "|")
                For j = 0 To UBound(arrSerie)
                    arrLinSerie = Split(arrSerie(j), ";")
                    arrLinSerie(1) = Format(i + 1, "000000")
                    strLinSerie = strLinSerie & Join(arrLinSerie, ";") & "|"
                Next
                dtDetalle.Rows(i).Item(18) = Left(strLinSerie, Len(strLinSerie) - 1)
            End If

        Next

        dgDetalle.DataSource = dtDetalle
        dgDetalle.DataBind()

        If dgDetalle.Items.Count = 0 Then
            tdBtnGrabar.Visible = False
        End If
    End Sub

    Private Function Repetir(ByVal strCar As String, ByVal intveces As Integer) As String
        Dim strResult As String = ""
        Dim i As Integer

        For i = 1 To intveces
            strResult += strCar
        Next
        Repetir = strResult
    End Function

    Private Function BuscaCliente() As Boolean
        Dim dsResultado As DataSet
        Dim dsOficina As DataSet
        Dim i As Integer
        k_nrolog = 0
        k_deslog = ""

        Dim isRecarga As Boolean = Session("recargaVirtual")

        Dim idLog As String = Session("Usuario")

        '**Consultamos los datos de la oficina **'
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=====================================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=========   INICIO  BUSCAR CLIENTE       ============")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=====================================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicia la consulta Parametros de Oficina: clsConsultaPvu.ConsultaParametrosOficina")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "     IN PDV: " & Session("ALMACEN"))
        dsOficina = clsConsultaPvu.ConsultaParametrosOficina(Session("ALMACEN"), k_nrolog, k_deslog)

        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicia la consulta: clsConsultaPvu.consultaDatosCliente")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Paramètros: ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "     Prm1: " & Funciones.CheckStr(cboTipDocumento.SelectedValue))
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "     Prm1: " & Funciones.CheckStr(txtNumDocumento.Text))
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Ejecuta consula")
        '****************************************************************************************************************************'
        k_nrolog = 0
        k_deslog = ""
        dsResultado = clsConsultaPvu.consultaDatosCliente(cboTipDocumento.SelectedValue, txtNumDocumento.Text, k_nrolog, k_deslog)
        '****************************************************************************************************************************'
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "     OUT k_nrolog => " & k_nrolog.ToString)
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "     OUT k_deslog => " & k_deslog.ToString)
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin consula cliente")

        If Not IsNothing(dsResultado) Then
            If k_deslog <> "OK" Then
                Session("MenCliente") = "Cliente no encontrado."
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Cliente no encontrado.")
                BuscaCliente = False
                Exit Function
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - Count dsResultado - " & Funciones.CheckStr(dsResultado.Tables(0).Columns.Count))
            Dim nTam As Integer = dsResultado.Tables(0).Columns.Count
            ReDim Preserve arrCliente(nTam)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - ReDim arrCliente() - " & Funciones.CheckStr(arrCliente.Length))

            '**Asignamos los datos del cliente en un ARRAY:
            Try
            For i = 0 To dsResultado.Tables(0).Columns.Count - 1
                'arrCliente(i) = IIf(IsDBNull(dsResultado.Tables(0).Rows(0).Item(i)), "", dsResultado.Tables(0).Rows(0).Item(i))
                arrCliente(i) = Funciones.CheckStr(dsResultado.Tables(0).Rows(0).Item(i))
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & i & " - " & Funciones.CheckStr(dsResultado.Tables(0).Rows(0).Item(i)))
            Next
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - Llena datos del cliente y continua el flujo")
            arrCliente(7) = Right(arrCliente(7), 2) & "/" & Mid(arrCliente(7), 5, 2) & "/" & Left(arrCliente(7), 4)
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - arrCliente(7) - " & arrCliente(7))
            Session("Ubigeo_legal") = ""
            Session("Ubigeo_legal") = Funciones.CheckStr(arrCliente(19))
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & " Direccion Cliente: " & arrCliente(17))
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & " Ubigeo: " & Funciones.CheckStr(Session("Ubigeo_legal")))
            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & " Error al Obtener la Data del Cliente : " & ex.Message.ToString)
                Session("MenCliente") = ConfigurationSettings.AppSettings("MSG_Cliente_VentaRapida").ToString
                objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Cliente data erronea.")
                BuscaCliente = False
                Exit Function
            End Try
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=====================================================")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=========     FIN BUSCAR CLIENTE         ============")
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "=====================================================")

        BuscaCliente = True

    End Function

    Private Sub btnSeries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeries.Click
        Dim drFila As DataRow
        Dim i As Integer
        Dim j As Integer

        dtDetalle.Rows.Clear()
        For i = 0 To dgDetalle.Items.Count - 1
            drFila = dtDetalle.NewRow()
            For j = 1 To dgDetalle.Items(i).Cells.Count - 1
                drFila(j - 1) = dgDetalle.Items(i).Cells(j).Text
            Next
            dtDetalle.Rows.Add(drFila)
        Next
        Session("detVenta") = dtDetalle

        Response.Redirect("seriesArticulos.aspx?strArti=" & txtCodArti.Text & "&strItem=" & (dgDetalle.Items.Count + 1))

    End Sub

    Public Function SeriesCountCant(ByVal CSA As String, ByVal Id As String) As Integer
        Dim aux As Integer
        Dim i As Long
        Dim DLin As Object
        Dim DCol As Object

        aux = 0
        If Len(Trim(CSA)) > 0 Then
            DLin = Split(CSA, "|")
            For i = 0 To UBound(DLin)
                DCol = Split(DLin(i), ";")

                If Trim(DCol(1)) = Trim(Id) Then
                    If (Not IsNothing(DCol(3))) And (Trim(DCol(3)) <> "") Then
                        aux = aux + CDbl(Right(Trim(DCol(3)), 15)) - CDbl(Right(Trim(DCol(2)), 15)) + 1
                    Else
                        aux = aux + 1
                    End If
                End If
            Next
        End If

        '***********************************************************************************************'
        '**Asignaciòn de la primera SERIE axiS***'
        Dim arraySerie() As String = CSA.Split(";")
        Dim primeraSerieRango As String = ""
        For s As Integer = 0 To arraySerie.Length - 1
            If (arraySerie(s).ToString.Trim.Length = 18) Then
                'txtIMEIArt.Value = Funciones.CheckStr(arraySerie(s).ToString.Trim)
                Session("serie_defecto") = Funciones.CheckStr(arraySerie(s).ToString.Trim)
                Exit For
            End If
        Next
        '************************************************************************************************'
        SeriesCountCant = aux
    End Function

    Public Sub bloquearPuntos()
        Dim idLog As String = Funciones.CheckStr(K_NROINTERNO_PEDIDO)

        Try

            'idLog = hidNroSEC.Value
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Inicio reservarPuntos()")
            Dim UsuarioProceso As String = Session("USUARIO") 'CurrentUser ' Usuario Proceso

            Dim ID_CCLUB As String
            Dim TipoDocumento As String
            Dim NroDocumento As String
            Dim tipoClie As String
            TipoDocumento = cboTipDocumento.SelectedValue
            NroDocumento = txtNumDocumento.Text
            'tipoClie = ConfigurationSettings.AppSettings("consTipoclie")
            tipoClie = ConfigurationSettings.AppSettings("constTipoClientePOSTPAGO")
            Dim objPuntosClaroClubNegocio As New clsPuntosClaroClub
            Dim objConsultarPuntosResponse As ConsultarPuntosWS.consultarPuntosResponse
            ID_CCLUB = getCCLUB(TipoDocumento)

            Dim txId As String = String.Empty
            Dim errorCode As String = String.Empty
            Dim errorMsg As String = String.Empty


            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & "- Inicio bloquearPuntos()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & "- Inp ID_CCLUB: " & ID_CCLUB)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & "- Inp NroDocumento: " & NroDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & "- Inp tipoClie: " & tipoClie)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & "- Inp UsuarioProceso: " & UsuarioProceso)

            objPuntosClaroClubNegocio.bloquearPuntos(ID_CCLUB, _
                                                     NroDocumento, _
                                                     tipoClie, _
                                                     UsuarioProceso, _
                                                     txId, _
                                                     errorCode, _
                                                     errorMsg)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " out txId:" & txId)
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: bloquearPuntos)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, " out errorCode:" & errorCode & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, " out errorMsg:" & errorMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " Fin bloquearPuntos()")

            If errorCode <> "0" Then
                Throw New Exception(errorMsg)
            End If

            'Dim codTrs As String = ConfigurationSettings.AppSettings("codReservarPuntos")
            'Dim descTrs As String = "Reservar Puntos Claro Club. NroSec: " & hidNroSEC.Value
            'descTrs &= " ClaroClubPuntosUtilizar: " & txtClaroClubPuntosUtilizar.Text
            'descTrs &= " ClaroClubSolesDeDescuento: " & txtDescuentoClaroClub.Text
            'Auditoria(codTrs, descTrs)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " ERROR reservarPuntos(): " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " ERROR reservarPuntos(): " & ex.StackTrace.ToString())

            Throw ex
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " Fin reservarPuntos()")
        End Try
    End Sub

    ' Devuelve el còdigo equivalente al tipo de documento entre SISACT y PUNTOSCC
    Public Function getCCLUB(ByVal TipoDocumento As String) As String
        Dim ID_CCLUB As String
        Dim dsTipDoc As DataSet
        Dim item, tope As Integer
        dsTipDoc = clsConsultaPvu.ConsultaTipoDocumento("")
        For Each row As DataRow In dsTipDoc.Tables(0).Rows
            If row("TDOCC_CODIGO") = TipoDocumento Then
                ID_CCLUB = row("ID_CCLUB")
                Exit For
            End If
        Next

        Return ID_CCLUB
    End Function
    ' Efectua el desbloqueo de puntos Claro Club con un llamado a webservices.
    Public Sub LiberarPuntosCC(ByVal strDocSap As String, _
                               ByVal ID_CCLUB As String, _
                               ByVal NroDocumento As String)
        Dim idLog As String = strDocSap

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Inicio LiberarPuntosCC()")
            Dim UsuarioProceso As String = Session("USUARIO") 'CurrentUser ' Usuario Proceso
            '''''''''''' Llamar al famoso WS
            Dim tipoClie As String
            Dim objPuntosClaroClubNegocio As New clsPuntosClaroClub
            Dim txId As String
            Dim errorCode As String
            Dim errorMsg As String
            tipoClie = ConfigurationSettings.AppSettings("constTipoClientePOSTPAGO")
            ID_CCLUB = getCCLUB(ID_CCLUB)

            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - Inicio liberarPuntos(): " & ConfigurationSettings.AppSettings("WSGestionarPuntosCC_url"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - inp ID_CCLUB: " & ID_CCLUB)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - inp NroDocumento: " & NroDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - inp tipoClie: " & tipoClie)

            objPuntosClaroClubNegocio.liberarPuntos(ID_CCLUB, _
                                                    NroDocumento, _
                                                    tipoClie, _
                                                    txId, _
                                                    errorCode, _
                                                    errorMsg)


            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - out txId:" & txId)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - out errorCode:" & errorCode)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - out errorMsg:" & errorMsg)
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " Fin liberarPuntos()")

            If errorCode <> "0" Then
                Throw New Exception(errorMsg)
            End If
        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: LiberarPuntosCC)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - ERROR LiberarPuntosCC(): " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - Fin LiberarPuntosCC(): ")
        End Try
    End Sub
    Public Sub EliminarCanjePuntos(ByVal nroPedido As String)
        Dim idLog As String = nroPedido
        Dim objclsTrsMsSap As New COM_SIC_Activaciones.clsTrsMsSap
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Inicio EliminarCanjePuntos()")
            objclsTrsMsSap.AnularPedido(nroPedido, "STO", "ANU", Session("ALMACEN"))

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR EliminarCanjePuntos(): " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "ERROR EliminarCanjePuntos(): " & ex.StackTrace.ToString())
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, idLog & " - " & "Fin EliminarCanjePuntos(): ")
    End Sub

End Class
