using System;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Request
{
	/// <summary>
	/// Summary description for ConsultarNotificaRequest.
	/// </summary>
	public class ConsultarNotificaRequest
	{
		private MessageRequest _MessageRequest ;
		public MessageRequest MessageRequest {get{ return _MessageRequest;} set{_MessageRequest = value;}}
		public ConsultarNotificaRequest()
		{
			MessageRequest = new MessageRequest();
		}
	}
	public class MessageRequest
	{
		private HeadersRequest _Header;
		private Body _Body;

		public HeadersRequest Header {get{ return _Header;} set{_Header  = value;}}
		public Body Body {get{ return _Body;} set{_Body  = value;}}
		public MessageRequest()
		{
			Header = new HeadersRequest();
			Body = new Body();
		}
	}

	public class Body
	{
		private ConsultarNotificacionesRequest _consultarNotificacionesRequest;
		public ConsultarNotificacionesRequest consultarNotificacionesRequest {get{ return _consultarNotificacionesRequest;} set{_consultarNotificacionesRequest  = value;}}
		public Body()
		{
			consultarNotificacionesRequest = new ConsultarNotificacionesRequest();
		}
	}

	public class ConsultarNotificacionesRequest

	{
		private string _tipoDoc;
		private string _numDoc;
		private string _numCont;
		private string _tipoLinea;
		private string _iccid;
		private string _modalidad;
		private string _estadoNotif;
		private string _aplicacion;
		private ListaOpcional[] _listaOpcional;

		public string tipoDoc {get{ return _tipoDoc;} set{_tipoDoc  = value;}}
		public string numDoc {get{ return _numDoc;} set{_numDoc  = value;}}
		public string numCont {get{ return _numCont;} set{_numCont  = value;}}
		public string tipoLinea {get{ return _tipoLinea;} set{_tipoLinea  = value;}}
		public string iccid {get{ return _iccid;} set{_iccid  = value;}}
		public string modalidad {get{ return _modalidad;} set{_modalidad  = value;}}
		public string estadoNotif {get{ return _estadoNotif;} set{_estadoNotif  = value;}}
		public string aplicacion {get{ return _aplicacion;} set{_aplicacion  = value;}}
		public ListaOpcional[] listaOpcional {get{ return _listaOpcional;} set{_listaOpcional  = value;}}

		public ConsultarNotificacionesRequest() 
		{
			tipoDoc = string.Empty;
			numDoc = string.Empty;
			numCont = string.Empty;
			tipoLinea = string.Empty;
			iccid = string.Empty;
			modalidad = string.Empty;
			estadoNotif = string.Empty;
			aplicacion = string.Empty;
		}
	}

	public class ListaOpcional
	{
		private string _campo;
		private string _valor;

		public string campo {get{ return _campo;} set{_campo  = value;}}
		public string valor {get{ return _valor;} set{_valor  = value;}}

		public ListaOpcional() 
		{
			campo = string.Empty;
			valor = string.Empty;
		}
	}
}
