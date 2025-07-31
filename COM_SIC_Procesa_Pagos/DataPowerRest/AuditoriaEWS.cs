using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	[Serializable]
	public class AuditoriaEWS
	{
		private string _USRAPP ;
		private string _IPAPLICACION  ;
		private string _APLICACION  ;
		private string _IDTRANSACCION  ;
		private string _idTransaccionNegocio  ;
		private string _userId  ;
		private string _applicationCodeWS  ;
		private string _nameRegEdit  ;

		public string USRAPP
		{
			set{_USRAPP= value;}
			get{ return _USRAPP;}
		}

		public string IPAPLICACION
		{
			set{_IPAPLICACION= value;}
			get{ return _IPAPLICACION;}
		}

		public string APLICACION
		{
			set{_APLICACION= value;}
			get{ return _APLICACION;}
		}

		public string IDTRANSACCION
		{
			set{_IDTRANSACCION= value;}
			get{ return _IDTRANSACCION;}
		}

		public string idTransaccionNegocio
		{
			set{_idTransaccionNegocio= value;}
			get{ return _idTransaccionNegocio;}
		}

		public string userId
		{
			set{_userId= value;}
			get{ return _userId;}
		}

		public string applicationCodeWS
		{
			set{_applicationCodeWS= value;}
			get{ return _applicationCodeWS;}
		}

		public string nameRegEdit
		{
			set{_nameRegEdit= value;}
			get{ return _nameRegEdit;}
		}

		public AuditoriaEWS()
		{
			
		}
	}
}
