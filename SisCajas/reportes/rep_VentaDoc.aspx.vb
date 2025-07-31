Imports SisCajas.GenFunctions
Imports SisCajas.clsAudi
Imports System.Globalization

Public Class rep_VentaDoc
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
    '    Public strFecha As String
    Public auxfechaact
    'Introducir aquí el código de usuario para inicializar la página
    Public StrXml
    Public iAux, sFechaActual, sHoraActual, sTipoTienda, sAncho
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB
    Public auxprint As String = ""
    Public bMostrar As Boolean = False
    Dim objComponente As New SAP_SIC_Pagos.clsPagos
    Dim dsReturn As DataSet
    Dim objRecordSet As DataTable
    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "INICIO - Page_Load")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")

        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        'INICIO - Modificado por TS-CCC
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim constValueZero As String = "0"

        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "USUARIO ->" & Funciones.CheckStr(Session("USUARIO")))

        Try
            If (Session("USUARIO") Is Nothing) Then
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "RUTA ->" & Funciones.CheckStr(ConfigurationSettings.AppSettings("RutaSite")))
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN - Page_Load")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")

                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            Else
                Dim i As Integer
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FECHA ->" & Funciones.CheckStr(Request("strFecha")))
                strFecha = Request("strFecha")

                Dim ObjAux As New GenFunctions(Server, Request, Response)
                Dim ObjAudi As New clsAudi
                auxfechaact = Me.strFecha
                Session("sFecVenta") = strFecha
                'Session("VarVal3") = "0006"
                'Session("ALMACEN") = "0006"
                'Session("VarVal2") = "MT"
                Session("STRMessage") = ""

                'Fecha limite
                'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepFactDet")
                Dim flagSinergia As String
                Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "strFechaLim ->" & Funciones.CheckStr(strFechaLim))
                Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "anio ->" & Funciones.CheckStr(anio))
                Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "mes ->" & Funciones.CheckStr(mes))
                Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "dia ->" & Funciones.CheckStr(dia))
                Dim dFechaLim As New Date(anio, mes, dia)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "dFechaLim ->" & Funciones.CheckStr(dFechaLim))
                Dim dFecha As Date = DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "dFecha ->" & Funciones.CheckStr(dFecha))

                sOficVenta = Session("ALMACEN")
                'Response.Write(strFecha)
                '  objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))
                'CCRRM
                ' StrXml = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), "")
                If dFecha < dFechaLim Then
                    dsReturn = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), "")
                    bMostrar = True
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "bMostrar ->" & Funciones.CheckStr(bMostrar))
                Else
                    dsReturn = objOffline.GetFacturacionDet(CStr(sOficVenta), CDate(strFecha).ToString("yyyyMMdd"), "0")
                    bMostrar = False
                    Session("rptFacturacionXPdv") = dsReturn
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "bMostrar ->" & Funciones.CheckStr(bMostrar))
                End If

                objRecordSet = dsReturn.Tables(0)

                objComponente = Nothing
                'objRecordSet = Nothing

                sFechaActual = Session("sFecVenta")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "sFechaActual ->" & Funciones.CheckStr(sFechaActual))
                sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "sHoraActual ->" & Funciones.CheckStr(sHoraActual))
                'sTipoTienda = Trim(Session("VarVal2"))
                sTipoTienda = Trim(Session("CANAL"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "sTipoTienda ->" & Funciones.CheckStr(sTipoTienda))
                iAux = 0
                'objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")
                If Not objRecordSet Is Nothing Then
                    '  If Not objRecordSet.eof Then
                    ' objRecordSet.MoveFirst()
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "objRecordSet no es nothing.")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "objRecordSet.Rows.Count ->" & Funciones.CheckStr(objRecordSet.Rows.Count))

                    For i = 0 To objRecordSet.Rows.Count - 1
                        ' Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                        iAux = iAux + 1
                        If iAux Mod 2 = 1 Then
                            auxprint = auxprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        Else
                            auxprint = auxprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("DSCOM")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("VBELN")) & "</td>"
                        If bMostrar Then
                            auxprint = auxprint & "<td height=25 align=center>" & Mid(Trim(objRecordSet.Rows(i).Item("REFERENCIA")), 2) & "</td>"
                        Else
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("REFERENCIA")) & "</td>"
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("KUNNR")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("WAERK")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("FKART")) & "</td>"
                        If sTipoTienda = "MT" Then
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUOTA")) & "</td>"
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("TOTFA")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEFE")) & "</td>"

                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZAEX")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCAR")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCHQ")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCIB")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDEL")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDIN")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDMT")) & "</td>"

                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZMCD")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZRIP")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZSAG")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVIS")) & "</td>"
                        If bMostrar Then
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("VIIN")) & "</td>" 'NN
                        Else
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCRS")) & "</td>"
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCZO")) & "</td>"
                        If bMostrar Then
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVW1")) & "</td>" 'NN
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVW2")) & "</td>" 'NN
                        Else
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZACE")) & "</td>"
                        If sTipoTienda = "MT" Then
                            If bMostrar Then
                                auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO3")) & "</td>"
                            Else
                                auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO1")) & "</td>"
                            End If
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO6")) & "</td>"
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO12")) & "</td>"
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO18")) & "</td>"
                            If Not bMostrar Then
                                auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO24")) & "</td>"
                            End If
                        End If
                        If bMostrar Then
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("NODEF")) & "</td>" 'NN
                        Else
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If
                        If sTipoTienda = "MT" Then
                            'Campaña Empleado
                            auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEAM")) & "</td>"
                            If bMostrar Then
                                auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEOV")) & "</td>" 'NN
                            Else
                                auxprint = auxprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                            End If
                            'Campaña Empleado
                        End If
                        auxprint = auxprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("SALDO")) & "</td>"
                        auxprint = auxprint & "</tr>"
                        ' objRecordSet.MoveNext()
                        'Loop
                    Next

                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "auxprint ->" & Funciones.CheckStr(auxprint))

                    'End If
                End If
                objRecordSet = Nothing
                objComponente = Nothing

                If Session("STRMessage") = "" Then
                    sAncho = "500"
                Else
                    sAncho = "200"
                End If
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "sAncho ->" & Funciones.CheckStr(sAncho))

                Session("STRMessage") = ""
                If (Len(Session("STRMessage")) > 0) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "STRMessage ->" & Funciones.CheckStr(Len(Session("STRMessage"))))
                    Response.Write("<script language=JavaScript type='text/javascript'>")
                    Response.Write("alert('" & Session("STRMessage") & "');")
                    Response.Write("</script>")
                End If
                Session("STRMessage") = ""
            End If
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message + "'); </script>")
        Finally
            objOffline = Nothing
        End Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "FIN - Page_Load")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "- " & "-------------------------------------------------")

        'FIN - Modificado por TS-CCC
    End Sub

End Class
