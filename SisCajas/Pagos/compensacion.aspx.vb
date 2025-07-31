Imports SisCajas.Funciones
Public Class compensacion
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDocSap As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecha As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumFact As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSaldo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNroSunat As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelar As System.Web.UI.WebControls.Button

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
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdentifyLog As String


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
            drPagos = Session("DocSel")
            strIdentifyLog = drPagos.Item("VBELN")
            btnGrabar.Attributes.Add("OnClick", "f_Grabar()")
            txtDocSap.Text = drPagos.Item("VBELN")
            txtFecha.Text = drPagos.Item("FKDAT")
            txtNumFact.Text = drPagos.Item("XBLNR")
            txtSaldo.Text = drPagos.Item("SALDO")

        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim dsReturn As DataSet
        Dim objPagos As New SAP_SIC_Pagos.clsPagos

        'I-DMZ
        Dim objCajas As New COM_SIC_Cajas.clsCajas
        Dim objSapCajas As New SAP_SIC_Cajas.clsCajas
        Dim objFE As New COM_SIC_FacturaElectronica.PaperLess
        Dim objclsVentas As New SAP_SIC_Ventas.clsVentas
        'F-DMZ
        Dim i As Integer

        'I-DMZ variable para cliente de boleta o factura 
        Dim clienteB_F As String
        Dim clienteNC As String
        Dim dsMultiUso As DataSet
        Dim dtMultiUso2 As DataTable
        Dim docSapNC As String
        Dim tipoDocNc As String
        Dim ValRptaCorr As String
        Dim arrayCorr() As String
        Dim SerieSunatNc As String
        Dim strNumSunatNc As String
        Dim strNumAsignado As String 'obtiene la referencia
        Dim strDocSunatNC As String  'se guarda la referencia
        Dim msg As String
        Dim tipoDocBF As String
        Dim dtCabecera As DataTable
        Dim dsConsultaFE As DataSet
        Dim mensajePpl As String = String.Empty
        Dim referencia As String
        Dim tipoDoc_Sap As String
        Dim estado As String
        Dim verificaEstadoBF As String
        Dim verificaEstadoNC As String
        Dim origen2 As String
        Dim v As Integer
        Dim saldo As String
        'Salto Correlativo FE
        Dim codOfVenta As String
        Dim corrFicNc As String
        Dim indicadorNC As String
        Dim Flag_paperlessNC As String
        Dim blnErrorPago As Boolean = False
        Dim CorrelativoSunat As String
        Dim Pago_errores As String = ""
        Dim valorPv As String = ""

        'FE-Mejora de validar de montos iguales en compensacion
        Dim saldoNC As String
        'F-DMZ

        'FE-Mejoras 
        'I-DMZ variable para cliente de boleta o factura 

        '****************************** Para Impresion
        Dim strDocSap As String = drPagos.Item("VBELN")
        Dim strDocSunat As String = drPagos.Item("XBLNR")
        Dim strNroDG As String = drPagos.Item("NRO_DEP_GARANTIA")
        Dim strTipDoc As String = drPagos.Item("FKART")
        Dim efectivo As String = "0.00"
        Dim recibido As String = "0.00"
        Dim vuelto As String = "0.00"
        '*******************************
        'Fin-Mejoras 

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
        Dim Detalle(4, 3) As String


        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria

        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPag")
        wParam5 = 1
        wParam6 = "Compensacion de documentos"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPCom")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        'AUDITORIA
        Detalle(1, 1) = "Fecha"
        Detalle(1, 2) = txtFecha.Text
        Detalle(1, 3) = "Fecha"

        Detalle(2, 1) = "Factura"
        Detalle(2, 2) = txtNumFact.Text
        Detalle(2, 3) = "Factura"

        Detalle(3, 1) = "Nota de Credito"
        Detalle(3, 2) = txtNroSunat.Text
        Detalle(3, 3) = "Nota de Credito"

        Detalle(4, 1) = "Usuario"
        Detalle(4, 2) = Session("USUARIO")
        Detalle(4, 3) = "Usuario"

        'FIN AUDITORIA
        'Fin variables de auditoria
'FE 
        If (txtNumFact.Text).IndexOf("E") < 0 Then
            msg = "El comprobante " & txtDocSap.Text & " no tiene No. Factura Sunat."
            Response.Write("<script>alert('" & msg & "')</script>")
            Return
        End If
