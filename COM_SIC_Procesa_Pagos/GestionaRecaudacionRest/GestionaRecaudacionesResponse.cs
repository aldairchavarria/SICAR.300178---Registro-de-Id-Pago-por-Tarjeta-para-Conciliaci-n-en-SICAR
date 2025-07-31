using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class GestionaRecaudacionesResponse
	{
		private MessageResponseGestionaRecaudacion _MessageResponse;

		public MessageResponseGestionaRecaudacion MessageResponse
		{
			set{_MessageResponse= value;}
			get{ return _MessageResponse;}
		}

		public GestionaRecaudacionesResponse()
		{
			_MessageResponse= new MessageResponseGestionaRecaudacion();
		}
	}
}
