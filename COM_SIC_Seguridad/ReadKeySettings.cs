using System;

namespace COM_SIC_Seguridad
{
	#region PROY-31836 | Mejora de Procesos Postventa del servicio Proteccion Movil | Bryan Chumbes Lizarraga 
	public class ReadKeySettings
	{
		private static string _Key_CantIntentosPagoRenovacion;
		private static string _Key_SubClaseRenovacionTipificaWS;
		private static string _Key_FlagPlantillaRenoTipificaWS;
		private static string _Key_UsuarioTipificacionWS;
		private static string _Key_MotivoCancelTipificacionWS;
		private static string _Key_NotaExitoTipificacionWS;
		private static string _Key_NotaErrorTipificacionWS;
		private static string _Key_TipoOperRenoTipificacion;
		private static string _Key_TipoOperRenovacionPostago;

		public static string Key_CantIntentosPagoRenovacion { get{return _Key_CantIntentosPagoRenovacion;} set{ _Key_CantIntentosPagoRenovacion = value;} } 
		public static string Key_SubClaseRenovacionTipificaWS { get{return _Key_SubClaseRenovacionTipificaWS;} set{ _Key_SubClaseRenovacionTipificaWS = value;} } 
		public static string Key_FlagPlantillaRenoTipificaWS { get{return _Key_FlagPlantillaRenoTipificaWS;} set{ _Key_FlagPlantillaRenoTipificaWS = value;} }
		public static string Key_UsuarioTipificacionWS { get{return _Key_UsuarioTipificacionWS;} set{ _Key_UsuarioTipificacionWS = value;} }
		public static string Key_MotivoCancelTipificacionWS { get{return _Key_MotivoCancelTipificacionWS;} set{ _Key_MotivoCancelTipificacionWS = value;} }
		public static string Key_NotaExitoTipificacionWS { get{return _Key_NotaExitoTipificacionWS;} set{ _Key_NotaExitoTipificacionWS = value;} }
		public static string Key_NotaErrorTipificacionWS { get{return _Key_NotaErrorTipificacionWS;} set{ _Key_NotaErrorTipificacionWS = value;} }
		public static string Key_TipoOperRenoTipificacion { get{return _Key_TipoOperRenoTipificacion;} set{ _Key_TipoOperRenoTipificacion = value;} }
		public static string Key_TipoOperRenovacionPostago { get{return _Key_TipoOperRenovacionPostago;} set{ _Key_TipoOperRenovacionPostago = value;} }
		
		//PROY-140379 INI
		private static string _Key_requestBodyIOT;
		private static string _Key_MaterialPermitidoSW;
		private static string _Key_serviceNameSW;
		private static string _Key_requestConsultaIot_ListaOpc;
		private static string _Key_consultarProcesarIotActualizar;
		private static string _Key_consultarProcesarIotDarBaja;
		private static string _Key_TipoOper_Devol;
		private static string _Key_HeaderResquest;
		
		public static string Key_requestBodyIOT { get{return _Key_requestBodyIOT;} set{ _Key_requestBodyIOT= value;} }
		public static string Key_MaterialPermitidoSW { get{return _Key_MaterialPermitidoSW;} set{ _Key_MaterialPermitidoSW = value;} }
		public static string Key_serviceNameSW { get{return _Key_serviceNameSW;} set{ _Key_serviceNameSW = value;} }
		public static string Key_requestConsultaIot_ListaOpc { get{return _Key_requestConsultaIot_ListaOpc;} set{ _Key_requestConsultaIot_ListaOpc = value;} }
		public static string Key_consultarProcesarIotActualizar { get{return _Key_consultarProcesarIotActualizar;} set{ _Key_consultarProcesarIotActualizar = value;} }
		public static string Key_consultarProcesarIotDarBaja { get{return _Key_consultarProcesarIotDarBaja;} set{ _Key_consultarProcesarIotDarBaja = value;} }
		public static string Key_TipoOper_Devol { get{return _Key_TipoOper_Devol;} set{ _Key_TipoOper_Devol = value;} }
		public static string Key_HeaderResquest { get{return _Key_HeaderResquest;} set{ _Key_HeaderResquest = value;} }
		//PROY-140379 FIN

