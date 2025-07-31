Imports System.Configuration

Public Class CambioPlanPostpagoNegocios

    Public Function ConsultaSolicitudNroSEC(ByVal nroSEC As String) As ArrayList
        Dim oSolicitudDatos As New ClsCambioPlanPostpago
        Return oSolicitudDatos.ConsultaSolicitudNroSEC(nroSEC)
    End Function

    Public Function ListarEquitSadicionales(ByVal P_PLAN As String) As DataTable
        Dim oSolicitudDatos As New ClsCambioPlanPostpago
        Return oSolicitudDatos.ListarEquitSadicionales(P_PLAN)
    End Function

    Public Function ListarEquitCintsistema(ByVal P_PLAN As String) As DataTable
        Dim oSolicitudDatos As New ClsCambioPlanPostpago
        Return oSolicitudDatos.ListarEquitCintsistema(P_PLAN)
    End Function
End Class
