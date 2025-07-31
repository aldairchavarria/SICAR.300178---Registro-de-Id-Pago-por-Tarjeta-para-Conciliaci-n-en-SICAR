using System;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for MessageResponse.
	/// </summary>
	public class MessageResponse
	{
		private HeadersResponse _Header;
		private BodyResponse _Body;

		public MessageResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public HeadersResponse Header
		{
			set{_Header= value;}
			get{ return _Header;}
		}

		public BodyResponse Body
		{
			set{_Body= value;}
			get{ return _Body;}
		}
	}
}
