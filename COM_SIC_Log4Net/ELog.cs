using System;
using System.Collections;
using System.Text;
using System.Threading;
namespace COM_SIC_Log4Net
{
	/// <summary>
	/// Summary description for ELog.
	/// </summary>
	public class ELog
	{
		public ELog()
		{
		}
		private string _Mensaje;
		private string _Detalle;
		private string _Usuario;
		private string _IPMaquina;
		private string _nombreMaquina;
		public string Mensaje {    
			get { return _Mensaje; }
			set { _Mensaje = value; } 
		}
		public string Detalle {     
			get { return _Detalle; }
			set { _Detalle = value; } 
		}
		public string Usuario {     
			get { return _Usuario; }
			set { _Usuario = value; } 
		}
		public string IPEquipo {     
			get { return _IPMaquina; }
			set { _IPMaquina = value; } 
		}
		public string nombreMaquina {     
			get { return _nombreMaquina; }
			set { _nombreMaquina = value; } 
		}
	}
}
