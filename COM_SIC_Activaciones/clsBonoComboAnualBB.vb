Public Class clsBonoComboAnualBB

	Public Function bonoBlackberryPrepago(ByVal vMsisdn As String, ByVal vURL As String, ByVal intTimeOut As Integer, ByVal vUsuario As String, ByVal vAplicacionRTC As String, ByRef vMensaje As String) As Integer
		Dim objSer As New WS_Bono_BlackAnual.EbsActivacionBonoBBService
		Dim objRes As New WS_Bono_BlackAnual.DatosBlackberryPrepagoResponse
		Dim objReq As New WS_Bono_BlackAnual.DatosBlackberryPrepagoRequest
		Dim intNuevo As Integer = -1
		Try

			objSer.Url = vURL
			objSer.Timeout = intTimeOut
			objSer.Credentials = System.Net.CredentialCache.DefaultCredentials

			objReq.msisdn = vMsisdn
			objReq.usuario = vUsuario
			objReq.aplicacionRTC = vAplicacionRTC

			objRes = objSer.bonoBlackberryPrepago(objReq)
			intNuevo = Integer.Parse(objRes.resultado)
			vMensaje = objRes.mensaje

		Catch ex As Exception
			intNuevo = -1
			vMensaje = objRes.mensaje
		End Try
		Return intNuevo
	End Function

End Class
