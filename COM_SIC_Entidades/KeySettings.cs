using System;

namespace COM_SIC_Entidades
{
	/// <summary>
	/// Summary description for AppSettings.
	/// </summary>
	public class KeySettings
	{
		private static string _Key_FlagCajeroVirtualAutomatico;
		private static string _Key_TipoVentaPermitido;
		private static string _Key_TipoOperacionPostPermitido;
		private static string _Key_TipoOperacionPrePermitido;
		private static string _Key_TipoProductoPostPermitido;
		private static string _Key_TipoProductoPrePermitido;

		private static string _Key_FlagPermitirProteccionMovil;
		private static string _Key_FlagPermitirBuyBack;


		private static string _Key_CodigoEstadoPagoExitoSICAR;
		private static string _Key_MsjRptaExitoPagoSICAR;

		private static string _Key_CodigoEstadoRecibidoSICAR;
		private static string _Key_CodigoRespuestaExito;
		private static string _Key_MsjRptaExitoRecibidoSICAR;
		private static string _Key_MsjRptaErrorRecibidoSICAR;

		private static string _Key_IdentificarNoCoincide;
		private static string _Key_PedidoNoRetornaDatos;

		private static string _Key_ErrorCajeroVirtual;

		private static string _Key_CodigoEstadoError;
		private static string _Key_CodigoRespuestaError;
		private static string _Key_MsjRptaErrorTipoVenta;
		private static string _Key_MsjRptaErrorTipoOperacion;
		private static string _Key_MsjRptaErrorTipoProducto;
		private static string _Key_MsjRptaErrorPedidoConPM;
		private static string _Key_MsjRptaErrorPedidoEsPM;
		private static string _Key_MsjRptaPedidoBuyBack;
		private static string _Key_MsjRptaPedidoSinDatos;

		private static string _Key_UsuarioNoCuentaConAcceso;
		private static string _Key_CuentaENoEnviada;

		private static string _Key_ReposicionPostpago;
		private static string _Key_Repo_Reno_RenoPackPrepago;

		private static string _Key_FlagNoReintentarPago;
		private static string _Key_FlagReintentarPago;
		
		private static string _Key_MsjErrorValidacionDeliverySICAR;
		private static string _Key_MsjErrorGeneralSICAR; 

		private static string _Key_VentasVariasPostpago;
		private static string _Key_ClaseFacturaPermitido; 
		private static string _Key_MsjRptaErrorClaseFactura;  
		private static string _Key_MsjRptaErrorEstadoPortabilidad; 
		private static string _Key_MsjRptaExitoPagoRASICAR; 	

		private static string _Key_CodigoUsuarioGenericoPago;
			
		//INI: INICIATIVA-219 | CBIO
		private static string _key_flagCBIO;
		private static string _key_DescricpionCBIO;
		private static string _key_TipoOperacionAltaCBIO;
		private static string _key_TipoOperacionMigracionAltaCBIO;
		private static string _key_TipoOperacionReposicionCBIO;
		private static string _key_TipoOperacionRenovacionMasReposicionCBIO;
		private static string _key_PoIDProteccionMovil;
		//FIN: INICIATIVA-219 | CBIO

		//JRM - PROY 140589
		private static string _Key_K_PEDIC_CODTIPOOPERACION;
		//JRM - PROY 140589

