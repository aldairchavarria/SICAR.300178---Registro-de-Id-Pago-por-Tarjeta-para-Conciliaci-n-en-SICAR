using SAP_SIC_Recaudacion;
using SAP.Connector;
using System;
using System.Data;
using SAP_SIC_Cajas;

namespace COM_SIC_Procesa_Pagos
{
	/// <summary>
	/// Summary description for AsignacionCajas.
	/// </summary>
	public class AsignacionCajas
	{
		private int EJECUCION_OK = 0;
		private int EJECUCION_ERROR = 8;

		/// <summary>
		/// No modificar el valor, es una constante para la validacion del ambiente que se ejecuta la aplicacion es de desarrollo
		/// </summary>
		private readonly string TEST_MODE;
		
		/// <summary>
		/// El valor para mostrar la informacion de inputs en el log es TEST
		/// en el ambiente de produccion debe ser PROD
		/// </summary>
		 private string LogMode;

		public AsignacionCajas()
		{
			LogMode = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["LogMode"]);
			TEST_MODE ="TEST";
		}


		public bool RegistrarTransaccionSAP(string fechaActual,string oficina, ref string mensaje)
		{
			int cont=0;
			int contReg=0;
			string descripcionTrs="";
			string deserror="";
			string descripcion,codigo, fecha, usuario,nombretabla="";

			Common.UsuarioCaja = mensaje;
			mensaje = "";

			Common.WriteLog("---------------------- INICIANDO ENVIO DE TRANSACCIONES DE CUADRE DE CAJAS ----------------------");

			Common.WriteLog("---- INICIANDO TRANSACCION DE ASIGNACION DE CAJAS ----");

			Common.WriteLog("Importando registros desde la BD: PCK_SICAR_OFF_SAP.MIG_TRS_CAJADIARIO…");
			DataTable tbNoMigradas = listarCajaDiarioNoMigrados(fechaActual,oficina);
			Common.WriteLog("Importacion completada");
			tbNoMigradas.Columns.Add("MigracionOK", typeof(bool));

			foreach (DataRow filaPago in tbNoMigradas.Rows)
			{
				SAP_SIC_Cajas.BAPIRET2Table logSapTable = new SAP_SIC_Cajas.BAPIRET2Table();
				String mensajeOperacion = String.Empty;

				SAP_SIC_Cajas.SAP_SIC_Cajas objCajas = new SAP_SIC_Cajas.SAP_SIC_Cajas(SAPDataConnector.ConnectionString);
				string numeroTransaccion = "";

				try
				{
					Common.WriteLog("------ ENVIANDO TRANSACCION DE CAJAS ------");

					string Fecha = FormatoFecha( filaPago["FECHA"].ToString());
					string Oficina= filaPago["OFICINA"].ToString();
					string Caja = filaPago["CAJA"].ToString();
					string Usuario = filaPago["USUARIO"].ToString();


					if(this.LogMode==TEST_MODE)
					{
						Common.WriteLog("Inicio del Método AsignacionCajas.RegistrarTransaccionSAP.Zpvu_Rfc_Trs_Cajero_Diario:Claro.SiscajasBatch.Data");
						Common.WriteLog("Fecha:{0}", Fecha);
						Common.WriteLog("Oficina:{0}", Oficina);
						Common.WriteLog("Caja:{0}", Caja);
						Common.WriteLog("Usuario:{0}",Usuario);
					}


					
					//CONTADOR REGISTROS LEIDOS
					cont++;
					Common.WriteLog("Enviando informacion a SAP: RFC Zpvu_Rfc_Trs_Cajero_Diario...");
					objCajas.Zpvu_Rfc_Trs_Cajero_Diario(Caja,Fecha,Usuario,Oficina,ref logSapTable);
					Common.WriteLog("Envio de informacion a SAP: RFC Zpvu_Rfc_Trs_Cajero_Diario terminada");

					DataTable tbLogResult = logSapTable.ToADODataTable();
					int numeroResultadoOperacion = -1;
					foreach (DataRow item in tbLogResult.Rows)
					{
						numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
						mensajeOperacion = String.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
						descripcionTrs =item[3].ToString();
					}

					//					string str = "";

					if (numeroResultadoOperacion == EJECUCION_OK)
					{
						contReg++;
						string RegistraZAP = "S";
						this.registrarResultadoOperacion(Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]), RegistraZAP, mensajeOperacion);
						filaPago[1] = true;

						if(this.LogMode ==TEST_MODE)
						{
							Common.WriteLog("{0}:{1}", "Inicio del Método AsignacionCajas.RegistrarTransaccionSAP.registrarResultadoOperacion", "Claro.SiscajasBatch.Data");
							Common.WriteLog("ID_T_TI_CAJA_DIARIO:{0}", Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]));
							Common.WriteLog("RegistraZAP:{0}",RegistraZAP);
						}
						Common.WriteLog("Respuesta desde SAP RFC Zpvu_Rfc_Trs_Cajero_Diario: {0}",mensajeOperacion);

