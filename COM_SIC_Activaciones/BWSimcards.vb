Imports System.Configuration
Public Class BWSimcards



    Dim oTransaccion As New COM_SIC_Activaciones.WSSimCards.ebsSimcards


    Public Function ObtenerDatosNroTelef(ByVal ID_TRANSACCION As String, ByVal ID_APLICACION As String, ByVal NOMBRE_APLICACION As String, ByVal USUARIO_APLICACION As String, ByVal NRO_TELEF As String, ByVal MATERIAL_COD As String, ByVal NRO_SERIE As String, ByRef ID_TRANSACCION_RESULTADO As String, ByRef COD_RESULTADO As String, ByRef MENSAJE_RESULTADO As String)



        'ConstUrlWSSiMCards    KEY DEL WEB CONFIG

        'oTransaccion.obtenerDatosNroTelef.(ID_TRANSACCION, ID_APLICACION, NOMBRE_APLICACION, USUARIO_APLICACION, NRO_TELEF, USUARIO_COD)
        'Dim oCallserviceDatosParametro As New COM_SIC_Activaciones.callserviceDatosParametro


        oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlWSSiMCards").ToString()
        oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        oTransaccion.Timeout = CInt(ConfigurationSettings.AppSettings("ConstTimeOutActivacion").ToString())

        Dim strResultadoSimCards As String

        Dim e As WSSimCards.itReturnType()
        Dim er As WSSimCards.nroSimcardsDataType()

        strResultadoSimCards = oTransaccion.obtenerDatosNroTelef(ID_TRANSACCION, ID_APLICACION, NOMBRE_APLICACION, USUARIO_APLICACION, NRO_TELEF, MATERIAL_COD, NRO_SERIE, ID_TRANSACCION_RESULTADO, er, e)

        Return strResultadoSimCards

    End Function


    Public Function EliminarNroTelef(ByVal ID_TRANSACCION As String, ByVal ID_APLICACION As String, ByVal NOMBRE_APLICACION As String, ByVal USUARIO_APLICACION As String, ByVal NRO_TELEF As String, ByVal USUARIO_COD As String, ByRef ID_TRANSACCION_RESULTADO As String, ByRef ID_CODIGO_RESULTADO As String, ByRef MENSAJE_RESULTADO As String)

        oTransaccion.Url = ConfigurationSettings.AppSettings("ConstUrlWSSiMCards").ToString()
        oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials
        oTransaccion.Timeout = CInt(ConfigurationSettings.AppSettings("ConstTimeOutActivacion").ToString())

        Dim strResultadoSimCards As String

        Dim strResultadoEliminarNroTelef As String



        Dim ID_CODIGO_RESULTADO1 As WSSimCards.itReturnType()


        'Dim MENSAJE_RESULTADO2 As New WSSimCards.ebsSimcards.

        Dim MENSAJE_RESULTADO2 As String

        strResultadoEliminarNroTelef = oTransaccion.eliminarNroTelef(ID_TRANSACCION, ID_APLICACION, NOMBRE_APLICACION, USUARIO_APLICACION, NRO_TELEF, USUARIO_COD, ID_TRANSACCION_RESULTADO, ID_CODIGO_RESULTADO1)
        '       oTransaccion.eliminarNroTelef()

        Return strResultadoEliminarNroTelef


    End Function




End Class
