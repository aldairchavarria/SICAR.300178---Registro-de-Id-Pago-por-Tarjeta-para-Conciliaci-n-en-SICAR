using System;
using System.Runtime.Serialization;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.claro_post_programacionRepo.Request
{
	/// <summary>
	/// Summary description for MessageRequestProgramarTarea.
	/// </summary>
	public class MessageRequestProgramarTarea
	{
		private MessageRequest _MessageRequest;
		public MessageRequest MessageRequest {get{return _MessageRequest;} set{ _MessageRequest = value;}}


		public MessageRequestProgramarTarea()
		{
			_MessageRequest = new MessageRequest();
		}
	}

	[Serializable]
	public class MessageRequest
	{
		private HeadersRequest _header;
		private Body _body;

		public HeadersRequest Header {get{return _header;} set{_header = value;}}
		public Body Body {get{return _body;} set{_body = value;} }

		public MessageRequest()
		{
			_header = new HeadersRequest();
			_body = new Body();
		}
	}

	[Serializable]
	public class Body
	{
		private ProgramarTareaRequest _programarTareaRequest;
		public ProgramarTareaRequest programarTareaRequest { get{ return _programarTareaRequest;} set{ _programarTareaRequest = value;}}
		public Body()
		{
			_programarTareaRequest = new ProgramarTareaRequest();
		}
	}

	[Serializable]
	public class ProgramarTareaRequest
	{
		private string _serviCodProgramar ;
		private string _msisdn ;
		private string _fechaProgramacion ;
		private string _codId ;
		private string _idTransaccion ;
		private string _tipoServicio ;
		private string _codigoServicio ;
		private string _tipoRegistro ;
		private string _mailUsuario ;
		private string _estadoProgramacion ;
		private string _esbatch ;
		private string _xmlEntrada ;
		private string _descCodigoServicio ;
		private string _codigoInteraccion ;
		private string _nroCuenta ;
		private string _usuarioSistema ;
		private string _usuarioApplicacion ;	
		private ListaRequestOpcional[] _listaRequestOpcional ;

		public string serviCodProgramar {get{return _serviCodProgramar;} set{ _serviCodProgramar=value;}}
		public string msisdn {get{ return _msisdn;} set{_msisdn = value;}}
		public string fechaProgramacion {get{return _fechaProgramacion;} set{ _fechaProgramacion = value ;}}
		public string codId {get{return _codId;} set{ _codId = value;} }
		public string idTransaccion {get{ return _idTransaccion;} set{ _idTransaccion = value;}}
		public string tipoServicio {get{ return _tipoServicio;} set{ _tipoServicio=value;} }
		public string codigoServicio {get{ return _codigoServicio;} set{ _codigoServicio=value;}}
		public string tipoRegistro {get{ return _tipoRegistro;} set{ _tipoRegistro=value;} }
		public string mailUsuario {get{ return _mailUsuario;} set{ _mailUsuario=value;}}
		public string estadoProgramacion {get{ return _estadoProgramacion;} set{ _estadoProgramacion=value;}}
		public string esbatch {get{ return _esbatch;} set{ _esbatch=value;} }
		public string xmlEntrada {get{ return _xmlEntrada;} set{ _xmlEntrada=value;} }
		public string descCodigoServicio {get{ return _descCodigoServicio;} set{ _descCodigoServicio=value;}}
		public string codigoInteraccion {get{ return _codigoInteraccion;} set{ _codigoInteraccion=value;}}
		public string nroCuenta {get{ return _nroCuenta;} set{ _nroCuenta=value;}}
		public string usuarioSistema {get{ return _usuarioSistema;} set{ _usuarioSistema=value;}}
		public string usuarioApplicacion {get{ return _usuarioApplicacion;} set{ _usuarioApplicacion=value;}}
		public ListaRequestOpcional[] listaRequestOpcional {get{ return _listaRequestOpcional;} set{ _listaRequestOpcional=value;}}
	

		public ProgramarTareaRequest()
		{
			_serviCodProgramar = string.Empty;
			_msisdn = string.Empty;
			_fechaProgramacion = string.Empty;
			_codId = string.Empty;
			_idTransaccion = string.Empty;
			_tipoServicio = string.Empty;
			_codigoServicio = string.Empty;
			_tipoRegistro = string.Empty;
			_mailUsuario = string.Empty;
			_estadoProgramacion = string.Empty;
			_esbatch = string.Empty;
			_xmlEntrada = string.Empty;
			_descCodigoServicio = string.Empty;
			_codigoInteraccion = string.Empty;
			_nroCuenta = string.Empty;
			_usuarioSistema = string.Empty;
			_usuarioApplicacion = string.Empty;
			_listaRequestOpcional = new ListaRequestOpcional[1];
		}


	}

	[Serializable]
	public class ListaRequestOpcional
	{
		private string _campo;
		private string _valor;
		public string campo { get{return _campo;} set{ _campo=value;}}
		public string valor { get{return _valor;} set{ _valor=value;}}
		public ListaRequestOpcional()
		{
			_campo = string.Empty;
			_valor = string.Empty;
		}
	}
}
