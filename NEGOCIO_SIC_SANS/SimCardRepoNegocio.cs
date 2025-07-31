using System;
using System.Configuration;
using System.Data;
using System.Collections;

namespace NEGOCIO_SIC_SANS
{
	/// <summary>
	/// Descripción breve de SimCardRepoNegocio.
	/// </summary>
	public class SimCardRepoNegocio
	{
		//audit
		SimCardsWS.ebsSimcards simCard = new NEGOCIO_SIC_SANS.SimCardsWS.ebsSimcards();
		string idAplicacion = ConfigurationSettings.AppSettings["CodAplicacion"].ToString();
		string nombreAplicacion = ConfigurationSettings.AppSettings["constAplicacion"].ToString();	
		string nameFile = ConfigurationSettings.AppSettings["constNameLogWS_Sans"].ToString() + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		string pathFile = ConfigurationSettings.AppSettings["constRutaLogWS_Sans"].ToString();

		public SimCardRepoNegocio()
		{
			simCard.Url = ConfigurationSettings.AppSettings["consRutaWS_Sans"].ToString();
			simCard.Credentials = System.Net.CredentialCache.DefaultCredentials;
			simCard.Timeout = int.Parse(ConfigurationSettings.AppSettings["consRutaWS_Sans_Timeout"].ToString());
		}

		public string ActualizarSimCardxReposicion(string NroTelefono, string NroSerie,	string desProducto,	string usuario)
		{
			string strResultado = string.Empty;

			//Log
			COM_SIC_Recaudacion.SICAR_Log objLog = new COM_SIC_Recaudacion.SICAR_Log();
			string strArchivo = objLog.Log_CrearNombreArchivo(nameFile);

			try
			{
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "------------ " + DateTime.Now.ToShortTimeString() + " | Metodo ActualizarSimCardxReposicion: Inicia Invocacion WS. actualizarSimcardPorReposicion ------------");
				
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Parametros del metodo");
				objLog.Log_WriteLog(pathFile, strArchivo, "- NroTelefono: " + NroTelefono);
				objLog.Log_WriteLog(pathFile, strArchivo, "- NroSerie: " + NroSerie);
				objLog.Log_WriteLog(pathFile, strArchivo, "- DescripProducto: " + desProducto);
				objLog.Log_WriteLog(pathFile, strArchivo, "- Usuario: " + usuario);

				//Invocar WS
                string idTransaccion = string.Empty;
				string mensajeResultado = string.Empty;
				SimCardsWS.itReturnType[] itReturn;

				strResultado = simCard.actualizarSimcardPorReposicion(ref idTransaccion, idAplicacion, nombreAplicacion, usuario,
												NroTelefono, NroSerie, desProducto, usuario, out mensajeResultado, out itReturn);

				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Datos obtenidos del WS");
				objLog.Log_WriteLog(pathFile, strArchivo, "- resultados del WS: " + strResultado);

				//Valida resultado
				if(strResultado == "0")
				{
					objLog.Log_WriteLog(pathFile, strArchivo, "itReturn Tipo: " + itReturn[0].tipo);
					objLog.Log_WriteLog(pathFile, strArchivo, "itReturn Mensaje: " + itReturn[0].mensaje);
					objLog.Log_WriteLog(pathFile, strArchivo, "itReturn Lineas: " + itReturn[0].lineas);
				}
				else
					objLog.Log_WriteLog(pathFile, strArchivo, "- Mensaje resultado: " + mensajeResultado);

			}
			catch(Exception ex)
			{
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. actualizarSimcardPorReposicion: " + ex.Message);
				objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
				objLog.Log_WriteLog(pathFile, strArchivo, "Error WS. actualizarSimcardPorReposicion: " + ex.StackTrace);
			}
			objLog.Log_WriteLog(pathFile, strArchivo, System.Environment.NewLine);
			objLog.Log_WriteLog(pathFile, strArchivo, "------------ Metodo ActualizarSimCardxReposicion: Finaliza Invocacion WS. actualizarSimcardPorReposicion ------------");

			return strResultado;
		}
	}
}
