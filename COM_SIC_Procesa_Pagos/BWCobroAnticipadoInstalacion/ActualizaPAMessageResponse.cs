using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ActualizaPAMessageResponse.
	/// </summary>
	public class ActualizaPAMessageResponse
	{
		public ActualizaPAMessageResponse()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private DataPowerRest.HeadersResponse _Header;
		private ActualizaPABodyResponse _Body;

		public DataPowerRest.HeadersResponse Header
		{
			set{_Header= value;}
			get{ return _Header;}
		}

		public ActualizaPABodyResponse Body
		{
			set{_Body= value;}
			get{ return _Body;}
		}
	}
}
