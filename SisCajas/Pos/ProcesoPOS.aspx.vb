Imports Thycotic.Web.RemoteScripting
Imports SisCajas.clsActivaciones
Imports System.Globalization 'INICIATIVA - 529
Public Class ProcesoPOS
    Inherits RSPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim objFileLog As New SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub
  Private Function Devolver_TipoPago(ByVal CodTipPago As String) As String
    Dim strDescripcion As String = ""
    Select Case CodTipPago
      Case "01"
        strDescripcion = "Ventas Rapidas"
      Case "02"
        strDescripcion = "Recarga Virtual Frecuente"
      Case "03"
        strDescripcion = "Documentos por pagar"
      Case "04"
        strDescripcion = "Pago de Recibos"
      Case "05"
        strDescripcion = "Recaudaciones a DAC"
      Case "06"
        strDescripcion = "Recaudación Clientes Corporativos"
      Case "07"
        strDescripcion = "Pago adelantado Postpago"
      Case "08"
        strDescripcion = "Pago Clientes Castigados"
      Case "09"
        strDescripcion = "Pago Recarga DTH"
      Case "10"
        strDescripcion = "Recaudación Clientes Fijos y Paginas"
      Case "11"
        strDescripcion = "Documentos Pagados"
      Case "12"
        strDescripcion = "Recaudaciones Procesadas"
      Case "13"
        strDescripcion = "Devoluciones"
      Case "14"
        strDescripcion = "Operaciones no financiera"
            Case "15"
                strDescripcion = "Recaudacion por DNI"
      Case Else
        strDescripcion = "Documentos por pagar"

    End Select
    Return strDescripcion

  End Function
  <RemoteScriptingMethod(Description:="POS")> _
  Public Function ObtenerTipoPos(ByVal CodTarjeta As String, ByVal CodOficina As String) As String
    'Try
    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "Inicio : " & CodTarjeta)

    Dim strCodRpt As String = ""
    Dim strMsgRpt As String = ""
    Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
    Dim strResult As String = objSicarDB.Obtener_TipoPOS(CodTarjeta, CodOficina, strCodRpt, strMsgRpt)

    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "CodTarjeta : " & CodTarjeta)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "CodOficina : " & CodOficina)

    ObtenerTipoPos = "<SELECT>" & strCodRpt & "|" & strResult & "</SELECT>"

    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "strCodRpt : " & strCodRpt)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "strMsgRpt : " & strMsgRpt)

    objFileLog.Log_WriteLog(pathFile, strArchivo, "ObtenerTipoPos : " & "Fin : " & CodTarjeta)

    'Catch ex As Exception
    '    Return "1|" & ex.Message
    '    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransaction ERROR : " & ex.Message.ToString)
    'End Try
  End Function

  <RemoteScriptingMethod(Description:="POS")> _
  Public Function GuardarAutorizacion(ByVal strTrama As String) As String
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "Inicio : " & strTrama)

    Dim strCodOficina As String = Session("ALMACEN")
    Dim objConf As New COM_SIC_Configura.clsConfigura
    Dim intAutoriza As Integer
    Dim strTipoTienda As String = Funciones.CheckStr(Session("CANAL"))
    Dim strNomVendedor As String = ""
    Dim strCodUsuario As String = Funciones.CheckStr(Session("USUARIO"))
    Dim i As Integer
    Dim strAplicacion As String = Funciones.CheckStr(ConfigurationSettings.AppSettings("codAplicacion"))
    Dim strNomCompleto As String = Funciones.CheckStr(Session("NOMBRE_COMPLETO"))

    Dim strTramaArrys As String() = strTrama.Split("|")
    Dim strNomCliente As String = strTramaArrys(0).Substring(strTramaArrys(0).IndexOf("=") + 1)
    Dim strNroTelefono As String = strTramaArrys(1).Substring(strTramaArrys(1).IndexOf("=") + 1)
    Dim strNroPedido As String = strTramaArrys(2).Substring(strTramaArrys(2).IndexOf("=") + 1)
    Dim intIdTransa As Int64 = Funciones.CheckInt64(strTramaArrys(3).Substring(strTramaArrys(3).IndexOf("=") + 1))
    Dim dblMonto As Double = Funciones.CheckDbl(strTramaArrys(4).Substring(strTramaArrys(4).IndexOf("=") + 1))

    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strCodOficina : " & strCodOficina)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strTipoTienda : " & strTipoTienda)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strCodUsuario : " & strCodUsuario)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strNomVendedor : " & strNomVendedor)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strAplicacion : " & strAplicacion)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strNomCompleto : " & strNomCompleto)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strNomCliente : " & strNomCliente)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strNroTelefono : " & strNroTelefono)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "strNroPedido : " & strNroPedido)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "Cod. Permiso x Transaccion: " & ClsKeyPOS.strCodPermisoAnuPOS.ToString())
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "intIdTransa : " & intIdTransa.ToString)
    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "dblMonto : " & dblMonto.ToString)

        intAutoriza = objConf.FP_Inserta_Aut_Transac(strTipoTienda, strCodOficina, strAplicacion, strCodUsuario, strNomCompleto, "", "", strNomCliente, strNroTelefono, "", strNroPedido, 0, ClsKeyPOS.strCodPermisoAnuPOS, 0, 0, 0, 0, 0, 0, "", strNomVendedor, dblMonto)

    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "intAutoriza : " & intAutoriza.ToString)

    GuardarAutorizacion = "<SELECT>" & intAutoriza.ToString & "</SELECT>"

    objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarAutorizacion : " & "Fin : " & strTrama)
  End Function
    <RemoteScriptingMethod(Description:="POS")> _
        Public Function GuardarTransaction(ByVal strTrama As String, ByVal NroTelefono As String, _
            ByVal NroPedido As String) As String

        Dim strPedidoLog As String = "Pedido: [" & Funciones.CheckStr(NroPedido) & "] "
        Dim strTipoPago As String = ""

        Try
            Dim strTramaArrys As String() = strTrama.Split("|")
            strTipoPago = Funciones.CheckStr(strTramaArrys(6).Substring(strTramaArrys(6).IndexOf("=") + 1))

            If NroPedido = String.Empty Then
                strPedidoLog = Me.Devolver_TipoPago(strTipoPago) & ": [" & Funciones.CheckStr(NroTelefono) & "] "
            Else
                strPedidoLog = Me.Devolver_TipoPago(strTipoPago) & ": [" & Funciones.CheckStr(NroPedido) & "] "
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "Inicio : " & strTrama)

            Dim strCodRpt As String = ""
            Dim strMsgRpt As String = ""
            Dim strIdTransation As String = NroTelefono & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff")
            Dim strNombreAplicacion As String = ""
            Dim strUsuAplicacion As String = ""
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim ipcliente As String = Funciones.CheckStr(Request.ServerVariables("REMOTE_HOST"))

            Dim strTipoTran As String = Funciones.CheckStr(strTramaArrys(9).Substring(strTramaArrys(9).IndexOf("=") + 1))

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS

            Dim strNroTienda As String = strTramaArrys(12).Substring(strTramaArrys(12).IndexOf("=") + 1)
            Dim strNroCaja As String = strTramaArrys(13).Substring(strTramaArrys(13).IndexOf("=") + 1)
            Dim strCodEstab As String = strTramaArrys(14).Substring(strTramaArrys(14).IndexOf("=") + 1)
            Dim strNumSeriePos As String = strTramaArrys(16).Substring(strTramaArrys(16).IndexOf("=") + 1)
            Dim strNomEquiPos As String = strTramaArrys(15).Substring(strTramaArrys(15).IndexOf("=") + 1)
            Dim strIpCaja As String = strTramaArrys(10).Substring(strTramaArrys(10).IndexOf("=") + 1)
            Dim strNroRegistro As String = strTramaArrys(11).Substring(strTramaArrys(11).IndexOf("=") + 1)




           
            objEntity.idCabecera = strTramaArrys(17).Substring(strTramaArrys(17).IndexOf("=") + 1)
            objEntity.codVenta = Funciones.CheckStr(Session("ALMACEN"))
            objEntity.nroTienda = strNroTienda
            objEntity.nroCaja = strNroCaja
            objEntity.nroReferencia = ""
            objEntity.nroAprobacion = ""
            objEntity.codOperacion = strTramaArrys(0).Substring(strTramaArrys(0).IndexOf("=") + 1)
            objEntity.desOperacion = strTramaArrys(1).Substring(strTramaArrys(1).IndexOf("=") + 1)
            objEntity.tipoOperacion = strTramaArrys(2).Substring(strTramaArrys(2).IndexOf("=") + 1)
            objEntity.montoOperacion = strTramaArrys(3).Substring(strTramaArrys(3).IndexOf("=") + 1)
            objEntity.monedaOperacion = strTramaArrys(4).Substring(strTramaArrys(4).IndexOf("=") + 1)
            objEntity.fechaTransaccion = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            objEntity.nroTarjeta = strTramaArrys(18).Substring(strTramaArrys(18).IndexOf("=") + 1)
            objEntity.fecExpiracion = ""
            objEntity.codCajero = Funciones.CheckStr(Session("USUARIO"))
            If (strTipoTran = "2") Then
                objEntity.codAnulador = Funciones.CheckStr(Session("USUARIO"))
            Else
                objEntity.codAnulador = ""
            End If
            objEntity.flagAnulacion = ""
            objEntity.nombreCliente = ""
            objEntity.idAnulacion = ""
            objEntity.obsAnulacion = ""
            objEntity.idTransaccionPos = ""

            objEntity.codEstablecimiento = strCodEstab
            objEntity.tipoTarjeta = strTramaArrys(5).Substring(strTramaArrys(5).IndexOf("=") + 1)
            objEntity.ipCliente = Funciones.CheckStr(ipcliente)
            objEntity.ipServidor = Funciones.CheckStr(ipServer)
            objEntity.nombrePcCliente = Funciones.CheckStr(nombreHost)
            objEntity.nombrePcServidor = Funciones.CheckStr(nombreServer)
            objEntity.impresionVoucher = ""
            objEntity.usuarioRed = Funciones.CheckStr(Me.CurrentUser())
            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            objEntity.tipoPago = strTramaArrys(6).Substring(strTramaArrys(6).IndexOf("=") + 1)
            objEntity.numPedido = Funciones.CheckStr(NroPedido)
            objEntity.estadoTransaccion = strTramaArrys(7).Substring(strTramaArrys(7).IndexOf("=") + 1)
            objEntity.codRespTransaccion = ""
            objEntity.codAprobTransaccion = ""
            objEntity.descTransaccion = ""
            objEntity.numVoucher = strTramaArrys(19).Substring(strTramaArrys(19).IndexOf("=") + 1)
            objEntity.numSeriePos = strNumSeriePos
            objEntity.nombreEquipoPos = strNomEquiPos
            objEntity.numTransaccion = ""
            objEntity.tipoPos = strTramaArrys(8).Substring(strTramaArrys(8).IndexOf("=") + 1)
            objEntity.tipoTransaccion = strTramaArrys(9).Substring(strTramaArrys(9).IndexOf("=") + 1)
            objEntity.fechaTransaccionPos = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            objEntity.horaTransaccionPos = String.Format("{0:HH:mm:ss}", DateTime.Now)
            objEntity.ipCaja = strIpCaja
            objEntity.nroRegistro = strNroRegistro
            objEntity.idTransaccionPos = ""


            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strIdTransation : " & strIdTransation)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombreHost : " & nombreHost)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombreServer : " & nombreServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ipServer : " & ipServer)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "hostInfo : " & Funciones.CheckStr(hostInfo))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ipcliente : " & ipcliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strNroTienda : " & strNroTienda)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strNroCaja : " & strNroCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strNumSeriePos : " & strNumSeriePos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strNomEquiPos : " & strNomEquiPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strIpCaja : " & strIpCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strNroRegistro : " & strNroRegistro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strTipoTran : " & strTipoTran)



            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codVenta : " & objEntity.codVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroTienda : " & objEntity.nroTienda)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroCaja : " & objEntity.nroCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroReferencia : " & objEntity.nroReferencia)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroAprobacion : " & objEntity.nroAprobacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codOperacion : " & objEntity.codOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "desOperacion : " & objEntity.desOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "tipoOperacion : " & objEntity.tipoOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "montoOperacion : " & objEntity.montoOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "monedaOperacion : " & objEntity.monedaOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "fechaTransaccion : " & objEntity.fechaTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroTarjeta : " & objEntity.nroTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "fecExpiracion : " & objEntity.fecExpiracion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codCajero : " & objEntity.codCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codAnulador : " & objEntity.codAnulador)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "flagAnulacion : " & objEntity.flagAnulacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombreCliente : " & objEntity.nombreCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "idAnulacion : " & objEntity.idAnulacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codEstablecimiento : " & objEntity.codEstablecimiento)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "tipoTarjeta : " & objEntity.tipoTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ipCliente : " & objEntity.ipCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ipServidor : " & objEntity.ipServidor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombrePcCliente : " & objEntity.nombrePcCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombrePcServidor : " & objEntity.nombrePcServidor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "impresionVoucher : " & objEntity.impresionVoucher)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "usuarioRed : " & objEntity.usuarioRed)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "UserAplicacion : " & objEntity.UserAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "tipoPago : " & objEntity.tipoPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "numPedido : " & objEntity.numPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "estadoTransaccion : " & objEntity.estadoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codRespTransaccion : " & objEntity.codRespTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "codAprobTransaccion : " & objEntity.codAprobTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "descTransaccion : " & objEntity.descTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "numVoucher : " & objEntity.numVoucher)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "numSeriePos : " & objEntity.numSeriePos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nombreEquipoPos : " & objEntity.nombreEquipoPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "numTransaccion : " & objEntity.numTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "tipoPos : " & objEntity.tipoPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "tipoTransaccion : " & objEntity.tipoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "fechaTransaccionPos : " & objEntity.fechaTransaccionPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "horaTransaccionPos : " & objEntity.horaTransaccionPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ipCaja : " & objEntity.ipCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "nroRegistro : " & objEntity.nroRegistro)

            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
            objSicarDB.RegistrarTransaction(objEntity, strCodRpt, strMsgRpt)


            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strCodRpt : " & strCodRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "strMsgRpt : " & strMsgRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "Fin : ")

            If strCodRpt <> "0" Then

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS

                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Error en el Servicio " & "strCodRpt : " & strCodRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Error en el Servicio " & "strMsgRpt : " & strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Ejecutando Contingencia - Inicio")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASI_TRANS_POS_CAB")
                objTransascPos.GuardarTransacPOS(objEntity, strCodRpt, strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction - Ejecutando Contingencia - Fin")

            End If


            GuardarTransaction = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "GuardarTransaction : " & "ERROR : " & ex.Message)
            Return "1|" & ex.Message
        End Try
    End Function

    Private ReadOnly Property CurrentUser() As String
        Get
            Dim domainUser As String = Request.ServerVariables("LOGON_USER")
            'Dim usuarioLogin As String = Trim(Mid(domainUser, InStr(1, domainUser, "\", vbTextCompare) + 1, 20))
            Dim usuarioLogin As String = domainUser.Substring(domainUser.IndexOf("\") + 1)
            If usuarioLogin Is Nothing Then usuarioLogin = ""
            Return usuarioLogin.Trim().ToUpper()
        End Get
    End Property

    <RemoteScriptingMethod(Description:="POS")> _
    Public Function ActualizarTransaction(ByVal strTrama As String, ByVal TrasnId As String) As String
        Dim strPedidoLog As String = "TrasnId: [" & Funciones.CheckStr(TrasnId) & "] "
        Dim strTipoPago As String = ""
        Dim strResIdViaPago As String = ""
        Try

            objFileLog.Log_WriteLog(pathFile, strArchivo, "ActualizarTransaction : " & "Inicio : " & strTrama)

            Dim strCodRpt As String = ""
            Dim strMsgRpt As String = ""

            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            Dim hostInfo As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(nombreHost)
            Dim ipcliente As String = Funciones.CheckStr(Request.ServerVariables("REMOTE_HOST"))

            Dim strTramaArrys As String() = strTrama.Split("|")
            strTipoPago = Funciones.CheckStr(strTramaArrys(26).Substring(strTramaArrys(26).IndexOf("=") + 1))
            Dim strNroPedido As String = strTramaArrys(22).Substring(strTramaArrys(22).IndexOf("=") + 1)

            If strNroPedido = String.Empty Then
                strPedidoLog = Me.Devolver_TipoPago(strTipoPago) & ": [" & Funciones.CheckStr(TrasnId) & "] "
            Else
                strPedidoLog = Me.Devolver_TipoPago(strTipoPago) & ": [" & Funciones.CheckStr(strNroPedido) & "] "
            End If



            Dim strNroRegistro As String = strTramaArrys(2).Substring(strTramaArrys(2).IndexOf("=") + 1)
            Dim strNumSeriePos As String = strTramaArrys(13).Substring(strTramaArrys(13).IndexOf("=") + 1)
            Dim strNomEquiPos As String = strTramaArrys(14).Substring(strTramaArrys(14).IndexOf("=") + 1)
            Dim strNroTienda As String = strTramaArrys(16).Substring(strTramaArrys(16).IndexOf("=") + 1)
            Dim strNroCaja As String = strTramaArrys(17).Substring(strTramaArrys(17).IndexOf("=") + 1)
            Dim strCodEstab As String = strTramaArrys(18).Substring(strTramaArrys(18).IndexOf("=") + 1)
            Dim strIpCaja As String = strTramaArrys(19).Substring(strTramaArrys(19).IndexOf("=") + 1)
            Dim strIdCabez As String = strTramaArrys(20).Substring(strTramaArrys(20).IndexOf("=") + 1)
            Dim strFlagPago As String = strTramaArrys(21).Substring(strTramaArrys(21).IndexOf("=") + 1)
            Dim strIdUnicoTra As String = strTramaArrys(23).Substring(strTramaArrys(23).IndexOf("=") + 1)
            Dim strTipoTrans As String = strTramaArrys(24).Substring(strTramaArrys(24).IndexOf("=") + 1)
            Dim strIdRefAnu As String = strTramaArrys(25).Substring(strTramaArrys(25).IndexOf("=") + 1)
            Dim strResTipoTarjeta As String = strTramaArrys(27).Substring(strTramaArrys(27).IndexOf("=") + 1)
            Dim strCCINS As String = ""
            Dim strCCINSCbo As String = ""



            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            objEntity.FlagPago = strFlagPago
            objEntity.idCabecera = strIdCabez
            objEntity.numPedido = strNroPedido
            objEntity.estadoTransaccion = strTramaArrys(15).Substring(strTramaArrys(15).IndexOf("=") + 1)
            objEntity.IdTransPos = strIdUnicoTra

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "idCabecera : " & strIdCabez)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "strTipoPago : " & strTipoPago)


            objEntity.TransId = Funciones.CheckStr(TrasnId)
            objEntity.monedaOperacion = strTramaArrys(0).Substring(strTramaArrys(0).IndexOf("=") + 1)
            objEntity.montoOperacion = strTramaArrys(1).Substring(strTramaArrys(1).IndexOf("=") + 1)
            objEntity.nroRegistro = strNroRegistro
            objEntity.numVoucher = strTramaArrys(3).Substring(strTramaArrys(3).IndexOf("=") + 1)
            objEntity.numTransaccion = strTramaArrys(4).Substring(strTramaArrys(4).IndexOf("=") + 1)
            objEntity.codRespTransaccion = strTramaArrys(5).Substring(strTramaArrys(5).IndexOf("=") + 1)
            objEntity.descTransaccion = strTramaArrys(6).Substring(strTramaArrys(6).IndexOf("=") + 1)
            objEntity.codAprobTransaccion = strTramaArrys(7).Substring(strTramaArrys(7).IndexOf("=") + 1)
            objEntity.tipoPos = strTramaArrys(8).Substring(strTramaArrys(8).IndexOf("=") + 1)
            objEntity.nroTarjeta = strTramaArrys(9).Substring(strTramaArrys(9).IndexOf("=") + 1)
            objEntity.fechaTransaccionPos = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            objEntity.horaTransaccionPos = String.Format("{0:HH:mm:ss}", DateTime.Now)
            objEntity.fecExpiracion = Funciones.CheckStr(strTramaArrys(10).Substring(strTramaArrys(10).IndexOf("=") + 1)).Replace("/", "").Trim()

            objEntity.nombreCliente = strTramaArrys(11).Substring(strTramaArrys(11).IndexOf("=") + 1)
            objEntity.impresionVoucher = strTramaArrys(12).Substring(strTramaArrys(12).IndexOf("=") + 1)
            objEntity.numSeriePos = strNumSeriePos
            objEntity.nombreEquipoPos = strNomEquiPos


            objEntity.ipCliente = Funciones.CheckStr(ipcliente)
            objEntity.ipServidor = Funciones.CheckStr(ipServer)
            objEntity.nombrePcCliente = Funciones.CheckStr(nombreHost)
            objEntity.nombrePcServidor = Funciones.CheckStr(nombreServer)
            objEntity.usuarioRed = Funciones.CheckStr(Me.CurrentUser())
            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            objEntity.codCajero = Funciones.CheckStr(Session("USUARIO"))

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "strTipoTrans : " & Funciones.CheckStr(strTipoTrans))
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "strIdRefAnu : " & Funciones.CheckStr(strIdRefAnu))

            If strTipoTrans = "2" Then
                objEntity.codAnulador = objEntity.codCajero : objEntity.IdRefAnu = strIdRefAnu
            Else
                objEntity.codAnulador = "" : objEntity.IdRefAnu = ""
            End If

            objEntity.idTransaccionPos = strTramaArrys(23).Substring(strTramaArrys(23).IndexOf("=") + 1)

            'CARGANDO CODIGOS DE TIPOS DE TARJETAS DEL POS'
            'PROY-31949 - INICIO

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "Validar Respuesta Tipo Tarjeta POS - Inicio ")

            If strTipoTrans = ClsKeyPOS.strTipoTransPAG And objEntity.estadoTransaccion = ClsKeyPOS.strEstTRanAce Then

                Dim objConsultasTarjetaPos As New COM_SIC_Activaciones.clsTarjetasPOS
                Dim dtViasPagoPOS As New DataTable
                Dim dtViaPago As New DataTable
                Dim strTipTarjeta As String
                Dim strCodRpta As String
                Dim strMsgRpta As String
                Dim strCodRespuesta As String
                dtViasPagoPOS = objConsultasTarjetaPos.CodigosTarjetaPos("", "", "", strCodRespuesta, strMsgRpta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarFormaPagoPos : " & "strCodRespuesta : " & Funciones.CheckStr(strCodRespuesta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarFormaPagoPos : " & "strMsgRpta : " & Funciones.CheckStr(strMsgRpta))
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarFormaPagoPos : " & "dtViasPagoPOS.count: " & dtViasPagoPOS.Rows.Count)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarFormaPagoPos : " & "Respuesta POS: " & strResTipoTarjeta)

                For i As Int32 = 0 To dtViasPagoPOS.Rows.Count - 1
                    If dtViasPagoPOS.Rows(i).Item("COTAV_ARD_ID") = strResTipoTarjeta Then
                        strCCINS = dtViasPagoPOS.Rows(i).Item("CCINS")
                        Exit For
                    End If
                Next

                If strCCINS = "" Then

                    strResIdViaPago = "2|" & ClsKeyPOS.strMsjErrorTipTarjeta
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarFormaPagoPos : " & "strMsjErrorTipTarjeta:" & ClsKeyPOS.strMsjErrorTipTarjeta)

                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strCCINS:" & strCCINS)
                    dtViaPago = objConsultasTarjetaPos.ConsultarViasPagoPos(strCCINS, strCodRpta, strMsgRpta)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strMsgRpta:" & strMsgRpta)

                    If dtViaPago.Rows.Count > 0 Then

                        strTipTarjeta = dtViaPago.Rows(0).Item("TIP_TARJETA")
                        strCCINSCbo = dtViaPago.Rows(0).Item("CCINS")

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strTipTarjeta:" & strTipTarjeta)
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strTipTarjeta:" & strCCINSCbo)

                        Select Case strTipTarjeta
                            Case "AMX"
                                objEntity.tipoPos = ClsKeyPOS.strTipoPosAM
                            Case "MCD"
                                objEntity.tipoPos = ClsKeyPOS.strTipoPosMC
                            Case "VIS"
                                objEntity.tipoPos = ClsKeyPOS.strTipoPosVI
                            Case "DIN"
                                objEntity.tipoPos = ClsKeyPOS.strTipoPosDI
                        End Select

                        objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strTipTarjeta:" & strCCINSCbo)
                        strResIdViaPago = "1|" & strCCINSCbo & ";" & strTipTarjeta
                    Else
                        strResIdViaPago = "2|" & ClsKeyPOS.strMsjErrorTipTarjeta
                        objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strMsjErrorTipTarjeta:" & ClsKeyPOS.strMsjErrorTipTarjeta)

                    End If


                End If

            Else
                strResIdViaPago = "0|OK"
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - ConsultarViasPagoPos : " & "strResIdViaPago: " & strResIdViaPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "Validar Respuesta Tipo Tarjeta POS - FIN ")
            'PROY-31949 - FIN
            'INI CONSULTA INTEGRACION AUTOMATICO POS


            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "TransId : " & objEntity.TransId)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "monedaOperacion : " & objEntity.monedaOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "montoOperacion : " & objEntity.montoOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "nroRegistro : " & objEntity.nroRegistro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "numVoucher : " & objEntity.numVoucher)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "numTransaccion : " & objEntity.numTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "codRespTransaccion : " & objEntity.codRespTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "descTransaccion : " & objEntity.descTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "codAprobTransaccion : " & objEntity.codAprobTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "tipoPos : " & objEntity.tipoPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "nroTarjeta : " & objEntity.nroTarjeta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "fechaTransaccionPos : " & objEntity.fechaTransaccionPos)

            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
            objSicarDB.ActualizarTransaction(objEntity, strCodRpt, strMsgRpt)


            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "estadoTransaccion : " & objEntity.estadoTransaccion)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "horaTransaccionPos : " & objEntity.horaTransaccionPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "ipCliente : " & objEntity.ipCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "ipServidor : " & objEntity.ipServidor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "nombrePcCliente : " & objEntity.nombrePcCliente)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "nombrePcServidor : " & objEntity.nombrePcServidor)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "usuarioRed : " & objEntity.usuarioRed)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "UserAplicacion : " & objEntity.UserAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "codCajero : " & objEntity.codCajero)

            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "strCodRpt : " & strCodRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction : " & "strMsgRpt : " & strMsgRpt)

            If strCodRpt <> "0" Then

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS

                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strCodRpt : " & strCodRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Error en el Servicio " & "strMsgRpt : " & strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Inicio")
                objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - FLAG PAGO " & objEntity.FlagPago)

                If (objEntity.FlagPago = 1) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASU_TRANSPOS")
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASU_ESTTRANS")
                    objTransascPos.UpdateDetTransaccPOS(objEntity, strCodRpt, strMsgRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Fin")
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASU_TRANSPOS")
                    objTransascPos.UpdateCabTransaccPOS(objEntity, strCodRpt, strMsgRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, strPedidoLog & "ActualizarTransaction - Ejecutando Contingencia - Fin")
                End If
            End If

            ActualizarTransaction = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "|" & strResIdViaPago & "</SELECT>"

        Catch ex As Exception
            Return "1|" & ex.Message & "|0|0"
            objFileLog.Log_WriteLog(pathFile, strArchivo, "ActualizarTransaction  ERROR: " & ex.Message.ToString)
        End Try
    End Function
    <RemoteScriptingMethod(Description:="CargarFormasPago")> _
  Public Function CargarFormasPago(ByVal NroPedido As String, ByVal TipoPago As String) As String

        Dim dsPagos As DataSet
        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim obPagos As New COM_SIC_Activaciones.clsConsultaMsSap
        Dim FILA As String = ""
        Dim CELDA As String = ""
        Dim codRespuesta As String = ""
        Dim msjRespuesta As String = ""
        Try

            Dim dtDetalle As DataTable = obPagos.ConsultarFormasPago(NroPedido)
            Dim dtPagosPOS As DataTable
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago - " & "Inicio : " & NroPedido)

            dsPagos = objConsultaPos.ObtenerFormasDePagoTrans(NroPedido, "", "", TipoPago, codRespuesta, msjRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago - " & "Count : " & Funciones.CheckStr(dsPagos.Tables(0).Rows.Count))
            Dim FormaPagoSap As String
            Dim DescPagoSap As String
            Dim ImporteSap As String
            Dim TipoTarjetaSAP As String
            Dim TipoTarjetaPOS As String
            Dim strCodRptTipPos As String
            Dim strMsgRptTipPos As String
            Dim PagoPosContingencias As String
            Dim objSicarDB As New COM_SIC_Activaciones.clsTransaccionPOS
            dtPagosPOS = dsPagos.Tables(0)

            For Each item As DataRow In dtDetalle.Rows
                PagoPosContingencias = "0"
                FormaPagoSap = Funciones.CheckStr(item("DEPAV_FORMAPAGO"))
                ImporteSap = Funciones.CheckStr(item("DEPAN_IMPORTE"))
                DescPagoSap = Funciones.CheckStr(item("DEPAV_DESCPAGO"))
                Dim strResult As String = objSicarDB.Obtener_TipoPOS(FormaPagoSap, Session("ALMACEN"), strCodRptTipPos, strMsgRptTipPos)
                If strResult.Length > 0 Then
                    TipoTarjetaSAP = strResult.Split("#")(0)
                Else
                    TipoTarjetaSAP = ""
                End If

                If TipoTarjetaSAP <> "" Then
                    Select Case TipoTarjetaSAP
                        Case "VIS"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosVI
                        Case "MCD"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosMC
                        Case "AMX"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosAM
                        Case "DIN"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosDI
                    End Select

                    For i As Integer = 0 To dtPagosPOS.Rows.Count - 1
                        Dim FORMA_PAGO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")))
                        Dim MONTO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONTO")), " ", dtPagosPOS.Rows(i).Item("MONTO")))

                        If FORMA_PAGO = TipoTarjetaPOS And MONTO = ImporteSap Then
                            FORMA_PAGO = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("FORMA_PAGO")), " ", dtPagosPOS.Rows(i).Item("FORMA_PAGO")))
                            Dim TIPO_TARJETA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA")))
                            Dim NRO_TARJETA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NRO_TARJETA")), " ", dtPagosPOS.Rows(i).Item("NRO_TARJETA")))
                            MONTO = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONTO")), " ", dtPagosPOS.Rows(i).Item("MONTO")))
                            Dim ESTADO_ANULACION As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("ESTADO_ANULACION")), " ", dtPagosPOS.Rows(i).Item("ESTADO_ANULACION")))
                            Dim RESULTADO_PROCESO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("RESULTADO_PROCESO")), " ", dtPagosPOS.Rows(i).Item("RESULTADO_PROCESO")))
                            Dim IP_CAJA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("POSV_IPCAJA")), " ", dtPagosPOS.Rows(i).Item("POSV_IPCAJA")))
                Dim BTN_ELIMINAR As String = "OK"
                Dim BTN_PRINT As String = "OK"

                'DATOS ENVIO POST'

                            Dim NUMERO_INTENTOS As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NUMERO_INTENTOS")), " ", dtPagosPOS.Rows(i).Item("NUMERO_INTENTOS")))
                            Dim OFICINA_VENTA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("OFICINA_VENTA")), " ", dtPagosPOS.Rows(i).Item("OFICINA_VENTA")))
                            Dim CAJERO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("CAJERO")), " ", dtPagosPOS.Rows(i).Item("CAJERO")))
                            Dim MONEDA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONEDA")), " ", dtPagosPOS.Rows(i).Item("MONEDA")))
                            Dim NUMERO_TELEFONO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NUMERO_TELEFONO")), " ", dtPagosPOS.Rows(i).Item("NUMERO_TELEFONO")))
                            Dim TIPO_TARJETA_POS As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")))
                            Dim ID_REF As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TRNSV_ID_REF")), " ", dtPagosPOS.Rows(i).Item("TRNSV_ID_REF")))
                            Dim TRANSACCION_ID_CAB As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TRNSN_ID_CAB")), " ", dtPagosPOS.Rows(i).Item("TRNSN_ID_CAB")))

                CELDA = FORMA_PAGO & "|" & _
                        TIPO_TARJETA & "|" & _
                        NRO_TARJETA & "|" & _
                        MONTO & "|" & _
                        ESTADO_ANULACION & "|" & _
                        RESULTADO_PROCESO & "|" & _
                        BTN_ELIMINAR & "|" & _
                        BTN_PRINT & "|" & _
                        NUMERO_INTENTOS & "|" & _
                        OFICINA_VENTA & "|" & _
                        CAJERO & "|" & _
                        MONEDA & "|" & _
                        NUMERO_TELEFONO & "|" & _
                        TIPO_TARJETA_POS & "|" & _
                        ID_REF & "|" & _
                        TRANSACCION_ID_CAB & "|" & _
                        IP_CAJA
                FILA = FILA & CELDA & ";"

                            dtPagosPOS.Rows.RemoveAt(i)
                            PagoPosContingencias = "1"
                            Exit For
                        End If

                    Next

                    If PagoPosContingencias = "0" Then
                        Dim FORMA_PAGO As String = Funciones.CheckStr(IIf(IsDBNull(item("DEPAV_DESCPAGO")), " ", item("DEPAV_DESCPAGO")))
                        Dim TIPO_TARJETA As String = ""
                        Dim NRO_TARJETA As String = ""
                        Dim MONTO As String = Funciones.CheckStr(IIf(IsDBNull(item("DEPAN_IMPORTE")), " ", item("DEPAN_IMPORTE")))
                        Dim ESTADO_ANULACION As String = ""
                        Dim RESULTADO_PROCESO As String = ""
                        Dim IP_CAJA As String = ""
                        Dim BTN_ELIMINAR As String = "OK"
                        Dim BTN_PRINT As String = "OK"

                        'DATOS ENVIO POST'

                        Dim NUMERO_INTENTOS As String = ""
                        Dim OFICINA_VENTA As String = ""
                        Dim CAJERO As String = ""
                        Dim MONEDA As String = ""
                        Dim NUMERO_TELEFONO As String = ""
                        Dim TIPO_TARJETA_POS As String = ""
                        Dim ID_REF As String = ""
                        Dim TRANSACCION_ID_CAB As String = ""

                        CELDA = FORMA_PAGO & "|" & _
                                TIPO_TARJETA & "|" & _
                                NRO_TARJETA & "|" & _
                                MONTO & "|" & _
                                ESTADO_ANULACION & "|" & _
                                RESULTADO_PROCESO & "|" & _
                                BTN_ELIMINAR & "|" & _
                                BTN_PRINT & "|" & _
                                NUMERO_INTENTOS & "|" & _
                                OFICINA_VENTA & "|" & _
                                CAJERO & "|" & _
                                MONEDA & "|" & _
                                NUMERO_TELEFONO & "|" & _
                                TIPO_TARJETA_POS & "|" & _
                                ID_REF & "|" & _
                                TRANSACCION_ID_CAB & "|" & _
                                IP_CAJA
                        FILA = FILA & CELDA & ";"

                    End If

                Else
                    Dim FORMA_PAGO As String = Funciones.CheckStr(IIf(IsDBNull(item("DEPAV_DESCPAGO")), " ", item("DEPAV_DESCPAGO")))
                    Dim TIPO_TARJETA As String = ""
                    Dim NRO_TARJETA As String = ""
                    Dim MONTO As String = Funciones.CheckStr(IIf(IsDBNull(item("DEPAN_IMPORTE")), " ", item("DEPAN_IMPORTE")))
                    Dim ESTADO_ANULACION As String = ""
                    Dim RESULTADO_PROCESO As String = ""
                    Dim IP_CAJA As String = ""
                Dim BTN_ELIMINAR As String = "OK"
                Dim BTN_PRINT As String = "OK"

                'DATOS ENVIO POST'

                    Dim NUMERO_INTENTOS As String = ""
                    Dim OFICINA_VENTA As String = ""
                    Dim CAJERO As String = ""
                    Dim MONEDA As String = ""
                    Dim NUMERO_TELEFONO As String = ""
                    Dim TIPO_TARJETA_POS As String = ""
                    Dim ID_REF As String = ""
                    Dim TRANSACCION_ID_CAB As String = ""

                CELDA = FORMA_PAGO & "|" & _
                        TIPO_TARJETA & "|" & _
                        NRO_TARJETA & "|" & _
                        MONTO & "|" & _
                        ESTADO_ANULACION & "|" & _
                        RESULTADO_PROCESO & "|" & _
                        BTN_ELIMINAR & "|" & _
                        BTN_PRINT & "|" & _
                        NUMERO_INTENTOS & "|" & _
                        OFICINA_VENTA & "|" & _
                        CAJERO & "|" & _
                        MONEDA & "|" & _
                        NUMERO_TELEFONO & "|" & _
                        TIPO_TARJETA_POS & "|" & _
                        ID_REF & "|" & _
                        TRANSACCION_ID_CAB & "|" & _
                        IP_CAJA
                FILA = FILA & CELDA & ";"
                End If



                ' dtDetalle.Rows.Remove(item) '.RemoveAt(0)
            Next

            If (FILA.Length > 0) Then
                CargarFormasPago = "<SELECT>" & "0!" & FILA.Substring(0, FILA.Length - 1) & "</SELECT>"
            Else
                CargarFormasPago = "<SELECT>" & "1!No se encontraron medios de Pago" & "</SELECT>"

            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago - " & CargarFormasPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago - " & "Fin")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago  ERROR: " & ex.Message.ToString)
            Return "<SELECT>" & "1!" & "CargarFormasPago  ERROR - " & ex.Message.ToString() & "</SELECT>"

        End Try

    End Function
    <RemoteScriptingMethod(Description:="CargarFormasPagoRecaudacion")> _
  Public Function CargarFormasPagoRecaudacion(ByVal NroPedido As String, ByVal strTipoPago As String, ByVal CodCaja As String, ByVal CodPDV As String, Optional ByVal FechaP As String = "") As String

        Dim dsPagos As DataSet
        Dim objConsultaPos As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim objConsultaPos2 As New COM_SIC_Activaciones.BWEnvioTransacPOS
        Dim FILA As String = ""
        Dim CELDA As String = ""
        Dim codRespuesta As String = ""
        Dim msjRespuesta As String = ""
        Dim listaResponse As New ArrayList
            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            Dim objTransaccionesPos As New COM_SIC_Activaciones.clsTransaccionPOS
            'Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
        Dim objSicarPos As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim dsDeuda As DataSet
        Dim dtDetalle As DataTable
        Dim objOffline As New COM_SIC_OffLine.clsOffline
        Dim PagoPosContingencias As String
        Dim FormaPagoSap As String
        Dim DescPagoSap As String
        Dim ImporteSap As String
        Dim NumTarjetaSap As String
        Dim TipoTarjetaSAP As String
        Dim TipoTarjetaPOS As String
        Dim strCodRptTipPos As String
        Dim strMsgRptTipPos As String
        Dim i As Integer
        Try


            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            Dim nombreHost As String = System.Net.Dns.GetHostName
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
            objEntity.ipServidor = Funciones.CheckStr(ipServer)

            objEntity.numPedido = NroPedido
            objEntity.tipoPago = strTipoPago
            objEntity.nroCaja = CodCaja
            objEntity.codVenta = CodPDV
            objEntity.fechaTransaccionPos = FechaP


            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion  - INICIO")
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Input - Id Recaudacion " & objEntity.numPedido)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Input - Tipo Pago " & objEntity.tipoPago)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Input - Cajero " & objEntity.nroCaja)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Input - PDV " & objEntity.codVenta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Input - Fecha Pago " & objEntity.fechaTransaccionPos)


            objSicarDB.ConsultaDetallePagoRec(objEntity, codRespuesta, msjRespuesta, listaResponse)

            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Ouput - codRespuesta" & codRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Ouput - msjRespuesta" & msjRespuesta)


            Dim dtPagosPOS As New DataTable

            If codRespuesta = "0" Then

                dtPagosPOS.Columns.Add("FORMA_PAGO")
                dtPagosPOS.Columns.Add("TIPO_TARJETA")
                dtPagosPOS.Columns.Add("NRO_TARJETA")
                dtPagosPOS.Columns.Add("MONTO")
                dtPagosPOS.Columns.Add("ESTADO_ANULACION")
                dtPagosPOS.Columns.Add("RESULTADO_PROCESO")
                dtPagosPOS.Columns.Add("TRNSN_ID_CAB")
                dtPagosPOS.Columns.Add("POSV_IPCAJA")
                dtPagosPOS.Columns.Add("TRANSACCION_ID")
                dtPagosPOS.Columns.Add("NUMERO_INTENTOS")
                dtPagosPOS.Columns.Add("OFICINA_VENTA")
                dtPagosPOS.Columns.Add("CAJERO")
                dtPagosPOS.Columns.Add("MONEDA")
                dtPagosPOS.Columns.Add("NUMERO_TELEFONO")
                dtPagosPOS.Columns.Add("TIPO_TARJETA_POS")
                dtPagosPOS.Columns.Add("TRNSV_ID_REF")
                dtPagosPOS.Columns.Add("NRO_CAJA")
                If (listaResponse Is Nothing) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion Ouput - listaResponse = Nothing")
                    FILA = ""
                Else
                    For i = 0 To listaResponse.Count - 1
                        Dim drPOS As DataRow = dtPagosPOS.NewRow

                        drPOS("FORMA_PAGO") = listaResponse.Item(i).FormaPago
                        drPOS("TIPO_TARJETA") = listaResponse.Item(i).TrnscTipoTarjeta
                        drPOS("NRO_TARJETA") = listaResponse.Item(i).TrnsvNroTarjeta
                        drPOS("MONTO") = listaResponse.Item(i).TrnsnMonto
                        drPOS("ESTADO_ANULACION") = listaResponse.Item(i).EstadoAnulacion
                        drPOS("RESULTADO_PROCESO") = listaResponse.Item(i).ResultadoProceso
                        drPOS("TRNSN_ID_CAB") = listaResponse.Item(i).TrnsnIdCab
                        drPOS("POSV_IPCAJA") = listaResponse.Item(i).PosvCodTerminal
                        drPOS("TRANSACCION_ID") = listaResponse.Item(i).TrnsnId
                        drPOS("NUMERO_INTENTOS") = listaResponse.Item(i).NumeroIntentos
                        drPOS("OFICINA_VENTA") = listaResponse.Item(i).OficinaVenta
                        drPOS("CAJERO") = listaResponse.Item(i).UsuavCajero
                        drPOS("MONEDA") = listaResponse.Item(i).IdConftipMoneda
                        drPOS("NUMERO_TELEFONO") = listaResponse.Item(i).NumeroTelefono
                        drPOS("TIPO_TARJETA_POS") = listaResponse.Item(i).TrnsvTipoTarjetaPos
                        drPOS("TRNSV_ID_REF") = listaResponse.Item(i).TrnsvIdRef
                        drPOS("NRO_CAJA") = listaResponse.Item(i).PosvNroCaja
                        dtPagosPOS.Rows.Add(drPOS)
                        dtPagosPOS.AcceptChanges()
                    Next
                End If

            Else

                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cargar Forma Pago Recaudacion - INICIO CONTINGENCIA")

                dtPagosPOS = objConsultaPos.ObtenerFormasDePagoRecTrans(NroPedido, "", strTipoPago, "", CodCaja, CodPDV, codRespuesta, msjRespuesta, FechaP).Tables(0)

                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cargar Forma Pago Recaudacion OUT - codRespuesta :" & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, " Cargar Forma Pago Recaudacion OUT - codRespuesta :" & msjRespuesta)

            End If




            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Devoluciones: [" & NroPedido & "] " & "CargarFormasPago - " & "Inicio : " & NroPedido)
            dsDeuda = objOffline.GetRegistroDeuda(NroPedido)
            dtDetalle = dsDeuda.Tables(2)

            For Each item As DataRow In dtDetalle.Rows
                PagoPosContingencias = "0"
                FormaPagoSap = Funciones.CheckStr(item("VIA_PAGO"))
                ImporteSap = Funciones.CheckStr(Math.Round(CDbl(item("IMPORTE_PAGADO")), 2))
                DescPagoSap = Funciones.CheckStr(item("DESC_VIA_PAGO"))
                NumTarjetaSap = Funciones.CheckStr(item("NRO_CHEQUE"))

                Dim strResult As String = objSicarPos.Obtener_TipoPOS(FormaPagoSap, Session("ALMACEN"), strCodRptTipPos, strMsgRptTipPos)
                If strResult.Length > 0 Then
                    TipoTarjetaSAP = strResult.Split("#")(0)
                Else
                    TipoTarjetaSAP = ""
                End If

                If TipoTarjetaSAP <> "" Then
                    Select Case TipoTarjetaSAP
                        Case "VIS"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosVI
                        Case "MCD"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosMC
                        Case "AMX"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosAM
                        Case "DIN"
                            TipoTarjetaPOS = ClsKeyPOS.strTipoPosDI
                    End Select


                    For i = 0 To dtPagosPOS.Rows.Count - 1

                        Dim FORMA_PAGO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")))
                        Dim MONTO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONTO")), " ", Math.Round(CDbl(dtPagosPOS.Rows(i).Item("MONTO")), 2)))
                        Dim NRO_TARJETA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NRO_TARJETA")), " ", dtPagosPOS.Rows(i).Item("NRO_TARJETA")))

                        If FORMA_PAGO = TipoTarjetaPOS And MONTO = ImporteSap And NRO_TARJETA = NumTarjetaSap Then

                            FORMA_PAGO = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("FORMA_PAGO")), " ", dtPagosPOS.Rows(i).Item("FORMA_PAGO")))
                            Dim TIPO_TARJETA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA")))
                            NRO_TARJETA = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NRO_TARJETA")), " ", dtPagosPOS.Rows(i).Item("NRO_TARJETA")))
                            MONTO = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONTO")), " ", Math.Round(CDbl(dtPagosPOS.Rows(i).Item("MONTO")), 2)))
                            Dim ESTADO_ANULACION As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("ESTADO_ANULACION")), " ", dtPagosPOS.Rows(i).Item("ESTADO_ANULACION")))
                            Dim RESULTADO_PROCESO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("RESULTADO_PROCESO")), " ", dtPagosPOS.Rows(i).Item("RESULTADO_PROCESO")))
                        Dim BTN_ELIMINAR As String = "OK"
                        Dim BTN_PRINT As String = "OK"

                        'DATOS ENVIO POST'
                            Dim ID_CAB As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TRNSN_ID_CAB")), " ", dtPagosPOS.Rows(i).Item("TRNSN_ID_CAB")))
                            Dim IP_CAJA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("POSV_IPCAJA")), " ", dtPagosPOS.Rows(i).Item("POSV_IPCAJA")))
                            Dim TRANSACCION_ID As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TRANSACCION_ID")), " ", dtPagosPOS.Rows(i).Item("TRANSACCION_ID")))
                            Dim NUMERO_INTENTOS As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NUMERO_INTENTOS")), " ", dtPagosPOS.Rows(i).Item("NUMERO_INTENTOS")))
                            Dim OFICINA_VENTA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("OFICINA_VENTA")), " ", dtPagosPOS.Rows(i).Item("OFICINA_VENTA")))
                            Dim CAJERO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("CAJERO")), " ", dtPagosPOS.Rows(i).Item("CAJERO")))
                            Dim MONEDA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("MONEDA")), " ", dtPagosPOS.Rows(i).Item("MONEDA")))
                            Dim NUMERO_TELEFONO As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NUMERO_TELEFONO")), " ", dtPagosPOS.Rows(i).Item("NUMERO_TELEFONO")))
                            Dim TIPO_TARJETA_POS As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")), " ", dtPagosPOS.Rows(i).Item("TIPO_TARJETA_POS")))
                            Dim ID_REF As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("TRNSV_ID_REF")), " ", dtPagosPOS.Rows(i).Item("TRNSV_ID_REF")))
                            Dim NRO_CAJA As String = Funciones.CheckStr(IIf(IsDBNull(dtPagosPOS.Rows(i).Item("NRO_CAJA")), " ", dtPagosPOS.Rows(i).Item("NRO_CAJA")))
                        CELDA = FORMA_PAGO & "|" & _
                                TIPO_TARJETA & "|" & _
                                NRO_TARJETA & "|" & _
                                MONTO & "|" & _
                                ESTADO_ANULACION & "|" & _
                                RESULTADO_PROCESO & "|" & _
                                BTN_ELIMINAR & "|" & _
                                BTN_PRINT & "|" & _
                                TRANSACCION_ID & "|" & _
                                NUMERO_INTENTOS & "|" & _
                                OFICINA_VENTA & "|" & _
                                CAJERO & "|" & _
                                MONEDA & "|" & _
                                NUMERO_TELEFONO & "|" & _
                                TIPO_TARJETA_POS & "|" & _
                                ID_REF & "|" & _
                                NRO_CAJA & "|" & _
                                IP_CAJA & "|" & _
                                ID_CAB
                        FILA = FILA & CELDA & ";"

                            dtPagosPOS.Rows.RemoveAt(i)
                            PagoPosContingencias = "1"
                            Exit For

                End If
                    Next

                    If PagoPosContingencias = "0" Then

                        Dim FORMA_PAGO = Funciones.CheckStr(item("DESC_VIA_PAGO"))
                        Dim TIPO_TARJETA As String = ""
                        Dim NRO_TARJETA = Funciones.CheckStr(item("NRO_CHEQUE"))
                        Dim MONTO = Funciones.CheckStr(Math.Round(CDbl(item("IMPORTE_PAGADO")), 2))
                        Dim ESTADO_ANULACION As String = ""
                        Dim RESULTADO_PROCESO As String = ""
                        Dim BTN_ELIMINAR As String = "OK"
                        Dim BTN_PRINT As String = "OK"

                        'DATOS ENVIO POST'
                        Dim ID_CAB As String = ""
                        Dim IP_CAJA As String = ""
                        Dim TRANSACCION_ID As String = ""
                        Dim NUMERO_INTENTOS As String = ""
                        Dim OFICINA_VENTA As String = ""
                        Dim CAJERO As String = ""
                        Dim MONEDA As String = ""
                        Dim NUMERO_TELEFONO As String = ""
                        Dim TIPO_TARJETA_POS As String = ""
                        Dim ID_REF As String = ""
                        Dim NRO_CAJA As String = ""

                        CELDA = FORMA_PAGO & "|" & _
                                TIPO_TARJETA & "|" & _
                                NRO_TARJETA & "|" & _
                                MONTO & "|" & _
                                ESTADO_ANULACION & "|" & _
                                RESULTADO_PROCESO & "|" & _
                                BTN_ELIMINAR & "|" & _
                                BTN_PRINT & "|" & _
                                TRANSACCION_ID & "|" & _
                                NUMERO_INTENTOS & "|" & _
                                OFICINA_VENTA & "|" & _
                                CAJERO & "|" & _
                                MONEDA & "|" & _
                                NUMERO_TELEFONO & "|" & _
                                TIPO_TARJETA_POS & "|" & _
                                ID_REF & "|" & _
                                NRO_CAJA & "|" & _
                                IP_CAJA & "|" & _
                                ID_CAB
                        FILA = FILA & CELDA & ";"
                    End If
                Else

                    Dim FORMA_PAGO = Funciones.CheckStr(item("DESC_VIA_PAGO"))
                    Dim TIPO_TARJETA As String = ""
                    Dim NRO_TARJETA = Funciones.CheckStr(item("NRO_CHEQUE"))
                    Dim MONTO = Funciones.CheckStr(Math.Round(CDbl(item("IMPORTE_PAGADO")), 2))
                    Dim ESTADO_ANULACION As String = ""
                    Dim RESULTADO_PROCESO As String = ""
                    Dim BTN_ELIMINAR As String = "OK"
                    Dim BTN_PRINT As String = "OK"

                    'DATOS ENVIO POST'
                    Dim ID_CAB As String = ""
                    Dim IP_CAJA As String = ""
                    Dim TRANSACCION_ID As String = ""
                    Dim NUMERO_INTENTOS As String = ""
                    Dim OFICINA_VENTA As String = ""
                    Dim CAJERO As String = ""
                    Dim MONEDA As String = ""
                    Dim NUMERO_TELEFONO As String = ""
                    Dim TIPO_TARJETA_POS As String = ""
                    Dim ID_REF As String = ""
                    Dim NRO_CAJA As String = ""

                    CELDA = FORMA_PAGO & "|" & _
                            TIPO_TARJETA & "|" & _
                            NRO_TARJETA & "|" & _
                            MONTO & "|" & _
                            ESTADO_ANULACION & "|" & _
                            RESULTADO_PROCESO & "|" & _
                            BTN_ELIMINAR & "|" & _
                            BTN_PRINT & "|" & _
                            TRANSACCION_ID & "|" & _
                            NUMERO_INTENTOS & "|" & _
                            OFICINA_VENTA & "|" & _
                            CAJERO & "|" & _
                            MONEDA & "|" & _
                            NUMERO_TELEFONO & "|" & _
                            TIPO_TARJETA_POS & "|" & _
                            ID_REF & "|" & _
                            NRO_CAJA & "|" & _
                            IP_CAJA & "|" & _
                            ID_CAB
                    FILA = FILA & CELDA & ";"

                End If
            Next



                If (FILA.Length > 0) Then
                    CargarFormasPagoRecaudacion = "<SELECT>" & FILA.Substring(0, FILA.Length - 1) & "</SELECT>"
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- Cargar Forma Pago Recaudacion - FILA :" & FILA.Substring(0, FILA.Length - 1))
                Else
                    CargarFormasPagoRecaudacion = "<SELECT>" & "SD|Sin Datos" & "</SELECT>"
                objFileLog.Log_WriteLog(pathFile, strArchivo, "- Cargar Forma Pago Recaudacion : " & "SD|Sin Datos")
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, " - Cargar Forma Pago Recaudacion - FIN ")

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "-Anulacion de Recaudaciones[ FILA.Substring(0, FILA.Length - 1)]: [" & "Error|" + ex.Message.ToString() & "]")

            Return "<SELECT>" & "Error|" + ex.Message.ToString() & "</SELECT>"

        End Try

    End Function
    <RemoteScriptingMethod(Description:="CargarTransaccionesPOS")> _
  Public Function CargarTransaccionesPOS(ByVal strTramaCargarTrans As String) As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarTransaccionesPOS : " & "Inicio : " & strTramaCargarTrans)

        Dim arrayParam As Array = strTramaCargarTrans.Split("|")
        Dim strPdv As String = arrayParam(0)
        Dim strTipoTarjetaPos As String = arrayParam(1)
        Dim strTipoTransaccion As String = arrayParam(2)
        Dim strTipoOperacionId As String = arrayParam(3)
        Dim strFechaInicial As String = arrayParam(4)
        Dim strFechaFinal As String = arrayParam(5)
        Dim strCodComercio As String = arrayParam(6)
        Dim strUsuarioCajero As String = arrayParam(7)
        Dim strEstado As String = arrayParam(8)
        Dim strNroRef As String = arrayParam(9)
        Dim dtTransacciones As DataTable
        Dim objTransaccionesPos As New COM_SIC_Activaciones.clsTransaccionPOS
        Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS
        Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS

        objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
        Dim nombreHost As String = System.Net.Dns.GetHostName
        Dim nombreServer As String = System.Net.Dns.GetHostName
        Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString
        objEntity.ipServidor = Funciones.CheckStr(ipServer)

        If (strPdv Is Nothing) Then
            objEntity.codVenta = ""
        Else
            objEntity.codVenta = strPdv
        End If

        If (strUsuarioCajero Is Nothing) Then
            objEntity.codCajero = ""
        Else
            objEntity.codCajero = strUsuarioCajero
        End If

        If (strCodComercio Is Nothing) Then
            objEntity.codEstablecimiento = ""
        Else
            objEntity.codEstablecimiento = strCodComercio
        End If

        If (strTipoTarjetaPos Is Nothing) Then
            objEntity.tipoTarjeta = ""
        Else
            objEntity.tipoTarjeta = strTipoTarjetaPos
        End If


            objEntity.codOperacion = ""


        If (strTipoTransaccion Is Nothing) Then

            objEntity.tipoTransaccion = ""
        Else
            objEntity.tipoTransaccion = strTipoTransaccion
        End If

        If (strEstado Is Nothing) Then
            objEntity.estadoTransaccion = ""
        Else
            objEntity.estadoTransaccion = strEstado
        End If
        If (strNroRef Is Nothing) Then
            objEntity.numVoucher = ""
        Else
            objEntity.numVoucher = strNroRef
        End If

        objFileLog.Log_WriteLog(pathFile, strArchivo, "Cargando Transsacciones POS|OPERACIONES NO FINANCIERAS ")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strPdv : " & objEntity.codVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTarjetaPos : " & objEntity.tipoTarjeta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strCodComercio: " & objEntity.codEstablecimiento)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaInicial : " & strFechaInicial)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strFechaFinal : " & strFechaFinal)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strUsuarioCajero : " & objEntity.codCajero)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoTransaccion : " & objEntity.tipoTransaccion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strTipoOperacion : " & objEntity.codOperacion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "strEstado : " & objEntity.estadoTransaccion)
        objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Input: " & "NroRef : " & objEntity.numVoucher)

        Dim FILA As String = ""
        Dim CELDA As String = ""
        Dim codRespuesta As String = ""
        Dim msjRespuesta As String = ""
        Dim listaResponse As New ArrayList
        Dim i As Integer = 0
        Try
            ' dsTransacciones = objTransaccionesPos.TransaccionesPOS(strPdv, strTipoTarjetaPos, strNroCaja, strFechaInicial, strFechaFinal, strUsuarioCajero, strTipoTransaccion, strTipoOperacionId, strEstado, strNroRef, codRespuesta, msjRespuesta)
            objSicarDB.ConsultaDetalleReporte(objEntity, strFechaInicial, strFechaFinal, codRespuesta, msjRespuesta, listaResponse)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarTransaccionesPOS: [CodRespuesta]" & codRespuesta & "[MsjRespuesta]" & msjRespuesta)

            If codRespuesta = 0 Then
                If codRespuesta <> 0 Or listaResponse Is Nothing Then
                    listaResponse = Nothing
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarTransaccionesPOS : " & "Count : " & Funciones.CheckStr(listaResponse.Count - 1))
                    For i = 0 To listaResponse.Count - 1

                        Dim TRNSN_ID_CAB As String = listaResponse.Item(i).trnsnIdCab
                        Dim PUNTO_VENTA As String = listaResponse.Item(i).codPdv
                        Dim NUMERO_CAJA As String = listaResponse(i).posvNroCaja
                        Dim NUMERO_PEDIDO As String = listaResponse(i).trnsvNumPedido
                        Dim TIPO_TRANSACCION As String = listaResponse(i).trnsvTipoTrans
                        Dim OPERACION As String = listaResponse(i).trnscOperacion
                        Dim TIPO_TARJETA_POS As String = listaResponse(i).trnsvTipoTarjetaPos
                        Dim NUMERO_TARJETA As String = listaResponse(i).trnsvNroTarjeta
                        Dim MONTO As String = listaResponse(i).trnsnMonto
                        Dim ESTADO As String = listaResponse(i).trnsvEstado
                        Dim MONEDA As String = listaResponse(i).idConftipMoneda
                        Dim USUARIO_CAJERO As String = listaResponse(i).usuavCajero
                        Dim FECHA_TRANSACCION As String = listaResponse(i).fechaTransaccionPos
                        Dim HORA_TRANSACCION As String = listaResponse(i).horaTransaccionPos
                        Dim TERMINAL As String = listaResponse(i).posvCodTerminal
                        Dim EQUIPO As String = listaResponse(i).posvCodEquipo
                        Dim CODIGO_CAJERO As String = listaResponse(i).usuavCajeroId
                        Dim NUMERO_TARJETA_CORTO As String = ""

                        If (NUMERO_TARJETA.Trim.Length >= 4) Then

                            NUMERO_TARJETA_CORTO = NUMERO_TARJETA.Substring(NUMERO_TARJETA.Length - 4, 4)
                        Else

                            NUMERO_TARJETA_CORTO = ""
                        End If

                        If NUMERO_PEDIDO = "" Then
                            NUMERO_PEDIDO = String.Format("{0}{1}{2}", objEntity.numVoucher, NUMERO_TARJETA_CORTO, CODIGO_CAJERO)
                        End If


                        CELDA = TRNSN_ID_CAB & "|" & _
                        PUNTO_VENTA & "|" & _
                    NUMERO_CAJA & "|" & _
                    NUMERO_PEDIDO & "|" & _
                    TIPO_TRANSACCION & "|" & _
                    OPERACION & "|" & _
                    TIPO_TARJETA_POS & "|" & _
                    NUMERO_TARJETA & "|" & _
                    MONTO & "|" & _
                    ESTADO & "|" & _
                    MONEDA & "|" & _
                    USUARIO_CAJERO & "|" & _
                    FECHA_TRANSACCION & "|" & _
                    HORA_TRANSACCION & "|" & _
                    TERMINAL & "|" & _
                    EQUIPO & "|" & _
                    CODIGO_CAJERO

                        FILA = FILA & CELDA & ";"
                    Next
                End If

            Else
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Error en el Servicio: " & "codRespuesta : " & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Error en el Servicio: " & "msjRespuesta : " & msjRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - Ejecutando Contingencia - Inicio")

                dtTransacciones = objTransaccionesPos.TransaccionesPOS(objEntity.codVenta, objEntity.tipoTarjeta, objEntity.codEstablecimiento, _
                                   strFechaInicial, strFechaFinal, objEntity.codCajero, objEntity.tipoTransaccion, _
                                   objEntity.codOperacion, objEntity.estadoTransaccion, objEntity.numVoucher, codRespuesta, msjRespuesta).Tables(0)

                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "codRespuesta : " & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "msjRespuesta : " & msjRespuesta)

                For i = 0 To dtTransacciones.Rows.Count - 1
                    'DATOS MOSTRAR TABLE'
                    Dim TRNSN_ID_CAB As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSN_ID_CAB")), " ", dtTransacciones.Rows(i).Item("TRNSN_ID_CAB")))
                    Dim PUNTO_VENTA As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("COD_PDV")), " ", dtTransacciones.Rows(i).Item("COD_PDV")))
                    Dim NUMERO_CAJA As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("POSV_NROCAJA")), " ", dtTransacciones.Rows(i).Item("POSV_NROCAJA")))
                    Dim NUMERO_PEDIDO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_NUMPEDIDO")), " ", dtTransacciones.Rows(i).Item("TRNSV_NUMPEDIDO")))
                    Dim NUMERO_REFERENCIA As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_ID_REF")), " ", dtTransacciones.Rows(i).Item("TRNSV_ID_REF")))
                    Dim TIPO_TRANSACCION As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_TIPO_TRANS")), " ", dtTransacciones.Rows(i).Item("TRNSV_TIPO_TRANS")))
                    Dim OPERACION As String = ""
                    Dim TIPO_TARJETA_POS As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_TIPO_TARJETA_POS")), " ", dtTransacciones.Rows(i).Item("TRNSV_TIPO_TARJETA_POS")))
                    Dim NUMERO_TARJETA As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_NRO_TARJETA")), " ", dtTransacciones.Rows(i).Item("TRNSV_NRO_TARJETA")))
                    Dim MONTO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSN_MONTO")), " ", dtTransacciones.Rows(i).Item("TRNSN_MONTO")))
                    Dim ESTADO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("TRNSV_ESTADO")), " ", dtTransacciones.Rows(i).Item("TRNSV_ESTADO")))
                    Dim MONEDA As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("ID_CONFTIP_MONEDA")), " ", dtTransacciones.Rows(i).Item("ID_CONFTIP_MONEDA")))
                    Dim USUARIO_CAJERO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("USUAV_CAJERO")), " ", dtTransacciones.Rows(i).Item("USUAV_CAJERO")))
                    Dim FECHA_TRANSACCION As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("FECHA_TRANSACCION_POS")), " ", dtTransacciones.Rows(i).Item("FECHA_TRANSACCION_POS")))
                    Dim HORA_TRANSACCION As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("HORA_TRANSACCION_POS")), " ", dtTransacciones.Rows(i).Item("HORA_TRANSACCION_POS")))
                    Dim TERMINAL As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("POSV_COD_TERMINAL")), " ", dtTransacciones.Rows(i).Item("POSV_COD_TERMINAL")))
                    Dim EQUIPO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("POSV_COD_EQUIPO")), " ", dtTransacciones.Rows(i).Item("POSV_COD_EQUIPO")))
                    Dim CODIGO_CAJERO As String = Funciones.CheckStr(IIf(IsDBNull(dtTransacciones.Rows(i).Item("USUAV_CAJERO_ID")), " ", dtTransacciones.Rows(i).Item("USUAV_CAJERO_ID")))
                    CELDA = TRNSN_ID_CAB & "|" & _
    PUNTO_VENTA & "|" & _
    NUMERO_CAJA & "|" & _
    NUMERO_PEDIDO & "|" & _
    TIPO_TRANSACCION & "|" & _
    OPERACION & "|" & _
    TIPO_TARJETA_POS & "|" & _
    NUMERO_TARJETA & "|" & _
    MONTO & "|" & _
    ESTADO & "|" & _
    MONEDA & "|" & _
    USUARIO_CAJERO & "|" & _
    FECHA_TRANSACCION & "|" & _
    HORA_TRANSACCION & "|" & _
    TERMINAL & "|" & _
    EQUIPO & "|" & _
    CODIGO_CAJERO

                    FILA = FILA & CELDA & ";"
                Next

                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "codRespuesta : " & codRespuesta)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "TransaccionesPOS - " & "msjRespuesta : " & msjRespuesta)
            End If

            If (FILA.Length > 0) Then
                FILA = FILA.Substring(0, FILA.Length - 1)
            End If

            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarTransaccionesPOS : " & "Fin : ")

            Return "<SELECT>" & FILA & "</SELECT>"

        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, strArchivo, "CargarTransaccionesPOS  ERROR: " & ex.Message.ToString)
            Return "<SELECT>Error|" + ex.Message.ToString() + "</SELECT>"

        End Try

    End Function

    <RemoteScriptingMethod(Description:="POS")> _
    Public Function GuardarTransactionApeCiePOS(ByVal strTrama As String) As String

        Try
            Dim strCodRpt As String = ""
            Dim strMsgRpt As String = ""
            Dim nombreServer As String = System.Net.Dns.GetHostName
            Dim ipServer As String = System.Net.Dns.GetHostByName(nombreServer).AddressList(0).ToString

            Dim strTramaArrys As String() = strTrama.Split("|")
            'Dim strTipoTran As String = Funciones.CheckStr(strTramaArrys(9).Substring(strTramaArrys(9).IndexOf("=") + 1))

            Dim objEntity As New COM_SIC_Activaciones.BeEnvioTransacPOS

            objEntity.ipServidor = Funciones.CheckStr(ipServer)
            objEntity.UserAplicacion = Funciones.CheckStr(Me.CurrentUser())
            objEntity.nroRegistro = strTramaArrys(3).Substring(strTramaArrys(3).IndexOf("=") + 1)
            objEntity.fechaTransaccionPos = String.Format("{0:dd/MM/yyyy}", DateTime.Now)
            objEntity.tipoOperacion = strTramaArrys(0).Substring(strTramaArrys(0).IndexOf("=") + 1)
            objEntity.estadoTransaccion = strTramaArrys(1).Substring(strTramaArrys(1).IndexOf("=") + 1)
            objEntity.descTransaccion = strTramaArrys(2).Substring(strTramaArrys(2).IndexOf("=") + 1)
            objEntity.codCajero = Funciones.CheckStr(Session("USUARIO"))
            objEntity.fechaTransaccion = String.Format("{0:dd/MM/yyyy}", DateTime.Now)


            objEntity.estadoTransaccion = IIf(objEntity.estadoTransaccion = "3", "1", "2") '3 Transaccion Aceptada - 4 Transaccion Rechazada

            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "nroRegistro : " & objEntity.nroRegistro)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "fechaMovimiento : " & objEntity.fechaTransaccionPos)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "tipoMovimiento : " & objEntity.tipoOperacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "estadoTransaccion : " & objEntity.estadoTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "descTransaccion : " & objEntity.descTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "codCajero : " & objEntity.codCajero)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "fechaTransaccion : " & objEntity.fechaTransaccion)

            Dim objSicarDB As New COM_SIC_Activaciones.BWEnvioTransacPOS
            objSicarDB.GuardarTransactionAperturaCierre(objEntity, strCodRpt, strMsgRpt)



            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "strCodRpt : " & strCodRpt)
            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "strMsgRpt : " & strMsgRpt)


            If strCodRpt <> "0" Then

                Dim objTransascPos As New COM_SIC_Activaciones.clsTransaccionPOS

                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Error en el Servicio " & "strCodRpt : " & strCodRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Error en el Servicio " & "strMsgRpt : " & strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Ejecutando Contingencia - Inicio")
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Ejecutando Contingencia - SP: PKG_SISCAJ_POS.SICASI_MOV_CAJA")
                objTransascPos.MovCajaPOS(objEntity, strCodRpt, strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Ejecutando Contingencia - strCodRpt:" & strCodRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Ejecutando Contingencia - strMsgRpt:" & strMsgRpt)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS  - Ejecutando Contingencia - Fin")

            End If

            GuardarTransactionApeCiePOS = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"


        Catch ex As Exception

            objFileLog.Log_WriteLog(pathFile, strArchivo, "GuardarTransactionCierrePOS : " & "ERROR : " & ex.Message)
            Return "<SELECT>1|" & ex.Message.ToString & "</SELECT>"

        End Try
    End Function

    'INICIATIVA-318 INI
    <RemoteScriptingMethod(Description:="ValidarCajaCerrada")> _
    Public Function ValidarCajaCerrada() As String
        Dim strCodRpt As String = String.Empty
        Dim strMsgRpt As String = String.Empty
        Try
            Dim objclsOffline = New COM_SIC_OffLine.clsOffline
            Dim strCajaAsignadaID As String
            Dim strFechaHoy As Date = DateTime.Now.ToString("dd/MM/yyyy")
            Dim strFechaAnterior As Date = strFechaHoy.Today.AddDays(-1)
            Dim strAlmacen As String = Session("ALMACEN")
            Dim strMensajeCierreAnterior As String = String.Empty
            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim ParametroConfigurable As String = ConfigurationSettings.AppSettings("KeyValidacionCajaCerrada")

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "ParametroConfigurable", ParametroConfigurable))
            If ParametroConfigurable = "1" Then


                objFileLog.Log_WriteLog(pathFile, strArchivo, "*************************************************************************************")
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", "LOG_INICIATIVA_318", "ObtenerCajaAsignadaCuadreIndividual Inicio"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "strAlmacen", strAlmacen))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "codUsuario", codUsuario))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "strFechaAnterior", strFechaAnterior))

                strCajaAsignadaID = objclsOffline.ObtenerCajaAsignadaCuadreIndividual(strAlmacen, codUsuario, Funciones.CheckStr(strFechaAnterior))

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 OUTPUT", "strCajaAsignadaID", strCajaAsignadaID))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", "LOG_INICIATIVA_318", "ObtenerCajaAsignadaCuadreIndividual Fin"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "*************************************************************************************")

                objFileLog.Log_WriteLog(pathFile, strArchivo, "*************************************************************************************")
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", "LOG_INICIATIVA_318", "GetCierreCajaIndividualAnterior Inicio"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "strAlmacen", strAlmacen))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "strCajaAsignadaID", strCajaAsignadaID))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "strFechaAnterior", strFechaAnterior))

                strMensajeCierreAnterior = objclsOffline.GetCierreCajaIndividualAnterior(strAlmacen, strCajaAsignadaID, Funciones.CheckStr(strFechaAnterior))

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 OUTPUT", "strMensajeCierreAnterior", strMensajeCierreAnterior))
                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}", "LOG_INICIATIVA_318", "GetCierreCajaIndividualAnterior Fin"))
                objFileLog.Log_WriteLog(pathFile, strArchivo, "*************************************************************************************")

                If Not strMensajeCierreAnterior.Equals(String.Empty) Then
                    Dim strMsjContinuar As String = " El proceso continuará"
                    strCodRpt = "1"
                    strMsgRpt = String.Format("{0} {1}", strMensajeCierreAnterior, " El proceso continuará")

                    ValidarCajaCerrada = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"
                Else
                    strCodRpt = "0"
                    strMsgRpt = String.Empty

                    ValidarCajaCerrada = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"


                End If

            Else

                objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0} - {1}: {2}", "LOG_INICIATIVA_318 INPUT", "ParametroConfigurable", ParametroConfigurable))

            End If



        Catch ex As Exception
            strCodRpt = "0"
            strMsgRpt = String.Empty
            ValidarCajaCerrada = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-{1}", "ERROR: ValidarCajaCerrada", ex.Message.ToString))
        End Try
    End Function
    'INICIATIVA - 529 INI
    <RemoteScriptingMethod(Description:="AutoAsignacionCaja")> _
    Public Function AutoAsignacionCaja() As String
        objFileLog.Log_WriteLog(pathFile, strArchivo, "*******************************************************")
        objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Inicio AutoAsignacionCaja()")
        Dim strCodRpt As String = String.Empty
        Dim strMsgRpt As String = String.Empty
        Try

            Dim codUsuario As String = Convert.ToString(Session("USUARIO")).PadLeft(10, CChar("0"))
            Dim objOffline As New COM_SIC_OffLine.clsOffline
            Dim dsCajeros As DataSet
            Dim dsCajas As DataSet
            dsCajeros = objOffline.Get_ConsultaCajeros(Session("ALMACEN"), "C")
            Dim existVend As Boolean = False
            For Each item As DataRow In dsCajeros.Tables(0).Rows
                If codUsuario = item("USUARIO") Then
                    existVend = True
                End If
            Next
            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("LOG_INICIATIVA-318 - Existe Vendedor: {0}", existVend))
            If Not existVend Then
                strCodRpt = "2"
                strMsgRpt = String.Empty
            Else
                Dim dsCajeroA As DataSet
                Dim dateTimeValueCaja As DateTime
                Dim cultureNameX As String = "es-PE"
                Dim cultureX As CultureInfo = New CultureInfo(cultureNameX)
                dateTimeValueCaja = Convert.ToDateTime(DateTime.Now, cultureX)
                Dim sFechaCaj As String = dateTimeValueCaja.ToLocalTime.ToShortDateString
                objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Inicio GetDatosAsignacionCajero()")
                dsCajeroA = objOffline.GetDatosAsignacionCajero(Funciones.CheckStr(Session("ALMACEN")), sFechaCaj, codUsuario)
                objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Fin GetDatosAsignacionCajero()")
                If (dsCajeroA Is Nothing OrElse dsCajeroA.Tables(0).Rows.Count <= 0) Then
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Inicio Get_CajaOficinas()")
                    dsCajas = objOffline.Get_CajaOficinas(Session("ALMACEN"))
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Fin Get_CajaOficinas()")
                    Dim strCodCaja As String = dsCajas.Tables(0).Rows.Item(0).ItemArray(0)
                    Dim strDescCaja As String = dsCajas.Tables(0).Rows.Item(0).ItemArray(1)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Inicio Set_CajeroDiario()")
                    objOffline.Set_CajeroDiario(Session("ALMACEN"), codUsuario, Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000"), strCodCaja)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Fin Set_CajeroDiario()")
                    strCodRpt = "0"
                    strMsgRpt = String.Format("Se detectó que no tiene caja asignada. Se le asignó a la caja: {0}.", strDescCaja)
                    objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("LOG_INICIATIVA-318 - Se detectó que no tiene caja asignada. Se le asignó a la caja: {0}.", strDescCaja))
                Else
                    objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Tiene Caja asignada")
                End If
            End If
            objFileLog.Log_WriteLog(pathFile, strArchivo, "LOG_INICIATIVA-318 - Fin AutoAsignacionCaja()")
            objFileLog.Log_WriteLog(pathFile, strArchivo, "*******************************************************")
            AutoAsignacionCaja = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"

        Catch ex As Exception
            strCodRpt = "1"
            strMsgRpt = String.Empty
            AutoAsignacionCaja = "<SELECT>" & strCodRpt & "|" & strMsgRpt & "</SELECT>"

            objFileLog.Log_WriteLog(pathFile, strArchivo, String.Format("{0}-{1}", "ERROR: AutoAsignacionCaja", ex.Message.ToString))
        End Try
    End Function
    'INICIATIVA - 529 FIN
    'INICIATIVA-318 FIN


End Class
