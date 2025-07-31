<%@ Page Language="vb" Codepage="1252" CodeBehind="sicar_fe_comprobante.aspx.vb" AutoEventWireup="false" Inherits="SisCajas.sicar_fe_comprobante" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>

	<HEAD>
		<title>ImpresionTicket</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<STYLE>
			.clsTexto { FONT-SIZE: 10pt; FONT-FAMILY: Arial; } 
			.clsTextoDeta { FONT-SIZE: 9pt; FONT-FAMILY: Arial; }
			.clsTextoR { FONT-SIZE: 10pt; FONT-FAMILY: Arial; }
			.clsTextoN { FONT-SIZE: 9.3pt; FONT-FAMILY: Arial; }
			.clsTextoDoc { FONT-SIZE: 13pt; FONT-FAMILY: Arial Narrow; }
			.clsTextoCabecera{ FONT-SIZE: 10pt; FONT-FAMILY: Arial Narrow; }
			.clsNumero { FONT-SIZE: 11pt; FONT-FAMILY: Arial Narrow; }
			.clsVigencia { FONT-SIZE: 10pt; FONT-FAMILY: Tahoma; }
			
			.tabla_borde
			{
				BORDER-RIGHT: #336699 1px solid;
				BORDER-TOP: #336699 1px solid;
				FONT-SIZE: 12px;
				BORDER-LEFT: #336699 1px solid;
				COLOR: #ff0000;
				BORDER-BOTTOM: #336699 1px solid;
				FONT-FAMILY: Arial;
				TEXT-DECORATION: none
			}
			
			.Boton
			{
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x;
				background-color: #e9f2fe;
				/*BACKGROUND-IMAGE: url(../images/toolgrad.gif); */
				border-color :#95b7f3	
			}
			
			.BotonResaltado
			{
				
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x; 	
				border-color :#95b7f3
				
			}
		</STYLE>
		<script language="javascript">
		window.onload = window_onload;
		function window_onload() {	
				window.parent.Imprimir();
		}

		function doprint() {
			printbtn.style.visibility = "HIDDEN";
			window.print();
			printbtn.style.visibility = "VISIBLE";
		}
		
		function fe_imprimir(){
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
		// PBI000002160737
		$(document).ready(function () {
			
			$('body').bind('cut copy paste', function (e) {
				e.preventDefault();
			});
		   			
			$("body").on("contextmenu",function(e){
				return false;
			});
		});
		// PBI000002160737
		</script>
		</script>
		<script runat="server">
    
    Dim Const_IGV as String 
    dim DesTotal as decimal
	dim servRec as string
	dim cadenaaa as string
	
	  'Impresion en sumatoria
    dim cadenaP as string
	
    dim FlagCuotas
    dim NumeroCuotas as Integer
    dim CodVendedor as string
    dim NomVendedor as string
    dim CodCajero as string
    dim NomCajero as string
    dim CorrelativoSunat as string
	dim strCodUsuario as string
    dim strNomUsuario as string
    dim Reimpresion as string
    dim FlagPrepago 'as boolean
    'Dim isOffline As Boolean = False
    
    Dim NumComp as String
    Dim NumCompr as String
	
    Dim EfectivoTotal as Double=0
    Dim Efectivo as Double=0
    
    '-------------------------
	' Equipo Remanufacturado  |
	'-------------------------
    dim isDiscleimer as Boolean = False
	Dim strLabelDiscleimer As String = ""
	dim strTamanoLabelDiscleimer As String = ""
    '-------------------------
	'     Formas de Pago	  |
	'-------------------------
    Dim dsConsultaPago As System.Data.DataTable
	'-------------------------
	' Direccion del Cliente	  |
	'------------------------- 
    Dim obUbigeo As New COM_SIC_Cajas.clsCajas
	Dim dtResultDist as System.Data.DataTable
	Dim dtResultDep as System.Data.DataTable
	Dim dtResultDireccion as System.Data.DataTable
	'-------------------------
	' Datos de Vendedor		  |
	'------------------------- 
	Dim dtVendedor as System.Data.DataTable
	Dim Direccion as String
	Dim arrayUbigeo As String()
	Dim DirecUbicacion As String
	Dim CodUbigeo AS String				
	Dim Departamento as String
	Dim Provincia as String
	Dim Distrito as String
	Dim NombreDistrito As String
	Dim NomDepartamento As String
	'-------------------------
	'		Para LOGS		  |
	'------------------------- 
    Dim objFileLog As New SisCajas.SICAR_Log
    Dim nameFile As String = ConfigurationSettings.AppSettings("constNameLogRecarga")
    Dim pathFile As String = ConfigurationSettings.AppSettings("constRutaLogRecarga")
    Dim strArchivo As String = objFileLog.Log_CrearNombreArchivo(nameFile)
    Dim strIdentifyLog As String = ""
    
    'PROY-24724 - Iteracion 3
    Dim blnProteccionMovil As Boolean = False
    
    Public Function IsOffLine() as Boolean
		return Request.QueryString("isOffline") = "1"
    End Function
    
public Function FncImprCabecera(byval dsCompro as System.Data.DataSet, byval dtCabecera as System.Data.DataTable) as string
Dim cad as string

	objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			1.- Imprimir Cabecera   ")

	DIM gStrRazonSocial AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRazonSocial")
    DIM gStrMarca AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrMarca")
    DIM gStrRUC AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRUC")
   
	if dsCompro.tables(0).rows.count>0 then 
		cad= "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
		cad=cad & "<TR>"
		cad=cad & "<TD width='160' class='clsTextoCabecera'><b>" & StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(1)))),VbStrConv.ProperCase) &"</b></TD><TD class='clsTextoR' style='text-align:Center;'><b>R.U.C. " & Trim(cstr(dtCabecera.rows(0)(4))) & "</b></TD>"
		cad=cad & "</TR><TR>"
		cad=cad & "<TD class='clsTexto'><b>"& Trim(cstr(dtCabecera.rows(0)(2))) & "</b></TD><TD rowspan='5' class=clsTextoDoc valign='top'; style='text-align:center;'>"
		
		select case (Trim(cstr(dtCabecera.rows(0)(5))))
		case "E1"
			cad=cad & "<b>"& cstr(System.Configuration.ConfigurationSettings.AppSettings("FE_NomDocFactura"))&"</b></BR>"
		case "E3" 
			cad=cad & "<b>"& cstr(System.Configuration.ConfigurationSettings.AppSettings("FE_NomDocBoleta"))&"</b></BR>"
		case "E7" 
			cad=cad & "<b>"& cstr(System.Configuration.ConfigurationSettings.AppSettings("FE_NomDocNotaCredito"))&"</b></BR>"
		case "E8" 
			cad=cad & "<b>"& cstr(System.Configuration.ConfigurationSettings.AppSettings("FE_NomDocNotaDebito"))&"</b></BR>"
		end select
			
		cad=cad & "<span class='clsTextoN'><b>" & dtCabecera.rows(0)(6) & "</b></span></br>"
		
		select case (Trim(cstr(dtCabecera.rows(0)(5))))
			case "E7"  
				'cad=cad & "Cod.: " & Trim(cstr(IIf(IsDBNull(dtCabecera.rows(0)(25)), "",dtCabecera.rows(0)(25)))) & "" 
			case "E8" 
				'cad=cad & "Cod.: " & Trim(cstr(dtCabecera.rows(0)(25))) & ""
			end select
		
		cad=cad & "</td></TR>"
		
		CorrelativoSunat=replace(Trim(cstr(dsCompro.tables(0).rows(0)(5))),"*","")
		
		if(IsOffLine) then
			Dim objOfflin As New COM_SIC_OffLine.clsOffline
			Dim direccionPDV = objOfflin.ObtenerDireccionPDV(Session("ALMACEN"))

			strCodUsuario = Trim(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")) )
			
			cad=cad & Trim(direccionPDV) & "<BR>"'DIRECCION DEL PDV
			cad=cad & "RUC <FONT class=clsNumero>" & gStrRUC & "</FONT><BR>"
			cad=cad & "<FONT class=clsNumero>" & Trim(Convert.ToDateTime(dsCompro.tables(0).rows(0)("AUDAT")).ToString("dd/MM/yyyy")) & " " & now.tostring("t") & "</FONT><BR>"'12/12/2006 11:23:25 p.m.
			CorrelativoSunat=replace(Trim(cstr(dsCompro.tables(0).rows(0)("XBLNR"))),"*","")
			cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT><BR><FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)("ID_T_TRS_PEDIDO")) ) & "</FONT><BR>"'15-009-0129959/0101588843
			cad=cad & "Numero de Serie <FONT class=clsNumero>" & trim(cstr(session("SerieImprTicket"))) & "</FONT><BR>"'25049873
			
		else
			cad=cad & "<TR><td class='clsTextoCabecera'>" & mid(StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(3)))),VbStrConv.ProperCase),1,28) & "<br>"& mid(StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(3)))),VbStrConv.ProperCase),29,20) & "<br>"
			cad=cad & mid(StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(3)))),VbStrConv.ProperCase),64,12)&"-"& mid(StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(3)))),VbStrConv.ProperCase),57,4)&"-"& mid(StrConv(LCase(Trim(cstr(dtCabecera.rows(0)(3)))),VbStrConv.ProperCase),57,4)
			cad=cad & "</td></tr>"
			cad=cad & "<TR><td class='clsTextoCabecera'>Telefono: " & System.Configuration.ConfigurationSettings.AppSettings("FE_TelefonoClaro") & "</td></tr>"
			cad=cad & "<TR><td class='clsTextoCabecera'>Fax: " & System.Configuration.ConfigurationSettings.AppSettings("FE_FaxClaro") & "</td></tr>"
			cad=cad & "<TR><td class='clsTexto'> " & System.Configuration.ConfigurationSettings.AppSettings("FE_WebClaro") & "</td></tr>"
			'cad=cad & "<TR><td colspan='2' style='text-align:right;' class='clsTexto'><b>" & FormatoFechaFE(cstr(dsCompro.tables(0).rows(0)(3))) & " " & Ucase(now.tostring("t")) & "</b></td></tr>"
			cad=cad & "<TR><td colspan='2' style='text-align:right;' class='clsTexto'><b>" & FormatoFechaFE(cstr(dsCompro.tables(0).rows(0)(0))) & " " & Ucase(now.tostring("t")) & "</b></td></tr>"
			cad=cad & "<TR><td colspan='2'></td></tr>"
			cad=cad & "</TABLE>"
			cad=cad & "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			
		end if
		
		Dim objOff as new COM_SIC_OffLine.clsOffline
		Dim iCodVend%
		if(IsOffLine) then
			'iCodVend= CInt(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")))
			'Dim nombreVendedor$ = objOff.ObtenerNombreCajero(cstr(dsCompro.tables(0).rows(0)("VENDEDOR")))

			'CodVendedor= String.Format("{0:00000#####}", iCodVend)
			'NomVendedor=nombreVendedor
			'if(nombreVendedor = "" or IsNothing(nombreVendedor)) then
			'	CodVendedor= cstr(String.Format("{0:00000#####}", Session("USUARIO")))
			'	NomVendedor=Trim(cstr(Session("NOMBRE_COMPLETO")))
			'end if

			'CodCajero = cstr(Session("USUARIO"))
			'NomCajero = Trim(cstr(Session("NOMBRE_COMPLETO")))		
		else
			CodCajero = strCodUsuario
			NomCajero = strNomUsuario
		end if
	end if
	return cad
