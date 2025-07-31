using System;
using System.Runtime.Serialization;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.claro_post_programacionRepo.Response
{
	/// <summary>
	/// Summary description for MessageResponseProgramarTarea.
	/// </summary>
	[Serializable]
	public class MessageResponseProgramarTarea
	{
		private MessageResponse _MessageResponse;
		public MessageResponse MessageResponse {get{return _MessageResponse;} set{ _MessageResponse = value;}}
		public MessageResponseProgramarTarea()
		{
			_MessageResponse = new MessageResponse();
		}
	}
	[Serializable]
	public class MessageResponse
	{
		private HeadersResponse _Header;
		private Body _Body;

		public HeadersResponse Header {get{return _Header;} set{ _Header = value;}}
		public Body Body {get{return _Body;} set{ _Body = value;}}

		public MessageResponse()
		{
			_Header = new HeadersResponse();
			_Body = new Body();
		}
	}

	[Serializable]
	public class Body
	{
		private ProgramarTareaResponse _programarTareaResponse;
		private Clarofault _clarofault;

		public ProgramarTareaResponse programarTareaResponse {get{return _programarTareaResponse;} set{ _programarTareaResponse = value;}}
		public Clarofault clarofault {get{return _clarofault;} set{ _clarofault = value;}}
		public Body()
		{
			_programarTareaResponse = new ProgramarTareaResponse();
			_clarofault = new Clarofault();
		}
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

		public string idAudit {get{return _idAudit;} set{ _idAudit = value;}}
		public string codeError {get{return _codeError;} set{ _codeError = value;}}
		public string descriptionError {get{return _descriptionError;} set{ _descriptionError = value;}}
		public string locationError {get{return _locationError;} set{ _locationError = value;}}
		public string date {get{return _date;} set{ _date = value;}}
		public string originError {get{return _originError;} set{ _originError = value;}}

		public Clarofault()
		{
			_idAudit = "";
			_codeError = "";
			_descriptionError = "";
			_locationError = "";
			_date = "";
			_originError = "";
		}
	}

	[Serializable]
	public class ProgramarTareaResponse
	{
		private ResponseStatus _responseStatus;
		private object _listaResponseOpcional;

		public ResponseStatus responseStatus {get{return _responseStatus;} set{ _responseStatus = value;}}
		public object listaResponseOpcional {get{return _listaResponseOpcional;} set{ _listaResponseOpcional = value;}}

		public ProgramarTareaResponse()
		{
			_responseStatus = new ResponseStatus();
			_listaResponseOpcional = new object();
		}
	}

	[Serializable]
	public class ResponseStatus
	{
		private string _status;
		private string _idTransaccion;
		private string _codigoRespuesta;
		private string _mensajeRespuesta;

		public string status {get{return _status;} set{ _status = value;}}
		public string idTransaccion {get{return _idTransaccion;} set{ _idTransaccion = value;}}
		public string codigoRespuesta {get{return _codigoRespuesta;} set{ _codigoRespuesta = value;}}
		public string mensajeRespuesta {get{return _mensajeRespuesta;} set{ _mensajeRespuesta = value;}}

		public ResponseStatus()
		{
			_status = "";
			_idTransaccion = "";
			_codigoRespuesta = "";
			_mensajeRespuesta = "";
		}
	}

	public class Status
	{
		private string _type;
		private string _code;
		private string _message;
		private string _msgid;

		public string type {get{return _type;} set{ _type = value;}}
		public string code {get{return _code;} set{ _code = value;}}
		public string message {get{return _message;} set{ _message = value;}}
		public string msgid {get{return _msgid;} set{ _msgid = value;}}

		public Status()
		{
			_type = "";
			_code = "";
			_message = "";
			_msgid = "";
		}
	}
}
