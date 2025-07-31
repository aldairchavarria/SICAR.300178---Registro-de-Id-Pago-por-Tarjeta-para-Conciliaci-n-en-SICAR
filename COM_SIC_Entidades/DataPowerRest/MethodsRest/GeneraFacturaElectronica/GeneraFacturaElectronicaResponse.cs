using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for GeneraFacturaElectronicaResponse.
	/// </summary>
	public class GeneraFacturaElectronicaResponse
	{
		private MessageResponse _MessageResponse;

		public GeneraFacturaElectronicaResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public MessageResponse MessageResponse
		{
			set{_MessageResponse= value;}
			get{ return _MessageResponse;}
		}
	}
}
