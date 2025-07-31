using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	//INICIATIVA 712 Cobro Anticipado
	/// <summary>
	/// Summary description for ActualizaPABodyRequest.
	/// </summary>
	public class ActualizaPABodyRequest
	{
		public ActualizaPABodyRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private ActualizaPARequest _actualizaPARequest;

		public ActualizaPARequest actualizaPARequest
		{
			set{_actualizaPARequest = value;}
			get{return _actualizaPARequest;}
		}
	}
}
