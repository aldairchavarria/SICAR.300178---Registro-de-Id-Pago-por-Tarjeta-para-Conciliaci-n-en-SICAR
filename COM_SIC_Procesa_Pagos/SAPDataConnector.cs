using System;
using System.Configuration;
using System.Reflection;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for SAPDataConnector.
	/// </summary>
	public class SAPDataConnector
	{
		static SAPDataConnector()
		{

		}
		private static string strConex;
		public static string ConnectionString
		{
			get
			{
				Type clsSeguridad = Type.GetTypeFromProgID("Seguridad_Test_UTL.clsSeguridad");
				if (clsSeguridad == null) throw new NullReferenceException("Seguridad_Test_UTL.clsSeguridad no registrado");
				Object clsSeguridadInstance = Activator.CreateInstance(clsSeguridad);
				string strAplicacion =System.Configuration.ConfigurationSettings.AppSettings["KEYSAP"].ToString();
				string strUsuario = (string)clsSeguridad.InvokeMember("Usuario", BindingFlags.InvokeMethod, null, clsSeguridadInstance, new object[] { strAplicacion });
				string strPassword = (string)clsSeguridad.InvokeMember("Password", BindingFlags.InvokeMethod, null, clsSeguridadInstance, new object[] { strAplicacion });
				string strCliente = "200";
				string strLanguage = ConfigurationSettings.AppSettings["IDIOMA_SAP"].ToString();
				string strApplicationServer = ConfigurationSettings.AppSettings["SERVIDOR_SAP"].ToString();
				string strSistema = ConfigurationSettings.AppSettings["SISTEMA_SAP"].ToString();

//				string strUsuario = "T11498";
//				string strPassword = "123456";
//				string strCliente = "200";
//				string strLanguage = "ES";
//				string strApplicationServer = "172.19.43.34";
//				string strSistema = "00";

				strConex = string.Format("CLIENT={0} USER={1} PASSWD={2} LANG={3} ASHOST={4} SYSNR={5}", strCliente, strUsuario, strPassword, strLanguage, strApplicationServer, strSistema);
				return strConex;
			}
		}

		public static string FormatoFecha(string fechaToTransform)
		{
			if (fechaToTransform.Length > 0)
				return fechaToTransform.Substring(6, 4) + "/" + fechaToTransform.Substring(3, 2) + "/" + fechaToTransform.Substring(0, 2);
			else
				return "0000/00/00";
		}
	}



}