		//INI: INICIATIVA-219 | CBIO
		private static string _key_flagCBIO;
		public static string key_flagCBIO { get{return _key_flagCBIO;} set{ _key_flagCBIO = value;} }	
	
		private static string _key_DescricpionCBIO;
		public static string key_DescricpionCBIO { get{return _key_DescricpionCBIO;} set{ _key_DescricpionCBIO = value;} }

		private static string _key_TipoOperacionAltaCBIO;
		public static string key_TipoOperacionAltaCBIO { get{return _key_TipoOperacionAltaCBIO;} set{ _key_TipoOperacionAltaCBIO = value;} }

		private static string _key_TipoOperacionMigracionAltaCBIO;
		public static string key_TipoOperacionMigracionAltaCBIO { get{return _key_TipoOperacionMigracionAltaCBIO;} set{ _key_TipoOperacionMigracionAltaCBIO = value;} }

		private static string _key_TipoOperacionReposicionCBIO;
		public static string key_TipoOperacionReposicionCBIO { get{return _key_TipoOperacionReposicionCBIO;} set{ _key_TipoOperacionReposicionCBIO = value;} }
		
		private static string _key_TipoOperacionRenovacionMasReposicionCBIO;
		public static string key_TipoOperacionRenovacionMasReposicionCBIO { get{return _key_TipoOperacionRenovacionMasReposicionCBIO;} set{ _key_TipoOperacionRenovacionMasReposicionCBIO = value;} }
		
		private static string _key_PoIDProteccionMovil;
		public static string key_PoIDProteccionMovil { get{return _key_PoIDProteccionMovil;} set{ _key_PoIDProteccionMovil = value;} }
		
		private static string _key_FlagMejoraRenovacion;
		public static string key_FlagMejoraRenovacion { get{return _key_FlagMejoraRenovacion;} set{ _key_FlagMejoraRenovacion = value;} }
		//INI: INICIATIVA-219 | CBIO

		//JLOPETAS - PROY 140589 - INI
		private static string _Key_Msj_Costo_DLV;
		public static string Key_Msj_Costo_DLV { get{return _Key_Msj_Costo_DLV;} set{ _Key_Msj_Costo_DLV = value;} }

		private static string _Key_Msj_AnuCostdlv;
		public static string Key_Msj_AnuCostdlv { get{return _Key_Msj_AnuCostdlv;} set{ _Key_Msj_AnuCostdlv = value;} }

		private static string _Key_Msj_Generico_Anula_DLV;
		public static string Key_Msj_Generico_Anula_DLV { get{return _Key_Msj_Generico_Anula_DLV;} set{ _Key_Msj_Generico_Anula_DLV = value;} }

		private static string _Key_MensajePorta_envio;
		public static string Key_MensajePorta_envio { get{return _Key_MensajePorta_envio;} set{ _Key_MensajePorta_envio = value;} }

		private static string _Key_MensajePorta_aprobacion;
		public static string Key_MensajePorta_aprobacion { get{return _Key_MensajePorta_aprobacion;} set{ _Key_MensajePorta_aprobacion = value;} }


		//JLOPETAS - PROY 140589 - FIN

		//PROY-140662 -  DELIVERY F4 - INI
		private static string _Key_ModoCargaPropiedadesUpdate;
		public static string Key_ModoCargaPropiedadesUpdate { get{return _Key_ModoCargaPropiedadesUpdate;} set{ _Key_ModoCargaPropiedadesUpdate = value;} }

		private static string _Key_TipoCarga;
		public static string Key_TipoCarga { get{return _Key_TipoCarga;} set{ _Key_TipoCarga = value;} }

