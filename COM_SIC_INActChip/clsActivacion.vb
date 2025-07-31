Imports System.EnterpriseServices
'<Transaction(TransactionOption.Required), JustInTimeActivation(True), Synchronization(SynchronizationOption.Required)> _
Public Class clsActivacion
    '   Inherits ServicedComponent
    Public Enum PVUSIXError
        TransAprob = 0  'Transaccion procesada
        TransRech = 1  ' Transaccion rechazada
        TransNoProc = 2  ' Transaccion no pudo ser enviada al servidor aplicativo
    End Enum
    Private Const PVUSIXComponentName As String = "PvuSixJava.clsPVUSIX"

    '  <AutoComplete(True)> _
        Public Function Set_CHIPPrepagoPorRobo(ByVal strValores As String) As String



        Dim objComponente As Object
        Dim arrValores As Object

        ' Valores de entrada para el componente
        Dim strTerminal As String
        Dim strTrace As String
        Dim strCanal As String
        Dim strAdquiriente As String
        Dim strOfiVta As String
        Dim strMSISDN As String
        Dim strMotivo As String
        Dim dblCodCaso As Double
        Dim strICCID As String

        ' Valores de salida para el componente
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescRpta As String
        Dim strFechaTXN As String
        Dim strHoraTXN As String
        Dim strIdOper As String
        Dim strIdSix As String
        Dim dblCosto As String

        Dim strERR As String

        Dim intRpta As Integer


        Try
            'Se recibe la trama de valores y se pasa a un arreglo
            arrValores = Split(strValores, ";")

            strTerminal = arrValores(10)
            strTrace = arrValores(4)
            strCanal = arrValores(9)
            strAdquiriente = arrValores(8)
            strOfiVta = arrValores(3)
            strMSISDN = arrValores(0)
            strMotivo = arrValores(2)
            dblCodCaso = CDbl("0" & arrValores(7))
            strICCID = arrValores(6)

            strERR = 0

            objComponente = CreateObject(PVUSIXComponentName)

            strERR = 1

            With objComponente

                .Trace = strTrace
                strERR = 2
                .Canal = strCanal
                strERR = 3
                .BinAdquiriente = strAdquiriente
                strERR = 4
                .TerminalID = strTerminal
                strERR = 5
                .Comercio = strOfiVta
                strERR = 6
                .MSISDN = strMSISDN
                strERR = 7
                .Motivo = strMotivo
                strERR = 8
                .CodigoCaso = dblCodCaso
                strERR = 9
                .ICCID = strICCID
                strERR = 10

                intRpta = .fpintChgSIMCardPrePagoRobo()
                strERR = 11
                strOrigenRpta = .OrigenRespuesta
                strERR = 12
                strCodigoRpta = .CodigoRespuesta
                strERR = 13
                strDescRpta = .DescripcionRespuesta & ". IDSIX: " & .IdentificadorSIX
                strERR = 14

                If intRpta = PVUSIXError.TransAprob Then

                    strFechaTXN = .FechaTxn
                    strERR = 15
                    strHoraTXN = .HoraTxn
                    strERR = 16
                    strIdOper = .IdentificadorOperacion
                    strERR = 17
                    strIdSix = .IdentificadorSIX
                    strERR = 18
                    dblCosto = .Costo
                    strERR = 19
                End If

            End With

        Catch ex As Exception
            ' se colocara log
        Finally
            objComponente = Nothing

            Set_CHIPPrepagoPorRobo = CStr(intRpta) & ";|;|" & CStr(strOrigenRpta) & ";" & CStr(strCodigoRpta) & ";" & _
                                     CStr(strDescRpta) & ";" & CStr(strFechaTXN) & ";" & CStr(strHoraTXN) & ";" & _
                                     CStr(strIdOper) & ";" & CStr(strIdSix) & ";" & CStr(dblCosto)
        End Try

    End Function

    ' <AutoComplete(True)> _
        Public Function Set_CHIPPrepago(ByVal strValores As String) As String



        Dim objComponente As Object
        Dim arrValores As Object

        ' Valores de entrada para el componente
        Dim strTerminal As String
        Dim strTrace As String
        Dim strCanal As String
        Dim strAdquiriente As String
        Dim strOfiVta As String
        Dim strMSISDN As String
        Dim strMotivo As String
        Dim dblCodCaso As String
        Dim strICCID As String

        ' Valores de salida para el componente
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescRpta As String
        Dim strFechaTXN As String
        Dim strHoraTXN As String
        Dim strIdOper As String
        Dim strIdSix As String
        Dim dblCosto As String


        Dim intRpta As Integer

        Try
            'Se recibe la trama de valores y se pasa a un arreglo
            arrValores = Split(strValores, ";")

            strTerminal = arrValores(10)
            strTrace = arrValores(4)
            strCanal = arrValores(9)
            strAdquiriente = arrValores(8)
            strOfiVta = arrValores(3)
            strMSISDN = arrValores(0)
            strMotivo = arrValores(2)
            dblCodCaso = CDbl("0" & arrValores(7))
            strICCID = arrValores(6)


            objComponente = CreateObject(PVUSIXComponentName)

            With objComponente
                .Trace = strTrace
                .Canal = strCanal
                .BinAdquiriente = strAdquiriente
                .TerminalID = strTerminal
                .Comercio = strOfiVta
                .MSISDN = strMSISDN
                .Motivo = strMotivo
                .CodigoCaso = dblCodCaso
                .ICCID = strICCID

                intRpta = .fpintChgSIMCardPrePago()
                strOrigenRpta = .OrigenRespuesta
                strCodigoRpta = .CodigoRespuesta
                strDescRpta = .DescripcionRespuesta & ". IDSIX: " & .IdentificadorSIX

                If intRpta = PVUSIXError.TransAprob Then
                    strFechaTXN = .FechaTxn
                    strHoraTXN = .HoraTxn
                    strIdOper = .IdentificadorOperacion
                    strIdSix = .IdentificadorSIX
                    dblCosto = .Costo
                End If

            End With
        Catch ex As Exception
            ' se colocara log
        Finally

            objComponente = Nothing

            Set_CHIPPrepago = CStr(intRpta) & ";|;|" & CStr(strOrigenRpta) & ";" & CStr(strCodigoRpta) & ";" & _
                                     CStr(strDescRpta) & ";" & CStr(strFechaTXN) & ";" & CStr(strHoraTXN) & ";" & _
                                     CStr(strIdOper) & ";" & CStr(strIdSix) & ";" & CStr(dblCosto)
        End Try

    End Function

    ' <AutoComplete(True)> _
        Public Function Set_CHIPControl(ByVal strValores As String) As String

        Dim objComponente As Object
        Dim arrValores As Object

        ' Valores de entrada para el componente
        Dim strTerminal As String
        Dim strTrace As String
        Dim strCanal As String
        Dim strAdquiriente As String
        Dim strOfiVta As String
        Dim strMSISDN As String
        Dim strMotivo As String
        Dim dblCodCaso As String
        Dim strICCID As String
        Dim strERR As String

        ' Valores de salida para el componente
        Dim strOrigenRpta As String
        Dim strCodigoRpta As String
        Dim strDescRpta As String
        Dim strFechaTXN As String
        Dim strHoraTXN As String
        Dim strIdOper As String
        Dim strIdSix As String
        Dim dblCosto As String
        Dim strOpcCambio As String


        Dim intRpta As Integer

        Try

            'Se recibe la trama de valores y se pasa a un arreglo
            arrValores = Split(strValores, ";")

            strTerminal = arrValores(10)
            strTrace = arrValores(4)
            strCanal = arrValores(9)
            strAdquiriente = arrValores(8)
            strOfiVta = arrValores(3)
            strMSISDN = arrValores(0)
            strMotivo = arrValores(2)
            dblCodCaso = CDbl("0" & arrValores(7))
            strICCID = arrValores(6)
            strOpcCambio = arrValores(11)

            objComponente = CreateObject(PVUSIXComponentName)

            strERR = 1
            With objComponente
                .Trace = strTrace
                strERR = 2
                .Canal = strCanal
                strERR = 3
                .BinAdquiriente = strAdquiriente
                strERR = 4
                .TerminalID = strTerminal
                strERR = 5
                .Comercio = strOfiVta
                strERR = 6
                .MSISDN = strMSISDN
                strERR = 7
                .Motivo = strMotivo
                strERR = 8
                .CodigoCaso = dblCodCaso
                strERR = 9
                .ICCID = strICCID
                strERR = 10
                .OpcionCambio = strOpcCambio
                strERR = 11
                intRpta = .fpintChgSIMCardControl()
                strERR = 12
                strOrigenRpta = .OrigenRespuesta
                strERR = 13
                strCodigoRpta = .CodigoRespuesta
                strERR = 14
                strDescRpta = .DescripcionRespuesta & ". IDSIX: " & .IdentificadorSIX

                If intRpta = PVUSIXError.TransAprob Then
                    strERR = 15
                    strFechaTXN = .FechaTxn
                    strERR = 16
                    strHoraTXN = .HoraTxn
                    strERR = 17
                    strIdOper = .IdentificadorOperacion
                    strERR = 18
                    strIdSix = .IdentificadorSIX
                    strERR = 19
                    dblCosto = .Costo
                    strERR = 20
                End If

            End With
        Catch ex As Exception
            ' se colocara log

        Finally

            objComponente = Nothing

            Set_CHIPControl = CStr(intRpta) & ";|;|" & CStr(strOrigenRpta) & ";" & CStr(strCodigoRpta) & ";" & _
                                     CStr(strDescRpta) & ";" & CStr(strFechaTXN) & ";" & CStr(strHoraTXN) & ";" & _
                                     CStr(strIdOper) & ";" & CStr(strIdSix) & ";" & CStr(dblCosto)
        End Try

    End Function

End Class
