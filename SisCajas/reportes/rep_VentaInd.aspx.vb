Imports SisCajas.clsContantes_site
Imports SisCajas.clsLib_Session
Imports SisCajas.clsAudi
Imports SisCajas.GenFunctions
Imports System.Globalization

Public Class rep_VentaInd
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
    'Public objComponente, objRecordSet, StrXml
    Public iAux, sFechaActual, sHoraActual, sTipoTienda, sAncho
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB

    Public strcadenaprint As String = ""
    Public strRuta As String
    Public bMostrar As Boolean = False
    Dim objComponente As New SAP_SIC_Pagos.clsPagos
    Dim dsReturn As DataSet
    Dim objRecordSet As DataTable


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        'INICIO - Modificado por TS-CCC
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim constValueZero As String = "0"

        Try
            If (Session("USUARIO") Is Nothing) Then
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            Else
                Dim i As Integer
                strFecha = Request("strFecha")

                Dim ObjCtes As New clsContantes_site
                strRuta = ObjCtes.strRutaSite
                Dim ObjAux As New GenFunctions(Server, Request, Response)
                Dim ObjAudi As New clsAudi
                Session("sFecVenta") = strFecha
                'Session("VarVal3") = "0006"
                'Session("ALMACEN") = "0006"
                'Session("VarVal2") = "MT"
                'Session("STRMessage") = ""

                'Fecha limite
                Dim flagSinergia As String
                Dim strFechaLim As String = objOffline.GetFechaOficinaSinergia(CStr(Session("ALMACEN")), flagSinergia)
                'Dim strFechaLim As String = ConfigurationSettings.AppSettings("FechaFinConsultaRFC_RepFactDet")
                Dim anio As Integer = CInt(strFechaLim.Substring(6, 4))
                Dim mes As Integer = CInt(strFechaLim.Substring(3, 2))
                Dim dia As Integer = CInt(strFechaLim.Substring(0, 2))
                Dim dFechaLim As New Date(anio, mes, dia)
                Dim dFecha As Date = DateTime.ParseExact(strFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)

                sOficVenta = Session("ALMACEN")
                'objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))
                'CCRRM
                If (Session("CANAL") = "MT") Then
                    If dFecha < dFechaLim Then
                        'StrXml = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), Session("USUARIO"))
                        dsReturn = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), Session("USUARIO"))
                        bMostrar = True
                    Else
                        Dim strUsuario As String = CStr(Session("USUARIO")).PadLeft(10, CChar("0"))
                        dsReturn = objOffline.GetFacturacionDet(CStr(sOficVenta), CDate(strFecha).ToString("yyyyMMdd"), strUsuario)
                        bMostrar = False
                        Session("rptFacturacionXCajero") = dsReturn
                    End If
                Else
                    If dFecha < dFechaLim Then
                        'StrXml = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), "")
                        dsReturn = objComponente.Get_CuadreCajaVtasFact(CStr(strFecha), CStr(sOficVenta), "")
                        bMostrar = True
                    Else
                        dsReturn = objOffline.GetFacturacionDet(CStr(sOficVenta), CDate(strFecha).ToString("yyyyMMdd"), "0")
                        bMostrar = False
                        Session("rptFacturacionXPdv") = dsReturn
                    End If

                End If

                objComponente = Nothing
                'objRecordSet = Nothing

                objRecordSet = dsReturn.Tables(0)

                sFechaActual = Session("sFecVenta")
                'sHoraActual = Now()
                sHoraActual = DateTime.Now.ToString("t")
                sTipoTienda = Trim(Session("CANAL"))

                'vadenaprint
                iAux = 0
                ' objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")
                If Not objRecordSet Is Nothing Then
                    ' If Not objRecordSet.eof Then
                    ' objRecordSet.MoveFirst()
                    For i = 0 To objRecordSet.Rows.Count - 1
                        ' Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                        iAux = iAux + 1
                        If iAux Mod 2 = 1 Then
                            strcadenaprint = strcadenaprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        Else
                            strcadenaprint = strcadenaprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        End If
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("DSCOM")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("VBELN")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Mid(Trim(objRecordSet.Rows(i).Item("REFERENCIA")), 2) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("KUNNR")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("WAERK")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("FKART")) & "</td>"
                        If sTipoTienda = "MT" Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUOTA")) & "</td>"
                        End If
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("TOTFA")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEFE")) & "</td>"

                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZAEX")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCAR")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCHQ")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCIB")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDEL")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDIN")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZDMT")) & "</td>"

                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZMCD")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZRIP")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZSAG")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVIS")) & "</td>"
                        If bMostrar Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("VIIN")) & "</td>" 'NN
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If

                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCRS")) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZCZO")) & "</td>"

                        If bMostrar Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVW1")) & "</td>"
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZVW2")) & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If

                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZACE")) & "</td>"
                        If sTipoTienda = "MT" Then
                            If bMostrar Then
                                strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO3")) & "</td>"
                            Else
                                strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO1")) & "</td>"
                            End If

                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO6")) & "</td>"
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO12")) & "</td>"
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO18")) & "</td>"

                            If Not bMostrar Then
                                strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("CUO24")) & "</td>"
                            End If
                        End If
                        If bMostrar Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("NODEF")) & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                        End If

                        If sTipoTienda = "MT" Then
                            'Campaña Empleado
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEAM")) & "</td>"
                            If bMostrar Then
                                strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("ZEOV")) & "</td>"
                            Else
                                strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(constValueZero) & "</td>"
                            End If
                            'Campaña Empleado
                        End If
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item("SALDO")) & "</td>"
                        strcadenaprint = strcadenaprint & "</tr>"
                        '  objRecordSet.MoveNext()
                        ' Loop
                    Next
                    ' End If
                End If
                objRecordSet = Nothing
                objComponente = Nothing
                'fin cadneprint

                If Session("STRMessage") = "" Then
                    sAncho = "500"
                Else
                    sAncho = "200"
                End If
                Session("STRMessage") = ""
                If (Len(Session("STRMessage")) > 0) Then
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
        'FIN - Modificado por TS-CCC
    End Sub

End Class
