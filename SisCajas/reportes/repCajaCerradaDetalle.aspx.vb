Imports SisCajas.clsContantes_site
Imports SisCajas.clsLib_Session
Imports SisCajas.clsAudi
Imports SisCajas.GenFunctions
Imports System.Data.OracleClient
Imports COM_SIC_Cajas
Imports System.Globalization


Public Class repCajaCerradaDetalle
    Inherits SICAR_WebBase '''System.Web.UI.Page

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents hdnFecFinal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnFecInicio As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents DgCajaCerrada As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnPuntoDeVenta As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hidIdentificador As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtFecInicio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFecFinal As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnRegresar As System.Web.UI.WebControls.Button


    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region

    Public objComponente, objRecordSet
    Public iAux, sFechaActual, sHoraActual, StrXml
    Public strFecIni, strFecFin, sOficVenta
    Public strcadenaprint As String = ""
    Public strRuta As String

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else

            'strFecIni = Session("fecini")
            'strFecFin = Session("fecfin")

            Dim Vari As String = Request.QueryString().ToString()
            strFecIni = Vari.Substring(4, 2) + "/" + Vari.Substring(9, 2) + "/" + Vari.Substring(14, 4)
            strFecFin = Vari.Substring(27, 2) + "/" + Vari.Substring(32, 2) + "/" + Vari.Substring(37, 4)

            txtFecInicio.Text = strFecIni
            txtFecFinal.Text = strFecFin

            Dim d As String
            Dim m As String
            Dim a As String

            d = txtFecInicio.Text.Substring(0, 2)
            m = txtFecInicio.Text.Substring(3, 2)
            a = txtFecInicio.Text.Substring(6, 4)

            strFecIni = a + m + d

            d = txtFecFinal.Text.Substring(0, 2)
            m = txtFecFinal.Text.Substring(3, 2)
            a = txtFecFinal.Text.Substring(6, 4)

            strFecFin = a + m + d

            If Not Page.IsPostBack Then
                Buscar(strFecIni, strFecFin)
            End If

            If (Len(Session("STRMessage")) > 0) Then
                Response.Write("<script language=JavaScript type='text/javascript'>")
                Response.Write("alert('" & CType(Session("STRMessage"), String).Replace("'", " ") & "');")
                Response.Write("</script>")
            End If
            Session("STRMessage") = ""
        End If
    End Sub


    Private Sub Buscar(ByVal fecini As String, ByVal fecfin As String)

        Dim strOficina As String = Session("ALMACEN")

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
        Dim Detalle(3, 3) As String

        Dim objAudiBus As New COM_SIC_AudiBus.clsAuditoria
        ' fin de variables de auditoria

        'AUDITORIA
        wParam1 = Session("codUsuario")
        wParam2 = Request.ServerVariables("REMOTE_ADDR")
        wParam3 = Request.ServerVariables("SERVER_NAME")
        wParam4 = ConfigurationSettings.AppSettings("gConstOpcPPRP")
        wParam5 = 1
        wParam6 = "Pool de Recaudaciones Procesadas"
        wParam7 = ConfigurationSettings.AppSettings("CodAplicacion")
        wParam8 = ConfigurationSettings.AppSettings("gConstEvtPPrc")
        wParam9 = Session("codPerfil")
        wParam10 = Mid(Request.ServerVariables("Logon_User"), InStr(1, Request.ServerVariables("Logon_User"), "\", vbTextCompare) + 1, 20)
        wParam11 = 1

        Detalle(1, 1) = "Fecha Inicio"
        Detalle(1, 2) = hdnFecInicio.Value
        Detalle(1, 3) = "Fecha Inicio"

        Detalle(1, 1) = "Fecha Final"
        Detalle(1, 2) = hdnFecFinal.Value
        Detalle(1, 3) = "Fecha Final"

        Detalle(2, 1) = "OfVta"
        Detalle(2, 2) = strOficina
        Detalle(2, 3) = "Oficina venta"

        'FIN AUDITORIA
        objFileLog.Log_WriteLog(pathFile, strArchivo, ":.1")

        Try
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim intSAP = objOffline.Get_ConsultaSAP
            Dim objSAPRecau As Object
            'intSAP = 1
            If intSAP = 1 Then
                objSAPRecau = New SAP_SIC_Recaudacion.clsRecaudacion
            Else
                objSAPRecau = New COM_SIC_OffLine.clsOffline
            End If

            '--trae todos, incluidos los anulados
            'nhuaringa auto filtro

            Dim dsResult As DataSet = objSAPRecau.Get_PoolCajaCerrada("", strOficina, fecini, fecfin)
            Me.DgCajaCerrada.DataSource = dsResult.Tables(0)
            Me.DgCajaCerrada.DataBind()

            If dsResult.Tables(0).Rows.Count <= 0 Then
                Response.Write("<script language=jscript> alert('No existe resultado para el rango de fecha mostrado'); </script>")
            End If
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        Catch ex As Exception
            Response.Write("<script language=jscript> alert('" + ex.Message.ToString.Replace("'", " ") + "'); </script>")
            'Response.Write("EFC1:" + ex.Message)
            wParam5 = 0
            wParam6 = "Error en Pool de Cajas Cerradas." & ex.Message
            objAudiBus.AddAuditoria(wParam1, wParam2, wParam3, wParam4, wParam5, wParam6, wParam7, wParam8, wParam9, wParam10, wParam11, Detalle)
        End Try

    End Sub

    Private Sub btnRegresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("repCajaCerrada.aspx")
    End Sub
End Class
