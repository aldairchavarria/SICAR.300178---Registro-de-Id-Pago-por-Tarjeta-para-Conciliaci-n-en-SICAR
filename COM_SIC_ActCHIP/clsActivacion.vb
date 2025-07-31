Imports System.EnterpriseServices
Public Class clsActivacion
    ' Inherits ServicedComponent
    Public Function FK_ActualizaEstadoChip(ByVal p_ValorCampo As String, ByVal StrUrl As String, ByVal StrNomServicio As String, ByVal strUsuario As String) As String
        Dim strResult As String
        Dim objComponente As Object
        objComponente = CreateObject("COM_PVU_ActChip.clsActivacion")
        FK_ActualizaEstadoChip = objComponente.FK_ActualizaEstadoChip(p_ValorCampo, StrUrl, StrNomServicio, strUsuario)
        objComponente = Nothing
    End Function

End Class
