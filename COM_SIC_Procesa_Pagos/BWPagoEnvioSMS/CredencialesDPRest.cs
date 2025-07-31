using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{
	/// <summary>
	/// Summary description for CredencialesDPRest.
	/// </summary>
	//INICIATIVA 995-INI

	[Serializable]
	public class CredencialesDPRest
	{
		public CredencialesDPRest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string _usuario;
		private string _clave;
		private string _timeOutServicio;
		private string _urlServicio;
   
		public string usuario{set{ _usuario = value;}
			get{ return _usuario;}
		}

		public string clave{set{ _clave = value;}
			get{ return _clave;}
		}

		public string timeOutServicio{set{ _timeOutServicio = value;}
			get{ return _timeOutServicio;}
		}

		public string urlServicio
		{
			set{_urlServicio= value;}
			get{return _urlServicio;}
		}

	}
}
