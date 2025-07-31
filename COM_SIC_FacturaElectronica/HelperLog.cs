using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;
using COM_SIC_Log4Net; //PROY-140126
namespace COM_SIC_FacturaElectronica
{
	/// <summary>
	/// Summary description for HelperLog.
	/// </summary>
	public class HelperLog
	{
		public HelperLog(){}
		public static void EscribirLog(string vRuta,string vNombreFila,string vMsg,bool vSobreScribir)
		{
			
			string strFecha = DateTime.Now.ToString("yyyy-MM-dd");

			if (vNombreFila.IndexOf(DateTime.Now.ToString("yyyy-MM-dd"))==-1)
			{
				vNombreFila= string.Format("{0}_{1}.{2}",vNombreFila,strFecha,"txt");                                
			}
			else if (vNombreFila.IndexOf(DateTime.Now.ToString("dd-MM-yyyy"))==-1)
			{
				vNombreFila= string.Format("{0}_{1}.{2}",vNombreFila,strFecha,"txt");
			}
			else if (vNombreFila.IndexOf(DateTime.Now.ToString("yyyyMMdd"))==-1)
			{
				vNombreFila= string.Format("{0}_{1}.{2}",vNombreFila,strFecha,"txt");
			}

			//			bool bLoggingEnabled =	false;
			//			bLoggingEnabled = CheckLoggingEnabled();
			//			if( bLoggingEnabled == false) return;

			if (vRuta == "")
			{
				vRuta =  GetPathLog();
			}
			vRuta +="//" + vNombreFila;	
			try
			{				
				NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, vNombreFila.Trim(), vMsg);
			}
			catch(Exception ex)
			{
				NetLogger.EscribirLog(NetLogger.NivelLog.Aplicacion, vNombreFila.Trim(), ex.Message);
				throw ex;
			}
		}
		private static bool CheckLoggingEnabled()
		{
			string strLoggingStatusConfig = string.Empty;
	
			strLoggingStatusConfig = GetLoggingStatusConfigFileName();
			if (strLoggingStatusConfig.Equals(string.Empty))
				return false;
			//If it's empty then enable the logging status 
			if (strLoggingStatusConfig.Equals(string.Empty))
			{
				return  true;
			}
			else
			{
			
				//Read the value from xml and set the logging status
				bool bTemp = false;	
				string  loggingEnabled = getValue("LoggingEnabled");
				if ( loggingEnabled.Equals("0") )
				{
					bTemp = false;
				}
				else if ( loggingEnabled.Equals("1") )
				{
					bTemp = true;
				}
				return bTemp;
			}
		}
		private static string GetLoggingStatusConfigFileName()
		{
			string strCheckinBaseDirecotry	= AppDomain.CurrentDomain.BaseDirectory + "Sicar.config";

			if (File.Exists(strCheckinBaseDirecotry))
				return strCheckinBaseDirecotry;
			else
			{
				string strCheckinApplicationDirecotry =	GetApplicationPath() + "Sicar.config";
				if (File.Exists(strCheckinApplicationDirecotry))
					return strCheckinApplicationDirecotry;
				else
					return "";
			}
		}
		private static string GetPathLog()
		{
			string strBaseDirectory	= GetApplicationPath();
			try
			{
				// creamos la carpeta log
				string carpeta = ConfigurationSettings.AppSettings["strLogConectaSICAR"];//"log";//"AppLog";
				string ruta = strBaseDirectory + carpeta;
				if (Directory.Exists(carpeta)==false )
				{
					Directory.CreateDirectory(carpeta);
				}
				return carpeta; 

			}
			catch(Exception)
			{
				return strBaseDirectory	;
			}
		}
		private static string GetApplicationPath()
		{
			try
			{
				string strBaseDirectory	= AppDomain.CurrentDomain.BaseDirectory.ToString();
				int nFirstSlashPos		= strBaseDirectory.LastIndexOf("\\");
				string strTemp			= string.Empty ;

				if (0 < nFirstSlashPos)
					strTemp			= strBaseDirectory.Substring(0,nFirstSlashPos);

				int nSecondSlashPos		= strTemp.LastIndexOf("\\");
				string strTempAppPath	= string.Empty;
				if (0 < nSecondSlashPos)
					strTempAppPath	= strTemp.Substring(0,nSecondSlashPos);

				string strAppPath = strTempAppPath.Replace("bin","");
				if (strAppPath == "" )
					strAppPath = strBaseDirectory;
				return strAppPath;
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}
		public static string getValue(string vClave)
		{
			string strXmlPath = GetLoggingStatusConfigFileName();
			if (strXmlPath == "" ){return "";}
			try
			{
				//Open a FileStream on the Xml file
				FileStream docIn = new FileStream(strXmlPath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
				XmlDocument contactDoc = new XmlDocument();
				//Load the Xml Document
				contactDoc.Load(docIn);
				//Get a node
				XmlNodeList UserList = contactDoc.GetElementsByTagName(vClave);
				//get the value
				string strGetValue= UserList.Item(0).InnerText.ToString(); 
				return strGetValue;
			}
			catch(Exception ex)
			{	
				string msg = ex.Message;
				return "";
			}
		}

		public static void CrearArchivolog(string pagina,string descevento, string valoresentrada, string valoressalida,string error ) 
		{

			DirectoryInfo DIR = new DirectoryInfo(ConfigurationSettings.AppSettings["strDirectorioLogSISACT"].ToString().Trim());

			if (!DIR.Exists)
			{
				DIR.Create();
			} 
          
			StringBuilder sbFile = new StringBuilder();
			StreamWriter swWriter;
			string fileName;
			string emailUser = string.Empty;

			//Definición del nombre del fichero, ruta y apertura
			sbFile.Append(ConfigurationSettings.AppSettings["strDirectorioLogSISACT"].ToString().Trim());
			sbFile.Append(@"LogProcesos" + System.DateTime.Now.ToString("dd-MM-yyyy"));
			sbFile.Append(".log");
			fileName = sbFile.ToString();
			swWriter = File.AppendText(fileName);
           
           
			//Escritura dentro del log
			if (!error.Equals(""))
			{
				swWriter.WriteLine("~~~~~~~~~~~~~~~~ ERROR ~~~~~~~~~~~~~~~~");
			}
			else 
			{
				swWriter.WriteLine("~~~~~~~~~~~~~~~~ LOG EVENTOS ~~~~~~~~~~~~~~~~");
			}
           
			swWriter.WriteLine("    Fecha: " + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
			swWriter.WriteLine("    Pagina: " + pagina);
			swWriter.WriteLine("    Desc. Evento: " + descevento);
			swWriter.WriteLine("    Valores de Entrada: " + valoresentrada);
			swWriter.WriteLine("    Valores de Salida: " + valoressalida);
			swWriter.WriteLine("    Desc. Error: " + error);
			swWriter.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			swWriter.WriteLine("");

			//Cerramos los objetos
			swWriter.Close();

		}


	}
}