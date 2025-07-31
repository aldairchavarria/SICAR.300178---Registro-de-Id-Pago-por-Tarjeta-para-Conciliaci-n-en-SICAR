Imports System
Imports System.Collections
Imports System.Configuration
Imports COM_SIC_Seguridad

Public Class BWObtenerOrdenToa

    Public objFileLog As New SICAR_Log
    Public pathFile As String = "<----LOG_ORDENES_TOA---->"
    Public nameLog As String = objFileLog.Log_CrearNombreArchivo("LogOrdenesTOA")

    Public Function ObtenerOrdenToaWS(ByVal NroPedido As String)

        Dim objResponseMessage As New ObtenerOrdenToaResponse
        objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[INICIO][PROY-140662 IDEA-142180 Mejora Delivery FASE 4][ObtenerOrdenToaWS]", String.Empty))

        Try
            Dim strUrlWS As String = "ConstUrlDetalleDLV"
            Dim user_ObtenerOrdenTOA As String
            Dim pass_ObtenerOrdenTOA As String
            Dim TimeOut_ObtenerOrdenTOA As String
            Dim objResponse As Object
            user_ObtenerOrdenTOA = ReadKeySettings.Key_EncriptBase64DP
            TimeOut_ObtenerOrdenTOA = ConfigurationSettings.AppSettings("TimeOut_OrdenTOA")

            Dim paramHeaders As Hashtable = New Hashtable

            paramHeaders.Add("idTransaccion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("msgid", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            paramHeaders.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            'paramHeaders.Add("accept", DateTime.Now.ToString("yyyyMMddHHmmssfff"))
            paramHeaders.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings("constAplicacion")))
            'paramHeaders.Add("ipAplicacion", DateTime.Now.ToString("yyyyMMddHHmmssfff"))

            Dim rs As New RestService
            objResponse = rs.GetInvoque(strUrlWS, paramHeaders, user_ObtenerOrdenTOA, Nothing, TimeOut_ObtenerOrdenTOA, NroPedido, GetType(ObtenerOrdenToaResponse), False)
            objResponseMessage = CType(objResponse, ObtenerOrdenToaResponse)


        Catch ex As Exception
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[PROY-140662 IDEA-142180 Mejora Delivery FASE 4][Error]", String.Empty))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[ERROR]", ex.Message))
            objFileLog.Log_WriteLog(pathFile, nameLog, String.Format("{0}-->{1}", "[ERROR]", ex.StackTrace))

        End Try

        Return objResponseMessage
    End Function
End Class