'FE
        codOfVenta = Session("ALMACEN")

        If Len(Trim(txtNroSunat.Text)) >= 7 Then
            'I-DMz
            dsMultiUso = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
            clienteB_F = dsMultiUso.Tables(0).Rows(0).Item("CLIENTE")

            'dsMultiUso = objCajas.GetCorrelativoNC("", Session("ALMACEN"), txtNroSunat.Text, "")
            corrFicNc = txtNroSunat.Text

            If corrFicNc.IndexOf("E7") > -1 Then

                arrayCorr = corrFicNc.Split("-")
                SerieSunatNc = (arrayCorr(1).ToString).Substring(2, (arrayCorr(1).ToString).Length - 2).ToString() 'serie 
                strNumSunatNc = (CInt(arrayCorr(2).ToString)).ToString()
                referencia = SerieSunatNc & " - " & strNumSunatNc

                indicadorNC = "ACS" 'ASC=AsignadoCorrelativoSunat

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Obtencion de datos de la Nota de Credito (SP_CON_CORRELATIVO_F_NC)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo Documento	           :" & "07")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp SerieSunat	               :" & SerieSunatNc)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta              :" & codOfVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Correlativo_Ficticio_NC     :" & strNumSunatNc)

                dsMultiUso = objCajas.Get_NC("07", SerieSunatNc, strNumSunatNc, codOfVenta)
                'smeza ini 16/07/2015
                ValidaDuplicadoNC(dsMultiUso)
                'smeza fin
                If dsMultiUso.Tables.Count > 0 Then
                    msg = (dsMultiUso.Tables(0).Rows.Count).ToString()

                    If msg = "0" Then
                        msg = "La nota de crédito con número " & corrFicNc & " no se encuentra pagada."
                        Response.Write("<script>alert('" & msg & "')</script>")
                        Return
                    End If

                Else

                    msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                    Response.Write("<script>alert('" & msg & "')</script>")
                    Exit Sub

                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio GetCorrelativoNC")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "codOfVenta: " & codOfVenta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "corrFicNc: " & corrFicNc)

                dsMultiUso = objCajas.GetCorrelativoNC("", codOfVenta, corrFicNc, "")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin GetCorrelativoNC")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "busqueda.Tables.Count: " & dsMultiUso.Tables.Count)

                If dsMultiUso.Tables.Count > 0 Then

                    If dsMultiUso.Tables(0).Rows.Count > 0 Then

                    msg = (dsMultiUso.Tables(0).Rows(0)(1)).ToString

                    If msg <> "07" Then
                        msg = "El número " & corrFicNc & " no pertenece a una nota de Crédito."
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                        Response.Write("<script>alert('" & msg & "')</script>")
                        Exit Sub
                    End If

                Else

                        msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                        Response.Write("<script>alert('" & msg & "')</script>")
                        Exit Sub
                    End If


                Else
                    msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                    Response.Write("<script>alert('" & msg & "')</script>")
                    Exit Sub
                End If

                If dsMultiUso.Tables.Count > 0 Then
                    msg = (dsMultiUso.Tables(0).Rows.Count).ToString()

                    If msg = "0" Then
                        msg = "La nota de crédito con número " & corrFicNc & " no se encuentra pagada."
                        Response.Write("<script>alert('" & msg & "')</script>")
                        Return
                    End If
                Else
                    msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                    Response.Write("<script>alert('" & msg & "')</script>")
                    Return
                End If

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la obtencion de datos de la Nota de Credito (SP_CON_CORRELATIVO_F_NC)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

            docSapNC = (dsMultiUso.Tables(0).Rows(0)(0)).ToString
            tipoDocNc = (dsMultiUso.Tables(0).Rows(0)(1)).ToString

            'Mejora Salto Correlativo
            verificaEstadoNC = (dsMultiUso.Tables(0).Rows(0)(6)).ToString
            Flag_paperlessNC = IIf(IsDBNull(dsMultiUso.Tables(0).Rows(0)("FLAG_PAPERLESS")), "", dsMultiUso.Tables(0).Rows(0)("FLAG_PAPERLESS").ToString())
            'Mejora Salto Correlativo


            dsMultiUso = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), docSapNC, "")
            clienteNC = dsMultiUso.Tables(0).Rows(0).Item("CLIENTE")

            '--------------------------------------------------------------------------------------smeza ini - E7--
            If dsMultiUso.Tables.Count > 0 Then
                CorrelativoSunat = dsMultiUso.Tables(0).Rows(0)("NRO_REFERENCIA").ToString
            Else
                msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                Response.Write("<script>alert('" & msg & "')</script>")
                Return
            End If


            If Not (CorrelativoSunat = "0000000000000000" Or CorrelativoSunat = "") Then

                arrayCorr = (CorrelativoSunat).Split("-")

                SerieSunatNc = (arrayCorr(1).ToString).Substring(1, (arrayCorr(1).ToString).Length - 1)  'serie
                strNumSunatNc = (CInt(arrayCorr(2).ToString)).ToString
                referencia = SerieSunatNc & " - " & strNumSunatNc
                indicadorNC = "ACS"

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo Sunat      :" & dsMultiUso.Tables(0).Rows(0)("NRO_REFERENCIA").ToString())
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
            End If
            '--------------------------------------------------------------------------------------smeza fin - E7--

            saldoNC = dsMultiUso.Tables(0).Rows(0).Item("TOTAL_DOCUMENTO")

            If CDbl(saldoNC) > CDbl(drPagos.Item("SALDO")) Then

                msg = "El total del documento no puede ser menor al total de la Nota de Crédito"
                Response.Write("<script>alert('" & msg & "')</script>")
                Exit Sub

            End If

            If clienteNC <> clienteB_F Then
                'Throw New Exception("La nota de crédito le pertenece a otro Cliente")
                msg = "La nota de crédito le pertenece a otro Cliente"
                Response.Write("<script>alert('" & msg & "')</script>")
                Return
            End If

            Dim claseDoc As String



            dsMultiUso = objclsVentas.Get_ConsultaClasePedido(Session("ALMACEN"))

            dtMultiUso2 = dsMultiUso.Tables(0)

            If dtMultiUso2.Rows.Count > 0 Then
                For v = 0 To dtMultiUso2.Rows.Count - 1
                    If dtMultiUso2.Rows(v)(2) = drPagos.Item("FKART") Then
                        claseDoc = dtMultiUso2.Rows(v).Item("KSCHL")
                    End If
                Next
            End If

            If claseDoc = "ZBOL" Then
                tipoDocBF = "03"
                tipoDoc_Sap = "E3"

            ElseIf claseDoc = "ZFAC" Then
                tipoDocBF = "01"
                tipoDoc_Sap = "E1"

            ElseIf claseDoc = "ZNCV" Then
                tipoDocBF = "07"
                tipoDoc_Sap = "E7"

            ElseIf claseDoc = "ZNDV" Then
                tipoDocBF = "08"
                tipoDoc_Sap = "E8"

            Else

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "claseDoc: " & claseDoc)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc: " & tipoDocBF)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc_Sap: " & tipoDoc_Sap)

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo de documento: " & drPagos.Item("FKART"))

                Response.Write("<script>alert('" & ConfigurationSettings.AppSettings("FE_Doc_No_Configurado_PDV") & " Doc. SAP : " & txtDocSap.Text & "')</script>")
                Return

            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "claseDoc: " & claseDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc: " & tipoDocBF)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "tipoDoc_Sap: " & tipoDoc_Sap)

            If tipoDocBF = "01" Then
                origen2 = "F"
            End If

            If tipoDocBF = "03" Then
                origen2 = "B"
            End If


            If (clienteB_F = clienteNC) Then

                'Inicio del Pago  de la Nota de crédito en SAP 
                'Inicio logs
                If indicadorNC <> "ACS" Then
                    'En esta sección obtengo el correlativo

                    dsMultiUso = objCajas.GetCorrelativoNC(docSapNC, codOfVenta, "", "")

                    If dsMultiUso.Tables.Count > 0 Then  'asumiendo que esta validacion es para ver si se cae y evitar eso
                        'BUSQUEDa si el documento no ha sido enviado a paperless
                        If dsMultiUso.Tables(0).Rows.Count > 0 AndAlso Not dsMultiUso.Tables(0).Rows(0)("CORRELATIVO") Is Nothing Then
                            SerieSunatNc = dsMultiUso.Tables(0).Rows(0)("SERIE").ToString() 'serie
                            strNumSunatNc = dsMultiUso.Tables(0).Rows(0)("CORRELATIVO").ToString() 'CORRELATIVO
                            If SerieSunatNc = "" Or strNumSunatNc = "" Then
                                valorPv = "SSC" 'variable para valor por primera vez
                            End If
                        End If
                        If valorPv = "SSC" Then
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Obtencion del correlativo de la Nota de Credito (SSAPSS_CORRELATIVOFE)")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta              :" & codOfVenta)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo de Documento           :" & tipoDocNc)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Documento de Referencia     :" & tipoDocBF)

                            dsMultiUso = objCajas.FP_CalcularCorrelativo(codOfVenta, tipoDocNc, tipoDocBF)

                            If dsMultiUso.Tables.Count > 0 Then

                                If dsMultiUso.Tables(0).Rows.Count = 0 Then
                                    msg = "No Se ha formado el correlativo sunat para la Nota de Crédito."
                                    Response.Write("<script>alert('" & msg & "')</script>")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha generado el correlativo de la Nota de Crédito.")
                                    Return
                                Else
                                    ValRptaCorr = dsMultiUso.Tables(0).Rows(0).Item(0)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo           :" & ValRptaCorr)
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la obtención del correlativo de la Boleta o Factura.")
                                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                                End If
                            Else

                                msg = "No se ha podido consultar la información para generar el Correlativo de la Nota de Crédito, volver a Intentar."
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg & "- " & "No se obtuvo ninguna tabla en la consulta al Modulo.")
                                Response.Write("<script>alert('" & msg & "')</script>")
                                Return

                            End If

                            arrayCorr = ValRptaCorr.Split("-")
                            SerieSunatNc = (arrayCorr(0).ToString).Substring(1, (arrayCorr(0).ToString).Length - 1) 'serie
                            strNumSunatNc = (CInt(arrayCorr(1).ToString)).ToString  'Correlativo 

                            Dim corrCompleto As String = tipoDocBF & "|" & ValRptaCorr
                            Dim updCorrCompleto As String = objCajas.SP_UPD_FLAG_PAPER(docSapNC, "V", corrCompleto)

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualización del Correlativo Ficticio (SP_UPD_CORRELATIVO_F_NC)")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap del Nota de Crédito            :" & docSapNC)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de Serie de la Nota de Crédito           :" & SerieSunatNc)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Correlativo de la Nota de Crédito               :" & strNumSunatNc)

                            Dim updCorrFNc As String = objCajas.Actualizar_CorrFic_Nc(docSapNC, SerieSunatNc, strNumSunatNc)
                            If updCorrFNc = "" Then
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado              :" & "No se ha actualizado.")

                            Else
                                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Resultado           :" & "Actualización del Correlativo Ficticio Exitoso.")
                            End If
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la actualización del Correlativo Ficticio.")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                            'Mejora FE Salto Correlativo

                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo           :" & ValRptaCorr)
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la obtención del correlativo de la Nota de Credito.")
                            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                        End If

                    Else

                        msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                        Response.Write("<script>alert('" & msg & "')</script>")
                        Return

                    End If

                    referencia = SerieSunatNc & " - " & strNumSunatNc
                End If
            End If


            'Inicio logs
            tipoDocNc = "E7"

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Pago  de la Nota de crédito en SAP (Set_NroSunatCajero_FE)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Doc. SAP       :" & docSapNC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Num_SUNAT      :" & strNumSunatNc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Serie_SUNAT    :" & SerieSunatNc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Tipo_NC        : " & tipoDocNc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta :" & Session("ALMACEN"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Fecha_Pago     :" & drPagos.Item("FKDAT"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen         :" & origen2)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Usuario        :" & Session("USUARIO"))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Referencia     :" & strNumAsignado)

            'dsMultiUso = objSapCajas.Set_NroSunatCajero_FE(docSapNC, strNumSunatNc, SerieSunatNc, "E7", "", Session("ALMACEN"), "", drPagos.Item("FKDAT"), "", "", "", origen2, Session("USUARIO"), "", "", strNumAsignado)

            Try
                dsMultiUso = objSapCajas.Set_NroSunatCajero_FE(docSapNC, strNumSunatNc, SerieSunatNc, tipoDocNc, "", Session("ALMACEN"), "", drPagos.Item("FKDAT"), "", "", "", origen2, Session("USUARIO"), "", "", strNumAsignado)

                For i = 0 To dsMultiUso.Tables(0).Rows.Count - 1
                    If dsMultiUso.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " - " & " ERROR: " & dsMultiUso.Tables(0).Rows(i).Item("MESSAGE"))
                        blnErrorPago = True
                    End If
                Next

            Catch ex As Exception
                objFileLog.Log_WriteLog(pathFile, strArchivo, "ERROR : " & ex.Message.ToString())
                blnErrorPago = True
            End Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out strNumAsignadoNC:" & strNumAsignado)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Set_NroSunatCajero_FE (Zpsd_Rfc_Trs_Caj_Set_Nro_Sunat)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

            'FE Mejora salto correlativo verificar si fallo SAP y si genero correlativo sunat

            If blnErrorPago Or strNumAsignado = "" Then

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Oficina_Venta      :" & Session("ALMACEN"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Nro_Documento	  :" & docSapNC)


                dsMultiUso = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), docSapNC, "")

                If dsMultiUso.Tables.Count > 0 Then

                    CorrelativoSunat = dsMultiUso.Tables(0).Rows(0)("NRO_REFERENCIA").ToString
                Else
                    msg = "No se ha podido consultar la información para realizar el Pago, volver a Intentar."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                    Response.Write("<script>alert('" & msg & "')</script>")
                    Return
                End If


                If (CorrelativoSunat = "0000000000000000" Or CorrelativoSunat = "") Then


                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "No se ha generado el correlativo de la Nota de Crédito.")
                    msg = "No se ha realizado el pago correctamente volver a Pagar."
                    Response.Write("<script>alert('" & msg & "')</script>")
                    Exit Sub

                Else

                    arrayCorr = (CorrelativoSunat).Split("-")

                    SerieSunatNc = (arrayCorr(1).ToString).Substring(1, (arrayCorr(1).ToString).Length - 1)  'serie
                    strNumSunatNc = (CInt(arrayCorr(2).ToString)).ToString
                    referencia = SerieSunatNc & " - " & strNumSunatNc
                    blnErrorPago = False

                    Pago_errores = "NVS"

                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Out Correlativo Sunat      :" & dsMultiUso.Tables(0).Rows(0)("NRO_REFERENCIA").ToString())
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta del RFC (ZPVU_RFC_CON_PEDIDO)para verificar si hubo Asignación")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")
                End If

            End If

            'FE Mejora salto correlativo
            If Pago_errores = "NVS" Then
                strDocSunatNC = CorrelativoSunat
            Else
                strDocSunatNC = strNumAsignado
            End If

            Pago_errores = ""

            'Fin del pago de la nota de crédito en Sap
            'FIN logs
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Se Realizó el Pago  de la Nota de crédito en SAP")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "************************************************")


            'F-DMZ
            'Inicio de la compensación

            'Inicio logs
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de Compensación (Get_RegistroCompensacion)")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Fecha              :" & txtFecha.Text)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Documento SAP de la B/F     :" & txtNumFact.Text)
            'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Serie_SUNAT de la Nota de Credito    :" & strDocSunatNC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "     Inp Doc_SAP de la Nota de Credito :" & docSapNC)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Usuario     :" & Session("USUARIO"))

            'dsReturn = objPagos.Get_RegistroCompensacion(txtFecha.Text, txtNumFact.Text, strDocSunatNC, Session("USUARIO"))
            dsReturn = objPagos.Get_RegistroCompensacion(txtFecha.Text, txtNumFact.Text, docSapNC, Session("USUARIO"))


            If Not IsNothing(dsReturn) Then

                For i = 0 To dsReturn.Tables(0).Rows.Count - 1
                    If dsReturn.Tables(0).Rows(i).Item(0) = "E" Then
                        wParam5 = 0
                        wParam6 = "Error la Compensacion de documentos. " & dsReturn.Tables(0).Rows(i).Item(1)
                        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                        Response.Write("<script language = javascript>alert('" & dsReturn.Tables(0).Rows(i).Item(1) & "')</script>")
                        Exit Sub
                    End If
                Next

            Else

                wParam5 = 0
                wParam6 = "Error al Compensacion de documentos. Estructura devuelta es nula o vacia"
                objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
                Response.Write("<script language = javascript>alert('Ha ocurrido un error. Por favor reintentar')</script>")
                Exit Sub

            End If

            'FIN logs
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Se Realizó la compensación")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "************************************************")

            'Final de la compensación
            'I-DMZ 
            'Grabado de datos en Sicar
            Dim updGrupoNc As String = objCajas.Actualizar_Grupo(docSapNC, txtDocSap.Text)

            'Dim updCorrFNc As String = objCajas.Actualizar_CorrFic_Nc(docSapNC, SerieSunatNc, strNumSunatNc)

            'Envio a paperless de la Boleta o FActura

            'Verificar si ya no presenta Saldo
            dtMultiUso2 = objPagos.Get_ConsultaPoolFactura(Session("ALMACEN"), drPagos.Item("FKDAT"), "I", "", "", "", "20", "1").Tables(0)


            Dim K As Integer
            'Dim saldo As String
            Dim SociedadSap = drPagos.Item("BUKRS")


            saldo = "PT" 'pagado totalmente

            If dtMultiUso2.Rows.Count > 0 Then
                For K = 0 To dtMultiUso2.Rows.Count - 1
                    If dtMultiUso2.Rows(K)(2) = txtDocSap.Text Then
                        saldo = dtMultiUso2.Rows(K).Item("SALDO")
                    End If
                Next
            End If


            'Cambio FE para Busqueda  por DOA
            Dim ds_trs_comprobante As DataSet
            Dim NRORIGEN As String = ""

            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio de la Consulta del RFC (Zpvu_Rfc_Trs_Comprobante) .")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap del Nota de Crédito            :" & docSapNC)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Punto de Venta        :" & Session("ALMACEN"))

            ds_trs_comprobante = objPagos.Get_ConsultaComprobante(docSapNC, Session("ALMACEN"))
            If ds_trs_comprobante.Tables.Count > 0 Then
                If ds_trs_comprobante.Tables(0).Rows.Count > 0 Then
                    NRORIGEN = ds_trs_comprobante.Tables(0).Rows(0)("NREFORIG").ToString()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Out Doc. Referencia Origen :" & NRORIGEN)
                Else
                    msg = "No Se ha encontrado registros en la tabla cabecera de la consulta en SAP del rfc (Zpvu_Rfc_Trs_Comprobante)."
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & msg)
                End If
            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "El RFC(Zpvu_Rfc_Trs_Comprobante) no ha devuelto información.")
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la Consulta del RFC (Zpvu_Rfc_Trs_Comprobante) .")
            'Fin del cambio FE para Busqueda  por DOA


          If NRORIGEN = "" Then
            'Actualizacion de la referencia del documento a compensar con la nota de crédito

            dsMultiUso = objSapCajas.Relacionar_NC_con_B_o_F(docSapNC, txtNumFact.Text) 'relacionar la factura con la NC

            Try
                For i = 0 To dsMultiUso.Tables(0).Rows.Count - 1
                    If dsMultiUso.Tables(0).Rows(i).Item("TYPE") = "E" Then
                        wParam6 = "Uso del Rfc (ZFE_UPDATE_REFORIGEN)" & " - " & " ERROR: " & dsMultiUso.Tables(0).Rows(i).Item("MESSAGE")
                    End If
                Next
            Catch ex As Exception

                wParam6 = "Fin ZFE_UPDATE_REFORIGEN (ZFE_UPDATE_REFORIGEN)" & " - " & "ERROR : " & ex.Message.ToString()

            End Try
          End If


            'Obtencion de los datos para el envio de la nota de credito a paperless
            If verificaEstadoNC = "1" And Flag_paperlessNC = "" Then

                referencia = strDocSunatNC
                arrayCorr = referencia.Split("-")
                SerieSunatNc = (arrayCorr(1).ToString).Substring(1, (arrayCorr(1).ToString).Length - 1) 'serie
                strNumSunatNc = (CInt(arrayCorr(2).ToString)).ToString
                referencia = SerieSunatNc & "-" & strNumSunatNc

                mensajePpl = _
                objFE.GenerarFacturaElectronica( _
                    docSapNC, _
                    Session("ALMACEN"), _
                    referencia, _
                    SociedadSap, "", "") 'MMR - NUEVOS PARAMETROS

                RegistrarAuditoria("Envio factura electrónica NumSunat = " & referencia, CheckStr(ConfigurationSettings.AppSettings("codTrsPaperless")))


                estado = mensajePpl

                If estado = "F" Then
                    estado = "E"
                End If

                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Actualización del Estado de la Nota de Credito (SP_UPD_ESTADO_DOCUM)")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Numero de DocSap   :" & docSapNC)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Origen             :" & "1")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "    Inp Estado             :" & estado)

                Dim updateEstado As String = objCajas.Actualizar_Estado_Pago(docSapNC, "1", estado)

                If updateEstado = "" Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Out updateEstado  :" & updateEstado)
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin de la ctualización del Estado de la Boleta o Factura.")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "*****************************************************************")

                'Inicio DMZ-Agregado para la impresion de la Nota de Credito
                Session("NC_Impresion") = docSapNC
                'Fin DMZ-Agregado para la impresion de la Nota de Credito
            End If

        Else
            wParam5 = 0
            wParam6 = "La nota de Crédito le pertenece a otro Cliente"
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
            Response.Write("<script language = javascript>alert('La nota de Crédito le pertenece a otro Cliente')</script>")
            Exit Sub

        End If

        Dim sUrl As String = "Terminar.aspx"

        Dim dsPedido As DataSet = objPagos.Get_ConsultaPedido("", Session("ALMACEN"), drPagos.Item("VBELN"), "")
        Dim flagVentaEquipoPrepago As String = "0"

        dsMultiUso = objPagos.Get_ParamGlobal(Session("ALMACEN"))
        If saldo = "PT" Then
            If Trim(dsMultiUso.Tables(0).Rows(0).Item("IMPRESION_SAP")) = "" Then
                sUrl &= "?pImp=S" & _
                                "&pDocSap=" & strDocSap & _
                                "&pDocSunat=" & strDocSunat & _
                                "&pNroDG=" & strNroDG & _
                                        "&pTipDoc=" & strTipDoc & _
                                        "&strEfectivo=" & efectivo & _
                                        "&strRecibido=" & recibido & _
                                        "&strRecibidoUS=" & Funciones.CheckInt(String.Empty) & _
                                        "&strEntregar=" & vuelto & _
                                        "&flagVentaEquipoPrepago=" & flagVentaEquipoPrepago

            End If

        Else
            sUrl &= "?pImp=S" & _
                    "&pDocSap=" & docSapNC & _
                    "&pDocSunat=" & strDocSunatNC & _
                    "&pNroDG=" & "" & _
                    "&pTipDoc=" & "" & _
                    "&strEfectivo=" & CStr(0) & _
                    "&strRecibido=" & "0" & _
                    "&strRecibidoUS=" & CStr(0) & _
                    "&strEntregar=" & "0" & _
                    "&flagVentaEquipoPrepago=" & ""

            Session.Remove("NC_Impresion")

        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "URL IMPRESION : " & sUrl)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Grabar pagos.")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------------------------------------")
        Response.Redirect(sUrl)

        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        Session("FechaPago") = drPagos.Item("FKDAT")
        Response.Write("<script>alert('El Pago se Realizo con éxito');</script>")
        Response.Redirect("PoolPagos.aspx")

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session("FechaPago") = drPagos.Item("FKDAT")
        Response.Redirect("PoolPagos.aspx")
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

    'smeza ini 16/07/2015
    Private Sub ValidaDuplicadoNC(ByRef datos As DataSet)
        Dim v As Integer
        Dim tipodoc As String = ""
        Dim tipodocAux As String = ""

        If Not datos Is Nothing Then

            If datos.Tables(0).Rows.Count > 1 Then

                drPagos = Session("DocSel")
                tipodoc = Mid(drPagos.Item("XBLNR"), 1, 2)

                If tipodoc = "E1" Then
                    tipodoc = "01"
                ElseIf tipodoc = "E3" Then
                    tipodoc = "03"
                End If


                For v = 0 To datos.Tables(0).Rows.Count - 1
                    tipodocAux = Convert.ToString(datos.Tables(0).Rows(v)(12))
                    tipodocAux = Mid(tipodocAux, 1, 2)

                    If tipodocAux <> "" And tipodocAux <> "00" Then
                        If Not (tipodocAux = tipodoc) Then
                            datos.Tables(0).Rows(v).Delete()
                        End If
                    End If
                    tipodocAux = ""
                Next

                datos.Tables(0).AcceptChanges()

            End If

        End If

    End Sub
    'smeza fin


End Class
