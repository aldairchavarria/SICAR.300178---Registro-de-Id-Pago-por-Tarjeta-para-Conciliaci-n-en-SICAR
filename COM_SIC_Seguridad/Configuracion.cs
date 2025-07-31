using System;
using System.Text;
using Microsoft.Win32;
using System.Configuration;
using System.Web;
using System.Web.Caching;


namespace COM_SIC_Seguridad
{
	/// <summary>
	/// Summary description for Configuracion
	/// </summary>
	public class Configuracion
	{
		private DES  _cryDES;
		private ClsConexion  _objcon;
		//ClsConexion objconexion;
		String rutakey = String.Empty;
		private string _strLlaveNombre;

		public Configuracion(string llaveNombre)
		{
			if (llaveNombre.Equals(""))
			{
				throw new Exception("No se ha especificado la llave del registro.");
			}

			this._strLlaveNombre = llaveNombre;

			if (_cryDES == null)
			{
				rutakey = ConfigurationSettings.AppSettings["RUTAREGEDIT"];
				string strPalabraSecreta = Registry.LocalMachine.OpenSubKey(rutakey).GetValue("key").ToString();
				_cryDES = new DES(strPalabraSecreta);				
			}
		}
		

		#region LeerValor
//		String rutakey = ConfigurationSettings.AppSettings("ORIGEN_BSCS");
		private string LeerValor(string valorNombre)
		{
			return Registry.LocalMachine.OpenSubKey(rutakey+@"\"+ this._strLlaveNombre).GetValue(valorNombre, "").ToString();
		}

		#endregion

		#region LeerValorEncriptado

		private string LeerValorEncriptado(string valorNombre)
		{
			string strPassword = this.LeerValor(valorNombre);
			if(strPassword.Length == 0) return string.Empty;

			byte[] ByteArray = System.Text.Encoding.Default.GetBytes(strPassword);

			_cryDES.DecryptByte(ref ByteArray);

			return System.Text.Encoding.Default.GetString(ByteArray);
		}

		#endregion

		#region LeerProveedor

		public string LeerProveedor()
		{
			return LeerValor("Provider");
		}

		#endregion

		#region LeerBaseDatos

		public string LeerBaseDatos()
		{
			return LeerValor("BD_Activa");
		}

		#endregion

		#region LeerServidor

		public string LeerServidor()
		{
			return LeerValor("Server");
		}

		#endregion

		#region LeerUsuario

		public string LeerUsuario()
		{
			return this.LeerValorEncriptado("User");
		}

public string LeerUsuarioEncriptado()
		{
			string strPassword = this.LeerValor("User");
			if(strPassword.Length == 0)
			{ 
				return string.Empty;
			 }
			else
			{
				return strPassword;
			}
		}

		#endregion

		#region LeerContrasena

		public string LeerContrasena()
		{
			return this.LeerValorEncriptado("Password");
		}

public string LeerContrasenaEncriptado()
		{
			string strPassword = this.LeerValor("Password");
			if(strPassword.Length == 0)
			{ 
				return string.Empty;
			}
			else
			{
				return strPassword;
			}
		}
		#endregion
		
		#region getConexion
		public ClsConexion GetConexion()
		{
				_objcon = new ClsConexion();
				_objcon.GetBD = LeerBaseDatos();
				_objcon.GetUsuario = LeerUsuario();
				_objcon.GetPassword = LeerContrasena();
				_objcon.GetServidor = LeerServidor();
				return _objcon;
		}

		#endregion


	
	}

}
