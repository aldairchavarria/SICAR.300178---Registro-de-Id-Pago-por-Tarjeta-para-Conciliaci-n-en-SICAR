using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for Body.
	/// </summary>
	public class BodyRequest
	{
		private OnlineGenerationRequest _onlineGeneration;


		public BodyRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public OnlineGenerationRequest onlineGeneration
		{
			set{_onlineGeneration= value;}
			get{ return _onlineGeneration;}
		}
		

	}
}
