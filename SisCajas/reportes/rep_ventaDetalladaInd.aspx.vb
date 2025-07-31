Imports SisCajas.clsContantes_site
Imports SisCajas.clsLib_Session
Imports SisCajas.clsAudi
Imports SisCajas.GenFunctions
Imports System.Globalization

Public Class rep_ventaDetalladaInd
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

    '  Public objComponente, objRecordSet
    Public iAux, sFechaActual, sHoraActual, StrXml
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB

    Public strcadenaprint As String = ""
    Public strRuta As String
    Dim objComponente As New SAP_SIC_Pagos.clsPagos
    Dim dsReturn As DataSet
    Dim objRecordSet As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")

        'INICIO - Modificado por TS-CCC
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim bMostrar As Boolean = False
        Try

            If (Session("USUARIO") Is Nothing) Then
                Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
                Response.Redirect(strRutaSite)
                Response.End()
                Exit Sub
            Else
                strFecha = Request("strFecha")
                Dim i As Integer
                Dim ObjCtes As New clsContantes_site
                strRuta = ObjCtes.strRutaSite
                Dim ObjAux As New GenFunctions(Server, Request, Response)
                Dim ObjAudi As New clsAudi
                Session("sFecVenta") = strFecha
                'Session("VarVal3") = "0006"
                'Session("ALMACEN") = "0006"
                'Session("VarVal2") = "MT"
                Session("STRMessage") = ""

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
                '  objComponente = ObjAux.CreateObjectCuadreCaja(Session("ALMACEN"))
                'CCRRM
                If (Session("CANAL") = "MT") Then
                    If dFecha < dFechaLim Then
                        ' StrXml = objComponente.Get_CuadreCajaMaterFact(CStr(strFecha), CStr(sOficVenta), Session("USUARIO"))
                        dsReturn = objComponente.Get_CuadreCajaMaterFact(CStr(strFecha), CStr(sOficVenta), Session("USUARIO"))
                        bMostrar = True
                    Else
                        Dim strCajero As String = CStr(Session("USUARIO")).PadLeft(10, CChar("0"))
                        dsReturn = objOffline.GetFactMaterialDet(CStr(sOficVenta), CDate(strFecha).ToString("yyyyMMdd"), strCajero)
                        Session("rptFactMaterXCajero") = dsReturn
                        bMostrar = False
                    End If
                Else
                    If dFecha < dFechaLim Then
                        'StrXml = objComponente.Get_CuadreCajaMaterFact(CStr(strFecha), CStr(sOficVenta), "")
                        dsReturn = objComponente.Get_CuadreCajaMaterFact(CStr(strFecha), CStr(sOficVenta), "")
                        bMostrar = True
                    Else
                        dsReturn = objOffline.GetFactMaterialDet(CStr(sOficVenta), CDate(strFecha).ToString("yyyyMMdd"), "0")
                        Session("rptFactMaterXPdv") = dsReturn
                        bMostrar = False
                    End If
                End If

                objComponente = Nothing
                objRecordSet = dsReturn.Tables(0)
                ' objRecordSet = Nothing

                sFechaActual = Session("sFecVenta")
                'sHoraActual = Now()
                sHoraActual = DateTime.Now.ToString("t")

                'cadenaprint

                'objRecordSet = ObjAux.XmlToRecordset(StrXml, "RS01")
                If Not objRecordSet Is Nothing Then
                    ' If objRecordSet.RecordCount > 0 Then
                    ' objRecordSet.MoveFirst()
                    iAux = 0
                    For i = 0 To objRecordSet.Rows.Count - 1
                        'Do While (Not objRecordSet.BOF And Not objRecordSet.EOF)
                        iAux = iAux + 1
                        If iAux Mod 2 = 1 Then
                            strcadenaprint = strcadenaprint & "<tr align=center bgcolor=#d0d8f0 class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        Else
                            strcadenaprint = strcadenaprint & "<tr align=center bgcolor=#DEE9FA class=Arial11B onMouseOut=this.className='Arial11B';return false>"
                        End If
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(0)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(1)) & "</td>"
                        If bMostrar Then
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Mid(Trim(objRecordSet.Rows(i).Item(12)), 2) & "</td>"
                        Else
                            strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(2)) & "</td>"
                        End If

                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(3)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(4)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(5)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(6)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(7)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(8)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(9)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(10)) & "</td>"
                        strcadenaprint = strcadenaprint & "<td height=25 align=center>" & Trim(objRecordSet.Rows(i).Item(11)) & "</td>"
                        strcadenaprint = strcadenaprint & "</tr>"
                        ' objRecordSet.MoveNext()
                        '  Loop
                    Next
                    'End If
                End If
                objRecordSet = Nothing
                objComponente = Nothing
                'fin cadena print
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
