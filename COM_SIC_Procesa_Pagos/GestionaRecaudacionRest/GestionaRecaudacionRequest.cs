using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class GestionaRecaudacionRequest
	{
		private MessageRequestGestionaRecaudacion _MessageRequest;


		public MessageRequestGestionaRecaudacion MessageRequest
		{
			set{_MessageRequest= value;}
			get{ return _MessageRequest;}
		}

	

		public GestionaRecaudacionRequest()
		{
			_MessageRequest= new MessageRequestGestionaRecaudacion();
		}
	}
}
