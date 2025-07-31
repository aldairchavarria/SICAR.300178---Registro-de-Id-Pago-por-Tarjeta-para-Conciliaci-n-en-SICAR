Public Class clsComDemo


    Public Function Get_ConsultaPoolFactura( _
                    ByVal OficinaVenta As String, ByVal FechaVenta As String, _
                    ByVal TipoPool As String, Optional ByVal FechaHasta As String = "", _
                    Optional ByVal NroDocCliente As String = "", _
                    Optional ByVal TipoDocCliente As String = "", _
                    Optional ByVal NumRegistros As String = "", _
                    Optional ByVal MostrarPagina As String = "") As DataSet

        Const K_PSTRMETODO = "Get_ConsultaPoolFactura"
        ' Dim ObjRespRsHistorico As ADODB.Recordset
        Dim StrTempRs

        Dim PaginaMostrar As Integer
        Dim ParPaginado As Boolean
        Dim NumTotalPaginas As Integer
        Dim ComponentName As String = "PVU.DCOM_IMPRESION.1"
        Dim CantidadRegistros As Integer
        Dim ApplicationName As String = "PVU"

        Try

            'Paso 1 :   Declarar los objetos con para llamar al RFC
            Dim objRFC1 As Object
            objRFC1 = CreateObject(ComponentName)     ' colocar el nombre de la clase de interfaz de cominucacion con SAP

            'Dim objRFC1 As DESRfcsDcomPvuLib.DCOM_PVU
            'Set objRFC1 = New DESRfcsDcomPvuLib.DCOM_PVU

            'Paso 2 :   Definir tanto objetos RecordSet como estructuras maneje el RFC
            Dim objRsFacturas As DataTable
            Dim objRsReturn As DataTable

            'Paso 3 :   Obtener la conexión hacia SAP con los parámetros
            If FP_ConectaSAP(objRFC1, ApplicationName) Then
                FechaVenta = FormatValorIngreso("VINGFORMAT_FECHA", FechaVenta)
                FechaHasta = FormatValorIngreso("VINGFORMAT_FECHA", FechaHasta)

                'Paso 4 :   Obtiene las estructuras de todos los RecordSet que intervienen en el RFC
                objRFC1.DimAs("ZPVU_RFC_TRS_POOL_FACTURAS", "TI_FACTURAS", objRsFacturas)
                objRFC1.DimAs("ZPVU_RFC_TRS_POOL_FACTURAS", "TI_RETURN", objRsReturn)

                If Trim(FechaHasta) = "" Then
                    FechaHasta = Format(Now, "DD/MM/YYYY")
                End If

                'Paso 5 :   Se ejecuta el RFC con los parámetros indicados
                objRFC1.Zpvu_Rfc_Trs_Pool_Facturas(objRsFacturas, FechaHasta, FechaVenta, _
                                NroDocCliente, OficinaVenta, TipoDocCliente, _
                                TipoPool, objRsReturn)

                If Trim(NumRegistros) = "" Then
                    ParPaginado = False
                Else
                    ParPaginado = True
                    If Trim(MostrarPagina) = "" Or Not IsNumeric(MostrarPagina) Or Trim(MostrarPagina) = "0" Then
                        PaginaMostrar = 1
                    Else
                        PaginaMostrar = MostrarPagina
                    End If

                    If IsNumeric(NumRegistros) Then
                        CantidadRegistros = CInt(NumRegistros)
                    End If
                End If
                NumTotalPaginas = -1
                'Dim ObjRsRegistro As ADODB.Recordset
                'StrTempRs = RecordsetTOXml(objRsFacturas, "RS01")
                ParPaginado = False
                'ObjRsRegistro = New ADODB.Recordset
                'ObjRsRegistro.Fields.Append("TotalPaginas", adInteger)
                'ObjRsRegistro.Open()
                'ObjRsRegistro.AddNew("TotalPaginas", NumTotalPaginas)

                'Paso 6 : Capturar Parametros de salida (OutPuts)

                'Paso 7 :   Se obtiene una cadena del tipo XML con todos los RecordSet que se han generado

                Get_ConsultaPoolFactura.Tables.Add(objRsFacturas)
                Get_ConsultaPoolFactura.Tables.Add(objRsReturn)

            End If

        Catch ex As Exception
            'App.LogEvent("Error : " & Err.Number & " Description : " & Err.Description, vbLogEventTypeInformation)
            'Get_ConsultaPoolFactura = ReturnFail(Err.Number, Err.Description, "Get_ConsultaPoolFactura")

        End Try
    End Function






    Private Sub FP_GetConnectionString( _
                        ByRef strcliente As String, _
                        ByRef strUsuario As String, _
                        ByRef strPassword As String, _
                        ByRef strLanguage As String, _
                        ByRef strApplicationServer As String, _
                        ByVal strObjITSAP As String)
        Dim ObjUser As Object
        Dim strCadena As String

        ObjUser = CreateObject("Seguridad_Test_UTL.clsSeguridad")

        strCadena = ObjUser.BaseDatos(strObjITSAP)
        strcliente = ObjUser.BaseDatos(strObjITSAP)
        strUsuario = ObjUser.Usuario(strObjITSAP)
        strPassword = ObjUser.Password(strObjITSAP)
        strLanguage = "ES" 'ObjUser.Proveedor(strObjITSAP)
        strApplicationServer = "TIMPVU" 'ObjUser.Servidor(strObjITSAP)
    End Sub

    Public Function FP_ConectaSAP(ByRef ObjRfc As Object, ByVal strObjITSAP As String) As Boolean
        Dim strcliente As String, strUsuario As String, strPassword As String
        Dim strLanguage As String, strApplicationServer As String

        Try
            Call FP_GetConnectionString(strcliente, strUsuario, strPassword, strLanguage, strApplicationServer, strObjITSAP)

            ObjRfc.PutSessionInfo(strApplicationServer, strUsuario, strPassword, strLanguage, strcliente)
            If Trim(strUsuario) = "" Then
                FP_ConectaSAP = False
            Else
                FP_ConectaSAP = True
            End If
        Catch ex As Exception
            FP_ConectaSAP = False
        End Try

    End Function

    Public Function FormatValorIngreso(ByVal NombreCampo As String, ByVal Valor As String) As String
        'If TipoDato = adInteger Then
        Select Case UCase(NombreCampo)

            Case "VINGFORMAT_NRO" : FormatValorIngreso = IIf(Trim(Valor) = "", 0, Valor)

            Case "VINGFORMAT_FECHA"
                If Not IsDate(Trim(Valor)) Or Trim(Valor) = "" Or InStr(1, Valor, ".") > 0 Or Valor = "12:00:00 AM" Or Not (InStr(1, Valor, "/") > 0) Then
                    FormatValorIngreso = "12:00:00 AM" 'Format("12 AM", "HH:MM:SS ampm")
                Else
                    FormatValorIngreso = Mid(Trim(Valor), 7, 4) & "/" & Mid(Trim(Valor), 4, 2) & "/" & Mid(Trim(Valor), 1, 2)
                End If

            Case Else : FormatValorIngreso = CStr(Valor)
        End Select
        'End If
    End Function

End Class
