Imports System.EnterpriseServices
Imports System.Configuration
Public Class clsDatosLinea

    Public Function DatosLineaPrepago(ByVal vTelefono As String, _
                                        ByVal providerIdPrepago As String, _
                                        ByVal providerIdControl As String, _
                                        ByVal intTimeOut As Integer, _
                                        ByVal vURL As String, _
                                        ByRef strEstado As String, _
                                        ByRef MensajeError As String) As String


        Dim strCodRetorno As String = ""

        Try

            Dim oTransaccion As New DatosPrepagoWS.EbsDatosPrepagoService
            Dim obPrepagoReq As New DatosPrepagoWS.INDatosPrepagoRequest
            Dim objPrepagoResp As New DatosPrepagoWS.INDatosPrepagoResponse

            oTransaccion.Url = vURL
            oTransaccion.Timeout = intTimeOut
            oTransaccion.Credentials = System.Net.CredentialCache.DefaultCredentials

            obPrepagoReq.telefono = vTelefono

            Dim listPrepago As String() = providerIdPrepago.Split("|")
            Dim listControl As String() = providerIdControl.Split("|")

            'Invocacion Metodo
            objPrepagoResp = oTransaccion.leerDatosPrepago(obPrepagoReq)
            strEstado = ""

            If Trim(objPrepagoResp.resultado) = "0" Then
                strEstado = objPrepagoResp.datosPrePago.isLocked
                strCodRetorno = "E"
                MensajeError = "Provider ID no identificado"

                For i As Integer = 0 To listPrepago.Length - 1
                    If (objPrepagoResp.datosPrePago.providerID = listPrepago(i)) Then
                        strCodRetorno = "P"
                        MensajeError = "Prepago"
                        Exit For
                    End If
                Next

                For i As Integer = 0 To listControl.Length - 1
                    If (objPrepagoResp.datosPrePago.providerID = listControl(i)) Then
                        strCodRetorno = "C"
                        MensajeError = "Control"
                        Exit For
                    End If
                Next
            Else
                strCodRetorno = "E"
                MensajeError = "No es Prepago ni Control"
            End If

            oTransaccion.Dispose()

        Catch ex As Exception

            strCodRetorno = "E"
            MensajeError = "El servicio esta temporalmente fuera de servicio." + ex.Message
        End Try

        Return strCodRetorno

    End Function
End Class
