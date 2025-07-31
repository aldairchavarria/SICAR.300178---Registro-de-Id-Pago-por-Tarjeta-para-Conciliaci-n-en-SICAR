Option Strict Off
Imports System
Imports System.Web
Imports System.Configuration
Imports System.IO

Public NotInheritable Class ControlProxyFTP

    Const TRANSFER_TYPE_ASCII = 1
    Const TRANSFER_TYPE_BINARY = 2


    Public Shared Function PutFileFTP(ByVal pRutaArchivoOrigen As String, ByVal pRutaArchivoDestino As String) As Boolean
        Dim objFTP As Object
        Dim bResultado As Boolean
        Dim sError As String


        Dim ipFTP As String = ConfigurationSettings.AppSettings("DOC_IPFTP").ToString
        Dim puertoFTP As String = ConfigurationSettings.AppSettings("DOC_PuertoFTP").ToString

        Dim objCuentaSegFS As New CuentaSeguridad(ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO)
        Dim userFTP As String = objCuentaSegFS.User
        Dim passFTP As String = objCuentaSegFS.Password
        objCuentaSegFS = Nothing


        'create reference to object
        objFTP = HttpContext.Current.Server.CreateObject("NIBLACK.ASPFTP")

        '---connection worked...now get the file
        bResultado = objFTP.bQPutFile(ipFTP, userFTP, passFTP, pRutaArchivoOrigen, pRutaArchivoDestino, TRANSFER_TYPE_BINARY)
        'get was successful
        If (Not bResultado) Then
            Throw New Exception(objFTP.sError)
        Else
            File.Delete(pRutaArchivoOrigen)
        End If

        '---
        objFTP = Nothing

        Return bResultado

    End Function

    Public Shared Function PutFileFTPSGA(ByVal pRutaArchivoOrigen As String, ByVal pRutaArchivoDestino As String) As Boolean
        Dim objFTP As Object
        Dim bResultado As Boolean
        Dim sError As String


        Dim ipFTP As String = ConfigurationSettings.AppSettings("DOC_IPFTP_DTH").ToString
        Dim puertoFTP As String = ConfigurationSettings.AppSettings("DOC_PuertoFTP_DTH").ToString

        Dim objCuentaSegFS As New CuentaSeguridadDTH(ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO)
        Dim userFTP As String = objCuentaSegFS.User
        Dim passFTP As String = objCuentaSegFS.Password
        objCuentaSegFS = Nothing

        'create reference to object
        objFTP = HttpContext.Current.Server.CreateObject("NIBLACK.ASPFTP")

        '---connection worked...now get the file
        bResultado = objFTP.bQPutFile(ipFTP, userFTP, passFTP, pRutaArchivoOrigen, pRutaArchivoDestino, TRANSFER_TYPE_BINARY)
        'get was successful
        If (Not bResultado) Then
            Throw New Exception(objFTP.sError)
        Else
            File.Delete(pRutaArchivoOrigen)
        End If

        '---
        objFTP = Nothing

        Return bResultado

    End Function

    Public Shared Function DeleteFileFTP(ByVal pRutaArchivo As String) As Boolean
        Dim objFTP As Object
        Dim bResultado As Boolean
        Dim sError As String


        Dim ipFTP As String = ConfigurationSettings.AppSettings("DOC_IPFTP").ToString
        Dim puertoFTP As String = ConfigurationSettings.AppSettings("DOC_PuertoFTP").ToString

        Dim objCuentaSegFS As New CuentaSeguridad(ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO)
        Dim userFTP As String = objCuentaSegFS.User
        Dim passFTP As String = objCuentaSegFS.Password
        objCuentaSegFS = Nothing


        'create reference to object
        objFTP = HttpContext.Current.Server.CreateObject("NIBLACK.ASPFTP")

        '---connection worked...now get the file
        bResultado = objFTP.bQDeleteFile(ipFTP, userFTP, passFTP, pRutaArchivo)
        'get was successful
        If (Not bResultado) Then
            Throw New Exception(objFTP.sError)
        End If

        '---
        objFTP = Nothing

        Return bResultado
    End Function

    Public Shared Function DownloadFileFTP(ByVal pRutaArchivoOrigen As String, ByVal pRutaArchivoDestino As String) As Boolean
        Dim objFTP As Object
        Dim bResultado As Boolean
        Dim sError As String


        Dim ipFTP As String = ConfigurationSettings.AppSettings("ACU_CORP_IPFTP").ToString
        Dim puertoFTP As String = ConfigurationSettings.AppSettings("ACU_CORP_PuertoFTP").ToString

        Dim objCuentaSegFS As New CuentaSeguridad(ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO)
        Dim userFTP As String = objCuentaSegFS.User
        Dim passFTP As String = objCuentaSegFS.Password
        objCuentaSegFS = Nothing

        'create reference to object
        objFTP = HttpContext.Current.Server.CreateObject("NIBLACK.ASPFTP")

        '---connection worked...now get the file
        bResultado = objFTP.bQGetFile(ipFTP, userFTP, passFTP, pRutaArchivoOrigen, pRutaArchivoDestino, TRANSFER_TYPE_ASCII, True)
        'get was successful
        If (Not bResultado) Then
            Throw New Exception(objFTP.sError)
        End If

        '---
        objFTP = Nothing

        Return bResultado

    End Function

    Public Shared Function CreateDirFTP(ByVal NameDirectorio As String) As Boolean
        Dim objFTP As Object
        Dim bResultado As Boolean
        Dim sError As String

        Dim ipFTP As String = ConfigurationSettings.AppSettings("DOC_IPFTP").ToString
        Dim objCuentaSegFS As New CuentaSeguridad(ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO)
        Dim userFTP As String = objCuentaSegFS.User
        Dim passFTP As String = objCuentaSegFS.Password
        objCuentaSegFS = Nothing

        ' Create reference to object
        objFTP = HttpContext.Current.Server.CreateObject("NIBLACK.ASPFTP")
        bResultado = objFTP.bQMakeDir(ipFTP, userFTP, passFTP, NameDirectorio)

        If (Not bResultado) Then
            Throw New Exception(objFTP.sError)
        End If

        objFTP = Nothing

        Return bResultado

    End Function

End Class
