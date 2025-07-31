using System;
using System.Configuration;
using COM_SIC_Activaciones;


namespace NEGOCIO_SIC_SANS
{
	/// <summary>
	/// Descripción breve de clsCambioPlanPostpago.
	/// </summary>
	public class CambioPlanPostpagoNg
	{
		public CambioPlanPostpagoNg()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

		//Realiza Cambio de Plan Postpago
		public bool ServicioCambioPlanPostPago1(CambioPlanPostpago oCambioPlanPostpago,ref string id,ref string mensaje) 
		{
			Migracion_Postpago.ebsMigracionPlanPostpago_ep oMigracion_Postpago=new Migracion_Postpago.ebsMigracionPlanPostpago_ep();

			string _idTransaccion, _ipAplicacion, _aplicacion, _msisdn, _coId, _customerId, _Cuenta, _escenario, _tipoProducto, _codigoProducto, _codigoPlanBase;
			string _serviciosAdicionales;
			string _montoApadece, _montoFidelizar, _montoPCS;
			string _flagValidaApadece, _flagAplicaApadece, _TopeConsumo, _tipoTope, _descripcionTipoTpe, _tipoRegistroTope;
			string _cicloFacturacion, _topeControlConsumo;
			string _CAC, _asesor, _codigoInteraccion, _areaPCS, _motivoPCS, _subMotivoPCS, _numeroDocumento, _tipoClarify, _numeroCuentaPadre, _usuarioAplicacion, _usuarioSistema, _idTipoCliente, _flagServicioOnTop, _flagLimiteCredito;
			string _fechaProgramacionTope, _fechaProgramacion;

			oMigracion_Postpago.Url = ConfigurationSettings.AppSettings["consRutaWSCambioPlanPostago"];

			_idTransaccion = oCambioPlanPostpago.idTransaccion;
			_ipAplicacion = oCambioPlanPostpago.ipAplicacion;
			_aplicacion =oCambioPlanPostpago.aplicacion;
			_msisdn = oCambioPlanPostpago.msisdn;
			_coId = oCambioPlanPostpago.coId;
			_customerId = oCambioPlanPostpago.customerId;
			_Cuenta = oCambioPlanPostpago.Cuenta;
			_escenario = oCambioPlanPostpago.escenario;
			_tipoProducto = oCambioPlanPostpago.tipoProducto;
			_serviciosAdicionales = oCambioPlanPostpago.serviciosAdicionales;
			_codigoProducto = oCambioPlanPostpago.codigoProducto;
			_codigoPlanBase = oCambioPlanPostpago.codigoPlanBase;
			_montoApadece = oCambioPlanPostpago.montoApadece;
			_montoFidelizar =oCambioPlanPostpago.montoFidelizar;
			_flagValidaApadece = oCambioPlanPostpago.flagValidaApadece;
			_flagAplicaApadece = oCambioPlanPostpago.flagAplicaApadece;
			_TopeConsumo = oCambioPlanPostpago.TopeConsumo;
			_tipoTope = oCambioPlanPostpago.tipoTope;
			_descripcionTipoTpe = oCambioPlanPostpago.descripcionTipoTpe;
			_tipoRegistroTope = oCambioPlanPostpago.tipoRegistroTope;
			_topeControlConsumo = oCambioPlanPostpago.topeControlConsumo;
			_fechaProgramacionTope = oCambioPlanPostpago.fechaProgramacionTope;
			_CAC = oCambioPlanPostpago.CAC;
			_asesor = oCambioPlanPostpago.asesor;
			_codigoInteraccion = oCambioPlanPostpago.codigoInteraccion;
			_montoPCS = oCambioPlanPostpago.montoPCS;
			_areaPCS = oCambioPlanPostpago.areaPCS;
			_motivoPCS = oCambioPlanPostpago.motivoPCS;
			_subMotivoPCS = oCambioPlanPostpago.subMotivoPCS;
			_cicloFacturacion = oCambioPlanPostpago.cicloFacturacion;
			_idTipoCliente = oCambioPlanPostpago.idTipoCliente;
			_numeroDocumento = oCambioPlanPostpago.numeroDocumento;
			//ConsultaSolicitudPospago(hidNroSEC.Value)
			_flagServicioOnTop = oCambioPlanPostpago.flagServicioOnTop;
			_fechaProgramacion = oCambioPlanPostpago.fechaProgramacion;
			_flagLimiteCredito = oCambioPlanPostpago.flagLimiteCredito;
			_tipoClarify = oCambioPlanPostpago.tipoClarify;
			_numeroCuentaPadre = oCambioPlanPostpago.numeroCuentaPadre;
			_usuarioAplicacion = oCambioPlanPostpago.usuarioAplicacion;
			_usuarioSistema = oCambioPlanPostpago.usuarioSistema;

			bool Respuesta=false;
			string RespuestaProgramarMigracion = "",mensajeRespuesta="";
			try
			{
				RespuestaProgramarMigracion = oMigracion_Postpago.programarMigracion(ref _idTransaccion,_ipAplicacion, _aplicacion, _msisdn, _coId, _customerId, _Cuenta, _escenario, _tipoProducto, _serviciosAdicionales, _codigoProducto, _codigoPlanBase, Convert.ToDecimal(_montoApadece), Convert.ToDecimal(_montoFidelizar), _flagValidaApadece, _flagAplicaApadece, _TopeConsumo, Funciones.CheckStr(_tipoTope), Funciones.CheckStr(_descripcionTipoTpe), Funciones.CheckStr(_tipoRegistroTope), Convert.ToInt32(_topeControlConsumo), Convert.ToDateTime(_fechaProgramacionTope), _CAC, _asesor, _codigoInteraccion, Convert.ToDecimal(_montoPCS), _areaPCS, _motivoPCS, _subMotivoPCS, Convert.ToInt32(_cicloFacturacion), _idTipoCliente, _numeroDocumento, _flagServicioOnTop, Convert.ToDateTime(_fechaProgramacion), _flagLimiteCredito, _tipoClarify, _numeroCuentaPadre, _usuarioAplicacion, _usuarioSistema, out mensajeRespuesta);
				id = _idTransaccion;
				mensaje=mensajeRespuesta;
				if(RespuestaProgramarMigracion=="0")
					Respuesta = true;
			}
			catch( Exception ex)
			{
				Respuesta=false;			
			}
			finally
			{
				oMigracion_Postpago=null;
			}			
			return 	Respuesta;
		}

		//Rollback elimina Cambio de Plan Postpago
		public string RollbackCambioPlan(string NumeroCliente, int CodigoTransaccionIntegracion, string CodigoEstadoProgramacion)
		{
			AccionesPostPagoWS.DevIntWS oDevIntWS= new AccionesPostPagoWS.DevIntWS();

			oDevIntWS.Url = ConfigurationSettings.AppSettings["consRutaWSRoolbackCambioPlanPostago"];

			int Respuesta =-1;
			try
			{
				Respuesta = oDevIntWS.borrarProgramacion(NumeroCliente, CodigoTransaccionIntegracion, CodigoEstadoProgramacion);
			}
			catch( Exception ex)
			{
				Respuesta =-1;
			}
			finally
			{
				oDevIntWS=null;
			}
			return Respuesta.ToString(); 
		}
	}
}
