using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica
{
	/// <summary>
	/// Summary description for OnlineGenerationRequest.
	/// </summary>
	public class OnlineGenerationRequest
	{
		public OnlineGenerationRequest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string _ruc;
		private string _login;
		private string _clave;
		private string _docTxt;
		private string _tipoFoliacion;
		private string _tipoRetorno;

		public string ruc
		{
			set{_ruc= value;}
			get{ return _ruc;}
		}
		public string login
		{
			set{_login= value;}
			get{ return _login;}
		}
		public string clave
		{
			set{_clave= value;}
			get{ return _clave;}
		}
		public string docTxt
		{
			set{_docTxt= value;}
			get{ return _docTxt;}
		}
		public string tipoFoliacion
		{
			set{_tipoFoliacion= value;}
			get{ return _tipoFoliacion;}
		}
		public string tipoRetorno
		{
			set{_tipoRetorno= value;}
			get{ return _tipoRetorno;}
		}
	}
}
