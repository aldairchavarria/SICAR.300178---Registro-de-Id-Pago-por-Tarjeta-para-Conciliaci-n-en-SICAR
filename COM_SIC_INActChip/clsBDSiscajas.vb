Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Imports System.Data.SqlClient

Public Class clsBDSiscajas
    Dim strCadenaConexion As String
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function FP_ConsultaUsuarioRed(ByVal codigoSAP As String) As DataSet

        Dim oDataset As DataSet
        Try

            strCadenaConexion = objSeg.FP_GetConnectionString("1", "INTRATIM")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.SQL, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAAB.DAABRequest.Parameter("@K_COD_SAP", DbType.String, 8, codigoSAP, ParameterDirection.Input)}

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "GET_USUARIO_RED"
            objRequest.Parameters.AddRange(arrParam)

            oDataset = objRequest.Factory.ExecuteDataset(objRequest)
        Catch ex As Exception
            oDataset = Nothing
        End Try

        FP_ConsultaUsuarioRed = oDataset
    End Function
End Class
