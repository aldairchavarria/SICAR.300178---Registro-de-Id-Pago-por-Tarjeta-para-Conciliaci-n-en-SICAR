Imports System.IO
Imports SisCajas.Funciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Activaciones
Imports COM_SIC_Adm_Cajas


Public Class ConsultaDetallePOS 
    'Inherits System.Web.UI.Page
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents procesarHandler As System.Web.UI.WebControls.Button
    Protected WithEvents btnExportar As System.Web.UI.WebControls.Button
    Protected WithEvents btnLimpiar As System.Web.UI.WebControls.Button
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtNomCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents Checkbox1 As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTodosOficina As System.Web.UI.WebControls.CheckBox
    Protected WithEvents frmPrincipal As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents trRemesa As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents txtOficinaVenta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodComercio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecFin As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCajero As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboTipoTarjeta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipoTransaccion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboTipoOperacion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cboEstadoOperacion As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lstPuntosVenta As System.Web.UI.WebControls.ListBox
    Protected WithEvents lstPDV As System.Web.UI.WebControls.ListBox
    Protected WithEvents lstPuntoVentas As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkTodosComercio As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cboCajero As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkTodosCajero As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnBusar As System.Web.UI.WebControls.Button
    Protected WithEvents dgTransacciones As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecIni As System.Web.UI.WebControls.TextBox

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
    Dim objTransPos As New COM_SIC_Activaciones.clsTransaccionPOS
    Dim objclsAdmCaja As New COM_SIC_Adm_Cajas.clsAdmCajas
    Private Const CAJERO As String = "C"
    Dim dsTiposTarjeta As DataSet
    Dim dsTiposTransaccion As DataSet
    Dim dsTiposOperacion As DataSet
    Dim dsEstadosTransaccion As DataSet
    Dim dsPDV As DataSet
    Dim dsCajeros As DataSet
    Dim dsTransacciones As DataSet
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

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

            If Not IsPostBack Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Reporte de Transassacciones POS")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Combos")
                LlenarDatosCombos()

            End If
        End If


    End Sub

    Private Sub LlenarDatosCombos()
        Try
            Dim strCodRpta As String = ""
            Dim strMsgRpta As String = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Tipos de Tarjeta POS")
            dsTiposTarjeta = objTransPos.ObtenerTiposTarjeta(strCodRpta, strMsgRpta)
            cboTipoTarjeta.DataSource = dsTiposTarjeta.Tables(0)
            cboTipoTarjeta.DataTextField = "SPARV_VALUE2"
            cboTipoTarjeta.DataValueField = "SPARV_VALUE"
            cboTipoTarjeta.DataBind()
            cboTipoTarjeta.Items.Insert(0, "TODOS")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Codigo Respuesta: " & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Mensaje Respuesta: " & strMsgRpta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Tipos de Transaccion POS")
            dsTiposTransaccion = objTransPos.ObtenerTiposTransaccion(strCodRpta, strMsgRpta)
            cboTipoTransaccion.DataSource = dsTiposTransaccion.Tables(0)
            cboTipoTransaccion.DataTextField = "SPARV_VALUE2"
            cboTipoTransaccion.DataValueField = "SPARV_VALUE"
            cboTipoTransaccion.DataBind()
            cboTipoTransaccion.Items.Insert(0, "TODOS")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Codigo Respuesta: " & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Mensaje Respuesta: " & strMsgRpta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Tipos de Operacion POS")
            dsTiposOperacion = objTransPos.ObtenerTiposOperacion(strCodRpta, strMsgRpta)
            cboTipoOperacion.DataSource = dsTiposOperacion.Tables(0)
            cboTipoOperacion.DataTextField = "SPARV_VALUE2"
            cboTipoOperacion.DataValueField = "SPARV_VALUE"
            cboTipoOperacion.DataBind()
            cboTipoOperacion.Items.Insert(0, "TODOS")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Codigo Respuesta: " & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Mensaje Respuesta: " & strMsgRpta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Tipos de Estados de Transaccion POS")
            dsEstadosTransaccion = objTransPos.ObtenerEstadosTransaccion(strCodRpta, strMsgRpta)
            cboEstadoOperacion.DataSource = dsEstadosTransaccion.Tables(0)
            cboEstadoOperacion.DataTextField = "SPARV_VALUE2"
            cboEstadoOperacion.DataValueField = "SPARV_VALUE"
            cboEstadoOperacion.DataBind()
            cboEstadoOperacion.Items.Insert(0, "TODOS")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Codigo Respuesta: " & strCodRpta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Mensaje Respuesta: " & strMsgRpta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando PDV")
            dsPDV = objclsAdmCaja.GetOficinas("")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cantidad de PDV: " & dsPDV.Tables(0).Rows.Count)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "PDV Actual: " & Session("ALMACEN"))
            Dim tblPDV As DataTable = dsPDV.Tables(0)
            Dim i As Integer
            Dim j As Integer = 0
            For i = 0 To dsPDV.Tables(0).Rows.Count - 1

                lstPDV.Items.Add(New ListItem(IIf(IsDBNull(tblPDV.Rows(i).Item("DESCRIPCION")), "", tblPDV.Rows(i).Item("DESCRIPCION")), IIf(IsDBNull(tblPDV.Rows(i).Item("CODIGO")), "", tblPDV.Rows(i).Item("CODIGO"))))

                If (tblPDV.Rows(i).Item("CODIGO") = Session("ALMACEN")) Then
                    j = i
                End If
            Next

            Session("PDV_INDEX") = j

            lstPDV.SelectedIndex = j
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cargando Cajeros")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "PDV INPUT: " & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ROL INPUT: " & CAJERO)

            dsCajeros = objclsAdmCaja.GetVendedores("", lstPDV.SelectedValue, CAJERO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Cantidad de Cajeros : " & dsCajeros.Tables(0).Rows.Count - 1)

            cboCajero.DataSource = dsCajeros.Tables(0)
            cboCajero.DataTextField = "DESCRIPCION"
            cboCajero.DataValueField = "CODIGO"
            cboCajero.DataBind()
            cboCajero.Items.Insert(0, "Seleccionar")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - LlenarDatosCombos : " & ex.Message.ToString())
            Me.RegisterStartupScript("CatchCBO", "<script language=javascript>alert('" & "Error al Cargar Formas de Pago - " & ex.Message.ToString().Replace("'", " ") & "');</script>")
        End Try


    End Sub
    Private Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Try

            chkTodosOficina.Checked = False
            chkTodosCajero.Checked = False
            chkTodosComercio.Checked = False
            cboTipoTarjeta.SelectedIndex = 0
            cboTipoOperacion.SelectedIndex = 0
            cboTipoTransaccion.SelectedIndex = 0
            cboEstadoOperacion.SelectedIndex = 0
            cboCajero.SelectedIndex = 0
            lstPDV.SelectedIndex = Session("PDV_INDEX")

            cboCajero.Enabled = True
            lstPDV.Enabled = True
            txtCodComercio.Enabled = True
            txtFecIni.Text = ""
            txtFecFin.Text = ""
            txtCodComercio.Text = ""

            dgTransacciones.DataSource = Nothing
            dgTransacciones.DataBind()

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - Limpiar : " & ex.Message.ToString())
            Me.RegisterStartupScript("CatchCBO", "<script language=javascript>alert('" & "Error limpiar datos - " & ex.Message.ToString().Replace("'", " ") & "');</script>")
        End Try

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        llenar_grid()
    End Sub

    Private Sub llenar_grid()

        Dim strFechaInicial As String = ""
        Dim strFechaFinal As String = ""
        Dim codRespuesta As String = ""
        Dim msjRespuesta As String = ""
        Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
        Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
        Dim nombreHost As String = System.Net.Dns.GetHostName
        Dim nombreServer As String = System.Net.Dns.GetHostName
        Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
        Dim listaResponse As New ArrayList
        Dim sMensaje As String = String.Empty
        Try
            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            objEntity.ipServidor = Funciones.CheckStr(ipServer)

            If (chkTodosOficina.Checked) Then
                objEntity.codVenta = ""
            Else
                objEntity.codVenta = lstPDV.SelectedValue
            End If

            If (chkTodosCajero.Checked) Then
                objEntity.codCajero = ""
            Else
                objEntity.codCajero = Funciones.CheckStr(Funciones.CheckInt(cboCajero.SelectedValue))
            End If

            If (chkTodosComercio.Checked) Then
                objEntity.codEstablecimiento = ""
            Else
                objEntity.codEstablecimiento = txtCodComercio.Text
            End If

            If (cboTipoTarjeta.SelectedIndex = 0) Then
                objEntity.tipoTarjeta = ""
            Else
                objEntity.tipoTarjeta = cboTipoTarjeta.SelectedValue
            End If

            If (cboTipoOperacion.SelectedIndex = 0) Then

                objEntity.codOperacion = ""
            Else
                objEntity.codOperacion = cboTipoOperacion.SelectedValue
            End If

            If (cboTipoTransaccion.SelectedIndex = 0) Then

                objEntity.tipoTransaccion = ""
            Else
                objEntity.tipoTransaccion = cboTipoTransaccion.SelectedValue
            End If

            If (cboEstadoOperacion.SelectedIndex = 0) Then
                objEntity.estadoTransaccion = ""
            Else
                objEntity.estadoTransaccion = cboEstadoOperacion.SelectedValue
            End If

            objEntity.numVoucher = ""


            strFechaFinal = txtFecFin.Text
            strFechaInicial = txtFecIni.Text
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Cargando Transsacciones POS ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strPdv : " & objEntity.codVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTarjetaPos : " & objEntity.tipoTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strCodComercio: " & objEntity.codEstablecimiento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaInicial : " & strFechaInicial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaFinal : " & strFechaFinal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strUsuarioCajero : " & objEntity.codCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTransaccion : " & objEntity.tipoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoOperacion : " & objEntity.codOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strEstado : " & objEntity.estadoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "NroRef : " & objEntity.numVoucher)


            objSicarDB.ConsultaDetalleReporte(objEntity, strFechaInicial, strFechaFinal, codRespuesta, msjRespuesta,listaResponse)


            'dsTransacciones = objTransPos.TransaccionesPOS(objEntity, , , codRespuesta, msjRespuesta)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ouput: " & "codRespuesta : " & codRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ouput: " & "msjRespuesta : " & msjRespuesta)

            If (codRespuesta = 0) Then

                If (listaResponse Is Nothing) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - MSJ: " & ClsKeyPOS.strMsjSinTransacciones)
                    Me.RegisterStartupScript("Sin Datos", "<script language=javascript>alert('" & ClsKeyPOS.strMsjSinTransacciones & "');</script>")

                    dgTransacciones.DataSource = Nothing
                    dgTransacciones.DataBind()
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - NRO TRANSACCIONES : " & listaResponse.Count)
                    ' Create new DataTable.
                    Dim dtTransaccionesPos As DataTable = New DataTable("dtTransacciones")
                    ' Declare DataColumn and DataRow variables.
                    Dim codPdv As DataColumn
                    Dim usuavCajero As DataColumn
                    Dim posvIdestablec As DataColumn
                    Dim trnsvNumPedido As DataColumn
                    Dim trnsvIdRef As DataColumn
                    Dim trnsvTipoTrans As DataColumn
                    Dim trnsvEstado As DataColumn
                    Dim trnscOperacion As DataColumn
                    Dim trnsvTipoTarjetaPos As DataColumn
                    Dim trnsvNroTarjeta As DataColumn
                    Dim trnsnMonto As DataColumn
                    Dim i As Integer = 0

                    Dim myDataRow As DataRow


                    ' Create new DataColumn, set DataType, ColumnName and add to DataTable.
                    codPdv = New DataColumn
                    codPdv.DataType = System.Type.GetType("System.String")
                    codPdv.ColumnName = "codPdv"
                    dtTransaccionesPos.Columns.Add(codPdv)

                    usuavCajero = New DataColumn
                    usuavCajero.DataType = System.Type.GetType("System.String")
                    usuavCajero.ColumnName = "usuavCajero"
                    dtTransaccionesPos.Columns.Add(usuavCajero)

                    posvIdestablec = New DataColumn
                    posvIdestablec.DataType = System.Type.GetType("System.String")
                    posvIdestablec.ColumnName = "posvIdestablec"
                    dtTransaccionesPos.Columns.Add(posvIdestablec)

                    trnsvNumPedido = New DataColumn
                    trnsvNumPedido.DataType = System.Type.GetType("System.String")
                    trnsvNumPedido.ColumnName = "trnsvNumPedido"
                    dtTransaccionesPos.Columns.Add(trnsvNumPedido)

                    trnsvIdRef = New DataColumn
                    trnsvIdRef.DataType = System.Type.GetType("System.String")
                    trnsvIdRef.ColumnName = "trnsvIdRef"
                    dtTransaccionesPos.Columns.Add(trnsvIdRef)

                    trnsvTipoTrans = New DataColumn
                    trnsvTipoTrans.DataType = System.Type.GetType("System.String")
                    trnsvTipoTrans.ColumnName = "trnsvTipoTrans"
                    dtTransaccionesPos.Columns.Add(trnsvTipoTrans)

                    trnsvEstado = New DataColumn
                    trnsvEstado.DataType = System.Type.GetType("System.String")
                    trnsvEstado.ColumnName = "trnsvEstado"
                    dtTransaccionesPos.Columns.Add(trnsvEstado)

                    trnscOperacion = New DataColumn
                    trnscOperacion.DataType = System.Type.GetType("System.String")
                    trnscOperacion.ColumnName = "trnscOperacion"
                    dtTransaccionesPos.Columns.Add(trnscOperacion)

                    trnsvTipoTarjetaPos = New DataColumn
                    trnsvTipoTarjetaPos.DataType = System.Type.GetType("System.String")
                    trnsvTipoTarjetaPos.ColumnName = "trnsvTipoTarjetaPos"
                    dtTransaccionesPos.Columns.Add(trnsvTipoTarjetaPos)

                    trnsvNroTarjeta = New DataColumn
                    trnsvNroTarjeta.DataType = System.Type.GetType("System.String")
                    trnsvNroTarjeta.ColumnName = "trnsvNroTarjeta"
                    dtTransaccionesPos.Columns.Add(trnsvNroTarjeta)


                    trnsnMonto = New DataColumn
                    trnsnMonto.DataType = System.Type.GetType("System.String")
                    trnsnMonto.ColumnName = "trnsnMonto"
                    dtTransaccionesPos.Columns.Add(trnsnMonto)


                    For i = 0 To listaResponse.Count - 1
                        myDataRow = dtTransaccionesPos.NewRow
                        myDataRow("codPdv") = listaResponse.Item(i).codPdv
                        myDataRow("usuavCajero") = listaResponse(i).usuavCajero
                        myDataRow("posvIdestablec") = listaResponse(i).posvIdestablec
                        myDataRow("trnsvNumPedido") = listaResponse(i).trnsvNumPedido
                        myDataRow("trnsvIdRef") = listaResponse(i).trnsvIdRef
                        myDataRow("trnsvTipoTrans") = listaResponse(i).trnsvTipoTrans
                        myDataRow("trnsvEstado") = listaResponse(i).trnsvEstado
                        myDataRow("trnscOperacion") = listaResponse(i).trnscOperacion
                        myDataRow("trnsvTipoTarjetaPos") = listaResponse(i).trnsvTipoTarjetaPos
                        myDataRow("trnsvNroTarjeta") = listaResponse(i).trnsvNroTarjeta
                        myDataRow("trnsnMonto") = listaResponse(i).trnsnMonto
                        dtTransaccionesPos.Rows.Add(myDataRow)
                    Next

                    dgTransacciones.DataSource = dtTransaccionesPos
                    dgTransacciones.DataBind()
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Error en el Servicio: " & "codRespuesta : " & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Error en el Servicio: " & "msjRespuesta : " & msjRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ejecutando Contingencia - Inicio")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - SP -> SICASS_REP_PAGO")

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS
                Dim dtTransacciones As New DataTable
                dtTransacciones = objTransascPos.TransaccionesPOS(objEntity.codVenta, objEntity.tipoTarjeta, objEntity.codEstablecimiento, _
                                                strFechaInicial, strFechaFinal, objEntity.codCajero, objEntity.tipoTransaccion, _
                                                objEntity.codOperacion, objEntity.estadoTransaccion, objEntity.numVoucher, codRespuesta, msjRespuesta).Tables(0)

                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "codRespuesta : " & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "msjRespuesta : " & msjRespuesta)
                
                If (codRespuesta = "0") Then


                    If (dtTransacciones.Rows.Count = 0) Then

                        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - MSJ: " & ClsKeyPOS.strMsjSinTransacciones)
                        Me.RegisterStartupScript("Sin Datos", "<script language=javascript>alert('" & ClsKeyPOS.strMsjSinTransacciones & "');</script>")

                        dgTransacciones.DataSource = Nothing
                        dgTransacciones.DataBind()


                    Else

                        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "NRO TRANSACCIONES : " & dtTransacciones.Rows.Count)
                        dtTransacciones.Columns(3).ColumnName = "codPdv"
                        dtTransacciones.Columns(9).ColumnName = "usuavCajero"
                        dtTransacciones.Columns(29).ColumnName = "posvIdestablec"
                        dtTransacciones.Columns(14).ColumnName = "trnsvNumPedido"
                        dtTransacciones.Columns(17).ColumnName = "trnsvIdRef"
                        dtTransacciones.Columns(11).ColumnName = "trnsvTipoTrans"
                        dtTransacciones.Columns(27).ColumnName = "trnsvEstado"
                        dtTransacciones.Columns(12).ColumnName = "trnscOperacion"
                        dtTransacciones.Columns(23).ColumnName = "trnsvTipoTarjetaPos"
                        dtTransacciones.Columns(24).ColumnName = "trnsvNroTarjeta"
                        dtTransacciones.Columns(16).ColumnName = "trnsnMonto"

                        dgTransacciones.DataSource = dtTransacciones
                        dgTransacciones.DataBind()
                    End If

                Else

                    Me.RegisterStartupScript("Sin Datos", "<script language=javascript>alert('" & msjRespuesta & "');</script>")

                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "Ejecutando Contingencia - Fin")


            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - TransaccionesPOS : " & ex.Message.ToString())
            Me.RegisterStartupScript("Catch", "<script language=javascript>alert('" + ex.Message.ToString().Replace("'", " ") + "');</script>")
        Finally

            sMensaje = "Consulta detalle POS. " & sMensaje & ". Datos: Canal=" & "|PDV=" & Funciones.CheckStr(objEntity.codVenta) & "|Cajero=" & _
                                                Funciones.CheckStr(objEntity.codCajero)
            '--registra auditoria
            RegistrarAuditoria(sMensaje, ConfigurationSettings.AppSettings("ConsOpeRepTransPOS_codTrsAuditoria"))

        End Try

    End Sub

    Private Sub lstPDV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPDV.SelectedIndexChanged

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, "Cargando Cajeros")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "PDV INPUT: " & lstPDV.SelectedValue)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ROL INPUT: " & CAJERO)

            dsCajeros = objclsAdmCaja.GetVendedores("", lstPDV.SelectedValue, CAJERO)

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Cantidad de Cajeros : " & dsCajeros.Tables(0).Rows.Count - 1)
            cboCajero.DataSource = dsCajeros.Tables(0)
            cboCajero.DataTextField = "DESCRIPCION"
            cboCajero.DataValueField = "CODIGO"
            cboCajero.DataBind()
            cboCajero.Items.Insert(0, "Seleccionar")


        Catch ex As Exception
            Me.RegisterStartupScript("Cath - ERROR:", "<script language=javascript>alert('" & ex.Message.ToString().Replace("'", " ") & "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - lstPDV_SelectedIndexChanged " & ex.Message.ToString())
        End Try



    End Sub

    Private Sub chkTodosOficina_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodosOficina.CheckedChanged


        Try
            If (chkTodosOficina.Checked) Then
                lstPDV.Enabled = False
            Else
                lstPDV.Enabled = True
            End If


        Catch ex As Exception
            Me.RegisterStartupScript("Cath - ERROR:", "<script language=javascript>alert('" & ex.Message.ToString().Replace("'", " ") & "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - chkTodosOficina_CheckedChanged " & ex.Message.ToString())
        End Try


    End Sub

    Private Sub chkTodosComercio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodosComercio.CheckedChanged

        Try
            If (chkTodosComercio.Checked) Then
                txtCodComercio.Enabled = False
            Else
                txtCodComercio.Enabled = True
            End If


        Catch ex As Exception
            Me.RegisterStartupScript("Cath - ERROR:", "<script language=javascript>alert('" & ex.Message.ToString().Replace("'", " ") & "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - chkTodosComercio_CheckedChanged " & ex.Message.ToString())
        End Try


    End Sub

    Private Sub chkTodosCajero_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodosCajero.CheckedChanged


        Try
            If (chkTodosCajero.Checked) Then
                cboCajero.Enabled = False
            Else
                cboCajero.Enabled = True
            End If


        Catch ex As Exception
            Me.RegisterStartupScript("Cath - ERROR:", "<script language=javascript>alert('" & ex.Message.ToString().Replace("'", " ") & "');</script>")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "ERROR - chkTodosCajero_CheckedChanged " & ex.Message.ToString())
        End Try


    End Sub

    Private Sub RegistrarAuditoria(ByVal DesTrx As String, ByVal CodTrx As String)
        Try
            Dim user As String = Me.CurrentUser
            Dim ipHost As String = CurrentTerminal
            Dim nameHost As String = System.Net.Dns.GetHostName
            Dim nameServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nameServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nameHost)

            Dim CadMensaje As String
            Dim CodServicio As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim oAuditoria As New COM_SIC_Activaciones.clsAuditoriaWS

            oAuditoria.RegistrarAuditoria(CodTrx, _
                                            CodServicio, _
                                            ipHost, _
                                            nameHost, _
                                            ipServer, _
                                            nameServer, _
                                            user, _
                                            "", _
                                            "0", _
                                            DesTrx)

        Catch ex As Exception
            ' Throw New Exception("Error Registrar Auditoria.")
        End Try

    End Sub

End Class