End Function

public Function FncImprCliente(byval dsCompro as System.Data.DataSet, byval dtCabecera as System.Data.DataTable) as string
Dim cad as string
   objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			2.- Imprimir Cliente")
	if dsCompro.tables(0).rows.count>0 then
		if(IsOffLine) then
			cad= cad & Trim(cstr(dsCompro.tables(0).rows(0)("NOMBRE_CLIENTE"))) & "<BR>"
			cad= cad & "<FONT class=clsNumero>" & Trim(cstr(dsCompro.tables(0).rows(0)("CLIENTE"))) & "</FONT><BR>"'10810842
		else
			cad = "<tr Style='border-style:Solid; border-bottom: thick'><td colspan='2' class='clsTexto'>"

			cad = cad & "<TABLE WIDTH=240 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			cad = caD & "<tr><td class='clsTexto'><b>" & Trim(cstr(dtCabecera.rows(0)(9))) & "</b></TD></tr>"
			'cad = cad & "<TR><td class='clsTexto'><b>" & Trim(cstr(dtCabecera.rows(0)(30))) & "</b></td></TR>"
			cad = cad & "<TR><td class='clsTexto'><b>" & DirecUbicacion & " - " & NomDepartamento & " - " & NombreDistrito & "</b></td></TR>"
			cad = cad & "<TR><td class='clsTexto'><b>" & Trim(cstr(dtCabecera.rows(0)(8))) & "</b></td></TR>"

			Dim OficinaVta as String = Session("ALMACEN")
			
			cad = cad & "<TR><td class='clsTexto'><b>OFICINA DE VTA: " & OficinaVta & "</b></td></TR>"
			cad = cad & "<TR><TD style='padding-left:20px;' class='clsTexto'>" & ""
			
			Dim vMotiv() As String 
			Dim Motivo As String
			if dtCabecera.rows(0)(26) <> "" then
				vMotiv = dtCabecera.rows(0)(26).Split("|")
				Motivo = vMotiv(0)
			else
				Motivo = ""
			end if
			vMotiv = dtCabecera.rows(0)(26).Split("|")
			select case (Trim(cstr(dtCabecera.rows(0)(5))))
			case "E7" ' "07" 
				'cad = cad & "MOTIVO: " & Motivo & ""
			case "E8" ' "08"
				'cad = cad & "MOTIVO: " & Motivo & ""
			end select
			cad = cad & "</TD></TR>"
			
			cad = cad & "</TABLE>"
			
			cad = cad & "</BR>"
			
			cad = cad & "</td></TR>"
			
		end if

	end if 		

	return cad
End Function

