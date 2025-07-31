Public Class clsAuditoriaWS
    Public Sub New()
    End Sub

    Public Function RegistrarAuditoria(ByVal vTransaccion As String, _
                                        ByVal vServicio As String, _
                                        ByVal vIPCliente As String, _
                                        ByVal vNombreCliente As String, _
                                        ByVal vIPServidor As String, _
                                        ByVal vNombreServidor As String, _
                                        ByVal vCuentaUsuario As String, _
                                        ByVal vTelefono As String, _
                                        ByRef vMonto As String, _
                                        ByRef vTexto As String) As Boolean

        Dim blnRespuesta As Boolean = False
        Dim strResultado As String

        Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
        objAuditoriaWS.Url = Configuration.ConfigurationSettings.AppSettings("consRutaWSSeguridad")
        objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim objRequestAuditoria As New AuditoriaWS.RegistroRequest
        Dim objResponseAuditoria As New AuditoriaWS.RegistroResponse

        objRequestAuditoria.transaccion = vTransaccion
        objRequestAuditoria.servicio = vServicio
        objRequestAuditoria.ipCliente = vIPCliente
        objRequestAuditoria.nombreCliente = vNombreCliente
        objRequestAuditoria.ipServidor = vIPServidor
        objRequestAuditoria.nombreServidor = vNombreServidor
        objRequestAuditoria.cuentaUsuario = vCuentaUsuario
        objRequestAuditoria.telefono = vTelefono
        objRequestAuditoria.monto = vMonto
        objRequestAuditoria.texto = vTexto

        objResponseAuditoria = objAuditoriaWS.registroAuditoria(objRequestAuditoria)
        strResultado = objResponseAuditoria.resultado

        If objResponseAuditoria.estado = 0 Then
            blnRespuesta = False
        Else
            blnRespuesta = True
        End If
        Return blnRespuesta
    End Function


    Public Function ConsultarOpcionesPagina(ByVal vUser As Integer, ByVal vAplicacion As Integer, Optional ByRef vResultado As Boolean = False) As ArrayList

        Dim Opciones As ArrayList

        Try
            Opciones = New ArrayList
            Dim objAuditoriaWS As New AuditoriaWS.EbsAuditoriaService
            Dim objRequestAuditoria As New AuditoriaWS.PaginaOpcionesUsuarioRequest
            Dim objResponseAuditoria As New AuditoriaWS.PaginaOpcionesUsuarioResponse

            objAuditoriaWS.Url = Configuration.ConfigurationSettings.AppSettings("consRutaWSSeguridad")
            objAuditoriaWS.Credentials = System.Net.CredentialCache.DefaultCredentials

            objRequestAuditoria.user = vUser
            objRequestAuditoria.aplicCod = vAplicacion

            objResponseAuditoria = objAuditoriaWS.leerPaginaOpcionesPorUsuario(objRequestAuditoria)

            If objResponseAuditoria.resultado = 0 Then
                For Each rpt As AuditoriaWS.PaginaOpcionType In objResponseAuditoria.listaOpciones
                    Opciones.Add(rpt.clave)
                Next
                vResultado = True
            End If
        Catch ex As Exception
        End Try

        Return Opciones

    End Function



End Class
