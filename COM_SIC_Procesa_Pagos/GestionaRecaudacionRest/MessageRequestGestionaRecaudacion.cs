using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class MessageRequestGestionaRecaudacion
	{
		private DataPowerRest.HeadersRequest _header;
		private BodyRequestGestionaRecaudacion _body;

		public DataPowerRest.HeadersRequest Header
		{
			set{_header= value;}
			get{ return _header;}
		}

		public BodyRequestGestionaRecaudacion Body
		{
			set{_body= value;}
			get{ return _body;}
		}

		public MessageRequestGestionaRecaudacion()
		{
			_header = new DataPowerRest.HeadersRequest();
			_body= new BodyRequestGestionaRecaudacion();
		}
	}
}