public Function FncImprDetVentas(byval dsCompro as System.Data.DataSet, byval dtDetalle as System.Data.DataTable) as string
dim cad
objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			3.- Imprimir Detalle Venta")
    
    'PROY-24724 - IIteracion 3 - INICIO
    Dim objBWPostventaProteccionMovil As New COM_SIC_Activaciones.BWGestionaPostventaProteccionMovil
    Dim strIdTransaccion As String = String.Empty
    Dim strIPAplicacion As String = String.Empty
    Dim strNombaplicacion As String = String.Empty
    Dim strUsrProceso As String = String.Empty
    Dim strMsjRespuesta As String = String.Empty
    Dim strCodRespuesta As String = String.Empty
    Dim listaResponse As New ArrayList
    Dim nroCertificado As String = String.Empty
    Dim objProteccionMovil As New COM_SIC_Activaciones.clsProteccionMovil
    Dim objConsSeedsSock As New COM_SIC_Activaciones.clsConsultaMsSap 'ConsultaSeedsSock
    strIdTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff")
    strIPAplicacion = CurrentTerminal
    strNombaplicacion = ConfigurationSettings.AppSettings("constAplicacion")
    strUsrProceso = CurrentUser
    Dim strNroPedidoEquipo As String = String.Empty
    Dim strMsjRpt As String = String.Empty
    Dim strCodRpt As String = String.Empty
    Try
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Inicio - Validar Equipo Protección Móvil")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	Validar Equipo Protección Móvil. Parámetros de entrada:")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	NroPedido:" & Trim(request.QueryString("codRefer")))
        objProteccionMovil.ValidaPagoEquipoProteccionMovil(Trim(request.QueryString("codRefer")), strNroPedidoEquipo, strCodRpt, strMsjRpt)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	Validar Equipo Protección Móvil. Parámetros de salida:")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strNroPedidoEquipo:" & strNroPedidoEquipo)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strCodRpta:" & strCodRpt)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strMsgRpta:" & strMsjRpt)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Fin - Validar Equipo Protección Móvil")
        If (strCodRpt.Equals("0") Or strCodRpt.Equals("-1")) AndAlso Not Trim(request.QueryString("codRefer")).Equals("") Then
			blnProteccionMovil = True
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Inicio - Obtener Datos Protección Móvil")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	Obtener Datos Protección Móvil. Parámetros de entrada:")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strNroPedidoEquipo:" & strNroPedidoEquipo)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strIdTransaccion:" & strIdTransaccion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strIPAplicacion:" & strIPAplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strNombaplicacion:" & strNombaplicacion)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strUsrProceso:" & strUsrProceso)
            objBWPostventaProteccionMovil.ObtenerDatosProteccionMovil(strNroPedidoEquipo, strIdTransaccion, strIPAplicacion, strNombaplicacion, strUsrProceso, strCodRespuesta, strMsjRespuesta, listaResponse)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	Obtener Datos Protección Móvil. Parámetros de salida:")
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strCodRespuesta:" & strCodRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	strMsjRespuesta:" & strMsjRespuesta)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	listaResponse registros:" & listaResponse.Count)
            objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Fin - Obtener Datos Protección Móvil")
        End If

    Catch ex As Exception
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==  ERROR ObtenerDatosProteccionMovil Catch" & ex.Message.ToString())
    End Try	
    'PROY-24724 - IIteracion 3 - FIN
    'PROY-24724 - Iteracion 2 Siniestros -INI
     objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "== INICIO - Validar Pedido de Siniestro ==")
     Dim strCodMatPedido As String = (dtDetalle.rows(0)(3))
	 Dim strCodMatSiniestroParam As String = hidCodMaterialSiniestro.Value
	 Dim listaRespoSini As New Arraylist
	 Dim strIdTransaccionSin = DateTime.Now.ToString("yyyyMMddHHmmssfff")
	 
	 if strCodMatPedido.Equals(strCodMatSiniestroParam) Then 
		blnProteccionMovil = True 
		Dim strTelefSini  As String = String.Empty
		Dim strEstdFinalSiniestro As String = hidEstdFinalSini.Value
		Dim strCodRpta As String = String.Empty
		Dim strMsjRpta As String = String.Empty
		
		
		if dsCompro.tables(1).rows.count > 0 then
			If(Not IsNothing(Session("numeroTelefono"))) Then
				strTelefSini = Session("numeroTelefono")
			else
				If IsDBNull(dtDetalle.rows(0)(22)) Then
					strTelefSini = ""
				Else
					strTelefSini = Cstr(dtDetalle.rows(0)(22))
				End If
			End If	
		End if
		try
		
		
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== MostrarDetalleSiniestro -  INICIO ==")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   WS: consGestionaPostventaProteccionMovilWS ")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==   Método: obtenerDetalleSiniestro")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    IN  IdTransaccion: " & strIdTransaccionSin)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    IN  Teléfono: " & strTelefSini)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    IN  Estado Siniestro: " & strEstdFinalSiniestro)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    IN  Usuario: " & strUsrProceso)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    IN  Terminal: " & strIPAplicacion)
		
		objBWPostventaProteccionMovil.MostrarDetalleSiniestro(strIdTransaccionSin,strTelefSini, strEstdFinalSiniestro, CurrentUser, CurrentTerminal, strCodRpta, strMsjRpta, listaRespoSini)

		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    OUT Cod. Rpta.: " & strCodRpta)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    OUT Msj. Rpta.: " & strMsjRpta)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==    OUT Lista.Count.: " & listaRespoSini.Count)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "== MostrarDetalleSiniestro -  FIN ==")
		
		Catch ex As Exception
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "==  ERROR ObtenerDatosProteccionMovil Catch" & ex.Message.ToString())
		Finally  
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "== INICIO - Validar Pedido de Siniestro ==")
		End Try	
		
	 End If
	 
    'PROY-24724 - Iteracion 2 Siniestros -FIN
    dim FlagRecarga as boolean
    DesTotal=0
    
    '****Bloque que valida si es una Recarga
    
    'if len(trim(FncImprRecargas(dsCompro)))=0 then
	' FlagRecarga=false
	'else   
	   FlagRecarga=true
	'end if 
	
	'********************************
	
	'if dsCompro.tables(2).rows.count > 0 then
	objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			- Trae Datos dsCompro: " & dsCompro.tables(1).rows.count)
	if dsCompro.tables(1).rows.count > 0 then
			   	
			cad = "<TR>"
			cad = cad & "<TD COLSPAN='2' STYLE='PADDING-LEFT:20;' STYLE='border-top:solid 1px;'>"
			cad = cad & "<TABLE WIDTH=220 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			
			dim dr as System.Data.datarow
			dim indexTable as Integer
			dim i as integer = 0
			'Do While (Not objRecordSetA.BOF AND Not objRecordSetA.EOF)
			if(IsOffline) then
				indexTable = 1
			else
				'indexTable = 2
				indexTable = 1
			end if
			
			'-----------------------------------------------------
			' 03/02/2015: Equipo Remanufacturado
			'-----------------------------------------------------
			Dim dsParam As System.Data.DataSet			
			Dim objSicarDB As New COM_SIC_Cajas.clsCajas				
			Dim strGrupo as String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerCodGrupo")		
			Dim strCodMat As String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdMateriales")
			Dim strCodLabDis As String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdEtiqueta")
			Dim strCodTamLab AS String = System.Configuration.ConfigurationSettings.AppSettings("constDisclaimerIdTamEtiqueta")
			Dim strArrCodMateriales() As String
			Dim strCodMateriales As String =""
			
			'****************************************

			
			for each dr in dsCompro.Tables(indexTable).rows 
				cad = cad & "<TR>"
				cad = cad & "<TD class='clsTextoDeta'>" 
				
				if(IsOffLine) then
					servRec = Trim(cstr(dr("MATNR")))
					cad = cad & Trim(cstr(dr("MATNR"))) & "&nbsp;&nbsp;" & Trim(FormatNumber(cstr(dr("KWMENG")), 3))
				else
					servRec = Trim(cstr(dr(1)))
					cad = cad & Trim(cstr(dtDetalle.rows(i)(3))) & "&nbsp;&nbsp;" & Trim(cstr(dtDetalle.rows(i)(2)))
					
				end if
				
				
				Dim preUnit as Double =0
				Dim igvRecaudacion as Double =0
				Dim pUnitRV as Double =0
				Dim Tot as Double = 0
				if(IsOffLine) then
					Tot = CDbl(dr("TOTAL_PAGO"))
					pUnitRV = CDbl(System.Configuration.ConfigurationSettings.AppSettings("precioUnitarioRecarga"))
					preUnit = Math.Round(Tot * pUnitRV, 2)
				end if
				
				cad = cad & "</TD>"
				cad = cad & "<TD ALIGN='RIGHT' class='clsTextoDeta'>"
				if(IsOffLine) then
					cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(preUnit))) & "&nbsp;</FONT>"
				else
					cad = cad & "S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dtDetalle.rows(i)(5)))) & "&nbsp;</FONT>"
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "				- Detalle del Artiulo: " & cstr(dtDetalle.rows(i)(5)))
				end if
				
				cad = cad & "</TD>"
				cad = cad & "</TR>"
				cad = cad & "<TR>"
				cad = cad & "<TD colspan='2' class='clsTextoDeta'>"
				
				if(IsOffLine) then
					cad = cad & Trim(cstr(dsCompro.tables(1).rows(0)("DESCRIPCION_PRODUCTO"))) & "&nbsp;"
				else
					'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Empieza Disclaimer")
					dsParam = objSicarDB.FP_ConsultaParametros(strGrupo)	           
            			
					For idx As Integer = 0 To dsParam.Tables(0).Rows.Count - 1
						
						if(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodLabDis)
							strLabelDiscleimer = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE")
						else if(dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodMat)	
							strArrCodMateriales = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE").Split("|")
							strCodMateriales = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE")
						else if (dsParam.Tables(0).Rows(idx).Item("SPARN_CODIGO") = strCodTamLab)
							strTamanoLabelDiscleimer = dsParam.Tables(0).Rows(idx).Item("SPARV_VALUE")
						end if
					Next
					
					'----------------------------------------------------------
					' Valida si el Cod de Material es un Equipo Remanfacturado
					'----------------------------------------------------------
					
					If strCodMateriales.IndexOf(servRec) <> -1 Then					
						If strLabelDiscleimer <> "" Then 
							strLabelDiscleimer = "- " + strLabelDiscleimer.SubString(2)
						End If					
					else
						strLabelDiscleimer = ""											
					End If
										
					cad = cad & Trim(cstr(dtDetalle.rows(i)(4))) & "&nbsp;" & strLabelDiscleimer
					'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " Detalle + Disclarimer: " & cstr(dtDetalle.rows(i)(4)) & " - " & strLabelDiscleimer)
					
				end if
				cad = cad & "</TD>"
				cad = cad & "</TR>"
				
				objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "				- Flag Recarga: " & FlagRecarga)
				'objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "				- Tipo de Articulo: " & dsCompro.Tables(0).Rows(0)(1))
				if (not FlagRecarga or (dsCompro.Tables(0).Rows(0)(1)="SERRCDTHTM")) then
					cad = cad & "<TR>"
					cad = cad & "<TD colspan='2' class='clsTextoDeta'>"
					if(IsOffLine) then		
					
						Dim totalVendido as Double
						Dim precioUnitario as Double
						
						Dim cantidadProducto as Double = Cdbl(dsCompro.tables(1).rows(0)("KWMENG"))
												
						totalVendido = CDbl(dr("TOTAL_PAGO"))

						precioUnitario = Math.Round(totalVendido / igvRecaudacion, 2)

						cad = cad & "Pvta:&nbsp;S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(precioUnitario))) & "</FONT>&nbsp;&nbsp;Dscto:&nbsp;S/<FONT class=clsNumero>" & cant_dec(Trim(cstr(dr("DESCUENTO")))) & "</FONT>&nbsp;</FONT>&nbsp;"
					else
						
					end if
					cad = cad & "</TD>"
					cad = cad & "</TR>"
				end if 
				cad = cad & "<TR>"
				cad = cad & "<TD colspan='2' class=clsTextoDeta >"
				
				if(IsOffLine) then
					If(Not IsNothing(Session("numeroTelefono"))) Then
						cad = cad & "<FONT class=clsNumero>" & Trim(cstr("Telefono: 000000" + Session("numeroTelefono"))) & "</FONT>&nbsp;"
					End If					
					cad = cad & "</TD>"
					cad = cad & "</TR>"
				else
					Dim varSerie, varTelefono As String
					'Validando SERIE
					If IsDBNull(dtDetalle.rows(i)(23)) Then
						varSerie = "000000000000000"
					Else
						varSerie = Cstr(dtDetalle.rows(i)(23))
					End If
					'**********************seedstock*****************************'
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "***********[SeedsSock][INICIO][VALIDACION]************")
					Dim codRespuesta As String=""
					Dim strMsjSeedsSock As String =objConsSeedsSock.ConsultaSeedsSock(varSerie,codRespuesta)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "***********[SeedsSock][VALIDACION][codRespuesta]" & codRespuesta)
					Dim cadSeedStock As String=""
					If codRespuesta="0" Then
						cadSeedStock= " <br><b><FONT class='clsTexto'> *** &quot;" & strMsjSeedsSock & "&quot; ***</FONT></b>&nbsp;" 
						objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "***********[SeedsSock][INICIO][cadSeedStock]" & cadSeedStock)
					End If
					'**********************seedstock*****************************'
					'Validando TELEFONO
					If IsDBNull(dtDetalle.rows(i)(22)) Then
						varTelefono = ""
					Else
						varTelefono = Cstr(dtDetalle.rows(i)(22))
					End If
					'PROY-24724 - IIteracion 3 - INICIO
                                       'PROY-24724 - Iteracion 2 Siniestros -INI
					if (listaResponse.count >0)
						cad = cad & "<FONT class=clsNumero>Certificado: " & listaResponse(0).nroCertificadoReq & " <br> Serie(s): " & varSerie & cadSeedStock & " <br> Teléfono: " & varTelefono & "</FONT>&nbsp;"
					else if (listaRespoSini.count >0)
						cad = cad & "<FONT class=clsNumero>Certificado: " & listaRespoSini(0).nroCertif & " <br> Serie(s): " & varSerie & cadSeedStock & " <br> Teléfono: " & varTelefono & "</FONT>&nbsp;"
					'PROY-24724 - Iteracion 2 Siniestros -FIN
					else
					cad = cad & "<FONT class=clsNumero>Serie(s): " & varSerie & cadSeedStock & " <br> Teléfono: " & varTelefono & "</FONT>&nbsp;"  
					End if
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "***********[SeedsSock][CADENAIMPRESION]" & cad)
					objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "***********[SeedsSock][FIN][VALIDACION]************")
					'PROY-24724 - IIteracion 3 - FIN
					cad = cad & "</TD>"
					cad = cad & "</TR>"				
					DesTotal=DesTotal + 0 
				end if
				i+=1
			next
			cad = cad & "</TABLE>"
			cad = cad & "</TD>"
			cad = cad & "</TR>"
		end if 		
