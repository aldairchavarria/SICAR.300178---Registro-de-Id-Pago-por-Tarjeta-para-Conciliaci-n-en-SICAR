using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ConsultaPARequestBody.
	/// </summary>
	public class ConsultaPARequestBody
	{
		private ConsultaPARequest _consultaPARequest;

		public ConsultaPARequestBody()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ConsultaPARequest consultaPARequest
		{
			set{_consultaPARequest = value;}
			get{return _consultaPARequest;}
		}
	}
}