		private static string _Key_ConfiguracionSOT;
		public static string Key_ConfiguracionSOT { get{return _Key_ConfiguracionSOT;} set{ _Key_ConfiguracionSOT = value;} }

		private static string _Key_ConfiguracionInventario;
		public static string Key_ConfiguracionInventario { get{return _Key_ConfiguracionInventario;} set{ _Key_ConfiguracionInventario = value;} }

		private static string _Key_TipoComando;
		public static string Key_TipoComando { get{return _Key_TipoComando;} set{ _Key_TipoComando = value;} }

		private static string _Key_EncriptBase64DP;
		public static string Key_EncriptBase64DP { get{return _Key_EncriptBase64DP;} set{ _Key_EncriptBase64DP = value;} }

		private static string _Key_MsjOrdenTOA;
		public static string Key_MsjOrdenTOA { get{return _Key_MsjOrdenTOA;} set{ _Key_MsjOrdenTOA = value;} }

		private static string _Key_MsjOrdenTOACaido;
		public static string Key_MsjOrdenTOACaido { get{return _Key_MsjOrdenTOACaido;} set{ _Key_MsjOrdenTOACaido = value;} }

		private static string _Key_EstadoAuditTOA;
		public static string Key_EstadoAuditTOA { get{return _Key_EstadoAuditTOA;} set{ _Key_EstadoAuditTOA = value;} }

		private static string _Key_EstadoOrdenTOA;
		public static string Key_EstadoOrdenTOA { get{return _Key_EstadoOrdenTOA;} set{ _Key_EstadoOrdenTOA = value;} }
		
		private static string _Key_flagTOA;
		public static string Key_flagTOA { get{return _Key_flagTOA;} set{ _Key_flagTOA = value;} }
		

		// PARAMETROS DE DLV F2 - INI
		private static string _Key_PDVVentaExpress;
		public static string Key_PDVVentaExpress { get{return _Key_PDVVentaExpress;} set{ _Key_PDVVentaExpress = value;} }

		private static string _Key_TipoOrdenRegular;
		public static string Key_TipoOrdenRegular { get{return _Key_TipoOrdenRegular;} set{ _Key_TipoOrdenRegular = value;} }

		private static string _Key_TipoOrdenExpress;
		public static string Key_TipoOrdenExpress { get{return _Key_TipoOrdenExpress;} set{ _Key_TipoOrdenExpress = value;} }

		//Key_EncriptBase64DP
		// PARAMETROS DE DLV F2 - INI

		//PROY-140662 -  DELIVERY F4 - FIN


	//INICIATIVA 712 Cobro Anticipado INI
		private static string _ConsFlagAplicaCAI;
		private static string _ConsTiempoSecPendientePagoLink;// En Horas
		private static string _ConsConsumerConsultaPA;
		private static string _ConsCountryConsultaPA;
		private static string _ConsDispositivoConsultaPA;
		private static string _ConsLanguageConsultaPA;
		private static string _ConsModuloConsultaPA;
		private static string _ConsMsgTypeConsultaPA;
		private static string _ConsOperationConsultaPA;
		private static string _ConsPidConsultaPA;
		private static string _ConsSystemConsultaPA;
		private static string _ConsWsIpConsultaPA;
		private static string _ConsCodigoPDVTeleventas;
		private static string _ConsMsjValidacionSecPendPagoLink;
		private static string _ConsPDVPermitidosCAI;
		private static string _ConsMsjValidacionSubFormularioCAI;
		private static string _ConsMsjValidacionSecPendPagoLinkEnVentaExp;
		private static string _ConsMsjSMS;
		private static string _Key_MsjValidacionSinNumeroTelefono;
		private static string _Key_TipoOperacionSICAR;
		private static string _Key_CodMaterialPermitidosSICAR;
		private static string _Key_MsjSecPendienteEvaluacionSICAR;
		private static string _Key_MsjSecPendienteAprobacionSICAR;
			