		public static string Key_FlagCajeroVirtualAutomatico{get{return _Key_FlagCajeroVirtualAutomatico;} set{_Key_FlagCajeroVirtualAutomatico = value;}}
		public static string Key_TipoVentaPermitido{get{return _Key_TipoVentaPermitido;} set{_Key_TipoVentaPermitido = value;}}
		public static string Key_TipoOperacionPostPermitido{get{return _Key_TipoOperacionPostPermitido;} set{_Key_TipoOperacionPostPermitido = value;}}
		public static string Key_TipoOperacionPrePermitido{get{return _Key_TipoOperacionPrePermitido;} set{_Key_TipoOperacionPrePermitido = value;}}
		public static string Key_TipoProductoPostPermitido{get{return _Key_TipoProductoPostPermitido;} set{_Key_TipoProductoPostPermitido = value;}}
		public static string Key_TipoProductoPrePermitido{get{return _Key_TipoProductoPrePermitido;} set{_Key_TipoProductoPrePermitido = value;}}
		public static string Key_FlagPermitirProteccionMovil{get{return _Key_FlagPermitirProteccionMovil;} set{_Key_FlagPermitirProteccionMovil = value;}}
		public static string Key_FlagPermitirBuyBack{get{return _Key_FlagPermitirBuyBack;} set{_Key_FlagPermitirBuyBack = value;}}
		public static string Key_MsjRptaPedidoSinDatos{get{return _Key_MsjRptaPedidoSinDatos;} set{_Key_MsjRptaPedidoSinDatos = value;}}

		public static string Key_UsuarioNoCuentaConAcceso{get{return _Key_UsuarioNoCuentaConAcceso;} set{_Key_UsuarioNoCuentaConAcceso = value;}}
		public static string Key_CuentaENoEnviada{get{return _Key_CuentaENoEnviada;} set{_Key_CuentaENoEnviada = value;}}

		public static string Key_ReposicionPostpago{get{return _Key_ReposicionPostpago;} set{_Key_ReposicionPostpago = value;}}
		public static string Key_Repo_Reno_RenoPackPrepago{get{return _Key_Repo_Reno_RenoPackPrepago;} set{_Key_Repo_Reno_RenoPackPrepago = value;}}

		public static string Key_CodigoEstadoPagoExitoSICAR{get{return _Key_CodigoEstadoPagoExitoSICAR;} set{_Key_CodigoEstadoPagoExitoSICAR = value;}}
		public static string Key_MsjRptaExitoPagoSICAR{get{return _Key_MsjRptaExitoPagoSICAR;} set{_Key_MsjRptaExitoPagoSICAR = value;}}
		public static string Key_CodigoEstadoRecibidoSICAR{get{return _Key_CodigoEstadoRecibidoSICAR;} set{_Key_CodigoEstadoRecibidoSICAR = value;}}
		public static string Key_CodigoRespuestaExito{get{return _Key_CodigoRespuestaExito;} set{_Key_CodigoRespuestaExito = value;}}
		public static string Key_MsjRptaExitoRecibidoSICAR{get{return _Key_MsjRptaExitoRecibidoSICAR;} set{_Key_MsjRptaExitoRecibidoSICAR = value;}}
		public static string Key_MsjRptaErrorRecibidoSICAR{get{return _Key_MsjRptaErrorRecibidoSICAR;} set{_Key_MsjRptaErrorRecibidoSICAR = value;}}

		public static string Key_IdentificarNoCoincide{get{return _Key_IdentificarNoCoincide;} set{_Key_IdentificarNoCoincide = value;}}
		public static string Key_PedidoNoRetornaDatos{get{return _Key_PedidoNoRetornaDatos;} set{_Key_PedidoNoRetornaDatos = value;}}
        
		public static string Key_CodigoEstadoError{get{return _Key_CodigoEstadoError;} set{_Key_CodigoEstadoError = value;}}
		public static string Key_CodigoRespuestaError{get{return _Key_CodigoRespuestaError;} set{_Key_CodigoRespuestaError = value;}}
		public static string Key_MsjRptaErrorTipoVenta{get{return _Key_MsjRptaErrorTipoVenta;} set{_Key_MsjRptaErrorTipoVenta = value;}}
		public static string Key_MsjRptaErrorTipoOperacion{get{return _Key_MsjRptaErrorTipoOperacion;} set{_Key_MsjRptaErrorTipoOperacion = value;}}
		public static string Key_MsjRptaErrorTipoProducto{get{return _Key_MsjRptaErrorTipoProducto;} set{_Key_MsjRptaErrorTipoProducto = value;}}
		public static string Key_MsjRptaErrorPedidoConPM{get{return _Key_MsjRptaErrorPedidoConPM;} set{_Key_MsjRptaErrorPedidoConPM = value;}}
		public static string Key_MsjRptaErrorPedidoEsPM{get{return _Key_MsjRptaErrorPedidoEsPM;} set{_Key_MsjRptaErrorPedidoEsPM = value;}}
		public static string Key_MsjRptaPedidoBuyBack{get{return _Key_MsjRptaPedidoBuyBack;} set{_Key_MsjRptaPedidoBuyBack = value;}}

