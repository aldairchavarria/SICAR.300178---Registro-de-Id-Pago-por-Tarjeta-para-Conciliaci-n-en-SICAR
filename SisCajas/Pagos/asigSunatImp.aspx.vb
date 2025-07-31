Imports SisCajas.Funciones
Imports COM_SIC_INActChip
Imports COM_SIC_Activaciones
Imports SisCajas.clsActivaciones
Public Class asigSunatImp
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSunatProp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSunatImp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button
    Protected WithEvents txtSunatActual As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkImprimir As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim drPagos As DataRow
    Dim objPagos As New SAP_SIC_Pagos.clsPagos
    Dim dsResult As DataSet
    Dim strOficina As String
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

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
            Dim strCorrelativo As String
            Dim strCompleto As String
            Dim dsReturn As DataSet
            Dim i As Integer

            strOficina = Session("ALMACEN")

            drPagos = Session("DocSel")
            If Session("Pool") = "" Then
                strCompleto = drPagos.Item("XBLNR")
            End If

'FE
            'If Left(strCompleto, 2) = ConfigurationSettings.AppSettings("k_Prefijo_Ticket") And Trim(Session("CodImprTicket")) <> "" Then
            If ConfigurationSettings.AppSettings("FE_PrefijoComprobantes").IndexOf(Left(strCompleto, 2)) > -1 Then
