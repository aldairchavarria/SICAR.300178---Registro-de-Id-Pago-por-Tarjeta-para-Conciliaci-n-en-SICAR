using System;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for MessageRequest.
	/// </summary>
	public class MessageRequest
	{
		private HeadersRequest _header;
		private BodyRequest _Body;

		public MessageRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public HeadersRequest Header
		{
			set{_header= value;}
			get{ return _header;}
		}
		public BodyRequest Body
		{
			set{_Body= value;}
			get{ return _Body;}
		}

	}
}
