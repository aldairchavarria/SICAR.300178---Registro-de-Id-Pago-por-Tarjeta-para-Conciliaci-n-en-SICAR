'PROY-31766 - INICIO
Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB

Public Class clsWConsultaIGV

    Public Function ObtenerIGV(ByRef strCodRpta As String, ByRef strMsgRpta As String) As Double
        Dim objConsultaPvu As New COM_SIC_Activaciones.clsConsultaPvu
        Dim objWSConsultaIGV As New COM_SIC_Activaciones.BWConsultaIGV
        Dim strUpdateCache As String = "0"
        Dim dblIGVD As Double

        Try
            objWSConsultaIGV.ObtenerIGV(strUpdateCache, strCodRpta, strMsgRpta, dblIGVD)
            If Not strCodRpta.Equals("0") Or dblIGVD <= 0 Then
                dblIGVD = objConsultaPvu.ObtenerIGV(strCodRpta, strMsgRpta)
            End If
        Catch ex As Exception
            dblIGVD = objConsultaPvu.ObtenerIGV(strCodRpta, strMsgRpta)
        End Try
        dblIGVD = Math.Round(dblIGVD, 2)
        Return dblIGVD
    End Function
End Class
'PROY-31766 - FIN