'FE
                txtNumSunat.ReadOnly = True
                txtNumSunat.CssClass = "clsInputDisable"
                chkImprimir.Checked = True
                chkImprimir.Enabled = True
            End If



            'I-DMZ
            txtSunatProp.Text = drPagos.Item("VBELN")
            txtSunatImp.Text = Format(drPagos.Item("TOTAL"), "###0.00")
            txtSunatActual.Text = drPagos.Item("NAME1")
            'F-DMZ

        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("FechaPago") = drPagos.Item("FKDAT")
        Session("Pool") = ""
        If Session("Consulta") = "1" Then
            Session("Consulta") = ""
            Response.Redirect("PoolConsultaPagos.aspx")
        Else
            Response.Redirect("PoolPagos.aspx")
        End If

    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim strNumSunat As String
        Dim strNumAsignado As String
        Dim strErrorMsg As String
        Dim i As Integer
        Dim intOper As Int32
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim objCajero As New SAP_SIC_Cajas.clsCajas

        Dim strNroDoc As String
        Dim blnFlagPago As Boolean
        Dim strPrepago As String
        Dim strPostPago As String
        Dim intCeros As Integer
        Dim strMensaje As String
        Dim strCadenaCam As String
        Dim j As Integer

        Dim strContrato As String
        Dim PorMigracion As String
        Dim PorRenovacion As String
        Dim PorReposicion As String
        Dim PorAprobador As String
        Dim EstadoActualAcuerdo As String
        Dim strObsEstado As String

        'CARIAS
        Dim strNroSEC, strNroDocSEC, strTipDocSEC, strNroAprobacion, strMotivoMig As String
        'FIN CARIAS

        Dim strIND_ACTIV_BSCS As String
        Dim dsAcuerdo As DataSet
        Dim dsEstado As DataSet
        Dim conEquipo As Boolean
        Dim strValSAP As String
        Dim blnDebeSerRevisado As Boolean
        Dim strRespuesta As String
        Dim strEstado As String
        Dim objActivaciones As New COM_SIC_Activaciones.clsActivacion

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
        Dim Detalle(6, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria

        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPag")
        wParam5 = 1
        wParam6 = "Asignacion/Reasignacion de numero SUNAT"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPRea")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1
        'Fin variables de auditoria

        'If Len(Trim(txtNumSunat.Text)) > 0 Then
        '    strNumSunat = txtNumSunat.Text
        'Else
        '    strNumSunat = txtSunatProp.Text
        'End If

        'AUDITORIA
        Detalle(1, 1) = "Fecha"
        Detalle(1, 2) = drPagos.Item("FKDAT")
        Detalle(1, 3) = "Fecha"

        Detalle(2, 1) = "Factura"
        Detalle(2, 2) = strNumSunat
        Detalle(2, 3) = "Factura"

        Detalle(3, 1) = "DocSap"
        Detalle(3, 2) = drPagos.Item("VBELN")
        Detalle(3, 3) = "Documento SAP"

        Detalle(4, 1) = "Reasigna"
        Detalle(4, 2) = IIf(Len(Trim(Session("ReAsigna"))) > 0, Session("ReAsigna"), "(vacio)")
        Detalle(4, 3) = "Reasignar"

        Detalle(5, 1) = "Oficina"
        Detalle(5, 2) = Session("ALMACEN")
        Detalle(5, 3) = "oficina"

        Detalle(6, 1) = "Usuario"
        Detalle(6, 2) = Session("USUARIO")
        Detalle(6, 3) = "Usuario"

        'FIN AUDITORIA
        Dim strIdentifyLog As String = drPagos.Item("VBELN")
        Dim nroDocSap As String = drPagos.Item("VBELN")
        Dim idLog As String = nroDocSap
        Try


            ' Inicio E77568
            ' PS - Automatización de canje y nota de crédito.
            ' Verificar si existe reserva de puntos Claro Club para efectuar el canje de puntos
            ' por soles de decuento.
            If ExisteCanjePuntosClaroClub(nroDocSap, idLog) Then
                ' Actualiza WS PuntosCC
                If CanjeDescuentoEquipo(nroDocSap, idLog, "") Then
                    ' Actualizar canje puntos SISACT_CANJE_PUNTOS
                    ActualizarCanjePuntos(nroDocSap, txtSunatImp.Text.Trim, idLog)
                End If
            End If
            ' PS - Automatización de canje y nota de crédito.
            ' Fin E77568

            'If Left(drPagos.Item("XBLNR"), 2) = ConfigurationSettings.AppSettings("k_Prefijo_Ticket") And Trim(Session("CodImprTicket")) <> "" Then
            'If Left(txtSunatImp.Text, 2) = ConfigurationSettings.AppSettings("k_Prefijo_Ticket") And Trim(Session("CodImprTicket")) <> "" Then
            
            'I-DMZ
            'Dim Origen As String = "1"
            Dim codUsuario As String = CurrentUser.ToString 'Session("Usuario")
            Dim codOfVenta As String = Session("ALMACEN")
            Dim descTipoDoc As String = drPagos.Item(7) 'campo que describe el tipo de comprobante
            Dim NumFactSap As String = nroDocSap
            Dim tipoDoc As String = ""
            Dim Origen As String = "1"
            Dim resultado As String = ""
            Dim busqueda As DataSet
            Dim objclsVentas As New SAP_SIC_Ventas.clsVentas
            Dim dtDocvnta As DataTable
            Dim claseDoc As String
            Dim v As Integer
            'FE - mejora
            Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
            Dim dsNumCorr As DataSet
            Dim ValRptaCorr As String
            Dim arrayCorr As String()
            Dim SerieSunat As String
            Dim tipoDoc_Sap As String
            Dim blnErrorPago As Boolean = False
            Dim strDocSunat As String
            Dim referencia As String
            Dim objFE As New COM_SIC_FacturaElectronica.PaperLess
            Dim mensajePpl As String = String.Empty
            Dim tipDocSap = drPagos.Item("FKART")
            Dim SociedadSap = drPagos.Item("BUKRS")
            Dim estado As String
            Dim msj As String = ""

            Dim Pago_errores As String = ""
            Dim CorrelativoSunat As String = ""
            Dim verificaEstadoBF As String = ""
            Dim Flag_paperless As String = ""

            'FE - mejora

	    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta al RFC ZPVU_RFC_MAE_CLASE_PEDIDO ")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Punto de Venta:" & codOfVenta)
			
            busqueda = objclsVentas.Get_ConsultaClasePedido(codOfVenta)

            dtDocvnta = busqueda.Tables(0)

            If dtDocvnta.Rows.Count > 0 Then
			    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Se ha encontrado " & dtDocvnta.Rows.Count & " registros.")
                For v = 0 To dtDocvnta.Rows.Count - 1
                    If dtDocvnta.Rows(v)(2) = drPagos.Item("FKART") Then
                        claseDoc = dtDocvnta.Rows(v).Item("KSCHL")
                    End If
                Next
            End If

            If claseDoc = "ZBOL" Then
                tipoDoc = "03"
                tipoDoc_Sap = "E3"
            ElseIf claseDoc = "ZFAC" Then
                tipoDoc = "01"
                tipoDoc_Sap = "E1"
            ElseIf claseDoc = "ZNCV" Then
                tipoDoc = "07"
                tipoDoc_Sap = "E7"
            ElseIf claseDoc = "ZNDV" Then
                tipoDoc = "08"
                tipoDoc_Sap = "E8"
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "claseDoc: " & claseDoc)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc: " & tipoDoc)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc_Sap: " & tipoDoc_Sap)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo de documento: " & drPagos.Item("FKART"))

                Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("FE_Doc_No_Configurado_PDV") & " Doc. SAP : " & nroDocSap & "')</script>")
                Return

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "claseDoc: " & claseDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc: " & tipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc_Sap: " & tipoDoc_Sap)

            If (tipoDoc = "07" Or tipoDoc = "08") Then

            busqueda = objCajas.GetCorrelativoNC(NumFactSap, codOfVenta, "", "")

                If busqueda.Tables.Count > 0 Then

            If busqueda.Tables(0).Rows.Count > 0 Then
                    'FE MEjora log
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultados de la Busqueda de la Nota de Crédito antes de Grabar")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Nro_Documento:" & busqueda.Tables(0).Rows(0)(0).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Tipo_Documento:" & busqueda.Tables(0).Rows(0)(1).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Correlativo_Temp:" & busqueda.Tables(0).Rows(0)(4).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Usuario:" & busqueda.Tables(0).Rows(0)(7).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Oficina_Venta:" & busqueda.Tables(0).Rows(0)(10).ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Origen:" & busqueda.Tables(0).Rows(0)(9).ToString())
                    'FE MEjora log
                Throw New Exception("La nota de credito con Doc. Sap:" & NumFactSap & " ya se encuentra pagada.")
            End If
            'F-DMZ
                Else
                    msj = "No se ha podido consultar la información de la Nota de Crédito para realizar el Pago, volver a Intentar."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msj & "- " & "No devolvio ninguna tabla al hacer la busqueda en SICAR ")
                    Response.Write("<script>alert('" & msj & "')</script>")
                    Return
                End If
               

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio FP_Grabar_PagoNC")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Nro_Documento:" & NumFactSap)  
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Tipo_Documento:" & tipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Usuario:" & codUsuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Oficina_Venta:" & codOfVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp Origen:" & Origen)
            

                ' If Trim(Session("CodImprTicket")) <> "" Then
                'dsResult = objPagos.Set_NumeroSUNAT(drPagos.Item("VBELN"), strNumSunat, "", strOficina, Session("ReAsigna"), drPagos.Item("FKDAT"), "", "", "", Session("USUARIO"), Trim(Session("CodImprTicket")), "", strNumAsignado)
                If Session("ReAsigna") = "S" Then
                    Throw New Exception("No debe reasignar documentos cuando se ha elegido una ticketera")
                End If
                'I-DMZ
                'dsResult = objCajero.Set_NroSunatCajero_FE(drPagos.Item("VBELN"), strNumSunat, "", strOficina, Session("ReAsigna"), drPagos.Item("FKDAT"), "", "", "", Session("USUARIO"), Trim(Session("CodImprTicket")), "", strNumAsignado)
                resultado = objCajas.FP_Grabar_PagoNC(NumFactSap, tipoDoc, codUsuario, Origen, codOfVenta)
                ' End If
            strErrorMsg = ""
            If resultado <> "" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out Resultado:" & resultado)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_Grabar_PagoFicticioNC")
                    msj = "El pago se registro correctamente."
                Response.Write("<script>alert('" & msj & "')</script>")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out Resultado:" & resultado)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha realizado el Pago Ficticio")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin FP_Grabar_PagoFicticioNC")
            End If
                Response.Redirect("PoolPagos.aspx")

            End If

            'FE- Adicion para pagos que no sean NC, ND



            'FE CAMBIO SALTO CORRELATIVO
            busqueda = objCajas.GetCorrelativoNC(NumFactSap, codOfVenta, "", "")

            If busqueda.Tables.Count > 0 Then
                'BUSQUEDa si el documento no ha sido enviado a paperless
                If busqueda.Tables(0).Rows.Count > 0 AndAlso Not busqueda.Tables(0).Rows(0)("CORRELATIVO") Is Nothing Then
                    SerieSunat = busqueda.Tables(0).Rows(0)("SERIE").ToString() 'serie
                    strNumSunat = busqueda.Tables(0).Rows(0)("CORRELATIVO").ToString() 'CORRELATIVO
                    referencia = SerieSunat & " - " & strNumSunat
                Else
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Obtencion del correlativo de la Boleta o Factura (SSAPSS_CORRELATIVOFE)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta              :" & codOfVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo de Documento           :" & tipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Documento de Referencia     :" & "")

            dsNumCorr = objCajas.FP_CalcularCorrelativo(codOfVenta, tipoDoc, "")

                    If dsNumCorr.Tables.Count > 0 Then

            If dsNumCorr.Tables(0).Rows.Count = 0 Then
                            msj = "No Se ha formado el correlativo sunat para la Boleta o Factura."
                Response.Write("<script>alert('" & msj & "')</script>")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha generado el correlativo de la Boleta o Factura.")
                Return
            Else

                            ValRptaCorr = dsNumCorr.Tables(0).Rows(0).Item(0).ToString()
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo           :" & ValRptaCorr)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la obtención del correlativo de la Boleta o Factura.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                        End If
                    Else

                        msj = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msj)
                        Response.Write("<script>alert('" & msj & "')</script>")
                        Return

            End If

            arrayCorr = ValRptaCorr.Split("-")
            SerieSunat = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1) 'serie
            strNumSunat = (CInt(arrayCorr(1).ToString)).ToString   'Correlativo


                    If SerieSunat <> "" Then
                        referencia = SerieSunat & "-" & strNumSunat

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Registro de datos del pago de la Boleta o Factura (SP_SET_CORRELATIVO_BF)")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap            :" & NumFactSap)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo de Documento           :" & tipoDoc)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Serie Sunat                 :" & SerieSunat)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Correlativo Sunat           :" & strNumSunat)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Código de Usuario           :" & CurrentUser.ToString)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen                      :" & Origen)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta              :" & codOfVenta)

                        resultado = objCajas.FP_Grabar_PagoBF(NumFactSap, tipoDoc, SerieSunat, strNumSunat, CurrentUser.ToString, Origen, codOfVenta)
                        If resultado = "" Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado              :" & "No se ha Grabado los Datos del Pago.")

                        Else
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado           :" & "Registro de Datos del pago Exitoso.")
                        End If

                        Dim corrCompleto As String = "00" & "|" & ValRptaCorr
                        Dim updCorrCompleto As String = objCajas.SP_UPD_FLAG_PAPER(NumFactSap, "V", corrCompleto)

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin del Registro de datos de la Boleta o Factura.")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
                    End If
                End If

            Else

                msj = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msj)
                Response.Write("<script>alert('" & msj & "')</script>")
                Return

            End If
            'FE CAMBIO SALTO CORRELATIVO


            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio del RFC (Zpsd_Rfc_Trs_Caj_Set_Nro_Sunat) para la Boleta o Factura")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Documento	  :" & drPagos.Item("VBELN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Asignar        :" & strNumSunat)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Nro_Serie      :" & SerieSunat)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Tipo_Doc       :" & tipoDoc_Sap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Ref_Franquicia :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Oficina_Venta      :" & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Reasignar          :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Fecha              :" & drPagos.Item("FKDAT"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp strPagos           :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Ref_Corner     :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo_Doc_Corner    :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen             :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Usuario            :" & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Caja               :" & "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nota_Credito       :" & "")


            'strFacturaSunat = tipoDoc_Sap & "-" & Format(CheckInt(SerieSunat), "00000") & "-" & Format(CheckInt(strNumSunat), "0000000")

            Try
                'FE Mejora salto correlativo
                dsResult = objSapCajas.Set_NroSunatCajero_FE(drPagos.Item("VBELN"), strNumSunat, SerieSunat, tipoDoc_Sap, "", Session("ALMACEN"), "", drPagos.Item("FKDAT"), "", "", "", "", Session("USUARIO"), "", "", strNumAsignado)
                'FE Mejora salto correlativo
                For i = 0 To dsResult.Tables(0).Rows.Count - 1
                    If dsResult.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " ERROR: " & dsResult.Tables(0).Rows(i).Item("MESSAGE"))
                        blnErrorPago = True
                        Pago_errores = "NVS" 'No Valida Saldo
                        strErrorMsg = dsResult.Tables(0).Rows(i).Item(3)
                    End If
                Next
            Catch ex As Exception
                blnErrorPago = True
                Pago_errores = "NVS"
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR : " & ex.Message.ToString())
            End Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strNumAsignado:" & strNumAsignado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "FIn Set_NroSunatCajero_FE (Zpsd_Rfc_Trs_Caj_Set_Nro_Sunat)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
            strDocSunat = strNumAsignado

            If (blnErrorPago And Pago_errores = "NVS") Or strNumAsignado = "" Then
            
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Oficina_Venta      :" & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Documento	  :" & drPagos.Item("VBELN"))


                dsNumCorr = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                If dsNumCorr.Tables.Count > 0 Then

                    CorrelativoSunat = dsNumCorr.Tables(0).Rows(0)("NRO_REFERENCIA").ToString()

                    If (CorrelativoSunat = "0000000000000000" Or CorrelativoSunat = "") Then

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha generado el correlativo de la Boleta o Factura.")
                        msj = "No se ha realizado el pago correctamente, por favor volver a Pagar."
                        Response.Write("<script>alert('" & msj & "')</script>")
                        'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha generado el correlativo de la Boleta o Factura.")
                        Exit Sub

                    Else

                        arrayCorr = (CorrelativoSunat).Split("-")
                        SerieSunat = (arrayCorr(1).ToString).Substring(1, (arrayCorr(1).ToString).Length - 1)  'serie
                        strNumSunat = (CInt(arrayCorr(2).ToString)).ToString()
                referencia = SerieSunat & "-" & strNumSunat
                        blnErrorPago = False

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo Sunat      :" & dsNumCorr.Tables(0).Rows(0)("NRO_REFERENCIA").ToString())
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
                    End If

                Else

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo Sunat      :" & dsNumCorr.Tables(0).Rows(0)("NRO_REFERENCIA").ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                    msj = "Ocurrio un error al momento de consultar datos del Pedido, volver a realizar el Pago."
                    Response.Write("<script>alert('" & msj & "')</script>")
                    Exit Sub

                End If


            End If

            If Not blnErrorPago Then


                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de la verificación si anteriormente se ha enviado a la Sunat las Boletas o Facturas")

                busqueda = objCajas.GetCorrelativoNC(NumFactSap, codOfVenta, "", "")

                If busqueda.Tables.Count > 0 Then
                    verificaEstadoBF = (busqueda.Tables(0).Rows(0)("ESTADO")).ToString()
                    Flag_paperless = IIf(IsDBNull(busqueda.Tables(0).Rows(0)("FLAG_PAPERLESS")), "", busqueda.Tables(0).Rows(0)("FLAG_PAPERLESS").ToString())
                End If


                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado de la busqueda :" & verificaEstadoBF)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la verificación si anteriormente se ha enviado a la Sunat las Boletas o Facturas")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")


                If verificaEstadoBF = "1" And Flag_paperless = "" Then

                mensajePpl = _
                objFE.GenerarFacturaElectronica( _
                    drPagos.Item("VBELN"), _
                    Session("ALMACEN"), _
                    referencia, _
                        SociedadSap, "", "") 'MMR - NUEVOS PARAMETROS

                RegistrarAuditoria("Envio factura electrónica NumSunat = " & referencia, CheckStr(ConfigurationSettings.AppSettings("codTrsPaperless")))


                estado = mensajePpl

                If estado = "F" Then
                    estado = "E"
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualización del Estado (SP_UPD_ESTADO_DOCUM)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap   :" & drPagos.Item("VBELN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen             :" & "1")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Estado             :" & estado)

                Dim updateEstado As String = objCajas.Actualizar_Estado_Pago(drPagos.Item("VBELN"), "1", estado)

                If updateEstado = "" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la ctualización del Estado de la Boleta o Factura.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
                End If
            End If

            If Len(Trim(strErrorMsg)) > 0 Then
                wParam5 = 0
                wParam6 = "Error en Asignacion/Reasignacion de numero SUNAT. " & strErrorMsg
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Write("<script>alert('" & strErrorMsg & "')</script>")
            Else

                ' Inicio E77568
                If ExisteNdDevolDscto(drPagos.Item("VBELN"), strIdentifyLog) Then
                    Call ActualizarNdDevolDscto(drPagos.Item("VBELN"), _
                                             strIdentifyLog)
                ElseIf ExisteDevolucionPuntosCC(drPagos.Item("VBELN"), strIdentifyLog) Then
                    ' Actualiza WS devolucion PuntosCC
                    CanjeDevolucionEquipo(drPagos.Item("VBELN"), _
                                          strIdentifyLog)

                    ' Actualizar la devolución del canje puntos SISACT_CANJE_PUNTOS_DEVOL
                    ActualizarDevolucionCC(drPagos.Item("VBELN"), _
                                           strIdentifyLog)
                End If
                ' Fin IDEA-13006 ClaroClub - Mejoras en notas de créditos
                ' Fin E77568

                Session("Pool") = ""
                Session("FechaPago") = drPagos.Item("FKDAT")
                Dim sUrl As String = "PoolPagos.aspx"
                If Session("Consulta") = "1" Then
                    sUrl = "PoolConsultaPagos.aspx"
                End If
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

                If Session("ReAsigna") <> "S" Then

                    '----- CAMPAÑA PRE + POST

                    'dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                    dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                    If Not IsNothing(dsResult) Then
                        strNroDoc = dsResult.Tables(0).Rows(0).Item("DOCUMENTO")
                    Else
                        strNroDoc = ""
                    End If
                    blnFlagPago = True
                    If strNroDoc <> "" Then
                        dsResult = objPagos.ConsultaTriacionPrePost(strNroDoc, "", "", "")
                        If Not IsNothing(dsResult) Then
                            'blnFlagPago = True
                            For i = 0 To dsResult.Tables(0).Rows.Count - 1
                                strPrepago = dsResult.Tables(0).Rows(i).Item("NRO_DOC_PREPAGO")
                                strPostPago = dsResult.Tables(0).Rows(i).Item("NRO_DOC_POSTPAGO")
                                intCeros = (Len(strNroDoc) - Len(strPrepago))
                                If intCeros > 0 Then
                                    For j = 1 To intCeros
                                        strPrepago = "0" & strPrepago
                                    Next
                                End If

                                intCeros = Len(strNroDoc) - Len(strPostPago)
                                If intCeros > 0 Then
                                    For j = 1 To intCeros
                                        strPostPago = "0" & strPostPago
                                    Next
                                End If

                                If strNroDoc = strPrepago Then
                                    strEstado = ConfigurationSettings.AppSettings("gstrCamEstadoVendido")
                                    strMensaje = ConfigurationSettings.AppSettings("gstrMsgPagodePre")
                                    If dsResult.Tables(0).Rows(i).Item("ESTADO") <> ConfigurationSettings.AppSettings("gstrCamEstadoPagado") Then
                                        blnFlagPago = False
                                    End If
                                End If
                                If strNroDoc = strPostPago Then
                                    strEstado = ConfigurationSettings.AppSettings("gstrCamEstadoPagado")
                                    strMensaje = ConfigurationSettings.AppSettings("gstrMsgPagodePost")
                                    If dsResult.Tables(0).Rows(i).Item("ESTADO") <> ConfigurationSettings.AppSettings("gstrCamEstadoUsado") Then
                                        blnFlagPago = False
                                    End If
                                End If

                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("SECUENCIAL") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("OFICINA_VENTA") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("CLIENTE") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("TIPO_DOC_CLIENTE") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("NRO_TEL_PREPAGO") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("NRO_DOC_PREPAGO") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("NRO_TEL_POSTPAGO") & ";"
                                strCadenaCam = strCadenaCam & dsResult.Tables(0).Rows(i).Item("NRO_DOC_POSTPAGO") & ";"
                                strCadenaCam = strCadenaCam & strEstado & "|"

                            Next
                            If Len(Trim(strCadenaCam)) > 0 Then
                                strCadenaCam = Mid(strCadenaCam, 1, Len(strCadenaCam) - 1)
                            End If
                        End If

                        If Not blnFlagPago Then
                            Session("strMensajeCaja") = strMensaje
                            'Response.Write("<script>alert('" & strMensaje & "')</script>")
                            Response.Redirect("PoolPagos.aspx")
                        End If

                    End If

                    ' FIN CAMPAÑA PRE + POST

                    Session("mensajeCHIPRepuesto") = ""
                    ActivaChipRepuesto(drPagos.Item("VBELN"))

                    Dim strDocSap As String = drPagos.Item("VBELN")

    
                    'Agregar codigo de activacion
                    strContrato = drPagos.Item("NRO_CONTRATO")
                    If strContrato = "" Then
                        'dsResult = objPagos.Get_ConsultaNroContrato(Session("ALMACEN"), "", drPagos.Item("VBELN"), strContrato)
                        dsResult = objPagos.Get_ConsultaNroContrato(Session("ALMACEN"), "", drPagos.Item("VBELN"), strContrato)
                    End If

                    'If Trim(drPagos.Item("NUMBR")) <> "" Then  'Running Program
                    '    Dim dsRunning As DataSet
                    '    dsRunning = objPagos.Get_ConsultaPedidoPreVenta(Trim(drPagos.Item("NUMBR")))
                    '    strContrato = dsRunning.Tables(0).Rows(0).Item("NUMERO_CONTRATO")
                    'End If  'Running Program

                    If Not IsNumeric(strContrato) Then
                        strContrato = "KO"
                    End If

                    Dim codRespConvergente As String = ""
                    codRespConvergente = EjecutarActivacion(strDocSap, strNumAsignado, drPagos.Item("TOTAL"), CurrentUser, CurrentTerminal, strNroSEC)

                    If codRespConvergente = CheckStr(ConfigurationSettings.AppSettings("codRespDocSapNoConvergente")) Then
                        If strContrato <> "KO" And CDbl(strContrato) > 0 Then
                            dsAcuerdo = objPagos.Get_ConsultaAcuerdoPCS(strContrato)
                            If dsAcuerdo.Tables(0).Rows.Count > 0 Then
                                PorMigracion = dsAcuerdo.Tables(0).Rows(0).Item("MIGRACION")
                                PorRenovacion = dsAcuerdo.Tables(0).Rows(0).Item("RENOVACION")
                                PorReposicion = dsAcuerdo.Tables(0).Rows(0).Item("REPOSICION")
                                PorAprobador = dsAcuerdo.Tables(0).Rows(0).Item("APROBADOR")
                                EstadoActualAcuerdo = dsAcuerdo.Tables(0).Rows(0).Item("ESTADO")
                                strNroAprobacion = dsAcuerdo.Tables(0).Rows(0).Item("NRO_APROBACION")
                                'CARIAS
                                strNroSEC = dsAcuerdo.Tables(0).Rows(0).Item("NRO_CARPETA_OBS")
                                strNroDocSEC = dsAcuerdo.Tables(0).Rows(0).Item("CLIENTE")
                                strTipDocSEC = dsAcuerdo.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE")
                                strMotivoMig = dsAcuerdo.Tables(0).Rows(0).Item("MOTIVO_MIG")
                                'FIN CARIAS                               

                                dsResult = objPagos.Get_ConsultaEstadoPCS(strContrato)
                                For i = 0 To dsResult.Tables(0).Rows.Count - 1
                                    If dsResult.Tables(0).Rows(i).Item("ESTADO") = "6" Then
                                        strObsEstado = dsResult.Tables(0).Rows(i).Item("OBSERVACION")
                                    End If
                                Next

                                If EstadoActualAcuerdo = "6" Then

                                    'dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                                    dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                                    strIND_ACTIV_BSCS = dsResult.Tables(0).Rows(0).Item("IND_ACTIV_BSCS")

                                    'Mejora en Bandeja de Renovaciones
                                    If dsAcuerdo.Tables(1).Rows(0).Item("COD_EQUIPO") <> "" Then
                                        conEquipo = True
                                    Else
                                        conEquipo = False
                                    End If

                                    strValSAP = dsResult.Tables(0).Rows(0).Item("REN_REP_HDC")

                                    If (Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "") And (strValSAP <> "X") Then 'Not(valSAP=="X"): VIA_DSF = ‘S’
                                        blnDebeSerRevisado = True
                                    End If
                                    If blnDebeSerRevisado Then
                                        strEstado = ConfigurationSettings.AppSettings("CTE_ESTREVISADO")
                                    Else
                                        strEstado = ConfigurationSettings.AppSettings("CTE_ESTNUEVO")
                                    End If

                                    dsEstado = objPagos.Set_EstadoAcuerdoPCS(strContrato, strEstado, ConfigurationSettings.AppSettings("gStrUsuarioActivacionWS"), "", "", "", strObsEstado, "")

                                    'If Trim(PorMigracion) <> "" Or Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "" Then
                                    If (Trim(PorMigracion) <> "" And Trim(strMotivoMig) = "") Or Trim(PorRenovacion) <> "" Or Trim(PorReposicion) <> "" Then
                                        strRespuesta = ""
                                    Else
                                        If Trim(PorAprobador) = "4" Then
                                            strRespuesta = ""
                                        Else

                                            'CARIAS: Post + Control

                                            Dim strTIPO_ACTIV_CLTE As String
                                            strTIPO_ACTIV_CLTE = drPagos.Item("TIPO_ACTIV_CLTE")

                                            If Trim(strTIPO_ACTIV_CLTE) = "" Then
                                                strTIPO_ACTIV_CLTE = 0
                                            End If

                                            If strTIPO_ACTIV_CLTE = 0 Then
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Inicio getCuentasCliente-----")
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strTipDocSEC --> " & strTipDocSEC)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strNroDocSEC --> " & strNroDocSEC)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Input: strNroAprobacion --> " & strNroAprobacion)

                                                Dim objCuentas As New COM_SIC_Activaciones.clsCuenta
                                                Dim strResultEAI As String
                                                Dim arrCuentas As New ArrayList
                                                Dim strMsgCuentasCliente As String
                                                arrCuentas = objActivaciones.getCuentasCliente(strTipDocSEC, strNroDocSEC, strNroAprobacion, strResultEAI, strMsgCuentasCliente)

                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Outpu: strResultEAI --> " & strResultEAI)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Outpu: strMsgCuentasCliente --> " & strMsgCuentasCliente)
                                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Fin getCuentasCliente-----")

                                                If strResultEAI = "1" Then
                                                    Dim cantLineas As Integer = 0
                                                    Dim cantCuentas As Integer = arrCuentas.Count
                                                    For Each cuenta As COM_SIC_Activaciones.clsCuenta In arrCuentas
                                                        If Not cuenta.linea Is Nothing Then
                                                            cantLineas = cantLineas + cuenta.linea.Length
                                                        End If
                                                    Next
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     cantLineas --> " & cantLineas.ToString())
                                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     cantCuentas --> " & cantCuentas.ToString())
                                                    If cantLineas <= 19 Then
                                                        If cantCuentas <= 9 Then
                                                            objPagos.Set_ActualizaPCS(strContrato, "1", "1", "")
                                                            strTIPO_ACTIV_CLTE = "1"
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            'FIN CARIAS: Post + Control

                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Inicio Proceso Activacion-----")
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input: IND_ACTIV_BSCS --> " & strIND_ACTIV_BSCS)
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input: ACTIVACION_LINEA --> " & CheckStr(drPagos.Item("ACTIVACION_LINEA")))

                                            'If Trim(drPagos.Item("NUMBR")) = "" Then							   'CARIAS: Indica que el pedido no vino por Running Program 24/04/2007
                                            If strIND_ACTIV_BSCS <> "" Then
                                                If drPagos.Item("ACTIVACION_LINEA") = ConfigurationSettings.AppSettings("gStrValorActLin") Then
                                                    If CheckStr(strMotivoMig) <> "104" Then
                                                        Try
                                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Input: strContrato --> " & strContrato)
                                                            strRespuesta = objActivaciones.FK_ActivacionClienteRecurrente(strContrato)
                                                        Catch ex As Exception
                                                            strRespuesta = "Error Activación. " + ex.Message.ToString()
                                                        End Try
                                                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Output: strRespuesta --> " & strRespuesta)
                                                    End If
                                                End If
                                            End If
                                            'If
                                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "-----Fin Proceso Activacion-----")
                                        End If

                                    End If
                                End If
                            End If
                        End If
                        ' Fin de codigo de activacion
                    End If
                    'CARIAS: SEC marcada como pagada
                    If Len(Trim(strNroSEC)) > 0 Then
                        objCajas.FP_Actualiza_Pago_Solicitud(strNroSEC, "X")
                    End If
                    'FIN CARIAS: SEC marcada como pagada

                    'INICIO ACTUALIZAR LA TABLA DE CONTROL (Post + Pre)
                    Dim arrCampana() As String
                    Dim intCampana As Integer

                    If Trim(strCadenaCam) <> "" Then
                        arrCampana = Split(strCadenaCam, "|")
                        If UBound(arrCampana) >= 0 Then
                            For intCampana = 0 To UBound(arrCampana)
                                dsResult = objPagos.Set_TriacionPrePost(arrCampana(intCampana))
                            Next
                        End If
                    End If
                    'FIN ACTUALIZAR LA TABLA DE CONTROL (Post + Pre)

                End If
                dsResult = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
                If Not IsNothing(dsResult) Then

                    intOper = objCajas.FP_Cab_Oper(Session("CANAL"), Session("ALMACEN"), ConfigurationSettings.AppSettings("codAplicacion"), Session("Usuario"), dsResult.Tables(0).Rows(0).Item("TIPO_DOC_CLIENTE"), _
                                dsResult.Tables(0).Rows(0).Item("CLIENTE"), dsResult.Tables(0).Rows(0).Item("TIPO_DOCUMENTO"), drPagos.Item("VBELN"), dsResult.Tables(0).Rows(0).Item("REFERENCIA"), _
                                dsResult.Tables(0).Rows(0).Item("TOTAL_MERCADERIA"), dsResult.Tables(0).Rows(0).Item("TOTAL_IMPUESTO"), dsResult.Tables(0).Rows(0).Item("TOTAL_DOCUMENTO"), "P")

                    For i = 0 To dsResult.Tables(1).Rows.Count - 1
                        objCajas.FP_Det_Oper(intOper, i + 1, dsResult.Tables(1).Rows(i).Item("ARTICULO"), dsResult.Tables(1).Rows(i).Item("SERIE"), dsResult.Tables(1).Rows(i).Item("NUMERO_TELEFONO"), _
                        dsResult.Tables(1).Rows(i).Item("CANTIDAD"), dsResult.Tables(1).Rows(i).Item("SUBTOTAL"), dsResult.Tables(1).Rows(i).Item("IMPUESTO1"), dsResult.Tables(1).Rows(i).Item("SUBTOTAL") + dsResult.Tables(1).Rows(i).Item("IMPUESTO1"))
                    Next
                End If

                dsResult = objPagos.Get_ParamGlobal(Session("ALMACEN"))
                'si la impresion no es por SAP, abrir cuadro de impresoras

                'I-DMZ impresion  de esta pagina no se realizara
                If Trim(dsResult.Tables(0).Rows(0).Item("IMPRESION_SAP")) = "" Then
                '    If Me.chkImprimir.Checked Then
                    sUrl += "?pImp=S" + _
                                "&pDocSap=" + drPagos.Item("VBELN") + _
                                "&pDocSunat=" + strNumAsignado
                '    End If
                End If
                Response.Redirect(sUrl)
                'F-DMZ
                'I-DMz
                'Response.Redirect("PoolPagos.aspx")
                'F-DMZ
            End If
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "')</script>")
        End Try

    End Sub

    Sub ActivaChipRepuesto(ByVal strNroPedidoSAP As String)

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

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogCambioSIM")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogCambioSIM")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
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

        'strSolicitud = "1;0197204226;895110010400404862;716100100740825;0013;;R01;895110010401250542;;0000010380;1;28/06/2005;05:35:33;;;;;;"

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

            If strTipoVenta = ConfigurationSettings.AppSettings("strTVPrepago") Then 'or strTipoVenta = strTVControl then  'CARIAS

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
                    Dim oConsulta As New COM_SIC_INActChip.clsBDSiscajas
                    Dim dsUsuario As DataSet

                    Do While InStr(1, vendedor, "0") = 1
                        vendedor = Mid(vendedor, 2, Len(vendedor))
                    Loop
                    dsUsuario = oConsulta.FP_ConsultaUsuarioRed(vendedor)

                    If Not IsNothing(dsUsuario) Then
                        If dsUsuario.Tables(0).Rows.Count > 0 Then
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

                                    retornoCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                            OLD_ICCID, _
                                                                            strID, _
                                                                            strIP, _
                                                                            strEmpleado, _
                                                                            ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                            ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                            ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                            mensajeCambioSIM)

                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoCambioSIM)
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

                                retornoCambioSIM = oCambioSIM.CambioSIM(MSISDN, _
                                                                        OLD_ICCID, _
                                                                        strID, _
                                                                        strIP, _
                                                                        strEmpleado, _
                                                                        ConfigurationSettings.AppSettings("constAplicacion"), _
                                                                        ConfigurationSettings.AppSettings("constTimeOutWSCambioSIM"), _
                                                                        ConfigurationSettings.AppSettings("constRutaWSCambioSIM"), _
                                                                        mensajeCambioSIM)

                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Resultado : " & retornoCambioSIM)
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

                    If (strTipoVenta = ConfigurationSettings.AppSettings("strTVPrepago")) And (strESTADOINI = "3") Then ' (strMotivo = "R01" or strMotivo = "R02") then
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

                    strCodError = Split(arrFilCHIP(2), ";")(1) 'Codigo de mensaje
                    strMenError = Split(arrFilCHIP(2), ";")(2) 'Descripcion de mensaje
                    'Response.Write(strCodError):Response.End 

                    dsResult = objPagos.Set_LogVariacion(strConsulta, "RETCODE;ERRORMSG", strCodError & ";" & strMenError)

                    'Response.Write(strResLog):Response.end

                    'if arrResulFilCHIPError(0)<>"XX00XX" then
                    If IsNumeric(strCodError) Then
                        If CInt(strCodError) = 0 Then
                            Session("FLAGCHIPREP") = "S"

                            arrSolicitud(13) = ConfigurationSettings.AppSettings("gstrEstSolicVariado")
                            arrSolicitud(8) = "" 'strIMSI_NUEVO
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
                        If CInt(strCodError) <> 0 Then 'error en el cambio de chip 
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
            End If  'CARIAS 	
        Else
            Session("FLAGCHIPREP") = ""
        End If
        '******************************Fin Consulta**************************************

    End Sub

    Function ValorResultCHIP(ByVal strNombreCampo As String, ByVal arrResulFilCHIPNomb() As String, ByVal arrResulFilCHIPValor() As String)
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
            If InStr(1, aux, "1") = 1 Then 'Si es lima adicionar 0 adelante
                aux = "0" & aux
            End If
        End If
        If aux = "" Then
            FormatoTelefono = telefono
        Else
            FormatoTelefono = aux
        End If
    End Function

    Function FormatoTelefonoPrepago(ByVal telefono)
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
    End Function

    Private Function DatosInteraccion() As Interaccion

        Dim oInteraccion As New Interaccion

        'oInteraccion.TIPO = ConfigurationSettings.AppSettings("TIPO_DEFAULT")
        'oInteraccion.SUBCLASE = ConfigurationSettings.AppSettings("SUBCLASE_CHIP")
        oInteraccion.TIPO_INTER = ConfigurationSettings.AppSettings("TIPO_INTER_DEFAULT")
        oInteraccion.METODO = ConfigurationSettings.AppSettings("METODO_DEFAULT")
        'oInteraccion.AGENTE = Session("strUsuario")
        oInteraccion.USUARIO_PROCESO = ConfigurationSettings.AppSettings("USR_PROCESO")
        oInteraccion.HECHO_EN_UNO = ConfigurationSettings.AppSettings("ECHO_UNO_DEFAULT")
        oInteraccion.FLAG_CASO = ConfigurationSettings.AppSettings("FLAG_CASO_DEFAULT")
        oInteraccion.RESULTADO = ConfigurationSettings.AppSettings("RESULTADO_DEFAULT")

        Return oInteraccion
    End Function

    Public Sub ActualizarCanjePuntos(ByVal NroDocSap As String, _
                                     ByVal NroReferenciaImprimir As String, _
                                     ByVal strIdentifyLog As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ActualizarCanjePuntos()-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroReferenciaImprimir : " & NroReferenciaImprimir)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  USUARIO : " & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CONS_FLAG_CANJE : " & ConfigurationSettings.AppSettings("CONS_FLAG_CANJE"))

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            objCajas.actualizarPuntosClaroClub(NroDocSap, _
                                               NroReferenciaImprimir, _
                                               Session("USUARIO"), _
                                               ConfigurationSettings.AppSettings("CONS_FLAG_CANJE"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente actualizarPuntosClaroClub()")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  ERROR : " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ActualizarCanjePuntos()-----------")
        End Try
    End Sub


    Public Function ExisteCanjePuntosClaroClub(ByVal NroDocSap As String, _
                                               ByVal strIdentifyLog As String) As Boolean
        Dim bExiste As Boolean = False
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ExisteCanjePuntosClaroClub()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dt As DataTable
            Dim CONS_FLAG_CANJE As String = CheckStr(ConfigurationSettings.AppSettings("CONS_FLAG_CANJE"))
            dt = objCajas.ListarCanjePuntos(NroDocSap)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If dr.Item("FLAG_CANJE").ToString <> CONS_FLAG_CANJE Then
                    bExiste = True
                End If
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   out  bExiste : " & bExiste)

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT ERROR : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT ERROR : " & ex.StackTrace.ToString())

            bExiste = False
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ExisteCanjePuntosClaroClub()")
        End Try
        Return bExiste
    End Function


    Public Function CanjeDescuentoEquipo(ByVal NroDocSap As String, _
                                    ByVal strIdentifyLog As String, ByVal nroLiena As String) As Boolean

        Dim blnRespuesta As Boolean
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio CanjeDescuentoEquipo(): " & ConfigurationSettings.AppSettings("WSCanjePuntosCC_url"))
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dtResultado As DataTable
        Dim drResultado As DataRow
        Dim numDocumento As String

        Try
            drPagos = Session("DocSel")
            dtResultado = objCajas.ListarCanjePuntos(NroDocSap)
            If dtResultado.Rows.Count > 0 Then
                drResultado = dtResultado.Rows(0)
            End If

            Dim objCanjeDevolucionServicioComercialService As New CanjeDescuentoEquipoWS.ebsCanjeDevolucionServicioComercialService
            objCanjeDevolucionServicioComercialService.Url = ConfigurationSettings.AppSettings("WSCanjePuntosCC_url")
            objCanjeDevolucionServicioComercialService.Timeout = Convert.ToInt32((ConfigurationSettings.AppSettings("WSCanjePuntosCC_timeout")))
            Dim objCanjeDescuentoEquipoRequest As New CanjeDescuentoEquipoWS.canjeDescuentoEquipoRequest
            Dim objInfoDeTransacccion As New CanjeDescuentoEquipoWS.InfoDeTransaccionType
            Dim objCanjeDescuentoEquipoResponse As New CanjeDescuentoEquipoWS.canjeDescuentoEquipoResponse

            objInfoDeTransacccion.usrApp = CurrentUser  ' Usuario de Aplicación que invoca al servicio.
            objInfoDeTransacccion.usrName = Session("NOMBRE_COMPLETO") ' Nombre de usuario de aplicación cliente.
            objInfoDeTransacccion.aplicacion = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO") ' Alias de la aplicación que invoca al servicio.
            objInfoDeTransacccion.estacion = System.Net.Dns.GetHostName ' Nombre de la aplicación que invoca al servicio.
            objInfoDeTransacccion.ipAplicacion = Request.ServerVariables("REMOTE_HOST") ' IP de la aplicación que invoca el servicio..
            Dim idTime As String = Now.ToString("yyyyMMddHHmmss")
            objInfoDeTransacccion.fechaHora = idTime ' Fecha y hora de la transacción.
            objInfoDeTransacccion.idTransaccion = NroDocSap ' ID que debe ser generada por el cliente que invoca al servicio.
            objCanjeDescuentoEquipoRequest.infoDeTransaccion = objInfoDeTransacccion

            numDocumento = drResultado.Item("NUM_DOC")
            objCanjeDescuentoEquipoRequest.nombreCliente = drPagos.Item("NAME1") ' Nombre de Cliente.
            objCanjeDescuentoEquipoRequest.tipoDocumento = Funciones.CheckStr(drResultado("ID_CCLUB")) ' Tipo de Documento.
            objCanjeDescuentoEquipoRequest.numDocumento = numDocumento ' Numero del Documento.
            objCanjeDescuentoEquipoRequest.puntoVenta = Session("ALMACEN") ' Punto de Venta.
            objCanjeDescuentoEquipoRequest.codAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            ' Inicio SD_812077 
            objCanjeDescuentoEquipoRequest.codAsesor = Funciones.CheckStr(drResultado("USUARIO_REG"))  ' Código del asesor
            Dim dsPedido As DataSet = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), NroDocSap, "")
            Dim codVendedor As String = dsPedido.Tables(0).Rows(0).Item("VENDEDOR")
            Dim dsVendedor As DataSet = objPagos.Get_ConsultaVend(codVendedor)
            Dim strNomVendedor As String = CheckStr(dsVendedor.Tables(0).Rows(0).Item("NOMBRE"))
            objCanjeDescuentoEquipoRequest.nomAsesor = strNomVendedor ' Nombre del asesor
            ' Fin SD_812077
            objCanjeDescuentoEquipoRequest.idVenta = NroDocSap ' Codigo del Id de Venta.
            objCanjeDescuentoEquipoRequest.idProceso = ConfigurationSettings.AppSettings("CONST_VTA_RENOVACION") ' Tipo de Operacion  : AP(Vta Alta Postpago)/AE(Vta Alta Prepago)/VR(Vta Renovación).
            objCanjeDescuentoEquipoRequest.puntosVenta = CInt(Funciones.CheckInt64(drResultado("PUNTOS_USADOS")).ToString)
            objCanjeDescuentoEquipoRequest.solesVenta = CInt(Funciones.CheckInt64(drResultado("SOLES_DESCUENTO")).ToString)
            objCanjeDescuentoEquipoRequest.idCampana = Funciones.CheckStr(drResultado("IDCAMPANA")) ' Codigo de Campaña
            objCanjeDescuentoEquipoRequest.usuario = CurrentUser
            objCanjeDescuentoEquipoRequest.codigoCliente = nroLiena
            objCanjeDescuentoEquipoRequest.segmento = Funciones.CheckStr(drResultado("SEGMENTO"))
            objCanjeDescuentoEquipoRequest.lineaTipificacion = Funciones.CheckStr(drResultado("NRO_LINEA")) 'NRO_LINEA

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp usrApp : " & objInfoDeTransacccion.usrApp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp usrName : " & objInfoDeTransacccion.usrName)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp aplicacion : " & objInfoDeTransacccion.aplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp estacion : " & objInfoDeTransacccion.estacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ipAplicacion : " & objInfoDeTransacccion.ipAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp fechaHora : " & objInfoDeTransacccion.fechaHora)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idTransaccion : " & objInfoDeTransacccion.idTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp nombreCliente : " & objCanjeDescuentoEquipoRequest.nombreCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp tipoDocumento : " & objCanjeDescuentoEquipoRequest.tipoDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp numDocumento : " & objCanjeDescuentoEquipoRequest.numDocumento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp puntoVenta : " & objCanjeDescuentoEquipoRequest.puntoVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp codAplicacion : " & objCanjeDescuentoEquipoRequest.codAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp codAsesor : " & objCanjeDescuentoEquipoRequest.codAsesor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp nomAsesor : " & objCanjeDescuentoEquipoRequest.nomAsesor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idVenta : " & objCanjeDescuentoEquipoRequest.idVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idProceso : " & objCanjeDescuentoEquipoRequest.idProceso)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp puntosVenta : " & objCanjeDescuentoEquipoRequest.puntosVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp solesVenta : " & objCanjeDescuentoEquipoRequest.solesVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idCampana : " & objCanjeDescuentoEquipoRequest.idCampana)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp usuario : " & objCanjeDescuentoEquipoRequest.usuario)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp segmento : " & objCanjeDescuentoEquipoRequest.segmento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp lineaTipificacion : " & objCanjeDescuentoEquipoRequest.lineaTipificacion)

            objCanjeDescuentoEquipoResponse = objCanjeDevolucionServicioComercialService.canjeDescuentoEquipo(objCanjeDescuentoEquipoRequest)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out codigoRespuesta : " & objCanjeDescuentoEquipoResponse.codigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out idTransaccion : " & objCanjeDescuentoEquipoResponse.idTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out mensajeRespuesta : " & objCanjeDescuentoEquipoResponse.mensajeRespuesta)

            If objCanjeDescuentoEquipoResponse.codigoRespuesta = "0" Then
                blnRespuesta = True
            Else
                Throw New Exception("Ocurrió un error al realizar el canje de los Puntos Claro Club.")
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ERROR : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  StackTrace : " & ex.StackTrace.ToString())
            Throw New Exception("Ocurrió un error al realizar el canje de los Puntos Claro Club.")
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin CanjeDescuentoEquipo()")
        End Try

        Return blnRespuesta
    End Function
    ' Fin PS - Automatización de canje y nota de crédito

    ' Inicio IDEA-13006 ClaroClub - Mejoras en notas de créditos
    ''' <summary>
    ''' Actualiza la operaciòn de devolución de puntos Claro Club, cuando se efectuó el pago de la nota de débito.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568.
    ''' Inicio IDEA-13006 ClaroClub - Mejoras en notas de créditos
    ''' </remarks>
    Public Sub ActualizarDevolucionCC(ByVal NroDocSap As String, _
                                      ByVal strIdentifyLog As String)
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Inicio ActualizarDevolucionCC()-----------")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  USUARIO : " & CurrentUser)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  CONS_FLAG_DEVOLUCION : " & ConfigurationSettings.AppSettings("CONS_FLAG_DEVOLUCION"))

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            objCajas.actualizarDevolucionPuntosCC(NroDocSap, _
                                                  CurrentUser, _
                                                  ConfigurationSettings.AppSettings("CONS_FLAG_DEVOLUCION"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualizó correctamente ActualizarDevolucionCC()")
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  Message : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT  StackTrace : " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ActualizarDevolucionCC()-----------")
        End Try
    End Sub
    'Public Sub ActualizarNcDevolCC(ByVal P_NRO_DOCSAP_NC As String, _
    '                                ByVal strIdentifyLog As String)
    '     Dim objCajas As New COM_SIC_Cajas.clsCajas

    '     Try
    '         objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio ActualizarNcDevolCC()---")

    '         objCajas.actualizarNcDevolPuntosCC(P_NRO_DOCSAP_NC, _
    '                                            Session("USUARIO"))
    '         objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Se actualizó el usuario y la fecha y hora de la nota de crédito devolución CC.")
    '     Catch ex As Exception
    '         objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ex.Message : " & ex.Message.ToString())
    '         objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ex.StackTrace : " & ex.StackTrace.ToString())
    '     Finally
    '         objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---Fin ActualizarNcDevolCC()---")
    '     End Try
    ' End Sub
    Public Sub ActualizarNdDevolDscto(ByVal P_NRO_DOCSAP_ND_DSCTO As String, _
                                      ByVal strIdentifyLog As String)
        Dim objCajas As New COM_SIC_Cajas.clsCajas

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ActualizarNdDevolDscto")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp P_NRO_DOCSAP_ND_DSCTO: " & P_NRO_DOCSAP_ND_DSCTO)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "inp usuario: " & CurrentUser)

            objCajas.actualizarNdDevolDscto(P_NRO_DOCSAP_ND_DSCTO, CurrentUser)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ex.Message : " & ex.Message.ToString() & " " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ActualizarNdDevolDscto")
        End Try
    End Sub
    ''' <summary>
    ''' Hace efectiva la devolución de los puntos por el monto de soles equivalente DB PuntosCC.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568.
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos
    ''' </remarks>
    Public Sub CanjeDevolucionEquipo(ByVal NRO_DOCSAP_ND As String, _
                                     ByVal strIdentifyLog As String)
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dtResultado As DataTable
        Dim dtResultadoDevol As DataTable
        Dim drResultado As DataRow
        Dim drResultadoDevol As DataRow
        'Dim NroDocSap As String
        Dim NUM_DOC As String
        Dim linea As String
        Dim puntos_usados As String
        Dim soles_descuento As String
        Dim id_cclub As String
        Dim sResultWS As String
        Dim NRO_DOC_SAP_NC As String

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "--- Inicio CanjeDevolucionEquipo()---")

            drPagos = Session("DocSel")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ListarDevolXNotaDebito")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp NRO_DOCSAP_ND: " & NRO_DOCSAP_ND)
            dtResultadoDevol = objCajas.ListarDevolXNotaDebito(NRO_DOCSAP_ND)
            Dim usuario_reg As String
            If dtResultadoDevol.Rows.Count > 0 Then
                drResultadoDevol = dtResultadoDevol.Rows(0)
                usuario_reg = drResultadoDevol.Item("usuario_reg")
                NRO_DOC_SAP_NC = drResultadoDevol.Item("NRO_DOC_SAP_NC")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out NroDocSap (NC por Canje):" & NRO_DOC_SAP_NC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ListarDevolXNotaDebito")

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ListarXDocSAP")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp NRO_DOC_SAP_NC: " & NRO_DOC_SAP_NC)
            dtResultado = objCajas.ListarXDocSAP(NRO_DOC_SAP_NC)
            If dtResultado.Rows.Count > 0 Then
                drResultado = dtResultado.Rows(0)
                NUM_DOC = drResultado.Item("NUM_DOC")
                linea = drResultado.Item("NRO_LINEA")
                puntos_usados = drResultado.Item("puntos_usados")
                soles_descuento = drResultado.Item("soles_descuento")
                id_cclub = drResultado.Item("id_cclub")
                'NRO_DOC_SAP_NC = drResultado.Item("NRO_DOC_SAP_NC")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out NUM_DOC:" & NUM_DOC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out linea:" & linea)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out puntos_usados:" & puntos_usados)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out id_cclub:" & id_cclub)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "out NRO_DOC_SAP_NC:" & NRO_DOC_SAP_NC)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ListarXDocSAP")

            Dim objCanjeDevolucionServicioComercialService As New CanjeDescuentoEquipoWS.ebsCanjeDevolucionServicioComercialService
            Dim objInfoDeTransacccion As New CanjeDescuentoEquipoWS.InfoDeTransaccionType
            Dim codigoRespuesta As String
            Dim mensajeRespuesta As String
            Dim saldo As String

            objInfoDeTransacccion.usrApp = usuario_reg  ' Usuario de Aplicación que invoca al servicio.
            objInfoDeTransacccion.usrName = Session("NOMBRE_COMPLETO") ' Nombre de usuario de aplicación cliente.
            objInfoDeTransacccion.aplicacion = ConfigurationSettings.AppSettings("CONS_RENOVACION_USUARIO") ' Alias de la aplicación que invoca al servicio.
            objInfoDeTransacccion.estacion = System.Net.Dns.GetHostName ' Nombre de la aplicación que invoca al servicio.
            objInfoDeTransacccion.ipAplicacion = Request.ServerVariables("REMOTE_HOST") ' IP de la aplicación que invoca el servicio..
            Dim idTime As String = Now.ToString("yyyyMMddHHmmss")
            objInfoDeTransacccion.fechaHora = idTime ' Fecha y hora de la transacción.
            objInfoDeTransacccion.idTransaccion = idTime ' ID que debe ser generada por el cliente que invoca al servicio.

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp usrApp : " & objInfoDeTransacccion.usrApp)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp usrName : " & objInfoDeTransacccion.usrName)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp aplicacion : " & objInfoDeTransacccion.aplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp estacion : " & objInfoDeTransacccion.estacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ipAplicacion : " & objInfoDeTransacccion.ipAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp fechaHora : " & objInfoDeTransacccion.fechaHora)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idTransaccion : " & objInfoDeTransacccion.idTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idSolicitud : " & idTime)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp puntoVenta : " & Session("ALMACEN")) ' Punto de Venta.
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp idVenta : " & NRO_DOC_SAP_NC) ' Codigo del Id de Venta.
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp proceso : " & ConfigurationSettings.AppSettings("CONST_VTA_RENOVACION")) ' Tipo de Operacion  : AP(Vta Alta Postpago)/AE(Vta Alta Prepago)/VR(Vta Renovación).
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp linea : " & linea) 'linea
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp tipoDoc : " & id_cclub) 'tipo doc en puntos cc
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp numDoc : " & NUM_DOC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp ptosDevueltos : " & puntos_usados)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inp solesDevueltos : " & soles_descuento)

            sResultWS = objCanjeDevolucionServicioComercialService.devolucionDescuentoEquipo(objInfoDeTransacccion, _
                                                                                             idTime, _
                                                                                             Session("ALMACEN"), _
                                                                                             NRO_DOC_SAP_NC, _
                                                                                             ConfigurationSettings.AppSettings("CONST_VTA_RENOVACION"), _
                                                                                             linea, _
                                                                                             id_cclub, _
                                                                                             NUM_DOC, _
                                                                                             puntos_usados, _
                                                                                             soles_descuento, _
                                                                                             codigoRespuesta, _
                                                                                             mensajeRespuesta, _
                                                                                             saldo)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out codigoRespuesta : " & codigoRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out mensajeRespuesta : " & mensajeRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out saldo : " & saldo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Out sResultWS : " & sResultWS)
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ex.Message : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out  ex.StackTrace : " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---Fin CanjeDevolucionEquipo()---")
        End Try
    End Sub
    Public Function ExisteNcDevolCC(ByVal NroDocSap As String, _
                                    ByVal strIdentifyLog As String) As Boolean
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dt As DataTable
        Dim bExiste As Boolean = False

        strIdentifyLog = NroDocSap
        Try
            dt = objCajas.ListarDevolXNotaCredito(NroDocSap)

            If dt.Rows.Count > 0 Then
                bExiste = True
            Else
                bExiste = False
            End If

            Return bExiste
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Message : " & ex.Message.ToString())
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT StackTrace : " & ex.StackTrace.ToString())

            Return bExiste
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "---------------Fin ExisteNcDevolCC()-----------")
        End Try
    End Function
    Public Function ExisteNdDevolDscto(ByVal NroDocSap As String, _
                                       ByVal strIdentifyLog As String) As Boolean
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim dt As DataTable
        Dim bExiste As Boolean = False

        strIdentifyLog = NroDocSap
        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ListarDevolXNotaDebitoDscto()")
            dt = objCajas.ListarDevolXNotaDebitoDscto(NroDocSap)

            If dt.Rows.Count > 0 Then
                bExiste = True
            Else
                bExiste = False
            End If
        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Message : " & ex.Message.ToString() & " " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "bExiste: " & bExiste)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ListarDevolXNotaDebitoDscto()")
        End Try
        Return bExiste
    End Function
    ''' <summary>
    ''' Verifica si existe una nota de débito pendiente (diferente de '1') en Claro Club por canjear.
    ''' </summary>
    ''' <remarks>
    ''' Autor: E77568.
    ''' IDEA-13006 ClaroClub - Mejoras en notas de créditos.
    ''' </remarks>
    Public Function ExisteDevolucionPuntosCC(ByVal NroDocSap As String, _
                                             ByVal strIdentifyLog As String) As Boolean
        Dim bExiste As Boolean = False

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio ExisteDevolucionPuntosCC()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   INP  NroDocSap : " & NroDocSap)

            Dim objCajas As New COM_SIC_Cajas.clsCajas
            Dim dt As DataTable
            Dim CONS_FLAG_DEVOLUCION As String = ConfigurationSettings.AppSettings("CONS_FLAG_DEVOLUCION")
            dt = objCajas.ListarDevolXNotaDebito(NroDocSap)

            If dt.Rows.Count > 0 Then
                Dim dr As DataRow = dt.Rows(0)
                If dr.Item("FLAG_DEVOL").ToString <> CONS_FLAG_DEVOLUCION Then
                    bExiste = True
                End If
            End If


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT Message : " & ex.Message.ToString() & " " & ex.StackTrace.ToString())
        Finally
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "bExiste: " & bExiste)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin ExisteDevolucionPuntosCC()")
        End Try
        Return bExiste
    End Function
    ' Fin IDEA-13006 ClaroClub - Mejoras en notas de créditos
    'FE Mejora Auditoria
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
    'FE Mejora Auditoria
End Class
