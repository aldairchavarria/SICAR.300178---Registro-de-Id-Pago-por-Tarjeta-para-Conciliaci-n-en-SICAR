using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{
	//INICIATIVA 995-INI
	[Serializable]
	public class BodyResponsePagoEnvioSMS
	{
		private string _itemId;
		private string _statusId;
		private string _flowCode;
		private string _externalId;
		private string _msisdn;
		private string _type;
		private string _operation;
		private string _flowId;

	
		
		public string itemId
		{
			set { _itemId = value; }
			get { return _itemId; }
		}

		public string statusId
		{
			set { _statusId = value; }
			get { return _statusId; }
		}

		public string flowCode
		{
			set { _flowCode = value; }
			get { return _flowCode; }
		}

		public string externalId
		{
			set { _externalId = value; }
			get { return _externalId; }
		}

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
        
		public string flowId
		{
			set { _flowId = value; }
			get { return _flowId; }
		}
		

//		public BodyResponsePagoEnvioSMS()
//		{
////			this.listaFormaPagoPDV = new listaFormaPagoPDV[0]; 
////			this.listaOficinasVenta = new listaOficinasVenta[0];
//		}
	}
}
