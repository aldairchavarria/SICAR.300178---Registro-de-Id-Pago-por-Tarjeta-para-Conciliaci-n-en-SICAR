//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for MessageResponse.
	/// </summary>
	[Serializable]
	public class MessageResponse
	{
		private HeaderResponse _headerResponse;
		private BodyResponse _bodyResponse;

		public HeaderResponse Header {get{ return _headerResponse;} set{_headerResponse = value;}}
		public BodyResponse Body {get{ return _bodyResponse;} set{_bodyResponse = value;}}

		public MessageResponse()
		{
			_headerResponse = new HeaderResponse();
			_bodyResponse = new BodyResponse();
		}



	}
}
//FIN PROY-140332 (Try & Buy)
