using log4net;
using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Threading;

namespace COM_SIC_Log4Net
{
    public class ExceptionHelper
    {
        public static int LineNumber( Exception e)
        {

            int linenum = 0;
            try
            {
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));
            }


            catch
            {
            
            }
            return linenum;
        }
    }
    
	public class NetLogger
    {
        public static ILog log = LogManager.GetLogger("");
        public static string FormatoExcepciones(Exception ex)
        {
			string _Error;
			string _Excepcion;
			string _Mensaje;
			string _StackTrace;
			if (ex.Source=="" || ex.Source ==null)
			{
				_Error ="Hubo un error no reconocido.";
			}
			else
			{
				_Error =ex.Source.Trim();
			}
			if (ex.GetType().Name=="" || ex.GetType().Name ==null)
			{
				_Excepcion ="Hubo un error sin nombre excepción.";
			}
			else
			{
				_Excepcion =ex.GetType().Name.Trim();
			}
			
			if (ex.Message=="" || ex.Message ==null)
			{
				_Mensaje ="Hubo un error sin mensaje.";
			}
			else
			{
				_Mensaje =ex.Message.Trim();
			}
			if (ex.StackTrace=="" || ex.StackTrace ==null)
			{
				_StackTrace ="Hubo un error sin fuente de error.";
			}
			else
			{
				_StackTrace = ex.StackTrace.Trim().Replace(System.Environment.NewLine, "");
			}
			return string.Format("[Error : {0}] [Excepción : {1}] [Mensaje : {2}] [Fuente : {3}]",				_Error,				_Excepcion,				_Mensaje,            _StackTrace);
        }
        public static string FormatoError(string msj)
        {
                   return "[Mensaje: " + msj + "]";
        }
        public static void EscribirLog(NivelLog nivel, string nombreArchivo, string detalle)
        {
            Globales global = new Globales();
            ELog request = new ELog();
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                request.Mensaje = nombreArchivo;
                request.Detalle = detalle;
                request.IPEquipo = global.IPMaquina;
                request.nombreMaquina = global.NombreMaquina;
                string usuario = "";
                try
                {
                    usuario = (string)log4net.GlobalContext.Properties["matricula"];
                }
                catch (Exception ex)
                {
                    usuario = "";
                    log.Error(ex.ToString());
                }
                request.Usuario = usuario;
				
				string path  = String.Format("{0}{1}{2}{3}{4}{5}","Report_","Log_", "xyz","_",DateTime.Now.ToString("yyyyMMddHH"),".log");
//				NetLogger.ChangeFilePath("ERROR", path);
                switch (nivel)
                {
                    case NivelLog.Aplicacion:
                    //PROY 140126 INI
                        log.Error("[" + nombreArchivo + "] " +   request.Detalle  );
                        break;
                    case NivelLog.Auditoria:
						log.Info("[" + nombreArchivo + "] " +   request.Detalle  );
						break;
                    case NivelLog.Seguridad:
						log.Info("[" + nombreArchivo + "] " +   request.Detalle  );
						break;
                    case NivelLog.SeguridadSinAcceso:
                        log.Info("[" + nombreArchivo + "] " +   request.Detalle  );
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("[IP:" + request.IPEquipo + "] " + FormatoExcepciones(ex));
            }

        }
        
		public enum NivelLog
        {
            Aplicacion = 1,
            Seguridad = 2,
            Auditoria = 3,
            SeguridadSinAcceso = 4
        };
		public static void ChangeFilePath(string appenderName, string newFilename)
		{                       
			log4net.Repository.ILoggerRepository repository = log4net.LogManager.GetRepository();
			foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
			{
				if (appender.Name.CompareTo(appenderName) == 0 && appender is log4net.Appender.FileAppender)
				{
					log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)appender;
					fileAppender.File = System.IO.Path.Combine(fileAppender.File, newFilename);
					fileAppender.ActivateOptions();                   
				}
			}           
		}

	}
}
