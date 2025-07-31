Public Class clsRecarga


    Public Enum eNvTIMPPVError
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Nombre            : eNvTIMPPVError
        ' Proposito         : Define los codigos de error devueltos por los metodos
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        OperacionExitosa = 0
        ErrorDeComunicaciones = -1  ' Error de comunicaciones. En la propiedad CodigoRpta se almacena el codigo de error generado
        ErrorAplicativo = -2        ' Error del Aplicativo. Revisar las propiedades OrigenRpta y CodigoRpta para determinar que sistema contesto la transaccion y el código de rechazo
        ErrorDeExcepcion = -3       ' Error de comunicaciones. En la propiedad CodigoRpta se almacena el codigo de error generado
    End Enum



    Private Const NvTIMPPVComponentName As String = "NvTIMPPVx.NvTIMPPV"
    Private Const NvTIMPPVCaller As Long = 3




    Public Function Anulacion(ByVal pvTerminalID As String, _
                              ByVal pvTrace As String, _
                              ByVal pvCanal As String, _
                              ByVal pvTelefono As String, _
                              ByVal pvMonto As String, _
                              ByVal pvMoneda As String, _
                              ByVal pvProducto As String, _
                              ByVal pvBinAdquiriente As String, _
                              ByVal pvCodCadena As String, _
                              ByVal pvCodComercio As String, _
                              ByVal pvNumeroRecargaOriginal As String, _
                              ByVal pvRUC As String, _
                              ByVal pvNombreCliente As String, _
                              ByVal strCodOficina As String) _
                              As String
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Proposito         : Anulacion de recarga. La anulacion se hace con el numero
        '                     de recarga original. Si el cliente no conoce el numero de
        '                     recarga, salvo que la aplicacion PVU tenga una base de
        '                     datos donde pueda encontrar la recarga original no se
        '                     podra realizar la anulacion
        '                     Esta transacción permite extornar la recarga efectuada.
        '                     Para que la anulación sea aprobada debe cumplir con
        '                     las siguientes condiciones
        '                     (estas condiciones son validadas por el switch):
        '                     - La anulación deberá efectuarse el mismo día que se realizó la compra.
        '                     - La anulación será procesada hasta 5 horas después de efectuada la recarga.
        '                     - La anulación será aprobada si el celular cuenta con el importe total recargado
        ' Output            : Error, OrigenRpta, CodigoRpta, DescripcionRpta,
        '                     SaldoResultante, MonedaSaldo, NumeroRecarga,
        '                     NumeroDocumentoAutorizadoAnulado
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim lError As eNvTIMPPVError
        Dim lOrigenRpta As Long 'eNvTIMPPVOrigenRpta
        Dim lCodigoRpta As Long
        Dim lDescripcionRpta As String
        Dim lSaldoResultante As Double
        Dim lMonedaSaldo As Long
        Dim lNumeroRecarga As Long
        Dim lNumeroDocumentoAutorizado As String
        Dim objComponente

        lError = eNvTIMPPVError.OperacionExitosa   ' Por defecto se asume la operacion como exitosa


        If Left(Trim(strCodOficina), 1) = "S" Then
            strCodOficina = "0"
        Else
            strCodOficina = "1"
        End If
        Try
            Select Case strCodOficina
                Case "1"
                    ' Inicio logica de la operacion
                    objComponente = CreateObject(NvTIMPPVComponentName)
                    With objComponente
                        .TerminalID = Left(Trim(pvTerminalID), 8)
                        .Trace = pvTrace
                        .Canal = pvCanal
                        .Telefono = Left(Trim(pvTelefono), 11)
                        .Monto = pvMonto
                        .Moneda = pvMoneda
                        .Producto = pvProducto
                        .BinAdquiriente = Left(Trim(pvBinAdquiriente), 6)
                        .CodCadena = Left(Trim(pvCodCadena), 15)
                        .CodComercio = Left(Trim(pvCodComercio), 15)
                        .NumeroRecargaOriginal = pvNumeroRecargaOriginal
                        .Ruc = Left(Trim(pvRUC), 14)
                        .NombreCliente = Left(Trim(pvNombreCliente), 50)

                        lError = .Anulacion

                        lOrigenRpta = .OrigenRpta
                        lCodigoRpta = .CodigoRpta
                        lDescripcionRpta = .DescripcionRpta

                        If lError = eNvTIMPPVError.OperacionExitosa Then
                            lSaldoResultante = .SaldoResultante
                            lMonedaSaldo = .MonedaSaldo
                            lNumeroRecarga = .NumeroRecarga
                            lNumeroDocumentoAutorizado = .NumeroDocumentoAutorizado
                        End If
                    End With
                    ' Fin logica de la operacion
                Case Else
                    Anulacion = "0;2;0;RESPUESTA OK;;;;;;;;;;;"
            End Select
        Catch ex As Exception
            lError = eNvTIMPPVError.ErrorAplicativo
            lOrigenRpta = NvTIMPPVCaller
            lCodigoRpta = -1
            lDescripcionRpta = ex.Message
        Finally
            objComponente = Nothing
        End Try
        ' Valores devueltos por el metodo
        Return Trim(CStr(lError)) & ";" & Trim(CStr(lOrigenRpta)) & ";" & _
                    Trim(CStr(lCodigoRpta)) & ";" & Trim(CStr(lDescripcionRpta)) & ";" & _
                    Trim(CStr(lSaldoResultante)) & ";" & Trim(CStr(lMonedaSaldo)) & ";" & _
                    Trim(CStr(lNumeroRecarga)) & ";" & Trim(CStr(lNumeroDocumentoAutorizado))

    End Function


End Class
