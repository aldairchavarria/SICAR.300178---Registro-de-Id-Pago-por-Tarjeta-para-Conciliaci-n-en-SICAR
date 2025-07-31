Imports System.Runtime.Serialization

'PROY-140126
<Serializable()> _
Public Class SGAVenta
    Public SGAVenta() 'PROY-140126
    Public Sub New()
    End Sub
    Public idprocess As String
    Public idnegocio As String
    Public codcliente As String
    Public tipoDocCliente As String
    Public nroDocCliente As String
    Public apePaterno As String
    Public apeMaterno As String
    Public nombreCliente As String
    Public nombreComercial As String
    Public codTelef1 As String
    Public nroTelefono1 As String
    Public codTelef2 As String
    Public nroTelefono2 As String
    Public codTelefMovil1 As String
    Public nroTelefMovil1 As String
    Public codTelefMovil2 As String
    Public nroTelefMovil2 As String
    Public correo As String
    Public codSucursalInst As String
    Public tipoViaInst As String
    Public nombreViaInst As String
    Public nroViaInst As String
    Public tipoUrbInst As String
    Public nombreUrbInst As String
    Public manzazaInst As String
    Public loteInst As String
    Public ubigeoInst As String
    Public referenciaInst As String
    Public codSucursalFact As String
    Public tipoViaFact As String
    Public nombreViaFact As String
    Public nroViaFact As String
    Public tipoUrbFact As String
    Public nombreUrbFact As String
    Public manzazaFact As String
    Public loteFact As String
    Public ubigeoFact As String
    Public referenciaFact As String
    Public codContacto As String
    Public apePaternoCont As String
    Public apeMaternoCont As String
    Public nombreContacto As String
    Public codSolot As String
    Public usuarioRegistro As String
    Public fechaRegistro As String
    Public codCanalVenta As String
    Public codMotivoAnulacion As String
    Public codSupervisor As String
    Public codCon As String
    Public tipoSupervisor As String
    Public tipoDocVendedor As String
    Public nroDocVendedor As String
    Public nombreCompletoVendedor As String
    Public idPaq As String
    Public nroContrato As String
    Public fechaInstalacion As String
    Public flagRecarga As String
    Public tipoPago As String
    Public datoPago As String
    Public datoRecarga As String
    Public correoAfiliacion As String
    Public nroCartaPreSeleccion As String
    Public codOperadorLD As String
    Public flag_Presuscrito As String
    Public flag_Publicar As String
    Public idPlano As String
    Public usuModifica As String
    Public fechaModifica As String
End Class
