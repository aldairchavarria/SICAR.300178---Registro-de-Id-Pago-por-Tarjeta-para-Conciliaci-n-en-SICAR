Imports System.Configuration
Public Class clsDatosPostpagoNegocios

    Dim _DatosPostpago As New ConsultaPostpagoWS.ConsultasPostpagoWSService
    
    Dim objFileLog As New SICAR_Log
    Dim nameFilePos As String = ConfigurationSettings.AppSettings("constNameLogPOS")
    Dim pathFilePos As String = ConfigurationSettings.AppSettings("constRutaLogPOS")
    Dim strArchivoPos As String = objFileLog.Log_CrearNombreArchivo(nameFilePos)
    
    Public Function LeerDatosCliente(ByVal nroTelefono As String, ByVal custcode As String, ByVal usuarioaplicacion As String, ByRef MensajeError As String) As clsClienteBSCS

        _DatosPostpago.Url = ConfigurationSettings.AppSettings("RutaWS_DatosPostpago").ToString()
        _DatosPostpago.Credentials = System.Net.CredentialCache.DefaultCredentials
        _DatosPostpago.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("TimeoutWS").ToString())

        Dim item As clsClienteBSCS = Nothing

        Try
            Dim objPostpagoResponse As New ConsultaPostpagoWS.datosClienteResponse
            Dim objCliente As New ConsultaPostpagoWS.objetoClienteType

            
            Dim auditoriaWS = New ConsultaPostpagoWS.auditRequestType
            auditoriaWS.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            auditoriaWS.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            auditoriaWS.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            auditoriaWS.usuarioAplicacion = usuarioaplicacion


            Dim objdatosClienteRequest As New ConsultaPostpagoWS.datosClienteRequest
            objdatosClienteRequest.custcode = custcode
            objdatosClienteRequest.dnnum = nroTelefono
            objdatosClienteRequest.auditRequest = auditoriaWS
            objPostpagoResponse = _DatosPostpago.consultarDatosCliente(objdatosClienteRequest)
            
            '''''''''''''''''''''''''''''''''''''INC000002156122'''''''''''''''''''''''''''''''''''''''''''
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "----------------------inicio log fallas INC000002156122 ------------------ ")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "request LeerDatosCliente")
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "auditoriaWS.idTransaccion : " & auditoriaWS.idTransaccion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "auditoriaWS.ipAplicacion  : " & auditoriaWS.ipAplicacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "auditoriaWS.nombreAplicacion: " & auditoriaWS.nombreAplicacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "auditoriaWS.usuarioAplicacion : " & auditoriaWS.usuarioAplicacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.custcode : " & objdatosClienteRequest.custcode)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.dnnum  : " & objdatosClienteRequest.dnnum)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.auditRequest.idTransaccion  : " & objdatosClienteRequest.auditRequest.idTransaccion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.auditRequest.ipAplicacion  : " & objdatosClienteRequest.auditRequest.ipAplicacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.auditRequest.nombreAplicacion  : " & objdatosClienteRequest.auditRequest.nombreAplicacion)
            objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "objdatosClienteRequest.auditRequest.usuarioAplicacion  : " & objdatosClienteRequest.auditRequest.usuarioAplicacion)
            ''''''''''''''''''''''''''''''''''''''INC000002156122''''''''''''''''''''''''''''''''''''''''''

            objCliente = objPostpagoResponse.datosCliente.listaDatosClientes(0) '.datosCliente(0)

            If Funciones.CheckStr(objCliente.customerId) <> "" Then

                item = New clsClienteBSCS

                item.customerId = objCliente.customerId
                item.cuenta = objCliente.cuenta
                item.Nombre = objCliente.nombre
                item.apellidos = objCliente.apellidos
                item.razonSocial = objCliente.razonSocial
                item.tip_doc = objCliente.tip_doc
                item.num_doc = objCliente.num_doc
                item.titulo = objCliente.titulo
                item.telef_principal = objCliente.telef_principal
                item.estado_civil = objCliente.estado_civil
                item.fecha_nac = objCliente.fecha_nac
                item.lug_nac = objCliente.lug_nac
                item.ruc_dni = objCliente.ruc_dni
                item.nomb_comercial = objCliente.nomb_comercial
                item.contacto_cliente = objCliente.contacto_cliente
                item.rep_legal = objCliente.rep_legal
                item.telef_contacto = objCliente.telef_contacto
                item.fax = objCliente.fax
                item.email = objCliente.email
                item.cargo = objCliente.cargo
                item.asesor = objCliente.asesor
                item.direccion_fac = objCliente.direccion_fac
                item.urbanizacion_fac = objCliente.urbanizacion_fac
                item.distrito_fac = objCliente.distrito_fac
                item.provincia_fac = objCliente.provincia_fac
                item.cod_postal_fac = objCliente.cod_postal_fac
                item.departamento_fac = objCliente.departamento_fac
                item.pais_fac = objCliente.pais_fac
                item.direccion_leg = objCliente.direccion_leg
                item.urbanizacion_leg = objCliente.urbanizacion_leg
                item.distrito_leg = objCliente.distrito_leg
                item.provincia_leg = objCliente.provincia_leg
                item.cod_postal_leg = objCliente.cod_postal_leg
                item.departamento_leg = objCliente.departamento_leg
                item.pais_leg = objCliente.pais_leg
                item.co_id = objCliente.co_id
                item.nicho_id = objCliente.nicho_id
                item.num_cuentas = objCliente.num_cuentas
                item.num_lineas = objCliente.num_lineas
                item.ciclo_fac = objCliente.ciclo_fac
                item.status_cuenta = objCliente.status_cuenta
                item.modalidad = objCliente.modalidad
                item.tipo_cliente = objCliente.tipo_cliente
                item.fecha_act = objCliente.fecha_act
                item.limite_credito = objCliente.limite_credito
                item.segmento = objCliente.segmento
                item.respon_pago = objCliente.respon_pago
                item.credit_score = objCliente.credit_score
                item.forma_pago = objCliente.forma_pago
                item.codigo_tipo_cliente = objCliente.codigo_tipo_cliente
                item.sexo = objCliente.sexo
                item.nacionalidad = objCliente.nacionalidad
                item.consultor = objCliente.consultor

                ''''''''''''''''''''''''''''''''INC000002156122'''''''''''''''''''''''''''''''''''
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "response LeerDatosCliente")
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.customerId: " & item.customerId)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.cuenta  : " & item.cuenta)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.Nombre : " & item.Nombre)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.apellidos : " & item.apellidos)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.razonSocial : " & item.razonSocial)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.tip_doc : " & item.tip_doc)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.num_doc : " & item.num_doc)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.titulo: " & item.titulo)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.telef_principal : " & item.telef_principal)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.estado_civil : " & item.estado_civil)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.fecha_nac : " & item.fecha_nac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.lug_nac : " & item.lug_nac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.ruc_dni : " & item.ruc_dni)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.nomb_comercial : " & item.nomb_comercial)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.contacto_cliente : " & item.contacto_cliente)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.rep_legal : " & item.rep_legal)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.telef_contacto : " & item.telef_contacto)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.fax : " & item.fax)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.email : " & item.email)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.cargo : " & item.cargo)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.asesor : " & item.asesor)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.direccion_fac : " & item.direccion_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.urbanizacion_fac : " & item.urbanizacion_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.distrito_fac : " & item.distrito_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.provincia_fac : " & item.provincia_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.cod_postal_fac  : " & item.cod_postal_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.departamento_fac : " & item.departamento_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.pais_fac : " & item.pais_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.direccion_leg : " & item.direccion_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.urbanizacion_leg : " & item.urbanizacion_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.distrito_leg : " & item.distrito_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.provincia_leg : " & item.provincia_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.cod_postal_leg : " & item.cod_postal_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.departamento_leg : " & item.departamento_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.pais_leg : " & item.pais_leg)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.co_id : " & item.co_id)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.nicho_id : " & item.nicho_id)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.num_cuentas : " & item.num_cuentas)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.num_lineas : " & item.num_lineas)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.ciclo_fac : " & item.ciclo_fac)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.status_cuenta : " & item.status_cuenta)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.modalidad : " & item.modalidad)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.tipo_cliente : " & item.tipo_cliente)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.fecha_act : " & item.fecha_act)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.limite_credito : " & item.limite_credito)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.segmento : " & item.segmento)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.respon_pago : " & item.respon_pago)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.credit_score : " & item.credit_score)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.forma_pago : " & item.forma_pago)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.codigo_tipo_cliente : " & item.codigo_tipo_cliente)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.sexo : " & item.sexo)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.nacionalidad : " & item.nacionalidad)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "item.consultor : " & item.consultor)
                objFileLog.Log_WriteLog(pathFilePos, strArchivoPos, "----------------------fin log fallas INC000002156122 ------------------ ")
                '''''''''''''''''''''''''''''''''''INC000002156122''''''''''''''''''''''''''''''''

            Else
                MensajeError = "El número de teléfono no es Postpago."

            End If


        Catch ex As Exception
            MensajeError = "El servicio esta temporalmente fuera de servicio. " + ex.Message
        End Try
        Return item

    End Function
    'PROY 26210 EGSC BEGIN 
    'Se agrego metodo que busca los datos del contacto. Copiado de WEB_SISACT_EXPRESS
    Public Function DatosContrato(ByVal p_codid As Integer, ByRef MensajeError As String, ByVal usuarioaplicacion As String) As Contrato

        Dim item As Contrato = Nothing
        _DatosPostpago.Url = ConfigurationSettings.AppSettings("RutaWS_DatosPostpago").ToString()
        _DatosPostpago.Credentials = System.Net.CredentialCache.DefaultCredentials
        _DatosPostpago.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("TimeoutWS").ToString())
        Try


            Dim objPostpagoResponse As New ConsultaPostpagoWS.datosContratoResponse
            Dim objcontrato As New ConsultaPostpagoWS.objetoContratoType

            Dim auditoriaWS = New ConsultaPostpagoWS.auditRequestType
            auditoriaWS.idTransaccion = DateTime.Now.ToString("yyyyMMddHHss")
            auditoriaWS.ipAplicacion = ConfigurationSettings.AppSettings("CodAplicacion")
            auditoriaWS.nombreAplicacion = ConfigurationSettings.AppSettings("constAplicacion")
            auditoriaWS.usuarioAplicacion = usuarioaplicacion

            Dim objdatosContratoRequest As New ConsultaPostpagoWS.datosContratoRequest
            objdatosContratoRequest.p_coid = p_codid
            objdatosContratoRequest.auditRequest = auditoriaWS

            objPostpagoResponse = _DatosPostpago.consultarDatosContrato(objdatosContratoRequest)

            objcontrato = objPostpagoResponse.datosContrato.listaDatosContratos(0) '.datosCliente(0)
            MensajeError = objPostpagoResponse.auditResponse.mensajeRespuesta

            If Funciones.CheckStr(objcontrato.co_id) <> "" Then

                item = New Contrato

                item.telefono = objcontrato.telefono
                item.estado = objcontrato.estado
                item.motivo = objcontrato.motivo
                item.fec_estado = objcontrato.fec_estado
                item.plan = objcontrato.plan
                item.plazo_contrato = objcontrato.plazo_contrato
                item.iccid = objcontrato.iccid
                item.imsi = objcontrato.imsi
                item.campania = objcontrato.campania
                item.p_venta = objcontrato.p_venta
                item.vendedor = objcontrato.vendedor
                item.co_id = objcontrato.co_id
                item.fecha_act = objcontrato.fecha_act
                item.flag_plataforma = objcontrato.flag_plataforma
                item.pin1 = objcontrato.pin1
                item.puk1 = objcontrato.puk1
                item.pin2 = objcontrato.pin2
                item.puk2 = objcontrato.puk2

                item.codigo_plan_tarifario = objcontrato.codigo_plan_tarifario
            Else
                MensajeError = "El codigo del contrato no existe.."


            End If
        Catch ex As Exception
            MensajeError = "El servicio esta temporalmente fuera de servicio. " + ex.Message
        End Try

        Return item

    End Function
    'PROY 26210 EGSC END
End Class
