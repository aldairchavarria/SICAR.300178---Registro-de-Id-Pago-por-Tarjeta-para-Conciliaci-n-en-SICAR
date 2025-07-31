using System;
using log4net;
using log4net.Config;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for LogError.
	/// </summary>
	public class LogError
	{
		private static readonly ILog logger =
			LogManager.GetLogger(typeof(LogError));

		static LogError()
		{			
			XmlConfigurator.Configure();			
		}

		//static void Main(string[] args)
		//{

		//    BasicConfigurator.Configure();
		//}
	}
}
