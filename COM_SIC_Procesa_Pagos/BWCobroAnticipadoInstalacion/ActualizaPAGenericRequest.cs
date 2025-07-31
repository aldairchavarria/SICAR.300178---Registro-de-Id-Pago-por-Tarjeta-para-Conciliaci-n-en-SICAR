using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPAGenericRequest.
	/// </summary>
	public class ActualizaPAGenericRequest
	{
		public ActualizaPAGenericRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private ActualizaPAMessageRequest _MessageRequest;

		public ActualizaPAMessageRequest MessageRequest
		{
			set{_MessageRequest = value;}
			get{return _MessageRequest;}
		}
	}
}