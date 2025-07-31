Imports System.EnterpriseServices
Imports System.Configuration
Public Class clsClarify

    Public Function CrearInteraccion(ByVal vInteraccion As clsDatosInteraccion, _
                                    ByVal vURL As String, _
                                    ByVal intTimeOut As Integer, _
                                    ByRef strId As String, _
                                    ByRef strMensaje As String) As String


        Dim strResultado As String

        Try

            Dim oTransaccion As New WSClarify.clarifyWSService 'PROY-26366 - FASE I
            Dim oInteraccionRequest As New WSClarify.InteraccionRequest
            Dim oInteraccionResponse As New WSClarify.InteraccionResponse

            oTransaccion.Url = vURL
            oTransaccion.Timeout = intTimeOut
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials

            oInteraccionRequest.objId = vInteraccion.OBJID_CONTACTO
            oInteraccionRequest.siteObjId = vInteraccion.OBJID_SITE
            oInteraccionRequest.cuenta = vInteraccion.CUENTA
            oInteraccionRequest.telefono = vInteraccion.TELEFONO
            oInteraccionRequest.tipo = vInteraccion.TIPO_CODIGO
            oInteraccionRequest.clase = vInteraccion.CLASE_CODIGO
            oInteraccionRequest.subClase = vInteraccion.SUBCLASE_CODIGO
            oInteraccionRequest.metodoContacto = vInteraccion.METODO
            oInteraccionRequest.tipoInteraccion = vInteraccion.TIPO_INTER
            oInteraccionRequest.codigoEmpleado = vInteraccion.AGENTE
            oInteraccionRequest.codigoSistema = vInteraccion.USUARIO_PROCESO
            oInteraccionRequest.hechoEnUno = vInteraccion.HECHO_EN_UNO
            oInteraccionRequest.notas = vInteraccion.NOTAS
            oInteraccionRequest.flagCaso = vInteraccion.FLAG_CASO
            oInteraccionRequest.textResultado = vInteraccion.RESULTADO

            'Invocacion Metodo
            oInteraccionResponse = oTransaccion.nuevaInteraccion(oInteraccionRequest)

            strId = oInteraccionResponse.idInteraccion
            strResultado = oInteraccionResponse.resultado
            strMensaje = oInteraccionResponse.mensaje

            oInteraccionRequest = Nothing
            oInteraccionResponse = Nothing
            oTransaccion.Dispose()

        Catch ex As Exception

            strResultado = "999"
            strMensaje = "El servicio no se encuentra disponible en este momento. " + ex.Message
        End Try

        Return strResultado

    End Function

    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Public Function consultarDatosUsuario(ByVal vInteraccion As clsDatosInteraccion, _
                                ByRef strId As String, _
                                ByRef strMensaje As String) As String


        Dim strResultado As String
        Dim strModalidad As String

        Try

            Dim oTransaccion As New WSClarify.clarifyWSService
            Dim oConsultaDatosUsuarioRequest As New WSClarify.ConsultaDatosUsuarioRequest
            Dim oConsultaDatosUsuarioResponse As New WSClarify.ConsultaDatosUsuarioResponse


            oTransaccion.Url = ConfigurationSettings.AppSettings("consWSClarify")
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
            oTransaccion.Timeout = Funciones.CheckInt(ConfigurationSettings.AppSettings("consWSClarify_timeout"))

            oConsultaDatosUsuarioRequest.telefono = vInteraccion.TELEFONO
            oConsultaDatosUsuarioRequest.cuenta = ""
            oConsultaDatosUsuarioRequest.contactobjid_1 = ""
            oConsultaDatosUsuarioRequest.flag_registrado = "1"

            'Invocacion Metodo
            oConsultaDatosUsuarioResponse = oTransaccion.consultarDatosUsuario(oConsultaDatosUsuarioRequest)

            strResultado = oConsultaDatosUsuarioResponse.resultado
            strId = strResultado
            strMensaje = oConsultaDatosUsuarioResponse.mensaje
            If strResultado = 1 Then
                strModalidad = oConsultaDatosUsuarioResponse.usLista(0).modalidad
            Else
                strModalidad = ""
            End If

            oConsultaDatosUsuarioRequest = Nothing
            oConsultaDatosUsuarioResponse = Nothing
            oTransaccion.Dispose()

        Catch ex As Exception

            strResultado = "999"
            strMensaje = "El servicio no se encuentra disponible en este momento. " + ex.Message
        End Try

        Return strModalidad

    End Function
    'PROY-26366-IDEA-34247 FASE 1 - FIN

End Class
