using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPAResponseType.
	/// </summary>
	public class ActualizaPAResponseType
	{
		public ActualizaPAResponseType()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private DataPowerRest.BEAuditResponse _actualizaPAResponseType;

		public DataPowerRest.BEAuditResponse responseStatus
		{
			set{_actualizaPAResponseType= value;}
			get{ return _actualizaPAResponseType;}
		}
	}
}
