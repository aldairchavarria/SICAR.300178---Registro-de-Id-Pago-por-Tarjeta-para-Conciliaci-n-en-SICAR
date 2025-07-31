using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Data;
using System.Configuration;
using COM_SIC_Entidades.DataPowerRest;
using COM_SIC_Activaciones;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;
using COM_SIC_Entidades.DataPowerRest.RestReferences;
using COM_SIC_Entidades.claro_post_programacionRepo.Request;
using COM_SIC_Entidades.claro_post_programacionRepo.Response;
namespace COM_SIC_Procesa_Pagos.ProgramarTarea
{
	/// <summary>
	/// Summary description for ProgramarTarea.
	/// </summary>
	public class ProgramarTarea
	{
		public ProgramarTarea()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		string nameArchivo = "log_ProgramarTareaRepo" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		SICAR_Log objFileLog = new SICAR_Log();
				public MessageResponseProgramarTarea ProgramarTareaRepo(MessageRequestProgramarTarea objRequestProgramarTareaRepo)
				{
					
					objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0}{1}{0}","**************************","INICIO PROY-140846 ProgramarTareaRepo "));
		
					MessageResponseProgramarTarea objRpta = new MessageResponseProgramarTarea();
		
					try
					{
						Hashtable paramHeader = new Hashtable();
		
						string userEncriptado = Funciones.CheckStr(ConfigurationSettings.AppSettings["User_ProgramacionTareaRepo"]);
						string passEncriptado = Funciones.CheckStr(ConfigurationSettings.AppSettings["Pass_ProgramacionTareaRepo"]);
						string idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
		
						string strUsuario = Funciones.CheckStr(objRequestProgramarTareaRepo.MessageRequest.Body.programarTareaRequest.usuarioSistema);
						objRequestProgramarTareaRepo.MessageRequest.Body.programarTareaRequest.idTransaccion = idTransaccion;
		
						paramHeader.Add("idTransaccion", idTransaccion);
						paramHeader.Add("msgId", idTransaccion);
						paramHeader.Add("userId", strUsuario);
						paramHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
						paramHeader.Add("aplicacion", Funciones.CheckStr(ConfigurationSettings.AppSettings["constAplicacion"]));
		
						string[] ParamsXml = objRequestProgramarTareaRepo.MessageRequest.Body.programarTareaRequest.xmlEntrada.Split(',');
		
						if (ParamsXml.Length > 7)
						{
							string sTipoDoc = Funciones.CheckStr(ParamsXml[0]);
							string sNumDoc = Funciones.CheckStr(ParamsXml[1]);
							string sCodigoBloq = Funciones.CheckStr(ParamsXml[2]);
							string sIccidNuevo = Funciones.CheckStr(ParamsXml[3]);
							string sCodMaterial = Funciones.CheckStr(ParamsXml[4]);
							string sOficinaVenta = Funciones.CheckStr(ParamsXml[5]);
							string sClienteSap = Funciones.CheckStr(ParamsXml[6]);
							string sDesPDV = Funciones.CheckStr(ParamsXml[7]);
		
							objRequestProgramarTareaRepo.MessageRequest.Body.programarTareaRequest.xmlEntrada = ArmarEstructuraXMLProgramacionRepo(sTipoDoc,sNumDoc, sCodigoBloq, sIccidNuevo, sCodMaterial, sOficinaVenta, sClienteSap, sDesPDV);
						}
		
		
						object oResponse;
						foreach (object valor in paramHeader.Values)
						{
							objFileLog.Log_WriteLog("", nameArchivo, String.Format("{0} --> {1}", "Value {0}", valor));
						}

							BWCobroAnticipadoInstalacion.CredencialesDPRest objCredencialesDPRest= new BWCobroAnticipadoInstalacion.CredencialesDPRest();
							objCredencialesDPRest.usuario = userEncriptado;
							objCredencialesDPRest.clave = passEncriptado;
							objCredencialesDPRest.urlServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings["consUrlProgramacionTareaRepo"]);
							objCredencialesDPRest.timeOutServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings["Time_Out_ProgramacionTareaRepo"]);
		
