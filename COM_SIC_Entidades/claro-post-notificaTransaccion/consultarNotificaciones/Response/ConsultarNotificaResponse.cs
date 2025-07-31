using System;
using System.Runtime.Serialization;
using System.Text;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Response
{
	/// <summary>
	/// Summary description for ConsultarNotificaResponse.
	/// </summary>
	
	[Serializable]
	public class ConsultarNotificaResponse
	{
		
		private MessageResponse _MessageResponse ;
		public MessageResponse MessageResponse {get{ return _MessageResponse;} set{_MessageResponse = value;}}

		public ConsultarNotificaResponse()
		{
			MessageResponse = new MessageResponse();
		}
	}

	[Serializable]
	public class MessageResponse
	{
		private HeadersResponse _Header;
		private Body _Body;

		public HeadersResponse Header {get{ return _Header;} set{_Header = value;}}
		public Body Body {get{ return _Body;} set{_Body = value;}}
		public MessageResponse()
		{
			Header = new HeadersResponse();
			Body = new Body();
		}
	}

	[Serializable]
	public class Body
	{
		private ConsultarNotificacionesResponse _consultarNotificacionesResponse;
		private Clarofault _clarofault;

		public ConsultarNotificacionesResponse consultarNotificacionesResponse {get{ return _consultarNotificacionesResponse;} set{_consultarNotificacionesResponse = value;}}
		public Clarofault clarofault {get{ return _clarofault;} set{_clarofault = value;}}

		public Body()
		{
			consultarNotificacionesResponse = new ConsultarNotificacionesResponse();
			clarofault = new Clarofault();
		}
	}


	[Serializable]
	public class ConsultarNotificacionesResponse
	{
		private ResponseStatus _responseStatus;
		private ListaNotificaciones[] _listaNotificaciones;
		private responseOpcional[] _responseOpcional;

		public ResponseStatus responseStatus {get{ return _responseStatus;} set{_responseStatus = value;}}
		public ListaNotificaciones[] listaNotificaciones {get{ return _listaNotificaciones;} set{_listaNotificaciones = value;}}
		public responseOpcional[] responseOpcional {get{ return _responseOpcional;} set{_responseOpcional = value;}}

		public ConsultarNotificacionesResponse()
		{
			responseStatus = new ResponseStatus();
		}
		
	}

	[Serializable]
	public class responseOpcional
	{
		private string _campo;
		private string _valor;

		public string campo {get{ return _campo;} set{_campo = value;}}
		public string valor {get{ return _valor;} set{_valor = value;}}
		
		public responseOpcional()
		{
			campo = string.Empty;
			valor = string.Empty;
		}
	}
	


	[Serializable]
	public class ListaNotificaciones
	{
		private string _notificacionTransaccion;
		private string _tipoDoc;
		private string _numDoc;
		private string _numCont;
		private string _tipoLinea;
		private string _iccid;
		private string _modalidad;
		private string _estadoNotif;
		private string _fechaHoraReg;
		private int _numLineasEnviadas;
		private string _fechaHoraEjec;
		private string _pdv;
		private string _asesor;
		private string _vigencia;
		private string _aplicacion;
		private string _centrodeAtencion;
		private string _plan;
		private string _equipo;
		private string _monto;
		private string _tipoDocNuevoTitular;
		private string _numDocNuevoTitular;
		private string _nomApeNuevoTitular;
		private string _tipoOperacion;
		private string _sec;
		private string _nroPedido;
		private listaOpcional[] _listaOpcional;

		public string notificacionTransaccion {get{ return _notificacionTransaccion;} set{_notificacionTransaccion = value;}}
		public string tipoDoc {get{ return _tipoDoc;} set{_tipoDoc = value;}}
		public string numDoc {get{ return _numDoc;} set{_numDoc = value;}}
		public string numCont {get{ return _numCont;} set{_numCont = value;}}
		public string tipoLinea {get{ return _tipoLinea;} set{_tipoLinea = value;}}
		public string iccid {get{ return _iccid;} set{_iccid = value;}}
		public string modalidad {get{ return _modalidad;} set{_modalidad = value;}}
		public string estadoNotif {get{ return _estadoNotif;} set{_estadoNotif = value;}}
		public string fechaHoraReg {get{ return _fechaHoraReg;} set{_fechaHoraReg = value;}}
		public int numLineasEnviadas {get{ return _numLineasEnviadas;} set{_numLineasEnviadas = value;}}
		public string fechaHoraEjec {get{ return _fechaHoraEjec;} set{_fechaHoraEjec = value;}}
		public string pdv {get{ return _pdv;} set{_pdv = value;}}
		public string asesor {get{ return _asesor;} set{_asesor = value;}}
		public string vigencia {get{ return _vigencia;} set{_vigencia = value;}}
		public string aplicacion {get{ return _aplicacion;} set{_aplicacion = value;}}
		public string centrodeAtencion {get{ return _centrodeAtencion;} set{_centrodeAtencion = value;}}
		public string plan {get{ return _plan;} set{_plan = value;}}
		public string equipo {get{ return _equipo;} set{_equipo = value;}}
		public string monto {get{ return _monto;} set{_monto = value;}}
		public string tipoDocNuevoTitular {get{ return _tipoDocNuevoTitular;} set{_tipoDocNuevoTitular = value;}}
		public string numDocNuevoTitular {get{ return _numDocNuevoTitular;} set{_numDocNuevoTitular = value;}}
		public string nomApeNuevoTitular {get{ return _nomApeNuevoTitular;} set{_nomApeNuevoTitular = value;}}
		public string tipoOperacion {get{ return _tipoOperacion;} set{_tipoOperacion = value;}}
		public string sec {get{ return _sec;} set{_sec = value;}}
		public string nroPedido {get{ return _nroPedido;} set{_nroPedido = value;}}
		public listaOpcional[] listaOpcional {get{ return _listaOpcional;} set{_listaOpcional = value;}}


	}

	[Serializable]
	public class listaOpcional
	{
		private string _campo;
		private string _valor;

		public string campo {get{ return _campo;} set{_campo = value;}}
		public string valor {get{ return _valor;} set{_valor = value;}}
		
	}

	[Serializable]
	public class Clarofault
	{
		private string _idAudit;
		private string _codeError;
		private string _descriptionError;
		private string _locationError;
		private string _date;
		private string _originError;


		public string idAudit {get{ return _idAudit;} set{_idAudit = value;}}
		public string codeError {get{ return _codeError;} set{_codeError = value;}}
		public string descriptionError {get{ return _descriptionError;} set{_descriptionError = value;}}
		public string locationError {get{ return _locationError;} set{_locationError = value;}}
		public string date {get{ return _date;} set{_date = value;}}
		public string originError {get{ return _originError;} set{_originError = value;}}

		
	}

	[Serializable]
	public class ResponseStatus
	{
		private string _status;
		private string _idTransaccion;
		private string _codigoRespuesta;
		private string _mensajeRespuesta;

		public string status {get{ return _status;} set{_status = value;}}
		public string idTransaccion {get{ return _idTransaccion;} set{_idTransaccion = value;}}
		public string codigoRespuesta {get{ return _codigoRespuesta;} set{_codigoRespuesta = value;}}
		public string mensajeRespuesta {get{ return _mensajeRespuesta;} set{_mensajeRespuesta = value;}}

		
	}


	[Serializable]
	public class Status
	{
		private string _type;
		private string _code;
		private string _message;
		private string _msgid;

		public string type {get{ return _type;} set{_type = value;}}
		public string code {get{ return _code;} set{_code = value;}}
		public string message {get{ return _message;} set{_message = value;}}
		public string msgid {get{ return _msgid;} set{_msgid = value;}}

	}


}
