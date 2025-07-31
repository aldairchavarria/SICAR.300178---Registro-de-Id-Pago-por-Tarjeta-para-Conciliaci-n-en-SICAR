using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	
	public class HeaderResponse
	{
		private string _consumer;
		private string _pid;
		private Status _status;
		private string _timestamp;
		private string _varArg;

		public string consumer
		{
			set{_consumer= value;}
			get{ return _consumer;}
		}

		public string pid
		{
			set{_pid= value;}
			get{ return _pid;}
		}

		public Status status
		{
			set{_status= value;}
			get{ return _status;}
		}

		public string timestamp
		{
			set{_timestamp= value;}
			get{ return _timestamp;}
		}

		public string varArg
		{
			set{_varArg= value;}
			get{ return _varArg;}
		}
//INICIATIVA 995 - INI
		public string VarArg
		{
			set{_varArg= value;}
			get{ return _varArg;}
		}
//INICIATIVA 995 - FIN

		public HeaderResponse()
		{
			_status = new Status();
		}
	}
}