return cad
End Function


public Function FncImprRecargas(byval dsCompro as System.Data.DataSet) as string
dim cad
dim cadRV
cadRV="0"
dim recargaefectiva,valorventa,descuento, igv, totalpago, numrecarga as decimal
dim cteMSG_GLOSA_RECARGAVIRTUAL_1 as string = System.Configuration.ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_1")
dim cteMSG_GLOSA_RECARGAVIRTUAL_2 as string = System.Configuration.ConfigurationSettings.AppSettings("cteMSG_GLOSA_RECARGAVIRTUAL_2")
Dim indexTable as Integer

if(IsOffLine) then
	indexTable=1
else
	indexTable=1 '4
end if
'*****Recarga Virtual****
'set objRecordSetRV= XmlToRecordset(StrXml,"RS06")

if dsCompro.tables(indexTable).rows.count>0  or IsOffLine then
	if dsCompro.tables(indexTable).rows(0)(4) > 0 or IsOffLine then
		if(IsOffLine) then
			if (dsCompro.tables(1).rows(0)("MATNR").StartsWith("SERECVI")) then
				Dim totalRecargado as Double = Cdbl(dsCompro.tables(1).rows(0)("KWMENG"))
				Dim igvRecaudacion as Double = CDbl(hidIgvActual.value) + 1
				Dim pUnitarioRV as Double = CDbl(System.Configuration.ConfigurationSettings.AppSettings("precioUnitarioRecarga"))
				Dim valVen as Double = 0
				recargaefectiva= totalRecargado
				
				valorventa= Math.Round((recargaefectiva/igvRecaudacion), 2)
				'valorventa= Math.Round((recargaefectiva*pUnitarioRV), 2)
				
				descuento= dsCompro.tables(1).rows(0)("DESCUENTO")
				igv= recargaefectiva - valorventa
				
				totalpago= recargaefectiva
				
				'recargaefectiva= dsCompro.tables(1).rows(0)("VAL_VENTA")
				'valorventa= dsCompro.tables(1).rows(0)("VAL_VENTA")
				'descuento= dsCompro.tables(1).rows(0)("DESCUENTO")
				'igv= dsCompro.tables(1).rows(0)("IGV")
				'totalpago= dsCompro.tables(1).rows(0)("TOTAL_PAGO")
				
				numrecarga= dsCompro.tables(1).rows(0)("NRO_REC_SWITCH")
				cadRV="1"
				
			Else
				Return ""
			End If
		else
			recargaefectiva= dsCompro.tables(4).rows(0)(4)
			valorventa= dsCompro.tables(4).rows(0)(5)
			descuento= dsCompro.tables(4).rows(0)(6)
			igv= dsCompro.tables(4).rows(0)(7)
			totalpago= dsCompro.tables(4).rows(0)(8)
			numrecarga= dsCompro.tables(4).rows(0)(9)
			cadRV="1"			
		end if

		cad = "<TR>"
		cad=cad + "<TD>"
		cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>" 
		cad=cad + "<TR >"
		cad=cad + "<TD WIDTH=50% >NUM RECARGA</TD><TD WIDTH=25% >:</TD><TD WIDTH=15% ALIGN=RIGHT class=clsNumero>" & cstr(numrecarga) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >RECARGA EFECT.</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cant_dec(cstr(recargaefectiva)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >VALOR VENTA</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(valorventa) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >DESCUENTO</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cant_dec(cstr(descuento)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >SUB TOTAL</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(valorventa-descuento) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >I.G.V</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(igv) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=48% >TOTAL PAGO</TD><TD WIDTH=2% >:</TD><TD WIDTH=30% ALIGN=RIGHT class=clsNumero>s/" & cstr(cant_dec(totalpago)) & "</TD><TD WIDTH=20% ></TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD colspan=4 >" & cteMSG_GLOSA_RECARGAVIRTUAL_1  & cteMSG_GLOSA_RECARGAVIRTUAL_2 & "</TD>"
		cad=cad + "</TR>"
		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
		
		end if
	end if

	return cad
End Function

public Function FncImpCuotas(byval dsCompro as System.Data.DataSet) as string
dim cad
dim scFecha
dim strDesCuota
dim PosVen as integer
    'dim objRecordSetA
'	Set objRecordSetA = XmlToRecordset(StrXml,"RS03")
	'if not objRecordSetA is nothing then
	'	if not objRecordSetA.eof then
	if dsCompro.tables(1).rows.count>0 then
			'cad="<TR><TD>&nbsp;</TD></TR>"			
			cad="<TR>"
			cad=cad + "<TD>"
			cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>"

		   NumeroCuotas=-1
		   dim dr as system.data.datarow
		   for each dr in dsCompro.tables(1).rows
			'Do While (Not objRecordSetA.BOF AND Not objRecordSetA.EOF)
				NumeroCuotas=NumeroCuotas+1
				FlagCuotas=True
				scFecha = cstr(dr(2))
				strDesCuota = trim(cstr(dr(1)))
				PosVen=instr(1,strDesCuota,"Vencimiento")
				if PosVen>0 then
				strDesCuota=mid(strDesCuota,1,posVen-1)
				end if 
				PosVen=instr(1,strDesCuota,":")
				if PosVen>0 then
				strDesCuota=mid(strDesCuota,1,posVen-1)
				end if 
				strDesCuota=trim(strDesCuota)
				if (Trim(scFecha)="12:00:00 a.m." or scFecha= "00000000") then scFecha = ""
			    
			    if NumeroCuotas = 0 then
				     'Dim obSap as new SAP_SIC_PAGOS.clsPagos
  				     'dim dsPedido as System.Data.Dataset = obSap.Get_ConsultaPedido("",session("ALMACEN"),request.QueryString("codRefer"),"")
					 'if dsPedido.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") then
					 '   strDesCuota = strDesCuota & "&nbsp;" & ConfigurationSettings.AppSettings("gConstPorcPrePago") & "%" & "&nbsp;"
					 '   strDesCuota = Replace(strDesCuota," ","&nbsp;")
					 'end if
					 'dsPedido = nothing
				end if
			
				cad=cad & "<TR>"
				cad=cad & "<TD WIDTH=32% class=clsTexto>"
				cad=cad & strDesCuota & "&nbsp;"'Cuota Inicial
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=13% >"
				cad=cad & FormatFecha(scFecha) & "&nbsp;"
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=35% ALIGN=RIGHT class=clsTexto>"
				cad=cad & "S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dr(3)))) &  "&nbsp;</FONT>"'10012.30
				cad=cad & "</TD>"
				cad=cad & "<TD WIDTH=20% >"
				cad=cad & "&nbsp;"
				cad=cad & "</TD>"
				cad=cad & "</TR>"
  		    next
	cad=cad + "</TABLE>"
	cad=cad + "</TD>"
	cad=cad + "</TR>"
end if 		
return cad
End Function

public Function FormatFecha(byval strFecha as string)  as string
dim Resp
dim ArrFecha() as string
Resp = strFecha
if trim(strFecha)<>"" then
   ArrFecha= strFecha.split("/")
   if ubound(ArrFecha)>=1 then
	  Resp = ArrFecha(0) & "/" & ArrFecha(1)
	else
	  Resp = strFecha.substring(6,2) & "/" & strFecha.substring(4,2)
   end if 
end if 
FormatFecha=Resp 
End Function 

public Function FncImprTotales (byval dsCompro as System.Data.DataSet,byval dtCabecera as System.Data.DataTable) as string
dim cad
objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			4.- Imprimir Totales")
	Dim indexTable as Integer
	Dim T_VN_GRATUITAS as decimal 'INC000001591136 :: INI
	if(IsOffLine) Then
		indexTable = 1
	else
		'indexTable = 3
		indexTable = 1
	end if
	
    if dsCompro.tables(indexTable).rows.count>0 then
		cad= "<TR class=clsTexto ><TD COLSPAN='2' STYLE='PADDING-LEFT:20;'>"
		cad=cad + "</BR>"
		cad=cad + "<TABLE WIDTH=220 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
		cad=cad + "<TABLE WIDTH=220 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
		
		'INC000001591136 :: INI
		if (cstr(dtCabecera.rows(0)(13)) <> "") then
			T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(cstr(dtCabecera.rows(0)(13))),2))
			if (T_VN_GRATUITAS > 0) then
				cad=cad + "<TR><TD  class='clsTexto'>SON: " + cstr(System.Configuration.ConfigurationSettings.AppSettings("cosTransferenciaGratuita")) + " </TD></TR>"
			else
		cad=cad + "<TR><TD  class='clsTexto'>SON: " + dtCabecera.rows(0)(20) + " SOLES</TD></TR>"
			end if
		end if
		'INC000001591136 :: FIN
		cad=cad + "</TABLE>"
		cad=cad + "</BR>"
		cad=cad + "</TD></TR>"
		cad=cad + "<TR><TD COLSPAN='2'>"
		cad=cad + "<TABLE WIDTH=220 class='clsTexto' CELLSPACING=0 CELLPADDING=0 BORDER=0>"
		cad=cad + "<TR>"
		cad=cad + "<TD style='padding-left:38px'>"
		cad=cad + "Valor Venta Oper.Grav."
		cad=cad + "</TD>"
		cad=cad + "<TD ALIGN=RIGHT class=clsTexto>"
		cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(cstr(decimal.Parse(cstr(dtCabecera.rows(0)(10)))+Math.abs(decimal.Parse(cstr(dtCabecera.rows(0)(18)))))) & "</FONT>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD style='padding-left:38px' class='clsTexto'>"
		cad=cad + "Valor Venta Oper.Inaf."
		cad=cad + "</TD>"
		cad=cad + "<TD ALIGN=RIGHT class=clsTexto>"
		
		if(IsOffline) Then
			cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dsCompro.tables(indexTable).rows(0)("VAL_VENTA")))) & "</FONT>"
		else
			cad=cad + "&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(11)))) & "</FONT>"
		end if
		
		cad=cad + "</TD>"
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD style='padding-left:38px' class=clsTexto>"
		if(IsOffline) Then
			Dim total# =CDbl(dsCompro.tables(1).rows(0)("TOTAL_PAGO"))
			Dim igv# = CDbl(System.Configuration.ConfigurationSettings.AppSettings("valorIGV") + 1)
			Dim Redo# = 0
			Dim Sbt#
			Dim MontoIGV#
			Dim preUNit#
	        
			Sbt = total / igv
			Dim monsinr = Math.Round(Sbt, 3)
			Dim monround = Math.Round(Sbt, 2)
			preUNit = Math.Round(Sbt, 2)
			Redo = Math.Round(monround - monsinr, 2)
			MontoIGV# = (total - preUNit) + Redo
		
		
			cad=cad + "I.G.V " & hidIgvActualP.value  & "%</FONT>" 'PROY-31766'
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(MontoIGV))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>AJUSTE REDONDEO</TD>"
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & negativo(cant_dec(trim(cstr(Redo)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD WIDTH=65% class=clsTexto>TOTAL</TD>"
			cad=cad + "<TD WITDH=35% ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(total))) & "</FONT></TD>"
			cad=cad + "</TR>"
		else
			cad=cad + "Valor Venta Oper.Exon." & "</FONT>"
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(12)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>Descuento</TD>"
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;-S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(18)))) & "</FONT></TD>"
			
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>Total Valor Venta</TD>"
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(10)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>I.G.V. "
			'PROY-24724 - Iteracion 3 - INICIO
			If blnProteccionMovil Then
				cad=cad + "0%</TD>"
			Else
				cad=cad + " " & hidIgvActualP.value & "%</TD>" 'PROY-31766'
			End If 
			'PROY-24724 - Iteracion 3 - FIN
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(14)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>Otros Tributos</TD>"
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(16)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>Redondeo</TD>"'INC000001591136'
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(17)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			cad=cad + "<TR>"
			cad=cad + "<TD style='padding-left:38px' class=clsTexto>Total</TD>"
			cad=cad + "<TD ALIGN=RIGHT class=clsTexto>&nbsp;S/ <FONT class=clsNumero>" & cant_dec(trim(cstr(dtCabecera.rows(0)(19)))) & "</FONT></TD>"
			cad=cad + "</TR>"
			
			cad=cad + "<TR>"
			select case (Trim(cstr(dtCabecera.rows(0)(5))))
			case "E7" ' "07" "
				cad=cad + "<TD colspan='2' class='clsTexto'>DOC. MODIFICA: " & cstr(dtCabecera.rows(0)("DOCREFERENCIA")) & "</TD>"
			case "E8" ' "08"
				cad=cad + "<TD colspan='2' class='clsTexto'>DOC. MODIFICA: " & cstr(dtCabecera.rows(0)(27)) & "</TD>"
			end select
			cad=cad + "</TR>"			
			
			cad=cad + "</table></br>"
			cad=cad + "<table style='border:Solid 1px;' ><tr><td>"
			cad=cad + "<table b class=clsTexto WIDTH=220 CELLSPACING=0 CELLPADDING=0 BORDER=0>"
			cad=cad + "<tr><td STYLE='FONT-SIZE: 7pt;'>" + System.Configuration.ConfigurationSettings.AppSettings("FE_PiePagina1") + "</td></tr></colgroup>"
			cad=cad + "</table>"
			cad=cad + "</td></tr></table>"
			cad=cad + "<table b class=clsTexto WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0>"		
			'NumComp = Trim(cstr(dtCabecera.rows(0)(6))).Substring(0,5)
			''NumCompr = Trim(cstr(dtCabecera.rows(0)(6))).Substring(5,7)
			'NumCompr = Trim(cstr(dtCabecera.rows(0)(6))).Substring(5,6)
			'cad=cad + "<tr><td style='text-align:right'><b>" + NumComp + "0" + NumCompr + "</b>"
			cad=cad + "<tr><td style='text-align:right'><b>" + dtCabecera.rows(0)(6) + "</b>"
			cad=cad + "</td></tr>"
			if len(trim(cstr(dtCabecera.rows(0)(32))))<>0 then
			cad=cad + "<TR><TD align='center' STYLE='FONT-SIZE: 8pt;' colspan='2' class=clsTexto>" + trim(cstr(dtCabecera.rows(0)(32))) + "</TD></TR>" 'Codigo Hash
			else
			cad=cad + "<TR><TD align='center' STYLE='FONT-SIZE: 8pt;' colspan='2' class=clsTexto>" + System.Configuration.ConfigurationSettings.AppSettings("FE_MensajeComprobanteInvalido") + "</TD></TR>" 'Codigo Hash
			end if
			cad=cad + "<TR><TD align='center' STYLE='FONT-SIZE: 6pt;'>" + System.Configuration.ConfigurationSettings.AppSettings("FE_PiePagina2") + "</TD></TR>"
		end if
		
		if(IsOffline) Then
			if len(trim(cstr(dsCompro.tables(1).rows(0)("IGV"))))<>0 then
				cad=cad + "<TR>"				
				cad=cad + "<TD WIDTH=65% class=clsTexto colspan='2'>" & "" & "</TD>"
				cad=cad + "</TR>"
			end if			
		Else
			'***COMENTADO***
			'if len(trim(cstr(dsCompro.tables(3).rows(0)(7))))<>0 then 
			'	cad=cad + "<TR>"
			'	cad=cad + "<TD WIDTH=65% class=clsTexto colspan='2'>" & trim(cstr(dsCompro.tables(3).rows(0)(7))) & "</TD>"	
			'	cad=cad + "</TR>"
			'end if
			'** FIN 
		End IF		

		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"
  end if 		
