'INC000002161718  inicio
Imports System
Imports System.Collections
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Configuration
Imports System.Data
Imports System.Text

Public Class DatosPostpagoNegocios

    Dim _DatosPostpago As New ConsultasPostpagoWS.SIACPostpagoConsultasWSService

    Public Function DatosPostpagoNegocios()
        _DatosPostpago.Url = ConfigurationSettings.AppSettings("RutaWS_DatosPostpagos").ToString()
        _DatosPostpago.Credentials = System.Net.CredentialCache.DefaultCredentials
        _DatosPostpago.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings("strRelationPlanTimeout").ToString())

    End Function

    Public Function LeerDatosCliente(ByVal nroTelefono As String, ByVal custcode As String, ByRef MensajeError As String) As clsClienteBSCS

        Dim item As clsClienteBSCS = Nothing
        Try

            Dim objPostpagoResponse As New ConsultasPostpagoWS.datosClienteResponse
            Dim objCliente As New ConsultasPostpagoWS.cliente

            objPostpagoResponse = _DatosPostpago.datosCliente(custcode, nroTelefono)
            objCliente = objPostpagoResponse.cliente(0)

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
            Else
                MensajeError = "El número de teléfono no es Postpago."

            End If


        Catch ex As Exception
            MensajeError = "El servicio esta temporalmente fuera de servicio. " + ex.Message
        End Try
        Return item

    End Function
End Class
'INC000002161718  fin