		public static string Key_ErrorCajeroVirtual{get{return _Key_ErrorCajeroVirtual;} set{_Key_ErrorCajeroVirtual = value;}}

		public static string Key_FlagNoReintentarPago{get{return _Key_FlagNoReintentarPago;} set{_Key_FlagNoReintentarPago = value;}}
		public static string Key_FlagReintentarPago{get{return _Key_FlagReintentarPago;} set{_Key_FlagReintentarPago = value;}}

		public static string Key_MsjErrorValidacionDeliverySICAR{get{return _Key_MsjErrorValidacionDeliverySICAR;} set{_Key_MsjErrorValidacionDeliverySICAR = value;}}
		public static string Key_MsjErrorGeneralSICAR{get{return _Key_MsjErrorGeneralSICAR;} set{_Key_MsjErrorGeneralSICAR = value;}}

		public static string Key_VentasVariasPostpago{get{return _Key_VentasVariasPostpago;} set{_Key_VentasVariasPostpago = value;}}
		public static string Key_ClaseFacturaPermitido{get{return _Key_ClaseFacturaPermitido;} set{_Key_ClaseFacturaPermitido = value;}}
		public static string Key_MsjRptaErrorClaseFactura{get{return _Key_MsjRptaErrorClaseFactura;} set{_Key_MsjRptaErrorClaseFactura = value;}}
		public static string Key_MsjRptaErrorEstadoPortabilidad{get{return _Key_MsjRptaErrorEstadoPortabilidad;} set{_Key_MsjRptaErrorEstadoPortabilidad = value;}}
		public static string Key_MsjRptaExitoPagoRASICAR{get{return _Key_MsjRptaExitoPagoRASICAR;} set{_Key_MsjRptaExitoPagoRASICAR = value;}}

		public static string Key_CodigoUsuarioGenericoPago{get{return _Key_CodigoUsuarioGenericoPago;} set{_Key_CodigoUsuarioGenericoPago = value;}}
		
		//INI: INICIATIVA-219 | CBIO
		public static string key_flagCBIO { get{return _key_flagCBIO;} set{ _key_flagCBIO = value;} }	
		public static string key_DescricpionCBIO { get{return _key_DescricpionCBIO;} set{ _key_DescricpionCBIO = value;} }
		public static string key_TipoOperacionAltaCBIO { get{return _key_TipoOperacionAltaCBIO;} set{ _key_TipoOperacionAltaCBIO = value;} }
		public static string key_TipoOperacionMigracionAltaCBIO { get{return _key_TipoOperacionMigracionAltaCBIO;} set{ _key_TipoOperacionMigracionAltaCBIO = value;} }
		public static string key_TipoOperacionReposicionCBIO { get{return _key_TipoOperacionReposicionCBIO;} set{ _key_TipoOperacionReposicionCBIO = value;} }
		public static string key_TipoOperacionRenovacionMasReposicionCBIO { get{return _key_TipoOperacionRenovacionMasReposicionCBIO;} set{ _key_TipoOperacionRenovacionMasReposicionCBIO = value;} }		
		public static string key_PoIDProteccionMovil { get{return _key_PoIDProteccionMovil;} set{ _key_PoIDProteccionMovil = value;} }
		//FIN: INICIATIVA-219 | CBIO
		//JRM - PROY 140589
		public static string Key_K_PEDIC_CODTIPOOPERACION { get{return _Key_K_PEDIC_CODTIPOOPERACION;} set{ _Key_K_PEDIC_CODTIPOOPERACION = value;} }
		//JRM - PROY 140589
		

	}
}