return cad
End Function


public Function FncImprVendedores() as string
dim cad
dim tam
dim tamstrcaj
Dim nombreVendedor
Dim nombreCajero
tam=len(codVendedor)
tamstrcaj=len(codCajero)
objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			7.- Imprimir Vendedores")
'codCajero=string(tam-tamstrcaj,"0") & codCajero 
codCajero = codCajero.padleft(Math.Abs(tam-tamstrcaj),"0")
'cad="<TR><TD>&nbsp;</TD></TR>"
cad="<TR>"
cad=cad & "<TD class=clsTexto>"
cad=cad & "<FONT class=clsNumero>V: " & codVendedor & "</FONT> " & mid(nomVendedor,1,14) & "<BR>"
cad=cad & "<FONT class=clsNumero>C: " & codCajero & "</FONT> " & mid(nomCajero,1,14) & "<BR>"
cad=cad & "</TD>"
cad=cad & "</TR>"
if trim(Reimpresion)<>"" then
	cad=cad & "<TR>"
	cad=cad & "<TD align='center' class=clsTexto>"
	cad=cad & "***REIMPRESION***"
	cad=cad & "</TD>"
	cad=cad & "</TR>"
end if 
'INICIO Comentado - Kerly Adriana 
'cad=cad & "<TR>"
'cad=cad & "<TD align='center' class=clsTexto>"
'cad=cad & "Tr. <FONT class=clsNumero>" & CorrelativoSunat & "</FONT>"
'cad=cad & "</TD>"
'cad=cad & "</TR>"
'cad=cad & "<TR>"
'FIN Comentado - Kerly Adriana 
'dim objPagos as new SAP_SIC_Pagos.clsPagos
Dim objOff as new COM_SIC_OffLine.clsOffline
cad=cad & "<TD align='center' class=clsTexto>"
'cad=cad & "T/C: S/&nbsp;<FONT class=clsNumero>" & objPagos.Get_TipoCambio(Now.ToString("d")).ToString("N2") & "</FONT>"
'----------------Mejoras 09/02/2015------------------
cad=cad & "T/C: S/&nbsp;<FONT class=clsNumero>" & objOff.Obtener_TipoCambio(right("00" & Now.Day, 2) & "/" & right("00" & Now.Month, 2) & "/" & right("0000" & Now.Year, 4)).ToString("N3") & "</FONT>" 'clsoffline
'cad=cad & "T/C: S/.&nbsp;<FONT class=clsNumero>" & objPagos.Get_TipoCambio(Format(Now.Day, "00") & "/" & Format(Now.Month, "00") & "/" & Format(Now.Year, "0000")).ToString("N2") & "</FONT>" 'SAP
'---------------Fin Mejoras Kerly Adriana------------
cad=cad & "</TD>"
cad=cad & "</TR>"
return cad
End Function

