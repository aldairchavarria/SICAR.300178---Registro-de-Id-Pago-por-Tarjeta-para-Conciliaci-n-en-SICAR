Imports SisCajas.GenFunctions
Imports SisCajas.clsAudi
Imports COM_SIC_INActChip
Imports System.IO
Imports COM_SIC_Activaciones
Imports SisCajas.Funciones
Imports SisCajas.clsActivaciones
Imports System.Text
Imports System.Net
Imports System.Globalization
Imports COM_SIC_Cajas
Imports COM_SIC_FacturaElectronica
Imports NEGOCIO_SIC_SANS
Imports System.Xml
Imports System.Configuration
Imports COM_SIC_Procesa_Pagos.GestionaRecaudacionRest

Public Class SICAR_Reporte_ArqueoCaja
    Inherits SICAR_WebBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBuscar As System.Web.UI.WebControls.Button
    Protected WithEvents txtFechaDesde As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtFechaHasta As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents DgArqueoCaja As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Variables"
    '    Public strFecha As String
    Public auxfechaact
    'Introducir aquí el código de usuario para inicializar la página
    Public objComponente, objRecordSet, StrXml
    Public iAux, sFechaActual, sHoraActual, sTipoTienda, sAncho
    Public strFecha, DLin, DCol, sOficVenta, sValorA, sValorB
    Public auxprint As String = ""
    Dim dsCajero As New DataSet
    Dim dsAnulacion As New DataSet

    Public bMostrar As Boolean = False

    Public objFileLog As New SICAR_Log
    Public nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecaudacion")
    Public pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecaudacion")
    Public strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Public Cantidadlista As Integer
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
            Dim codUsuario As String = Session("USUARIO")
            Dim codPerfil As String = Session("PERFIL_SAP")
            strFecha = Request("strFecha")
            Dim strTipoUsuario As String = "C"

            Dim objPoolPagos As New SAP_SIC_Pagos.clsPagos
            auxfechaact = Me.strFecha

            Session("sFecVenta") = strFecha

            objComponente = Nothing
            objRecordSet = Nothing

            sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Now.Date 'Session("sFecVenta")
            sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
            sTipoTienda = Trim(Session("CANAL"))

            objRecordSet = Nothing
            objComponente = Nothing

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

            If Not Page.IsPostBack Then
                txtFechaDesde.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Date.ToString("d")
                txtFechaHasta.Value = Format(Day(Now), "00") & "/" & Format(Month(Now), "00") & "/" & Format(Year(Now), "0000") 'Now.Date.ToString("d")

                LlenaGrillaReporte()
            End If
        End If
    End Sub


    Private Sub LlenaGrillaReporte()

        Try

            Dim objBEAuditoriaREST As New COM_SIC_Procesa_Pagos.DataPowerRest.AuditoriaEWS
            Dim objHeaderRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.HeaderRequest
            Dim objauditRequest As New COM_SIC_Procesa_Pagos.DataPowerRest.BEAuditRequest
            Dim objGestionaRecaudacionRequest As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionRequest
            Dim objGestionaRecaudacionesResponse As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.GestionaRecaudacionesResponse
            Dim objBWGestionaRecaudacion As New COM_SIC_Procesa_Pagos.GestionaRecaudacionRest.BWGestionaRecaudacion
            Dim strCodigoRespuesta As String = String.Empty
            Dim strMensajeRespuesta As String = String.Empty

            objHeaderRequest.consumer = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.country = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_country"))
            objHeaderRequest.dispositivo = ""
            objHeaderRequest.language = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_language"))
            objHeaderRequest.modulo = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.msgType = CheckStr(ConfigurationSettings.AppSettings("DAT_ConsultaNacionalidad_msgType"))
            objHeaderRequest.operation = "consultarArqueoCaja"
            objHeaderRequest.pid = CheckStr(ConfigurationSettings.AppSettings("pid"))
            objHeaderRequest.system = CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objHeaderRequest.timestamp = Convert.ToDateTime(String.Format("{0:u}", DateTime.UtcNow))
            objHeaderRequest.userId = ""
            objHeaderRequest.wsIp = Funciones.CheckStr(ConfigurationSettings.AppSettings("DAT_GestionaRecaudacion_wsIp"))

            objGestionaRecaudacionRequest.MessageRequest.Header.HeaderRequest = objHeaderRequest

            objBEAuditoriaREST.IDTRANSACCION = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            objBEAuditoriaREST.IPAPLICACION = Request.ServerVariables("LOCAL_ADDR")
            objBEAuditoriaREST.idTransaccionNegocio = CurrentTerminal
            objBEAuditoriaREST.USRAPP = CurrentUser
            objBEAuditoriaREST.applicationCodeWS = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.APLICACION = ConfigurationSettings.AppSettings("constAplicacion")
            objBEAuditoriaREST.userId = CurrentUser
            objBEAuditoriaREST.nameRegEdit = ""

            objauditRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
            objauditRequest.ipAplicacion = CurrentTerminal
            objauditRequest.nombreAplicacion = Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion"))
            objauditRequest.usuarioAplicacion = CurrentUser

            objGestionaRecaudacionRequest.MessageRequest.Body.fechaInicio = txtFechaDesde.Value
            objGestionaRecaudacionRequest.MessageRequest.Body.fechaFin = txtFechaHasta.Value


            objGestionaRecaudacionesResponse = objBWGestionaRecaudacion.ConsultarArqueoCajas(objGestionaRecaudacionRequest, objBEAuditoriaREST)
          
            strCodigoRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            strMensajeRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            If strCodigoRespuesta.Equals("0") Then

                DgArqueoCaja.DataSource = objGestionaRecaudacionesResponse.MessageResponse.Body.listaArqueoCaja
                DgArqueoCaja.DataBind()
                Cantidadlista = DgArqueoCaja.Items.Count()

                Dim descTrans As String
                descTrans = "Consulta de Arqueo de Caja desde : " & txtFechaDesde.Value & " - Hasta : " & txtFechaHasta.Value & " Resultado : " & Cantidadlista
                RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsAuditoriaReporteArqueoCaja"), descTrans)

            Else
                Cantidadlista = 0
                Dim descTrans As String
                descTrans = "Error en Consultar Arqueo de Caja desde : " & txtFechaDesde.Value & " - Hasta : " & txtFechaHasta.Value & " Resultado : " & Cantidadlista
                RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsAuditoriaReporteArqueoCaja"), descTrans)

                Response.Write("<script language=jscript> alert('" + "Error en la Consulta, por favor vuelva a interntar por favor" + "'); </script>")

            End If

        Catch ex As Exception
            ex.Message.ToString()

            Cantidadlista = 0
            Dim descTrans As String
            descTrans = "Error en Consultar Arqueo de Caja desde : " & txtFechaDesde.Value & " - Hasta : " & txtFechaHasta.Value & " Resultado : " & ex.Message.ToString()
            RegistrarAuditoria(ConfigurationSettings.AppSettings("codTrsAuditoriaReporteArqueoCaja"), descTrans)

            Response.Write("<script language=jscript> alert('" + ex.Message.ToString() + "'); </script>")

        End Try

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        LlenaGrillaReporte()
   
    End Sub


    Private Sub RegistrarAuditoria(ByVal strCodTrans As String, ByVal descTrans As String)
        Try
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim usuario_id As String = CurrentUser
            Dim ipcliente As String = CurrentTerminal
            Dim strMensaje As String

            Dim strCodServ As String = ConfigurationSettings.AppSettings("CONS_COD_SICAR_SERV")
            Dim objAuditoriaWS As New COM_SIC_Activaciones.clsAuditoriaWS
            Dim auditoriaGrabado As Boolean

            auditoriaGrabado = objAuditoriaWS.RegistrarAuditoria(strCodTrans, strCodServ, ipcliente, nombreHost, ipServer, nombreServer, usuario_id, "", "0", descTrans)

        Catch ex As Exception

        End Try
    End Sub


End Class
