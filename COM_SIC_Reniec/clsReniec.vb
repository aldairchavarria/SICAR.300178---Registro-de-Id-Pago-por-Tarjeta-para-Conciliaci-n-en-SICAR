Public Class clsReniec

    Private strCodSession As String
    Private intCompCode As Integer
    Private intErrCode As Integer
    Private strMsgErr As String
    Private Const k_lstrNombMod = "clsTxReniec"

    Public Function FP_ExtraerSession() As String
        FP_ExtraerSession = strCodSession
    End Function

    Public Function FP_CodigoError() As Integer
        FP_CodigoError = intErrCode
    End Function

    Public Function FP_MensajeError() As String
        FP_MensajeError = strMsgErr
    End Function

    Public Function FP_OrigenError() As Integer
        FP_OrigenError = intCompCode
    End Function

    Public Function FP_ExtraerConsolidado(ByVal pvTDOC As String, _
                                        ByVal pvNDoc As String, _
                                        ByVal pvUnidadNegocio As String, _
                                        ByVal pvCentroCosto As String, _
                                        ByVal pvCodigoCanal As String, _
                                        ByVal pvUsuarioAPP As String, _
                                        ByVal pvPasswdAPP As String, _
                                        ByVal pvUsuarioAcc As String, _
                                        ByVal pvCodSession As String, _
                                        ByVal pvLogPath As String, _
                                        ByVal pvLogLevel As String) As Object
        Dim objAdmRENIEC As Object 'NovaSR.clsAdmRENIEC
        Dim objRENDatos As Object 'clsRENDatos
        Dim intResultado As Integer
        Dim objDato As Collection

        Try

            objAdmRENIEC = CreateObject("NovaSR.clsAdmRENIEC")

            objAdmRENIEC.LogPath = pvLogPath
            objAdmRENIEC.LogLevel = pvLogLevel

            intResultado = objAdmRENIEC.gfi_ExtraerConsolidado(pvTDOC, pvNDoc, pvUnidadNegocio, pvCentroCosto, pvCodigoCanal, pvUsuarioAPP, pvPasswdAPP, pvUsuarioAcc, pvCodSession)

            If (intResultado = 0) Then

                strCodSession = pvCodSession

                objRENDatos = objAdmRENIEC.Consolidado

                objDato = New Collection

                objDato.Add(0, "CodigoError")
                objDato.Add("", "MensajeError")

                objDato.Add(objRENDatos.NumDoc, "NumDoc")
                objDato.Add(objRENDatos.ChrVerifica, "ChrVerifica")
                objDato.Add(objRENDatos.TipoDoc, "TipoDoc")
                objDato.Add(objRENDatos.NumLibro, "NumLibro")
                objDato.Add(objRENDatos.ApePaterno, "ApePaterno")
                objDato.Add(objRENDatos.ApeMaterno, "ApeMaterno")
                objDato.Add(objRENDatos.Nombres, "Nombres")
                objDato.Add(objRENDatos.Departamento, "Departamento")
                objDato.Add(objRENDatos.Provincia, "Provincia")
                objDato.Add(objRENDatos.Distrito, "Distrito")
                objDato.Add(objRENDatos.Localidad, "Localidad")
                objDato.Add(objRENDatos.Direccion, "Direccion")

                objDato.Add(objRENDatos.EstadoCivil, "EstadoCivil")
                objDato.Add(objRENDatos.GradoInstruccion, "GradoInstruccion")
                objDato.Add(objRENDatos.AnioEstudio, "AnioEstudio")
                objDato.Add(objRENDatos.Estatura, "Estatura")
                objDato.Add(objRENDatos.Sexo, "Sexo")

                objDato.Add(objRENDatos.DepartamentoNac, "DepartamentoNac")
                objDato.Add(objRENDatos.ProvinciaNac, "ProvinciaNac")
                objDato.Add(objRENDatos.DistritoNac, "DistritoNac")
                objDato.Add(objRENDatos.LocalidadNac, "LocalidadNac")
                objDato.Add(objRENDatos.FechaNac, "FechaNac")

                objDato.Add(objRENDatos.NombrePadre, "NombrePadre")
                objDato.Add(objRENDatos.NombreMadre, "NombreMadre")
                objDato.Add(objRENDatos.FechaInscripcion, "FechaInscripcion")
                objDato.Add(objRENDatos.FechaExpedicion, "FechaExpedicion")

                objDato.Add(objRENDatos.ConstanciaVotacion, "ConstanciaVotacion")
                objDato.Add(objRENDatos.Restricciones, "Restricciones")


                'On Error Resume Next
                objDato.Add(objRENDatos.Foto, "Foto")
                'On Error Resume Next
                objDato.Add(objRENDatos.Firma, "Firma")

                objRENDatos = Nothing

                FP_ExtraerConsolidado = objDato

            Else
                'intErrCode = objAdmRENIEC.ErrCode
                'strMsgErr = objAdmRENIEC.MsgErr
                'intCompCode = objAdmRENIEC.CompCode

                objDato = New Collection
                objDato.Add(objAdmRENIEC.ErrCode, "CodigoError")
                objDato.Add(objAdmRENIEC.MsgErr, "MensajeError")

                FP_ExtraerConsolidado = objDato


            End If

            objAdmRENIEC = Nothing
        Catch
            objRENDatos = Nothing
            objAdmRENIEC = Nothing
            FP_ExtraerConsolidado = Nothing
        End Try

    End Function

    Public Function FP_ExtraerAproximacion(ByVal pvApPaterno As String, _
                                        ByVal pvApMaterno As String, _
                                        ByVal pvNombre As String, _
                                        ByVal pvRegSol As Integer, _
                                        ByVal pvOffSet As Integer, _
                                        ByVal pvUnidadNegocio As String, _
                                        ByVal pvCentroCosto As String, _
                                        ByVal pvCodigoCanal As String, _
                                        ByVal pvUsuarioAPP As String, _
                                        ByVal pvPasswdAPP As String, _
                                        ByVal pvUsuarioAcc As String, _
                                        ByVal pvCodSession As String, _
                                        ByVal pvLogPath As String, _
                                        ByVal pvLogLevel As String) As Object
        Dim objAdmRENIEC As Object 'NovaSR.clsAdmRENIEC
        Dim objRENAproxima As Object 'clsRENAproxima
        Dim intResultado As Integer
        Dim intIndice As Integer
        Dim objColumna As Collection
        Dim objRegistro As Collection
        Dim objValores As Collection

        Try

            objAdmRENIEC = CreateObject("NovaSR.clsAdmRENIEC")

            objAdmRENIEC.LogPath = pvLogPath
            objAdmRENIEC.LogLevel = pvLogLevel

            intResultado = objAdmRENIEC.gfi_ExtraerAproximacion(pvApPaterno, pvApMaterno, pvNombre, pvRegSol, pvOffSet, pvUnidadNegocio, pvCentroCosto, pvCodigoCanal, pvUsuarioAPP, pvPasswdAPP, pvUsuarioAcc, pvCodSession)

            If (intResultado = 0) Then

                strCodSession = pvCodSession

                objRENAproxima = objAdmRENIEC.Aproximacion

                objValores = New Collection

                objValores.Add(0, "CodigoError")
                objValores.Add("", "MensajeError")

                objValores.Add(objRENAproxima.RegTotal, "RegTotal")
                objValores.Add(objRENAproxima.RegEnviados, "RegEnviados")

                objRegistro = New Collection

                For intIndice = 1 To objRENAproxima.RegEnviados

                    objColumna = New Collection

                    objColumna.Add(objRENAproxima.Item(intIndice).NumDoc, "NumDoc")
                    objColumna.Add(objRENAproxima.Item(intIndice).ApePaterno, "ApePaterno")
                    objColumna.Add(objRENAproxima.Item(intIndice).ApeMaterno, "ApeMaterno")
                    objColumna.Add(objRENAproxima.Item(intIndice).Nombres, "Nombres")

                    objRegistro.Add(objColumna)
                Next

                objValores.Add(objRegistro, "Registro")

                objRENAproxima = Nothing

                FP_ExtraerAproximacion = objValores


            Else
                'intErrCode = objAdmRENIEC.ErrCode
                'strMsgErr = objAdmRENIEC.MsgErr
                'intCompCode = objAdmRENIEC.CompCode

                objValores = New Collection
                objValores.Add(objAdmRENIEC.ErrCode, "CodigoError")
                objValores.Add(objAdmRENIEC.MsgErr, "MensajeError")

                FP_ExtraerAproximacion = objValores


            End If

            objAdmRENIEC = Nothing
            Exit Function
        Catch

            objRENAproxima = Nothing
            objAdmRENIEC = Nothing
            FP_ExtraerAproximacion = Nothing
        End Try
    End Function

End Class