public Function FncPieCuotas (byval dsCompro as System.Data.DataSet, byval cuotas as Integer) as string
dim cad
dim cteMSG_GLOSA_VENTA_CUOTAS = ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_CUOTAS")
If(Not IsOffline) then
	if dsCompro.tables(1).rows.count>0 then 
	'cad="<TR><TD>&nbsp;</TD></TR>"
	cad="<TR>"	
	cad=cad & "<TD class=clsTexto>" & Replace(cteMSG_GLOSA_VENTA_CUOTAS, "#", cuotas) & "</TD>"
	cad=cad & "</TR>"
	end if
End If
return cad
End Function

public Function FncPiePrepago () as string
dim cad
objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			6.- Imprimir Pie Prepago")
if FlagPrepago then 
'cad="<TR><TD>&nbsp;</TD></TR>"
cad="<TR>"
cad=cad & "<TD class=clsTexto>" & ConfigurationSettings.AppSettings("cteMSG_GLOSA_VENTA_PREPAGO") & "</TD>"
cad=cad & "</TR>"
end if 
return cad
End Function

public Function cant_dec(byval cantidad as string) as string
	   Dim ok as integer = 0
	   dim i as integer
	   	   
	   for i=1 to len(cantidad)
			if(mid(cantidad,i,1)=".") then
				ok = len(mid(trim(cantidad),i+1))
			end if
	   Next
	   if ok=0 then cant_dec = cantidad & ".00" 
	   if ok=1 then cant_dec = cantidad & "0" 
	   if ok>1 then cant_dec = cantidad	   	   
	end Function
	
	
	public Function FormatoFechaFE(byval Fecha as string) as string
		if (len(trim(Fecha))> 0 ) then
			FormatoFechaFE =  mid(Fecha,7,4) + "/" + mid(Fecha,4,2) + "/" + mid(Fecha,1,2)
		else
			FormatoFechaFE = ""
		end if
			
	end Function

public Function negativo(byval cadena as string) as string
		if(len(trim(cadena))>4) then
			negativo = "(" + trim(mid(cadena,2)) + ")"
		else
			negativo = cadena
		end if
	end Function
	
public Function negativoFE(byval cadena as string) as string
		if(len(trim(cadena))>4) then
			negativoFE = "" + trim(mid(cadena,2)) + ""
		else
			negativoFE = cadena
		end if
	end Function
	
