Imports Claro.Datos
Imports Claro.Datos.DAAB
Imports System.Data

Public Class clsError

	Public Shared Function Get_DescripcionError(ByVal pCodigo As String) As String
		'---
		Dim oDescripcion As Object
		Dim objSeg As New Seguridad_NET.clsSeguridad
		Dim sCadenaConexion As String = objSeg.FP_GetConnectionString("2", "SISCAJA")
		Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, sCadenaConexion)

		Try
			'--define e inicializa parámetros
			Dim arrParam() As DAAB.DAABRequest.Parameter = { _
				New DAAB.DAABRequest.Parameter("P_CODIGO", DbType.String, 3, pCodigo, ParameterDirection.Input), _
                New DAAB.DAABRequest.Parameter("P_DESCRIPCION", DbType.String, 100, ParameterDirection.Output) _
				}
			objRequest.Parameters.AddRange(arrParam)

			objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "PKG_SISCAJ.SP_GET_DESCRIPCION_ERROR"
			'--
			objRequest.Factory.ExecuteScalar(objRequest)
			'--
			oDescripcion = CType(objRequest.Parameters(1), IDataParameter).Value
			'--retorna respuesta
			If IsNothing(oDescripcion) Or IsDBNull(oDescripcion) Then
				Return String.Empty
			Else
				Return oDescripcion.ToString
			End If
		Catch ex As Exception
			Throw ex
		Finally
			objRequest.Factory.Dispose()
			objRequest.Parameters.Clear()
		End Try

	End Function
End Class
