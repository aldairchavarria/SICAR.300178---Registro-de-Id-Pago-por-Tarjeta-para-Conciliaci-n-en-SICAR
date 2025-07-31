using System;
using System.Configuration;
using System.Collections;
using System.Text;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for DES.
	/// </summary>
	public class DES
	{
		public DES()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static string GetEncryptedBase64(Hashtable table)
		{
			ArrayList responseDecrypt = QueryKeys(table);
			
			if (Convert.ToString(responseDecrypt[0]) == "0")
				return EncryptedBase64(Convert.ToString(responseDecrypt[2]),Convert.ToString(responseDecrypt[3]));
			else
				return Convert.ToString(responseDecrypt[0]);
		}

		private static string EncryptedBase64(string user, string key)
		{
			return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", user, key)));
		}

		public static ArrayList QueryKeys(Hashtable table)
		{

			COM_SIC_Activaciones.ConsultaClavesWS.ebsConsultaClavesService  oConsultaClaves= new COM_SIC_Activaciones.ConsultaClavesWS.ebsConsultaClavesService();
					
			string strCodigoResultado = string.Empty;
			ArrayList listParams;

			try
			{
				oConsultaClaves.Url = ConfigurationSettings.AppSettings["RutaWS_ConsultaClaves"];
				oConsultaClaves.Credentials = System.Net.CredentialCache.DefaultCredentials;
				
				string strIdTransaccion = DateTime.Now.ToString("YYYYMMDDHHMISSMS");
				string strIpAplicacion = string.Empty;
				string strIpTransicion = string.Empty;
				string strUsrAplicacion = string.Empty;
				string strCodigoAplicacion = Convert.ToString(ConfigurationSettings.AppSettings["CodigoAplicacion"]);
				string strIdAplicacion = Convert.ToString(ConfigurationSettings.AppSettings["CONS_APLICACION"]);
				string strUsuarioAplicacionEncriptado = Convert.ToString(ConfigurationSettings.AppSettings["User_ConsultaNacionalidad"]);
				string strClaveEncriptado = Convert.ToString(ConfigurationSettings.AppSettings["Password_ConsultaNacionalidad"]);
				
				string strMensajeResultado = string.Empty;
				string strUsuarioAplicacionDesencriptado = string.Empty;
				string strClaveDesencriptado = string.Empty;
				
				strCodigoResultado = oConsultaClaves.desencriptar(ref strIdTransaccion, strIpAplicacion, strIpTransicion, strUsrAplicacion, strCodigoAplicacion, strIdAplicacion, strUsuarioAplicacionEncriptado, strClaveEncriptado, ref strMensajeResultado, ref strUsuarioAplicacionDesencriptado, ref strClaveDesencriptado);
				listParams = new ArrayList();
				listParams.Add(strCodigoResultado);
				listParams.Add(strMensajeResultado);
				listParams.Add(strUsuarioAplicacionDesencriptado);
				listParams.Add(strClaveDesencriptado);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
			}

			return listParams;
		}

		//PROY-140372 INI
		public static string GetEncryptedBase64_Generic(Hashtable table, CredencialesDPRest objCredencialesDPRest)
		{
			ArrayList responseDecrypt = QueryKeys_Generic(table,objCredencialesDPRest);
			
			if (Convert.ToString(responseDecrypt[0]) == "0")
				return EncryptedBase64(Convert.ToString(responseDecrypt[2]),Convert.ToString(responseDecrypt[3]));
			else
				return Convert.ToString(responseDecrypt[0]);
			
		}

		public static ArrayList QueryKeys_Generic(Hashtable table,CredencialesDPRest objCredencialesDPRest)
		{
			COM_SIC_Activaciones.ConsultaClavesWS.ebsConsultaClavesService  oConsultaClaves= new COM_SIC_Activaciones.ConsultaClavesWS.ebsConsultaClavesService();
					
			string strCodigoResultado = string.Empty;
			ArrayList listParams;

			try
			{
				oConsultaClaves.Url = ConfigurationSettings.AppSettings["strURLConsultaClavesWS"];
				oConsultaClaves.Credentials = System.Net.CredentialCache.DefaultCredentials;
				
				string strIdTransaccion = DateTime.Now.ToString("YYYYMMDDHHMISSMS");
				string strIpAplicacion = string.Empty;
				string strIpTransicion = string.Empty;
				string strUsrAplicacion = string.Empty;
				string strCodigoAplicacion = Convert.ToString(ConfigurationSettings.AppSettings["CodAplicacion"]);
				string strIdAplicacion = Convert.ToString(ConfigurationSettings.AppSettings["CONS_APLICACION"]);
		
				string strUsuarioAplicacionEncriptado = Convert.ToString(objCredencialesDPRest.usuario);
				string strClaveEncriptado = Convert.ToString(objCredencialesDPRest.clave);
				
				string strMensajeResultado = string.Empty;
				string strUsuarioAplicacionDesencriptado = string.Empty;
				string strClaveDesencriptado = string.Empty;
				

				strCodigoResultado = oConsultaClaves.desencriptar(ref strIdTransaccion, strIpAplicacion, strIpTransicion, strUsrAplicacion, strCodigoAplicacion, strIdAplicacion, strUsuarioAplicacionEncriptado, strClaveEncriptado, ref strMensajeResultado, ref strUsuarioAplicacionDesencriptado, ref strClaveDesencriptado);

				listParams = new ArrayList();
				listParams.Add(strCodigoResultado);
				listParams.Add(strMensajeResultado);
				listParams.Add(strUsuarioAplicacionDesencriptado);
				listParams.Add(strClaveDesencriptado);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
			}

			return listParams;
		}

		//PROY-140372 FIN
	}
	//PROY-140332 FIN
}