		public static string ConsFlagAplicaCAI { get{return _ConsFlagAplicaCAI;} set{ _ConsFlagAplicaCAI = value;} }
		public static string ConsTiempoSecPendientePagoLink{ get{return _ConsTiempoSecPendientePagoLink;} set{ _ConsTiempoSecPendientePagoLink = value;} }
		public static string ConsConsumerConsultaPA { get{return _ConsConsumerConsultaPA;} set{ _ConsConsumerConsultaPA = value;} }
		public static string ConsCountryConsultaPA { get{return _ConsCountryConsultaPA;} set{ _ConsCountryConsultaPA = value;} }
		public static string ConsDispositivoConsultaPA { get{return _ConsDispositivoConsultaPA;} set{ _ConsDispositivoConsultaPA = value;} }
		public static string ConsLanguageConsultaPA { get{return _ConsLanguageConsultaPA;} set{ _ConsLanguageConsultaPA = value;} }
		public static string ConsModuloConsultaPA { get{return _ConsModuloConsultaPA;} set{ _ConsModuloConsultaPA = value;} }
		public static string ConsMsgTypeConsultaPA { get{return _ConsMsgTypeConsultaPA;} set{ _ConsMsgTypeConsultaPA = value;} }
		public static string ConsOperationConsultaPA { get{return _ConsOperationConsultaPA;} set{ _ConsOperationConsultaPA = value;} }
		public static string ConsPidConsultaPA { get{return _ConsPidConsultaPA;} set{ _ConsPidConsultaPA = value;} }
		public static string ConsSystemConsultaPA { get{return _ConsSystemConsultaPA;} set{ _ConsSystemConsultaPA = value;} }
		public static string ConsWsIpConsultaPA { get{return _ConsWsIpConsultaPA;} set{ _ConsWsIpConsultaPA = value;} }
		public static string ConsCodigoPDVTeleventas { get{return _ConsCodigoPDVTeleventas;} set{ _ConsCodigoPDVTeleventas = value;} }
		public static string ConsMsjValidacionSecPendPagoLink { get{return _ConsMsjValidacionSecPendPagoLink;} set{ _ConsMsjValidacionSecPendPagoLink = value;} }
		public static string ConsPDVPermitidosCAI { get{return _ConsPDVPermitidosCAI;} set{ _ConsPDVPermitidosCAI = value;} }
		public static string ConsMsjValidacionSubFormularioCAI { get{return _ConsMsjValidacionSubFormularioCAI;} set{ _ConsMsjValidacionSubFormularioCAI = value;} }
		public static string ConsMsjValidacionSecPendPagoLinkEnVentaExp { get{return _ConsMsjValidacionSecPendPagoLinkEnVentaExp;} set{ _ConsMsjValidacionSecPendPagoLinkEnVentaExp = value;} }
		public static string ConsMsjSMS { get{return _ConsMsjSMS;} set{ _ConsMsjSMS = value;} }
		public static string Key_MsjValidacionSinNumeroTelefono { get{return _Key_MsjValidacionSinNumeroTelefono;} set{ _Key_MsjValidacionSinNumeroTelefono = value;} }
		public static string Key_TipoOperacionSICAR { get{return _Key_TipoOperacionSICAR;} set{ _Key_TipoOperacionSICAR = value;} }
		public static string Key_CodMaterialPermitidosSICAR { get{return _Key_CodMaterialPermitidosSICAR;} set{ _Key_CodMaterialPermitidosSICAR = value;} }
		public static string Key_MsjSecPendienteEvaluacionSICAR { get{return _Key_MsjSecPendienteEvaluacionSICAR;} set{ _Key_MsjSecPendienteEvaluacionSICAR = value;} }
		public static string Key_MsjSecPendienteAprobacionSICAR { get{return _Key_MsjSecPendienteAprobacionSICAR;} set{ _Key_MsjSecPendienteAprobacionSICAR = value;} }
		//INICIATIVA 712 Cobro Anticipado FIN
		//PROY-140715 | No Biometria en SISACT x caida RENIEC | INI
		private static string _Key_FlagGeneral;
		public static string Key_FlagGeneral { get{return _Key_FlagGeneral;} set{ _Key_FlagGeneral = value;} }
		//PROY-140715 | FIN
		//INICIATIVA-1006- TIENDA VIRTUAL--INICIO
		private static string _Key_MsjAccPendientePago;
		public static string Key_MsjAccPendientePago{get{return _Key_MsjAccPendientePago;} set{_Key_MsjAccPendientePago = value;}}
		private static string _Key_MsjAccAnulacion;
		public static string Key_MsjAccAnulacion{get{return _Key_MsjAccAnulacion;} set{_Key_MsjAccAnulacion = value;}}
		private static string _Key_MsjAccPagado;
		public static string Key_MsjAccPagado{get{return _Key_MsjAccPagado;} set{_Key_MsjAccPagado = value;}}
		//INICIATIVA-1006- TIENDA VIRTUAL--FIN