							oResponse = BWCobroAnticipadoInstalacion.RestService.PostInvoque_Generic(objCredencialesDPRest,paramHeader,objRequestProgramarTareaRepo, typeof(MessageResponseProgramarTarea));
							objRpta = (MessageResponseProgramarTarea)oResponse;
						}
					catch (Exception ex)
					{
						objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ProgramarTareaRepo", Funciones.CheckStr(ex.Message)));
						objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ProgramarTareaRepo", Funciones.CheckStr(ex.StackTrace)));
					}
		
					objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0}{1}{0}","**************************","FIN ProgramarTareaRepo"));
					return objRpta;
				}
		private string ArmarEstructuraXMLProgramacionRepo(string sTipoDoc, string sNumDoc, string sCodBloqueo, string sNuevoIccid, string sCodMaterial, string sOficVenta, string sClienteSap,string sDesPdv)
		{
			StringBuilder strBldTransaccion = new StringBuilder();
			ArrayList objListaServicios = new ArrayList();
			objFileLog.Log_WriteLog("", nameArchivo, string.Format("{0}{1}", "[INICIO][PROY-140846 - IDEA 143176][ArmarEstructuraXMLProgramacionRepo]", string.Empty));
			try
			{
				strBldTransaccion.AppendFormat("{0}", @"<?xml version='1.0' encoding='UTF-8'?>");
				strBldTransaccion.AppendFormat("{0}", "<RepoChipRequest>");//CAMBIAR POR XML PENDIENTE
				strBldTransaccion.AppendFormat("{0}", ("<tipoDocumento>" + Funciones.CheckStr(sTipoDoc) + "</tipoDocumento>"));
				strBldTransaccion.AppendFormat("{0}", ("<numeroDocumento>" + Funciones.CheckStr(sNumDoc) + "</numeroDocumento>"));
				strBldTransaccion.AppendFormat("{0}", ("<codigoBloqueo>" + Funciones.CheckStr(sCodBloqueo) + "</codigoBloqueo>"));
				strBldTransaccion.AppendFormat("{0}", ("<iccidNuevo>" + Funciones.CheckStr(sNuevoIccid) + "</iccidNuevo>"));
				strBldTransaccion.AppendFormat("{0}", ("<codigoMaterial>" + Funciones.CheckStr(sCodMaterial) + "</codigoMaterial>"));
				strBldTransaccion.AppendFormat("{0}", ("<oficinaVenta>" + Funciones.CheckStr(sOficVenta) + "</oficinaVenta>"));
				strBldTransaccion.AppendFormat("{0}", ("<clienteSap>" + Funciones.CheckStr(sClienteSap) + "</clienteSap>"));
				strBldTransaccion.AppendFormat("{0}", ("<pdv>" + Funciones.CheckStr(sDesPdv) + "</pdv>"));
				//strBldTransaccion.AppendFormat("{0}", ("<fechaProgramacion>" + Funciones.CheckStr(strFechaProgramacion) + "</fechaProgramacion>\n"));//VALIDAR SI SE QUITA
				strBldTransaccion.AppendFormat("{0}", "</RepoChipRequest>");//CAMBIAR POR XML PENDIENTE
				objFileLog.Log_WriteLog("", nameArchivo, string.Format("{0}{1}", "[PROY-140846 - IDEA 143176][ArmarEstructuraXMLProgramacionRepo]", "OK"));
				//strBldTransaccion.AppendFormat("{0}", "]]>");
				objFileLog.Log_WriteLog("", nameArchivo, string.Format("{0}{1}", "[PROY-140846 - IDEA 143176][ArmarEstructuraXMLProgramacionRepo][strBldTransaccion][Resultado XML]", Funciones.CheckStr(strBldTransaccion)));
			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog("", nameArchivo, "[IDEA 143176][ArmarEstructuraXMLProgramacionRepo][Ocurrio un error al armar XML");
				objFileLog.Log_WriteLog("", nameArchivo, string.Format("{0} {1} | {2}", "[PROY-140846 - IDEA 143176][ArmarEstructuraXMLProgramacionRepo][ERROR]", Funciones.CheckStr(ex.Message), Funciones.CheckStr(ex.StackTrace)));
			}
			objFileLog.Log_WriteLog("", nameArchivo, string.Format("{0}{1}", "[FIN][PROY-140846 - IDEA 143176][ArmarEstructuraXMLProgramacionRepo]", string.Empty));

			return Funciones.CheckStr(strBldTransaccion.ToString());
		}
	}
}
