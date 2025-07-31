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

Public Class SICAR_Reporte_ArqueoCajaExcel
    Inherits SICAR_WebBase

#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblfechahasta As System.Web.UI.WebControls.Label
    Protected WithEvents lblfechadesde As System.Web.UI.WebControls.Label
    Protected WithEvents lblHora As System.Web.UI.WebControls.Label
    Protected WithEvents DgArqueoCaja As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image

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

        Response.Write("<script language='javascript' src='../librerias/Lib_FuncValidacion.js'></script>")
        Response.Write("<script language=javascript>validarUrl('" & ConfigurationSettings.AppSettings("RutaSite") & "');</script>")
        If (Session("USUARIO") Is Nothing) Then
            Dim strRutaSite As String = ConfigurationSettings.AppSettings("RutaSite")
            Response.Redirect(strRutaSite)
            Response.End()
            Exit Sub
        Else


            Dim strfechaDesde As String = Request.QueryString("pfechaDesde")
            Dim strfechaHasta As String = Request.QueryString("pfechaHasta")
            Dim strFecha As String = System.DateTime.Today.ToString
            Dim sFechaActual, sHoraActual, sTipoTienda As String

            sFechaActual = Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000") 'Now.Date 'Session("sFecVenta")
            sHoraActual = TimeOfDay().Hour & ":" & TimeOfDay().Minute
            sTipoTienda = Trim(Session("CANAL"))

      
            strfechaDesde = CInt(Right(strfechaDesde, 2)) & "/" & CInt(Mid(strfechaDesde, 5, 2)) & "/" & CInt(Left(strfechaDesde, 4))
            strfechaHasta = CInt(Right(strfechaHasta, 2)) & "/" & CInt(Mid(strfechaHasta, 5, 2)) & "/" & CInt(Left(strfechaHasta, 4))

            lblfechadesde.Text = strfechaDesde
            lblfechahasta.Text = strfechaHasta
            lblHora.Text = sHoraActual


            If Not Page.IsPostBack Then
                llena_grid(strfechaDesde, strfechaHasta)
            End If
            Response.AddHeader("Content-Disposition", "attachment;filename=reporteArqueoCaja.xls")
            Response.ContentType = "application/vnd.ms-excel"
        End If
    End Sub


    Private Sub llena_grid(ByVal strfechaDesde As String, ByVal strfechaHasta As String)

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

            objGestionaRecaudacionRequest.MessageRequest.Body.fechaInicio = strfechaDesde.ToString()
            objGestionaRecaudacionRequest.MessageRequest.Body.fechaFin = strfechaHasta.ToString()


            objGestionaRecaudacionesResponse = objBWGestionaRecaudacion.ConsultarArqueoCajas(objGestionaRecaudacionRequest, objBEAuditoriaREST)

            strCodigoRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.codigoRespuesta
            strMensajeRespuesta = objGestionaRecaudacionesResponse.MessageResponse.Body.mensajeRespuesta

            If strCodigoRespuesta.Equals("0") Then
                DgArqueoCaja.DataSource = objGestionaRecaudacionesResponse.MessageResponse.Body.listaArqueoCaja
                DgArqueoCaja.DataBind()

            Else
                Response.Write("<script language=jscript> alert('" + "Error en la Consulta, por favor vuelva a interntar por favor" + "'); </script>")

            End If

        Catch ex As Exception
            ex.Message.ToString()
            Response.Write("<script language=jscript> alert('" + ex.Message.ToString() + "'); </script>")

        End Try

    End Sub

End Class
