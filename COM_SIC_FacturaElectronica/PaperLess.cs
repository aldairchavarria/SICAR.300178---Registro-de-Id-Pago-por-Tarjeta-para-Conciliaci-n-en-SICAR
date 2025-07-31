using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Text;
using COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica;
using COM_SIC_Entidades.DataPowerRest.RestReferences.GenerarFacturaElectronica;
using COM_SIC_Activaciones;

namespace COM_SIC_FacturaElectronica
{
	/// <summary>
	/// Summary description for PaperLess.
	/// </summary>
	public class PaperLess
	{
		// PBI000002128547
		string nameFile  = ConfigurationSettings.AppSettings["constNameLogPoolPagos"].ToString();
		string pathFile = ConfigurationSettings.AppSettings["constRutaLogRecarga"].ToString();
		// PBI000002128547
		string idAplicacion = ConfigurationSettings.AppSettings["CodAplicacion"].ToString();
		string nombreAplicacion = ConfigurationSettings.AppSettings["constAplicacion"].ToString();	
		/*
		string nameFile = "Log_PaperLess" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		string pathFile =ConfigurationSettings.AppSettings["constRutaLogWS_Sans"].ToString();		
		*/	
		string tDocSinergia = string.Empty;
		COM_SIC_Seguridad.Configuracion objseg;

		public static string K_CURRENT_USER =""; // PROY-140621
		private double K_MONTO_FACTURA = 0;//PROY-140621
		private string K_SERIE_NUMERO_FACTURA = "";//PROY-140621
		string strsaltodelinea = "\\n";//PROY-140621
		public PaperLess()
		{		
		}		

		
		public string GenerarFacturaElectronicaMSSAP
			(string nroDocumento,
			  Int64 IdPago
			,string oficina
			,string referencia
			,string sociedad
			)
		{
			PaperLessWS.Online objFE = new PaperLessWS.Online();
			string respuesta = string.Empty;
			string codigo = string.Empty;
			string mensaje = string.Empty;
			string tipoDocumento = string.Empty;
			string estado=string.Empty;
			string tdocRefe=string.Empty;
			string eMailCliente = string.Empty;
			string trama = string.Empty;
			string origen =string.Empty;
			string login = string.Empty ;
			string clave = string.Empty; 

			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
		    string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			string KEY_PAPERLESS = ConfigurationSettings.AppSettings["KEY_PAPERLESS"];
			try
			{
				COM_SIC_Seguridad.ClsConexion objDatosConex;
				objLog.Log_WriteLog(pathFile, strArchivo,"Iniciando SEG_SEGURIDAD_NET :  ");
				objseg = new COM_SIC_Seguridad.Configuracion(KEY_PAPERLESS);
				objDatosConex = objseg.GetConexion();
			
				login = objDatosConex.GetUsuario;
				
				clave = objDatosConex.GetPassword;
								
			}
			catch
				(Exception ex)
			{
				
				objLog.Log_WriteLog(pathFile, strArchivo," Mensaje:  " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo," Traza:  " + ex.StackTrace);
				throw ex;

			}

			string cod_tipooperacion = string.Empty; //ADD: PROY 32815 - RMZ
			string cod_establecimiento = string.Empty; //ADD: PROY 32815 - RMZ
			bool blCodProducto = true; //ADD: PROY-140336
			
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Metodo Generar Factura Electronica");
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp nroDocumento:" + nroDocumento);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp oficina:" + oficina);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp referencia:" + referencia);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp sociedad:" + sociedad);
			try 
			 {
				//INICIO PROY-140621
				if (ConfigurationSettings.AppSettings["ConsFlagFacturadorNuevo"] == "1")
				{
					objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "ConsFlagFacturadorNuevo = 1");
		
				}
				else
				{
					objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "ConsFlagFacturadorNuevo = 0");
						
				}

				trama = GenerarTramaDocumentoMSSAP(nroDocumento, oficina,sociedad, ref tipoDocumento, ref eMailCliente,
					ref cod_tipooperacion,ref cod_establecimiento,ref blCodProducto); //MOD: PROY 32815 - RMZ //PROY-140336
				//FIN PROY-140621
				
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out trama:" + trama);
				
				clsFacturaElectronica objDatosFact = new clsFacturaElectronica();
				
			 }
			catch(Exception ex)
			 {
			   trama="";
			   objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Error de GenerarTramaDocumentoMSSAP :  " + ex.Message.ToString());
			 }


			//trama = GenerarTramaDocumentoMSSAP(nroDocumento, oficina, ref tipoDocumento, ref eMailCliente);
			
			//COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			
			objLog.Log_WriteLog(pathFile, strArchivo, nroDocumento + "- " + " Inicia consulta de punto de venta de pedido: ConsultarPuntoVentaPedido");
			objLog.Log_WriteLog(pathFile, strArchivo, nroDocumento + "- " + "SP: " + "PKG_MSSAP.SSAPT_VALIDA_PDV_VIRTUAL");
			objLog.Log_WriteLog(pathFile, strArchivo, nroDocumento + "- " + "ID de Pedido : " + nroDocumento);

			COM_SIC_Activaciones.clsConsultaMsSap objConsultaMsSap = new COM_SIC_Activaciones.clsConsultaMsSap();
			string P_COD_RPTA = string.Empty;
			string P_MSGE_RPTA = string.Empty;
			objConsultaMsSap.ConsultarPuntoVentaPedido(nroDocumento, ref P_COD_RPTA, ref P_MSGE_RPTA);

			objLog.Log_WriteLog(pathFile, strArchivo, nroDocumento + "- " + " OUT K_COD_RPTA :" + P_COD_RPTA);
			objLog.Log_WriteLog(pathFile, strArchivo, nroDocumento + "- " + " OUT K_MSGE_RPTA :" + P_MSGE_RPTA);

			//Si no es punto de venta virtual 
			if(P_COD_RPTA == ConfigurationSettings.AppSettings["CodigoRespuestaNoEsCACVirtual"].ToString())
			{
				try		
				{		 
					if(blCodProducto) //PROY-140336
					{
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "WEBSERVICE OnlineGeneration");
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp RUC:" + ConfigurationSettings.AppSettings["FE_PPL_Ruc"]);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp TRAMA:" + trama);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp TiPO_FOLACION:" + ConfigurationSettings.AppSettings["FE_PPL_TipoFoliacion"]);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp TIPO_FOLACION_ESPEDIFICADA:" + "TRUE");
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp TiPO_RETORNO:" + ConfigurationSettings.AppSettings["FE_PPL_TipoRetorno"]);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp TiPO_RETORNO_ESPEDIFICADO:" + "TRUE");

						GenerarFacturaElectronica(ConfigurationSettings.AppSettings["FE_PPL_Ruc"],login,clave,trama,ConfigurationSettings.AppSettings["FE_PPL_TipoFoliacion"],ConfigurationSettings.AppSettings["FE_PPL_TipoRetorno"],
							out codigo,out mensaje);

						if (codigo!= "0")
						{
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Inicio Registro de errores de OnlineGeneration");
							clsFacturaElectronica objDatosFact = new clsFacturaElectronica();
							string sCodRpta ="", sMsjRpta="";
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pIdPago:" + IdPago);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pComprobante:" + K_SERIE_NUMERO_FACTURA);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pCodError:" + codigo);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pMensaje:" + mensaje);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pMonto:" + K_MONTO_FACTURA.ToString());
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pAplicacion:" + ConfigurationSettings.AppSettings["USRAPP"]);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp pUsuaCreacion:" + K_CURRENT_USER);
							objDatosFact.RegistrarEventoError(IdPago,K_SERIE_NUMERO_FACTURA,codigo,mensaje,K_MONTO_FACTURA.ToString(),ConfigurationSettings.AppSettings["USRAPP"],K_CURRENT_USER,out sCodRpta,out sMsjRpta);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out sCodRpta:" + sCodRpta);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out sMsjRpta:" + sMsjRpta);
							objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Fin Registro de errores de OnlineGeneration");
						}
						objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Codigo:" + codigo);
						objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Mensaje:" + mensaje);
						objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Fin del WEBSERVICE OnlineGeneration");
						objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "*****************************************************************");
					}
					else{				
						objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "No se envio a PAPERLESS"); //PROY-140336
					}
				
			}

			catch(Exception ex)
			{
				mensaje=ex.Message.ToString();
			}
			}		
			else
			{
				codigo=ConfigurationSettings.AppSettings["CodigoRespuestaExitoPaperless"].ToString();
			}

			//ENviado
			if(codigo=="0")
			{			
				//FE Mejoras
				if((origen=="B" & tipoDocumento=="E7")|| tipoDocumento=="E3")	
				{
					estado=ConfigurationSettings.AppSettings["FE_HC_Aceptado"]; 
				}
				else
				{
					estado=ConfigurationSettings.AppSettings["FE_HC_Enviado"]; 				
				}
				//FE Mejoras
								
			}

			//PEndiente
			if(codigo!="0")
			{
				estado=ConfigurationSettings.AppSettings["FE_HC_Pendiente"];
				mensaje="";
			}

			//FE-Mejora Salto Correlativo

			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Grabando Estado de Flag Paperless");
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Nro de Documento:" + nroDocumento);
			objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Estado:" + estado);

			COM_SIC_Cajas.clsCajas setFlagPaperless = new COM_SIC_Cajas.clsCajas(); 
			string rpt=setFlagPaperless.SP_UPD_FLAG_PAPER(nroDocumento,estado,"E") ;
			if (rpt=="0")
			{
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Codigo_Respuesta:" + rpt);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Descripción: Se actualizo Correctamente");
			}
			else 
			{	
				if (rpt=="1")
				{
					objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Codigo_Respuesta:" + rpt);
					objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Descripción: Documento no existe");
				}
				else
				{
					objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Ocurrio un error al Grabar.");
				}
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "No se Procedio la Actualización.");
			}
			//FE-Mejora Salto Correlativo

			//Para el SP que devuelve el HASHCODE - Kerly Adriana
			COM_SIC_Activaciones.clsTrsMsSap objTrsMsSap = new COM_SIC_Activaciones.clsTrsMsSap();

			DataSet dsResultado;
