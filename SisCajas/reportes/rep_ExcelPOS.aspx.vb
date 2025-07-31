Imports System.IO
Imports SisCajas.Funciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Activaciones
Imports COM_SIC_Adm_Cajas

Public Class rep_ExcelPOS
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPDV As System.Web.UI.WebControls.Label
    Protected WithEvents lblCajero As System.Web.UI.WebControls.Label
    Protected WithEvents dgTransacciones As System.Web.UI.WebControls.DataGrid

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
    Dim dsTransacciones As DataSet
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            llena_grid()
         
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteTransacciones.xls")
            Response.ContentType = "application/vnd.ms-excel"

        End If

        'End If
    End Sub

    Private Sub llena_grid()

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

        Try
            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            objEntity.ipServidor = Funciones.CheckStr(ipServer)
            objEntity.codVenta = Request.QueryString("varPDV")
            objEntity.tipoTarjeta = Request.QueryString("varTipoTarjeta")
            objEntity.codEstablecimiento = Request.QueryString("varCodComercio")
            strFechaInicial = Request.QueryString("varFechaInicio")
            strFechaFinal = Request.QueryString("varFechaFin")
            objEntity.codCajero = IIf((Request.QueryString("varCajero") = ""), "", Funciones.CheckStr(Funciones.CheckInt(Request.QueryString("varCajero"))))
            objEntity.tipoTransaccion = Request.QueryString("varTipoTransaccion")
            objEntity.codOperacion = Request.QueryString("varTipoOperacion")
            objEntity.estadoTransaccion = Request.QueryString("varEstadoTransaccion")
            objEntity.numVoucher = ""
            codRespuesta = ""
            msjRespuesta = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, "Cargando Transsacciones POS - EXCEL")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strPdv : " & objEntity.codVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTarjetaPos : " & objEntity.tipoTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strCodComercio : " & objEntity.codEstablecimiento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaInicial : " & strFechaInicial)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaFinal : " & strFechaFinal)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strUsuarioCajero : " & objEntity.codCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTransaccion : " & objEntity.tipoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoOperacion : " & objEntity.codOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strEstado : " & objEntity.estadoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "NroRef : " & objEntity.numVoucher)


            objSicarDB.ConsultaDetalleReporte(objEntity, strFechaInicial, strFechaFinal, codRespuesta, msjRespuesta, listaResponse)


            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ouput: " & "codRespuesta : " & codRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ouput: " & "msjRespuesta : " & msjRespuesta)

            If (codRespuesta = 0) Then

                If (listaResponse Is Nothing) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - MSJ: " & ClsKeyPOS.strMsjSinTransacciones)
                    Me.RegisterStartupScript("Sin Datos", "<script language=javascript>alert('" & ClsKeyPOS.strMsjSinTransacciones & "');</script>")

                    dgTransacciones.DataSource = Nothing
                    dgTransacciones.DataBind()
                Else

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "NRO TRANSACCIONES : " & listaResponse.Count)
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
                    Dim fechaTransaccionPos As DataColumn
                    Dim horaTransaccionPos As DataColumn
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

                    fechaTransaccionPos = New DataColumn
                    fechaTransaccionPos.DataType = System.Type.GetType("System.String")
                    fechaTransaccionPos.ColumnName = "fechaTransaccionPos"
                    dtTransaccionesPos.Columns.Add(fechaTransaccionPos)


                    horaTransaccionPos = New DataColumn
                    horaTransaccionPos.DataType = System.Type.GetType("System.String")
                    horaTransaccionPos.ColumnName = "horaTransaccionPos"
                    dtTransaccionesPos.Columns.Add(horaTransaccionPos)


                    For i = 0 To listaResponse.Count - 1
                        myDataRow = dtTransaccionesPos.NewRow
                        myDataRow("codPdv") = listaResponse.Item(i).codPdv
                        myDataRow("usuavCajero") = listaResponse(i).usuavCajero
                        myDataRow("posvIdestablec") = listaResponse(i).posvIdestablec
                        myDataRow("trnsvNumPedido") = listaResponse(i).trnsvNumPedido
                        myDataRow("trnsvIdRef") = IIf(Funciones.CheckStr(listaResponse(i).trnsvIdRef).Length > 0, "''" & listaResponse(i).trnsvIdRef & "''", listaResponse(i).trnsvIdRef)
                        myDataRow("trnsvTipoTrans") = listaResponse(i).trnsvTipoTrans
                        myDataRow("trnsvEstado") = listaResponse(i).trnsvEstado
                        myDataRow("trnscOperacion") = listaResponse(i).trnscOperacion
                        myDataRow("trnsvTipoTarjetaPos") = listaResponse(i).trnsvTipoTarjetaPos
                        myDataRow("trnsvNroTarjeta") = listaResponse(i).trnsvNroTarjeta
                        myDataRow("trnsnMonto") = listaResponse(i).trnsnMonto
                        myDataRow("fechaTransaccionPos") = listaResponse(i).fechaTransaccionPos
                        myDataRow("horaTransaccionPos") = listaResponse(i).horaTransaccionPos
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

                        objFileLog.Log_WriteLog(pathFile, strArchivo, "NRO TRANSACCIONES : " & dtTransacciones.Rows.Count)
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
                        dtTransacciones.Columns(25).ColumnName = "fechaTransaccionPos"
                        dtTransacciones.Columns(26).ColumnName = "horaTransaccionPos"

                        dgTransacciones.DataSource = dtTransacciones
                        dgTransacciones.DataBind()
                    End If

                Else

                    Me.RegisterStartupScript("Sin Datos", "<script language=javascript>alert('" & msjRespuesta.Replace("'", " ") & "');</script>")

                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransaction - Ejecutando Contingencia - Fin")
            End If

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR - TransaccionesPOS : " & ex.Message.ToString())
            Me.RegisterStartupScript("Catch", "<script language=javascript>alert('" + ex.Message.ToString().Replace("'", " ") + "');</script>")
        End Try

    End Sub

End Class