                //VCANCHIC  PROY  140743
		private static string _Key_TipoPagoAccCuo;
		private static string _Key_TipoMovPos;
		private static string _Key_SubClasMovPos;
		private static string _Key_ClasMovPos;
		private static string _Key_CodMovPos;
		private static string _CONS_ACCCUO_METODO;
		private static string _CONS_ACCCUO_TIPO_INTER;
		private static string _CONS_ACCCUO_AGENTE;
		private static string _CONS_ACCUO_USUARIO;
		private static string _CONS_ACCCUO_FLAG;
		private static string _CONS_ACCCUO_RESULTADO;
		private static string _CONS_ACCCUO_HECHO;

		public static string Key_TipoMovPos { get{return _Key_TipoMovPos;} set{ _Key_TipoMovPos = value;} }
		public static string Key_TipoPagoAccCuo { get{return _Key_TipoPagoAccCuo;} set{ _Key_TipoPagoAccCuo = value;} }
		public static string Key_SubClasMovPos{get{return _Key_SubClasMovPos;}set{_Key_SubClasMovPos= value;}}
		public static string Key_ClasMovPos{get{return _Key_ClasMovPos;}set{_Key_ClasMovPos= value;}}
		public static string Key_CodMovPos{get{return _Key_CodMovPos;}set{_Key_CodMovPos= value;}}
		public static string CONS_ACCCUO_METODO{get{return _CONS_ACCCUO_METODO;}set{_CONS_ACCCUO_METODO= value;}}
		public static string CONS_ACCCUO_TIPO_INTER{get{return _CONS_ACCCUO_TIPO_INTER;}set{_CONS_ACCCUO_TIPO_INTER= value;}}	
		public static string CONS_ACCCUO_AGENTE{get{return _CONS_ACCCUO_AGENTE;}set{_CONS_ACCCUO_AGENTE= value;}}
		public static string CONS_ACCUO_USUARIO{get{return _CONS_ACCUO_USUARIO;}set{_CONS_ACCUO_USUARIO= value;}}
		public static string CONS_ACCCUO_FLAG{get{return _CONS_ACCCUO_FLAG;}set{_CONS_ACCCUO_FLAG= value;}}
		public static string CONS_ACCCUO_RESULTADO{get{return _CONS_ACCCUO_RESULTADO;}set{_CONS_ACCCUO_RESULTADO= value;}}
		public static string CONS_ACCCUO_HECHO{get{return _CONS_ACCCUO_HECHO;}set{_CONS_ACCCUO_HECHO= value;}}
		//VCANCHIC  PROY  140743

		//IDEA IDEA-143176 - GPRD

