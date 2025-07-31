using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	
	public class HeaderRequest
	{

		private string _consumer; 
		private string _country; 
		private string _dispositivo; 
		private string _language; 
		private string _modulo; 
		private string _msgType; 
		private string _operation; 
		private string _pid; 
		private string _system; 
		private string _timestamp; 
		private string _userId; 
		private string _wsIp; 
		
		public string consumer
		{
			set{_consumer= value;}
			get{ return _consumer;}
		}

		public string country
		{
			set{_country= value;}
			get{ return _country;}
		}

		public string dispositivo
		{
			set{_dispositivo= value;}
			get{ return _dispositivo;}
		}

		public string language
		{
			set{_language= value;}
			get{ return _language;}
		}


		public string modulo
		{
			set{_modulo= value;}
			get{ return _modulo;}
		}

		public string msgType
		{
			set{_msgType= value;}
			get{ return _msgType;}
		}
		
		public string operation
		{
			set{_operation= value;}
			get{ return _operation;}
		}
		
		public string pid
		{
			set{_pid= value;}
			get{ return _pid;}
		}

		public string system
		{
			set{_system= value;}
			get{ return _system;}
		}

		public string timestamp
		{
			set{_timestamp= value;}
			get{ return _timestamp;}
		}

		public string userId
		{
			set{_userId= value;}
			get{ return _userId;}
		}

		public string wsIp
		{
			set{_wsIp= value;}
			get{ return _wsIp;}
		}

		public HeaderRequest()
		{
			
		}
	}
}