						//						str = String.Format("{0}:{1}", "Se realizó con exito la Transacción Zpvu_Rfc_Trs_Cajero_Diario", mensajeOperacion);
						
					}
					else
					{
						//						str = String.Format("{0}:{1}", "No Se realizó con exito la Transacción Zpvu_Rfc_Trs_Cajero_Diario", mensajeOperacion);
						Common.WriteLog("ERROR Respuesta desde SAP RFC Zpvu_Rfc_Trs_Cajero_Diario: {0}",mensajeOperacion);
					}
					objCajas.Connection.Close();
					
					//					this.LogTransaction.Info(String.Format("RESULTADO DE CAJERO DIARIO: {0}", str));
					Common.WriteLog("------ FIN DE TRANSACCION DE CAJAS ------\n");
				}
				catch (Exception ex)
				{
					deserror="";
					deserror=ex.ToString();
					//this.LogTransaction.Error(String.Format("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Cajero_Diario", "IDCajaDiario: " + Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]), "Claro.SiscajasBatch.Data.AsignacionCajas.RegistrarTransaccionSAP"), ex);

					//					this.LogTransaction.Error(string.Format("************************ ERROR EN LA TRANSACCION DE CAJERO DIARIO  ************************ Numero de Transaccion: {0}",numeroTransaccion) ,ex);
					Common.WriteLog("************************ ERROR EN LA TRANSACCION DE CAJERO DIARIO  ************************ Numero de Transaccion: {0}",numeroTransaccion);

				}
				//REGISTRO EN LOG (T_LOG_TRANSACCION)
				descripcion= descripcionTrs;
				codigo=numeroTransaccion;
				//deserror=descripcionTrs;
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario= Environment.UserDomainName.ToString();
				nombretabla="T_TRS_CAJA_DIARIO";
				long archivo=0;
				RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);
			}

			int enviosConError = cont - contReg;
			if(enviosConError>0)
			{
				mensaje = "No se procesaron las asignaciones";
			}
			else
			{
				mensaje ="Se procesaron correctamente las asignaciones";
			}

			if(cont==0)
			{
				mensaje ="No se encontraron asignaciones";
			}

			Common.WriteLog("Registros leidos: {0}", cont);
			Common.WriteLog("Registros enviados: {0}", contReg);
			Common.WriteLog("Registros no enviados: {0}", cont - contReg);
			Common.WriteLog("---------------------- TRANSACCCION DE CAJERO DIARIO FINALIZADA ----------------------");

			return (cont==contReg || cont == 0);
		}
		
		
		public void RegistrarTransaccionSAP(string fechaActual)
		{
			int cont=0;
			int contReg=0;
			string descripcionTrs="";
			string deserror="";
			string descripcion,codigo, fecha, usuario,nombretabla="";

			DataTable tbNoMigradas = listarCajaDiarioNoMigrados(fechaActual,"");
			tbNoMigradas.Columns.Add("MigracionOK", typeof(bool));
			

			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* INICIANDO TRANSACCION DE ASIGNACION DE CAJAS ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*");
			//log.InfoFormat("\n");
			Common.WriteLog("¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* RFC (Zpvu_Rfc_Trs_Cajero_Diario)¨*¨*¨*¨*¨*¨*¨*¨*¨*¨**");

			foreach (DataRow filaPago in tbNoMigradas.Rows)
			{
				SAP_SIC_Cajas.BAPIRET2Table logSapTable = new SAP_SIC_Cajas.BAPIRET2Table();
				String mensajeOperacion = String.Empty;

				SAP_SIC_Cajas.SAP_SIC_Cajas objCajas = new SAP_SIC_Cajas.SAP_SIC_Cajas(SAPDataConnector.ConnectionString);
				string numeroTransaccion = "";

				try
				{	
					Common.WriteLog("************************** ENVIANDO TRANSACCION DE CAJAS ****************************");

					string Fecha = FormatoFecha( filaPago["FECHA"].ToString());
					string Oficina= filaPago["OFICINA"].ToString();
					string Caja = filaPago["CAJA"].ToString();
					string Usuario = filaPago["USUARIO"].ToString();
					
					Common.WriteLog("{0}:{1}", "Inicio del Método AsignacionCajas.RegistrarTransaccionSAP.Zpvu_Rfc_Trs_Cajero_Diario", "Claro.SiscajasBatch.Data");
					Common.WriteLog("Fecha:{0}", Fecha);
					Common.WriteLog("Oficina:{0}", Oficina);
					Common.WriteLog("Caja:{0}", Caja);
					Common.WriteLog("Usuario:{0}",Usuario);
					
					//CONTADOR REGISTROS LEIDOS
					cont++;
					objCajas.Zpvu_Rfc_Trs_Cajero_Diario(Caja,Fecha,Usuario,Oficina,ref logSapTable);

					DataTable tbLogResult = logSapTable.ToADODataTable();
					int numeroResultadoOperacion = -1;
					foreach (DataRow item in tbLogResult.Rows)
					{
						numeroResultadoOperacion = item[0].Equals("E") ? EJECUCION_ERROR : EJECUCION_OK;
						mensajeOperacion = String.Format("{0}@,{1};{2}", numeroResultadoOperacion, numeroTransaccion, item[3]);
						descripcionTrs =item[3].ToString();
					}

					string str = "";

					if (numeroResultadoOperacion == EJECUCION_OK)
					{
						contReg++;
						string RegistraZAP = "S";
						this.registrarResultadoOperacion(Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]), RegistraZAP, mensajeOperacion);
						filaPago[1] = true;
						//string numOpera = Convert.ToString(IDCajaDiario);

						Common.WriteLog("{0}:{1}", "Inicio del Método AsignacionCajas.RegistrarTransaccionSAP.registrarResultadoOperacion", "Claro.SiscajasBatch.Data");
						Common.WriteLog("ID_T_TI_CAJA_DIARIO:{0}", Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]));
						Common.WriteLog("RegistraZAP:{0}",RegistraZAP);
						Common.WriteLog("mensajeOperacion:{0}",mensajeOperacion);

						str = String.Format("{0}:{1}", "Se realizó con exito la Transacción Zpvu_Rfc_Trs_Cajero_Diario", mensajeOperacion);
					}
					else
					{
						str = String.Format("{0}:{1}", "No Se realizó con exito la Transacción Zpvu_Rfc_Trs_Cajero_Diario", mensajeOperacion);
					}					
					objCajas.Connection.Close();			
					
					Common.WriteLog(String.Format("RESULTADO DE CAJERO DIARIO: {0}", str));//PROY-140126

					//this.LogTransaction.Info(String.Format("{0}:{1}", "Metodo: AsignacionCajas.RegistrarTransaccionSAP.Zpvu_Rfc_Trs_Cajero_Diario", str));	

					Common.WriteLog("*************************** FIN DE TRANSACCION DE CAJAS *****************************\n");//PROY-140126
				}
				catch (Exception ex)
				{
					deserror="";
					deserror=ex.ToString();
					//this.LogTransaction.Error(String.Format("{0}:{1}", "Excepción en la Transacción Zpvu_Rfc_Trs_Cajero_Diario", "IDCajaDiario: " + Convert.ToInt32(filaPago["ID_T_TI_CAJA_DIARIO"]), "Claro.SiscajasBatch.Data.AsignacionCajas.RegistrarTransaccionSAP"), ex);

					Common.WriteLog(string.Format("************************ ERROR EN LA TRANSACCION DE CAJERO DIARIO  ************************ Numero de Transaccion: {0}",numeroTransaccion) +ex);//PROY-140126

				}
				//REGISTRO EN LOG (T_LOG_TRANSACCION)
				descripcion= descripcionTrs;
				codigo=numeroTransaccion;
				//deserror=descripcionTrs;
				fecha = DateTime.Today.ToString("dd/MM/yyyy");
				usuario= Environment.UserDomainName.ToString();
				nombretabla="T_TRS_CAJA_DIARIO";
				long archivo=0;

				RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);	


			}
	                //INI PROY-140126
			Common.WriteLog(String.Format("Total de Registros Leidos: {0}", cont));
			Common.WriteLog(String.Format("Total de Registros Enviados: {0}", contReg));			
			Common.WriteLog("*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨* TRANSACCCION DE CAJERO DIARIO FINALIZADA ¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*¨*");			
                        //FIN PROY-140126

		}
		

		private DataTable listarCajaDiarioNoMigrados(string fecha, string oficina)
		{
			DataSet dsCajaDiario;
			DataTable dtCajaDiario;
			Common obj = new Common();
			dsCajaDiario=obj.listarCajaDiarioNoMigradas(fecha,oficina); 
			dtCajaDiario = dsCajaDiario.Tables[0];
			return dtCajaDiario;
		}


		private void registrarResultadoOperacion(Int32 IDCajaDiario, String RegistraZAP,String mensajeOperacion)
		{
			Int32 i;
			Common obj = new Common();
			i=obj.registrarResultadoCajas(IDCajaDiario,RegistraZAP,mensajeOperacion);
		}


		private void RegistrarLog(string descripcion,string codigo, string deserror, string fecha, string usuario,string nombretabla,long archivo)
		{
			Common obj = new Common();
			obj.RegistrarLog(descripcion,codigo, deserror, fecha, usuario,nombretabla,archivo);
		}
		

		private string FormatoFecha(string Fecha)
		{

			if (Fecha.Length > 0 )
				return Fecha.Substring(6,4) + "/" + Fecha.Substring(3,2) + "/" + Fecha.Substring(0,2);
			else
				return "";

		}


	}
}
