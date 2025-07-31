//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for RespuestaRest.
	/// </summary>
	[Serializable]
	public class RespuestaRest
	{
		
		private MessageResponse _messageResponse;

		public MessageResponse MessageResponse {get{ return _messageResponse;} set{_messageResponse = value;}}

		public RespuestaRest()
		{
			_messageResponse = new MessageResponse();
		}


	}
}
//FIN PROY-140332 (Try & Buy)