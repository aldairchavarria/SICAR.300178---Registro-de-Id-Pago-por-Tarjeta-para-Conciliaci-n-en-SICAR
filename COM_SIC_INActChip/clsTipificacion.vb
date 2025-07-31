Imports Claro.Datos
Imports System.Configuration
Imports Claro.Datos.DAAB
Imports System

Public Class clsTipificacion
    Dim strCadenaConexion As String ' = "user id=sa;data source=TIMPRB;password=pyclarify"
    Dim objSeg As New Seguridad_NET.clsSeguridad

    Public Function ConsultaCliente(ByVal vPHONE As String, _
                                    ByVal vACCOUNT As String, _
                                    ByVal vCONTACTOBJID_1 As Int64, _
                                    ByVal vFLAG_REG As String, _
                                    ByRef vFLAG_CONSULTA As String, _
                                    ByRef vMSG_TEXT As String) As DataSet

        Dim oDataset As DataSet
        Try
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_PHONE", DbType.String, 20, vPHONE, ParameterDirection.Input), _
                    New DAABRequest.Parameter("P_ACCOUNT", DbType.String, 80, vACCOUNT, ParameterDirection.Input), _
                    New DAABRequest.Parameter("P_CONTACTOBJID_1", DbType.Int64, vCONTACTOBJID_1, ParameterDirection.Input), _
                    New DAABRequest.Parameter("P_FLAG_REG", DbType.String, 20, vFLAG_REG, ParameterDirection.Input), _
                    New DAABRequest.Parameter("P_FLAG_CONSULTA", DbType.String, 255, ParameterDirection.Output), _
                    New DAABRequest.Parameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output), _
                    New DAABRequest.Parameter("CUSTOMER", DbType.Object, ParameterDirection.Output)}

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_CUSTOMER_CLFY.SP_CUSTOMER_CLFY"
        objRequest.Parameters.AddRange(arrParam)

            oDataset = objRequest.Factory.ExecuteDataset(objRequest)

        Catch ex As Exception
            oDataset = Nothing
        End Try

        Return oDataset
    End Function

    Public Function InsertarPlantillaInteraccion(ByVal item As PlantillaInteraccion, _
                                                    ByVal vInteraccionId As String, _
                                                    ByRef rFlagInsercion As String, _
                                                    ByRef rMsgText As String) As String

        Try
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
        Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_NRO_INTERACCION", DbType.String, 255, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_1", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_2", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_3", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_4", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_5", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_6", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_7", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_8", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_9", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_10", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_11", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_12", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_13", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_14", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_15", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_16", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_17", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_18", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_19", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_20", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_21", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_22", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_23", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_24", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_25", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_26", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_27", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_28", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_29", DbType.String, 255, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_INTER_30", DbType.AnsiString, 7000, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_PLUS_INTER2INTERACT", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ADJUSTMENT_AMOUNT", DbType.Double, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ADJUSTMENT_REASON", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ADDRESS", DbType.String, 100, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_AMOUNT_UNIT", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_BIRTHDAY", DbType.Date, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARIFY_INTERACTION", DbType.String, 15, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARO_LDN1", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARO_LDN2", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARO_LDN3", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARO_LDN4", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL1", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL2", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL3", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL4", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL5", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLAROLOCAL6", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CONTACT_PHONE", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_DNI_LEGAL_REP", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_DOCUMENT_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_EMAIL", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FIRST_NAME", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FIXED_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_CHANGE_USER", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_LEGAL_REP", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_OTHER", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_TITULAR", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_IMEI", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_LAST_NAME", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_LASTNAME_REP", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_LDI_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_NAME_LEGAL_REP", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLARO_LDN1", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLARO_LDN2", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLARO_LDN3", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLARO_LDN4", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL1", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL2", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL3", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL4", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL5", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_CLAROLOCAL6", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_DOC_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_FIRST_NAME", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_FIXED_PHONE", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_LAST_NAME", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_LDI_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OLD_FIXED_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OPERATION_TYPE", DbType.String, 50, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OTHER_DOC_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OTHER_FIRST_NAME", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OTHER_LAST_NAME", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OTHER_PHONE", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_PHONE_LEGAL_REP", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_REFERENCE_PHONE", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_REASON", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_MODEL", DbType.String, 50, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_LOT_CODE", DbType.String, 50, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_REGISTERED", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_REGISTRATION_REASON", DbType.String, 80, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CLARO_NUMBER", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_MONTH", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OST_NUMBER", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_BASKET", DbType.String, 50, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_EXPIRE_DATE", DbType.Date, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ADDRESS5", DbType.String, 200, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CHARGE_AMOUNT", DbType.Decimal, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CITY", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_CONTACT_SEX", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_DEPARTMENT", DbType.String, 40, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_DISTRICT", DbType.String, 200, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_EMAIL_CONFIRMATION", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FAX", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_FLAG_CHARGE", DbType.String, 1, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_MARITAL_STATUS", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_OCCUPATION", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_POSITION", DbType.String, 30, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_REFERENCE_ADDRESS", DbType.String, 50, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_TYPE_DOCUMENT", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ZIPCODE", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("P_ICCID", DbType.String, 20, ParameterDirection.Input), _
                                                        New DAABRequest.Parameter("ID_INTERACCION", DbType.String, 255, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("FLAG_CREACION", DbType.String, 255, ParameterDirection.Output), _
                                                        New DAABRequest.Parameter("MSG_TEXT", DbType.String, 255, ParameterDirection.Output)}

        For j As Integer = 0 To arrParam.Length - 1
            arrParam(j).Value = System.DBNull.Value
        Next

        Dim i As Integer = 0
        Dim dateInicio As New DateTime(1, 1, 1)

        If Not vInteraccionId Is Nothing Then
            arrParam(i).Value = vInteraccionId
            item.X_PLUS_INTER2INTERACT = CheckDbl(vInteraccionId)
        End If
        i += 1
        If Not item.X_INTER_1 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_1)
        End If
        i += 1
        If Not item.X_INTER_2 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_2)
        End If
        i += 1
        If Not item.X_INTER_3 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_3)
        End If
        i += 1
        If Not item.X_INTER_4 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_4)
        End If
        i += 1
        If Not item.X_INTER_5 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_5)
        End If
        i += 1
        If Not item.X_INTER_6 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_6)
        End If
        i += 1
        If Not item.X_INTER_7 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_7)
        End If
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_8)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_9)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_10)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_11)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_12)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_13)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_14)
        i += 1
        If Not item.X_INTER_15 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_15)
        End If
        i += 1
        If Not item.X_INTER_16 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_16)
        End If
        i += 1
        If Not item.X_INTER_17 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_17)
        End If
        i += 1
        If Not item.X_INTER_18 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_18)
        End If
        i += 1
        If Not item.X_INTER_19 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_19)
        End If
        i += 1
        If Not item.X_INTER_20 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_20)
        End If
        i += 1
        If Not item.X_INTER_21 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_21)
        End If
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_22)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_23)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_24)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_25)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_26)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_27)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_INTER_28)
        i += 1
        If Not item.X_INTER_29 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_29)
        End If
        i += 1
        If Not item.X_INTER_30 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_INTER_30)
        End If
        i += 1
        arrParam(i).Value = CheckDbl(item.X_PLUS_INTER2INTERACT)
        i += 1
        arrParam(i).Value = CheckDbl(item.X_ADJUSTMENT_AMOUNT)
        i += 1
        If Not item.X_ADJUSTMENT_REASON Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_ADJUSTMENT_REASON)
        End If
        i += 1
        If Not item.X_ADDRESS Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_ADDRESS)
        End If
        i += 1
        If Not item.X_AMOUNT_UNIT Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_AMOUNT_UNIT)
        End If
        i += 1
        If  item.X_BIRTHDAY <> dateInicio Then
            arrParam(i).Value = CheckDate(item.X_BIRTHDAY)
        End If
        i += 1
        If Not item.X_CLARIFY_INTERACTION Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARIFY_INTERACTION)
        End If
        i += 1
        If Not item.X_CLARO_LDN1 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARO_LDN1)
        End If
        i += 1
        If Not item.X_CLARO_LDN2 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARO_LDN2)
        End If
        i += 1
        If Not item.X_CLARO_LDN3 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARO_LDN3)
        End If
        i += 1
        If Not item.X_CLARO_LDN4 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARO_LDN4)
        End If
        i += 1
        If Not item.X_CLAROLOCAL1 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL1)
        End If
        i += 1
        If Not item.X_CLAROLOCAL2 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL2)
        End If
        i += 1
        If Not item.X_CLAROLOCAL3 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL3)
        End If
        i += 1
        If Not item.X_CLAROLOCAL4 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL4)
        End If
        i += 1
        If Not item.X_CLAROLOCAL5 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL5)
        End If
        i += 1
        If Not item.X_CLAROLOCAL6 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLAROLOCAL6)
        End If
        i += 1
        If Not item.X_CONTACT_PHONE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CONTACT_PHONE)
        End If
        i += 1
        If Not item.X_DNI_LEGAL_REP Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_DNI_LEGAL_REP)
        End If
        i += 1
        If Not item.X_DOCUMENT_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_DOCUMENT_NUMBER)
        End If
        i += 1
        If Not item.X_EMAIL Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_EMAIL)
        End If
        i += 1
        If Not item.X_FIRST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FIRST_NAME)
        End If
        i += 1
        If Not item.X_FIXED_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FIXED_NUMBER)
        End If
        i += 1
        If Not item.X_FLAG_CHANGE_USER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_CHANGE_USER)
        End If
        i += 1
        If Not item.X_FLAG_LEGAL_REP Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_LEGAL_REP)
        End If
        i += 1
        If Not item.X_FLAG_OTHER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_OTHER)
        End If
        i += 1
        If Not item.X_FLAG_TITULAR Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_TITULAR)
        End If
        i += 1
        If Not item.X_IMEI Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_IMEI)
        End If
        i += 1
        If Not item.X_LAST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_LAST_NAME)
        End If
        i += 1
        If Not item.X_LASTNAME_REP Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_LASTNAME_REP)
        End If
        i += 1
        If Not item.X_LDI_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_LDI_NUMBER)
        End If
        i += 1
        If Not item.X_NAME_LEGAL_REP Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_NAME_LEGAL_REP)
        End If
        i += 1
        If Not item.X_OLD_CLARO_LDN1 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLARO_LDN1)
        End If
        i += 1
        If Not item.X_OLD_CLARO_LDN2 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLARO_LDN2)
        End If
        i += 1
        If Not item.X_OLD_CLARO_LDN3 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLARO_LDN3)
        End If
        i += 1
        If Not item.X_OLD_CLARO_LDN4 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLARO_LDN4)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL1 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL1)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL2 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL2)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL3 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL3)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL4 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL4)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL5 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL5)
        End If
        i += 1
        If Not item.X_OLD_CLAROLOCAL6 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_CLAROLOCAL6)
        End If
        i += 1
        If Not item.X_OLD_DOC_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_DOC_NUMBER)
        End If
        i += 1
        If Not item.X_OLD_FIRST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_FIRST_NAME)
        End If
        i += 1
        If Not item.X_OLD_FIXED_PHONE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_FIXED_PHONE)
        End If
        i += 1
        If Not item.X_OLD_LAST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_LAST_NAME)
        End If
        i += 1
        If Not item.X_OLD_LDI_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_LDI_NUMBER)
        End If
        i += 1
        If Not item.X_OLD_FIXED_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OLD_FIXED_NUMBER)
        End If
        i += 1
        If Not item.X_OPERATION_TYPE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OPERATION_TYPE)
        End If
        i += 1
        If Not item.X_OTHER_DOC_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OTHER_DOC_NUMBER)
        End If
        i += 1
        If Not item.X_OTHER_FIRST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OTHER_FIRST_NAME)
        End If
        i += 1
        If Not item.X_OTHER_LAST_NAME Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OTHER_LAST_NAME)
        End If
        i += 1
        If Not item.X_OTHER_PHONE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OTHER_PHONE)
        End If
        i += 1
        If Not item.X_PHONE_LEGAL_REP Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_PHONE_LEGAL_REP)
        End If
        i += 1
        If Not item.X_REFERENCE_PHONE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_REFERENCE_PHONE)
        End If
        i += 1
        If Not item.X_REASON Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_REASON)
        End If
        i += 1
        If Not item.X_MODEL Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_MODEL)
        End If
        i += 1
        If Not item.X_LOT_CODE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_LOT_CODE)
        End If
        i += 1
        If Not item.X_FLAG_REGISTERED Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_REGISTERED)
        End If
        i += 1
        If Not item.X_REGISTRATION_REASON Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_REGISTRATION_REASON)
        End If
        i += 1
        If Not item.X_CLARO_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CLARO_NUMBER)
        End If
        i += 1
        If Not item.X_MONTH Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_MONTH)
        End If
        i += 1
        If Not item.X_OST_NUMBER Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OST_NUMBER)
        End If
        i += 1
        If Not item.X_BASKET Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_BASKET)
        End If
        i += 1
        If  item.X_EXPIRE_DATE <> dateInicio Then
            arrParam(i).Value = CheckDate(item.X_EXPIRE_DATE)
        End If
        i += 1
        If Not item.X_ADDRESS5 Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_ADDRESS5)
        End If
        i += 1
        arrParam(i).Value = CheckDbl(item.X_CHARGE_AMOUNT)
        i += 1
        If Not item.X_CITY Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CITY)
        End If
        i += 1
        If Not item.X_CONTACT_SEX Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_CONTACT_SEX)
        End If
        i += 1
        If Not item.X_DEPARTMENT Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_DEPARTMENT)
        End If
        i += 1
        If Not item.X_DISTRICT Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_DISTRICT)
        End If
        i += 1
        If Not item.X_EMAIL_CONFIRMATION Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_EMAIL_CONFIRMATION)
        End If
        i += 1
        If Not item.X_FAX Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FAX)
        End If
        i += 1
        If Not item.X_FLAG_CHARGE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_FLAG_CHARGE)
        End If
        i += 1
        If Not item.X_MARITAL_STATUS Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_MARITAL_STATUS)
        End If
        i += 1
        If Not item.X_OCCUPATION Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_OCCUPATION)
        End If
        i += 1
        If Not item.X_POSITION Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_POSITION)
        End If
        i += 1
        If Not item.X_REFERENCE_ADDRESS Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_REFERENCE_ADDRESS)
        End If
        i += 1
        If Not item.X_TYPE_DOCUMENT Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_TYPE_DOCUMENT)
        End If
        i += 1
        If Not item.X_ZIPCODE Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_ZIPCODE)
        End If
        i += 1
        If Not item.X_ICCID Is Nothing Then
            arrParam(i).Value = CheckStr(item.X_ICCID)
        End If

        objRequest.CommandType = CommandType.StoredProcedure
        objRequest.Command = "PCK_INTERACT_CLFY.SP_CREATE_PLUS_INTER"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        vInteraccionId = CheckStr(CType(objRequest.Parameters(112), IDataParameter).Value)
        rFlagInsercion = CheckStr(CType(objRequest.Parameters(113), IDataParameter).Value)
        rMsgText = CheckStr(CType(objRequest.Parameters(114), IDataParameter).Value)

        Catch ex As Exception
            vInteraccionId = ""
            rFlagInsercion = "NO OK"
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_INActChip/clsTipificacion.vb; Function: InsertarPlantillaInteraccion)"
            rMsgText = ex.Message & MaptPath
            'FIN PROY-140126


        End Try

        InsertarPlantillaInteraccion = rFlagInsercion

    End Function

    Public Function CrearInteraccion(ByVal item As Interaccion, _
                                        ByRef vInteraccionId As String, _
                                        ByRef rFlagInsercion As String, _
                                        ByRef rMsgText As String) As String

        Try
        strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

        Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_CONTACTOBJID_1", DbType.Int64, 0, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SITEOBJID_1", DbType.Int64, 0, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_ACCOUNT", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_PHONE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_TIPO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SUBCLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_METODO_CONTACTO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_TIPO_INTER", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_AGENTE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_USR_PROCESO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_HECHO_EN_UNO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_NOTAS", DbType.String, 5000, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_FLAG_CASO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_RESULTADO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("ID_INTERACCION", DbType.String, 255, "", ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("FLAG_CREACION", DbType.String, 255, "", ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("MSG_TEXT", DbType.String, 255, "", ParameterDirection.Output)}

        For j As Integer = 0 To arrParam.Length - 1
            arrParam(j).Value = System.DBNull.Value
        Next
        Dim i As Integer = 0
        If Not item.OBJID_CONTACTO Is Nothing Then
            arrParam(i).Value = CheckInt64(item.OBJID_CONTACTO)
        End If
        i += 1
        If Not item.OBJID_SITE Is Nothing Then
            arrParam(i).Value = CheckInt64(item.OBJID_SITE)
        End If
        i += 1
        If Not item.CUENTA Is Nothing Then
            arrParam(i).Value = item.CUENTA
        End If
        i += 1
        If Not item.TELEFONO Is Nothing Then
            arrParam(i).Value = item.TELEFONO
        End If
        i += 1
        If Not item.TIPO Is Nothing Then
            arrParam(i).Value = item.TIPO
        End If
        i += 1
        If Not item.CLASE Is Nothing Then
            arrParam(i).Value = item.CLASE
        End If
        i += 1
        If Not item.SUBCLASE Is Nothing Then
            arrParam(i).Value = item.SUBCLASE
        End If
        i += 1
        If Not item.METODO Is Nothing Then
            arrParam(i).Value = item.METODO
        End If
        i += 1
        If Not item.TIPO_INTER Is Nothing Then
            arrParam(i).Value = item.TIPO_INTER
        End If
        i += 1
        If Not item.AGENTE Is Nothing Then
            arrParam(i).Value = item.AGENTE
        End If
        i += 1
        If Not item.USUARIO_PROCESO Is Nothing Then
            arrParam(i).Value = item.USUARIO_PROCESO
        End If
        i += 1
        If Not item.HECHO_EN_UNO Is Nothing Then
            arrParam(i).Value = item.HECHO_EN_UNO
        End If
        i += 1
        If Not item.NOTAS Is Nothing Then
            arrParam(i).Value = item.NOTAS
        End If
        i += 1
        If Not item.FLAG_CASO Is Nothing Then
            arrParam(i).Value = item.FLAG_CASO
        End If
        i += 1
        If Not item.RESULTADO Is Nothing Then
            arrParam(i).Value = item.RESULTADO
        End If

        objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SA.PCK_INTERACT_CLFY.SP_CREATE_INTERACT"
        objRequest.Parameters.AddRange(arrParam)
        objRequest.Factory.ExecuteNonQuery(objRequest)

        vInteraccionId = CheckStr(CType(objRequest.Parameters(15), IDataParameter).Value)
        rFlagInsercion = CheckStr(CType(objRequest.Parameters(16), IDataParameter).Value)
        rMsgText = CheckStr(CType(objRequest.Parameters(17), IDataParameter).Value)

        Catch ex As Exception
            vInteraccionId = ""
            rFlagInsercion = "NO OK"
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Recaudacion\clsConsultas.vb; Function: CrearInteraccion)"
            rMsgText = ex.Message & MaptPath
            'FIN PROY-140126

        End Try

        CrearInteraccion = rFlagInsercion

    End Function
    'PROY-26366-IDEA-34247 FASE 1 - INICIO
    Public Function CrearInteraccionDetalle(ByVal item As Interaccion, _
                                    ByRef vInteraccionId As String, _
                                    ByRef rFlagInsercion As String, _
                                    ByRef rMsgText As String) As String

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("inCASO", DbType.String, 255, "",ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inINTERACT_ID", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inTIPO_CODE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inCLASE_CODE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inSUBCLASE_CODE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inSERVAFECT_CODE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inINCONVEN_CODE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inTIPO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inCLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inSUBCLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inSERVAFECT", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inINCONVEN", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("inUSUARIO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("ouCOD_ERR", DbType.Int32, 5000, "", ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("ouMSG_ERR", DbType.String, 255, "", ParameterDirection.Output)}



            For j As Integer = 0 To arrParam.Length - 1
                arrParam(j).Value = System.DBNull.Value
            Next

            Dim i As Integer = 0
            If Not item.Caso Is Nothing Then
                arrParam(i).Value = CheckInt64(item.Caso)
            End If
            i += 1
            If Not item.ID_INTERACCION Is Nothing Then
                arrParam(i).Value = CheckInt64(item.ID_INTERACCION)
            End If
            i += 1
            If Not item.TIPO_CODIGO Is Nothing Then
                arrParam(i).Value = item.TIPO_CODIGO

            End If
            i += 1
            If Not item.CLASE_CODIGO Is Nothing Then
                arrParam(i).Value = item.CLASE_CODIGO

            End If
            i += 1
            If Not item.SUBCLASE_CODIGO Is Nothing Then
                arrParam(i).Value = item.SUBCLASE_CODIGO
            End If
            i += 1
            If Not item.SERVAFECT_CODE Is Nothing Then
                arrParam(i).Value = item.SERVAFECT_CODE
            End If
            i += 1
            If Not item.INCONVEN_CODE Is Nothing Then
                arrParam(i).Value = item.INCONVEN_CODE
            End If
            i += 1
            If Not item.TIPO Is Nothing Then
                arrParam(i).Value = item.TIPO
            End If
            i += 1
            If Not item.CLASE Is Nothing Then
                arrParam(i).Value = item.CLASE
            End If
            i += 1
            If Not item.SUBCLASE Is Nothing Then
                arrParam(i).Value = item.SUBCLASE
            End If
            i += 1
            If Not item.SERVAFECT Is Nothing Then
                arrParam(i).Value = item.SERVAFECT
            End If
            i += 1
            If Not item.INCONVEN Is Nothing Then
                arrParam(i).Value = item.INCONVEN
            End If
            i += 1
            If Not item.USUARIO_PROCESO Is Nothing Then
                arrParam(i).Value = item.USUARIO_PROCESO
            End If


            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SA.PCK_CASE_CLFY.SP_INS_DET_INTERACCION"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            vInteraccionId = CheckStr(CType(objRequest.Parameters(13), IDataParameter).Value)
            rFlagInsercion = CheckStr(CType(objRequest.Parameters(14), IDataParameter).Value)

        Catch ex As Exception
            vInteraccionId = ""
            rFlagInsercion = "NO OK"
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Recaudacion\clsConsultas.vb; Function: CrearInteraccionDetalle)"
            rMsgText = ex.Message & MaptPath
            'FIN PROY-140126

        End Try

        CrearInteraccionDetalle = rFlagInsercion

    End Function
    'PROY-26366-IDEA-34247 FASE 1 - FIN

    Public Function CheckStr(ByVal value As Object) As String

        Dim salida As String = ""
        If IsNothing(value) Or IsDBNull(value) Then
            salida = ""
        Else
            salida = value.ToString()
        End If

        Return salida.Trim()
    End Function

    Public Function CheckDbl(ByVal value As Object) As Double
        Dim salida As Double = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToDouble(value)
            End If
        End If

        Return salida
    End Function

    Public Function CheckDate(ByVal value As Object) As DateTime
        Dim salida As DateTime
        If IsNothing(value) Or IsDBNull(value) Then
            salida = DateTime.Now
        Else
            salida = Convert.ToDateTime(value)
        End If

        Return salida
    End Function

    Public Function CheckInt64(ByVal value As Object) As Int64
        Dim salida As Int64 = 0
        If IsNothing(value) Or IsDBNull(value) Then
            salida = 0
        Else
            If Convert.ToString(value) = "" Then
                salida = 0
            Else
                salida = Convert.ToInt64(value)
            End If
        End If

        Return salida
    End Function

    Public Function CrearInteraccionHFC(ByVal item As Interaccion, _
                                        ByRef vInteraccionId As String, _
                                        ByRef rFlagInsercion As String, _
                                        ByRef rMsgText As String) As String

        Try
            strCadenaConexion = objSeg.FP_GetConnectionString("2", "PVU_CLARIFY")

            Dim objRequest As New DAAB.DAABRequest(DAAB.DAABRequest.TipoOrigenDatos.ORACLE, strCadenaConexion)
            Dim arrParam() As DAAB.DAABRequest.Parameter = {New DAABRequest.Parameter("P_CONTACTOBJID_1", DbType.Int64, 0, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SITEOBJID_1", DbType.Int64, 0, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_ACCOUNT", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_PHONE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_TIPO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SUBCLASE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_METODO_CONTACTO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_TIPO_INTER", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_AGENTE", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_USR_PROCESO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_HECHO_EN_UNO", DbType.Int64, 0, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_NOTAS", DbType.String, 4000, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_FLAG_CASO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_RESULTADO", DbType.String, 255, "", ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SERVAFECT", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_INCONVEN", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_SERVAFECT_CODE", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_INCONVEN_CODE", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_CO_ID", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_COD_PLANO", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_VALOR1", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("P_VALOR2", DbType.String, 255, ParameterDirection.Input), _
                                                            New DAABRequest.Parameter("ID_INTERACCION", DbType.String, 255, "", ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("FLAG_CREACION", DbType.String, 255, "", ParameterDirection.Output), _
                                                            New DAABRequest.Parameter("MSG_TEXT", DbType.String, 255, "", ParameterDirection.Output)}

            For j As Integer = 0 To arrParam.Length - 1
                arrParam(j).Value = System.DBNull.Value
            Next
            Dim i As Integer = 0
            If Not item.OBJID_CONTACTO Is Nothing Then
                arrParam(i).Value = CheckInt64(item.OBJID_CONTACTO)
            End If
            i += 1
            If Not item.OBJID_SITE Is Nothing Then
                arrParam(i).Value = CheckInt64(item.OBJID_SITE)
            End If
            i += 1
            If Not item.CUENTA Is Nothing Then
                arrParam(i).Value = item.CUENTA
            End If
            i += 1
            If Not item.TELEFONO Is Nothing Then
                arrParam(i).Value = item.TELEFONO
            End If
            i += 1
            If Not item.TIPO Is Nothing Then
                arrParam(i).Value = item.TIPO
            End If
            i += 1
            If Not item.CLASE Is Nothing Then
                arrParam(i).Value = item.CLASE
            End If
            i += 1
            If Not item.SUBCLASE Is Nothing Then
                arrParam(i).Value = item.SUBCLASE
            End If
            i += 1
            If Not item.METODO Is Nothing Then
                arrParam(i).Value = item.METODO
            End If
            i += 1
            If Not item.TIPO_INTER Is Nothing Then
                arrParam(i).Value = item.TIPO_INTER
            End If
            i += 1
            If Not item.AGENTE Is Nothing Then
                arrParam(i).Value = item.AGENTE
            End If
            i += 1
            If Not item.USUARIO_PROCESO Is Nothing Then
                arrParam(i).Value = item.USUARIO_PROCESO
            End If
            i += 1
            If Not item.HECHO_EN_UNO Is Nothing Then
                arrParam(i).Value = item.HECHO_EN_UNO
            End If
            i += 1
            If Not item.NOTAS Is Nothing Then
                arrParam(i).Value = item.NOTAS
            End If
            i += 1
            If Not item.FLAG_CASO Is Nothing Then
                arrParam(i).Value = item.FLAG_CASO
            End If
            i += 1
            If Not item.RESULTADO Is Nothing Then
                arrParam(i).Value = item.RESULTADO
            End If
            i += 1
            If Not item.SERVAFECT Is Nothing Then
                arrParam(i).Value = item.SERVAFECT
            End If
            i += 1
            If Not item.INCONVEN Is Nothing Then
                arrParam(i).Value = item.INCONVEN
            End If
            i += 1
            If Not item.SERVAFECT_CODE Is Nothing Then
                arrParam(i).Value = item.SERVAFECT_CODE
            End If
            i += 1
            If Not item.INCONVEN_CODE Is Nothing Then
                arrParam(i).Value = item.INCONVEN_CODE
            End If
            i += 1
            If Not item.CO_ID Is Nothing Then
                arrParam(i).Value = item.CO_ID
            End If
            i += 1
            If Not item.COD_PLANO Is Nothing Then
                arrParam(i).Value = item.COD_PLANO
            End If
            i += 1
            If Not item.VALOR1 Is Nothing Then
                arrParam(i).Value = item.VALOR1
            End If
            i += 1
            If Not item.VALOR2 Is Nothing Then
                arrParam(i).Value = item.VALOR2
            End If

            objRequest.CommandType = CommandType.StoredProcedure
            objRequest.Command = "SA.PCK_INTERACT_CLFY_HFC.SP_CREATE_INTERACT_HFC"
            objRequest.Parameters.AddRange(arrParam)
            objRequest.Factory.ExecuteNonQuery(objRequest)

            vInteraccionId = CheckStr(CType(objRequest.Parameters(23), IDataParameter).Value)
            rFlagInsercion = CheckStr(CType(objRequest.Parameters(24), IDataParameter).Value)
            rMsgText = CheckStr(CType(objRequest.Parameters(25), IDataParameter).Value) & CheckStr(strCadenaConexion)

        Catch ex As Exception
            vInteraccionId = ""
            rFlagInsercion = "NO OK"
            'Inicio PROY-140126
            Dim MaptPath As String
            MaptPath = "( Class : COM_SIC_Recaudacion\clsConsultas.vb; Function: CrearInteraccionHFC)"
            rMsgText = ex.Message & MaptPath & CheckStr(strCadenaConexion)
            'FIN PROY-140126

        End Try

        CrearInteraccionHFC = rFlagInsercion

    End Function

End Class
