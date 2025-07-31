using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPABodyResponse.
	/// </summary>
	public class ActualizaPABodyResponse
	{
		public ActualizaPABodyResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		private ActualizaPAResponseType _actualizaPAResponseType;

		private DataPowerRest.ClaroFault _claroFault;

		public ActualizaPAResponseType actualizaPAResponseType
		{
			set{_actualizaPAResponseType= value;}
			get{ return _actualizaPAResponseType;}
		}

		public DataPowerRest.ClaroFault claroFault
		{
			set{_claroFault= value;}
			get{ return _claroFault;}
		}
	}
}