		private static string _Key_CodigoProgramacionReposicion;
		public static string Key_CodigoProgramacionReposicion { get{return _Key_CodigoProgramacionReposicion;} set{ _Key_CodigoProgramacionReposicion = value;} }
		private static string _Key_MensajeReposicionProgramada;
		public static string Key_MensajeReposicionProgramada { get{return _Key_MensajeReposicionProgramada;} set{ _Key_MensajeReposicionProgramada = value;} }
		private static string _Key_FlagReposicionProgramada;
		public static string Key_FlagReposicionProgramada { get{return _Key_FlagReposicionProgramada;} set{ _Key_FlagReposicionProgramada = value;} }
		private static string _Key_TiempoProgramacionReposicion;
		public static string Key_TiempoProgramacionReposicion { get{return _Key_TiempoProgramacionReposicion;} set{ _Key_TiempoProgramacionReposicion = value;} }
		private static string _Key_EstadoProgramacionReposicion;
		public static string Key_EstadoProgramacionReposicion { get{return _Key_EstadoProgramacionReposicion;} set{ _Key_EstadoProgramacionReposicion = value;} }
		private static string _Key_TipoServicioPrepago;
		public static string Key_TipoServicioPrepago { get{return _Key_TipoServicioPrepago;} set{ _Key_TipoServicioPrepago = value;} }
		private static string _Key_TipoServicioPostpago;
		public static string Key_TipoServicioPostpago { get{return _Key_TipoServicioPostpago;} set{ _Key_TipoServicioPostpago = value;} }
		private static string _Key_CodigoServicioProgRepo;
		public static string Key_CodigoServicioProgRepo { get{return _Key_CodigoServicioProgRepo;} set{ _Key_CodigoServicioProgRepo = value;} }
		private static string _Key_TipoRegistroProgRepo;
		public static string Key_TipoRegistroProgRepo { get{return _Key_TipoRegistroProgRepo;} set{ _Key_TipoRegistroProgRepo = value;} }
		private static string _Key_EsBatchProgRepo;
		public static string Key_EsBatchProgRepo { get{return _Key_EsBatchProgRepo;} set{ _Key_EsBatchProgRepo = value;} }
		private static string _Key_descCodServicioProgRepo;
		public static string Key_descCodServicioProgRepo { get{return _Key_descCodServicioProgRepo;} set{ _Key_descCodServicioProgRepo = value;} }
		private static string _Key_ClienteSapPrepago;
		public static string Key_ClienteSapPrepago { get{return _Key_ClienteSapPrepago;} set{ _Key_ClienteSapPrepago = value;} }
		private static string _Key_ClienteSapPostpago;
		public static string Key_ClienteSapPostpago { get{return _Key_ClienteSapPostpago;} set{ _Key_ClienteSapPostpago = value;} }

		//IDEA IDEA-143176 - GPRD
                //IDEA300216 INI
                private static string _Key_DescripcionOperacionAlta;
		public static string Key_DescripcionOperacionAlta { get{return _Key_DescripcionOperacionAlta;} set{ _Key_DescripcionOperacionAlta = value;} }
		private static string _Key_DescripcionOperacionPortabilidad;
		public static string Key_DescripcionOperacionPortabilidad { get{return _Key_DescripcionOperacionPortabilidad;} set{ _Key_DescripcionOperacionPortabilidad = value;} }
		private static string _Key_DescripcionOperacionRenovacion;
		public static string Key_DescripcionOperacionRenovacion { get{return _Key_DescripcionOperacionRenovacion;} set{ _Key_DescripcionOperacionRenovacion = value;} }
		private static string _Key_DescripcionOperacionRenovacionPack;
		public static string Key_DescripcionOperacionRenovacionPack { get{return _Key_DescripcionOperacionRenovacionPack;} set{ _Key_DescripcionOperacionRenovacionPack = value;} }
		private static string _Key_DescripcionOperacionReposicion;
		public static string Key_DescripcionOperacionReposicion { get{return _Key_DescripcionOperacionReposicion;} set{ _Key_DescripcionOperacionReposicion = value;} }
		private static string _Key_TipoLineaPostpago;
		public static string Key_TipoLineaPostpago { get{return _Key_TipoLineaPostpago;} set{ _Key_TipoLineaPostpago = value;} }
		private static string _Key_TipoLineaPrepago;
		public static string Key_TipoLineaPrepago { get{return _Key_TipoLineaPrepago;} set{ _Key_TipoLineaPrepago = value;} }
		private static string _Key_ConsultaEstadoClaveUnica;
		public static string Key_ConsultaEstadoClaveUnica { get{return _Key_ConsultaEstadoClaveUnica;} set{ _Key_ConsultaEstadoClaveUnica = value;} }

