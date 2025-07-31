using System;
using System.Configuration;
using System.Collections;
using System.Text;
using COM_SIC_Activaciones;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	public class DES
	{
		public DES()
		{
			
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
				oConsultaClaves.Url = ConfigurationSettings.AppSettings["strURLConsultaClavesWS"];
				oConsultaClaves.Credentials = System.Net.CredentialCache.DefaultCredentials;
				
				string strIdTransaccion = Funciones.CheckStr(table["idTransaccion"].ToString());
				string strIpAplicacion = Funciones.CheckStr(table["ipAplicacion"].ToString());
				string strIpTransicion = Funciones.CheckStr(table["idTransaccionNegocio"].ToString());
				string strUsrAplicacion = Funciones.CheckStr(table["usuarioAplicacion"].ToString());
				string strCodigoAplicacion = Funciones.CheckStr(table["applicationCodeWS"].ToString());
				string strIdAplicacion = Funciones.CheckStr(table["applicationCode"].ToString());
				string strUsuarioAplicacionEncriptado = Funciones.CheckStr(Convert.ToString(ConfigurationSettings.AppSettings["strUserEncriptado"])); 
				string strClaveEncriptado = Funciones.CheckStr(Convert.ToString(ConfigurationSettings.AppSettings["strPassEncriptado"]));
				string strMensajeResultado = string.Empty;
				string strUsuarioAplicacionDesencriptado = string.Empty;
				string strClaveDesencriptado = string.Empty;
				
				strCodigoResultado = oConsultaClaves.desencriptar(ref strIdTransaccion, strIpAplicacion, strIpTransicion, strUsrAplicacion, strCodigoAplicacion, strIdAplicacion, strUsuarioAplicacionEncriptado, strClaveEncriptado,ref strMensajeResultado,ref strUsuarioAplicacionDesencriptado,ref  strClaveDesencriptado);
				
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

	}
}