public function format(byval cadena as string) as string
		Dim dec, ent,  aux as string
		dim i, j as integer
		j=0
	    for i=1 to len(cadena)
			if (mid(cadena,i,1)=".") then
				dec = mid (cadena,i)
				ent = mid(cadena,1,j)
			else
				j =j + 1
			end if
		Next
		if len(ent) > 3 and len(ent) < 6 then
			aux = mid(ent,1,len(ent)-3)
			ent = aux + "," + mid(ent,len(aux)+1)
		end if
		format =  ent + dec
	end Function
	
	
	public Function FncImprFormaPago(byval dsCompro as System.Data.DataSet) as string
		dim cad
		 '' PROY-30166 - IDEA-38863 -INICIO
		dim objClsTrsPvu As New COM_SIC_Activaciones.clsTrsPvu
		dim NroPedido As string
		dim strCdRpta As String
		dim strMjRpta As String
		dim dsCuotasxPedido As System.Data.DataSet
		dim strCuotaInicial As string = String.Empty
		dim strContrato As String = String.Empty
        dim strTelef As String = String.Empty
        dim strNrPedido As String = "'" + Trim(request.QueryString("codRefer")) + "'" 
	        
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " == INI ObtenerMontoInicialxPedidos ==")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " == Validar que pedido tenga Cuota Inicial ==")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	* NRO PEDIDO:" & strNrPedido)
		dsCuotasxPedido = objClsTrsPvu.ObtenerMontoInicialxPedidos(strNrPedido,strContrato,strTelef, strCdRpta, strMjRpta)		
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	* TOTAL REGISTROS" & dsCuotasxPedido.Tables(0).Rows.Count)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	* COD RPTA:" & strCdRpta)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "	* MSJ RPTA:" & strMjRpta)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & " == FIN ObtenerMontoInicialxPedidos ==")
		If  dsCuotasxPedido.tables(0).rows.count>0 then
			If  (dsCuotasxPedido.tables(0).rows(0)("MONTO_CUOTA_INICIAL"))>0 then
				 strCuotaInicial = Trim(cstr(dsCuotasxPedido.tables(0).rows(0)("MONTO_CUOTA_INICIAL")))
			end if
		end if	
    '' PROY-30166 - IDEA-38863 - FIN
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			5.- Imprimir Formas de Pago")
		'Dim indexTable = IIf(IsOffline,1,3)
		Dim indexTable = IIf(IsOffline,1,1)
		if dsCompro.tables(indexTable).rows.count>0 then	
			cad = "<TR WIDTH=230>"
			cad=cad + "<TD>"
			cad=cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>"  
						
			'Dim obSap as new SAP_SIC_PAGOS.clsPagos
			'dim dsPagos = obSap.Get_ConsultaPagos(Session("ALMACEN"),replace(Trim(cstr(dsCompro.tables(0).rows(0)(5))),"*",""),"")
			
			if(Not IsOffline) Then
				'SAP
				'dim dsPedido1 as System.Data.Dataset = obSap.Get_ConsultaPedido("",session("ALMACEN"),request.QueryString("codRefer"),"")			
				'FlagPrepago = False				
				'if dsPedido1.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsPedido1.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then
					FlagPrepago = True
				'end if
				'dsPedido1 = nothing					
			else
				if dsCompro.Tables(0).Rows(0).Item("TIPO_VENTA") = ConfigurationSettings.AppSettings("strTVPrepago") and dsCompro.Tables(0).Rows(0).Item("CLASE_VENTA")=ConfigurationSettings.AppSettings("strDTVAlta") then
					FlagPrepago = True
				end if
			end if
	
			
			dim i as integer
			dim c as integer=0
			If(IsOffline) Then
				if dsCompro.Tables(2).Rows.Count > 0 then
					cad=cad + "<TR><TD colspan=4>FORMAS DE PAGO"
					cad=cad + "</TD></TR>"
					Dim objOffline As New COM_SIC_OffLine.clsOffline
					Dim viaPagoNombre = objOffline.DescripcionViaPagoByCode(dsCompro.Tables(2).Rows(i).Item("Via_Pago").ToString())
					for i=0 to dsCompro.Tables(2).Rows.Count - 1
						cad=cad + "<TR>"
						if (servRec.Substring(0, 7) <> "SERECVI") then
							cad=cad + "<TD WIDTH=50% >" + viaPagoNombre + "<TD WIDTH=20% > </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(dsCompro.Tables(1).Rows(i).Item("REC_EFECTIVA")) + "</FONT></TD>"
						else
  							cad=cad + "<TD WIDTH=50% >" + viaPagoNombre + "<TD WIDTH=20% > </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cant_dec(dsCompro.Tables(1).Rows(i).Item("KWMENG")) + "</FONT></TD>"
						end if
						cad=cad + "</TR>"
					next
				end if
			Else
				If Not isnothing(dsConsultaPago) Then
					If dsConsultaPago.Rows.Count > 0 Then
					
						cad=cad + "<TR><TD colspan=4>FORMAS DE PAGO"
						cad=cad + "</TD></TR>"
						'PROY-30166 -IDEA-38863 - INICIO 
						If strCuotaInicial <> "" Then
							cad=cad + "<TR>"												
							cad=cad + "<TD WIDTH=50% >" + "CUOTA INICIAL" + "<TD WIDTH=10% > </TD></TD><TD WIDTH=40% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cstr(strCuotaInicial) + "</FONT></TD>"
							cad=cad + "</TR>"					
						End If
						'PROY-30166 -IDEA-38863 - FIN
						For i = 0 To dsConsultaPago.Rows.Count - 1
						
							If dsConsultaPago.Rows(i).Item("DEPAV_DESCPAGO") = "EFECTIVO" Then
								Efectivo = Cdbl(dsConsultaPago.Rows(i).Item("DEPAN_IMPORTE"))
								EfectivoTotal = EfectivoTotal + Efectivo
								c = c + 1							
							End If	
							
							If 	dsConsultaPago.Rows(i).Item("DEPAV_DESCPAGO")<>"EFECTIVO" Then
								cad=cad + "<TR>"							
								cad=cad + "<TD WIDTH=50% >" + dsConsultaPago.Rows(i).Item("DEPAV_DESCPAGO") + "<TD WIDTH=10% > </TD></TD><TD WIDTH=40% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + CStr(dsConsultaPago.Rows(i).Item("DEPAN_IMPORTE")) + "</FONT></TD>"
								cad=cad + "</TR>"
							End If
						Next	
						
						If c > 0 then							
							cad=cad + "<TR>"												
							cad=cad + "<TD WIDTH=50% >" + "EFECTIVO" + "<TD WIDTH=10% > </TD></TD><TD WIDTH=40% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + cstr(EfectivoTotal) + "</FONT></TD>"
							cad=cad + "</TR>"
						End if			
					
					End If
				End If 

			End if
			
			'cad=cad + "<TR><TD>&nbsp;</TD></TR>"
			If(IsOffline) Then
				'cad=cad + "<tr><td colspan=2 class=clsTexto>" + Trim(cstr(dsCompro.tables(2).rows(0)("GLOSA"))) + "</td></tr>"
			else
				'SAP 
				'cad=cad + "<tr><td colspan=2 class=clsTexto>" + obSap.Get_GlosaCampaña(Trim(cstr(dsCompro.tables(0).rows(0)(4))),Session("ALMACEN")) + "</td></tr>"
			End If
			
			cad=cad + "</TABLE>"
			cad=cad + "</TD>"
			cad=cad + "</TR>"
						
		end if

		return cad
	End Function
	'Fe Mejora
	
	public Function FncImprImporteVuelto(strEfectivo As String, strRecibido As String, strRecibidoUS As String, strEntregar As String, cuotas As Integer) as string
		Dim cad	
		Dim efectivo
		If cuotas > 0 Then
			efectivo = "0"
		Else 
			efectivo = Cstr(EfectivoTotal)
		End If
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			8.- Imprimir Vuelto")
		cad = "<TR>"
		cad = cad + "<TD>"
		cad = cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>" 
		cad = cad + "<TR><TD colspan=4>IMPORTE DE VUELTO</TD></TR>"	
		cad=cad + "<TR>"
		'cad=cad + "<TD WIDTH=50% >EFECTIVO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/.<FONT class=clsNumero>" + strEfectivo + "</FONT></TD>"				
		cad=cad + "<TD WIDTH=50% >EFECTIVO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + efectivo + "</FONT></TD>"				
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		'---------------------------------
		'Para Recibir Dolares - FE Mejoras
		'---------------------------------
		Dim RecibidoTotal As Double
		If CStr(strRecibidoUS) <> "" Then 
			RecibidoTotal = CDbl(strRecibido) + CDbl(strRecibidoUS)
		Else
			RecibidoTotal = CDbl(strRecibido)
		End If
				
		Dim recibido
		If cuotas > 0 Then
			recibido = "0"
			strEntregar = "0"
		Else 
			recibido = CStr(RecibidoTotal)	
		End If
		cad=cad + "<TD WIDTH=50% >RECIBIDO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + recibido + "</FONT></TD>"	
		'cad=cad + "<TD WIDTH=50% >RECIBIDO:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/.<FONT class=clsNumero>" + strRecibido + "</FONT></TD>"				
		'---------------------------------
		'FIN Para Recibir Dolares - FE Mejoras
		'---------------------------------
		cad=cad + "</TR>"
		cad=cad + "<TR>"
		cad=cad + "<TD WIDTH=50% >A ENTREGAR:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;S/<FONT class=clsNumero>" + strEntregar + "</FONT></TD>"				
		cad=cad + "</TR>"	
		cad=cad + "</TABLE>"
		cad=cad + "</TD>"
		cad=cad + "</TR>"					
		
		return cad
	End Function
	
	public Function FncImprRecargaDTH(strCodPago As String, strFechaExpira As String, strNroRecarga As String) as String
		Dim cad	
		
		cad = "<TR>"
		cad = cad + "<TD>"
		cad = cad + "<TABLE WIDTH=230 CELLSPACING=0 CELLPADDING=0 BORDER=0 class=clsVigencia>" 
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Código de Pago:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strCodPago + "</FONT></TD>"				
		cad = cad + "</TR>"
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Fecha de Expiración:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strFechaExpira + "</FONT></TD>"				
		cad = cad + "</TR>"
		cad = cad + "<TR>"
		cad = cad + "<TD WIDTH=50% >Código Recarga:<TD WIDTH=20%> </TD></TD><TD WIDTH=30% ALIGN=RIGHT >&nbsp;<FONT class=clsNumero>" + strNroRecarga + "</FONT></TD>"				
		cad = cad + "</TR>"		
		cad = cad + "<TR>"
		cad = cad + "<TD colspan=4 >&nbsp;</TD>"
		cad = cad + "</TR>"		
		cad = cad + "</TABLE>"
		cad = cad + "</TD>"
		cad = cad + "</TR>"					
		
		return cad
	End Function	
	
	public Function FncImprMensajeAcepGarantia() as string
		Dim cad
		cad = "<TR>"
		cad=cad + "<BR/>"
		cad=cad + "<TD align='center'class=clsTexto>" + ConfigurationSettings.AppSettings("mensajeTicketEquipoPrepago") + "</td>"
		cad=cad + "</TR>"
		return cad
	End Function		
	'INICIO Kerly Adriana entra cuando es NC 
	public Function FncImprNotasCredito(byval dsCompro as System.Data.DataSet,byval dtCabecera as System.Data.DataTable) as string
		Dim cad as string
		DIM gStrRazonSocial AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRazonSocial")
		DIM gStrMarca AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrMarca")
		DIM gStrRUC AS string = System.Configuration.ConfigurationSettings.AppSettings("gStrRUC")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "			9.- Imprimir Notas de Credito")

		if dsCompro.tables(0).rows.count>0 then 
			select case (Trim(cstr(dtCabecera.rows(0)(5))))
				case "E7"
					cad=cad + "<TR>"
					cad=cad + "<TD align='center' STYLE='FONT-SIZE: 8pt;' colspan='2' class=clsTexto>" + ConfigurationSettings.AppSettings("FE_MensajeNotasCredito") + "</TD>"
					cad=cad + "</TR>"
			end select
		end if		
		return cad
	end Function
	'FIN Kerly Adriana

		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
                  <input id="hidIgvActual" type="hidden" name="hidIgvActual" runat="server"><!--PROY 31766-->
	          <input id="hidIgvActualP" type="hidden" name="hidIgvActualP" runat="server"><!--PROY 31766-->
		<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:fe_imprimir();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<TABLE WIDTH="240" border="0" CELLSPACING="0" CELLPADDING="0">
				<% 
	'*** VARIABLES LOCALES ***
    dim oficinaVenta as string
    dim strTipoOficina as string
    dim fechaVenta as string
    dim docSap as string
    dim msgErr as string=""
    Dim isOffline as Boolean=false
    '*** FIN VARIABLES LOCALES *** 
        
    '******************************************************
    Dim dsResult as System.Data.DataSet
    'Dim obSap as new SAP_SIC_PAGOS.clsPagos
    Dim objOffline As New COM_SIC_OffLine.clsOffline
    Dim objFileLog As New SisCajas.SICAR_Log
    '******************************************************
    Dim objAct as New COM_SIC_Activaciones.clsConsultaMsSap
    '******************************************************
    Dim dsConsultaFE as System.Data.DataSet
    Dim dtCabecera as System.Data.DataTable
    Dim dtDetalle as System.Data.DataTable
    Dim dtTexto as System.Data.DataTable
    
    strCodUsuario = session("USUARIO")
    strNomUsuario = session("NOMBRE_COMPLETO")
    
    FlagCuotas=false
    NumeroCuotas=0
    
	strTipoOficina = Session("CANAL")
	oficinaVenta = Session("ALMACEN")
	fechaVenta = right("00" & Now.Day, 2) & "/" & right("00" & Now.Month, 2) & "/" & right("0000" & Now.Year, 4) 'now.date.tostring("d") ' Session("FechaAct")					
	
	'*****lee parametros
	docSap = request.QueryString("codRefer")
	Reimpresion = Request.QueryString("Reimpresion")
	
	isOffline= Request.QueryString("isOffline") = "1"
	
	if Request.QueryString("Oficina")<>"" then
	  oficinaVenta = Request.QueryString("Oficina")
	end if
	Dim reimp As String
	if trim(Reimpresion)<>"" then
			reimp = "SI"
		else
			reimp = "NO"
		end if
	if trim(session("CodImprTicket"))="" then
	   %>
				<script language="javascript">
			 alert('ADVERTENCIA!!! Va a Reimprimir un TICKET via windows asegurese de imprimirlo en una impresora diferente a la ticketera.');
				</script>
				<%
	end if
		Dim dtFechaIGV as string=""
	if(isOffline) then
		dsResult = objOffline.GetConsultaPedido(docSap)
		dtFechaIGV = Convert.ToDateTime(Convert.ToString(dsResult.Tables(0).Rows(0).Item("AUDAT"))).ToString("dd/MM/yyyy")
	else
	
		strIdentifyLog = cstr(docSap)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "------------------------------------------------------------------------")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Inicio Impresion del Documento (" & docSap & ") - REIMPRESIÓN ( " & reimp & ")")
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fecha: " & fechaVenta)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "USUARIO: " & strCodUsuario)
        objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   PDV    : " & oficinaVenta)
		
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Consulta Comprobante - Metodo: ConsultarComprobante SP: SSAPSS_PAPERLESS_FE")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "			 IN : " & docSap)
		dsResult = objAct.ConsultarComprobante(Cstr(docSap))
		
		If dsResult.Tables(0).Rows.Count > 0 Then
			dsConsultaFE = objAct.ConsultarComprobante(Cstr(docSap))
			dtCabecera=dsConsultaFE.Tables(0)
			dtDetalle=dsConsultaFE.Tables(1)
			dtFechaIGV= Convert.ToDateTime(Convert.ToString(dtCabecera.Rows(0).Item("FECHA_EMISION"))).ToString("dd/MM/yyyy")
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT : Consulta de Datos exitoso!")
		Else
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   OUT : No se encontraron Datos para el Nro de Doc: " & docSap )
			msgErr = "No se encontraron Datos para el Nro de Documento: " & docSap
		End If 
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Consulta Comprobante")
			
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Consulta Formas de Pago - Metodo: ConsultarFormasPago SP: SSAPSS_FORMASDEPAGO")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "			 IN : " & docSap)
		dsConsultaPago = objAct.ConsultarFormasPago(Cstr(docSap))
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Fin Consulta Formas de Pago")
		
		If Not IsDBNull(dsResult.Tables(0).Rows(0).Item("PEDIV_CODVENDEDOR")) Then 
			CodVendedor = dsResult.Tables(0).Rows(0).Item("PEDIV_CODVENDEDOR")
		Else
			 CodVendedor = ""
		End If
		
		If Len(Trim(CodVendedor)) > 0 Then
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "   Inicio Consultar de Vendedor - Metodo: ConsultaVendedor SP:SISACT_VENDEDORES_MSSAP_CONS")
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "			IN Cod Vendedor: " & CodVendedor)
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "			IN Punto de Venta: ")
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "IN Estado: 1")
			
		
			dtVendedor = obUbigeo.ConsultaVendedor(CodVendedor.PadLeft(10,"0"), "", 1)
						
			'Nomvendedor = IIf(IsDBNull(dtVendedor.Rows(0).Item("VEND_NOMBRE")), "", dtVendedor.Rows(0).Item("VEND_NOMBRE"))
			If (Not dtVendedor Is Nothing) AndAlso (dtVendedor.Rows.Count > 0) Then 			
				If IsDBNull(dtVendedor.Rows(0).Item("VEND_NOMBRE")) Then
					Nomvendedor = ""
				Else
					Nomvendedor = dtVendedor.Rows(0).Item("VEND_NOMBRE")
				End If
			Else
				CodVendedor = ""
				NomVendedor = ""	
			End If					
		Else
			CodVendedor = ""
			NomVendedor = ""
		End If
		
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Cod Vendedor: " & CodVendedor)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "OUT Nom Vendedor: " & NomVendedor)
		
		Dim dtIGV As System.Data.DataTable	
		dtIGV = Session("Lista_Impuesto")
		if dtIGV.rows.count = 0 then
		%>
			<script language="javascript">
				alert('No existe IGV configurado');
			</script>
		<%Response.end
		end if
		
		Dim IGVactual As string 
		IGVactual = "0"
		
		Const_IGV = "0"
		
		For Each row As System.Data.DataRow In dtIGV.Rows
			If (CDate(dtFechaIGV) >= CDate(row("impudFecIniVigencia").ToString.Trim) And CDate(dtFechaIGV) <= CDate(row("impudFecFinVigencia").ToString.Trim) And CInt(row("impunTipDoc").ToString.Trim) = 0) Then
				IGVactual = Math.Round(CDec(row("IGV").ToString.Trim()) / 100, 2).ToString
				Const_IGV = Cint((CDec(IGVactual)*100)).ToString
				Exit For
			End If    
		Next
		
		
		'***Para Direccion del Cliente
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "----------------------------------")
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Consulta Direccion")
		
		If len(Trim(cstr(dtCabecera.rows(0)(30)))) <> 0 Then 
			'Direccion = Trim(cstr(dtCabecera.rows(0)(30)))			
			Direccion=IIf(IsDBNull(dtCabecera.rows(0)(30)), "||", Trim(cstr(dtCabecera.rows(0)(30))))
			objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Direccion : " & Direccion)			
			
			arrayUbigeo = Split(Direccion, "||")
			'CodUbigeo = arrayUbigeo(1).ToString
			CodUbigeo = IIf(IsDBNull(arrayUbigeo(1).ToString), "", arrayUbigeo(1).ToString)
			DirecUbicacion = IIf(IsDBNull(arrayUbigeo(0).ToString), "", arrayUbigeo(0).ToString)
			'dtResultDireccion=obUbigeo.GetDireccion(docSap,"","")
		
			If len(Trim(cstr(CodUbigeo))) <> 0 Then
				Departamento = CodUbigeo.Substring(0,2)
				Provincia = CodUbigeo.Substring(2,3)
				Distrito = CodUbigeo.Substring(5,(CodUbigeo.ToString().Trim().Length - 5))
				
				'**Consultamos Departamento
				dtResultDep = obUbigeo.GetDepartamento(Departamento,"1")
				If dtResultDep.rows.count>0 then 
					NomDepartamento = dtResultDep.rows(0)(1)
				Else
					NomDepartamento = ""
				End If
				
				'**Consultamos Distrito
				dtResultDist = obUbigeo.GetDistrito(Distrito,Provincia,Departamento,"1")
				If dtResultDist.rows.count>0 Then 
					NombreDistrito = dtResultDist.rows(0)(4) 
				Else
					NombreDistrito = ""
				End If	
	
			else				
				DirecUbicacion = "--"
				NomDepartamento = "--"
				NombreDistrito = "--"					
			End If 			
		Else
			DirecUbicacion = "--"
			NomDepartamento = "--"
			NombreDistrito = "--"
		End If
		
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "DirecUbicacion : " & DirecUbicacion)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NomDepartamento : " & NomDepartamento)
		objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "NombreDistrito : " & NombreDistrito)
        
	end if
	
	if msgErr.trim().length>0 then
		response.write("<script language=jscript> alert('"+msgErr+"'); </script>")	
	end if
	               
           	
