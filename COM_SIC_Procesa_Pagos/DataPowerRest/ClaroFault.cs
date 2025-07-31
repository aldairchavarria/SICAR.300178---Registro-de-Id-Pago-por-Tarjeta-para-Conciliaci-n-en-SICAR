using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	//INICIATIVA 712 Cobro Anticipado
	/// <summary>
	/// Summary description for ClaroFault.
	/// </summary>
	public class ClaroFault
	{
		public ClaroFault()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string _idAudit;
		private string _codeError;
		private string _descriptionError;
		private string _locationError;
		private string _date;
		private string _originError;

		public string idAudit
		{
			set{_idAudit= value;}
			get{ return _idAudit;}
		}

		public string codeError
		{
			set{_codeError= value;}
			get{ return _codeError;}
		}

		public string descriptionError
		{
			set{_descriptionError= value;}
			get{ return _descriptionError;}
		}

		public string locationError
		{
			set{_locationError= value;}
			get{ return _locationError;}
		}

		public string date
		{
			set{_date= value;}
			get{ return _date;}
		}

		public string originError
		{
			set{_originError= value;}
			get{ return _originError;}
		}
		
	}
}
