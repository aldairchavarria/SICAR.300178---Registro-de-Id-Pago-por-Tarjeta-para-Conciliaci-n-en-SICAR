using System;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for ÁctualizaPARequestType.
	/// </summary>
	public class ActualizaPARequestType
	{
		public ActualizaPARequestType()
		{
			//
			// TODO: Add constructor logic here
			//

		}

		
		private string _numeroSolicitud;  //PBI000002148250
		private Int64 _numeroContrato;
		private string _estado;
		private string _usuarioActualizacion;
		private string _montoInicialModificado;
		private string _transaccionPago;
		

		public string numeroSolicitud  //PBI000002148250
		{
			set{_numeroSolicitud = value;}
			get{return _numeroSolicitud;}
		}

		public Int64 numeroContrato
		{
			set{_numeroContrato = value;}
			get{return _numeroContrato;}
		}

		public string estado
		{
			set{_estado = value;}
			get{return _estado;}
		}

		public string usuarioActualizacion
		{
			set{_usuarioActualizacion = value;}
			get{return _usuarioActualizacion;}
		}

		public string montoInicialModificado
		{
			set{_montoInicialModificado = value;}
			get{return _montoInicialModificado;}
		}

		public string transaccionPago
		{
			set{_transaccionPago = value;}
			get{return _transaccionPago;}
		}

	}
}