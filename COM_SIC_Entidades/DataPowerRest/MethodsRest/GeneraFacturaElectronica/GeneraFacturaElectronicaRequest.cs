using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for GeneraFacturaElectronicaRequest.
	/// </summary>
	public class GeneraFacturaElectronicaRequest
	{
		private MessageRequest _MessageRequest;

		public GeneraFacturaElectronicaRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public MessageRequest MessageRequest
		{
			set{_MessageRequest= value;}
			get{ return _MessageRequest;}
		}
	}
}
