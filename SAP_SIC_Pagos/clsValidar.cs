using System;
using System.Data;

namespace SAP_SIC_Pagos
{
	/// <summary>
	/// Descripción breve de clsValidar.
	/// </summary>
	public class clsValidar
	{
		public clsValidar()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

		private SAP_SIC_Validacion ConectaSAP()
		{

			Seguridad_NET.clsSeguridad objSeg = new Seguridad_NET.clsSeguridad();
				
			string strCliente= objSeg.BaseDatos;
			string strUsuario = objSeg.Usuario;
			string strPassword = objSeg.Password;
			string strLanguage= objSeg.Idioma;
			string strApplicationServer = objSeg.Servidor;
			string strSistema = objSeg.Sistema;
			string strLoadBal = objSeg.LoadBal;
			string strMessServ = objSeg.MessServ;
			string strR3Name = objSeg.R3Name;
			string strGroup = objSeg.Group;

			SAP_SIC_Validacion  proxy;

			if (strLoadBal == "0")
			{
				proxy = new SAP_SIC_Validacion("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" ASHOST=" + strApplicationServer + " SYSNR=" + strSistema);
			}
			else
			{
				proxy = new SAP_SIC_Validacion("CLIENT=" + strCliente + " USER=" + strUsuario + " PASSWD=" + strPassword + " LANG=" + strLanguage +" MSHOST=" + strMessServ + " R3NAME=" + strR3Name + " GROUP=" + strGroup);
			}

			return proxy;
		}

	
		public void Get_ConsultaEstadoCaja(string Oficina, string Fecha, string Usuario,ref string Resultado,ref string msgError, ref decimal cajaBuzon, ref string cierreRealizado, ref decimal saldoInicial)
		{
			try
			{
				string strFecha, strUsuario;

				Resultado = "0";
				msgError = string.Empty;
				BAPIRET2Table objReturn = new BAPIRET2Table();
				SAP_SIC_Validacion proxy = ConectaSAP();
				strFecha = Fecha.Substring(6,4) + "/" + Fecha.Substring(3,2) + "/" + Fecha.Substring(0,2);
				
				if (Usuario.Length == 6)
				{
					strUsuario = Usuario.Substring(1,5);
				}
				else
				{
					strUsuario = Usuario;
				}

				proxy.Zpvu_Rfc_Con_Cajero_Caja_Cerra(strFecha, Oficina, strUsuario,out cajaBuzon,out cierreRealizado,out saldoInicial,ref objReturn);
				proxy.Connection.Close();
			}
			catch(Exception ex)
			{	
				msgError = ex.Message.ToString();
				Resultado = "-1";
			}
				
		}
	}
}
