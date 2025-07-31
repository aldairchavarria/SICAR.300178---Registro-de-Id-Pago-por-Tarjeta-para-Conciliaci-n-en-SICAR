using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{
	//INICIATIVA 995-INI
	[Serializable] 
	public class BodyRequestPagoEnvioSMS
	{
		private string _msisdn;
		private string _type;
		private string _operation;
		private string _externalId;
		

		

		public string msisdn
		{
			set { _msisdn = value; }
			get { return _msisdn; }
		}
		
		public string type
		{
			set { _type = value; }
			get { return _type; }
		}

		public string operation
		{
			set { _operation = value; }
			get { return _operation; }
		}

		public string externalId
		{
			set { _externalId = value; }
			get { return _externalId; }
		}

		
		public BodyRequestPagoEnvioSMS()
		{	
		}
	}
}
