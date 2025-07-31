Imports SisCajas.GenFunctions
Imports SisCajas.clsAudi
Imports SisCajas.clsLib_Session
Imports SisCajas.clsContantes_site

Public Class rep_RVentFact
    Inherits System.Web.UI.Page


    Public codAplicacion As Integer
    Public strOptCuadreCaja As String             'Codigo Opcion
    Public strEvtCuadreCaja As String 'Codigo Evento

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
            codAplicacion = Page.Request.QueryString("codapp")
            strOptCuadreCaja = Page.Request.QueryString("opcion")
            strEvtCuadreCaja = Page.Request.QueryString("evento")

            '************** Ini :: Session Clear ********* //TS-CCC
            Session("rptFacturacionXPdv") = Nothing
            Session("rptFacturacionXCajero") = Nothing
            Session("rptFactMaterXPdv") = Nothing
            Session("rptFactMaterXCajero") = Nothing
            '************** Fin :: Session Clear *********

            Dim ObjAux As New GenFunctions(Server, Request, Response)
            Dim ObjAudi As New clsAudi

            Dim objComponente, objRecordSet, objRecordSetB, sValorA, sValorB
            Dim strCodOper, sOficVenta, sFecVenta
            Dim strSaldo, strIngreso, strCaja, strRemesa, strMontoP, strMontoS, strCierre, strFecha
            Dim strIndividual, paginaRet, strUsuario

            ' AUDITORIA - Variables
            Dim Detalle(1, 1), intNumReg, strResultado, strDescResultado, Codigo_Auditoria
            Dim wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8

            Dim arrRS01(50)
            Dim strData
            Dim ii, jj
            Dim strCodVendedor, strSaldoIni, strActualizar, strFechaSal
            Dim flgError, ErrorMessage
            Dim strDay, strMonth, strYear

            ' Variables Globales
            strCodOper = Request.QueryString("codOperacion")
            strSaldo = Trim(Request.Form("txtSaldo"))
            strIngreso = Trim(Request.Form("txtIngreso"))
            strCaja = Trim(Request.Form("txtCaja"))
            strRemesa = Trim(Request.Form("txtRemesa"))
            strMontoP = Trim(Request.Form("txtMontoP"))
            strMontoS = Trim(Request.Form("txtMontoS"))
            strIndividual = Trim(Request.Form("Individual"))

            strFecha = Trim(Request.Form("txtFecha"))
            Session("sFecVenta") = strFecha

            If Request.Form("txtCierre") = "1" Then
                strCierre = "X"
            Else
                strCierre = " "
            End If
            If strCodOper = "" Then
                strCodOper = Request.Form("codOperacion")
            End If
            If strSaldo = "" Then
                strSaldo = "0.00"
            End If
            If strIngreso = "" Then
                strIngreso = "0.00"
            End If
            If strCaja = "" Then
                strCaja = "0.00"
            End If
            If strRemesa = "" Then
                strRemesa = "0.00"
            End If
            If strMontoP = "" Then
                strMontoP = "0.00"
            End If
            If strMontoS = "" Then
                strMontoS = "0.00"
            End If

            sOficVenta = Session("ALMACEN")
            'sFecVenta = Session("FechaAct")

            ' Variables de Auditoria
            wParam1 = Session("USUARIO") 'Codigo Usuario
            wParam5 = codAplicacion   'Codigo Aplicacion
            wParam7 = Session("codPerfil") 'Codigo Perfil
            wParam8 = Session("Host")  'Nombre Host Remote

            If Trim(strCodOper) = "01" Then
                'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")

                ' Variables de Auditoria
                wParam2 = strOptCuadreCaja 'Codigo Opcion
                wParam6 = strEvtCuadreCaja 'Codigo Evento

                intNumReg = 9
                ReDim Detalle(intNumReg, 3)

                Detalle(1, 1) = "Saldo" '"STCDT"
                Detalle(1, 2) = strSaldo
                Detalle(1, 3) = "Saldo"

                Detalle(2, 1) = "Ingreso" '"STCD1"
                Detalle(2, 2) = strIngreso
                Detalle(2, 3) = "Ingreso"

                Detalle(3, 1) = "CajaBzn" '"STCD1"
                Detalle(3, 2) = strCaja
                Detalle(3, 3) = "Caja Buzon"

                Detalle(4, 1) = "Remesa" '"STCD1"
                Detalle(4, 2) = strRemesa
                Detalle(4, 3) = "Remesa"

                Detalle(5, 1) = "MontoP" '"STCD1"
                Detalle(5, 2) = strMontoP
                Detalle(5, 3) = "Monto Pendiente"

                Detalle(6, 1) = "MontoS" '"STCD1"
                Detalle(6, 2) = strMontoS
                Detalle(6, 3) = "Monto Saldo"

                Detalle(7, 1) = "Cierre" '"STCD1"
                Detalle(7, 2) = strCierre
                Detalle(7, 3) = "Cierre"

                If strIndividual = "1" Then
                    Detalle(8, 1) = "Usuario"
                    Detalle(8, 2) = Session("USUARIO")
                    Detalle(8, 3) = "Usuario"
                Else
                    Detalle(8, 1) = "Usuario"
                    Detalle(8, 2) = ""
                    Detalle(8, 3) = "Usuario"
                End If

                Detalle(9, 1) = "AuditLog"
                Detalle(9, 2) = Session("VarVal2") & "/" & Session("ALMACEN") & "/" & Request.ServerVariables("SERVER_NAME")
                Detalle(9, 3) = "AuditLog"

                'Set objRecordSet = objComponente.Get_CuadreCaja(cstr(sFecVenta), cstr(sOficVenta), cstr(strSaldo), cstr(strIngreso), cstr(strCaja), cstr(strRemesa), cstr(strMontoP), cstr(strMontoS), cstr(strCierre))
                'Set objRecordSet = objComponente.Get_CuadreCaja("", "", "", "", "", "", "", "", "")

                objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))
                'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")

                '*****************CAMBIO DE DLL******************
                Dim StrXmlDll1
                'Response.Write  cstr(strFecha) & "," & cstr(sOficVenta) & "," & cstr(strSaldo) & "," & cstr(strIngreso) & "," & cstr(strCaja) & "," & cstr(strRemesa) & "," & cstr(strMontoP) & "," & cstr(strMontoS) & "," & cstr(strCierre)
                'Response.End 

                'StrXmlDll1= objComponente.Get_CuadreCajaProceso(cstr(strFecha), cstr(sOficVenta), cstr(strSaldo), cstr(strIngreso), cstr(strCaja), cstr(strRemesa), cstr(strMontoP), cstr(strMontoS), cstr(strCierre))

                If strIndividual = "1" Then
                    strUsuario = Session("USUARIO")
                    paginaRet = "repCuadreProcesoInd.asp"
                Else
                    strUsuario = ""
                    paginaRet = "repCuadreProceso.asp"
                End If

                'CCRRM
                StrXmlDll1 = objComponente.Get_CuadreCajaProceso(CStr(strFecha), CStr(sOficVenta), CStr(strSaldo), _
                    CStr(strIngreso), CStr(strCaja), CStr(strRemesa), CStr(strMontoP), CStr(strMontoS), _
                    CStr(strCierre), strUsuario)

                objRecordSet = ObjAux.XmlToRecordset(StrXmlDll1, "RS")
                'Set objRecordSet = objComponente.Get_CuadreCajaProceso(cstr(strFecha), cstr(sOficVenta), cstr(strSaldo), cstr(strIngreso), cstr(strCaja), cstr(strRemesa), cstr(strMontoP), cstr(strMontoS), cstr(strCierre))		
                '************************************************

                objComponente = Nothing

                If Not objRecordSet Is Nothing Then
                    If Not objRecordSet.eof Then
                        objRecordSet.MoveFirst()
                        Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                            sValorA = Trim(objRecordSet.Fields(0).Value)
                            sValorB = Trim(objRecordSet.Fields(3).Value)
                            If sValorA = "E" Then
                                'Set Session("InfoCuadreCaja") = objComponente
                                Session("STRMessage") = sValorB

                                ' Auditoria
                                strResultado = 0
                                strDescResultado = sValorB
                                wParam3 = strResultado   'Resultado Búsqueda   
                                wParam4 = strDescResultado  'Descripcion Resultado
                                Codigo_Auditoria = ObjAudi.Registro_Audi(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, Detalle.GetValue(1))
                                Page.Response.Write("<form id=frm method='POST' action='" & Trim(paginaRet) & "'>" & Chr(10) & Chr(13))
                                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                                Page.Response.Write("</script>" & Chr(10) & Chr(13))
                            End If
                            objRecordSet.MoveNext()
                        Loop
                    End If
                End If

                If CStr(strCierre) = "X" Then
                    ' Auditoria
                    strResultado = 1
                    strDescResultado = "Cierre Realizado."
                    wParam3 = strResultado   'Resultado Búsqueda   
                    wParam4 = strDescResultado  'Descripcion Resultado
                    Codigo_Auditoria = ObjAudi.Registro_Audi(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, Detalle.GetValue(1))
                End If

                objRecordSet = Nothing
                objComponente = Nothing
                Session("STRMessage") = "Proceso enviado con éxito"

                Page.Response.Write("<form id=frm method='POST' action='" & Trim(paginaRet) & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                Page.Response.Write("</script>" & Chr(10) & Chr(13))
            End If

            If Trim(strCodOper) = "02" Then

                Page.Response.Write("<form id=frm method='POST' action='rep_VentaDoc.aspx'>" & Chr(10) & Chr(13))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                Page.Response.Write("</script>" & Chr(10) & Chr(13))

            End If

            If Trim(strCodOper) = "03" Then

                Page.Response.Write("<form id=frm method='POST' action='rep_ventaDetallada.aspx'>" & Chr(10) & Chr(13))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                Page.Response.Write("</script>" & Chr(10) & Chr(13))

            End If

            If Trim(strCodOper) = "04" Then

                Page.Response.Write("<form id=frm method='POST' action='cuadreCaja.asp'>" & Chr(10) & Chr(13))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("<input type=hidden name='Individual' value='" & strIndividual & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                Page.Response.Write("</script>" & Chr(10) & Chr(13))

            End If

            If Trim(strCodOper) = "05" Then

                ' Variables de Auditoria
                wParam2 = strOptCuadreCaja 'Codigo Opcion
                wParam6 = strEvtCuadreCaja 'Codigo Evento

                objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))
                'Set objComponente = Server.CreateObject("COM_PVU_CuadreCaja.SAPCuadreCaja")

                strDay = CStr(Day(Now()))
                If Len(strDay) = 1 Then strDay = 0 & strDay
                strMonth = CStr(Month(Now()))
                If Len(strMonth) = 1 Then strMonth = 0 & strMonth
                strYear = CStr(Year(Now()))

                strFechaSal = strDay & "/" & strMonth & "/" & strYear
                strActualizar = "S"

                strSaldoIni = Request("txtSaldoInicial")
                strCodVendedor = Request("cboVendedor")

                intNumReg = 6
                ReDim Detalle(intNumReg, 3)

                Detalle(1, 1) = "Fecha Saldo"
                Detalle(1, 2) = strFechaSal
                Detalle(1, 3) = "Fecha Saldo"

                Detalle(2, 1) = "Oficina"
                Detalle(2, 2) = Session("ALMACEN")
                Detalle(2, 3) = "Oficina"

                Detalle(3, 1) = "CodVendedor"
                Detalle(3, 2) = strCodVendedor
                Detalle(3, 3) = "Codigo de Vendedor"

                Detalle(4, 1) = "SaldoIni"
                Detalle(4, 2) = strSaldoIni
                Detalle(4, 3) = "Saldo Inicial"

                Detalle(5, 1) = "Actualizar"
                Detalle(5, 2) = strActualizar
                Detalle(5, 3) = "Flag Actualizar"

                Detalle(6, 1) = "AuditLog" '"STCDT"
                Detalle(6, 2) = Session("VarVal2") & "/" & Session("Almacen") & "/" & Request.ServerVariables("SERVER_NAME")
                Detalle(6, 3) = "AuditLog"
                Dim StrXmlDll1
                StrXmlDll1 = objComponente.Set_AsignaSaldoInicial(CStr(strFechaSal), CStr(Session("ALMACEN")), _
                   strCodVendedor, strSaldoIni, strActualizar)

                objRecordSet = ObjAux.XmlToRecordset(StrXmlDll1, "RS")

                ErrorMessage = ObjAux.IsExistError(objRecordSet, 1)
                'Response.Write "StrXml = " & StrXml & "<br>"

                If Len(ErrorMessage) > 0 Then
                    ' Hubo errores en la Asignacion de Saldo Inicial
                    flgError = True
                    Session("STRMessage") = ErrorMessage

                    strResultado = 0
                    strDescResultado = "Error al asignar Saldo Inicial."
                    wParam3 = strResultado   'Resultado Búsqueda   
                    wParam4 = strDescResultado  'Descripcion Resultado
                    Codigo_Auditoria = ObjAudi.Registro_Audi(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, Detalle.GetValue(1))

                Else
                    Session("STRMessage") = "Saldo Inicial Actualizado"
                    flgError = False

                    strResultado = 1
                    strDescResultado = "Cierre Realizado."
                    wParam3 = strResultado   'Resultado Búsqueda   
                    wParam4 = strDescResultado  'Descripcion Resultado
                    Codigo_Auditoria = ObjAudi.Registro_Audi(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, Detalle.GetValue(1))
                End If

                Page.Response.Write("<form id=frm method='POST' action='asigCajero.asp'>" & Chr(13) & Chr(10))
                Page.Response.Write("</form>" & Chr(13) & Chr(10))
                Page.Response.Write("<script language=javascript>" & Chr(13) & Chr(10))
                Page.Response.Write("frm.submit()" & Chr(13) & Chr(10))
                Page.Response.Write("</script>" & Chr(13) & Chr(10))
            End If

            If Trim(strCodOper) = "06" Then
                Page.Response.Write("<form id=frm method='POST' action='rep_VentaInd.aspx'>" & Chr(13) & Chr(10))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(13) & Chr(10))
                Page.Response.Write("</form>" & Chr(13) & Chr(10))

                Page.Response.Write("<script language=javascript>" & Chr(13) & Chr(10))
                Page.Response.Write("frm.submit()" & Chr(13) & Chr(10))
                Page.Response.Write("</script>" & Chr(13) & Chr(10))
            End If
            If Trim(strCodOper) = "07" Then
                Page.Response.Write("<form id=frm method='POST' action='rep_ventaDetalladaInd.aspx'>" & Chr(13) & Chr(10))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(13) & Chr(10))
                Page.Response.Write("</form>" & Chr(13) & Chr(10))

                Page.Response.Write("<script language=javascript>" & Chr(13) & Chr(10))
                Page.Response.Write("frm.submit()" & Chr(13) & Chr(10))
                Page.Response.Write("</script>" & Chr(13) & Chr(10))
            End If

            If Trim(strCodOper) = "08" Then

                Page.Response.Write("<form id=frm method='POST' action='rep_OperaDetallada.aspx'>" & Chr(10) & Chr(13))
                Page.Response.Write("<input type=hidden name='strFecha' value='" & strFecha & "'>" & Chr(10) & Chr(13))
                Page.Response.Write("</form>" & Chr(10) & Chr(13))

                Page.Response.Write("<script language=javascript>" & Chr(10) & Chr(13))
                Page.Response.Write("frm.submit()" & Chr(10) & Chr(13))
                Page.Response.Write("</script>" & Chr(10) & Chr(13))
            End If
        End If
    End Sub

End Class
