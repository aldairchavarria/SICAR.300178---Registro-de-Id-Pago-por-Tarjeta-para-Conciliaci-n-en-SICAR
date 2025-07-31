using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPAMessageRequest.
	/// </summary>
	public class ActualizaPAMessageRequest
	{
		public ActualizaPAMessageRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private DataPowerRest.HeadersRequest _Header;
		private ActualizaPABodyRequest _Body;


		public DataPowerRest.HeadersRequest Header
		{
			set{_Header = value;}
			get{return _Header;}
		}

		public ActualizaPABodyRequest Body
		{
			set{_Body = value;}
			get{return _Body;}
		}
	}
}