		private static string _Key_MensajeAnuladaClaveUnica;
		public static string Key_MensajeAnuladaClaveUnica { get{return _Key_MensajeAnuladaClaveUnica;} set{ _Key_MensajeAnuladaClaveUnica = value;} }
		private static string _Key_MensajeAprobadaClaveUnica;
		public static string Key_MensajeAprobadaClaveUnica { get{return _Key_MensajeAprobadaClaveUnica;} set{ _Key_MensajeAprobadaClaveUnica = value;} }
		private static string _Key_MensajeErrorConsultaClaveUnica;
		public static string Key_MensajeErrorConsultaClaveUnica { get{return _Key_MensajeErrorConsultaClaveUnica;} set{ _Key_MensajeErrorConsultaClaveUnica = value;} }
		private static string _Key_MensajePendienteClaveUnica;
		public static string Key_MensajePendienteClaveUnica { get{return _Key_MensajePendienteClaveUnica;} set{ _Key_MensajePendienteClaveUnica = value;} }
		private static string _Key_MensajeRechazadaClaveUnica;
		public static string Key_MensajeRechazadaClaveUnica { get{return _Key_MensajeRechazadaClaveUnica;} set{ _Key_MensajeRechazadaClaveUnica = value;} }
                //IDEA300216 FIN
		//IDEA-300846 INI
		private static string _Key_MsjValidaPinNoPinCAC;
		private static string _Key_MsjValidaPinNoVigenteCAC;
		private static string _Key_MsjSPEnviadaCAC;
		private static string _Key_MsjSPRechazoNoPinNoDeudaCAC;
		private static string _Key_MsjSPRechazoPinCAC;
		private static string _Key_MsjSPRechazoDeudaCAC;

		private static string _Key_MotivoErrorPin;
		private static string _Key_MotivoRechazoDeuda;
		private static string _Key_MsjErrorGenerarPin;
		
		public static string Key_MsjValidaPinNoPinCAC { get{return _Key_MsjValidaPinNoPinCAC;} set{ _Key_MsjValidaPinNoPinCAC= value;} }
		public static string Key_MsjValidaPinNoVigenteCAC { get{return _Key_MsjValidaPinNoVigenteCAC;} set{ _Key_MsjValidaPinNoVigenteCAC = value;} }
		public static string Key_MsjSPEnviadaCAC { get{return _Key_MsjSPEnviadaCAC;} set{ _Key_MsjSPEnviadaCAC = value;} }
		public static string Key_MsjSPRechazoNoPinNoDeudaCAC { get{return _Key_MsjSPRechazoNoPinNoDeudaCAC;} set{ _Key_MsjSPRechazoNoPinNoDeudaCAC = value;} }
		public static string Key_MsjSPRechazoPinCAC { get{return _Key_MsjSPRechazoPinCAC;} set{ _Key_MsjSPRechazoPinCAC = value;} }
		public static string Key_MsjSPRechazoDeudaCAC { get{return _Key_MsjSPRechazoDeudaCAC;} set{ _Key_MsjSPRechazoDeudaCAC = value;} }

		public static string Key_MotivoErrorPin { get{return _Key_MotivoErrorPin;} set{ _Key_MotivoErrorPin = value;} }
		public static string Key_MotivoRechazoDeuda { get{return _Key_MotivoRechazoDeuda;} set{ _Key_MotivoRechazoDeuda = value;} }
		public static string Key_MsjErrorGenerarPin { get{return _Key_MsjErrorGenerarPin;} set{ _Key_MsjErrorGenerarPin = value;} }

		//IDEA-300846 FIN
	}
	#endregion
}
