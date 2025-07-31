using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class MessageResponseGestionaRecaudacion
	{

		private DataPowerRest.HeadersResponse _header;
		private BodyResponseGestionaRecaudacion _body;

		public DataPowerRest.HeadersResponse Header
		{
			set{_header= value;}
			get{ return _header;}
		}

		public BodyResponseGestionaRecaudacion Body
		{
			set{_body= value;}
			get{ return _body;}
		}

		public MessageResponseGestionaRecaudacion()
		{
			_header = new DataPowerRest.HeadersResponse();
			_body = new BodyResponseGestionaRecaudacion();
		}
	}
}
