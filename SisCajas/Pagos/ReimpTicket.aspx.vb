Public Class ReimpTicket
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNumRef As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnImprimir As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Public dsDeuda As DataSet 'PROY-26366-IDEA-34247 FASE 1
    
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
            'Dim dsOficina As DataSet
            'Dim objPagos As New SAP_SIC_Pagos.clsPagos
            btnImprimir.Attributes.Add("onClick", "f_Valida()")
            'If Not Page.IsPostBack Then
            '    dsOficina = objPagos.Get_ConsultaOficinaVenta("", "MT")

            '    cboOficina.DataValueField = "VKBUR"
            '    cboOficina.DataTextField = "BEZEI"
            '    cboOficina.DataSource = dsOficina.Tables(0)
            '    cboOficina.DataBind()
        'End If
        End If
    End Sub
    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

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
        Dim Detalle(2, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        Dim BoolOK As Boolean
        BoolOK = False
        ImprimirComprobante(BoolOK)

        If (BoolOK) Then
        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcRTic")
        wParam5 = 1
        wParam6 = "Reimpresion de tickets"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtRTic")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "NumRef"
        Detalle(1, 2) = txtNumRef.Text
        Detalle(1, 3) = "Numero de referencia SUNAT"

        Detalle(2, 1) = "DocSap"
            'Detalle(2, 2) = strDocSap
        Detalle(2, 3) = "Documento SAP"

        objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)

        'FIN AUDITORIA
        Else

            Response.Write("<script> alert('No se tiene tickets asociados a este número de referencia'); </script>")
        End If
    End Sub

    Private Sub ImprimirComprobante(ByRef BoolOK As Boolean)

        Dim objFileLog As New SICAR_Log
        Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
        Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
        Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
        Dim strIdentifyLog As String = "Imprimir Comprobanate"

        Dim strDocSap As String
        Dim dsDocumento As DataSet
        Dim objConsulta As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim strScript As String
        Dim Nro As String
        Dim NroSplit() As String
        Dim NroDoc As String
        Dim TipoDoc As String
        Dim TipoDocRecaudacion As String
        Dim dsConsulta As DataSet
        Dim strDealer As String
        Dim dblMonto As Double
        Dim strTrama As String
        Dim strPDV As String
        Dim strCajero As String
        Dim strFechaRegistro As String

        Dim i As Integer

        BoolOK = False

        Nro = txtNumRef.Text
        NroSplit = Nro.Split("-")
        TipoDoc = NroSplit(0)

        If NroSplit.Length.ToString.Equals("3") Then
            NroDoc = NroSplit(1) + "-" + NroSplit(2)
        Else
            If NroSplit.Length.ToString.Equals("2") Then
                NroDoc = NroSplit(1)
            Else
                NroDoc = ""
        End If

        End If

        If NroDoc.Equals("") Then
            NroDoc = Nro
            TipoDoc = "NC"
        End If

        Try
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo ConsultaComprobante()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Documento : " & NroDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Tipo Documento : " & TipoDoc)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Documento Sap: " & strDocSap)
            dsDocumento = objConsulta.ConsultaComprobante(NroDoc, TipoDoc, strDocSap, "", "")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo ConsultaComprobante()")
            If strDocSap.Equals("") Then
                NroDoc = Nro
                TipoDoc = "RE"
                dsDocumento = objConsulta.ConsultaComprobante(NroDoc, TipoDoc, strDocSap, "", "")
        End If
        Catch ex As Exception
            strDocSap = ""
        End Try

        If (strDocSap <> "") Then

            BoolOK = True

            If TipoDoc.Equals("RE") Then
                strScript = "<iframe id='IfrmImpresion' name='IfrmImpresion' style='VISIBILITY: hidden; WIDTH: 100px; HEIGHT: 100px' ></iframe>" & Chr(13) & Chr(13)
        strScript &= "<script language=javascript>" & Chr(13)
                strScript &= "function f_Imprimir(){" & Chr(13)
                strScript &= "var objIframe = document.getElementById(" & Chr(34) & "IfrmImpresion" & Chr(34) & ");" & Chr(13)
                strScript &= "	objIframe.style.visibility = " & Chr(34) & "visible" & Chr(34) & ";" & Chr(13)
                strScript &= "	objIframe.style.width = 0;" & Chr(13)
                strScript &= "	objIframe.style.height = 0;" & Chr(13)
                strScript &= "	 objIframe.src = 'OperacionesImp_DG.aspx?Reimpresion=1&numDepGar=" & strDocSap & "'"
                strScript &= "}" & Chr(13) & Chr(13)
                strScript &= "function Imprimir(){" & Chr(13)
                strScript &= "var objIframe = document.getElementById(" & Chr(34) & "IfrmImpresion" & Chr(34) & ");" & Chr(13)
                strScript &= "if(IfrmImpresion.window.document.all[" & Chr(34) & "printbtn" & Chr(34) & "])IfrmImpresion.window.document.all[" & Chr(34) & "printbtn" & Chr(34) & "].style.visibility = " & Chr(34) & "HIDDEN" & Chr(34) & ";" & Chr(13)
                strScript &= "window.frames[" & Chr(34) & "IfrmImpresion" & Chr(34) & "].focus();" & Chr(13)
                strScript &= "window.frames[" & Chr(34) & "IfrmImpresion" & Chr(34) & "].print();" & Chr(13)
                strScript &= "	objIframe.style.visibility = " & Chr(34) & "hidden" & Chr(34) & ";" & Chr(13)
                strScript &= "	objIframe.style.width = 0;" & Chr(13)
                strScript &= "	objIframe.style.height = 0;" & Chr(13)
                strScript &= "  objIframe.contentWindow.location.replace(" & Chr(39) & "#" & Chr(39) & ");" & Chr(13)
                strScript &= "}" & Chr(13) & Chr(13)
                strScript &= "f_Imprimir();" & Chr(13)
                strScript &= "</script>" & Chr(13)

                RegisterClientScriptBlock("Imprime", strScript)
            Else
                strScript = "<iframe id='IfrmImpresion' name='IfrmImpresion' style='VISIBILITY: hidden; WIDTH: 100px; HEIGHT: 100px'		src='#'></iframe>" & Chr(13) & Chr(13)
                strScript &= "<script language=javascript>" & Chr(13)
        strScript &= "function f_Imprimir(){" & Chr(13)
        strScript &= "var objIframe = document.getElementById(" & Chr(34) & "IfrmImpresion" & Chr(34) & ");" & Chr(13)
        strScript &= "	objIframe.style.visibility = " & Chr(34) & "visible" & Chr(34) & ";" & Chr(13)
        strScript &= "	objIframe.style.width = 0;" & Chr(13)
        strScript &= "	objIframe.style.height = 0;" & Chr(13)
                strScript &= "	 objIframe.src = 'OperacionesImp.aspx?codRefer=" & strDocSap & "' + '&FactSunat=' + '" & Session("ALMACEN") & "' + '&Reimpresion=1&TipoDoc=' + '" & TipoDoc & "'"
                strScript &= "}" & Chr(13) & Chr(13)

                strScript &= "function Imprimir(){" & Chr(13)
                strScript &= "var objIframe = document.getElementById(" & Chr(34) & "IfrmImpresion" & Chr(34) & ");" & Chr(13)
                strScript &= "window.open(objIframe.contentWindow.location)" & Chr(13)
        strScript &= "}" & Chr(13) & Chr(13)

        strScript &= "f_Imprimir();" & Chr(13)
        strScript &= "</script>" & Chr(13)

        RegisterClientScriptBlock("Imprime", strScript)
            End If
        Else

            Dim objOffline As New COM_SIC_OffLine.clsOffline
            'PROY-26366 LOG - INI
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo GetRegistroDeuda()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Transacción : " & Nro)
            dsDeuda = objOffline.GetRegistroDeuda(Nro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT dsDeuda: " & dsDeuda.Tables(1).Rows.Count)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo GetRegistroDeuda()")
            'PROY-26366 LOG - FIN

            Dim drFila As DataRow
            For Each drFila In dsDeuda.Tables(3).Rows
                If drFila("TYPE") = "E" Then
                    BoolOK = False
                Else
                    If dsDeuda.Tables(1).Rows.Count > 0 Then
                        BoolOK = True
                        TipoDocRecaudacion = Convert.ToString(dsDeuda.Tables(1).Rows(0)("TIPO_DOC_RECAUD"))
                    Else
                        BoolOK = False
                    End If
                End If
            Next

            If (BoolOK) Then
                strScript = "<script language=javascript>" & Chr(13)
                strScript &= "function f_imprimir_recaudacion(){" & Chr(13)
                strScript &= "var numeroDocumento = " & Chr(34) & Nro & Chr(34) & ";" & Chr(13)
                strScript &= "var tipoDocumento = " & Chr(34) & TipoDocRecaudacion & Chr(34) & ";" & Chr(13)
                strScript &= "var urlRedirect = '../Recaudacion/docRecaudacion.aspx?p_tipodoc=" & TipoDocRecaudacion & "' + ' &Reimpresion=1&p_nrosap=' + '" & Nro & "'" & ";" & Chr(13)
                strScript &= "window.open(urlRedirect," & Chr(34) & "docRecaudacion" & Chr(34) & "," & Chr(34) & "menubar=false,width=325,height=420" & Chr(34) & ");" & Chr(13)
                strScript &= "}" & Chr(13) & Chr(13)
                strScript &= "f_imprimir_recaudacion();" & Chr(13)
                strScript &= "</script>" & Chr(13)

                RegisterClientScriptBlock("Imprime_recaudacion", strScript)
            Else
                Try
                    Dim IntVacio As Int64

                    'PROY-26366 LOG - INI
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Metodo GetRecaudacionDACDetalle()")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Nro Transacción : " & Nro)
                    dsConsulta = objOffline.GetRecaudacionDACDetalle(Nro, Nothing)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "COD_CLIENTE : " & dsConsulta.Tables(0).Rows(0).Item("COD_CLIENTE"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "CLIENTE : " & dsConsulta.Tables(0).Rows(0).Item("CLIENTE"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Metodo GetRecaudacionDACDetalle()")
                    'PROY-26366 LOG - FIN
                    strDealer = dsConsulta.Tables(0).Rows(0).Item("COD_CLIENTE") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("CLIENTE"), "&", " ")
                    BoolOK = True
                    strTrama = ""
                    strPDV = dsConsulta.Tables(0).Rows(0).Item("VKBUR") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("BEZEI"), "&", " ")
                    strCajero = dsConsulta.Tables(0).Rows(0).Item("USUARIO") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("NOMBRE"), "&", " ")
                    strFechaRegistro = dsConsulta.Tables(0).Rows(0).Item("FECHA") & " - " & Replace(dsConsulta.Tables(0).Rows(0).Item("HORA"), "&", " ")


                    For i = 0 To dsConsulta.Tables(0).Rows.Count - 1
                        strTrama &= dsConsulta.Tables(0).Rows(i).Item("VTEXT") & ";|"
                        dblMonto += dsConsulta.Tables(0).Rows(i).Item("MONTO")
                    Next

                    If Len(strTrama) > 0 Then
                        strTrama = Left(strTrama, Len(strTrama) - 1)

                    End If


                    'PROY-26366 REIMPRESION - INI

                    strScript = "<iframe id='ifrmRecDac' name='IfrmImpresion' style='VISIBILITY: hidden; WIDTH: 100px; HEIGHT: 100px'		src='#'></iframe>" & Chr(13) & Chr(13)
                    strScript &= "<script language=javascript>" & Chr(13)
                    strScript &= "function f_Imprimir(){" & Chr(13)
                    strScript &= "var objIframe = document.getElementById(" & Chr(34) & "IfrmImpresion" & Chr(34) & ");" & Chr(13)
                    strScript &= "	objIframe.style.visibility = " & Chr(34) & "visible" & Chr(34) & ";" & Chr(13)
                    strScript &= "	objIframe.style.width = 0;" & Chr(13)
                    strScript &= "	objIframe.style.height = 0;" & Chr(13)
                    strScript &= "	 objIframe.src = '../Recaudacion/docRecaudacionDAC.aspx?strTrama=" & strTrama & "' + '&MontoTotalPagado=' + '" & dblMonto & "' + '&PDV=' + '" & strPDV & "' + '&Cajero=' + '" & strCajero & "' + '&FechaRegistro=' + '" & strFechaRegistro & "' + '&Reimpresion=1&Dealer=' + '" & strDealer & "'"
                    strScript &= "}" & Chr(13) & Chr(13)

                    strScript &= "function Imprimir(){" & Chr(13)
                    strScript &= "var objIframe = document.getElementById(" & Chr(34) & "ifrmRecDac" & Chr(34) & ");" & Chr(13)
                    strScript &= "window.open(objIframe.contentWindow.location)" & Chr(13)
                    strScript &= "}" & Chr(13) & Chr(13)

                    strScript &= "f_Imprimir();" & Chr(13)
                    strScript &= "</script>" & Chr(13)

                    'PROY-26366 REIMPRESION - FIN
                    BoolOK = True

                    RegisterClientScriptBlock("Imprime", strScript)
                Catch ex As Exception
                    BoolOK = False

                End Try
            End If
        End If
    End Sub
    'PROY-26366-IDEA-34247 FASE 1 - FIN
End Class