%>
								<!--Inicio de Cabecera-->
				<%
  cadenaP=cadenaP &  FncImprCabecera(dsResult, dtCabecera)
%>
				<!--Fin de Cabecera-->
				<!--Inicio de Cliente-->
				<%
   cadenaP=cadenaP & FncImprCliente(dsResult,dtCabecera)
%>
				<!--Fin de Cliente-->
<!--Inicio de Detalle de Ventas-->
				<%
  cadenaP=cadenaP & FncImprDetVentas(dsResult, dtDetalle)
%>
<!--Fin de Detalle de Ventas-->
				<!--Inicio de Recarga-->
				<%
	If Request.QueryString("Reimpresion") = ""   then
	'Response.Write( FncImprRecargas(dsResult))
  end if
%>
<!--Fin de Recarga-->
<!--Inicio de Recarga DTH-->
<%
	if Request.QueryString("strCodPago") <> "" then
		cadenaP=cadenaP & FncImprRecargaDTH(Request.QueryString("strCodPago"),Request.QueryString("strFechaExpira"),Request.QueryString("strNroRecarga"))
	end if
%>
<!--Fin de Recarga DTH-->
				<!--Inicio de Cuotas-->
				<%
  'Response.Write( FncImpCuotas(dsResult))
%>
				<!--Fin de Cuotas-->
				<!--Inicio Totales-->
				<%
  	 cadenaP=cadenaP & FncImprTotales(dsResult,dtCabecera)
%>
				<!--Fin Totales-->
				<!--Inicio Formas de Pago-->
				<%				
     cadenaP=cadenaP & FncImprFormaPago(dsResult)
%>
				<!--Fin Formas de Pago-->
				<!--Inicio de Glosa Prepago-->
				<%
  	Dim NroCuotas As Integer
	'NroCuotas = CInt(IIf(IsDBNull(dtCabecera.Rows(0).Item("FAELN_NROCUOTA")), "0" , dtCabecera.Rows(0).Item("FAELN_NROCUOTA")))
	If IsDBNull(dtCabecera.Rows(0).Item("FAELN_NROCUOTA")) Then
		NroCuotas = "0"
	Else
		NroCuotas = dtCabecera.Rows(0).Item("FAELN_NROCUOTA")
	End If
	
	If NroCuotas = 0 then 
  	cadenaP=cadenaP & FncPiePrepago()
  	End If
  'end if  
%>
				<!--Fin de Glosa Prepago-->
				<!--Inicio de Cuotas-->
				<%
	If NroCuotas > 0 then 
		cadenaP=cadenaP & FncPieCuotas(dsResult,NroCuotas)
	End If
%>
				<!--Fin de Cuotas-->
				<!--Inicio Comentarios-->
				<%
  cadenaP=cadenaP & FncImprVendedores()
%>
				<!--Fin Comentarios-->
				<!--Inicio Importe de vuelto-->
				<%
if trim(Reimpresion)="" and Request.QueryString("strEfectivo") <> "" then
	 cadenaP=cadenaP & FncImprImporteVuelto(Request.QueryString("strEfectivo"), Request.QueryString("strRecibido"), Request.QueryString("strRecibidoUS"), Request.QueryString("strEntregar"), NroCuotas )
end if
%>
				<!--Fin Importe de vuelto-->
				<!--Inicio Mensaje de Aceptacion de Condiciones de garantia (solo prepago)-->
				<%
	If NroCuotas = 0 Then 
		cadenaP=cadenaP & FncImprMensajeAcepGarantia()
	End If  
%>
<!--Fin Mensaje de Aceptacion de Condiciones de garantia (solo prepago)-->
<!--Inicio Glosa de Notas de Credito-->
<%
		cadenaP=cadenaP & FncImprNotasCredito(dsResult, dtCabecera)
%>
<!--Fin Glosa de Notas de Credito-->
<!--Inicio Impresion-->
<%
	objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "Fin Impresion del Documento (" & docSap & ") - REIMPRESIÓN ( " & reimp & ")")
	objFileLog.Log_WriteLog(pathFile, strArchivo, strIdentifyLog & "- " & "------------------------------------------------------------------------")
		Response.Write (cadenaP)
%>
<!--Fin Impresion-->
			</TABLE>
			<!--PROY-24724 - Iteracion 2 Siniestros-->
			<input id="hidEstdFinalSini" type="text" name="hidEstdFinalSini" runat="server"  style="display:none">
			<input id="hidCodMaterialSiniestro" type="text" name="hidCodMaterialSiniestro" runat="server"  style="display:none">
			
		</form>
	</body>
</HTML>