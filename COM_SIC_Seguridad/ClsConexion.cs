using System;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace COM_SIC_Seguridad 
{
	/// <summary>
	/// Summary description for ClsConexion.
	/// </summary>
	public class ClsConexion 
	{

		private String nombreBaseDatos;
		private String usuario;
		private String password;
		private String servidor;

		
			public string  GetBD 
			{
				get{return this.nombreBaseDatos;}
				set{nombreBaseDatos =value;}

			}
			public string  GetUsuario
			{
				get{return this.usuario;}
				set{usuario =value;}
				
			}
			public string  GetPassword
			{
				get{return this.password;}
				set{password =value;}
			
			}
			public string  GetServidor
			{
				get{return this.servidor;}
				set{servidor =value;}
	
			}

			public ClsConexion()
			{
				//
				
				// TODO: Add constructor logic here
				//
			}
	}
}
