Imports SisCajas.GenFunctions

Public Class toExcel
    Inherits System.Web.UI.Page

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
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else
            Dim objComponente, objRecordSet
            Dim iTotalCampos, iIndexCampo, sValor, StrXml
            Dim strTipo, sTipoTienda, strFecha, sOficVenta
            Dim strIndividual, strUsuario
            strFecha = Request.QueryString("strFecha")

            Dim ObjAux As New GenFunctions(Server, Request, Response)
            Session("FechaAct") = strFecha
            'Session("VarVal3") = "0006"
            'Session("VarVal2") = "MT"
            'Session("STRMessage") = ""
            'Session("ALMACEN") = "0006"
            'Session("MUNDOTIM") = "CAC BEGONIAS"

            sTipoTienda = Trim(Session("CANAL"))
            sOficVenta = Session("ALMACEN")
            objComponente = Session("InfoCuadreCaja")

            strTipo = Request.QueryString("tipo")
            strIndividual = Trim(Request.QueryString("Individual"))

            If strIndividual = "1" Then
                If (Session("CANAL") = "MT") Then
                    strUsuario = Session("USUARIO")
                Else
                    strUsuario = ""
                End If
            Else
                strUsuario = ""
            End If

            Select Case strTipo
                Case "1"
                    objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))

                    'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")
                    ''Set objRecordSet = objComponente.Get_CuadreCajaVtasFact(cstr(strFecha), cstr(sOficVenta))

                    'CCRRM
                    StrXml = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), strUsuario)

                    objComponente = Nothing
                    ''Set objRecordSet = objComponente.Get_RS01()

                    'Set objRecordSet = XmlToRecordset(StrXml,"RS")
                    objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")


                Case "2"
                    objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))

                    'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")
                    ''Set objRecordSet = objComponente.Get_CuadreCajaMaterFact(cstr(strFecha), cstr(sOficVenta))

                    'StrXml = objComponente.Get_CuadreCajaMaterFact(cstr(strFecha), cstr(sOficVenta))

                    'CCRRM
                    StrXml = objComponente.Get_CuadreCajaMaterFact(CStr(strFecha), CStr(sOficVenta), strUsuario)

                    objComponente = Nothing
                    ''Set objRecordSet = objComponente.Get_RS01()

                    'Set objRecordSet = XmlToRecordset(StrXml,"RS")
                    objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")
                Case "3"
                    objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))

                    'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")
                    ''Set objRecordSet = objComponente.Get_CuadreCajaResumDia(cstr(strFecha), cstr(sOficVenta))
                    StrXml = objComponente.Get_CuadreCajaResumDia(CStr(strFecha), CStr(sOficVenta), strUsuario)
                    objComponente = Nothing
                    ''Set objRecordSet = objComponente.Get_RS01()

                    'Set objRecordSet = XmlToRecordset(StrXml,"RS")
                    objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")

            End Select

            Response.ContentType = "application/vnd.ms-excel"

            Select Case strTipo
                Case "1"
                    Response.Write("<TABLE border=1>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "REPORTE :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "CIERRE DE CAJA"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    If strIndividual = "1" Then
                        sValor = "VENTA POR TIPO DE DOCUMENTO (Individual)"
                    Else
                        sValor = "VENTA POR TIPO DE DOCUMENTO"
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "TIENDA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("ALMACEN"))
                    If Left(sValor, 1) = "0" Then
                        sValor = "'" & sValor
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("OFICINA"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "FECHA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("FechaAct"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "HORA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(TimeOfDay().Hour & ":" & TimeOfDay().Minute)
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Tipo Documento"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Fact. SAP"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Doc. SUNAT"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Vendedor"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Moneda"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Clase Factura"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    If sTipoTienda = "MT" Then
                        Response.Write("<TD align=center><B>")
                        sValor = "Cuotas"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                    End If
                    Response.Write("<TD align=center><B>")
                    sValor = "Total Doc."
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Efectivo"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "American Exp."
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "NetCard"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Cheque"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Cob. Interbank"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Electron"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Diners"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Maestro"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "MasterCard"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Ripley"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "CMR"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Visa"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Visa Internet"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Carsa"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Curacao"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Visa WEB"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Maestro WEB"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    If sTipoTienda = "MT" Then
                        Response.Write("<TD align=center><B>")
                        sValor = "3 Cuotas"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                        Response.Write("<TD align=center><B>")
                        sValor = "6 Cuotas"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                        Response.Write("<TD align=center><B>")
                        sValor = "12 Cuotas"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                    End If
                    Response.Write("<TD align=center><B>")
                    sValor = "Moneda Doc."
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    If sTipoTienda = "MT" Then
                        Response.Write("<TD align=center><B>")
                        sValor = "Cuotas Empl. Claro"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                        Response.Write("<TD align=center><B>")
                        sValor = "Cuotas Empl. Overall"
                        Response.Write(Server.HtmlEncode(sValor))
                        Response.Write("</B></TD>")
                    End If
                    Response.Write("<TD align=center><B>")
                    sValor = "Saldo"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    If Not objRecordSet Is Nothing Then
                        If objRecordSet.RecordCount > 0 Then
                            objRecordSet.MoveFirst()
                            iTotalCampos = objRecordSet.Fields.Count
                            Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                                Response.Write("<TR>" & vbCrLf)
                                For iIndexCampo = 0 To (iTotalCampos - 2)
                                    If iIndexCampo <> 0 Then
                                        sValor = CStr(objRecordSet.Fields(iIndexCampo).Value)
                                        If iIndexCampo = 3 Then
                                            sValor = Mid(CStr(objRecordSet.Fields(31).Value), 2)
                                        End If

                                        If iIndexCampo = 7 Or iIndexCampo = 26 Or iIndexCampo = 27 Or iIndexCampo = 28 Then
                                            If sTipoTienda = "MT" Then
                                                Response.Write("<TD>")
                                                Response.Write(Server.HtmlEncode(sValor))
                                                Response.Write("</TD>")
                                            End If
                                        Else
                                            If (iIndexCampo = 16) Then
                                            Else
                                                Response.Write("<TD>")
                                                If (iIndexCampo < 30) Then
                                                    If iIndexCampo = 2 Or iIndexCampo = 4 Then
                                                        If Left(sValor, 1) = "0" Then
                                                            sValor = "'" & sValor
                                                        End If
                                                        Response.Write(Server.HtmlEncode(sValor))
                                                    Else
                                                        Response.Write(Server.HtmlEncode(sValor))
                                                    End If
                                                End If
                                                If (iIndexCampo = 8) Then
                                                    Response.Write("</TD>")
                                                    Response.Write("<TD>")
                                                    sValor = CStr(objRecordSet.Fields(16).Value)
                                                    If iIndexCampo = 2 Or iIndexCampo = 4 Then
                                                        If Left(sValor, 1) = "0" Then
                                                            sValor = "'" & sValor
                                                        End If
                                                        Response.Write(Server.HtmlEncode(sValor))
                                                    Else
                                                        Response.Write(Server.HtmlEncode(sValor))
                                                    End If
                                                End If
                                                If (iIndexCampo >= 30) Then
                                                    If sTipoTienda = "MT" Then
                                                        If (iIndexCampo = 30) Then
                                                            sValor = CStr(objRecordSet.Fields(32).Value)
                                                            Response.Write(Server.HtmlEncode(sValor))
                                                        End If
                                                        If (iIndexCampo = 31) Then
                                                            sValor = CStr(objRecordSet.Fields(33).Value)
                                                            Response.Write(Server.HtmlEncode(sValor))
                                                        End If
                                                    End If
                                                    If (iIndexCampo = 32) Then
                                                        sValor = CStr(objRecordSet.Fields(30).Value)
                                                        Response.Write(Server.HtmlEncode(sValor))
                                                    End If
                                                End If

                                                Response.Write("</TD>")
                                            End If
                                        End If
                                    End If
                                Next
                                objRecordSet.MoveNext()
                                Response.Write("</TR>" & vbCrLf)
                            Loop
                        End If
                    End If
                    Response.Write("</TABLE>" & vbCrLf)

                Case "2"
                    Response.Write("<TABLE border=1>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "REPORTE :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "CIERRE DE CAJA"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    If strIndividual = "1" Then
                        sValor = "VENTA DETALLADA (Individual)"
                    Else
                        sValor = "VENTA DETALLADA"
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "TIENDA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("ALMACEN"))
                    If Left(sValor, 1) = "0" Then
                        sValor = "'" & sValor
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("OFICINA"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "FECHA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("FechaAct"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "HORA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(TimeOfDay().Hour & ":" & TimeOfDay().Minute)
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Tipo Documento"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Fact. SAP"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Doc. SUNAT"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Vendedor"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Material"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Descripción"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "UMB"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Cantidad"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Valor Venta"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Número Serie"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Utilización"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Descripción"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    If Not objRecordSet Is Nothing Then
                        If objRecordSet.RecordCount > 0 Then
                            objRecordSet.MoveFirst()
                            iTotalCampos = objRecordSet.Fields.Count
                            Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                                Response.Write("<TR>" & vbCrLf)
                                For iIndexCampo = 0 To (iTotalCampos - 1)
                                    Response.Write("<TD>")
                                    sValor = CStr(objRecordSet.Fields(iIndexCampo).Value)
                                    If iIndexCampo = 2 Then
                                        sValor = Mid(CStr(objRecordSet.Fields(12).Value), 2)
                                    End If
                                    If iIndexCampo < 12 Then
                                        If iIndexCampo = 1 Or iIndexCampo = 3 Or iIndexCampo = 9 Or iIndexCampo = 10 Then
                                            If Left(sValor, 1) = "0" Or iIndexCampo = 9 Then
                                                'sValor = "'" & sValor
                                                sValor = sValor
                                                If Len(sValor) > 10 Then
                                                    sValor = "'" & sValor
                                                End If
                                            End If
                                            Response.Write(Server.HtmlEncode(sValor))
                                        Else
                                            Response.Write(Server.HtmlEncode(sValor))
                                        End If
                                        Response.Write("</TD>")
                                    End If
                                Next
                                objRecordSet.MoveNext()
                                Response.Write("</TR>" & vbCrLf)
                            Loop
                        End If
                    End If
                    Response.Write("</TABLE>" & vbCrLf)

                Case "3"
                    Response.Write("<TABLE border=1>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "REPORTE :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    If strIndividual = "1" Then
                        sValor = "CIERRE DE CAJA INDIVIDUAL"
                    Else
                        sValor = "CIERRE DE CAJA"
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "CUADRE DE CAJA"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "TIENDA :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("ALMACEN"))
                    If Left(sValor, 1) = "0" Then
                        sValor = "'" & sValor
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("OFICINA"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    'Response.Write "<TR>" & vbCrLf
                    'Response.Write "<TD align=center><B>"
                    'sValor = "FECHA :"
                    'Response.Write Server.HTMLEncode(sValor)
                    'Response.Write "</B></TD>"
                    'Response.Write "<TD align=center><B>"
                    'sValor = CStr(Session("FechaAct"))
                    'Response.Write Server.HTMLEncode(sValor)
                    'Response.Write "</B></TD>"
                    'Response.Write "<TD align=center><B>"
                    'sValor = "HORA :"
                    'Response.Write Server.HTMLEncode(sValor)
                    'Response.Write "</B></TD>"
                    'Response.Write "<TD align=center><B>"
                    'sValor = CStr(time())
                    'Response.Write Server.HTMLEncode(sValor)
                    'Response.Write "</B></TD>"
                    'Response.Write "</TR>" & vbCrLf
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Orden"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Descripción"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Monto"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    If Not objRecordSet Is Nothing Then
                        If objRecordSet.RecordCount > 0 Then
                            objRecordSet.MoveFirst()
                            iTotalCampos = objRecordSet.Fields.Count
                            Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                                Response.Write("<TR>" & vbCrLf)
                                For iIndexCampo = 0 To (iTotalCampos - 1)
                                    Response.Write("<TD>")
                                    sValor = CStr(objRecordSet.Fields(iIndexCampo).Value)
                                    Response.Write(Server.HtmlEncode(sValor))
                                    Response.Write("</TD>")
                                Next
                                objRecordSet.MoveNext()
                                Response.Write("</TR>" & vbCrLf)
                            Loop
                        End If
                    End If
                    Response.Write("</TABLE>" & vbCrLf)

                Case "4"
                    Response.Write("<TABLE border=1>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "REPORTE :"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    If strIndividual = "1" Then
                        sValor = "ANULACION POR CAJERO (Individual)"
                    Else
                        sValor = "ANULACION POR CAJERO"
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")

                    Response.Write("<TD align=center><B>")
                    sValor = "CUADRE DE CAJA"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Tienda:"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("ALMACEN"))
                    If Left(sValor, 1) = "0" Then
                        sValor = "'" & sValor
                    End If
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = CStr(Session("OFICINA"))
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")

                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Cajero:"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")

                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("</TR>" & vbCrLf)
                    Response.Write("<TR>" & vbCrLf)
                    Response.Write("<TD align=center><B>")
                    sValor = "Fact. SAP"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Doc.Sunat"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Total Factura"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Forma Pago"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Nro Tarjeta/Documento "
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("<TD align=center><B>")
                    sValor = "Monto Pagado"
                    Response.Write(Server.HtmlEncode(sValor))
                    Response.Write("</B></TD>")
                    Response.Write("</TR>" & vbCrLf)
                    If Not objRecordSet Is Nothing Then
                        If objRecordSet.RecordCount > 0 Then
                            objRecordSet.MoveFirst()
                            iTotalCampos = objRecordSet.Fields.Count
                            Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                                Response.Write("<TR>" & vbCrLf)
                                For iIndexCampo = 0 To (iTotalCampos - 1)
                                    Response.Write("<TD>")
                                    sValor = CStr(objRecordSet.Fields(iIndexCampo).Value)
                                    Response.Write(Server.HtmlEncode(sValor))
                                    Response.Write("</TD>")
                                Next
                                objRecordSet.MoveNext()
                                Response.Write("</TR>" & vbCrLf)
                            Loop
                        End If
                    End If
                    Response.Write("</TABLE>" & vbCrLf)

            End Select

            objRecordSet = Nothing
        objComponente = Nothing
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
