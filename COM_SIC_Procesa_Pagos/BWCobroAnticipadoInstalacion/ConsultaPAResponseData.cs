using System;
using System.Collections;
using System.Text;
using System.Runtime.Serialization;
namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{

	public class ConsultaPAResponseData
	{
		private PagoAnticipado [] _pagoAnticipado;

		public PagoAnticipado [] pagoAnticipado
		{
			set{_pagoAnticipado = value;}
			get{return _pagoAnticipado;}
		}

		public ConsultaPAResponseData()
		{
			_pagoAnticipado	 = new PagoAnticipado[0];
		}
	}
}
