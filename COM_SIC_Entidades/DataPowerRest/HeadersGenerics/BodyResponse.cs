//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for BodyResponse.
	/// </summary>
	[Serializable]
	public class BodyResponse
	{
		public BodyResponse()
		{
			
		}

		private string _mensajeError;
		private string _codigoRespuesta;
		private string _mensajeRespuesta;
		//PROY-140372
		private string _resultCode;
		private string _resultMessage;
		//PROY-140372


		public string mensajeError
		{
			set{_mensajeError= value;}
			get{ return _mensajeError;}
		}

		  
		public string codigoRespuesta
		{
			set{_codigoRespuesta= value;}
			get{ return _codigoRespuesta;}
		}

		  
		public string mensajeRespuesta
		{
			set{_mensajeRespuesta= value;}
			get{ return _mensajeRespuesta;}
		}
        
		//PROY-140372 INI 
		public string resultCode
		{
			set{_resultCode=value;}
			get{return _resultCode;}
		}

		public string resultMessage
		{
			set {_resultMessage=value;}
			get{return _resultMessage;}
		}
		//PROY-140372 FIN

	}
}
//FIN PROY-140332 (Try & Buy)