//			DataTable dtResultado;
			if (referencia !="")
			{
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "ACTUALIZAR HASHCODE");
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Sociedad:" + sociedad);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Tipo de Documento:" + tDocSinergia);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Origen:" + origen);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Serie Sunat:" + referencia);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Correlativo Sunat:" + referencia.Split('-')[1]);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp HashCode:" + mensaje);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Enviado:" + estado);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Correo del Cliente:" + eMailCliente);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Tipo Operacion:" + cod_tipooperacion); //ADD: PROY 32815 - RMZ
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	inp Codigo Establecimiento SUNAT:" + cod_establecimiento); //ADD: PROY 32815 - RMZ
				//refenrencia
				string serieSunat = referencia.Split('-')[0];
				int correlativoSunat = int.Parse(referencia.Split('-')[1]);
				

				dsResultado = objTrsMsSap.Set_HashCode(sociedad, tDocSinergia, origen, serieSunat, 
					correlativoSunat, mensaje, estado, eMailCliente,cod_tipooperacion,cod_establecimiento); //MOD: PROY 32815 - RMZ

				//dtResultado = dsResultado.Tables[0];

				//				if (dtResultado.Rows[0][0].ToString() == "E" )
				//				{
				//					mensaje = dtResultado.Rows[0][3].ToString();
				//					objLog.Log_WriteLog(pathFile, strArchivo,  "- " + "out Mensaje:" + mensaje);
				//				}
				//				else
				//				{
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Mensaje:" + mensaje);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	out Mensaje:" + "Se actualizo Correctamente");
				//				}
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Fin del SP ACTUALIZAR HASHCODE");
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "*****************************************************************");
			}
			else
			{
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "	La Referencia:" + referencia);
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "Fin del SP ACTUALIZAR HASHCODE");
				objLog.Log_WriteLog(pathFile, strArchivo,nroDocumento + " - " + "*****************************************************************");
			}

			return estado;
		}

		
		public string GenerarTramaDocumentoMSSAP(string NroDocumento, string oficina,string sociedad,
												ref string tipoDocumentoSunat, ref string eMailCliente, ref string cod_operacion,
												ref string cod_establecimiento,ref bool blCodProducto) //MOD: PROY 32815 - RMZ //PROY-140336
		{
			COM_SIC_Activaciones.clsConsultaMsSap objConsultaMsSap = new COM_SIC_Activaciones.clsConsultaMsSap();
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log(); //ADD: PROY 32815 - RMZ
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile); //ADD: PROY 32815 - RMZ
			DataSet dsBolFac;
			DataSet dsCliente;
			DataTable dtCabecera;
			DataTable dtDetalle;
			DataTable dtCliente;
			DataSet dsParam;
			
			string strGrupo = ConfigurationSettings.AppSettings["constDisclaimerCodGrupo"];	

			COM_SIC_Cajas.clsCajas objSicarDB = new COM_SIC_Cajas.clsCajas();

			dsParam = objSicarDB.FP_ConsultaParametros(strGrupo);

			string strCodMat = ConfigurationSettings.AppSettings["constDisclaimerIdMateriales"];
			string strCodLabDis = ConfigurationSettings.AppSettings["constDisclaimerIdEtiqueta"];
			string strCodTamLab = ConfigurationSettings.AppSettings["constDisclaimerIdTamEtiqueta"];
			string strCodMateriales="";
			string strLabelDiscleimer="";
			//INICIO PROY-140621
			if(ConfigurationSettings.AppSettings["ConsFlagFacturadorNuevo"] == "0")
			{
				strsaltodelinea = "\n";
			}
			//FIN PROY-140621
			for(int idx=0; idx<dsParam.Tables[0].Rows.Count; idx++)
			{
				if(dsParam.Tables[0].Rows[idx]["SPARN_CODIGO"].ToString() == strCodLabDis)
				{
					strLabelDiscleimer = dsParam.Tables[0].Rows[idx]["SPARV_VALUE"].ToString();
				}
				if(dsParam.Tables[0].Rows[idx]["SPARN_CODIGO"].ToString() == strCodMat)
				{
					strCodMateriales = dsParam.Tables[0].Rows[idx]["SPARV_VALUE"].ToString();
				}
			}


			// INICIO - PROY 140174
			string codGrupo= ConfigurationSettings.AppSettings["ParamGrupoUBL"];

			string versionUbl = "";
			string TipoOperacion_1 = "";
			string TipoOperacion_2 = "";
			string codigoCargoDescuento = "";	
			string CodPrecioTG = "";
			string TipoImpuestoIGV = "";
			string CodAfectaTG = "";
			string CodTributoTG = "";
			string NomTributoTG = "";
			string CodTipoTributoTG = "";
			//INI :: INC000001591136
			string TipoImpuestoINA = "";
			string CodAfectaINA = "";
			string CodTributoINA = "";
			string NomTributoINA = "";
			string CodTipoTributoINA = "";
			string codMaterialDeduciblePM = "";
			string codMaterialPrimaPM = "";
			string unidadMedida = "";
			string codRecargaVirtual = "";
			//FIN :: INC000001591136
			//Iniciativa-770
			string sFormaPago = "", sContado_Valor = "", sCredito_Valor = "", sCuota_Valor = "",sFormaPagoDocsPermitidos="",sFP_NotaCredito="",sTipoDocOrigen = "";
			//Iniciativa-770
			string codBoni = ""; //JRM - COBRO DELIVERY
			COM_SIC_Cajas.clsCajas objParametro = new COM_SIC_Cajas.clsCajas();
			DataSet dsParametro = objParametro.ObtenerParamByGrupo(Convert.ToInt32(codGrupo));
		  

			foreach(DataRow _item in dsParametro.Tables[0].Rows)
			{
				if(_item["PARAV_VALOR1"].ToString() == "ENEX1")
				{
					versionUbl = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "ENEX2_1")
				{
					TipoOperacion_1 = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "ENEX2_2")
				{
					TipoOperacion_2 = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEDR3")
				{
					codigoCargoDescuento = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DE7")
				{
					CodPrecioTG = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM5")
				{
					TipoImpuestoIGV = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM6")
				{
					CodAfectaTG = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM8")
				{
					CodTributoTG = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM9")
				{
					NomTributoTG = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM10")
				{
					CodTipoTributoTG = _item["PARAV_VALOR"].ToString();
				}
                                //INI :: INC000001591136
				if(_item["PARAV_VALOR1"].ToString() == "DEIM5_INA")
				{
					TipoImpuestoINA = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM6_INA")
				{
					CodAfectaINA = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM8_INA")
				{
					CodTributoINA = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM9_INA")
				{
					NomTributoINA = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DEIM10_INA")
				{
					CodTipoTributoINA = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DE6_1")
				{
					codMaterialDeduciblePM = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DE6_2")
				{
					codMaterialPrimaPM = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DE3")
				{
					unidadMedida = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "DE6_3")
				{
					codRecargaVirtual = _item["PARAV_VALOR"].ToString();
				}
                                //FIN :: INC000001591136
				//Iniciativa-770 Inicio
				if(_item["PARAV_VALOR1"].ToString() == "FP_VALOR")
				{
					sFormaPago = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "CONTADO_VALOR")
				{
					sContado_Valor = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "CREDITO_VALOR")
				{
					sCredito_Valor = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "Cuota_Valor")
				{
					sCuota_Valor = _item["PARAV_VALOR"].ToString();
				}

				if(_item["PARAV_VALOR1"].ToString() == "FP_permitido")
				{
					sFormaPagoDocsPermitidos = _item["PARAV_VALOR"].ToString();
				}
				
				///CREDITO_VALOR,Cuota_Valor
				//Iniciativa-770 Fin
				if(_item["PARAV_VALOR1"].ToString() == "DEIM6_BONI")
				{
					codBoni = _item["PARAV_VALOR"].ToString();
				}
			}
			// FIN - PROY 140174 - IDEA 140281

			string trama = string.Empty;
			string neto = string.Empty;
			decimal descuento = 0;
			//string docReferencia = string.Empty;
			string cadena=string.Empty;
			string cadena2=string.Empty;
			string serie=string.Empty;
			string correlativo=string.Empty;
			decimal T_VN_GRATUITAS=0;
			decimal T_DESCUENTOS=0;
			string fechamotivo;
			string codMaterial; //INI :: INC000001591136

			//consulta al Pago - Cabecera y Detalle			
			dsBolFac = objConsultaMsSap.ConsultarComprobante(Convert.ToInt32(NroDocumento));
			dtCabecera = dsBolFac.Tables[0];
			dtDetalle = dsBolFac.Tables[1];
			
			// EN: ENCABEZADO
			trama = trama + "EN|";

			string valor=string.Empty;

			tDocSinergia = dtCabecera.Rows[0][5].ToString();

			switch(dtCabecera.Rows[0][5].ToString())
			{
				case "E1" : 
					// Factura
					valor= "01"; 
					break; 

				case "E3" : 
					// Boleta
					valor= "03"; 
					break; 

				case "E7" : 
					// Nota credito
					valor= "07";  
					break; 

				case "E8" : 
					// Nota de debito
					valor= "08"; 
					break; 
				
			}

			trama = trama + valor + "|";  // TIPODOC

			cadena=dtCabecera.Rows[0][6].ToString();
			serie=cadena.Split('-')[0];
			correlativo=cadena.Split('-')[1];
			cadena=serie+"-" + "0"+correlativo;
			K_SERIE_NUMERO_FACTURA = cadena; //PROY-140621
			trama = trama + cadena.Trim() + "|";  // NUMERACION

			

			//aumento de un cero al correlativo para  enviar a peperless
			cadena2=dtCabecera.Rows[0][27].ToString();
			
			//INI - PROY-140550
			if ( ConfigurationSettings.AppSettings["strCodTipoDocAplicaTramaMotivo"].ToString().IndexOf(valor) > -1) //"07;08" 
			{
				trama = trama + dtCabecera.Rows[0][25].ToString() + "|";					// TIPONCND  //[3]			
				trama = trama + cadena2 + "|";												// DOCREFERENCIA//[4] 					
				trama = trama + dtCabecera.Rows[0][26].ToString().Split('|')[0] + "|";		// MOTIVONCND //[5] 
				
			}
			else
			{
				trama =trama  + "|";									// TIPONCND	//[3]
				trama = trama + cadena2 + "|";							// DOCREFERENCIA //[4] 					
				trama = trama  + "|";									// MOTIVONCND//[5] 
			
			}
			//FIN- PROY-140550
			trama = trama + dtCabecera.Rows[0][38].ToString() + "|";					// FECHA_EMISION
			trama = trama + dtCabecera.Rows[0][22].ToString() + "|";					// MONEDA			
			trama = trama + " " + dtCabecera.Rows[0][4].ToString().Trim() + "|";		// RUC
			trama = trama + ConfigurationSettings.AppSettings["FE_EN_TipoIdentificadorEmisor"] + "|";  // Tipo Identificador Emisor (6)
			trama = trama + DeletePipe(dtCabecera.Rows[0][2].ToString()) + "|";			// NOMBRE_COMERCIAL EN|10
			trama = trama + DeletePipe(dtCabecera.Rows[0][1].ToString()) + "|";			// APE_RAZON_SOCIAL
			trama = trama + dtCabecera.Rows[0][36].ToString().Trim() + "|";				// Codigo Ubigeo Emisor
			trama = trama + DeletePipe(dtCabecera.Rows[0][3].ToString()) + "|";			// DOMICILIO
			trama = trama + "|";														// Departamento
			trama = trama + "|";														// Provincia
			trama = trama + "|";														// Distito
			trama = trama + dtCabecera.Rows[0][8].ToString().Trim() + "|";				// DOCID
			trama = trama + dtCabecera.Rows[0][7].ToString().Trim() + "|";				// TIPO_DOCID
			trama = trama + DeletePipe(dtCabecera.Rows[0][9].ToString()) + "|";			// NOMBRE_ADQUI
			
			//Para generar Direccion desde PVU 			
			//VARIABLES PARA OBTENER DIRECCION
			COM_SIC_Cajas.clsCajas obUbigeo = new COM_SIC_Cajas.clsCajas();
			DataTable dtResultDist;
			DataTable dtResultDep;
			DataTable dtResultDireccion;
			//variables a usar
			string Direccion;
			string DirecUbicacion;			
			string CodUbigeo;		
			string Departamento = "";
			string Provincia = "";
			string Distrito = "";
			string NomDepartamento = "";
			string NombreDistrito = "";
			//			string DireccionFinal;
			// 1. Obtenemos Direccion Almacenada en Codigos en MSSAP
			if (dtCabecera.Rows[0][30].ToString().Trim().Length != 0 )
			{
				//Llenar variables
				Direccion = dtCabecera.Rows[0][30].ToString();		
				string[] arrayUbigeo  = Direccion.Split(new char [] {'|'});
				CodUbigeo = arrayUbigeo[2];
				DirecUbicacion = arrayUbigeo[0];
				if (CodUbigeo.ToString().Trim().Length > 0)
				{ 
					Departamento = CodUbigeo.Substring(0,2);
					Provincia = CodUbigeo.Substring(2,3);
					Distrito = CodUbigeo.Substring(4,(CodUbigeo.ToString().Trim().Length - 4));

					//Consultamos Departamento
					dtResultDep = obUbigeo.GetDepartamento(Departamento,"1");
					if (dtResultDep.Rows.Count > 0) 
					{
						NomDepartamento = dtResultDep.Rows[0][1].ToString();
					}
					else
					{
						NomDepartamento = "";
					}
		
					//Consultamos Distrito
					dtResultDist = obUbigeo.GetDistrito(Distrito,Provincia,Departamento,"1");
					if (dtResultDist.Rows.Count > 0)
					{
						NombreDistrito = dtResultDist.Rows[0][4].ToString();
					}
					else
					{
						NombreDistrito = "";
					}					
					//Consultamos Direccion
//					string codres="";
//					dtResultDireccion = obUbigeo.GetDireccion(Convert.ToInt32(NroDocumento),ref codres,ref codres);
//					if(dtResultDireccion != null)
//						//if (dtResultDireccion.Rows.Count > 0)
//					{
//						DirecUbicacion = dtResultDireccion.Rows[0][33].ToString();
//					}
//					else
//					{
//						DirecUbicacion = "";
//					}					
				}
				
			}
			else
			{
				DirecUbicacion = "--";
				NomDepartamento = "--";
				NombreDistrito = "--";
			}
			
			
			//FIN

			//trama = trama + dtCabecera.Rows[0][30].ToString() + "|"; // DIRECCLIENTE
			trama = trama + DeletePipe(DirecUbicacion + " - " + NomDepartamento + " - " + NombreDistrito) + "|"; // DIRECCLIENTE EN|20
			trama = trama + dtCabecera.Rows[0][10].ToString().Trim() + "|";				// T_VN_AFECTAS
			trama = trama + dtCabecera.Rows[0][14].ToString().Trim() + "|";				// S_IGV
			//INI - PROY-140550
			trama = trama + ConfigurationSettings.AppSettings["strMontoDescuentoTramaEN"].ToString() + "|";	// T_DESCUENTOS  //[23]
			//FIN - PROY-140550
                        trama = trama + "|";														// Monto Recargos EN|24
			trama = trama + dtCabecera.Rows[0][19].ToString().Trim() + "|";				// IT_VT_CESESION
			trama = trama + dtCabecera.Rows[0][31].ToString().Trim() + "|";				// OTROSCONTRIB EN|26
			
			switch(dtCabecera.Rows[0][31].ToString())
			{
				case "1001" : 
					// Operaciones Gravadas
					neto = dtCabecera.Rows[0][10].ToString(); // T_VN_AFECTAS
					break; 
				case "1002" : 
					// Operacioens No Gravadas
					neto = dtCabecera.Rows[0][11].ToString(); // T_VN_INAFECT
					break; 
				case "1003" : 
					// Operaciones Exoneradas
					neto = dtCabecera.Rows[0][12].ToString(); // T_VN_EXONERA
					break; 
				default:
					// Otros casos
					neto = ""; 
					break;
			}
			//INI: PROY 32815 - RMZ
			DataSet dsTipoDoc;
			DataTable dtTipoDoc;
			string CodTipoOperacion = string.Empty;
			string IdDocPVUDB = string.Empty;
			string IdDocMSSAP = dtCabecera.Rows[0][7].ToString();
			COM_SIC_Activaciones.clsConsultaPvu objConsultaPvu = new COM_SIC_Activaciones.clsConsultaPvu();
			dsTipoDoc = objConsultaPvu.ConsultarTipoOperacion(string.Empty);
			dtTipoDoc = dsTipoDoc.Tables[0];
			for(int i=0; i<dtTipoDoc.Rows.Count; i++)
			{	
				if (dtTipoDoc.Rows[i][0].ToString().Substring(0,1) == "0")
				{
					IdDocPVUDB = dtTipoDoc.Rows[i][0].ToString().Substring(1,1);
				}
				else
				{
                    IdDocPVUDB = dtTipoDoc.Rows[i][0].ToString();
				}
                objLog.Log_WriteLog(pathFile, strArchivo, "	Leyendo codigo tipo documento:" + IdDocPVUDB);
				if (IdDocPVUDB == IdDocMSSAP)
				{
				CodTipoOperacion = dtTipoDoc.Rows[i][7].ToString();
				break;
				}
			}
			cod_operacion = CodTipoOperacion;
			//FIN: PROY 32815 - RMZ
			trama = trama + neto + "|"; // Total de valor venta neta EN|27
			trama = trama + dtCabecera.Rows[0][8].ToString().Trim() + "|";  //DOCID EN|28
			trama = trama + dtCabecera.Rows[0][7].ToString() + "|";  //TIPO_DOCID EN|29
			trama = trama + "|"; // Código País Emisor EN|30 
			trama = trama ; // Urbanización Emisor EN|31
			trama = trama + strsaltodelinea; //PROY-140621


			// INICIO - PROY 140174 - TAG ENEX  UBL 2.1

			// ENEX : -----
			trama = trama + "ENEX|";  
			trama = trama + versionUbl + "|"; //Version UBL  ENEX|1

			if(dtCabecera.Rows[0][7].ToString() == "1" || dtCabecera.Rows[0][7].ToString() == "6") //tipo de documento 
				{
				trama = trama + TipoOperacion_2 + "|";  // Tipo de Operación  ENEX|2  
				}
			else
			{
				trama = trama + TipoOperacion_1  + "|";   //Tipo de Operación  ENEX|2 
			}

			trama = trama + "|";   //Orden de Compra ENEX|3
			trama = trama +  dtCabecera.Rows[0][46].ToString()  + "|";   //Redondeo ENEX|4 
			trama = trama + "|";   //Total Anticipos ENEX|5
			
			string fechavencimiento = "";

			if (dtCabecera.Rows[0][26].ToString() !="")
			{
				if (dtCabecera.Rows[0][26].ToString().Split('|')[1] !="")
				{ 
					fechavencimiento = dtCabecera.Rows[0][26].ToString().Split('|')[1];

					trama = trama + fechavencimiento.Substring(0,4)+"-"+fechavencimiento.Substring(4,2)+"-"+ fechavencimiento.Substring(6,2)+ "|"; //Fecha de Vencimiento de la Factura ENEX|6
				}		
			}
			else
				{
				trama = trama + dtCabecera.Rows[0][26].ToString() + "|"; //Fecha de Vencimiento de la Factura ENEX|6
				}
  
			trama = trama + dtCabecera.Rows[0][45].ToString() + "|";   //Hora de Emisión ENEX|7  

			 
			COM_SIC_Activaciones.ClsConsultaSIA objConsultaSIA = new COM_SIC_Activaciones.ClsConsultaSIA();
			string strTipoComprobante = string.Empty;
			string strSerie = string.Empty;
			string codEstablecimiento = string.Empty;
			string codMensaje = string.Empty;
			string respMensaje = string.Empty;
			strTipoComprobante = dtCabecera.Rows[0][5].ToString().Trim(); //TIPO COMPROBANTE
			strSerie = dtCabecera.Rows[0][41].ToString().Trim(); //SERIE
			string strEstablecimientoSUNAT;
			string strEstablecimientoSUNATBD;

			objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO CONSULTA ESTABLECIMIENTO SUNAT WS -*******************");
			objLog.Log_WriteLog(pathFile, strArchivo," URL - " + ConfigurationSettings.AppSettings["RutaWS_EstablecimientoSUNAT"]);
			objLog.Log_WriteLog(pathFile, strArchivo,"	input Sociedad:" + sociedad);
			objLog.Log_WriteLog(pathFile, strArchivo,"	input strTipoComprobante:" + strTipoComprobante);
			objLog.Log_WriteLog(pathFile, strArchivo,"	input strSerie:" + strSerie);
			
			strEstablecimientoSUNAT = objConsultaSIA.ConsultaEstablecimientoWS(sociedad,strTipoComprobante,strSerie, ref codEstablecimiento,ref codMensaje, ref respMensaje);
			
			objLog.Log_WriteLog(pathFile, strArchivo,"	output codEstablecimiento:" + codEstablecimiento);
			objLog.Log_WriteLog(pathFile, strArchivo,"	output codMensaje:" + codMensaje);
			objLog.Log_WriteLog(pathFile, strArchivo,"	output respMensaje:" + respMensaje);
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN CONSULTA ESTABLECIMIENTO SUNAT WS -*******************");
			if (codMensaje == "-1")
			{
				codMensaje = string.Empty;
				respMensaje = string.Empty;
				objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO CONSULTA ESTABLECIMIENTO POR BD SIAPDV -*******************");

				strEstablecimientoSUNATBD = objConsultaSIA.ConsultaEstablecimiento(sociedad,strTipoComprobante,strSerie, ref codEstablecimiento,ref codMensaje, ref respMensaje);

				objLog.Log_WriteLog(pathFile, strArchivo,"	output codEstablecimiento:" + codEstablecimiento);
				objLog.Log_WriteLog(pathFile, strArchivo,"	output codMensaje:" + codMensaje);
				objLog.Log_WriteLog(pathFile, strArchivo,"	output respMensaje:" + respMensaje);
				objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN CONSULTA ESTABLECIMIENTO POR BD SIAPDV -*******************");

			}
			cod_establecimiento = codEstablecimiento;
				

			trama = trama + codEstablecimiento + "|";   //Código asignado por SUNAT  ENEX|8


			trama = trama + dtCabecera.Rows[0][19].ToString().Trim() +  strsaltodelinea; //PROY-140621 - //Total Precio de Venta ENEX|9  
			K_MONTO_FACTURA = Convert.ToDouble(dtCabecera.Rows[0][19].ToString().Trim());//PROY-140621
			// FIN - PROY 140174 - TAG ENEX  UBL 2.1

	// INICIO - PROY 140174 - TAG DE  UBL 2.1
			// DN: NOTAS DEL DOCUMENTO
			trama = trama + "DN|";
			trama = trama + ConfigurationSettings.AppSettings["FE_DN_Correlativo"] + "|"; // Correlativo DN|1
			trama = trama + ConfigurationSettings.AppSettings["FE_DN_CodigoLeyenda"] + "|"; // Código de la Leyenda DN|2

			//INI :: INC000001591136
			if (dtCabecera.Rows[0][13].ToString()!= "")
			{
				T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));
				objLog.Log_WriteLog(pathFile, strArchivo,"	T_VN_GRATUITAS:" + T_VN_GRATUITAS.ToString());
				if (T_VN_GRATUITAS > 0)
				{
					trama = trama + ConfigurationSettings.AppSettings["cosTransferenciaGratuita"] + "|"; // LEYENDA DN|3

					objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO INC000001591136 TRANSFERENCIA GRATUITA = 0-*******************");
					objLog.Log_WriteLog(pathFile, strArchivo,"	cosTransferenciaGratuita:" + ConfigurationSettings.AppSettings["cosTransferenciaGratuita"].ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
					

				}else
			trama = trama + DeletePipe(dtCabecera.Rows[0][20].ToString()) + "|"; // LEYENDA DN|3
			objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN INC000001591136-*******************");
			}
			//FIN :: INC000001591136

			trama = trama + strsaltodelinea; //PROY-140621  // Descripción del Tramo o Viaje  DN|4

			if (dtCabecera.Rows[0][13].ToString()!= "")
			{
				T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));

				if (T_VN_GRATUITAS > 0)
				{
					trama = trama + "DN|";
					trama = trama + (int.Parse(ConfigurationSettings.AppSettings["FE_DN_Correlativo"])+1).ToString() + "|"; // Correlativo DN|1
					trama = trama + ConfigurationSettings.AppSettings["FE_Tranferencia_Gratuitas"] + "|"; // Código de la Leyenda DN|2
					trama = trama + ConfigurationSettings.AppSettings["FE_Tranferencia_Gratuitas_DESC"] + "|"; // LEYENDA DN|3
					trama = trama + strsaltodelinea; //PROY-140621  //DN|4
				}
			}

			decimal suma_deim2 = 0;
			decimal suma_deim3 = 0;

			//JRM - PROY COBRO DELIVERY
			Decimal Defan_t_vn_gratuitas = 0;
			string flagDLV_Gra = "0";
			//JRM - PROY COBRO DELIVERY

			for(int i=0; i<dtDetalle.Rows.Count; i++) //JRM - INICIO TRAMA DETALLE
			{
				decimal de4 = 0;
				decimal de2 = 0;
				decimal de5 = 0;
				decimal monto_total = 0;
				decimal monto_final = 0;
				descuento = 0;
				decimal deim3 = 0;	
				decimal igv = 0;

				T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));

				//INI-PROY-140550
				decimal montTotal = Convert.ToDecimal(dtDetalle.Rows[i][11].ToString()); 
				decimal montFinal = Convert.ToDecimal(dtDetalle.Rows[i][12].ToString()); 
				decimal DU = montTotal - montFinal;
				decimal PU = Convert.ToDecimal(dtDetalle.Rows[i][5].ToString());
				decimal	Q  =  Convert.ToDecimal(dtDetalle.Rows[i][2].ToString());// 1.00;	
				igv= decimal.Parse(dtCabecera.Rows[0][23].ToString());
				//FIN-PROY-140550

				//JRM - PROY COBRO DELIVERY
				Defan_t_vn_gratuitas = Convert.ToDecimal(dtDetalle.Rows[i][16].ToString());
				//JRM - PROY COBRO DELIVERY

				// DE: DETALLE ÍTEM
				trama = trama + "DE|";
				trama = trama + (i + 1).ToString() + "|"; // Correlativo DE|1
				//INI -PROY-140550

                //INICIO INC000004347404
                string pos2_DE = "0.00"; // PBI000002128547
                string pos9_DE = "0.00"; // PBI000002128547
				objLog.Log_WriteLog(pathFile, strArchivo, "pos2_DE: " + pos2_DE); // PBI000002128547
				objLog.Log_WriteLog(pathFile, strArchivo, "pos9_DE: " + pos9_DE); // PBI000002128547
                decimal col11_DE = Convert.ToDecimal(dtDetalle.Rows[i][11]);
                objLog.Log_WriteLog(pathFile, strArchivo, "INC000004347404|col11_DE: " + col11_DE.ToString());
                decimal col12_DE = Convert.ToDecimal(dtDetalle.Rows[i][12]);
                objLog.Log_WriteLog(pathFile, strArchivo, "INC000004347404|col12_DE: " + col12_DE.ToString());
                //FIN INC000004347404

				if(T_VN_GRATUITAS > 0) //GRATUITA AFECTA
				{
					trama = trama + dtDetalle.Rows[i][5].ToString() + "|";  // PV_UNI_I_CONIGV DE|2
				} 
				else if(Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY GRATUITA INAFECTA
				{
					flagDLV_Gra = "1";
					trama = trama + dtDetalle.Rows[i][5].ToString() + "|";  // °2 //PROY-140589
				} //JRM - PROY COBRO DELIVERY
				else 
				{
					//trama = trama + (decimal.Round(PU-(DU * (1+(igv/100)) ) ,2)).ToString()  + "|";  // PV_UNI_I_CONIGV DE|2  // PV_UNI_I_CONIGV DE|2 //EVALENZS[2] - Posición 2 = PU – (DU * 1.18)
                    //INICIO INC000004347404
                    pos2_DE = (decimal.Round(((col11_DE - (col11_DE - col12_DE)) * (1 + (igv / 100))), 2)).ToString();
                    objLog.Log_WriteLog(pathFile, strArchivo, "INC000004347404 | PBI000002128547 | Posición 2 de sección DE: " + pos2_DE.ToString()); //PBI000002128547
                    trama = trama + pos2_DE + "|"; //PV_UNI_I_CONIGV DE|2
                    //FIN INC000004347404
                }
				//FIN -PROY-140550				 

				//INI :: INC000001591136
				codMaterial =  dtDetalle.Rows[i][3].ToString();
				string[] lisCodRecargaVirtual = codRecargaVirtual.Split(',');
				bool esRecargaVirtual = false;
				
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INICIO INC000001591136 => unidad de medida *********");
				
				foreach(string codLisRecargaVirtual in lisCodRecargaVirtual)
				{	
					if(codMaterial == codLisRecargaVirtual)
					{
						esRecargaVirtual = true;
						objLog.Log_WriteLog(pathFile, strArchivo,"	codMaterial:" + codMaterial.ToString());
						objLog.Log_WriteLog(pathFile, strArchivo,"	lisCodRecargaVirtual:" + lisCodRecargaVirtual.ToString());
						objLog.Log_WriteLog(pathFile, strArchivo,"	codLisRecargaVirtual:" + codLisRecargaVirtual.ToString());
						objLog.Log_WriteLog(pathFile, strArchivo,"	esRecargaVirtual:" + esRecargaVirtual.ToString());
						break;
					}
				}
				if (codMaterial == codMaterialDeduciblePM.ToString() || codMaterial== codMaterialPrimaPM.ToString() || esRecargaVirtual == true)
				{
					trama = trama + unidadMedida.ToString() + "|";  // UNIDADMEDIDA DE|3

					objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo," ********- FIN INC000001591136 => unidad de medida *********");
				}
				else
				{ // PBI000002128547
				//FIN :: INC000001591136
				trama = trama + dtDetalle.Rows[i][1].ToString() + "|";  // UNIDADMEDIDA DE|3
					objLog.Log_WriteLog(pathFile, strArchivo, " PBI000002128547 | Unidad de medida " + dtDetalle.Rows[i][1].ToString());
				} // PBI000002128547
				trama = trama + (decimal.Round(decimal.Parse(dtDetalle.Rows[i][2].ToString()),2)).ToString() + "|";  // UNIXITEM DE|4
				if(T_VN_GRATUITAS > 0) 
				{
					// Inicio Cambio para conformidad en TG -- V_VT_ITEM DE|5
					de4 = decimal.Parse(dtDetalle.Rows[i][2].ToString());
					de2 = decimal.Parse(dtDetalle.Rows[i][5].ToString());
					igv = decimal.Parse(dtCabecera.Rows[0][23].ToString());
					de2 = (de2 / (1 + (igv/100)));
					de5 = Math.Round(de4 * de2, 2);
					trama = trama + de5.ToString() + "|"; // V_VT_ITEM DE|5
					// Fin Cambio para conformidad en TG -- V_VT_ITEM DE|5
				}
				else if(Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY
				{
					// Inicio Cambio para conformidad en TG -- V_VT_ITEM DE|5
					de4 = decimal.Parse(dtDetalle.Rows[i][2].ToString());
					de2 = decimal.Parse(dtDetalle.Rows[i][5].ToString());
					de5 = Math.Round(de4 * de2, 2);
					trama = trama + de5.ToString() + "|"; // V_VT_ITEM DE|5
					// Fin Cambio para conformidad en TG -- V_VT_ITEM DE|5
				}
				else //JRM - PROY COBRO DELIVERY
				{
					//trama = trama + dtDetalle.Rows[i][12].ToString() + "|"; // V_VT_ITEM DE|5
                    //INICIO INC000004347404
                    pos9_DE = (decimal.Round((Convert.ToDecimal(pos2_DE) / (1 + (igv / 100))), 2)).ToString();
                    objLog.Log_WriteLog(pathFile, strArchivo, " INC000004347404 | PBI000002128547 | Posición 9 de la sección DE: " + pos9_DE.ToString()); //PBI000002128547
                    trama = trama + (decimal.Round((Convert.ToDecimal(pos2_DE) / (1 + (igv / 100))), 2)).ToString() + "|"; // V_VT_ITEM DE|5                    
                    //FIN INC000004347404
				}
					

				trama = trama + dtDetalle.Rows[i][3].ToString() + "|"; // CODPROD DE|6

				
				if (T_VN_GRATUITAS > 0)
				{
					trama = trama + CodPrecioTG + "|"; // Tipo de Precio de Venta DE|7   
				}
				else if(Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY
				{
					trama = trama + CodPrecioTG + "|"; // Tipo de Precio de Venta DE|7
				}
				else //JRM - PROY COBRO DELIVERY
				{
					trama = trama + ConfigurationSettings.AppSettings["FE_DE_TipoPrecioVenta"] + "|"; // Tipo de Precio de Venta DE|7
				}
				
				if(T_VN_GRATUITAS > 0) 
				{
					trama = trama + "0.00" + "|"; // V_VT_U_ITEM DE|8
					trama = trama + de5.ToString() + "|"; // V_VT_ITEM DE|9
				}
				else if(Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY
				{
					trama = trama + "0.00" + "|"; // V_VT_U_ITEM DE|8
					trama = trama + de5.ToString() + "|"; // V_VT_ITEM DE|9
				}
				else //JRM - PROY COBRO DELIVERY
				{
					// trama = trama + dtDetalle.Rows[i][11].ToString() + "|"; // V_VT_U_ITEM DE|8
					trama = trama + Math.Round(decimal.Parse(pos9_DE)/Q,2).ToString() + "|"; // V_VT_U_ITEM DE|8 // PBI000002128647 
					objLog.Log_WriteLog(pathFile, strArchivo, " PBI000002128647 | Posición 8 de la sección DE: " + Math.Round(decimal.Parse(pos9_DE)/Q,2).ToString()); //INC000005494294
					//trama = trama + dtDetalle.Rows[i][12].ToString() + "|"; // V_VT_ITEM DE|9
                    trama = trama + pos9_DE + "|"; // V_VT_ITEM DE|9 //INC000004347404
				}

				trama = trama + "|"; // Número de Lote DE|10
			
				trama = trama + "|"; // Marca DE|11
				trama = trama + "|"; // Pais de origen DE|12
				trama = trama ; // Nª de Posicion que el Item comprado DE|13
				// FIN - PROY 140174 - TAG DE  UBL 2.1 
				trama = trama + strsaltodelinea; //PROY-140621 


				//INI - PROY 140336				
				COM_SIC_Activaciones.clsConsultaMsSap objConsultaMsSAP = new COM_SIC_Activaciones.clsConsultaMsSap();
				string codProductoSUNAT=string.Empty;
				string codigoMensaje = string.Empty;
				string rspMensaje = string.Empty;
				string strRpta = string.Empty;
				string strObtenerCodProductoSUNAT;
				string strObtenerCodProductoSUNATBD;
				string strMsgEmail = string.Empty;
					
				objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO OBTENER CODIGO PRODUCTO SUNAT WS -*******************");
				objLog.Log_WriteLog(pathFile, strArchivo," URL - " + ConfigurationSettings.AppSettings["RutaWS_EstablecimientoSUNAT"]);
				objLog.Log_WriteLog(pathFile, strArchivo,"	input codigoMaterial:" + codMaterial);
				
				strObtenerCodProductoSUNAT = objConsultaMsSAP.ObtenerCodigoProductoWS(codMaterial,ref codProductoSUNAT,ref codigoMensaje,ref rspMensaje);
			
				objLog.Log_WriteLog(pathFile, strArchivo,"	output codProductoSUNAT:" + codProductoSUNAT);
				objLog.Log_WriteLog(pathFile, strArchivo,"	output codMensaje:" + codigoMensaje);
				objLog.Log_WriteLog(pathFile, strArchivo,"	output respMensaje:" + rspMensaje);
				objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN OBTENER CODIGO PRODUCTO SUNAT WS -*******************");
				if (codigoMensaje == "-2" || codigoMensaje == "-1" || codigoMensaje == "1")
				{
					objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO OBTENER CODIGO PRODUCTO SUNAT POR BD MSSAP -*******************");

					strObtenerCodProductoSUNATBD = objConsultaMsSAP.ObtenerCodigoProducto(codMaterial, ref codProductoSUNAT, ref codigoMensaje, ref rspMensaje);

					objLog.Log_WriteLog(pathFile, strArchivo,"	output codProductoSUNAT:" + codProductoSUNAT);
					objLog.Log_WriteLog(pathFile, strArchivo,"	output codMensaje:" + codigoMensaje);
					objLog.Log_WriteLog(pathFile, strArchivo,"	output respMensaje:" + rspMensaje);
					objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN OBTENER CODIGO PRODUCTO SUNAT POR BD MSSAP -*******************");

					
					if(codigoMensaje != "0" ) 
					{
						strMsgEmail = EnviarEmail(codMaterial,rspMensaje);
						blCodProducto = false;
					}					
				}
				if(codigoMensaje == "2")
				{
					strMsgEmail = EnviarEmail(codMaterial,rspMensaje);
					blCodProducto = false;
				}
				//FIN - PROY 140336

				if (strCodMateriales.IndexOf(dtDetalle.Rows[i][3].ToString())!=-1)  
				{
					// DEDI: DESCRIPCIÓN DEL ÍTEM
					trama = trama + "DEDI|";
					trama = trama + dtDetalle.Rows[i][4].ToString() + " " + strLabelDiscleimer.Substring(2) + "|";  // DESCRIPCION DEDI|1
					// INICIO - PROY 140174 - TAG DEDI  UBL 2.1
					trama = trama + "|"; // Notas Complementarias de Descripción  DEDI|2
					trama = trama + "|"; //  Nombre del Concepto   DEDI|3 
					trama = trama + "|"; //   Codigo del Concepto   DEDI|4 
					trama = trama + "|"; //  Número de placa del vehículo   DEDI|5 
					trama = trama + codProductoSUNAT + "|"; //  Codigo producto de SUNAT  DEDI|6  //PROY-140336
					trama = trama + "|"; //  Código de producto GS1  DEDI|7 
					trama = trama ;		 //  Tipo de estructura GTIN  DEDI|8
					// FIN - PROY 140174 - TAG DEDI  UBL 2.1
					trama = trama + strsaltodelinea; //PROY-140621  
				}
				else
				{
					trama = trama + "DEDI|";
					trama = trama + dtDetalle.Rows[i][4].ToString() + "|";  // DESCRIPCION DEDI|1
					// INICIO - PROY 140174 - TAG DEDI  UBL 2.1
					trama = trama + "|"; // Notas Complementarias de Descripción  DEDI|2
					trama = trama + "|"; //  Nombre del Concepto   DEDI|3 
					trama = trama + "|"; //   Codigo del Concepto   DEDI|4 
					trama = trama + "|"; //  Número de placa del vehículo   DEDI|5 
					trama = trama + codProductoSUNAT + "|"; //  Codigo producto de SUNAT  DEDI|6  //PROY-140336
					trama = trama + "|"; //  Código de producto GS1  DEDI|7 
					trama = trama ;		 //  Tipo de estructura GTIN  DEDI|8
					// FIN - PROY 140174 - TAG DEDI  UBL 2.1
					trama = trama + strsaltodelinea; //PROY-140621  
				}			

				// DEDR: DESCUENTOS Y RECARGOS DEL ÍTEM
	                        // INICIO - PROY 140174 - TAG UBL 2.1
				// Cambio para corregir error en cálculo de descuentos y no modificar el SP SSAPSI_FACTELECTRONICA del PKG_PAGO
				monto_total = Convert.ToDecimal(dtDetalle.Rows[i][11].ToString()); // V_VT_U_ITEM DE|8
				monto_final = Convert.ToDecimal(dtDetalle.Rows[i][12].ToString()); // V_VT_ITEM DE|9

				// Cambio para conformidad en TG - no enviar campo de descuento en caso de TG
				T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));
				if(T_VN_GRATUITAS == 0 && Defan_t_vn_gratuitas == 0) //PROY-140589  
				{
					descuento = Math.Abs(Math.Round((monto_total - monto_final),2));
					//descuento = Math.Abs(decimal.Round(decimal.Parse(dtDetalle.Rows[i][17].ToString()),2));
					
					//Boolean isNotaCredito = dtCabecera.Rows[0][7].ToString() == "1" || dtCabecera.Rows[0][7].ToString() == "6"; //INC000004228061
					Boolean isNotaCredito = dtCabecera.Rows[0][5].ToString() == "E7"; //INC000004347404
					
				if(descuento>0 && !isNotaCredito) //INC000004228061
				{
					trama = trama + "DEDR|";
						// INICIO - PROY 140174 - TAG DEDR  UBL 2.1
						trama = trama + (descuento>0?"false":"true") + "|";  // recargo: true, descuento: false DEDR|1  
						//INI - PROY-140550 
						decimal pos2_DEDR=0;
						pos2_DEDR=decimal.Round(DU*Q,2);
						trama = trama + pos2_DEDR.ToString() + "|";  
						//FIN - PROY-140550
						trama = trama + codigoCargoDescuento + "|"; // Código de cargo/descuento  DEDR|3  
						//INI :: INC000001591136
						//decimal DEDR5 = decimal.Round((PU/(1 + (igv/100)))*Q , 2) ; //PROY-140550
                        //INICIO INC000004347404
                        decimal DEDR5 = decimal.Round(Convert.ToDecimal(dtDetalle.Rows[i][11].ToString()), 2);
                        objLog.Log_WriteLog(pathFile, strArchivo, "INC000004347404|DEDR5: " + DEDR5.ToString());
                        //FIN INC000004347404
						decimal cargo_descuento  = 0;
						objLog.Log_WriteLog(pathFile, strArchivo," ****** INICIO INC000001591136 CAMBIO DE POSICION => Convert.ToDecimal(dtDetalle.Rows[i][11]*******");
						objLog.Log_WriteLog(pathFile, strArchivo,"	Convert.ToDecimal(dtDetalle.Rows[i][11]):" + DEDR5.ToString());

						if( DEDR5!= 0)
						{
                            cargo_descuento = decimal.Round(pos2_DEDR / DEDR5, 5);  //PROY-140550 //INC000004228061
							objLog.Log_WriteLog(pathFile, strArchivo,"	cargo_descuento:" + cargo_descuento.ToString());
						} 

						trama = trama  + Convert.ToDecimal(cargo_descuento).ToString() + "|"; // Factor de cargo/descuento  DEDR|4
						//INI - PROY-140550
						trama = trama  + DEDR5.ToString();// Posición 5 = (PU/1.18)*Q 
					//FIN - PROY-140550
						objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo," ****** FIN INC000001591136 CAMBIO DE POSICION => Convert.ToDecimal(dtDetalle.Rows[i][11]*******");
					//FIN :: INC000001591136
						// FIN - PROY 140174 - TAG DEDR  UBL 2.1
					trama = trama + strsaltodelinea; //PROY-140621
				} 
				}
				/*
				 descuento = decimal.Parse(dtDetalle.Rows[i][17].ToString());
				if(Math.Abs(descuento)>0)
				{
					trama = trama + "DEDR|";
					trama = trama + (descuento>0?"true":"false") + "|";  // recargo: true, descuento: false
					trama = trama + dtDetalle.Rows[i][17].ToString() + "|";  // T_DESCUENTOS
				}
				 */
                                 // FIN - PROY 140174 - TAG UBL 2.1
				// DEIM: IMPUESTOS DEL ÍTEM

				// INICIO - PROY 140174 - TAG DEIM  UBL 2.1
				// DEIM: IMPUESTOS DEL ÍTEM
				trama = trama + "DEIM|";
				//INI- PROY-140550
                //decimal pos1_DEIM =0;
                //pos1_DEIM =decimal.Round(((PU /(1 + (igv/100)) - DU) * Q) * (igv/100),2);
				//trama = trama + pos1_DEIM.ToString() + "|";  // S_IGV_ITEM DEIM|1 -

                //INICIO INC000004347404
                decimal newPos1_DEIM = decimal.Round(Convert.ToDecimal(pos9_DE) * (igv / 100), 2);
                objLog.Log_WriteLog(pathFile, strArchivo, "INC000004347404|newPos1_DEIM: " + newPos1_DEIM.ToString());
                //FIN INC000004347404
				
				//FIN - PROY-140550
				if (T_VN_GRATUITAS > 0)
				{
					//trama = trama + pos1_DEIM.ToString() + "|";  // S_IGV_ITEM DEIM|1 -
                    trama = trama + newPos1_DEIM.ToString() + "|";  // S_IGV_ITEM DEIM|1 - //INC000004347404
					// Inicio Cambio para conformidad en TG -- Base Imponible DEIM|2
					suma_deim2 = suma_deim2 + de5;
					trama = trama + de5.ToString() +"|"; // Base Imponible DEIM|2
					// Fin Cambio para conformidad en TG -- Base Imponible DEIM|2

					// Inicio Cambio para conformidad en TG -- S_IGV_ITEM DEIM|3
					//deim3 = Math.Round((de5 * (igv/100)), 2); //PROY-140550
                    deim3 = Math.Round(newPos1_DEIM, 2); //INC000004347404
					suma_deim3 = suma_deim3 + deim3; 
					trama = trama + deim3.ToString() + "|";  // S_IGV_ITEM DEIM|3
					// Fin Cambio para conformidad en TG -- S_IGV_ITEM DEIM|3
					trama = trama + dtCabecera.Rows[0][23].ToString() + "|"; // TASAIGV  DEIM|4
				}
				else if(Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY
				{
					// Inicio Cambio para conformidad en TG -- Base Imponible DEIM|2
					trama = trama + "0.00" + "|";
					suma_deim2 = suma_deim2 + de5;
					trama = trama + de5.ToString() +"|"; // Base Imponible DEIM|2
					// Fin Cambio para conformidad en TG -- Base Imponible DEIM|2
					trama = trama + "0.00" + "|";  // S_IGV_ITEM DEIM|3
					trama = trama + "0.00" + "|"; // S_IGV_ITEM DEIM|3
					// Fin Cambio para conformidad en TG -- S_IGV_ITEM DEIM|3
				}
				else //JRM - PROY COBRO DELIVERY
				{
					//trama = trama + pos1_DEIM.ToString() + "|";  // S_IGV_ITEM DEIM|1 -                    
					//trama = trama + Math.Round(((decimal.Parse(dtDetalle.Rows[i][2].ToString()) * decimal.Parse(dtDetalle.Rows[i][11].ToString())) - descuento),2).ToString() +"|"; // Base Imponible DEIM|2
                    //trama = trama + dtDetalle.Rows[i][9].ToString() + "|";  // S_IGV_ITEM DEIM|3

                    //INICIO INC000004347404
                    //trama = trama + newPos1_DEIM.ToString() + "|"; //S_IGV_ITEM DEIM|1
                    //trama = trama + pos9_DE + "|"; //DEIM|2
                    //trama = trama + newPos1_DEIM + "|"; //S_IGV_ITEM DEIM|3
                    //FIN INC000004347404
					
					//INICIO PBI000002128547
					trama = trama + Math.Round(decimal.Parse(dtDetalle.Rows[i][9].ToString()),2).ToString() + "|"; 
					objLog.Log_WriteLog(pathFile, strArchivo, " PBI000002128547 | Posición 8 de sección DE: " + Math.Round(decimal.Parse(dtDetalle.Rows[i][9].ToString()),2).ToString());
					trama = trama + Math.Round(decimal.Parse(dtDetalle.Rows[i][12].ToString()),2).ToString() + "|"; 
					objLog.Log_WriteLog(pathFile, strArchivo, " PBI000002128547 | Posición 9 de sección DE: " + Math.Round(decimal.Parse(dtDetalle.Rows[i][12].ToString()),2).ToString());
					trama = trama + Math.Round(decimal.Parse(dtDetalle.Rows[i][9].ToString()),2).ToString() + "|"; 
					objLog.Log_WriteLog(pathFile, strArchivo, " PBI000002128547 | Posición 10 de sección DE: " + Math.Round(decimal.Parse(dtDetalle.Rows[i][9].ToString()),2).ToString());
					//FIN PBI000002128547
					
					trama = trama + dtCabecera.Rows[0][23].ToString() + "|"; // TASAIGV  DEIM|4
				}
				
				//trama = trama + dtCabecera.Rows[0][23].ToString() + "|"; // TASAIGV  DEIM|4

				T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));

				if (T_VN_GRATUITAS > 0)
				{
					trama = trama + "|"; // Tipo de Impuesto DEIM|5  
					trama = trama + CodAfectaTG.ToString() + "|";  // AFEC_IGV_I DEIM|6 
					trama = trama + "|"; // Sistema de ISC DEIM|7 
					trama = trama + CodTributoTG.ToString() + "|"; // Identificación del tributo DEIM|8  
					trama = trama + NomTributoTG.ToString() + "|"; // Nombre del Tributo DEIM|9
					trama = trama + CodTipoTributoTG.ToString() ; // Código del Tipo de Tributo DEIM|10
				}
	
				else if (Defan_t_vn_gratuitas > 0) //JRM - PROY COBRO DELIVERY
				{
					trama = trama + "|"; // Tipo de Impuesto DEIM|5  
					trama = trama + codBoni.ToString() + "|";  // AFEC_IGV_I DEIM|6 
					trama = trama + "|"; // Sistema de ISC DEIM|7 
					trama = trama + CodTributoTG.ToString() + "|"; // Identificación del tributo DEIM|8  
					trama = trama + NomTributoTG.ToString() + "|"; // Nombre del Tributo DEIM|9
					trama = trama + CodTipoTributoTG.ToString() ; // Código del Tipo de Tributo DEIM|10
				}
	
				//INI :: INC000001591136
				else if (dtCabecera.Rows[0][14].ToString() == "0.00" && dtCabecera.Rows[0][13].ToString() == "0.00")
				{
					trama = trama + TipoImpuestoINA +"|"; // Tipo de Impuesto DEIM|5  
					trama = trama + CodAfectaINA.ToString() + "|";  // AFEC_IGV_I DEIM|6 
					trama = trama + "|"; // Sistema de ISC DEIM|7 
					trama = trama + CodTributoINA.ToString() + "|"; // Identificación del tributo DEIM|8  
					trama = trama + NomTributoINA.ToString() + "|"; // Nombre del Tributo DEIM|9
					trama = trama + CodTipoTributoINA.ToString() ; // Código del Tipo de Tributo DEIM|10

					objLog.Log_WriteLog(pathFile, strArchivo," ********- INICIO INC000001591136 => SI S_IGV=0 Y T_VN_GRATUITAS=0 ENVIAR A LA TRAMA EL VALOR DE LAS OPERACIONES INAFECTAS-*********");
					objLog.Log_WriteLog(pathFile, strArchivo,"	TipoImpuestoINA:" + TipoImpuestoINA.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	CodAfectaINA:" + CodAfectaINA.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	CodTributoINA:" + CodTributoINA.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	NomTributoINA:" + NomTributoINA.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	CodTipoTributoINA:" + CodTipoTributoINA.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
					objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN INC000001591136-*******************");
				}
				//FIN :: INC000001591136

				else
				{
					trama = trama +  TipoImpuestoIGV + "|"; // Tipo de Impuesto DEIM|5  
					trama = trama + dtDetalle.Rows[i][7].ToString() + "|";  // AFEC_IGV_I DEIM|6
					trama = trama + "|"; // Sistema de ISC DEIM|7 
					trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_IdTributo"] + "|"; // Identificación del tributo DEIM|8 
					trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_NombreTributo"] + "|"; // Nombre del Tributo DEIM|9
					trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_TipoTributo"] ; // Código del Tipo de Tributo DEIM|10    
					
				}
				trama = trama + strsaltodelinea; //PROY-140621 
				// FIN - PROY 140174 - TAG DEIM  UBL 2.1
			}

			// DI: IMPUESTOS GLOBALES
			trama = trama + "DI|";
                        // INICIO - PROY 140174 - TAG DI  UBL 2.1
			trama = trama + dtCabecera.Rows[0][14].ToString() + "|"; // S_IGV DI|1

			if (T_VN_GRATUITAS > 0)
				trama = trama + suma_deim3.ToString() + "|"; // S_IGV DI|2 -- Cambio para conformidad en TG
			else
				trama = trama + dtCabecera.Rows[0][14].ToString() + "|"; // S_IGV DI|2

			
			T_VN_GRATUITAS = Math.Abs(decimal.Round(decimal.Parse(dtCabecera.Rows[0][13].ToString()),2));

			if (T_VN_GRATUITAS > 0)
			{
				trama = trama + CodTributoTG.ToString() + "|"; // Identificación del tributo DI|3 
				trama = trama + NomTributoTG.ToString() + "|"; // Nombre del Tributo DI|4 
				trama = trama + CodTipoTributoTG.ToString() + "|"; // Código del Tipo de Tributo DI|5
			}
			//INI :: INC000001591136
			else if (dtCabecera.Rows[0][14].ToString() == "0.00" && dtCabecera.Rows[0][13].ToString() == "0.00")
			{
				trama = trama + CodTributoINA.ToString() + "|"; // Identificación del tributo DI|3  
				trama = trama + NomTributoINA.ToString() + "|"; // Nombre del Tributo DI|4
				trama = trama + CodTipoTributoINA.ToString() + "|"; // Código del Tipo de Tributo DI|5

				objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO INC000001591136 SI S_IGV=0 Y T_VN_GRATUITAS=0 ENVIAR A LA TRAMA EL VALOR DE LAS OPERACIONES INAFECTAS-*******************");
				objLog.Log_WriteLog(pathFile, strArchivo,"	CodTributoINA:" + CodTributoINA.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo,"	NomTributoINA:" + NomTributoINA.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo,"	CodTipoTributoINA:" + CodTipoTributoINA.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo,"	trama:" + trama.ToString());
				objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN INC000001591136-*******************");
			}
			//FIN :: INC000001591136
			else
			{
				trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_IdTributo"] + "|"; // Identificación del tributo DI|3  
				trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_NombreTributo"] + "|"; // Nombre del Tributo DI|4 
				trama = trama + ConfigurationSettings.AppSettings["FE_DEIM_TipoTributo"] + "|"; // Código del Tipo de Tributo DI|5 
			}
			
			if (T_VN_GRATUITAS > 0)
				trama = trama + suma_deim2.ToString() + strsaltodelinea; //PROY-140621 //  Monto Base DI|6
			else
				trama = trama + neto + strsaltodelinea; //PROY-140621 //  Monto Base DI|6
			
			//PROY-140589 INI
			if (flagDLV_Gra == "1")
			{
				trama = trama + "DI|";
				trama = trama + "0.00" + "|";
				trama = trama + "0.00" + "|"; // °2
				trama = trama + CodTributoTG.ToString() + "|"; // °3  
				trama = trama + NomTributoTG.ToString() + "|"; // °4
				trama = trama + CodTipoTributoTG.ToString() + "|"; // °5
				trama = trama + suma_deim2.ToString() + strsaltodelinea; //PROY-140621 // °6
			}
			// PROY-140589 fin 

			if (valor=="07" || valor=="08")
				// RE: REFERENCIAS
			{
				trama = trama + "RE|";

				//dtCabecera.Rows[0][26].ToString();		

				cadena2 = cortarPrimerElemento(cadena2, 4);//INC000004228061
				trama = trama + cadena2 + "|"; // docReferencia RE|1
				trama = trama + dtCabecera.Rows[0][47].ToString() +"|"; // Fecha de Emisión RE|2
				trama = trama + dtCabecera.Rows[0][28].ToString() + "|"; // TIPODOCREFERENCIA RE|3

				if (dtCabecera.Rows[0][26].ToString() !="")
				{
					trama = trama + dtCabecera.Rows[0][26].ToString().Split('|')[0]; // MOTIVONCND RE|4
						
				}
				else 
				{
					trama = trama + dtCabecera.Rows[0][26].ToString() + "|"; // MOTIVONCND		RE|4			
				}					

            // FIN - PROY 140174 - TAG DI  UBL 2.1

				cadena=dtCabecera.Rows[0][21].ToString();
			
				if (cadena!="")
				{			
					serie=cadena.Split('-')[0];
					correlativo=cadena.Split('-')[1];
					cadena2=cadena.Split('-')[2];
					cadena=serie+"-" + correlativo+"-"+"0"+cadena2;
				}
				else
				{
					cadena=dtCabecera.Rows[0][21].ToString();
				}
                                // INICIO - PROY 140174 - TAG RE  UBL 2.1
				trama = trama + cadena + "|"; // GUIAREMISION RE|5
			
				trama = trama + dtCabecera.Rows[0][28].ToString() + "|"; // TIPODOCREFERENCIA RE|6
				trama = trama + "|"; // En el caso de otros tipos de Documentos Número de documento relacionado RE|7

				trama = trama + "|"; // En el caso de otros Tipo de Documento (no factura no guia de remisión) RE|8
				trama = trama + "|"; //  RE|9
				trama = trama ;		//  RE|10 
				trama = trama + strsaltodelinea; //PROY-140621
				// FIN - PROY 140174 - TAG RE  UBL 2.1

			}
				



			tipoDocumentoSunat = dtCabecera.Rows[0][5].ToString();
			eMailCliente = dtCabecera.Rows[0][33].ToString();

			string orden_compra=dtCabecera.Rows[0][29].ToString();

			

			//INICIATIVA-770 Nuevos cambios en Fact Electronica.
			objLog.Log_WriteLog(pathFile, strArchivo," ********- INICIO INICIATIVA-770 => Nuevos Campos en Fact Electronica *********");
			objLog.Log_WriteLog(pathFile, strArchivo," ********- sFormaPagoDocsPermitidos=>"+ sFormaPagoDocsPermitidos);
			objLog.Log_WriteLog(pathFile, strArchivo," ********- tipoDocumentoSunat=>"+ tipoDocumentoSunat);
			if( tipoDocumentoSunat.IndexOf(sFormaPagoDocsPermitidos) > -1)
			{
				//Si es Factura (E1)o Boleta (E3)
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INP NroDocumento=>"+ NroDocumento);
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INP sFormaPago=>"+ sFormaPago);
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INP sCredito_Valor=>"+ sCredito_Valor);
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INP sContado_Valor=>"+ sContado_Valor);
				objLog.Log_WriteLog(pathFile, strArchivo," ********- INP sCuota_Valor=>"+ sCuota_Valor);
				string sTrama = ObtenerTramaFormaPago(NroDocumento,sFormaPago,sCredito_Valor,sContado_Valor,sCuota_Valor);
				objLog.Log_WriteLog(pathFile, strArchivo," ********- OUT sTrama=>"+ sTrama);
				trama = trama + sTrama;
			}
			//INICIATIVA-770 Nuevos cambios en Fact Electronica.
			objLog.Log_WriteLog(pathFile, strArchivo," ********- FIN INICIATIVA-770 => Nuevos Campos en Fact Electronica *********");

			
			// PE: PERSONALIZADOS
			
			if (ConfigurationSettings.AppSettings["FE_PE_PLANTILLA"]!="" )
			{
				trama = trama + "PE|" + ConfigurationSettings.AppSettings["FE_Texto_Plantilla"] + "|" + ConfigurationSettings.AppSettings["FE_PE_PLANTILLA"]+ strsaltodelinea; //PROY-140621
			}

			if (eMailCliente!= "" )
			{
				trama = trama + "PE|" + ConfigurationSettings.AppSettings["FE_Texto_CorreoCliente"] + "|" + eMailCliente + strsaltodelinea; //PROY-140621
			}

			if (orden_compra!="" )
			{
				trama = trama + "PE|" + ConfigurationSettings.AppSettings["FE_Texto_OrdenCompra"] + "|" + orden_compra + strsaltodelinea; //PROY-140621
			}	
			
			

			return trama;
		}

        //INICIO INC000004228061
        private static string cortarPrimerElemento(string cadena, int longitud)
        {
            string respuesta = cadena;

            int cantidadDigitos = longitud;
            char separador = '-';
            string cadenaTotal = cadena;
            string[] elementos = cadenaTotal.Split(separador);
            string primerElemento = elementos[0];
            int tamanioElemento = primerElemento.Length;
            int inicioCorte = tamanioElemento - cantidadDigitos;
            int finCadenaTotal = cadenaTotal.Length - inicioCorte;
            if (tamanioElemento > cantidadDigitos)
            {
                respuesta = cadenaTotal.Substring(inicioCorte, finCadenaTotal);
            }
            return respuesta;
        }
        //FIN INC000004228061

		//INI PROY-140336
		public string EnviarEmail(string codMaterial, string msjCodProducto)
		{
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log(); 
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile); 
			string strRemitente = "";
			string strDestinatario = "";
			string strAsunto = "";
			string strMensaje = "";
			string codGrupoCorreo = ConfigurationSettings.AppSettings["ParamGrupoCorreo"];				
			string strIP = ConfigurationSettings.AppSettings["CodAplicacion"];
			string strUsuario = "";
			string strCodRespuesta = string.Empty;
			string strMgsRespuesta = string.Empty;
			COM_SIC_Cajas.clsCajas objParametro = new COM_SIC_Cajas.clsCajas();
			COM_SIC_Activaciones.BWEnvioCorreo objEnvioCorreo = new COM_SIC_Activaciones.BWEnvioCorreo();

			DataSet dsParametroCorreo = objParametro.ObtenerParamByGrupo(Convert.ToInt64(codGrupoCorreo));
			foreach(DataRow _item in dsParametroCorreo.Tables[0].Rows)
			{					
				if(_item["PARAV_VALOR1"].ToString() == "Key_Destinatario_SICAR")
				{
					strDestinatario = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "Key_Remitente_SICAR")
				{
					strRemitente = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "Key_Asunto_SICAR")
				{
					strAsunto = _item["PARAV_VALOR"].ToString();
				}
				if(_item["PARAV_VALOR1"].ToString() == "Key_Mensaje")
				{
					strMensaje = _item["PARAV_VALOR"].ToString();
				}
			}
			
			strMensaje = string.Format(strMensaje,codMaterial);			 
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- INICIO ENVIO CORREO -*******************");	

			objEnvioCorreo.EnviarCorreoCP(strRemitente, strDestinatario,strAsunto, strMensaje, "1", strIP, strUsuario,ref strCodRespuesta,ref strMgsRespuesta);
			
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- FIN ENVIO CORREO -*******************");
			return strCodRespuesta;
		}
		//FIN PROY-140336
		//Borra los "|" Palotes
		private string DeletePipe(string cadena)
		{
			string strAux="";
			if(cadena != null)
			{
				if(cadena.Length>0)
				{
					strAux=cadena.Replace("|","");					
				}			
			}
			return strAux;
		}

		public string ObtenerTramaFormaPago(string pNumeroPedido,string pEtiquetaFormaPago, string pEtiquetaCredito, string pEtiquetaContado ,string pEtiquetaCuota)
		{
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log(); 
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile); 
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- ObtenerTramaFormaPago INICIO");


			COM_SIC_Activaciones.clsTrsPvu objPvu = new COM_SIC_Activaciones.clsTrsPvu();
			DataTable objData = objPvu.ConsultarDetalleCuotas(pNumeroPedido);

			string sTrama = "";
			objLog.Log_WriteLog(pathFile, strArchivo," objData.Rows.Count" + objData.Rows.Count.ToString());
			if (objData.Rows.Count > 0)
			{
				double nImporteTotal =  Convert.ToDouble(objData.Compute("SUM(CVEN_IMPORTE)",""));
				string nImporteTotal_str = String.Format("PBI000002130505 - Number {0, 0:N2}", nImporteTotal);// PBI000002130505
				if(nImporteTotal > 0) 
				{
				Int16 nCuotaMax = Convert.ToInt16(objData.Compute("MAX(CVEN_COUTA_INC)",""));

					sTrama = sTrama + "FP|"+ pEtiquetaFormaPago+ "|" + pEtiquetaCredito+ "|" + nImporteTotal_str + "||" + strsaltodelinea; //PROY-140621 //PBI000002130505

				int nNumCuota = 1;
				foreach ( DataRow drFila in objData.Rows)
				{
					double importe = Funciones.CheckDbl(drFila["CVEN_IMPORTE"]); // PBI000002128547
					
					sTrama = sTrama + "FP" + "|" +
						pEtiquetaFormaPago + "|" +
						pEtiquetaCuota + string.Format("{0:000}", nNumCuota) + "|" +
							//String.Format("PBI000002130505 - Number {0, 0:N2}", drFila["CVEN_IMPORTE"]) + "|" + //PBI000002130505
						String.Format("{0, 0:N2}", importe) + "|" +  // PBI000002128547
						Convert.ToDateTime(drFila["CVED_FECHA_VENCIMIENTO"]).ToString("yyyy-MM-dd") + strsaltodelinea; //PROY-140621
					nNumCuota ++;
				}

				}// PBI000002130505
				else // INICIO - PBI000002130505
				{
					sTrama = sTrama + "FP|" + pEtiquetaFormaPago + "|" + pEtiquetaContado + "|" + "||" + strsaltodelinea; //PROY-140621
					objLog.Log_WriteLog(pathFile, strArchivo," PBI000002130505 | nImporteTotal menor o igual a 0");
				}// FIN - PBI000002130505
			}
			else
			{
				sTrama = sTrama + "FP|" + pEtiquetaFormaPago + "|" + pEtiquetaContado + "|" + "||" + strsaltodelinea; //PROY-140621
			}
			objLog.Log_WriteLog(pathFile, strArchivo," *******************- ObtenerTramaFormaPago FIN");
			return sTrama;

		}

		//PROY-140621 Inicio
		public void GenerarFacturaElectronica(string pRuc,string pLogin,string pClave, string pTrama,string pTipoFolacion,string pTipoRetorno, out string pCodigo, out string pMensaje)
		{
			
			pCodigo="";pMensaje="";
			
			//ConsFlagFacturadorNuevo

			if (ConfigurationSettings.AppSettings["ConsFlagFacturadorNuevo"] == "1")
			{

				OnlineGenerationRequest oRequest = new OnlineGenerationRequest();
				oRequest.ruc = pRuc;
				oRequest.login = pLogin;
				oRequest.clave = pClave;
				oRequest.docTxt = pTrama;
				oRequest.tipoFoliacion = pTipoFolacion;
				oRequest.tipoRetorno = pTipoRetorno;
				FacturaElectronicaControlador oControlador = new FacturaElectronicaControlador();
				oControlador.GenerarFactura(oRequest,out pCodigo,out pMensaje);

			}
			else
			{
				string sRespuesta = "";
				PaperLessWS.Online objFE = new PaperLessWS.Online();
				objFE.Url = ConfigurationSettings.AppSettings["FE_Url_PaperLess"];
				sRespuesta = objFE.OnlineGeneration(pRuc,pLogin,pClave,pTrama,int.Parse(pTipoFolacion),true,int.Parse(pTipoRetorno),true);
				XmlDocument myXml = new XmlDocument();
				myXml.LoadXml(sRespuesta);

				pCodigo =myXml.SelectSingleNode("Respuesta/Codigo").InnerText;								 
				pMensaje =myXml.SelectSingleNode("Respuesta/Mensaje").InnerText;
			}


			
		}
		//PROY-140621 Fin

	}	
}