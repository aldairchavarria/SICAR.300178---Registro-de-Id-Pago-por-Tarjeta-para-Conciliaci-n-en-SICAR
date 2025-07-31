using System;
using System.Text;
using System.Runtime.Serialization;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPARequest
	{
		private string _tipoConsulta;
		private Int64 _numeroSolicitud;
		private string _numeroDocumento;
		private string _estado;

		public string tipoConsulta
		{
			set{_tipoConsulta = value;}
			get{return _tipoConsulta;}
		}

		
		public Int64 numeroSolicitud
		{
			set{_numeroSolicitud = value;}
			get{return _numeroSolicitud;}
		}

		
		public string numeroDocumento
		{
			set{_numeroDocumento = value;}
			get{return _numeroDocumento;}
		}

		public string estado
		{
			set{_estado = value;}
			get{return _estado;}
		}
	}
}
