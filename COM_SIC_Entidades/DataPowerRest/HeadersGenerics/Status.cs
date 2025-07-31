//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for Status.
	/// </summary>
	[Serializable]
	public class Status
	{
		private string _type;
		private string _code;
		private string _message;
		private string _msgid;

		public string type
		{
			set{_type= value;}
			get{ return _type;}
		}
		public string code
		{
			set{_code= value;}
			get{ return _code;}
		}
		public string message
		{
			set{_message= value;}
			get{ return _message;}
		}
		public string msgid
		{
			set{_msgid= value;}
			get{ return _msgid;}
		}

		public Status()
		{
		}
	}
	//FIN PROY-140332 (Try & Buy)
}
