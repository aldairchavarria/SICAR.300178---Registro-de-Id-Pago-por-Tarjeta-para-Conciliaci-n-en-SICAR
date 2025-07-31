using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPAGenericResponse.
	/// </summary>
	public class ActualizaPAGenericResponse
	{
		public ActualizaPAGenericResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private ActualizaPAMessageResponse _MessageResponse;

		public ActualizaPAMessageResponse MessageResponse
		{
			set{_MessageResponse = value;}
			get{return _MessageResponse;}
		}
	}
}
