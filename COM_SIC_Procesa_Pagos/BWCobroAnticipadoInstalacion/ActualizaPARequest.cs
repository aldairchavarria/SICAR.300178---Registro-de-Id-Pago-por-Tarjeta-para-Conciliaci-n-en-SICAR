using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPARequest.
	/// </summary>
	public class ActualizaPARequest
	{
		public ActualizaPARequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private ActualizaPARequestType [] _actualizaPARequestType;

		public ActualizaPARequestType [] actualizaPARequestType
		{
			set{_actualizaPARequestType = value;}
			get{return _actualizaPARequestType;}
		}

	}
}
