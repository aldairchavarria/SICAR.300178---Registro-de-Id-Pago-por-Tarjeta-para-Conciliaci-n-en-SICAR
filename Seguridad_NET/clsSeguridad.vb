Imports System.Web
Imports System.Configuration
'Imports System.EnterpriseServices
Imports System.Web.Caching
Imports COM_SIC_Seguridad


Public Class clsSeguridad
    '  Inherits ServicedComponent

    Dim strSistema As String = "PVU"
    Public BaseDatos As String

    Shared itemRemoved As Boolean = False
    Shared reason As CacheItemRemovedReason
    Dim onRemove As CacheItemRemovedCallback

    Public Usuario As String
    Public Password As String
    Public Idioma As String
    Public Servidor As String
    Public Sistema As String
    Public LoadBal As String
    Public MessServ As String
    Public R3Name As String
    Public Group As String
    Public objseg As COM_SIC_Seguridad.Configuracion

    Public nameArchivo As String = "LOG_SICAR_CONEXION"
    Public initFormatLog As String = String.Format("{0:dd-MM-yyyy hh:mm:ss}", DateTime.Now) + " | "





    Public Sub New()
        Dim objUser As Object
        Dim colData As New Collection


        BaseDatos = New COM_SIC_Seguridad.Configuracion(strSistema).LeerBaseDatos
        Usuario = New COM_SIC_Seguridad.Configuracion(strSistema).LeerUsuario
        Password = New COM_SIC_Seguridad.Configuracion(strSistema).LeerContrasena
        Servidor = New COM_SIC_Seguridad.Configuracion(strSistema).LeerServidor


        Idioma = "ES"
        Servidor = ConfigurationSettings.AppSettings("Servidor")
        Sistema = ConfigurationSettings.AppSettings("Sistema")
        LoadBal = ConfigurationSettings.AppSettings("gConstLoadBalancing")
        MessServ = ConfigurationSettings.AppSettings("gConstMessageServer")
        R3Name = ConfigurationSettings.AppSettings("gConstR3Name")
        Group = ConfigurationSettings.AppSettings("gConstGroup")

    End Sub

    Public Sub RemovedCallback(ByVal a As String, ByVal b As Object, ByVal r As CacheItemRemovedReason)
        itemRemoved = True
        reason = r

    End Sub




    Public Function FP_GetConnectionString(ByVal strCliente As String, ByVal strKeyRegEdit As String) As String
        Dim objUser As Object
        Dim strCadena As String
        Dim BaseDatos As String
        Dim Usuario As String
        Dim Password As String
        Dim Servidor As String
        Dim registro As Object
        Dim objDatosConex As COM_SIC_Seguridad.ClsConexion

        Try
            objDatosConex = System.Web.HttpContext.Current.Cache.Get(strKeyRegEdit)
            If IsNothing(objDatosConex) Then
                objseg = New COM_SIC_Seguridad.Configuracion(strKeyRegEdit)
                objDatosConex = objseg.GetConexion()
                BaseDatos = objDatosConex.GetBD
                Usuario = objDatosConex.GetUsuario
                Password = objDatosConex.GetPassword
                Servidor = objDatosConex.GetServidor
                itemRemoved = False
                onRemove = New CacheItemRemovedCallback(AddressOf Me.RemovedCallback)
                System.Web.HttpContext.Current.Cache.Add(strKeyRegEdit, objDatosConex, Nothing, DateTime.Now.AddMinutes(30), System.Web.HttpContext.Current.Cache.NoSlidingExpiration, CacheItemPriority.High, onRemove)
            Else
                BaseDatos = objDatosConex.GetBD
                Usuario = objDatosConex.GetUsuario
                Password = objDatosConex.GetPassword
                Servidor = objDatosConex.GetServidor
            End If
        Catch ex As Exception            
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Ini Error: FP_GetConnectionString", False)
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Var: strKeyRegEdit - " + strKeyRegEdit, False)
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Var: objDatosConex - ", False)
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Error Message: " + ex.Message.ToString(), False)
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Error StackTrace: " + ex.StackTrace.ToString(), False)
            HelperLog.EscribirLog("", nameArchivo, initFormatLog + strKeyRegEdit + "|Fin Error: FP_GetConnectionString", False)
            Throw ex
        End Try



        If strCliente = "1" Then 'SQL
            strCadena = "Data Source=" & Servidor & ";Initial Catalog=" & BaseDatos & ";User Id=" & Usuario & ";Password=" & Password & ";"
        End If

        If strCliente = "2" Then 'ORACLE
            strCadena = "user id=" & Usuario & ";data source=" & BaseDatos & ";password=" & Password
        End If

        FP_GetConnectionString = strCadena

    End Function

    Public Function FP_GetConnectionString_SinergiaPVU(ByVal strCliente As String, ByVal strKeyRegEdit As String) As String
        Dim objUser As Object
        Dim strCadena As String
        Dim BaseDatos As String
        Dim Usuario As String
        Dim Password As String
        Dim Servidor As String

        objUser = CreateObject("Seguridad_Test_UTL.clsSeguridad")
        BaseDatos = objUser.BaseDatos(strKeyRegEdit)
        BaseDatos = objUser.BaseDatos(strKeyRegEdit)
        Usuario = objUser.Usuario(strKeyRegEdit)
        Password = objUser.Password(strKeyRegEdit)
        Servidor = objUser.Servidor(strKeyRegEdit)


        If strCliente = "1" Then 'SQL
            strCadena = "Data Source=" & Servidor & ";Initial Catalog=" & BaseDatos & ";User Id=" & Usuario & ";Password=" & Password & ";"
        End If

        If strCliente = "2" Then 'ORACLE
            strCadena = "user id=" & Usuario & ";data source=" & BaseDatos & ";password=" & Password
        End If

        FP_GetConnectionString_SinergiaPVU = strCadena

        objUser = Nothing

    End Function

    Public Function GetHost(ByVal strIp As String) As String
        Dim objSeguBus As Object

        objSeguBus = CreateObject("Segu_Bus.Host")
        GetHost = objSeguBus.GetHost(strIp)
    End Function

End Class
