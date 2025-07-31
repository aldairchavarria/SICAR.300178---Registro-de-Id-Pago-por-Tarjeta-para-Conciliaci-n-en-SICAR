Imports System.Xml
Imports SisCajas.Funciones
Imports COM_SIC_Activaciones
Public Class PoolRechazoPagoSunat
    Inherits SICAR_WebBase
    Dim ds As DataSet
    Dim drFila As DataRow
    Dim strOficinaVta As String
    Dim strCanal As String
    Dim strUsuario As String
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = "Log_PoolRechazosFE"
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)


    Dim dtsap, dtsicar As DataTable
    Dim dtcon As New DataTable
    Dim dsBusqGrupo As DataSet

    Dim valorCombo As String = ""

    Protected WithEvents btnPagar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCompensar As System.Web.UI.WebControls.Button
    Protected WithEvents btnReasignar As System.Web.UI.WebControls.Button
    Protected WithEvents txtDocSap As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDocSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroDG As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTipDoc As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtpImp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtOffline As System.Web.UI.WebControls.TextBox
    Protected WithEvents hidVerif As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtEfectivo As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtRecibido As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtEntregar As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents flagVentaEquipoPrepago As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtsession As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFecha2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblOficina As System.Web.UI.WebControls.Label
    Protected WithEvents cboOficina As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hidAccion As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtTipoUsuario As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtCodOficina2 As System.Web.UI.HtmlControls.HtmlInputHidden

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgPool As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents cmdAnular As System.Web.UI.WebControls.Button
    Protected WithEvents txtRbPagos As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents cmdDescompensar As System.Web.UI.WebControls.Button
    Protected WithEvents cmdAnularPago As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
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
            Dim strFecha As String

            Me.cboOficina.Attributes.Add("onchange", "CambiaOficina();")

            strCanal = Session("CANAL")
            strUsuario = Session("USUARIO")

            If Not Page.IsPostBack Then

                If Session("FechaPago") = "" Then
                    txtFecha.Text = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Format(Now, "d")
                    txtFecha2.Text = txtFecha.Text
                Else
                    txtFecha.Text = Session("FechaPago")
                    txtFecha2.Text = txtFecha.Text
                    Session("FechaPago") = ""
                End If

                strOficinaVta = Session("ALMACEN")

                '****** INCIO cargar el combo puntos de venta *******'
                Dim blnVer As Boolean = verificarPerfilRechazo()
                Dim dsOficinas As DataSet
                Dim objOficinas As New COM_SIC_OffLine.clsOffline


                If blnVer Then
                    strOficinaVta = ""
                    cboOficina.Visible = True
                    lblOficina.Visible = True
                    dsOficinas = objOficinas.Get_ConsultaOficinaVenta("", "")
                    cboOficina.DataSource = dsOficinas.Tables(0)
                    cboOficina.DataValueField = "VKBUR"
                    cboOficina.DataTextField = "BEZEI"
                    cboOficina.DataBind()
                    cboOficina.Items.Insert(0, New ListItem("-- TODOS --", ""))
                Else
                    cboOficina.Visible = False
                    lblOficina.Visible = False
                End If
                '****** FIN cargar el combo puntos de venta *******'
            End If

            If cboOficina.Visible = False And lblOficina.Visible = False Then
                strOficinaVta = Session("ALMACEN")
            Else
                strOficinaVta = cboOficina.SelectedValue
            End If

            CargarGrilla(strOficinaVta)

            If dtsap.Rows.Count = 0 Then
                Response.Write("<script> alert('No existen documentos para la fecha indicada')</script>")
            End If
        End If

    End Sub
    Private Sub CargarGrilla(ByVal strOficinaVta As String)
        Try
            Dim idLog As String = Session("ALMACEN")

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "-----------------------------------------------")
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       INICIO POOL DE RECHAZOS SUNAT           ")
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "-----------------------------------------------")

            Dim blnVerALL As Boolean = verificarPerfilRechazo()
            Dim objMssap As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim fecIni As String = DateTime.Today.ToShortDateString()
            Dim fecFin As String = DateTime.Today.ToShortDateString()
            Dim sinergiaPDV As String

            Dim dsConsulta As New DataSet
            Dim strIdentifyLog As String = strOficinaVta

            Dim strTipoPool As String = "X" 'ConfigurationSettings.AppSettings("FE_PoolRechazo_TipoPool")
            Dim strEnviado As String = ConfigurationSettings.AppSettings("FE_PoolRechazo_Enviado")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "strTipoPool:" & strTipoPool)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "strEnviado:" & strEnviado)

            Dim punto_venta As String = ""
            punto_venta = ConsultaPuntoVenta(txtCodOficina2.Value.ToString)
            sinergiaPDV = ConsultaPuntoVenta(Session("ALMACEN"))

            If (IsDate(Me.txtFecha.Text)) Then
                fecIni = Convert.ToDateTime(Me.txtFecha.Text).ToShortDateString()
            End If

            If (IsDate(Me.txtFecha2.Text)) Then
                fecFin = Convert.ToDateTime(Me.txtFecha2.Text).ToShortDateString()
            End If

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, " Inicio Consulta Documentos Rechazados ")
            If blnVerALL Then
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "   ConsultaPagosRechazados - SP: ")
                If cboOficina.Visible Then
                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Estado: " & strTipoPool)
                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Fecha Inicio: " & txtFecha.Text)
                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Fecha Fin: " & txtFecha2.Text)
                    objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN PDV: " & punto_venta)
                    dtsap = objMssap.ConsultaPagosRechazados(strTipoPool, fecIni, fecFin, punto_venta)

                End If
            Else
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Estado: ")
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Fecha Inicio: " & txtFecha.Text)
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN Fecha Fin: " & txtFecha2.Text)
                objFileLog.EscribirLog(pathFile, strArchivo, idLog, "       IN PDV: " & strOficinaVta)
                dtsap = objMssap.ConsultaPagosRechazados(strTipoPool, fecIni, fecFin, sinergiaPDV)
            End If
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, " Fin Consulta Documentos Rechazados ")

            dtsap.Columns.Add("Estado_SAP", GetType(String))
            dtsap.Columns.Add("ID_T_TRS_PEDIDO", GetType(String))

            If dtsap.Rows.Count > 0 Then
                For i As Integer = 0 To dtsap.Rows.Count - 1
                    dtsap.Rows.Item(i)("Estado_SAP") = "PROCESADO"
                Next
            End If
            dtsap.AcceptChanges()


            dgPool.DataSource = dtsap
            dgPool.DataBind()

            If blnVerALL Then
                RegistrarAuditoria("Consulta Pool Rechazos SUNAT. Todos los PDVs ", CheckStr(ConfigurationSettings.AppSettings("codTrsPoolRechazadoSunat")))
            Else
                RegistrarAuditoria("Consulta Pool Rechazos SUNAT PDV: " & Session("ALMACEN"), CheckStr(ConfigurationSettings.AppSettings("codTrsPoolRechazadoSunat")))
            End If

            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "-----------------------------------------------")
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "           FIN POOL DE RECHAZOS SUNAT          ")
            objFileLog.EscribirLog(pathFile, strArchivo, idLog, "-----------------------------------------------")

        Catch ex As Exception
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: CargarGrilla)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.StackTrace.ToString())
        End Try
    End Sub

    Private Function AnularPago(ByVal strNroDocSap As String, ByVal strCodOficina As String, ByVal strUsuario As String) As Boolean
        Dim blnAnulaPago As Boolean

        Return blnAnulaPago

    End Function

    Private Function AnularPedido(ByVal strNroDocSap As String, ByVal strCodOficina As String, ByVal strUsuario As String) As String
        'Anulación con RFC ZPVU_RFC_TRS_PEDIDO_ANULACION
        Dim aCadena(35) As String
        aCadena(0) = " "
        aCadena(1) = "ZAFR"
        aCadena(5) = strCodOficina ' Oficina de Ventas
        aCadena(7) = strNroDocSap ' Numero Documento Sap
        Dim strCadenaDoc As String = String.Join(";", aCadena)
        Dim obSAP As New SAP_SIC_Pagos.clsPagos
        Dim strCodTrs As String = "0"
        Dim strIdentifyLog As String = strOficinaVta
        Try
            'Inicio LOGs
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio: AnularPedido - Set_AnularDocumentoJob(" & strCadenaDoc & ", " & strUsuario & ")")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha: " & txtFecha.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "USUARIO: " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOficinaVta: " & strOficinaVta)

            Dim dsResult2 As DataSet = obSAP.Set_AnularDocumentoJob(CStr(strCadenaDoc), strUsuario)
            If dsResult2.Tables(1).Rows.Count > 0 Then
                Dim drMsg As DataRow
                For Each drMsg In dsResult2.Tables(1).Rows
                    If CStr(drMsg("TYPE")) = "E" Then
                        'Throw New ApplicationException(CStr(drMsg("MSG")))
                        strCodTrs = "1"
                        Session("strMensajeCajaRec") = Session("strMensajeCajaRec") + ". " + CStr(drMsg("MSG"))
                        Exit For
                    End If
                Next
            End If

            'Fin LOGs
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin: AnularPedido - Set_AnularDocumentoJob(" & strCadenaDoc & ", " & strUsuario & ")")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha: " & txtFecha.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "USUARIO: " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "strOficinaVta: " & strOficinaVta)

        Catch ex As Exception
            strCodTrs = "1"
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: AnularPedido)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.StackTrace.ToString())
        End Try
        Return strCodTrs
    End Function

    Private Sub cmdAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnular.Click

        'Objetos
        Dim sDocumentoSap As String = txtDocSap.Text
        Dim ConsultaMssap As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim TrsMssap As New COM_SIC_Activaciones.clsTrsMsSap
        Dim objConf As New COM_SIC_Configura.clsConfigura

        'Variables
        Dim dsPedido As DataSet
        Dim strCodOficina As String = Session("ALMACEN")
        Dim strCodUsuario As String = Session("USUARIO")
        Dim strTipoTienda As String = Session("CANAL")
        Dim strNomVendedor As String = ""
        Dim strIdentifyLog As String
        Dim intAutoriza As Integer
        Dim esNC As String
        Dim nroGeneradoSap As String

        'Obtener Fila Seleccioanda
        Dim dvPagos As New DataView(dtsap)
        Dim filter As String = "PEDIN_NROPEDIDO=" & Request.Item("rbPagos")
        dvPagos.RowFilter = filter
        drFila = dvPagos.Item(0).Row
        Session("drFilaDoc") = drFila

        'Log
        strIdentifyLog = Funciones.CheckStr(drFila.Item("PEDIN_NROPEDIDO"))

        objFileLog.EscribirLog(pathFile, strArchivo, strIdentifyLog, " *** INICIO ANULAR DOCUMENTO POOL DE RECHAZOS SUNAT ***")

        intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, ConfigurationSettings.AppSettings("codAplicacion"), strCodUsuario, Session("NOMBRE_COMPLETO"), "", "", _
                            drFila("PEDIV_NOMBRECLIENTE"), "", "", drFila("PEDIN_NROPEDIDO"), 0, 3, 0, 0, 0, 0, 0, 0, "", strNomVendedor)

        Try
            Dim i As Integer
            If intAutoriza = 1 Then
                '***Consulta PEDIDO 
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    Inicio Consultar Pedido - SP: SSAPSS_PEDIDO")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        IN Nro PEDIDO: " & drFila.Item("PEDIN_NROPEDIDO"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        IN Punto de Venta: " & ConsultaPuntoVenta(Session("ALMACEN")))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        IN Cod Interlocutor: " & "")

                dsPedido = ConsultaMssap.ConsultaPedido(drFila.Item("PEDIN_NROPEDIDO"), ConsultaPuntoVenta(Session("ALMACEN")), "")
                nroGeneradoSap = drFila.Item("PEDIN_NROPEDIDO")

                If Not IsNothing(dsPedido) AndAlso dsPedido.Tables.Count > 0 AndAlso dsPedido.Tables(0).Rows.Count > 0 Then
                    esNC = Funciones.CheckStr(dsPedido.Tables(0).Rows(0).Item("PEDIC_ISFORMAPAGO"))
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "        OUT Es NC: " & esNC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & "    Fin Consultar Pedido - SP: SSAPSS_PEDIDO")
                '**FIN Consulta Pedido

                If esNC = ConfigurationSettings.AppSettings("EsFormaDePago") Then
                    Response.Write("<script>alert('No se pueden Anular Notas de Crédito que fueron usadas como medio de pago');</script>")
                    Exit Sub
                Else

                    strOficinaVta = ConsultaPuntoVenta(strOficinaVta)

                    '*******
                    'If AnularPago() Then
                    '    AnularPedido()
                    'End If

                    Response.Write("<script>alert('Se Anuló el Documento Seleccionado!')</script>")
                    CargarGrilla(strCodOficina)
                End If

            Else
                Response.Write("<script language=jscript> alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador'); </script>")
            End If

        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
            'INI PROY-140126
            Dim MaptPath As String
            MaptPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath.ToString
            MaptPath = "( Class : " & MaptPath & "; Function: cmdAnular_Click)"
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.Message.ToString() & MaptPath)
            'FIN PROY-140126

            objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR: " & ex.StackTrace.ToString())
        End Try

        RegistrarAuditoria("Anulación de Documentos Rechazados por Sunat = " & sDocumentoSap, CheckStr(ConfigurationSettings.AppSettings("codTrsPoolRechazadoSunat")))


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

    Private Function verificarPerfilRechazo() As Boolean
        Dim LOpciones As New ArrayList
        LOpciones = Session("WS_OpcionesPagina")
        Dim strCodOpcionPaginaVerTodos As String = CheckStr(ConfigurationSettings.AppSettings("FE_OpcionPagina_VerRechazos"))
        Dim blnVerTodos As Boolean
        For Each strCodOpcion As String In LOpciones
            If strCodOpcionPaginaVerTodos.IndexOf(strCodOpcion) > -1 Then
                blnVerTodos = True
                Exit For
            End If
        Next
        Return blnVerTodos
    End Function

    Private Function ConsultaPuntoVenta(ByVal P_OVENC_CODIGO As String) As String
        Try
            Dim obj As New COM_SIC_Activaciones.clsConsultaMsSap
            Dim dsReturn As DataSet
            dsReturn = obj.ConsultaPuntoVenta(P_OVENC_CODIGO)
            If dsReturn.Tables(0).Rows.Count > 0 Then
                Return dsReturn.Tables(0).Rows(0).Item("OVENV_CODIGO_SINERGIA")
            End If

            Return Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class
