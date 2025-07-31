Imports System
Imports System.Configuration

Public Class CuentaSeguridad
    Protected _KeyAcuerdoCorp As String
    Protected _User As String
    Protected _Password As String
    Protected _Tipo As Type
    Public objsegu As COM_SIC_Seguridad.Configuracion

    Public ReadOnly Property User() As String
        Get
            Return _User
        End Get

    End Property

    Public ReadOnly Property Password() As String
        Get
            Return _Password
        End Get

    End Property

    Public Sub New(ByVal pCodAplicacion As String)
        If (pCodAplicacion = ConstantesFS.K_COD_APP_REGISTRO_DOCUMENTO) Then
            InicializarCuenta_APP_ACUERDO_CORPORATIVO()
        End If
    End Sub


    Protected Sub InicializarCuenta_APP_ACUERDO_CORPORATIVO()

        _KeyAcuerdoCorp = ConfigurationSettings.AppSettings("DOC_Key").ToString()

        Dim objDatosConex As COM_SIC_Seguridad.ClsConexion
        objsegu = New COM_SIC_Seguridad.Configuracion(_KeyAcuerdoCorp)
        objDatosConex = objsegu.GetConexion()
        _User = objDatosConex.GetUsuario
        _Password = objDatosConex.GetPassword

    End Sub
End Class
